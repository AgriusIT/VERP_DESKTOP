''TFS3274 : Ayesha Rehman : 22-05-2018 :Configurations implementation on new design Accounts
Public Class frmConfigAccountsAcc

    Public isFormOpen As Boolean = False


    Private Sub linkAccount_Click(sender As Object, e As EventArgs) Handles linkAccount.Click
        Try
            If frmConfigAccounts.isFormOpen = True Then
                frmConfigAccounts.Dispose()
            End If

            frmConfigAccounts.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

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


    Private Sub frmConfigAccountsAcc_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.isFormOpen = True
        FillCombos("PLAccount")
        FillCombos("MainAccountforRevenueImport")
        FillCombos("ClaimAccount")
        FillCombos("CMFAExpAcHead")
        FillCombos("LeadProfileAccountHead")


        getConfigValueList()
        GetAllRecords()
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try
            Me.cmbPLAccountId.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PLAccountId").ToString))
            Me.cmbMainAccountforRevenueImport.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("MainAccountforRevenueImport").ToString))
            Me.cmbCMFAExpAccountHead.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CMFAExpAccountHead").ToString))
            Me.cmbClaimAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ClaimAccountId").ToString))
            Me.cmbLeadProfileHeadAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("LeadProfileHeadAccount").ToString))
            Me.chkInvoiceWiseTax.Checked = Convert.ToBoolean(getConfigValueByType("InvoiceWiseTaxPercent").ToString)
            '' TFS3411 : Ayesha Rehman :  Restrict entry in Parent Account not included in new design.
            If Not getConfigValueByType("RestrictEntryInParentDetailAC").ToString = "Error" Then
                Me.chkRestrictEntryInParentDetailAC.Checked = Convert.ToBoolean(getConfigValueByType("RestrictEntryInParentDetailAC").ToString)
            End If
            If Me.chkInvoiceWiseTax.Checked = False Then
                Me.chkInvoiceWiseTaxNot.Checked = True
            End If
            If Me.chkRestrictEntryInParentDetailAC.Checked = False Then
                Me.chkRestrictEntryInParentDetailACNot.Checked = True
            End If
            If Not getConfigValueByType("DuplicateAccountName").ToString = "Error" Then
                Me.chkDuplicateAccountName.Checked = Convert.ToBoolean(getConfigValueByType("DuplicateAccountName").ToString)
            End If
            If Me.chkDuplicateAccountName.Checked = False Then
                Me.chkDuplicateAccountNameNot.Checked = True
            End If
            Dim cbValues As String = String.Empty
            cbValues = getConfigValueByType("COAGroupWithAccountsModule").ToString()
            If cbValues.Length > 0 Then
                Dim arday() As String = cbValues.Split("|")
                If arday.Length > 3 Then
                    'Vocher&False|Payment&True|Receipt&True|Expense&True
                    cbVocher.Checked = Convert.ToBoolean(arday(0).Trim.Substring(8))
                    cbPayment.Checked = Convert.ToBoolean(arday(1).Trim.Substring(8))
                    cbReceipt.Checked = Convert.ToBoolean(arday(2).Trim.Substring(8))
                    cbExpense.Checked = Convert.ToBoolean(arday(3).Trim.Substring(8))

                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "")

        Try
            Dim strSQL As String = String.Empty
            Select Case Condition
                Case "PLAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbPLAccountId, strSQL, True)
                Case "MainAccountforRevenueImport"
                    strSQL = "SELECT  coa_main_id,  main_title +' - '+ main_code as main_title, main_type FROM         dbo.tblCOAMain order by 2"
                    FillDropDown(Me.cmbMainAccountforRevenueImport, strSQL, True)
                Case "ClaimAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbClaimAccount, strSQL, True)
                Case "CMFAExpAcHead"
                    strSQL = "SELECT main_sub_sub_Id, sub_sub_title, sub_sub_code From tblCOAMainSubsub WHERE Account_Type='Expense'"
                    FillDropDown(Me.cmbCMFAExpAccountHead, strSQL)

                Case "LeadProfileAccountHead"
                    strSQL = "SELECT main_sub_sub_Id, sub_sub_title, sub_sub_code From tblCOAMainSubsub WHERE Account_Type='Customer'"
                    FillDropDown(Me.cmbLeadProfileHeadAccount, strSQL)
            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbMainAccountforRevenueImport_Leave(sender As Object, e As EventArgs) Handles cmbMainAccountforRevenueImport.Leave, cmbClaimAccount.Leave, _
        cmbCMFAExpAccountHead.Leave, cmbPLAccountId.Leave, cmbLeadProfileHeadAccount.Leave
        Try
            frmConfigCompany.saveComboBoxValueConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkInvoiceWiseTax_CheckedChanged(sender As Object, e As EventArgs) Handles chkInvoiceWiseTax.CheckedChanged, chkRestrictEntryInParentDetailAC.CheckedChanged, chkDuplicateAccountName.CheckedChanged
        Try
            frmConfigCompany.saveRadioBtnConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cbExpense_CheckedChanged(sender As Object, e As EventArgs) Handles cbExpense.CheckedChanged, cbPayment.CheckedChanged, cbReceipt.CheckedChanged, _
        cbVocher.CheckedChanged
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim strValues As String = String.Empty

            strValues += "Voucher^" & cbVocher.Checked & "|"
            strValues += "Payment^" & cbPayment.Checked & "|"
            strValues += "Receipt^" & cbReceipt.Checked & "|"
            strValues += "Expense^" & cbExpense.Checked

            If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, strValues)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class