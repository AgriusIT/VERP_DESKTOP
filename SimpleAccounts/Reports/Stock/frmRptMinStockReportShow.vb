Imports SBDal
Public Class frmRptMinStockReportShow
    Public Function GetNotificationStock() As DataTable
        Try
            Dim str As String = "SELECT dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, " _
           & " dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleUnitName, ISNULL(Stock.Qty, 0) " _
           & " AS ClosingQty FROM dbo.ArticleDefView LEFT OUTER JOIN " _
           & " (SELECT ArticleDefId, Round(SUM(ISNULL(InQty, 0) - ISNULL(OutQty, 0)),0) AS Qty " _
           & " FROM StockDetailTable GROUP BY ArticleDefId) Stock ON Stock.ArticleDefId = dbo.ArticleDefView.ArticleId " _
           & " (WHERE ArticleDefView.StockLevel < 0  " _
           & " ORDER BY dbo.ArticleDefView.SortOrder "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmRptMinStockReportShow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.grd.DataSource = GetNotificationStock()
            Me.grd.RetrieveStructure()

            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                col.Visible = False
            Next
            Me.grd.RootTable.Columns("ArticleCode").Visible = True
            Me.grd.RootTable.Columns("ArticleDescription").Visible = True
            Me.grd.RootTable.Columns("ClosingQty").Visible = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class