Public Class Accounts
    Dim ControlName As New Form
    Dim enm As EnumForms = EnumForms.Non

    Private Sub UiButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton6.Click
        'ApplyStyleSheet(rptLedger)
        'rptLedger.ShowDialog()
        frmModProperty.ShowForm("rptLedger")
    End Sub
    Private Sub UiButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton2.Click
        rptDateRange.ReportName = rptDateRange.ReportList.CashFlowStatment
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub
    Private Sub UiButton12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton12.Click
        rptPLComparison.ReportName = rptPLComparison.ReportList.PLComparison
        ApplyStyleSheet(rptPLComparison)
        rptPLComparison.ShowDialog()
    End Sub
    Private Sub UiButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton4.Click
        'ShowReport("AgeingPayable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)
        ApplyStyleSheet(frmGrdRptAgingPayables)
        frmModProperty.ShowForm("frmGrdRptAgingPayables")
    End Sub
    Private Sub UiButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton5.Click
        'ShowReport("AgeingReceivable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)
        ApplyStyleSheet(frmGrdRptAgingReceiveables)
        frmModProperty.ShowForm("frmGrdRptAgingReceiveables")
    End Sub
    Private Sub UiButton14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton14.Click
        rptDateRange.ReportName = rptDateRange.ReportList.rptExpenses
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub
    Private Sub UiButton11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton11.Click
        'rptDateRange.ReportName = rptDateRange.ReportList.PLSingleDate
        'ApplyStyleSheet(rptDateRange)
        'rptDateRange.ShowDialog()
        ApplyStyleSheet(frmProfitAndLoss)
        frmModProperty.ShowForm("frmProfitAndLoss")
    End Sub
    Private Sub UiButton13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton13.Click
        'rptDateRange.ReportName = rptDateRange.ReportList.PLNotesDetail
        'ApplyStyleSheet(rptDateRange)
        'rptDateRange.ShowDialog()
        ApplyStyleSheet(frmProfitAndLoss)
        frmModProperty.ShowForm("frmProfitAndLoss")
    End Sub
    Private Sub UiButton10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton10.Click
        ShowReport("rptProftAndLossStatement")
    End Sub
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        ShowReport("rptChartofAccounts")
    End Sub
    Private Sub UiButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton3.Click
        rptDateRange.ReportName = rptDateRange.ReportList.CashFlowStatmentStander
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub
    'Private Sub UiButton15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    rptDateRange.ReportName = rptDateRange.ReportList.ReceivingReport
    '    ApplyStyleSheet(rptDateRange)
    '    rptDateRange.ShowDialog()
    'End Sub
    Private Sub UiButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton7.Click
        frmModProperty.ShowForm("rptTrial")
    End Sub
    Private Sub UiButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton8.Click
        'rptDateRange.ReportName = rptDateRange.ReportList.rptBSFomated
        'ApplyStyleSheet(rptDateRange)
        'rptDateRange.ShowDialog()
        ApplyStyleSheet(frmBalanceSheet)
        frmModProperty.ShowForm("frmBalanceSheet")
    End Sub

    Private Sub UiButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton9.Click
        frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.CashAccounting
        ApplyStyleSheet(frmRptEnhancementNew)
        frmRptEnhancementNew.ShowDialog()
    End Sub

    Private Sub UiButton16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.ProfitAndLossNotes
        ApplyStyleSheet(frmRptEnhancementNew)
        frmRptEnhancementNew.ShowDialog()
    End Sub

    Private Sub UiButton17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton17.Click
        frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.BalanceSheetNotes
        ApplyStyleSheet(frmRptEnhancementNew)
        frmRptEnhancementNew.ShowDialog()
    End Sub

    Private Sub UiButton18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton18.Click
        frmModProperty.ShowForm("frmRptGrdPLCostCenter")
    End Sub
    Private Sub BtnLegendLedger_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLegendLedger.Click
        Try
            ApplyStyleSheet(frmRptLedgerNew)
            frmModProperty.ShowForm("frmRptLedgerNew")
            'ApplyStyleSheet(frmRptLedgerNew)
            'frmRptLedgerNew.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UiButton19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton19.Click
        Try
            ApplyStyleSheet(frmRptPostDatedCheques)
            frmRptPostDatedCheques.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UiButton20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton20.Click
        Try
            frmRptAgingReport.ReportName = frmRptAgingReport.ReportList.Receiveables
            ApplyStyleSheet(frmRptAgingReport)
            frmRptAgingReport.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UiButton21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            frmRptAgingReport.ReportName = frmRptAgingReport.ReportList.Payables
            ApplyStyleSheet(frmRptAgingReport)
            frmRptAgingReport.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UiButton22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton22.Click
        Try
            'ApplyStyleSheet(frmRptTrialNew)
            'frmRptTrialNew.ShowDialog()
            frmModProperty.ShowForm("frmRptTrialNew")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UiButton23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton23.Click
        Try
            rptDateRange.ReportName = rptDateRange.ReportList.BalanceSheetNotesSummary
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UiButton24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton24.Click
        Try
            frmModProperty.ShowForm("frmRptGrdAdvances")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UiButton15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton15.Click
        Try
            ApplyStyleSheet(frmBalanceSheet)
            frmModProperty.ShowForm("frmBalanceSheet")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton16_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton16.Click
        Try
            ApplyStyleSheet(frmProfitAndLoss)
            frmModProperty.ShowForm("frmProfitAndLoss")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton25.Click
        Try
            rptDateRange.ReportName = rptDateRange.ReportList.LedgerByInvoices
            ApplyStyleSheet(rptDateRange)
            rptDateRange.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton27_Click(sender As Object, e As EventArgs) Handles UiButton27.Click
        Try
            frmModProperty.ShowForm("EmpSalaryRpt")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton28_Click(sender As Object, e As EventArgs) Handles UiButton28.Click
        Try
            frmModProperty.ShowForm("rptCashFlowStander")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton29_Click(sender As Object, e As EventArgs) Handles UiButton29.Click
        Try
            frmModProperty.ShowForm("frmCashRecoveryReport")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton30_Click(sender As Object, e As EventArgs) Handles UiButton30.Click
        Try
            frmModProperty.ShowForm("rptCashReceiptsDetailAgainstEmployee")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton26_Click(sender As Object, e As EventArgs) Handles UiButton26.Click
        Try
            frmModProperty.ShowForm("frmRptInvoiceAgingFormated")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton21_Click_1(sender As Object, e As EventArgs) Handles UiButton21.Click
        Try
            frmModProperty.ShowForm("frmGrdRptInvoiceAging")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton31_Click(sender As Object, e As EventArgs) Handles UiButton31.Click
        Try
            frmModProperty.ShowForm("FrmVoucherCheckList")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton32_Click(sender As Object, e As EventArgs) Handles UiButton32.Click
        Try
            frmModProperty.ShowForm("frmRptDirectorDebitors")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton33_Click(sender As Object, e As EventArgs) Handles UiButton33.Click
        Try
            frmModProperty.ShowForm("PostDateChequeSumm")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton34_Click(sender As Object, e As EventArgs) Handles UiButton34.Click
        Try
            frmModProperty.ShowForm("rptLCDetail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton35_Click(sender As Object, e As EventArgs) Handles UiButton35.Click
        Try
            frmModProperty.ShowForm("WithHoldingTaxCertificate")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton36_Click(sender As Object, e As EventArgs) Handles UiButton36.Click
        Try
            frmModProperty.ShowForm("CustomerChequesDueAll")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton37_Click(sender As Object, e As EventArgs) Handles UiButton37.Click
        Try
            frmModProperty.ShowForm("frmRptGrdPLCostCenter")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton38_Click(sender As Object, e As EventArgs) Handles UiButton38.Click
        Try
            frmModProperty.ShowForm("rptPLNoteSubSubAccountSummary")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton39_Click(sender As Object, e As EventArgs) Handles UiButton39.Click
        Try
            frmModProperty.ShowForm("rptPLNoteDetailAccountSummary")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton40_Click(sender As Object, e As EventArgs) Handles UiButton40.Click
        frmModProperty.ShowForm("frmRptDailyWorkingReport")
    End Sub

    Private Sub UiButton41_Click(sender As Object, e As EventArgs) Handles UiButton41.Click
        frmModProperty.ShowForm("frmBankTypeWiseCashFlow")
    End Sub

    Private Sub UiButton42_Click(sender As Object, e As EventArgs) Handles UiButton42.Click
        frmModProperty.ShowForm("frmAgingPayablesNew")
    End Sub

    Private Sub UiButton43_Click(sender As Object, e As EventArgs) Handles UiButton43.Click
        frmModProperty.ShowForm("frmPLSubSubAccountWiseSummary")
    End Sub

    Private Sub UiButton44_Click(sender As Object, e As EventArgs) Handles UiButton44.Click
        frmModProperty.ShowForm("frmPLSubSubAccountCostCenterWiseSummary")
    End Sub

    Private Sub UiButton45_Click(sender As Object, e As EventArgs) Handles UiButton45.Click
        frmModProperty.ShowForm("frmBSSubSubAccountSummary")
    End Sub

    Private Sub UiButton46_Click(sender As Object, e As EventArgs) Handles UiButton46.Click
        frmModProperty.ShowForm("frmPLAccountGroupWiseSummary")
    End Sub

    Private Sub UiButton47_Click(sender As Object, e As EventArgs) Handles UiButton47.Click
        frmModProperty.ShowForm("frmBSAccountGroupWiseSummary")
    End Sub

    Private Sub UiButton48_Click(sender As Object, e As EventArgs) Handles UiButton48.Click
        frmModProperty.ShowForm("frmAcctualVsBudgetedPLReport")
    End Sub

    Private Sub UiButton49_Click(sender As Object, e As EventArgs) Handles UiButton49.Click
        frmModProperty.ShowForm("frmAcctualVsBudgetedCategoryWisePL")
    End Sub

    Private Sub UiButton50_Click(sender As Object, e As EventArgs) Handles UiButton50.Click
        frmModProperty.ShowForm("frmInvoiceAgingNew")
    End Sub

    Private Sub UiButton51_Click(sender As Object, e As EventArgs) Handles UiButton51.Click
        frmModProperty.ShowForm("frmAssetsAndLiabilityReport")
    End Sub
End Class