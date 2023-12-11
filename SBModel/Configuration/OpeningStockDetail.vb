Public Class OpeningStockDetail

    Private _LocationId As Integer
    Public Property LocationId() As Integer
        Get
            Return _LocationId
        End Get
        Set(ByVal value As Integer)
            _LocationId = value
        End Set
    End Property

    Private _ArticleId As Integer
    Public Property ArticleId() As Integer
        Get
            Return _ArticleId
        End Get
        Set(ByVal value As Integer)
            _ArticleId = value
        End Set
    End Property

    Private _Qty As Double
    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal value As Double)
            _Qty = value
        End Set
    End Property

    Private _Price As Double
    Public Property Price() As Double
        Get
            Return _Price
        End Get
        Set(ByVal value As Double)
            _Price = value
        End Set
    End Property

    Private _ArticleSize As String
    Public Property ArticleSize() As String
        Get
            Return _ArticleSize
        End Get
        Set(ByVal value As String)
            _ArticleSize = value
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
