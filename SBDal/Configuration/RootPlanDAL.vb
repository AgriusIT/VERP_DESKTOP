Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class RootPlanDAL
    Public Function Add(ByVal RootPlan As RootPlanBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strQuery As String = "INSERT INTO tblDefRootPlan(RootPlanName, Description,SortOrder,Active) Values(N'" & RootPlan.RootPlanName & "', N'" & RootPlan.Description & "', " & RootPlan.SortOrder & ", " & IIf(RootPlan.Active = True, 1, 0) & ")"
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
    Public Function Update(ByVal RootPlan As RootPlanBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strQuery As String = "Update tblDefRootPlan SET RootPlanName=N'" & RootPlan.RootPlanName & "', Description=N'" & RootPlan.Description & "',SortOrder=" & RootPlan.SortOrder & ",Active=" & IIf(RootPlan.Active = True, 1, 0) & " WHERE RootPlanId=" & RootPlan.RootPlanId & ""
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
    Public Function Delete(ByVal RootPlan As RootPlanBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strQuery As String = "Delete From tblDefRootPlan Where RootPlanId=" & RootPlan.RootPlanId & ""
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
    Public Function GetAllRecords() As DataTable
        Try
            Return UtilityDAL.GetDataTable("Select tblDefRootPlan.RootPlanId, RootPlanName as [Route Plan], Description, SortOrder, tblDefRootPlan.Active From tblDefRootPlan Order By 1 Desc")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
