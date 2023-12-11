Imports SBUtility.Utility
Imports SBDal
Imports SBModel
Imports System
Imports System.Data

Public Class frmAttendanceImport
    Dim Access_Con As OleDb.OleDbConnection

    Private Sub btnAlternativeDBPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlternativeDBPath.Click
        Try
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "Microsoft Access|*.*mdb"
            If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtDBPath.Text = OpenFileDialog1.FileName
                Dim cmd As New OleDb.OleDbCommand
                If Con.State = ConnectionState.Closed Then Con.Open()
                cmd.Connection = Con
                cmd.CommandText = "Update ConfigValuesTable Set Config_Value='" & Me.txtDBPath.Text & "' WHERE Config_Type='AlternateAttendanceDBPath'"
                cmd.ExecuteNonQuery()
                cmd = Nothing
                ConfigValuesDataTable = GetConfigValuesdt()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub
    Private Sub frmAttendanceImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ProgressBar1.Visible = False
        Try
            Me.txtDBPath.Text = getConfigValueByType("AlternateAttendanceDBPath").ToString
            Dim dt As New DataTable
            dt = GetDataTable("select Max(AttendanceDate) as AttendanceDate From tblAttendanceDetail")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If Not IsDBNull(dt.Rows(0).Item(0)) Then
                    Me.dtpDateFrom.Value = dt.Rows(0).Item(0)
                Else
                    Me.dtpDateFrom.Value = Now
                End If
            End If
            Me.dtpDateTo.Value = Now
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    'Private Sub frmAttendanceImport_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    Try
    '        e.Graphics.FillRectangle(Brushes.Honeydew, 0, 0, Me.Width, Me.Height - 75)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Me.ProgressBar1.Visible = True
        Application.DoEvents()
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Try
            Access_Con = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Me.txtDBPath.Text & "")
            If Access_Con.State = ConnectionState.Closed Then Access_Con.Open()
            Dim Acc_dt As New DataTable
            Dim Acc_da As New OleDb.OleDbDataAdapter
            Dim Acc_cm As New OleDb.OleDbCommand("Select Userid, CheckTime, CheckType From Checkinout WHERE (((Checkinout.CheckTime) >=#" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "# And (Checkinout.CheckTime) <=#" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "#))", Access_Con)
            Acc_da.SelectCommand = Acc_cm
            Acc_da.Fill(Acc_dt)
            Access_Con.Close()
            Acc_cm = Nothing
            Acc_da = Nothing


            Dim cm As New OleDb.OleDbCommand
            cm.Connection = Con
            cm.Transaction = trans

            cm.CommandText = ""
            cm.CommandText = "Truncate Table tempCheckInOut"
            cm.ExecuteNonQuery()

            If Acc_dt IsNot Nothing Then
                For Each r As DataRow In Acc_dt.Rows
                    Try
                        'cm = New OleDb.OleDbCommand
                        'cm.Parameters.Clear()
                        cm.CommandText = ""
                        'cm.CommandType = CommandType.StoredProcedure
                        'cm.CommandText = "SPInserttempCheckInOut"
                        'cm.Parameters.Add(New OleDb.OleDbParameter("@AlterEmpNo", Val(r.Item("Userid").ToString)))
                        'cm.Parameters.Add(New OleDb.OleDbParameter("@CheckTime", r.Item("CheckTime")))
                        'cm.Parameters.Add(New OleDb.OleDbParameter("@CheckType", r.Item("CheckType").ToString.Replace("'", "''")))
                        cm.CommandText = "INSERT INTO tempCheckInOut(AlterEmpNo,CheckTime,CheckType) Values(" & Val(r.Item("Userid").ToString) & ",'" & r.Item("CheckTime") & "', '" & r.Item("CheckType").ToString.Replace("'", "''") & "')"
                        cm.ExecuteNonQuery()
                    Catch ex As Exception
                        ShowErrorMessage(ex.Message)
                    End Try
                Next

                cm.CommandText = ""
                cm.CommandText = "Delete From tblAttendanceDetail WHERE (Convert(Varchar, AttendanceDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND Isnull(Auto,0)=1"
                cm.ExecuteNonQuery()


                cm.CommandText = ""
                'Task No 2593 Update The Insert Query Of Table Attendace Detail
                'cm.CommandText = "INSERT INTO tblAttendanceDetail(EmpId, AttendanceDate, AttendanceType, AttendanceTime, AttendanceStatus, ShiftId, [Auto]) SELECT a.Employee_ID, CONVERT(DateTime, CONVERT(Varchar, b.CheckTime, 102), 102) AS AttendanceDate, Case When b.CheckType='I' Then 'In' ELSE 'Out' End AS AttendanceType,b.CheckTime AS AttendanceTime, CONVERT(Varchar, 'Present') AS AttendanceStatus, Isnull(c.ShiftId,0) as ShiftId, CONVERT(bit, 1) AS [Auto],SF.Sch_In_Time FROM dbo.tblDefEmployee AS a INNER JOIN dbo.tempCheckInOut AS b ON a.AlternateEmpNo = b.AlterEmpNo LEFT OUTER JOIN dbo.ShiftScheduleTable AS c ON a.ShiftGroupId = c.ShiftGroupId LEFT OUTER JOIN ShiftTable SF on SF.ShiftId = c.ShiftId WHERE (Convert(Varchar, CheckTime, 102) BETWEEN Convert(DateTime, '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "
                cm.CommandText = "INSERT INTO tblAttendanceDetail(EmpId, AttendanceDate, AttendanceType, AttendanceTime, AttendanceStatus, ShiftId, [Auto], Flexibility_In_Time, Flexibility_Out_Time, Sch_In_Time, Sch_Out_Time) SELECT a.Employee_ID, CONVERT(DateTime, CONVERT(Varchar, b.CheckTime, 102), 102) AS AttendanceDate, Case When b.CheckType='I' Then 'In' ELSE 'Out' End AS AttendanceType,b.CheckTime AS AttendanceTime, CONVERT(Varchar, 'Present') AS AttendanceStatus, Isnull(c.ShiftId,0) as ShiftId, CONVERT(bit, 1) AS [Auto], Convert(Varchar, b.CheckTime,102) + ' ' + SF.FlexInTime as FlexInTime, Convert(Varchar, b.CheckTime,102) + ' ' + SF.FlexOutTime as FlexOutTime,  Convert(Varchar, b.CheckTime,102) + ' ' + SF.ShiftStartTime as ShiftStartTime, Convert(Varchar, b.CheckTime,102) + ' ' + SF.ShiftEndTime as ShiftEndTime FROM dbo.tblDefEmployee AS a INNER JOIN dbo.tempCheckInOut AS b ON a.AlternateEmpNo = b.AlterEmpNo LEFT OUTER JOIN dbo.ShiftScheduleTable AS c ON a.ShiftGroupId = c.ShiftGroupId LEFT OUTER JOIN ShiftTable SF on SF.ShiftId = c.ShiftId WHERE (Convert(Varchar, CheckTime, 102) BETWEEN Convert(DateTime, '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "
                cm.ExecuteNonQuery()
                'End Task

                trans.Commit()

                cm = Nothing
                Me.ProgressBar1.Visible = False
                msg_Information("Process Completed Successfully.")
            End If
            
        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub
End Class