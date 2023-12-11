Public Class dialouge
    Public Shared IsReleaseUpdaterForm = True
    Private Sub dialouge_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        frmbg.Hide()
    End Sub

    'Private Sub dialouge_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    '    If e.KeyCode = Keys.Escape Then
    '        Me.Close()
    '    End If
    'End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.OK
            IsReleaseUpdaterForm = False
            frmbg.Hide()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Ignore
            IsReleaseUpdaterForm = False
            frmbg.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            IsReleaseUpdaterForm = True
            frmbg.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class