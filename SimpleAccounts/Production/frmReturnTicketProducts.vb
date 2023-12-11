Imports SBModel
Imports SBDal
Imports System.Data.SqlClient

Public Class frmReturnTicketProducts
    Implements IGeneral
    Public StoreIssue As Boolean
    Public ItemsConsumption As Boolean
    Dim LocationId As Integer = 0
    Public dtMerging As DataTable
    Public IsWIPAccount As Boolean = False
    Public CostCenterId As Integer = 0

    Sub New(ByVal frmStoreIssue As Boolean, frmItemsConsumption As Boolean, ByVal LocationId As Integer)
        Try
            InitializeComponent()
            Me.StoreIssue = frmStoreIssue
            Me.ItemsConsumption = frmItemsConsumption
            Me.LocationId = LocationId
        Catch ex As Exception
            Throw ex
        End Try
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
            'FillDropDown(cmbStage, " SELECT ProdStep_id , prod_step FROM tblproSteps")
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
                FillDropDown(cmbTicketNo, "select PlanTicketsMasterID , TicketNo , PlanID from PlanTicketsMaster")

            Else
                FillDropDown(cmbTicketNo, "select PlanTicketsMasterID , BatchNo , PlanID from PlanTicketsMaster where BatchNo <> '' And planid = " & cmbPlanNo.SelectedValue)

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
                'FillDropDown(cmbStage, "SELECT ProdStep_id , prod_step FROM tblproSteps")

            Else

                'FillDropDown(cmbStage, "select tblproSteps.ProdStep_id , tblproSteps.prod_step from ProductionTicketStages INNER JOIN PlanTicketsMaster " _
                '             & "on ProductionTicketStages.TicketId = PlanTicketsMaster.PlanTicketsMasterID INNER JOIN tblproSteps " _
                '             & "on ProductionTicketStages.ProductionStageId = tblproSteps.ProdStep_id where ProductionTicketStages.TicketId = " & cmbTicketNo.SelectedValue)

                FillDropDown(cmbStage, "Select Distinct ProdStep_Id, prod_step, prod_Less, sort_Order , DispatchDetailTable.TicketId from tblProSteps  " _
                             & "INNER JOIN DispatchDetailTable ON tblProSteps.ProdStep_Id = DispatchDetailTable.SubDepartmentID " _
                             & "INNER JOIN DispatchMasterTable ON DispatchDetailTable.DispatchId = DispatchMasterTable.DispatchId  " _
                             & "where DispatchDetailTable.TicketId = " & cmbTicketNo.SelectedValue & " order by sort_Order")

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If StoreIssue = True Then

                'If Me.cmbStage.SelectedValue > 0 Then

                'End If


                Dim dt As New DataTable

                'If getConfigValueByType("AvgRate").ToString = "True" Then

                '    dt = PlanTicketsStandardDAL.GetTicketRecordForStoreIssuance(Me.cmbSalesOrder.SelectedValue, Me.cmbPlanNo.SelectedValue, Me.cmbTicketNo.SelectedValue, Me.cmbStage.SelectedValue, 1)

                'Else

                '    dt = PlanTicketsStandardDAL.GetTicketRecordForStoreIssuance(Me.cmbSalesOrder.SelectedValue, Me.cmbPlanNo.SelectedValue, Me.cmbTicketNo.SelectedValue, Me.cmbStage.SelectedValue, 0)

                'End If

                dt = PlanTicketsStandardDAL.GetTicketRecordForReturnStoreIssuance(Me.cmbSalesOrder.SelectedValue, Me.cmbPlanNo.SelectedValue, Me.cmbTicketNo.SelectedValue, Me.cmbStage.SelectedValue, 0)

                'For Each Row As DataRow In dt.Rows
                '    SetWIPAccount(Row, dt)
                'Next

                Me.grdItems.DataSource = dt
                Me.grdItems.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInValue



                ''Below commented code is according to Old estimation on store issuance 
                ' fillGrid("SELECT 1 As LocationId , ArticleDefTable.ArticleId, Article.ArticleCode, Article.ArticleDescription AS Item, ArticleColorDefTable.ArticleColorName as Color, '' As BatchNo  , '' AS unit,  ((Convert(Float, IsNull(Recv_D.Quantity, 0))+Convert(Float, IsNull(Returned.ReturnedTotalQty, 0)))-Convert(Float, IsNull(tblTrackEstimation.DispatchedQty, 0)))) AS Qty, Article.Cost_Price as Rate, " _
                '& "((Convert(Float, IsNull(Recv_D.Quantity, 0))+Convert(Float, IsNull(Returned.ReturnedTotalQty, 0)))-IsNull(tblTrackEstimation.DispatchedQty, 0))  *  IsNull(Article.PurchasePrice, 0)  AS Total , Article.ArticleGroupId as CategoryId , " _
                '& " , 0 As PackQty, Article.PurchasePrice As CurrentPrice, 0 As PackPrice, 0 As BatchID, 0 as ArticleDefMasterId, '' as [ArticleDescriptionMaster], '' As Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, Article.Cost_Price as CostPrice, '' As PlanUnit, 0 As PlanQty, '' as LotNo, '' As Rack_No, '' As Comments, IsNull(Stock.CurrStock,0) as Stock, " _
                '& " Convert(Decimal(18, " & DecimalPointInQty & "), ((Convert(Float, IsNull(Recv_D.Quantity, 0))+Convert(Float, IsNull(Returned.ReturnedTotalQty, 0)))-Convert(Float, IsNull(tblTrackEstimation.DispatchedQty, 0)))) As TotalQty, IsNull(Recv_D.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, 0 As AllocationDetailId, 0 As ParentId, IsNull(Recv_D.EstimationId, 0) As EstimationId, " _
                '& " Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Recv_D.Quantity, 0)) As EstimatedQty, 0 As DispatchId, 0 As DispatchDetailId, Convert(Decimal(18, " & DecimalPointInQty & "), ((Convert(Float, IsNull(Recv_D.Quantity, 0))+Convert(Float, IsNull(Returned.ReturnedTotalQty, 0)))-Convert(Float, IsNull(tblTrackEstimation.DispatchedQty, 0)))) As CheckQty, Convert(Decimal(18, " & DecimalPointInQty & "), " _
                '& " IsNull(tblTrackEstimation.DispatchedQty, 0)) As IssuedQty, 0 As SubItem  from PlanTicketMaterialDetail " _
                '& "left JOIN PlanTicketsMaster on " _
                '& "PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
                '& "left JOIN ArticleDefTable on " _
                '& "ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
                '& "left JOIN tblproSteps on " _
                '& "tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
                '& "left JOIN ArticleColorDefTable on " _
                '& "ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
                '& "where TicketId = " & cmbTicketNo.SelectedValue & " " _
                '& "group By PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , " _
                '& "ArticleDefTable.ArticleDescription , tblproSteps.prod_step , ArticleDefTable.ArticleCode , ArticleColorDefTable.ArticleColorName , PlanTicketMaterialDetail.CostPrice")





                '    fillGrid(" Select PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
                '      & " SUM(IsNull((PlanTicketMaterialDetail.Qty),0)) as PendingQty , ArticleDefTable.ArticleCode as Code , ArticleColorDefTable.ArticleColorName as Color , PlanTicketMaterialDetail.CostPrice as Rate , sum(IsNull((PlanTicketMaterialDetail.Qty),0))*PlanTicketMaterialDetail.CostPrice as Total " _
                '      & " FROM PlanTicketMaterialDetail " _
                '      & " INNER JOIN PlanTicketsMaster ON " _
                '      & " PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
                '      & " LEFT OUTRER JOIN ArticleDefTable ON " _
                '      & " ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
                '      & " LEFT JOIN tblproSteps ON " _
                '      & " tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
                '      & " LEFT JOIN ArticleColorDefTable on " _
                '      & " ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
                '      & " WHERE TicketId = " & cmbTicketNo.SelectedValue & " " _
                '      & "group By PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , " _
                '      & "ArticleDefTable.ArticleDescription , tblproSteps.prod_step , ArticleDefTable.ArticleCode , ArticleColorDefTable.ArticleColorName , PlanTicketMaterialDetail.CostPrice")
                'ElseIf ItemsConsumption = True Then

                '    fillGrid("SELECT PlanTicketsMaster.TicketNo , dispatchDetailTable.ArticleDefId as ArticleId , dispatchDetailTable.SubDepartmentID as DepartmentID , dispatchMasterTable.PlanTicketId as TicketId " _
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


            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Public Function RowHasWIPAccount(ByVal Row As DataRow) As Boolean
        Try
            If Row.Item("WIPAccountId") > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS2668 done 
    ''' </summary>
    ''' <param name="_Row"></param>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Public Sub SetWIPAccount(ByVal _Row As DataRow, ByVal dt As DataTable)
        Try
            If RowHasWIPAccount(_Row) = True Then
                If IsWIPAccount = False Then
                    IsWIPAccount = True
                End If
                'dt.Rows.Remove(_Row)
                'dt.AcceptChanges()
                'dtMerging.Rows.Add(_Row)
                Dim dr() As DataRow = dt.Select(" ParentTicketNo ='" & _Row.Item("TicketNo").ToString & "'")
                If dr.Length > 0 Then
                    For Each Row As DataRow In dr
                        If Val(Row.Item("WIPAccountId").ToString) < 1 Then
                            Row.BeginEdit()
                            Row.Item("WIPAccountId") = _Row.Item("WIPAccountId")
                            Row.EndEdit()
                            SetWIPAccount(Row, dt)
                        End If
                    Next
                End If
                'Else
                'dt.Rows.Remove(_Row)
                'dtMerging.Rows.Add(_Row)
            End If
        Catch ex As Exception
            Throw ex
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
        Try
            If StoreIssue = True Then
                dt.Columns.Add("LocationID", System.Type.GetType("System.Int32"))
                dt.Columns.Add("ArticleCode", System.Type.GetType("System.String"))
                dt.Columns.Add("item", System.Type.GetType("System.String"))
                dt.Columns.Add("Color", System.Type.GetType("System.String"))
                dt.Columns.Add("BatchNo", System.Type.GetType("System.String"))
                dt.Columns.Add("Unit", System.Type.GetType("System.String"))
                dt.Columns.Add("Qty", System.Type.GetType("System.Double"))
                dt.Columns.Add("Rate", System.Type.GetType("System.Double"))
                dt.Columns.Add("Total", System.Type.GetType("System.Double"))
                dt.Columns.Add("CategoryId", System.Type.GetType("System.Int32"))
                dt.Columns.Add("ItemId", System.Type.GetType("System.Int32"))
                dt.Columns.Add("PackQty", System.Type.GetType("System.Double"))
                dt.Columns.Add("CurrentPrice", System.Type.GetType("System.Double"))
                dt.Columns.Add("BatchID", System.Type.GetType("System.Int32"))
                dt.Columns.Add("ArticleDefMasterId", System.Type.GetType("System.Int32"))
                dt.Columns.Add("ArticleDescriptionMaster", System.Type.GetType("System.String"))
                dt.Columns.Add("Pack_Desc", System.Type.GetType("System.String"))
                dt.Columns.Add("PurchaseAccountId", System.Type.GetType("System.Int32"))
                dt.Columns.Add("CGSAccountId", System.Type.GetType("System.Int32"))
                dt.Columns.Add("CostPrice", System.Type.GetType("System.Double"))
                dt.Columns.Add("PlanUnit", System.Type.GetType("System.String"))
                dt.Columns.Add("PlanNo", System.Type.GetType("System.String"))
                dt.Columns.Add("PlanQty", System.Type.GetType("System.Double"))
                dt.Columns.Add("TicketNo", System.Type.GetType("System.String"))
                dt.Columns.Add("TicketQty", System.Type.GetType("System.Double"))
                dt.Columns.Add("LotNo", System.Type.GetType("System.String"))
                dt.Columns.Add("Rack_No", System.Type.GetType("System.String"))
                dt.Columns.Add("Comments", System.Type.GetType("System.String"))
                dt.Columns.Add("TotalQty", System.Type.GetType("System.Double"))
                dt.Columns.Add("DispatchDetailId", System.Type.GetType("System.Int32"))
                dt.Columns.Add("CheckQty", System.Type.GetType("System.Double"))
                dt.Columns.Add("EstimationId", System.Type.GetType("System.Int32"))
                dt.Columns.Add("TicketId", System.Type.GetType("System.Int32"))
                dt.Columns.Add("DepartmentId", System.Type.GetType("System.Int32"))
                dt.Columns.Add("Department", System.Type.GetType("System.String"))
                dt.Columns.Add("WIPAccountId", System.Type.GetType("System.Int32"))
                dt.Columns.Add("CostCenterId", System.Type.GetType("System.Int32"))
                'dt.Columns.Add("PlanId", System.Type.GetType("System.Int32"))
                'dt.Columns.Add("Stock", System.Type.GetType("System.Double"))
                'dt.Columns.Add("PackPrice", System.Type.GetType("System.Double"))       
                'dt.Columns.Add("AllocationDetailId", System.Type.GetType("System.Int32"))
                'dt.Columns.Add("ParentId", System.Type.GetType("System.Int32"))
                'dt.Columns.Add("EstimatedQty", System.Type.GetType("System.Double"))
                'dt.Columns.Add("DispatchId", System.Type.GetType("System.Int32"))
                'dt.Columns.Add("IssuedQty", System.Type.GetType("System.Double"))
                'dt.Columns.Add("SubItem", GetType(Boolean))
                'dt.Columns.Add("WIPAccountId", System.Type.GetType("System.Int32"))

            ElseIf ItemsConsumption = True Then

                dt1.Columns.Add("LocationId")
                dt1.Columns.Add("Location")
                dt1.Columns.Add("ConsumptionDetailId")
                dt1.Columns.Add("ConsumptionId")
                dt1.Columns.Add("ArticleId")
                dt1.Columns.Add("ArticleCode")
                dt1.Columns.Add("ArticleDescription")
                dt1.Columns.Add("Color")
                dt1.Columns.Add("Qty")
                dt1.Columns.Add("ConsumedQty")
                dt1.Columns.Add("AvailableQty")
                dt1.Columns.Add("Rate")
                dt1.Columns.Add("Total")
                dt1.Columns.Add("DispatchId")
                dt1.Columns.Add("DispatchDetailId")
                dt1.Columns.Add("CGAccountId")
                dt1.Columns.Add("Comments")
                dt1.Columns.Add("CheckQty")
                dt1.Columns.Add("DepartmentId")
                dt1.Columns.Add("TotalIssuedQty")
                dt1.Columns.Add("TotalConsumedQty")
                dt1.Columns.Add("TotalReturnedQty")
            End If
            For Each row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
                Dim R As DataRow = dt.NewRow
                Dim R1 As DataRow = dt1.NewRow
                If StoreIssue = True Then
                    R("LocationID") = Val(row.Cells("LocationID").Value.ToString)
                    R("ArticleCode") = row.Cells("ArticleCode").Value.ToString
                    R("Item") = row.Cells("Item").Value.ToString
                    R("Color") = row.Cells("Color").Value.ToString
                    R("BatchNo") = row.Cells("BatchNo").Value.ToString
                    R("Unit") = row.Cells("Unit").Value.ToString
                    R("Qty") = Val(row.Cells("Qty").Value.ToString)
                    R("Rate") = Val(row.Cells("Rate").Value.ToString)
                    R("Total") = Val(row.Cells("Total").Value.ToString)
                    R("CategoryId") = Val(row.Cells("CategoryId").Value.ToString)
                    R("ItemId") = Val(row.Cells("ItemId").Value.ToString)
                    R("PackQty") = Val(row.Cells("PackQty").Value.ToString)
                    R("CurrentPrice") = Val(row.Cells("CurrentPrice").Value.ToString)
                    R("BatchId") = Val(row.Cells("BatchId").Value.ToString)
                    R("ArticleDefMasterId") = Val(row.Cells("ArticleDefMasterId").Value.ToString)
                    R("ArticleDescriptionMaster") = row.Cells("ArticleDescriptionMaster").Value.ToString
                    R("Pack_Desc") = row.Cells("Pack_Desc").Value.ToString
                    R("PurchaseAccountId") = Val(row.Cells("PurchaseAccountId").Value.ToString)
                    R("CGSAccountId") = Val(row.Cells("CGSAccountId").Value.ToString)
                    R("CostPrice") = Val(row.Cells("CostPrice").Value.ToString)
                    R("PlanUnit") = row.Cells("PlanUnit").Value.ToString
                    R("PlanNo") = row.Cells("PlanNo").Value.ToString
                    R("PlanQty") = Val(row.Cells("PlanQty").Value.ToString)
                    R("TicketNo") = row.Cells("TicketNo").Value.ToString
                    R("TicketQty") = Val(row.Cells("TicketQty").Value.ToString)
                    R("LotNo") = row.Cells("LotNo").Value.ToString
                    R("Rack_No") = row.Cells("Rack_No").Value.ToString
                    R("Comments") = ""
                    R("TotalQty") = Val(row.Cells("TotalQty").Value.ToString)
                    R("DispatchDetailId") = Val(row.Cells("DispatchDetailId").Value.ToString)
                    R("CheckQty") = Val(row.Cells("CheckQty").Value.ToString)
                    R("EstimationId") = Val(row.Cells("EstimationId").Value.ToString)
                    R("TicketId") = Val(row.Cells("TicketId").Value.ToString)
                    R("DepartmentId") = Val(row.Cells("DepartmentId").Value.ToString)
                    R("Department") = row.Cells("Department").Value
                    R("WIPAccountId") = row.Cells("WIPAccountId").Value

                    If Val(row.Cells("CostCenterId").Value.ToString) > 0 Then
                        CostCenterId = Val(row.Cells("CostCenterId").Value.ToString)
                    End If
                    'R("PlanId") = Val(row.Cells("PlanId").Value.ToString)
                    'R("PackPrice") = 0
                    'R("Stock") = 0
                    'R("AllocationDetailId") = 0
                    'R("ParentId") = 0
                    'R("EstimatedQty") = Val(row.Cells("EstimatedQty").Value.ToString)
                    'R("DispatchId") = 0
                    'R("IssuedQty") = Val(row.Cells("IssuedQty").Value.ToString)
                    'R("SubItem") = 0
                    'R("SubDepartmentID") = Val(row.Cells("DepartmentId").Value.ToString)
                    'R("SubDepartment") = row.Cells("Stage").Value
                    'R("WIPAccountId") = Val(row.Cells("WIPAccountId").Value.ToString)

                    dt.Rows.Add(R)


                    'msg_Confirm(row.Cells("ProductName").Value & "---" & Val(row.Cells("PendingQty").Value.ToString))

                ElseIf ItemsConsumption = True Then
                    R1("LocationId") = 1
                    R1("Location") = "office"
                    R1("ConsumptionDetailId") = 1
                    R1("ConsumptionId") = 1
                    R1("ArticleId") = Val(row.Cells("ArticleId").Value.ToString)
                    R1("ArticleCode") = row.Cells("Code").Value
                    R1("ArticleDescription") = row.Cells("ProductName").Value
                    R1("Color") = row.Cells("Color").Value
                    R1("Qty") = Val(row.Cells("PendingQty").Value.ToString)
                    R1("ConsumedQty") = 0
                    R1("AvailableQty") = 0
                    R1("Rate") = Val(row.Cells("Rate").Value.ToString)
                    R1("Total") = Val(row.Cells("Total").Value.ToString)
                    R1("DispatchId") = 0
                    R1("DispatchDetailId") = 0
                    R1("CGAccountId") = 0
                    R1("Comments") = String.Empty
                    R1("CheckQty") = 0
                    R1("DepartmentId") = Val(row.Cells("DepartmentId").Value.ToString)
                    R1("DepartmentId") = 0
                    R1("TotalIssuedQty") = 0
                    R1("TotalConsumedQty") = 0
                    R1("TotalReturnedQty") = Val(row.Cells("TotalReturnedQty").Value.ToString)

                    dt1.Rows.Add(R1)

                End If

            Next

        Catch ex As Exception
            Throw ex
        End Try
        'For Each Row As DataRow In dt.Rows

        '    msg_Confirm(Row.Item("TicketNo").ToString & "---" & Row.Item("ProductName").ToString & Row.Item("Stage").ToString & "---" & "---" & Val(Row.Item("PendingQty").ToString) & "---" & Val(Row.Item("ArticleId").ToString) & "---" & Val(Row.Item("DepartmentId").ToString) & "---" & Val(Row.Item("TicketId").ToString))

        'Next


        Try

            If ItemsConsumption = True Then

                'If cmbTicketNo.SelectedValue > 0 Then
                frmItemsConsumption.fillItemsConsumptionGrid(dt1, 0, 0) 'cmbTicketNo.SelectedValue, CType(cmbTicketNo.SelectedItem, DataRowView).Item("PlanID")

                Me.Close()

                'Else
                '    ShowErrorMessage("Ticket Number must be selected")

                'End If

            ElseIf StoreIssue = True Then
                'If cmbTicketNo.SelectedValue > 0 Then
                frmReturnStoreIssuence.fillReturnStoreIssuenceGrid(dt, cmbPlanNo.SelectedValue, cmbTicketNo.SelectedValue, cmbStage.SelectedValue, IsWIPAccount, CostCenterId)
                'frmStoreIssuenceNew.IsEstimation = True
                'IsWIPAccount = False
                Me.Close()
                'Else
                '    ShowErrorMessage("Ticket Number must be selected")

                'End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Function GetTicketRecord(ByVal TicketId As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            fillGrid(" Select PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
              & " SUM(IsNull((PlanTicketMaterialDetail.Qty),0)) as PendingQty , ArticleDefTable.ArticleCode as Code , ArticleColorDefTable.ArticleColorName as Color , PlanTicketMaterialDetail.CostPrice as Rate , sum(IsNull((PlanTicketMaterialDetail.Qty),0))*PlanTicketMaterialDetail.CostPrice as Total " _
              & " FROM PlanTicketMaterialDetail " _
              & " INNER JOIN PlanTicketsMaster ON " _
              & " PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
              & " LEFT OUTRER JOIN ArticleDefTable ON " _
              & " ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
              & " LEFT JOIN tblproSteps ON " _
              & " tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
              & " LEFT JOIN ArticleColorDefTable on " _
              & " ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId " _
              & " WHERE TicketId = " & cmbTicketNo.SelectedValue & " " _
              & "group By PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleId , PlanTicketMaterialDetail.DepartmentId , PlanTicketMaterialDetail.TicketId , " _
              & "ArticleDefTable.ArticleDescription , tblproSteps.prod_step , ArticleDefTable.ArticleCode , ArticleColorDefTable.ArticleColorName , PlanTicketMaterialDetail.CostPrice")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Public Function RowHasWIPAccount(ByVal Row As DataRow) As Boolean
    '    Try
    '        If Row.Item("WIPAccountId") > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Public Sub SetWIPAccount(ByVal _Row As DataRow)
    '    Try
    '        If RowHasWIPAccount(_Row) = True Then

    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click

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