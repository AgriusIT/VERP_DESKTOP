Public Class BillAnalaysisDetailBE

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

    Private _FabricDetail As String
    Public Property Fabric() As String
        Get
            Return _FabricDetail
        End Get
        Set(ByVal value As String)
            _FabricDetail = value
        End Set
    End Property

    Private _DesignNo As String
    Public Property DesignNo() As String
        Get
            Return _DesignNo
        End Get
        Set(ByVal value As String)
            _DesignNo = value
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

    Private _Stitches As Double
    Public Property Stitches() As Double
        Get
            Return _Stitches
        End Get
        Set(ByVal value As Double)
            _Stitches = value
        End Set
    End Property

    Private _Sequins As Double
    Public Property Sequins() As Double
        Get
            Return _Sequins
        End Get
        Set(ByVal value As Double)
            _Sequins = value
        End Set
    End Property

    Private _Tilla As Double
    Public Property Tilla() As Double
        Get
            Return _Tilla
        End Get
        Set(ByVal value As Double)
            _Tilla = value
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

    Private _Rate As Double
    Public Property Rate() As Double
        Get
            Return _Rate
        End Get
        Set(ByVal value As Double)
            _Rate = value
        End Set
    End Property
End Class
