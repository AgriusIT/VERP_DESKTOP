Public Class StockAuditReportMasterBE
    Public Property ID As Integer
    Public Property DocumentDate As datetime
    Public Property DocumentNo As String
    Public Property StockAuditId As Integer
    Public Property Detail As List(Of StockAuditReportDetailBE)
    Public Property Stock As StockMaster
    Public Property ActivityLog() As ActivityLog

    Public Sub New()
        Detail = New List(Of StockAuditReportDetailBE)
        ActivityLog = New ActivityLog()
        Stock = New StockMaster()
    End Sub
End Class


