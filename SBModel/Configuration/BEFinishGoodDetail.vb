Public Class BEFinishGoodDetail
    Public Property Id As Integer
    Public Property FinishGoodId As Integer
    Public Property MaterialArticleId As Integer
    Public Property DetailArticleId As Integer
    Public Property PackingId As Integer
    Public Property Qty As Double
    Public Property ArticleSize As String
    Public Property Category As String
    Public Property Tax_Percent As Double
    Public Property Tax_Amount As Double
    Public Property Tax_Apply As String
    Public Property Remarks As String
    Public Property CostPrice As Double
    Public Property ItemSpecificationId As Integer
    Public Property SubDepartmentId As Integer
    Public Property Percentage_Con As Double
    Public Property TotalQty As Double
    ''TASK TFS3581
    Public Property PackQty As Double
    ''END TASK TFS3581

End Class
