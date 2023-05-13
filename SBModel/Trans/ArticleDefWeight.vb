Public Class ArticleDefWeight
    Private _ItemWeightId As Integer
    Private _ArticleId As Integer
    Private _Weight As Double
    Private _UserName As String
    Private _FeedingDate As DateTime
    Public Property ItemWeightID() As Integer
        Get
            Return _ItemWeightId
        End Get
        Set(ByVal value As Integer)
            _ItemWeightId = value
        End Set
    End Property
    Public Property ArticleId() As Integer
        Get
            Return _ArticleId
        End Get
        Set(ByVal value As Integer)
            _ArticleId = value
        End Set
    End Property
    Public Property Weight() As Double
        Get
            Return _Weight
        End Get
        Set(ByVal value As Double)
            _Weight = value
        End Set
    End Property
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Public Property FeedingDate() As DateTime
        Get
            Return _FeedingDate
        End Get
        Set(ByVal value As DateTime)
            _FeedingDate = value
        End Set
    End Property

End Class
