Public Class frmConfigSales


    Public isFormOpen As Boolean = False
    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty
    Dim _SearchBCode As New DataTable


    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try
            ''Start TFS4161
            Me.chkDisablePackQty.Checked = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)

            If Me.chkDisablePackQty.Checked = False Then
                Me.chkDisablePackQtyNot.Checked = True
            End If
            ''End TFS4161

            If Not getConfigValueByType("ShowVendorOnSales").ToString = "Error" Then
                Me.rdoShowVendorsOnSales.Checked = Convert.ToBoolean(getConfigValueByType("ShowVendorOnSales").ToString)
            End If

            Dim StrIDS As String = getConfigValueByType("FinishGoodsDepartment").ToString
            If StrIDS <> "" Then
                Me.lstFinishGood.SelectItemsByIDs(StrIDS)
            End If

            If rdoShowVendorsOnSales.Checked = False Then
                Me.rdoShowVendorsOnSalesNot.Checked = True
            End If


            Me.rdoStockViewOnSale.Checked = Convert.ToBoolean(getConfigValueByType("StockViewOnSale").ToString)
            If rdoStockViewOnSale.Checked = False Then
                Me.rdoStockViewOnSaleNo.Checked = True
            End If


            Me.rdoLoadAllItemsInSales.Checked = Convert.ToBoolean(getConfigValueByType("LoadAllItemsInSales").ToString)
            If Me.rdoLoadAllItemsInSales.Checked = False Then
                Me.rdoLoadAllItemsInSalesNo.Checked = True
            End If

            Me.rdoSalesOrderAnalysis.Checked = Convert.ToBoolean(getConfigValueByType("SalesOrderAnalysis").ToString)
            If Me.rdoSalesOrderAnalysis.Checked = False Then
                Me.rdoSalesOrderAnalysisNo.Checked = True
            End If


            Me.rdoCompanyBasedPrefix.Checked = Convert.ToBoolean(getConfigValueByType("Company-Based-Prefix").ToString)
            If Me.rdoCompanyBasedPrefix.Checked = False Then
                Me.rdoCompanyBasedPrefixNo.Checked = True
            End If

            Me.rdoMultipleSalesOrder.Checked = Convert.ToBoolean(getConfigValueByType("MultipleSalesOrder").ToString)
            If Me.rdoMultipleSalesOrder.Checked = False Then
                Me.rdoMultipleSalesOrderNo.Checked = True
            End If

            Me.rdoShowVendorsOnSales.Checked = Convert.ToBoolean(getConfigValueByType("ShowVendorOnDeliveryChalan").ToString)
            If Me.rdoShowVendorsOnSales.Checked = False Then
                Me.rdoShowVendorsOnSalesNot.Checked = True
            End If

            Me.rdoCMFADocumentOnSales.Checked = Convert.ToBoolean(getConfigValueByType("CMFADocumentOnSales").ToString)
            If Me.rdoCMFADocumentOnSales.Checked = False Then
                Me.rdoCMFADocumentOnSalesNo.Checked = True
            End If

            Me.rdoflgVehicleIdentificationInfo.Checked = Convert.ToBoolean(getConfigValueByType("flgVehicleIdentificationInfo").ToString)
            If Me.rdoflgVehicleIdentificationInfo.Checked = False Then
                Me.rdoflgVehicleIdentificationInfoNo.Checked = True
            End If

            Me.rdoflgMargeItem.Checked = Convert.ToBoolean(getConfigValueByType("flgMargeItem").ToString)
            If Me.rdoflgMargeItem.Checked = False Then
                Me.rdoflgMargeItemNo.Checked = True
            End If

            Me.rdoDCStockImpact.Checked = Convert.ToBoolean(getConfigValueByType("DCStockImpact").ToString)
            If Me.rdoDCStockImpact.Checked = False Then
                Me.rdoDCStockImpactNo.Checked = True
            End If

            Me.rdoLoadItemAfterDeliveredOnDC.Checked = Convert.ToBoolean(getConfigValueByType("LoadItemAfterDeliveredOnDC").ToString)
            If Me.rdoLoadItemAfterDeliveredOnDC.Checked = False Then
                Me.rdoLoadItemAfterDeliveredOnDCNo.Checked = True
            End If

            Me.rdoEmailAlertDueInvoice.Checked = Convert.ToBoolean(getConfigValueByType("EmailAlertDueInvoice").ToString)
            If Me.rdoEmailAlertDueInvoice.Checked = False Then
                Me.rdoEmailAlertDueInvoiceNo.Checked = True
            End If

            Me.rdoMultipleSalesOrder.Checked = Convert.ToBoolean(getConfigValueByType("LoadMultiChalanOnSale").ToString)
            If Me.rdoMultipleSalesOrder.Checked = False Then
                Me.rdoMultipleSalesOrderNo.Checked = True
            End If

            Me.rdoEnableDuplicateQuotation.Checked = Convert.ToBoolean(getConfigValueByType("EnableDuplicateQuotation").ToString)
            If Me.rdoEnableDuplicateQuotation.Checked = False Then
                Me.rdoEnableDuplicateQuotationNo.Checked = True
            End If

            Me.rdoEnableDuplicateSalesOrder.Checked = Convert.ToBoolean(getConfigValueByType("EnableDuplicateSalesOrder").ToString)
            If Me.rdoEnableDuplicateSalesOrder.Checked = False Then
                Me.rdoEnableDuplicateSalesOrderNo.Checked = True
            End If

            'If Decrypt(getConfigValueByType("RepeatedCustomerCount").ToString) <> "Error" Then
            '    Me.txtRepeatedCustomerCount.Text = Decrypt(getConfigValueByType("RepeatedCustomerCount").ToString)
            'Else
            '    Me.txtRepeatedCustomerCount.Text = String.Empty
            'End If
            Me.txtRepeatedCustomerCount.Text = Convert.ToInt32(Val(getConfigValueByType("CustomerRepeatedCount").ToString))

            'If Decrypt(getConfigValueByType("Sale Certificate Prefix").ToString) <> "Error" Then
            '    Me.txtSaleCertificatePrefix.Text = Decrypt(getConfigValueByType("Sale Certificate Prefix").ToString)
            'Else
            '    Me.txtSaleCertificatePrefix.Text = String.Empty
            'End If
            Me.txtSaleCertificatePrefix.Text = getConfigValueByType("SaleCertificatePreFix").ToString

            Me.chkWeighbridgeDC.Checked = Convert.ToBoolean(getConfigValueByType("WeighbridgeDC").ToString)
            Me.chkWeighbridgeSaleOrder.Checked = Convert.ToBoolean(getConfigValueByType("WeighbridgeSaleOrder").ToString)

            Me.chkSeparateClosureOfSODC.Checked = Convert.ToBoolean(getConfigValueByType("SeparateClosureOfSODC").ToString)
            If Me.chkSeparateClosureOfSODC.Checked = False Then
                Me.chkSeparateClosureOfSODCNot.Checked = True
            End If

            Me.chkDCBasedonSO.Checked = Convert.ToBoolean(getConfigValueByType("DCDependentonSO").ToString)
            If Me.chkDCBasedonSO.Checked = False Then
                Me.chkDCBasedonSONot.Checked = True
            End If
            ''TASK TFS4391
            Me.chkDuplicateSales.Checked = Convert.ToBoolean(getConfigValueByType("IsDuplicateSales").ToString)
            If Me.chkDuplicateSales.Checked = False Then
                Me.chkDuplicateSalesNot.Checked = True
            End If
            ''END TASK TFS4391
            ''Aashir: 4441 :Added Addidtion verification on SO
            'start
            Me.chkVerificationOnSO.Checked = Convert.ToBoolean(getConfigValueByType("AdditionalVerificationOnSO").ToString)
            If Me.chkVerificationOnSO.Checked = False Then
                Me.chkVerificationOnSONot.Checked = True
            End If
            'end
            Me.rbPaymentVoucherToSalesReturn.Checked = Convert.ToBoolean(getConfigValueByType("PaymentVoucherToSalesReturn").ToString)
            If Me.rbPaymentVoucherToSalesReturn.Checked = False Then
                Me.rbPaymentVoucherToSalesReturnNot.Checked = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub FillCombos(Optional Condition As String = "")
        Try

            FillListBox(Me.lstFinishGood.ListItem, "SELECT  ArticleGroupDefTable.ArticleGroupId AS Id, ArticleGroupDefTable.ArticleGroupName AS Name, ArticleGroupDefTable.SubSubID, ArticleGroupDefTable.GroupCode " & _
                     " FROM   ArticleGroupDefTable " & _
                     " WHERE (ArticleGroupDefTable.Active = 1) " & _
                     " ORDER BY ArticleGroupDefTable.SortOrder")
            lstFinishGood.DeSelect()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub frmConfigSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        FillCombos()
        getConfigValueList()
        GetAllRecords()

    End Sub

    Private Sub rdoShowVendorsOnSales_CheckedChanged(sender As Object, e As EventArgs) Handles rdoShowVendorsOnSales.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub
    Private Sub rdoStockViewOnSale_CheckedChanged(sender As Object, e As EventArgs) Handles rdoStockViewOnSale.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoLoadAllItemsInSales_CheckedChanged(sender As Object, e As EventArgs) Handles rdoLoadAllItemsInSales.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoSalesOrderAnalysis_CheckedChanged(sender As Object, e As EventArgs) Handles rdoSalesOrderAnalysis.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoCompanyBasedPrefix_CheckedChanged(sender As Object, e As EventArgs) Handles rdoCompanyBasedPrefix.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub


    Private Sub rdoMultipleSalesOrder_CheckedChanged(sender As Object, e As EventArgs) Handles rdoMultipleSalesOrder.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoShowVendorOnDeliveryChalan_CheckedChanged(sender As Object, e As EventArgs) Handles rdoShowVendorOnDeliveryChalan.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoCMFADocumentOnSales_CheckedChanged(sender As Object, e As EventArgs) Handles rdoCMFADocumentOnSales.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoflgVehicleIdentificationInfo_CheckedChanged(sender As Object, e As EventArgs) Handles rdoflgVehicleIdentificationInfo.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoflgMargeItem_CheckedChanged(sender As Object, e As EventArgs) Handles rdoflgMargeItem.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoDCStockImpact_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDCStockImpact.CheckedChanged

        If rdoDCStockImpact.Checked = True Then
            If CheckOpenDCs() Then
                msg_Information("Opened DCs are found. Please close them first. ")
                rdoDCStockImpactNo.Checked = True
                Exit Sub
            End If
        End If

        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub


    Private Sub rdoLoadItemAfterDeliveredOnDC_CheckedChanged(sender As Object, e As EventArgs) Handles rdoLoadItemAfterDeliveredOnDC.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoEmailAlertDueInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles rdoEmailAlertDueInvoice.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoLoadMultiChalanOnSale_CheckedChanged(sender As Object, e As EventArgs) Handles rdoLoadMultiChalanOnSale.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoEnableDuplicateQuotation_CheckedChanged(sender As Object, e As EventArgs) Handles rdoEnableDuplicateQuotation.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoEnableDuplicateSalesOrder_CheckedChanged(sender As Object, e As EventArgs) Handles rdoEnableDuplicateSalesOrder.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub txtRepeatedCustomerCount_Leave(sender As Object, e As EventArgs) Handles txtRepeatedCustomerCount.Leave
        Try
            KeyType = "CustomerRepeatedCount"
            KeyValue = Me.txtRepeatedCustomerCount.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSaleCertificatePrefix_Leave(sender As Object, e As EventArgs) Handles txtSaleCertificatePrefix.Leave
        Try
            KeyType = "SaleCertificatePreFix"
            KeyValue = Me.txtSaleCertificatePrefix.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblAccounts_Click(sender As Object, e As EventArgs) Handles lblAccounts.Click
        Try
            If frmConfigSalesAccount.isFormOpen = True Then
                frmConfigSalesAccount.Dispose()
            End If

            frmConfigSalesAccount.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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

    Private Sub chkWeighbridgeDC_CheckedChanged(sender As Object, e As EventArgs) Handles chkWeighbridgeDC.CheckedChanged
        frmConfigCompany.saveCheckConfig(sender)
    End Sub

    Private Sub chkWeighbridgeSaleOrder_CheckedChanged(sender As Object, e As EventArgs) Handles chkWeighbridgeSaleOrder.CheckedChanged
        frmConfigCompany.saveCheckConfig(sender)
    End Sub

    Private Sub chkSeparateClosureOfSODC_CheckedChanged(sender As Object, e As EventArgs) Handles chkSeparateClosureOfSODC.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkDCBasedonSO_CheckedChanged(sender As Object, e As EventArgs) Handles chkDCBasedonSO.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Function CheckOpenDCs() As Boolean
        Dim checkOpen As String = ""
        Try
            checkOpen = "SELECT IsNull(DeliveryId, 0) As DeliveryId FROM DeliveryChalanMasterTable WHERE Status ='Open' "
            Dim dtCheckOpen As DataTable = GetDataTable(checkOpen)
            If dtCheckOpen.Rows.Count > 0 AndAlso dtCheckOpen.Rows(0).Item(0) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub chkDuplicateSales_CheckedChanged(sender As Object, e As EventArgs) Handles chkDuplicateSales.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub
    ''Aashir: 4441 :Added Addidtion verification on SO
    Private Sub chkVerificationOnSO_CheckedChanged(sender As Object, e As EventArgs) Handles chkVerificationOnSO.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub lbUiBarcodeItems_Leave(sender As Object, e As EventArgs) Handles lstFinishGood.Leave
        Try
            Dim strValues As String = String.Empty
            strValues = Me.lstFinishGood.SelectedIDs
            If lstFinishGood.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(lstFinishGood.Tag, strValues)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbPaymentVoucherToSalesReturn_CheckedChanged(sender As Object, e As EventArgs) Handles rbPaymentVoucherToSalesReturn.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkDisablePackQty_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisablePackQty.CheckedChanged
        Try
            frmConfigCompany.saveRadioBtnConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class