Public Class SalariesExpenseDetail
    Private _SalaryExpDetailId As Integer
    Public Property SalaryExpDetailId() As Integer
        Get
            Return _SalaryExpDetailId
        End Get
        Set(ByVal value As Integer)
            _SalaryExpDetailId = value
        End Set
    End Property

    Private _SalaryExpId As Integer
    Public Property SalaryExpId() As Integer
        Get
            Return _SalaryExpId
        End Get
        Set(ByVal value As Integer)
            _SalaryExpId = value
        End Set
    End Property

    Private _SalaryTypeId As Integer
    Public Property SalaryTypeId() As Integer
        Get
            Return _SalaryTypeId
        End Get
        Set(ByVal value As Integer)
            _SalaryTypeId = value
        End Set
    End Property

    Private _Earning As Double
    Public Property Earning() As Double
        Get
            Return _Earning
        End Get
        Set(ByVal value As Double)
            _Earning = value
        End Set
    End Property

    Private _Deduction As Double
    Public Property Deduction() As Double
        Get
            Return _Deduction
        End Get
        Set(ByVal value As Double)
            _Deduction = value
        End Set
    End Property

    Private _DeductionFlag As Boolean
    Public Property DeductionFlag() As Boolean
        Get
            Return _DeductionFlag
        End Get
        Set(ByVal value As Boolean)
            _DeductionFlag = value
        End Set
    End Property

    Private _AccountId As Integer
    Public Property AccountId() As Integer
        Get
            Return _AccountId
        End Get
        Set(ByVal value As Integer)
            _AccountId = value
        End Set
    End Property

    Private _Advance As Boolean
    Public Property Advance() As Boolean
        Get
            Return _Advance
        End Get
        Set(ByVal value As Boolean)
            _Advance = value
        End Set
    End Property

    Private _SalaryType As String
    Public Property SalaryType() As String
        Get
            Return _SalaryType
        End Get
        Set(ByVal value As String)
            _SalaryType = value
        End Set
    End Property

End Class
