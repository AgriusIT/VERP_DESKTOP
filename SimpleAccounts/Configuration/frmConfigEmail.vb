'' TASK TFS4657 Email Notification On Approval Log is not getting and setting  values. Fixed by Muhammad Amin on 04-10-2018
'' TASK TFS4659 User should get or does not get email upon Quotation generating from Comparison Statement. Done by Muhammad Amin
Public Class frmConfigEmail

    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty

    Dim setValue As Boolean = False

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

    Private Sub txtAdminEmail_Leave(sender As Object, e As EventArgs) Handles txtAdminEmail.Leave
        Try
            KeyType = "AdminEmailId"
            KeyValue = Me.txtAdminEmail.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try
            If getConfigValueByType("AdminEmailId").ToString <> "Error" Then
                Me.txtAdminEmail.Text = getConfigValueByType("AdminEmailId").ToString
            Else
                Me.txtAdminEmail.Text = String.Empty
            End If

            If Convert.ToString(getConfigValueByType("FileExportPath").ToString) <> "Error" Then
                Me.txtFileExportPath.Text = Convert.ToString(getConfigValueByType("FileExportPath").ToString)
            Else
                Me.txtFileExportPath.Text = String.Empty
            End If

            Me.chkAttachments.Checked = Convert.ToBoolean(getConfigValueByType("EmailAttachment").ToString)

            If Me.chkAttachments.Checked = False Then
                Me.chkAttachmentsNot.Checked = True
            End If

            Me.chkEmailAlert.Checked = Convert.ToBoolean(getConfigValueByType("EmailAlert").ToString)

            If Me.chkEmailAlert.Checked = False Then
                Me.chkEmailAlertNot.Checked = True
            End If

            '' TASK TFS4437
            Me.chkAutoEmail.Checked = Convert.ToBoolean(getConfigValueByType("AutoEmail").ToString)
            If Me.chkAutoEmail.Checked = False Then
                Me.chkAutoEmailNot.Checked = True
            End If
            ''END TASK TFS4437
            'NotificationEmailonApproval

            '' TASK TFS4444
            If Not getConfigValueByType("EmailNotificationOnApproval").ToString = "Error" Then
                Me.chkEmailNotificationOnApproval.Checked = Convert.ToBoolean(getConfigValueByType("EmailNotificationOnApproval").ToString)
                If Me.chkEmailNotificationOnApproval.Checked = False Then
                    Me.chkEmailNotificationOnApprovalNot.Checked = True
                End If
            End If
            ''END TASK TFS4659

            '' TASK TFS4659
            If Not getConfigValueByType("EmailToUser").ToString = "Error" Then
                Me.rbEmailToUser.Checked = Convert.ToBoolean(getConfigValueByType("EmailToUser").ToString)
                If Me.rbEmailToUser.Checked = False Then
                    Me.rbEmailToUserNot.Checked = True
                End If
            End If
            ''END TASK TFS4659
            Me.cmbDefaultEmail.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("DefaultEmailId").ToString))

            Me.nudDefaultReminder.Value = Convert.ToInt32(Val(getConfigValueByType("DefaultReminder").ToString))

            Me.chkAutoFollowupEmail.Checked = Convert.ToBoolean(getConfigValueByType("AutoFollowUpEmail").ToString)
            If Me.chkAutoFollowupEmail.Checked = False Then
                Me.chkAutoFollowupEmailNot.Checked = True
            End If

            Dim suitableTime As String = String.Empty

            suitableTime = getConfigValueByType("FollowUpEmailTime").ToString()

            Dim specificTime() As String = suitableTime.Split("|")
            If specificTime.Length > 0 Then
                dtpStartat.Value = Convert.ToDateTime(specificTime(0).Trim.Substring(0).ToString())
                dtpEndat.Value = Convert.ToDateTime(specificTime(1).Trim.Substring(0).ToString())
            End If

            Dim userIds As String = getConfigValueByType("FollowUpEmailUsers").ToString()

            Me.lstEmailUsers.SelectItemsByIDs(userIds)

            Me.setValue = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmConfigEmail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetStartEndDate()
        FillCombos("Email")
        FillCombos("Users")
        getConfigValueList()
        GetAllRecords()
    End Sub

    Private Sub txtFileExportPath_Click(sender As Object, e As EventArgs) Handles txtFileExportPath.Click

        Try
            If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtFileExportPath.Text = FolderBrowserDialog1.SelectedPath
                frmConfigCompany.SaveConfiguration("FileExportPath", Me.txtFileExportPath.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub chkAttachments_CheckedChanged(sender As Object, e As EventArgs) Handles chkAttachments.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkEmailAlert_CheckedChanged(sender As Object, e As EventArgs) Handles chkEmailAlert.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub nudDefaultReminder_ValueChanged(sender As Object, e As EventArgs) Handles nudDefaultReminder.ValueChanged
        frmConfigCompany.saveComboBoxNumConfig(sender)
    End Sub

    Private Sub cmbDefaultEmail_Leave(sender As Object, e As EventArgs) Handles cmbDefaultEmail.Leave

        Try

            If Me.cmbDefaultEmail.SelectedValue > 0 Then

                frmConfigCompany.SaveConfiguration("DefaultEmailId", Me.cmbDefaultEmail.SelectedValue)

            Else

                Dim frm As New FrmEmailconfig
                frm.SplitContainer1.Panel1Collapsed = True
                frm.Size = New Size(565, 479)
                frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
                frm.StartPosition = FormStartPosition.CenterScreen
                frm.ShowDialog()
                FillCombos("Email")
                frmConfigCompany.SaveConfiguration("DefaultEmailId", Me.cmbDefaultEmail.SelectedValue)

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub FillCombos(Optional ByVal Condition As String = "")

        Try
            Dim strSQL As String = String.Empty
            Select Case Condition
                Case "Email"
                    strSQL = "select EmailId as ID , Email as [Account Description] From TblDefEmail where Email is not null "
                    FillDropDown(Me.cmbDefaultEmail, strSQL, True)

                Case "Users"
                    FillListBox(Me.lstEmailUsers.ListItem, "select USER_ID , FULLNAME from tblUser where Email != ''")
            End Select

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
    ''' <summary>
    ''' TASK TFS4437
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub chkAutoEmail_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoEmail.CheckedChanged
        Try
            frmConfigCompany.saveRadioBtnConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkNotificationEmailonApproval_CheckedChanged(sender As Object, e As EventArgs) Handles chkEmailNotificationOnApproval.CheckedChanged
        'NotificationEmailonApproval
        Try
            frmConfigCompany.saveRadioBtnConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkAutoFollowupEmail_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoFollowupEmail.CheckedChanged
        Try
            frmConfigCompany.saveRadioBtnConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpStartat_ValueChanged(sender As Object, e As EventArgs) Handles dtpStartat.ValueChanged, dtpEndat.ValueChanged
        Try
            Dim dtp As DateTimePicker = CType(sender, DateTimePicker)
            Dim strValues As String = String.Empty

            strValues += dtpStartat.Value.ToShortTimeString() & "|"
            strValues += dtpEndat.Value.ToShortTimeString()

            If Me.setValue = True Then
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

    Private Sub lstEmailUsers_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstEmailUsers.SelectedIndexChaned

        If Me.lstEmailUsers.SelectedIDs.Length > 0 Then
            Dim strIds As String = Me.lstEmailUsers.SelectedIDs

            If Me.setValue = True Then
                If strIds.Length > 0 Then frmConfigCompany.SaveConfiguration("FollowUpEmailUsers", strIds)
            End If
        End If

    End Sub

    Private Sub rbEmailToUser_CheckedChanged(sender As Object, e As EventArgs) Handles rbEmailToUser.CheckedChanged
        Try
            frmConfigCompany.saveRadioBtnConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Panel6_Paint(sender As Object, e As PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs) Handles Panel7.Paint

    End Sub

    Private Sub Panel8_Paint(sender As Object, e As PaintEventArgs) Handles Panel8.Paint

    End Sub

    Private Sub Panel9_Paint(sender As Object, e As PaintEventArgs) Handles Panel9.Paint

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class