Public Class SizeBE

    Private _SizeId As Integer
    Public Property SizeId() As Integer
        Get
            Return _SizeId
        End Get
        Set(ByVal value As Integer)
            _SizeId = value
        End Set
    End Property

    Private _SizeName As String
    Public Property SizeName() As String
        Get
            Return _SizeName
        End Get
        Set(ByVal value As String)
            _SizeName = value
        End Set
    End Property

    Private _SizeCode As String
    Public Property SizeCode() As String
        Get
            Return _SizeCode
        End Get
        Set(ByVal value As String)
            _SizeCode = value
        End Set
    End Property
    Private _ActivityLog As ActivityLog
    Public Property ActivityLog() As ActivityLog
        Get
            Return Me._ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            Me._ActivityLog = value
        End Set
    End Property
End Class
