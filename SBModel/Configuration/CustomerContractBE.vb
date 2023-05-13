Public Class CustomerContractBE

    Public Property ContractId As Integer
    Public Property ContractNo As String
    Public Property ContractDate As Date
    Public Property CustomerId As Integer
    Public Property CustomerName As String
    Public Property SOId As Integer
    Public Property SOQty As Double
    Public Property CostCenter As String
    Public Property ItemId As Integer
    Public Property ItemName As String
    Public Property TermId As Integer
    Public Property ContractValue As Double
    Public Property RetentionPercentage As Double
    Public Property RetentionValue As Double
    Public Property MobilizationPerBill As Double
    Public Property MobilizationPercentage As Double
    Public Property MobilizationValue As Double
    Public Property BankId As Double
    Public Property ChequeAmount As Double
    Public Property ChequeNo As Long
    Public Property ChequeDate As Date
    Public Property ChequeDetails As String
    Public Property RetentionAccountId As Integer
    Public Property MobilizationAccountId As Integer
    Public Property VoucherId As Integer
    Public Property CostCenterId As Integer
    Public Property Detail As List(Of CustomerContractDetailBE)

End Class


Public Class CustomerContractDetailBE
    Public Property DetailId As Integer
    Public Property TaskId As Integer
    Public Property TaskTitle As String
    Public Property TaskDetail As String
    Public Property TaskUnit As String
    Public Property TaskRate As Double
    Public Property Qty As Double
    Public Property TotalMeasurment As Double
    Public Property NetValue As Double
End Class