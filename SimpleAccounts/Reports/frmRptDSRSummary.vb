Public Class frmRptDSRSummary
    Private Sub frmRptDSRSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.DateTimePicker1.Value = Date.Now
            FillDropDown(Me.cmbSaleman, "Select Employee_Id, Employee_Name From tblDefEmployee WHERE isnull(SalePerson,0)=1")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Me.cmbSaleman.SelectedIndex = 0 Then
                ShowErrorMessage("Please select Employee")
                Me.cmbSaleman.Focus()
                Exit Sub
            End If
            AddRptParam("@DocDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@RootPlanID", Me.cmbSaleman.SelectedValue)
            ShowReport("rptDSRSummary")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If Me.cmbSaleman.SelectedIndex = 0 Then
                ShowErrorMessage("Please select Employee")
                Me.cmbSaleman.Focus()
                Exit Sub
            End If
            AddRptParam("@DocDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@RootPlanID", Me.cmbSaleman.SelectedValue)
            ShowReport("rptDSRSummary", , "Nothing", "Nothing", True)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class