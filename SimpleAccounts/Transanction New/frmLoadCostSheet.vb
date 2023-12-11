Public Class frmLoadCostSheet
    Public Shared _ArticleId As Integer = 0I
    Public Sub FillCombo()
        Try
            FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as Item, ArticleCode as [Item Code] From ArticleDefTableMaster WHERE ArticleDescription <> '' AND ArticleId In(Select DISTINCT MasterArticleId From tblCostSheet)")
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then
                Me.cmbItem.Rows(0).Activate()
            End If
            Dim id As Integer = 0I
            id = Me.cmbItem.ActiveRow.Cells(0).Value
            FillCombo()
            Me.cmbItem.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmLoadCostSheet_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            _ArticleId = 0I
            FillCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            If Me.cmbItem.IsItemInList = False Then
                Exit Sub
            End If
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            _ArticleId = Me.cmbItem.Value
            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPlanQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPlanQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class