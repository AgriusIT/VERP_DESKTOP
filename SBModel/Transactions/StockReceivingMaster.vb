Public Class StockReceivingMaster

    Private _ReceivingId As Integer
    Public Property ReceivingId() As Integer
        Get
            Return _ReceivingId
        End Get
        Set(ByVal value As Integer)
            _ReceivingId = value
        End Set
    End Property

    Private _LocationId As Integer
    Public Property LocationId() As Integer
        Get
            Return _LocationId
        End Get
        Set(ByVal value As Integer)
            _LocationId = value
        End Set
    End Property

    Private _ReceivingNo As String
    Public Property ReceivingNo() As String
        Get
            Return _ReceivingNo
        End Get
        Set(ByVal value As String)
            _ReceivingNo = value
        End Set
    End Property

    Private _ReceivingDate As DateTime
    Public Property ReceivingDate() As DateTime
        Get
            Return _ReceivingDate
        End Get
        Set(ByVal value As DateTime)
            _ReceivingDate = value
        End Set
    End Property

    Private _VendorId As Integer
    Public Property VendorId() As Integer
        Get
            Return _VendorId
        End Get
        Set(ByVal value As Integer)
            _VendorId = value
        End Set
    End Property

    Private _PurchaseOrderID As Integer
    Public Property PurchaseOrderID() As Integer
        Get
            Return _PurchaseOrderID
        End Get
        Set(ByVal value As Integer)
            _PurchaseOrderID = value
        End Set
    End Property

    Private _PartyInvoiceNo As String
    Public Property PartyInvoiceNo() As String
        Get
            Return _PartyInvoiceNo
        End Get
        Set(ByVal value As String)
            _PartyInvoiceNo = value
        End Set
    End Property

    Private _PartySlipNo As String
    Public Property PartySlipNo() As String
        Get
            Return _PartySlipNo
        End Get
        Set(ByVal value As String)
            _PartySlipNo = value
        End Set
    End Property

    Private _ReceivingQty As Double
    Public Property ReceivingQty() As Double
        Get
            Return _ReceivingQty
        End Get
        Set(ByVal value As Double)
            _ReceivingQty = value
        End Set
    End Property

    Private _ReceivingAmount As Double
    Public Property ReceivingAmount() As Double
        Get
            Return _ReceivingAmount
        End Get
        Set(ByVal value As Double)
            _ReceivingAmount = value
        End Set
    End Property

    Private _CashPaid As Integer
    Public Property CashPaid() As Integer
        Get
            Return _CashPaid
        End Get
        Set(ByVal value As Integer)
            _CashPaid = value
        End Set
    End Property

    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

    Private _UserName As String
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property

    Private _IGPNo As String
    Public Property IGPNo() As String
        Get
            Return _IGPNo
        End Get
        Set(ByVal value As String)
            _IGPNo = value
        End Set
    End Property

    Private _DcNo As String
    Public Property DcNo() As String
        Get
            Return _DcNo
        End Get
        Set(ByVal value As String)
            _DcNo = value
        End Set
    End Property

    Private _Post As Boolean
    Public Property Post() As Boolean
        Get
            Return _Post
        End Get
        Set(ByVal value As Boolean)
            _Post = value
        End Set
    End Property

    Private _DcDate As DateTime
    Public Property DcDate() As DateTime
        Get
            Return _DcDate
        End Get
        Set(ByVal value As DateTime)
            _DcDate = value
        End Set
    End Property

    Private _Driver_Name As String
    Public Property Driver_Name() As String
        Get
            Return _Driver_Name
        End Get
        Set(ByVal value As String)
            _Driver_Name = value
        End Set
    End Property

    Private _Vehicle_No As String
    Public Property Vehicle_No() As String
        Get
            Return _Vehicle_No
        End Get
        Set(ByVal value As String)
            _Vehicle_No = value
        End Set
    End Property

    Private _vendor_invoice_no As String
    Public Property vendor_invoice_no() As String
        Get
            Return _vendor_invoice_no
        End Get
        Set(ByVal value As String)
            _vendor_invoice_no = value
        End Set
    End Property

    Private _CostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return _CostCenterId
        End Get
        Set(ByVal value As Integer)
            _CostCenterId = value
        End Set
    End Property

    Private _StockReceivingDetail As List(Of StockReceivingDetail)
    Public Property StockReceivingDetail() As List(Of StockReceivingDetail)
        Get
            Return _StockReceivingDetail
        End Get
        Set(ByVal value As List(Of StockReceivingDetail))
            _StockReceivingDetail = value
        End Set
    End Property

    Private _ActivityLog As ActivityLog
    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property

    Private _StockMaster As StockMaster
    Public Property StockMaster() As StockMaster
        Get
            Return _StockMaster
        End Get
        Set(ByVal value As StockMaster)
            _StockMaster = value
        End Set
    End Property

    Private _StockDetail As List(Of StockDetail)
    Public Property StockDetail() As List(Of StockDetail)
        Get
            Return _StockDetail
        End Get
        Set(ByVal value As List(Of StockDetail))
            _StockDetail = value
        End Set
    End Property




End Class
