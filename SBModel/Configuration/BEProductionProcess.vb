Public Class BEProductionProcess
    Public Property ProductionProcessId As Integer
    Public Property ProcessName As String
    Public Property Remarks As String
    Public Property WIPAccountId As Integer
    Public Property Detail As List(Of BEProductionProcessDetail)
    Sub New()
        Detail = New List(Of BEProductionProcessDetail)
    End Sub
End Class
Public Class BEProductionProcessDetail
    Public Property ProductionProcessDetailId As Integer
    Public Property ProductionProcessId
    Public Property ProductionStepId As Integer
    Public Property SortOrder As Integer
    Public Property State As String
End Class
