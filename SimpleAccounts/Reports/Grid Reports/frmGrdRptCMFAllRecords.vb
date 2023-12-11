Public Class frmGrdRptCMFAllRecords
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            FillGrid()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillGrid(Optional ByVal Condition As String = "")
        Try

            Dim str As String = String.Empty
            str = "SELECT CMFA.DocId,CMFA.LocationId,CMFA.ProjectId,COA.coa_detail_id, Isnull(CMFA.POId,0) as POId, CMFA.DocNo, CMFA.DocDate, CMFA.UserName, Comp.CompanyName as Company, COA.detail_title as [Customer], Project.Name as Project, SO.SalesOrderNo as [SO No], CMFA.Remarks, CMFA.InvoiceNo, CMFA.ExptJobCompDate, " _
                  & " CMFA.ExptPaymentFromClient, CMFA.JobStartingTime, CMFA.TentiveInvoiceDate, CMFA.VerificationPeriodAfterCompletionJob, CMFA.Status, IsNull(CMFA.ApprovedBudget,0) as ApprovedBudget, IsNull(CMFA.TaxPercent,0) as TaxPercent, Isnull(CMFA.WHTaxPercent,0) as WHTaxPercent, IsNull(CMFA.ApprovedUserId,0) as ApprovedUserId, IsNull(CMFA.Approved,0) as Approved, ISNULL(CMFA.OPEX_Sale_Percent,0) as OPEX_Sale_Percent, Isnull(CMFA.EstimateExpense,0) as EstimateExpense, CMFA.ReturnComment, ISNULL(CMFA.ReturnStatus,0) as ReturnStatus, CMFA.CMFAType, IsNull(CMFA.UserId,0) as UserId,IsNull(CMFA.CheckedByUserId,0) as CheckedByUserId, IsNull(CMFA.CheckedStatus,0) as CheckedStatus  " _
                  & " FROM dbo.CMFAMasterTable AS CMFA LEFT OUTER JOIN " _
                  & " dbo.tblDefCostCenter AS Project ON CMFA.ProjectId = Project.CostCenterID LEFT OUTER JOIN " _
                  & " dbo.vwCOADetail AS COA ON CMFA.CustomerCode = COA.coa_detail_id LEFT OUTER JOIN " _
                  & " dbo.CompanyDefTable AS Comp ON CMFA.LocationId = Comp.CompanyId LEFT OUTER JOIN " _
                  & " dbo.SalesOrderMasterTable AS SO ON CMFA.POId = SO.SalesOrderId  WHERE  CMFA.DocId <> 0 "
            str += " AND (Convert(varchar, CMFA.DocDate, 102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102))"
            str += " ORDER BY CMFA.DocNo DESC "

            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.AcceptChanges()

            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("DocId").Visible = False
            Me.grdSaved.RootTable.Columns("locationId").Visible = False
            Me.grdSaved.RootTable.Columns("coa_detail_id").Visible = False
            Me.grdSaved.RootTable.Columns("ProjectId").Visible = False
            Me.grdSaved.RootTable.Columns("POId").Visible = False
            Me.grdSaved.RootTable.Columns("ApprovedUserId").Visible = False
            Me.grdSaved.RootTable.Columns("OPEX_Sale_Percent").Visible = False

            Me.grdSaved.RootTable.Columns("UserId").Visible = False
            Me.grdSaved.RootTable.Columns("CheckedByUserId").Visible = False

            Me.grdSaved.RootTable.Columns("DocDate").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("ExptJobCompDate").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("JobStartingTime").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("TentiveInvoiceDate").FormatString = "dd/MMM/yyyy"

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdSaved.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
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
    Private Sub frmGrdRptCMFAllRecords_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.cmbPeriod.Text = "Current Month"
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.btnPrint.Visible = True
            Else
                Me.btnPrint.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@DocId", Me.grdSaved.GetRow.Cells("DocId").Value)
            ShowReport("RptCmfaDocument")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "CMFA Detail History" & Chr(10) & "From Date: " & dtpFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpTo.Value.ToString("dd-MM-yyyy").ToString & ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class