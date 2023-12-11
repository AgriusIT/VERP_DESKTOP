Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Public Class frmConfigSecurityRights

    Public isFormOpen As Boolean = False

    Private Sub chkNewSecurityRights_CheckedChanged(sender As Object, e As EventArgs) Handles chkNewSecurityRights.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkUserCompanyRights_CheckedChanged(sender As Object, e As EventArgs) Handles chkUserCompanyRights.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkMenuRights_CheckedChanged(sender As Object, e As EventArgs) Handles chkMenuRights.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try
            Me.chkNewSecurityRights.Checked = Convert.ToBoolean(getConfigValueByType("NewSecurityRights").ToString)

            If Me.chkNewSecurityRights.Checked = False Then
                Me.chkNewSecurityRightsNot.Checked = True
            End If

            Me.chkUserCompanyRights.Checked = Convert.ToBoolean(getConfigValueByType("CompanyRights").ToString)

            If Me.chkUserCompanyRights.Checked = False Then
                Me.chkUserCompanyRightsNot.Checked = True
            End If

            Me.chkMenuRights.Checked = Convert.ToBoolean(getConfigValueByType("MenuRights").ToString)

            If Me.chkMenuRights.Checked = False Then
                Me.chkMenuRightsNot.Checked = True
            End If

            Me.chkChangeDocumentNo.Checked = Convert.ToBoolean(getConfigValueByType("ChangeDocNo").ToString)

            If Me.chkChangeDocumentNo.Checked = False Then
                Me.chkDocumentNoNot.Checked = True
            End If

            Me.chkShowCompanyAddress.Checked = IIf(getConfigValueByType("ShowCompanyAddressOnPageHeader") = "True", 1, 0)

            If Me.chkShowCompanyAddress.Checked = False Then
                Me.chkCompanyAddressNot.Checked = True
            End If

            Me.chkSIRIUSPartner.Checked = Convert.ToBoolean(getConfigValueByType("SIRIUSPartner").ToString)

            If Me.chkSIRIUSPartner.Checked = False Then
                Me.chkSIRIUSPartnerNot.Checked = True
            End If

            Me.chkAllowMinusStock.Checked = Convert.ToBoolean(getConfigValueByType("AllowMinusStock").ToString)

            If Me.chkAllowMinusStock.Checked = False Then
                Me.chkAllowMinusStockNot.Checked = True
            End If

            Me.chkShowMasterGrid.Checked = Convert.ToBoolean(getConfigValueByType("ShowMasterGrid").ToString)

            If Me.chkShowMasterGrid.Checked = False Then
                Me.chkShowMasterGridNot.Checked = True
            End If

            Me.chkUserwiseCompany.Checked = Convert.ToBoolean(getConfigValueByType("UserwiseCompany"))

            If Me.chkUserwiseCompany.Checked = False Then
                Me.chkUserwiseCompanyNot.Checked = True
            End If

            Me.chkUserwiseLocation.Checked = Convert.ToBoolean(getConfigValueByType("UserwiseLocation"))

            If Me.chkUserwiseLocation.Checked = False Then
                Me.chkUserwiseLocationNot.Checked = True
            End If

            Me.chkFreezColumn.Checked = Convert.ToBoolean(getConfigValueByType("GridFreezColumn").Replace("Error", "False").Replace("''", "False"))

            If Me.chkFreezColumn.Checked = False Then
                Me.chkFreezColumnNot.Checked = True
            End If

            If Not Me.chkHelpnSupport Is Nothing Then
                Me.chkHelpnSupport.Checked = Convert.ToBoolean(getConfigValueByType("HelpnSupportPanel").ToString)
            End If

            If Me.chkHelpnSupport.Checked = False Then
                Me.chkHelpnSupportNot.Checked = True
            End If

            If Not getConfigValueByType("DuplicateMobileInLeadsInfo").ToString = "Error" Then
                Me.chkDuplicateMobile.Checked = Convert.ToBoolean(getConfigValueByType("DuplicateMobileInLeadsInfo").ToString)
            End If

            If Me.chkDuplicateMobile.Checked = False Then
                Me.chkDuplicateMobileNot.Checked = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmConfigSecurityRights_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()
    End Sub

    Private Sub linkSMS_Click(sender As Object, e As EventArgs) Handles linkSMS.Click

        Try
            If frmConfigSMS.isFormOpen = True Then
                frmConfigSMS.Dispose()
            End If

            frmConfigSMS.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub linkCompany_Click(sender As Object, e As EventArgs) Handles linkCompany.Click

        Try
            If frmConfigCompany.isFormOpen = True Then
                frmConfigCompany.Dispose()
            End If

            frmConfigCompany.ShowDialog()
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

    Private Sub chkChangeDocumentNo_CheckedChanged(sender As Object, e As EventArgs) Handles chkChangeDocumentNo.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkShowCompanyAddress_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowCompanyAddress.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkSIRIUSPartner_CheckedChanged(sender As Object, e As EventArgs) Handles chkSIRIUSPartner.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkAllowMinusStock_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowMinusStock.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub ShowMasterGrid_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowMasterGrid.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkUserwiseCompany_CheckedChanged(sender As Object, e As EventArgs) Handles chkUserwiseCompany.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkUserwiseLocation_CheckedChanged(sender As Object, e As EventArgs) Handles chkUserwiseLocation.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkFreezColumn_CheckedChanged(sender As Object, e As EventArgs) Handles chkFreezColumn.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkHelpnSupport_CheckedChanged(sender As Object, e As EventArgs) Handles chkHelpnSupport.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkDuplicateMobile_CheckedChanged(sender As Object, e As EventArgs) Handles chkDuplicateMobile.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub linkCompanyInfo_Click(sender As Object, e As EventArgs) Handles linkCompanyInfo.Click
        Try
            If frmConfigCompanyInfo.isFormOpen = True Then
                frmConfigCompanyInfo.Dispose()
            End If

            frmConfigCompanyInfo.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkPath_Click(sender As Object, e As EventArgs) Handles linkPath.Click
        Try
            If frmConfigCompanyPath.isFormOpen = True Then
                frmConfigCompanyPath.Dispose()
            End If

            frmConfigCompanyPath.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class