Public Class LPO

    Private _CompayID As Integer
    Private _Name As String
    Private _Code As String
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

    Public Property Code() As String
        Get
            Return Me._Code
        End Get
        Set(ByVal value As String)
            Me._Code = value
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

    Public Property CompanyID() As Integer
        Get
            Return Me._CompayID
        End Get
        Set(ByVal value As Integer)
            Me._CompayID = value
        End Set
    End Property

    Private _LPOCode As String
    Public Property LPOCode() As String
        Get
            Return _LPOCode
        End Get
        Set(ByVal value As String)
            _LPOCode = value
        End Set
    End Property
End Class
