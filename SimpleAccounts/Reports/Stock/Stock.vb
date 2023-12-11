Public Class Stock
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHeader.Click

    End Sub
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        frmMain.LoadControl("grdrptStock")
    End Sub
    Private Sub UiButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton2.Click
        frmMain.LoadControl("rptgrdInvForm")
    End Sub
    Private Sub UiButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton3.Click
        frmMain.LoadControl("rtpInventoryLevel")
    End Sub
    Private Sub UiButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton4.Click
        frmMain.LoadControl("PriceChangeReport")
    End Sub
    Private Sub UiButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton5.Click
        frmMain.LoadControl("frmRptStockStatment")
    End Sub
    Private Sub UiButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton6.Click
        frmMain.LoadControl("frmRptGrdStockStatement")
    End Sub
    Private Sub UiButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton7.Click
        frmMain.LoadControl("frmStockwithCriteria")
    End Sub
    Private Sub UiButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton8.Click
        frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.StockStatementByLPO
        ApplyStyleSheet(frmRptEnhancementNew)
        frmRptEnhancementNew.ShowDialog()
    End Sub

    Private Sub UiButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton9.Click
        frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.StockStatementByType
        ApplyStyleSheet(frmRptEnhancementNew)
        frmRptEnhancementNew.ShowDialog()
    End Sub
    Private Sub UiButton10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton10.Click
        frmMain.LoadControl("frmStockStatmentBySize")
    End Sub

    Private Sub UiButton11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton11.Click
        frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.StockInventoryReport
        ApplyStyleSheet(frmRptEnhancementNew)
        frmRptEnhancementNew.ShowDialog()
    End Sub
    Private Sub UiButton12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton12.Click
        frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.StockInventoryValue
        ApplyStyleSheet(frmRptEnhancementNew)
        frmRptEnhancementNew.ShowDialog()
    End Sub
    Private Sub UiButton13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton13.Click
        frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.DispatchSummary
        ApplyStyleSheet(frmRptEnhancementNew)
        frmRptEnhancementNew.ShowDialog()
    End Sub
    Private Sub UiButton14_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton14.Click
        frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.DispatchDetail
        ApplyStyleSheet(frmRptEnhancementNew)
        frmRptEnhancementNew.ShowDialog()
    End Sub
    Private Sub UiButton15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton15.Click
        frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.RptStoreIssuanceSummary
        ApplyStyleSheet(frmRptEnhancementNew)
        frmRptEnhancementNew.ShowDialog()
    End Sub
    Private Sub UiButton16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton16.Click
        frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.RptStoreIssuanceDetail
        ApplyStyleSheet(frmRptEnhancementNew)
        frmRptEnhancementNew.ShowDialog()
    End Sub
    Private Sub UiButton17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton17.Click
        ShowReport("ListOfItems")
    End Sub
    Private Sub UiButton18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton18.Click
        Try
            frmMain.LoadControl("frmRptGrdStockStatement")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UiButton19_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton19.Click
        Try
            rptDateRange.ReportName = rptDateRange.ReportList.ItemsDetailReport
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UiButton20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton20.Click
        Try
            frmMain.LoadControl("ItemWiseSalesHistory")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton21.Click
        Try
            frmMain.LoadControl("frmrptGrdProducedItems")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton22.Click
        Try
            rptDateRange.ReportName = rptDateRange.ReportList.ReceivingReport
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton23_Click(sender As Object, e As EventArgs) Handles UiButton23.Click
        Try
            frmMain.LoadControl("frmRptGrdStockInOutDetail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton24_Click(sender As Object, e As EventArgs) Handles UiButton24.Click
        Try
            frmMain.LoadControl("ProjectWiseStockLedger")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton25_Click(sender As Object, e As EventArgs) Handles UiButton25.Click
        Try
            frmMain.LoadControl("frmGrdRptStockStatementUnitWise")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton26_Click(sender As Object, e As EventArgs) Handles UiButton26.Click
        Try
            frmMain.LoadControl("frmGrdStockDeliveryChalan")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton27_Click(sender As Object, e As EventArgs) Handles UiButton27.Click
        Try
            frmMain.LoadControl("rptArticleBarcode")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton28_Click(sender As Object, e As EventArgs) Handles UiButton28.Click
        Try
            frmMain.LoadControl("frmGrdCostSheetComparisonWithStock")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton29_Click(sender As Object, e As EventArgs) Handles UiButton29.Click
        Try
            frmMain.LoadControl("frmGrdPlanComparison")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton30_Click(sender As Object, e As EventArgs) Handles UiButton30.Click
        Try
            frmMain.LoadControl("frmGrdArticleLedger")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton31_Click(sender As Object, e As EventArgs) Handles UiButton31.Click
        Try
            frmMain.LoadControl("rptStorageBilling")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton32_Click(sender As Object, e As EventArgs) Handles UiButton32.Click
        Try
            frmMain.LoadControl("rptProjectWiseLedger")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton33_Click(sender As Object, e As EventArgs) Handles UiButton33.Click
        Try
            frmMain.LoadControl("frmGrdRptLocationWiseStockStatement")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton34_Click(sender As Object, e As EventArgs) Handles UiButton34.Click
        Try
            frmMain.LoadControl("frmGrdRptLocationWiseStockStatementNew")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton35_Click(sender As Object, e As EventArgs) Handles UiButton35.Click
        Try
            frmMain.LoadControl("rptLocationWiseClosingStock")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton36_Click(sender As Object, e As EventArgs) Handles UiButton36.Click
        Try
            frmMain.LoadControl("rptStoreIssuanceDetailBatchWise")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton37_Click(sender As Object, e As EventArgs) Handles UiButton37.Click
        Try
            frmMain.LoadControl("WarrantyDetailReport")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton38_Click(sender As Object, e As EventArgs) Handles UiButton38.Click
        Try
            frmMain.LoadControl("rptDispatchStatus")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton39_Click(sender As Object, e As EventArgs) Handles UiButton39.Click
        Try
            frmMain.LoadControl("frmGrdRptMinimumStockLevel")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton40_Click(sender As Object, e As EventArgs) Handles UiButton40.Click
        Try
            frmMain.LoadControl("ClosingStockByOrder")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub UiButton41_Click(sender As Object, e As EventArgs) Handles UiButton41.Click
        Try
            frmMain.LoadControl("frmTransferredStockReport")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton42_Click(sender As Object, e As EventArgs) Handles UiButton42.Click
        Try
            frmMain.LoadControl("frmLocationWiseStockReport")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton43_Click(sender As Object, e As EventArgs) Handles UiButton43.Click
        Try
            frmMain.LoadControl("frmDefGroupVoucherApproval")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton44_Click(sender As Object, e As EventArgs) Handles UiButton44.Click
        Try
            frmMain.LoadControl("frmGrdRptCostSheetPlanDetail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton45_Click(sender As Object, e As EventArgs) Handles UiButton45.Click
        Try
            frmMain.LoadControl("frmGrdArticleLedgerByPack")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton46_Click(sender As Object, e As EventArgs) Handles UiButton46.Click
        Try
            frmMain.LoadControl("StockStatementbyPack")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton47_Click(sender As Object, e As EventArgs) Handles UiButton47.Click
        Try
            frmMain.LoadControl("RackWiseClosingStock")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton48_Click(sender As Object, e As EventArgs) Handles UiButton48.Click
        Try
            frmMain.LoadControl("frmInventoryColumnStrings")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Changes added by Murtaza (12/30/2022)
    Private Sub UiButton49_Click(sender As Object, e As EventArgs) Handles UiButton49.Click
        Try
            frmMain.LoadControl("frmGrdStockMovement")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Changes added by Murtaza (12/30/2022)
End Class