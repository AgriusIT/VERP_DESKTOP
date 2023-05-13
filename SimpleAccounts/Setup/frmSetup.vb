
Public Class frmSetup
    Public _CompanyTitle As String = String.Empty
    Public _CompanyAddress As String = String.Empty
    Public _CompanyPhone As String = String.Empty
    Public _CompanyEmail As String = String.Empty
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            If Not IsValidate() Then Exit Sub
            _CompanyTitle = Me.txtSchoolName.Text
            _CompanyAddress = Me.txtAddress.Text
            _CompanyPhone = Me.txtPhoneNo.Text
            _CompanyEmail = Me.txtEmailAddress.Text
            frmServerSetup.TopLevel = False
            frmServerSetup.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            frmServerSetup.Dock = DockStyle.Fill
            Me.Panel1.Controls.Add(frmServerSetup)
            frmServerSetup.Show()
            frmServerSetup.BringToFront()
            UltraTabControl1.SelectedTab = UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function IsValidate() As Boolean
        Try

            If Not Me.txtSchoolName.Text.Trim.Length > 3 Then
                MsgBox("Please enter valid company name.", MsgBoxStyle.Critical, "SIRIUS")
                txtSchoolName.Focus()
                Return False

            End If

            If Not Me.txtAddress.Text.Trim.Length > 3 Then
                MsgBox("Please enter valid address.", MsgBoxStyle.Critical, "SIRIUS")
                txtAddress.Focus()
                Return False
            End If

            If Not Me.txtPhoneNo.Text.Trim.Length > 3 Then
                MsgBox("Please enter valid phone no.", MsgBoxStyle.Critical, "SIRIUS")
                txtPhoneNo.Focus()
                Return False

            End If

            If Not Me.txtEmailAddress.Text.Trim.Length > 3 Then
                MsgBox("Please enter valid email.", MsgBoxStyle.Critical, "SIRIUS")
                txtEmailAddress.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            CompanyAndConnectionInfo.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class