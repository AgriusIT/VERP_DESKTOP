Public Class LoginUser

    Private Shared _LoginUserId As Integer
    Public Shared Property LoginUserId() As Integer
        Get
            Return _LoginUserId
        End Get
        Set(ByVal value As Integer)
            _LoginUserId = value
        End Set
    End Property
    Private Shared _LoginUserName As String
    Public Shared Property LoginUserName() As String
        Get
            Return _LoginUserName
        End Get
        Set(ByVal value As String)
            _LoginUserName = value
        End Set
    End Property
    Private Shared _LoginUserGroup As String
    Public Shared Property LoginUserGroup() As String
        Get
            Return _LoginUserGroup
        End Get
        Set(ByVal value As String)
            _LoginUserGroup = value
        End Set
    End Property
    Private Shared _Block As Boolean
    Public Shared Property Block() As Boolean
        Get
            Return _Block
        End Get
        Set(ByVal value As Boolean)
            _Block = value
        End Set
    End Property
    Private Shared _LoginUserCode As String
    Public Shared Property LoginUserCode() As String
        Get
            Return _LoginUserCode
        End Get
        Set(ByVal value As String)
            _LoginUserCode = value
        End Set
    End Property
    Private Shared _LoginUserEmail As String
    Public Shared Property LoginUserEmail() As String
        Get
            Return _LoginUserEmail
        End Get
        Set(ByVal value As String)
            _LoginUserEmail = value
        End Set
    End Property
    Private Shared _DashBoardRights As Boolean
    Public Shared Property DashBoardRights() As Boolean
        Get
            Return _DashBoardRights
        End Get
        Set(ByVal value As Boolean)
            _DashBoardRights = value
        End Set
    End Property
    Private Shared _LoginUserPassword As String
    Public Shared Property LoginUserPassword() As String
        Get
            Return _LoginUserPassword
        End Get
        Set(ByVal value As String)
            _LoginUserPassword = value
        End Set
    End Property
    Private Shared _DisplayName As String
    Public Shared Property DisplayName() As String
        Get
            Return _DisplayName
        End Get
        Set(ByVal value As String)
            _DisplayName = value
        End Set
    End Property
    Private Shared _LoginGroupId As Integer
    Public Shared Property LoginGroupId() As Integer
        Get
            Return _LoginGroupId
        End Get
        Set(ByVal value As Integer)
            _LoginGroupId = value
        End Set
    End Property






End Class
