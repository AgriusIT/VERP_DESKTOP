Public Class Email

    Private _MailSentBoxId As Integer
    Public Property MailSentBoxId() As Integer
        Get
            Return _MailSentBoxId
        End Get
        Set(ByVal value As Integer)
            _MailSentBoxId = value
        End Set
    End Property

    Private _ToEamil As String
    Public Property ToEmail() As String
        Get
            Return _ToEamil
        End Get
        Set(ByVal value As String)
            _ToEamil = value
        End Set
    End Property

    Private _CCEmail As String
    Public Property CCEmail() As String
        Get
            Return _CCEmail
        End Get
        Set(ByVal value As String)
            _CCEmail = value
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

    Private _Attachment As String
    Public Property Attachment() As String
        Get
            Return _Attachment
        End Get
        Set(ByVal value As String)
            _Attachment = value
        End Set
    End Property

    Private _TotalQty As Double
    Public Property TotalQty() As Double
        Get
            Return _TotalQty
        End Get
        Set(ByVal value As Double)
            _TotalQty = value
        End Set
    End Property

    Private _TotalAmount As Double
    Public Property TotalAmount() As Double
        Get
            Return _TotalAmount
        End Get
        Set(ByVal value As Double)
            _TotalAmount = value
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

    Private _Status As String
    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
        End Set
    End Property

End Class
