Public Class SiteRegisrationBE

    Private _SiteRegistrationID As Integer
    Public Property SiteRegistrationID() As Integer
        Get
            Return _SiteRegistrationID
        End Get
        Set(ByVal value As Integer)
            _SiteRegistrationID = value
        End Set
    End Property

    Private _Registration_No As String
    Public Property Registration_No() As String
        Get
            Return _Registration_No
        End Get
        Set(ByVal value As String)
            _Registration_No = value
        End Set
    End Property

    Private _Registration_Date As DateTime
    Public Property Registration_Date() As DateTime
        Get
            Return _Registration_Date
        End Get
        Set(ByVal value As DateTime)
            _Registration_Date = value
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

    Private _Region_ID As Integer
    Public Property Region_ID() As Integer
        Get
            Return _Region_ID
        End Get
        Set(ByVal value As Integer)
            _Region_ID = value
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

    Private _Area_ID As Integer
    Public Property Area_ID() As Integer
        Get
            Return _Area_ID
        End Get
        Set(ByVal value As Integer)
            _Area_ID = value
        End Set
    End Property

    Private _Location As String
    Public Property Location() As String
        Get
            Return _Location
        End Get
        Set(ByVal value As String)
            _Location = value
        End Set
    End Property

    Private _Area_Category As String
    Public Property Area_Category() As String
        Get
            Return _Area_Category
        End Get
        Set(ByVal value As String)
            _Area_Category = value
        End Set
    End Property

    Private _Site_Type As String
    Public Property Site_Type() As String
        Get
            Return _Site_Type
        End Get
        Set(ByVal value As String)
            _Site_Type = value
        End Set
    End Property

    Private _Clutter_Info As String
    Public Property Clutter_Info() As String
        Get
            Return _Clutter_Info
        End Get
        Set(ByVal value As String)
            _Clutter_Info = value
        End Set
    End Property

    Private _Singnal_Info As String
    Public Property Singnal_Info() As String
        Get
            Return _Singnal_Info
        End Get
        Set(ByVal value As String)
            _Singnal_Info = value
        End Set
    End Property

    Private _Visibility_Distance As Double
    Public Property Visibility_Distance() As Double
        Get
            Return _Visibility_Distance
        End Get
        Set(ByVal value As Double)
            _Visibility_Distance = value
        End Set
    End Property


    Private _Traffic_Coming_From As String
    Public Property Traffic_Coming_From() As String
        Get
            Return _Traffic_Coming_From
        End Get
        Set(ByVal value As String)
            _Traffic_Coming_From = value
        End Set
    End Property


    Private _Traffic_Going_To As String
    Public Property Traffic_Going_To() As String
        Get
            Return _Traffic_Going_To
        End Get
        Set(ByVal value As String)
            _Traffic_Going_To = value
        End Set
    End Property

    Private _Traffic_Per_Day As String
    Public Property Traffic_Per_Day() As String
        Get
            Return _Traffic_Per_Day
        End Get
        Set(ByVal value As String)
            _Traffic_Per_Day = value
        End Set
    End Property

    Private _Size_Width As String
    Public Property Size_Width() As String
        Get
            Return _Size_Width
        End Get
        Set(ByVal value As String)
            _Size_Width = value
        End Set
    End Property

    Private _Size_Height As String
    Public Property Size_Height() As String
        Get
            Return _Size_Height
        End Get
        Set(ByVal value As String)
            _Size_Height = value
        End Set
    End Property

    Private _Longitude As String
    Public Property Longitude() As String
        Get
            Return _Longitude
        End Get
        Set(ByVal value As String)
            _Longitude = value
        End Set
    End Property

    Private _Latitude As String
    Public Property Latitude() As String
        Get
            Return _Latitude
        End Get
        Set(ByVal value As String)
            _Latitude = value
        End Set
    End Property

    Private _Sided As String
    Public Property Sided() As String
        Get
            Return _Sided
        End Get
        Set(ByVal value As String)
            _Sided = value
        End Set
    End Property

    Private _Authority As String
    Public Property Authority() As String
        Get
            Return _Authority
        End Get
        Set(ByVal value As String)
            _Authority = value
        End Set
    End Property

    Private _RA As String
    Public Property RA() As String
        Get
            Return _RA
        End Get
        Set(ByVal value As String)
            _RA = value
        End Set
    End Property


    Private _Owner_Name As String
    Public Property Owner_Name() As String
        Get
            Return _Owner_Name
        End Get
        Set(ByVal value As String)
            _Owner_Name = value
        End Set
    End Property

    Private _Owner_Address As String
    Public Property Owner_Address() As String
        Get
            Return _Owner_Address
        End Get
        Set(ByVal value As String)
            _Owner_Address = value
        End Set
    End Property

    Private _Owner_CNIC_No As String
    Public Property Owner_CNIC_No() As String
        Get
            Return _Owner_CNIC_No
        End Get
        Set(ByVal value As String)
            _Owner_CNIC_No = value
        End Set
    End Property

    Private _Bank_Ac_No1 As String
    Public Property Bank_Ac_No1() As String
        Get
            Return _Bank_Ac_No1
        End Get
        Set(ByVal value As String)
            _Bank_Ac_No1 = value
        End Set
    End Property

    Private _Bank_Ac_No2 As String
    Public Property Bank_Ac_No2() As String
        Get
            Return _Bank_Ac_No2
        End Get
        Set(ByVal value As String)
            _Bank_Ac_No2 = value
        End Set
    End Property

    Private _Availability_Date As DateTime
    Public Property Availability_Date() As DateTime
        Get
            Return _Availability_Date
        End Get
        Set(ByVal value As DateTime)
            _Availability_Date = value
        End Set
    End Property

    Private _UserId As Integer
    Public Property UserId() As Integer
        Get
            Return _UserId
        End Get
        Set(ByVal value As Integer)
            _UserId = value
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
    Private _SiteRegistrationCostDetail As List(Of SiteRegistrationCostDetailBE)
    Public Property SiteRegistrationCostDetail() As List(Of SiteRegistrationCostDetailBE)
        Get
            Return _SiteRegistrationCostDetail
        End Get
        Set(ByVal value As List(Of SiteRegistrationCostDetailBE))
            _SiteRegistrationCostDetail = value
        End Set
    End Property

    Private _SiteRegistationDocument As SiteRegistrationDocumentsBE
    Public Property SiteRegistationDocument() As SiteRegistrationDocumentsBE
        Get
            Return _SiteRegistationDocument
        End Get
        Set(ByVal value As SiteRegistrationDocumentsBE)
            _SiteRegistationDocument = value
        End Set
    End Property



End Class
