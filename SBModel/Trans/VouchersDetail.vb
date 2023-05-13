''16-Jul-2014 TASK:2745 Imran Ali Cheque Comments On Ledger (Ravi)
Public Class VouchersDetail
    Private _VoucherDetailId As Integer = 0I
    Private _Voucher_Id As Integer = 0I
    Private _Location_Id As Integer = 0I
    Private _Coa_Detail_Id As Integer = 0I
    Private _Comments As String = String.Empty
    Private _Debit_Amount As Double = 0D
    Private _Credit_Amount As Double = 0D
    Private _Currency_Debit_Amount As Double = 0D
    Private _Currency_Credit_Amount As Double = 0D
    Private _SP_Reference As String = 0D
    Private _Direction As String = 0D
    Private _CostCenter As Integer = 0D
    Private _contra_coa_detail_id As Integer = 0I
    Private _EmpId As Integer = 0I
    Private _PlotNo As String = String.Empty
    Private _VoucherMaster As VouchersMaster



    Private _Cheque_Status As Boolean
    Public Property Cheque_Status() As Boolean
        Get
            Return _Cheque_Status
        End Get
        Set(ByVal value As Boolean)
            _Cheque_Status = value
        End Set
    End Property

    Public Property VoucherDetailId() As Integer
        Get
            Return _VoucherDetailId
        End Get
        Set(ByVal value As Integer)
            _VoucherDetailId = value
        End Set
    End Property
    Public Property VoucherId() As Integer
        Get
            Return _Voucher_Id
        End Get
        Set(ByVal value As Integer)
            _Voucher_Id = value
        End Set
    End Property
    Public Property LocationId() As Integer
        Get
            Return _Location_Id
        End Get
        Set(ByVal value As Integer)
            _Location_Id = value
        End Set
    End Property
    Public Property CoaDetailId() As Integer
        Get
            Return _Coa_Detail_Id
        End Get
        Set(ByVal value As Integer)
            _Coa_Detail_Id = value
        End Set
    End Property
    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
        End Set
    End Property

    Private _Cheque_No As String = String.Empty

    Public Property Cheque_No() As String
        Get
            Return _Cheque_No
        End Get
        Set(ByVal value As String)
            _Cheque_No = value
        End Set
    End Property

    Private _Cheque_Date As DateTime = Date.MinValue

    Public Property Cheque_Date() As DateTime
        Get
            Return _Cheque_Date
        End Get
        Set(ByVal value As DateTime)
            _Cheque_Date = value
        End Set
    End Property

    Public Property DebitAmount() As Double
        Get
            Return _Debit_Amount
        End Get
        Set(ByVal value As Double)
            _Debit_Amount = value
        End Set
    End Property
    Public Property CreditAmount() As Double
        Get
            Return _Credit_Amount
        End Get
        Set(ByVal value As Double)
            _Credit_Amount = value
        End Set
    End Property
    Public Property CurrencyDebitAmount() As Double
        Get
            Return _Currency_Debit_Amount
        End Get
        Set(ByVal value As Double)
            _Currency_Debit_Amount = value
        End Set
    End Property
    Public Property CurrencyCreditAmount() As Double
        Get
            Return _Currency_Credit_Amount
        End Get
        Set(ByVal value As Double)
            _Currency_Credit_Amount = value
        End Set
    End Property
    Public Property SPReference() As String
        Get
            Return _SP_Reference
        End Get
        Set(ByVal value As String)
            _SP_Reference = value
        End Set
    End Property
    Public Property Direction() As String
        Get
            Return _Direction
        End Get
        Set(ByVal value As String)
            _Direction = value
        End Set
    End Property
    Public Property CostCenter() As Integer
        Get
            Return _CostCenter
        End Get
        Set(ByVal value As Integer)
            _CostCenter = value
        End Set
    End Property

    Public Property PlotNo() As String
        Get
            Return _PlotNo
        End Get
        Set(ByVal value As String)
            _PlotNo = value
        End Set
    End Property
    Public Property VoucherMaster() As VouchersMaster
        Get
            Return _VoucherMaster
        End Get
        Set(ByVal value As VouchersMaster)
            _VoucherMaster = value
        End Set
    End Property

    Private _Discount As Double = 0D
    Public Property Discount() As Double
        Get
            Return _Discount
        End Get
        Set(ByVal value As Double)
            _Discount = value
        End Set
    End Property

    Private _Cheque_Clearance_Date As DateTime = Date.MinValue

    Public Property Cheque_Clearance_Date() As DateTime
        Get
            Return _Cheque_Clearance_Date
        End Get
        Set(ByVal value As DateTime)
            _Cheque_Clearance_Date = value
        End Set
    End Property

    Private _PayeeTitle As String = String.Empty
    Public Property PayeeTitle() As String
        Get
            Return _PayeeTitle
        End Get
        Set(ByVal value As String)
            _PayeeTitle = value
        End Set
    End Property
    ''16-Jul-2014 TASK:2745 Imran Ali Cheque Comments On Ledger (Ravi)
    Private _ChequeDescription As String = String.Empty
    Public Property ChequeDescription() As String
        Get
            Return _ChequeDescription
        End Get
        Set(ByVal value As String)
            _ChequeDescription = value
        End Set
    End Property
    'End Task 2745


    Public Property contra_coa_detail_id() As Integer
        Get
            Return _contra_coa_detail_id
        End Get
        Set(ByVal value As Integer)
            _contra_coa_detail_id = value
        End Set
    End Property
    Public Property EmpId() As Integer
        Get
            Return _EmpId
        End Get
        Set(ByVal value As Integer)
            _EmpId = value
        End Set
    End Property
    Public Property ReversalVoucherDetailId As Integer
End Class
