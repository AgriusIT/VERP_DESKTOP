Public Class ProductionOrderInputMaterialBE
    Public Property ID As Integer
    Public Property ProductionOrderId As Integer
    Public Property LocationId As Integer
    Public Property ItemId As Integer
    Public Property Unit As String
    Public Property Qty As Double
    Public Property PackQty As Double
    Public Property Rate As Double
    Public Property TotalQty As Double

    'Task 3394 Added Column FinishGoodid in ProductionOrderInputMaterial
    Public Property FinishGoodId As Integer
    Public Property ActivityLog() As ActivityLog

End Class
