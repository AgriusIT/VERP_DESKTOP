''18-June-2014 TASK:M55 Imran Ali Optional Job Verification
''19-June-2014 TASK:2697 IMRAN ALI Optional Note  Entry On CMFA Document(Ravi)
''20-June-2014 TASK:2701 IMRAN ALI Expense Entry on CMFA Document(Ravi)
''25-June-2014 TASK:2703 IMRAN ALI Enhancement In CMFA (RAVI)
''07-Jul-2014 TASK:2723 IMRAN ALI Add Column Comments In CMFA Detail (Ravi)
''11-Jul-2014 Task:2734 IMRAN ALI Ehancement CMFA Document
''13-Aug-2014 Task:2783 Imran Ali  Frequently CMFA on Home Page (Ravi)
''03-Sep-2014 Task:2824 Imran Ali Apply all rights of admin on CMFA Document
''27-Sep-2014 Task:2856 Imran Ali CMFA Detail Report
Public Class CmfaBE

    Private _DocId As Integer
    Public Property DocId() As Integer
        Get
            Return _DocId
        End Get
        Set(ByVal value As Integer)
            _DocId = value
        End Set
    End Property

    Private _DocNo As String
    Public Property docNo() As String
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

    Private _LocationId As Integer
    Public Property locationId() As Integer
        Get
            Return _LocationId
        End Get
        Set(ByVal value As Integer)
            _LocationId = value
        End Set
    End Property

    Private _CustomerCode As Integer
    Public Property CustomerCode() As Integer
        Get
            Return _CustomerCode
        End Get
        Set(ByVal value As Integer)
            _CustomerCode = value
        End Set
    End Property

    Private _ProjectId As Integer
    Public Property ProjectId() As Integer
        Get
            Return _ProjectId
        End Get
        Set(ByVal value As Integer)
            _ProjectId = value
        End Set
    End Property

    Private _POId As Integer
    Public Property POId() As Integer
        Get
            Return _POId
        End Get
        Set(ByVal value As Integer)
            _POId = value
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

    Private _PONo As String
    Public Property PONo() As String
        Get
            Return _PONo
        End Get
        Set(ByVal value As String)
            _PONo = value
        End Set
    End Property

    Private _TotalQty As Double
    Public Property TotalQty() As Double
        Get
            Return _TotalQty
        End Get
        Set(ByVal value As Double)
            _TotalQty = value
        End Set
    End Property

    Private _TotalAmount As Double
    Public Property TotalAmount() As Double
        Get
            Return _TotalAmount
        End Get
        Set(ByVal value As Double)
            _TotalAmount = value
        End Set
    End Property

    Private _ApprovedBudget As Double
    Public Property ApproveBudget() As Double
        Get
            Return _ApprovedBudget
        End Get
        Set(ByVal value As Double)
            _ApprovedBudget = value
        End Set
    End Property

    Private _TaxPercent As Double
    Public Property TaxPercent() As Double
        Get
            Return _TaxPercent
        End Get
        Set(ByVal value As Double)
            _TaxPercent = value
        End Set
    End Property

    Private _WHTaxPercent As Double
    Public Property WHTaxPercent() As Double
        Get
            Return _WHTaxPercent
        End Get
        Set(ByVal value As Double)
            _WHTaxPercent = value
        End Set
    End Property

    Private _ExptJobCompDate As DateTime
    Public Property ExptJobCompDate() As DateTime
        Get
            Return _ExptJobCompDate
        End Get
        Set(ByVal value As DateTime)
            _ExptJobCompDate = value
        End Set
    End Property

    Private _ExptPaymentFromClient As String
    Public Property ExptPaymentFromClient() As String
        Get
            Return _ExptPaymentFromClient
        End Get
        Set(ByVal value As String)
            _ExptPaymentFromClient = value
        End Set
    End Property


    Private _JobStartingTime As DateTime
    Public Property JobStartingTime() As DateTime
        Get
            Return _JobStartingTime
        End Get
        Set(ByVal value As DateTime)
            _JobStartingTime = value
        End Set
    End Property

    Private _TentiveInvoiceDate As DateTime
    Public Property TentiveInvoiceDate() As DateTime
        Get
            Return _TentiveInvoiceDate
        End Get
        Set(ByVal value As DateTime)
            _TentiveInvoiceDate = value
        End Set
    End Property
    ''19-June-2014 TASK:2697 IMRAN ALI Optional Note  Entry On CMFA Document(Ravi)
    Private _VerificationPeriodAfterCompletionJob As DateTime
    Public Property VerificationPeriodAfterCompletionJob() As DateTime
        Get
            Return _VerificationPeriodAfterCompletionJob
        End Get
        Set(ByVal value As DateTime)
            _VerificationPeriodAfterCompletionJob = value
        End Set
    End Property
    'End TAsk:2697
    Private _Status As Boolean
    Public Property Status() As Boolean
        Get
            Return _Status
        End Get
        Set(ByVal value As Boolean)
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

    Private _ApprovedUserId As Integer
    Public Property ApprovedUserId() As Integer
        Get
            Return _ApprovedUserId
        End Get
        Set(ByVal value As Integer)
            _ApprovedUserId = value
        End Set
    End Property
    Private _Approved As Boolean
    Public Property Approved() As Boolean
        Get
            Return _Approved
        End Get
        Set(ByVal value As Boolean)
            _Approved = value
        End Set
    End Property

    Private _OpexSalePercent As Double
    Public Property OpexSalePercent() As Double
        Get
            Return _OpexSalePercent
        End Get
        Set(ByVal value As Double)
            _OpexSalePercent = value
        End Set
    End Property


    Private _CMFADetail As List(Of CMFADetailBE)
    Public Property CMFADetail() As List(Of CMFADetailBE)
        Get
            Return _CMFADetail
        End Get
        Set(ByVal value As List(Of CMFADetailBE))
            _CMFADetail = value
        End Set
    End Property
    'Task:2701 Added Property CMFA Expense Detail 
    Private _CMFAExpenseVoucher As List(Of CMFAExpVoucherBE)
    Public Property CMFAExpVoucher() As List(Of CMFAExpVoucherBE)
        Get
            Return _CMFAExpenseVoucher
        End Get
        Set(ByVal value As List(Of CMFAExpVoucherBE))
            _CMFAExpenseVoucher = value
        End Set
    End Property
    'End Task:2701

    Private _EstimateExpense As Double
    Public Property EstimateExpense() As Double
        Get
            Return _EstimateExpense
        End Get
        Set(ByVal value As Double)
            _EstimateExpense = value
        End Set
    End Property

    Private _ReturnComments As String
    Public Property ReturnComments() As String
        Get
            Return _ReturnComments
        End Get
        Set(ByVal value As String)
            _ReturnComments = value
        End Set
    End Property

    Private _ReturnStatus As Boolean
    Public Property ReturnStatus() As Boolean
        Get
            Return _ReturnStatus
        End Get
        Set(ByVal value As Boolean)
            _ReturnStatus = value
        End Set
    End Property


    Private _CMFAType As String
    Public Property CMFAType() As String
        Get
            Return _CMFAType
        End Get
        Set(ByVal value As String)
            _CMFAType = value
        End Set
    End Property

    Private _UserID As Integer
    Public Property UserID() As Integer
        Get
            Return _UserID
        End Get
        Set(ByVal value As Integer)
            _UserID = value
        End Set
    End Property

    Private _CheckedByUserID As Integer
    Public Property CheckedByUserID() As Integer
        Get
            Return _CheckedByUserID
        End Get
        Set(ByVal value As Integer)
            _CheckedByUserID = value
        End Set
    End Property
    'Task:2734 Added Field
    Private _ProjectedExpAmount As Double
    Public Property ProjectedExpAmount() As Double
        Get
            Return _ProjectedExpAmount
        End Get
        Set(ByVal value As Double)
            _ProjectedExpAmount = value
        End Set
    End Property
    'End Task:2734

    ''13-Aug-2014 Task:2783 Imran Ali  Frequently CMFA on Home Page (Ravi)
    Private _ActivityLog As SBModel.ActivityLog
    Public Property ActivityLog() As SBModel.ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As SBModel.ActivityLog)
            _ActivityLog = value
        End Set
    End Property
    'End Task:2783
    'TAsk:2823 Added Property Checked Status
    Private _CheckedStatus As Boolean
    Public Property CheckedStatus() As Boolean
        Get
            Return _CheckedStatus
        End Get
        Set(ByVal value As Boolean)
            _CheckedStatus = value
        End Set
    End Property
    'End Task:2823

    'Task:2856 Added Property' 
    Private _ApprovedUserName As String
    Public Property ApprovedUserName() As String
        Get
            Return _ApprovedUserName
        End Get
        Set(ByVal value As String)
            _ApprovedUserName = value
        End Set
    End Property

    Private _CheckedUserName As String
    Public Property CheckedUserName() As String
        Get
            Return _CheckedUserName
        End Get
        Set(ByVal value As String)
            _CheckedUserName = value
        End Set
    End Property
    'End Task:2856

End Class



Public Class CMFADetailBE

    Private _DocDetailId As Integer
    Public Property DocDetailId() As Integer
        Get
            Return _DocDetailId
        End Get
        Set(ByVal value As Integer)
            _DocDetailId = value
        End Set
    End Property

    Private _DocId As Integer
    Public Property DocId() As Integer
        Get
            Return _DocId
        End Get
        Set(ByVal value As Integer)
            _DocId = value
        End Set
    End Property

    Private _LocationId As Integer
    Public Property LocationId() As Integer
        Get
            Return _LocationId
        End Get
        Set(ByVal value As Integer)
            _LocationId = value
        End Set
    End Property


    Private _ArticleDefId As Integer
    Public Property ArticleDefId() As Integer
        Get
            Return _ArticleDefId
        End Get
        Set(ByVal value As Integer)
            _ArticleDefId = value
        End Set
    End Property

    Private _ArticleSize As String
    Public Property ArticleSize() As String
        Get
            Return _ArticleSize
        End Get
        Set(ByVal value As String)
            _ArticleSize = value
        End Set
    End Property

    Private _Sz1 As Double
    Public Property Sz1() As Double
        Get
            Return _Sz1
        End Get
        Set(ByVal value As Double)
            _Sz1 = value
        End Set
    End Property

    Private _Sz2 As Double
    Public Property Sz2() As Double
        Get
            Return _Sz2
        End Get
        Set(ByVal value As Double)
            _Sz2 = value
        End Set
    End Property


    Private _Sz7 As Double
    Public Property Sz7() As Double
        Get
            Return _Sz7
        End Get
        Set(ByVal value As Double)
            _Sz7 = value
        End Set
    End Property


    Private _Qty As Double
    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal value As Double)
            _Qty = value
        End Set
    End Property


    Private _POQty As Double
    Public Property POQty() As Double
        Get
            Return _POQty
        End Get
        Set(ByVal value As Double)
            _POQty = value
        End Set
    End Property


    Private _Price As Double
    Public Property Price() As Double
        Get
            Return _Price
        End Get
        Set(ByVal value As Double)
            _Price = value
        End Set
    End Property

    Private _Current_Price As Double
    Public Property Current_Price() As Double
        Get
            Return _Current_Price
        End Get
        Set(ByVal value As Double)
            _Current_Price = value
        End Set
    End Property


    Private _Tax_Percent As Double
    Public Property Tax_Percent() As Double
        Get
            Return _Tax_Percent
        End Get
        Set(ByVal value As Double)
            _Tax_Percent = value
        End Set
    End Property

    Private _VendorId As Integer
    Public Property VendorId() As Integer
        Get
            Return _VendorId
        End Get
        Set(ByVal value As Integer)
            _VendorId = value
        End Set
    End Property

    Private _PackDesc As String
    Public Property PackDesc() As String
        Get
            Return _PackDesc
        End Get
        Set(ByVal value As String)
            _PackDesc = value
        End Set
    End Property

    Private _InvoicePrice As Double
    Public Property InvoicePrice() As Double
        Get
            Return _InvoicePrice
        End Get
        Set(ByVal value As Double)
            _InvoicePrice = value
        End Set
    End Property
    'Task:2723 Added Property Comments
    Private _Comments As String
    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
        End Set
    End Property
    'End Task:2723
   
End Class
