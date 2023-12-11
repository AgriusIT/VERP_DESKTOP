Public Class StockAdjustmenDetailBE

    Private _SA_detail_id As Integer
    Public Property SA_detail_id() As Integer
        Get
            Return _SA_detail_id
        End Get
        Set(ByVal value As Integer)
            _SA_detail_id = value
        End Set
    End Property

    Private _SA_id As Integer
    Public Property SA_id() As Integer
        Get
            Return _SA_id
        End Get
        Set(ByVal value As Integer)
            _SA_id = value
        End Set
    End Property

    Private _location_id As Integer
    Public Property location_id() As Integer
        Get
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
        End Set
    End Property

    Private _Artical_id As Integer
    Public Property Artical_id() As Integer
        Get
            Return _Artical_id
        End Get
        Set(ByVal value As Integer)
            _Artical_id = value
        End Set
    End Property

    Private _ArticalSize As String
    Public Property ArticalSize() As String
        Get
            Return _ArticalSize
        End Get
        Set(ByVal value As String)
            _ArticalSize = value
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

    Private _S1 As Double
    Public Property S1() As Double
        Get
            Return _S1
        End Get
        Set(ByVal value As Double)
            _S1 = value
        End Set
    End Property

    Private _S2 As Double
    Public Property S2() As Double
        Get
            Return _S2
        End Get
        Set(ByVal value As Double)
            _S2 = value
        End Set
    End Property

    Private _S3 As Double
    Public Property S3() As Double
        Get
            Return _S3
        End Get
        Set(ByVal value As Double)
            _S3 = value
        End Set
    End Property

    Private _S4 As Double
    Public Property S4() As Double
        Get
            Return _S4
        End Get
        Set(ByVal value As Double)
            _S4 = value
        End Set
    End Property
    Private _S5 As Double
    Public Property S5() As Double
        Get
            Return _S5
        End Get
        Set(ByVal value As Double)
            _S5 = value
        End Set
    End Property

    Private _S6 As Double
    Public Property S6() As Double
        Get
            Return _S6
        End Get
        Set(ByVal value As Double)
            _S6 = value
        End Set
    End Property

    Private _S7 As Double
    Public Property S7() As Double
        Get
            Return _S7
        End Get
        Set(ByVal value As Double)
            _S7 = value
        End Set
    End Property

    Private _Current_price As Double
    Public Property Current_price() As Double
        Get
            Return _Current_price
        End Get
        Set(ByVal value As Double)
            _Current_price = value
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

    Private _AdjustmentTypeId As Integer
    Public Property AdjustmentTypeId() As Integer
        Get
            Return _AdjustmentTypeId
        End Get
        Set(ByVal value As Integer)
            _AdjustmentTypeId = value
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
    Private _BatchNo As String
    Public Property BatchNo() As String
        Get
            Return _BatchNo
        End Get
        Set(ByVal value As String)
            _BatchNo = value
        End Set
    End Property
    Private _ExpiryDate As DateTime
    Public Property ExpiryDate() As DateTime
        Get
            Return _ExpiryDate
        End Get
        Set(ByVal value As DateTime)
            _ExpiryDate = value
        End Set
    End Property
    Private _Origin As String
    Public Property Origin() As String
        Get
            Return _Origin
        End Get
        Set(ByVal value As String)
            _Origin = value
        End Set
    End Property


End Class
