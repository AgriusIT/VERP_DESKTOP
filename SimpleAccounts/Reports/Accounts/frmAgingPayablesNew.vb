''TASK TFS3425 Muhammad Amin done on 01-06-2018 : Show record of zero or null cost centers.
Imports SBModel
Imports SBDal
Imports SBUtility
Imports System.Data.SqlClient

Public Class frmAgingPayablesNew
    Dim dt As DataTable
    Public _FromDate As DateTime
    Public _ToDate As DateTime
    Public DrillDown As Boolean = False
    Dim dtData As DataTable
    Dim strAging As Integer = 1
    Dim str1stAging As Integer = 30
    Dim str1stAgingName As String = "1_30"
    Dim str2ndAging As Integer = 60
    Dim str2ndAgingName As String = "30_60"
    Dim str3rdAging As Integer = 90
    Dim str3rdAgingName As String = "60_90"
    Dim str4thAging As Integer = 180
    Dim str4thAgingName As String = "90_180"
    Dim str5thAging As Integer = 270
    Dim str5thAgingName As String = "180_270"
    Dim str6thAging As Integer = 360
    Dim str6thAgingName As String = "360+"
    Dim IsOpenForm As Boolean = False

    Public Sub GetSecurityRights(Optional ByVal Condition As String = "")
        Try
            If LoginGroup = "Administrator" Then
                Me.btnAddFormate.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.btnAddFormate.Enabled = False
            Me.btnPrint.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Add Aging" Then
                    Me.btnAddFormate.Enabled = True
                End If
                If RightsDt.FormControlName = "Not Show Payble Balance" Then
                    Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Else
                    Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmGrdRptAgingPayables_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.P AndAlso e.Control = True Then
            btnPrint_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub frmGrdRptAgingPayables_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Dim lbl As New Label
        Try
            IsOpenForm = True
            btnNew_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub FillGrid()
        Try
            Dim str As String = "SP_Rpt_PayableNew '" & Date.Now.ToString("yyyy-M-d 00:00:00") & "','" & Date.Now.ToString("yyyy-M-d 23:59:59") & "', " & strAging & ", " & str1stAging & ", '" & str1stAgingName & "'," & str2ndAging & ", '" & str2ndAgingName & "'," & str3rdAging & ", '" & str3rdAgingName & "'," & str4thAging & ", '" & str4thAgingName & "'," & str5thAging & ", '" & str5thAgingName & "'," & str6thAging & ", '" & str6thAgingName & "', " & IIf(Me.chkIncludeUnPosted.Checked = True, 1, 0) & ", '" & IIf(Me.chkShowWithOutCostCenter.Checked = True, "0", "0," & Me.lstCostCenter.SelectedIDs & "") & "', " & Me.cmbSubSubAccount.Value & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RootTable.Columns("Current_Amount").Caption = "Not Due"
            Me.grd.RootTable.Columns("1_30").Caption = str1stAgingName
            Me.grd.RootTable.Columns("30_60").Caption = str2ndAgingName
            Me.grd.RootTable.Columns("60_90").Caption = str3rdAgingName
            Me.grd.RootTable.Columns("90_180").Caption = str4thAgingName
            Me.grd.RootTable.Columns("180_270").Caption = str5thAgingName
            Me.grd.RootTable.Columns("360+").Caption = str6thAgingName

            Me.grd.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("Current_Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Current_Amount").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("1_30").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("1_30").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("30_60").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("30_60").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("60_90").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("60_90").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("90_180").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("90_180").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("180_270").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("180_270").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("360+").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("360+").TotalFormatString = "N" & DecimalPointInValue
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillCombo(Optional Condition As String = "")
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
            ElseIf Condition = "SubSubAccount" Then
                FillUltraDropDown(Me.cmbSubSubAccount, "SELECT main_sub_sub_id, sub_sub_title AS Title, sub_sub_code AS Code, account_type AS AccountType FROM tblCOAMainSubSub WHERE account_type = 'Vendor' ORDER BY sub_sub_code ASC", True)
                Me.cmbSubSubAccount.Rows(0).Activate()
                If Me.cmbSubSubAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSubSubAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try


        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim lbl As New Label
        Try
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()
            FillGrid()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            AddRptParam("@Aging", strAging)
            AddRptParam("@1stAging", str1stAging)
            AddRptParam("@1stAgingName", str1stAgingName)
            AddRptParam("@2ndAging", str2ndAging)
            AddRptParam("@2ndAgingName", str2ndAgingName)
            AddRptParam("@3rdAging", str3rdAging)
            AddRptParam("@3rdAgingName", str3rdAgingName)
            AddRptParam("@IncludeUnPosted", "" & IIf(Me.chkIncludeUnPosted.Checked = True, 1, 0) & "")
            ShowReport("AgeingPayable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 23:59:59"), False)
        Catch ex As Exception
            ShowErrorMessage("Error occurred while show report: " & ex.Message)
        End Try
    End Sub
    Private Sub grd_LinkClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.LinkClicked
        Me.Cursor = Cursors.WaitCursor
        Dim CreditDays As Double = 0
        Try
            If Me.grd.RowCount > 0 Then
                CreditDays = Val(Me.grd.GetRow.Cells("CreditDays").Value)
            End If
            If e.Column.Key = "Balance" Then
                'TFS3415: Waqar Raza: Commented this line because TUV wants to see data where its Closing Date will be From Date
                'Start  TFS3415
                '_FromDate = "2001-1-1 00:00:00"
                _FromDate = Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)).AddDays(1)
                'End TFS3415
                _ToDate = Date.Now
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked
                If chkShowWithOutCostCenter.Checked = True Then
                    rptLedger.Costid = 0
                End If
                rptLedger.GetLedger()
            ElseIf e.Column.Key = "Current_Amount" Then
                _FromDate = Date.Now.AddDays(-CreditDays)
                _ToDate = Date.Now
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked
                rptLedger.GetLedgerPayables()
            ElseIf e.Column.Key = "1_30" Then
                _FromDate = Date.Now.AddDays(-(str1stAging + CreditDays))
                _ToDate = Date.Now.AddDays(-(strAging + CreditDays))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked
                rptLedger.GetLedgerPayables()
            ElseIf e.Column.Key = "30_60" Then
                _FromDate = Date.Now.AddDays(-(str2ndAging + CreditDays))
                _ToDate = Date.Now.AddDays(-(str1stAging + strAging + CreditDays))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked
                rptLedger.GetLedgerPayables()
            ElseIf e.Column.Key = "60_90" Then
                _FromDate = Date.Now.AddDays(-(str3rdAging + CreditDays))
                _ToDate = Date.Now.AddDays(-(str2ndAging + strAging + CreditDays))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked
                rptLedger.GetLedgerPayables()
            ElseIf e.Column.Key = "90_180" Then
                _FromDate = Date.Now.AddDays(-(str4thAging + CreditDays))
                _ToDate = Date.Now.AddDays(-(str3rdAging + strAging + CreditDays))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked
                rptLedger.GetLedgerPayables()
            ElseIf e.Column.Key = "180_270" Then
                _FromDate = Date.Now.AddDays(-(str5thAging + CreditDays))
                _ToDate = Date.Now.AddDays(-(str4thAging + strAging + CreditDays))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked
                rptLedger.GetLedgerPayables()
            ElseIf e.Column.Key = "360+" Then
                _FromDate = "2001-1-1 00:00:00"
                _ToDate = Date.Now.AddDays(-(str5thAging + strAging + CreditDays))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked
                rptLedger.GetLedgerPayables()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub



    Private Sub cmbFormate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFormate.SelectedIndexChanged
        Try
            If IsOpenForm = False Then Exit Sub
            strAging = Val(CType(Me.cmbFormate.SelectedItem, DataRowView).Item("Aging").ToString)
            str1stAging = Val(CType(Me.cmbFormate.SelectedItem, DataRowView).Item("1stAging").ToString)
            str2ndAging = Val(CType(Me.cmbFormate.SelectedItem, DataRowView).Item("2ndAging").ToString)
            str3rdAging = Val(CType(Me.cmbFormate.SelectedItem, DataRowView).Item("3rdAging").ToString)
            str4thAging = Val(CType(Me.cmbFormate.SelectedItem, DataRowView).Item("4thAging").ToString)
            str5thAging = Val(CType(Me.cmbFormate.SelectedItem, DataRowView).Item("5thAging").ToString)
            str6thAging = Val(CType(Me.cmbFormate.SelectedItem, DataRowView).Item("6thAging").ToString)
            str1stAgingName = CType(Me.cmbFormate.SelectedItem, DataRowView).Item("1stAgingName").ToString
            str2ndAgingName = CType(Me.cmbFormate.SelectedItem, DataRowView).Item("2ndAgingName").ToString
            str3rdAgingName = CType(Me.cmbFormate.SelectedItem, DataRowView).Item("3rdAgingName").ToString
            str4thAgingName = CType(Me.cmbFormate.SelectedItem, DataRowView).Item("4thAgingName").ToString
            str5thAgingName = CType(Me.cmbFormate.SelectedItem, DataRowView).Item("5thAgingName").ToString
            str6thAgingName = CType(Me.cmbFormate.SelectedItem, DataRowView).Item("6thAgingName").ToString
            btnRefresh_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub lnkRefresh_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRefresh.LinkClicked
        Try

            dtData = New DataTable
            dtData.TableName = "tblAgingTemplate"
            dtData.Columns.Add("Id", GetType(System.Int32))
            dtData.Columns("Id").AutoIncrement = True
            dtData.Columns("Id").AutoIncrementSeed = 1
            dtData.Columns("Id").AutoIncrementStep = 1
            dtData.Columns.Add("Format_Name", GetType(System.String))
            dtData.Columns.Add("Aging", GetType(System.Int32))
            dtData.Columns.Add("1stAging", GetType(System.Int32))
            dtData.Columns.Add("1stAgingName", GetType(System.String))
            dtData.Columns.Add("2ndAging", GetType(System.Int32))
            dtData.Columns.Add("2ndAgingName", GetType(System.String))
            dtData.Columns.Add("3rdAging", GetType(System.Int32))
            dtData.Columns.Add("3rdAgingName", GetType(System.String))
            dtData.Columns.Add("4thAging", GetType(System.Int32))
            dtData.Columns.Add("4thAgingName", GetType(System.String))
            dtData.Columns.Add("5thAging", GetType(System.Int32))
            dtData.Columns.Add("5thAgingName", GetType(System.String))
            dtData.Columns.Add("6thAging", GetType(System.Int32))
            dtData.Columns.Add("6thAgingName", GetType(System.String))

            If IO.File.Exists(Application.StartupPath & "\TemplateAgingBalanceNew.xml") Then
                dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
            Else
                Dim dr As DataRow
                dr = dtData.NewRow
                dr(1) = "Default"
                dr(2) = 1
                dr(3) = 30
                dr(4) = "1_30"
                dr(5) = 60
                dr(6) = "30_60"
                dr(7) = 90
                dr(8) = "60_90"
                dr(9) = 180
                dr(10) = "90_180"
                dr(11) = 270
                dr(12) = "180_270"
                dr(13) = 360
                dr(14) = "360+"
                dtData.Rows.Add(dr)
                dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
            End If
            Me.cmbFormate.ValueMember = "Id"
            Me.cmbFormate.DisplayMember = "Format_Name"
            Me.cmbFormate.DataSource = dtData
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddFormate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFormate.Click, btnAddLayout.Click
        Try
            If frmAgingBalancesTemplateNew.ShowDialog = Windows.Forms.DialogResult.Yes Then
                dtData = New DataTable
                dtData.TableName = "tblAgingTemplate"
                dtData.Columns.Add("Id", GetType(System.Int32))
                dtData.Columns("Id").AutoIncrement = True
                dtData.Columns("Id").AutoIncrementSeed = 1
                dtData.Columns("Id").AutoIncrementStep = 1
                dtData.Columns.Add("Format_Name", GetType(System.String))
                dtData.Columns.Add("Aging", GetType(System.Int32))
                dtData.Columns.Add("1stAging", GetType(System.Int32))
                dtData.Columns.Add("1stAgingName", GetType(System.String))
                dtData.Columns.Add("2ndAging", GetType(System.Int32))
                dtData.Columns.Add("2ndAgingName", GetType(System.String))
                dtData.Columns.Add("3rdAging", GetType(System.Int32))
                dtData.Columns.Add("3rdAgingName", GetType(System.String))
                dtData.Columns.Add("4thAging", GetType(System.Int32))
                dtData.Columns.Add("4thAgingName", GetType(System.String))
                dtData.Columns.Add("5thAging", GetType(System.Int32))
                dtData.Columns.Add("5thAgingName", GetType(System.String))
                dtData.Columns.Add("6thAging", GetType(System.Int32))
                dtData.Columns.Add("6thAgingName", GetType(System.String))
                If IO.File.Exists(Application.StartupPath & "\TemplateAgingBalanceNew.xml") Then
                    dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                Else
                    Dim dr As DataRow
                    dr = dtData.NewRow
                    dr(1) = "Default"
                    dr(2) = 1
                    dr(3) = 30
                    dr(4) = "1_30"
                    dr(5) = 60
                    dr(6) = "30_60"
                    dr(7) = 90
                    dr(8) = "60_90"
                    dr(9) = 180
                    dr(10) = "90_180"
                    dr(11) = 270
                    dr(12) = "180_270"
                    dr(13) = 360
                    dr(14) = "360+"
                    dtData.Rows.Add(dr)
                    dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                    dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                End If
                Me.cmbFormate.ValueMember = "Id"
                Me.cmbFormate.DisplayMember = "Format_Name"
                Me.cmbFormate.DataSource = dtData
                CtrlGrdBar1_Load(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Custom Aging Payable Report" & Chr(10) & "UpTo : " & Now.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ShowInvoiceWiseAgingData()
        Try
            Application.DoEvents()

            Dim Str As String = " Exec PayablesInvoiceWise '" & Me.DateTimePicker1.Value.ToString("dd-MMM-yyyy") & " 00:00:00','" & Me.DateTimePicker2.Value.ToString("dd-MMM-yyyy") & " 23:59:59'"

            Dim adp As New SqlDataAdapter("PayablesInvoiceWise '" & Me.DateTimePicker1.Value.ToString("dd-MMM-yyyy") & " 00:00:00','" & Me.DateTimePicker2.Value.ToString("dd-MMM-yyyy") & " 23:59:59'", SQLHelper.CON_STR)
            Dim dt As New DataTable
            adp.Fill(dt)
            Me.grdInvoiceWise.RootTable.Columns("PurchaseOrderDate").FormatString = "dd-MMM-yyyy"
            If dt.Rows.Count > 0 Then
                Dim CustomerId As Integer = 0
                Dim AvailableBalance As Double = 0

                For Each row As DataRow In dt.Rows

                    '//Setting values for every 1st record of a customer
                    If CustomerId <> Val(row.Item("AccountId").ToString) Then
                        CustomerId = Val(row.Item("AccountId").ToString)
                        AvailableBalance = (Val(row.Item("PaymentAmount").ToString) - Val(row.Item("OpeningBalance").ToString))
                    End If

                    '// Filling Due amount
                    If AvailableBalance > 0 Then

                        If AvailableBalance < Val(row.Item("InvoiceAmount").ToString) Then
                            row.Item("DueAmount") = Val(row.Item("InvoiceAmount").ToString) - AvailableBalance
                            AvailableBalance = 0
                        Else
                            AvailableBalance = AvailableBalance - Val(row.Item("InvoiceAmount").ToString)
                        End If

                    Else
                        row.Item("DueAmount") = row.Item("InvoiceAmount")

                    End If
                Next

            End If
            Me.grdInvoiceWise.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally

        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSendSMS_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSMSTempSettings_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnAddTemplate_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub UltraTabControl1_TabIndexChanged(sender As Object, e As EventArgs) Handles UltraTabControl1.TabIndexChanged
        Try
            If Not Me.UltraTabControl1.SelectedTab Is Nothing Then
                If Me.UltraTabControl1.SelectedTab.TabPage.Name = UltraTabControl1.Tabs(1).TabPage.Name Then
                    Me.ShowInvoiceWiseAgingData()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ShowInvoiceWiseAgingData()
            CtrlGrdBar2_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.ShowInvoiceWiseAgingData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdInvoiceWise.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdInvoiceWise.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdInvoiceWise.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Invoice Wise Payable Report" & Chr(10) & "From Date: " & DateTimePicker1.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & DateTimePicker2.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1441 : Get all scheduled payables and apply grid settings too
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>'Ali Faisal : TFS1441 : 12-Sep-2017</remarks>
    Private Sub btnShowSchPayables_Click(sender As Object, e As EventArgs) Handles btnShowSchPayables.Click
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT Sch.PayScheduleId SchId, Sch.OrderId AS POId, PO.PurchaseOrderNo AS PONo, PO.PurchaseOrderDate AS PODate, COA.detail_code VendorCode, COA.detail_title Vendor, Sch.SchDate DueDate, Sch.Amount, Sch.PaymentStatus Status, CASE WHEN DATEDIFF(dd, Sch.SchDate, GETDATE()) > 0 THEN DATEDIFF(dd, Sch.SchDate, GETDATE()) ELSE 0 END OverDue FROM tblPaymentSchedule AS Sch INNER JOIN PurchaseOrderMasterTable AS PO ON Sch.OrderId = PO.PurchaseOrderId INNER JOIN vwCOADetail AS COA ON PO.VendorId = COA.coa_detail_id WHERE (Sch.SchDate <= '" & Me.dtpSchDate.Value.ToString("yyyy-MM-dd 23:59:59") & "')  AND COA.account_type = 'Vendor'  " & IIf(chkGetPaidUnpaid.Checked = True, "", "And PaymentStatus <> 'Paid'") & "  ORDER BY Sch.SchDate ASC"
            dt = GetDataTable(str)
            Me.grdSchPayables.DataSource = dt
            Me.grdSchPayables.RetrieveStructure()
            'Ali Faisal : TFS1441 : Add a column for selection of rows
            Dim SCol As New Janus.Windows.GridEX.GridEXColumn("Selection")
            SCol.ActAsSelector = True
            Me.grdSchPayables.RootTable.Columns.Insert(0, SCol)
            Me.grdSchPayables.RootTable.Columns(0).Width = 22
            Me.grdSchPayables.RootTable.Columns("Vendor").Width = 222
            'Ali Faisal : TFS1441 : Add new button of paid
            If Me.grdSchPayables.RootTable.Columns.Contains("Paid") = False Then
                Me.grdSchPayables.RootTable.Columns.Add("Paid")
                Me.grdSchPayables.RootTable.Columns("Paid").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdSchPayables.RootTable.Columns("Paid").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdSchPayables.RootTable.Columns("Paid").ButtonText = "Paid"
                Me.grdSchPayables.RootTable.Columns("Paid").Key = "Paid"
                Me.grdSchPayables.RootTable.Columns("Paid").Caption = "Action"
                Me.grdSchPayables.RootTable.Columns("Paid").Width = 90
                Me.grdSchPayables.RootTable.Columns("Paid").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdSchPayables.RootTable.Columns("Paid").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            End If
            'Ali Faisal : TFS1441 : Apply visibility and formating of columns
            Me.grdSchPayables.ColumnAutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCellsAndHeader
            Me.grdSchPayables.RootTable.Columns("SchId").Visible = False
            Me.grdSchPayables.RootTable.Columns("POId").Visible = False
            Me.grdSchPayables.RootTable.Columns("VendorCode").Visible = False
            Me.grdSchPayables.RootTable.Columns("DueDate").FormatString = str_DisplayDateFormat
            Me.grdSchPayables.RootTable.Columns("PODate").FormatString = str_DisplayDateFormat
            Me.grdSchPayables.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSchPayables.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSchPayables.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSchPayables.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.grdSchPayables.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue
            'Ali Faisal : TFS1441 : Setting edit types
            Me.grdSchPayables.RootTable.Columns("SchId").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSchPayables.RootTable.Columns("POId").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSchPayables.RootTable.Columns("PONo").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSchPayables.RootTable.Columns("PODate").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSchPayables.RootTable.Columns("VendorCode").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSchPayables.RootTable.Columns("Vendor").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSchPayables.RootTable.Columns("Amount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSchPayables.RootTable.Columns("Status").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSchPayables.RootTable.Columns("OverDue").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSchPayables.RootTable.Columns("DueDate").EditType = Janus.Windows.GridEX.EditType.CalendarCombo
            'Ali Faisal : TFS1441 : Column back color changed
            Me.grdSchPayables.RootTable.Columns("DueDate").CellStyle.BackColor = Color.LightYellow
            CtrlGrdBar3_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1441 : Grid Control settings
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSchPayables.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSchPayables.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdSchPayables.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Scheduled PO Payable" & Chr(10) & "Upto Date: " & dtpSchDate.Value.ToString("dd-MM-yyyy").ToString
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1441 : Update trans status to Paid on button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPaid_Click(sender As Object, e As EventArgs) Handles btnPaid.Click
        Try
            Dim Con As New SqlConnection(SQLHelper.CON_STR)
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim objTrans As SqlTransaction = Con.BeginTransaction
            Dim strSQL As String = String.Empty
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSchPayables.GetCheckedRows
                strSQL = "Update tblPaymentSchedule Set PaymentStatus = N'Paid' Where PayScheduleId = " & Me.grdSchPayables.GetRow.Cells("SchId").Value & " "
                SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
            Next
            objTrans.Commit()
            btnShowSchPayables_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1441 : Update trans status to paid on grid column button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSchPayables_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSchPayables.ColumnButtonClick
        Try
            Dim Con As New SqlConnection(SQLHelper.CON_STR)
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim objTrans As SqlTransaction = Con.BeginTransaction
            Dim strSQL As String = String.Empty
            strSQL = "Update tblPaymentSchedule Set PaymentStatus = N'Paid' Where PayScheduleId = " & Me.grdSchPayables.GetRow.Cells("SchId").Value & " "
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
            objTrans.Commit()
            Me.btnShowSchPayables_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1441 : Formating rows to set colors in rows and other things
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSchPayables_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSchPayables.FormattingRow
        Try
            Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
            If e.Row.Cells("Status").Text <> "" AndAlso e.Row.Cells("Status").Text = "UnPaid" Then
                rowStyle.BackColor = Color.LightGray
                e.Row.RowStyle = rowStyle
            End If
            If e.Row.Cells("OverDue").Text <> "" AndAlso e.Row.Cells("OverDue").Text > 0 Then
                rowStyle.BackColor = Color.Yellow
                e.Row.RowStyle = rowStyle
                e.Row.BeginEdit()
                e.Row.Cells("Status").Text = "OverDue"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1441 : Update due date on cell date changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSchPayables_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSchPayables.CellEdited
        Try
            If Not Me.grdSchPayables.GetRow.Cells("Status").Text = "Paid" Then
                Dim Con As New SqlConnection(SQLHelper.CON_STR)
                If Con.State = ConnectionState.Closed Then Con.Open()
                Dim objTrans As SqlTransaction = Con.BeginTransaction
                Dim strSQL As String = String.Empty
                strSQL = "Update tblPaymentSchedule Set SchDate = '" & Me.grdSchPayables.GetRow.Cells("DueDate").Value.ToString & "' Where PayScheduleId = " & Me.grdSchPayables.GetRow.Cells("SchId").Value & " "
                SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
                objTrans.Commit()
                btnShowSchPayables_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Me.Cursor = Cursors.WaitCursor
        Dim lbl As New Label
        Try
            dtData = New DataTable
            dtData.TableName = "tblAgingTemplate"
            dtData.Columns.Add("Id", GetType(System.Int32))
            dtData.Columns("Id").AutoIncrement = True
            dtData.Columns("Id").AutoIncrementSeed = 1
            dtData.Columns("Id").AutoIncrementStep = 1
            dtData.Columns.Add("Format_Name", GetType(System.String))
            dtData.Columns.Add("Aging", GetType(System.Int32))
            dtData.Columns.Add("1stAging", GetType(System.Int32))
            dtData.Columns.Add("1stAgingName", GetType(System.String))
            dtData.Columns.Add("2ndAging", GetType(System.Int32))
            dtData.Columns.Add("2ndAgingName", GetType(System.String))
            dtData.Columns.Add("3rdAging", GetType(System.Int32))
            dtData.Columns.Add("3rdAgingName", GetType(System.String))
            dtData.Columns.Add("4thAging", GetType(System.Int32))
            dtData.Columns.Add("4thAgingName", GetType(System.String))
            dtData.Columns.Add("5thAging", GetType(System.Int32))
            dtData.Columns.Add("5thAgingName", GetType(System.String))
            dtData.Columns.Add("6thAging", GetType(System.Int32))
            dtData.Columns.Add("6thAgingName", GetType(System.String))
            If IO.File.Exists(Application.StartupPath & "\TemplateAgingBalanceNew.xml") Then
                dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
            Else
                Dim dr As DataRow
                dr = dtData.NewRow
                dr(1) = "Default"
                dr(2) = 1
                dr(3) = 30
                dr(4) = "1_30"
                dr(5) = 60
                dr(6) = "30_60"
                dr(7) = 90
                dr(8) = "60_90"
                dr(9) = 180
                dr(10) = "90_180"
                dr(11) = 270
                dr(12) = "180_270"
                dr(13) = 360
                dr(14) = "360+"
                dtData.Rows.Add(dr)
                dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
            End If

            Me.cmbFormate.ValueMember = "Id"
            Me.cmbFormate.DisplayMember = "Format_Name"
            Me.cmbFormate.DataSource = dtData

            IsOpenForm = True
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()
            FillGrid()
            GetSecurityRights()
            Me.UltraTabControl1.Tabs(1).Selected = True
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            FillCombo("HeadCostCenter")
            FillCombo("CostCenter")
            FillCombo("SubSubAccount")
            Me.cmbSubSubAccount.Value = 0
            Me.chkIncludeUnPosted.Checked = True
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub lstHeadCostCenter_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstHeadCostCenter.SelectedIndexChaned
        Try
            If Me.lstHeadCostCenter.SelectedIDs.Length > 0 Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1  AND CostCenterGroup IN (" & Me.lstHeadCostCenter.SelectedItems & ")")
            Else
                FillCombo("CostCenter")
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