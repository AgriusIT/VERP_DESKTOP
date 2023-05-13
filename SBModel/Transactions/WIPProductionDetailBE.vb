Public Class WIPProductionDetailBE

    Private _DocDetailId As Integer
    Public Property DocDetailId() As Integer
        Get
            Return _DocDetailId
        End Get
        Set(ByVal value As Integer)
            _DocDetailId = value
        End Set
    End Property

    Private _DocId As Integer
    Public Property DocId() As Integer
        Get
            Return _DocId
        End Get
        Set(ByVal value As Integer)
            _DocId = value
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
    Public Property AritlceSize() As String
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

    Private _Qty As Double
    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal value As Double)
            _Qty = value
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

    Private _Rate As Double
    Public Property Rate() As Double
        Get
            Return _Rate
        End Get
        Set(ByVal value As Double)
            _Rate = value
        End Set
    End Property

    Private _TotalAmount As Integer
    Public Property TotalAmount() As Integer
        Get
            Return _TotalAmount
        End Get
        Set(ByVal value As Integer)
            _TotalAmount = value
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

    Private _VehicleNo As String
    Public Property VehicleNo() As String
        Get
            Return _VehicleNo
        End Get
        Set(ByVal value As String)
            _VehicleNo = value
        End Set
    End Property

    Private _GatepassNo As String
    Public Property GatepassNo() As String
        Get
            Return _GatepassNo
        End Get
        Set(ByVal value As String)
            _GatepassNo = value
        End Set
    End Property

    Private _TruckNo As String
    Public Property TruckNo() As String
        Get
            Return _TruckNo
        End Get
        Set(ByVal value As String)
            _TruckNo = value
        End Set
    End Property
    Private _TransType As String
    Public Property TransType() As String
        Get
            Return _TransType
        End Get
        Set(ByVal value As String)
            _TransType = value
        End Set
    End Property
    ''Altered By Ali Ansari against Task# 201507015 Adding Inqty,outqty,inamount,outamount  Properties 
    Private _InQty As Double
    Public Property InQty() As Double
        Get
            Return _InQty
        End Get
        Set(ByVal value As Double)
            _InQty = value
        End Set
    End Property
    Private _OutQty As Double
    Public Property OutQty() As Double
        Get
            Return _OutQty
        End Get
        Set(ByVal value As Double)
            _OutQty = value
        End Set
    End Property
    Private _InAmount As Double
    Public Property InAmount() As Double
        Get
            Return _InAmount
        End Get
        Set(ByVal value As Double)
            _InAmount = value
        End Set
    End Property

    Private _OutAmount As Double
    Public Property OutAmount() As Double
        Get
            Return _OutAmount
        End Get
        Set(ByVal value As Double)
            _OutAmount = value
        End Set
    End Property
    'Altered By Ali Ansari against Task# 201507015 Adding Inqty,outqty,inamount,outamount  Properties 
End Class
