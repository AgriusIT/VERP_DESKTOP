Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class ProductGroupDAL


    Public Function Add(ByVal objModel As ProductGroup) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()

        Try

            'insert 
            'Dim strSQL As String = "insert into ArticleGroupDefTable(ArticleGroupName, Comments,sortorder, Active )" & _
            '" values(N'" & objModel.Name.Trim.Replace("'", "''") & "',N'" & _
            'objModel.Comments.Trim.Replace("'", "''") & "','0'," & 1 & ") Select @@Identity"




            Dim str As String = "Select Count(*), ArticleGroupName,GroupCode From ArticleGroupDefTable WHERE (GroupCode=N'" & objModel.GroupCode.Replace("'", "''") & "' Or ArticleGroupName=N'" & objModel.Name.Replace("'", "''") & "') GROUP BY ArticleGroupName,GroupCode"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(str, trans)
            dt.AcceptChanges()

            If dt.HasErrors = False Then
                If dt.Rows.Count > 0 Then
                    If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                        If objModel.Name.ToUpper = dt.Rows(0).Item(1).ToString.ToUpper Then
                            Throw New Exception("[Department: " & dt.Rows(0).Item(1).ToString.Replace("'", "''") & "] is already exist")
                        ElseIf objModel.GroupCode.ToUpper = dt.Rows(0).Item(2).ToString.ToUpper Then
                            Throw New Exception("[Department Code: " & dt.Rows(0).Item(2).ToString.Replace("'", "''") & "] is already exist")
                        End If
                    End If
                End If
            End If


            Dim strSQL As String = "insert into ArticleGroupDefTable(ArticleGroupName,GroupCode, Comments,sortorder, Active )" & _
       " values(N'" & objModel.Name.Trim.Replace("'", "''") & "',N'" & objModel.GroupCode.Replace("'", "''") & "', N'" & _
       objModel.Comments.Trim.Replace("'", "''") & "','0'," & 1 & ") Select @@Identity"

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
