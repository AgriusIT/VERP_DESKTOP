Public Class RelatedItem
    '[RelationId] [int] IDENTITY(1,1) NOT NULL,
    '[ArticleId] [int] NOT NULL,
    '[RelatedArticleId] [int] NOT NULL,
    Public Property RelationId As Integer
    Public Property ArticleId As Integer
    Public Property RelatedArticleId As Integer
    Public Property RowState As String
End Class
