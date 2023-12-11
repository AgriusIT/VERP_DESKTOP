Public Class EmailSeeting
    Private _EmailID As Integer
    Private _DisplayName As String
    



    Public Property EmailID() As String
        Get
            Return Me._EmailID
        End Get
        Set(ByVal value As String)
            Me._EmailID = value
        End Set
    End Property


    Public Property DisplayName() As String
        Get
            Return Me._DisplayName
        End Get
        Set(ByVal value As String)
            Me._DisplayName = value
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
    Private _EmailUser As String
    Public Property EmailUser() As String
        Get
            Return _EmailUser
        End Get
        Set(ByVal value As String)
            _EmailUser = value
        End Set
    End Property

    Private _Host As String
    Public Property Host() As String
        Get
            Return _Host
        End Get
        Set(ByVal value As String)
            _Host = value
        End Set
    End Property

    Private _EmailPassword As String
    Public Property EmailPassword() As String
        Get
            Return _EmailPassword
        End Get
        Set(ByVal value As String)
            _EmailPassword = value
        End Set
    End Property

    Private _SmtpServer As String
    Public Property SmtpServer() As String
        Get
            Return _SmtpServer
        End Get
        Set(ByVal value As String)
            _SmtpServer = value
        End Set
    End Property

    Private _port As Integer
    Public Property port() As Integer
        Get
            Return _port
        End Get
        Set(ByVal value As Integer)
            _port = value
        End Set
    End Property



    Private _ssl As Boolean
    Public Property ssl() As Boolean
        Get
            Return _SSL
        End Get
        Set(ByVal value As Boolean)
            _SSL = value
        End Set
    End Property



End Class
