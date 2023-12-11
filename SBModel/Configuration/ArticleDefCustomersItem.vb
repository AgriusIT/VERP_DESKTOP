Public Class ArticleDefVendorsItem
    Private _MasterId As Integer
    Private _ArticleDefId As Integer
    Private _VendorId As Integer
    Private _UserName As String
    Private _DateLog As DateTime

    Public Property MasterId() As Integer
        Get
            Return _MasterId
        End Get
        Set(ByVal value As Integer)
            _MasterId = value
        End Set
    End Property
    Public Property ArticleDefId() As Integer
        Get
            Return _ArticleDefId
        End Get
        Set(ByVal value As Integer)
            _ArticleDefId = value
        End Set
    End Property
    Public Property VendorId() As Integer
        Get
            Return _VendorId
        End Get
        Set(ByVal value As Integer)
            _VendorId = value
        End Set
    End Property
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Public Property DateLog() As DateTime
        Get
            Return _DateLog
        End Get
        Set(ByVal value As DateTime)
            _DateLog = value
        End Set
    End Property
End Class
