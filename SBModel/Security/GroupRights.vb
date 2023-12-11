Public Class GroupRights

    Private _FormId As Integer
    Public Property FormId() As Integer
        Get
            Return _FormId
        End Get
        Set(ByVal value As Integer)
            _FormId = value
        End Set
    End Property

    Private _FormName As String
    Public Property FormName() As String
        Get
            Return _FormName
        End Get
        Set(ByVal value As String)
            _FormName = value
        End Set
    End Property

    Private _FormControlId As Integer
    Public Property FormControlId() As Integer
        Get
            Return _FormControlId
        End Get
        Set(ByVal value As Integer)
            _FormControlId = value
        End Set
    End Property

    Private _FormControlName As String
    Public Property FormControlName() As String
        Get
            Return _FormControlName
        End Get
        Set(ByVal value As String)
            _FormControlName = value
        End Set
    End Property

    Private _Rights As Boolean
    Public Property Rights() As Boolean
        Get
            Return _Rights
        End Get
        Set(ByVal value As Boolean)
            _Rights = value
        End Set
    End Property

    Private _UserId As Integer
    Public Property UserId() As Integer
        Get
            Return _UserId
        End Get
        Set(ByVal value As Integer)
            _UserId = value
        End Set
    End Property
End Class
