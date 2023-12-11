Public Class BECloseBatch
    Public Property CloseBatchId As Integer
    Public Property PlanId As Integer
    Public Property TicketId As Integer
    Public Property ProductId As Integer
    Public Property BatchNo As String
    Public Property IsClosedBatch As Boolean
    Public Property Production As ProductionMaster
    Public Property Voucher As VouchersMaster

    Public CloseBatchMCDetail As List(Of BECloseBatchMCDetail)
    Public CloseBatchDEDetail As List(Of BECloseBatchDEDetail)
    Public CloseBatchOHDetail As List(Of BECloseBatchOHDetail)
    Public CloseBatchByProductDetail As List(Of BECloseBatchByProductDetail)
    Public CloseBatchFinishGoodsDetail As List(Of BECloseBatchFinishGoodsDetail)
    Public CloseBatchLCDetail As List(Of BECloseBatchLCDetail)
    Public CloseBatchDetail As List(Of BECloseBatchDetail)
    Sub New()
        Voucher = New VouchersMaster()
        Production = New ProductionMaster()
        CloseBatchMCDetail = New List(Of BECloseBatchMCDetail)
        CloseBatchDEDetail = New List(Of BECloseBatchDEDetail)
        CloseBatchOHDetail = New List(Of BECloseBatchOHDetail)
        CloseBatchByProductDetail = New List(Of BECloseBatchByProductDetail)
        CloseBatchFinishGoodsDetail = New List(Of BECloseBatchFinishGoodsDetail)
        CloseBatchLCDetail = New List(Of BECloseBatchLCDetail)
        CloseBatchDetail = New List(Of BECloseBatchDetail)
    End Sub
End Class
