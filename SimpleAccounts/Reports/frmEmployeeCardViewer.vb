Imports CRUFLIDAutomation
Public Class frmEmployeeCardViewer
    Public _Dt As DataTable
    Public ReportName As String
    Private Sub frmEmployeeCardViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim ds As New dsEmployeeInformation
            ds.Tables.Add(_Dt)
            'Dim MDS As New Microsoft.Reporting.WinForms.ReportDataSource("dsEmployeeInformation_dtEmployeeInformation", ds.Tables(1))
            'Me.ReportViewer1.LocalReport.DataSources.Add(MDS)
            'Me.ReportViewer1.LocalReport.ReportPath = str_ApplicationStartUpPath & "\Reports\" & ReportName
            'Me.ReportViewer1.Visible = True
            'Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class