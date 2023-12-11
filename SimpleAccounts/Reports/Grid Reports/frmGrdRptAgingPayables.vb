''29-Jan-2014 Task:2399 Aging Payable And Receivable Balance Match With ledger
''03-Mar-2014  Task:M22  Aging Payables Balances Problem
''22-Aug-2014 Task:2798 Imran Ali Type Wise Group Total In Payables/Receivables #Added ControlBar On ToolStrip And Set Group Wise Total On Design Time
''22-Sep-2014 TaskM229141 Imran Ali Not Show Balance User Based Rights
'' 12-06-2017 TASK901 Hidden column PaymentAmount by Ameen
''TAKS TFS4897 Ayesha Rehman done on 08-11-2018. Property type filter is not working accurately on Payable and Receivable report.
Imports SBModel
Imports SBDal
Imports SBUtility
Imports System.Data.SqlClient

Public Class frmGrdRptAgingPayables
    Dim dt As DataTable
    Public _FromDate As DateTime
    Public _ToDate As DateTime
    Public DrillDown As Boolean = False
    Dim dtData As DataTable
    Dim strAging As Integer = 30
    Dim str1stAging As Integer = 60
    Dim str1stAgingName As String = "30_60"
    Dim str2ndAging As Integer = 90
    Dim str2ndAgingName As String = "60_90"
    Dim str3rdAging As Integer = 90
    Dim str3rdAgingName As String = "90+"
    Dim IsOpenForm As Boolean = False
    Dim ArePropertyFilters As Boolean = False
    ''22-Sep-2014 TaskM229141 Imran Ali Not Show Balance User Based Rights
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
                    ' Me.btnPrint.Enabled = False
                Else
                    Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ' Me.btnPrint.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'End Task:M229141
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
            If IO.File.Exists(Application.StartupPath & "\TemplateAgingBalance.xml") Then
                dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalance.xml")
            Else
                Dim dr As DataRow
                dr = dtData.NewRow
                dr(1) = "Default"
                dr(2) = 30
                dr(3) = 60
                dr(4) = "30_60"
                dr(5) = 90
                dr(6) = "60_90"
                dr(7) = 90
                dr(8) = "90+"
                dtData.Rows.Add(dr)
                dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalance.xml")
                dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalance.xml")
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


            'If BackgroundWorker1.IsBusy Then Exit Sub
            'BackgroundWorker1.RunWorkerAsync()
            'Do While BackgroundWorker1.IsBusy
            '    Application.DoEvents()
            'Loop
            If Not getConfigValueByType("PropertyFiltersOnReports").ToString = "Error" Then
                ArePropertyFilters = Convert.ToBoolean(getConfigValueByType("PropertyFiltersOnReports").ToString)
            End If
            If ArePropertyFilters = True Then
                Me.cmbPropertyType.Visible = True
                Me.lblPropertyType.Visible = True
            Else
                Me.cmbPropertyType.Visible = False
                Me.lblPropertyType.Visible = False
            End If
            FillDropDown(cmbPropertyType, "SELECT PropertyTypeId, PropertyType FROM PropertyType", True)
            FillGrid()
            GetSecurityRights() ''22-Sep-2014 TaskM229141 Imran Ali Not Show Balance User Based Rights

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub FillGrid()
        Try
            Dim str As String = "SP_Rpt_Payable '" & Date.Now.ToString("yyyy-M-d 00:00:00") & "', " & strAging & ", " & str1stAging & ", '" & str1stAgingName & "'," & str2ndAging & ", '" & str2ndAgingName & "'," & str3rdAging & ", '" & str3rdAgingName & "', " & IIf(Me.chkIncludeUnPosted.Checked = True, 1, 0) & ", " & IIf(cmbPropertyType.SelectedValue > 0, cmbPropertyType.SelectedValue, 0) & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RootTable.Columns("30_60").Caption = str1stAgingName
            Me.grd.RootTable.Columns("60_90").Caption = str2ndAgingName
            Me.grd.RootTable.Columns("90+").Caption = str3rdAgingName
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            'Waqar: Added these lines to remove Zero Balanced rows from grid
            'Start Task
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                Dim balancerounding As Integer = Val(row.Cells("Balance").Value)
                If balancerounding = 0 Then
                    row.Delete()
                    grd.UpdateData()
                End If
            Next
            'End Task
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

            'If BackgroundWorker1.IsBusy Then Exit Sub
            'BackgroundWorker1.RunWorkerAsync()
            'Do While BackgroundWorker1.IsBusy
            '    Application.DoEvents()
            'Loop

            FillGrid()
            GetSecurityRights() ''22-Sep-2014 TaskM229141 Imran Ali Not Show Balance User Based Rights
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
            AddRptParam("@PropertyType", "" & IIf(cmbPropertyType.SelectedValue > 0, cmbPropertyType.SelectedValue, 0) & "")
            ShowReport("AgeingPayable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 23:59:59"), False)
        Catch ex As Exception
            ShowErrorMessage("Error occurred while show report: " & ex.Message)
        End Try
    End Sub
    Private Sub grd_LinkClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.LinkClicked
        Me.Cursor = Cursors.WaitCursor
        Try
            If e.Column.Key = "Balance" Then
                'TFS3415: Waqar Raza: Commented this line because TUV wants to see data where its Closing Date will be From Date
                'Start  TFS3415
                '_FromDate = "2001-1-1 00:00:00"
                _FromDate = Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)).AddDays(1)
                'End TFS3415
                _ToDate = Date.Now
                'AddRptParam("@FromDate", _FromDate.ToString("yyyy-M-d 00:00:00"))
                'AddRptParam("@ToDate", _ToDate.ToString("yyyy-M-d 23:59:59"))
                'ShowReport("Ledger", "{SP_Rpt_Ledger.coa_detail_id}=" & Me.GridEX1.GetRow.Cells("coa_detail_id").Value & "")
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab  'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                'rptLedger.cmbCostCenter.SelectedValue = Me.cmbCostCenter.Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.PropertyType = Me.cmbPropertyType.SelectedValue
                rptLedger.GetLedger()
            ElseIf e.Column.Key = "Current_Amount" Then
                _FromDate = Date.Now.AddDays(-strAging)
                _ToDate = Date.Now
                'AddRptParam("@FromDate", _FromDate.ToString("yyyy-M-d 00:00:00"))
                'AddRptParam("@ToDate", _ToDate.ToString("yyyy-M-d 23:59:59"))
                'ShowReport("Ledger", "{SP_Rpt_Ledger.coa_detail_id}=" & Me.GridEX1.GetRow.Cells("coa_detail_id").Value & "")
                'rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(1).TabPage.Tab
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab 'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                'rptLedger.cmbCostCenter.SelectedValue = Me.cmbCostCenter.Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.PropertyType = Me.cmbPropertyType.SelectedValue
                rptLedger.GetLedger()
            ElseIf e.Column.Key = "30_60" Then
                _FromDate = Date.Now.AddDays(-str1stAging)
                _ToDate = Date.Now.AddDays(-(strAging + 1))
                'AddRptParam("@FromDate", _FromDate.ToString("yyyy-M-d 00:00:00"))
                'AddRptParam("@ToDate", _ToDate.ToString("yyyy-M-d 23:59:59"))
                'ShowReport("Ledger", "{SP_Rpt_Ledger.coa_detail_id}=" & Me.GridEX1.GetRow.Cells("coa_detail_id").Value & "")
                'rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(1).TabPage.Tab
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab 'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                'rptLedger.cmbCostCenter.SelectedValue = Me.cmbCostCenter.Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.PropertyType = Me.cmbPropertyType.SelectedValue
                rptLedger.GetLedger()
            ElseIf e.Column.Key = "60_90" Then
                _FromDate = Date.Now.AddDays(-str2ndAging)
                _ToDate = Date.Now.AddDays(-(str1stAging + 1))
                'AddRptParam("@FromDate", _FromDate.ToString("yyyy-M-d 00:00:00"))
                'AddRptParam("@ToDate", _ToDate.ToString("yyyy-M-d 23:59:59"))
                'ShowReport("Ledger", "{SP_Rpt_Ledger.coa_detail_id}=" & Me.GridEX1.GetRow.Cells("coa_detail_id").Value & "")
                'rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(1).TabPage.Tab
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab 'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                'rptLedger.cmbCostCenter.SelectedValue = Me.cmbCostCenter.Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.PropertyType = Me.cmbPropertyType.SelectedValue
                rptLedger.GetLedger()
            ElseIf e.Column.Key = "90+" Then
                _FromDate = "2007-1-1 00:00:00"
                _ToDate = Date.Now.AddDays(-(str3rdAging + 1))
                'AddRptParam("@FromDate", _FromDate.ToString("yyyy-M-d 00:00:00"))
                'AddRptParam("@ToDate", _ToDate.ToString("yyyy-M-d 23:59:59"))
                'ShowReport("Ledger", "{SP_Rpt_Ledger.coa_detail_id}=" & Me.GridEX1.GetRow.Cells("coa_detail_id").Value & "")
                'rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(1).TabPage.Tab
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab 'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                'rptLedger.cmbCostCenter.SelectedValue = Me.cmbCostCenter.Value
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.PropertyType = Me.cmbPropertyType.SelectedValue
                rptLedger.GetLedger()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

  

    Private Sub cmbFormate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFormate.SelectedIndexChanged, cmbPropertyType.SelectedIndexChanged
        Try
            If IsOpenForm = False Then Exit Sub
            strAging = Val(CType(Me.cmbFormate.SelectedItem, DataRowView).Item("Aging").ToString)
            str1stAging = Val(CType(Me.cmbFormate.SelectedItem, DataRowView).Item("1stAging").ToString)
            str2ndAging = Val(CType(Me.cmbFormate.SelectedItem, DataRowView).Item("2ndAging").ToString)
            str3rdAging = Val(CType(Me.cmbFormate.SelectedItem, DataRowView).Item("3rdAging").ToString)
            str1stAgingName = CType(Me.cmbFormate.SelectedItem, DataRowView).Item("1stAgingName").ToString
            str2ndAgingName = CType(Me.cmbFormate.SelectedItem, DataRowView).Item("2ndAgingName").ToString
            str3rdAgingName = CType(Me.cmbFormate.SelectedItem, DataRowView).Item("3rdAgingName").ToString
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

            If IO.File.Exists(Application.StartupPath & "\TemplateAgingBalance.xml") Then
                dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalance.xml")
            Else
                Dim dr As DataRow
                dr = dtData.NewRow
                dr(1) = "Default"
                dr(2) = 30
                dr(3) = 60
                dr(4) = "30_60"
                dr(5) = 90
                dr(6) = "60_90"
                dr(7) = 90
                dr(8) = "90+"
                dtData.Rows.Add(dr)
                dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalance.xml")
                dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalance.xml")
            End If
            Me.cmbFormate.ValueMember = "Id"
            Me.cmbFormate.DisplayMember = "Format_Name"
            Me.cmbFormate.DataSource = dtData
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddFormate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFormate.Click
        Try



            If frmAgingBalancesTemplate.ShowDialog = Windows.Forms.DialogResult.Yes Then
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
                If IO.File.Exists(Application.StartupPath & "\TemplateAgingBalance.xml") Then
                    dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalance.xml")
                Else
                    Dim dr As DataRow
                    dr = dtData.NewRow
                    dr(1) = "Default"
                    dr(2) = 30
                    dr(3) = 60
                    dr(4) = "30_60"
                    dr(5) = 90
                    dr(6) = "60_90"
                    dr(7) = 90
                    dr(8) = "90+"
                    dtData.Rows.Add(dr)
                    dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalance.xml")
                    dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalance.xml")
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


    'Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
    '    Try
    '        Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Aging Payable Report" & Chr(10) & "UpTo: " & Now.ToString("dd-MM-yyyy").ToString & ""
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub


    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Aging Payable Report" & Chr(10) & "UpTo: " & Now.ToString("dd-MM-yyyy").ToString & ""
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ShowInvoiceWiseAgingData()
        Try
            'Me.TabControl1.Visible = False
            'Me.lblProgress.Visible = True
            Application.DoEvents()

            Dim Str As String = " Exec PayablesInvoiceWise '" & Me.DateTimePicker1.Value.ToString("dd-MMM-yyyy") & " 00:00:00','" & Me.DateTimePicker2.Value.ToString("dd-MMM-yyyy") & " 23:59:59'"

            Dim adp As New SqlDataAdapter("PayablesInvoiceWise '" & Me.DateTimePicker1.Value.ToString("dd-MMM-yyyy") & " 00:00:00','" & Me.DateTimePicker2.Value.ToString("dd-MMM-yyyy") & " 23:59:59'", SQLHelper.CON_STR)
            Dim dt As New DataTable
            'dt = ds_InvoiceWiseAging(0)
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
            ' Me.grdInvoiceWise.RetrieveStructure()
            'Dim rpt As CrystalDecisions.CrystalReports.Engine.ReportDocument = CrystalReportViewer1.ReportSource()
            'rpt.Database.Tables(0).SetDataSource(dt)
            'Me.CrystalReportViewer1.RefreshReport()
            ' ds_InvoiceWiseAging.tblInvoiceWiseAgingDataTable = dt
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            'Me.lblProgress.Visible = False
            'Application.DoEvents()
            'Me.TabControl1.Visible = True
            'Application.DoEvents()
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
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
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

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub

    Private Sub UltraTabPageControl1_Paint(sender As Object, e As PaintEventArgs) Handles UltraTabPageControl1.Paint

    End Sub
End Class