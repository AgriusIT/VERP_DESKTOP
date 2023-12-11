''TFS3274 : Ayesha Rehman : 22-05-2018 :Configurations implementation on new design Approval
''TFS4431 : Ayesha Rehman : 07-09-2018 Configure the approval hierarchy of invoice transfer.
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Public Class frmConfigApproval

    Public isFormOpen As Boolean = False


    Private Sub frmConfigApproval_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try


            Me.isFormOpen = True

            FillCombos("PurchaseApproval")
            FillCombos("PurchaseDemandApproval")
            FillCombos("PurchaseOrderApproval")
            FillCombos("PurchaseReturnApproval")
            FillCombos("GRNApproval")
            FillCombos("VoucherApproval")
            FillCombos("PaymentApproval")
            FillCombos("ReceiptApproval")
            FillCombos("ExpenseApproval")
            FillCombos("PurchaseInquiryApproval")
            FillCombos("VendorQuotationApproval")
            FillCombos("ActivityFeedBackApproval")
            FillCombos("SalesInquiryApproval")
            FillCombos("SalesQuotationApproval")
            FillCombos("SalesOrderApproval")
            FillCombos("DeliveryChallanApproval")
            FillCombos("SalesInvoiceApproval")
            FillCombos("SalesReturnApproval")
            FillCombos("CashRequestApproval")
            FillCombos("EmployeeLoanRequestApproval")
            FillCombos("SalesInvoiceTransferApproval") ''TFS4431

            getConfigValueList()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try
            Me.cmbPurchaseApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseApproval").ToString))
            Me.cmbPurchaseDemandApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseDemandApproval").ToString))
            Me.cmbPurchaseOrderApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseOrderApproval").ToString))
            Me.cmbPurchaseReturnApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseReturnApproval").ToString))
            Me.cmbPurchaseInquiryApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseInquiryApproval").ToString))
            Me.cmbGRNApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("GRNApproval").ToString))
            Me.cmbVendorQuotationApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("VendorQuotationApproval").ToString))
            Me.cmbActivityFeedBackApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ActivityFeedBackApproval").ToString))
            Me.cmbVoucherEntryApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("VoucherEntryApproval").ToString))
            Me.cmbPaymentApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PaymentApproval").ToString))
            Me.cmbReceiptApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ReceiptApproval").ToString))
            Me.cmbExpenseApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ExpenseApproval").ToString))
            Me.cmbSalesInquiry.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesInquiryApproval").ToString))
            Me.cmbSalesQuotation.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesQuotationApproval").ToString))
            Me.cmbSalesOrder.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesOrderApproval").ToString))
            Me.cmbDeliveryChallan.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("DeliveryChallanApproval").ToString))
            Me.cmbSalesInvoice.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesInvoiceApproval").ToString))
            Me.cmbSalesReturn.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesReturnApproval").ToString))
            Me.cmbCashRequestApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CashRequestApproval").ToString))
            Me.cmbEmployeeLoanRequestApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("EmployeeLoanRequestApproval").ToString))
            Me.cmbSalesInvoiceTransfer.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesInvoiceTranferApproval").ToString))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "")

        Try
            Dim strSQL As String = String.Empty
            Select Case Condition

                Case "PurchaseApproval"
                    FillDropDown(Me.cmbPurchaseApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "PurchaseDemandApproval"
                    FillDropDown(Me.cmbPurchaseDemandApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "PurchaseOrderApproval"
                    FillDropDown(Me.cmbPurchaseOrderApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "PurchaseReturnApproval"
                    FillDropDown(Me.cmbPurchaseReturnApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "GRNApproval"
                    FillDropDown(Me.cmbGRNApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "PurchaseInquiryApproval"
                    FillDropDown(Me.cmbPurchaseInquiryApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "VendorQuotationApproval"
                    FillDropDown(Me.cmbVendorQuotationApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "VoucherApproval"
                    FillDropDown(Me.cmbVoucherEntryApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "PaymentApproval"
                    FillDropDown(Me.cmbPaymentApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "ReceiptApproval"
                    FillDropDown(Me.cmbReceiptApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "ExpenseApproval"
                    FillDropDown(Me.cmbExpenseApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "ActivityFeedBackApproval"
                    FillDropDown(Me.cmbActivityFeedBackApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "ActivityFeedBackApproval"
                    FillDropDown(Me.cmbActivityFeedBackApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "SalesInquiryApproval"
                    FillDropDown(Me.cmbSalesInquiry, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "SalesQuotationApproval"
                    FillDropDown(Me.cmbSalesQuotation, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "SalesOrderApproval"
                    FillDropDown(Me.cmbSalesOrder, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "DeliveryChallanApproval"
                    FillDropDown(Me.cmbDeliveryChallan, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "SalesInvoiceApproval"
                    FillDropDown(Me.cmbSalesInvoice, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "SalesReturnApproval"
                    FillDropDown(Me.cmbSalesReturn, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "CashRequestApproval"
                    FillDropDown(Me.cmbCashRequestApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "EmployeeLoanRequestApproval"
                    FillDropDown(Me.cmbEmployeeLoanRequestApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "SalesInvoiceTransferApproval"
                    FillDropDown(Me.cmbSalesInvoiceTransfer, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")

            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbDeliveryChallan_Leave(sender As Object, e As EventArgs) Handles cmbDeliveryChallan.Leave, cmbSalesInquiry.Leave, cmbActivityFeedBackApproval.Leave, cmbExpenseApproval.Leave, _
        cmbActivityFeedBackApproval.Leave, cmbSalesInvoice.Leave, cmbSalesOrder.Leave, cmbEmployeeLoanRequestApproval.Leave, cmbCashRequestApproval.Leave, _
        cmbSalesReturn.Leave, cmbGRNApproval.Leave, cmbPurchaseApproval.Leave, cmbPaymentApproval.Leave, cmbPurchaseOrderApproval.Leave, _
        cmbPurchaseDemandApproval.Leave, cmbPurchaseReturnApproval.Leave, cmbSalesQuotation.Leave, cmbVendorQuotationApproval.Leave, cmbVoucherEntryApproval.Leave, cmbReceiptApproval.Leave, _
        cmbPurchaseInquiryApproval.Leave, cmbSalesInvoiceTransfer.Leave
        Try
            frmConfigCompany.saveComboBoxValueConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

   
End Class