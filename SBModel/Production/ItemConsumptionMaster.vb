''TASK:908 on 13-06-2017. Done by Ameen
Public Class ItemConsumptionMaster
    Public Property ComsumptionId As Integer
    Public Property DocNo As String
    Public Property DocDate As DateTime
    Public Property Remarks As String
    Public Property PlanId As Integer
    Public Property TicketId As Integer
    Public Property DepartmentId As Integer
    Public Property StoreIssuanceAccountId As Integer
    Public Property Detail As List(Of ItemConsumptionDetail)
    Public Property DispatchId As Integer
    Sub New()
        Detail = New List(Of ItemConsumptionDetail)
    End Sub
End Class
