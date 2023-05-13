Public Class frmSalesTarget
    Public id As Integer
    Public ename As String
    Private Sub frmSalesTarget_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtYear.Text = Date.Now.Year.ToString
        Me.Text = ename
    End Sub

End Class