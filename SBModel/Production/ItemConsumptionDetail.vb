''TASK:908 on 13-06-2017. Done by Ameen
''TAKS: 1009 Items on consumption should be loaded ticket, tag and department wise from Estimation
Public Class ItemConsumptionDetail
    Public Property ConsumptionDetailId As Integer
    Public Property ConsumptionId As Integer
    Public Property ArticleId As Integer
    Public Property Qty As Double
    Public Property Rate As Double
    Public Property DispatchId As Integer
    Public Property DispatchDetailId As Integer
    Public Property CGSAccountId As Integer
    Public Property LocationId As Integer
    Public Property Comments As String
    ''TASK:1009
    Public Property EstimationId As Integer
    Public Property ParentTagNo As Integer
    Public Property EstimatedQty As Integer
    Public Property DepartmentId As Integer
    Public Property TicketId As Integer

    '' END TASK:1009
End Class
