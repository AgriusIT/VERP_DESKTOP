Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Public Class frmConfigSMS

    Public isFormOpen As Boolean = False
    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty

    Private Sub chkBrandedSMS_CheckedChanged(sender As Object, e As EventArgs) Handles chkBrandedSMS.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try
            Me.chkBrandedSMS.Checked = Convert.ToBoolean(getConfigValueByType("EnabledBrandedSMS").ToString)

            If Me.chkBrandedSMS.Checked = False Then
                Me.chkBrandedSMSNot.Checked = True
            End If

            If Decrypt(getConfigValueByType("BrandedSMSMask").ToString) <> "Error" Then
                Me.txtBrandedSMSMask.Text = Decrypt(getConfigValueByType("BrandedSMSMask").ToString)
            Else
                Me.txtBrandedSMSMask.Text = String.Empty
            End If

            If getConfigValueByType("BrandedSMSUser").ToString <> "Error" Then
                Me.txtBrandedSMSUser.Text = getConfigValueByType("BrandedSMSUser").ToString
            Else
                Me.txtBrandedSMSUser.Text = String.Empty
            End If

            If Decrypt(getConfigValueByType("BrandedSMSPassword").ToString) <> "Error" Then
                Me.txtBrandedSMSPassword.Text = Decrypt(getConfigValueByType("BrandedSMSPassword").ToString)
            Else
                Me.txtBrandedSMSPassword.Text = String.Empty
            End If

            If getConfigValueByType("DNSHostForSMS").ToString <> "Error" Then
                Me.txtDNSHost.Text = getConfigValueByType("DNSHostForSMS").ToString
            Else
                Me.txtDNSHost.Text = String.Empty
            End If

            If getConfigValueByType("AdminMobileNo").ToString <> "Error" Then
                Me.txtAdminMobile.Text = getConfigValueByType("AdminMobileNo").ToString
            Else
                Me.txtAdminMobile.Text = String.Empty
            End If

            Me.cmbSMSLanguage.Text = getConfigValueByType("SMSLanguage").ToString.Replace("Error", "English").Replace("''", "English")

            Me.nudSMSScheduleTime.Value = Convert.ToInt32(Val(getConfigValueByType("SMSScheduleTime").ToString))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmConfigSMS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()
    End Sub

    Private Sub txtBrandedSMSMask_Leave(sender As Object, e As EventArgs) Handles txtBrandedSMSMask.Leave

        Try
            KeyType = "BrandedSMSMask"
            KeyValue = Encrypt(Me.txtBrandedSMSMask.Text)

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub txtBrandedSMSUser_Leave(sender As Object, e As EventArgs) Handles txtBrandedSMSUser.Leave

        Try
            KeyType = "BrandedSMSUser"
            KeyValue = Me.txtBrandedSMSUser.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub txtBrandedSMSPassword_Leave(sender As Object, e As EventArgs) Handles txtBrandedSMSPassword.Leave

        Try
            KeyType = "BrandedSMSPassword"
            KeyValue = Encrypt(Me.txtBrandedSMSPassword.Text)

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub txtDNSHost_Click(sender As Object, e As EventArgs) Handles txtDNSHost.Click

        Try
            Dim frmD As New frmDomains
            'ApplyStyleSheet(frmD)
            If frmD.ShowDialog = Windows.Forms.DialogResult.Yes Then
                Me.txtDNSHost.Text = frmD._HostName.ToString
                frmConfigCompany.SaveConfiguration("DNSHostForSMS", Me.txtDNSHost.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub txtAdminMobile_Leave(sender As Object, e As EventArgs) Handles txtAdminMobile.Leave

        Try
            KeyType = "AdminMobileNo"
            KeyValue = Me.txtAdminMobile.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbSMSLanguage_Leave(sender As Object, e As EventArgs) Handles cmbSMSLanguage.Leave

        frmConfigCompany.saveComboBoxConfig(sender)

    End Sub

    Private Sub nudSMSScheduleTime_ValueChanged(sender As Object, e As EventArgs) Handles nudSMSScheduleTime.ValueChanged

        frmConfigCompany.saveComboBoxNumConfig(sender)

    End Sub

    Private Sub linkCompanyConfiguration_Click(sender As Object, e As EventArgs) Handles linkCompanyConfiguration.Click
        Try
            If frmConfigCompany.isFormOpen = True Then
                frmConfigCompany.Dispose()
            End If

            frmConfigCompany.ShowDialog()
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

End Class