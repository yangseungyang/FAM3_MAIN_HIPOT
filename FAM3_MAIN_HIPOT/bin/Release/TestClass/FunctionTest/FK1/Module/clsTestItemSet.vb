Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Ivi.Visa.Interop
'---------------------------------------------------------------
'   clsTestItemSet
'   Copyright(c)  2016 Yokogawa Electric Corporation.
'
'   Comment:  
'   Modify : Ver 0.00 2016/11/01 新規作成
'---------------------------------------------------------------
Public Class clsTestItemSet
    Inherits clsTestBase

    Public ACWMemories As Object = Nothing
    Public IRMemories As Object = Nothing

    Public IR_No(100) As String
    Public IR_TestVoltage(100) As String
    Public IR_LowerRes(100) As String
    Public IR_DelAuto(100) As String
    Public IR_TestTime(100) As String
    Public IR_TimStat(100) As String
    Public IR_UpperJudgement(100) As String
    Public IR_LowerJudgement(100) As String
    Public IR_VoltageRiseTime(100) As String
    Public IR_LowPassFilter(100) As String
    Public IR_ContactCheck(100) As String
    Public IR_Scanner101(100) As String
    Public IR_Scanner102(100) As String
    Public IR_Scanner103(100) As String
    Public IR_Scanner104(100) As String
    Public IR_Scanner201(100) As String
    Public IR_Scanner202(100) As String
    Public IR_Scanner203(100) As String
    Public IR_Scanner204(100) As String

    Public ACW_No(100) As String
    Public ACW_TestVoltage(100) As String
    Public ACW_LowerJudgement(100) As String
    Public ACW_LowerCurr(100) As String
    Public ACW_UpperCurr(100) As String
    Public ACW_TimStat(100) As String
    Public ACW_TestTime(100) As String
    Public ACW_Freq(100) As String
    Public ACW_VoltageRiseTime(100) As String
    Public ACW_ContactCheck(100) As String
    Public ACW_Scanner101(100) As String
    Public ACW_Scanner102(100) As String
    Public ACW_Scanner103(100) As String
    Public ACW_Scanner104(100) As String
    Public ACW_Scanner201(100) As String
    Public ACW_Scanner202(100) As String
    Public ACW_Scanner203(100) As String
    Public ACW_Scanner204(100) As String

    Private JUXTAtest As clsTestAll_Function

    'Private myExcel As clsExcelRW
    Private myfunc As clsFunctionBase
    Private myPlc As clsHiPot

    Public Sub TestItemSet()

        myfunc = New clsFunctionBase
        myPlc = New clsHiPot
        Dim i As Integer
        Dim x As Integer

        Dim xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
        Dim xlBook As Microsoft.Office.Interop.Excel.Workbook = Nothing
        Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing
        Dim xlSheet2 As Microsoft.Office.Interop.Excel.Worksheet = Nothing




        Try
            xlApp = CType(VB.CreateObject("Excel.Application"), Microsoft.Office.Interop.Excel.Application)
            xlBook = xlApp.Workbooks.Open("C:\NewSC_HIPOT\2. DataBase\TestDB_NewSC.xlsx")
            xlSheet = CType(xlBook.Worksheets("ACW"), Microsoft.Office.Interop.Excel.Worksheet)
            xlSheet2 = CType(xlBook.Worksheets("IR"), Microsoft.Office.Interop.Excel.Worksheet)
            ACWMemories = xlSheet.Range("A1:S200").Value                              '(19X200)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet)
            IRMemories = xlSheet2.Range("A1:S200").Value                              '(19X200)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet2)

            xlApp.Quit()
            xlSheet = Nothing
            xlSheet2 = Nothing
            xlBook = Nothing
            xlApp = Nothing

            System.GC.Collect()

            For i = 2 To 100
                ACW_No(i) = ACWMemories(i, 1)
                ACW_TestVoltage(i) = ACWMemories(i, 2)
                ACW_LowerJudgement(i) = ACWMemories(i, 3)
                ACW_LowerCurr(i) = ACWMemories(i, 4)
                ACW_UpperCurr(i) = ACWMemories(i, 5)
                ACW_TimStat(i) = ACWMemories(i, 6)
                ACW_TestTime(i) = ACWMemories(i, 7)
                ACW_Freq(i) = ACWMemories(i, 8)
                ACW_VoltageRiseTime(i) = ACWMemories(i, 9)
                ACW_ContactCheck(i) = ACWMemories(i, 10)
                ACW_Scanner101(i) = ACWMemories(i, 11)
                ACW_Scanner102(i) = ACWMemories(i, 12)
                ACW_Scanner103(i) = ACWMemories(i, 13)
                ACW_Scanner104(i) = ACWMemories(i, 14)
                ACW_Scanner201(i) = ACWMemories(i, 15)
                ACW_Scanner202(i) = ACWMemories(i, 16)
                ACW_Scanner203(i) = ACWMemories(i, 17)
                ACW_Scanner204(i) = ACWMemories(i, 18)
                System.Windows.Forms.Application.DoEvents()
                If ACW_No(i) = "#" Then
                    Exit For
                End If
            Next

            myfunc.Waits(0.2)
            For x = 2 To 100
                IR_No(x) = IRMemories(x, 1)
                IR_TestVoltage(x) = IRMemories(x, 2)
                IR_LowerRes(x) = IRMemories(x, 3)
                IR_DelAuto(x) = IRMemories(x, 4)
                IR_TestTime(x) = IRMemories(x, 5)
                IR_TimStat(x) = IRMemories(x, 6)
                IR_UpperJudgement(x) = IRMemories(x, 7)
                IR_LowerJudgement(x) = IRMemories(x, 8)
                IR_VoltageRiseTime(x) = IRMemories(x, 9)
                IR_LowPassFilter(x) = IRMemories(x, 10)
                IR_ContactCheck(x) = IRMemories(x, 11)
                IR_Scanner101(x) = IRMemories(x, 12)
                IR_Scanner102(x) = IRMemories(x, 13)
                IR_Scanner103(x) = IRMemories(x, 14)
                IR_Scanner104(x) = IRMemories(x, 15)
                IR_Scanner201(x) = IRMemories(x, 16)
                IR_Scanner202(x) = IRMemories(x, 17)
                IR_Scanner203(x) = IRMemories(x, 18)
                IR_Scanner204(x) = IRMemories(x, 19)
                System.Windows.Forms.Application.DoEvents()
                If IR_No(x) = "#" Then
                    Exit For
                End If
            Next

            Waits(0.2)
        Catch ex As System.Exception

            Waits(0.2)
            Throw New System.ApplicationException("DB Read NG!!")

        End Try
    End Sub


End Class
