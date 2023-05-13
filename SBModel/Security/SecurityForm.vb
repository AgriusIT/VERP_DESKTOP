Public Class SecurityForm
    Private _FormID As Integer
    Private _FormName As String
    Private _FormLabel As String
    Private _FormCategory As String

    Public Property FormID() As Integer
        Get
            Return _FormID
        End Get
        Set(ByVal value As Integer)
            _FormID = value
        End Set
    End Property

    Public Property FormName() As String
        Get
            Return _FormName
        End Get
        Set(ByVal value As String)
            _FormName = value
        End Set
    End Property

    Public Property FormLabel() As String
        Get
            Return _FormLabel
        End Get
        Set(ByVal value As String)
            _FormLabel = value
        End Set
    End Property

    Public Property FormCategory() As String
        Get
            Return _FormCategory
        End Get
        Set(ByVal value As String)
            _FormCategory = value
        End Set
    End Property


End Class
