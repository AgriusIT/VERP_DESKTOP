Public Class frmGrdRptItemExpiryDateDetail
    Dim IsOpenForm As Boolean = False
    Private Sub GridEX1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.SelectionChanged
        Try
            If IsOpenForm = True Then
                If Me.GridEX1.RowCount = 0 Then Exit Sub
                Me.SplitContainer1.Panel2Collapsed = False
                FillGridExpiryDate(Me.GridEX1.CurrentRow.Cells("ArticleId").Value)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillGrid()
        Try
            Dim str As String = "SELECT dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, " _
           & " dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleUnitName, ISNULL(Stock.Qty, 0) " _
           & " AS ClosingQty FROM dbo.ArticleDefView LEFT OUTER JOIN " _
           & " (SELECT ArticleDefId, Round(SUM(ISNULL(InQty, 0) - ISNULL(OutQty, 0)),0) AS Qty " _
           & " FROM StockDetailTable GROUP BY ArticleDefId) Stock ON Stock.ArticleDefId = dbo.ArticleDefView.ArticleId " _
           & " ORDER BY dbo.ArticleDefView.SortOrder "
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            Me.GridEX1.DataSource = dt
            Me.GridEX1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillGridExpiryDate(ByVal ArticleDefId As Integer)
        Try
            Dim str As String = "SELECT dbo.ReceivingMasterTable.ReceivingNo, dbo.ReceivingMasterTable.ReceivingDate, dbo.ReceivingDetailTable.ExpiryDate , " _
           & " dbo.ReceivingDetailTable.Qty FROM  dbo.ReceivingDetailTable INNER JOIN " _
           & " dbo.ReceivingMasterTable ON dbo.ReceivingDetailTable.ReceivingId = dbo.ReceivingMasterTable.ReceivingId WHERE ReceivingDetailTable.ArticleDefId=" & ArticleDefId & " ORDER BY ReceivingDate DESC"
            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.GridEX2.DataSource = dt
            GridEX2.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            If IsOpenForm = True Then FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptItemExpiryDateDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnNew_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmGrdRptItemExpiryDateDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillGrid()
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = "Daily Working Report"
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs1 As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs1)
                fs1.Close()
                fs1.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Item Expiry Date" & Chr(10) & "UpTo: " & Now.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class