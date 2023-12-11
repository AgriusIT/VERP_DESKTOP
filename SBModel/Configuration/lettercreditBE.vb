Public Class lettercreditBE


    Private _LCdoc_Id As Integer
    Public Property LCdoc_Id() As Integer
        Get
            Return _LCdoc_Id
        End Get
        Set(ByVal value As Integer)
            _LCdoc_Id = value
        End Set
    End Property

    Private _LCdoc_Type As String
    Public Property LCdoc_Type() As String
        Get
            Return _LCdoc_Type
        End Get
        Set(ByVal value As String)
            _LCdoc_Type = value
        End Set
    End Property

    Private _LCdoc_No As String
    Public Property LCdoc_No() As String
        Get
            Return _LCdoc_No
        End Get
        Set(ByVal value As String)
            _LCdoc_No = value
        End Set
    End Property

    Private _LCdoc_Date As DateTime
    Public Property LCdoc_Date() As DateTime
        Get
            Return _LCdoc_Date
        End Get
        Set(ByVal value As DateTime)
            _LCdoc_Date = value
        End Set
    End Property

    Private _Bank As String
    Public Property Bank() As String
        Get
            Return _Bank
        End Get
        Set(ByVal value As String)
            _Bank = value
        End Set
    End Property

    Private _LCdescription As String
    Public Property LCdescription() As String
        Get
            Return _LCdescription
        End Get
        Set(ByVal value As String)
            _LCdescription = value
        End Set
    End Property

    Private _LCtype As String
    Public Property LCtype() As String
        Get
            Return _LCtype
        End Get
        Set(ByVal value As String)
            _LCtype = value
        End Set
    End Property

    Private _LCAmount As Double
    Public Property LCAmount() As Double
        Get
            Return _LCAmount
        End Get
        Set(ByVal value As Double)
            _LCAmount = value
        End Set
    End Property

    Private _PaidAmount As Double
    Public Property PaidAmount() As Double
        Get
            Return _PaidAmount
        End Get
        Set(ByVal value As Double)
            _PaidAmount = value
        End Set
    End Property


    Private _VoucherTypeId As Integer
    Public Property VoucherTypeId() As Integer
        Get
            Return _VoucherTypeId
        End Get
        Set(ByVal value As Integer)
            _VoucherTypeId = value
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

    Private _Cheque_No As String
    Public Property Cheque_No() As String
        Get
            Return _Cheque_No
        End Get
        Set(ByVal value As String)
            _Cheque_No = value
        End Set
    End Property


    Private _Cheque_Date As DateTime
    Public Property Cheque_Date() As DateTime
        Get
            Return _Cheque_Date
        End Get
        Set(ByVal value As DateTime)
            _Cheque_Date = value
        End Set
    End Property

    Private _Active As Boolean
    Public Property Active() As Boolean
        Get
            Return _Active
        End Get
        Set(ByVal value As Boolean)
            _Active = value
        End Set
    End Property


    Private _vendorId As Integer
    Public Property vendorId() As Integer
        Get
            Return _vendorId
        End Get
        Set(ByVal value As Integer)
            _vendorId = value
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

    Private _CostCenter As Integer
    Public Property CostCenter() As Integer
        Get
            Return _CostCenter
        End Get
        Set(ByVal value As Integer)
            _CostCenter = value
        End Set
    End Property

    Private _Advising_Bank As String
    Public Property Advising_Bank() As String
        Get
            Return _Advising_Bank
        End Get
        Set(ByVal value As String)
            _Advising_Bank = value
        End Set
    End Property

    Private _Special_Instruction As String
    Public Property Special_Instruction() As String
        Get
            Return _Special_Instruction
        End Get
        Set(ByVal value As String)
            _Special_Instruction = value
        End Set
    End Property

    Private _Reference_No As String
    Public Property Reference_No() As String
        Get
            Return _Reference_No
        End Get
        Set(ByVal value As String)
            _Reference_No = value
        End Set
    End Property

    Private _Performa_No As String
    Public Property Performa_No() As String
        Get
            Return _Performa_No
        End Get
        Set(ByVal value As String)
            _Performa_No = value
        End Set
    End Property

    Private _Performa_Date As DateTime
    Public Property Performa_Date() As DateTime
        Get
            Return _Performa_Date
        End Get
        Set(ByVal value As DateTime)
            _Performa_Date = value
        End Set
    End Property

    Private _Vessel As String
    Public Property Vessel() As String
        Get
            Return _Vessel
        End Get
        Set(ByVal value As String)
            _Vessel = value
        End Set
    End Property

    Private _BL_No As String
    Public Property BL_No() As String
        Get
            Return _BL_No
        End Get
        Set(ByVal value As String)
            _BL_No = value
        End Set
    End Property

    Private _BL_Date As DateTime
    Public Property BL_Date() As DateTime
        Get
            Return _BL_Date
        End Get
        Set(ByVal value As DateTime)
            _BL_Date = value
        End Set
    End Property

    Private _ETD_Date As DateTime
    Public Property ETD_Date() As DateTime
        Get
            Return _ETD_Date
        End Get
        Set(ByVal value As DateTime)
            _ETD_Date = value
        End Set
    End Property

    Private _ETA_Date As DateTime
    Public Property ETA_Date() As DateTime
        Get
            Return _ETA_Date
        End Get
        Set(ByVal value As DateTime)
            _ETA_Date = value
        End Set
    End Property

    Private _Clearing_Agent As String
    Public Property Clearing_Agent() As String
        Get
            Return _Clearing_Agent
        End Get
        Set(ByVal value As String)
            _Clearing_Agent = value
        End Set
    End Property

    Private _TransporterID As Integer
    Public Property TransporterID() As Integer
        Get
            Return _TransporterID
        End Get
        Set(ByVal value As Integer)
            _TransporterID = value
        End Set
    End Property

    Private _OpenedBy As String
    Public Property OpendBy() As String
        Get
            Return _OpenedBy
        End Get
        Set(ByVal value As String)
            _OpenedBy = value
        End Set
    End Property

    Private _Expiry_Date As DateTime
    Public Property Expiry_Date() As DateTime
        Get
            Return _Expiry_Date
        End Get
        Set(ByVal value As DateTime)
            _Expiry_Date = value
        End Set
    End Property


    Private _CurrencyType As Integer
    Public Property CurrencyType() As Integer
        Get
            Return _CurrencyType
        End Get
        Set(ByVal value As Integer)
            _CurrencyType = value
        End Set
    End Property

    Private _CurrencyRate As Double
    Public Property CurrencyRate() As Double
        Get
            Return _CurrencyRate
        End Get
        Set(ByVal value As Double)
            _CurrencyRate = value
        End Set
    End Property

    Private _PortDischarge As String
    Public Property PortDischarge() As String
        Get
            Return _PortDischarge
        End Get
        Set(ByVal value As String)
            _PortDischarge = value
        End Set
    End Property

    Private _PortLoading As String
    Public Property PortLoading() As String
        Get
            Return _PortLoading
        End Get
        Set(ByVal value As String)
            _PortLoading = value
        End Set
    End Property

    Private _LatestDateShipment As DateTime
    Public Property LatestDateShipment() As DateTime
        Get
            Return _LatestDateShipment
        End Get
        Set(ByVal value As DateTime)
            _LatestDateShipment = value
        End Set
    End Property

    Private _LastDateShipmentBefore As DateTime
    Public Property LastDateShipmentBefore() As DateTime
        Get
            Return _LastDateShipmentBefore
        End Get
        Set(ByVal value As DateTime)
            _LastDateShipmentBefore = value
        End Set
    End Property
    Private _InsurranceValue As Double
    Public Property InsurranceValue() As Double
        Get
            Return _InsurranceValue
        End Get
        Set(ByVal value As Double)
            _InsurranceValue = value
        End Set
    End Property
    Private _NN_Date As DateTime
    Public Property NN_Date() As DateTime
        Get
            Return _NN_Date
        End Get
        Set(ByVal value As DateTime)
            _NN_Date = value
        End Set
    End Property
    Private _BDR_Date As DateTime
    Public Property BDR_Date() As DateTime
        Get
            Return _BDR_Date
        End Get
        Set(ByVal value As DateTime)
            _BDR_Date = value
        End Set
    End Property
    Private _DD_Date As DateTime
    Public Property DD_Date() As DateTime
        Get
            Return _DD_Date
        End Get
        Set(ByVal value As DateTime)
            _DD_Date = value
        End Set
    End Property
    Private _DTB_Date As DateTime
    Public Property DTB_Date() As DateTime
        Get
            Return _DTB_Date
        End Get
        Set(ByVal value As DateTime)
            _DTB_Date = value
        End Set
    End Property
    Private _Freight As Double
    Public Property Freight() As Double
        Get
            Return _Freight
        End Get
        Set(ByVal value As Double)
            _Freight = value
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
    Private _Origin As String
    Public Property Origin() As String
        Get
            Return _Origin
        End Get
        Set(ByVal value As String)
            _Origin = value
        End Set
    End Property
    Public Property Status As String
    Public Property CostOfMaterial As Double
    Public Property Closed As Boolean
End Class
