Public Class PropertyPurchaseBE
    Public Property PropertyPurchaseId As Integer
    Public Property Title As String
    Public Property PlotNo As String
    Public Property PlotSize As String
    Public Property Block As String
    Public Property Sector As String
    Public Property SerialNo As String
    Public Property Location As String

    Public Property CellNo As String

    Public Property City As String
    Public Property SellerAccountId As Integer

    Public Property PurchaseId As Integer

    Public Property Price As Integer
    Public Property Remarks As String
    Public Property PropertyType As String
    Public Property Cost_CenterId As Integer
    Public Property ActivityLog() As ActivityLog

    Public Property VoucherNo As String

    Public Property PDetail As List(Of PurchaseTypeBE)

    Public Property TDetail As List(Of TaskBE)
    Public Property PropertyDate As DateTime
End Class


Public Class PurchaseTypeBE

    Public Property PaymentTypeId As Integer

    Public Property PropertyPurchaseId As Integer

    Public Property PaymentTypeDate As String

    Public Property PaymentTypeName As String

    Public Property Amount As Double

    Public Property AmountPaid As Double
End Class

Public Class TaskBE

    Public Property TaskId As Integer

    Public Property Name As String

    Public Property PropertyPurchaseId As Integer

    Public Property DueDate As String

    Public Property Status As Boolean

End Class