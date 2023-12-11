Imports sbModel
Public Class SecurityGroupRights
    Private _RightID As Integer
    Private _ControlID As Integer
    Private _GroupInfo As SecurityGroup
    Private _FormInfo As SecurityForm
    Private _IsSelected As Boolean
    Private _ActivityLog As ActivityLog
    Public Property ActivityLog() As ActivityLog
        Get
            Return Me._ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            Me._ActivityLog = value
        End Set
    End Property
    Public Property IsSelected() As Boolean
        Get
            Return Me._IsSelected
        End Get
        Set(ByVal value As Boolean)
            Me._IsSelected = value
        End Set
    End Property
    Public Property FormInfo() As SecurityForm
        Get
            Return Me._FormInfo
        End Get
        Set(ByVal value As SecurityForm)
            Me._FormInfo = value
        End Set
    End Property
    Public Property GroupInfo() As SecurityGroup
        Get
            Return Me._GroupInfo
        End Get
        Set(ByVal value As SecurityGroup)
            Me._GroupInfo = value
        End Set
    End Property
    Public Property ControlID() As Integer
        Get
            Return Me._ControlID
        End Get
        Set(ByVal value As Integer)
            Me._ControlID = value
        End Set
    End Property
    Public Property RightID() As Integer
        Get
            Return Me._RightID
        End Get
        Set(ByVal value As Integer)
            Me._RightID = value
        End Set
    End Property

End Class
