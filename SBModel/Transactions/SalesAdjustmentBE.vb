Public Class SalesAdjustmentBE
    Public Property AdjustmentId As Integer
    Public Property VoucherId As Integer
    Public Property DocNo As String
    Public Property DocDate As DateTime
    Public Property CompanyId As Integer
    Public Property CostCenterId As Integer
    Public Property CustomerId As Integer
    Public Property Remarks As String
    Public Property Detail As List(Of SalesAdjustmentDetailBE)
End Class
Public Class SalesAdjustmentDetailBE
    Public Property AdjustmentDetailId As Integer
    Public Property InvoiceId As Integer
    Public Property ItemId As Integer
    Public Property ItemAccountId As Integer
    Public Property Amount As Double
    Public Property Reason As String
End Class