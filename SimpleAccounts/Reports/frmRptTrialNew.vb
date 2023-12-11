Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class frmRptTrialNew
    Dim AcCriteria As String = String.Empty
    Dim IsFormLoaded As Boolean = False
    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try
            GetDataTrial()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function GetDataTrial() As DataTable
        Try
            Dim strSql As String
            strSql = " SELECT vwCOADetail.coa_detail_id, vwCOADetail.detail_code, vwCOADetail.detail_title, ISNULL(OpeningBL.Opening, 0) AS Opening, " _
            & " ISNULL(DebitBL.Debit, 0) AS Debit, ISNULL(CreditBL.Credit, 0) AS Credit, ISNULL(DebitBL.Debit, 0) + ISNULL(OpeningBL.Opening, 0) + ISNULL(CreditBL.Credit, 0) AS Closing " _
            & " FROM vwCOADetail " _
            & " LEFT OUTER JOIN (SELECT tblVoucherDetail.coa_detail_id, SUM(ISNULL(tblVoucherDetail.debit_amount, 0) - ISNULL(tblVoucherDetail.credit_amount, 0))  " _
            & " AS Opening FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id INNER JOIN vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id " _
            & " WHERE ((Convert(varchar, tblVoucher.voucher_date,102) < CONVERT(DATETIME, '" & Me.dtpDateFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102))" & IIf(Me.chkUnPostedVoucher.Checked = True, " AND (Post in(1,0))", "AND (Post=1)") & "  " & IIf(Me.chkPostDatedCheques.Checked = False, "  AND tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "   " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId= " & Me.cmbCostCenter.SelectedValue & "", "") & ") GROUP BY tblVoucherDetail.coa_detail_id) OpeningBL ON vwCOADetail.coa_detail_id = OpeningBL.coa_detail_id " _
            & " LEFT OUTER JOIN (SELECT tblVoucherDetail.coa_detail_id, SUM(ISNULL(tblVoucherDetail.debit_amount, 0) - ISNULL(tblVoucherDetail.credit_amount, 0)) AS Debit FROM tblVoucher INNER JOIN " _
            & " tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id INNER JOIN vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id " _
            & " WHERE ( Convert(varchar, tblVoucher.voucher_date,102) BETWEEN CONVERT(DATETIME, '" & Me.dtpDateFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(DATETIME, '" & Me.dtpDateTo.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me.chkUnPostedVoucher.Checked = True, " AND (Post in(1,0))", "AND (Post=1)") & " " & IIf(Me.chkPostDatedCheques.Checked = False, "  AND tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "  " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId= " & Me.cmbCostCenter.SelectedValue & "", "") & " " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND tblVoucher.location_id = " & Me.cmbCompany.SelectedValue & "", "") & " GROUP BY tblVoucherDetail.coa_detail_id " _
            & " HAVING (SUM(ISNULL(tblVoucherDetail.debit_amount, 0) - ISNULL(tblVoucherDetail.credit_amount, 0)) > 0)) DebitBL ON vwCOADetail.coa_detail_id = DebitBL.coa_detail_id " _
            & " LEFT OUTER JOIN(SELECT tblVoucherDetail.coa_detail_id, SUM(ISNULL(tblVoucherDetail.debit_amount, 0) - ISNULL(tblVoucherDetail.credit_amount, 0)) AS Credit " _
            & " FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id INNER JOIN " _
            & " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id WHERE (Convert(varchar, tblVoucher.voucher_date,102) BETWEEN CONVERT(DATETIME, '" & Me.dtpDateFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(DATETIME, '" & Me.dtpDateTo.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me.chkUnPostedVoucher.Checked = True, " AND (tblVoucher.Post in(1,0))", "AND (tblVoucher.Post=1)") & "  " & IIf(Me.chkPostDatedCheques.Checked = False, "  AND tblVoucherDetail.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & "   " & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND tblVoucherDetail.CostCenterId= " & Me.cmbCostCenter.SelectedValue & "", "") & " " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND tblVoucher.location_id = " & Me.cmbCompany.SelectedValue & "", "") & " GROUP BY tblVoucherDetail.coa_detail_id " _
            & " HAVING (SUM(ISNULL(tblVoucherDetail.debit_amount, 0) - ISNULL(tblVoucherDetail.credit_amount, 0)) < 0)) CreditBL ON vwCOADetail.coa_detail_id = CreditBL.coa_detail_id " _
            & " WHERE  (dbo.vwCOADetail.detail_title IS NOT NULL) "
            If Me.cmbAccount.Text = "Main" Then
                strSql = strSql & "" & IIf(Me.cmbAccountName.SelectedIndex > 0, " AND vwCOADetail.coa_main_id = " & Me.cmbAccountName.SelectedValue & "", "") & ""
            ElseIf Me.cmbAccount.Text = "Sub" Then
                strSql = strSql & "" & IIf(Me.cmbAccountName.SelectedIndex > 0, " AND vwCOADetail.main_sub_id = " & Me.cmbAccountName.SelectedValue & "", "") & ""
            ElseIf Me.cmbAccount.Text = "SubSub" Then
                strSql = strSql & "" & IIf(Me.cmbAccountType.SelectedIndex > 0, " AND vwCOADetail.Account_Type='" & Me.cmbAccountType.Text & "'", "") & " "
                strSql = strSql & "" & IIf(Me.cmbAccountName.SelectedIndex > 0, " AND vwCOADetail.main_sub_sub_id = " & Me.cmbAccountName.SelectedValue & " ", "") & " "
                'strSql = strSql.Remove(strSql.Length - 4, 4)
            ElseIf Me.cmbAccount.Text = "Detail" Then
                strSql = strSql & "" & IIf(Me.cmbAccountType.SelectedIndex > 0, " AND vwCOADetail.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                strSql = strSql & "" & IIf(Me.cmbAccountName.SelectedIndex > 0, " AND vwCOADetail.coa_detail_id = " & Me.cmbAccountName.SelectedValue & "", "") & " "
                'strSql = strSql.Remove(strSql.Length - 4, 4)
            Else
                strSql = strSql & "" & IIf(Me.cmbAccountType.SelectedIndex > 0, " AND vwCOADetail.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                strSql = strSql & "" & IIf(Me.cmbAccountName.SelectedIndex > 0, " AND vwCOADetail.coa_detail_id = " & Me.cmbAccountName.SelectedValue & "", "") & " "
            End If
            Dim Dt As New DataTable
            Dt.Clear()
            Dt = GetDataTable(strSql.ToString)
            Dt.AcceptChanges()
            Return Dt
        Catch ex As Exception
            Throw ex
        End Try
        ''& IIf(Me.chkPostDatedCheques.Checked = True, "", " AND (Convert(varchar, tblVoucher.Cheque_Date,102) < GetDate() OR tblVoucher.Cheque_Date IS NULL) ") & 
    End Function
    Private Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            Dim str As String = String.Empty
            If Condition = "CostCenterHead" Then
                str = "Select DISTINCT CostCenterGroup, CostCenterGroup From tblDefCostCenter"
                FillDropDown(Me.cmbProjectHead, str)
            ElseIf Condition = "CostCenter" Then
                str = "Select CostCenterId, Name as [Cost Center] From tblDefCostCenter"
                If Me.chkCostCenter.Checked = True Then
                    str += " WHERE Active IN(1,0,NULL)"
                Else
                    str += " WHERE Active=1"
                End If
                str += " ORDER BY Name"
                FillDropDown(Me.cmbCostCenter, str)
                ''TASK TFS4889
            ElseIf Condition = "Company" Then
                str = "Select  CompanyId, CompanyName FROM CompanyDefTable "
                FillDropDown(Me.cmbCompany, str)
                ''END TASK TFS4889
            ElseIf Condition = String.Empty Then
                If Me.cmbAccount.Text = "Main" Then
                    str = "Select coa_main_id as Id, main_title  as [Title] From tblCOAMain"
                    cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, str)
                ElseIf Me.cmbAccount.Text = "Sub" Then
                    str = "Select main_sub_id as Id, sub_title  as [Title] From tblCOAMainSub"
                    cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, str)
                ElseIf Me.cmbAccount.Text = "SubSub" Then
                    str = "Select main_sub_sub_id as Id, sub_sub_title as [Title] from tblCOAMainSubSub " & IIf(Me.cmbAccountType.SelectedIndex > 0, " WHERE b.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                    cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, str)
                ElseIf Me.cmbAccount.Text = "Detail" Then
                    str = "Select a.coa_detail_id as Id, a.detail_title as [Title] From tblCOAMainSubSubDetail a INNER JOIN tblCOAMainSubSub b on a.main_sub_sub_id = b.main_sub_sub_id " & IIf(Me.cmbAccountType.SelectedIndex > 0, " WHERE b.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                    cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, str)
                Else
                    str = "Select a.coa_detail_id as Id, a.detail_title as [Title] From tblCOAMainSubSubDetail a INNER JOIN tblCOAMainSubSub b on a.main_sub_sub_id = b.main_sub_sub_id " & IIf(Me.cmbAccountType.SelectedIndex > 0, " WHERE b.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                    cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, str)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbAccount_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccount.SelectedValueChanged
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmRptTrialNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            ' Me.dtpDateFrom.Value = Date.Now.AddMonths(-1)
            FillCombos("CostCenterHead")
            FillCombos("CostCenter")
            FillCombos("Company")
            Me.cmbAccount.SelectedIndex = 0
            Me.cmbAccountType.SelectedIndex = 0
            Me.cmbProjectHead.SelectedIndex = 0
            Me.cmbCostCenter.SelectedIndex = 0
            FillCombos()
            IsFormLoaded = True
        Catch ex As Exception
        End Try
    End Sub
    Private Sub cmbProjectHead_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProjectHead.SelectedIndexChanged
        Try
            If Me.IsFormLoaded = True Then
                Dim str As String = String.Empty
                If Me.cmbProjectHead.SelectedIndex > 0 Then
                    str = "Select CostCenterId, Name as [Cost Center] From tblDefCostCenter WHERE CostCenterGroup='" & Me.cmbProjectHead.SelectedValue & "'"
                    FillDropDown(Me.cmbCostCenter, str)
                Else
                    str = "Select CostCenterId, Name as [Cost Center] From tblDefCostCenter ORDER BY name"
                    FillDropDown(Me.cmbCostCenter, str)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetAllRecord()
        Try
            Me.grdTrial.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdTrial.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdTrial.DataSource = GetDataTrial()
            Me.grdTrial.RetrieveStructure()
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdTrial.RootTable.Columns
                If col.Index <> 0 AndAlso col.Index <> 1 AndAlso col.Index <> 2 Then
                    col.FormatString = "N"
                    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.TotalFormatString = "N"
                End If
            Next
            Me.grdTrial.RootTable.Columns("coa_detail_id").Visible = False
            Me.grdTrial.RootTable.Columns("detail_code").Caption = "Account Code"
            Me.grdTrial.RootTable.Columns("detail_title").Caption = "Account Description"
            Me.grdTrial.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnGenerate_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        GetAllRecord()
    End Sub
    Private Sub cmbAccount_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccount.SelectedIndexChanged
        Try
            Dim Str As String = String.Empty
            If Me.IsFormLoaded = True Then
                If Me.cmbAccount.Text = "Main" Then
                    Str = "Select coa_main_id as Id, main_title as [Title] From tblCOAMain"
                    cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, Str)
                ElseIf Me.cmbAccount.Text = "Sub" Then
                    Str = "Select main_sub_id as Id, sub_title as [Title] From tblCOAMainSub"
                    cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, Str)
                ElseIf Me.cmbAccount.Text = "SubSub" Then
                    Str = "Select main_sub_sub_id as Id, sub_sub_title as [Title] From tblCOAMainSubSub " & IIf(Me.cmbAccountType.SelectedIndex > 0, " WHERE Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                    cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, Str)
                ElseIf Me.cmbAccount.Text = "Detail" Then
                    Str = "Select a.coa_detail_id as Id, a.detail_title as [Title] From tblCOAMainSubSubDetail a INNER JOIN tblCOAMainSubSub b on a.main_sub_sub_id = b.main_sub_sub_id " & IIf(Me.cmbAccountType.SelectedIndex > 0, " WHERE b.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                    cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, Str)
                Else
                    Str = "Select a.coa_detail_id as Id, a.detail_title as [Title] From tblCOAMainSubSubDetail a INNER JOIN tblCOAMainSubSub b on a.main_sub_sub_id = b.main_sub_sub_id " & IIf(Me.cmbAccountType.SelectedIndex > 0, " WHERE b.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                    cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, Str)
                End If
                If (Me.cmbAccount.Text = "SubSub" Or Me.cmbAccount.Text = "Detail") Then
                    Me.Panel2.Visible = True
                Else
                    Me.Panel2.Visible = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub cmbAccountType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccountType.SelectedIndexChanged
        Try
            If Me.IsFormLoaded = True Then
                Dim Str As String = String.Empty
                If Me.cmbAccount.Text = "SubSub" Then
                    Str = "Select main_sub_sub_id as Id, sub_sub_title as [Title] From tblCOAMainSubSub " & IIf(Me.cmbAccountType.SelectedIndex > 0, " WHERE Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                    cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, Str)
                ElseIf Me.cmbAccount.Text = "Detail" Then
                    Str = "Select a.coa_detail_id as Id, a.detail_title as [Title] From tblCOAMainSubSubDetail a INNER JOIN tblCOAMainSubSub b on a.main_sub_sub_id = b.main_sub_sub_id " & IIf(Me.cmbAccountType.SelectedIndex > 0, " WHERE b.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                    Me.cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, Str)
                Else
                    Str = "Select a.coa_detail_id as Id, a.detail_title as [Title] From tblCOAMainSubSubDetail a INNER JOIN tblCOAMainSubSub b on a.main_sub_sub_id = b.main_sub_sub_id " & IIf(Me.cmbAccountType.SelectedIndex > 0, " WHERE b.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                    Me.cmbAccountName.DataSource = Nothing
                    FillDropDown(Me.cmbAccountName, Str)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0
            Dim strDisplayMember As String = String.Empty
            id = Me.cmbAccountName.SelectedValue
            FillCombos("")
            Me.cmbAccountName.SelectedValue = id

            strDisplayMember = Me.cmbProjectHead.Text
            FillCombos("CostCenterHead")
            Me.cmbProjectHead.Text = strDisplayMember

            id = Me.cmbCostCenter.SelectedValue
            FillCombos("CostCenter")
            Me.cmbCostCenter.SelectedValue = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub chkCostCenter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCostCenter.CheckedChanged
        Try
            FillCombos("CostCenter")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub UiCtrlGridBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiCtrlGridBar1.Load
    '    Try
    '        Me.UiCtrlGridBar1.txtGridTitle.Text = "Trial Balance From " & Me.dtpDateFrom.Value.Date.ToString("dd-MMM-yyyy") & " To " & Me.dtpDateTo.Value.Date.ToString("dd-MMM-yyyy") & ""
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'Private Sub dtpDateFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateFrom.ValueChanged, dtpDateTo.ValueChanged
    '    Try
    '        UiCtrlGridBar1_Load(Nothing, Nothing)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpDateFrom.Value = Date.Today
            Me.dtpDateTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpDateFrom.Value = Date.Today.AddDays(-1)
            Me.dtpDateTo.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpDateFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpDateTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpDateFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpDateTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpDateFrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpDateTo.Value = Date.Today
        End If
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click

    End Sub
End Class