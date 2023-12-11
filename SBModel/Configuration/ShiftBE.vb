'26-APRIL-2014 TASK:2591 BY JUNAID SHEHZAD  New Enhancement In Define Shift

Public Class ShiftBE
    Private _ShiftId As Integer
    Public Property ShiftId() As Integer
        Get
            Return _ShiftId
        End Get
        Set(ByVal value As Integer)
            _ShiftId = value
        End Set
    End Property

    Private _ShiftCode As String
    Public Property ShiftCode() As String
        Get
            Return _ShiftCode
        End Get
        Set(ByVal value As String)
            _ShiftCode = value
        End Set
    End Property

    Private _ShiftName As String
    Public Property ShiftName() As String
        Get
            Return _ShiftName
        End Get
        Set(ByVal value As String)
            _ShiftName = value
        End Set
    End Property

    Private _ShiftComments As String
    Public Property ShiftComments() As String
        Get
            Return _ShiftComments
        End Get
        Set(ByVal value As String)
            _ShiftComments = value
        End Set
    End Property

    Private _ShiftStartDate As DateTime
    Public Property ShiftStartDate() As DateTime
        Get
            Return _ShiftStartDate
        End Get
        Set(ByVal value As DateTime)
            _ShiftStartDate = value
        End Set
    End Property

    Private _ShiftEndDate As DateTime
    Public Property ShiftEndDate() As DateTime
        Get
            Return _ShiftEndDate
        End Get
        Set(ByVal value As DateTime)
            _ShiftEndDate = value
        End Set
    End Property

    Private _ShiftStartTime As DateTime
    Public Property ShiftStartTime() As DateTime
        Get
            Return _ShiftStartTime
        End Get
        Set(ByVal value As DateTime)
            _ShiftStartTime = value
        End Set
    End Property

    Private _ShiftEndTime As DateTime
    Public Property ShiftEndTime() As DateTime
        Get
            Return _ShiftEndTime
        End Get
        Set(ByVal value As DateTime)
            _ShiftEndTime = value
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

    Private _SortOrder As Integer
    Public Property SortOrder() As Integer
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Integer)
            _SortOrder = value
        End Set
    End Property

    'Task: 2591 Flexibility Time Property Added
    Private _FlexInTime As String
    Public Property FlexInTime() As String
        Get
            Return _FlexInTime
        End Get
        Set(ByVal value As String)
            _FlexInTime = value
        End Set
    End Property

    Private _FlexOutTime As String
    Public Property FlexOutTime() As String
        Get
            Return _FlexOutTime
        End Get
        Set(ByVal value As String)
            _FlexOutTime = value
        End Set
    End Property
    'End Task: 2591
    Private _OverTimeRate As Double
    Public Property OverTimeRate() As Double
        Get
            Return _OverTimeRate
        End Get
        Set(ByVal value As Double)
            _OverTimeRate = value
        End Set
    End Property

    Private _BreakStartTime As DateTime
    Public Property BreakStartTime() As DateTime
        Get
            Return _BreakStartTime
        End Get
        Set(ByVal value As DateTime)
            _BreakStartTime = value
        End Set
    End Property

    Private _BreakEndTime As DateTime
    Public Property BreakEndTime() As DateTime
        Get
            Return _BreakEndTime
        End Get
        Set(ByVal value As DateTime)
            _BreakEndTime = value
        End Set
    End Property

    Private _SpecialDayBreak As String
    Public Property SpecialDayBreak() As String
        Get
            Return _SpecialDayBreak
        End Get
        Set(ByVal value As String)
            _SpecialDayBreak = value
        End Set
    End Property

    Private _SpecialDayBreakStartTime As DateTime
    Public Property SpecialDayBreakStartTime() As DateTime
        Get
            Return _SpecialDayBreakStartTime
        End Get
        Set(ByVal value As DateTime)
            _SpecialDayBreakStartTime = value
        End Set
    End Property

    Private _SpecialDayBreakEndTime As DateTime
    Public Property SpecialDayBreakEndTime() As DateTime
        Get
            Return _SpecialDayBreakEndTime
        End Get
        Set(ByVal value As DateTime)
            _SpecialDayBreakEndTime = value
        End Set
    End Property
    Private _NightShift As Boolean
    Public Property NightShift() As Boolean
        Get
            Return _NightShift
        End Get
        Set(ByVal value As Boolean)
            _NightShift = value
        End Set
    End Property

    Private _OverTime_StartTime As DateTime
    Public Property OverTime_StartTime() As DateTime
        Get
            Return _OverTime_StartTime
        End Get
        Set(ByVal value As DateTime)
            _OverTime_StartTime = value
        End Set
    End Property
    Private _MinHalfAbsentHours As Integer
    Public Property MinHalfAbsentHours() As Integer
        Get
            Return _MinHalfAbsentHours
        End Get
        Set(ByVal value As Integer)
            _MinHalfAbsentHours = value
        End Set
    End Property


End Class
