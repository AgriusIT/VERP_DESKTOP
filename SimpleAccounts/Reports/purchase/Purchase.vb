Public Class Purchase
    Private Sub UiButton10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton10.Click
        rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfPurchaseInvoices
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfPurchaseReturn
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub

    Private Sub UiButton2_Click(sender As Object, e As EventArgs) Handles UiButton2.Click
        frmMain.LoadControl("frmRptGrdPurchaseDetailWithWeight")
    End Sub

    Private Sub UiButton3_Click(sender As Object, e As EventArgs) Handles UiButton3.Click
        frmMain.LoadControl("ItemExpiryDate")
    End Sub

    Private Sub UiButton4_Click(sender As Object, e As EventArgs) Handles UiButton4.Click
        frmMain.LoadControl("frmGrdPurchaseSummary")
    End Sub

    Private Sub UiButton5_Click(sender As Object, e As EventArgs) Handles UiButton5.Click
        frmMain.LoadControl("rptPurchaseDailyWorkingReport")
    End Sub

    Private Sub UiButton6_Click(sender As Object, e As EventArgs) Handles UiButton6.Click
        frmMain.LoadControl("rptPurchaseItemSummary")
    End Sub

    Private Sub UiButton7_Click(sender As Object, e As EventArgs) Handles UiButton7.Click
        frmMain.LoadControl("frmRptMonthlyPurchaseSummary")
    End Sub

    Private Sub UiButton8_Click(sender As Object, e As EventArgs) Handles UiButton8.Click
        frmMain.LoadControl("rptAdvancePaymentsPO")
    End Sub

    Private Sub UiButton9_Click(sender As Object, e As EventArgs) Handles UiButton9.Click
        frmMain.LoadControl("rptImportLedger")
    End Sub

    Private Sub UiButton11_Click(sender As Object, e As EventArgs) Handles UiButton11.Click
        frmMain.LoadControl("SummaryofPurchasesAndReturns")
    End Sub

    Private Sub UiButton12_Click(sender As Object, e As EventArgs) Handles UiButton12.Click
        frmMain.LoadControl("frmGrdRptToOrderQty")
    End Sub

    Private Sub UiButton13_Click(sender As Object, e As EventArgs) Handles UiButton13.Click
        frmMain.LoadControl("frmRptAccountWisePurchaseReport")
    End Sub

    Private Sub UiButton16_Click(sender As Object, e As EventArgs) Handles UiButton16.Click
        frmMain.LoadControl("frmGrdRptTaxDuductionDetail")
    End Sub

    Private Sub UiButton15_Click(sender As Object, e As EventArgs) Handles UiButton15.Click
        frmMain.LoadControl("frmGrdRptAgingPayables")
    End Sub

    Private Sub UiButton14_Click(sender As Object, e As EventArgs)
        frmMain.LoadControl("frmGrdRptAgingPayables")
    End Sub
End Class