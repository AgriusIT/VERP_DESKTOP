''TASK TFS2170 done by Muhammad Ameen on 20-01-2018. Addition of two new filed ProductionStageId and LabourTypeId

Public Class DailySalarydetail
    Private _DailySalariesDetailId As Integer
    Public Property DailySalariesDetailId() As Integer
        Get
            Return _DailySalariesDetailId
        End Get
        Set(ByVal value As Integer)
            _DailySalariesDetailId = value
        End Set
    End Property

    Private _DailySalariesId As Integer
    Public Property DailySalariesId() As Integer
        Get
            Return _DailySalariesId
        End Get
        Set(ByVal value As Integer)
            _DailySalariesId = value
        End Set
    End Property

    Private _EmployId As Integer
    Public Property EmployId() As Integer
        Get
            Return _EmployId
        End Get
        Set(ByVal value As Integer)
            _EmployId = value
        End Set
    End Property

    Private _CostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return _CostCenterId
        End Get
        Set(ByVal value As Integer)
            _CostCenterId = value
        End Set
    End Property

    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

    Private _DailyWage As String
    Public Property DailyWage() As String
        Get
            Return _DailyWage
        End Get
        Set(ByVal value As String)
            _DailyWage = value
        End Set
    End Property

    Private _Salary As Double
    Public Property Salary() As Double
        Get
            Return _Salary
        End Get
        Set(ByVal value As Double)
            _Salary = value
        End Set
    End Property

    Private _WorkingTime As Double
    Public Property WorkingTime() As Double
        Get
            Return _WorkingTime
        End Get
        Set(ByVal value As Double)
            _WorkingTime = value
        End Set
    End Property

    Public Property ProductionStepId As Integer
    Public Property LabourTypeId As Integer


End Class





