'' 05-12-15 TASK12515 Muahammad Ameen: Terminal Pattern to select desire enviroment for different users.
Public Class TerminalConfigurationMaster
    Private _TCMId As Int32
    Public Property TCMId() As Int32
        Get
            Return _TCMId
        End Get
        Set(ByVal value As Int32)
            _TCMId = value
        End Set
    End Property
    Private _Title As String
    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
        End Set
    End Property
    Private _Layout As String
    Public Property Layout() As String
        Get
            Return _Layout
        End Get
        Set(ByVal value As String)
            _Layout = value
        End Set
    End Property
    Private _FormId As String
    Public Property FormId() As String
        Get
            Return _FormId
        End Get
        Set(ByVal value As String)
            _FormId = value
        End Set
    End Property

    Public Detail As List(Of TerminalConfigurationDetail)
    Public Users As List(Of TerminalConfigurationUsers)
    Public Systems As List(Of TerminalConfigurationSystems)


    Public Sub New()
        Detail = New List(Of TerminalConfigurationDetail)
        Users = New List(Of TerminalConfigurationUsers)
        Systems = New List(Of TerminalConfigurationSystems)
    End Sub









End Class
' TerminalConfigurationDetail
Public Class TerminalConfigurationDetail
    Private _TCDId As String
    Public Property TCDId() As Int32
        Get
            Return _TCDId
        End Get
        Set(ByVal value As Int32)
            _TCDId = value
        End Set
    End Property
    Private _TCMId As Int32
    Public Property TCMId() As String
        Get
            Return _TCMId
        End Get
        Set(ByVal value As String)
            _TCMId = value
        End Set
    End Property
    Private _FormId As String
    Public Property FormId() As Int32
        Get
            Return _FormId
        End Get
        Set(ByVal value As Int32)
            _FormId = value
        End Set
    End Property
    Private _ManuTitle As String
    Public Property ManuTitle() As String
        Get
            Return _ManuTitle
        End Get
        Set(ByVal value As String)
            _ManuTitle = value
        End Set
    End Property

    Private _FormCaption As String
    Public Property FormCaption() As String
        Get
            Return _FormCaption
        End Get
        Set(ByVal value As String)
            _FormCaption = value
        End Set
    End Property






End Class

Public Class TerminalConfigurationSystems
    Private _Id As String
    Public Property Id() As String
        Get
            Return _Id
        End Get
        Set(ByVal value As String)
            _Id = value
        End Set
    End Property
    Private _SystemName As String
    Public Property SystemName() As String
        Get
            Return _SystemName
        End Get
        Set(ByVal value As String)
            _SystemName = value
        End Set
    End Property
    Private _TCMId As Int32
    Public Property TCMId() As Int32
        Get
            Return _TCMId
        End Get
        Set(ByVal value As Int32)
            _TCMId = value
        End Set
    End Property

    Private _TCDId As Int32
    Public Property TCDId() As Int32
        Get
            Return _TCDId
        End Get
        Set(ByVal value As Int32)
            _TCDId = value
        End Set
    End Property
    Private _SystemId As Int32
    Public Property SystemId() As Int32
        Get
            Return _SystemId
        End Get
        Set(ByVal value As Int32)
            _SystemId = value
        End Set
    End Property





End Class
Public Class TerminalConfigurationUsers
    Private _Id As String
    Public Property Id() As Int32
        Get
            Return _Id
        End Get
        Set(ByVal value As Int32)
            _Id = value
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
    Private _TCMId As Int32
    Public Property TCMId() As Int32
        Get
            Return _TCMId
        End Get
        Set(ByVal value As Int32)
            _TCMId = value
        End Set
    End Property
    Private _TCDId As Int32
    Public Property TCDId() As Int32
        Get
            Return _TCDId
        End Get
        Set(ByVal value As Int32)
            _TCDId = value
        End Set
    End Property

    Private _UserId As String
    Public Property UserId() As Int32
        Get
            Return _UserId
        End Get
        Set(ByVal value As Int32)
            _UserId = value
        End Set
    End Property


End Class