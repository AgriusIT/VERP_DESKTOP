Imports SBModel
Public Class frmGrdLCDetail
    Public Enum enmLC
        'LcDoc_Id
        'lcDoc_Dat
        'lcDoc_no
        'Amount
        'PaidAmount
        'LCAmount
        LCdoc_Id
        LCdoc_Date
        LCdoc_No
        Insurrance
        ETD_Date
        ETA_Date
        NN_Date
        BDR_Date
        Clearing_Agent
        DD_Date
        DTB_Date
        CostCenter
        TransporterName
        Freight
        Status
        Remarks
        LCAmount
        PaidAmount
        BalanceAmount
        Count
    End Enum
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdLCDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Back Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If
            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFrom.Value = Date.Today
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-1)
                Me.dtpTo.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpTo.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdLCDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Me.cmbPeriod.Text = "Current Month"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh1.Click
        Try
            btnShow_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.ToolStripProgressBar1.Visible = True
        Try

            Dim str As String = String.Empty
            'str = "Select LcDoc_Id, lcDoc_Date as Date, lcDoc_no as [LC No], LCAmount as [Amount], isnull(PaidAmount,0) as [Opening LC Charges], (ISNULL(LCAmount,0)-isnull(PaidAmount,0)) as [Retire Charges] From tblLetterofCredit WHERE (Convert(varchar, LCDoc_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "
            'str = "Select LcDoc_Id, lcDoc_Date as Date, lcDoc_no as [LC No], IsNull(InsurranceValue,0) as Insurrance, ETD_Date, ETA_Date, NN_Date, BDR_Date, Clearing_Agent as [Clr Agent], DD_Date, DTB_Date, tblDefCostCenter.Name as [Project], TransporterName as Transporter, IsNull(Freight,0) as Freight, LC.[Dispatch Status], tblLetterofCredit.Remarks, LCAmount as [Amount], isnull(PaidAmount,0) as [Opening LC Charges], (ISNULL(LCAmount,0)-isnull(PaidAmount,0)) as [Retire Charges] From tblLetterofCredit LEFT OUTER JOIN tblDefTransporter on tblDefTransporter.TransporterID = tblLetterofCredit.TransporterID LEFT OUTER JOIN tblDefCostCenter on tblDefCostCenter.CostCenterID = tblLetterofCredit.CostCenter  LEFT OUTER JOIN (" _
            '     & " Select DISTINCT IsNull(LCID,0) as LCID, tblStockDispatchStatus.StockDispatchStatusName as [Dispatch Status] From ReceivingNoteDetailTable " _
            '     & " INNER JOIN PurchaseOrderMasterTable PO on  PO.PurchaseOrderID = ReceivingNoteDetailTable.PO_ID  " _
            '     & " INNER JOIN tblStockDispatchStatus on tblStockDispatchStatus.StockDispatchStatusID = PO.POStockDispatchStatus " _
            '     & " WHERE IsNull(LCID,0) in(Select IsNull(Max(IsNull(LCID,0)),0) as LCID From PurchaseOrderMasterTable) " _
            '     & " AND IsNull(LCID,0)  <> 0 ) LC On LC.LCID = tblLetterofCredit.LcDoc_Id WHERE (Convert(varchar, LCDoc_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "

            str = "Select LcDoc_Id, lcDoc_Date as Date, lcDoc_no as [LC No], IsNull(LC.Insurrance,0) as Insurrance, ETD_Date, ETA_Date, NN_Date, BDR_Date, Clearing_Agent as [Clr Agent], DD_Date, DTB_Date, tblDefCostCenter.Name as [Project], TransporterName as Transporter, IsNull(Freight,0) as Freight, Case When IsNull(LC.Financial_Impact,0)=0 then 'Pending' else 'Received' end as   [Dispatch Status], tblLetterofCredit.Remarks, LCAmount as [Amount], isnull(PaidAmount,0) as [Opening LC Charges], (ISNULL(LCAmount,0)-isnull(PaidAmount,0)) as [Retire Charges] From tblLetterofCredit LEFT OUTER JOIN tblDefTransporter on tblDefTransporter.TransporterID = tblLetterofCredit.TransporterID LEFT OUTER JOIN tblDefCostCenter on tblDefCostCenter.CostCenterID = tblLetterofCredit.CostCenter  LEFT OUTER JOIN (" _
                 & " Select IsNull(LCDocID,0) as LCID, IsNull(Financial_Impact,0) as Financial_Impact, IsNull(Insurrance,0) as Insurrance,IsNull(ExpenseByLC,0) as LCExpense From LCMasterTable WHERE IsNull(LCDocID,0) In(Select Max(IsNull(LCDocID,0)) From LCMasterTable)) LC On LC.LCID = tblLetterofCredit.LcDoc_Id WHERE (Convert(varchar, LCDoc_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "

            Dim dt As New DataTable
            dt = GetDataTable(str)

            str = String.Empty
            str = "Select coa_detail_id, detail_title from vwCOADetail WHERE main_sub_sub_id in (select config_Value From configvaluesTable where config_type='InwardExpHeadAcId')"
            Dim dtCost As New DataTable
            dtCost = GetDataTable(str)

            For Each r As DataRow In dtCost.Rows
                If Not dtCost.Columns.Contains(r(1)) Then
                    dt.Columns.Add(r(0), GetType(System.Int32), r(0))
                    dt.Columns.Add(r(1), GetType(System.Double))
                End If
            Next


            For Each r As DataRow In dt.Rows
                For c As Int32 = enmLC.Count To dt.Columns.Count - 2 Step 2
                    r.BeginEdit()
                    r(c + 1) = 0
                    r.EndEdit()
                Next
            Next
            str = String.Empty
            str = "SELECT dbo.PurchaseOrderMasterTable.LCId, dbo.InwardExpenseDetailTable.AccountId, dbo.InwardExpenseDetailTable.Exp_Amount " _
                  & "   FROM   dbo.InwardExpenseDetailTable INNER JOIN " _
                  & "    dbo.ReceivingMasterTable ON dbo.InwardExpenseDetailTable.PurchaseId = dbo.ReceivingMasterTable.ReceivingId INNER JOIN " _
                  & "    dbo.PurchaseOrderMasterTable ON dbo.ReceivingMasterTable.PurchaseOrderID = dbo.PurchaseOrderMasterTable.PurchaseOrderId WHERE (Convert(Varchar, ReceivingDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "
            Dim dtData As New DataTable
            dtData = GetDataTable(str)
            Dim dr() As DataRow
            For Each r As DataRow In dt.Rows
                dr = dtData.Select("LCID=" & r(0) & "")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drfound As DataRow In dr
                            r.BeginEdit()
                            r(dt.Columns.IndexOf(drfound(1)) + 1) = drfound(2)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next


            Dim strTotal As String = String.Empty
            For c As Int32 = enmLC.Count To dt.Columns.Count - 2 Step 2
                If strTotal.Length > 0 Then
                    strTotal += "+" & "[" & dt.Columns(c + 1).ColumnName & "]"
                Else
                    strTotal = "[" & dt.Columns(c + 1).ColumnName & "]"
                End If
            Next

            dt.Columns.Add("Total Charges", GetType(System.Double))
            dt.Columns("Total Charges").Expression = strTotal & "+" & "[Opening LC Charges]+[Retire Charges]"
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns(0).Visible = False

            ApplyGridSettings()
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdSaved.AutoSizeColumns()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.ToolStripProgressBar1.Visible = False
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try
            Me.grdSaved.RootTable.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            For c As Integer = enmLC.Count To Me.grdSaved.RootTable.Columns.Count - 2 Step 2
                Me.grdSaved.RootTable.Columns(c).Visible = False
                Me.grdSaved.RootTable.Columns(c + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Next
            Me.grdSaved.RootTable.Columns("Amount").Visible = False
            Me.grdSaved.RootTable.Columns("Date").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("ETD_Date").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("ETA_Date").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("NN_Date").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("DD_Date").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("DTB_Date").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("BDR_Date").FormatString = "dd/MMM/yyyy"

            Me.grdSaved.RootTable.Columns("Freight").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Freight").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Freight").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Freight").FormatString = "N" & DecimalPointInValue

            Me.grdSaved.RootTable.Columns("Insurrance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Insurrance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Insurrance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Insurrance").FormatString = "N" & DecimalPointInValue

            Me.grdSaved.RootTable.Columns("Total Charges").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Total Charges").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Total Charges").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaved.RootTable.Columns("Opening LC Charges").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Opening LC Charges").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Opening LC Charges").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaved.RootTable.Columns("Retire Charges").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Retire Charges").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Retire Charges").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "LC Detail Ledger" & Chr(10) & "From Date: " & dtpFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpTo.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

    End Sub
End Class