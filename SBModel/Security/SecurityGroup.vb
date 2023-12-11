Public Class SecurityGroup

    Private _groupID As Integer
    Private _groupName As String
    Private _groupType As Integer
    Private _groupComments As String
    Private _isSelectedShop As Boolean




    Public Property GroupID() As Integer
        Get
            Return _groupID
        End Get
        Set(ByVal value As Integer)
            _groupID = value
        End Set
    End Property

    Public Property GroupName() As String
        Get
            Return _groupName
        End Get
        Set(ByVal value As String)
            _groupName = value
        End Set
    End Property

    Public Property GroupType() As Integer
        Get
            Return _groupType
        End Get
        Set(ByVal value As Integer)
            _groupType = value
        End Set
    End Property

    Public Property GroupComments() As String
        Get
            Return _groupComments
        End Get
        Set(ByVal value As String)
            _groupComments = value
        End Set
    End Property

    Public Property IsSelectedShop() As Boolean
        Get
            Return _isSelectedShop
        End Get
        Set(ByVal value As Boolean)
            _isSelectedShop = value
        End Set
    End Property


End Class
