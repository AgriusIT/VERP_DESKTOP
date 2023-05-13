Public Class UserGroup

    Private _GroupId As Integer
    Public Property GroupId() As Integer
        Get
            Return _GroupId
        End Get
        Set(ByVal value As Integer)
            _GroupId = value
        End Set
    End Property
    Private _GroupType As String
    Public Property GroupType() As String
        Get
            Return _GroupType
        End Get
        Set(ByVal value As String)
            _GroupType = value
        End Set
    End Property

    Private _GroupName As String
    Public Property GroupName() As String
        Get
            Return _GroupName
        End Get
        Set(ByVal value As String)
            _GroupName = value
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
