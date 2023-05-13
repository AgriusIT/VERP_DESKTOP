''24-Feb-2014 TASK:M22 Imran Ali Leave Wise Salary Calculation
Public Class PayRollDivisionBE


    Private _PayRollDivision_Id As Integer
    Public Property PayRollDivision_Id() As Integer
        Get
            Return _PayRollDivision_Id
        End Get
        Set(ByVal value As Integer)
            _PayRollDivision_Id = value
        End Set
    End Property


    Private _Division_Id As Integer
    Public Property Division_Id() As Integer
        Get
            Return _Division_Id
        End Get
        Set(ByVal value As Integer)
            _Division_Id = value
        End Set
    End Property


    Private _PayRollDivisionName As String
    Public Property PayRollDivisionName() As String
        Get
            Return _PayRollDivisionName
        End Get
        Set(ByVal value As String)
            _PayRollDivisionName = value
        End Set
    End Property


    Private _Level As Integer
    Public Property [Level]() As Integer
        Get
            Return _Level
        End Get
        Set(ByVal value As Integer)
            _Level = value
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


    Private _Sort_Order As Integer
    Public Property Sort_Order() As Integer
        Get
            Return _Sort_Order
        End Get
        Set(ByVal value As Integer)
            _Sort_Order = value
        End Set
    End Property
    'Task:M22 Added Alowed Leave Field
    Private _AnnualAllowedLeave As Integer
    Public Property AnnualAllowedLeave() As Integer
        Get
            Return _AnnualAllowedLeave
        End Get
        Set(ByVal value As Integer)
            _AnnualAllowedLeave = value
        End Set
    End Property
    'Task:M22

End Class
