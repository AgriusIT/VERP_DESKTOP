Public Class frmShowSalesRichtext

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
    'Passing value from frmShowRichText's rich textbox to frmSales's rich remarks textbox.
    Public Sub richText()
        frmSales.richTxtInternalRemarks.Rtf = Me.richTextBox.Rtf
    End Sub

    Private Sub frmShowSalesRichtext_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            'escape button will close the form and replace all the text written in richTextbox into another form.
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
End Class