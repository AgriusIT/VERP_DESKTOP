Imports SBModel
Imports System.Data.SqlClient

Public Class PlanDetailTableDAL
    Function Add(ByVal objModel As PlanDetailTableBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As PlanDetailTableBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  PlanDetailTable (PlanId, LocationId, ArticleDefId, ArticleSize, Sz1, Sz2, Sz3, Sz4, Sz5, Sz6, Sz7, Qty, Price, CurrentPrice, Pack_Desc, ProducedQty, ProductionQty, SODetailId, SOId, PLevelId, Comments, ProducedTotalQty, PlanTicketsDetailID, TicketIssuedQty, ArticleAliasName, SalesOrderId) values (N'" & objModel.PlanId & "', N'" & objModel.LocationId & "', N'" & objModel.ArticleDefId & "', N'" & objModel.ArticleSize.Replace("'", "''") & "', N'" & objModel.Sz1 & "', N'" & objModel.Sz2 & "', N'" & objModel.Sz3 & "', N'" & objModel.Sz4 & "', N'" & objModel.Sz5 & "', N'" & objModel.Sz6 & "', N'" & objModel.Sz7 & "', N'" & objModel.Qty & "', N'" & objModel.Price & "', N'" & objModel.CurrentPrice & "', N'" & objModel.Pack_Desc.Replace("'", "''") & "', N'" & objModel.ProducedQty & "', N'" & objModel.ProductionQty & "', N'" & objModel.SODetailId & "', N'" & objModel.SOId & "', N'" & objModel.PLevelId & "', N'" & objModel.Comments.Replace("'", "''") & "', N'" & objModel.ProducedTotalQty & "', N'" & objModel.PlanTicketsDetailID & "', N'" & objModel.TicketIssuedQty & "', N'" & objModel.ArticleAliasName.Replace("'", "''") & "', N'" & objModel.SalesOrderId & "') Select @@Identity"
            objModel.PlanDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Insert(ByVal objModel As PlanMasterTableBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Insert(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
            Return False
        End Try
    End Function
    Public Shared Function Insert(ByVal Obj As PlanMasterTableBE, trans As SqlTransaction) As Boolean
        'Obj.LocationId = Val(Row.Item("LocationId").ToString)
        'Obj.ArticleDefId = Val(Row.Item("ArticleDefId").ToString)
        'Obj.ArticleSize = Row.Item("ArticleSize").ToString
        'Obj.Sz1 = Row.Item("Sz1").ToString
        'Obj.Sz2 = Val(Row.Item("Sz2").ToString)
        'Obj.Sz3 = Val(Row.Item("Sz3").ToString)
        'Obj.Sz4 = Val(Row.Item("Sz4").ToString)
        'Obj.Sz5 = Val(Row.Item("Sz5").ToString)
        'Obj.Sz6 = Val(Row.Item("Sz6").ToString)
        'Obj.Sz7 = Val(Row.Item("Sz7").ToString)
        'Obj.Qty = Val(Row.Item("Qty").ToString)
        'Obj.Price = Val(Row.Item("Price").ToString)
        'Obj.Sz4 = Val(Row.Item("MasterArticleId").ToString)
        'Obj.CurrentPrice = Val(Row.Item("CurrentPrice").ToString)
        'Obj.TicketIssuedQty = Val(Row.Item("TicketQty").ToString)
        'Obj.SalesOrderId = Val(Row.Item("SalesOrderId").ToString)
        'Obj.SODetailId = Val(Row.Item("SalesOrderDetailId").ToString)
        Try
            For Each objModel As PlanDetailTableBE In Obj.Detail

                Dim strSQL As String = String.Empty
                strSQL = " insert into  PlanDetailTable (PlanId, LocationId, ArticleDefId, ArticleSize, Sz1, Sz2, Sz3, Sz4, Sz5, Sz6, Sz7, Qty, Price, CurrentPrice, SODetailId, SOId) " _
                         & " values (" & objModel.PlanId & ", " & objModel.LocationId & ", " & objModel.ArticleDefId & ", N'" & objModel.ArticleSize.Replace("'", "''") & "', " _
                         & " " & objModel.Sz1 & ", " & objModel.Sz2 & ", " & objModel.Sz3 & ", " & objModel.Sz4 & ", " & objModel.Sz5 & ", " & objModel.Sz6 & ", " _
                         & " " & objModel.Sz7 & ", " & objModel.Qty & ", " & objModel.Price & ", " & objModel.CurrentPrice & ", " & objModel.SODetailId & ", " & objModel.SOId & ") Select @@Identity"
                objModel.PlanDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                'objModel.ActivityLog.ActivityName = "Save"
                'objModel.ActivityLog.RecordType = "Configuration"
                ''objModel.ActivityLog.RefNo = ""
                'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Next
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Function Update(ByVal objModel As PlanDetailTableBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As PlanDetailTableBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update PlanDetailTable set PlanId= N'" & objModel.PlanId & "', LocationId= N'" & objModel.LocationId & "', ArticleDefId= N'" & objModel.ArticleDefId & "', ArticleSize= N'" & objModel.ArticleSize.Replace("'", "''") & "', Sz1= N'" & objModel.Sz1 & "', Sz2= N'" & objModel.Sz2 & "', Sz3= N'" & objModel.Sz3 & "', Sz4= N'" & objModel.Sz4 & "', Sz5= N'" & objModel.Sz5 & "', Sz6= N'" & objModel.Sz6 & "', Sz7= N'" & objModel.Sz7 & "', Qty= N'" & objModel.Qty & "', Price= N'" & objModel.Price & "', CurrentPrice= N'" & objModel.CurrentPrice & "', Pack_Desc= N'" & objModel.Pack_Desc.Replace("'", "''") & "', ProducedQty= N'" & objModel.ProducedQty & "', ProductionQty= N'" & objModel.ProductionQty & "', SODetailId= N'" & objModel.SODetailId & "', SOId= N'" & objModel.SOId & "', PLevelId= N'" & objModel.PLevelId & "', Comments= N'" & objModel.Comments.Replace("'", "''") & "', ProducedTotalQty= N'" & objModel.ProducedTotalQty & "', PlanTicketsDetailID= N'" & objModel.PlanTicketsDetailID & "', TicketIssuedQty= N'" & objModel.TicketIssuedQty & "', ArticleAliasName= N'" & objModel.ArticleAliasName.Replace("'", "''") & "', SalesOrderId= N'" & objModel.SalesOrderId & "' where PlanDetailId=" & objModel.PlanDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As PlanDetailTableBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal objModel As PlanDetailTableBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from PlanDetailTable  where PlanDetailId= " & objModel.PlanDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select PlanDetailId, PlanId, LocationId, ArticleDefId, ArticleSize, Sz1, Sz2, Sz3, Sz4, Sz5, Sz6, Sz7, Qty, Price, CurrentPrice, Pack_Desc, ProducedQty, ProductionQty, SODetailId, SOId, PLevelId, Comments, ProducedTotalQty, PlanTicketsDetailID, TicketIssuedQty, ArticleAliasName, SalesOrderId from PlanDetailTable  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select PlanDetailId, PlanId, LocationId, ArticleDefId, ArticleSize, Sz1, Sz2, Sz3, Sz4, Sz5, Sz6, Sz7, Qty, Price, CurrentPrice, Pack_Desc, ProducedQty, ProductionQty, SODetailId, SOId, PLevelId, Comments, ProducedTotalQty, PlanTicketsDetailID, TicketIssuedQty, ArticleAliasName, SalesOrderId from PlanDetailTable  where PlanDetailId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
