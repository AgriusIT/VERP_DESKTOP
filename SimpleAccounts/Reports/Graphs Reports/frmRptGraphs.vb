Public Class frmRptGraphs
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        Try
            frmMain.LoadControl("frmGrdRptChart")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmRptGraphs_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class