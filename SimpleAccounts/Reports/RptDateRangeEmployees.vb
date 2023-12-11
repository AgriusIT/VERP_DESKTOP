Public Class RptDateRangeEmployees

    Public Enum ReportList
        ReportAttendance
        ReportAttendanceSummary
        EmployeeTask
        rptTaskAssign

    End Enum
    Public ReportName As ReportList

    Private Sub RptDateRangeEmployees_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            If ReportName = ReportList.ReportAttendance Then
                Me.Text = "Attandance Detail Report"
            ElseIf ReportName = ReportList.ReportAttendanceSummary Then
                Me.Text = "Attendance Summary Report"
            ElseIf ReportName = ReportList.EmployeeTask Then
                Me.Text = "Employee Wise Task"
            ElseIf ReportName = ReportList.rptTaskAssign Then
                Me.Text = "Task Assingment"
            End If
            FillEmployeeCombo()
            FillDropDown(Me.cmbCostCentre, "Select CostCenterID, Name, Code, SortOrder From tblDefCostCenter Where Active = 1 ORDER BY 2 ASC")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub CallShowReport()
        Try

            If ReportName = ReportList.ReportAttendance Then
                AddRptParam("@EmpID", Me.cmbAccounts.Value)
                AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
                AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
                AddRptParam("@CostCentre", Me.cmbCostCentre.SelectedValue)
                ShowReport("rptEmpAttendance")
                Exit Sub
                'ElseIf ReportName = ReportList.ReportAttendanceSummary Then
                '    AddRptParam("@EmpID", Me.cmbAccounts.Value)
                '    AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
                '    AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
                '    AddRptParam("@CostCentre", Me.cmbCostCentre.SelectedValue)
                '    ShowReport("rptEmpAttendanceSummary")
                '    Exit Sub
            ElseIf ReportName = ReportList.EmployeeTask Then
                AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
                AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
                AddRptParam("@EmpID", Me.cmbAccounts.Value)
                ShowReport("rptEmployeeTask")
                Exit Sub
            ElseIf ReportName = ReportList.rptTaskAssign Then
                AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
                AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
                AddRptParam("@EmpID", Me.cmbAccounts.Value)
                ShowReport("rptTaskAssign")
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGenerate.Click
        CallShowReport()
    End Sub
    Sub FillEmployeeCombo()
        'FillDropDown(Me.CmbAccounts, "Select Employee_ID, Employee_Name From tblDefEmployee Order By Employee_ID Asc", True)
        FillUltraDropDown(Me.CmbAccounts, "Select Employee_Id, Employee_Name,Father_Name,Employee_Code From EmployeesView")
        Me.CmbAccounts.Rows(0).Activate()
        If Me.CmbAccounts.DisplayLayout.Bands(0).Columns.Count > 0 Then
            Me.CmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
        End If
    End Sub
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
End Class