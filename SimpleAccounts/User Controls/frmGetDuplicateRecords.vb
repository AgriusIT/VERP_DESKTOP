Public Class frmGetDuplicateRecords

    Public _dt As New DataTable
    Private Sub frmGetDuplicateRecords_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.grd.DataSource = _dt
            Me.grd.RetrieveStructure()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class