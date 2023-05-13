Imports Janus.Windows.GridEX
Imports SBModel
Imports System.Data
Imports System.Linq
Imports SBDal.StockDAL
Imports SBDal.StockDocTypeDAL
Imports System.Data.SqlClient

Public Class MaterialDecompositionBAL
    Dim DAL As MaterialDecompositionDAL
    Public Clone As Boolean = False
    Public dtAllItems As DataTable
    Public UniqueId As Integer = 0
    Public dtGetCostSheet As New DataTable
    Public dtEstimationConsumption As DataTable
    Public CounTag As Integer = 0
    Dim dtParents As DataTable
    Dim ParentList As List(Of Integer)
    Public Function Save_Transation(ByVal dt As DataTable, ByVal Obj As MaterialDecompositionModel, ByVal Source As String, ByVal MyCompanyId As Integer, ByVal WastedStockAccount As Integer, ByVal ScrappedStockAccount As Integer) As List(Of String)
        Try
            Dim DetailQueryList As List(Of String) = New List(Of String)
            DAL = New MaterialDecompositionDAL()
            Dim MasterQuery As String = "insert into MaterialDecompositionMaster(DecompositionDate, DocumentNo, Remarks, CustomerId, SalesOrderId, PlanId, TicketId, EstimationId) values(N'" & Obj.DecompositionDate.ToString("yyyy-M-d hh:mm:ss tt") & "', N'" & Obj.DocumentNo.Replace("'", "''") & "', N'" & Obj.Remarks.Replace("'", "''") & "', " & Obj.CustomerId & ", " & Obj.SalesOrderId & ", " & Obj.PlanId & ", " & Obj.TicketId & ", " & Obj.EstimationId & ")Select @@Identity"
            Dim Detailquery As String
            For i As Integer = 0 To dt.Rows.Count - 1
                'If Val(dt.Rows(i).Item("DecomposedQty").ToString) > 0 Or Val(dt.Rows(i).Item("WastedQty").ToString) > 0 Or Val(dt.Rows(i).Item("ScrappedQty").ToString) > 0 Then
                Detailquery = "Insert Into MaterialDecompositionDetail(DecompositionId, EstimationDetailId, PlanItemId, ProductId, ParentId, Price, DecomposedQty, WastedQty, ScrappedQty, Tag#, ParentTag#, DepartmentId,  LocationId, UniqueId, ParentUniqueId, TotalConsumedQty, StockImpact , TicketId) Values(`, " & Val(dt.Rows(i).Item("EstimationDetailId").ToString) & ", " & Val(dt.Rows(i).Item("PlanItemId").ToString) & ", " & Val(dt.Rows(i).Item("ProductId").ToString) & ", " & Val(dt.Rows(i).Item("ParentId").ToString) & ", " & Val(dt.Rows(i).Item("Price").ToString) & ", " & Val(dt.Rows(i).Item("DecomposedQty").ToString) & ", " & Val(dt.Rows(i).Item("WastedQty").ToString) & ", " & Val(dt.Rows(i).Item("ScrappedQty").ToString) & ", " & Val(dt.Rows(i).Item("Tag").ToString) & ", " & Val(dt.Rows(i).Item("ParentTag").ToString) & ", " & Val(dt.Rows(i).Item("DepartmentId").ToString) & ", " & Val(dt.Rows(i).Item("LocationId").ToString) & ", " & Val(dt.Rows(i).Item("UniqueId").ToString) & ", " & Val(dt.Rows(i).Item("UniqueParentId").ToString) & ", " & Val(dt.Rows(i).Item("TotalConsumedQty").ToString) & ", " & Val(dt.Rows(i).Item("StockImpact").ToString) & " , " & Val(dt.Rows(i).Item("TicketId").ToString) & ")"
                DetailQueryList.Add(Detailquery)
                'End If
            Next
            DAL.Master_Insertion(MasterQuery, DetailQueryList, Obj.StockMaster, Obj, dt, Source, MyCompanyId, WastedStockAccount, ScrappedStockAccount)
            Return DetailQueryList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetMaster() As DataTable
        Try
            Dim MasterQuery As String = "SELECT Decomposition.DecompositionId, Decomposition.DocumentNo, Decomposition.DecompositionDate, Decomposition.CustomerId, Customer.detail_title AS Customer, Decomposition.SalesOrderId, SalesOrder.SalesOrderNo, Decomposition.PlanId, Pla.PlanNo, Decomposition.TicketId, Ticket.TicketNo, Decomposition.EstimationId, Estimation.DocNo As [Estimation No], Decomposition.Remarks " _
                                  & " FROM MaterialDecompositionMaster AS Decomposition LEFT OUTER JOIN " _
                                  & " vwCOADetail AS Customer ON Decomposition.CustomerId = Customer.coa_detail_id LEFT OUTER JOIN " _
                                  & " SalesOrderMasterTable AS SalesOrder ON Decomposition.SalesOrderId = SalesOrder.SalesOrderId LEFT OUTER JOIN " _
                                  & " PlanMasterTable AS Pla ON Decomposition.PlanId = Pla.PlanId LEFT OUTER JOIN " _
                                  & " PlanTicketsMaster AS Ticket ON Decomposition.TicketId = Ticket.PlanTicketsMasterId LEFT OUTER JOIN " _
                                  & " MaterialEstimation AS Estimation ON Decomposition.EstimationId = Estimation.Id " _
                                  & " ORDER BY Decomposition.DecompositionId DESC"
            DAL = New MaterialDecompositionDAL()
            Dim table = DAL.ReadTable(MasterQuery)
            Return table
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetEstimation(ByVal EstimationId As Integer, ByVal LocationId As Integer) As DataTable
        Try
            Dim MasterQuery As String = " Select 0 AS DecompositionDetailId, 0 AS DecompositionId, 0 AS EstimationDetailId, EstimationDetail.PlanItemId, PlanArticle.ArticleDescription As PlanItem, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription As Product, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(EstimationDetail.Price, 0) AS Price, " _
                                        & "  Sum(Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End) AS Qty, Sum(IsNull(Decomposition.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(Decomposition.WastedQty, 0)) AS WastedQty, Sum(IsNull(Decomposition.ScrappedQty, 0)) AS ScrappedQty, Sum((Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End -(IsNull(Decomposition.DecomposedQty, 0)+IsNull(Decomposition.WastedQty, 0)+IsNull(Decomposition.ScrappedQty, 0)))) AS DecomposableQty, IsNull(EstimationDetail.Tag#, 0) AS Tag, IsNull(EstimationDetail.ParentTag#, 0) AS ParentTag, EstimationDetail.SubDepartmentId AS DepartmentId, tblProSteps.prod_step As Department, " & LocationId & " AS LocationId, Decomposition.Action, 0 As UniqueId, 0 As ParentUniqueId, Sum(IsNull(Decomposition.DecomposedQty, 0)) AS TempDecQty, Sum(IsNull(Decomposition.WastedQty, 0)) AS TempWasQty, Sum(IsNull(Decomposition.ScrappedQty, 0)) AS TempScrQty " _
                                        & " FROM MaterialEstimationDetailTable As EstimationDetail INNER JOIN MaterialEstimation AS Estimation ON EstimationDetail.MaterialEstMasterID = Estimation.Id INNER JOIN ArticleDefTable AS Article ON EstimationDetail.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleDefTable AS PlanArticle ON EstimationDetail.PlanItemId = PlanArticle.ArticleId " _
                                        & " INNER JOIN (SELECT IsNull(Production.PlanId, 0) As PlanId, IsNull(Production.PlanTicketId, 0) As TicketId FROM ProductionMasterTable AS Production INNER JOIN ProductionDetailTable AS ProductionDetail ON Production.Production_ID = ProductionDetail.Production_ID) AS Production ON Estimation.MasterPlanId = Production.PlanId AND Estimation.PlanTicketId = Production.TicketId " _
                                        & " LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN tblProSteps ON EstimationDetail.SubDepartmentID = tblProSteps.ProdStep_Id " _
                                        & " LEFT OUTER JOIN (SELECT Sum(IsNull(DecompositionDetail.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(DecompositionDetail.WastedQty, 0)) AS WastedQty, Sum(IsNull(DecompositionDetail.ScrappedQty, 0)) AS ScrappedQty, IsNull(DecompositionDetail.ProductId, 0) AS ProductId, IsNull(DecompositionDetail.PlanItemId, 0) AS PlanItemId, DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0) AS EstimationId, IsNull(Decomposition.TicketId, 0) AS TicketId, IsNull(Decomposition.PlanId, 0) AS PlanId, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, DecompositionDetail.Action AS Action FROM MaterialDecompositionMaster AS Decomposition INNER JOIN MaterialDecompositionDetail AS DecompositionDetail ON Decomposition.DecompositionId = DecompositionDetail.DecompositionId Group By IsNull(DecompositionDetail.ProductId, 0), IsNull(DecompositionDetail.PlanItemId, 0), DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0), IsNull(Decomposition.TicketId, 0), IsNull(Decomposition.PlanId, 0), IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.Action) AS Decomposition " _
                                        & " ON EstimationDetail.ProductId = Decomposition.ProductId AND EstimationDetail.PlanItemId = Decomposition.PlanItemId AND EstimationDetail.Tag# = Decomposition.Tag# AND EstimationDetail.ParentTag# = Decomposition.ParentTag# AND Estimation.Id = Decomposition.EstimationId AND Estimation.PlanTicketId = Decomposition.TicketId AND Estimation.MasterPlanId = Decomposition.PlanId " _
                                        & " WHERE Estimation.Id = " & EstimationId & " Group By EstimationDetail.PlanItemId, PlanArticle.ArticleDescription, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(EstimationDetail.Price, 0), IsNull(EstimationDetail.Tag#, 0), IsNull(EstimationDetail.ParentTag#, 0), EstimationDetail.SubDepartmentId, tblProSteps.prod_step, IsNull(Decomposition.LocationId, 0), Decomposition.Action Having Sum(Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End) > Sum(IsNull(Decomposition.DecomposedQty, 0)+IsNull(Decomposition.WastedQty, 0)+IsNull(Decomposition.ScrappedQty, 0)) "
            DAL = New MaterialDecompositionDAL()
            Dim table As DataTable = DAL.ReadTable(MasterQuery)
            'table.AcceptChanges()
            Return table
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetEstimation(ByVal TicketId As Integer, ByVal EstimationId As Integer, ByVal ProductId As Integer, ByVal LocationId As Integer, ByVal Qty As Double) As DataTable
        Try
            Dim MasterQuery As String = " Select 0 AS DecompositionDetailId, 0 AS DecompositionId, 0 AS EstimationDetailId, EstimationDetail.PlanItemId, PlanArticle.ArticleDescription As PlanItem, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription As Product, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(EstimationDetail.Price, 0) AS Price, " _
                                        & "  Sum(Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End) AS Qty, Sum(IsNull(Decomposition.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(Decomposition.WastedQty, 0)) AS WastedQty, Sum(IsNull(Decomposition.ScrappedQty, 0)) AS ScrappedQty, Sum((Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End -(IsNull(Decomposition.DecomposedQty, 0)+IsNull(Decomposition.WastedQty, 0)+IsNull(Decomposition.ScrappedQty, 0)))) AS DecomposableQty, IsNull(EstimationDetail.Tag#, 0) AS Tag, IsNull(EstimationDetail.ParentTag#, 0) AS ParentTag, EstimationDetail.SubDepartmentId AS DepartmentId, tblProSteps.prod_step As Department, " & LocationId & " AS LocationId, Decomposition.Action, 0 As UniqueId, 0 As ParentUniqueId, Sum(IsNull(Decomposition.DecomposedQty, 0)) AS TempDecQty, Sum(IsNull(Decomposition.WastedQty, 0)) AS TempWasQty, Sum(IsNull(Decomposition.ScrappedQty, 0)) AS TempScrQty " _
                                        & " FROM MaterialEstimationDetailTable As EstimationDetail INNER JOIN MaterialEstimation AS Estimation ON EstimationDetail.MaterialEstMasterID = Estimation.Id INNER JOIN ArticleDefTable AS Article ON EstimationDetail.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleDefTable AS PlanArticle ON EstimationDetail.PlanItemId = PlanArticle.ArticleId " _
                                        & " INNER JOIN (SELECT IsNull(Production.PlanId, 0) As PlanId, IsNull(Production.PlanTicketId, 0) As TicketId FROM ProductionMasterTable AS Production INNER JOIN ProductionDetailTable AS ProductionDetail ON Production.Production_ID = ProductionDetail.Production_ID) AS Production ON Estimation.MasterPlanId = Production.PlanId AND Estimation.PlanTicketId = Production.TicketId " _
                                        & " LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN tblProSteps ON EstimationDetail.SubDepartmentID = tblProSteps.ProdStep_Id " _
                                        & " LEFT OUTER JOIN (SELECT Sum(IsNull(DecompositionDetail.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(DecompositionDetail.WastedQty, 0)) AS WastedQty, Sum(IsNull(DecompositionDetail.ScrappedQty, 0)) AS ScrappedQty, IsNull(DecompositionDetail.ProductId, 0) AS ProductId, IsNull(DecompositionDetail.PlanItemId, 0) AS PlanItemId, DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0) AS EstimationId, IsNull(Decomposition.TicketId, 0) AS TicketId, IsNull(Decomposition.PlanId, 0) AS PlanId, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, DecompositionDetail.Action AS Action FROM MaterialDecompositionMaster AS Decomposition INNER JOIN MaterialDecompositionDetail AS DecompositionDetail ON Decomposition.DecompositionId = DecompositionDetail.DecompositionId Group By IsNull(DecompositionDetail.ProductId, 0), IsNull(DecompositionDetail.PlanItemId, 0), DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0), IsNull(Decomposition.TicketId, 0), IsNull(Decomposition.PlanId, 0), IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.Action) AS Decomposition " _
                                        & " ON EstimationDetail.ProductId = Decomposition.ProductId AND EstimationDetail.PlanItemId = Decomposition.PlanItemId AND EstimationDetail.Tag# = Decomposition.Tag# AND EstimationDetail.ParentTag# = Decomposition.ParentTag# AND Estimation.Id = Decomposition.EstimationId AND Estimation.PlanTicketId = Decomposition.TicketId AND Estimation.MasterPlanId = Decomposition.PlanId " _
                                        & " WHERE Estimation.Id = " & EstimationId & " AND  Group By EstimationDetail.PlanItemId, PlanArticle.ArticleDescription, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(EstimationDetail.Price, 0), IsNull(EstimationDetail.Tag#, 0), IsNull(EstimationDetail.ParentTag#, 0), EstimationDetail.SubDepartmentId, tblProSteps.prod_step, IsNull(Decomposition.LocationId, 0), Decomposition.Action Having Sum(Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End) > Sum(IsNull(Decomposition.DecomposedQty, 0)+IsNull(Decomposition.WastedQty, 0)+IsNull(Decomposition.ScrappedQty, 0)) "
            DAL = New MaterialDecompositionDAL()
            Dim table As DataTable = DAL.ReadTable(MasterQuery)
            'table.AcceptChanges()
            Return table
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DisplayDetail(ByVal DecompositionId As Integer, ByVal DecimalPointInQty As Integer) As DataTable
        Try
            ''Below lines are commented on 07-12-2017
            'Dim MasterQuery As String = " Select 0 AS DecompositionDetailId, 0 AS DecompositionId, 0 AS EstimationDetailId, DecompositionDetail.PlanItemId, PlanArticle.ArticleDescription As PlanItem, DecompositionDetail.ProductId, DecompositionDetail.ParentId, Article.ArticleDescription As Product, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(DecompositionDetail.Price, 0) AS Price, " _
            '                            & " Sum(Convert(Decimal(18, " & DecimalPointInQty & "),IsNull(Estimation.Quantity, 0))) AS Qty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.DecomposedQty, 0))) AS DecomposedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.WastedQty, 0))) AS WastedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.ScrappedQty, 0))) AS ScrappedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Estimation.Quantity, 0)-(IsNull(DecompositionDetail.DecomposedQty, 0)+IsNull(DecompositionDetail.WastedQty, 0)+IsNull(DecompositionDetail.ScrappedQty, 0))))) AS DecomposableQty, IsNull(DecompositionDetail.Tag#, 0) AS Tag, IsNull(DecompositionDetail.ParentTag#, 0) AS ParentTag, DecompositionDetail.DepartmentId, tblProSteps.prod_step As Department, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, DecompositionDetail.Action, 0 As UniqueId, 0 As ParentUniqueId, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.TotalConsumedQty, 0))) AS TotalConsumedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.DecomposedQty, 0))) AS TempDecQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.WastedQty, 0))) AS TempWasQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.ScrappedQty, 0))) AS TempScrQty, 0 As IsTopParent, 0 AS StockImpact " _
            '                            & " FROM MaterialDecompositionDetail As DecompositionDetail INNER JOIN MaterialDecompositionMaster AS Decomposition ON DecompositionDetail.DecompositionId = Decomposition.DecompositionId INNER JOIN ArticleDefTable AS Article ON DecompositionDetail.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleDefTable AS PlanArticle ON DecompositionDetail.PlanItemId = PlanArticle.ArticleId " _
            '                            & " LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN tblProSteps ON DecompositionDetail.DepartmentId = tblProSteps.ProdStep_Id " _
            '                            & " LEFT OUTER JOIN (SELECT Sum(Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End) AS Quantity, IsNull(EstimationDetail.ProductId, 0) AS ProductId, IsNull(EstimationDetail.ParentId, 0) AS ParentId, IsNull(EstimationDetail.PlanItemId, 0) AS PlanItemId, EstimationDetail.Tag#, EstimationDetail.ParentTag#, IsNull(Estimation.Id, 0) AS EstimationId, IsNull(Estimation.PlanTicketId, 0) AS TicketId, IsNull(Estimation.MasterPlanId, 0) AS PlanId, IsNull(EstimationDetail.SubDepartmentId, 0) AS DepartmentId FROM MaterialEstimation AS Estimation INNER JOIN MaterialEstimationDetailTable As EstimationDetail ON EstimationDetail.MaterialEstMasterID = Estimation.Id Group By IsNull(EstimationDetail.ProductId, 0), IsNull(EstimationDetail.ParentId, 0), IsNull(EstimationDetail.PlanItemId, 0), EstimationDetail.Tag#, EstimationDetail.ParentTag#, IsNull(Estimation.Id, 0), IsNull(Estimation.PlanTicketId, 0), IsNull(Estimation.MasterPlanId, 0), IsNull(EstimationDetail.SubDepartmentId, 0)) AS Estimation " _
            '                            & " ON Estimation.ProductId = DecompositionDetail.ProductId AND Estimation.ParentId = DecompositionDetail.ParentId AND Estimation.DepartmentId = DecompositionDetail.DepartmentId AND Estimation.PlanItemId = DecompositionDetail.PlanItemId AND Estimation.Tag# = DecompositionDetail.Tag# AND Estimation.ParentTag# = DecompositionDetail.ParentTag# AND Estimation.EstimationId = Decomposition.EstimationId AND Estimation.TicketId = Decomposition.TicketId AND Estimation.PlanId = Decomposition.PlanId " _
            '                            & " WHERE Decomposition.DecompositionId = " & DecompositionId & " Group By DecompositionDetail.PlanItemId, PlanArticle.ArticleDescription, DecompositionDetail.ProductId, DecompositionDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(DecompositionDetail.Price, 0), IsNull(DecompositionDetail.Tag#, 0), IsNull(DecompositionDetail.ParentTag#, 0), DecompositionDetail.DepartmentId, tblProSteps.prod_step, IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.DecompositionDetailId, DecompositionDetail.DecompositionId, DecompositionDetail.Action "

            Dim MasterQuery As String = " Select 0 AS DecompositionDetailId, 0 AS DecompositionId, 0 AS EstimationDetailId, DecompositionDetail.PlanItemId, PlanArticle.ArticleDescription As PlanItem, DecompositionDetail.ProductId, DecompositionDetail.ParentId, Article.ArticleDescription As Product, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(DecompositionDetail.Price, 0) AS Price, " _
                                      & " Sum(Convert(Decimal(18, " & DecimalPointInQty & "),IsNull(Estimation.Quantity, 0))) AS Qty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.DecomposedQty, 0))) AS DecomposedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.WastedQty, 0))) AS WastedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.ScrappedQty, 0))) AS ScrappedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Estimation.Quantity, 0)-(IsNull(DecompositionQty.TotalDecomposedQty, 0))))) AS DecomposableQty, IsNull(DecompositionDetail.Tag#, 0) AS Tag, IsNull(DecompositionDetail.ParentTag#, 0) AS ParentTag, DecompositionDetail.DepartmentId, tblProSteps.prod_step As Department, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, DecompositionDetail.Action, 0 As UniqueId, 0 As ParentUniqueId, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.TotalConsumedQty, 0))) AS TotalConsumedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.DecomposedQty, 0))) AS TempDecQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.WastedQty, 0))) AS TempWasQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(DecompositionDetail.ScrappedQty, 0))) AS TempScrQty, 0 As IsTopParent, Convert(Int, IsNull(DecompositionDetail.StockImpact, 0)) AS StockImpact, Sum(IsNull(DecCount.ChildCount, 0)) AS ConsumedChildCount, IsNull(ArticleAccount.SubSubId, 0) AS SubSubId, IsNull(PlanItemAccount.SubSubId, 0) AS PlanItemSubSubId, 0 AS DValue, 0 AS WValue, 0 AS SValue " _
                                      & " FROM MaterialDecompositionDetail As DecompositionDetail INNER JOIN MaterialDecompositionMaster AS Decomposition ON DecompositionDetail.DecompositionId = Decomposition.DecompositionId INNER JOIN ArticleDefTable AS Article ON DecompositionDetail.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleDefTable AS PlanArticle ON DecompositionDetail.PlanItemId = PlanArticle.ArticleId " _
                                      & " LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN tblProSteps ON DecompositionDetail.DepartmentId = tblProSteps.ProdStep_Id " _
                                      & " LEFT OUTER JOIN ArticleGroupDefTable AS ArticleAccount ON Article.ArticleGroupId = ArticleAccount.ArticleGroupId " _
                                      & " LEFT OUTER JOIN ArticleGroupDefTable AS PlanItemAccount ON PlanArticle.ArticleGroupId = PlanItemAccount.ArticleGroupId " _
                                      & " LEFT OUTER JOIN (SELECT Sum(Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End) AS Quantity, IsNull(EstimationDetail.ProductId, 0) AS ProductId, IsNull(EstimationDetail.ParentId, 0) AS ParentId, IsNull(EstimationDetail.PlanItemId, 0) AS PlanItemId, EstimationDetail.Tag#, EstimationDetail.ParentTag#, IsNull(Estimation.Id, 0) AS EstimationId, IsNull(Estimation.PlanTicketId, 0) AS TicketId, IsNull(Estimation.MasterPlanId, 0) AS PlanId, IsNull(EstimationDetail.SubDepartmentId, 0) AS DepartmentId FROM MaterialEstimation AS Estimation INNER JOIN MaterialEstimationDetailTable As EstimationDetail ON EstimationDetail.MaterialEstMasterID = Estimation.Id Group By IsNull(EstimationDetail.ProductId, 0), IsNull(EstimationDetail.ParentId, 0), IsNull(EstimationDetail.PlanItemId, 0), EstimationDetail.Tag#, EstimationDetail.ParentTag#, IsNull(Estimation.Id, 0), IsNull(Estimation.PlanTicketId, 0), IsNull(Estimation.MasterPlanId, 0), IsNull(EstimationDetail.SubDepartmentId, 0)) AS Estimation " _
                                      & " ON Estimation.ProductId = DecompositionDetail.ProductId AND Estimation.ParentId = DecompositionDetail.ParentId AND Estimation.DepartmentId = DecompositionDetail.DepartmentId AND Estimation.PlanItemId = DecompositionDetail.PlanItemId AND Estimation.Tag# = DecompositionDetail.Tag# AND Estimation.ParentTag# = DecompositionDetail.ParentTag# AND Estimation.EstimationId = Decomposition.EstimationId AND Estimation.TicketId = Decomposition.TicketId AND Estimation.PlanId = Decomposition.PlanId " _
                                      & " Left Outer Join(SELECT Sum(IsNull(DecompositionDetail.DecomposedQty, 0)+IsNull(DecompositionDetail.WastedQty, 0)+IsNull(DecompositionDetail.ScrappedQty, 0)) AS TotalDecomposedQty, IsNull(DecompositionDetail.ProductId, 0) AS ProductId, IsNull(DecompositionDetail.ParentId, 0) AS ParentId, IsNull(DecompositionDetail.PlanItemId, 0) AS PlanItemId, DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0) AS EstimationId, IsNull(Decomposition.TicketId, 0) AS TicketId, IsNull(Decomposition.PlanId, 0) AS PlanId, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, IsNull(DecompositionDetail.DepartmentId, 0) AS DepartmentId FROM MaterialDecompositionMaster AS Decomposition INNER JOIN MaterialDecompositionDetail AS DecompositionDetail ON Decomposition.DecompositionId = DecompositionDetail.DecompositionId Group By IsNull(DecompositionDetail.ProductId, 0), IsNull(DecompositionDetail.ParentId, 0), IsNull(DecompositionDetail.PlanItemId, 0), DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0), IsNull(Decomposition.TicketId, 0), IsNull(Decomposition.PlanId, 0), IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.Action, IsNull(DecompositionDetail.DepartmentId, 0)) As DecompositionQty ON Estimation.ProductId = DecompositionQty.ProductId AND Estimation.ParentId = DecompositionQty.ParentId AND Estimation.DepartmentId = DecompositionQty.DepartmentId AND Estimation.PlanItemId = DecompositionQty.PlanItemId AND Estimation.Tag# = DecompositionQty.Tag# AND Estimation.ParentTag# = DecompositionQty.ParentTag# AND Estimation.EstimationId = DecompositionQty.EstimationId AND Estimation.TicketId = DecompositionQty.TicketId AND Estimation.PlanId = DecompositionQty.PlanId " _
                                      & " LEFT OUTER JOIN (SELECT Count(ParentTag#) As ChildCount, ParentTag#, IsNull(EstimationId, 0) AS EstimationId From MaterialDecompositionDetail AS Detail INNER JOIN MaterialDecompositionMaster AS Decomposition ON Detail.DecompositionId = Decomposition.DecompositionId Where IsNull(Detail.StockImpact, 0) = 1 Group By EstimationId, ParentTag#) AS DecCount ON Estimation.Tag# = DecCount.ParentTag# AND Estimation.EstimationId = DecCount.EstimationId " _
                                      & " WHERE Decomposition.DecompositionId = " & DecompositionId & " Group By DecompositionDetail.PlanItemId, PlanArticle.ArticleDescription, DecompositionDetail.ProductId, DecompositionDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(DecompositionDetail.Price, 0), IsNull(DecompositionDetail.Tag#, 0), IsNull(DecompositionDetail.ParentTag#, 0), DecompositionDetail.DepartmentId, tblProSteps.prod_step, IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.DecompositionDetailId, DecompositionDetail.DecompositionId, DecompositionDetail.Action, IsNull(DecompositionDetail.StockImpact, 0), IsNull(ArticleAccount.SubSubId, 0), IsNull(PlanItemAccount.SubSubId, 0) "

            DAL = New MaterialDecompositionDAL()
            Dim table As DataTable = DAL.ReadTable(MasterQuery)
            'table.AcceptChanges()
            Return table
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Function DeleteMaster(ByVal MasterID As Integer, Optional ByVal DocumentNo As String = "", Optional ByVal VoucherId As Integer = 0)
        Try
            DAL = New MaterialDecompositionDAL()
            Dim MasterDetailQuery As String = "Delete from MaterialDecompositionDetail where DecompositionId=" & MasterID
            Dim MasterQuery As String = "Delete from MaterialDecompositionMaster where DecompositionId=" & MasterID
            Dim StockTransId1 As Integer = StockTransId(DocumentNo)
            DAL.Master_Deletion(MasterDetailQuery, MasterQuery, StockTransId1, , VoucherId)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Update_Transation(ByVal dt As DataTable, ByVal Obj As MaterialDecompositionModel, ByVal Source As String, ByVal MyCompanyId As Integer, ByVal WastedStockAccount As Integer, ByVal ScrappedStockAccount As Integer, ByVal VoucherId As Integer) As List(Of String)
        Dim DetailQueryList As List(Of String) = New List(Of String)
        Try
            DAL = New MaterialDecompositionDAL()
            Dim MasterQuery As String = "UPDATE  MaterialDecompositionMaster SET DecompositionDate = N'" & Obj.DecompositionDate.ToString("yyyy-M-d hh:mm:ss tt") & "', DocumentNo = N'" & Obj.DocumentNo.Replace("'", "''") & "', Remarks=N'" & Obj.Remarks.Replace("'", "''") & "' , CustomerId= " & Obj.CustomerId & ", SalesOrderId= " & Obj.SalesOrderId & ", PlanId= " & Obj.PlanId & " , TicketId= " & Obj.TicketId & " , EstimationId= " & Obj.EstimationId & " WHERE DecompositionId=" & Obj.DecompositionId
            Dim Detailquery As String
            For i As Integer = 0 To dt.Rows.Count - 1
                'If Val(dt.Rows(i).Item("DecomposedQty").ToString) > 0 Or Val(dt.Rows(i).Item("WastedQty").ToString) > 0 Or Val(dt.Rows(i).Item("ScrappedQty").ToString) > 0 Then
                Detailquery = "Insert Into MaterialDecompositionDetail(DecompositionId, EstimationDetailId, PlanItemId, ProductId, ParentId, Price, DecomposedQty, WastedQty, ScrappedQty, Tag#, ParentTag#, DepartmentId,  LocationId, UniqueId, ParentUniqueId, TotalConsumedQty, StockImpact) Values(`, " & Val(dt.Rows(i).Item("EstimationDetailId").ToString) & ", " & Val(dt.Rows(i).Item("PlanItemId").ToString) & ", " & Val(dt.Rows(i).Item("ProductId").ToString) & ", " & Val(dt.Rows(i).Item("ParentId").ToString) & ", " & Val(dt.Rows(i).Item("Price").ToString) & ", " & Val(dt.Rows(i).Item("DecomposedQty").ToString) & ", " & Val(dt.Rows(i).Item("WastedQty").ToString) & ", " & Val(dt.Rows(i).Item("ScrappedQty").ToString) & ", " & Val(dt.Rows(i).Item("Tag").ToString) & ", " & Val(dt.Rows(i).Item("ParentTag").ToString) & ", " & Val(dt.Rows(i).Item("DepartmentId").ToString) & ", " & Val(dt.Rows(i).Item("LocationId").ToString) & ", " & Val(dt.Rows(i).Item("UniqueId").ToString) & ", " & Val(dt.Rows(i).Item("ParentUniqueId").ToString) & ", " & Val(dt.Rows(i).Item("TotalConsumedQty").ToString) & ", " & Val(dt.Rows(i).Item("StockImpact").ToString) & ")"
                DetailQueryList.Add(Detailquery)
                'End If
            Next
            Dim Update_Detail_Del As String = "Delete from MaterialDecompositionDetail WHERE DecompositionId=" & Obj.DecompositionId
            Obj.StockMaster.StockTransId = StockTransId(Obj.DocumentNo)
            DAL.Master_Update(Obj.DecompositionId, MasterQuery, DetailQueryList, Update_Detail_Del, Obj.StockMaster, Obj, Source, dt, MyCompanyId, WastedStockAccount, ScrappedStockAccount, VoucherId)
            Return DetailQueryList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDamageLocation() As Integer
        Dim str As String = ""
        Dim LocationId As Integer = 0
        Try
            str = "SELECT Top 1 IsNull(location_id, 0) AS LocationId FROM tblDefLocation WHERE  location_type = 'Damage'"
            Dim dt As DataTable = DAL.ReadTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                LocationId = dt.Rows(0).Item(0)
            End If
            Return LocationId
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function StockTransId(ByVal DocNo As String) As Integer
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select StockTransId From StockMasterTable WHERE DocNo='" & DocNo & "'")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    Return Val(dt.Rows(0).Item(0).ToString)
                Else
                    Return 0I
                End If
            Else
                Return 0I
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetPlansItems(ByVal TicketId As Integer) As DataTable
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select ArticleDefId, Sum(IsNull(ProductionDetailTable.Qty, 0)-IsNull(Decomposition.Qty, 0)) As Qty FROM ProductionDetailTable INNER JOIN ProductionMasterTable ON ProductionDetailTable.Production_ID = ProductionMasterTable.Production_ID " _
                                         & " LEFT OUTER JOIN (SELECT ProductId, TicketId, Sum(IsNull(DecomposedQty, 0) + IsNull(WastedQty, 0) + IsNull(ScrappedQty, 0)) As Qty FROM MaterialDecompositionDetail AS Detail INNER JOIN  MaterialDecompositionMaster AS Decomposition ON Detail.DecompositionId = Decomposition.DecompositionId Where TicketId = " & TicketId & " Group By ProductId , TicketId) AS Decomposition ON ProductionMasterTable.PlanTicketId = Decomposition.TicketId AND ProductionDetailTable.ArticleDefId = Decomposition.ProductId  WHERE ProductionMasterTable.PlanTicketId = " & TicketId & " Group By ArticleDefId, PlanTicketId Having Sum(IsNull(ProductionDetailTable.Qty, 0)-IsNull(Decomposition.Qty, 0)) > 0 ")
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetParents(ByVal TicketId As Integer, ByVal EstimationId As Integer, ByVal ProductId As Integer, ByVal Qty As Double, ByVal LocationId As Integer, ByVal DecimalPointInQty As Integer) As DataTable
        Dim dtGetParents As New DataTable
        dtParents = New DataTable
        Dim strGetParents As String = ""

        'Dim MasterQuery As String = " Select 0 AS DecompositionDetailId, 0 AS DecompositionId, 0 AS EstimationDetailId, EstimationDetail.PlanItemId, PlanArticle.ArticleDescription As PlanItem, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription As Product, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(EstimationDetail.Price, 0) AS Price, " _
        '                               & "  Sum(Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End) AS Qty, Sum(IsNull(Decomposition.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(Decomposition.WastedQty, 0)) AS WastedQty, Sum(IsNull(Decomposition.ScrappedQty, 0)) AS ScrappedQty, Sum((Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End -(IsNull(Decomposition.DecomposedQty, 0)+IsNull(Decomposition.WastedQty, 0)+IsNull(Decomposition.ScrappedQty, 0)))) AS DecomposableQty, IsNull(EstimationDetail.Tag#, 0) AS Tag, IsNull(EstimationDetail.ParentTag#, 0) AS ParentTag, EstimationDetail.SubDepartmentId AS DepartmentId, tblProSteps.prod_step As Department, " & LocationId & " AS LocationId, Decomposition.Action, 0 As UniqueId, 0 As ParentUniqueId " _
        '                               & " FROM MaterialEstimationDetailTable As EstimationDetail INNER JOIN MaterialEstimation AS Estimation ON EstimationDetail.MaterialEstMasterID = Estimation.Id INNER JOIN ArticleDefTable AS Article ON EstimationDetail.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleDefTable AS PlanArticle ON EstimationDetail.PlanItemId = PlanArticle.ArticleId " _
        '                               & " INNER JOIN (SELECT IsNull(Production.PlanId, 0) As PlanId, IsNull(Production.PlanTicketId, 0) As TicketId FROM ProductionMasterTable AS Production INNER JOIN ProductionDetailTable AS ProductionDetail ON Production.Production_ID = ProductionDetail.Production_ID) AS Production ON Estimation.MasterPlanId = Production.PlanId AND Estimation.PlanTicketId = Production.TicketId " _
        '                               & " LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN tblProSteps ON EstimationDetail.SubDepartmentID = tblProSteps.ProdStep_Id " _
        '                               & " LEFT OUTER JOIN (SELECT Sum(IsNull(DecompositionDetail.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(DecompositionDetail.WastedQty, 0)) AS WastedQty, Sum(IsNull(DecompositionDetail.ScrappedQty, 0)) AS ScrappedQty, IsNull(DecompositionDetail.ProductId, 0) AS ProductId, IsNull(DecompositionDetail.PlanItemId, 0) AS PlanItemId, DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0) AS EstimationId, IsNull(Decomposition.TicketId, 0) AS TicketId, IsNull(Decomposition.PlanId, 0) AS PlanId, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, DecompositionDetail.Action AS Action FROM MaterialDecompositionMaster AS Decomposition INNER JOIN MaterialDecompositionDetail AS DecompositionDetail ON Decomposition.DecompositionId = DecompositionDetail.DecompositionId Group By IsNull(DecompositionDetail.ProductId, 0), IsNull(DecompositionDetail.PlanItemId, 0), DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0), IsNull(Decomposition.TicketId, 0), IsNull(Decomposition.PlanId, 0), IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.Action) AS Decomposition " _
        '                               & " ON EstimationDetail.ProductId = Decomposition.ProductId AND EstimationDetail.PlanItemId = Decomposition.PlanItemId AND EstimationDetail.Tag# = Decomposition.Tag# AND EstimationDetail.ParentTag# = Decomposition.ParentTag# AND Estimation.Id = Decomposition.EstimationId AND Estimation.PlanTicketId = Decomposition.TicketId AND Estimation.MasterPlanId = Decomposition.PlanId " _
        '                               & " WHERE Estimation.Id = " & EstimationId & " AND  Group By EstimationDetail.PlanItemId, PlanArticle.ArticleDescription, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(EstimationDetail.Price, 0), IsNull(EstimationDetail.Tag#, 0), IsNull(EstimationDetail.ParentTag#, 0), EstimationDetail.SubDepartmentId, tblProSteps.prod_step, IsNull(Decomposition.LocationId, 0), Decomposition.Action Having Sum(Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End) > Sum(IsNull(Decomposition.DecomposedQty, 0)+IsNull(Decomposition.WastedQty, 0)+IsNull(Decomposition.ScrappedQty, 0)) "

        Try
            ''Below lines are commented on 07-12-2017
            'strGetParents = " Select Top " & Qty & "  0 AS DecompositionDetailId, 0 AS DecompositionId, 0 AS EstimationDetailId, EstimationDetail.PlanItemId, PlanArticle.ArticleDescription As PlanItem, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription As Product, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(EstimationDetail.Price, 0) AS Price, " _
            '                               & "  Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) AS Qty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))) AS DecomposedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))) AS WastedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))) AS ScrappedQty, Sum((Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End -(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))+ Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))+Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))))) AS DecomposableQty, IsNull(EstimationDetail.Tag#, 0) AS Tag, IsNull(EstimationDetail.ParentTag#, 0) AS ParentTag, EstimationDetail.SubDepartmentId AS DepartmentId, tblProSteps.prod_step As Department, " & LocationId & " AS LocationId, Decomposition.Action, 0 As UniqueId, 0 As ParentUniqueId, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) AS TotalConsumedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))) AS TempDecQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))) AS TempWasQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))) AS TempScrQty, 0 As IsTopParent, 0 AS StockImpact " _
            '                               & " FROM MaterialEstimationDetailTable As EstimationDetail INNER JOIN MaterialEstimation AS Estimation ON EstimationDetail.MaterialEstMasterID = Estimation.Id INNER JOIN ArticleDefTable AS Article ON EstimationDetail.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleDefTable AS PlanArticle ON EstimationDetail.PlanItemId = PlanArticle.ArticleId " _
            '                               & " INNER JOIN (SELECT IsNull(Production.PlanId, 0) As PlanId, IsNull(Production.PlanTicketId, 0) As TicketId FROM ProductionMasterTable AS Production INNER JOIN ProductionDetailTable AS ProductionDetail ON Production.Production_ID = ProductionDetail.Production_ID) AS Production ON Estimation.MasterPlanId = Production.PlanId AND Estimation.PlanTicketId = Production.TicketId " _
            '                               & " LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN tblProSteps ON EstimationDetail.SubDepartmentID = tblProSteps.ProdStep_Id " _
            '                               & " LEFT OUTER JOIN (SELECT Sum(IsNull(DecompositionDetail.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(DecompositionDetail.WastedQty, 0)) AS WastedQty, Sum(IsNull(DecompositionDetail.ScrappedQty, 0)) AS ScrappedQty, IsNull(DecompositionDetail.ProductId, 0) AS ProductId, IsNull(DecompositionDetail.ParentId, 0) AS ParentId, IsNull(DecompositionDetail.PlanItemId, 0) AS PlanItemId, DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0) AS EstimationId, IsNull(Decomposition.TicketId, 0) AS TicketId, IsNull(Decomposition.PlanId, 0) AS PlanId, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, DecompositionDetail.Action AS Action, IsNull(DecompositionDetail.DepartmentId, 0) AS DepartmentId, Sum(IsNull(DecompositionDetail.TotalConsumedQty, 0)) AS TotalConsumedQty FROM MaterialDecompositionMaster AS Decomposition INNER JOIN MaterialDecompositionDetail AS DecompositionDetail ON Decomposition.DecompositionId = DecompositionDetail.DecompositionId Group By IsNull(DecompositionDetail.ProductId, 0), IsNull(DecompositionDetail.ParentId, 0), IsNull(DecompositionDetail.PlanItemId, 0), DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0), IsNull(Decomposition.TicketId, 0), IsNull(Decomposition.PlanId, 0), IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.Action, DecompositionDetail.DepartmentId) AS Decomposition " _
            '                               & " ON EstimationDetail.ProductId = Decomposition.ProductId AND EstimationDetail.ParentId = Decomposition.ParentId AND EstimationDetail.SubDepartmentId = Decomposition.DepartmentId AND EstimationDetail.PlanItemId = Decomposition.PlanItemId AND EstimationDetail.Tag# = Decomposition.Tag# AND EstimationDetail.ParentTag# = Decomposition.ParentTag# AND Estimation.Id = Decomposition.EstimationId AND Estimation.PlanTicketId = Decomposition.TicketId AND Estimation.MasterPlanId = Decomposition.PlanId " _
            '                               & " WHERE Estimation.Id = " & EstimationId & " AND Estimation.PlanTicketId = " & TicketId & " AND EstimationDetail.ProductId = " & ProductId & " Group By EstimationDetail.PlanItemId, PlanArticle.ArticleDescription, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(EstimationDetail.Price, 0), IsNull(EstimationDetail.Tag#, 0), IsNull(EstimationDetail.ParentTag#, 0), EstimationDetail.SubDepartmentId, tblProSteps.prod_step, IsNull(Decomposition.LocationId, 0), Decomposition.Action Having Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) > Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) "
            strGetParents = " Select Top " & Qty & "  0 AS DecompositionDetailId, 0 AS DecompositionId, 0 AS EstimationDetailId, EstimationDetail.PlanItemId, PlanArticle.ArticleDescription As PlanItem, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription As Product, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(EstimationDetail.Price, 0) AS Price, " _
                                        & "  Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) AS Qty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS DecomposedQty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS WastedQty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS ScrappedQty, Sum((Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End -(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))+ Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))+Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))))) AS DecomposableQty, IsNull(EstimationDetail.Tag#, 0) AS Tag, IsNull(EstimationDetail.ParentTag#, 0) AS ParentTag, EstimationDetail.SubDepartmentId AS DepartmentId, tblProSteps.prod_step As Department, " & LocationId & " AS LocationId, Decomposition.Action, 0 As UniqueId, 0 As ParentUniqueId, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) AS TotalConsumedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))) AS TempDecQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))) AS TempWasQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))) AS TempScrQty, 0 As IsTopParent, 0 AS StockImpact, Sum(IsNull(DecCount.ChildCount, 0)) AS ConsumedChildCount, IsNull(ArticleAccount.SubSubId, 0) AS SubSubId, IsNull(PlanItemAccount.SubSubId, 0) AS PlanItemSubSubId, 0 AS DValue, 0 AS WValue, 0 AS SValue " _
                                        & " FROM MaterialEstimationDetailTable As EstimationDetail INNER JOIN MaterialEstimation AS Estimation ON EstimationDetail.MaterialEstMasterID = Estimation.Id INNER JOIN ArticleDefTable AS Article ON EstimationDetail.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleDefTable AS PlanArticle ON EstimationDetail.PlanItemId = PlanArticle.ArticleId " _
                                        & " INNER JOIN (SELECT IsNull(Production.PlanId, 0) As PlanId, IsNull(Production.PlanTicketId, 0) As TicketId FROM ProductionMasterTable AS Production INNER JOIN ProductionDetailTable AS ProductionDetail ON Production.Production_ID = ProductionDetail.Production_ID) AS Production ON Estimation.MasterPlanId = Production.PlanId AND Estimation.PlanTicketId = Production.TicketId " _
                                        & " LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN tblProSteps ON EstimationDetail.SubDepartmentID = tblProSteps.ProdStep_Id " _
                                        & " LEFT OUTER JOIN ArticleGroupDefTable AS ArticleAccount ON Article.ArticleGroupId = ArticleAccount.ArticleGroupId " _
                                        & " LEFT OUTER JOIN ArticleGroupDefTable AS PlanItemAccount ON PlanArticle.ArticleGroupId = PlanItemAccount.ArticleGroupId " _
                                        & " LEFT OUTER JOIN (SELECT Sum(IsNull(DecompositionDetail.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(DecompositionDetail.WastedQty, 0)) AS WastedQty, Sum(IsNull(DecompositionDetail.ScrappedQty, 0)) AS ScrappedQty, IsNull(DecompositionDetail.ProductId, 0) AS ProductId, IsNull(DecompositionDetail.ParentId, 0) AS ParentId, IsNull(DecompositionDetail.PlanItemId, 0) AS PlanItemId, DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0) AS EstimationId, IsNull(Decomposition.TicketId, 0) AS TicketId, IsNull(Decomposition.PlanId, 0) AS PlanId, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, DecompositionDetail.Action AS Action, IsNull(DecompositionDetail.DepartmentId, 0) AS DepartmentId, Sum(IsNull(DecompositionDetail.TotalConsumedQty, 0)) AS TotalConsumedQty FROM MaterialDecompositionMaster AS Decomposition INNER JOIN MaterialDecompositionDetail AS DecompositionDetail ON Decomposition.DecompositionId = DecompositionDetail.DecompositionId Group By IsNull(DecompositionDetail.ProductId, 0), IsNull(DecompositionDetail.ParentId, 0), IsNull(DecompositionDetail.PlanItemId, 0), DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0), IsNull(Decomposition.TicketId, 0), IsNull(Decomposition.PlanId, 0), IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.Action, DecompositionDetail.DepartmentId) AS Decomposition " _
                                        & " ON EstimationDetail.ProductId = Decomposition.ProductId AND EstimationDetail.ParentId = Decomposition.ParentId AND EstimationDetail.SubDepartmentId = Decomposition.DepartmentId AND EstimationDetail.PlanItemId = Decomposition.PlanItemId AND EstimationDetail.Tag# = Decomposition.Tag# AND EstimationDetail.ParentTag# = Decomposition.ParentTag# AND Estimation.Id = Decomposition.EstimationId AND Estimation.PlanTicketId = Decomposition.TicketId AND Estimation.MasterPlanId = Decomposition.PlanId " _
                                        & " LEFT OUTER JOIN (SELECT Count(ParentTag#) As ChildCount, ParentTag#, IsNull(EstimationId, 0) AS EstimationId From MaterialDecompositionDetail AS Detail INNER JOIN MaterialDecompositionMaster AS Decomposition ON Detail.DecompositionId = Decomposition.DecompositionId Where IsNull(Detail.StockImpact, 0) = 1 Group By EstimationId, ParentTag#) AS DecCount ON EstimationDetail.Tag# = DecCount.ParentTag# AND EstimationDetail.MaterialEstMasterID = DecCount.EstimationId " _
                                        & " WHERE Estimation.Id = " & EstimationId & " AND Estimation.PlanTicketId = " & TicketId & " AND EstimationDetail.ProductId = " & ProductId & " Group By EstimationDetail.PlanItemId, PlanArticle.ArticleDescription, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(EstimationDetail.Price, 0), IsNull(EstimationDetail.Tag#, 0), IsNull(EstimationDetail.ParentTag#, 0), EstimationDetail.SubDepartmentId, tblProSteps.prod_step, IsNull(Decomposition.LocationId, 0), Decomposition.Action, IsNull(ArticleAccount.SubSubId, 0), IsNull(PlanItemAccount.SubSubId, 0) Having Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) > Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) "

            '& " WHERE Estimation.Id = " & EstimationId & " AND Estimation.PlanTicketId = " & TicketId & " AND EstimationDetail.ProductId = " & ProductId & " Group By EstimationDetail.PlanItemId, PlanArticle.ArticleDescription, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(EstimationDetail.Price, 0), IsNull(EstimationDetail.Tag#, 0), IsNull(EstimationDetail.ParentTag#, 0), EstimationDetail.SubDepartmentId, tblProSteps.prod_step, IsNull(Decomposition.LocationId, 0), Decomposition.Action Having Sum(Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End) > Sum(IsNull(Decomposition.DecomposedQty, 0)+IsNull(Decomposition.WastedQty, 0)+IsNull(Decomposition.ScrappedQty, 0)) "

            'strGetParents = " SELECT Top " & Qty & " ProductId, Tag#, SubDepartmentID, MaterialEstMasterID, ParentTag#, Sum(Quantity) As Qty FROM MaterialEstimationDetailTable AS Detail INNER JOIN MaterialEstimation AS Estimation ON Detail.MaterialEstMasterID = Estimation.Id " _
            '                & " LEFT OUTER JOIN  (SELECT Tag#, ProductId, ParentTag#, Sum(Qty) AS Qty, EstimationId, DepartmentId, TicketId From MaterialDecompositionDetail AS Detail INNER JOIN MaterialDecompositionMaster AS Decomposition ON Detail.DecompositionId = Decomposition.DecompositionId Group By Tag#, ProductId, ParentTag#, EstimationId, DepartmentId) AS Decomposition ON Detail.MaterialEstMasterID = Decomposition.EstimationId AND Decomposition.TicketId = Estimation.PlanTicketId AND Detail.SubDepartmentId = Decomposition.DepartmentId AND Detail.ProductId = Decomposition.ProductId AND Detail.ParentTag# = Decomposition.ParentTag# And Detail.Tag# = Decomposition.Tag#  Where Estimation.PlanTicketId = " & TicketId & " AND Detail.ProductId = " & ProductId & " AND Estimation.Id = " & EstimationId & " " _
            '                & " Group By ProductId, Tag#, SubDepartmentID, MaterialEstMasterID, ParentTag# Having Sum(EstimationDetail.Quantity) > Sum(Decomposition.Qty)"
            dtGetParents = UtilityDAL.GetDataTable(strGetParents)
            If dtGetParents.Rows.Count > 0 Then
                dtParents = dtGetParents.Clone()
                dtParents.Merge(dtGetParents)
            End If
            For Each Row As DataRow In dtGetParents.Rows
                GetChildRecursive(Row.Item("Tag"), LocationId, DecimalPointInQty)
            Next
            Return dtParents
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub GetChildRecursive(ByVal Tag As Integer, ByVal LocationId As Integer, ByVal DecimalPointInQty As Integer)
        Dim dtGetParents As New DataTable
        Dim strGetParents As String = ""
        ''To work here tomorrow

        ''Convert(Decimal(18, " & DecimalPointInQty & "), 0)
        Try
            'strGetParents = " SELECT ProductId, Tag#, SubDepartmentID, MaterialEstMasterID, ParentTag#, Sum(Quantity) As Qty FROM MaterialEstimationDetailTable AS Detail INNER JOIN MaterialEstimation AS Estimation ON Detail.MaterialEstMasterID = Estimation.Id " _
            '               & " LEFT OUTER JOIN  (SELECT Tag#, ProductId, ParentTag#, Sum(Qty) AS Qty, EstimationId, DepartmentId, TicketId From MaterialDecompositionDetail AS Detail INNER JOIN MaterialDecompositionMaster AS Decomposition ON Detail.DecompositionId = Decomposition.DecompositionId Group By Tag#, ProductId, ParentTag#, EstimationId, DepartmentId) AS Decomposition ON Detail.MaterialEstMasterID = Decomposition.EstimationId AND Decomposition.TicketId = Estimation.PlanTicketId AND Detail.SubDepartmentId = Decomposition.DepartmentId AND Detail.ProductId = Decomposition.ProductId AND Detail.ParentTag# = Decomposition.ParentTag# And Detail.Tag# = Decomposition.Tag#  Where Detail.ParentTag# = " & Tag & "  " _
            '               & " Group By ProductId, Tag#, SubDepartmentID, MaterialEstMasterID, ParentTag# Having Sum(EstimationDetail.Quantity) > Sum(Decomposition.Qty)"
            ''Below lines are commented on 07-12-2017
            'strGetParents = " Select  0 AS DecompositionDetailId, 0 AS DecompositionId, 0 AS EstimationDetailId, EstimationDetail.PlanItemId, PlanArticle.ArticleDescription As PlanItem, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription As Product, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(EstimationDetail.Price, 0) AS Price, " _
            '                          & "  Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) AS Qty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))) AS DecomposedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))) AS WastedQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))) AS ScrappedQty, Sum((Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End -(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))+Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))+Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))))) AS DecomposableQty, IsNull(EstimationDetail.Tag#, 0) AS Tag, IsNull(EstimationDetail.ParentTag#, 0) AS ParentTag, EstimationDetail.SubDepartmentId AS DepartmentId, tblProSteps.prod_step As Department, " & LocationId & " AS LocationId, Decomposition.Action, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) AS TotalConsumedQty, 0 As UniqueId, 0 As ParentUniqueId, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))) AS TempDecQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))) AS TempWasQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))) AS TempScrQty, 0 As IsTopParent, 0 AS StockImpact " _
            '                          & " FROM MaterialEstimationDetailTable As EstimationDetail INNER JOIN MaterialEstimation AS Estimation ON EstimationDetail.MaterialEstMasterID = Estimation.Id INNER JOIN ArticleDefTable AS Article ON EstimationDetail.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleDefTable AS PlanArticle ON EstimationDetail.PlanItemId = PlanArticle.ArticleId " _
            '                          & " INNER JOIN (SELECT IsNull(Production.PlanId, 0) As PlanId, IsNull(Production.PlanTicketId, 0) As TicketId FROM ProductionMasterTable AS Production INNER JOIN ProductionDetailTable AS ProductionDetail ON Production.Production_ID = ProductionDetail.Production_ID) AS Production ON Estimation.MasterPlanId = Production.PlanId AND Estimation.PlanTicketId = Production.TicketId " _
            '                          & " LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN tblProSteps ON EstimationDetail.SubDepartmentID = tblProSteps.ProdStep_Id " _
            '                          & " LEFT OUTER JOIN (SELECT Sum(IsNull(DecompositionDetail.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(DecompositionDetail.WastedQty, 0)) AS WastedQty, Sum(IsNull(DecompositionDetail.ScrappedQty, 0)) AS ScrappedQty, IsNull(DecompositionDetail.ProductId, 0) AS ProductId, IsNull(DecompositionDetail.ParentId, 0) AS ParentId, IsNull(DecompositionDetail.PlanItemId, 0) AS PlanItemId, DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0) AS EstimationId, IsNull(Decomposition.TicketId, 0) AS TicketId, IsNull(Decomposition.PlanId, 0) AS PlanId, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, DecompositionDetail.Action AS Action, IsNull(DecompositionDetail.DepartmentId, 0) AS DepartmentId, Sum(IsNull(DecompositionDetail.TotalConsumedQty, 0)) AS TotalConsumedQty FROM MaterialDecompositionMaster AS Decomposition INNER JOIN MaterialDecompositionDetail AS DecompositionDetail ON Decomposition.DecompositionId = DecompositionDetail.DecompositionId Group By IsNull(DecompositionDetail.ProductId, 0), IsNull(DecompositionDetail.ParentId, 0), IsNull(DecompositionDetail.PlanItemId, 0), DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0), IsNull(Decomposition.TicketId, 0), IsNull(Decomposition.PlanId, 0), IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.Action, IsNull(DecompositionDetail.DepartmentId, 0)) AS Decomposition " _
            '                          & " ON EstimationDetail.ProductId = Decomposition.ProductId AND EstimationDetail.ParentId = Decomposition.ParentId AND EstimationDetail.SubDepartmentId = Decomposition.DepartmentId AND EstimationDetail.PlanItemId = Decomposition.PlanItemId AND EstimationDetail.Tag# = Decomposition.Tag# AND EstimationDetail.ParentTag# = Decomposition.ParentTag# AND Estimation.Id = Decomposition.EstimationId AND Estimation.PlanTicketId = Decomposition.TicketId AND Estimation.MasterPlanId = Decomposition.PlanId " _
            '                          & " WHERE EstimationDetail.ParentTag# = " & Tag & "   Group By EstimationDetail.PlanItemId, PlanArticle.ArticleDescription, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(EstimationDetail.Price, 0), IsNull(EstimationDetail.Tag#, 0), IsNull(EstimationDetail.ParentTag#, 0), EstimationDetail.SubDepartmentId, tblProSteps.prod_step, IsNull(Decomposition.LocationId, 0), Decomposition.Action Having Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) > Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) "
            strGetParents = " Select  0 AS DecompositionDetailId, 0 AS DecompositionId, 0 AS EstimationDetailId, EstimationDetail.PlanItemId, PlanArticle.ArticleDescription As PlanItem, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription As Product, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(EstimationDetail.Price, 0) AS Price, " _
                                    & "  Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) AS Qty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS DecomposedQty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS WastedQty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS ScrappedQty, Sum((Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End -(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))+Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))+Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))))) AS DecomposableQty, IsNull(EstimationDetail.Tag#, 0) AS Tag, IsNull(EstimationDetail.ParentTag#, 0) AS ParentTag, EstimationDetail.SubDepartmentId AS DepartmentId, tblProSteps.prod_step As Department, " & LocationId & " AS LocationId, Decomposition.Action, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) AS TotalConsumedQty, 0 As UniqueId, 0 As ParentUniqueId, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.DecomposedQty, 0))) AS TempDecQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.WastedQty, 0))) AS TempWasQty, Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.ScrappedQty, 0))) AS TempScrQty, 0 As IsTopParent, 0 AS StockImpact, Sum(IsNull(DecCount.ChildCount, 0)) AS ConsumedChildCount, IsNull(ArticleAccount.SubSubId, 0) AS SubSubId, IsNull(PlanItemAccount.SubSubId, 0) AS PlanItemSubSubId, 0 AS DValue, 0 AS WValue, 0 AS SValue " _
                                    & " FROM MaterialEstimationDetailTable As EstimationDetail INNER JOIN MaterialEstimation AS Estimation ON EstimationDetail.MaterialEstMasterID = Estimation.Id INNER JOIN ArticleDefTable AS Article ON EstimationDetail.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleDefTable AS PlanArticle ON EstimationDetail.PlanItemId = PlanArticle.ArticleId " _
                                    & " INNER JOIN (SELECT IsNull(Production.PlanId, 0) As PlanId, IsNull(Production.PlanTicketId, 0) As TicketId FROM ProductionMasterTable AS Production INNER JOIN ProductionDetailTable AS ProductionDetail ON Production.Production_ID = ProductionDetail.Production_ID) AS Production ON Estimation.MasterPlanId = Production.PlanId AND Estimation.PlanTicketId = Production.TicketId " _
                                    & " LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  LEFT OUTER JOIN tblProSteps ON EstimationDetail.SubDepartmentID = tblProSteps.ProdStep_Id " _
                                    & " LEFT OUTER JOIN ArticleGroupDefTable AS ArticleAccount ON Article.ArticleGroupId = ArticleAccount.ArticleGroupId " _
                                    & " LEFT OUTER JOIN ArticleGroupDefTable AS PlanItemAccount ON PlanArticle.ArticleGroupId = PlanItemAccount.ArticleGroupId " _
                                    & " LEFT OUTER JOIN (SELECT Sum(IsNull(DecompositionDetail.DecomposedQty, 0)) AS DecomposedQty, Sum(IsNull(DecompositionDetail.WastedQty, 0)) AS WastedQty, Sum(IsNull(DecompositionDetail.ScrappedQty, 0)) AS ScrappedQty, IsNull(DecompositionDetail.ProductId, 0) AS ProductId, IsNull(DecompositionDetail.ParentId, 0) AS ParentId, IsNull(DecompositionDetail.PlanItemId, 0) AS PlanItemId, DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0) AS EstimationId, IsNull(Decomposition.TicketId, 0) AS TicketId, IsNull(Decomposition.PlanId, 0) AS PlanId, IsNull(DecompositionDetail.LocationId, 0) AS LocationId, DecompositionDetail.Action AS Action, IsNull(DecompositionDetail.DepartmentId, 0) AS DepartmentId, Sum(IsNull(DecompositionDetail.TotalConsumedQty, 0)) AS TotalConsumedQty FROM MaterialDecompositionMaster AS Decomposition INNER JOIN MaterialDecompositionDetail AS DecompositionDetail ON Decomposition.DecompositionId = DecompositionDetail.DecompositionId Group By IsNull(DecompositionDetail.ProductId, 0), IsNull(DecompositionDetail.ParentId, 0), IsNull(DecompositionDetail.PlanItemId, 0), DecompositionDetail.Tag#, DecompositionDetail.ParentTag#, IsNull(Decomposition.EstimationId, 0), IsNull(Decomposition.TicketId, 0), IsNull(Decomposition.PlanId, 0), IsNull(DecompositionDetail.LocationId, 0), DecompositionDetail.Action, IsNull(DecompositionDetail.DepartmentId, 0)) AS Decomposition " _
                                    & " ON EstimationDetail.ProductId = Decomposition.ProductId AND EstimationDetail.ParentId = Decomposition.ParentId AND EstimationDetail.SubDepartmentId = Decomposition.DepartmentId AND EstimationDetail.PlanItemId = Decomposition.PlanItemId AND EstimationDetail.Tag# = Decomposition.Tag# AND EstimationDetail.ParentTag# = Decomposition.ParentTag# AND Estimation.Id = Decomposition.EstimationId AND Estimation.PlanTicketId = Decomposition.TicketId AND Estimation.MasterPlanId = Decomposition.PlanId " _
                                    & " LEFT OUTER JOIN (SELECT Count(ParentTag#) As ChildCount, ParentTag#, IsNull(EstimationId, 0) AS EstimationId From MaterialDecompositionDetail AS Detail INNER JOIN MaterialDecompositionMaster AS Decomposition ON Detail.DecompositionId = Decomposition.DecompositionId Where IsNull(Detail.StockImpact, 0) = 1 Group By EstimationId, ParentTag#) AS DecCount ON EstimationDetail.Tag# = DecCount.ParentTag# AND Estimation.Id = DecCount.EstimationId " _
                                    & " WHERE EstimationDetail.ParentTag# = " & Tag & "   Group By EstimationDetail.PlanItemId, PlanArticle.ArticleDescription, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(EstimationDetail.Price, 0), IsNull(EstimationDetail.Tag#, 0), IsNull(EstimationDetail.ParentTag#, 0), EstimationDetail.SubDepartmentId, tblProSteps.prod_step, IsNull(Decomposition.LocationId, 0), Decomposition.Action, IsNull(ArticleAccount.SubSubId, 0), IsNull(PlanItemAccount.SubSubId, 0) Having Sum(Case When Types='Minus' Then -Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) else Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(EstimationDetail.Quantity, 0)) End) > Sum(Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Decomposition.TotalConsumedQty, 0))) "

            '& " WHERE EstimationDetail.ParentTag# = " & Tag & "   Group By EstimationDetail.PlanItemId, PlanArticle.ArticleDescription, EstimationDetail.ProductId, EstimationDetail.ParentId, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName, IsNull(EstimationDetail.Price, 0), IsNull(EstimationDetail.Tag#, 0), IsNull(EstimationDetail.ParentTag#, 0), EstimationDetail.SubDepartmentId, tblProSteps.prod_step, IsNull(Decomposition.LocationId, 0), Decomposition.Action Having Sum(Case When Types='Minus' Then -IsNull(EstimationDetail.Quantity, 0) else IsNull(EstimationDetail.Quantity, 0) End) > Sum(IsNull(Decomposition.DecomposedQty, 0)+IsNull(Decomposition.WastedQty, 0)+IsNull(Decomposition.ScrappedQty, 0)) "

            dtGetParents = UtilityDAL.GetDataTable(strGetParents)
            If dtGetParents.Rows.Count > 0 Then
                dtParents.Merge(dtGetParents)
                'If dtGetParents.Rows(0).Item("Tag") > 0 Then
                For Each row As DataRow In dtGetParents.Rows
                    If row.Item("Tag") > 0 Then
                        GetChildRecursive(row.Item("Tag"), LocationId, DecimalPointInQty)
                    End If
                Next
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetDecompositionItems(ByVal DecompositionId As Integer) As DataTable
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select Tag# As Tag, LocationId As LocationId FROM DecompositionDetailTable INNER JOIN DecompositionMasterTable ON DecompositionDetailTable.DecompositionId = DecompositionMasterTable.DecompositionId WHERE DecompositionMasterTable.DecompositionId = " & DecompositionId & " Group By ArticleDefId")
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ' ''' <summary>
    ' ''' TASK TFS1927
    ' ''' </summary>
    ' ''' <param name="Obj"></param>
    ' ''' <param name="Trans"></param>
    ' ''' <param name="Source"></param>
    ' ''' <param name="MyCompanyId"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function AddVoucher(ByVal Obj As ItemConsumptionMaster, ByVal Trans As SqlTransaction, ByVal Source As String, ByVal MyCompanyId As Integer) As Boolean
    '    Dim Query As String = ""
    '    Dim VoucherId As Integer = 0
    '    Try
    '        '[ConsumptionDetailId] [int] IDENTITY(1,1) NOT NULL,
    '        '[ConsumptionId] [int] NULL,
    '        '[ArticleId] [int] NULL,
    '        '[Qty] [float] NULL,
    '        '[Rate] [float] NULL,
    '        '[DispatchId] [int] NULL,
    '        '[DispatchDetailId] [int] NULL,

    '        Query = "  Insert Into tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, cheque_no, cheque_date, post, Source, voucher_code, Remarks) " _
    '                & " Values(" & MyCompanyId & ", 1, 1, '" & Obj.DocNo & "', '" & Obj.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', NULL, NULL, 1, N'" & Source & "', '" & Obj.DocNo & "', N'" & Obj.Remarks.Replace("'", "''") & "') Select @@Identity"
    '        VoucherId = SQLHelper.ExecuteScaler(Trans, CommandType.Text, Query)
    '        For Each Detail As ItemConsumptionDetail In Obj.Detail
    '            'Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments
    '            Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
    '              & " Values(" & VoucherId & ", " & Detail.LocationId & ", " & Obj.StoreIssuanceAccountId & ", 0, " & (Detail.Qty * Detail.Rate) & ", '" & Detail.Comments.Replace("'", "''") & "')"
    '            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
    '            Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
    '             & " Values(" & VoucherId & ", " & Detail.LocationId & ", " & Detail.CGSAccountId & ", " & (Detail.Qty * Detail.Rate) & ", 0, '" & Detail.Comments.Replace("'", "''") & "')"
    '            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
    '        Next
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    ' ''' <summary>
    ' ''' TASK TFS1927
    ' ''' </summary>
    ' ''' <param name="Obj"></param>
    ' ''' <param name="Trans"></param>
    ' ''' <param name="Source"></param>
    ' ''' <param name="MyCompanyId"></param>
    ' ''' <param name="VoucherId"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function UpdateVoucher(ByVal Obj As ItemConsumptionMaster, ByVal Trans As SqlTransaction, ByVal Source As String, ByVal MyCompanyId As Integer, ByVal VoucherId As Integer) As Boolean
    '    Dim Query As String = ""
    '    'Dim VoucherId As Integer = 0
    '    Try
    '        '[ConsumptionDetailId] [int] IDENTITY(1,1) NOT NULL,
    '        '[ConsumptionId] [int] NULL,
    '        '[ArticleId] [int] NULL,
    '        '[Qty] [float] NULL,
    '        '[Rate] [float] NULL,
    '        '[DispatchId] [int] NULL,
    '        '[DispatchDetailId] [int] NULL,
    '        Query = "  UPDATE tblVoucher SET location_id = " & MyCompanyId & ", finiancial_year_id = 1, voucher_type_id=1, voucher_no='" & Obj.DocNo & "', voucher_date='" & Obj.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', cheque_no=NULL, cheque_date=NULL, post=1, Source= N'" & Source & "', voucher_code='" & Obj.DocNo & "', Remarks=N'" & Obj.Remarks.Replace("'", "''") & "' Where voucher_id=" & VoucherId & ""
    '        SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
    '        Query = "Delete FROM tblVoucherDetail Where voucher_id = " & VoucherId & ""
    '        SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
    '        For Each Detail As ItemConsumptionDetail In Obj.Detail
    '            'Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments
    '            Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
    '              & " Values(" & VoucherId & ", " & Detail.LocationId & ", " & Obj.StoreIssuanceAccountId & ", 0, " & Detail.Qty * Detail.Rate & ", '" & Detail.Comments.Replace("'", "''") & "')"
    '            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
    '            Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
    '             & " Values(" & VoucherId & ", " & Detail.LocationId & ", " & Detail.CGSAccountId & ", " & Detail.Qty * Detail.Rate & ", 0, '" & Detail.Comments.Replace("'", "''") & "')"
    '            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
    '        Next
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    ' ''' <summary>
    ' ''' TASK TFS1927
    ' ''' </summary>
    ' ''' <param name="VoucherId"></param>
    ' ''' <param name="Trans"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function DeleteVoucher(ByVal VoucherId As Integer, ByVal Trans As SqlTransaction) As Boolean
    '    Dim Query As String = ""
    '    'Dim VoucherId As Integer = 0
    '    Try
    '        '[ConsumptionDetailId] [int] IDENTITY(1,1) NOT NULL,
    '        '[ConsumptionId] [int] NULL,
    '        '[ArticleId] [int] NULL,
    '        '[Qty] [float] NULL,
    '        '[Rate] [float] NULL,
    '        '[DispatchId] [int] NULL,
    '        '[DispatchDetailId] [int] NULL,
    '        Query = "  Delete From tblVoucherDetail Where voucher_id =" & VoucherId & " "
    '        SQLHelper.ExecuteScaler(Trans, CommandType.Text, Query)
    '        Query = "  Delete From tblVoucher Where voucher_id =" & VoucherId & " "
    '        SQLHelper.ExecuteScaler(Trans, CommandType.Text, Query)
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
End Class


