Public Class reminderBE



    Private _reminderId As Integer
    Public Property reminderId() As Integer
        Get
            Return _reminderId
        End Get
        Set(ByVal value As Integer)
            _reminderId = value
        End Set
    End Property

    Private _Reminder_Date As DateTime
    Public Property Reminder_Date() As DateTime
        Get
            Return _Reminder_Date
        End Get
        Set(ByVal value As DateTime)
            _Reminder_Date = value
        End Set
    End Property


    Private _Reminder_Time As DateTime
    Public Property Reminder_Time() As DateTime
        Get
            Return _Reminder_Time
        End Get
        Set(ByVal value As DateTime)
            _Reminder_Time = value
        End Set
    End Property

    Private _Reminder_Description As String
    Public Property Reminder_Description() As String
        Get
            Return _Reminder_Description
        End Get
        Set(ByVal value As String)
            _Reminder_Description = value
        End Set
    End Property

    Private _Subject As String
    Public Property Subject() As String
        Get
            Return _Subject
        End Get
        Set(ByVal value As String)
            _Subject = value
        End Set
    End Property

    Private _Owner As Boolean
    Private _ReminderDetail As List(Of ReminderDetail)
    Public Property ReminderDetail() As List(Of ReminderDetail)
        Get
            Return _ReminderDetail
        End Get
        Set(ByVal value As List(Of ReminderDetail))
            _ReminderDetail = value
        End Set
    End Property

    Private _User As String
    Public Property User() As String
        Get
            Return _User
        End Get
        Set(ByVal value As String)
            _User = value
        End Set
    End Property

End Class
