Public Class PlanMasterTableBE
    Public Property PlanId As Integer
    Public Property LocationId As Integer
    Public Property PlanNo As String
    Public Property PlanDate As DateTime
    Public Property CustomerId As Integer
    Public Property PartyInvoiceNo As String
    Public Property PartySlipNo As String
    Public Property PlanQty As Integer
    Public Property PlanAmount As Integer
    Public Property Remarks As String
    Public Property UserName As String
    Public Property Status As String
    Public Property PoId As Integer
    Public Property EmployeeCode As Integer
    Public Property ProductionStatus As String
    Public Property CompletionDate As DateTime
    Public Property StartDate As DateTime
    Public Property ActivityLog() As ActivityLog

    Public Property Detail As List(Of PlanDetailTableBE)

    Sub New()
        Detail = New List(Of PlanDetailTableBE)
    End Sub

End Class
