'TaskId:2532 Revised Sales Certificate 
''01-Jul-2014 TASK:2708 Imran Ali Add new fields Registrtion for and sales tax % in Sales Certificate
''05-Jul-2014 TAS:2717 Imran Ali Add new field Reference No In Sales Certificate
''16-Aug-2014 Task:2788 Imran Ali Color Field in Sales Certificate Form
Public Class SalesCertificateBE

    Private _SaleCertificateId As Integer
    Public Property SaleCertificateId() As Integer
        Get
            Return _SaleCertificateId
        End Get
        Set(ByVal value As Integer)
            _SaleCertificateId = value
        End Set
    End Property

    Private _SaleCertificateNo As String
    Public Property SaleCertificateNo() As String
        Get
            Return _SaleCertificateNo
        End Get
        Set(ByVal value As String)
            _SaleCertificateNo = value
        End Set
    End Property

    Private _SaleCertificateDate As DateTime
    Public Property SaleCertificateDate() As DateTime
        Get
            Return _SaleCertificateDate
        End Get
        Set(ByVal value As DateTime)
            _SaleCertificateDate = value
        End Set
    End Property

    Private _DeliveredTo As String
    Public Property DeliveredTo() As String
        Get
            Return _DeliveredTo
        End Get
        Set(ByVal value As String)
            _DeliveredTo = value
        End Set
    End Property

    Private _Engine_No As String
    Public Property Engine_No() As String
        Get
            Return _Engine_No
        End Get
        Set(ByVal value As String)
            _Engine_No = value
        End Set
    End Property

    Private _Chassis_No As String
    Public Property Chassis_No() As String
        Get
            Return _Chassis_No
        End Get
        Set(ByVal value As String)
            _Chassis_No = value
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
    'TaskId:2532 Revised Sales Certificate, Add One Property Of ModelCode Aginst the New Field placed in table and store procedure of Database
    Private _ModelCode As String
    Public Property ModelCode() As String
        Get
            Return _ModelCode
        End Get
        Set(ByVal value As String)
            _ModelCode = value
        End Set
    End Property

    Private _Model_Desc As String
    Public Property Model_Desc() As String
        Get
            Return _Model_Desc
        End Get
        Set(ByVal value As String)
            _Model_Desc = value
        End Set
    End Property

    Private _Max_Laden_Weight As String
    Public Property Max_Laden_Weight() As String
        Get
            Return _Max_Laden_Weight
        End Get
        Set(ByVal value As String)
            _Max_Laden_Weight = value
        End Set
    End Property

    Private _Max_Weight_Front_Axel As String
    Public Property Max_Weight_Front_Axel() As String
        Get
            Return _Max_Weight_Front_Axel
        End Get
        Set(ByVal value As String)
            _Max_Weight_Front_Axel = value
        End Set
    End Property

    Private _Max_Weight_Rear_Axel As String
    Public Property Max_Weight_Rear_Axel() As String
        Get
            Return _Max_Weight_Rear_Axel
        End Get
        Set(ByVal value As String)
            _Max_Weight_Rear_Axel = value
        End Set
    End Property

    Private _Tyre_Front_Wheel As String
    Public Property Tyre_Front_Wheel() As String
        Get
            Return _Tyre_Front_Wheel
        End Get
        Set(ByVal value As String)
            _Tyre_Front_Wheel = value
        End Set
    End Property

    Private _Tyre_Rear_Wheel As String
    Public Property Tyre_Rear_Wheel() As String
        Get
            Return _Tyre_Rear_Wheel
        End Get
        Set(ByVal value As String)
            _Tyre_Rear_Wheel = value
        End Set
    End Property

    Private _Base_Wheel As String
    Public Property Base_Wheel() As String
        Get
            Return _Base_Wheel
        End Get
        Set(ByVal value As String)
            _Base_Wheel = value
        End Set
    End Property

    Private _Comments As String
    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
        End Set
    End Property

    Private _SalesId As Integer
    Public Property SalesId() As Integer
        Get
            Return _SalesId
        End Get
        Set(ByVal value As Integer)
            _SalesId = value
        End Set
    End Property

    Private _SalesDetailId As Integer
    Public Property SalesDetailId() As Integer
        Get
            Return _SalesDetailId
        End Get
        Set(ByVal value As Integer)
            _SalesDetailId = value
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

    Private _InvoiceAmount As Double
    Public Property InvoiceAmount() As Double
        Get
            Return _InvoiceAmount
        End Get
        Set(ByVal value As Double)
            _InvoiceAmount = value
        End Set
    End Property

    Private _SalesTax As Double
    Public Property SalesTax() As Double
        Get
            Return _SalesTax
        End Get
        Set(ByVal value As Double)
            _SalesTax = value
        End Set
    End Property

    Private _Address As String
    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property

    Private _NTN As String
    Public Property NTN() As String
        Get
            Return _NTN
        End Get
        Set(ByVal value As String)
            _NTN = value
        End Set
    End Property

    '04-Jun-2015 Ahmad Sharif : added new fields DCNO 
    Private _DC_No As String
    Public Property DC_NO() As String
        Get
            Return _DC_No
        End Get
        Set(ByVal value As String)
            _DC_No = value
        End Set
    End Property

    '04-Jun-2015  Ahmad Sharif : added new fields Remarks
    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

    ''01-Jul-2014 TASK:2708 Imran Ali Add new fields Registrtion for and sales tax % in Sales Certificate
    Private _RegistrationFor As String
    Public Property RegistrationFor() As String
        Get
            Return _RegistrationFor
        End Get
        Set(ByVal value As String)
            _RegistrationFor = value
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
    'End Task:2708

    Private _Reference_No As String
    Public Property Reference_No() As String
        Get
            Return _Reference_No
        End Get
        Set(ByVal value As String)
            _Reference_No = value
        End Set
    End Property

    Private _Color As String
    Public Property Color() As String
        Get
            Return _Color
        End Get
        Set(ByVal value As String)
            _Color = value
        End Set
    End Property

    Private _FatherName As String
    Public Property FatherName() As String
        Get
            Return _FatherName
        End Get
        Set(ByVal value As String)
            _FatherName = value
        End Set
    End Property
    Private _Person_Cast As String
    Public Property Person_Cast() As String
        Get
            Return _Person_Cast
        End Get
        Set(ByVal value As String)
            _Person_Cast = value
        End Set
    End Property
    Private _AdvanceAmount As Double
    Public Property AdvanceAmount() As Double
        Get
            Return _AdvanceAmount
        End Get
        Set(ByVal value As Double)
            _AdvanceAmount = value
        End Set
    End Property
    Private _MeterNo As String
    Public Property MeterNo() As String
        Get
            Return _MeterNo
        End Get
        Set(ByVal value As String)
            _MeterNo = value
        End Set
    End Property
    Private _Installment As Double
    Public Property Installment() As Double
        Get
            Return _Installment
        End Get
        Set(ByVal value As Double)
            _Installment = value
        End Set
    End Property
    Private _RegistrationNo As String
    Public Property RegistrationNo() As String
        Get
            Return _RegistrationNo
        End Get
        Set(ByVal value As String)
            _RegistrationNo = value
        End Set
    End Property
    Private _ContractDate As DateTime
    Public Property ContractDate() As DateTime
        Get
            Return _ContractDate
        End Get
        Set(ByVal value As DateTime)
            _ContractDate = value
        End Set
    End Property






End Class
