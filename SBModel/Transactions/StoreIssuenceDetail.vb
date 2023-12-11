Public Class StoreIssuenceDetail

    Private _DispatchDetailId As Integer
    Private _ArticleDefMasterId As Integer = 0I

    Public Property DispatchDetailId() As Integer
        Get
            Return _DispatchDetailId
        End Get
        Set(ByVal value As Integer)
            _DispatchDetailId = value
        End Set
    End Property

    Private _DispatchId As Integer
    Public Property DispatchId() As Integer
        Get
            Return _DispatchId
        End Get
        Set(ByVal value As Integer)
            _DispatchId = value
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

    Private _Sz1 As Double
    Public Property Sz1() As Double
        Get
            Return _Sz1
        End Get
        Set(ByVal value As Double)
            _Sz1 = value
        End Set
    End Property
    Private _Sz2 As Double
    Public Property Sz2() As Double
        Get
            Return _Sz2
        End Get
        Set(ByVal value As Double)
            _Sz2 = value
        End Set
    End Property
    Private _Sz3 As Double
    Public Property Sz3() As Double
        Get
            Return _Sz3
        End Get
        Set(ByVal value As Double)
            _Sz3 = value
        End Set
    End Property
    Private _Sz4 As Double
    Public Property Sz4() As Double
        Get
            Return _Sz4
        End Get
        Set(ByVal value As Double)
            _Sz4 = value
        End Set
    End Property
    Private _Sz5 As Double
    Public Property Sz5() As Double
        Get
            Return _Sz5
        End Get
        Set(ByVal value As Double)
            _Sz5 = value
        End Set
    End Property
    Private _Sz6 As Double
    Public Property Sz6() As Double
        Get
            Return _Sz6
        End Get
        Set(ByVal value As Double)
            _Sz6 = value
        End Set
    End Property
    Private _Sz7 As Double
    Public Property Sz7() As Double
        Get
            Return _Sz7
        End Get
        Set(ByVal value As Double)
            _Sz7 = value
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

    Private _CurrentPrice As Double
    Public Property CurrentPrice() As Double
        Get
            Return _CurrentPrice
        End Get
        Set(ByVal value As Double)
            _CurrentPrice = value
        End Set
    End Property

    Private _BatchNo As String
    Public Property BatchNo() As String
        Get
            Return _BatchNo
        End Get
        Set(ByVal value As String)
            _BatchNo = value
        End Set
    End Property

    Private _BatchID As Integer
    Public Property BatchID() As Integer
        Get
            Return _BatchID
        End Get
        Set(ByVal value As Integer)
            _BatchID = value
        End Set
    End Property

    Private _Pack_Desc As String
    Public Property Pack_Desc() As String
        Get
            Return _Pack_Desc
        End Get
        Set(ByVal value As String)
            _Pack_Desc = value
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

    Public Property ArticleDefMasterId() As Integer
        Get
            Return _ArticleDefMasterId
        End Get
        Set(ByVal value As Integer)
            _ArticleDefMasterId = value
        End Set
    End Property

End Class
