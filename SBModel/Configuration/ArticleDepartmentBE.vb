Public Class ArticleDepartmentBE

    Private _ArticleGroupId As Integer
    Public Property ArticleGroupId() As Integer
        Get
            Return _ArticleGroupId
        End Get
        Set(ByVal value As Integer)
            _ArticleGroupId = value
        End Set
    End Property

    Private _ArticleGroupName As String
    Public Property ArticleGroupName() As String
        Get
            Return _ArticleGroupName
        End Get
        Set(ByVal value As String)
            _ArticleGroupName = value
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

    Private _IsDate As DateTime
    Public Property IsDate() As DateTime
        Get
            Return _IsDate
        End Get
        Set(ByVal value As DateTime)
            _IsDate = value
        End Set
    End Property

    Private _SubSubId As Integer
    Public Property SubSubId() As Integer
        Get
            Return _SubSubId
        End Get
        Set(ByVal value As Integer)
            _SubSubId = value
        End Set
    End Property

    Private _SalesItem As Boolean
    Public Property SalesItem() As Boolean
        Get
            Return _SalesItem
        End Get
        Set(ByVal value As Boolean)
            _SalesItem = value
        End Set
    End Property

    Private _ServiceItem As Boolean
    Public Property ServiceItem() As Boolean
        Get
            Return _ServiceItem
        End Get
        Set(ByVal value As Boolean)
            _ServiceItem = value
        End Set
    End Property

    Private _GroupCode As String
    Public Property GroupCode() As String
        Get
            Return _GroupCode
        End Get
        Set(ByVal value As String)
            _GroupCode = value
        End Set
    End Property

    Private _SalesAccountId As Integer
    Public Property SalesAccountId() As Integer
        Get
            Return _SalesAccountId
        End Get
        Set(ByVal value As Integer)
            _SalesAccountId = value
        End Set
    End Property

    Private _CGSAccountId As Integer
    Public Property CGSAccountId() As Integer
        Get
            Return _CGSAccountId
        End Get
        Set(ByVal value As Integer)
            _CGSAccountId = value
        End Set
    End Property
End Class
