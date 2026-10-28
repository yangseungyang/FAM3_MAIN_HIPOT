Public Class cls102_SignalTower

    Dim cLnk As cls080_F3LinkToLE
    Dim cSQL As cls010_MDB_Operation

    Dim FAM3Slot As String = "2"

    Dim mSigTower As Boolean
    Dim mComment As String
    Dim mError As String
    'Dim mAppPath As String = Mid(Application.StartupPath, 1, (InStr(Application.StartupPath, Application.ProductName) - 1) + Len(Application.ProductName)) & "\"
    Dim mAppPath As String = Mid(Application.StartupPath, 1, InStr(Application.StartupPath, "bin") - 1)
    Public WriteOnly Property SignalTowerSTATUS() As Boolean
        Set(ByVal Value As Boolean)
            mSigTower = Value
        End Set
    End Property

    Public Sub SignalTower(ByVal color As String, ByVal buzz1 As String, ByVal buzz2 As String, ByVal flash As String)
        '==========================================================================================
        ' F.Name   : SignalTower
        ' Function : Signal tower control
        ' Data     : color,buzz1,buzz2,flash
        ' Return   : Nothing
        ' Remarks  :
        '==========================================================================================

        Dim scmd As String = ""
        Dim rCmd As String = ""

        Dim dbName As String
        Dim tbName As String
        Dim sSQL As String
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim rField As String
        'Dim kField As String
        Dim fCount As Integer = 0
        Dim rCount As Integer = 0

        'Signaltower Control Check =====================
        cSQL = New cls010_MDB_Operation

        dbName = mAppPath & "Files\EquipmentSetting.mdb"
        tbName = "tblEquipmentSetting"
        rField = "SignalTowerControl"

        sSQL = "SELECT " & rField & " FROM " & tbName

        cSQL.TB_NAME = tbName
        cSQL.DB_PATH = dbName
        cSQL.DB_SQL = sSQL

        Call cSQL.DB_READ_JET(fName, sData)
        If cSQL.ERR_COMMENT <> "" Then GoTo ErrorTrap

        cSQL = Nothing

        If InStr(sData(0), "N") <> 0 Then Exit Sub

        'Relay OFF =====================================
        cLnk = New cls080_F3LinkToLE

        cLnk.IPAdd = "192.168.1.2"

        If buzz1 = "OFF" And buzz2 = "OFF" And flash = "OFF" Then
            Select Case color
                Case "RED" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00100000"
                Case "YELLOW" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00100000"
                Case "GREEN" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00100000"
            End Select
        End If

        If buzz1 = "ON" And buzz2 = "OFF" And flash = "OFF" Then
            Select Case color
                'Case "RED" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000100"        'Ver0.11 YSH DEL
                Case "RED" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000010"        'Ver0.11 YSH ADD : 부저 Y206 → Y207 : 신규 설비 FD1-AIEC002_1 P3 에서 YC16 6번(Y206) 미배선, 7번(Y207) 시그널타워 배선 (A8 cls102 와 같음)
                'Case "YELLOW" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000100"        'Ver0.11 YSH DEL
                Case "YELLOW" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000010"        'Ver0.11 YSH ADD : 부저 Y206 → Y207 : 신규 설비 FD1-AIEC002_1 P3 에서 YC16 6번(Y206) 미배선, 7번(Y207) 시그널타워 배선 (A8 cls102 와 같음)
                'Case "GREEN" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000100"        'Ver0.11 YSH DEL
                Case "GREEN" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000010"        'Ver0.11 YSH ADD : 부저 Y206 → Y207 : 신규 설비 FD1-AIEC002_1 P3 에서 YC16 6번(Y206) 미배선, 7번(Y207) 시그널타워 배선 (A8 cls102 와 같음)
            End Select
        End If

        If buzz1 = "OFF" And buzz2 = "ON" And flash = "OFF" Then
            Select Case color
                'Case "RED" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000100"        'Ver0.11 YSH DEL
                Case "RED" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000010"        'Ver0.11 YSH ADD : 부저 Y206 → Y207 : 신규 설비 FD1-AIEC002_1 P3 에서 YC16 6번(Y206) 미배선, 7번(Y207) 시그널타워 배선 (A8 cls102 와 같음)
                'Case "YELLOW" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000100"        'Ver0.11 YSH DEL
                Case "YELLOW" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000010"        'Ver0.11 YSH ADD : 부저 Y206 → Y207 : 신규 설비 FD1-AIEC002_1 P3 에서 YC16 6번(Y206) 미배선, 7번(Y207) 시그널타워 배선 (A8 cls102 와 같음)
                'Case "GREEN" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000100"        'Ver0.11 YSH DEL
                Case "GREEN" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000010"        'Ver0.11 YSH ADD : 부저 Y206 → Y207 : 신규 설비 FD1-AIEC002_1 P3 에서 YC16 6번(Y206) 미배선, 7번(Y207) 시그널타워 배선 (A8 cls102 와 같음)
            End Select
        End If

        If buzz1 = "OFF" And buzz2 = "OFF" And flash = "ON" Then
            Select Case color
                Case "RED" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,10000000"
                Case "YELLOW" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00100000"
                Case "GREEN" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00001000"
            End Select
        End If

        If buzz1 = "ON" And buzz2 = "OFF" And flash = "ON" Then
            Select Case color
                'Case "RED" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,10000100"        'Ver0.11 YSH DEL
                Case "RED" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,10000010"        'Ver0.11 YSH ADD : 부저 Y206 → Y207 : 신규 설비 FD1-AIEC002_1 P3 에서 YC16 6번(Y206) 미배선, 7번(Y207) 시그널타워 배선 (A8 cls102 와 같음)
                'Case "YELLOW" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00100100"        'Ver0.11 YSH DEL
                Case "YELLOW" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00100010"        'Ver0.11 YSH ADD : 부저 Y206 → Y207 : 신규 설비 FD1-AIEC002_1 P3 에서 YC16 6번(Y206) 미배선, 7번(Y207) 시그널타워 배선 (A8 cls102 와 같음)
                'Case "GREEN" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00001100"        'Ver0.11 YSH DEL
                Case "GREEN" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00001010"        'Ver0.11 YSH ADD : 부저 Y206 → Y207 : 신규 설비 FD1-AIEC002_1 P3 에서 YC16 6번(Y206) 미배선, 7번(Y207) 시그널타워 배선 (A8 cls102 와 같음)
            End Select
        End If

        If buzz1 = "OFF" And buzz2 = "ON" And flash = "ON" Then
            Select Case color
                'Case "RED" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,10000100"        'Ver0.11 YSH DEL
                Case "RED" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,10000010"        'Ver0.11 YSH ADD : 부저 Y206 → Y207 : 신규 설비 FD1-AIEC002_1 P3 에서 YC16 6번(Y206) 미배선, 7번(Y207) 시그널타워 배선 (A8 cls102 와 같음)
                'Case "YELLOW" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00100100"        'Ver0.11 YSH DEL
                Case "YELLOW" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00100010"        'Ver0.11 YSH ADD : 부저 Y206 → Y207 : 신규 설비 FD1-AIEC002_1 P3 에서 YC16 6번(Y206) 미배선, 7번(Y207) 시그널타워 배선 (A8 cls102 와 같음)
                'Case "GREEN" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00001100"        'Ver0.11 YSH DEL
                Case "GREEN" : scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00001010"        'Ver0.11 YSH ADD : 부저 Y206 → Y207 : 신규 설비 FD1-AIEC002_1 P3 에서 YC16 6번(Y206) 미배선, 7번(Y207) 시그널타워 배선 (A8 cls102 와 같음)
            End Select
        End If

        'Sound OFF ---------------------------------
        If color = "" And buzz1 = "OFF" And buzz2 = "OFF" And flash = "" Then
            'scmd = "01" & "BWRY" & FAM3Slot & "06" & ",002,00"        'Ver0.11 YSH DEL
            scmd = "01" & "BWRY" & FAM3Slot & "07" & ",002,00"        'Ver0.11 YSH ADD : 부저 끄기도 Y207~Y208 (A8 cls102 와 같음, FD1-AIEC002_1 P3)
        End If

        Call cLnk.F3PCLinkCommunication(scmd, rCmd)
        If cLnk.ERR_COMMENT <> "" Then
            mError = cLnk.ERR_COMMENT
            GoTo ErrorTrap
        End If

        cLnk = Nothing

        Exit Sub

ErrorTrap:

        MsgBox("Signal tower control NG " & mError, MsgBoxStyle.OkOnly)

        cLnk = Nothing
        cSQL = Nothing

    End Sub

    Public Sub SignalTowerReset()
        '==========================================================================================
        ' F.Name   : SignalTowerReset
        ' Function : Signal tower All OFF
        ' Data     : Nothing
        ' Return   : Nothing
        ' Remarks  :
        '==========================================================================================

        Dim scmd As String
        Dim rCmd As String = ""

        Dim dbName As String
        Dim tbName As String
        Dim sSQL As String
        Dim fName() As String = {""}
        Dim sData() As String = {""}
        Dim rField As String
        'Dim kField As String
        Dim fCount As Integer = 0
        Dim rCount As Integer = 0

        'Signaltower Control Check =====================
        dbName = mAppPath & "Files\EquipmentSetting.mdb"
        tbName = "tblEquipmentSetting"
        rField = "SignalTowerControl"

        sSQL = "SELECT " & rField & " FROM " & tbName & " WHERE " & "SystemVersion" & " in "
        sSQL = sSQL & "(SELECT " & "Max(" & "SystemVersion" & ") as maxver" & " FROM " & tbName & ")"
        sSQL = sSQL & " ORDER BY " & "SystemVersion" & " DESC;"

        sSQL = "SELECT " & rField & " FROM " & tbName

        cSQL = New cls010_MDB_Operation
        cSQL.DB_PATH = dbName
        cSQL.TB_NAME = tbName
        cSQL.DB_SQL = sSQL

        Call cSQL.DB_READ_JET(fName, sData)
        If cSQL.ERR_COMMENT <> "" Then GoTo ErrorTrap

        cSQL = Nothing

        If InStr(sData(0), "N") <> 0 Then Exit Sub

        'Relay OFF =====================================
        cLnk = New cls080_F3LinkToLE

        cLnk.IPAdd = "192.168.1.2"
        
        scmd = "01" & "BWRY" & FAM3Slot & "01" & ",008,00000000"
        Call cLnk.F3PCLinkCommunication(scmd, rCmd)
        If cLnk.ERR_COMMENT <> "" Then
            mError = cLnk.ERR_COMMENT
            GoTo ErrorTrap
        End If

        cLnk = Nothing

        Exit Sub

ErrorTrap:

        MsgBox("Signal tower reset NG " & mError)

        cLnk = Nothing
        cSQL = Nothing

    End Sub


End Class
