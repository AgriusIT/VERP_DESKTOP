Public Class CompanyContactBE

    Private _PK_ID As Integer
    Public Property PK_ID() As Integer
        Get
            Return _PK_ID
        End Get
        Set(ByVal value As Integer)
            _PK_ID = value
        End Set
    End Property

    Private _RefCompanyId As Integer
    Public Property RefCompanyId() As Integer
        Get
            Return _RefCompanyId
        End Get
        Set(ByVal value As Integer)
            _RefCompanyId = value
        End Set
    End Property

    Private _ContactName As String
    Public Property ContactName() As String
        Get
            Return _ContactName
        End Get
        Set(ByVal value As String)
            _ContactName = value
        End Set
    End Property

    Private _Designation As String
    Public Property Designation() As String
        Get
            Return _Designation
        End Get
        Set(ByVal value As String)
            _Designation = value
        End Set
    End Property

    Private _Mobile As String
    Public Property Mobile() As String
        Get
            Return _Mobile
        End Get
        Set(ByVal value As String)
            _Mobile = value
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


    Private _Fax As String
    Public Property Fax() As String
        Get
            Return _Fax
        End Get
        Set(ByVal value As String)
            _Fax = value
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

    Private _Address As String
    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property

    Private _IndexNo As Integer
    Public Property IndexNo() As Integer
        Get
            Return _IndexNo
        End Get
        Set(ByVal value As Integer)
            _IndexNo = value
        End Set
    End Property


    Private _Type As String
    Public Property Type() As String
        Get
            Return _Type
        End Get
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property

    Private _Company As String
    Public Property Company() As String
        Get
            Return _Company
        End Get
        Set(ByVal value As String)
            _Company = value
        End Set
    End Property

    Private _Department As String
    Public Property Department() As String
        Get
            Return _Department
        End Get
        Set(ByVal value As String)
            _Department = value
        End Set
    End Property

    Private _NamePrefix As String
    Public Property NamePrefix() As String
        Get
            Return _NamePrefix
        End Get
        Set(ByVal value As String)
            _NamePrefix = value
        End Set
    End Property
    Private _CompanyLocation As String
    Public Property CompanyLocation() As String
        Get
            Return _CompanyLocation
        End Get
        Set(ByVal value As String)
            _CompanyLocation = value
        End Set
    End Property

    Private _CompanyLocationId As String
    Public Property CompanyLocationId() As String
        Get
            Return _CompanyLocationId
        End Get
        Set(ByVal value As String)
            _CompanyLocationId = value
        End Set
    End Property
    Private _LeadProfileContactId As String
    Public Property LeadProfileContactId() As String
        Get
            Return _LeadProfileContactId
        End Get
        Set(ByVal value As String)
            _LeadProfileContactId = value
        End Set
    End Property

End Class
