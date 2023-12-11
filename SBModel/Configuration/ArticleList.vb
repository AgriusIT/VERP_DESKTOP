Public Class ArticleList

    Private _ArticleId As Integer
    Public Property ArticleId() As Integer
        Get
            Return _ArticleId
        End Get
        Set(ByVal value As Integer)
            _ArticleId = value
        End Set
    End Property

    Private _ArticleCode As String
    Public Property ArticleCode() As String
        Get
            Return _ArticleCode
        End Get
        Set(ByVal value As String)
            _ArticleCode = value
        End Set
    End Property

    Private _ArticleDescription As String
    Public Property ArticleDescription() As String
        Get
            Return _ArticleDescription
        End Get
        Set(ByVal value As String)
            _ArticleDescription = value
        End Set
    End Property

    Private _ArticleSizeName As String
    Public Property ArticleSizeName() As String
        Get
            Return _ArticleSizeName
        End Get
        Set(ByVal value As String)
            _ArticleSizeName = value
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

    Private _PurchasePrice As Double
    Public Property PurchasePrice() As Double
        Get
            Return _PurchasePrice
        End Get
        Set(ByVal value As Double)
            _PurchasePrice = value
        End Set
    End Property

    Private _SellingPrice As Double
    Public Property SellingPrice() As Double
        Get
            Return _SellingPrice
        End Get
        Set(ByVal value As Double)
            _SellingPrice = value
        End Set
    End Property


    Private _MasterId As Integer
    Public Property MasterId() As Integer
        Get
            Return _MasterId
        End Get
        Set(ByVal value As Integer)
            _MasterId = value
        End Set
    End Property
End Class
