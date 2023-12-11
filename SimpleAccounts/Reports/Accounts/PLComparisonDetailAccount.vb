Imports System.Windows.Forms

Public Class PLComparisonDetailAccount
    Public ReportName As String = String.Empty
    Enum ReportList
        PLComparison
        PLComparisonDetailAccount
        PLComparisonSubSubAccount
    End Enum
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If ReportName = ReportList.PLComparison Then
                AddRptParam("@FromDateF", Me.dtpFromDateFirst.Value)
                AddRptParam("@ToDateF", Me.dtpToDateFirst.Value)
                AddRptParam("@FromDateS", Me.dtpFromDateS.Value)
                AddRptParam("@ToDateS", Me.dtpToDateS.Value)
                AddRptParam("@CostCenterID", Me.cmbCostCenter.SelectedValue)
                AddRptParam("@CostCenter", Me.cmbCostCenter.Text.ToString)
                ShowReport("rptProftAndLossStatementComparison")
            ElseIf ReportName = ReportList.PLComparisonDetailAccount Then
                AddRptParam("@FromDate", Me.dtpFromDateFirst.Value)
                AddRptParam("@ToDate", Me.dtpToDateFirst.Value)
                AddRptParam("@CompareFromDate", Me.dtpFromDateS.Value)
                AddRptParam("@CompareToDate", Me.dtpToDateS.Value)
                AddRptParam("@CostCenter", Me.cmbCostCenter.Text.ToString)
                ShowReport("rptProftAndLossDetailComparison")
            ElseIf ReportName = ReportList.PLComparisonSubSubAccount Then
                AddRptParam("@FromDate", Me.dtpFromDateFirst.Value)
                AddRptParam("@ToDate", Me.dtpToDateFirst.Value)
                AddRptParam("@CompareFromDate", Me.dtpFromDateS.Value)
                AddRptParam("@CompareToDate", Me.dtpToDateS.Value)
                AddRptParam("@CostCenter", Me.cmbCostCenter.Text.ToString)
                ShowReport("rptProftAndLossSubSubComparison")
            Else
                AddRptParam("@FromDateF", Me.dtpFromDateFirst.Value)
                AddRptParam("@ToDateF", Me.dtpToDateFirst.Value)
                AddRptParam("@FromDateS", Me.dtpFromDateS.Value)
                AddRptParam("@ToDateS", Me.dtpToDateS.Value)
                AddRptParam("@CostCenterID", Me.cmbCostCenter.SelectedValue)
                AddRptParam("@CostCenter", Me.cmbCostCenter.Text.ToString)
                ShowReport("rptProftAndLossStatementComparison")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.Close()
    End Sub
    Private Sub rptPLComparison_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            'If ReportName = ReportList.PLComparison Then
            '    Me.Text = "Profit & Loss Comparison"
            'ElseIf ReportName = ReportList.PLComparisonSubSubAccount Then
            '    Me.Text = "Profit & Loss Account Head Comparison"
            'ElseIf ReportName = ReportList.PLComparisonDetailAccount Then
            '    Me.Text = "Profit & Loss Account Detail Comparison"
            'Else
            '    Me.Text = "Profit & Loss Comparison"
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub lnkRefresh_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRefresh.LinkClicked
        Try
            Dim id As Integer = 0
            id = Me.cmbCostCenter.SelectedValue
            FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            Me.cmbCostCenter.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
