'15-Jun-2015 'Task#1 Ahmad Sharif: email configurations user wise

Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Net
Imports System.Net.Mail
Imports SimpleAccounts
Public Class frmOutgoingEmail
    Private _Email As SBModel.SendingEmail
    Public Property Email() As SBModel.SendingEmail
        Get
            Return _Email
        End Get
        Set(ByVal value As SBModel.SendingEmail)
            _Email = value
        End Set
    End Property
    Private GetEmailConfig As List(Of EmailSeeting)
    Public txtCc As New TextBox
    Public txtBcc As New TextBox
    Public lblCc As New Label
    Public lblBcc As New Label
    Private isLoaded As Boolean = False
    Private Sub frmOutgoingEmail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ApplyStyleSheet(Me)
            FillCombos()
            Me.txtAttachFile.Text = String.Empty
            Me.lblProgress.Text = String.Empty
            Me.sendProgress.Value = 0
            Me.btnRemove.Enabled = True
            Me.btnRemove1.Enabled = True
            Me.btnBrowse.Enabled = True
            Me.btnBrowse1.Enabled = True
            Me.TextBox1.Text = LoginUser.DisplayName  'Logged_In_Users.DisplayName.ToString
            Me.cmbFrom.Text = LoginUser.LoginUserEmail 'Logged_In_Users.LoggedInUserEmail.ToString

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try

            Dim dt As DataTable = New EmailSettingDAL().GetAllRecords
            Me.cmbFrom.ComboBox.DisplayMember = dt.Columns("Email").ColumnName
            Me.cmbFrom.ComboBox.ValueMember = dt.Columns("EmailId").ColumnName
            Me.cmbFrom.ComboBox.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub SendEmail()

        If Me.BackgroundWorker1.IsBusy Then Exit Sub
        Me.BackgroundWorker1.RunWorkerAsync()
        Do While Me.BackgroundWorker1.IsBusy
            Application.DoEvents()
        Loop
        Me.sendProgress.Value = 0
        Me.lblProgress.Text = "Verify mail setting ..."
        Do Until Me.sendProgress.Value > 15
            Me.sendProgress.Value = Me.sendProgress.Value + 1
            Application.DoEvents()
            System.Threading.Thread.Sleep(50)
        Loop
        If GetEmailConfig Is Nothing Then Exit Sub
        Me.lblProgress.Text = ""
        Me.lblProgress.Text = "Validating information ..."
        Do Until Me.sendProgress.Value > 15
            Me.sendProgress.Value = Me.sendProgress.Value + 1
            Application.DoEvents()
            System.Threading.Thread.Sleep(50)
        Loop
        Dim sndEmail As New MailMessage
        sndEmail.To.Add(Me.txtTo.Text.ToString.Replace(";", ","))
        If Me.txtCc.Text <> String.Empty Then
            sndEmail.CC.Add(Me.txtCc.Text.ToString.Replace(";", ","))
        End If
        If Me.txtBcc.Text <> String.Empty Then
            sndEmail.Bcc.Add(Me.txtBcc.Text.ToString.Replace(";", ","))
        End If

        sndEmail.Subject = Me.txtSubject.Text
        sndEmail.Body = Me.txtBody.Text

        'Task#1 check if logged in user has email ,then set user email in from otherwise set default email id
        Dim emailID() As String
        emailID = LoginUser.LoginUserEmail.Split("@")      'split array on @ sign and store before and after strings in array
        Dim domainName As String = String.Empty


        'check if array has length 2 then after path of @ sign store in domainName variable
        If emailID.Length = 2 Then
            domainName = "@" & emailID(1)
        End If

        cmbFrom_SelectedIndexChanged(Nothing, Nothing)

        'check if domain same then execute if otherwise else
        If GetEmailConfig(0).Host.ToUpper.Trim = domainName.ToUpper.Trim Then
            sndEmail.From = New MailAddress(IIf(LoginUser.LoginUserEmail.Length > 0, LoginUser.LoginUserEmail.ToString, Me.cmbFrom.Text.ToString), IIf(LoginUser.DisplayName.Length > 0, LoginUser.DisplayName.ToString, Me.TextBox1.Text.ToString))
        Else
            sndEmail.From = New MailAddress(Me.cmbFrom.Text.ToString, Me.TextBox1.Text.ToString)
        End If

        'End Task#1

        If Me.txtAttachFile.Text <> String.Empty Then
            'Dim DirInfo As IO.DirectoryInfo = New IO.DirectoryInfo(txtAttachFile.Text.Substring(0, Me.txtAttachFile.Text.LastIndexOf("\")))
            'Dim FolderSecurity As New System.Security.AccessControl.DirectorySecurity
            'FolderSecurity.AddAccessRule(New System.Security.AccessControl.FileSystemAccessRule(Environment.UserDomainName & "\\" & Environment.UserName, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow))
            'DirInfo.SetAccessControl(FolderSecurity)
            If IO.File.Exists(Me.txtAttachFile.Text) = True Then
                Dim my_AttachmentFile As Mail.Attachment = New Mail.Attachment(Me.txtAttachFile.Text)
                sndEmail.Attachments.Add(my_AttachmentFile)
            End If
        End If
        If Me.txtDataFile.Text <> String.Empty Then
            'Dim DirInfo As IO.DirectoryInfo = New IO.DirectoryInfo(txtDataFile.Text.Substring(0, Me.txtDataFile.Text.LastIndexOf("\")))
            'Dim FolderSecurity As New System.Security.AccessControl.DirectorySecurity
            'FolderSecurity.AddAccessRule(New System.Security.AccessControl.FileSystemAccessRule(Environment.UserDomainName & "\\" & Environment.UserName, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow))
            'DirInfo.SetAccessControl(FolderSecurity)
            If IO.File.Exists(txtDataFile.Text) = True Then
                Dim my_AttachmentFile1 As Mail.Attachment = New Mail.Attachment(Me.txtDataFile.Text)
                sndEmail.Attachments.Add(my_AttachmentFile1)
            End If

        End If
        Me.lblProgress.Text = ""
        Me.lblProgress.Text = "Log on to the server ..."
        Do Until Me.sendProgress.Value > 50
            Me.sendProgress.Value = Me.sendProgress.Value + 1
            Application.DoEvents()
            System.Threading.Thread.Sleep(50)
        Loop
        sndEmail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
        Dim Client As New SmtpClient(GetEmailConfig(0).SmtpServer)
        Client.Port = GetEmailConfig(0).port
        Dim EmailPwd As String = Decrypt(GetEmailConfig(0).EmailPassword)
        Client.Credentials = New System.Net.NetworkCredential(GetEmailConfig(0).Email, EmailPwd)
        Client.EnableSsl = IIf(GetEmailConfig(0).ssl = True, True, False)
        Client.DeliveryMethod = SmtpDeliveryMethod.Network
        Me.lblProgress.Text = ""
        Me.lblProgress.Text = "Sending mail ..."
        Do Until Me.sendProgress.Value > 95
            Me.sendProgress.Value = Me.sendProgress.Value + 1
            Application.DoEvents()
            System.Threading.Thread.Sleep(50)
        Loop
        Try
            Client.Send(sndEmail)
            sndEmail.Dispose()

            If IO.File.Exists(Me.txtAttachFile.Text) = True Then
                IO.File.Delete(Me.txtAttachFile.Text)
            End If
            If IO.File.Exists(Me.txtDataFile.Text) = True Then
                IO.File.Delete(Me.txtDataFile.Text)
            End If
            Me.lblProgress.Text = ""
            Me.lblProgress.Text = "Mail send successfully"
            Do Until Me.sendProgress.Value > 99
                Me.sendProgress.Value = Me.sendProgress.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            Me.txtTo.Text = String.Empty
            Me.txtCc.Text = String.Empty
            Me.txtBcc.Text = String.Empty
            Me.txtSubject.Text = String.Empty
            Me.txtBody.Text = String.Empty
            Me.txtDataFile.Text = String.Empty
            Me.txtAttachFile.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Try
            lblErrorMessage.Text = String.Empty
            SendEmail()
            If Not DialogResult = Windows.Forms.DialogResult.OK Then
                Me.Close()
            End If
        Catch ex As Exception
            Me.lblErrorMessage.ForeColor = Color.Red
            Me.lblErrorMessage.Text = ex.Message.ToString

        End Try
    End Sub
    'Private Sub cmbFrom_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFrom.SelectedIndexChanged
    '    Try
    '        GetEmailConfig = New EmailSettingDAL().GetEmailSetting(Me.cmbFrom.Text)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            If Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtAttachFile.Text = Me.OpenFileDialog1.FileName
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnBrowse1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse1.Click
        Try

            If Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtDataFile.Text = Me.OpenFileDialog1.FileName
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRemove1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove1.Click
        Try
            Me.txtDataFile.Text = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Try
            Me.txtAttachFile.Text = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmOutgoingEmail_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Me.lblErrorMessage.Text = ex.Message.ToString
        Finally

        End Try
    End Sub
    Private Sub btnCCAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCCAdd.Click
        Try
            If txtCc.Name = "txtCc" Then Exit Sub
            ' Add Cc TextBox
            txtCc.Size = New Size(452, 25)
            txtCc.Name = "txtCc"
            txtCc.TabIndex = 6
            txtCc.Multiline = True
            Me.Controls.Add(txtCc)

            'Add cc Label
            lblCc.Text = "Cc:"
            lblCc.Name = "lblCc"
            Me.lblCc.TabIndex = 5
            Me.Controls.Add(lblCc)

            If Me.txtBcc.Location.X > 0 Then
                lblBcc.Location = New Point(22, 129)
                txtBcc.Location = New Point(95, 126)
                lblCc.Location = New Point(22, 103)
                txtCc.Location = New Point(95, 100)
                Me.txtSubject.Location = New Point(95, 126)
                Me.lblSubject.Location = New Point(22, 129)
                Me.txtBody.Size = New Size(452, 199)
                Me.txtBody.Location = New Point(95, 183)
                Me.Label2.Location = New Point(22, 183)
            Else
                lblCc.Location = New Point(22, 103)
                txtCc.Location = New Point(95, 100)
                Me.txtSubject.Location = New Point(95, 126)
                Me.lblSubject.Location = New Point(22, 129)
                Me.txtBody.Size = New Size(452, 199)
                Me.txtBody.Location = New Point(95, 183)
                Me.Label2.Location = New Point(22, 183)
            End If

        Catch ex As Exception
            Me.lblErrorMessage.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub btnBCCAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBCCAdd.Click
        Try

            If Me.txtBcc.Name = "txtBcc" Then Exit Sub
            ' Add Cc TextBox
            txtBcc.Size = New Size(452, 25)
            txtBcc.Name = "txtBcc"
            txtBcc.TabIndex = 7
            txtBcc.Multiline = True
            Me.Controls.Add(txtBcc)

            'Add cc Label
            lblBcc.Text = "Bcc:"
            lblBcc.Name = "lblBcc"
            lblBcc.TabIndex = 8
            Me.Controls.Add(lblBcc)

            If Me.txtCc.Location.X = 0 Then
                lblBcc.Location = New Point(22, 103)
                txtBcc.Location = New Point(95, 100)
                Me.txtSubject.Location = New Point(95, 126)
                Me.lblSubject.Location = New Point(22, 129)
                Me.txtBody.Size = New Size(452, 199)
                Me.txtBody.Location = New Point(95, 183)
                Me.Label2.Location = New Point(22, 183)
            Else
                lblCc.Location = New Point(22, 103)
                txtCc.Location = New Point(95, 100)
                lblBcc.Location = New Point(22, 129)
                txtBcc.Location = New Point(95, 126)
                Me.txtSubject.Location = New Point(95, 152)
                Me.lblSubject.Location = New Point(22, 155)
                Me.txtBody.Size = New Size(452, 199)
                Me.txtBody.Location = New Point(95, 183)
                Me.Label2.Location = New Point(22, 183)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub cmbFrom_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFrom.SelectedIndexChanged
    '    Try
    '        If Me.cmbFrom.Items Is Nothing Then Exit Sub

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub cmbFrom_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFrom.SelectedIndexChanged
        Try

            'If isLoaded = False Then
            '    Exit Sub
            'End If
            GetEmailConfig = New EmailSettingDAL().GetEmailSetting(Me.cmbFrom.Text)
            If GetEmailConfig Is Nothing Then Exit Sub

            Me.TextBox1.Text = GetEmailConfig.Item(0).DisplayName.ToString
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AddEmailAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddEmailAccountToolStripMenuItem.Click
        Try
            Dim frm As New FrmEmailconfig
            frm.SplitContainer1.Panel1Collapsed = True
            frm.Size = New Size(565, 479)
            frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            frm.StartPosition = FormStartPosition.CenterScreen
            ApplyStyleSheet(frm)
            frm.ShowDialog()
            FillCombos()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAdd_ButtonClick(sender As Object, e As EventArgs) Handles btnAdd.ButtonClick

    End Sub
End Class