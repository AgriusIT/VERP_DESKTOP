Public Class EmployeeAccountsBE

    Private _EmpAcDetailId As Integer
    Public Property EmpAcDetailId() As Integer
        Get
            Return _EmpAcDetailId
        End Get
        Set(ByVal value As Integer)
            _EmpAcDetailId = value
        End Set
    End Property


    Private _Employee_Id As Integer
    Public Property Employee_Id() As Integer
        Get
            Return _Employee_Id
        End Get
        Set(ByVal value As Integer)
            _Employee_Id = value
        End Set
    End Property

    Private _Account_Id As Integer
    Public Property Account_Id() As Integer
        Get
            Return _Account_Id
        End Get
        Set(ByVal value As Integer)
            _Account_Id = value
        End Set
    End Property

    Private _Type_Id As Integer
    Public Property Type_Id() As Integer
        Get
            Return _Type_Id
        End Get
        Set(ByVal value As Integer)
            _Type_Id = value
        End Set
    End Property

    Private _FlgReceivable As Boolean
    Public Property FlgReceivable() As Boolean
        Get
            Return _FlgReceivable
        End Get
        Set(ByVal value As Boolean)
            _FlgReceivable = value
        End Set
    End Property

    Private _Active As Boolean
    Public Property Active() As Boolean
        Get
            Return _Active
        End Get
        Set(ByVal value As Boolean)
            _Active = value
        End Set
    End Property

    Private _Sort_Order As Integer
    Public Property Sort_Order() As Integer
        Get
            Return _Sort_Order
        End Get
        Set(ByVal value As Integer)
            _Sort_Order = value
        End Set
    End Property

    Private _Amount As Double
    Public Property Amount() As Double
        Get
            Return _Amount
        End Get
        Set(ByVal value As Double)
            _Amount = value
        End Set
    End Property

End Class
