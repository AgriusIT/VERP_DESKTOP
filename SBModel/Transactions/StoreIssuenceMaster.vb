Public Class StoreIssuenceMaster
    Private _PlanId As Integer = 0
    Private _DispatchId As Integer
    Public Property DispatchId() As Integer
        Get
            Return _DispatchId
        End Get
        Set(ByVal value As Integer)
            _DispatchId = value
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

    Private _DispatchNo As String
    Public Property DispatchNo() As String
        Get
            Return _DispatchNo
        End Get
        Set(ByVal value As String)
            _DispatchNo = value
        End Set
    End Property

    Private _DispatchDate As DateTime
    Public Property DispatchDate() As DateTime
        Get
            Return _DispatchDate
        End Get
        Set(ByVal value As DateTime)
            _DispatchDate = value
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

    Private _DispatchQty As Double
    Public Property DispatchQty() As Double
        Get
            Return _DispatchQty
        End Get
        Set(ByVal value As Double)
            _DispatchQty = value
        End Set
    End Property

    Private _DispatchAmount As Double
    Public Property DispatchAmount() As Double
        Get
            Return _DispatchAmount
        End Get
        Set(ByVal value As Double)
            _DispatchAmount = value
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
    Private _Post As Boolean
    Public Property Post() As Boolean
        Get
            Return _Post
        End Get
        Set(ByVal value As Boolean)
            _Post = value
        End Set
    End Property

    Private _RefDocument As String
    Public Property RefDocument() As String
        Get
            Return _RefDocument
        End Get
        Set(ByVal value As String)
            _RefDocument = value
        End Set
    End Property
    Public Property PlanId() As Integer
        Get
            Return _PlanId
        End Get
        Set(ByVal value As Integer)
            _PlanId = value
        End Set
    End Property


    Private _StoreIssuenceDetail As List(Of StoreIssuenceDetail)
    Public Property StoreIssuenceDetail() As List(Of StoreIssuenceDetail)
        Get
            Return _StoreIssuenceDetail
        End Get
        Set(ByVal value As List(Of StoreIssuenceDetail))
            _StoreIssuenceDetail = value
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

    Private _CGSAccountId As Integer
    Public Property CGSAccountId() As Integer
        Get
            Return _CGSAccountId
        End Get
        Set(ByVal value As Integer)
            _CGSAccountId = value
        End Set
    End Property


End Class
