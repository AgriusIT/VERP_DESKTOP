Public Class frmGrdRptCustomersWiseSummarySalesChart
    Private _DateFrom As DateTime
    Private _DateTo As DateTime
    Private _dtData As New DataTable
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFrom.Value = Date.Today
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-1)
            Me.dtpTo.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpTo.Value = Date.Today
        End If
    End Sub

 

   

    Private Sub frmGrdRptCustomersWiseSummarySalesChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            _DateFrom = Me.dtpFrom.Value
            _DateTo = Me.dtpTo.Value

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub GetData()
        Try
            _dtData = GetDataTable("SP_CustomersWiseSummarySalesChart '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "'")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FillGrid()
        Try
            If _dtData IsNot Nothing Then Me.GridEX1.DataSource = _dtData
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            For i As Integer = Me.GridEX1.RootTable.Columns("Sales").Index To Me.GridEX1.RootTable.Columns.Count - 1
                Me.GridEX1.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.GridEX1.RootTable.Columns(i).FormatString = "N0"
                Me.GridEX1.RootTable.Columns(i).TotalFormatString = "N0"
                Me.GridEX1.RootTable.Columns(i).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(i).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Next
            Me.GridEX1.RootTable.Columns("SalesIncTaxFreight").CellStyle.BackColor = Color.Ivory
            Me.GridEX1.RootTable.Columns("Net Amount").CellStyle.BackColor = Color.Snow

            Me.GridEX1.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            GetData()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptCustomersWiseSummarySalesChart_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim lbl As New Label
        Try
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()

            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim lbl As New Label
        Try
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()
            _DateFrom = Me.dtpFrom.Value
            _
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Customers Wise Growth Sales " & Chr(10) & "Date From:" & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & " "

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFrom.ValueChanged, dtpTo.ValueChanged
        Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Customers Wise Growth Sales " & vbCrLf & "Date From:" & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & " "
    End Sub

  
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

    End Sub
End Class