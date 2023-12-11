Public Class SalesInquiryRights
    Public Property SalesInquiryRightsId As Integer
    Public Property GroupId As Integer
    Public Property SalesInquiryDetailId As Integer
    Public Property SalesInquiryId As Integer
    Public Property Rights As Boolean
    Public Property UserName As String
    Public Property Status As String
    Public Property Qty As Double
    Public Property PurchasedQty As Double
    Public Property UserId As Integer
    Public Property VendorId As Integer
    Public Property IsPurchaseInquiry As Boolean
    Public Property RequirementDescription As String
    Public Property Groups As List(Of SalesInquiryRightsGroups)
    Public Property Users As List(Of SalesInquiryRightsUsers)
    Public Sub New()
        Groups = New List(Of SalesInquiryRightsGroups)
        Users = New List(Of SalesInquiryRightsUsers)
    End Sub
    Property Notification As AgriusNotifications

End Class
