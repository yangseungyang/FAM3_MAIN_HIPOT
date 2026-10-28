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
Public Class clsTestDBSet

    Public ACWMemories As Object = Nothing
    Public Test_Item As Object = Nothing

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
    Public ACW_Scanner301(100) As String
    Public ACW_Scanner302(100) As String
    Public ACW_Scanner303(100) As String
    Public ACW_Scanner304(100) As String
    Public ACDC_Testmode(100) As String             'Ver 0.07 KDH ADD

    Dim mAppPath As String = Mid(Application.StartupPath, 1, InStr(Application.StartupPath, "bin") - 1)

    Private Sub mSecWait(ByVal mSec As Integer)
        System.Threading.Thread.Sleep(mSec)         'mSec秒のWait
    End Sub

    Public Sub TesDBSet()

        Dim i As Integer

        Dim xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
        Dim xlBook As Microsoft.Office.Interop.Excel.Workbook = Nothing
        Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing

        Try
            xlApp = CType(VB.CreateObject("Excel.Application"), Microsoft.Office.Interop.Excel.Application)
            xlBook = xlApp.Workbooks.Open(mAppPath & "\TestDB\TestDB_FAM3_Main.xls")
            xlSheet = CType(xlBook.Worksheets("ACW"), Microsoft.Office.Interop.Excel.Worksheet)
            'ACWMemories = xlSheet.Range("A2:V200").Value                              '(19X200) except colum name 'Ver 0.07 KDH DEL
            ACWMemories = xlSheet.Range("A2:W200").Value                              '(19X200) except colum name 'Ver 0.07 KDH ADD
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet)

            xlApp.Quit()
            xlSheet = Nothing
            xlBook = Nothing
            xlApp = Nothing

            System.GC.Collect()

            For i = 1 To 100
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
                ACW_Scanner301(i) = ACWMemories(i, 19)
                ACW_Scanner302(i) = ACWMemories(i, 20)
                ACW_Scanner303(i) = ACWMemories(i, 21)
                ACW_Scanner304(i) = ACWMemories(i, 22)
                ACDC_Testmode(i) = ACWMemories(i, 23)           'Ver 0.07 KDH ADD
                System.Windows.Forms.Application.DoEvents()
                If ACW_No(i) = "#" Then
                    Exit For
                End If
            Next

            mSecWait(0.2)
        Catch ex As System.Exception

            mSecWait(0.2)
            Throw New System.ApplicationException("DB Read NG!!")

        End Try
    End Sub


End Class
