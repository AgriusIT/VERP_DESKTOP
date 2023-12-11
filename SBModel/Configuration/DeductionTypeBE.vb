Public Class DeductionTypeBE

    Private _DeductionTypeId As Integer
    Public Property DeductionTypeId() As Integer
        Get
            Return _DeductionTypeId
        End Get
        Set(ByVal value As Integer)
            _DeductionTypeId = value
        End Set
    End Property

    Private _DeductionType As String
    Public Property DeductionType() As String
        Get
            Return _DeductionType
        End Get
        Set(ByVal value As String)
            _DeductionType = value
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
