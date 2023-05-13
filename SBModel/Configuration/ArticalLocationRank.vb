Public Class ArticalLocationRank
    Private _LocationID As Integer
    Private _Rank As String

    Public Property LocationID() As Integer
        Get
            Return Me._LocationID
        End Get
        Set(ByVal value As Integer)
            Me._LocationID = value
        End Set
    End Property

    Public Property Rank() As String
        Get
            Return Me._Rank
        End Get
        Set(ByVal value As String)
            Me._Rank = value
        End Set
    End Property

End Class
Public Class LocationBE

    Private _Location_Id As Integer
    Public Property Location_Id() As Integer
        Get
            Return _Location_Id
        End Get
        Set(ByVal value As Integer)
            _Location_Id = value
        End Set
    End Property

    Private _Location_Code As String
    Public Property Location_Code() As String
        Get
            Return _Location_Code
        End Get
        Set(ByVal value As String)
            _Location_Code = value
        End Set
    End Property

    Private _Location_Name As String
    Public Property Location_Name() As String
        Get
            Return _Location_Name
        End Get
        Set(ByVal value As String)
            _Location_Name = value
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

    Private _Sort_Order As Integer
    Public Property Sort_Order() As Integer
        Get
            Return _Sort_Order
        End Get
        Set(ByVal value As Integer)
            _Sort_Order = value
        End Set
    End Property

    Private _Location_Address As String
    Public Property Location_Address() As String
        Get
            Return _Location_Address
        End Get
        Set(ByVal value As String)
            _Location_Address = value
        End Set
    End Property

    Private _Location_Phone As String
    Public Property Location_Phone() As String
        Get
            Return _Location_Phone
        End Get
        Set(ByVal value As String)
            _Location_Phone = value
        End Set
    End Property

    Private _Location_Fax As String
    Public Property Location_Fax() As String
        Get
            Return _Location_Fax
        End Get
        Set(ByVal value As String)
            _Location_Fax = value
        End Set
    End Property

    Private _Location_URL As String
    Public Property Location_URL() As String
        Get
            Return _Location_URL
        End Get
        Set(ByVal value As String)
            _Location_URL = value
        End Set
    End Property

    Private _Location_Type As String
    Public Property Location_Type() As String
        Get
            Return _Location_Type
        End Get
        Set(ByVal value As String)
            _Location_Type = value
        End Set
    End Property

    Private _RestrictedItems As Boolean
    Public Property RestrictedItems() As Boolean
        Get
            Return _RestrictedItems
        End Get
        Set(ByVal value As Boolean)
            _RestrictedItems = value
        End Set
    End Property

    Private _Mobile_No As String
    Public Property Mobile_No() As String
        Get
            Return _Mobile_No
        End Get
        Set(ByVal value As String)
            _Mobile_No = value
        End Set
    End Property

    Private Shared _LocationList As List(Of LocationBE)
    Public Shared Property LocationList() As List(Of LocationBE)
        Get
            Return _LocationList
        End Get
        Set(ByVal value As List(Of LocationBE))
            _LocationList = value
        End Set
    End Property


End Class
