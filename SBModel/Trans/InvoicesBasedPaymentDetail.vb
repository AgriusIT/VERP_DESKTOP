''7-Jan-2014 TASK:2370        Imran Ali         Sale and purchase invoice wise aging report 
''7-Mar-2014 TASK:2470 Imran Ali Add month in invoice base payment narration
Public Class InvoicesBasedPaymentDetail
    Private _PaymentDetailId As Integer
    Private _InvoiceId As Integer
    Private _InvoiceNo As String
    Private _InvoiceDate As Date
    Private _Remarks As String
    Private _Gst_Percentage As Double
    Private _InvoiceAmount As Double
    Private _PaymentAmount As Double
    Private _InvoiceBalance As Double
    Private _Vendor_Invoice_No As String
    Private _InvoiceBasedPaymentMaster As InvoicesBasedPaymentMaster
    Private _Discount As Double
    Private _OtherPayment As Double
    Private _CostCenter As Integer
    Public Property PaymentDetailID() As Integer
        Get
            Return _PaymentDetailId
        End Get
        Set(ByVal value As Integer)
            Me._PaymentDetailId = value
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
    Public Property PaymentAmount() As Double
        Get
            Return Me._PaymentAmount
        End Get
        Set(ByVal value As Double)
            Me._PaymentAmount = value
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

    Public Property InvoiceBasedPaymentMaster() As InvoicesBasedPaymentMaster
        Get
            Return _InvoiceBasedPaymentMaster
        End Get
        Set(ByVal value As InvoicesBasedPaymentMaster)
            _InvoiceBasedPaymentMaster = value
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
    Public Property Vendor_Invoice_No() As String
        Get
            Return _Vendor_Invoice_No
        End Get
        Set(ByVal value As String)
            _Vendor_Invoice_No = value
        End Set
    End Property
    Public Property Discount() As Double
        Get
            Return _Discount
        End Get
        Set(ByVal value As Double)
            _Discount = value
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
    ''7-Mar-2014 TASK:2470 Imran Ali Add month in invoice base payment narration
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

    Public Property OtherPayment() As Double
        Get
            Return _OtherPayment
        End Get
        Set(ByVal value As Double)
            _OtherPayment = value
        End Set
    End Property
    Private _InvoiceTax As Double
    Public Property InvoiceTax() As Double
        Get
            Return _InvoiceTax
        End Get
        Set(ByVal value As Double)
            _InvoiceTax = value
        End Set
    End Property

    'Task 3199 Add CostCenter in Details'
    Public Property CostCenter() As Integer
        Get
            Return _CostCenter
        End Get
        Set(ByVal value As Integer)
            _CostCenter = value
        End Set
    End Property
End Class
