Option Strict On : Option Explicit On
'==========================================================
'Form Name  : Main_Form
'Function   : FAM3 Main HIPOT Test 
'Framwork   : VB2019 Framework4.8
'Reference  : 
'Memo       : FORM_NEWSC 절연 내전압 검사장치
'             기타 기능에 맞춤
'Issue      : 2021/08/23  Rev 0.00    Youngseo.Gweon       New Release

'==========================================================

'GetProcess
Imports System.Diagnostics.Process
'Doevents
Imports System.Windows.Forms.Application
Imports System.Net.Sockets
Imports System.Net
Imports VB = Microsoft.VisualBasic


Public Class frmMain

    'Program Ver ==========================================
    'Private Const Version As String = "0.00"   '2023.05.19 Ver 0.00 ATH
    'Private Const Version As String = "0.01"   '2023.05.30 Ver 0.01 ATH
    'Private Const Version As String = "0.02"   '2023.06.22 Ver 0.02 KSM
    'Private Const Version As String = "0.03"   '2023.06.27 Ver 0.03 KSM
    'Private Const Version As String = "0.04"   '2024.07.24 Ver 0.04 HSJ
    'Private Const Version As String = "0.05"   '2024.09.26 Ver 0.05 KDH
    'Private Const Version As String = "0.06"   '2024.11.28 Ver 0.06 HSJ
    'Private Const Version As String = "0.07"   '2025.03.28 Ver 0.07 KDH
    'Private Const Version As String = "0.08"   '2025.05.26 Ver 0.08 KDH
    'Private Const Version As String = "0.09"   '2025.05.28 Ver 0.09 KDH
    Private Const Version As String = "0.10"   '2025.06.30 Ver 0.10 KDH
    'Local ================================================
    Dim mAppPath As String = Mid(Application.StartupPath, 1, InStr(Application.StartupPath, "bin") - 1)
    Private EtherComN As New EtherComN.Dll

    Dim mErrorComment As String
    Dim DB_Replace As Boolean

    Dim mBarCode As Boolean = False
    Dim mSetting As String = ""
    Dim mPort As String = ""
    Dim mDelim As String = ""

    Dim sInsModel As String = ""
    Dim slnsModel_Find_LM As String = "" 'Ver 0.03 ADD 
    Dim sInSerial As String = ""
    Dim SetVFlag As String = ""

    Dim Systemname As String
    Dim Systemversion As String

    Dim sJisEnc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

    'Class ================================================
    Dim cADO As cls010_MDB_Operation
    Dim cFop As cls012_File_Operation
    Dim cTxt As cls013_TextFile_Operation
    Dim cSrv As cls110_ServerConnect
    Public myDataSet As clsTestDBSet
    Private myTos As clsTos9300A
    Dim cPsm As cls100_PLASMA_Order
    Dim dfrm_Model_Input As Model_Input
    Dim dfrm As frm500_MessagePopUp
    Private myPlc As clsHiPot
    Dim cLnk As cls080_F3LinkToLE

    Dim cBcd As cls101_ComRS232C
    Private Const SerialSelfTest As String = "SelfTest"
    'Private myInfo As clsProductInfo
    'Local Property =======================================
    Dim mSystemLog As String = ""
    Dim Manual As String = "N"
    Public SelfTest As String = "N"
    Private COND As String = ""
    'Private myTOS9300No As String = "AW002329"          'USB Serial Number 'ver 0.06 HSJ del
    'Private myTOS9300No As String = "DN000939"          'USB Serial Number 'ver 0.06 HSJ add 'ver 0.07 KDH del
    'Private myTOS9300No As String = "ZB002990"          'USB Serial Number 'ver 0.07 KDH add 'ver 0.08 KDH del
    'Private myTOS9300No As String = "DN000939"          'USB Serial Number 'ver 0.08 KDH add 'ver 0.09 KDH del
    Private myTOS9300No As String = "FR000554"          'USB Serial Number 'ver 0.09 KDH add

    Public selftest_Fin As Boolean = False
    Public selftest_NG As Boolean = False
    Const Process_Code As String = "FAM3_010"
    Dim RDATE_S As String = ""
    Dim RTIME_S As String = ""
    Private I As Integer
    Dim mInspectionResult As String = ""
    Dim mInspectionTestItem As String = ""
    Dim mInspectionSpec As String = ""
    Dim mInspectionMeasure As String = ""
    Dim mPLASMABarcode As String
    Private myLbTestItem() As System.Windows.Forms.Label

    Public ReadOnly Property TOS9300No() As String
        Get
            TOS9300No = myTOS9300No
        End Get
    End Property
    Public ReadOnly Property SYSTEM_LOG() As String
        Get
            SYSTEM_LOG = mSystemLog
        End Get
    End Property

    Public ReadOnly Property COM_BARCODE() As Boolean
        Get
            COM_BARCODE = mBarCode
        End Get
    End Property

    Public ReadOnly Property COM_BARCODE_SETTING() As String
        Get
            COM_BARCODE_SETTING = mSetting
        End Get
    End Property

    Public ReadOnly Property COM_BARCODE_PORT() As String
        Get
            COM_BARCODE_PORT = mPort
        End Get
    End Property

    Public ReadOnly Property COM_BARCODE_TERMINAL_CHAR() As String
        Get
            COM_BARCODE_TERMINAL_CHAR = mDelim
        End Get
    End Property
    Public Property TEST_RESULT() As String
        Get
            TEST_RESULT = mInspectionResult
        End Get
        Set(ByVal value As String)
            mInspectionResult = value
        End Set
    End Property
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim dbName As String = ""
        Dim tbName As String
        Dim sSQL As String
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim rField As String

        Dim fCount As Integer = 0
        Dim rCount As Integer = 0

        Dim scmd As String = ""
        Dim rCmd As String = ""

        'Application check ===============================
        If UBound(GetProcessesByName(GetCurrentProcess.ProcessName)) > 0 Then
            MessageBox.Show("이 프로그램은 이미 시작하고 있습니다", "Application start NG", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End If

        'Read of BarcodeReader Equipment No. ====================================================================
        Try
            dbName = mAppPath & "Files\EquipmentSetting.mdb"
            tbName = "tblEquipmentSetting"

            cADO = New cls010_MDB_Operation

            cADO.DB_PATH = dbName
            cADO.TB_NAME = tbName

            sSQL = "SELECT " & "BarcodeReaderPort , BarcodeReaderSetting" & " FROM " & tbName
            cADO.DB_SQL = sSQL

            Call cADO.DB_READ_JET(fName, sData)

            fCount = cADO.DB_FIELD_COUNT
            rCount = cADO.DB_RECORD_COUNT

        Catch ex As Exception
            mErrorComment = cADO.ERR_COMMENT
            If mErrorComment = "" Then mErrorComment = ex.ToString()
            Call Me.SysLogWrite(True, "バーコードリーダー：" & mErrorComment)

        Finally
            cADO = Nothing

        End Try

        If rCount <> 0 Then

            If mErrorComment <> "" Then Exit Sub
            Dim BarSetting() As String = Split(sData(1), "-")

            cBcd = New cls101_ComRS232C
            cBcd.TERMINAL_CHAR = vbCr : mDelim = vbCr
            cBcd.RS_COMM = Me.SerialPort1 : cBcd.PORT = sData(0) : mPort = sData(0)
            cBcd.SETTING = BarSetting(0) & "," & BarSetting(1) & "," & BarSetting(2) & "," & BarSetting(3)
            mSetting = BarSetting(0) & "," & BarSetting(1) & "," & BarSetting(2) & "," & BarSetting(3)
            mBarCode = True

            cBcd.PortSetup()

        Else
            TextBox_BarCode.Text = ""

        End If

        'Make of System Log =======================================================================
        MakeSystemLog()
        If mErrorComment <> "" Then
            MessageBox.Show(mErrorComment, "Make System Log", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End If

        'Time, Temp,Humi Check =======================================================================
        Dim msg As String
        msg = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm")
        frmDateCK.TextBox2.Text = msg
        frmDateCK.ShowDialog()
        If frmDateCK.DateCKResult = False Then MyBase.Close()

        'Program Name & Version Check ===============================
        ProgramVerCheck()
        SysLogWrite(True, "Program Version Check OK")

        'OperatorName,ID Check ===============================
        Try
            cADO = Nothing

            cADO = New cls010_MDB_Operation

            cADO.DB_PATH = mAppPath & "Files\EquipmentSetting.mdb"
            tbName = "tblDefNameEntry"
            cADO.TB_NAME = tbName
            rField = "OperatorName,Operatorid"

            sSQL = "SELECT " & rField & " FROM " & tbName
            cADO.DB_SQL = sSQL

            Call cADO.DB_READ_JET(fName, sData)

            If cADO.DB_DATA_COUNT = 0 Then
                mErrorComment = "Unable to retrieve Operator information."
                Throw New ApplicationException(mErrorComment)
            End If

            TextBox_op_name.Text = sData(0)
            TextBox_op_id.Text = sData(1)

            SysLogWrite(True, "OpratorName, ID Read OK")
        Catch
            SysLogWrite(True, "OpratorName, ID Read NG")
            MessageBox.Show(mErrorComment, "OperatorName, ID read Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        Finally
            cADO = Nothing
        End Try

        'MSCODE.mdb Copy & Check ===============================
        Call Button_MS_Update_Click(sender, e)
        SysLogWrite(True, "MSCODE.mdb Update OK")

        'TestDB Read_Test_Item/ACW ===============================
        myTos = New clsTos9300A(myTOS9300No)

        myDataSet = New clsTestDBSet
        myDataSet.TesDBSet()
        SysLogWrite(True, "TestDB Read OK")

        myTos.Preset()
        SysLogWrite(True, "TOS Preset OK")

        COND = myTos.PortSendReceive("STAT:OPER:TEST:COND?")

        'Test Item Set =====================================
        ComboBox_TestItem_Listing()
        ComboBox2_selfItem_Listing()

        'self test check =====================================
        dbName = mAppPath & "Result\Result_Data\Selftest_Result.mdb"
        tbName = "tblRES"

        cADO = New cls010_MDB_Operation

        cADO.DB_PATH = dbName
        cADO.TB_NAME = tbName

        sSQL = "SELECT " & "*" & " FROM " & tbName & " WHERE StartDate = '" & Format(DateTime.Now, "yyyy-MM-dd") & "'"
        cADO.DB_SQL = sSQL

        Call cADO.DB_READ_JET(fName, sData)

        If cADO.DB_DATA_COUNT = 0 Then
            ComboBox2_selfItem.Text = "Continue"
        Else

            sSQL = "SELECT " & "*" & " FROM " & tbName & " WHERE StartDate = '" & Format(DateTime.Now, "yyyy-MM-dd") & "'"
            cADO.DB_SQL = sSQL

            Call cADO.DB_READ_JET(fName, sData)

            For I = 1 To 8
                If sData(I) <> "Pass" Then
                    ComboBox2_selfItem.Text = "Continue"
                    Exit For
                Else
                    ComboBox2_selfItem.Text = "Pass"
                End If
            Next

            cADO = New cls010_MDB_Operation

            cADO.DB_PATH = dbName
            cADO.TB_NAME = tbName

            If ComboBox2_selfItem.Text = "Pass" Then

                sSQL = "UPDATE " & tbName & " SET "
                sSQL &= "TestMode" & " = '" & "Normal" & "'"
                sSQL &= " WHERE StartDate = '" & Format(DateTime.Now, "yyyy-MM-dd") & "'"

                cADO.DB_SQL = sSQL
                Call cADO.DB_EXEC_JET()
                selftest_Fin = True
            End If
        End If

        'Reset =====================================
        'Call Button_ResultClear_Click(sender, e)

        PLC_RUN("192.168.1.2", "01" & "BWRY" & "201" & ",016," & "0000000000000000")


        TextBox_BarCode.Focus()

    End Sub
    Private Sub frmMain_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        PLC_RUN("192.168.1.2", "01" & "BWRY" & "201" & ",016," & "0000000000000000")
        BoxPickingRelayReset()
        cBcd.PortClose()

    End Sub
    Private Delegate Sub COM_Recieve()
    ''==========================================================================================
    ''Function : COM recieve event
    ''Remarks  :
    ''==========================================================================================
    Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived

        AddHandler TextBox_BarCode.TextChanged, AddressOf TextBox_BarCode_TextChanged

        Try
            Me.Invoke(New COM_Recieve(AddressOf BeginningProcess), New Object() {})
        Catch

        Finally

        End Try

        cBcd.PortClose()

        Try
            TextBox_BarCode.Invoke(New COM_Recieve(AddressOf DataWriteProcess), New Object() {})
        Catch

        Finally

        End Try

        cBcd.PortSetup()

        RemoveHandler TextBox_BarCode.TextChanged, AddressOf TextBox_BarCode_TextChanged

    End Sub

    Private Sub BeginningProcess()
        '==========================================================================================
        'Function : Beginning process of inspection
        'Remarks  :
        '==========================================================================================

        Try
            Call cBcd.FuncRecvCmd()
            mPLASMABarcode = cBcd.RECEIVE_DATA

        Catch oExcept As Exception
            mErrorComment = cBcd.ERR_COMMENT : If mErrorComment = "" Then oExcept.ToString()

        End Try

    End Sub

    Private Sub DataWriteProcess()

        TextBox_BarCode.Text = mPLASMABarcode

    End Sub
    Private Sub MakeSystemLog()
        '==============================================================================================================
        ' Function : System log
        ' Data     : Nothing
        ' Return   : Nothing
        ' Remarks  :
        '==============================================================================================================

        Dim res As String() = {}

        'Make Folder ==============================================================================
        Dim SearchArea As String = mAppPath & "System_Log"
        Dim SearchFolder As String = Format(DateTime.Now, "yyyy-MM")
        Dim SearchFile As String = Format(DateTime.Now, "yyyy-MM-dd")

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
        mSystemLog = SearchArea & "\" & SearchFolder & "\" & Format(DateTime.Now, "yyyy-MM-dd") & ".txt"

        Try
            cFop = New cls012_File_Operation

            cFop.ROOT_FOLDER = SearchArea & "\" & SearchFolder & "\"
            cFop.FILE_NAME = Format(DateTime.Now, "yyyy-MM-dd") & ".txt"
            Call cFop.FILE_SEARCH(res)

        Catch ex As Exception
            mErrorComment = cFop.ERR_COMMENT
            If mErrorComment = "" Then mErrorComment = ex.ToString()

        Finally
            cFop = Nothing

        End Try
        If mErrorComment <> "" Then Exit Sub

        'Empty check ------------------------------------------------
        Dim sFile = String.Empty
        For Each stFilePath As String In res
            sFile &= stFilePath & System.Environment.NewLine
        Next stFilePath

        'Log Write --------------------------------------------------
        If sFile = String.Empty Then
            Call Me.SysLogWrite(False, mSystemLog)
        End If

        Call Me.SysLogWrite(False, "")
        Call Me.SysLogWrite(True, "■　システム起動 ==========================================================")

    End Sub

    Public Sub SysLogWrite(ByVal tStmp As Boolean, ByVal msg As String)
        '==============================================================================================================
        ' Function : System Log write
        ' Data     : Time stapY/N , Message
        ' Return   : Nothing
        ' Remarks  :
        '==============================================================================================================

        Try
            cTxt = New cls013_TextFile_Operation
            cTxt.FILE_PATH = mSystemLog

            Call cTxt.WRITE_TEXT_FILE(tStmp, msg)

        Finally
            cTxt = Nothing

        End Try

    End Sub

    Private Sub ProgramVerCheck()
        '==========================================================================================
        'Function : Check program version between Date in VB.NET and Systemsetting.mdb
        'Remarks  :
        '==========================================================================================

        Dim dbName As String = ""
        Dim tbName As String = ""
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim sSQL As String = ""
        Dim fCount As Integer = 0
        Dim rCount As Integer = 0
        Dim rField As String = ""
        Dim kField As String = ""

        'Read of System setting ==================================================================
        Try
            dbName = mAppPath & "Files\SystemSetting.mdb"
            tbName = "tblSystemSetting"

            cADO = New cls010_MDB_Operation

            cADO.DB_PATH = dbName
            cADO.TB_NAME = tbName
            rField = "SystemName,SystemVersion"
            kField = "SystemVersion"

            sSQL = "SELECT " & rField & " FROM " & tbName & " WHERE " & kField & " in "
            sSQL = sSQL & "(SELECT " & "Max(" & kField & ") as maxver" & " FROM " & tbName & ")"
            cADO.DB_SQL = sSQL
            Call cADO.DB_READ_JET(fName, sData)

            If Version <> sData(1) Then
                mErrorComment = "It's not the latest program."
                Throw New ApplicationException(mErrorComment)
            End If

            'Save the Value
            Me.Text = sData(0) & "  Ver : " & sData(1)
            Systemname = sData(0)
            Systemversion = sData(1)

        Catch ex As Exception
            If mErrorComment = "" Then
                mErrorComment = "Check the 'Systemsetting.mdb' "
            End If
            MessageBox.Show(mErrorComment & vbCrLf & "생산기술 호출바랍니다." & vbCrLf & "VB Version : " & Version & vbCrLf & "DB Version : " & sData(0), "Program Check NG")
            End
        Finally
            cADO = Nothing
        End Try
    End Sub

    Private Sub Button_MS_Update_Click(sender As Object, e As EventArgs) Handles Button_MS_Update.Click
        '==============================================================================================================
        ' Function : Get Server file
        ' Data     : Nothing
        ' Return   : Nothing
        ' Remarks  :
        '==============================================================================================================

        Dim dbName As String = ""
        Dim tbName As String = ""
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim sSQL As String = ""
        Dim fCount As Integer = 0
        Dim rCount As Integer = 0
        Dim mServerDrive As String = ""
        Dim mWarningComment As String = ""
        Dim mNormalComment As String = ""

        DB_Replace = True
        TextBox_MS.Text = "미 갱 신"

        'Server Connect ===========================================================================
        Try
            cSrv = New cls110_ServerConnect

            cSrv.FORM_NAME = Me
            Call cSrv.ServerConnect()

            mServerDrive = cSrv.SERVER_DRIVE

        Catch ex As Exception
            mWarningComment = cSrv.ERR_COMMENT
            If mWarningComment = "" Then mWarningComment = ex.ToString()

            mServerDrive = ""

        Finally
            cSrv = Nothing

        End Try
        If mServerDrive = "" Then mWarningComment = "サーバーに接続できません。"

        If mWarningComment <> "" Then
            DB_Replace = False
            Call Me.SysLogWrite(True, "サーバー接続：" & mWarningComment)
            MessageBox.Show(mWarningComment, "MSCODE.mdb", MessageBoxButtons.OK, MessageBoxIcon.Error)

            TextBox_MS.Text = "미 갱 신"
            Exit Sub
        End If

        'Get of Common information ================================================================

        Dim oFileName() As String = {}
        Dim mFoldName() As String = {}
        Dim mFileName() As String = {}

        Dim mFileCount As Integer = 0

        ReDim oFileName(mFileCount)
        ReDim mFoldName(mFileCount)
        ReDim mFileName(mFileCount)

        'Path -------------------------------------------------------

        oFileName(0) = Mid(mServerDrive, 1, 2) & "\001 Run system\Common File\FA-M3\Common Data\MSCODE.mdb "
        mFoldName(0) = mAppPath & "Files\"
        mFileName(0) = "MSCODE.mdb"

        'Copy -------------------------------------------------------
        For i As Integer = 0 To mFileCount

            Try
                cFop = New cls012_File_Operation

                cFop.ORIGINAL_PATH = oFileName(i)
                cFop.FOLDER_PATH = mFoldName(i)
                cFop.FILE_NAME = mFileName(i)

                Call cFop.FILE_COPY()

            Catch ex As Exception
                mErrorComment = cFop.ERR_COMMENT
                If mErrorComment = "" Then mErrorComment = ex.ToString()
                Call Me.SysLogWrite(True, "共通ファイル取得：" & mErrorComment)

                mNormalComment = ""
                DB_Replace = False
                TextBox_MS.Text = "미 갱 신"
                Exit Sub
            Finally
                cFop = Nothing

            End Try

            If mErrorComment = "" Then
                DB_Replace = True
                TextBox_MS.Text = "갱신완료"
                Call Me.SysLogWrite(True, "共通ファイル取得：" & mFileName(i))
                ComboBox1.Focus()
            End If
        Next

        'Server Disconnect ========================================================================
        Try
            cSrv = New cls110_ServerConnect

            Call cSrv.ServerDisconnect()

        Catch ex As Exception
            DB_Replace = False
            mErrorComment = cSrv.ERR_COMMENT
            If mErrorComment = "" Then mErrorComment = ex.ToString()
            Call Me.SysLogWrite(True, "サーバー切断失敗")
        Finally
            cSrv = Nothing

        End Try

        If mErrorComment <> "" Then
            Call Me.SysLogWrite(True, "サーバー切断：" & mErrorComment)
            DB_Replace = False
            TextBox_MS.Text = "미 갱 신"
        End If
    End Sub
    Private Sub ComboBox_TestItem_Listing()
        Dim sSQL As String
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim tbName As String
        Dim rField As String
        Dim count As Integer

        Me.ComboBox_TestItem.DropDownStyle = ComboBoxStyle.DropDownList

        cADO = New cls010_MDB_Operation
        cADO.DB_PATH = mAppPath & "TestDB\TestDB_Model.mdb"
        tbName = "tblTest_Select"
        cADO.TB_NAME = tbName
        rField = "耐電圧型式"

        sSQL = "SELECT " & rField & " FROM " & tbName ' & " WHERE 耐電圧型式 = '" & Test_Model & "'"
        cADO.DB_SQL = sSQL

        Call cADO.DB_READ_JET(fName, sData)

        If cADO.DB_DATA_COUNT = 0 Then
            mErrorComment = "File確認：" & vbCrLf & mAppPath & "TestDB\TestDB_Model.mdb"

        End If

        count = 0
        Do While sData(count) <> "検査なし"
            Me.ComboBox_TestItem.Items.Add(sData(count))
            count += 1
        Loop

    End Sub
    Private Sub ComboBox2_selfItem_Listing()
        Dim sSQL As String
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim tbName As String
        Dim rField As String
        Dim count As Integer

        Me.ComboBox2_selfItem.DropDownStyle = ComboBoxStyle.DropDownList

        cADO = New cls010_MDB_Operation
        cADO.DB_PATH = mAppPath & "TestDB\TestDB_Model2.mdb"
        tbName = "tblTest_Select"
        cADO.TB_NAME = tbName
        rField = "耐電圧型式"

        sSQL = "SELECT " & rField & " FROM " & tbName ' & " WHERE 耐電圧型式 = '" & Test_Model & "'"
        cADO.DB_SQL = sSQL

        Call cADO.DB_READ_JET(fName, sData)

        If cADO.DB_DATA_COUNT = 0 Then
            mErrorComment = "File確認：" & vbCrLf & mAppPath & "TestDB\TestDB_Model2.mdb"

        End If

        count = 0
        Do While sData(count) <> "検査なし"
            Me.ComboBox2_selfItem.Items.Add(sData(count))
            count += 1
        Loop

    End Sub

    Private Sub Button_ResultClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button_ResultClear.Click

        Me.TextBox_MSCode.Text = ""
        Me.TextBox_SerialNo.Text = ""
        Me.TextBox_OrderNo.Text = ""
        Me.TextBox_XjNo.Text = ""
        Me.TextBox_StartTime.Text = ""
        Me.TextBox_TestTime.Text = ""
        Me.TextBox_StartDate.Text = ""
        Me.TextBox_StationNo.Text = ""

        Me.ComboBox_TestItem.Text = ""
        Me.TextBox_BarCode.Text = ""
        'Me.TextBox_Log.Text = "생산정보 BarCode를 Hit해 주십시오!"   'Ver 0.01 ATH DEL
        Me.TextBox_Log.Text = "Log"   'Ver 0.01 ATH ADD

        BoxPickingRelayReset()

    End Sub
    Private Sub TextBox_BarCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox_BarCode.TextChanged
        Dim SumCheck As String = ""
        Dim SumCount As Integer = 0

        cLnk = New cls080_F3LinkToLE

        '作業者情報更新
        If Len(TextBox_BarCode.Text) = 8 Then
            Call Op_Excange(TextBox_BarCode.Text)

            For I = 1 To Len(TextBox_BarCode.Text)
                SumCount = SumCount + Asc(Mid(TextBox_BarCode.Text, I, 1))
            Next I

            SumCheck = CStr(CInt(SumCount + (SumCount / 10) + 1))
            SumCheck = Strings.Right(SumCheck, 2)

            cLnk.IPAdd = "192.168.1.1" : cLnk.Port = 1600
            Call cLnk.SocketCommunication_LabelPrint(TextBox_BarCode.Text & SumCheck)

            Me.TextBox_BarCode.Text = ""
            Me.TextBox_BarCode.Focus()

            cLnk = Nothing
            Exit Sub
        Else

        End If

        ' Click EXCUTE Button ===============================
        If TextBox_BarCode.Text <> "" Then
            cBcd.PortClose()
            Me.Button_Exec_Click(sender, e)
        End If

        ' Click Clear Button ===============================
        Me.Button_ResultClear_Click(sender, e)

    End Sub
    Private Function Op_Excange(ByVal OpID As String) As Boolean
        Dim mPlasmaResult As String = ""
        Dim mErrorCode As String = ""
        Dim sSQL As String = ""

        Dim tbName As String

        Try
            sSQL = OpID & "-E"
            mPlasmaResult = EtherComN.ComHost("10.7.15.254", "10.7.15.140", 2031, "EMP-" & sSQL)

            Debug.Print(mPlasmaResult)

            Select Case Mid(mPlasmaResult, 1, 8)
                Case "ERROR:A0"
                    mErrorCode = mPlasmaResult
                    mErrorComment = "生産情報がありません。このバ-コ-ドは無効です。"
                Case "ERROR:A1"
                    mErrorCode = mPlasmaResult
                    mErrorComment = "生産情報がｷｬﾝｾルされているか。無効の着工札です。"
                Case "ERROR:A2" : Case "ERROR:A3" : Case "ERROR:A4"
                    mErrorCode = mPlasmaResult
                    mErrorComment = "プログラム エラーです。"
                Case "ERROR:A5"
                    mErrorCode = mPlasmaResult
                    mErrorComment = "ＯＲＡＣＬＥエラーです。"
                Case "ERROR:13"
                    mErrorCode = mPlasmaResult
                    mErrorComment = "タイムアウトエラーです。"
                Case ""
                    mErrorCode = mPlasmaResult
                    mErrorComment = "データがありません。"
            End Select
            If mErrorCode <> "" Then Throw New ApplicationException(mErrorComment)

            Op_Excange = True

        Catch ex As Exception
            mErrorComment = Mid(ex.ToString, InStr(ex.ToString, ":") + 1, Len(ex.ToString))
            mErrorComment = Mid(mErrorComment, 1, InStr(mErrorComment, vbCr) - 1)
            Debug.Print(mErrorComment)

            Throw New ApplicationException(mErrorComment)

        End Try

        TextBox_op_name.Text = mPlasmaResult
        TextBox_op_id.Text = OpID

        Try
            cADO = New cls010_MDB_Operation

            cADO.DB_PATH = mAppPath & "Files\EquipmentSetting.mdb"
            tbName = "tblDefNameEntry"
            cADO.TB_NAME = "tblDefNameEntry"

            sSQL = "UPDATE " & tbName & " SET "
            sSQL &= "OperatorName = '" & TextBox_op_name.Text & "' , "
            sSQL &= "Operatorid = '" & OpID & "'"
            cADO.DB_SQL = sSQL

            Call cADO.DB_EXEC_JET()

        Catch ex As Exception
            mErrorComment = Mid(ex.ToString, InStr(ex.ToString, ":") + 1, Len(ex.ToString))
            mErrorComment = Mid(mErrorComment, 1, InStr(mErrorComment, vbCr) - 1)
            Debug.Print(mErrorComment)

            Throw New ApplicationException(mErrorComment)
        Finally
            cADO = Nothing
        End Try

    End Function

    Private Sub Button_Exec_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Manual_Name As String = ""
        Dim rDTkey As String = ""
        Dim dbName As String = ""
        Dim tbName As String = ""
        Dim sSQL As String = ""
        Dim rField As String = ""
        Dim sData() As String = {""}
        Dim fName() As String = {""}

        Dim EF1_Flag As String = ""
        Dim Test_Stop_Flag As String = ""
        Dim Test_Stop_MSG As String = ""

        Dim Test_Model As String = ""

        cLnk = New cls080_F3LinkToLE
        cPsm = New cls100_PLASMA_Order

        RDATE_S = Format(Now, "yyyy/MM/dd")
        RTIME_S = Format(Now, "H:mm:ss")

        Me.TextBox_StartDate.Text = RDATE_S
        Me.TextBox_StartTime.Text = RTIME_S

        'Manual
        If TextBox_MSCode.Text <> "" Then
            Manual = "Y"
        End If

        If Manual = "Y" Then
            Manual_Name = Me.TextBox_MSCode.Text
            sInsModel = Manual_Name
            sInSerial = Me.TextBox_SerialNo.Text

            Call WriteData("RunMode", "MAN")
            Call WriteData("MSCODE", Manual_Name)
            Call WriteData("SerialNo", Me.TextBox_SerialNo.Text)
        Else

            'PLASMAデータ読込み ==============================================
            Try
                'Model name Input
                If TextBox_BarCode.Text.Contains("-") Then
                    Manual = "Y"

                    Manual_Name = Me.TextBox_BarCode.Text
                    sInsModel = Manual_Name

                    dfrm_Model_Input = New Model_Input
                    Call dfrm_Model_Input.Model_Input_Load()

                    Do
                        If dfrm_Model_Input.Result <> "" Then

                            Exit Do
                        End If

                        DoEvents()
                    Loop

                    TextBox_SerialNo.Text = dfrm_Model_Input.Result
                    dfrm_Model_Input = Nothing

                    Call WriteData("RunMode", "BC")
                    Call WriteData("MSCODE", Manual_Name)
                    Call WriteData("SerialNo", Me.TextBox_SerialNo.Text)

                Else
                    Dim recv As String = ""
                    Dim Index_No As String = TextBox_BarCode.Text

                    cPsm.BARCODE_TEXT = TextBox_BarCode.Text
                    rDTkey = TextBox_BarCode.Text
                    Call cPsm.GET_ORDER_INFORMAION()

                    Me.TextBox_MSCode.Text = cPsm.MSCODE
                    Me.TextBox_SerialNo.Text = cPsm.SERIAL
                    Me.TextBox_OrderNo.Text = cPsm.ORDER
                    Me.TextBox_XjNo.Text = cPsm.XJNO
                    sInsModel = cPsm.MSCODE

                    'Temp Dataの更新(Write of data)
                    Call WriteData("RunMode", "AUT")
                    Call WriteData("MSCODE", cPsm.MSCODE)
                    Call WriteData("SerialNo", cPsm.SERIAL)

                    'Label Print ============================================ 
                    Dim SumCheck As String = ""
                    Dim SumCount As Integer = 0

                    For I = 1 To Len(Index_No)
                        SumCount = SumCount + Asc(Mid(Index_No, I, 1))
                    Next I

                    SumCheck = CStr(CInt(SumCount + (SumCount / 10) + 1))
                    SumCheck = Strings.Right(SumCheck, 2)

                    cLnk.IPAdd = "192.168.1.1" : cLnk.Port = 1600
                    Call cLnk.SocketCommunication_LabelPrint(Index_No & SumCheck)

                    Call Me.SysLogWrite(True, "Index No & SumCheck Sent : " & Index_No & SumCheck)

                    'Traceability Data Save ==================================================
                    Traceability_Data_Update(TextBox_BarCode.Text)
                End If

            Catch oExcept As Exception
                mErrorComment = cPsm.ERR_COMMENT : If mErrorComment = "" Then oExcept.ToString()
                Err_Close("PLASMA DATA READ NG " & mErrorComment)
                Exit Sub
            Finally
                cLnk = Nothing
                cPsm = Nothing
            End Try
        End If

        ' ==============================================================
        '入力間違え検出
        If TextBox_BarCode.Text = "" And TextBox_MSCode.Text = "" Then
            Err_Close("There is no barcode data and manual input data.")
            Exit Sub
        End If
        If Manual = "N" And DB_Replace = False Then
            Err_Close("MSCODE.mdb Update NG!! Please try to update the MSCODE.mdb. ")
            Exit Sub
        End If

        slnsModel_Find_LM = Me.TextBox_MSCode.Text 'Ver 0.03 ADD
        'Inspection Model製作 ==============================================================
        subSetModule(sInsModel)

        'Set Value Module 製品ID入力 ==============================================================
        If SetVFlag = "True" Then
            dfrm_Model_Input = New Model_Input


            Call dfrm_Model_Input.Model_Input_Load()

            Do
                If dfrm_Model_Input.Result <> "" Then

                    Exit Do
                End If

                DoEvents()
            Loop

            Manual_Name = dfrm_Model_Input.Result
            dfrm_Model_Input = Nothing

            If Manual_Name = "Cancle" Then
                Err_Close(sInsModel & "→Cancel the bar code entry.")
                dfrm_Model_Input = Nothing
                Exit Sub
            ElseIf Len(Manual_Name) < 9 Then
                Err_Close(sInsModel & "→Model name Barcode abnormal. ")
                dfrm_Model_Input = Nothing
                Exit Sub
            End If

            sInsModel = Manual_Name

            If Manual = "N" Then

                'Make Sum Data ----------------------------------------------
                Dim SumCheck As String = ""
                Dim SumCount As Integer = 0

                For I = 1 To Len(sInsModel)
                    SumCount = SumCount + Asc(Mid(sInsModel, I, 1))
                Next I

                SumCheck = CStr(CInt(SumCount + (SumCount / 10) + 1))
                SumCheck = Strings.Right(SumCheck, 2)

                'Log Write --------------------------------------------------
                Try
                    cTxt = New cls013_TextFile_Operation
                    cTxt.FILE_PATH = "C:\Temp2\" & Me.TextBox_SerialNo.Text & ".txt"

                    If System.IO.File.Exists("C:\Temp2\" & Me.TextBox_SerialNo.Text & ".txt") = True Then

                        VB.Kill("C:\Temp2\" & Me.TextBox_SerialNo.Text & ".txt")
                        WaitTimer(0.5)

                    End If

                    Call cTxt.WRITE_TEXT_FILE(False, sInsModel & SumCheck)

                Finally
                    cTxt = Nothing

                End Try

            End If
        End If

        Me.TextBox_MSCode.Text = sInsModel

        ' 検査中止フラグ確認 ==============================================================
        'MSCODE.mdbファイルの読み込み
        Try
            cADO = New cls010_MDB_Operation
            cADO.DB_PATH = mAppPath & "Files\MSCODE.mdb"
            tbName = "tblMSCODE"
            cADO.TB_NAME = tbName
            rField = "EF1_D0042,検査中止フラグ,検査中止メッセージ,耐電圧型式, 耐電圧メッセージ"

            sSQL = "SELECT " & rField & " FROM " & tbName & " WHERE Model = '" & sInsModel & "'"
            cADO.DB_SQL = sSQL

            Call cADO.DB_READ_JET(fName, sData)

            If cADO.DB_DATA_COUNT = 0 Then
                mErrorComment = "MSCode.mdb could not retrieve the stop inspection flag."
                MsgBox(mErrorComment, MsgBoxStyle.OkOnly, "MSCODEの検査中止Flag")
                Exit Sub
            End If

            'MSCODE.mdb Search Result
            EF1_Flag = sData(0) : Test_Stop_Flag = sData(1) : Test_Stop_MSG = sData(2)
            Test_Model = sData(3)
            'TextBox_StationNo.Text = sData(4)   'Ver 0.01 ATH DEL

            '-----------------Ver 0.01 ATH ADD Start ----------------
            Dim StationNo As String
            StationNo = sData(4).Replace("号機", "")
            TextBox_StationNo.Text = StationNo
            '-----------------Ver 0.01 ATH ADD END ------------------

            '耐電圧型式確認
            If Test_Model = "別" Or Test_Model = "" Then
                MsgBox("Main耐電圧検査対象ではありません。" & vbCrLf & "Please check the model", MsgBoxStyle.OkOnly, "耐電圧型式")
                Exit Sub
            End If

            '検査中止フラグ確認
            If Manual = "N" And Test_Stop_Flag = "True" Then
                MsgBox("検査中止Flagがあります" & vbCrLf & Test_Stop_MSG, MsgBoxStyle.OkOnly, "検査中止Flag")
                Exit Sub
            ElseIf Manual <> "N" And EF1_Flag = "False" Then
                MsgBox("EF1_D0042に無いモデル名です。", MsgBoxStyle.OkOnly, "EF1_D0042_Flag")
                Exit Sub
            End If

        Catch ex As Exception
            mErrorComment = cADO.ERR_COMMENT
            If mErrorComment = "" Then mErrorComment = ex.ToString
            Err_Close(mErrorComment)
            Exit Sub
        Finally
            cADO = Nothing

        End Try

        ' ==============================================================
        BOXPicking(sInsModel)

        ' ==============================================================
        If Test_Model <> "検査なし" Then
            ComboBox_TestItem.SelectedItem = Test_Model
        Else
            Product_Info_Main(TextBox_BarCode.Text)   'Ver 0.01 ATH ADD
            Make_ResultLog_Main()   'Ver 0.01 ATH ADD
        End If

        ' ==============================================================
        If Manual = "N" Then
            ProgDataWrite()
            AWS_Upload()
        End If


    End Sub
    '-------------------------Ver 0.01 ATH ADD Start ------------------------------
    Private Sub Make_ResultLog_Main()
        '==============================================================================================================
        ' Function : System log
        ' Data     : Nothing
        ' Return   : Nothing
        ' Remarks  :
        '==============================================================================================================

        Dim res As String() = {}
        Dim m As System.IO.StreamWriter

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
            SearchFile = Format(DateTime.Now, "yyyyMMdd_HHmmss") & "_" & TextBox_SerialNo.Text & ".txt"
            cFop.FILE_NAME = SearchFile
            Call cFop.FILE_SEARCH(res)

            Try
                m = New System.IO.StreamWriter(SearchArea & "\" & SearchFolder & "\" & SearchFile, True, sJisEnc)
                m.WriteLine(TextBox_Log.Text)

            Catch ex As Exception
                mErrorComment = Mid(ex.ToString, InStr(ex.ToString, ":") + 1, Len(ex.ToString))
                'mErrorComment = Left(mErrorComment, InStr(mErrorComment, vbCr) - 1)
                Debug.Print(mErrorComment)

                Throw New ApplicationException(mErrorComment)

            Finally
                m.Close()

            End Try

        Catch ex As Exception
            mErrorComment = cFop.ERR_COMMENT
            If mErrorComment = "" Then mErrorComment = ex.ToString()

        Finally
            cFop = Nothing

        End Try
        If mErrorComment <> "" Then Exit Sub

    End Sub

    Private Sub Test_LogMsgChanged_Main(ByVal tStmp As Boolean, ByVal msg As String)

        'Log
        Dim tNow As String
        If tStmp = True Then
            tNow = "[" & Format(DateTime.Now, "HH:mm:ss") & "] "
        Else
            tNow = ""
        End If


        TextBox_Log.Text &= VB.vbCrLf & tNow & msg
        TextBox_Log.SelectionStart = TextBox_Log.Text.Length
        TextBox_Log.Focus()
        TextBox_Log.ScrollToCaret()

        System.Windows.Forms.Application.DoEvents() 'Redraw

    End Sub
    Private Sub Product_Info_Main(ByVal Barcode As String)
        Test_LogMsgChanged_Main(True, "")
        Test_LogMsgChanged_Main(False, "System = " & Text)
        Test_LogMsgChanged_Main(False, "***************")
        Test_LogMsgChanged_Main(False, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
        Test_LogMsgChanged_Main(False, "Temperature = " & TextBox_temp.Text)
        Test_LogMsgChanged_Main(False, "Humidity = " & TextBox_humi.Text)
        Test_LogMsgChanged_Main(False, "Operator = " & TextBox_op_name.Text)
        Test_LogMsgChanged_Main(False, "BAR = " & Barcode)
        Test_LogMsgChanged_Main(False, "MSCode = " & TextBox_MSCode.Text)
        Test_LogMsgChanged_Main(False, "Serial = " & TextBox_SerialNo.Text)
        Test_LogMsgChanged_Main(False, "OrderNo = " & TextBox_OrderNo.Text)
        Test_LogMsgChanged_Main(False, "検査なし")
        Test_LogMsgChanged_Main(False, "PASS")
        Test_LogMsgChanged_Main(False, "***************")


    End Sub
    '-------------------------Ver 0.01 ATH ADD END ------------------------------

    Private Sub ProgDataWrite()
        Dim mEtherComResult As String = ""

        Dim mCommand As String
        Dim rCommand(300) As String
        Dim fs As New Scripting.FileSystemObject
        Dim fc As New Scripting.FileSystemObject
        Dim w As System.IO.StreamWriter
        'Dim i_fnum As Integer
        Dim i As Integer

        On Error Resume Next
        If Len(Me.TextBox_BarCode.Text) = 6 And Manual = "N" Then
            ' Update Command Write
            mCommand = "DBRP-\\10.36.14.196\工程進捗管理データ\進捗管理.mdb,\\10.36.14.196\工程進捗管理データ\進捗管理ブランク.mdb,tblProgData,"
            mCommand = mCommand & "計器番号='" & Me.TextBox_SerialNo.Text & "' AND "

            If InStr(Me.TextBox_MSCode.Text, "F3SC") <> 0 Or InStr(Me.TextBox_MSCode.Text, "000321") <> 0 Then
                mCommand = mCommand & "形名='" & Me.TextBox_MSCode.Text & "',"
            Else
                mCommand = mCommand & "形名='" & Me.TextBox_MSCode.Text & "',"
            End If
            mCommand = mCommand & "耐電圧日時=" & Now & ",計器番号=" & Me.TextBox_SerialNo.Text & ","

            If InStr(Me.TextBox_MSCode.Text, "F3SC") <> 0 Or InStr(Me.TextBox_MSCode.Text, "000321") <> 0 Then
                mCommand = mCommand & "形名=" & Me.TextBox_MSCode.Text
            Else
                mCommand = mCommand & "形名=" & Me.TextBox_MSCode.Text
            End If

            mEtherComResult = EtherComN.ComHost("10.36.14.208", "", 1192, mCommand)   '"FAM3TAIATSU"
            Debug.Print(mEtherComResult)

            If InStr(mEtherComResult, "ERROR") <> 0 Then        'Errorが帰ってきたらテキストファイルに書き込む

                'File system
                'i_fnum = FreeFile()
                'Open mAppPath & "\ProgDataWrite.txt" For Append As i_fnum
                'Print #i_fnum, mCommand
                'Close i_fnum

                w = New System.IO.StreamWriter(mAppPath & "\ProgDataWrite.txt", True, sJisEnc)
                w.WriteLine(mCommand)

            Else                                                'テキストファイルに書かれた進捗データの1番上のデータのみ送信

                If fs.FileExists(mAppPath & "\ProgDataWrite.txt") Then

                    fc.OpenTextFile(mAppPath & "\ProgDataWrite.txt")
                    '
                    i = 0
                    Do Until fc.OpenTextFile(mAppPath & "\ProgDataWrite.txt").AtEndOfStream
                        rCommand(i) = fc.OpenTextFile(mAppPath & "\ProgDataWrite.txt").ReadLine()
                        i = i + 1
                    Loop

                    fc = Nothing

                End If

                If rCommand(0) <> "" Then
                    mEtherComResult = EtherComN.ComHost("10.36.14.208", "", 1192, rCommand(0))   '"FAM3TAIATSU"
                    Debug.Print(mEtherComResult)
                Else
                    fs = Nothing
                    Exit Sub
                End If

                If InStr(mEtherComResult, "ERROR") <> 0 Then    'Errorが帰ってきたら何もしない

                Else                                            '書き込みができたら2つ目以下のデータを再度書き込み

                    fc.CreateTextFile(mAppPath & "\ProgDataWrite.txt", True)

                    i = 1
                    Do Until rCommand(i) = ""
                        fc.CreateTextFile(mAppPath & "\ProgDataWrite.txt", True).WriteLine(rCommand(i))
                        i = i + 1
                    Loop

                    fc = Nothing

                End If

                fs = Nothing

            End If

        End If

    End Sub
    Private Sub WriteData(ByVal fName As String, ByVal sData As String)
        '==========================================================================================
        ' Function : Write data(Temporary.mdb)
        ' Data     : Field name , data
        ' Return   : Nothing
        ' Remarks  :
        '==========================================================================================

        Dim dbName As String = ""
        Dim tbName As String = ""
        Dim sSQL As String = ""

        dbName = mAppPath & "Result\Result_Data\Temporary.mdb"
        tbName = "tblResult"

        'Write of data ----------------------------------------------
        sSQL = "UPDATE " & tbName & " SET "
        sSQL &= fName & " = '" & sData & "'"

        Try
            cADO = New cls010_MDB_Operation

            cADO.DB_PATH = dbName
            cADO.TB_NAME = tbName
            cADO.DB_SQL = sSQL

            Call cADO.DB_EXEC_JET()

        Catch ex As Exception
            mErrorComment = cADO.ERR_COMMENT
            If mErrorComment = "" Then mErrorComment = ex.ToString

        Finally
            cADO = Nothing

        End Try

    End Sub
    Public Sub Err_Close(ByVal msg As String)
        '==========================================================================================
        ' Function : Write of message
        ' Data     : Time stamp Y/N , Message
        ' Return   : Nothing
        ' Remarks  :
        '==========================================================================================

        Try
            'Setting message =======================================
            dfrm = New frm500_MessagePopUp
            dfrm.DISP_FILENAME = mAppPath & "PPT\NGWin.ppt"
            dfrm.Button2.Visible = False
            dfrm.Button1.Left = CInt(dfrm.Width / 2) - CInt(dfrm.Button1.Width / 2)
            dfrm.LED_COLOR = "RED" : dfrm.BUZZ1 = "ON" : dfrm.BUZZ2 = "OFF" : dfrm.FLASH = "ON"

            Call dfrm.frmShow()

            Do
                If dfrm.DIALOG_RESULT <> "" Then
                    Exit Do
                End If
                DoEvents()
            Loop

        Catch ex As Exception
            MsgBox("에러 PPT 출력이상, 생산설계 담당자에게 문의 하세요.")
        Finally
            dfrm.Close()
            dfrm = Nothing

            'WRITE_TEXT_FILE(False, "Test Fail : " & msg)
            Call WriteData("Result", "NG")
            Call WriteData("ResultComment", msg)

            'Msg_BOX 確認
            MsgBox(msg, MsgBoxStyle.OkOnly, "NG!!!!")

            'Temp Dataの更新(Write of data)
            Call WriteData("MSCODE", "")
            Call WriteData("SerialNo", "")

            Manual = "N"
        End Try

    End Sub
    Private Sub Traceability_Data_Update(ByVal barcode As String)
        '==============================================================================================================
        ' Function : Traceability_Data save
        ' Data     : Temperary Colum
        ' Return   : Nothing
        ' Remarks  :
        '==============================================================================================================
        Dim dbName As String = ""
        Dim tbName As String = ""
        Dim sSQL As String = ""
        Dim rField As String = ""

        Try
            cADO = New cls010_MDB_Operation

            dbName = mAppPath & "Result\Result_Data\Traceability_Temp.mdb"
            tbName = "tblResult"

            sSQL = "UPDATE " & tbName & " SET "
            sSQL &= "BAR = '" & barcode & "' , "
            sSQL &= "SERIAL = '" & cPsm.SERIAL & "' , "
            sSQL &= "PROD_NO = '" & cPsm.PRDNO & "' , "
            sSQL &= "SAP_LINKAGE_NO = '" & cPsm.LINKG & "' , "
            sSQL &= "SAP_COMP_NO = '" & cPsm.COMP & "' , "
            sSQL &= "CRP_GR_NO = '" & cPsm.CORPG & "' , "
            sSQL &= "START_NO = '" & cPsm.START & "' , "
            sSQL &= "MS_CODE = '" & cPsm.MSCODE & "' , "
            sSQL &= "FINISH_SCHDULE_D = '" & cPsm.FINSC & "' , "
            sSQL &= "INST_FINISH_D = '" & cPsm.INSFN & "' , "
            sSQL &= "XJ_NO = '" & cPsm.XJNO & "' , "
            sSQL &= "PROCESS_CODE = '" & Process_Code & "' , "
            sSQL &= "RDATE_S = '" & RDATE_S & "' , "
            sSQL &= "RTIME_S = '" & RTIME_S & "' , "
            sSQL &= "WORKERID = '" & TextBox_op_id.Text & "' , "
            sSQL &= "INDEX_NO = '" & cPsm.Index & "' , "
            sSQL &= "BYORD_START_NO = '' , "
            sSQL &= "PC_Name = '" & System.Net.Dns.GetHostName & "' , "
            sSQL &= "SOFT = '" & Systemname & "' , "
            sSQL &= "VER = '" & Systemversion & "'"

            cADO.DB_PATH = dbName
            cADO.TB_NAME = tbName
            cADO.DB_SQL = sSQL

            Call cADO.DB_EXEC_JET()

        Catch ex As Exception
            mErrorComment = cADO.ERR_COMMENT
            If mErrorComment = "" Then
                mErrorComment = ex.ToString
            End If
            Err_Close(mErrorComment)
        Finally
            cADO = Nothing
        End Try
    End Sub

    Private Sub subSetModule(ByRef sModel As String)
        '==============================================================================================================
        ' Function : Decision of inspection model
        ' Data     : Inspection model(Return)
        ' Return   : Nothing
        ' Remarks  :
        '==============================================================================================================

        Dim dbName As String = ""
        Dim tbName As String = ""
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim sSQL As String = ""
        Dim fCount As Integer = 0
        Dim rCount As Integer = 0
        Dim rField, kField As String
        Dim model() As String = Split(sModel, ",")
        Dim Suffix As String
        Dim SerModel As String

        If UBound(model) > 1 Then
            mErrorComment = "MSCOdeが一つ以上です。"
            Exit Sub
        End If

        SerModel = model(0)

        If InStr(SerModel, "/CT") > 0 Then

            dbName = mAppPath & "Files\MSCODE.mdb"

            cADO = New cls010_MDB_Operation

            tbName = "tblCT"
            'rField = "MSCode,Option1,Option2,Option3" : kField = "MSCode"
            rField = "*" : kField = "MSCode"

            sSQL = "SELECT " & rField & " FROM " & tbName
            sSQL = sSQL & " WHERE " & kField & " LIKE '" & SerModel & "'"  '& "ORDER BY " & kField        '& " DESC"

            cADO.DB_PATH = dbName
            cADO.TB_NAME = tbName
            cADO.DB_SQL = sSQL

            Call cADO.DB_READ_JET(fName, sData)
            If cADO.DB_DATA_COUNT > 0 Then
                For I = 2 To UBound(sData)
                    If InStr(SerModel, sData(I)) <> 0 And sData(I) <> "" Then
                        SerModel = Replace(SerModel, sData(I), "")
                    End If
                Next
            Else
                If InStr(SerModel, "/CT") <> 0 Then
                    SerModel = Replace(SerModel, "/CT", "")
                End If
            End If
        End If


        dbName = mAppPath & "Files\MSCODE.mdb"

        cADO = New cls010_MDB_Operation

        tbName = "tblMSCODE"
        rField = "SetValue , Model" : kField = "Model"

Reserch1:
        sSQL = "SELECT " & rField & " FROM " & tbName
        sSQL = sSQL & " WHERE " & kField & " LIKE '" & SerModel & "'"  '& "ORDER BY " & kField        '& " DESC"

        cADO.DB_PATH = dbName
        cADO.TB_NAME = tbName
        cADO.DB_SQL = sSQL

        Call cADO.DB_READ_JET(fName, sData)

        If cADO.DB_DATA_COUNT = 0 Then

            If InStr(SerModel, "/") <> 0 Then

                Suffix = Mid(SerModel, InStr(1, SerModel, "/") + 1, Len(SerModel) - InStr(1, SerModel, "/") + 1)
                If InStr(Suffix, "/") <> 0 Then
                    SerModel = Mid(SerModel, 1, InStr(1, SerModel, "/") - 1) & "/" & Mid(Suffix, 1, InStr(1, Suffix, "/") - 1)
                Else
                    SerModel = Mid(SerModel, 1, InStr(1, SerModel, "/") - 1)
                End If
                GoTo Reserch1

            Else

                Err_Close("MSCODE.mdbに形名登録がありません。")
                cADO = Nothing
                Exit Sub

            End If
        End If

        SetVFlag = sData(0)
        sInsModel = SerModel
        cADO = Nothing

    End Sub
    Private Sub Button_TestEnd_Click(sender As Object, e As EventArgs) Handles Button_TestEnd.Click
        PLC_RUN("192.168.1.2", "01" & "BWRY" & "201" & ",016," & "0000000000000000")
        BoxPickingRelayReset()
        cBcd.PortClose()
        System.Windows.Forms.Application.Exit()

    End Sub

    Private Sub BOXPicking(model As String)
        'Dim tbName As String
        Dim sSQL As String
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim rField As String
        Dim mSearchModel As String

        Dim Hako_cmd As String = ""

        Dim mHakoSize As Boolean    'False：シングル,True：ダブル
        Dim mCoating As Boolean    'False：標準,True：コーティング品


        mSearchModel = model

        If InStr(1, slnsModel_Find_LM, "F3YP2") <> 0 And InStr(1, slnsModel_Find_LM, "/L1/M1") <> 0 Then 'Ver 0.03 CHG Me.TextBox_MSCode.Text -> slnsModel_Find_LM
            mSearchModel = model + "/L1/M1"
        End If
        If InStr(1, slnsModel_Find_LM, "F3YP2") <> 0 And InStr(1, slnsModel_Find_LM, "/L2/M1") <> 0 Then 'Ver 0.03 CHG Me.TextBox_MSCode.Text -> slnsModel_Find_LM
            mSearchModel = model + "/L2/M1"
        End If

        Try
            cADO = New cls010_MDB_Operation
            cADO.DB_PATH = mAppPath & "TestDB\" & "BoxSize.xls"
            cADO.TB_NAME = "[Sheet1$]"
            rField = "Single,Double"
            sSQL = "select " & rField & " from " & "[Sheet1$]" & " where FORM like '" & mSearchModel & "'"
            cADO.DB_SQL = sSQL
            Call cADO.DB_READ_EXCEL_JET(fName, sData)

            If cADO.DB_DATA_COUNT = 0 Then
                mErrorComment = "If we don't have any data, we won't do anything."
                MsgBox("BOXPICKING.MDB에 해당 모델이 등록되어있지 않습니다.") 'Ver 0.02 ADD
                Throw New ApplicationException(mErrorComment)
            End If

        Catch ex As Exception
            If mErrorComment = "" Then
                mErrorComment = Mid(ex.ToString, InStr(ex.ToString, ":") + 1, Len(ex.ToString))
                mErrorComment = Mid(mErrorComment, 1, InStr(mErrorComment, vbCr) - 1)
            End If
            Throw New ApplicationException(mErrorComment)
        Finally
            cADO = Nothing
        End Try

        Select Case sData(0)
            Case "○"
                If sData(1) = "" Then
                    mHakoSize = False
                Else
                    Exit Sub
                End If
            Case ""
                If sData(1) = "○" Then
                    mHakoSize = True
                Else
                    Exit Sub
                End If
        End Select

        'FA-M3ﾋﾟｯｷﾝｸﾞLED用ﾘﾚｰの設定

        If mHakoSize = False Then
            If mCoating = False Then Hako_cmd = "10000000"
            If mCoating = True Then Hako_cmd = "00000010"
        ElseIf mHakoSize = True Then
            If mCoating = False Then Hako_cmd = "01000000"
            If mCoating = True Then Hako_cmd = "00000001"
        End If

        PLC_RUN("192.168.0.2", "01BWRI00001,008," & Hako_cmd)

        'PPT ==============================================================
        cADO = New cls010_MDB_Operation
        cADO.DB_PATH = mAppPath & "Files\MSCODE.mdb"
        cADO.TB_NAME = "tblMSCODE"
        rField = "EF1_D0042,検査中止フラグ,検査中止メッセージ,耐電圧型式, 耐電圧メッセージ"

        sSQL = "SELECT " & rField & " FROM " & "tblMSCODE" & " WHERE Model = '" & sInsModel & "'"
        cADO.DB_SQL = sSQL

        Call cADO.DB_READ_JET(fName, sData)
        If sData(3) = "検査なし" Then
            Try
                dfrm = New frm500_MessagePopUp
                dfrm.DISP_TITLENAME = "Inspection "
                dfrm.DISP_FILENAME = mAppPath & "PPT\Connector\x.ppt"

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
                SysLogWrite(True, TextBox_MSCode.Text & "(" & TextBox_SerialNo.Text & ")" & " : " & mErrorComment)
            Finally
                dfrm.Close()
                dfrm = Nothing
            End Try
        End If

    End Sub
    Private Sub BoxPickingRelayReset()

        PLC_RUN("192.168.0.2", "01BWRI00001,008,00000000")

    End Sub

    Private Sub ComboBox_TestItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox_TestItem.SelectedIndexChanged
        Dim dbName As String = mAppPath & "Result\Result_Data\Selftest_Result.mdb"
        Dim tbname As String = "tblRES"
        Dim sSQL As String = ""
        Dim rField As String = ""
        Dim sData() As String = {""}
        Dim fName() As String = {""}
        Dim Test_Model As String = ""
        Dim Manual_Check As Integer
        Dim str As String = sInsModel

        If ComboBox_TestItem.Text = "" Then Exit Sub

        myPlc = New clsHiPot

        Try
            cADO = New cls010_MDB_Operation

            cADO.DB_PATH = dbName
            cADO.TB_NAME = tbname

            sSQL = "SELECT " & "*" & " FROM " & tbname & " WHERE StartDate = '" & Format(DateTime.Now, "yyyy-MM-dd") & "'"
            cADO.DB_SQL = sSQL

            Call cADO.DB_READ_JET(fName, sData)

            If cADO.DB_DATA_COUNT = 0 Then
                MsgBox("시업점검이 완료되지 않았습니다. 시업점검을 실시해 주세요.")
                Exit Sub
            End If

            For I = 1 To 8
                If sData(I) <> "Pass" Or sData(9) <> "Normal" Then
                    MsgBox("시업점검이 완료되지 않았습니다. 시업점검을 실시해 주세요.")
                    Exit Sub
                End If
            Next

            If TextBox_BarCode.Text = "" Then Manual = "Y"

            If Manual = "Y" Then
                Manual_Check = MsgBox("수동검사를 진행하시겠습니까?", MsgBoxStyle.YesNo, "수동검사 확인")
                If Manual_Check = 6 Then      'Yes Return
                Else
                    Exit Sub
                End If
            End If

            'PPT ==============================================================

            Try
                dfrm = New frm500_MessagePopUp
                dfrm.DISP_TITLENAME = "Inspection "
                'dfrm.DISP_FILENAME = mAppPath & "PPT\Connector\" & TextBox_BarCode.Text & ".ppt"
                If TextBox_BarCode.Text = "検査なし" Then
                    dfrm.DISP_FILENAME = mAppPath & "PPT\Connector\x.ppt"
                Else
                    If sInsModel <> "" Then
                        Dim pptmodel As String = str.Substring(0, 9)
                        dfrm.DISP_FILENAME = mAppPath & "PPT\Connector\" & pptmodel & ".ppt"
                    Else
                        dfrm.DISP_FILENAME = mAppPath & "PPT\Connector\" & ComboBox_TestItem.Text & ".ppt"
                    End If
                End If
                dfrm.LED_COLOR = "YELLOW" : dfrm.BUZZ1 = "OFF" : dfrm.BUZZ2 = "OFF" : dfrm.FLASH = "ON"

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
                SysLogWrite(True, TextBox_MSCode.Text & "(" & TextBox_SerialNo.Text & ")" & " : " & mErrorComment)
                Exit Sub
            Finally
                dfrm.Close()
                dfrm = Nothing
            End Try

            myPlc.TestMain(ComboBox_TestItem.Text)

        Catch ex As Exception
            mErrorComment = cADO.ERR_COMMENT
            If mErrorComment = "" Then mErrorComment = ex.ToString

        Finally
            cADO = Nothing
        End Try

        Me.ComboBox_TestItem.Text = ""
        Manual = "N"
        ' panel
        'List BOX Modeling

    End Sub


    Private Sub AWS_Upload()
        '==========================================================================================
        'Function : 生産情報更新
        'Remarks  : AWS Serverに登録
        '==========================================================================================
        Dim mPlasmaResult As String = ""
        Dim Send_Table As String = "YMFK_FAM3_PROD_LOG_TB"
        Dim Send_clonm As String = ""
        Dim Send_Data As String = ""
        Dim sSQL As String = ""
        Dim mErrorCode As String = ""

        Dim RDATE_E As String = Format(Now, "yyyy/MM/dd")
        Dim RTIME_E As String = Format(Now, "H:mm:ss")

        Dim dbName As String = ""
        Dim tbName As String = ""
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim fCount As Integer = 0
        Dim rCount As Integer = 0
        Dim mErrorComment2 As String = ""

        cADO = New cls010_MDB_Operation

        If Manual = "N" Then

            Try
                'Get of temporary data ====================================================================
                dbName = mAppPath & "Result\Result_Data\Traceability_Temp.mdb"
                tbName = "tblResult"

                sSQL = "SELECT * FROM " & tbName

                cADO = New cls010_MDB_Operation

                cADO.DB_PATH = dbName
                cADO.TB_NAME = tbName
                cADO.DB_SQL = sSQL

                Call cADO.DB_READ_JET(fName, sData)

                fCount = cADO.DB_FIELD_COUNT
                rCount = cADO.DB_RECORD_COUNT

                If rCount = 0 Or fCount = 0 Then
                    Err_Close("Traceability_Temp Data Nothing")
                End If
            Catch ex As Exception

            End Try

            If mInspectionResult = "PASS" Or mInspectionResult = "" Then
                Try
                    ' 1:RECNO      2    3       4          5            6           7         8       9          10               11        12       13
                    Send_clonm = "BAR,SERIAL,PROD_NO,SAP_LINKAGE_NO,SAP_COMP_NO,CRP_GR_NO,START_NO,MS_CODE,FINISH_SCHDULE_D,INST_FINISH_D,XJ_NO,PROCESS_CODE"
                    Send_clonm = Send_clonm & ",RDATE_S,RTIME_S,RDATE_E,RTIME_E,WORKERID,INDEX_NO,BYORD_START_NO,PC_NAME,SOFT,VER"
                    '                             14      15      16      17       18       19        20           21     22   23

                    '                    2                  3                  4                  5                   6                7
                    Send_Data = "'" & sData(0) & "','" & sData(1) & "','" & sData(2) & "','" & sData(3) & "','" & sData(4) & "','" & sData(5)
                    Send_Data = Send_Data & "','" & sData(6) & "','" & sData(7) & "','" & sData(8) & "','" & sData(9) & "','" & sData(10) & "','" & sData(11)
                    '                                   8                9                  10                 11                  12                 13
                    '                                  14                  15                 16                17                 18                  19                   20
                    Send_Data = Send_Data & "','" & sData(12) & "','" & sData(13) & "','" & RDATE_E & "','" & RTIME_E & "','" & sData(14) & "','" & sData(15) & "','" & sData(16)
                    Send_Data = Send_Data & "','" & sData(17) & "','" & sData(18) & "','" & sData(19) & "'"
                    '                                  21                  22                 23

                    sSQL = Send_Table & "|" & Send_clonm & "|" & Send_Data

                    mPlasmaResult = EtherComN.ComHost("10.7.15.254", "10.7.15.140", 2031, "QDBW-" & sSQL)
                    Debug.Print(mPlasmaResult)

                    Select Case Mid(mPlasmaResult, 1, 8)
                        Case "ERROR:A0"
                            mErrorCode = mPlasmaResult
                            mErrorComment = "生産情報がありません。このバーコードは無効です。"
                        Case "ERROR:A1"
                            mErrorCode = mPlasmaResult
                            mErrorComment = "生産情報がｷｬﾝｾﾙされています。無効の着工札です。"
                        Case "ERROR:A2" : Case "ERROR:A3" : Case "ERROR:A4"
                            mErrorCode = mPlasmaResult
                            mErrorComment = "プログラムエラーです。"
                        Case "ERROR:A5"
                            mErrorCode = mPlasmaResult
                            mErrorComment = "ＯＲＡＣＬＥエラーです。"
                        Case "ERROR:13"
                            mErrorCode = mPlasmaResult
                            mErrorComment = "タイムアウトエラーです。"
                        Case ""
                            mErrorCode = mPlasmaResult
                            mErrorComment = "データがありません。"
                        Case Else

                    End Select

                    If mErrorCode <> "" Then Throw New ApplicationException(mErrorComment)
                    mErrorComment = ""
                Catch ex As Exception
                    Err_Close("AWS Data Up Load NG")
                Finally
                    cPsm = Nothing
                    cADO = Nothing
                End Try
            Else
                Try
                    If mInspectionMeasure = "" Then mInspectionMeasure = "-----"
                    If mInspectionSpec = "" Then mInspectionSpec = "-----"
                    If mInspectionTestItem = "" Then mInspectionSpec = "-----"

                    Send_Table = "YMFK_FAM3_NG_LOG_TB"

                    ' 1:RECNO        2      3        4          5       6      7       8           9            10        11     12    13
                    Send_clonm = "MS_CODE,SERIAL,STARTDATE,STARTTIME,RUNMODE,RESULT,TESTNAME,RESULTCOMMENT,TESTMEASURE,TESTSPEC,XJNO,ORDERNO"
                    Send_clonm = Send_clonm & ",ITEMNO,COMPNO,QICREQUEST,OPERATOR,APPROVER,TEMPERATURE,HUMIDITY,SYSTEMNAME,SYSTEMVERSION,SYSTEMVERSIONDATE"
                    '                             14     15      16         17       18         19        20         21         22              23

                    '                                24           25              26          27      28     38        
                    Send_clonm = Send_clonm & ",EQUIPMENTNO,INSPECTIONMODEL,INSPECTIONFILE,ENDDATE,ENDTIME,LINKAGE"

                    '                    2                  3                   4                   5                 6              7
                    Send_Data = "'" & sData(7) & "','" & sData(1) & "','" & sData(12) & "','" & sData(13) & "','" & "AUT" & "','" & "NG"
                    Send_Data = Send_Data & "','" & mInspectionTestItem & "','" & TEMP_Read("ResultComment") & "','" & mInspectionMeasure & "','" & mInspectionSpec & "','" & sData(10)
                    '                                        8                                9                                10                         11                   12     
                    '                                         13                            14                      15                16              17               18     
                    Send_Data = Send_Data & "','" & Mid(sData(3), 1, 10) & "','" & Mid(sData(3), 11, 6) & "','" & sData(4) & "','" & " " & "','" & sData(14) & "','" & " "
                    Send_Data = Send_Data & "','" & " " & "','" & " " & "','" & sData(18) & "','" & sData(19) & "','" & " " & "','" & "01"
                    '                               19            20               21                  22                23             24

                    '                                  25              26              27                28                38     
                    Send_Data = Send_Data & "','" & sData(2) & "','" & " " & "','" & RDATE_E & "','" & RTIME_E & "','" & sData(3) & "'"

                    sSQL = Send_Table & "|" & Send_clonm & "|" & Send_Data

                    mPlasmaResult = EtherComN.ComHost("10.7.15.254", "10.7.15.140", 2031, "QDBW-" & sSQL)     'Ver 0.36 GYS ADD
                    Debug.Print(mPlasmaResult)

                    Me.SysLogWrite(True, mPlasmaResult)

                    Select Case Mid(mPlasmaResult, 1, 8)
                        Case "ERROR:A0"
                            mErrorComment2 = "生産情報がありません。このバーコードは無効です。"
                        Case "ERROR:A1"
                            mErrorComment2 = "生産情報がｷｬﾝｾﾙされています。無効の着工札です。"
                        Case "ERROR:A2" : Case "ERROR:A3" : Case "ERROR:A4"
                            mErrorComment2 = "プログラムエラーです。"
                        Case "ERROR:A5"
                            mErrorComment2 = "ＯＲＡＣＬＥエラーです。"
                        Case "ERROR:13"
                            mErrorComment2 = "タイムアウトエラーです。"
                        Case ""
                            mErrorComment2 = "データがありません。"
                        Case Else

                    End Select

                    If mErrorComment2 <> "" Then Throw New ApplicationException(mErrorComment)

                    dbName = "\\10.36.14.215\Fam3\Err_Data\" & Format(DateTime.Now, "yyyy-MM") & "_NG.mdb"
                    tbName = "tblResult"

                    sSQL = "INSERT INTO " & tbName & " ("
                    sSQL = sSQL & "StartDateTime,TestName,ResultComment,TestMeasure,TestSpec,MSCODE,SerialNo,Operator,Approver,SystemName"
                    sSQL = sSQL & ",RunMode,Result,XJNo,OrderNo,ItemNo,CompNo,SystemVersion,EquipmentNo,InspectionModel,EndDateTime,Linkage"
                    sSQL = sSQL & ") VALUES ("
                    sSQL = sSQL & "'" & sData(12) & " " & sData(13) & "' , "
                    sSQL = sSQL & "'" & mInspectionTestItem & "' , "
                    sSQL = sSQL & "'" & TEMP_Read("ResultComment") & "' , "
                    sSQL = sSQL & "'" & mInspectionMeasure & "' , "
                    sSQL = sSQL & "'" & mInspectionSpec & "' , "
                    sSQL = sSQL & "'" & sData(7) & "' , "
                    sSQL = sSQL & "'" & sData(1) & "' , "
                    sSQL = sSQL & "'" & sData(14) & "' , "
                    sSQL = sSQL & "'" & "---" & "' , "
                    sSQL = sSQL & "'" & sData(18) & "' , "
                    sSQL = sSQL & "'" & "AUT" & "' , "
                    sSQL = sSQL & "'" & "NG" & "' , "
                    sSQL = sSQL & "'" & sData(10) & "' , "
                    sSQL = sSQL & "'" & Mid(sData(3), 1, 10) & "' , "
                    sSQL = sSQL & "'" & Mid(sData(3), 11, 6) & "' , "
                    sSQL = sSQL & "'" & sData(4) & "' , "
                    sSQL = sSQL & "'" & sData(19) & "' , "
                    sSQL = sSQL & "'" & "1" & "' , "
                    sSQL = sSQL & "'" & sData(7) & "' , "
                    sSQL = sSQL & "'" & RDATE_E & " " & RTIME_E & "' , "
                    sSQL = sSQL & "'" & sData(3) & "'"
                    sSQL = sSQL & ");"

                    cADO = New cls010_MDB_Operation

                    cADO.DB_PATH = dbName
                    cADO.TB_NAME = tbName

                    cADO.DB_SQL = sSQL

                    Call cADO.DB_EXEC_JET()

                    'NG品の“StartDateTime、MSCODE、SerialNo”をmdbに保存、処理票印字SystemのSearch用
                    dbName = "\\10.36.14.215\Fam3\Err_Data\TestSearch.mdb"
                    tbName = "tblDate"

                    sSQL = "INSERT INTO " & tbName & " (StartDateTime , MSCODE , SerialNo) VALUES ('" & Format(DateTime.Now, "yyyy-MM-dd") & " " & sData(13) & "' , '" & sData(7) & "' , '" & sData(1) & "')"
                    cADO.DB_SQL = sSQL

                    cADO.DB_PATH = dbName
                    cADO.TB_NAME = tbName
                    Call cADO.DB_EXEC_JET()

                Catch ex As Exception
                    Err_Close("AWS Data Up Load NG")
                Finally
                    cPsm = Nothing
                    cADO = Nothing
                End Try
            End If

        End If

    End Sub
    Private Function TEMP_Read(ByVal Column As String) As String

        '検査型式の読込み
        Dim dbName As String = mAppPath & "Result\Result_Data\Temporary.mdb"
        Dim rDTkey As String = ""
        Dim tbName As String
        Dim rField As String = Column
        Dim sSQL As String
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim Model As String = ""

        TEMP_Read = ""

        Try
            cADO = New cls010_MDB_Operation
            cADO.DB_PATH = dbName
            tbName = "tblResult"
            cADO.TB_NAME = tbName

            sSQL = "SELECT " & rField & " FROM " & tbName
            cADO.DB_SQL = sSQL

            Call cADO.DB_READ_JET(fName, sData)

            TEMP_Read = sData(0)

        Catch ex As Exception

        Finally
            cADO = Nothing
        End Try

    End Function
    Private Function PLC_RUN(ByVal IP As String, ByVal scmd As String) As Long
        Dim cLnk As New cls080_F3LinkToLE
        Dim rCmd As String = ""
        Dim mErrorComment As String

        cLnk.IPAdd = IP

        Try
            Call cLnk.F3PCLinkCommunication(scmd, rCmd)
            If cLnk.ERR_COMMENT <> "" Then
                mErrorComment = cLnk.ERR_COMMENT
                Exit Function
            End If
            PLC_RUN = 0
        Catch ex As Exception
            Throw New System.Exception("PLC Commnucation NG!")
        Finally
            cLnk = Nothing
        End Try

    End Function
    Private Function PLC_RUN2(ByVal scmd As String) As Long
        Dim cLnk As New cls080_F3LinkToLE
        Dim rCmd As String = ""
        Dim mErrorComment As String

        cLnk.IPAdd = "192.168.1.2"
        Try
            Call cLnk.F3PCLinkCommunication(scmd, rCmd)
            If cLnk.ERR_COMMENT <> "" Then
                mErrorComment = cLnk.ERR_COMMENT
                PLC_RUN2 = 1
                Exit Function
            End If
            PLC_RUN2 = 0
        Catch ex As Exception
            Throw New System.Exception("PLC Communication NG!")
        Finally
            cLnk = Nothing
        End Try

    End Function

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2_selfItem.SelectedIndexChanged
        'Dim j As Integer
        Dim dbName As String = mAppPath & "Result\Result_Data\Selftest_Result.mdb"
        Dim tbname As String = "tblRES"
        Dim sSQL As String = ""
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim kField As String = ""
        Dim SearchR As String = ""
        Dim InputSetReg2 As Single = 0.0
        Dim mOIC As String = ""
        Dim Test_Status As String = "Continue"

        If ComboBox2_selfItem.Text = "" Then
            Exit Sub
        End If
        selftest_Fin = False

        Try
            cADO = New cls010_MDB_Operation

            cADO.DB_PATH = dbName
            cADO.TB_NAME = tbname

            sSQL = "SELECT " & "*" & " FROM " & tbname & " WHERE StartDate = '" & Format(DateTime.Now, "yyyy-MM-dd") & "'"
            cADO.DB_SQL = sSQL

            Call cADO.DB_READ_JET(fName, sData)

            If cADO.DB_DATA_COUNT = 0 Then
                sSQL = "INSERT INTO " & tbname & " ("
                sSQL = sSQL & "StartDate" & ", "
                sSQL = sSQL & "TestMode" & ", "
                sSQL = sSQL & ComboBox2_selfItem.Text
                sSQL = sSQL & ") VALUES("
                sSQL = sSQL & "'" & Format(DateTime.Now, "yyyy-MM-dd") & "', "
                sSQL = sSQL & "'" & "Self" & "', "
                sSQL = sSQL & "'" & "Fail" & "'"
                sSQL = sSQL & ")"
            Else

                sSQL = "UPDATE " & tbname & " SET "
                sSQL &= "TestMode" & " = '" & "Self" & "' , "
                sSQL &= ComboBox2_selfItem.Text & " = '" & "Fail" & "'"
                sSQL &= " WHERE StartDate = '" & Format(DateTime.Now, "yyyy-MM-dd") & "'"
            End If

            cADO.DB_SQL = sSQL
            Call cADO.DB_EXEC_JET()

            cADO = Nothing

        Catch ex As Exception
            mErrorComment = cADO.ERR_COMMENT
            If mErrorComment = "" Then mErrorComment = ex.ToString

        Finally
            cADO = Nothing
        End Try

        'PPT ==============================================================
        If ComboBox2_selfItem.Text = "Continue" Or ComboBox2_selfItem.Text = "Pass" Then

        Else

            Try
                dfrm = New frm500_MessagePopUp
                dfrm.DISP_TITLENAME = "Inspection "
                'dfrm.DISP_FILENAME = mAppPath & "PPT\Connector\" & ComboBox2_selfItem.Text & ".ppt"
                If ComboBox2_selfItem.Text = "検査なし" Then
                    dfrm.DISP_FILENAME = mAppPath & "PPT\Connector\x.ppt"
                Else
                    'dfrm.DISP_FILENAME = mAppPath & "PPT\Connector\t.ppt"
                    dfrm.DISP_FILENAME = mAppPath & "PPT\Connector\" & ComboBox2_selfItem.Text & ".ppt"
                End If
                dfrm.LED_COLOR = "YELLOW" : dfrm.BUZZ1 = "OFF" : dfrm.BUZZ2 = "OFF" : dfrm.FLASH = "ON"

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
                SysLogWrite(True, TextBox_MSCode.Text & "(" & TextBox_SerialNo.Text & ")" & " : " & mErrorComment)
                Exit Sub
            Finally
                dfrm.Close()
                dfrm = Nothing
            End Try

            myPlc = New clsHiPot

            myPlc.TestMain(ComboBox2_selfItem.Text)
        End If

        Try
            If selftest_NG = False Then
                Exit Try
            End If
            cADO = New cls010_MDB_Operation

            cADO.DB_PATH = dbName
            cADO.TB_NAME = tbname



            sSQL = "UPDATE " & tbname & " SET "
            sSQL &= ComboBox2_selfItem.Text & " = '" & "Pass" & "'"
            sSQL &= " WHERE StartDate = '" & Format(DateTime.Now, "yyyy-MM-dd") & "'"

            cADO.DB_SQL = sSQL
            Call cADO.DB_EXEC_JET()


            sSQL = "SELECT " & "*" & " FROM " & tbname & " WHERE StartDate = '" & Format(DateTime.Now, "yyyy-MM-dd") & "'"
            cADO.DB_SQL = sSQL

            Call cADO.DB_READ_JET(fName, sData)

            For I = 1 To 8
                If sData(I) <> "Pass" Then
                    ComboBox2_selfItem.Text = "Continue"
                    Exit For
                Else
                    ComboBox2_selfItem.Text = "Pass"
                End If
            Next

            cADO = New cls010_MDB_Operation

            cADO.DB_PATH = dbName
            cADO.TB_NAME = tbname

            If ComboBox2_selfItem.Text = "Pass" Then

                sSQL = "UPDATE " & tbname & " SET "
                sSQL &= "TestMode" & " = '" & "Normal" & "'"
                sSQL &= " WHERE StartDate = '" & Format(DateTime.Now, "yyyy-MM-dd") & "'"

                cADO.DB_SQL = sSQL
                Call cADO.DB_EXEC_JET()
                selftest_Fin = True
            End If


        Catch ex As Exception
            mErrorComment = cADO.ERR_COMMENT
            If mErrorComment = "" Then mErrorComment = ex.ToString

        Finally
            cADO = Nothing
        End Try

    End Sub

    Private Sub WaitTimer(ByVal tm As Double)
        '==========================================================================================
        ' Function : Wait timer
        ' Data     : tm : Wait time(Sec)
        ' Return   : Nothing
        ' Remarks  :
        '==========================================================================================

        Dim sTime As Double
        Dim nTime As Double
        Dim eTime As Double

        sTime = Microsoft.VisualBasic.DateAndTime.Timer()

        Do While tm > eTime
            nTime = Microsoft.VisualBasic.DateAndTime.Timer()
            eTime = nTime - sTime
            If eTime < 0 Then eTime = 24.0# * 3600.0# - sTime + nTime
        Loop

        Debug.Print("Wait:" & CStr(eTime))

    End Sub

    Private Sub TextBox_op_name_TextChanged(sender As Object, e As EventArgs) Handles TextBox_op_name.TextChanged

    End Sub

    Private Sub TextBox_MSCode_TextChanged(sender As Object, e As EventArgs) Handles TextBox_MSCode.TextChanged

    End Sub

    Private Sub TextBox_SerialNo_TextChanged(sender As Object, e As EventArgs) Handles TextBox_SerialNo.TextChanged

    End Sub

    Private Sub GroupBox_ProdSel_Enter(sender As Object, e As EventArgs) Handles GroupBox_ProdSel.Enter

    End Sub

    Private Sub TextBox_StationNo_TextChanged(sender As Object, e As EventArgs) Handles TextBox_StationNo.TextChanged

    End Sub

    Private Sub Label23_Click(sender As Object, e As EventArgs) 

    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click

    End Sub
End Class