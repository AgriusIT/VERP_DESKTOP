Public Class ReturnAbleGatePassMaster
    Private _Issue_id As Integer
    Private _Issue_No As String
    Private _Issue_date As DateTime
    Private _Issue_to As String
    Private _Remarks As String
    Private _Username As String
    Private _Fdate As DateTime
    Private _ReturnableGatePassDetail As List(Of ReturnAbleGatePassDetail)
    Public Property Issue_id() As Integer
        Get
            Return _Issue_id
        End Get
        Set(ByVal value As Integer)
            _Issue_id = value

        End Set
    End Property
    Public Property Issue_No() As String
        Get
            Return _Issue_No
        End Get
        Set(ByVal value As String)
            _Issue_No = value
        End Set
    End Property
    Public Property Issue_date() As DateTime
        Get
            Return _Issue_date
        End Get
        Set(ByVal value As DateTime)
            _Issue_date = value
        End Set
    End Property
    Public Property Issue_to() As String
        Get
            Return _Issue_to
        End Get
        Set(ByVal value As String)
            _Issue_to = value

        End Set
    End Property
    Public Property Remarks() As String
        Get
            Return _Remarks

        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property
    Public Property Username() As String
        Get
            Return _Username

        End Get
        Set(ByVal value As String)
            _Username = value

        End Set
    End Property
    Public Property Fdate() As DateTime
        Get
            Return _Fdate
        End Get
        Set(ByVal value As DateTime)
            _Fdate = value
        End Set
    End Property
    Public Property ReturnableGatePassDetail() As List(Of ReturnAbleGatePassDetail)
        Get
            Return _ReturnableGatePassDetail
        End Get
        Set(ByVal value As List(Of ReturnAbleGatePassDetail))
            _ReturnableGatePassDetail = value
        End Set
    End Property
    ''TASK TFS1556
    Public Property DriverName As String
    Public Property VehicleNo As String
    ''End TASK TFS1557
End Class
