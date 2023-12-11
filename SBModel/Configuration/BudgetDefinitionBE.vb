Public Class BudgetDefinitionBE
    Public Property AccountBudgetMasterId As Integer
    Public Property Title As String
    Public Property CostCenterId As Integer
    Public Property FromDate As DateTime
    Public Property ToDate As DateTime
    Public Property Amount As Double
    Public Property CurrencyId As Integer
    Public Property Remarks As String
    Public Property Details As List(Of BudgetDefinitionDetailBE)
End Class

Public Class BudgetDefinitionDetailBE
    Public Property AccountBudgetDetailId As Integer
    Public Property AccountId As Integer
    Public Property AccountLevel As Integer
    Public Property AmountRequiredAtAccountLevel As Boolean
    Public Property BudgetAmount As Double
    Public Property Comments As String
    Public Property CategoryId As Integer
End Class