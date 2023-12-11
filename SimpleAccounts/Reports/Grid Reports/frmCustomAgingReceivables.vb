''29-Jan-2014 Task:2399 Aging Payable And Receivable Balance Match With ledger
''03-Mar-2014 Task:M23  Imran Ali  Aging Receivables Balance Problem
'' Tasak No 2553 Update The Query Of Receivables 
''17-Apr-2014 TASK:2574 Aging Receivables SortOrder Problem
''changed DataMember Name Customer Type To Type.
''22-Aug-2014 Task:2798 Imran Ali Type Wise Group Total In Payables/Receivables #Added ControlBar On ToolStrip And Set Group Wise Total On Design Time
''22-Sep-2014 TaskM229141 Imran Ali Not Show Balance User Based Rights
'29-04-2016 TASK-417 Muhammad Ameen: Added feature to search by Sub Sub Account too. 
''TASK TFS3425 Muhammad Amin done on 06-06-2018 : Show record of zero or null cost centers.
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class frmCustomAgingReceivables

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
    Dim blnIncludeUnPosted As Boolean = True
    Dim IsOpenForm As Boolean = False
    Public Sub GetSecurityRights(Optional ByVal Condition As String = "")
        Try
            If LoginGroup = "Administrator" Then
                Me.btnPrint.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                Me.CtrlGrdBar2.mGridPrint.Enabled = True
                Me.CtrlGrdBar2.mGridExport.Enabled = True
                Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                Me.CtrlGrdBar3.mGridPrint.Enabled = True
                Me.CtrlGrdBar3.mGridExport.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnPrint.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar2.mGridChooseFielder.Enabled = False
            Me.CtrlGrdBar2.mGridPrint.Enabled = False
            Me.CtrlGrdBar2.mGridExport.Enabled = False
            Me.CtrlGrdBar3.mGridChooseFielder.Enabled = False
            Me.CtrlGrdBar3.mGridPrint.Enabled = False
            Me.CtrlGrdBar3.mGridExport.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                    'ElseIf Rights.Item(i).FormControlName = "Print" Then
                    '    Me.btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                    Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    Me.CtrlGrdBar2.mGridPrint.Enabled = True
                    Me.CtrlGrdBar3.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                    Me.CtrlGrdBar2.mGridExport.Enabled = True
                    Me.CtrlGrdBar3.mGridExport.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
        Try
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "Not Show Receivable Balance" Then
                    Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.CtrlGrdBar2.mGridExport.Enabled = False
                    Me.CtrlGrdBar2.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar3.mGridPrint.Enabled = False
                    Me.CtrlGrdBar3.mGridExport.Enabled = False
                    Me.btnPrint.Enabled = False

                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmGrdRptAgingReceiveables_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.P AndAlso e.Control = True Then
            btnPrint_Click(Nothing, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
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
            lbl.Text = "Loading please wait..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()


            'If BackgroundWorker1.IsBusy Then Exit Sub
            'BackgroundWorker1.RunWorkerAsync()
            'Do While BackgroundWorker1.IsBusy
            '    Application.DoEvents()
            'Loop
            GetSecurityRights() ''22-Sep-2014 TaskM229141 Imran Ali Not Show Balance User Based Rights
            FillCombo()
            FillGrid()
            ' Comment
            Me.DateTimePicker1.Value = Date.Now.AddMonths(-1)
        Catch ex As Exception
        Finally
            lbl.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub FillGrid()
        Try
            Dim str As String = String.Empty
            If Me.chkShowWithOutCostCenter.Checked = False Then
                ''Below line is commented against TASK TFS3350 on 11-07-2018
                str = "SP_Rpt_ReceivableNew '" & dtpToDate.Value.ToString("yyyy-M-d 00:00:00") & "','" & dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', " & strAging & ", " & str1stAging & ", '" & str1stAgingName & "'," & str2ndAging & ", '" & str2ndAgingName & "'," & str3rdAging & ", '" & str3rdAgingName & "', " & str4thAging & ", '" & str4thAgingName & "'," & str5thAging & ", '" & str5thAgingName & "'," & str6thAging & ", '" & str6thAgingName & "'," & IIf(Me.chkIncludeUnPosted.Checked = True, 1, 0) & ", '" & lstSubSubAccount.SelectedIDs & "','" & lstCostCenter.SelectedIDs & "','" & lstRegion.SelectedIDs & "','" & lstZone.SelectedIDs & "','" & lstBelt.SelectedIDs & "'"
                'str = "SP_Rpt_ReceivableNew '" & Date.Now.ToString("yyyy-M-d 23:59:59") & "','" & Date.Now.ToString("yyyy-M-d 23:59:59") & "', " & strAging & ", " & str1stAging & ", '" & str1stAgingName & "'," & str2ndAging & ", '" & str2ndAgingName & "'," & str3rdAging & ", '" & str3rdAgingName & "', " & str4thAging & ", '" & str4thAgingName & "'," & str5thAging & ", '" & str5thAgingName & "'," & str6thAging & ", '" & str6thAgingName & "'," & IIf(Me.chkIncludeUnPosted.Checked = True, 1, 0) & ", '" & IIf(lstSubSubAccount.SelectedIDs.Length > 0, lstSubSubAccount.SelectedIDs, 0) & "','" & IIf(lstCostCenter.SelectedIDs.Length > 0, lstCostCenter.SelectedIDs, 0) & "','" & IIf(lstRegion.SelectedIDs.Length > 0, lstRegion.SelectedIDs, 0) & "','" & IIf(lstZone.SelectedIDs.Length > 0, lstZone.SelectedIDs, 0) & "','" & IIf(lstBelt.SelectedIDs.Length > 0, lstBelt.SelectedIDs, 0) & "'"
            Else ''TASK TFS3425
                ''Below line is commented against TASK TFS3350 ON 11-07-2018
                str = "SP_Rpt_ReceivableNew '" & dtpToDate.Value.ToString("yyyy-M-d 00:00:00") & "','" & dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', " & strAging & ", " & str1stAging & ", '" & str1stAgingName & "'," & str2ndAging & ", '" & str2ndAgingName & "'," & str3rdAging & ", '" & str3rdAgingName & "', " & str4thAging & ", '" & str4thAgingName & "'," & str5thAging & ", '" & str5thAgingName & "'," & str6thAging & ", '" & str6thAgingName & "'," & IIf(Me.chkIncludeUnPosted.Checked = True, 1, 0) & ", '" & lstSubSubAccount.SelectedIDs & "','" & 0 & "','" & lstRegion.SelectedIDs & "','" & lstZone.SelectedIDs & "','" & lstBelt.SelectedIDs & "'"
                ''END TASK TFS3425
                'str = "SP_Rpt_ReceivableNew '" & Date.Now.ToString("yyyy-M-d 23:59:59") & "','" & Date.Now.ToString("yyyy-M-d 23:59:59") & "', " & strAging & ", " & str1stAging & ", '" & str1stAgingName & "'," & str2ndAging & ", '" & str2ndAgingName & "'," & str3rdAging & ", '" & str3rdAgingName & "', " & str4thAging & ", '" & str4thAgingName & "'," & str5thAging & ", '" & str5thAgingName & "'," & str6thAging & ", '" & str6thAgingName & "', " & IIf(Me.chkIncludeUnPosted.Checked = True, 1, 0) & ", '" & IIf(lstSubSubAccount.SelectedIDs.Length > 0, lstSubSubAccount.SelectedIDs, 0) & "','" & 0 & "','" & IIf(lstRegion.SelectedIDs.Length > 0, lstRegion.SelectedIDs, 0) & "', '" & IIf(lstZone.SelectedIDs.Length, lstZone.SelectedIDs, 0) & "', '" & IIf(lstBelt.SelectedIDs.Length > 0, lstBelt.SelectedIDs, 0) & "'"
            End If
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            If grd.RowCount > 0 Then
                Me.UltraTabControl1.Tabs(1).Selected = True
                'TFS3406: Waqar Raza: Added this line to show the message if no record Found
                'Start TFS3406
            Else
                msg_Information("No Record Found")
                'End TFS3406
            End If
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
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try

            'Dim str As String = "SP_Rpt_Receivable '" & Date.Now.ToString("yyyy-M-d 23:59:59") & "'"
            'Dim str As String = "SP_Rpt_Receivable '" & Date.Now.ToString("yyyy-M-d 23:59:59") & "', " & strAging & ", " & str1stAging & ", '" & str1stAgingName & "'," & str2ndAging & ", '" & str2ndAgingName & "'," & str3rdAging & ", '" & str3rdAgingName & "'"
            'dt = GetDataTable(str)

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
            pnlFollowUp.Visible = False
            'If Not Me.cmbSubSub.Value Is Nothing Then
            '    Me.cmbSubSub.Rows(0).Activate()
            'End If
            'FillCombo()
            'cmbRegion.SelectedIndex = 0
            'cmbZone.SelectedIndex = 0
            'cmbBelt.SelectedIndex = 0


            'If BackgroundWorker1.IsBusy Then Exit Sub
            'BackgroundWorker1.RunWorkerAsync()
            'Do While BackgroundWorker1.IsBusy
            '    Application.DoEvents()
            'Loop
            FillGrid()
            GetSecurityRights() ''22-Sep-2014 TaskM229141 Imran Ali Not Show Balance User Based Rights
            ''Comment

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            '            @FromDate DateTime,
            '@ToDate			datetime,
            '@Aging int,
            '@1stAging int,
            '@1stAgingName varchar(300),
            '@2ndAging int,
            '@2ndAgingName varchar(300),
            '@3rdAging int,
            '@3rdAgingName varchar(300),
            '@4thAging int,
            '@4thAgingName varchar(300),
            '@5thAging int,
            '@5thAgingName varchar(300),
            '@6thAging int,
            '@6thAgingName varchar(300),
            '@IncludeUnPosted bit,  
            '@SubSubID varchar(3000) = NULL ,
            '@CostCenterId varchar(3000) , 
            '@RegionId varchar(3000),
            '@ZoneId varchar(3000),
            '@BeltId varchar(3000) 
            GetCrystalReportRights()

            AddRptParam("@FromDate", Date.Now.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("@ToDate", Date.Now.ToString("yyyy-M-d 23:59:59"))


            AddRptParam("@Aging", strAging)
            AddRptParam("@1stAging", str1stAging)
            AddRptParam("@1stAgingName", str1stAgingName)
            AddRptParam("@2ndAging", str2ndAging)
            AddRptParam("@2ndAgingName", str2ndAgingName)
            AddRptParam("@3rdAging", str3rdAging)
            AddRptParam("@3rdAgingName", str3rdAgingName)

            AddRptParam("@4thAging", str4thAging)
            AddRptParam("@4thAgingName", str4thAgingName)
            AddRptParam("@5thAging", str5thAging)
            AddRptParam("@5thAgingName", str5thAgingName)
            AddRptParam("@6thAging", str6thAging)
            AddRptParam("@6thAgingName", str6thAgingName)

            ''Below lines are commented against TASK TFS3350 to send zero in case no item is selected from lists. Dated 11-07-2018
            'AddRptParam("@IncludeUnPosted", "" & IIf(Me.chkIncludeUnPosted.Checked = True, 1, 0) & "")
            'AddRptParam("@SubSubID", lstSubSubAccount.SelectedIDs)
            'AddRptParam("@CostCenterId", lstCostCenter.SelectedIDs)
            'AddRptParam("@RegionId", lstRegion.SelectedIDs)
            'AddRptParam("@ZoneId", lstZone.SelectedIDs)
            'AddRptParam("@BeltId", lstBelt.SelectedIDs)
            If lstSubSubAccount.SelectedIDs.Length > 0 Then
                AddRptParam("@SubSubID", lstSubSubAccount.SelectedIDs)
            Else
                AddRptParam("@SubSubID", "" & 0 & "")
            End If
            If lstCostCenter.SelectedIDs.Length > 0 Then
                AddRptParam("@CostCenterId", lstCostCenter.SelectedIDs)
            Else
                AddRptParam("@CostCenterId", "" & 0 & "")
            End If
            If lstRegion.SelectedIDs.Length > 0 Then
                AddRptParam("@RegionId", lstRegion.SelectedIDs)
            Else
                AddRptParam("@RegionId", "" & 0 & "")
            End If
            If lstZone.SelectedIDs.Length > 0 Then
                AddRptParam("@ZoneId", lstZone.SelectedIDs)
            Else
                AddRptParam("@ZoneId", "" & 0 & "")
            End If
            If lstBelt.SelectedIDs.Length > 0 Then
                AddRptParam("@BeltId", lstBelt.SelectedIDs)
            Else
                AddRptParam("@BeltId", "" & 0 & "")
            End If
            'ShowReport("AgeingReceivable1", "" & IIf(Me.lstSubSubAccount.SelectedIDs <> "", " {SP_Rpt_Receivable.main_sub_sub_id} IN [" & Me.lstSubSubAccount.SelectedIDs & "]", "") & " ", Now.Date.ToString("yyyy-M-d 23:59:59"), Now.Date.ToString("yyyy-M-d 23:59:59"), False) ''{SP_Rpt_Receivable;1.main_sub_sub_id} =
            ShowReport("AgeingReceivable1") ''{SP_Rpt_Receivable;1.main_sub_sub_id} =

        Catch ex As Exception
            ShowErrorMessage("Error occurred while show report: " & ex.Message)
        End Try
    End Sub
    Private Sub GridEX1_LinkClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.LinkClicked
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
                _ToDate = dtpToDate.Value
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab  'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.dptFromDate = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.dptToDate = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.CoaDetailId = grd.GetRow.Cells("coa_detail_id").Value
                Dim CostCenterIds As String = ""
                If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso lstCostCenter.Enabled = True Then
                    CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                    rptLedger.Costid = CostCenterIds
                Else
                    CostCenterIds = ""
                    rptLedger.Costid = ""
                End If
                If chkShowWithOutCostCenter.Checked = True Then
                    CostCenterIds = "0"
                    rptLedger.Costid = 0
                End If
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.Tracking = True
                rptLedger.GetLedger()
                'rptLedger.GetMultiCostCenterRecord(grd.GetRow.Cells("coa_detail_id").Value, CostCenterIds)
            ElseIf e.Column.Key = "Current_Amount" Then
                _FromDate = dtpToDate.Value.AddDays(-CreditDays)
                _ToDate = dtpToDate.Value
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab 'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.dptFromDate = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.dptToDate = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                Dim CostCenterIds As String = ""
                If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso lstCostCenter.Enabled = True Then
                    CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                    rptLedger.Costid = CostCenterIds
                Else
                    CostCenterIds = ""
                    rptLedger.Costid = ""
                End If
                If chkShowWithOutCostCenter.Checked = True Then
                    CostCenterIds = "0"
                    rptLedger.Costid = 0
                End If
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.Tracking = True
                rptLedger.GetLedgerReceiveables()
            ElseIf e.Column.Key = "1_30" Then
                _FromDate = dtpToDate.Value.AddDays(-(str1stAging + CreditDays))
                _ToDate = dtpToDate.Value.AddDays(-(strAging + CreditDays))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab 'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.dptFromDate = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.dptToDate = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                Dim CostCenterIds As String = ""
                If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso lstCostCenter.Enabled = True Then
                    CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                    rptLedger.Costid = CostCenterIds
                Else
                    CostCenterIds = ""
                    rptLedger.Costid = ""
                End If
                If chkShowWithOutCostCenter.Checked = True Then
                    CostCenterIds = "0"
                    rptLedger.Costid = 0
                End If
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.Tracking = True
                rptLedger.GetLedgerReceiveables()
            ElseIf e.Column.Key = "30_60" Then
                _FromDate = dtpToDate.Value.AddDays(-(str2ndAging + CreditDays))
                _ToDate = dtpToDate.Value.AddDays(-(str1stAging + strAging + CreditDays))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab 'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.dptFromDate = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.dptToDate = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                Dim CostCenterIds As String = ""
                If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso lstCostCenter.Enabled = True Then
                    CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                    rptLedger.Costid = CostCenterIds
                Else
                    CostCenterIds = ""
                    rptLedger.Costid = ""
                End If
                If chkShowWithOutCostCenter.Checked = True Then
                    CostCenterIds = "0"
                    rptLedger.Costid = 0
                End If
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.Tracking = True
                rptLedger.GetLedgerReceiveables()
            ElseIf e.Column.Key = "60_90" Then
                _FromDate = dtpToDate.Value.AddDays(-(str3rdAging + CreditDays))
                _ToDate = dtpToDate.Value.AddDays(-(str2ndAging + strAging + CreditDays))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab 'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.dptFromDate = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.dptToDate = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                Dim CostCenterIds As String = ""
                If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso lstCostCenter.Enabled = True Then
                    CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                    rptLedger.Costid = CostCenterIds
                Else
                    CostCenterIds = ""
                    rptLedger.Costid = ""
                End If
                If chkShowWithOutCostCenter.Checked = True Then
                    CostCenterIds = "0"
                    rptLedger.Costid = 0
                End If
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.Tracking = True
                rptLedger.GetLedgerReceiveables()
            ElseIf e.Column.Key = "90_180" Then
                _FromDate = dtpToDate.Value.AddDays(-(str4thAging + CreditDays))
                _ToDate = dtpToDate.Value.AddDays(-(str3rdAging + strAging + CreditDays))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab 'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.dptFromDate = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.dptToDate = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                Dim CostCenterIds As String = ""
                If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso lstCostCenter.Enabled = True Then
                    CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                    rptLedger.Costid = CostCenterIds
                Else
                    CostCenterIds = ""
                    rptLedger.Costid = ""
                End If
                If chkShowWithOutCostCenter.Checked = True Then
                    CostCenterIds = "0"
                    rptLedger.Costid = 0
                End If
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.Tracking = True
                rptLedger.GetLedgerReceiveables()
            ElseIf e.Column.Key = "180_270" Then
                _FromDate = dtpToDate.Value.AddDays(-(str5thAging + CreditDays))
                _ToDate = dtpToDate.Value.AddDays(-(str4thAging + strAging + CreditDays))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab 'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.dptFromDate = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.dptToDate = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                Dim CostCenterIds As String = ""
                If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso lstCostCenter.Enabled = True Then
                    CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                    rptLedger.Costid = CostCenterIds
                Else
                    CostCenterIds = ""
                    rptLedger.Costid = ""
                End If
                If chkShowWithOutCostCenter.Checked = True Then
                    CostCenterIds = "0"
                    rptLedger.Costid = 0
                End If
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.Tracking = True
                rptLedger.GetLedgerReceiveables()
            ElseIf e.Column.Key = "360+" Then
                _FromDate = "2001-1-1 00:00:00"
                _ToDate = dtpToDate.Value.AddDays(-(str5thAging + strAging + CreditDays))
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab 'Task:2399 Aging Payable And Receivable Balance Match With ledger
                frmMain.LoadControl("rptLedger")
                rptLedger.DateTimePicker1.Value = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.DateTimePicker2.Value = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.dptFromDate = _FromDate.ToString("yyyy-M-d 00:00:00")
                rptLedger.dptToDate = _ToDate.ToString("yyyy-M-d 23:59:59")
                rptLedger.cmbAccount.Value = grd.GetRow.Cells("coa_detail_id").Value
                Dim CostCenterIds As String = ""
                If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso lstCostCenter.Enabled = True Then
                    CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                    rptLedger.Costid = CostCenterIds
                Else
                    CostCenterIds = ""
                    rptLedger.Costid = ""
                End If
                If chkShowWithOutCostCenter.Checked = True Then
                    CostCenterIds = "0"
                    rptLedger.Costid = 0
                End If
                rptLedger.chkUnPostedVouchers.Checked = Me.chkIncludeUnPosted.Checked 'False 'Task:M23 Assign Value To Post
                rptLedger.Tracking = True
                rptLedger.GetLedgerReceiveables()
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

    Private Sub btnAddTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTemplate.Click
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
    ''Added Event Completed
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try

            FillGrid()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load

        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Aging Receivable Report" & Chr(10) & "UpTo: " & Now.ToString("dd-MM-yyyy").ToString & ""
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
    Private Sub FillCombo()
        Dim Str As String = ""
        Try
            Str = "Select main_sub_sub_id, sub_sub_title As [Sub Sub Title], account_type As [Account Type] From tblCOAMainSubSub Where account_type='Customer' "
            FillListBox(lstSubSubAccount.ListItem, Str)
            'Me.cmbSubSub.Rows(0).Activate()
            'Me.cmbSubSub.DisplayLayout.Bands(0).Columns(0).Hidden = True
            'Me.cmbSubSub.DisplayLayout.Bands(0).Columns(1).c()

            'Str = "If  exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") " _
            '      & " Select CostCenterID, Name from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights  where UserID = " & LoginUserId & ") And Active = 1 ORDER BY 2 ASC " _
            '      & " Else " _
            '      & " Select CostCenterID, Name from tblDefCostCenter Where Active = 1 ORDER BY 2 ASC "
            FillListBox(Me.lstHeadCostCenter.ListItem, "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
            FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1")
            FillListBox(Me.lstRegion.ListItem, "Select RegionId,RegionName from tblListRegion Where Active=1")
            FillListBox(Me.lstZone.ListItem, "Select ZoneId,ZoneName from tblListZone Where Active=1")
            FillListBox(Me.lstBelt.ListItem, "Select BeltId,BeltName from tblListBelt Where Active=1")
            'TFS3406: Waqar Raza: Added these Lines to Deselect all Lists to make it User Friendly
            'Start TFS3406
            lstSubSubAccount.DeSelect()
            lstHeadCostCenter.DeSelect()
            lstCostCenter.DeSelect()
            lstRegion.DeSelect()
            lstZone.DeSelect()
            lstBelt.DeSelect()
            'End TFS3406
            'Me.cmbCostCenter.Rows(0).Activate()
            'If Me.cmbCostCenter.DisplayLayout.Bands(0).Columns.Count > 0 Then
            '    Me.cmbCostCenter.DisplayLayout.Bands(0).Columns("CostCenterID").Hidden = True
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


#Region "SMS Settings"
    Public Function GetSMSParamters() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("@Customer")
            str.Add("@CustomerType")
            str.Add("@CreditLimit")
            str.Add("@Mobile")
            str.Add("@Phone")
            str.Add("@SaleMan")
            str.Add("@Current_Amount")
            str.Add("@Balance")
            str.Add("@30_60")
            str.Add("@60_90")
            str.Add("@90+")
            'str.Add("@SIRIUS")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSKey() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("Receivables To Customers")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Private Sub btnSMSTempSettings_Click(sender As Object, e As EventArgs) Handles btnSMSTempSettings.Click
        Try
            Dim frmSMS As New frmSMSTemplate
            ApplyStyleSheet(frmSMS)
            frmSMS.cmbKey.DataSource = GetSMSKey()
            frmSMS.lstParameters.DataSource = GetSMSParamters()
            frmSMS.Show()
            frmSMS.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub




    Private Sub btnSendSMS_Click(sender As Object, e As EventArgs) Handles btnSendSMS.Click
        Try

            'Dim strSMSBody As String = String.Empty

            'For Each row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
            '    strSMSBody = String.Empty
            '    strSMSBody += "Receivables To Customers, Customer: " & row.Cells("Customer").Value.ToString & ", Type: " & row.Cells("CustomerType").Value.ToString & ", 30_60: " & row.Cells("30_60").Value.ToString & ", 60_90: " & row.Cells("60_90").Value.ToString & ", " _
            '        & " 90+ :" & row.Cells("90+").Value.ToString & ", Credit Limit:" & row.Cells("CreditLimit").Value.ToString & ", Mobile: " & row.Cells("Mobile").Value.ToString & ", Phone:" & row.Cells("Phone").Value.ToString & "  "

            '    Dim dblTotalQty As Double = 0D
            '    Dim dblTotalAmount As Double = 0D

            '    strSMSBody += " Current Amount: " & row.Cells("Current_Amount").Value & ""
            '    strSMSBody += " Balance: " & row.Cells("Balance").Value & ""
            '    Dim CustomerPhone() As String = row.Cells("Mobile").Value.ToString.Replace(",", ";").Replace("|", ";").Replace("\", ";").Replace("/", ";").Replace("^", ";").Replace("*", ";").Split(";")

            '    If CustomerPhone.Length > 0 Then
            '        For Each strPhone As String In CustomerPhone
            '            If strPhone.Length > 0 Then
            '                Try
            '                    SaveSMSLog(strSMSBody, strPhone, "SMS to Receivable Customers")
            '                Catch ex As Exception
            '                    Throw ex
            '                End Try
            '            End If
            '        Next
            '    End If
            'Next

            Dim strDetailMessage As String = String.Empty

            Dim objTemp As New SMSTemplateParameter
            Dim obj As Object = GetSMSTemplate("Receivables To Customers")
            If obj IsNot Nothing Then
                objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                Dim strMessage As String = objTemp.SMSTemplate
                For Each _row As Janus.Windows.GridEX.GridEXRow In grd.GetCheckedRows
                    strMessage = strMessage.Replace("@Customer", _row.Cells("Customer").Value.ToString).Replace("@CustomerType", _row.Cells("CustomerType").Value.ToString).Replace("@CreditLimit", _row.Cells("CreditLimit").Value.ToString).Replace("@Mobile", _row.Cells("Mobile").Value.ToString).Replace("@Phone", _row.Cells("Phone").Value.ToString).Replace("@SaleMan", _row.Cells("Sale man").Value.ToString).Replace("@Balance", Val(_row.Cells("Balance").Value.ToString)).Replace("@Current_Amount", Val(_row.Cells("Current_Amount").Value.ToString)).Replace("@30_60", _row.Cells("30_60").Value.ToString).Replace("@60_90", _row.Cells("60_90").Value.ToString).Replace("@90+", _row.Cells("90+").Value.ToString)
                    SaveSMSLog(strMessage, _row.Cells("Mobile").Value.ToString, "Receivables To Customers")
                    If GetSMSConfig("Receivables To Customers").EnabledAdmin = True Then
                        For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                            If strMob.Length > 10 Then
                                SaveSMSLog(strMessage, strMob, "Receivables To Customers")
                            End If
                        Next
                    End If

                Next
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
#End Region

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.ShowInvoiceWiseAgingData()
        CtrlGrdBar2_Load(Nothing, Nothing)
    End Sub
    Sub ShowInvoiceWiseAgingData()
        Try
            'Me.TabControl1.Visible = False
            'Me.lblProgress.Visible = True
            Application.DoEvents()

            Dim adp As New SqlDataAdapter("Sp_Invoice_Receipt_Details '" & Me.DateTimePicker1.Value.ToString("dd-MMM-yyyy") & " 00:00:00','" & Me.DateTimePicker2.Value.ToString("dd-MMM-yyyy") & " 23:59:59'", SQLHelper.CON_STR)
            Dim dt As New DataTable
            'dt = ds_InvoiceWiseAging(0)
            adp.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim CustomerId As Integer = 0
                Dim AvailableBalance As Double = 0
                Me.GridEX1.RootTable.Columns("DueDate").FormatString = "dd/MMM/yyyy"
                For Each row As DataRow In dt.Rows

                    '//Setting values for every 1st record of a customer
                    If CustomerId <> Val(row.Item("AccountId").ToString) Then
                        CustomerId = Val(row.Item("AccountId").ToString)
                        AvailableBalance = (Val(row.Item("ReceiptAmount").ToString) - Val(row.Item("OpeningBalance").ToString))
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
            Me.GridEX1.DataSource = dt
            ' Me.GridEX1.RetrieveStructure()
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

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try

            If Me.UltraTabControl1.SelectedTab.TabPage.Name = UltraTabControl1.Tabs(1).TabPage.Name Then
                Me.ShowInvoiceWiseAgingData()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            GetCrystalReportRights()
            ShowReport("rptInvoiceWiseAging", , , , , , , CType(Me.GridEX1.DataSource, DataTable))

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Invoice Wise Receivable Report" & Chr(10) & "From Date: " & DateTimePicker1.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & DateTimePicker2.Value.ToString("dd-MM-yyyy").ToString & ""
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Added by WAQAR RAZA for FolLow_Up history 
    Private Sub grd_Click(sender As Object, e As EventArgs) Handles grd.Click, grd.KeyDown
        Try
            pnlFollowUp.Visible = True
            Dim cid As String = String.Empty
            If grd.GetRow().RowIndex > 0 Then
                cid = Val(Me.grd.GetRow.Cells("coa_detail_id").Value.ToString)
                Dim str As String = String.Empty
                str = "SELECT MAX(FollowUp_Id) AS id, FollowUp_History, FollowUp_Date FROM tblFollowUpReceivables WHERE Customer_Id = " & cid & " group by FollowUp_History, FollowUp_Date"
                dt = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    Me.txtFollowUpRemarks.Text = dt.Rows(0).Item("FollowUp_History").ToString
                    Me.dtpFollowUp.Text = dt.Rows(0).Item("FollowUp_Date").ToString
                Else
                    Me.txtFollowUpRemarks.Text = ""
                    Me.dtpFollowUp.Value = Date.Now
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim cid As String = String.Empty
            cid = Val(Me.grd.GetRow.Cells("coa_detail_id").Value.ToString)
            Dim str As New OleDbCommand
            If Con.State = ConnectionState.Closed Then Con.Open()
            str.Connection = Con
            str.CommandText = "Insert into tblFollowUpReceivables (Customer_Id, FollowUp_History, FollowUp_Date, User_Id) values(" & cid & ", N'" & txtFollowUpRemarks.Text.ToString & "', '" & dtpFollowUp.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & LoginUserId & ") "
            Dim identity As Integer = Convert.ToInt32(str.ExecuteScalar())
            If identity > 0 Then
                msg_Information(str_informSave)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Aging Receivable Report" & Chr(10) & "UpTo: " & Now.ToString("dd-MM-yyyy").ToString & ""
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

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            lnkRefresh_LinkClicked(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

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

    Private Sub lstHeadCostCenter_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstHeadCostCenter.SelectedIndexChaned
        Try
            ''TASK TFS3350
            If Me.lstHeadCostCenter.SelectedIDs.Length > 0 Then
                'Dim CommaSplit As String = Me.lstHeadCostCenter.SelectedIDs.Replace(",", "','")
                'FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1  AND CostCenterGroup IN ('" & CommaSplit & "')") ''TFS3320
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1  AND CostCenterGroup IN (" & Me.lstHeadCostCenter.SelectedItems & ")") ''TFS3320
                Me.lstCostCenter.DeSelect()
            Else
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1")
                Me.lstCostCenter.DeSelect()

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class