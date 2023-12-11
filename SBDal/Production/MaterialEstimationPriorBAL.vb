Imports Janus.Windows.GridEX
'Imports SBModel.MaterialEstimationModel
Imports SBModel

Public Class MaterialEstimationPriorBAL

    Dim DAL As MaterialEstimationPriorDAL


    Public Function Save_Transation(ByVal dt As DataTable, ByVal matEstObj As MaterialEstimationModel) As List(Of String)
        Try
            Dim DetailQueryList As List(Of String) = New List(Of String)
            DAL = New MaterialEstimationPriorDAL()
            'Dim MasterQuery As String = "insert into MaterialEstimation (EstimationDate, MasterPlanId, PlanTicketId, Remarks, DocNo, SaleOrderId, PlanItemId) values(N'" & matEstObj.EstimationDate.ToString("yyyy-M-d hh:mm:ss tt") & "', " & matEstObj.MasterPlanId & " , " & matEstObj.PlanTicketId & " , N'" & matEstObj.Remarks & "', N'" & matEstObj.DocNo & "', " & matEstObj.SaleOrderId & ", " & matEstObj.PlanItemId & ")Select @@Identity"
            Dim MasterQuery As String = "insert into MaterialEstimation(EstimationDate, MasterPlanId, PlanTicketId, Remarks, DocNo, SaleOrderId, PlanItemId) values(N'" & matEstObj.EstimationDate.ToString("yyyy-M-d hh:mm:ss tt") & "', " & matEstObj.MasterPlanId & " , " & matEstObj.PlanTicketId & " , N'" & matEstObj.Remarks & "', N'" & matEstObj.DocNo & "', " & matEstObj.SaleOrderId & ", " & matEstObj.PlanItemId & ")Select @@Identity"
            Dim Detailquery As String
            For i As Integer = 0 To dt.Rows.Count - 1
                'S MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve
                Detailquery = "insert into MaterialEstimationDetailTable(MaterialEstMasterID ,Quantity, Types, Approve, PlanItemId, ProductId, CostSheetID, SubDepartmentID, Tag#, Price, ParentId, SerialNo, ParentSerialNo) values(`, " & Val(dt.Rows(i).Item("Qty").ToString) & ", N'" & dt.Rows(i).Item("Types").ToString.Replace("'", "''") & "', " & IIf(dt.Rows(i).Item("Approve") = False, 0, 1) & ", " & Val(dt.Rows(i).Item("MasterArticleID").ToString) & ", " & Val(dt.Rows(i).Item("ArticleID").ToString) & ", " & Val(dt.Rows(i).Item("CostSheetID").ToString) & ", " & Val(dt.Rows(i).Item("SubDepartmentID").ToString) & ", " & Val(dt.Rows(i).Item("Tag#").ToString) & ", " & Val(dt.Rows(i).Item("Price").ToString) & ", " & Val(dt.Rows(i).Item("ParentId").ToString) & ", N'" & dt.Rows(i).Item("SerialNo").ToString.Replace("'", "''") & "', N'" & dt.Rows(i).Item("ParentSerialNo").ToString.Replace("'", "''") & "')"
                DetailQueryList.Add(Detailquery)
            Next
            DAL.Master_Insertion(MasterQuery, DetailQueryList)
            Return DetailQueryList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function GetMaster() As DataTable
    '    Try
    '        Dim MasterQuery As String = "SELECT     ME.Id, som.SalesOrderNo, som.SalesOrderDate, pm.PlanNo, pm.PlanDate, pt.TicketNo, ME.DocNo, ME.EstimationDate, ME.MasterPlanId, ME.PlanTicketId, ME.Remarks, ME.SaleOrderId,ME.PlanItemId " _
    '                                & " FROM         MaterialEstimation AS ME LEFT OUTER JOIN" _
    '                                & " PlanTickets AS pt ON ME.PlanTicketId = pt.PlanTicketsId LEFT OUTER JOIN" _
    '                                & " PlanMasterTable AS pm ON ME.MasterPlanId = pm.PlanId LEFT OUTER JOIN" _
    '                                & " SalesOrderMasterTable AS som ON som.SalesOrderId = ME.SaleOrderId" _
    '                                & " ORDER BY ME.Id DESC"
    '        DAL = New MaterialEstimationPriorDAL()
    '        Dim table = DAL.ReadTable(MasterQuery)
    '        Return table
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Public Function GetMaster() As DataTable
        Try
            Dim MasterQuery As String = "SELECT ME.Id, ME.DocNo, ME.EstimationDate, pt.TicketNo, pm.PlanNo, pm.PlanDate, som.SalesOrderNo, som.SalesOrderDate, ME.MasterPlanId, ME.PlanTicketId, ME.Remarks, ME.SaleOrderId,ME.PlanItemId " _
                                    & " FROM MaterialEstimation AS ME LEFT OUTER JOIN" _
                                    & " PlanTickets AS pt ON ME.PlanTicketId = pt.PlanTicketsId LEFT OUTER JOIN" _
                                    & " PlanMasterTable AS pm ON ME.MasterPlanId = pm.PlanId LEFT OUTER JOIN" _
                                    & " SalesOrderMasterTable AS som ON som.SalesOrderId = ME.SaleOrderId" _
                                    & " ORDER BY ME.Id DESC"
            DAL = New MaterialEstimationPriorDAL()
            Dim table = DAL.ReadTable(MasterQuery)
            Return table
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCostSheetItems(ByVal PlanItemId As Integer, ByVal Quantity As Double) As DataTable
        Dim ArticleMasterId As Integer = 0
        Try

            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, Case When IsNull(MED.Quantity, 0) > 0 Then MED.Quantity Else IsNull(CS.Qty, 0) End As Qty, MED.Types, IsNull(MED.Approve, 0) As Approve, Ticket.PlanTicketsId, CS.CostSheetID from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Inner Join PlanTickets Ticket On ME.PlanTicketId = Ticket.PlanTicketsId Right Outer Join tblCostSheet CS  ON MED.CostSheetID = CS.CostSheetID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & PlanItemId & ""
            Dim MasterQuery As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, Article.ArticleDescription, MasterItem.PlanItem, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Quantity & " As Qty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID, IsNull(CS.ParentId, 0) As ParentId, CS.SerialNo, CS.ParentSerialNo from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTableMaster) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId  LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Where CS.MasterArticleID = " & PlanItemId & ""



            DAL = New MaterialEstimationPriorDAL()
            Dim table As DataTable = DAL.ReadTable(MasterQuery)
            'Dim dtMerged As New DataTable
            'If table.Rows.Count > 0 Then
            '    For Each Dr As DataRow In table.Rows
            '        Dim dtChild As New DataTable
            '        'Dim ChildQuery As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Val(Dr.Item("Qty").ToString) & " As Qty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTableMaster) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId  LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id  Where CS.MasterArticleID = " & Dr.Item("ArticleID") & ""
            '        Dim ChildQuery As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Val(Dr.Item("Qty").ToString) & " As Qty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID, M.MasterID  from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTableMaster) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId  LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Inner Join (Select IsNull(MasterID, 0) AS MasterID FROM ArticleDefTable Where ArticleId = " & Dr.Item("ArticleID") & " ) As M ON  CS.MasterArticleID = M.MasterID "
            '        dtChild = UtilityDAL.GetDataTable(ChildQuery)
            '        If dtChild.Rows.Count > 0 Then
            '            dtMerged.Merge(dtChild)
            '            For Each Dr1 As DataRow In dtChild.Rows
            '                Dim dt3 As New DataTable
            '                'Dim Str3 As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Val(Dr1.Item("Qty").ToString) & " As Qty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTableMaster) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId  LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Where CS.MasterArticleID = " & Dr1.Item("ArticleID") & ""
            '                Dim Str3 As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Val(Dr1.Item("Qty").ToString) & " As Qty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID, M.MasterID from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTableMaster) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId  LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Inner Join (Select IsNull(MasterID, 0) AS MasterID FROM ArticleDefTable Where ArticleId = " & Dr1.Item("ArticleID") & " ) As M ON  CS.MasterArticleID = M.MasterID "
            '                dt3 = UtilityDAL.GetDataTable(Str3)
            '                If dt3.Rows.Count > 0 Then
            '                    dtMerged.Merge(dt3)
            '                End If
            '            Next
            '        End If
            '    Next
            'End If
            ''table.AcceptChanges()
            'If dtMerged.Rows.Count > 0 Then
            '    table.Merge(dtMerged)
            'End If
            table.AcceptChanges()
            Return table
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function PlanCostSheetItems(ByVal PlanItemId As Integer, ByVal Quantity As Double) As DataTable
        Try

            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, Case When IsNull(MED.Quantity, 0) > 0 Then MED.Quantity Else IsNull(CS.Qty, 0) End As Qty, MED.Types, IsNull(MED.Approve, 0) As Approve, Ticket.PlanTicketsId, CS.CostSheetID from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Inner Join PlanTickets Ticket On ME.PlanTicketId = Ticket.PlanTicketsId Right Outer Join tblCostSheet CS  ON MED.CostSheetID = CS.CostSheetID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & PlanItemId & ""
            Dim MasterQuery As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Quantity & " As Qty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID, IsNull(CS.ParentId, 0) As ParentId from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTableMaster) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Where CS.MasterArticleID = " & PlanItemId & ""
            DAL = New MaterialEstimationPriorDAL()
            Dim table As DataTable = DAL.ReadTable(MasterQuery)
            'table.AcceptChanges()
            Return table
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function PlanCostSheetItemsForIssuence(ByVal PlanItemId As Integer, ByVal Quantity As Double) As DataTable
        Try

            'Str = "SELECT 1 As LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, '' As BatchNo , '' AS unit, IsNull(Recv_D.Quantity, 0) AS Qty, 0 as Rate, " _
            ' & " 0 AS Total, " _
            ' & " Article.ArticleGroupId as CategoryId, Recv_D.ProductID as ItemId, 0 As PackQty, 0 As CurrentPrice, 0 As BatchID, Isnull(Recv_D.ProductMasterID, 0) as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster], '' As Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, 0 as CostPrice, '' As PlanUnit, 0 As PlanQty, '' as LotNo, '' As Rack_No, Recv_D.Remarks As Comments, IsNull(Stock.CurrStock,0) as Stock, IsNull(Recv_D.Quantity, 0) As TotalQty  FROM AllocationDetail Recv_D INNER JOIN AllocationMaster ON Recv_D.Master_Allocation_ID = AllocationMaster.Master_Allocation_ID LEFT OUTER JOIN " _
            ' & " dbo.ArticleDefTable Article ON Recv_D.ProductID = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
            ' & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ProductMasterID LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ProductID " _
            '& " Where AllocationMaster.TicketID =" & ReceivingID & ""
            Dim MasterQuery As String = " Select 1 As LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName as Color, '' As BatchNo, '' AS unit, IsNull(Recv_D.Qty, 0) * " & Quantity & " AS Qty,  0 as Rate, 0 AS Total, " _
             & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleID as ItemId, 0 As PackQty, 0 As CurrentPrice, 0 As BatchID, Isnull(Recv_D.MasterArticleID, 0) as ArticleDefMasterId, ArticleDefTableMaster.ArticleDescription as [ArticleDescriptionMaster], '' As Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Article_Group.CGSAccountId, IsNull(Recv_D.CostPrice, 0) As CostPrice, '' As PlanUnit, 0 As PlanQty, '' as LotNo, '' As Rack_No, Recv_D.Remarks As Comments, IsNull(Stock.CurrStock,0) as Stock, IsNull(Recv_D.Qty, 0) * " & Quantity & " As TotalQty,  IsNull(tblProSteps.ProdStep_Id, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, 0 As AllocationDetailId, IsNull(Recv_D.ParentId, 0) As ParentId FROM tblCostSheet Recv_D LEFT OUTER JOIN " _
             & " dbo.ArticleDefTable Article ON Recv_D.ArticleID = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId LEFT OUTER JOIN " _
             & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.MasterArticleID LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleID LEFT JOIN tblProSteps ON Recv_D.SubDepartmentID = tblProSteps.ProdStep_Id" _
            & " Where Recv_D.MasterArticleID =" & PlanItemId & ""

            DAL = New MaterialEstimationPriorDAL()
            Dim table As DataTable = DAL.ReadTable(MasterQuery)
            'table.AcceptChanges()
            Return table
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function GetDetails(ByVal MasterID As Integer) As DataTable
        Try

            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, MED.PlanItemId As MasterArticleID, MED.ProductId As ArticleID, Article.ArticleDescription, MasterItem.PlanItem, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, Case When IsNull(MED.Quantity, 0) > 0 Then MED.Quantity Else IsNull(CS.Qty, 0) End As Qty, IsNull(MED.Price, 0) As Price, MED.Types, MED.Tag#, IsNull(MED.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, IsNull(MED.Approve, 0) As Approve, Ticket.PlanTicketsId, CS.CostSheetID, IsNull(MED.ParentId, 0) As ParentId, MED.SerialNo, MED.ParentSerialNo from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Left Join PlanTickets Ticket On ME.PlanTicketId = Ticket.PlanTicketsId Left Outer Join tblCostSheet CS  ON MED.CostSheetID = CS.CostSheetID Inner Join ArticleDefTable Article On MED.ProductId = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTableMaster ) As MasterItem On MED.PlanItemId = MasterItem.ArticleId LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Where MED.MaterialEstMasterID = " & MasterID & ""
            DAL = New MaterialEstimationPriorDAL()
            Dim table As DataTable = DAL.ReadTable(MasterQuery)
            'table.AcceptChanges()
            Return table
        Catch ex As Exception
            Throw ex
        End Try

    End Function



    Public Function DeleteMaster(ByVal MasterID As Integer)
        DAL = New MaterialEstimationPriorDAL()
        Dim MasterDetailQuery As String = "Delete from MaterialEstimationDetailTable where MaterialEstMasterID=" & MasterID
        Dim MasterQuery As String = "Delete from MaterialEstimation where Id=" & MasterID
        DAL.Master_Deletion(MasterDetailQuery, MasterQuery)
        Return True
    End Function
    Public Function GetDetailDS(ByVal mArticleId As Integer, ByVal planId As Integer) As DataSet
        Dim ds As New DataSet
        Dim dtM As New DataTable
        Dim dtD As New DataTable
        Try

            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, Article.ArticleId As PlanItemId, Article.ArticleDescription As PlanItem, Ticket.PlanId, Ticket.TicketQuantity, Ticket.ProductionStartDate As ProductionDate, MED.Types, MED.Approve from PlanTickets Ticket Left Outer Join MaterialEstimation ME On Ticket.PlanTicketsId = ME.PlanTicketId Inner Join MaterialEstimationDetailTable MED.MaterialEstMasterID On ME.Id = MED.MaterialEstMasterID Inner Join ArticleDefTable Article ON  Ticket.ArticleId = Article.ArticleId Where Ticket.ArticleId = " & mArticleId & " And  Ticket.PlanId = " & planId & ""
            Dim DetailQuery As String = " Select CostSheet.ArticleID, CostSheet.MasterArticleID, CostSheet.Qty, Article.ArticleDescription As [Raw Material] From tblCostSheet As CostSheet Inner Join ArticleDefTable As Article On CostSheet.ArticleId = Article.ArticleId "
            dtM.TableName = "PlanItem"
            dtD.TableName = "RawMaterial"
            DAL = New MaterialEstimationPriorDAL()
            dtM = DAL.ReadTable(MasterQuery)
            dtD = DAL.ReadTable(DetailQuery)
            ds.DataSetName = "RawMaterialList"
            ds.Tables.Add(dtM)
            ds.Tables.Add(dtD)
            ds.Relations.Add("MaterialEstimation", dtM.Columns(2), dtD.Columns(1))
            'dtM.AcceptChanges()
            Return ds
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Update_Transation(ByVal dt As DataTable, ByVal matEstObj As MaterialEstimationModel) As List(Of String)
        Dim DetailQueryList As List(Of String) = New List(Of String)
        DAL = New MaterialEstimationPriorDAL()
        'Dim MasterQuery As String = "insert into MaterialEstimation (EstimationDate, MasterPlanId, PlanTicketId, Remarks, DocNo, SaleOrderId, PlanItemId) values(N'" & matEstObj.EstimationDate.ToString("yyyy-M-d hh:mm:ss tt") & "', " & matEstObj.MasterPlanId & " , " & matEstObj.PlanTicketId & " , N'" & matEstObj.Remarks & "', N'" & matEstObj.DocNo & "', " & matEstObj.SaleOrderId & ", " & matEstObj.PlanItemId & ")Select @@Identity"

        Dim MasterQuery As String = "update  MaterialEstimation set EstimationDate = N'" & matEstObj.EstimationDate.ToString("yyyy-M-d hh:mm:ss tt") & "', MasterPlanId = " & matEstObj.MasterPlanId & ", PlanTicketId= " & matEstObj.PlanTicketId & ", Remarks=N'" & matEstObj.Remarks & "' , docno=N'" & matEstObj.DocNo & "',saleorderId= " & matEstObj.SaleOrderId & ", PlanItemId= " & matEstObj.PlanItemId & " where Id=" & matEstObj.Id
        Dim Detailquery As String
        'For i As Integer = 0 To grid.RowCount
        '    Detailquery = "insert into MaterialEstimationDetailTable (MaterialEstMasterID ,Quantity, Types ,Approve , PlanItemId , ProductId) values(`,'" & grid.GetRow(i).Cells(3).Value.ToString() & "','" & grid.GetRow(i).Cells(3).Value.ToString() & "','" & grid.GetRow(i).Cells(3).Value.ToString() & "','" & grid.GetRow(i).Cells(3).Value.ToString() & "','" & grid.GetRow(i).Cells(3).Value.ToString() & "' )"
        '    DetailQueryList.Add(Detailquery)
        'Next
        For i As Integer = 0 To dt.Rows.Count - 1
            'S MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve
            Detailquery = "insert into MaterialEstimationDetailTable(MaterialEstMasterID ,Quantity, Types, Approve, PlanItemId, ProductId, CostSheetID, SubDepartmentID, Tag#, Price, ParentId, SerialNo, ParentSerialNo) values(`, " & Val(dt.Rows(i).Item("Qty").ToString) & ", N'" & dt.Rows(i).Item("Types").ToString.Replace("'", "''") & "', " & IIf(dt.Rows(i).Item("Approve") = False, 0, 1) & ", " & Val(dt.Rows(i).Item("MasterArticleID").ToString) & ", " & Val(dt.Rows(i).Item("ArticleID").ToString) & ", " & Val(dt.Rows(i).Item("CostSheetID").ToString) & ", " & Val(dt.Rows(i).Item("SubDepartmentID").ToString) & ", " & Val(dt.Rows(i).Item("Tag#").ToString) & ", " & Val(dt.Rows(i).Item("Price").ToString) & ", " & Val(dt.Rows(i).Item("ParentId").ToString) & ", N'" & dt.Rows(i).Item("SerialNo").ToString.Replace("'", "''") & "', N'" & dt.Rows(i).Item("ParentSerialNo").ToString.Replace("'", "''") & "')"
            DetailQueryList.Add(Detailquery)
        Next
        Dim Update_Detail_Del As String = "Delete from MaterialEstimationDetailTable where MaterialEstMasterID=" & matEstObj.Id

        DAL.Master_Update(matEstObj.Id, MasterQuery, DetailQueryList, Update_Detail_Del)

        Return DetailQueryList
    End Function

End Class
