''TFS3274 : Ayesha Rehman : 22-05-2018 :Configurations implementation on new design Accounts
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Public Class frmConfigAccounts
    Public isFormOpen As Boolean = False

    Private Sub linkAccountSecurity_Click(sender As Object, e As EventArgs) Handles linkAccountSecurity.Click
        Try
            If frmConfigAccountsSecurity.isFormOpen = True Then
                frmConfigAccountsSecurity.Dispose()
            End If

            frmConfigAccountsSecurity.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

  
    Private Sub linkAccountsAcc_Click(sender As Object, e As EventArgs) Handles linkAccountsAcc.Click
        Try
            If frmConfigAccountsAcc.isFormOpen = True Then
                frmConfigAccountsAcc.Dispose()
            End If

            frmConfigAccountsAcc.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmConfigAccounts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.isFormOpen = True
            FillCombos("Currency")
            getConfigValueList()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    
    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try
            Me.cmbVoucherFormat.Text = getConfigValueByType("VoucherNo").ToString
            Me.chkChequeDetailEnable.Checked = Convert.ToBoolean(getConfigValueByType("EnableChequeDetailOnVoucherEntry").ToString)
            Me.chkAutoChequebook.Checked = Convert.ToBoolean(getConfigValueByType("EnableAutoChequeBook").ToString)
            Me.chkGLAccountArticleDepartment.Checked = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment").ToString)
            Me.chkTotalAmountWiseInvoiceBasedVoucher.Checked = Convert.ToBoolean(getConfigValueByType("TotalAmountWiseInvoiceBasedVoucher").ToString)
            Me.chkDeductionWHTaxOnTotal.Checked = Convert.ToBoolean(getConfigValueByType("DeductionWHTaxOnTotal").ToString)
            Me.chkCashBankOptionDetail.Checked = Convert.ToBoolean(getConfigValueByType("CashAccountOptionForDetail").ToString)
            If Not getConfigValueByType("MemoRemarks").ToString = "Error" Then
                Me.chkMemoRemarks.Checked = Convert.ToBoolean(getConfigValueByType("MemoRemarks").ToString)
            End If
            Me.rbtAcSortOrder.Checked = Convert.ToBoolean(getConfigValueByType("AcSortOrder").ToString)
            Me.rbtAcSortOrderByCode.Checked = Convert.ToBoolean(getConfigValueByType("AcSortOrderByCode").ToString)
            Me.rbtAcSortOrderByName.Checked = Convert.ToBoolean(getConfigValueByType("AcSortOrderByName").ToString)
            Me.chkAcAscending.Checked = Convert.ToBoolean(getConfigValueByType("AcAscending").ToString)
            Me.chkAcDescending.Checked = Convert.ToBoolean(getConfigValueByType("AcDescending").ToString)
            Me.dtpEndOfDate.Value = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
            Me.chkCommentsChequeNo.Checked = Convert.ToBoolean(getConfigValueByType("CommentsChequeNo").ToString)
            Me.chkCommentsChequeDate.Checked = Convert.ToBoolean(getConfigValueByType("CommentsChequeDate").ToString)
            Me.chkCommentsPartyName.Checked = Convert.ToBoolean(getConfigValueByType("CommentsPartyName").ToString)
            ''PropertyFiltersOnReports
            If getConfigValueByType("PropertyFiltersOnReports").ToString <> "Error" Then
                Me.chkPropertyFiltersOnReports.Checked = Convert.ToBoolean(getConfigValueByType("PropertyFiltersOnReports").ToString)
            End If
            If Me.chkChequeDetailEnable.Checked = False Then
                Me.chkChequeDetailEnableNot.Checked = True
            End If
            If Me.chkAutoChequebook.Checked = False Then
                Me.chkAutoChequebookNot.Checked = True
            End If
            If Me.chkGLAccountArticleDepartment.Checked = False Then
                Me.chkGLAccountArticleDepartmentNot.Checked = True
            End If
            If Me.chkTotalAmountWiseInvoiceBasedVoucher.Checked = False Then
                Me.chkTotalAmountWiseInvoiceBasedVoucherNot.Checked = True
            End If
            If Me.chkDeductionWHTaxOnTotal.Checked = False Then
                Me.chkDeductionWHTaxOnTotalNot.Checked = True
            End If
            If Me.chkCashBankOptionDetail.Checked = False Then
                Me.chkCashBankOptionDetailNot.Checked = True
            End If
            Me.nudAmount.Value = Val(getConfigValueByType("DecimalPointInValue").ToString)
            Me.nudQty.Value = Val(getConfigValueByType("DecimalPointInQty").ToString)
            Me.nudTotalAmountRounding.Value = Val(getConfigValueByType("TotalAmountRounding").ToString)
            If IsCurrencyTransaction() = True Then
                Me.cmbCurrency.Enabled = False
                Me.cmbCurrency.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("Currency").ToString))
            Else
                Me.cmbCurrency.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillCombos(Optional ByVal Condition As String = "")

        Try
            Dim strSQL As String = String.Empty
            Select Case Condition
                  Case "Currency"
                  FillDropDown(Me.cmbCurrency, "Select Distinct currency_id, currency_code From tblCurrency")
            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkCommentsChequeDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkCommentsChequeDate.CheckedChanged, _
        chkCommentsChequeNo.CheckedChanged, chkCommentsPartyName.CheckedChanged
        Try
            frmConfigCompany.saveCheckConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtAcSortOrder_CheckedChanged(sender As Object, e As EventArgs) Handles rbtAcSortOrder.CheckedChanged, _
        rbtAcSortOrderByCode.CheckedChanged, rbtAcSortOrderByName.CheckedChanged, chkAcAscending.CheckedChanged, _
        chkAcDescending.CheckedChanged, chkAutoChequebook.CheckedChanged, _
        chkCashBankOptionDetail.CheckedChanged, chkChequeDetailEnable.CheckedChanged, chkDeductionWHTaxOnTotal.CheckedChanged, _
        chkGLAccountArticleDepartment.CheckedChanged, chkMemoRemarks.CheckedChanged, chkTotalAmountWiseInvoiceBasedVoucher.CheckedChanged, chkPropertyFiltersOnReports.CheckedChanged
        Try
            frmConfigCompany.saveRadioBtnConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpEndOfDate_Leave(sender As Object, e As EventArgs) Handles dtpEndOfDate.Leave
        Try
            If Me.dtpEndOfDate.Checked = True Then
                frmConfigCompany.saveDTPValueConfig(sender)
            End If
             Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub nudAmount_ValueChanged(sender As Object, e As EventArgs) Handles nudAmount.ValueChanged, nudQty.ValueChanged, nudTotalAmountRounding.ValueChanged
        Try
            frmConfigCompany.saveComboBoxNumConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCurrency_Leave(sender As Object, e As EventArgs) Handles cmbCurrency.Leave
        Try
            frmConfigCompany.saveComboBoxValueConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVoucherFormat_Leave(sender As Object, e As EventArgs) Handles cmbVoucherFormat.Leave
        Try
            frmConfigCompany.saveComboBoxConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class




