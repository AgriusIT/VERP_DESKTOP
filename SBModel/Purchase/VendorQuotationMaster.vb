Public Class VendorQuotationMaster
    Public Property VendorQuotationId As Integer
    Public Property VendorQuotationNo As String
    Public Property VendorQuotationDate As DateTime
    Public Property VendorQuotationExpiryDate As DateTime
    Public Property VendorId As Integer
    Public Property PurchaseInquiryId As Integer
    Public Property Amount As Double
    Public Property Discount As Double
    Public Property NetTotal As Double
    Public Property ReferenceNo As String
    Public Property Remarks As String
    Public Property UserName As String
    Public Property DetailList As List(Of VendorQuotationDetail)
    Public Sub New()
        DetailList = New List(Of VendorQuotationDetail)
    End Sub
End Class
