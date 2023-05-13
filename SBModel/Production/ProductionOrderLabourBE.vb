Public Class ProductionOrderLabourBE
    Public Property ID As Integer
    Public Property ProductionOrderId As Integer

    Public Property LabourTypeId As Integer
    Public Property Amount As Double
    Public Property ActivityLog() As ActivityLog
End Class
