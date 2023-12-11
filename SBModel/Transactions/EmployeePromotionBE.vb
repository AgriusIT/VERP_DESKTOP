Public Class EmployeePromotionBE


    Private _PromotionId As Integer
    Public Property PromotionId() As Integer
        Get
            Return _PromotionId
        End Get
        Set(ByVal value As Integer)
            _PromotionId = value
        End Set
    End Property


    Private _PromotionType As String
    Public Property PromotionType() As String
        Get
            Return _PromotionType
        End Get
        Set(ByVal value As String)
            _PromotionType = value
        End Set
    End Property


    Private _Ref_No As String
    Public Property Ref_No() As String
        Get
            Return _Ref_No
        End Get
        Set(ByVal value As String)
            _Ref_No = value
        End Set
    End Property


    Private _Ref_Date As Date
    Public Property Ref_Date() As Date
        Get
            Return _Ref_Date
        End Get
        Set(ByVal value As Date)
            _Ref_Date = value
        End Set
    End Property


    Private _EmployeeId As Integer
    Public Property EmployeeId() As Integer
        Get
            Return _EmployeeId
        End Get
        Set(ByVal value As Integer)
            _EmployeeId = value
        End Set
    End Property


    Private _OldDepartmentId As Integer
    Public Property OldDepartmentId() As Integer
        Get
            Return _OldDepartmentId
        End Get
        Set(ByVal value As Integer)
            _OldDepartmentId = value
        End Set
    End Property


    Private _OldDivisionId As Integer
    Public Property OldDivisionId() As Integer
        Get
            Return _OldDivisionId
        End Get
        Set(ByVal value As Integer)
            _OldDivisionId = value
        End Set
    End Property


    Private _OldDesignationId As Integer
    Public Property OldDesignationId() As Integer
        Get
            Return _OldDesignationId
        End Get
        Set(ByVal value As Integer)
            _OldDesignationId = value
        End Set
    End Property


    Private _DepartmentId As Integer
    Public Property DepartmentId() As Integer
        Get
            Return _DepartmentId
        End Get
        Set(ByVal value As Integer)
            _DepartmentId = value
        End Set
    End Property


    Private _DivisionId As Integer
    Public Property DivisionId() As Integer
        Get
            Return _DivisionId
        End Get
        Set(ByVal value As Integer)
            _DivisionId = value
        End Set
    End Property


    Private _DesignationId As Integer
    Public Property DesignationId() As Integer
        Get
            Return _DesignationId
        End Get
        Set(ByVal value As Integer)
            _DesignationId = value
        End Set
    End Property


    Private _Increament_Salary As Double
    Public Property Increament_Salary() As Double
        Get
            Return _Increament_Salary
        End Get
        Set(ByVal value As Double)
            _Increament_Salary = value
        End Set
    End Property


    Private _NewBasicSalary As Double
    Public Property NewBasicSalary() As Double
        Get
            Return _NewBasicSalary
        End Get
        Set(ByVal value As Double)
            _NewBasicSalary = value
        End Set
    End Property


    Private _BasicSalary As Double
    Public Property BasicSalary() As Double
        Get
            Return _BasicSalary
        End Get
        Set(ByVal value As Double)
            _BasicSalary = value
        End Set
    End Property


    Private _Status As Boolean
    Public Property Status() As Boolean
        Get
            Return _Status
        End Get
        Set(ByVal value As Boolean)
            _Status = value
        End Set
    End Property


    Private _EntryDate As Date
    Public Property EntryDate() As Date
        Get
            Return _EntryDate
        End Get
        Set(ByVal value As Date)
            _EntryDate = value
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


    Private _Old_EmployeeId As Integer
    Public Property Old_EmployeeId() As Integer
        Get
            Return _Old_EmployeeId
        End Get
        Set(ByVal value As Integer)
            _Old_EmployeeId = value
        End Set
    End Property


    Private _Old_BasicSalary As Double
    Public Property Old_BasicSalary() As Double
        Get
            Return _Old_BasicSalary
        End Get
        Set(ByVal value As Double)
            _Old_BasicSalary = value
        End Set
    End Property


    Private _OldDepartmentName As String
    Public Property OldDepartmentName() As String
        Get
            Return _OldDepartmentName
        End Get
        Set(ByVal value As String)
            _OldDepartmentName = value
        End Set
    End Property


    Private _OldDivisionName As String
    Public Property OldDivisionName() As String
        Get
            Return _OldDivisionName
        End Get
        Set(ByVal value As String)
            _OldDivisionName = value
        End Set
    End Property


    Private _OldDesignationName As String
    Public Property OldDesignationName() As String
        Get
            Return _OldDesignationName
        End Get
        Set(ByVal value As String)
            _OldDesignationName = value
        End Set
    End Property



End Class
