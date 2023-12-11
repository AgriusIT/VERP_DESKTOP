Public Class PaymentTypeBE

    Private _PayTypeId As Integer
    Public Property PayTypeId() As Integer
        Get
            Return _PayTypeId
        End Get
        Set(ByVal value As Integer)
            _PayTypeId = value
        End Set
    End Property

    Private _PaymentType As String
    Public Property PaymentType() As String
        Get
            Return _PaymentType
        End Get
        Set(ByVal value As String)
            _PaymentType = value
        End Set
    End Property

    Private _Active As Boolean
    Public Property Active() As Boolean
        Get
            Return _Active
        End Get
        Set(ByVal value As Boolean)
            _Active = value
        End Set
    End Property

    Private _SortOrder As Integer
    Public Property SortOrder() As Integer
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Integer)
            _SortOrder = value
        End Set
    End Property
End Class
