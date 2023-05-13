Public Class frmgrdrptDailyUpdate
    Dim fromdate As DateTime
    Dim todate As DateTime

    Private Sub frmgrdrptDailyUpdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            NewToolStripButton_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmgrdrptDailyUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cmbPeriod.SelectedIndex = 0
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If cmbPeriod.Text = "Today" Then
                fromdate = Date.Now
                todate = Date.Now
            ElseIf cmbPeriod.Text = "Last Day" Then
                fromdate = Date.Now.AddDays(-1)
                todate = Date.Now.AddDays(-1)
            ElseIf cmbPeriod.Text = "Current Week" Then
                fromdate = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                todate = Date.Now
            ElseIf cmbPeriod.Text = "Current Month" Then
                fromdate = New Date(Date.Now.Year, Date.Now.Month, 1)
                todate = Date.Now
            ElseIf cmbPeriod.Text = "Current Year" Then
                fromdate = New Date(Date.Now.Year, 1, 1)
                todate = Date.Now
            End If
            GridFill(fromdate, todate)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GridFill(ByVal fromdate As DateTime, ByVal todate As DateTime)
        Try
            Dim str As String = "sp_DailyUpdate '" & fromdate.Date.ToString("yyyy-M-d 00:00:00") & "', '" & todate.Date.ToString("yyyy-M-d 23:59:59") & "'"
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            grddailyupdate.DataSource = dt
            grddailyupdate.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            cmbPeriod_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs)
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grddailyupdate.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grddailyupdate.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grddailyupdate.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Daily Updates"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load_1(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try

            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grddailyupdate.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grddailyupdate.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grddailyupdate.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Daily Update Report " & vbCrLf & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class