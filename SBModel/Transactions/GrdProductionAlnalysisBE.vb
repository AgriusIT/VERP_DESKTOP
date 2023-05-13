
Public Class GrdProductionAlnalysisBE


    Private _Id As Integer
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property


    Private _AnalysisDate As Date
    Public Property AnalysisDate() As Date
        Get
            Return _AnalysisDate
        End Get
        Set(ByVal value As Date)
            _AnalysisDate = value
        End Set
    End Property


    Private _ProjectId As Integer
    Public Property ProjectId() As Integer
        Get
            Return _ProjectId
        End Get
        Set(ByVal value As Integer)
            _ProjectId = value
        End Set
    End Property


    Private _ArticleDefId As Integer
    Public Property ArticleDefId() As Integer
        Get
            Return _ArticleDefId
        End Get
        Set(ByVal value As Integer)
            _ArticleDefId = value
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


    Private _Demand As Double
    Public Property Demand() As Double
        Get
            Return _Demand
        End Get
        Set(ByVal value As Double)
            _Demand = value
        End Set
    End Property


    Private _CurrentStock As Double
    Public Property CurrentStock() As Double
        Get
            Return _CurrentStock
        End Get
        Set(ByVal value As Double)
            _CurrentStock = value
        End Set
    End Property


    Private _Production As Double
    Public Property Production() As Double
        Get
            Return _Production
        End Get
        Set(ByVal value As Double)
            _Production = value
        End Set
    End Property


    Private _Estimate As Double
    Public Property Estimate() As Double
        Get
            Return _Estimate
        End Get
        Set(ByVal value As Double)
            _Estimate = value
        End Set
    End Property


    Private _Batch As Double
    Public Property Batch() As Double
        Get
            Return _Batch
        End Get
        Set(ByVal value As Double)
            _Batch = value
        End Set
    End Property


End Class
