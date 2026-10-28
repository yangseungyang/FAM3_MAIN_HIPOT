<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox_ProdSel = New System.Windows.Forms.GroupBox()
        Me.ComboBox_TestItem = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Information1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TextBox_TestTime = New System.Windows.Forms.TextBox()
        Me.TextBox_MacAdr = New System.Windows.Forms.TextBox()
        Me.TextBox_StartTime = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.TextBox_MSCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBox_SerialNo = New System.Windows.Forms.TextBox()
        Me.TextBox_OrderNo = New System.Windows.Forms.TextBox()
        Me.TextBox_StartDate = New System.Windows.Forms.TextBox()
        Me.TextBox_XjNo = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.TextBox_TagNo = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ComboBox_TestCertSign = New System.Windows.Forms.ComboBox()
        Me.TextBox_StationNo = New System.Windows.Forms.TextBox()
        Me.GroupBox_BarCode = New System.Windows.Forms.GroupBox()
        Me.Label_Indication = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TextBox_BarCode = New System.Windows.Forms.TextBox()
        Me.Panel_TestItem = New System.Windows.Forms.Panel()
        Me.GroupBox_Status = New System.Windows.Forms.GroupBox()
        Me.TextBox_Log = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox_Exec = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComboBox2_selfItem = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Button_ResultClear = New System.Windows.Forms.Button()
        Me.Button_TestEnd = New System.Windows.Forms.Button()
        Me.GroupBox_Debug = New System.Windows.Forms.GroupBox()
        Me.Button_Forward = New System.Windows.Forms.Button()
        Me.Button_Back = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Button_MS_Update = New System.Windows.Forms.Button()
        Me.TextBox_MS = New System.Windows.Forms.TextBox()
        Me.TextBox_humi = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.TextBox_op_name = New System.Windows.Forms.TextBox()
        Me.TextBox_op_id = New System.Windows.Forms.TextBox()
        Me.TextBox_temp = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label_Item = New System.Windows.Forms.Label()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.GroupBox_ProdSel.SuspendLayout()
        Me.Information1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox_BarCode.SuspendLayout()
        Me.GroupBox_Status.SuspendLayout()
        Me.GroupBox_Exec.SuspendLayout()
        Me.GroupBox_Debug.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_ProdSel
        '
        Me.GroupBox_ProdSel.Controls.Add(Me.ComboBox_TestItem)
        Me.GroupBox_ProdSel.Controls.Add(Me.Label14)
        Me.GroupBox_ProdSel.Location = New System.Drawing.Point(267, 7)
        Me.GroupBox_ProdSel.Name = "GroupBox_ProdSel"
        Me.GroupBox_ProdSel.Size = New System.Drawing.Size(456, 59)
        Me.GroupBox_ProdSel.TabIndex = 4
        Me.GroupBox_ProdSel.TabStop = False
        '
        'ComboBox_TestItem
        '
        Me.ComboBox_TestItem.CausesValidation = False
        Me.ComboBox_TestItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.ComboBox_TestItem.DropDownWidth = 150
        Me.ComboBox_TestItem.FormattingEnabled = True
        Me.ComboBox_TestItem.Location = New System.Drawing.Point(8, 31)
        Me.ComboBox_TestItem.Name = "ComboBox_TestItem"
        Me.ComboBox_TestItem.Size = New System.Drawing.Size(92, 20)
        Me.ComboBox_TestItem.TabIndex = 4
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(458, 19)
        Me.Label14.TabIndex = 3
        Me.Label14.Text = "Test Item Name"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(12, 7)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(248, 16)
        Me.Label13.TabIndex = 20
        Me.Label13.Text = "INFORMATION OF PRODUCT"
        '
        'Information1
        '
        Me.Information1.Controls.Add(Me.TabPage1)
        Me.Information1.Location = New System.Drawing.Point(11, 26)
        Me.Information1.Name = "Information1"
        Me.Information1.SelectedIndex = 0
        Me.Information1.Size = New System.Drawing.Size(250, 257)
        Me.Information1.TabIndex = 21
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.TextBox_TestTime)
        Me.TabPage1.Controls.Add(Me.TextBox_MacAdr)
        Me.TabPage1.Controls.Add(Me.TextBox_StartTime)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.Label21)
        Me.TabPage1.Controls.Add(Me.TextBox_MSCode)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.TextBox_SerialNo)
        Me.TabPage1.Controls.Add(Me.TextBox_OrderNo)
        Me.TabPage1.Controls.Add(Me.TextBox_StartDate)
        Me.TabPage1.Controls.Add(Me.TextBox_XjNo)
        Me.TabPage1.Controls.Add(Me.Label22)
        Me.TabPage1.Controls.Add(Me.TextBox_TagNo)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.ComboBox_TestCertSign)
        Me.TabPage1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(242, 231)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        '
        'TextBox_TestTime
        '
        Me.TextBox_TestTime.Location = New System.Drawing.Point(106, 150)
        Me.TextBox_TestTime.Name = "TextBox_TestTime"
        Me.TextBox_TestTime.ReadOnly = True
        Me.TextBox_TestTime.Size = New System.Drawing.Size(128, 19)
        Me.TextBox_TestTime.TabIndex = 27
        '
        'TextBox_MacAdr
        '
        Me.TextBox_MacAdr.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox_MacAdr.Location = New System.Drawing.Point(106, 284)
        Me.TextBox_MacAdr.Name = "TextBox_MacAdr"
        Me.TextBox_MacAdr.Size = New System.Drawing.Size(128, 20)
        Me.TextBox_MacAdr.TabIndex = 28
        '
        'TextBox_StartTime
        '
        Me.TextBox_StartTime.Location = New System.Drawing.Point(106, 126)
        Me.TextBox_StartTime.Name = "TextBox_StartTime"
        Me.TextBox_StartTime.ReadOnly = True
        Me.TextBox_StartTime.Size = New System.Drawing.Size(128, 19)
        Me.TextBox_StartTime.TabIndex = 26
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(2, 128)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(95, 15)
        Me.Label20.TabIndex = 29
        Me.Label20.Text = "START TIME："
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(2, 289)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(95, 16)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "MAC ADDRESS："
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label21.Location = New System.Drawing.Point(2, 153)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(95, 15)
        Me.Label21.TabIndex = 30
        Me.Label21.Text = "TOTAL TIME[s]："
        '
        'TextBox_MSCode
        '
        Me.TextBox_MSCode.Location = New System.Drawing.Point(2, 22)
        Me.TextBox_MSCode.Name = "TextBox_MSCode"
        Me.TextBox_MSCode.Size = New System.Drawing.Size(232, 19)
        Me.TextBox_MSCode.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(2, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 17)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "MODEL NAME："
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(2, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 15)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "SERIAL NO.："
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(2, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 15)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "ORDER NO.："
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(2, 177)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 15)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "DATE："
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(2, 314)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 15)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "QIC ："
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(2, 104)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(96, 16)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "XJ NO.："
        '
        'TextBox_SerialNo
        '
        Me.TextBox_SerialNo.Location = New System.Drawing.Point(106, 51)
        Me.TextBox_SerialNo.Name = "TextBox_SerialNo"
        Me.TextBox_SerialNo.Size = New System.Drawing.Size(128, 19)
        Me.TextBox_SerialNo.TabIndex = 1
        '
        'TextBox_OrderNo
        '
        Me.TextBox_OrderNo.Location = New System.Drawing.Point(106, 76)
        Me.TextBox_OrderNo.Name = "TextBox_OrderNo"
        Me.TextBox_OrderNo.Size = New System.Drawing.Size(128, 19)
        Me.TextBox_OrderNo.TabIndex = 2
        '
        'TextBox_StartDate
        '
        Me.TextBox_StartDate.Location = New System.Drawing.Point(106, 174)
        Me.TextBox_StartDate.Name = "TextBox_StartDate"
        Me.TextBox_StartDate.ReadOnly = True
        Me.TextBox_StartDate.Size = New System.Drawing.Size(128, 19)
        Me.TextBox_StartDate.TabIndex = 3
        '
        'TextBox_XjNo
        '
        Me.TextBox_XjNo.Location = New System.Drawing.Point(106, 101)
        Me.TextBox_XjNo.Name = "TextBox_XjNo"
        Me.TextBox_XjNo.Size = New System.Drawing.Size(128, 19)
        Me.TextBox_XjNo.TabIndex = 8
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.Location = New System.Drawing.Point(2, 269)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(96, 16)
        Me.Label22.TabIndex = 21
        Me.Label22.Text = "TAG NO.："
        '
        'TextBox_TagNo
        '
        Me.TextBox_TagNo.Location = New System.Drawing.Point(106, 265)
        Me.TextBox_TagNo.Name = "TextBox_TagNo"
        Me.TextBox_TagNo.Size = New System.Drawing.Size(128, 19)
        Me.TextBox_TagNo.TabIndex = 20
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(154, 315)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(80, 16)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "0=NO,1=YES"
        '
        'ComboBox_TestCertSign
        '
        Me.ComboBox_TestCertSign.Location = New System.Drawing.Point(106, 310)
        Me.ComboBox_TestCertSign.Name = "ComboBox_TestCertSign"
        Me.ComboBox_TestCertSign.Size = New System.Drawing.Size(40, 20)
        Me.ComboBox_TestCertSign.TabIndex = 6
        '
        'TextBox_StationNo
        '
        Me.TextBox_StationNo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 17.0!)
        Me.TextBox_StationNo.Location = New System.Drawing.Point(726, 312)
        Me.TextBox_StationNo.Name = "TextBox_StationNo"
        Me.TextBox_StationNo.ReadOnly = True
        Me.TextBox_StationNo.Size = New System.Drawing.Size(128, 30)
        Me.TextBox_StationNo.TabIndex = 32
        '
        'GroupBox_BarCode
        '
        Me.GroupBox_BarCode.Controls.Add(Me.Label_Indication)
        Me.GroupBox_BarCode.Controls.Add(Me.Label19)
        Me.GroupBox_BarCode.Controls.Add(Me.TextBox_BarCode)
        Me.GroupBox_BarCode.Location = New System.Drawing.Point(14, 449)
        Me.GroupBox_BarCode.Name = "GroupBox_BarCode"
        Me.GroupBox_BarCode.Size = New System.Drawing.Size(248, 70)
        Me.GroupBox_BarCode.TabIndex = 22
        Me.GroupBox_BarCode.TabStop = False
        '
        'Label_Indication
        '
        Me.Label_Indication.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label_Indication.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Indication.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_Indication.Location = New System.Drawing.Point(2, 11)
        Me.Label_Indication.Name = "Label_Indication"
        Me.Label_Indication.Size = New System.Drawing.Size(242, 19)
        Me.Label_Indication.TabIndex = 2
        Me.Label_Indication.Text = "Input the product info using BarCode"
        Me.Label_Indication.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(8, 33)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(96, 15)
        Me.Label19.TabIndex = 3
        Me.Label19.Text = "BARCODE："
        '
        'TextBox_BarCode
        '
        Me.TextBox_BarCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox_BarCode.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TextBox_BarCode.Location = New System.Drawing.Point(104, 33)
        Me.TextBox_BarCode.Name = "TextBox_BarCode"
        Me.TextBox_BarCode.Size = New System.Drawing.Size(136, 19)
        Me.TextBox_BarCode.TabIndex = 0
        '
        'Panel_TestItem
        '
        Me.Panel_TestItem.AutoScroll = True
        Me.Panel_TestItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_TestItem.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Panel_TestItem.Location = New System.Drawing.Point(268, 82)
        Me.Panel_TestItem.Name = "Panel_TestItem"
        Me.Panel_TestItem.Size = New System.Drawing.Size(456, 201)
        Me.Panel_TestItem.TabIndex = 24
        '
        'GroupBox_Status
        '
        Me.GroupBox_Status.Controls.Add(Me.TextBox_Log)
        Me.GroupBox_Status.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox_Status.Location = New System.Drawing.Point(268, 289)
        Me.GroupBox_Status.Name = "GroupBox_Status"
        Me.GroupBox_Status.Size = New System.Drawing.Size(456, 230)
        Me.GroupBox_Status.TabIndex = 25
        Me.GroupBox_Status.TabStop = False
        Me.GroupBox_Status.Text = "進行状況"
        '
        'TextBox_Log
        '
        Me.TextBox_Log.BackColor = System.Drawing.SystemColors.Info
        Me.TextBox_Log.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox_Log.Location = New System.Drawing.Point(8, 24)
        Me.TextBox_Log.Multiline = True
        Me.TextBox_Log.Name = "TextBox_Log"
        Me.TextBox_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox_Log.Size = New System.Drawing.Size(440, 197)
        Me.TextBox_Log.TabIndex = 1
        Me.TextBox_Log.Text = "LOG"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label16.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.Location = New System.Drawing.Point(268, 289)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(456, 16)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "PROGRESS"
        '
        'GroupBox_Exec
        '
        Me.GroupBox_Exec.Controls.Add(Me.Label8)
        Me.GroupBox_Exec.Controls.Add(Me.ComboBox2_selfItem)
        Me.GroupBox_Exec.Controls.Add(Me.Label15)
        Me.GroupBox_Exec.Controls.Add(Me.Button_ResultClear)
        Me.GroupBox_Exec.Controls.Add(Me.Button_TestEnd)
        Me.GroupBox_Exec.Controls.Add(Me.GroupBox_Debug)
        Me.GroupBox_Exec.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox_Exec.Location = New System.Drawing.Point(729, 7)
        Me.GroupBox_Exec.Name = "GroupBox_Exec"
        Me.GroupBox_Exec.Size = New System.Drawing.Size(128, 261)
        Me.GroupBox_Exec.TabIndex = 26
        Me.GroupBox_Exec.TabStop = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(1, 66)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(128, 16)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "EXECUTE BUTTON"
        '
        'ComboBox2_selfItem
        '
        Me.ComboBox2_selfItem.CausesValidation = False
        Me.ComboBox2_selfItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.ComboBox2_selfItem.FormattingEnabled = True
        Me.ComboBox2_selfItem.Location = New System.Drawing.Point(16, 31)
        Me.ComboBox2_selfItem.Name = "ComboBox2_selfItem"
        Me.ComboBox2_selfItem.Size = New System.Drawing.Size(92, 20)
        Me.ComboBox2_selfItem.TabIndex = 6
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label15.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(128, 16)
        Me.Label15.TabIndex = 5
        Me.Label15.Text = "Self Test"
        '
        'Button_ResultClear
        '
        Me.Button_ResultClear.Location = New System.Drawing.Point(16, 117)
        Me.Button_ResultClear.Name = "Button_ResultClear"
        Me.Button_ResultClear.Size = New System.Drawing.Size(96, 32)
        Me.Button_ResultClear.TabIndex = 2
        Me.Button_ResultClear.Text = "CLEAR"
        '
        'Button_TestEnd
        '
        Me.Button_TestEnd.Location = New System.Drawing.Point(16, 177)
        Me.Button_TestEnd.Name = "Button_TestEnd"
        Me.Button_TestEnd.Size = New System.Drawing.Size(96, 32)
        Me.Button_TestEnd.TabIndex = 3
        Me.Button_TestEnd.Text = "END"
        '
        'GroupBox_Debug
        '
        Me.GroupBox_Debug.Controls.Add(Me.Button_Forward)
        Me.GroupBox_Debug.Controls.Add(Me.Button_Back)
        Me.GroupBox_Debug.Location = New System.Drawing.Point(16, 264)
        Me.GroupBox_Debug.Name = "GroupBox_Debug"
        Me.GroupBox_Debug.Size = New System.Drawing.Size(96, 96)
        Me.GroupBox_Debug.TabIndex = 4
        Me.GroupBox_Debug.TabStop = False
        Me.GroupBox_Debug.Text = "Debug"
        '
        'Button_Forward
        '
        Me.Button_Forward.Location = New System.Drawing.Point(8, 24)
        Me.Button_Forward.Name = "Button_Forward"
        Me.Button_Forward.Size = New System.Drawing.Size(80, 24)
        Me.Button_Forward.TabIndex = 0
        Me.Button_Forward.Text = "FORWARD"
        '
        'Button_Back
        '
        Me.Button_Back.Location = New System.Drawing.Point(8, 56)
        Me.Button_Back.Name = "Button_Back"
        Me.Button_Back.Size = New System.Drawing.Size(80, 24)
        Me.Button_Back.TabIndex = 1
        Me.Button_Back.Text = "BACK"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 289)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(250, 154)
        Me.TabControl1.TabIndex = 28
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.Button_MS_Update)
        Me.TabPage2.Controls.Add(Me.TextBox_MS)
        Me.TabPage2.Controls.Add(Me.TextBox_humi)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.Label18)
        Me.TabPage2.Controls.Add(Me.Label24)
        Me.TabPage2.Controls.Add(Me.Label27)
        Me.TabPage2.Controls.Add(Me.TextBox_op_name)
        Me.TabPage2.Controls.Add(Me.TextBox_op_id)
        Me.TabPage2.Controls.Add(Me.TextBox_temp)
        Me.TabPage2.Controls.Add(Me.TextBox2)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label26)
        Me.TabPage2.Controls.Add(Me.Label28)
        Me.TabPage2.Controls.Add(Me.TextBox10)
        Me.TabPage2.Controls.Add(Me.Label29)
        Me.TabPage2.Controls.Add(Me.ComboBox1)
        Me.TabPage2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(242, 128)
        Me.TabPage2.TabIndex = 0
        Me.TabPage2.Text = "TabPage2"
        '
        'Button_MS_Update
        '
        Me.Button_MS_Update.Location = New System.Drawing.Point(199, 101)
        Me.Button_MS_Update.Name = "Button_MS_Update"
        Me.Button_MS_Update.Size = New System.Drawing.Size(39, 23)
        Me.Button_MS_Update.TabIndex = 50
        Me.Button_MS_Update.Text = "갱신"
        Me.Button_MS_Update.UseVisualStyleBackColor = True
        '
        'TextBox_MS
        '
        Me.TextBox_MS.Location = New System.Drawing.Point(109, 103)
        Me.TextBox_MS.Name = "TextBox_MS"
        Me.TextBox_MS.ReadOnly = True
        Me.TextBox_MS.Size = New System.Drawing.Size(128, 19)
        Me.TextBox_MS.TabIndex = 49
        '
        'TextBox_humi
        '
        Me.TextBox_humi.Location = New System.Drawing.Point(109, 79)
        Me.TextBox_humi.Name = "TextBox_humi"
        Me.TextBox_humi.ReadOnly = True
        Me.TextBox_humi.Size = New System.Drawing.Size(128, 19)
        Me.TextBox_humi.TabIndex = 48
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 15)
        Me.Label5.TabIndex = 51
        Me.Label5.Text = "Humidity(%)："
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(5, 106)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(95, 15)
        Me.Label9.TabIndex = 52
        Me.Label9.Text = "MSCODE.mdb："
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label18.Location = New System.Drawing.Point(5, 7)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(107, 15)
        Me.Label18.TabIndex = 45
        Me.Label18.Text = "Operator NAME："
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label24.Location = New System.Drawing.Point(5, 33)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(96, 15)
        Me.Label24.TabIndex = 46
        Me.Label24.Text = "Operator ID.："
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label27.Location = New System.Drawing.Point(5, 57)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(107, 16)
        Me.Label27.TabIndex = 47
        Me.Label27.Text = "Temperature(℃).："
        '
        'TextBox_op_name
        '
        Me.TextBox_op_name.Location = New System.Drawing.Point(109, 4)
        Me.TextBox_op_name.Name = "TextBox_op_name"
        Me.TextBox_op_name.ReadOnly = True
        Me.TextBox_op_name.Size = New System.Drawing.Size(128, 19)
        Me.TextBox_op_name.TabIndex = 42
        '
        'TextBox_op_id
        '
        Me.TextBox_op_id.Location = New System.Drawing.Point(109, 29)
        Me.TextBox_op_id.Name = "TextBox_op_id"
        Me.TextBox_op_id.ReadOnly = True
        Me.TextBox_op_id.Size = New System.Drawing.Size(128, 19)
        Me.TextBox_op_id.TabIndex = 43
        '
        'TextBox_temp
        '
        Me.TextBox_temp.Location = New System.Drawing.Point(109, 54)
        Me.TextBox_temp.Name = "TextBox_temp"
        Me.TextBox_temp.ReadOnly = True
        Me.TextBox_temp.Size = New System.Drawing.Size(128, 19)
        Me.TextBox_temp.TabIndex = 44
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(106, 284)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(128, 20)
        Me.TextBox2.TabIndex = 28
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(2, 289)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(95, 16)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "MAC ADDRESS："
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label26.Location = New System.Drawing.Point(2, 314)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(96, 15)
        Me.Label26.TabIndex = 15
        Me.Label26.Text = "QIC ："
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label28.Location = New System.Drawing.Point(2, 269)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(96, 16)
        Me.Label28.TabIndex = 21
        Me.Label28.Text = "TAG NO.："
        '
        'TextBox10
        '
        Me.TextBox10.Location = New System.Drawing.Point(106, 265)
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(128, 19)
        Me.TextBox10.TabIndex = 20
        '
        'Label29
        '
        Me.Label29.Location = New System.Drawing.Point(154, 315)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(80, 16)
        Me.Label29.TabIndex = 18
        Me.Label29.Text = "0=NO,1=YES"
        '
        'ComboBox1
        '
        Me.ComboBox1.Location = New System.Drawing.Point(106, 310)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(40, 20)
        Me.ComboBox1.TabIndex = 6
        '
        'Label_Item
        '
        Me.Label_Item.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label_Item.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_Item.Location = New System.Drawing.Point(267, 73)
        Me.Label_Item.Name = "Label_Item"
        Me.Label_Item.Size = New System.Drawing.Size(456, 16)
        Me.Label_Item.TabIndex = 29
        Me.Label_Item.Text = "TEST ITEMS"
        '
        'SerialPort1
        '
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label17.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(726, 289)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(128, 16)
        Me.Label17.TabIndex = 34
        Me.Label17.Text = "TEST EQUIP. NO."
        '
        'frmMain
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(858, 522)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label_Item)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TextBox_StationNo)
        Me.Controls.Add(Me.GroupBox_Exec)
        Me.Controls.Add(Me.GroupBox_Status)
        Me.Controls.Add(Me.Panel_TestItem)
        Me.Controls.Add(Me.GroupBox_BarCode)
        Me.Controls.Add(Me.Information1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.GroupBox_ProdSel)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FAM3 Main 내전압 검사장치"
        Me.GroupBox_ProdSel.ResumeLayout(False)
        Me.Information1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox_BarCode.ResumeLayout(False)
        Me.GroupBox_BarCode.PerformLayout()
        Me.GroupBox_Status.ResumeLayout(False)
        Me.GroupBox_Status.PerformLayout()
        Me.GroupBox_Exec.ResumeLayout(False)
        Me.GroupBox_Debug.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox_ProdSel As GroupBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Information1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TextBox_TestTime As TextBox
    Friend WithEvents TextBox_MacAdr As TextBox
    Friend WithEvents TextBox_StartTime As TextBox
    Friend WithEvents TextBox_StationNo As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents TextBox_MSCode As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents TextBox_SerialNo As TextBox
    Friend WithEvents TextBox_OrderNo As TextBox
    Friend WithEvents TextBox_StartDate As TextBox
    Friend WithEvents TextBox_XjNo As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents TextBox_TagNo As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents ComboBox_TestCertSign As ComboBox
    Friend WithEvents GroupBox_BarCode As GroupBox
    Friend WithEvents Label_Indication As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents TextBox_BarCode As TextBox
    Friend WithEvents Panel_TestItem As Panel
    Friend WithEvents GroupBox_Status As GroupBox
    Friend WithEvents TextBox_Log As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents GroupBox_Exec As GroupBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Button_ResultClear As Button
    Friend WithEvents Button_TestEnd As Button
    Friend WithEvents GroupBox_Debug As GroupBox
    Friend WithEvents Button_Forward As Button
    Friend WithEvents Button_Back As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents TextBox10 As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label_Item As Label
    Friend WithEvents Button_MS_Update As Button
    Friend WithEvents TextBox_MS As TextBox
    Friend WithEvents TextBox_humi As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents TextBox_op_name As TextBox
    Friend WithEvents TextBox_op_id As TextBox
    Friend WithEvents TextBox_temp As TextBox
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents Label8 As Label
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents ComboBox_TestItem As ComboBox
    Friend WithEvents ComboBox2_selfItem As ComboBox
    Friend WithEvents Label17 As Label
End Class
