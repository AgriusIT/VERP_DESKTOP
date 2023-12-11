Public Class ItemTaskProgressBE
    Public Property ProgressId As Integer
    Public Property ProgressNo As String
    Public Property ProgressDate As DateTime
    Public Property VendorId As Integer
    Public Property POId As Integer
    Public Property ItemId As Integer
    Public Property AccountId As Integer
    Public Property Approved As Boolean
    Public Property CostCenterId As Integer
    Public Property SendForApproval As Boolean
    Public Property Detail As List(Of ItemTaskProgressDetailBE)

End Class

Public Class ItemTaskProgressDetailBE
    Public Property DetailId As Integer
    Public Property ContractId As Integer
    Public Property TaskId As Integer
    Public Property TaskTitle As String
    Public Property TaskDetail As String
    Public Property TaskRate As Double
    Public Property TaskUnit As String
    Public Property Qty As Double
    Public Property Measurment As Double
    Public Property ContractValue As Double
    Public Property PreviousProgress As Double
    Public Property CurrentProgress As Double
    Public Property ApprovedProgress As Double
    Public Property NetProgress As Double
    Public Property PendingProgress As Double
    Public Property NetValue As Double
End Class