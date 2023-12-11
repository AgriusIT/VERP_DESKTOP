Public Class SiteRegistrationDocumentsBE

    Private _ID As Integer
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Private _SiteRegistrationId As Integer
    Public Property SiteRegistrationId() As Integer
        Get
            Return _SiteRegistrationId
        End Get
        Set(ByVal value As Integer)
            _SiteRegistrationId = value
        End Set
    End Property

    Private _File_Name As String
    Public Property File_Name() As String
        Get
            Return _File_Name
        End Get
        Set(ByVal value As String)
            _File_Name = value
        End Set
    End Property

    Private _File_Path As String
    Public Property File_Path() As String
        Get
            Return _File_Path
        End Get
        Set(ByVal value As String)
            _File_Path = value
        End Set
    End Property

    


End Class
