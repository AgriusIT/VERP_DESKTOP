Public Class InvoicesBasedReceiptMaster
    Private _ReceiptID As Integer
    Private _ReceiptNo As String
    Private _ReceiptDate As Date
    Private _Reference As String
    Private _ReceiptAmount As Double
    Private _PaymentMethod As Integer
    Private _PaymentAcountID As Integer
    Private _CustomerCode As Integer
    Private _ChequeNo As String
    Private _ChequeDate As String
    Private _UserName As String
    Private _RVNo As String
    Private _NetReceipt As Double
    Private _ProjectId As Integer
    Private _ActivityLog As ActivityLog
    Private _InvoiceBasedReceiptDetail As List(Of InvoicesBasedReceiptDetail)
    Private _VoucherMaster As VouchersMaster
    Private _VoucherDetail As List(Of VouchersDetail)
    Private _Post As Boolean      'Task No 2537 Adding New Property Of Post
    Private _CompanyName As Integer 'Task#04082015 added new property for CompanyName (Ahmad Sharif)


    Public Property ReceiptID() As Integer
        Get
            Return _ReceiptID
        End Get
        Set(ByVal value As Integer)
            Me._ReceiptID = value
        End Set
    End Property
    Public Property ReceiptNo() As String
        Get
            Return _ReceiptNo
        End Get
        Set(ByVal value As String)
            Me._ReceiptNo = value
        End Set
    End Property
    Public Property ReceiptDate() As Date
        Get
            Return _ReceiptDate
        End Get
        Set(ByVal value As Date)
            Me._ReceiptDate = value
        End Set
    End Property
    Public Property Reference() As String
        Get
            Return _Reference
        End Get
        Set(ByVal value As String)
            Me._Reference = value
        End Set
    End Property
    Public Property ReceiptAmount() As Double
        Get
            Return _ReceiptAmount
        End Get
        Set(ByVal value As Double)
            Me._ReceiptAmount = value
        End Set
    End Property
    Public Property PaymentMethod() As Integer
        Get
            Return _PaymentMethod
        End Get
        Set(ByVal value As Integer)
            Me._PaymentMethod = value
        End Set
    End Property
    Public Property PaymentAccountId() As Integer
        Get
            Return _PaymentAcountID
        End Get
        Set(ByVal value As Integer)
            Me._PaymentAcountID = value
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
    Public Property ChequeNo() As String
        Get
            Return _ChequeNo
        End Get
        Set(ByVal value As String)
            _ChequeNo = value
        End Set
    End Property
    Public Property ChequeDate() As DateTime
        Get
            Return _ChequeDate
        End Get
        Set(ByVal value As DateTime)
            _ChequeDate = value
        End Set
    End Property
    Public Property ActivityLog() As ActivityLog
        Get
            Return Me._ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            Me._ActivityLog = value
        End Set
    End Property
    Public Property InvoiceBasedReceiptDetail() As List(Of InvoicesBasedReceiptDetail)
        Get
            Return Me._InvoiceBasedReceiptDetail
        End Get
        Set(ByVal value As List(Of InvoicesBasedReceiptDetail))
            Me._InvoiceBasedReceiptDetail = value
        End Set
    End Property
    Public Property VoucherMaster() As VouchersMaster
        Get
            Return _VoucherMaster
        End Get
        Set(ByVal value As VouchersMaster)
            _VoucherMaster = value
        End Set
    End Property
    Public Property VoucherDetail() As List(Of VouchersDetail)
        Get
            Return _VoucherDetail
        End Get
        Set(ByVal value As List(Of VouchersDetail))
            _VoucherDetail = value
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
    Public Property RVNo() As String
        Get
            Return _RVNo
        End Get
        Set(ByVal value As String)
            _RVNo = value
        End Set
    End Property
    Public Property NetReceipt() As Double
        Get
            Return _NetReceipt
        End Get
        Set(ByVal value As Double)
            _NetReceipt = value
        End Set
    End Property
    Public Property ProjectId() As Integer
        Get
            Return _ProjectId
        End Get
        Set(ByVal value As Integer)
            _ProjectId = value
        End Set
    End Property
    'Task No 2537 Adding New Property Of Post
    Public Property Post() As Boolean
        Get
            Return _Post
        End Get
        Set(ByVal value As Boolean)
            _Post = value
        End Set
    End Property

    'Task#04082015
    Public Property CompanyName() As Integer
        Get
            Return _CompanyName
        End Get
        Set(ByVal value As Integer)
            _CompanyName = value
        End Set
    End Property
    'End Task#04082015
    Private _UserID As Integer
    Public Property UserID() As Integer
        Get
            Return _UserID
        End Get
        Set(ByVal value As Integer)
            _UserID = value
        End Set
    End Property

End Class
