Imports System.Type
Public Class Vendors
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        ShowReport("rptVendors_List")
    End Sub

    Private Sub UiButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton2.Click
        'AddRptParam("@1stAging", 60)
        'AddRptParam("@1stAgingName", "30_60")
        'AddRptParam("@1stAging", 90)
        'AddRptParam("@1stAgingName", "60_90")
        'AddRptParam("@1stAging", 90)
        'AddRptParam("@1stAgingName", "90+")
        Me.Cursor = Cursors.WaitCursor
        AddRptParam("@Aging", 0)
        AddRptParam("@1stAging", 60)
        AddRptParam("@1stAgingName", "30_60")
        AddRptParam("@2ndAging", 90)
        AddRptParam("@2ndAgingName", "60_90")
        AddRptParam("@3rdAging", 90)
        AddRptParam("@3rdAgingName", "90+")
        AddRptParam("@IncludeUnPosted", True)
        ShowReport("AgeingPayable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)
    End Sub
    Private Sub UiButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton3.Click
        rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfPurchaseInvoices
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub
    Private Sub UiButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton4.Click
        rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfPurchaseReturn
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub

    Private Sub UiButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton5.Click
        Try


            frmMain.LoadControl("frmRptGrdPurchaseDetailWithWeight")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton6_Click(sender As Object, e As EventArgs)

    End Sub
End Class