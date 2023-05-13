
Imports SBDal
Imports SBModel
Imports Infragistics.Win.UltraWinGrid
Imports System.Data.SqlClient

Public Class frmModProperty
    Public LastControlName As New Form
    Public NextControlName As New Form
    Public Shared FormName As String
    Public Shared fname = New Form
    Dim validFormListToShow As List(Of String) = New List(Of String)
    Dim BackupCalled As Boolean = False
    Public DownloadInProgress As Boolean = False
    Public Tags As String = String.Empty
    Dim arHistory As New ArrayList
    Dim enm As EnumForms = EnumForms.Non
    Dim RestrictForm As String = String.Empty
    Dim RestrictSheetAccess As String = String.Empty
    Dim IsBackgroundChanged As Boolean = False
    Dim strControlName As String
    Public dbVersion As String = String.Empty
    Dim NewSecurityRights As Boolean = True
    Dim flgCompanyRights As Boolean = False
    Public blnListSeachStartWith As Boolean = False
    Public blnListSeachEndWith As Boolean = False
    Public blnListSeachContains As Boolean = False
    'Public Shared FormNameList As List(Of String) = New List(Of String)
    'Dim displayedItems As Integer = 0

    'Added by Mohsin Rasheed on 22 Feb 2018
    'Function for loading the form in pnlMain in parent window

    Function ShowForm(ByVal MyForm As String) As Boolean
        Try
            enm = EnumForms.Non

        Catch ex As Exception
            Throw ex
        End Try
        Dim FormName = New Form
        FormName = MyForm
        'If MyForm.Length > 0 Then
        '    Me.LastControlName = FormName
        '    Me.BackToolStripButton.Enabled = True
        'Else
        '    Me.BackToolStripButton.Enabled = False
        'End If
        'QC Module Here'
        If LicenseExpiryType = "Monthly" Or gblnTrialVersion Then

            'Added by syed Irfan Ahmad on 19 Feb 2018, Task 2411
            If LicenseStatus = "Blocked" Then
                Dim gm As New AgriusMessage
                gm.Message = "License status of Agrius is Blocked please contact Sirius Support for more details."
                gm.ErrorCode = "GEC-LIC-0x007-1122"
                ModGlobel.AgriusMessageLogger.Log(gm)

                msg_Error("License status of Agrius is Blocked please contact Sirius Support for more details." & vbCrLf & vbCrLf & "Error Code: GEC-LIC-0x007-1122", 120)
                ''Task3459: Aashir: replaced because of "frmaboutus" error after logging in.
                MyForm = "frmaboutus"
                FormName = frmaboutus
            ElseIf LicenseStatus = "Expired" Then
                msg_Information("License of Agrius is expired please contact Sirius for more details", 120)
            End If

        End If

        If MyForm = "frmResult" Then
            FormName = frmResult

        ElseIf Myform = "frmObservationSample" Then
            FormName = frmObservationSample

        ElseIf Myform = "frmLabTestRequest" Then
            FormName = frmLabTestRequest

        ElseIf MyForm = "frmDashboard" Then
            FormName = frmDashboard

            ''TFS3078
        ElseIf MyForm = "frmServicesInwardGatePass" Then
            FormName = frmServicesInwardGatePass

            ' Accounts Module here ...
        ElseIf MyForm = "frmGrdRptAgingReceiveables" Then
            Dim IsDisplayedOldReceivable As Boolean = False
            If Not getConfigValueByType("IsDisplayedOldReceivable") = "Error" Then
                IsDisplayedOldReceivable = CBool(getConfigValueByType("IsDisplayedOldReceivable"))
            End If
            If IsDisplayedOldReceivable = False Then
                FormName = frmGrdRptAgingReceiveables
                enm = EnumForms.frmGrdRptAgingReceiveables
            Else
                FormName = frmGrdRptAgingReceiveablesOld
                enm = EnumForms.frmGrdRptAgingReceiveables
            End If
        ElseIf MyForm = "frmGrdRptAgingReceiveablesOld" Then
            FormName = frmGrdRptAgingReceiveablesOld
            enm = EnumForms.frmGrdRptAgingReceiveables
        ElseIf MyForm = "frmGrdRptAgingReceiveablesNew" Then
            FormName = frmGrdRptAgingReceiveables
            enm = EnumForms.frmGrdRptAgingReceiveables
        ElseIf MyForm = "frmLogicalBifurcationList" Then
            FormName = frmLogicalBifurcationList
        ElseIf MyForm = "frmVoucherNew" Or MyForm = "frmVoucher" Then
            FormName = frmVoucherNew
            If Tags.Length > 0 Then
                frmVoucherNew.Get_All(Tags)
            End If

        ElseIf MyForm = "frmRptInvoiceAging" Then
            FormName = frmRptInvoiceAging

        ElseIf MyForm = "frmCustomAgingReceivables" Then
            FormName = frmCustomAgingReceivables

        ElseIf MyForm = "frmConfigMain" Then
            FormName = frmConfigMain

        ElseIf MyForm = "frmVoucherPostUnpost" Then
            FormName = frmVoucherPostUnpost

        ElseIf MyForm = "frmVoucherPost" Then
            FormName = frmVoucherPost

            'ElseIf MyForm = "frmReconciliation" Then
            '    FormName = frmReconciliation

            'ElseIf MyForm = "frmBankReconciliation" Then
            '    FormName = frmBankReconciliation

        ElseIf MyForm = "frmRptBankReconciliation" Then
            FormName = frmRptBankReconciliation

        ElseIf MyForm = "frmReconciliation" Or MyForm = "frmBankReconciliation" Then
            Dim reconcilation As String = getConfigValueByType("NewReconcilationForm")
            'If getConfigValueByType("NewReconcilationForm") = "Error" Then
            '    reconcilation = False
            'Else
            '    reconcilation = True
            'End If
            If reconcilation = True Then
                FormName = frmReconciliation
            Else
                FormName = frmBankReconciliation
            End If
        ElseIf MyForm = "frmMobileExpense" Then
            FormName = frmMobileExpense

        ElseIf MyForm = "frmAdvanceRequest" Then
            FormName = frmAdvanceRequest
            If Tags.Length > 0 Then
                frmAdvanceRequest.Get_All(Tags)
            End If

        ElseIf MyForm = "frmAdvanceType" Then
            FormName = frmAdvanceType


        ElseIf MyForm = "frmLetterCredit" Then
            FormName = frmLetterCredit


        ElseIf MyForm = "frmDefPartners" Then
            FormName = frmDefPartners


        ElseIf MyForm = "frmBSandPLNotesDetail" Then
            FormName = frmBSandPLNotesDetail


        ElseIf MyForm = "frmBranchNew" Then
            FormName = frmBranchNew


        ElseIf MyForm = "frmBudget" Then
            FormName = frmBudget


        ElseIf MyForm = "frmBSandPLReports" Then
            FormName = frmBSandPLReports

        ElseIf MyForm = "rptChartofAccounts" Then
            ShowReport("rptChartofAccounts")
            Exit Function


            ' Chart Of Accounts Module Here ...

        ElseIf MyForm = "DefMainAcc" Then
            FormName = DefMainAcc
            If Tags.Length > 0 Then
                DefMainAcc.Get_All(Tags)
            End If


        ElseIf MyForm = "frmSubAccount" Then
            FormName = frmSubAccount
            If Tags.Length > 0 Then
                frmSubAccount.Get_All(Tags)
            End If


        ElseIf MyForm = "frmSubSubAccount" Then
            FormName = frmSubSubAccount
            If Tags.Length > 0 Then
                frmSubSubAccount.Get_All(Tags)
            End If


        ElseIf MyForm = "frmDetailAccount" Then
            FormName = frmDetailAccount
            If Tags.Length > 0 Then
                frmDetailAccount.Get_All(Tags)
            End If


        ElseIf MyForm = "frmChangeDetailAccount" Then
            FormName = frmChangeDetailAccount

            'Chart of Accounts Groups'

        ElseIf MyForm = "ChartOfAccountGroups" Then
            FormName = ChartOfAccountGroups

        ElseIf MyForm = "frmCOAGroupsToAccountsMapping" Then
            FormName = frmCOAGroupsToAccountsMapping

        ElseIf MyForm = "frmCOAGroupsToUserMapping" Then
            FormName = frmCOAGroupsToUserMapping


            ' Cash and Bank Module Here ...

        ElseIf MyForm = "frmCashrequest" Or MyForm = "frmCashRequest" Then
            FormName = frmCashrequest
            If Tags.Length > 0 Then
                frmCashrequest.Get_All(Tags)
            End If


        ElseIf MyForm = "frmPaymentNew" Or MyForm = "frmVendorPayment" Or MyForm = "VendorPayments" Then
            Dim payment As String = getConfigValueByType("NewPaymentForm")
            If payment = "True" Then
                FormName = frmPaymentNew
                enm = EnumForms.frmVendorPayment
                CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
                If Tags.Length > 0 Then
                    frmPaymentNew.Get_All(Tags)
                End If
            Else
                FormName = frmVendorPayment
                enm = EnumForms.frmVendorPayment
                CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
                If Tags.Length > 0 Then
                    frmVendorPayment.Get_All(Tags)
                End If
            End If


        ElseIf MyForm = "frmCustomerCollection" Or MyForm = "frmOldCustomerCollection" Or MyForm = "CustomerCollection" Then
            Dim ReceiptScreen As Boolean = Convert.ToBoolean(getConfigValueByType("NewReceiptForm").ToString)
            If ReceiptScreen = True Then
                FormName = frmCustomerCollection
                enm = EnumForms.frmCustomerCollection
                CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
                If Tags.Length > 0 Then
                    frmCustomerCollection.Get_All(Tags)
                    'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
                End If
            Else
                FormName = frmOldCustomerCollection
                enm = EnumForms.frmCustomerCollection
                CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
                If Tags.Length > 0 Then
                    frmOldCustomerCollection.Get_All(Tags)
                End If
            End If


        ElseIf MyForm = "frmExpense" Then
            FormName = frmExpense
            If Tags.Length > 0 Then
                frmExpense.Get_All(Tags)
            End If


        ElseIf MyForm = "frmChequeTransfer" Then
            FormName = frmChequeTransfer


        ElseIf MyForm = "frmPaymentVoucherNew" Then
            FormName = frmPaymentVoucherNew


        ElseIf MyForm = "frmReceiptVoucherNew" Then
            FormName = frmReceiptVoucherNew

            ''Task 3461: Aashir: Edited Becuase Shortcut link account add option was not working.
        ElseIf MyForm = "Cash" Then
            FormName = Nothing
            FormName = FrmAddCustomers
            FrmAddCustomers.Close()
            SetAddCustomersForm(MyForm, "Cash Balances")
            FormName.Name = "Cash"

        ElseIf MyForm = "frmChequesAdjustment" Then
            FormName = frmChequesAdjustment

            ''Task 3461: Aashir: Edited Becuase Shortcut link account add option was not working.
        ElseIf MyForm = "Bank" Then
            FormName = Nothing
            FormName = FrmAddCustomers
            FrmAddCustomers.Close()
            SetAddCustomersForm(MyForm, "Bank Balances")
            FormName.Name = "Bank"

            ''Task 3461: Aashir: Edited Becuase Shortcut link account add option was not working.

        ElseIf MyForm = "AddCustomer" Then
            FormName = Nothing
            FormName = FrmAddCustomers
            FrmAddCustomers.Close()
            SetAddCustomersForm("Customer", "Add New Customer")
            FormName.Name = "AddCustomer"

            ''Task 3461: Aashir: Edited Becuase Shortcut link account add option was not working.
        ElseIf MyForm = "AddExpense" Then
            FormName = Nothing
            FormName = FrmAddCustomers
            FrmAddCustomers.Close()
            SetAddCustomersForm("Expense", "Add New Expense")
            FormName.Name = "Expense"

            ''Task 3461: Aashir: Edited Becuase Shortcut link account add option was not working.
        ElseIf MyForm = "General" Then
            FormName = Nothing
            FormName = FrmAddCustomers
            FrmAddCustomers.Close()
            SetAddCustomersForm("General", "Add General")
            FormName.Name = "General"

        ElseIf MyForm = "frmAddChequeBookSerial" Then
            FormName = frmAddChequeBookSerial


        ElseIf MyForm = "frmCustomerRecoveryTarget" Then
            FormName = frmCustomerRecoveryTarget


        ElseIf MyForm = "frmLockerConfiguration" Then
            FormName = frmLockerConfiguration


            ' Sales Module Start Here ...
        ElseIf MyForm = "frmSalesInquiryRights" Then
            FormName = frmSalesInquiryRights
        ElseIf MyForm = "DemandSales" Then
            FormName = DemandSales

        ElseIf MyForm = "frmSalesInquiryApproval" Then
            FormName = frmSalesInquiryApproval

        ElseIf MyForm = "frmQoutationNew" Then
            FormName = frmQoutationNew
            If Tags.Length > 0 Then
                frmQoutationNew.Get_All(Tags)
            End If

        ElseIf MyForm = "frmSalesOrderNew" Then
            FormName = frmSalesOrderNew
            If Tags.Length > 0 Then
                frmSalesOrderNew.Get_All(Tags)
            End If


        ElseIf MyForm = "frmDeliveryChalanStatus" Then
            FormName = frmDeliveryChalanStatus

        ElseIf MyForm = "frmSales" Or MyForm = "RecordSales" Then
            FormName = frmSales
            If Tags.Length > 0 Then
                frmSales.Get_All(Tags)
            End If

        ElseIf MyForm = "frmPOSEntry" Then
            Dim str4 As String
            str4 = "IF EXISTS(SELECT POSId FROM tblUserPOSRights WHERE UserID = " & LoginUserId & ") SELECT POSId, POSTitle FROM tblPOSConfiguration WHERE POSId IN (SELECT POSId FROM tblUserPOSRights WHERE UserID = " & LoginUserId & ") AND Active = 1 ORDER BY POSId ELSE SELECT POSId, POSTitle FROM tblPOSConfiguration WHERE Active = 1 ORDER BY POSId"
            Dim dt4 As DataTable
            dt4 = GetDataTable(str4)
            If dt4.Rows.Count > 1 Then
                Dim frmposlist As New frmPOSList(dt4)
                frmposlist.ShowDialog()
                If frmposlist.ReturnIs = True Then
                    FormName = frmPOSEntry
                Else
                    Exit Function
                End If
            Else
                If dt4.Rows.Count = 0 Then
                    ShowErrorMessage("You don't have rights of any POS")
                    Exit Function
                Else
                    'Dim str1 As String = "SELECT POSTitle, CompanyId, LocationId, CostCenterId, CashAccountId, BankAccountId, SalesPersonId, DeliveryOption FROM tblPOSConfiguration where POSId = '" & dt4.Rows(0).Item("POSId") & "'"
                    'Dim dt1 As DataTable
                    'dt1 = GetDataTable(str1)
                    'If dt1 IsNot Nothing Then
                    '    frmPOSEntry.Title = dt1.Rows(0).Item("POSTitle")
                    '    frmPOSEntry.CID = dt1.Rows(0).Item("CompanyId")
                    '    frmPOSEntry.LID = dt1.Rows(0).Item("LocationId")
                    '    frmPOSEntry.CCID = dt1.Rows(0).Item("CostCenterId")
                    '    frmPOSEntry.CAID = dt1.Rows(0).Item("CashAccountId")
                    '    frmPOSEntry.BAID = dt1.Rows(0).Item("BankAccountId")
                    '    frmPOSEntry.SPID = dt1.Rows(0).Item("SalesPersonId")
                    '    frmPOSEntry.DevOption = dt1.Rows(0).Item("DeliveryOption")
                    'End If
                    Dim str1 As String = "SELECT POSTitle, CompanyId, LocationId, CostCenterId, CashAccountId, BankAccountId, SalesPersonId, DeliveryOption, DiscountPer FROM tblPOSConfiguration where POSId = '" & dt4.Rows(0).Item("POSId") & "'"
                    Dim dt1 As DataTable
                    dt1 = GetDataTable(str1)
                    If dt1 IsNot Nothing Then
                        frmPOSEntry.Title = dt1.Rows(0).Item("POSTitle")
                        frmPOSEntry.CID = dt1.Rows(0).Item("CompanyId")
                        frmPOSEntry.LID = dt1.Rows(0).Item("LocationId")
                        frmPOSEntry.CCID = dt1.Rows(0).Item("CostCenterId")
                        frmPOSEntry.CAID = dt1.Rows(0).Item("CashAccountId")
                        frmPOSEntry.BAID = dt1.Rows(0).Item("BankAccountId")
                        frmPOSEntry.SPID = dt1.Rows(0).Item("SalesPersonId")
                        frmPOSEntry.DevOption = dt1.Rows(0).Item("DeliveryOption")
                        frmPOSEntry.DiscountPer = Val(dt1.Rows(0).Item("DiscountPer").ToString)
                    End If
                    FormName = frmPOSEntry
                End If
            End If

        ElseIf MyForm = "frmInstallment" Then
            FormName = frmInstallment


        ElseIf MyForm = "frmSalesTransfer" Then
            FormName = frmSalesTransfer


        ElseIf MyForm = "frmSalesCertificate" Then
            FormName = frmSalesCertificate


        ElseIf MyForm = "frmSalesReturn" Or MyForm = "SalesReturn" Then
            FormName = frmSalesReturn
            If Tags.Length > 0 Then
                frmSalesReturn.Get_All(Tags)
            End If


        ElseIf MyForm = "frmSOStatus" Then
            FormName = frmSOStatus


        ElseIf MyForm = "frmDeliveryChalan" Then
            FormName = frmDeliveryChalan
            If Tags.Length > 0 Then
                frmDeliveryChalan.Get_All(Tags)
            End If


        ElseIf MyForm = "frmGrdRptQuotationStatus" Then
            FormName = frmGrdRptQuotationStatus


        ElseIf MyForm = "frmBillAnalysis" Then
            FormName = frmBillAnalysis


        ElseIf MyForm = "frmSalesAdjustmentVoucher" Then
            FormName = frmSalesAdjustmentVoucher


        ElseIf MyForm = "frmUpdatebitlyAndTransporter" Then
            FormName = frmUpdatebitlyAndTransporter


        ElseIf MyForm = "frmSalesReturnWeight" Then
            FormName = frmSalesReturnWeight

        ElseIf MyForm = "frmEmployeeWiseMonthlySale" Then
            FormName = frmEmployeeWiseMonthlySale

            'Purchase Report
        ElseIf MyForm = "frmPurchaseInvDetailReport" Then
            FormName = frmPurchaseInvDetailReport

            ' Purchase Module Starts Here ...

        ElseIf MyForm = "frmPurchaseInquiry" Then
            FormName = frmPurchaseInquiry
            If Tags.Length > 0 Then
                frmPurchaseInquiry.Get_All(Tags)
            End If


        ElseIf MyForm = "frmVendorQuotation" Then
            FormName = frmVendorQuotation
            If Tags.Length > 0 Then
                frmVendorQuotation.Get_All(Tags)
                'Tags = String.Empty
            End If


        ElseIf MyForm = "frmSalesInquiryApproval" Then
            FormName = frmSalesInquiryApproval


        ElseIf MyForm = "frmInquiryComparisonStatement" Then
            FormName = frmInquiryComparisonStatement


        ElseIf MyForm = "frmPurchaseDemand" Then
            FormName = frmPurchaseDemand
            If Tags.Length > 0 Then
                frmPurchaseDemand.Get_All(Tags)
            End If


        ElseIf MyForm = "frmPurchaseOrderNew" Then
            FormName = frmPurchaseOrderNew


        ElseIf MyForm = "frmPurchase" Or MyForm = "frmPurchaseNew" Then
            FormName = frmPurchaseNew
            If Tags.Length > 0 Then
                frmPurchaseNew.Get_All(Tags)
            End If

        ElseIf MyForm = "frmPurchaseReturn" Or MyForm = "PurchaseReturn" Then
            FormName = frmPurchaseReturn
            enm = EnumForms.frmPurchaseReturn
            Rights = GroupRights.FindAll(AddressOf ReturnRights)
            If Tags.Length > 0 Then
                frmPurchaseReturn.Get_All(Tags)
            End If


        ElseIf MyForm = "frmPurchaseAdjustmentVoucher" Then
            FormName = frmPurchaseAdjustmentVoucher


        ElseIf MyForm = "frmBSandPLTemplateMapping" Then
            FormName = frmBSandPLTemplateMapping


        ElseIf MyForm = "frmPOStatus" Then
            FormName = frmPOStatus


        ElseIf MyForm = "frmGrdRptPurchaseDemandStatus" Then
            FormName = frmGrdRptPurchaseDemandStatus


        ElseIf MyForm = "frmGRNStatus" Then
            FormName = frmGRNStatus


        ElseIf MyForm = "frmDefVendor" Then
            FormName = frmDefVendor
            If Tags.Length > 0 Then
                frmDefVendor.Get_All(Tags)
            End If


            'Inventory Module Starts Here ...

        ElseIf MyForm = "frmRptReturnStoreIssuence" Then
            FormName = frmRptReturnStoreIssuence

        ElseIf MyForm = "frmStockReport " Then
            FormName = frmStockReport

        ElseIf MyForm = "frmStockAudit " Then
            FormName = frmStockAudit

        ElseIf MyForm = "frmItemWiseDiscount" Then
            FormName = frmItemWiseDiscount

        ElseIf MyForm = "frmStockTransferItemsList" Then
            FormName = frmStockTransferItemsList

        ElseIf MyForm = "frmComplaintReturnFromFactoryList" Then
            FormName = frmComplaintReturnFromFactoryList

        ElseIf MyForm = "frmComplaintReturnFactoryList" Then
            FormName = frmComplaintReturnFactoryList

        ElseIf MyForm = "frmComplaintRequestList" Then
            FormName = frmComplaintRequestList

        ElseIf MyForm = "frmComplaintReturnList" Then
            FormName = frmComplaintReturnList


        ElseIf MyForm = "frmPurchaseDemand" Then
            FormName = frmPurchaseDemand


        ElseIf MyForm = "frmReceivingNote" Then
            FormName = frmReceivingNote
            If Tags.Length > 0 Then
                frmReceivingNote.Get_All(Tags)
            End If

        ElseIf MyForm = "StoreIssuence" Or MyForm = "frmStoreIssuence" Then
            Dim ConfigValue As String = getConfigValueByType("CostSheetType")
            If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then
                FormName = frmStoreIssuence
                enm = EnumForms.frmStoreIssuence
                If Tags.Length > 0 Then
                    frmStoreIssuence.Get_All(Tags)
                End If
            Else
                FormName = frmStoreIssuenceNew
                enm = EnumForms.frmStoreIssuence
                If Tags.Length > 0 Then
                    frmStoreIssuenceNew.Get_All(Tags)
                End If
            End If


        ElseIf MyForm = "frmReturnStoreIssuence" Then
            FormName = frmReturnStoreIssuence
            If Tags.Length > 0 Then
                frmReturnStoreIssuence.Get_All(Tags)
            End If


        ElseIf MyForm = "frmStockDispatch" Then
            FormName = frmStockDispatch
            If Tags.Length > 0 Then
                frmStockDispatch.Get_All(Tags)
            End If


        ElseIf MyForm = "frmStockReceive" Then
            FormName = frmStockReceive


        ElseIf MyForm = "frmStockAdjustment" Then
            FormName = frmStockAdjustment


        ElseIf MyForm = "frmAdjeustmentAveragerate" Then
            FormName = frmAdjeustmentAveragerate


        ElseIf MyForm = "frmClaim" Then
            FormName = frmClaim
            If Tags.Length > 0 Then
                frmClaim.Get_All(Tags)
            End If


        ElseIf MyForm = "frmMRPlan" Then
            FormName = frmMRPlan


        ElseIf MyForm = "frmDefArticle" Then
            FormName = frmDefArticle


        ElseIf MyForm = "frmItemBulk" Then
            FormName = frmItemBulk


        ElseIf MyForm = "frmDefArticleDepartment" Then
            FormName = frmDefArticleDepartment


        ElseIf MyForm = "FrmLocation" Then
            FormName = FrmLocation

            If MyForm = "frmAddCostCenter" Then
                FormName = frmAddCostCenter
            End If

            'Production Module Here ...
        ElseIf MyForm = "frmCustomerRetentionTransferList" Then
            FormName = frmCustomerRetentionTransferList

        ElseIf MyForm = "frmRetentionTransferList" Then
            FormName = frmRetentionTransferList

        ElseIf MyForm = "frmProductionControlList" Then
            FormName = frmProductionControlList

        ElseIf MyForm = "frmDepartmentWiseProduction" Then
            FormName = frmDepartmentWiseProduction


        ElseIf MyForm = "frmProductionStore" Or MyForm = "ProductionStore" Then
            FormName = frmProductionStore
            If Tags.Length > 0 Then
                frmProductionStore.Get_All(Tags)
            End If


        ElseIf MyForm = "frmReturnStoreIssuance" Then
            FormName = frmReturnStoreIssuence


        ElseIf MyForm = "frmStockDispatch" Then
            FormName = frmStockDispatch


        ElseIf MyForm = "frmStockReceive" Then
            FormName = frmStockReceive


        ElseIf MyForm = "frmCostSheet" Then
            FormName = frmCostSheet


        ElseIf MyForm = "frmFinishGoodStandard" Then
            FormName = frmFinishGoodStandard

        ElseIf MyForm = "frmItemsConsumption" Then
            FormName = frmItemsConsumption


        ElseIf MyForm = "frmCloseBatch" Then
            FormName = frmCloseBatch


        ElseIf MyForm = "frmProductionOrderList" Then
            FormName = frmProductionOrderList



        ElseIf MyForm = "frmProductionLevel" Then
            FormName = frmProductionLevel


        ElseIf MyForm = "frmGrdProductionAnalaysis" Then
            FormName = frmGrdProductionAnalaysis


        ElseIf MyForm = "frmMRPlan" Then
            FormName = frmMRPlan


        ElseIf MyForm = "frmGrdRptProductionLevel" Then
            FormName = frmGrdRptProductionLevel


        ElseIf MyForm = "frmrptGrdProducedItems" Then
            FormName = frmrptGrdProducedItems


        ElseIf MyForm = "frmRptProductionSummary" Then
            FormName = frmRptProductionSummary


        ElseIf MyForm = "frmGrdRptProductionComparison" Then
            FormName = frmGrdRptProductionComparison

        ElseIf MyForm = "StoreIssuanceSummary" Then
            FormName = StoreIssuanceSummary

        ElseIf MyForm = "StoreIssuanceDetail" Then
            FormName = StoreIssuanceDetail

        ElseIf MyForm = "frmStockStatmentBySize" Then
            FormName = frmStockStatmentBySize

        ElseIf MyForm = "frmRptGrdStockStatement" Then
            FormName = frmRptGrdStockStatement

        ElseIf MyForm = "RptGridItemSalesHistory" Then
            FormName = RptGridItemSalesHistory

        ElseIf MyForm = "frmRptGrdStockInOutDetail" Then
            FormName = frmRptGrdStockInOutDetail

        ElseIf MyForm = "frmGrdRptLocationWiseStockLedger" Then
            FormName = frmGrdRptLocationWiseStockLedger

        ElseIf MyForm = "frmGrdRptProjectWiseStockLedger" Then
            FormName = frmGrdRptProjectWiseStockLedger

        ElseIf MyForm = "frmRptProductionSummary" Then
            FormName = frmRptProductionSummary

        ElseIf MyForm = "frmRptSummaryOfProduction" Then
            FormName = frmRptSummaryOfProduction

        ElseIf MyForm = "frmRptPlansStatus" Then
            FormName = frmRptPlansStatus

        ElseIf MyForm = "frmRptProductionBasedSalary" Then
            FormName = frmRptProductionBasedSalary

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "frmReturnStoreIssuenceReport" Then
            FormName = frmReturnStoreIssuenceReport

        ElseIf MyForm = "frmProductionLevel" Then
            FormName = frmProductionLevel

        ElseIf MyForm = "frmGrdProductionAnalaysis" Then
            FormName = frmGrdProductionAnalaysis

        ElseIf MyForm = "frmMRPlan" Then
            FormName = frmMRPlan

        ElseIf MyForm = "frmGrdProductionAnalaysis" Then
            FormName = frmGrdProductionAnalaysis

            'ElseIf MyForm = "frmProductionPlanningStandard" Then
            '    FormName = frmProductionPlanningStandard

        ElseIf MyForm = "frmProductionPlanningStandard" Then
            Dim ConfigValue As String = getConfigValueByType("CostSheetType")
            If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then
                FormName = frmCustomerPlanning
                'enm = EnumForms.frmCustomerPlanning
                'If Tags.Length > 0 Then
                '    frmCustomerPlanning.Get_All(Tags)
                'End If
            Else
                FormName = frmProductionPlanningStandard
                'If Tags.Length > 0 Then
                '    frmProductionPlanningStandard.Get_All(Tags)
                'End If
            End If

        ElseIf MyForm = "frmPlanTicketStandard" Then

            Dim ConfigValue As String = getConfigValueByType("CostSheetType")
            If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then
                FormName = frmPlanTickets
            Else
                FormName = frmPlanTicketStandard
            End If

        ElseIf MyForm = "frmMaterialDecomposition" Then
            FormName = frmMaterialDecomposition

        ElseIf MyForm = "frmMaterialEstimation" Then
            Dim ConfigValue As String = getConfigValueByType("CostSheetType")
            If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then
                FormName = frmMaterialEstimationPrior
            Else
                FormName = frmMaterialEstimation
            End If

        ElseIf MyForm = "frmTicketBookingList" Then
            FormName = frmTicketBookingList

        ElseIf MyForm = "frmBulkTicketsCreation" Then
            FormName = frmBulkTicketsCreation

        ElseIf MyForm = "frmProductionPlanStatus" Then
            FormName = frmProductionPlanStatus

        ElseIf MyForm = "frmRptPlansStatus" Then
            FormName = frmRptPlansStatus

        ElseIf MyForm = "frmSalesOrderNew" Then
            FormName = frmSalesOrderNew
            If Tags.Length > 0 Then
                frmSalesOrderNew.Get_All(Tags)
            End If

        ElseIf MyForm = "frmSOStatus" Then
            FormName = frmSOStatus

        ElseIf MyForm = "frmLetterCredit" Then
            FormName = frmLetterCredit

        ElseIf MyForm = "frmImport" Then
            FormName = frmImport

        ElseIf MyForm = "frmGrdLCDetail" Then
            FormName = frmGrdLCDetail

        ElseIf MyForm = "rptGridItemWiseLC" Then
            FormName = rptGridItemWiseLC

        ElseIf MyForm = "frmImportDetailReport" Then
            FormName = frmImportDetailReport

        ElseIf MyForm = "frmLCOutstandingDetailReport" Then
            FormName = frmLCOutstandingDetailReport

            'Property Module Here

        ElseIf MyForm = "frmPropertyProfileDetailReport" Then
            FormName = frmPropertyProfileDetailReport

            ' HR Module Here ...

        ElseIf MyForm = "frmDefEmployee" Then
            FormName = frmDefEmployee

        ElseIf MyForm = "frmEmployeeProfile" Then
            FormName = frmEmployeeProfile

        ElseIf MyForm = "frmEmployeePromotion" Then
            FormName = frmEmployeePromotion

        ElseIf MyForm = "frmDefLeaveEncashment" Then
            FormName = frmDefLeaveEncashment

        ElseIf MyForm = "frmAttendanceEmployees" Then
            FormName = frmAttendanceEmployees

        ElseIf MyForm = "frmEmployeeCard" Then
            FormName = frmEmployeeCard

        ElseIf MyForm = "frmAttendance" Then
            FormName = frmAttendance

        ElseIf MyForm = "FrmEmployeeSiteVisit" Then
            FormName = FrmEmployeeSiteVisit

        ElseIf MyForm = "FrmEmployeeSiteVisitCharges" Then
            FormName = FrmEmployeeSiteVisitCharges

        ElseIf MyForm = "frmLeaveApplication" Then
            FormName = frmLeaveApplication

        ElseIf MyForm = "frmDefDepartment" Then
            FormName = frmDefDepartment

        ElseIf MyForm = "frmDefEmpDesignation" Then
            FormName = frmDefEmpDesignation

        ElseIf MyForm = "frmHolidySetup" Then
            FormName = frmHolidySetup

        ElseIf MyForm = "frmGrdRptAttendanceRegisterUpdate" Then
            FormName = frmGrdRptAttendanceRegisterUpdate

        ElseIf MyForm = "frmEmpAttendanceEmailAlertSchedule" Then
            FormName = frmEmpAttendanceEmailAlertSchedule

        ElseIf MyForm = "frmDefLeaveTypes" Then
            FormName = frmDefLeaveTypes

        ElseIf MyForm = "frmLeaveAdjustment" Then
            FormName = frmLeaveAdjustment

        ElseIf MyForm = "frmNewLeaveApplication" Then
            FormName = frmNewLeaveApplication

        ElseIf MyForm = "frmEmployeeWarning" Then
            FormName = frmEmployeeWarning

        ElseIf MyForm = "frmEmployeeTermination" Then
            FormName = frmEmployeeTermination

        ElseIf MyForm = "frmShiftChangeRequest" Then
            FormName = frmShiftChangeRequest

        ElseIf MyForm = "frmEmployeeVisitPlan" Then
            FormName = frmEmployeeVisitPlan

        ElseIf MyForm = "frmRptEmpDetailWithBasicPayAndAllownces" Then
            FormName = frmRptEmpDetailWithBasicPayAndAllownces

            'Payroll Module Here ...

        ElseIf MyForm = "frmAutoSalaryGenerate" Then
            FormName = frmAutoSalaryGenerate

        ElseIf MyForm = "frmEmployeeSalaryVoucher" Then
            FormName = frmEmployeeSalaryVoucher

        ElseIf MyForm = "frmEmpOverTimeSchedule" Then
            FormName = frmEmpOverTimeSchedule
        ElseIf MyForm = "frmlatetimeSlot" Then
            FormName = frmlatetimeSlot

        ElseIf MyForm = "frmGrdRptLoanApprovalList" Then
            FormName = frmGrdRptLoanApprovalList

        ElseIf MyForm = "frmEmployeeDeductions" Then
            FormName = frmEmployeeDeductions

        ElseIf MyForm = "frmEmployeeAutoOverTime" Then
            FormName = frmEmployeeAutoOverTime

        ElseIf MyForm = "frmDefShift" Then
            FormName = frmDefShift

        ElseIf MyForm = "frmDefShiftGroup" Then
            FormName = frmDefShiftGroup

        ElseIf MyForm = "frmHolidySetup" Then
            FormName = frmHolidySetup

        ElseIf MyForm = "frmDefTaxSlabs" Then
            FormName = frmDefTaxSlabs

        ElseIf MyForm = "frmDailySalaries" Then
            FormName = frmDailySalaries

        ElseIf MyForm = "frmDefEmployee" Then
            FormName = frmDefEmployee

        ElseIf MyForm = "frmGrdRptEmployeeSalarySheetDetail" Then
            FormName = frmGrdRptEmployeeSalarySheetDetail

        ElseIf MyForm = "DailySalarySheet" Then
            FormName = DailySalarySheet

        ElseIf MyForm = "DailySalarySheetSummary" Then
            FormName = DailySalarySheetSummary

        ElseIf MyForm = "frmGrdRptGenerelEmployeeSalary" Then
            FormName = frmGrdRptGenerelEmployeeSalary

        ElseIf MyForm = "frmGrdRptEmployeeSalarySheetDetail" Then
            FormName = frmGrdRptEmployeeSalarySheetDetail

        ElseIf MyForm = "frmRptEmpSalarySheetDetail" Then
            FormName = frmRptEmpSalarySheetDetail

            ' CRM Module here ...

        ElseIf MyForm = "frmLeadProfileList" Then
            FormName = frmLeadProfileList
        ElseIf MyForm = "frmPlanNewActivity" Then
            FormName = frmPlanNewActivity
        ElseIf MyForm = "frmActivityPlanList" Then
            FormName = frmActivityPlanList
        ElseIf MyForm = "frmActivityFeedback" Then
            FormName = frmActivityFeedback
        ElseIf MyForm = "frmActivityCalender" Then
            FormName = frmActivityCalender
        ElseIf MyForm = "frmMissedVisitGraph" Then
            FormName = frmMissedVisitGraph
        ElseIf MyForm = "frmMissedVisitApproval" Then
            FormName = frmMissedVisitApproval
        ElseIf MyForm = "frmActivityHistory" Then
            FormName = frmActivityHistory
        ElseIf MyForm = "frmProjectList" Then
            FormName = frmProjectList

        ElseIf MyForm = "frmStatus" Then
            FormName = frmStatus

        ElseIf MyForm = "frmTasks" Then
            FormName = frmTasks

        ElseIf MyForm = "frmTypes" Then
            FormName = frmTypes

        ElseIf MyForm = "frmLeads" Then
            FormName = frmLeads

        ElseIf MyForm = "frmProjectPortFolio" Then
            FormName = frmProjectPortFolio

        ElseIf MyForm = "frmProjectVisit" Then
            FormName = frmProjectVisit

        ElseIf MyForm = "frmProjQuotion" Then
            FormName = frmProjQuotion

        ElseIf MyForm = "frmProjectVisitType" Then
            FormName = frmProjectVisitType

        ElseIf MyForm = "frmCompanyLocations" Then
            FormName = frmCompanyLocations

        ElseIf MyForm = "frmCompanyContacts" Then
            FormName = frmCompanyContacts

        ElseIf MyForm = "frmSalesInquiry" Then
            FormName = frmSalesInquiry
            If Tags.Length > 0 Then
                frmSalesInquiry.Get_All(Tags)
            End If

        ElseIf MyForm = "frmSalesInquiryRights" Then
            FormName = frmSalesInquiryRights

        ElseIf MyForm = "frmQoutationNew" Then
            FormName = frmQoutationNew
            If Tags.Length > 0 Then
                frmQoutationNew.Get_All(Tags)
            End If

        ElseIf MyForm = "frmGrdRptProjectVisitDetail" Then
            FormName = frmGrdRptProjectVisitDetail

        ElseIf MyForm = "frmPurchaseOrderNew" Then
            FormName = frmPurchaseOrderNew
            If Tags.Length > 0 Then
                frmPurchaseOrderNew.Get_All(Tags)
            End If

        ElseIf MyForm = "frmServiceItemTask" Then
            FormName = frmServiceItemTask

        ElseIf MyForm = "frmVendorContract" Then
            FormName = frmVendorContract

        ElseIf MyForm = "frmItemTaskProgress" Then
            FormName = frmItemTaskProgress

        ElseIf MyForm = "frmProjectProgressApproval" Then
            FormName = frmProjectProgressApproval

        ElseIf MyForm = "frmItemProgressReport" Then
            FormName = frmItemProgressReport

            ' Assets Module Here ...

        ElseIf MyForm = "frmAsset" Then
            FormName = frmAsset

        ElseIf MyForm = "frmFixedAssetCategory" Then
            FormName = frmFixedAssetCategory

        ElseIf MyForm = "frmAssetCategory" Then

            FormName = frmAssetCategory

        ElseIf MyForm = "AssetType" Then
            FormName = AssetType

        ElseIf MyForm = "frmAssetLocation" Then
            FormName = frmAssetLocation

        ElseIf MyForm = "AssetCondition" Then
            FormName = AssetCondition

        ElseIf MyForm = "frmAssetStatus" Then
            FormName = frmAssetStatus

        ElseIf MyForm = "frmGrdRptAssetsDetail" Then
            FormName = frmGrdRptAssetsDetail

            ' Site Module Here ...

        ElseIf MyForm = "frmSiteRegistration" Then
            FormName = frmSiteRegistration

        ElseIf MyForm = "frmCMFAAll" Then
            FormName = frmCMFAAll

        ElseIf MyForm = "frmGrdRptCMFAllRecords" Then
            FormName = frmGrdRptCMFAllRecords

        ElseIf MyForm = "frmGrdRptCMFASummary" Then
            FormName = frmGrdRptCMFASummary

        ElseIf MyForm = "frmGrdRptCMFAOfSummaries" Then
            FormName = frmGrdRptCMFAOfSummaries

        ElseIf MyForm = "frmRptCMFADetail" Then
            FormName = frmRptCMFADetail

        ElseIf MyForm = "frmDefJobCard" Then
            FormName = frmDefJobCard

        ElseIf MyForm = "frmLiftAssociation" Then
            FormName = frmLiftAssociation

        ElseIf MyForm = "frmIGP" Then
            FormName = frmIGP

        ElseIf MyForm = "frmWIP" Then
            FormName = frmWIP

        ElseIf MyForm = "JobCardDetail" Then


        ElseIf MyForm = "frmBudgetConfiguration" Then
            FormName = frmBudgetConfiguration

        ElseIf MyForm = "frmDefBudget" Then
            FormName = frmDefBudget

        ElseIf MyForm = "frmDefArticle" Then
            FormName = frmDefArticle

            'Configuration Module Here ...


        ElseIf MyForm = "frmDefArticleDepartment" Then
            FormName = frmDefArticleDepartment

        ElseIf MyForm = "frmDetailAccountCat" Then
            FormName = frmDetailAccountCat

        ElseIf MyForm = "frmDefSize" Then
            FormName = frmDefSize
            If Tags.Length > 0 Then
                frmDefSize.Get_All(Tags)
            End If

        ElseIf MyForm = "frmDefColor" Then
            FormName = frmDefColor
            If Tags.Length > 0 Then
                frmDefColor.Get_All(Tags)
            End If

        ElseIf MyForm = "frmDefCategory" Then
            FormName = frmDefCategory
            If Tags.Length > 0 Then
                frmDefCategory.Get_All(Tags)
            End If

        ElseIf MyForm = "frmDefSubCategory" Then
            FormName = frmDefSubCategory
            If Tags.Length > 0 Then
                frmDefSubCategory.Get_All(Tags)
            End If

        ElseIf MyForm = "frmDefType" Then
            FormName = frmDefType
            If Tags.Length > 0 Then
                frmDefType.Get_All(Tags)
            End If

        ElseIf MyForm = "frmDefUnit" Then
            FormName = frmDefUnit
            If Tags.Length > 0 Then
                frmDefUnit.Get_All(Tags)
            End If

        ElseIf MyForm = "frmDefGender" Then
            FormName = frmDefGender
            If Tags.Length > 0 Then
                frmDefGender.Get_All(Tags)
            End If

        ElseIf MyForm = "frmDefBatch" Then

            FormName = frmDefBatch
            If Tags.Length > 0 Then
                frmDefBatch.Get_All(Tags)
            End If

        ElseIf MyForm = "frmEmailConfiguration" Then
            FormName = frmEmailConfiguration

        ElseIf MyForm = "frmSMSConfig" Then
            FormName = frmSMSConfig

        ElseIf MyForm = "frmDefServices" Then
            FormName = frmDefServices


        ElseIf MyForm = "FrmLocation" Then
            FormName = FrmLocation

        ElseIf MyForm = "frmInventoryLevel" Then
            FormName = frmInventoryLevel

        ElseIf MyForm = "frmModelList" Then
            FormName = frmModelList

        ElseIf MyForm = "frmDefCustomer" Then
            FormName = frmDefCustomer
            If Tags.Length > 0 Then
                frmDefCustomer.Get_All(Tags)
            End If

        ElseIf MyForm = "frmDefCustomerType" Then
            FormName = frmDefCustomerType
            If Tags.Length > 0 Then
                frmDefCustomerType.Get_All(Tags)
            End If

        ElseIf MyForm = "CustomerBasedDiscountsFlat" Then
            FormName = frmCustomerDiscountsFlat


        ElseIf MyForm = "frmCustomerDiscountsFlat" Then
            FormName = frmCustomerDiscountsFlat

        ElseIf MyForm = "frmGrdCustomerBasedTarget" Then
            FormName = frmGrdCustomerBasedTarget

        ElseIf MyForm = "frmUserWiseCustomerList" Then
            FormName = frmUserWiseCustomerList

        ElseIf MyForm = "frmGrdSalesmanCommissionDetail" Then
            FormName = frmGrdSalesmanCommissionDetail

        ElseIf MyForm = "frmCustomerTypeWisePriceList" Then
            FormName = frmCustomerTypeWisePriceList

        ElseIf MyForm = "frmDefVendor" Then
            FormName = frmDefVendor

        ElseIf MyForm = "frmVendorType" Then
            FormName = frmVendorType

        ElseIf MyForm = "frmAgingBalancesTemplate" Then
            FormName = frmAgingBalancesTemplate


        ElseIf MyForm = "frmDefEmployee" Then
            If Tags.Length > 0 Then
                frmDefEmployee.Get_All(Tags)
            End If

        ElseIf MyForm = "frmDefEmpDesignation" Then
            FormName = frmDefEmpDesignation
            If Tags.Length > 0 Then
                frmDefEmpDesignation.Get_All(Tags)
            End If

        ElseIf MyForm = "frmDefDepartment" Then
            FormName = frmDefDepartment
            If Tags.Length > 0 Then
                frmDefDepartment.Get_All(Tags)
            End If

        ElseIf MyForm = "frmEmployeeArticleCostRate" Then
            FormName = frmEmployeeArticleCostRate

        ElseIf MyForm = "frmDefCity" Then
            FormName = frmDefCity
            If Tags.Length > 0 Then
                frmDefCity.Get_All(Tags)
            End If

        ElseIf MyForm = "frmDefArea" Then
            FormName = frmDefArea
            If Tags.Length > 0 Then
                frmDefArea.Get_All(Tags)
            End If

        ElseIf MyForm = "frmCostCenter" Then
            FormName = frmCostCenter
            If Tags.Length > 0 Then
                frmCostCenter.Get_All(Tags)
            End If

        ElseIf MyForm = "FrmCountry" Then
            FormName = FrmCountry

        ElseIf MyForm = "frmState" Then
            FormName = frmState

        ElseIf MyForm = "FrmRegions" Then
            FormName = FrmRegions

        ElseIf MyForm = "FrmZone" Then
            FormName = FrmZone

        ElseIf MyForm = "FrmBelt" Then
            FormName = FrmBelt

        ElseIf MyForm = "frmPOSConfiguration" Then
            FormName = frmPOSConfiguration

        ElseIf MyForm = "frmSystemConfigurationNew" Then
            FormName = frmSystemConfigurationNew

        ElseIf MyForm = "FrmEmailconfig" Then
            FormName = FrmEmailconfig

        ElseIf MyForm = "frmDefVehicle" Then
            FormName = frmDefVehicle

        ElseIf MyForm = "frmRootPlan" Then
            FormName = frmRootPlan

        ElseIf MyForm = "frmDefDocumentPrefix" Then
            FormName = frmDefDocumentPrefix

        ElseIf MyForm = "frmDefTaxSlabs" Then
            FormName = frmDefTaxSlabs

        ElseIf MyForm = "frmDefEmployeeMonthlyTarget" Then
            FormName = frmDefEmployeeMonthlyTarget

        ElseIf MyForm = "frmTerminalConfiguration" Then
            FormName = frmTerminalConfiguration

        ElseIf MyForm = "frmDefGroupVoucherApproval" Then
            FormName = frmDefGroupVoucherApproval

        ElseIf MyForm = "frmNotificationList" Then
            FormName = frmNotificationList

        ElseIf MyForm = "frmTermsandConditions" Then
            FormName = frmTermsandConditions

        ElseIf MyForm = "frmInventoryColumnStrings" Then
            FormName = frmInventoryColumnStrings

        ElseIf MyForm = "frmCustomerBottomSaleRate" Then
            FormName = frmCustomerBottomSaleRate

        ElseIf MyForm = "frmUserGroup" Then
            FormName = frmUserGroup

            'ElseIf Myform = "frmReturnablegatepass" Then
            '    FormName = frmReturnablegatepass
            'End If
        ElseIf MyForm = "frmUpdateReturnableGatepassDetail" Then
            FormName = frmUpdateReturnableGatepassDetail
        ElseIf MyForm = "frmDefVehicle" Then
            FormName = frmDefVehicle

        ElseIf MyForm = "FrmVehicle" Then
            FormName = FrmVehicle

        ElseIf MyForm = "frmDateLock" Then
            FormName = frmDateLock

        ElseIf MyForm = "frmDateLockPermission" Then
            FormName = frmDateLockPermission

        ElseIf MyForm = "frmAgreement" Then
            FormName = frmAgreement

        ElseIf MyForm = "frmGrdRptContactList" Then
            FormName = frmGrdRptContactList

        ElseIf MyForm = "frmdbbackup" Then
            FormName = frmdbbackup

        ElseIf MyForm = "frmRestoreBackup" Then
            FormName = frmRestoreBackup

        ElseIf MyForm = "frmActivityLog" Then
            FormName = frmActivityLog

        ElseIf MyForm = "frmTerminal" Then
            FormName = frmTerminal

        ElseIf MyForm = "frmReleaseUpdate" Then
            FormName = frmReleaseUpdate

        ElseIf MyForm = "frmComposeMessage" Then
            FormName = frmComposeMessage

        ElseIf MyForm = "frmMessageView" Then
            FormName = frmMessageView

        ElseIf MyForm = "frmOpening" Then
            FormName = frmOpening

        ElseIf MyForm = "frmActiveLicense" Then
            FormName = frmActiveLicense

        ElseIf MyForm = "FrmVehicle" Then
            FormName = FrmVehicle

        ElseIf MyForm = "frmScheduleSMS" Then
            FormName = frmScheduleSMS

        ElseIf MyForm = "frmTroubleshoot" Then
            FormName = frmTroubleshoot

        ElseIf MyForm = "frmGrdRptDuplicateDocuments" Then
            FormName = frmGrdRptDuplicateDocuments

        ElseIf MyForm = "frmUpdateCurrency" Then
            FormName = frmUpdateCurrency

        ElseIf MyForm = "frmCostCentreReshuffle" Then
            FormName = frmCostCentreReshuffle

        ElseIf MyForm = "frmDataTransfer" Then
            FormName = frmDataTransfer

        ElseIf MyForm = "frmDataImport" Then
            FormName = frmDataImport


        ElseIf MyForm = "frmUtilityApplyAverageRate" Then
            FormName = frmUtilityApplyAverageRate

        ElseIf MyForm = "frmApplySalePriceUtility" Then
            FormName = frmApplySalePriceUtility

        ElseIf MyForm = "frmSkippingSalesInvoicesNumbers" Then
            FormName = frmSkippingSalesInvoicesNumbers

        ElseIf MyForm = "frmRevenueDataImport" Then
            FormName = frmRevenueDataImport

        ElseIf MyForm = "frmSMSLog" Then
            FormName = frmSMSLog

            'Approval hierarchy Module'

        ElseIf MyForm = "frmApprovalStages" Then
            FormName = frmApprovalStages

        ElseIf MyForm = "frmApprovalProcess" Then
            FormName = frmApprovalProcess

        ElseIf MyForm = "frmApprovalRejectionReason" Then
            FormName = frmApprovalRejectionReason

        ElseIf MyForm = "frmApprovalStagesMapping" Then
            FormName = frmApprovalStagesMapping

            'Production Configration'
        ElseIf MyForm = "frmWarrantyClaimList" Then
            FormName = frmWarrantyClaimList

        ElseIf MyForm = "frmproductionSteps" Then
            FormName = frmproductionSteps

        ElseIf MyForm = "frmProductionProcess" Then
            FormName = frmProductionProcess

        ElseIf MyForm = "frmLabourType" Then
            FormName = frmLabourType

        ElseIf MyForm = "frmUpdatebitlyAndTransporter" Then
            FormName = frmUpdatebitlyAndTransporter

        ElseIf MyForm = "frmAdjustments" Then
            FormName = frmAdjustments


        ElseIf MyForm = "frmBillAnalysis" Then
            FormName = frmBillAnalysis

        ElseIf MyForm = "DemandSummary" Then
            FormName = DemandSummary

        ElseIf MyForm = "frmGrdRptFrequentellySalesOrderItems" Then
            FormName = frmGrdRptFrequentellySalesOrderItems

        ElseIf MyForm = "frmRptCustomerSalesContribution" Then
            FormName = frmRptCustomerSalesContribution

        ElseIf MyForm = "frmGrdRptMinimumStockLevel" Then
            FormName = frmGrdRptMinimumStockLevel


        ElseIf MyForm = "frmDefGroupVoucherApproval" Then
            FormName = frmDefGroupVoucherApproval

        ElseIf MyForm = "frmGrdArticleLedgerByPack" Then
            FormName = frmGrdArticleLedgerByPack

        ElseIf MyForm = "frmRptGrdStockStatementByPack" Then
            FormName = frmRptGrdStockStatementByPack

        ElseIf MyForm = "frmGrdRptRackWiseClosingStock" Then
            FormName = frmGrdRptRackWiseClosingStock

        ElseIf MyForm = "frmInventoryColumnStrings" Then
            FormName = frmInventoryColumnStrings

        ElseIf MyForm = "frmGrdLCDetail" Then
            FormName = frmGrdLCDetail

        ElseIf MyForm = "rptGridItemWiseLC" Then
            FormName = rptGridItemWiseLC

        ElseIf MyForm = "frmImportDetailReport" Then
            FormName = frmImportDetailReport

        ElseIf MyForm = "frmLCOutstandingDetailReport" Then
            FormName = frmLCOutstandingDetailReport

        ElseIf MyForm = "frmMRPlan" Then
            FormName = frmMRPlan


        ElseIf MyForm = "frmAllowancetype" Then
            FormName = frmAllownaceType

        ElseIf MyForm = "frmDeductionType" Then
            FormName = frmDeductionType

        ElseIf MyForm = "frmDefDivision" Then
            FormName = frmDefDivision

        ElseIf MyForm = "frmDefPayRollDivision" Then
            FormName = frmDefPayRollDivision

        ElseIf MyForm = "frmTerminalConfiguration" Then
            FormName = frmTerminalConfiguration

        ElseIf MyForm = "frmYearClose" Then
            FormName = frmYearClose




            ' Property Module Here ...
        ElseIf MyForm = "frmUtilityApplyAverageRate" Then
            FormName = frmUtilityApplyAverageRate

        ElseIf MyForm = "frmProItemList" Then
            FormName = frmProItemList

        ElseIf MyForm = "frmPropertyProfileList" Then
            FormName = frmPropertyProfileList

        ElseIf MyForm = "frmProSalesList" Or MyForm = "frmProSales" Then
            FormName = frmProSalesList
            If Tags.Length > 0 Then
                frmProSalesList.Get_All(Tags)
            End If

        ElseIf MyForm = "frmProPurchaseList" Or MyForm = "frmProPurchase" Then
            FormName = frmProPurchaseList
            If Tags.Length > 0 Then
                frmProPurchaseList.Get_All(Tags)
            End If

        ElseIf MyForm = "frmProInvestorList" Then
            FormName = frmProInvestorList

        ElseIf MyForm = "frmProEstateList" Then
            FormName = frmProEstateList

        ElseIf MyForm = "frmProAgentList" Then
            FormName = frmProAgentList

        ElseIf MyForm = "frmProOfficeList" Then
            FormName = frmProOfficeList

        ElseIf MyForm = "frmProBranchList" Then
            FormName = frmProBranchList

        ElseIf MyForm = "frmProDealerList" Then
            FormName = frmProDealerList

        ElseIf MyForm = "frmInvoiceAdjustment" Then
            FormName = frmInvoiceAdjustment

        ElseIf MyForm = "frmaboutus" Then
            FormName = frmaboutus

            ' Report Button Starts here ...

            ' Cash and Bank

        ElseIf MyForm = "CashFlowStatementStandard" Then
            FormName = CashFlowStatementStandard

        ElseIf MyForm = "rptExpenses" Then
            FormName = rptExpenses

        ElseIf MyForm = "CashFlowStatement" Then
            FormName = CashFlowStatement

        ElseIf MyForm = "frmGrdRptAgingPayables" Then
            FormName = frmGrdRptAgingPayables

        ElseIf MyForm = "frmGrdRptAgingReceiveables" Then
            FormName = frmGrdRptAgingReceiveables

        ElseIf MyForm = "InvoiceBasedPaymentSummaryReport" Then
            FormName = InvoiceBasedPaymentSummaryReport

            'If MyForm = "frmReturnablegatepass" Then
            '    FormName = frmReturnablegatepass
            'End If

            'If MyForm = "frmServicesInwardGatePass" Then
            '    FormName = frmServicesInwardGatePass
            'End If


        ElseIf MyForm = "frmServicesInwardGatePass" Then
            FormName = frmServicesInwardGatePass

        ElseIf MyForm = "JobCardDetail" Then
            FormName = JobCardDetail

        ElseIf MyForm = "JobCardSummary" Then
            FormName = JobCardSummary

        ElseIf MyForm = "frmFreeServiceCardReport" Then
            FormName = frmFreeServiceCardReport

        ElseIf MyForm = "frmRepeateCustomerReport" Then
            FormName = frmRepeateCustomerReport

        ElseIf MyForm = "GroupWiseSalesReport" Then
            FormName = GroupWiseSalesReport

        ElseIf MyForm = "JobCardCommissionReport" Then
            FormName = JobCardCommissionReport

        ElseIf MyForm = "frmLiftWisePercentageReport" Then
            FormName = frmLiftWisePercentageReport

            ' Property Module Here ...

        ElseIf MyForm = "frmLiftWiseBusinessReport" Then
            FormName = frmLiftWiseBusinessReport

        ElseIf MyForm = "frmLiftWiseDetailReport" Then
            FormName = frmLiftWiseDetailReport

        ElseIf MyForm = "frmRptInvoiceAging" Then
            FormName = frmRptInvoiceAging

            'Reports

        ElseIf MyForm = "rptReturnableGatepassNew" Then
            FormName = rptReturnableGatepassNew

        ElseIf MyForm = "AccountsReports" Then
            FormName = AccountsReports
            'enm = EnumForms.frmVoucher

        ElseIf MyForm = "Customer" Then
            FormName = Customer
            enm = EnumForms.frmVoucher

        ElseIf MyForm = "Vendors" Then
            FormName = Vendors
            enm = EnumForms.frmVoucher
        ElseIf MyForm = "frmGrdRptCharts" Then
            FormName = frmGrdRptCharts
            enm = EnumForms.Non
        ElseIf MyForm = "Employee" Then
            FormName = Employee
            enm = EnumForms.frmVoucher
        ElseIf MyForm = "Stock" Then
            FormName = Stock
            enm = EnumForms.frmVoucher
        ElseIf MyForm = "Purchase" Then
            FormName = Purchase
            enm = EnumForms.frmVoucher
        ElseIf MyForm = "Sales" Then
            FormName = Sales
            enm = EnumForms.frmVoucher

        ElseIf MyForm = "frmGrdRptSaleOrderStatusSummary" Then
            FormName = frmGrdRptSaleOrderStatusSummary

            'Sales Reports
        ElseIf MyForm = "frmInvDetailReport" Then
            FormName = frmInvDetailReport

        ElseIf MyForm = "frmGrdRptCustomersItemsSummarySales" Then
            FormName = frmGrdRptCustomersItemsSummarySales

        ElseIf MyForm = "frmGrdSales" Then
            FormName = frmGrdSales

        ElseIf MyForm = "frmGrdRptSalesByGender" Then
            FormName = frmGrdRptSalesByGender

        ElseIf MyForm = "frmGrdDispatchDetail" Then
            FormName = frmGrdDispatchDetail

        ElseIf MyForm = "frmGrdSalemansDemandDetail" Then
            FormName = frmGrdSalemansDemandDetail

        ElseIf MyForm = "DamageBudget" Then
            FormName = DamageBudget

        ElseIf MyForm = "frmGrdRptSectorSales" Then
            FormName = frmGrdRptSectorSales

        ElseIf MyForm = "DeliveryChallanDetail" Then
            FormName = DeliveryChallanDetail

        ElseIf MyForm = "DeliveryChallanSummary" Then
            FormName = DeliveryChallanSummary

        ElseIf MyForm = "frmrptRoutePlanGatePass" Then
            FormName = frmrptRoutePlanGatePass

        ElseIf MyForm = "frmRptDSRStatement" Then
            FormName = frmRptDSRStatement

        ElseIf MyForm = "frmGrdSalesmanCommissionDetail" Then
            FormName = frmGrdSalesmanCommissionDetail

        ElseIf MyForm = "NetSalesReport" Then
            FormName = NetSalesReport

        ElseIf MyForm = "frmGrdSalesSummary" Then
            FormName = frmGrdSalesSummary

        ElseIf MyForm = "frmGrdSalesReturnDetail" Then
            FormName = frmGrdSalesReturnDetail

        ElseIf MyForm = "frmSalesComparisonCustomerWise" Then
            FormName = frmSalesComparisonCustomerWise

        ElseIf MyForm = "frmGrdRptSalesComparison" Then
            FormName = frmGrdRptSalesComparison

        ElseIf MyForm = "SalesCertificateIssued" Then
            FormName = SalesCertificateIssued

        ElseIf MyForm = "frmGrdRptConsolidateItemSalesCustomerWise" Then
            FormName = frmGrdRptConsolidateItemSalesCustomerWise

        ElseIf MyForm = "frmGrdRptSalesCertificateIssued" Then
            FormName = frmGrdRptSalesCertificateIssued

        ElseIf MyForm = "frmGrdRptProductCustomerWiseReport" Then
            FormName = frmGrdRptProductCustomerWiseReport

        ElseIf MyForm = "frmGrdRptProductDateWiseReport" Then
            FormName = frmGrdRptProductDateWiseReport

        ElseIf MyForm = "frmGrdRptSalesSummaries" Then
            FormName = frmGrdRptSalesSummaries

        ElseIf MyForm = "frmGrdRptCustomerItemWiseSummary" Then
            FormName = frmGrdRptCustomerItemWiseSummary

        ElseIf MyForm = "frmGrdRptItemWiseSalesSummary" Then
            FormName = frmGrdRptItemWiseSalesSummary

        ElseIf MyForm = "frmGrdRptSalesPriceChange" Then
            FormName = frmGrdRptSalesPriceChange

        ElseIf MyForm = "rptAdvanceReceiptsSO" Then

            rptDateRange.ReportName = rptDateRange.ReportList.AdvanceReceiptsSO
            'ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()

        ElseIf MyForm = "frmGrdRptCostSheetQtyWise" Then
            FormName = frmGrdRptCostSheetQtyWise

        ElseIf MyForm = "frmGrdRptSalesRegisterActivity" Then
            FormName = frmGrdRptSalesRegisterActivity

        ElseIf MyForm = "frmGrdRptSaleInvoicesDue" Then
            FormName = frmGrdRptSaleInvoicesDue

        ElseIf MyForm = "frmRptSalesCertificateLedger" Then
            FormName = frmRptSalesCertificateLedger

        ElseIf MyForm = "frmGrdRptRackWiseClosingStock" Then
            FormName = frmGrdRptRackWiseClosingStock

        ElseIf MyForm = "rptVoucher" Then
            FormName = rptVoucher

        ElseIf MyForm = "frmDSRStatementNew" Then
            FormName = frmDSRStatementNew

        ElseIf MyForm = "frmGrdRptInstallmentBalance" Then
            FormName = frmGrdRptInstallmentBalance

        ElseIf MyForm = "frmGrdRptCostSheetMarginCalculationDetail" Then
            FormName = frmGrdRptCostSheetMarginCalculationDetail

        ElseIf MyForm = "frmGrdRptTaxDuductionDetail" Then
            FormName = frmGrdRptTaxDuductionDetail

        ElseIf MyForm = "frmGrdRptEmployeeMonthlyTergetAchieved" Then
            FormName = frmGrdRptEmployeeMonthlyTergetAchieved

        ElseIf MyForm = "frmCustomerDiscounts" Then
            FormName = frmCustomerDiscounts

        ElseIf MyForm = "frmGrdRptEngineWiseStock" Then
            FormName = frmGrdRptEngineWiseStock

        ElseIf MyForm = "SummaryofSalesAndReturns" Then
            FormName = SummaryofSalesAndReturns

        ElseIf MyForm = "SummaryofSalesAndReturns" Then
            FormName = SummaryofSalesAndReturns

        ElseIf MyForm = "frmGrdRptSaleOrderStatusSummary" Then
            FormName = frmGrdRptSaleOrderStatusSummary

        ElseIf MyForm = "frmInvoiceWiseProfitReport" Then
            FormName = frmInvoiceWiseProfitReport

            'If MyForm = "frmRptList" Then
            '    FormName = frmRptList
            'End If



            ' Purchase Reports

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "frmRptGrdPurchaseDetailWithWeight" Then
            FormName = frmRptGrdPurchaseDetailWithWeight

        ElseIf MyForm = "frmGrdRptItemExpiryDateDetail" Then
            FormName = frmGrdRptItemExpiryDateDetail

        ElseIf MyForm = "frmGrdPurchaseSummary" Then
            FormName = frmGrdPurchaseSummary

        ElseIf MyForm = "rptPurchaseDailyWorkingReport" Then
            FormName = rptPurchaseDailyWorkingReport

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "frmRptMonthlyPurchaseSummary" Then
            FormName = frmRptMonthlyPurchaseSummary

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "frmGrdImportLedger" Then
            FormName = frmGrdImportLedger

        ElseIf MyForm = "frmGrdRptTaxDuductionDetail" Then
            FormName = frmGrdRptTaxDuductionDetail

        ElseIf MyForm = "frmGrdRptAgingPayables" Then
            FormName = frmGrdRptAgingPayables

        ElseIf MyForm = "frmGrdRptInvoiceAging" Then
            FormName = frmGrdRptInvoiceAging

        ElseIf MyForm = "SummaryofPurchasesAndReturns" Then
            FormName = SummaryofPurchasesAndReturns

        ElseIf MyForm = "frmGrdRptToOrderQty" Then
            FormName = frmGrdRptToOrderQty

        ElseIf MyForm = "frmGRNDetailReport" Then
            FormName = frmGRNDetailReport

        ElseIf MyForm = "frmRptPurchaseGRNRejectedQty" Then
            FormName = frmRptPurchaseGRNRejectedQty

        ElseIf MyForm = "frmRptAccountWisePurchaseReport" Then
            FormName = frmRptAccountWisePurchaseReport

            ' Inventory Reports

        ElseIf MyForm = "frmBulkStockTransfer " Then
            FormName = frmBulkStockTransfer

        ElseIf MyForm = "StoreIssuanceSummary" Then
            FormName = StoreIssuanceSummary

        ElseIf MyForm = "StoreIssuanceDetail" Then
            FormName = StoreIssuanceDetail

        ElseIf MyForm = "rptStockForm" Then
            FormName = rptStockForm

        ElseIf MyForm = "StockStatementByLPO" Then
            FormName = StockStatementByLPO

        ElseIf MyForm = "StockStatementWithSize" Then
            FormName = StockStatementWithSize

        ElseIf MyForm = "frmStockStatmentBySize" Then
            FormName = frmStockStatmentBySize

        ElseIf MyForm = "frmRptGrdStockStatement" Then
            FormName = frmRptGrdStockStatement

        ElseIf MyForm = "RptGridItemSalesHistory" Then
            FormName = RptGridItemSalesHistory

        ElseIf MyForm = "frmRptGrdStockInOutDetail" Then
            FormName = frmRptGrdStockInOutDetail

        ElseIf MyForm = "frmGrdRptLocationWiseStockLedger" Then
            FormName = frmGrdRptLocationWiseStockLedger

        ElseIf MyForm = "frmGrdRptStockStatementUnitWise" Then
            FormName = frmGrdRptStockStatementUnitWise

        ElseIf MyForm = "frmGrd_Prod_DC_WiseStock" Then
            FormName = frmGrd_Prod_DC_WiseStock

        ElseIf MyForm = "ListOfItems" Then
            ShowReport("ListOfItems")

        ElseIf MyForm = "frmRptArticleBarcode" Then
            FormName = frmRptArticleBarcode

        ElseIf MyForm = "frmGrdCostSheetComparisonWithStock" Then
            FormName = frmGrdCostSheetComparisonWithStock

        ElseIf MyForm = "frmGrdPlanComparison" Then
            FormName = frmGrdPlanComparison

        ElseIf MyForm = "frmGrdArticleLedger" Then
            FormName = frmGrdArticleLedger

        ElseIf MyForm = "frmGrdArticleLedgerByPack" Then
            FormName = frmGrdArticleLedgerByPack

        ElseIf MyForm = "frmRptRental" Then
            FormName = frmRptRental

        ElseIf MyForm = "frmGrdRptProjectWiseStockLedger" Then
            FormName = frmGrdRptProjectWiseStockLedger

        ElseIf MyForm = "frmGrdRptLocationWiseStockStatement" Then
            FormName = frmGrdRptLocationWiseStockStatement

        ElseIf MyForm = "frmGrdRptLocationWiseStockStatementNew" Then
            FormName = frmGrdRptLocationWiseStockStatementNew

        ElseIf MyForm = "frmRptLocationWiseClosingStock" Then
            FormName = frmRptLocationWiseClosingStock

        ElseIf MyForm = "StoreIssuanceDetailBatchWise" Then
            FormName = StoreIssuanceDetailBatchWise

        ElseIf MyForm = "WarrantyDetailReport" Then
            FormName = WarrantyDetailReport

        ElseIf MyForm = "DispatchStatus" Then
            FormName = DispatchStatus

        ElseIf MyForm = "frmGrdRptClosingStockByOrders" Then
            FormName = frmGrdRptClosingStockByOrders

        ElseIf MyForm = "frmItemBarCodePrinting" Then
            FormName = frmItemBarCodePrinting

        ElseIf MyForm = "frmGrdRptClosingStockByGRNnDC" Then
            FormName = frmGrdRptClosingStockByGRNnDC

        ElseIf MyForm = "frmGRNStockReport" Then
            FormName = frmGRNStockReport

        ElseIf MyForm = "frmGrdRptCostSheetPlanDetail" Then
            FormName = frmGrdRptCostSheetPlanDetail

        ElseIf MyForm = "frmRptGrdStockStatementByPack" Then
            FormName = frmRptGrdStockStatementByPack

        ElseIf MyForm = "frmTransferredStockReport" Then
            FormName = frmTransferredStockReport

        ElseIf MyForm = "frmLocationWiseStockReport" Then
            FormName = frmLocationWiseStockReport

            ' HR Reports

        ElseIf MyForm = "DailyAttendance" Then
            FormName = DailyAttendance

        ElseIf MyForm = "AttedanceSummary" Then
            FormName = AttedanceSummary

        ElseIf MyForm = "EmployeeAttendanceDetail" Then
            FormName = EmployeeAttendanceDetail

        ElseIf MyForm = "frmGrdRptEmployeeMonthlyAttendance" Then
            FormName = frmGrdRptEmployeeMonthlyAttendance

        ElseIf MyForm = "frmGrdRptEmployeeSalarySheetDetail" Then
            FormName = frmGrdRptEmployeeSalarySheetDetail

        ElseIf MyForm = "DailySalarySheet" Then
            FormName = DailySalarySheet

        ElseIf MyForm = "DailySalarySheetSummary" Then
            FormName = DailySalarySheetSummary

        ElseIf MyForm = "frmGrdRptGenerelEmployeeSalary" Then
            FormName = frmGrdRptGenerelEmployeeSalary

        ElseIf MyForm = "frmRptEmpSalarySheetDetail" Then
            FormName = frmRptEmpSalarySheetDetail

        ElseIf MyForm = "frmGrdRptAttendanceRegister" Then
            FormName = frmGrdRptAttendanceRegister

        ElseIf MyForm = "frmEmployeeOverTimeReport" Then
            FormName = frmEmployeeOverTimeReport

        ElseIf MyForm = "frmAttendanceStatusDetailReport" Then
            FormName = frmAttendanceStatusDetailReport

        ElseIf MyForm = "frmEmployeeStatusList" Then
            FormName = frmEmployeeStatusList

        ElseIf MyForm = "frmCashInLeaveReport" Then
            FormName = frmCashInLeaveReport

        ElseIf MyForm = "LateComingEmployee" Then
            FormName = LateComingEmployee

        ElseIf MyForm = "OverTimeEmployee" Then
            FormName = OverTimeEmployee

        ElseIf MyForm = "LateInTimeSummary" Then
            FormName = LateInTimeSummary

        ElseIf MyForm = "LateArrivalDays" Then
            FormName = LateArrivalDays

        ElseIf MyForm = "frmEmployeeBirthday" Then
            FormName = frmEmployeeBirthday

        ElseIf MyForm = "frmEmployeeCNICExpiry" Then
            FormName = frmEmployeeCNICExpiry

        ElseIf MyForm = "frmEmployeeNoOfHits" Then
            FormName = frmEmployeeNoOfHits

        ElseIf MyForm = "frmEmpAttendenceInOutMissing" Then
            FormName = frmEmpAttendenceInOutMissing

        ElseIf MyForm = "frmEmployeeWiseLedger" Then
            FormName = frmEmployeeWiseLedger

            'Payroll Reports

        ElseIf MyForm = "frmGrdRptEmployeeSalarySheetDetail" Then
            FormName = frmGrdRptEmployeeSalarySheetDetail

        ElseIf MyForm = "DailySalarySheet" Then
            FormName = DailySalarySheet

        ElseIf MyForm = "DailySalarySheetSummary" Then
            FormName = DailySalarySheetSummary

        ElseIf MyForm = "frmGrdRptGenerelEmployeeSalary" Then
            FormName = frmGrdRptGenerelEmployeeSalary

        ElseIf MyForm = "frmGrdRptEmployeeSalarySheetDetail" Then
            FormName = frmGrdRptEmployeeSalarySheetDetail

        ElseIf MyForm = "frmRptEmpSalarySheetDetail" Then
            FormName = frmRptEmpSalarySheetDetail

        ElseIf MyForm = "frmEmployeeOverTimeReport" Then
            FormName = frmEmployeeOverTimeReport

            'CRM Reports

        ElseIf MyForm = "frmComplaintDetail" Then
            FormName = frmComplaintDetail

        ElseIf MyForm = "frmComplaintComparison" Then
            FormName = frmComplaintComparison

        ElseIf MyForm = "frmComplaintToFactoryAndReturnFromFactory" Then
            FormName = frmComplaintToFactoryAndReturnFromFactory

        ElseIf MyForm = "RptDateRangeEmployees" Then
            FormName = RptDateRangeEmployees

        ElseIf MyForm = "frmRptTaskDetail" Then
            FormName = frmRptTaskDetail

        ElseIf MyForm = "frmRptProjectHistory" Then
            FormName = frmRptProjectHistory

        ElseIf MyForm = "frmGrdRptProjectVisitDetail" Then
            FormName = frmGrdRptProjectVisitDetail

        ElseIf MyForm = "rptTodayTasks" Then
            FormName = rptTodayTasks

        ElseIf MyForm = "frmRptServicesStockLedger" Then
            FormName = frmRptServicesStockLedger

        ElseIf MyForm = "frmRptServicesProduction" Then
            FormName = frmRptServicesProduction

        ElseIf MyForm = "frmRptServicesReports" Then
            FormName = frmRptServicesReports

        ElseIf MyForm = "frmItemProgressReport" Then
            FormName = frmItemProgressReport

        ElseIf MyForm = "frmItemWiseProgressUpto" Then
            FormName = frmItemWiseProgressUpto

            ' Services Reports

        ElseIf MyForm = "frmFreeServiceCardReport" Then
            FormName = frmFreeServiceCardReport

        ElseIf MyForm = "frmRepeateCustomerReport" Then
            FormName = frmRepeateCustomerReport

        ElseIf MyForm = "JobCardSalesReport" Then
            FormName = JobCardSalesReport

        ElseIf MyForm = "GroupWiseSalesReport" Then
            FormName = GroupWiseSalesReport

        ElseIf MyForm = "JobCardCommissionReport" Then
            FormName = JobCardCommissionReport

        ElseIf MyForm = "frmReturnablegatepass" Then
            FormName = frmReturnablegatepass

        ElseIf MyForm = "frmAverageRateUpdate" Then
            FormName = frmAverageRateUpdate

        ElseIf MyForm = "frmServicesDispatch" Then
            FormName = frmServicesDispatch

        ElseIf MyForm = "frmServicesProduction" Then
            FormName = frmServicesProduction

            'If MyForm = "frmServicesInwardGatePass" Then
            '    FormName = "frmServicesInwardGatePass"
            'End If




        ElseIf MyForm = "frmReturnablegatepass" Then
            FormName = frmReturnablegatepass

        ElseIf MyForm = "frmServicesInwardGatePass" Then
            FormName = "frmServicesInwardGatePass"




            'Sales Reports

        ElseIf MyForm = "frmGrdRptCustomersItemsSummarySales" Then
            FormName = frmGrdRptCustomersItemsSummarySales

        ElseIf MyForm = "frmRptCustomersSales" Then
            FormName = frmRptCustomersSales

        ElseIf MyForm = "frmGrdRptSalesByGender" Then
            FormName = frmGrdRptSalesByGender

        ElseIf MyForm = "frmGrdDispatchDetail" Then
            FormName = frmGrdDispatchDetail

        ElseIf MyForm = "frmGrdSalemansDemandDetail" Then
            FormName = frmGrdSalemansDemandDetail

        ElseIf MyForm = "DamageBudget" Then
            FormName = DamageBudget

        ElseIf MyForm = "frmGrdRptSectorSales" Then
            FormName = frmGrdRptSectorSales

        ElseIf MyForm = "DeliveryChallanDetail" Then
            FormName = DeliveryChallanDetail

        ElseIf MyForm = "DeliveryChallanSummary" Then
            FormName = DeliveryChallanSummary

        ElseIf MyForm = "frmrptRoutePlanGatePass" Then
            FormName = frmrptRoutePlanGatePass

        ElseIf MyForm = "frmRptDSRStatement" Then
            FormName = frmRptDSRStatement

        ElseIf MyForm = "frmGrdSalesmanCommissionDetail" Then
            FormName = frmGrdSalesmanCommissionDetail

        ElseIf MyForm = "NetSalesReport" Then
            FormName = NetSalesReport

        ElseIf MyForm = "frmGrdSalesSummary" Then
            FormName = frmGrdSalesSummary

        ElseIf MyForm = "frmGrdSalesReturnDetail" Then
            FormName = frmGrdSalesReturnDetail

        ElseIf MyForm = "frmSalesComparisonCustomerWise" Then
            FormName = frmSalesComparisonCustomerWise

        ElseIf MyForm = "frmGrdRptSalesComparison" Then
            FormName = frmGrdRptSalesComparison

        ElseIf MyForm = "SalesCertificateIssued" Then
            FormName = SalesCertificateIssued

        ElseIf MyForm = "frmGrdRptConsolidateItemSalesCustomerWise" Then
            FormName = frmGrdRptConsolidateItemSalesCustomerWise

        ElseIf MyForm = "frmGrdRptSalesCertificateIssued" Then
            FormName = frmGrdRptSalesCertificateIssued

        ElseIf MyForm = "frmGrdRptProductCustomerWiseReport" Then
            FormName = frmGrdRptProductCustomerWiseReport

        ElseIf MyForm = "frmGrdRptProductDateWiseReport" Then
            FormName = frmGrdRptProductDateWiseReport

        ElseIf MyForm = "frmGrdRptSalesSummaries" Then
            FormName = frmGrdRptSalesSummaries

        ElseIf MyForm = "frmGrdRptCustomerItemWiseSummary" Then
            FormName = frmGrdRptCustomerItemWiseSummary

        ElseIf MyForm = "frmGrdRptItemWiseSalesSummary" Then
            FormName = frmGrdRptItemWiseSalesSummary

        ElseIf MyForm = "frmGrdRptSalesPriceChange" Then
            FormName = frmGrdRptSalesPriceChange

        ElseIf MyForm = "rptAdvanceReceiptsSO" Then
            'FormName = rptAdvanceReceiptsSO

        ElseIf MyForm = "frmGrdRptCostSheetQtyWise" Then
            FormName = frmGrdRptCostSheetQtyWise

        ElseIf MyForm = "frmGrdRptSalesRegisterActivity" Then
            FormName = frmGrdRptSalesRegisterActivity

        ElseIf MyForm = "frmGrdRptSaleInvoicesDue" Then
            FormName = frmGrdRptSaleInvoicesDue

        ElseIf MyForm = "frmRptSalesCertificateLedger" Then
            FormName = frmRptSalesCertificateLedger

        ElseIf MyForm = "frmGrdRptRackWiseClosingStock" Then
            FormName = frmGrdRptRackWiseClosingStock

        ElseIf MyForm = "rptVoucher" Then
            FormName = rptVoucher

        ElseIf MyForm = "frmDSRStatementNew" Then
            FormName = frmDSRStatementNew

        ElseIf MyForm = "frmGrdRptInstallmentBalance" Then
            FormName = frmGrdRptInstallmentBalance

        ElseIf MyForm = "frmGrdRptCostSheetMarginCalculationDetail" Then
            FormName = frmGrdRptCostSheetMarginCalculationDetail

        ElseIf MyForm = "frmGrdRptTaxDuductionDetail" Then
            FormName = frmGrdRptTaxDuductionDetail

        ElseIf MyForm = "frmGrdRptEmployeeMonthlyTergetAchieved" Then
            FormName = frmGrdRptEmployeeMonthlyTergetAchieved

        ElseIf MyForm = "frmCustomerDiscounts" Then
            FormName = frmCustomerDiscounts

        ElseIf MyForm = "frmGrdRptEngineWiseStock" Then
            FormName = frmGrdRptEngineWiseStock

        ElseIf MyForm = "SummaryofSalesAndReturns" Then
            FormName = SummaryofSalesAndReturns

        ElseIf MyForm = "SummaryofSalesAndReturns" Then
            FormName = SummaryofSalesAndReturns

        ElseIf MyForm = "frmGrdRptSaleOrderStatusSummary" Then
            FormName = frmGrdRptSaleOrderStatusSummary

        ElseIf MyForm = "frmInvoiceWiseProfitReport" Then
            FormName = frmInvoiceWiseProfitReport

            ' Purchase Reports

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "frmRptGrdPurchaseDetailWithWeight" Then
            FormName = frmRptGrdPurchaseDetailWithWeight

        ElseIf MyForm = "frmGrdRptItemExpiryDateDetail" Then
            FormName = frmGrdRptItemExpiryDateDetail

        ElseIf MyForm = "frmGrdPurchaseSummary" Then
            FormName = frmGrdPurchaseSummary

        ElseIf MyForm = "rptPurchaseDailyWorkingReport" Then
            FormName = rptPurchaseDailyWorkingReport

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "frmRptMonthlyPurchaseSummary" Then
            FormName = frmRptMonthlyPurchaseSummary

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "frmGrdImportLedger" Then
            FormName = frmGrdImportLedger

        ElseIf MyForm = "frmGrdRptTaxDuductionDetail" Then
            FormName = frmGrdRptTaxDuductionDetail

        ElseIf MyForm = "frmGrdRptAgingPayables" Then
            FormName = frmGrdRptAgingPayables

        ElseIf MyForm = "frmGrdRptInvoiceAging" Then
            FormName = frmGrdRptInvoiceAging

        ElseIf MyForm = "SummaryofPurchasesAndReturns" Then
            FormName = SummaryofPurchasesAndReturns

        ElseIf MyForm = "frmGrdRptToOrderQty" Then
            FormName = frmGrdRptToOrderQty

        ElseIf MyForm = "frmGRNDetailReport" Then
            FormName = frmGRNDetailReport

        ElseIf MyForm = "frmRptPurchaseGRNRejectedQty" Then
            FormName = frmRptPurchaseGRNRejectedQty

        ElseIf MyForm = "frmRptAccountWisePurchaseReport" Then
            FormName = frmRptAccountWisePurchaseReport

            ' Inventory Reports

        ElseIf MyForm = "StoreIssuanceSummary" Then
            FormName = StoreIssuanceSummary

        ElseIf MyForm = "StoreIssuanceDetail" Then
            FormName = StoreIssuanceDetail

        ElseIf MyForm = "rptStockForm" Then
            FormName = rptStockForm

        ElseIf MyForm = "StockStatementByLPO" Then
            FormName = StockStatementByLPO

        ElseIf MyForm = "StockStatementWithSize" Then
            FormName = StockStatementWithSize

        ElseIf MyForm = "frmStockStatmentBySize" Then
            FormName = frmStockStatmentBySize

        ElseIf MyForm = "frmRptGrdStockStatement" Then
            FormName = frmRptGrdStockStatement

        ElseIf MyForm = "RptGridItemSalesHistory" Then
            FormName = RptGridItemSalesHistory

        ElseIf MyForm = "frmRptGrdStockInOutDetail" Then
            FormName = frmRptGrdStockInOutDetail

        ElseIf MyForm = "frmGrdRptLocationWiseStockLedger" Then
            FormName = frmGrdRptLocationWiseStockLedger

        ElseIf MyForm = "frmGrdRptStockStatementUnitWise" Then
            FormName = frmGrdRptStockStatementUnitWise

        ElseIf MyForm = "frmGrd_Prod_DC_WiseStock" Then
            FormName = frmGrd_Prod_DC_WiseStock

        ElseIf MyForm = "ListOfItems" Then
            'FormName = ListofItems

        ElseIf MyForm = "frmRptArticleBarcode" Then
            FormName = frmRptArticleBarcode

        ElseIf MyForm = "frmGrdCostSheetComparisonWithStock" Then
            FormName = frmGrdCostSheetComparisonWithStock

        ElseIf MyForm = "frmGrdPlanComparison" Then
            FormName = frmGrdPlanComparison

        ElseIf MyForm = "frmGrdArticleLedger" Then
            FormName = frmGrdArticleLedger
            'Changes added by Murtaza(12/30/2022)

        ElseIf MyForm = "frmRptRental" Then
            FormName = frmRptRental

        ElseIf MyForm = "frmGrdRptProjectWiseStockLedger" Then
            FormName = frmGrdRptProjectWiseStockLedger

        ElseIf MyForm = "frmGrdRptLocationWiseStockStatement" Then
            FormName = frmGrdRptLocationWiseStockStatement

        ElseIf MyForm = "frmGrdRptLocationWiseStockStatementNew" Then
            FormName = frmGrdRptLocationWiseStockStatementNew

        ElseIf MyForm = "frmRptLocationWiseClosingStock" Then
            FormName = frmRptLocationWiseClosingStock

        ElseIf MyForm = "StoreIssuanceDetailBatchWise" Then
            FormName = StoreIssuanceDetailBatchWise

        ElseIf MyForm = "WarrantyDetailReport" Then
            FormName = WarrantyDetailReport

        ElseIf MyForm = "DispatchStatus" Then
            FormName = DispatchStatus

        ElseIf MyForm = "frmGrdRptClosingStockByOrders" Then
            FormName = frmGrdRptClosingStockByOrders

        ElseIf MyForm = "frmItemBarCodePrinting" Then
            FormName = frmItemBarCodePrinting

        ElseIf MyForm = "frmGrdRptClosingStockByGRNnDC" Then
            FormName = frmGrdRptClosingStockByGRNnDC

        ElseIf MyForm = "frmGRNStockReport" Then
            FormName = frmGRNStockReport

        ElseIf MyForm = "frmGrdRptCostSheetPlanDetail" Then
            FormName = frmGrdRptCostSheetPlanDetail

        ElseIf MyForm = "frmRptGrdStockStatementByPack" Then
            FormName = frmRptGrdStockStatementByPack

        ElseIf MyForm = "frmTransferredStockReport" Then
            FormName = frmTransferredStockReport

        ElseIf MyForm = "frmLocationWiseStockReport" Then
            FormName = frmLocationWiseStockReport

            ' HR Reports

        ElseIf MyForm = "DailyAttendance" Then
            FormName = DailyAttendance

        ElseIf MyForm = "AttedanceSummary" Then
            FormName = AttedanceSummary

        ElseIf MyForm = "EmployeeAttendanceDetail" Then
            FormName = EmployeeAttendanceDetail

        ElseIf MyForm = "frmGrdRptEmployeeMonthlyAttendance" Then
            FormName = frmGrdRptEmployeeMonthlyAttendance

        ElseIf MyForm = "frmGrdRptEmployeeSalarySheetDetail" Then
            FormName = frmGrdRptEmployeeSalarySheetDetail

        ElseIf MyForm = "DailySalarySheet" Then
            FormName = DailySalarySheet

        ElseIf MyForm = "DailySalarySheetSummary" Then
            FormName = DailySalarySheetSummary

        ElseIf MyForm = "frmGrdRptGenerelEmployeeSalary" Then
            FormName = frmGrdRptGenerelEmployeeSalary

        ElseIf MyForm = "frmRptEmpSalarySheetDetail" Then
            FormName = frmRptEmpSalarySheetDetail

        ElseIf MyForm = "frmGrdRptAttendanceRegister" Then
            FormName = frmGrdRptAttendanceRegister

        ElseIf MyForm = "frmEmployeeOverTimeReport" Then
            FormName = frmEmployeeOverTimeReport

        ElseIf MyForm = "frmAttendanceStatusDetailReport" Then
            FormName = frmAttendanceStatusDetailReport

        ElseIf MyForm = "frmEmployeeStatusList" Then
            FormName = frmEmployeeStatusList

        ElseIf MyForm = "frmCashInLeaveReport" Then
            FormName = frmCashInLeaveReport

        ElseIf MyForm = "LateComingEmployee" Then
            FormName = LateComingEmployee

        ElseIf MyForm = "OverTimeEmployee" Then
            FormName = OverTimeEmployee

        ElseIf MyForm = "LateInTimeSummary" Then
            FormName = LateInTimeSummary

        ElseIf MyForm = "LateArrivalDays" Then
            FormName = LateArrivalDays

        ElseIf MyForm = "frmEmployeeBirthday" Then
            FormName = frmEmployeeBirthday

        ElseIf MyForm = "frmEmployeeCNICExpiry" Then
            FormName = frmEmployeeCNICExpiry

        ElseIf MyForm = "frmEmployeeNoOfHits" Then
            FormName = frmEmployeeNoOfHits

        ElseIf MyForm = "frmEmpAttendenceInOutMissing" Then
            FormName = frmEmpAttendenceInOutMissing

        ElseIf MyForm = "frmEmployeeWiseLedger" Then
            FormName = frmEmployeeWiseLedger

            'Payroll Reports

        ElseIf MyForm = "frmGrdRptEmployeeSalarySheetDetail" Then
            FormName = frmGrdRptEmployeeSalarySheetDetail

        ElseIf MyForm = "DailySalarySheet" Then
            FormName = DailySalarySheet

        ElseIf MyForm = "DailySalarySheetSummary" Then
            FormName = DailySalarySheetSummary

        ElseIf MyForm = "frmGrdRptGenerelEmployeeSalary" Then
            FormName = frmGrdRptGenerelEmployeeSalary

        ElseIf MyForm = "frmGrdRptEmployeeSalarySheetDetail" Then
            FormName = frmGrdRptEmployeeSalarySheetDetail

        ElseIf MyForm = "frmRptEmpSalarySheetDetail" Then
            FormName = frmRptEmpSalarySheetDetail

        ElseIf MyForm = "frmEmployeeOverTimeReport" Then
            FormName = frmEmployeeOverTimeReport

            'CRM Reports

        ElseIf MyForm = "RptDateRangeEmployees" Then
            FormName = RptDateRangeEmployees

        ElseIf MyForm = "frmRptTaskDetail" Then
            FormName = frmRptTaskDetail

        ElseIf MyForm = "frmRptProjectHistory" Then
            FormName = frmRptProjectHistory

        ElseIf MyForm = "frmGrdRptProjectVisitDetail" Then
            FormName = frmGrdRptProjectVisitDetail

        ElseIf MyForm = "rptTodayTasks" Then
            FormName = rptTodayTasks

            'PM Reports
        ElseIf MyForm = "frmStockComparisonReport" Then
            FormName = frmStockComparisonReport

        ElseIf MyForm = "frmItemProgressReport" Then
            FormName = frmItemProgressReport

        ElseIf MyForm = "frmItemWiseProgressUpto" Then
            FormName = frmItemWiseProgressUpto

            ' Services Reports

        ElseIf MyForm = "JobCardDetail" Then
            FormName = JobCardDetail

        ElseIf MyForm = "JobCardSummary" Then
            FormName = JobCardSummary

        ElseIf MyForm = "frmFreeServiceCardReport" Then
            FormName = frmFreeServiceCardReport

        ElseIf MyForm = "frmRepeateCustomerReport" Then
            FormName = frmRepeateCustomerReport

        ElseIf MyForm = "JobCardSalesReport" Then
            FormName = JobCardSalesReport

        ElseIf MyForm = "GroupWiseSalesReport" Then
            FormName = GroupWiseSalesReport

        ElseIf MyForm = "JobCardCommissionReport" Then
            FormName = JobCardCommissionReport

        ElseIf MyForm = "frmReturnablegatepass" Then
            FormName = frmReturnablegatepass

        ElseIf MyForm = "frmreminder" Then
            FormName = frmreminder

            'If MyForm = "frmServicesInwardGatePass" Then
            '    FormName = "frmServicesInwardGatePass"
            'End If




        ElseIf MyForm = "frmReturnablegatepass" Then
            FormName = frmReturnablegatepass

        ElseIf MyForm = "frmServicesInwardGatePass" Then
            FormName = "frmServicesInwardGatePass"



            'Sales Reports

        ElseIf MyForm = "frmGrdRptCustomersItemsSummarySales" Then
            FormName = frmGrdRptCustomersItemsSummarySales

        ElseIf MyForm = "frmRptCustomersSales" Then
            FormName = frmRptCustomersSales

        ElseIf MyForm = "frmGrdRptSalesByGender" Then
            FormName = frmGrdRptSalesByGender

        ElseIf MyForm = "frmGrdDispatchDetail" Then
            FormName = frmGrdDispatchDetail

        ElseIf MyForm = "frmGrdSalemansDemandDetail" Then
            FormName = frmGrdSalemansDemandDetail

        ElseIf MyForm = "DamageBudget" Then
            FormName = DamageBudget

        ElseIf MyForm = "frmGrdRptSectorSales" Then
            FormName = frmGrdRptSectorSales

        ElseIf MyForm = "DeliveryChallanDetail" Then
            FormName = DeliveryChallanDetail

        ElseIf MyForm = "DeliveryChallanSummary" Then
            FormName = DeliveryChallanSummary

        ElseIf MyForm = "frmrptRoutePlanGatePass" Then
            FormName = frmrptRoutePlanGatePass

        ElseIf MyForm = "frmRptDSRStatement" Then
            FormName = frmRptDSRStatement

        ElseIf MyForm = "frmGrdSalesmanCommissionDetail" Then
            FormName = frmGrdSalesmanCommissionDetail

        ElseIf MyForm = "NetSalesReport" Then
            FormName = NetSalesReport

        ElseIf MyForm = "frmGrdSalesSummary" Then
            FormName = frmGrdSalesSummary

        ElseIf MyForm = "frmGrdSalesReturnDetail" Then
            FormName = frmGrdSalesReturnDetail

        ElseIf MyForm = "frmSalesComparisonCustomerWise" Then
            FormName = frmSalesComparisonCustomerWise

        ElseIf MyForm = "frmGrdRptSalesComparison" Then
            FormName = frmGrdRptSalesComparison

        ElseIf MyForm = "SalesCertificateIssued" Then
            FormName = SalesCertificateIssued

        ElseIf MyForm = "frmGrdRptConsolidateItemSalesCustomerWise" Then
            FormName = frmGrdRptConsolidateItemSalesCustomerWise

        ElseIf MyForm = "frmGrdRptSalesCertificateIssued" Then
            FormName = frmGrdRptSalesCertificateIssued

        ElseIf MyForm = "frmGrdRptProductCustomerWiseReport" Then
            FormName = frmGrdRptProductCustomerWiseReport

        ElseIf MyForm = "frmGrdRptProductDateWiseReport" Then
            FormName = frmGrdRptProductDateWiseReport

        ElseIf MyForm = "frmGrdRptSalesSummaries" Then
            FormName = frmGrdRptSalesSummaries

        ElseIf MyForm = "frmGrdRptCustomerItemWiseSummary" Then
            FormName = frmGrdRptCustomerItemWiseSummary

        ElseIf MyForm = "frmGrdRptItemWiseSalesSummary" Then
            FormName = frmGrdRptItemWiseSalesSummary

        ElseIf MyForm = "frmGrdRptSalesPriceChange" Then
            FormName = frmGrdRptSalesPriceChange

        ElseIf MyForm = "rptAdvanceReceiptsSO" Then
            'FormName = rptAdvanceReceiptsSO

        ElseIf MyForm = "frmGrdRptCostSheetQtyWise" Then
            FormName = frmGrdRptCostSheetQtyWise

        ElseIf MyForm = "frmGrdRptSalesRegisterActivity" Then
            FormName = frmGrdRptSalesRegisterActivity

        ElseIf MyForm = "frmGrdRptSaleInvoicesDue" Then
            FormName = frmGrdRptSaleInvoicesDue

        ElseIf MyForm = "frmRptSalesCertificateLedger" Then
            FormName = frmRptSalesCertificateLedger

        ElseIf MyForm = "frmGrdRptRackWiseClosingStock" Then
            FormName = frmGrdRptRackWiseClosingStock

        ElseIf MyForm = "rptVoucher" Then
            FormName = rptVoucher

        ElseIf MyForm = "frmDSRStatementNew" Then
            FormName = frmDSRStatementNew

        ElseIf MyForm = "frmGrdRptInstallmentBalance" Then
            FormName = frmGrdRptInstallmentBalance

        ElseIf MyForm = "frmGrdRptCostSheetMarginCalculationDetail" Then
            FormName = frmGrdRptCostSheetMarginCalculationDetail

        ElseIf MyForm = "frmGrdRptTaxDuductionDetail" Then
            FormName = frmGrdRptTaxDuductionDetail

        ElseIf MyForm = "frmGrdRptEmployeeMonthlyTergetAchieved" Then
            FormName = frmGrdRptEmployeeMonthlyTergetAchieved

        ElseIf MyForm = "frmCustomerDiscounts" Then
            FormName = frmCustomerDiscounts

        ElseIf MyForm = "frmGrdRptEngineWiseStock" Then
            FormName = frmGrdRptEngineWiseStock

        ElseIf MyForm = "SummaryofSalesAndReturns" Then
            FormName = SummaryofSalesAndReturns

        ElseIf MyForm = "SummaryofSalesAndReturns" Then
            FormName = SummaryofSalesAndReturns

        ElseIf MyForm = "frmGrdRptSaleOrderStatusSummary" Then
            FormName = frmGrdRptSaleOrderStatusSummary

        ElseIf MyForm = "frmInvoiceWiseProfitReport" Then
            FormName = frmInvoiceWiseProfitReport

            ' Purchase Reports

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "frmRptGrdPurchaseDetailWithWeight" Then
            FormName = frmRptGrdPurchaseDetailWithWeight

        ElseIf MyForm = "frmGrdRptItemExpiryDateDetail" Then
            FormName = frmGrdRptItemExpiryDateDetail

        ElseIf MyForm = "frmGrdPurchaseSummary" Then
            FormName = frmGrdPurchaseSummary

        ElseIf MyForm = "rptPurchaseDailyWorkingReport" Then
            FormName = rptPurchaseDailyWorkingReport

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "frmRptMonthlyPurchaseSummary" Then
            FormName = frmRptMonthlyPurchaseSummary

        ElseIf MyForm = "rptDateRange" Then
            FormName = rptDateRange

        ElseIf MyForm = "frmGrdImportLedger" Then
            FormName = frmGrdImportLedger

        ElseIf MyForm = "frmGrdRptTaxDuductionDetail" Then
            FormName = frmGrdRptTaxDuductionDetail

        ElseIf MyForm = "frmGrdRptAgingPayables" Then
            FormName = frmGrdRptAgingPayables

        ElseIf MyForm = "frmGrdRptInvoiceAging" Then
            FormName = frmGrdRptInvoiceAging

        ElseIf MyForm = "SummaryofPurchasesAndReturns" Then
            FormName = SummaryofPurchasesAndReturns

        ElseIf MyForm = "frmGrdRptToOrderQty" Then
            FormName = frmGrdRptToOrderQty

        ElseIf MyForm = "frmGRNDetailReport" Then
            FormName = frmGRNDetailReport

        ElseIf MyForm = "frmRptPurchaseGRNRejectedQty" Then
            FormName = frmRptPurchaseGRNRejectedQty

        ElseIf MyForm = "frmRptAccountWisePurchaseReport" Then
            FormName = frmRptAccountWisePurchaseReport


            'Lead Profile here
        ElseIf MyForm = "frmLeadProfileList2" Then
            FormName = frmLeadProfileList2

        ElseIf MyForm = "frmOpportunityList" Then
            FormName = frmOpportunityList

            ' Inventory Reports

        ElseIf MyForm = "StoreIssuanceSummary" Then
            FormName = StoreIssuanceSummary

        ElseIf MyForm = "StoreIssuanceDetail" Then
            FormName = StoreIssuanceDetail

        ElseIf MyForm = "rptStockForm" Then
            FormName = rptStockForm

        ElseIf MyForm = "StockStatementByLPO" Then
            FormName = StockStatementByLPO

        ElseIf MyForm = "StockStatementWithSize" Then
            FormName = StockStatementWithSize

        ElseIf MyForm = "frmStockStatmentBySize" Then
            FormName = frmStockStatmentBySize

        ElseIf MyForm = "frmRptGrdStockStatement" Then
            FormName = frmRptGrdStockStatement

        ElseIf MyForm = "RptGridItemSalesHistory" Then
            FormName = RptGridItemSalesHistory

        ElseIf MyForm = "frmRptGrdStockInOutDetail" Then
            FormName = frmRptGrdStockInOutDetail

        ElseIf MyForm = "frmGrdRptLocationWiseStockLedger" Then
            FormName = frmGrdRptLocationWiseStockLedger

        ElseIf MyForm = "frmGrdRptStockStatementUnitWise" Then
            FormName = frmGrdRptStockStatementUnitWise

        ElseIf MyForm = "frmGrd_Prod_DC_WiseStock" Then
            FormName = frmGrd_Prod_DC_WiseStock

        ElseIf MyForm = "ListOfItems" Then
            'FormName = ListofItems

        ElseIf MyForm = "frmRptArticleBarcode" Then
            FormName = frmRptArticleBarcode

        ElseIf MyForm = "frmGrdCostSheetComparisonWithStock" Then
            FormName = frmGrdCostSheetComparisonWithStock

        ElseIf MyForm = "frmGrdPlanComparison" Then
            FormName = frmGrdPlanComparison

        ElseIf MyForm = "frmGrdArticleLedger" Then
            FormName = frmGrdArticleLedger
            'Changes added by Murtaza(12/30/2022)
        ElseIf MyForm = "frmGrdStockMovement" Then
            FormName = frmGrdStockMovement
            'Changes added by Murtaza(12/30/2022)

        ElseIf MyForm = "frmRptRental" Then
            FormName = frmRptRental

        ElseIf MyForm = "frmGrdRptProjectWiseStockLedger" Then
            FormName = frmGrdRptProjectWiseStockLedger

        ElseIf MyForm = "frmGrdRptLocationWiseStockStatement" Then
            FormName = frmGrdRptLocationWiseStockStatement

        ElseIf MyForm = "frmGrdRptLocationWiseStockStatementNew" Then
            FormName = frmGrdRptLocationWiseStockStatementNew

        ElseIf MyForm = "frmRptLocationWiseClosingStock" Then
            FormName = frmRptLocationWiseClosingStock

        ElseIf MyForm = "StoreIssuanceDetailBatchWise" Then
            FormName = StoreIssuanceDetailBatchWise

        ElseIf MyForm = "WarrantyDetailReport" Then
            FormName = WarrantyDetailReport

        ElseIf MyForm = "DispatchStatus" Then
            FormName = DispatchStatus

        ElseIf MyForm = "frmGrdRptClosingStockByOrders" Then
            FormName = frmGrdRptClosingStockByOrders

        ElseIf MyForm = "frmItemBarCodePrinting" Then
            FormName = frmItemBarCodePrinting

        ElseIf MyForm = "frmGrdRptClosingStockByGRNnDC" Then
            FormName = frmGrdRptClosingStockByGRNnDC

        ElseIf MyForm = "frmGRNStockReport" Then
            FormName = frmGRNStockReport

        ElseIf MyForm = "frmGrdRptCostSheetPlanDetail" Then
            FormName = frmGrdRptCostSheetPlanDetail

        ElseIf MyForm = "frmRptGrdStockStatementByPack" Then
            FormName = frmRptGrdStockStatementByPack

        ElseIf MyForm = "frmTransferredStockReport" Then
            FormName = frmTransferredStockReport

        ElseIf MyForm = "frmLocationWiseStockReport" Then
            FormName = frmLocationWiseStockReport

            ' HR Reports

        ElseIf MyForm = "DailyAttendance" Then
            FormName = DailyAttendance

        ElseIf MyForm = "AttedanceSummary" Then
            FormName = AttedanceSummary

        ElseIf MyForm = "EmployeeAttendanceDetail" Then
            FormName = EmployeeAttendanceDetail

        ElseIf MyForm = "frmGrdRptEmployeeMonthlyAttendance" Then
            FormName = frmGrdRptEmployeeMonthlyAttendance

        ElseIf MyForm = "frmGrdRptEmployeeSalarySheetDetail" Then
            FormName = frmGrdRptEmployeeSalarySheetDetail

        ElseIf MyForm = "DailySalarySheet" Then
            FormName = DailySalarySheet

        ElseIf MyForm = "DailySalarySheetSummary" Then
            FormName = DailySalarySheetSummary

        ElseIf MyForm = "frmGrdRptGenerelEmployeeSalary" Then
            FormName = frmGrdRptGenerelEmployeeSalary

        ElseIf MyForm = "frmRptEmpSalarySheetDetail" Then
            FormName = frmRptEmpSalarySheetDetail

        ElseIf MyForm = "frmGrdRptAttendanceRegister" Then
            FormName = frmGrdRptAttendanceRegister

        ElseIf MyForm = "frmEmployeeOverTimeReport" Then
            FormName = frmEmployeeOverTimeReport

        ElseIf MyForm = "frmAttendanceStatusDetailReport" Then
            FormName = frmAttendanceStatusDetailReport

        ElseIf MyForm = "frmEmployeeStatusList" Then
            FormName = frmEmployeeStatusList

        ElseIf MyForm = "frmCashInLeaveReport" Then
            FormName = frmCashInLeaveReport

        ElseIf MyForm = "LateComingEmployee" Then
            FormName = LateComingEmployee

        ElseIf MyForm = "OverTimeEmployee" Then
            FormName = OverTimeEmployee

        ElseIf MyForm = "LateInTimeSummary" Then
            FormName = LateInTimeSummary

        ElseIf MyForm = "LateArrivalDays" Then
            FormName = LateArrivalDays

        ElseIf MyForm = "frmEmployeeBirthday" Then
            FormName = frmEmployeeBirthday

        ElseIf MyForm = "frmEmployeeCNICExpiry" Then
            FormName = frmEmployeeCNICExpiry

        ElseIf MyForm = "frmEmployeeNoOfHits" Then
            FormName = frmEmployeeNoOfHits

        ElseIf MyForm = "frmEmpAttendenceInOutMissing" Then
            FormName = frmEmpAttendenceInOutMissing

        ElseIf MyForm = "frmEmployeeWiseLedger" Then
            FormName = frmEmployeeWiseLedger

            'Assets Reports

        ElseIf MyForm = "frmAssetsAndLiabilityReport" Then
            FormName = frmAssetsAndLiabilityReport

        ElseIf MyForm = "frmDepriciation" Then
            FormName = frmDepriciation

        ElseIf MyForm = "frmGrdRptAssetsDetail" Then
            FormName = frmGrdRptAssetsDetail
            'Payroll Reports

        ElseIf MyForm = "frmGrdRptEmployeeSalarySheetDetail" Then
            FormName = frmGrdRptEmployeeSalarySheetDetail

        ElseIf MyForm = "DailySalarySheet" Then
            FormName = DailySalarySheet

        ElseIf MyForm = "DailySalarySheetSummary" Then
            FormName = DailySalarySheetSummary

        ElseIf MyForm = "frmGrdRptGenerelEmployeeSalary" Then
            FormName = frmGrdRptGenerelEmployeeSalary

        ElseIf MyForm = "frmGrdRptEmployeeSalarySheetDetail" Then
            FormName = frmGrdRptEmployeeSalarySheetDetail

        ElseIf MyForm = "frmRptEmpSalarySheetDetail" Then
            FormName = frmRptEmpSalarySheetDetail

        ElseIf MyForm = "frmEmployeeOverTimeReport" Then
            FormName = frmEmployeeOverTimeReport

        ElseIf MyForm = "frmRptLedgerNew" Then
            FormName = frmRptLedgerNew

        ElseIf MyForm = "frmRptTrialNew" Then
            FormName = frmRptTrialNew
            'CRM Reports

        ElseIf MyForm = "RptDateRangeEmployees" Then
            FormName = RptDateRangeEmployees

        ElseIf MyForm = "frmRptTaskDetail" Then
            FormName = frmRptTaskDetail

        ElseIf MyForm = "frmRptProjectHistory" Then
            FormName = frmRptProjectHistory

        ElseIf MyForm = "frmGrdRptProjectVisitDetail" Then
            FormName = frmGrdRptProjectVisitDetail

        ElseIf MyForm = "rptTodayTasks" Then
            FormName = rptTodayTasks

            'PM Reports


            'PM Module Here

        ElseIf MyForm = "frmCustomerContractList" Then
            FormName = frmCustomerContractList

        ElseIf MyForm = "frmCustomerProjectProgressApprovalList" Then
            FormName = frmCustomerProjectProgressApprovalList

        ElseIf MyForm = "frmInterimPaymentCertificateList" Then
            FormName = frmInterimPaymentCertificateList

        ElseIf MyForm = "frmItemProgressReport" Then
            FormName = frmItemProgressReport

        ElseIf MyForm = "frmItemWiseProgressUpto" Then
            FormName = frmItemWiseProgressUpto

        ElseIf MyForm = "frmProductionTicketsView" Then
            FormName = frmProductionTicketsView

            ' Services Reports

        ElseIf MyForm = "JobCardDetail" Then
            FormName = JobCardDetail

        ElseIf MyForm = "JobCardSummary" Then
            FormName = JobCardSummary

        ElseIf MyForm = "frmFreeServiceCardReport" Then
            FormName = frmFreeServiceCardReport

        ElseIf MyForm = "frmRepeateCustomerReport" Then
            FormName = frmRepeateCustomerReport

        ElseIf MyForm = "JobCardSalesReport" Then
            FormName = JobCardSalesReport

        ElseIf MyForm = "GroupWiseSalesReport" Then
            FormName = GroupWiseSalesReport

        ElseIf MyForm = "JobCardCommissionReport" Then
            FormName = JobCardCommissionReport

        ElseIf MyForm = "frmLiftWisePercentageReport" Then
            FormName = frmLiftWisePercentageReport

        ElseIf MyForm = "frmGrdRptAgingPayables" Then
            FormName = frmGrdRptAgingPayables

        ElseIf MyForm = "frmServicesInwardGatePass" Then
            FormName = "frmServicesInwardGatePass"


        ElseIf MyForm = "frmGrdRptAgingPayables" Then
            FormName = frmGrdRptAgingPayables

        ElseIf MyForm = "frmGrdRptAgingReceiveables" Then
            FormName = frmGrdRptAgingReceiveables

        ElseIf MyForm = "frmGrdRptPostDatedCheques" Then
            FormName = frmGrdRptPostDatedCheques

        ElseIf MyForm = "frmCashRecoveryReport" Then
            FormName = frmCashRecoveryReport

        ElseIf MyForm = "CashReceiptDetailAgainstEmployee" Then
            FormName = CashReceiptDetailAgainstEmployee

        ElseIf MyForm = "frmRptGrdAdvances" Then
            FormName = frmRptGrdAdvances

        ElseIf MyForm = "frmRptInvoiceAgingFormated" Then
            FormName = frmRptInvoiceAgingFormated

        ElseIf MyForm = "frmGrdRptInvoiceAging" Then
            FormName = frmGrdRptInvoiceAging

        ElseIf MyForm = "FrmVoucherCheckList" Then
            FormName = FrmVoucherCheckList

        ElseIf MyForm = "frmRptDirectorDebitors" Then
            FormName = frmRptDirectorDebitors

        ElseIf MyForm = "frmGrdRptPostDatedChequesSummary" Then
            FormName = frmGrdRptPostDatedChequesSummary

        ElseIf MyForm = "frmGrdLCDetail" Then
            FormName = frmGrdLCDetail

        ElseIf MyForm = "WithHoldingTaxCertificate" Then
            FormName = WithHoldingTaxCertificate

        ElseIf MyForm = "frmCashRecoveryReport" Then
            FormName = frmCashRecoveryReport

        ElseIf MyForm = "PLNotesDetail" Then
            FormName = PLNotesDetail

        ElseIf MyForm = "frmProfitAndLoss" Then
            FormName = frmProfitAndLoss

        ElseIf MyForm = "rptPLComparison" Then
            Dim rptPLComparison As New rptPLComparison
            rptPLComparison.ReportName = rptPLComparison.ReportList.PLComparison
            FormName = rptPLComparison
            enm = EnumForms.rptPLComparison

        ElseIf MyForm = "rptPLAcDetailComparison" Then
            Dim rptPLComparison As New rptPLComparison
            rptPLComparison.ReportName = rptPLComparison.ReportList.PLComparisonDetailAccount
            FormName = PLComparisonDetailAccount
            enm = EnumForms.PLComparisonDetailAccount

        ElseIf MyForm = "rptPLAcHeadComparison" Then
            Dim rptPLComparison As New rptPLComparison
            rptPLComparison.ReportName = rptPLComparison.ReportList.PLComparisonSubSubAccount
            FormName = PLComparisonSubSubAccount
            enm = EnumForms.PLComparisonSubSubAccount

        ElseIf MyForm = "frmRptGrdPLCostCenter" Then
            FormName = frmRptGrdPLCostCenter

        ElseIf MyForm = "PLSubSubAccountSummary" Then
            FormName = PLSubSubAccountSummary

        ElseIf MyForm = "PLDetailAccountSummary" Then
            FormName = PLDetailAccountSummary

        ElseIf MyForm = "frmBalanceSheet" Then
            FormName = frmBalanceSheet

        ElseIf MyForm = "BalanceSheetNotesSummary" Then
            FormName = BalanceSheetNotesSummary

        ElseIf MyForm = "FrmGridRptNonIntractCustomers" Then
            FormName = FrmGridRptNonIntractCustomers

        ElseIf MyForm = "frmRptProjectBasedTransactionDetail" Then
            FormName = frmRptProjectBasedTransactionDetail

        ElseIf MyForm = "frmRptDailyWorkingReport" Then
            FormName = frmRptDailyWorkingReport

        ElseIf MyForm = "frmBankTypeWiseCashFlow" Then
            FormName = frmBankTypeWiseCashFlow

        ElseIf MyForm = "frmAgingPayablesNew" Then
            FormName = frmAgingPayablesNew

        ElseIf MyForm = "frmPLSubSubAccountWiseSummary" Then
            FormName = frmPLSubSubAccountWiseSummary

        ElseIf MyForm = "frmPLSubSubAccountCostCenterWiseSummary" Then
            FormName = frmPLSubSubAccountCostCenterWiseSummary

        ElseIf MyForm = "frmBSSubSubAccountSummary" Then
            FormName = frmBSSubSubAccountSummary

        ElseIf MyForm = "frmPLAccountGroupWiseSummary" Then
            FormName = frmPLAccountGroupWiseSummary

        ElseIf MyForm = "frmBSAccountGroupWiseSummary" Then
            FormName = frmBSAccountGroupWiseSummary

        ElseIf MyForm = "frmAcctualVsBudgetedPLReport" Then
            FormName = frmAcctualVsBudgetedPLReport

        ElseIf MyForm = "frmInvoiceAgingNew" Then
            FormName = frmInvoiceAgingNew

        ElseIf MyForm = "frmAssetsAndLiabilityReport" Then
            FormName = frmAssetsAndLiabilityReport

        ElseIf MyForm = "CashFlowStatement" Then
            FormName = CashFlowStatement

        ElseIf MyForm = "CashFlowStatementStandard" Then
            FormName = CashFlowStatementStandard

            ' Property Module Here ...

        ElseIf MyForm = "frmGrdRptAgingPayables" Then
            FormName = frmGrdRptAgingPayables

        ElseIf MyForm = "frmGrdRptAgingReceiveables" Then
            FormName = frmGrdRptAgingReceiveables

        ElseIf MyForm = "frmRptCashDetail" Then
            FormName = frmRptCashDetail


        ElseIf MyForm = "InvoiceBasedPaymentSummaryReport" Then
            FormName = InvoiceBasedPaymentSummaryReport

        ElseIf MyForm = "frmPLAccountGroupWiseSummary" Then
            FormName = frmPLAccountGroupWiseSummary

        ElseIf MyForm = "SummaryofSalesInvoices" Then
            FormName = SummaryofSalesInvoices

        ElseIf MyForm = "SummaryofSalesTaxInvoices" Then
            FormName = SummaryofSalesTaxInvoices

        ElseIf MyForm = "SummaryofSalesInvoicesReturn" Then
            FormName = SummaryofSalesInvoicesReturn

        ElseIf MyForm = "rptCategoryWiseSaleReport" Then
            FormName = rptCategoryWiseSaleReport

        ElseIf MyForm = "rptItemWiseSales" Then
            FormName = rptItemWiseSales

        ElseIf MyForm = "frmRptEnhancementNew" Then
            frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.CashAccounting
            FormName = frmRptEnhancementNew


            'Help Module'

        ElseIf MyForm = "rptEmployeeBarcodeSticker" Then
            ShowReport("rptEmployeeBarcodeSticker", , , , , , , GetEmployeeBarcodeStickerData)

        ElseIf MyForm = "frmReleaseDownload" Then
            FormName = frmReleaseDownload

        ElseIf MyForm = "frmCompanyInformation" Then
            FormName = frmCompanyInformation

        ElseIf MyForm = "frmDefTransporter" Then
            FormName = frmDefTransporter

        ElseIf MyForm = "frmReGenerateCGV" Then
            FormName = frmReGenerateCGV

        ElseIf MyForm = "frmNotificationUtility" Then
            FormName = frmNotificationUtility


        ElseIf MyForm = "rptSalesmanMonthlySalesReport" Then
            FormName = rptSalesmanMonthlySalesReport

        ElseIf MyForm = "rptDailyWorkingReport" Then
            FormName = rptDailyWorkingReport

        ElseIf MyForm = "rptGrdEachDaysWorking" Then
            FormName = rptGrdEachDaysWorking

        ElseIf MyForm = "frmGrdRptAgingReceiveables" Then
            FormName = frmGrdRptAgingReceiveables

        ElseIf MyForm = "frmGrdDailySupply" Then
            FormName = frmGrdDailySupply

        ElseIf MyForm = "DemandSales" Then
            FormName = DemandSales

        ElseIf MyForm = "WeightReport" Then
            FormName = WeightReport

            ' Property Module Here ...

        ElseIf MyForm = "frmRptGrdMinMaxPriceSalesDetail" Then
            FormName = frmRptGrdMinMaxPriceSalesDetail

        ElseIf MyForm = "frmGrdRptSalesHistory" Then
            FormName = frmGrdRptSalesHistory

        ElseIf MyForm = "frmGrdRptCustomersWiseSummarySalesChart" Then
            FormName = frmGrdRptCustomersWiseSummarySalesChart

        ElseIf MyForm = "frmGrdRptCustomersItemsSummarySales" Then
            FormName = frmGrdRptCustomersItemsSummarySales

        ElseIf MyForm = "frmGrdRptCustomersWiseSummarySalesChart" Then
            FormName = frmGrdRptCustomersWiseSummarySalesChart

        ElseIf MyForm = "frmRptCustomersSales" Then
            FormName = frmRptCustomersSales

        ElseIf MyForm = "frmGrdRptSalesByGender" Then
            FormName = frmGrdRptSalesByGender

        ElseIf MyForm = "SummaryofSalesInvoicesReturn" Then
            FormName = SummaryofSalesInvoicesReturn

        ElseIf MyForm = "rptCategoryWiseSaleReport" Then
            FormName = rptCategoryWiseSaleReport

        ElseIf MyForm = "rptItemWiseSales" Then
            FormName = rptItemWiseSales

        ElseIf MyForm = "rptItemWiseSalesReturn" Then
            FormName = rptItemWiseSalesReturn

            ' Property Module Here ...

        ElseIf MyForm = "rptSalesManTarget" Then
            FormName = rptSalesManTarget

        ElseIf MyForm = "rptInventoryForm" Then
            FormName = rptInventoryForm

        ElseIf MyForm = "rptSalesChart" Then
            FormName = rptSalesChart

        ElseIf MyForm = "rptLedger" Then
            FormName = rptLedger

        ElseIf MyForm = "frmEmployeeWiseLedger" Then
            FormName = frmEmployeeWiseLedger

        ElseIf MyForm = "rptTrialBalance" Then
            FormName = rptTrialBalance

        ElseIf MyForm = "frmGrdRptEmployeeSalarySheetDetail" Then
            FormName = frmGrdRptEmployeeSalarySheetDetail

        ElseIf MyForm = "frmApprovalLog" Then
            FormName = frmApprovalLog

        ElseIf MyForm = "frmApprovalStages" Then
            FormName = frmApprovalStages

        ElseIf MyForm = "frmApprovalProcess" Then
            FormName = frmApprovalProcess

        ElseIf MyForm = "frmApprovalRejectionReason" Then
            FormName = frmApprovalRejectionReason

        ElseIf MyForm = "frmApprovalStagesMapping" Then
            FormName = frmApprovalStagesMapping

        ElseIf MyForm = "ChangePassword" Then
            FormName = ChangePassword

        ElseIf MyForm = "frmProSalaryList" Then
            FormName = frmProSalaryList

        ElseIf MyForm = "frmPartialReceiptGatePass" Then
            FormName = frmPartialReceiptGatePass

        ElseIf MyForm = "rptProftAndLossStatement" Then
            ShowReport("rptProftAndLossStatement")

        ElseIf MyForm = "rptsales" Then
            ShowReport("rptsales")

        ElseIf MyForm = "frmGrdRptDemandDetail" Then
            ShowReport("frmGrdRptDemandDetail")

        ElseIf MyForm = ("SumOfPurInv") Then
            rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfPurchaseInvoices
            rptDateRange.IsVendor = True 'Task:2609 Set Satus 
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Function

        ElseIf MyForm = ("rptPurchasereturn") Then
            rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfPurchaseReturn
            rptDateRange.IsVendor = True 'Task:2609 Set Satus 
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Function

        ElseIf MyForm = "rptPurchaseItemSummary" Then
            rptDateRange.ReportName = rptDateRange.ReportList.PurchaseItemSummary
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Function

            'ElseIf MyForm = "frmConfigMain" Then
            '    FormName = frmConfigMain

            ''Aashir_TFS3278_Added missing screen in search menu

        ElseIf FormName = "frmAcctualVsBudgetedCategoryWisePL" Then
            FormName = frmAcctualVsBudgetedCategoryWisePL

        ElseIf FormName = "frmDefSecurityUser" Then
            FormName = frmDefSecurityUser
            enm = EnumForms.Non

        ElseIf FormName = "frmInwardGatePass" Then
            FormName = frmIGP

        ElseIf FormName = "frmRptGrdInwardgatepass" Then
            'ApplyStyleSheet(frmRptGrdInwardgatepass)
            FormName = frmRptGrdInwardgatepass

        ElseIf FormName = "frmDefSecurityGroup" Then
            FormName = frmDefSecurityGroup
            enm = EnumForms.frmDefSecurityGroup
            Me.Cursor = Cursors.WaitCursor

        ElseIf FormName = "frmCashRequest" Then
            FormName = frmCashrequest

        ElseIf FormName = "VendorPayments" Or FormName = "frmPaymentNew" Then
            Dim payment As String = getConfigValueByType("NewPaymentForm")
            If payment = "True" Then
                FormName = frmPaymentNew
                enm = EnumForms.frmVendorPayment
                CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
                If Tags.Length > 0 Then
                    frmPaymentNew.Get_All(Tags)
                End If
            Else
                FormName = frmVendorPayment
                enm = EnumForms.frmVendorPayment
                CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
                If Tags.Length > 0 Then
                    frmVendorPayment.Get_All(Tags)
                End If
            End If

        ElseIf FormName = "frmYearlySaleTarget" Then
            FormName = frmYearlySaleTarget
            enm = EnumForms.frmYearlySaleTarget

        ElseIf FormName = "CompanyAndConnectionInfo" Then
            FormName = CompanyAndConnectionInfo
            enm = EnumForms.frmDefUser

        ElseIf FormName = "frmDefCommissionBySaleman" Then
            FormName = frmDefCommissionBySaleman

        ElseIf FormName = "frmEmployeeCards" Then
            FormName = frmEmployeeCards
            enm = EnumForms.Non



        ElseIf FormName = "ActivityLog" Then
            FormName = frmActivityLog

        ElseIf FormName = "frmDefAllocateShiftSchedule" Then
            FormName = frmDefAllocateShiftSchedule

        ElseIf FormName = "frmEmployeeVisitPlanEntry" Then
            FormName = frmEmployeeVisitPlanEntry

        ElseIf FormName = "frmFineDeduction" Then
            FormName = frmFineDeduction

        ElseIf FormName = "frmCmfa" Then
            'ApplyStyleSheet(frmCMFAAll)
            frmCMFAAll.ShowDialog()
            Exit Function

        ElseIf FormName = "frmCustomerPlanning" Then
            FormName = frmCustomerPlanning
            enm = EnumForms.frmCustomerPlanning

        ElseIf FormName = "frmMaterialDecomposition" Then
            FormName = frmMaterialDecomposition


        ElseIf FormName = "AddVendor" Then
            ''Task:2830 Apply Security Add New Vendor
            'If Not LoginGroup.ToString = "Administrator" Then
            '    FormName.Name = "AddVendor"
            '    Rights = GroupRights.FindAll(AddressOf ReturnRights)
            '    If Rights.Count = 0 Then Exit Function
            'End If
            ''End Task:2830
            'Dim CustId As Integer = 0
            'FrmAddCustomers.FormType = "Vendor"
            'FrmAddCustomers.ShowDialog()
            FormName = FrmAddCustomers
            FrmAddCustomers.Close()
            SetAddCustomersForm("Vendor", "Add New Vendor")
            FormName.Name = "AddVendor"
            enm = enmForms.frmDefVendor
        ElseIf FormName = "RptCustomerSalesAnlysis" Then
            FormName = frmRptCustomersSales
            enm = EnumForms.frmInventoryLevel

        ElseIf FormName = "Customer" Then
            FormName = Customer
            enm = EnumForms.frmVoucher

        ElseIf FormName = "frmgrdrptDailyUpdate" Then
            FormName = frmgrdrptDailyUpdate

        ElseIf FormName = "frmProductionPlanningStandard" Then
            FormName = frmProductionPlanningStandard

        ElseIf FormName = "frmGrdRptEmployeeSalarySheet" Then
            FormName = frmGrdRptEmployeeSalarySheet

        ElseIf FormName = "frmEmployeeWiseMonthlySale" Then
            FormName = frmEmployeeWiseMonthlySale
            enm = EnumForms.Non

        ElseIf FormName = "Employee" Then
            FormName = Employee
            enm = EnumForms.frmVoucher

        ElseIf FormName = "frmRptGraphs" Then
            FormName = frmRptGraphs
            enm = EnumForms.Non

        ElseIf FormName = "frmHome" Then
            'ApplyStyleSheet(frmHome)
            FormName = frmHome

        ElseIf FormName = "frmRptLedgerNew" Then
            FormName = frmRptLedgerNew

        ElseIf FormName = "rptInventoryLevelComparison" Then
            FormName = rptInventoryLevelComparison
            enm = EnumForms.rtpInventoryLevel

        ElseIf FormName = "frmGrdRptChart" Then
            FormName = frmGrdRptCharts
            enm = EnumForms.Non

        ElseIf FormName = "frmGrdRptOrdersDetail" Then
            FormName = frmGrdRptOrdersDetail

        ElseIf FormName = "frmGrdRptPostDatedChequeSummary" Then
            FormName = frmGrdRptPostDatedChequesSummary

        ElseIf FormName = "rptPriceChangeReport" Then
            FormName = rptPriceChangeReport
            enm = EnumForms.rptPriceChangeReport

        ElseIf FormName = "frmRptGrdPurchaseDetailWithWeight" Then
            FormName = frmRptGrdPurchaseDetailWithWeight
            '''
        ElseIf FormName = "PLComparisonDetailAccount" Then
            Dim rptPLComparison As New rptPLComparison
            rptPLComparison.ReportName = rptPLComparison.ReportList.PLComparisonDetailAccount
            'ApplyStyleSheet(rptPLComparison)
            'rptPLComparison.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 07-Feb-2017
            FormName = PLComparisonDetailAccount
            enm = EnumForms.PLComparisonDetailAccount
        ElseIf FormName = "PLComparisonSubSubAccount" Then
            Dim rptPLComparison As New rptPLComparison
            rptPLComparison.ReportName = rptPLComparison.ReportList.PLComparisonSubSubAccount
            'ApplyStyleSheet(rptPLComparison)
            'rptPLComparison.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 07-Feb-2017
            FormName = PLComparisonSubSubAccount
            enm = EnumForms.PLComparisonSubSubAccount

        ElseIf FormName = "frmApprovalStages" Then
            FormName = frmApprovalStages

        ElseIf FormName = "frmTermsandConditions" Then
            FormName = frmTermsandConditions

        ElseIf FormName = "frmLeadProfileList" Then
            FormName = frmLeadProfileList

        ElseIf FormName = "frmProjectList" Then
            FormName = frmProjectList

        ElseIf FormName = "frmAutoSalaryGenerate" Then
            FormName = frmAutoSalaryGenerate

        ElseIf FormName = "frmConsumptionEstimationReport" Then
            FormName = frmConsumptionEstimationReport

        ElseIf FormName = "frmIssuanceConsumptionReport" Then
            FormName = frmIssuanceConsumptionReport

        ElseIf FormName = "frmPlanTickets" Then
            FormName = frmPlanTickets

        ElseIf FormName = "frmTicketTracking" Then
            FormName = frmTicketTracking

        ElseIf FormName = "frmGrdRptCustomerWiseCashRecovery" Then
            FormName = frmGrdRptCustomerWiseCashRecovery

        ElseIf FormName = "frmGrdRptRackWiseClosingStock" Then
            FormName = frmGrdRptRackWiseClosingStock

        ElseIf FormName = "Purchase" Then
            FormName = frmPurchaseNew
            enm = EnumForms.frmPurchase
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Tags.Length > 0 Then
                frmPurchaseNew.Get_All(Tags)
                'Tags = String.Empty
            End If

        ElseIf FormName = "frmUserWiseCustomer" Then
            FormName = frmUserWiseCustomerList

        ElseIf FormName = "CostCenter" Then
            FormName = frmCostCenter
            enm = EnumForms.DefMainAcc
            If Tags.Length > 0 Then
                frmCostCenter.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf FormName = "frmProductionProcessing" Then
            FormName = frmServicesProduction

        ElseIf FormName = "Expense" Then
            FormName = frmExpense
            enm = EnumForms.frmExpense
            If Tags.Length > 0 Then
                frmExpense.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf FormName = "frmIncomeTaxOrSalesTaxAccount" Then
            FormName = frmIncomeTaxOrSalesTaxAccount

        ElseIf FormName = "frmDefCRMProject" Then
            FormName = frmDefCRMProject

        ElseIf FormName = "frmDialog" Then
            FormName = frmDialog

        ElseIf FormName = "IssuanceHistoryByProduction" Then
            FormName = IssuanceHistoryByProduction

        ElseIf FormName = "frmGrdRptSOPlanStatusDetail" Then
            FormName = frmGrdRptSOPlanStatusDetail

        ElseIf FormName = "frmRptGrdProductionDetail" Then
            FormName = frmRptGrdProductionDetail

        ElseIf FormName = "frmGrdProductionReceived" Then
            FormName = frmGrdProductionReceived

        ElseIf FormName = "frmMaterialDecomposition" Then
            FormName = frmMaterialDecomposition

        ElseIf FormName = "frmGrdProductionPlaning" Then
            FormName = frmStockDispatch
            enm = EnumForms.frmStockDispatch
            If Tags.Length > 0 Then
                frmStockDispatch.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf FormName = "frmDefTermAndConditions" Then
            FormName = frmDefTermAndConditions

        ElseIf FormName = "frmActivityHistory" Then
            FormName = frmActivityHistory

        ElseIf FormName = "frmActivityPlanList" Then
            FormName = frmActivityPlanList

        ElseIf FormName = "frmMissedVisitGraph" Then
            FormName = frmMissedVisitGraph

        ElseIf FormName = "frmPropertyProfileList" Then
            FormName = frmPropertyProfileList

        ElseIf FormName = "Accounts" Then
            FormName = AccountsToolStripMenuItem1
            enm = EnumForms.frmVoucher

        ElseIf FormName = "frmGrdRptDemandDetail" Then
            FormName = frmGrdRptDemandDetail
            enm = EnumForms.Non

        ElseIf FormName = "frmGrdRptItemWiseSalesSummary" Then
            FormName = frmGrdRptItemWiseSalesSummary

        ElseIf FormName = "frmGrdSales" Then
            FormName = frmGrdSales

        ElseIf FormName = "Stock" Then
            FormName = Stock
            enm = EnumForms.frmVoucher

        ElseIf FormName = "rptStockAccountsReport" Then
            FormName = rptStockAccountsReport
            enm = EnumForms.rptStockAccountsReport

        ElseIf FormName = "rptStockByLocation" Then
            'ControlName = rptStockReportWithCritera
            'enm = EnumForms.rptStockReportWithCritera
            FormName = rptStockByLocation
            enm = EnumForms.rptInventoryForm

        ElseIf FormName = "frmRptStockStatment" Then
            'frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.StockStatementByLPO
            'ApplyStyleSheet(frmRptEnhancementNew)
            'frmRptEnhancementNew.ShowDialog()
            'Exit Sub
            FormName = StockStatementByLPO
            enm = EnumForms.StockStatementByLPO

        ElseIf FormName = "RptGrdTopCustomers" Then
            FormName = RptGrdTopCustomers

        ElseIf FormName = "Vendors" Then
            FormName = Vendors
            enm = EnumForms.frmVoucher

        ElseIf FormName = "AdvanceReceiptsSO" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.AdvanceReceiptsSO
            'ApplyStyleSheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            FormName = AdvanceReceiptsSO
            enm = EnumForms.AdvanceReceiptsSO

        ElseIf FormName = "JobCardDetail" Then
            FormName = JobCardDetail

        ElseIf FormName = "frmRptDSRSummary" Then
            'ApplyStyleSheet(frmRptDSRSummary)
            'frmRptDSRSummary.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            FormName = frmRptDSRSummary
            enm = EnumForms.frmRptDSRSummary

        ElseIf FormName = "frmServicesInvoices" Then
            FormName = frmServicesInvoices

        ElseIf FormName = "frmRequestViews" Then
            FormName = frmRequestViews



        ElseIf MyForm = "SwitchUser" Then
            Dim frmML As New frmMainLogin
            frmML.Text = "Switch User"
            frmML.blnSwitchUser = True
            frmML.ComboBox1.Enabled = False
            frmML.txtUsername.Text = LoginUserName
            frmML.txtUsername.ReadOnly = True
            frmML.txtPassword.Text = ""
            Me.Hide()
            frmML.ShowDialog()
            Exit Function
        End If

        fname = FormName
        'Waqar Raza New Code Here
        'If Me.BackgroundWorker8.IsBusy Then
        '    Application.DoEvents()
        'End If

        If NewSecurityRights = True Then

            Rights = GroupRights.FindAll(AddressOf ReturnRights)
            If Not Rights.Count = 0 Or LoginGroup = "Administrator" Then
                ''Task 3461: Aashir: Edited Becuase Shortcut link account add option was not working.
                If FormName.Name = "Cash" Or FormName.Name = "Bank" Or FormName.Name = "AddCustomer" Or FormName.Name = "AddVendor" Or FormName.Name = "General" Or FormName.Name = "Expense" Then
                    FormName = FrmAddCustomers
                End If
                If Rights.Count > 0 AndAlso LoginGroup <> "Administrator" Then
                    Dim VwRights As SBModel.GroupRights = Rights.Find(AddressOf chkViewFormRights) 'Filter View Rights
                    If VwRights Is Nothing Then
                        msg_Error("You don't have access rights.")
                        Exit Function
                    End If
                End If
                FormName.TopLevel = False
                FormName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                FormName.Dock = DockStyle.Fill
                Me.pnlMain.Controls.Add(FormName)
                'ApplyStyleSheet(FormName)
                If IsBackgroundChanged = True Then
                    FormName.BackColor = Color.White
                End If
                IsBackgroundChanged = False
                FormName.Show()
                FormName.BringToFront()
                Me.LoadLayouts()
            Else
                If LoginUserId = 0 Then
                    FormName = frmUserGroup
                    'ApplyStyleSheet(FormName)
                    If IsBackgroundChanged = True Then
                        FormName.BackColor = Color.White
                    End If
                    IsBackgroundChanged = False
                    FormName.Show()
                    FormName.BringToFront()
                    Me.LoadLayouts()
                ElseIf MyForm = "frmMainHome" Or MyForm = "ChangePassword" Then
                Else
                    ''                 msg_Information("Sorry! you don't have access rights.")
                    Exit Function
                End If
            End If
        Else

            ''Task 3461: Aashir: Edited Becuase Shortcut link account add option was not working.
            If FormName.Name = "Cash" Or FormName.Name = "Bank" Or FormName.Name = "AddCustomer" Or FormName.Name = "AddVendor" Or FormName.Name = "General" Or FormName.Name = "Expense" Then
                FormName = FrmAddCustomers
            End If
            FormName.TopLevel = False
            FormName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            FormName.Dock = DockStyle.Fill
            Me.pnlMain.Controls.Add(FormName)
            Dim dtRights As New DataTable
            Dim rightlist As New Specialized.NameValueCollection
            If IsEnhancedSecurity = True Then
                rightlist = GetFormSecurityControls(FormName.Name)
            Else
                dtRights = GetFormRights(enm)
            End If
            If enm <> EnumForms.Non Then
                If LoginUserId = 0 Then
                    FormName = frmDefUser
                    'ApplyStyleSheet(FormName)
                    If IsBackgroundChanged = True Then
                        FormName.BackColor = Color.White
                    End If
                    IsBackgroundChanged = False
                    FormName.Show()
                    FormName.BringToFront()
                    Me.LoadLayouts() ' ToggleFoldersVisible()
                ElseIf dtRights.Rows.Count > 0 AndAlso IsEnhancedSecurity = False Then
                    If dtRights.Rows(0).Item("View_Rights") = True Then
                        'ApplyStyleSheet(FormName)
                        If IsBackgroundChanged = True Then
                            FormName.BackColor = Color.White
                        End If
                        IsBackgroundChanged = False

                        Try

                            FormName.Show()
                            FormName.BringToFront()
                            Me.LoadLayouts()
                        Catch ex As Exception
                        End Try
                    ElseIf LoginGroup = "Administrator" Then
                        'ApplyStyleSheet(FormName)
                        If IsBackgroundChanged = True Then
                            FormName.BackColor = Color.White
                        End If
                        IsBackgroundChanged = False

                        Try
                            FormName.Show()
                            FormName.BringToFront()
                            Me.LoadLayouts()
                        Catch ex As Exception
                        End Try
                    End If
                ElseIf IsEnhancedSecurity = True AndAlso rightlist.Count > 0 Then
                    If Not rightlist.Item("View") Is Nothing Then
                        'ApplyStyleSheet(FormName)
                        If IsBackgroundChanged = True Then
                            FormName.BackColor = Color.White
                        End If
                        IsBackgroundChanged = False

                        Try
                            FormName.Show()
                            FormName.BringToFront()
                            Me.LoadLayouts()
                        Catch ex As Exception
                        End Try
                    End If
                End If
            Else
                'ApplyStyleSheet(FormName)
                If IsBackgroundChanged = True Then
                    FormName.BackColor = Color.White
                End If
                IsBackgroundChanged = False
                FormName.Show()
                FormName.BringToFront()
                Me.LoadLayouts()
            End If
        End If
        '        Me.LoadLayouts()
        '    End If
        'End If

        ''ApplyStyleSheet(FormName)
        FormName.FormBorderStyle = FormBorderStyle.None
        FormName.Dock = DockStyle.Fill
        FormName.TopLevel = False
        Me.pnlMain.Controls.Add(FormName)
        FormName.Show()
        FormName.BringToFront()
        Dim str As String
        Dim dt As DataTable
        str = "SELECT        tblForms.FormId, tblForms.FormName, tblForms.FormCaption, tblForms.FormModule, tblForms.SortOrder, tblForms.Active, tblForms.AccessKey FROM tblForms INNER JOIN tblFavouriteForms ON tblForms.FormName = tblFavouriteForms.FormName where tblFavouriteForms.UserId=" & LoginUserId & " AND tblForms.FormCaption = '" & FormName.text & "' ORDER BY tblForms.FormModule, tblForms.FormCaption"
        dt = GetDataTable(str)
        If dt.Rows.Count > 0 Then
            btnFav.BackgroundImage = My.Resources.btn_fvrt_active
        Else
            btnFav.BackgroundImage = My.Resources.btn_fvrt
        End If
        fname = FormName
        ''ApplyStyleSheet(fname)
        Me.pnlMain.VerticalScroll.Enabled = True
        'If Not Me.LastControlName.Name = FormName.Name Then
        '    LastItem = MyForm.ToString
        'End If
        Me.AddHistoryItem(FormName.Text, MyForm)

        If arHistory.Count > 0 Then
            If Not arHistory.Item(0).ToString = MyForm.ToString Then
                arHistory.Insert(0, MyForm.ToString)
            End If
        Else
            arHistory.Insert(0, MyForm.ToString)

        End If

    End Function

    Public Function chkViewFormRights(ByVal Rights As SBModel.GroupRights) As Boolean
        Try
            If Rights.FormControlName = "View" Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Sub SetAddCustomersForm(ByVal FormText As String, ByVal FormHeading As String)
        Try
            FrmAddCustomers.Text = FormHeading
            FrmAddCustomers.lblHeader.Text = FormHeading

            FillDropDown(FrmAddCustomers.ComboBox1, "SELECT main_sub_sub_id, sub_sub_title " _
            & " FROM dbo.tblCOAMainSubSub " _
            & "WHERE      (account_type IN('" & FormText & "' " & IIf(FormText.ToString = "Vendor", ",'LC'", "") & ")) ")

            If FrmAddCustomers.ComboBox1.Items.Count > 1 Then
                FrmAddCustomers.ComboBox1.SelectedIndex = 1
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnAdd_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.MouseHover
        cmModProperty.Show(btnAdd, 0, btnAdd.Height)
    End Sub
    ''TFS2543 hide Right base menu
    ''SAbaShabbir
    Public Sub GetMenu()
        Try

            'Dim strArray() As String = {"DefMainAcc", "frmVoucherNew", "frmVoucherPostUnpost", "frmVoucherPost", "frmReconciliation", "frmBankReconciliation", "frmRptBankReconciliation", "frmMobileExpense", "frmAdvanceRequest", "frmAdvanceType", "frmLetterCredit", "frmDefPartners", "frmBSandPLNotesDetail", "frmBranchNew", "frmBudget", "frmBSandPLReports", "frmSubAccount", "frmSubSubAccount", "frmDetailAccount", "frmChangeDetailAccount", "frmCashrequest", "frmPaymentNew", "frmCustomerCollection", "frmExpense", "frmChequeTransfer", "frmPaymentVoucherNew", "frmReceiptVoucherNew", "frmChequesAdjustment", "frmAddChequeBookSerial", "frmCustomerRecoveryTarget", "frmLockerConfiguration", "frmQoutationNew", "frmSalesOrderNew", "frmDeliveryChalanStatus", "frmSales", "frmPOSEntry", "frmInstallment", "frmSalesTransfer", "frmSalesCertificate", "frmSalesReturn", "frmSOStatus", "frmDeliveryChalanStatus", "frmGrdRptQuotationStatus", "frmBillAnalysis", "frmSalesAdjustmentVoucher", "frmUpdatebitlyAndTransporter", "frmSalesReturnWeight", "frmPurchaseInquiry", "frmVendorQuotation", "frmSalesInquiryApproval", "frmInquiryComparisonStatement", "frmPurchaseDemand", "frmPurchaseOrderNew", "frmReceivingNote", "frmPurchaseNew", "frmPurchaseReturn", "frmPurchaseAdjustmentVoucher", "frmPOStatus", "frmGrdRptPurchaseDemandStatus", "frmGRNStatus", "frmDefVendor", "frmPurchaseDemand", "frmReceivingNote", "frmStoreIssuence", "frmReturnStoreIssuence", "frmStockDispatch", "frmStockReceive", "frmStockAdjustment", "frmAdjeustmentAveragerate", "frmClaim", "frmMRPlan", "frmDefArticle", "frmItemBulk", "frmDefArticleDepartment", "FrmLocation", "frmDepartmentWiseProduction", "frmProductionStore", "frmProductionLevel", "frmGrdProductionAnalaysis", "frmReturnStoreIssuence", "frmMRPlan", "frmStockDispatch", "frmStockReceive", "frmCostSheet", "frmGrdRptProductionLevel", "frmrptGrdProducedItems", "frmRptProductionSummary", "frmGrdRptProductionComparison", "StoreIssuanceSummary", "StoreIssuanceDetail", "frmStockStatmentBySize", "frmRptGrdStockStatement", "RptGridItemSalesHistory", "frmRptGrdStockInOutDetail", "frmGrdRptLocationWiseStockLedger", "frmGrdRptProjectWiseStockLedger", "frmRptProductionSummary", "frmRptSummaryOfProduction", "frmRptPlansStatus", "frmRptProductionBasedSalary", "frmCustomerPlanning", "frmPlanTickets", "frmMaterialEstimation", "frmItemsConsumption", "frmIssuanceConsumptionReport", "frmConsumptionEstimationReport", "MaterialAnalysis", "frmMaterialAllocation", "frmMaterialDecomposition", "frmProductionProcess", "frmProductionPlanStatus", "frmRptPlansStatus", "frmSalesOrderNew",
            '                        "frmSOStatus", "frmLetterCredit", "frmImport", "frmGrdLCDetail", "rptGridItemWiseLC", "frmImportDetailReport", "frmLCOutstandingDetailReport", "frmDefEmployee", "frmEmployeeProfile", "frmEmployeePromotion", "frmAttendanceEmployees", "frmEmployeeCard", "frmAttendance", "FrmEmployeeSiteVisit", "FrmEmployeeSiteVisitCharges", "frmLeaveApplication", "frmDefDepartment", "frmDefEmpDesignation", "frmHolidySetup", "frmGrdRptAttendanceRegisterUpdate", "frmEmpAttendanceEmailAlertSchedule", "frmDefLeaveTypes", "frmLeaveAdjustment", "frmNewLeaveApplication", "frmEmployeeWarning", "frmEmployeeTermination", "frmShiftChangeRequest", "frmEmployeeVisitPlan", "frmAutoSalaryGenerate", "frmEmployeeSalaryVoucher", "frmEmpOverTimeSchedule", "frmGrdRptLoanApprovalList", "frmEmployeeDeductions", "frmEmployeeAutoOverTime", "frmDefShift", "frmDefShiftGroup", "frmHolidySetup", "frmDefTaxSlabs", "frmDailySalaries", "frmDefEmployee", "frmGrdRptEmployeeSalarySheetDetail", "DailySalarySheet", "DailySalarySheetSummary", "frmGrdRptGenerelEmployeeSalary", "frmGrdRptEmployeeSalarySheetDetail", "frmRptEmpSalarySheetDetail", "frmStatus", "frmTasks", "frmTypes", "frmLeads", "frmProjectPortFolio", "frmProjectVisit", "frmProjQuotion", "frmProjectVisitType", "frmCompanyLocations", "frmCompanyContacts", "frmSalesInquiry", "frmSalesInquiryRights", "frmQoutationNew", "frmGrdRptProjectVisitDetail", "frmPurchaseOrderNew", "frmServiceItemTask", "frmVendorContract", "frmItemTaskProgress", "frmProjectProgressApproval", "frmItemProgressReport", "frmAsset", "frmAssetCategory", "AssetType", "frmAssetLocation", "AssetCondition", "frmAssetStatus", "frmGrdRptAssetsDetail", "frmSiteRegistration", "frmCmfa", "frmGrdRptCMFAllRecords", "frmGrdRptCMFASummary", "frmGrdRptCMFAOfSummaries", "frmRptCMFADetail", "frmDefJobCard", "frmLiftAssociation", "frmIGP", "frmWIP", "JobCardDetail", "frmDefArticle", "frmDefArticleDepartment", "frmDetailAccountCat", "frmDefSize", "frmDefColor", "frmDefCategory", "frmDefSubCategory", "frmDefType", "frmDefUnit", "frmDefGender", "frmDefBatch", "FrmLocation", "frmInventoryLevel", "frmModelList", "frmDefCustomer", "frmDefCustomerType", "frmCustomerDiscountsFlat", "frmCustomerDiscountsFlat", "frmGrdCustomerBasedTarget", "frmUserWiseCustomer", "frmGrdSalesmanCommissionDetail", "frmCustomerTypeWisePriceList", "frmDefVendor", "frmVendorType", "frmDefEmployee", "frmDefEmpDesignation", "frmDefDepartment", "frmEmployeeArticleCostRate", "frmDefCity", "frmDefArea", "frmCostCenter", "FrmCountry", "frmState", "FrmRegions", "FrmZone", "FrmBelt", "frmPOSConfiguration", "frmSystemConfigurationNew", "FrmEmailconfig",
            '                            "frmDefVehicle", "frmRootPlan", "frmproductionSteps", "frmDefDocumentPrefix", "frmDefTaxSlabs", "frmDefEmployeeMonthlyTarget", "frmTerminalConfiguration", "frmDefGroupVoucherApproval", "frmNotificationList", "frmTermsandConditions", "frmInventoryColumnStrings", "frmCustomerBottomSaleRate", "frmProductionProcess", "frmLabourType", "frmUserGroup", "frmReturnablegatepass", "frmUpdateReturnableGatepassDetail", "frmServicesInwardGatePass", "frmDefVehicle", "FrmVehicle", "frmDateLock", "frmDateLockPermission", "frmAgreement", "frmGrdRptContactList", "frmdbbackup", "frmRestoreBackup", "frmActivityLog", "frmTerminal", "frmReleaseUpdate", "frmComposeMessage", "frmMessageView", "frmOpening", "frmActiveLicense", "FrmVehicle", "frmScheduleSMS", "frmTroubleshoot", "frmGrdRptDuplicateDocuments", "frmUpdateCurrency", "frmCostCentreReshuffle", "frmUtilityApplyAverageRate", "frmApplySalePriceUtility", "frmSkippingSalesInvoicesNumbers", "frmRevenueDataImport", "frmProItemList", "frmPropertyProfileList", "frmProSalesList", "frmProPurchaseList", "frmProInvestorList", "frmProEstateList", "frmProAgentList", "frmProOfficeList", "frmProBranchList", "frmProDealerList", "frmaboutus", "CashFlowStatementStandard", "rptExpenses", "CashFlowStatement", "frmGrdRptAgingPayables", "frmGrdRptAgingReceiveables", "InvoiceBasedPaymentSummaryReport", "frmRptGrdInwardgatepass", "frmItemProgressReport", "frmItemWiseProgressUpto"}
            'Dim ArrayList As New List(Of String)

            'For Each value As String In strArray
            'Next            '    ArrayList.Add(value)


            Dim val As String
            MyMainMenuViwRights = MainMenuViewRights()
            For Each objModel As Users In MyMainMenuViwRights
                val = objModel.FormName
                validFormListToShow.Add(val)
            Next
            Dim str As String = Me.ContextMenuStrip1.Items.Item(0).ToString
            Dim str1 As String = Me.CMenuReports.Items.Item(0).ToString
            FindMenuItemByName(ContextMenuStrip1, str)
            FindMenuItemByName(CMenuReports, str1)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''TFS2543 hide Right base menu
    ''SAbaShabbir
    Private Function FindMenuItemByName(parent As ContextMenuStrip, name As String) As ToolStripMenuItem
        Try
            'Dim itemsList As List(Of ToolStripMenuItem) = New List(Of ToolStripMenuItem)
            Dim control As ToolStripMenuItem
            For i As Integer = 0 To parent.Items.Count - 1
                control = parent.Items(i)
                If control IsNot Nothing Then
                    If (control.Name = name) Then
                        Return control
                    ElseIf (control.DropDownItems.Count > 0) Then
                        control = FindSubMenuItemByName(control, name)
                        'If (control.DropDownItems.Count > 0) Then
                        '    control.Visible = True
                        '    Return control
                        If control IsNot Nothing Then
                            ' control.Visible = False
                            Return control
                        End If
                    End If
                End If
            Next

            'For i As Integer = 0 To parent.Items.Count - 1
            '    control = parent.Items(i)
            '    If (control.DropDownItems.Count > 0) Then
            '        control.Visible = True
            '    Else
            '        control.Visible = False
            '    End If
            'Next
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function
    ''TFS2543 hide Right base menu
    ''SAbaShabbir
    Private Function FindSubMenuItemByName(parent As ToolStripMenuItem, name As String) As ToolStripMenuItem
        Try
            Dim control As ToolStripMenuItem
            Dim Count As Integer = 0
            For i As Integer = 0 To parent.DropDownItems.Count - 1
                FormName = parent.DropDownItems(i).Tag
                control = parent.DropDownItems(i)
                For k As Integer = 0 To validFormListToShow.Count - 1
                    If (FormName = validFormListToShow(k)) Then
                        parent.DropDownItems(i).Visible = True
                        parent.Visible = True
                        Exit For
                    Else
                        parent.DropDownItems(i).Visible = False
                        Count = Count + 1
                        If Count = parent.DropDownItems.Count Then
                            parent.Visible = False
                        End If
                    End If
                Next
                If control IsNot Nothing Then
                    If (control.Name = name) Then
                        Return control
                    ElseIf (control.DropDownItems.Count > 0) Then
                        control = FindSubMenuItemByName(control, name)
                        If control IsNot Nothing Then
                            Return control
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Sub ValidateLicense()
        Try
            GetLicenseDetails()
            If LicenseVersion = "" Then
            End If
            If LicenseVersion <> String.Empty Then
                Me.Text = Me.Text & " (" & LicenseVersion & ")"
            Else
                Me.Text = Me.Text
            End If
            'Me.UltraStatusBar2.Panels("LicenseExpiry").Text = IIf(LicenseExpiryType <> "Monthly", "Service Expiry: ", "License Expiry: ") & LicenseExpiry.ToString("dd-MMM-yyyy")
            If LicenseExpiry <= Date.Now.AddMonths(-1) Then
                LicenseStatus = "Blocked"
                '    Me.UltraStatusBar2.Panels("LicenseExpiry").Appearance.BackColor = Color.Red
                '    Me.UltraStatusBar2.Panels("LicenseExpiry").Appearance.ForeColor = Color.White
            ElseIf LicenseExpiry.ToString("dd-MMM-yyyy 23:59:59") < Date.Now Then
                LicenseStatus = "Expired"
                '    Me.UltraStatusBar2.Panels("LicenseExpiry").Appearance.BackColor = Color.Yellow
            ElseIf LicenseExpiry.ToString("dd-MMM-yyyy 23:59:59") > Date.Now AndAlso LicenseExpiry.ToString("dd-MMM-yyyy 23:59:59") <= Date.Now.AddDays(7) Then
                LicenseStatus = "Expiring"
                '    Me.UltraStatusBar2.Panels("LicenseExpiry").Appearance.BackColor = Color.Blue
                '    Me.UltraStatusBar2.Panels("LicenseExpiry").Appearance.ForeColor = Color.White
            Else
                LicenseStatus = "Valid"
            End If
            Dim cn As New SqlClient.SqlConnectionStringBuilder(SBDal.SQLHelper.CON_STR)
            If cn.DataSource.ToUpper.Contains(System.Environment.MachineName.ToUpper.ToString) Then
                gblnTrialVersion = True
                If (LicenseSystemId1.ToString.Length > 0 AndAlso LicenseSystemId1.ToString.ToUpper = GetMotherboard().ToString.ToUpper) AndAlso (LicenseSystemId2.ToString.Length > 0 AndAlso LicenseSystemId2.ToString.ToUpper = GetBIOS().ToString.ToUpper) Then
                    gblnTrialVersion = False
                Else
                    Dim strMac As String()
                    Dim MACAddressList As String = ""
                    'strMac = GetMACAddressList.Split(",")  'GetMACAddressListNew() is used instead

                    'Added by Syed Irfan Ahmad on 15 Feb 2018, Task 2411
                    If ModGlobel.GetMACAddressListNew(MACAddressList) = False Then
                        Dim gm As New AgriusMessage
                        gm.Message = "Error reading system information. Please contact your System Administrator."
                        gm.ErrorCode = "GEC-LIC-0x000-1302"
                        ModGlobel.AgriusMessageLogger.Log(gm)

                        ShowErrorMessage("Error reading system information. Please contact your System Administrator." & vbCrLf & "Error Code: GEC-LIC-0x000-1302")
                        Exit Sub
                    End If
                    strMac = MACAddressList.Split(",")
                    If strMac.Length > 0 Then
                        For Each Str As String In strMac
                            If Str.ToString.Trim.Replace(" ", "").Length > 4 AndAlso LicenseSystemId = Str.ToString.Trim.Replace(" ", "") Then
                                gblnTrialVersion = False
                                Exit For
                            End If
                        Next
                    Else
                        'Added by Syed Irfan Ahmad on 15 Feb 2018, Task 2411
                        Dim gm As New AgriusMessage
                        gm.Message = "Error reading system information. Please contact your System Administrator or Agrius Support."
                        gm.ErrorCode = "GEC-LIC-0x000-1205"
                        ModGlobel.AgriusMessageLogger.Log(gm)

                        ShowErrorMessage("Error reading system information. Please contact your System Administrator or Agrius Support." & vbCrLf & vbCrLf & "Error Code: GEC-LIC-0x000-1205")
                        Exit Sub
                    End If
                End If
                If LicenseDBName.ToString.Length > 0 AndAlso LicenseDBName.ToString.ToUpper <> cn.InitialCatalog.ToString.ToUpper Then
                    'Added by Syed Irfan Ahmad on 15 Feb 2018, Task 2411
                    Dim gm As New AgriusMessage
                    gm.Message = "The license information does not match with currennt system details." & vbCrLf & "Please contact your System Administrator or Agrius Support with the error code."
                    gm.ErrorCode = "GEC-LIC-0x087-1928"
                    gm.Criticality = SBUtility.Utility.MessageCriticality.High
                    gm.Details = "License DB Name: " & LicenseDBName & ", Initial Catalog: " & cn.InitialCatalog.ToString
                    ModGlobel.AgriusMessageLogger.Log(gm)

                    LicenseStatus = "Blocked"
                    ShowErrorMessage("The license information does not match with currennt system details." & vbCrLf & "Please contact your System Administrator or Agrius Support with below error code." & vbCrLf & vbCrLf & "Error Code: GEC-LIC-0x087-1928")
                End If
                If Date.Now > "31-Jan-2017 23:59:59" AndAlso LicenseStatus <> "Blocked" AndAlso gblnTrialVersion Then
                    'Added by Syed Irfan Ahmad on 15 Feb 2018, Task 2411
                    Dim gm As New AgriusMessage
                    gm.Message = "There is a mismatch in system information. Please contact your System Administrator or Agrius Support."
                    gm.ErrorCode = "GEC-LIC-0x655-1820"
                    ModGlobel.AgriusMessageLogger.Log(gm)

                    LicenseStatus = "Blocked"
                    ShowErrorMessage("There is a mismatch in system information. Please contact your System Administrator or Agrius Support." & vbCrLf & vbCrLf & "Error Code: GEC-LIC-0x655-1820")
                End If
            Else
                'TODO: code for terminal license
            End If
            '// Setting Module configurations
            '
            'If Not LicenseModuleList Is Nothing AndAlso LicenseModuleList.Trim.Length > 0 Then
            '    Me.UltraToolbarsManager1.Tools("mnuAccounts").SharedProps.Visible = False
            '    Me.UltraToolbarsManager1.Tools("mnuCashManagement").SharedProps.Visible = False
            '    Me.UltraToolbarsManager1.Tools("mnuSales").SharedProps.Visible = False
            '    Me.UltraToolbarsManager1.Tools("mnuPurchases").SharedProps.Visible = False
            '    Me.UltraToolbarsManager1.Tools("mnuInventory").SharedProps.Visible = False
            '    Me.UltraToolbarsManager1.Tools("Production").SharedProps.Visible = False
            '    Me.UltraToolbarsManager1.Tools("mnuImport").SharedProps.Visible = False
            '    Me.UltraToolbarsManager1.Tools("Human Resource").SharedProps.Visible = False
            '    Me.UltraToolbarsManager1.Tools("Payroll").SharedProps.Visible = False
            '    Me.UltraToolbarsManager1.Tools("CRM").SharedProps.Visible = False
            '    Me.UltraToolbarsManager1.Tools("AssetsManagement").SharedProps.Visible = False
            '    Me.UltraToolbarsManager1.Tools("Site Management").SharedProps.Visible = False
            '    Me.UltraToolbarsManager1.Tools("grpmnuServices").SharedProps.Visible = False
            '    For Each mStr As String In LicenseModuleList.Split(",")
            '        Select Case mStr.ToString
            '            Case "Accounts"
            '                Me.UltraToolbarsManager1.Tools("mnuAccounts").SharedProps.Visible = True
            '            Case "Cash"
            '                Me.UltraToolbarsManager1.Tools("mnuCashManagement").SharedProps.Visible = True
            '            Case "Sales"
            '                Me.UltraToolbarsManager1.Tools("mnuSales").SharedProps.Visible = True
            '            Case "Purchase"
            '                Me.UltraToolbarsManager1.Tools("mnuPurchases").SharedProps.Visible = True
            '            Case "Inventory"
            '                Me.UltraToolbarsManager1.Tools("mnuInventory").SharedProps.Visible = True
            '            Case "Production"
            '                Me.UltraToolbarsManager1.Tools("Production").SharedProps.Visible = True
            '            Case "Imports"
            '                Me.UltraToolbarsManager1.Tools("mnuImport").SharedProps.Visible = True
            '            Case "HR"
            '                Me.UltraToolbarsManager1.Tools("Human Resource").SharedProps.Visible = True
            '            Case "Payroll"
            '                Me.UltraToolbarsManager1.Tools("Payroll").SharedProps.Visible = True
            '            Case "CRM"
            '                Me.UltraToolbarsManager1.Tools("CRM").SharedProps.Visible = True
            '            Case "Assets"
            '                Me.UltraToolbarsManager1.Tools("AssetsManagement").SharedProps.Visible = True
            '            Case "Site"
            '                Me.UltraToolbarsManager1.Tools("Site Management").SharedProps.Visible = True
            '            Case "Services"
            '                Me.UltraToolbarsManager1.Tools("grpmnuServices").SharedProps.Visible = True
            '        End Select
            '    Next
            'End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ShowErrorNotification(ByVal MessageText As String, Optional ByVal MessageStyle As MsgBoxStyle = MsgBoxStyle.Information, Optional ByVal MessageWaitTime As Integer = 10)
        Try
            tmrMessageNotificationLabel.Stop()
            tmrMessageNotificationLabel.Enabled = False
            tmrMessageNotificationLabel.Interval = 1000 * If(MessageWaitTime > 0, MessageWaitTime, 10)

            'If MessageStyle = MsgBoxStyle.Information Or MessageStyle = Nothing Then

            If Me.pnlErrorNotification.Visible = True Then
                Me.pnlErrorNotification.Visible = False
                Application.DoEvents()
            End If

            If MessageStyle = MsgBoxStyle.Critical Then
                pnlErrorNotification.BackColor = Color.FromArgb(238, 17, 17)

            Else
                pnlErrorNotification.BackColor = Color.FromArgb(0, 91, 174)
            End If

            Me.lblErrorNotification.Text = MessageText
            Me.pnlErrorNotification.Visible = True
            Me.pnlErrorNotification.BringToFront()
            tmrMessageNotificationLabel.Enabled = True
            tmrMessageNotificationLabel.Start()

        Catch ex As Exception
            msg_Error(ex.Message)

        End Try
    End Sub

    Private Sub tmrLabel_Tick(sender As Object, e As EventArgs) Handles tmrMessageNotificationLabel.Tick
        pnlErrorNotification.Visible = False
    End Sub

    Private Sub BackgroundWorker4_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker4.DoWork
        Try
            GetSMSSchedule()
        Catch ex As Exception
            ShowErrorNotification(ex.Message)
        End Try
    End Sub

    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        Try
            Me.Timer4.Enabled = False
            If BackgroundWorker4.IsBusy Then Exit Sub
            BackgroundWorker4.RunWorkerAsync()
            Do While BackgroundWorker4.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
        Finally
            Me.Timer4.Enabled = True
        End Try
        'end task 2640
    End Sub

    Private Sub BackgroundWorker6_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker6.DoWork
        Try
            'GroupRights = New SBDal.GroupRightsBL().GetRights(LoginUserId)
            'If Not getConfigValueByType("NewSecurityRights").ToString = "Error" Then
            '    NewSecurityRights = getConfigValueByType("NewSecurityRights")
            'End If
            GetEnventKeyList()
            GetAllSMSTemplate()
            GetLocationList()
            CompanyList()
            CompanyRightsList()
            LocationRightsList()
            GetDateLock()
            'ConfigValuesDataTable = GetConfigValuesdt()
            'If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            '    flgCompanyRights = getConfigValueByType("CompanyRights")
            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task: 2640 SMS Schedule
    Public Sub GetSMSSchedule()
        Try
            Dim dtSchedule As DataTable = GetDataTable("SELECT * FROM tblSMSSchedule WHERE IsCustomer='True' OR IsVendor='True'")
            If dtSchedule IsNot Nothing Then
                If dtSchedule.Rows(0).Item("IsCustomer") = "True" Then
                    Dim dtCustomer As New DataTable
                    dtCustomer = GetDataTable("Select cust.CustomerName,cust.Phone,vch.Debit_Amount-vch.Credit_Amount as Balance, coaDetail.account_type FROM tblCustomer cust Left Outer JOIN" & _
                    "tblCOAMainSubSubDetail coaMain ON cust.AccountId=coaMain.coa_detail_id Left Outer Join" & _
                    "tblVoucherDetail vch ON vch.coa_detail_id=coaMain.coa_detail_id INNER JOIN" & _
                    "tblCOAMainSubSub coaDetail ON coaDetail.main_sub_sub_id=coaMain.main_sub_sub_id" & _
                    "WHERE coaDetail.Account_Type='Customer'")
                    For Each row As DataRow In dtCustomer.Rows

                        'EmailSave = Nothing
                        'Dim toEmail As String = String.Empty
                        'Dim flg As Boolean = False
                        'If IsEmailAlert = True Then
                        '    Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name='" & Me.Name & "' AND EmailAlert=1")
                        '    If dtForm.Rows.Count > 0 Then
                        '        flg = True
                        '    Else
                        '        flg = False
                        '    End If
                        '    If flg = True Then
                        '        If AdminEmail <> "" Then
                        '            Dim Email As SBModel.Email
                        '            Email.ToEmail = AdminEmail
                        '            Email.CCEmail = String.Empty
                        '            Email.BccEmail = String.Empty
                        '            Email.Attachment = SourceFile
                        '            Email.Subject = "" & IIf(row.Item("") = "Cash", "Cash Receipt", "Bank Receipt") & " " & setVoucherNo & " "
                        '            Email.Body = "" & IIf(setVoucherType = "Cash", "Cash Receipt", "Bank Receipt") & " " _
                        '            & " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Total_Previouse_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
                        '            Email.Status = "Pending"
                        '            Call New MailSentDAL().Add(Email)
                        '        End If
                        '    End If
                        'End If
                    Next

                ElseIf dtSchedule.Rows(0).Item("IsVendor") = "True" Then

                    Dim str As String = "Select vnd.VendorName,vnd.Mobile,vch.Debit_Amount-vch.Credit_Amount as Balance, coaDetail.account_type FROM tblVendor vnd Left Outer JOIN" & _
                                        "tblCOAMainSubSubDetail coaMain ON vnd.AccountId=coaMain.coa_detail_id Left Outer Join" & _
                                        "tblVoucherDetail vch ON vch.coa_detail_id=coaMain.coa_detail_id INNER JOIN " & _
                                        "tblCOAMainSubSub coaDetail ON coaDetail.main_sub_sub_id=coaMain.main_sub_sub_id" & _
                                        "WHERE coaDetail.Account_Type='Vendor'"
                    Dim dtVendor As New DataTable
                    dtVendor = GetDataTable(str)

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmModProperty_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed

    End Sub

    Private Sub frmModProperty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not getConfigValueByType("NewSecurityRights").ToString = "Error" Then
            NewSecurityRights = Convert.ToBoolean(getConfigValueByType("NewSecurityRights").ToString)
        End If
        If BackgroundWorker8.IsBusy Then Exit Sub
        BackgroundWorker8.RunWorkerAsync()
        Do While BackgroundWorker8.IsBusy
            Application.DoEvents()
        Loop

        Dim MainMenu As Boolean
        Dim str As String
        Dim dt As DataTable
        str = "SELECT ISNULL(ShowMainMenuRights, 0) as MenuRights from tblUser where User_ID = " & LoginUserId & ""
        dt = GetDataTable(str)
        MainMenu = dt.Rows(0).Item("MenuRights").ToString
        If MainMenu = True Then
            GetMenu()
        End If

        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.FlatAppearance.MouseOverBackColor = btnAdd.BackColor

        btnFav.FlatAppearance.BorderSize = 0
        btnFav.FlatAppearance.MouseOverBackColor = btnFav.BackColor

        btnFavList.FlatAppearance.BorderSize = 0
        btnFavList.FlatAppearance.MouseOverBackColor = btnFavList.BackColor

        btnBack.FlatAppearance.BorderSize = 0
        btnBack.FlatAppearance.MouseOverBackColor = btnBack.BackColor

        btnNotification.FlatAppearance.BorderSize = 0
        btnNotification.FlatAppearance.MouseOverBackColor = btnNotification.BackColor

        btnMod.FlatAppearance.BorderSize = 0
        btnMod.FlatAppearance.MouseOverBackColor = btnMod.BackColor

        btnHome.FlatAppearance.BorderSize = 0
        btnHome.FlatAppearance.MouseOverBackColor = btnHome.BackColor

        btnReport.FlatAppearance.BorderSize = 0
        btnReport.FlatAppearance.MouseOverBackColor = btnReport.BackColor

        btnSettings.FlatAppearance.BorderSize = 0
        btnSettings.FlatAppearance.MouseOverBackColor = btnSettings.BackColor
        ''Start TFS4765 : Ayesha Rehman : 10-10-2018
        If Convert.ToString(getConfigValueByType("MainMenuNavigatorColor").ToString) <> "Error" Then
            If Convert.ToInt32(Val(getConfigValueByType("MainMenuNavigatorColor").ToString)) = 0 Then
                Me.Panel2.BackColor = Color.FromArgb(68, 176, 85)
            ElseIf Convert.ToInt32(Val(getConfigValueByType("MainMenuNavigatorColor").ToString)) = 1 Then
                Me.Panel2.BackColor = Color.FromArgb(209, 52, 56)
            ElseIf Convert.ToInt32(Val(getConfigValueByType("MainMenuNavigatorColor").ToString)) = 2 Then
                Me.Panel2.BackColor = Color.FromArgb(0, 120, 215)
            ElseIf Convert.ToInt32(Val(getConfigValueByType("MainMenuNavigatorColor").ToString)) = 3 Then
                Me.Panel2.BackColor = Color.FromArgb(16, 124, 16)
            End If
        Else
            Me.Panel2.BackColor = Color.FromArgb(68, 176, 85)
        End If
        ''End TFS4765
        'ValidateLicense()
        dbVersion = getConfigValueByType("Version").ToString
        If getConfigValueByType("EnabledBrandedSMS").ToString = "True" Then
            Me.Timer5.Interval = 60000 * Convert.ToInt32(getConfigValueByType("SMSScheduleTime").ToString)
            Me.Timer5.Enabled = True
        End If
        ''Start TFS4767 : Ayesha Rehman : 09-10-2018
        'If dbVersion.ToString.Replace(".", "") <> Replace(Application.ProductVersion, ".", "") Then
        '    frmbg.Show()
        '    dialouge.ShowDialog()
        '    If dialouge.DialogResult = Windows.Forms.DialogResult.OK Then
        '        ShowForm("frmReleaseUpdate")
        '        frmReleaseUpdate.btnUpdate.Focus()
        '    ElseIf dialouge.DialogResult = Windows.Forms.DialogResult.Cancel Then
        '        frmModProperty_FormClosing(Nothing, Nothing)
        '    End If
        'End If
        ''End TFS4767  09-10-2018
        'If dbVersion.ToString.Replace(".", "") < Replace(Application.ProductVersion, ".", "") Then
        '    ShowForm("frmReleaseUpdate")
        'End If
        If BackgroundWorker6.IsBusy Then Exit Sub
        BackgroundWorker6.RunWorkerAsync()
        Do While BackgroundWorker6.IsBusy
            Application.DoEvents()
        Loop

        Me.Timer6.Enabled = True
        Me.Timer6.Interval = 100

        Timer7.Enabled = True
        Timer7.Interval = 100
        EnableAutoBackup()
        GetFavouriteFormsList()

        If getConfigValueByType("ListSearchStartWith").ToString <> "Error" Then
            blnListSeachStartWith = Convert.ToBoolean(getConfigValueByType("ListSearchStartWith").ToString)
        End If
        If getConfigValueByType("ListSearchContains").ToString <> "Error" Then
            blnListSeachContains = Convert.ToBoolean(getConfigValueByType("ListSearchContains").ToString)
        End If

        If Not LicenseStatus = "Blocked" Then
            frmMainHome.TopLevel = False
            Me.pnlMain.Controls.Add(frmMainHome)
            frmMainHome.Show()
            frmMainHome.BringToFront()
            frmMainHome.Dock = DockStyle.Fill
            Me.pnlMain.VerticalScroll.Enabled = True
        End If
        'Aashir: Release was not downloading So These lines of code were added
        If getConfigValueByType("EnableAutoUpdate").ToString = True Then
            frmReleaseDownload.Visible = False
            frmReleaseDownload.Show()
        End If

        'TFS3784 Ayesha Rehman : Append Company Name of the logged in company
        Dim formText As String = Me.Text
        formText = formText & " " & ConCompany
        Me.Text = formText
        'end TFS3784
    End Sub

    Private Sub pbSearch_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbSearch.MouseHover
        txtsearch.Visible = True
    End Sub

    Private Sub pbSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbSearch.Click
        txtsearch.Visible = False
    End Sub

    Private Sub btnMod_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.MouseHover, Button1.Click
        ContextMenuStrip1.Show(Button1, 0, Button1.Height)
    End Sub

    Private Sub InventoryEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InventoryEntryToolStripMenuItem.Click
        'frmInventoryEntry.ShowDialog()
    End Sub

    Private Sub PurchaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseToolStripMenuItem.Click
        frmProPurchase.ShowDialog()
    End Sub

    Private Sub btnBack_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.MouseHover
        BackToolStripButton.Show(btnBack, 0, btnBack.Height)
    End Sub

    Private Sub btnSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettings.Click
        'frmConfigMain.TopLevel = False
        'Me.pnlMain.Controls.Add(frmConfigMain)
        'frmConfigMain.Show()
        'frmConfigMain.BringToFront()
        'Me.pnlMain.VerticalScroll.Enabled = True
        'frmConfigMain.FormBorderStyle = FormBorderStyle.None
        'frmConfigMain.Dock = DockStyle.Fill

        ShowForm("frmConfigMain")

    End Sub

    Private Sub SalesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesToolStripMenuItem.Click
        frmProSales.ShowDialog()
    End Sub

    Private Sub InvestorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InvestorToolStripMenuItem.Click
        frmProInvestor.ShowDialog()
    End Sub

    Private Sub DealerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DealerToolStripMenuItem.Click
        frmProDealer.ShowDialog()
    End Sub

    Private Sub AgentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AgentToolStripMenuItem.Click
        'frmProAgent.ShowDialog()
    End Sub

    Private Sub EstateListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EstateListToolStripMenuItem.Click
        frmProEstateList.ShowDialog()
    End Sub

    Private Sub Timer5_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer5.Tick
        Try
            Timer5.Enabled = False
            Application.DoEvents()
            If System.Net.Dns.GetHostName.ToString.ToUpper <> getConfigValueByType("DNSHostForSMS").ToString.ToUpper Then Exit Sub
            If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
                ShowErrorMessage("SMS Error: Agrius ERP is opened multiple times.")
                Exit Sub
            End If
            Application.DoEvents()
            If BackgroundWorker10.IsBusy Then Exit Sub
            BackgroundWorker10.WorkerReportsProgress = True
            BackgroundWorker10.RunWorkerAsync()
            Do While BackgroundWorker10.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Timer5.Enabled = True
        End Try
    End Sub

    Private Sub frmModProperty_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If frmMainLogin.blnSwitchUser = False Then

            Try
                'frmBackupReminder.ShowDialog()
                Try
                    If BackupCalled = False Then


                        Dim dt As New DataTable

                        dt = GetDataTable("select top 5 * from msdb.dbo.backupset where database_name = db_name() order by backup_finish_date desc")

                        If dt.Rows.Count > 0 Then

                            If CType(dt.Rows(0).Item("backup_finish_date").ToString, DateTime) < Date.Now.ToString("dd-MMM-yyyy 00:00:00") Then

                                Dim lastBackup As DateTime = CType(dt.Rows(0).Item("backup_finish_date").ToString, DateTime)

                                frmBackupReminder.lblDay.Text = lastBackup.DayOfWeek.ToString.ToUpper
                                frmBackupReminder.lblDate.Text = lastBackup.Day
                                frmBackupReminder.lblMonthYear.Text = lastBackup.ToString("MMM yyyy").ToString.ToUpper

                                If frmBackupReminder.ShowDialog() = Windows.Forms.DialogResult.Yes Then

                                    ShowForm("frmdbbackup")
                                    BackupCalled = True
                                    e.Cancel = True
                                    Exit Sub

                                End If

                            End If


                            '    If msg_Confirm("Do you want to backup your data now ?" & Chr(10) & "Your last backup was done at " & CType(dt.Rows(0).Item("backup_finish_date").ToString, DateTime).ToString("dd-MMM-yyy HH:mm") & Chr(10) & Chr(10) & "It is recomended to backup you data now otherwise you might loose your all data.") = True Then

                            '        LoadControl("UtilBackupNew")
                            '        BackupCalled = True
                            '        e.Cancel = True
                            '        Exit Sub

                            '    End If


                        Else

                            msg_Error("You have never backed up your data so do it now otherwise you might loose your all data." & Chr(10) & Chr(10) & "You will be redirected to backup screen where you can do this." & Chr(10) & "Incase you face any issue you can contact your Agrius ERP Administrator")
                            ShowForm("frmdbbackup")
                            BackupCalled = True
                            e.Cancel = True
                            Exit Sub

                        End If

                    End If

                Catch ex As Exception

                End Try

                If DownloadInProgress = True Then

                    e.Cancel = True

                    Application.DoEvents()
                    frmReleaseDownload.WindowState = FormWindowState.Normal
                    frmReleaseDownload.TopMost = True

                    Exit Sub

                End If

                If msg_Confirm("Do you want to log out?") = True Then
                    If System.IO.Directory.Exists(str_ApplicationStartUpPath & "\ApplicationSettings") = False Then
                        System.IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\ApplicationSettings")
                    End If
                    'Me.ContextMenuStrip1.SaveAsXml(str_ApplicationStartUpPath & "\TempExpSetting.xml")
                    'Me.SaveLayouts(Me.SplitContainer.Panel2)
                    'LastGroup = Me.UltraExplorerBar1.ActiveItem.Group.Index
                    'LastItem = Me.UltraExplorerBar1.ActiveItem.Index
                    SaveLastSettings()
                    If LoggedIn = True Then
                        Dim Con1 As New SqlConnection(SQLHelper.CON_STR)
                        If Con1.State = ConnectionState.Closed Then Con1.Open()
                        Dim trans As SqlTransaction = Con1.BeginTransaction()
                        Dim strSql As String
                        strSql = "update tblUser set LoggedIn = 0 where User_ID=" & LoginUserId & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql)
                        trans.Commit()
                    End If
                    'If System.IO.File.Exists(str_ApplicationStartUpPath & "\System Files\Sounds\Goodbye.wav") Then
                    '    Me.AxWindowsMediaPlayer1.URL = str_ApplicationStartUpPath & "\System Files\Sounds\Goodbye.wav"
                    '    Me.AxWindowsMediaPlayer1.Refresh() ' str_ApplicationStartUpPath & "\System Files\Sounds\Goodbye.wav")
                    '    'Me.AxWindowsMediaPlayer1.playState.wmppsPlaying()
                    'End If
                    Con.Close()
                    Con.Dispose()
                    frmMainLogin.Close()
                Else
                    ' ''Start TFS4767 : Ayesha Rehman : 09-10-2018
                    'If dialouge.IsReleaseUpdaterForm = True Then
                    '    frmbg.Show()
                    '    dialouge.ShowDialog()
                    '    Exit Sub
                    'End If
                    ' ''End TFS4767
                    If e IsNot Nothing Then
                        e.Cancel = True
                        Exit Sub
                    End If
                End If

            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
        End If
        'frmMainLogin.Show()
    End Sub
    Sub EnableAutoBackup()
        Try
            Dim flag As Boolean = False

            Dim day As String = getConfigValueByType("BackupScheduleDays")
            Dim str() As String = day.Split("|")
            If str.Length > 0 Then
                For Each s As String In str
                    Dim strday() As String = s.Split("^")
                    If strday.Length > 0 Then
                        If strday(0) = Date.Now.ToString("ddd") AndAlso Convert.ToBoolean(strday(1)) = True Then
                            flag = True
                            Exit For
                        End If
                    End If
                Next
            End If

            If flag = False Then
                Exit Sub
            End If

            Dim schedule As String = getConfigValueByType("BackupSuitableTime")

            Dim schedularr() As String = schedule.Split("^")

            If schedularr.Length > 0 Then
                Dim str1 As String = schedularr(0)
                If str1 = "Any" Then

                    Timer10.Enabled = True

                Else
                    Dim strTimes() As String = schedularr(1).Split("|")
                    If strTimes.Length > 0 Then

                        BackupStartTime = strTimes(0)
                        BackupEndTime = strTimes(1)
                        If Date.Now.ToShortTimeString >= strTimes(0) And Date.Now.ToShortTimeString <= strTimes(1) Then
                            Timer10.Enabled = True
                        ElseIf CType(Date.Now.ToShortTimeString, Date) < CType(strTimes(0), Date) Then

                            Dim interveral As Integer = 60000 * DateDiff(DateInterval.Minute, CType(Date.Now.ToShortTimeString, Date), CType(strTimes(0), Date))

                            If interveral > 0 Then

                                Timer10.Interval = interveral
                                Timer10.Enabled = True

                            End If
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            '// Message displayed here because don't want to handle exception in calling function.
            msg_Error("Error in auto backup schedule settings: " & ex.Message)
        End Try
    End Sub
    Private Sub btnHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHome.Click, Button2.Click
        frmMainHome.TopLevel = False
        Me.pnlMain.Controls.Add(frmMainHome)
        frmMainHome.Show()
        frmMainHome.BringToFront()
        Me.pnlMain.VerticalScroll.Enabled = True
    End Sub


    Private Sub btnUserProfile_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserProfile.MouseHover
        ContextMenuStrip2.Show(btnUserProfile, 0, btnUserProfile.Height)
    End Sub


    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        'ContextMenuStrip1.Items(1).Enabled = False
        'If Me.ContextMenuStrip1.SelectedItems.Count = 0 Then
        '    e.Cancel = True
        'End If
    End Sub

    'Public Sub New()

    '    ' This call is required by the designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub

    Private Sub btnDismissMessage_Click(sender As Object, e As EventArgs) Handles btnDismissMessage.Click
        Try

            Me.pnlErrorNotification.Visible = False

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub AccountsToolStripMenuItem_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles AccountsToolStripMenuItem.DropDownItemClicked, ChartOfAccountToolStripMenuItem.DropDownItemClicked, CashAndBankToolStripMenuItem.DropDownItemClicked, SalesToolStripMenuItem1.DropDownItemClicked, PurchaseToolStripMenuItem1.DropDownItemClicked, InventoryToolStripMenuItem.DropDownItemClicked, ProductionToolStripMenuItem.DropDownItemClicked, MRPToolStripMenuItem.DropDownItemClicked, ImportToolStripMenuItem.DropDownItemClicked, HRToolStripMenuItem.DropDownItemClicked, PayrollToolStripMenuItem.DropDownItemClicked, CRMToolStripMenuItem.DropDownItemClicked, PMToolStripMenuItem.DropDownItemClicked, AssetsToolStripMenuItem.DropDownItemClicked, SitesToolStripMenuItem.DropDownItemClicked, ServicesToolStripMenuItem.DropDownItemClicked, ConfigurationToolStripMenuItem.DropDownItemClicked, AdministrationToolStripMenuItem.DropDownItemClicked, UtilitiesToolStripMenuItem.DropDownItemClicked, PropertyToolStripMenuItem.DropDownItemClicked, HelpToolStripMenuItem.DropDownItemClicked, AccountsToolStripMenuItem1.DropDownItemClicked, CashAndBankToolStripMenuItem1.DropDownItemClicked, SalesToolStripMenuItem4.DropDownItemClicked, PurchaseToolStripMenuItem4.DropDownItemClicked, InventoryToolStripMenuItem2.DropDownItemClicked, HRToolStripMenuItem1.DropDownItemClicked, PayrollToolStripMenuItem1.DropDownItemClicked, CRMToolStripMenuItem1.DropDownItemClicked, PMToolStripMenuItem1.DropDownItemClicked, ServicesToolStripMenuItem1.DropDownItemClicked, AdminToolStripMenuItem.DropDownItemClicked, InventoryToolStripMenuItem1.DropDownItemClicked, CustomersToolStripMenuItem.DropDownItemClicked, VendorsToolStripMenuItem.DropDownItemClicked, EmployeesConfigurationsToolStripMenuItem.DropDownItemClicked, OtherConfigurationToolStripMenuItem.DropDownItemClicked, ConfigurationsToolStripMenuItem.DropDownItemClicked, QCToolStripMenuItem.DropDownItemClicked, ProductionConfigurationToolStripMenuItem.DropDownItemClicked, ChartOfAccountsGroupsToolStripMenuItem.DropDownItemClicked, ApprovalHierarchyToolStripMenuItem.DropDownItemClicked, ProductionToolStripMenuItem2.DropDownItemClicked, CashConfigurationToolStripMenuItem.DropDownItemClicked, AssetsConfigurationToolStripMenuItem.DropDownItemClicked, AccountsConfigurationsToolStripMenuItem.DropDownItemClicked, PropertyToolStripMenuItem1.DropDownItemClicked, SalesInquiryRightsToolStripMenuItem.DropDownItemClicked, ReleaseDownloadToolStripMenuItem.DropDownItemClicked, AssetsToolStripMenuItem2.DropDownItemClicked, ReportsToolStripMenuItem.DropDownItemClicked, ImportToolStripMenuItem1.DropDownItemClicked, ComplaintManagmentToolStripMenuItem.DropDownItemClicked, LeadToolStripMenuItem.DropDownItemClicked, VendorProjectManagementToolStripMenuItem.DropDownItemClicked, CustomerProjectManagementToolStripMenuItem.DropDownItemClicked
        Try
            ContextMenuStrip1.Hide()
            CMenuReports.Hide()
            cmModProperty.Hide()
            ContextMenuStrip2.Hide()
            BackToolStripButton.Hide()
            FoldersToolStripButton.Hide()
            ShowForm(e.ClickedItem.Tag)
            'ApplyStyleSheet(fname)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If arHistory.Count > 1 Then

                arHistory.RemoveAt(0)
                ShowForm(arHistory.Item(0).ToString)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Sub AddHistoryItem(ByVal Caption As String, ByVal MyForm As String)
        Try

            Dim DIR As ToolStripDropDownItem
            DIR = Nothing
            For Each DI As ToolStripDropDownItem In Me.BackToolStripButton.Items
                DI.Visible = True
                If DI.Tag.ToString = MyForm.ToString Then
                    DIR = DI
                End If
            Next
            If Not DIR Is Nothing Then BackToolStripButton.Items.Remove(DIR)
            Dim DItem As ToolStripDropDownItem
            DItem = New ToolStripMenuItem
            DItem.Text = Caption.ToString & " [" & Date.Now.ToString("HH:mm:ss") & "]"
            DItem.Tag = MyForm
            DItem.Visible = False

            BackToolStripButton.Items.Insert(0, DItem)

            If BackToolStripButton.Items.Count > 1 Then
                ToolTip1.SetToolTip(Me.btnBack, "Switch back to " & BackToolStripButton.Items(1).Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub BackgroundWorker10_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker10.DoWork
        Dim objSMSLogList As New List(Of SMSLogBE)
        Try
            'Dim objWebClient As New Net.WebClient
            'Try
            If My.Computer.Network.IsAvailable = False Then Exit Sub 'objWebClient.OpenRead("http:\\www.google.com.pk")
            'Catch ex As Exception
            'BackgroundWorker10.ReportProgress(1)
            'Throw New Exception("No internet connection available.")
            'End Try
            'If Val(SoftwareVersion) <= 2 Then Exit Sub
            objSMSLogList = GetPendingSMS()
            If objSMSLogList Is Nothing Then Exit Sub
            If objSMSLogList.Count > 0 Then
                For Each objSMSLog As SMSLogBE In objSMSLogList
                    BackgroundWorker10.ReportProgress(1)
                    'Application.DoEvents()
                    SendBrandedSMS(objSMSLog)
                    BackgroundWorker10.ReportProgress(2)
                    'Application.DoEvents()
                Next
            Else
                Me.BackgroundWorker10.ReportProgress(3)
                'Application.DoEvents()
            End If

        Catch ex As Exception
            BackgroundWorker10.ReportProgress(4)
            'Application.DoEvents()
        Finally
            objSMSLogList.Clear()
        End Try
    End Sub

    Private Sub BackgroundWorker10_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker10.ProgressChanged
        Try
            lblMessage.Text = ""
            If e.ProgressPercentage = 1 Then
                lblMessage.text = "Sending Message..."
            ElseIf e.ProgressPercentage = 2 Then
                lblMessage.text = "Message Sent Sucessfully."
            ElseIf e.ProgressPercentage = 3 Then
                lblMessage.text = "No Pending Messages."
            ElseIf e.ProgressPercentage = 4 Then
                lblMessage.text = "While sending message an error."
                'ElseIf e.ProgressPercentage = 5 Then
                'Me.UltraStatusBar2.Panels("frmSMSLog").Text = "SMS Error: Agrius ERP is opened multiple times."
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker11_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker11.DoWork
        Try
            UploadAutoAttendance()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Timer6_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer6.Tick
        Try
            Timer6.Enabled = False
            If Not Environment.MachineName.ToUpper.ToString = getConfigValueByType("DNSHostForSMS").ToUpper.ToString Then
                Exit Sub
            End If
            If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
                'Me.UltraStatusBar2.Panels("frmSMSLog").Text = "SMS Error: Agrius ERP is opened multiple times."
                Exit Sub
            End If
            BackgroundWorker11.WorkerReportsProgress = True
            If BackgroundWorker11.IsBusy Then Exit Sub
            BackgroundWorker11.RunWorkerAsync()
            Do While BackgroundWorker11.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Timer6.Enabled = True
        End Try
    End Sub

    Private Sub Timer7_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer7.Tick
        Try
            Timer7.Enabled = False
            If Not Environment.MachineName.ToUpper.ToString = getConfigValueByType("DNSHostForSMS").ToUpper.ToString Then
                Exit Sub
            End If
            If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
                'Me.UltraStatusBar2.Panels("frmSMSLog").Text = "SMS Error: Agrius ERP is opened multiple times."
                Exit Sub
            End If
            Me.BackgroundWorker12.WorkerReportsProgress = True
            If Me.BackgroundWorker12.IsBusy Then Exit Sub
            Me.BackgroundWorker12.RunWorkerAsync()
            Do While Me.BackgroundWorker12.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Timer7.Enabled = True
        End Try
    End Sub

    Private Sub BackgroundWorker12_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker12.DoWork
        Try
            If getConfigValueByType("AutoBreakAttendance").ToString = "True" Then
                Dim blnStatus As Boolean = InsertBreakAttendance()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker13_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker13.DoWork
        Try
            CreateCurrentDBBackup()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Timer10_Tick(sender As Object, e As EventArgs) Handles Timer10.Tick
        Try

            Timer10.Enabled = False

            If HasBackup = True Then Exit Sub
            If BackgroundWorker20.IsBusy Then Exit Sub
            Me.BackgroundWorker20.RunWorkerAsync()
            Do While Me.BackgroundWorker20.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            ' Timer10.Enabled = True
        End Try
    End Sub

    Private Sub BackgroundWorker20_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker20.DoWork
        Try
            DatabaseBackupPro()
            ' HasBackup = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            'HasBackup = True
        End Try
    End Sub



    Private Sub cmModProperty_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles cmModProperty.ItemClicked, ContextMenuStrip2.ItemClicked, BackToolStripButton.ItemClicked, FoldersToolStripButton.ItemClicked
        Try
            cmModProperty.Hide()
            ContextMenuStrip2.Hide()
            BackToolStripButton.Hide()
            FoldersToolStripButton.Hide()
            ShowForm(e.ClickedItem.Tag)
            'ApplyStyleSheet(fname)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub BackToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.NextControlName = fname
    '        ' Me.ForwardToolStripButton.Enabled = True
    '        Me.BackToolStripButton.Enabled = False
    '        LastControlName.TopLevel = False
    '        LastControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
    '        LastControlName.Dock = DockStyle.Fill
    '        Me.pnlMain.Controls.Add(LastControlName)
    '        LastControlName.Show()
    '        LastControlName.BringToFront()

    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub btnFav_Click(sender As Object, e As EventArgs) Handles btnFav.Click
        Try
            If FoldersToolStripButton.Items.ContainsKey(fname.Name) AndAlso msg_Confirm("Do you want to remove [" & fname.Text & "] from favourites?") = False Then
                Exit Sub
            End If
            Dim dal As New SBDal.UtilityDAL
            dal.AddFormToFavourite(fname.Name, LoginUserId)

            Dim ToolItem As ToolStripDropDownItem
            ToolItem = New ToolStripMenuItem
            ToolItem.Name = fname.Name
            ToolItem.Text = fname.Text
            ToolItem.Tag = fname.Name

            If FoldersToolStripButton.Items.ContainsKey(fname.Name) Then
                FoldersToolStripButton.Items.RemoveByKey(fname.Name)
                btnFav.BackgroundImage = My.Resources.btn_fvrt
            Else
                FoldersToolStripButton.Items.Add(ToolItem)
                btnFav.BackgroundImage = My.Resources.btn_fvrt_active
                msg_Information(fname.Text & " is added to favourites")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFavList_MouseHover(sender As Object, e As EventArgs) Handles btnFavList.MouseHover, btnFavList.Click, Button4.MouseHover, Button4.Click
        Try
            FoldersToolStripButton.Show(Button4, 0, Button4.Height)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Sub GetFavouriteFormsList()
        Try
            Dim StrSql As String = "SELECT        tblForms.FormId, tblForms.FormName, tblForms.FormCaption, tblForms.FormModule, tblForms.SortOrder, tblForms.Active, tblForms.AccessKey FROM tblForms INNER JOIN tblFavouriteForms ON tblForms.FormName = tblFavouriteForms.FormName where tblFavouriteForms.UserId=" & LoginUserId & " ORDER BY tblForms.FormModule, tblForms.FormCaption"
            Dim dt As DataTable = GetDataTable(StrSql)
            For Each row As DataRow In dt.Rows
                Dim ToolItem As ToolStripDropDownItem
                ToolItem = New ToolStripMenuItem
                ToolItem.Name = row.Item("FormName").ToString
                ToolItem.Text = row.Item("FormCaption").ToString
                ToolItem.Tag = row.Item("FormName").ToString
                Me.FoldersToolStripButton.Items.Add(ToolItem)
            Next
        Catch ex As Exception
            ' Throw ex
        End Try
    End Sub

    Public Function RestrictSheet(ByVal MyForm As String) As Boolean
        Try

            RestrictSheetAccess = MyForm
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
            If Rights.FormName = fname.Name Or Rights.FormName = enm.ToString Then
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub BackgroundWorker8_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker8.DoWork
        Try
            Dim workingDays As Integer = 0I
            workingDays = GetWorkingDaysInCurrentMonth()
            '   UpdateWorkingDaysConfiguration(workingDays)

            getConfigValueList()
            GroupRights = GetRights(LoginUserId) 'New SBDal.GroupRightsBL().GetRights(LoginUserId)
            'If Not getConfigValueByType("NewSecurityRights").ToString = "Error" Then
            '    NewSecurityRights = Convert.ToBoolean(getConfigValueByType("NewSecurityRights").ToString)
            'End If
            'If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            '    flgCompanyRights = Convert.ToBoolean(getConfigValueByType("CompanyRights").ToString)
            'End If
            'If Not getConfigValueByType("TotalAmountRounding").ToString = "Error" Then
            '    TotalAmountRounding = Val(getConfigValueByType("TotalAmountRounding").ToString)
            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub BackgroundWorker8_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker8.RunWorkerCompleted
        Try
            IsEmailAttachment()
            EmailAlter()
            AdminEmails()
            ShowHeaderCompany()
            FileExportPath()

            _EmployeePicPath = getConfigValueByType("EmployeePicturePath").ToString
            _ArticlePicPath = getConfigValueByType("ArticlePicturePath").ToString
            _BackupDBPath = getConfigValueByType("BackupDBPath").ToString

            'Tsk:2359 Set Configuration Decimal Point In Value 
            If Not getConfigValueByType("DecimalPointInValue").ToString = "Error" Then
                DecimalPointInValue = getConfigValueByType("DecimalPointInValue").ToString
            End If
            If Not getConfigValueByType("DecimalPointInQty").ToString = "Error" Then
                DecimalPointInQty = getConfigValueByType("DecimalPointInQty").ToString
            End If

            ''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
            If Not getConfigValueByType("ItemSortOrder").ToString = "Error" Then
                ItemSortOrder = getConfigValueByType("ItemSortOrder").ToString
            End If
            If Not getConfigValueByType("ItemSortOrderByCode").ToString = "Error" Then
                ItemSortOrderByCode = getConfigValueByType("ItemSortOrderByCode").ToString
            End If
            If Not getConfigValueByType("ItemSortOrderByName").ToString = "Error" Then
                ItemSortOrderByName = getConfigValueByType("ItemSortOrderByName").ToString
            End If
            If Not getConfigValueByType("ItemAscending").ToString = "Error" Then
                ItemAscending = getConfigValueByType("ItemAscending").ToString
            End If
            If Not getConfigValueByType("ItemDescending").ToString = "Error" Then
                ItemDescending = getConfigValueByType("ItemDescending").ToString
            End If
            If Not getConfigValueByType("AcSortOrder").ToString = "Error" Then
                AcSortOrder = getConfigValueByType("AcSortOrder").ToString
            End If
            If Not getConfigValueByType("AcSortOrderByCode").ToString = "Error" Then
                AcSortOrderByCode = getConfigValueByType("AcSortOrderByCode").ToString
            End If
            If Not getConfigValueByType("AcSortOrderByName").ToString = "Error" Then
                AcSortOrderByName = getConfigValueByType("AcSortOrderByName").ToString
            End If
            If Not getConfigValueByType("AcAscending").ToString = "Error" Then
                AcAscending = getConfigValueByType("AcAscending").ToString
            End If
            If Not getConfigValueByType("AcDescending").ToString = "Error" Then
                AcDescending = getConfigValueByType("AcDescending").ToString
            End If
            'End Task:2452

            If Not getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                NewSecurityRights = Convert.ToBoolean(getConfigValueByType("NewSecurityRights").ToString)
            End If
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = Convert.ToBoolean(getConfigValueByType("CompanyRights").ToString)
            End If

            If Not getConfigValueByType("TotalAmountRounding").ToString = "Error" Then
                TotalAmountRounding = Val(getConfigValueByType("TotalAmountRounding").ToString)
            End If

        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function FindRestrictForm(ByVal FormName As String) As Boolean
        Try
            If RestrictForm.Trim.ToUpper = FormName.Trim.ToUpper Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetFormAccessByArray() As List(Of String)
        Try

            'Small Business
            'Corporate()
            'Enterprise()
            'Enterprise Plus
            'Custom()

            Dim strFormList As New List(Of String)
            If LicenseVersion = "" Then 'Basic Edition
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
            ElseIf LicenseVersion = "Small Business" Then 'Small Business Edition
                strFormList.Add("frmCostCenter")
                strFormList.Add("FrmEmailconfig")
                strFormList.Add("frmLeads")
                strFormList.Add("frmTasks")

            ElseIf LicenseVersion = "Corporate" Then 'Corporate Edition
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

            ElseIf LicenseVersion = "Enterprise" Then 'Enterprise Edition

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

            ElseIf LicenseVersion = "Enterprise Plus" Or LicenseVersion = "Custom" Then 'Enterprise Edition Plus

            End If
            Return strFormList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub LoadLayouts()

        For Each ctl As Control In fname.Controls
            If ctl.HasChildren Then Me.SaveLayouts(ctl)
            If TypeOf ctl Is UltraGrid Then
                Dim grd As UltraGrid = CType(ctl, UltraGrid)
                If System.IO.File.Exists(str_ApplicationStartUpPath & "\ApplicationSettings\" & fname.Name & "_" & ctl.Name & ".xml") Then
                    grd.DisplayLayout.LoadFromXml(str_ApplicationStartUpPath & "\ApplicationSettings\" & fname.Name & "_" & ctl.Name & ".xml")
                End If
            ElseIf TypeOf ctl Is UltraCombo Then
                Dim cmb As UltraCombo = CType(ctl, UltraCombo)
                If System.IO.File.Exists(str_ApplicationStartUpPath & "\ApplicationSettings\" & fname.Name & "_" & ctl.Name & ".xml") Then
                    cmb.DisplayLayout.LoadFromXml(str_ApplicationStartUpPath & "\ApplicationSettings\" & fname.Name & "_" & ctl.Name & ".xml")
                End If
            End If
        Next
    End Sub

    Private Sub SaveLayouts(ByVal control As Control)

        For Each ctl As Control In control.Controls
            If TypeOf ctl Is Form Then
                strControlName = ctl.Name & "_"
            End If
            If control.HasChildren Then Me.SaveLayouts(ctl)
            If TypeOf ctl Is UltraGrid Then
                Dim grd As UltraGrid = CType(ctl, UltraGrid)
                grd.DisplayLayout.SaveAsXml(str_ApplicationStartUpPath & "\ApplicationSettings\" & strControlName & ctl.Name & ".xml")
            ElseIf TypeOf ctl Is UltraCombo Then
                Dim cmb As UltraCombo = CType(ctl, UltraCombo)
                '  cmb.DisplayLayout.SaveAsXml(str_ApplicationStartUpPath & "\ApplicationSettings\" & strControlName & ctl.Name & ".xml")
            End If
        Next
    End Sub

    Private Sub btnReport_MouseHover(sender As Object, e As EventArgs) Handles btnReport.MouseHover, btnReport.Click, Button3.MouseHover, Button3.Click
        Try
            CMenuReports.Show(Button3, 0, Button3.Height)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Try
            frmModProperty_FormClosing(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Panel4_Click(sender As Object, e As EventArgs) Handles Panel4.Click, lblMessage.Click
        Try
            Dim smslog As New frmSMSLog
            smslog.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmModProperty_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Try
            If e.KeyCode = Keys.Enter Then
                If Not fname.Name = "frmSalesInquiry" AndAlso Not fname.Name = "frmPurchaseInquiry" AndAlso Not fname.Name = "frmVendorQuotation" AndAlso Not fname.Name = "frmAddChildItem" AndAlso Not fname.Name = "frmLoadJobCardCard" Then
                    SendKeys.Send("{TAB}")
                End If
            End If

            If e.KeyCode = Keys.I AndAlso e.Alt Then
                ShowForm("RecordSales")
            End If
            'Before against request no. RM6
            'If e.KeyCode = Keys.P AndAlso e.Alt Then
            If e.KeyCode = Keys.Y AndAlso e.Alt Then
                ShowForm("frmPurchase")
            End If
            If e.KeyCode = Keys.V AndAlso e.Alt Then
                ShowForm("frmVoucher")
            End If
            If e.KeyCode = Keys.L AndAlso e.Alt Then
                ShowForm("rptLedger")
            End If
            'Before gainst request no. RM6
            'If e.KeyCode = Keys.U AndAlso e.Alt Then
            If e.KeyCode = Keys.R AndAlso e.Alt Then
                ShowForm("StoreIssuence")
            End If
            'End R:M6
            If e.KeyCode = Keys.T AndAlso e.Alt Then
                ShowForm("frmSalesOrderNew")
            End If

            If e.Control And e.KeyCode = Keys.F Then
                frmSearchMenu._Menu = String.Empty
                frmSearchMenu.BringToFront()
                frmSearchMenu.ShowDialog()
            End If
            If e.KeyCode = Keys.F11 Then
                frmItemSearch.ShowDialog()
            End If

            If e.KeyCode = Keys.F12 Then
                frmSearchCustomersVendors.ShowDialog()
            End If

        Catch ex As Exception

            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub InterimPaymentCertificateIPCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InterimPaymentCertificateIPCToolStripMenuItem.Click

    End Sub


    Private Sub CustomerProjectManagementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerProjectManagementToolStripMenuItem.Click

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

    End Sub
End Class