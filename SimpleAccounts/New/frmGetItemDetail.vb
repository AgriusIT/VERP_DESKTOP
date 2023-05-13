Public Class frmGetItemDetail
    Dim IsOpenForm As Boolean = False
    Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "Item" Then
                FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as Item, ArticleCode as Code, ArticleColorName as Combination, ArticleSizeName as Size, ArticleGroupName as Department, ArticleTypeName as [Item Type], PurchasePrice as Price,IsNull(PackQty,0) as PackQty From ArticleDefView WHERE ArticleDescription <> ''")
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("PackQty").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Item").Width = 250
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Code").Width = 150
                End If
            ElseIf Condition = "Unit" Then
                Me.cmbUnit.ValueMember = "ArticlePackId"
                Me.cmbUnit.DisplayMember = "PackName"
                Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try


            DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmGetItemDetail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombo("Item")
            FillCombo("Unit")
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub ResetControls(Optional ByVal Condition As String = "")
        Try
            Me.cmbItem.Rows(0).Activate()
            If Not Me.cmbUnit.SelectedIndex = -1 Then Me.cmbUnit.SelectedIndex = 0
            Me.txtPackQty.Text = String.Empty
            Me.txtQty.Text = String.Empty
            Me.txtTotalQty.Text = String.Empty
            Me.txtRate.Text = String.Empty
            Me.txtTotalAmount.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        Try
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbUnit.Text <> "Loose" Then
                Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Row.Item("PackQty").ToString)
            Else
                Me.txtPackQty.Text = 1
            End If
            Me.txtRate.Text = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            FillCombo("Unit")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPackQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalQty.KeyPress, txtTotalAmount.KeyPress, txtRate.KeyPress, txtQty.KeyPress, txtPackQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalQty.TextChanged, txtTotalAmount.TextChanged, txtRate.TextChanged, txtQty.TextChanged, txtPackQty.TextChanged
        Try
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub GetTotalAmount()
        Try

            Dim dblTotalQty As Double = 0D
            Dim dblTotalAmount As Double = 0D
            dblTotalQty = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            dblTotalAmount = Val(dblTotalQty) * Val(txtRate.Text)

            Me.txtTotalQty.Text = dblTotalQty
            Me.txtTotalAmount.Text = dblTotalAmount

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GetPackQty()
        Try

            If Me.txtPackQty.TextLength <= 0 Then
                Me.txtPackQty.Text = (Val(Me.txtTotalQty.Text) / Val(Me.txtQty.Text))
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try

            If Me.cmbUnit.SelectedIndex = -1 Then Exit Sub
            If Me.cmbUnit.Text <> "Loose" Then
                Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Row.Item("PackQty").ToString)
            Else
                Me.txtPackQty.Text = 1
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class