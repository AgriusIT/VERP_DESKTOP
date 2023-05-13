Imports SBModel
Imports SBDal
Imports System.Data.SqlClient

Public Class frmLoadCloseBatch
    Implements IGeneral

    Dim LocationId As Integer
    Dim LocationName As String

    Sub New(ByVal locationId, ByVal locationName)

        ' This call is required by the designer.
        InitializeComponent()

        Me.LocationId = locationId
        Me.LocationName = locationName

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

        Try
            If Condition = "Plan" Then

                FillDropDown(cmbPlanNo, " SELECT PlanId, PlanNo from PlanMasterTable ORDER BY PlanDate DESC")

            ElseIf Condition = "TicketAll" Then

                FillDropDown(cmbTicketNo, "select PlanTicketsMasterID , BatchNo As TicketNo, PlanID from PlanTicketsMaster where BatchNo <> '' ORDER BY PlanTicketsMasterID DESC")

            ElseIf Condition = "TicketFilter" Then

                FillDropDown(cmbTicketNo, "select PlanTicketsMasterID , BatchNo As TicketNo , PlanID from PlanTicketsMaster where BatchNo <> '' And planid = " & cmbPlanNo.SelectedValue & " ORDER BY PlanTicketsMasterID DESC")

            ElseIf Condition = "BatchAll" Then

                FillDropDown(cmbBatchNo, "select CloseBatchId , BatchNo from CloseBatch")

            ElseIf Condition = "BatchFilter" Then

                FillDropDown(cmbBatchNo, "select CloseBatchId , BatchNo from CloseBatch where TicketId = " & cmbTicketNo.SelectedValue)

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

        If Not cmbPlanNo.SelectedIndex = -1 Then
            Me.cmbPlanNo.SelectedIndex = 0
        End If

        If Not cmbTicketNo.SelectedIndex = -1 Then
            Me.cmbTicketNo.SelectedIndex = 0
        End If

        If Not cmbBatchNo.SelectedIndex = -1 Then
            Me.cmbBatchNo.SelectedIndex = 0
        End If

        grdItems.DataSource = Nothing

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

    Private Sub frmLoadCloseBatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        FillCombos("Plan")
        FillCombos("TicketAll")
        FillCombos("BatchAll")

    End Sub

    Private Sub cmbPlanNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlanNo.SelectedIndexChanged

        Try
            If cmbPlanNo.SelectedValue <= 0 Then

                FillCombos("TicketAll")
                FillCombos("BatchAll")

            Else

                FillCombos("TicketFilter")

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbTicketNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTicketNo.SelectedIndexChanged

        Try
            If cmbTicketNo.SelectedValue <= 0 Then

                FillCombos("BatchAll")

            Else

                FillCombos("BatchFilter")

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click

        If Me.cmbPlanNo.SelectedValue > 0 And Me.cmbTicketNo.SelectedValue > 0 And Me.cmbBatchNo.SelectedValue <= 0 Then

            fillGrid("Select CloseBatchFinishGoodsDetail.CloseBatchFinishGoodsDetailId, DepartmentWiseProductionDetail.PlanID , planmastertable.PlanNo , " _
                     & "DepartmentWiseProductionDetail.TicketID , planticketsmaster.BatchNo As TicketNo , CloseBatchFinishGoodsDetail.CloseBatchId , CloseBatch.BatchNo, " _
                     & "CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId, CloseBatchFinishGoodsDetail.ArticleId, Article.ArticleDescription, " _
                     & "IsNull(CloseBatchFinishGoodsDetail.Qty, 0) AS Qty , Article.PurchasePrice as Price, Article.ArticleCode as Code , ArticleColorDeftable.ArticleColorName as Color, ArticlesizeDeftable.ArticleSizeName as Size , ArticleunitDeftable.ArticleUnitName as UnitName ,  IsNull(Article.PackQty, 0) AS PackQty , Article.MasterId , IsNull(ArticleDefView.subsubId,0) as PurchaseAccountId , IsNull(articlegroupdeftable.CGSAccountId,0) as DepartmentCGSAccountId , IsNull(ProductionProcess.WIPAccountId,0) as ProcessCGSAccountId , IsNull(articledeftablemaster.CGSAccountId,0) as ItemCGSAccountId , ArticleDefView.ArticleUnitName As UOM , articledeftablemaster.ProductionProcessId , tblprosteps.prod_step as Stage , ProductionProcessDetail.SortOrder " _
                     & "FROM CloseBatchFinishGoodsDetail " _
                     & "LEFT OUTER JOIN CloseBatch ON CloseBatch.CloseBatchId = CloseBatchFinishGoodsDetail.CloseBatchId " _
                     & "LEFT OUTER JOIN ArticleDefTable AS Article ON CloseBatchFinishGoodsDetail.ArticleId = Article.ArticleId " _
                     & "LEFT OUTER JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.Id " _
                     & "LEFT OUTER JOIN tblprosteps ON DepartmentWiseProductionDetail.DepartmentId = tblprosteps.ProdStep_Id " _
                     & "LEFT OUTER JOIN articledeftablemaster ON articledeftablemaster.ArticleId = Article.MasterId " _
                     & "LEFT OUTER JOIN planticketsmaster ON planticketsmaster.PlanTicketsMasterID = DepartmentWiseProductionDetail.TicketID " _
                     & "LEFT OUTER JOIN planmastertable ON planmastertable.PlanId = DepartmentWiseProductionDetail.PlanID " _
                     & "LEFT OUTER JOIN ArticleColorDeftable ON ArticleColorDeftable.ArticleColorId = Article.ArticleColorId " _
                     & "LEFT OUTER JOIN ArticlesizeDeftable ON ArticlesizeDeftable.ArticleSizeId = Article.SizeRangeId " _
                     & "LEFT OUTER JOIN ArticleunitDeftable ON ArticleunitDeftable.ArticleUnitId = Article.ArticleUnitId " _
                     & "LEFT OUTER JOIN ArticleDefView ON ArticleDefView.articleId = Article.ArticleId " _
                     & "LEFT OUTER JOIN ProductionProcess ON ProductionProcess.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                     & "LEFT OUTER JOIN articlegroupdeftable ON articlegroupdeftable.articlegroupId = articledeftablemaster.articlegroupId " _
                     & "LEFT OUTER JOIN ProductionProcessDetail  On ProductionProcessDetail.ProductionProcessId = articledeftablemaster.ProductionProcessId and " _
                     & "ProductionProcessDetail.ProductionStepId = tblprosteps.ProdStep_Id " _
                     & "Left Outer JOIN " _
                     & "(select Article.MasterId as MasterId, Max(ProductionProcessDetail.SortOrder) as HigherOrder from CloseBatchFinishGoodsDetail " _
                     & "LEFT OUTER JOIN CloseBatch ON CloseBatch.CloseBatchId = CloseBatchFinishGoodsDetail.CloseBatchId " _
                     & "LEFT OUTER JOIN ArticleDefTable AS Article ON CloseBatchFinishGoodsDetail.ArticleId = Article.ArticleId " _
                     & "LEFT OUTER JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.Id " _
                     & "LEFT OUTER JOIN tblprosteps ON DepartmentWiseProductionDetail.DepartmentId = tblprosteps.ProdStep_Id " _
                     & "LEFT OUTER JOIN articledeftablemaster ON articledeftablemaster.ArticleId = Article.MasterId " _
                     & "LEFT OUTER JOIN planticketsmaster ON planticketsmaster.PlanTicketsMasterID = DepartmentWiseProductionDetail.TicketID " _
                     & "LEFT OUTER JOIN planmastertable ON planmastertable.PlanId = DepartmentWiseProductionDetail.PlanID " _
                     & "LEFT JOIN ProductionProcess ON ProductionProcess.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                     & "LEFT JOIN ProductionProcessDetail  On ProductionProcessDetail.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                     & "And ProductionProcessDetail.ProductionStepId = tblprosteps.ProdStep_Id " _
                     & "WHERE DepartmentWiseProductionDetail.PlanID = " & Me.cmbPlanNo.SelectedValue & " And DepartmentWiseProductionDetail.TicketID = " & Me.cmbTicketNo.SelectedValue & " " _
                     & "Group By Article.MasterId) " _
                     & "As HigherSortOrder ON Article.MasterId = HigherSortOrder.MasterId " _
                     & "WHERE DepartmentWiseProductionDetail.PlanID = " & Me.cmbPlanNo.SelectedValue & "And DepartmentWiseProductionDetail.TicketID = " & Me.cmbTicketNo.SelectedValue & "" _
                     & " and ProductionProcessDetail.SortOrder = HigherSortOrder.HigherOrder ")

        ElseIf Me.cmbPlanNo.SelectedValue <= 0 And Me.cmbTicketNo.SelectedValue > 0 And Me.cmbBatchNo.SelectedValue <= 0 Then

            fillGrid("Select CloseBatchFinishGoodsDetail.CloseBatchFinishGoodsDetailId, DepartmentWiseProductionDetail.PlanID , planmastertable.PlanNo , " _
                     & "DepartmentWiseProductionDetail.TicketID , planticketsmaster.BatchNo As TicketNo , CloseBatchFinishGoodsDetail.CloseBatchId , CloseBatch.BatchNo, " _
                     & "CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId, CloseBatchFinishGoodsDetail.ArticleId, Article.ArticleDescription, " _
                     & "IsNull(CloseBatchFinishGoodsDetail.Qty, 0) AS Qty , Article.PurchasePrice as Price, Article.ArticleCode as Code , ArticleColorDeftable.ArticleColorName as Color, ArticlesizeDeftable.ArticleSizeName as Size , ArticleunitDeftable.ArticleUnitName as UnitName , IsNull(Article.PackQty, 0) AS PackQty , Article.MasterId , IsNull(ArticleDefView.subsubId,0) as PurchaseAccountId , IsNull(articlegroupdeftable.CGSAccountId,0) as DepartmentCGSAccountId , IsNull(ProductionProcess.WIPAccountId,0) as ProcessCGSAccountId , IsNull(articledeftablemaster.CGSAccountId,0) as ItemCGSAccountId , ArticleDefView.ArticleUnitName As UOM , articledeftablemaster.ProductionProcessId , tblprosteps.prod_step as Stage , ProductionProcessDetail.SortOrder " _
                     & "FROM CloseBatchFinishGoodsDetail " _
                     & "LEFT OUTER JOIN CloseBatch ON CloseBatch.CloseBatchId = CloseBatchFinishGoodsDetail.CloseBatchId " _
                     & "LEFT OUTER JOIN ArticleDefTable AS Article ON CloseBatchFinishGoodsDetail.ArticleId = Article.ArticleId " _
                     & "LEFT OUTER JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.Id " _
                     & "LEFT OUTER JOIN tblprosteps ON DepartmentWiseProductionDetail.DepartmentId = tblprosteps.ProdStep_Id " _
                     & "LEFT OUTER JOIN articledeftablemaster ON articledeftablemaster.ArticleId = Article.MasterId " _
                     & "LEFT OUTER JOIN planticketsmaster ON planticketsmaster.PlanTicketsMasterID = DepartmentWiseProductionDetail.TicketID " _
                     & "LEFT OUTER JOIN planmastertable ON planmastertable.PlanId = DepartmentWiseProductionDetail.PlanID " _
                     & "LEFT OUTER JOIN ArticleColorDeftable ON ArticleColorDeftable.ArticleColorId = Article.ArticleColorId " _
                     & "LEFT OUTER JOIN ArticlesizeDeftable ON ArticlesizeDeftable.ArticleSizeId = Article.SizeRangeId " _
                     & "LEFT OUTER JOIN ArticleunitDeftable ON ArticleunitDeftable.ArticleUnitId = Article.ArticleUnitId " _
                     & "LEFT OUTER JOIN ArticleDefView ON ArticleDefView.articleId = Article.ArticleId " _
                     & "LEFT OUTER JOIN ProductionProcess ON ProductionProcess.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                     & "LEFT OUTER JOIN articlegroupdeftable ON articlegroupdeftable.articlegroupId = articledeftablemaster.articlegroupId " _
                     & "LEFT OUTER JOIN ProductionProcessDetail  On ProductionProcessDetail.ProductionProcessId = articledeftablemaster.ProductionProcessId and " _
                     & "ProductionProcessDetail.ProductionStepId = tblprosteps.ProdStep_Id " _
                     & "Left Outer JOIN " _
                     & "(select Article.MasterId as MasterId, Max(ProductionProcessDetail.SortOrder) as HigherOrder from CloseBatchFinishGoodsDetail " _
                     & "LEFT OUTER JOIN CloseBatch ON CloseBatch.CloseBatchId = CloseBatchFinishGoodsDetail.CloseBatchId " _
                     & "LEFT OUTER JOIN ArticleDefTable AS Article ON CloseBatchFinishGoodsDetail.ArticleId = Article.ArticleId " _
                     & "LEFT OUTER JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.Id " _
                     & "LEFT OUTER JOIN tblprosteps ON DepartmentWiseProductionDetail.DepartmentId = tblprosteps.ProdStep_Id " _
                     & "LEFT OUTER JOIN articledeftablemaster ON articledeftablemaster.ArticleId = Article.MasterId " _
                     & "LEFT OUTER JOIN planticketsmaster ON planticketsmaster.PlanTicketsMasterID = DepartmentWiseProductionDetail.TicketID " _
                     & "LEFT OUTER JOIN planmastertable ON planmastertable.PlanId = DepartmentWiseProductionDetail.PlanID " _
                     & "LEFT JOIN ProductionProcess ON ProductionProcess.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                     & "LEFT JOIN ProductionProcessDetail  On ProductionProcessDetail.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                     & "And ProductionProcessDetail.ProductionStepId = tblprosteps.ProdStep_Id " _
                     & "WHERE DepartmentWiseProductionDetail.TicketID = " & Me.cmbTicketNo.SelectedValue & " " _
                     & "Group By Article.MasterId) " _
                     & "As HigherSortOrder ON Article.MasterId = HigherSortOrder.MasterId " _
                     & "where DepartmentWiseProductionDetail.TicketID = " & Me.cmbTicketNo.SelectedValue & "" _
                     & " and ProductionProcessDetail.SortOrder = HigherSortOrder.HigherOrder ")

        ElseIf Me.cmbPlanNo.SelectedValue <= 0 And Me.cmbTicketNo.SelectedValue > 0 And Me.cmbBatchNo.SelectedValue > 0 Then

            fillGrid("Select CloseBatchFinishGoodsDetail.CloseBatchFinishGoodsDetailId, DepartmentWiseProductionDetail.PlanID , planmastertable.PlanNo , " _
                    & "DepartmentWiseProductionDetail.TicketID , planticketsmaster.BatchNo As TicketNo , CloseBatchFinishGoodsDetail.CloseBatchId , CloseBatch.BatchNo, " _
                    & "CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId, CloseBatchFinishGoodsDetail.ArticleId, Article.ArticleDescription, " _
                    & "IsNull(CloseBatchFinishGoodsDetail.Qty, 0) AS Qty , Article.PurchasePrice as Price, Article.ArticleCode as Code , ArticleColorDeftable.ArticleColorName as Color, ArticlesizeDeftable.ArticleSizeName as Size , ArticleunitDeftable.ArticleUnitName as UnitName , IsNull(Article.PackQty, 0) AS PackQty , Article.MasterId , IsNull(ArticleDefView.subsubId,0) as PurchaseAccountId , IsNull(articlegroupdeftable.CGSAccountId,0) as DepartmentCGSAccountId , IsNull(ProductionProcess.WIPAccountId,0) as ProcessCGSAccountId , IsNull(articledeftablemaster.CGSAccountId,0) as ItemCGSAccountId , ArticleDefView.ArticleUnitName As UOM , articledeftablemaster.ProductionProcessId , tblprosteps.prod_step as Stage , ProductionProcessDetail.SortOrder " _
                    & "FROM CloseBatchFinishGoodsDetail " _
                    & "LEFT OUTER JOIN CloseBatch ON CloseBatch.CloseBatchId = CloseBatchFinishGoodsDetail.CloseBatchId " _
                    & "LEFT OUTER JOIN ArticleDefTable AS Article ON CloseBatchFinishGoodsDetail.ArticleId = Article.ArticleId " _
                    & "LEFT OUTER JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.Id " _
                    & "LEFT OUTER JOIN tblprosteps ON DepartmentWiseProductionDetail.DepartmentId = tblprosteps.ProdStep_Id " _
                    & "LEFT OUTER JOIN articledeftablemaster ON articledeftablemaster.ArticleId = Article.MasterId " _
                    & "LEFT OUTER JOIN planticketsmaster ON planticketsmaster.PlanTicketsMasterID = DepartmentWiseProductionDetail.TicketID " _
                    & "LEFT OUTER JOIN planmastertable ON planmastertable.PlanId = DepartmentWiseProductionDetail.PlanID " _
                    & "LEFT OUTER JOIN ArticleColorDeftable ON ArticleColorDeftable.ArticleColorId = Article.ArticleColorId " _
                    & "LEFT OUTER JOIN ArticlesizeDeftable ON ArticlesizeDeftable.ArticleSizeId = Article.SizeRangeId " _
                    & "LEFT OUTER JOIN ArticleunitDeftable ON ArticleunitDeftable.ArticleUnitId = Article.ArticleUnitId " _
                    & "LEFT OUTER JOIN ArticleDefView ON ArticleDefView.articleId = Article.ArticleId " _
                    & "LEFT OUTER JOIN ProductionProcess ON ProductionProcess.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                    & "LEFT OUTER JOIN articlegroupdeftable ON articlegroupdeftable.articlegroupId = articledeftablemaster.articlegroupId " _
                    & "LEFT OUTER JOIN ProductionProcessDetail  On ProductionProcessDetail.ProductionProcessId = articledeftablemaster.ProductionProcessId and " _
                    & "ProductionProcessDetail.ProductionStepId = tblprosteps.ProdStep_Id " _
                    & "Left Outer JOIN " _
                    & "(select Article.MasterId as MasterId, Max(ProductionProcessDetail.SortOrder) as HigherOrder from CloseBatchFinishGoodsDetail " _
                    & "LEFT OUTER JOIN CloseBatch ON CloseBatch.CloseBatchId = CloseBatchFinishGoodsDetail.CloseBatchId " _
                    & "LEFT OUTER JOIN ArticleDefTable AS Article ON CloseBatchFinishGoodsDetail.ArticleId = Article.ArticleId " _
                    & "LEFT OUTER JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.Id " _
                    & "LEFT OUTER JOIN tblprosteps ON DepartmentWiseProductionDetail.DepartmentId = tblprosteps.ProdStep_Id " _
                    & "LEFT OUTER JOIN articledeftablemaster ON articledeftablemaster.ArticleId = Article.MasterId " _
                    & "LEFT OUTER JOIN planticketsmaster ON planticketsmaster.PlanTicketsMasterID = DepartmentWiseProductionDetail.TicketID " _
                    & "LEFT OUTER JOIN planmastertable ON planmastertable.PlanId = DepartmentWiseProductionDetail.PlanID " _
                    & "LEFT JOIN ProductionProcess ON ProductionProcess.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                    & "LEFT JOIN ProductionProcessDetail  On ProductionProcessDetail.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                    & "And ProductionProcessDetail.ProductionStepId = tblprosteps.ProdStep_Id " _
                    & "WHERE DepartmentWiseProductionDetail.TicketID = " & Me.cmbTicketNo.SelectedValue & " And CloseBatchFinishGoodsDetail.CloseBatchId = " & Me.cmbBatchNo.SelectedValue & " " _
                    & "Group By Article.MasterId) " _
                    & "As HigherSortOrder ON Article.MasterId = HigherSortOrder.MasterId " _
                    & "WHERE DepartmentWiseProductionDetail.TicketID = " & Me.cmbTicketNo.SelectedValue & " and CloseBatchFinishGoodsDetail.CloseBatchId = " & Me.cmbBatchNo.SelectedValue & "" _
                    & " and ProductionProcessDetail.SortOrder = HigherSortOrder.HigherOrder ")

        ElseIf Me.cmbPlanNo.SelectedValue > 0 And Me.cmbTicketNo.SelectedValue > 0 And Me.cmbBatchNo.SelectedValue > 0 Then

            fillGrid("Select CloseBatchFinishGoodsDetail.CloseBatchFinishGoodsDetailId, DepartmentWiseProductionDetail.PlanID , planmastertable.PlanNo , " _
                     & "DepartmentWiseProductionDetail.TicketID , planticketsmaster.BatchNo As TicketNo , CloseBatchFinishGoodsDetail.CloseBatchId , CloseBatch.BatchNo, " _
                     & "CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId, CloseBatchFinishGoodsDetail.ArticleId, Article.ArticleDescription, " _
                     & "IsNull(CloseBatchFinishGoodsDetail.Qty, 0) AS Qty , Article.PurchasePrice as Price, Article.ArticleCode as Code , ArticleColorDeftable.ArticleColorName as Color, ArticlesizeDeftable.ArticleSizeName as Size , ArticleunitDeftable.ArticleUnitName as UnitName , IsNull(Article.PackQty, 0) AS PackQty , Article.MasterId , IsNull(ArticleDefView.subsubId,0) as PurchaseAccountId , IsNull(ArticleDefView.CGSAccountId,0) as CGSAccountId , ArticleDefView.ArticleUnitName As UOM , IsNull(articlegroupdeftable.CGSAccountId,0) as DepartmentCGSAccountId , IsNull(ProductionProcess.WIPAccountId,0) as ProcessCGSAccountId , IsNull(articledeftablemaster.CGSAccountId,0) as ItemCGSAccountId , tblprosteps.prod_step as Stage , ProductionProcessDetail.SortOrder " _
                     & "FROM CloseBatchFinishGoodsDetail " _
                     & "LEFT OUTER JOIN CloseBatch ON CloseBatch.CloseBatchId = CloseBatchFinishGoodsDetail.CloseBatchId " _
                     & "LEFT OUTER JOIN ArticleDefTable AS Article ON CloseBatchFinishGoodsDetail.ArticleId = Article.ArticleId " _
                     & "LEFT OUTER JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.Id " _
                     & "LEFT OUTER JOIN tblprosteps ON DepartmentWiseProductionDetail.DepartmentId = tblprosteps.ProdStep_Id " _
                     & "LEFT OUTER JOIN articledeftablemaster ON articledeftablemaster.ArticleId = Article.MasterId " _
                     & "LEFT OUTER JOIN planticketsmaster ON planticketsmaster.PlanTicketsMasterID = DepartmentWiseProductionDetail.TicketID " _
                     & "LEFT OUTER JOIN planmastertable ON planmastertable.PlanId = DepartmentWiseProductionDetail.PlanID " _
                     & "LEFT OUTER JOIN ArticleColorDeftable ON ArticleColorDeftable.ArticleColorId = Article.ArticleColorId " _
                     & "LEFT OUTER JOIN ArticlesizeDeftable ON ArticlesizeDeftable.ArticleSizeId = Article.SizeRangeId " _
                     & "LEFT OUTER JOIN ArticleunitDeftable ON ArticleunitDeftable.ArticleUnitId = Article.ArticleUnitId " _
                     & "LEFT OUTER JOIN ArticleDefView ON ArticleDefView.articleId = Article.ArticleId " _
                     & "LEFT OUTER JOIN ProductionProcess ON ProductionProcess.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                     & "LEFT OUTER JOIN articlegroupdeftable ON articlegroupdeftable.articlegroupId = articledeftablemaster.articlegroupId " _
                     & "LEFT OUTER JOIN ProductionProcessDetail  On ProductionProcessDetail.ProductionProcessId = articledeftablemaster.ProductionProcessId and " _
                     & "ProductionProcessDetail.ProductionStepId = tblprosteps.ProdStep_Id " _
                     & "Left Outer JOIN " _
                     & "(select Article.MasterId as MasterId, Max(ProductionProcessDetail.SortOrder) as HigherOrder from CloseBatchFinishGoodsDetail " _
                     & "LEFT OUTER JOIN CloseBatch ON CloseBatch.CloseBatchId = CloseBatchFinishGoodsDetail.CloseBatchId " _
                     & "LEFT OUTER JOIN ArticleDefTable AS Article ON CloseBatchFinishGoodsDetail.ArticleId = Article.ArticleId " _
                     & "LEFT OUTER JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.Id " _
                     & "LEFT OUTER JOIN tblprosteps ON DepartmentWiseProductionDetail.DepartmentId = tblprosteps.ProdStep_Id " _
                     & "LEFT OUTER JOIN articledeftablemaster ON articledeftablemaster.ArticleId = Article.MasterId " _
                     & "LEFT OUTER JOIN planticketsmaster ON planticketsmaster.PlanTicketsMasterID = DepartmentWiseProductionDetail.TicketID " _
                     & "LEFT OUTER JOIN planmastertable ON planmastertable.PlanId = DepartmentWiseProductionDetail.PlanID " _
                     & "LEFT JOIN ProductionProcess ON ProductionProcess.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                     & "LEFT JOIN ProductionProcessDetail  On ProductionProcessDetail.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                     & "And ProductionProcessDetail.ProductionStepId = tblprosteps.ProdStep_Id " _
                     & "WHERE DepartmentWiseProductionDetail.PlanID = " & Me.cmbPlanNo.SelectedValue & " And DepartmentWiseProductionDetail.TicketID = " & Me.cmbTicketNo.SelectedValue & " And CloseBatchFinishGoodsDetail.CloseBatchId = " & Me.cmbBatchNo.SelectedValue & " " _
                     & "Group By Article.MasterId) " _
                     & "As HigherSortOrder ON Article.MasterId = HigherSortOrder.MasterId " _
                     & "WHERE DepartmentWiseProductionDetail.PlanID = " & Me.cmbPlanNo.SelectedValue & " And DepartmentWiseProductionDetail.TicketID = " & Me.cmbTicketNo.SelectedValue & " and CloseBatchFinishGoodsDetail.CloseBatchId = " & Me.cmbBatchNo.SelectedValue & "" _
                     & " and ProductionProcessDetail.SortOrder = HigherSortOrder.HigherOrder ")

        ElseIf Me.cmbPlanNo.SelectedValue <= 0 And Me.cmbTicketNo.SelectedValue <= 0 And Me.cmbBatchNo.SelectedValue > 0 Then

            fillGrid("Select CloseBatchFinishGoodsDetail.CloseBatchFinishGoodsDetailId, DepartmentWiseProductionDetail.PlanID , planmastertable.PlanNo , " _
                     & "DepartmentWiseProductionDetail.TicketID , planticketsmaster.BatchNo As TicketNo , CloseBatchFinishGoodsDetail.CloseBatchId , CloseBatch.BatchNo, " _
                     & "CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId, CloseBatchFinishGoodsDetail.ArticleId, Article.ArticleDescription, " _
                     & "IsNull(CloseBatchFinishGoodsDetail.Qty, 0) AS Qty , Article.PurchasePrice as Price, Article.ArticleCode as Code , ArticleColorDeftable.ArticleColorName as Color, ArticlesizeDeftable.ArticleSizeName as Size , ArticleunitDeftable.ArticleUnitName as UnitName ,  IsNull(Article.PackQty, 0) AS PackQty , Article.MasterId , IsNull(ArticleDefView.subsubId,0) as PurchaseAccountId , IsNull(articlegroupdeftable.CGSAccountId,0) as DepartmentCGSAccountId , IsNull(ProductionProcess.WIPAccountId,0) as ProcessCGSAccountId , IsNull(articledeftablemaster.CGSAccountId,0) as ItemCGSAccountId , ArticleDefView.ArticleUnitName As UOM , articledeftablemaster.ProductionProcessId  , tblprosteps.prod_step as Stage , ProductionProcessDetail.SortOrder " _
                     & "FROM CloseBatchFinishGoodsDetail " _
                     & "LEFT OUTER JOIN CloseBatch ON CloseBatch.CloseBatchId = CloseBatchFinishGoodsDetail.CloseBatchId " _
                     & "LEFT OUTER JOIN ArticleDefTable AS Article ON CloseBatchFinishGoodsDetail.ArticleId = Article.ArticleId " _
                     & "LEFT OUTER JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.Id " _
                     & "LEFT OUTER JOIN tblprosteps ON DepartmentWiseProductionDetail.DepartmentId = tblprosteps.ProdStep_Id " _
                     & "LEFT OUTER JOIN articledeftablemaster ON articledeftablemaster.ArticleId = Article.MasterId " _
                     & "LEFT OUTER JOIN planticketsmaster ON planticketsmaster.PlanTicketsMasterID = DepartmentWiseProductionDetail.TicketID " _
                     & "LEFT OUTER JOIN planmastertable ON planmastertable.PlanId = DepartmentWiseProductionDetail.PlanID " _
                     & "LEFT OUTER JOIN ArticleColorDeftable ON ArticleColorDeftable.ArticleColorId = Article.ArticleColorId " _
                     & "LEFT OUTER JOIN ArticlesizeDeftable ON ArticlesizeDeftable.ArticleSizeId = Article.SizeRangeId " _
                     & "LEFT OUTER JOIN ArticleunitDeftable ON ArticleunitDeftable.ArticleUnitId = Article.ArticleUnitId " _
                     & "LEFT OUTER JOIN ArticleDefView ON ArticleDefView.articleId = Article.ArticleId " _
                     & "LEFT OUTER JOIN ProductionProcess ON ProductionProcess.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                     & "LEFT OUTER JOIN articlegroupdeftable ON articlegroupdeftable.articlegroupId = articledeftablemaster.articlegroupId " _
                     & "LEFT OUTER JOIN ProductionProcessDetail  On ProductionProcessDetail.ProductionProcessId = articledeftablemaster.ProductionProcessId and " _
                     & "ProductionProcessDetail.ProductionStepId = tblprosteps.ProdStep_Id " _
                     & "Left Outer JOIN " _
                     & "(select Article.MasterId as MasterId, Max(ProductionProcessDetail.SortOrder) as HigherOrder from CloseBatchFinishGoodsDetail " _
                     & "LEFT OUTER JOIN CloseBatch ON CloseBatch.CloseBatchId = CloseBatchFinishGoodsDetail.CloseBatchId " _
                     & "LEFT OUTER JOIN ArticleDefTable AS Article ON CloseBatchFinishGoodsDetail.ArticleId = Article.ArticleId " _
                     & "LEFT OUTER JOIN DepartmentWiseProductionDetail ON CloseBatchFinishGoodsDetail.DepartmentWiseProductionDetailId = DepartmentWiseProductionDetail.Id " _
                     & "LEFT OUTER JOIN tblprosteps ON DepartmentWiseProductionDetail.DepartmentId = tblprosteps.ProdStep_Id " _
                     & "LEFT OUTER JOIN articledeftablemaster ON articledeftablemaster.ArticleId = Article.MasterId " _
                     & "LEFT OUTER JOIN planticketsmaster ON planticketsmaster.PlanTicketsMasterID = DepartmentWiseProductionDetail.TicketID " _
                     & "LEFT OUTER JOIN planmastertable ON planmastertable.PlanId = DepartmentWiseProductionDetail.PlanID " _
                     & "LEFT JOIN ProductionProcess ON ProductionProcess.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                     & "LEFT JOIN ProductionProcessDetail  On ProductionProcessDetail.ProductionProcessId = articledeftablemaster.ProductionProcessId " _
                     & "And ProductionProcessDetail.ProductionStepId = tblprosteps.ProdStep_Id " _
                     & "WHERE CloseBatchFinishGoodsDetail.CloseBatchId = " & Me.cmbBatchNo.SelectedValue & " " _
                     & "Group By Article.MasterId) " _
                     & "As HigherSortOrder ON Article.MasterId = HigherSortOrder.MasterId " _
                     & "WHERE CloseBatchFinishGoodsDetail.CloseBatchId = " & Me.cmbBatchNo.SelectedValue & "" _
                     & " and ProductionProcessDetail.SortOrder = HigherSortOrder.HigherOrder ")

        Else

            ShowInformationMessage("No rows found in grid")

        End If

    End Sub

    Public Sub fillGrid(query As String)

        Try
            Dim dt As New DataTable
            dt = New PlanTicketsDAL().GetDeatilsByTickets(query)
            Me.grdItems.DataSource = dt

            If dt.Rows.Count < 0 Then

                ShowInformationMessage("No rows found in grid")

            End If

            Me.grdItems.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdItems.RootTable.Columns("Price").FormatString = "N" & DecimalPointInValue

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub grdItems_KeyDown(sender As Object, e As KeyEventArgs) Handles grdItems.KeyDown

        Try

            Dim dt As New DataTable

            dt.Columns.Add("Location_Id", System.Type.GetType("System.Int32"))
            dt.Columns.Add("ArticleDefId", System.Type.GetType("System.Int32"))
            dt.Columns.Add("Location_Name", System.Type.GetType("System.String"))
            dt.Columns.Add("ArticleCode", System.Type.GetType("System.String"))
            dt.Columns.Add("ArticleDescription", System.Type.GetType("System.String"))
            dt.Columns.Add("ArticleColorName", System.Type.GetType("System.String"))
            dt.Columns.Add("ArticleSizeName", System.Type.GetType("System.String"))
            dt.Columns.Add("UnitName", System.Type.GetType("System.String"))
            dt.Columns.Add("ArticleSize", System.Type.GetType("System.String"))
            dt.Columns.Add("PackQty", System.Type.GetType("System.Double"))
            dt.Columns.Add("Qty", System.Type.GetType("System.Double"))
            dt.Columns.Add("Dim1", System.Type.GetType("System.Double"))
            dt.Columns.Add("Dim2", System.Type.GetType("System.Double"))
            dt.Columns.Add("CurrentRate", System.Type.GetType("System.Double"))
            dt.Columns.Add("PackPrice", System.Type.GetType("System.Double"))
            dt.Columns.Add("Total", System.Type.GetType("System.Double"))
            dt.Columns.Add("Comments", System.Type.GetType("System.String"))
            dt.Columns.Add("BatchNo", System.Type.GetType("System.String"))
            dt.Columns.Add("UOM", System.Type.GetType("System.String"))
            dt.Columns.Add("Pack_Desc", System.Type.GetType("System.String"))
            dt.Columns.Add("RetailPrice", System.Type.GetType("System.Double"))
            dt.Columns.Add("PurchaseAccountId", System.Type.GetType("System.Int32"))
            dt.Columns.Add("CGSAccountId", System.Type.GetType("System.Int32"))
            dt.Columns.Add("EmployeeId", System.Type.GetType("System.Int32"))
            dt.Columns.Add("ExpiryDate", GetType(DateTime))
            dt.Columns.Add("EngineNo", System.Type.GetType("System.String"))
            dt.Columns.Add("ChasisNo", System.Type.GetType("System.String"))
            dt.Columns.Add("PlanDetailId", System.Type.GetType("System.Int32"))
            dt.Columns.Add("MasterId", System.Type.GetType("System.Int32"))
            dt.Columns.Add("TotalQty", System.Type.GetType("System.Double"))
            dt.Columns.Add("PlanID", System.Type.GetType("System.Int32"))
            dt.Columns.Add("TicketID", System.Type.GetType("System.Int32"))


            For Each row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows

                Dim R As DataRow = dt.NewRow

                R("Location_Id") = Me.LocationId
                R("ArticleDefId") = Val(row.Cells("ArticleId").Value.ToString)
                R("Location_Name") = Me.LocationName
                R("ArticleCode") = row.Cells("Code").Value.ToString
                R("ArticleDescription") = row.Cells("ArticleDescription").Value.ToString
                R("ArticleColorName") = row.Cells("Color").Value.ToString
                R("ArticleSizeName") = row.Cells("Size").Value.ToString
                R("UnitName") = row.Cells("UnitName").Value.ToString

                R("ArticleSize") = String.Empty
                R("PackQty") = Val(row.Cells("PackQty").Value.ToString)

                R("Qty") = Val(row.Cells("Qty").Value.ToString)

                R("Dim1") = 0
                R("Dim2") = 0

                R("CurrentRate") = Val(row.Cells("Price").Value.ToString)

                R("PackPrice") = 0
                R("Total") = Val(row.Cells("Qty").Value.ToString) * Val(row.Cells("Price").Value.ToString)
                R("Comments") = String.Empty

                R("BatchNo") = row.Cells("BatchNo").Value.ToString

                R("UOM") = row.Cells("UOM").Value.ToString
                R("Pack_Desc") = String.Empty
                R("RetailPrice") = 0
                R("PurchaseAccountId") = Val(row.Cells("PurchaseAccountId").Value.ToString)

                Val(row.Cells("ProcessCGSAccountId").Value.ToString)
                Val(row.Cells("ItemCGSAccountId").Value.ToString)
                Val(row.Cells("DepartmentCGSAccountId").Value.ToString)

                If Val(row.Cells("ProcessCGSAccountId").Value.ToString) > 0 Then

                    R("CGSAccountId") = Val(row.Cells("ProcessCGSAccountId").Value.ToString)

                ElseIf Val(row.Cells("ProcessCGSAccountId").Value.ToString) <= 0 And Val(row.Cells("ItemCGSAccountId").Value.ToString) > 0 Then

                    R("CGSAccountId") = Val(row.Cells("ItemCGSAccountId").Value.ToString)

                ElseIf Val(row.Cells("ProcessCGSAccountId").Value.ToString) <= 0 And Val(row.Cells("ItemCGSAccountId").Value.ToString) <= 0 And Val(row.Cells("DepartmentCGSAccountId").Value.ToString) > 0 Then

                    R("CGSAccountId") = Val(row.Cells("DepartmentCGSAccountId").Value.ToString)

                Else

                    R("CGSAccountId") = 0

                End If


                R("EmployeeId") = 0
                R("ExpiryDate") = DBNull.Value
                R("EngineNo") = String.Empty
                R("ChasisNo") = String.Empty
                R("PlanDetailId") = 0
                R("MasterId") = Val(row.Cells("MasterId").Value.ToString)
                R("TotalQty") = Val(row.Cells("Qty").Value.ToString)

                R("PlanID") = Val(row.Cells("PlanID").Value.ToString)
                R("TicketID") = Val(row.Cells("TicketID").Value.ToString)

                dt.Rows.Add(R)

            Next

            'frmProductionStore.fillProductionStoreGrid(dt)

            Me.Close()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click

        ReSetControls()

    End Sub
End Class