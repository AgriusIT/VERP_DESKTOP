''08-Feb-2014 TASK:2418 Imran Ali Add new report sales certificate and wh tax ceritificate
''04-Mar-2014 TASK:2456   Imran Ali   Tax Certificate Report In ERP
''07-Mar-2014 TASK:2468  Imran Ali  Date sort order in cash flow statement
'18-Mar-2014 Imran Ali  TASK:2501  Add project filter on profit and loss detail reort
''18-Mar-2014 Imran Ali TASK:2502 Problem in Production Detail Report 
''19-Mar-2014 Task:2506 Imran Ali  Add batch quantity and finish goods name in store issue detail report
''27-Mar-2014 Task:2522 Imran Ali 2-no. of sale certificate issued record.
''7-May-2014 TASK:2609 Imran Ali Vendor List Visible on Purchase/Purchase Return Invoices Summary Report form
''26-Aug-2014 Task:2810 Imran Ali PL Note Sub Sub Account Wise Summary Report (Cotton Craft)
''22-Sep-2014 TAKS:2850 Imran Ali Department And Category Wise Purchase Report
''23-Sep-2014 TASK:2851 Imran Ali SummaryOfInvoices Report Revised
''03-Oct-2014 Task:2864 Imran Ali Cash Receipt Detail Against Employee
''31-07-2017 TASK:1212 Muhammad Ameen added Cost Center Group filter.
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Public Class rptDateRange
    Enum ReportList
        SummaryOfSalesInvoices
        SummaryOfPurchaseInvoices
        SummaryOfPurchaseReturn
        CashFlowStatment
        CashFlowStatmentStander
        ItemLedger
        PLSingleDate
        PLComparison
        voucherDetail
        rptDiscountNetRate
        ReceivingReport
        rptExpenses
        PLNotesDetail
        rptBSFomated
        RptSalesTaxDetail
        RptSalesTaxSummary
        rptExpenseStatementStd
        DailySupply
        AttendanceSummary
        DailyAttendance
        DemandSales
        WeightReport
        EmployeesTask
        BalanceSheetNotesSummary
        ReturnableGatepass
        ItemsDetailReport
        DailySalarySheet
        EmpSalarySheet
        DailySalarySheetSummary
        SalesReturnSummary
        ProductionSummary
        PrintVehicleLog
        LedgerByInvoices
        DemandSummary
        DamageBudget
        DeliveryChalanDetail
        DeliveryChalanSummary
        DailyEmployeeAttendance
        NetSalesReport
        WHTaxCertificate 'Task:2456 Added Index
        StoreIssuanceDetailBatchWise 'Task:2506 Added Index
        SalesCertificateIssued 'Task:2522 Added Report Index
        PLSubsubAccountSummary 'TAsk:2810 Added Report Index
        PLDetailAccountSummary 'TAsk:2810 Added Report Index
        PurchaseItemSummary 'Task:2850 Added report Index
        CashReceiptDetailAgainstEmployee
        MonthlyPurchaseSummary
        rptSummaryOfSalesTaxInvoices
        EmployeeAttendanceDetail
        WarrantyDetailReport
        AdvanceReceiptsSO
        AdvancePaymentsPO
        DispatchStatus
        Production      'Task#18082015 add enum for rptProduction report by Ahmad Sharif
        WIP             'Task#18082015 add enum fro rptWIP report by Ahmad Sharif
        ServicesInvoice             'Task#18082015 add enum fro rptServiceInvoice report by Ali Ansari
    End Enum
    Public ReportName As String
    Public AccountId As Integer
    Public strBatchNo As String = String.Empty
    Public IsCustomer As Boolean = False
    Public IsVendor As Boolean = False
    Public IsCostCenter As Boolean = False
    Public IsEmployee As Boolean = False
    Public IsProduction As Boolean = False
    Public PnlCostTop As Boolean = True
    Public flgCompanyRights As Boolean = False
    Dim CostCenterCriterial As String = ""
    Dim CostCenterCriterial1 As String = ""
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            GetCrystalReportRights()
            Me.CallShowReport()
        Catch Ex As Exception
            ShowErrorMessage(Ex.Message)
        End Try
    End Sub

    'Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    '    'Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    '    ' Me.Close()
    'End Sub
    Private Sub rptLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If

            lblCustomer.Text = "Customer"
            Me.cmbPeriod.Text = "Current Month"
            Me.pnlCost.Visible = False
            Me.pnlInvType.Visible = False
            Me.pnlVendorCustomer.Visible = False
            Me.pnlCashBank.Visible = False
            Me.chkIncludeCheque.Visible = False
            Me.chkUnposted.Visible = False

            Me.lblAccount.Visible = False
            Me.lblCompany.Visible = False
            Me.lblCostCenter.Visible = False
            Me.lblCustomer.Visible = False
            Me.lblEmployee.Visible = False
            Me.lblProductionLocation.Visible = False
            Me.lblVendor.Visible = False
            Me.lblCustomerType.Visible = False
            Me.lblCCGroup.Visible = False

            Me.cmbVendor.Visible = False
            Me.cmbCompany.Visible = False
            Me.cmbCostCenter.Visible = False
            Me.cmbCashAccount.Visible = False
            Me.cmbCustomer.Visible = False
            Me.cmbEmployee.Visible = False
            Me.cmbProducationLocation.Visible = False
            Me.cmbCustomerType.Visible = False
            Me.cmbCCGroup.Visible = False


            Me.pnlCost.Visible = False
            Me.pnlInvType.Visible = False
            Me.pnlVendorCustomer.Visible = False
            Me.pnlSalesType.Visible = False
            Me.pnlCustomerType.Visible = False
            Me.pnlEmployee.Visible = False
            Me.cbExcludeTax.Visible = False

            If Me.ReportName = Me.ReportName = ReportList.rptSummaryOfSalesTaxInvoices Then
                Me.Text = "Summary Of Sales Tax Invoices"
                pnlVendorCustomer.Visible = True
                Me.pnlInvType.Visible = True
                Me.pnlSalesType.Visible = True
                Me.pnlCost.Visible = True
                Me.cmbCostCenter.Visible = True
                Me.lblCostCenter.Visible = True
                PnlCostTop = False
                IsCustomer = True
                If IsCustomer = True Then
                    'lblVendor.Visible = False
                    'cmbVendor.Visible = False
                    'lblEmployee.Visible = False
                    'cmbEmployee.Visible = False
                    'cmbProducationLocation.Visible = False
                    'lblProductionLocation.Visible = False
                    Me.cmbCompany.Visible = True
                    Me.lblCompany.Visible = True
                    lblCustomer.Visible = True
                    cmbCustomer.Visible = True
                    Me.pnlCustomerType.Visible = True
                    Me.lblCustomerType.Visible = True
                    Me.cmbCustomerType.Visible = True
                End If
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & " ")
                FillDropDown(Me.cmbCustomerType, "Select Typeid, Name From TblDefCustomerType Where Active=1")
                FillUltraDropDown(cmbCustomer, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE Account_Type='Customer' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " ORDER BY 2 ASC")
                Me.cmbCustomer.Rows(0).Activate()
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True

            ElseIf Me.ReportName = ReportList.AdvanceReceiptsSO Then
                Me.Text = "Advance Receipts Against SO"
                pnlVendorCustomer.Visible = True
                Me.pnlInvType.Visible = True
                IsCustomer = True
                If IsCustomer = True Then
                    Me.cmbCompany.Visible = True
                    Me.lblCompany.Visible = True
                    lblCustomer.Visible = True
                    cmbCustomer.Visible = True
                End If
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & " ")
                FillUltraDropDown(cmbCustomer, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE Account_Type='Customer' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " ORDER BY 2 ASC")
                Me.cmbCustomer.Rows(0).Activate()
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True

            ElseIf Me.ReportName = ReportList.AdvancePaymentsPO Then
                Me.Text = "Advance Payments Against PO"
                Dim Str As String
                pnlVendorCustomer.Visible = True
                IsVendor = True
                If IsVendor = True Then
                    Me.cmbCompany.Visible = False
                    Me.lblCompany.Visible = False
                    lblVendor.Visible = True
                    cmbVendor.Visible = True
                End If

                ''Start TFS2124
                Str = "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE 1=1 " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " "
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    Str += " AND vwCOADetail.Account_Type in ('Vendor','Customer') "
                Else
                    Str += " AND  vwCOADetail.Account_Type='Vendor' "
                End If
                Str += " ORDER BY 2 ASC"
                ''End TFS2124
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & " ")
                FillUltraDropDown(cmbVendor, Str)
                Me.cmbVendor.Rows(0).Activate()
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True

            ElseIf Me.ReportName = ReportList.WarrantyDetailReport Or Me.ReportName = ReportList.WarrantyDetailReport Then
                Me.Text = "Warranty Detail Report"
                pnlVendorCustomer.Visible = True
                Me.pnlInvType.Visible = True
                Me.pnlSalesType.Visible = False
                PnlCostTop = False
                IsCustomer = True
                If IsCustomer = True Then
                    'lblVendor.Visible = False
                    'cmbVendor.Visible = False
                    'lblEmployee.Visible = False
                    'cmbEmployee.Visible = False
                    'cmbProducationLocation.Visible = False
                    'lblProductionLocation.Visible = False
                    Me.cmbCompany.Visible = True
                    Me.lblCompany.Visible = True
                    lblCustomer.Visible = True
                    cmbCustomer.Visible = True
                End If
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & " ")
                FillUltraDropDown(cmbCustomer, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE Account_Type='Customer' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " ORDER BY 2 ASC")
                Me.cmbCustomer.Rows(0).Activate()
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Me.ReportName = ReportList.NetSalesReport Then
                Me.Text = "Net Sales Report"
                pnlVendorCustomer.Visible = True
                Me.pnlInvType.Visible = True
                PnlCostTop = False
                IsCustomer = True
                If IsCustomer = True Then
                    'lblVendor.Visible = False
                    'cmbVendor.Visible = False
                    'lblEmployee.Visible = False
                    'cmbEmployee.Visible = False
                    'cmbProducationLocation.Visible = False
                    'lblProductionLocation.Visible = False
                    Me.cmbCompany.Visible = True
                    Me.lblCompany.Visible = True
                    lblCustomer.Visible = True
                    cmbCustomer.Visible = True
                End If
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & " ")
                FillUltraDropDown(cmbCustomer, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE Account_Type='Customer' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " ORDER BY 2 ASC")
                Me.cmbCustomer.Rows(0).Activate()
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Me.ReportName = ReportList.DeliveryChalanDetail Then
                Me.Text = "Delivery Chalan Detail"
                pnlVendorCustomer.Visible = True
                Me.pnlInvType.Visible = True
                PnlCostTop = True
                IsCustomer = True
                If IsCustomer = True Then
                    'lblVendor.Visible = False
                    'cmbVendor.Visible = False
                    'lblEmployee.Visible = False
                    'cmbEmployee.Visible = False
                    'cmbProducationLocation.Visible = False
                    'lblProductionLocation.Visible = False
                    Me.cmbCompany.Visible = True
                    Me.lblCompany.Visible = True
                    lblCustomer.Visible = True
                    cmbCustomer.Visible = True
                End If
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & "")
                FillUltraDropDown(cmbCustomer, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE Account_Type='Customer' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " ORDER BY 2 ASC")
                Me.cmbCustomer.Rows(0).Activate()
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Me.ReportName = ReportList.DeliveryChalanSummary Then
                Me.Text = "Delivery Chalan Summary"
                pnlVendorCustomer.Visible = True
                Me.pnlInvType.Visible = True
                PnlCostTop = False
                IsCustomer = True
                If IsCustomer = True Then
                    'lblVendor.Visible = False
                    'cmbVendor.Visible = False
                    'lblEmployee.Visible = False
                    'cmbEmployee.Visible = False
                    'cmbProducationLocation.Visible = False
                    'lblProductionLocation.Visible = False
                    Me.cmbCompany.Visible = True
                    Me.lblCompany.Visible = True
                    lblCustomer.Visible = True
                    cmbCustomer.Visible = True
                End If
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & "")
                FillUltraDropDown(cmbCustomer, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE Account_Type='Customer' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " ORDER BY 2 ASC")
                Me.cmbCustomer.Rows(0).Activate()
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Me.ReportName = ReportList.DamageBudget Then
                Me.Text = "Damage Budget"
                pnlVendorCustomer.Visible = True
                'Me.pnlInvType.Visible = True
                PnlCostTop = True
                IsCustomer = True
                If IsCustomer = True Then
                    'lblVendor.Visible = False
                    'cmbVendor.Visible = False
                    'lblEmployee.Visible = False
                    'cmbEmployee.Visible = False
                    cmbProducationLocation.Visible = False
                    lblProductionLocation.Visible = False
                    lblCustomer.Visible = True
                    cmbCustomer.Visible = True
                End If
                'FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable")
                FillUltraDropDown(cmbCustomer, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE Account_Type='Customer' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " ORDER BY 2 ASC")
                Me.cmbCustomer.Rows(0).Activate()
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Me.ReportName = ReportList.DemandSummary Then
                Me.Text = "Demand Summary"
                pnlVendorCustomer.Visible = False
                Me.pnlInvType.Visible = True
                PnlCostTop = True
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & " ")
            ElseIf Me.ReportName = ReportList.LedgerByInvoices Then
                Me.Text = "Ledger By Invoices"
                pnlVendorCustomer.Visible = True
                PnlCostTop = False
                IsCustomer = True
                lblCustomer.Text = "Accounts"
                If IsCustomer = True Then
                    'lblVendor.Visible = False
                    'cmbVendor.Visible = False
                    'lblEmployee.Visible = False
                    'cmbEmployee.Visible = False
                    'cmbProducationLocation.Visible = False
                    'lblProductionLocation.Visible = False
                    lblCustomer.Visible = True
                    cmbCustomer.Visible = True
                End If
                'FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable")
                FillUltraDropDown(cmbCustomer, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head], vwCOAdetail.Contact_Email as Email from vwCOADetail WHERE Account_Type <> 'Inventory' AND detail_title is not null " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & "")
                Me.cmbCustomer.Rows(0).Activate()
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Email").Hidden = True
            ElseIf Me.ReportName = ReportList.SummaryOfPurchaseInvoices Then
                Me.Text = "Summary Of Purchase Invoices"
                Dim str As String
                pnlVendorCustomer.Visible = True
                Me.pnlCost.Visible = True
                PnlCostTop = True
                Me.cmbCostCenter.Visible = True
                Me.lblCostCenter.Visible = True
                IsVendor = True
                If IsVendor = True Then
                    'Task:2609 unCommented line
                    Me.lblCustomer.Visible = False
                    Me.cmbCustomer.Visible = False
                    Me.lblEmployee.Visible = False
                    Me.cmbEmployee.Visible = False
                    cmbProducationLocation.Visible = False
                    lblProductionLocation.Visible = False
                    'End Task:2609
                    Me.lblVendor.Visible = True
                    Me.cmbVendor.Visible = True
                End If
                ''Start TFS2124
                str = "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE 1=1 " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & ""
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    str += " AND Account_Type in ('Vendor','Customer') "
                Else
                    str += " AND  Account_Type='Vendor' "
                End If
                ''End TFS2124
                FillUltraDropDown(cmbVendor, str)

                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
                Me.cmbVendor.Rows(0).Activate()
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf ReportName = ReportList.MonthlyPurchaseSummary Then
                Me.Text = "Monthly Purchase Summary"
                pnlVendorCustomer.Visible = True
                IsVendor = True
                If IsVendor = True Then
                    'Task:2609 unCommented line
                    Me.lblCustomer.Visible = False
                    Me.cmbCustomer.Visible = False
                    Me.lblEmployee.Visible = False
                    Me.cmbEmployee.Visible = False
                    cmbProducationLocation.Visible = False
                    lblProductionLocation.Visible = False
                    'End Task:2609
                    Me.lblVendor.Visible = True
                    Me.cmbVendor.Visible = True
                End If

                FillUltraDropDown(cmbVendor, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE Account_Type='Vendor' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & "")
                Me.cmbVendor.Rows(0).Activate()
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Me.ReportName = ReportList.SummaryOfPurchaseReturn Then
                Me.Text = "Summary Of Purchase Return"
                Dim str As String
                pnlVendorCustomer.Visible = True
                Me.pnlCost.Visible = True
                PnlCostTop = True
                Me.cmbCostCenter.Visible = True
                Me.lblCostCenter.Visible = True
                IsVendor = True
                If IsVendor = True Then
                    'Task:2609 unCommented line
                    Me.lblCustomer.Visible = False
                    Me.cmbCustomer.Visible = False
                    Me.lblEmployee.Visible = False
                    Me.cmbEmployee.Visible = False
                    cmbProducationLocation.Visible = False
                    lblProductionLocation.Visible = False
                    'End Task:2609
                    Me.lblVendor.Visible = True
                    Me.cmbVendor.Visible = True
                End If
                ''Start TFS2124
                str = "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE 1=1 " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & ""
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    str += " AND Account_Type in ('Vendor','Customer') "
                Else
                    str += " AND  Account_Type='Vendor' "
                End If
                ''End TFS2124
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
                FillUltraDropDown(cmbVendor, str)
                Me.cmbVendor.Rows(0).Activate()
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                '''''''''''''''''''
            ElseIf Me.ReportName = ReportList.ServicesInvoice Then
                Me.Text = "Services Invoice"
                FillDropDown(Me.cmbCashAccount, "Select coa_detail_id, detail_title From vwCOADetail WHERE  Active=1  AND Account_Type='Customer'")
                FillDropDown(Me.cmbCostCenter, "select docid,docno from ServicesInvoiceMasterTable  a ,vwcoadetail b where a.customercode = b.coa_detail_id")
                Me.pnlInvType.Visible = True
                Me.pnlVendorCustomer.Visible = True
                ''''''''''''''
            ElseIf Me.ReportName = ReportList.CashFlowStatment Then
                Me.Text = "Cash & Bank Statment"
                Me.pnlVendorCustomer.Visible = True
                Me.pnlCost.Visible = True
                PnlCostTop = True
                Me.cmbCostCenter.Visible = True
                Me.lblCostCenter.Visible = True
                Me.chkIncludeCheque.Visible = True
                Me.chkUnposted.Visible = True
                Me.pnlCashBank.Visible = True
                Me.cmbCashAccount.Visible = True
                Me.lblAccount.Visible = True
                Me.lblCCGroup.Visible = True
                Me.cmbCCGroup.Visible = True
                'Me.cmbCompany.Visible = False
                'Me.lblCompany.Visible = False
                'Me.cmbCustomer.Visible = False
                'Me.lblCustomer.Visible = False
                Dim strAccounType As String = String.Empty
                If Me.rdoCash.Checked = True Then
                    strAccounType = "Cash"
                ElseIf rdoBank.Checked = True Then
                    strAccounType = "Bank"
                ElseIf Me.rdoBoth.Checked = True Then
                    strAccounType = ""
                End If
                FillDropDown(Me.cmbCashAccount, "Select coa_detail_id, detail_title From vwCOADetail WHERE  Active=1 " & IIf(strAccounType.Length > 0, " AND Account_Type='" & strAccounType.ToString & "'", " AND Account_Type IN('Cash','Bank')") & "")
                '' "Select distinct CostCenterGroup, CostCenterGroup As [Cost Center Group] from tbldefCostCenter Where CostCenterGroup <> ''"
                FillDropDown(Me.cmbCCGroup, "Select distinct CostCenterGroup, CostCenterGroup As [Cost Center Group] from tbldefCostCenter Where CostCenterGroup <> ''", True)
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            ElseIf Me.ReportName = ReportList.PLNotesDetail Then
                Me.Text = "Profit & Loss Notes Detail"
                Me.pnlCost.Visible = True
                PnlCostTop = False
                Me.lblCostCenter.Visible = True 'TASKM29 Add Costcenter
                Me.cmbCostCenter.Visible = True 'TASKM29 Add Costcenter
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            ElseIf Me.ReportName = ReportList.CashFlowStatmentStander Then
                Me.Text = "Cash flow statment standard"
                'Me.pnlVendorCustomer.Visible = True
                Me.pnlCost.Visible = True
                Me.chkUnposted.Visible = True
                Me.cmbCostCenter.Visible = True
                Me.lblCostCenter.Visible = True
                Me.chkIncludeCheque.Visible = True
                Me.cbExcludeTax.Visible = True
                'Task:2850 Display Purchase Item Summary
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            ElseIf Me.ReportName = ReportList.PurchaseItemSummary Then
                Me.Text = "Purchase Item Summary"
                'End Task:2850
            ElseIf Me.ReportName = ReportList.ItemLedger Then
                Me.Text = "Item Ledger"
            ElseIf Me.ReportName = ReportList.PLSingleDate Then
                Me.Text = "Profit & Loss"
                Me.pnlCost.Visible = True
                PnlCostTop = False
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            ElseIf Me.ReportName = ReportList.voucherDetail Then
                Me.Text = "Voucher Detail"
            ElseIf Me.ReportName = ReportList.rptDiscountNetRate Then
                Me.Text = "Discount Net Rate"
            ElseIf Me.ReportName = ReportList.ReceivingReport Then
                Me.Text = "Daily Receiving Report"
            ElseIf Me.ReportName = ReportList.SummaryOfPurchaseReturn Then
                Me.Text = "Summary Of Purchase Return"
            ElseIf Me.ReportName = ReportList.rptExpenses Then
                Me.Text = "Expense Report"
                Me.pnlCost.Visible = True
                PnlCostTop = False
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            ElseIf Me.ReportName = ReportList.rptExpenseStatementStd Then
                Me.Text = "Expense Standard Report"
                Me.pnlCost.Visible = True
                PnlCostTop = False
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            ElseIf Me.ReportName = ReportList.rptBSFomated Then
                Me.Text = "Balance Sheet"
            ElseIf Me.ReportName = ReportList.DispatchStatus Then
                Me.Text = "Dispatch Status"
            ElseIf Me.ReportName = ReportList.RptSalesTaxDetail Then
                Me.Text = "Sales Tax Detail"
            ElseIf Me.ReportName = ReportList.RptSalesTaxSummary Then
                Me.Text = "Sales Tax Summary"
            ElseIf Me.ReportName = ReportList.DailySupply Then
                Me.Text = "Daily Supply & Gate Pass"
            ElseIf Me.ReportName = ReportList.AttendanceSummary Then
                Me.Text = "Employees Attendance Summary"
            ElseIf Me.ReportName = ReportList.EmployeeAttendanceDetail Then
                Me.Text = "Employees Attendance Detail"
                Me.lblCostCenter.Visible = True
                Me.cmbCostCenter.Visible = True
                If Me.pnlCost.Visible = False Then Me.pnlCost.Visible = True
                IsEmployee = True
                If IsEmployee = True Then
                    'Me.pnlCost.Visible = False
                    Me.pnlVendorCustomer.Visible = True
                    Me.pnlEmployee.Visible = True
                    Me.lblCustomer.Visible = False
                    Me.lblVendor.Visible = False
                    Me.cmbCustomer.Visible = False
                    Me.cmbVendor.Visible = False
                    cmbProducationLocation.Visible = False
                    lblProductionLocation.Visible = False
                    Me.lblEmployee.Visible = True
                    Me.cmbEmployee.Visible = True
                    FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
                End If
                FillUltraDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name,Father_Name,Employee_Code From EmployeesView")
                Me.cmbEmployee.Rows(0).Activate()
                If Me.cmbEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbEmployee.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            ElseIf Me.ReportName = ReportList.DemandSales Then
                Me.Text = "Demand Sales"
            ElseIf Me.ReportName = ReportList.WeightReport Then
                Me.Text = "Weight Report"
                'ElseIf Me.ReportName = ReportList.EmployeesTask Then
                '    Me.Text = "Employee Wise Task"
            ElseIf Me.ReportName = ReportList.BalanceSheetNotesSummary Then
                Me.Text = "Balance Sheet Notes Summary"
            ElseIf Me.ReportName = ReportList.ReturnableGatepass Then
                Me.Text = "Returnable Gatepass"
            ElseIf Me.ReportName = ReportList.PrintVehicleLog Then
                Me.Text = "Print Vehicle Log"
            ElseIf Me.ReportName = ReportList.ItemsDetailReport Then
                Me.Text = "Items Detail Report"
            ElseIf Me.ReportName = ReportList.DailySalarySheet Then
                Me.Text = "Daily Salary Sheet"
                Me.lblCostCenter.Visible = True
                Me.cmbCostCenter.Visible = True
                If Me.pnlCost.Visible = False Then Me.pnlCost.Visible = True
                IsEmployee = True
                If IsEmployee = True Then
                    Me.pnlVendorCustomer.Visible = True
                    Me.lblCustomer.Visible = False
                    Me.lblVendor.Visible = False
                    Me.cmbCustomer.Visible = False
                    Me.cmbVendor.Visible = False
                    cmbProducationLocation.Visible = False
                    lblProductionLocation.Visible = False
                    Me.lblEmployee.Visible = True
                    Me.cmbEmployee.Visible = True
                    FillDropDown(Me.cmbCostCenter, "Select CostCenterId, Name as CostCenter from tblDefCostCenter")
                    'FillDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name From tblDefEmployee")
                    FillUltraDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name,Father_Name,Employee_Code From EmployeesView")
                    Me.cmbEmployee.Rows(0).Activate()
                    If Me.cmbEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                        Me.cmbEmployee.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    End If
                End If
                'Task:2864 Setting For Cash Receipt Detail Report
            ElseIf Me.ReportName = ReportList.CashReceiptDetailAgainstEmployee Then
                Me.Text = "Cash Receipts Detail Against Employee"
                Me.pnlCost.Visible = True
                IsEmployee = True
                If IsEmployee = True Then
                    Me.pnlVendorCustomer.Visible = True
                    Me.lblCustomer.Visible = False
                    Me.lblVendor.Visible = False
                    Me.cmbCustomer.Visible = False
                    Me.cmbVendor.Visible = False
                    cmbProducationLocation.Visible = False
                    lblProductionLocation.Visible = False
                    Me.lblEmployee.Visible = True
                    Me.cmbEmployee.Visible = True
                    FillDropDown(Me.cmbCostCenter, "Select CostCenterId, Name as CostCenter from tblDefCostCenter")
                    FillUltraDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name,Father_Name,Employee_Code From EmployeesView")
                    Me.cmbEmployee.Rows(0).Activate()
                    If Me.cmbEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                        Me.cmbEmployee.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    End If
                End If
                'End Task:2864
            ElseIf Me.ReportName = ReportList.EmpSalarySheet Then
                Me.Text = "Employee Salary Sheet"
            ElseIf Me.ReportName = ReportList.DailySalarySheetSummary Then
                Me.Text = "Daily Salary Sheet Summary"
                Me.lblCostCenter.Visible = True
                Me.cmbCostCenter.Visible = True
                If Me.pnlCost.Visible = False Then Me.pnlCost.Visible = True
                IsEmployee = True
                If IsEmployee = True Then
                    Me.pnlVendorCustomer.Visible = True
                    Me.lblCustomer.Visible = False
                    Me.lblVendor.Visible = False
                    Me.cmbCustomer.Visible = False
                    Me.cmbVendor.Visible = False
                    cmbProducationLocation.Visible = False
                    lblProductionLocation.Visible = False
                    Me.lblEmployee.Visible = True
                    Me.cmbEmployee.Visible = True
                    FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
                    'FillDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name From tblDefEmployee")
                    FillUltraDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name,Father_Name,Employee_Code From EmployeesView")
                    Me.cmbEmployee.Rows(0).Activate()
                    If Me.cmbEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                        Me.cmbEmployee.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    End If
                End If
            ElseIf Me.ReportName = ReportList.SalesReturnSummary Then
                Me.Text = "Sales Return Summary"
                pnlVendorCustomer.Visible = True
                PnlCostTop = True
                IsCustomer = True
                'Me.pnlInvType.Visible = False
                If IsCustomer = True Then
                    lblVendor.Visible = False
                    cmbVendor.Visible = False
                    lblEmployee.Visible = False
                    cmbEmployee.Visible = False
                    cmbProducationLocation.Visible = False
                    lblProductionLocation.Visible = False
                    lblCustomer.Visible = True
                    cmbCustomer.Visible = True
                    Me.pnlInvType.Visible = True
                    Me.pnlCustomerType.Visible = True
                    Me.lblCustomerType.Visible = True
                    Me.cmbCustomerType.Visible = True
                End If
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & "")
                FillDropDown(Me.cmbCustomerType, "Select Typeid, Name From TblDefCustomerType Where Active=1")
                FillUltraDropDown(cmbCustomer, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE Account_Type='Customer' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & "")
                Me.cmbCustomer.Rows(0).Activate()
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf ReportName = ReportList.ProductionSummary Then
                Me.Text = "Production Summary"
                pnlVendorCustomer.Visible = True
                Me.Button1.Visible = False
                If pnlCost.Visible = False Then pnlCost.Visible = True

                IsProduction = True
                If IsProduction = True Then
                    lblVendor.Visible = False
                    cmbVendor.Visible = False
                    Me.pnlEmployee.Visible = True
                    lblEmployee.Visible = True
                    cmbEmployee.Visible = True
                    cmbProducationLocation.Visible = True
                    lblProductionLocation.Visible = True
                    lblCustomer.Visible = False
                    cmbCustomer.Visible = False
                    FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
                    'Comment against Task:2502
                    'FillDropDown(Me.cmbProducationLocation, "Select Location_Id, Location_Name from tblDefLocation WHERE (Location_Type='Production' Or Location_Type='WIP')")
                    'Task:2502 ReMove Filter
                    FillDropDown(Me.cmbProducationLocation, "Select Location_Id, Location_Name from tblDefLocation")
                    FillUltraDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name, Father_Name, Employee_Code From EmployeesView")
                    Me.cmbEmployee.Rows(0).Activate()
                    If Me.cmbEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                        Me.cmbEmployee.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    End If
                    'End Task:2502
                End If
            ElseIf ReportName = ReportList.DailyEmployeeAttendance Then
                Me.Text = "Daily Employee Attendance"
                Me.cmbCostCenter.Visible = True
                Me.lblCostCenter.Visible = True
                If Me.pnlCost.Visible = False Then Me.pnlCost.Visible = True
                IsEmployee = True
                If IsEmployee = True Then
                    'Me.pnlCost.Visible = True
                    Me.pnlVendorCustomer.Visible = True
                    Me.pnlEmployee.Visible = True
                    Me.lblCustomer.Visible = False
                    Me.lblVendor.Visible = False
                    Me.cmbCustomer.Visible = False
                    Me.cmbVendor.Visible = False
                    cmbProducationLocation.Visible = False
                    lblProductionLocation.Visible = False
                    Me.lblEmployee.Visible = True
                    Me.cmbEmployee.Visible = True
                    FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
                    'FillDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name From tblDefEmployee")
                    FillUltraDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name,Father_Name,Employee_Code From EmployeesView")
                    Me.cmbEmployee.Rows(0).Activate()
                    If Me.cmbEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                        Me.cmbEmployee.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    End If
                End If
            ElseIf ReportName = ReportList.WHTaxCertificate Then
                Me.Text = "With Holding Tax Certificate"
                pnlVendorCustomer.Visible = True
                IsVendor = True
                If IsVendor = True Then
                    Me.lblCustomer.Visible = False
                    Me.cmbCustomer.Visible = False
                    Me.lblEmployee.Visible = False
                    Me.cmbEmployee.Visible = False
                    cmbProducationLocation.Visible = False
                    lblProductionLocation.Visible = False
                    Me.lblVendor.Visible = True
                    Me.cmbVendor.Visible = True
                End If
                cmbVendor.DataSource = Nothing
                FillUltraDropDown(cmbVendor, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE Account_Type='Vendor' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & "")
                Me.cmbVendor.Rows(0).Activate()
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'Task:2506 Set Status Report Store Issaunce Detail Batch Wise
            ElseIf ReportName = ReportList.StoreIssuanceDetailBatchWise Then
                Me.Text = "Store Issuance Detail Batch Wise"
                'End Task:2506
                'Task:2522 Added Report Sales Certificate Issued.
            ElseIf ReportName = ReportList.SalesCertificateIssued Then
                Me.Text = "Issued Sales Certificate"
                pnlVendorCustomer.Visible = True
                Me.pnlInvType.Visible = True
                PnlCostTop = False
                IsCustomer = True
                If IsCustomer = True Then
                    Me.cmbCompany.Visible = True
                    Me.lblCompany.Visible = True
                    lblCustomer.Visible = True
                    cmbCustomer.Visible = True
                End If
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & " ")
                FillUltraDropDown(cmbCustomer, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE Account_Type='Customer' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " ORDER BY 2 ASC")
                Me.cmbCustomer.Rows(0).Activate()
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'End Task:2522

                'Task:2810 Report Setting And Fill Dropdown CostCenter
            ElseIf ReportName = ReportList.PLSubsubAccountSummary Then
                Me.Text = "PL Sub Sub Account Summary"
                Me.cmbCostCenter.Visible = True
                Me.lblCostCenter.Visible = True
                Me.pnlCost.Visible = True
                PnlCostTop = False
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            ElseIf ReportName = ReportList.PLDetailAccountSummary Then
                Me.Text = "PL Detail Account Summary"
                Me.pnlCost.Visible = True
                Me.cmbCostCenter.Visible = True
                Me.lblCostCenter.Visible = True
                Me.pnlCost.Visible = True
                PnlCostTop = False
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
                'Task#18082015 by Ahmad Sharif
            ElseIf ReportName = ReportList.Production Then
                Me.Text = "Summary Of Production"
                Me.pnlInvType.Visible = False
                Me.pnlVendorCustomer.Visible = False
                Me.pnlCashBank.Visible = False
                Me.pnlSalesType.Visible = False
                Me.pnlEmployee.Visible = True
                Me.lblEmployee.Visible = True
                Me.cmbEmployee.Visible = True
                Me.Button1.Visible = False
                FillUltraDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name, Father_Name, Employee_Code From EmployeesView")
                Me.cmbEmployee.Rows(0).Activate()
                If Me.cmbEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbEmployee.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
                'End Task#18082015
            End If
            'End Task:2810

            'If Me.pnlCost.Visible = False Then
            '    Me.pnlCost.Location = New Point(11, 2)
            '    Me.pnlVendorCustomer.Location = New Point(11, 35)
            'Else
            '    Me.pnlCost.Location = New Point(11, 35)
            'End If

            'If PnlCostTop = True Then
            '    Me.pnlCost.Location = New Point(11, 2)
            'Else
            '    Me.pnlCost.Location = New Point(11, 35)
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.Button1.Enabled = True
                Me.OK_Button.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.Button1.Enabled = False
                    Me.OK_Button.Enabled = False
                    Exit Sub
                End If
            Else
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Show" Then
                        Me.OK_Button.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.Button1.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Sub CallShowReport(Optional ByVal Print As Boolean = False)
        Try


            Me.Cursor = Cursors.WaitCursor
            Dim fromDate As String = Me.DateTimePicker1.Value.Year & "," & Me.DateTimePicker1.Value.Month & "," & Me.DateTimePicker1.Value.Day & ",0,0,0"
            Dim ToDate As String = Me.DateTimePicker2.Value.Year & "," & Me.DateTimePicker2.Value.Month & "," & Me.DateTimePicker2.Value.Day & ",23,59,59"
            If Me.ReportName = ReportList.SummaryOfSalesInvoices Then
                AddRptParam("FromDate", Me.DateTimePicker1.Value.Date)
                AddRptParam("ToDate", Me.DateTimePicker2.Value.Date)
                Dim CostCentre As Integer = IIf(Me.cmbCostCenter.SelectedIndex = -1, 0, Me.cmbCostCenter.SelectedValue)


                'Taks:2851 Before 
                'If cmbCustomer.Visible = True Then
                '    If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                '        ShowReport("SummaryOfInvoices", "{SalesMasterTable.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SalesMasterTable.CustomerCode}=" & Me.cmbCustomer.Value & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SalesMasterTable.LocationId}=" & Me.cmbCompany.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                '    Else
                '        ShowReport("SummaryOfInvoices", "{SalesMasterTable.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")" & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SalesMasterTable.LocationId}=" & Me.cmbCompany.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                '    End If
                'Else
                '    ShowReport("SummaryOfInvoices", "{SalesMasterTable.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")" & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SalesMasterTable.LocationId}=" & Me.cmbCompany.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                'End If
                ''23-Sep-2014 TASK:2851 Imran Ali SummaryOfInvoices Report Revised
                Dim strInvoiceTypeFilter As String = String.Empty
                If cmbCustomer.Visible = True Then
                    If Me.pnlSalesType.Visible = True Then
                        If Me.rbtSalesTypeBoth.Checked = True Then
                            strInvoiceTypeFilter += " AND {SP_SalesOfInvoiceSummary;1.InvoiceType} in ['Cash', 'Credit','']"
                        ElseIf Me.rbtCreditSales.Checked = True Then
                            strInvoiceTypeFilter += " AND {SP_SalesOfInvoiceSummary;1.InvoiceType} = 'Credit'"
                        ElseIf Me.rbtCashSales.Checked = True Then
                            strInvoiceTypeFilter += " AND {SP_SalesOfInvoiceSummary;1.InvoiceType} = 'Cash'"
                        End If
                    End If

                    'IIf(Me.pnlSalesType.Visible = False, "" & IIf(Me.rbtSalesTypeBoth.Checked = True, "  " & IIf(Me.rbtCreditSales.Checked = True, " AND {SP_SalesOfInvoiceSummary;1.InvoiceType}=0 " & IIf(Me.rbtCashSales.Checked = True, " AND {SP_SalesOfInvoiceSummary;1.InvoiceType} =1  ", ""), ""), ""), "") & ""
                    If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                        'ShowReport("SummaryOfInvoices", "{SP_SalesOfInvoiceSummary;1.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SP_SalesOfInvoiceSummary;1.CustomerCode}=" & Me.cmbCustomer.Value & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.LocationId}=" & Me.cmbCompany.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                        If CostCentre > 0 Then
                            ShowReport("SummaryOfInvoices", "{SP_SalesOfInvoiceSummary;1.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") " & strInvoiceTypeFilter & " AND {SP_SalesOfInvoiceSummary;1.CostCenterID} =" & CostCentre & "  AND {SP_SalesOfInvoiceSummary;1.CustomerCode} =" & Me.cmbCustomer.Value & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.LocationId} =" & Me.cmbCompany.SelectedValue & "", "") & " " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.CustomerTypeID} =" & Me.cmbCustomerType.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                        Else
                            ShowReport("SummaryOfInvoices", "{SP_SalesOfInvoiceSummary;1.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") " & strInvoiceTypeFilter & "  AND {SP_SalesOfInvoiceSummary;1.CustomerCode} =" & Me.cmbCustomer.Value & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.LocationId} =" & Me.cmbCompany.SelectedValue & "", "") & " " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.CustomerTypeID} =" & Me.cmbCustomerType.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                        End If
                    Else
                        If CostCentre > 0 Then
                            ShowReport("SummaryOfInvoices", "{SP_SalesOfInvoiceSummary;1.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SP_SalesOfInvoiceSummary;1.CostCenterID} =" & CostCentre & " " & strInvoiceTypeFilter & " " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.LocationId} =" & Me.cmbCompany.SelectedValue & "", "") & " " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.CustomerTypeID} =" & Me.cmbCustomerType.SelectedValue & "", ""), "Nothing", "Nothing", Print)

                        Else
                            ShowReport("SummaryOfInvoices", "{SP_SalesOfInvoiceSummary;1.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") " & strInvoiceTypeFilter & " " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.LocationId} =" & Me.cmbCompany.SelectedValue & "", "") & " " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.CustomerTypeID} =" & Me.cmbCustomerType.SelectedValue & "", ""), "Nothing", "Nothing", Print)

                        End If

                    End If
                Else
                    If CostCentre > 0 Then
                        ShowReport("SummaryOfInvoices", "{SP_SalesOfInvoiceSummary;1.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SP_SalesOfInvoiceSummary;1.CostCenterID} =" & CostCentre & " " & strInvoiceTypeFilter & " " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.LocationId} =" & Me.cmbCompany.SelectedValue & "", "") & " " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.CustomerTypeID} =" & Me.cmbCustomerType.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                    Else
                        ShowReport("SummaryOfInvoices", "{SP_SalesOfInvoiceSummary;1.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") " & strInvoiceTypeFilter & " " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.LocationId} =" & Me.cmbCompany.SelectedValue & "", "") & " " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SP_SalesOfInvoiceSummary;1.CustomerTypeID} =" & Me.cmbCustomerType.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                    End If
                End If
                'End Task:2851

            ElseIf ReportName = ReportList.AdvanceReceiptsSO Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59"))
                AddRptParam("@AccountId", Me.cmbCustomer.Value)
                ShowReport("rptAdvanceReceipts")
            ElseIf ReportName = ReportList.AdvancePaymentsPO Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59"))
                AddRptParam("@AccountId", Me.cmbVendor.Value)
                ShowReport("rptAdvancePayments")

            ElseIf Me.ReportName = ReportList.WarrantyDetailReport Then

                Dim strInvoiceTypeFilter As String = String.Empty

                strInvoiceTypeFilter = "{SP_WarrantyClaimDetail;1.DocNo} <> ''"

                If Me.cmbCompany.SelectedIndex > 0 Then
                    strInvoiceTypeFilter += " AND {SP_WarrantyClaimDetail;1.LocationId}=" & Me.cmbCompany.SelectedValue & ""
                End If
                If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                    strInvoiceTypeFilter += " AND {SP_WarrantyClaimDetail;1.CustomerCode}=" & Me.cmbCustomer.Value & ""
                End If

                AddRptParam("@FromDate", Me.DateTimePicker1.Value.Date)
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.Date)
                ShowReport("rptWarrantyClaimDetail", strInvoiceTypeFilter.ToString())

            ElseIf Me.ReportName = ReportList.rptSummaryOfSalesTaxInvoices Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.Date)
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.Date)
                Dim CostCentre As Integer = IIf(Me.cmbCostCenter.SelectedIndex = -1, 0, Me.cmbCostCenter.SelectedValue)
                If cmbCustomer.Visible = True Then
                    If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                        If CostCentre > 0 Then
                            ShowReport("rptSummaryOfSalesTaxInvoices", "{SP_SummaryOfSalesTaxInvoices;1.CustomerCode}=" & Me.cmbCustomer.Value & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SP_SummaryOfSalesTaxInvoices;1.LocationId}=" & Me.cmbCompany.SelectedValue & "", "") & " AND {SP_SummaryOfSalesTaxInvoices;1.CostCenterId}=" & CostCentre & "", "Nothing", "Nothing", Print)
                        Else
                            ShowReport("rptSummaryOfSalesTaxInvoices", "{SP_SummaryOfSalesTaxInvoices;1.CustomerCode}=" & Me.cmbCustomer.Value & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SP_SummaryOfSalesTaxInvoices;1.LocationId}=" & Me.cmbCompany.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                        End If
                    Else
                        If CostCentre > 0 Then
                            ShowReport("rptSummaryOfSalesTaxInvoices", "{SP_SummaryOfSalesTaxInvoices;1.CostCenterId}=" & CostCentre & "" & IIf(Me.cmbCompany.SelectedIndex > 0, " {SP_SummaryOfSalesTaxInvoices;1.LocationId}=" & Me.cmbCompany.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                        Else
                            ShowReport("rptSummaryOfSalesTaxInvoices", IIf(Me.cmbCompany.SelectedIndex > 0, " {SP_SummaryOfSalesTaxInvoices;1.LocationId}=" & Me.cmbCompany.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                        End If
                    End If
                Else
                    If CostCentre > 0 Then
                        ShowReport("rptSummaryOfSalesTaxInvoices", "{SP_SummaryOfSalesTaxInvoices;1.CostCenterId}=" & CostCentre & "" & IIf(Me.cmbCompany.SelectedIndex > 0, " {SP_SummaryOfSalesTaxInvoices;1.LocationId}=" & Me.cmbCompany.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                    Else
                        ShowReport("rptSummaryOfSalesTaxInvoices", IIf(Me.cmbCompany.SelectedIndex > 0, " {SP_SummaryOfSalesTaxInvoices;1.LocationId}=" & Me.cmbCompany.SelectedValue & "", ""), "Nothing", "Nothing", Print)
                    End If
                End If
            ElseIf Me.ReportName = ReportList.NetSalesReport Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.Date)
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.Date)
                ShowReport("rptNetSales", , , , , , , GetNetSalesReportData)
            ElseIf Me.ReportName = ReportList.DeliveryChalanDetail Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.Date)
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.Date)
                ShowReport("rptDeliveryChalanDetail", "{SP_DeliveryChalanDetail;1.DeliveryDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")" & IIf(Me.cmbCustomer.ActiveRow.Cells(0).Value > 0, " AND {SP_DeliveryChalanDetail;1.CustomerCode} = " & Me.cmbCustomer.Value & "" & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SP_DeliveryChalanDetail;1.CompanyId} = " & Me.cmbCompany.SelectedValue, ""), ""), "Nothing", "Nothing")
            ElseIf Me.ReportName = ReportList.DeliveryChalanSummary Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.Date)
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.Date)
                AddRptParam("@FromDate_1", Me.DateTimePicker1.Value.Date)
                AddRptParam("@ToDate_1", Me.DateTimePicker2.Value.Date)
                ShowReport("rptDeliveryChalanSummary", "{SP_DeliveryChalanSummary;1.detail_title} <> '' " & IIf(Me.cmbCustomer.ActiveRow.Cells(0).Value > 0, " AND {SP_DeliveryChalanSummary;1.CustomerCode} = " & Me.cmbCustomer.Value & "" & IIf(Me.cmbCompany.SelectedIndex > 0, " AND {SP_DeliveryChalanSummary;1.CompanyId} = " & Me.cmbCompany.SelectedValue, ""), ""), "Nothing", "Nothing")
            ElseIf Me.ReportName = ReportList.DamageBudget Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
                ShowReport("rptDamageBudget", IIf(Me.cmbCustomer.ActiveRow.Cells(0).Value > 0, "{SP_Damage_Budget;1.coa_detail_id} = " & Me.cmbCustomer.Value & "", "") & "")
            ElseIf Me.ReportName = ReportList.LedgerByInvoices Then
                If Me.cmbCustomer.ActiveRow.Cells(0).Value = 0 Then
                    ShowErrorMessage("Please select customer")
                    Me.cmbCustomer.Focus()
                    Exit Sub
                End If
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
                AddRptParam("@AccountId", Me.cmbCustomer.Value)
                ShowReport("rptLedgerByInvoices", , , , , , , , , , , Me.cmbCustomer.ActiveRow.Cells("Email").Value.ToString)
            ElseIf Me.ReportName = ReportList.DemandSummary Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59"))
                ShowReport("rptDemandSummary", IIf(Me.cmbCompany.SelectedIndex > 0, "{SP_DemandSummary;1.LocationId}=" & Me.cmbCompany.SelectedValue & "", "Nothing"))
            ElseIf Me.ReportName = ReportList.SummaryOfPurchaseInvoices Then
                'AddRptParam("@DateFrom", Me.DateTimePicker1.Value.Date.ToString("yyyy-MM-dd 00:00:00"))
                'AddRptParam("@DateTo", Me.DateTimePicker2.Value.Date.ToString("yyyy-MM-dd 23:59:59"))
                'If Me.cmbVendor.Visible = True Then
                '    If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then
                '        ShowReport("SummaryOfPurchaseInvoices", "{ReceivingMasterTable.ReceivingDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {ReceivingMasterTable.VendorId}=" & Me.cmbVendor.Value & "", "Nothing", "Nothing", Print)
                '    Else
                '        ShowReport("SummaryOfPurchaseInvoices", "{ReceivingMasterTable.ReceivingDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing", Print)
                '    End If
                'Else
                '    ShowReport("SummaryOfPurchaseInvoices", "{ReceivingMasterTable.ReceivingDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing", Print)
                'End If

                AddRptParam("@DateFrom", Me.DateTimePicker1.Value.Date.ToString("yyyy-MM-dd 00:00:00"))
                AddRptParam("@DateTo", Me.DateTimePicker2.Value.Date.ToString("yyyy-MM-dd 23:59:59"))
                Dim CostCentre As Integer = IIf(Me.cmbCostCenter.SelectedIndex = -1, 0, Me.cmbCostCenter.SelectedValue)
                If Me.cmbVendor.Visible = True Then
                    If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then
                        If CostCentre > 0 Then
                            ShowReport("SummaryOfPurchaseInvoices", "{SP_SummaryOfPurchaseInvoices;1.ReceivingDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SP_SummaryOfPurchaseInvoices;1.VendorId}=" & Me.cmbVendor.Value & " AND {SP_SummaryOfPurchaseInvoices;1.CostCenterId}=" & CostCentre & "", "Nothing", "Nothing", Print)
                        Else
                            ShowReport("SummaryOfPurchaseInvoices", "{SP_SummaryOfPurchaseInvoices;1.ReceivingDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SP_SummaryOfPurchaseInvoices;1.VendorId}=" & Me.cmbVendor.Value & "", "Nothing", "Nothing", Print)
                        End If
                    Else
                        If CostCentre > 0 Then
                            ShowReport("SummaryOfPurchaseInvoices", "{SP_SummaryOfPurchaseInvoices;1.ReceivingDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SP_SummaryOfPurchaseInvoices;1.CostCenterId}=" & CostCentre & "", "Nothing", "Nothing", Print)
                        Else
                            ShowReport("SummaryOfPurchaseInvoices", "{SP_SummaryOfPurchaseInvoices;1.ReceivingDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing", Print)
                        End If
                    End If
                Else
                    If CostCentre > 0 Then
                        ShowReport("SummaryOfPurchaseInvoices", "{SP_SummaryOfPurchaseInvoices;1.ReceivingDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SP_SummaryOfPurchaseInvoices;1.CostCenterId}=" & CostCentre & "", "Nothing", "Nothing", Print)
                    Else
                        ShowReport("SummaryOfPurchaseInvoices", "{SP_SummaryOfPurchaseInvoices;1.ReceivingDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing", Print)
                    End If
                End If
            ElseIf ReportName = ReportList.MonthlyPurchaseSummary Then
                ShowReport("rptMonthlyPurchaseSummary", , , , , , , GetMonthlyPurchaseSummary)
            ElseIf Me.ReportName = ReportList.SummaryOfPurchaseReturn Then
                AddRptParam("@DateFrom", Me.DateTimePicker1.Value.Date.ToString("yyyy-MM-dd 00:00:00"))
                AddRptParam("@DateTo", Me.DateTimePicker2.Value.Date.ToString("yyyy-MM-dd 23:59:59"))
                'If Me.cmbVendor.Visible = True Then
                '    If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then
                '        ShowReport("SummaryOfPurchaseReturn", "{ReceivingMasterTable.PurchaseReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {ReceivingMasterTable.VendorId}=" & Me.cmbVendor.Value & "", "Nothing", "Nothing", Print)
                '    Else
                '        ShowReport("SummaryOfPurchaseReturn", "{ReceivingMasterTable.PurchaseReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing", Print)
                '    End If
                'Else
                '    ShowReport("SummaryOfPurchaseReturn", "{ReceivingMasterTable.PurchaseReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing", Print)
                'nn()                'End If
                Dim CostCentre As Integer = IIf(Me.cmbCostCenter.SelectedIndex = -1, 0, Me.cmbCostCenter.SelectedValue)
                If Me.cmbVendor.Visible = True Then
                    If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then
                        If CostCentre > 0 Then
                            ShowReport("SummaryOfPurchaseReturn", "{SP_SummaryOfPurchaseReturnInvoices;1.PurchaseReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SP_SummaryOfPurchaseReturnInvoices;1.VendorId}=" & Me.cmbVendor.Value & " AND {SP_SummaryOfPurchaseReturnInvoices;1.CostCenterId}=" & CostCentre & "", "Nothing", "Nothing", Print)
                        Else
                            ShowReport("SummaryOfPurchaseReturn", "{SP_SummaryOfPurchaseReturnInvoices;1.PurchaseReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SP_SummaryOfPurchaseReturnInvoices;1.VendorId}=" & Me.cmbVendor.Value & "", "Nothing", "Nothing", Print)
                        End If
                    Else
                        If CostCentre > 0 Then
                            ShowReport("SummaryOfPurchaseReturn", "{SP_SummaryOfPurchaseReturnInvoices;1.PurchaseReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SP_SummaryOfPurchaseReturnInvoices;1.CostCenterId}=" & CostCentre & "", "Nothing", "Nothing", Print)

                        Else
                            ShowReport("SummaryOfPurchaseReturn", "{SP_SummaryOfPurchaseReturnInvoices;1.PurchaseReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing", Print)

                        End If
                    End If
                Else
                    If CostCentre > 0 Then
                        ShowReport("SummaryOfPurchaseReturn", "{SP_SummaryOfPurchaseReturnInvoices;1.PurchaseReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SP_SummaryOfPurchaseReturnInvoices;1.CostCenterId}=" & CostCentre & "", "Nothing", "Nothing", Print)

                    Else
                        ShowReport("SummaryOfPurchaseReturn", "{SP_SummaryOfPurchaseReturnInvoices;1.PurchaseReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing", Print)

                    End If
                End If
            ElseIf Me.ReportName = ReportList.CashFlowStatment Then

                ''TASK: TFS1212 Cost Center Group wise filter.
                If Me.cmbCCGroup.SelectedIndex > 0 AndAlso cmbCostCenter.SelectedIndex > 0 Then
                    CostCenterCriterial = " And tblVoucherDetail.CostCenterId = " & Me.cmbCostCenter.SelectedValue & ""
                    CostCenterCriterial1 = " And CostCenterID = " & Me.cmbCostCenter.SelectedValue & ""
                ElseIf Me.cmbCCGroup.SelectedIndex > 0 AndAlso cmbCostCenter.SelectedIndex <= 0 Then
                    CostCenterCriterial = " And tblVoucherDetail.CostCenterId In(Select CostCenterID From tbldefCostCenter Where CostCenterGroup='" & Me.cmbCCGroup.Text & "')"
                    CostCenterCriterial1 = " And CostCenterID In(Select CostCenterID From tbldefCostCenter Where CostCenterGroup='" & Me.cmbCCGroup.Text & "')"
                ElseIf Me.cmbCCGroup.SelectedIndex <= 0 AndAlso cmbCostCenter.SelectedIndex > 0 Then
                    CostCenterCriterial = " And tblVoucherDetail.CostCenterId = " & Me.cmbCostCenter.SelectedValue & ""
                    CostCenterCriterial1 = " And CostCenterID = " & Me.cmbCostCenter.SelectedValue & ""
                ElseIf Me.cmbCCGroup.SelectedIndex <= 0 AndAlso cmbCostCenter.SelectedIndex <= 0 Then
                    CostCenterCriterial = ""
                    CostCenterCriterial1 = ""
                End If
                ''END TASK TFS1212
                If Me.cmbCashAccount.SelectedIndex = -1 Then
                    ShowErrorMessage("Please define cash account")
                    Me.cmbCashAccount.Focus()
                    Exit Sub
                End If
                Dim opening As Integer = GetAccountOpeningBalance("" & IIf(Me.cmbCashAccount.SelectedIndex > 0, Me.cmbCashAccount.SelectedValue, 0) & "", Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00", IIf(rdoCash.Checked = True, "Cash" & IIf(Me.rdoBoth.Checked = True, "CashAndBank", ""), IIf(Me.rdoBank.Checked = True, "Bank", "CashAndBank")), IIf(Me.chkUnposted.Checked = True, True, False), IIf(Me.cmbCostCenter.SelectedValue > 0, cmbCostCenter.SelectedValue, 0), CostCenterCriterial1)
                AddRptParam("FromDate", Me.DateTimePicker1.Value)
                AddRptParam("ToDate", Me.DateTimePicker2.Value)
                AddRptParam("CostCenter", IIf(Me.cmbCostCenter.SelectedValue > 0, Me.cmbCostCenter.Text, " "))
                ShowReport("rptCashFlowStatementNew", , , , Print, Val(opening).ToString, , GetCashAndBankData, , , , , , "Cash And Bank Statement", "Date Form " & Me.DateTimePicker1.Value.ToString("dd/MMM/yyyy") & " Date To " & Me.DateTimePicker2.Value.ToString("dd/MMM/yyyy") & "")
                'ShowReport("rptCashFlowStatment", "{vw_cash_Flow.voucher_date} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")" & IIf(Me.cmbCostCenter.SelectedIndex > 0, " and {vw_cash_Flow.CostCenterID} =" & Me.cmbCostCenter.SelectedValue, ""), "Nothing", "Nothing", Print, Val(opening).ToString)
                'AddRptParam("@Account_Type", IIf(Me.rdoCash.Checked = True, "Cash" & IIf(Me.rdoBank.Checked = True, "Bank", ""), IIf(Me.rdoBoth.Checked = True, "Both", "")))
                'ShowReport("rptCashFlowStatment", IIf(Me.cmbCostCenter.SelectedIndex > 0, "{SP_Cash_AND_Bank_Statement;1.CostCenterId}=" & Me.cmbCostCenter.SelectedValue & "", ""), Me.DateTimePicker1.Value.Date.ToString("dd/MMM/yyyy"), Me.DateTimePicker2.Value.Date.ToString("dd/MMM/yyyy"), Print, Val(opening).ToString)
            ElseIf Me.ReportName = ReportList.CashFlowStatmentStander Then
                'Dim opening As Integer = GetAccountOpeningBalance(0,  Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
                If Me.cbExcludeTax.Checked = False Then
                    Me.FunAddReportCriteria()
                Else
                    Me.FunAddReportCriteriaExcludeTax()
                End If
                str_ReportParam = "fromdate|" & Me.DateTimePicker1.Value.ToString("dd/MMM/yyyy") & "&" & "todate|" & Me.DateTimePicker2.Value.ToString("dd/MMM/yyyy")
                AddRptParam("CostCenter", IIf(Me.cmbCostCenter.SelectedValue > 0, Me.cmbCostCenter.Text, " "))
                ShowReport("rptCashFlowStatmentStander", , , , , , , , , , , , , "Profit And Loss", "Date Form " & Me.DateTimePicker1.Value.ToString("dd/MMM/yyyy") & " Date To " & Me.DateTimePicker2.Value.ToString("dd/MMM/yyyy") & "")
            ElseIf Me.ReportName = ReportList.PrintVehicleLog Then


                'ShowReport("rptVehicleLog", , fromDate, ToDate, True)
                ShowReport("rptVehicleLog", "{SP_VehicleLog;1.LogDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") ")
            ElseIf Me.ReportName = ReportList.ItemLedger Then
                Dim opening As Integer = GetStockOpeningBalance(AccountId, Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
                ShowReport("rptItemLedger", "{vw_item_Ledger.SalesReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") and {vw_Item_Ledger.ArticleDefId} = " & AccountId & IIf(Me.strBatchNo <> String.Empty, " and {vw_Item_Ledger.BatchNo} = '" & strBatchNo & "'", "") & "", "Nothing", "Nothing", Print, Val(opening).ToString)
            ElseIf Me.ReportName = ReportList.PLSingleDate Then
                str_ReportParam = "@FromDate|" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "@ToDate|" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "@CostCenterID|" & Me.cmbCostCenter.SelectedValue & "@ExcludeClosing" & "|" & 0
                ShowReport("rptProftAndLossStatementSingleDate", , Me.DateTimePicker1.Value.Date.ToString("yyyy-M-d 00:00:00"), Me.DateTimePicker2.Value.Date.ToString("yyyy-M-d 23:59:59"), Print)
            ElseIf Me.ReportName = ReportList.PLNotesDetail Then
                '18-Mar-2014Imran Ali  TASK:2501  Add project filter on profit and loss detail reort
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59"))
                'End Task:2501
                If Not Me.cmbCostCenter.SelectedIndex = -1 AndAlso Me.cmbCostCenter.SelectedIndex > 0 Then
                    ShowReport("cRptprofitLossNotesDetail", "{spProfitLossNotesDetail;1.CostCenterID} = " & Me.cmbCostCenter.SelectedValue & "")
                Else
                    ShowReport("cRptprofitLossNotesDetail")
                End If
            ElseIf Me.ReportName = ReportList.PLComparison Then
                ShowReport("rptProftAndLossStatementComparison", , Me.DateTimePicker1.Value.ToString("yyyy-MM-dd"), Me.DateTimePicker2.Value.ToString("yyyy-MM-dd"), Print)
            ElseIf Me.ReportName = ReportList.voucherDetail Then
                Dim strStartDate As String
                Dim strEndDate As String
                With Me.DateTimePicker1.Value
                    strStartDate = "(" & .Year & ", " & .Month & ", " & .Day & ",00, 00, 00)"
                End With
                With Me.DateTimePicker2.Value
                    strEndDate = "(" & .Year & " , " & .Month & " , " & .Day & " ,23, 59, 59)"
                End With
                ShowReport("vocuherDetail", "{tblVoucher.voucher_date} in DateTime " & strStartDate & " to DateTime " & strEndDate & " ", "Nothing", "Nothing", Print)
            ElseIf Me.ReportName = ReportList.rptDiscountNetRate Then
                ShowReport("DiscountNetRates", "{SalesMasterTable.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing", Print)
            ElseIf Me.ReportName = ReportList.ReceivingReport Then
                ShowReport("Receiving", "{ReceivingMasterTable.ReceivingDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing", Print)
            ElseIf Me.ReportName = ReportList.rptExpenses Then
                Dim opening As Integer = 0 'GetAccountOpeningBalance(0, Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
                AddRptParam("FromDate", Me.DateTimePicker1.Value)
                AddRptParam("ToDate", Me.DateTimePicker2.Value)
                ShowReport("rptExpenseStatment", "{vw_Expenses.voucher_date} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " and {vw_Expenses.CostCenterID}=" & Me.cmbCostCenter.SelectedValue, ""), "Nothing", "Nothing", Print, Val(opening).ToString)
            ElseIf Me.ReportName = ReportList.rptExpenseStatementStd Then
                Dim opening As Integer = 0 'GetAccountOpeningBalance(0, Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
                ShowReport("rptExpenseStatmentStd", "{vw_cash_Flow.voucher_date} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")" & IIf(Me.cmbCostCenter.SelectedIndex > 0, " and {vw_cash_Flow.CostCenterID}=" & Me.cmbCostCenter.SelectedValue, ""), "Nothing", "Nothing", Print, Val(opening).ToString)
            ElseIf Me.ReportName = ReportList.rptBSFomated Then
                ShowReport("rptBSFormated", "Nothing", Me.DateTimePicker1.Value.Date, Me.DateTimePicker2.Value.Date, False)
            ElseIf Me.ReportName = ReportList.RptSalesTaxDetail Then
                ShowReport("rptSalesTaxDetail", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, False)
            ElseIf Me.ReportName = ReportList.RptSalesTaxSummary Then
                ShowReport("rptSalesTaxSummary", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, False)
            ElseIf Me.ReportName = ReportList.DailySupply Then
                ShowReport("rptDailySupply", "Nothing", Me.DateTimePicker1.Value.Date, Me.DateTimePicker2.Value.Date, False)
            ElseIf Me.ReportName = ReportList.AttendanceSummary Then
                ShowReport("rptEmployeesAttendanceSummary", "Nothing", Me.DateTimePicker1.Value.Date.ToString("yyyy-M-d 00:00:00"), Me.DateTimePicker2.Value.Date.ToString("yyyy-M-d 23:59:59"), False)
            ElseIf Me.ReportName = ReportList.DemandSales Then
                ShowReport("rptDemandSales", "Nothing", Me.DateTimePicker1.Value.Date, Me.DateTimePicker2.Value.Date, False)
            ElseIf Me.ReportName = ReportList.WeightReport Then
                ShowReport("rptWeight", "Nothing", Me.DateTimePicker1.Value.Date, Me.DateTimePicker2.Value.Date, False)
            ElseIf ReportName = ReportList.BalanceSheetNotesSummary Then
                ShowReport("rptBalanceSheetSummary", , Me.DateTimePicker1.Value.Date.ToString("dd/MMM/yyyy"), Me.DateTimePicker2.Value.Date.ToString("dd/MMM/yyyy"))
            ElseIf ReportName = ReportList.ReturnableGatepass Then
                ShowReport("rptReturnableGatepass", "Nothing", Me.DateTimePicker1.Value.Date.ToString("dd/MMM/yyyy"), Me.DateTimePicker2.Value.Date.ToString("dd/MMM/yyyy"), False)
            ElseIf ReportName = ReportList.ItemsDetailReport Then
                ShowReport("rptstocksummerybydate", "Nothing", Me.DateTimePicker1.Value.Date.ToString("yyyy-MM-dd"), Me.DateTimePicker2.Value.Date.ToString("yyyy-MM-dd"), False)
            ElseIf ReportName = ReportList.DailySalarySheet Then
                ShowReport("DailySalarySheet", IIf(Me.cmbCostCenter.SelectedIndex > 0, "{SP_DailySalary;1.CostCenterId}=" & Me.cmbCostCenter.SelectedValue & IIf(Me.cmbEmployee.ActiveRow.Cells(0).Value > 0, " and  {SP_DailySalary;1.EmployId}=" & Me.cmbEmployee.Value, ""), IIf(Me.cmbEmployee.ActiveRow.Cells(0).Value > 0, "{SP_DailySalary;1.EmployId}=" & Me.cmbEmployee.Value, "Nothing")), Me.DateTimePicker1.Value.Date, Me.DateTimePicker2.Value.Date)
                'Task:2864 Setting For Show Report Cash Receipt Detail
            ElseIf ReportName = ReportList.CashReceiptDetailAgainstEmployee Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59"))
                ShowReport("rptCashReceiptDetailAgainstEmployee", IIf(Me.cmbEmployee.ActiveRow.Cells(0).Value > 0, "{SP_CashReceiptAgainstEmployee;1.Employee_Id}=" & Me.cmbEmployee.Value & "", ""))
                'End Taks:2864
            ElseIf ReportName = ReportList.DailySalarySheetSummary Then
                ShowReport("rptDailySalarySheetSummary", IIf(Me.cmbCostCenter.SelectedIndex > 0, "{SP_DailySalarySummary.CostCenterId}=" & Me.cmbCostCenter.SelectedValue & IIf(Me.cmbEmployee.ActiveRow.Cells(0).Value > 0, " and  {SP_DailySalarySummary.EmployId}=" & Me.cmbEmployee.Value, ""), IIf(Me.cmbEmployee.ActiveRow.Cells(0).Value > 0, "{SP_DailySalarySummary.EmployId}=" & Me.cmbEmployee.Value, "Nothing")), Me.DateTimePicker1.Value.Date.ToString("dd/MMM/yyyy"), Me.DateTimePicker2.Value.Date.ToString("dd/MMM/yyyy"))
            ElseIf Me.ReportName = ReportList.SalesReturnSummary Then
                If cmbCustomer.Visible = True Then
                    If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                        ShowReport("SaleReturnSummary", "{SaleReturnSummary;1.SalesReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SaleReturnSummary;1.CustomerCode}=" & Me.cmbCustomer.Value & " " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SaleReturnSummary;1.CustomerTypeID}=" & Me.cmbCustomerType.SelectedValue & "", "") & "", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, Print)
                    Else
                        ShowReport("SaleReturnSummary", "{SaleReturnSummary;1.SalesReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")  " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SaleReturnSummary;1.CustomerTypeID}=" & Me.cmbCustomerType.SelectedValue & "", "") & "", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, Print)
                    End If
                Else
                    ShowReport("SaleReturnSummary", "{SaleReturnSummary;1.SalesReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")  " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SaleReturnSummary;1.CustomerTypeID}=" & Me.cmbCustomerType.SelectedValue & "", "") & "", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, Print)
                End If
            ElseIf Me.ReportName = ReportList.ProductionSummary Then
                GetCrystalReportRights()
                ''18-Mar-2014 Imran Ali TASK:2502 Problem in Production Detail Report 
                'ShowReport("rptProductionSummary", "{ProductionSummary;1.Production_Date} In DateTime (" & fromDate & ") To DateTime(" & ToDate & ") " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " And {ProductionSummary;1.Project}=" & Me.cmbCostCenter.SelectedValue & IIf(Me.cmbProducationLocation.SelectedIndex > 0, " And {ProductionSummary;1.Production_store}=" & Me.cmbProducationLocation.SelectedValue.ToString, ""), IIf(Me.cmbProducationLocation.SelectedIndex > 0, " And {ProductionSummary;1.Production_store}=" & Me.cmbProducationLocation.SelectedValue.ToString, "")) & "", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, Print)
                'ShowReport("rptProductionSummary", "{ProductionSummary;1.Production_date} In DateTime (" & fromDate & ") To DateTime(" & ToDate & ") " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " And {ProductionSummary;1.Project}=" & Me.cmbCostCenter.SelectedValue & IIf(Me.cmbProducationLocation.SelectedIndex > 0, " And {ProductionSummary;1.Location_Id}=" & Me.cmbProducationLocation.SelectedValue.ToString, ""), IIf(Me.cmbProducationLocation.SelectedIndex > 0, " And {ProductionSummary;1.Location_Id}=" & Me.cmbProducationLocation.SelectedValue.ToString, "")) & " " & IIf(Me.cmbEmployee.Value > 0, " AND {ProductionSummary;1.EmployeeID}=" & Me.cmbEmployee.Value & "", "") & "", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, Print)
                ShowReport("rptProductionSummary", "{ProductionSummary;1.Production_date} In DateTime (" & fromDate & ") To DateTime(" & ToDate & ") " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " And {ProductionSummary;1.Project}=" & Me.cmbCostCenter.SelectedValue, "") & "" & IIf(Me.cmbProducationLocation.SelectedIndex > 0, " And {ProductionSummary;1.Location_Id}=" & Me.cmbProducationLocation.SelectedValue.ToString, "") & " " & IIf(Me.cmbEmployee.Value > 0, " AND {ProductionSummary;1.EmployeeID}=" & Me.cmbEmployee.Value & "", "") & "", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, Print)
                'End Task:2502
            ElseIf ReportName = ReportList.DailyEmployeeAttendance Then
                GetDailyAttendanceData()
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
                'ShowReport("rptEmployeeAttendanceNew", "{SP_Employee_Attendance;1.AttendanceDate} in DateTime(" & fromDate & ") to DateTime (" & ToDate & ")" & IIf(Me.cmbEmployee.SelectedIndex > 0, " and {SP_Employee_Attendance;1.Employee_ID}=" & Me.cmbEmployee.SelectedValue & "", ""))
                ShowReport("rptEmployeeAttendanceNew", "{SP_Employee_Attendance;1.Employee_Id} <> 0 " & IIf(Me.cmbEmployee.ActiveRow.Cells(0).Value > 0, " and {SP_Employee_Attendance;1.Employee_Id}=" & Me.cmbEmployee.Value & "", "") & " " & IIf(Me.cmbCostCenter.SelectedValue > 0, " and {SP_Employee_Attendance;1.CostCentre}=" & Me.cmbCostCenter.SelectedValue & "", ""))
                'Task:2418 Added Report Withholding tax certificate
            ElseIf ReportName = ReportList.EmployeeAttendanceDetail Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
                'ShowReport("rptEmployeeAttendanceNew", "{SP_Employee_Attendance;1.AttendanceDate} in DateTime(" & fromDate & ") to DateTime (" & ToDate & ")" & IIf(Me.cmbEmployee.SelectedIndex > 0, " and {SP_Employee_Attendance;1.Employee_ID}=" & Me.cmbEmployee.SelectedValue & "", ""))
                ShowReport("rptEmployeeAttendanceDetail", "{SP_EmpAttendanceDetailByIn;1.Employee_Id} <> 0 " & IIf(Me.cmbEmployee.ActiveRow.Cells(0).Value > 0, " and {SP_EmpAttendanceDetailByIn;1.Employee_ID}=" & Me.cmbEmployee.Value & "", "") & " " & IIf(Me.cmbCostCenter.SelectedValue > 0, " and {SP_EmpAttendanceDetailByIn;1.CostCentre}=" & Me.cmbCostCenter.SelectedValue & "", ""))
                'Task:2418 Added Report Withholding tax certificate
                'Task:2418 Added Report Withholding tax certificate
            ElseIf ReportName = ReportList.DispatchStatus Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59"))
                ShowReport("rptDispatchStatus")
            ElseIf ReportName = ReportList.WHTaxCertificate Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
                AddRptParam("@AccountId", Me.cmbVendor.Value) 'Task:2456 Added Parameter 
                ShowReport("rpttaxcertificate", "Nothing", "Nothing", "Nothing")
                'End Task:2418
                'Task:2506 Set Store Issaunce Detail Batch Wise Report
            ElseIf ReportName = ReportList.StoreIssuanceDetailBatchWise Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
                ShowReport("rptStoreIssuanceDetailBatchWise")
                'End Task:2506
                'Task:2522 Added Report Sales Certificate
            ElseIf ReportName = ReportList.SalesCertificateIssued Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
                AddRptParam("@CustomerCode", Me.cmbCustomer.Value)
                ShowReport("rptIssuedSalesCertificate")
                'End Task:2522
                'Task:2810 Setting Report Show PLSubsubAccountSummary And PLDetailAccountSummary
            ElseIf ReportName = ReportList.PLSubsubAccountSummary Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59"))
                ShowReport("rptPLNoteSubSubAccountSummary", IIf(Me.cmbCostCenter.SelectedIndex > 0, "{SP_PLSubSubAccountSummary;1.CostCenterId}=" & Me.cmbCostCenter.SelectedValue & "", ""))
            ElseIf ReportName = ReportList.PLDetailAccountSummary Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
                ShowReport("rptPLNoteDetailAccountSummary", IIf(Me.cmbCostCenter.SelectedIndex > 0, "{SP_PLDetailAccountSummary;1.CostCenterId}=" & Me.cmbCostCenter.SelectedValue & "", ""))
                'End Task:2810
                'Task:2850 Purchase Item Summary Report Show Setting 
            ElseIf Me.ReportName = ReportList.PurchaseItemSummary Then
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
                ShowReport("rptPurchaseItemSummary")
                'End Task:2850
                'Task#18082015 Add Reports rptProduction and rptWIP by Ahmad Sharif
            ElseIf Me.ReportName = ReportList.Production Then
                GetCrystalReportRights()
                AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
                ShowReport("RptProduction", "" & IIf(Me.cmbEmployee.Value > 0, " {ProductionSummary;1.EmployeeID} = " & Me.cmbEmployee.Value & "", "") & "")
            ElseIf Me.ReportName = ReportList.WIP Then
                AddRptParam("@FromDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 00:00:00"))
                AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
                ShowReport("RptWIP")
                'End Task#18082015 
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Function GetMonthlyPurchaseSummary() As DataTable
        Try

            Dim strSQL As String = String.Empty
            strSQL = ""

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CallShowReport(True)
    End Sub
    Public Function FunAddReportCriteria() As String
        Dim strSql As String
        Dim strCondAccount As String = String.Empty
        Dim strYearCriteria As String
        Dim strLocationCriteria As String

        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con


        strYearCriteria = " ( dbo.tblVoucher.Voucher_no <> '000000' ) AND  "
        strLocationCriteria = "  "

        Dim strPostCriteria As String = String.Empty
        Dim strOther_Voucher_Criteria As String = String.Empty
        Dim intlocation_id As Integer

        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check
        'If chkunposted.Value = vbUnchecked Then

        '    strPostCriteria = "  (tblVoucher.post = 1) AND "
        'Else

        '    strPostCriteria = ""
        'End If

        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check


        strOther_Voucher_Criteria = ""

        '   get the location id

        intlocation_id = 0

        Dim ReceiptType As String
        Dim PaymentType As String
        Dim AccType As String



        ReceiptType = "'BR', 'CR'"
        PaymentType = "'BP', 'CP'"
        AccType = "'Cash','Bank'"

        strSql = "SELECT SUM(credit_amount)-SUM(debit_amount) from ("
        strSql = strSql & "SELECT     dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
                         "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
                         "                      dbo.tblVoucherDetail.credit_amount, dbo.tblVoucher.post " & _
                         "FROM         dbo.tblVoucher INNER JOIN " & _
                         "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(Me.DateTimePicker1.MinDate, "yyyy-M-d 00:00:00") & "', 102)  AND Convert(DateTime, '" & Format(Me.DateTimePicker1.Value.Date.AddDays(-1), "yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & " AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ")  AND (dbo.tblVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblDefVoucherType " & _
                         "                            WHERE      gl_type IN (" & PaymentType & "))) AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0) > 0)  " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "))      " & _
                         "Union " & _
                         "SELECT     dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
                         "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount , " & _
                         "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
                         "FROM         dbo.tblVoucher INNER JOIN " & _
                         "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(DateTimePicker1.MinDate, "yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Format(DateTimePicker1.Value.AddDays(-1), "yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & " AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") AND (dbo.tblVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblDefVoucherType " & _
                         "                            WHERE      gl_type IN (" & ReceiptType & "))) AND (ISNULL(dbo.tblVoucherDetail.credit_amount, 0) > 0)   " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "))   "

        strSql = strSql & ")tblOpeningBalance"


        'dblCashBankOpening = Val(UtilityDAL.ReturnDataRow(strSql).Item(0).ToString)


        strSql = "Alter View vwCashFlowPeriodRPT As "

        'strSql = strSql & "SELECT  0 AS Tr_Type ,   dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
        '                 "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
        '                 "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
        '                 "FROM         dbo.tblVoucher INNER JOIN " & _
        '                 "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
        '                 "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
        '                 "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
        '                 "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(DateTimePicker1.Value, "yyyy/MM/dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy/MM/dd") & "') AND (dbo.tblVoucher.voucher_type_id IN " & _
        '                 "                          (SELECT     voucher_Type_id " & _
        '                 "                            From tblDefVoucherType " & _
        '                 "                            WHERE      gl_type IN (" & PaymentType & "))) AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0) > 0) " & _
        '                 "Union " & _
        '                 "SELECT    1 AS Tr_Type , dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
        '                 "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
        '                 "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
        '                 "FROM         dbo.tblVoucher INNER JOIN " & _
        '                 "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
        '                 "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
        '                 "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
        '                 "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(DateTimePicker1.Value, "yyyy/MM/dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy/MM/dd") & "') AND (dbo.tblVoucher.voucher_type_id IN " & _
        '                 "                          (SELECT     voucher_Type_id " & _
        '                 "                            From tblDefVoucherType " & _
        '                 "                            WHERE      gl_type IN (" & ReceiptType & "))) AND (ISNULL(dbo.tblVoucherDetail.credit_amount, 0) > 0) "

        strSql = strSql & "SELECT     CASE WHEN (dbo.tblVoucherDetail.credit_amount > 0) THEN 1 ELSE 0 END AS tr_type, dbo.tblVoucherDetail.coa_detail_id, " _
                        & " dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, dbo.tblVoucherDetail.comments, " _
                        & " dbo.vwCOADetail.detail_title, dbo.tblVoucherDetail.debit_amount, dbo.tblVoucherDetail.credit_amount, dbo.tblVoucher.post " _
                        & " FROM         dbo.tblVoucherDetail INNER JOIN " _
                        & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
                        & " dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
                        & " dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id " _
                        & " WHERE     (dbo.tblVoucher.voucher_id IN " _
                        & " (SELECT DISTINCT tblVoucher_1.voucher_id " _
                        & " FROM          dbo.tblVoucherDetail AS tblVoucherDetail_1 INNER JOIN" _
                        & " dbo.tblVoucher AS tblVoucher_1 ON tblVoucherDetail_1.voucher_id = tblVoucher_1.voucher_id INNER JOIN " _
                        & " dbo.tblDefVoucherType AS tblDefVoucherType_1 ON  " _
                        & " tblVoucher_1.voucher_type_id = tblDefVoucherType_1.voucher_type_id INNER JOIN " _
                        & " dbo.vwCOADetail AS vwCOADetail_1 ON tblVoucherDetail_1.coa_detail_id = vwCOADetail_1.coa_detail_id " _
                        & " WHERE      (vwCOADetail_1.account_type in( " & AccType & ")) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail_1.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ")) AND (dbo.vwCOADetail.account_type NOT IN ( " & AccType & "))" _
                        & " and " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & " " _
                        & " (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(Me.DateTimePicker1.Value.Date, "yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Format(DateTimePicker2.Value, "yyyy-M-d 23:59:59") & "',102)) AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0)-ISNULL(dbo.tblVoucherDetail.credit_amount, 0) <> 0) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & " "
        cm.CommandText = strSql
        cm.ExecuteNonQuery()

        'Dim ObjDAL As New DAL.RptCashFlowDal

        'If ObjDAL.InsertDataForReport("Stander") Then

        strSql = " truncate table TblrptCashFlowStander "
        cm.CommandText = strSql
        cm.ExecuteNonQuery()

        strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post ) " & _
                      "Select Tr_Type ,Tr_Type + 1, coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post  from vwCashFlowPeriodRPT WHERE (IsNull(debit_amount,0) <> 0 Or IsNull(credit_amount,0) <> 0)"

        cm.CommandText = strSql
        cm.ExecuteNonQuery()

        If 1 = 1 Then
            'strSql = "SELECT     tblCOAMainSubSubDetail.coa_detail_id, tblCOAMainSubSubDetail.detail_title, dbo.tblCOAMainSubSub.account_type " & _
            '                     "FROM         dbo.tblCOAMainSubSubDetail AS tblCOAMainSubSubDetail INNER JOIN " & _
            '                     "dbo.tblCOAMainSubSub ON tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " & _
            '                     "WHERE     (dbo.tblCOAMainSubSub.account_type IN ( " & AccType & "))"

            'Dim dt As DataTable
            'dt = UtilityDAL.GetDataTable(strSql).Copy

            Dim ilocation_id As Integer

            ' Get Location ID .. 
            ilocation_id = 0



            '//Preparing Query string to insert opening balance
            strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
                        " SELECT     1 AS tr_Type, 0 AS Sort, coa_detail_id, '" & Me.DateTimePicker1.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Opening Balance' AS Comments, detail_title, 0 AS Credit, OpeningBalance, " & _
                        " 1 AS Post " & _
                        " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
                        " AS OpeningBalance " & _
                        " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount)-SUM(dbo.tblVoucherDetail.credit_amount), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (Convert(Varchar, dbo.tblVoucher.voucher_date,102) < Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "  GROUP BY dbo.tblVoucherDetail.coa_detail_id Having  ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) <> 0)as tmpTblAccountsOpening  INNER JOIN " & _
                        " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                        " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
                        " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            'values(" & _
            '                             "1 ,0, " & dtRow.Item(0).ToString & " ,'" & Format(Me.DateTimePicker1.Value.Date.AddDays(-1), "yyyy/MM/dd") & "' ,'' ,'Opening Balance' ,'" & dtRow.Item(1).ToString & "' ," & dblCashBankOpening & " ,0 ,'True')"
            '//Inserting Opening Balance
            cm.CommandText = strSql
            cm.ExecuteNonQuery()


            ''//Preparing Query string to insert opening balance

            '//Preparing Query string to insert Closing balance
            strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
                        " SELECT     0 AS tr_Type, 3 AS Sort, coa_detail_id, '" & Me.DateTimePicker2.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Closing Balance' AS Comments, detail_title, OpeningBalance,  0 AS Credit," & _
                        " 1 AS Post " & _
                        " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
                        " AS OpeningBalance " & _
                        " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) <= Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 00:00:00") & "', 102))  AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "     GROUP BY dbo.tblVoucherDetail.coa_detail_id Having ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) <> 0)as tmpTblAccountsOpening  INNER JOIN " & _
                        " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                        " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
                        " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            'values(" & _
            '                             "1 ,0, " & dtRow.Item(0).ToString & " ,'" & Format(Me.DateTimePicker1.Value.Date.AddDays(-1), "yyyy/MM/dd") & "' ,'' ,'Opening Balance' ,'" & dtRow.Item(1).ToString & "' ," & dblCashBankOpening & " ,0 ,'True')"
            '//Inserting Closing Balance

            cm.CommandText = strSql
            cm.ExecuteNonQuery()

        Else
        End If

        Return ""

    End Function
    Public Function FunAddReportCriteriaExcludeTax() As String
        Dim strSql As String
        Dim strCondAccount As String = String.Empty
        Dim strYearCriteria As String
        Dim strLocationCriteria As String

        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con


        strYearCriteria = " ( dbo.tblVoucher.Voucher_no <> '000000' ) AND  "
        strLocationCriteria = "  "

        Dim strPostCriteria As String = String.Empty
        Dim strOther_Voucher_Criteria As String = String.Empty
        Dim intlocation_id As Integer

        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check
        'If chkunposted.Value = vbUnchecked Then

        '    strPostCriteria = "  (tblVoucher.post = 1) AND "
        'Else

        '    strPostCriteria = ""
        'End If

        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check


        strOther_Voucher_Criteria = ""

        '   get the location id

        intlocation_id = 0

        Dim ReceiptType As String
        Dim PaymentType As String
        Dim AccType As String



        ReceiptType = "'BR', 'CR'"
        PaymentType = "'BP', 'CP'"
        AccType = "'Cash','Bank'"

        strSql = "SELECT SUM(credit_amount)-SUM(debit_amount) from ("
        strSql = strSql & "SELECT     dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
                         "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, (IsNull(dbo.tblVoucherDetail.debit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) As debit_amount, " & _
                         "                      (IsNull(dbo.tblVoucherDetail.credit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) AS credit_amount  , dbo.tblVoucher.post " & _
                         "FROM         dbo.tblVoucher INNER JOIN " & _
                         "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(Me.DateTimePicker1.MinDate, "yyyy-M-d 00:00:00") & "', 102)  AND Convert(DateTime, '" & Format(Me.DateTimePicker1.Value.Date.AddDays(-1), "yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & " AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ")  AND (dbo.tblVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblDefVoucherType " & _
                         "                            WHERE      gl_type IN (" & PaymentType & "))) AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0) > 0)  " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "))      " & _
                         "Union " & _
                         "SELECT     dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
                         "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, (IsNull(dbo.tblVoucherDetail.debit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) As debit_amount , " & _
                         "                      (IsNull(dbo.tblVoucherDetail.credit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) As credit_amount , dbo.tblVoucher.post " & _
                         "FROM         dbo.tblVoucher INNER JOIN " & _
                         "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(DateTimePicker1.MinDate, "yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Format(DateTimePicker1.Value.AddDays(-1), "yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & " AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") AND (dbo.tblVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblDefVoucherType " & _
                         "                            WHERE      gl_type IN (" & ReceiptType & "))) AND (ISNULL(dbo.tblVoucherDetail.credit_amount, 0) > 0)   " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "))   "

        strSql = strSql & ")tblOpeningBalance"


        'dblCashBankOpening = Val(UtilityDAL.ReturnDataRow(strSql).Item(0).ToString)


        strSql = "Alter View vwCashFlowPeriodRPT As "

        'strSql = strSql & "SELECT  0 AS Tr_Type ,   dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
        '                 "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
        '                 "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
        '                 "FROM         dbo.tblVoucher INNER JOIN " & _
        '                 "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
        '                 "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
        '                 "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
        '                 "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(DateTimePicker1.Value, "yyyy/MM/dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy/MM/dd") & "') AND (dbo.tblVoucher.voucher_type_id IN " & _
        '                 "                          (SELECT     voucher_Type_id " & _
        '                 "                            From tblDefVoucherType " & _
        '                 "                            WHERE      gl_type IN (" & PaymentType & "))) AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0) > 0) " & _
        '                 "Union " & _
        '                 "SELECT    1 AS Tr_Type , dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
        '                 "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
        '                 "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
        '                 "FROM         dbo.tblVoucher INNER JOIN " & _
        '                 "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
        '                 "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
        '                 "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
        '                 "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(DateTimePicker1.Value, "yyyy/MM/dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy/MM/dd") & "') AND (dbo.tblVoucher.voucher_type_id IN " & _
        '                 "                          (SELECT     voucher_Type_id " & _
        '                 "                            From tblDefVoucherType " & _
        '                 "                            WHERE      gl_type IN (" & ReceiptType & "))) AND (ISNULL(dbo.tblVoucherDetail.credit_amount, 0) > 0) "

        strSql = strSql & "SELECT     CASE WHEN (dbo.tblVoucherDetail.credit_amount > 0) THEN 1 ELSE 0 END AS tr_type, dbo.tblVoucherDetail.coa_detail_id, " _
                        & " dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, dbo.tblVoucherDetail.comments, " _
                        & " dbo.vwCOADetail.detail_title, (IsNull(dbo.tblVoucherDetail.debit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) As debit_amount, (IsNull(dbo.tblVoucherDetail.credit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) As credit_amount, dbo.tblVoucher.post " _
                        & " FROM         dbo.tblVoucherDetail INNER JOIN " _
                        & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
                        & " dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
                        & " dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id " _
                        & " WHERE     (dbo.tblVoucher.voucher_id IN " _
                        & " (SELECT DISTINCT tblVoucher_1.voucher_id " _
                        & " FROM          dbo.tblVoucherDetail AS tblVoucherDetail_1 INNER JOIN" _
                        & " dbo.tblVoucher AS tblVoucher_1 ON tblVoucherDetail_1.voucher_id = tblVoucher_1.voucher_id INNER JOIN " _
                        & " dbo.tblDefVoucherType AS tblDefVoucherType_1 ON  " _
                        & " tblVoucher_1.voucher_type_id = tblDefVoucherType_1.voucher_type_id INNER JOIN " _
                        & " dbo.vwCOADetail AS vwCOADetail_1 ON tblVoucherDetail_1.coa_detail_id = vwCOADetail_1.coa_detail_id " _
                        & " WHERE      (vwCOADetail_1.account_type in( " & AccType & ")) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail_1.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ")) AND (dbo.vwCOADetail.account_type NOT IN ( " & AccType & "))" _
                        & " and " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & " " _
                        & " (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(Me.DateTimePicker1.Value.Date, "yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Format(DateTimePicker2.Value, "yyyy-M-d 23:59:59") & "',102)) AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0)-ISNULL(dbo.tblVoucherDetail.credit_amount, 0) <> 0) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & " "
        cm.CommandText = strSql
        cm.ExecuteNonQuery()

        'Dim ObjDAL As New DAL.RptCashFlowDal

        'If ObjDAL.InsertDataForReport("Stander") Then

        strSql = " truncate table TblrptCashFlowStander "
        cm.CommandText = strSql
        cm.ExecuteNonQuery()

        strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post ) " & _
                      "Select Tr_Type ,Tr_Type + 1, coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post  from vwCashFlowPeriodRPT WHERE (IsNull(debit_amount,0) <> 0 Or IsNull(credit_amount,0) <> 0)"

        cm.CommandText = strSql
        cm.ExecuteNonQuery()

        If 1 = 1 Then
            'strSql = "SELECT     tblCOAMainSubSubDetail.coa_detail_id, tblCOAMainSubSubDetail.detail_title, dbo.tblCOAMainSubSub.account_type " & _
            '                     "FROM         dbo.tblCOAMainSubSubDetail AS tblCOAMainSubSubDetail INNER JOIN " & _
            '                     "dbo.tblCOAMainSubSub ON tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " & _
            '                     "WHERE     (dbo.tblCOAMainSubSub.account_type IN ( " & AccType & "))"

            'Dim dt As DataTable
            'dt = UtilityDAL.GetDataTable(strSql).Copy

            Dim ilocation_id As Integer

            ' Get Location ID .. 
            ilocation_id = 0



            '//Preparing Query string to insert opening balance
            strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
                        " SELECT     1 AS tr_Type, 0 AS Sort, coa_detail_id, '" & Me.DateTimePicker1.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Opening Balance' AS Comments, detail_title, 0 AS Credit, OpeningBalance, " & _
                        " 1 AS Post " & _
                        " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
                        " AS OpeningBalance " & _
                        " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL((SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount))-Sum(IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (Convert(Varchar, dbo.tblVoucher.voucher_date,102) < Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "  GROUP BY dbo.tblVoucherDetail.coa_detail_id Having  ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) <> 0)as tmpTblAccountsOpening  INNER JOIN " & _
                        " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                        " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
                        " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            'values(" & _
            '                             "1 ,0, " & dtRow.Item(0).ToString & " ,'" & Format(Me.DateTimePicker1.Value.Date.AddDays(-1), "yyyy/MM/dd") & "' ,'' ,'Opening Balance' ,'" & dtRow.Item(1).ToString & "' ," & dblCashBankOpening & " ,0 ,'True')"
            '//Inserting Opening Balance
            cm.CommandText = strSql
            cm.ExecuteNonQuery()


            ''//Preparing Query string to insert opening balance

            '//Preparing Query string to insert Closing balance
            strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
                        " SELECT     0 AS tr_Type, 3 AS Sort, coa_detail_id, '" & Me.DateTimePicker2.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Closing Balance' AS Comments, detail_title, OpeningBalance,  0 AS Credit," & _
                        " 1 AS Post " & _
                        " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
                        " AS OpeningBalance " & _
                        " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL((SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount))-Sum(IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) <= Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 00:00:00") & "', 102))  AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "     GROUP BY dbo.tblVoucherDetail.coa_detail_id Having ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) <> 0)as tmpTblAccountsOpening  INNER JOIN " & _
                        " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                        " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
                        " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            'values(" & _
            '                             "1 ,0, " & dtRow.Item(0).ToString & " ,'" & Format(Me.DateTimePicker1.Value.Date.AddDays(-1), "yyyy/MM/dd") & "' ,'' ,'Opening Balance' ,'" & dtRow.Item(1).ToString & "' ," & dblCashBankOpening & " ,0 ,'True')"
            '//Inserting Closing Balance

            cm.CommandText = strSql
            cm.ExecuteNonQuery()

        Else
        End If

        Return ""

    End Function
    Private Sub lnkRefresh_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
            Dim id As Integer = 0
            id = Me.cmbCustomer.Value
            FillUltraDropDown(cmbCustomer, "Select coa_detail_id, detail_title from vwCOADetail WHERE Account_Type='Customer'")
            Me.cmbCustomer.Value = id

            id = Me.cmbVendor.Value
            FillUltraDropDown(cmbCustomer, "Select coa_detail_id, detail_title from vwCOADetail WHERE Account_Type='Vendor'")
            Me.cmbVendor.Value = id

            id = Me.cmbCostCenter.SelectedValue
            FillDropDown(Me.cmbCostCenter, "Select CostCenterId, Name as CostCenter from tblDefCostCenter")
            Me.cmbCostCenter.SelectedValue = id

            id = Me.cmbEmployee.ActiveRow.Cells(0).Value
            FillUltraDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name,Father_Name,Employee_Code From EmployeesView")
            Me.cmbEmployee.Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetCashAndBankData() As DataTable
        Try

            Dim dt As New DataTable
            Dim str As String = String.Empty
            str = "SELECT dbo.tblDefVoucherType.voucher_type, dbo.tblVoucher.voucher_no, dbo.tblVoucher.voucher_date, dbo.tblVoucherDetail.coa_detail_id, " _
           & "           dbo.tblVoucherDetail.debit_amount, dbo.tblVoucherDetail.credit_amount, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title,  " _
           & "           ISNULL(dbo.tblVoucherDetail.CostCenterID, 0) AS CostCenterID, dbo.tblDefCostCenter.Name AS CostCenter, dbo.tblVoucher.post,  " _
            & "           dbo.tblVoucherDetail.cheque_no, dbo.tblVoucherDetail.cheque_date, tblVoucherDetail.Comments as Description,dbo.tblDefVoucherType.sort_order, vwCOADetail.Sub_Sub_Code, vwCOADetail.Sub_Sub_Title, vwCOADetail.Account_Type " _
           & "           FROM dbo.tblVoucherDetail INNER JOIN " _
           & "           dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
           & "           dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
           & "           dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
           & "           dbo.tblDefCostCenter ON dbo.tblVoucherDetail.CostCenterID = dbo.tblDefCostCenter.CostCenterID " _
           & "           WHERE (dbo.tblVoucher.voucher_id IN " _
           & "           (SELECT DISTINCT tblvoucher.voucher_id " _
           & "           FROM dbo.tblVoucherDetail INNER JOIN " _
           & "           dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
           & "           dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
           & "           dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id "
            str += " WHERE (Convert(varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(Datetime, '" & Me.DateTimePicker1.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.DateTimePicker2.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) AND Isnull(tblVoucher.Post,0) In " & IIf(Me.chkUnposted.Checked = True, "(1,0,NULL)", "(1)") & " " & IIf(MyCompanyId > 0, " AND vwCoaDetail.CompanyId=" & MyCompanyId & "", "") & " " & IIf(Me.cmbCashAccount.SelectedIndex > 0, " AND tblVoucherDetail.coa_detail_Id=" & Me.cmbCashAccount.SelectedValue & "", "") & ""
            str += " " & IIf(Me.rdoCash.Checked = True, " AND vwcoadetail.account_type = 'Cash')) AND (dbo.vwCOADetail.account_type NOT IN ('Cash')  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ") ", "") & " "
            str += " " & IIf(Me.rdoBank.Checked = True, " AND vwcoadetail.account_type = 'Bank')) AND (dbo.vwCOADetail.account_type NOT IN ('Bank')  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ") ", "") & " "
            str += " " & IIf(Me.rdoBoth.Checked = True, " AND(vwcoadetail.account_type In('Cash','Bank'))) AND (dbo.vwCOADetail.account_type NOT IN ('Cash','Bank'))   " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ") ", "") & ""
            'str += " " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & ""
            ''TFS TASK: TFS1212 
            str += " " & CostCenterCriterial & ""
            ''END TFS1212
            If MyCompanyId > 0 Then
                str += " AND vwCOADetail.CompanyId = " & MyCompanyId & ""
            End If
            ''07-Mar-2014 TASK:2468  Imran Ali  Date sort order in cash flow statement
            str += " ORDER BY dbo.tblVoucher.voucher_date, dbo.tblDefVoucherType.sort_order ASC "
            'End Task:2468
            dt = GetDataTable(str)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetNetSalesReportData() As DataTable
        Try
            Dim strQuery As String = String.Empty

            strQuery = "SELECT detail_code, detail_title, ArticleCode, ArticleDescription, Color, Size, ISNULL(Price, 0) AS Price, SUM(ISNULL(SalesQty, 0)) " _
                  & "  AS SalesQty, SUM(ISNULL(ReturnQty, 0)) AS ReturnQty " _
                  & "  FROM (SELECT dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription,  " _
                  & "  dbo.ArticleDefView.ArticleColorName AS Color, dbo.ArticleDefView.ArticleSizeName AS Size, dbo.SalesDetailTable.Price,  " _
                  & "  SUM(dbo.SalesDetailTable.Qty) AS SalesQty, 0 AS ReturnQty " _
                  & "  FROM dbo.SalesDetailTable INNER JOIN " _
                  & "  dbo.ArticleDefView ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId INNER JOIN " _
                  & "  dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId INNER JOIN " _
                  & "  dbo.vwCOADetail ON dbo.SalesMasterTable.CustomerCode = dbo.vwCOADetail.coa_detail_id "
            strQuery += " WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)) "
            If Me.cmbCompany.SelectedIndex > 0 Then
                strQuery += " AND SalesMasterTable.LocationId=" & Me.cmbCompany.SelectedValue & ""
            End If
            If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                strQuery += " AND SalesMasterTable.CustomerCode=" & Me.cmbCustomer.Value & ""
            End If
            strQuery += "  GROUP BY dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription,  " _
                  & "  dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName, dbo.SalesDetailTable.Price " _
                  & "  UNION ALL " _
                  & "  SELECT vwCOADetail_1.detail_code, vwCOADetail_1.detail_title, ArticleDefView_1.ArticleCode, ArticleDefView_1.ArticleDescription,  " _
                  & "  ArticleDefView_1.ArticleColorName AS Color, ArticleDefView_1.ArticleSizeName AS Size, dbo.SalesReturnDetailTable.Price,  " _
                  & "  0 AS SalesQty, SUM(dbo.SalesReturnDetailTable.Qty) AS ReturnQty " _
                  & "  FROM dbo.SalesReturnDetailTable INNER JOIN " _
                  & "  dbo.ArticleDefView AS ArticleDefView_1 ON dbo.SalesReturnDetailTable.ArticleDefId = ArticleDefView_1.ArticleId INNER JOIN " _
                  & "  dbo.SalesReturnMasterTable ON dbo.SalesReturnDetailTable.SalesReturnId = dbo.SalesReturnMasterTable.SalesReturnId INNER JOIN  " _
                  & "  dbo.vwCOADetail AS vwCOADetail_1 ON dbo.SalesReturnMasterTable.CustomerCode = vwCOADetail_1.coa_detail_id "
            strQuery += " WHERE (Convert(Varchar, SalesReturnMasterTable.SalesReturnDate, 102) BETWEEN Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)) "
            If Me.cmbCompany.SelectedIndex > 0 Then
                strQuery += " AND SalesReturnMasterTable.LocationId=" & Me.cmbCompany.SelectedValue & ""
            End If
            If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                strQuery += " AND SalesReturnMasterTable.CustomerCode=" & Me.cmbCustomer.Value & ""
            End If
            strQuery += "  GROUP BY vwCOADetail_1.detail_code, vwCOADetail_1.detail_title, ArticleDefView_1.ArticleCode, ArticleDefView_1.ArticleDescription,  " _
            & "  ArticleDefView_1.ArticleColorName, ArticleDefView_1.ArticleSizeName, dbo.SalesReturnDetailTable.Price)  " _
            & "  AS derivedtbl_1 " _
            & "  GROUP BY detail_code, detail_title, ArticleCode, ArticleDescription, Color, Size, ISNULL(Price, 0) "
            Dim dtSale As New DataTable
            dtSale = GetDataTable(strQuery)

            Return dtSale

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.DateTimePicker1.Value = Date.Today
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-1)
            Me.DateTimePicker2.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, 1, 1)
            Me.DateTimePicker2.Value = Date.Today
        End If
    End Sub

    Private Sub rdoCash_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCash.CheckedChanged
        Try
            Dim strAccounType As String = String.Empty
            If Me.rdoCash.Checked = True Then
                strAccounType = "Cash"
            ElseIf rdoBank.Checked = True Then
                strAccounType = "Bank"
            ElseIf Me.rdoBoth.Checked = True Then
                strAccounType = ""
            End If
            FillDropDown(Me.cmbCashAccount, "Select coa_detail_id, detail_title From vwCOADetail WHERE Active=1 " & IIf(strAccounType.Length > 0, " AND Account_Type='" & strAccounType.ToString & "'", "") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoBank_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoBank.CheckedChanged
        Try
            Dim strAccounType As String = String.Empty
            If Me.rdoCash.Checked = True Then
                strAccounType = "Cash"
            ElseIf rdoBank.Checked = True Then
                strAccounType = "Bank"
            ElseIf Me.rdoBoth.Checked = True Then
                strAccounType = ""
            End If
            FillDropDown(Me.cmbCashAccount, "Select coa_detail_id, detail_title From vwCOADetail WHERE Active=1 " & IIf(strAccounType.Length > 0, " AND Account_Type='" & strAccounType.ToString & "'", "") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoBoth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoBoth.CheckedChanged
        Try
            Dim strAccounType As String = String.Empty
            If Me.rdoCash.Checked = True Then
                strAccounType = "Cash"
            ElseIf rdoBank.Checked = True Then
                strAccounType = "Bank"
            ElseIf Me.rdoBoth.Checked = True Then
                strAccounType = ""
            End If
            FillDropDown(Me.cmbCashAccount, "Select coa_detail_id, detail_title From vwCOADetail WHERE Active=1 " & IIf(strAccounType.Length > 0, " AND Account_Type='" & strAccounType.ToString & "'", " AND Account_Type IN('Cash','Bank')") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetDailyAttendanceData()
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDbCommand
        Dim strQuery As String = String.Empty
        cmd.Connection = Con
        cmd.Transaction = trans

        Try





            strQuery = "SELECT EmpId, AttendanceDate, AttendanceType, AttendanceTime, AttendanceStatus, Flexibility_In_Time, Flexibility_Out_Time, " _
            & " Sch_In_Time, Sch_Out_Time FROM dbo.tblAttendanceDetail AS Att_d WHERE (Convert(Varchar,AttendanceDate,102) BETWEEN Convert(DateTime,'" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)) ORDER BY EmpId, AttendanceDate, AttendanceTime "

            Dim dt As New DataTable
            dt = GetDataTable(strQuery)
            dt.AcceptChanges()

            Dim strAttendanceType As String = String.Empty
            Dim intEmpId As Integer = 0I
            Dim strType As String = String.Empty

            cmd.CommandType = CommandType.Text
            cmd.CommandText = "Truncate Table tblTempAttendanceDetail"
            cmd.ExecuteNonQuery()


            For Each r As DataRow In dt.Rows


                If intEmpId <> Val(r.Item("EmpId").ToString) Then
                    strType = String.Empty
                    strAttendanceType = String.Empty
                End If


                'If strAttendanceType = "" Then
                '    strType = "In"
                'Else
                '    strType = "Out"
                'End If
                Select Case strAttendanceType
                    Case "In"
                        strType = "Out"
                    Case "Out"
                        strType = "In"
                    Case ""
                        strType = "In"
                End Select

                If r.Item("AttendanceStatus").ToString = "Break" Then
                    strType = "Out"
                End If

                Dim strSQL As String = String.Empty
                strSQL = " INSERT INTO tblTempAttendanceDetail(EmpId,AttendanceDate,AttendanceType,AttendanceTime,AttendanceStatus,Flexibility_In_Time,Flexibility_Out_Time,Sch_In_Time,Sch_Out_Time) " _
                & " VALUES(" & Val(r.Item("EmpId").ToString) & ", Convert(DateTime,'" & r.Item("AttendanceDate").ToString & "',102), '" & strType.Replace("'", "''") & "',Convert(DateTime,'" & r.Item("AttendanceTime") & "',102), '" & r.Item("AttendanceStatus").ToString.Replace("'", "''") & "',Convert(DateTime,'" & r.Item("Flexibility_In_Time") & "',102),Convert(DateTime,'" & r.Item("Flexibility_Out_Time") & "',102),Convert(DateTime,'" & r.Item("Sch_In_Time") & "',102),Convert(DateTime,'" & r.Item("Sch_Out_Time") & "',102))"
                cmd.CommandType = CommandType.Text
                cmd.CommandText = strSQL


                cmd.ExecuteNonQuery()

                intEmpId = Val(r.Item("EmpId").ToString)

                Select Case strType
                    Case "In"
                        strAttendanceType = "In"
                    Case "Out"
                        strAttendanceType = "Out"
                    Case ""
                        strAttendanceType = "In"
                End Select


            Next


            trans.Commit()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''TASK: TFS1212 Applied Cost Center Group Wise filter to Bank & Cash Statement on 31-07-2017
    Private Sub cmbCCGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCCGroup.SelectedIndexChanged
        Try
            If Not cmbCCGroup.SelectedIndex = -1 AndAlso cmbCCGroup.SelectedIndex > 0 Then
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter Where CostCenterGroup= '" & Me.cmbCCGroup.Text & "' order by sortorder , name", True)
            Else
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''END TASK: TFS1212
End Class
