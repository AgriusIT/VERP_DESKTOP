'29-Apr-2014 TSK:2592 JUNAID SHEHZAD New Enhancement Employee OverTime Schedule define

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class frmEmpOverTimeScheduleDAL

    Public Function Save(ByVal EmpObj As EmpOverTimeScheduleBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strQuery As String = String.Empty
            strQuery = "INSERT INTO tblEmployeeOverTimeSchedule(EmployeeId, Start_Date, End_Date, Start_Time, End_Time, OverTime_Rate_HR, Active) Values(N'" & EmpObj.Employee_Id & "', N'" & EmpObj.Start_Date & "', N'" & EmpObj.End_Date & "', N'" & EmpObj.Start_Time & "', N'" & EmpObj.End_Time & "', N'" & EmpObj.Emp_OverTimeRate & "', " & IIf(EmpObj.Active, 1, 0) & ")"
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

    'Task" 2592 Retrieve Records related to Employee and Employee Over Time Schedule table
    Public Function GetAllRecord() As DataTable

        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strQuery As String = String.Empty
            strQuery = "SELECT dbo.tblDefEmployee.Employee_ID, dbo.tblDefEmployee.Employee_Name, dbo.tblDefEmployee.Employee_Code, dbo.tblDefEmployee.Father_Name, dbo.tblDefEmployee.Address, dbo.tblDefEmployee.Mobile,  dbo.tblEmployeeOverTimeSchedule.OverTimeSchId, dbo.tblEmployeeOverTimeSchedule.EmployeeId, dbo.tblEmployeeOverTimeSchedule.Start_Date, dbo.tblEmployeeOverTimeSchedule.End_Date, dbo.tblEmployeeOverTimeSchedule.Start_Time, dbo.tblEmployeeOverTimeSchedule.End_Time, dbo.tblEmployeeOverTimeSchedule.Active,  dbo.tblEmployeeOverTimeSchedule.OverTime_Rate_HR FROM dbo.tblDefEmployee INNER JOIN dbo.tblEmployeeOverTimeSchedule ON dbo.tblDefEmployee.Employee_ID = dbo.tblEmployeeOverTimeSchedule.EmployeeId"
            ' Added new column over time hours
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            'Dim dt As DataTable
            'cmd.Connection = New SqlConnection(ConString.ConnectionString)
            cmd.CommandText = strQuery
            cmd.Connection = Con
            cmd.Transaction = trans
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt

            trans.Commit()
            ' Return dt
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    'Task: 2592 Display Employee Detail with Over Time Schedule
    Public Function DisplayEmpOverTimeDetail(ByVal Emp_Ids As String) As DataTable

        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        Dim intDefaultWorkingHours As Integer = 0

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            'ApplyDefaultWorkingHoursOnOverTime
            'Task:2562 Display User detail when mouse click on listUser
            'Dim str As String = "SELECT  dbo.tblDefEmployee.Employee_Id, dbo.tblDefEmployee.Employee_Name, dbo.tblDefEmployee.Address, dbo.tblDefEmployee.Mobile,0 as OverTimeSchId, 0 as EmployeeId , GetDate() as Start_Date, GetDate() as End_Date, '' as Start_Time, '' as End_Time, Convert(float,0) as OverTime_Rate_HR, Convert(bit,1) as  Active FROM dbo.tblDefEmployee WHERE Employee_Id IN (" & Emp_Ids.ToString & ")"
            If UtilityDAL.GetConfigValue("ApplyDefaultWorkingHoursOnOverTime", trans).ToString = "TRUE" Then
                intDefaultWorkingHours = Val(UtilityDAL.GetConfigValue("DefaultWorkingHours", trans).ToString)
            End If
            Dim str As String = "SELECT  dbo.EmployeesView.Employee_Id, dbo.EmployeesView.Employee_Name, dbo.EmployeesView.Address, dbo.EmployeesView.Mobile,0 as OverTimeSchId, 0 as EmployeeId , Convert(DateTime,Null) as Start_Date, Convert(DateTime,Null) as End_Date, '' as Start_Time, '' as End_Time, Convert(float,0) as OverTime_Rate_HR, Convert(bit,1) as  Active,IsNull(Salary,0) as Salary," & intDefaultWorkingHours & " as Working_Hours, IsNull(ShiftStartTime,'09:00:00 AM') as ShiftStartTime,IsNull(ShiftEndTime,'05:30:00 PM') as ShiftEndTime FROM dbo.EmployeesView WHERE EmployeesView.Employee_Id IN (" & Emp_Ids.ToString & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Dim dt As DataTable
            'cmd.Connection = New SqlConnection(ConString.ConnectionString)
            'Dim str As String = "SELECT DISTINCT dbo.EmployeesView.Employee_Id, dbo.EmployeesView.Employee_Name, dbo.EmployeesView.Address, dbo.EmployeesView.Mobile,0 as OverTimeSchId, Employee_Id as EmployeeId , att.AttendanceDate as [Attendance Date],   Convert(DateTime,(Convert(varchar,att.AttendanceDate,102) +  ' ' + IsNull(EmployeesView.ShiftEndTime,'05:30:00 PM')),102) as Start_Time, Convert(DateTime,DateAdd(hh," & FixHrs & ", Convert(DateTime,(Convert(varchar,att.AttendanceDate,102) +  ' ' + IsNull(EmployeesView.ShiftEndTime,'05:30:00 PM')),102)),102) as End_Time,  Datediff(hh,Convert(DateTime,(Convert(varchar,att.AttendanceDate,102) +  ' ' + IsNull(EmployeesView.ShiftEndTime,'05:30:00 PM')),102),  Convert(DateTime,DateAdd(hh," & FixHrs & ", Convert(DateTime,(Convert(varchar,att.AttendanceDate,102) +  ' ' + IsNull(EmployeesView.ShiftEndTime,'05:30:00 PM')),102)),102)) as [OverTime Hours], ((IsNull(dbo.EmployeesView.Salary,0)/ Day(dateadd(month,1+datediff(month,0,att.AttendanceDate),-1)))/" & intDefaultWorkingDays & ") as OverTime_Rate_HR, Convert(bit,1) as  Active FROM dbo.EmployeesView INNER JOIN tblAttendanceDetail att on att.EmpId = EmployeesView.Employee_Id WHERE EmployeesView.Employee_Id IN (" & Emp_Ids.ToString & ") " _
            '                    & " AND (Convert(varchar,att.AttendanceDate,102) BETWEEN Convert(DateTime,'" & FromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102)) AND att.AttendanceType='In' And AttendanceStatus='Present'"

            cmd.CommandText = str
            cmd.Connection = Con
            cmd.Transaction = trans
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
    'END TASK: 2592

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

    'Task: 2592 Update Employee Over TIME Records
    Public Function Update(ByVal empObj As EmpOverTimeScheduleBE) As Boolean
        Dim conn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim strquery As String = String.Empty
            strquery = "UPDATE dbo.tblEmployeeOverTimeSchedule SET Start_Date=N'" & empObj.Start_Date & "', End_Date=N'" & empObj.End_Date & "', Start_Time=N'" & empObj.Start_Time & "', End_Time=N'" & empObj.End_Time & "', Active=" & IIf(empObj.Active, 1, 0) & ", OverTime_Rate_HR=N'" & empObj.Emp_OverTimeRate.ToString & "' WHERE OverTimeSchId = " & empObj.OverTimeSchedule_Id & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strquery)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    'end task: 2592

    Public Function GetDetailRecord(ByVal OverTimeId As Integer) As DataTable   ' That Record Get from tblDefSubject and tblExamDetail For DateSheet Tab

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
            Dim str As String = String.Empty
            'str = "SELECT dbo.tblDefEmployee.Employee_Id, dbo.tblDefEmployee.Employee_Name, dbo.tblDefEmployee.Address, dbo.tblDefEmployee.Mobile,dbo.tblEmployeeOverTimeSchedule.OverTimeSchId , dbo.tblEmployeeOverTimeSchedule.EmployeeId ,dbo.tblEmployeeOverTimeSchedule.Start_Date, dbo.tblEmployeeOverTimeSchedule.End_Date, dbo.tblEmployeeOverTimeSchedule.Start_Time, dbo.tblEmployeeOverTimeSchedule.End_Time, dbo.tblEmployeeOverTimeSchedule.OverTime_Rate_HR, dbo.tblEmployeeOverTimeSchedule.Active FROM dbo.tblDefEmployee LEFT OUTER JOIN  dbo.tblEmployeeOverTimeSchedule ON dbo.tblDefEmployee.Employee_ID = dbo.tblEmployeeOverTimeSchedule.EmployeeId WHERE dbo.tblEmployeeOverTimeSchedule.OverTimeSchId=" & OverTimeId
            str = "SELECT dbo.tblDefEmployee.Employee_Id, dbo.tblDefEmployee.Employee_Name, dbo.tblDefEmployee.Address, dbo.tblDefEmployee.Mobile,dbo.tblEmployeeOverTimeSchedule.OverTimeSchId , dbo.tblEmployeeOverTimeSchedule.EmployeeId ,dbo.tblEmployeeOverTimeSchedule.Start_Date, dbo.tblEmployeeOverTimeSchedule.End_Date, dbo.tblEmployeeOverTimeSchedule.Start_Time, dbo.tblEmployeeOverTimeSchedule.End_Time,Case When tblEmployeeOverTimeSchedule.RegularDayHrs > 0 then tblEmployeeOverTimeSchedule.RegularDayOTRate Else tblEmployeeOverTimeSchedule.OffDayOTRate End as RatePerHour, dbo.tblEmployeeOverTimeSchedule.Active FROM dbo.tblDefEmployee LEFT OUTER JOIN  dbo.tblEmployeeOverTimeSchedule ON dbo.tblDefEmployee.Employee_ID = dbo.tblEmployeeOverTimeSchedule.EmployeeId WHERE dbo.tblEmployeeOverTimeSchedule.OverTimeSchId=" & OverTimeId
            cmd.CommandText = str
            cmd.Connection = Con
            cmd.Transaction = trans
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

    Public Function Delete(ByVal empObj As EmpOverTimeScheduleBE) As Boolean
        Dim conn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim strquery As String = String.Empty
            strquery = "DELETE FROM tblEmployeeOverTimeSchedule WHERE OverTimeSchId = " & empObj.OverTimeSchedule_Id & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strquery)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
End Class
