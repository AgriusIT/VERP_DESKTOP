''TASK TFS1418 Muhammad Ameen: Made this new Apply Sale Price Utility form to modify Sale Price on 16-09-2017


Imports SBModel

Public Class frmApplySalePriceUtility
    Dim IsDelete As Boolean = False
    Private Sub txtNewSalePrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewSalePrice.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombo()
        Try
            Dim Str As String = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(PurchasePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock, Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SubSubId,0) as AccountId, Isnull(ServiceItem,0) as ServiceItem, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand], SalesAccountId, CGSAccountId, IsNull(Cost_Price,0) as Cost_Price, IsNull(SalePrice,0) as SalePrice FROM ArticleDefView where Active=1 AND SalesItem=1"
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
            FillUltraDropDown(Me.cmbItem, Str)
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
            'If rdoName.Checked = True Then
            '    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            'Else
            '    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmApplySalePriceUtility_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            GetSecurityRights()
            FillCombo()
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnShow.Click
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
            dt = GetDataTable("SP_ApplySalePrice '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "','" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "'," & Me.cmbItem.Value & "")
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
            Me.grd.RootTable.Columns("Sale Price").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Sale Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Sale Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Old Sale Price").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Old Sale Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Old Sale Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grd.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("PackQty").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("PackQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PackQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("TotalQty").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("TotalQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("TotalQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnAlter.Click
        Try
            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt.AcceptChanges()
                For Each r As DataRow In dt.Rows
                    r.BeginEdit()
                    If Val(Me.txtNewSalePrice.Text) > 0 Then
                        r("Sale Price") = Val(Me.txtNewSalePrice.Text)
                    End If
                    r.EndEdit()
                Next
            Else
                msg_Error("Grid has no record where new price could apply.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_Leave(sender As Object, e As EventArgs) Handles cmbItem.Leave
        Try
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            Me.txtSalePrice.Text = Val(Me.cmbItem.ActiveRow.Cells("SalePrice").Value.ToString)
            Me.txtNewSalePrice.Text = Val(Me.txtSalePrice.Text)
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
            Me.txtProcessNo.Text = GetDocumentNo()
            Me.dtpProcessDate.Value = Date.Now
            Me.dtpDateFrom.Value = Date.Now
            Me.dtpDateTo.Value = Date.Now
            Me.dtpProcessDate.Focus()
            Me.lblProgressbar.Text = String.Empty
            Me.lblProgressbar.Visible = False
            Me.txtSalePrice.Text = ""
            Me.txtNewSalePrice.Text = ""
            'Me.ToolStripProgressBar1.Visible = False
            'Me.ToolStripProgressBar1.Minimum = 0
            'Me.ToolStripProgressBar1.Maximum = 0
            'Me.ToolStripProgressBar1.Value = 0
            Me.UltraProgressBar1.Visible = False
            Me.UltraProgressBar1.Minimum = 0
            Me.UltraProgressBar1.Maximum = 0
            Me.UltraProgressBar1.Value = 0
            '' FillGrid()
            Me.grd.DataSource = Nothing
            FillMasterGrid()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not Me.grd.RowCount > 0 Then
            msg_Error("No record is found in grid")
            Exit Sub
        End If
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

            Me.UltraProgressBar1.Visible = True
            Me.UltraProgressBar1.Maximum = Me.grd.RowCount
            Me.UltraProgressBar1.Minimum = 1


            strSQL = "INSERT INTO tblAlterSalePrice(ProcessDate, ProcessNo, FromDate, ToDate, ArticleDefId,SalePrice, UserName) VALUES(Convert(DateTime,'" & Me.dtpProcessDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102), N'" & Me.txtProcessNo.Text.Replace("'", "''") & "', Convert(DateTime,'" & Me.dtpDateFrom.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102), Convert(DateTime,'" & Me.dtpDateTo.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & Me.cmbItem.Value & "," & Val(Me.txtNewSalePrice.Text) & ", N'" & LoginUserName.Replace("'", "''") & "') Select @@Identity"
            cmd.CommandText = strSQL
            Dim intProcessID As Integer = cmd.ExecuteScalar
            Dim i As Integer = 1
            For Each jRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                cmd.CommandText = ""
                'cmd.CommandText = "Update SalesDetailTable SET SalePrice = " & IIf(Val(jRow.Cells("Purchase Price").Value.ToString) = 0, "" & Val(jRow.Cells("Old Purchase Price").Value.ToString) & "", "" & Val(jRow.Cells("Purchase Price").Value.ToString) & "") & ", CostPrice=" & IIf(Val(jRow.Cells("Cost Price").Value.ToString) = 0, "" & Val(jRow.Cells("Old Cost Price").Value.ToString) & "", "" & Val(jRow.Cells("Cost Price").Value.ToString) & "") & ", Old_Purchase_Price=" & Val(jRow.Cells("Old Purchase Price").Value.ToString) & ", Old_Cost_Price=" & Val(jRow.Cells("Old Cost Price").Value.ToString) & ", AvgRateProcessID=" & intProcessID & " WHERE SaleDetailId=" & Val(jRow.Cells("SaleDetailId").Value.ToString) & ""
                cmd.CommandText = "Update SalesDetailTable SET Price = " & IIf(Val(jRow.Cells("Sale Price").Value.ToString) = 0, "" & Val(jRow.Cells("Old Sale Price").Value.ToString) & "", "" & Val(jRow.Cells("Sale Price").Value.ToString) & "") & ", OldSalePrice=" & Val(jRow.Cells("Old Sale Price").Value.ToString) & ",  SalePriceProcessId=" & intProcessID & " WHERE SaleDetailId=" & Val(jRow.Cells("SaleDetailId").Value.ToString) & ""
                cmd.ExecuteNonQuery()
                lblProgressbar.Text = ""
                lblProgressbar.Text = "" & Me.UltraProgressBar1.Maximum & " out of " & i & ""
                Application.DoEvents()
                Me.UltraProgressBar1.Value = i
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
            dt = GetDataTable("Select ProcessId, ProcessDate, ProcessNo As [Process No], ArticleDefId, FromDate, ToDate, ArticleCode as [Item Code], ArticleDescription as [Item], tblAlterSalePrice.SalePrice, tblAlterSalePrice.UserName From tblAlterSalePrice INNER JOIN ArticleDefView Art On Art.ArticleId = tblAlterSalePrice.ArticleDefId Order By tblAlterSalePrice.ProcessId DESC")
            dt.AcceptChanges()
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("ProcessId").Visible = False
            Me.grdSaved.RootTable.Columns("ArticleDefId").Visible = False
            Me.grdSaved.RootTable.Columns("FromDate").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("ToDate").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("ProcessDate").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("SalePrice").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("SalePrice").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("SalePrice").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            Me.grd.UpdateData()
            If e.Column.Key = "btnDelete" Then
                If msg_Confirm("Do you want to delete the record?") Then
                    Me.grd.GetRow.Delete()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode.CheckedChanged
        If rbCode.Checked Then
            Me.cmbItem.DisplayMember = "Code"
        Else
            Me.cmbItem.DisplayMember = "Item"
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim Id As Integer
        Try
            Id = Me.cmbItem.Value
            FillCombo()
            Me.cmbItem.Value = Id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            'If e.Tab.Index = 1 Then
            '    Me.btnNew.Enabled = False
            '    Me.btnSave.Enabled = False
            '    Me.btnRefresh.Enabled = False
            '    Me.btnDelete.Enabled = True
            '    Me.CtrlGrdBar2.Visible = True
            '    Me.CtrlGrdBar1.Visible = False
            'ElseIf e.Tab.Index = 0 Then
            '    Me.btnNew.Enabled = True
            '    Me.btnSave.Enabled = True
            '    Me.btnRefresh.Enabled = True
            '    Me.CtrlGrdBar2.Visible = False
            '    Me.CtrlGrdBar1.Visible = True
            '    Me.btnDelete.Enabled = False
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                CtrlGrdBar2.mGridChooseFielder.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar2.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar2.mGridExport.Enabled = True
                Exit Sub
            End If
            If RegisterStatus = EnumRegisterStatus.Expired Then
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                CtrlGrdBar2.mGridChooseFielder.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar2.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar2.mGridExport.Enabled = False
                Exit Sub
            End If

            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                CtrlGrdBar2.mGridChooseFielder.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar2.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar2.mGridExport.Enabled = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                        'ElseIf RightsDt.FormControlName = "Update" Then
                        '    If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Report Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                        CtrlGrdBar2.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Report Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        CtrlGrdBar2.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        CtrlGrdBar2.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    ElseIf RightsDt.FormControlName = "Print Voucher" Then
                        'Me.btnPrintVoucher.Enabled = True
                        'Me.btnPrintVoucher1.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("SPU" + "-" + Microsoft.VisualBasic.Right(Me.dtpProcessDate.Value.Year, 2) + "-", "tblAlterSalePrice", "ProcessNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("SPU" & "-" & Format(Me.dtpProcessDate.Value, "yy") & Me.dtpProcessDate.Value.Month.ToString("00"), 4, "tblAlterSalePrice", "ProcessNo")
            Else
                Return GetNextDocNo("SPU", 6, "tblAlterSalePrice", "ProcessNo")
            End If
            'Else
            'Return ""
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function Delete() As Boolean
        'If msg_Confirm("Please Read Carefully, " & vbCrLf & "This process is beginning for cost rate update on selected invoices and this can't revert. " & vbCrLf & "Do you want to proceed. ?") = False Then Exit Function
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
            'Me.UltraProgressBar1.Visible = True
            'Me.UltraProgressBar1.Maximum = Me.grd.RowCount
            'Me.UltraProgressBar1.Minimum = 1
            'strSQL = "INSERT INTO tblAlterSalePrice(ProcessDate, ProcessNo, FromDate, ToDate, ArticleDefId,SalePrice) VALUES(Convert(DateTime,'" & Me.dtpProcessDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102), N'" & Me.txtProcessNo.Text.Replace("'", "''") & "', Convert(DateTime,'" & Me.dtpDateFrom.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102), Convert(DateTime,'" & Me.dtpDateTo.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & Me.cmbItem.Value & "," & Val(Me.txtNewSalePrice.Text) & ") Select @@Identity"
            strSQL = "Delete From tblAlterSalePrice Where ProcessId=" & Me.grdSaved.CurrentRow.Cells("ProcessId").Value & ""
            cmd.CommandText = strSQL
            Dim intProcessID As Integer = cmd.ExecuteScalar
            ''
            strSQL = "Select SaleDetailId, IsNull(Price, 0) As SalePrice, IsNull(OldSalePrice, 0) As OldSalePrice From SalesDetailTable Where SalePriceProcessId=" & Me.grdSaved.CurrentRow.Cells("ProcessId").Value & " And ArticleDefId = " & Me.grdSaved.CurrentRow.Cells("ArticleDefId").Value & ""
            cmd.CommandText = strSQL
            Dim da As New OleDb.OleDbDataAdapter()
            Dim dtSalePrice As New DataTable()
            da.SelectCommand = cmd
            da.Fill(dtSalePrice)

            ''
            Dim i As Integer = 1
            For Each Row As DataRow In dtSalePrice.Rows
                cmd.CommandText = ""
                cmd.CommandText = "Update SalesDetailTable SET Price = " & Row.Item("OldSalePrice") & ", OldSalePrice = 0, SalePriceProcessId = 0 Where SaleDetailId = " & Row.Item("SaleDetailId") & ""
                cmd.ExecuteNonQuery()
            Next
            'For Each jRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            '    cmd.CommandText = ""
            '    'cmd.CommandText = "Update SalesDetailTable SET SalePrice = " & IIf(Val(jRow.Cells("Purchase Price").Value.ToString) = 0, "" & Val(jRow.Cells("Old Purchase Price").Value.ToString) & "", "" & Val(jRow.Cells("Purchase Price").Value.ToString) & "") & ", CostPrice=" & IIf(Val(jRow.Cells("Cost Price").Value.ToString) = 0, "" & Val(jRow.Cells("Old Cost Price").Value.ToString) & "", "" & Val(jRow.Cells("Cost Price").Value.ToString) & "") & ", Old_Purchase_Price=" & Val(jRow.Cells("Old Purchase Price").Value.ToString) & ", Old_Cost_Price=" & Val(jRow.Cells("Old Cost Price").Value.ToString) & ", AvgRateProcessID=" & intProcessID & " WHERE SaleDetailId=" & Val(jRow.Cells("SaleDetailId").Value.ToString) & ""
            '    cmd.CommandText = "Update SalesDetailTable SET Price = " & IIf(Val(jRow.Cells("Sale Price").Value.ToString) = 0, "" & Val(jRow.Cells("Old Sale Price").Value.ToString) & "", "" & Val(jRow.Cells("Sale Price").Value.ToString) & "") & ", OldSalePrice=" & Val(jRow.Cells("Old Sale Price").Value.ToString) & ",  SalePriceProcessId=" & intProcessID & " WHERE SaleDetailId=" & Val(jRow.Cells("SaleDetailId").Value.ToString) & ""
            '    cmd.ExecuteNonQuery()
            '    lblProgressbar.Text = ""
            '    lblProgressbar.Text = "" & Me.UltraProgressBar1.Maximum & " out of " & i & ""
            '    Application.DoEvents()
            '    Me.UltraProgressBar1.Value = i
            '    i += 1
            '    Application.DoEvents()

            'Next
            trans.Commit()

            Return True

        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
            Me.Cursor = Cursors.Default
        End Try
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If msg_Confirm("Do you want to delete the record?") Then
                If Delete() Then
                    msg_Information("Record has been deleted successfully.")
                    ResetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

  
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Sale  Price Utility " & Chr(10) & "From Date: " & dtpDateFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpDateTo.Value.ToString("dd-MM-yyyy").ToString & ""

            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle

            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
