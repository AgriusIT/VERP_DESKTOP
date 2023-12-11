Public Class frmRptEnhancementNew
    Enum ReportList
        StockStatementByLPO
        StockStatementByType
        StockStatementWithSize
        StockInventoryReport
        StockInventoryValue
        DispatchSummary
        DispatchDetail
        DailySalesReport
        CashAccounting
        ProfitAndLossNotes
        BalanceSheetNotes
        RptStoreIssuanceSummary
        RptStoreIssuanceDetail
    End Enum
    Public Report_Name As ReportList
    Private Sub frmRptEnhancementNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        combobox("costCenter")
        combobox("ArticleType")

        cmbCostCenter.Visible = False
        Me.lblCostCenter.Visible = False
        Me.lblArticleType.Enabled = False
        Me.cmbArticleType.Enabled = False

        Me.cmbPeriod.Text = "Current Month"
        If Report_Name = ReportList.StockStatementByLPO Then
            Me.Text = "Stock Statement By LPO"
        ElseIf Report_Name = ReportList.StockStatementByType Then
            Me.Text = "Stock Statement By Type"
        ElseIf Report_Name = ReportList.StockStatementWithSize Then
            Me.Text = "Stock Statement With Size"
        ElseIf Report_Name = ReportList.StockInventoryReport Then
            Me.Text = "Stock Inventory Report"
        ElseIf Report_Name = ReportList.StockInventoryValue Then
            Me.Text = "Stock Inventory Value Report"
        ElseIf Report_Name = ReportList.DispatchSummary Then
            Me.Text = "Dispatch Summary"
        ElseIf Report_Name = ReportList.DispatchDetail Then
            Me.Text = "Dispatch Detail"
        ElseIf Report_Name = ReportList.DailySalesReport Then
            Me.Text = "Daily Sales Report"
        ElseIf Report_Name = ReportList.CashAccounting Then
            Me.Text = "Cash Accounting"
        ElseIf Report_Name = ReportList.ProfitAndLossNotes Then
            Me.Text = "Profit And Loss Notes Report"
        ElseIf Report_Name = ReportList.BalanceSheetNotes Then
            Me.Text = "Balance Sheet Notes Report"
        ElseIf Report_Name = ReportList.RptStoreIssuanceSummary Then
            cmbCostCenter.Visible = True
            Me.lblArticleType.Enabled = True
            Me.cmbArticleType.Enabled = True
            Me.Text = "Store Issuance Summary"
        ElseIf Report_Name = ReportList.RptStoreIssuanceDetail Then
            cmbCostCenter.Visible = True
            Me.lblCostCenter.Visible = True
            Me.Text = "Store Issuance Detail"
        End If
        If Me.Report_Name = ReportList.ProfitAndLossNotes Then
            Me.dtpFrom.Visible = False
            Me.dtpTo.Visible = False
            Me.Label1.Visible = False
            Me.Label2.Visible = False
        Else
            Me.dtpFrom.Visible = True
            Me.dtpTo.Visible = True
            Me.Label1.Visible = True
            Me.Label2.Visible = True
        End If


    End Sub
    Public Sub CallShowReport()


        If Report_Name = ReportList.StockStatementByLPO Then
            AddRptParam("@Pack", False)
            ShowReport("rptStockStatment", "Nothing", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"), Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"), False)
        ElseIf Report_Name = ReportList.StockStatementByType Then
            ShowReport("rptStockStatmentType", "Nothing", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"), Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"), False)
        ElseIf Report_Name = ReportList.StockStatementWithSize Then
            frmMain.LoadControl("frmRptStockStatment")
        ElseIf Report_Name = ReportList.StockInventoryReport Then
            ShowReport("", "Nothing", Me.dtpFrom.Value, Me.dtpTo.Value, False)
        ElseIf Report_Name = ReportList.StockInventoryValue Then
            ShowReport("rptStockValueAccounting", "Nothing", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"), Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"), False)
        ElseIf Report_Name = ReportList.DispatchSummary Then
            ShowReport("", "Nothing", Me.dtpFrom.Value, Me.dtpTo.Value, False)
        ElseIf Report_Name = ReportList.DispatchDetail Then
            ShowReport("", "Nothing", Me.dtpFrom.Value, Me.dtpTo.Value, False)
        ElseIf Report_Name = ReportList.DailySalesReport Then
            ShowReport("cRptDailySales", "Nothing", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"), Me.dtpTo.Value.Date.ToString("yyyy-M-d 23:59:59"), False)
        ElseIf Report_Name = ReportList.CashAccounting Then
            ShowReport("cRptCashAccounting", "Nothing", Me.dtpFrom.Value, Me.dtpTo.Value, False)
        ElseIf Report_Name = ReportList.ProfitAndLossNotes Then
            ShowReport("cRptProfitLossNotesDetail", "Nothing", Me.dtpFrom.Value, Me.dtpTo.Value, False)
        ElseIf Report_Name = ReportList.BalanceSheetNotes Then
            ShowReport("cRptBalanceSheetNotesDetail", "Nothing", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"), Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"), False)
        ElseIf Report_Name = ReportList.RptStoreIssuanceSummary Then
            AddRptParam("@FromDate", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("RptStoreIssuence_Summary", "{sp_Store_Issuence_Summary.ArticleDescription} <> '' " & IIf(cmbCostCenter.SelectedIndex > 0, " AND {sp_Store_Issuence_Summary.costCenter} = " & Me.cmbCostCenter.SelectedValue & "", "") & "" & IIf(cmbArticleType.SelectedIndex > 0, " AND {sp_Store_Issuence_Summary.ArticleTypeId} = " & Me.cmbArticleType.SelectedValue & "", "") & "")
        ElseIf Report_Name = ReportList.RptStoreIssuanceDetail Then

            AddRptParam("@FromDate", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("RptStoreIssuence", "" & IIf(cmbCostCenter.SelectedIndex > 0, "{sp_Store_Issuence.costCenter} = " & Me.cmbCostCenter.SelectedValue & "", "") & "")
        End If
    End Sub
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        Try
            GetCrystalReportRights()
            Me.CallShowReport()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged, cmbCostCenter.SelectedIndexChanged
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
    Public Sub combobox(Optional ByVal Condition As String = "")
        If Condition = "costCenter" Then
            FillDropDown(cmbCostCenter, "SELECT CostCenterID, Name FROM  dbo.tblDefCostCenter")
        ElseIf Condition = "ArticleType" Then
            FillDropDown(cmbArticleType, "Select ArticleTypeId, ArticleTypeName  From ArticleTypeDefTable Order By 1 ASC")
        End If
    End Sub

End Class