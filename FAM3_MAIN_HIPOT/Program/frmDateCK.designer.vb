<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDateCK
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.cmbTempHumi_2 = New System.Windows.Forms.ComboBox()
        Me.cmbTempHumi_1 = New System.Windows.Forms.ComboBox()
        Me.Temp_Text = New System.Windows.Forms.TextBox()
        Me.Humi_Text = New System.Windows.Forms.TextBox()
        Me.Temp_Humi_Text = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TextBox1.BackColor = System.Drawing.Color.Silver
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox1.ForeColor = System.Drawing.Color.Red
        Me.TextBox1.Location = New System.Drawing.Point(1, 21)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(363, 53)
        Me.TextBox1.TabIndex = 2
        Me.TextBox1.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "날짜와 시간이 정확하게 표시되는 것을 확인하세요!!"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("MS UI Gothic", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Blue
        Me.Button1.Location = New System.Drawing.Point(60, 195)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(82, 32)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("MS UI Gothic", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Red
        Me.Button2.Location = New System.Drawing.Point(239, 195)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(78, 32)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "NG"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TextBox2.BackColor = System.Drawing.Color.Azure
        Me.TextBox2.Font = New System.Drawing.Font("MS UI Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox2.ForeColor = System.Drawing.Color.OrangeRed
        Me.TextBox2.Location = New System.Drawing.Point(1, 78)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(363, 59)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmbTempHumi_2
        '
        Me.cmbTempHumi_2.FormattingEnabled = True
        Me.cmbTempHumi_2.Location = New System.Drawing.Point(226, 167)
        Me.cmbTempHumi_2.Name = "cmbTempHumi_2"
        Me.cmbTempHumi_2.Size = New System.Drawing.Size(66, 20)
        Me.cmbTempHumi_2.TabIndex = 4
        Me.cmbTempHumi_2.Visible = False
        '
        'cmbTempHumi_1
        '
        Me.cmbTempHumi_1.FormattingEnabled = True
        Me.cmbTempHumi_1.IntegralHeight = False
        Me.cmbTempHumi_1.Location = New System.Drawing.Point(226, 142)
        Me.cmbTempHumi_1.Name = "cmbTempHumi_1"
        Me.cmbTempHumi_1.Size = New System.Drawing.Size(66, 20)
        Me.cmbTempHumi_1.TabIndex = 5
        Me.cmbTempHumi_1.Visible = False
        '
        'Temp_Text
        '
        Me.Temp_Text.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Temp_Text.BackColor = System.Drawing.Color.Silver
        Me.Temp_Text.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Temp_Text.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Temp_Text.ForeColor = System.Drawing.Color.Black
        Me.Temp_Text.Location = New System.Drawing.Point(297, 142)
        Me.Temp_Text.Margin = New System.Windows.Forms.Padding(2)
        Me.Temp_Text.Multiline = True
        Me.Temp_Text.Name = "Temp_Text"
        Me.Temp_Text.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Temp_Text.Size = New System.Drawing.Size(25, 20)
        Me.Temp_Text.TabIndex = 6
        Me.Temp_Text.Text = "℃"
        Me.Temp_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Temp_Text.Visible = False
        '
        'Humi_Text
        '
        Me.Humi_Text.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Humi_Text.BackColor = System.Drawing.Color.Silver
        Me.Humi_Text.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Humi_Text.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Humi_Text.ForeColor = System.Drawing.Color.Black
        Me.Humi_Text.Location = New System.Drawing.Point(297, 166)
        Me.Humi_Text.Margin = New System.Windows.Forms.Padding(2)
        Me.Humi_Text.Multiline = True
        Me.Humi_Text.Name = "Humi_Text"
        Me.Humi_Text.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Humi_Text.Size = New System.Drawing.Size(25, 20)
        Me.Humi_Text.TabIndex = 7
        Me.Humi_Text.Text = "%"
        Me.Humi_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Humi_Text.Visible = False
        '
        'Temp_Humi_Text
        '
        Me.Temp_Humi_Text.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Temp_Humi_Text.BackColor = System.Drawing.Color.Silver
        Me.Temp_Humi_Text.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Temp_Humi_Text.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Temp_Humi_Text.ForeColor = System.Drawing.Color.Red
        Me.Temp_Humi_Text.Location = New System.Drawing.Point(30, 155)
        Me.Temp_Humi_Text.Margin = New System.Windows.Forms.Padding(2)
        Me.Temp_Humi_Text.Multiline = True
        Me.Temp_Humi_Text.Name = "Temp_Humi_Text"
        Me.Temp_Humi_Text.Size = New System.Drawing.Size(191, 18)
        Me.Temp_Humi_Text.TabIndex = 8
        Me.Temp_Humi_Text.Text = "온습도 설정을 확인해주세요."
        Me.Temp_Humi_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Temp_Humi_Text.Visible = False
        '
        'frmDateCK
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(375, 251)
        Me.Controls.Add(Me.Temp_Humi_Text)
        Me.Controls.Add(Me.Humi_Text)
        Me.Controls.Add(Me.Temp_Text)
        Me.Controls.Add(Me.cmbTempHumi_1)
        Me.Controls.Add(Me.cmbTempHumi_2)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmDateCK"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Date & Time Check"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents cmbTempHumi_2 As ComboBox
    Friend WithEvents cmbTempHumi_1 As ComboBox
    Friend WithEvents Temp_Text As TextBox
    Friend WithEvents Humi_Text As TextBox
    Friend WithEvents Temp_Humi_Text As TextBox
End Class
