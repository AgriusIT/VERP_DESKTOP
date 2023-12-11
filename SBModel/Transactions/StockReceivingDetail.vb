Public Class StockReceivingDetail
    Private _ExpiryDate As DateTime ''TFS4181
    Private _Origin As String = String.Empty

    Private _ReceivingDetailId As Integer
    Public Property ReceivingDetailId() As Integer
        Get
            Return _ReceivingDetailId
        End Get
        Set(ByVal value As Integer)
            _ReceivingDetailId = value
        End Set
    End Property

    Private _ReceivingId As Integer
    Public Property ReceivingId() As Integer
        Get
            Return _ReceivingId
        End Get
        Set(ByVal value As Integer)
            _ReceivingId = value
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
    ''Start TFS4345
    Private _FromLocationId As Integer
    Public Property FromLocationId() As Integer
        Get
            Return _FromLocationId
        End Get
        Set(ByVal value As Integer)
            _FromLocationId = value
        End Set
    End Property
    ''End TFS4345
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

    Private _CustomerID As Integer
    Public Property CustomerID() As String
        Get
            Return _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
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

    Private _ReceivedQty As Double
    Public Property ReceivedQty() As Double
        Get
            Return _ReceivedQty
        End Get
        Set(ByVal value As Double)
            _ReceivedQty = value
        End Set
    End Property

    Private _RejectedQty As Double
    Public Property RejectedQty() As Double
        Get
            Return _RejectedQty
        End Get
        Set(ByVal value As Double)
            _RejectedQty = value
        End Set
    End Property

    Private _TaxPercent As Double
    Public Property TaxPercent() As Double
        Get
            Return _TaxPercent
        End Get
        Set(ByVal value As Double)
            _TaxPercent = value
        End Set
    End Property

    Private _TaxAmount As Double
    Public Property TaxAmount() As Double
        Get
            Return _TaxAmount
        End Get
        Set(ByVal value As Double)
            _TaxAmount = value
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
    Public Property ExpiryDate() As DateTime
        Get
            Return _ExpiryDate
        End Get
        Set(ByVal value As DateTime)
            _ExpiryDate = value
        End Set
    End Property
    ''End TASK-4181
    Public Property Origin() As String
        Get
            Return _Origin
        End Get
        Set(ByVal value As String)
            _Origin = value
        End Set
    End Property

End Class
