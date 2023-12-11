Public Class LogicalBifurcationBE
    Public Property LogicalBifurcationId As Integer
    Public Property DocumentNo As String
    Public Property DocumentDate As DateTime
    Public Property StartDate As DateTime
    Public Property FromCostCenterId As Integer
    Public Property Remarks As String
    Public Property ActivityLog() As ActivityLog
    Public Property Detail As List(Of LogicalBifurcationDetailBE)
    Public Sub New()
        Detail = New List(Of LogicalBifurcationDetailBE)
        ActivityLog = New ActivityLog()
    End Sub
End Class
