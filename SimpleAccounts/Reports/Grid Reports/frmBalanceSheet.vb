Imports CrystalDecisions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports SBUtility.Utility
Imports SBModel

Public Class frmBalanceSheet
    Dim startDate As DateTime = "01/Jan/2001"
    Dim crpt As New ReportDocument
    Dim str_Path As String = String.Empty
    Dim ReportName As String = String.Empty

    Private Sub frmBalanceSheet_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.P AndAlso e.Control = True Then
            PrintToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
    Private Sub frmBalanceSheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 
        GetSecurityRights()
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            GetSecurityRights()
            Dim Str As String = String.Empty
            Str = "Select CostCenterId, name as [Cost Center] From tblDefCostCenter"
            FillToolStripDropDown(Me.cmbCostCentre, Str)
            'Me.cmbPeriod.Text = "Current Month"
            Button1_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.ToolStripButton3.Enabled = True
                Me.ToolStripButton2.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            Me.ToolStripButton3.Enabled = False
            Me.ToolStripButton2.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    Me.ToolStripButton3.Enabled = True
                ElseIf RightsDt.FormControlName = "Send Email" Then
                    Me.ToolStripButton2.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
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
    Public Function GetBalanceSheet() As Boolean
        Me.Cursor = Cursors.WaitCursor
        Try
            'Ali Faisal : TFS4701 : Cost Center id issue in query filter.
            Dim str As String = "Sp_BalanceSheetFormated '" & startDate.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', " & IIf(Me.chkIncludeUnPostedVouchers.Checked = True, 1, 0) & ", " & IIf(Me.cmbCostCentre.SelectedIndex = -1, 0, Me.cmbCostCentre.ComboBox.SelectedValue) & ""
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Me.Label17.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance9"), 0), , , TriState.True)
                    Me.lblSharedDeposit.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance30"), 0), , , TriState.True)
                    Me.Label18.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance10"), 0), , , TriState.True)
                    Me.Label19.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance14"), 0), , , TriState.True)
                    '---------------Total Owner Equity -------------
                    Me.lblTotalOwnerEquity.Text = FormatNumber(Math.Round(((dt.Rows(0).Item("Balance9") + dt.Rows(0).Item("Balance10")) + dt.Rows(0).Item("Balance14") + dt.Rows(0).Item("Balance30")), 0), , , TriState.True)
                    '-----------------------------------------------

                    Me.Label20.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance15"), 0), , , TriState.True)


                    Me.Label21.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance16"), 0), , , TriState.True)
                    Me.Label22.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance17"), 0), , , TriState.True)
                    Me.Label23.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance18"), 0), , , TriState.True)
                    '---------------Total None Current Liabilities -------
                    Me.lblTotalNoneLiabilities.Text = FormatNumber(Math.Round(((dt.Rows(0).Item("Balance16") + dt.Rows(0).Item("Balance17")) + dt.Rows(0).Item("Balance18")), 0), , , TriState.True)
                    '-----------------------------------------------------
                    Me.Label24.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance19"), 0), , , TriState.True)
                    Me.Label25.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance20"), 0), , , TriState.True)
                    Me.Label26.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance21"), 0), , , TriState.True)
                    Me.Label27.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance11"), 0), , , TriState.True)
                    '--------------- Total Capital / Liabilities ----------
                    Me.lblCurrentLiabilities.Text = FormatNumber(Math.Round(((dt.Rows(0).Item("Balance19") + dt.Rows(0).Item("Balance20")) + (dt.Rows(0).Item("Balance21") + dt.Rows(0).Item("Balance11"))), 0), , , TriState.True)
                    Me.lblTotalCapital.Text = FormatNumber(Math.Round(((Convert.ToDouble(FormatNumber(Me.lblCurrentLiabilities.Text, , TriState.True)) + Convert.ToDouble(FormatNumber(Me.lblTotalNoneLiabilities.Text, , TriState.True))) + (Convert.ToDouble(FormatNumber(Me.lblTotalOwnerEquity.Text, , TriState.True)) + Convert.ToDouble(dt.Rows(0).Item("Balance15")))), 0), , , TriState.True)
                    '------------------------------------------------------
                    Me.Label28.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance12"), 0), , , TriState.True)
                    Me.Label29.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance13"), 0), , , TriState.True)
                    Me.Label30.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance22"), 0), , , TriState.True)
                    '------------------ Total None Current Assets ----------
                    Me.lblTotalNoneCurrentAssets.Text = FormatNumber(Math.Round(((dt.Rows(0).Item("Balance12") + dt.Rows(0).Item("Balance13")) + dt.Rows(0).Item("Balance22")), 0), , , TriState.True)
                    '-------------------------------------------------------
                    Me.Label31.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance23"), 0), , , TriState.True)
                    '------------------------------------------------------
                    Me.Label37.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance24"), 0), , , TriState.True)
                    Me.Label32.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance25"), 0), , , TriState.True)
                    Me.Label33.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance26"), 0), , , TriState.True)
                    Me.Label34.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance27"), 0), , , TriState.True)
                    Me.Label35.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance28"), 0), , , TriState.True)
                    Me.Label36.Text = FormatNumber(Math.Round(dt.Rows(0).Item("Balance29"), 0), , , TriState.True)
                    Me.lblTotalCurrentAssets.Text = FormatNumber(Math.Round((dt.Rows(0).Item("Balance24") + dt.Rows(0).Item("Balance25") + dt.Rows(0).Item("Balance26") + dt.Rows(0).Item("Balance27") + dt.Rows(0).Item("Balance28") + dt.Rows(0).Item("Balance29")), 0), , , TriState.True)
                    Me.lblTotalAssets.Text = FormatNumber(Math.Round(((Convert.ToDouble(FormatNumber(Me.lblTotalCurrentAssets.Text, , TriState.True)) + Convert.ToDouble(FormatNumber(Me.lblTotalNoneCurrentAssets.Text, , TriState.True))) + dt.Rows(0).Item("Balance23")), 0), , , TriState.True)
                End If
            End If


            If dt IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim lbl As New Label
        Try
            Me.Controls.Add(lbl)
            lbl.Visible = True
            lbl.Size = New Size(742, 460)
            lbl.Location = New System.Drawing.Point(11, 126)
            lbl.AutoSize = False
            lbl.Text = "Loading... please wait"
            lbl.BackColor = Color.Transparent
            lbl.BringToFront()
            Application.DoEvents()
            GetBalanceSheet()
            lbl.Visible = False

        Catch ex As Exception
            lbl.Visible = True
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 9
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = "1/1/2001" ' Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 10
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 14
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel23_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel23.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 15
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel4_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 16
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel5_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 17
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel6_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel6.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 18
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel7_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel7.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 19
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel8_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel8.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 20
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel9_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel9.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 21
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel10_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel10.LinkClicked
        Try
            frmMain.LoadControl("rptTrialBalance")
            rptTrialBalance.NoteId = 11
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel12_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel12.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 12
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel13_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel13.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 13
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel14_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel14.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 22
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel15_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel15.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 23
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel17_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel17.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 24
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel18_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel18.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 25
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel16_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel16.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 26
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel19_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel19.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 27
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel20_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel20.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 28
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel21_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel21.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 29
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = startDate
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                rptTrialBalance.cmbCostCenter.Value = cmbCostCentre.ComboBox.SelectedValue
            Else
                rptTrialBalance.cmbCostCenter.Rows(0).Activate()
            End If
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            GetCrystalReportRights()
            Dim startDate As DateTime = "2001-1-1"
            AddRptParam("@IncludeUnPosted", IIf(Me.chkIncludeUnPostedVouchers.Checked = True, 1, 0))
            ShowReport("rptBSFormated", , startDate.ToString("yyyy-M-d 00:00:00"), Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"), True, , , , , , , , , "Balance Sheet", "Balance Sheet Up To " & Me.dtpToDate.Value.ToString("dd/MMM/yyyy") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmBalanceSheet_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.BackColor = Color.White
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpToDate.ValueChanged
        Dim lbl As New Label
        Try
            Me.Controls.Add(lbl)
            lbl.Visible = True
            lbl.Size = New Size(788, 610)
            lbl.Anchor = AnchorStyles.Bottom
            lbl.Anchor = AnchorStyles.Left
            lbl.Anchor = AnchorStyles.Top
            lbl.Anchor = AnchorStyles.Right
            lbl.Location = New System.Drawing.Point(25, 100)
            lbl.AutoSize = False
            lbl.Text = "Loading... please wait"
            lbl.BackColor = Color.Transparent
            lbl.BringToFront()
            Application.DoEvents()
            GetBalanceSheet()
        Catch ex As Exception
            ShowErrorMessage("Error occured while loading... Report: " & ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            GetCrystalReportRights()
            Dim startDate As DateTime = "1/1/2001"

            AddRptParam("@IncludeUnPosted", IIf(Me.chkIncludeUnPostedVouchers.Checked = True, 1, 0))
            AddRptParam("@CostCentreID", IIf(Me.cmbCostCentre.SelectedIndex = -1, 0, Me.cmbCostCentre.ComboBox.SelectedValue))

            ShowReport("rptBSFormated", , startDate.ToString("yyyy-M-d 00:00:00"), Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"), False, , , , , , , , , "Balance Sheet", "Balance Sheet Up To " & Me.dtpToDate.Value.ToString("dd/MMM/yyyy") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        If Me.SaveFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim IsCompanyInfo As Boolean = Convert.ToBoolean(GetConfigValue("ShowCompanyAddressOnPageHeader").ToString)
            Dim CompanyTitle As String = GetConfigValue("CompanyNameHeader").ToString()
            Dim CompanyAddressHeader As String = GetConfigValue("CompanyAddressHeader").ToString()

            ReportName = "rptBSFormated"
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
            Dim startDate As DateTime = "1/1/2001"
            crpt.Load(str_ApplicationStartUpPath & "\Reports\" & ReportName & ".rpt", DBServerName)
            Try
                crpt.SetParameterValue("@CompanyName", CompanyTitle)
                crpt.SetParameterValue("@CompanyAddress", CompanyAddressHeader)
                crpt.SetParameterValue("@ShowHeader", IsCompanyInfo)
            Catch ex As Exception

            End Try
            crpt.SetParameterValue("@FromDate", startDate.ToString("yyyy-M-d 00:00:00"))
            crpt.SetParameterValue("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            crpt.SetParameterValue("@IncludeUnPosted", IIf(Me.chkIncludeUnPostedVouchers.Checked = True, 1, 0))
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

            ReportName = "rptBSFormated"
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
            Dim startDate As DateTime = "1/1/2001"
            crpt.SetParameterValue("@FromDate", startDate.ToString("yyyy-M-d 00:00:00"))
            crpt.SetParameterValue("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            crpt.SetParameterValue("@IncludeUnPosted", IIf(Me.chkIncludeUnPostedVouchers.Checked = True, 1, 0))
            If DBUserName <> "" Then
                crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, DBUserName, DBPassword)
                crpt.DataSourceConnections.Item(0).SetLogon(DBUserName, DBPassword)
            Else
                crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, True)
            End If
            Dim CRExportOpts As ExportOptions
            Dim CRDiskFileOpts As New DiskFileDestinationOptions
            Dim CRFormatTypeOpts As New PdfRtfWordFormatOptions
            str_Path = _FileExportPath & "\Balance Sheet" & "-" & Date.Today.ToString("dd-MM-yyyy") & ".Pdf"
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
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim lbl As New Label
        Try
            Me.Controls.Add(lbl)
            lbl.Visible = True
            lbl.Size = New Size(788, 610)
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
            GetBalanceSheet()
        Catch ex As Exception
            ShowErrorMessage("Error occured while loading Report: " & ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Private Sub lnkSharedDeposit_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSharedDeposit.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.NoteId = 30
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = "1/1/2001" ' Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPostedVouchers.Checked
            rptTrialBalance.GetSubSubAccountsTrial()
            frmMain.LoadControl("rptTrialBalance")
            'rptTrialBalance.NoteId = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class