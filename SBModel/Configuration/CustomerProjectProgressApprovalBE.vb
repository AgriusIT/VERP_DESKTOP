Public Class CustomerProjectProgressApprovalBE
    Public Property ApprovalId As Integer
    Public Property CustomerId As Integer
    Public Property CustomerName As String
    Public Property SOId As Integer
    Public Property ContractId As Integer
    Public Property ContractNo As String
    Public Property CostCenter As String
    Public Property ProgressId As Integer
    Public Property ProgressNo As String
    Public Property ProgressDate As String
    Public Property CurrentProgress As Double
    Public Property RetentionPercentage As Double
    Public Property RetentionValue As Double
    Public Property MobilizationPercentage As Double
    Public Property MobilizationValue As Double
    Public Property BillAmount As Double
    Public Property TotalDeduction As Double
    Public Property NetValue As Double
    Public Property Remarks As String
    Public Property ContractValue As Double
    Public Property AccountId As Integer
    Public Property Approved As Boolean
    Public Property RetentionAccountId As Integer
    Public Property MobilizationAccountId As Integer
    Public Property CostCenterId As Integer
    Public Property RejectedId As Integer
    Public Property ItemName As String
    Public Property TotalProgress As Double
    Public Property PreviousAmount As Double
    Public Property ExpenseList As List(Of CustomerProjectProgressApprovalExpenseBE)
End Class


Public Class CustomerProjectProgressApprovalExpenseBE
    Public Property ApprovalId As Integer
    Public Property AccountId As Integer
    Public Property detail_code As String
    Public Property detail_title As String
    Public Property Amount As Double
    Public Property Comments As String
End Class
