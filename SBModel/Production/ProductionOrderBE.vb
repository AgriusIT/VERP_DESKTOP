Public Class ProductionOrderBE
    Public Property ProductionOrderId As Integer
    Public Property ProductionOrderNo As String
    Public Property TicketNo As String
    Public Property ProductionOrderDate As DateTime
    Public Property BatchNo As String
    Public Property ExpiryDate As DateTime
    Public Property ProductId As Integer
    Public Property FinishGoodId As Integer
    Public Property BatchSize As Double
    Public Property Section As String
    Public Property Remarks As String
    Public Property CGSAccountId As Integer
    Public Property Approved As Boolean
    Public Property TotalQuantity As Double
    Public Property Voucher As VouchersMaster
    Public Property Stock As StockMaster

    'Task 3420 Saad Afzaal Add Dispatch id in Production Order Model  
    Public Property DispatchId As Integer
    'Task 3420 Saad Afzaal Add Production id in Production Order Model  
    Public Property Production_Id As Integer

    Public Property InputList As List(Of ProductionOrderInputMaterialBE)
    Public Property OverHeadList As List(Of ProductionOrderOverHeadsBE)
    Public Property LabourList As List(Of ProductionOrderLabourBE)
    Public Property OutputList As List(Of ProductionOrderOutputMaterialBE)
    Public Property ActivityLog() As ActivityLog
    Sub New()
        InputList = New List(Of ProductionOrderInputMaterialBE)
        OverHeadList = New List(Of ProductionOrderOverHeadsBE)
        LabourList = New List(Of ProductionOrderLabourBE)
        OutputList = New List(Of ProductionOrderOutputMaterialBE)
        ActivityLog = New ActivityLog()
        Voucher = New VouchersMaster()
        Stock = New StockMaster()
    End Sub
End Class
