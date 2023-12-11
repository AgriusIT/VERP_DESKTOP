Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Public Class CashFlowStatement
    Public flgCompanyRights As Boolean = False
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            '01-Aug-2017: Task# TFS1129: Waqar Raza: 
            'Added to apply security Rights on Crystal Report Print And Export
            'Using this Admin can restrict other users to Export and Print.
            'Start TFS1129:
            GetCrystalReportRights()
            'End TFS1129:
            Me.CallShowReport()
        Catch Ex As Exception
            ShowErrorMessage(Ex.Message)
        End Try
    End Sub
    '01-Aug-2017: Task# TFS1129: Waqar Raza:
    'Add to apply security Rights for different users
    'Using this admin can give any user to apply the rights 
    Public Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.OK_Button.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.OK_Button.Enabled = False
            Me.btnPrint.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    OK_Button.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    btnPrint.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CallShowReport(Optional ByVal Print As Boolean = False)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim fromDate As String = Me.DateTimePicker1.Value.Year & "," & Me.DateTimePicker1.Value.Month & "," & Me.DateTimePicker1.Value.Day & ",0,0,0"
            Dim ToDate As String = Me.DateTimePicker2.Value.Year & "," & Me.DateTimePicker2.Value.Month & "," & Me.DateTimePicker2.Value.Day & ",23,59,59"
            If Me.cmbCashAccount.SelectedIndex = -1 Then
                ShowErrorMessage("Please define cash account")
                Me.cmbCashAccount.Focus()
                Exit Sub
            End If
            Dim opening As Integer = GetAccountOpeningBalance("" & IIf(Me.cmbCashAccount.SelectedIndex > 0, Me.cmbCashAccount.SelectedValue, 0) & "", Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00", IIf(rdoCash.Checked = True, "Cash" & IIf(Me.rdoBoth.Checked = True, "CashAndBank", ""), IIf(Me.rdoBank.Checked = True, "Bank", "CashAndBank")), IIf(Me.chkUnposted.Checked = True, True, False), IIf(Me.cmbCostCenter.SelectedValue > 0, cmbCostCenter.SelectedValue, 0))
            AddRptParam("FromDate", Me.DateTimePicker1.Value)
            AddRptParam("ToDate", Me.DateTimePicker2.Value)
            AddRptParam("CostCenter", IIf(Me.cmbCostCenter.SelectedValue > 0, Me.cmbCostCenter.Text, " "))
            ShowReport("rptCashFlowStatementNew", , , , Print, Val(opening).ToString, , GetCashAndBankData, , , , , , "Cash And Bank Statement", "Date Form " & Me.DateTimePicker1.Value.ToString("dd/MMM/yyyy") & " Date To " & Me.DateTimePicker2.Value.ToString("dd/MMM/yyyy") & "")
        Catch ex As Exception
            Throw ex
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
    Private Sub CashFlowStatment_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If
            Me.pnlVendorCustomer.Visible = True
            Me.pnlCost.Visible = True
            Me.cmbCostCenter.Visible = True
            Me.lblCostCenter.Visible = True
            Me.chkIncludeCheque.Visible = True
            Me.chkUnposted.Visible = True
            Me.cmbCashAccount.Visible = True
            Me.lblAccount.Visible = True
            Dim strAccounType As String = String.Empty
            If Me.rdoCash.Checked = True Then
                strAccounType = "Cash"
            ElseIf rdoBank.Checked = True Then
                strAccounType = "Bank"
            ElseIf Me.rdoBoth.Checked = True Then
                strAccounType = ""
            End If
            FillDropDown(Me.cmbCashAccount, "Select coa_detail_id, detail_title From vwCOADetail WHERE  Active=1 " & IIf(strAccounType.Length > 0, " AND Account_Type='" & strAccounType.ToString & "'", " AND Account_Type IN('Cash','Bank')") & "")
            FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetCashAndBankData() As DataTable
        Try

            Dim dt As New DataTable
            Dim str As String = String.Empty
            str = "SELECT dbo.tblDefVoucherType.voucher_type, dbo.tblVoucher.voucher_no, dbo.tblVoucher.voucher_date, dbo.tblVoucherDetail.coa_detail_id, " _
           & "           dbo.tblVoucherDetail.debit_amount, dbo.tblVoucherDetail.credit_amount, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title,  " _
           & "           ISNULL(dbo.tblVoucherDetail.CostCenterID, 0) AS CostCenterID, dbo.tblDefCostCenter.Name AS CostCenter, dbo.tblVoucher.post,  " _
            & "           dbo.tblVoucherDetail.cheque_no, dbo.tblVoucherDetail.cheque_date, tblVoucherDetail.Comments as Description,dbo.tblDefVoucherType.sort_order, vwCOADetail.Sub_Sub_Code, vwCOADetail.Sub_Sub_Title, vwCOADetail.Account_Type " _
           & "           FROM dbo.tblVoucherDetail INNER JOIN " _
           & "           dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
           & "           dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
           & "           dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
           & "           dbo.tblDefCostCenter ON dbo.tblVoucherDetail.CostCenterID = dbo.tblDefCostCenter.CostCenterID " _
           & "           WHERE (dbo.tblVoucher.voucher_id IN " _
           & "           (SELECT DISTINCT tblvoucher.voucher_id " _
           & "           FROM dbo.tblVoucherDetail INNER JOIN " _
           & "           dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
           & "           dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN " _
           & "           dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id "
            str += " WHERE (Convert(varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(Datetime, '" & Me.DateTimePicker1.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.DateTimePicker2.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) AND Isnull(tblVoucher.Post,0) In " & IIf(Me.chkUnposted.Checked = True, "(1,0,NULL)", "(1)") & " " & IIf(MyCompanyId > 0, " AND vwCoaDetail.CompanyId=" & MyCompanyId & "", "") & " " & IIf(Me.cmbCashAccount.SelectedIndex > 0, " AND tblVoucherDetail.coa_detail_Id=" & Me.cmbCashAccount.SelectedValue & "", "") & ""
            str += " " & IIf(Me.rdoCash.Checked = True, " AND vwcoadetail.account_type = 'Cash')) AND (dbo.vwCOADetail.account_type IN ('Cash')  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ") ", "") & " "
            str += " " & IIf(Me.rdoBank.Checked = True, " AND vwcoadetail.account_type = 'Bank')) AND (dbo.vwCOADetail.account_type IN ('Bank')  " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ") ", "") & " "
            str += " " & IIf(Me.rdoBoth.Checked = True, " AND(vwcoadetail.account_type In('Cash','Bank'))) AND (dbo.vwCOADetail.account_type IN ('Cash','Bank'))   " & IIf(Me.chkIncludeCheque.Checked = False, "  AND dbo.tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ") ", "") & ""
            str += " " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId=" & Me.cmbCostCenter.SelectedValue & "", "") & ""
            If MyCompanyId > 0 Then
                str += " AND vwCOADetail.CompanyId = " & MyCompanyId & ""
            End If
            ''07-Mar-2014 TASK:2468  Imran Ali  Date sort order in cash flow statement
            str += " ORDER BY dbo.tblVoucher.voucher_date, dbo.tblDefVoucherType.sort_order ASC "
            'End Task:2468
            dt = GetDataTable(str)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rdoCash_CheckedChanged(sender As Object, e As EventArgs) Handles rdoCash.CheckedChanged
        Try
            Dim strAccounType As String = String.Empty
            If Me.rdoCash.Checked = True Then
                strAccounType = "Cash"
            ElseIf rdoBank.Checked = True Then
                strAccounType = "Bank"
            ElseIf Me.rdoBoth.Checked = True Then
                strAccounType = ""
            End If
            FillDropDown(Me.cmbCashAccount, "Select coa_detail_id, detail_title From vwCOADetail WHERE Active=1 " & IIf(strAccounType.Length > 0, " AND Account_Type='" & strAccounType.ToString & "'", "") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoBank_CheckedChanged(sender As Object, e As EventArgs) Handles rdoBank.CheckedChanged
        Try
            Dim strAccounType As String = String.Empty
            If Me.rdoCash.Checked = True Then
                strAccounType = "Cash"
            ElseIf rdoBank.Checked = True Then
                strAccounType = "Bank"
            ElseIf Me.rdoBoth.Checked = True Then
                strAccounType = ""
            End If
            FillDropDown(Me.cmbCashAccount, "Select coa_detail_id, detail_title From vwCOADetail WHERE Active=1 " & IIf(strAccounType.Length > 0, " AND Account_Type='" & strAccounType.ToString & "'", "") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoBoth_CheckedChanged(sender As Object, e As EventArgs) Handles rdoBoth.CheckedChanged
        Try
            Dim strAccounType As String = String.Empty
            If Me.rdoCash.Checked = True Then
                strAccounType = "Cash"
            ElseIf rdoBank.Checked = True Then
                strAccounType = "Bank"
            ElseIf Me.rdoBoth.Checked = True Then
                strAccounType = ""
            End If
            FillDropDown(Me.cmbCashAccount, "Select coa_detail_id, detail_title From vwCOADetail WHERE Active=1 " & IIf(strAccounType.Length > 0, " AND Account_Type='" & strAccounType.ToString & "'", " AND Account_Type IN('Cash','Bank')") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class