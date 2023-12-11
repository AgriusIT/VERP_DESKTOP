Public Class CostCenter

    Private _CostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return _CostCenterId
        End Get
        Set(ByVal value As Integer)
            _CostCenterId = value
        End Set
    End Property

    Private _CostCenter As String
    Public Property CostCenter() As String
        Get
            Return _CostCenter
        End Get
        Set(ByVal value As String)
            _CostCenter = value
        End Set
    End Property

    Private _CostCenterHead As String
    Public Property CostCenterHead() As String
        Get
            Return _CostCenterHead
        End Get
        Set(ByVal value As String)
            _CostCenterHead = value
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

    Private _OutWardGatePass As Boolean
    Public Property OutWardGatePass() As Boolean
        Get
            Return _OutWardGatePass
        End Get
        Set(ByVal value As Boolean)
            _OutWardGatePass = value
        End Set
    End Property
End Class
