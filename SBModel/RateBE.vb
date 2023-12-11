Public Class RateBE
    Public Property [to]() As String
        Get
            Return _to
        End Get
        Set(value As String)
            _to = value
        End Set
    End Property
    Private _to As String
    Public Property from() As String
        Get
            Return _from
        End Get
        Set(value As String)
            _from = value
        End Set
    End Property
    Private _from As String
    Public Property rate() As Double
        Get
            Return _rate
        End Get
        Set(value As Double)
            _rate = value
        End Set
    End Property
    Private _rate As Double
End Class