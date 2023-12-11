Public Class frmAddAdvanceTpye

    Private Sub frmAddAdvanceTpye_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.F4 Then
            'Save function
        End If
        If e.KeyCode = Keys.Insert Then
            'Save+ function
        End If
        If e.KeyCode = Keys.F5 Then
            'Refresh Function
        End If
    End Sub

    Private Sub frmAddAdvanceTpye_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnCancel.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.BorderSize = 0
    End Sub
End Class