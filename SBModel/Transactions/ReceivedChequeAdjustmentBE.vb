Public Class ReceivedChequeAdjustmentBE

    Private _PK_ID As Integer
    Public Property PK_ID() As Integer
        Get
            Return _PK_ID
        End Get
        Set(ByVal value As Integer)
            _PK_ID = value
        End Set
    End Property

    Private _DocNo As String
    Public Property DocNo() As String
        Get
            Return _DocNo
        End Get
        Set(ByVal value As String)
            _DocNo = value
        End Set
    End Property

    Private _DocDate As DateTime
    Public Property DocDate() As DateTime
        Get
            Return _DocDate
        End Get
        Set(ByVal value As DateTime)
            _DocDate = value
        End Set
    End Property

    Private _coa_detail_id As Integer
    Public Property coa_detail_id() As Integer
        Get
            Return _coa_detail_id
        End Get
        Set(ByVal value As Integer)
            _coa_detail_id = value
        End Set
    End Property

    Private _cheque_voucher_id As Integer
    Public Property cheque_voucher_id() As Integer
        Get
            Return _cheque_voucher_id
        End Get
        Set(ByVal value As Integer)
            _cheque_voucher_id = value
        End Set
    End Property

    Private _cheque_voucher_detail_id As Integer
    Public Property cheque_voucher_detail_id() As Integer
        Get
            Return _cheque_voucher_detail_id
        End Get
        Set(ByVal value As Integer)
            _cheque_voucher_detail_id = value
        End Set
    End Property

    Private _cheque_voucher_no As String
    Public Property cheque_voucher_no() As String
        Get
            Return _cheque_voucher_no
        End Get
        Set(ByVal value As String)
            _cheque_voucher_no = value
        End Set
    End Property

    Private _cheque_no As String
    Public Property cheque_no() As String
        Get
            Return _cheque_no
        End Get
        Set(ByVal value As String)
            _cheque_no = value
        End Set
    End Property

    Private _cheque_date As DateTime
    Public Property cheque_date() As DateTime
        Get
            Return _cheque_date
        End Get
        Set(ByVal value As DateTime)
            _cheque_date = value
        End Set
    End Property

    Private _Adjustment_Amount As Double
    Public Property Adjustment_Amount() As Double
        Get
            Return _Adjustment_Amount
        End Get
        Set(ByVal value As Double)
            _Adjustment_Amount = value
        End Set
    End Property

    Private _Adjusted_voucher_id As Integer
    Public Property Adjusted_voucher_id() As Integer
        Get
            Return _Adjusted_voucher_id
        End Get
        Set(ByVal value As Integer)
            _Adjusted_voucher_id = value
        End Set
    End Property

    Private _User_Name As String
    Public Property User_Name() As String
        Get
            Return _User_Name
        End Get
        Set(ByVal value As String)
            _User_Name = value
        End Set
    End Property

    Private _Prefix As String
    Public Property Prefix() As String
        Get
            Return _Prefix
        End Get
        Set(ByVal value As String)
            _Prefix = value
        End Set
    End Property

    Private _Length As Integer
    Public Property Length() As Integer
        Get
            Return _Length
        End Get
        Set(ByVal value As Integer)
            _Length = value
        End Set
    End Property


End Class
