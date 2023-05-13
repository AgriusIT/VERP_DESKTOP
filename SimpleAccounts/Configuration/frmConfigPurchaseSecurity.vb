''TFS4362 : Ayesha Rehman : 05-09-2018 :Excess Qty Restrictions are require for Purchase module.
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Public Class frmConfigPurchaseSecurity

    Public isFormOpen As Boolean = False

    Private Sub frmConfigPurchaseSecurity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try

            Me.chkShowCustomerOnPurchase.Checked = IIf(getConfigValueByType("Show Customer On Purchase") = "False", False, True)

            If Me.chkShowCustomerOnPurchase.Checked = False Then
                Me.chkShowCustomerOnPurchaseNot.Checked = True
            End If

            Me.chkPurchaseAllowedWithPO.Checked = IIf(getConfigValueByType("PurchaseAllowedWithPO") = "False", False, True)

            If Me.chkPurchaseAllowedWithPO.Checked = False Then
                Me.chkPurchaseAllowedWithPONot.Checked = True
            End If

            Me.chkAutoLoadPO.Checked = IIf(getConfigValueByType("AutoLoadPO") = "False", False, True)

            If Me.chkAutoLoadPO.Checked = False Then
                Me.chkAutoLoadPONot.Checked = True
            End If

            Me.chkAllowAddZeroPriceOnPurchase.Checked = Convert.ToBoolean(getConfigValueByType("AllowAddZeroPriceOnPurchase").ToString.Replace("Error", "False").Replace("''", "False"))

            If Me.chkAllowAddZeroPriceOnPurchase.Checked = False Then
                Me.chkAllowAddZeroPriceOnPurchaseNot.Checked = True
            End If

            Me.chkAllowChangePO.Checked = Convert.ToBoolean(getConfigValueByType("AllowChangePO").ToString.Replace("Error", "False").Replace("''", "False"))

            If Me.chkAllowChangePO.Checked = False Then
                Me.chkAllowChangePONot.Checked = True
            End If

            If Not getConfigValueByType("VisiblerateonPO").ToString = "Error" Then
                Me.chkVisiblerateonPO.Checked = Convert.ToBoolean(getConfigValueByType("VisiblerateonPO").ToString)
            End If

            If Me.chkVisiblerateonPO.Checked = False Then
                Me.chkVisiblerateonPONot.Checked = True
            End If

            Me.chkPOPrintAfterApproval.Checked = IIf(getConfigValueByType("POPrintAfterApproval") = "False", False, True) ''TFS2377

            If Me.chkPOPrintAfterApproval.Checked = False Then
                Me.chkPOPrintAfterApprovalNot.Checked = True
            End If
            ''Start TFS4362 : Ayesha Rehman: 05-09-2018
            If Not getConfigValueByType("OrderQtyExceedAgainstGRN").ToString = "Error" Then
                Me.chkOrderQtyExceedGRN.Checked = Convert.ToBoolean(getConfigValueByType("OrderQtyExceedAgainstGRN").ToString)
            End If
            If Not getConfigValueByType("OrderQtyExceedAgainstPurchase").ToString = "Error" Then
                Me.chkOrderQtyExceedPurchase.Checked = Convert.ToBoolean(getConfigValueByType("OrderQtyExceedAgainstPurchase").ToString)
            End If
            If Not getConfigValueByType("GRNQtyExceedAgainstPurchase").ToString = "Error" Then
                Me.chkGRNQtyExceedPurchase.Checked = Convert.ToBoolean(getConfigValueByType("GRNQtyExceedAgainstPurchase").ToString)
            End If
            ''End TFS4362
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

    Private Sub linkAccounts_Click(sender As Object, e As EventArgs) Handles linkAccounts.Click
        Try
            If frmConfigPurchaseAccount.isFormOpen = True Then
                frmConfigPurchaseAccount.Dispose()
            End If

            frmConfigPurchaseAccount.ShowDialog()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkPaymentVoucherOnPurchase_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowCustomerOnPurchase.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkPurchaseAllowedWithPO_CheckedChanged(sender As Object, e As EventArgs) Handles chkPurchaseAllowedWithPO.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkAutoLoadPO_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoLoadPO.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkAllowAddZeroPriceOnPurchase_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowAddZeroPriceOnPurchase.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkAllowChangePO_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowChangePO.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkVisiblerateonPO_CheckedChanged(sender As Object, e As EventArgs) Handles chkVisiblerateonPO.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkPOPrintAfterApproval_CheckedChanged(sender As Object, e As EventArgs) Handles chkPOPrintAfterApproval.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkOrderQtyExceedGRN_CheckedChanged(sender As Object, e As EventArgs) Handles chkOrderQtyExceedGRN.CheckedChanged, chkGRNQtyExceedPurchase.CheckedChanged, chkOrderQtyExceedPurchase.CheckedChanged
        Try
            frmConfigCompany.saveCheckConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class