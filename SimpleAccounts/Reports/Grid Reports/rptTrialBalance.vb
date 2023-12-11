''27-Dec-2013 R:M-2    Imran         Rounding Figure
'2015-02-21 Changes Against Task# 20150211 decimal values of Debit Amount by Ali Ansari  
'6-8-2015 TASKM86151 Imran Ali Ledger Problem
'30-July-2017       TFS# 1102       R@! Shahid      Zero values should not display in Trial Balance

Imports System.Data.OleDb
Imports SBModel
Public Class rptTrialBalance
    Public DrillDown As Boolean = False
    Public NoteId As Integer = 0
    Public PLNoteId As Integer = 0
    Public Tracking As Boolean = False
    Public CostID As String = ""
    Public Company As Integer = 0
    Public SubSubAccount As Boolean = False
    Dim flgCompanyRights As Boolean = False

    Private Sub rptTrialBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub rptAccountBalances_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.DateTimePicker1.Value = Date.Now.AddMonths(-1)
        If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            flgCompanyRights = getConfigValueByType("CompanyRights")
        End If
        Me.cmbPeriod.Text = "Current Month"
        Me.GetSecurityRights()
        Me.FillCombo("CostCenter")
        FillCombo("Currency")
        Me.cmbCurrency.SelectedIndex = 0
        Me.cmbAcLevel.SelectedIndex = 0
        DateTimePicker1_ValueChanged(Nothing, Nothing)
    End Sub
    ''' <summary>
    ''' Ali Faisal : Changes for Currency wise Trial Balance Report for OTC on 29-May-2018
    ''' </summary>
    ''' <remarks></remarks>
    Sub FillGrid()
        Dim strSql As String
        Dim fromDate As DateTime = CDate(Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
        Dim ToDate As DateTime = CDate(Me.DateTimePicker2.Value.Year & "-" & Me.DateTimePicker2.Value.Month & "-" & Me.DateTimePicker2.Value.Day & " 23:59:59")
        Try


            '' Request No 875 
            '' 19-11-2013 By Imran Ali
            '' Column Wise Balance/Opening 


            ''Add columns Opening/Balance in debit/credit in this query
            If Me.cmbAcLevel.Text = "Detail A/c" Then
                'strSql = "SELECT TOP 100 PERCENT dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_code Code, dbo.vwCOADetail.detail_title, ISNULL(Opening.Opning, 0) AS Opening, CASE WHEN  ISNULL(Opening.Opning, 0) >=0 THEN  ISNULL(Opening.Opning, 0) ELSE 0 End as ODebit, CASE WHEN  ISNULL(Opening.Opning, 0) < 0 THEN  Abs(ISNULL(Opening.Opning, 0)) ELSE 0 End as OCredit, " & _
                '               "ISNULL(VoucherSum.Debit_Amount, 0) AS Debit_Amount, ISNULL(VoucherSum.Credit_Amount, 0) AS Credit_Amount,(ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0)) AS Balance, CASE WHEN (ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0)) > =0 THEN (ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0)) ELSE 0 End as BDebit, CASE WHEN (ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0)) < 0 Then Abs(ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0)) ELSE 0 end as BCredit, dbo.vwCOADetail.account_type,  " & _
                '               "dbo.vwCOADetail.main_sub_id, dbo.vwCOADetail.CityName " & _
                '               "FROM         dbo.vwCOADetail LEFT OUTER JOIN " & _
                '               "(SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " & _
                '               "ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " & _
                '               "FROM          dbo.tblVoucherDetail INNER JOIN " & _
                '               "dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " & _
                '               "WHERE      tblvoucher.voucher_date BETWEEN '" & fromDate & "' AND '" & ToDate & "' " & _
                '               " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " & _
                '               "(SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
                '               "FROM          dbo.tblVoucher V INNER JOIN " & _
                '               "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " & _
                '               "WHERE      (V.voucher_date < '" & fromDate & "') " & _
                '               "GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                'strSql += " WHERE vwCoaDetail.detail_title Is Not NULL "
                strSql = "SELECT TOP 100 PERCENT dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_code Code, dbo.vwCOADetail.detail_title, ISNULL(Opening.Opning, 0) AS Opening, CASE WHEN  ISNULL(Opening.Opning, 0) >=0 THEN  ISNULL(Opening.Opning, 0) ELSE 0 End as ODebit, CASE WHEN  ISNULL(Opening.Opning, 0) < 0 THEN  Abs(ISNULL(Opening.Opning, 0)) ELSE 0 End as OCredit, " & _
                           "ISNULL(VoucherSum.Debit_Amount, 0) AS Debit_Amount, ISNULL(VoucherSum.Credit_Amount, 0) AS Credit_Amount,(ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0)) AS Balance, CASE WHEN (ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0)) > =0 THEN (ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0)) ELSE 0 End as BDebit, CASE WHEN (ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0)) < 0 Then Abs(ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0)) ELSE 0 end as BCredit, dbo.vwCOADetail.account_type,  " & _
                           "dbo.vwCOADetail.main_sub_id, dbo.vwCOADetail.CityName,  dbo.vwCOADetail.Parent_Id " & _
                           "FROM         dbo.vwCOADetail LEFT OUTER JOIN " & _
                           "(SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " & _
                           "ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " & _
                           "FROM dbo.tblVoucherDetail INNER JOIN " & _
                           "dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " & _
                           "WHERE (Convert(Varchar,tblvoucher.voucher_date,102) BETWEEN Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & "  " & IIf(Me.chkcommission.Checked = False, " AND tblvoucher.voucher_type_id <> 12", "") & " " & _
                           " " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And dbo.tblVoucherDetail.CostCenterID =" & Me.cmbCostCenter.Value & "") & " " & IIf(Me.cmbCurrency.SelectedValue > 0, " AND dbo.tblVoucherDetail.CurrencyId =" & Me.cmbCurrency.SelectedValue & " ", "  ") & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " & _
                           "(SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
                           "FROM          dbo.tblVoucher V INNER JOIN " & _
                           "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " & _
                           "WHERE      (Convert(varchar,V.voucher_date,102) < Convert(Datetime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & "  " & IIf(Me.chkcommission.Checked = False, " AND V.voucher_type_id <> 12", "") & "  " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And VD.CostCenterID =" & Me.cmbCostCenter.Value & "") & IIf(Me.cmbCurrency.SelectedValue > 0, " AND VD.CurrencyId =" & Me.cmbCurrency.SelectedValue & " ", "  ") & "   " & _
                           "GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                strSql += " WHERE vwCoaDetail.detail_title Is Not NULL "
                If Me.chkZeroValue.Checked = True Then

                    '30-July-2017       TFS# 1102       R@! Shahid      Zero values should not display in Trial Balance
                    'Commenting old line
                    'strSql += " And (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "

                    'Query was bringing records due to decimal values on 4th or 5th decimal digit
                    'Now rounded balance on 2 digints
                    'It will exlude all the balances which are smaller than 2 digits
                    strSql += " And (round(ISNULL(Opening.Opning, 0),2) <> 0 Or round(ISNULL(VoucherSum.Debit_Amount, 0), 2) <> 0 Or round(ISNULL(VoucherSum.Credit_Amount, 0), 2) <> 0) "

                End If
                If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                    strSql = strSql + " AND vwCOADetail.detail_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "' "
                End If
                strSql = strSql + "ORDER BY dbo.vwCOADetail.coa_detail_id "

                Me.ContextMenuStrip1.Items(0).Enabled = True
                ''Add columns Opening/Balance in debit/credit in this query
            ElseIf Me.cmbAcLevel.Text = "Sub Sub A/c" Then
                'strSql = "SELECT  DISTINCT   TOP 100 PERCENT dbo.vwCOADetail.main_sub_sub_id as coa_detail_id, dbo.vwCOADetail.sub_sub_code AS Code, dbo.vwCOADetail.sub_sub_title as detail_title, " _
                '         & "  SUM(ISNULL(Opening.Opning, 0)) AS Opening, CASE WHEN SUM(ISNULL(Opening.Opning, 0)) >=0 THEN SUM(ISNULL(Opening.Opning, 0)) ELSE 0 End as ODebit, CASE WHEN SUM(ISNULL(Opening.Opning, 0)) < 0 THEN Abs(SUM(ISNULL(Opening.Opning, 0))) ELSE 0 End as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, " _
                '         & " SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0))  " _
                '         & " - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) AS Balance, CASE WHEN (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0 then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, Case When (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then Abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_sub_id as main_sub_id" _
                '         & " FROM dbo.vwCOADetail LEFT OUTER JOIN " _
                '         & " (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                '         & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                '         & " FROM          dbo.tblVoucherDetail INNER JOIN " _
                '         & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                '         & " WHERE      tblvoucher.voucher_date BETWEEN '" & fromDate & "' AND '" & ToDate & "' " _
                '         & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                '         & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                '         & " FROM          dbo.tblVoucher V INNER JOIN " _
                '         & " dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                '         & " WHERE      (V.voucher_date < '" & fromDate & "') " _
                '         & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                'strSql += " WHERE vwCoaDetail.detail_title Is Not NULL "
                strSql = "SELECT  DISTINCT   TOP 100 PERCENT dbo.vwCOADetail.main_sub_sub_id as coa_detail_id, dbo.vwCOADetail.sub_sub_code AS Code, dbo.vwCOADetail.sub_sub_title as detail_title, " _
                       & "  SUM(ISNULL(Opening.Opning, 0)) AS Opening, CASE WHEN SUM(ISNULL(Opening.Opning, 0)) >=0 THEN SUM(ISNULL(Opening.Opning, 0)) ELSE 0 End as ODebit, CASE WHEN SUM(ISNULL(Opening.Opning, 0)) < 0 THEN Abs(SUM(ISNULL(Opening.Opning, 0))) ELSE 0 End as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, " _
                       & " SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0))  " _
                       & " - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) AS Balance, CASE WHEN (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0 then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, Case When (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then Abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_sub_id as main_sub_id" _
                       & " FROM dbo.vwCOADetail LEFT OUTER JOIN " _
                       & " (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                       & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                       & " FROM          dbo.tblVoucherDetail INNER JOIN " _
                       & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                       & " WHERE     (Convert(Varchar,tblvoucher.voucher_date,102) BETWEEN Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & "  " & IIf(Me.chkcommission.Checked = False, " AND tblvoucher.voucher_type_id <> 12", "") & " " _
                       & " " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And dbo.tblVoucherDetail.CostCenterID =" & Me.cmbCostCenter.Value & "") & " " & IIf(Me.cmbCurrency.SelectedValue > 0, " AND dbo.tblVoucherDetail.CurrencyId =" & Me.cmbCurrency.SelectedValue & " ", "  ") & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                       & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                       & " FROM          dbo.tblVoucher V INNER JOIN " _
                       & " dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                       & " WHERE      (Convert(Varchar,V.voucher_date,102) < Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & "  " & IIf(Me.chkcommission.Checked = False, " AND V.voucher_type_id <> 12", "") & "  " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And VD.CostCenterID =" & Me.cmbCostCenter.Value & "") & IIf(Me.cmbCurrency.SelectedValue > 0, " AND VD.CurrencyId =" & Me.cmbCurrency.SelectedValue & " ", "  ") & "  " _
                       & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                strSql += " WHERE vwCoaDetail.detail_title Is Not NULL "
                If Me.chkZeroValue.Checked = True Then
                    '30-July-2017       TFS# 1102       R@! Shahid      Zero values should not display in Trial Balance
                    'Commenting old line
                    'strSql += " And (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "

                    'Query was bringing records due to decimal values on 4th or 5th decimal digit
                    'Now rounded balance on 2 digints
                    'It will exlude all the balances which are smaller than 2 digits

                    strSql += " And (round(ISNULL(Opening.Opning, 0),2) <> 0 Or round(ISNULL(VoucherSum.Debit_Amount, 0), 2) <> 0 Or round(ISNULL(VoucherSum.Credit_Amount, 0), 2) <> 0) "
                End If
                If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                    strSql = strSql + " AND dbo.vwCOADetail.sub_sub_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "'"
                End If
                strSql = strSql + " GROUP BY dbo.vwCOADetail.main_sub_sub_id, dbo.vwCOADetail.sub_sub_code, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_id,  " _
                     & " dbo.vwCOADetail.sub_sub_title "

                strSql += "ORDER BY dbo.vwCOADetail.main_sub_sub_id "

                Me.ContextMenuStrip1.Items(0).Enabled = False

                ''Add columns Opening/Balance in debit/credit in this query
            ElseIf Me.cmbAcLevel.Text = "Sub A/c" Then
                'strSql = " SELECT  DISTINCT   TOP 100 PERCENT dbo.vwCOADetail.main_sub_id as coa_detail_id, dbo.vwCOADetail.sub_code AS Code, dbo.vwCOADetail.sub_title as detail_title, SUM(ISNULL(Opening.Opning, 0)) " _
                '       & " AS Opening, CASE WHEN SUM(ISNULL(Opening.Opning,0)) >=0 then SUM(ISNULL(Opening.Opning,0)) else 0 end as ODebit, CASE WHEN SUM(ISNULL(Opening.Opning,0)) < 0 then Abs(SUM(ISNULL(Opening.Opning,0))) else 0 end as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, " _
                '       & " SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Balance, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0 then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) <0 then Abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit,  " _
                '       & " dbo.vwCOADetail.main_sub_id" _
                '       & " FROM dbo.vwCOADetail LEFT OUTER JOIN " _
                '       & " (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                '       & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                '       & " FROM          dbo.tblVoucherDetail INNER JOIN " _
                '       & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                '       & " WHERE      tblvoucher.voucher_date BETWEEN '" & fromDate & "' AND '" & ToDate & "' " _
                '       & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                '       & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                '       & " FROM          dbo.tblVoucher V INNER JOIN " _
                '       & " dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                '       & " WHERE      (V.voucher_date < '" & fromDate & "') " _
                '       & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                'strSql += " WHERE vwCoaDetail.detail_title Is Not NULL "
                strSql = " SELECT  DISTINCT   TOP 100 PERCENT dbo.vwCOADetail.main_sub_id as coa_detail_id, dbo.vwCOADetail.sub_code AS Code, dbo.vwCOADetail.sub_title as detail_title, SUM(ISNULL(Opening.Opning, 0)) " _
                   & " AS Opening, CASE WHEN SUM(ISNULL(Opening.Opning,0)) >=0 then SUM(ISNULL(Opening.Opning,0)) else 0 end as ODebit, CASE WHEN SUM(ISNULL(Opening.Opning,0)) < 0 then Abs(SUM(ISNULL(Opening.Opning,0))) else 0 end as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, " _
                   & " SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Balance, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0 then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) <0 then Abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit,  " _
                   & " dbo.vwCOADetail.main_sub_id" _
                   & " FROM dbo.vwCOADetail LEFT OUTER JOIN " _
                   & " (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                   & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                   & " FROM          dbo.tblVoucherDetail INNER JOIN " _
                   & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                   & " WHERE    (Convert(varchar,tblvoucher.voucher_date,102) BETWEEN Convert(dateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & "  " & IIf(Me.chkcommission.Checked = False, " AND tblvoucher.voucher_type_id <> 12", "") & " " _
                   & " " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And dbo.tblVoucherDetail.CostCenterID =" & Me.cmbCostCenter.Value & "") & " " & IIf(Me.cmbCurrency.SelectedValue > 0, " AND dbo.tblVoucherDetail.CurrencyId =" & Me.cmbCurrency.SelectedValue & " ", "  ") & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                   & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                   & " FROM          dbo.tblVoucher V INNER JOIN " _
                   & " dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                   & " WHERE      (Convert(Varchar,V.voucher_date,102) < Convert(Datetime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & "  " & IIf(Me.chkcommission.Checked = False, " AND V.voucher_type_id <> 12", "") & "  " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And VD.CostCenterID =" & Me.cmbCostCenter.Value & "") & IIf(Me.cmbCurrency.SelectedValue > 0, " AND VD.CurrencyId =" & Me.cmbCurrency.SelectedValue & " ", "  ") & "  " _
                   & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                strSql += " WHERE vwCoaDetail.detail_title Is Not NULL "
                If Me.chkZeroValue.Checked = True Then
                    '30-July-2017       TFS# 1102       R@! Shahid      Zero values should not display in Trial Balance
                    'Commenting old line
                    'strSql += " And (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "

                    'Query was bringing records due to decimal values on 4th or 5th decimal digit
                    'Now rounded balance on 2 digints
                    'It will exlude all the balances which are smaller than 2 digits
                    strSql += " And (round(ISNULL(Opening.Opning, 0),2) <> 0 Or round(ISNULL(VoucherSum.Debit_Amount, 0), 2) <> 0 Or round(ISNULL(VoucherSum.Credit_Amount, 0), 2) <> 0) "
                End If
                If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                    strSql = strSql + " AND dbo.vwCOADetail.sub_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "'"
                End If

                strSql = strSql + " GROUP BY dbo.vwCOADetail.main_sub_id, dbo.vwCOADetail.sub_code, dbo.vwCOADetail.main_sub_id,  " _
                     & " dbo.vwCOADetail.sub_title "

                strSql = strSql + "ORDER BY dbo.vwCOADetail.main_sub_id "

                Me.ContextMenuStrip1.Items(0).Enabled = False
                ''Add columns Opening/Balance in debit/credit in this query
            Else
                'strSql = " SELECT DISTINCT TOP 100 PERCENT dbo.vwCOADetail.coa_main_id as coa_detail_id, dbo.vwCOADetail.main_code AS Code, dbo.vwCOADetail.main_title as detail_title , SUM(ISNULL(Opening.Opning, " _
                '        & "   0)) AS Opening, CASE when sum(isnull(opening.opning,0)) > =0 then sum(isnull(opening.opning,0))  else 0 end as ODebit, case when sum(isnull(opening.opning,0)) <0 then Abs(sum(isnull(opening.opning,0)))  else 0 end as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, " _
                '        & "(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) AS Balance,  case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0 then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then Abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, " _
                '        & "  vwCOADetail.coa_main_id as main_sub_id " _
                '        & " FROM          dbo.vwCOADetail LEFT OUTER JOIN" _
                '        & "          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                '        & "                                ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                '        & "      FROM          dbo.tblVoucherDetail INNER JOIN " _
                '        & "                          dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                '        & " WHERE      tblvoucher.voucher_date BETWEEN '" & fromDate & "' AND '" & ToDate & "' " _
                '        & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                '        & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                '        & " FROM          dbo.tblVoucher V INNER JOIN " _
                '        & "                    dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                '        & " WHERE      (V.voucher_date < '" & fromDate & "') " _
                '        & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                'strSql += " WHERE vwCoaDetail.detail_title Is Not NULL "
                strSql = " SELECT DISTINCT TOP 100 PERCENT dbo.vwCOADetail.coa_main_id as coa_detail_id, dbo.vwCOADetail.main_code AS Code, dbo.vwCOADetail.main_title as detail_title , SUM(ISNULL(Opening.Opning, " _
            & "   0)) AS Opening, CASE when sum(isnull(opening.opning,0)) > =0 then sum(isnull(opening.opning,0))  else 0 end as ODebit, case when sum(isnull(opening.opning,0)) <0 then Abs(sum(isnull(opening.opning,0)))  else 0 end as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, " _
            & "(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) AS Balance,  case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0 then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then Abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, " _
            & "  vwCOADetail.coa_main_id as main_sub_id " _
            & " FROM          dbo.vwCOADetail LEFT OUTER JOIN" _
            & " (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
            & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
            & " FROM  dbo.tblVoucherDetail INNER JOIN " _
            & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
            & " WHERE     (Convert(Varchar,tblvoucher.voucher_date,102) BETWEEN Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & "  " & IIf(Me.chkcommission.Checked = False, " AND tblvoucher.voucher_type_id <> 12", "") & " " _
            & " " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And dbo.tblVoucherDetail.CostCenterID =" & Me.cmbCostCenter.Value & "") & " " & IIf(Me.cmbCurrency.SelectedValue > 0, " AND dbo.tblVoucherDetail.CurrencyId =" & Me.cmbCurrency.SelectedValue & " ", "  ") & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
            & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
            & " FROM          dbo.tblVoucher V INNER JOIN " _
            & "                    dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
            & " WHERE      (Convert(varchar,V.voucher_date,102) < Convert(Datetime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & "  " & IIf(Me.chkcommission.Checked = False, " AND V.voucher_type_id <> 12", "") & "  " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And VD.CostCenterID =" & Me.cmbCostCenter.Value & "") & IIf(Me.cmbCurrency.SelectedValue > 0, " AND VD.CurrencyId =" & Me.cmbCurrency.SelectedValue & " ", "  ") & "  " _
            & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                strSql += " WHERE vwCoaDetail.detail_title Is Not NULL "
                If Me.chkZeroValue.Checked = True Then
                    '30-July-2017       TFS# 1102       R@! Shahid      Zero values should not display in Trial Balance
                    'Commenting old line
                    'strSql += " And (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "

                    'Query was bringing records due to decimal values on 4th or 5th decimal digit
                    'Now rounded balance on 2 digints
                    'It will exlude all the balances which are smaller than 2 digits
                    strSql += " And (round(ISNULL(Opening.Opning, 0),2) <> 0 Or round(ISNULL(VoucherSum.Debit_Amount, 0), 2) <> 0 Or round(ISNULL(VoucherSum.Credit_Amount, 0), 2) <> 0) "
                End If
                If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                    strSql = strSql + " AND dbo.vwCOADetail.main_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "'"
                End If

                strSql = strSql + " GROUP BY dbo.vwCOADetail.coa_main_id, dbo.vwCOADetail.main_code,   " _
                     & " dbo.vwCOADetail.main_title "

                strSql = strSql + "ORDER BY dbo.vwCOADetail.coa_main_id "

            End If

            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable


            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            'dt.Columns("ODebit").Expression = "IIF(Opening >= 0,Opening,0)"
            'dt.Columns("OCredit").Expression = "IIF(Opening < 0,Opening,0)"
            'dt.Columns("BDebit").Expression = "IIF(Balance >= 0,Balance,0)"
            'dt.Columns("BCredit").Expression = "IIF(Balance < 0,Balance,0)"
            dt.AcceptChanges()

            Me.grdStock.DataSource = dt
            Me.grdStock1.DataSource = dt
            'Task:2359 Set Decimal Poin 

            Me.grdStock.RootTable.Columns("Opening").FormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Opening").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Debit_Amount").FormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Debit_Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Credit_Amount").FormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Credit_Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInValue


            Me.grdStock1.RootTable.Columns("Opening").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("Opening").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("ODebit").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("ODebit").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("OCredit").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("OCredit").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("Credit_Amount").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("Credit_Amount").TotalFormatString = "N" & DecimalPointInValue
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '2015-02-21 Changes Against Task# 20150211 decimal values of Debit Amount by Ali Ansari  
            Me.grdStock1.RootTable.Columns("Debit_Amount").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("Debit_Amount").TotalFormatString = "N" & DecimalPointInValue
            '2015-02-21 Changes Against Task# 20150211 decimal values of Debit Amount by Ali Ansari  
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Me.grdStock1.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("BDebit").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("BDebit").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("BCredit").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("BCredit").TotalFormatString = "N" & DecimalPointInValue

            'End Task:2359





            grdStock.ExpandRecords()
            Me.CtrlGrdBar1_Load(Nothing, Nothing) 'Call CtrlGrdbar 
            Me.grdStock.AutoSizeColumns()
            Me.grdStock1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GetCrystalReportRights()
        Me.FillGrid()
        Me.GetSecurityRights()
        Tracking = False
    End Sub

    Private Sub ExportToExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        Me.SaveFileDialog1.Filter = "Excel Files|.xls"
        'Me.SaveFileDialog1.DefaultExt = ".xls"
        Me.SaveFileDialog1.FileName = "Account Balances"
        Me.SaveFileDialog1.InitialDirectory = "C:\"
        If Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        If Me.SaveFileDialog1.FileName = "" Then Exit Sub

        'Me.UltraGridExcelExporter1.Export(Me.grdStock, Me.SaveFileDialog1.FileName) '& ".xls") '"c:\temp.xls")
        'Me.GridEXExporter1.GridEX(Me.grdStock, Me.SaveFileDialog1.FileName) '& ".xls") '"c:\temp.xls")
        System.Diagnostics.Process.Start(Me.SaveFileDialog1.FileName) '& ".xls")

    End Sub

    Private Sub RefreshDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshDataToolStripMenuItem.Click
        'If Not Me.grdStock.Rows.Count > 0 Then
        If Not Me.grdStock.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound) : Exit Sub
        End If
        Me.BackgroundWorker1.RunWorkerAsync()

    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.Button1.Enabled = True
                Me.Button2.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                CtrlGrdBar1.SendEmailToolStripMenuItem.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.rptTrialBalance)
            If Not dt Is Nothing Then
                If Not dt.Rows.Count = 0 Then
                    Me.Button1.Enabled = False
                    Me.Button2.Enabled = False
                    CtrlGrdBar1.mGridPrint.Enabled = False
                    CtrlGrdBar1.mGridExport.Enabled = False
                    CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    CtrlGrdBar1.SendEmailToolStripMenuItem.Enabled = False
                    'If Me.SaveToolStripButton.Text = "Save" Or Me.SaveToolStripButton.Text = "&Save" Then
                    '    Me.SaveToolStripButton.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                    'Else
                    '    Me.SaveToolStripButton.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                    'End If
                    'Me.DeleteToolStripButton.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                    'Me.PrintToolS      tripButton.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                    For Each RightsDt As GroupRights In Rights
                        If RightsDt.FormControlName = "Show" Then
                            Me.Button1.Enabled = True
                        ElseIf RightsDt.FormControlName = "Print" Then
                            Me.Button2.Enabled = True
                        ElseIf RightsDt.FormControlName = "Grid Print" Then
                            CtrlGrdBar1.mGridPrint.Enabled = True
                        ElseIf RightsDt.FormControlName = "Export" Then
                            CtrlGrdBar1.mGridExport.Enabled = True
                        ElseIf RightsDt.FormControlName = "Field Chooser" Then
                            CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        ElseIf RightsDt.FormControlName = "Send Email" Then
                            CtrlGrdBar1.SendEmailToolStripMenuItem.Enabled = True
                        End If
                    Next
                    Me.ContextMenuStrip1.Items("RefreshDataToolStripMenuItem").Enabled = dt.Rows(0).Item("Print_Rights").ToString
                    If Me.cmbAcLevel.SelectedIndex > 0 Then
                        Me.ContextMenuStrip1.Items("RefreshDataToolStripMenuItem").Enabled = False
                    End If
                    Me.ContextMenuStrip1.Items("ExportToExcelToolStripMenuItem").Enabled = dt.Rows(0).Item("Export_Rights").ToString
                    Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                End If
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub cmbAcLevel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAcLevel.SelectedIndexChanged
        If Me.cmbAcLevel.SelectedIndex = -1 Then Exit Sub

        If Me.cmbAcLevel.Text = "Detail A/c" Then

            Dim Str As String = "SELECT     TOP 100 PERCENT coa_detail_id,  detail_code, detail_title FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0) AND active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & "  order by detail_code"
            FillUltraDropDown(Me.cmbAccountFrom, Str)
            Me.cmbAccountFrom.Rows(0).Activate()

            FillUltraDropDown(Me.cmbAccountTo, Str)
            Me.cmbAccountTo.Rows(0).Activate()

        ElseIf Me.cmbAcLevel.Text = "Sub Sub A/c" Then

            Dim Str As String = "SELECT  DISTINCT   TOP 100 PERCENT main_sub_sub_id as  coa_detail_id,  sub_sub_code as  detail_code, sub_sub_title  as detail_title FROM   dbo.vwCOADetail WHERE     (main_sub_sub_id > 0) " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & " order by sub_sub_code"
            FillUltraDropDown(Me.cmbAccountFrom, Str)
            Me.cmbAccountFrom.Rows(0).Activate()

            FillUltraDropDown(Me.cmbAccountTo, Str)
            Me.cmbAccountTo.Rows(0).Activate()

        ElseIf Me.cmbAcLevel.Text = "Sub A/c" Then

            Dim Str As String = "SELECT DISTINCT    TOP 100 PERCENT main_sub_id as  coa_detail_id,  sub_code as  detail_code, sub_title as detail_title FROM   dbo.vwCOADetail WHERE     (main_sub_id > 0) order by sub_code"
            FillUltraDropDown(Me.cmbAccountFrom, Str)
            Me.cmbAccountFrom.Rows(0).Activate()

            FillUltraDropDown(Me.cmbAccountTo, Str)
            Me.cmbAccountTo.Rows(0).Activate()

        Else

            Dim Str As String = "SELECT  DISTINCT   TOP 100 PERCENT coa_main_id as  coa_detail_id,  main_code as  detail_code,  main_title as detail_title FROM   dbo.vwCOADetail WHERE     (coa_main_id > 0) order by main_code"
            FillUltraDropDown(Me.cmbAccountFrom, Str)
            Me.cmbAccountFrom.Rows(0).Activate()

            FillUltraDropDown(Me.cmbAccountTo, Str)
            Me.cmbAccountTo.Rows(0).Activate()

        End If


    End Sub

    Private Sub ShowCOATrialBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowCOATrialBalanceToolStripMenuItem.Click

        AddRptParam("@FromDate", Me.DateTimePicker1.Value.Date)
        AddRptParam("@ToDate", Me.DateTimePicker2.Value.Date)
        AddRptParam("PLevel", Me.cmbAcLevel.Text)
        AddRptParam("@IncludeUnPosted", IIf(Me.chkIncludeUnPostedVouchers.Checked = True, 1, 0))
        ShowReport("rptChartofAccountsTrial", , , , , , , , , , , , , "Trial Balance", "Date From " & Me.DateTimePicker1.Value.ToString("dd/MMM/yyyy") & " Date To " & Me.DateTimePicker2.Value.ToString("dd/MMM/yyyy") & "") ',  IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value, "Nothing"), fromDate, ToDate) Else ShowReport("Trial", IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value, "Nothing"))

    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'For Each r As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.grdStock.Selected.Rows
        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdStock.GetRows
            Dim StrVal As String = String.Empty
            If Val(r.Cells("Opening").Value) > 0 Then
                StrVal = StrVal & r.Cells("Opening").Value
            End If
            If Val(r.Cells("Debit_Amount").Value) > 0 Then
                StrVal = StrVal & r.Cells("Debit_Amount").Value
            End If
            If Val(r.Cells("Credit_Amount").Value) > 0 Then
                StrVal = StrVal & r.Cells("Credit_Amount").Value
            End If
            If Val(r.Cells("Balance").Value) > 0 Then
                StrVal = StrVal & r.Cells("Balance").Value
            End If
            'Val(Me.grdStock.ActiveRow.Cells("Opening").Value & Me.grdStock.ActiveRow.Cells("Debit_Amount").Value & Me.grdStock.ActiveRow.Cells("Credit_Amount").Value & Me.grdStock.ActiveRow.Cells("Balance").Value)
            'If StrVal.Length = 0 Then
            '    msg_Error(str_ErrorNoRecordExist) : Exit Sub
            'End If
            Dim fromDate As String = Me.DateTimePicker1.Value.Date 'Year & "." & Me.DateTimePicker1.Value.Month & "." & Me.DateTimePicker1.Value.Day
            Dim ToDate As String = Me.DateTimePicker2.Value.Date 'Year & "." & Me.DateTimePicker2.Value.Month & "." & Me.DateTimePicker2.Value.Day
            ShowReport("Ledger", "{SP_Rpt_Ledger.coa_detail_id}=" & r.Cells(0).Value, fromDate, ToDate, True, , , , , , , , , "Ledger", "Date From " & Me.DateTimePicker1.Value.ToString("dd/MMM/yyyy") & " Date To " & Me.DateTimePicker2.Value.ToString("dd/MMM/yyyy") & "")
            'Else ShowReport("Trial", IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value, "Nothing"), fromDate, ToDate)
        Next
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Me.Cursor = Cursors.WaitCursor
        Try

            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If
            Dim id As Integer = 0
            id = Me.cmbAccountFrom.Value
            cmbAcLevel_SelectedIndexChanged(cmbAcLevel, Nothing)
            Me.cmbAccountFrom.Value = id

            id = Me.cmbAccountTo.Value
            cmbAcLevel_SelectedIndexChanged(cmbAcLevel, Nothing)
            Me.cmbAccountTo.Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TableLayoutPanel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Tracking = False
            AddRptParam("@FromDate", Me.DateTimePicker1.Value.Date)
            AddRptParam("@ToDate", Me.DateTimePicker2.Value.Date)
            AddRptParam("PLevel", Me.cmbAcLevel.Text)
            AddRptParam("@IncludeUnPosted", IIf(Me.chkIncludeUnPostedVouchers.Checked = True, 1, 0))
            ShowReport("rptChartofAccountsTrial", IIf(Me.chkZeroValue.Checked = True, "{vwGlCOADetail.Opening} <> 0 or {vwGlCOADetail.Credit_Amount} <> 0 or {vwGlCOADetail.Debit_Amount} <> 0", ""), , , , , , , , , , , , "Trial Balance", "Date From " & Me.DateTimePicker1.Value.ToString("dd/MMM/yyyy") & " Date To " & Me.DateTimePicker2.Value.ToString("dd/MMM/yyyy") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStock.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdStock.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdStock.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "TrialBalance" & vbCrLf & "Date From:" & Me.DateTimePicker1.Value & " Date To: " & Me.DateTimePicker2.Value & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Tracking = False
            GetCrystalReportRights()
            AddRptParam("@FromDate", Me.DateTimePicker1.Value.Date)
            AddRptParam("@ToDate", Me.DateTimePicker2.Value.Date)
            AddRptParam("PLevel", Me.cmbAcLevel.Text)
            AddRptParam("@IncludeUnPosted", IIf(Me.chkIncludeUnPostedVouchers.Checked = True, 1, 0))
            ShowReport("rptChartofAccountsTrial", IIf(Me.chkZeroValue.Checked = True, "{vwGlCOADetail.Opening} <> 0 or {vwGlCOADetail.Credit_Amount} <> 0 or {vwGlCOADetail.Debit_Amount} <> 0", ""), , , True, , , , , , , , , "Trial Balance", "Date From " & Me.DateTimePicker1.Value.ToString("dd/MMM/yyyy") & " Date To " & Me.DateTimePicker2.Value.ToString("dd/MMM/yyyy") & "")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.DateTimePicker1.Value = Date.Today
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-1)
            Me.DateTimePicker2.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, 1, 1)
            Me.DateTimePicker2.Value = Date.Today
        End If
    End Sub
    Private Sub grdStock_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdStock.DoubleClick
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim fromDate As DateTime = CDate(Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
            Dim ToDate As DateTime = CDate(Me.DateTimePicker2.Value.Year & "-" & Me.DateTimePicker2.Value.Month & "-" & Me.DateTimePicker2.Value.Day & " 23:59:59")



            Dim strSql As String = String.Empty
            If Me.grdStock.RowCount = 0 Then Exit Sub
            If Me.cmbAcLevel.Text = "Detail A/c" Then
                'Value Set In Variables
                ''TASKM86151_IMR Check Form In SplitContainer
                'DrillDown = True
                frmMain.LoadControl("rptLedger")
                'End TASKM86151
                rptLedger.CoaDetailId = Me.grdStock.GetRow.Cells(0).Value
                rptLedger.dptFromDate = Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00")
                rptLedger.dptToDate = Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59")
                rptLedger.DateTimePicker1.Value = Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = Me.grdStock.GetRow.Cells(0).Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
                If Me.cmbCostCenter.ActiveRow IsNot Nothing Then
                    rptLedger.cmbCostCenter.SelectedValue = Me.cmbCostCenter.Value
                End If
                DrillDown = True
                rptLedger.GetLedger()
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")

                '' Request No 875 
                '' 19-11-2013 By Imran Ali
                '' Column Wise Balance/Opening 

                ''Add columns Opening/Balance in debit/credit in this query

            ElseIf cmbAcLevel.Text = "Main A/c" Then
                Me.cmbAcLevel.Text = "Sub A/c"
                Me.cmbAccountFrom.Rows(0).Activate()
                Me.cmbAccountTo.Rows(0).Activate()
                'strSql = " SELECT  DISTINCT TOP 100 PERCENT dbo.vwCOADetail.main_sub_id as coa_detail_id, dbo.vwCOADetail.sub_code AS Code, dbo.vwCOADetail.sub_title as detail_title, SUM(ISNULL(Opening.Opning, 0)) " _
                '           & " AS Opening, Case when SUM(ISNULL(Opening.Opning, 0)) >=0 then SUM(ISNULL(Opening.Opning, 0)) else 0 end as ODebit, case when SUM(ISNULL(Opening.Opning, 0)) < 0 then Abs(SUM(ISNULL(Opening.Opning, 0))) else 0 end as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, " _
                '           & " (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) AS Balance, Case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0  then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit,  " _
                '           & " dbo.vwCOADetail.main_sub_id" _
                '           & " FROM         dbo.vwCOADetail LEFT OUTER JOIN " _
                '           & " (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                '           & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                '           & " FROM          dbo.tblVoucherDetail INNER JOIN " _
                '           & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                '           & " WHERE      tblvoucher.voucher_date BETWEEN '" & fromDate & "' AND '" & ToDate & "' " _
                '           & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                '           & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                '           & " FROM          dbo.tblVoucher V INNER JOIN " _
                '           & " dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                '           & " WHERE      (V.voucher_date < '" & fromDate & "') " _
                '           & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                'strSql += " WHERE vwcoadetail.detail_title is not null "
                strSql = " SELECT  DISTINCT TOP 100 PERCENT dbo.vwCOADetail.main_sub_id as coa_detail_id, dbo.vwCOADetail.sub_code AS Code, dbo.vwCOADetail.sub_title as detail_title, SUM(ISNULL(Opening.Opning, 0)) " _
                        & " AS Opening, Case when SUM(ISNULL(Opening.Opning, 0)) >=0 then SUM(ISNULL(Opening.Opning, 0)) else 0 end as ODebit, case when SUM(ISNULL(Opening.Opning, 0)) < 0 then Abs(SUM(ISNULL(Opening.Opning, 0))) else 0 end as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, " _
                        & " (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) AS Balance, Case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0  then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit,  " _
                        & " dbo.vwCOADetail.main_sub_id" _
                        & " FROM         dbo.vwCOADetail LEFT OUTER JOIN " _
                        & " (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                        & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                        & " FROM          dbo.tblVoucherDetail INNER JOIN " _
                        & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                        & " WHERE (Convert(Varchar,tblvoucher.voucher_date,102) BETWEEN Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & "  " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And dbo.tblVoucherDetail.CostCenterID =" & Me.cmbCostCenter.Value & "") & " " _
                        & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                        & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                        & " FROM          dbo.tblVoucher V INNER JOIN " _
                        & " dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                        & " WHERE      (Convert(Varchar,V.voucher_date,102) < Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & " " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And VD.CostCenterID =" & Me.cmbCostCenter.Value & "") & "  " _
                        & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                strSql += " WHERE vwcoadetail.detail_title is not null "
                If Me.chkZeroValue.Checked = True Then
                    strSql += " AND (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "
                End If
                If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                    strSql = strSql + " AND dbo.vwCOADetail.sub_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "'"
                End If
                strSql += " AND vwCOADetail.coa_main_id=" & Me.grdStock.GetRow.Cells("main_sub_id").Value
                strSql = strSql + " GROUP BY dbo.vwCOADetail.main_sub_id, dbo.vwCOADetail.sub_code, dbo.vwCOADetail.main_sub_id,  " _
                         & " dbo.vwCOADetail.sub_title "
                strSql = strSql + "ORDER BY dbo.vwCOADetail.main_sub_id "
                Me.ContextMenuStrip1.Items(0).Enabled = False
            ElseIf cmbAcLevel.Text = "Sub A/c" Then
                Me.cmbAcLevel.Text = "Sub Sub A/c"
                Me.cmbAccountFrom.Rows(0).Activate()
                Me.cmbAccountTo.Rows(0).Activate()
                'strSql = "SELECT  DISTINCT   TOP 100 PERCENT dbo.vwCOADetail.main_sub_sub_id as coa_detail_id, dbo.vwCOADetail.sub_sub_code AS Code, dbo.vwCOADetail.sub_sub_title as detail_title, " _
                '     & "  SUM(ISNULL(Opening.Opning, 0)) AS Opening, Case when SUM(ISNULL(Opening.Opning, 0)) >=0 then SUM(ISNULL(Opening.Opning, 0)) else 0 end as ODebit, case when SUM(ISNULL(Opening.Opning, 0)) < 0 then Abs(SUM(ISNULL(Opening.Opning, 0))) else 0 end as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, " _
                '     & " SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0))  " _
                '     & " - SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Balance, Case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0  then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_sub_id as main_sub_id " _
                '     & " FROM         dbo.vwCOADetail LEFT OUTER JOIN " _
                '     & " (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                '     & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                '     & " FROM          dbo.tblVoucherDetail INNER JOIN " _
                '     & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                '     & " WHERE      tblvoucher.voucher_date BETWEEN '" & fromDate & "' AND '" & ToDate & "' " _
                '     & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                '     & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                '     & " FROM          dbo.tblVoucher V INNER JOIN " _
                '     & " dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                '     & " WHERE      (V.voucher_date < '" & fromDate & "') " _
                '     & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                'strSql += " WHERE vwcoadetail.detail_title is not null "
                strSql = "SELECT  DISTINCT   TOP 100 PERCENT dbo.vwCOADetail.main_sub_sub_id as coa_detail_id, dbo.vwCOADetail.sub_sub_code AS Code, dbo.vwCOADetail.sub_sub_title as detail_title, " _
                     & "  SUM(ISNULL(Opening.Opning, 0)) AS Opening, Case when SUM(ISNULL(Opening.Opning, 0)) >=0 then SUM(ISNULL(Opening.Opning, 0)) else 0 end as ODebit, case when SUM(ISNULL(Opening.Opning, 0)) < 0 then Abs(SUM(ISNULL(Opening.Opning, 0))) else 0 end as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, " _
                     & " SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0))  " _
                     & " - SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Balance, Case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0  then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_sub_id as main_sub_id " _
                     & " FROM         dbo.vwCOADetail LEFT OUTER JOIN " _
                     & " (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                     & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                     & " FROM          dbo.tblVoucherDetail INNER JOIN " _
                     & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                     & " WHERE      (Convert(Varchar,tblvoucher.voucher_date,102) BETWEEN Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & " " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And dbo.tblVoucherDetail.CostCenterID =" & Me.cmbCostCenter.Value & "") & " " _
                     & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                     & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                     & " FROM          dbo.tblVoucher V INNER JOIN " _
                     & " dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                     & " WHERE      (Convert(Varchar,V.voucher_date,102) < Convert(Datetime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & " " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And VD.CostCenterID =" & Me.cmbCostCenter.Value & "") & "  " _
                     & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                strSql += " WHERE vwcoadetail.detail_title is not null "
                If Me.chkZeroValue.Checked = True Then
                    strSql += " AND (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "
                End If
                If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                    strSql = strSql + " AND dbo.vwCOADetail.sub_sub_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "'"
                End If
                strSql += " AND dbo.vwCOADetail.main_sub_id =" & Me.grdStock.GetRow.Cells("main_sub_id").Value
                strSql = strSql + " GROUP BY dbo.vwCOADetail.main_sub_sub_id, dbo.vwCOADetail.sub_sub_code, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_id,  " _
                         & " dbo.vwCOADetail.sub_sub_title "
                strSql += "ORDER BY dbo.vwCOADetail.main_sub_sub_id "
                Me.ContextMenuStrip1.Items(0).Enabled = False
            ElseIf cmbAcLevel.Text = "Sub Sub A/c" Then
                Me.cmbAcLevel.Text = "Detail A/c"
                Me.cmbAccountFrom.Rows(0).Activate()
                Me.cmbAccountTo.Rows(0).Activate()
                'strSql = "SELECT TOP 100 PERCENT dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_code Code, dbo.vwCOADetail.detail_title, ISNULL(Opening.Opning, 0) AS Opening, Case when (ISNULL(Opening.Opning, 0)) >=0 then (ISNULL(Opening.Opning, 0)) else 0 end as ODebit, case when (ISNULL(Opening.Opning, 0)) < 0 then Abs((ISNULL(Opening.Opning, 0))) else 0 end as OCredit, " & _
                '           "ISNULL(VoucherSum.Debit_Amount, 0) AS Debit_Amount, ISNULL(VoucherSum.Credit_Amount, 0) AS Credit_Amount,ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0) AS Balance, Case when ((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) >=0  then ((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when ((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then abs((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, dbo.vwCOADetail.account_type,  " & _
                '           "dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.CityName " & _
                '           "FROM  dbo.vwCOADetail LEFT OUTER JOIN " & _
                '           "(SELECT  dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " & _
                '           "ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " & _
                '           "FROM dbo.tblVoucherDetail INNER JOIN " & _
                '           "dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " & _
                '           "WHERE  tblvoucher.voucher_date BETWEEN '" & fromDate & "' AND '" & ToDate & "' " & _
                '           " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " & _
                '           "(SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
                '           "FROM   dbo.tblVoucher V INNER JOIN " & _
                '           "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " & _
                '           "WHERE (V.voucher_date < '" & fromDate & "') " & _
                '           "GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                'strSql += " WHERE vwcoadetail.detail_title is not null "
                strSql = "SELECT TOP 100 PERCENT dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_code Code, dbo.vwCOADetail.detail_title, ISNULL(Opening.Opning, 0) AS Opening, Case when (ISNULL(Opening.Opning, 0)) >=0 then (ISNULL(Opening.Opning, 0)) else 0 end as ODebit, case when (ISNULL(Opening.Opning, 0)) < 0 then Abs((ISNULL(Opening.Opning, 0))) else 0 end as OCredit, " & _
                           "ISNULL(VoucherSum.Debit_Amount, 0) AS Debit_Amount, ISNULL(VoucherSum.Credit_Amount, 0) AS Credit_Amount,ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0) AS Balance, Case when ((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) >=0  then ((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when ((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then abs((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, dbo.vwCOADetail.account_type,  " & _
                           "dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.CityName " & _
                           "FROM  dbo.vwCOADetail LEFT OUTER JOIN " & _
                           "(SELECT  dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " & _
                           "ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " & _
                           "FROM dbo.tblVoucherDetail INNER JOIN " & _
                           "dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " & _
                           "WHERE  (Convert(Varchar,tblvoucher.voucher_date,102) BETWEEN Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & " " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And dbo.tblVoucherDetail.CostCenterID =" & Me.cmbCostCenter.Value & "") & " " & _
                           " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " & _
                           "(SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
                           "FROM   dbo.tblVoucher V INNER JOIN " & _
                           "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " & _
                           "WHERE (Convert(Varchar,V.voucher_date,102) < Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & " " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And VD.CostCenterID =" & Me.cmbCostCenter.Value & "") & "  " & _
                           "GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
                strSql += " WHERE vwcoadetail.detail_title is not null "
                If Me.chkZeroValue.Checked = True Then
                    strSql += " AND (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "
                End If
                If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                    strSql = strSql + " AND vwCOADetail.detail_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "' "
                End If
                strSql += " AND dbo.vwCOADetail.main_sub_sub_id=" & Me.grdStock.GetRow.Cells("main_sub_id").Value
                strSql = strSql + " ORDER BY dbo.vwCOADetail.coa_detail_id "

                Me.ContextMenuStrip1.Items(0).Enabled = True

            End If
            If strSql.Length > 0 Then
                Dim adp As New OleDbDataAdapter
                Dim dt As New DataTable
                adp = New OleDbDataAdapter(strSql, Con)
                adp.Fill(dt)
                dt.AcceptChanges()

                Me.grdStock.DataSource = dt
                Me.grdStock1.DataSource = dt
                Me.grdStock.AutoSizeColumns()
                Me.grdStock1.AutoSizeColumns()
            End If
            strSql = String.Empty


            '''''''''''
            Me.grdStock.RootTable.Columns("Opening").FormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Opening").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Debit_Amount").FormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Debit_Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Credit_Amount").FormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Credit_Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInValue
            Me.grdStock.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInValue


            Me.grdStock1.RootTable.Columns("Opening").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("Opening").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("ODebit").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("ODebit").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("OCredit").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("OCredit").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("Credit_Amount").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("Credit_Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("BDebit").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("BDebit").TotalFormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("BCredit").FormatString = "N" & DecimalPointInValue
            Me.grdStock1.RootTable.Columns("BCredit").TotalFormatString = "N" & DecimalPointInValue
            '''''''''''''''''''
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub grdStock_LinkClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdStock.LinkClicked
        Me.Cursor = Cursors.WaitCursor
        Try
            If Tracking = False Then
                If e.Column.Key = "detail_title" Then
                    grdStock_DoubleClick(Nothing, Nothing)
                End If
            Else
                If SubSubAccount = True Then
                    GetSubSubWiseDetailAccountsTrial(Me.grdStock.GetRow.Cells(0).Value, CostID)
                    SubSubAccount = False
                Else

                    If Not frmMain.Panel2.Controls.Contains(rptLedger) Then
                        frmMain.LoadControl("rptLedger")
                    End If
                    rptLedger.Tracking = True
                    rptLedger.Costid = CostID
                    rptLedger.NotesId = NoteId
                    rptLedger.Company = Company
                    rptLedger.CoaDetailId = Me.grdStock.GetRow.Cells(0).Value
                    rptLedger.cmbAccount.Value = Me.grdStock.GetRow.Cells(0).Value
                    rptLedger.dptFromDate = Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00")
                    rptLedger.dptToDate = Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59")
                    rptLedger.DateTimePicker1.Value = Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00")
                    rptLedger.DateTimePicker2.Value = Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59")
                    rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
                    rptLedger.GetLedger()
                    rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                    DrillDown = True
                    frmMain.LoadControl("rptLedger")
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub grdStock1_LinkClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdStock1.LinkClicked
        Me.Cursor = Cursors.WaitCursor
        Try
            If e.Column.Key = "detail_title" Then
                grdStock_DoubleClick(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub GetSubSubAccountsTrial()
        Try
            Dim fromDate As DateTime = CDate(Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
            Dim ToDate As DateTime = CDate(Me.DateTimePicker2.Value.Year & "-" & Me.DateTimePicker2.Value.Month & "-" & Me.DateTimePicker2.Value.Day & " 23:59:59")

            Dim strSql As String = String.Empty
            Me.cmbAcLevel.Text = "Sub Sub A/c"
            Me.cmbAccountFrom.Rows(0).Activate()
            Me.cmbAccountTo.Rows(0).Activate()
            strSql = "SELECT  DISTINCT  TOP 100 PERCENT dbo.vwCOADetail.main_sub_sub_id as coa_detail_id, dbo.vwCOADetail.sub_sub_code AS Code, dbo.vwCOADetail.sub_sub_title as detail_title, " _
                 & "  SUM(ISNULL(Opening.Opning, 0)) AS Opening, CASE WHEN SUM(ISNULL(Opening.Opning, 0)) >=0 THEN SUM(ISNULL(Opening.Opning, 0)) ELSE 0 End as ODebit, CASE WHEN SUM(ISNULL(Opening.Opning, 0)) < 0 THEN Abs(SUM(ISNULL(Opening.Opning, 0))) ELSE 0 End as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, " _
                 & " SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0))  " _
                 & " - SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Balance, CASE WHEN (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0 then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, Case When (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then Abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_sub_id as main_sub_id " _
                 & " FROM  dbo.vwCOADetail LEFT OUTER JOIN " _
                 & " (SELECT dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                 & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                 & " FROM dbo.tblVoucherDetail INNER JOIN " _
                 & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                 & " WHERE (Convert(Varchar,tblvoucher.voucher_date,102) BETWEEN Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & " " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And dbo.tblVoucherDetail.CostCenterID =" & Me.cmbCostCenter.Value & "") & " " _
                 & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                 & " (SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                 & " FROM dbo.tblVoucher V INNER JOIN " _
                 & " dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                 & " WHERE (Convert(varchar,V.voucher_date,102) < (Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102))  " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & "  ) " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And VD.CostCenterID =" & Me.cmbCostCenter.Value & "") & "  " _
                 & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
            strSql += " WHERE (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "
            If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                strSql = strSql + " AND dbo.vwCOADetail.sub_sub_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "'"
            End If
            If NoteId > 0 Then
                strSql += " AND vwCOADetail.crBS_Note_Id=" & NoteId & ""
            End If
            If PLNoteId > 0 Then
                strSql += " AND vwCOADetail.pl_note_id=" & PLNoteId & ""
            End If
            strSql = strSql + " GROUP BY dbo.vwCOADetail.main_sub_sub_id, dbo.vwCOADetail.sub_sub_code, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_id,  " _
                     & " dbo.vwCOADetail.sub_sub_title "
            strSql += "ORDER BY dbo.vwCOADetail.main_sub_sub_id "

            Me.ContextMenuStrip1.Items(0).Enabled = False

            If strSql.Length > 0 Then
                Dim adp As New OleDbDataAdapter
                Dim dt As New DataTable
                adp = New OleDbDataAdapter(strSql, Con)
                adp.Fill(dt)
                dt.AcceptChanges()
                Me.grdStock.DataSource = dt
                Me.grdStock1.DataSource = dt
                Me.grdStock.AutoSizeColumns()
                Me.grdStock1.AutoSizeColumns()
            End If
            strSql = String.Empty


        Catch ex As Exception
            Throw ex
        End Try
    End Sub





    Public Sub GetDetailAccountsTrial(Optional ByVal Condition As String = "")
        Try
            Dim fromDate As DateTime = CDate(Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
            Dim ToDate As DateTime = CDate(Me.DateTimePicker2.Value.Year & "-" & Me.DateTimePicker2.Value.Month & "-" & Me.DateTimePicker2.Value.Day & " 23:59:59")

            Dim strSql As String = String.Empty
            Me.cmbAcLevel.Text = "Detail A/c"
            'Me.cmbAccountFrom.Rows(0).Activate()
            'Me.cmbAccountTo.Rows(0).Activate()
            strSql = "SELECT TOP 100 PERCENT dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_code Code, dbo.vwCOADetail.detail_title, ISNULL(Opening.Opning, 0) AS Opening, Case when (ISNULL(Opening.Opning, 0)) >=0 then (ISNULL(Opening.Opning, 0)) else 0 end as ODebit, case when (ISNULL(Opening.Opning, 0)) < 0 then Abs((ISNULL(Opening.Opning, 0))) else 0 end as OCredit, " & _
                           "ISNULL(VoucherSum.Debit_Amount, 0) AS Debit_Amount, ISNULL(VoucherSum.Credit_Amount, 0) AS Credit_Amount,ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0) AS Balance, Case when ((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) >=0  then ((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when ((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then abs((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, dbo.vwCOADetail.account_type,  " & _
                           "dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.CityName " & _
                           "FROM  dbo.vwCOADetail LEFT OUTER JOIN " & _
                           "(SELECT  dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " & _
                           "ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " & _
                           "FROM dbo.tblVoucherDetail INNER JOIN " & _
                           "dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " & _
                           "WHERE  (Convert(Varchar,tblvoucher.voucher_date,102) BETWEEN Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & " " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And dbo.tblVoucherDetail.CostCenterID =" & Me.cmbCostCenter.Value & "") & " " & _
                           " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " & _
                           "(SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
                           "FROM   dbo.tblVoucher V INNER JOIN " & _
                           "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " & _
                           "WHERE (Convert(Varchar,V.voucher_date,102) < Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & " " & IIf(Me.cmbCostCenter.Value = 0 Or Me.cmbCostCenter.Value = Nothing, "", "And VD.CostCenterID =" & Me.cmbCostCenter.Value & "") & "  " & _
                           "GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
            strSql += " WHERE vwcoadetail.detail_title is not null AND Account_Type ='" & Condition & "'"
            If Me.chkZeroValue.Checked = True Then
                strSql += " AND (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "
            End If
            If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                strSql = strSql + " AND vwCOADetail.detail_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "' "
            End If
            'strSql += " AND dbo.vwCOADetail.main_sub_sub_id=" & Me.grdStock.GetRow.Cells("main_sub_id").Value
            strSql = strSql + " ORDER BY dbo.vwCOADetail.coa_detail_id "

            Me.ContextMenuStrip1.Items(0).Enabled = True

            If strSql.Length > 0 Then
                Dim adp As New OleDbDataAdapter
                Dim dt As New DataTable
                adp = New OleDbDataAdapter(strSql, Con)
                adp.Fill(dt)
                dt.AcceptChanges()
                Me.grdStock.DataSource = dt
                Me.grdStock1.DataSource = dt
                Me.grdStock.AutoSizeColumns()
                Me.grdStock1.AutoSizeColumns()
            End If
            strSql = String.Empty


        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged, DateTimePicker2.ValueChanged
        Try
            CtrlGrdBar1.Email = New SBModel.SendingEmail
            CtrlGrdBar1.Email.ToEmail = AdminEmail
            CtrlGrdBar1.Email.Subject = "Trial Balance: " + "(From " & Me.DateTimePicker1.Value.ToString("dd-MM-yyyy") & " To " & Me.DateTimePicker2.Value.ToString("dd-MM-yyyy") & ")"
            CtrlGrdBar1.Email.DocumentNo = "Trial Balance" 'Me.dtpAttendanceDate.Value.ToString("dd-MM-yyyy")
            CtrlGrdBar1.Email.DocumentDate = Date.Now.Date '"From" & Me.DateTimePicker1.Value.Date & " To" & Me.DateTimePicker2.Value.Date
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rbtDefaultBalance_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtDefaultBalance.CheckedChanged, rbtColumBalance.CheckedChanged
        Try

            Dim rbt As RadioButton = CType(sender, RadioButton)
            Select Case rbt.Name
                Case Me.rbtDefaultBalance.Name
                    Me.grdStock.Visible = True
                    Me.grdStock1.Visible = False
                    Me.CtrlGrdBar1.MyGrid = grdStock
                    Me.grdStock.BringToFront()
                Case Me.rbtColumBalance.Name
                    Me.grdStock.Visible = False
                    Me.grdStock1.Visible = True
                    Me.CtrlGrdBar1.MyGrid = grdStock1
                    Me.grdStock1.BringToFront()
            End Select

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub btnChart_Click(sender As Object, e As EventArgs) Handles btnChart.Click
    '    Try
    '        ApplyStyleSheet(frmRptCustomizeCharts)
    '        frmRptCustomizeCharts.cmbChartType.SelectedIndex = frmRptCustomizeCharts.enmChartType.Column
    '        frmRptCustomizeCharts._TopRecords = 5
    '        frmRptCustomizeCharts._grd = Me.grdStock
    '        frmRptCustomizeCharts._XValueMember = "detail_title"
    '        frmRptCustomizeCharts._YValueMember = "Balance"
    '        frmRptCustomizeCharts.Text = "Trial Balance"
    '        frmRptCustomizeCharts.ShowDialog()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub FillCombo(Optional ByVal condition As String = "")
        Dim Str As String = ""
        Try
            If condition = "CostCenter" Then
                Str = "If  exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") " _
                      & " Select CostCenterID, Name from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights  where UserID = " & LoginUserId & ") And Active = 1 ORDER BY 2 ASC " _
                      & " Else " _
                      & " Select CostCenterID, Name from tblDefCostCenter Where Active = 1 ORDER BY 2 ASC "
                FillUltraDropDown(Me.cmbCostCenter, Str)
                Me.cmbCostCenter.Rows(0).Activate()
                If Me.cmbCostCenter.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbCostCenter.DisplayLayout.Bands(0).Columns("CostCenterID").Hidden = True
                End If
            ElseIf condition = "Currency" Then
                Str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
                FillDropDown(Me.cmbCurrency, Str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetSubSubWiseDetailAccountsTrial(ByVal SubSubId As Integer, Optional ByVal CostCenterIds As String = "")
        Try
            Dim fromDate As DateTime = CDate(Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
            Dim ToDate As DateTime = CDate(Me.DateTimePicker2.Value.Year & "-" & Me.DateTimePicker2.Value.Month & "-" & Me.DateTimePicker2.Value.Day & " 23:59:59")
            CostID = CostCenterIds
            Dim strSql As String = String.Empty
            Me.cmbAcLevel.Text = "Detail A/c"
            Me.cmbAccountFrom.Rows(0).Activate()
            Me.cmbAccountTo.Rows(0).Activate()
            strSql = "SELECT TOP 100 PERCENT dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_code Code, dbo.vwCOADetail.detail_title, ISNULL(Opening.Opning, 0) AS Opening, Case when (ISNULL(Opening.Opning, 0)) >=0 then (ISNULL(Opening.Opning, 0)) else 0 end as ODebit, case when (ISNULL(Opening.Opning, 0)) < 0 then Abs((ISNULL(Opening.Opning, 0))) else 0 end as OCredit, " & _
                           "ISNULL(VoucherSum.Debit_Amount, 0) AS Debit_Amount, ISNULL(VoucherSum.Credit_Amount, 0) AS Credit_Amount,ISNULL(Opening.Opning, 0)  + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0) AS Balance, Case when ((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) >=0  then ((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when ((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then abs((ISNULL(Opening.Opning, 0)) + (ISNULL(VoucherSum.Debit_Amount, 0)) - (ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, dbo.vwCOADetail.account_type,  " & _
                           "dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.CityName " & _
                           "FROM  dbo.vwCOADetail LEFT OUTER JOIN " & _
                           "(SELECT  dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " & _
                           "ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " & _
                           "FROM dbo.tblVoucherDetail INNER JOIN " & _
                           "dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " & _
                           "WHERE  (Convert(Varchar,tblvoucher.voucher_date,102) BETWEEN Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & " " & IIf(CostCenterIds = "" Or CostCenterIds Is Nothing, "", "And isnull(dbo.tblVoucherDetail.CostCenterID, 0) IN (" & CostCenterIds & ")") & "  " & IIf(Company = 0, "", "And isnull(tblVoucher.location_id, 0) = " & Company) & "  " & _
                           " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " & _
                           "(SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
                           "FROM   dbo.tblVoucher V INNER JOIN " & _
                           "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " & _
                           "WHERE (Convert(Varchar,V.voucher_date,102) < Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & " " & IIf(CostCenterIds = "" Or CostCenterIds Is Nothing, "", "And isnull(VD.CostCenterID, 0) IN (" & CostCenterIds & ")") & "  " & IIf(Company = 0, "", "And isnull(V.location_id, 0) = " & Company) & " " & _
                           "GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
            strSql += " WHERE vwcoadetail.detail_title is not null "
            If Me.chkZeroValue.Checked = True Then
                strSql += " AND (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "
            End If
            If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                strSql = strSql + " AND vwCOADetail.detail_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "' "
            End If
            strSql += " AND dbo.vwCOADetail.main_sub_sub_id=" & SubSubId
            strSql = strSql + " ORDER BY dbo.vwCOADetail.coa_detail_id "

            Me.ContextMenuStrip1.Items(0).Enabled = True

            If strSql.Length > 0 Then
                Dim adp As New OleDbDataAdapter
                Dim dt As New DataTable
                adp = New OleDbDataAdapter(strSql, Con)
                adp.Fill(dt)
                dt.AcceptChanges()
                Me.grdStock.DataSource = dt
                Me.grdStock1.DataSource = dt
                Me.grdStock.AutoSizeColumns()
                Me.grdStock1.AutoSizeColumns()
            End If
            strSql = String.Empty


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetSubSubWiseSubSubAccountsTrial(ByVal SubId As Integer, Optional ByVal CostCenterIds As String = "")
        Try
            Dim fromDate As DateTime = CDate(Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
            Dim ToDate As DateTime = CDate(Me.DateTimePicker2.Value.Year & "-" & Me.DateTimePicker2.Value.Month & "-" & Me.DateTimePicker2.Value.Day & " 23:59:59")
            CostID = CostCenterIds
            Dim strSql As String = String.Empty
            Me.cmbAcLevel.Text = "Sub Sub A/c"
            Me.cmbAccountFrom.Rows(0).Activate()
            Me.cmbAccountTo.Rows(0).Activate()
            strSql = "SELECT  DISTINCT   TOP 100 PERCENT dbo.vwCOADetail.main_sub_sub_id as coa_detail_id, dbo.vwCOADetail.sub_sub_code AS Code, dbo.vwCOADetail.sub_sub_title as detail_title, " _
                     & "  SUM(ISNULL(Opening.Opning, 0)) AS Opening, Case when SUM(ISNULL(Opening.Opning, 0)) >=0 then SUM(ISNULL(Opening.Opning, 0)) else 0 end as ODebit, case when SUM(ISNULL(Opening.Opning, 0)) < 0 then Abs(SUM(ISNULL(Opening.Opning, 0))) else 0 end as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, " _
                     & " SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0))  " _
                     & " - SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Balance, Case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0  then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_sub_id as main_sub_id " _
                     & " FROM         dbo.vwCOADetail LEFT OUTER JOIN " _
                     & " (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                     & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                     & " FROM          dbo.tblVoucherDetail INNER JOIN " _
                     & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                     & " WHERE      (Convert(Varchar,tblvoucher.voucher_date,102) BETWEEN Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & " " & IIf(CostCenterIds = "" Or CostCenterIds Is Nothing Or CostCenterIds = "0", "", "And dbo.tblVoucherDetail.CostCenterID IN (" & CostCenterIds & ")") & " " _
                     & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                     & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                     & " FROM          dbo.tblVoucher V INNER JOIN " _
                     & " dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                     & " WHERE      (Convert(Varchar,V.voucher_date,102) < Convert(Datetime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & " " & IIf(CostCenterIds = "" Or CostCenterIds Is Nothing Or CostCenterIds = "0", "", "And VD.CostCenterID IN (" & CostCenterIds & ")") & "  " _
                     & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
            strSql += " WHERE vwcoadetail.detail_title is not null "
            If Me.chkZeroValue.Checked = True Then
                strSql += " AND (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "
            End If
            If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                strSql = strSql + " AND dbo.vwCOADetail.sub_sub_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "'"
            End If
            strSql += " AND dbo.vwCOADetail.main_sub_id =" & SubId
            strSql = strSql + " GROUP BY dbo.vwCOADetail.main_sub_sub_id, dbo.vwCOADetail.sub_sub_code, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_id,  " _
                     & " dbo.vwCOADetail.sub_sub_title "
            strSql += "ORDER BY dbo.vwCOADetail.main_sub_sub_id "

            Me.ContextMenuStrip1.Items(0).Enabled = True

            If strSql.Length > 0 Then
                Dim adp As New OleDbDataAdapter
                Dim dt As New DataTable
                adp = New OleDbDataAdapter(strSql, Con)
                adp.Fill(dt)
                dt.AcceptChanges()
                Me.grdStock.DataSource = dt
                Me.grdStock1.DataSource = dt
                Me.grdStock.AutoSizeColumns()
                Me.grdStock1.AutoSizeColumns()
            End If
            strSql = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub GetSubSubWiseSubSubAccounts(ByVal SubId As Integer, ByVal NotesId As Integer, Optional ByVal CostCenterIds As String = "")
        Try
            Dim fromDate As DateTime = CDate(Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
            Dim ToDate As DateTime = CDate(Me.DateTimePicker2.Value.Year & "-" & Me.DateTimePicker2.Value.Month & "-" & Me.DateTimePicker2.Value.Day & " 23:59:59")
            CostID = CostCenterIds
            Dim strSql As String = String.Empty
            Me.cmbAcLevel.Text = "Sub Sub A/c"
            Me.cmbAccountFrom.Rows(0).Activate()
            Me.cmbAccountTo.Rows(0).Activate()
            NoteId = NotesId
            strSql = "SELECT  DISTINCT   TOP 100 PERCENT dbo.vwCOADetail.main_sub_sub_id as coa_detail_id, dbo.vwCOADetail.sub_sub_code AS Code, dbo.vwCOADetail.sub_sub_title as detail_title, " _
                     & "  SUM(ISNULL(Opening.Opning, 0)) AS Opening, Case when SUM(ISNULL(Opening.Opning, 0)) >=0 then SUM(ISNULL(Opening.Opning, 0)) else 0 end as ODebit, case when SUM(ISNULL(Opening.Opning, 0)) < 0 then Abs(SUM(ISNULL(Opening.Opning, 0))) else 0 end as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, " _
                     & " SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0))  " _
                     & " - SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Balance, Case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0  then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_sub_id as main_sub_id " _
                     & " FROM         dbo.vwCOADetail LEFT OUTER JOIN " _
                     & " (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                     & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                     & " FROM          dbo.tblVoucherDetail INNER JOIN " _
                     & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                     & " WHERE (Convert(Varchar,tblvoucher.voucher_date,102) BETWEEN Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & " " & IIf(CostCenterIds = "" Or CostCenterIds Is Nothing, "", "And ISNULL(dbo.tblVoucherDetail.CostCenterID, 0) IN (" & CostCenterIds & ")") & " " _
                     & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                     & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                     & " FROM          dbo.tblVoucher V INNER JOIN " _
                     & " dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                     & " WHERE (Convert(Varchar,V.voucher_date,102) < Convert(Datetime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & " " & IIf(CostCenterIds = "" Or CostCenterIds Is Nothing, "", "And ISNULL(VD.CostCenterID, 0) IN (" & CostCenterIds & ")") & "  " _
                     & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
            strSql += " WHERE vwCOADetail.DrBS_Note_id = " & NotesId & " AND vwcoadetail.detail_title is not null "
            If Me.chkZeroValue.Checked = True Then
                strSql += " AND (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "
            End If
            If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                strSql = strSql + " AND dbo.vwCOADetail.sub_sub_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "'"
            End If
            strSql += " AND dbo.vwCOADetail.main_sub_id =" & SubId
            strSql = strSql + " GROUP BY dbo.vwCOADetail.main_sub_sub_id, dbo.vwCOADetail.sub_sub_code, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_id,  " _
                     & " dbo.vwCOADetail.sub_sub_title "
            strSql += "ORDER BY dbo.vwCOADetail.main_sub_sub_id "

            Me.ContextMenuStrip1.Items(0).Enabled = True

            If strSql.Length > 0 Then
                Dim adp As New OleDbDataAdapter
                Dim dt As New DataTable
                adp = New OleDbDataAdapter(strSql, Con)
                adp.Fill(dt)
                dt.AcceptChanges()
                Me.grdStock.DataSource = dt
                Me.grdStock1.DataSource = dt
                Me.grdStock.AutoSizeColumns()
                Me.grdStock1.AutoSizeColumns()
            End If
            strSql = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub GetSubSubWiseDetailAccounts(ByVal SubSubId As Integer, ByVal NotesId As Integer, Optional ByVal CostCenterIds As String = "")
        Try
            Dim fromDate As DateTime = CDate(Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
            Dim ToDate As DateTime = CDate(Me.DateTimePicker2.Value.Year & "-" & Me.DateTimePicker2.Value.Month & "-" & Me.DateTimePicker2.Value.Day & " 23:59:59")
            CostID = CostCenterIds
            Dim strSql As String = String.Empty
            Me.cmbAcLevel.Text = "Detail A/c"
            Me.cmbAccountFrom.Rows(0).Activate()
            Me.cmbAccountTo.Rows(0).Activate()
            NoteId = NotesId
            strSql = "SELECT  DISTINCT   TOP 100 PERCENT dbo.vwCOADetail.coa_detail_id as coa_detail_id, dbo.vwCOADetail.detail_code AS Code, dbo.vwCOADetail.detail_title as detail_title, " _
                     & "  SUM(ISNULL(Opening.Opning, 0)) AS Opening, Case when SUM(ISNULL(Opening.Opning, 0)) >=0 then SUM(ISNULL(Opening.Opning, 0)) else 0 end as ODebit, case when SUM(ISNULL(Opening.Opning, 0)) < 0 then Abs(SUM(ISNULL(Opening.Opning, 0))) else 0 end as OCredit, SUM(ISNULL(VoucherSum.Debit_Amount, 0)) AS Debit_Amount, " _
                     & " SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Credit_Amount, SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0))  " _
                     & " - SUM(ISNULL(VoucherSum.Credit_Amount, 0)) AS Balance, Case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) >=0  then (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BDebit, case when (SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) < 0 then abs(SUM(ISNULL(Opening.Opning, 0)) + SUM(ISNULL(VoucherSum.Debit_Amount, 0)) - SUM(ISNULL(VoucherSum.Credit_Amount, 0))) else 0 end as BCredit, dbo.vwCOADetail.account_type, dbo.vwCOADetail.coa_detail_id as main_sub_id " _
                     & " FROM         dbo.vwCOADetail LEFT OUTER JOIN " _
                     & " (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount), 0) AS Debit_Amount,  " _
                     & " ISNULL(SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Credit_Amount " _
                     & " FROM          dbo.tblVoucherDetail INNER JOIN " _
                     & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " _
                     & " WHERE (Convert(Varchar,tblvoucher.voucher_date,102) BETWEEN Convert(DateTime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND tblvoucher.Post=1", "") & " " & IIf(CostCenterIds = "" Or CostCenterIds Is Nothing, "", "And ISNULL(dbo.tblVoucherDetail.CostCenterID, 0) IN (" & CostCenterIds & ")") & " " _
                     & " GROUP BY dbo.tblVoucherDetail.coa_detail_id) VoucherSum ON dbo.vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                     & " (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " _
                     & " FROM          dbo.tblVoucher V INNER JOIN " _
                     & " dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                     & " WHERE (Convert(Varchar,V.voucher_date,102) < Convert(Datetime,'" & fromDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.chkIncludeUnPostedVouchers.Checked = False, " AND V.Post=1", "") & " " & IIf(CostCenterIds = "" Or CostCenterIds Is Nothing, "", "And ISNULL(VD.CostCenterID, 0) IN (" & CostCenterIds & ")") & "  " _
                     & " GROUP BY VD.coa_detail_id) Opening ON dbo.vwCOADetail.coa_detail_id = Opening.coa_detail_id "
            strSql += " WHERE vwCOADetail.DrBS_Note_id = " & NotesId & " AND vwcoadetail.detail_title is not null "
            If Me.chkZeroValue.Checked = True Then
                strSql += " AND (ISNULL(Opening.Opning, 0) <> 0 Or ISNULL(VoucherSum.Debit_Amount, 0) <> 0 Or ISNULL(VoucherSum.Credit_Amount, 0) <> 0) "
            End If
            If Me.cmbAccountFrom.ActiveRow.Cells(0).Value > 0 Then
                strSql = strSql + " AND dbo.vwCOADetail.sub_sub_code between '" & Me.cmbAccountFrom.Text & "' and '" & Me.cmbAccountTo.Text & "'"
            End If
            strSql += " AND dbo.vwCOADetail.main_sub_sub_id =" & SubSubId
            strSql = strSql + " GROUP BY dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.account_type, dbo.vwCOADetail.main_sub_id,  " _
                     & " dbo.vwCOADetail.detail_title "
            strSql += "ORDER BY dbo.vwCOADetail.coa_detail_id "

            Me.ContextMenuStrip1.Items(0).Enabled = True

            If strSql.Length > 0 Then
                Dim adp As New OleDbDataAdapter
                Dim dt As New DataTable
                adp = New OleDbDataAdapter(strSql, Con)
                adp.Fill(dt)
                dt.AcceptChanges()
                Me.grdStock.DataSource = dt
                Me.grdStock1.DataSource = dt
                Me.grdStock.AutoSizeColumns()
                Me.grdStock1.AutoSizeColumns()
            End If
            strSql = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class