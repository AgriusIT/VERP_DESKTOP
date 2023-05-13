Public Class frmCustomizedReports
    Public reports As New List(Of String)
    Public IsClosed As Boolean = False

    Private Sub frmCustomizedReports_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            IsClosed = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmCustomizedReports_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then

                OK_Button_Click(Me, Nothing)

            End If

            If e.KeyCode = Keys.Escape Then

                Cancel_Button_Click(Me, Nothing)
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmCustomizedReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.KeyPreview = True
            If reports.Count > 0 Then
                Me.lbCustomizedReports.DataSource = reports
                Me.lbCustomizedReports.Update()
                Me.lbCustomizedReports.SelectedIndex = 0
                CustomizedReportName = Me.lbCustomizedReports.SelectedItem.ToString
                IsNoNewFolder = True
                If Me.lbCustomizedReports.Items.Count = 1 Then

                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                End If
            End If
            'Me.DialogResult = Windows.Forms.DialogResult.Cancel
            'IsCustomizedReportFormOpened = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lbCustomizedReports_Click(sender As Object, e As EventArgs) Handles lbCustomizedReports.Click
        Try
            CustomizedReportName = Me.lbCustomizedReports.SelectedItem.ToString
            IsNoNewFolder = True
            'If Not Me.DialogResult = Windows.Forms.DialogResult.Cancel Then
            'Me.Close()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class