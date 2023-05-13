''18-Mar-2014 IMRAN ALI TASK2502 Problem in Production Detail Report 
Imports SBModel
Public Class frmrptGrdProducedItems
    Public fromDate As Date
    Public toDate As Date
    Public ProjectId As Integer

    Private Sub frmrptGrdProducedItems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmrptGrdProducedItems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            'Me.dtpFromDate.Value = Me.dtpFromDate.Value.Date.AddMonths(-1)
            Me.cmbPeriod.Text = "Current Month"
            FillDropDown(Me.cmbCostCenter, "Select CostCenterId, Name as CostCenter From tblDefCostCenter")
            FillDropDown(Me.cmbPlanNo, "Select PlanId, PlanNo From PlanMasterTable Order By PlanId DESC")
            GetAllRecords()
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
                ElseIf RightsDt.FormControlName = "Grid Print" Then
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
    Private Sub GetAllRecords(Optional ByVal Condition As String = "")
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim str As String = String.Empty
            str = "Sp_ProduceItems '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', " & Me.cmbCostCenter.SelectedValue & ""
            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.TableName = "Default"
            Dim dv As New DataView
            dv.Table = dt

            If Me.cmbPlanNo.SelectedIndex > 0 Then
                dv.RowFilter = " PlanId=" & Me.cmbPlanNo.SelectedValue & ""
            End If
            Me.GridEX1.DataSource = Nothing
            Me.GridEX1.DataSource = dv.ToTable
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns(0).Visible = False
            Me.GridEX1.RootTable.Columns(1).Visible = False
            Me.GridEX1.RootTable.Columns("PlanId").Visible = False
            Me.GridEX1.RootTable.Columns("Location_Name").Caption = "Location"
            'Me.GridEX1.RootTable.Columns("Production_Date").Caption = "Document Date"
            'Me.GridEX1.RootTable.Columns("Production_No").Caption = "Document No"
            Me.GridEX1.RootTable.Columns("ArticleCode").Caption = "Code"
            Me.GridEX1.RootTable.Columns("ArticleDescription").Caption = "Item"
            Me.GridEX1.RootTable.Columns("ArticleColorName").Caption = "Color"
            Me.GridEX1.RootTable.Columns("ArticleSizeName").Caption = "Size"
            Me.GridEX1.RootTable.Columns("ArticleUnitName").Caption = "Unit"

            ''18-Mar-2014 IMRAN ALI TASK2502 Problem in Production Detail Report 
            GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.GridEX1.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("PlanQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Diff Plan").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("PlanQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Diff Plan").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("PlanQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("Diff Plan").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            'End Task:2502
            Me.GridEX1.AutoSizeColumns()

            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try
            'If Me.cmbCostCenter.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select project")
            '    Me.cmbCostCenter.Focus()
            '    Exit Sub
            'End If
            GetAllRecords()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GridEX1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.SelectionChanged
        Try
            If Me.SplitContainer1.Panel2Collapsed = True Then Exit Sub
            If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                frmRptGrdProductionDetail.Close()
            End If
            If Me.GridEX1.RowCount = 0 Then Exit Sub
            frmRptGrdProductionDetail.FromDate = Me.dtpFromDate.Value.Date
            frmRptGrdProductionDetail.ToDate = Me.dtpToDate.Value.Date
            If IsDBNull(Me.GridEX1.GetRow.Cells("CostCenterId")) Then
                frmRptGrdProductionDetail.ProjectId = 0  'Me.cmbCostCenter.SelectedValue
            Else
                frmRptGrdProductionDetail.ProjectId = Me.GridEX1.GetRow.Cells("CostCenterId").Value
            End If
            frmRptGrdProductionDetail.ProduceItem = Me.GridEX1.GetRow.Cells("ArticleDefId").Value
            frmRptGrdProductionDetail.TopLevel = False
            frmRptGrdProductionDetail.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            frmRptGrdProductionDetail.Dock = DockStyle.Fill
            Me.SplitContainer1.Panel2.Controls.Add(frmRptGrdProductionDetail)
            frmRptGrdProductionDetail.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try
            If Me.SplitContainer1.Panel2Collapsed = True Then
                Me.SplitContainer1.Panel2Collapsed = False
                GridEX1_SelectionChanged(Me.GridEX1, Nothing)
            Else
                Me.SplitContainer1.Panel2Collapsed = True
                If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                    frmRptGrdProductionDetail.Close()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        Try
            If Me.GridEX1.RowCount = 0 Then Exit Sub
            frmGrdProductionReceived.ArticleId = Me.GridEX1.GetRow.Cells(0).Value
            frmGrdProductionReceived.startDate = Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00")
            frmGrdProductionReceived.endDate = Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59")
            If Not IsDBNull(Me.GridEX1.GetRow.Cells("CostCenterId")) Then
                frmGrdProductionReceived.ProjectId = Me.GridEX1.GetRow.Cells("CostCenterId").Value 'Me.cmbCostCenter.SelectedValue
            Else
                frmGrdProductionReceived.ProjectId = 0
            End If
            ApplyStyleSheet(frmGrdProductionReceived)
            frmGrdProductionReceived.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Production Detail" & Chr(10) & "From Date: " & dtpFromDate.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpToDate.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0
            id = Me.cmbCostCenter.SelectedValue
            Me.dtpFromDate.Value = Me.dtpFromDate.Value.Date.AddMonths(-1)
            FillDropDown(Me.cmbCostCenter, "Select CostCenterId, Name as CostCenter From tblDefCostCenter")
            Me.cmbCostCenter.SelectedValue = id
            id = Me.cmbPlanNo.SelectedIndex
            FillDropDown(Me.cmbPlanNo, "Select PlanId, PlanNo From PlanMasterTable Order By PlanId DESC")
            Me.cmbPlanNo.SelectedIndex = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
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
    End Sub
End Class