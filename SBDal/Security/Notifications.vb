Public Class NotificationActivity
    Private _NotificationActivityId As Integer
    Public Property NotificationActivityId() As Integer
        Get
            Return _NotificationActivityId
        End Get
        Set(ByVal value As Integer)
            _NotificationActivityId = value
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
    Private _NotificationActivityName As String
    Public Property NotificationActivityName() As String
        Get
            Return _NotificationActivityName
        End Get
        Set(ByVal value As String)
            _NotificationActivityName = value
        End Set
    End Property

    Private _NotificationActivityConfigList As List(Of NotificationActivityConfig)
    Public Property NotificationActivityConfigList() As List(Of NotificationActivityConfig)
        Get
            Return _NotificationActivityConfigList
        End Get
        Set(ByVal value As List(Of NotificationActivityConfig))
            _NotificationActivityConfigList = value
        End Set
    End Property

End Class
Public Class NotificationActivityConfig
    Private _NotificationActivityDetailId As Integer
    Public Property NotificationActivityDetailId() As Integer
        Get
            Return _NotificationActivityDetailId
        End Get
        Set(ByVal value As Integer)
            _NotificationActivityDetailId = value
        End Set
    End Property

    Private _NotificationActivityId As Integer
    Public Property NotificationActivityId() As Integer
        Get
            Return _NotificationActivityId
        End Get
        Set(ByVal value As Integer)
            _NotificationActivityId = value
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
    Private _GroupId As Integer
    Public Property GroupId() As Integer
        Get
            Return _GroupId
        End Get
        Set(ByVal value As Integer)
            _GroupId = value
        End Set
    End Property
    Private _SMS As Boolean
    Public Property SMS() As Boolean
        Get
            Return _SMS
        End Get
        Set(ByVal value As Boolean)
            _SMS = value
        End Set
    End Property
    Private _Email As Boolean
    Public Property Email() As Boolean
        Get
            Return _Email
        End Get
        Set(ByVal value As Boolean)
            _Email = value
        End Set
    End Property
    Private _Notifications As Boolean
    Public Property Notifications() As Boolean
        Get
            Return _Notifications
        End Get
        Set(ByVal value As Boolean)
            _Notifications = value
        End Set
    End Property
   

End Class