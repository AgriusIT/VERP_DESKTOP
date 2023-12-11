Public Class PurchaseOrderMaster
    Public Property PurchaseOrderId As Integer
    Public Property LocationId As Integer
    Public Property PurchaseOrderNo As String
    Public Property PurchaseOrderDate As DateTime
    Public Property VendorId As Integer
    Public Property PartyInvoiceNo As String
    Public Property PartySlipNo As String
    Public Property PurchaseOrderQty As Integer
    Public Property PurchaseOrderAmount As Integer
    Public Property CashPaid As Decimal
    Public Property Remarks As String
    Public Property UserName As String
    Public Property Status As String
    Public Property DcNo As String
    Public Property LCId As Integer
    Public Property CurrencyType As Integer
    Public Property CurrencyRate As Double
    Public Property Receiving_Date As DateTime
    Public Property Terms_And_Condition As String
    Public Property Post As Boolean
    Public Property RefCMFADocId As Integer
    Public Property CostCenterId As Integer
    Public Property PurchaseDemandId As Integer
    Public Property POType As String
    Public Property POStockDispatchStatus As Integer
    Public Property UpdateUserName As String
    Public Property DetailList As List(Of PurchaseOrderDetail)
    Public Sub New()
        DetailList = New List(Of PurchaseOrderDetail)
    End Sub
End Class
