Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility

Public Class ArticleAliasDAL
    Public Function GetAllRecords(Id As Integer) As DataTable
        Try

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT ArticleAliasDefTable.ArticleAliasID, ArticleAliasDefTable.ArticleMasterId, ArticleAliasDefTable.ArticleAliasCode as [Code], ArticleAliasDefTable.ArticleAliasName as [Article Alias], ArticleAliasDefTable.VendorID, vwCOADetail.detail_code as [Customer Code], vwCOADetail.detail_title as [Customer], vwCOADetail.account_type as [Ac Type], ArticleAliasDefTable.Active, ArticleAliasDefTable.SortOrder " _
                                       & " FROM ArticleAliasDefTable LEFT OUTER JOIN vwCOADetail ON ArticleAliasDefTable.VendorID = vwCOADetail.coa_detail_id WHERE ArticleMasterID=" & Id & " Order By ArticleAliasName ASC")

            dt.AcceptChanges()
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Add(objMod As ArticleAliasBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try


            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO ArticleAliasDefTable(ArticleMasterId, ArticleAliasCode, ArticleAliasName,VendorId, Active,SortOrder) VALUES(" & objMod.ArticleMasterId & ",N'" & objMod.ArticleAliasCode.Replace("'", "''") & "',N'" & objMod.ArticleAliasName.Replace("'", "''") & "'," & Val(objMod.VendorId) & "," & IIf(objMod.Active = True, 1, 0) & "," & Val(objMod.SortOrder) & ")Select @@Identity"
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(objMod As ArticleAliasBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Update ArticleAliasDefTable SET ArticleMasterId=" & objMod.ArticleMasterId & ", ArticleAliasCode=N'" & objMod.ArticleAliasCode.Replace("'", "''") & "', ArticleAliasName=N'" & objMod.ArticleAliasName.Replace("'", "''") & "', VendorId=" & objMod.VendorId & ",Active=" & IIf(objMod.Active = True, 1, 0) & ",SortOrder=" & Val(objMod.SortOrder) & " WHERE ArticleAliasID=" & objMod.ArticleAliasId & ""
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Delete(objMod As ArticleAliasBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Delete From ArticleAliasDefTable WHERE ArticleAliasID=" & objMod.ArticleAliasId & ""
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function IsValidate(objMod As ArticleAliasBE) As Boolean
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select Count(*) From ArticleAliasDefTable WHERE ArticleAliasID <> " & objMod.ArticleAliasId & " AND (ArticleAliasCode =N'" & objMod.ArticleAliasCode.Replace("'", "''") & "' Or ArticleAliasName='" & objMod.ArticleAliasName.Replace("'", "''") & "')")
            dt.AcceptChanges()

            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    Throw New Exception("Article alias code or name is already exists.") : Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
