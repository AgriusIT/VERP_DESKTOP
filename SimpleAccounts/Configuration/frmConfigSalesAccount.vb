Public Class frmConfigSalesAccount

    Implements IGeneral

    Public isFormOpen As Boolean = False
    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty
    Dim IsChangedValue As Object


    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        If Not getConfigValueByType("ReceiptVoucherOnSales").ToString = "Error" Then
            Me.rdoReceiptVoucherOnSales.Checked = Convert.ToBoolean(getConfigValueByType("ReceiptVoucherOnSales").ToString)
        End If
        If rdoReceiptVoucherOnSales.Checked = False Then
            Me.rdoReceiptVoucherOnSalesNo.Checked = True
        End If


        If Not getConfigValueByType("DiscountVoucherOnSale").ToString = "Error" Then
            Me.rdoDiscountVoucherOnSale.Checked = Convert.ToBoolean(getConfigValueByType("DiscountVoucherOnSale").ToString)
        End If
        If rdoDiscountVoucherOnSale.Checked = False Then
            Me.rdoDiscountVoucherOnSaleNo.Checked = True
        End If


        If Not getConfigValueByType("MarketReturnVoucher").ToString = "Error" Then
            Me.rdoMarketReturnVoucher.Checked = Convert.ToBoolean(getConfigValueByType("MarketReturnVoucher").ToString)
        End If
        If rdoMarketReturnVoucher.Checked = False Then
            Me.rdoMarketReturnVoucherNo.Checked = True
        End If


        If Not getConfigValueByType("ExpenseChargeToCustomer").ToString = "Error" Then
            Me.rdoExpenseChargeToCustomer.Checked = Convert.ToBoolean(getConfigValueByType("ExpenseChargeToCustomer").ToString)
        End If
        If rdoExpenseChargeToCustomer.Checked = False Then
            Me.rdoExpenseChargeToCustomerNo.Checked = True
        End If

        If Not getConfigValueByType("CommentCustomerFormat").ToString = "Error" Then
            Me.chkCommentCustomerFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentCustomerFormat").ToString)
        End If


        If Not getConfigValueByType("CommentArticleFormat").ToString = "Error" Then
            Me.chkCommentArticleDescriptionFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentArticleFormat").ToString)
        End If


        If Not getConfigValueByType("CommentArticleSizeFormat").ToString = "Error" Then
            Me.chkCommentArticleSizeFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentArticleSizeFormat").ToString)
        End If


        If Not getConfigValueByType("CommentArticleColorFormat").ToString = "Error" Then
            Me.chkCommentColorFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentArticleColorFormat").ToString)
        End If


        If Not getConfigValueByType("CommentQtyFormat").ToString = "Error" Then
            Me.chkCommentQtyFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentQtyFormat").ToString)
        End If


        If Not getConfigValueByType("CommentRemarksFormat").ToString = "Error" Then
            Me.chkCommentsRemarksFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentRemarksFormat").ToString)
        End If


        If Not getConfigValueByType("CommentSalesDCNo").ToString = "Error" Then
            Me.chkCommentSalesDCNo.Checked = Convert.ToBoolean(getConfigValueByType("CommentSalesDCNo").ToString)
        End If


        If Not getConfigValueByType("CommentPriceFormat").ToString = "Error" Then
            Me.chkCommentPriceFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentPriceFormat").ToString)
        End If


        If Not getConfigValueByType("CommentEngineNo").ToString = "Error" Then
            Me.chkCommentEngineNo.Checked = Convert.ToBoolean(getConfigValueByType("CommentEngineNo").ToString)
        End If


        If Not getConfigValueByType("BiltyCommentsSales").ToString = "Error" Then
            Me.chkBiltyNo.Checked = Convert.ToBoolean(getConfigValueByType("BiltyCommentsSales").ToString)
        End If


        If Not getConfigValueByType("TransporterCommentsSales").ToString = "Error" Then
            Me.chkTransporter.Checked = Convert.ToBoolean(getConfigValueByType("TransporterCommentsSales").ToString)
        End If

        'If Decrypt(getConfigValueByType("AdditionalTaxPercentage").ToString) <> "Error" Then
        '    Me.txtTransitInssuranceTax.Text = Decrypt(getConfigValueByType("AdditionalTaxPercentage").ToString)
        'Else
        '    Me.txtTransitInssuranceTax.Text = String.Empty
        'End If
        Me.txtTransitInssuranceTax.Text = getConfigValueByType("TransitInssuranceTax").ToString
        'If Decrypt(getConfigValueByType("WHTax").ToString) <> "Error" Then
        '    Me.txtTransitInssuranceTax.Text = Decrypt(getConfigValueByType("WHTax").ToString)
        'Else
        '    Me.txtTransitInssuranceTax.Text = String.Empty
        'End If
        Me.txtWHTax.Text = getConfigValueByType("WHTax_Percentage").ToString
        'If getConfigValueByType("DefaulttaxPercentage").ToString <> "Error" Then
        '    Me.txtDefaultTax.Text = Decrypt(getConfigValueByType("DefaulttaxPercentage").ToString)
        'Else
        '    Me.txtDefaultTax.Text = String.Empty
        'End If
        Me.txtDefaultTax.Text = getConfigValueByType("Default_Tax_Percentage").ToString

        Me.cmbSalesAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesCreditAccount").ToString))
        Me.SalesTaxAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesTaxCreditAccount").ToString))
        Me.cmbTaxPayableAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("taxpayableACid").ToString))
        Me.cmbFuelExpAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("FuelExpAccount").ToString))
        Me.cmbOtherExpAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("OtherExpAccount").ToString))
        Me.cmbSEDAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SEDAccountId").ToString))
        Me.cmbTransitInsuranceAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("TransitInsuranceAccountId").ToString))
        Me.cmbSalesDiscAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesDiscountAccount").ToString))
        Me.cmbDefaultAccountInPlaceCustomer.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("DefaultAccountInPlaceCustomer").ToString))
        Me.cmbSaleTaxDeductioAcId.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SaleTaxDeductionAcId").ToString))

        Dim cbValues As String = String.Empty
        cbValues = getConfigValueByType("COAGroupWithSalesModule").ToString()
        If cbValues.Length > 0 Then
            Dim arday() As String = cbValues.Split("|")
            If arday.Length > 4 Then
                'Quotation&False|SalesOrder&True|DeliveryChalan&True|Sales&True|SalesReturn&True
                cbQuotation.Checked = Convert.ToBoolean(arday(0).Trim.Substring(10))
                cbSalesOrder.Checked = Convert.ToBoolean(arday(1).Trim.Substring(11))
                cbDeliveryChalan.Checked = Convert.ToBoolean(arday(2).Trim.Substring(15))
                cbSales.Checked = Convert.ToBoolean(arday(3).Trim.Substring(6))
                cbSalesReturn.Checked = Convert.ToBoolean(arday(4).Trim.Substring(12))
            End If
        End If

        If Not getConfigValueByType("CGSVoucher").ToString = "Error" Then
            Me.chkCostofsalevoucher.Checked = Convert.ToBoolean(getConfigValueByType("CGSVoucher").ToString)
        End If
        If chkCostofsalevoucher.Checked = False Then
            Me.chkCostofsalevoucherNot.Checked = True
        End If

        If Not getConfigValueByType("ExcludeTaxPrice").ToString = "Error" Then
            Me.chkTaxExcludePrice.Checked = Convert.ToBoolean(getConfigValueByType("ExcludeTaxPrice").ToString)
        End If
        If chkTaxExcludePrice.Checked = False Then
            Me.chkTaxExcludePriceNot.Checked = True
        End If

        If Not getConfigValueByType("ApplyFlatDiscountOnSale").ToString = "Error" Then
            Me.chkApplyFlatDiscountOnSale.Checked = Convert.ToBoolean(getConfigValueByType("ApplyFlatDiscountOnSale").ToString)
        End If
        If chkApplyFlatDiscountOnSale.Checked = False Then
            Me.chkApplyFlatDiscountOnSaleNot.Checked = True
        End If

        If Not getConfigValueByType("ShowMiscAccountsOnSales").ToString = "Error" Then
            Me.chkShowMiscAccountsOnSales.Checked = Convert.ToBoolean(getConfigValueByType("ShowMiscAccountsOnSales").ToString)
        End If
        If chkShowMiscAccountsOnSales.Checked = False Then
            Me.chkShowMiscAccountsOnSalesNot.Checked = True
        End If

        Me.cmbAdjustmentExpAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AdjustmentExpAccount").ToString))

    End Sub

    Private Sub frmConfigSalesAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        btnAdd.FlatAppearance.BorderSize = 0

        Me.isFormOpen = True

        getConfigValueList()
        Try
            FillCombos("SalesAccount")
            FillCombos("SalesTaxAccount")
            FillCombos("PayableTax")
            FillCombos("FuelExpAccount")
            FillCombos("OtherExpAccount")
            FillCombos("SEDAccount")
            FillCombos("TransitInsurance")
            FillCombos("SalesDiscount")
            FillCombos("DefaultAccountInPlaceCustomer")
            FillCombos("SaleTaxDeductionAcId")
            FillCombos("AdjustmentExpAccount")
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading" & ex.Message)
        End Try
        GetAllRecords()
    End Sub

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty
            Select Case Condition
                Case "SalesAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbSalesAccount, strSQL, True)
                Case "SalesTaxAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.SalesTaxAccount, strSQL, True)
                Case "PayableTax"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbTaxPayableAccount, strSQL, True)
                Case "FuelExpAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbFuelExpAccount, strSQL, True)
                Case "OtherExpAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbOtherExpAccount, strSQL, True)
                Case "SEDAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbSEDAccount, strSQL, True)
                Case "TransitInsurance"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbTransitInsuranceAccount, strSQL, True)
                Case "SalesDiscount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbSalesDiscAccount, strSQL, True)
                Case "DefaultAccountInPlaceCustomer"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbDefaultAccountInPlaceCustomer, strSQL, True)
                Case "SaleTaxDeductionAcId"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbSaleTaxDeductioAcId, strSQL, True)
                Case "AdjustmentExpAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbAdjustmentExpAccount, strSQL, True)
            End Select
        Catch ex As Exception

        End Try
    End Sub


    Private Sub rdoReceiptVoucherOnSales_CheckedChanged(sender As Object, e As EventArgs) Handles rdoReceiptVoucherOnSales.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoDiscountVoucherOnSale_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDiscountVoucherOnSale.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoMarketReturnVoucher_CheckedChanged(sender As Object, e As EventArgs) Handles rdoMarketReturnVoucher.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoExpenseChargeToCustomer_CheckedChanged(sender As Object, e As EventArgs) Handles rdoExpenseChargeToCustomer.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkCommentCustomerFormat_CheckedChanged(sender As Object, e As EventArgs) Handles chkCommentCustomerFormat.CheckedChanged
        Dim chk As Windows.Forms.CheckBox
        chk = CType(sender, CheckBox)
        If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
    End Sub

    Private Sub chkCommentArticleDescriptionFormat_CheckedChanged(sender As Object, e As EventArgs) Handles chkCommentArticleDescriptionFormat.CheckedChanged
        Dim chk As Windows.Forms.CheckBox
        chk = CType(sender, CheckBox)
        If chk.Tag IsNot Nothing Then
            If chk.Tag.ToString.Length > 0 Then
                frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
            End If
        End If
    End Sub

    Private Sub chkCommentArticleSizeFormat_CheckedChanged(sender As Object, e As EventArgs) Handles chkCommentArticleSizeFormat.CheckedChanged
        Dim chk As Windows.Forms.CheckBox
        chk = CType(sender, CheckBox)
        If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
    End Sub

    Private Sub chkCommentColorFormat_CheckedChanged(sender As Object, e As EventArgs) Handles chkCommentColorFormat.CheckedChanged
        Dim chk As Windows.Forms.CheckBox
        chk = CType(sender, CheckBox)
        If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
    End Sub

    Private Sub chkCommentQtyFormat_CheckedChanged(sender As Object, e As EventArgs) Handles chkCommentQtyFormat.CheckedChanged
        Dim chk As Windows.Forms.CheckBox
        chk = CType(sender, CheckBox)
        If chk.Tag IsNot Nothing Then
            If chk.Tag.ToString.Length > 0 Then
                frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
            End If
        End If
    End Sub

    Private Sub chkCommentsRemarksFormat_CheckedChanged(sender As Object, e As EventArgs) Handles chkCommentsRemarksFormat.CheckedChanged
        Dim chk As Windows.Forms.CheckBox
        chk = CType(sender, CheckBox)
        If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
    End Sub

    Private Sub chkCommentSalesDCNo_CheckedChanged(sender As Object, e As EventArgs) Handles chkCommentSalesDCNo.CheckedChanged
        Dim chk As Windows.Forms.CheckBox
        chk = CType(sender, CheckBox)
        If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
    End Sub

    Private Sub chkCommentPriceFormat_CheckedChanged(sender As Object, e As EventArgs) Handles chkCommentPriceFormat.CheckedChanged
        Try
            Dim chk As Windows.Forms.CheckBox
            chk = CType(sender, CheckBox)

            If chk.Tag IsNot Nothing Then
                If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        
    End Sub

    Private Sub chkCommentEngineNo_CheckedChanged(sender As Object, e As EventArgs) Handles chkCommentEngineNo.CheckedChanged
        Dim chk As Windows.Forms.CheckBox
        chk = CType(sender, CheckBox)
        If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
    End Sub

    Private Sub chkBiltyNo_CheckedChanged(sender As Object, e As EventArgs) Handles chkBiltyNo.CheckedChanged
        Dim chk As Windows.Forms.CheckBox
        chk = CType(sender, CheckBox)
        If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
    End Sub

    Private Sub chkTransporter_CheckedChanged(sender As Object, e As EventArgs) Handles chkTransporter.CheckedChanged
        Dim chk As Windows.Forms.CheckBox
        chk = CType(sender, CheckBox)
        If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords1(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub cmbSalesAccount_Enter(sender As Object, e As EventArgs) Handles cmbSalesAccount.Enter, SalesTaxAccount.Enter, cmbTaxPayableAccount.Enter, cmbFuelExpAccount.Enter, cmbOtherExpAccount.Enter, cmbSEDAccount.Enter, cmbTransitInsuranceAccount.Enter, cmbSalesDiscAccount.Enter, cmbDefaultAccountInPlaceCustomer.Enter, cmbSaleTaxDeductioAcId.Enter
        Try
            Dim cmb As ComboBox = CType(sender, ComboBox)
            If cmb.Items.Count > 0 Then
                If cmb.SelectedValue IsNot Nothing Then IsChangedValue = cmb.SelectedValue Else IsChangedValue = -1
            Else
                IsChangedValue = -1
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSalesAccount_Leave(sender As Object, e As EventArgs) Handles cmbSalesAccount.Leave, SalesTaxAccount.Leave, cmbTaxPayableAccount.Leave, cmbFuelExpAccount.Leave, cmbOtherExpAccount.Leave, cmbSEDAccount.Leave, cmbTransitInsuranceAccount.Leave, cmbSalesDiscAccount.Leave, cmbDefaultAccountInPlaceCustomer.Leave, cmbSaleTaxDeductioAcId.Leave
        Try
            'If isFormOpen = False Then Exit Sub
            Dim cmb As Windows.Forms.ComboBox = CType(sender, Windows.Forms.ComboBox)
            'If cmb.SelectedIndex = -1 Then Exit Sub

            'If cmb.SelectedValue IsNot Nothing Then
            '    If IsChangedValue.ToString <> cmb.SelectedValue.ToString Then frmConfigCompany.saveComboBoxConfig(sender)
            'Else
            '    If IsChangedValue.ToString <> cmb.Text.ToString Then If cmb.Text.Length > 0 Then frmConfigCompany.saveComboBoxConfig(sender)
            'End If


            frmConfigCompany.SaveConfiguration(cmb.Tag.ToString, cmb.SelectedValue)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtTransitInssuranceTax_Leave(sender As Object, e As EventArgs) Handles txtTransitInssuranceTax.Leave
        Try
            KeyType = "TransitInssuranceTax"
            KeyValue = Me.txtTransitInssuranceTax.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtWHTax_Leave(sender As Object, e As EventArgs) Handles txtWHTax.Leave
        Try
            KeyType = "WHTax_Percentage"
            KeyValue = Me.txtWHTax.Text
            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDefaultTax_Leave(sender As Object, e As EventArgs) Handles txtDefaultTax.Leave
        Try
            KeyType = "Default_Tax_Percentage"
            KeyValue = Me.txtDefaultTax.Text
            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cbSales_CheckedChanged(sender As Object, e As EventArgs) Handles cbSales.CheckedChanged, cbDeliveryChalan.CheckedChanged, _
        cbSalesOrder.CheckedChanged, cbSalesReturn.CheckedChanged, cbQuotation.CheckedChanged
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim strValues As String = String.Empty
            ''Save Configuration For COA Mapping : Ayesha Rehman : TFS3322
            strValues += "Quotation^" & cbQuotation.Checked & "|"
            strValues += "SalesOrder^" & cbSalesOrder.Checked & "|"
            strValues += "DeliveryChalan^" & cbDeliveryChalan.Checked & "|"
            strValues += "Sales^" & cbSales.Checked & "|"
            strValues += "SalesReturn^" & cbSalesReturn.Checked
     

            If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, strValues)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub lblSecurity_Click(sender As Object, e As EventArgs) Handles lblSecurity.Click
        Try
            If frmconfigSalesSecurity.isFormOpen = True Then
                frmconfigSalesSecurity.Dispose()
            End If

            frmconfigSalesSecurity.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblItems_Click(sender As Object, e As EventArgs) Handles lblItems.Click
        Try
            If frmConfigSalesItems.isFormOpen = True Then
                frmConfigSalesItems.Dispose()
            End If

            frmConfigSalesItems.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblSales_Click(sender As Object, e As EventArgs) Handles lblSales.Click
        Try
            If frmConfigSales.isFormOpen = True Then
                frmConfigSales.Dispose()
            End If

            frmConfigSales.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkCostofsalevoucher_CheckedChanged(sender As Object, e As EventArgs) Handles chkCostofsalevoucher.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkTaxExcludePrice_CheckedChanged(sender As Object, e As EventArgs) Handles chkTaxExcludePrice.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkApplyFlatDiscountOnSale_CheckedChanged(sender As Object, e As EventArgs) Handles chkApplyFlatDiscountOnSale.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkShowMiscAccountsOnSales_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowMiscAccountsOnSales.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        frmMiscAccountsonSales.ShowDialog()
    End Sub

    Private Sub cmbAdjustmentExpAccount_Leave(sender As Object, e As EventArgs) Handles cmbAdjustmentExpAccount.Leave
        Dim cmb As Windows.Forms.ComboBox = CType(sender, Windows.Forms.ComboBox)
        frmConfigCompany.SaveConfiguration(cmb.Tag.ToString, cmb.SelectedValue)
    End Sub

End Class