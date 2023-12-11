Public Class CashRequestDetailBE

    Private _RequestDetailID As Integer
    Public Property RequestDetailID() As Integer
        Get
            Return _RequestDetailID
        End Get
        Set(ByVal value As Integer)
            _RequestDetailID = value
        End Set
    End Property

    Private _RequestID As Integer
    Public Property RequestID() As Integer
        Get
            Return _RequestID
        End Get
        Set(ByVal value As Integer)
            _RequestID = value
        End Set
    End Property

    Private _coa_detail_id As Integer
    Public Property coa_detail_id() As Integer
        Get
            Return _coa_detail_id
        End Get
        Set(ByVal value As Integer)
            _coa_detail_id = value
        End Set
    End Property

    Private _Amount As Integer
    Public Property Amount() As Integer
        Get
            Return _Amount
        End Get
        Set(ByVal value As Integer)
            _Amount = value
        End Set
    End Property

    Private _Paid_Amount As Integer
    Public Property Paid_Amount() As Integer
        Get
            Return _Paid_Amount
        End Get
        Set(ByVal value As Integer)
            _Paid_Amount = value
        End Set
    End Property

    Private _Comments As String
    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
        End Set
    End Property
    Private _CostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return _CostCenterId
        End Get
        Set(ByVal value As Integer)
            _CostCenterId = value
        End Set
    End Property

    Private _EmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return _EmployeeId
        End Get
        Set(ByVal value As Integer)
            _EmployeeId = value
        End Set
    End Property

End Class
