Imports System.Data.OleDb
Public Class rptInventoryForm
    Dim CurrentId As Integer

    Private Sub rptInventoryForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub rptInventoryForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 27 -1-14
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
         
            If e.KeyCode = Keys.F2 Then
                OpenToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If

        

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    Private Sub DefMainAcc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.BindGrid()
        'Me.FillAllCombos()
        'Me.RefreshForm()    'FillComboBox()
        Me.cmbType.SelectedIndex = 0
        'Me.dtpFrom.Value = Me.dtpFrom.Value.AddMonths(-1)
        Me.cmbPeriod.Text = "Current Month"
        Me.GetSecurityRights()

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
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable
        Dim strSql As String = String.Empty
        'strSql = "SELECT     TOP 100 PERCENT dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleGroupName, dbo.ArticleDefView.ArticleDescription, " & _
        '                "dbo.ArticleDefView.ArticleTypeName, dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName, dbo.vw_Batch_Stock.BatchNo,  " & _
        '                "dbo.vw_Batch_Stock.SalesQty, dbo.vw_Batch_Stock.PurchaseQty, dbo.vw_Batch_Stock.DispatchQty, dbo.vw_Batch_Stock.SalesReturnQty, " & _
        '                "dbo.vw_Batch_Stock.PurchaseReturnQty, dbo.vw_Batch_Stock.Stock, dbo.vw_Batch_Stock.Stock * dbo.ArticleDefView.PurchasePrice AS Value " & _
        '                "FROM         dbo.ArticleDefView INNER JOIN " & _
        '                "dbo.vw_Batch_Stock ON dbo.ArticleDefView.ArticleId = dbo.vw_Batch_Stock.ArticleId " & _
        '                "ORDER BY dbo.vw_Batch_Stock.Stock DESC"

        If Me.cmbType.SelectedIndex = 0 Then

            strSql = "SELECT    dbo.ArticleDefView.ArticleId,  dbo.ArticleDefView.ArticleGroupName AS ArticleGroupName, dbo.ArticleDefView.ArticleCode,dbo.ArticleDefView.ArticleDescription AS [Item Name], " _
                      & " dbo.ArticleDefView.ArticleTypeName AS Type, dbo.ArticleDefView.ArticleColorName AS Color, dbo.ArticleDefView.ArticleSizeName AS Size, " _
                      & " ISNULL(sales.SalesQty, 0) AS [Qty], ISNULL(sales.SalesValue, 0) AS [Value]" _
                      & " FROM         dbo.ArticleDefTable art LEFT OUTER JOIN" _
                      & "                   dbo.ArticleDefView ON art.ArticleId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                      & "                   (SELECT     dbo.SalesDetailTable.ArticleDefId, SUM(dbo.SalesDetailTable.Qty) AS SalesQty,  " _
                      & "                                        SUM(dbo.SalesDetailTable.Price * dbo.SalesDetailTable.Qty) AS SalesValue " _
                      & "             FROM          dbo.SalesMasterTable INNER JOIN " _
                      & "                                dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId " _
                      & "             WHERE      (Convert(varchar,dbo.SalesMasterTable.SalesDate,102) BETWEEN CONVERT(DATETIME, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(DATETIME, " _
                      & " '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
                      & "             GROUP BY dbo.SalesDetailTable.ArticleDefId) sales ON art.ArticleId = sales.ArticleDefId WHERE art.Active=1 "

        ElseIf Me.cmbType.SelectedIndex = 1 Then

            strSql = " SELECT      dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleGroupName AS ArticleGroupName, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription AS [Item Name], " _
                    & " dbo.ArticleDefView.ArticleTypeName AS Type, dbo.ArticleDefView.ArticleColorName AS Color, dbo.ArticleDefView.ArticleSizeName AS Size, " _
                    & " ISNULL(Pur.PurchaseQty, 0) AS [Qty], ISNULL(Pur.PurchaseValue, 0) AS [Value] " _
                    & " FROM         dbo.ArticleDefTable art LEFT OUTER JOIN " _
                    & "                   dbo.ArticleDefView ON art.ArticleId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN" _
                    & "                   (SELECT     dbo.ReceivingDetailTable.ArticleDefId, SUM(dbo.ReceivingDetailTable.Qty) AS PurchaseQty, " _
                    & "                                        SUM(dbo.ReceivingDetailTable.Price * dbo.ReceivingDetailTable.Qty) AS PurchaseValue" _
                    & "             FROM          dbo.ReceivingMasterTable INNER JOIN " _
                    & "                                dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId " _
                    & "             WHERE      (Convert(varchar,dbo.ReceivingMasterTable.ReceivingDate,102) BETWEEN CONVERT(DATETIME, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(DATETIME,  " _
                    & " '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102))  " _
                    & "             GROUP BY dbo.ReceivingDetailTable.ArticleDefId) Pur ON art.ArticleId = Pur.ArticleDefId WHERE art.Active=1"

        ElseIf Me.cmbType.SelectedIndex = 2 Then

            strSql = " SELECT      dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleGroupName AS ArticleGroupName, dbo.ArticleDefView.ArticleCode,dbo.ArticleDefView.ArticleDescription AS [Item Name], " _
                    & " dbo.ArticleDefView.ArticleTypeName AS Type, dbo.ArticleDefView.ArticleColorName AS Color, dbo.ArticleDefView.ArticleSizeName AS Size, " _
                    & " ISNULL(SR.SRQty, 0) AS [Qty], ISNULL(SR.SRValue, 0) AS [Value] " _
                    & " FROM         dbo.ArticleDefTable art LEFT OUTER JOIN " _
                    & "                   dbo.ArticleDefView ON art.ArticleId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN" _
                    & "                   (SELECT     dbo.SalesReturnDetailTable.ArticleDefId, SUM(dbo.SalesReturnDetailTable.Qty) AS SRQty,  " _
                    & "                                        SUM(dbo.SalesReturnDetailTable.Price * dbo.SalesReturnDetailTable.Qty) AS SRValue " _
                    & "             FROM          dbo.SalesReturnMasterTable INNER JOIN " _
                    & "                                dbo.SalesReturnDetailTable ON dbo.SalesReturnMasterTable.SalesReturnId = dbo.SalesReturnDetailTable.SalesReturnId " _
                    & "             WHERE      (Convert(varchar,dbo.SalesReturnMasterTable.SalesReturnDate,102) BETWEEN CONVERT(DATETIME, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(DATETIME,  " _
                    & " '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102))  " _
                    & "             GROUP BY dbo.SalesReturnDetailTable.ArticleDefId) SR ON art.ArticleId = SR.ArticleDefId WHERE art.Active=1 "

        ElseIf Me.cmbType.SelectedIndex = 3 Then

            strSql = " SELECT     dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleGroupName AS ArticleGroupName, dbo.ArticleDefView.ArticleCode,dbo.ArticleDefView.ArticleDescription AS [Item Name], " _
                    & " dbo.ArticleDefView.ArticleTypeName AS Type, dbo.ArticleDefView.ArticleColorName AS Color, dbo.ArticleDefView.ArticleSizeName AS Size, " _
                    & "   ISNULL(PR.PRQty, 0) AS [Qty], ISNULL(PR.PRValue, 0) AS [Value] " _
                    & " FROM         dbo.ArticleDefTable art LEFT OUTER JOIN  " _
                    & "                   dbo.ArticleDefView ON art.ArticleId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                    & "                   (SELECT     dbo.PurchaseReturnDetailTable.ArticleDefId, SUM(dbo.PurchaseReturnDetailTable.Qty) AS PRQty,  " _
                    & "                                        SUM(dbo.PurchaseReturnDetailTable.Price * dbo.PurchaseReturnDetailTable.Qty) AS PRValue" _
                    & "             FROM          dbo.PurchaseReturnMasterTable INNER JOIN  " _
                    & "                                dbo.PurchaseReturnDetailTable ON " _
                    & "                            dbo.PurchaseReturnMasterTable.PurchaseReturnId = dbo.PurchaseReturnDetailTable.PurchaseReturnId " _
                    & "             WHERE      (Convert(varchar,dbo.PurchaseReturnMasterTable.PurchaseReturnDate,102) BETWEEN CONVERT(DATETIME, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND  " _
                    & "                                CONVERT(DATETIME, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102))  " _
                    & "     GROUP BY dbo.PurchaseReturnDetailTable.ArticleDefId) PR ON art.ArticleId = PR.ArticleDefId WHERE art.Active=1 "


        End If

        adp = New OleDbDataAdapter(strSql, Con)
        adp.Fill(dt)
        Me.grdStock.DataSource = dt
        'Me.grdStock.
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
   
 
  
  

 
    Private Sub RefreshDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshDataToolStripMenuItem.Click
        Me.BindGrid()
        GetSecurityRights()
    End Sub

    Private Sub ExportToExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        Me.SaveFileDialog1.Filter = "Excel Files|.xls"
        'Me.SaveFileDialog1.DefaultExt = ".xls"
        Me.SaveFileDialog1.FileName = "Transaction Report"
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
        'rptDateRange.ReportName = rptDateRange.ReportList.ItemLedger
        'rptDateRange.AccountId = Me.grdStock.ActiveRow.Cells(0).Value
        'rptDateRange.ShowDialog()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        'System.IO.File.Delete(str_ApplicationStartUpPath)

        Me.BindGrid()
        Me.GetSecurityRights()
    End Sub

    Private Sub GetSecurityRights()
        '    Try
        '        Dim dt As DataTable = GetFormRights(EnumForms.rptInventoryForm)
        '        If Not dt Is Nothing Then
        '            If Not dt.Rows.Count = 0 Then
        '                If Me.SaveToolStripButton.Text = "Save" Or Me.SaveToolStripButton.Text = "&Save" Then
        '                    Me.SaveToolStripButton.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
        '                Else
        '                    Me.SaveToolStripButton.Enabled = dt.Rows(0).Item("Update_Rights").ToString
        '                End If
        '                Me.DeleteToolStripButton.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
        '                Me.PrintToolStripButton.Enabled = dt.Rows(0).Item("Print_Rights").ToString
        '                Me.ContextMenuStrip1.Items("PrintItemLedgerToolStripMenuItem").Enabled = dt.Rows(0).Item("Print_Rights").ToString
        '                Me.ContextMenuStrip1.Items("ExportToExcelToolStripMenuItem").Enabled = dt.Rows(0).Item("Export_Rights").ToString
        '                Me.Visible = dt.Rows(0).Item("View_Rights").ToString
        '            End If
        '        End If
        '    Catch ex As Exception
        '        msg_Error(ex.Message)
        '    End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFrom.Value = Date.Today
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-1)
            Me.dtpTo.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpTo.Value = Date.Today
        End If
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click

    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click

    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripButton.Click

    End Sub
End Class