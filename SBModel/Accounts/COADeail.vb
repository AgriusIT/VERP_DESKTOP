Public Class COADeail

    Private _COADetailID As Integer
    Private _MainSubSubID As Integer
    Private _DetailCode As String
    Private _DetailTitle As String
    Private _EndDate As Date
    Private _Active As Boolean
    Private _SortOrder As Integer
    Private _IsDate As Date

    Public Property IsDate() As Date
        Get
            Return Me._IsDate
        End Get
        Set(ByVal value As Date)
            Me._IsDate = value
        End Set
    End Property

    Public Property SortOrder() As Integer
        Get
            Return Me._SortOrder
        End Get
        Set(ByVal value As Integer)
            Me._SortOrder = value
        End Set
    End Property

    Public Property Active() As Boolean
        Get
            Return Me._Active
        End Get
        Set(ByVal value As Boolean)
            Me._Active = value
        End Set
    End Property

    Public Property EndDate() As Date
        Get
            Return Me._EndDate
        End Get
        Set(ByVal value As Date)
            Me._EndDate = value
        End Set
    End Property

    Public Property DetailTitle() As String
        Get
            Return Me._DetailTitle
        End Get
        Set(ByVal value As String)
            Me._DetailTitle = value
        End Set
    End Property

    Public Property DetailCode() As String
        Get
            Return Me._DetailCode
        End Get
        Set(ByVal value As String)
            Me._DetailCode = value
        End Set
    End Property

    Public Property MainSubSubID() As Integer
        Get
            Return Me._MainSubSubID
        End Get
        Set(ByVal value As Integer)
            Me._MainSubSubID = value
        End Set
    End Property

    Public Property COADetailID() As Integer
        Get
            Return Me._COADetailID
        End Get
        Set(ByVal value As Integer)
            Me._COADetailID = value
        End Set
    End Property

End Class
