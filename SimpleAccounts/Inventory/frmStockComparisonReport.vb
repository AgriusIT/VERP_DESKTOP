Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Imports SBDal
Imports SBUtility
Public Class frmStockComparisonReport
    Implements IGeneral

    Dim _SearchDt As New DataTable
    Private Sub UltraTabPageControl2_Paint(sender As Object, e As PaintEventArgs) Handles UltraTabPageControl2.Paint

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

        Me.grdCostComparison.RootTable.Columns("Quantity").FormatString = "N" & DecimalPointInQty
        Me.grdCostComparison.RootTable.Columns("MeterialCost").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("ByProductionCost").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("ActualByProductionCost").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("NetExpense").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("ActualNetExpense").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("LabourCost").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("ActualLabourCost").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("OHExpense").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("ActualOHExpense").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("ActualTotal").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("FixedTotal").FormatString = "N" & DecimalPointInValue

        Me.grdCostComparison.RootTable.Columns("TotalQuantity").FormatString = "N" & DecimalPointInQty
        Me.grdCostComparison.RootTable.Columns("SumActualTotal").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("SumFixedTotal").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("ActualPerunitCost").FormatString = "N" & DecimalPointInValue
        Me.grdCostComparison.RootTable.Columns("FixedPerUnitCost").FormatString = "N" & DecimalPointInValue

        Me.grdCostComparison.RootTable.Columns("ProductionOrderDate").FormatString = str_DisplayDateFormat

        Dim TotalQuatity As Double
        Dim SumActualTotal As Double
        Dim SumFixedTotal As Double
        Dim FixedPerUnitCost As Double
        Dim ActualPerunitCost As Double

        For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdCostComparison.GetRows
            TotalQuatity = Row.GetSubTotal(Me.grdCostComparison.RootTable.Columns("Quantity"), Janus.Windows.GridEX.AggregateFunction.Sum)
            SumActualTotal = Row.GetSubTotal(Me.grdCostComparison.RootTable.Columns("ActualTotal"), Janus.Windows.GridEX.AggregateFunction.Sum)
            SumFixedTotal = Row.GetSubTotal(Me.grdCostComparison.RootTable.Columns("FixedTotal"), Janus.Windows.GridEX.AggregateFunction.Sum)

            FixedPerUnitCost = SumFixedTotal / TotalQuatity
            ActualPerunitCost = SumActualTotal / TotalQuatity

            FixedPerUnitCost = Math.Round(FixedPerUnitCost, 2)
            ActualPerunitCost = Math.Round(ActualPerunitCost, 2)

            For Each subRow As Janus.Windows.GridEX.GridEXRow In Row.GetChildRecords
                subRow.BeginEdit()
                subRow.Cells("TotalQuantity").Value = TotalQuatity
                subRow.Cells("SumActualTotal").Value = SumActualTotal
                subRow.Cells("SumFixedTotal").Value = SumFixedTotal
                subRow.Cells("ActualPerunitCost").Value = FixedPerUnitCost
                subRow.Cells("FixedPerUnitCost").Value = ActualPerunitCost
                subRow.EndEdit()
            Next
            TotalQuatity = 0.0
            SumActualTotal = 0.0
            SumFixedTotal = 0.0
            FixedPerUnitCost = 0.0
            ActualPerunitCost = 0.0
        Next

        Dim dtCostComparison As DataTable = Me.grdCostComparison.DataSource
        ''TASK TFS4230
        'select ProductionOrder.ProductionOrderNo , ProductionOrder.ProductionOrderDate , ArticleDefView.ArticleDescription As Item , ProductionOrderOutputMaterial.Qty As Quantity , 
        'isNull(FinishGood.MaterialCost,0) AS MeterialCost , isNull(ProductionInput.ActualMaterialCost,0) As ActualMaterialCost , isNull(FinishGood.FixedByProductionCost,0) As ByProductionCost , isNull(ActualByProduct.ActualByProductionCost,0) As ActualByProductionCost, isNull(isNull(FinishGood.MaterialCost,0) - isNull(FinishGood.FixedByProductionCost,0),0) As NetExpense , isNull(isNull(ProductionInput.ActualMaterialCost,0) - isNull(ActualByProduct.ActualByProductionCost,0),0) As ActualNetExpense , isNull(FinishGood.LabourCost,0) As LabourCost , isNull(ActualLabour.ActualLabourCost,0) As ActualLabourCost , isNull(FinishGood.OHExpense , 0) As OHExpense , isNull(ActualOverHead.ActualOHExpense,0) As ActualOHExpense , isNull(isNull(isNull(ProductionInput.ActualMaterialCost,0) - isNull(ActualByProduct.ActualByProductionCost,0),0) + isNull(ActualLabour.ActualLabourCost,0) +  IsNull(ActualOverHead.ActualOHExpense,0),0) As ActualTotal , isNull(FinishGood.FixedTotal,0) As FixedTotal , Convert(float,0) AS TotalQuantity , Convert(float,0) As SumActualTotal , Convert(float,0) As SumFixedTotal , Convert(float,0) As ActualPerunitCost , Convert(float,0) As FixedPerUnitCost
        Me.grdCostComparison.RootTable.ColumnSets.Clear()
        Me.grdCostComparison.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
        Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
        Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
        Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet
        'Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet
        Me.grdCostComparison.RootTable.ColumnSetRowCount = 1
        ''Column Set Detail
        ColumnSet = Me.grdCostComparison.RootTable.ColumnSets.Add
        ColumnSet.ColumnCount = 4
        ColumnSet.Caption = "Detail"
        ColumnSet.Add(Me.grdCostComparison.RootTable.Columns("ProductionOrderNo"), 0, 0)
        ColumnSet.Add(Me.grdCostComparison.RootTable.Columns("ProductionOrderDate"), 0, 1)
        ColumnSet.Add(Me.grdCostComparison.RootTable.Columns("Item"), 0, 2)
        ColumnSet.Add(Me.grdCostComparison.RootTable.Columns("Quantity"), 0, 3)
        ''Column Set Standard
        ColumnSet1 = Me.grdCostComparison.RootTable.ColumnSets.Add
        ColumnSet1.ColumnCount = 9
        ColumnSet1.Caption = "Standard"
        ColumnSet1.Add(Me.grdCostComparison.RootTable.Columns("MeterialCost"), 0, 0)
        ColumnSet1.Add(Me.grdCostComparison.RootTable.Columns("ByProductionCost"), 0, 1)
        ColumnSet1.Add(Me.grdCostComparison.RootTable.Columns("NetExpense"), 0, 2)
        ColumnSet1.Add(Me.grdCostComparison.RootTable.Columns("LabourCost"), 0, 3)
        ColumnSet1.Add(Me.grdCostComparison.RootTable.Columns("OHExpense"), 0, 4)
        ColumnSet1.Add(Me.grdCostComparison.RootTable.Columns("FixedTotal"), 0, 5)
        ColumnSet1.Add(Me.grdCostComparison.RootTable.Columns("TotalQuantity"), 0, 6)
        ColumnSet1.Add(Me.grdCostComparison.RootTable.Columns("SumFixedTotal"), 0, 7)
        ColumnSet1.Add(Me.grdCostComparison.RootTable.Columns("FixedPerUnitCost"), 0, 8)
        ''Column Set Actual
        ColumnSet2 = Me.grdCostComparison.RootTable.ColumnSets.Add
        ColumnSet2.ColumnCount = 8
        ColumnSet2.Caption = "Actual"
        ColumnSet2.Add(Me.grdCostComparison.RootTable.Columns("ActualMaterialCost"), 0, 0)
        ColumnSet2.Add(Me.grdCostComparison.RootTable.Columns("ActualByProductionCost"), 0, 1)
        ColumnSet2.Add(Me.grdCostComparison.RootTable.Columns("ActualNetExpense"), 0, 2)
        ColumnSet2.Add(Me.grdCostComparison.RootTable.Columns("ActualLabourCost"), 0, 3)
        ColumnSet2.Add(Me.grdCostComparison.RootTable.Columns("ActualOHExpense"), 0, 4)
        ColumnSet2.Add(Me.grdCostComparison.RootTable.Columns("ActualTotal"), 0, 5)
        ColumnSet2.Add(Me.grdCostComparison.RootTable.Columns("SumActualTotal"), 0, 6)
        ColumnSet2.Add(Me.grdCostComparison.RootTable.Columns("ActualPerunitCost"), 0, 7)
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

        Try
            If Condition = "Location" Then
                FillListBox(Me.lstLocation.ListItem, "select location_id , location_name from tbldeflocation WHERE Active = 1")
            ElseIf Condition = "Company" Then
                FillListBox(Me.lstCompany.ListItem, "select CompanyId , CompanyName from companyDefTable")
            ElseIf Condition = "InventoryDepartment" Then
                FillListBox(Me.lstInventoryDepartment.ListItem, "select ArticleGroupId , ArticleGroupName from ArticleGroupDefTable")
            ElseIf Condition = "InventoryType" Then
                FillListBox(Me.lstInventoryType.ListItem, "select ArticleTypeId , ArticleTypeName from ArticleTypeDefTable WHERE Active=1")
            ElseIf Condition = "InventoryCategory" Then
                FillListBox(Me.lstInventoryCategory.ListItem, "select ArticleCompanyId , ArticleCompanyName from ArticleCompanyDefTable where Active = 1")
            ElseIf Condition = "Item" Then
                FillListBox(Me.lstItem.ListItem, "select ArticleId , ArticleCode + ' ~ ' + ArticleDescription + ' ~ ' + ArticleColorName + ' ~ ' + ArticleSizeName Item from ArticleDefView WHERE Active=1 ")
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            ''TFS3844 : Saad Afzaal :  DeSelect all the lists at Load Time
            Me.cmbPeriod.Text = "Current Month"
            Me.txtItemList.Text = String.Empty
            FillCombos("Location")
            Me.lstLocation.DeSelect()
            FillCombos("Company")
            Me.lstCompany.DeSelect()
            FillCombos("InventoryDepartment")
            Me.lstInventoryDepartment.DeSelect()
            FillCombos("InventoryType")
            Me.lstInventoryType.DeSelect()
            FillCombos("InventoryCategory")
            Me.lstInventoryCategory.DeSelect()
            FillCombos("Item")
            Me.lstItem.DeSelect()
            _SearchDt = CType(Me.lstItem.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.lstItem.DeSelect()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmStockComparisonReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReSetControls()
    End Sub

    Private Sub txtItemList_KeyUp(sender As Object, e As KeyEventArgs) Handles txtItemList.KeyUp
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "Item Like '%" & Me.txtItemList.Text & "%'"
            Me.lstItem.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmStockComparisonReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F5 Then
                ReSetControls()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                Me.btnShow.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.btnPrint.Enabled = False
                    Me.btnShow.Enabled = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                Me.btnPrint.Enabled = False
                Me.btnShow.Enabled = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Show" Then
                        Me.btnShow.Enabled = True
                    ElseIf RightsDt.FormControlName = "GridPrint" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            FillGrid()
            'GetCrystalReportRights()
            'Me.CallShowReport()
        Catch Ex As Exception
            ShowErrorMessage(Ex.Message)
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.fromDateTimePicker.Value = Date.Today
            Me.toDateTimePicker.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.fromDateTimePicker.Value = Date.Today.AddDays(-1)
            Me.toDateTimePicker.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.fromDateTimePicker.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.toDateTimePicker.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.fromDateTimePicker.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.toDateTimePicker.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.fromDateTimePicker.Value = New Date(Date.Now.Year, 1, 1)
            Me.toDateTimePicker.Value = Date.Today
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillGrid()

        Dim strSQL As String = ""
        Dim dt As New DataTable

        'strSQL = "select ProductionOrderData.ProductionOrderNo , ProductionOrderData.ProductionOrderDate , ArticleDefView.ArticleDescription As Item " _
        '         & ", isNull(ProductionOrderData.Qunatity,0) As Quatity , isNull(FinishGoodDetail.CostPrice,0) AS MeterialCost , " _
        '         & "isNull(ProductionOrderData.ActualMaterialCost , 0) As ActualMaterialCost , " _
        '         & "isNull(FixedByProduct.FixedByProductionCost,0) As ByProductionCost , " _
        '         & "ProductionOrderData.ActualByProductionCost ,  " _
        '         & "isNull(isNull(FinishGoodDetail.CostPrice,0) - isNull(FixedByProduct.FixedByProductionCost,0),0) As NetExpense , " _
        '         & "ProductionOrderData.ActualNetExpense , " _
        '         & "isNull(FixedLabour.FixedLabourCost , 0) As LabourCost , ProductionOrderData.ActualByProductionCost , isNull(FixedOverHead.FixedOHExpense , 0) As OHExpense , " _
        '         & "ProductionOrderData.ActualOHExpense , ProductionOrderData.ActualTotal , " _
        '         & "isNull(isNull(isNull(FinishGoodDetail.CostPrice,0) - isNull(FixedByProduct.FixedByProductionCost,0),0) + isNull(FixedLabour.FixedLabourCost , 0) " _
        '         & "+ isNull(FixedOverHead.FixedOHExpense , 0) , 0) As FixedTotal " _
        '         & "from FinishGoodDetail " _
        '         & "Left Outer Join ( select sum(isNull(FinishGoodByProducts.Rate,0)) As FixedByProductionCost , FinishGoodByProducts.FinishGoodId " _
        '         & "from FinishGoodByProducts Group By FinishGoodByProducts.FinishGoodId ) " _
        '         & "As FixedByProduct On FinishGoodDetail.FinishGoodId = FixedByProduct.FinishGoodId " _
        '         & "Left Outer Join ( select sum(isNull(FinishGoodLabourAllocation.RatePerUnit,0)) As FixedLabourCost , FinishGoodLabourAllocation.FinishGoodId " _
        '         & "from FinishGoodLabourAllocation Group By FinishGoodLabourAllocation.FinishGoodId ) " _
        '         & "As FixedLabour On FinishGoodDetail.FinishGoodId = FixedLabour.FinishGoodId " _
        '         & "Left Outer Join ( select sum(isNull(FinishGoodOverHeads.Amount,0)) As FixedOHExpense , FinishGoodOverHeads.FinishGoodId " _
        '         & "from FinishGoodOverHeads Group By FinishGoodOverHeads.FinishGoodId ) " _
        '         & "As FixedOverHead On FinishGoodDetail.FinishGoodId = FixedOverHead.FinishGoodId " _
        '         & "Left Outer Join ArticleDefView On FinishGoodDetail.MaterialArticleId = ArticleDefView.ArticleId " _
        '         & "Inner Join (select ProductionOrder.ProductionOrderNo , ProductionOrder.ProductionOrderDate , isNull(ProductionOrderOutputMaterial.Qty,0) As Qunatity , " _
        '         & "isNull(ProductionOrderInputMaterial.Rate,0) As ActualMaterialCost , ProductionOrderInputMaterial.ItemId , " _
        '         & "isNull(ActualByProduct.ActualByProductionCost,0) As ActualByProductionCost , " _
        '         & "isNull(isNull(ProductionOrderInputMaterial.Rate,0) - isNull(ActualByProduct.ActualByProductionCost,0),0) As ActualNetExpense , " _
        '         & "isNull(ActualLabour.ActualLabourCost,0) As ActualLabourCost , IsNull(ActualOverHead.ActualOHExpense,0) As ActualOHExpense , " _
        '         & "isNull(isNull(isNull(ProductionOrderInputMaterial.Rate,0) - isNull(ActualByProduct.ActualByProductionCost,0),0) + isNull(ActualLabour.ActualLabourCost,0) +  IsNull(ActualOverHead.ActualOHExpense,0),0) As ActualTotal , " _
        '         & "ProductionOrderInputMaterial.LocationId As InputLocationId  , " _
        '         & "ProductionOrderOutputMaterial.LocationId As OutputLocationId " _
        '         & "from ProductionOrderInputMaterial " _
        '         & "Left Outer Join ProductionOrder On ProductionOrderInputMaterial.ProductionOrderId = ProductionOrder.ProductionOrderId " _
        '         & "Left Outer Join ProductionOrderOutputMaterial On ProductionOrder.ProductionOrderId = ProductionOrderOutputMaterial.ProductionOrderId " _
        '         & "Left Outer Join ( Select isNull(Sum(isNull(isNull(Qty,0)*isNull(Rate,0),0)),0) As ActualByProductionCost , ProductionOrderId " _
        '         & "from ProductionOrderOutputMaterial where ItemType = 'ByProduct' Group By ProductionOrderId) " _
        '         & "As ActualByProduct On ProductionOrderOutputMaterial.ProductionOrderId = ActualByProduct.ProductionOrderId " _
        '         & "Left Outer Join ( Select isNull(Sum(isNull(Amount,0)),0) As ActualLabourCost , ProductionOrderId " _
        '         & "from ProductionOrderLabour Group By ProductionOrderId) " _
        '         & "As ActualLabour On ProductionOrderOutputMaterial.ProductionOrderId = ActualLabour.ProductionOrderId " _
        '         & "Left Outer Join ( Select isNull(Sum(isNull(Amount,0)),0) As ActualOHExpense , ProductionOrderId " _
        '         & "from ProductionOrderOverHeads Group By ProductionOrderId) " _
        '         & "As ActualOverHead On ProductionOrderOutputMaterial.ProductionOrderId = ActualOverHead.ProductionOrderId " _
        '         & "where ProductionOrderOutputMaterial.ItemType = 'Finish' "

        'If Me.lstItem.SelectedIDs.Length > 0 Then
        '    strSQL += " And ProductionOrderInputMaterial.ItemId In (" & Me.lstItem.SelectedIDs & ") "
        'End If

        'strSQL += ") As ProductionOrderData On FinishGoodDetail.MaterialArticleId = ProductionOrderData.ItemId where ProductionOrderData.ProductionOrderDate between '" & fromDateTimePicker.Value.ToString("yyyy-M-d 00:00:00") & "' And '" & toDateTimePicker.Value.ToString("yyyy-M-d 23:59:59") & "' "


        'If Me.lstItem.SelectedIDs.Length > 0 Then
        '    strSQL += "  And FinishGoodDetail.MaterialArticleId In (" & Me.lstItem.SelectedIDs & ")" & " And FinishGoodDetail.FinishGoodId In (select min (FinishGoodId) from FinishGoodDetail where MaterialArticleId In (" & Me.lstItem.SelectedIDs & "))"
        'End If

        'If Me.lstLocation.SelectedIDs.Length > 0 Then
        '    strSQL += " And (ProductionOrderData.InputLocationId In (" & Me.lstLocation.SelectedIDs & ")" & " Or ProductionOrderData.OutputLocationId In (" & Me.lstLocation.SelectedIDs & "))"
        'End If

        'If Me.lstInventoryDepartment.SelectedIDs.Length > 0 Then
        '    strSQL += " And ArticleDefView.ArticleGroupId In (" & Me.lstInventoryDepartment.SelectedIDs & ")"
        'End If

        'If Me.lstInventoryType.SelectedIDs.Length > 0 Then
        '    strSQL += " And ArticleDefView.ArticleTypeId In (" & Me.lstInventoryType.SelectedIDs & ")"
        'End If

        'If Me.lstInventoryCategory.SelectedIDs.Length > 0 Then
        '    strSQL += " And ArticleDefView.ArticleCompanyId In  (" & Me.lstInventoryCategory.SelectedIDs & ")"
        'End If


        'strSQL = "select ProductionOrder.ProductionOrderNo , ProductionOrder.ProductionOrderDate , ArticleDefView.ArticleDescription As Item  , " _
        '      & "isNull(ProductionOrderOutputMaterial.Qty,0) As Quantity , isNull(FinishGood.MeterialCost,0) As  MeterialCost , " _
        '      & "isNull(ProductionOrderInputMaterial.Rate,0) As ActualMaterialCost , isNull(FinishGood.ByProductionCost,0) As ByProductionCost , " _
        '      & "isNull(ActualByProduct.ActualByProductionCost,0) As ActualByProductionCost , " _
        '      & "isNull(isNull(FinishGood.MeterialCost,0) - isNull( FinishGood.ByProductionCost,0),0) As NetExpense , " _
        '      & "isNull(isNull(ProductionOrderInputMaterial.Rate,0) - isNull(ActualByProduct.ActualByProductionCost,0),0) As ActualNetExpense , " _
        '      & "isNull(FinishGood.LabourCost , 0) As LabourCost , " _
        '      & "isNull(ActualLabour.ActualLabourCost,0) As ActualLabourCost , isNull(FinishGood.OHExpense , 0) As OHExpense , " _
        '      & "IsNull(ActualOverHead.ActualOHExpense,0) As ActualOHExpense , " _
        '      & "isNull(isNull(isNull(ProductionOrderInputMaterial.Rate,0) - isNull(ActualByProduct.ActualByProductionCost,0),0) + isNull(ActualLabour.ActualLabourCost,0) +  IsNull(ActualOverHead.ActualOHExpense,0),0) As ActualTotal , " _
        '      & "isNull(FinishGood.FixedTotal,0) As FixedTotal ,  0 AS TotalQuantity , 0 As SumActualTotal , 0 As SumFixedTotal , 0 As ActualPerunitCost , 0 As FixedPerUnitCost " _
        '      & "from ProductionOrderInputMaterial " _
        '      & "Left Outer Join ProductionOrder On ProductionOrderInputMaterial.ProductionOrderId = ProductionOrder.ProductionOrderId " _
        '      & "Left Outer Join ProductionOrderOutputMaterial On ProductionOrder.ProductionOrderId = ProductionOrderOutputMaterial.ProductionOrderId " _
        '      & "Left Outer Join ( Select isNull(Sum(isNull(isNull(Qty,0)*isNull(Rate,0),0)),0) As ActualByProductionCost , ProductionOrderId " _
        '      & "from ProductionOrderOutputMaterial where ItemType = 'ByProduct' Group By ProductionOrderId) " _
        '      & "As ActualByProduct On ProductionOrderOutputMaterial.ProductionOrderId = ActualByProduct.ProductionOrderId " _
        '      & "Left Outer Join ( Select isNull(Sum(isNull(Amount,0)),0) As ActualLabourCost , ProductionOrderId " _
        '      & "from ProductionOrderLabour Group By ProductionOrderId) " _
        '      & "As ActualLabour On ProductionOrderOutputMaterial.ProductionOrderId = ActualLabour.ProductionOrderId " _
        '      & "Left Outer Join ( Select isNull(Sum(isNull(Amount,0)),0) As ActualOHExpense , ProductionOrderId " _
        '      & "from ProductionOrderOverHeads Group By ProductionOrderId) " _
        '      & "As ActualOverHead On ProductionOrderOutputMaterial.ProductionOrderId = ActualOverHead.ProductionOrderId " _
        '      & "Left Outer Join (" _
        '      & "Select isNull(FinishGoodDetail.CostPrice,0) AS MeterialCost , isNull(FixedByProduct.FixedByProductionCost,0) As ByProductionCost , " _
        '      & "isNull(isNull(FinishGoodDetail.CostPrice,0) - isNull(FixedByProduct.FixedByProductionCost,0),0) As NetExpense , " _
        '      & "isNull(FixedLabour.FixedLabourCost , 0) As LabourCost , isNull(FixedOverHead.FixedOHExpense , 0) As OHExpense , " _
        '      & "isNull(isNull(isNull(FinishGoodDetail.CostPrice,0) - isNull(FixedByProduct.FixedByProductionCost,0),0) + isNull(FixedLabour.FixedLabourCost , 0) " _
        '      & "+ isNull(FixedOverHead.FixedOHExpense , 0) , 0) As FixedTotal , FinishGoodDetail.MaterialArticleId " _
        '      & "from FinishGoodDetail " _
        '      & "Left Outer Join ( select sum(isNull(FinishGoodByProducts.Rate,0)) As FixedByProductionCost , FinishGoodByProducts.FinishGoodId " _
        '      & "from FinishGoodByProducts Group By FinishGoodByProducts.FinishGoodId ) " _
        '      & "As FixedByProduct On FinishGoodDetail.FinishGoodId = FixedByProduct.FinishGoodId " _
        '      & "Left Outer Join ( select sum(isNull(FinishGoodLabourAllocation.RatePerUnit,0)) As FixedLabourCost , FinishGoodLabourAllocation.FinishGoodId " _
        '      & "from FinishGoodLabourAllocation Group By FinishGoodLabourAllocation.FinishGoodId ) " _
        '      & "As FixedLabour On FinishGoodDetail.FinishGoodId = FixedLabour.FinishGoodId " _
        '      & "Left Outer Join ( select sum(isNull(FinishGoodOverHeads.Amount,0)) As FixedOHExpense , FinishGoodOverHeads.FinishGoodId " _
        '      & "from FinishGoodOverHeads Group By FinishGoodOverHeads.FinishGoodId ) " _
        '      & "As FixedOverHead On FinishGoodDetail.FinishGoodId = FixedOverHead.FinishGoodId where "

        'If Me.lstItem.SelectedIDs.Length > 0 Then
        '    strSQL += " FinishGoodDetail.MaterialArticleId In (" & Me.lstItem.SelectedIDs & ") And "
        'End If

        'strSQL += "FinishGoodDetail.FinishGoodId In " _
        '      & "(select min (FinishGoodId) from FinishGoodDetail "

        'If Me.lstItem.SelectedIDs.Length > 0 Then
        '    strSQL += "  where MaterialArticleId In (" & Me.lstItem.SelectedIDs & ") "
        'End If


        'strSQL += ")) As FinishGood " _
        '      & "On ProductionOrderInputMaterial.ItemId = FinishGood.MaterialArticleId And ProductionOrderOutputMaterial.ItemId = FinishGood.MaterialArticleId " _
        '      & "Left Outer Join ArticleDefView On ProductionOrderInputMaterial.ItemId = ArticleDefView.ArticleId And ProductionOrderOutputMaterial.ItemId = ArticleDefView.ArticleId " _
        '      & "where ProductionOrderOutputMaterial.ItemType = 'Finish' And ProductionOrder.ProductionOrderDate between '2016-12-24' And '2018-10-10' "

        'If Me.lstItem.SelectedIDs.Length > 0 Then
        '    strSQL += "  And ArticleDefView.ArticleId In (" & Me.lstItem.SelectedIDs & ")"
        'End If

        'If Me.lstLocation.SelectedIDs.Length > 0 Then
        '    strSQL += " And (ProductionOrderOutputMaterial.LocationId In (" & Me.lstLocation.SelectedIDs & ")" & " Or ProductionOrderInputMaterial.LocationId In (" & Me.lstLocation.SelectedIDs & ")) "
        'End If

        'If Me.lstInventoryDepartment.SelectedIDs.Length > 0 Then
        '    strSQL += " And ArticleDefView.ArticleGroupId In (" & Me.lstInventoryDepartment.SelectedIDs & ") "
        'End If

        'If Me.lstInventoryType.SelectedIDs.Length > 0 Then
        '    strSQL += " And ArticleDefView.ArticleTypeId In (" & Me.lstInventoryType.SelectedIDs & ") "
        'End If

        'If Me.lstInventoryCategory.SelectedIDs.Length > 0 Then
        '    strSQL += " And ArticleDefView.ArticleCompanyId In  (" & Me.lstInventoryCategory.SelectedIDs & ") "
        'End If




        strSQL = "select ProductionOrder.ProductionOrderNo , ProductionOrder.ProductionOrderDate , ArticleDefView.ArticleDescription As Item , " _
                 & "ProductionOrderOutputMaterial.Qty As Quantity , isNull(FinishGood.MaterialCost,0) AS MeterialCost , " _
                 & "isNull(ProductionInput.ActualMaterialCost,0) As ActualMaterialCost , " _
                 & "isNull(FinishGood.FixedByProductionCost,0) As ByProductionCost , " _
                 & "isNull(ActualByProduct.ActualByProductionCost,0) As ActualByProductionCost, " _
                 & "isNull(isNull(FinishGood.MaterialCost,0) - isNull(FinishGood.FixedByProductionCost,0),0) As NetExpense , " _
                 & "isNull(isNull(ProductionInput.ActualMaterialCost,0) - isNull(ActualByProduct.ActualByProductionCost,0),0) As ActualNetExpense , " _
                 & "isNull(FinishGood.LabourCost,0) As LabourCost , " _
                 & "isNull(ActualLabour.ActualLabourCost,0) As ActualLabourCost , " _
                 & "isNull(FinishGood.OHExpense , 0) As OHExpense , " _
                 & "isNull(ActualOverHead.ActualOHExpense,0) As ActualOHExpense , " _
                 & "isNull(isNull(isNull(ProductionInput.ActualMaterialCost,0) - isNull(ActualByProduct.ActualByProductionCost,0),0) + " _
                 & "isNull(ActualLabour.ActualLabourCost,0) +  IsNull(ActualOverHead.ActualOHExpense,0),0) As ActualTotal , " _
                 & "isNull(FinishGood.FixedTotal,0) As FixedTotal , " _
                 & "Convert(float,0) AS TotalQuantity , Convert(float,0) As SumActualTotal , Convert(float,0) As SumFixedTotal , Convert(float,0) As ActualPerunitCost , Convert(float,0) As FixedPerUnitCost " _
                 & "from ProductionOrderOutputMaterial " _
                 & "Left Outer Join ProductionOrder On ProductionOrderOutputMaterial.ProductionOrderId = ProductionOrder.ProductionOrderId " _
                 & "Left Outer Join ( Select isNull(Sum(isNull(isNull(Qty,0)*isNull(Rate,0),0)),0) As ActualMaterialCost , ProductionOrderId " _
                 & "from ProductionOrderInputMaterial group By ProductionOrderId) As ProductionInput " _
                 & "On ProductionOrderOutputMaterial.ProductionOrderId = ProductionInput.ProductionOrderId " _
                 & "Left Outer Join ( Select isNull(Sum(isNull(isNull(Qty,0)*isNull(Rate,0),0)),0) As ActualByProductionCost , ProductionOrderId " _
                 & "from ProductionOrderOutputMaterial where ItemType = 'ByProduct' Group By ProductionOrderId) " _
                 & "As ActualByProduct On ProductionOrderOutputMaterial.ProductionOrderId = ActualByProduct.ProductionOrderId " _
                 & "Left Outer Join ( Select isNull(Sum(isNull(Amount,0)),0) As ActualLabourCost , ProductionOrderId " _
                 & "from ProductionOrderLabour Group By ProductionOrderId) " _
                 & "As ActualLabour On ProductionOrderOutputMaterial.ProductionOrderId = ActualLabour.ProductionOrderId " _
                 & "Left Outer Join ( Select isNull(Sum(isNull(Amount,0)),0) As ActualOHExpense , ProductionOrderId " _
                 & "from ProductionOrderOverHeads Group By ProductionOrderId) " _
                 & "As ActualOverHead On ProductionOrderOutputMaterial.ProductionOrderId = ActualOverHead.ProductionOrderId " _
                 & "Left Outer Join ArticleDefView On ProductionOrderOutputMaterial.ItemId = ArticleDefView.ArticleId " _
                 & "Left Outer Join ( " _
                 & "select  isNull(FinishGoodDetails.MaterialCost,0) As MaterialCost , MasterArticleId , " _
                 & "isNull(FixedByProduct.FixedByProductionCost,0) As FixedByProductionCost , " _
                 & "isNull(isNull(FinishGoodDetails.MaterialCost,0) - isNull(FixedByProduct.FixedByProductionCost,0),0) As NetExpense , " _
                 & "isNull(FixedLabour.FixedLabourCost,0) As LabourCost , isNull(FixedOverHead.FixedOHExpense , 0) As OHExpense , " _
                 & "isNull(isNull(isNull(FinishGoodDetails.MaterialCost,0) - isNull(FixedByProduct.FixedByProductionCost,0),0) + isNull(FixedLabour.FixedLabourCost , 0) " _
                 & "+ isNull(FixedOverHead.FixedOHExpense , 0) , 0) As FixedTotal " _
                 & "from FinishGoodMaster " _
                 & "Left Outer Join (select isNull(Sum(isNull(isNull(Qty,0)*isNull(CostPrice,0),0)),0) As MaterialCost , FinishGoodId " _
                 & "from FinishGoodDetail group By FinishGoodId) " _
                 & "As FinishGoodDetails On FinishGoodMaster.id = FinishGoodDetails.FinishGoodId " _
                 & "Left Outer Join ( select sum(isNull(FinishGoodByProducts.Rate,0)) As FixedByProductionCost , FinishGoodByProducts.FinishGoodId " _
                 & "from FinishGoodByProducts Group By FinishGoodByProducts.FinishGoodId ) " _
                 & "As FixedByProduct On FinishGoodMaster.id = FixedByProduct.FinishGoodId " _
                 & "Left Outer Join ( select sum(isNull(FinishGoodLabourAllocation.RatePerUnit,0)) As FixedLabourCost , FinishGoodLabourAllocation.FinishGoodId " _
                 & "from FinishGoodLabourAllocation Group By FinishGoodLabourAllocation.FinishGoodId ) " _
                 & "As FixedLabour On FinishGoodMaster.id = FixedLabour.FinishGoodId " _
                 & "Left Outer Join ( select sum(isNull(FinishGoodOverHeads.Amount,0)) As FixedOHExpense , FinishGoodOverHeads.FinishGoodId " _
                 & "from FinishGoodOverHeads Group By FinishGoodOverHeads.FinishGoodId ) " _
                 & "As FixedOverHead On FinishGoodMaster.id = FixedOverHead.FinishGoodId " _
                 & "where FinishGoodMaster.id In (select min (Id) from FinishGoodMaster group By MasterArticleId) " _
                 & ") As FinishGood " _
                 & "On  ArticleDefView.MasterId = FinishGood.MasterArticleId " _
                 & "where ProductionOrderOutputMaterial.ItemType = 'Finish' And ProductionOrder.ProductionOrderDate between '" & fromDateTimePicker.Value.ToString("yyyy-M-d 00:00:00") & "' And '" & toDateTimePicker.Value.ToString("yyyy-M-d 23:59:59") & "' "

        If Me.lstItem.SelectedIDs.Length > 0 Then
            strSQL += "  And ProductionOrderOutputMaterial.ItemId In (" & Me.lstItem.SelectedIDs & ")"
        End If

        If Me.lstLocation.SelectedIDs.Length > 0 Then
            strSQL += " And ProductionOrderOutputMaterial.LocationId In (" & Me.lstLocation.SelectedIDs & ")" & ""
        End If

        If Me.lstInventoryDepartment.SelectedIDs.Length > 0 Then
            strSQL += " And ArticleDefView.ArticleGroupId In (" & Me.lstInventoryDepartment.SelectedIDs & ") "
        End If

        If Me.lstInventoryType.SelectedIDs.Length > 0 Then
            strSQL += " And ArticleDefView.ArticleTypeId In (" & Me.lstInventoryType.SelectedIDs & ") "
        End If

        If Me.lstInventoryCategory.SelectedIDs.Length > 0 Then
            strSQL += " And ArticleDefView.ArticleCompanyId In  (" & Me.lstInventoryCategory.SelectedIDs & ") "
        End If


        dt = GetDataTable(strSQL)

        Me.grdCostComparison.DataSource = dt

        ApplyGridSettings()

        Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            CallShowReport()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CallShowReport()

        Try
            AddRptParam("@FromDate", Me.fromDateTimePicker.Value.ToString("yyyy-MM-d 00:00:00"))
            AddRptParam("@ToDate", Me.toDateTimePicker.Value.ToString("yyyy-MM-d 23:59:59"))
            ''Start TFS3418
            AddRptParam("@ItemId", Me.lstItem.SelectedIDs)
            AddRptParam("@LocationId", Me.lstLocation.SelectedIDs)
            AddRptParam("@Department", Me.lstInventoryDepartment.SelectedIDs)
            AddRptParam("@TypeId", Me.lstInventoryType.SelectedIDs)
            AddRptParam("@CategoryId", Me.lstInventoryCategory.SelectedIDs)

            ''End TFS3418
            ShowReport("rptCostComparison") ' IIf(Me.cmbEmployee.ActiveRow.Cells(0).Value > 0, " and {SP_Employee_Attendance;1.Employee_Id}=" & Me.cmbEmployee.Value & "", "") & " " & IIf(Me.cmbCostCenter.SelectedValue > 0, " and {SP_Employee_Attendance;1.CostCentre}=" & Me.cmbCostCenter.SelectedValue & "", ""))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub CtrlGrdBar_Load(sender As Object, e As EventArgs)
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdCostComparison.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdCostComparison.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdCostComparison.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar1.strForm_Name = CtrlGrdBar1.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Delivery Chalan"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class