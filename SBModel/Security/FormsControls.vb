Public Class FormsControls

    Private _FormControlId As Integer
    Public Property FormControlId() As Integer
        Get
            Return _FormControlId
        End Get
        Set(ByVal value As Integer)
            _FormControlId = value
        End Set
    End Property
    Private _FormId As Integer
    Public Property FormId() As Integer
        Get
            Return _FormId
        End Get
        Set(ByVal value As Integer)
            _FormId = value
        End Set
    End Property

    Private _FormControlName As String
    Public Property FormControlName() As String
        Get
            Return _FormControlName
        End Get
        Set(ByVal value As String)
            _FormControlName = value
        End Set
    End Property

    Private _SortOrder As Integer
    Public Property SortOrder() As Boolean
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Boolean)
            _SortOrder = value
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


    Private _RightsDetail As List(Of RightsDetail)
    Public Property RightsDetail() As List(Of RightsDetail)
        Get
            Return _RightsDetail
        End Get
        Set(ByVal value As List(Of RightsDetail))
            _RightsDetail = value
        End Set
    End Property

End Class
