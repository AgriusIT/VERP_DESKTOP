Imports SBModel
Imports SBDal
Imports System.Data.SqlClient

Public Class frmTicketDecompositionSearch
    Implements IGeneral

    Public StoreIssue As Boolean
    Public ItemsConsumption As Boolean
    Public IsDecomposition As Boolean

    Public LocationId As Integer = 0
    Public Location1 As String = String.Empty

    Sub New(ByVal IsDecomposition As Boolean, ByVal LocationId As Integer, ByVal Location As String)

        InitializeComponent()
        Me.IsDecomposition = IsDecomposition
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
            FillDropDown(cmbTicketNo, " SELECT PlanTicketsMasterID , TicketNo , PlanID from PlanTicketsMaster ORDER BY TicketDate DESC")
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

        Try
            If cmbPlanNo.SelectedValue <= 0 Then
                FillDropDown(cmbTicketNo, "select PlanTicketsMasterID , TicketNo , PlanID from PlanTicketsMaster")

            Else
                FillDropDown(cmbTicketNo, "select PlanTicketsMasterID , TicketNo , PlanID from PlanTicketsMaster where planid = " & cmbPlanNo.SelectedValue)

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbTicketNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTicketNo.SelectedIndexChanged

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

            If IsDecomposition = True Then
                Dim dt As New DataTable
                dt = PlanTicketsStandardDAL.GetTicketRecordForDecomposition(Me.cmbSalesOrder.SelectedValue, Me.cmbPlanNo.SelectedValue, Me.cmbTicketNo.SelectedValue, Me.cmbStage.SelectedValue, DecimalPointInQty)
                Me.grdItems.DataSource = dt
                Me.grdItems.RootTable.Columns("PendingQty").FormatString = "N" & DecimalPointInQty
                Me.grdItems.RootTable.Columns("Total").FormatString = "N" & DecimalPointInValue
                Me.grdItems.RootTable.Columns("DecomposedQty").FormatString = "N" & DecimalPointInQty
                Me.grdItems.RootTable.Columns("ScrappedQty").FormatString = "N" & DecimalPointInQty
                Me.grdItems.RootTable.Columns("DecomposableQty").FormatString = "N" & DecimalPointInQty
                Me.grdItems.RootTable.Columns("TotalConsumedQty").FormatString = "N" & DecimalPointInQty
                Me.grdItems.RootTable.Columns("TempDecQty").FormatString = "N" & DecimalPointInQty
                Me.grdItems.RootTable.Columns("TempWasQty").FormatString = "N" & DecimalPointInQty
                Me.grdItems.RootTable.Columns("TempScrQty").FormatString = "N" & DecimalPointInQty
                Me.grdItems.RootTable.Columns("TempScrQty").FormatString = "N" & DecimalPointInQty
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

        Dim dt As DataTable = CreateTableColumns()
        For Each row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
            Dim R As DataRow = dt.NewRow
            R("DecompositionDetailId") = 0
            R("DecompositionId") = 0
            R("EstimationDetailId") = 0
            R("PlanItemId") = Val(row.Cells("MasterArticleId").Value.ToString)
            R("PlanItem") = row.Cells("MasterArticle").Value.ToString
            R("ProductId") = Val(row.Cells("ArticleId").Value.ToString)
            R("ParentId") = 0
            R("Product") = row.Cells("ProductName").Value.ToString
            R("ArticleUnitId") = Val(row.Cells("ArticleUnitId").Value.ToString)
            R("Unit") = row.Cells("Unit").Value.ToString
            R("Price") = Val(row.Cells("Rate").Value.ToString)
            R("Qty") = Val(row.Cells("PendingQty").Value.ToString)
            R("DecomposedQty") = Val(row.Cells("DecomposedQty").Value.ToString)
            R("WastedQty") = Val(row.Cells("DecomposedQty").Value.ToString)
            R("ScrappedQty") = Val(row.Cells("ScrappedQty").Value.ToString)
            R("DecomposableQty") = Val(row.Cells("DecomposableQty").Value.ToString)
            R("Tag") = 0
            R("ParentTag") = 0
            R("DepartmentId") = Val(row.Cells("DepartmentId").Value.ToString)
            R("Department") = row.Cells("Department").Value.ToString
            R("LocationId") = LocationId
            R("Action") = ""
            R("UniqueId") = 0
            R("ParentUniqueId") = 0
            R("TicketId") = Val(row.Cells("TicketId").Value.ToString)
            'dt.Columns.Add("TotalConsumedQty")
            'dt.Columns.Add("TempDecQty")
            'dt.Columns.Add("TempWasQty")
            'dt.Columns.Add("TempScrQty")
            'dt.Columns.Add("IsTopParent")
            'dt.Columns.Add("StockImpact")
            'dt.Columns.Add("ConsumedChildCount")
            'dt.Columns.Add("SubSubId")
            'dt.Columns.Add("PlanItemSubSubId")
            'dt.Columns.Add("DValue")
            'dt.Columns.Add("WValue")
            'dt.Columns.Add("SValue")


            dt.Rows.Add(R)
        Next
        Try
            If dt.Rows.Count > 0 Then
                'If cmbTicketNo.SelectedValue > 0 Then
                frmMaterialDecomposition.fillDecompositionGrid(dt)
                Me.Close()
            End If
            'Else
            'ShowErrorMessage("Ticket Number must be selected")
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Function CreateTableColumns() As DataTable
        Try
            'Query = " Select  0 AS DecompositionDetailId, 0 AS DecompositionId, 0 AS EstimationDetailId, EstimationDetail.PlanItemId, PlanArticle.ArticleDescription As PlanItem, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription As Product, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(EstimationDetail.Price, 0) AS Price, " _
            '                           & "  Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) AS Qty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS DecomposedQty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS WastedQty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS ScrappedQty, Sum((Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End -(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))+ Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))+Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))))) AS DecomposableQty, IsNull(EstimationDetail.Tag#, 0) AS Tag, IsNull(EstimationDetail.ParentTag#, 0) AS ParentTag, EstimationDetail.SubDepartmentId AS DepartmentId, tblProSteps.prod_step As Department, " & LocationId & " AS LocationId, Decomposition.Action, 0 As UniqueId, 0 As ParentUniqueId, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) AS TotalConsumedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))) AS TempDecQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))) AS TempWasQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))) AS TempScrQty, 0 As IsTopParent, 0 AS StockImpact, Sum(IsNull(DecCount.ChildCount, 0)) AS ConsumedChildCount, IsNull(ArticleAccount.SubSubId, 0) AS SubSubId, IsNull(PlanItemAccount.SubSubId, 0) AS PlanItemSubSubId, 0 AS DValue, 0 AS WValue, 0 AS SValue " _
            '                           & " FROM MaterialEstimationDetailTable As EstimationDetail INNER JOIN MaterialEstimation AS Estimation ON EstimationDetail.MaterialEstMasterID = Estimation.Id INNER JOIN ArticleDefTable AS Article ON EstimationDetail.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleDefTable AS PlanArticle ON EstimationDetail.PlanItemId = PlanArticle.ArticleId " _
            '                           & " INNER JOIN (SELECT IsNull(Production.PlanId, 0) As PlanId, IsNull(Production.PlanTicketId, 0) As TicketId FROM ProductionMasterTable AS Production INNER JOIN ProductionDetailTable AS ProductionDetail ON Production.Production_ID = ProductionDetail.Production_ID) AS Production ON Estimation.MasterPlanId = Production.PlanId AND Estimation.PlanTicketId = Production.TicketId " _
            '                           & " LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN tblProSteps ON EstimationDetail.SubDepartmentID = tblProSteps.ProdStep_Id " _
            '                           & " LEFT OUTER JOIN ArticleGroupDefTable AS ArticleAccount ON Article.ArticleGroupId = ArticleAccount.ArticleGroupId " _
            '                           & " LEFT OUTER JOIN ArticleGroupDefTable AS PlanItemAccount ON PlanArticle.ArticleGroupId = PlanItemAccount.ArticleGroupId " _
            '                           & " LEFT OUTER JOIN (SELECT Sum(IsNull(DecompositionDetail.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(DecompositionDetail.WastedQty, 0)) AS WastedQty, Sum(IsNull(DecompositionDetail.ScrappedQty, 0)) AS ScrappedQty, IsNull(DecompositionDetail.ProductId, 0) AS ProductId, IsNull(DecompositionDetail.ParentId, 0) AS ParentId, IsNull(DecompositionDetail.PlanItemId, 0) AS PlanItemId, DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0) AS EstimationId, IsNull(Decomposition.TicketId, 0) AS TicketId, IsNull(Decomposition.PlanId, 0) AS PlanId, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, DecompositionDetail.Action AS Action, IsNull(DecompositionDetail.DepartmentId, 0) AS DepartmentId, Sum(IsNull(DecompositionDetail.TotalConsumedQty, 0)) AS TotalConsumedQty FROM MaterialDecompositionMaster AS Decomposition INNER JOIN MaterialDecompositionDetail AS DecompositionDetail ON Decomposition.DecompositionId = DecompositionDetail.DecompositionId Group By IsNull(DecompositionDetail.ProductId, 0), IsNull(DecompositionDetail.ParentId, 0), IsNull(DecompositionDetail.PlanItemId, 0), DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0), IsNull(Decomposition.TicketId, 0), IsNull(Decomposition.PlanId, 0), IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.Action, DecompositionDetail.DepartmentId) AS Decomposition " _
            '                           & " ON EstimationDetail.ProductId = Decomposition.ProductId AND EstimationDetail.ParentId = Decomposition.ParentId AND EstimationDetail.SubDepartmentId = Decomposition.DepartmentId AND EstimationDetail.PlanItemId = Decomposition.PlanItemId AND EstimationDetail.Tag# = Decomposition.Tag# AND EstimationDetail.ParentTag# = Decomposition.ParentTag# AND Estimation.Id = Decomposition.EstimationId AND Estimation.PlanTicketId = Decomposition.TicketId AND Estimation.MasterPlanId = Decomposition.PlanId " _
            '                           & " LEFT OUTER JOIN (SELECT Count(ParentTag#) As ChildCount, ParentTag#, IsNull(EstimationId, 0) AS EstimationId From MaterialDecompositionDetail AS Detail INNER JOIN MaterialDecompositionMaster AS Decomposition ON Detail.DecompositionId = Decomposition.DecompositionId Where IsNull(Detail.StockImpact, 0) = 1 Group By EstimationId, ParentTag#) AS DecCount ON EstimationDetail.Tag# = DecCount.ParentTag# AND EstimationDetail.MaterialEstMasterID = DecCount.EstimationId " _
            '                           & " WHERE Estimation.Id = " & EstimationId & " AND Estimation.PlanTicketId = " & TicketId & " AND EstimationDetail.ProductId = " & ProductId & " Group By EstimationDetail.PlanItemId, PlanArticle.ArticleDescription, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(EstimationDetail.Price, 0), IsNull(EstimationDetail.Tag#, 0), IsNull(EstimationDetail.ParentTag#, 0), EstimationDetail.SubDepartmentId, tblProSteps.prod_step, IsNull(Decomposition.LocationId, 0), Decomposition.Action, IsNull(ArticleAccount.SubSubId, 0), IsNull(PlanItemAccount.SubSubId, 0) Having Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) > Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) "



            '                           & "    , 0 AS , 0 AS , 0 AS  " _
            '                           & " FROM MaterialEstimationDetailTable As EstimationDetail INNER JOIN MaterialEstimation AS Estimation ON EstimationDetail.MaterialEstMasterID = Estimation.Id INNER JOIN ArticleDefTable AS Article ON EstimationDetail.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleDefTable AS PlanArticle ON EstimationDetail.PlanItemId = PlanArticle.ArticleId " _
            '                           & " INNER JOIN (SELECT IsNull(Production.PlanId, 0) As PlanId, IsNull(Production.PlanTicketId, 0) As TicketId FROM ProductionMasterTable AS Production INNER JOIN ProductionDetailTable AS ProductionDetail ON Production.Production_ID = ProductionDetail.Production_ID) AS Production ON Estimation.MasterPlanId = Production.PlanId AND Estimation.PlanTicketId = Production.TicketId " _
            '                           & " LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN tblProSteps ON EstimationDetail.SubDepartmentID = tblProSteps.ProdStep_Id " _
            '                           & " LEFT OUTER JOIN ArticleGroupDefTable AS ArticleAccount ON Article.ArticleGroupId = ArticleAccount.ArticleGroupId " _
            '                           & " LEFT OUTER JOIN ArticleGroupDefTable AS PlanItemAccount ON PlanArticle.ArticleGroupId = PlanItemAccount.ArticleGroupId " _
            '                           & " LEFT OUTER JOIN (SELECT Sum(IsNull(DecompositionDetail.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(DecompositionDetail.WastedQty, 0)) AS WastedQty, Sum(IsNull(DecompositionDetail.ScrappedQty, 0)) AS ScrappedQty, IsNull(DecompositionDetail.ProductId, 0) AS ProductId, IsNull(DecompositionDetail.ParentId, 0) AS ParentId, IsNull(DecompositionDetail.PlanItemId, 0) AS PlanItemId, DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0) AS EstimationId, IsNull(Decomposition.TicketId, 0) AS TicketId, IsNull(Decomposition.PlanId, 0) AS PlanId, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, DecompositionDetail.Action AS Action, IsNull(DecompositionDetail.DepartmentId, 0) AS DepartmentId, Sum(IsNull(DecompositionDetail.TotalConsumedQty, 0)) AS TotalConsumedQty FROM MaterialDecompositionMaster AS Decomposition INNER JOIN MaterialDecompositionDetail AS DecompositionDetail ON Decomposition.DecompositionId = DecompositionDetail.DecompositionId Group By IsNull(DecompositionDetail.ProductId, 0), IsNull(DecompositionDetail.ParentId, 0), IsNull(DecompositionDetail.PlanItemId, 0), DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0), IsNull(Decomposition.TicketId, 0), IsNull(Decomposition.PlanId, 0), IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.Action, DecompositionDetail.DepartmentId) AS Decomposition " _
            '                           & " ON EstimationDetail.ProductId = Decomposition.ProductId AND EstimationDetail.ParentId = Decomposition.ParentId AND EstimationDetail.SubDepartmentId = Decomposition.DepartmentId AND EstimationDetail.PlanItemId = Decomposition.PlanItemId AND EstimationDetail.Tag# = Decomposition.Tag# AND EstimationDetail.ParentTag# = Decomposition.ParentTag# AND Estimation.Id = Decomposition.EstimationId AND Estimation.PlanTicketId = Decomposition.TicketId AND Estimation.MasterPlanId = Decomposition.PlanId " _
            '                           & " LEFT OUTER JOIN (SELECT Count(ParentTag#) As ChildCount, ParentTag#, IsNull(EstimationId, 0) AS EstimationId From MaterialDecompositionDetail AS Detail INNER JOIN MaterialDecompositionMaster AS Decomposition ON Detail.DecompositionId = Decomposition.DecompositionId Where IsNull(Detail.StockImpact, 0) = 1 Group By EstimationId, ParentTag#) AS DecCount ON EstimationDetail.Tag# = DecCount.ParentTag# AND EstimationDetail.MaterialEstMasterID = DecCount.EstimationId " _
            '                           & " WHERE Estimation.Id = " & EstimationId & " AND Estimation.PlanTicketId = " & TicketId & " AND EstimationDetail.ProductId = " & ProductId & " Group By EstimationDetail.PlanItemId, PlanArticle.ArticleDescription, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(EstimationDetail.Price, 0), IsNull(EstimationDetail.Tag#, 0), IsNull(EstimationDetail.ParentTag#, 0), EstimationDetail.SubDepartmentId, tblProSteps.prod_step, IsNull(Decomposition.LocationId, 0), Decomposition.Action, IsNull(ArticleAccount.SubSubId, 0), IsNull(PlanItemAccount.SubSubId, 0) Having Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) > Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) "
            Dim dt As New DataTable
            dt.Columns.Add("DecompositionDetailId")
            dt.Columns.Add("DecompositionId")
            dt.Columns.Add("EstimationDetailId")
            dt.Columns.Add("PlanItemId")
            dt.Columns.Add("PlanItem")
            dt.Columns.Add("ProductId")
            dt.Columns.Add("ParentId")
            dt.Columns.Add("Product")
            dt.Columns.Add("ArticleUnitId")
            dt.Columns.Add("Unit")
            dt.Columns.Add("Price")
            dt.Columns.Add("Qty")
            dt.Columns.Add("DecomposedQty")
            dt.Columns.Add("WastedQty")
            dt.Columns.Add("ScrappedQty")
            dt.Columns.Add("DecomposableQty")
            dt.Columns.Add("Tag")
            dt.Columns.Add("ParentTag")
            dt.Columns.Add("DepartmentId")
            dt.Columns.Add("Department")
            dt.Columns.Add("LocationId")
            dt.Columns.Add("Action")
            dt.Columns.Add("UniqueId")
            dt.Columns.Add("ParentUniqueId")
            dt.Columns.Add("TotalConsumedQty")
            dt.Columns.Add("TempDecQty")
            dt.Columns.Add("TempWasQty")
            dt.Columns.Add("TempScrQty")
            dt.Columns.Add("IsTopParent")
            dt.Columns.Add("StockImpact")
            dt.Columns.Add("ConsumedChildCount")
            dt.Columns.Add("SubSubId")
            dt.Columns.Add("PlanItemSubSubId")
            dt.Columns.Add("DValue")
            dt.Columns.Add("WValue")
            dt.Columns.Add("SValue")
            dt.Columns.Add("TicketId")
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class