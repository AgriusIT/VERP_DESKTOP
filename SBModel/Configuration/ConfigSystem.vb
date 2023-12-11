Imports SBModel
Public Class ConfigSystem
    Private _Config_Id As Integer
    Private _Config_Type As String
    Private _Config_Value As String
    Private _Comments As String
    Private _IsActive As Boolean
    Private _ActivityLog As ActivityLog

    Public Property Config_Id() As Integer
        Get
            Return _Config_Id
        End Get
        Set(ByVal value As Integer)
            _Config_Id = value
        End Set
    End Property
    Public Property Config_Value() As String
        Get
            Return _Config_Value
        End Get
        Set(ByVal value As String)
            _Config_Value = value
        End Set
    End Property
    Public Property Config_Type() As String
        Get
            Return _Config_Type
        End Get
        Set(ByVal value As String)
            _Config_Type = value
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
    Public Property IsActive() As Boolean
        Get
            Return _IsActive
        End Get
        Set(ByVal value As Boolean)
            _IsActive = value
        End Set
    End Property
    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property
    
End Class
