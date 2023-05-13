Imports SBModel
Imports SBDal
Imports System.Data.SqlClient

Public Class frmTicketConsumptionDisplay
    Implements IGeneral

    Public StoreIssue As Boolean
    Public ItemsConsumption As Boolean
    Public LocationId As Integer = 0
    Public Location1 As String = String.Empty

    Sub New(ByVal frmStoreIssue As Boolean, frmItemsConsumption As Boolean, ByVal LocationId As Integer, ByVal Location As String)

        InitializeComponent()

        Me.StoreIssue = frmStoreIssue
        Me.ItemsConsumption = frmItemsConsumption
        Me.LocationId = LocationId
        Me.Location1 = Location
    End Sub

    Private Sub grdItems_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs)

    End Sub
    Private Sub dtpDate_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub grdItems_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdItems.FormattingRow

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            FillDropDown(cmbSalesOrder, " SELECT SalesOrderId, SalesOrderNo from SalesOrderMasterTable ORDER BY SalesOrderDate DESC")
            FillDropDown(cmbPlanNo, " SELECT PlanId, PlanNo from PlanMasterTable ORDER BY PlanDate DESC")
            FillDropDown(cmbTicketNo, " SELECT PlanTicketsMasterID , BatchNo , PlanID from PlanTicketsMaster where BatchNo <> '' ORDER BY TicketDate DESC")
            FillDropDown(cmbStage, " SELECT ProdStep_id , prod_step FROM tblproSteps")
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
            If Not cmbSalesOrder.SelectedIndex = -1 Then
                Me.cmbSalesOrder.SelectedIndex = 0
            End If

            If Not cmbPlanNo.SelectedIndex = -1 Then
                Me.cmbPlanNo.SelectedIndex = 0
            End If

            If Not cmbTicketNo.SelectedIndex = -1 Then
                Me.cmbTicketNo.SelectedIndex = 0
            End If

            If Not cmbStage.SelectedIndex = -1 Then
                Me.cmbStage.SelectedIndex = 0
            End If

            grdItems.DataSource = Nothing
        Catch ex As Exception
            Throw ex
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

    Private Sub frmTicketProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ReSetControls()
            FillCombos()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbSalesOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSalesOrder.SelectedIndexChanged

        Try
            If cmbSalesOrder.SelectedValue <= 0 Then
                FillDropDown(cmbPlanNo, "select PlanId , PlanNo from PlanMasterTable")

            Else
                FillDropDown(cmbPlanNo, "select PlanId , PlanNo from PlanMasterTable where POid = " & cmbSalesOrder.SelectedValue)

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbPlanNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlanNo.SelectedIndexChanged

        If Me.cmbPlanNo.SelectedValue > 0 Then
            btnSearch_Click(Nothing, Nothing)
        End If

        Try
            If cmbPlanNo.SelectedValue <= 0 Then
                FillDropDown(cmbTicketNo, "select PlanTicketsMasterID , BatchNo , PlanID from PlanTicketsMaster where BatchNo <> ''")

            Else
                FillDropDown(cmbTicketNo, "select PlanTicketsMasterID , BatchNo , PlanID from PlanTicketsMaster where BatchNo <> '' And planid  = " & cmbPlanNo.SelectedValue)

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbTicketNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTicketNo.SelectedIndexChanged

        If Me.cmbTicketNo.SelectedValue > 0 Then
            btnSearch_Click(Nothing, Nothing)
        End If

        Try
            If cmbTicketNo.SelectedValue <= 0 Then
                FillDropDown(cmbStage, "SELECT ProdStep_id , prod_step FROM tblproSteps")

            Else

                FillDropDown(cmbStage, "select tblproSteps.ProdStep_id , tblproSteps.prod_step from ProductionTicketStages INNER JOIN PlanTicketsMaster " _
                             & "on ProductionTicketStages.TicketId = PlanTicketsMaster.PlanTicketsMasterID INNER JOIN tblproSteps " _
                             & "on ProductionTicketStages.ProductionStageId = tblproSteps.ProdStep_id where ProductionTicketStages.TicketId = " & cmbTicketNo.SelectedValue)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Try

            'If (cmbTicketNo.SelectedValue > 0 And cmbStage.SelectedValue <= 0) Then

            If StoreIssue = True Then

              

                '    fillGrid("select PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
                '      & "sum(IsNull((PlanTicketMaterialDetail.Qty),0)) as PendingQty , ArticleDefTable.ArticleCode as Code , ArticleColorDefTable.ArticleColorName as Color , PlanTicketMaterialDetail.CostPrice as Rate , sum(IsNull((PlanTicketMaterialDetail.Qty),0))*PlanTicketMaterialDetail.CostPrice as Total from PlanTicketMaterialDetail " _
                '      & "left JOIN PlanTicketsMaster on " _
                '      & "PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
                '      & "left JOIN ArticleDefTable on " _
                '      & "ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
                '      & "left JOIN tblproSteps on " _
                '      & "tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
                '      & "left JOIN ArticleColorDefTable on " _
                '      & "ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
                '      & "where TicketId = " & cmbTicketNo.SelectedValue & " " _
                '      & "group By PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , " _
                '      & "ArticleDefTable.ArticleDescription , tblproSteps.prod_step , ArticleDefTable.ArticleCode , ArticleColorDefTable.ArticleColorName , PlanTicketMaterialDetail.CostPrice")

                '    'msg_Information(cmbTicketNo.SelectedValue)

            ElseIf ItemsConsumption = True Then
                Dim dt As New DataTable
                dt = PlanTicketsStandardDAL.GetTicketRecordForConsumption(Me.cmbSalesOrder.SelectedValue, Me.cmbPlanNo.SelectedValue, Me.cmbTicketNo.SelectedValue, Me.cmbStage.SelectedValue)
                Me.grdItems.DataSource = dt
                Me.grdItems.RootTable.Columns("PendingQty").FormatString = "N" & DecimalPointInQty
                Me.grdItems.RootTable.Columns("Total").FormatString = "N" & DecimalPointInValue
                Me.grdItems.RootTable.Columns("TotalReturnedQty").FormatString = "N" & DecimalPointInQty
                Me.grdItems.RootTable.Columns("EstimatedQty").FormatString = "N" & DecimalPointInQty
                Me.grdItems.RootTable.Columns("TotalIssuedQty").FormatString = "N" & DecimalPointInQty

                Me.grdItems.RootTable.Columns("IssuancePending").FormatString = "N" & DecimalPointInQty
                'Me.grdItems.RootTable.Columns("TotalIssuedQty").FormatString = "N" & DecimalPointInQty
                '    fillGrid("select PlanTicketsMaster.TicketNo , dispatchDetailTable.ArticleDefId as ArticleId , dispatchDetailTable.SubDepartmentID as DepartmentID , dispatchMasterTable.PlanTicketId as TicketId " _
                '     & ", ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage , IsNull(PlanTicketMaterialDetail.Qty,0)-IsNull(tblTrackEstimationConsumption.ConsumedQty,0) as PendingQty , ArticleDefTable.ArticleCode as Code , ArticleColorDefTable.ArticleColorName as Color " _
                '     & ", PlanTicketMaterialDetail.CostPrice as Rate , (IsNull(PlanTicketMaterialDetail.Qty,0)-IsNull(tblTrackEstimationConsumption.ConsumedQty,0))*PlanTicketMaterialDetail.CostPrice as Total , IsNull(dispatchDetailTable.ReturnedTotalQty,0) as TotalReturnedQty from dispatchMasterTable " _
                '     & "LEFT JOIN PlanTicketsMaster " _
                '     & "on PlanTicketsMaster.PlanTicketsMasterId = dispatchMasterTable.PlanTicketId " _
                '     & "Left JOIN dispatchDetailTable " _
                '     & "on  dispatchDetailTable.DispatchId = dispatchMasterTable.DispatchId " _
                '     & "Left JOIN ArticleDefTable " _
                '     & "on ArticleDefTable.ArticleId = dispatchDetailTable.ArticleDefId " _
                '     & "Left JOIN tblproSteps " _
                '     & "on tblproSteps.ProdStep_id = dispatchDetailTable.SubDepartmentID " _
                '     & "Left JOIN ArticleColorDefTable " _
                '     & "on ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
                '     & "Left JOIN PlanTicketMaterialDetail " _
                '     & "on PlanTicketMaterialDetail.TicketId = dispatchMasterTable.PlanTicketId and PlanTicketMaterialDetail.DepartmentId = dispatchDetailTable.SubDepartmentID " _
                '     & "Left JOIN tblTrackEstimationConsumption " _
                '     & "on tblTrackEstimationConsumption.ArticleId = dispatchDetailTable.ArticleDefId " _
                '     & "where dispatchMasterTable.PlanTicketId = " & cmbTicketNo.SelectedValue)

                'End If

                'ElseIf (cmbTicketNo.SelectedValue > 0 And cmbStage.SelectedValue > 0) Then

                'If StoreIssue = True Then

                '    fillGrid("select PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
                '      & "sum(IsNull((PlanTicketMaterialDetail.Qty),0)) as PendingQty , ArticleDefTable.ArticleCode as Code , ArticleColorDefTable.ArticleColorName as Color , PlanTicketMaterialDetail.CostPrice as Rate , sum(IsNull((PlanTicketMaterialDetail.Qty),0))*PlanTicketMaterialDetail.CostPrice as Total from PlanTicketMaterialDetail " _
                '      & "left JOIN PlanTicketsMaster on " _
                '      & "PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
                '      & "left JOIN ArticleDefTable on " _
                '      & "ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
                '      & "left JOIN tblproSteps on " _
                '      & "tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
                '      & "left JOIN ArticleColorDefTable on " _
                '      & "ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
                '      & "where TicketId = " & cmbTicketNo.SelectedValue & " and PlanTicketMaterialDetail.DepartmentId = " & cmbStage.SelectedValue & " " _
                '      & "group By PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , " _
                '      & "ArticleDefTable.ArticleDescription , tblproSteps.prod_step , ArticleDefTable.ArticleCode , ArticleColorDefTable.ArticleColorName , PlanTicketMaterialDetail.CostPrice")

                '    'msg_Information(cmbTicketNo.SelectedValue & "---" & cmbStage.SelectedValue)

                'ElseIf ItemsConsumption = True Then

                '    fillGrid("select PlanTicketsMaster.TicketNo , dispatchDetailTable.ArticleDefId as ArticleId , dispatchDetailTable.SubDepartmentID as DepartmentID , dispatchMasterTable.PlanTicketId as TicketId " _
                '     & ", ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage , IsNull(PlanTicketMaterialDetail.Qty,0)-IsNull(tblTrackEstimationConsumption.ConsumedQty,0) as PendingQty , ArticleDefTable.ArticleCode as Code , ArticleColorDefTable.ArticleColorName as Color " _
                '     & ", PlanTicketMaterialDetail.CostPrice as Rate , (IsNull(PlanTicketMaterialDetail.Qty,0)-IsNull(tblTrackEstimationConsumption.ConsumedQty,0))*PlanTicketMaterialDetail.CostPrice as Total , IsNull(dispatchDetailTable.ReturnedTotalQty,0) as TotalReturnedQty from dispatchMasterTable " _
                '     & "LEFT JOIN PlanTicketsMaster " _
                '     & "on PlanTicketsMaster.PlanTicketsMasterId = dispatchMasterTable.PlanTicketId " _
                '     & "Left JOIN dispatchDetailTable " _
                '     & "on  dispatchDetailTable.DispatchId = dispatchMasterTable.DispatchId " _
                '     & "Left JOIN ArticleDefTable " _
                '     & "on ArticleDefTable.ArticleId = dispatchDetailTable.ArticleDefId " _
                '     & "Left JOIN tblproSteps " _
                '     & "on tblproSteps.ProdStep_id = dispatchDetailTable.SubDepartmentID " _
                '     & "Left JOIN ArticleColorDefTable " _
                '     & "on ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
                '     & "Left JOIN PlanTicketMaterialDetail " _
                '     & "on PlanTicketMaterialDetail.TicketId = dispatchMasterTable.PlanTicketId and PlanTicketMaterialDetail.DepartmentId = dispatchDetailTable.SubDepartmentID " _
                '     & "Left JOIN tblTrackEstimationConsumption " _
                '     & "on tblTrackEstimationConsumption.ArticleId = dispatchDetailTable.ArticleDefId " _
                '     & "where dispatchMasterTable.PlanTicketId = " & cmbTicketNo.SelectedValue & "and dispatchDetailTable.SubDepartmentID = " & cmbStage.SelectedValue)

                'End If

                'Else
                'ShowErrorMessage("Tickets or Stage with Tickets must selected")

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Public Sub fillGrid(query As String)

        Try
            Dim dt As New DataTable
            dt = New PlanTicketsDAL().GetDeatilsByTickets(query)
            Me.grdItems.DataSource = dt

            Me.grdItems.RootTable.Columns("PendingQty").FormatString = "N" & DecimalPointInValue
            'Me.grdItems.RootTable.Columns("CurrentValue").FormatString = "N" & DecimalPointInValue
            'Me.grdItems.RootTable.Columns("ClosingValue").FormatString = "N" & DecimalPointInValue
            'Me.grdItems.RootTable.Columns("AcquireCost").FormatString = "N" & DecimalPointInValue
            'Me.grdItems.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub grdItems_KeyDown(sender As Object, e As KeyEventArgs) Handles grdItems.KeyDown

        Dim dt As New DataTable
        Dim dt1 As New DataTable

        If StoreIssue = True Then

            'dt.Columns.Add("LocationId")
            'dt.Columns.Add("ArticleCode")
            'dt.Columns.Add("Item")
            'dt.Columns.Add("Color")
            'dt.Columns.Add("BatchNo")
            'dt.Columns.Add("Unit")
            'dt.Columns.Add("Qty")
            'dt.Columns.Add("Rate")
            'dt.Columns.Add("Total")
            'dt.Columns.Add("CategoryId")
            'dt.Columns.Add("ItemId")
            'dt.Columns.Add("PackQty")
            'dt.Columns.Add("CurrentPrice")
            'dt.Columns.Add("PackPrice")
            'dt.Columns.Add("BatchId")
            'dt.Columns.Add("ArticleDefMasterId")
            'dt.Columns.Add("ArticleDepriciationMaster")
            'dt.Columns.Add("Pack_Desc")
            'dt.Columns.Add("PurchaseAccountId")
            'dt.Columns.Add("CGSAccountId")
            'dt.Columns.Add("CostPrice")
            'dt.Columns.Add("PlanUnit")
            'dt.Columns.Add("PlanQty")
            'dt.Columns.Add("LotNo")
            'dt.Columns.Add("Rack_No")
            'dt.Columns.Add("Comments")

            'dt.Columns.Add("Stock")

            'dt.Columns.Add("TotalQty")

            'dt.Columns.Add("SubDepartmentID")
            'dt.Columns.Add("SubDepartment")
            'dt.Columns.Add("AllocationDetailId")
            'dt.Columns.Add("ParentId")
            'dt.Columns.Add("EstimationId")
            'dt.Columns.Add("EstimatedQty")
            'dt.Columns.Add("DispatchId")
            'dt.Columns.Add("DispatchDetailId")
            'dt.Columns.Add("CheckQty")
            'dt.Columns.Add("IssuedQty")
            'dt.Columns.Add("SubItem")



            'dt.Columns.Add("DepartmentId")
            'dt.Columns.Add("Department")
            'dt.Columns.Add("TicketNo")
            'dt.Columns.Add("TicketId")


        ElseIf ItemsConsumption = True Then
            dt1.Columns.Add("LocationId", GetType(Int32))
            dt1.Columns.Add("Location", GetType(String))
            dt1.Columns.Add("ConsumptionDetailId", GetType(Int32))
            dt1.Columns.Add("ConsumptionId", GetType(Int32))
            dt1.Columns.Add("ArticleId", GetType(Int32))
            dt1.Columns.Add("ArticleCode", GetType(String))
            dt1.Columns.Add("ArticleDescription", GetType(String))
            dt1.Columns.Add("Color", GetType(String))
            dt1.Columns.Add("Qty", GetType(Double))
            dt1.Columns.Add("ConsumedQty", GetType(Double))
            dt1.Columns.Add("AvailableQty", GetType(Double))
            dt1.Columns.Add("Rate", GetType(Double))
            dt1.Columns.Add("Total", GetType(Double))
            dt1.Columns.Add("DispatchId", GetType(Int32))
            dt1.Columns.Add("DispatchDetailId", GetType(Int32))
            dt1.Columns.Add("CGSAccountId", GetType(Int32))
            dt1.Columns.Add("Comments", GetType(String))
            dt1.Columns.Add("CheckQty", GetType(Double))
            dt1.Columns.Add("EstimationId", GetType(Int32))
            dt1.Columns.Add("ParentTag#", GetType(Int32)) '
            dt1.Columns.Add("EstimatedQty", GetType(Double)) 'EstimatedQty
            dt1.Columns.Add("DepartmentId", GetType(Int32)) 'EstimatedQty
            dt1.Columns.Add("TotalIssuedQty", GetType(Double))
            dt1.Columns.Add("TotalConsumedQty", GetType(Double))
            dt1.Columns.Add("TotalReturnedQty", GetType(Double))
            dt1.Columns.Add("TicketId", GetType(Int32))
            dt1.Columns.Add("PlanNo", GetType(String))
            dt1.Columns.Add("TicketNo", GetType(String))

        End If
        For Each row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
            Dim R As DataRow = dt.NewRow
            Dim R1 As DataRow = dt1.NewRow
            If StoreIssue = True Then
                'R("LocationId") = Location
                'R("ArticleCode") = row.Cells("Code").Value
                'R("Item") = row.Cells("ProductName").Value
                'R("Color") = row.Cells("Color").Value
                'R("BatchNo") = String.Empty
                'R("Unit") = String.Empty
                'R("Qty") = Val(row.Cells("PendingQty").Value.ToString)
                'R("Rate") = Val(row.Cells("Rate").Value.ToString)
                'R("Total") = Val(row.Cells("Total").Value.ToString)
                'R("CategoryId") = 0
                'R("ItemId") = Val(row.Cells("ArticleId").Value.ToString)
                'R("PackQty") = 0
                'R("CurrentPrice") = 0
                'R("PackPrice") = 0
                'R("BatchId") = 0
                'R("ArticleDefMasterId") = 0
                'R("ArticleDepriciationMaster") = String.Empty
                'R("Pack_Desc") = String.Empty
                'R("PurchaseAccountId") = 0
                'R("CGSAccountId") = 0
                'R("CostPrice") = 0
                'R("PlanUnit") = String.Empty
                'R("PlanQty") = 0
                'R("LotNo") = String.Empty
                'R("Rack_No") = String.Empty
                'R("Comments") = String.Empty
                'R("Stock") = 0
                'R("TotalQty") = Val(row.Cells("PendingQty").Value.ToString)
                'R("SubDepartmentID") = Val(row.Cells("DepartmentId").Value.ToString)
                'R("SubDepartment") = row.Cells("Stage").Value
                'R("AllocationDetailId") = 0
                'R("ParentId") = 0
                'R("EstimationId") = 0
                'R("EstimatedQty") = 0
                'R("DispatchId") = 0
                'R("DispatchDetailId") = 0
                'R("CheckQty") = 0
                'R("IssuedQty") = 0
                'R("SubItem") = String.Empty
                'R("TicketNo") = row.Cells("TicketNo").Value
                'R("TicketId") = Val(row.Cells("TicketId").Value.ToString)
                'dt.Rows.Add(R)








            ElseIf ItemsConsumption = True Then
                ''
                R1("LocationId") = LocationId
                If LocationId > 0 Then
                    R1("Location") = Location1
                Else
                    R1("Location") = String.Empty
                End If
                ''
                R1("ConsumptionDetailId") = 0
                R1("ConsumptionId") = 0
                R1("ArticleId") = Val(row.Cells("ArticleId").Value.ToString)
                R1("ArticleCode") = row.Cells("Code").Value
                R1("ArticleDescription") = row.Cells("ProductName").Value
                R1("Color") = row.Cells("Color").Value
                R1("Qty") = Val(row.Cells("PendingQty").Value.ToString)
                R1("ConsumedQty") = 0
                R1("AvailableQty") = Val(row.Cells("AvailableQty").Value.ToString)
                R1("Rate") = Val(row.Cells("Rate").Value.ToString)
                R1("Total") = Val(row.Cells("Total").Value.ToString)
                R1("DispatchId") = 0
                R1("DispatchDetailId") = 0
                R1("CGSAccountId") = Val(row.Cells("CGSAccountId").Value.ToString)
                R1("Comments") = String.Empty
                R1("CheckQty") = Val(row.Cells("CheckQty").Value.ToString)
                R1("EstimationId") = 0
                R1("ParentTag#") = 0
                R1("EstimatedQty") = Val(row.Cells("EstimatedQty").Value.ToString)
                R1("DepartmentId") = Val(row.Cells("DepartmentId").Value.ToString)
                R1("TotalIssuedQty") = Val(row.Cells("TotalIssuedQty").Value.ToString)
                R1("TotalConsumedQty") = Val(row.Cells("TotalConsumedQty").Value.ToString)
                R1("TotalReturnedQty") = Val(row.Cells("TotalReturnedQty").Value.ToString)
                R1("TicketId") = Val(row.Cells("TicketId").Value.ToString)
                R1("PlanNo") = row.Cells("PlanNo").Value.ToString
                R1("TicketNo") = row.Cells("BatchNo").Value.ToString
                dt1.Rows.Add(R1)
            End If
        Next

        'For Each Row As DataRow In dt.Rows

        '    msg_Confirm(Row.Item("TicketNo").ToString & "---" & Row.Item("ProductName").ToString & Row.Item("Stage").ToString & "---" & "---" & Val(Row.Item("PendingQty").ToString) & "---" & Val(Row.Item("ArticleId").ToString) & "---" & Val(Row.Item("DepartmentId").ToString) & "---" & Val(Row.Item("TicketId").ToString))

        'Next


        Try

            If ItemsConsumption = True Then

                'If cmbTicketNo.SelectedValue > 0 Then
                frmItemsConsumption.fillItemsConsumptionGrid(dt1, cmbTicketNo.SelectedValue, Val(CType(cmbTicketNo.SelectedItem, DataRowView).Item("PlanID").ToString))

                Me.Close()

                'Else
                '    ShowErrorMessage("Ticket Number must be selected")

                'End If

            ElseIf StoreIssue = True Then

                If cmbTicketNo.SelectedValue > 0 Then
                    frmStoreIssuenceNew.fillReturnStoreIssuenceGrid(dt, cmbTicketNo.SelectedValue, CType(cmbTicketNo.SelectedItem, DataRowView).Item("PlanID"))

                    Me.Close()

                Else
                    ShowErrorMessage("Ticket Number must be selected")

                End If

            End If

        Catch ex As Exception

            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub BtnResfresh_Click(sender As Object, e As EventArgs) Handles BtnResfresh.Click
        ReSetControls()
    End Sub

    Private Sub cmbStage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStage.SelectedIndexChanged

        If Me.cmbStage.SelectedValue > 0 Then
            btnSearch_Click(Nothing, Nothing)
        End If

    End Sub

    Private Sub BtnLoad_Click(sender As Object, e As EventArgs) Handles BtnLoad.Click

        grdItems_KeyDown(Nothing, Nothing)

    End Sub
End Class