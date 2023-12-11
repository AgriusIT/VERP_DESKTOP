Public Class ChequeBookDetailBE

    Private _ChequeSerialDtId As Integer
    Public Property ChequeSerialDtId() As Integer
        Get
            Return _ChequeSerialDtId
        End Get
        Set(ByVal value As Integer)
            _ChequeSerialDtId = value
        End Set
    End Property

    Private _ChequeSerialId As Integer
    Public Property ChequeSerialId() As Integer
        Get
            Return _ChequeSerialId
        End Get
        Set(ByVal value As Integer)
            _ChequeSerialId = value
        End Set
    End Property

    Private _ChequeNo As String
    Public Property ChequeNo() As String
        Get
            Return _ChequeNo
        End Get
        Set(ByVal value As String)
            _ChequeNo = value
        End Set
    End Property

    Private _Cheque_Issued As Boolean
    Public Property Cheque_Issued() As Boolean
        Get
            Return _Cheque_Issued
        End Get
        Set(ByVal value As Boolean)
            _Cheque_Issued = value
        End Set
    End Property

    Private _VoucherDetailId As Integer
    Public Property VoucherDetailId() As Integer
        Get
            Return _VoucherDetailId
        End Get
        Set(ByVal value As Integer)
            _VoucherDetailId = value
        End Set
    End Property

End Class
