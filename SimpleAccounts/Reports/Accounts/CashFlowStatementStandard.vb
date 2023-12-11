'' TASK: TFS1081 Inclusion of Cost Center Group filter to Cash Flow Statement.  If a group is selected then data should be loaded of all cost center of a such group. Dated 21-07-2017 by Ameen

Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Public Class CashFlowStatementStandard
    Public flgCompanyRights As Boolean = False
    Dim CostCenterCriterial As String = ""
    Dim CostCenterCriterial1 As String = ""

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            GetCrystalReportRights()
            Me.CallShowReport()
        Catch Ex As Exception
            ShowErrorMessage(Ex.Message)
        End Try
    End Sub
    Public Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.Button2.Enabled = True
                Me.Button1.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.Button2.Enabled = False
            Me.Button1.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Button2.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    Button1.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CallShowReport(Optional ByVal Print As Boolean = False)
        Try
            ''Start TFS1081 
            If Me.cmbCSGroup.SelectedIndex > 0 AndAlso cmbCostCenter.SelectedIndex > 0 Then
                CostCenterCriterial = " And tblVoucherDetail.CostCenterId = " & Me.cmbCostCenter.SelectedValue & ""
                CostCenterCriterial1 = " And tblVoucherDetail_1.CostCenterId = " & Me.cmbCostCenter.SelectedValue & ""
            ElseIf Me.cmbCSGroup.SelectedIndex > 0 AndAlso cmbCostCenter.SelectedIndex <= 0 Then
                CostCenterCriterial = " And tblVoucherDetail.CostCenterId In(Select CostCenterID From tbldefCostCenter Where CostCenterGroup='" & Me.cmbCSGroup.Text & "')"
                CostCenterCriterial1 = " And tblVoucherDetail_1.CostCenterId In(Select CostCenterID From tbldefCostCenter Where CostCenterGroup='" & Me.cmbCSGroup.Text & "')"
            ElseIf Me.cmbCSGroup.SelectedIndex <= 0 AndAlso cmbCostCenter.SelectedIndex > 0 Then
                CostCenterCriterial = " And tblVoucherDetail.CostCenterId = " & Me.cmbCostCenter.SelectedValue & ""
                CostCenterCriterial1 = " And tblVoucherDetail_1.CostCenterId = " & Me.cmbCostCenter.SelectedValue & ""
            ElseIf Me.cmbCSGroup.SelectedIndex <= 0 AndAlso cmbCostCenter.SelectedIndex <= 0 Then
                CostCenterCriterial = ""
                CostCenterCriterial1 = ""
            End If
            '' END  TFS1081 
            Me.Cursor = Cursors.WaitCursor
            Dim fromDate As String = Me.DateTimePicker1.Value.Year & "," & Me.DateTimePicker1.Value.Month & "," & Me.DateTimePicker1.Value.Day & ",0,0,0"
            Dim ToDate As String = Me.DateTimePicker2.Value.Year & "," & Me.DateTimePicker2.Value.Month & "," & Me.DateTimePicker2.Value.Day & ",23,59,59"

            If Me.cbExcludeTax.Checked = False Then
                Me.FunAddReportCriteria()
            Else
                Me.FunAddReportCriteriaExcludeTax()
            End If
            str_ReportParam = "fromdate|" & Me.DateTimePicker1.Value.ToString("dd/MMM/yyyy") & "&" & "todate|" & Me.DateTimePicker2.Value.ToString("dd/MMM/yyyy")
            AddRptParam("CostCenter", IIf(Me.cmbCostCenter.SelectedValue > 0, Me.cmbCostCenter.Text, " "))
            ShowReport("rptCashFlowStatmentStander", , , , , , , , , , , , , "Profit And Loss", "Date Form " & Me.DateTimePicker1.Value.ToString("dd/MMM/yyyy") & " Date To " & Me.DateTimePicker2.Value.ToString("dd/MMM/yyyy") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            CallShowReport(True)
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
    Private Sub CashFlowStatmentStandard_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If
            Me.pnlCost.Visible = True
            Me.chkUnposted.Visible = True
            Me.cmbCostCenter.Visible = True
            Me.lblCostCenter.Visible = True
            Me.chkIncludeCheque.Visible = True
            Me.cbExcludeTax.Visible = True
            FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            FillDropDown(Me.cmbCSGroup, "Select distinct CostCenterGroup, CostCenterGroup As [Cost Center Group] from tbldefCostCenter Where CostCenterGroup <> ''", True)
            Me.cmbPeriod.SelectedIndex = 3
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function FunAddReportCriteria() As String
        Dim strSql As String
        Dim strCondAccount As String = String.Empty
        Dim strYearCriteria As String
        Dim strLocationCriteria As String

        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con


        strYearCriteria = " ( dbo.tblVoucher.Voucher_no <> '000000' ) AND  "
        strLocationCriteria = "  "

        Dim strPostCriteria As String = String.Empty
        Dim strOther_Voucher_Criteria As String = String.Empty
        Dim intlocation_id As Integer

        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check
        'If chkunposted.Value = vbUnchecked Then

        '    strPostCriteria = "  (tblVoucher.post = 1) AND "
        'Else

        '    strPostCriteria = ""
        'End If

        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check


        strOther_Voucher_Criteria = ""

        '   get the location id

        intlocation_id = 0

        Dim ReceiptType As String
        Dim PaymentType As String
        Dim AccType As String



        ReceiptType = "'BR', 'CR'"
        PaymentType = "'BP', 'CP'"
        'AccType = "'Cash','Bank'"

        If rdoCash.Checked = True Then
            AccType = "'Cash'"
        ElseIf rdoBank.Checked = True Then
            AccType = "'Bank'"
        Else
            AccType = "'Cash','Bank'"
        End If

        strSql = "SELECT SUM(credit_amount)-SUM(debit_amount) from ("
        strSql = strSql & "SELECT     dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
                         "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
                         "                      dbo.tblVoucherDetail.credit_amount, dbo.tblVoucher.post " & _
                         "FROM         dbo.tblVoucher INNER JOIN " & _
                         "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(Me.DateTimePicker1.MinDate, "yyyy-M-d 00:00:00") & "', 102)  AND Convert(DateTime, '" & Format(Me.DateTimePicker1.Value.Date.AddDays(-1), "yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & " AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ")  AND (dbo.tblVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblDefVoucherType " & _
                         "                            WHERE      gl_type IN (" & PaymentType & "))) AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0) > 0)  " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "))      " & _
                         "Union " & _
                         "SELECT     dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
                         "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount , " & _
                         "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
                         "FROM         dbo.tblVoucher INNER JOIN " & _
                         "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(DateTimePicker1.MinDate, "yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Format(DateTimePicker1.Value.AddDays(-1), "yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & " AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") AND (dbo.tblVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblDefVoucherType " & _
                         "                            WHERE      gl_type IN (" & ReceiptType & "))) AND (ISNULL(dbo.tblVoucherDetail.credit_amount, 0) > 0)   " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "))   "

        strSql = strSql & ")tblOpeningBalance"


        'dblCashBankOpening = Val(UtilityDAL.ReturnDataRow(strSql).Item(0).ToString)


        strSql = "Alter View vwCashFlowPeriodRPT As "

        'strSql = strSql & "SELECT  0 AS Tr_Type ,   dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
        '                 "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
        '                 "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
        '                 "FROM         dbo.tblVoucher INNER JOIN " & _
        '                 "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
        '                 "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
        '                 "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
        '                 "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(DateTimePicker1.Value, "yyyy/MM/dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy/MM/dd") & "') AND (dbo.tblVoucher.voucher_type_id IN " & _
        '                 "                          (SELECT     voucher_Type_id " & _
        '                 "                            From tblDefVoucherType " & _
        '                 "                            WHERE      gl_type IN (" & PaymentType & "))) AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0) > 0) " & _
        '                 "Union " & _
        '                 "SELECT    1 AS Tr_Type , dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
        '                 "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
        '                 "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
        '                 "FROM         dbo.tblVoucher INNER JOIN " & _
        '                 "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
        '                 "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
        '                 "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
        '                 "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(DateTimePicker1.Value, "yyyy/MM/dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy/MM/dd") & "') AND (dbo.tblVoucher.voucher_type_id IN " & _
        '                 "                          (SELECT     voucher_Type_id " & _
        '                 "                            From tblDefVoucherType " & _
        '                 "                            WHERE      gl_type IN (" & ReceiptType & "))) AND (ISNULL(dbo.tblVoucherDetail.credit_amount, 0) > 0) "
        '' Below query is commented on 20-07-2017 to add filter of Cost Center Group.
        'strSql = strSql & "SELECT     CASE WHEN (dbo.tblVoucherDetail.credit_amount > 0) THEN 1 ELSE 0 END AS tr_type, dbo.tblVoucherDetail.coa_detail_id, " _
        '                & " dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, dbo.tblVoucherDetail.comments, " _
        '                & " dbo.vwCOADetail.detail_title, dbo.tblVoucherDetail.debit_amount, dbo.tblVoucherDetail.credit_amount, dbo.tblVoucher.post " _
        '                & " FROM         dbo.tblVoucherDetail INNER JOIN " _
        '                & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
        '                & " dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
        '                & " dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id " _
        '                & " WHERE     (dbo.tblVoucher.voucher_id IN " _
        '                & " (SELECT DISTINCT tblVoucher_1.voucher_id " _
        '                & " FROM          dbo.tblVoucherDetail AS tblVoucherDetail_1 INNER JOIN" _
        '                & " dbo.tblVoucher AS tblVoucher_1 ON tblVoucherDetail_1.voucher_id = tblVoucher_1.voucher_id INNER JOIN " _
        '                & " dbo.tblDefVoucherType AS tblDefVoucherType_1 ON  " _
        '                & " tblVoucher_1.voucher_type_id = tblDefVoucherType_1.voucher_type_id INNER JOIN " _
        '                & " dbo.vwCOADetail AS vwCOADetail_1 ON tblVoucherDetail_1.coa_detail_id = vwCOADetail_1.coa_detail_id " _
        '                & " WHERE      (vwCOADetail_1.account_type in( " & AccType & ")) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail_1.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ")) AND (dbo.vwCOADetail.account_type NOT IN ( " & AccType & "))" _
        '                & " and " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & " " _
        '                & " (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(Me.DateTimePicker1.Value.Date, "yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Format(DateTimePicker2.Value, "yyyy-M-d 23:59:59") & "',102)) AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0)-ISNULL(dbo.tblVoucherDetail.credit_amount, 0) <> 0) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & " "
        'cm.CommandText = strSql
        'cm.ExecuteNonQuery()

        '' Start TFS1081 added cost centre group filter.
        strSql = strSql & "SELECT     CASE WHEN (dbo.tblVoucherDetail.credit_amount > 0) THEN 1 ELSE 0 END AS tr_type, dbo.tblVoucherDetail.coa_detail_id, " _
                        & " dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, dbo.tblVoucherDetail.comments, " _
                        & " dbo.vwCOADetail.detail_title, dbo.tblVoucherDetail.debit_amount, dbo.tblVoucherDetail.credit_amount, dbo.tblVoucher.post " _
                        & " FROM         dbo.tblVoucherDetail INNER JOIN " _
                        & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
                        & " dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
                        & " dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id " _
                        & " WHERE     (dbo.tblVoucher.voucher_id IN " _
                        & " (SELECT DISTINCT tblVoucher_1.voucher_id " _
                        & " FROM          dbo.tblVoucherDetail AS tblVoucherDetail_1 INNER JOIN" _
                        & " dbo.tblVoucher AS tblVoucher_1 ON tblVoucherDetail_1.voucher_id = tblVoucher_1.voucher_id INNER JOIN " _
                        & " dbo.tblDefVoucherType AS tblDefVoucherType_1 ON  " _
                        & " tblVoucher_1.voucher_type_id = tblDefVoucherType_1.voucher_type_id INNER JOIN " _
                        & " dbo.vwCOADetail AS vwCOADetail_1 ON tblVoucherDetail_1.coa_detail_id = vwCOADetail_1.coa_detail_id " _
                      & " WHERE      (vwCOADetail_1.account_type in( " & AccType & ")) " & CostCenterCriterial1 & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ")) AND (dbo.vwCOADetail.account_type NOT IN ( " & AccType & "))" _
                        & " and " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & " " _
                      & " (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(Me.DateTimePicker1.Value.Date, "yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Format(DateTimePicker2.Value, "yyyy-M-d 23:59:59") & "',102)) AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0)-ISNULL(dbo.tblVoucherDetail.credit_amount, 0) <> 0) " & CostCenterCriterial & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & " "
        cm.CommandText = strSql
        cm.ExecuteNonQuery()


        'Dim ObjDAL As New DAL.RptCashFlowDal

        'If ObjDAL.InsertDataForReport("Stander") Then

        strSql = " truncate table TblrptCashFlowStander "
        cm.CommandText = strSql
        cm.ExecuteNonQuery()

        strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post ) " & _
                      "Select Tr_Type ,Tr_Type + 1, coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post  from vwCashFlowPeriodRPT WHERE (IsNull(debit_amount,0) <> 0 Or IsNull(credit_amount,0) <> 0)"

        cm.CommandText = strSql
        cm.ExecuteNonQuery()

        If 1 = 1 Then
            'strSql = "SELECT     tblCOAMainSubSubDetail.coa_detail_id, tblCOAMainSubSubDetail.detail_title, dbo.tblCOAMainSubSub.account_type " & _
            '                     "FROM         dbo.tblCOAMainSubSubDetail AS tblCOAMainSubSubDetail INNER JOIN " & _
            '                     "dbo.tblCOAMainSubSub ON tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " & _
            '                     "WHERE     (dbo.tblCOAMainSubSub.account_type IN ( " & AccType & "))"

            'Dim dt As DataTable
            'dt = UtilityDAL.GetDataTable(strSql).Copy

            Dim ilocation_id As Integer

            ' Get Location ID .. 
            ilocation_id = 0



            '//Preparing Query string to insert opening balance
            ''Below query is commented on 20-07-2017
            'strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
            '            " SELECT     1 AS tr_Type, 0 AS Sort, coa_detail_id, '" & Me.DateTimePicker1.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Opening Balance' AS Comments, detail_title, 0 AS Credit, OpeningBalance, " & _
            '            " 1 AS Post " & _
            '            " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
            '            " AS OpeningBalance " & _
            '            " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount)-SUM(dbo.tblVoucherDetail.credit_amount), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (Convert(Varchar, dbo.tblVoucher.voucher_date,102) < Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "  GROUP BY dbo.tblVoucherDetail.coa_detail_id Having  ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) <> 0)as tmpTblAccountsOpening  INNER JOIN " & _
            '            " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
            '            " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
            '            " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            'values(" & _
            '                             "1 ,0, " & dtRow.Item(0).ToString & " ,'" & Format(Me.DateTimePicker1.Value.Date.AddDays(-1), "yyyy/MM/dd") & "' ,'' ,'Opening Balance' ,'" & dtRow.Item(1).ToString & "' ," & dblCashBankOpening & " ,0 ,'True')"
            '' Start TFS1081 added cost centre group filter.
            strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
                        " SELECT     1 AS tr_Type, 0 AS Sort, coa_detail_id, '" & Me.DateTimePicker1.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Opening Balance' AS Comments, detail_title, 0 AS Credit, OpeningBalance, " & _
                        " 1 AS Post " & _
                        " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
                        " AS OpeningBalance " & _
                      " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount)-SUM(dbo.tblVoucherDetail.credit_amount), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (Convert(Varchar, dbo.tblVoucher.voucher_date,102) < Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") " & CostCenterCriterial & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "  GROUP BY dbo.tblVoucherDetail.coa_detail_id Having  ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) <> 0)as tmpTblAccountsOpening  INNER JOIN " & _
                        " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                        " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
                        " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            '//Inserting Opening Balance
            cm.CommandText = strSql
            cm.ExecuteNonQuery()


            ''//Preparing Query string to insert opening balance

            '//Preparing Query string to insert Closing balance

            ''Below query is commented on 20-07-2017
            'strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
            '            " SELECT     0 AS tr_Type, 3 AS Sort, coa_detail_id, '" & Me.DateTimePicker2.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Closing Balance' AS Comments, detail_title, OpeningBalance,  0 AS Credit," & _
            '            " 1 AS Post " & _
            '            " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
            '            " AS OpeningBalance " & _
            '            " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) <= Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 00:00:00") & "', 102))  AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "     GROUP BY dbo.tblVoucherDetail.coa_detail_id Having ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) <> 0)as tmpTblAccountsOpening  INNER JOIN " & _
            '            " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
            '            " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
            '            " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            '' Start TFS1081 added cost centre group filter.
            strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
                        " SELECT     0 AS tr_Type, 3 AS Sort, coa_detail_id, '" & Me.DateTimePicker2.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Closing Balance' AS Comments, detail_title, OpeningBalance,  0 AS Credit," & _
                        " 1 AS Post " & _
                        " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
                        " AS OpeningBalance " & _
                       " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) <= Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 00:00:00") & "', 102))  AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") " & CostCenterCriterial & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "     GROUP BY dbo.tblVoucherDetail.coa_detail_id Having ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) <> 0)as tmpTblAccountsOpening  INNER JOIN " & _
                        " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                        " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
                        " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            'values(" & _
            '                             "1 ,0, " & dtRow.Item(0).ToString & " ,'" & Format(Me.DateTimePicker1.Value.Date.AddDays(-1), "yyyy/MM/dd") & "' ,'' ,'Opening Balance' ,'" & dtRow.Item(1).ToString & "' ," & dblCashBankOpening & " ,0 ,'True')"
            '//Inserting Closing Balance

            cm.CommandText = strSql
            cm.ExecuteNonQuery()

        Else
        End If

        Return ""

    End Function
    Public Function FunAddReportCriteriaExcludeTax() As String
        Dim strSql As String
        Dim strCondAccount As String = String.Empty
        Dim strYearCriteria As String
        Dim strLocationCriteria As String

        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con


        strYearCriteria = " ( dbo.tblVoucher.Voucher_no <> '000000' ) AND  "
        strLocationCriteria = "  "

        Dim strPostCriteria As String = String.Empty
        Dim strOther_Voucher_Criteria As String = String.Empty
        Dim intlocation_id As Integer

        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check
        'If chkunposted.Value = vbUnchecked Then

        '    strPostCriteria = "  (tblVoucher.post = 1) AND "
        'Else

        '    strPostCriteria = ""
        'End If

        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check


        strOther_Voucher_Criteria = ""

        '   get the location id

        intlocation_id = 0

        Dim ReceiptType As String
        Dim PaymentType As String
        Dim AccType As String



        ReceiptType = "'BR', 'CR'"
        PaymentType = "'BP', 'CP'"
        'AccType = "'Cash','Bank'"

        If rdoCash.Checked = True Then
            AccType = "'Cash'"
        ElseIf rdoBank.Checked = True Then
            AccType = "'Bank'"
        Else
            AccType = "'Cash','Bank'"
        End If



        strSql = "SELECT SUM(credit_amount)-SUM(debit_amount) from ("
        strSql = strSql & "SELECT     dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
                         "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, (IsNull(dbo.tblVoucherDetail.debit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) As debit_amount, " & _
                         "                      (IsNull(dbo.tblVoucherDetail.credit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) AS credit_amount  , dbo.tblVoucher.post " & _
                         "FROM         dbo.tblVoucher INNER JOIN " & _
                         "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(Me.DateTimePicker1.MinDate, "yyyy-M-d 00:00:00") & "', 102)  AND Convert(DateTime, '" & Format(Me.DateTimePicker1.Value.Date.AddDays(-1), "yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & " AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ")  AND (dbo.tblVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblDefVoucherType " & _
                         "                            WHERE      gl_type IN (" & PaymentType & "))) AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0) > 0)  " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "))      " & _
                         "Union " & _
                         "SELECT     dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
                         "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, (IsNull(dbo.tblVoucherDetail.debit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) As debit_amount , " & _
                         "                      (IsNull(dbo.tblVoucherDetail.credit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) As credit_amount , dbo.tblVoucher.post " & _
                         "FROM         dbo.tblVoucher INNER JOIN " & _
                         "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
                         "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
                         "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
                         "                      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(DateTimePicker1.MinDate, "yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Format(DateTimePicker1.Value.AddDays(-1), "yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & " AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") AND (dbo.tblVoucher.voucher_type_id IN " & _
                         "                          (SELECT     voucher_Type_id " & _
                         "                            From tblDefVoucherType " & _
                         "                            WHERE      gl_type IN (" & ReceiptType & "))) AND (ISNULL(dbo.tblVoucherDetail.credit_amount, 0) > 0)   " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "))   "

        strSql = strSql & ")tblOpeningBalance"


        'dblCashBankOpening = Val(UtilityDAL.ReturnDataRow(strSql).Item(0).ToString)


        strSql = "Alter View vwCashFlowPeriodRPT As "

        'strSql = strSql & "SELECT  0 AS Tr_Type ,   dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
        '                 "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
        '                 "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
        '                 "FROM         dbo.tblVoucher INNER JOIN " & _
        '                 "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
        '                 "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
        '                 "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
        '                 "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(DateTimePicker1.Value, "yyyy/MM/dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy/MM/dd") & "') AND (dbo.tblVoucher.voucher_type_id IN " & _
        '                 "                          (SELECT     voucher_Type_id " & _
        '                 "                            From tblDefVoucherType " & _
        '                 "                            WHERE      gl_type IN (" & PaymentType & "))) AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0) > 0) " & _
        '                 "Union " & _
        '                 "SELECT    1 AS Tr_Type , dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, " & _
        '                 "                      dbo.tblVoucherDetail.comments, dbo.tblCOAMainSubSubDetail.detail_title, dbo.tblVoucherDetail.debit_amount, " & _
        '                 "                      dbo.tblVoucherDetail.credit_amount , dbo.tblVoucher.post " & _
        '                 "FROM         dbo.tblVoucher INNER JOIN " & _
        '                 "                      dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN " & _
        '                 "                      dbo.tblCOAMainSubSubDetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.tblCOAMainSubSubDetail.coa_detail_id " & _
        '                 "WHERE  " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & "  " & _
        '                 "                      (dbo.tblVoucher.voucher_date BETWEEN '" & Format(DateTimePicker1.Value, "yyyy/MM/dd") & "' AND '" & Format(DateTimePicker2.Value, "yyyy/MM/dd") & "') AND (dbo.tblVoucher.voucher_type_id IN " & _
        '                 "                          (SELECT     voucher_Type_id " & _
        '                 "                            From tblDefVoucherType " & _
        '                 "                            WHERE      gl_type IN (" & ReceiptType & "))) AND (ISNULL(dbo.tblVoucherDetail.credit_amount, 0) > 0) "
        ''Below query is commented on 20-07-2017
        'strSql = strSql & "SELECT     CASE WHEN (dbo.tblVoucherDetail.credit_amount > 0) THEN 1 ELSE 0 END AS tr_type, dbo.tblVoucherDetail.coa_detail_id, " _
        '                & " dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, dbo.tblVoucherDetail.comments, " _
        '                & " dbo.vwCOADetail.detail_title, (IsNull(dbo.tblVoucherDetail.debit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) As debit_amount, (IsNull(dbo.tblVoucherDetail.credit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) As credit_amount, dbo.tblVoucher.post " _
        '                & " FROM         dbo.tblVoucherDetail INNER JOIN " _
        '                & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
        '                & " dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
        '                & " dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id " _
        '                & " WHERE     (dbo.tblVoucher.voucher_id IN " _
        '                & " (SELECT DISTINCT tblVoucher_1.voucher_id " _
        '                & " FROM          dbo.tblVoucherDetail AS tblVoucherDetail_1 INNER JOIN" _
        '                & " dbo.tblVoucher AS tblVoucher_1 ON tblVoucherDetail_1.voucher_id = tblVoucher_1.voucher_id INNER JOIN " _
        '                & " dbo.tblDefVoucherType AS tblDefVoucherType_1 ON  " _
        '                & " tblVoucher_1.voucher_type_id = tblDefVoucherType_1.voucher_type_id INNER JOIN " _
        '                & " dbo.vwCOADetail AS vwCOADetail_1 ON tblVoucherDetail_1.coa_detail_id = vwCOADetail_1.coa_detail_id " _
        '                & " WHERE      (vwCOADetail_1.account_type in( " & AccType & ")) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail_1.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ")) AND (dbo.vwCOADetail.account_type NOT IN ( " & AccType & "))" _
        '                & " and " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & " " _
        '                & " (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(Me.DateTimePicker1.Value.Date, "yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Format(DateTimePicker2.Value, "yyyy-M-d 23:59:59") & "',102)) AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0)-ISNULL(dbo.tblVoucherDetail.credit_amount, 0) <> 0) " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & " "
        '' Start TFS1081 added cost centre group filter.
        strSql = strSql & "SELECT     CASE WHEN (dbo.tblVoucherDetail.credit_amount > 0) THEN 1 ELSE 0 END AS tr_type, dbo.tblVoucherDetail.coa_detail_id, " _
                        & " dbo.tblVoucher.voucher_date, dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, dbo.tblVoucherDetail.comments, " _
                        & " dbo.vwCOADetail.detail_title, (IsNull(dbo.tblVoucherDetail.debit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) As debit_amount, (IsNull(dbo.tblVoucherDetail.credit_amount, 0)-IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)) As credit_amount, dbo.tblVoucher.post " _
                        & " FROM         dbo.tblVoucherDetail INNER JOIN " _
                        & " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
                        & " dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
                        & " dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id " _
                        & " WHERE     (dbo.tblVoucher.voucher_id IN " _
                        & " (SELECT DISTINCT tblVoucher_1.voucher_id " _
                        & " FROM          dbo.tblVoucherDetail AS tblVoucherDetail_1 INNER JOIN" _
                        & " dbo.tblVoucher AS tblVoucher_1 ON tblVoucherDetail_1.voucher_id = tblVoucher_1.voucher_id INNER JOIN " _
                        & " dbo.tblDefVoucherType AS tblDefVoucherType_1 ON  " _
                        & " tblVoucher_1.voucher_type_id = tblDefVoucherType_1.voucher_type_id INNER JOIN " _
                        & " dbo.vwCOADetail AS vwCOADetail_1 ON tblVoucherDetail_1.coa_detail_id = vwCOADetail_1.coa_detail_id " _
                       & " WHERE      (vwCOADetail_1.account_type in( " & AccType & ")) " & CostCenterCriterial1 & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ")) AND (dbo.vwCOADetail.account_type NOT IN ( " & AccType & "))" _
                        & " and " & strYearCriteria & strLocationCriteria & strPostCriteria & strOther_Voucher_Criteria & " " _
                       & " (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(DateTime, '" & Format(Me.DateTimePicker1.Value.Date, "yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Format(DateTimePicker2.Value, "yyyy-M-d 23:59:59") & "',102)) AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") AND (ISNULL(dbo.tblVoucherDetail.debit_amount, 0)-ISNULL(dbo.tblVoucherDetail.credit_amount, 0) <> 0) " & CostCenterCriterial & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & " "


        cm.CommandText = strSql
        cm.ExecuteNonQuery()

        'Dim ObjDAL As New DAL.RptCashFlowDal

        'If ObjDAL.InsertDataForReport("Stander") Then

        strSql = " truncate table TblrptCashFlowStander "
        cm.CommandText = strSql
        cm.ExecuteNonQuery()

        strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post ) " & _
                      "Select Tr_Type ,Tr_Type + 1, coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post  from vwCashFlowPeriodRPT WHERE (IsNull(debit_amount,0) <> 0 Or IsNull(credit_amount,0) <> 0)"

        cm.CommandText = strSql
        cm.ExecuteNonQuery()

        If 1 = 1 Then
            'strSql = "SELECT     tblCOAMainSubSubDetail.coa_detail_id, tblCOAMainSubSubDetail.detail_title, dbo.tblCOAMainSubSub.account_type " & _
            '                     "FROM         dbo.tblCOAMainSubSubDetail AS tblCOAMainSubSubDetail INNER JOIN " & _
            '                     "dbo.tblCOAMainSubSub ON tblCOAMainSubSubDetail.main_sub_sub_id = dbo.tblCOAMainSubSub.main_sub_sub_id " & _
            '                     "WHERE     (dbo.tblCOAMainSubSub.account_type IN ( " & AccType & "))"

            'Dim dt As DataTable
            'dt = UtilityDAL.GetDataTable(strSql).Copy

            Dim ilocation_id As Integer

            ' Get Location ID .. 
            ilocation_id = 0



            '//Preparing Query string to insert opening balance
            'strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
            '            " SELECT     1 AS tr_Type, 0 AS Sort, coa_detail_id, '" & Me.DateTimePicker1.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Opening Balance' AS Comments, detail_title, 0 AS Credit, OpeningBalance, " & _
            '            " 1 AS Post " & _
            '            " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
            '            " AS OpeningBalance " & _
            '            " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL((SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount))-Sum(IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (Convert(Varchar, dbo.tblVoucher.voucher_date,102) < Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "  GROUP BY dbo.tblVoucherDetail.coa_detail_id Having  ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) <> 0)as tmpTblAccountsOpening  INNER JOIN " & _
            '            " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
            '            " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
            '            " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            '' Start TFS1081 added cost centre group filter.
            strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
                        " SELECT     1 AS tr_Type, 0 AS Sort, coa_detail_id, '" & Me.DateTimePicker1.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Opening Balance' AS Comments, detail_title, 0 AS Credit, OpeningBalance, " & _
                        " 1 AS Post " & _
                        " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
                        " AS OpeningBalance " & _
                       " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL((SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount))-Sum(IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (Convert(Varchar, dbo.tblVoucher.voucher_date,102) < Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") " & CostCenterCriterial & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "  GROUP BY dbo.tblVoucherDetail.coa_detail_id Having  ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) <> 0)as tmpTblAccountsOpening  INNER JOIN " & _
                        " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                        " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
                        " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            'values(" & _
            '                             "1 ,0, " & dtRow.Item(0).ToString & " ,'" & Format(Me.DateTimePicker1.Value.Date.AddDays(-1), "yyyy/MM/dd") & "' ,'' ,'Opening Balance' ,'" & dtRow.Item(1).ToString & "' ," & dblCashBankOpening & " ,0 ,'True')"
            '//Inserting Opening Balance
            cm.CommandText = strSql
            cm.ExecuteNonQuery()


            ''//Preparing Query string to insert opening balance

            '//Preparing Query string to insert Closing balance
            'strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
            '            " SELECT     0 AS tr_Type, 3 AS Sort, coa_detail_id, '" & Me.DateTimePicker2.Value & "' AS Voucher_Date, '' AS ChequeNo, 'Closing Balance' AS Comments, detail_title, OpeningBalance,  0 AS Credit," & _
            '            " 1 AS Post " & _
            '            " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
            '            " AS OpeningBalance " & _
            '            " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL((SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount))-Sum(IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) <= Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 00:00:00") & "', 102))  AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "     GROUP BY dbo.tblVoucherDetail.coa_detail_id Having ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) <> 0)as tmpTblAccountsOpening  INNER JOIN " & _
            '            " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
            '            " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
            '            " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            '' Start TFS1081 added cost centre group filter.
            strSql = "Insert Into TblrptCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Comments ,detail_title ,debit_amount ,credit_amount ,post )  " & _
                        " SELECT     0 AS tr_Type, 3 AS Sort, coa_detail_id, '" & Me.DateTimePicker2.Value.ToString("yyyy-MM-dd hh:mm:ss") & "' AS Voucher_Date, '' AS ChequeNo, 'Closing Balance' AS Comments, detail_title, OpeningBalance,  0 AS Credit," & _
                        " 1 AS Post " & _
                        " FROM         (SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, SUM(tmptblAccountsOpening.OpeningBalance) " & _
                        " AS OpeningBalance " & _
                      " FROM          (SELECT     dbo.tblVoucherDetail.coa_detail_id, ISNULL((SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount))-Sum(IsNull(dbo.tblVoucherDetail.Tax_Amount, 0)), 0) AS OpeningBalance  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id  WHERE      (Convert(Varchar, dbo.tblVoucher.voucher_date, 102) <= Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 00:00:00") & "', 102))  AND ISNULL(dbo.tblVoucher.post,0) IN(" & IIf(Me.chkUnposted.Checked = True, "1,0", "1") & ") " & CostCenterCriterial & "  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "     GROUP BY dbo.tblVoucherDetail.coa_detail_id Having ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) <> 0)as tmpTblAccountsOpening  INNER JOIN " & _
                        " dbo.vwCOADetail ON tmptblAccountsOpening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                        " WHERE      (dbo.vwCOADetail.account_type IN ( " & AccType & ")) " & _
                        " GROUP BY dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id) AS OpeningTable "
            'values(" & _
            '                             "1 ,0, " & dtRow.Item(0).ToString & " ,'" & Format(Me.DateTimePicker1.Value.Date.AddDays(-1), "yyyy/MM/dd") & "' ,'' ,'Opening Balance' ,'" & dtRow.Item(1).ToString & "' ," & dblCashBankOpening & " ,0 ,'True')"
            '//Inserting Closing Balance

            cm.CommandText = strSql
            cm.ExecuteNonQuery()
        Else
        End If

        Return ""

    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub cmbCSGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCSGroup.SelectedIndexChanged
        Try
            If Not cmbCSGroup.SelectedIndex = -1 AndAlso cmbCSGroup.SelectedIndex > 0 Then
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter Where CostCenterGroup= '" & Me.cmbCSGroup.Text & "' order by sortorder , name", True)
            Else
                FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            GetCrystalReportRights()
            Me.CallShowReport()
        Catch Ex As Exception
            ShowErrorMessage(Ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        GetCrystalReportRights()
        CallShowReport(True)
    End Sub
End Class