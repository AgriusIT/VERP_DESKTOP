
Public Class SalesReturnWeight
    Private _ItemWeightId As Integer
    Private _SalesReturnDate As DateTime
    Private _ArticleDefId As Integer
    Private _Weight As Double

    Public Property ItemWeightId() As Integer
        Get
            Return _ItemWeightId
        End Get
        Set(ByVal value As Integer)
            _ItemWeightId = value
        End Set
    End Property
    Public Property SalesReturnDate() As DateTime
        Get
            Return _SalesReturnDate
        End Get
        Set(ByVal value As DateTime)
            _SalesReturnDate = value
        End Set
    End Property
    Public Property ArticleDefId() As Integer
        Get
            Return _ArticleDefId
        End Get
        Set(ByVal value As Integer)
            _ArticleDefId = value
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
End Class
