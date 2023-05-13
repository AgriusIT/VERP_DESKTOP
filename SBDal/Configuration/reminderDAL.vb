Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal.SqlHelper
Imports SBDal


Public Class reminderDAL

    Public Function save(ByVal later As reminderBE) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "insert into tblreminder(Reminder_Date,Reminder_Time,Reminder_Description, Subject, [User]) values( N'" & later.Reminder_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(later.Reminder_Time = Nothing, "NULL", "N'" & later.Reminder_Time & "'") & ", N'" & later.Reminder_Description & "', N'" & later.Subject & "', N'" & later.User & "') Select @@Identity"
            later.reminderId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            Call AddDetail(later, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function


    Public Function update(ByVal later As reminderBE) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "UPDATE tblreminder  set Reminder_Date=N'" & later.Reminder_Date.ToString("yyyy-M-d h:mm:ss tt") & "', Reminder_Time=" & IIf(later.Reminder_Time = Nothing, "NULL", "N'" & later.Reminder_Time & "'") & ", Reminder_Description=N'" & later.Reminder_Description & "', Subject=N'" & later.Subject & "', [User]=N'" & later.User & "' where reminderId=" & later.reminderId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = "Delete From tblReminderDetail WHERE ReminderId=" & later.reminderId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            Call AddDetail(later, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Public Function AddDetail(ByVal Reminder As reminderBE, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each Reminded As ReminderDetail In Reminder.ReminderDetail
                strSQL = "INSERT INTO tblReminderDetail(ReminderId, [User_Id], User_Reminder_Date, User_Reminder_Time,Dismiss) VALUES(" & Reminder.reminderId & ", " & Reminded.UserID & ", N'" & Reminded.User_Reminder_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(Reminded.User_Reminder_Time = Nothing, "NULL", "N'" & Reminded.User_Reminder_Time & "'") & ", " & IIf(Reminded.Dismiss = True, 1, 0) & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function delete(ByVal later As reminderBE) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = " delete from tblreminderDetail where reminderId=" & later.reminderId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = " delete from tblreminder where reminderId=" & later.reminderId
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
    Public Function getall() As DataTable
        Try
            Dim str As String
            str = "SELECT  reminderId, Reminder_Date, Reminder_Time, Subject, Reminder_Description, [User] " _
               & "  FROM dbo.tblreminder "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DismissAll(ByVal ReminderList As List(Of ReminderDetail)) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            For Each Reminded As ReminderDetail In ReminderList
                strSQL = "Update tblReminderDetail SET Dismiss=1 WHERE ReminderId=" & Reminded.ReminderId & " AND User_Id=" & Reminded.UserID & " ReminderDetailId=" & Reminded.ReminderDetailId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function Dismiss(ByVal Reminder As List(Of ReminderDetail)) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            For Each Reminded As ReminderDetail In Reminder
                strSQL = "Update tblReminderDetail SET Dismiss=1 WHERE ReminderId=" & Reminded.ReminderId & " AND User_Id=" & Reminded.UserID & " AND ReminderDetailId=" & Reminded.ReminderDetailId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function Snooze(ByVal ReminderList As List(Of ReminderDetail)) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            For Each Reminded As ReminderDetail In ReminderList
                strSQL = "UPDATE tblReminderDetail  SET User_Reminder_Date=N'" & Reminded.User_Reminder_Date.ToString("yyyy-M-d h:mm:ss tt") & "', User_Reminder_Time=" & IIf(Reminded.User_Reminder_Time = Nothing, "NULL", "N'" & Reminded.User_Reminder_Time & "'") & "  WHERE ReminderId=" & Reminded.ReminderId & " AND User_Id= " & Reminded.UserID & " AND ReminderDetailId=" & Reminded.ReminderDetailId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
End Class
