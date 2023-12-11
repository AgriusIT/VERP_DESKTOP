''16-June-2014 TASK:2690 Imran Ali Add Department and Employee Fields On Production Entry
''21-Jul-2014 TASK2753 Imran Ali Employee Field On Production Store
Public Class ProductionMaster
    Private _Production_ID As Integer
    Private _Production_date As DateTime
    Private _Production_no As String
    Private _Production_store As Integer
    Private _CustomerCode As Integer
    Private _Order_No As Integer
    Private _Project As Integer
    Private _Remarks As String
    Private _TotalQty As Double
    Private _TotalAmount As Double
    Private _UserName As String
    Private _FDate As DateTime
    Private _ProductionDetail As List(Of ProductionDetail)
    Private _StockMaster As StockMaster
    Private _ActivityLog As ActivityLog
    Private _IGPNo As String
    Private _Post As Boolean
    Private _IssuedStore As Integer
    Private _RefDocument As String
    Private _RefDispatchNo As String

    Private _PlanId As Integer
    Public Property PlanId() As Integer
        Get
            Return _PlanId
        End Get
        Set(ByVal value As Integer)
            _PlanId = value
        End Set
    End Property


    Public Property ProductionId() As Integer
        Get
            Return _Production_ID
        End Get
        Set(ByVal value As Integer)
            _Production_ID = value
        End Set
    End Property
    Public Property Production_Date() As DateTime
        Get
            Return _Production_date
        End Get
        Set(ByVal value As DateTime)
            _Production_date = value
        End Set
    End Property
    Public Property Production_No() As String
        Get
            Return _Production_no
        End Get
        Set(ByVal value As String)
            _Production_no = value
        End Set
    End Property
    Public Property Production_Store() As Integer
        Get
            Return _Production_store
        End Get
        Set(ByVal value As Integer)
            _Production_store = value
        End Set
    End Property
    Public Property CustomerCode() As Integer
        Get
            Return _CustomerCode
        End Get
        Set(ByVal value As Integer)
            _CustomerCode = value
        End Set
    End Property
    Public Property Order_No() As Integer
        Get
            Return _Order_No
        End Get
        Set(ByVal value As Integer)
            _Order_No = value
        End Set
    End Property
    Public Property Project() As Integer
        Get
            Return _Project
        End Get
        Set(ByVal value As Integer)
            _Project = value
        End Set
    End Property
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property
    Public Property TotalQty() As Double
        Get
            Return _TotalQty
        End Get
        Set(ByVal value As Double)
            _TotalQty = value
        End Set
    End Property
    Public Property TotalAmount() As Double
        Get
            Return _TotalAmount
        End Get
        Set(ByVal value As Double)
            _TotalAmount = value
        End Set
    End Property
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Public Property FDate() As DateTime
        Get
            Return _FDate
        End Get
        Set(ByVal value As DateTime)
            _FDate = value
        End Set
    End Property
    Public Property ProductionDetail() As List(Of ProductionDetail)
        Get
            Return _ProductionDetail
        End Get
        Set(ByVal value As List(Of ProductionDetail))
            _ProductionDetail = value
        End Set
    End Property
    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property
    Public Property StockMaster() As StockMaster
        Get
            Return _StockMaster
        End Get
        Set(ByVal value As StockMaster)
            _StockMaster = value
        End Set
    End Property
    Public Property IGPNo() As String
        Get
            Return _IGPNo
        End Get
        Set(ByVal value As String)
            _IGPNo = value
        End Set
    End Property
    Public Property Post() As Boolean
        Get
            Return _Post
        End Get
        Set(ByVal value As Boolean)
            _Post = value
        End Set
    End Property
    Public Property IssuedStore() As Integer
        Get
            Return _IssuedStore
        End Get
        Set(ByVal value As Integer)
            _IssuedStore = value
        End Set
    End Property
    Public Property RefDocument() As String
        Get
            Return _RefDocument
        End Get
        Set(ByVal value As String)
            _RefDocument = value
        End Set
    End Property
    Public Property RefDispatchNo() As String
        Get
            Return _RefDispatchNo
        End Get
        Set(ByVal value As String)
            _RefDispatchNo = value
        End Set
    End Property
    'Task:2690 Added Property   
    Private _DepartmentId As Integer
    Public Property DepartmentId() As Integer
        Get
            Return _DepartmentId
        End Get
        Set(ByVal value As Integer)
            _DepartmentId = value
        End Set
    End Property
    'End Task:2690
    ''21-Jul-2014 TASK2753 Imran Ali Employee Field On Production Store
    Private _EmployeeID As Integer
    Public Property EmployeeID() As Integer
        Get
            Return _EmployeeID
        End Get
        Set(ByVal value As Integer)
            _EmployeeID = value
        End Set
    End Property
    'End Task:2753

    Private _CGSAccountId As Integer
    Public Property CGSAccountId() As Integer
        Get
            Return _CGSAccountId
        End Get
        Set(ByVal value As Integer)
            _CGSAccountId = value
        End Set
    End Property

    Private _VoucherHead As VouchersMaster
    Public Property VoucherHead() As VouchersMaster
        Get
            Return _VoucherHead
        End Get
        Set(ByVal value As VouchersMaster)
            _VoucherHead = value
        End Set
    End Property

    Private _PlanTicketId As Integer
    Public Property PlanTicketId() As Integer
        Get
            Return _PlanTicketId
        End Get
        Set(ByVal value As Integer)
            _PlanTicketId = value
        End Set
    End Property


End Class
