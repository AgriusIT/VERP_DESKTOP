Public Class PaymentTermsScheduleBE

    Private _PayScheduleId As Integer
    Public Property PayScheduleId() As Integer
        Get
            Return _PayScheduleId
        End Get
        Set(ByVal value As Integer)
            _PayScheduleId = value
        End Set
    End Property

    Private _PayTypeId As Integer
    Public Property PayTypeId() As Integer
        Get
            Return _PayTypeId
        End Get
        Set(ByVal value As Integer)
            _PayTypeId = value
        End Set
    End Property

    Private _SchDate As DateTime
    Public Property SchDate() As DateTime
        Get
            Return _SchDate
        End Get
        Set(ByVal value As DateTime)
            _SchDate = value
        End Set
    End Property

    Private _OrderId As Integer
    Public Property OrderId() As Integer
        Get
            Return _OrderId
        End Get
        Set(ByVal value As Integer)
            _OrderId = value
        End Set
    End Property

    Private _OrderType As String
    Public Property OrderType() As String
        Get
            Return _OrderType
        End Get
        Set(ByVal value As String)
            _OrderType = value
        End Set
    End Property

    Private _Amount As Double
    Public Property Amount() As Double
        Get
            Return _Amount
        End Get
        Set(ByVal value As Double)
            _Amount = value
        End Set
    End Property
End Class
