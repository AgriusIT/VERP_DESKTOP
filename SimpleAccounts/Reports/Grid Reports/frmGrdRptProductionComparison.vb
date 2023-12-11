Public Class frmGrdRptProductionComparison
    Public Sub FillData()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "  SELECT dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode as [Item Code], dbo.ArticleDefView.ArticleDescription as [Item], ISNULL(Issued.PlanQty, 0) AS IssuedQty, ISNULL(Prod.ProdQty, 0) " _
                        & "  AS ProdQty, IsNull(Cost.Rate,0) AS CostPrice, CONVERT(float, 0) AS CostAmount FROM         dbo.ArticleDefView LEFT OUTER JOIN " _
                        & "  (SELECT ArticleDefView_1.ArticleId, SUM(CASE WHEN PlanUnit = 'Loose' THEN 1 ELSE IsNull(ArticleDefView_1.PackQty, 0)  " _
                        & "   END * ISNULL(dbo.DispatchDetailTable.PlanQty, 0)) AS PlanQty " _
                        & "  FROM  dbo.DispatchMasterTable INNER JOIN " _
                        & "  dbo.DispatchDetailTable ON dbo.DispatchMasterTable.DispatchId = dbo.DispatchDetailTable.DispatchId INNER JOIN " _
                        & "  dbo.ArticleDefView AS ArticleDefView_1 ON dbo.DispatchDetailTable.ArticleDefMasterId = ArticleDefView_1.MasterID " _
                        & "  WHERE (dbo.DispatchDetailTable.PlanUnit <> '') AND (IsNull(DispatchMasterTable.EmpID,0)=" & Me.cmbEmployee.SelectedValue & ") " & IIf(Me.cmbPlan.SelectedIndex > 0, " AND (IsNull(DispatchMasterTable.PlanId,0)=" & Me.cmbPlan.SelectedValue & ")", "") & " " & IIf(Me.dtpDateFrom.Checked = True, "  AND (Convert(varchar,DispatchMasterTable.DispatchDate,102) >= Convert(DateTime,'" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102))", " ") & IIf(Me.dtpDateTo.Checked = True, " AND (Convert(varchar,DispatchMasterTable.DispatchDate,102) <= Convert(DateTime,'" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))", "") & " " _
                        & "  GROUP BY ArticleDefView_1.ArticleId) AS Issued ON Issued.ArticleId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                        & "  (SELECT dbo.ProductionDetailTable.ArticledefID, SUM(dbo.ProductionDetailTable.Qty) AS ProdQty " _
                        & "  FROM dbo.ProductionMasterTable INNER JOIN " _
                        & "  dbo.ProductionDetailTable ON dbo.ProductionMasterTable.Production_ID = dbo.ProductionDetailTable.Production_ID WHERE (IsNull(ProductionMasterTable.EmployeeID,0)=" & Me.cmbEmployee.SelectedValue & ") " & IIf(Me.cmbPlan.SelectedIndex > 0, " AND (IsNull(ProductionMasterTable.PlanId,0)=" & Me.cmbPlan.SelectedValue & ")", "") & IIf(Me.cmbPlan.SelectedIndex > 0, " AND (IsNull(DispatchMasterTable.PlanId,0)=" & Me.cmbPlan.SelectedValue & ")", "") & IIf(Me.dtpDateFrom.Checked = True, "  AND (Convert(varchar,ProductionMasterTable.Production_date,102) >= Convert(DateTime,'" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102))", " ") & IIf(Me.dtpDateTo.Checked = True, " AND (Convert(varchar,ProductionMasterTable.Production_date,102) <= Convert(DateTime,'" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))", "") & "  " _
                        & "  GROUP BY dbo.ProductionDetailTable.ArticledefID) AS Prod ON Prod.ArticledefID = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                        & "  (SELECT dbo.tblEmployeeArticleCostRate.ArticleDefId, Rate " _
                        & "  FROM dbo.tblEmployeeArticleCostRate WHERE tblEmployeeArticleCostRate.Employee_ID=" & Me.cmbEmployee.SelectedValue & " " _
                        & "  GROUP BY dbo.tblEmployeeArticleCostRate.ArticleDefId, Rate) AS Cost ON Cost.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE MasterID In (Select DISTINCT MasterArticleId From tblCostSheet) "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.Columns("CostAmount").Expression = "CostPrice*ProdQty"
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()

            Me.grd.RootTable.Columns("IssuedQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("ProdQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("CostPrice").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("CostAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("IssuedQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("ProdQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("CostPrice").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("CostAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("IssuedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("ProdQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("CostAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("IssuedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ProdQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("CostPrice").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("CostAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("IssuedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ProdQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("CostPrice").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("CostAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(0).Visible = False
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try

            Dim strSQL As String = String.Empty
            If Condition = "Employee" Then
                FillDropDown(Me.cmbEmployee, "Select Employee_ID, Employee_Name, Employee_Code From tblDefEmployee Where Active=1 ORDER BY 2 ASC") ''TASKTFS75 added and set active = 1
            ElseIf Condition = "Plan" Then
                FillDropDown(Me.cmbPlan, "Select PlanId, PlanNo From PlanMasterTable ORDER BY PlanNo DESC")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmGrdRptProductionComparison_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.dtpDateFrom.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpDateFrom.Checked = False
            Me.dtpDateTo.Value = Date.Now
            Me.dtpDateTo.Checked = False
            FillCombos("Employee")
            FillCombos("Plan")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0
            id = Me.cmbEmployee.SelectedIndex
            FillCombos("Employee")
            Me.cmbEmployee.SelectedIndex = id

            id = Me.cmbPlan.SelectedIndex
            FillCombos("Plan")
            Me.cmbPlan.SelectedIndex = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try

            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Employee Wise Article Cost" & vbCrLf & " Employee: " & Me.cmbEmployee.Text & "  Plan No: " & Me.cmbPlan.Text & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            If Me.cmbEmployee.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select Employee.")
                Me.cmbEmployee.Focus()
                Exit Sub
            End If
            FillData()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabPageControl1.Tab.Selected = True Then
                Me.CtrlGrdBar1.Visible = False
            Else
                Me.CtrlGrdBar1.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class