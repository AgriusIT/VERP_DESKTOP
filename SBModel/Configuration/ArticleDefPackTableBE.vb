Public Class ArticleDefPackTableBE


    Private _ArticlePackId As Integer
    Public Property ArticlePackId() As Integer
        Get
            Return _ArticlePackId
        End Get
        Set(ByVal value As Integer)
            _ArticlePackId = value
        End Set
    End Property


    Private _ArticleMasterId As Integer
    Public Property ArticleMasterId() As Integer
        Get
            Return _ArticleMasterId
        End Get
        Set(ByVal value As Integer)
            _ArticleMasterId = value
        End Set
    End Property


    Private _PackName As String
    Public Property PackName() As String
        Get
            Return _PackName
        End Get
        Set(ByVal value As String)
            _PackName = value
        End Set
    End Property


    Private _PackQty As Double
    Public Property PackQty() As Double
        Get
            Return _PackQty
        End Get
        Set(ByVal value As Double)
            _PackQty = value
        End Set
    End Property


End Class
