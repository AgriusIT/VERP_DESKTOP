'' 16-Jan-2014 TASK:2381            Imran Ali Problem In Invoice Based Payment 
'04-Aug-2015 Task#04082015 added _CompanyName property  by Ahmad Sharif

Public Class InvoicesBasedPaymentMaster
    Private _PaymentID As Integer
    Private _PaymentNo As String
    Private _PaymentDate As Date
    Private _Reference As String
    Private _PaymentAmount As Double
    Private _PaymentMethod As Integer
    Private _PaymentAcountID As Integer
    Private _VendorCode As Integer
    Private _ChequeNo As String
    Private _ChequeDate As String
    Private _UserName As String
    Private _PVNO As String
    Private _ProjectId As Integer
    Private _NetPayment As Integer
    Private _ActivityLog As ActivityLog
    Private _InvoiceBasedPaymentDetail As List(Of InvoicesBasedPaymentDetail)
    Private _VoucherMaster As VouchersMaster
    Private _VoucherDetail As List(Of VouchersDetail)
    Private _Post As Boolean  'Task No 2537 Adding New Property Of Post
    Private _CompanyName As Integer 'Task#04082015 Added by Ahmad Sharif
    Private _DueDate

    Public Property PaymentID() As Integer
        Get
            Return _PaymentID
        End Get
        Set(ByVal value As Integer)
            Me._PaymentID = value
        End Set
    End Property
    Public Property PaymentNo() As String
        Get
            Return _PaymentNo
        End Get
        Set(ByVal value As String)
            Me._PaymentNo = value
        End Set
    End Property
    Public Property PaymentDate() As Date
        Get
            Return _PaymentDate
        End Get
        Set(ByVal value As Date)
            Me._PaymentDate = value
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
    Public Property PaymentAmount() As Double
        Get
            Return _PaymentAmount
        End Get
        Set(ByVal value As Double)
            Me._PaymentAmount = value
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
    Public Property VendorCode() As Integer
        Get
            Return _VendorCode
        End Get
        Set(ByVal value As Integer)
            _VendorCode = value
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
    Public Property InvoiceBasedPaymentDetail() As List(Of InvoicesBasedPaymentDetail)
        Get
            Return Me._InvoiceBasedPaymentDetail
        End Get
        Set(ByVal value As List(Of InvoicesBasedPaymentDetail))
            Me._InvoiceBasedPaymentDetail = value
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
    Public Property PVNo() As String
        Get
            Return _PVNO

        End Get
        Set(ByVal value As String)
            _PVNO = value
        End Set
    End Property
    Public Property NetPayment() As Double
        Get
            Return _NetPayment
        End Get
        Set(ByVal value As Double)
            _NetPayment = value
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

    Private _Posted_UserName As String
    Public Property Posted_UserName() As String
        Get
            Return _Posted_UserName
        End Get
        Set(ByVal value As String)
            _Posted_UserName = value
        End Set
    End Property

    Private _PayeeTitle As String
    Public Property PayeeTitle() As String
        Get
            Return _PayeeTitle
        End Get
        Set(ByVal value As String)
            _PayeeTitle = value
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

    Private _ChequeLayoutIndex As Integer
    Public Property ChequeLayoutIndex() As Integer
        Get
            Return _ChequeLayoutIndex
        End Get
        Set(ByVal value As Integer)
            _ChequeLayoutIndex = value
        End Set
    End Property


    'Task#04082015 added by Ahmad Sharif
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

    Public Property DueDate() As DateTime
        Get
            Return _DueDate
        End Get
        Set(ByVal value As DateTime)
            Me._DueDate = value
        End Set
    End Property


End Class
