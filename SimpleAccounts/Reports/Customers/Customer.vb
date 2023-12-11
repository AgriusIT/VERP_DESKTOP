Public Class Customer
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        ShowReport("rptCustomers_List")
    End Sub
    Private Sub UiButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton5.Click
        frmMain.LoadControl("DailyWorkingReport")
    End Sub
    Private Sub UiButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton6.Click
        rptDateRange.ReportName = rptDateRange.ReportList.rptDiscountNetRate
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub
    Private Sub UiButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton2.Click
        Dim frm As New rptVoucher
        ApplyStyleSheet(frm)
        frm.ShowDialog()
    End Sub
    Private Sub UiButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton3.Click
        ShowReport("AgeingReceivable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)
    End Sub
    Private Sub UiButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton4.Click
        rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfSalesInvoices
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub
    Private Sub UiButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton8.Click
        frmMain.LoadControl("RptGrdTopCustomers")
    End Sub
    Private Sub UiButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'ApplyStyleSheet(frmRptCustomerSalesAnlysis)
        'frmRptCustomerSalesAnlysis.ShowDialog()
        frmMain.LoadControl("frmRptCustomerSalesAnlysis")
    End Sub
    Private Sub UiButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton9.Click
        frmRptCustomersSales.ReportName = frmRptCustomersSales.enmReportList.RptCustomerItemSalesSummary
        ApplyStyleSheet(frmRptCustomersSales)
        frmRptCustomersSales.ShowDialog()
    End Sub
    'Private Sub UiButton11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton11.Click
    '    ApplyStyleSheet(RptCustomerSalesAnlysis)
    '    frmMain.LoadControl("RptCustomerSalesAnlysis")
    'End Sub
    Private Sub UiButton10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton10.Click
        frmRptCustomersSales.ReportName = frmRptCustomersSales.enmReportList.RptCustomerITemSalesDetail
        ApplyStyleSheet(frmRptCustomersSales)
        frmRptCustomersSales.ShowDialog()
    End Sub
    Private Sub UiButton7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton7.Click
        ApplyStyleSheet(RptGridItemSalesHistory)
        frmMain.LoadControl("RptGridItemSalesHistory")
    End Sub
    Private Sub UiButton12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton12.Click
        Try
            frmMain.LoadControl("SalesmanMonthlySales")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton11_Click(sender As Object, e As EventArgs) Handles UiButton11.Click

    End Sub
End Class