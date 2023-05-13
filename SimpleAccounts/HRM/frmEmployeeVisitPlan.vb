'Ali Faisal : TFS1533 : Add new form to show Visit Plans
Public Class frmEmployeeVisitPlan
    Public Shared PlanDate As DateTime
    Public Shared PlanId As Integer
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Show dialog box to save, update and delete the visit plans
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Schedule1_Click(sender As Object, e As EventArgs) Handles Schedule1.Click
        Try
            PlanId = 0
            PlanDate = Me.Schedule1.GetDateAt()
            Schedule1_AppointmentDoubleClick(Nothing, Nothing)
            frmMain.LoadControl("frmEmployeeVisitPlanEntry")
            GetAppointments()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnShow.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnShow.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    btnShow.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Load planner view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmEmployeeVisitPlan_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            Me.Schedule1.MinDate = Me.dtpFromDate.Value
            Me.Schedule1.MaxDate = Me.dtpToDate.Value
            Me.Schedule1.Date = Me.dtpFromDate.Value
            ApplySecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Get all saved visits and show them as appointment in schedule
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetAppointments()
        'Ali Faisal : TFS1533 : Remove all the selected visits as appointment in schedule
        Try
            Dim str As String = ""
            Dim dt As DataTable
            Dim CountApp As Integer = 0I
            str = "SELECT EmpPlan.PlanId, EmpPlan.DocNo, EmpPlan.DocDate, EmpPlan.EmployeeId, Emp.Employee_Name AS [Emp Name], EmpPlan.TimeFrom, EmpPlan.TimeTo, EmpPlan.Remarks FROM tblEmployeeVisitPlan AS EmpPlan LEFT OUTER JOIN tblDefEmployee AS Emp ON EmpPlan.EmployeeId = Emp.Employee_ID " 'WHERE EmpPlan.DocDate = '" & FromDate.ToString("yyyy-MM-dd") & "'"
            dt = GetDataTable(str)
            For Each row As DataRow In dt.Rows
                CountApp = 0I
                If Me.Schedule1.Appointments.Count > 0 Then
                    If CountApp < Me.Schedule1.Appointments.Count Then
                        Me.Schedule1.Appointments.RemoveAt(CountApp)
                    End If
                    CountApp += 1
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
        'Ali Faisal : TFS1533 : Show all saved visits as appointment in schedule
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT EmpPlan.PlanId, EmpPlan.DocNo, EmpPlan.DocDate, EmpPlan.EmployeeId, Emp.Employee_Name AS [Emp Name], EmpPlan.TimeFrom, EmpPlan.TimeTo, EmpPlan.Remarks FROM tblEmployeeVisitPlan AS EmpPlan LEFT OUTER JOIN tblDefEmployee AS Emp ON EmpPlan.EmployeeId = Emp.Employee_ID WHERE EmpPlan.DocDate BETWEEN '" & Me.dtpFromDate.Value & "' AND '" & Me.dtpToDate.Value & "' "
            dt = GetDataTable(str)
            For Each row As DataRow In dt.Rows
                Me.Schedule1.Appointments.Add(row.Item(5), row.Item(6), row.Item(4) + " ~ " + row.Item(7).ToString)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Change date range according to period
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFromDate.Value = Date.Today
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-1)
            Me.dtpToDate.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpToDate.Value = Date.Today
        End If
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Change view to Month view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbtnMonthlyView_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnMonthlyView.CheckedChanged
        Try
            Me.Schedule1.View = Janus.Windows.Schedule.ScheduleView.MonthView
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Change view to Week view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbtnWeeklyView_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnWeeklyView.CheckedChanged
        Try
            Me.Schedule1.View = Janus.Windows.Schedule.ScheduleView.WeekView
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Change view to Daily view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbtnDailyView_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnDailyView.CheckedChanged
        Try
            Me.Schedule1.View = Janus.Windows.Schedule.ScheduleView.DayView
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Show button to get visits
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Me.Schedule1.MinDate = Me.dtpFromDate.Value
            Me.Schedule1.MaxDate = Me.dtpToDate.Value
            Me.Schedule1.Date = Me.dtpFromDate.Value
            GetAppointments()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Close form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Schedule1_AppointmentDoubleClick(sender As Object, e As Janus.Windows.Schedule.AppointmentEventArgs) Handles Schedule1.AppointmentDoubleClick
        Try
            Dim Appointment As String = ""
            Dim Emp As String = ""
            Dim Remarks As String = ""
            If Not Me.Schedule1.CurrentAppointment Is Nothing Then
                Appointment = Me.Schedule1.CurrentAppointment.Text
            End If
            Dim separator() As String = {" ~ "}
            Dim words() As String = Appointment.Split(separator, StringSplitOptions.RemoveEmptyEntries)
            If words.Length > 0 Then
                Emp = words(0).ToString
                Remarks = words(1).ToString
            End If
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT EmpPlan.PlanId, EmpPlan.DocNo, EmpPlan.DocDate, EmpPlan.EmployeeId, Emp.Employee_Name AS [Emp Name], EmpPlan.TimeFrom, EmpPlan.TimeTo, EmpPlan.Remarks FROM tblEmployeeVisitPlan AS EmpPlan LEFT OUTER JOIN tblDefEmployee AS Emp ON EmpPlan.EmployeeId = Emp.Employee_ID WHERE EmpPlan.DocDate BETWEEN '" & PlanDate & "' AND '" & PlanDate & "' AND Emp.Employee_Name = '" & Emp & "' AND EmpPlan.Remarks = '" & Remarks & "' "
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                PlanId = dt.Rows(0).Item(0)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class