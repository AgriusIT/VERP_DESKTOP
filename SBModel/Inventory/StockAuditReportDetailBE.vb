Public Class StockAuditReportDetailBE
    Public Property ID As Integer
    Public Property StockAuditReportId As Integer
    Public Property ArticleId As Integer
    Public Property Unit As String
    Public Property Rate As Double
    Public Property PackQty As Double
    Public Property Qty As Double
    Public Property PackStockQty As Double
    Public Property StockQty As Double

    Public Property TotalQty As Double
    Public Property BalancePackQty As Double

    Public Property BalanceQty As Double

    Public Property LocationId As Integer
    Public Property ActivityLog() As ActivityLog
End Class


