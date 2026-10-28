
Option Strict On
Option Explicit On

Imports VB = Microsoft.VisualBasic

Public Class frmDateCK
    Public myDateCKResult As Boolean = False

    Dim cADO As cls010_MDB_Operation
    Dim myModbus1 As clsNeptune
    Dim Temp As Integer
    Dim Hum As Integer
    Dim Revdata As Double
    Dim a As String

    Dim mWarningComment As String = ""
    Dim mAppPath As String = Mid(Application.StartupPath, 1, InStr(Application.StartupPath, "bin") - 1)

    Public ReadOnly Property DateCKResult() As Boolean
        Get
            Return myDateCKResult
        End Get
    End Property
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        SaveData()

        If Temp > 18 And Temp < 28 And Hum > 20 And Hum < 80 Then
            myDateCKResult = True
            Me.Close()
        Else
            myDateCKResult = False
            MsgBox("온습도 설정 NG!!" & vbCrLf & "다시 한번 확인해주세요!", MsgBoxStyle.OkOnly, "Temperature&Humidity")
            Temp_Humi_Text.Visible = True
            Temp_Text.Visible = True
            Humi_Text.Visible = True
            cmbTempHumi_1.Visible = True
            cmbTempHumi_2.Visible = True
            Exit Sub
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MYdateCKResult = False
        Me.Close()
    End Sub


    Private Sub frmDateCK_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Load Temperature , Humidity
        ReadTempHumi()

    End Sub
    Private Sub ReadTempHumi()
        '==============================================================================================================
        ' Function : Form draw by process result
        ' Data     : Nothing
        ' Return   : Nothing
        ' Remarks  :
        '==============================================================================================================

        Call frmMain.SysLogWrite(False, "")

        cmbTempHumi_1.Items.Clear()
        cmbTempHumi_2.Items.Clear()

        For i As Integer = 160 To 300 Step 5
            cmbTempHumi_1.Items.Add(Format(i / 10, "##.0"))
        Next
        For i As Integer = 200 To 800 Step 5
            cmbTempHumi_2.Items.Add(Format(i / 10, "##.0"))
        Next

        'Read of select temp & humi ===============================================================
        Call frmMain.SysLogWrite(True, "温度・湿度設定読込　・・・")


        cmbTempHumi_1.Text = humid_temp("1916")
        cmbTempHumi_2.Text = humid_temp("1918")

        Call frmMain.SysLogWrite(True, "温度・湿度設定：" & cmbTempHumi_1.Text & "℃　" & cmbTempHumi_2.Text & "%")

    End Sub

    Public Function humid_temp(ByVal rg_num As String) As String


        Dim SetupFileName As String = mAppPath & "Reference\SetupNeptune.ini"
        myModbus1 = New clsNeptune(4, SetupFileName)

        Try
            myModbus1.EtherOpen()
            'Call WaitTimer(0.5)

            '온도, 습도 취득
            myModbus1.SendReceive2(a, "000100000006" & "0103" & rg_num & "0002")

            Revdata = myModbus1.IEEEtoDECIMAL_change(VB.Right(a, 8))

            ' 온도,습도 정상범위 이외
            If rg_num = "1916" Then ' 온도
                If Revdata < 18 Or Revdata > 28 Then
                    mWarningComment = "Out of temperature range! Please check the temperature again!"
                    Call frmMain.SysLogWrite(True, mWarningComment)
                    MsgBox("적정범위를 벗어났습니다!!  L/L를 불러주세요!! " & VB.vbCrLf & "현재 온도 : " & Revdata & ", Spec : 18 ~ 28", MsgBoxStyle.Critical)

                End If

            ElseIf rg_num = "1918" Then '습도

                If Revdata < 20 Or Revdata > 80 Then
                    mWarningComment = "Out of the humidity range! Please check the humidity again!"
                    Call frmMain.SysLogWrite(True, mWarningComment)
                    MsgBox("적정범위를 벗어났습니다!! L/L를 불러주세요!! " & VB.vbCrLf & "현재 습도 : " & Revdata & ", Spec : 20 ~ 80", MsgBoxStyle.Critical)

                End If
            End If

            myModbus1.Close()

        Catch ex As System.Exception
            Call frmMain.SysLogWrite(True, "The acquisition of temperature and humidity data failed." & VB.vbCrLf & " Please write down the temperature and humidity manually.")
        End Try

        Return Format(Revdata, "00.0")

    End Function
    Private Sub SaveData()
        '==============================================================================================================
        ' Function : Data save
        ' Remarks  :
        '==============================================================================================================

        Call frmMain.SysLogWrite(False, "")

        Dim dbName As String = ""
        Dim tbName As String = ""
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim sSQL As String = ""
        Dim fCount As Integer = 0
        Dim rCount As Integer = 0

        'Write of Temp. & Humi. setting ===========================================================
        Try
            dbName = mAppPath & "Files\EquipmentSetting.mdb"
            tbName = "tblDefTempHumi"

            cADO = New cls010_MDB_Operation

            cADO.DB_PATH = dbName
            cADO.TB_NAME = tbName

            sSQL = "UPDATE " & tbName & " SET "
            sSQL &= "Temperature = '" & cmbTempHumi_1.Text & "' , "
            sSQL &= "Humidity = '" & cmbTempHumi_2.Text & "'"
            cADO.DB_SQL = sSQL

            Call cADO.DB_EXEC_JET()

            Temp = CInt(cmbTempHumi_1.Text)
            Hum = CInt(cmbTempHumi_2.Text)

            'frmMain form 
            frmMain.TextBox_temp.Text = cmbTempHumi_1.Text
            frmMain.TextBox_humi.Text = cmbTempHumi_2.Text

        Catch ex As Exception
            mWarningComment = cADO.ERR_COMMENT
            MessageBox.Show(mWarningComment, "OperatorName, ID read Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            cADO = Nothing

        End Try

        Call frmMain.SysLogWrite(True, "データ保存：" & cmbTempHumi_1.Text & "℃ " & cmbTempHumi_2.Text & "%")

    End Sub

End Class



