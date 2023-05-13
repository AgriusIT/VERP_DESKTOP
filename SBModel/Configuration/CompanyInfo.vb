''11-June-2014 TASK:2677 Imran Ali Commercial Invoice Configuration On Company Information (Ravi)
Public Class CompanyInfo
    Private _ID As Integer
    Private _CompanyName As String
    Private _LegalName As String
    Private _Phone As String
    Private _Fax As String
    Private _Email As String
    Private _WebPage As String
    Private _Address As String
    Private _CostCenterId As Integer
    Private _ActivityLog As ActivityLog

    Public Property CompanyID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            Me._ID = value
        End Set
    End Property
    Public Property CompanyName() As String
        Get
            Return _CompanyName
        End Get
        Set(ByVal value As String)
            Me._CompanyName = value
        End Set
    End Property
    Public Property LegalName() As String
        Get
            Return _LegalName
        End Get
        Set(ByVal value As String)
            Me._LegalName = value
        End Set
    End Property
    Public Property Phone() As String
        Get
            Return _Phone
        End Get
        Set(ByVal value As String)
            _Phone = value
        End Set
    End Property
    Public Property Fax() As String
        Get
            Return _Fax
        End Get
        Set(ByVal value As String)
            _Fax = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property
    Public Property WebPage() As String
        Get
            Return _WebPage
        End Get
        Set(ByVal value As String)
            _WebPage = value
        End Set
    End Property
    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property
    Public Property CostCenterId() As Integer
        Get
            Return _CostCenterId
        End Get
        Set(ByVal value As Integer)
            _CostCenterId = value
        End Set
    End Property
    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            Me._ActivityLog = value
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
    'Task:2677 Added Property Enable/Disabled CommercialInvoice by User
    Private _CommercialInvoice As Boolean
    Public Property CommercialInvoice() As Boolean
        Get
            Return _CommercialInvoice
        End Get
        Set(ByVal value As Boolean)
            _CommercialInvoice = value
        End Set
    End Property
    'End task:2677
    Public Property SalesTaxAccountId As Integer
End Class
