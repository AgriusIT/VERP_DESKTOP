Public Class BEFinishGood
    '[Id] [int] IDENTITY(1,1) NOT NULL,
    '	[StandardNo] [nvarchar](100) NULL,
    '	[StandardName] [nvarchar](100) NULL,
    '	[Version] [int] NULL,
    '	[MasterArticleId] [int] NULL,
    '	[BatchSize] [Float] NULL,
    '	[Default] [bit] NULL,
    Public Property Id As Integer
    Public Property StandardNo As String
    Public Property StandardName As String
    Public Property MasterArticleId As Integer
    Public Property Version As Integer
    Public Property BatchSize As Double
    Public Property Default1 As Boolean
    Public Property Detail As List(Of BEFinishGoodDetail)
    Public Property OverHeadsList As List(Of BEFinishGoodOverHeads)
    Public Property ByProductList As List(Of BEFinishGoodByProducts)
    Public Property LabourAllocationList As List(Of BEFinishGoodLabourAllocation)
    Sub New()
        Detail = New List(Of BEFinishGoodDetail)
        OverHeadsList = New List(Of BEFinishGoodOverHeads)
        ByProductList = New List(Of BEFinishGoodByProducts)
        LabourAllocationList = New List(Of BEFinishGoodLabourAllocation)
    End Sub
End Class
