Public Class frmGrdRptTaxDuductionDetail

    Private Sub frmGrdRptTaxDuductionDetail_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            Me.dtpFromDate.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpToDate.Value = Date.Now

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillGrid()
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim dt As New DataTable
            If rbtPurchase.Checked = True Then
                dt = GetDataTable("SP_TaxDeductionDetail '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "','" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "'")
            ElseIf rbtSales.Checked = True Then
                dt = GetDataTable("SP_SalesTaxDeductionDetail '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "','" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "'")
            End If
            dt.AcceptChanges()
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdSaved.GroupByBoxVisible = True
            Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
            Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
            Me.grdSaved.RootTable.Columns("Invoice Date").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Rate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            If rbtPurchase.Checked = True Then
                Me.grdSaved.RootTable.Columns("ReceivingAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Else
                Me.grdSaved.RootTable.Columns("SalesAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End If
            Me.grdSaved.RootTable.Columns("Value_Excl_Sales_Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Sale Tax Rate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("SaleTax Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Value_Incl_Sales_Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("STWPercent").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("STW Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("SalesTax Receivable").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Payable Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("ITW Percent").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("ITW Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Net Payable").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaved.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Rate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            If rbtPurchase.Checked = True Then
                Me.grdSaved.RootTable.Columns("ReceivingAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Else
                Me.grdSaved.RootTable.Columns("SalesAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End If
            Me.grdSaved.RootTable.Columns("Value_Excl_Sales_Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Sale Tax Rate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("SaleTax Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Value_Incl_Sales_Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("STWPercent").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("STW Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("SalesTax Receivable").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Payable Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("ITW Percent").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("ITW Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Net Payable").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaved.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            If rbtPurchase.Checked = True Then
                Me.grdSaved.RootTable.Columns("ReceivingAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Else
                Me.grdSaved.RootTable.Columns("SalesAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            End If
            Me.grdSaved.RootTable.Columns("Value_Excl_Sales_Tax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("SaleTax Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Value_Incl_Sales_Tax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("STW Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("SalesTax Receivable").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Payable Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("ITW Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Net Payable").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            If Me.rbtPurchase.Checked = True Then
                Me.grdSaved.RootTable.Columns("ReceivingAmount").FormatString = "N" & DecimalPointInValue
            Else
                Me.grdSaved.RootTable.Columns("SalesAmount").FormatString = "N" & DecimalPointInValue
            End If

            Me.grdSaved.RootTable.Columns("Value_Excl_Sales_Tax").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Sale Tax Rate").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("SaleTax Amount").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Value_Incl_Sales_Tax").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("STWPercent").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("STW Amount").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("SalesTax Receivable").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Payable Amount").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("ITW Percent").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("ITW Amount").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Net Payable").FormatString = "N" & DecimalPointInValue

            Me.grdSaved.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInQty
            If Me.rbtPurchase.Checked = True Then
                Me.grdSaved.RootTable.Columns("ReceivingAmount").TotalFormatString = "N" & DecimalPointInValue
            Else
                Me.grdSaved.RootTable.Columns("SalesAmount").TotalFormatString = "N" & DecimalPointInValue
            End If
            Me.grdSaved.RootTable.Columns("Value_Excl_Sales_Tax").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("SaleTax Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Value_Incl_Sales_Tax").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("STW Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("SalesTax Receivable").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Payable Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("ITW Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Net Payable").TotalFormatString = "N" & DecimalPointInValue

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdSaved.AutoSizeColumns()

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 23:59:00"))
            ShowReport("rptTaxDeductionDetail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try

            FillGrid()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try

            If Me.rbtPurchase.Checked = True Then
                Me.grdSaved.Name = "grdSaved_Purchase"
            Else
                Me.grdSaved.Name = "grdSaved_Sales"
            End If


            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Tax Deduction Report" & vbCrLf & "Date From:" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy") & " Month: " & Me.dtpToDate.Value.ToString("dd-MMM-yyyy") & " "
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class