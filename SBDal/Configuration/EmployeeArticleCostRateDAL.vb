Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data.SqlClient
Imports SBUtility.Utility
Public Class EmployeeArticleCostRateDAL
    Public Function Add(ByVal Cost As List(Of EmployeeArticleCostRateBE)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            If Cost IsNot Nothing Then
                If Cost.Count > 0 Then
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "Delete From tblEmployeeArticleCostRate WHERE Employee_ID=" & Cost.Item(0).Employee_ID & "")
                    For Each objCost As EmployeeArticleCostRateBE In Cost
                        strSQL = String.Empty
                        strSQL = "INSERT INTO tblEmployeeArticleCostRate(Employee_ID, ArticleDefID, Rate) VALUES(" & objCost.Employee_ID & "," & objCost.ArticleDefId & "," & objCost.Rate & ")"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    Next
                Else
                    Throw New Exception("Some of data is not provided.")
                End If
            Else
                Throw New Exception("Some of data is not provided.")
            End If

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal EmployeeId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblEmployeeArticleCostRate WHERE Employee_ID=" & EmployeeId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
End Class
