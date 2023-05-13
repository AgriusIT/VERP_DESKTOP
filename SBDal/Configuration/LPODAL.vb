Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class LPODAL

    Public Function Add(ByVal objModel As LPO) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()

        Try

            'insert 
            'Dim strSQL As String = "insert into ArticleLPODefTable(ArticleLPOName,ArticleCompanyid," & _
            '" Comments,sortorder, Active ) values(N'" & objModel.Name.Trim.Replace("'", "''") & _
            '"'," & objModel.CompanyID & ",N'" & objModel.Comments.Trim.Replace("'", "''") & _
            '"','0'," & 1 & ") Select @@Identity "


            Dim str As String = "Select Count(*),ArticleLPOName,SubCategoryCode From ArticleLPODefTable WHERE (SubCategoryCode=N'" & objModel.LPOCode.Replace("'", "''") & "' Or ArticleLPOName=N'" & objModel.Name.Replace("'", "''") & "') GROUP BY ArticleLPOName,SubCategoryCode"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(str, trans)
            dt.AcceptChanges()

            If dt.HasErrors = False Then
                If dt.Rows.Count > 0 Then
                    If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                        If objModel.Name.ToUpper = dt.Rows(0).Item(1).ToString.ToUpper Then
                            Throw New Exception("[Sub Category: " & dt.Rows(0).Item(1).ToString.Replace("'", "''") & "] is already exist")
                        ElseIf objModel.LPOCode.ToUpper = dt.Rows(0).Item(2).ToString.ToUpper Then
                            Throw New Exception("[Sub Category Code: " & dt.Rows(0).Item(2).ToString.Replace("'", "''") & "] is already exist")
                        End If
                    End If
                End If
            End If

            Dim strSQL As String = "insert into ArticleLPODefTable(ArticleLPOName,SubCategoryCode,ArticleCompanyid," & _
      " Comments,sortorder, Active ) values(N'" & objModel.Name.Trim.Replace("'", "''") & _
      "',N'" & objModel.LPOCode.Replace("'", "''") & "', " & objModel.CompanyID & ",N'" & objModel.Comments.Trim.Replace("'", "''") & _
      "','0'," & 1 & ") Select @@Identity "

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
