Public Class ActivityLog

    Private _ID As Integer
    Private _ApplicationName As String = String.Empty
    Private _FormCaption As String = String.Empty
    Private _ActivityName As String = String.Empty
    Private _UserID As Integer
    Private _RecordType As String = String.Empty
    Private _RefNo As String = String.Empty
    Private _LogDateTime As Date
    Private _LogComments As String = String.Empty
    Private _FormName As String = String.Empty

    Public Property LogDateTime() As Date
        Get
            Return Me._LogDateTime
        End Get
        Set(ByVal value As Date)
            Me._LogDateTime = value
        End Set
    End Property

    Public Property RefNo() As String
        Get
            Return Me._RefNo
        End Get
        Set(ByVal value As String)
            Me._RefNo = value
        End Set
    End Property

    Public Property RecordType() As String
        Get
            Return Me._RecordType
        End Get
        Set(ByVal value As String)
            Me._RecordType = value
        End Set
    End Property

    Public Property UserID() As Integer
        Get
            Return Me._UserID
        End Get
        Set(ByVal value As Integer)
            Me._UserID = value
        End Set
    End Property

    Private _User_Name As String
    Public Property User_Name() As String
        Get
            Return _User_Name
        End Get
        Set(ByVal value As String)
            _User_Name = value
        End Set
    End Property

    Public Property ActivityName() As String
        Get
            Return Me._ActivityName
        End Get
        Set(ByVal value As String)
            Me._ActivityName = value
        End Set
    End Property

    Public Property FormCaption() As String
        Get
            Return Me._FormCaption
        End Get
        Set(ByVal value As String)
            Me._FormCaption = value
        End Set
    End Property

    Public Property ApplicationName() As String
        Get
            Return Me._ApplicationName
        End Get
        Set(ByVal value As String)
            Me._ApplicationName = value
        End Set
    End Property

    Public Property ID() As Integer
        Get
            Return Me._ID
        End Get
        Set(ByVal value As Integer)
            Me._ID = value
        End Set
    End Property
    Public Property LogComments() As String
        Get
            Return _LogComments
        End Get
        Set(ByVal value As String)
            _LogComments = value
        End Set
    End Property
    Private _Source As String = String.Empty
    Public Property Source() As String
        Get
            Return _Source
        End Get
        Set(ByVal value As String)
            _Source = value
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

End Class

