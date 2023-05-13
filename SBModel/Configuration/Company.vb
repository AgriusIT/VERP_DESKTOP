Public Class Company

    Private _ID As Integer
    Private _Name As String
    Private _ActivityLog As ActivityLog
    Private _Comments As String

    Public Property Comments() As String
        Get
            Return Me._Comments
        End Get
        Set(ByVal value As String)
            Me._Comments = value
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

    Public Property Name() As String
        Get
            Return Me._Name
        End Get
        Set(ByVal value As String)
            Me._Name = value
        End Set
    End Property

    Public Property ID() As Integer
        Get
            Return Me._ID
        End Get
        Set(ByVal value As Integer)
            Me._ID = value
        End Set
    End Property

    Private _CategoryCode As String
    Public Property CategoryCode() As String
        Get
            Return _CategoryCode
        End Get
        Set(ByVal value As String)
            _CategoryCode = value
        End Set
    End Property


End Class
