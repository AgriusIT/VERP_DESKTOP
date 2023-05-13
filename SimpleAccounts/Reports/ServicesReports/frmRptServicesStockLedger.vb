Public Class frmRptServicesStockLedger

    Private Sub frmRptServicesStockLedger_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            Me.dtpDateFrom.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpDateTo.Value = Date.Now

            FillUltraDropDown(Me.cmbCustomer, "Select coa_detail_id, detail_title as [Customer], detail_code as [Code], Account_Type as [Type], Contact_Mobile as Mobile, Contact_Email as Email From vwCOADetail where detail_title <> ''")
            Me.cmbCustomer.Rows(0).Activate()
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            AddRptParam("@CustomerCode", Me.cmbCustomer.Value)
            AddRptParam("@FromDate", Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptServicesStockRegister")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class