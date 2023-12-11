

Public Class MaterialAnalysisMaster
    Public Property MaterialAnalysisMasterID As Integer
    Public Property TicketQty As Decimal
    Public Property ProductID As Integer
    Public Property CustomerID As Integer
    Public Property SaleOrderID As Integer
    Public Property PlanMasterID As Integer
    Public Property TicketID As Integer
    Public Property EstimationMasterID As Integer
    Public Property DocNo As String
    Public Property MDate As DateTime
    Public Property Remarks As String
    Public Property MatAnalysisDetailList As List(Of MaterialAnalysisDetail)
    Public Sub New()
        MatAnalysisDetailList = New List(Of MaterialAnalysisDetail)
    End Sub
End Class
