Public Class ProductionLevelDetailBE

    Private _PLevelId As Integer
    Public Property PLevelId() As Integer
        Get
            Return _PLevelId
        End Get
        Set(ByVal value As Integer)
            _PLevelId = value
        End Set
    End Property

    Private _PLevelDetailId As Integer
    Public Property PLevelDetailId() As Integer
        Get
            Return _PLevelDetailId
        End Get
        Set(ByVal value As Integer)
            _PLevelDetailId = value
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
    Public Property Articlesize() As String
        Get
            Return _ArticleSize
        End Get
        Set(ByVal value As String)
            _ArticleSize = value
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
End Class
