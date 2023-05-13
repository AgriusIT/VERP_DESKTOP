''19-Feb-2014 Task:M19  Imran Ali   Leave + Attendance Based Salary Calc

Public Class frmEmpAttendanceSumm
    Public _EmpID As Integer
    Public _GrossSalary As Double
    Public _BasicSalary As Double
    Private _DateFrom As DateTime
    Private _DateTo As DateTime
    Private _SelectYear As Integer
    Private _SelectMonth As String
    Private _IsOpenForm As Boolean = False
    Private _WorkingDays As Integer
    Private _PresentDays As Integer = 0I


    Private Sub frmEmpAttendanceSumm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If Not getConfigValueByType("Working_Days").ToString = "Error" Then
                _WorkingDays = getConfigValueByType("Working_Days")
            Else
                _WorkingDays = 0
            End If

            Me.cmbYear.ValueMember = "Year"
            Me.cmbYear.DisplayMember = "Year"
            Me.cmbYear.DataSource = GetYears()
            Me.cmbMonth.ValueMember = "Month"
            Me.cmbMonth.DisplayMember = "Month_Name"
            Me.cmbMonth.DataSource = GetMonths()
            Me.cmbMonth.SelectedValue = Date.Now.Month
            Me.cmbYear.SelectedValue = Date.Now.Year
            Me.txtWorkingDays.Text = _WorkingDays
            Me.txtBasicSalary.Text = _BasicSalary
            _IsOpenForm = True
            Me.OptMonthly.Checked = True
            grpDateRange.Enabled = False
            Me.grpMonth.Enabled = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetEmpAttendanceSummary(ByVal EmpId As Integer)
        Try

            Dim strQuery As String = "SP_EmpAttendanceSummary '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "',  " & EmpId & ""
            Dim dt As New DataTable
            dt = GetDataTable(strQuery)
            Me.grdEmpAttendance.DataSource = dt
            Me.grdEmpAttendance.RetrieveStructure()
            Me.grdEmpAttendance.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdEmpAttendance.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdEmpAttendance.RootTable.Columns("Days").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmpAttendance.RootTable.Columns("Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmpAttendance.RootTable.Columns("Days").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grdEmpAttendance.RootTable.Columns("EmpId").Visible = False
            Me.grdEmpAttendance.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Me.OptMonthly.Checked = True Then
                cmbMonth_SelectedIndexChanged(Nothing, Nothing)
            Else
                _DateFrom = Me.dtpFromDate.Value
                _DateTo = Me.dtpToDate.Value
            End If
            GetEmpAttendanceSummary(_EmpID)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMonth.SelectedIndexChanged
        Try

            If _IsOpenForm = False Then Exit Sub

            _SelectYear = Me.cmbYear.Text
            _SelectMonth = Me.cmbMonth.Text

            Select Case Me.cmbMonth.Text
                Case "January"
                    _DateFrom = New Date(_SelectYear, 1, 1)
                    _DateTo = New Date(_SelectYear, 1, 31)
                Case "February"
                    _DateFrom = New Date(_SelectYear, 2, 1)
                    If Date.IsLeapYear(Me.cmbYear.Text) = True Then
                        _DateTo = New Date(_SelectYear, 2, 29)
                    Else
                        _DateTo = New Date(_SelectYear, 2, 28)
                    End If
                Case "March"
                    _DateFrom = New Date(_SelectYear, 3, 1)
                    _DateTo = New Date(_SelectYear, 3, 31)
                Case "April"
                    _DateFrom = New Date(_SelectYear, 4, 1)
                    _DateTo = New Date(_SelectYear, 4, 30)
                Case "May"
                    _DateFrom = New Date(_SelectYear, 5, 1)
                    _DateTo = New Date(_SelectYear, 5, 31)
                Case "June"
                    _DateFrom = New Date(_SelectYear, 6, 1)
                    _DateTo = New Date(_SelectYear, 6, 30)
                Case "July"
                    _DateFrom = New Date(_SelectYear, 7, 1)
                    _DateTo = New Date(_SelectYear, 7, 31)
                Case "August"
                    _DateFrom = New Date(_SelectYear, 8, 1)
                    _DateTo = New Date(_SelectYear, 8, 31)
                Case "September"
                    _DateFrom = New Date(_SelectYear, 9, 1)
                    _DateTo = New Date(_SelectYear, 9, 30)
                Case "October"
                    _DateFrom = New Date(_SelectYear, 10, 1)
                    _DateTo = New Date(_SelectYear, 10, 31)
                Case "November"
                    _DateFrom = New Date(_SelectYear, 11, 1)
                    _DateTo = New Date(_SelectYear, 11, 30)
                Case "December"
                    _DateFrom = New Date(_SelectYear, 12, 1)
                    _DateTo = New Date(_SelectYear, 12, 31)
                Case "Year"
                    _DateFrom = New Date(_SelectYear, 12, 1)
                    _DateTo = New Date(_SelectYear, 12, 31)
            End Select

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdEmpAttendance_LoadingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdEmpAttendance.LoadingRow
        Try
            'If Me.grdEmpAttendance.RowCount = 0 Then Exit Sub
            'If e.Row.Cells("AttendanceStatus").Text = "Present" Then
            '    _PresentDays = e.Row.Cells("Days").Value
            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnGenerateSalary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateSalary.Click
        Try
            Me.grdEmpAttendance.UpdateData()
            _PresentDays = 0I
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdEmpAttendance.GetRows
                'Before against Task:M19 
                'If r.Cells("AttendanceStatus").Value = "Present" Then
                'Task:M19 Change Comparison
                If r.Cells("AttendanceStatus").Value = "Present" Or r.Cells("AttendanceStatus").Value = "Leave" Then
                    '_PresentDays = r.Cells("Days").Value
                    _PresentDays += r.Cells("Days").Value 'Task:M19 Append Present Days
                    'Exit For
                End If
            Next

            Dim dblGrossSalary As Double = 0D
            If Not (Val(Me.txtBasicSalary.Text) = 0 Or Val(Me.txtWorkingDays.Text) = 0) Then
                dblGrossSalary = (Val(Me.txtBasicSalary.Text) / Val(Me.txtWorkingDays.Text)) * _PresentDays
                Me.txtGrossSalary.Text = dblGrossSalary
            Else
                Me.txtGrossSalary.Text = 0
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub OptMonthly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptMonthly.CheckedChanged
        Try
            If Me.OptDateRange.Checked = False Then
                Me.grpMonth.Enabled = True
            Else
                Me.grpMonth.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub OptDateRange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptDateRange.CheckedChanged
        Try
            If OptMonthly.Checked = False Then
                Me.grpDateRange.Enabled = True
            Else
                Me.grpDateRange.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class