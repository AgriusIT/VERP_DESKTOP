Public Class OpeningStockMaster

    Private _ReceivingId As Integer
    Public Property ReceivingId() As Integer
        Get
            Return _ReceivingId
        End Get
        Set(ByVal value As Integer)
            _ReceivingId = value
        End Set
    End Property

    Private _Supplier As Integer
    Public Property Supplier() As Integer
        Get
            Return _Supplier
        End Get
        Set(ByVal value As Integer)
            _Supplier = value
        End Set
    End Property

    Private _DcDate As DateTime
    Public Property DcDate() As DateTime
        Get
            Return _DcDate
        End Get
        Set(ByVal value As DateTime)
            _DcDate = value
        End Set
    End Property

    Private _OpeningStockDetail As List(Of OpeningStockDetail)
    Public Property OpeningStockDetail() As List(Of OpeningStockDetail)
        Get
            Return _OpeningStockDetail
        End Get
        Set(ByVal value As List(Of OpeningStockDetail))
            _OpeningStockDetail = value
        End Set
    End Property

    Private _Document As String
    Public Property Document() As String
        Get
            Return _Document
        End Get
        Set(ByVal value As String)
            _Document = value
        End Set
    End Property

    Private _UserName As String
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property

    Private _NetAmount As Double
    Public Property NetAmount() As Double
        Get
            Return _NetAmount
        End Get
        Set(ByVal value As Double)
            _NetAmount = value
        End Set
    End Property

    Private _TotalQty As Double
    Public Property TotalQty() As Double
        Get
            Return _TotalQty
        End Get
        Set(ByVal value As Double)
            _TotalQty = value
        End Set
    End Property

    Private _StockAccountId As Integer
    Public Property StockAccountId() As Integer
        Get
            Return _StockAccountId
        End Get
        Set(ByVal value As Integer)
            _StockAccountId = value
        End Set
    End Property

    Private _DocTypeId As Integer
    Public Property DocTypeId() As Integer
        Get
            Return _DocTypeId
        End Get
        Set(ByVal value As Integer)
            _DocTypeId = value
        End Set
    End Property






End Class
