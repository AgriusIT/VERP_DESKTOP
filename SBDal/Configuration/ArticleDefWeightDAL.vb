Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class ArticleDefWeightDAL

    Public Function AddArticleDefWeight(ByVal ArticleWeight As List(Of ArticleDefWeight)) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            Dim ArticleWeightList As List(Of ArticleDefWeight) = ArticleWeight
            For Each ItemWeight As ArticleDefWeight In ArticleWeightList

                If GetItemByArticleId(ItemWeight.ArticleId, trans).Rows.Count < 1 Then
                    str = "Insert Into ArticleDefWeight(ArticleDefId, Weight, UserName, FDate) Values(" & ItemWeight.ArticleId & ", " & ItemWeight.Weight & ", N'" & ItemWeight.UserName & "', N'" & ItemWeight.FeedingDate.ToString("yyyy-M-d h:mm:ss tt") & "') Select @@Identity"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                Else
                    str = "Update ArticleDefWeight Set Weight=" & ItemWeight.Weight & ", UserName=N'" & ItemWeight.UserName & "', FDate=N'" & ItemWeight.FeedingDate.ToString("yyyy-M-d h:mm:ss tt") & "' WHERE ArticleDefId=" & ItemWeight.ArticleId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
                End If

            Next

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetItemByArticleId(ByVal ArticleId As Integer, ByVal trans As SqlClient.SqlTransaction) As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select ArticleDefId From ArticleDefWeight WHERE ArticleDefID=" & ArticleId & ""
            Return UtilityDAL.GetDataTable(str, trans)
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function
    Public Function LoadSalesReturnData(ByVal SalesReturnDate As DateTime, ByVal Type As String) As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = 1 Then Con.Open()
        Try
            Dim str As String = String.Empty
            str = "SP_SalesReturnWeight N'" & SalesReturnDate.Date.ToString("yyyy-M-d 00:00:00") & "', N'" & Type & "' "
            Dim dt As New DataTable
            Dim adp As SqlClient.SqlDataAdapter
            adp = New SqlClient.SqlDataAdapter(str, Con)
            adp.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
End Class
