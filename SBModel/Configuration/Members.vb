Public Class DefMembers


    Private _MembersID As Integer
    Private _MembersName As String = String.Empty = String.Empty
    Private _MembersCode As String = String.Empty
    Private _Address As Integer
    Private _SortOrder As String = String.Empty
    Private _Comments As String = String.Empty
    Private _Active As Boolean
    Private _ActivityLog As ActivityLog

    Public Sub New()
        Me._ActivityLog = New ActivityLog
    End Sub

    Public Property ActivityLog() As ActivityLog
        Get
            Return Me._ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            Me._ActivityLog = value
        End Set
    End Property


    Public Property MembersID() As Integer
        Get
            Return _MembersID
        End Get
        Set(ByVal value As Integer)
            _MembersID = value
        End Set
    End Property


    Public Property MembersName() As String
        Get
            Return _MembersName
        End Get
        Set(ByVal value As String)
            _MembersName = value
        End Set
    End Property


    Public Property MembersCode() As String
        Get
            Return _MembersCode
        End Get
        Set(ByVal value As String)
            _MembersCode = value
        End Set
    End Property


    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property


    Public Property SortOrder() As Integer
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Integer)
            _SortOrder = value
        End Set
    End Property


    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
        End Set
    End Property


    Public Property Active() As Boolean
        Get
            Return _Active
        End Get
        Set(ByVal value As Boolean)
            _Active = value
        End Set
    End Property


End Class
