Imports SBModel
Imports System.Data.SqlClient
Public Class ProductionOrderOutputMaterialDAL
    Function Add(ByVal objModel As ProductionOrderOutputMaterialBE) As Boolean
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
    Function Add(ByVal objModel As ProductionOrderOutputMaterialBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  ProductionOrderOutputMaterial (ProductionOrderId, LocationId,  ItemId, ItemType, Unit, Qty, PackQty, Rate, TotalQty) values (N'" & objModel.ProductionOrderId & "', N'" & objModel.LocationId & "', N'" & objModel.ItemId & "', N'" & objModel.ItemType.Replace("'", "''") & "', N'" & objModel.Unit.Replace("'", "''") & "', N'" & objModel.Qty & "', N'" & objModel.PackQty & "', N'" & objModel.Rate & "', N'" & objModel.TotalQty & "') Select @@Identity"
            objModel.ID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Add(ByVal Obj As ProductionOrderBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each objModel As ProductionOrderOutputMaterialBE In Obj.OutputList
                If objModel.ID = 0 Then
                    strSQL = "insert into  ProductionOrderOutputMaterial (ProductionOrderId, LocationId, ItemId, ItemType, Unit, Qty, PackQty, Rate, TotalQty) values (" & Obj.ProductionOrderId & ", " & objModel.LocationId & ", " & objModel.ItemId & ", N'" & objModel.ItemType.Replace("'", "''") & "', '" & objModel.Unit.Replace("'", "''") & "', " & objModel.Qty & ", " & objModel.PackQty & ", " & objModel.Rate & ", " & objModel.TotalQty & ") "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                Else
                    strSQL = " Update ProductionOrderOutputMaterial SET ProductionOrderId = " & Obj.ProductionOrderId & ", LocationId = " & objModel.LocationId & ", ItemId= " & objModel.ItemId & ", ItemType = N'" & objModel.ItemType.Replace("'", "''") & "', Unit = N'" & objModel.Unit.Replace("'", "''") & "', Qty =" & objModel.Qty & " , PackQty= " & objModel.PackQty & ", Rate= " & objModel.Rate & ", TotalQty = " & objModel.TotalQty & " WHERE ID = " & objModel.ID & ""
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

    Function Update(ByVal objModel As ProductionOrderOutputMaterialBE) As Boolean
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
    Function Update(ByVal objModel As ProductionOrderOutputMaterialBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update ProductionOrderOutputMaterial set ProductionOrderId= N'" & objModel.ProductionOrderId & "', LocationId= N'" & objModel.LocationId & "', ItemId= N'" & objModel.ItemId & "', Unit= N'" & objModel.Unit.Replace("'", "''") & "', Qty= N'" & objModel.Qty & "', PackQty= N'" & objModel.PackQty & "', Rate= N'" & objModel.Rate & "', TotalQty= N'" & objModel.TotalQty & "' where ID=" & objModel.ID
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

    Public Shared Function Delete(ByVal objModel As ProductionOrderOutputMaterialBE) As Boolean
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
    Public Shared Function Delete(ByVal objModel As ProductionOrderOutputMaterialBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ProductionOrderOutputMaterial  where ID= " & objModel.ID
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
            strSQL = "Delete from ProductionOrderOutputMaterial  WHERE ProductionOrderId= " & ProductionOrderId
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

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, ProductionOrderId, LocationId, ItemId, ItemType, Unit, Qty, PackQty, Rate, TotalQty from ProductionOrderOutputMaterial  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetOutputs(ByVal ProductionOrderId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select Output.ID, Output.ProductionOrderId, Output.LocationId, Output.ItemId, Article.ArticleDescription AS Item, Output.ItemType, Output.Unit, Output.Qty, Output.PackQty, Output.Rate, Output.TotalQty, (ISNULL(Output.Rate, 0)*IsNull(Output.TotalQty, 0)) As NetAmount, IsNull(ArticleGroup.SubSubId, 0) As SubSubId, IsNull(ArticleGroup.CGSAccountId, 0) As CGSAccountId from ProductionOrderOutputMaterial AS Output " _
                   & " INNER JOIN ArticleDefTable AS Article ON Output.ItemId = Article.ArticleId LEFT OUTER JOIN ArticleGroupDefTable AS ArticleGroup ON Article.ArticleGroupId = ArticleGroup.ArticleGroupId WHERE Output.ProductionOrderId = " & ProductionOrderId & "  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            dt.Columns("NetAmount").Expression = "IsNull(TotalQty, 0) * IsNull(Rate, 0)"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, ProductionOrderId, LocationId, ItemId, Unit, Qty, PackQty, Rate, TotalQty from ProductionOrderOutputMaterial  where ID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
