''TASK:1006 Added TagIds to estimation.
''TASK:TFS1141 inclusion of pending estimation qty comparing with issuance to GetDetail function.
''TASK: TFS1272 Get consumed quantity to be checked on estimation. Done on 09-08-2017 by Ameen
Imports Janus.Windows.GridEX
Imports SBModel
Imports System.Data
Imports System.Linq
Public Class MaterialEstimationBAL

    Dim DAL As MaterialEstimationDAL
    Public Clone As Boolean = False
    Public dtAllItems As DataTable
    Public UniqueId As Integer = 0
    Public dtGetCostSheet As New DataTable
    Public dtEstimationConsumption As DataTable
    Public CounTag As Integer = 0


    Public Function Save_Transation(ByVal dt As DataTable, ByVal matEstObj As MaterialEstimationModel) As List(Of String)
        Try
            Dim DetailQueryList As List(Of String) = New List(Of String)
            DAL = New MaterialEstimationDAL()
            'Dim MasterQuery As String = "insert into MaterialEstimation (EstimationDate, MasterPlanId, PlanTicketId, Remarks, DocNo, SaleOrderId, PlanItemId) values(N'" & matEstObj.EstimationDate.ToString("yyyy-M-d hh:mm:ss tt") & "', " & matEstObj.MasterPlanId & " , " & matEstObj.PlanTicketId & " , N'" & matEstObj.Remarks & "', N'" & matEstObj.DocNo & "', " & matEstObj.SaleOrderId & ", " & matEstObj.PlanItemId & ")Select @@Identity"
            Dim MasterQuery As String = "insert into MaterialEstimation(EstimationDate, MasterPlanId, PlanTicketId, Remarks, DocNo, SaleOrderId, PlanItemId) values(N'" & matEstObj.EstimationDate.ToString("yyyy-M-d hh:mm:ss tt") & "', " & matEstObj.MasterPlanId & " , " & matEstObj.PlanTicketId & " , N'" & matEstObj.Remarks & "', N'" & matEstObj.DocNo & "', " & matEstObj.SaleOrderId & ", " & matEstObj.PlanItemId & ")Select @@Identity"
            Dim Detailquery As String
            For i As Integer = 0 To dt.Rows.Count - 1
                'S MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve
                ''TASK:1006 Added TagIds to estimation.
                Detailquery = "insert into MaterialEstimationDetailTable(MaterialEstMasterID ,Quantity, Types, Approve, PlanItemId, ProductId, CostSheetID, SubDepartmentID, Tag#, Price, ParentId, SerialNo, ParentSerialNo, TotalQty, UniqueId, ParentUniqueId, ParentTag#) values(`, " & Val(dt.Rows(i).Item("Qty").ToString) & ", N'" & dt.Rows(i).Item("Types").ToString.Replace("'", "''") & "', " & IIf(dt.Rows(i).Item("Approve") = False, 0, 1) & ", " & Val(dt.Rows(i).Item("MasterArticleID").ToString) & ", '" & dt.Rows(i).Item("ArticleID").ToString & "', " & Val(dt.Rows(i).Item("CostSheetID").ToString) & ", " & Val(dt.Rows(i).Item("SubDepartmentID").ToString) & ", N'" & dt.Rows(i).Item("Tag#").ToString.Replace("'", "''") & "', " & Val(dt.Rows(i).Item("Price").ToString) & ", '" & dt.Rows(i).Item("ParentId").ToString & "', N'" & dt.Rows(i).Item("SerialNo").ToString.Replace("'", "''") & "', N'" & dt.Rows(i).Item("ParentSerialNo").ToString.Replace("'", "''") & "', " & Val(dt.Rows(i).Item("TotalQty").ToString) & ", " & Val(dt.Rows(i).Item("UniqueId").ToString) & ", " & Val(dt.Rows(i).Item("ParentUniqueId").ToString) & ", " & Val(dt.Rows(i).Item("ParentTag#").ToString) & ")"
                ''TASK:1006 Added TagIds to estimation.
                DetailQueryList.Add(Detailquery)
            Next
            DAL.Master_Insertion(MasterQuery, DetailQueryList)
            Return DetailQueryList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetMaster() As DataTable
        Try
            Dim MasterQuery As String = "SELECT ME.Id, ME.DocNo, ME.EstimationDate, pt.TicketNo, pm.PlanNo, pm.PlanDate, som.SalesOrderNo, som.SalesOrderDate, ME.MasterPlanId, ME.PlanTicketId, ME.Remarks, ME.SaleOrderId,ME.PlanItemId " _
                                    & " FROM MaterialEstimation AS ME LEFT OUTER JOIN" _
                                    & " PlanTickets AS pt ON ME.PlanTicketId = pt.PlanTicketsId LEFT OUTER JOIN" _
                                    & " PlanMasterTable AS pm ON ME.MasterPlanId = pm.PlanId LEFT OUTER JOIN" _
                                    & " SalesOrderMasterTable AS som ON som.SalesOrderId = ME.SaleOrderId" _
                                    & " ORDER BY ME.Id DESC"
            DAL = New MaterialEstimationDAL()
            Dim table = DAL.ReadTable(MasterQuery)
            Return table
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCostSheetItems(ByVal PlanItemId As String, ByVal Quantity As Double, ByVal SetParentId As String, ByVal IsParent As Boolean, ByVal UniqueId As Integer, ByVal ParentUniqueId As Integer, Optional ByVal IsMulQty As Boolean = False, Optional ByVal AppendId As Integer = 0) As DataTable
        Dim ArticleMasterId As Integer = 0
        Try

            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, Case When IsNull(MED.Quantity, 0) > 0 Then MED.Quantity Else IsNull(CS.Qty, 0) End As Qty, MED.Types, IsNull(MED.Approve, 0) As Approve, Ticket.PlanTicketsId, CS.CostSheetID from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Inner Join PlanTickets Ticket On ME.PlanTicketId = Ticket.PlanTicketsId Right Outer Join tblCostSheet CS  ON MED.CostSheetID = CS.CostSheetID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & PlanItemId & ""
            'Dim MasterQuery As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, " & IIf(AppendId = 0, "Convert(nvarchar, CS.ArticleID) As ArticleId", "Convert(nvarchar, CS.ArticleID) +'-'+ Convert(nvarchar, " & AppendId & ") As ArticleId") & " , MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Quantity & " As Qty, IsNull(CS.Total_Qty, 0) * " & Quantity & " As TotalQty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID,  '" & SetParentId & "' As ParentId, CS.SerialNo, CS.ParentSerialNo, " & Quantity & " As ParentQty from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable Where ArticleId = " & PlanItemId & ") As MasterItem On CS.ParentId = MasterItem.ArticleId  LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Where " & IIf(IsParent = True, "CS.ArticleId = " & PlanItemId & "", " CS.ParentId = " & PlanItemId & "") & ""
            Dim MasterQuery As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, Convert(nvarchar, CS.ArticleID) As ArticleId, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Quantity & " As Qty, IsNull(CS.Total_Qty, 0) * " & Quantity & " As TotalQty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID,  Convert(nvarchar, CS.ParentId) As ParentId, CS.SerialNo, CS.ParentSerialNo, " & Quantity & " As ParentQty, " & UniqueId & " As UniqueId, " & ParentUniqueId & " As ParentUniqueId  from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable Where ArticleId = " & PlanItemId & ") As MasterItem On CS.ParentId = MasterItem.ArticleId  LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Where " & IIf(IsParent = True, "CS.ArticleId = " & PlanItemId & "", " CS.ParentId = " & PlanItemId & "") & ""
            DAL = New MaterialEstimationDAL()
            Dim table As DataTable = DAL.ReadTable(MasterQuery)
            Dim dtEdited As DataTable = table
            If Clone = False Then
                dtAllItems = table.Clone()
                Clone = True
            End If
            If dtEdited.Rows.Count > 0 Then
                For Each dr As DataRow In dtEdited.Rows
                    'If GetCostSheet(dr("ArticleId")) Then
                    'Dim ArticleIdIndexHyphen As String = ""
                    Dim TotalQty As Double = dr("TotalQty")
                    'If dr("ArticleId").ToString.Contains("-") Then
                    '    Dim ArticleIdIndex As Integer = dr("ArticleId").ToString.IndexOf("-")
                    '    ArticleIdIndexHyphen = dr("ArticleId").ToString.Substring(0, ArticleIdIndex)
                    'Else
                    '    ArticleIdIndexHyphen = dr("ArticleId").ToString
                    'End If
                    If TotalQty > 1 AndAlso GetCostSheet(dr("ArticleId")) = True Then
                        'If TotalQty > 1 Then
                        For i As Integer = 1 To TotalQty
                            'UniqueId += 1
                            '    int  max = Convert.ToInt32(datatable_name.AsEnumerable()
                            '.Max(row => row["column_Name"]));
                            Dim MaxValue As Integer = 0
                            If dtAllItems.Rows.Count > 0 Then
                                'Dim MaxValue As Integer = Convert.ToInt32(dtAllItems.AsEnumerable().Max(row => row["column_Name"]))
                                Dim dr2() As DataRow = dtAllItems.Select("UniqueId=Max(UniqueId)")
                                For Each dRow1 As DataRow In dr2
                                    MaxValue = dRow1.Item("UniqueId")
                                Next
                                'MaxValue = CType(dr2.GetValue(0), DataRow).Item("UniqueId")

                            End If
                            Dim DRow As DataRow
                            DRow = dtAllItems.NewRow
                            DRow("Id") = dr("Id")
                            DRow("MaterialEstMasterID") = dr("MaterialEstMasterID")
                            DRow("MasterArticleID") = dr("MasterArticleID")
                            DRow("ArticleId") = dr("ArticleId")
                            DRow("PlanItem") = dr("PlanItem")
                            DRow("ArticleDescription") = dr("ArticleDescription")
                            DRow("ArticleUnitId") = dr("ArticleUnitId")
                            DRow("Unit") = dr("Unit")
                            DRow("Qty") = 1
                            DRow("TotalQty") = 1
                            DRow("Price") = dr("Price")
                            DRow("Types") = dr("Types")
                            DRow("Tag#") = dr("Tag#")
                            DRow("SubDepartmentID") = dr("SubDepartmentID")
                            DRow("SubDepartment") = dr("SubDepartment")
                            DRow("Approve") = dr("Approve")
                            DRow("PlanTicketsId") = dr("PlanTicketsId")
                            DRow("CostSheetID") = dr("CostSheetID")
                            DRow("ParentId") = dr("ParentId")
                            DRow("SerialNo") = dr("SerialNo")
                            DRow("ParentSerialNo") = dr("ParentSerialNo")
                            DRow("ParentQty") = dr("ParentQty")
                            DRow("UniqueId") = MaxValue + 1
                            DRow("ParentUniqueId") = dr("ParentUniqueId")
                            'If DRow("ArticleId").ToString.Contains("-") Then
                            '    Dim AIndex As Integer = DRow("ArticleId").ToString.IndexOf("-")
                            '    Dim AHyphen As String = DRow("ArticleId").ToString.Substring(0, AIndex)
                            '    DRow("ArticleId") = AHyphen & "-" & i.ToString
                            '    DRow("ParentId") = SetParentId
                            'Else
                            '    DRow("ArticleId") = DRow("ArticleId").ToString & "-" & i.ToString
                            '    DRow("ParentId") = SetParentId
                            'End If
                            dtAllItems.Rows.Add(DRow)
                            'Dim BIndex As Integer = DRow("ArticleId").ToString.IndexOf("-")
                            'Dim BHyphen As String = DRow("ArticleId").ToString.Substring(0, BIndex)
                            GetCostSheetItems(DRow("ArticleId"), DRow("TotalQty"), "", False, DRow("UniqueId"), DRow("UniqueId"))
                        Next
                    Else
                        'UniqueId += 1
                        Dim MaxValue As Integer = 0
                        dtAllItems.GetChanges()
                        If dtAllItems.Rows.Count > 0 Then
                            Dim dr2() As DataRow = dtAllItems.Select("UniqueId=Max(UniqueId)")
                            For Each dRow1 As DataRow In dr2
                                MaxValue = dRow1.Item("UniqueId")
                            Next

                            'MaxValue = CType(dr2.GetValue(0), DataRow).Item("UniqueId")

                            'dtAllItems.Columns("UniqueId").

                        End If
                        Dim DRow As DataRow
                        DRow = dtAllItems.NewRow
                        DRow("Id") = dr("Id")
                        DRow("MaterialEstMasterID") = dr("MaterialEstMasterID")
                        DRow("MasterArticleID") = dr("MasterArticleID")
                        DRow("ArticleId") = dr("ArticleId")
                        DRow("PlanItem") = dr("PlanItem")
                        DRow("ArticleDescription") = dr("ArticleDescription")
                        DRow("ArticleUnitId") = dr("ArticleUnitId")
                        DRow("Unit") = dr("Unit")
                        DRow("Qty") = dr("Qty")
                        DRow("TotalQty") = dr("TotalQty")
                        DRow("Price") = dr("Price")
                        DRow("Types") = dr("Types")
                        DRow("Tag#") = dr("Tag#")
                        DRow("SubDepartmentID") = dr("SubDepartmentID")
                        DRow("SubDepartment") = dr("SubDepartment")
                        DRow("Approve") = dr("Approve")
                        DRow("PlanTicketsId") = dr("PlanTicketsId")
                        DRow("CostSheetID") = dr("CostSheetID")
                        DRow("ParentId") = dr("ParentId")
                        DRow("SerialNo") = dr("SerialNo")
                        DRow("ParentSerialNo") = dr("ParentSerialNo")
                        DRow("ParentQty") = dr("ParentQty")
                        'DRow("ParentId") = SetParentId
                        DRow("ArticleId") = dr("ArticleId").ToString
                        DRow("UniqueId") = MaxValue + 1
                        DRow("ParentUniqueId") = dr("ParentUniqueId")
                        dtAllItems.Rows.Add(DRow)
                        'If DRow("ArticleId").ToString.Contains("-") Then
                        '    Dim BIndex As Integer = DRow("ArticleId").ToString.IndexOf("-")
                        '    Dim BHyphen As String = DRow("ArticleId").ToString.Substring(0, BIndex)
                        '    Dim AppendId1 As String = DRow("ArticleId").ToString.Substring(BIndex + 1)
                        '    Dim AppendId2 As Integer = Val(AppendId1)
                        '    GetCostSheetItems(BHyphen, dr("TotalQty"), dr("ArticleId"), False, , AppendId2)
                        'Else
                        GetCostSheetItems(DRow("ArticleId"), dr("TotalQty"), "", False, DRow("UniqueId"), DRow("UniqueId"))
                        'End If
                    End If
                Next
            End If
            dtAllItems.AcceptChanges()
            Return dtAllItems
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'Public Function MergeParentItem(ByVal ArticleId As String) As DataTable
    '    Dim ArticleMasterId As Integer = 0
    '    Try

    '        'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
    '        'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
    '        'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, Case When IsNull(MED.Quantity, 0) > 0 Then MED.Quantity Else IsNull(CS.Qty, 0) End As Qty, MED.Types, IsNull(MED.Approve, 0) As Approve, Ticket.PlanTicketsId, CS.CostSheetID from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Inner Join PlanTickets Ticket On ME.PlanTicketId = Ticket.PlanTicketsId Right Outer Join tblCostSheet CS  ON MED.CostSheetID = CS.CostSheetID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & PlanItemId & ""
    '        Dim MasterQuery As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, Convert(nvarchar, CS.ArticleID) As ArticleId, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, 1 As Qty, 1 As TotalQty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID, Convert(nvarchar, " & SetParentId & ") As ParentId, CS.SerialNo, CS.ParentSerialNo, " & Quantity & " As ParentQty from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable Where ArticleId = " & PlanItemId & ") As MasterItem On CS.ParentId = MasterItem.ArticleId  LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Where CS.ParentId = " & PlanItemId & ""
    '        DAL = New MaterialEstimationDAL()
    '        Dim table As DataTable = DAL.ReadTable(MasterQuery)
    '        Dim dtEdited As DataTable = table
    '        If Clone = False Then
    '            dtAllItems = table.Clone()
    '            Clone = True
    '        End If
    '        If dtEdited.Rows.Count > 0 Then
    '            For Each dr As DataRow In dtEdited.Rows
    '                'If GetCostSheet(dr("ArticleId")) Then
    '                Dim TotalQty As Double = dr("TotalQty")
    '                If TotalQty > 1 AndAlso GetCostSheet(dr("ArticleId")) = True Then
    '                    'If TotalQty > 1 Then
    '                    For i As Integer = 1 To TotalQty
    '                        Dim DRow As DataRow
    '                        DRow = dtAllItems.NewRow
    '                        DRow("Id") = dr("Id")
    '                        DRow("MaterialEstMasterID") = dr("MaterialEstMasterID")
    '                        DRow("MasterArticleID") = dr("MasterArticleID")
    '                        DRow("ArticleId") = dr("ArticleId")
    '                        DRow("PlanItem") = dr("PlanItem")
    '                        DRow("ArticleDescription") = dr("ArticleDescription")
    '                        DRow("ArticleUnitId") = dr("ArticleUnitId")
    '                        DRow("Unit") = dr("Unit")
    '                        DRow("Qty") = 1
    '                        DRow("TotalQty") = 1
    '                        DRow("Price") = dr("Price")
    '                        DRow("Types") = dr("Types")
    '                        DRow("Tag#") = dr("Tag#")
    '                        DRow("SubDepartmentID") = dr("SubDepartmentID")
    '                        DRow("SubDepartment") = dr("SubDepartment")
    '                        DRow("Approve") = dr("Approve")
    '                        DRow("PlanTicketsId") = dr("PlanTicketsId")
    '                        DRow("CostSheetID") = dr("CostSheetID")
    '                        DRow("ParentId") = dr("ParentId")
    '                        DRow("SerialNo") = dr("SerialNo")
    '                        DRow("ParentSerialNo") = dr("ParentSerialNo")
    '                        DRow("ParentQty") = dr("ParentQty")
    '                        'If dtAllItems.Rows.Count > 0 Then
    '                        '    'For Each row As DataRow In dtAllItems.Rows
    '                        '    Dim IncrementalId As String = ""
    '                        '    Dim ArticleId As String = ""
    '                        '    Dim NewArticleId As String = ""
    '                        '    If DRow("ArticleID").ToString.Contains("-") Then
    '                        '        Dim AIndex As Integer = DRow("ArticleId").ToString.IndexOf("-")
    '                        '        ArticleId = DRow("ArticleId").ToString.Substring(0, AIndex)
    '                        '        'grdMaterialEstimation.GetRow.Cells("ArticleID").Value = AHyphen & "-" & i.ToString
    '                        '        Dim drRows() As DataRow = dtAllItems.Select("ArticleId LIKE '" & ArticleId & "%'")
    '                        '        'Dim IncrementIds As DataTable = drRows.CopyToDataTable
    '                        '        Dim IncrementalIds As New List(Of String)
    '                        '        For Each IncrementId As DataRow In drRows
    '                        '            If IncrementId("ArticleID").ToString.Contains("-") Then
    '                        '                Dim AIndex1 As Integer = IncrementId("ArticleID").ToString.IndexOf("-")
    '                        '                Dim Id As String = IncrementId("ArticleID").ToString.Substring(AIndex + 1)
    '                        '                IncrementalIds.Add(Id)
    '                        '            End If
    '                        '        Next
    '                        '        IncrementalId = IncrementalIds(IncrementalIds.Count - 1)
    '                        '        IncrementalId += 1
    '                        '        NewArticleId = ArticleId & "-" & IncrementalId.ToString
    '                        '        'DRow("ParentId") = SetParentId
    '                        '        DRow("ArticleId") = NewArticleId
    '                        '    Else
    '                        '        IncrementalId = 1
    '                        '        ArticleId = DRow("ArticleID").ToString
    '                        '        NewArticleId = ArticleId & "-" & IncrementalId.ToString
    '                        '        DRow("ArticleId") = NewArticleId
    '                        '        'grdMaterialEstimation.GetRow.Cells("ArticleID").Value
    '                        '    End If
    '                        '    'Next
    '                        'End If
    '                        'DRow("ParentId") = SetParentId

    '                        If DRow("ArticleId").ToString.Contains("-") Then
    '                            Dim AIndex As Integer = DRow("ArticleId").ToString.IndexOf("-")
    '                            Dim AHyphen As String = DRow("ArticleId").ToString.Substring(0, AIndex)
    '                            DRow("ArticleId") = AHyphen & "-" & i.ToString
    '                            'dr("Qty") = 1
    '                            'dr("TotalQty") = 1
    '                            'If Val(dr1("ParentId").ToString) <> 0 Then
    '                            '    Dim PIndex As Integer = dr1("ParentId").ToString.IndexOf("-")
    '                            '    Dim PHyphen As String = dr1("ParentId").ToString.Substring(0, PIndex)
    '                            '    dr("ParentId") = PHyphen & "-" & i.ToString
    '                            'End If
    '                            DRow("ParentId") = SetParentId
    '                        Else
    '                            DRow("ArticleId") = DRow("ArticleId").ToString & "-" & i.ToString
    '                            'dr("Qty") = 1
    '                            'dr("TotalQty") = 1
    '                            'If Val(dr1("ParentId").ToString) <> 0 Then
    '                            DRow("ParentId") = SetParentId
    '                            'End If
    '                        End If
    '                        dtAllItems.Rows.Add(DRow)
    '                        Dim BIndex As Integer = DRow("ArticleId").ToString.IndexOf("-")
    '                        Dim BHyphen As String = DRow("ArticleId").ToString.Substring(0, BIndex)
    '                        GetCostSheetItems(BHyphen, DRow("TotalQty"), DRow("ArticleId"))
    '                    Next
    '                Else
    '                    Dim DRow As DataRow
    '                    DRow = dtAllItems.NewRow
    '                    DRow("Id") = dr("Id")
    '                    DRow("MaterialEstMasterID") = dr("MaterialEstMasterID")
    '                    DRow("MasterArticleID") = dr("MasterArticleID")
    '                    DRow("ArticleId") = dr("ArticleId")
    '                    DRow("PlanItem") = dr("PlanItem")
    '                    DRow("ArticleDescription") = dr("ArticleDescription")
    '                    DRow("ArticleUnitId") = dr("ArticleUnitId")
    '                    DRow("Unit") = dr("Unit")
    '                    DRow("Qty") = dr("Qty")
    '                    DRow("TotalQty") = dr("TotalQty")
    '                    DRow("Price") = dr("Price")
    '                    DRow("Types") = dr("Types")
    '                    DRow("Tag#") = dr("Tag#")
    '                    DRow("SubDepartmentID") = dr("SubDepartmentID")
    '                    DRow("SubDepartment") = dr("SubDepartment")
    '                    DRow("Approve") = dr("Approve")
    '                    DRow("PlanTicketsId") = dr("PlanTicketsId")
    '                    DRow("CostSheetID") = dr("CostSheetID")
    '                    DRow("ParentId") = dr("ParentId")
    '                    DRow("SerialNo") = dr("SerialNo")
    '                    DRow("ParentSerialNo") = dr("ParentSerialNo")
    '                    DRow("ParentQty") = dr("ParentQty")
    '                    'If DRow("ArticleId").ToString.Contains("-") Then
    '                    '    Dim AIndex As Integer = DRow("ArticleId").ToString.IndexOf("-")
    '                    '    Dim AHyphen As String = DRow("ArticleId").ToString.Substring(0, AIndex)
    '                    '    DRow("ArticleId") = AHyphen & "-" & i.ToString
    '                    '    'dr("Qty") = 1
    '                    '    'dr("TotalQty") = 1
    '                    '    'If Val(dr1("ParentId").ToString) <> 0 Then
    '                    '    '    Dim PIndex As Integer = dr1("ParentId").ToString.IndexOf("-")
    '                    '    '    Dim PHyphen As String = dr1("ParentId").ToString.Substring(0, PIndex)
    '                    '    '    dr("ParentId") = PHyphen & "-" & i.ToString
    '                    '    'End If
    '                    DRow("ParentId") = SetParentId
    '                    'Else
    '                    DRow("ArticleId") = dr("ArticleId").ToString
    '                    ''dr("Qty") = 1
    '                    ''dr("TotalQty") = 1
    '                    ''If Val(dr1("ParentId").ToString) <> 0 Then
    '                    'DRow("ParentId") = SetParentId
    '                    'End If
    '                    'End If
    '                    dtAllItems.Rows.Add(DRow)
    '                    GetCostSheetItems(DRow("ArticleId"), dr("TotalQty"), dr("ArticleId"))
    '                End If
    '            Next
    '        End If
    '        '     CS.ParentSerialNo, " & Quantity & " As ParentQty

    '        'Dim totalQty As Double = dr("TotalQty")
    '        'If totalQty > 1 Then
    '        '    For i As Integer = 1 To totalQty
    '        '        If i > 1 Then
    '        '            Dim AppendId As Integer = i - 1
    '        '            Dim dr1 As DataRow
    '        '            dr1 = table.NewRow
    '        '            dr1 = dr
    '        '            dr1.BeginEdit()
    '        '            If dr1("ArticleId").ToString.Contains("-") Then
    '        '                Dim AIndex As Integer = dr1("ArticleId").ToString.IndexOf("-")
    '        '                Dim AHyphen As String = dr1("ArticleId").ToString.Substring(0, AIndex)
    '        '                dr("ArticleId") = AHyphen & "-" & AppendId.ToString
    '        '                'dr("Qty") = 1
    '        '                'dr("TotalQty") = 1
    '        '                'If Val(dr1("ParentId").ToString) <> 0 Then
    '        '                '    Dim PIndex As Integer = dr1("ParentId").ToString.IndexOf("-")
    '        '                '    Dim PHyphen As String = dr1("ParentId").ToString.Substring(0, PIndex)
    '        '                '    dr("ParentId") = PHyphen & "-" & i.ToString
    '        '                'End If
    '        '                dr1("ParentId") = SetParentId
    '        '            Else
    '        '                dr1("ArticleId") = dr1("ArticleId").ToString & "-" & AppendId.ToString
    '        '                'dr("Qty") = 1
    '        '                'dr("TotalQty") = 1
    '        '                'If Val(dr1("ParentId").ToString) <> 0 Then
    '        '                dr1("ParentId") = SetParentId
    '        '                'End If
    '        '            End If
    '        '            dr1.EndEdit()
    '        '            dtAllItems.ImportRow(dr1)
    '        '            'GetCostSheetItems(dr("ArticleId"), dr("TotalQty"), dr1("ArticleId"))

    '        '        Else
    '        '            dtAllItems.ImportRow(dr)

    '        '        End If
    '        '    Next
    '        'End If
    '        'dtAllItems.Rows.Add(DRow)
    '        'GetCostSheetItems(dr("ArticleId"), dr("TotalQty"), dr("ArticleId"))


    '        'If table.Rows.Count > 0 Then
    '        '    For Each Row As DataRow In table.Rows


    '        '        dtAllItems.ImportRow(Row)
    '        '        GetCostSheetItems(Row("ArticleId").ToString, Row("TotalQty"), Row("ParentId").ToString)
    '        '    Next
    '        'End If
    '        'Dim dtMerged As New DataTable
    '        'If table.Rows.Count > 0 Then
    '        '    For Each Dr As DataRow In table.Rows
    '        '        Dim dtChild As New DataTable
    '        '        'Dim ChildQuery As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Val(Dr.Item("Qty").ToString) & " As Qty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTableMaster) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId  LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id  Where CS.MasterArticleID = " & Dr.Item("ArticleID") & ""
    '        '        Dim ChildQuery As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Val(Dr.Item("Qty").ToString) & " As Qty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID, M.MasterID  from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTableMaster) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId  LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Inner Join (Select IsNull(MasterID, 0) AS MasterID FROM ArticleDefTable Where ArticleId = " & Dr.Item("ArticleID") & " ) As M ON  CS.MasterArticleID = M.MasterID "
    '        '        dtChild = UtilityDAL.GetDataTable(ChildQuery)
    '        '        If dtChild.Rows.Count > 0 Then
    '        '            dtMerged.Merge(dtChild)
    '        '            For Each Dr1 As DataRow In dtChild.Rows
    '        '                Dim dt3 As New DataTable
    '        '                'Dim Str3 As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Val(Dr1.Item("Qty").ToString) & " As Qty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTableMaster) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId  LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Where CS.MasterArticleID = " & Dr1.Item("ArticleID") & ""
    '        '                Dim Str3 As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Val(Dr1.Item("Qty").ToString) & " As Qty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID, M.MasterID from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTableMaster) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId  LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Inner Join (Select IsNull(MasterID, 0) AS MasterID FROM ArticleDefTable Where ArticleId = " & Dr1.Item("ArticleID") & " ) As M ON  CS.MasterArticleID = M.MasterID "
    '        '                dt3 = UtilityDAL.GetDataTable(Str3)
    '        '                If dt3.Rows.Count > 0 Then
    '        '                    dtMerged.Merge(dt3)
    '        '                End If
    '        '            Next
    '        '        End If
    '        '    Next
    '        'End If
    '        ''table.AcceptChanges()
    '        'If dtMerged.Rows.Count > 0 Then
    '        '    table.Merge(dtMerged)
    '        'End If
    '        'table.AcceptChanges()
    '        dtAllItems.AcceptChanges()
    '        Return dtAllItems
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function
    Public Function PlanCostSheetItems(ByVal PlanItemId As Integer, ByVal Quantity As Double) As DataTable
        Try
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, Case When IsNull(MED.Quantity, 0) > 0 Then MED.Quantity Else IsNull(CS.Qty, 0) End As Qty, MED.Types, IsNull(MED.Approve, 0) As Approve, Ticket.PlanTicketsId, CS.CostSheetID from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Inner Join PlanTickets Ticket On ME.PlanTicketId = Ticket.PlanTicketsId Right Outer Join tblCostSheet CS  ON MED.CostSheetID = CS.CostSheetID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & PlanItemId & ""
            Dim MasterQuery As String = " Select 0 As Id, 0 As MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, IsNull(CS.Qty, 0) * " & Quantity & " As Qty, IsNull(CS.CostPrice, 0) As Price, '' As Types, '' As Tag#, IsNull(CS.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, Convert(bit, 0) As Approve,  0 As PlanTicketsId, CS.CostSheetID, IsNull(CS.ParentId, 0) As ParentId from tblCostSheet CS Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTableMaster) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId LEFT OUTER JOIN tblProSteps ON CS.SubDepartmentID = tblProSteps.ProdStep_Id Where CS.MasterArticleID = " & PlanItemId & ""
            DAL = New MaterialEstimationDAL()
            Dim table As DataTable = DAL.ReadTable(MasterQuery)
            'table.AcceptChanges()
            Return table
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function GetCostSheet(ByVal PlanItemId As String) As Boolean
        Try
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & mArticleId & " "
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, Case When IsNull(MED.Quantity, 0) > 0 Then MED.Quantity Else IsNull(CS.Qty, 0) End As Qty, MED.Types, IsNull(MED.Approve, 0) As Approve, Ticket.PlanTicketsId, CS.CostSheetID from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Inner Join PlanTickets Ticket On ME.PlanTicketId = Ticket.PlanTicketsId Right Outer Join tblCostSheet CS  ON MED.CostSheetID = CS.CostSheetID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On CS.MasterArticleID = MasterItem.ArticleId Where CS.MasterArticleID = " & PlanItemId & ""
            Dim MasterQuery As String = "Select CostSheetID From tblCostSheet Where ParentId =" & PlanItemId & ""
            DAL = New MaterialEstimationDAL()
            Dim table As DataTable = DAL.ReadTable(MasterQuery)
            If table.Rows.Count > 0 Then
                Return True
            ElseIf table Is Nothing Then
                Return False
            Else
                Return False
            End If
            'table.AcceptChanges()
            'Return table
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

            DAL = New MaterialEstimationDAL()
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
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, MED.PlanItemId As MasterArticleID, MED.ProductId As ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, Case When IsNull(MED.Quantity, 0) <> 0 Then MED.Quantity Else IsNull(CS.Qty, 0) End As Qty, Case When IsNull(MED.TotalQty, 0) <> 0 Then MED.TotalQty Else IsNull(CS.Total_Qty, 0) End As TotalQty, IsNull(MED.Price, 0) As Price, MED.Types, MED.Tag#, IsNull(MED.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, IsNull(MED.Approve, 0) As Approve, Ticket.PlanTicketsId, CS.CostSheetID, MED.ParentId As ParentId, MED.SerialNo, MED.ParentSerialNo,  IsNull(MED.UniqueId, 0) As UniqueId, IsNull(MED.ParentUniqueId, 0) As ParentUniqueId  from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Left Join PlanTickets Ticket On ME.PlanTicketId = Ticket.PlanTicketsId Left Outer Join tblCostSheet CS  ON MED.CostSheetID = CS.CostSheetID Inner Join ArticleDefTable Article On CASE WHEN ProductId LIKE '%-%' THEN Convert(Int, LEFT([ProductId], CHARINDEX('-', [ProductId]) - 1)) Else ProductId End = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On  CASE WHEN MED.ParentId LIKE '%-%' THEN Convert(Int, LEFT(MED.ParentId, CHARINDEX('-', MED.ParentId) - 1)) Else MED.ParentId End = MasterItem.ArticleId LEFT OUTER JOIN tblProSteps ON MED.SubDepartmentID = tblProSteps.ProdStep_Id Where MED.MaterialEstMasterID = " & MasterID & ""

            '' Below line is commented to join issued qty too for validation. on 31-07-2017
            'Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, MED.PlanItemId As MasterArticleID, MED.ProductId As ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, Case When IsNull(MED.Quantity, 0) <> 0 Then MED.Quantity Else IsNull(CS.Qty, 0) End As Qty, Case When IsNull(MED.TotalQty, 0) <> 0 Then MED.TotalQty Else IsNull(CS.Total_Qty, 0) End As TotalQty, IsNull(MED.Price, 0) As Price, MED.Types, MED.Tag#, IsNull(MED.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, IsNull(MED.Approve, 0) As Approve, Ticket.PlanTicketsId, CS.CostSheetID, MED.ParentId As ParentId, MED.SerialNo, MED.ParentSerialNo,  IsNull(MED.UniqueId, 0) As UniqueId, IsNull(MED.ParentUniqueId, 0) As ParentUniqueId, IsNull(MED.ParentTag#, 0) As ParentTag# from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Left Join PlanTickets Ticket On ME.PlanTicketId = Ticket.PlanTicketsId Left Outer Join tblCostSheet CS  ON MED.CostSheetID = CS.CostSheetID Inner Join ArticleDefTable Article On ProductId = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On MED.ParentId = MasterItem.ArticleId LEFT OUTER JOIN tblProSteps ON MED.SubDepartmentID = tblProSteps.ProdStep_Id Where MED.MaterialEstMasterID = " & MasterID & ""

            '' Added issued qty
            ''TASK:TFS1141 added pending estimation qty column
            Dim MasterQuery As String = " Select MED.Id, MED.MaterialEstMasterID, MED.PlanItemId As MasterArticleID, MED.ProductId As ArticleID, MasterItem.PlanItem, Article.ArticleDescription, ArticleUnitDefTable.ArticleUnitId, ArticleUnitDefTable.ArticleUnitName As Unit, Case When IsNull(MED.Quantity, 0) <> 0 Then MED.Quantity Else IsNull(CS.Qty, 0) End As Qty, Case When IsNull(MED.TotalQty, 0) <> 0 Then MED.TotalQty Else IsNull(CS.Total_Qty, 0) End As TotalQty, IsNull(MED.Price, 0) As Price, MED.Types, MED.Tag#, IsNull(MED.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, IsNull(MED.Approve, 0) As Approve, Ticket.PlanTicketsId, CS.CostSheetID, MED.ParentId As ParentId, MED.SerialNo, MED.ParentSerialNo,  IsNull(MED.UniqueId, 0) As UniqueId, IsNull(MED.ParentUniqueId, 0) As ParentUniqueId, IsNull(MED.ParentTag#, 0) As ParentTag#, PendingEstimation.PendingEstQty from MaterialEstimationDetailTable MED Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Left Join PlanTickets Ticket On ME.PlanTicketId = Ticket.PlanTicketsId Left Outer Join tblCostSheet CS  ON MED.CostSheetID = CS.CostSheetID Inner Join ArticleDefTable Article On ProductId = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Left Outer Join (Select ArticleId, ArticleDescription As PlanItem FROM ArticleDefTable ) As MasterItem On MED.ParentId = MasterItem.ArticleId LEFT OUTER JOIN tblProSteps ON MED.SubDepartmentID = tblProSteps.ProdStep_Id " _
                                        & " LEFT OUTER JOIN (Select Sum(IsNull(Case When EstimationDetail.Types ='Minus' Then 0 Else EstimationDetail.Quantity End, 0)-(IsNull(Qty, 0)-IsNull(ReturnedTotalQty, 0))) As PendingEstQty, EstimationDetail.MaterialEstMasterID As EstimationId , IsNull(EstimationDetail.SubDepartmentID, 0) As SubDepartmentID, IsNull(EstimationDetail.ProductId, 0) As ProductId From DispatchDetailTable As DispatchDetail Right Outer Join MaterialEstimationDetailTable As EstimationDetail ON DispatchDetail.EstimationId = EstimationDetail.MaterialEstMasterID And DispatchDetail.SubDepartmentID = EstimationDetail.SubDepartmentID And DispatchDetail.ArticleDefId = EstimationDetail.ProductId  Where EstimationDetail.MaterialEstMasterID = " & MasterID & " And EstimationDetail.Types <> 'Minus' Group By EstimationDetail.MaterialEstMasterID, EstimationDetail.SubDepartmentID, EstimationDetail.ProductId) As PendingEstimation ON MED.MaterialEstMasterID = PendingEstimation.EstimationId And MED.SubDepartmentID = PendingEstimation.SubDepartmentID And MED.ProductId = PendingEstimation.ProductId Where MED.MaterialEstMasterID = " & MasterID & ""

            DAL = New MaterialEstimationDAL()
            Dim table As DataTable = DAL.ReadTable(MasterQuery)
            'table.AcceptChanges()
            Return table
        Catch ex As Exception
            Throw ex
        End Try

    End Function



    Public Function DeleteMaster(ByVal MasterID As Integer)
        DAL = New MaterialEstimationDAL()
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
            DAL = New MaterialEstimationDAL()
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
        DAL = New MaterialEstimationDAL()
        'Dim MasterQuery As String = "insert into MaterialEstimation (EstimationDate, MasterPlanId, PlanTicketId, Remarks, DocNo, SaleOrderId, PlanItemId) values(N'" & matEstObj.EstimationDate.ToString("yyyy-M-d hh:mm:ss tt") & "', " & matEstObj.MasterPlanId & " , " & matEstObj.PlanTicketId & " , N'" & matEstObj.Remarks & "', N'" & matEstObj.DocNo & "', " & matEstObj.SaleOrderId & ", " & matEstObj.PlanItemId & ")Select @@Identity"

        Dim MasterQuery As String = "update  MaterialEstimation set EstimationDate = N'" & matEstObj.EstimationDate.ToString("yyyy-M-d hh:mm:ss tt") & "', MasterPlanId = " & matEstObj.MasterPlanId & ", PlanTicketId= " & matEstObj.PlanTicketId & ", Remarks=N'" & matEstObj.Remarks & "' , docno=N'" & matEstObj.DocNo & "',saleorderId= " & matEstObj.SaleOrderId & ", PlanItemId= " & matEstObj.PlanItemId & " where Id=" & matEstObj.Id
        Dim Detailquery As String
        'For i As Integer = 0 To grid.RowCount
        '    Detailquery = "insert into MaterialEstimationDetailTable (MaterialEstMasterID ,Quantity, Types ,Approve , PlanItemId , ProductId) values(`,'" & grid.GetRow(i).Cells(3).Value.ToString() & "','" & grid.GetRow(i).Cells(3).Value.ToString() & "','" & grid.GetRow(i).Cells(3).Value.ToString() & "','" & grid.GetRow(i).Cells(3).Value.ToString() & "','" & grid.GetRow(i).Cells(3).Value.ToString() & "' )"
        '    DetailQueryList.Add(Detailquery)
        'Next
        For i As Integer = 0 To dt.Rows.Count - 1
            'S MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, MasterItem.PlanItem, Article.ArticleDescription, CS.Qty, MED.Types, MED.Approve
            ''TASK:1006 Added TagIds to estimation.
            Detailquery = "insert into MaterialEstimationDetailTable(MaterialEstMasterID ,Quantity, Types, Approve, PlanItemId, ProductId, CostSheetID, SubDepartmentID, Tag#, Price, ParentId, SerialNo, ParentSerialNo, TotalQty, UniqueId, ParentUniqueId, ParentTag#) values(`, " & Val(dt.Rows(i).Item("Qty").ToString) & ", N'" & dt.Rows(i).Item("Types").ToString.Replace("'", "''") & "', " & IIf(dt.Rows(i).Item("Approve") = False, 0, 1) & ", " & Val(dt.Rows(i).Item("MasterArticleID").ToString) & ", '" & dt.Rows(i).Item("ArticleID").ToString & "', " & Val(dt.Rows(i).Item("CostSheetID").ToString) & ", " & Val(dt.Rows(i).Item("SubDepartmentID").ToString) & ", N'" & dt.Rows(i).Item("Tag#").ToString.Replace("'", "''") & "', " & Val(dt.Rows(i).Item("Price").ToString) & ", '" & dt.Rows(i).Item("ParentId").ToString & "', N'" & dt.Rows(i).Item("SerialNo").ToString.Replace("'", "''") & "', N'" & dt.Rows(i).Item("ParentSerialNo").ToString.Replace("'", "''") & "', " & Val(dt.Rows(i).Item("TotalQty").ToString) & ", " & Val(dt.Rows(i).Item("UniqueId").ToString) & ", " & Val(dt.Rows(i).Item("ParentUniqueId").ToString) & ", " & Val(dt.Rows(i).Item("ParentTag#").ToString) & ")"
            '' TASK:1006 Added TagIds to estimation.
            DetailQueryList.Add(Detailquery)
        Next
        Dim Update_Detail_Del As String = "Delete from MaterialEstimationDetailTable where MaterialEstMasterID=" & matEstObj.Id

        DAL.Master_Update(matEstObj.Id, MasterQuery, DetailQueryList, Update_Detail_Del)

        Return DetailQueryList
    End Function
    Public Function GetCostSheet(ByVal ArticleId As Integer, ByVal Qty As Double, Optional ByVal Counter As Integer = 0, Optional ByVal GridCount As Integer = 0, Optional ByVal ParentId As Integer = 0, Optional ByVal GridMaxTagId As Integer = 0, Optional ByVal ParentTagId As Integer = 0, Optional ByVal Price As Double = 0, Optional ByVal PlanItemId As Integer = 0, Optional ByVal PlanItem As String = "") As DataTable
        Try
            Dim TagId As Integer = 0
            'Dim dt As DataTable = DAL.ReadTable(" Exec GetCostSheetItem " & ArticleId & ", " & ParentId & ", " & Qty & " ")
            Dim dt As DataTable = DAL.ReadTable("Exec GetCostSheetItem " & ArticleId & ", " & Qty & " ")
            dt.AcceptChanges()
            dtGetCostSheet = dt.Clone()
            ''TASK:1006 Added TagIds to estimation.
            Dim TagNo As Integer = GetTagMax()
            If GridMaxTagId > TagNo Then
                TagId = GridMaxTagId
            Else
                TagId = TagNo
            End If
            '' END TASK:1006 Added TagIds to estimation.
            GetArticle(dt, Counter, GridCount, TagId, ParentTagId, ParentId, Price, PlanItemId, PlanItem)
            Return dtGetCostSheet
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCostSheetPlus(ByVal ArticleId As Integer, ByVal Qty As Double, Optional ByVal Counter As Integer = 0, Optional ByVal GridCount As Integer = 0, Optional ByVal ParentId As Integer = 0) As DataTable
        Try
            Dim dt As DataTable = DAL.ReadTable("Exec GetCostSheetItemPlus " & ArticleId & ", " & ParentId & ", " & Qty & " ")
            dt.AcceptChanges()
            dtGetCostSheet = dt.Clone()
            Dim TagNo As Integer = GetTagMax()
            GetArticle(dt, Counter, GridCount, TagNo, 0, ParentId)
            Return dtGetCostSheet
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub GetArticle(ByVal dt As DataTable, ByVal ParentUniqueId As Integer, ByVal GridCount As Integer, ByVal TagNo As Integer, ByVal ParentTagNo As Integer, Optional ByVal ParentId As Integer = 0, Optional ByVal Price As Integer = 0, Optional ByVal PlanItemId As Integer = 0, Optional ByVal PlanItem As String = "")
        Try
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim TotalQty As Double = dr("Qty")
                    If TotalQty > 1 AndAlso GetCostSheet(dr("ArticleId")) = True Then
                        For i As Integer = 1 To TotalQty
                            Dim DRow As DataRow
                            DRow = dtGetCostSheet.NewRow
                            DRow("Id") = dr("Id")
                            DRow("MaterialEstMasterID") = dr("MaterialEstMasterID")
                            'DRow("MasterArticleID") = dr("MasterArticleID")
                            DRow("MasterArticleID") = PlanItemId

                            DRow("ArticleId") = dr("ArticleId")
                            'DRow("PlanItem") = dr("PlanItem")
                            DRow("PlanItem") = PlanItem
                            DRow("ArticleDescription") = dr("ArticleDescription")
                            DRow("ArticleUnitId") = dr("ArticleUnitId")
                            DRow("Unit") = dr("Unit")
                            DRow("Qty") = 1
                            DRow("TotalQty") = 1
                            If Price > 0 Then
                                DRow("Price") = Price
                            Else
                                DRow("Price") = dr("Price")
                            End If
                            DRow("Types") = dr("Types")
                            ''TASK:1006 Added TagIds to estimation.
                            'DRow("Tag#") = (dtGetCostSheet.Rows.Count + 1 + TagNo - CounTag)
                            Dim Tag1 As Integer = 0
                            If dtGetCostSheet.Rows.Count > 0 Then
                                Tag1 = Val(dtGetCostSheet.AsEnumerable().Max(Function(R) R.Field(Of Integer)("Tag#")).ToString)
                                DRow("Tag#") = (Tag1 + 1)
                            Else
                                DRow("Tag#") = (1 + TagNo)
                            End If
                            DRow("ParentTag#") = ParentTagNo
                            'TagNo += 1
                            '' END TASK:1006 Added TagIds to estimation.
                            DRow("SubDepartmentID") = dr("SubDepartmentID")
                            DRow("SubDepartment") = dr("SubDepartment")
                            DRow("Approve") = dr("Approve")
                            DRow("PlanTicketsId") = dr("PlanTicketsId")
                            DRow("CostSheetID") = dr("CostSheetID")
                            If ParentId > 0 Then
                                DRow("ParentId") = ParentId
                            Else
                                DRow("ParentId") = dr("ParentId")
                            End If
                            DRow("SerialNo") = dr("SerialNo")
                            DRow("ParentSerialNo") = dr("ParentSerialNo")
                            DRow("UniqueId") = (dtGetCostSheet.Rows.Count + 1 + GridCount)
                            DRow("ParentUniqueId") = ParentUniqueId
                            dtGetCostSheet.Rows.Add(DRow)
                            'Dim Values = From row In dtGetCostSheet.AsEnumerable
                            '             Select row.Field(Of Integer)("Tag#")
                            'Dim Max As Integer = Convert.ToInt32(dtGetCostSheet.Compute("MAX(Tag#)", String.Empty))
                            'Dim Values As Integer = dtGetCostSheet.AsEnumerable().Select(Function(r) r.Field(Of Integer)("Tag#"))
                            'Dim Values As Integer = dtGetCostSheet.AsEnumerable().(Function(r) r.Field(Of Integer)("Tag#"))
                            'Dim Value As Integer =  
                            GetChildArticles(DRow("ArticleId"), DRow("UniqueId"), DRow("Qty"), GridCount, TagNo, DRow("Tag#"), PlanItemId, PlanItem)
                        Next
                    Else
                        'Dim ParentTag As Integer = TagNo
                        Dim ParentTag As Integer = ParentTag
                        Dim DRow As DataRow
                        DRow = dtGetCostSheet.NewRow
                        DRow("Id") = dr("Id")
                        DRow("MaterialEstMasterID") = dr("MaterialEstMasterID")
                        'DRow("MasterArticleID") = dr("MasterArticleID")
                        DRow("MasterArticleID") = PlanItemId

                        DRow("ArticleId") = dr("ArticleId")
                        'DRow("PlanItem") = dr("PlanItem")
                        DRow("PlanItem") = PlanItem

                        DRow("ArticleDescription") = dr("ArticleDescription")
                        DRow("ArticleUnitId") = dr("ArticleUnitId")
                        DRow("Unit") = dr("Unit")
                        DRow("Qty") = dr("Qty")
                        DRow("TotalQty") = dr("TotalQty")
                        If Price > 0 Then
                            DRow("Price") = Price
                        Else
                            DRow("Price") = dr("Price")
                        End If
                        DRow("Types") = dr("Types")
                        DRow("SubDepartmentID") = dr("SubDepartmentID")
                        DRow("SubDepartment") = dr("SubDepartment")
                        DRow("Approve") = dr("Approve")
                        DRow("PlanTicketsId") = dr("PlanTicketsId")
                        DRow("CostSheetID") = dr("CostSheetID")
                        If ParentId > 0 Then
                            DRow("ParentId") = ParentId
                        Else
                            DRow("ParentId") = dr("ParentId")
                        End If
                        DRow("SerialNo") = dr("SerialNo")
                        DRow("ParentSerialNo") = dr("ParentSerialNo")
                        DRow("ArticleId") = dr("ArticleId").ToString
                        DRow("UniqueId") = (dtGetCostSheet.Rows.Count + 1 + GridCount)
                        DRow("ParentUniqueId") = ParentUniqueId
                        ''TASK:1006 Added TagIds to estimation.
                        DRow("ParentTag#") = ParentTagNo
                        If GetCostSheet(dr("ArticleId")) = True Then
                            'DRow("Tag#") = (dtGetCostSheet.Rows.Count + 1 + TagNo - CounTag)
                            Dim Tag1 As Integer = 0
                            If dtGetCostSheet.Rows.Count > 0 Then
                                Tag1 = Val(dtGetCostSheet.AsEnumerable().Max(Function(R) R.Field(Of Integer)("Tag#")).ToString)
                                DRow("Tag#") = (Tag1 + 1)
                            Else
                                DRow("Tag#") = (1 + TagNo)
                                ParentTag = DRow("Tag#")
                            End If
                            ' += 1
                            CounTag = 0
                        Else
                            DRow("Tag#") = 0
                            CounTag = 1
                        End If
                        '' END TASK:1006 Added TagIds to estimation.
                        'Dim Max() As DataRow = dtGetCostSheet.Select("Max(Tag#)")
                        'Dim Tag As Integer = Convert.ToInt32(Max(0))
                        'Dim Max As Integer = Convert.ToInt32(dtGetCostSheet.Compute("MAX(Tag#)", String.Empty))
                        dtGetCostSheet.Rows.Add(DRow)
                        'GetChildArticles(DRow("ArticleId"), DRow("UniqueId"), DRow("Qty"), GridCount, TagNo, ParentTag)
                        '' TASK TFS2006 on 28-12-2017 done by Muhammad Ameen included PlanItemId and PlanItem values in this funciton 
                        GetChildArticles(DRow("ArticleId"), DRow("UniqueId"), DRow("Qty"), GridCount, TagNo, DRow("Tag#"), PlanItemId, PlanItem)

                    End If
                Next
                'Else
                '    TagNo -= 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetChildArticles(ByVal ParentId As Integer, ByVal ParentUniqueId As Integer, ByVal Qty As Double, Optional ByVal GridCount As Integer = 0, Optional ByVal TagNo As Integer = 0, Optional ByVal ParentTagNo As Integer = 0, Optional ByVal PlanItemId As Integer = 0, Optional ByVal PlanItem As String = "")
        Try
            Dim dt As DataTable = DAL.ReadTable("Exec GetCostSheetItems " & ParentId & ", " & Qty & "")
            'If Not dt.Rows.Count > 0 Then
            '    dtGetCostSheet.Rows.Item(dtGetCostSheet.Rows.Count - 1).BeginEdit()
            '    dtGetCostSheet.Rows.Item(dtGetCostSheet.Rows.Count - 1).Item("Tag#") = 0
            '    dtGetCostSheet.Rows.Item(dtGetCostSheet.Rows.Count - 1).EndEdit()
            '    'TagNo -= 1
            'End If
            dt.AcceptChanges()
            GetArticle(dt, ParentUniqueId, GridCount, TagNo, ParentTagNo, , , PlanItemId, PlanItem)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''TASK:1006 Added TagIds to estimation.
    Public Function GetTagMax() As Integer
        Try
            Dim MasterQuery As String = "SELECT Case When Max(Tag#) IS NULL Then 0 Else Max(Tag#) End As Tag# From MaterialEstimationDetailTable"
            Dim table As DataTable = New MaterialEstimationDAL().ReadTable(MasterQuery)
            Return table.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '' END TASK:1006 Added TagIds to estimation.

    ''TASK:1006 Added TagIds to estimation.
    Public Function GetEstimationConsumption(ByVal TagId As Integer, ByVal IsConsumed As Boolean) As DataTable
        Try
            'Dim dt As DataTable = DAL.ReadTable(" Exec GetCostSheetItem " & ArticleId & ", " & ParentId & ", " & Qty & " ")
            DAL = New MaterialEstimationDAL()
            Dim dt As DataTable = DAL.ReadTable("Exec GetEstimationConsumption1 " & TagId & "")
            dt.AcceptChanges()
            dtEstimationConsumption = New DataTable()
            dtEstimationConsumption = dt.Clone()
            GetECParetn(dt, IsConsumed)
            Return dtEstimationConsumption
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '' END TASK:1006 Added TagIds to estimation.

    ''TASK:1006 Added TagIds to estimation.
    Public Sub GetECParetn(ByVal dt As DataTable, ByVal IsConsumed As Boolean)
        Try
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim DRow As DataRow
                    DRow = dtEstimationConsumption.NewRow
                    DRow("Id") = dr("Id")
                    DRow("MaterialEstMasterID") = dr("MaterialEstMasterID")
                    DRow("MasterArticleID") = dr("MasterArticleID")
                    DRow("ArticleId") = dr("ArticleId")
                    DRow("PlanItem") = dr("PlanItem")
                    DRow("ArticleDescription") = dr("ArticleDescription")
                    DRow("ArticleUnitId") = dr("ArticleUnitId")
                    DRow("Unit") = dr("Unit")
                    DRow("Qty") = dr("Qty")
                    DRow("TotalQty") = dr("Qty")
                    DRow("ConsumedQty") = dr("ConsumedQty")
                    DRow("BalanceQty") = dr("BalanceQty")
                    DRow("Price") = dr("Price")

                    DRow("Tag#") = dr("Tag#")
                    DRow("SubDepartmentID") = dr("SubDepartmentID")
                    DRow("SubDepartment") = dr("SubDepartment")
                    DRow("PlanTicketsId") = dr("PlanTicketsId")
                    DRow("ParentId") = dr("ParentId")
                    DRow("UniqueId") = dr("UniqueId")
                    DRow("ParentUniqueId") = dr("ParentUniqueId")
                    DRow("ParentTag#") = dr("ParentTag#")
                    dtEstimationConsumption.Rows.Add(DRow)
                    'Dim Values = From row In dtGetCostSheet.AsEnumerable
                    '             Select row.Field(Of Integer)("Tag#")



                    'Dim Max As Integer = Convert.ToInt32(dtGetCostSheet.Compute("MAX(Tag#)", String.Empty))
                    'Dim Values As Integer = dtGetCostSheet.AsEnumerable().Select(Function(r) r.Field(Of Integer)("Tag#"))
                    'Dim Values As Integer = dtGetCostSheet.AsEnumerable().(Function(r) r.Field(Of Integer)("Tag#"))

                    'Dim Value As Integer =  

                    If DRow("Tag#") > 0 Then
                        'Dim IsConsumed1 As Integer = 0
                        'If GetCostSheet(dr("ArticleId")) = True Then
                        '    IsConsumed1 = 0
                        'Else
                        '    IsConsumed1 = IsConsumed
                        'End If
                        GetECChilds(DRow("Tag#"), IsConsumed, DRow("ArticleId"))
                    End If
                    '    Next
                    'Else
                    '    Dim ParentTag As Integer = TagNo
                    '    Dim DRow As DataRow
                    '    DRow = dtGetCostSheet.NewRow
                    '    DRow("Id") = dr("Id")
                    '    DRow("MaterialEstMasterID") = dr("MaterialEstMasterID")
                    '    DRow("MasterArticleID") = dr("MasterArticleID")
                    '    DRow("ArticleId") = dr("ArticleId")
                    '    DRow("PlanItem") = dr("PlanItem")
                    '    DRow("ArticleDescription") = dr("ArticleDescription")
                    '    DRow("ArticleUnitId") = dr("ArticleUnitId")
                    '    DRow("Unit") = dr("Unit")
                    '    DRow("Qty") = dr("Qty")
                    '    DRow("TotalQty") = dr("TotalQty")
                    '    DRow("Price") = dr("Price")
                    '    DRow("Types") = dr("Types")
                    '    DRow("SubDepartmentID") = dr("SubDepartmentID")
                    '    DRow("SubDepartment") = dr("SubDepartment")
                    '    DRow("Approve") = dr("Approve")
                    '    DRow("PlanTicketsId") = dr("PlanTicketsId")
                    '    DRow("CostSheetID") = dr("CostSheetID")
                    '    DRow("ParentId") = dr("ParentId")
                    '    DRow("SerialNo") = dr("SerialNo")
                    '    DRow("ParentSerialNo") = dr("ParentSerialNo")
                    '    DRow("ArticleId") = dr("ArticleId").ToString
                    '    DRow("UniqueId") = (dtGetCostSheet.Rows.Count + 1 + GridCount)
                    '    DRow("ParentUniqueId") = ParentUniqueId
                    '    DRow("ParentTag#") = ParentTagNo
                    '    If GetCostSheet(dr("ArticleId")) = True Then
                    '        DRow("Tag#") = TagNo
                    '        TagNo += 1
                    '    Else
                    '        DRow("Tag#") = 0
                    '    End If
                    '    'Dim Max As Integer = Convert.ToInt32(dtGetCostSheet.Compute("MAX(Tag#)", String.Empty))
                    '    dtGetCostSheet.Rows.Add(DRow)
                    '    GetChildArticles(DRow("ArticleId"), DRow("UniqueId"), DRow("Qty"), GridCount, TagNo, ParentTag)
                    'End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '' END TASK:1006 Added TagIds to estimation.
    Public Sub GetECChilds(ByVal TagId As Integer, ByVal IsConsumed As Boolean, ByVal ArticleId As Integer)
        Try
            DAL = New MaterialEstimationDAL()
            Dim dt As DataTable = DAL.ReadTable("Exec GetEstimationConsumption2 " & TagId & ", " & IsConsumed & ", " & ArticleId & "")
            dt.AcceptChanges()
            GetECParetn(dt, IsConsumed)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''TASK:1006 Added TagIds to estimation.

    Public Function ValidatePendingEstimation(ByVal EstimationId As Integer, ByVal DepartmentId As Integer, ByVal ArticleId As Integer) As Double
        Try
            Dim PendingEstQty As Double = 0
            DAL = New MaterialEstimationDAL()
            Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(EstimationDetail.Quantity, 0)-(IsNull(Qty, 0)-IsNull(ReturnedTotalQty, 0))) As PendingEstQty From DispatchDetailTable As DispatchDetail Right Outer Join MaterialEstimationDetailTable As EstimationDetail ON DispatchDetail.EstimationId = EstimationDetail.MaterialEstMasterID And DispatchDetail.SubDepartmentID = EstimationDetail.SubDepartmentID And DispatchDetail.ArticleDefId = EstimationDetail.ProductId Group By EstimationDetail.MaterialEstMasterID, EstimationDetail.SubDepartmentID, EstimationDetail.ProductId  Where EstimationDetail.MaterialEstMasterID=" & EstimationId & " And  EstimationDetail.SubDepartmentID =" & DepartmentId & " And EstimationDetail.ProductId =" & ArticleId & "")
            dt.AcceptChanges()
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                PendingEstQty = dt.Rows(0).Item(0)
            End If
            Return PendingEstQty
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetIssuedAgainstEstimation(ByVal EstimationId As Integer, ByVal DepartmentId As Integer, ByVal ArticleId As Integer) As Double
        Try
            Dim PendingEstQty As Double = 0
            DAL = New MaterialEstimationDAL()

            Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(Qty, 0)-IsNull(ReturnedTotalQty, 0)) As IssuedQty From DispatchDetailTable As Detail Where Detail.EstimationId=" & EstimationId & " And  Detail.SubDepartmentID =" & DepartmentId & " And Detail.ArticleDefId =" & ArticleId & "")
            'Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(Track.EstimatedQty, 0)-IsNull(Track.ConsumedQty, 0)) As IssuedQty From tblTrackEstimationConsumption As Track INNER JOIN ItemConsumptionDetail As Detail ON Track.EstimationId = Detail.EstimationId And Track.ArticleId = Detail.ArticleId And Track.DepartmentId = Detail.DepartmentId Where Detail.EstimationId=" & EstimationId & " And  Detail.DepartmentId =" & DepartmentId & " And Detail.ArticleId =" & ArticleId & "")
            dt.AcceptChanges()
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                PendingEstQty = Val(dt.Rows(0).Item(0).ToString)
            End If
            Return PendingEstQty
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK: TFS1272 Getting consumed quantity against estimation
    ''' </summary>
    ''' <param name="EstimationId"></param>
    ''' <param name="DepartmentId"></param>
    ''' <param name="ArticleId"></param>
    ''' <returns></returns>
    ''' <remarks>Getting consumed quantity upon which estimation should be reduced </remarks>
    Public Function GetConsumedAgainstEstimation(ByVal EstimationId As Integer, ByVal DepartmentId As Integer, ByVal ArticleId As Integer) As Double
        Try
            Dim PendingEstQty As Double = 0
            DAL = New MaterialEstimationDAL()
            'Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(Qty, 0)-IsNull(ReturnedTotalQty, 0)) As IssuedQty From DispatchDetailTable As Detail Where Detail.EstimationId=" & EstimationId & " And  Detail.SubDepartmentID =" & DepartmentId & " And Detail.ArticleDefId =" & ArticleId & "")
            'Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(Track.EstimatedQty, 0)-IsNull(Track.ConsumedQty, 0)) As IssuedQty From tblTrackEstimationConsumption As Track INNER JOIN ItemConsumptionDetail As Detail ON Track.EstimationId = Detail.EstimationId And Track.ArticleId = Detail.ArticleId And Track.DepartmentId = Detail.DepartmentId Where Detail.EstimationId=" & EstimationId & " And  Detail.DepartmentId =" & DepartmentId & " And Detail.ArticleId =" & ArticleId & "")
            'Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(Track.ConsumedQty, 0)) As IssuedQty From tblTrackEstimationConsumption As Track INNER JOIN ItemConsumptionDetail As Detail ON Track.EstimationId = Detail.EstimationId And Track.ArticleId = Detail.ArticleId And Track.DepartmentId = Detail.DepartmentId Where Detail.EstimationId=" & EstimationId & " And  Detail.DepartmentId =" & DepartmentId & " And Detail.ArticleId =" & ArticleId & "")
            Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(Track.ConsumedQty, 0)) As ConsumedQty From tblTrackEstimationConsumption As Track Where Track.EstimationId=" & EstimationId & " And  Track.DepartmentId =" & DepartmentId & " And Track.ArticleId =" & ArticleId & "")

            dt.AcceptChanges()
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                PendingEstQty = Val(dt.Rows(0).Item(0).ToString)
            End If
            Return PendingEstQty
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS1436
    ''' </summary>
    ''' <param name="EstimationId"></param>
    ''' <param name="DepartmentId"></param>
    ''' <param name="ArticleId"></param>
    ''' <param name="ParentTagId"></param>
    ''' <returns></returns>
    ''' <remarks> This function helps to get consumed quantity against estimation.</remarks>
    Public Function GetConsumedAgainstEstimation(ByVal EstimationId As Integer, ByVal DepartmentId As Integer, ByVal ArticleId As Integer, ByVal ParentTagId As Integer) As Double
        Try
            Dim PendingEstQty As Double = 0
            DAL = New MaterialEstimationDAL()
            'Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(Qty, 0)-IsNull(ReturnedTotalQty, 0)) As IssuedQty From DispatchDetailTable As Detail Where Detail.EstimationId=" & EstimationId & " And  Detail.SubDepartmentID =" & DepartmentId & " And Detail.ArticleDefId =" & ArticleId & "")
            'Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(Track.EstimatedQty, 0)-IsNull(Track.ConsumedQty, 0)) As IssuedQty From tblTrackEstimationConsumption As Track INNER JOIN ItemConsumptionDetail As Detail ON Track.EstimationId = Detail.EstimationId And Track.ArticleId = Detail.ArticleId And Track.DepartmentId = Detail.DepartmentId Where Detail.EstimationId=" & EstimationId & " And  Detail.DepartmentId =" & DepartmentId & " And Detail.ArticleId =" & ArticleId & "")
            'Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(Track.ConsumedQty, 0)) As IssuedQty From tblTrackEstimationConsumption As Track INNER JOIN ItemConsumptionDetail As Detail ON Track.EstimationId = Detail.EstimationId And Track.ArticleId = Detail.ArticleId And Track.DepartmentId = Detail.DepartmentId Where Detail.EstimationId=" & EstimationId & " And  Detail.DepartmentId =" & DepartmentId & " And Detail.ArticleId =" & ArticleId & "")
            'Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(Track.ConsumedQty, 0)) As ConsumedQty From tblTrackEstimationConsumption As Track Where Track.EstimationId=" & EstimationId & " And  Track.DepartmentId =" & DepartmentId & " And Track.ArticleId =" & ArticleId & " And Track.ParentTag# =" & ParentTagId & "")
            'Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(Consumption.Qty, 0)-IsNull(Track.ConsumedQty, 0)) As ConsumedQty From tblTrackEstimationConsumption As Track Inner Join ItemConsumptionDetail As Consumption ON Track.EstimationId=Consumption.EstimationId And Track.ArticleId = Consumption.ArticleId And Track.DepartmentId= Consumption.DepartmentId And Track.ParentTag# = Consumption.ParentTag#  Where Track.EstimationId=" & EstimationId & " And  Track.DepartmentId =" & DepartmentId & " And Track.ArticleId =" & ArticleId & " And Track.ParentTag# =" & ParentTagId & "")
            Dim dt As DataTable = DAL.ReadTable("Select Sum(IsNull(Track.ConsumedQty, 0)) As ConsumedQty From tblTrackEstimationConsumption As Track Where Track.EstimationId=" & EstimationId & " And  Track.DepartmentId =" & DepartmentId & " And Track.ArticleId =" & ArticleId & " And Track.ParentTag# =" & ParentTagId & "")

            dt.AcceptChanges()
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                PendingEstQty = Val(dt.Rows(0).Item(0).ToString)
            End If
            Return PendingEstQty
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
