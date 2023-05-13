Public Class Users

    Private _UserId As Integer
    Public Property UserId() As Integer
        Get
            Return _UserId
        End Get
        Set(ByVal value As Integer)
            _UserId = value
        End Set
    End Property



    Private _UserCode As String
    Public Property UserCode() As String
        Get
            Return _UserCode
        End Get
        Set(ByVal value As String)
            _UserCode = value
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

    Private _Password As String
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property


    Private _Email As String
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Private _GroupId As Integer
    Public Property GroupId() As Integer
        Get
            Return _GroupId
        End Get
        Set(ByVal value As Integer)
            _GroupId = value
        End Set
    End Property

    Private _Block As Boolean
    Public Property Block() As Boolean
        Get
            Return _Block
        End Get
        Set(ByVal value As Boolean)
            _Block = value
        End Set
    End Property

    Private _DashBoardRights As Boolean
    Public Property DashBoardRights() As Boolean
        Get
            Return _DashBoardRights
        End Get
        Set(ByVal value As Boolean)
            _DashBoardRights = value
        End Set
    End Property

    Private _UserCompanyRights As List(Of UserCompanyRightsBE)
    Public Property UserCompanyRights() As List(Of UserCompanyRightsBE)
        Get
            Return _UserCompanyRights
        End Get
        Set(ByVal value As List(Of UserCompanyRightsBE))
            _UserCompanyRights = value
        End Set
    End Property
    Private _UserLocationRights As List(Of UserLocationRightsBE)
    Public Property UserLocationRights() As List(Of UserLocationRightsBE)
        Get
            Return _UserLocationRights
        End Get
        Set(ByVal value As List(Of UserLocationRightsBE))
            _UserLocationRights = value
        End Set
    End Property
    Private _UserAccountRights As List(Of UserAccountRightsBE)
    Public Property UserAccountRights() As List(Of UserAccountRightsBE)
        Get
            Return _UserAccountRights
        End Get
        Set(ByVal value As List(Of UserAccountRightsBE))
            _UserAccountRights = value
        End Set
    End Property
    Private _UserCostCentreRights As List(Of UserCostCentreRightsBE)
    Public Property UserCostCentreRights() As List(Of UserCostCentreRightsBE)
        Get
            Return _UserCostCentreRights
        End Get
        Set(ByVal value As List(Of UserCostCentreRightsBE))
            _UserCostCentreRights = value
        End Set
    End Property


    Private _RefUserId As Integer
    Public Property RefUserId() As Integer
        Get
            Return _RefUserId
        End Get
        Set(ByVal value As Integer)
            _RefUserId = value
        End Set
    End Property

    Private _FullName As String
    Public Property FullName() As String
        Get
            Return _FullName
        End Get
        Set(ByVal value As String)
            _FullName = value
        End Set
    End Property
    Private _UserPicture As String
    Public Property UserPicture() As String
        Get
            Return _UserPicture
        End Get
        Set(ByVal value As String)
            _UserPicture = value
        End Set
    End Property
    ''TASK:988 Display rights based voucher types on voucher entry. Ameen on 22-06-2017 
    Private _UserVoucherTypesRights As List(Of UserVoucherTypesRightsBE)
    Public Property UserVoucherTypesRights() As List(Of UserVoucherTypesRightsBE)
        Get
            Return _UserVoucherTypesRights
        End Get
        Set(ByVal value As List(Of UserVoucherTypesRightsBE))
            _UserVoucherTypesRights = value
        End Set
    End Property
    '' END TASK:988
    ''TASK TFS1417
    Private _ShowCostPriceRights As Boolean
    Public Property ShowCostPriceRights() As Boolean
        Get
            Return _ShowCostPriceRights
        End Get
        Set(ByVal value As Boolean)
            _ShowCostPriceRights = value
        End Set
    End Property
    ''End TASK TFS1417
    'TFS1751: Waqar Raza
    'Start TFS1751
    Private _UserPOSRights As List(Of UserPOSRightsBE)
    Public Property UserPOSRights() As List(Of UserPOSRightsBE)
        Get
            Return _UserPOSRights
        End Get
        Set(ByVal value As List(Of UserPOSRightsBE))
            _UserPOSRights = value
        End Set
    End Property
    'End TFS1751

    Private _FormId As Integer
    Public Property FormId() As Integer
        Get
            Return _FormId
        End Get
        Set(ByVal value As Integer)
            _FormId = value
        End Set
    End Property

    Private _FormName As String
    Public Property FormName() As String
        Get
            Return _FormName
        End Get
        Set(ByVal value As String)
            _FormName = value
        End Set
    End Property

    ''ShowMainMenuRights
    Private _ShowMainMenuRights As Boolean
    Public Property ShowMainMenuRights() As Boolean
        Get
            Return _ShowMainMenuRights
        End Get
        Set(ByVal value As Boolean)
            _ShowMainMenuRights = value
        End Set
    End Property

    ''TASK TFS4867
    Private _EmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return _EmployeeId
        End Get
        Set(ByVal value As Integer)
            _EmployeeId = value
        End Set
    End Property
    ''END TASK TFS4867
End Class
