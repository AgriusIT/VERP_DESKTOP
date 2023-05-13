Public Class LeaveEncashmentBE
    Private _LeaveEncashmentId As Integer
    Private _LeaveEncashment As String
    Private _WorkingDays As String
    Public Property LeaveEncashmentId() As Integer
        Get
            Return _LeaveEncashmentId
        End Get
        Set(ByVal value As Integer)
            _LeaveEncashmentId = value
        End Set
    End Property
    Public Property LeaveEncashment() As Integer
        Get
            Return _LeaveEncashment
        End Get
        Set(ByVal value As Integer)
            _LeaveEncashment = value
        End Set
    End Property

    Public Property TotalWorkingDays() As Integer
        Get
            Return _WorkingDays
        End Get
        Set(ByVal value As Integer)
            _WorkingDays = value
        End Set
    End Property
End Class
