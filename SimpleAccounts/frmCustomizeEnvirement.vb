Imports System.Windows.Forms
Imports SimpleAccounts
Public Class frmCustomizeEnvirement



    Dim imageCount As Integer = 1
    Public ControlName As New Form
    Public LastControlName As New Form
    Public NextControlName As New Form
    'Indicates if we are changing the selected node of the treeview programmatically
    Private ChangingSelectedNode As Boolean
    Dim enm As EnumForms = EnumForms.Non
    Dim Lastenum As EnumForms = EnumForms.Non
    Dim Nextenm As EnumForms = EnumForms.Non
    Dim strControlName As String
    Public dbVersion As String = String.Empty
    Public IsOpenMainForm As Boolean = False
    Public Tags As String = String.Empty
    Public Shared strStartUpPath As String = String.Empty
    Public Shared strDownloadReleasePath As String = String.Empty
    Dim IsBackgroundChanged As Boolean = False
    Dim ShowStartupTip As Boolean = False
    Dim ReminderFromDate As DateTime = Date.Now
    Dim ReminderTime As String = String.Empty
    Dim dtReminder As DataTable
    'Dim frmDefArt As frmDefArticle
    Public Shared flg As Boolean = False
    Dim flgReminder As Boolean = False
    Dim NewSecurityRights As Boolean = True
    Dim FormTag As String = String.Empty
    Dim flgCompanyRights As Boolean = False
    Dim RestrictForm As String = String.Empty
    Dim RestrictSheetAccess As String = String.Empty

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripMenuItem.Click, NewWindowToolStripMenuItem.Click
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CutToolStripMenuItem.Click
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PasteToolStripMenuItem.Click
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer

    Public Sub InitilizingCustomizeEnvirement()
        Try

            Dim strSQL As String = String.Empty

            'If blnSystemWiseMDI = True Then
            '    strSQL = "Select Count(*) as Cont from TerminalConfigurationMaster INNER JOIN TerminalConfigurationUsers on TerminalConfigurationUsers.TCMID = TerminalConfigurationMaster.TCMID WHERE TerminalConfigurationUsers.UserId='" & System.Environment.MachineName.ToString & "' AND Layout=N'Single Application'"
            'Else
            strSQL = "Select Count(*) as Cont from TerminalConfigurationMaster INNER JOIN TerminalConfigurationUsers on TerminalConfigurationUsers.TCMID = TerminalConfigurationMaster.TCMID WHERE TerminalConfigurationUsers.UserId=" & LoginUserId & " AND Layout=N'Single Application'"
            'End If

            Dim dtSingleApp As New DataTable
            dtSingleApp = GetDataTable(strSQL)
            dtSingleApp.AcceptChanges()
            Dim dtScreen As New DataTable

            If dtSingleApp.Rows.Count > 0 Then
                If Val(dtSingleApp.Rows(0).Item(0).ToString) > 0 Then
                    dtScreen = GetDataTable("SP_SingleModulesMDI " & LoginUserId & "")
                Else
                    dtScreen = GetDataTable("SP_MultiModulesMDI " & LoginUserId & "")
                End If
            End If

            dtScreen.TableName = "Default"
            dtScreen.AcceptChanges()
            Dim dv As New DataView
            dv.Table = dtScreen
            If blnSystemWiseMDI = True Then
                dv.RowFilter = " SystemName='" & System.Environment.MachineName.ToString & "'"
            End If
            Dim dt As New DataTable
            dt.TableName = "Default"
            dt = dv.ToTable

            Dim strMainMenu As String = String.Empty
            Dim strsubmenu As String = String.Empty
            Dim menuItem As New ToolStripMenuItem
            For Each oRow As DataRow In dt.Rows

                If strMainMenu.ToString.ToUpper <> oRow.Item("Title").ToString.ToUpper Then
                    menuItem = New ToolStripMenuItem
                    If oRow.Item("Title").ToString.Length > 0 Then
                        menuItem.Name = "mnu_" & oRow.Item("Title").ToString.Trim
                        menuItem.Text = oRow.Item("Title").ToString
                        menuItem.Tag = "mnu_" & oRow.Item("Title").ToString
                        Me.MenuStrip.Items.Add(menuItem)
                    End If
                End If
                Dim submenu As New ToolStripMenuItem
                If strsubmenu.ToString.ToUpper <> oRow.Item("FormName").ToString.Trim Then
                    submenu.Name = oRow.Item("FormName").ToString.Trim
                    submenu.Tag = oRow.Item("AccessKey").ToString
                    submenu.Text = oRow.Item("Menu").ToString
                    AddHandler submenu.Click, AddressOf OpenControl
                    menuItem.DropDownItems.Add(submenu)
                    strsubmenu = oRow.Item("FormName").ToString.Trim
                End If
                strMainMenu = oRow.Item("Title").ToString.ToUpper
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ShowForm(FormName As String)

        Dim ChildForm As New System.Windows.Forms.Form
        ChildForm.Name = FormName
        ApplyStyleSheet(ChildForm)
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me
        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber
        ChildForm.Show()
    End Sub
    Private Sub OpenControl(sender As Object, e As EventArgs)
        Try
            Dim menu As ToolStripItem = sender
            LoadControl(menu.Tag.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        InitilizingCustomizeEnvirement()
    End Sub

    Public Sub LoadControl(ByVal key As String)
        'If ControlName.Text.Length > 0 Then
        '    Me.LastControlName = ControlName
        '    Me.BackToolStripButton.Enabled = True
        '    Me.Lastenum = Me.enm
        'Else
        '    Me.BackToolStripButton.Enabled = False
        'End If
        'enm = EnumForms.Non
        ' With UltraExplorerBar1.ActiveItem
        'Forms


        If key = "Users" Then
            ControlName = frmDefSecurityUser
            enm = EnumForms.Non
        ElseIf key = "GroupRights" Then
            ControlName = FrmGroupRights
            enm = EnumForms.Non
        End If

        If key = "Exit" Then
            Me.Close()
        End If

        If key = "frmMainAccount" Then
            ControlName = DefMainAcc
            enm = EnumForms.DefMainAcc
            If Tags.Length > 0 Then
                DefMainAcc.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
            ''22-Sep-2014 TAKS:2850 Imran Ali Department And Category Wise Purchase Report
            'Altered By Ali Ansari against Task#20150510 
            ''add missing documents form 
            'VoucherPost 
            'ProductionProcessing
            'ElseIf key = "frmGrdRptCustomerCashRecovery" Then
            '    ControlName = frmGrdRptCustomerWiseCashRecovery
        ElseIf key = "frmRptServicesStockLedger" Then
            ApplyStyleSheet(frmRptServicesStockLedger)
            frmRptServicesStockLedger.ShowDialog()
            Exit Sub
            'ClosingStockByOrder
        ElseIf key = "frmInstallment" Then
            ControlName = frmInstallment
            'ElseIf key = "frmGrdRptInstallmentBalance" Then
            '    ControlName = frmGrdRptInstallmentBalance
        ElseIf key = "frmRptServicesReports" Then
            ApplyStyleSheet(frmRptServicesReports)
            frmRptServicesReports.ShowDialog()
            Exit Sub
        ElseIf key = "rptTodayTasks" Then
            ApplyStyleSheet(rptTodayTasks)
            rptTodayTasks.ShowDialog()
            Exit Sub
        ElseIf key = "frmTerminalConfiguration" Then
            ControlName = frmTerminalConfiguration
        ElseIf key = "frmDefEmployeeMonthlyTarget" Then
            ControlName = frmDefEmployeeMonthlyTarget
        ElseIf key = "ProductionDispatch" Then
            ControlName = frmServicesDispatch
        ElseIf key = "frmGrdRptCustomerWiseCashRecovery" Then
            ControlName = frmGrdRptCustomerWiseCashRecovery
        ElseIf key = "ServicesInvoice" Then
            ControlName = frmServicesInvoices

        ElseIf key = "ProductionProcessing" Then
            ControlName = frmServicesProduction

        ElseIf key = "LoanApprovalList" Then
            ControlName = frmGrdRptLoanApprovalList
        ElseIf key = "rptDuplicateDocuments" Then
            ControlName = frmGrdRptDuplicateDocuments
        ElseIf key = "frmInwardGatePass" Then
            ControlName = frmIGP
        ElseIf key = "frmWorkInProcess" Then
            ControlName = frmWIP
        ElseIf key = "VoucherPost" Then
            ControlName = frmVoucherPost

        ElseIf key = "HolidaySetup" Then
            ControlName = frmHolidySetup
            'Ali 
        ElseIf key = "TaxSlabs" Then
            ControlName = frmDefTaxSlabs

            'Task#17082015 Add link for Employee Site Visit Charges (Ahmad Sharif)
        ElseIf key = "EmployeeSiteVisitCharges" Then
            ControlName = FrmEmployeeSiteVisitCharges

        ElseIf key = "EmployeeNoofSiteVisits" Then
            ControlName = FrmEmployeeSiteVisit
            'End Task#17082015
        ElseIf key = "frmGRNStatus" Then
            ControlName = frmGRNStatus
        ElseIf key = "frmDeliveryChalanStatus" Then
            ControlName = frmDeliveryChalanStatus
        ElseIf key = "CustomerMonthlyTarget" Then
            ControlName = frmCustomerRecoveryTarget
        ElseIf key = "Installment" Then
            ControlName = frmInstallment
        ElseIf key = "ProjectVisit" Then
            ControlName = frmGrdRptProjectVisitDetail
        ElseIf key = "frmProductionProcessing" Then
            ControlName = frmProductionProcessing
        ElseIf key = "TroubleShoot" Then
            ControlName = frmTroubleshoot
            'Altered By Ali Ansari against Task#20150510 
        ElseIf key = "frmDateLockPermission" Then
            ControlName = frmDateLockPermission
        ElseIf key = "frmRptProjectHistory" Then
            ApplyStyleSheet(frmRptProjectHistory)
            frmRptProjectHistory.ShowDialog()
            frmRptProjectHistory.BringToFront()
            Exit Sub
            'Altered By Ali Ansari against Task#20150618 Add Partner Form
        ElseIf key = "Partner" Then
            ControlName = frmDefPartners
            'Altered By Ali Ansari against Task#20150618 Add Partner Form
        ElseIf key = "ProjectPortfolio" Then
            ControlName = frmProjectPortFolio
            'Altered By Ali Ansari against Task#20150511 
            ''add Region,Zone,Belt,State,Country forms
        ElseIf key = "QuotationStatus" Then
            ControlName = frmGrdRptQuotationStatus
        ElseIf key = "frmProjectVisit" Then
            ControlName = frmProjectVisit
        ElseIf key = "frmProjectVisitType" Then
            ControlName = frmProjectVisitType
        ElseIf key = "Country" Then
            ControlName = FrmCountry
        ElseIf key = "State" Then
            ControlName = frmState
        ElseIf key = "Region" Then
            ControlName = FrmRegions
        ElseIf key = "Zone" Then
            ControlName = FrmZone
        ElseIf key = "Belt" Then
            ControlName = FrmBelt
            'Altered By Ali Ansari against Task#2015051
        ElseIf key = "frmGrdRptCostSheetQtyWise" Then
            ControlName = frmGrdRptCostSheetQtyWise
        ElseIf key = "CustomerSalesContribution" Then
            ApplyStyleSheet(frmRptCustomerSalesContribution)
            frmRptCustomerSalesContribution.ShowDialog()
            Exit Sub

            'ElseIf key = "frmSalaryConfig" Then
            '    ApplyStyleSheet(frmSalaryConfig)
            '    frmSalaryConfig.ShowDialog()
            '    Exit Sub
        ElseIf key = "frmAutoSalaryGenerate" Then
            ControlName = frmAutoSalaryGenerate
        ElseIf key = "frmAdvanceRequest" Then
            ControlName = frmAdvanceRequest
        ElseIf key = "frmGrdRptPurchaseDemandStatus" Then
            ControlName = frmGrdRptPurchaseDemandStatus
        ElseIf key = "frmEmpLoanDeductions" Then
            ControlName = frmEmployeeDeductions
        ElseIf key = "rptAdvancePaymentsPO" Then
            rptDateRange.ReportName = rptDateRange.ReportList.AdvancePaymentsPO
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "frmGrdRptLocationWiseStockStatementNew" Then
            ControlName = frmGrdRptLocationWiseStockStatementNew
        ElseIf key = "rptDispatchStatus" Then
            rptDateRange.ReportName = rptDateRange.ReportList.DispatchStatus
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptAdvanceReceiptsSO" Then
            rptDateRange.ReportName = rptDateRange.ReportList.AdvanceReceiptsSO
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "frmRptProjectBasedTransactionDetail" Then
            ApplyStyleSheet(frmRptProjectBasedTransactionDetail)
            frmRptProjectBasedTransactionDetail.ShowDialog()
            Exit Sub
        ElseIf key = "frmRptEmpSalarySheetDetail" Then
            ApplyStyleSheet(frmRptEmpSalarySheetDetail)
            frmRptEmpSalarySheetDetail.ShowDialog()
            Exit Sub
        ElseIf key = "rptLocationWiseClosingStock" Then
            ApplyStyleSheet(frmRptLocationWiseClosingStock)
            frmRptLocationWiseClosingStock.ShowDialog()
            Exit Sub
        ElseIf key = "frmSalesTransfer" Then
            ControlName = frmSalesTransfer
        ElseIf key = "frmCashRecoveryReport" Then
            frmCashRecoveryReport.ReportName = frmCashRecoveryReport.enmReportList.ChequeRecovery
            ApplyStyleSheet(frmCashRecoveryReport)
            frmCashRecoveryReport.ShowDialog()
            Exit Sub
        ElseIf key = "PriceCompare" Then
            ControlName = frmGrdRptSalesPriceChange
        ElseIf key = "frmGrdRptEmployeeTargetAchieved" Then
            ControlName = frmGrdRptEmployeeMonthlyTergetAchieved
        ElseIf key = "CustomerChequesDueAll" Then
            frmCashRecoveryReport.ReportName = frmCashRecoveryReport.enmReportList.ChequeDueAll
            ApplyStyleSheet(frmCashRecoveryReport)
            frmCashRecoveryReport.ShowDialog()
            Exit Sub

        ElseIf key = "frmRptTaskDetail" Then
            'Marked Against Task#20150516 blocking forms and style sheets
            'ApplyStyleSheet(frmRptTaskDetail)
            'frmRptTaskDetail.ShowDialog()
            'Exit Sub
            'Marked Against Task#20150516 blocking forms and style sheets
            'Altered Against Task#20150516 blocking forms and style sheets
            RestrictForm = key
            If RestrictSheet(RestrictForm) = False Then
                ApplyStyleSheet(frmRptTaskDetail)
                frmRptTaskDetail.ShowDialog()
                Exit Sub
            Else
                ControlName = frmdisplay
                RestrictForm = String.Empty
            End If
            'Altered Against Task#20150516 blocking forms and style sheets
        ElseIf key = "WarrantyDetailReport" Then
            rptDateRange.ReportName = rptDateRange.ReportList.WarrantyDetailReport
            rptDateRange.PnlCostTop = True
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptSummaryOfSalesTaxInvoices" Then
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ReportName = rptDateRange.ReportList.rptSummaryOfSalesTaxInvoices
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "frmGrdRptCMFAllRecords" Then
            ControlName = frmGrdRptCMFAllRecords
        ElseIf key = "frmCMFAAll" Then
            ApplyStyleSheet(frmCMFAAll)
            frmCMFAAll.ShowDialog()
            Exit Sub
        ElseIf key = "frmImport" Then
            ControlName = frmImport
        ElseIf key = "ChequeAdjustment" Then
            ControlName = frmChequesAdjustment

        ElseIf key = "rptEmpAttendanceDetail" Then
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ReportName = rptDateRange.ReportList.EmployeeAttendanceDetail
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "frmRptMonthlyPurchaseSummary" Then
            ApplyStyleSheet(frmRptMonthlyPurchaseSummary)
            frmRptMonthlyPurchaseSummary.ShowDialog()
            Exit Sub
        ElseIf key = "frmGrdRptLocationWiseStockStatement" Then
            ControlName = frmGrdRptLocationWiseStockStatement


        ElseIf key = "frmSMSConfiguration" Then
            'Marked Against Task#20150516 regarding block style sheets
            'If RestrictSheet(key) = False Then
            ' ApplyStyleSheet(frmSMSConfig)
            ' frmSMSConfig.ShowDialog()
            ' Exit Sub
            'End If
            'Marked Against Task#20150516 regarding block style sheets
            'Altered Against Task#20150516 regarding block style sheets
            RestrictForm = key
            If RestrictSheet(RestrictForm) = False Then
                ApplyStyleSheet(frmSMSConfig)
                frmSMSConfig.ShowDialog()
                Exit Sub
            Else
                ControlName = frmdisplay
                RestrictForm = String.Empty
            End If
            'Altered Against Task#20150516 regarding block style sheets
        ElseIf key = "frmMRPlan" Then
            ControlName = frmMRPlan

        ElseIf key = "rptPurchaseItemSummary" Then
            rptDateRange.ReportName = rptDateRange.ReportList.PurchaseItemSummary
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
            'End Task:2850
            '
            ''2-Oct-2014 Task:2863 Imran Ali Add new report Item Wise Sales Summary

        ElseIf key = "frmGrdRptSalesRegisterActivity" Then
            ControlName = frmGrdRptSalesRegisterActivity
        ElseIf key = "frmSaleInvoiceDueDate" Then
            ControlName = frmGrdRptSaleInvoicesDue

        ElseIf key = "DefineDocumentsPrefix" Then
            ControlName = frmDefDocumentPrefix
        ElseIf key = "frmGrdRptItemWiseSalesSummary" Then
            ControlName = frmGrdRptItemWiseSalesSummary
            'End Task:2863
        ElseIf key = "frmGrdRptCMFAOfSummaries" Then
            ControlName = frmGrdRptCMFAOfSummaries
            'TAsk:2864 Setting for show criteria cash receipt detail report
        ElseIf key = "rptCashReceiptsDetailAgainstEmployee" Then
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ReportName = rptDateRange.ReportList.CashReceiptDetailAgainstEmployee
            rptDateRange.IsEmployee = True
            rptDateRange.ShowDialog()
            Exit Sub
            'End task:2864
        ElseIf key = "frmRptCMFADetail" Then
            If Not LoginGroup.ToString = "Administrator" Then
                ControlName.Name = "frmRptCMFADetail"
                Rights = GroupRights.FindAll(AddressOf ReturnRights)
                If Rights.Count = 0 Then Exit Sub
            End If
            ApplyStyleSheet(frmRptCMFADetail)
            frmRptCMFADetail.ShowDialog()
            Exit Sub
        ElseIf key = "frmRptBankReconciliation" Then
            ApplyStyleSheet(frmRptBankReconciliation)
            frmRptBankReconciliation.ShowDialog()
            Exit Sub
            ''05-Aug-2014 Task:2769 Imran Ali Add new report CMFA Summary (Ravi)
        ElseIf key = "frmSMSTemplate" Then
            ApplyStyleSheet(frmSMSTemplate)
            frmSMSTemplate.ShowDialog()
            Exit Sub
        ElseIf key = "frmInvoiceAdjustment" Then
            ControlName = frmInvoiceAdjustment
        ElseIf key = "frmGrdRptCMFASummary" Then
            ControlName = frmGrdRptCMFASummary
            'End Task:2769
            'Task:2810 Show PL Account Summary Report
        ElseIf key = "rptPLNoteDetailAccountSummary" Then
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ReportName = rptDateRange.ReportList.PLDetailAccountSummary
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptPLNoteSubSubAccountSummary" Then
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ReportName = rptDateRange.ReportList.PLSubsubAccountSummary
            rptDateRange.ShowDialog()
            Exit Sub
            'End Task:2810
        ElseIf key = "frmRptDirectorDebitors" Then
            ApplyStyleSheet(frmRptDirectorDebitors)
            frmRptDirectorDebitors.ShowDialog()
            Exit Sub
            ''Task:2434 Added Menu Average Rate
        ElseIf key = "frmAdjeustmentAveragerate" Then
            ControlName = frmAdjeustmentAveragerate
            'Task:2678 Open Sales Certificate Issued Report By User
        ElseIf key = "frmGrdRptSalesCertificateIssued" Then
            ControlName = frmGrdRptSalesCertificateIssued
            'End Task:2678
            'Task:2690 Added Menu Production Comparison in Produciton
        ElseIf key = "frmGrdRptProductionComparison" Then
            ControlName = frmGrdRptProductionComparison
            'End Task:2690
        ElseIf key = "updAvgRate" Then
            ApplyStyleSheet(frmAverageRateUpdate)
            frmAverageRateUpdate.ShowDialog()
            Exit Sub
            'End Task:2434
        ElseIf key = "frmRptInvoiceAgingFormated" Then
            ApplyStyleSheet(frmRptInvoiceAgingFormated)
            frmRptInvoiceAgingFormated.ShowDialog()
            Exit Sub
            'Task:2638 Added New Menu Warranty Claim In Inventory Menu
        ElseIf key = "frmClaim" Then
            ControlName = frmClaim
            'End Task:2638
            If Tags.Length > 0 Then
                frmClaim.Get_All(Tags)
            End If
        ElseIf key = "Groups" Then
            ControlName = frmDefSecurityGroup
            enm = EnumForms.frmDefSecurityGroup
            Me.Cursor = Cursors.WaitCursor
        ElseIf key = "CustomerBasedDiscounts" Then
            ControlName = frmCustomerDiscounts
            enm = EnumForms.frmSubAccount
        ElseIf key = "CustomerBasedDiscountsFlat" Then
            ControlName = frmCustomerDiscountsFlat
            enm = EnumForms.frmSubAccount
        ElseIf key = "frmSubAccount" Then
            ControlName = frmSubAccount
            enm = EnumForms.frmSubAccount
            If Tags.Length > 0 Then
                frmSubAccount.Get_All(Tags)
            End If
        ElseIf key = "frmSubSubAccount" Then
            ControlName = frmSubSubAccount
            enm = EnumForms.frmSubSubAccount
            If Tags.Length > 0 Then
                frmSubSubAccount.Get_All(Tags)
            End If
        ElseIf key = "frmDetailAccount" Then
            ControlName = frmDetailAccount
            enm = EnumForms.frmDetailAccount
            If Tags.Length > 0 Then
                frmDetailAccount.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ProductionStep" Then
            ControlName = frmproductionSteps
        ElseIf key = "frmProductionLevel" Then
            ControlName = frmProductionLevel
            enm = EnumForms.Non
        ElseIf key = "frmGrdProductionAnalysis" Then
            ControlName = frmGrdProductionAnalaysis
        ElseIf key = "frmGrdRptProductionLevel" Then
            ControlName = frmGrdRptProductionLevel
            enm = EnumForms.Non
        ElseIf key = "FrmEmailconfig" Then
            ControlName = FrmEmailconfig
            ' enm = EnumForms.FrmEmailconfig
        ElseIf key = "frmNewInvItem" Then
            ControlName = frmDefArticle
            enm = EnumForms.SimpleItemDefForm
        ElseIf key = "ArticleDepartment" Then
            ControlName = frmDefArticleDepartment
            enm = EnumForms.SimpleItemDefForm
        ElseIf key = "GenerateBarcodes" Then
            'ControlName = frmBarcodes
            ' enm = EnumForms.SimpleItemDefForm
            'ElseIf key = "frmVoucher" Then
            '    ControlName = frmVoucher
            '    enm = EnumForms.frmVoucher
        ElseIf key = "frmVoucher" Then
            ControlName = frmVoucherNew
            enm = EnumForms.frmVoucher
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            If Tags.Length > 0 Then
                frmVoucherNew.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "voucherposting" Then
            ControlName = frmVoucherPostUnpost
            enm = EnumForms.frmVoucher
        ElseIf key = "frmBankReconciliation" Then
            ControlName = frmBankReconciliation
            enm = EnumForms.frmVoucher
        ElseIf key = "MobileExpenseEntry" Then
            ControlName = frmMobileExpense
            enm = EnumForms.frmVoucher

            'Task#31072015 Employee Attendance email alert
        ElseIf key = "EmployeeAttendanceEmailAlert" Then
            ControlName = frmEmpAttendanceEmailAlertSchedule
            'End Task#31072015

        ElseIf key = "frmGrdRptCustomerItemWiseSummary" Then
            ControlName = frmGrdRptCustomerItemWiseSummary
        ElseIf key = "frmChangeDetailAccount" Then
            ControlName = frmChangeDetailAccount
        ElseIf key = "frmFrequentlySalesItem" Then
            ControlName = frmGrdRptFrequentellySalesOrderItems

        ElseIf key = "LC" Then
            ControlName = frmLetterCredit
            enm = EnumForms.frmVoucher
        ElseIf key = "EmpSalary" Then
            ControlName = frmEmployeeSalaryVoucher
            enm = EnumForms.frmVoucher
        ElseIf key = "empbarcodestiker" Then
            ShowReport("rptEmployeeBarcodeSticker", , , , , , , GetEmployeeBarcodeStickerData)
            Exit Sub
        ElseIf key = "DailyWagies" Then
            ControlName = frmDailySalaries
            enm = EnumForms.frmVoucher
        ElseIf key = "rptIssuedSalesCertificate" Then
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ReportName = rptDateRange.ReportList.SalesCertificateIssued
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "frmPurchase" Then
            ControlName = frmPurchaseNew
            enm = EnumForms.frmPurchase
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Tags.Length > 0 Then
                frmPurchaseNew.Get_All(Tags)
                'Tags = String.Empty
            End If
        ElseIf key = "frmReceivingNote" Then
            ControlName = frmReceivingNote
            enm = EnumForms.Purchase
        ElseIf key = "frmPurchaseOrder" Then
            ControlName = frmPurchaseOrderNew
            enm = EnumForms.frmPurchaseOrder
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Me.Tags.Length > 0 Then
                frmPurchaseOrderNew.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmItemBulk" Then
            ControlName = frmItemBulk
        ElseIf key = "PurchaseReturn" Then
            ControlName = frmPurchaseReturn
            enm = EnumForms.frmPurchaseReturn
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Tags.Length > 0 Then
                frmPurchaseReturn.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmSalesOrder" Then
            ControlName = frmSalesOrderNew
            enm = EnumForms.frmSaleOrder
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            If Tags.Length > 0 Then
                frmSalesOrderNew.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmQoutationNew" Then
            ControlName = frmQoutationNew
            enm = EnumForms.frmSaleOrder
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            If Tags.Length > 0 Then
                frmQoutationNew.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "RecordSales" Then
            ControlName = frmSales
            enm = EnumForms.frmSales
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            If Tags.Length > 0 Then
                frmSales.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "frmDeliveryChalan" Then
            ControlName = frmDeliveryChalan
            enm = EnumForms.frmSales
        ElseIf key = "frmRptGraphs" Then
            ControlName = frmRptGraphs
            enm = EnumForms.Non
        ElseIf key = "SalesReturn" Then
            ControlName = frmSalesReturn
            enm = EnumForms.frmSalesReturn
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            If Tags.Length > 0 Then
                frmSalesReturn.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ProductionStore" Then
            ControlName = frmProductionStore
            enm = EnumForms.frmProductionStore
            If Tags.ToString.Length > 0 Then
                frmProductionStore.Get_All(Tags.ToString)
            End If
        ElseIf key = "frmCmfa" Then
            ControlName = frmCmfa
        ElseIf key = "frmStockAdjustment" Then
            ControlName = frmStockAdjustment
            'enm = EnumForms.frmStockAdjustment

            'ElseIf key = "ReturnableGatepass" Then
            '    ControlName = frmReturnablegatepass
            '    enm = EnumForms.frmReturnablegatepass

            'Ahmad sharif: added project quotation
        ElseIf key = "frmProjectQuotation" Then
            ControlName = frmProjQuotion
            'enm = EnumForms.frmReturnablegatepass

        ElseIf key = "InwardGatepass" Then

            ControlName = frmServicesInwardGatePass
            enm = EnumForms.frmInwardGatePass

        ElseIf key = "outwardGatepass" Then

            ControlName = frmServicesInwardGatePass
            enm = EnumForms.frmInwardGatePass


        ElseIf key = "frmRptGrdPurchaseDetailWithWeight" Then
            ControlName = frmRptGrdPurchaseDetailWithWeight
        ElseIf key = "frmGrdRptEmployeeMonthlyAttendance" Then
            ControlName = frmGrdRptEmployeeMonthlyAttendance

            'Task#118062015 Ahmad Sharif
        ElseIf key = "frmGrdRptCostSheetMarginCalculationDetail" Then
            ControlName = frmGrdRptCostSheetMarginCalculationDetail
            'End Task#118062015
        ElseIf key = "frmGrdPlanComparison" Then
            ControlName = frmGrdPlanComparison
            enm = EnumForms.Non

        ElseIf key = "UpdateReturnableGatepass" Then
            ControlName = frmUpdateReturnableGatepassDetail
            enm = EnumForms.frmReturnablegatepass

        ElseIf key = "itemWiseRpt" Then
            ControlName = ItemWiseSalesrpt
            enm = EnumForms.frmSalesReturn
        ElseIf key = "StoreIssuence" Then
            ControlName = frmStoreIssuence
            enm = EnumForms.frmStoreIssuence
            If Tags.Length > 0 Then
                frmStoreIssuence.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmReturnStoreIssuance" Then
            ControlName = frmReturnStoreIssuence
            enm = EnumForms.frmStoreIssuence
            If Tags.Length > 0 Then
                frmReturnStoreIssuence.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "SalesChart" Then
            ControlName = rptSalesChart
            enm = EnumForms.frmStoreIssuence

        ElseIf key = "Tasks" Then
            ControlName = frmTasks
            'Task 2639 JUNAID VendorType
        ElseIf key = "frmVendorTypeKey" Then
            ControlName = frmVendorType
            'End task 2639
            'Task 2640 JUNAID
        ElseIf key = "SMSScheduleKey" Then
            ControlName = frmScheduleSMS
            'End task 2640

        ElseIf key = "TaskWorking" Then
            ControlName = frmTaskWork

        ElseIf key = "Type" Then
            ControlName = frmTypes

        ElseIf key = "Status" Then
            ControlName = frmStatus

        ElseIf key = "frmRptGrdAdvances" Then
            ApplyStyleSheet(frmRptGrdAdvances)
            ControlName = frmRptGrdAdvances

        ElseIf key = "rptTaskAssign" Then
            'Marked Against Task#20150516 regarding block style sheets
            '    RptDateRangeEmployees.ReportName = RptDateRangeEmployees.ReportList.rptTaskAssign
            '    ApplyStyleSheet(RptDateRangeEmployees)
            '    RptDateRangeEmployees.ShowDialog()
            'Marked Against Task#20150516 regarding block style sheets
            'Altered Against Task#20150516 regarding block style sheets
            RestrictForm = key
            If RestrictSheet(RestrictForm) = False Then
                RptDateRangeEmployees.ShowDialog()
                Exit Sub
            Else
                ControlName = frmdisplay
                RestrictSheetAccess = String.Empty
            End If
            'Altered Against Task#20150516 regarding block style sheets
        ElseIf key = "frmGrdRptMinimumStockLevel" Then
            ControlName = frmGrdRptMinimumStockLevel
        ElseIf key = "ClosingStockByOrder" Then
            ControlName = frmGrdRptClosingStockByOrders
        ElseIf key = "ArticleBarcodePrinting" Then
            ApplyStyleSheet(frmItemBarCodePrinting)
            frmItemBarCodePrinting.ShowDialog()
            Exit Sub
        ElseIf key = "frmStockStatmentBySize" Then
            ApplyStyleSheet(frmStockStatmentBySize)
            ControlName = frmStockStatmentBySize

        ElseIf key = "ComposeMessage" Then
            ControlName = frmComposeMessage

        ElseIf key = "Message" Then
            ControlName = frmMessageView
        ElseIf key = "salesmancommission" Then
            ControlName = frmGrdSalesmanCommissionDetail
            ''Task:2357 Added Control
        ElseIf key = "frmGrdRptSalesComparison" Then
            ControlName = frmGrdRptSalesComparison
            'End Task:2357
            ''Tsk:2370 Added Control 
        ElseIf key = "frmGrdRptInvoiceAging" Then
            ControlName = frmGrdRptInvoiceAging
            ''End Task:2370
            ''12-Mar-2014  TASK:2488  Imran Ali Sales Certificate In ERP
        ElseIf key = "frmSalesCertificate" Then
            ControlName = frmSalesCertificate
            'End task:2488
        ElseIf key = "salescomparison" Then
            ControlName = frmSalesComparisonCustomerWise

        ElseIf key = "frmRequestViews" Then
            ControlName = frmRequestViews

        ElseIf key = "OpeningBL" Then
            ApplyStyleSheet(frmOpening)
            frmOpening.ShowDialog()
        ElseIf key = "frmYearClose" Then
            ControlName = frmYearClose
            enm = EnumForms.frmVoucher
        ElseIf key = "SalesmanDealerVoucher" Then
            ApplyStyleSheet(rptVoucher)
            rptVoucher.ShowDialog()
            Exit Sub
        ElseIf key = "SalesmanMonthlySales" Then
            ControlName = rptSalesmanMonthlySalesReport
            enm = EnumForms.Sales
        ElseIf key = "SalesDetail" Then
            ControlName = frmGrdSales
        ElseIf key = "SectorWiseSales" Then
            ControlName = frmGrdRptSectorSales
        ElseIf key = "SalesDtbyCategory" Then
            ControlName = frmGrdRptSalesByGender
        ElseIf key = "DailyWorkingReport" Then
            ControlName = rptDailyWorkingReport
            enm = EnumForms.Sales
            ''06-Mar-2014   Task:2462  Imran Ali    Add New Report Purchase Daily Working
        ElseIf key = "rptPurchaseDailyWorkingReport" Then
            ControlName = rptPurchaseDailyWorkingReport
            enm = EnumForms.frmPurchase
            'End Task:2462

        ElseIf key = "rptGrdEachDaysWorking" Then
            ControlName = rptGrdEachDaysWorking
        ElseIf key = "CustomerCollection" Then
            ControlName = frmCustomerCollection
            enm = EnumForms.frmCustomerCollection
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            If Tags.Length > 0 Then
                frmCustomerCollection.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "VendorPayments" Then
            ControlName = frmVendorPayment
            enm = EnumForms.frmVendorPayment
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            If Tags.Length > 0 Then
                frmVendorPayment.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "PaymentVendor" Then
            ControlName = frmPaymentVoucherNew
            enm = EnumForms.frmVendorPayment
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General

        ElseIf key = "ReceiptCustomer" Then
            ControlName = frmReceiptVoucherNew
            enm = EnumForms.frmCustomerCollection
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General

        ElseIf key = "CustomerType" Then
            ControlName = frmDefCustomerType
            enm = EnumForms.DefMainAcc
            If Tags.Length > 0 Then
                frmDefCustomerType.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "EmpAttendance" Then
            ControlName = frmAttendance
            enm = EnumForms.frmAttendance
        ElseIf key = "Adjustment" Then
            ControlName = frmAdjustments
            'enm = EnumForms.Sales
        ElseIf key = "EmpSalaryRpt" Or key = "EmpSalaryRpt1" Then
            ControlName = frmGrdRptEmployeeSalarySheetDetail
            enm = EnumForms.Non
        ElseIf key = "DailyAttendance" Then
            ControlName = frmAttendanceEmployees
            enm = EnumForms.frmAttendance

        ElseIf key = "rptDailyAttendance" Then
            'AddRptParam("@CurrentDate", Date.Now.Date.ToString("yyyy-M-d 00:00:00"))
            'ShowReport("rptEmployeesAttendance")
            rptDateRange.ReportName = rptDateRange.ReportList.DailyEmployeeAttendance
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
            'Task:2418 Added Report With Holding Tax Certificate
        ElseIf key = "WithHoldingTaxCertificate" Then
            rptDateRange.ReportName = rptDateRange.ReportList.WHTaxCertificate
            ApplyStyleSheet(rptDateRange)
            rptDateRange.IsVendor = True
            rptDateRange.ShowDialog()
            rptDateRange.IsVendor = False
            Exit Sub
            'End Task:2418
        ElseIf key = "FrmVoucherCheckList" Then
            ApplyStyleSheet(FrmVoucherCheckList)
            FrmVoucherCheckList.ShowDialog()
            Exit Sub
        ElseIf key = "rptAttendanceSummary" Then
            rptDateRange.ReportName = rptDateRange.ReportList.AttendanceSummary
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "DemandSumm" Then
            rptDateRange.ReportName = rptDateRange.ReportList.DemandSummary
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "DemandDt" Then
            ControlName = frmGrdRptDemandDetail
            enm = EnumForms.Non
        ElseIf key = "grdDispatchDetail" Then
            ControlName = frmGrdDispatchDetail
            'Task:2410   Added Menu Return Detail
        ElseIf key = "frmGrdSalesReturnDetail" Then
            ControlName = frmGrdSalesReturnDetail
            'End Task:2410
        ElseIf key = "grdSaleManDemandDetail" Then
            ControlName = frmGrdSalemansDemandDetail
        ElseIf key = "AttendanceSummary" Then
            RptDateRangeEmployees.ReportName = RptDateRangeEmployees.ReportList.ReportAttendanceSummary
            ApplyStyleSheet(RptDateRangeEmployees)
            RptDateRangeEmployees.ShowDialog()
            Exit Sub
        ElseIf key = "DemandSales" Then
            rptDateRange.ReportName = rptDateRange.ReportList.DemandSales
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptReturnableGatepass" Then
            rptDateRange.ReportName = rptDateRange.ReportList.ReturnableGatepass
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "frmDateLock" Then
            ControlName = frmDateLock
            enm = EnumForms.Non
        ElseIf key = "frmChequeBook" Then
            ControlName = frmAddChequeBookSerial
            enm = EnumForms.Non
        ElseIf key = "DailySupplyGatepass" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.DailySupply
            'ApplyStyleSheet(rptDateRange)
            'rptDateRange.ShowDialog()
            ControlName = frmGrdDailySupply
            'Customer Collection
        ElseIf key = "ConfigCompany" Then
            ControlName = frmDefCategory
            enm = EnumForms.frmDefCompany
            If Tags.Length > 0 Then
                frmDefCategory.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmDepartment" Then
            ControlName = frmDefDepartment
            enm = EnumForms.frmDefEmployee
            If Tags.Length > 0 Then
                frmDefDepartment.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "frmEmployeeCards" Then
            ControlName = frmEmployeeCards
            enm = EnumForms.Non
        ElseIf key = "EmployeeCard" Then
            ControlName = frmEmployeeCard
        ElseIf key = "UnitOfItem" Then
            ControlName = frmDefUnit
            ' enm = EnumForms.frmDefEmployee
            If Tags.Length > 0 Then
                frmDefUnit.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "frmDesignation" Then
            ControlName = frmDefEmpDesignation
            enm = EnumForms.frmDefEmployee
            If Tags.Length > 0 Then
                frmDefEmpDesignation.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigLPO" Then
            ControlName = frmDefSubCategory
            enm = EnumForms.frmDefLpo
            If Tags.Length > 0 Then
                frmDefSubCategory.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigSize" Then
            ControlName = frmDefSize
            enm = EnumForms.frmDefSize
            If Tags.Length > 0 Then
                frmDefSize.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigColor" Then
            ControlName = frmDefColor
            enm = EnumForms.frmDefColor
            If Tags.Length > 0 Then
                frmDefColor.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmDefGender" Then
            ControlName = frmDefGender
            enm = EnumForms.frmDefColor
            If Tags.Length > 0 Then
                frmDefGender.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigCategory" Then
            'ControlName = frmDefCategory
            ControlName = frmDetailAccountCat
            enm = EnumForms.frmDefCategory

        ElseIf key = "CostCenter" Then
            ControlName = frmCostCenter
            enm = EnumForms.DefMainAcc
            If Tags.Length > 0 Then
                frmCostCenter.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "frmAddCostCenter" Then
            'ControlName = frmAddCostCenter
            'enm = EnumForms.DefMainAcc
            'Task:2830 Apply Security Add New Customer
            If Not LoginGroup.ToString = "Administrator" Then
                ControlName.Name = "CostCenter"
                Rights = GroupRights.FindAll(AddressOf ReturnRights)
                If Rights.Count = 0 Then Exit Sub
            End If
            'End Task:2830

            frmAddCostCenter.ShowDialog()

        ElseIf key = "frmEmailTemplate" Then
            ApplyStyleSheet(frmEmailTemplate)
            frmEmailTemplate.ShowDialog()
            Exit Sub
        ElseIf key = "DailyTask" Then
            AddRptParam("@CurrentDate", Date.Now.Date.ToString("yyyy-M-d 00:00:00"))
            ShowReport("rptTask")

        ElseIf key = "EmpWiseTaskReport" Then
            RptDateRangeEmployees.ReportName = RptDateRangeEmployees.ReportList.EmployeeTask
            ApplyStyleSheet(RptDateRangeEmployees)
            RptDateRangeEmployees.ShowDialog()
            Exit Sub
        ElseIf key = "PrintVehicleLog" Then
            rptDateRange.ReportName = rptDateRange.ReportList.PrintVehicleLog
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "CompanyInfo" Then
            ControlName = frmCompanyInformation
            enm = EnumForms.DefMainAcc
            If Tags.Length > 0 Then
                frmCompanyInformation.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "AddCustomer" Then
            ControlName = FrmAddCustomers
            enm = EnumForms.DefMainAcc

        ElseIf key = "NewCustomerDefine" Then
            'Task:2830 Apply Security Add New Customer
            If Not LoginGroup.ToString = "Administrator" Then
                ControlName.Name = "AddCustomer"
                Rights = GroupRights.FindAll(AddressOf ReturnRights)
                If Rights.Count = 0 Then Exit Sub
            End If
            'End Task:2830
            Dim CustId As Integer = 0
            FrmAddCustomers.FormType = "Customer"
            FrmAddCustomers.ShowDialog()

        ElseIf key = "NewCashAccount" Then
            'Task:2830 Apply Security Add New Cash Account
            If Not LoginGroup.ToString = "Administrator" Then
                ControlName.Name = "Cash"
                Rights = GroupRights.FindAll(AddressOf ReturnRights)
                If Rights.Count = 0 Then Exit Sub
            End If
            'End Task:2830
            Dim CustId As Integer = 0
            FrmAddCustomers.FormType = "Cash"
            FrmAddCustomers.ShowDialog()
        ElseIf key = "frmDataTransfer" Then
            ApplyStyleSheet(frmDataTransfer)
            frmDataTransfer.ShowDialog()
            Exit Sub
        ElseIf key = "frmDataImport" Then
            ApplyStyleSheet(frmDataImport)
            frmDataImport.ShowDialog()
            Exit Sub
        ElseIf key = "NewBankAccount" Then
            'Task:2830 Apply Security Add New Bank Account
            If Not LoginGroup.ToString = "Administrator" Then
                ControlName.Name = "Bank"
                Rights = GroupRights.FindAll(AddressOf ReturnRights)
                If Rights.Count = 0 Then Exit Sub
            End If
            'End Task:2830
            Dim CustId As Integer = 0
            FrmAddCustomers.FormType = "Bank"
            FrmAddCustomers.ShowDialog()
            Exit Sub
        ElseIf key = "NewVendorDefine" Then
            'Task:2830 Apply Security Add New Vendor
            If Not LoginGroup.ToString = "Administrator" Then
                ControlName.Name = "AddVendor"
                Rights = GroupRights.FindAll(AddressOf ReturnRights)
                If Rights.Count = 0 Then Exit Sub
            End If
            'End Task:2830
            Dim CustId As Integer = 0
            FrmAddCustomers.FormType = "Vendor"
            FrmAddCustomers.ShowDialog()
            Exit Sub
        ElseIf key = "AboutUs" Then
            'ApplyStyleSheet(frmaboutus)
            frmaboutus.ShowDialog()
            Exit Sub
        ElseIf key = "ConfigVendor" Then
            ControlName = frmDefVendor
            enm = EnumForms.frmDefVendor
            If Tags.Length > 0 Then
                frmDefVendor.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigCustomer" Then
            ControlName = frmDefCustomer
            enm = EnumForms.frmDefCustomer
            If Tags.Length > 0 Then
                frmDefCustomer.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigType" Then
            ControlName = frmDefType
            enm = EnumForms.frmDefType
            If Tags.Length > 0 Then
                frmDefType.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

            ''changes by ali ansari
        ElseIf key = "rptSaleCertificateLedger" Then
            ApplyStyleSheet(frmRptSalesCertificateLedger)
            frmRptSalesCertificateLedger.ShowDialog()
            Exit Sub
        ElseIf key = "frmGrdRptOrdersDetail" Then
            ''changes by ali ansari
            ControlName = frmGrdRptOrdersDetail

        ElseIf key = "CustomerBasedTarget" Then
            ControlName = frmGrdCustomerBasedTarget
        ElseIf key = "SalesmanCommission" Then
            ControlName = frmDefCommissionBySaleman
        ElseIf key = "frmGrdRptContactList" Then
            ControlName = frmGrdRptContactList
            enm = EnumForms.Non
        ElseIf key = "CustomerSalesHistory" Then
            ControlName = frmGrdRptSalesHistory
        ElseIf key = "VehicleInformation" Then
            ControlName = frmDefVehicle
        ElseIf key = "RootPlan" Then
            ControlName = frmRootPlan
        ElseIf key = "VehicleLog" Then
            ControlName = FrmVehicle
        ElseIf key = "CustSumSalesChart" Then
            ControlName = frmGrdRptCustomersWiseSummarySalesChart
        ElseIf key = "CustItemsSummarySales" Then
            ControlName = frmGrdRptCustomersItemsSummarySales
        ElseIf key = "EmpMonthlySales" Then
            ControlName = frmEmployeeWiseMonthlySale
            enm = EnumForms.Non
        ElseIf key = "frmGrdArticleLedger" Then
            ControlName = frmGrdArticleLedger
            enm = EnumForms.Non
        ElseIf key = "frmGrdRptTaxDuductionDetail" Then
            ControlName = frmGrdRptTaxDuductionDetail
        ElseIf key = "ConfigTransporter" Then
            ControlName = frmDefTransporter
            enm = EnumForms.frmDefTransporter
            If Tags.Length > 0 Then
                frmDefTransporter.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "EmployeeInfo" Then
            ControlName = frmDefEmployee
            enm = EnumForms.frmDefEmployee
            If Tags.Length > 0 Then
                frmDefEmployee.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmAllowancetype" Then
            ControlName = frmAllownaceType
            'ElseIf key = "TaskNotifications" Then
            '    ControlName = frmNotificationUtility
            'TaskNotifications
        ElseIf key = "frmDeductionType" Then
            ControlName = frmDeductionType

            'ElseIf key = "UtilBackup" Then
            '    ControlName = frmSqlServerBackup
        ElseIf key = "frmGRNStatus" Then
            ControlName = frmGRNStatus

        ElseIf key = "UtilBackupNew" Then
            ControlName = frmdbbackup

        ElseIf key = "UtilRestoreDatabase" Then
            ControlName = frmRestoreBackup

        ElseIf key = "UserControl" Then
            ControlName = frmDefUser
            enm = EnumForms.frmDefUser
        ElseIf key = "ConfigCity" Then
            ControlName = frmDefCity
            enm = EnumForms.frmDefCity
            If Tags.Length > 0 Then
                frmDefCity.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigTerritory" Then
            ControlName = frmDefArea
            enm = EnumForms.frmDefArea
        ElseIf key = "mnuConfigYearSaleTarget" Then
            ControlName = frmYearlySaleTarget
            enm = EnumForms.frmYearlySaleTarget
        ElseIf key = "invloc" Then
            ControlName = FrmLocation

            enm = EnumForms.frmDefArea
            If Tags.Length > 0 Then
                FrmLocation.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ActivityLog" Then
            ControlName = frmActivityLog

        ElseIf key = "UpdateVersion" Then
            ControlName = frmReleaseUpdate

        ElseIf key = "Stock Dispatch" Then
            ControlName = frmStockDispatch
            enm = EnumForms.frmStockDispatch
            If Tags.Length > 0 Then
                frmStockDispatch.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "Stock Receiving" Then
            ControlName = frmStockReceive
            enm = EnumForms.frmStockReceive
            If Tags.Length > 0 Then
                frmStockReceive.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "StoreSummary" Then
            frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.RptStoreIssuanceSummary
            ApplyStyleSheet(frmRptEnhancementNew)
            frmRptEnhancementNew.ShowDialog()
            Exit Sub
        ElseIf key = "StoreDetail" Then
            frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.RptStoreIssuanceDetail
            ApplyStyleSheet(frmRptEnhancementNew)
            frmRptEnhancementNew.ShowDialog()
            Exit Sub
        ElseIf key = "SOStatus" Then
            ControlName = frmSOStatus
            enm = EnumForms.frmSOStatus
        ElseIf key = "frmusergroup" Then
            ControlName = frmUserGroup


        ElseIf key = "frmBudget" Then
            ControlName = frmBudget
            enm = EnumForms.frmBudget
        ElseIf key = "frmCustomerPlanning" Then
            ControlName = frmCustomerPlanning
            enm = EnumForms.frmCustomerPlanning

        ElseIf key = "frmProductionPlanStatus" Then
            ControlName = frmProductionPlanStatus
            enm = EnumForms.frmCustomerPlanning
        ElseIf key = "frmBillAnalysis" Then
            ControlName = frmBillAnalysis
            enm = EnumForms.Sales
        ElseIf key = "UpdateBuiltyNoAndTransportor" Then
            ControlName = frmUpdatebitlyAndTransporter
            enm = EnumForms.frmUpdatebitlyAndTransporter

        ElseIf key = "frmGrdRptStockStatementUnitWise" Then
            ControlName = frmGrdRptStockStatementUnitWise

        ElseIf key = "frmChequeTransfer" Then
            ControlName = frmChequeTransfer
            enm = EnumForms.frmChequeTransfer

        ElseIf key = "POStatus" Then
            ControlName = frmPOStatus
            enm = EnumForms.frmPOStatus

        ElseIf key = "frmSystemConfiguration" Then
            ControlName = frmSystemConfigurationNew
            enm = EnumForms.frmSystemConfiguration
            'Dialog Section

        ElseIf key = "WeightCalculator" Then
            ControlName = frmSalesReturnWeight

        ElseIf key = "CompConnectionInfomation" Then
            ControlName = CompanyAndConnectionInfo
            enm = EnumForms.frmDefUser

        ElseIf key = "frmConfigurationSystemNew" Then
            ControlName = frmSystemConfigurationNew
            enm = EnumForms.frmSystemConfiguration

        ElseIf key = "rtpInventoryLevel" Then
            ControlName = rptInventoryLevelComparison
            enm = EnumForms.rtpInventoryLevel

            'ElseIf key = "frmEmployeeSalaryVoucherNew" Then
            '    ControlName = frmEmployeeSalaryVoucherNew
            '    enm = EnumForms.Non

        ElseIf key = "frmRptStockStatment" Then
            frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.StockStatementByLPO
            ApplyStyleSheet(frmRptEnhancementNew)
            frmRptEnhancementNew.ShowDialog()
            Exit Sub
        ElseIf key = "frmRptStockStatementSize" Then
            frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.StockStatementWithSize
            ApplyStyleSheet(frmRptEnhancementNew)
            frmRptEnhancementNew.ShowDialog()
            Exit Sub
        ElseIf key = "PLNotesDetail" Then
            rptDateRange.ReportName = rptDateRange.ReportList.PLNotesDetail
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptDamageBudget" Then
            rptDateRange.ReportName = rptDateRange.ReportList.DamageBudget
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "frmExpense" Then
            ControlName = frmExpense
            enm = EnumForms.frmExpense
            If Tags.Length > 0 Then
                frmExpense.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "frmDefBatch" Then
            ControlName = frmDefBatch
            enm = EnumForms.frmDefBatch
            If Tags.Length > 0 Then
                frmDefBatch.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "rptCategoryWiseSaleReport" Then
            ControlName = rptCategoryWiseSaleReport
            enm = EnumForms.rptCategoryWiseSaleReport
        ElseIf key = "agingtemplates" Then
            ApplyStyleSheet(frmAgingBalancesTemplate)
            frmAgingBalancesTemplate.ShowDialog()
            Exit Sub
        ElseIf key = "PriceChangeReport" Then
            ControlName = rptPriceChangeReport
            enm = EnumForms.rptPriceChangeReport

        ElseIf key = "frmRptLedgerNew" Then
            ControlName = frmRptLedgerNew
        ElseIf key = "frmGrdRptAgingPayables" Then
            ControlName = frmGrdRptAgingPayables
        ElseIf key = "frmGrdRptAgingReceiveables" Then
            ControlName = frmGrdRptAgingReceiveables
        ElseIf key = "rptStockAccountsReport" Then
            ControlName = rptStockAccountsReport
            enm = EnumForms.rptStockAccountsReport

            'ElseIf key = "RptCustomerSalesAnlysis" Then
            '    ControlName = RptCustomerSalesAnlysis
            '    enm = EnumForms.rptStockAccountsReport

        ElseIf key = "RptGridItemSalesHistory" Then
            ControlName = RptGridItemSalesHistory
            enm = EnumForms.rptStockAccountsReport

        ElseIf key = "frmGrdStockDeliveryChalan" Then
            ControlName = frmGrd_Prod_DC_WiseStock
            enm = EnumForms.Non
        ElseIf key = "frmGrdCostSheetComparisonWithStock" Then
            ControlName = frmGrdCostSheetComparisonWithStock
            enm = EnumForms.Non

        ElseIf key = "frmGrdSalesSummary" Then
            ControlName = frmGrdSalesSummary
            enm = EnumForms.Non

        ElseIf key = "frmGrdPurchaseSummary" Then
            ControlName = frmGrdPurchaseSummary
            enm = EnumForms.Non

        ElseIf key = "frmDefShift" Then
            ControlName = frmDefShift

        ElseIf key = "frmDefShiftGroup" Then
            ControlName = frmDefShiftGroup

        ElseIf key = "frmDefAllocateShiftSchedule" Then
            ControlName = frmDefAllocateShiftSchedule

        ElseIf key = "ChangePassword" Then
            ApplyStyleSheet(ChangePassword)
            ChangePassword.ShowDialog()

        ElseIf key = "frmTerminal" Then
            ApplyStyleSheet(frmTerminal)
            ControlName = frmTerminal
            enm = EnumForms.frmDefUser
            'Reports Section
        ElseIf key = "DailyVoucher" Then

            rptDateRange.ReportName = rptDateRange.ReportList.voucherDetail
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub

        ElseIf key = "WeightReport" Then
            rptDateRange.ReportName = rptDateRange.ReportList.WeightReport
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
            ''19-Mar-2014 TASK:2506 Imran Ali  Add batch quantity and finish goods name in store issue detail report
        ElseIf key = "rptStoreIssuanceDetailBatchWise" Then
            rptDateRange.ReportName = rptDateRange.ReportList.StoreIssuanceDetailBatchWise
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
            'End Task:2506
        ElseIf key = "ItemExpiryDate" Then
            ControlName = frmGrdRptItemExpiryDateDetail

        ElseIf key = "BalanceSheetNotesSummary" Then
            rptDateRange.ReportName = rptDateRange.ReportList.BalanceSheetNotesSummary
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()

        ElseIf key = "ChartOfAccounts" Then
            ShowReport("rptChartofAccounts")
            Exit Sub
        ElseIf key = "ListOfItems" Then
            ShowReport("ListOfItems")
            Exit Sub
        ElseIf key = "ItemWiseSales" Then
            ControlName = rptItemWiseSales
            enm = EnumForms.rptInventoryForm
        ElseIf key = "ItemWiseSalesReturn" Then
            ControlName = rptItemWiseSalesReturn
            enm = EnumForms.rptInventoryForm
        ElseIf key = "frmRptGrdPLCostCenter" Then
            ControlName = frmRptGrdPLCostCenter
            enm = EnumForms.frmVoucher

        ElseIf key = "frmRptGrdStockInOutDetail" Then
            ControlName = frmRptGrdStockInOutDetail
            '' 21-12-2013 ReqID-957   M Ijaz Javed      Bank information entry option
        ElseIf key = "bankbranch" Then
            ControlName = frmBranchNew
            '''''''''''''''''''''''''''''''''
        ElseIf key = "ProductionPlan" Then
            ControlName = frmGrdProductionPlaning
        ElseIf key = "SalesMarketing" Then
            ControlName = frmLeads
        ElseIf key = "rptPL" Then
            ShowReport("rptProftAndLossStatement")
        ElseIf key = "rptSMTarget" Then
            ApplyStyleSheet(rptSalesManTarget)
            rptSalesManTarget.ShowDialog()
            Exit Sub
        ElseIf key = "rptPLSingleDate" Then
            rptDateRange.ReportName = rptDateRange.ReportList.PLSingleDate
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptPLComparison" Then
            Dim rptPLComparison As New rptPLComparison
            rptPLComparison.ReportName = rptPLComparison.ReportList.PLComparison
            ApplyStyleSheet(rptPLComparison)
            rptPLComparison.ShowDialog()
            Exit Sub
        ElseIf key = "rptPLAcDetailComparison" Then
            Dim rptPLComparison As New rptPLComparison
            rptPLComparison.ReportName = rptPLComparison.ReportList.PLComparisonDetailAccount
            ApplyStyleSheet(rptPLComparison)
            rptPLComparison.ShowDialog()
            Exit Sub
        ElseIf key = "rptPLAcHeadComparison" Then
            Dim rptPLComparison As New rptPLComparison
            rptPLComparison.ReportName = rptPLComparison.ReportList.PLComparisonSubSubAccount
            ApplyStyleSheet(rptPLComparison)
            rptPLComparison.ShowDialog()
            Exit Sub
        ElseIf key = "RcvReport" Then
            rptDateRange.ReportName = rptDateRange.ReportList.ReceivingReport
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
            '// Report Criteria section
        ElseIf key = "rptLedger" Then
            rptLedger.Text = "Ledger Report"
            ApplyStyleSheet(rptLedger)
            ControlName = rptLedger
            'rptLedger.ShowDialog()
            'Exit Sub
            'ShowReport("Ledger")  
            If Not rptTrialBalance.DrillDown = False Then
                ''Before against request no. 2363
                'rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(1).TabPage.Tab
                'Tsk:2363 tab's index change
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                'End Tsk:2363
                rptLedger.DateTimePicker1.Value = rptTrialBalance.DateTimePicker1.Value
                rptLedger.DateTimePicker2.Value = rptTrialBalance.DateTimePicker2.Value
                rptLedger.cmbAccount.Value = rptTrialBalance.grdStock.GetRow.Cells(0).Value
                'rptLedger.cmbCostCenter.SelectedIndex = 0
                rptLedger.GetLedger()
            End If
        ElseIf key = "rptImportLedger" Then
            ControlName = frmGrdImportLedger
            enm = EnumForms.Non
        ElseIf key = "rptLCDetail" Then
            ControlName = frmGrdLCDetail
            enm = EnumForms.Non
        ElseIf key = "frmBalanceSheet" Then
            ControlName = frmBalanceSheet
            IsBackgroundChanged = True
            ' enm = EnumForms.Accounts
        ElseIf key = "frmProfitAndLoss" Then
            ControlName = frmProfitAndLoss
            IsBackgroundChanged = True
        ElseIf key = "rptTrial" Then
            ControlName = rptTrialBalance
            enm = EnumForms.rptTrialBalance
        ElseIf key = "frmRptTrialNew" Then
            ControlName = frmRptTrialNew
            enm = EnumForms.rptTrialBalance
        ElseIf key = "PostDatedCheques" Then
            ApplyStyleSheet(frmRptPostDatedCheques)
            frmRptPostDatedCheques.ShowDialog()
            Exit Sub
        ElseIf key = "PostDateChequeSumm" Then
            ControlName = frmGrdRptPostDatedChequesSummary
            'enm = EnumForms.Non
        ElseIf key = "rptPostDatedCheques" Then
            ControlName = frmGrdRptPostDatedCheques

        ElseIf key = "rptSumOfInv" Then
            'ShowReport("SummaryOfInvoices")
            rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfSalesInvoices
            ApplyStyleSheet(rptDateRange)
            rptDateRange.PnlCostTop = True
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptSumOfInvReturn" Then
            rptDateRange.ReportName = rptDateRange.ReportList.SalesReturnSummary
            rptDateRange.IsCustomer = True 'Task:2609 Set Satus 
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
            'Task#18082015 by Ahmad Sharif
        ElseIf key = "rptProduction" Then
            rptDateRange.ReportName = rptDateRange.ReportList.Production
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()

        ElseIf key = "RptWIP" Then
            rptDateRange.ReportName = rptDateRange.ReportList.WIP
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            'End Task#18082015
        ElseIf key = "rptProductionSummary" Then
            rptDateRange.ReportName = rptDateRange.ReportList.ProductionSummary
            ApplyStyleSheet(rptDateRange)
            rptDateRange.PnlCostTop = True
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = ("rptPurchasereturn") Then
            rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfPurchaseReturn
            rptDateRange.IsVendor = True 'Task:2609 Set Satus 
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptArticleBarcode" Then
            ApplyStyleSheet(frmRptArticleBarcode)
            frmRptArticleBarcode.ShowDialog()
            Exit Sub
        ElseIf key = ("SumOfPurInv") Then
            rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfPurchaseInvoices
            rptDateRange.IsVendor = True 'Task:2609 Set Satus 
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = ("rptCashFlow") Then
            rptDateRange.ReportName = rptDateRange.ReportList.CashFlowStatment
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = ("rptCashFlowStander") Then
            rptDateRange.ReportName = rptDateRange.ReportList.CashFlowStatmentStander
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = ("rptExpense") Then
            Me.Cursor = Cursors.WaitCursor
            rptDateRange.ReportName = rptDateRange.ReportList.rptExpenses
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Me.Cursor = Cursors.Default
            Exit Sub
        ElseIf key = "rptDiscNetRates" Then
            rptDateRange.ReportName = rptDateRange.ReportList.rptDiscountNetRate
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "BalanceSheet" Then
            rptDateRange.ReportName = rptDateRange.ReportList.rptBSFomated
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "DailySalarySheet" Then
            rptDateRange.ReportName = rptDateRange.ReportList.DailySalarySheet
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "EmpSalarySheet" Then
            ControlName = frmGrdRptEmployeeSalarySheet
        ElseIf key = "DailySalarySheetSummary" Then
            rptDateRange.ReportName = rptDateRange.ReportList.DailySalarySheetSummary
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptDeliveryChalanDetail" Then
            rptDateRange.ReportName = rptDateRange.ReportList.DeliveryChalanDetail
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptDeliveryChalanSummary" Then
            rptDateRange.ReportName = rptDateRange.ReportList.DeliveryChalanSummary
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptDSRSummary" Then
            ApplyStyleSheet(frmRptDSRSummary)
            frmRptDSRSummary.ShowDialog()
            Exit Sub
        ElseIf key = "rptDSRStatement" Then
            ApplyStyleSheet(frmRptDSRStatement)
            frmRptDSRStatement.ShowDialog()
            Exit Sub
        ElseIf key = "areawiseproductsales" Then
            ShowReport("rptAreaProdSale")
        ElseIf key = "rptPayables" Then
            Cursor = Cursors.WaitCursor
            AddRptParam("@1stAging", 60)
            AddRptParam("@1stAgingName", "30_60")
            AddRptParam("@2ndAging", 90)
            AddRptParam("@2ndAgingName", "60_90")
            AddRptParam("@3rdAging", 90)
            AddRptParam("@3rdAgingName", "90+")
            ShowReport("AgeingPayable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)
            Cursor = Cursors.Default
        ElseIf key = "rptReceiveables" Then
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@1stAging", 60)
            AddRptParam("@1stAgingName", "30_60")
            AddRptParam("@2ndAging", 90)
            AddRptParam("@2ndAgingName", "60_90")
            AddRptParam("@3rdAging", 90)
            AddRptParam("@3rdAgingName", "90+")
            ShowReport("AgeingReceivable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)
            Cursor = Cursors.Default
        ElseIf key = "grdrptStock" Then
            ControlName = rptStockForm
            enm = EnumForms.rptStockForm
        ElseIf key = "rptgrdInvForm" Then
            ControlName = rptInventoryForm
            enm = EnumForms.rptInventoryForm
        ElseIf key = "frmStockwithCriteria" Then
            'ControlName = rptStockReportWithCritera
            'enm = EnumForms.rptStockReportWithCritera
            ControlName = rptStockByLocation
            enm = EnumForms.rptInventoryForm
        ElseIf key = "frmCompanyContacts" Then
            ControlName = frmCompanyContacts
        ElseIf key = "areawisesalereport" Then
            ShowReport("rptsales")
        ElseIf key = "SetInventoryLevel" Then
            ControlName = frmInventoryLevel
            enm = EnumForms.frmInventoryLevel
        ElseIf key = "frmRptCustomerSalesAnlysis" Then
            '  ControlName = frmRptCustomerSalesAnlysis
            ' enm = EnumForms.frmInventoryLevel
            'Reports grouping section
        ElseIf key = "AccountReports" Then
            ControlName = AccountsReports
            enm = EnumForms.frmVoucher

        ElseIf key = "CustomerReports" Then
            ControlName = Customer
            enm = EnumForms.frmVoucher

        ElseIf key = "Vendor" Then
            ControlName = Vendors
            enm = EnumForms.frmVoucher
        ElseIf key = "frmGrdRptChart" Then
            'ControlName = frmGrdRptCharts
            'enm = EnumForms.Non
        ElseIf key = "EmpReport" Then
            ControlName = Employee
            enm = EnumForms.frmVoucher
        ElseIf key = "Inventory" Then
            ControlName = Stock
            enm = EnumForms.frmVoucher
        ElseIf key = "purchase" Then
            ControlName = Purchase
            enm = EnumForms.frmVoucher
        ElseIf key = "sales" Then
            ControlName = Sales
            enm = EnumForms.frmVoucher
        ElseIf key = "LicenseActivation" Then
            ControlName = frmActiveLicense
        ElseIf key = "RptGrdTopCustomers" Then
            ControlName = RptGrdTopCustomers
            enm = EnumForms.frmVoucher
        ElseIf key = "ItemWiseSalesSummary" Then
            frmRptCustomersSales.ReportName = frmRptCustomersSales.enmReportList.RptCustomerItemSalesSummary
            ApplyStyleSheet(frmRptCustomersSales)
            frmRptCustomersSales.ShowDialog()
            Exit Sub
        ElseIf key = "ItemWiseSalesDetail" Then
            frmRptCustomersSales.ReportName = frmRptCustomersSales.enmReportList.RptCustomerITemSalesDetail
            ApplyStyleSheet(frmRptCustomersSales)
            frmRptCustomersSales.ShowDialog()
            Exit Sub
        ElseIf key = "frmRptGrdInwardgatepass" Then
            ApplyStyleSheet(frmRptGrdInwardgatepass)
            ControlName = frmRptGrdInwardgatepass
        ElseIf key = "ProjectWiseStockLedger" Then
            ControlName = frmGrdRptLocationWiseStockLedger
        ElseIf key = "rptProjectWiseLedger" Then
            ControlName = frmGrdRptProjectWiseStockLedger
        ElseIf key = "rptStorageBilling" Then
            ControlName = frmRptRental
            enm = EnumForms.Non
        ElseIf key = "frmHome" Then
            ApplyStyleSheet(frmHome)
            ControlName = frmHome
        ElseIf key = "ItemWiseSalesHistory" Then
            ApplyStyleSheet(RptGridItemSalesHistory)
            ControlName = RptGridItemSalesHistory
        ElseIf key = "frmrptGrdProducedItems" Then
            ApplyStyleSheet(frmrptGrdProducedItems)
            ControlName = frmrptGrdProducedItems
        ElseIf key = "frmRptGrdStockStatement" Then
            ApplyStyleSheet(frmRptGrdStockStatement)
            ControlName = frmRptGrdStockStatement
        ElseIf key = "frmAgreement" Then
            ControlName = frmAgreement
            enm = EnumForms.Non
        ElseIf key = "StoreIssuanceSummary" Then
            frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.RptStoreIssuanceSummary
            ApplyStyleSheet(frmRptEnhancementNew)
            frmRptEnhancementNew.ShowDialog()
            Exit Sub
        ElseIf key = "StoreIssuanceDetailRpt" Then
            frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.RptStoreIssuanceDetail
            ApplyStyleSheet(frmRptEnhancementNew)
            frmRptEnhancementNew.ShowDialog()
        ElseIf key = "PLBSNotesSetting" Then
            ControlName = frmBSandPLNotesDetail
        ElseIf key = "frmgrdrptDailyUpdate" Then
            ControlName = frmgrdrptDailyUpdate

        ElseIf key = "EmpAttendanceRpt" Then
            ApplyStyleSheet(RptDateRangeEmployees)
            RptDateRangeEmployees.ReportName = RptDateRangeEmployees.ReportList.ReportAttendance
            RptDateRangeEmployees.ShowDialog()
            Exit Sub
        ElseIf key = "frmRptCashDetail" Then
            ApplyStyleSheet(frmRptCashDetail)
            frmRptCashDetail.ShowDialog()
            Exit Sub
        ElseIf key = "EmpAttendanceSummaryRpt" Then
            ApplyStyleSheet(RptDateRangeEmployees)
            RptDateRangeEmployees.ReportName = RptDateRangeEmployees.ReportList.ReportAttendanceSummary
            RptDateRangeEmployees.ShowDialog()
            Me.Cursor = Cursors.Default
            Exit Sub
        ElseIf key = "rptNetSales" Then
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ReportName = rptDateRange.ReportList.NetSalesReport
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "frmRptGrdMinMaxPriceSalesDetail" Then
            ControlName = frmRptGrdMinMaxPriceSalesDetail
        ElseIf key = "SwitchUser" Then
            'LoginForm4.Text = "Switch User"
            'LoginForm4.ComboBox1.Enabled = False
            'LoginForm4.UsernameTextBox.ReadOnly = False
            'LoginForm4.ShowDialog()
            Exit Sub
        ElseIf key = "EmpPromotion" Then
            ControlName = frmEmployeePromotion
            enm = EnumForms.Non
            '''''''''''''''''''' Assets Management Menu ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        ElseIf key = "frmAsset" Then
            ControlName = frmAsset
            enm = EnumForms.Non
        ElseIf key = "frmAssetCategory" Then
            ControlName = frmAssetCategory
            enm = EnumForms.Non
        ElseIf key = "AssetType" Then
            ControlName = AssetType
            enm = EnumForms.Non
        ElseIf key = "AssetLocation" Then
            ControlName = frmAssetLocation
            enm = EnumForms.Non
        ElseIf key = "AssetCondition" Then
            ControlName = AssetCondition
            enm = EnumForms.Non
        ElseIf key = "AssetStatus" Then
            ControlName = frmAssetStatus
            enm = EnumForms.Non
        ElseIf key = "AssetsDetail" Then
            ControlName = frmGrdRptAssetsDetail
            enm = EnumForms.Non
        ElseIf key = "Remainder" Then
            ControlName = frmreminder
            ControlName.ShowDialog()
            Exit Sub
        ElseIf key = "division" Then
            ControlName = frmDefDivision
            enm = EnumForms.Non
        ElseIf key = "payrolldivision" Then
            ControlName = frmDefPayRollDivision
            enm = EnumForms.Non

            'Task:2594 Added Report Employee Salaries Detail
        ElseIf key = "rptEmpSalariesDetail" Then
            ControlName = frmGrdRptGenerelEmployeeSalary
            'End Task:2594

            'Task: 2592 Assign key to open "Employee Over Time" Form
        ElseIf key = "frmEmpOverTime" Then
            ControlName = frmEmpOverTimeSchedule
            'End Task: 2592
        ElseIf key = "LeaveEncashment" Then
            ApplyStyleSheet(frmDefLeaveEncashment)
            frmDefLeaveEncashment.ShowDialog()
            Exit Sub
            'Task no 2616 cODE fOR nEWLY Added Field
        ElseIf key = "ConsolidateItemSales" Then
            ApplyStyleSheet(frmGrdRptConsolidateItemSalesCustomerWise)
            ControlName = frmGrdRptConsolidateItemSalesCustomerWise
            'Task End 2616
            'TAASK nO 2624 Adding The Key LateTimeSlot
            'Task:M41 Added Cost Sheet Menu
        ElseIf key = "defineCostSheet" Then
            ControlName = frmCostSheet
            'End Task:M41
        ElseIf key = "LateTimeSlot" Then
            ApplyStyleSheet(frmlatetimeSlot)
            frmlatetimeSlot.ShowDialog()
            Exit Sub
            'End Task 2624
        ElseIf key = "TaxRate" Then
            ApplyStyleSheet(frmDefServices)
            frmDefServices.ShowDialog()
            Exit Sub
            'Task:2725 Add new report product summary.
        ElseIf key = "frmGrdRptProductCustomerWiseReport" Then
            ControlName = frmGrdRptProductCustomerWiseReport
            'End Task:2725
        ElseIf key = "frmGrdRptSalesSummaries" Then
            ControlName = frmGrdRptSalesSummaries
        ElseIf key = "frmGrdRptProductDateWiseReport" Then
            ControlName = frmGrdRptProductDateWiseReport
        ElseIf key = "frmDSRStatementNew" Then
            ApplyStyleSheet(frmDSRStatementNew)
            frmDSRStatementNew.ShowDialog()
            Exit Sub
        ElseIf key = "frmEmployeeArticleCostRate" Then
            ControlName = frmEmployeeArticleCostRate
        ElseIf key = "frmUtilityApplyAverageRate" Then
            ControlName = frmUtilityApplyAverageRate

            'ElseIf key = "frmClose" Then
            '    If Me.Timer1.Enabled = True Then
            '        Me.Timer1.Enabled = False
            '        Me.Timer1.Stop()
            '    Else
            '        Me.Timer1.Enabled = True
            '        Me.Timer1.Start()
            '    End If
            '    If Me.SplitContainer.Panel2.Controls.Count > 0 Then
            '        ControlName = Me.SplitContainer.Panel2.Controls(0)
            '        ControlName.Close()
            '    End If
            '    Exit Sub

            'ElseIf key = "EnhancedReports" Then
            '    Shell(str_ApplicationStartUpPath & "\SSC Reports\SSC Reports.exe")
            '    Exit Sub
        ElseIf key = "TodayTopic" Then
            ApplyStyleSheet(frmTodayTopic)
            frmTodayTopic.ShowDialog()
            Exit Sub
        ElseIf key = "frmPurchaseDemand" Then
            ControlName = frmPurchaseDemand
        ElseIf key = "frmGrdRptNonIntractCustomers" Then
            ControlName = FrmGridRptNonIntractCustomers
        ElseIf key = "frmSiteRegistration" Then
            ControlName = frmSiteRegistration
            ''26-June-2014 TASK2704 Imran Ali Add new functionality cash request in erp.
        ElseIf key = "frmCashRequest" Then
            ControlName = frmCashrequest
            'End Task:2704
        End If
        '        End With

        '// It will check security rights
        'If GetConfigValue("EnableSecurity") = "True" Then
        '    'If Not CheckSecurityRight(ControlName) = True Then Exit Sub
        'End If
        If key = "UpdateVersion" Then
            'ControlName.TopLevel = False
            'ControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            'ControlName.Dock = DockStyle.Fill
            'Me.SplitContainer.Panel2.Controls.Add(ControlName)
            ControlName.MdiParent = Me
            ApplyStyleSheet(ControlName)
            If IsBackgroundChanged = True Then
                ControlName.BackColor = Color.White
            End If
            IsBackgroundChanged = False
            ControlName.Show()
            ControlName.WindowState = FormWindowState.Maximized
            ControlName.BringToFront()
            'Me.LoadLayouts()
            Exit Sub
        End If

        'If Me.BackgroundWorker8.IsBusy Then
        '    Application.DoEvents()
        'End If


        ' ''R-912 Company Default Login ...
        'If flgCompanyRights = True Then
        '    If Me.BackgroundWorker6.IsBusy Then
        '        Application.DoEvents()
        '    End If
        '    If MyCompanyId = 0 Then
        '        If CompanyRightsList.Count > 0 Then
        '            MyCompanyId = CompanyRightsList.Item(0).CompanyId
        '            Dim MyComp As New SBModel.CompanyInfo
        '            MyComp = CompanyList.Find(AddressOf GetMyCompany)
        '            Me.UltraStatusBar2.Panels(3).Text = MyComp.CompanyName
        '        End If
        '    End If
        'End If
        'End R-912

        RestrictForm = ControlName.Name
        Dim obj As Object = GetFormAccessByArray.Find(AddressOf FindRestrictForm)
        If obj IsNot Nothing AndAlso obj.ToString.Length > 0 Then
            ControlName = frmdisplay
            RestrictForm = String.Empty
        End If


        If NewSecurityRights = True Then
            'GetFormRights(ControlName.Name)
            Rights = GroupRights.FindAll(AddressOf ReturnRights)
            If Not Rights.Count = 0 Or LoginGroup = "Administrator" Then
                ''Req-918 campare rights and login group
                If Rights.Count > 0 AndAlso LoginGroup <> "Administrator" Then
                    Dim VwRights As SBModel.GroupRights = Rights.Find(AddressOf chkViewFormRights) 'Filter View Rights
                    If VwRights Is Nothing Then
                        Exit Sub
                    End If
                End If
                'ControlName.TopLevel = False
                'ControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                'ControlName.Dock = DockStyle.Fill
                'Me.SplitContainer.Panel2.Controls.Add(ControlName)
                ControlName.MdiParent = Me
                ApplyStyleSheet(ControlName)
                If IsBackgroundChanged = True Then
                    ControlName.BackColor = Color.White
                End If
                IsBackgroundChanged = False
                ControlName.Show()
                ControlName.WindowState = FormWindowState.Maximized
                ControlName.BringToFront()
                'Me.LoadLayouts()
                'End If
                '  Next
            Else
                If LoginUserId = 0 Then
                    ControlName = frmUserGroup
                    ApplyStyleSheet(ControlName)
                    If IsBackgroundChanged = True Then
                        ControlName.BackColor = Color.White
                    End If
                    IsBackgroundChanged = False
                    ControlName.Show()
                    ControlName.WindowState = FormWindowState.Maximized
                    ControlName.BringToFront()
                    'Me.LoadLayouts()
                    '   ToggleFoldersVisible()
                End If
            End If
        Else
            'ControlName.TopLevel = False
            'ControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            'ControlName.Dock = DockStyle.Fill
            'Me.SplitContainer.Panel2.Controls.Add(ControlName)
            ControlName.MdiParent = Me
            Dim dtRights As New DataTable
            Dim rightlist As New Specialized.NameValueCollection
            If IsEnhancedSecurity = True Then
                rightlist = GetFormSecurityControls(ControlName.Name)
            Else
                dtRights = GetFormRights(enm)
            End If
            If enm <> EnumForms.Non Then
                If LoginUserId = 0 Then
                    ControlName = frmDefUser
                    ApplyStyleSheet(ControlName)
                    If IsBackgroundChanged = True Then
                        ControlName.BackColor = Color.White
                    End If
                    IsBackgroundChanged = False
                    ControlName.Show()
                    ControlName.WindowState = FormWindowState.Maximized
                    ControlName.BringToFront()
                    'Me.LoadLayouts()
                    ' ToggleFoldersVisible()
                ElseIf dtRights.Rows.Count > 0 AndAlso IsEnhancedSecurity = False Then
                    If dtRights.Rows(0).Item("View_Rights") = True Then
                        ApplyStyleSheet(ControlName)
                        If IsBackgroundChanged = True Then
                            ControlName.BackColor = Color.White
                        End If
                        IsBackgroundChanged = False
                        'If Me.SplitContainer.Panel2.Controls.Count > 0 AndAlso Not ControlName.Name = Me.SplitContainer.Panel2.Controls(0).Name Then
                        Try

                            ControlName.Show()
                            ControlName.WindowState = FormWindowState.Maximized
                            ControlName.BringToFront()
                            'Me.LoadLayouts()
                        Catch ex As Exception
                        End Try
                    ElseIf LoginGroup = "Administrator" Then
                        ApplyStyleSheet(ControlName)
                        If IsBackgroundChanged = True Then
                            ControlName.BackColor = Color.White
                        End If
                        IsBackgroundChanged = False
                        'If Me.SplitContainer.Panel2.Controls.Count > 0 AndAlso Not ControlName.Name = Me.SplitContainer.Panel2.Controls(0).Name Then
                        Try
                            ControlName.Show()
                            ControlName.WindowState = FormWindowState.Maximized
                            ControlName.BringToFront()
                            'Me.LoadLayouts()
                        Catch ex As Exception
                        End Try
                    End If
                ElseIf IsEnhancedSecurity = True AndAlso rightlist.Count > 0 Then
                    If Not rightlist.Item("View") Is Nothing Then
                        ApplyStyleSheet(ControlName)
                        If IsBackgroundChanged = True Then
                            ControlName.BackColor = Color.White
                        End If
                        IsBackgroundChanged = False
                        'If Me.SplitContainer.Panel2.Controls.Count > 0 AndAlso Not ControlName.Name = Me.SplitContainer.Panel2.Controls(0).Name Then
                        Try
                            ControlName.Show()
                            ControlName.WindowState = FormWindowState.Maximized
                            ControlName.BringToFront()
                            'Me.LoadLayouts()
                        Catch ex As Exception
                        End Try
                    End If
                End If
            Else
                ApplyStyleSheet(ControlName)
                If IsBackgroundChanged = True Then
                    ControlName.BackColor = Color.White
                End If
                IsBackgroundChanged = False
                ControlName.Show()
                ControlName.WindowState = FormWindowState.Maximized
                ControlName.BringToFront()
                'Me.LoadLayouts()
            End If
        End If



        If key = "frmusergroup" AndAlso GroupType = EnumGroupType.Administrator.ToString Or key = "frmHome" Then
            'If Not Rights Is Nothing Then
            '    For j As Integer = 0 To Rights.Count - 1
            '        If Rights.Item(j).FormControlName = "FirstTabHome" Then
            '            frmHome.UltraTabControl1.SelectedTab = frmHome.UltraTabControl1.Tabs(0).TabPage.Tab
            '        Else
            '            frmHome.UltraTabControl1.SelectedTab = frmHome.UltraTabControl1.Tabs(1).TabPage.Tab
            '        End If
            '    Next
            'Else
            '    frmHome.UltraTabControl1.SelectedTab = frmHome.UltraTabControl1.Tabs(1).TabPage.Tab
            'End If
            'ControlName.TopLevel = False
            'ControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            'ControlName.Dock = DockStyle.Fill
            'Me.SplitContainer.Panel2.Controls.Add(ControlName)
            ControlName.MdiParent = Me
            ApplyStyleSheet(ControlName)
            If IsBackgroundChanged = True Then
                ControlName.BackColor = Color.White
            End If
            IsBackgroundChanged = False
            ControlName.Show()
            ControlName.WindowState = FormWindowState.Maximized
            ControlName.BringToFront()
            'Me.LoadLayouts()
        End If
        If Not Me.LastControlName.Name = Me.ControlName.Name Then
            LastItem = key.ToString
        End If
        'Tags = String.Empty
        'Me.AddHistoryItem(ControlName.Text, key)
    End Sub
    Public Function chkViewFormRights(ByVal Rights As SBModel.GroupRights) As Boolean
        Try
            If Rights.FormControlName = "View" Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function RestrictSheet(ByVal key As String) As Boolean
        Try

            RestrictSheetAccess = key
            Dim obj As Object = GetFormAccessByArray.Find(AddressOf FindRestrictForm)
            If obj IsNot Nothing AndAlso obj.ToString.Length > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ReturnRights(ByVal Rights As SBModel.GroupRights) As Boolean
        Try
            If Rights.FormName = ControlName.Name Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getRightsFroms(ByVal Rights As SBModel.GroupRights) As Boolean
        Try
            If Rights.FormName = FormTag AndAlso Rights.FormControlName = "View" Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetFormAccessByArray() As List(Of String)
        Try

            Dim strFormList As New List(Of String)
            If Val(SoftwareVersion) = 0 Or Val(SoftwareVersion) = 1 Then 'Basic Edition
                'strFormList.Add("FrmLocation")
                strFormList.Add("frmProductionStore")
                strFormList.Add("frmDefEmployee")
                strFormList.Add("frmAttendanceEmployees")
                strFormList.Add("frmEmployeeSalaryVoucher")
                strFormList.Add("frmSiteRegistration")
                strFormList.Add("frmCostCenter")
                strFormList.Add("FrmEmailconfig")
                strFormList.Add("frmTasks")
                strFormList.Add("frmCustomerPlanning")
                strFormList.Add("frmProductionLevel")
                strFormList.Add("frmProductionPlanStatus")
                strFormList.Add("frmCostSheet")
                strFormList.Add("frmSMSConfiguration")
                strFormList.Add("rptTaskAssign")

                strFormList.Add("FrmLocation")

                'Task#119062015 CRM section
                strFormList.Add("frmProjectPortFolio")
                strFormList.Add("frmProjectVisit")
                strFormList.Add("frmProjQuotion")
                strFormList.Add("frmProjectVisitType")
                strFormList.Add("frmRptProjectHistory")
                strFormList.Add("frmTasks")
                strFormList.Add("frmGrdRptQuotationStatus")
                strFormList.Add("frmProjectVisitType")
                strFormList.Add("RptDateRangeEmployees")
                strFormList.Add("frmRptTaskDetail")
                strFormList.Add("SalesMarketing")
                'End Task#119062015 CRM Section

                'Task#119062015 Asset Management section
                strFormList.Add("frmAsset")
                strFormList.Add("frmAssetCategory")
                strFormList.Add("AssetType")
                strFormList.Add("frmAssetLocation")
                strFormList.Add("AssetCondition")
                strFormList.Add("frmAssetStatus")
                strFormList.Add("frmGrdRptAssetsDetail")
                'End Task#119062015 Asset Management section

                'Task#119062015 Site Management section
                strFormList.Add("frmRptCMFADetail")
                'strFormList.Add("frmCMFAAll")
                'strFormList.Add("frmCMFAAll")
                strFormList.Add("frmCMFAAll")
                strFormList.Add("frmGrdRptCMFAllRecords")
                strFormList.Add("frmGrdRptCMFASummary")
                strFormList.Add("frmGrdRptCMFAOfSummaries")
                strFormList.Add("frmRptCMFADetail")
                'End Task#119062015 Site Management section

                'Task#119062015 Production section
                strFormList.Add("frmGrdProductionAnalaysis")
                strFormList.Add("frmMRPlan")
                strFormList.Add("frmStoreIssuence")
                strFormList.Add("frmReturnStoreIssuence")
                strFormList.Add("frmProductionLevel")
                strFormList.Add("frmProductionStore")
                strFormList.Add("frmStockDispatch")
                strFormList.Add("frmStockReceive")
                strFormList.Add("frmCostSheet")
                strFormList.Add("frmProductionPlanStatus")
                strFormList.Add("frmGrdRptProductionLevel")
                strFormList.Add("frmrptGrdProducedItems")
                strFormList.Add("rptDateRange")
                strFormList.Add("frmGrdRptProductionComparison")
                strFormList.Add("frmRptEnhancementNew")
                strFormList.Add("frmRptEnhancementNew")
                strFormList.Add("frmStockStatmentBySize")
                strFormList.Add("frmRptGrdStockStatement")
                strFormList.Add("RptGridItemSalesHistory")
                strFormList.Add("frmRptGrdStockInOutDetail")
                strFormList.Add("frmGrdRptLocationWiseStockLedger")
                strFormList.Add("frmGrdRptProjectWiseStockLedger")
                'End Task#119062015 Production section

                ''Task#20150516 Blcoking block call center,Email COnfigration,Event Configration in basic version Ali Ansari
                strFormList.Add("frmStatus")
                strFormList.Add("frmTypes")
                strFormList.Add("frmLeads")
                strFormList.Add("frmRptTaskDetail")
                'strFormList.Add("frmSMSConfiguration")
                ''Task#20150516 Blcoking block call center,Email COnfigration,Event Configration in basic version Ali Ansari
            ElseIf Val(SoftwareVersion) = 2 Then 'Small Business Edition
                strFormList.Add("frmCostCenter")
                strFormList.Add("FrmEmailconfig")
                strFormList.Add("frmLeads")
                strFormList.Add("frmTasks")

            ElseIf Val(SoftwareVersion) = 3 Then 'Corporate Edition
                strFormList.Add("frmLeads")
                strFormList.Add("frmTasks")

                'Task#119062015 CRM section
                strFormList.Add("frmProjectPortFolio")
                strFormList.Add("frmProjectVisit")
                strFormList.Add("frmProjQuotion")
                strFormList.Add("frmProjectVisitType")
                strFormList.Add("frmRptProjectHistory")
                strFormList.Add("frmTasks")
                strFormList.Add("frmGrdRptQuotationStatus")
                strFormList.Add("frmProjectVisitType")
                strFormList.Add("RptDateRangeEmployees")
                strFormList.Add("frmRptTaskDetail")
                strFormList.Add("SalesMarketing")
                'End Task#119062015 CRM Section

                'Task#119062015 Asset Management section
                strFormList.Add("frmAsset")
                strFormList.Add("frmAssetCategory")
                strFormList.Add("AssetType")
                strFormList.Add("frmAssetLocation")
                strFormList.Add("AssetCondition")
                strFormList.Add("frmAssetStatus")
                strFormList.Add("frmGrdRptAssetsDetail")
                'End Task#119062015 Asset Management section

            ElseIf Val(SoftwareVersion) = 4 Then 'Enterprise Edition

                'Task#119062015 CRM section
                strFormList.Add("frmProjectPortFolio")
                strFormList.Add("frmProjectVisit")
                strFormList.Add("frmProjQuotion")
                strFormList.Add("frmProjectVisitType")
                strFormList.Add("frmRptProjectHistory")
                strFormList.Add("frmTasks")
                strFormList.Add("frmGrdRptQuotationStatus")
                strFormList.Add("frmProjectVisitType")
                strFormList.Add("RptDateRangeEmployees")
                strFormList.Add("frmRptTaskDetail")
                strFormList.Add("SalesMarketing")
                'End Task#119062015 CRM Section

                'Task#119062015 Asset Management section
                strFormList.Add("frmAsset")
                strFormList.Add("frmAssetCategory")
                strFormList.Add("AssetType")
                strFormList.Add("frmAssetLocation")
                strFormList.Add("AssetCondition")
                strFormList.Add("frmAssetStatus")
                strFormList.Add("frmGrdRptAssetsDetail")
                'End Task#119062015 Asset Management section

            ElseIf Val(SoftwareVersion) = 5 Then 'Enterprise Edition Plus

            End If
            Return strFormList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function FindRestrictForm(ByVal FormName As String) As Boolean
        Try
            If RestrictForm.Trim.ToUpper = FormName.Trim.ToUpper Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Altered against Task#20150516 blocking sheet access  Ali Ansari
    'Get List of restrict sheets
    Private Function FindRestrictSheet(ByVal Key As String) As Boolean
        Try
            If RestrictSheetAccess.Trim.ToUpper = Key.Trim.ToUpper Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmCustomizeEnvirement_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.Text = CompanyTitle
            dbVersion = getConfigValueByType("Version").ToString
            Me.ToolStripStatusLabel.Text = "User Name: " & LoginUserName.ToUpper & " | " & " Version : " & dbVersion.ToUpper
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
