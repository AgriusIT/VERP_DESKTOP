Public Class SiteRegistrationCostDetailBE

    Private _SiteCostId As Integer
    Public Property SiteCostId() As Integer
        Get
            Return _SiteCostId
        End Get
        Set(ByVal value As Integer)
            _SiteCostId = value
        End Set
    End Property


    Private _SiteRegistrationId As Integer
    Public Property SiteRegistrationId() As Integer
        Get
            Return _SiteRegistrationId
        End Get
        Set(ByVal value As Integer)
            _SiteRegistrationId = value
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

    Private _Tenure_Start As DateTime
    Public Property Tenure_Start() As DateTime
        Get
            Return _Tenure_Start
        End Get
        Set(ByVal value As DateTime)
            _Tenure_Start = value
        End Set
    End Property

    Private _Tenure_End As DateTime
    Public Property Tenure_End() As DateTime
        Get
            Return _Tenure_End
        End Get
        Set(ByVal value As DateTime)
            _Tenure_End = value
        End Set
    End Property

    Private _Payee_Name As String
    Public Property Payee_Name() As String
        Get
            Return _Payee_Name
        End Get
        Set(ByVal value As String)
            _Payee_Name = value
        End Set
    End Property

    Private _Payment_Term As String
    Public Property Payment_Term() As String
        Get
            Return _Payment_Term
        End Get
        Set(ByVal value As String)
            _Payment_Term = value
        End Set
    End Property

    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

End Class
