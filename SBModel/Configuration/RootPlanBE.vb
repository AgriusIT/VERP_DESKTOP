Public Class RootPlanBE

    Private _RootPlanId As Integer
    Public Property RootPlanId() As Integer
        Get
            Return _RootPlanId
        End Get
        Set(ByVal value As Integer)
            _RootPlanId = value
        End Set
    End Property

    Private _RootPlanName As String
    Public Property RootPlanName() As String
        Get
            Return _RootPlanName
        End Get
        Set(ByVal value As String)
            _RootPlanName = value
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

    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

    Private _SortOrder As Integer
    Public Property SortOrder() As Integer
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Integer)
            _SortOrder = value
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
End Class
