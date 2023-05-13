Imports SBDal
Imports SBModel
Imports System.Data.SqlClient

Public Class StockAuditDAL
    Function Add(ByVal objModel As StockAuditTableBE) As Boolean
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
    Function Add(ByVal objModel As StockAuditTableBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  StockAuditTable (AuditDate, StockAuditName, SessionName, Remarks, IsClosed) values (N'" & objModel.StockDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objModel.StockAuditName.Replace("'", "''") & "', N'" & objModel.SessionName.Replace("'", "''") & "', N'" & objModel.Remarks.Replace("'", "''") & "', " & IIf(objModel.IsClosed = True, 1, 0) & ") Select @@Identity"
            objModel.ID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Call New StockAuditLocationsDAL().Add(objModel)
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As StockAuditTableBE) As Boolean
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
    Function Update(ByVal objModel As StockAuditTableBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update StockAuditTable set AuditDate= '" & objModel.StockDate.ToString("yyyy-M-d h:mm:ss tt") & "', StockAuditName= N'" & objModel.StockAuditName.Replace("'", "''") & "', SessionName= N'" & objModel.SessionName.Replace("'", "''") & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "', IsClosed = " & IIf(objModel.IsClosed = True, 1, 0) & " WHERE ID=" & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete from StockAuditLocations  where StockAuditId= " & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Call New StockAuditLocationsDAL().Add(objModel)
            'objModel.ActivityLog.ActivityName = "Update"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            '
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As StockAuditTableBE) As Boolean
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
    Function Delete(ByVal objModel As StockAuditTableBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from StockAuditTable  where ID= " & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete from StockAuditLocations  where StockAuditId= " & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, AuditDate, StockAuditName, SessionName, Remarks, Convert(BIT, IsNull(IsClosed, 0)) AS IsClosed from StockAuditTable ORDER BY ID DESC  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, Date, StockAuditName, SessionName, Remarks from StockAuditTable  where ID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetAticlesStock() As DataTable
        Dim Query As String = String.Empty
        Try
            Query = "SELECT Article.ArticleId, Article.ArticleCode, Article.ArticleDescription, SUM(ISNULL(Stock.InQty, 0)-IsNull(Stock.OutQty, 0)) AS StockQty FROM StockDetailTable AS Stock INNER JOIN ArticleDefTable AS Article ON Stock.ArticleDefId = Article.ArticleId Group By Article.ArticleId, Article.ArticleCode, Article.ArticleDescription"
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetAticlesStock(ByVal ArticleId As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            Query = "SELECT Article.ArticleId, Article.ArticleCode, Article.ArticleDescription, SUM(ISNULL(Stock.InQty, 0)-IsNull(Stock.OutQty, 0)) AS StockQty, 0 AS Qty FROM StockDetailTable AS Stock INNER JOIN ArticleDefTable AS Article ON Stock.ArticleDefId = Article.ArticleId WHERE Article.ArticleId = " & ArticleId & " Group By Article.ArticleId, Article.ArticleCode, Article.ArticleDescription"
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
