Public Class WarrantyMasterTable
    Public Property WarrantyMasterId As Integer
    Public Property WarrantyNo As String
    Public Property WarrantyDate As DateTime
    Public Property SOId As Integer
    Public Property PlanId As Integer
    Public Property TicketId As Integer
    Public Property FinishGoodStandardId As Integer
    Public Property StockTransId As Integer
    Public Property DispatchId As Integer
    Public Property ActivityLog() As ActivityLog
    Public Property Detail As List(Of WarrantyDetailTable)

End Class

Public Class WarrantyDetailTable
    Public Property WarrantyDetailId As Integer
    Public Property WarrantyMasterId As Integer
    Public Property FinishGoodId As Integer
    Public Property Qty As Double
    Public Property Amount As Double
    Public Property Remarks As String
    
End Class
