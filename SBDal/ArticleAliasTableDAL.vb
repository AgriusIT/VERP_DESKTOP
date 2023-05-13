Imports SBModel
Imports System.Data.SqlClient

Public Class ArticleAliasTableDAL
    Function Add(ByVal objModel As List(Of ArticleAliasTableBE)) As Boolean
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
    Function Add(ByVal objMod As List(Of ArticleAliasTableBE), trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each objModel As ArticleAliasTableBE In objMod
                strSQL = "insert into  ArticleAliasTable (MasterId, ArticleId, ArticleAliasName, VendorID, SizeId, ColorId) values (N'" & objModel.MasterId & "', N'" & objModel.ArticleId & "', N'" & objModel.ArticleAliasName.Replace("'", "''") & "', N'" & objModel.VendorID & "', N'" & objModel.SizeId & "', N'" & objModel.ColorId & "') Select @@Identity"
                objModel.ArticleAliasID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
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

    Function Update(ByVal objModel As List(Of ArticleAliasTableBE)) As Boolean
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
    Function Update(ByVal objMod As List(Of ArticleAliasTableBE), trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each objModel As ArticleAliasTableBE In objMod
                strSQL = "update ArticleAliasTable set ArticleAliasName= N'" & objModel.ArticleAliasName.Replace("'", "''") & "', SizeId= N'" & objModel.SizeId & "', ColorId= N'" & objModel.ColorId & "' where MasterId= " & objModel.MasterId & " And ArticleId= " & objModel.ArticleId & " And  VendorID= " & objModel.VendorID & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            'objModel.ActivityLog.ActivityName = "Update"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As ArticleAliasTableBE) As Boolean
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
    Function Delete(ByVal objModel As ArticleAliasTableBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.empty
        Try
            strSQL = "Delete from ArticleAliasTable  where MasterId= " & objModel.MasterId & " And VendorId  = " & objModel.VendorID & ""
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
        Dim strSQL As String = String.empty
        Try
            strSQL = " Select ArticleAliasID, MasterId, ArticleId, ArticleAliasName, VendorID, SizeId, ColorId from ArticleAliasTable  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.empty
        Try
            strSQL = " Select ArticleAliasID, MasterId, ArticleId, ArticleAliasName, VendorID, SizeId, ColorId from ArticleAliasTable  where ArticleAliasID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
