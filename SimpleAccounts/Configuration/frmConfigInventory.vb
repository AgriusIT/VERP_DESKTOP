Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Public Class frmConfigInventory

    Public isFormOpen As Boolean = False
    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty

    Private Sub frmConfigInventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try
            If Not getConfigValueByType("CostSheetType").ToString = "Error" Then
                Me.cmbCostSheetType.Text = getConfigValueByType("CostSheetType").ToString
            End If

            If Not getConfigValueByType("StockInConfigration").ToString = "Error" Then
                Me.cmbStockIn.Text = getConfigValueByType("StockInConfigration").ToString
            End If

            If Not getConfigValueByType("StockOutConfigration").ToString = "Error" Then
                Me.cmbStockOut.Text = getConfigValueByType("StockOutConfigration").ToString
            End If

            Me.chkStockDispatchTransfer.Checked = Convert.ToBoolean(getConfigValueByType("StockTransferFromDispatch").ToString)

            If Me.chkStockDispatchTransfer.Checked = False Then
                Me.chkStockDispatchTransferNot.Checked = True
            End If

            Me.chkStoreIssuenceWithProduction.Checked = Convert.ToBoolean(getConfigValueByType("StoreIssuenceWithProduction").ToString)

            If Me.chkStoreIssuenceWithProduction.Checked = False Then
                Me.chkStoreIssuenceWithProductionNot.Checked = True
            End If

            Me.chkArticleShowImage.Checked = Convert.ToBoolean(getConfigValueByType("ArticleShowImageOnStoreIssuance").ToString)

            If Me.chkArticleShowImage.Checked = False Then
                Me.chkArticleShowImageNot.Checked = True
            End If

            Me.chkStoreIssuanceDependonProductionPlan.Checked = Convert.ToBoolean(getConfigValueByType("StoreIssuaneDependonProductionPlan").ToString)

            If Me.chkStoreIssuanceDependonProductionPlan.Checked = False Then
                Me.chkStoreIssuanceDependonProductionPlanNot.Checked = True
            End If

            chkEnabledSizeCombinationCodeArticleCode.Checked = Convert.ToBoolean(getConfigValueByType("EnabledSizeCombinationCodeOnArticleCode").ToString.Replace("Error", "False").Replace("''", "False"))

            If Me.chkEnabledSizeCombinationCodeArticleCode.Checked = False Then
                Me.chkEnabledSizeCombinationCodeArticleCodeNot.Checked = True
            End If

            Me.chkAutoArticleCode.Checked = Convert.ToBoolean(getConfigValueByType("chkAutoArticleCode").ToString)

            If Me.chkAutoArticleCode.Checked = False Then
                Me.chkAutoArticleCodeNot.Checked = True
            End If

            Me.ndArticleCodeLength.Value = Val(getConfigValueByType("ArticleCodePrefixLength").ToString)

            Me.chkApplyCostSheetRateOnProduction.Checked = Convert.ToBoolean(getConfigValueByType("ApplyCostSheetRateOnProduction").ToString.Replace("Error", "False").Replace("''", "False"))

            If Me.chkApplyCostSheetRateOnProduction.Checked = False Then
                Me.chkApplyCostSheetRateOnProductionNot.Checked = True
            End If

            Me.chkAssociateItems.Checked = Convert.ToBoolean(getConfigValueByType("AssociateItems").ToString)

            If Me.chkAssociateItems.Checked = False Then
                Me.chkAssociateItemsNot.Checked = True
            End If

            Me.chkCostImplementationLotWiseOnStockMovement.Checked = Convert.ToBoolean(getConfigValueByType("CostImplementationLotWiseOnStockMovement").ToString)

            If Me.chkCostImplementationLotWiseOnStockMovement.Checked = False Then
                Me.chkCostImplementationLotWiseOnStockMovementNot.Checked = True
            End If

            Me.rbtItemSortOrder.Checked = Convert.ToBoolean(getConfigValueByType("ItemSortOrder").ToString)
            Me.rbtItemSortOrderByCode.Checked = Convert.ToBoolean(getConfigValueByType("ItemSortOrderByCode").ToString)
            Me.rbtItemSortOrderByName.Checked = Convert.ToBoolean(getConfigValueByType("ItemSortOrderByName").ToString)
            Me.chkItemAscending.Checked = Convert.ToBoolean(getConfigValueByType("ItemAscending").ToString)
            Me.chkItemDescending.Checked = Convert.ToBoolean(getConfigValueByType("ItemDescending").ToString)

            If Not getConfigValueByType("BagStock").ToString = "Error" Then
                Me.chkBagStock.Checked = Convert.ToBoolean(getConfigValueByType("BagStock").ToString)
            End If

            If Me.chkBagStock.Checked = False Then
                Me.chkBagStockNot.Checked = True
            End If

            Me.txtConversionTitle.Text = IIf(getConfigValueByType("ConversionTitle").ToString = "Error", "", getConfigValueByType("ConversionTitle").ToString)

            Me.txtConversionFactor.Text = IIf(getConfigValueByType("ConversionFactor").ToString = "Error", "", getConfigValueByType("ConversionFactor").ToString)

            'Task 3394 Saad Afzaal get this new configuration BOMLoadReceipy in InventoryConfig General Department on page load'

            Me.chkBOMLoadReceipy.Checked = Convert.ToBoolean(getConfigValueByType("BOMloadReceipy").ToString)

            If Me.chkBOMLoadReceipy.Checked = False Then
                Me.chkBOMLoadReceipyNot.Checked = True
            End If

            'End Task 3394

            ''TASK TFS3762
            If Not getConfigValueByType("CreateAutoItem").ToString = "Error" Then
                Me.chkCreateAutoItem.Checked = Convert.ToBoolean(getConfigValueByType("CreateAutoItem").ToString)
            End If
            ''END TASK TFS3762

            ''TASK TFS4097
            If Not getConfigValueByType("ItemWiseDiscount").ToString = "Error" Then
                Me.chkItemWiseDiscount.Checked = Convert.ToBoolean(getConfigValueByType("ItemWiseDiscount").ToString)
            End If
            ''END TASK TFS4097

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub linkAccount_Click(sender As Object, e As EventArgs) Handles linkAccount.Click
        Try
            If frmConfigInvAccounts.isFormOpen = True Then
                frmConfigInvAccounts.Dispose()
            End If

            frmConfigInvAccounts.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkSecurity_Click(sender As Object, e As EventArgs) Handles linkSecurity.Click
        Try
            If frmConfigInvSecurity.isFormOpen = True Then
                frmConfigInvSecurity.Dispose()
            End If

            frmConfigInvSecurity.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCostSheetType_Leave(sender As Object, e As EventArgs) Handles cmbCostSheetType.Leave

        Try
            KeyType = "CostSheetType"
            KeyValue = Me.cmbCostSheetType.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbStockIn_Leave(sender As Object, e As EventArgs) Handles cmbStockIn.Leave
        Try
            KeyType = "StockInConfigration"
            KeyValue = Me.cmbStockIn.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbStockOut_Leave(sender As Object, e As EventArgs) Handles cmbStockOut.Leave
        Try
            KeyType = "StockOutConfigration"
            KeyValue = Me.cmbStockOut.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkStockDispatchTransfer_CheckedChanged(sender As Object, e As EventArgs) Handles chkStockDispatchTransfer.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkStoreIssuenceWithProduction_CheckedChanged(sender As Object, e As EventArgs) Handles chkStoreIssuenceWithProduction.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkArticleShowImage_CheckedChanged(sender As Object, e As EventArgs) Handles chkArticleShowImage.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkStoreIssuanceDependonProductionPlan_CheckedChanged(sender As Object, e As EventArgs) Handles chkStoreIssuanceDependonProductionPlan.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkEnabledSizeCombinationCodeArticleCode_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnabledSizeCombinationCodeArticleCode.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkAutoArticleCode_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoArticleCode.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub ndArticleCodeLength_ValueChanged(sender As Object, e As EventArgs) Handles ndArticleCodeLength.ValueChanged
        frmConfigCompany.saveComboBoxNumConfig(sender)
    End Sub

    Private Sub chkApplyCostSheetRateOnProduction_CheckedChanged(sender As Object, e As EventArgs) Handles chkApplyCostSheetRateOnProduction.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkAssociateItems_CheckedChanged(sender As Object, e As EventArgs) Handles chkAssociateItems.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkCostImplementationLotWiseOnStockMovement_CheckedChanged(sender As Object, e As EventArgs) Handles chkCostImplementationLotWiseOnStockMovement.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rbtItemSortOrder_CheckedChanged(sender As Object, e As EventArgs) Handles rbtItemSortOrder.CheckedChanged, rbtItemSortOrderByCode.CheckedChanged, rbtItemSortOrderByName.CheckedChanged, chkItemDescending.CheckedChanged, chkItemAscending.CheckedChanged
        Dim rbt As RadioButton = CType(sender, RadioButton)
        frmConfigCompany.SaveConfiguration(rbt.Tag.ToString(), rbt.Checked)
    End Sub

    Private Sub chkBagStock_CheckedChanged(sender As Object, e As EventArgs) Handles chkBagStock.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub txtConversionTitle_Leave(sender As Object, e As EventArgs) Handles txtConversionTitle.Leave
        Try
            KeyType = "ConversionTitle"
            KeyValue = Me.txtConversionTitle.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtConversionFactor_Leave(sender As Object, e As EventArgs) Handles txtConversionFactor.Leave
        Try
            KeyType = "ConversionFactor"
            KeyValue = Val(Me.txtConversionFactor.Text)

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task 3394 Saad Afzaal Add this new configuration BOMLoadReceipy in InventoryConfig General Department'

    Private Sub chkBOMLoadReceipy_CheckedChanged(sender As Object, e As EventArgs) Handles chkBOMLoadReceipy.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    'End Task 3394

    Private Sub chkCreateAutoItem_CheckedChanged(sender As Object, e As EventArgs) Handles chkCreateAutoItem.CheckedChanged
        Try
            frmConfigCompany.saveRadioBtnConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkItemWiseDiscount_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemWiseDiscount.CheckedChanged
        Try
            frmConfigCompany.saveRadioBtnConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class