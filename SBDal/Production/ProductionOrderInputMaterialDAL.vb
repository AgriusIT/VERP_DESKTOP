Imports SBModel
Imports System.Data.SqlClient


Public Class ProductionOrderInputMaterialDAL
    Function Add(ByVal objModel As ProductionOrderInputMaterialBE) As Boolean
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
    Function Add(ByVal objModel As ProductionOrderInputMaterialBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  ProductionOrderInputMaterial (ProductionOrderId, LocationId, ItemId, Unit, Qty, PackQty, Rate, TotalQty) values (N'" & objModel.ProductionOrderId & "', N'" & objModel.LocationId & "', N'" & objModel.ItemId & "', N'" & objModel.Unit.Replace("'", "''") & "', N'" & objModel.Qty & "', N'" & objModel.PackQty & "', N'" & objModel.Rate & "', N'" & objModel.TotalQty & "') Select @@Identity"
            objModel.ID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Add(ByVal Obj As ProductionOrderBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each objModel As ProductionOrderInputMaterialBE In Obj.InputList
                If objModel.ID = 0 Then
                    strSQL = "insert into  ProductionOrderInputMaterial (ProductionOrderId, LocationId, ItemId, Unit, Qty, PackQty, Rate, TotalQty , FinishGoodId) values (" & Obj.ProductionOrderId & ", " & objModel.LocationId & ", " & objModel.ItemId & ", '" & objModel.Unit.Replace("'", "''") & "', " & objModel.Qty & ", " & objModel.PackQty & ", " & objModel.Rate & ", " & objModel.TotalQty & " , " & objModel.FinishGoodId & " ) "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                Else
                    strSQL = " Update ProductionOrderInputMaterial SET ProductionOrderId = " & Obj.ProductionOrderId & ", LocationId = " & objModel.LocationId & ", ItemId= " & objModel.ItemId & ", Unit = N'" & objModel.Unit.Replace("'", "''") & "', Qty =" & objModel.Qty & " , PackQty= " & objModel.PackQty & ", Rate= " & objModel.Rate & ", TotalQty = " & objModel.TotalQty & " , FinishGoodId = " & objModel.FinishGoodId & " WHERE ID = " & objModel.ID & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
            Next
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As ProductionOrderInputMaterialBE) As Boolean
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
    Function Update(ByVal objModel As ProductionOrderInputMaterialBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update ProductionOrderInputMaterial set ProductionOrderId= N'" & objModel.ProductionOrderId & "', LocationId= N'" & objModel.LocationId & "', ItemId= N'" & objModel.ItemId & "', Unit= N'" & objModel.Unit.Replace("'", "''") & "', Qty= N'" & objModel.Qty & "', PackQty= N'" & objModel.PackQty & "', Rate= N'" & objModel.Rate & "', TotalQty= N'" & objModel.TotalQty & "' where ID=" & objModel.ID
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

    Public Shared Function Delete(ByVal objModel As ProductionOrderInputMaterialBE) As Boolean
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
    Public Shared Function Delete(ByVal objModel As ProductionOrderInputMaterialBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ProductionOrderInputMaterial  where ID= " & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Delete(ByVal ProductionOrderId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ProductionOrderInputMaterial  where ProductionOrderId= " & ProductionOrderId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, ProductionOrderId, LocationId, ItemId, Unit, Qty, PackQty, Rate, TotalQty from ProductionOrderInputMaterial  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetInputs(ByVal ProductionOrderId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select Input.ID, Input.ProductionOrderId, Input.LocationId, Input.ItemId, Article.ArticleDescription AS Item, Input.Unit, Input.Qty, Input.PackQty, Input.Rate, Input.TotalQty, (ISNULL(Input.Rate, 0)*IsNull(Input.TotalQty, 0)) As NetAmount, IsNull(ArticleGroup.SubSubId, 0) As SubSubId, IsNull(ArticleGroup.CGSAccountId, 0) As CGSAccountId, IsNull(Article.MasterId, 0) As MasterArticleId , FinishGoodId from ProductionOrderInputMaterial AS Input " _
                   & " INNER JOIN ArticleDefTable AS Article ON Input.ItemId = Article.ArticleId LEFT OUTER JOIN ArticleGroupDefTable AS ArticleGroup ON Article.ArticleGroupId = ArticleGroup.ArticleGroupId  WHERE Input.ProductionOrderId = " & ProductionOrderId & "  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            'dt.Columns("TotalQty").Expression = "IsNull(Qty,0)*IsNull(PackQty, 0)"
            dt.Columns("NetAmount").Expression = "IsNull(TotalQty,0)*IsNull(Rate, 0)"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, ProductionOrderId, LocationId, ItemId, Unit, Qty, PackQty, Rate, TotalQty from ProductionOrderInputMaterial  where ID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
