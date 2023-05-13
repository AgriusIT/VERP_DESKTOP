''26-May-2014 TASK:2647 Imran Ali Cross Cheque Printing
Imports CrystalDecisions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Public Class frmRptChequePrintReportViewer
    Dim crpt As New ReportDocument
    Public ReportName As String
    Private Sub frmRptChequePrintReportViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            crpt.Load(Application.StartupPath & "\Reports\" & ReportName & ".rpt")
            Me.CrystalReportViewer1.ReportSource = crpt
            If str_ReportParam.Length > 1 Then
                Try 'Task:2647 Added Try Catch
                    Dim str() As String = str_ReportParam.Split("&")
                    For Each str1 As String In str
                        Dim strParam() As String = str1.Split("|")
                        crpt.SetParameterValue(strParam(0), strParam(1))
                    Next
                Catch ex As Exception

                End Try
                'End Task:2647
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            str_ReportParam = String.Empty
        End Try
    End Sub
End Class