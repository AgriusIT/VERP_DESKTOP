Public Class frmAutoBackup

    Private Sub btnDNSHost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDNSHost.Click
        Try
            Dim frmD As New frmDomains
            ApplyStyleSheet(frmD)
            If frmD.ShowDialog = Windows.Forms.DialogResult.Yes Then
                Me.txtDNSHost.Text = frmD._HostName.ToString
                'SaveConfiguration("DNSHostForSMS", Me.txtDNSHost.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Utility
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class