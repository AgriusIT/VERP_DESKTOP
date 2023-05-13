Public Class OverTimeBE
    Private _OverTimeSchedule_Id As Integer
    Public Property OverTimeSchedule_Id() As Integer
        Get
            Return _OverTimeSchedule_Id
        End Get
        Set(ByVal value As Integer)
            _OverTimeSchedule_Id = value
        End Set
    End Property

    Private _Employee_Id As Integer
    Public Property Employee_Id() As Integer
        Get
            Return _Employee_Id
        End Get
        Set(ByVal value As Integer)
            _Employee_Id = value
        End Set
    End Property

    Private _Start_Date As DateTime
    Public Property Start_Date() As DateTime
        Get
            Return _Start_Date
        End Get
        Set(ByVal value As DateTime)
            _Start_Date = value
        End Set
    End Property

    Private _End_Date As DateTime
    Public Property End_Date() As DateTime
        Get
            Return _End_Date
        End Get
        Set(ByVal value As DateTime)
            _End_Date = value
        End Set
    End Property

    Private _Start_Time As String
    Public Property Start_Time() As String
        Get
            Return _Start_Time
        End Get
        Set(ByVal value As String)
            _Start_Time = value
        End Set
    End Property

    Private _End_Time As String
    Public Property End_Time() As String
        Get
            Return _End_Time
        End Get
        Set(ByVal value As String)
            _End_Time = value
        End Set
    End Property

    Private _Active As Boolean
    Public Property Active() As Boolean
        Get
            Return _Active
        End Get
        Set(ByVal value As Boolean)
            _Active = value
        End Set
    End Property
End Class
