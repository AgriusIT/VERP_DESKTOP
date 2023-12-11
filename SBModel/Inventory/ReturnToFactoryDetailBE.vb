Public Class ReturnToFactoryDetailBE
    Public Property ID As Integer
    Public Property ReturnToFactoryId As Integer
    Public Property LocationId As Integer
    Public Property ArticleId As Integer
    Public Property Unit As String
    Public Property Rate As Double
    Public Property Sz1 As Double
    Public Property Sz7 As Double
    Public Property Qty As Double
    Public Property Comments As String
    Public Property ReturnedQty As Double
    Public Property ReturnedTotalQty As Double

    Public Property ActivityLog() As ActivityLog
End Class

