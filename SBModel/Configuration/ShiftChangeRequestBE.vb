Public Class ShiftChangeRequestBE
    Public Property RequestId As Integer
    Public Property DocNo As String
    Public Property DocDate As DateTime
    Public Property RequestTypeId As Integer
    Public Property Remarks As String
    Public Property Detail As List(Of ShiftChangeRequestDetailBE)
End Class
Public Class ShiftChangeRequestDetailBE
    Public Property RequestDetailId As Integer
    Public Property EmployeeId As Integer
    Public Property CurrentShifId As Integer
    Public Property NewShiftId As Integer
    Public Property CurrentCostCenterId As Integer
    Public Property NewCostCenterId As Integer
    Public Property Comments As String
End Class
