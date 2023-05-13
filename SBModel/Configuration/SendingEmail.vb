Public Class SendingEmail
    Private _FromEmail As String
    Public Property FromEmail() As String
        Get
            Return _FromEmail
        End Get
        Set(ByVal value As String)
            _FromEmail = value
        End Set
    End Property

    Private _ToEmail As String
    Public Property ToEmail() As String
        Get
            Return _ToEmail
        End Get
        Set(ByVal value As String)
            _ToEmail = value
        End Set
    End Property

    Private _CcEmail As String
    Public Property CcEmail() As String
        Get
            Return _CcEmail
        End Get
        Set(ByVal value As String)
            _CcEmail = value
        End Set
    End Property

    Private _BccEmail As String
    Public Property BccEmail() As String
        Get
            Return _BccEmail
        End Get
        Set(ByVal value As String)
            _BccEmail = value
        End Set
    End Property

    Private _Subject As String
    Public Property Subject() As String
        Get
            Return _Subject
        End Get
        Set(ByVal value As String)
            _Subject = value
        End Set
    End Property

    Private _Body As String
    Public Property Body() As String
        Get
            Return _Body
        End Get
        Set(ByVal value As String)
            _Body = value
        End Set
    End Property

    Private _FileAttachment As String
    Public Property FileAttachment() As String
        Get
            Return _FileAttachment
        End Get
        Set(ByVal value As String)
            _FileAttachment = value
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

    Private _DocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return _DocumentDate
        End Get
        Set(ByVal value As DateTime)
            _DocumentDate = value
        End Set
    End Property



End Class
