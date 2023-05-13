'2015-06-22 Task#2015060024 Add Time in and Time Out Ali Ansari 
'2015-08-04 Task#201500801 Add User Id in Table Ali Ansari
Public Class Task
    Private _TaskId As Integer
    Private _TaskNo As String
    Private _TaskName As String
    Private _TaskRemarks As String
    Private _TaskProject As Integer
    Private _TaskType As Integer
    Private _TaskCustomer As Integer
    Private _TaskUser As Integer
    Private _TaskStatus As Integer
    Private _Active As Integer
    Private _ActivityLog As ActivityLog
    Private _TaskDate As DateTime
    Private _TaskHours As Double
    Private _TaskRate As Double
    Private _CustomerId As Integer
    Private _ClosingDate As DateTime
    Private _TimeIn As DateTime
    Private _TimeOut As DateTime
    Private _UserId As Integer 'Task20150801 
    Private _AssignedTo As Integer
    Private _Completed As Integer
    Private _Ref_No As String
    Private _FormName As String
    Private _CreatedBy As String
    Private _LastUpdatedBy As String
    Private _CreatedByID As Integer
    Private _ContactPersonID As Integer
    Private _CustomEndDate As DateTime
   

   

    Public Property CreatedByID() As Integer
        Get
            Return _CreatedByID
        End Get
        Set(ByVal value As Integer)
            _CreatedByID = value
        End Set
    End Property

    

    Public Property FormName() As String
        Get
            Return _FormName
        End Get
        Set(ByVal value As String)
            _FormName = value
        End Set
    End Property

    Public Property Ref_No() As String
        Get
            Return _Ref_No
        End Get
        Set(ByVal value As String)
            _Ref_No = value
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
    Public Property TaskNo() As String
        Get
            Return _TaskNo
        End Get
        Set(ByVal value As String)
            _TaskNo = value
        End Set
    End Property
    Public Property TaskName() As String
        Get
            Return _TaskName
        End Get
        Set(ByVal value As String)
            _TaskName = value
        End Set
    End Property
    Public Property TaskRemarks() As String
        Get
            Return _TaskRemarks
        End Get
        Set(ByVal value As String)
            _TaskRemarks = value
        End Set
    End Property
    Public Property TaskProject() As Integer
        Get
            Return _TaskProject
        End Get
        Set(ByVal value As Integer)
            _TaskProject = value
        End Set
    End Property
    Public Property TaskType() As Integer
        Get
            Return _TaskType
        End Get
        Set(ByVal value As Integer)
            _TaskType = value
        End Set
    End Property
    Public Property TaskCustomer() As Integer
        Get
            Return _TaskCustomer
        End Get
        Set(ByVal value As Integer)
            _TaskCustomer = value
        End Set
    End Property
    Public Property TaskUser() As Integer
        Get
            Return _TaskUser
        End Get
        Set(ByVal value As Integer)
            _TaskUser = value
        End Set
    End Property
    Public Property TaskStatus() As Integer
        Get
            Return _TaskStatus
        End Get
        Set(ByVal value As Integer)
            _TaskStatus = value
        End Set
    End Property
    Public Property Active() As Integer
        Get
            Return _Active
        End Get
        Set(ByVal value As Integer)
            _Active = value
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
    Public Property TaskDate() As DateTime
        Get
            Return _TaskDate
        End Get
        Set(ByVal value As DateTime)
            _TaskDate = value
        End Set
    End Property
    Public Property TaskHours() As Double
        Get
            Return _TaskHours
        End Get
        Set(ByVal value As Double)
            _TaskHours = value
        End Set
    End Property
    Public Property TaskRate() As Double
        Get
            Return _TaskRate
        End Get
        Set(ByVal value As Double)
            _TaskRate = value
        End Set
    End Property
    Public Property CustomerId() As Integer
        Get
            Return _CustomerId
        End Get
        Set(ByVal value As Integer)
            _CustomerId = value
        End Set
    End Property
    Public Property ClosingDate() As DateTime
        Get
            Return _ClosingDate
        End Get
        Set(ByVal value As DateTime)
            _ClosingDate = value
        End Set
    End Property

    Private _Prefix As String
    Public Property Prefix() As String
        Get
            Return _Prefix
        End Get
        Set(ByVal value As String)
            _Prefix = value
        End Set
    End Property
    'Altered Against Task#2015060024 Add Time in and Time Out Ali Ansari 

    Public Property TimeIn() As DateTime
        Get
            Return _TimeIn
        End Get
        Set(ByVal value As DateTime)
            _TimeIn = value
        End Set
    End Property


    Public Property TimeOut() As DateTime
        Get
            Return _TimeOut
        End Get
        Set(ByVal value As DateTime)
            _TimeOut = value
        End Set
    End Property
    'Altered Against Task#2015060024 Add Time in and Time Out Ali Ansari 
    'Altered Against Task#201500801 Add User Id in Table Ali Ansari
    Public Property UserId() As Integer
        Get
            Return _UserID
        End Get
        Set(ByVal value As Integer)
            _UserId = value
        End Set
    End Property
    Public Property AssignedTo() As Integer
        Get
            Return _AssignedTo
        End Get
        Set(ByVal value As Integer)
            _AssignedTo = value
        End Set
    End Property
    Public Property Completed() As Integer
        Get
            Return _Completed
        End Get
        Set(ByVal value As Integer)
            _Completed = value
        End Set
    End Property
    Public Property CreatedBy() As String
        Get
            Return _CreatedBy
        End Get
        Set(ByVal value As String)
            _CreatedBy = value
        End Set
    End Property

    Public Property LastUpdatedBy() As String
        Get
            Return _LastUpdatedBy
        End Get
        Set(ByVal value As String)
            _LastUpdatedBy = value
        End Set
    End Property
    Public Property ContactPersonID() As Integer
        Get
            Return _ContactPersonID
        End Get
        Set(ByVal value As Integer)
            _ContactPersonID = value
        End Set
    End Property
    Public Property CustomEndDate() As DateTime
        Get
            Return _CustomEndDate
        End Get
        Set(ByVal value As DateTime)
            _CustomEndDate = value
        End Set
    End Property

    'Altered Against Task#201500801 Add User Id in Table Ali Ansari
End Class
