Public Class AgriusNotifications
    Private _NotificationId As Integer
    Public Property NotificationId() As Integer
        Get
            Return _NotificationId
        End Get
        Set(ByVal value As Integer)
            _NotificationId = value
        End Set
    End Property

    Private _NotificationDate As DateTime
    Public Property NotificationDate() As DateTime
        Get
            Return _NotificationDate
        End Get
        Set(ByVal value As DateTime)
            _NotificationDate = value
        End Set
    End Property


    Private _NotificationTitle As String
    Public Property NotificationTitle() As String
        Get
            Return _NotificationTitle
        End Get
        Set(ByVal value As String)
            _NotificationTitle = value
        End Set
    End Property

    Private _NotificationDescription As String
    Public Property NotificationDescription() As String
        Get
            Return _NotificationDescription
        End Get
        Set(ByVal value As String)
            _NotificationDescription = value
        End Set
    End Property

    Private _SourceApplication As String
    Public Property SourceApplication() As String
        Get
            Return _SourceApplication
        End Get
        Set(ByVal value As String)
            _SourceApplication = value
        End Set
    End Property

    Private _ApplicationReference As String
    Public Property ApplicationReference() As String
        Get
            Return _ApplicationReference
        End Get
        Set(ByVal value As String)
            _ApplicationReference = value
        End Set
    End Property

    Private _ExpireOn As DateTime
    Public Property ExpireOn() As DateTime
        Get
            Return _ExpireOn
        End Get
        Set(ByVal value As DateTime)
            _ExpireOn = value
        End Set
    End Property

    Private NotificationDetails As List(Of NotificationDetail)
    Public Property NotificationDetils() As List(Of NotificationDetail)
        Get
            Return NotificationDetails
        End Get
        Set(ByVal value As List(Of NotificationDetail))
            NotificationDetails = value
        End Set
    End Property
    Sub New()

        NotificationDetails = New List(Of NotificationDetail)
    End Sub

End Class
Public Class NotificationDetail
    Sub New()

    End Sub
    Sub New(User As SecurityUser)
        NotificationUser = User
    End Sub

    Property NotificationDetailId As Integer
    Property NotificationUser As SecurityUser = New SecurityUser()
    Property ReadStatus As Boolean = False
    Property ClearStatus As Boolean = False
    Public Property GroupId As Integer = 0

End Class
