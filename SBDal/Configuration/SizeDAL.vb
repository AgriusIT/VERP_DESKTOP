Imports SBDal
Imports SBUtility.Utility
Imports SBModel
Imports System.Data.SqlClient
Public Class SizeDAL
    Public Function Add(ByVal Size As SizeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Dim strSQL As String = "insert into ArticleSizeDefTable (ArticleSizeName,SizeCode,Active,SortOrder,IsDate) values (N'" & Size.SizeName.Replace("'", "''") & "',N'" & Size.SizeCode.Replace("'", "''") & "',1,1,N'" & Now.ToString("yyyy-M-d h:mm:ss tt") & "') Select @@Identity"
            Size.SizeId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            ''add activity log
            Size.ActivityLog.ActivityName = "Save"
            Size.ActivityLog.RecordType = "Configuration"
            Size.ActivityLog.RefNo = Size.SizeCode
            UtilityDAL.BuildActivityLog(Size.ActivityLog, trans)

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
