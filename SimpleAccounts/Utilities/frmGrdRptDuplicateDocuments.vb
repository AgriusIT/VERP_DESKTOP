Public Class frmGrdRptDuplicateDocuments
    Public Sub FillGrid()
        Try
            Dim dt As New DataTable
            dt = GetDataTable("SP_DuplicateDocuments")
            dt.AcceptChanges()

            Me.grd.DataSource = dt
            'Me.grd.RetrieveStructure()
            'Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmGrdRptDuplicateDocuments_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class