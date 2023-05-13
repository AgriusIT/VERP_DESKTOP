''25-Sep-2014 Task:2858 Imran Ali Add New CMFA Detail Report
Public Class frmRptCMFADetail
    Dim IsFormOpened As Boolean = False
    Public Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "Customer" Then
                FillUltraDropDown(Me.cmbAccounts, "Select coa_detail_id, detail_title as [Customer], detail_code as [A/C Code], sub_sub_title as [A/C Head] From vwCOADetail WHERE detail_title <> '' Order By detail_title ASC")
                Me.cmbAccounts.Rows(0).Activate()
                If Me.cmbAccounts.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(1).Width = 250
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(2).Width = 125
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Width = 200
                End If
            ElseIf Condition = "CostCenter" Then
                FillDropDown(Me.cmbProject, "Select costcenterid,name as [Cost Center], [Code],[CostCenterGroup] as [Group] From tblDefCostCenter Order BY Name ASC")
            ElseIf Condition = "CMFA" Then
                FillUltraDropDown(Me.cmbCMFA, "Select DocId, DocNo +'~'+Convert(varchar,DocDate,102) As DocNo From CMFAMasterTable WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) ORDER BY DocNo DESC")
                Me.cmbCMFA.Rows(0).Activate()
                Me.cmbCMFA.DisplayLayout.Bands(0).Columns("DocId").Hidden = True
                Me.cmbCMFA.DisplayLayout.Bands(0).Columns("DocNo").Width = 350
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ResetControls(Optional ByVal Condition As String = "")
        Try
            Me.dtpFromDate.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpToDate.Value = Date.Now
            Me.btnShow.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmRptCMFADetail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombo("Customer")
            FillCombo("CostCenter")
            FillCombo("CMFA")
            IsFormOpened = True
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            'If Me.cmbAccounts.ActiveRow Is Nothing Then Exit Sub
            'If Me.cmbAccounts.Value <= 0 Then
            '    ShowErrorMessage("Please select Customer.")
            '    Me.cmbAccounts.Focus()
            '    Exit Sub
            'End If
            If Me.cmbCMFA.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select Document.")
                Me.cmbCMFA.Focus()
                Exit Sub
            End If
            InsertData()
            AddRptParam("@DocId", Me.cmbCMFA.Value)
            ShowReport("RptCMFADetail", IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, "{SP_CMFADetail;1.coa_detail_id}=" & Me.cmbAccounts.Value & "", ""), "Nothing", "Nothing")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub InsertData()
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand
        cmd.Connection = Con
        cmd.Transaction = trans

        Try
            Dim strSQL As String = String.Empty
            strSQL = "TRUNCATE TABLE tmptblPOAgainstCMFA "
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            strSQL = " TRUNCATE TABLE tmptblAdvanceAgainstCMFA "
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            strSQL = " TRUNCATE TABLE tmptblExpenseAgainstCMFA"
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()

            strSQL = String.Empty
            strSQL = "SELECT IsNull(dbo.PurchaseOrderMasterTable.PurchaseOrderId,0) as PurchaseOrderId, " _
                      & " dbo.PurchaseOrderMasterTable.PurchaseOrderNo, Convert(Varchar,dbo.PurchaseOrderMasterTable.PurchaseOrderDate,102) as PurchaseOrderDate, dbo.PurchaseOrderMasterTable.VendorId, dbo.vwCOADetail.detail_title, " _
                      & " SUM(ISNULL(dbo.PurchaseOrderDetailTable.Qty, 0) * ISNULL(dbo.PurchaseOrderDetailTable.Price, 0)) AS POAmount, " _
                      & " SUM((ISNULL(dbo.PurchaseOrderDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.PurchaseOrderDetailTable.Qty, 0) * ISNULL(dbo.PurchaseOrderDetailTable.Price, 0))) " _
                      & " AS GSTAmount, IsNull(Payment.PaymentAmount,0) as PaymentAmount, 0 AS POBalance, IsNull(dbo.PurchaseOrderMasterTable.RefCMFADocId,0) as RefCMFADocId " _
                      & " FROM (SELECT ISNULL(dbo.ReceivingMasterTable.RefCMFADocId,0) AS CMFAId, dbo.ReceivingMasterTable.VendorId, dbo.InvoiceBasedPaymentsDetail.InvoiceId, " _
                      & " SUM(ISNULL(dbo.InvoiceBasedPaymentsDetail.PaymentAmount, 0)) AS PaymentAmount " _
                      & " FROM dbo.ReceivingMasterTable INNER JOIN " _
                      & " dbo.InvoiceBasedPaymentsDetail ON dbo.ReceivingMasterTable.ReceivingId = dbo.InvoiceBasedPaymentsDetail.InvoiceId INNER JOIN " _
                      & " dbo.InvoiceBasedPayments ON dbo.InvoiceBasedPaymentsDetail.PaymentId = dbo.InvoiceBasedPayments.PaymentId " _
                      & " WHERE(ISNULL(dbo.ReceivingMasterTable.RefCMFADocId, 0) <> 0)  "
            If Me.cmbCMFA.ActiveRow.Cells(0).Value > 0 Then
                strSQL += " AND ISNULL(dbo.ReceivingMasterTable.RefCMFADocId,0) =" & Me.cmbCMFA.Value & ""
            End If
            If Me.cmbAccounts.ActiveRow.Cells(0).Value > 0 Then
                strSQL += " AND dbo.ReceivingMasterTable.VendorId =" & Me.cmbAccounts.Value & ""
            End If
            If Me.cmbProject.SelectedIndex > 0 Then
                strSQL = " AND ISNULL(dbo.ReceivingMasterTable.CostCenterId,0) =" & Me.cmbProject.SelectedValue & ""
            End If
            strSQL += " GROUP BY ISNULL(dbo.ReceivingMasterTable.RefCMFADocId, 0), dbo.ReceivingMasterTable.VendorId, dbo.InvoiceBasedPaymentsDetail.InvoiceId)  " _
                      & " AS Payment INNER JOIN " _
                      & " dbo.ReceivingMasterTable AS ReceivingMasterTable_1 ON Payment.InvoiceId = ReceivingMasterTable_1.ReceivingId RIGHT OUTER JOIN " _
                      & " dbo.PurchaseOrderMasterTable INNER JOIN " _
                      & " dbo.vwCOADetail ON dbo.PurchaseOrderMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id INNER JOIN " _
                      & " dbo.PurchaseOrderDetailTable ON dbo.PurchaseOrderMasterTable.PurchaseOrderId = dbo.PurchaseOrderDetailTable.PurchaseOrderId ON  " _
                      & " ReceivingMasterTable_1.PurchaseOrderID = dbo.PurchaseOrderMasterTable.PurchaseOrderId AND  " _
                      & " Payment.CMFAId = dbo.PurchaseOrderMasterTable.RefCMFADocId " _
                      & " WHERE(ISNULL(dbo.PurchaseOrderMasterTable.RefCMFADocId, 0) <> 0) "
            If Me.cmbCMFA.ActiveRow.Cells(0).Value > 0 Then
                strSQL += " AND ISNULL(dbo.PurchaseOrderMasterTable.RefCMFADocId,0)=" & Me.cmbCMFA.Value & ""
            End If
            'If Me.cmbAccounts.ActiveRow.Cells(0).Value > 0 Then
            '    strSQL += " AND dbo.PurchaseOrderMasterTable.VendorId =" & Me.cmbAccounts.Value & ""
            'End If
            If Me.cmbProject.SelectedIndex > 0 Then
                strSQL = " AND ISNULL(dbo.PurchaseOrderMasterTable.CostCenterId,0) =" & Me.cmbProject.SelectedValue & ""
            End If
            strSQL += " GROUP BY ISNULL(dbo.PurchaseOrderMasterTable.RefCMFADocId, 0), IsNull(dbo.PurchaseOrderMasterTable.PurchaseOrderId,0), dbo.PurchaseOrderMasterTable.PurchaseOrderNo,  " _
                      & " Convert(Varchar,dbo.PurchaseOrderMasterTable.PurchaseOrderDate,102), dbo.PurchaseOrderMasterTable.VendorId,dbo.vwCOADetail.detail_title, IsNull(Payment.PaymentAmount,0) "

            strSQL = "INSERT INTO tmptblPOAgainstCMFA(POId,PONo,PODate,VendorId,Vendor,POAmount,GSTAmount,PaymentAmount,POBalance,CMFAId) " & strSQL & ""
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()

            strSQL = String.Empty
            strSQL = "SELECT dbo.tblVoucher.voucher_id, dbo.tblVoucher.voucher_no, Convert(varchar,dbo.tblVoucher.voucher_date) as voucher_date, dbo.tblVoucherDetail.coa_detail_id, dbo.vwCOADetail.detail_title, " _
            & " dbo.tblVoucherDetail.comments, SUM(dbo.tblVoucherDetail.debit_amount) AS ExpenseAmount, ISNULL(dbo.tblVoucher.CMFADocId, 0) AS CMFAId " _
            & " FROM  dbo.tblVoucher INNER JOIN " _
            & " dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " _
            & " dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id " _
            & " WHERE (dbo.vwCOADetail.account_type = 'Expense')"
            If Me.cmbCMFA.ActiveRow.Cells(0).Value > 0 Then
                strSQL += " AND  ISNULL(dbo.tblVoucher.CMFADocId, 0)=" & Me.cmbCMFA.Value & ""
            End If
            If Me.cmbProject.SelectedIndex > 0 Then
                strSQL = " AND ISNULL(dbo.tblVoucherDetail.CostCenterId,0) =" & Me.cmbProject.SelectedValue & ""
            End If
            strSQL += " GROUP BY dbo.tblVoucher.voucher_id, dbo.tblVoucher.voucher_no, Convert(varchar,dbo.tblVoucher.voucher_date), dbo.tblVoucherDetail.coa_detail_id, dbo.vwCOADetail.detail_title,  " _
            & " dbo.tblVoucherDetail.comments, ISNULL(dbo.tblVoucher.CMFADocId, 0) " _
            & " HAVING (SUM(dbo.tblVoucherDetail.debit_amount) <> 0) AND (ISNULL(dbo.tblVoucher.CMFADocId, 0) <> 0)"

            strSQL = "INSERT INTO tmptblExpenseAgainstCMFA(VoucherId,VoucherNo,VoucherDate,AccountId,AccountTitle,Narration,ExpenseAmount,CMFAId) " & strSQL & ""
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()


            strSQL = String.Empty
            strSQL = "SELECT a.RequestId, a.RequestNo, a.RequestDate, a.coa_detail_id, a.detail_title, a.Remarks, Sum(IsNull(a.AdvAmount,0)) as AdvAmount, SUM(IsNull(Adj.Adjusted,0)) as Adjusted, Sum(IsNull(a.Balance,0)) as Balance, a.CMFADocId " _
                     & " FROM (SELECT ISNULL(dbo.CashRequestHead.CMFADocId, 0) as CMFADocId,ISNULL(dbo.CashRequestHead.RequestId, 0) AS RequestId, dbo.CashRequestHead.RequestNo, dbo.CashRequestHead.RequestDate,  " _
                     & " dbo.tblVoucherDetail.coa_detail_id, dbo.vwCOADetail.detail_title, dbo.CashRequestHead.Remarks, SUM(dbo.tblVoucherDetail.debit_amount) AS AdvAmount,  " _
                     & " 0 AS Balance " _
                     & " FROM dbo.tblVoucherDetail INNER JOIN " _
                     & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
                     & " dbo.CashRequestHead ON dbo.tblVoucher.CashRequestID = dbo.CashRequestHead.RequestId INNER JOIN " _
                     & " dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id WHERE  (ISNULL(dbo.CashRequestHead.RequestId, 0) <> 0) "
            If Me.cmbCMFA.ActiveRow.Cells(0).Value > 0 Then
                strSQL += " AND  ISNULL(dbo.CashRequestHead.CMFADocId, 0)=" & Me.cmbCMFA.Value & ""
            End If
            If Me.cmbProject.SelectedIndex > 0 Then
                strSQL = " AND ISNULL(dbo.tblVoucherDetail.CostCenterId,0) =" & Me.cmbProject.SelectedValue & ""
            End If
            strSQL += " GROUP BY ISNULL(dbo.CashRequestHead.CMFADocId, 0), ISNULL(dbo.CashRequestHead.RequestId, 0), dbo.CashRequestHead.RequestNo, dbo.CashRequestHead.RequestDate,  " _
                     & " dbo.tblVoucherDetail.coa_detail_id, dbo.vwCOADetail.detail_title, dbo.CashRequestHead.Remarks " _
                     & " HAVING (SUM(dbo.tblVoucherDetail.debit_amount) <> 0)) AS a LEFT OUTER JOIN " _
                     & " (SELECT ISNULL(CashRequestHead_1.RequestId, 0) AS RequestId, tblVoucherDetail_1.coa_detail_id, SUM(tblVoucherDetail_1.credit_amount) AS Adjusted " _
                     & " FROM dbo.tblVoucherDetail AS tblVoucherDetail_1 INNER JOIN " _
                     & " dbo.tblVoucher AS tblVoucher_1 ON tblVoucherDetail_1.voucher_id = tblVoucher_1.voucher_id INNER JOIN " _
                     & " dbo.CashRequestHead AS CashRequestHead_1 ON tblVoucher_1.CashRequestID = CashRequestHead_1.RequestId INNER JOIN " _
                     & " dbo.vwCOADetail AS vwCOADetail_1 ON tblVoucherDetail_1.coa_detail_id = vwCOADetail_1.coa_detail_id WHERE (ISNULL(CashRequestHead_1.RequestId, 0) <> 0) " _
                     & " GROUP BY ISNULL(CashRequestHead_1.RequestId,0), tblVoucherDetail_1.coa_detail_id " _
                     & " HAVING (SUM(tblVoucherDetail_1.credit_amount) <> 0)) AS Adj ON a.RequestId = Adj.RequestId AND  " _
                     & " Adj.coa_detail_id = a.coa_detail_id Group By a.RequestId, a.RequestNo, a.RequestDate, a.coa_detail_id, a.detail_title, a.Remarks, a.CMFADocId"

            strSQL = "INSERT INTO tmptblAdvanceAgainstCMFA(CRQId,CRQNo,CRQDate,AccountId,AccountTitle,Narration,CRQAmount,AdjustedAmount,CRQBalance,CMFAId) " & strSQL & ""
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()


            trans.Commit()


        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub

    'Private Sub cmbAccounts_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccounts.Leave
    '    Try
    '        If IsFormOpened = True Then FillCombo("CMFA")
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbAccounts.ActiveRow.Cells(0).Value
            FillCombo("Customer")
            Me.cmbAccounts.Value = id
            id = Me.cmbProject.SelectedValue
            FillCombo("CostCenter")
            Me.cmbProject.SelectedValue = id
            id = Me.cmbCMFA.ActiveRow.Cells(0).Value
            FillCombo("CMFA")
            Me.cmbCMFA.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class