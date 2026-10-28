'================================================================================================================================
'Form Name  : Form2(Model_Input)
'Function   : F3SCxx Model ID Read
'Language   : VB2008 Framework3.5
'Reference  : Model Barcode
'Memo       :
'           :
'Issue      : 2017/08/25  Rev 0.00    Daeil.Jang       New Release
'================================================================================================================================

Public Class Model_Input
    Dim cBcd1 As cls101_ComRS232C
    Dim cADO As cls010_MDB_Operation
    Dim fMain As frmMain

    Dim mResult As String = ""
    Dim mErrorComment As String
    Dim mPLASMABarcode As String

    Dim mAppPath As String = Mid(Application.StartupPath, 1, InStr(Application.StartupPath, "bin") - 1)

    Public ReadOnly Property Result() As String
        Get
            Result = mResult
        End Get
    End Property

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        cBcd1 = New cls101_ComRS232C
        cBcd1.PortClose()

        cBcd1 = Nothing

        mResult = TextBox1.Text
        TextBox1.Text = ""
        Me.Close()
    End Sub
    Private Delegate Sub COM_Recieve()
    ''==========================================================================================
    ''Function : COM recieve event
    ''Remarks  :
    ''===========

    Private Sub DataWriteProcess()

        TextBox1.Text = mPLASMABarcode

    End Sub

    Private Sub SerialPort2_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort2.DataReceived

        AddHandler TextBox1.TextChanged, AddressOf TextBox1_TextChanged

        Try
            Me.Invoke(New COM_Recieve(AddressOf BeginningProcess), New Object() {})
        Catch

        Finally

        End Try

        cBcd1.PortClose()

        Try
            TextBox1.Invoke(New COM_Recieve(AddressOf DataWriteProcess), New Object() {})
        Catch

        Finally

        End Try

        'cBcd1.PortSetup()

        'RemoveHandler TextBox1.TextChanged, AddressOf TextBox1_TextChanged

    End Sub
    Private Sub BeginningProcess()
        '==========================================================================================
        'Function : Beginning process of inspection
        'Remarks  :
        '==========================================================================================

        Try
            Call cBcd1.FuncRecvCmd()
            Me.TextBox1.Enabled = False
            mPLASMABarcode = cBcd1.RECEIVE_DATA
            TextBox1.Text = mPLASMABarcode
            mResult = TextBox1.Text
            TextBox1.Text = ""
            Me.Close()

            'Me.Button1_Click(sender, e)
        Catch oExcept As Exception
            mErrorComment = cBcd1.ERR_COMMENT : If mErrorComment = "" Then oExcept.ToString()

        End Try

    End Sub

    Public Sub Model_Input_Load()
        'Private Sub Model_Input_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '==========================================================================================
        'Function : Start
        'Remarks  :
        '==========================================================================================
        Dim dbName As String = ""
        Dim tbName As String
        Dim sSQL As String
        Dim fName() As String = {""}
        Dim sData() As String = {""}

        'Read of BarcodeReader Equipment No. ====================================================================
        cBcd1 = New cls101_ComRS232C
        fMain = New frmMain

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
            Call fMain.SysLogWrite(True, "バーコードリーダー：" & mErrorComment)

        Finally
            cADO = Nothing

        End Try


        If mErrorComment <> "" Then Exit Sub
        Dim BarSetting() As String = Split(sData(1), "-")

        cBcd1.TERMINAL_CHAR = vbCr : mDelim = vbCr
        cBcd1.RS_COMM = Me.SerialPort2 : cBcd1.PORT = sData(0) : mPort = sData(0)
        cBcd1.SETTING = BarSetting(0) & "," & BarSetting(1) & "," & BarSetting(2) & "," & BarSetting(3)
        Setting = BarSetting(0) & "," & BarSetting(1) & "," & BarSetting(2) & "," & BarSetting(3)
        mBarCode = True

        cBcd1.PortSetup()

        Me.Show()
        TextBox1.Focus()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        mResult = "Cancle"
        Me.Close()
    End Sub

    Private Sub TextBox1_keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        TextBox1.Text = StrConv(TextBox1.Text, VbStrConv.Uppercase)
        TextBox1.SelectionStart = TextBox1.Text.Length
        If e.KeyChar = Chr(Keys.Enter) Then '-------------エンターキーが入力されたら

            Me.TextBox1.Enabled = False
            Me.Button1_Click(sender, e)

        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
