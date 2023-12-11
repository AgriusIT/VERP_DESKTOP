Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class ArticleModelsDAL
    Public Function Insert(ByVal ArticleDetail As ArticleDetail, ByVal MasteraArticleId As Integer, ByVal Trans As SqlTransaction) As Boolean
        Dim strInsert As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        'Dim Trans As SqlTransaction = conn.BeginTransaction
        Try
            For Each ArticleModel As ArticleModels In ArticleDetail.ArticleModelList
                strInsert = " Insert Into ArticleModelList(ArticleId, ArticleMasterId, ModelId) Values(" & ArticleDetail.ArticleID & ", " & MasteraArticleId & ", " & ArticleModel.ModelId & " )"
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, strInsert)
            Next
            'Trans.Commit()
            Return True
        Catch ex As Exception
            Trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function Remove(ByVal ArticleId As Integer, ByVal Trans As SqlTransaction) As Boolean
        Dim strDelete As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        ''TASK TFS2128 Removed local trans in order to use ArticleDAL's trans.
        'Dim Trans As SqlTransaction = conn.BeginTransaction
        Try
            strDelete = "Delete From ArticleModelList Where ArticleId = " & ArticleId & ""
            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, strDelete)
            'Trans.Commit()
            Return True
        Catch ex As Exception
            Trans.Rollback()
            Throw ex
        End Try
    End Function
End Class
