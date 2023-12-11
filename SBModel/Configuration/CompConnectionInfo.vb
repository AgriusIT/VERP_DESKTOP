Public Class CompConnectionInfo
    Private _Title As String
    Private _Data_Source As String
    Private _UserName As String
    Private _Password As String
    Private _Initial_Catalog As String
    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
        End Set
    End Property
    Property Data_Source() As String
        Get
            Return _Data_Source
        End Get
        Set(ByVal value As String)
            _Data_Source = value
        End Set
    End Property
    Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
    Public Property InitialCatalog() As String
        Get
            Return _Initial_Catalog
        End Get
        Set(ByVal value As String)
            _Initial_Catalog = value
        End Set
    End Property
End Class
