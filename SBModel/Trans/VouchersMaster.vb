Public Class VouchersMaster

    Private _Voucher_Id As Integer = 0I
    Private _Location_Id As Integer = 0I
    Private _Voucher_Code As String = String.Empty
    Private _Financial_Year_Id As Integer = 0I
    Private _Voucher_Type_Id As Integer = 0I
    Private _Voucher_Month As String = String.Empty
    Private _Voucher_No As String = String.Empty
    Private _Voucher_Date As Date
    Private _Coa_Detail_Id As Integer = 0I
    Private _Cheque_No As String = String.Empty
    Private _Cheque_Date As String = String.Empty
    Private _Post As Integer = 0I
    Private _Source As String = String.Empty
    Private _Reference As String = String.Empty
    Private _UserName As String = String.Empty
    Private _VNo As String = String.Empty
    Private _VoucherMaster As VouchersMaster
    Private _VoucherDetail As List(Of VouchersDetail)
    Private _ActivityLog As ActivityLog
    Private _Remarks As String = String.Empty

    Private _Cheque_Status As Boolean
    Public Property Cheque_Status() As Boolean
        Get
            Return _Cheque_Status
        End Get
        Set(ByVal value As Boolean)
            _Cheque_Status = value
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
    Public Property VoucherCode() As String
        Get
            Return _Voucher_Code
        End Get
        Set(ByVal value As String)
            _Voucher_Code = value
        End Set
    End Property
    Public Property FinancialYearId() As Integer
        Get
            Return _Financial_Year_Id
        End Get
        Set(ByVal value As Integer)
            _Financial_Year_Id = value
        End Set
    End Property
    Public Property VoucherTypeId() As Integer
        Get
            Return _Voucher_Type_Id
        End Get
        Set(ByVal value As Integer)
            _Voucher_Type_Id = value
        End Set
    End Property
    Public Property VoucherMonth() As String
        Get
            Return _Voucher_Month
        End Get
        Set(ByVal value As String)
            _Voucher_Month = value
        End Set
    End Property
    Public Property VoucherNo() As String
        Get
            Return _Voucher_No
        End Get
        Set(ByVal value As String)
            _Voucher_No = value
        End Set
    End Property
    Public Property VoucherDate() As Date
        Get
            Return _Voucher_Date
        End Get
        Set(ByVal value As Date)
            _Voucher_Date = value
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
    Public Property ChequeNo() As String
        Get
            Return _Cheque_No
        End Get
        Set(ByVal value As String)
            _Cheque_No = value
        End Set
    End Property
    'Public Property ChequeDate() As String
    '    Get
    '        Return _Cheque_Date
    '    End Get
    '    Set(ByVal value As String)
    '        _Cheque_Date = value
    '    End Set
    'End Property
    Public Property ChequeDate() As DateTime
        Get
            Return _Cheque_Date
        End Get
        Set(ByVal value As DateTime)
            _Cheque_Date = value
        End Set
    End Property

    Private _BankDescription As String = String.Empty
    Public Property BankDescription() As String
        Get
            Return _BankDescription
        End Get
        Set(ByVal value As String)
            _BankDescription = value
        End Set
    End Property

    Public Property Post() As Boolean
        Get
            Return _Post
        End Get
        Set(ByVal value As Boolean)
            _Post = value
        End Set
    End Property
    Public Property Source() As String
        Get
            Return _Source
        End Get
        Set(ByVal value As String)
            _Source = value
        End Set
    End Property
    Public Property References() As String
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
    Public Property VNo() As String
        Get
            Return _VNo
        End Get
        Set(ByVal value As String)
            _VNo = value
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
    Public Property VoucherDatail() As List(Of VouchersDetail)
        Get
            Return _VoucherDetail
        End Get
        Set(ByVal value As List(Of VouchersDetail))
            _VoucherDetail = value
        End Set
    End Property
    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property

    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

    Private _Posted_UserName As String = String.Empty
    Public Property Posted_UserName() As String
        Get
            Return _Posted_UserName
        End Get
        Set(ByVal value As String)
            _Posted_UserName = value
        End Set
    End Property'
    ' TFS# 947 : Add property for notification by Ali Faisal on 16-June-2017
    Property Notification As AgriusNotifications

    ' TFS# 947 : End
    Property Reversal As Boolean
    Property ReversalVoucherId As Integer

End Class
