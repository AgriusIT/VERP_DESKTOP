Public Class StockAuditTableBE
    Public Property ID As Integer
    Public Property StockDate As DateTime
    Public Property StockAuditName As String
    Public Property SessionName As String
    Public Property Remarks As String

    Public Property Locations As List(Of StockAuditLocationsBE)
    Public Property IsClosed As Boolean
    Public Property ActivityLog() As ActivityLog

    Public Sub New()
        Locations = New List(Of StockAuditLocationsBE)
        ActivityLog = New ActivityLog()
    End Sub
End Class


