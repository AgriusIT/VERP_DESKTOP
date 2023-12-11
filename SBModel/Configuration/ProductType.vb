Public Class ProductType

    Private _Name As String
    Private _Comments As String
    Private _ActivityLog As ActivityLog
    Private _ID As Integer

    Public Property ID() As Integer
        Get
            Return Me._ID
        End Get
        Set(ByVal value As Integer)
            Me._ID = value
        End Set
    End Property

    Public Property ActivityLog() As ActivityLog
        Get
            Return Me._ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            Me._ActivityLog = value
        End Set
    End Property

    Public Property Comments() As String
        Get
            Return Me._Comments
        End Get
        Set(ByVal value As String)
            Me._Comments = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return Me._Name
        End Get
        Set(ByVal value As String)
            Me._Name = value
        End Set
    End Property

    Private _TypeCode As String
    Public Property TypeCode() As String
        Get
            Return _TypeCode
        End Get
        Set(ByVal value As String)
            _TypeCode = value
        End Set
    End Property


End Class
