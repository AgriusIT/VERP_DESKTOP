Public Class ComplaintRequestBE
    Public Property ComplaintId As Integer
    Public Property ComplaintNo As String
    Public Property ComplaintDate As DateTime
    Public Property ComplaintReturnDate As DateTime
    Public Property CustomerId As Integer
    Public Property PersonName As String
    Public Property ContactNo As String
    Public Property Remarks As String
    Public Property post As Boolean
    Public Property StockTransId As Integer
    Public Property ActivityLog() As ActivityLog
    Public Property Detail As List(Of ComplaintRequestDetailBE)

End Class

Public Class ComplaintRequestDetailBE
    Public Property ComplaintDetailId As Integer
    Public Property ComplaintId As Integer
    Public Property LocationId As Integer
    Public Property ItemId As Integer
    Public Property Unit As String
    Public Property Price As Double
    Public Property Sz1 As Double
    Public Property Sz7 As Double
    Public Property Qty As Double
    Public Property TotalQty As Double
    Public Property Comments As String
    
End Class
