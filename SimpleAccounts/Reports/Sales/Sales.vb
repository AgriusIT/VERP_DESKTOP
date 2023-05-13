Public Class Sales
    Dim ControlName As Form

    Private Sub Sales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub
    Private Sub UiButton10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton10.Click
        rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfSalesInvoices
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        frmMain.LoadControl("rptCategoryWiseSaleReport")
    End Sub
    Private Sub UiButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmMain.LoadControl("ItemWiseSales")
    End Sub
    Private Sub UiButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton3.Click
        frmMain.LoadControl("SalesChart")
    End Sub
    'Private Sub UiButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton4.Click
    '    ShowReport("rptSales")
    'End Sub
    Private Sub UiButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton5.Click
        frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.DailySalesReport
        ApplyStyleSheet(frmRptEnhancementNew)
        frmRptEnhancementNew.ShowDialog()
    End Sub
    Private Sub UiButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            rptDateRange.ReportName = rptDateRange.ReportList.RptSalesTaxDetail
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            rptDateRange.ReportName = rptDateRange.ReportList.RptSalesTaxSummary
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub UiButton7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton7.Click

        'Try
        '    rptDateRange.ReportName = rptDateRange.ReportList.DailySupply
        '    ApplyStyleSheet(rptDateRange)
        '    rptDateRange.ShowDialog()
        'Catch ex As Exception

        'End Try
        frmMain.LoadControl("areawisesalereport")
    End Sub
    Private Sub UiButton6_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton6.Click
        Try
            frmMain.LoadControl("DailySupplyGatepass")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UiButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton8.Click
        frmMain.LoadControl("frmRptGrdMinMaxPriceSalesDetail")
    End Sub
    Private Sub UiButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton9.Click
        frmMain.LoadControl("rptGrdEachDaysWorking")
    End Sub

    Private Sub UiButton11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton11.Click
        frmMain.LoadControl("DailyWorkingReport")
    End Sub

    Private Sub UiButton50_Click(sender As Object, e As EventArgs) Handles UiButton50.Click
        frmMain.LoadControl("CustomerSalesContribution")
    End Sub

    Private Sub UiButton49_Click(sender As Object, e As EventArgs) Handles UiButton49.Click

        frmMain.LoadControl("frmGrdRptCustomerWiseCashRecovery")
    End Sub

    Private Sub UiButton46_Click(sender As Object, e As EventArgs) Handles UiButton46.Click
        frmMain.LoadControl("frmGrdRptSalesRegisterActivity")
    End Sub

    Private Sub UiButton51_Click(sender As Object, e As EventArgs) Handles UiButton51.Click
        frmMain.LoadControl("SalesmanDealerVoucher")
    End Sub

    Private Sub UiButton48_Click(sender As Object, e As EventArgs) Handles UiButton48.Click
        frmMain.LoadControl("rptSaleCertificateLedger")
    End Sub

    Private Sub UiButton47_Click(sender As Object, e As EventArgs) Handles UiButton47.Click
        frmMain.LoadControl("frmSaleInvoiceDueDate")
    End Sub

    Private Sub UiButton45_Click(sender As Object, e As EventArgs) Handles UiButton45.Click
        frmMain.LoadControl("frmGrdRptCostSheetQtyWise")
    End Sub

    Private Sub UiButton44_Click(sender As Object, e As EventArgs) Handles UiButton44.Click
        frmMain.LoadControl("rptAdvanceReceiptsSO")
    End Sub

    Private Sub UiButton43_Click(sender As Object, e As EventArgs) Handles UiButton43.Click
        frmMain.LoadControl("PriceCompare")
    End Sub

    Private Sub UiButton42_Click(sender As Object, e As EventArgs) Handles UiButton42.Click
        frmMain.LoadControl("frmGrdRptItemWiseSalesSummary")
    End Sub

    Private Sub UiButton41_Click(sender As Object, e As EventArgs) Handles UiButton41.Click
        frmMain.LoadControl("frmGrdRptCustomerItemWiseSummary")
    End Sub

    Private Sub UiButton40_Click(sender As Object, e As EventArgs) Handles UiButton40.Click
        frmMain.LoadControl("frmGrdRptSalesSummaries")
    End Sub

    Private Sub UiButton39_Click(sender As Object, e As EventArgs) Handles UiButton39.Click
        frmMain.LoadControl("frmGrdRptProductDateWiseReport")
    End Sub

    Private Sub UiButton38_Click(sender As Object, e As EventArgs) Handles UiButton38.Click
        frmMain.LoadControl("frmGrdRptProductCustomerWiseReport")
    End Sub

    Private Sub UiButton37_Click(sender As Object, e As EventArgs) Handles UiButton37.Click
        frmMain.LoadControl("frmGrdRptSalesCertificateIssued")
    End Sub

    Private Sub UiButton36_Click(sender As Object, e As EventArgs) Handles UiButton36.Click
        frmMain.LoadControl("ConsolidateItemSales")
    End Sub

    Private Sub UiButton35_Click(sender As Object, e As EventArgs) Handles UiButton35.Click
        frmMain.LoadControl("rptIssuedSalesCertificate")
    End Sub

    Private Sub UiButton34_Click(sender As Object, e As EventArgs) Handles UiButton34.Click
        frmMain.LoadControl("frmGrdRptSalesComparison")
    End Sub

    Private Sub UiButton33_Click(sender As Object, e As EventArgs) Handles UiButton33.Click
        frmMain.LoadControl("salescomparison")
    End Sub

    Private Sub UiButton32_Click(sender As Object, e As EventArgs) Handles UiButton32.Click
        frmMain.LoadControl("frmGrdSalesReturnDetail")
    End Sub

    Private Sub UiButton31_Click(sender As Object, e As EventArgs) Handles UiButton31.Click
        frmMain.LoadControl("frmGrdSalesSummary")
    End Sub

    Private Sub UiButton30_Click(sender As Object, e As EventArgs) Handles UiButton30.Click
        frmMain.LoadControl("rptNetSales")
    End Sub

    Private Sub UiButton29_Click(sender As Object, e As EventArgs) Handles UiButton29.Click
        frmMain.LoadControl("salesmancommission")
    End Sub

    Private Sub UiButton28_Click(sender As Object, e As EventArgs) Handles UiButton28.Click
        frmMain.LoadControl("rptDSRStatement")
    End Sub

    Private Sub UiButton27_Click(sender As Object, e As EventArgs) Handles UiButton27.Click
        frmMain.LoadControl("rptDSRSummary")
    End Sub

    Private Sub UiButton26_Click(sender As Object, e As EventArgs) Handles UiButton26.Click
        frmMain.LoadControl("rptDeliveryChalanSummary")
    End Sub

    Private Sub UiButton25_Click(sender As Object, e As EventArgs) Handles UiButton25.Click
        frmMain.LoadControl("rptDeliveryChalanDetail")
    End Sub

    Private Sub UiButton24_Click(sender As Object, e As EventArgs) Handles UiButton24.Click
        frmMain.LoadControl("grdSaleManDemandDetail")
    End Sub

    Private Sub UiButton23_Click(sender As Object, e As EventArgs) Handles UiButton23.Click
        frmMain.LoadControl("grdDispatchDetail")
    End Sub

    Private Sub UiButton22_Click(sender As Object, e As EventArgs) Handles UiButton22.Click
        frmMain.LoadControl("frmEmployeeWiseMonthlySale")
    End Sub

    Private Sub UiButton21_Click(sender As Object, e As EventArgs) Handles UiButton21.Click
        frmMain.LoadControl("SalesDtbyCategory")
    End Sub

    Private Sub UiButton20_Click(sender As Object, e As EventArgs) Handles UiButton20.Click
        frmMain.LoadControl("SalesDetail")
    End Sub

    Private Sub UiButton19_Click(sender As Object, e As EventArgs) Handles UiButton19.Click
        frmMain.LoadControl("CustItemsSummarySales")
    End Sub

    Private Sub UiButton18_Click(sender As Object, e As EventArgs) Handles UiButton18.Click
        frmMain.LoadControl("CustSumSalesChart")
    End Sub

    Private Sub UiButton17_Click(sender As Object, e As EventArgs) Handles UiButton17.Click
        frmMain.LoadControl("CustomerSalesHistory")
    End Sub

    Private Sub UiButton16_Click(sender As Object, e As EventArgs) Handles UiButton16.Click
        frmMain.LoadControl("frmRptGrdMinMaxPriceSalesDetail")
    End Sub

    Private Sub UiButton15_Click(sender As Object, e As EventArgs) Handles UiButton15.Click
        frmMain.LoadControl("WeightReport")
    End Sub

    Private Sub UiButton14_Click(sender As Object, e As EventArgs) Handles UiButton14.Click
        frmMain.LoadControl("DemandDt")
    End Sub

    Private Sub UiButton13_Click(sender As Object, e As EventArgs) Handles UiButton13.Click
        frmMain.LoadControl("DemandSales")
    End Sub

    Private Sub UiButton12_Click(sender As Object, e As EventArgs) Handles UiButton12.Click
        frmMain.LoadControl("DailySupplyGatepass")
    End Sub

    Private Sub UiButton58_Click(sender As Object, e As EventArgs) Handles UiButton58.Click
        frmMain.LoadControl("rptSummaryOfSalesTaxInvoices")
    End Sub

    Private Sub UiButton2_Click_1(sender As Object, e As EventArgs) Handles UiButton2.Click
        frmMain.LoadControl("rptSumOfInvReturn")
    End Sub

    Private Sub UiButton57_Click(sender As Object, e As EventArgs) Handles UiButton57.Click
        frmMain.LoadControl("rptCategoryWiseSaleReport")
    End Sub

    Private Sub UiButton4_Click(sender As Object, e As EventArgs) Handles UiButton4.Click
        frmMain.LoadControl("ItemWiseSales")

    End Sub

    Private Sub UiButton56_Click(sender As Object, e As EventArgs) Handles UiButton56.Click
        frmMain.LoadControl("ItemWiseSalesReturn")

    End Sub

    Private Sub UiButton55_Click(sender As Object, e As EventArgs) Handles UiButton55.Click
        frmMain.LoadControl("rptSMTarget")
    End Sub

    Private Sub UiButton54_Click(sender As Object, e As EventArgs) Handles UiButton54.Click
        frmMain.LoadControl("rptgrdInvForm")
    End Sub

    Private Sub UiButton53_Click(sender As Object, e As EventArgs) Handles UiButton53.Click
        frmMain.LoadControl("SalesChart")
    End Sub

    Private Sub UiButton52_Click(sender As Object, e As EventArgs) Handles UiButton52.Click
        frmMain.LoadControl("SalesmanMonthlySales")
    End Sub

    Private Sub UiButton59_Click(sender As Object, e As EventArgs) Handles UiButton59.Click
        frmMain.LoadControl("rptSumOfInv")
    End Sub
    'Ali Faisal : TFS1370 : 24-Aug-2017 : Add links to open from sales reports
    Private Sub UiButton60_Click(sender As Object, e As EventArgs) Handles UiButton60.Click
        frmMain.LoadControl("frmGrdRptEngineWiseStock")
    End Sub

    Private Sub UiButton61_Click(sender As Object, e As EventArgs) Handles UiButton61.Click
        frmMain.LoadControl("SummaryofSalesAndReturns")
    End Sub
    'Ali Faisal : TFS1370 : 24-Aug-2017 : End

    Private Sub UiButton62_Click(sender As Object, e As EventArgs) Handles UiButton62.Click
        frmMain.LoadControl("frmInvoiceWiseProfitReport")
    End Sub

    Private Sub UiButton67_Click(sender As Object, e As EventArgs) Handles UiButton67.Click
        frmMain.LoadControl("frmGrdRptInstallmentBalance")
    End Sub

    Private Sub UiButton63_Click(sender As Object, e As EventArgs) Handles UiButton63.Click
        frmMain.LoadControl("SectorWiseSales")
    End Sub

    Private Sub UiButton64_Click(sender As Object, e As EventArgs) Handles UiButton64.Click
        frmMain.LoadControl("frmGrdSalesReturnDetail")
    End Sub

    Private Sub UiButton65_Click(sender As Object, e As EventArgs) Handles UiButton65.Click
        frmMain.LoadControl("frmGrdRptCostSheetMarginCalculationDetail")
    End Sub

    Private Sub UiButton66_Click(sender As Object, e As EventArgs) Handles UiButton66.Click
        frmMain.LoadControl("frmFrequentlySalesItem")
    End Sub

    Private Sub UiButton68_Click(sender As Object, e As EventArgs) Handles UiButton68.Click
        frmMain.LoadControl("frmGrdRptEmployeeTargetAchieved")
    End Sub

    Private Sub UiButton69_Click(sender As Object, e As EventArgs) Handles UiButton69.Click
        frmMain.LoadControl("frmSalesInquiryStatus")
    End Sub

    Private Sub UiButton70_Click(sender As Object, e As EventArgs) Handles UiButton70.Click
        frmMain.LoadControl("frmSOArticleAliasPendingMapping")
    End Sub

    Private Sub UiButton71_Click(sender As Object, e As EventArgs) Handles UiButton71.Click
        frmMain.LoadControl("frmGrdRptSaleOrderStatusSummary")
    End Sub
End Class
