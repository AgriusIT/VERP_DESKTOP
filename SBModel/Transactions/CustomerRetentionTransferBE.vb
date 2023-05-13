Public Class CustomerRetentionTransferBE
    Public Property RetentionMasterId As Integer
    Public Property VoucherNo As String
    Public Property VoucherDate As DateTime
    Public Property CustomerId As Integer
    Public Property SOId As Integer
    Public Property ContractId As Integer
    Public Property CostCenterId As Integer
    Public Property ArticleId As Integer
    Public Property Remarks As String
    Public Property TransferPer As Double
    Public Property RetentionAccountId As Integer
    Public Property CurrentReceivables As Double
    Public Property ActivityLog() As ActivityLog
    Public Property Detail As List(Of RetentionTransferDetail)
End Class

Public Class CustomerRetentionTransferDetail
    Public Property RetentionDetailId As Integer
    Public Property RetentionMasterId As Integer
    Public Property ContractId As Integer
    Public Property ContractValue As Double
    Public Property AmountReceived As Double
    Public Property BalanceAmount As Double
    Public Property RententionValue As Double
    Public Property RetentionReceived As Double
    Public Property TransferPer As Double
    Public Property RealizedAmount As Double
    Public Property CurrentReceivables As Double
    Public Property Remarks As String
End Class
