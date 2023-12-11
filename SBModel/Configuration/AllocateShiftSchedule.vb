Public Class AllocateShiftSchedule

    Private _ShiftScheduleId As Integer
    Public Property ShiftScheduleId() As Integer
        Get
            Return _ShiftScheduleId
        End Get
        Set(ByVal value As Integer)
            _ShiftScheduleId = value
        End Set
    End Property

    Private _ShiftId As Integer
    Public Property ShiftId() As Integer
        Get
            Return _ShiftId
        End Get
        Set(ByVal value As Integer)
            _ShiftId = value
        End Set
    End Property

    Private _ShiftGroupId As Integer
    Public Property ShiftGroupId() As Integer
        Get
            Return _ShiftGroupId
        End Get
        Set(ByVal value As Integer)
            _ShiftGroupId = value
        End Set
    End Property

    Private _LoginUserId As Integer
    Public Property LoginUserId() As Integer
        Get
            Return _LoginUserId
        End Get
        Set(ByVal value As Integer)
            _LoginUserId = value
        End Set
    End Property

    Private _DateTimeLog As DateTime
    Public Property DateTimeLog() As DateTime
        Get
            Return _DateTimeLog
        End Get
        Set(ByVal value As DateTime)
            _DateTimeLog = value
        End Set
    End Property
End Class
