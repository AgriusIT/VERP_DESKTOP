Public Class frmRptProjectBasedTransactionDetail

    Private Sub frmRptProjectBasedTransactionDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.dtpFromDate.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpToDate.Value = Date.Now

            FillDropDown(Me.cmbProject, "Select CostCenterId, Name From tblDefCostCenter ORDER BY Name ASC")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            GetCrystalReportRights()
            If Me.cmbProject.SelectedIndex = -1 Then Exit Sub
            If Me.cmbProject.SelectedIndex = 0 Then
                ShowErrorMessage("Please select a project.")
                Me.cmbProject.Focus()
                Exit Sub
            End If
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("@CostCenterId", Me.cmbProject.SelectedValue)
            ShowReport("rptProjectBasedTransactionDetail")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class