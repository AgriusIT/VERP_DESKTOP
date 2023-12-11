'Ali Faisal : TFS2048 : Add report to show the Bank Type wise Cash flow Statement
''TASK TFS3425 Muhammad Amin done on 01-06-2018 : Show record of zero or null cost centers.
Imports System
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmBankTypeWiseCashFlow
    Implements IGeneral
    Public Shared _DetailId As Integer = 0
    Private IsOpenForm As Boolean = False
    ''' <summary>
    ''' Ali Faisal : TFS2048 : Set indexing of Records in grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        BankType
        DesignatedTo
        CostCenter
        SubSubId
        SubSubCode
        SubSubTitle
        DetailId
        DetailCode
        DetailTitle
        Opening
        DebitAmount
        CreditAmount
        Closing
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS2048 : Apply grid settings to hide some columns and formating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.SubSubId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.DetailId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CostCenter).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SubSubCode).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SubSubTitle).Visible = False
            Me.grdSaved.RootTable.Columns(grd.DetailTitle).Width = 250

            If Me.rdoCash.Checked = True Then
                Me.grdSaved.RootTable.Columns(grd.BankType).Visible = False
                Me.grdSaved.RootTable.Columns(grd.DesignatedTo).Visible = False
            End If

            Me.grdSaved.RootTable.Columns(grd.DebitAmount).Caption = "Receipts"
            Me.grdSaved.RootTable.Columns(grd.CreditAmount).Caption = "Payments"

            For Each col As Janus.Windows.GridEX.GridEXColumn In grdSaved.RootTable.Columns
                If col.Index > 8 Then
                    col.FormatString = "N" & DecimalPointInValue
                    col.TotalFormatString = "N" & DecimalPointInValue
                    col.Width = 125
                    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                End If
            Next

            If Me.rdoCash.Checked = False Then
                Dim grdGroupBy As New Janus.Windows.GridEX.GridEXGroup(Me.grdSaved.RootTable.Columns(grd.BankType))
                grdGroupBy.GroupPrefix = String.Empty
                Me.grdSaved.RootTable.Groups.Add(grdGroupBy)
            End If

            Me.grdSaved.RootTable.Columns(grd.DetailTitle).ColumnType = Janus.Windows.GridEX.ColumnType.Link
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS2048 : Apply security to show specific controls to standard users
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                Me.btnShow.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnPrint.Enabled = False
            Me.btnShow.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    If Me.btnPrint.Text = "&Print" Then btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    ''' <summary>
    ''' Ali Faisal : TFS2048 : Fill list bosex
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "HeadCostCenter" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
                'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
                'Start TFS3412
                lstHeadCostCenter.DeSelect()
                'End TFS3412
            ElseIf Condition = "CostCenter" Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1")
                'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
                'Start TFS3412
                lstCostCenter.DeSelect()
                'End TFS3412
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS2048 : Get all records to get data in given duration
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim strData As String = ""
            Dim dtData As New DataTable
            ''Below lines are commented against TASK TFS3425 on 01-06-2018
            'strData = "SELECT tblBankInfo.Bank_Type AS BankType, tblBankInfo.Designated_To AS DesignatedTo, '' AS CostCenter, vwCOADetail.main_sub_sub_id AS SubSubId, vwCOADetail.sub_sub_code AS SubSubCode, vwCOADetail.sub_sub_title AS SubSubTitle, vwCOADetail.coa_detail_id AS DetailId, vwCOADetail.detail_code AS DetailCode, vwCOADetail.detail_title AS DetailTitle, ISNULL(Opening.Opening, 0) AS Opening, ISNULL(VoucherSum.Debit_Amount, 0) AS DebitAmount, ISNULL(VoucherSum.Credit_Amount, 0) AS CreditAmount, ISNULL(Opening.Opening, 0) + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0) AS Closing " _
            '        & "FROM vwCOADetail LEFT OUTER JOIN tblBankInfo ON vwCOADetail.coa_detail_id = tblBankInfo.Bank_Id LEFT OUTER JOIN " _
            '        & "(SELECT tblVoucherDetail.coa_detail_id, ISNULL(SUM(tblVoucherDetail.debit_amount), 0) AS Debit_Amount, ISNULL(SUM(tblVoucherDetail.credit_amount), 0) AS Credit_Amount FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.voucher_id = tblVoucher.voucher_id " _
            '        & "WHERE (CONVERT(varchar, tblVoucher.voucher_date, 102) BETWEEN CONVERT(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) " & IIf(Me.lstCostCenter.SelectedIDs.Length > 0, " AND tblVoucherDetail.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") ", " ") & " " & IIf(Me.chkIncludeUnPosted.Checked = True, " AND ISNULL(tblVoucher.post,0) IN (0,1) ", "AND ISNULL(tblVoucher.post,0) IN (1) ") & " " _
            '        & "GROUP BY tblVoucherDetail.coa_detail_id) AS VoucherSum ON vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
            '        & "(SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opening FROM tblVoucher AS V INNER JOIN tblVoucherDetail AS VD ON V.voucher_id = VD.voucher_id LEFT OUTER JOIN tblForm AS frm ON frm.Form_Name = V.source " _
            '        & "WHERE (CONVERT(varchar, V.voucher_date, 102) < CONVERT(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102)) " & IIf(Me.lstCostCenter.SelectedIDs.Length > 0, " AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") ", " ") & " " & IIf(Me.chkIncludeUnPosted.Checked = True, " AND ISNULL(V.post,0) IN (0,1) ", "AND ISNULL(V.post,0) IN (1) ") & " GROUP BY VD.coa_detail_id) AS Opening ON vwCOADetail.coa_detail_id = Opening.coa_detail_id " _
            '        & "WHERE " & IIf(Me.rdoCash.Checked = True, " vwCOADetail.account_type = 'Cash' AND dbo.vwCOADetail.account_type IN ('Cash') ", " ") & " " & IIf(Me.rdoBank.Checked = True, " vwCOADetail.account_type = 'Bank' AND dbo.vwCOADetail.account_type IN ('Bank') ", " ") & " " & IIf(Me.rdoBoth.Checked = True, " (vwCOADetail.account_type In('Cash','Bank')) AND (dbo.vwCOADetail.account_type IN ('Cash','Bank')) ", " ") & " ORDER BY DetailId"

            strData = "SELECT tblBankInfo.Bank_Type AS BankType, tblBankInfo.Designated_To AS DesignatedTo, '' AS CostCenter, vwCOADetail.main_sub_sub_id AS SubSubId, vwCOADetail.sub_sub_code AS SubSubCode, vwCOADetail.sub_sub_title AS SubSubTitle, vwCOADetail.coa_detail_id AS DetailId, vwCOADetail.detail_code AS DetailCode, vwCOADetail.detail_title AS DetailTitle, ISNULL(Opening.Opening, 0) AS Opening, ISNULL(VoucherSum.Debit_Amount, 0) AS DebitAmount, ISNULL(VoucherSum.Credit_Amount, 0) AS CreditAmount, ISNULL(Opening.Opening, 0) + ISNULL(VoucherSum.Debit_Amount, 0) - ISNULL(VoucherSum.Credit_Amount, 0) AS Closing " _
                  & "FROM vwCOADetail LEFT OUTER JOIN tblBankInfo ON vwCOADetail.coa_detail_id = tblBankInfo.Bank_Id LEFT OUTER JOIN " _
                  & "(SELECT tblVoucherDetail.coa_detail_id, ISNULL(SUM(tblVoucherDetail.debit_amount), 0) AS Debit_Amount, ISNULL(SUM(tblVoucherDetail.credit_amount), 0) AS Credit_Amount FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.voucher_id = tblVoucher.voucher_id " _
                  & "WHERE (CONVERT(varchar, tblVoucher.voucher_date, 102) BETWEEN CONVERT(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) " & IIf(Me.chkShowWithOutCostCenter.Checked = True, " AND ISNULL(tblVoucherDetail.CostCenterID, 0) IN (" & 0 & ") ", "" & IIf(Me.lstCostCenter.SelectedIDs.Length > 0, " AND tblVoucherDetail.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") ", " ") & "") & "  " & IIf(Me.chkIncludeUnPosted.Checked = True, " AND ISNULL(tblVoucher.post,0) IN (0,1) ", "AND ISNULL(tblVoucher.post,0) IN (1) ") & " " _
                  & "GROUP BY tblVoucherDetail.coa_detail_id) AS VoucherSum ON vwCOADetail.coa_detail_id = VoucherSum.coa_detail_id LEFT OUTER JOIN " _
                  & "(SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opening FROM tblVoucher AS V INNER JOIN tblVoucherDetail AS VD ON V.voucher_id = VD.voucher_id LEFT OUTER JOIN tblForm AS frm ON frm.Form_Name = V.source " _
                  & "WHERE (CONVERT(varchar, V.voucher_date, 102) < CONVERT(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102)) " & IIf(Me.chkShowWithOutCostCenter.Checked = True, " AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") ", "" & IIf(Me.lstCostCenter.SelectedIDs.Length > 0, " AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") ", " ") & "") & " " & IIf(Me.chkIncludeUnPosted.Checked = True, " AND ISNULL(V.post,0) IN (0,1) ", "AND ISNULL(V.post,0) IN (1) ") & " GROUP BY VD.coa_detail_id) AS Opening ON vwCOADetail.coa_detail_id = Opening.coa_detail_id " _
                  & "WHERE " & IIf(Me.rdoCash.Checked = True, " vwCOADetail.account_type = 'Cash' AND dbo.vwCOADetail.account_type IN ('Cash') ", " ") & " " & IIf(Me.rdoBank.Checked = True, " vwCOADetail.account_type = 'Bank' AND dbo.vwCOADetail.account_type IN ('Bank') ", " ") & " " & IIf(Me.rdoBoth.Checked = True, " (vwCOADetail.account_type In('Cash','Bank')) AND (dbo.vwCOADetail.account_type IN ('Cash','Bank')) ", " ") & " ORDER BY DetailId"
            dtData = GetDataTable(strData)
            dtData.AcceptChanges()
            Me.grdSaved.DataSource = dtData
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    ''' <summary>
    ''' Ali Faisal : TFS2048 : Reset all controls to their default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Label3.Text = ""
            Me.cmbPeriod.SelectedIndex = 0
            Me.cmbPeriod.Text = "Current Month"
            FillCombos("HeadCostCenter")
            FillCombos("CostCenter")
            Me.chkIncludeUnPosted.Checked = True
            Me.UltraTabControl1.Tabs(0).Selected = True
            CtrlGrdBar1_Load(Nothing, Nothing)
            ApplySecurity(Utility.EnumDataMode.New)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFromDate.Value = Date.Today
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFromDate.Value = Date.Today.AddDays(-1)
                Me.dtpToDate.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpToDate.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Bank Balances" & Chr(10) & "From Date : " & Me.dtpFromDate.Value.ToString("yyyy-MM-dd") & "To Date : " & Me.dtpToDate.Value.ToString("yyyy-MM-dd")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPLSubSubAccountWiseSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            IsOpenForm = True
            btnNew_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS2048 : Show crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@IncludeUnPosted", IIf(Me.chkIncludeUnPosted.Checked = True, 1, 0))
            If Me.rdoBank.Checked = True Then
                AddRptParam("@AccountType", "Bank")
            ElseIf Me.rdoCash.Checked = True Then
                AddRptParam("@AccountType", "Cash")
            ElseIf Me.rdoBoth.Checked = True Then
                AddRptParam("@AccountType", "Both")
            End If
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            Else
                AddRptParam("@CostCenterIds", "")
            End If
            ShowReport("rptAccountWiseCashStatus")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            GetAllRecords()
            Label3.Text = dtpFromDate.Value.ToString("dd-MM-yyyy") + " To " + dtpToDate.Value.ToString("dd-MM-yyyy")
            Me.UltraTabControl1.Tabs(1).Selected = True
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.btnPrint.Visible = False
            End If
            If Me.UltraTabControl1.Tabs(1).Selected = True Then
                Me.btnPrint.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstHeadCostCenter_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstHeadCostCenter.SelectedIndexChaned
        Try
            If Me.lstHeadCostCenter.SelectedIDs.Length > 0 Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1  AND CostCenterGroup IN (" & Me.lstHeadCostCenter.SelectedItems & ")")
            Else
                FillCombos("CostCenter")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/C"
            rptTrialBalance.DateTimePicker1.Value = Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPosted.CheckState
            Dim CostCenterIds As String = ""
            If chkShowWithOutCostCenter.Checked = True Then
                If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso lstCostCenter.Enabled = True Then
                    CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                    rptTrialBalance.CostID = "0," + CostCenterIds
                Else
                    CostCenterIds = "0"
                End If
            Else
                If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                    CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                    rptTrialBalance.CostID = "0," + CostCenterIds
                Else
                    CostCenterIds = ""
                End If
            End If
            rptTrialBalance.GetSubSubWiseDetailAccountsTrial(Val(Me.grdSaved.GetRow.Cells(grd.SubSubId).Value.ToString), CostCenterIds)
            frmMain.LoadControl("rptTrialBalance")
            _DetailId = Val(Me.grdSaved.GetRow.Cells(grd.DetailId).Value.ToString)
            If Not frmMain.Panel2.Controls.Contains(rptLedger) Then
                frmMain.LoadControl("rptLedger")
            End If
            rptLedger.Tracking = True
            rptLedger.Costid = CostCenterIds
            rptLedger.CoaDetailId = Val(Me.grdSaved.GetRow.Cells(grd.DetailId).Value.ToString)
            rptLedger.DateTimePicker1.Value = Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00")
            rptLedger.DateTimePicker2.Value = Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59")
            rptLedger.dptFromDate = Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00")
            rptLedger.dptToDate = Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59")
            rptLedger.cmbAccount.Value = Val(Me.grdSaved.GetRow.Cells(grd.DetailId).Value.ToString)
            'rptTrialBalance.DrillDown = True
            'rptLedger.GetMultiCostCenterRecord(Val(Me.grdSaved.GetRow.Cells(grd.DetailId).Value.ToString), CostCenterIds)
            rptLedger.GetLedger()
            rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
            frmMain.LoadControl("rptLedger")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS3425
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkShowWithOutCostCenter_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowWithOutCostCenter.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If chkShowWithOutCostCenter.Checked Then
                Me.lstHeadCostCenter.Enabled = False
                Me.lstCostCenter.Enabled = False
            Else
                Me.lstHeadCostCenter.Enabled = True
                Me.lstCostCenter.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class