Public Class UserForm

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

    Private _FormCaption As String
    Public Property FormCaption() As String
        Get
            Return _FormCaption
        End Get
        Set(ByVal value As String)
            _FormCaption = value
        End Set
    End Property

End Class
