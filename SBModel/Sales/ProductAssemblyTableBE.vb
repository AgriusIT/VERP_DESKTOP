Public Class ProductAssemblyTableBE
    Public Property ProductAssemblyId As Integer
    Public Property SalesId As Integer
    Public Property LocationId As Integer
    Public Property ArticleDefId As Integer
    Public Property ArticleSize As String
    Public Property Qty As Double
    Public Property Price As Double
    Public Property ActivityLog() As ActivityLog
End Class
