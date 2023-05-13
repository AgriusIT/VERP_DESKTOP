Public Class frmConfigPurchaseAccount

    Public isFormOpen As Boolean = False

    Private Sub frmConfigPurchaseAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillCombos("PurchaseAccount")
        FillCombos("PurchaseTaxAccount")
        FillCombos("ReceiveableTax")
        FillCombos("InwardExpense")
        FillCombos("PurchaseTaxDeductionAcId")
        FillCombos("AdditionalTax")
        FillCombos("RetentionAccount")
        FillCombos("VendorDifferenceQty")
        FillCombos("MobilizationAccount")
        FillCombos("PurchaseDiscountAccount")
        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try

            Me.chkPaymentVoucherOnPurchase.Checked = Convert.ToBoolean(getConfigValueByType("PaymentVoucherOnPurchase").ToString)

            If Me.chkPaymentVoucherOnPurchase.Checked = False Then
                Me.chkPaymentVoucherOnPurchaseNot.Checked = True
            End If

            Me.cmbPurchaseAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseDebitAccount").ToString))

            Me.cmbPurchaseTaxAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseTaxDebitAccountId").ToString))

            Me.cmbTaxReceiveableAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("TaxreceiveableACid").ToString))

            Me.cmbInwardExpenseHeadAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("InwardExpHeadAcId").ToString))

            Me.chkTransporterCharges.Checked = Convert.ToBoolean(getConfigValueByType("TransaportationChargesVoucher").ToString)

            If Me.chkTransporterCharges.Checked = False Then
                Me.chkTransporterChargesNot.Checked = True
            End If

            Me.CmbPurchaseTaxIDeductionAccountNo.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseTaxDeductionAcId").ToString))

            Me.chkPurchaseAccountFrontEnd.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseAccountFrontEnd").ToString)

            If Me.chkPurchaseAccountFrontEnd.Checked = False Then
                Me.chkPurchaseAccountFrontEndNot.Checked = True
            End If

            Me.cmbAdditionalTaxAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AdditionalTaxAcId").ToString))

            Me.cmbRetentionAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("RetentionAccount").ToString))

            Me.cmbVDQty.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("VendorDifferenceQty").ToString))

            Me.cmbMobilizationAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("MobilizationAccount").ToString))

            Me.cmbPurchaseDiscountAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseDiscountAccount").ToString))

            Dim cbValues As String = String.Empty
            cbValues = getConfigValueByType("COAGroupWithPurchaseModule").ToString()
            If cbValues.Length > 0 Then
                Dim arday() As String = cbValues.Split("|")
                If arday.Length > 4 Then
                    'PD&False|PO&True|GRN&True|Purchase&True|PR&True
                    cbPurchaseDemand.Checked = Convert.ToBoolean(arday(0).Trim.Substring(3))
                    cbPurchaseOrder.Checked = Convert.ToBoolean(arday(1).Trim.Substring(3))
                    cbGRN.Checked = Convert.ToBoolean(arday(2).Trim.Substring(4))
                    cbPurchase.Checked = Convert.ToBoolean(arday(3).Trim.Substring(9))
                    cbPurchaseReturn.Checked = Convert.ToBoolean(arday(4).Trim.Substring(3))
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

                Case "PurchaseAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc "
                    FillDropDown(Me.cmbPurchaseAccount, strSQL, True)

                Case "PurchaseTaxAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbPurchaseTaxAccount, strSQL, True)

                Case "ReceiveableTax"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbTaxReceiveableAccount, strSQL, True)

                Case "InwardExpense"
                    strSQL = "Select main_sub_sub_id, sub_sub_title From tblCOAMainSubSub Order By 2 Asc"
                    FillDropDown(Me.cmbInwardExpenseHeadAccount, strSQL)

                Case "PurchaseTaxDeductionAcId"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.CmbPurchaseTaxIDeductionAccountNo, strSQL, True)

                Case "AdditionalTax"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbAdditionalTaxAccount, strSQL, True)

                Case "RetentionAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc "
                    FillDropDown(Me.cmbRetentionAccount, strSQL, True)

                Case "VendorDifferenceQty"
                    FillDropDown(Me.cmbVDQty, "SELECT coa_detail_id,detail_title FROM tblCOAMainSubSubDetail")

                Case "MobilizationAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc "
                    FillDropDown(Me.cmbMobilizationAccount, strSQL, True)
                Case "PurchaseDiscountAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbPurchaseDiscountAccount, strSQL, True)

            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkGeneral_Click(sender As Object, e As EventArgs) Handles linkGeneral.Click
        Try
            If frmConfigPurchase.isFormOpen = True Then
                frmConfigPurchase.Dispose()
            End If

            frmConfigPurchase.ShowDialog()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkSecurity_Click(sender As Object, e As EventArgs) Handles linkSecurity.Click
        Try
            If frmConfigPurchaseSecurity.isFormOpen = True Then
                frmConfigPurchaseSecurity.Dispose()
            End If

            frmConfigPurchaseSecurity.ShowDialog()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkShowCustomerOnPurchase_CheckedChanged(sender As Object, e As EventArgs) Handles chkPaymentVoucherOnPurchase.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub cmbPurchaseAccount_Leave(sender As Object, e As EventArgs) Handles cmbPurchaseAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub PurchaseTaxDebitAccountId_Leave(sender As Object, e As EventArgs)
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbTaxReceiveableAccount_Leave(sender As Object, e As EventArgs) Handles cmbTaxReceiveableAccount.Leave, cmbPurchaseTaxAccount.Leave, cmbPurchaseDiscountAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbInwardExpenseHeadAccount_Leave(sender As Object, e As EventArgs) Handles cmbInwardExpenseHeadAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub chkTransporterCharges_CheckedChanged(sender As Object, e As EventArgs) Handles chkTransporterCharges.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub CmbPurchaseTaxIDeductionAccountNo_Leave(sender As Object, e As EventArgs)
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub chkPurchaseAccountFrontEnd_CheckedChanged(sender As Object, e As EventArgs) Handles chkPurchaseAccountFrontEnd.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub cmbAdditionalTaxAccount_Leave(sender As Object, e As EventArgs) Handles cmbAdditionalTaxAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbRetentionAccount_Leave(sender As Object, e As EventArgs) Handles cmbRetentionAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbVDQty_Leave(sender As Object, e As EventArgs) Handles cmbVDQty.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbMobilizationAccount_Leave(sender As Object, e As EventArgs) Handles cmbMobilizationAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub CmbPurchaseTaxIDeductionAccountNo_Leave_1(sender As Object, e As EventArgs) Handles CmbPurchaseTaxIDeductionAccountNo.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

   
    Private Sub cbGRN_CheckedChanged(sender As Object, e As EventArgs) Handles cbGRN.CheckedChanged, cbPurchase.CheckedChanged, cbPurchaseDemand.CheckedChanged, _
        cbPurchaseOrder.CheckedChanged, cbPurchaseReturn.CheckedChanged
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim strValues As String = String.Empty
            ''Save Configuration For COA Mapping : Ayesha Rehman : TFS3322
            strValues += "PD^" & cbPurchaseDemand.Checked & "|"
            strValues += "PO^" & cbPurchaseOrder.Checked & "|"
            strValues += "GRN^" & cbGRN.Checked & "|"
            strValues += "Purchase^" & cbPurchase.Checked & "|"
            strValues += "PR^" & cbPurchaseReturn.Checked


            If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, strValues)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class