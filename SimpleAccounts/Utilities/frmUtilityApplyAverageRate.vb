Public Class frmUtilityApplyAverageRate
    Dim IsFormOpen As Boolean = False
    Private Sub txtNewCostPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewPurchasePrice.KeyPress, txtNewCostPrice.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombo()
        Try
            Dim Str As String = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(PurchasePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock, Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SubSubId,0) as AccountId, Isnull(ServiceItem,0) as ServiceItem, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand], SalesAccountId, CGSAccountId, IsNull(Cost_Price,0) as Cost_Price FROM ArticleDefView where Active=1"
            'If flgCompanyRights = True Then
            '    Str += " AND ArticleDefView.CompanyId=" & MyCompanyId
            'End If
            If ItemSortOrder = True Then
                Str += " ORDER By ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByCode = True Then
                Str += " ORDER By ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByName = True Then
                Str += " ORDER By ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            Else
                Str += " ORDER By ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            End If
            'End Task:2452

            FillUltraDropDown(Me.cmbItem, Str, True)
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns("ID").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost_Price").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True '' Add New Column for Sales Account Id
            Me.cmbItem.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True '' Task:2388 Servicie Item Column Hidden
            Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("SalesAccountId").Hidden = True '' Task:2388 Servicie Item Column Hidden
            Me.cmbItem.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmUtilityApplyAverageRate_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            FillCombo()
            ResetControls()
            IsFormOpen = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Sub FillGrid()
        Me.Cursor = Cursors.WaitCursor
        Try

            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub

            Dim dt As New DataTable
            If rdoSales.Checked = True Then
                dt = GetDataTable("SP_ApplyAverageRateOnSalesInvoice '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "','" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "'," & Me.cmbItem.Value & "")
                dt.AcceptChanges()
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                Me.grd.RootTable.Columns.Add("btnDelete")
                Me.grd.RootTable.Columns("btnDelete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("btnDelete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("btnDelete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("btnDelete").Caption = "Action"
                Me.grd.RootTable.Columns("btnDelete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("btnDelete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

                Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
                Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grd.RootTable.Columns("SalesId").Visible = False
                Me.grd.RootTable.Columns("SaleDetailId").Visible = False
                Me.grd.RootTable.Columns("CompanyId").Visible = False
                Me.grd.RootTable.Columns("LocationId").Visible = False
                Me.grd.RootTable.Columns("CustomerCode").Visible = False
                Me.grd.RootTable.Columns("PackQty").Visible = False
                Me.grd.RootTable.Columns("TotalQty").Visible = False
                Me.grd.RootTable.Columns("SalesDate").FormatString = "dd/MMM/yyyy"
                Me.grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns("PackQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns("TotalQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns("Purchase Price").FormatString = "N" & DecimalPointInValue
                Me.grd.RootTable.Columns("Cost Price").FormatString = "N" & DecimalPointInValue
                Me.grd.RootTable.Columns("Purchase Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Cost Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Purchase Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Cost Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.AutoSizeColumns()
            Else
                dt = GetDataTable("SP_ApplyAverageRateOnStoreIssuence '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "','" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "'," & Me.cmbItem.Value & "")
                dt.AcceptChanges()
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                Me.grd.RootTable.Columns.Add("btnDelete")
                Me.grd.RootTable.Columns("btnDelete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("btnDelete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("btnDelete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("btnDelete").Caption = "Action"
                Me.grd.RootTable.Columns("btnDelete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("btnDelete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

                Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
                Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grd.RootTable.Columns("DispatchId").Visible = False
                Me.grd.RootTable.Columns("DispatchDetailId").Visible = False
                Me.grd.RootTable.Columns("CompanyId").Visible = False
                Me.grd.RootTable.Columns("Customer Code").Visible = False
                Me.grd.RootTable.Columns("PackQty").Visible = False
                Me.grd.RootTable.Columns("TotalQty").Visible = False
                Me.grd.RootTable.Columns("DispatchDate").FormatString = "dd/MMM/yyyy"
                Me.grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns("PackQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns("TotalQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns("Purchase Price").FormatString = "N" & DecimalPointInValue
                Me.grd.RootTable.Columns("Cost Price").FormatString = "N" & DecimalPointInValue
                Me.grd.RootTable.Columns("Purchase Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Cost Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Purchase Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Cost Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.AutoSizeColumns()
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Try
            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
            dt.AcceptChanges()
            For Each r As DataRow In dt.Rows
                r.BeginEdit()
                If Val(Me.txtNewCostPrice.Text) > 0 Then
                    r("Cost Price") = Val(Me.txtNewCostPrice.Text)
                End If
                If Val(Me.txtNewPurchasePrice.Text) > 0 Then
                    r("Purchase Price") = Val(Me.txtNewPurchasePrice.Text)
                End If
                r.EndEdit()
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_Leave(sender As Object, e As EventArgs) Handles cmbItem.Leave
        Try
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            Me.txtCurrentPurchasePrice.Text = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            Me.txtCurrentCostPrice.Text = Val(Me.cmbItem.ActiveRow.Cells("Cost_Price").Value.ToString)
            Me.txtNewPurchasePrice.Text = Val(Me.txtCurrentPurchasePrice.Text)
            Me.txtNewCostPrice.Text = Val(Me.txtCurrentCostPrice.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ResetControls()
        Try
            If Not cmbItem.IsItemInList = False Then
                If Me.cmbItem.ActiveRow Is Nothing Then
                    Me.cmbItem.Rows(0).Activate()
                Else
                    Me.cmbItem.Rows(0).Activate()
                End If
            End If
            Me.dtpProcessDate.Value = Date.Now
            Me.dtpDateFrom.Value = Date.Now
            Me.dtpDateTo.Value = Date.Now
            Me.dtpProcessDate.Focus()
            Me.lblProgressbar.Text = String.Empty
            Me.lblProgressbar.Visible = False
            Me.ToolStripProgressBar1.Visible = False
            Me.ToolStripProgressBar1.Minimum = 0
            Me.ToolStripProgressBar1.Maximum = 0
            Me.ToolStripProgressBar1.Value = 0
            Me.txtCurrentCostPrice.Text = ""
            Me.txtCurrentPurchasePrice.Text = ""
            Me.txtNewCostPrice.Text = ""
            Me.txtNewPurchasePrice.Text = ""
            FillGrid()
            FillMasterGrid()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If msg_Confirm("Please Read Carefully, " & vbCrLf & "This process is beginning for cost rate update on selected invoices and this can't revert. " & vbCrLf & "Do you want to proceed. ?") = False Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            Dim cmd As New OleDb.OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 300

            Me.ToolStripProgressBar1.Visible = True
            Me.ToolStripProgressBar1.Minimum = 1
            Me.ToolStripProgressBar1.Maximum = Me.grd.RowCount

            strSQL = "INSERT INTO tblUpdateAvgRateOnSalesProcess(Process_Date, FromDate, ToDate, ArticleDefId,CostPrice,PurchasePrice) VALUES(Convert(DateTime,'" & Me.dtpProcessDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(DateTime,'" & Me.dtpDateFrom.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102), Convert(DateTime,'" & Me.dtpDateTo.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & Me.cmbItem.Value & "," & Val(Me.txtNewCostPrice.Text) & "," & Val(Me.txtNewPurchasePrice.Text) & ") Select @@Identity"
            cmd.CommandText = strSQL
            Dim intProcessID As Integer = cmd.ExecuteScalar
            Dim i As Integer = 1
            For Each jRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                cmd.CommandText = ""
                If rdoSales.Checked = True Then
                    cmd.CommandText = "Update SalesDetailTable SET PurchasePrice = " & IIf(Val(jRow.Cells("Purchase Price").Value.ToString) = 0, "" & Val(jRow.Cells("Old Purchase Price").Value.ToString) & "", "" & Val(jRow.Cells("Purchase Price").Value.ToString) & "") & ", CostPrice=" & IIf(Val(jRow.Cells("Cost Price").Value.ToString) = 0, "" & Val(jRow.Cells("Old Cost Price").Value.ToString) & "", "" & Val(jRow.Cells("Cost Price").Value.ToString) & "") & ", Old_Purchase_Price=" & Val(jRow.Cells("Old Purchase Price").Value.ToString) & ", Old_Cost_Price=" & Val(jRow.Cells("Old Cost Price").Value.ToString) & ", AvgRateProcessID=" & intProcessID & " WHERE SaleDetailId=" & Val(jRow.Cells("SaleDetailId").Value.ToString) & ""
                Else
                    cmd.CommandText = "Update DispatchDetailTable SET Price = " & IIf(Val(jRow.Cells("Purchase Price").Value.ToString) = 0, "" & Val(jRow.Cells("Old Purchase Price").Value.ToString) & "", "" & Val(jRow.Cells("Purchase Price").Value.ToString) & "") & ", CostPrice=" & IIf(Val(jRow.Cells("Cost Price").Value.ToString) = 0, "" & Val(jRow.Cells("Old Cost Price").Value.ToString) & "", "" & Val(jRow.Cells("Cost Price").Value.ToString) & "") & ", Old_Purchase_Price=" & Val(jRow.Cells("Old Purchase Price").Value.ToString) & ", Old_Cost_Price=" & Val(jRow.Cells("Old Cost Price").Value.ToString) & ", AvgRateProcessID=" & intProcessID & " WHERE DispatchDetailId=" & Val(jRow.Cells("DispatchDetailId").Value.ToString) & ""
                End If
                cmd.ExecuteNonQuery()
                lblProgressbar.Text = ""
                lblProgressbar.Text = "" & Me.ToolStripProgressBar1.Maximum & " out of " & i & ""
                Application.DoEvents()
                Me.ToolStripProgressBar1.Value = i
                i += 1
                Application.DoEvents()

            Next
            trans.Commit()
            msg_Information("Process Completed Successfully, Please Update Sales Invoice Between This Date Range.")
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Sub FillMasterGrid()
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select Process_ID, Process_Date, ArticleDefID, FromDate, ToDate, ArticleCode as [Item Code], ArticleDescription as [Item], tblUpdateAvgRateOnSalesProcess.CostPrice, tblUpdateAvgRateOnSalesProcess.PurchasePrice From tblUpdateAvgRateOnSalesProcess INNER JOIN ArticleDefView Art On Art.ArticleId = tblUpdateAvgRateOnSalesProcess.ArticleDefID Order By tblUpdateAvgRateOnSalesProcess.Process_ID DESC")
            dt.AcceptChanges()
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("Process_ID").Visible = False
            Me.grdSaved.RootTable.Columns("ArticleDefID").Visible = False
            Me.grdSaved.RootTable.Columns("FromDate").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("ToDate").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("Process_Date").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("CostPrice").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("PurchasePrice").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("CostPrice").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("PurchasePrice").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("CostPrice").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("PurchasePrice").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            Me.grd.UpdateData()
            If e.Column.Key = "btnDelete" Then
                Me.grd.GetRow.Delete()

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoCode_CheckedChanged(sender As Object, e As EventArgs) Handles rdoCode.CheckedChanged, rdoName.CheckedChanged
        Try
            If Not IsFormOpen = True Then Exit Sub
            If Me.rdoCode.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
