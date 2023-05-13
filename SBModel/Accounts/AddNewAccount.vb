Public Class AddNewAccount

    Private _Account_Type As String
    Private _Sub_Sub_Id As Integer
    Private _Sub_Sub_Title As String
    Public Property Account_Type() As String
        Get
            Return _Account_Type
        End Get
        Set(ByVal value As String)
            _Account_Type = value
        End Set
    End Property
    Public Property Sub_Sub_Id() As Integer
        Get
            Return _Sub_Sub_Id
        End Get
        Set(ByVal value As Integer)
            _Sub_Sub_Id = value
        End Set
    End Property
    Public Property Sub_Sub_Title() As String
        Get
            Return _Sub_Sub_Title
        End Get
        Set(ByVal value As String)
            _Sub_Sub_Title = value
        End Set

    End Property


End Class
