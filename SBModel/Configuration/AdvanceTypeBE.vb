Public Class AdvanceTypeBE

    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _Title As String
    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
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
    Private _SalaryDeduct As Boolean
    Public Property SalaryDeduct() As Boolean
        Get
            Return _SalaryDeduct
        End Get
        Set(ByVal value As Boolean)
            _SalaryDeduct = value
        End Set
    End Property

   

    Private _SortOrder As Integer
    Public Property SortOrder() As Integer
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Integer)
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
    Private _AccountId As Integer
    Public Property AccountId() As Integer
        Get
            Return _AccountId
        End Get
        Set(ByVal value As Integer)
            _AccountId = value
        End Set
    End Property

End Class
