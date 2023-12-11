Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class ProductTypeDAL

    Public Function Add(ByVal objModel As ProductType) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()

        Try

            'insert 
            'Dim strSQL As String = "insert into ArticleTypeDefTable(ArticleTypeName," & _
            '" Comments,sortorder, Active ) values(N'" & objModel.Name.Trim.Replace("'", "''") & _
            '"',N'" & objModel.Comments.Trim.Replace("'", "''") & _
            '"','0'," & 1 & ") Select @@Identity"


            Dim str As String = "Select Count(*),ArticleTypeName,TypeCode From ArticleTypeDefTable WHERE (TypeCode=N'" & objModel.TypeCode.Replace("'", "''") & "' Or ArticleTypeName=N'" & objModel.Name.Replace("'", "''") & "') GROUP BY ArticleTypeName,TypeCode"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(str, trans)
            dt.AcceptChanges()

            If dt.HasErrors = False Then
                If dt.Rows.Count > 0 Then
                    If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                        If objModel.Name.ToUpper = dt.Rows(0).Item(1).ToString.ToUpper Then
                            Throw New Exception("[Article Type: " & dt.Rows(0).Item(1).ToString.Replace("'", "''") & "] is already exist")
                        ElseIf objModel.TypeCode.ToUpper = dt.Rows(0).Item(2).ToString.ToUpper Then
                            Throw New Exception("[Article Type Code: " & dt.Rows(0).Item(2).ToString.Replace("'", "''") & "] is already exist")
                        End If
                    End If
                End If
            End If


            Dim strSQL As String = "insert into ArticleTypeDefTable(ArticleTypeName,TypeCode," & _
     " Comments,sortorder, Active ) values(N'" & objModel.Name.Trim.Replace("'", "''") & _
     "',N'" & objModel.TypeCode.Replace("'", "''") & "',N'" & objModel.Comments.Trim.Replace("'", "''") & _
     "','0'," & 1 & ") Select @@Identity"

            objModel.ID = Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL))

            ''add activity log
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RefNo = objModel.ID
            objModel.ActivityLog.RecordType = "Configuration"

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
