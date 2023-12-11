Public Class frmSalesOrderStatus
    Dim intOrderStatusId As Integer = 0I
    Private Sub frmSalesOrderStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Resetcontrols()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombo()
        Try
            FillDropDown(Me.cmbGroup, "Select OrderGroup, OrderGroup as Group From SalesOrderStatusTable WHERE OrderGroup <> ''", False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Resetcontrols()
        Try
            Me.Height = New Point(225).X
            Me.grd.Visible = False
            intOrderStatusId = 0I
            btnSave.Text = "&Save"
            Me.txtOrderStatus.Text = String.Empty
            FillCombo()
            If Not Me.cmbGroup.SelectedIndex = -1 Then Me.cmbGroup.SelectedIndex = 0
            chkActive.Checked = True
            Me.txtSortOrder.Text = 1
            GetAllRecords()
            Me.txtOrderStatus.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetAllRecords()
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select OrderStatusID, OrderStatus, OrderGroup, Active, SortOrder From SalesOrderStatusTable ORDER BY OrderStatusID DESC")
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()

            Me.grd.RootTable.Columns("OrderStatusID").Visible = False


            Me.grd.RootTable.Columns.Add("btnDelete", Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.TextBox)
            Me.grd.RootTable.Columns("btnDelete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grd.RootTable.Columns("btnDelete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grd.RootTable.Columns("btnDelete").Caption = "Action"
            Me.grd.RootTable.Columns("btnDelete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            Me.grd.RootTable.Columns("btnDelete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            Me.grd.RootTable.Columns("btnDelete").ButtonText = "Delete"
            Me.grd.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If Me.txtOrderStatus.Text = String.Empty Then
            ShowErrorMessage("Please enter order type.")
            Me.txtOrderStatus.Focus()
            Exit Sub
        End If

        Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand
        Try

            cmd.Transaction = trans
            cmd.Connection = objCon
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 300



            cmd.CommandText = ""
            cmd.CommandText = "Select Count(*) From SalesOrderStatusTable WHERE OrderStatusID <> " & intOrderStatusId & " AND OrderStatus='" & Me.txtOrderStatus.Text.Replace("'", "''") & "'"
            Dim intID As Integer = cmd.ExecuteScalar()

            If intID > 0 Then
                Throw New Exception("Order Type Name Is Already Exists.")
            End If

            If intOrderStatusId > 0 Then
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                cmd.CommandText = ""
                cmd.CommandText = "Update SalesOrderStatusTable SET OrderStatus='" & Me.txtOrderStatus.Text.Replace("'", "''") & "', OrderGroup=" & IIf(Me.cmbGroup.Text.Length > 0, "'" & Me.cmbGroup.Text.Replace("'", "''") & "'", "NULL") & ",Active=" & IIf(Me.chkActive.Checked = True, 1, 0) & ",SortOrder=" & Val(Me.txtSortOrder.Text) & " WHERE OrderStatusID=" & intOrderStatusId & ""
                cmd.ExecuteNonQuery()

            Else
                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO SalesOrderStatusTable(OrderStatus,OrderGroup,Active,SortOrder) VALUES('" & Me.txtOrderStatus.Text.Replace("'", "''") & "'," & IIf(Me.cmbGroup.Text.Length > 0, "'" & Me.cmbGroup.Text.Replace("'", "''") & "'", "NULL") & "," & IIf(Me.chkActive.Checked = True, 1, 0) & "," & Val(Me.txtSortOrder.Text) & ")"
                cmd.ExecuteNonQuery()

            End If


            trans.Commit()


            Resetcontrols()

        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
        End Try
    End Sub

    Private Sub grdBranch_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        If Me.grd.RowCount = 0 Then Exit Sub
        If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
        Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand

        Try

            cmd.Transaction = trans
            cmd.Connection = objCon
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 300

            cmd.CommandText = ""
            cmd.CommandText = "Delete From SalesOrderStatusTable WHERE OrderStatusID=" & Val(grd.GetRow.Cells("OrderStatusID").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            trans.Commit()
            Resetcontrols()

        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
        End Try
    End Sub

    Private Sub grdBranch_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try

            If Me.grd.RowCount = 0 Then Exit Sub
            intOrderStatusId = Val(Me.grd.GetRow.Cells("OrderStatusID").Value.ToString)
            Me.txtOrderStatus.Text = Me.grd.GetRow.Cells("OrderStatus").Value.ToString
            Me.cmbGroup.Text = Me.grd.GetRow.Cells("OrderGroup").Value.ToString
            Me.chkActive.Checked = Convert.ToBoolean(Me.grd.GetRow.Cells("Active").Value.ToString)
            Me.txtSortOrder.Text = Val(Me.grd.GetRow.Cells("SortOrder").Value.ToString)
            btnSave.Text = "&Update"

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDetail_Click(sender As Object, e As EventArgs) Handles btnDetail.Click
        Try
            If Me.grd.Visible = False Then
                Me.grd.Visible = True
                Me.Height = New Point(420).X
            Else
                Me.grd.Visible = False
                Me.Height = New Point(230).X
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class