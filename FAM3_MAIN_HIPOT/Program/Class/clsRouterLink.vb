Option Strict On : Option Explicit On
'============================================================================================================================
'Class Name : clsRouterLink
'Function   : 바코드 분배 프로그램(Barcode_Router) 연동 (FAM3)
'Framwork   : VB2019 Framework4.8
'Memo       : ----------------------------- Ver0.11 YSH ADD (클래스 전체 신규) -----------------------------
'             - FAM3 는 원래부터 바코드를 SerialPort1 (EquipmentSetting.mdb BarcodeReaderPort) 로 받는다.
'               포트 값만 Router 쪽 가상 COM 으로 바꾸면 되므로 수신 처리는 기존 frmMain.SerialPort1_DataReceived 그대로 쓴다
'             - 이 클래스는 Router 연동에 필요한 두 가지만 한다
'                 ShowFront : 바코드를 받으면 FAM3 창을 맨 앞으로 (숨김 / 최소화 해제)        (md 7-7항 / 구조설계 F3-04)
'                 SendEnd   : 처리가 끝나면 같은 포트로 "END + CR" 을 Router 에 회신           (md 7-9항 / 구조설계 F3-03, P-04)
'                             보내기 전에 AllowSetForegroundWindow 로 Router 가 다시 맨 앞으로 올 수 있게 권한을 넘긴다 (P-08)
'             frmMain 의 연동 코드를 이 클래스로 분리 (2026-10-26 지시)
'Issue      : 2026/10/26  Ver0.11    YSH       New Release
'             근거 : FAM3 증산 대응_내전압 설비 ADD-ON.md Rev 3  7-7 ~ 7-9항 / A8_FAM3_Router_구조설계_v1.1.xlsx F3-03, F3-04
'============================================================================================================================
Imports System.Runtime.InteropServices

Public Class clsRouterLink

    Private Const ROUTER_END As String = "END"      'Router 로 보내는 검사 종료 신호 (구조설계 5_통신규약 P-04)
    Private Const ASFW_ANY As Integer = -1

    <DllImport("user32.dll")>
    Private Shared Function AllowSetForegroundWindow(ByVal dwProcessId As Integer) As Boolean
    End Function

    Private Delegate Sub FormDelegate(ByVal form As Form)

    Public Shared Sub ShowFront(ByVal form As Form)
        '==========================================================================================
        'Function : 창을 맨 앞으로 (숨김 / 최소화 해제). 수신 스레드에서 불러도 된다
        '==========================================================================================
        Try
            If form.InvokeRequired Then
                form.Invoke(New FormDelegate(AddressOf ShowFront), form)
                Exit Sub
            End If
            If form.WindowState = FormWindowState.Minimized Then form.WindowState = FormWindowState.Normal
            form.Show()
            form.TopMost = True
            form.TopMost = False
            form.Activate()
        Catch ex As Exception
            Debug.Print("Router ShowFront NG : " & ex.Message)
        End Try
    End Sub

    Public Shared Sub SendEnd(ByVal form As frmMain, ByVal bcd As cls101_ComRS232C)
        '==========================================================================================
        'Function : "END + CR" 을 Router 로 회신 (포트를 다시 연 뒤 부른다)
        '==========================================================================================
        Try
            Call AllowSetForegroundWindow(ASFW_ANY)
            bcd.SendCmd = ROUTER_END
            bcd.FuncSendCmd()
            Call form.SysLogWrite(True, "Router END Send OK")
        Catch ex As Exception
            Call form.SysLogWrite(True, "Router END Send NG : " & ex.Message)
        End Try
    End Sub

End Class
