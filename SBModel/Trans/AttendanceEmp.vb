'Task No 2593 Addng Properties of Newly Added Fields 
Public Class AttendanceEmp
    Private _AttendanceId As Integer
    Private _EmpID As Integer
    Private _AttendanceDate As DateTime
    Private _AttendanceType As String
    Private _AttendanceTime As DateTime
    Private _AttendanceStatus As String
    Private _ActivityLog As ActivityLog
    Private _ShiftId As Integer
    Private _FlexiblityInTime As String 'Task 2593 Add New Property 
    Private _FlexiblityOutTime As String  'Task 2593 Add New Property
    Private _Auto As Boolean 'Task 2593 Add New Property
    Private _SchInTime As String 'Task 2593 Add New Property
    Private _SchOutTime As String 'Task 2593 Add New Property

    Public Property AttendanceId() As Integer
        Get
            Return _AttendanceId
        End Get
        Set(ByVal value As Integer)
            _AttendanceId = value
        End Set
    End Property
    Public Property EmpID() As Integer
        Get
            Return _EmpID
        End Get
        Set(ByVal value As Integer)
            _EmpID = value
        End Set
    End Property
    Public Property AttendanceDate() As DateTime
        Get
            Return _AttendanceDate
        End Get
        Set(ByVal value As DateTime)
            _AttendanceDate = value
        End Set
    End Property
    Public Property AttendanceType() As String
        Get
            Return _AttendanceType
        End Get
        Set(ByVal value As String)
            _AttendanceType = value
        End Set
    End Property
    Public Property AttendanceTime() As DateTime
        Get
            Return _AttendanceTime
        End Get
        Set(ByVal value As DateTime)
            _AttendanceTime = value
        End Set
    End Property
    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property
    Public Property AttendanceStatus() As String
        Get
            Return _AttendanceStatus
        End Get
        Set(ByVal value As String)
            _AttendanceStatus = value
        End Set
    End Property
    Public Property ShiftId() As Integer
        Get
            Return _ShiftId
        End Get
        Set(ByVal value As Integer)
            _ShiftId = value
        End Set
    End Property
    'Task No 2593 Added New Proprty Of Flexiblity In Time 
    Public Property FlexiblityInTime() As String
        Get
            Return _FlexiblityInTime
        End Get
        Set(ByVal value As String)
            _FlexiblityInTime = value
        End Set
    End Property
    'End Task
    'Task No 2593 Added New Proprty Of Flexiblity Out Time 
    Public Property FlexiblityOutTime() As String
        Get
            Return _FlexiblityOutTime
        End Get
        Set(ByVal value As String)
            _FlexiblityOutTime = value
        End Set
    End Property
    'End Task
    'Task No 2593 Added New Proprty Of Auto  
    Public Property Auto() As Boolean
        Get
            Return _Auto
        End Get
        Set(ByVal value As Boolean)
            _Auto = value
        End Set
    End Property
    'End Task
    'Task No 2593 Added New Proprty Of Scholl In Time 
    Public Property SchInTime() As String
        Get
            Return _SchInTime
        End Get
        Set(ByVal value As String)
            _SchInTime = value
        End Set
    End Property
    'End Task
    'Task No 2593 Added New Proprty Of Scholl Out Time 
    Public Property SchOutTime() As String
        Get
            Return _SchOutTime
        End Get
        Set(ByVal value As String)
            _SchOutTime = value
        End Set
    End Property
    'End Task
End Class
