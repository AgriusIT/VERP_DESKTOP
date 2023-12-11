Public Class frmRptServicesReports

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            If Me.cmbCustomer.IsItemInList = False Then Exit Sub
            If Me.cmbCustomer.ActiveRow Is Nothing Then Exit Sub
            Dim strReportName As String = String.Empty
            If Me.rbtIGP.Checked = True Then
                strReportName = "rptIGPDetail"
            ElseIf Me.rbtWIP.Checked = True Then
                strReportName = "rptWIPDetail"
            ElseIf Me.rbtProduction.Checked = True Then
                strReportName = "rptServicesProductionDetail"
            ElseIf Me.rbtSalesInvoice.Checked = True Then
                strReportName = "rptServicesInvoiceDetail"
            ElseIf Me.rbtDispatch.Checked = True Then
                strReportName = "rptServicesDispatchDetail"
            End If
            AddRptParam("@CustomerCode", Me.cmbCustomer.Value)
            AddRptParam("@FromDate", Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport(strReportName.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmRptServicesReports_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
End Class