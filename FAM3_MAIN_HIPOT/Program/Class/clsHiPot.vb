'---------------------------------------------------------------
'   clsHiPot
'   Copyright(c)  2016 Yokogawa Manufacturing Corporation.
'
'   Comment: 절연 내전압 검사
'  
'---------------------------------------------------------------
Option Strict Off
Option Explicit On

Imports VB = Microsoft.VisualBasic
Imports System
Imports System.Diagnostics
'Doevents
Imports System.Windows.Forms.Application


Public Class clsHiPot
    Private COND As String = ""

    Dim mAppPath As String = Mid(Application.StartupPath, 1, InStr(Application.StartupPath, "bin") - 1)
    Dim mErrorComment As String
    Dim cADO As cls010_MDB_Operation
    Dim cfrmMain As frmMain

    'Local ================================================
    Dim sJisEnc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
    'Text File ============================================
    Dim w As System.IO.StreamWriter

    Private ResultData As String
    Private ResultData_Current As String
    Dim cFop As cls012_File_Operation
    Private myTos As clsTos9300A
    Dim dfrm As frm500_MessagePopUp

    Private Sub Waits(ByVal Sec As Double)
        System.Threading.Thread.Sleep(CInt(Sec * 1000)) 'Sec = seconds
    End Sub

    Public Function TestMain(ByVal Test_Model As String)
        Dim TestItem As String = ""
        Dim TestModel As String = ""
        Dim TestNo As Integer = 0


        Dim StartTime As Integer = 0
        Dim TestTime As Integer = 0
        Dim CountTime As Integer = 0
        Dim Totalwait As Integer = 0


        Dim dbName As String = mAppPath & "Result\Result_Data\Selftest_Result.mdb"
        Dim tbname As String = "tblRES"
        Dim sSQL As String = ""
        Dim rField As String = ""
        Dim sData() As String = {""}
        Dim fName() As String = {""}
        Dim RecvDataLE As String = ""


        frmMain.TextBox_Log.Text = frmMain.TextBox_StartDate.Text & " " & frmMain.TextBox_StartTime.Text

        Product_Info(frmMain.TextBox_BarCode.Text)
        Try
            StartTime = System.Environment.TickCount

            PLC_Check("01" & "BRDX" & "301" & ",2")

            Test_LogMsgChanged(True, "BOX Open")
            Waits(4)
            myTos = New clsTos9300A(frmMain.TOS9300No)
            myTos.Preset()

            Waits(0.5)
            Do                                                                                          '제품검사 시작 전 커버 열림 유무 확인
                COND = myTos.PortSendReceive("STAT:OPER:TEST:COND?")

                Waits(0.1)

                If COND = 2048 Then                                                                      'Protection Error
                    myTos.Preset()
                    Throw New Exception("Interlock SW is weird." & VB.vbCrLf & "생산기술을 호출해주세요.")

                ElseIf COND = 512 Or COND = 256 Then                                                    'TOS Ready, Standby
                    Exit Do

                Else
                    myTos.Preset()
                    Throw New System.Exception("The instrument status is abnormal." & VB.vbCrLf & " 생산기술을 호출해주세요 : " & CDbl(COND).ToString)
                End If

            Loop

            Try
                Try
                    cADO = New cls010_MDB_Operation

                    cADO.DB_PATH = dbName
                    cADO.TB_NAME = tbName

                    sSQL = "SELECT " & "*" & " FROM " & tbName & " WHERE StartDate = '" & Format(DateTime.Now, "yyyy-MM-dd") & "'"
                    cADO.DB_SQL = sSQL

                    Call cADO.DB_READ_JET(fName, sData)

                    cADO = Nothing

                Catch ex As Exception
                    mErrorComment = cADO.ERR_COMMENT
                    If mErrorComment = "" Then mErrorComment = ex.ToString

                Finally
                    cADO = Nothing
                End Try


                If sData(9) = "Normal" Then
                    cADO = New cls010_MDB_Operation
                    cADO.DB_PATH = mAppPath & "TestDB\TestDB_Model.mdb"
                    tbname = "tblTest_Select"
                    cADO.TB_NAME = tbname
                    rField = "Test_Select_1,Test_Select_2,Test_Select_3,Test_Select_4"
                    sSQL = "SELECT " & rField & " FROM " & tbname & " WHERE 耐電圧型式 = '" & Test_Model & "'"
                    cADO.DB_SQL = sSQL

                    Call cADO.DB_READ_JET(fName, sData)
                Else
                    If frmMain.ComboBox2_selfItem.Text = "Continue" Or frmMain.ComboBox2_selfItem.Text = "Pass" Then
                        Exit Function
                    Else
                    End If
                    cADO = New cls010_MDB_Operation
                    cADO.DB_PATH = mAppPath & "TestDB\TestDB_Model2.mdb"
                    tbName = "tblTest_Select"
                    cADO.TB_NAME = tbname
                    'rField = "Test_Select_1,Test_Select_2,Test_Select_3,Test_Select_4,Test_Select_5,Test_Select_6,Test_Select_7,Test_Select_8,Test_Select_9,Test_Select_10,Test_Select_11,Test_Select_12"          Ver. 0.10 KDH DEL
                    rField = "Test_Select_1,Test_Select_2,Test_Select_3,Test_Select_4,Test_Select_5,Test_Select_6,Test_Select_7,Test_Select_8,Test_Select_9,Test_Select_10,Test_Select_11,Test_Select_12,Test_Select_13"           'Ver. 0.10 KDH ADD

                    sSQL = "SELECT " & rField & " FROM " & tbName & " WHERE 耐電圧型式 = '" & Test_Model & "'"
                    cADO.DB_SQL = sSQL

                    Call cADO.DB_READ_JET(fName, sData)
                End If

                If cADO.DB_DATA_COUNT = 0 Then
                    mErrorComment = "File確認：" & vbCrLf & mAppPath & "TestDB\TestDB_Model.mdb"
                    frmMain.Err_Close(mErrorComment)
                    Exit Function
                End If

                Test_LogMsgChanged(True, "TestDB Read OK")



            Catch ex As Exception

                mErrorComment = cADO.ERR_COMMENT
                Test_LogMsgChanged(True, mErrorComment)
                If mErrorComment = "" Then mErrorComment = ex.ToString
            Finally
                cADO = Nothing

            End Try

            'Test Model, Item ==============================================================
            'For s = 0 To 11            'Ver. 0.10 KDH DEL
            For s = 0 To sData.Length - 1           'Ver. 0.10 KDH ADD
                If sData(s) = "" Then
                    Test_LogMsgChanged(True, "End")
                    Exit For
                Else

                    Test_LogMsgChanged(True, "Test_Select : " & sData(s) & " Ready")
                    myTos.SetAcwStatus(sData(s))
                    Test_LogMsgChanged(True, "Test_Select : " & sData(s) & " Start")
                    myTos.Start()
                    Test_LogMsgChanged(True, "Test_Select : " & sData(s) & " Test Wait")
                    TestWait(sData(s))
                    Test_LogMsgChanged(False, CDbl(frmMain.myDataSet.ACW_TestTime(sData(s))) + CDbl(frmMain.myDataSet.ACW_VoltageRiseTime(sData(s))) + 0.8)

                    For t = 1 To 3

                        If TOS9300_CONDTION(sData(s), Test_Model) = True Then
                            frmMain.TEST_RESULT = "PASS"
                            Exit For
                        End If

                        If t = 3 Then
                            Throw New System.Exception("TimeOut Error, TOS Status : " & CDbl(COND).ToString)

                        Else
                            Waits(0.5)
                        End If
                    Next

                End If
            Next

            CountTime = System.Environment.TickCount
            TestTime = (CountTime - StartTime) / 1000
            frmMain.TextBox_TestTime.Text = TestTime
            Test_LogMsgChanged(False, "TestTime = " & frmMain.TextBox_TestTime.Text)


            '
            '==============================================================
            Try
                dfrm = New frm500_MessagePopUp
                dfrm.DISP_TITLENAME = "Inspection "
                dfrm.DISP_FILENAME = mAppPath & "PPT\PassWin.ppt"
                dfrm.LED_COLOR = "GREEN" : dfrm.BUZZ1 = "OFF" : dfrm.BUZZ2 = "ON" : dfrm.FLASH = "ON"

                Call dfrm.frmShow()
                Do
                    'wait push button -------------------
                    If dfrm.DIALOG_RESULT = "NG" Then
                        mErrorComment = "Setting NG "
                        Throw New ApplicationException(mErrorComment)
                    End If

                    If dfrm.DIALOG_RESULT = "OK" Then
                        Exit Do
                    End If
                    DoEvents()
                Loop
            Catch ex As Exception
                If mErrorComment = "" Then mErrorComment = ex.ToString

            Finally
                dfrm.Close()
                dfrm = Nothing

            End Try

        Catch ex As System.Exception
            Test_LogMsgChanged(True, ex.Message)
            frmMain.Err_Close(ex.Message)
        Finally
            CountTime = System.Environment.TickCount
            Totalwait = (CountTime - StartTime) / 1000
            Test_LogMsgChanged(False, "TotalTime = " & Totalwait)
            Make_ResultLog()
            myTos.Preset()
            PLC_RUN("01" & "BWRY" & "209" & ",008," & "00000000")
        End Try

    End Function

    Private Sub Product_Info(ByVal Barcode As String)
        Test_LogMsgChanged(True, "")
        Test_LogMsgChanged(False, "System = " & frmMain.Text)
        Test_LogMsgChanged(False, "***************")
        Test_LogMsgChanged(False, DateTime.Now)
        Test_LogMsgChanged(False, "Temperature = " & frmMain.TextBox_temp.Text)
        Test_LogMsgChanged(False, "Humidity = " & frmMain.TextBox_humi.Text)
        Test_LogMsgChanged(False, "Operator = " & frmMain.TextBox_op_name.Text)
        Test_LogMsgChanged(False, "BAR = " & Barcode)
        Test_LogMsgChanged(False, "MSCode = " & frmMain.TextBox_MSCode.Text)
        Test_LogMsgChanged(False, "Serial = " & frmMain.TextBox_SerialNo.Text)
        Test_LogMsgChanged(False, "OrderNo = " & frmMain.TextBox_OrderNo.Text)
        Test_LogMsgChanged(False, "XJNo = " & frmMain.TextBox_XjNo.Text)
        Test_LogMsgChanged(False, "TestItem = " & frmMain.ComboBox_TestItem.Text)
        Test_LogMsgChanged(False, "***************")


    End Sub
    Private Sub TOS9300_Result(ByVal COND As String, ByVal type As String, TestNo As Integer)

        Dim DataArray() As String
        'Dim TOS9300test_min As String = 0.738 & " mA"   'Ver 0.01 ATH DEL
        'Dim TOS9300test_max As String = 0.768 & " mA"   'Ver 0.01 ATH DEL
        'Dim TOS9300test_min As String = 0.73 & " mA"   'Ver 0.01 ATH ADD           'Ver 0.07 KDH DEL
        'Dim TOS9300test_max As String = 0.79 & " mA"   'Ver 0.01 ATH ADD           'Ver 0.07 KDH DEL
        Dim TOS9300test_min As String = 0.694 & " mA"   'Ver 0.07 KDH ADD
        Dim TOS9300test_max As String = 0.76 & " mA"   'Ver 0.07 KDH ADD

        '---------------------------------------------- Ver 0.07 KDH ADD Start ------------------------------------------------
        Dim Testmode As String = frmMain.myDataSet.ACDC_Testmode(TestNo).ToString
        Dim ACDC As String = ""
        Select Case Testmode
            Case "ACW"
                ACDC = "VAC"
            Case "DCW"
                ACDC = "VDC"
            Case Else
                Throw New System.Exception("Check TestDB.cls")
        End Select
        '---------------------------------------------- Ver 0.07 KDH ADD End ------------------------------------------------

        DataArray = VB.Split(COND, ",")


        If frmMain.ComboBox2_selfItem.Text = "TOS9300Test" Then

            'Test_LogMsgChanged(False, "[ACW TEST " & frmMain.myDataSet.ACW_No(TestNo) & "] " & frmMain.myDataSet.ACW_TestVoltage(TestNo) & " VAC, " & frmMain.myDataSet.ACW_TestTime(TestNo) & "sec")          Ver 0.07 KDH DEL
            Test_LogMsgChanged(False, "[" & Testmode & " TEST " & frmMain.myDataSet.ACW_No(TestNo) & "] " & frmMain.myDataSet.ACW_TestVoltage(TestNo) & " " & ACDC & ", " & frmMain.myDataSet.ACW_TestTime(TestNo) & "sec")          'Ver 0.07 KDH ADD
            Test_LogMsgChanged(False, "  Scanner 1 : " & frmMain.myDataSet.ACW_Scanner101(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner102(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner103(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner104(TestNo))
            Test_LogMsgChanged(False, "  Scanner 2 : " & frmMain.myDataSet.ACW_Scanner201(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner202(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner203(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner204(TestNo))
            Test_LogMsgChanged(False, "  Scanner 3 : " & frmMain.myDataSet.ACW_Scanner301(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner302(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner303(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner304(TestNo))

            ResultData = (CDbl(DataArray(0)) * 1000).ToString("0.0000") & " mA"
            Test_LogMsgChanged(False, ResultData)

            If ResultData >= TOS9300test_min And ResultData <= TOS9300test_max Then
                ResultData_Current = CDbl(DataArray(0))
            Else
                frmMain.selftest_NG = False
                Throw New System.Exception(" 始業点検NG!!! 生産技術を呼び出してください!: " & CDbl(DataArray(0) * 1000).ToString & " mA NG")
            End If



        Else

            'Test_LogMsgChanged(False, "[ACW TEST " & frmMain.myDataSet.ACW_No(TestNo) & "] " & frmMain.myDataSet.ACW_TestVoltage(TestNo) & " VAC, " & frmMain.myDataSet.ACW_TestTime(TestNo) & "sec")          Ver 0.07 KDH DEL
            Test_LogMsgChanged(False, "[" & Testmode & " TEST " & frmMain.myDataSet.ACW_No(TestNo) & "] " & frmMain.myDataSet.ACW_TestVoltage(TestNo) & " " & ACDC & ", " & frmMain.myDataSet.ACW_TestTime(TestNo) & "sec")          'Ver 0.07 KDH ADD
            Test_LogMsgChanged(False, "  Scanner 1 : " & frmMain.myDataSet.ACW_Scanner101(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner102(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner103(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner104(TestNo))
            Test_LogMsgChanged(False, "  Scanner 2 : " & frmMain.myDataSet.ACW_Scanner201(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner202(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner203(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner204(TestNo))
            Test_LogMsgChanged(False, "  Scanner 3 : " & frmMain.myDataSet.ACW_Scanner301(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner302(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner303(TestNo) & ", " & frmMain.myDataSet.ACW_Scanner304(TestNo))

            ResultData = "  Current : " & (CDbl(DataArray(0)) * 1000).ToString("0.0000") & " mA"
            Test_LogMsgChanged(False, ResultData)                            '전류치

            ResultData = CDbl(DataArray(0)).ToString("0.000000000")
        End If

        myTos.SendOnly("RES:REM:ALL")           'VER 0.09 KDH ADD

    End Sub

    Private Function TOS9300_CONDTION(ByVal TestNo As Integer, ByVal TestItem As String)
        Dim type As String
        frmMain.selftest_NG = True

        COND = myTos.PortSendReceive("STAT:OPER:TEST:COND?")
        type = VB.Left(TestItem, 1)

        Select Case CDbl(COND)
            Case 1                                                                                                          'PASS
                If frmMain.selftest_Fin = False Then
                    If frmMain.ComboBox2_selfItem.Text = "TOS9300Test" Then
                        'TOS9300_Result(myTos.PortSendReceive("MEAS?"), type, TestNo)           VER 0.09 KDH DEL
                        myTos.SendOnly("RES:FORM CURR")         'VER 0.09 KDH ADD
                        TOS9300_Result(myTos.PortSendReceive("RES?"), type, TestNo)             'VER 0.09 KDH ADD
                        Test_LogMsgChanged(False, TestItem & "始業点検 PASS")
                        myTos.TosStop()
                        Return True
                    End If
                    frmMain.TEST_RESULT = "NG"
                    frmMain.selftest_NG = False
                    Throw New System.Exception(TestItem & " 始業点検NG!!! 生産技術を呼び出してください!")
                    Return False

                Else
                    'TOS9300_Result(myTos.PortSendReceive("MEAS?"), type, TestNo)           VER 0.09 KDH DEL
                    myTos.SendOnly("RES:FORM CURR")         'VER 0.09 KDH ADD
                    TOS9300_Result(myTos.PortSendReceive("RES?"), type, TestNo)             'VER 0.09 KDH ADD
                    myTos.TosStop()
                    Test_LogMsgChanged(False, "PASS")
                    Return True
                End If
            Case 2                                                                                                        'L-FAIL
                'TOS9300_Result(myTos.PortSendReceive("MEAS?"), type, TestNo)           VER 0.09 KDH DEL
                myTos.SendOnly("RES:FORM CURR")         'VER 0.09 KDH ADD
                TOS9300_Result(myTos.PortSendReceive("RES?"), type, TestNo)             'VER 0.09 KDH ADD
                frmMain.TEST_RESULT = "NG"
                Throw New System.Exception(TestItem & " Hipot Test NG(L-FAIL), TOS Status : " & CDbl(COND).ToString)
                Test_LogMsgChanged(False, "NG")
                Return False

            Case 4, 12                                                                                                    'U-FAIL
                If frmMain.selftest_Fin = False Then
                    Test_LogMsgChanged(False, TestItem & "始業点検 PASS")
                    myTos.TosStop()
                    Return True
                Else
                    'TOS9300_Result(myTos.PortSendReceive("MEAS?"), type, TestNo)           VER 0.09 KDH DEL
                    myTos.SendOnly("RES:FORM CURR")         'VER 0.09 KDH ADD
                    TOS9300_Result(myTos.PortSendReceive("RES?"), type, TestNo)             'VER 0.09 KDH ADD
                    frmMain.TEST_RESULT = "NG"
                    Throw New System.Exception(TestItem & " Hipot Test NG(U-FAIL), TOS Status : " & CDbl(COND).ToString)
                    Test_LogMsgChanged(False, "NG")
                    Return False
                End If
            Case Else
                '----------------------------------- Ver. 0.10 KDH ADD Start -----------------------------------
                COND = myTos.PortSendReceive("STAT:OPER:PROT:COND?")
                If CDbl(COND) = 256 Then
                    If frmMain.selftest_Fin = False Then
                        Test_LogMsgChanged(False, TestItem & "始業点検 PASS")
                        myTos.TosStop()
                        Return True
                    Else
                        myTos.SendOnly("RES:FORM CURR")
                        TOS9300_Result(myTos.PortSendReceive("RES?"), type, TestNo)
                        frmMain.TEST_RESULT = "NG"
                        Throw New System.Exception(TestItem & " DCV Hipot Test NG, TOS(PROT) Status : " & CDbl(COND).ToString)
                        Test_LogMsgChanged(False, "NG")
                        Return False
                    End If
                Else
                    myTos.SendOnly("RES:FORM CURR")
                    TOS9300_Result(myTos.PortSendReceive("RES?"), type, TestNo)
                    frmMain.TEST_RESULT = "NG"
                    Throw New System.Exception("設備異常!!! 生産技術を呼び出してください! TOS Status : " & CDbl(COND).ToString)
                    Test_LogMsgChanged(False, "NG")
                    Return False
                End If
                '----------------------------------- Ver. 0.10 KDH ADD End -----------------------------------

                'TOS9300_Result(myTos.PortSendReceive("MEAS?"), type, TestNo)           VER 0.09 KDH DEL
                '----------------------------------- Ver. 0.10 KDH Del Start -----------------------------------
                'myTos.SendOnly("RES:FORM CURR")         'VER 0.09 KDH ADD
                'TOS9300_Result(myTos.PortSendReceive("RES?"), type, TestNo)             'VER 0.09 KDH ADD
                'frmMain.TEST_RESULT = "NG"
                'Throw New System.Exception("設備異常!!! 生産技術を呼び出してください! TOS Status : " & CDbl(COND).ToString)
                'Test_LogMsgChanged(False, "NG")
                'Return False
                '----------------------------------- Ver. 0.10 KDH Del End -----------------------------------
        End Select

    End Function
    Private Sub TestWait(ByVal TestNo As Integer)
        Waits(CDbl(frmMain.myDataSet.ACW_TestTime(TestNo)) + CDbl(frmMain.myDataSet.ACW_VoltageRiseTime(TestNo)) + 0.8)
    End Sub
    Private Function PLC_RUN(ByVal scmd As String) As Long
        Dim cLnk As New cls080_F3LinkToLE
        Dim rCmd As String = ""
        Dim mErrorComment As String

        cLnk.IPAdd = "192.168.1.2"
        Try
            Call cLnk.F3PCLinkCommunication(scmd, rCmd)
            If cLnk.ERR_COMMENT <> "" Then
                mErrorComment = cLnk.ERR_COMMENT
                PLC_RUN = 1
                Exit Function
            End If
            PLC_RUN = 0
        Catch ex As Exception
            cLnk = Nothing
            Throw New System.Exception("PLC Communication NG!")
        End Try

    End Function

    Private Function PLC_Check(ByVal scmd As String) As Long
        Dim cLnk As New cls080_F3LinkToLE
        Dim rCmd As String = ""
        Dim StartTime As Double
        Dim NowTime As Double
        Dim ErrTime As Double
        Dim mTimeOut As Double = 20

        cLnk.IPAdd = "192.168.1.2"
        Try

            StartTime = Microsoft.VisualBasic.DateAndTime.Timer()

            Do
                Call cLnk.F3PCLinkCommunication(scmd, rCmd)
                NowTime = Microsoft.VisualBasic.DateAndTime.Timer()
                ErrTime = NowTime - StartTime
                If ErrTime < 0 Then ErrTime = 24.0# * 3600.0# - StartTime + NowTime
                DoEvents()
                If Mid(rCmd, 5, 2) = "11" Then
                    PLC_RUN("01" & "BWRY" & "209" & ",008," & "00000001")   ' box cover close

                    Exit Do
                End If

            Loop While mTimeOut > ErrTime

        Catch ex As Exception
            cLnk = Nothing
            Throw New System.Exception("Switch NG!")
        End Try

    End Function

    Private Sub Test_LogMsgChanged(ByVal tStmp As Boolean, ByVal msg As String)

        'Log
        Dim tNow As String
        If tStmp = True Then
            tNow = "[" & Format(DateTime.Now, "HH:mm:ss") & "] "
        Else
            tNow = ""
        End If


        frmMain.TextBox_Log.Text &= VB.vbCrLf & tNow & msg
        frmMain.TextBox_Log.SelectionStart = frmMain.TextBox_Log.Text.Length
        frmMain.TextBox_Log.Focus()
        frmMain.TextBox_Log.ScrollToCaret()

        System.Windows.Forms.Application.DoEvents() 'Redraw

    End Sub

    Private Sub Make_ResultLog()
        '==============================================================================================================
        ' Function : System log
        ' Data     : Nothing
        ' Return   : Nothing
        ' Remarks  :
        '==============================================================================================================

        Dim res As String() = {}

        'Make Folder ==============================================================================
        Dim SearchArea As String = mAppPath & "\Result\Result_Log"
        Dim SearchFolder As String = Format(DateTime.Now, "yyyy-MM")
        Dim SearchFile As String

        'Search date folder -----------------------------------------
        Try
            cFop = New cls012_File_Operation

            cFop.ROOT_FOLDER = SearchArea
            cFop.FOLDER_NAME = SearchFolder

            Call cFop.FOLDER_SEARCH(res)

        Catch ex As Exception
            mErrorComment = cFop.ERR_COMMENT
            If mErrorComment = "" Then mErrorComment = ex.ToString()

        Finally
            cFop = Nothing

        End Try
        If mErrorComment <> "" Then Exit Sub

        'Empty check ------------------------------------------------
        Dim sFolder As String = String.Empty
        For Each stFilePath As String In res
            sFolder &= stFilePath & System.Environment.NewLine
        Next stFilePath

        'Empty then make folder -------------------------------------
        If sFolder = String.Empty Then
            Try
                cFop = New cls012_File_Operation

                cFop.FOLDER_PATH = SearchArea & "\" & SearchFolder

                Call cFop.FOLDER_MAKE()

            Catch ex As Exception
                mErrorComment = cFop.ERR_COMMENT
                If mErrorComment = "" Then mErrorComment = ex.ToString()

            Finally
                cFop = Nothing

            End Try
            If mErrorComment <> "" Then Exit Sub

        End If

        'Search Log file ------------------------------------------- 
        Try
            cFop = New cls012_File_Operation

            cFop.ROOT_FOLDER = SearchArea & "\" & SearchFolder & "\"
            SearchFile = Format(DateTime.Now, "yyyyMMdd_HHmmss") & "_" & frmMain.TextBox_SerialNo.Text & ".txt"
            cFop.FILE_NAME = SearchFile
            Call cFop.FILE_SEARCH(res)

            Try
                w = New System.IO.StreamWriter(SearchArea & "\" & SearchFolder & "\" & SearchFile, True, sJisEnc)
                w.WriteLine(frmMain.TextBox_Log.Text)

            Catch ex As Exception
                mErrorComment = Mid(ex.ToString, InStr(ex.ToString, ":") + 1, Len(ex.ToString))
                mErrorComment = Left(mErrorComment, InStr(mErrorComment, vbCr) - 1)
                Debug.Print(mErrorComment)

                Throw New ApplicationException(mErrorComment)

            Finally
                w.Close()

            End Try

        Catch ex As Exception
            mErrorComment = cFop.ERR_COMMENT
            If mErrorComment = "" Then mErrorComment = ex.ToString()

        Finally
            cFop = Nothing

        End Try
        If mErrorComment <> "" Then Exit Sub

    End Sub

End Class
