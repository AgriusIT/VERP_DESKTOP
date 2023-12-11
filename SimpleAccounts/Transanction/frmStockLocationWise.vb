Public Class frmStockLocationWise

    Private Sub frmStockLocationWise_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmStockLocationWise_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim str As String
            str = "SELECT dbo.StockDetailTable.LocationId, dbo.tblDefLocation.location_name, SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) AS Stock FROM dbo.StockDetailTable INNER JOIN dbo.tblDefLocation ON dbo.StockDetailTable.LocationId = dbo.tblDefLocation.location_id WHERE ArticleDefID = " & frmItemSearch.grd.CurrentRow.Cells("ArticleId").Value & "  GROUP BY dbo.StockDetailTable.LocationId, dbo.tblDefLocation.location_name "
            Dim dt As DataTable
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns("LocationId").Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class