Public Class ComplaintReturnMasterBE
    Public Property ComplaintReturnId As Integer
    Public Property ComplaintReturnNo As String
    Public Property ComplaintReturnDate As DateTime
    Public Property ComplaintId As Integer
    Public Property CustomerId As Integer
    Public Property ReceivedBy As String
    Public Property ContactNo As String
    Public Property Remarks As String
    Public Property Post As Boolean
    Public Property StockTransId As Integer
    Public Property ActivityLog() As ActivityLog
    Public Property Detail As List(Of ComplaintReturnDetailBE)
End Class

Public Class ComplaintReturnDetailBE
    Public Property ComplaintReturnDetailId As Integer
    Public Property ComplaintReturnId As Integer
    Public Property LocationId As Integer
    Public Property ItemId As Integer
    Public Property AlternateId As Integer
    Public Property Unit As String
    Public Property Price As Double
    Public Property PurchasePrice As Double
    Public Property Sz1 As Double
    Public Property Sz7 As Double
    Public Property Qty As Double
    Public Property Comments As String
    Public Property Status As String
    Public Property RequestDetailId As Integer

End Class
