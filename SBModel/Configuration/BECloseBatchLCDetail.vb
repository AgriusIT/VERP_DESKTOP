Public Class BECloseBatchLCDetail
    '   [CloseBatchLCDetailId] [int] IDENTITY(1,1) NOT NULL,
    '[CloseBatchId] [int] NULL,
    '[LabourTypeId] [int] NULL,
    '[Amount] [float] NULL,
    Public Property CloseBatchLCDetailId As Integer
    Public Property CloseBatchId As Integer
    Public Property LabourTypeId As Integer
    Public Property Amount As Double
End Class
