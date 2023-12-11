Public Class ucSMSBalance
    Private _lblSMS As String = String.Empty
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            _lblSMS = GetBrandedSMSBalance()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ucSMSBalance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lbl As New Label
        lbl.Visible = True
        lbl.BringToFront()
        lbl.Dock = DockStyle.Fill
        lbl.BackColor = Color.White
        lbl.Text = "Loading please wait...."
        Me.Controls.Add(lbl)
        Try

            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop

            Me.lblSMSBalance.Text = _lblSMS
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
            Me.Controls.Remove(lbl)
        End Try
    End Sub
End Class
