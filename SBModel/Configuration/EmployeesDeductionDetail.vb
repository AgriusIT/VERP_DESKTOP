Public Class EmployeesDeductionDetail

    Private _DeductionDetailId As Integer
    Public Property DeductionDetailId() As Integer
        Get
            Return _DeductionDetailId
        End Get
        Set(ByVal value As Integer)
            _DeductionDetailId = value
        End Set
    End Property

    Private _DeductionTypeId As Integer
    Public Property DeductionTypeId() As Integer
        Get
            Return _DeductionTypeId
        End Get
        Set(ByVal value As Integer)
            _DeductionTypeId = value
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

    Private _DeductionAmount As Double
    Public Property Deduction_Amount() As Double
        Get
            Return _DeductionAmount
        End Get
        Set(ByVal value As Double)
            _DeductionAmount = value
        End Set
    End Property
End Class
