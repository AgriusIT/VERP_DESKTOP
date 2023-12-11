Public Class ArticleDetail
    Private _ArticleId As Integer
    Private _ArticleCode As String
    Private _ArticleDescription As String
    Private _ArticleGenderID As Integer
    Private _ArticleUnitID As Integer
    Private _ArticleTypeID As Integer
    Private _ArticleLPOID As Integer
    Private _ArticleGroupID As Integer
    Private _ArticleTaxID As Integer ''TFS1799
    Private _ArticleBARCode As String ''TFS3762
    Private _ArticleBARCodeDisable As Boolean  ''TFS3763
    Private _PurchasePrice As Double
    Private _SalePrice As Double
    Private _PackQty As Double
    Private _StockLevel As Double
    Private _StockLevelOpt As Double
    Private _StockLevelMax As Double
    Private _Active As Boolean
    Private _SortOrder As Integer
    Private _IsDate As Date
    Private _AccountID As Integer
    Private _SizeRangeID As Integer
    Private _ArticleColorID As Integer
    Private _ActivityLog As ActivityLog
    Private _ServiceItem As Boolean
    Private _TradePrice As Double
    Private _Freight As Double
    Private _MarketReturns As Double
    Private _GST_Applicable As Boolean
    Private _FlatRate_Applicable As Boolean
    Private _FlatRate As Double
    Private _ItemWeight As Double
    Private _LargestPackQty As Double
    Private _CostPrice As Double = 0D
    Private _ArticleCategoryId As Integer = 0I
    Private _ArticleStatusID As Integer
    Private _ApplyAdjustmentFuelExp As Boolean
    Private _MultiDimentionalItem As Boolean ''TFS1772
    Private _LogicalItem As Boolean ''TFS1957

    Public Property ArticleStatusID() As Integer
        Get
            Return _ArticleStatusID
        End Get
        Set(ByVal value As Integer)
            _ArticleStatusID = value
        End Set
    End Property

    ''Start TFS1799
    Public Property ArticleTaxID() As Integer
        Get
            Return Me._ArticleTaxID
        End Get
        Set(ByVal value As Integer)
            Me._ArticleTaxID = value
        End Set
    End Property
    ''End TFS1799

    Public Property ActivityLog() As ActivityLog
        Get
            Return Me._ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            Me._ActivityLog = value
        End Set
    End Property


    Public Property ArticleColorID() As Integer
        Get
            Return Me._ArticleColorID
        End Get
        Set(ByVal value As Integer)
            Me._ArticleColorID = value
        End Set
    End Property

    Public Property SizeRangeID() As Integer
        Get
            Return Me._SizeRangeID
        End Get
        Set(ByVal value As Integer)
            Me._SizeRangeID = value
        End Set
    End Property


    Public Property AccountID() As Integer
        Get
            Return Me._AccountID
        End Get
        Set(ByVal value As Integer)
            Me._AccountID = value
        End Set
    End Property

    Public Property IsDate() As Date
        Get
            Return Me._IsDate
        End Get
        Set(ByVal value As Date)
            Me._IsDate = value
        End Set
    End Property

    Public Property SortOrder() As Integer
        Get
            Return Me._SortOrder
        End Get
        Set(ByVal value As Integer)
            Me._SortOrder = value
        End Set
    End Property

    Public Property Active() As Boolean
        Get
            Return Me._Active
        End Get
        Set(ByVal value As Boolean)
            Me._Active = value
        End Set
    End Property

    Public Property StockLevelMax() As Double
        Get
            Return Me._StockLevelMax
        End Get
        Set(ByVal value As Double)
            Me._StockLevelMax = value
        End Set
    End Property

    Public Property StockLevelOpt() As Double
        Get
            Return Me._StockLevelOpt
        End Get
        Set(ByVal value As Double)
            Me._StockLevelOpt = value
        End Set
    End Property

    Public Property StockLevel() As Double
        Get
            Return Me._StockLevel
        End Get
        Set(ByVal value As Double)
            Me._StockLevel = value
        End Set
    End Property

    Public Property PackQty() As Double
        Get
            Return Me._PackQty
        End Get
        Set(ByVal value As Double)
            Me._PackQty = value
        End Set
    End Property

    Public Property SalePrice() As Double
        Get
            Return Me._SalePrice
        End Get
        Set(ByVal value As Double)
            Me._SalePrice = value
        End Set
    End Property

    Public Property PurchasePrice() As Double
        Get
            Return Me._PurchasePrice
        End Get
        Set(ByVal value As Double)
            Me._PurchasePrice = value
        End Set
    End Property


    Public Property ArticleGroupID() As Integer
        Get
            Return Me._ArticleGroupID
        End Get
        Set(ByVal value As Integer)
            Me._ArticleGroupID = value
        End Set
    End Property

    Public Property ArticleLPOID() As Integer
        Get
            Return Me._ArticleLPOID
        End Get
        Set(ByVal value As Integer)
            Me._ArticleLPOID = value
        End Set
    End Property

    Public Property ArticleTypeID() As Integer
        Get
            Return Me._ArticleTypeID
        End Get
        Set(ByVal value As Integer)
            Me._ArticleTypeID = value
        End Set
    End Property

    Public Property ArticleGenderID() As Integer
        Get
            Return Me._ArticleGenderID
        End Get
        Set(ByVal value As Integer)
            Me._ArticleGenderID = value
        End Set
    End Property

    Public Property ArticleUnitID() As Integer
        Get
            Return Me._ArticleUnitID
        End Get
        Set(ByVal value As Integer)
            Me._ArticleUnitID = value
        End Set
    End Property

    Public Property ArticleDescription() As String
        Get
            Return Me._ArticleDescription
        End Get
        Set(ByVal value As String)
            Me._ArticleDescription = value
        End Set
    End Property

    Public Property ArticleCode() As String
        Get
            Return Me._ArticleCode
        End Get
        Set(ByVal value As String)
            Me._ArticleCode = value
        End Set
    End Property


    Public Property ArticleID() As Integer
        Get
            Return Me._ArticleId
        End Get
        Set(ByVal value As Integer)
            Me._ArticleId = value
        End Set
    End Property
    Public Property ServiceItem() As Boolean
        Get
            Return _ServiceItem
        End Get
        Set(ByVal value As Boolean)
            _ServiceItem = value
        End Set
    End Property
    Public Property TradePrice() As Double
        Get
            Return _TradePrice
        End Get
        Set(ByVal value As Double)
            _TradePrice = value
        End Set
    End Property
    Public Property Freight() As Double
        Get
            Return _Freight
        End Get
        Set(ByVal value As Double)
            _Freight = value
        End Set
    End Property
    Public Property MarketReturns() As Double
        Get
            Return _MarketReturns
        End Get
        Set(ByVal value As Double)
            _MarketReturns = value
        End Set
    End Property
    Public Property GST_Applicable() As Boolean
        Get
            Return _GST_Applicable
        End Get
        Set(ByVal value As Boolean)
            _GST_Applicable = value
        End Set
    End Property
    Public Property FlatRate_Applicable() As Boolean
        Get
            Return _FlatRate_Applicable
        End Get
        Set(ByVal value As Boolean)
            _FlatRate_Applicable = value
        End Set
    End Property
    Public Property FlatRate() As Double
        Get
            Return _FlatRate
        End Get
        Set(ByVal value As Double)
            _FlatRate = value
        End Set
    End Property
    Public Property ItemWeight() As Double
        Get
            Return _ItemWeight
        End Get
        Set(ByVal value As Double)
            _ItemWeight = value
        End Set
    End Property

    Private _HS_Code As String
    Public Property HS_Code() As String
        Get
            Return _HS_Code
        End Get
        Set(ByVal value As String)
            _HS_Code = value
        End Set
    End Property
    Public Property LargestPackQty() As Double
        Get
            Return _LargestPackQty
        End Get
        Set(ByVal value As Double)
            _LargestPackQty = value
        End Set
    End Property

    Public Property CostPrice() As Double
        Get
            Return _CostPrice
        End Get
        Set(ByVal value As Double)
            _CostPrice = value
        End Set
    End Property
    Public Property ArticleCategoryId() As Integer
        Get
            Return _ArticleCategoryId
        End Get
        Set(ByVal value As Integer)
            _ArticleCategoryId = value
        End Set
    End Property
    Private _ArticleBrandId As Integer
    Public Property ArticleBrandId() As Integer
        Get
            Return _ArticleBrandId
        End Get
        Set(ByVal value As Integer)
            _ArticleBrandId = value
        End Set
    End Property
    Public Property ApplyAdjustmentFuelExp() As Boolean
        Get
            Return _ApplyAdjustmentFuelExp
        End Get
        Set(value As Boolean)
            _ApplyAdjustmentFuelExp = value
        End Set
    End Property
    Public Property ArticleModelList As List(Of ArticleModels)
    ''TASK TFS1772
    Public Property MultiDimentionalItem() As Boolean
        Get
            Return _MultiDimentionalItem
        End Get
        Set(value As Boolean)
            _MultiDimentionalItem = value
        End Set
    End Property
    ''TFS1957
    Public Property LogicalItem() As Boolean
        Get
            Return _LogicalItem
        End Get
        Set(value As Boolean)
            _LogicalItem = value
        End Set
    End Property
    ''End TFS1957

    ''Start TFS3762
    Public Property ArticleBARCode() As String
        Get
            Return Me._ArticleBARCode
        End Get
        Set(ByVal value As String)
            Me._ArticleBARCode = value
        End Set
    End Property
    ''End TFS3762
    ''Start TFS3763
    Public Property ArticleBARCodeDisable() As String
        Get
            Return Me._ArticleBARCodeDisable
        End Get
        Set(ByVal value As String)
            Me._ArticleBARCodeDisable = value
        End Set
    End Property
    ''End TFS3763

End Class
