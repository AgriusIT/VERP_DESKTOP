Public Class frmAdvanceTypeList

    Private Sub frmAdvanceTypeList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnExport.FlatAppearance.BorderSize = 0
    End Sub

    Private Sub btnExport_MouseHover(sender As Object, e As EventArgs) Handles btnExport.MouseHover
        ContextMenuStrip1.Show(btnExport, 0, btnExport.Height)
    End Sub
End Class