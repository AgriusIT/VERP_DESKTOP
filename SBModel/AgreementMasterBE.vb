Public Class AgreementMasterBE

    Private _AgreementId As Integer
    Public Property AgreementId() As Integer
        Get
            Return _AgreementId
        End Get
        Set(ByVal value As Integer)
            _AgreementId = value
        End Set
    End Property

    Private _AgreementNo As String
    Public Property AgreementNo() As String
        Get
            Return _AgreementNo
        End Get
        Set(ByVal value As String)
            _AgreementNo = value
        End Set
    End Property

    Private _AgreementDate As DateTime
    Public Property AgreementDate() As DateTime
        Get
            Return _AgreementDate
        End Get
        Set(ByVal value As DateTime)
            _AgreementDate = value
        End Set
    End Property

    Private _Delivery_Date As DateTime
    Public Property Delivery_Date() As DateTime
        Get
            Return _Delivery_Date
        End Get
        Set(ByVal value As DateTime)
            _Delivery_Date = value
        End Set
    End Property

    Private _First_Payment As Double
    Public Property First_Payment() As Double
        Get
            Return _First_Payment
        End Get
        Set(ByVal value As Double)
            _First_Payment = value
        End Set
    End Property

    Private _AgreementType As String
    Public Property AgreementType() As String
        Get
            Return _AgreementType
        End Get
        Set(ByVal value As String)
            _AgreementType = value
        End Set
    End Property

    Private _Business_Name As String
    Public Property Business_Name() As String
        Get
            Return _Business_Name
        End Get
        Set(ByVal value As String)
            _Business_Name = value
        End Set
    End Property

    Private _Contact_Name As String
    Public Property Contact_Name() As String
        Get
            Return _Contact_Name
        End Get
        Set(ByVal value As String)
            _Contact_Name = value
        End Set
    End Property

    Private _Business_Type As String
    Public Property Business_Type() As String
        Get
            Return _Business_Type
        End Get
        Set(ByVal value As String)
            _Business_Type = value
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

    Private _Phone As String
    Public Property Phone() As String
        Get
            Return _Phone
        End Get
        Set(ByVal value As String)
            _Phone = value
        End Set
    End Property

    Private _FaxNo As String
    Public Property FaxNo() As String
        Get
            Return _FaxNo
        End Get
        Set(ByVal value As String)
            _FaxNo = value
        End Set
    End Property

    Private _Email As String
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Private _StateID As Integer
    Public Property StateID() As Integer
        Get
            Return _StateID
        End Get
        Set(ByVal value As Integer)
            _StateID = value
        End Set
    End Property

    Private _CityID As Integer
    Public Property CityID() As Integer
        Get
            Return _CityID
        End Get
        Set(ByVal value As Integer)
            _CityID = value
        End Set
    End Property


    Private _TerritoryID As Integer
    Public Property TerritoryID() As Integer
        Get
            Return _TerritoryID
        End Get
        Set(ByVal value As Integer)
            _TerritoryID = value
        End Set
    End Property


    Private _Product_Category_Condition As String
    Public Property Product_Category_Condition() As String
        Get
            Return _Product_Category_Condition
        End Get
        Set(ByVal value As String)
            _Product_Category_Condition = value
        End Set
    End Property

    Private _Term_Condition As String
    Public Property Term_Condition() As String
        Get
            Return _Term_Condition
        End Get
        Set(ByVal value As String)
            _Term_Condition = value
        End Set
    End Property

    Private _Warranty_Condition As String
    Public Property Warranty_Condition() As String
        Get
            Return _Warranty_Condition
        End Get
        Set(ByVal value As String)
            _Warranty_Condition = value
        End Set
    End Property

    Private _Termination_Condition As String
    Public Property Termination_Condition() As String
        Get
            Return _Termination_Condition
        End Get
        Set(ByVal value As String)
            _Termination_Condition = value
        End Set
    End Property

    Private _Status As Boolean
    Public Property Status() As Boolean
        Get
            Return _Status
        End Get
        Set(ByVal value As Boolean)
            _Status = value
        End Set
    End Property

    Private _AgreementDetail As List(Of AgreementDetailBE)
    Public Property AgreementDetail() As List(Of AgreementDetailBE)
        Get
            Return _AgreementDetail
        End Get
        Set(ByVal value As List(Of AgreementDetailBE))
            _AgreementDetail = value
        End Set
    End Property

    Private _Total_Qty As Double
    Public Property Total_Qty() As Double
        Get
            Return _Total_Qty
        End Get
        Set(ByVal value As Double)
            _Total_Qty = value
        End Set
    End Property

    Private _Total_Amount As Double
    Public Property Total_Amount() As Double
        Get
            Return _Total_Amount
        End Get
        Set(ByVal value As Double)
            _Total_Amount = value
        End Set
    End Property

    Private _User_Name As String
    Public Property User_Name() As String
        Get
            Return _User_Name
        End Get
        Set(ByVal value As String)
            _User_Name = value
        End Set
    End Property

    Private _Discount As Double
    Public Property Discount() As Double
        Get
            Return _Discount
        End Get
        Set(ByVal value As Double)
            _Discount = value
        End Set
    End Property
    ''Start TFS1854
    Private _Customer_Name As String
    Public Property Customer_Name() As String
        Get
            Return _Customer_Name
        End Get
        Set(ByVal value As String)
            _Customer_Name = value
        End Set
    End Property
    Private _No_of_Attachment As Integer
    Public Property No_of_Attachment() As Integer
        Get
            Return _No_of_Attachment
        End Get
        Set(ByVal value As Integer)
            _No_of_Attachment = value
        End Set
    End Property
    Public Property ArrFile As List(Of String)
    Public Property Source As String
    Public Property AttachmentPath As String
  
    ''End TFS1854

End Class
