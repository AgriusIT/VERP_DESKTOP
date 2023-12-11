Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data.SqlClient
Public Class frmGrdRptAgingInstallment
    Dim dtData As DataTable
    Dim dt As DataTable
    Public _FromDate As DateTime
    Public _ToDate As DateTime
    Dim strAging As Integer = 30
    Dim str1stAging As Integer = 60
    Dim str1stAgingName As String = "30_60"
    Dim str2ndAging As Integer = 90
    Dim str2ndAgingName As String = "60_90"
    Dim str3rdAging As Integer = 90
    Dim str3rdAgingName As String = "90+"
    Dim IsOpenForm As Boolean = False

    Private Sub frmGrdRptAgingInstallment_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                btnPrint_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmGrdRptAgingInstallment_Load(sender As Object, e As EventArgs) Handles Me.Load
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
            lbl.Text = "Loading please wait..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()
            FillGrid()
            GetSecurityRights()
        Catch ex As Exception
        Finally
            lbl.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub GetSecurityRights(Optional ByVal Condition As String = "")
        Try
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "Not Show Installment Balance" Then
                    Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.btnPrint.Enabled = False
                Else
                    Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    Me.btnPrint.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillGrid()
        Try
            Dim str As String = "SP_Rpt_Installment"
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Aging Installments Report" & Chr(10) & "UpTo: " & Now.ToString("dd-MM-yyyy").ToString & ""
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
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
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            ShowReport("AgeingInstallment")
        Catch ex As Exception
            ShowErrorMessage("Error occurred while show report: " & ex.Message)
        End Try
    End Sub

    Private Sub grd_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.LinkClicked
        Me.Cursor = Cursors.WaitCursor
        Try
            If e.Column.Key = "Current" Then
                _FromDate = "2007-1-1 00:00:00"
                _ToDate = Date.Now
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.cmbCostCenter.SelectedIndex = 0
                rptLedger.GetLedger()
            ElseIf e.Column.Key = "Current_30" Then
                _FromDate = Date.Now.AddDays(-strAging)
                _ToDate = Date.Now
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.cmbCostCenter.SelectedIndex = 0
                rptLedger.GetLedger()
            ElseIf e.Column.Key = "30_60" Then
                _FromDate = Date.Now.AddDays(-str1stAging)
                _ToDate = Date.Now.AddDays(-(strAging + 1))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.cmbCostCenter.SelectedIndex = 0
                rptLedger.GetLedger()
            ElseIf e.Column.Key = "60_90" Then
                _FromDate = Date.Now.AddDays(-str2ndAging)
                _ToDate = Date.Now.AddDays(-(str1stAging + 1))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.cmbCostCenter.SelectedIndex = 0
                rptLedger.GetLedger()
            ElseIf e.Column.Key = "90+" Then
                _FromDate = "2007-1-1 00:00:00"
                _ToDate = Date.Now.AddDays(-(str3rdAging + 1))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                rptLedger.cmbCostCenter.SelectedIndex = 0
                rptLedger.GetLedger()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnAddLayout_Click(sender As Object, e As EventArgs) Handles btnAddLayout.Click
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
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbFormate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFormate.SelectedIndexChanged
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
End Class