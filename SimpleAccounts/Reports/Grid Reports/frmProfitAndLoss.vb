Imports System.IO
Imports CrystalDecisions
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports CrystalDecisions.Shared.ExportDestinationOptions
Imports SBUtility.Utility
Imports SBModel

Public Class frmProfitAndLoss
    Dim lbl As New Label
    Dim crpt As New ReportDocument
    Dim str_Path As String = String.Empty
    Dim ReportName As String = String.Empty
    Dim BaseCurrencyId As Integer
    Dim BaseCurrencyName As String = String.Empty
    Dim CurrencyRate As Double
    Dim costcenterid As String
    Dim ArePropertyFilters As Boolean = False

    Private Sub frmProfitAndLoss_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.P AndAlso e.Control = True Then
            PrintToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
    Private Sub frmProfitAndLoss_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.BackColor = Color.White
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.ToolStripButton3.Enabled = True
                Me.ToolStripButton2.Enabled = True
                Me.Button1.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            Me.Button1.Enabled = False
            Me.ToolStripButton3.Enabled = False
            Me.ToolStripButton2.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    Me.ToolStripButton3.Enabled = True
                ElseIf RightsDt.FormControlName = "Send Email" Then
                    Me.ToolStripButton2.Enabled = True
                ElseIf RightsDt.FormControlName = "Generate" Then
                    Me.Button1.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Function getProfitAndLoss() As DataTable
        Try
            Dim str As String = String.Empty
            If Me.cmbCompany.ActiveRow.Index > 0 Then
                ''TASK TFS4943
                str = "sp_PL_SingleDateCompanyWise '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00.000") & "', '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "'," & IIf(Me.chkExcludeClosingVoucher.Checked = True, 1, 0) & "," & IIf(Me.chkposted.Checked = True, 1, 0) & "," & IIf(Me.chkinclsalescomission.Checked = True, 1, 0) & ""
                ''END TASK TFS4943
            Else
                str = "sp_PL_SingleDate '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00.000") & "', '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "'," & IIf(Me.chkExcludeClosingVoucher.Checked = True, 1, 0) & "," & IIf(Me.chkposted.Checked = True, 1, 0) & "," & IIf(Me.chkinclsalescomission.Checked = True, 1, 0) & ""
                'str = "sp_PL_SingleDate '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00.000") & "', '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', '" & Me.ComboBox1.SelectedValue & "'," & IIf(Me.chkExcludeClosingVoucher.Checked = True, 1, 0) & "," & Me.ComboBox1.SelectedValue & ""
            End If
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Me.lblSalesNet.Text = FormatNumber(Val(dt.Rows(0).Item(1).ToString), , , TriState.True)
                    Me.lblCostGoodSold.Text = FormatNumber(Val(dt.Rows(0).Item(3).ToString), , , TriState.True)
                    ' ................... Total Profit .........................
                    Me.lblGrossProfit.Text = FormatNumber(Math.Round(((Val(dt.Rows(0).Item(1).ToString)) + (Val(dt.Rows(0).Item(3).ToString))), 0), , , TriState.True)
                    '-----------------------------------------------------------
                    Me.lblNoneOperatingIncome.Text = FormatNumber(Math.Round(Val(dt.Rows(0).Item(5).ToString), 0), , , TriState.True)
                    Me.lblTotalProfit.Text = FormatNumber(Math.Round(Convert.ToDouble(FormatNumber(Me.lblGrossProfit.Text, , TriState.True)) + Val(dt.Rows(0).Item(5).ToString), 0), , , TriState.True)
                    '--------------------------------------------------------------
                    Me.lblAdministrativeExpense.Text = FormatNumber(Math.Round(Val(dt.Rows(0).Item(7).ToString), 0), , , TriState.True)
                    Me.lblSellingExpense.Text = FormatNumber(Math.Round(Val(dt.Rows(0).Item(9).ToString), 0), , , TriState.True)
                    Me.lblOperatingExp.Text = FormatNumber(Math.Round(Val(dt.Rows(0).Item(11).ToString), 0), , , TriState.True)
                    '..................... Total Operating Profit ................
                    Me.lblTotalOperatingExpense.Text = FormatNumber(Math.Round(Val(((Val(dt.Rows(0).Item(7).ToString) + Val(dt.Rows(0).Item(9).ToString)) + Val(dt.Rows(0).Item(11).ToString))), 0), , , TriState.True)
                    Me.lblOepratingProfit.Text = FormatNumber(Math.Round(Val(((Convert.ToDouble(FormatNumber(Me.lblGrossProfit.Text, , TriState.True)) + Val(dt.Rows(0).Item(5).ToString)))) + ((((Val(dt.Rows(0).Item(7).ToString) + Val(dt.Rows(0).Item(9).ToString)) + Val(dt.Rows(0).Item(11).ToString)))), 0), , , TriState.True)
                    '--------------------------------------------------------------
                    Me.lblFinanceCost.Text = FormatNumber(Math.Round(Val(dt.Rows(0).Item(13).ToString), 0), , , TriState.True)
                    '------------------------ Total Profit Before Taxation ---------
                    Me.lblProfitBeforeTaxation.Text = FormatNumber(Math.Round((Convert.ToDouble(FormatNumber(Me.lblOepratingProfit.Text, , TriState.True)) + (Val(dt.Rows(0).Item(13).ToString))), 0), , , TriState.True)
                    '---------------------------------------------------------------
                    Me.lblTaxation.Text = FormatNumber(Math.Round(Val(dt.Rows(0).Item(15).ToString), 0), , , TriState.True)
                    '------------------------ Net Profit After Taxation ------------
                    Me.lblProfitAfterTaxation.Text = FormatNumber(Math.Round((Convert.ToDouble(FormatNumber(Me.lblProfitBeforeTaxation.Text, , TriState.True)) + Convert.ToDouble(FormatNumber(Me.lblTaxation.Text, , TriState.True))), 0), , , TriState.True)
                End If
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CurrencyBasedValues()
        Try
            'Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
            'CurrencyRate = dr.Row.Item("CurrencyRate").ToString
            'If CurrencyRate = 0 Then CurrencyRate = 1
            'If lblSalesNet.Text <> 0 Then
            '    lblSalesNet.Text = lblSalesNet.Text / CurrencyRate
            'End If
            If lblCostGoodSold.Text <> 0 Then
                lblCostGoodSold.Text = lblCostGoodSold.Text / CurrencyRate
            End If
            If lblNoneOperatingIncome.Text <> 0 Then
                lblNoneOperatingIncome.Text = lblNoneOperatingIncome.Text / CurrencyRate
            End If
            If lblAdministrativeExpense.Text <> 0 Then
                lblAdministrativeExpense.Text = lblAdministrativeExpense.Text / CurrencyRate
            End If
            If lblSellingExpense.Text <> 0 Then
                lblSellingExpense.Text = lblSellingExpense.Text / CurrencyRate
            End If
            If lblOperatingExp.Text <> 0 Then
                lblOperatingExp.Text = lblOperatingExp.Text / CurrencyRate
            End If
            If lblFinanceCost.Text <> 0 Then
                lblFinanceCost.Text = lblFinanceCost.Text / CurrencyRate
            End If
            If lblTaxation.Text <> 0 Then
                lblTaxation.Text = lblTaxation.Text / CurrencyRate
            End If
            Dim SalesNet As Double = lblSalesNet.Text
            Dim CGS As Double = lblCostGoodSold.Text
            Dim NOP As Double = lblNoneOperatingIncome.Text
            Dim Admisitrative As Double = lblAdministrativeExpense.Text
            Dim SellingExpense As Double = lblSellingExpense.Text
            Dim OperatingExp As Double = lblOperatingExp.Text
            Dim FinanceCost As Double = lblFinanceCost.Text
            Dim Taxation As Double = lblTaxation.Text
            lblGrossProfit.Text = SalesNet + CGS
            Dim GrossProfit As Double = lblGrossProfit.Text
            lblTotalProfit.Text = GrossProfit + NOP
            lblTotalOperatingExpense.Text = Admisitrative + SellingExpense + OperatingExp
            lblOepratingProfit.Text = GrossProfit + NOP + Admisitrative + SellingExpense + OperatingExp
            Dim OperatingProfit As Double = lblOepratingProfit.Text
            lblProfitBeforeTaxation.Text = OperatingProfit + FinanceCost
            Dim PBT As Double = lblProfitBeforeTaxation.Text
            lblProfitAfterTaxation.Text = PBT + Taxation
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmProfitAndLoss_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Not getConfigValueByType("PropertyFiltersOnReports").ToString = "Error" Then
                ArePropertyFilters = Convert.ToBoolean(getConfigValueByType("PropertyFiltersOnReports").ToString)
            End If
            Dim dt As DataTable = GetDataTable("Select CostCenterId, name as [Cost Center] From tblDefCostCenter Union Select 0,'.... All Projects ....'")
            Me.ComboBox1.DisplayMember = "Cost Center"
            Me.ComboBox1.ValueMember = "CostCenterId"
            Me.ComboBox1.DataSource = dt
            'BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            Dim Str = String.Empty
            'Str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
            'FillDropDown(Me.cmbCurrency, Str, False)
            'Me.cmbCurrency.SelectedValue = BaseCurrencyId
            'If Not Me.ComboBox1.SelectedIndex = -1 Then Me.ComboBox1.SelectedIndex = 0
            ''TASK TFS4318
            'Str = "Select BranchId, Name  from Branch where Active = 1"
            Str = "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter"
            FillUltraDropDown(Me.cmbBranch, Str)
            Me.cmbBranch.Rows(0).Activate()
            'Me.cmbBranch.DisplayLayout.Bands(0).Columns("BranchId").Hidden = True
            'Str = "SELECT PropertyTypeId, PropertyType FROM PropertyType"
            'FillUltraDropDown(Me.cmbPropertyType, Str)
            'Me.cmbPropertyType.Rows(0).Activate()
            'Me.cmbPropertyType.DisplayLayout.Bands(0).Columns("PropertyTypeId").Hidden = True
            ''END TASK TFS4318
            'Me.cmbPeriod.Text = "Current Month"
            ''TASK TFS4943
            Str = "Select  CompanyId, CompanyName FROM CompanyDefTable "
            FillUltraDropDown(Me.cmbCompany, Str)
            Me.cmbCompany.Rows(0).Activate()
            Me.cmbCompany.DisplayLayout.Bands(0).Columns("CompanyId").Hidden = True
            ''END TASK TFS44943
            If ArePropertyFilters = True Then
                Me.pnlProperty.Visible = True
            Else
                Me.pnlProperty.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Me.cmbPeriod.Text = "Today" Then
    '        Me.dtpFromDate.Value = Date.Today
    '        Me.dtpToDate.Value = Date.Today
    '    ElseIf Me.cmbPeriod.Text = "Yesterday" Then
    '        Me.dtpFromDate.Value = Date.Today.AddDays(-1)
    '        Me.dtpToDate.Value = Date.Today.AddDays(-1)
    '    ElseIf Me.cmbPeriod.Text = "Current Week" Then
    '        Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
    '        Me.dtpToDate.Value = Date.Today
    '    ElseIf Me.cmbPeriod.Text = "Current Month" Then
    '        Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
    '        Me.dtpToDate.Value = Date.Today
    '    ElseIf Me.cmbPeriod.Text = "Current Year" Then
    '        Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
    '        Me.dtpToDate.Value = Date.Today
    '    End If
    'End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.Controls.Add(lbl)
            lbl.Visible = True
            lbl.Size = New Size(788, 780)
            lbl.Anchor = AnchorStyles.Bottom
            lbl.Anchor = AnchorStyles.Left
            lbl.Anchor = AnchorStyles.Top
            lbl.Anchor = AnchorStyles.Right
            lbl.Location = New System.Drawing.Point(25, 100)
            lbl.AutoSize = False
            lbl.Text = "Loading..."
            lbl.BackColor = Color.Transparent
            lbl.BringToFront()
            Application.DoEvents()
            getProfitAndLoss()
            'If chkShowSelectCurrency.Checked = False Then
            '    CurrencyBasedValues()
            'End If
            Me.lbl.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub lnkSalesNet_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSalesNet.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptLedger) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.PLNoteId = 1
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            If Me.ComboBox1.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = Me.ComboBox1.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            rptTrialBalance.NoteId = 0
            rptTrialBalance.PLNoteId = 0

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkCostGoodSold_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCostGoodSold.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptLedger) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.PLNoteId = 2
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            If Me.ComboBox1.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = Me.ComboBox1.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            rptTrialBalance.NoteId = 0
            rptTrialBalance.PLNoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkNonOperatingIncome_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkNonOperatingIncome.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptLedger) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.PLNoteId = 3
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            If Me.ComboBox1.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = Me.ComboBox1.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            rptTrialBalance.NoteId = 0
            rptTrialBalance.PLNoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkAdministrativeExp_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkAdministrativeExp.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptLedger) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.PLNoteId = 4
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            If Me.ComboBox1.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = Me.ComboBox1.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            rptTrialBalance.NoteId = 0
            rptTrialBalance.PLNoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkSellingExpense_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSellingExpense.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptLedger) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.PLNoteId = 5
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            If Me.ComboBox1.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = Me.ComboBox1.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            rptTrialBalance.NoteId = 0
            rptTrialBalance.PLNoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkOtherOperatingExp_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkOtherOperatingExp.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptLedger) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.PLNoteId = 6
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            If Me.ComboBox1.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = Me.ComboBox1.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            rptTrialBalance.NoteId = 0
            rptTrialBalance.PLNoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkFinanceCost_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkFinanceCost.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptLedger) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.PLNoteId = 7
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            If Me.ComboBox1.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = Me.ComboBox1.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            rptTrialBalance.NoteId = 0
            rptTrialBalance.PLNoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkTaxation_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkTaxation.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptLedger) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.PLNoteId = 8
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            If Me.ComboBox1.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = Me.ComboBox1.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            rptTrialBalance.NoteId = 0
            rptTrialBalance.PLNoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Dim Str As String = String.Empty
        Try
            GetCrystalReportRights()
            If Me.cmbCompany.ActiveRow.Index > 0 Then
                ''TASK TFS4943
                Str = "sp_PL_SingleDateCompanyWise '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00.000") & "', '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', '" & Me.ComboBox1.SelectedValue & "'," & IIf(Me.chkExcludeClosingVoucher.Checked = True, 1, 0) & "," & CurrencyRate & ", '" & IIf(Me.cmbBranch.ActiveRow.Index > 0, Me.cmbBranch.Text, "") & "', " & Me.cmbCompany.Value & ""
                ''END TASK TFS4943
            Else
                Str = "sp_PL_SingleDate '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00.000") & "', '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "'," & IIf(Me.chkExcludeClosingVoucher.Checked = True, 1, 0) & "," & IIf(Me.chkposted.Checked = True, 1, 0) & "," & IIf(Me.chkinclsalescomission.Checked = True, 1, 0) & ""
            End If
            Dim dt As DataTable = GetDataTable(Str)
            ''Below lines of code are commented against TASK TFS4925 ON 
            'AddRptParam("@CostCenterID", Me.ComboBox1.SelectedValue)
            'AddRptParam("@ExcludeClosing", IIf(Me.chkExcludeClosingVoucher.Checked = True, 1, 0))
            'AddRptParam("@CostCenter", Me.ComboBox1.Text.ToString)
            'AddRptParam("@CurrencyId", IIf(Me.chkShowSelectCurrency.Checked = True, Me.cmbCurrency.SelectedValue, 0))
            'Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
            'CurrencyRate = dr.Row.Item("CurrencyRate").ToString
            'If CurrencyRate = 0 Then CurrencyRate = 1
            'AddRptParam("@CurrencyRate", CurrencyRate)
            ' ''TASK TFS4897 
            'AddRptParam("@Branch", IIf(Me.cmbBranch.ActiveRow.Index > 0, Me.cmbBranch.Text, ""))
            'AddRptParam("@PropertyType", IIf(Me.cmbPropertyType.Value > 0, Me.cmbPropertyType.Value, 0))
            '' END TASK TFS4897
            'ShowReport("rptProftAndLossStatementSingleDate", , , , , , , , , , , , , "Profit And Loss", "Date Form " & Me.dtpFromDate.Value.ToString("dd/MMM/yyyy") & " Date To " & Me.dtpToDate.Value.ToString("dd/MMM/yyyy") & "")
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptProftAndLossStatementSingleDateTable", , , , False, , , dt)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            GetCrystalReportRights()
            Dim Str As String = "sp_PL_SingleDate '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00.000") & "', '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "'," & IIf(Me.chkExcludeClosingVoucher.Checked = True, 1, 0) & "," & IIf(Me.chkposted.Checked = True, 1, 0) & "," & IIf(Me.chkinclsalescomission.Checked = True, 1, 0) & ""
            Dim dt As DataTable = GetDataTable(Str)
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("@CostCenterID", Me.ComboBox1.SelectedValue)
            AddRptParam("@ExcludeClosing", IIf(Me.chkExcludeClosingVoucher.Checked = True, 1, 0))
            AddRptParam("@CostCenter", Me.ComboBox1.Text.ToString)
            'AddRptParam("@CurrencyId", IIf(Me.chkShowSelectCurrency.Checked = True, Me.cmbCurrency.SelectedValue, 0))
            'Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
            'CurrencyRate = dr.Row.Item("CurrencyRate").ToString
            'If CurrencyRate = 0 Then CurrencyRate = 1
            ''AddRptParam("@CurrencyRate", CurrencyRate)
            'ShowReport("rptProftAndLossStatementSingleDate", , "" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "", "" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "", False, , , dt)
            ShowReport("rptProftAndLossStatementSingleDate", , , , , , , , , , , , , "Profit And Loss", "Date Form " & Me.dtpFromDate.Value.ToString("dd/MMM/yyyy") & " Date To " & Me.dtpToDate.Value.ToString("dd/MMM/yyyy") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click

        If Me.SaveFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Dim IsCompanyInfo As Boolean = Convert.ToBoolean(GetConfigValue("ShowCompanyAddressOnPageHeader").ToString)
            Dim CompanyTitle As String = GetConfigValue("CompanyNameHeader").ToString()
            Dim CompanyAddressHeader As String = GetConfigValue("CompanyAddressHeader").ToString()

            ReportName = "rptProftAndLossStatementSingleDate"
            Dim crptConnection As New ConnectionInfo
            With crptConnection
                .ServerName = DBServerName
                .DatabaseName = DBName
                If .UserID <> "" Then
                    .UserID = DBUserName
                    .Password = DBPassword
                    .IntegratedSecurity = False
                Else
                    .IntegratedSecurity = True
                End If
            End With
            crpt.Load(str_ApplicationStartUpPath & "\Reports\" & ReportName & ".rpt", DBServerName)
            Try
                crpt.SetParameterValue("@CompanyName", CompanyTitle)
                crpt.SetParameterValue("@CompanyAddress", CompanyAddressHeader)
                crpt.SetParameterValue("@ShowHeader", IsCompanyInfo)
            Catch ex As Exception

            End Try

            crpt.SetParameterValue("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            crpt.SetParameterValue("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            crpt.SetParameterValue("@CostCenterID", Me.ComboBox1.SelectedValue)
            crpt.SetParameterValue("@ExcludeClosing", IIf(Me.chkExcludeClosingVoucher.Checked = True, 1, 0))
            crpt.SetParameterValue("@CostCenter", Me.ComboBox1.Text.ToString)
            If DBUserName <> "" Then
                crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, DBUserName, DBPassword)
                crpt.DataSourceConnections.Item(0).SetLogon(DBUserName, DBPassword)
            Else
                crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, True)
            End If

            Dim CRExportOpts As ExportOptions
            Dim CRDiskFileOpts As New DiskFileDestinationOptions
            Dim CRFormatTypeOpts As New PdfRtfWordFormatOptions

            CRDiskFileOpts.DiskFileName = Me.SaveFileDialog1.FileName
            CRExportOpts = crpt.ExportOptions
            With CRExportOpts
                .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                .ExportDestinationOptions = CRDiskFileOpts
                .ExportFormatOptions = CRFormatTypeOpts
            End With
            Try
                crpt.Export()
            Catch ex As Exception

            End Try
        Else
            Exit Sub
        End If
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try

            Dim IsCompanyInfo As Boolean = Convert.ToBoolean(GetConfigValue("ShowCompanyAddressOnPageHeader").ToString)
            Dim CompanyTitle As String = GetConfigValue("CompanyNameHeader").ToString()
            Dim CompanyAddressHeader As String = GetConfigValue("CompanyAddressHeader").ToString()

            ReportName = "rptProftAndLossStatementSingleDate"
            Dim crptConnection As New ConnectionInfo
            With crptConnection
                .ServerName = DBServerName
                .DatabaseName = DBName
                If .UserID <> "" Then
                    .UserID = DBUserName
                    .Password = DBPassword
                    .IntegratedSecurity = False
                Else
                    .IntegratedSecurity = True
                End If
            End With

            crpt.Load(str_ApplicationStartUpPath & "\Reports\" & ReportName & ".rpt", DBServerName)
            Try
                crpt.SetParameterValue("@CompanyName", CompanyTitle)
                crpt.SetParameterValue("@CompanyAddress", CompanyAddressHeader)
                crpt.SetParameterValue("@ShowHeader", IsCompanyInfo)
            Catch ex As Exception

            End Try
            crpt.SetParameterValue("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            crpt.SetParameterValue("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            crpt.SetParameterValue("@CostCenterID", Me.ComboBox1.SelectedValue)
            crpt.SetParameterValue("@ExcludeClosing", IIf(Me.chkExcludeClosingVoucher.Checked = True, 1, 0))
            crpt.SetParameterValue("@CostCenter", Me.ComboBox1.Text.ToString)
            If DBUserName <> "" Then
                crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, DBUserName, DBPassword)
                crpt.DataSourceConnections.Item(0).SetLogon(DBUserName, DBPassword)
            Else
                crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, True)
            End If
            Dim CRExportOpts As ExportOptions
            Dim CRDiskFileOpts As New DiskFileDestinationOptions
            Dim CRFormatTypeOpts As New PdfRtfWordFormatOptions
            str_Path = _FileExportPath & "Profit And Loss" & "-" & Date.Today.ToString("dd-MM-yyyy") & ".Pdf"
            CRDiskFileOpts.DiskFileName = str_Path 'str_ApplicationStartUpPath & "\EmailAttachments\" & ReportName & Date.Today.ToString("dd-MM-yyyy") & ".Pdf"
            CRExportOpts = crpt.ExportOptions
            With CRExportOpts
                .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                .ExportDestinationOptions = CRDiskFileOpts
                .ExportFormatOptions = CRFormatTypeOpts
            End With
            Try
                crpt.Export()
            Catch ex As Exception

            End Try

            Dim frm As New frmOutgoingEmail
            frm.txtDataFile.Text = str_Path
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.Show()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbBranch_ValueChanged(sender As Object, e As EventArgs) Handles cmbBranch.ValueChanged
        If Me.cmbBranch.ActiveRow.Index > 0 Then
            Dim dt As DataTable = GetDataTable("Select CostCenterId, name as [Cost Center] From tblDefCostCenter WHERE CostCenterGroup = '" & cmbBranch.Text & "'  Union Select 0,'.... All Projects ....'")
            Me.ComboBox1.DisplayMember = "Cost Center"
            Me.ComboBox1.ValueMember = "CostCenterId"
            Me.ComboBox1.DataSource = dt
        Else
            Dim dt As DataTable = GetDataTable("Select CostCenterId, name as [Cost Center] From tblDefCostCenter Union Select 0,'.... All Projects ....'")
            Me.ComboBox1.DisplayMember = "Cost Center"
            Me.ComboBox1.ValueMember = "CostCenterId"
            Me.ComboBox1.DataSource = dt
        End If
    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub
End Class