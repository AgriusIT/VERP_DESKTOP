Public Class PropertySalesBE
    Public Property PropertySalesId As Integer
    Public Property Title As String
    Public Property PlotNo As String
    Public Property PlotSize As String
    Public Property Block As String
    Public Property Sector As String
    Public Property Location As String
    Public Property City As String
    Public Property SerialNo As String
    Public Property BuyerAccountId As Integer

    Public Property SalesId As Integer
    Public Property Cost_CenterId As Integer
    Public Property ItemId As Integer
    Public Property Price As Double
    Public Property SellerPrice As Double

    Public Property Remarks As String
    Public Property PropertyType As String
    Public Property CellNo As String
    Public Property ActivityLog() As ActivityLog

    Public Property VoucherNo As String

    Public Property PDetail As List(Of PurchaseTypeSalesBE)

    Public Property TDetail As List(Of TaskSalesBE)

    Public Property PurchaseAccountId As Integer
    Public Property CostOfSalesAccountId As Integer
    Public Property AdjustedVoucherNo As String

    Public Property PropertyDate As DateTime

End Class

Public Class PurchaseTypeSalesBE

    Public Property PaymentTypeId As Integer

    Public Property PropertyPurchaseId As Integer

    Public Property PaymentTypeDate As String

    Public Property PaymentTypeName As String

    Public Property Amount As Double

    Public Property AmountPaid As Double
End Class

Public Class TaskSalesBE

    Public Property TaskId As Integer

    Public Property Name As String

    Public Property PropertyPurchaseId As Integer

    Public Property DueDate As String

    Public Property Status As Boolean

End Class