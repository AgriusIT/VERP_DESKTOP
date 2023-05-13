Public Class frmSalesOrderList
    Dim flgCompanyRights As Boolean = False
    Public ReceivingID As Integer = 0

    Private Sub frmSalesOrderList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            btnLoad_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmSalesOrderList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If


            Dim Str As String = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo from SalesOrderMasterTable where salesorderId not in(select PoId from salesMasterTable) " & IIf(flgCompanyRights = True, " AND LocationId=" & MyCompanyId & "", "") & " ORDER BY 1 ASC"
            FillDropDown(Me.cmbSalesOrder, Str)
            Me.cmbSalesOrder.Focus()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Me.cmbSalesOrder.SelectedIndex = -1 Then Exit Sub
            ReceivingID = Me.cmbSalesOrder.SelectedValue
            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
End Class