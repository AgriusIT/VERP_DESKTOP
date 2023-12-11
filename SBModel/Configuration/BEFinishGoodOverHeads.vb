Public Class BEFinishGoodOverHeads
    '   [Id] [int] IDENTITY(1,1) NOT NULL,
    '[FinishGoodId] [int] NULL,
    '[ProductionStepId] [int] NULL,
    '[AccountId] [int] NULL,
    '[Amount] [float] NULL,
    '[Remarks] [nvarchar](300) NULL,
    Public Property Id As Integer
    Public Property FinishGoodId As Integer
    Public Property ProductionStepId As Integer
    Public Property AccountId As Integer
    Public Property Amount As Double
    Public Property Remarks As String
End Class
