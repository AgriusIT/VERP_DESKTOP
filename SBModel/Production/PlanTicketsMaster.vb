Public Class PlanTicketsMaster
    '    PlanTicketsMasterID
    'TicketNo
    'TicketDate
    'CustomerID
    'SalesOrderID
    'PlanID
    'SpecialInstructions
    Public Property PlanTicketsMasterID As Integer
    Public Property TicketNo As String
    Public Property TicketDate As DateTime
    Public Property CustomerID As Integer
    Public Property SalesOrderID As Integer
    Public Property PlanID As Integer
    Public Property SpecialInstructions As String
    Public Property ExpiryDate As DateTime
    Public Property BatchNo As String
    Public Property MasterArticleId As Integer
    Public Property BatchSize As Double
    Public Property Detail As List(Of PlanTicketsDetail)
    Public Property StagesList As List(Of BEProductionTicketStages)
    Public Property MaterialDetail As List(Of BEPlanTicketMaterialDetail)
    Public Property FinishGoodId As Integer
    Public Property NoOfBatches As Double
    Public Property ParentTicketNo As String
    Public Property ActivityLog() As ActivityLog

    Public Sub New()
        Detail = New List(Of PlanTicketsDetail)
        StagesList = New List(Of BEProductionTicketStages)
        MaterialDetail = New List(Of BEPlanTicketMaterialDetail)
    End Sub

End Class
