
Public Class frmShowText



    'This button will close this form and replace all the text written in richTextbox into another form.
    Private Sub okButton_Click(sender As Object, e As EventArgs) Handles okButton.Click
        Try
            Me.Close()
            'Call richText function
            richText()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmShowText_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            'escape button will cose the form and replace all the text written in richTextbox into another form.
            If e.KeyCode = Keys.Escape Then
                Me.Close()
                'Call richText function
                richText()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'form closes with this button
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
            'Call richText function
            richText()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    'Passing value from frmShowText's rich textbox to frmDefArticle's rich remarks textbox.
    Public Sub richText()
        frmDefArticle.richRemarks.Rtf = Me.richTextBox.Rtf
    End Sub

End Class