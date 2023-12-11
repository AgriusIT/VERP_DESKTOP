Public Class EmployeeArticleCostRateBE

    Private _ID As Integer
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Private _Employee_ID As Integer
    Public Property Employee_ID() As Integer
        Get
            Return _Employee_ID
        End Get
        Set(ByVal value As Integer)
            _Employee_ID = value
        End Set
    End Property

    Private _ArticleDefId As Integer
    Public Property ArticleDefId() As Integer
        Get
            Return _ArticleDefId
        End Get
        Set(ByVal value As Integer)
            _ArticleDefId = value
        End Set
    End Property

    Private _Rate As Double
    Public Property Rate() As Double
        Get
            Return _Rate
        End Get
        Set(ByVal value As Double)
            _Rate = value
        End Set
    End Property
End Class
