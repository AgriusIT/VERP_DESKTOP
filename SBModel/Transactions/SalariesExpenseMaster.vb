Public Class SalariesExpenseMaster

    Private _SalaryExpId As Integer
    Public Property SalaryExpId() As Integer
        Get
            Return _SalaryExpId
        End Get
        Set(ByVal value As Integer)
            _SalaryExpId = value
        End Set
    End Property

    Private _SalaryExpDate As DateTime
    Public Property SalaryExpDate() As DateTime
        Get
            Return _SalaryExpDate
        End Get
        Set(ByVal value As DateTime)
            _SalaryExpDate = value
        End Set
    End Property

    Private _SalaryExpNo As String
    Public Property SalaryExpNo() As String
        Get
            Return _SalaryExpNo
        End Get
        Set(ByVal value As String)
            _SalaryExpNo = value
        End Set
    End Property

    Private _EmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return _EmployeeId
        End Get
        Set(ByVal value As Integer)
            _EmployeeId = value
        End Set
    End Property

    Private _PaymentMethodId As Integer
    Public Property PaymentMethodId() As Integer
        Get
            Return _PaymentMethodId
        End Get
        Set(ByVal value As Integer)
            _PaymentMethodId = value
        End Set
    End Property

    Private _PayFromId As Integer
    Public Property PayFromId() As Integer
        Get
            Return _PayFromId
        End Get
        Set(ByVal value As Integer)
            _PayFromId = value
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

    Private _Post As Boolean
    Public Property Post() As Boolean
        Get
            Return _Post
        End Get
        Set(ByVal value As Boolean)
            _Post = value
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

    Private _FDate As DateTime
    Public Property FDate() As DateTime
        Get
            Return _FDate
        End Get
        Set(ByVal value As DateTime)
            _FDate = value
        End Set
    End Property

    Private _SalariesExpenseDetail As List(Of SalariesExpenseDetail)
    Public Property SalariesExpenseDetail() As List(Of SalariesExpenseDetail)
        Get
            Return _SalariesExpenseDetail
        End Get
        Set(ByVal value As List(Of SalariesExpenseDetail))
            _SalariesExpenseDetail = value
        End Set
    End Property

    Private _VoucherDetail As List(Of VouchersDetail)
    Public Property VoucherDetail() As List(Of VouchersDetail)
        Get
            Return _VoucherDetail
        End Get
        Set(ByVal value As List(Of VouchersDetail))
            _VoucherDetail = value
        End Set
    End Property

    Private _ChequeNo As String
    Public Property ChequeNo() As String
        Get
            Return _ChequeNo
        End Get
        Set(ByVal value As String)
            _ChequeNo = value
        End Set
    End Property
    Private _ChequeDate As String
    Public Property ChequeDate() As String
        Get
            Return _ChequeDate
        End Get
        Set(ByVal value As String)
            _ChequeDate = value
        End Set
    End Property

    Private _EmpSalaryPayableAccountId As Integer
    Public Property EmpSalaryPayableAccountId() As Integer
        Get
            Return _EmpSalaryPayableAccountId
        End Get
        Set(ByVal value As Integer)
            _EmpSalaryPayableAccountId = value
        End Set
    End Property

    Private _NetSalary As Double
    Public Property NetSalary() As Double
        Get
            Return _NetSalary
        End Get
        Set(ByVal value As Double)
            _NetSalary = value
        End Set
    End Property

    Private _SalaryAccountId As Integer
    Public Property SalaryAccountId() As Integer
        Get
            Return _SalaryAccountId
        End Get
        Set(ByVal value As Integer)
            _SalaryAccountId = value
        End Set
    End Property

    Private _GrossSalary As Double
    Public Property GrossSalary() As Double
        Get
            Return _GrossSalary
        End Get
        Set(ByVal value As Double)
            _GrossSalary = value
        End Set
    End Property





End Class
