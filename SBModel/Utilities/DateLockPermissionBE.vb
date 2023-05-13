Public Class DateLockPermissionBE

    Private _ID As Integer
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Private _DateFromLock As DateTime
    Public Property DateFromLock() As DateTime
        Get
            Return _DateFromLock
        End Get
        Set(ByVal value As DateTime)
            _DateFromLock = value
        End Set
    End Property

    Private _DateToLock As DateTime
    Public Property DateToLock() As DateTime
        Get
            Return _DateToLock
        End Get
        Set(ByVal value As DateTime)
            _DateToLock = value
        End Set
    End Property

    Private _Lock As Boolean
    Public Property Lock() As Boolean
        Get
            Return _Lock
        End Get
        Set(ByVal value As Boolean)
            _Lock = value
        End Set
    End Property

    Private _UserIDs As String
    Public Property UserIDs() As String
        Get
            Return _UserIDs
        End Get
        Set(ByVal value As String)
            _UserIDs = value
        End Set
    End Property

    Private _PermissionUserName As String
    Public Property PermissionUserName() As String
        Get
            Return _PermissionUserName
        End Get
        Set(ByVal value As String)
            _PermissionUserName = value
        End Set
    End Property
End Class
Public Class DateLockPermissionListByUserID

    Private _DateFrom As DateTime
    Public Property DateFrom() As DateTime
        Get
            Return _DateFrom
        End Get
        Set(ByVal value As DateTime)
            _DateFrom = value
        End Set
    End Property

    Private _DateTo As DateTime
    Public Property DateTo() As DateTime
        Get
            Return _DateTo
        End Get
        Set(ByVal value As DateTime)
            _DateTo = value
        End Set
    End Property

    Private _UserID As Integer
    Public Property UserID() As Integer
        Get
            Return _UserID
        End Get
        Set(ByVal value As Integer)
            _UserID = value
        End Set
    End Property

    Private _DocDate As DateTime
    Public Property DocDate() As DateTime
        Get
            Return _DocDate
        End Get
        Set(ByVal value As DateTime)
            _DocDate = value
        End Set
    End Property

End Class
