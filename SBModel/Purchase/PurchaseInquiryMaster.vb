Public Class PurchaseInquiryMaster
    Public Property PurchaseInquiryId As Integer
    Public Property PurchaseInquiryNo As String
    Public Property PurchaseInquiryDate As DateTime
    Public Property DueDate As DateTime
    Public Property IndentNo As String
    Public Property IndentingDepartment As String
    Public Property OldInquiryNo As String
    Public Property OldInquiryDate As DateTime
    Public Property Remarks As String
    Public Property UserName As String
    Public Property SalesInquiryId As Integer
    Public Property Posted As Boolean
    Public Property Posted_UserName As String
    Public Property PostedDate As DateTime
    Public Property DetailList As List(Of PurchaseInquiryDetail)
    Public Property VendorsList As List(Of PurchaseInquiryVendors)

   
    Public Sub New()
        DetailList = New List(Of PurchaseInquiryDetail)
        VendorsList = New List(Of PurchaseInquiryVendors)
    End Sub
End Class
