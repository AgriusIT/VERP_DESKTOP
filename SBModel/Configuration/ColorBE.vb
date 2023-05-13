Public Class ColorBE


    Private _ArticleColorId As Integer
    Public Property ArticleColorId() As Integer
        Get
            Return _ArticleColorId
        End Get
        Set(ByVal value As Integer)
            _ArticleColorId = value
        End Set
    End Property


    Private _ArticleColorName As String
    Public Property ArticleColorName() As String
        Get
            Return _ArticleColorName
        End Get
        Set(ByVal value As String)
            _ArticleColorName = value
        End Set
    End Property


    Private _Comments As String
    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
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


    Private _IsDate As Date
    Public Property IsDate() As Date
        Get
            Return _IsDate
        End Get
        Set(ByVal value As Date)
            _IsDate = value
        End Set
    End Property


    Private _ColorCode As String
    Public Property ColorCode() As String
        Get
            Return _ColorCode
        End Get
        Set(ByVal value As String)
            _ColorCode = value
        End Set
    End Property
    Private _ActivityLog As ActivityLog
    Public Property ActivityLog() As ActivityLog
        Get
            Return Me._ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            Me._ActivityLog = value
        End Set
    End Property

End Class
