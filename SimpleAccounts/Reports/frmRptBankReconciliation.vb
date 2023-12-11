Public Class frmRptBankReconciliation
    Private Sub frmRptBankReconciliation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillDropDown(Me.cmbAccounts, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], sub_sub_title as [Account Head] From vwCOADetail WHERE Account_Type='Bank' ORDER BY 2 ASC")
            Me.dtpDateFrom.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpDateTo.Value = Date.Now
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try

            AddRptParam("@FromDate", Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("@AccountId", Me.cmbAccounts.SelectedValue)
            AddRptParam("@IncludeInProcessCheque", IIf(Me.chkIncludeInProcess.Checked = True, 1, 0))
            AddRptParam("BankAccountTitle", Me.cmbAccounts.Text)
            AddRptParam("BankAccountCode", CType(Me.cmbAccounts.SelectedItem, DataRowView).Row.Item("Account Code").ToString)
            Dim opening As Integer = GetAccountOpeningBalance(Me.cmbAccounts.SelectedValue, Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:0:00"), "Bank", True)
            ShowReport("rptBankReconciliation", , , , , Val(opening).ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class