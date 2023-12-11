Public Class PropertySalaryBE
    Public Property SalaryId As Integer
    Public Property SalaryDate As datetime
    Public Property SalaryMonth As Integer
    Public Property SalaryYear As Integer
    Public Property SalaryNo As String
    Public Property Remarks As String
    Public Property UserName As String
    Public Property ActivityLog() As ActivityLog
    Public Property VoucherId As Integer
    Public Property Detail As List(Of PropertySalaryDetailBE)
End Class
Public Class PropertySalaryDetailBE
    Public Property EmployeeId As Integer
    Public Property EmployeeName As String
    Public Property AccountId As Integer
    Public Property CostCenterId As Integer
    Public Property Salary As Double
    Public Property ExpenseAccountId As Integer
End Class