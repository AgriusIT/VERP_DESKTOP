Public Class OpeningBalance
    Private _Voucher_Id As Integer
    Private _LocationId As Integer
    Private _voucher_code As String
    Private _voucher_no As String
    Private _voucher_date As DateTime
    Private _voucher_type As Integer
    Private _detail_id As Integer
    Private _Reference As String
    Private _UserName As String
    Private _OpeningBalanceDt As List(Of OpeningBalanceDt)


    Public Property LocationId() As Integer
        Get
            Return _LocationId
        End Get
        Set(ByVal value As Integer)
            _LocationId = value
        End Set
    End Property
    Public Property voucher_code() As String
        Get
            Return _voucher_code
        End Get
        Set(ByVal value As String)
            _voucher_code = value
        End Set
    End Property
    Public Property voucher_no() As String
        Get
            Return _voucher_no
        End Get
        Set(ByVal value As String)
            _voucher_no = value

        End Set
    End Property
    Public Property voucher_date() As DateTime
        Get
            Return _voucher_date

        End Get
        Set(ByVal value As DateTime)
            _voucher_date = value

        End Set
    End Property
    Public Property Reference() As String
        Get
            Return _Reference

        End Get
        Set(ByVal value As String)
            _Reference = value

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
    Public Property OpeningBalanceDt() As List(Of OpeningBalanceDt)
        Get
            Return _OpeningBalanceDt
        End Get
        Set(ByVal value As List(Of OpeningBalanceDt))
            _OpeningBalanceDt = value
        End Set
    End Property
    Public Property Voucher_Id() As Integer
        Get
            Return _Voucher_Id
        End Get
        Set(ByVal value As Integer)
            _Voucher_Id = value
        End Set
    End Property
    Public Property detail_Id() As Integer
        Get
            Return _detail_id
        End Get
        Set(ByVal value As Integer)
            _detail_id = value
        End Set
    End Property
    Public Property voucher_type() As Integer
        Get
            Return _voucher_type
        End Get
        Set(ByVal value As Integer)
            _voucher_type = value
        End Set
    End Property
End Class
