Public Class ItemViewModel
    'ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ISNULL(SalePrice,0) as Price, Isnull(PurchasePrice,0) as PurchasePrice, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand],IsNull(ArticleDefView.Cost_Price,0) as [Cost Price] FROM ArticleDefView where Active=1 AND ArticleDefView.SalesItem=1"
    Public Property ID As Integer
    Public Property Code As String
    Public Property Item As String
    Public Property Size As String
    Public Property Combination As String  ''ArticleColorName
    Public Property SalePrice As Decimal
    Public Property PurchasePrice As Decimal
    Public Property SortOrder As Integer
    Public Property GroupName As String
    Public Property Type As String
    Public Property Gender As String
    Public Property PO As String
    Public Property CostPrice As Decimal
End Class
