Public Class frmMainHome

    Private Sub frmMainHome_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnAddDock.FlatAppearance.BorderSize = 0
        lblusername.Text = "WELCOME! " & LoginUserName & ""
    End Sub
    ''' <summary>
    ''' Open Dashboard on click of hand image
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Try
            frmMain.LoadControl("frmDashboard")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblusername_Click(sender As Object, e As EventArgs) Handles lblusername.Click

    End Sub

    Private Sub btnAddDock_Click(sender As Object, e As EventArgs) Handles btnAddDock.Click

    End Sub
End Class