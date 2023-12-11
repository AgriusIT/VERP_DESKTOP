Imports System
Imports System.Diagnostics.Process
Imports System.Management
Imports System.Net
Imports System.Reflection

Public Class frmaboutus
    Declare Auto Function SetParent Lib "user32.dll" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As Integer
    Private Sub frmaboutus_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim LicenseKey As String = Decrypt(getConfigValueByType("LicenseKey").ToString)
            Dim proc As New Process
            Me.lblServerName.Text = Con.DataSource
            Me.lblDatabaseName.Text = Con.Database
            Me.lblVersion.Text = frmModProperty.dbVersion
            Me.lblAppVer.Text = Application.ProductVersion
            Me.lblProduct.Text = LicenseVersion

            lblStatus.Text = LicenseStatus
            Me.lblExpiryLabel.Text = IIf(LicenseExpiryType <> "Monthly", "Service Expiry: ", "License Expiry: ")

            If LicenseStatus = "Blocked" Then

                lblStatus.ForeColor = Color.Red

            ElseIf LicenseStatus = "Expired" Then

                lblStatus.ForeColor = Color.Yellow

            ElseIf LicenseStatus = "Expiring" Then

                lblStatus.ForeColor = Color.FromArgb(0, 91, 174)

            Else

                lblStatus.ForeColor = Color.Green

            End If

            If LicenseStatus <> "Blocked" AndAlso LicenseStatus <> "Expired" AndAlso gblnTrialVersion Then
                lblStatus.ForeColor = Color.Red
                lblStatus.Text = "Trial"
            End If

            If LicenseKey.Contains("|") Then

                Dim str As String() = LicenseKey.Split("|")
                For Each key As String In str
                    If key.Contains("=") Then
                        Dim strKey As String() = key.Split("=")

                        Select Case strKey(0).ToString
                            Case "key"
                                Me.txtLicenseKey.Text = strKey(1)
                                lblRegistrationKey.Text = txtLicenseKey.Text

                            Case "expiry"
                                Me.lblExpiryDate.Text = CType(strKey(1).ToString, DateTime).ToString("dd-MMM-yyyy")
                            Case "dbname"
                                Me.lblDatabaseName.Text = strKey(1)
                            Case "users"
                                Me.lblUsers.Text = strKey(1)
                        End Select


                    End If
                Next

            Else

                txtLicenseKey.Text = String.Empty
                lblRegistrationKey.Text = "License key is missing"

            End If
            Me.pnlLicenseUpdate.Visible = False
            If getConfigValueByType("HelpnSupportPanel").ToString = "False" Then
                Me.Panel1.Visible = False
                ''TASK TFS1175
                Me.Label5.Visible = False
                Me.Label6.Visible = False
                ''END TASK TFS1175
            Else
                Me.Panel1.Visible = True
                ''TASK TFS1175
                Me.Label5.Visible = True
                Me.Label6.Visible = True
                ''END TASK TFS1175
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Process.Start("http://www.siriussolution.com/")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            Process.Start("mailto:info@siriussolution.com")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSaveKey_Click(sender As Object, e As EventArgs) Handles btnSaveKey.Click

        Try

            'Dim KeyList As New System.Text.StringBuilder

            'Dim assembly__1 = Assembly.GetExecutingAssembly()
            'Dim resourceName = "JMT.ReleaseUpdate.txt"
            'Using Stream1 As IO.Stream = assembly__1.GetManifestResourceStream(resourceName)

            '    Using reader1 As IO.StreamReader = New IO.StreamReader(Stream1)
            '        KeyList = reader1.ReadToEnd
            '    End Using

            'End Using

            If txtLicenseKey.Text.Trim.Replace(" ", "").Replace("-", "").Length <> 20 Then
                msg_Error("Please enter a valid license key.")
                Exit Sub
            End If

            If msg_Confirm("Do you want to update your key?") = True Then

                Dim LicenseKey As String = Decrypt(getConfigValueByType("LicenseKey").ToString)
                Dim KeyString As String = String.Empty
                Dim ExpiryString As String = String.Empty
                Dim SystemIdString As String = String.Empty

                If LicenseKey.Contains("|") Then

                    Dim str As String() = LicenseKey.Split("|")
                    For Each key As String In str
                        If key.Contains("=") Then
                            Dim strKey As String() = key.Split("=")

                            Select Case strKey(0).ToString
                                Case "key"

                                    KeyString = "key=" & Me.txtLicenseKey.Text.ToString.Trim.Replace(" ", "").Replace("-", "").ToUpper


                                Case "expiry"

                                    ExpiryString = key
                                Case "systemid"
                                    SystemIdString = key

                            End Select


                        End If
                    Next

                Else

                    KeyString = "key=" & Me.txtLicenseKey.Text.ToString.Trim.Replace(" ", "").Replace("-", "").ToUpper
                    Dim VDate As DateTime

                    Dim dt As DataTable = GetDataTable(" select min(voucher_date) from tblVoucher ")

                    If dt.Rows.Count > 0 Then
                        VDate = dt.Rows(0).Item(0).ToString
                    Else
                        VDate = Date.Now()
                    End If

                    If VDate <= CType("30-Nov-2016 23:59:59", DateTime) Then

                        VDate = "31-Dec-2016 23:59:59"

                    Else
                        VDate = VDate.AddMonths(1)
                    End If

                    ExpiryString = "expiry=" & VDate.ToString("dd-MMM-yyyy")

                    SystemIdString = "systemid="
                End If


                frmSystemConfigurationNew.SaveConfiguration("LicenseKey", Encrypt(KeyString & "|" & ExpiryString & "|" & SystemIdString))
                msg_Information("Key saved successfully, thank you for using Agrius ERP.")
                pnlLicenseUpdate.Visible = False
                frmaboutus_Load(Me, Nothing)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtLicenseKey_TextChanged(sender As Object, e As EventArgs)

    End Sub


    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        frmaboutus_Load(Me, Nothing)
    End Sub

    Private Sub lblExpiryDate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblExpiryDate.LinkClicked

        pnlLicenseUpdate.Visible = True

    End Sub

    Private Sub lblRegistrationKey_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblRegistrationKey.LinkClicked
        Clipboard.SetText(lblRegistrationKey.Text)
        msg_Information("Key copied to clipboard")
        'pnlLicenseUpdate.Visible = True
        'pnlLicenseUpdate.BringToFront()

    End Sub

    Private Sub btnGetFingerPrints_Click(sender As Object, e As EventArgs) Handles btnGetFingerPrints.Click
        Try
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK AndAlso SaveFileDialog1.CheckPathExists = True Then

                If SaveFileDialog1.CheckFileExists() = True Then
                    msg_Error("File already exist")
                    Exit Sub
                End If

                Dim strFingerPrintsValue As String = "type=aitfp|dbname=" & Me.lblDatabaseName.Text.ToString.ToUpper & "|expriy=" & lblExpiryDate.Text.ToString.ToUpper & "|users=" & Me.lblUsers.Text.ToString.ToUpper
                IO.File.WriteAllText(SaveFileDialog1.FileName.ToString, Encrypt(strFingerPrintsValue))

                msg_Information("Finger print generted successfully. You can get file from " & SaveFileDialog1.FileName)

            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnLoadLicense_Click(sender As Object, e As EventArgs) Handles btnLoadLicense.Click
        Try
            If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK AndAlso IO.File.Exists(OpenFileDialog1.FileName) Then

                Me.lblProgress.Text = "Loading Please Wait ..."
                Me.lblProgress.BackColor = Color.LightYellow
                Me.lblProgress.Visible = True
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()
                Me.lblLicenseKey.Text = IO.File.ReadAllText(OpenFileDialog1.FileName)
                Dim LicenseKey As String = Decrypt(lblLicenseKey.Text)
                If LicenseKey.Contains("|") Then
                    Dim str As String() = LicenseKey.Split("|")
                    For Each key As String In str
                        If key.Contains("=") Then
                            Dim strKey As String() = key.Split("=")
                            Select Case strKey(0).ToString
                                Case "dbname"
                                    If lblDatabaseName.Text = strKey(1) Then
                                        frmSystemConfigurationNew.SaveConfiguration("LicenseKey", IO.File.ReadAllText(OpenFileDialog1.FileName))
                                        msg_Information("Key saved successfully, thank you for using Agrius products.")
                                        frmaboutus_Load(Me, Nothing)
                                        pnlLicenseUpdate.Visible = False
                                    Else
                                        msg_Error("Please enter a valid license key.")
                                        Exit Sub
                                    End If
                            End Select
                        End If
                    Next
                Else
                    txtLicenseKey.Text = String.Empty
                    lblRegistrationKey.Text = "License key is missing"
                End If
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub btnViewMessageLog_Click(sender As Object, e As EventArgs) Handles btnViewMessageLog.Click
        'frmMessageLog.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'frmAboutUsdialog.ShowDialog()      
        frmAboutUsdialog.ShowDialog()
        'End of license code for api
        frmaboutus_Load(Me, Nothing)
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
End Class