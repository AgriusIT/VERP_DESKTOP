Imports SBModel
Imports System.Data.SqlClient

Public Class StockAuditReportMasterDAL
    Function Add(ByVal objModel As StockAuditReportMasterBE) As Boolean
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
    Function Add(ByVal objModel As StockAuditReportMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Insert into  StockAuditReportMaster(DocumentDate, DocumentNo, StockAuditId) values (N'" & objModel.DocumentDate.ToString("yyyy-M-dd h:mm:ss tt") & "', N'" & objModel.DocumentNo.Replace("'", "''") & "', " & objModel.StockAuditId & ") Select @@Identity"
            objModel.ID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Call New StockAuditReportDetailDAL().Add(objModel, trans)
            Call New StockDAL().Add(objModel.Stock, trans)
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As StockAuditReportMasterBE) As Boolean
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
    Function Update(ByVal objModel As StockAuditReportMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update StockAuditReportMaster set DocumentDate= N'" & objModel.DocumentDate.ToString("yyyy-M-dd h:mm:ss tt") & "', DocumentNo= N'" & objModel.DocumentNo.Replace("'", "''") & "', StockAuditId= " & objModel.StockAuditId & " where ID=" & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Call New StockAuditReportDetailDAL().Add(objModel, trans)

            'objModel.ActivityLog.ActivityName = "Update"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As StockAuditReportMasterBE) As Boolean
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
    Function Delete(ByVal objModel As StockAuditReportMasterBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from StockAuditReportMaster  WHERE ID= " & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete from StockAuditReportDetail  WHERE StockAuditReportId = " & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Call New StockDAL().Delete(objModel.Stock, trans)
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
            strSQL = " Select StockAuditReportMaster.ID,  StockAuditReportMaster.DocumentNo, StockAuditReportMaster.DocumentDate, StockAuditReportMaster.StockAuditId, StockAuditTable.StockAuditName From StockAuditReportMaster LEFT OUTER JOIN StockAuditTable ON StockAuditReportMaster.StockAuditId = StockAuditTable.ID ORDER BY StockAuditReportMaster.ID DESC"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, DocumentDate, DocumentNo, StockAuditId from StockAuditReportMaster  where ID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetStock() As DataTable
        Dim Query As String = String.Empty
        Try
            Query = " SELECT Article.ArticleId, Article.ArticleCode, Article.ArticleDescription, SUM(ISNULL(Stock.InQty, 0)-IsNull(Stock.OutQty, 0)) AS StockQty, SUM(ISNULL(Stock.In_PackQty, 0)-IsNull(Stock.Out_PackQty, 0)) AS PackStockQty, Stock.LocationId, IsNull(Rate.Rate, 0) AS Rate, Article.ArticleBARCode FROM StockDetailTable AS Stock INNER JOIN ArticleDefTable AS Article ON Stock.ArticleDefId = Article.ArticleId " _
                    & " Left Outer Join (SELECT Rate, ArticleDefId FROM StockDetailTable AS Stock WHERE StockTransDetailId IN (SELECT Max(StockTransDetailId) FROM StockDetailTable WHERE ISNULL(InQty, 0) > 0 Group By ArticleDefId)) AS Rate ON Article.ArticleId = Rate.ArticleDefId Group By Article.ArticleId, Article.ArticleCode, Article.ArticleDescription, Stock.LocationId, Rate.Rate, Article.ArticleBARCode "
            Dim dt As DataTable = UtilityDAL.GetDataTable(Query)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

