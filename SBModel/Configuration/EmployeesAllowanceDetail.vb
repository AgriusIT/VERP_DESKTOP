Public Class EmployeesAllowanceDetail

    Private _AllowanceDetailId As Integer
    Public Property AllowanceDetailId() As Integer
        Get
            Return _AllowanceDetailId
        End Get
        Set(ByVal value As Integer)
            _AllowanceDetailId = value
        End Set
    End Property

    Private _AllowanceTypeId As Integer
    Public Property AllowanceTypeId() As Integer
        Get
            Return _AllowanceTypeId
        End Get
        Set(ByVal value As Integer)
            _AllowanceTypeId = value
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

    Private _AllowanceAmount As Double
    Public Property Allowance_Amount() As Double
        Get
            Return _AllowanceAmount
        End Get
        Set(ByVal value As Double)
            _AllowanceAmount = value
        End Set
    End Property
End Class
