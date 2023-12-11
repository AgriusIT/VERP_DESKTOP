Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class UnitDAL
    Public Function Add(ByVal objModel As Unit) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try

            'insert 
            Dim strSQL As String = "INSERT INTO ArticleUnitDefTable   (ArticleUnitName, Comments, Active, SortOrder)" & _
            " values(N'" & objModel.Name.Trim.Replace("'", "''") & "'," & _
             " N'" & objModel.Comments & "',  " & IIf(objModel.Active = False, 0, 1) & ", " & objModel.SortOrder & " " & ") Select @@Identity"

            objModel.ID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))

            ''add activity log
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.ID
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            trans.Commit()
            Return True

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function UpdateRec(ByVal objModel As Unit) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()

        Try
            'Update Record
            Dim strSQL As String = "UPDATE ArticleUnitDefTable set ArticleUnitName= N'" & objModel.Name & "', Comments=N'" & objModel.Comments & "', Active=" & IIf(objModel.Active = False, 0, 1) & ",  SortOrder=" & objModel.SortOrder & " WHERE ArticleUnitId=" & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.ID
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            trans.Commit()
            Return True

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function DeleteRec(ByVal objModel As Unit) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Dim srtSQL As String = "Delete From ArticleUnitDefTable WHERE ArticleUnitId=" & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, srtSQL)

            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.ID
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            trans.Commit()
            Return True
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Function GetAllRecords() As DataTable
        Try
            Dim strsql As String = "Select * From ArticleUnitDefTable"
            Dim dt As New DataTable
            Dim da As SqlClient.SqlDataAdapter
            da = New SqlClient.SqlDataAdapter(strsql, SQLHelper.CON_STR)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
