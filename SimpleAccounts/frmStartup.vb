Public Class frmStartup

    Private Sub frmStartup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Database1DataSet.ChartTAble' table. You can move, or remove it, as needed.
        'Me.UltraChart1.Data.DataSource = Me.UltraDataSource1
        'Me.UltraChart1.DataBind()
        If IsConnectionAvailable() = True Then '(http://www.blogs.SIRIUS.net/)
            Me.WebBrowser1.Visible = True

        Else
            Me.WebBrowser1.Visible = False
        End If
    End Sub

    Private Sub ChartTAbleBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChartTAbleBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.ChartTAbleBindingSource.EndEdit()
       
    End Sub
End Class