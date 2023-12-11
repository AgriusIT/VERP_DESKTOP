Public Class frmBackupReminder

    'Private Sub pnlCloseForm_Click(sender As Object, e As EventArgs) Handles pnlCloseForm.Click
    '    Me.Close()

    'End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        DialogResult = Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub btnSkip_Click(sender As Object, e As EventArgs) Handles btnSkip.Click
        DialogResult = Windows.Forms.DialogResult.No
        Me.Close()
    End Sub

    Private Sub frmBackupReminder_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class