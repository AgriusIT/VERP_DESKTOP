Public Class frmCurrentStock
    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub New(ByVal ArticleId As Integer, Optional ByVal LocationId As Integer = 0, Optional ByVal ArticleName As String = "")
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Text = ArticleName
        FillGrid(ArticleId)
    End Sub
    Public Sub FillGrid(ByVal ArticleId As Integer, Optional ByVal LocationId As Integer = 0)
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT     dbo.StockDetailTable.LocationId, dbo.tblDefLocation.location_name, SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) " _
                     & " AS Stock FROM dbo.StockDetailTable INNER JOIN dbo.tblDefLocation ON dbo.StockDetailTable.LocationId = dbo.tblDefLocation.location_id WHERE ArticleDefID=" & ArticleId & " " & IIf(LocationId > 0, " AND dbo.StockDetailTable.LocationId=" & LocationId & "", "") & " GROUP BY dbo.StockDetailTable.LocationId, dbo.tblDefLocation.location_name "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            grd.RetrieveStructure()
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns("Stock").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Stock").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Stock").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(1).Caption = "Location"
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class