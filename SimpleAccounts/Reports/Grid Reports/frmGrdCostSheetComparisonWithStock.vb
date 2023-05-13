'' 21-Jan-2014 TASK2387 IMRAN ALI Item List Not Show In Stock Estimation
'' 12-June-2014 TASK:2682 IMRAN ALI Multi Finish Goods Stock Estimation Report
Imports SBModel
Public Class frmGrdCostSheetComparisonWithStock
    Dim flgCompanyRights As Boolean = False
    Private Sub frmGrdCostSheetComparisonWithStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmGrdCostSheetComparisonWithStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If
            GetSecurityRights()

            Dim Str As String = "SELECT ArticleDefView.MasterID as Id, ArticleDefView.ArticleCode Code, ArticleDefView.ArticleDescription Item,  ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID] FROM  ArticleDefView "
            'str += " INNER JOIN vwStock ON vwStock.ArticleId = ArticleDefView.ArticleId"
            'Before against task:2387
            'Str += " where Active=1 And ArticleDefView.SalesItem=1 AND  ArticleDefView.MasterId  In (Select MasterArticleID From tblCostSheet)"
            'Task:2387 Remove Filter SalesItem
            Str += " where Active=1 And ArticleDefView.MasterId  In (Select DISTINCT MasterArticleID From tblCostSheet)"
            'End Task:2387
            If flgCompanyRights = True Then
                Str += " AND ArticleDefview.CompanyId=" & MyCompanyId
            End If
            Str += " ORDER BY ArticleDefView.SortOrder asc"

            'FillDropDown(cmbItem, str)
            FillUltraDropDown(Me.cmbItem, Str, True)
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                If Me.RadioButton2.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
            Me.cmbItem.Rows(0).Activate()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
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
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            Dim id As Integer = 0I

            id = Me.cmbItem.ActiveRow.Cells(0).Value
            Dim Str As String = "SELECT ArticleDefView.MasterID as Id, ArticleDefView.ArticleCode Code, ArticleDefView.ArticleDescription Item,  ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID] FROM  ArticleDefView "
            'str += " INNER JOIN vwStock ON vwStock.ArticleId = ArticleDefView.ArticleId"
            'Before against task:2387
            'Str += " where Active=1 And ArticleDefView.SalesItem=1 AND  ArticleDefView.MasterId  In (Select MasterArticleID From tblCostSheet) "
            'Task:2387 Remove Filter SalesItem
            Str += " where Active=1 AND  ArticleDefView.MasterId  In (Select DISTINCT MasterArticleID From tblCostSheet) "
            'End Task:2387
            If flgCompanyRights = True Then
                Str += " AND ArticleDefview.CompanyId=" & MyCompanyId
            End If
            Str += " ORDER BY ArticleDefView.SortOrder asc"

            'FillDropDown(cmbItem, str)
            FillUltraDropDown(Me.cmbItem, Str)
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            Me.cmbItem.Rows(0).Activate()
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                If Me.RadioButton2.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
            Me.cmbItem.ActiveRow.Cells(0).Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            ToolStripButton1_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Try
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                If Me.RadioButton2.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            FillGrid()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillGrid()
        Try
            Dim strIDs As String = String.Empty
            If Me.GridEX1.RowCount = 0 Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetRows
                If strIDs.Length > 0 Then
                    strIDs += "," & r.Cells(0).Value
                Else
                    strIDs = r.Cells(0).Value
                End If
            Next

            'Dim strSQL As String = "SELECT   dbo.ArticleDefView.MasterID, dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName, " _
            '        & " dbo.ArticleDefView.ArticleSizeName, Convert(float, 0) AS SuggestedQty, ISNULL(CostSheet.Qty, 0) AS CostSheetQty, Convert(float,0) as Qty, ISNULL(Stock.Qty, 0) AS StockQty " _
            '        & " FROM dbo.ArticleDefView LEFT OUTER JOIN " _
            '        & " (SELECT ArticleID, SUM(ISNULL(Qty, 0)) AS Qty " _
            '        & " FROM dbo.tblCostSheet   WHERE MasterArticleId=" & Me.cmbItem.Value & " " _
            '        & " GROUP BY ArticleID) AS CostSheet ON CostSheet.ArticleID = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
            '        & " (SELECT     ArticleDefId, SUM(ISNULL(InQty, 0) - ISNULL(OutQty, 0)) AS Qty " _
            '        & " FROM dbo.StockDetailTable " _
            '        & " GROUP BY ArticleDefId ) AS Stock ON Stock.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE dbo.ArticleDefView.ArticleId in (Select ArticleId From tblCostSheet WHERE MasterArticleId=" & Me.cmbItem.Value & ") "


            Dim strSQL As String = "SELECT   CostSheet.MasterArticleId, dbo.ArticleDefView.MasterID, dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode as [Item Code], dbo.ArticleDefView.ArticleDescription as Item, dbo.ArticleDefView.ArticleColorName as Color, " _
                & " dbo.ArticleDefView.ArticleSizeName as Size, Convert(float, 0) AS SuggestedQty, Sum(ISNULL(CostSheet.Qty, 0)) AS CostSheetQty, Convert(float,0) as Qty, Sum(ISNULL(Stock.Qty, 0)) AS StockQty " _
                & " FROM dbo.ArticleDefView LEFT OUTER JOIN " _
                & " (SELECT MasterArticleId, ArticleID, SUM(ISNULL(Qty, 0)) AS Qty " _
                & " FROM dbo.tblCostSheet   WHERE MasterArticleId IN(" & IIf(strIDs.Length > 0, strIDs, 0) & ") " _
                & " GROUP BY ArticleID,MasterArticleId) AS CostSheet ON CostSheet.ArticleID = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                & " (SELECT     ArticleDefId, SUM(ISNULL(InQty, 0) - ISNULL(OutQty, 0)) AS Qty " _
                & " FROM dbo.StockDetailTable INNER JOIN ArticleDefTable ON ArticleDefTable.ArticleId = ArticleDefId LEFT OUTER JOIN tblCostSheet On tblCostSheet.MasterArticleId = ArticleDefTable.MasterId " _
                & " GROUP BY ArticleDefId,tblCostSheet.MasterArticleId) AS Stock ON Stock.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE dbo.ArticleDefView.ArticleId in (Select ArticleId From tblCostSheet WHERE MasterArticleId IN(" & IIf(strIDs.Length > 0, strIDs, 0) & ")) " _
                & " GROUP BY CostSheet.MasterArticleId, dbo.ArticleDefView.MasterID, dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName, " _
                & " dbo.ArticleDefView.ArticleSizeName "


            Dim dt As New DataTable
            dt = GetDataTable(strSQL)

            If dt IsNot Nothing Then


                For Each r As DataRow In dt.Rows
                    For Each jr As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetRows
                        If Val(r(0).ToString) = Val(jr.Cells(0).Value.ToString) Then
                            r.BeginEdit()
                            r(7) = Val(jr.Cells("PlanQty").Value.ToString)
                            r.EndEdit()
                        End If
                    Next
                Next
                dt.AcceptChanges()

                'dt.Columns("SuggestedQty").Expression = Me.txtPlanQty.Text.ToString
                dt.Columns.Add("Difference", GetType(System.Double))
                dt.Columns("Qty").Expression = "(SuggestedQty*CostSheetQty)"
                dt.Columns("Difference").Expression = "(Qty-StockQty)"
            End If

            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()


            Me.CtrlGrdBar1.txtGridTitle.Text = "" & CompanyTitle & "" & Chr(10) & "" & "Stock esstimation" & "" & Chr(10) & Me.txtRemarks.Text.ToString

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetMasterId(ByVal ArticleId As Integer) As Integer
        Try

            Dim strsql As String = "Select ArticleId From ArticleDefTableMaster WHERE ArticleId=" & ArticleId & ""
            Dim dt As New DataTable
            dt = GetDataTable(strsql)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Sub ApplyGridSettings()
        Try

            If Me.grd.RootTable Is Nothing Then Exit Sub

            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

            Me.grd.RootTable.Columns("MasterID").Visible = False
            Me.grd.RootTable.Columns("ArticleId").Visible = False
            Me.grd.RootTable.Columns("SuggestedQty").Visible = False
            Me.grd.RootTable.Columns("CostSheetQty").Visible = False
            Me.grd.RootTable.Columns("MasterArticleId").Visible = False
            Me.grd.RootTable.Columns("Qty").Caption = "Suggested Qty"
            Me.grd.RootTable.Columns("SuggestedQty").Caption = "Plan Qty"
            Me.grd.RootTable.Columns("StockQty").Caption = "Current Stock"

            Me.grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("StockQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Difference").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("StockQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Difference").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far



            Me.grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("StockQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Difference").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far



            Me.grd.RootTable.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    'Task:2682 Added Event Multi Production Plan 
    Private Sub frmGrdCostSheetComparisonWithStock_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Dim dt As New DataTable
            dt.TableName = "Default"
            dt.Columns.Add("ArticleId", GetType(System.Int32))
            dt.Columns.Add("ArticleCode", GetType(System.String))
            dt.Columns.Add("ArticleDescription", GetType(System.String))
            dt.Columns.Add("PlanQty", GetType(System.Double))
            dt.Columns.Add("Remarks", GetType(System.String))
            dt.AcceptChanges()
            Me.GridEX1.DataSource = dt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Task:2682 Added Event Add Multi Production Plan
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim objDTPlan As DataTable = CType(Me.GridEX1.DataSource, DataTable)
            Dim dr As DataRow
            dr = objDTPlan.NewRow
            dr(0) = Me.cmbItem.Value
            dr(1) = Me.cmbItem.ActiveRow.Cells("Code").Value.ToString
            dr(2) = Me.cmbItem.ActiveRow.Cells("Item").Value.ToString
            dr(3) = Val(Me.txtPlanQty.Text)
            dr(4) = Me.txtRemarks.Text
            objDTPlan.Rows.Add(dr)
            objDTPlan.AcceptChanges()
            Me.txtPlanQty.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
            Me.cmbItem.Focus()
            Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Dim dt As DataTable = CType(Me.GridEX1.DataSource, DataTable)
            dt.Clear()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Enter
        Try
            Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GridEX1_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnButtonClick
        Try
            If e.Column.Key = "Column1" Then
                Me.GridEX1.GetRow.Delete()
                Me.GridEX1.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2682

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Try
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                If Me.RadioButton2.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Stock Estimation"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class