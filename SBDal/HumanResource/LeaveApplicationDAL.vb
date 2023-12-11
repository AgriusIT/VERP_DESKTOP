'Ali Faisal : TFS1525 : DAL for save, update and delete functions
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class LeaveApplicationDAL
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Saves Records on 29-Sep-2017
    ''' </summary>
    ''' <param name="Application"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal Application As LeaveApplicationBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert records
            str = "INSERT INTO tblLeaveApplication (ApplicationNo, ApplicationDate, EmployeeId, ForwardToId, LeaveTypeId, ApplicationReason, AttendanceStatusId, FromDate, ToDate, NoOfDays, PeriodId, AlternateContactNo, ApplicationDetails,CompensatoryLeave,ScheduledLeave,Difference, JoiningDate) " _
                & "VALUES(N'" & Application.ApplicationNo & "' ,'" & Application.ApplicationDate & "' ," & Application.EmployeeId & " ," & Application.ForwardToId & ", " & Application.LeaveTypeId & ", N'" & Application.Reason & "', " & Application.AttendanceStatusId & ", '" & Application.FromDate & "', " & IIf(Application.ToDate = Nothing, "NULL", "Convert(Datetime,'" & Application.ToDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", '" & Application.NoOfDays & "', " & Application.PeriodId & ", N'" & Application.AlternateContactNo & "', N'" & Application.Description & "', " & IIf(Application.ScheduledLeave = Nothing, "NULL", "Convert(Datetime,'" & Application.ScheduledLeave.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", " & IIf(Application.CompensatoryLeave = Nothing, "NULL", "Convert(Datetime,'" & Application.CompensatoryLeave.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", '" & Application.Difference & "', '" & Application.JoiningDate & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Update Records on 29-Sep-2017
    ''' </summary>
    ''' <param name="Application"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal Application As LeaveApplicationBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update records
            str = "UPDATE tblLeaveApplication SET ApplicationNo = N'" & Application.ApplicationNo & "', ApplicationDate = '" & Application.ApplicationDate & "', EmployeeId = " & Application.EmployeeId & ", ForwardToId = " & Application.ForwardToId & ", LeaveTypeId = " & Application.LeaveTypeId & ", ApplicationReason = N'" & Application.Reason & "', AttendanceStatusId = " & Application.AttendanceStatusId & ", FromDate = '" & Application.FromDate & "', ToDate =  " & IIf(Application.ToDate = Nothing, "NULL", "Convert(Datetime,'" & Application.ToDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", NoOfDays = '" & Application.NoOfDays & "', PeriodId = " & Application.PeriodId & ", AlternateContactNo = N'" & Application.AlternateContactNo & "', ApplicationDetails = N'" & Application.Description & "',Status = " & IIf(Application.Status = True, 1, 0) & ", ApprovedBy = N'" & Application.ApprovedBy & "',ScheduledLeave =  " & IIf(Application.ScheduledLeave = Nothing, "NULL", "Convert(Datetime,'" & Application.ScheduledLeave.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ",CompensatoryLeave =  " & IIf(Application.CompensatoryLeave = Nothing, "NULL", "Convert(Datetime,'" & Application.CompensatoryLeave.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", Difference  = N'" & Application.Difference & "', JoiningDate = '" & Application.JoiningDate & "' WHERE LeaveApplicationId = " & Application.ApplicationId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Public Function UpdateLeaveApplication(ByVal Application As LeaveApplicationBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update records
            str = "UPDATE tblLeaveApplication SET ApplicationNo = N'" & Application.ApplicationNo & "', ApplicationDate = '" & Application.ApplicationDate & "', EmployeeId = " & Application.EmployeeId & ", ForwardToId = " & Application.ForwardToId & ", LeaveTypeId = " & Application.LeaveTypeId & ", ApplicationReason = N'" & Application.Reason & "', AttendanceStatusId = " & Application.AttendanceStatusId & ", FromDate = '" & Application.FromDate & "', ToDate =  " & IIf(Application.ToDate = Nothing, "NULL", "Convert(Datetime,'" & Application.ToDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", NoOfDays = '" & Application.NoOfDays & "', PeriodId = " & Application.PeriodId & ", AlternateContactNo = N'" & Application.AlternateContactNo & "', ApplicationDetails = N'" & Application.Description & "',ScheduledLeave =  " & IIf(Application.ScheduledLeave = Nothing, "NULL", "Convert(Datetime,'" & Application.ScheduledLeave.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ",CompensatoryLeave =  " & IIf(Application.CompensatoryLeave = Nothing, "NULL", "Convert(Datetime,'" & Application.CompensatoryLeave.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", Difference  = N'" & Application.Difference & "', JoiningDate = '" & Application.JoiningDate & "' WHERE LeaveApplicationId = " & Application.ApplicationId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1525 : Delete Records on 29-Sep-2017
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(ByVal Id As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete records
            str = "DELETE FROM tblLeaveApplication WHERE LeaveApplicationId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
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
