Public Class BECloseBatchDetail
    '   [ID] [int] IDENTITY(1,1) NOT NULL,
    '[CloseBatchId] [int] NULL,
    '[ArticleId] [int] NULL,
    '[Quantity] [float] NULL,
    '[PackingId] [int] NULL,
    '[LocationId] [int] NULL,
    Public Property ID As Integer
    Public Property CloseBatchId As Integer
    Public Property ArticleId As Integer
    Public Property Quantity As Double
    Public Property PackingId As Integer
    Public Property LocationId As Integer
End Class
