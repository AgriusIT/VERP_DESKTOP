Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Public Class frmConfigDB

    Public isFormOpen As Boolean = False

    Private Sub frmConfigDB_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.isFormOpen = True
        SetStartEndDate()
        getConfigValueList()
        GetAllRecords()
        GetSecurityRightsForBackup()

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try
            If getConfigValueByType("DatabaseBackup").ToString <> "Error" Then
                Me.txtBacklocation.Text = getConfigValueByType("DatabaseBackup").ToString
            Else
                Me.txtBacklocation.Text = String.Empty
            End If

            Dim cbValues As String = String.Empty
            Dim day As String = String.Empty
            Dim suitableTime As String = String.Empty
            cbValues = getConfigValueByType("BackupScheduleDays").ToString()
            If cbValues.Length > 0 Then
                Dim arday() As String = cbValues.Split("|")
                If arday.Length > 5 Then
                    'Mon&False|Tue&True|Wed&True|Thu&True|Fri&True|Sat&True|Sun&True
                    cbMon.Checked = Convert.ToBoolean(arday(0).Trim.Substring(4))
                    cbTue.Checked = Convert.ToBoolean(arday(1).Trim.Substring(4))
                    cbWed.Checked = Convert.ToBoolean(arday(2).Trim.Substring(4))
                    cbThu.Checked = Convert.ToBoolean(arday(3).Trim.Substring(4))
                    cbFri.Checked = Convert.ToBoolean(arday(4).Trim.Substring(4))
                    cbSta.Checked = Convert.ToBoolean(arday(5).Trim.Substring(4))
                    cbSun.Checked = Convert.ToBoolean(arday(6).Trim.Substring(4))
                End If

                suitableTime = getConfigValueByType("BackupSuitableTime").ToString()

            End If



            If suitableTime.Length > 0 Then
                If suitableTime.StartsWith("Any") Then
                    rbAnyTime.Checked = True
                    'dtpStartat.Value = CType(suitableTime.Substring(3), Date)
                Else
                    Dim specificTime() As String = suitableTime.Split("|")
                    If specificTime.Length > 0 Then
                        dtpStartat.Value = Convert.ToDateTime(specificTime(0).Trim.Substring(3).ToString())  'CType(specificTime(0).Trim.Substring(2), Date)
                        dtpEndat.Value = Convert.ToDateTime(specificTime(1).Trim.Substring(0).ToString())  'CType(specificTime(1).Trim.Substring(2), Date)
                        rbSpecificTime.Checked = True
                    End If
                End If
            End If
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

    Private Sub cbMon_CheckedChanged(sender As Object, e As EventArgs) Handles cbWed.CheckedChanged, cbTue.CheckedChanged, cbThu.CheckedChanged, cbSun.CheckedChanged, cbSta.CheckedChanged, cbMon.CheckedChanged, cbFri.CheckedChanged
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim strValues As String = String.Empty

            strValues += "Mon^" & cbMon.Checked & "|"
            strValues += "Tue^" & cbTue.Checked & "|"
            strValues += "Wed^" & cbWed.Checked & "|"
            strValues += "Thu^" & cbThu.Checked & "|"
            strValues += "Fri^" & cbFri.Checked & "|"
            strValues += "Sat^" & cbSta.Checked & "|"
            strValues += "Sun^" & cbSun.Checked

            If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, strValues)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rbAnyTime_CheckedChanged(sender As Object, e As EventArgs) Handles rbSpecificTime.CheckedChanged, rbAnyTime.CheckedChanged
        Try
            Dim rb As RadioButton = CType(sender, RadioButton)
            Dim strValues As String = String.Empty
            Select Case rb.Name
                Case rbAnyTime.Name
                    strValues += "Any^10:00 AM"
                Case rbSpecificTime.Name
                    strValues += "St^" & dtpStartat.Value.ToShortTimeString() & "|"
                    strValues += dtpEndat.Value.ToShortTimeString()
            End Select
            If rb.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(rb.Tag, strValues)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub dtpStartat_ValueChanged(sender As Object, e As EventArgs) Handles dtpStartat.ValueChanged, dtpEndat.ValueChanged

        Try
            Dim dtp As DateTimePicker = CType(sender, DateTimePicker)
            Dim strValues As String = String.Empty

            strValues += "St^" & dtpStartat.Value.ToShortTimeString() & "|"
            strValues += dtpEndat.Value.ToShortTimeString()

            If rbSpecificTime.Checked = True Then
                If dtp.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(dtp.Tag, strValues)
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub SetStartEndDate()
        Try
            Me.dtpStartat.Format = DateTimePickerFormat.Custom
            Me.dtpStartat.ShowUpDown = True
            Me.dtpStartat.CustomFormat = "hh:mm:ss tt"
            Me.dtpEndat.Format = DateTimePickerFormat.Custom
            Me.dtpEndat.ShowUpDown = True
            Me.dtpEndat.CustomFormat = "hh:mm:ss tt"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPassword_Leave(sender As Object, e As EventArgs) Handles txtPassword.Leave

        frmConfigCompany.SaveConfiguration("DatabaseBackupPassword", Encrypt(Me.txtPassword.Text.ToString()))

    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If Asc(e.KeyChar) = Keys.Space Then
            MessageBox.Show("Space is not allowed")
        End If
    End Sub

    Private Sub GetSecurityRightsForBackup()
        Try
            Me.lblPassword.Visible = False
            Me.txtPassword.Visible = False
            Me.Panel6.Visible = False
            Me.lblBackupLocation.Visible = False
            Me.txtBacklocation.Visible = False
            Me.btnBrowse.Visible = False
            Me.Panel8.Visible = False

            If Rights Is Nothing Then

                Exit Sub

            End If

            For Each RightstDt As GroupRights In Rights

                If RightstDt.FormControlName = "Backup Password Change" Then
                    Dim Pass As String = getConfigValueByType("DatabaseBackupPassword").ToString()

                    If Pass.Length > 0 AndAlso Pass <> "Error" Then
                        Pass = Decrypt(Pass)
                        Me.txtPassword.Text = Pass
                    Else
                        Me.txtPassword.Text = ""
                    End If

                    Me.lblPassword.Visible = True
                    Me.txtPassword.Visible = True
                    Me.Panel6.Visible = True
                    Me.lblBackupLocation.Visible = True
                    Me.txtBacklocation.Visible = True
                    Me.btnBrowse.Visible = True
                    Me.Panel8.Visible = True

                End If
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            If Me.FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtBacklocation.Text = FolderBrowserDialog1.SelectedPath
                frmConfigCompany.SaveConfiguration("DatabaseBackup", Me.txtBacklocation.Text)
            End If
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