Public Class Status
    Private _StatusId As Integer
    Private _StatusName As String
    Private _StatusRemarks As String
    Private _SortOrder As Integer
    Private _Active As Integer
    Private _ActivityLog As ActivityLog
    Property StatusId() As Integer
        Get
            Return _StatusId
        End Get
        Set(ByVal value As Integer)
            _StatusId = value
        End Set
    End Property
    Property StatusName() As String
        Get
            Return _StatusName
        End Get
        Set(ByVal value As String)
            _StatusName = value
        End Set
    End Property
    Property StatusRemarks() As String
        Get
            Return _StatusRemarks
        End Get
        Set(ByVal value As String)
            _StatusRemarks = value
        End Set
    End Property
    Property SortOrder() As Integer
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Integer)
            _SortOrder = value
        End Set
    End Property
    Property Active() As Integer
        Get
            Return _Active
        End Get
        Set(ByVal value As Integer)
            _Active = value
        End Set
    End Property
    Property ActivitLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property

End Class
