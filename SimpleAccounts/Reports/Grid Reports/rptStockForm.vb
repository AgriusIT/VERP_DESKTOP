Imports System.Data.OleDb
Public Class rptStockForm
    Dim CurrentId As Integer


    Private Sub DefMainAcc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetSecurityRights()
        'Me.BindGrid()
        'Me.FillAllCombos()
        'Me.RefreshForm()    'FillComboBox()
        Me.rdbBatchWise.Checked = True

    End Sub
    Sub RefreshForm()
        Me.SaveToolStripButton.Text = "&Save"
        'If Me.uicmbCategory.ActiveRow.Cells(0).Value > 0 Then Me.uitxtItemCode.Text = GetNextDocNo(Me.uicmbCategory.ActiveRow.Cells(0).Value, 6, "ArticleDefTable", "ArticleCode") Else Me.uitxtItemCode.Text = ""
        'Me.uitxtItemCode.Focus()
        'Me.uitxtItemName.Text = ""
        'Me.uitxtPackQty.Text = 1
        'Me.uitxtStockLevel.Text = 0
        'Me.uitxtPrice.Text = 0
        'Me.uitxtSalePrice.Text = 0
        ''Me.uitxtSortOrder.Text = 0
        'Me.uichkActive.Checked = True
        '' Me.uicmbCategory.SelectedIndex = 0
        'If Me.uicmbSize.Rows.Count > 0 Then Me.uicmbSize.Rows(0).Activate()
        'If Me.uicmbColor.Rows.Count > 0 Then Me.uicmbColor.Rows(0).Activate()
        'If Me.uicmbType.Rows.Count > 0 Then Me.uicmbType.Rows(0).Activate()
        '' If Me.uicmbCategory.Rows.Count > 0 Then Me.uicmbCategory.Rows(0).Activate()
        Me.BindGrid()

    End Sub
    Sub BindGrid()
        'If Me.uicmbCategory.Rows.Count > 0 Then
        '    If Not Me.uicmbCategory.ActiveRow.Cells(0).Value > 0 Then
        '        If Me.DataGridView1.RowCount > 0 Then DataGridView1.DataSource.clear()
        '        Exit Sub
        '    End If
        'Else
        '    Exit Sub
        'End If
        Try

        
            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim strSql As String
            strSql = "SELECT     TOP 100 PERCENT dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleGroupName,dbo.ArticleDefView.ArticleTypeName, dbo.ArticleDefView.ArticleCompanyName, dbo.ArticleDefView.ArticleCode,dbo.ArticleDefView.ArticleDescription, " & _
                              "  dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName,  " & _
                              " dbo.vw_ArticleStock.SalesQty, dbo.vw_ArticleStock.PurchaseQty, dbo.vw_ArticleStock.DispatchQty, dbo.vw_ArticleStock.DispatchReturnQty, dbo.vw_ArticleStock.SalesReturnQty, " & _
                              " dbo.vw_ArticleStock.PurchaseReturnQty, dbo.vw_ArticleStock.Stock, dbo.vw_ArticleStock.Stock * dbo.ArticleDefView.PurchasePrice AS Value " & _
                              " FROM         dbo.ArticleDefView INNER JOIN " & _
                              " dbo.vw_ArticleStock ON dbo.ArticleDefView.ArticleId = dbo.vw_ArticleStock.ArticleId " & _
                              " WHERE ArticleDefView.Active=1 " & _
                              " ORDER BY dbo.vw_ArticleStock.Stock DESC"

            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            Me.grdStock.DataSource = dt
            Me.grdStock.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub EditRecord()
        'Me.uitxtItemCode.Text = DataGridView1.CurrentRow.Cells("Articlecode").Value
        'Me.uitxtItemName.Text = DataGridView1.CurrentRow.Cells("ArticleDescription").Value
        '' Me.uicmbCategory.Text = DataGridView1.CurrentRow.Cells("ArticleGroupId").Value
        'Me.uicmbSize.Text = DataGridView1.CurrentRow.Cells("ArticleSizeName").Value
        'Me.uicmbColor.Text = DataGridView1.CurrentRow.Cells("ArticleColorName").Value
        'Me.uicmbType.Text = DataGridView1.CurrentRow.Cells("ArticleTypeName").Value
        'Me.uitxtPackQty.Text = DataGridView1.CurrentRow.Cells("PackQty").Value
        'Me.uitxtStockLevel.Text = DataGridView1.CurrentRow.Cells("StockLevel").Value
        'Me.uitxtPrice.Text = DataGridView1.CurrentRow.Cells("PurchasePrice").Value
        'Me.uitxtSalePrice.Text = DataGridView1.CurrentRow.Cells("SalePrice").Value
        ''Me.uitxtSortOrder.Text = DataGridView1.CurrentRow.Cells("SortOrder").Value
        'Me.uichkActive.Checked = DataGridView1.CurrentRow.Cells("Active").Value

        'Me.CurrentId = Me.DataGridView1.CurrentRow.Cells(0).Value
        'Me.SaveToolStripButton.Text = "&Update"
    End Sub
    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Me.EditRecord()
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        'If Not Me.uicmbCategory.ActiveRow.Cells(0).Value > 0 Then
        '    msg_Error("Please select category")
        '    Me.uicmbCategory.Focus()
        '    Exit Sub
        'End If

        'If Not Me.uicmbType.ActiveRow.Cells(0).Value > 0 Then
        '    msg_Error("Please select type")
        '    Me.uicmbType.Focus()
        '    Exit Sub
        'End If

        'If Me.uitxtItemCode.Text = "" Then
        '    msg_Error("Please enter valid Item Code")
        '    Me.uitxtItemCode.Focus()
        '    Exit Sub

        'End If

        'If Me.uitxtItemName.Text = "" Then
        '    msg_Error("Please enter valid Item Name")
        '    Me.uitxtItemName.Focus()
        '    Exit Sub
        'End If

        'If Not Me.uicmbColor.ActiveRow.Cells(0).Value > 0 Then
        '    msg_Error("Please select color")
        '    Me.uicmbColor.Focus()
        '    Exit Sub
        'End If

        'If Not Me.uicmbSize.ActiveRow.Cells(0).Value > 0 Then
        '    msg_Error("Please select size")
        '    Me.uicmbSize.Focus()
        '    Exit Sub
        'End If


        ''If Not Me.uitxtPrice.Text > 0 Then
        ''    msg_Error("Please enter valid purchase price")
        ''    Me.uitxtPrice.Focus()
        ''    Exit Sub
        ''End If

        ''If Not Me.uitxtSalePrice.Text > 0 Then
        ''    msg_Error("Please enter valid sale price")
        ''    Me.uitxtSalePrice.Focus()
        ''    Exit Sub
        ''End If

        'If Not Me.uitxtPackQty.Text > 0 Then
        '    msg_Error("Please enter minimum pack quantity of 1")
        '    Me.uitxtPackQty.Focus()
        '    Exit Sub
        'End If

        'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub

        'Dim cm As New OleDbCommand

        'If Con.State = ConnectionState.Closed Then Con.Open()
        'cm.Connection = Con
        'Try
        '    If Me.SaveToolStripButton.Text = "&Save" Or Me.SaveToolStripButton.Text = "&Save" Then
        '        cm.CommandText = "insert into ArticleDefTable(ArticleCode,ArticleDescription,ArticleGroupId,SizeRangeId,ArticleColorId,ArticleTypeId,PurchasePrice,SalePrice,PackQty,StockLevel,Active,SortOrder,IsDate) values('" & Me.uitxtItemCode.Text & "','" & Me.uitxtItemName.Text & "'," & Me.uicmbCategory.ActiveRow.Cells(0).Value & "," & Me.uicmbSize.ActiveRow.Cells(0).Value & "," & Me.uicmbColor.ActiveRow.Cells(0).Value & "," & Me.uicmbType.ActiveRow.Cells(0).Value & "," & Me.uitxtPrice.Text & "," & Me.uitxtSalePrice.Text & "," & Me.uitxtPackQty.Text & "," & Me.uitxtStockLevel.Text & "," & IIf(Me.uichkActive.Checked = True, 1, 0) & "," & Me.uitxtSortOrder.Text & "," & Date.Today & ")"
        '    Else
        '        cm.CommandText = "update ArticleDefTable set ArticleCode='" & Me.uitxtItemCode.Text & "',ArticleDescription='" & Me.uitxtItemName.Text & "', ArticleGroupId='" & Me.uicmbCategory.ActiveRow.Cells(0).Value & "',SizeRangeId=" & Me.uicmbSize.ActiveRow.Cells(0).Value & ",articleColorId=" & Me.uicmbColor.ActiveRow.Cells(0).Value & ",ArticleTypeId=" & Me.uicmbType.ActiveRow.Cells(0).Value & ",PurchasePrice=" & Me.uitxtPrice.Text & ",SalePrice=" & Me.uitxtSalePrice.Text & ",PackQty=" & Me.uitxtPackQty.Text & ",StockLevel=" & Me.uitxtStockLevel.Text & ",Active=" & IIf(Me.uichkActive.Checked = True, 1, 0) & ",SortOrder=" & Me.uitxtSortOrder.Text & " where ArticleId=" & Me.CurrentId
        '    End If
        '    cm.ExecuteNonQuery()
        '    msg_Information(str_informSave)
        '    Me.CurrentId = 0
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'Finally
        '    Con.Close()
        'End Try
        'Me.RefreshForm()
    End Sub
    Sub FillAllCombos()

        'FillUltraDropDown(Me.uicmbColor, "select ArticleColorId as Id,ArticleColorName as Name from ArticleColorDefTable where active=1 order by sortOrder")
        'FillUltraDropDown(Me.uicmbSize, "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 order by sortOrder")
        'FillUltraDropDown(Me.uicmbCategory, "select ArticleGroupId as Id, ArticleGroupName as Name from ArticleGroupDefTable where active=1 order by sortOrder")
        'FillUltraDropDown(Me.uicmbType, "select ArticleTypeId as Id, ArticleTypeName as Name from ArticleTypeDefTable where active=1 order by sortOrder")

        'If Me.uicmbSize.Rows.Count > 0 Then Me.uicmbSize.Rows(0).Activate()
        'If Me.uicmbColor.Rows.Count > 0 Then Me.uicmbColor.Rows(0).Activate()
        'If Me.uicmbType.Rows.Count > 0 Then Me.uicmbType.Rows(0).Activate()
        'If Me.uicmbCategory.Rows.Count > 0 Then Me.uicmbCategory.Rows(0).Activate()

    End Sub
    Sub FillComboBox()
        'Dim adp As New OleDbDataAdapter
        'Dim dt As New DataTable

        'Try
        '    'strSql = " SELECT coa_main_id,main_sub_id, main_title + '  " - "  ' + main_code  AS Title, tblCoaMainSub.sub_code AS sub_code, sub_title" & _
        '    '     " From tblCoaMain INNER JOIN" & _
        '    '     " tblCoaMainSub ON tblCoaMain.coa_main_id = tblCoaMainSub.coa_main_id" & _
        '    '     "  where tblCoaMain.coa_main_id = " & cboAccMaincode.ItemData(cboAccMaincode.ListIndex) & _
        '    '     " ORDER BY sub_code "
        '    adp = New OleDbDataAdapter("select * from ArticleGroupDefTable where active=1 order by sortOrder", Con)
        '    adp.Fill(dt)

        '    Me.uicmbCategory.ValueMember = "ARTICLEGROUPID"
        '    Me.uicmbCategory.DisplayMember = "ARTICLEGROUPNAME"
        '    Me.uicmbCategory.DataSource = dt

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try

    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        Me.RefreshForm()
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Me.EditRecord()
    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'FillUltraDropDown(Me.uicmbCategory, "select ArticleGroupId as Id, ArticleGroupName as Name from ArticleGroupDefTable where active=1 order by sortOrder")
        'If Me.uicmbCategory.Rows.Count > 0 Then Me.uicmbCategory.Rows(0).Activate()

    End Sub

    Private Sub Label11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'FillUltraDropDown(Me.uicmbColor, "select ArticleColorId as Id,ArticleColorName as Name from ArticleColorDefTable where active=1 order by sortOrder")

        'If Me.uicmbColor.Rows.Count > 0 Then Me.uicmbColor.Rows(0).Activate()
    End Sub

    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'FillUltraDropDown(Me.uicmbSize, "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 order by sortOrder")

        'If Me.uicmbSize.Rows.Count > 0 Then Me.uicmbSize.Rows(0).Activate()
    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'FillUltraDropDown(Me.uicmbType, "select ArticleTypeId as Id, ArticleTypeName as Name from ArticleTypeDefTable where active=1 order by sortOrder")

        'If Me.uicmbType.Rows.Count > 0 Then Me.uicmbType.Rows(0).Activate()

    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripButton.Click
        'If Not DataGridView1.RowCount > 0 Then
        '    msg_Error(str_ErrorNoRecordFound)
        '    Exit Sub
        'End If
        'If IsValidToDelete("PurchaseOrderDetailTable", "ArticleDefId", Me.DataGridView1.CurrentRow.Cells("ArticleId").Value.ToString) = True And IsValidToDelete("ReceivingDetailTable", "ArticleDefId", Me.DataGridView1.CurrentRow.Cells("ArticleId").Value.ToString) = True And IsValidToDelete("SalesDetailTable", "ArticleDefId", Me.DataGridView1.CurrentRow.Cells("ArticleId").Value.ToString) = True And IsValidToDelete("SalesOrderDetailTable", "ArticleDefId", Me.DataGridView1.CurrentRow.Cells("ArticleId").Value.ToString) = True Then
        '    If msg_Confirm(str_ConfirmDelete) = True Then
        '        Try
        '            Dim cm As New OleDbCommand

        '            If Con.State = ConnectionState.Closed Then Con.Open()
        '            cm.Connection = Con
        '            cm.CommandText = "delete from ArticleDefTable where ArticleId=" & Me.DataGridView1.CurrentRow.Cells("ArticleId").Value.ToString
        '            cm.ExecuteNonQuery()
        '            msg_Information(str_informDelete)
        '            Me.CurrentId = 0
        '        Catch ex As Exception
        '            msg_Error("Error occured while deleting record: " & ex.Message)
        '        Finally
        '            Con.Close()
        '        End Try
        '        Me.RefreshForm()


        '    End If
        'Else
        '    msg_Error(str_ErrorDependentRecordFound)
        'End If
    End Sub

    Private Sub uicmbCategory_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs)
        'If Not Me.uicmbCategory.ActiveRow Is Nothing Then
        '    Me.BindGrid()
        '    If Me.uicmbCategory.ActiveRow.Cells(0).Value > 0 Then Me.uitxtItemCode.Text = GetNextDocNo(Me.uicmbCategory.ActiveRow.Cells(0).Value, 6, "ArticleDefTable", "ArticleCode") Else Me.uitxtItemCode.Text = ""
        'End If
    End Sub

    Private Sub RefreshDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshDataToolStripMenuItem.Click
        Me.BindGrid()
        Me.GetSecurityRights()
    End Sub

    Private Sub ExportToExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        Me.SaveFileDialog1.Filter = "Excel Files|.xls"
        'Me.SaveFileDialog1.DefaultExt = ".xls"
        Me.SaveFileDialog1.FileName = "Stock List"
        Me.SaveFileDialog1.InitialDirectory = "C:\"
        If Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        If Me.SaveFileDialog1.FileName = "" Then Exit Sub

        'Me.UltraGridExcelExporter1.Export(Me.grdStock, Me.SaveFileDialog1.FileName) '& ".xls") '"c:\temp.xls")
        System.Diagnostics.Process.Start(Me.SaveFileDialog1.FileName) '& ".xls")
    End Sub

    Private Sub PrintItemLedgerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintItemLedgerToolStripMenuItem.Click
        Dim FromDate As Date = CDate("01-jan-2008")
        Dim ToDate As Date = Date.Now
        Dim opening As Integer = GetStockOpeningBalance(0, FromDate.Year & "-" & FromDate.Month & "-" & FromDate.Day & " 00:00:00")
        If Not Me.grdStock.RowCount > 0 Then msg_Error(str_ErrorNoRecordFound) : Exit Sub
        'Dim fromDate As String = Me.DateTimePicker1.Value.Year & "." & Me.DateTimePicker1.Value.Month & "." & Me.DateTimePicker1.Value.Day
        'Dim ToDate As String = Me.DateTimePicker2.Value.Year & "." & Me.DateTimePicker2.Value.Month & "." & Me.DateTimePicker2.Value.Day
        ' ShowReport("Ledger", "{SP_Rpt_Ledger.coa_detail_id}=" & Me.grdStock.ActiveRow.Cells(0).Value, fromDate, ToDate)
        rptDateRange.ReportName = rptDateRange.ReportList.ItemLedger
        rptDateRange.AccountId = Me.grdStock.CurrentRow.Cells(0).Value
        If Me.rdbAll.Checked Then
            rptDateRange.strBatchNo = String.Empty
        Else
            rptDateRange.strBatchNo = Me.grdStock.CurrentRow.Cells("BatchNo").Value.ToString
        End If
        rptDateRange.ShowDialog()
    End Sub
    Private Sub GetSecurityRights()
        Try

        
        If LoginGroup = "Administrator" Then
            Me.SaveToolStripButton.Enabled = True
            Me.DeleteToolStripButton.Enabled = True
                Me.PrintToolStripButton.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
            'Me.btnPrint.Enabled = True
            Exit Sub
        End If
            Dim dt As DataTable = GetFormRights(EnumForms.rptStockForm)
        If Not dt Is Nothing Then
                If Not dt.Rows.Count = 0 Then
                    If Me.SaveToolStripButton.Text = "Save" Or Me.SaveToolStripButton.Text = "&Save" Then
                        Me.SaveToolStripButton.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                    Else
                        Me.SaveToolStripButton.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                    End If
                    Me.DeleteToolStripButton.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                    Me.PrintToolStripButton.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                    Me.ContextMenuStrip1.Items("PrintItemLedgerToolStripMenuItem").Enabled = dt.Rows(0).Item("Print_Rights").ToString
                    Me.ContextMenuStrip1.Items("ExportToExcelToolStripMenuItem").Enabled = dt.Rows(0).Item("Export_Rights").ToString
                    Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                End If
                Me.SaveToolStripButton.Enabled = False
                Me.DeleteToolStripButton.Enabled = False
                Me.PrintToolStripButton.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False


                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                   ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub rdbBatchWise_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbBatchWise.CheckedChanged
        Try
            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim strSql As String
            strSql = "SELECT     TOP 100 PERCENT dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleGroupName,dbo.ArticleDefView.ArticleTypeName, dbo.ArticleDefView.ArticleCompanyName, dbo.ArticleDefView.ArticleCode,dbo.ArticleDefView.ArticleDescription, " & _
                           "  dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName,  " & _
                           " dbo.vw_ArticleStock.SalesQty, dbo.vw_ArticleStock.PurchaseQty, dbo.vw_ArticleStock.DispatchQty, dbo.vw_ArticleStock.DispatchReturnQty, dbo.vw_ArticleStock.SalesReturnQty, " & _
                           " dbo.vw_ArticleStock.PurchaseReturnQty, dbo.vw_ArticleStock.Stock, dbo.vw_ArticleStock.Stock * dbo.ArticleDefView.PurchasePrice AS Value " & _
                           " FROM         dbo.ArticleDefView INNER JOIN " & _
                           " dbo.vw_ArticleStock ON dbo.ArticleDefView.ArticleId = dbo.vw_ArticleStock.ArticleId " & _
                           " WHERE ArticleDefView.Active=1 " & _
                           " ORDER BY dbo.vw_ArticleStock.Stock DESC"

            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            Me.grdStock.DataSource = dt
            Me.grdStock.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdbAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAll.CheckedChanged
        Try
            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim strSql As String

            ', 'xxxx' as  BatchNo

            strSql = "SELECT     TOP 100 PERCENT dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleGroupName,dbo.ArticleDefView.ArticleTypeName, dbo.ArticleDefView.ArticleCompanyName, dbo.ArticleDefView.ArticleCode,dbo.ArticleDefView.ArticleDescription, " & _
                            "  dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName,  " & _
                            " dbo.vw_ArticleStock.SalesQty, dbo.vw_ArticleStock.PurchaseQty, dbo.vw_ArticleStock.DispatchQty, dbo.vw_ArticleStock.DispatchReturnQty, dbo.vw_ArticleStock.SalesReturnQty, " & _
                            " dbo.vw_ArticleStock.PurchaseReturnQty, dbo.vw_ArticleStock.Stock, dbo.vw_ArticleStock.Stock * dbo.ArticleDefView.PurchasePrice AS Value " & _
                            " FROM         dbo.ArticleDefView INNER JOIN " & _
                            " dbo.vw_ArticleStock ON dbo.ArticleDefView.ArticleId = dbo.vw_ArticleStock.ArticleId " & _
                            " WHERE ArticleDefView.Active=1 " & _
                            " ORDER BY dbo.vw_ArticleStock.Stock DESC"

            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            Me.grdStock.DataSource = dt
            Me.grdStock.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStock.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStock.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdStock.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Current Stock" & Chr(10) & " Print Date: " & Date.Now.ToString("dd/MMM/yyyy hh:mm:ss tt") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class