Public Class AssetBE

    Private _Asset_Id As Integer
    Public Property Asset_Id() As Integer
        Get
            Return _Asset_Id
        End Get
        Set(ByVal value As Integer)
            _Asset_Id = value
        End Set
    End Property

    Private _Asset_Name As String
    Public Property Asset_Name() As String
        Get
            Return _Asset_Name
        End Get
        Set(ByVal value As String)
            _Asset_Name = value
        End Set
    End Property

    Private _Asset_Number As String
    Public Property Asset_Number() As String
        Get
            Return _Asset_Number
        End Get
        Set(ByVal value As String)
            _Asset_Number = value
        End Set
    End Property

    Private _Asset_Description As String
    Public Property Asset_Description() As String
        Get
            Return _Asset_Description
        End Get
        Set(ByVal value As String)
            _Asset_Description = value
        End Set
    End Property

    Private _Asset_Location As String
    Public Property Asset_Location() As String
        Get
            Return _Asset_Location
        End Get
        Set(ByVal value As String)
            _Asset_Location = value
        End Set
    End Property

    Private _Asset_Manufacturer As String
    Public Property Asset_Manufacturer() As String
        Get
            Return _Asset_Manufacturer
        End Get
        Set(ByVal value As String)
            _Asset_Manufacturer = value
        End Set
    End Property

    Private _Asset_Brand As String
    Public Property Asset_Brand() As String
        Get
            Return _Asset_Brand
        End Get
        Set(ByVal value As String)
            _Asset_Brand = value
        End Set
    End Property

    Private _Asset_Model As String
    Public Property Asset_Model() As String
        Get
            Return _Asset_Model
        End Get
        Set(ByVal value As String)
            _Asset_Model = value
        End Set
    End Property

    Private _Asset_Serial_No As String
    Public Property Asset_Serial_No() As String
        Get
            Return _Asset_Serial_No
        End Get
        Set(ByVal value As String)
            _Asset_Serial_No = value
        End Set
    End Property

    Private _Asset_Picture As String
    Public Property Asset_Picture() As String
        Get
            Return _Asset_Picture
        End Get
        Set(ByVal value As String)
            _Asset_Picture = value
        End Set
    End Property

    Private _Asset_Detail As String
    Public Property Asset_Detail() As String
        Get
            Return _Asset_Detail
        End Get
        Set(ByVal value As String)
            _Asset_Detail = value
        End Set
    End Property

    Private _Sort_Order As Integer
    Public Property Sort_Order() As Integer
        Get
            Return _Sort_Order
        End Get
        Set(ByVal value As Integer)
            _Sort_Order = value
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

    Private _Asset_Category_Id As Integer
    Public Property Asset_Category_Id() As Integer
        Get
            Return _Asset_Category_Id
        End Get
        Set(ByVal value As Integer)
            _Asset_Category_Id = value
        End Set
    End Property

    Private _Asset_Type_Id As Integer
    Public Property Asset_Type_Id() As Integer
        Get
            Return _Asset_Type_Id
        End Get
        Set(ByVal value As Integer)
            _Asset_Type_Id = value
        End Set
    End Property

    Private _Asset_Status_Id As Integer
    Public Property Asset_Status_Id() As Integer
        Get
            Return _Asset_Status_Id
        End Get
        Set(ByVal value As Integer)
            _Asset_Status_Id = value
        End Set
    End Property

    Private _Asset_Condition_Id As Integer
    Public Property Asset_Condition_Id() As Integer
        Get
            Return _Asset_Condition_Id
        End Get
        Set(ByVal value As Integer)
            _Asset_Condition_Id = value
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

    Private _PurchaseDate As DateTime
    Public Property PurchaseDate() As DateTime
        Get
            Return _PurchaseDate
        End Get
        Set(ByVal value As DateTime)
            _PurchaseDate = value
        End Set
    End Property

    Private _PurchasePrice As Double
    Public Property PurchasePrice() As Double
        Get
            Return _PurchasePrice
        End Get
        Set(ByVal value As Double)
            _PurchasePrice = value
        End Set
    End Property

    Private _CurrentValue As Double
    Public Property CurrentValue() As Double
        Get
            Return _CurrentValue
        End Get
        Set(ByVal value As Double)
            _CurrentValue = value
        End Set
    End Property

    Private _Warranty_Expire_Date As DateTime
    Public Property Warranty_Expire_Date() As DateTime
        Get
            Return _Warranty_Expire_Date
        End Get
        Set(ByVal value As DateTime)
            _Warranty_Expire_Date = value
        End Set
    End Property

    Private _EmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return _EmployeeId
        End Get
        Set(ByVal value As Integer)
            _EmployeeId = value
        End Set
    End Property
End Class
