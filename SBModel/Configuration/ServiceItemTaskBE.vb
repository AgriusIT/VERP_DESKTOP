Public Class ServiceItemTaskBE

    Public Property ItemId As Integer
    Public Property Detail As List(Of ServiceItemDetailTaskBE)

End Class

Public Class ServiceItemDetailTaskBE
    Public Property TaskId As Integer
    Public Property TaskTitle As String
    Public Property TaskDetail As String
    Public Property TaskUnit As String
    Public Property TaskRate As Double
    Public Property SortOrder As Integer
    Public Property Active As Boolean
End Class
