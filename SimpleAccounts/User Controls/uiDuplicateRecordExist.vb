Public Class uiDuplicateRecordExist
    Dim dt As New DataTable
    Public Sub getDuplicateRecords()
        Try
            If Me.BackgroundWorker1.IsBusy Then Exit Sub
            Me.BackgroundWorker1.RunWorkerAsync()
            Do While Me.BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub lnkExpectedDuplicateRecords_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkExpectedDuplicateRecords.LinkClicked
        Try
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    frmGetDuplicateRecords.ShowDialog()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            dt = GetDataTable("SP_DuplicateRecords")
            dt.AcceptChanges()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Me.lblExpectedRecords.Text = dt.Rows.Count
                Else
                    Me.lblExpectedRecords.Text = 0
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
