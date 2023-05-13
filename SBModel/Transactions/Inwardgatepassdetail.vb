Public Class Inwardgatepassdetail


    Private _InwardgatepassdetailId As Integer
    Public Property InwardgatepassdetailId() As Integer
        Get
            Return _InwardgatepassdetailId
        End Get
        Set(ByVal value As Integer)
            _InwardgatepassdetailId = value
        End Set
    End Property

    Private _InwardgatepassId As Integer
    Public Property InwardgatepassId() As Integer
        Get
            Return _InwardgatepassId
        End Get
        Set(ByVal value As Integer)
            _InwardgatepassId = value
        End Set
    End Property

    Private _Detail As String
    Public Property Detail() As String
        Get
            Return _Detail
        End Get
        Set(ByVal value As String)
            _Detail = value
        End Set
    End Property
    ''Start TFS3078 :Ayesha Rehman : 18-04-2018
    Private _PreviousQty As Double
    Public Property PreviousQty() As Double
        Get
            Return _PreviousQty
        End Get
        Set(ByVal value As Double)
            _PreviousQty = value
        End Set
    End Property
    ''End TFS3078
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

    Private _Unit As String
    Public Property Unit() As String
        Get
            Return _Unit
        End Get
        Set(ByVal value As String)
            _Unit = value
        End Set
    End Property



End Class
