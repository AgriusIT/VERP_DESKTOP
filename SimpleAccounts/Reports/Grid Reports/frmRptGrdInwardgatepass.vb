Public Class frmRptGrdInwardgatepass
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            'Me.dtpfromdate.Value = Me.dtpfromdate.Value.AddMonths(-1)
            'Me.dtptodate.Value = Date.Now
            Me.cmbPeriod.Text = "Current Month"
            dtpfromdate_ValueChanged(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim _string As String
            _string = " SELECT dbo.Inwardgatepassmastertable.Inwardgatepassdate as [Date], dbo.Inwardgatepassmastertable.InwardGatePassNo as [IGP No], dbo.Inwardgatepassmastertable.BillNo, " & _
                                     " dbo.Inwardgatepassmastertable.PartyName, dbo.Inwardgatepassmastertable.Category, dbo.tblListCity.CityName, " & _
                                     " dbo.Inwardgatepassmastertable.Drivername, dbo.Inwardgatepassmastertable.VehicleNo, dbo.Inwardgatepassmastertable.Remarks, " & _
                                     " dbo.Inwardgatepassdetailtable.Detail, dbo.Inwardgatepassdetailtable.Qty, dbo.Inwardgatepassdetailtable.Price " & _
                                     " FROM dbo.Inwardgatepassdetailtable INNER JOIN " & _
                                     " dbo.Inwardgatepassmastertable ON dbo.Inwardgatepassdetailtable.InwardgatepassId = dbo.Inwardgatepassmastertable.InwardgatepassId INNER JOIN " & _
                                     " dbo.tblListCity ON dbo.Inwardgatepassmastertable.CityId = dbo.tblListCity.CityId " & _
                                     " WHERE (dbo.Inwardgatepassmastertable.Inwardgatepassdate BETWEEN CONVERT(DATETIME, '" & Me.dtpfromdate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(DATETIME, " & _
                                     " '" & Me.dtptodate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102))"
            Dim _dt As New DataTable
            _dt = GetDataTable(_string)
            _dt.Columns.Add("Total", GetType(System.Double))
            _dt.Columns("Total").Expression = "Price*Qty"
            _dt.AcceptChanges()

            Me.grdDailyinwardgatepass.DataSource = _dt
            Me.grdDailyinwardgatepass.RetrieveStructure()


            Me.grdDailyinwardgatepass.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDailyinwardgatepass.RootTable.Columns("Total").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdDailyinwardgatepass.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDailyinwardgatepass.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDailyinwardgatepass.RootTable.Columns("Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDailyinwardgatepass.RootTable.Columns("Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdDailyinwardgatepass.AutoSizeColumns()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub dtpfromdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpfromdate.ValueChanged, dtptodate.ValueChanged
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = "Inward Gatepass Detail " & vbCrLf & " From: " & Me.dtpfromdate.Value.Date.ToString("dd-MM-yyyy") & "  To: " & Me.dtptodate.Value.Date.ToString("dd-MM-yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDailyinwardgatepass.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDailyinwardgatepass.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grdDailyinwardgatepass.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Daily Inward GatePass Summery"
        Catch ex As Exception

        End Try
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
