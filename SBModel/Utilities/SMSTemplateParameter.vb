Public Class SMSTemplateParameter

    Private _ID As Integer
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Private _Key As String
    Public Property Key() As String
        Get
            Return _Key
        End Get
        Set(ByVal value As String)
            _Key = value
        End Set
    End Property

    Private _SMSTemplate As String
    Public Property SMSTemplate() As String
        Get
            Return _SMSTemplate
        End Get
        Set(ByVal value As String)
            _SMSTemplate = value
        End Set
    End Property

#Region "Parameter Enum"
    Public Enum enmSMSTemplateParameter
        AccountCode
        AccountTitle
        DocumentNo
        DocumentDate
        OtherDocNo
        Remarks
        Amount
        Quantity
        ChequeNo
        ChequeDate
        CompanyName
        CellNo
        SIRIUS
    End Enum
    Public Enum enmTemplateKey
        Payment
        Receipt
        InvoicePayment
        InvoiceReceipt
        SaleInvoice
        PurchaseInvoice
        JournalVoucher
    End Enum
#End Region

End Class
#Region "Parameters"
Public Class SMSParameters
    Private _AccountCode As String = String.Empty
    Public Property AccountCode() As String
        Get
            Return _AccountCode
        End Get
        Set(ByVal value As String)
            _AccountCode = value
        End Set
    End Property

    Private _AccountTitle As String = String.Empty
    Public Property AccountTitle() As String
        Get
            Return _AccountTitle
        End Get
        Set(ByVal value As String)
            _AccountTitle = value
        End Set
    End Property

    Private _DocumentNo As String = String.Empty
    Public Property DocumentNo() As String
        Get
            Return _DocumentNo
        End Get
        Set(ByVal value As String)
            _DocumentNo = value
        End Set
    End Property

    Private _DocumentDate As DateTime = Nothing
    Public Property DocumentDate() As DateTime
        Get
            Return _DocumentDate
        End Get
        Set(ByVal value As DateTime)
            _DocumentDate = value
        End Set
    End Property

    Private _OtherDocNo As String = String.Empty
    Public Property OtherDocNo() As String
        Get
            Return _OtherDocNo
        End Get
        Set(ByVal value As String)
            _OtherDocNo = value
        End Set
    End Property

    Private _Remarks As String = String.Empty
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

    Private _Amount As Double = 0D
    Public Property Amount() As Double
        Get
            Return _Amount
        End Get
        Set(ByVal value As Double)
            _Amount = value
        End Set
    End Property

    Private _Quantity As Double = 0D
    Public Property Quantity() As Double
        Get
            Return _Quantity
        End Get
        Set(ByVal value As Double)
            _Quantity = value
        End Set
    End Property

    Private _ChequeNo As String = String.Empty
    Public Property ChequeNo() As String
        Get
            Return _ChequeNo
        End Get
        Set(ByVal value As String)
            _ChequeNo = value
        End Set
    End Property

    Private _ChequeDate As DateTime = Nothing
    Public Property ChequeDate() As DateTime
        Get
            Return _ChequeDate
        End Get
        Set(ByVal value As DateTime)
            _ChequeDate = value
        End Set
    End Property

    Private _CompanyName As String = String.Empty
    Public Property CompanyName() As String
        Get
            Return _CompanyName
        End Get
        Set(ByVal value As String)
            _CompanyName = value
        End Set
    End Property

    Private _CellNo As String = String.Empty
    Public Property CellNo() As String
        Get
            Return _CellNo
        End Get
        Set(ByVal value As String)
            _CellNo = value
        End Set
    End Property

    Private _SIRIUS As String = String.Empty
    Public Property SIRIUS() As String
        Get
            Return _SIRIUS
        End Get
        Set(ByVal value As String)
            _SIRIUS = value
        End Set
    End Property


End Class
#End Region
#Region "SMS Config BE"
Public Class SMSConfigBE

    Private _ID As Integer
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Private _Event_Key As String
    Public Property Event_Key() As String
        Get
            Return _Event_Key
        End Get
        Set(ByVal value As String)
            _Event_Key = value
        End Set
    End Property

    Private _Enable As Boolean
    Public Property Enable() As Boolean
        Get
            Return _Enable
        End Get
        Set(ByVal value As Boolean)
            _Enable = value
        End Set
    End Property

    Private _EnabledAdmin As Boolean
    Public Property EnabledAdmin() As Boolean
        Get
            Return _EnabledAdmin
        End Get
        Set(ByVal value As Boolean)
            _EnabledAdmin = value
        End Set
    End Property


    Private Shared _EventKeyList As List(Of SMSConfigBE)
    Public Shared Property EventKeyList() As List(Of SMSConfigBE)
        Get
            Return _EventKeyList
        End Get
        Set(ByVal value As List(Of SMSConfigBE))
            _EventKeyList = value
        End Set
    End Property

End Class
#End Region
#Region "SMS Log BE"
Public Class SMSLogBE

    Private _SMSLogID As Integer
    Public Property SMSLogID() As Integer
        Get
            Return _SMSLogID
        End Get
        Set(ByVal value As Integer)
            _SMSLogID = value
        End Set
    End Property

    Private _SMSLogDate As DateTime = Date.Now
    Public Property SMSLogDate() As DateTime
        Get
            Return _SMSLogDate
        End Get
        Set(ByVal value As DateTime)
            _SMSLogDate = value
        End Set
    End Property

    Private _SMSBody As String = String.Empty
    Public Property SMSBody() As String
        Get
            Return _SMSBody
        End Get
        Set(ByVal value As String)
            _SMSBody = value
        End Set
    End Property

    Private _SMSType As String = String.Empty
    Public Property SMSType() As String
        Get
            Return _SMSType
        End Get
        Set(ByVal value As String)
            _SMSType = value
        End Set
    End Property

    Private _PhoneNo As String = String.Empty
    Public Property PhoneNo() As String
        Get
            Return _PhoneNo
        End Get
        Set(ByVal value As String)
            _PhoneNo = value
        End Set
    End Property

    Private _SentStatus As String = "Pending"
    Public Property SentStatus() As String
        Get
            Return _SentStatus
        End Get
        Set(ByVal value As String)
            _SentStatus = value
        End Set
    End Property

    Private _SentDate As DateTime
    Public Property SentDate() As DateTime
        Get
            Return _SentDate
        End Get
        Set(ByVal value As DateTime)
            _SentDate = value
        End Set
    End Property

    Private _DeliveryStatus As String
    Public Property DeliveryStatus() As String
        Get
            Return _DeliveryStatus
        End Get
        Set(ByVal value As String)
            _DeliveryStatus = value
        End Set
    End Property

    Private _DeliveryDate As DateTime
    Public Property DeliveryDate() As DateTime
        Get
            Return _DeliveryDate
        End Get
        Set(ByVal value As DateTime)
            _DeliveryDate = value
        End Set
    End Property

    Private _TransactionID As Integer = 0I
    Public Property TransactionID() As Integer
        Get
            Return _TransactionID
        End Get
        Set(ByVal value As Integer)
            _TransactionID = value
        End Set
    End Property

    Private _ProcessLogID As Integer = 0I
    Public Property ProcessLogID() As Integer
        Get
            Return _ProcessLogID
        End Get
        Set(ByVal value As Integer)
            _ProcessLogID = value
        End Set
    End Property

    Private _CreatedByUserID As Integer
    Public Property CreatedByUserID() As Integer
        Get
            Return _CreatedByUserID
        End Get
        Set(ByVal value As Integer)
            _CreatedByUserID = value
        End Set
    End Property

    Private _SentByUserID As Integer
    Public Property SentByUserID() As Integer
        Get
            Return _SentByUserID
        End Get
        Set(ByVal value As Integer)
            _SentByUserID = value
        End Set
    End Property
End Class
#End Region


'Public Class Logged_In_Users

'    Private _LoggedInUserID As Integer
'    Public Property LoggedInUserID() As Integer
'        Get
'            Return _LoggedInUserID
'        End Get
'        Set(ByVal value As Integer)
'            _LoggedInUserID = value
'        End Set
'    End Property

'    Private _LoggedInUserName As String
'    Public Property LoggedInUserName() As String
'        Get
'            Return _LoggedInUserName
'        End Get
'        Set(ByVal value As String)
'            _LoggedInUserName = value
'        End Set
'    End Property

'    Private _LoggedInUserEmail As String
'    Public Property LoggedInUserEmail() As String
'        Get
'            Return _LoggedInUserEmail
'        End Get
'        Set(ByVal value As String)
'            _LoggedInUserEmail = value
'        End Set
'    End Property

'    Private _LoggedInUserPassword As String
'    Public Property LoggedInUserPassword() As String
'        Get
'            Return _LoggedInUserPassword
'        End Get
'        Set(ByVal value As String)
'            _LoggedInUserPassword = value
'        End Set
'    End Property

'    Private _DisplayName As String
'    Public Property DisplayName() As String
'        Get
'            Return _DisplayName
'        End Get
'        Set(ByVal value As String)
'            _DisplayName = value
'        End Set
'    End Property


'End Class
