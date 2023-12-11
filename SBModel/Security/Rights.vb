Public Class Rights
    Private _RightsId As Integer
    Public Property RightsId() As Integer
        Get
            Return _RightsId
        End Get
        Set(ByVal value As Integer)
            _RightsId = value
        End Set
    End Property

    Private _GroupId As Integer
    Public Property GroupId() As Integer
        Get
            Return _GroupId
        End Get
        Set(ByVal value As Integer)
            _GroupId = value
        End Set
    End Property

    Private _FormId As Integer
    Public Property FormId() As Integer
        Get
            Return _FormId
        End Get
        Set(ByVal value As Integer)
            _FormId = value
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

    Private _Rights As Boolean
    Public Property Rights() As Boolean
        Get
            Return _Rights
        End Get
        Set(ByVal value As Boolean)
            _Rights = value
        End Set
    End Property
End Class
