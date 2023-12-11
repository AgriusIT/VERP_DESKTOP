Imports Janus.Windows.GridEX
Imports SBModel.MaterialAllocationModel
Imports SBModel

Public Class MaterialAllocationMaster
    Dim DAL As CRUD_db

    ' [int] NULL,
    '[Quantity] [int] NULL,
    '[Remarks] [varchar](100) NULL,
    Public Sub Save_Transation(ByVal table As DataTable, ByVal matAllObj As MaterialAllocationModel)
        Dim DetailQueryList As List(Of String) = New List(Of String)
        DAL = New CRUD_db()
        Dim MasterQuery As String = "insert into AllocationMaster(MasterDate, DocNo, tblMaterialEstimation_Id, Remarks, Status, TicketID, PlanId, SalesOrderId) values(N'" & matAllObj.DateEntry.ToString("yyyy-M-d hh:mm:ss tt") & "', N'" & matAllObj.DocNo & "' , " & matAllObj.tblMaterialEstimation_Id & ", N'" & matAllObj.Remarks & "', " & IIf(matAllObj.Status = True, 1, 0) & ", " & matAllObj.TicketID & ", " & matAllObj.PlanId & " , " & matAllObj.SalesOrderId & ")select @@Identity"
        Dim Detailquery As String
        For i As Integer = 0 To table.Rows.Count - 1
            Detailquery = "insert into AllocationDetail(Master_Allocation_ID, ProductID, ProductMasterID, DepartmentID, Quantity, Remarks, AllocDetailDate, MaterialEstimationDetailId, ParentId, SerialNo, ParentSerialNo, UniqueId, ParentUniqueId) values(`, " & table.Rows(i).Item("ProductID") & ", " & table.Rows(i).Item("ProductMasterID") & ", " & table.Rows(i).Item("DepartmentID") & ", " & table.Rows(i).Item("Quantity") & ", N'" & table.Rows(i).Item("Remarks").ToString.Replace("'", "''") & "' ,N'" & table.Rows(i).Item("AllocDetailDate") & "', " & table.Rows(i).Item("MaterialEstimationDetailId") & " , " & Val(table.Rows(i).Item("ParentId").ToString) & ", N'" & table.Rows(i).Item("SerialNo").ToString.Replace("'", "''") & "', N'" & table.Rows(i).Item("ParentSerialNo").ToString.Replace("'", "''") & "', " & Val(table.Rows(i).Item("UniqueId").ToString) & ", " & Val(table.Rows(i).Item("ParentUniqueId").ToString) & ")"
            DetailQueryList.Add(Detailquery)
        Next
        DAL.Master_Insertion(MasterQuery, DetailQueryList)
    End Sub

    Public Function GetRecordsByQuery(ByVal Query As String) As DataTable
        Try

            DAL = New CRUD_db()
            Dim table As New DataTable
            table = DAL.ReadTable(Query)
            table.AcceptChanges()
            Return table
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetMaster() As DataTable
        Try
            Dim MasterQuery As String = "Select ME.*, ArticleDefTable.ArticleDescription As PlanItem from MaterialEstimation ME Join ArticleDefTable On ME.PlanItemId = ArticleDefTable.ArticleId Left Join PlanTickets pt on Me.PlanTicketId = pt.PlanTicketsId Left Join PlanMasterTable pm on Me.MasterPlanId = pm.PlanId left join SalesOrderMasterTable som on som.SalesOrderId = Me.SaleOrderId"
            DAL = New CRUD_db()
            Dim table = DAL.ReadTable(MasterQuery)
            Return table
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetDetails(ByVal MasterID As Integer) As DataTable

        'Dim MasterQuery As String = "select * from MaterialEstimationDetailTable me join MaterialEstimation e on Me.MaterialEstMasterID = e.Id join ArticleDefTable ad on ad.ArticleId = Me.PlanItemId join ArticleDefTable dt on dt.ArticleId = Me.ProductId where Me.MaterialEstMasterID =" + MasterID
        Dim MasterQuery As String = "Select MED.Id, MED.MaterialEstMasterID, CS.MasterArticleID, CS.ArticleID, Article.ArticleDescription, SC.Qty, MED.Types, MED.Approve, IsNull(MED.ParentId, 0) As ParentId from MaterialEstimationDetailTable Inner Join MaterialEstimation ME ON MED.MaterialEstMasterID = ME.Id Right Outer Join tblCostSheet CS  ON MED.ProductId = CS.ArticleID Inner Join ArticleDefTable Article On CS.ArticleID = Article.ArticleId CS.MasterArticleID =" + MasterID
        DAL = New CRUD_db()
        Dim table = DAL.ReadTable(MasterQuery)
        Return table

    End Function
    Public Function DeleteMaster(ByVal MasterID As Integer)
        DAL = New CRUD_db()
        Dim MasterDetailQuery As String = "Delete from AllocationMaster where Master_Allocation_ID =" & MasterID
        Dim MasterQuery As String = "Delete from AllocationDetail where Master_Allocation_ID =" & MasterID
        DAL.Master_Deletion(MasterDetailQuery, MasterQuery)
        Return True
    End Function

    'Public Function Update_Transation1(ByVal grid As GridEX, ByVal matEstObj As MaterialEstimationModel) As List(Of String)
    '    Dim DetailQueryList As List(Of String) = New List(Of String)
    '    DAL = New CRUD_db()
    '    Dim MasterQuery As String = "update  MaterialEstimation set EstimationDate = '" + matEstObj.EstimationDate + "',MasterPlanId = '" + matEstObj.MasterPlanId + "' ,PlanTicketId='" + matEstObj.PlanTicketId + "',Remarks='" + matEstObj.Remarks + "' , docno='" + matEstObj.DocNo + "',saleorderId='" + matEstObj.SaleOrderId + "' where Id=" + matEstObj.Id
    '    Dim Detailquery As String
    '    For i As Integer = 0 To grid.RowCount - 1
    '        Detailquery = "insert into MaterialEstimationDetailTable (MaterialEstMasterID ,Quantity, Types ,Approve , PlanItemId , ProductId) values(`,'" + table.GetRow(i).Cells(3).Value.ToString() + "','" + table.GetRow(i).Cells(3).Value.ToString() + "','" + grid.GetRow(i).Cells(3).Value.ToString() + "','" + grid.GetRow(i).Cells(3).Value.ToString() + "','" + grid.GetRow(i).Cells(3).Value.ToString() + "' )"
    '        DetailQueryList.Add(Detailquery)
    '    Next
    '    Dim Update_Detail_Del As String = "Delete from MaterialEstimation where Id=" + matEstObj.Id

    '    DAL.Master_Update(MasterQuery, DetailQueryList, Update_Detail_Del)

    '    Return DetailQueryList
    'End Function
    Public Sub Update_Transation(ByVal table As DataTable, ByVal matAllObj As MaterialAllocationModel)
        Try
            Dim DetailQueryList As List(Of String) = New List(Of String)
            DAL = New CRUD_db()
            Dim MasterQuery As String = " Update AllocationMaster Set MasterDate = N'" & matAllObj.DateEntry.ToString("yyyy-M-d hh:mm:ss tt") & "', DocNo = N'" & matAllObj.DocNo & "', tblMaterialEstimation_Id = " & matAllObj.tblMaterialEstimation_Id & ", Remarks = N'" & matAllObj.Remarks & "', Status = " & IIf(matAllObj.Status = True, 1, 0) & ", TicketID = " & matAllObj.TicketID & " , PlanId = " & matAllObj.PlanId & " , SalesOrderId = " & matAllObj.SalesOrderId & " Where Master_Allocation_ID = " & matAllObj.Master_Allocation_ID & ""
            Dim Detailquery As String
            For i As Integer = 0 To table.Rows.Count - 1
                Detailquery = "insert into AllocationDetail(Master_Allocation_ID ,ProductID, ProductMasterID, DepartmentID, Quantity, Remarks, AllocDetailDate, MaterialEstimationDetailId, ParentId, SerialNo, ParentSerialNo, UniqueId, ParentUniqueId) values(`, " & table.Rows(i).Item("ProductID") & ", " & table.Rows(i).Item("ProductMasterID") & ", " & table.Rows(i).Item("DepartmentID") & ", " & table.Rows(i).Item("Quantity") & ", N'" & table.Rows(i).Item("Remarks") & "' , N'" & table.Rows(i).Item("AllocDetailDate") & "', " & table.Rows(i).Item("MaterialEstimationDetailId") & ", N'" & table.Rows(i).Item("SerialNo").ToString.Replace("'", "''") & "', N'" & table.Rows(i).Item("ParentSerialNo").ToString.Replace("'", "''") & "', " & Val(table.Rows(i).Item("UniqueId").ToString) & ", " & Val(table.Rows(i).Item("ParentUniqueId").ToString) & ")"
                DetailQueryList.Add(Detailquery)
            Next
            Dim Update_Detail_Del As String = "Delete from AllocationDetail where Master_Allocation_ID =" & matAllObj.Master_Allocation_ID
            DAL.Master_Update(matAllObj.Master_Allocation_ID, MasterQuery, DetailQueryList, Update_Detail_Del)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
