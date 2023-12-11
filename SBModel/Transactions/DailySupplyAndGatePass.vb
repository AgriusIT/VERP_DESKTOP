Public Class DailySupplyAndGatePass

    Private _Id As Integer
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property

    Private _DeliveryDate As DateTime
    Public Property DeliveryDate() As DateTime
        Get
            Return _DeliveryDate
        End Get
        Set(ByVal value As DateTime)
            _DeliveryDate = value
        End Set
    End Property

    Private _SalesNo As String
    Public Property SalesNo() As String
        Get
            Return _SalesNo
        End Get
        Set(ByVal value As String)
            _SalesNo = value
        End Set
    End Property

    Private _Status As String
    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
        End Set
    End Property

    Private _UserName As String
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property

    Private _EntryDate As DateTime
    Public Property EntryDate() As DateTime
        Get
            Return _EntryDate
        End Get
        Set(ByVal value As DateTime)
            _EntryDate = value
        End Set
    End Property

    Private _BiltyNo As String
    Public Property BiltyNo() As String
        Get
            Return _BiltyNo
        End Get
        Set(ByVal value As String)
            _BiltyNo = value
        End Set
    End Property

End Class
