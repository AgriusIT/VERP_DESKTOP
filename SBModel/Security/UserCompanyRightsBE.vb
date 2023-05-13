Public Class UserCompanyRightsBE

    Private _CompanyRightsId As Integer
    Public Property CompanyRightsId() As Integer
        Get
            Return _CompanyRightsId
        End Get
        Set(ByVal value As Integer)
            _CompanyRightsId = value
        End Set
    End Property

    Private _User_Id As Integer
    Public Property User_Id() As Integer
        Get
            Return _User_Id
        End Get
        Set(ByVal value As Integer)
            _User_Id = value
        End Set
    End Property

    Private _CompanyId As Integer
    Public Property CompanyId() As Integer
        Get
            Return _CompanyId
        End Get
        Set(ByVal value As Integer)
            _CompanyId = value
        End Set
    End Property

    Private _Rights As Boolean
    Public Property Rights() As Boolean
        Get
            Return _Rights
        End Get
        Set(ByVal value As Boolean)
            _Rights = value
        End Set
    End Property
End Class
