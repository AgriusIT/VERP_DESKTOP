Public Class frmTrialVersion

    Private Sub lblContinue_Click(sender As Object, e As EventArgs) Handles lblContinue.Click
        Try
            If MaskedTextBox1.Visible = True AndAlso MaskedTextBox1.Text.Length > 0 Then
                If Me.MaskedTextBox1.Text = "" Then
                    MsgBox("Please enter license key")
                    MaskedTextBox1.Focus()
                Else
                    'Me.Close()
                    Me.Visible = False

                    frmActiveLicense.LicenseKey = Me.MaskedTextBox1.Text
                    frmActiveLicense.StartPosition = FormStartPosition.CenterScreen
                    frmActiveLicense.IsFormOpenedFromTrial = True
                    frmActiveLicense.ShowDialog()
                End If
            Else
                Me.Close()
            End If

            'If MaskedTextBox1.Visible = True Then
            '    If Not MaskedTextBox1.Text = "00371-177-0000061-85537" Then
            '        ShowErrorMessage("Please enter a valid key")
            '        MaskedTextBox1.Focus()
            '        Exit Sub
            '    End If
            '    Me.lblProgress.Visible = True
            '   
            'Else
            '    Me.Close()
            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Public Function CheckInterNet() As Boolean
        Try
            'If My.Computer.Network.IsAvailable = True Then
            '    Return My.Computer.Network.Ping("8.8.8.8")
            'Else
            '    Return False
            'End If
            Dim Req As Net.HttpWebRequest = Net.HttpWebRequest.Create("http://www.siriussolution.com")
            Dim Resp As Net.HttpWebResponse = Req.GetResponse
            If (Resp.StatusCode = Net.HttpStatusCode.OK) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function SIRIUS_Con() As SqlClient.SqlConnection
        Try
            Dim cn As New SqlClient.SqlConnection("Password=Sa_2012_123*1023;Persist Security Info=True;User ID=SIRIUS_net_sa;Initial Catalog=112_softb;Data Source=SIRIUS.net")
            cn.Open()
            Return cn
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub frmTrialVersion_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.MaskedTextBox1.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub MaskedTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles MaskedTextBox1.KeyDown

        Try
            If e.KeyCode = Keys.Enter Then
                lblContinue_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub lblEnterKey_Click(sender As Object, e As EventArgs) Handles lblEnterKey.Click
        Try
            Me.MaskedTextBox1.Visible = True
            Me.MaskedTextBox1.Focus()
            Me.lblEnterKey.Visible = False
            Me.lblBuy.Visible = False

            'frmActiveLicense.LicenseKey = N
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblBuy_Click(sender As Object, e As EventArgs) Handles lblBuy.Click
        Try
            Me.Visible = False
            Me.Close()
            NavigateWebURL("http://www.siriussolution.com", "default")
            Environment.Exit(0)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub NavigateWebURL(ByVal URL As String, Optional browser As String = "default")

        If Not (browser = "default") Then
            Try
                '// try set browser if there was an error (browser not installed)
                Process.Start(browser, URL)
            Catch ex As Exception
                '// use default browser
                Process.Start(URL)
            End Try

        Else
            '// use default browser
            Process.Start(URL)

        End If

    End Sub
End Class