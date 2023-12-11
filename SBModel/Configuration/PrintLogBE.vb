
Public Class PrintLogBE

    Private _Id As Integer
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property

    Private _DocumentNo As String
    Public Property DocumentNo() As String
        Get
            Return _DocumentNo
        End Get
        Set(ByVal value As String)
            _DocumentNo = value
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

    Private _PrintDateTime As DateTime
    Public Property PrintDateTime() As DateTime
        Get
            Return _PrintDateTime
        End Get
        Set(ByVal value As DateTime)
            _PrintDateTime = value
        End Set
    End Property
End Class
