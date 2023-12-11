Imports SBDal
Imports SBModel
Imports SBUtility.Utility

Public Class TaskWorkingDAL
    Public Function Add(ByVal TaskWork As TaskWorking) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try

            Dim str As String = "Insert Into TaskWorkDetail(TaskWorkDate, EmployeeId, TaskId, Hours, Rate, UserName, FDate) Values(N'" & TaskWork.TaskWorkDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & TaskWork.EmployeeId & ", " & TaskWork.TaskId & ", " & TaskWork.TaskHours & ", " & TaskWork.TaskRate & ", N'" & TaskWork.UserName & "', N'" & TaskWork.FeedingDate & "') Select @@Identity"
            SQLHelper.ExecuteScaler(trans, CommandType.Text, str, Nothing)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal TaskWork As TaskWorking) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = "Update TaskWorkDetail Set TaskWorkDate=N'" & TaskWork.TaskWorkDate.ToString("yyyy-M-d h:mm:ss tt") & "', EmployeeID=" & TaskWork.EmployeeId & ", TaskId=" & TaskWork.TaskId & ", Hours=" & TaskWork.TaskHours & ", Rate=" & TaskWork.TaskRate & ", UserName=N'" & TaskWork.UserName & "', FDate=N'" & TaskWork.FeedingDate & "' WHERE TaskWorkId=" & TaskWork.TaskWorkId & " "
            SQLHelper.ExecuteScaler(trans, CommandType.Text, str, Nothing)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal TaskWork As TaskWorking) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = "Delete From TaskWorkDetail WHERE TaskWorkId=" & TaskWork.TaskWorkId & " "
            SQLHelper.ExecuteScaler(trans, CommandType.Text, str, Nothing)
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAllRecords() As DataTable
        Try
            Dim str As String = "   SELECT TaskWorkDetail.TaskWorkId, TaskWorkDetail.EmployeeId, TaskWorkDetail.TaskId, TaskWorkDetail.TaskWorkDate, tblDefEmployee.Employee_Name, TblDefTasks.Name AS Task, TaskWorkDetail.Hours, TaskWorkDetail.Rate " _
                                & " FROM TaskWorkDetail LEFT OUTER JOIN TblDefTasks ON TaskWorkDetail.TaskId = TblDefTasks.TaskId INNER JOIN tblDefEmployee ON TaskWorkDetail.EmployeeId = tblDefEmployee.Employee_ID "
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
