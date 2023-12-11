Public Class AttendanceStatus

    Private _Att_Status_ID As Integer
    Public Property Att_Status_ID() As Integer
        Get
            Return _Att_Status_ID
        End Get
        Set(ByVal value As Integer)
            _Att_Status_ID = value
        End Set
    End Property
    Private _Att_Status_Code As String
    Public Property Att_Status_Code() As String
        Get
            Return _Att_Status_Code
        End Get
        Set(ByVal value As String)
            _Att_Status_Code = value
        End Set
    End Property
    Private _Att_Status_Name As String
    Public Property Att_Status_Name() As String
        Get
            Return _Att_Status_Name
        End Get
        Set(ByVal value As String)
            _Att_Status_Name = value
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
    Private _SortOrder As Integer
    Public Property SortORder() As Integer
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Integer)
            _SortOrder = value
        End Set
    End Property


End Class
