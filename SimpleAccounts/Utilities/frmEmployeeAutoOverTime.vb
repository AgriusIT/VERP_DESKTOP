Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports System.Text.RegularExpressions

Public Class frmEmployeeAutoOverTime

    '// Variable to hold Days in month value
    '// This value will be used to determin Per day salary and it will be used to calculate salary of one day
    '// Value of this variable is set on form load
    '// Variable value is set based on 2 different configurations
    '//     1:  KeepConfigurationMonthDays
    '//     2:  Working_Days
    '// Full month days will be considered if "KeepConfigurationMonthDays" will hold value "True"
    '// Otherwise days will be considered from "Working_Days" configuration
    Dim intDayinMonth As Integer = 0

    '// Variable to hold value of hours of the day
    '// This value will be used to determin Hours in day and it will be used to calculate hourly overtime rate
    '// Value of the variable is set on Form load
    '// Variable value is set based on configuration key "DefaultWorkingHours"
    '// If Configuration "ApplyDefaultWorkingHoursOnOverTime" will be false then "totalHours" variable will be overwritten on calculation and will be calculated based on shift Start Time and End Time from Shift Table
    Dim totalHours As Integer = 0

    Dim CellEdited As Boolean = False
    Dim EditMode As Boolean = False
    Dim OffDayHours As Double = 0
    Dim RegularDayHours As Double = 0


    ''' <summary>
    ''' Data will be loaded in grid on button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            GetEmployeeList()
            btnLoad.BackColor = Control.DefaultBackColor
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ''' <summary>
    ''' This function will save or update data in table "tblEmployeeOverTimeSchedule"
    ''' Data will be updated in case overtime aleady exist in specific date for an employee
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save()
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim dt As New DataTable
        Dim employeeId As Integer = 0
        Try
            Me.grdAutoOT.UpdateData()
            dt = CType(Me.grdAutoOT.DataSource, DataTable)
            For Each row As Janus.Windows.GridEX.GridEXRow In grdAutoOT.GetRows
                ''                Emp.Employee_Name AS Name, Dept.DEPTNAME As Department, ST.OverTime_StartTime As OverTimeStart,  
                ''AD.AttendanceTime As OutTime, DATEDIFF(HH, Convert(DateTime, ST.ShiftEndTime, 102),  AD.AttendanceTime) As [Hours],
                ''EOTS.OverTime_Rate_HR As OverTimeRate
                Dim strQuery As String = String.Empty
                'strQuery = "INSERT INTO tblEmployeeOverTimeSchedule(EmployeeId, OverTime_Rate_HR, Active, AttendanceDate, AutoOverTime, Hours) Values(" & Val(row.Cells("Employee_ID").Value.ToString) & ", " & Val(row.Cells("OverTimeRate").Value.ToString) & ", " & row.Cells("Active").Value & ", N'" & row.Cells("AttendanceDate").Value & "', " & 1 & ", " & Val(row.Cells("Hours").Value.ToString) & ") Where Day(AttendanceDate) <> Day(" & row.Cells("AttendanceDate").Value & ")"
                'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdAutoOT.RootTable.Columns

                    'Dim Str As String = ""
                    'Dim dt As DataTable = SQLHelper.Get_DataTable(trans, CommandType.Text, )
                    'If col.Key.Contains("Day") Then '' AndAlso Val(row.Cells(col.Key).Value.ToString) > 0 Then ''// This commented row is moved from if condition to save zero value too. Performed by Ameen
                    '    employeeId = Val(row.Cells("employee_id").Value.ToString)
                    '    If Val(row.Cells(col.Key).Value.ToString) > 0 Then
                    '        strQuery = " If Not EXISTS(Select * From tblEmployeeOverTimeSchedule Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00') " & _
                    '        " INSERT INTO tblEmployeeOverTimeSchedule(EmployeeId, Start_Date, End_Date, Start_Time, End_Time, OverTime_Rate_HR, Active, OverTime_Hours) Values(" & Val(row.Cells("employee_id").Value.ToString) & ", '" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00', '" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00','" & row.Cells("OverTime_StartTime").Value & "', '" & CType(row.Cells("OverTime_StartTime").Value, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', " & Val(row.Cells("OverTimeRate").Value.ToString) & ", 1, " & Val(row.Cells(col.Key).Value.ToString) & ") " & _
                    '        " Else " & _
                    '        " update tblEmployeeOverTimeSchedule set Start_Time='" & row.Cells("OverTime_StartTime").Value & "', End_Time='" & CType(row.Cells("OverTime_StartTime").Value, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', OverTime_Rate_HR=" & Val(row.Cells("OverTimeRate").Value.ToString) & ", OverTime_Hours=" & Val(row.Cells(col.Key).Value.ToString) & "  Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And OverTime_Hours Is Not Null And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00'"
                    '        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
                    '    ElseIf Val(row.Cells(col.Key).Value.ToString) < 0 Then
                    '        strQuery = " If Not EXISTS(Select * From tblEmployeeOverTimeSchedule Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00') " & _
                    '        " INSERT INTO tblEmployeeOverTimeSchedule(EmployeeId, Start_Date, End_Date, Start_Time, End_Time, OverTime_Rate_HR, Active, OverTime_Hours) Values(" & Val(row.Cells("employee_id").Value.ToString) & ", '" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00', '" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00', '11:59:59 PM', '" & CType(#11:59:59 PM#, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', " & Val(row.Cells("OverTimeRate").Value.ToString) & ", 1, " & Val(row.Cells(col.Key).Value.ToString) & ") " & _
                    '        " Else " & _
                    '        " update tblEmployeeOverTimeSchedule set Start_Time='11:59:59 PM', End_Time='" & CType(#11:59:59 PM#, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', OverTime_Rate_HR=" & Val(row.Cells("OverTimeRate").Value.ToString) & ", OverTime_Hours=" & Val(row.Cells(col.Key).Value.ToString) & "  Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And OverTime_Hours Is Not Null And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00'"
                    '        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
                    '        'ElseIf Val(row.Cells(col.Key).Value.ToString) < 0 Then
                    '        '    strQuery = " If Not EXISTS(Select * From tblEmployeeOverTimeSchedule Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00') " & _
                    '        '    " INSERT INTO tblEmployeeOverTimeSchedule(EmployeeId, Start_Date, End_Date, Start_Time, End_Time, OverTime_Rate_HR, Active, OverTime_Hours) Values(" & Val(row.Cells("employee_id").Value.ToString) & ", '" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00', '" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00', '1/1/1900 11:59:59 PM', '" & CType(#1/1/1900 11:59:59 PM#, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', " & Val(row.Cells("OverTimeRate").Value.ToString) & ", 1, " & Val(row.Cells(col.Key).Value.ToString) & ") " & _
                    '        '    " Else " & _
                    '        '    " update tblEmployeeOverTimeSchedule set Start_Time='1/1/1900 11:59:59 PM', End_Time='" & CType(#1/1/1900 11:59:59 PM#, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', OverTime_Rate_HR=" & Val(row.Cells("OverTimeRate").Value.ToString) & ", OverTime_Hours=" & Val(row.Cells(col.Key).Value.ToString) & "  Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And OverTime_Hours Is Not Null And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00'"
                    '        '    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
                    '    Else
                    '        'strQuery = " update tblEmployeeOverTimeSchedule set Start_Time='" & row.Cells("OverTime_StartTime").Value & "', End_Time='" & CType(row.Cells("OverTime_StartTime").Value, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', OverTime_Rate_HR=" & Val(row.Cells("OverTimeRate").Value.ToString) & ", OverTime_Hours=" & Val(row.Cells(col.Key).Value.ToString) & "  Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And OverTime_Hours Is Not Null And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00'"
                    '        If Val(row.Cells(col.Key).Value.ToString) = 0 Then
                    '            strQuery = "Delete FROM tblEmployeeOverTimeSchedule Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And OverTime_Hours Is Not Null And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00'"
                    '        Else
                    '            strQuery = " update tblEmployeeOverTimeSchedule set Start_Time='" & row.Cells("OverTime_StartTime").Value & "', End_Time='" & CType(row.Cells("OverTime_StartTime").Value, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', OverTime_Rate_HR=" & Val(row.Cells("OverTimeRate").Value.ToString) & ", OverTime_Hours=" & Val(row.Cells(col.Key).Value.ToString) & "  Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And OverTime_Hours Is Not Null And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00'"
                    '        End If


                    '        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

                    '    End If

                    '    'strQuery = " If Not EXISTS(Select * From tblEmployeeOverTimeSchedule Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00') " & _
                    '    '" INSERT INTO tblEmployeeOverTimeSchedule(EmployeeId, Start_Date, End_Date, Start_Time, End_Time, OverTime_Rate_HR, Active, OverTime_Hours) Values(" & Val(row.Cells("employee_id").Value.ToString) & ", '" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00', '" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00','" & row.Cells("OverTime_StartTime").Value & "', '" & CType(row.Cells("OverTime_StartTime").Value, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', " & Val(row.Cells("OverTimeRate").Value.ToString) & ", 1, " & Val(row.Cells(col.Key).Value.ToString) & ") " & _
                    '    '" Else " & _
                    '    '" update tblEmployeeOverTimeSchedule set Start_Time='" & row.Cells("OverTime_StartTime").Value & "', End_Time='" & CType(row.Cells("OverTime_StartTime").Value, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', OverTime_Rate_HR=" & Val(row.Cells("OverTimeRate").Value.ToString) & ", OverTime_Hours=" & Val(row.Cells(col.Key).Value.ToString) & "  Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00'"
                    '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

                    'End If



                    If col.Key.StartsWith("Day") Then
                        Dim OffDay As Integer = GetOffDays(col.Key)
                        employeeId = Val(row.Cells("employee_id").Value.ToString)
                        If Val(row.Cells(col.Key).Value.ToString) > 0 Then
                            strQuery = " If Not EXISTS(Select * From tblEmployeeOverTimeSchedule Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00') " & _
                            " INSERT INTO tblEmployeeOverTimeSchedule(EmployeeId, Start_Date, End_Date, Start_Time, End_Time, Active, OverTime_Hours, RegularDayHrs,OffDayHrs,RegularDayOTRate,OffDayOTRate) Values(" & Val(row.Cells("employee_id").Value.ToString) & ", '" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00', '" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00','" & row.Cells("OverTime_StartTime").Value & "', '" & CType(row.Cells("OverTime_StartTime").Value, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', 1, " & Val(row.Cells(col.Key).Value.ToString) & " , " & IIf(OffDay > 0, 0, Val(row.Cells(col.Key).Value.ToString)) & ", " & IIf(OffDay > 0, Val(row.Cells(col.Key).Value.ToString), 0) & ", " & Val(row.Cells("RegularDayOTRate").Value.ToString) & "," & Val(row.Cells("OffDayOTRate").Value.ToString) & "  ) " & _
                            " Else " & _
                            " update tblEmployeeOverTimeSchedule set Start_Time='" & row.Cells("OverTime_StartTime").Value & "', End_Time='" & CType(row.Cells("OverTime_StartTime").Value, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', OverTime_Hours=" & Val(row.Cells(col.Key).Value.ToString) & ", RegularDayHrs=" & IIf(OffDay > 0, 0, Val(row.Cells(col.Key).Value.ToString)) & ", OffDayHrs=" & IIf(OffDay > 0, Val(row.Cells(col.Key).Value.ToString), 0) & ",RegularDayOTRate=" & Val(row.Cells("RegularDayOTRate").Value.ToString) & ",OffDayOTRate=" & Val(row.Cells("OffDayOTRate").Value.ToString) & "  Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And OverTime_Hours Is Not Null And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00'"
                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
                        ElseIf Val(row.Cells(col.Key).Value.ToString) < 0 Then
                            strQuery = " If Not EXISTS(Select * From tblEmployeeOverTimeSchedule Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00') " & _
                            " INSERT INTO tblEmployeeOverTimeSchedule(EmployeeId, Start_Date, End_Date, Start_Time, End_Time, Active, OverTime_Hours,RegularDayHrs,OffDayHrs,RegularDayOTRate,OffDayOTRate) Values(" & Val(row.Cells("employee_id").Value.ToString) & ", '" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00', '" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00', '11:59:59 PM', '" & CType(#11:59:59 PM#, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "',1, " & Val(row.Cells(col.Key).Value.ToString) & ", " & IIf(OffDay > 0, 0, Val(row.Cells(col.Key).Value.ToString)) & ", " & IIf(OffDay > 0, Val(row.Cells(col.Key).Value.ToString), 0) & ", " & Val(row.Cells("RegularDayOTRate").Value.ToString) & "," & Val(row.Cells("OffDayOTRate").Value.ToString) & "  ) " & _
                            " Else " & _
                            " update tblEmployeeOverTimeSchedule set Start_Time='11:59:59 PM', End_Time='" & CType(#11:59:59 PM#, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', OverTime_Hours=" & Val(row.Cells(col.Key).Value.ToString) & ",RegularDayHrs=" & IIf(OffDay > 0, 0, Val(row.Cells(col.Key).Value.ToString)) & ",OffDayHrs=" & IIf(OffDay > 0, Val(row.Cells(col.Key).Value.ToString), 0) & ",RegularDayOTRate=" & Val(row.Cells("RegularDayOTRate").Value.ToString) & ",OffDayOTRate=" & Val(row.Cells("OffDayOTRate").Value.ToString) & "  Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And OverTime_Hours Is Not Null And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00'"
                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
                        Else
                            If Val(row.Cells(col.Key).Value.ToString) = 0 Then
                                strQuery = "Delete FROM tblEmployeeOverTimeSchedule Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And OverTime_Hours Is Not Null And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00'"
                            Else
                                strQuery = " update tblEmployeeOverTimeSchedule set Start_Time='" & row.Cells("OverTime_StartTime").Value & "', End_Time='" & CType(row.Cells("OverTime_StartTime").Value, DateTime).AddHours(Val(row.Cells(col.Key).Value.ToString)) & "', OverTime_Hours=" & Val(row.Cells(col.Key).Value.ToString) & ", RegularDayHrs=" & IIf(OffDay > 0, 0, Val(row.Cells(col.Key).Value.ToString)) & ", OffDayHrs= " & IIf(OffDay > 0, Val(row.Cells(col.Key).Value.ToString), 0) & ", RegularDayOTRate=" & Val(row.Cells("RegularDayOTRate").Value.ToString) & ", OffDayOTRate= " & Val(row.Cells("OffDayOTRate").Value.ToString) & "  Where EmployeeId=" & Val(row.Cells("employee_id").Value.ToString) & " And OverTime_Hours Is Not Null And Start_Date='" & col.Key.Replace("Day", "") & "-" & Me.dtpMonth.Value.ToString("MMM-yyyy") & " 00:00:00'"
                            End If
                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
                        End If
                    End If
                Next

            Next

            trans.Commit()


        Catch ex As Exception
            trans.Rollback()
            Dim id As Integer = employeeId
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    ''' <summary>
    ''' This function will get previously saved over time from table tblEmployeeOverTimeSchedule
    ''' </summary>
    ''' <remarks></remarks>
    Sub GetSavedOverTime()
        Try
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Dim str As String = String.Empty
            Dim dt As New DataTable

            str = "SELECT dbo.tblEmployeeOverTimeSchedule.EmployeeId, dbo.tblEmployeeOverTimeSchedule.Start_Date,tblEmployeeOverTimeSchedule.OverTime_Hours, tblEmployeeOverTimeSchedule.RegularDayHrs, tblEmployeeOverTimeSchedule.OffDayHrs,  IsNull(tblEmployeeOverTimeSchedule.RegularDayOTRate, 0) As RegularDayOTRate,  IsNull(tblEmployeeOverTimeSchedule.OffDayOTRate, 0) As OffDayOTRate FROM tblEmployeeOverTimeSchedule INNER JOIN tblDefEmployee ON tblEmployeeOverTimeSchedule.EmployeeId = tblDefEmployee.Employee_ID where " & IIf(Me.cmbDepartment.SelectedIndex > 0, " dept_id=" & Me.cmbDepartment.SelectedValue & " and ", "") & " dbo.tblEmployeeOverTimeSchedule.Start_Date BETWEEN '" & Me.dtpMonth.Value.ToString("yyyy-MMM-01 00:00:00") & "' AND '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 23:59:59") & "' order by 1,2 "
            dt = GetDataTable(str)

            If dt.Rows.Count > 0 Then

                '// Place holder for Last employee in loop
                Dim EmployeeId As Integer = 0

                '// Row number in grid of EmployeeId
                Dim RowN As Integer = -1

                Dim dtData As DataTable = Me.grdAutoOT.DataSource
                Dim NormalMul As String = String.Empty
                Dim OffMul As String = String.Empty
                If Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString) > 1 Then
                    NormalMul = Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString)
                Else
                    NormalMul = 1
                End If
                If Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString) > 1 Then
                    OffMul = Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString)
                Else
                    OffMul = 1
                End If
                For Each row As DataRow In dt.Rows
                    If Val(row.Item(0).ToString) <> EmployeeId Then
                        EmployeeId = Val(row.Item(0).ToString)
                        RowN = FindEmployeeRowNumber(Val(row.Item(0).ToString), dtData)
                    End If

                    If RowN >= 0 Then

                        'dtData.Rows(RowN).Item("Day" & CType(row.Item("Start_Date").ToString, DateTime).Day) = Val(row.Item("OverTime_Hours").ToString)
                        If Val(row.Item("OffDayHrs").ToString) > 0 Then
                            dtData.Rows(RowN).Item("Day" & CType(row.Item("Start_Date").ToString, DateTime).Day) = Val(row.Item("OffDayHrs").ToString)
                            dtData.Rows(RowN).Item("OffDayOTAmount") += Val(row.Item("OffDayHrs").ToString) * (Val(row.Item("OffDayOTRate").ToString))
                            dtData.Rows(RowN).Item("OffDayHrs") += Val(row.Item("OffDayHrs").ToString)

                        Else
                            dtData.Rows(RowN).Item("Day" & CType(row.Item("Start_Date").ToString, DateTime).Day) = Val(row.Item("RegularDayHrs").ToString)
                            dtData.Rows(RowN).Item("RegularDayOTAmount") += (Val(row.Item("RegularDayHrs").ToString)) * (Val(row.Item("RegularDayOTRate").ToString))
                            dtData.Rows(RowN).Item("RegularDayHrs") += Val(row.Item("RegularDayHrs").ToString)
                        End If
                    End If

                Next

                Me.grdAutoOT.AutoSizeColumns()
                Me.grdAutoOT.UpdateData()

                For Each grdCol As Janus.Windows.GridEX.GridEXColumn In grdAutoOT.RootTable.Columns
                    If grdCol.Key.StartsWith("Day") Then
                        grdCol.Caption = grdCol.Caption.ToString.Replace("Day", "")
                        grdCol.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        grdCol.CellStyle.BackColor = Color.White
                        ' grdCol.Width = 26
                    End If
                Next
            Else

                Me.grdAutoOT.AutoSizeColumns()

                For Each grdCol As Janus.Windows.GridEX.GridEXColumn In grdAutoOT.RootTable.Columns
                    If grdCol.Caption.StartsWith("Day") Then
                        grdCol.Caption = grdCol.Caption.ToString.Replace("Day", "")
                        grdCol.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        grdCol.CellStyle.BackColor = Color.FromArgb(255, 255, 192)
                        grdCol.Width = 30
                    End If
                Next

            End If

        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
            Application.DoEvents()

        End Try
    End Sub


    ''' <summary>
    ''' This will populate employees' data who were active in specified Month
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Private Sub GetEmployeeList()
        Try
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Dim str As String = String.Empty
            Dim dt As New DataTable
            If Not getConfigValueByType("OverTimeBasedOnWorkingDays").ToString = "True" Then
                intDayinMonth = DateTime.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month)
            Else
                intDayinMonth = Val(getConfigValueByType("OverTimeWorkingDays").ToString)
            End If
            If getConfigValueByType("ApplyDefaultWorkingHoursOnOverTime").ToString = "False" Then
                totalHours = 0
            Else
                totalHours = Val(getConfigValueByType("DefaultWorkingHours").ToString)
            End If
            Dim SFPer As String = String.Empty

            If Val(getConfigValueByType("OverTimeSalaryFactorPercentage").ToString) > 1 Then
                SFPer = Val(getConfigValueByType("OverTimeSalaryFactorPercentage").ToString) / 100
            Else
                SFPer = 1
            End If
            
            Dim NormalMul As String = String.Empty
            Dim OffMul As String = String.Empty
            If Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString) > 1 Then
                NormalMul = Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString)
            Else
                NormalMul = 1
            End If
            If Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString) > 1 Then
                OffMul = Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString)
            Else
                OffMul = 1
            End If
            Dim OffDays As String = String.Empty
            OffDays = getConfigValueByType("DayOff")
            If getConfigValueByType("SalaryGenerationPercentageBased").ToString = "True" Then
                str = "select tblDefEmployee.Employee_ID As Employee_Id, Employee_Code as Code, Employee_Name, Salary, 0.00 as RegularDayHrs, 0.00 as OffDayHrs, " & _
                            " (((tblDefEmployee.Salary*tblEmployeeAccounts.Amount)/100/" & intDayinMonth & ")/" & IIf(totalHours = 0, " Convert(Numeric(5, 1), Convert(numeric(18, 2), DateDiff(Minute, Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftStartTime), 108), Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftEndTime), 108))) / 60)", totalHours) & ") * " & NormalMul & " as RegularDayOTRate," & _
                            " (((tblDefEmployee.Salary*tblEmployeeAccounts.Amount)/100/" & intDayinMonth & ")/" & IIf(totalHours = 0, " Convert(Numeric(5, 1), Convert(numeric(18, 2), DateDiff(Minute, Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftStartTime), 108), Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftEndTime), 108))) / 60)", totalHours) & ") * " & OffMul & " as OffDayOTRate," & _
                            " 0.00 as RegularDayOTAmount,0.00 as OffDayOTAmount,0.00 as TotalAmount, IsNull(ShiftTable.OverTime_StartTime, ShiftTable.ShiftEndTime) as OverTime_StartTime , ShiftTable.ShiftStartTime, ShiftTable.ShiftEndTime from ShiftScheduleTable INNER JOIN ShiftTable ON ShiftScheduleTable.ShiftId = ShiftTable.ShiftId INNER JOIN tblDefEmployee INNER JOIN ShiftGroupTable ON tblDefEmployee.ShiftGroupId = ShiftGroupTable.ShiftGroupId ON ShiftScheduleTable.ShiftGroupId = ShiftGroupTable.ShiftGroupId " & _
                            " LEFT OUTER JOIN tblEmployeeAccounts ON tblDefEmployee.Employee_ID = tblEmployeeAccounts.Employee_Id " & _
                            " where isnull(Leaving_Date, '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "') >=  '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "' " & _
                            "  " & IIf(cmbDepartment.SelectedIndex > 0, " and dept_id=" & Me.cmbDepartment.SelectedValue, "") & " And tblEmployeeAccounts.Type_Id = 1 " & _
                            " order by Employee_Name "

                'str = "select tblDefEmployee.Employee_Id, Employee_Code as Code, Employee_Name, 0.00 as TotalOverTime,(tblDefEmployee.Salary*tblEmployeeAccounts.Amount)/100 As Salary, " & _
                '        '                    " ((tblDefEmployee.Salary*tblEmployeeAccounts.Amount)/100/" & intDayinMonth & ")/" & IIf(totalHours = 0, " Convert(Numeric(5, 1), Convert(numeric(18, 2), DateDiff(Minute, Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftStartTime), 108), Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftEndTime), 108))) / 60)", totalHours) & " as OverTimeRate," & _
                '        '                    " 0.00 as OverTimeAmount, IsNull(ShiftTable.OverTime_StartTime, ShiftTable.ShiftEndTime) as OverTime_StartTime , ShiftTable.ShiftStartTime, ShiftTable.ShiftEndTime from ShiftScheduleTable INNER JOIN ShiftTable ON ShiftScheduleTable.ShiftId = ShiftTable.ShiftId INNER JOIN tblDefEmployee INNER JOIN ShiftGroupTable ON tblDefEmployee.ShiftGroupId = ShiftGroupTable.ShiftGroupId ON ShiftScheduleTable.ShiftGroupId = ShiftGroupTable.ShiftGroupId  LEFT OUTER JOIN tblEmployeeAccounts ON tblDefEmployee.Employee_ID = tblEmployeeAccounts.Employee_Id " & _
                '        '                    "   where isnull(Leaving_Date, '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "') >=  '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "' " & _
                '        '                    "  " & IIf(cmbDepartment.SelectedIndex > 0, " and dept_id=" & Me.cmbDepartment.SelectedValue, "") & " And tblEmployeeAccounts.Type_Id = 1 " & _
                '        '                    " order by Employee_Name "


            Else
                str = "select tblDefEmployee.Employee_ID As Employee_Id, Employee_Code as Code, Employee_Name, Salary, 0.00 as RegularDayHrs, 0.00 as OffDayHrs, " & _
                            " ((Salary * " & SFPer & " /" & intDayinMonth & ")/" & IIf(totalHours = 0, " Convert(Numeric(5, 1), Convert(numeric(18, 2), DateDiff(Minute, Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftStartTime), 108), Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftEndTime), 108))) / 60)", totalHours) & ") * " & NormalMul & " as RegularDayOTRate," & _
                            " ((Salary * " & SFPer & " /" & intDayinMonth & ")/" & IIf(totalHours = 0, " Convert(Numeric(5, 1), Convert(numeric(18, 2), DateDiff(Minute, Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftStartTime), 108), Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftEndTime), 108))) / 60)", totalHours) & ") * " & OffMul & " as OffDayOTRate," & _
                            " 0.00 as RegularDayOTAmount,0.00 as OffDayOTAmount,0.00 as TotalAmount, IsNull(ShiftTable.OverTime_StartTime, ShiftTable.ShiftEndTime) as OverTime_StartTime , ShiftTable.ShiftStartTime, ShiftTable.ShiftEndTime from ShiftScheduleTable INNER JOIN ShiftTable ON ShiftScheduleTable.ShiftId = ShiftTable.ShiftId INNER JOIN tblDefEmployee INNER JOIN ShiftGroupTable ON tblDefEmployee.ShiftGroupId = ShiftGroupTable.ShiftGroupId ON ShiftScheduleTable.ShiftGroupId = ShiftGroupTable.ShiftGroupId " & _
                            "   where isnull(Leaving_Date, '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "') >=  '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "' " & _
                            "  " & IIf(cmbDepartment.SelectedIndex > 0, " and dept_id=" & Me.cmbDepartment.SelectedValue, "") & " " & _
                            " order by Employee_Name "
            End If


            dt = GetDataTable(str)
            Dim ExpressionString As String = String.Empty

            For i As Integer = 1 To Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month)
                Dim dCol As DataColumn = New DataColumn("Day" & i, System.Type.GetType("System.Double"))
                Dim dateDay As New DateTime(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month, i)
                'Dim dayOfWeek As DayOfWeek = dateDay.DayOfWeek
                dCol.DefaultValue = 0
                dt.Columns.Add(dCol)
                'Dim MultiOffDays() As String = OffDays.Split(",")
                'Dim dayOfWeek As DayOfWeek = dateDay.DayOfWeek
                'Dim Counter As Integer = 0
                'If MultiOffDays.Length > 0 Then
                '    For Each OffDay As String In MultiOffDays
                '        If dayOfWeek.ToString = OffDay.ToString Then
                '            Counter += 1
                '        End If
                '    Next
                'End If
                'If dayOfWeek.ToString = OffDays.ToString Then
                '    If ExpressionString.Length > 0 Then ExpressionString = ExpressionString & " + "
                '    ExpressionString = ExpressionString & "[Day" & i & "]"

                'End If
            Next
            'dt.Columns("RegularDayHrs").Expression = ExpressionString
            'dt.Columns("OffDayHrs").Expression = ExpressionString
            'dt.Columns("RegularDayOTAmount").Expression = "[RegularDayHrs] * [RegularDayOTRate]"
            'dt.Columns("OffDayOTAmount").Expression = "[OffDayHrs] * [OffDayOTRate]"
            dt.Columns("TotalAmount").Expression = "[RegularDayOTAmount] + [OffDayOTAmount]"
            Me.grdAutoOT.DataSource = dt
            Me.grdAutoOT.RetrieveStructure()
            Me.grdAutoOT.FrozenColumns = 4
            'Me.grdAutoOT.RootTable.Columns("TotalOverTime").CellStyle.FontBold = Janus.Windows.GridEX.TriState.True
            'Me.grdAutoOT.RootTable.Columns("TotalOverTime").Caption = "Total OT"
            'Me.grdAutoOT.RootTable.Columns("OverTimeRate").Caption = "Rate/Hr"
            'Me.grdAutoOT.RootTable.Columns("OverTimeAmount").Caption = "OT Amount"
            Me.grdAutoOT.RootTable.Columns("OverTime_StartTime").Caption = "OT Start At"
            Me.grdAutoOT.RootTable.Columns("OverTime_StartTime").FormatString = "hh:mm tt"
            Me.grdAutoOT.RootTable.Columns("Salary").FormatString = "N"
            Me.grdAutoOT.RootTable.Columns("RegularDayHrs").FormatString = "N"
            Me.grdAutoOT.RootTable.Columns("OffDayHrs").FormatString = "N"
            Me.grdAutoOT.RootTable.Columns("RegularDayOTAmount").FormatString = "N"
            Me.grdAutoOT.RootTable.Columns("OffDayOTAmount").FormatString = "N"
            Me.grdAutoOT.RootTable.Columns("RegularDayOTRate").FormatString = "N"
            Me.grdAutoOT.RootTable.Columns("OffDayOTRate").FormatString = "N"
            Me.grdAutoOT.RootTable.Columns("TotalAmount").FormatString = "N"
            'Me.grdAutoOT.RootTable.Columns("OverTimeRate").FormatString = "N"
            'Me.grdAutoOT.RootTable.Columns("TotalOverTime").FormatString = "N"
            'Me.grdAutoOT.RootTable.Columns("OverTimeAmount").FormatString = "N"
            Me.grdAutoOT.RootTable.Columns("Salary").TotalFormatString = "N"
            Me.grdAutoOT.RootTable.Columns("RegularDayHrs").TotalFormatString = "N"
            Me.grdAutoOT.RootTable.Columns("OffDayHrs").TotalFormatString = "N"
            Me.grdAutoOT.RootTable.Columns("RegularDayOTAmount").TotalFormatString = "N"
            Me.grdAutoOT.RootTable.Columns("OffDayOTAmount").TotalFormatString = "N"
            Me.grdAutoOT.RootTable.Columns("TotalAmount").TotalFormatString = "N"
            'Me.grdAutoOT.RootTable.Columns("OverTimeRate").TotalFormatString = "N"
            'Me.grdAutoOT.RootTable.Columns("TotalOverTime").TotalFormatString = "N"
            'Me.grdAutoOT.RootTable.Columns("OverTimeAmount").TotalFormatString = "N"
            Me.grdAutoOT.RootTable.Columns("Salary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdAutoOT.RootTable.Columns("RegularDayHrs").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdAutoOT.RootTable.Columns("OffDayHrs").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdAutoOT.RootTable.Columns("RegularDayOTAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdAutoOT.RootTable.Columns("OffDayOTAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdAutoOT.RootTable.Columns("TotalAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grdAutoOT.RootTable.Columns("OverTimeRate").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grdAutoOT.RootTable.Columns("TotalOverTime").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grdAutoOT.RootTable.Columns("OverTimeAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdAutoOT.RootTable.Columns("Salary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdAutoOT.RootTable.Columns("OverTimeRate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdAutoOT.RootTable.Columns("TotalOverTime").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdAutoOT.RootTable.Columns("OverTimeAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdAutoOT.RootTable.Columns("Salary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdAutoOT.RootTable.Columns("OverTimeRate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdAutoOT.RootTable.Columns("TotalOverTime").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdAutoOT.RootTable.Columns("OverTimeAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdAutoOT.RootTable.Columns("Salary").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAutoOT.RootTable.Columns("RegularDayHrs").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAutoOT.RootTable.Columns("OffDayHrs").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAutoOT.RootTable.Columns("RegularDayOTAmount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAutoOT.RootTable.Columns("OffDayOTAmount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAutoOT.RootTable.Columns("RegularDayOTRate").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAutoOT.RootTable.Columns("OffDayOTRate").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAutoOT.RootTable.Columns("TotalAmount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grdAutoOT.RootTable.Columns("OverTimeRate").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grdAutoOT.RootTable.Columns("TotalOverTime").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grdAutoOT.RootTable.Columns("OverTimeAmount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAutoOT.RootTable.Columns("OverTime_StartTime").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAutoOT.RootTable.Columns("Employee_Name").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAutoOT.RootTable.Columns("Code").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAutoOT.RootTable.Columns("Salary").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdAutoOT.RootTable.Columns("RegularDayHrs").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdAutoOT.RootTable.Columns("OffDayHrs").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdAutoOT.RootTable.Columns("RegularDayOTAmount").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdAutoOT.RootTable.Columns("OffDayOTAmount").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdAutoOT.RootTable.Columns("RegularDayOTRate").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdAutoOT.RootTable.Columns("OffDayOTRate").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdAutoOT.RootTable.Columns("TotalAmount").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            'Me.grdAutoOT.RootTable.Columns("OverTimeRate").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            'Me.grdAutoOT.RootTable.Columns("TotalOverTime").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            'Me.grdAutoOT.RootTable.Columns("OverTimeAmount").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdAutoOT.RootTable.Columns("OverTime_StartTime").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdAutoOT.RootTable.Columns("Employee_Name").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdAutoOT.RootTable.Columns("Code").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdAutoOT.RootTable.Columns("ShiftStartTime").Visible = False
            Me.grdAutoOT.RootTable.Columns("ShiftEndTime").Visible = False
            Me.grdAutoOT.RootTable.Columns("Employee_ID").Visible = False
            GetSavedOverTime()
            Me.CellEdited = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Application.DoEvents()
        End Try
    End Sub
    Private Sub grdAutoOT_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdAutoOT.CellUpdated
        Try
            Dim NormalMul As String = String.Empty
            Dim OffMul As String = String.Empty
            If Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString) > 1 Then
                NormalMul = Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString)
            Else
                NormalMul = 1
            End If
            If Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString) > 1 Then
                OffMul = Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString)
            Else
                OffMul = 1
            End If
            Dim OffDays As String = String.Empty
            OffDays = getConfigValueByType("DayOff")
            Dim Key As Integer = Num(e.Column.Key)
            Dim dateDay As New DateTime(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month, Key)
            Dim MultiOffDays() As String = OffDays.Split(",")
            Dim dayOfWeek As DayOfWeek = dateDay.DayOfWeek
            Dim Counter As Integer = 0
            If MultiOffDays.Length > 0 Then
                For Each OffDay As String In MultiOffDays
                    If dayOfWeek.ToString = OffDay.ToString Then
                        Counter += 1
                    End If
                Next
                If Counter > 0 Then
                    If OffDayHours < Val(Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value.ToString) Then
                        Dim DifferenceHours As Double = Val(Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value.ToString) - OffDayHours
                        Me.grdAutoOT.GetRow.Cells("OffDayOTAmount").Value += DifferenceHours * (Me.grdAutoOT.GetRow.Cells("OffDayOTRate").Value)
                        Me.grdAutoOT.GetRow.Cells("OffDayHrs").Value += DifferenceHours
                    Else
                        Dim DifferenceHours As Double = OffDayHours - Val(Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value.ToString)
                        Me.grdAutoOT.GetRow.Cells("OffDayOTAmount").Value -= DifferenceHours * (Me.grdAutoOT.GetRow.Cells("OffDayOTRate").Value)
                        Me.grdAutoOT.GetRow.Cells("OffDayHrs").Value -= DifferenceHours
                    End If
                Else
                    If RegularDayHours < Val(Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value.ToString) Then
                        Dim DifferenceHours As Double = Val(Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value.ToString) - RegularDayHours
                        Me.grdAutoOT.GetRow.Cells("RegularDayOTAmount").Value += DifferenceHours * (Me.grdAutoOT.GetRow.Cells("RegularDayOTRate").Value)
                        Me.grdAutoOT.GetRow.Cells("RegularDayHrs").Value += DifferenceHours
                    Else
                        Dim DifferenceHours As Double = RegularDayHours - Val(Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value.ToString)
                        Me.grdAutoOT.GetRow.Cells("RegularDayOTAmount").Value -= DifferenceHours * (Me.grdAutoOT.GetRow.Cells("RegularDayOTRate").Value)
                        Me.grdAutoOT.GetRow.Cells("RegularDayHrs").Value -= DifferenceHours
                    End If
                End If
            Else
                If RegularDayHours > Val(Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value.ToString) Then
                    Dim DifferenceHours As Double = RegularDayHours - Val(Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value.ToString)
                    Me.grdAutoOT.GetRow.Cells("RegularDayOTAmount").Value += DifferenceHours * (Me.grdAutoOT.GetRow.Cells("RegularDayOTRate").Value)
                    Me.grdAutoOT.GetRow.Cells("RegularDayHrs").Value += DifferenceHours
                    'Counter += 1
                Else
                    Dim DifferenceHours As Double = RegularDayHours - Val(Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value.ToString)
                    Me.grdAutoOT.GetRow.Cells("RegularDayOTAmount").Value -= DifferenceHours * (Me.grdAutoOT.GetRow.Cells("RegularDayOTRate").Value)
                    Me.grdAutoOT.GetRow.Cells("RegularDayHrs").Value -= DifferenceHours
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub GetEmployeeList()
    '    Try
    '        Me.lblProgress.Visible = True
    '        Application.DoEvents()

    '        Dim str As String = String.Empty
    '        Dim dt As New DataTable


    '        'If Not getConfigValueByType("KeepConfigurationMonthDays").ToString.ToUpper = "TRUE" Then
    '        '    intDayinMonth = DateTime.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month)
    '        'Else
    '        '    intDayinMonth = Val(getConfigValueByType("Working_Days").ToString)
    '        'End If

    '        'If getConfigValueByType("ApplyDefaultWorkingHoursOnOverTime").ToString.Replace("Error", "False").Replace("''", "False") = "False" Then
    '        '    totalHours = 0
    '        'Else
    '        '    totalHours = Val(getConfigValueByType("DefaultWorkingHours").ToString)
    '        'End If

    '        ''str = "select Employee_Id, Employee_Code as Code, Employee_Name, 0.00 as TotalOverTime, Salary, " & _
    '        ''        " (Salary/" & intDayinMonth & ")/" & IIf(totalHours = 0, " Convert(Numeric(5, 1), Convert(numeric(18, 2), DateDiff(Minute, Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftStartTime), 108), Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftEndTime), 108))) / 60)", totalHours) & " as OverTimeRate," & _
    '        ''        " 0.00 as OverTimeAmount, Isnull( ShiftTable. OverTime_StartTime, ShiftEndTime) as OverTime_StartTime , ShiftTable.ShiftStartTime, ShiftTable.ShiftEndTime from tblDefEmployee  INNER JOIN ShiftTable ON tblDefEmployee.ShiftGroupId = ShiftTable.ShiftId " & _
    '        ''        "   where isnull(Leaving_Date, '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "') >=  '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "' " & _
    '        ''        "  " & IIf(cmbDepartment.SelectedIndex > 0, " and dept_id=" & Me.cmbDepartment.SelectedValue, "") & " " & _
    '        ''        " order by Employee_Name "
    '        'If getConfigValueByType("SalaryGenerationPercentageBased").ToString.Replace("Error", "False").Replace("''", "False") = "False" Then
    '        '    str = "select Employee_Id, Employee_Code as Code, Employee_Name, 0.00 as TotalOverTime, Salary, " & _
    '        '                    " (Salary/" & intDayinMonth & ")/" & IIf(totalHours = 0, " Convert(Numeric(5, 1), Convert(numeric(18, 2), DateDiff(Minute, Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftStartTime), 108), Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftEndTime), 108))) / 60)", totalHours) & " as OverTimeRate," & _
    '        '                    " 0.00 as OverTimeAmount, IsNull(ShiftTable.OverTime_StartTime, ShiftTable.ShiftEndTime) as OverTime_StartTime , ShiftTable.ShiftStartTime, ShiftTable.ShiftEndTime from ShiftScheduleTable INNER JOIN ShiftTable ON ShiftScheduleTable.ShiftId = ShiftTable.ShiftId INNER JOIN tblDefEmployee INNER JOIN ShiftGroupTable ON tblDefEmployee.ShiftGroupId = ShiftGroupTable.ShiftGroupId ON ShiftScheduleTable.ShiftGroupId = ShiftGroupTable.ShiftGroupId " & _
    '        '                    "   where isnull(Leaving_Date, '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "') >=  '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "' " & _
    '        '                    "  " & IIf(cmbDepartment.SelectedIndex > 0, " and dept_id=" & Me.cmbDepartment.SelectedValue, "") & " " & _
    '        '                    " order by Employee_Name "
    '        'Else
    '        '    str = "select tblDefEmployee.Employee_Id, Employee_Code as Code, Employee_Name, 0.00 as TotalOverTime,(tblDefEmployee.Salary*tblEmployeeAccounts.Amount)/100 As Salary, " & _
    '        '                    " ((tblDefEmployee.Salary*tblEmployeeAccounts.Amount)/100/" & intDayinMonth & ")/" & IIf(totalHours = 0, " Convert(Numeric(5, 1), Convert(numeric(18, 2), DateDiff(Minute, Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftStartTime), 108), Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftEndTime), 108))) / 60)", totalHours) & " as OverTimeRate," & _
    '        '                    " 0.00 as OverTimeAmount, IsNull(ShiftTable.OverTime_StartTime, ShiftTable.ShiftEndTime) as OverTime_StartTime , ShiftTable.ShiftStartTime, ShiftTable.ShiftEndTime from ShiftScheduleTable INNER JOIN ShiftTable ON ShiftScheduleTable.ShiftId = ShiftTable.ShiftId INNER JOIN tblDefEmployee INNER JOIN ShiftGroupTable ON tblDefEmployee.ShiftGroupId = ShiftGroupTable.ShiftGroupId ON ShiftScheduleTable.ShiftGroupId = ShiftGroupTable.ShiftGroupId  LEFT OUTER JOIN tblEmployeeAccounts ON tblDefEmployee.Employee_ID = tblEmployeeAccounts.Employee_Id " & _
    '        '                    "   where isnull(Leaving_Date, '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "') >=  '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "' " & _
    '        '                    "  " & IIf(cmbDepartment.SelectedIndex > 0, " and dept_id=" & Me.cmbDepartment.SelectedValue, "") & " And tblEmployeeAccounts.Type_Id = 1 " & _
    '        '                    " order by Employee_Name "
    '        'End If
    '        'Ali Faisal : Commented to get Over Time Rate by Configuration based values
    '        If Not getConfigValueByType("OverTimeBasedOnWorkingDays").ToString = "True" Then
    '            intDayinMonth = DateTime.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month)
    '        Else
    '            intDayinMonth = Val(getConfigValueByType("OverTimeWorkingDays").ToString)
    '        End If
    '        If getConfigValueByType("ApplyDefaultWorkingHoursOnOverTime").ToString = "False" Then
    '            totalHours = 0
    '        Else
    '            totalHours = Val(getConfigValueByType("DefaultWorkingHours").ToString)
    '        End If
    '        Dim SFPer As String = String.Empty

    '        If Val(getConfigValueByType("SalaryGenerationPercentageBased").ToString) > 1 Then
    '            SFPer = Val(getConfigValueByType("SalaryGenerationPercentageBased").ToString) / 100
    '        Else
    '            SFPer = 1
    '        End If

    '        If getConfigValueByType("SalaryGenerationPercentageBased").ToString.Replace("Error", "False").Replace("''", "False") = "False" Then
    '            str = "select Employee_Id, Employee_Code as Code, Employee_Name, 0.00 as TotalOverTime, Salary, " & _
    '                            " (Salary * " & SFPer & " /" & intDayinMonth & ")/" & IIf(totalHours = 0, " Convert(Numeric(5, 1), Convert(numeric(18, 2), DateDiff(Minute, Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftStartTime), 108), Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftEndTime), 108))) / 60)", totalHours) & " as OverTimeRate," & _
    '                            " 0.00 as OverTimeAmount, IsNull(ShiftTable.OverTime_StartTime, ShiftTable.ShiftEndTime) as OverTime_StartTime , ShiftTable.ShiftStartTime, ShiftTable.ShiftEndTime from ShiftScheduleTable INNER JOIN ShiftTable ON ShiftScheduleTable.ShiftId = ShiftTable.ShiftId INNER JOIN tblDefEmployee INNER JOIN ShiftGroupTable ON tblDefEmployee.ShiftGroupId = ShiftGroupTable.ShiftGroupId ON ShiftScheduleTable.ShiftGroupId = ShiftGroupTable.ShiftGroupId " & _
    '                            "   where isnull(Leaving_Date, '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "') >=  '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "' " & _
    '                            "  " & IIf(cmbDepartment.SelectedIndex > 0, " and dept_id=" & Me.cmbDepartment.SelectedValue, "") & " " & _
    '                            " order by Employee_Name "
    '        Else
    '            str = "select tblDefEmployee.Employee_Id, Employee_Code as Code, Employee_Name, 0.00 as TotalOverTime,(tblDefEmployee.Salary*tblEmployeeAccounts.Amount)/100 As Salary, " & _
    '                            " ((tblDefEmployee.Salary*tblEmployeeAccounts.Amount)/100/" & intDayinMonth & ")/" & IIf(totalHours = 0, " Convert(Numeric(5, 1), Convert(numeric(18, 2), DateDiff(Minute, Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftStartTime), 108), Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftEndTime), 108))) / 60)", totalHours) & " as OverTimeRate," & _
    '                            " 0.00 as OverTimeAmount, IsNull(ShiftTable.OverTime_StartTime, ShiftTable.ShiftEndTime) as OverTime_StartTime , ShiftTable.ShiftStartTime, ShiftTable.ShiftEndTime from ShiftScheduleTable INNER JOIN ShiftTable ON ShiftScheduleTable.ShiftId = ShiftTable.ShiftId INNER JOIN tblDefEmployee INNER JOIN ShiftGroupTable ON tblDefEmployee.ShiftGroupId = ShiftGroupTable.ShiftGroupId ON ShiftScheduleTable.ShiftGroupId = ShiftGroupTable.ShiftGroupId  LEFT OUTER JOIN tblEmployeeAccounts ON tblDefEmployee.Employee_ID = tblEmployeeAccounts.Employee_Id " & _
    '                            "   where isnull(Leaving_Date, '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "') >=  '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 00:00:00") & "' " & _
    '                            "  " & IIf(cmbDepartment.SelectedIndex > 0, " and dept_id=" & Me.cmbDepartment.SelectedValue, "") & " And tblEmployeeAccounts.Type_Id = 1 " & _
    '                            " order by Employee_Name "
    '        End If
    '        dt = GetDataTable(str)

    '        Dim ExpressionString As String = String.Empty

    '        For i As Integer = 1 To Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month)
    '            'Dim DayCol As New Janus.Windows.GridEX.GridEXColumn(i, Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.TextBox)
    '            'Me.grdAutoOT.RootTable.Columns.Add(DayCol)
    '            Dim dCol As DataColumn = New DataColumn("Day" & i, System.Type.GetType("System.Double"))
    '            dCol.DefaultValue = 0
    '            dt.Columns.Add(dCol)
    '            If ExpressionString.Length > 0 Then ExpressionString = ExpressionString & " + "
    '            ExpressionString = ExpressionString & "[Day" & i & "]"
    '        Next
    '        'For i As Integer = 1 To Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month)
    '        '    Dim dateDay As New DateTime(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month, i)
    '        '    Dim dayOfWeek As DayOfWeek = dateDay.DayOfWeek
    '        '    If dayOfWeek.ToString = OffDays.ToString Then
    '        '        dt.Columns("TotalOverTime").Expression = ExpressionString
    '        '        dt.Columns("OverTimeAmount").Expression = "[TotalOverTime] * [OverTimeRate] * " & OffMul & ""
    '        '    Else
    '        '        dt.Columns("TotalOverTime").Expression = ExpressionString
    '        '        dt.Columns("OverTimeAmount").Expression = "[TotalOverTime] * [OverTimeRate] * " & NormalMul & ""
    '        '    End If
    '        'Next
    '        dt.Columns("TotalOverTime").Expression = ExpressionString
    '        'dt.Columns("OverTimeAmount").Expression = "[TotalOverTime] * [OverTimeRate]"

    '        Me.grdAutoOT.DataSource = dt
    '        Me.grdAutoOT.RetrieveStructure()

    '        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdAutoOT.GetRows
    '            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdAutoOT.RootTable.Columns
    '                If col.Key.Contains("Day") Then
    '                    Dim NormalMul As String = String.Empty
    '                    Dim OffMul As String = String.Empty
    '                    If Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString) > 1 Then
    '                        NormalMul = Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString)
    '                    Else
    '                        NormalMul = 1
    '                    End If
    '                    If Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString) > 1 Then
    '                        OffMul = Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString)
    '                    Else
    '                        OffMul = 1
    '                    End If
    '                    Dim OffDays As String = String.Empty
    '                    OffDays = getConfigValueByType("DayOff")
    '                    Dim Key As Integer = Num(col.Key)
    '                    Dim dateDay As New DateTime(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month, Key)
    '                    Dim dayOfWeek As DayOfWeek = dateDay.DayOfWeek
    '                    r.BeginEdit()
    '                    If dayOfWeek.ToString = OffDays.ToString Then
    '                        r.Cells("OverTimeAmount").Value += (r.Cells(col.Key.ToString).Value) * (r.Cells("OverTimeRate").Value) * OffMul
    '                    Else
    '                        r.Cells("OverTimeAmount").Value += (r.Cells(col.Key).Value) * (r.Cells("OverTimeRate").Value) * NormalMul
    '                    End If
    '                    r.EndEdit()
    '                End If
    '            Next
    '        Next

    '        'Dim col As New Janus.Windows.GridEX.GridEXColumn("Column1")
    '        'col.UseHeaderSelector = True
    '        'col.ActAsSelector = True
    '        'Me.grdAutoOT.RootTable.Columns.Insert(0, col)



    '        Me.grdAutoOT.FrozenColumns = 4

    '        Me.grdAutoOT.RootTable.Columns("TotalOverTime").CellStyle.FontBold = Janus.Windows.GridEX.TriState.True

    '        Me.grdAutoOT.RootTable.Columns("TotalOverTime").Caption = "Total OT"
    '        Me.grdAutoOT.RootTable.Columns("OverTimeRate").Caption = "Rate/Hr"
    '        Me.grdAutoOT.RootTable.Columns("OverTimeAmount").Caption = "OT Amount"
    '        Me.grdAutoOT.RootTable.Columns("OverTime_StartTime").Caption = "OT Start At"
    '        Me.grdAutoOT.RootTable.Columns("OverTime_StartTime").FormatString = "hh:mm tt"

    '        Me.grdAutoOT.RootTable.Columns("Salary").FormatString = "N"
    '        Me.grdAutoOT.RootTable.Columns("OverTimeRate").FormatString = "N"
    '        Me.grdAutoOT.RootTable.Columns("TotalOverTime").FormatString = "N"
    '        Me.grdAutoOT.RootTable.Columns("OverTimeAmount").FormatString = "N"

    '        Me.grdAutoOT.RootTable.Columns("Salary").TotalFormatString = "N"
    '        Me.grdAutoOT.RootTable.Columns("OverTimeRate").TotalFormatString = "N"
    '        Me.grdAutoOT.RootTable.Columns("TotalOverTime").TotalFormatString = "N"
    '        Me.grdAutoOT.RootTable.Columns("OverTimeAmount").TotalFormatString = "N"

    '        Me.grdAutoOT.RootTable.Columns("Salary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grdAutoOT.RootTable.Columns("OverTimeRate").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grdAutoOT.RootTable.Columns("TotalOverTime").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grdAutoOT.RootTable.Columns("OverTimeAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

    '        Me.grdAutoOT.RootTable.Columns("Salary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grdAutoOT.RootTable.Columns("OverTimeRate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grdAutoOT.RootTable.Columns("TotalOverTime").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grdAutoOT.RootTable.Columns("OverTimeAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

    '        Me.grdAutoOT.RootTable.Columns("Salary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grdAutoOT.RootTable.Columns("OverTimeRate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grdAutoOT.RootTable.Columns("TotalOverTime").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grdAutoOT.RootTable.Columns("OverTimeAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

    '        Me.grdAutoOT.RootTable.Columns("Salary").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '        Me.grdAutoOT.RootTable.Columns("OverTimeRate").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '        Me.grdAutoOT.RootTable.Columns("TotalOverTime").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '        Me.grdAutoOT.RootTable.Columns("OverTimeAmount").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '        Me.grdAutoOT.RootTable.Columns("OverTime_StartTime").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '        Me.grdAutoOT.RootTable.Columns("Employee_Name").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '        Me.grdAutoOT.RootTable.Columns("Code").EditType = Janus.Windows.GridEX.EditType.NoEdit

    '        Me.grdAutoOT.RootTable.Columns("Salary").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
    '        Me.grdAutoOT.RootTable.Columns("OverTimeRate").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
    '        Me.grdAutoOT.RootTable.Columns("TotalOverTime").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
    '        Me.grdAutoOT.RootTable.Columns("OverTimeAmount").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
    '        Me.grdAutoOT.RootTable.Columns("OverTime_StartTime").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
    '        Me.grdAutoOT.RootTable.Columns("Employee_Name").FilterEditType = Janus.Windows.GridEX.EditType.TextBox
    '        Me.grdAutoOT.RootTable.Columns("Code").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox

    '        Me.grdAutoOT.RootTable.Columns("ShiftStartTime").Visible = False
    '        Me.grdAutoOT.RootTable.Columns("ShiftEndTime").Visible = False
    '        Me.grdAutoOT.RootTable.Columns("Employee_ID").Visible = False

    '        GetSavedOverTime()
    '        Me.CellEdited = False

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    Finally

    '        Me.lblProgress.Visible = False
    '        Application.DoEvents()

    '    End Try
    'End Sub

    Private Sub frmEmployeeAutoOverTime_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()

            ResetControls()



        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
            Application.DoEvents()

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Save()
            msg_Information("Recrod saved successfully")
        Catch ex As Exception
            msg_Error(ex.Message)
        Finally

            Me.lblProgress.Visible = False
            Application.DoEvents()

        End Try
    End Sub

    ''' <summary>
    ''' Overtime will be calculated from table "tblAttendanceDetail" based on MAX(AttendanceTime) that is > OverTime_StartTime of "ShiftTable"
    ''' Existing data will be overwritten while populating data in the grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnImportAttendance_Click(sender As Object, e As EventArgs) Handles btnImportAttendance.Click
        Try

            If (Me.grdAutoOT.RootTable.Columns.Contains("Day1") AndAlso Me.grdAutoOT.RootTable.Columns("Day1").CellStyle.BackColor = Color.White) Or Me.CellEdited = True Then
                If Not msg_Confirm("This operation will overwrite exiting data." & Chr(10) & "Are you sure you want to perform this operation?") = True Then
                    Exit Sub
                End If
            End If

            Me.lblProgress.Visible = True
            Application.DoEvents()

            Dim str As String = String.Empty
            Dim dt As New DataTable
            'Ali Faisal : TFS1358 : 29-Aug-2017 : Query modification to get the over time from attendance records
            'str = " SELECT      tblAttendanceDetail.EmpId, tblAttendanceDetail.AttendanceDate, Convert(Numeric(5,1),Convert(numeric(18,2), DATEDIFF(minute, Convert(Varchar(5),ShiftTable.OverTime_StartTime,108) , Convert(Varchar(5),Max(tblAttendanceDetail.AttendanceTime),108)))/60) as OverTime, Convert(Numeric(5, 1), Convert(numeric(18, 2), DateDiff(Minute, Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftStartTime), 108), Convert(Varchar(5), Convert(DateTime, ShiftTable.ShiftEndTime), 108))) / 60) as WorkingHours " & _
            '        " FROM tblAttendanceDetail INNER JOIN ShiftTable ON tblAttendanceDetail.ShiftId = ShiftTable.ShiftId INNER JOIN tblDefEmployee ON tblAttendanceDetail.EmpId = tblDefEmployee.Employee_ID " & _
            '        " WHERE  " & IIf(Me.cmbDepartment.SelectedIndex > 0, " dept_id=" & Me.cmbDepartment.SelectedValue & " and ", "") & " (tblAttendanceDetail.AttendanceDate BETWEEN '" & Me.dtpMonth.Value.ToString("yyyy-MMM-01 00:00:00") & "' AND '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 23:59:59") & "') AND  " & _
            '        " (Convert(Varchar(5),tblAttendanceDetail.AttendanceTime,108) > Convert(Varchar(5),ShiftTable.OverTime_StartTime,108)) " & _
            '        "  Group by  tblAttendanceDetail.EmpId, tblAttendanceDetail.AttendanceDate, ShiftTable.OverTime_StartTime, ShiftTable.ShiftStartTime, ShiftTable.ShiftEndTime " & _
            '        " Order by 1,2 "
            str = " SELECT tblAttendanceDetail.EmpId, tblAttendanceDetail.AttendanceDate, Convert(numeric(5,2),Convert(Numeric(18,2),DateDiff(minute,Convert(Varchar(8),Convert(DateTime,ShiftTable.OverTime_StartTime,121),108),Convert(Varchar(8),tblAttendanceDetail.AttendanceTime,108)))/60) OverTime, Convert(Numeric(5, 1), Convert(numeric(18, 2), DateDiff(Minute, Convert(Varchar(8), Convert(DateTime, ShiftTable.ShiftStartTime), 108), Convert(Varchar(8), Convert(DateTime, ShiftTable.ShiftEndTime), 108))) / 60) as WorkingHours " & _
                    " FROM tblAttendanceDetail INNER JOIN ShiftTable ON tblAttendanceDetail.ShiftId = ShiftTable.ShiftId INNER JOIN tblDefEmployee ON tblAttendanceDetail.EmpId = tblDefEmployee.Employee_ID " & _
                    " WHERE  " & IIf(Me.cmbDepartment.SelectedIndex > 0, " dept_id=" & Me.cmbDepartment.SelectedValue & " and ", "") & " (tblAttendanceDetail.AttendanceDate BETWEEN '" & Me.dtpMonth.Value.ToString("yyyy-MMM-01 00:00:00") & "' AND '" & Me.dtpMonth.Value.ToString("yyyy-MMM-" & Date.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month) & " 23:59:59") & "') AND  " & _
                    " (Convert(Varchar(8),tblAttendanceDetail.AttendanceTime,108) > Convert(VarChar(8),Convert(Datetime,ShiftTable.OverTime_StartTime,121),108)) " & _
                    "  Group by  tblAttendanceDetail.EmpId, tblAttendanceDetail.AttendanceDate, ShiftTable.OverTime_StartTime, ShiftTable.ShiftStartTime, ShiftTable.ShiftEndTime,tblAttendanceDetail.AttendanceTime " & _
                    " Order by 1,2 "
            'Ali Faisal : TFS1358 : 29-Aug-2017 : End
            dt = GetDataTable(str)

            '// Place holder for Last employee in loop
            Dim EmployeeId As Integer = 0

            '// Row number in grid of EmployeeId
            Dim RowN As Integer = -1

            Dim dtData As DataTable = Me.grdAutoOT.DataSource
            
            For Each row As DataRow In dt.Rows

                If Val(row.Item(0).ToString) <> EmployeeId Then
                    EmployeeId = Val(row.Item(0).ToString)
                    RowN = FindEmployeeRowNumber(Val(row.Item(0).ToString), dtData)
                    'Ali Faisal : TFS1358 : 29-Aug-2017 : Commented because overtimerate column is no more in use
                    'dtData.Rows(RowN).Item("OverTimeRate") = (Val(dtData.Rows(RowN).Item("Salary").ToString) / intDayinMonth) / IIf(totalHours = 0, Val(row.Item("WorkingHours").ToString), totalHours)
                    'Ali Faisal : TFS1358 : 29-Aug-2017 : End
                End If

                If RowN >= 0 Then

                    'Dim frmt As New Janus.Windows.GridEX.GridEXFormatStyle
                    'frmt.BackColor = Color.Gray
                    'Me.grdAutoOT.GetRow(RowN).Cells("Day" & CType(row.Item("AttendanceDate").ToString, DateTime).Day).FormatStyle = frmt

                    dtData.Rows(RowN).Item("Day" & CType(row.Item("AttendanceDate").ToString, DateTime).Day) = Val(row.Item("OverTime").ToString)
                End If
                'Ali Faisal : TFS1358 : 29-Aug-2017 : Handles to update the records from over time columns to NormalDayOverTime and OffDayOvetTime 
                Try
                    Dim NormalMul As String = String.Empty
                    Dim OffMul As String = String.Empty
                    If Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString) > 1 Then
                        NormalMul = Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString)
                    Else
                        NormalMul = 1
                    End If
                    If Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString) > 1 Then
                        OffMul = Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString)
                    Else
                        OffMul = 1
                    End If
                    Dim OffDays As String = String.Empty
                    OffDays = getConfigValueByType("DayOff")
                    Dim DateDay1 As DateTime = row.Item("AttendanceDate").ToString
                    Dim Key As Integer = DatePart(DateInterval.Day, DateDay1)
                    Dim dateDay As New DateTime(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month, Key)
                    Dim MultiOffDays() As String = OffDays.Split(",")
                    Dim dayOfWeek As DayOfWeek = dateDay.DayOfWeek
                    Dim Counter As Integer = 0
                    If MultiOffDays.Length > 0 Then
                        For Each OffDay As String In MultiOffDays
                            If dayOfWeek.ToString = OffDay.ToString Then
                                Counter += 1
                            End If
                        Next
                        row.BeginEdit()
                        If Counter > 0 Then
                            Dim DifferenceHours As Double = Val(dtData.Rows(RowN).Item("Day" & CType(row.Item("AttendanceDate").ToString, DateTime).Day))
                            dtData.Rows(RowN).Item("OffDayOTAmount") += DifferenceHours * (dtData.Rows(RowN).Item("OffDayOTRate"))
                            dtData.Rows(RowN).Item("OffDayHrs") += DifferenceHours
                        Else
                            Dim DifferenceHours As Double = Val(dtData.Rows(RowN).Item("Day" & CType(row.Item("AttendanceDate").ToString, DateTime).Day))
                            dtData.Rows(RowN).Item("RegularDayOTAmount") += DifferenceHours * (dtData.Rows(RowN).Item("RegularDayOTRate"))
                            dtData.Rows(RowN).Item("RegularDayHrs") += DifferenceHours
                        End If
                        row.EndEdit()
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
                'Ali Faisal : TFS1358 : 29-Aug-2017 : End
            Next

            For Each grdCol As Janus.Windows.GridEX.GridEXColumn In grdAutoOT.RootTable.Columns
                If grdCol.Key.StartsWith("Day") Then
                    grdCol.CellStyle.BackColor = Color.LightGray
                End If
            Next

            Me.grdAutoOT.AutoSizeColumns()
            Me.grdAutoOT.UpdateData()
            msg_Information("Attendance import process completed successfully.")
            Me.CellEdited = False
        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Application.DoEvents()

        End Try
    End Sub

    ''' <summary>
    ''' Function will be used to find Row Number in data table against specific employee
    ''' </summary>
    ''' <param name="EmployeeId">Employee Id to find in the data table</param>
    ''' <param name="dtData">Data table from which row number will be find</param>
    ''' <returns>Row number will be returned normaly and "-1" will be returned in case employee does not exist in the data table</returns>
    ''' <remarks></remarks>
    Function FindEmployeeRowNumber(EmployeeId As Integer, dtData As DataTable) As Integer
        Try
            For i As Integer = 0 To dtData.Rows.Count - 1
                If Val(dtData.Rows(i).Item("Employee_Id").ToString) = EmployeeId Then
                    Return i
                End If
            Next

            Return -1

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Load button's color will be changed to highlight user that data in grid might be different from the criteria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dtpMonth_ValueChanged(sender As Object, e As EventArgs) Handles dtpMonth.ValueChanged
        Try
            btnLoad.BackColor = Color.FromArgb(255, 255, 192)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Load button's color will be changed to highlight user that data in grid might be different from the criteria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged

    End Sub

    Private Sub grdAutoOT_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdAutoOT.CellEdited
    End Sub

    Private Sub grdAutoOT_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdAutoOT.CellValueChanged
        Try
            Me.CellEdited = True

            Dim NormalMul As String = String.Empty
            Dim OffMul As String = String.Empty
            If Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString) > 1 Then
                NormalMul = Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString)
            Else
                NormalMul = 1
            End If
            If Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString) > 1 Then
                OffMul = Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString)
            Else
                OffMul = 1
            End If
            Dim OffDays As String = String.Empty
            OffDays = getConfigValueByType("DayOff")
            Dim Key As Integer = Num(e.Column.Key)
            Dim dateDay As New DateTime(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month, Key)
            Dim MultiOffDays() As String = OffDays.Split(",")
            Dim dayOfWeek As DayOfWeek = dateDay.DayOfWeek
            Dim Counter As Integer = 0
            If MultiOffDays.Length > 0 Then
                For Each OffDay As String In MultiOffDays
                    If dayOfWeek.ToString = OffDay.ToString Then
                        Counter += 1
                    End If
                Next
            End If
            If Counter > 0 Then
                OffDayHours = (Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value)
                'Me.grdAutoOT.GetRow.Cells("OffDayOTAmount").Value += (Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value) * (Me.grdAutoOT.GetRow.Cells("OffDayOTRate").Value) * OffMul
                'Me.grdAutoOT.GetRow.Cells("OffDayHrs").Value += (Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value)
            Else
                RegularDayHours = (Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbDepartment_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedValueChanged
        Try
            btnLoad.BackColor = Color.FromArgb(255, 255, 192)

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Sub ResetControls()
        Try
            Me.lblProgress.Visible = True
            Application.DoEvents()

            If Not getConfigValueByType("KeepConfigurationMonthDays").ToString.ToUpper = "TRUE" Then
                intDayinMonth = DateTime.DaysInMonth(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month)
            Else
                intDayinMonth = Val(getConfigValueByType("Working_Days").ToString)
            End If

            If getConfigValueByType("ApplyDefaultWorkingHoursOnOverTime").ToString.Replace("Error", "False").Replace("''", "False") = "False" Then
                totalHours = 0
            Else
                totalHours = Val(getConfigValueByType("DefaultWorkingHours").ToString)
            End If

            FillDropDown(Me.cmbDepartment, "Select EmployeeDeptID, EmployeeDeptName, IsNull(DeptAccountHeadId,0) as DeptAccountHeadId from EmployeeDeptDefTable")

            btnLoad.BackColor = Color.FromArgb(255, 255, 192)

            Me.dtpMonth.Format = DateTimePickerFormat.Custom
            Me.dtpMonth.CustomFormat = "MMMM yyyy"

            GetSecurityRights()

        Catch ex As Exception
            Throw ex
        Finally

            Me.lblProgress.Visible = False
            Application.DoEvents()

        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            ResetControls()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Shared Function Num(ByVal value As String) As Integer
        Dim returnVal As String = String.Empty
        Dim collection As MatchCollection = Regex.Matches(value, "\d+")
        For Each m As Match In collection
            returnVal += m.ToString()
        Next
        Return Convert.ToInt32(returnVal)
    End Function
    Private Function GetOffDays(ByVal Key1 As String) As Integer
        Try
            Dim OffDays As String = String.Empty
            OffDays = getConfigValueByType("DayOff")
            Dim Key As Integer = Num(Key1)
            Dim dateDay As New DateTime(Me.dtpMonth.Value.Year, Me.dtpMonth.Value.Month, Key)
            Dim MultiOffDays() As String = OffDays.Split(",")
            Dim dayOfWeek As DayOfWeek = dateDay.DayOfWeek
            Dim Counter As Integer = 0
            If MultiOffDays.Length > 0 Then
                For Each OffDay As String In MultiOffDays
                    If dayOfWeek.ToString = OffDay.ToString Then
                        Counter += 1
                    End If
                Next
                '    If Counter > 0 Then
                '        Me.grdAutoOT.GetRow.Cells("OffDayOTAmount").Value += (Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value) * (Me.grdAutoOT.GetRow.Cells("OffDayOTRate").Value) * OffMul
                '        Me.grdAutoOT.GetRow.Cells("OffDayHrs").Value += (Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value)
                '    Else
                '        Me.grdAutoOT.GetRow.Cells("RegularDayOTAmount").Value += (Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value) * (Me.grdAutoOT.GetRow.Cells("RegularDayOTRate").Value) * NormalMul
                '        Me.grdAutoOT.GetRow.Cells("RegularDayHrs").Value += (Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value)
                '    End If
                'Else
                '    Me.grdAutoOT.GetRow.Cells("RegularDayOTAmount").Value += (Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value) * (Me.grdAutoOT.GetRow.Cells("RegularDayOTRate").Value) * NormalMul
                '    Me.grdAutoOT.GetRow.Cells("RegularDayHrs").Value += (Me.grdAutoOT.GetRow.Cells(e.Column.Key.ToString).Value)
                '    'Counter += 1
                Return Counter
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.btnSave.Enabled = True
                Me.btnImportAttendance.Enabled = True
                Me.btnLoad.Enabled = True

                Exit Sub
            End If

            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            Me.btnSave.Enabled = False
            Me.btnImportAttendance.Enabled = False
            Me.btnLoad.Enabled = False

                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        Me.btnSave.Enabled = True
                ElseIf RightsDt.FormControlName = "Import Attendance" Then
                    Me.btnImportAttendance.Enabled = True
                ElseIf RightsDt.FormControlName = "Load" Then
                    Me.btnLoad.Enabled = True

                    End If
                Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class