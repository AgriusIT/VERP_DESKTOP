Public Class StockAuditLocationsBE
    Public Property ID As Integer
    Public Property LocationId As Integer
    Public Property StockAuditId As Integer
    Public Property ActivityLog() As ActivityLog

    Public Sub New()
        ActivityLog = New ActivityLog()
    End Sub
End Class

