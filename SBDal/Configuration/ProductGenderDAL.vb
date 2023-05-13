Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class ProductGenderDAL

    Public Function Add(ByVal objModel As ProductGender) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()

        Try

            'insert 
            Dim strSQL As String = "INSERT INTO ArticleGenderDefTable   (ArticleGenderName, Active, SortOrder)" & _
            " values(N'" & objModel.Name.Trim.Replace("'", "''") & "'," & _
             "'1',0 " & ") Select @@Identity"

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

End Class
