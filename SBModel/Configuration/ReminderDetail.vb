Public Class ReminderDetail

    Private _ReminderDetailId As Integer
    Public Property ReminderDetailId() As Integer
        Get
            Return _ReminderDetailId
        End Get
        Set(ByVal value As Integer)
            _ReminderDetailId = value
        End Set
    End Property

    Private _ReminderId As Integer
    Public Property ReminderId() As Integer
        Get
            Return _ReminderId
        End Get
        Set(ByVal value As Integer)
            _ReminderId = value
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

    Private _User_Reminder_Date As DateTime
    Public Property User_Reminder_Date() As DateTime
        Get
            Return _User_Reminder_Date
        End Get
        Set(ByVal value As DateTime)
            _User_Reminder_Date = value
        End Set
    End Property

    Private _User_Reminder_Time As String
    Public Property User_Reminder_Time() As String
        Get
            Return _User_Reminder_Time
        End Get
        Set(ByVal value As String)
            _User_Reminder_Time = value
        End Set
    End Property

    Private _SnoozeDate As DateTime
    Public Property SnoozeDate() As DateTime
        Get
            Return _SnoozeDate
        End Get
        Set(ByVal value As DateTime)
            _SnoozeDate = value
        End Set
    End Property

    Private _SnoozeTime As String
    Public Property SnoozeTime() As String
        Get
            Return _SnoozeTime
        End Get
        Set(ByVal value As String)
            _SnoozeTime = value
        End Set
    End Property

    Private _Dismiss As Boolean
    Public Property Dismiss() As Boolean
        Get
            Return _Dismiss
        End Get
        Set(ByVal value As Boolean)
            _Dismiss = value
        End Set
    End Property

End Class
