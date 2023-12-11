Public Class LeaveAdjustmentBE
    Public Property AdjustId As Integer
    Public Property AdjustNo As String
    Public Property AdjustDate As DateTime
    Public Property Remarks As String
    Public Property Detail As List(Of LeaveAdjustmentDetailBE)
End Class

Public Class LeaveAdjustmentDetailBE
    Public Property AdjustDetailId As Integer
    Public Property EmployeeId As Integer
    Public Property AdjustDays As Double
    Public Property LeaveTypeId As Integer
    Public Property ReasonId As Integer
    Public Property Comments As String
End Class