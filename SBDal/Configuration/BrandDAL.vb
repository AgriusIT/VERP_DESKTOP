Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class BrandDAL
    Public Function Add(ByVal col As BrandBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Dim strSQL As String = "insert into ArticleBrandDefTable (ArticleBrandName,[Description],Active,SortOrder) values (N'" & col.ArticleBrandName.Replace("'", "''") & "',N'" & col.Description.Replace("'", "''") & "'," & IIf(col.Active = True, 1, 0) & ",N'" & col.SortOrder & "')Select @@Identity"
            col.ArticleBrandId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            ''add activity log
            col.ActivityLog.ActivityName = "Save"
            col.ActivityLog.RecordType = "Configuration"
            col.ActivityLog.RefNo = col.ArticleBrandName
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
