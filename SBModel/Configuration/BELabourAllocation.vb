Public Class BELabourAllocation
    '   [LabourAllocationId] [int] IDENTITY(1,1) NOT NULL,
    '[ProductionStepId] [int] NULL,
    '[LabourTypeId] [int] NULL,
    '[RatePerUnit] [float] NULL,


    Public Property LabourAllocationId As Integer
    Public Property ArticleId As Integer
    Public Property ProductionStepId As Integer
    Public Property LabourTypeId As Integer
    Public Property RatePerUnit As Double
End Class
