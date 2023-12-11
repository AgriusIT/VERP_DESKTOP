Public Class OpeningBalanceDt
    Private _voucher_id As Integer
    Private _location_id As Integer
    Private _coa_detail_id As Integer
    Private _comments As String
    Private _debit_amount As Double
    Private _credit_amount As Double
    Public Property voucher_id() As Integer
        Get
            Return _voucher_id

        End Get
        Set(ByVal value As Integer)
            _voucher_id = value
        End Set
    End Property
    Public Property location_id() As Integer
        Get
            Return _location_id

        End Get
        Set(ByVal value As Integer)
            _location_id = value

        End Set
    End Property
    Public Property coa_detail_id() As Integer
        Get
            Return _coa_detail_id
        End Get
        Set(ByVal value As Integer)
            _coa_detail_id = value

        End Set
    End Property
    Public Property comments() As String
        Get
            Return _comments
        End Get
        Set(ByVal value As String)
            _comments = value
        End Set
    End Property
    Public Property debit_amount() As Double
        Get
            Return _debit_amount
        End Get
        Set(ByVal value As Double)
            _debit_amount = value
        End Set
    End Property
    Public Property credit_amount() As Double
        Get
            Return _credit_amount
        End Get
        Set(ByVal value As Double)
            _credit_amount = value
        End Set
    End Property

End Class

