Imports SBModel

Public Class frmActivityCalender
    Implements IGeneral
    Dim loaded As Boolean = False

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            If dtpFromDate.Value = dtpToDate.Value Then
                rbtnDailyView.Checked = True
            End If
            Me.Schedule1.MinDate = Me.dtpFromDate.Value
            Me.Schedule1.MaxDate = Me.dtpToDate.Value
            Me.Schedule1.Date = Me.dtpFromDate.Value
            GetAppointments()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub GetAppointments()
        Try
            Dim str As String = ""
            Dim dt As DataTable
            Dim CountApp As Integer = 0I
            str = "SELECT LeadActivity.ActivityId, LeadActivity.LeadId, LeadActivity.LeadContactId, LeadActivity.LeadOfficeId, LeadActivity.LeadActivityTypeID, LeadActivity.ActivityDate, LeadActivity.ActivityTime, LeadActivity.ResponsiblePerson_Employee_Id, LeadActivity.InsideSalesPerson_Employee_Id, LeadActivity.Manager_Employee_Id, LeadActivity.Objective, LeadProfile.LeadTitle, tblLeadOffice.OfficeTitle, LeadActivity.Address, LeadActivityType.ActivityType, tblConcernPerson.ConcernPerson, tblDefEmployee_2.Employee_Name as Manager, tblDefEmployee.Employee_Name AS Responsible, tblDefEmployee_1.Employee_Name AS InsideSales, LeadActivity.IsConfirmed FROM LeadActivity LEFT OUTER JOIN tblDefEmployee ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.Manager_Employee_Id = tblDefEmployee_1.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_2 ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee_2.Employee_ID LEFT OUTER JOIN tblConcernPerson ON LeadActivity.LeadContactId = tblConcernPerson.ConcernPersonId LEFT OUTER JOIN LeadActivityType ON LeadActivity.LeadActivityTypeID = LeadActivityType.LeadActivityTypeID LEFT OUTER JOIN tblLeadOffice ON LeadActivity.LeadOfficeId = tblLeadOffice.LeadOfficeId LEFT OUTER JOIN LeadProfile ON LeadActivity.LeadId = LeadProfile.LeadId"
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

        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT LeadActivity.ActivityId, LeadActivity.LeadId, LeadActivity.LeadContactId, LeadActivity.LeadOfficeId, LeadActivity.LeadActivityTypeID, LeadActivity.ActivityDate, LeadActivity.ActivityTime, LeadActivity.ResponsiblePerson_Employee_Id, LeadActivity.InsideSalesPerson_Employee_Id, LeadActivity.Manager_Employee_Id, LeadActivity.Objective, LeadProfile.LeadTitle, tblLeadOffice.OfficeTitle, LeadActivity.Address, LeadActivityType.ActivityType, tblConcernPerson.ConcernPerson, tblDefEmployee_2.Employee_Name as Manager, tblDefEmployee.Employee_Name AS Responsible, tblDefEmployee_1.Employee_Name AS InsideSales, LeadActivity.IsConfirmed FROM LeadActivity LEFT OUTER JOIN tblDefEmployee ON LeadActivity.InsideSalesPerson_Employee_Id = tblDefEmployee.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_1 ON LeadActivity.Manager_Employee_Id = tblDefEmployee_1.Employee_ID LEFT OUTER JOIN tblDefEmployee AS tblDefEmployee_2 ON LeadActivity.ResponsiblePerson_Employee_Id = tblDefEmployee_2.Employee_ID LEFT OUTER JOIN tblConcernPerson ON LeadActivity.LeadContactId = tblConcernPerson.ConcernPersonId LEFT OUTER JOIN LeadActivityType ON LeadActivity.LeadActivityTypeID = LeadActivityType.LeadActivityTypeID LEFT OUTER JOIN tblLeadOffice ON LeadActivity.LeadOfficeId = tblLeadOffice.LeadOfficeId LEFT OUTER JOIN LeadProfile ON LeadActivity.LeadId = LeadProfile.LeadId where " & IIf(Me.cmbResponsiblePerson.SelectedValue > 0, " LeadActivity.ResponsiblePerson_Employee_Id = " & Me.cmbResponsiblePerson.SelectedValue & "AND", "") & " " & IIf(Me.cmbInside.SelectedValue > 0, " LeadActivity.InsideSalesPerson_Employee_Id = " & Me.cmbInside.SelectedValue & "AND", "") & " " & IIf(Me.cmbManager.SelectedValue > 0, " LeadActivity.Manager_Employee_Id = " & Me.cmbManager.SelectedValue & "AND", "") & "  ActivityDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy 00:00:00") & "', 102) AND ActivityDate <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy 23:59:59") & "', 102)"
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    Me.Schedule1.Appointments.Add(row.Item(6), row.Item(14) + ":" + row.Item(11))
                Next
                If rbtnMonthlyView.Checked = True Then
                    Schedule1.Update()
                    Me.Schedule1.View = Janus.Windows.Schedule.ScheduleView.MonthView
                End If
                If rbtnDailyView.Checked = True Then
                    Schedule1.Update()
                    Me.Schedule1.View = Janus.Windows.Schedule.ScheduleView.DayView

                End If
                If rbtnWeeklyView.Checked = True Then
                    Schedule1.Update()
                    Me.Schedule1.View = Janus.Windows.Schedule.ScheduleView.WeekView

                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FillCombos()
        Try
            Dim Str As String
            Str = "select Employee_Id, Employee_Name +'-'+ Employee_Code as Employee_Name From tblDefEmployee Where Active = 1"
            FillDropDown(Me.cmbResponsiblePerson, Str, True)
            FillDropDown(Me.cmbInside, "Select Employee_ID, Employee_Name From tblDefEmployee", True)
            FillDropDown(Me.cmbManager, "Select Employee_ID, Employee_Name  From tblDefEmployee", True)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmActivityCalender_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            Me.cmbPeriod.Text = "Current Month"

            FillCombos()
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            loaded = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnShow.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.btnShow.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                Me.btnShow.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Show" Then
                        Me.btnShow.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos1(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub cmbPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriod.SelectedIndexChanged
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


    Private Sub rbtnDailyView_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnDailyView.CheckedChanged
        Try
            Me.Schedule1.View = Janus.Windows.Schedule.ScheduleView.DayView
            GetAppointments()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub rbtnWeeklyView_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnWeeklyView.CheckedChanged
        Try
            Me.Schedule1.View = Janus.Windows.Schedule.ScheduleView.WeekView
            GetAppointments()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub rbtnMonthlyView_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnMonthlyView.CheckedChanged
        Try
            If loaded = True Then
                If dtpFromDate.Value = dtpToDate.Value Then
                    ShowErrorMessage("The Difference between two days must be one month")
                Else
                    Me.Schedule1.View = Janus.Windows.Schedule.ScheduleView.MonthView
                    GetAppointments()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class