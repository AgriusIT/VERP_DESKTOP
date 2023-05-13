'31-Jul-2015 Task#31072015 Ahmad Sharif: Design screen for Employee Attendance alert schedule
'01-Aug-2015 Task#31072015 Ahmad Sharif: Load Attendace, Save Attendance report of email for email



Imports System.Data.OleDb
Imports System.Net.Mail
Imports SBModel
Imports SBDal

Public Class frmEmpAttendanceEmailAlertSchedule
    Public Enum enmEmail
        Employee_Id
        Employee_Name
        Employee_Code
        Designation
        Department
        Email
        Mobile
        Count
    End Enum
    Dim serverDate As DateTime
    Public GetEmailConfig As List(Of EmailSeeting)

    Private Sub ResetControls()
        Try
            Me.cmbType.SelectedIndex = 0
            Me.cmbMonth.Text = GetMonthName(Convert.ToInt32(serverDate.Month))
            Me.txtYear.Value = Convert.ToString(serverDate.Year)
            Me.cmbMonth.Enabled = True
            Me.grdEmp.DataSource = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub frmEmpAttendanceEmailAlertSchedule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            serverDate = GettingServerDate()
            ResetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        Try
            If Me.cmbType.SelectedIndex = 0 Then

                Me.dtpFrom.Value = serverDate.Date
                Me.dtpTo.Value = serverDate.Date
                Me.cmbMonth.Enabled = True

            ElseIf Me.cmbType.SelectedIndex = 1 Then

                Me.cmbMonth.Enabled = True
                Dim dt As DateTime
                dt = serverDate
                Dim weekNumber As Integer = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dt.DayOfYear / 7)))

                'Dim week As Integer = Calendar.GetWeekOfYear(serverDate.Today, CalendarWeekRule.FirstFourDayWeek, dayOfWeek.Monday)

                Dim startDateOfWeek As DateTime
                startDateOfWeek = GetWeekStartDate(weekNumber, serverDate.Year)

                Dim endDateOfWeek As DateTime
                endDateOfWeek = startDateOfWeek.AddDays(6).Date

                Me.dtpFrom.Value = startDateOfWeek.Date
                Me.dtpTo.Value = endDateOfWeek.Date

            ElseIf Me.cmbType.SelectedIndex = 2 Then
                Me.cmbMonth.Enabled = True
                Me.cmbMonth.Text = GetMonthName(Convert.ToInt32(serverDate.Month))

                Dim firstDate As New DateTime(serverDate.Year, serverDate.Month, 1)
                Dim totalDaysInMonth As Integer = CDate(serverDate).DaysInMonth(serverDate.Year, serverDate.Month)
                Dim lastDate As New DateTime(serverDate.Year, serverDate.Month, totalDaysInMonth)

                Me.dtpFrom.Value = firstDate.Date
                Me.dtpTo.Value = lastDate.Date

            Else
                Dim startDate As New DateTime(serverDate.Year, 1, 1)
                Dim endDate As New DateTime(serverDate.Year, 12, 31)

                Me.cmbMonth.Enabled = False
                Me.dtpFrom.Value = startDate
                Me.dtpTo.Value = endDate

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmHolidySetup)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        If Me.btnPrint.Text = "&Print" Then btnPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Function GetWeekStartDate(ByVal weekNumber As Integer, ByVal year As Integer) As DateTime
        Try

            Dim startDate As New DateTime(year, 1, 1)
            Dim weekDate As DateTime = DateAdd(DateInterval.WeekOfYear, weekNumber - 1, startDate)
            Return DateAdd(DateInterval.Day, (-weekDate.DayOfWeek) + 1, weekDate)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            Dim strSQL As String = String.Empty
            Dim dt As New DataTable
            strSQL = "SP_AttendanceSummamryForEmailAlert '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpTo.Value.ToString("yyyy-M-d 00:00:00") & "'"

            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            Me.grdEmp.DataSource = dt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.grdEmp.RowCount = 0 Then
                ShowErrorMessage("No record found in grid")
                Exit Sub
            End If

            If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                If Save() = True Then
                    ShowInformationMessage("Attendance report successfully saved.")
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function Save() As Boolean
        Dim trans As OleDbTransaction
        If Con.State = ConnectionState.Closed Then Con.Open()
        trans = Con.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            Dim strSQL As String = String.Empty
            Dim messageBoday As String = String.Empty

            cmd.CommandText = "insert into tblMasterProcessAttedanceAlert(Type,DateFrom,DateTo) values ('" & Me.cmbType.Text.ToString & "','" & Me.dtpFrom.Value.ToString("yyyy-MM-dd") & "','" & Me.dtpTo.Value.ToString("yyyy-MM-dd") & "')Select @@identity"
            Dim ProcessId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            For i As Integer = 0 To Me.grdEmp.RowCount - 1
                messageBoday = "Dear " & Me.grdEmp.GetRows(i).Cells("Employee_Name").Value.ToString & "," & Environment.NewLine & Environment.NewLine & "Please Check your " & Me.cmbType.Text.ToString & " attendance report mentioned below:" & Environment.NewLine & Environment.NewLine & "Present = " & Me.grdEmp.GetRows(i).Cells("PresentDayCount").Value.ToString & ", Leaves = " & Me.grdEmp.GetRows(i).Cells("LeaveDayCount").Value.ToString & ", Absents = " & Me.grdEmp.GetRows(i).Cells("AbsentDayCount").Value.ToString & ", Sick = " & Me.grdEmp.GetRows(i).Cells("SickDayCount").Value.ToString & ", Half Leaves = " & Me.grdEmp.GetRows(i).Cells("HLDayCount").Value.ToString & ", Short = " & Me.grdEmp.GetRows(i).Cells("SLDayCount").Value.ToString & Environment.NewLine & Environment.NewLine & "Kindly contact to accounts office if you have any queries or any suggestions in this regard." & Environment.NewLine & Environment.NewLine & "Automated by : SIRIUS Pvt. Ltd."
                cmd.CommandText = String.Empty
                cmd.CommandText = "insert into tblAttendanceAlert(EmpId,Email,Message,Status,Type,ProcessId) values (" & Me.grdEmp.GetRows(i).Cells("Employee_Id").Value.ToString & ",'" & Me.grdEmp.GetRows(i).Cells("Email").Value.ToString & "','" & messageBoday.ToString & "','Pending','" & Me.cmbType.Text.Replace("'", "''").ToString & "'," & ProcessId & ")"
                cmd.ExecuteNonQuery()
            Next

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    'Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim trans As OleDbTransaction
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    trans = Con.BeginTransaction()

    '    Try
    '        If My.Computer.Network.IsAvailable Then
    '            Dim strSQL As String = String.Empty
    '            Dim cmd As New OleDbCommand
    '            cmd.Connection = Con
    '            cmd.Transaction = trans
    '            cmd.CommandTimeout = 120
    '            cmd.CommandType = CommandType.Text

    '            strSQL = "select * from tblAttendanceAlert where Status='Pending'"
    '            cmd.CommandText = strSQL

    '            Dim dt As New DataTable
    '            Dim adapt As New OleDbDataAdapter(cmd)

    '            adapt.Fill(dt)
    '            dt.AcceptChanges()

    '            GetEmailConfig = New EmailSettingDAL().GetEmailSetting(String.Empty, getConfigValueByType("DefaultEmailId").ToString)
    '            If GetEmailConfig Is Nothing Then Exit Sub


    '            If Not dt Is Nothing Then
    '                If dt.Rows.Count > 0 Then
    '                    For Each row As DataRow In dt.Rows

    '                        Dim sendEmail As New MailMessage()

    '                        'sendEmail.To.Add(row("Email").ToString)
    '                        sendEmail.To.Add("ahmadsharif0017@gmail.com")
    '                        sendEmail.Subject = "Attendance Report"
    '                        sendEmail.Body = row("Message").ToString
    '                        sendEmail.From = New MailAddress(GetEmailConfig(0).Email)

    '                        sendEmail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
    '                        Dim Client As New SmtpClient(GetEmailConfig(0).SmtpServer)
    '                        Client.Port = GetEmailConfig(0).port

    '                        Dim EmailPwd As String = Decrypt(GetEmailConfig(0).EmailPassword)
    '                        Client.Credentials = New Net.NetworkCredential(GetEmailConfig(0).Email, EmailPwd)
    '                        Client.EnableSsl = IIf(GetEmailConfig(0).ssl = True, True, False)
    '                        Client.DeliveryMethod = SmtpDeliveryMethod.Network
    '                        Client.Send(sendEmail)

    '                        strSQL = String.Empty
    '                        strSQL = "Update tblAttendanceAlert set Status='Sent' where ID=" & Val(row("ID").ToString)
    '                        cmd.CommandText = strSQL
    '                        cmd.ExecuteNonQuery()
    '                    Next
    '                End If
    '            End If
    '        End If

    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        ShowErrorMessage(ex.Message)
    '    Finally
    '        Con.Close()
    '    End Try
    'End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim trans As OleDbTransaction
        If Con.State = ConnectionState.Closed Then Con.Open()
        trans = Con.BeginTransaction()

        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

            Dim strSQL As String = String.Empty
            Dim cmd As New OleDbCommand
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            strSQL = "Delete from tblAttendanceAlert where Status='Sent'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub

End Class