Public Class frmconfigSalesSecurity

    Public isFormOpen As Boolean = False
    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty

    Private Sub frmconfigSalesSecurity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        GetAllRecords()
        getConfigValueList()
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try

            If Not getConfigValueByType("UserWiseCustomer").ToString = "Error" Then
                Me.chkUserWiseCustomer.Checked = Convert.ToBoolean(getConfigValueByType("UserWiseCustomer").ToString)
            End If
            If chkUserWiseCustomer.Checked = False Then
                Me.chkUserWiseCustomerNot.Checked = True
            End If

            Me.txtSalemanVoucherPrintCount.Text = Convert.ToInt32(Val(getConfigValueByType("PrintCount").ToString))

            If Not getConfigValueByType("PrintLog").ToString = "Error" Then
                Me.chkSalemanVoucherPrintLog.Checked = Convert.ToBoolean(getConfigValueByType("PrintLog").ToString)
            End If
            If chkSalemanVoucherPrintLog.Checked = False Then
                Me.chkSalemanVoucherPrintLogNot.Checked = True
            End If

            If Not getConfigValueByType("OnetimeSalesReturn").ToString = "Error" Then
                Me.chkOnetimesalereturn.Checked = Convert.ToBoolean(getConfigValueByType("OnetimeSalesReturn").ToString)
            End If
            If chkOnetimesalereturn.Checked = False Then
                Me.chkOnetimesalereturnNot.Checked = True
            End If

            Me.txtHistoryLoadQuantity.Text = Convert.ToInt32(Val(getConfigValueByType("SalesHistoryLoadQuantity").ToString))

            If Not getConfigValueByType("AllowBelowRetailPrice").ToString = "Error" Then
                Me.chkAllowBelowRetailPrice.Checked = Convert.ToBoolean(getConfigValueByType("AllowBelowRetailPrice").ToString)
            End If
            If chkAllowBelowRetailPrice.Checked = False Then
                Me.chkAllowBelowRetailPriceNot.Checked = True
            End If

            If Not getConfigValueByType("SOUpdateAfterDelivery").ToString = "Error" Then
                Me.chkSOUpdateAfterDelivery.Checked = Convert.ToBoolean(getConfigValueByType("SOUpdateAfterDelivery").ToString)
            End If
            If chkSOUpdateAfterDelivery.Checked = False Then
                Me.chkSOUpdateAfterDeliveryNot.Checked = True
            End If

            If Not getConfigValueByType("AllDispatchLocations").ToString = "Error" Then
                Me.chkAllDispatchLocations.Checked = Convert.ToBoolean(getConfigValueByType("AllDispatchLocations").ToString)
            End If
            If chkAllDispatchLocations.Checked = False Then
                Me.chkAllDispatchLocationsNot.Checked = True
            End If

            Me.chkOrderQtyExceedDeliveryChalan.Checked = Convert.ToBoolean(getConfigValueByType("OrderQtyExceedAgainstDeliveryChalan").ToString)
            Me.chkOrderQtyExceedSales.Checked = Convert.ToBoolean(getConfigValueByType("OrderQtyExceedAgainstSales").ToString)
            Me.chkDCQtyExceedSales.Checked = Convert.ToBoolean(getConfigValueByType("DCQtyExceedAgainstSales").ToString)

            If Not getConfigValueByType("BardanaAdjustmentOnPOS").ToString = "Error" Then
                Me.chkBardanaAdjustmentOnPOS.Checked = Convert.ToBoolean(getConfigValueByType("BardanaAdjustmentOnPOS").ToString)
            End If
            If chkBardanaAdjustmentOnPOS.Checked = False Then
                Me.chkBardanaAdjustmentOnPOSNot.Checked = True
            End If

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

    Private Sub lblGeneral_Click(sender As Object, e As EventArgs) Handles lblGeneral.Click
        Try
            If frmConfigSales.isFormOpen = True Then
                frmConfigSales.Dispose()
            End If

            frmConfigSales.ShowDialog()
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

    Private Sub chkUserWiseCustomer_CheckedChanged(sender As Object, e As EventArgs) Handles chkUserWiseCustomer.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub txtSalemanVoucherPrintCount_Leave(sender As Object, e As EventArgs) Handles txtSalemanVoucherPrintCount.Leave
        Try

            KeyType = "PrintCount"
            KeyValue = Me.txtSalemanVoucherPrintCount.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkSalemanVoucherPrintLog_CheckedChanged(sender As Object, e As EventArgs) Handles chkSalemanVoucherPrintLog.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkOnetimesalereturn_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnetimesalereturn.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub txtHistoryLoadQuantity_Leave(sender As Object, e As EventArgs) Handles txtHistoryLoadQuantity.Leave
        Try

            KeyType = "SalesHistoryLoadQuantity"
            KeyValue = Me.txtHistoryLoadQuantity.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkAllowBelowRetailPrice_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowBelowRetailPrice.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkSOUpdateAfterDelivery_CheckedChanged(sender As Object, e As EventArgs) Handles chkSOUpdateAfterDelivery.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkAllDispatchLocations_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllDispatchLocations.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkOrderQtyExceedDeliveryChalan_CheckedChanged(sender As Object, e As EventArgs) Handles chkOrderQtyExceedDeliveryChalan.CheckedChanged
        frmConfigCompany.saveCheckConfig(sender)
    End Sub

    Private Sub chkOrderQtyExceedSales_CheckedChanged(sender As Object, e As EventArgs) Handles chkOrderQtyExceedSales.CheckedChanged
        frmConfigCompany.saveCheckConfig(sender)
    End Sub

    Private Sub chkDCQtyExceedSales_CheckedChanged(sender As Object, e As EventArgs) Handles chkDCQtyExceedSales.CheckedChanged
        frmConfigCompany.saveCheckConfig(sender)
    End Sub

    Private Sub chkBardanaAdjustmentOnPOS_CheckedChanged(sender As Object, e As EventArgs) Handles chkBardanaAdjustmentOnPOS.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub
End Class