Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class CompanyDAL


    Public Function Add(ByVal objModel As Company) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()

        Try

            'insert 
            ' Dim strSQL As String = "insert into ArticleCompanyDefTable(ArticleCompanyName,Comments,Active," & _
            '" SortOrder,IsDate) values(N'" & objModel.Name.Trim.Replace("'", "''") & _
            '"', N'" & objModel.Name.Trim.Replace("'", "''") & "',1 ,0, getdate()) Select @@Identity "


            Dim str As String = "Select Count(*), ArticleCompanyName,CategoryCode From ArticleCompanyDefTable WHERE (CategoryCode=N'" & objModel.CategoryCode.Replace("'", "''") & "' Or ArticleCompanyName=N'" & objModel.Name.Replace("'", "''") & "') GROUP BY ArticleCompanyName,CategoryCode"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(str, trans)
            dt.AcceptChanges()

            If dt.HasErrors = False Then
                If dt.Rows.Count > 0 Then
                    If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                        If objModel.Name.ToUpper = dt.Rows(0).Item(1).ToString.ToUpper Then
                            Throw New Exception("[Category: " & dt.Rows(0).Item(1).ToString.Replace("'", "''") & "] is already exist")
                        ElseIf objModel.CategoryCode.ToUpper = dt.Rows(0).Item(2).ToString.ToUpper Then
                            Throw New Exception("[Category Code: " & dt.Rows(0).Item(2).ToString.Replace("'", "''") & "] is already exist")
                        End If
                    End If
                End If
            End If

            Dim strSQL As String = "insert into ArticleCompanyDefTable(ArticleCompanyName,CategoryCode,Comments,Active," & _
          " SortOrder,IsDate) values(N'" & objModel.Name.Trim.Replace("'", "''") & _
          "',N'" & objModel.CategoryCode.Replace("'", "''") & "', N'" & objModel.Name.Trim.Replace("'", "''") & "',1 ,0, getdate()) Select @@Identity "

            objModel.ID = Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL))

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
