Public Class AdjustmentTypeBE

    Private _AdjTypeId As Integer
    Public Property AdjTypeId() As Integer
        Get
            Return _AdjTypeId
        End Get
        Set(ByVal value As Integer)
            _AdjTypeId = value
        End Set
    End Property

    Private _AdjTypeName As String
    Public Property AdjTypeName() As String
        Get
            Return _AdjTypeName
        End Get
        Set(ByVal value As String)
            _AdjTypeName = value
        End Set
    End Property

    Private _Sort_Order As Integer
    Public Property Sort_Order() As Integer
        Get
            Return _Sort_Order
        End Get
        Set(ByVal value As Integer)
            _Sort_Order = value
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

    Private _AdjustmentInShort As Boolean
    Public Property AdjustmentInShort() As Boolean
        Get
            Return _AdjustmentInShort
        End Get
        Set(ByVal value As Boolean)
            _AdjustmentInShort = value
        End Set
    End Property


End Class
