Imports System.Data.OleDb

Public Class frmGrdRptSalesRegisterActivity
    Dim IsLoaded As Boolean = False

    Private Sub FillGrid()

        Try

            Dim dt As New DataTable
            Dim strSQL As String = String.Empty

            strSQL = "SELECT SalesMasterTable.SalesId, SalesOrderMasterTable.SalesOrderId, DeliveryChalanMasterTable.DeliveryId, Case When IsNull(DeliveryChalanMasterTable.POId,0)=0 then Convert(datetime,SalesOrderMasterTable.SalesOrderDate,102) Else SalesOrderMasterTable_1.SalesOrderDate end as Date," _
            & " Case When IsNull(DeliveryChalanMasterTable.POId,0)=0 then SalesOrderMasterTable.SalesOrderNo else SalesOrderMasterTable_1.SalesOrderNo end as SalesOrderNo, Convert(datetime,DeliveryChalanMasterTable.DeliveryDate,102) as DeliveryDate, DeliveryChalanMasterTable.DeliveryNo, Convert(datetime,SalesMasterTable.SalesDate,102) as SalesDate, " _
            & " SalesMasterTable.SalesNo,COA.detail_code as [Customer Code], COA.detail_title as [Customer], coa.TerritoryName as [Area], IsNull(SalesMasterTable.Invoice_Status,1) as Invoice_Status, SalesMasterTable.Invoice_Status_Reference, " _
            & " SalesMasterTable.Other_Remarks, Isnull(S_dt.TotalAmount,0) AS [Value Exc Tax], IsNull(S_dt.SalesTax,0) as [Sales Tax], IsNull(S_dt.[Value Inc Tax],0) as [Value Inc Tax] FROM SalesOrderMasterTable RIGHT OUTER JOIN SalesMasterTable LEFT OUTER JOIN SalesOrderMasterTable AS SalesOrderMasterTable_1 RIGHT OUTER JOIN" _
            & " DeliveryChalanMasterTable ON SalesOrderMasterTable_1.SalesOrderId = DeliveryChalanMasterTable.POId ON " _
            & " SalesMasterTable.DeliveryChalanId = DeliveryChalanMasterTable.DeliveryId ON SalesOrderMasterTable.SalesOrderId = SalesMasterTable.POId INNER JOIN vwCOADetail COA on COA.coa_detail_id = SalesMasterTable.CustomerCode  LEFT OUTER JOIN(Select SalesId, SUM(IsNull(Qty,0)*IsNull(Price,0)) as TotalAmount, SUM( (IsNull(TaxPercent,0)/100)* (IsNull(Qty,0)*IsNull(Price,0))) as SalesTax, SUM(IsNull(Qty,0)*IsNull(Price,0))+SUM( (IsNull(TaxPercent,0)/100)* (IsNull(Qty,0)*IsNull(Price,0)))  as [Value Inc Tax] From SalesDetailTable Group By SalesId) S_dt on s_dt.SalesId = dbo.SalesMasterTable.SalesId  WHERE (Convert(varchar,SalesMasterTable.SalesDate,102) BETWEEN Convert(DateTime,'" & dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) And Convert(DateTime,'" & dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  Order By SalesMasterTable.SalesId DESC "

            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            Me.grdHistory.DataSource = dt
            Me.grdHistory.RetrieveStructure()

            Dim dtStatus As New DataTable
            dtStatus = GetDataTable("Select SalesTaxInvoiceStatus_ID, SalesTaxInvoice_Status From tblSalesTaxInvoiceStatus")
            dtStatus.AcceptChanges()
            Me.grdHistory.RootTable.Columns("Invoice_Status").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdHistory.RootTable.Columns("Invoice_Status").HasValueList = True
            Me.grdHistory.RootTable.Columns("Invoice_Status").ValueList.PopulateValueList(dtStatus.DefaultView, "SalesTaxInvoiceStatus_ID", "SalesTaxInvoice_Status")

            Me.grdHistory.RootTable.Columns("Invoice_Status").Caption = "Invoice Status"
            Me.grdHistory.RootTable.Columns("Invoice_Status_Reference").Caption = "Status Reference"
            Me.grdHistory.RootTable.Columns("Other_Remarks").Caption = "Remarks"

            Me.grdHistory.RootTable.Columns("SalesId").Visible = False
            Me.grdHistory.RootTable.Columns("SalesOrderId").Visible = False
            Me.grdHistory.RootTable.Columns("DeliveryId").Visible = False

            Me.grdHistory.RootTable.Columns("Value Exc Tax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdHistory.RootTable.Columns("Value Exc Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdHistory.RootTable.Columns("Value Exc Tax").FormatString = "N" & DecimalPointInQty
            Me.grdHistory.RootTable.Columns("Value Exc Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grdHistory.RootTable.Columns("Sales Tax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdHistory.RootTable.Columns("Sales Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdHistory.RootTable.Columns("Sales Tax").FormatString = "N" & DecimalPointInQty
            Me.grdHistory.RootTable.Columns("Sales Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdHistory.RootTable.Columns("Value Inc Tax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdHistory.RootTable.Columns("Value Inc Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdHistory.RootTable.Columns("Value Inc Tax").FormatString = "N" & DecimalPointInQty
            Me.grdHistory.RootTable.Columns("Value Inc Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grdHistory.RootTable.Columns("Date").FormatString = "dd/MMM/yyyy"
            Me.grdHistory.RootTable.Columns("DeliveryDate").FormatString = "dd/MMM/yyyy"
            Me.grdHistory.RootTable.Columns("SalesDate").FormatString = "dd//MMM/yyyy"

            Dim ButtonCol As New Janus.Windows.GridEX.GridEXColumn
            ButtonCol.Key = "btnUpdate" 'This is the way to identify the column when it is clicked
            ButtonCol.ButtonText = "Update" 'the caption on the button
            ButtonCol.ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            ButtonCol.ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            ButtonCol.Width = 100
            Me.grdHistory.RootTable.Columns.Add(ButtonCol)
            Me.grdHistory.RootTable.Columns("btnUpdate").Caption = "Action"
            Me.grdHistory.RootTable.Columns("btnUpdate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grdHistory.RootTable.Columns("btnUpdate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For c As Integer = 0 To Me.grdHistory.RootTable.Columns.Count - 1
                If Me.grdHistory.RootTable.Columns(c).DataMember <> "Invoice_Status" AndAlso Me.grdHistory.RootTable.Columns(c).DataMember <> "Other_Remarks" AndAlso Me.grdHistory.RootTable.Columns(c).DataMember <> "Invoice_Status_Reference" Then
                    Me.grdHistory.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
                Me.grdHistory.RootTable.Columns(c).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Next


            'CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdHistory.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GetSalesRegisterHistory()
        Try

            If Me.grdHistory.CurrentRow.RowIndex = -1 Then Exit Sub
            Me.grdHistory.UpdateData()
            SplitContainer1.Panel2Collapsed = False

            Dim dt As New DataTable
            Dim saleId As Integer = Val(Me.grdHistory.CurrentRow.Cells("SalesId").Value.ToString)

            Dim cmd As New OleDbCommand("SP_SalesRegisterHistory " & saleId & "", Con)

            Dim adp As New OleDbDataAdapter(cmd)
            adp.Fill(dt)

            dt.AcceptChanges()

            Me.grdSaleInvoiceHistory.DataSource = dt
            Me.grdSaleInvoiceHistory.RetrieveStructure()

            Me.grdSaleInvoiceHistory.RootTable.Columns("SalesId").Visible = False
            Me.grdSaleInvoiceHistory.RootTable.Columns("LocationId").Visible = False

            Me.grdSaleInvoiceHistory.RootTable.Columns("Price").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaleInvoiceHistory.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaleInvoiceHistory.RootTable.Columns("Price").FormatString = "N" & DecimalPointInQty
            Me.grdSaleInvoiceHistory.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaleInvoiceHistory.RootTable.Columns("TaxPercent").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaleInvoiceHistory.RootTable.Columns("TaxPercent").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaleInvoiceHistory.RootTable.Columns("TaxPercent").FormatString = "N" & DecimalPointInQty
            Me.grdSaleInvoiceHistory.RootTable.Columns("TaxPercent").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaleInvoiceHistory.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaleInvoiceHistory.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaleInvoiceHistory.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdSaleInvoiceHistory.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaleInvoiceHistory.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaleInvoiceHistory.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaleInvoiceHistory.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInQty
            Me.grdSaleInvoiceHistory.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaleInvoiceHistory.RootTable.Columns("Sales Tax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaleInvoiceHistory.RootTable.Columns("Sales Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaleInvoiceHistory.RootTable.Columns("Sales Tax").FormatString = "N" & DecimalPointInQty
            Me.grdSaleInvoiceHistory.RootTable.Columns("Sales Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaleInvoiceHistory.RootTable.Columns("Total Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaleInvoiceHistory.RootTable.Columns("Total Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaleInvoiceHistory.RootTable.Columns("Total Amount").FormatString = "N" & DecimalPointInQty
            Me.grdSaleInvoiceHistory.RootTable.Columns("Total Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            grdSaleInvoiceHistory.AutoSizeColumns()

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdHistory.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdHistory.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdHistory.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptSalesRegisterActivity_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SplitContainer1.Panel2Collapsed = True
            'FillGrid()
            Me.dtpDateFrom.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpDateTo.Value = Date.Now
            IsLoaded = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub grdHistory_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdHistory.ColumnButtonClick

        If Me.grdHistory.RowCount <= 0 Then Exit Sub
        If e.Column.Key = "btnUpdate" Then
            If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
            Dim cmd As New OleDb.OleDbCommand
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            Try
                Dim strSql As String = String.Empty

                strSql = "Update SalesMasterTable Set Invoice_Status='" & Val(Me.grdHistory.GetRow.Cells("Invoice_Status").Value.ToString) & "',Invoice_Status_Reference='" & Me.grdHistory.GetRow.Cells("Invoice_Status_Reference").Value.ToString.Replace("'", "''") & "', Other_Remarks='" & Me.grdHistory.GetRow.Cells("Other_Remarks").Value.ToString.Replace("'", "''") & "' WHERE SalesId=" & Val(Me.grdHistory.GetRow.Cells("SalesId").Value.ToString) & ""
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()
                trans.Commit()

                Dim rowFormat As New Janus.Windows.GridEX.GridEXFormatStyle
                rowFormat.BackColor = Color.Ivory
                Me.grdHistory.GetRow.RowStyle = rowFormat

            Catch ex As Exception
                trans.Rollback()
                ShowErrorMessage(ex.Message)
            Finally
                Con.Close()
            End Try
        Else
            Exit Sub
        End If
    End Sub

    Private Sub grdHistory_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdHistory.SelectionChanged
        Try
            If IsLoaded = False Then
                Exit Sub
            End If

            If Me.grdHistory.Row < 0 Then   'if Row selection less then zero then doesn't show record in grdSaleInvoiceHistory
                Exit Sub
            Else
                GetSalesRegisterHistory()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim lbl As New Label
        lbl.Text = "While loading please wait ...."
        lbl.Visible = True
        lbl.Dock = DockStyle.Fill
        Me.Controls.Add(lbl)
        lbl.BringToFront()
        Application.DoEvents()
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub
End Class