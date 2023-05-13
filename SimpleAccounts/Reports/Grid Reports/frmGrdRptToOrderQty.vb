'Ali Faisal : TFS1600 : Add new report to get Estimated order Qty for Ramzan Machinery
Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient
Public Class frmGrdRptToOrderQty
    Implements IGeneral
    Dim _SearchDt As New DataTable
    ''' <summary>
    ''' Ali Faisal : TFS1600 : Set indexing for history grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        ArticleGroupName
        ArticleTypeName
        ArticleCompanyName
        ArticleGenderName
        ArticleLpoName
        ArticleId
        ArticleCode
        ArticleDescription
        ArticleSizeName
        ArticleColorName
        ArticleUnitName
        HS_Code
        ItemWeight
        PurchasePrice
        SalePrice
        StockLevel
        StockLevelOpt
        StockLevelMax
        Cost_Price
        OutStock
        CurrentStock
        OrderQty
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS1600 :Apply grid settings to hide some columns and apply formating too on some of columns
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.ArticleCompanyName).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ArticleGenderName).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ArticleLpoName).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ArticleId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.HS_Code).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ItemWeight).Visible = False
            Me.grdSaved.RootTable.Columns(grd.PurchasePrice).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SalePrice).Visible = False
            Me.grdSaved.RootTable.Columns(grd.StockLevel).Visible = False
            Me.grdSaved.RootTable.Columns(grd.StockLevelOpt).Visible = False
            Me.grdSaved.RootTable.Columns(grd.StockLevelMax).Visible = False
            Me.grdSaved.RootTable.Columns(grd.Cost_Price).Visible = False
            Me.grdSaved.RootTable.Columns(grd.OutStock).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.OutStock).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.OutStock).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.OutStock).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.OutStock).TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.CurrentStock).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.CurrentStock).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.CurrentStock).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.CurrentStock).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.CurrentStock).TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.OrderQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.OrderQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.OrderQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.OrderQty).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.OrderQty).TotalFormatString = "N" & DecimalPointInQty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1600 : Apply security rights for the standard user to show specific controls to that
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnShow.Enabled = True
                Me.btnNew.Enabled = True
                Me.btnPrint.Enabled = True
                Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                Me.CtrlGrdBar2.mGridPrint.Enabled = True
                Me.CtrlGrdBar2.mGridExport.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnShow.Enabled = False
            Me.btnPrint.Enabled = False
            Me.CtrlGrdBar2.mGridChooseFielder.Enabled = False
            Me.CtrlGrdBar2.mGridPrint.Enabled = False
            Me.CtrlGrdBar2.mGridExport.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar2.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar2.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Report Print" Then
                    IsCrystalReportPrint = True
                ElseIf Rights.Item(i).FormControlName = "Report Export" Then
                    IsCrystalReportExport = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1600 : Fill data in all dropdowns
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "Department" Then
                FillListBox(Me.lstDepartment.ListItem, "SELECT ArticleGroupId, ArticleGroupName FROM ArticleGroupDefTable WHERE Active = 1 ORDER BY SortOrder ASC")
            ElseIf Condition = "Type" Then
                FillListBox(Me.lstType.ListItem, "SELECT ArticleTypeId, ArticleTypeName FROM ArticleTypeDefTable WHERE Active = 1 ORDER BY SortOrder ASC")
            ElseIf Condition = "Item" Then
                FillListBox(Me.lstItem.ListItem, "SELECT ArticleId, ArticleCode + ' ~ ' + ArticleDescription ArticleDescription FROM ArticleDefView WHERE Active = 1 ORDER BY SortOrder ASC")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1600 : Get all records according to given criteria
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT ArticleDefView.ArticleGroupName Department, ArticleDefView.ArticleTypeName Type, ArticleDefView.ArticleCompanyName Catagory, ArticleDefView.ArticleLpoName SubCatagory, ArticleDefView.ArticleGenderName Origion, StockDetailTable.ArticleDefId AS ArticleId, ArticleDefView.ArticleCode Code, ArticleDefView.ArticleDescription Item, ArticleDefView.ArticleSizeName Size, ArticleDefView.ArticleColorName Color, ArticleDefView.ArticleUnitName UOM, ArticleDefView.HS_Code [HS Code], ArticleDefView.ItemWeight Weight, ArticleDefView.PurchasePrice, ArticleDefView.SalePrice, ArticleDefView.StockLevel, ArticleDefView.StockLevelOpt, ArticleDefView.StockLevelMax, ArticleDefView.Cost_Price CostPrice, "
            If Me.rbtnLoose.Checked = True Then
                str += " SUM(ISNULL(StockDetailTable.OutQty, 0)) AS OutStock, CurrentStock.CurrentStock, CASE WHEN SUM(ISNULL(StockDetailTable.OutQty, 0)) > CurrentStock.CurrentStock THEN SUM(ISNULL(StockDetailTable.OutQty, 0)) - CurrentStock.CurrentStock ELSE 0 END AS OrderQty "
            Else
                str += " (SUM(ISNULL(StockDetailTable.OutQty, 0)) / ArticleDefView.PackQty) AS OutStock, (CurrentStock.CurrentStock / ArticleDefView.PackQty) AS CurrentStock, CASE WHEN SUM(ISNULL(StockDetailTable.OutQty, 0)) > CurrentStock.CurrentStock THEN ((SUM(ISNULL(StockDetailTable.OutQty, 0)) - CurrentStock.CurrentStock )/ ArticleDefView.PackQty)  ELSE 0 END AS OrderQty "
            End If
            str += " FROM StockDetailTable INNER JOIN StockMasterTable ON StockDetailTable.StockTransId = StockMasterTable.StockTransId INNER JOIN  (SELECT StockDetailTable.ArticleDefId, SUM(ISNULL(StockDetailTable.InQty, 0) - ISNULL(StockDetailTable.OutQty, 0)) AS CurrentStock FROM StockDetailTable INNER JOIN StockMasterTable ON StockDetailTable.StockTransId = StockMasterTable.StockTransId WHERE (StockMasterTable.DocDate <= '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "') GROUP BY StockDetailTable.ArticleDefId) CurrentStock ON StockDetailTable.ArticleDefId = CurrentStock.ArticleDefId LEFT OUTER JOIN ArticleDefView ON StockDetailTable.ArticleDefId = ArticleDefView.ArticleId "
            str += " WHERE StockMasterTable.DocDate BETWEEN '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "' AND '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "' "
            If Me.lstDepartment.SelectedIDs.Length > 0 Then
                str += " AND ArticleDefView.ArticleGroupId IN (" & Me.lstDepartment.SelectedIDs & ")"
            End If
            If Me.lstType.SelectedIDs.Length > 0 Then
                str += " AND ArticleDefView.ArticleTypeId IN (" & Me.lstType.SelectedIDs & ")"
            End If
            If Me.lstItem.SelectedIDs.Length > 0 Then
                str += " AND ArticleDefView.ArticleId IN (" & Me.lstItem.SelectedIDs & ")"
            End If
            str += " GROUP BY StockDetailTable.ArticleDefId, ArticleDefView.PackQty, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleGenderName, ArticleDefView.ArticleTypeName, ArticleDefView.ArticleCompanyName, ArticleDefView.ArticleLpoName, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, ArticleDefView.ArticleGroupName, ArticleDefView.StockLevel, ArticleDefView.ItemWeight, ArticleDefView.HS_Code, ArticleDefView.SalePrice, ArticleDefView.PurchasePrice, ArticleDefView.StockLevelOpt, ArticleDefView.StockLevelMax, ArticleDefView.Cost_Price , CurrentStock.CurrentStock"
            dt = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1600 : Reset all controls to their default state
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.cmbPeriod.Text = "Current Month"
            Me.txtSearch.Text = ""
            Me.rbtnLoose.Checked = True
            FillCombos("Department")
            FillCombos("Type")
            FillCombos("Item")
            _SearchDt = CType(Me.lstItem.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.UltraTabControl2.Tabs(0).Selected = True
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            CtrlGrdBar2_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmItemTaskProgress_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.btnNew_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmItemTaskProgress_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            btnNew_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1600 : New button handling
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1600 : Show records
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            GetAllRecords()
            Me.UltraTabControl2.Tabs(1).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "To Order Qty Report"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1600 : Date from and to value changes on the basis of dropdown text
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
    ''' <summary>
    ''' Ali Faisal : TFS1600 : Search Item by name or code in list box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "ArticleDescription Like '%" & Me.txtSearch.Text & "%'"
            Me.lstItem.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1600 : Visibility of buttons on the basis of tab change
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UltraTabControl2_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl2.SelectedTabChanged
        Try
            If Me.UltraTabControl2.Tabs(0).Selected = True Then
                Me.btnPrint.Visible = False
            End If
            If Me.UltraTabControl2.Tabs(1).Selected = True Then
                Me.btnPrint.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1600 : Print Report and apply report rights too
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@Unit", IIf(Me.rbtnLoose.Checked = True, "Loose", "Pack"))
            AddRptParam("@DepartmentIds", Me.lstDepartment.SelectedIDs)
            AddRptParam("@TypeIds", Me.lstType.SelectedIDs)
            AddRptParam("@ItemIds", Me.lstItem.SelectedIDs)
            ShowReport("rptEstimatedToOrderQty")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
End Class