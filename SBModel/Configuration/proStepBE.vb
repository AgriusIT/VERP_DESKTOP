Public Class proStepBE

    Private _ProdStep_Id As Integer
    Public Property ProdStep_Id() As Integer
        Get
            Return _ProdStep_Id
        End Get
        Set(ByVal value As Integer)
            _ProdStep_Id = value
        End Set
    End Property

    Private _Prod_Step As String
    Public Property Prod_Step() As String
        Get
            Return _Prod_Step
        End Get
        Set(ByVal value As String)
            _Prod_Step = value
        End Set
    End Property


    Private _Prod_Less As Boolean
    Public Property Prod_Less() As Boolean
        Get
            Return _Prod_Less
        End Get
        Set(ByVal value As Boolean)
            _Prod_Less = value
        End Set
    End Property

    Private _Sort_order As Integer
    Public Property Sort_order() As Integer
        Get
            Return _Sort_order
        End Get
        Set(ByVal value As Integer)
            _Sort_order = value
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
    Private _QCVerificationRequired As Boolean
    Public Property QCVerificationRequired() As Boolean
        Get
            Return _QCVerificationRequired
        End Get
        Set(ByVal value As Boolean)
            _QCVerificationRequired = value
        End Set
    End Property

End Class
