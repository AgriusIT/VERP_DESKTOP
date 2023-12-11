''7-Jan-2014 TASK:2370        Imran Ali         Sale and purchase invoice wise aging report
''07-Mar-2014 Task:2470 Add month in invoice base payment narration
Public Class InvoicesBasedReceiptDetail
    Private _ReceiptDetailId As Integer
    Private _InvoiceId As Integer
    Private _InvoiceNo As String
    Private _InvoiceDate As Date
    Private _Remarks As String
    Private _Gst_Percentage As Double
    Private _InvoiceAmount As Double
    Private _ReceiptAmount As Double
    Private _InvoiceBalance As Double
    Private _InvoiceBasedReceiptMaster As InvoicesBasedReceiptMaster
    Private _CostCenter As Integer

    Public Property ReceiptDetailID() As Integer
        Get
            Return _ReceiptDetailId
        End Get
        Set(ByVal value As Integer)
            Me._ReceiptDetailId = value
        End Set
    End Property
    Public Property InvoiceId() As Integer
        Get
            Return _InvoiceId
        End Get
        Set(ByVal value As Integer)
            Me._InvoiceId = value
        End Set
    End Property
    Public Property InvoiceNo() As String
        Get
            Return Me._InvoiceNo
        End Get
        Set(ByVal value As String)
            Me._InvoiceNo = value
        End Set
    End Property
    Public Property InvoiceDate() As Date
        Get
            Return _InvoiceDate
        End Get
        Set(ByVal value As Date)
            _InvoiceDate = value
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
    Public Property InvoiceAmount() As Double
        Get
            Return _InvoiceAmount
        End Get
        Set(ByVal value As Double)
            _InvoiceAmount = value
        End Set
    End Property
    Public Property ReceiptAmount() As Double
        Get
            Return Me._ReceiptAmount
        End Get
        Set(ByVal value As Double)
            Me._ReceiptAmount = value
        End Set
    End Property
    Public Property InvoiceBalance() As Double
        Get
            Return _InvoiceBalance
        End Get
        Set(ByVal value As Double)
            _InvoiceBalance = value
        End Set
    End Property
    Public Property InvoiceBasedReceiptMaster() As InvoicesBasedReceiptMaster
        Get
            Return _InvoiceBasedReceiptMaster
        End Get
        Set(ByVal value As InvoicesBasedReceiptMaster)
            _InvoiceBasedReceiptMaster = value
        End Set
    End Property
    Public Property Gst_Percentage() As Double
        Get
            Return _Gst_Percentage
        End Get
        Set(ByVal value As Double)
            _Gst_Percentage = value
        End Set
    End Property
    ''Task:2370 Added Properties

    Private _SalesTaxAmount As Double
    Public Property SalesTaxAmount() As Double
        Get
            Return _SalesTaxAmount
        End Get
        Set(ByVal value As Double)
            _SalesTaxAmount = value
        End Set
    End Property

    Private _OtherTaxAmount As Double
    Public Property OtherTaxAmount() As Double
        Get
            Return _OtherTaxAmount
        End Get
        Set(ByVal value As Double)
            _OtherTaxAmount = value
        End Set
    End Property

    Private _OtherTaxAccountId As Integer
    Public Property OtherTaxAccountId() As Integer
        Get
            Return _OtherTaxAccountId
        End Get
        Set(ByVal value As Integer)
            _OtherTaxAccountId = value
        End Set
    End Property
    ''End Task:2370
    ''07-Mar-2014 Task:2470 Add month in invoice base payment narration
    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property
    'End Task:2470

    'Task 3198 
    Public Property CostCenter() As Integer
        Get
            Return _CostCenter
        End Get
        Set(ByVal value As Integer)
            Me._CostCenter = value
        End Set
    End Property

End Class
