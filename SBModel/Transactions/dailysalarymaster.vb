Public Class dailysalarymaster
    Private _DailySalariesId As Integer
    Public Property DailySalariesId() As Integer
        Get
            Return _DailySalariesId
        End Get
        Set(ByVal value As Integer)
            _DailySalariesId = value
        End Set
    End Property

    Private _DcNo As String
    Public Property DcNo() As String
        Get
            Return _DcNo
        End Get
        Set(ByVal value As String)
            _DcNo = value
        End Set


    End Property
    Private _DcDate As DateTime
    Public Property DcDate() As DateTime
        Get
            Return _DcDate
        End Get
        Set(ByVal value As DateTime)
            _DcDate = value
        End Set

    End Property
    Private _Amount As Double
    Public Property Amount() As Double
        Get
            Return _Amount
        End Get
        Set(ByVal value As Double)
            _Amount = value
        End Set
    End Property


    Private _Reference As String
    Public Property Reference() As String
        Get
            Return _Reference
        End Get
        Set(ByVal value As String)
            _Reference = value
        End Set
    End Property

    Private _posted As Boolean
    Public Property posted() As Boolean
        Get
            Return _posted
        End Get
        Set(ByVal value As Boolean)
            _posted = value
        End Set
    End Property

    Private _UserName As String
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property

    Private _EntryDate As DateTime
    Public Property EntryDate() As DateTime
        Get
            Return _EntryDate
        End Get
        Set(ByVal value As DateTime)
            _EntryDate = value
        End Set
    End Property
    Private _DailySalaryDetail As List(Of DailySalarydetail)
    Public Property DailySalaryDetail() As List(Of DailySalarydetail)
        Get
            Return _DailySalaryDetail
        End Get
        Set(ByVal value As List(Of DailySalarydetail))
            _DailySalaryDetail = value
        End Set
    End Property



End Class
