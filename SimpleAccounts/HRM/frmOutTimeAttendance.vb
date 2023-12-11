Public Class frmOutTimeAttendance
    Public Shared _AttendanceDate As DateTime = Date.Now
    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            Dim dt As DataTable = GetEmpData()
            dt.AcceptChanges()

            Me.grdAttendance.DataSource = dt
            Me.grdAttendance.RetrieveStructure()

            Dim dtStatus As New DataTable
            dtStatus = GetDataTable("Select Att_Status_ID, Att_Status_Name From tblDefAttendenceStatus WHERE Att_Status_Code IN('PT','SLE','SKLE','HLE','OD')")
            dtStatus.AcceptChanges()
            Me.grdAttendance.RootTable.Columns("Att_Status_ID").HasValueList = True
            Me.grdAttendance.RootTable.Columns("Att_Status_ID").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdAttendance.RootTable.Columns("Att_Status_ID").ValueList.PopulateValueList(dtStatus.DefaultView, "Att_Status_ID", "Att_Status_Name")
            Me.grdAttendance.RootTable.Columns("Att_Status_ID").Caption = "Status"
            Me.grdAttendance.RootTable.Columns("AttendanceTime").FormatString = "dd/MMM/yyyy hh:mm:ss tt"
            Me.grdAttendance.RootTable.Columns("Employee_ID").Visible = False
            Me.grdAttendance.RootTable.Columns("ShiftId").Visible = False
            Me.grdAttendance.RootTable.Columns("FlexInTime").Visible = False
            Me.grdAttendance.RootTable.Columns("FlexOutTime").Visible = False
            Me.grdAttendance.RootTable.Columns("ShiftStartTime").Visible = False
            Me.grdAttendance.RootTable.Columns("ShiftEndTime").Visible = False
            Me.grdAttendance.AutoSizeColumns()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function GetEmpData() As DataTable
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Select Employee_Id, IsNull(ShiftId,0) as ShiftId, EmployeeDeptName as [Department],EmployeeDesignationName as [Designation], Employee_Code, Employee_Name, ShiftName, 1 as Att_Status_ID,  Convert(DateTime,Convert(varchar,'" & dtpAttendanceDate.Value.Date & "' + ' ' + IsNull(ShiftEndTime,'05:30:00 PM'),102),102)  as AttendanceTime, Convert(DateTime,IsNull(FlexInTime,Convert(Datetime,Convert(varchar,'" & Me.dtpAttendanceDate.Value.ToString("yyyy-M-d") & "',102) + ' ' + '09:30:00 AM',102)),102) as FlexInTime, Convert(DateTime,IsNull(FlexOutTime,Convert(Datetime,Convert(varchar,'" & Me.dtpAttendanceDate.Value.ToString("yyyy-M-d") & "',102) + ' ' + '04:30:00 PM',102)),102) as FlexOutTime, Convert(dateTime,Convert(varchar, '" & Me.dtpAttendanceDate.Value.Date.ToString("yyyy-M-d") & "' + ' ' + IsNull(ShiftStartTime,'09:00:00 AM'),102),102) as ShiftStartTime , Convert(dateTime,Convert(varchar, '" & Me.dtpAttendanceDate.Value.Date.ToString("yyyy-M-d") & "' + ' ' + IsNull(ShiftEndTime,'05:00:00 PM'),102),102) as ShiftEndTime From EmployeesView " _
                     & " WHERE Employee_Id Not In(Select Distinct EmpId From tblAttendanceDetail WHERE AttendanceType In('Out','Short Leave') AND (Convert(Varchar,AttendanceDate,102)= Convert(DateTime,'" & Me.dtpAttendanceDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))) AND Employee_Id In(Select Distinct EmpId From tblAttendanceDetail WHERE AttendanceType In('In') AND (Convert(Varchar,AttendanceDate,102)= Convert(DateTime,'" & Me.dtpAttendanceDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)))"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmOutTimeAttendance_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.dtpAttendanceDate.Value = _AttendanceDate
            btnLoad_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If msg_Confirm("Do you want to save this record") = False Then Exit Sub
        If Me.grdAttendance.RowCount = 0 Then
            msg_Error("Record not in grid.")
            Exit Sub
        End If
        Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objtrans As OleDb.OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand


        cmd.CommandType = CommandType.Text
        cmd.Connection = objCon
        cmd.Transaction = objtrans
        cmd.CommandTimeout = 120


        Try





            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdAttendance.GetRows



                Dim dFlexInTime As DateTime = Date.MinValue
                Dim dFlexOutTime As DateTime = Date.MinValue
                Dim strAttType As String = String.Empty


                If Not IsDBNull(r.Cells("FlexInTime").Value) Then
                    Dim intDiff As Integer = Me.dtpAttendanceDate.Value.Date.Subtract(CDate(r.Cells("FlexInTime").Value)).Days
                    dFlexInTime = CDate(r.Cells("FlexInTime").Value).AddDays(intDiff)
                End If



                If Not IsDBNull(r.Cells("FlexOutTime").Value) Then
                    Dim intDiff As Integer = Me.dtpAttendanceDate.Value.Date.Subtract(CDate(r.Cells("FlexOutTime").Value)).Days
                    dFlexOutTime = CDate(r.Cells("FlexOutTime").Value).AddDays(intDiff)
                End If


                If r.Cells("Att_Status_ID").Text.ToString() = "Present" Or r.Cells("Att_Status_ID").Text.ToString() = "Outdoor Duty" Then
                    strAttType = "Out"
                Else
                    strAttType = String.Empty
                End If

                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO tblAttendanceDetail(EmpId, AttendanceDate, AttendanceType, AttendanceTime, AttendanceStatus, ShiftId, Flexibility_In_Time,Flexibility_Out_Time, Sch_In_Time, Sch_Out_Time) " _
                    & " Values(" & r.Cells("Employee_ID").Value & ", Convert(datetime,'" & Me.dtpAttendanceDate.Value.Date.ToString("yyyy-M-d") & "', 102), " & IIf(strAttType.Length > 0, "'" & strAttType & "'", "NULL") & ", " & IIf(strAttType.Length > 0, "Convert(DateTime,'" & CDate(r.Cells("AttendanceTime").Value).ToString("yyyy-M-d hh:mm:ss tt") & "',102)", "NULL") & ",'" & r.Cells("Att_Status_ID").Text.Replace("'", "''") & "', " _
                    & " " & Val(r.Cells("ShiftId").Value.ToString) & ", " & IIf(dFlexInTime = Date.MinValue, "NULL", "Convert(dateTime,'" & dFlexInTime.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", " & IIf(dFlexOutTime = Date.MinValue, "NULL", "Convert(dateTime,'" & dFlexOutTime.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", Convert(dateTime, '" & CDate(r.Cells("ShiftStartTime").Value).ToString("yyyy-M-d hh:mm:ss tt") & "',102), Convert(dateTime, '" & CDate(r.Cells("ShiftEndTime").Value).ToString("yyyy-M-d hh:mm:ss tt") & "',102))"

                cmd.ExecuteNonQuery()
            Next

            objtrans.Commit()

            ShowInformationMessage("Out time attendance has been saved.")

            GetEmpData()


        Catch ex As Exception
            objtrans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
            cmd.Dispose()

        End Try
    End Sub
End Class