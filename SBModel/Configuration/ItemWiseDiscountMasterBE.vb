Public Class ItemWiseDiscountMasterBE
    Public Property ID As Integer
    Public Property FromDate As DateTime
    Public Property ToDate As DateTime
    Public Property VendorId As Integer
    Public Property CategoryId As Integer
    Public Property DiscountType As Integer
    Public Property Discount As Double
    Public Property RepeatingNextYear As Boolean
    Public Property Detail As List(Of ItemWiseDiscountDetailBE)
    Public Property ActivityLog() As ActivityLog
    Public Sub New()
        Detail = New List(Of ItemWiseDiscountDetailBE)
        ActivityLog = New ActivityLog()
    End Sub
End Class
