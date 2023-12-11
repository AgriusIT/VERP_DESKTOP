Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq


Public Class frmConfigImports

    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            Dim strSQL As String = String.Empty
            Select Case Condition

                Case "AdditionalCost"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbAdditionalCostAccount, strSQL, True)

                Case "CustomDuty"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbCustomDutyAccount, strSQL, True)

                Case "SalesTax"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbLCSalesTaxAccount, strSQL, True)

                Case "AdditionalSalesTax"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbAdditionalSalesTaxAccount, strSQL, True)

                Case "AdvanceIncome"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbAdvanceIncomeTaxAccount, strSQL)

                Case "ExciseDuty"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbExciseDutyAccount, strSQL)

            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try

            Me.cmbAdditionalCostAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AdditionalCostAccountId").ToString))
            Me.cmbCustomDutyAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CustomDutyAccountId").ToString))
            Me.cmbLCSalesTaxAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("LCSalesTaxAccountId").ToString))
            Me.cmbAdditionalSalesTaxAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AdditionalSalesTaxAccountId").ToString))
            Me.cmbAdvanceIncomeTaxAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AdvanceIncomeTaxAccountId").ToString))
            Me.cmbExciseDutyAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ExciseDutyAccountId").ToString))

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub linkAccounts_Click(sender As Object, e As EventArgs) Handles linkAccounts.Click
        Try
            If frmConfigAccounts.isFormOpen = True Then
                frmConfigAccounts.Dispose()
            End If

            frmConfigAccounts.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkDB_Click(sender As Object, e As EventArgs) Handles linkDB.Click
        Try
            If frmConfigDB.isFormOpen = True Then
                frmConfigDB.Dispose()
            End If

            frmConfigDB.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkSecurityRights_Click(sender As Object, e As EventArgs) Handles linkSecurityRights.Click
        Try
            If frmConfigSecurityRights.isFormOpen = True Then
                frmConfigSecurityRights.Dispose()
            End If

            frmConfigSecurityRights.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmConfigImports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillCombos("AdditionalCost")
        FillCombos("CustomDuty")
        FillCombos("SalesTax")
        FillCombos("AdditionalSalesTax")
        FillCombos("AdvanceIncome")
        FillCombos("ExciseDuty")

        getConfigValueList()
        GetAllRecords()
    End Sub

    Private Sub cmbAdditionalCostAccount_Leave(sender As Object, e As EventArgs) Handles cmbAdditionalCostAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub



    Private Sub cmbCustomDutyAccount_Leave(sender As Object, e As EventArgs) Handles cmbCustomDutyAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbLCSalesTaxAccount_Leave(sender As Object, e As EventArgs) Handles cmbLCSalesTaxAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbAdditionalSalesTaxAccount_Leave(sender As Object, e As EventArgs) Handles cmbAdditionalSalesTaxAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbAdvanceIncomeTaxAccount_Leave(sender As Object, e As EventArgs) Handles cmbAdvanceIncomeTaxAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbExciseDutyAccount_Leave(sender As Object, e As EventArgs) Handles cmbExciseDutyAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

End Class