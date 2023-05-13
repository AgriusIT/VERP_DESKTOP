Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Public Class frmConfigProjectManagment

    Public isFormOpen As Boolean = False

    Private Sub frmConfigProjectManagment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        FillCombos("PurchaseAccount")
        FillCombos("VendorRetentionAccount")
        FillCombos("VendorMobilizationAccount")
        FillCombos("SalesAccount")
        FillCombos("CustomerRetentionAccount")
        FillCombos("CustomerMobilizationAccount")
        getConfigValueList()
        GetAllRecords()
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")
        Try
            Me.cmbPurchaseAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseAccountVendorPM").ToString))
            Me.cmbRetentionAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("RetentionAccount").ToString))
            Me.cmbMobilizationAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("MobilizationAccount").ToString))
            Me.cmbSaleAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesAccountCustomerPM").ToString))
            Me.cmbRetentionAccountCustomer.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("RetentionAccountCustomerPM").ToString))
            Me.cmbMobilizationAccountCustomer.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("MobilizationAccountCustomerPM").ToString))

            Me.chkPurchaseAccountFrontEnd.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseAccountFrontEndVendorPM").ToString)

            If Me.chkPurchaseAccountFrontEnd.Checked = False Then
                Me.chkPurchaseAccountFrontEndNot.Checked = True
            End If

            Me.chkRevenueAccount.Checked = Convert.ToBoolean(getConfigValueByType("RevenueAccountFrontEnd").ToString)

            If Me.chkRevenueAccount.Checked = False Then
                Me.chkRevenueAccountNot.Checked = True
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
                    FillDropDown(Me.cmbPurchaseAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "VendorRetentionAccount"
                    FillDropDown(Me.cmbRetentionAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "VendorMobilizationAccount"
                    FillDropDown(Me.cmbMobilizationAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "SalesAccount"
                    FillDropDown(Me.cmbSaleAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "CustomerRetentionAccount"
                    FillDropDown(Me.cmbRetentionAccountCustomer, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "CustomerMobilizationAccount"
                    FillDropDown(Me.cmbMobilizationAccountCustomer, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPurchaseAccount_Leave(sender As Object, e As EventArgs) Handles cmbPurchaseAccount.Leave, cmbSaleAccount.Leave, cmbRetentionAccountCustomer.Leave, cmbRetentionAccount.Leave, cmbMobilizationAccountCustomer.Leave, cmbMobilizationAccount.Leave
        Try
            frmConfigCompany.saveComboBoxValueConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkPurchaseAccountFrontEnd_CheckedChanged(sender As Object, e As EventArgs) Handles chkPurchaseAccountFrontEnd.CheckedChanged, chkRevenueAccount.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

End Class