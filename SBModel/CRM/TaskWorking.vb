Public Class TaskWorking
    Private _TaskWorkId As Integer
    Private _TaskWorkDate As DateTime
    Private _EmployeeId As Integer
    Private _TaskId As Integer
    Private _Hours As Double
    Private _Rate As Double
    Private _UserName As String
    Private _FDate As DateTime
    Public Property TaskWorkId() As Integer
        Get
            Return _TaskWorkId
        End Get
        Set(ByVal value As Integer)
            _TaskWorkId = value
        End Set
    End Property
    Public Property TaskWorkDate() As DateTime
        Get
            Return _TaskWorkDate
        End Get
        Set(ByVal value As DateTime)
            _TaskWorkDate = value
        End Set
    End Property
    Public Property EmployeeId() As Integer
        Get
            Return _EmployeeId
        End Get
        Set(ByVal value As Integer)
            _EmployeeId = value
        End Set
    End Property
    Public Property TaskId() As Integer
        Get
            Return _TaskId
        End Get
        Set(ByVal value As Integer)
            _TaskId = value
        End Set
    End Property
    Public Property TaskHours() As Double
        Get
            Return _Hours
        End Get
        Set(ByVal value As Double)
            _Hours = value
        End Set
    End Property
    Public Property TaskRate() As Double
        Get
            Return _Rate
        End Get
        Set(ByVal value As Double)
            _Rate = value
        End Set
    End Property
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Public Property FeedingDate() As DateTime
        Get
            Return _FDate
        End Get
        Set(ByVal value As DateTime)
            _FDate = value
        End Set
    End Property
End Class
