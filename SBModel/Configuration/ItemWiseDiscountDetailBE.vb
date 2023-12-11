Public Class ItemWiseDiscountDetailBE
    Public Property ID As Integer
    Public Property ItemWiseDiscountId As Integer
    Public Property ArticleId As Integer
    Public Property IsDeleted As Boolean = False

    Public Property ActivityLog() As ActivityLog

End Class
