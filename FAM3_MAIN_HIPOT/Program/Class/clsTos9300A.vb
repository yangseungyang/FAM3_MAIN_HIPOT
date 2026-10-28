'---------------------------------------------------------------
'   cls5080A
'   Copyright(c)  2012 Yokogawa Electric Corporation.
'
'   Comment: Class of 5080A 
'   Modify : Ver 0.00 2015.07.xx Initial Release for InternShip by Yong
'   Caution: This class use only for internship. Because this class only contain performance of Output.
'---------------------------------------------------------------
Option Strict On
Option Explicit On

Imports System
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports VB = Microsoft.VisualBasic
Imports Ivi.Visa.Interop

Public Class clsTos9300A
    Private myPhaseLock As Boolean
    Private myOper As Boolean
    Private myVoltage As Double
    Private RM As New Ivi.Visa.Interop.ResourceManager       'USB Port 연결 참조
    Private TOS9300 As New Ivi.Visa.Interop.FormattedIO488   'USB Port 연결 참조

    Public Sub New(ByVal TOS9300No As String)
        MyBase.New()
        TOS9300.IO = CType(RM.Open("USB0::0x0B3E::0x104F::" & TOS9300No & "::INSTR"), Ivi.Visa.Interop.IMessage)    '제품의 MAC ADDRESS 설정
        TOS9300.WriteString("SYST:COMM:RLST REM")                                                                   'Remote상태로 설정
        myPhaseLock = False
        myOper = False
        myVoltage = 0
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Public Sub Preset()
        TOS9300.WriteString("ABOR:TEST")                                                            'Stop
        TOS9300.WriteString("*CLS")                                                                 'Clear
    End Sub
    Public Sub TosStop()
        TOS9300.WriteString("ABOR:TEST")
    End Sub
    Public Function PortSendReceive(ByVal cmd As String) As String
        Dim recData As String
        Dim Indata As String = ""

        Try
            TOS9300.WriteString(cmd)
            recData = TOS9300.ReadString()
            Return recData
        Catch ex As Exception
            Throw New ApplicationException("[ERROR] SendReceive: " & ex.ToString)
        End Try
    End Function
    Public Sub Start()
        Try
            Me.SendOnly("INIT:TEST")
        Catch ex As System.Exception
            Throw New ApplicationException("[ERROR] SendReceive: " & ex.ToString)
        End Try
    End Sub

    Public Sub SetAcwStatus(ByVal Row As Integer)
        '----------------------------------------------------- 'Ver 0.07 KDH DEL Start ----------------------------------------------------------------------
        'Dim i As Integer
        'Dim ACWItemCheck() As String = {"ACW:VOLT?", "SENS:ACW:JUDG:LOW:STAT?", "SENS:ACW:JUDG?", "ACW:VOLT:TIM:STAT?", "ACW:VOLT:TIM?", "ACW:VOLT:FREQ?", "ACW:VOLT:SWE:TIM?",
        '    "ROUT:ACW:TERM:CCH?", "ROUT:ACW:TERM? 101", "ROUT:ACW:TERM? 102", "ROUT:ACW:TERM? 103", "ROUT:ACW:TERM? 104",
        '    "ROUT:ACW:TERM? 201", "ROUT:ACW:TERM? 202", "ROUT:ACW:TERM? 203", "ROUT:ACW:TERM? 204",
        '    "ROUT:ACW:TERM? 301", "ROUT:ACW:TERM? 302", "ROUT:ACW:TERM? 303", "ROUT:ACW:TERM? 304"}
        'Dim ACWItemData(19) As String
        'Dim ACWItemCheckSpec(19) As String
        'Try
        '    Me.SendOnly("FUNC ACW")
        '    Me.SendOnly("ACW:VOLT " & frmMain.myDataSet.ACW_TestVoltage(Row).ToString)                       'TEST Voltage
        '    Me.SendOnly("SENS:ACW:JUDG:LOW:STAT " & frmMain.myDataSet.ACW_LowerJudgement(Row).ToString)      'Lower Pass OFF
        '    Me.SendOnly("SENS:ACW:JUDG:LOW " & frmMain.myDataSet.ACW_LowerCurr(Row).ToString)                'Lower Current Setting (mA)
        '    Me.SendOnly("SENS:ACW:JUDG " & frmMain.myDataSet.ACW_UpperCurr(Row).ToString)                    'Upper Current Setting (mA)
        '    Me.SendOnly("ACW:VOLT:TIM " & frmMain.myDataSet.ACW_TestTime(Row).ToString)                      'Test Time (s)
        '    Me.SendOnly("ACW:VOLT:FREQ " & frmMain.myDataSet.ACW_Freq(Row).ToString)                         'Test Freq(Hz)
        '    Me.SendOnly("ACW:VOLT:SWE:TIM " & frmMain.myDataSet.ACW_VoltageRiseTime(Row).ToString)           'Voltage Rise Time
        '    Me.SendOnly("ROUT:ACW:TERM:CCH " & frmMain.myDataSet.ACW_ContactCheck(Row).ToString)             'Contact Check Function (ON/OFF)
        '    Me.SendOnly("ROUT:ACW:TERM 101," & frmMain.myDataSet.ACW_Scanner101(Row).ToString)
        '    Me.SendOnly("ROUT:ACW:TERM 102," & frmMain.myDataSet.ACW_Scanner102(Row).ToString)
        '    Me.SendOnly("ROUT:ACW:TERM 103," & frmMain.myDataSet.ACW_Scanner103(Row).ToString)
        '    Me.SendOnly("ROUT:ACW:TERM 104," & frmMain.myDataSet.ACW_Scanner104(Row).ToString)
        '    Me.SendOnly("ROUT:ACW:TERM 201," & frmMain.myDataSet.ACW_Scanner201(Row).ToString)
        '    Me.SendOnly("ROUT:ACW:TERM 202," & frmMain.myDataSet.ACW_Scanner202(Row).ToString)
        '    Me.SendOnly("ROUT:ACW:TERM 203," & frmMain.myDataSet.ACW_Scanner203(Row).ToString)
        '    Me.SendOnly("ROUT:ACW:TERM 204," & frmMain.myDataSet.ACW_Scanner204(Row).ToString)
        '    Me.SendOnly("ROUT:ACW:TERM 301," & frmMain.myDataSet.ACW_Scanner301(Row).ToString)
        '    Me.SendOnly("ROUT:ACW:TERM 302," & frmMain.myDataSet.ACW_Scanner302(Row).ToString)
        '    Me.SendOnly("ROUT:ACW:TERM 303," & frmMain.myDataSet.ACW_Scanner303(Row).ToString)
        '    Me.SendOnly("ROUT:ACW:TERM 304," & frmMain.myDataSet.ACW_Scanner304(Row).ToString)

        '    ACWItemCheckSpec(0) = frmMain.myDataSet.ACW_TestVoltage(Row)
        '    ACWItemCheckSpec(1) = frmMain.myDataSet.ACW_LowerJudgement(Row)
        '    ACWItemCheckSpec(2) = frmMain.myDataSet.ACW_UpperCurr(Row)
        '    ACWItemCheckSpec(3) = frmMain.myDataSet.ACW_TimStat(Row)
        '    ACWItemCheckSpec(4) = frmMain.myDataSet.ACW_TestTime(Row)
        '    ACWItemCheckSpec(5) = frmMain.myDataSet.ACW_Freq(Row)
        '    ACWItemCheckSpec(6) = frmMain.myDataSet.ACW_VoltageRiseTime(Row)
        '    ACWItemCheckSpec(7) = frmMain.myDataSet.ACW_ContactCheck(Row)
        '    ACWItemCheckSpec(8) = frmMain.myDataSet.ACW_Scanner101(Row)
        '    ACWItemCheckSpec(9) = frmMain.myDataSet.ACW_Scanner102(Row)
        '    ACWItemCheckSpec(10) = frmMain.myDataSet.ACW_Scanner103(Row)
        '    ACWItemCheckSpec(11) = frmMain.myDataSet.ACW_Scanner104(Row)
        '    ACWItemCheckSpec(12) = frmMain.myDataSet.ACW_Scanner201(Row)
        '    ACWItemCheckSpec(13) = frmMain.myDataSet.ACW_Scanner202(Row)
        '    ACWItemCheckSpec(14) = frmMain.myDataSet.ACW_Scanner203(Row)
        '    ACWItemCheckSpec(15) = frmMain.myDataSet.ACW_Scanner204(Row)
        '    ACWItemCheckSpec(16) = frmMain.myDataSet.ACW_Scanner301(Row)
        '    ACWItemCheckSpec(17) = frmMain.myDataSet.ACW_Scanner302(Row)
        '    ACWItemCheckSpec(18) = frmMain.myDataSet.ACW_Scanner303(Row)
        '    ACWItemCheckSpec(19) = frmMain.myDataSet.ACW_Scanner304(Row)

        '    For i = 0 To 19
        '        ACWItemData(i) = Me.PortSendReceive(ACWItemCheck(i))
        '        Select Case i
        '            Case 0, 1, 2, 3, 4, 5, 6, 7
        '                If CDbl(ACWItemCheckSpec(i)) <> CDbl(ACWItemData(i)) Then
        '                    MsgBox("TOS상태가 DB데이터와 일치하지 않습니다" & VB.vbCrLf & "항목 : " & ACWItemCheck(i) & VB.vbCrLf & "행번호 : " & Row & VB.vbCrLf & "DB Data : " & ACWItemCheckSpec(i) & VB.vbCrLf & "TOS 상태 : " & CDbl(ACWItemData(i)))
        '                End If
        '            Case 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19
        '                If ACWItemCheckSpec(i).Length = 4 Then
        '                    If VB.Right(ACWItemCheckSpec(i), 4) <> VB.Left(ACWItemData(i), 4) Then
        '                        MsgBox("TOS상태가 DB데이터와 일치하지 않습니다" & VB.vbCrLf & "항목 : " & ACWItemCheck(i) & VB.vbCrLf & "행번호 : " & Row & VB.vbCrLf & "DB Data : " & ACWItemCheckSpec(i) & VB.vbCrLf & "TOS 상태 : " & ACWItemData(i))
        '                    End If
        '                ElseIf ACWItemCheckSpec(i).Length = 3 Then
        '                    If VB.Right(ACWItemCheckSpec(i), 3) <> VB.Left(ACWItemData(i), 3) Then
        '                        MsgBox("TOS상태가 DB데이터와 일치하지 않습니다" & VB.vbCrLf & "항목 : " & ACWItemCheck(i) & VB.vbCrLf & "행번호 : " & Row & VB.vbCrLf & "DB Data : " & ACWItemCheckSpec(i) & VB.vbCrLf & "TOS 상태 : " & ACWItemData(i))
        '                    End If
        '                Else
        '                    MsgBox("DB Data 이상")
        '                End If
        '        End Select

        '    Next
        '----------------------------------------------------- 'Ver 0.07 KDH DEL End ----------------------------------------------------------------------

        '----------------------------------------------------- 'Ver 0.07 KDH ADD Start ----------------------------------------------------------------------
        '
        Dim i As Integer
        Dim Testmode As String = frmMain.myDataSet.ACDC_Testmode(Row).ToString
        Dim ACWItemCheck() As String = {Testmode & ":VOLT?", "SENS:" & Testmode & ":JUDG:LOW:STAT?", "SENS:" & Testmode & ":JUDG?", Testmode & ":VOLT:TIM:STAT?",
        Testmode & ":VOLT:TIM?", Testmode & ":VOLT:FREQ?", Testmode & ":VOLT:SWE:TIM?",
        "ROUT:" & Testmode & ":TERM:CCH?", "ROUT:" & Testmode & ":TERM? 101", "ROUT:" & Testmode & ":TERM? 102", "ROUT:" & Testmode & ":TERM? 103", "ROUT:" & Testmode & ":TERM? 104",
        "ROUT:" & Testmode & ":TERM? 201", "ROUT:" & Testmode & ":TERM? 202", "ROUT:" & Testmode & ":TERM? 203", "ROUT:" & Testmode & ":TERM? 204",
        "ROUT:" & Testmode & ":TERM? 301", "ROUT:" & Testmode & ":TERM? 302", "ROUT:" & Testmode & ":TERM? 303", "ROUT:" & Testmode & ":TERM? 304", "FUNC?"}

        Dim ACWItemData(20) As String
        Dim ACWItemCheckSpec(20) As String
        Try
            Me.SendOnly("*RST")
            Me.SendOnly("FUNC " & Testmode)
            Me.SendOnly(Testmode & ":VOLT " & frmMain.myDataSet.ACW_TestVoltage(Row).ToString)                       'TEST Voltage
            Me.SendOnly("SENS:" & Testmode & ":JUDG:LOW:STAT " & frmMain.myDataSet.ACW_LowerJudgement(Row).ToString)      'Lower Pass OFF
            Me.SendOnly("SENS:" & Testmode & ":JUDG:LOW " & frmMain.myDataSet.ACW_LowerCurr(Row).ToString)                'Lower Current Setting (mA)
            Me.SendOnly("SENS:" & Testmode & ":JUDG " & frmMain.myDataSet.ACW_UpperCurr(Row).ToString)                    'Upper Current Setting (mA)
            Me.SendOnly(Testmode & ":VOLT:TIM " & frmMain.myDataSet.ACW_TestTime(Row).ToString)                      'Test Time (s)
            If Testmode = "ACW" Then
                Me.SendOnly(Testmode & ":VOLT:FREQ " & frmMain.myDataSet.ACW_Freq(Row).ToString)                         'Test Freq(Hz)
            End If
            Me.SendOnly(Testmode & ":VOLT:SWE:TIM " & frmMain.myDataSet.ACW_VoltageRiseTime(Row).ToString)           'Voltage Rise Time
            Me.SendOnly("ROUT:" & Testmode & ":TERM:CCH " & frmMain.myDataSet.ACW_ContactCheck(Row).ToString)             'Contact Check Function (ON/OFF)
            Me.SendOnly("ROUT:" & Testmode & ":TERM 101," & frmMain.myDataSet.ACW_Scanner101(Row).ToString)
            Me.SendOnly("ROUT:" & Testmode & ":TERM 102," & frmMain.myDataSet.ACW_Scanner102(Row).ToString)
            Me.SendOnly("ROUT:" & Testmode & ":TERM 103," & frmMain.myDataSet.ACW_Scanner103(Row).ToString)
            Me.SendOnly("ROUT:" & Testmode & ":TERM 104," & frmMain.myDataSet.ACW_Scanner104(Row).ToString)
            Me.SendOnly("ROUT:" & Testmode & ":TERM 201," & frmMain.myDataSet.ACW_Scanner201(Row).ToString)
            Me.SendOnly("ROUT:" & Testmode & ":TERM 202," & frmMain.myDataSet.ACW_Scanner202(Row).ToString)
            Me.SendOnly("ROUT:" & Testmode & ":TERM 203," & frmMain.myDataSet.ACW_Scanner203(Row).ToString)
            Me.SendOnly("ROUT:" & Testmode & ":TERM 204," & frmMain.myDataSet.ACW_Scanner204(Row).ToString)
            Me.SendOnly("ROUT:" & Testmode & ":TERM 301," & frmMain.myDataSet.ACW_Scanner301(Row).ToString)
            Me.SendOnly("ROUT:" & Testmode & ":TERM 302," & frmMain.myDataSet.ACW_Scanner302(Row).ToString)
            Me.SendOnly("ROUT:" & Testmode & ":TERM 303," & frmMain.myDataSet.ACW_Scanner303(Row).ToString)
            Me.SendOnly("ROUT:" & Testmode & ":TERM 304," & frmMain.myDataSet.ACW_Scanner304(Row).ToString)

            ACWItemCheckSpec(0) = frmMain.myDataSet.ACW_TestVoltage(Row)
            ACWItemCheckSpec(1) = frmMain.myDataSet.ACW_LowerJudgement(Row)
            ACWItemCheckSpec(2) = frmMain.myDataSet.ACW_UpperCurr(Row)
            ACWItemCheckSpec(3) = frmMain.myDataSet.ACW_TimStat(Row)
            ACWItemCheckSpec(4) = frmMain.myDataSet.ACW_TestTime(Row)
            ACWItemCheckSpec(5) = frmMain.myDataSet.ACW_Freq(Row)
            ACWItemCheckSpec(6) = frmMain.myDataSet.ACW_VoltageRiseTime(Row)
            ACWItemCheckSpec(7) = frmMain.myDataSet.ACW_ContactCheck(Row)
            ACWItemCheckSpec(8) = frmMain.myDataSet.ACW_Scanner101(Row)
            ACWItemCheckSpec(9) = frmMain.myDataSet.ACW_Scanner102(Row)
            ACWItemCheckSpec(10) = frmMain.myDataSet.ACW_Scanner103(Row)
            ACWItemCheckSpec(11) = frmMain.myDataSet.ACW_Scanner104(Row)
            ACWItemCheckSpec(12) = frmMain.myDataSet.ACW_Scanner201(Row)
            ACWItemCheckSpec(13) = frmMain.myDataSet.ACW_Scanner202(Row)
            ACWItemCheckSpec(14) = frmMain.myDataSet.ACW_Scanner203(Row)
            ACWItemCheckSpec(15) = frmMain.myDataSet.ACW_Scanner204(Row)
            ACWItemCheckSpec(16) = frmMain.myDataSet.ACW_Scanner301(Row)
            ACWItemCheckSpec(17) = frmMain.myDataSet.ACW_Scanner302(Row)
            ACWItemCheckSpec(18) = frmMain.myDataSet.ACW_Scanner303(Row)
            ACWItemCheckSpec(19) = frmMain.myDataSet.ACW_Scanner304(Row)
            ACWItemCheckSpec(20) = frmMain.myDataSet.ACDC_Testmode(Row)

            For i = 0 To 20
                If Testmode = "DCW" And i = 5 Then
                    Continue For
                End If
                ACWItemData(i) = Me.PortSendReceive(ACWItemCheck(i))
                Select Case i
                    Case 0, 1, 2, 3, 4, 5, 6, 7
                        If CDbl(ACWItemCheckSpec(i)) <> CDbl(ACWItemData(i)) Then
                            MsgBox("TOS상태가 DB데이터와 일치하지 않습니다" & VB.vbCrLf & "항목 : " & ACWItemCheck(i) & VB.vbCrLf & "행번호 : " & Row & VB.vbCrLf & "DB Data : " & ACWItemCheckSpec(i) & VB.vbCrLf & "TOS 상태 : " & CDbl(ACWItemData(i)))
                        End If
                    Case 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
                        If ACWItemCheckSpec(i).Length = 4 Then
                            If VB.Right(ACWItemCheckSpec(i), 4) <> VB.Left(ACWItemData(i), 4) Then
                                MsgBox("TOS상태가 DB데이터와 일치하지 않습니다" & VB.vbCrLf & "항목 : " & ACWItemCheck(i) & VB.vbCrLf & "행번호 : " & Row & VB.vbCrLf & "DB Data : " & ACWItemCheckSpec(i) & VB.vbCrLf & "TOS 상태 : " & ACWItemData(i))
                            End If
                        ElseIf ACWItemCheckSpec(i).Length = 3 Then
                            If VB.Right(ACWItemCheckSpec(i), 3) <> VB.Left(ACWItemData(i), 3) Then
                                MsgBox("TOS상태가 DB데이터와 일치하지 않습니다" & VB.vbCrLf & "항목 : " & ACWItemCheck(i) & VB.vbCrLf & "행번호 : " & Row & VB.vbCrLf & "DB Data : " & ACWItemCheckSpec(i) & VB.vbCrLf & "TOS 상태 : " & ACWItemData(i))
                            End If
                        Else
                            MsgBox("DB Data 이상")
                        End If
                End Select

            Next
            '----------------------------------------------------- 'Ver 0.07 KDH ADD End ----------------------------------------------------------------------
        Catch ex As System.Exception
            Throw New ApplicationException("[ERROR] SendReceive: " & ex.ToString)
        End Try
    End Sub
    Public Sub SendOnly(ByVal Data As String)

        Try
            TOS9300.WriteString(Data)
        Catch ex As Exception
            Throw New ApplicationException("[ERROR] Send: " & ex.ToString)
        End Try

    End Sub
    Public Function GetCOND() As Double
        Dim ReceiveString As String = ""
        Try
            Me.PortSendReceive("STAT:OPER:TEST:COND?")
            Return CDbl(ReceiveString)
        Catch ex As System.Exception
            Throw New ApplicationException(String.Format("{0} : GetCOND に失敗しました。" & ex.ToString))
        End Try
    End Function
End Class
