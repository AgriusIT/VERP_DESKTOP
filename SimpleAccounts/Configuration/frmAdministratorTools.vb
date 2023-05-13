Public Class frmAdministratorTools

    Private Sub lnkConnectToDatabase_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)

    End Sub
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        Try
            CompanyAndConnectionInfo.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UiButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton2.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'ApplyStyleSheet(frmcreatenewdatabase)
            frmSetup.BringToFront()
            frmSetup.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub UiButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton4.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            frmRestoreBackup.Height = "265"
            frmRestoreBackup.Width = "425"
            frmRestoreBackup.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            frmRestoreBackup.MaximizeBox = False
            frmRestoreBackup.MinimizeBox = False
            frmRestoreBackup.WindowState = FormWindowState.Normal
            frmRestoreBackup.ShowDialog()
            frmRestoreBackup.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub frmAdministratorTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class