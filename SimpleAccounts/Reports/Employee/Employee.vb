Public Class Employee
    Private Sub UiButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton2.Click
        ApplyStyleSheet(rptSalesManTarget)
        rptSalesManTarget.ShowDialog()
    End Sub
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        ShowReport("rptEmployees_List")
    End Sub
    Private Sub UiButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton4.Click
        RptDateRangeEmployees.ReportName = RptDateRangeEmployees.ReportList.ReportAttendance
        ApplyStyleSheet(RptDateRangeEmployees)
        RptDateRangeEmployees.ShowDialog()

    End Sub
    Private Sub UiButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton5.Click
        RptDateRangeEmployees.ReportName = RptDateRangeEmployees.ReportList.ReportAttendanceSummary
        ApplyStyleSheet(RptDateRangeEmployees)
        RptDateRangeEmployees.ShowDialog()
    End Sub

    Private Sub UiButton3_Click(sender As Object, e As EventArgs) Handles UiButton3.Click
        frmMain.LoadControl("frmEmployeeStatusList")
    End Sub

    Private Sub UiButton6_Click(sender As Object, e As EventArgs) Handles UiButton6.Click
        frmMain.LoadControl("LateComingEmployee")
    End Sub

    Private Sub UiButton7_Click(sender As Object, e As EventArgs) Handles UiButton7.Click
        frmMain.LoadControl("OverTimeEmployee")
    End Sub

    Private Sub UiButton8_Click(sender As Object, e As EventArgs) Handles UiButton8.Click
        frmMain.LoadControl("LateInTimeSummary")
    End Sub

    Private Sub UiButton9_Click(sender As Object, e As EventArgs) Handles UiButton9.Click
        frmMain.LoadControl("LateArrivalDays")
    End Sub

    Private Sub UiButton10_Click(sender As Object, e As EventArgs) Handles UiButton10.Click
        frmMain.LoadControl("EmpSalaryRpt")
    End Sub

    Private Sub UiButton11_Click(sender As Object, e As EventArgs) Handles UiButton11.Click
        frmMain.LoadControl("DailySalarySheet")
    End Sub

    Private Sub UiButton12_Click(sender As Object, e As EventArgs) Handles UiButton12.Click
        frmMain.LoadControl("DailySalarySheetSummary")
    End Sub

    Private Sub UiButton13_Click(sender As Object, e As EventArgs) Handles UiButton13.Click
        frmMain.LoadControl("rptEmpSalariesDetail")
    End Sub

    Private Sub UiButton14_Click(sender As Object, e As EventArgs) Handles UiButton14.Click
        frmMain.LoadControl("frmRptEmpSalarySheetDetail")
    End Sub

    Private Sub UiButton15_Click(sender As Object, e As EventArgs) Handles UiButton15.Click
        frmMain.LoadControl("EmployeeAttendanceEmailAlert")
    End Sub

    Private Sub UiButton16_Click(sender As Object, e As EventArgs) Handles UiButton16.Click
        frmMain.LoadControl("EmployeeSiteVisitCharges")
    End Sub

    Private Sub UiButton17_Click(sender As Object, e As EventArgs) Handles UiButton17.Click
        frmMain.LoadControl("frmGrdRptAttendanceRegister")
    End Sub

    Private Sub UiButton18_Click(sender As Object, e As EventArgs) Handles UiButton18.Click
        frmMain.LoadControl("EmployeeOverTimeReport")
    End Sub

    Private Sub UiButton22_Click(sender As Object, e As EventArgs) Handles UiButton22.Click
        frmMain.LoadControl("rptEmpAttendanceDetail")
    End Sub

    Private Sub UiButton19_Click(sender As Object, e As EventArgs) Handles UiButton19.Click
        frmMain.LoadControl("rptDailyAttendance")
    End Sub

    Private Sub UiButton20_Click(sender As Object, e As EventArgs) Handles UiButton20.Click
        frmMain.LoadControl("rptAttendanceSummary")
    End Sub

    Private Sub UiButton21_Click(sender As Object, e As EventArgs) Handles UiButton21.Click
        frmMain.LoadControl("AttendanceSummary")
    End Sub

    Private Sub UiButton23_Click(sender As Object, e As EventArgs) Handles UiButton23.Click
        frmMain.LoadControl("frmGrdRptEmployeeMonthlyAttendance")
    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub

    Private Sub Employee_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class