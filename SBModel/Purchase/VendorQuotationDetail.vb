Public Class VendorQuotationDetail
    Public Property VendorQuotationDetailId As Integer
    Public Property VendorQuotationId As Integer
    Public Property SerialNo As String
    Public Property RequirementDescription As String
    Public Property ArticleId As Integer
    Public Property UnitId As Integer
    Public Property ItemTypeId As Integer
    Public Property CategoryId As Integer
    Public Property SubCategoryId As Integer
    Public Property OriginId As Integer
    Public Property Qty As Double
    Public Property QuotedTerms As String
    Public Property ValidityOfQuotation As String
    Public Property DeliveryPeriod As String
    Public Property Warranty As String
    Public Property ApproxGrossWeight As String
    Public Property HSCode As String
    Public Property ExWorks As String
    Public Property DeliveryPort As String
    Public Property GenuineOrReplacement As String
    Public Property LiteratureOrDatasheet As String
    Public Property NewOrRefurbish As String
    Public Property Price As Decimal
    Public Property DiscountPer As Double
    Public Property SalesTaxPer As Double
    Public Property AddTaxPer As Double
    Public Property IncTaxPer As Double
    Public Property CDPer As Double
    Public Property NetPrice As Double
    Public Property OtherCharges As Decimal
    Public Property PurchaseInquiryDetailId As Integer
    Public Property ReferenceNo As String
    Public Property Comments As String
    Public Property HeadArticleId As Integer
    Public Property BaseCurrencyId As Integer
    Public Property BaseCurrencyRate As Integer
    Public Property CurrencyId As Integer
    Public Property CurrencyRate As Integer
    Public Property CurrencySymbol As String
    Public Property IsAlternate As Boolean
    Public Property ChargesDetail As List(Of VendorQuotationDetailCharges)
    Public Sub New()
        ChargesDetail = New List(Of VendorQuotationDetailCharges)
    End Sub
End Class
