Public Class MaterialDecompositionModel
    Public Property DecompositionId As Integer
    Public Property DecompositionDate As DateTime
    Public Property DocumentNo As String
    Public Property Remarks As String
    Public Property CustomerId As Integer
    Public Property SalesOrderId As Integer
    Public Property PlanId As Integer
    Public Property TicketId As Integer
    Public Property EstimationId As Integer
    Public StockMaster As StockMaster
    Sub New()
        StockMaster = New StockMaster()
    End Sub
End Class
