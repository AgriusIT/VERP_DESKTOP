''30-11-2015 Task291115 Muhammad Ameen: Added new rptTodayTasks Report.
Public Class rptTodayTasks

    Private Sub cmbPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.ccFrom.Value = Date.Today
            Me.ccTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.ccFrom.Value = Date.Today.AddDays(-1)
            Me.ccTo.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.ccFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.ccTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.ccFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.ccTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.ccFrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.ccTo.Value = Date.Today
        End If
    End Sub

    Private Sub rptTodayTasks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub PrintReport()
        Try

            Dim strQuery As String = String.Empty
            Dim dt As New DataTable
            strQuery = "Select TaskDate, Count(TaskId) As NewTask, 0 As ClosedTask FROM  dbo.TblDefTasks " _
                & " WHERE (Convert(varchar, TaskDate ,102)) >= (Convert(datetime, '" & Me.ccFrom.Value & "' ,102)) And (Convert(varchar, TaskDate ,102)) <= (Convert(datetime, '" & Me.ccTo.Value & "', 102))" _
                & " Group By TaskDate " _
                & " Union" _
                & " Select  ClosingDate, 0, Count(TaskId) As ClosedTask1 FROM  dbo.TblDefTasks" _
                & " WHERE (Convert(varchar, ClosingDate ,102)) >= (Convert(datetime,'" & Me.ccFrom.Value & "',102)) And (Convert(varchar, ClosingDate ,102)) <= (Convert(datetime, '" & Me.ccTo.Value & "', 102))" _
                & " Group By ClosingDate "

            dt = GetDataTable(strQuery)
            ShowReport("rptTodayTasks", , , , , , , dt, , , , , )


        Catch ex As Exception

        End Try

    End Sub

    Private Sub BtnGenerate_Click(sender As Object, e As EventArgs) Handles BtnGenerate.Click
        Try
            PrintReport()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class