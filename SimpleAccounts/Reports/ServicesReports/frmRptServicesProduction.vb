Public Class frmRptServicesProduction

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try

            If Me.cmbCustomer.Value <= 0 Then
                ShowErrorMessage("Please select account.")
                Me.cmbCustomer.Focus()
                Exit Sub
            End If

            AddRptParam("@CustomerCode", Me.cmbCustomer.Value)
            AddRptParam("@ProductionDate", Me.dtpDateTo.Value.ToString("yyyy-M-d 00:00:00"))

            Dim strCriteria As String = String.Empty
            strCriteria = IIf(Me.txtPlanNo.Text.Length > 0, "{SP_ServicesProduction;1.Job_No}='" & Me.txtPlanNo.Text.Replace("'", "''") & "'", "Nothing")
            ShowReport("rptProductionReport", strCriteria)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmRptServicesProduction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.dtpDateTo.Value = Date.Now
            FillUltraDropDown(Me.cmbCustomer, "Select coa_detail_id, detail_title as [Customer], detail_code as [Code], Account_Type as [Type], Contact_Mobile as Mobile, Contact_Email as Email From vwCOADetail where detail_title <> '' and Account_Type in ('Customer','Vendor')")
            Me.cmbCustomer.Rows(0).Activate()
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class