''TaskM126121 12/6/2015 Imran Ali Config Based Deduction Against Salary 
'Task# 201507004 Ali Ansari Add Leave Deduction in Salary Type
'2015-08-06 Task# 201508004 Ali Ansari Add Income Tax Excemption and Income Tax Deduction
''21-9-2015 Task:219151 Imran Ali: Enhancement Apply Value And Existing Account.
Public Class SalaryType

    Private _SalaryExpTypeId As Integer
    Public Property SalaryExpTypeId() As Integer
        Get
            Return _SalaryExpTypeId
        End Get
        Set(ByVal value As Integer)
            _SalaryExpTypeId = value
        End Set
    End Property

    Private _SalaryExpType As String
    Public Property SalaryExpType() As String
        Get
            Return _SalaryExpType
        End Get
        Set(ByVal value As String)
            _SalaryExpType = value
        End Set
    End Property

    Private _Deduction As Boolean
    Public Property Deduction() As Boolean
        Get
            Return _Deduction
        End Get
        Set(ByVal value As Boolean)
            _Deduction = value
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
    Public Property SortOrder() As Integer
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Integer)
            _SortOrder = value
        End Set
    End Property

    Private _AccountId As Integer
    Public Property AccountId() As Integer
        Get
            Return _AccountId
        End Get
        Set(ByVal value As Integer)
            _AccountId = value
        End Set
    End Property

    Private _Advance As Boolean
    Public Property Advance() As Boolean
        Get
            Return _Advance
        End Get
        Set(ByVal value As Boolean)
            _Advance = value
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

    Private _sub_sub_code As String
    Public Property sub_sub_code() As String
        Get
            Return _sub_sub_code
        End Get
        Set(ByVal value As String)
            _sub_sub_code = value
        End Set
    End Property

    Private _GrossSalaryType As Boolean
    Public Property GrossSalaryType() As Boolean
        Get
            Return _GrossSalaryType
        End Get
        Set(ByVal value As Boolean)
            _GrossSalaryType = value
        End Set
    End Property
    ''TaskM126121  Add Property Deduction Against Salary
    Private _DeductionAgainstSalary As Boolean
    Public Property DeductionAgainstSalary() As Boolean
        Get
            Return _DeductionAgainstSalary
        End Get
        Set(ByVal value As Boolean)
            _DeductionAgainstSalary = value
        End Set
    End Property
    'End Task ''TaskM126121 

    'Task #201507004  Add Property Deduction Against Leaves Ali Ansari
    Private _DeductionAgainstLeaves As Boolean
    Public Property DeductionAgainstLeaves() As Boolean
        Get
            Return _DeductionAgainstLeaves
        End Get
        Set(ByVal value As Boolean)
            _DeductionAgainstLeaves = value
        End Set
    End Property
    'Task #201507004  Add Property Deduction Against Leaves Ali Ansari

    'Task #201507004  Add Property Allowances Against Leaves Ali Ansari
    Private _AllowanceAgainstOT As Boolean
    Public Property AllowanceAgainstOT() As Boolean
        Get
            Return _AllowanceAgainstOT
        End Get
        Set(ByVal value As Boolean)
            _AllowanceAgainstOT = value
        End Set
    End Property
    'Task #201507004  Add Property Allowances Against Leaves Ali Ansari
    'Task #201508004 Ali Ansari  Add Property  Income Tax Deduction and Income Tax Excemption
    Private _IncomeTaxDeduction As Boolean
    Public Property IncomeTaxDeduction() As Boolean
        Get
            Return _IncomeTaxDeduction
        End Get
        Set(ByVal value As Boolean)
            _IncomeTaxDeduction = value
        End Set
    End Property
    Private _IncomeTaxExcemption As Boolean
    Public Property IncomeTaxExcemption() As Boolean
        Get
            Return _IncomeTaxExcemption
        End Get
        Set(ByVal value As Boolean)
            _IncomeTaxExcemption = value
        End Set
    End Property
    'Task #201508004 Ali Ansari  Add Property  Income Tax Deduction and Income Tax Excemption
    'TASK219151 Added Field ApplyValue And ExistingAccount
    Private _ApplyValue As String = String.Empty

    Public Property ApplyValue() As String
        Get
            Return _ApplyValue
        End Get
        Set(ByVal value As String)
            _ApplyValue = value
        End Set
    End Property
    Private _ExistingAccount As Boolean = False

    Public Property ExistingAccount() As Boolean
        Get
            Return _ExistingAccount
        End Get
        Set(ByVal value As Boolean)
            _ExistingAccount = value
        End Set
    End Property
    Private _SiteVisitAllowance As Boolean = False
    Public Property SiteVisitAllowance() As Boolean
        Get
            Return _SiteVisitAllowance
        End Get
        Set(ByVal value As Boolean)
            _SiteVisitAllowance = value
        End Set
    End Property
    'END TASK219151
End Class
