Public Class RetentionTransferBE
    Public Property RetentionMasterId As Integer
    Public Property VoucherNo As String
    Public Property VoucherDate As DateTime
    Public Property VendorId As Integer
    Public Property POId As Integer
    Public Property VendorConractId As Integer
    Public Property CostCenterId As Integer
    Public Property ArticleId As Integer
    Public Property Remarks As String
    Public Property TransferPer As Double
    Public Property RetentionAccountId As Integer
    Public Property CurrentPayables As Double
    Public Property ActivityLog() As ActivityLog
    Public Property Detail As List(Of RetentionTransferDetail)
End Class

Public Class RetentionTransferDetail
    Public Property RetentionDetailId As Integer
    Public Property RetentionMasterId As Integer
    Public Property ContractId As Integer
    Public Property ContractValue As Double
    Public Property AmountPaid As Double
    Public Property BalanceAmount As Double
    Public Property RententionValue As Double
    Public Property RetentionPaid As Double
    Public Property TransferPer As Double
    Public Property RealizedAmount As Double
    Public Property CurrentPayables As Double
    Public Property Remarks As String
End Class
