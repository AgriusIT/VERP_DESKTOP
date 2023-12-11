Public Class InvoiceAdjustmentBE

    Private _DocId As Integer
    Public Property DocId() As Integer
        Get
            Return _DocId
        End Get
        Set(ByVal value As Integer)
            _DocId = value
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

    Private _DocNo As String
    Public Property DocNo() As String
        Get
            Return _DocNo
        End Get
        Set(ByVal value As String)
            _DocNo = value
        End Set
    End Property

    Private _InvoiceType As String
    Public Property InvoiceType() As String
        Get
            Return _InvoiceType
        End Get
        Set(ByVal value As String)
            _InvoiceType = value
        End Set
    End Property

    Private _InvoiceId As Integer
    Public Property InvoiceId() As Integer
        Get
            Return _InvoiceId
        End Get
        Set(ByVal value As Integer)
            _InvoiceId = value
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

    Private _coa_detail_id As Integer
    Public Property coa_detail_id() As Integer
        Get
            Return _coa_detail_id
        End Get
        Set(ByVal value As Integer)
            _coa_detail_id = value
        End Set
    End Property

    Private _AdjustmentAmount As Double
    Public Property AdjustmentAmount() As Double
        Get
            Return _AdjustmentAmount
        End Get
        Set(ByVal value As Double)
            _AdjustmentAmount = value
        End Set
    End Property

    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
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
    Private _Voucher_Type As String=String.Empty 

    Public Property Voucher_Type() As String
        Get
            Return _Voucher_Type
        End Get
        Set(ByVal value As String)
            _Voucher_Type = value
        End Set
    End Property

    Private _ActivityLog As ActivityLog
    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property

End Class
