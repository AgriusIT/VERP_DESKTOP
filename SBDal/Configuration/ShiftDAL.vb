'26-APRIL-2014 TASK:2591 BY JUNAID SHEHZAD  New Enhancement In Define Shift
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class ShiftDAL
    Public Function Add(ByVal Shift As ShiftBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty

            'str = "INSERT INTO ShiftTable(ShiftCode, ShiftName, ShiftComments, ShiftStartDate, ShiftEndDate, ShiftStartTime, ShiftEndTime, Active, SortOrder) " _
            '& " Values(N'" & Shift.ShiftCode & "', N'" & Shift.ShiftName & "', N'" & Shift.ShiftComments & "', " & IIf(Shift.ShiftStartDate = Nothing, "NULL", "N'" & Shift.ShiftStartDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & IIf(Shift.ShiftEndDate = Nothing, "NULL", "N'" & Shift.ShiftEndDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & Shift.ShiftStartTime & "', N'" & Shift.ShiftEndTime & "', " & IIf(Shift.Active = True, 1, 0) & ", " & Val(Shift.SortOrder) & ")Select @@Identity"

            'Task: 2591 Insert Flexibility Time
            'str = "INSERT INTO ShiftTable(ShiftCode, ShiftName, ShiftComments, ShiftStartDate, ShiftEndDate, ShiftStartTime, ShiftEndTime, Active, SortOrder,FlexInTime, FlexOutTime, OverTimeRate) " _
            '& " Values(N'" & Shift.ShiftCode & "', N'" & Shift.ShiftName & "', N'" & Shift.ShiftComments & "', " & IIf(Shift.ShiftStartDate = Nothing, "NULL", "N'" & Shift.ShiftStartDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & IIf(Shift.ShiftEndDate = Nothing, "NULL", "N'" & Shift.ShiftEndDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & Shift.ShiftStartTime & "', N'" & Shift.ShiftEndTime & "', " & IIf(Shift.Active = True, 1, 0) & ", " & Val(Shift.SortOrder) & ", " & IIf(Shift.FlexInTime = Nothing, "NULL", "N'" & Shift.FlexInTime & "'") & ", " & IIf(Shift.FlexOutTime = Nothing, "NULL", "N'" & Shift.FlexOutTime & "'") & ", N'" & Shift.OverTimeRate & "')Select @@Identity"
            'End Task:2591

            str = "INSERT INTO ShiftTable(ShiftCode, ShiftName, ShiftComments, ShiftStartDate, ShiftEndDate, ShiftStartTime, ShiftEndTime, Active, SortOrder,FlexInTime, FlexOutTime, OverTimeRate,BreakStartTime,BreakEndTime,SpecialDayBreakTime,SpecialDayBreakStartTime, SpecialDayBreakEndTime,NightShift, OverTime_StartTime) " _
            & " Values(N'" & Shift.ShiftCode.Replace("'", "''") & "', N'" & Shift.ShiftName.Replace("'", "''") & "', N'" & Shift.ShiftComments.Replace("'", "''") & "', " & IIf(Shift.ShiftStartDate = Nothing, "NULL", "N'" & Shift.ShiftStartDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & IIf(Shift.ShiftEndDate = Nothing, "NULL", "N'" & Shift.ShiftEndDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & Shift.ShiftStartTime & "', N'" & Shift.ShiftEndTime & "', " & IIf(Shift.Active = True, 1, 0) & ", " & Val(Shift.SortOrder) & ", " & IIf(Shift.FlexInTime = Nothing, "NULL", "N'" & Shift.FlexInTime & "'") & ", " & IIf(Shift.FlexOutTime = Nothing, "NULL", "N'" & Shift.FlexOutTime & "'") & ", N'" & Shift.OverTimeRate & "',N'" & Shift.BreakStartTime & "', N'" & Shift.BreakEndTime & "', N'" & Shift.SpecialDayBreak.Replace("'", "''") & "', N'" & Shift.SpecialDayBreakStartTime & "', N'" & Shift.SpecialDayBreakEndTime & "'," & IIf(Shift.NightShift = True, 1, 0) & ", " & IIf(Shift.OverTime_StartTime = Nothing, "NULL", "N'" & Shift.OverTime_StartTime & "'") & ")Select @@Identity"

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal Shift As ShiftBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'str = "UPDATE ShiftTable SET ShiftCode=N'" & Shift.ShiftCode & "', " _
            '    & " ShiftName=N'" & Shift.ShiftName & "', " _
            '    & " ShiftComments=N'" & Shift.ShiftComments & "',  " _
            '    & " ShiftStartDate=" & IIf(Shift.ShiftStartDate = Nothing, "NULL", "N'" & Shift.ShiftStartDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
            '    & " ShiftEndDate=" & IIf(Shift.ShiftEndDate = Nothing, "NULL", "N'" & Shift.ShiftEndDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
            '    & " ShiftStartTime=N'" & Shift.ShiftStartTime & "',  " _
            '    & " ShiftEndTime=N'" & Shift.ShiftEndTime & "', " _
            '    & " Active=" & IIf(Shift.Active = True, 1, 0) & ", " _
            '    & " SortOrder=" & Val(Shift.SortOrder) & " WHERE ShiftId=" & Shift.ShiftId

            'Task: 2591 Update Flexibility Time
            'str = "UPDATE ShiftTable SET ShiftCode=N'" & Shift.ShiftCode & "', " _
            '& " ShiftName=N'" & Shift.ShiftName & "', " _
            '& " ShiftComments=N'" & Shift.ShiftComments & "',  " _
            '& " ShiftStartDate=" & IIf(Shift.ShiftStartDate = Nothing, "NULL", "N'" & Shift.ShiftStartDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
            '& " ShiftEndDate=" & IIf(Shift.ShiftEndDate = Nothing, "NULL", "N'" & Shift.ShiftEndDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
            '& " ShiftStartTime=N'" & Shift.ShiftStartTime & "',  " _
            '& " ShiftEndTime=N'" & Shift.ShiftEndTime & "', " _
            '& " Active=" & IIf(Shift.Active = True, 1, 0) & ", " _
            '& " SortOrder=" & Val(Shift.SortOrder) & ", FlexInTime=" & IIf(Shift.FlexInTime = Nothing, "NULL", "N'" & Shift.FlexInTime & "'") & ", FlexOutTime=" & IIf(Shift.FlexOutTime = Nothing, "NULL", "N'" & Shift.FlexOutTime & "'") & ", OverTimeRate=N'" & Shift.OverTimeRate & "' WHERE ShiftId=" & Shift.ShiftId
            'End task: 2591


            str = "UPDATE ShiftTable SET ShiftCode=N'" & Shift.ShiftCode & "', " _
         & " ShiftName=N'" & Shift.ShiftName & "', " _
         & " ShiftComments=N'" & Shift.ShiftComments & "',  " _
         & " ShiftStartDate=" & IIf(Shift.ShiftStartDate = Nothing, "NULL", "N'" & Shift.ShiftStartDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
         & " ShiftEndDate=" & IIf(Shift.ShiftEndDate = Nothing, "NULL", "N'" & Shift.ShiftEndDate.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
         & " ShiftStartTime=N'" & Shift.ShiftStartTime & "',  " _
         & " ShiftEndTime=N'" & Shift.ShiftEndTime & "', " _
         & " Active=" & IIf(Shift.Active = True, 1, 0) & ", " _
         & " SortOrder=" & Val(Shift.SortOrder) & ", FlexInTime=" & IIf(Shift.FlexInTime = Nothing, "NULL", "N'" & Shift.FlexInTime & "'") & ", FlexOutTime=" & IIf(Shift.FlexOutTime = Nothing, "NULL", "N'" & Shift.FlexOutTime & "'") & ", OverTimeRate=N'" & Shift.OverTimeRate & "', BreakStartTime=N'" & Shift.BreakStartTime & "', BreakEndTime=N'" & Shift.BreakEndTime & "', SpecialDayBreakTime=N'" & Shift.SpecialDayBreak.Replace("'", "''") & "',SpecialDayBreakStartTime=N'" & Shift.SpecialDayBreakStartTime & "', SpecialDayBreakEndTime=N'" & Shift.SpecialDayBreakEndTime & "',NightShift=" & IIf(Shift.NightShift = True, 1, 0) & ", OverTime_StartTime = " & IIf(Shift.OverTime_StartTime = Nothing, "NULL", "N'" & Shift.OverTime_StartTime & "'") & " WHERE ShiftId=" & Shift.ShiftId

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal Shift As ShiftBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete From ShiftTable WHERE ShiftId=" & Shift.ShiftId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAll() As DataTable
        Try
            Return UtilityDAL.GetDataTable("Select * From ShiftTable")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
