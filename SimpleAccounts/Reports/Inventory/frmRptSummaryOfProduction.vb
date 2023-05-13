﻿Imports SBModel

''TASK TFS1775 Muhammad Ameen on 16-11-2017. New form Production Summary to apply rights.
Public Class frmRptSummaryOfProduction

    Private Sub frmRptSummaryOfProduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillUltraDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name, Father_Name, Employee_Code From EmployeesView")
            Me.cmbEmployee.Rows(0).Activate()
            If Me.cmbEmployee.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbEmployee.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
            Me.cmbPeriod.Text = "Current Month"
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.DateTimePicker1.Value = Date.Today
                Me.DateTimePicker2.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.DateTimePicker1.Value = Date.Today.AddDays(-1)
                Me.DateTimePicker2.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.DateTimePicker1.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.DateTimePicker2.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.DateTimePicker1.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.DateTimePicker2.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.DateTimePicker1.Value = New Date(Date.Now.Year, 1, 1)
                Me.DateTimePicker2.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Try
            GetCrystalReportRights()
            AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
            AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
            ShowReport("RptProduction", "" & IIf(Me.cmbEmployee.Value > 0, " {ProductionSummary;1.EmployeeID} = " & Me.cmbEmployee.Value & "", "") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            GetCrystalReportRights()
            AddRptParam("@FromDate", Me.DateTimePicker1.Value.ToString("yyyy-MM-d 00:00:00"))
            AddRptParam("@ToDate", Me.DateTimePicker2.Value.ToString("yyyy-MM-d 23:59:59"))
            ShowReport("RptProduction", "" & IIf(Me.cmbEmployee.Value > 0, " {ProductionSummary;1.EmployeeID} = " & Me.cmbEmployee.Value & "", "") & "", , , True)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Button1.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Button1.Enabled = False
                    Exit Sub
                End If
            Else
                Button1.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                        ''TASK TFS1384 replaced Report Print and Report Export with Report Print and Report Export on 07-09-2017
                    ElseIf RightsDt.FormControlName = "Report Print" Then
                        Button1.Enabled = True
                        'ElseIf RightsDt.FormControlName = "Report Export" Then
                        '    Me.CtrlGrdBar1.mGridExport.Enabled = True
                        ''End TASK TFS1384
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class