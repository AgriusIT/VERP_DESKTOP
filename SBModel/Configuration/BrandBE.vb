Public Class BrandBE
    Private _ArticleBrandId As Integer
    Public Property ArticleBrandId() As Integer
        Get
            Return _ArticleBrandId
        End Get
        Set(ByVal value As Integer)
            _ArticleBrandId = value
        End Set
    End Property
    Private _ArticleBrandName As String
    Public Property ArticleBrandName() As String
        Get
            Return _ArticleBrandName
        End Get
        Set(ByVal value As String)
            _ArticleBrandName = value
        End Set
    End Property
    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
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
    Private _ActivityLog As ActivityLog
    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property


End Class
