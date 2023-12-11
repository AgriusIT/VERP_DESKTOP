Public Class ShiftGroupBE

    Private _ShiftGroupId As Integer
    Public Property ShiftGroupId() As Integer
        Get
            Return _ShiftGroupId
        End Get
        Set(ByVal value As Integer)
            _ShiftGroupId = value
        End Set
    End Property

    Private _ShiftGroupCode As String
    Public Property ShiftGroupCode() As String
        Get
            Return _ShiftGroupCode
        End Get
        Set(ByVal value As String)
            _ShiftGroupCode = value
        End Set
    End Property

    Private _ShiftGroupName As String
    Public Property ShiftGroupName() As String
        Get
            Return _ShiftGroupName
        End Get
        Set(ByVal value As String)
            _ShiftGroupName = value
        End Set
    End Property

    Private _ShiftGroupComments As String
    Public Property ShiftGroupComments() As String
        Get
            Return _ShiftGroupComments
        End Get
        Set(ByVal value As String)
            _ShiftGroupComments = value
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

    Private _SortOrder As Integer
    Public Property SortOrder() As Integer
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Integer)
            _SortOrder = value
        End Set
    End Property
End Class
