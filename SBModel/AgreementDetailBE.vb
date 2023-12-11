Public Class AgreementDetailBE

    Private _AgreementDetailId As Integer
    Public Property AgreementDetailId() As Integer
        Get
            Return _AgreementDetailId
        End Get
        Set(ByVal value As Integer)
            _AgreementDetailId = value
        End Set
    End Property

    Private _AgreementId As Integer
    Public Property AgreementId() As Integer
        Get
            Return _AgreementId
        End Get
        Set(ByVal value As Integer)
            _AgreementId = value
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

    Private _PackQty As Double
    Public Property PackQty() As Double
        Get
            Return _PackQty
        End Get
        Set(ByVal value As Double)
            _PackQty = value
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

    Private _Pack_Desc As String
    Public Property Pack_Desc() As String
        Get
            Return _Pack_Desc
        End Get
        Set(ByVal value As String)
            _Pack_Desc = value
        End Set
    End Property


End Class
