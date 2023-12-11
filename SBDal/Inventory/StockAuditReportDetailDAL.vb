Imports SBDal
Imports SBModel
Imports System.Data.SqlClient

Public Class StockAuditReportDetailDAL
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
    Function Add(ByVal Obj As StockAuditReportMasterBE, trans As SqlTransaction) As Boolean
        Try

            For Each objModel As StockAuditReportDetailBE In Obj.Detail
                Dim strSQL As String = String.Empty
                If objModel.ID = 0 Then
                    strSQL = "INSERT into  StockAuditReportDetail (StockAuditReportId, ArticleId, Unit, PackQty, Qty, TotalQty, LocationId) values (" & Obj.ID & ", " & objModel.ArticleId & ", N'" & objModel.Unit.Replace("'", "''") & "', " & objModel.PackQty & ", " & objModel.Qty & ", " & objModel.TotalQty & " , " & objModel.LocationId & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                Else
                    strSQL = "UPDATE  StockAuditReportDetail SET StockAuditReportId = " & Obj.ID & ", ArticleId = " & objModel.ArticleId & ", Unit = N'" & objModel.Unit.Replace("'", "''") & "', PackQty = " & objModel.PackQty & ", Qty = " & objModel.Qty & ", TotalQty = " & objModel.TotalQty & ", LocationId = " & objModel.LocationId & " WHERE ID = " & objModel.ID & ""
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

    Function Update(ByVal objModel As StockAuditReportDetailBE) As Boolean
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
    Function Update(ByVal objModel As StockAuditReportDetailBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update StockAuditReportDetail set StockAuditReportId= N'" & objModel.StockAuditReportId & "', ArticleId= N'" & objModel.ArticleId & "', Unit= N'" & objModel.Unit.Replace("'", "''") & "', PackQty= N'" & objModel.PackQty & "', Qty= N'" & objModel.Qty & "', TotalQty= N'" & objModel.TotalQty & "' where ID=" & objModel.ID
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

    Function Delete(ByVal objModel As StockAuditReportDetailBE) As Boolean
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
    Function Delete(ByVal objModel As StockAuditReportDetailBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from StockAuditReportDetail  where ID= " & objModel.ID
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

    Function GetAll(ByVal StockAuditReportId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Select Detail.ID, Detail.StockAuditReportId, Detail.LocationId, Location.location_name AS Location, Detail.ArticleId, Article.ArticleCode, Article.ArticleDescription, Detail.Unit, IsNull(Detail.Rate, 0) AS Rate, IsNull(Detail.PackQty, 1) AS PackQty, IsNull(Detail.Qty, 0) AS Qty, 0 AS PackStockQty, 0 AS StockQty, IsNull(Detail.TotalQty, 0) AS TotalQty, IsNull(Detail.Qty, 0) AS BalancePackQty, IsNull(Detail.TotalQty, 0) AS BalanceQty, Article.ArticleBARCode FROM StockAuditReportDetail AS Detail INNER JOIN ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT OUTER JOIN tblDefLocation AS Location ON Detail.LocationId = Location.location_id WHERE Detail.StockAuditReportId = " & StockAuditReportId & " ORDER BY Detail.ID DESC"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            'dt.Columns("BalanceQty").Expression = "Case WHEN TotalQty > 0 THEN StockQty+TotalQty ELSE StockQty-TotalQty END "
            'dt.Columns("BalancePackQty").Expression = "Case WHEN Qty > 0 THEN PackStockQty+Qty ELSE PackStockQty-Qty END"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, StockAuditReportId, ArticleId, Unit, PackQty, Qty, TotalQty from StockAuditReportDetail  where ID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

