''19-May-2014 TASK:2642 Imran Ali Adjustment Avg Rate In ERP
''01-Jul-2014 TASK:2707 Imran Ali AvgRate Adjustment Voucher In Adjustment Average Rate
Public Class AdjustmentAveragerateBE

    Private _Doc_Id As Integer
    Public Property Doc_Id() As Integer
        Get
            Return _Doc_Id
        End Get
        Set(ByVal value As Integer)
            _Doc_Id = value
        End Set
    End Property

    Private _Doc_Date As DateTime
    Public Property Doc_Date() As DateTime
        Get
            Return _Doc_Date
        End Get
        Set(ByVal value As DateTime)
            _Doc_Date = value
        End Set
    End Property

    Private _Doc_No As String
    Public Property Doc_No() As String
        Get
            Return _Doc_No
        End Get
        Set(ByVal value As String)
            _Doc_No = value
        End Set
    End Property

    Private _Post As Boolean
    Public Property Post() As Boolean
        Get
            Return _Post
        End Get
        Set(ByVal value As Boolean)
            _Post = value
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

    Private _UserId As Integer
    Public Property UserId() As Integer
        Get
            Return _UserId
        End Get
        Set(ByVal value As Integer)
            _UserId = value
        End Set
    End Property

    Private _EntryDate As DateTime
    Public Property EntryDate() As DateTime
        Get
            Return _EntryDate
        End Get
        Set(ByVal value As DateTime)
            _EntryDate = value
        End Set
    End Property

    Private _AdjustmentAvgRateDetail As List(Of AdjustmentAvgRateDetailBE)
    Public Property AdjustmentAvgRateDetail() As List(Of AdjustmentAvgRateDetailBE)
        Get
            Return _AdjustmentAvgRateDetail
        End Get
        Set(ByVal value As List(Of AdjustmentAvgRateDetailBE))
            _AdjustmentAvgRateDetail = value
        End Set
    End Property

    Private _StockTransId As Integer
    Public Property StockTransId() As Integer
        Get
            Return _StockTransId
        End Get
        Set(ByVal value As Integer)
            _StockTransId = value
        End Set
    End Property

    Private _VoucherId As Integer
    Public Property VoucherId() As Integer
        Get
            Return _VoucherId
        End Get
        Set(ByVal value As Integer)
            _VoucherId = value
        End Set
    End Property

End Class
Public Class AdjustmentAvgRateDetailBE

    Private _DocDetail_Id As Integer
    Public Property DocDetail_Id() As Integer
        Get
            Return _DocDetail_Id
        End Get
        Set(ByVal value As Integer)
            _DocDetail_Id = value
        End Set
    End Property
    Private _Doc_Id As Integer
    Public Property Doc_Id() As Integer
        Get
            Return _Doc_Id
        End Get
        Set(ByVal value As Integer)
            _Doc_Id = value
        End Set
    End Property

    Private _LocationId As Integer
    Public Property LocationId() As Integer
        Get
            Return _LocationId
        End Get
        Set(ByVal value As Integer)
            _LocationId = value
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

    Private _ArticleSize As String
    Public Property ArticleSize() As String
        Get
            Return _ArticleSize
        End Get
        Set(ByVal value As String)
            _ArticleSize = value
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

    Private _Current_Avg_Rate As Double
    Public Property Current_Avg_Rate() As Double
        Get
            Return _Current_Avg_Rate
        End Get
        Set(ByVal value As Double)
            _Current_Avg_Rate = value
        End Set
    End Property

    Private _Adj_New_Cost_Price As Double
    Public Property Adj_New_Cost_Price() As Double
        Get
            Return _Adj_New_Cost_Price
        End Get
        Set(ByVal value As Double)
            _Adj_New_Cost_Price = value
        End Set
    End Property

    Private _Adj_Amount As Double
    Public Property Adj_Amount() As Double
        Get
            Return _Adj_Amount
        End Get
        Set(ByVal value As Double)
            _Adj_Amount = value
        End Set
    End Property

    Private _PurchaseAccountId As Integer
    Public Property PurchaseAccountId() As Integer
        Get
            Return _PurchaseAccountId
        End Get
        Set(ByVal value As Integer)
            _PurchaseAccountId = value
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
    Private _fromDate As DateTime
    Public Property FromDate() As DateTime
        Get
            Return _fromDate
        End Get
        Set(ByVal value As DateTime)
            _fromDate = value
        End Set
    End Property
    Private _toDate As DateTime
    Public Property ToDate() As DateTime
        Get
            Return _toDate
        End Get
        Set(ByVal value As DateTime)
            _toDate = value
        End Set
    End Property



End Class

