Public Class AllowanceTypeBE

    Private _AllowanceTypeId As Integer
    Public Property AllowanceTypeId() As Integer
        Get
            Return _AllowanceTypeId
        End Get
        Set(ByVal value As Integer)
            _AllowanceTypeId = value
        End Set
    End Property

    Private _AllowanceType As String
    Public Property AllowanceType() As String
        Get
            Return _AllowanceType
        End Get
        Set(ByVal value As String)
            _AllowanceType = value
        End Set
    End Property

    Private _AccountId As Integer
    Public Property AccountId() As Integer
        Get
            Return _AccountId
        End Get
        Set(ByVal value As Integer)
            _AccountId = value
        End Set
    End Property

    Private _Active As Boolean
    Public Property Active() As Boolean
        Get
            Return _Active
        End Get
        Set(ByVal value As Boolean)
            _Active = value
        End Set
    End Property
End Class
