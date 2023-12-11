Public Class SecurityUser


    Public Sub New()
        Me._groupInfo = New SecurityGroup
    End Sub
    Public Sub New(UserId As Integer)
        Me._groupInfo = New SecurityGroup
        Me._userID = UserId
    End Sub

    Private _userID As Integer
    Private _userName As String
    Private _userEmail As String
    Private _loginID As String
    Private _loginPassword As String
    Private _userComments As String
    Private _userEndDate As Nullable(Of Date)
    Private _isBlocked As Boolean
    Private _groupInfo As SecurityGroup
    Private _SystemLanguage As String
    Private _IsRightToLeft As Boolean
    Private _IsWebUser As Boolean
    Private _POSCode As String
    Private _POSType As String
    Private _TerminalType As String = ""
    Private _MobileNo As String = String.Empty
    'Private _Companies As List(Of Company)

    'Public Property Companies() As List(Of Company)
    '    Get
    '        Return Me._Companies
    '    End Get
    '    Set(ByVal value As List(Of Company))
    '        Me._Companies = value
    '    End Set
    'End Property

    Public Property MobileNo() As String
        Get
            Return Me._MobileNo
        End Get
        Set(ByVal value As String)
            Me._MobileNo = value
        End Set
    End Property

    Public Property IsRightToLeft() As Boolean
        Get
            Return Me._IsRightToLeft
        End Get
        Set(ByVal value As Boolean)
            Me._IsRightToLeft = value
        End Set
    End Property


    Public Property IsWebUser() As Boolean
        Get
            Return Me._IsWebUser
        End Get
        Set(ByVal value As Boolean)
            Me._IsWebUser = value
        End Set
    End Property

    Public Property SystemLanguage() As String
        Get
            Return Me._SystemLanguage
        End Get
        Set(ByVal value As String)
            Me._SystemLanguage = value
        End Set
    End Property


    Public Property UserID() As Integer
        Get
            Return _userID
        End Get
        Set(ByVal value As Integer)
            _userID = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
        End Set
    End Property

    Public Property UserEmail() As String
        Get
            Return _userEmail
        End Get
        Set(ByVal value As String)
            _userEmail = value
        End Set
    End Property

    Public Property UserComments() As String
        Get
            Return _userComments
        End Get
        Set(ByVal value As String)
            _userComments = value
        End Set
    End Property

    Public Property UserEndDate() As Nullable(Of Date)
        Get
            Return _userEndDate
        End Get
        Set(ByVal value As Nullable(Of Date))
            _userEndDate = value
        End Set
    End Property

    Public Property IsBlocked() As Boolean
        Get
            Return _isBlocked
        End Get
        Set(ByVal value As Boolean)
            _isBlocked = value
        End Set
    End Property

    Public Property LoginID() As String
        Get
            Return _loginID
        End Get
        Set(ByVal value As String)
            _loginID = value
        End Set
    End Property

    Public Property LoginPassword() As String
        Get
            Return _loginPassword
        End Get
        Set(ByVal value As String)
            _loginPassword = value
        End Set
    End Property


    Public Property GroupInfo() As SecurityGroup
        Get
            Return _groupInfo
        End Get
        Set(ByVal value As SecurityGroup)
            _groupInfo = value
        End Set
    End Property
    Public Property POSCode() As String
        Get
            Return _POSCode
        End Get
        Set(ByVal value As String)
            _POSCode = value
        End Set
    End Property

    Public Property POSType() As String
        Get
            Return _POSType
        End Get
        Set(ByVal value As String)
            _POSType = value
        End Set
    End Property

    Public Property TerminalType() As String
        Get
            Return _TerminalType
        End Get
        Set(ByVal value As String)
            _TerminalType = value
        End Set
    End Property

End Class
