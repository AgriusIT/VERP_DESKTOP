Public Class UserLocationRightsBE
    Private _ID As Integer
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property
    Private _UserId As Integer
    Public Property UserID() As Integer
        Get
            Return _UserId
        End Get
        Set(ByVal value As Integer)
            _UserId = value
        End Set
    End Property
    Private _Location_ID As Integer
    Public Property Location_ID() As Integer
        Get
            Return _Location_ID
        End Get
        Set(ByVal value As Integer)
            _Location_ID = value
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
