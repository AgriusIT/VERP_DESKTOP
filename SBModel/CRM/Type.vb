Public Class Type
    Private _TypeId As Integer
    Private _TypeName As String
    Private _TypeRemarks As String
    Private _SortOrder As Integer
    Private _Active As Integer
    Private _ActivityLog As ActivityLog

    Property TypeId() As Integer
        Get
            Return _TypeId
        End Get
        Set(ByVal value As Integer)
            _TypeId = value
        End Set
    End Property
    Property TypeName() As String
        Get
            Return _TypeName
        End Get
        Set(ByVal value As String)
            _TypeName = value
        End Set
    End Property
    Property TypeRemarks() As String
        Get
            Return _TypeRemarks
        End Get
        Set(ByVal value As String)
            _TypeRemarks = value
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
    Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property


End Class
