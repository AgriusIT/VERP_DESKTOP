Public Class CustomerCategoryBE
    Public Property CategoryID As Integer
    Public Property CategoryName As String
    Public Property Active As Boolean
    Public Property SortOrder As Integer
    Public Property Remarks As String
    Public Property ActivityLog() As ActivityLog
End Class
