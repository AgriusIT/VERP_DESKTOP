Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class ColorDAL
    Public Function Add(ByVal col As ColorBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Dim strSQL As String = "insert into ArticleColorDefTable (ArticleColorName,Comments,Active,SortOrder,IsDate,ColorCode) values (N'" & col.ArticleColorName.Replace("'", "''") & "',N'" & col.Comments.Replace("'", "''") & "'," & IIf(col.Active = True, 1, 0) & ",N'" & col.SortOrder & "',N'" & col.IsDate.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & col.ColorCode.Replace("'", "''") & "')Select @@Identity"
            col.ArticleColorId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            ''add activity log
            col.ActivityLog.ActivityName = "Save"
            col.ActivityLog.RecordType = "Configuration"
            col.ActivityLog.RefNo = col.ColorCode
            UtilityDAL.BuildActivityLog(col.ActivityLog, trans)


            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
End Class
