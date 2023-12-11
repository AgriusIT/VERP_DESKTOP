Public Class frmRptCustomersSales
    Enum enmReportList
        RptCustomerSalesSummary
        RptCustomerItemSalesSummary
        RptCustomerITemSalesDetail
        RptCustomerTransactions
    End Enum
    Public ReportName As enmReportList
    Public Sub CallShowReport()
        Try
            If ReportName = enmReportList.RptCustomerSalesSummary Then
                AddRptParam("@CustomerID", Me.CmbAccounts.SelectedValue)
                AddRptParam("@FromDate", Me.dtpFromDate.Value.Date.ToString("dd/MMM/yyyy"))
                AddRptParam("@ToDate", Me.dtpToDate.Value.Date.ToString("dd/MMM/yyyy"))
                ShowReport("RptCustomerSalesSummary")
            ElseIf ReportName = enmReportList.RptCustomerItemSalesSummary Then
                AddRptParam("@CustomerID", Me.CmbAccounts.SelectedValue)
                AddRptParam("@FromDate", Me.dtpFromDate.Value.Date.ToString("dd/MMM/yyyy"))
                AddRptParam("@ToDate", Me.dtpToDate.Value.Date.ToString("dd/MMM/yyyy"))
                ShowReport("RptCustomerItemSalesSummary")
            ElseIf ReportName = enmReportList.RptCustomerITemSalesDetail Then
                AddRptParam("@CustomerID", Me.CmbAccounts.SelectedValue)
                AddRptParam("@FromDate", Me.dtpFromDate.Value.Date.ToString("dd/MMM/yyyy"))
                AddRptParam("@ToDate", Me.dtpToDate.Value.Date.ToString("dd/MMM/yyyy"))
                ShowReport("RptCustomerItemSalesDetail")
            ElseIf ReportName = enmReportList.RptCustomerTransactions Then
                AddRptParam("@CustomerID", Me.CmbAccounts.SelectedValue)
                AddRptParam("@FromDate", Me.dtpFromDate.Value.Date.ToString("dd/MMM/yyyy"))
                AddRptParam("@ToDate", Me.dtpToDate.Value.Date.ToString("dd/MMM/yyyy"))
                ShowReport("RtpCustomerTransactions")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmRptCustomersSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ReportName = enmReportList.RptCustomerSalesSummary Then
            Me.Text = "Sales Summary Report"
        ElseIf ReportName = enmReportList.RptCustomerItemSalesSummary Then
            Me.Text = "Item Wise Sales Summary"
        ElseIf ReportName = enmReportList.RptCustomerITemSalesDetail Then
            Me.Text = "Item Wise Sales Detail"
        ElseIf ReportName = enmReportList.RptCustomerTransactions Then
            Me.Text = "Transaction Report"
        End If
        FillCustomers()
    End Sub
    Private Sub BtnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGenerate.Click
        Try
            CallShowReport()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillCustomers()
        FillDropDown(Me.CmbAccounts, "Select Coa_detail_id, detail_title From vwCOADetail Where Account_Type='Customer'", True)
    End Sub
End Class