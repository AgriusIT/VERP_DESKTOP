Imports SBDal
Imports SBModel
Public Class FrmEmailconfig
    Implements IGeneral
    Dim Emailconfig As EmailSeeting
    Dim Email_id As Integer
    Sub SelectMail()
        Me.cmbport.Text = String.Empty
        Me.CmbSmtp.Text = String.Empty
        cmbport.Items.Clear()
        CmbSmtp.Items.Clear()
        If CmbMail.Text = "@yahoo.com" Then
            Me.cmbport.Items.Add("465")
            Me.CmbSmtp.Items.Add("smtp.mail.yahoo.com")
        ElseIf CmbMail.Text = "@ymail.com" Then
            Me.cmbport.Items.Add("465")
            Me.CmbSmtp.Items.Add("smtp.mail.yahoo.com")
        ElseIf CmbMail.Text = "@hotmail.com" Then
            Me.cmbport.Items.Add("587")
            Me.CmbSmtp.Items.Add("smtp.live.com")
        ElseIf CmbMail.Text = "@live.com" Then
            Me.cmbport.Items.Add("587")
            Me.CmbSmtp.Items.Add("smtp.live.com")
        ElseIf CmbMail.Text = "@msn.com" Then
            Me.cmbport.Items.Add("587")
            Me.CmbSmtp.Items.Add("smtp.live.com")
        ElseIf CmbMail.Text = "@gmail.com" Then
            Me.cmbport.Items.Add("587")
            Me.CmbSmtp.Items.Add("smtp.gmail.com")
        ElseIf CmbMail.Text = "@siriussolution.net" Then
            Me.cmbport.Items.Add("465")
            Me.CmbSmtp.Items.Add("siriussolution.com")
        ElseIf CmbMail.Text = "@aol.com" Then
            Me.cmbport.Items.Add("587")
            Me.CmbSmtp.Items.Add("smtp.aol.com")
        End If
        If Not Me.CmbMail.SelectedIndex = -1 Then
            Me.CmbSmtp.SelectedIndex = 0
            Me.cmbport.SelectedIndex = 0
        End If
    End Sub

    Private Sub FrmEmailconfig_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub FrmEmailconfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            CmbMail.SelectedIndex = 0
            SelectMail()
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default


        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If IsValidate() = True Then
                Call New EmailSettingDAL().Delete(Emailconfig)
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Emailconfig = New EmailSeeting
            Emailconfig.EmailID = Email_id
            Emailconfig.EmailPassword = Encrypt(Me.txtpassword.Text)
            Emailconfig.EmailUser = Me.txtemail.Text
            Emailconfig.Host = Me.CmbMail.Text
            Emailconfig.Email = Me.txtemail.Text + "" + Me.CmbMail.Text
            Emailconfig.DisplayName = Me.txtDisplayName.Text
            Emailconfig.SmtpServer = Me.CmbSmtp.Text
            Emailconfig.port = Me.cmbport.Text
            Emailconfig.ssl = Me.chckactive.Checked
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.GridEX1.DataSource = New EmailSettingDAL().GetAllRecords
            Me.GridEX1.RetrieveStructure()
            For Each Col As Janus.Windows.GridEX.GridEXColumn In GridEX1.RootTable.Columns
                If Col.Index <> 1 AndAlso Col.Index <> 2 AndAlso Col.Index <> 4 Then
                    Col.Visible = False
                End If
            Next
            Me.GridEX1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtDisplayName.Text = String.Empty Then
                ShowErrorMessage("Please Enter DisplayName")
                Me.txtDisplayName.Focus()
                Return False
            End If
            If Me.txtemail.Text = String.Empty Then
                ShowErrorMessage("Please Enter Email")
                Me.txtemail.Focus()
                Return False
            End If
            If Me.txtpassword.Text = String.Empty Then
                ShowErrorMessage("Please Enter Password")
                Me.txtpassword.Focus()
            End If
            If Me.CmbSmtp.Text = String.Empty Then
                ShowErrorMessage("Select Smpt Server")
                Me.CmbSmtp.Focus()
            End If
            If Me.cmbport.Text = String.Empty Then
                ShowErrorMessage("Please select port")
                Me.cmbport.Focus()
            End If
            Call FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Email_id = 0
            Me.btnSave.Text = "&Save"
            Me.txtDisplayName.Text = String.Empty
            Me.txtemail.Text = String.Empty
            Me.txtpassword.Text = String.Empty
            Me.lblProcess.Text = String.Empty
            Me.lblMessage.Text = String.Empty
            GetAllRecords()
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() = True Then
                Call New EmailSettingDAL().add(Emailconfig)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If IsValidate() = True Then
                Call New EmailSettingDAL().Update(Emailconfig)
                Return True
            Else
                Return False

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.btnSave.Text = "&Save" Then

                'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                'If Not msg_Confirm(str_ConfirmSave) = True Then
                'Exit Sub
                'End If
                If Save() = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                End If
                'msg_Information(str_informSave)
                ReSetControls()
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then
                    Exit Sub
                End If

                'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Update1() = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                End If
                'msg_Information(str_informUpdate)
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        Try
            Email_id = Me.GridEX1.GetRow.Cells("EmailId").Value
            Me.txtDisplayName.Text = Me.GridEX1.GetRow.Cells("DisplyName").Value
            Me.txtemail.Text = Me.GridEX1.GetRow.Cells("EmailUser").Value.ToString
            Me.CmbMail.Text = Me.GridEX1.GetRow.Cells("Host").Value.ToString
            Me.txtpassword.Text = Decrypt(Me.GridEX1.GetRow.Cells("EmailPassword").Value)
            Me.CmbSmtp.Text = Me.GridEX1.GetRow.Cells("SmtpServer").Value
            Me.cmbport.Text = Me.GridEX1.GetRow.Cells("port").Value
            Me.chckactive.Checked = Me.GridEX1.GetRow.Cells("SSL").Value
            Me.btnSave.Text = "&Update"
            GetSecurityRights()
            Me.txtDisplayName.Focus()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Email_id = 0 Then
                ShowErrorMessage("Please Select Record")
                Me.txtDisplayName.Focus()
                Exit Sub

            End If
            If Not IsValidate() = True Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

            'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Delete() Then DialogResult = Windows.Forms.DialogResult.Yes
            'msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProcess.Visible = False
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            GridEX1_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CmbMail_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbMail.SelectedIndexChanged
        Try
            Call SelectMail()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnTestingEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestingEmail.Click
        Try
            If Me.txtemail.Text = String.Empty Then Exit Sub
            Me.lblMessage.Text = String.Empty
            Me.lblProcess.Text = String.Empty
            Dim strEmail As String = Me.txtemail.Text + "" + Me.CmbMail.Text
            Dim Email As New Net.Mail.MailMessage
            Me.lblProcess.Text = "Verify Information ..."
            System.Threading.Thread.Sleep(600)
            Application.DoEvents()
            Email.From = New Net.Mail.MailAddress(strEmail, Me.txtDisplayName.Text)
            Email.To.Add(strEmail)
            Email.Subject = "Testing mail"
            Email.Body = "Thank you! your mail has been send successfully ..."
            Me.lblProcess.Text = "Log On to Server ..."
            System.Threading.Thread.Sleep(600)
            Application.DoEvents()
            Dim SmtpClient As New Net.Mail.SmtpClient(Me.CmbSmtp.Text)
            SmtpClient.Credentials = New Net.NetworkCredential(strEmail, Me.txtpassword.Text)
            SmtpClient.Port = Me.cmbport.Text
            SmtpClient.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            SmtpClient.EnableSsl = Me.chckactive.Checked
            Me.lblProcess.Text = "Sending mail ..."
            System.Threading.Thread.Sleep(600)
            Application.DoEvents()
            SmtpClient.Send(Email)
            Me.lblProcess.Text = "Your mail has been send successfully ..."
            Me.lblMessage.Text = "Successfully"
            Me.lblMessage.ForeColor = Color.Green
            System.Threading.Thread.Sleep(600)
            Application.DoEvents()

        Catch ex As Exception
            Me.lblProcess.Text = String.Empty
            lblMessage.Text = ex.Message.ToString
            lblMessage.ForeColor = Color.Red
            System.Threading.Thread.Sleep(600)
            Application.DoEvents()
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub GridEX1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub lblProgress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblProgress.Click

    End Sub
End Class