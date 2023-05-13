Public Class frmAB

    Private Sub btnAgent_Click(sender As Object, e As EventArgs) Handles btnAgent.Click
        Try
            Dim Agent As New frmProAgent(Val(Me.txtAgent.Text))
            Agent.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAgent_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAgent.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDealer_Click(sender As Object, e As EventArgs) Handles btnDealer.Click
        Try
            Dim Dealer As New frmDealer(Val(Me.txtDealer.Text))
            Dealer.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDealer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDealer.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEstate_Click(sender As Object, e As EventArgs) Handles btnEstate.Click
        Try
            Dim Estate As New frmProEstateList(Val(Me.txtEstate.Text))
            Estate.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtEstate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEstate.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class