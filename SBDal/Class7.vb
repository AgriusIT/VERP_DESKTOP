Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class OverTimeScheduleDAL

    Public Shared Function Save(ByVal PrintLog As PrintVoucherLogBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strQuery As String = String.Empty
            strQuery = "INSERT INTO tblPrintVoucherLog(SaleManId, VoucherDate, UserName) Values(" & PrintLog.SaleManId & ", N'" & PrintLog.VoucherDate & "', N'" & PrintLog.UserName.Replace("'", "''") & "')"
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

    Public Shared Function GetAllRecord() As DataTable

        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strQuery As String = String.Empty
            strQuery = "SELECT * FROM EmployeeDefTable"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            'Dim dt As DataTable
            'cmd.Connection = New SqlConnection(ConString.ConnectionString)
            cmd.CommandText = strQuery
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt

            trans.Commit()
            '            Return dt
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function DisplayEmployee(ByVal Emp_Id As Integer) As DataTable

        'Dim str As String = "Select User_Id, User_Name, User_Code, [Password], Email,  ISNULL(Block,0) as Block, ISNULL(GroupId,0) as GroupId, ISNULL(DashBoardRights,0) as DashBoardRights From tblUser WHERE User_Id=" & UserId & ""
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        'Dim cmd As New SqlCommand
        'Dim da As New SqlDataAdapter
        'Dim dt As New DataTable

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            'Task:2562 Display User detail when mouse click on listUser
            Dim str As String = "SELECT dbo.EmployeeDefTable.EmployeeId, dbo.EmployeeDefTable.EmployeeCode, dbo.EmployeeDefTable.EmployeeName, dbo.EmployeeDefTable.EmployeeSalary, dbo.EmployeeDefTable.EmployeePhoneNo, dbo.tblEmployeeOverTimeSchedule.Start_Date, dbo.tblEmployeeOverTimeSchedule.End_Date, dbo.tblEmployeeOverTimeSchedule.Start_Time, dbo.tblEmployeeOverTimeSchedule.End_Time, dbo.tblEmployeeOverTimeSchedule.Active() FROM dbo.EmployeeDefTable INNER JOIN dbo.tblEmployeeOverTimeSchedule ON dbo.EmployeeDefTable.EmployeeId = dbo.tblEmployeeOverTimeSchedule.EmployeeId WHERE EmployeeId=" & Emp_Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Dim dt As DataTable
            'cmd.Connection = New SqlConnection(ConString.ConnectionString)
            cmd.CommandText = str
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt

            trans.Commit()
            'End Task:2562

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetDefaultGroupRecords() As DataTable

        'Dim str As String = "Select User_Id, User_Name, User_Code, [Password], Email,  ISNULL(Block,0) as Block, ISNULL(GroupId,0) as GroupId, ISNULL(DashBoardRights,0) as DashBoardRights From tblUser WHERE User_Id=" & UserId & ""
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        'Dim cmd As New SqlCommand
        'Dim da As New SqlDataAdapter
        'Dim dt As New DataTable

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            'Task:2562 Display User detail when mouse click on listUser
            Dim str As String = "SELECT * FROM ShiftGroupTable "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Dim dt As DataTable
            'cmd.Connection = New SqlConnection(ConString.ConnectionString)
            cmd.CommandText = str
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt

            trans.Commit()
            'End Task:2562

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

End Class
