Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Public Class frmConfigInvAccounts

    Public isFormOpen As Boolean = False

    Private Sub frmConfigInvAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        FillCombos("CGSAccount")
        FillCombos("CostProduction")
        FillCombos("InvAccount")
        FillCombos("CostOfGoodSoldccount")
        FillCombos("CylinderStockAccount")
        FillCombos("WastedStockAccount")
        FillCombos("ScrappedStockAccount")
        getConfigValueList()
        GetAllRecords()
    End Sub

    Public Sub FillCombos(Optional ByVal Condition As String = "")

        Try
            Dim strSQL As String = String.Empty
            Select Case Condition

                Case "CostProduction"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc "
                    FillDropDown(Me.cmbCostOfProduction, strSQL, True)

                Case "CGSAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY  detail_title Asc"
                    FillDropDown(Me.cmbStoreIssuanceAccount, strSQL, True)

                Case "InvAccount"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbInvAccount, strSQL, True)

                Case "CostOfGoodSoldccount"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbCGS, strSQL, True)

                Case "CylinderStockAccount"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbCylinderStockAccount, strSQL, True)

                Case "WastedStockAccount"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbWastedStockAccount, strSQL, True)

                Case "ScrappedStockAccount"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbScrappedStockAccount, strSQL, True)

            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try

            Me.cmbStoreIssuanceAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("StoreIssuenceAccount").ToString))
            Me.cmbCostOfProduction.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("StoreCreditAccount").ToString))
            Me.cmbInvAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("InvAccountId").ToString))
            Me.cmbCGS.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CGSAccountId").ToString))
            Me.cmbCylinderStockAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CylinderStockAccount").ToString))

            Me.chkProductionVoucher.Checked = Convert.ToBoolean(getConfigValueByType("ProductionVoucher").ToString)

            If Me.chkProductionVoucher.Checked = False Then
                Me.chkProductionVoucherNot.Checked = True
            End If

            Me.chkStoreIssuenceVoucher.Checked = Convert.ToBoolean(getConfigValueByType("StoreIssuenceVoucher").ToString)

            If Me.chkStoreIssuenceVoucher.Checked = False Then
                Me.chkStoreIssuenceVoucherNot.Checked = True
            End If

            Me.chkAvgRate.Checked = Convert.ToBoolean(getConfigValueByType("AvgRate").ToString)

            If Me.chkAvgRate.Checked = False Then
                Me.chkAvgRateNot.Checked = True
            End If

            Me.chkClinderVoucher.Checked = Convert.ToBoolean(getConfigValueByType("CylinderVoucher").ToString)

            If Me.chkClinderVoucher.Checked = False Then
                Me.chkClinderVoucherNot.Checked = True
            End If

            Me.chkCGAccountOnStoreIssuance.Checked = Convert.ToBoolean(getConfigValueByType("CGAccountOnStoreIssuance").ToString)

            If Me.chkCGAccountOnStoreIssuance.Checked = False Then
                Me.chkCGAccountOnStoreIssuanceNot.Checked = True
            End If

            Me.chkApply40KgPackRate.Checked = Convert.ToBoolean(getConfigValueByType("Apply40KgRate").ToString)

            If Me.chkApply40KgPackRate.Checked = False Then
                Me.chkApply40KgPackRateNot.Checked = True
            End If

            Me.chkWeightWiseImport.Checked = Convert.ToBoolean(getConfigValueByType("ImportWieghtWiseCalculation").ToString)

            If Me.chkWeightWiseImport.Checked = False Then
                Me.chkWeightWiseImportNot.Checked = True
            End If

            If Not getConfigValueByType("WastedStockAccount").ToString = "Error" Then
                Me.cmbWastedStockAccount.SelectedValue = Val(getConfigValueByType("WastedStockAccount").ToString)
            End If

            If Not getConfigValueByType("ScrappedStockAccount").ToString = "Error" Then
                Me.cmbScrappedStockAccount.SelectedValue = Val(getConfigValueByType("ScrappedStockAccount").ToString)
            End If

            Dim cbValues As String = String.Empty
            cbValues = getConfigValueByType("COAGroupWithInventoryModule").ToString()
            If cbValues.Length > 0 Then
                Dim arday() As String = cbValues.Split("|")
                If arday.Length > 1 Then
                    'SI&False|RSI&True|SD&True|SR&True
                    cbStoreIssuance.Checked = Convert.ToBoolean(arday(0).Trim.Substring(3))
                    cbReturnStoreIssuance.Checked = Convert.ToBoolean(arday(1).Trim.Substring(4))
                End If
            End If

            Me.ChkDateWiseAverageRate.Checked = Convert.ToBoolean(getConfigValueByType("DateWiseAverageRate").ToString)

            If Me.ChkDateWiseAverageRate.Checked = False Then
                Me.ChkDateWiseAverageRateNot.Checked = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkInventory_Click(sender As Object, e As EventArgs) Handles linkInventory.Click
        Try
            If frmConfigInventory.isFormOpen = True Then
                frmConfigInventory.Dispose()
            End If

            frmConfigInventory.ShowDialog()
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

    Private Sub cmbStoreIssuanceAccount_Leave(sender As Object, e As EventArgs) Handles cmbStoreIssuanceAccount.Leave

        frmConfigCompany.saveComboBoxValueConfig(sender)

    End Sub

    Private Sub cmbCostOfProduction_Leave(sender As Object, e As EventArgs) Handles cmbCostOfProduction.Leave

        frmConfigCompany.saveComboBoxValueConfig(sender)

    End Sub

    Private Sub cmbInvAccount_Leave(sender As Object, e As EventArgs) Handles cmbInvAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbCGS_Leave(sender As Object, e As EventArgs) Handles cmbCGS.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbCylinderStockAccount_Leave(sender As Object, e As EventArgs) Handles cmbCylinderStockAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub chkProductionVoucher_CheckedChanged(sender As Object, e As EventArgs) Handles chkProductionVoucher.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkStoreIssuenceVoucher_CheckedChanged(sender As Object, e As EventArgs) Handles chkStoreIssuenceVoucher.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkAvgRate_CheckedChanged(sender As Object, e As EventArgs) Handles chkAvgRate.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkApply40KgPackRate_CheckedChanged(sender As Object, e As EventArgs) Handles chkApply40KgPackRate.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkWeightWiseImport_CheckedChanged(sender As Object, e As EventArgs) Handles chkWeightWiseImport.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub cmbWastedStockAccount_Leave(sender As Object, e As EventArgs) Handles cmbWastedStockAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbScrappedStockAccount_Leave(sender As Object, e As EventArgs) Handles cmbScrappedStockAccount.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub chkClinderVoucher_CheckedChanged(sender As Object, e As EventArgs) Handles chkClinderVoucher.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkCGAccountOnStoreIssuance_CheckedChanged(sender As Object, e As EventArgs) Handles chkCGAccountOnStoreIssuance.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub cbReturnStoreIssuance_CheckedChanged(sender As Object, e As EventArgs) Handles cbReturnStoreIssuance.CheckedChanged, cbStoreIssuance.CheckedChanged
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim strValues As String = String.Empty
            ''Save Configuration For COA Mapping : Ayesha Rehman : TFS3322
            strValues += "SI^" & cbStoreIssuance.Checked & "|"
            strValues += "RSI^" & cbReturnStoreIssuance.Checked
            If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, strValues)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ChkDateWiseAverageRate_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDateWiseAverageRate.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

End Class