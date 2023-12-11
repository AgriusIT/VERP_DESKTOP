Public Class PLVoucher

    Private _voucher_id As Integer
    Public Property voucher_id() As Integer
        Get
            Return _voucher_id
        End Get
        Set(ByVal value As Integer)
            _voucher_id = value
        End Set
    End Property


    Private _voucher_type_id As Integer
    Public Property voucher_type_id() As Integer
        Get
            Return _voucher_type_id
        End Get
        Set(ByVal value As Integer)
            _voucher_type_id = value
        End Set
    End Property

    Private _voucher_code As String
    Public Property voucher_code() As String
        Get
            Return _voucher_code
        End Get
        Set(ByVal value As String)
            _voucher_code = value
        End Set
    End Property

    Private _voucher_no As String
    Public Property voucher_no() As String
        Get
            Return _voucher_no
        End Get
        Set(ByVal value As String)
            _voucher_no = value
        End Set
    End Property

    Private _voucher_date As DateTime
    Public Property voucher_date() As DateTime
        Get
            Return _voucher_date
        End Get
        Set(ByVal value As DateTime)
            _voucher_date = value
        End Set
    End Property

    Private _Post As Boolean
    Public Property post() As Boolean
        Get
            Return _Post
        End Get
        Set(ByVal value As Boolean)
            _Post = value
        End Set
    End Property

    Private _source As String
    Public Property source() As String
        Get
            Return _source
        End Get
        Set(ByVal value As String)
            _source = value
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

    Private _PLDebit_Amount As Double
    Public Property PLDebitAmount() As Double
        Get
            Return _PLDebit_Amount
        End Get
        Set(ByVal value As Double)
            _PLDebit_Amount = value
        End Set
    End Property

    Private _RefDate As DateTime

    Public Property RefDate() As DateTime
        Get
            Return _RefDate
        End Get
        Set(ByVal value As DateTime)
            _RefDate = value
        End Set
    End Property


    Private _PLVoucherDetail As List(Of PLVoucherDetail)
    Public Property PLVoucherDetail() As List(Of PLVoucherDetail)
        Get
            Return _PLVoucherDetail
        End Get
        Set(ByVal value As List(Of PLVoucherDetail))
            _PLVoucherDetail = value
        End Set
    End Property

    Private _YearCloseId As Integer
    Public Property YearCloseId() As Integer
        Get
            Return _YearCloseId
        End Get
        Set(ByVal value As Integer)
            _YearCloseId = value
        End Set
    End Property


End Class
