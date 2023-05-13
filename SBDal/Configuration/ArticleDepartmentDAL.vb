Imports SBDal
Imports SBModel
Imports SBUtility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class ArticleDepartmentDAL
    Public Function Save(ByVal Dept As ArticleDepartmentBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strQuery As String = String.Empty
            'strQuery = "INSERT INTO ArticleGroupDefTable(ArticleGroupName,GroupCode, Comments, Active, SortOrder, IsDate, SubSubId, SalesItem, ServiceItem) Values(N'" & Dept.ArticleGroupName & "', N'" & Dept.GroupCode.Replace("'", "''") & "',N'" & Dept.Comments & "', " & IIf(Dept.Active = True, 1, 0) & ", 1, N'" & Date.Now & "', " & Dept.SubSubId & ", " & IIf(Dept.SalesItem = True, 1, 0) & ", " & IIf(Dept.ServiceItem = True, 1, 0) & ") "
            strQuery = "INSERT INTO ArticleGroupDefTable(ArticleGroupName,GroupCode, Comments, Active, SortOrder, IsDate, SubSubId, SalesItem, ServiceItem,SalesAccountId, CGSAccountId) Values(N'" & Dept.ArticleGroupName & "', N'" & Dept.GroupCode.Replace("'", "''") & "',N'" & Dept.Comments & "', " & IIf(Dept.Active = True, 1, 0) & ", 1, N'" & Date.Now & "', " & Dept.SubSubId & ", " & IIf(Dept.SalesItem = True, 1, 0) & ", " & IIf(Dept.ServiceItem = True, 1, 0) & "," & Dept.SalesAccountId & "," & Dept.CGSAccountId & ") "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal Dept As ArticleDepartmentBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strQuery As String = String.Empty
            'strQuery = "UPDATE  ArticleGroupDefTable SET ArticleGroupName=N'" & Dept.ArticleGroupName & "'," _
            '                                          & " GroupCode=N'" & Dept.GroupCode.Replace("'", "''") & "', " _
            '                                          & " Comments=N'" & Dept.Comments & "'," _
            '                                          & " Active=" & IIf(Dept.Active = True, 1, 0) & ", " _
            '                                          & " SortOrder=1, " _
            '                                          & " IsDate=N'" & Date.Now & "' , " _
            '                                          & " SubSubId=" & Dept.SubSubId & ", " _
            '                                          & " SalesItem=" & IIf(Dept.SalesItem = True, 1, 0) & ", " _
            '                                          & " ServiceItem = " & IIf(Dept.ServiceItem = True, 1, 0) & " WHERE ArticleGroupId=" & Dept.ArticleGroupId & ""

            strQuery = "UPDATE  ArticleGroupDefTable SET ArticleGroupName=N'" & Dept.ArticleGroupName & "'," _
                                                    & " GroupCode=N'" & Dept.GroupCode.Replace("'", "''") & "', " _
                                                    & " Comments=N'" & Dept.Comments & "'," _
                                                    & " Active=" & IIf(Dept.Active = True, 1, 0) & ", " _
                                                    & " SortOrder='" & Dept.SortOrder & "', " _
                                                    & " IsDate=N'" & Date.Now & "' , " _
                                                    & " SubSubId=" & Dept.SubSubId & ", " _
                                                    & " SalesItem=" & IIf(Dept.SalesItem = True, 1, 0) & ", " _
                                                    & " ServiceItem = " & IIf(Dept.ServiceItem = True, 1, 0) & ",SalesAccountId=" & Dept.SalesAccountId & ", CGSAccountId=" & Dept.CGSAccountId & " WHERE ArticleGroupId=" & Dept.ArticleGroupId & ""

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal Dept As ArticleDepartmentBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strQuery As String = String.Empty
            strQuery = "Delete From ArticleGroupDefTable  WHERE ArticleGroupId=" & Dept.ArticleGroupId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAll() As List(Of ArticleDepartmentBE)
        Try
            Dim DeptDt As New List(Of ArticleDepartmentBE)
            Dim Dept As ArticleDepartmentBE
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, "Select ArticleGroupId, ArticleGroupName,Comments, Isnull(Active,0) as Ative, Isnull(SortOrder,0) as SortOrder, isnull(SubSubId,0) as SubSubId, Isnull(SalesItem,0) as SalesItem, Isnull(ServiceItem,0) as ServiceItem, GroupCode, IsNull(SalesAccountId,0) as SalesAccountId, IsNull(CGSAccountId,0) as CGSAccountId From ArticleGroupDefTable", Nothing)
            If dr.HasRows Then
                While dr.Read
                    Dept = New ArticleDepartmentBE
                    Dept.ArticleGroupId = dr(0)
                    Dept.ArticleGroupName = dr(1).ToString
                    Dept.Comments = dr(2).ToString
                    Dept.Active = Val(dr(3))
                    Dept.SortOrder = Val(dr(4))
                    Dept.SubSubId = dr(5)
                    Dept.SalesItem = Val(dr(6))
                    Dept.ServiceItem = Val(dr(7))
                    Dept.GroupCode = dr(8).ToString
                    Dept.SalesAccountId = Val(dr(9).ToString)
                    Dept.CGSAccountId = Val(dr(10).ToString)
                    DeptDt.Add(Dept)
                End While
            End If
            Return DeptDt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
