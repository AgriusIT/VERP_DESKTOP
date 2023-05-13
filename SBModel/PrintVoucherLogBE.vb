Public Class PrintVoucherLogBE

    Private _PrintVoucherLogId As Integer
    Public Property PrintVoucherLogId() As Integer
        Get
            Return _PrintVoucherLogId
        End Get
        Set(ByVal value As Integer)
            _PrintVoucherLogId = value
        End Set
    End Property

    Private _SaleManId As Integer
    Public Property SaleManId() As Integer
        Get
            Return _SaleManId
        End Get
        Set(ByVal value As Integer)
            _SaleManId = value
        End Set
    End Property

    Private _VoucherDate As DateTime
    Public Property VoucherDate() As DateTime
        Get
            Return _VoucherDate
        End Get
        Set(ByVal value As DateTime)
            _VoucherDate = value
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
End Class
