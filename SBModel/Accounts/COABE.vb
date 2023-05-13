Public Class COABE

    Private _coa_main_id As Integer
    Public Property coa_main_id() As Integer
        Get
            Return _coa_main_id
        End Get
        Set(ByVal value As Integer)
            _coa_main_id = value
        End Set
    End Property

    Private _main_sub_id As Integer
    Public Property main_sub_id() As Integer
        Get
            Return _main_sub_id
        End Get
        Set(ByVal value As Integer)
            _main_sub_id = value
        End Set
    End Property

    Private _main_sub_sub_id As Integer
    Public Property main_sub_sub_id() As Integer
        Get
            Return _main_sub_sub_id
        End Get
        Set(ByVal value As Integer)
            _main_sub_sub_id = value
        End Set
    End Property

    Private _coa_detail_id As Integer
    Public Property coa_detail_id() As Integer
        Get
            Return _coa_detail_id
        End Get
        Set(ByVal value As Integer)
            _coa_detail_id = value
        End Set
    End Property

    Private _detail_code As String
    Public Property detail_code() As String
        Get
            Return _detail_code
        End Get
        Set(ByVal value As String)
            _detail_code = value
        End Set
    End Property

    Private _detail_title As String
    Public Property detail_title() As String
        Get
            Return _detail_title
        End Get
        Set(ByVal value As String)
            _detail_title = value
        End Set
    End Property

    Private _account_type As String
    Public Property account_type() As String
        Get
            Return _account_type
        End Get
        Set(ByVal value As String)
            _account_type = value
        End Set
    End Property

    Private _DrBS_Note_Id As Integer
    Public Property DrBS_Note_Id() As Integer
        Get
            Return _DrBS_Note_Id
        End Get
        Set(ByVal value As Integer)
            _DrBS_Note_Id = value
        End Set
    End Property

    Private _CrBS_Note_Id As Integer
    Public Property CrBS_Note_Id() As Integer
        Get
            Return _CrBS_Note_Id
        End Get
        Set(ByVal value As Integer)
            _CrBS_Note_Id = value
        End Set
    End Property

    Private _PL_note_id As Integer
    Public Property PL_note_id() As Integer
        Get
            Return _PL_note_id
        End Get
        Set(ByVal value As Integer)
            _PL_note_id = value
        End Set
    End Property

    Private _DrBS_Note_Title As String
    Public Property DrBS_Note_Title() As String
        Get
            Return _DrBS_Note_Title
        End Get
        Set(ByVal value As String)
            _DrBS_Note_Title = value
        End Set
    End Property

    Private _CrBS_Note_Title As String
    Public Property CrBS_Note_Title() As String
        Get
            Return _CrBS_Note_Title
        End Get
        Set(ByVal value As String)
            _CrBS_Note_Title = value
        End Set
    End Property

    Private _PL_Note_Title As String
    Public Property PL_Note_Title() As String
        Get
            Return _PL_Note_Title
        End Get
        Set(ByVal value As String)
            _PL_Note_Title = value
        End Set
    End Property

    Private _note_type As String
    Public Property note_type() As String
        Get
            Return _note_type
        End Get
        Set(ByVal value As String)
            _note_type = value
        End Set
    End Property

    Private _Mobile As String
    Public Property Mobile() As String
        Get
            Return _Mobile
        End Get
        Set(ByVal value As String)
            _Mobile = value
        End Set
    End Property

    Private _Email As String
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property



End Class
