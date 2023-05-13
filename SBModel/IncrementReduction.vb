Public Class IncrementReduction
    Private _ID As Integer
    Private _Date As Date
    Private _ArticleID As Integer
    Private _Stock As Double
    Private _PurchasePriceOld As Double
    Private _PurchasePriceNew As Double
    Private _SalePriceOld As Double
    Private _SalePriceNew As Double
    Private _Old_Cost_Price As Double = 0D
    Private _New_Cost_Price As Double = 0D
    Public Property Stock() As Double
        Get
            Return Me._Stock
        End Get
        Set(ByVal value As Double)
            Me._Stock = value
        End Set
    End Property

    Public Property SalePriceNew() As Double
        Get
            Return Me._SalePriceNew
        End Get
        Set(ByVal value As Double)
            Me._SalePriceNew = value
        End Set
    End Property

    Public Property SalePriceOld() As Double
        Get
            Return Me._SalePriceOld
        End Get
        Set(ByVal value As Double)
            Me._SalePriceOld = value
        End Set
    End Property

    Public Property PurchasePriceNew() As Double
        Get
            Return Me._PurchasePriceNew
        End Get
        Set(ByVal value As Double)
            Me._PurchasePriceNew = value
        End Set
    End Property


    Public Property PurchasePriceOld() As Double
        Get
            Return Me._PurchasePriceOld
        End Get
        Set(ByVal value As Double)
            Me._PurchasePriceOld = value
        End Set
    End Property


    Public Property ArticleID() As Integer
        Get
            Return Me._ArticleID
        End Get
        Set(ByVal value As Integer)
            Me._ArticleID = value
        End Set
    End Property

    Public Property IncrementDate() As Date
        Get
            Return Me._Date
        End Get
        Set(ByVal value As Date)
            Me._Date = value
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
    Public Property Old_Cost_Price() As Double
        Get
            Return _Old_Cost_Price
        End Get
        Set(ByVal value As Double)
            _Old_Cost_Price = value
        End Set
    End Property
    Public Property New_Cost_Price() As Double
        Get
            Return _New_Cost_Price
        End Get
        Set(ByVal value As Double)
            _New_Cost_Price = value
        End Set
    End Property

End Class
