Public Class EmployeeFineDeductionBE
    Public Property FineId As Integer
    Public Property DocNo As String
    Public Property DocDate As DateTime
    Public Property Detail As List(Of EmployeeFineDeductionDetailBE)
End Class
Public Class EmployeeFineDeductionDetailBE
    Public Property FineDetailId As Integer
    Public Property EmployeeId As Integer
    Public Property DeductionId As Integer
    Public Property Amount As Double
    Public Property Reason As String
End Class
