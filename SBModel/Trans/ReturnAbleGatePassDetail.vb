Public Class ReturnAbleGatePassDetail

    Private _IssueDetailID As Integer
    Private _Issue_id As Integer
    Private _IssueDetail As String
    Private _ReceivedDate As DateTime

    Public Property IssueDetailID() As Integer

        Get
            Return _IssueDetailID
        End Get
        Set(ByVal value As Integer)
            _IssueDetailID = value
        End Set
    End Property
    Public Property Issue_id() As Integer
        Get
            Return _Issue_id

        End Get
        Set(ByVal value As Integer)
            _Issue_id = value

        End Set
    End Property
    Public Property IssueDetail() As String
        Get
            Return _IssueDetail
        End Get
        Set(ByVal value As String)
            _IssueDetail = value
        End Set
    End Property
    Public Property RecivingDate() As DateTime
        Get
            Return _ReceivedDate
        End Get
        Set(ByVal value As DateTime)
            _ReceivedDate = value

        End Set
    End Property

    Private _IssueQty As Double
    Public Property IssueQty() As Double
        Get
            Return _IssueQty
        End Get
        Set(ByVal value As Double)
            _IssueQty = value
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
    Private _Comments As String
    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
        End Set
    End Property



End Class
