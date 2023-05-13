Imports System.Data.SqlClient
Imports SBDal

Public Class frmAutoHalfAbsentOrLeave

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        GetOverTime()
    End Sub
    Public Function Save()
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            For Each row As Janus.Windows.GridEX.GridEXRow In grdAutoOT.GetCheckedRows
                '                Emp.Employee_Name AS Name, Dept.DEPTNAME As Department, ST.OverTime_StartTime As OverTimeStart,  
                'AD.AttendanceTime As OutTime, DATEDIFF(HH, Convert(DateTime, ST.ShiftEndTime, 102),  AD.AttendanceTime) As [Hours],
                'EOTS.OverTime_Rate_HR As OverTimeRate
                Dim strQuery As String = String.Empty
                strQuery = "INSERT INTO tblEmployeeOverTimeSchedule(EmployeeId, OverTime_Rate_HR, Active, AttendanceDate, AutoOverTime, Hours) Values(" & Val(row.Cells("Employee_ID").Value.ToString) & ", " & Val(row.Cells("OverTimeRate").Value.ToString) & ", " & row.Cells("Active").Value & ", N'" & row.Cells("AttendanceDate").Value & "', " & 1 & ", " & Val(row.Cells("Hours").Value.ToString) & ") Where Day(AttendanceDate) <> Day(" & row.Cells("AttendanceDate").Value & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

                trans.Commit()
            Next


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Private Sub GetOverTime()

        Dim str As String = String.Empty
        Dim dt As New DataTable
        str = "SP_GetAutoEmployeesOverTime '" & Me.dtpMonth.Value.ToString("yyyy-M-d 00:00:00") & "'"
        dt = GetDataTable(str)
        Me.grdAutoOT.DataSource = dt
        Me.grdAutoOT.RetrieveStructure()
        Me.grdAutoOT.RootTable.Columns.Add("Column1")
        Me.grdAutoOT.RootTable.Columns("Column1").UseHeaderSelector = True
        Me.grdAutoOT.RootTable.Columns("Column1").ActAsSelector = True
        Me.grdAutoOT.AutoSizeColumns()

        '        AttendanceId	int	Unchecked
        'EmpId	int	Checked
        'AttendanceDate	datetime	Checked
        'AttendanceType	varchar(50)	Checked
        'AttendanceTime	datetime	Checked
        'AttendanceStatus	varchar(50)	Checked
        'ShiftId	int	Checked
        'Auto	bit	Checked
        'Flexibility_In_Time	datetime	Checked
        'Flexibility_Out_Time	datetime	Checked
        'Sch_In_Time	datetime	Checked
        'Sch_Out_Time	datetime	Checked
        'PolicyID	int	Checked
        '        Unchecked()
    End Sub

    Private Sub frmEmployeeAutoOverTime_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.dtpMonth.Format = DateTimePickerFormat.Custom
            Me.dtpMonth.CustomFormat = "MMMM yyyy"


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Save()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class