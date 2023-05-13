Imports Infragistics.UltraChart.Resources.Appearance

Public Class frmGrdRptInstallmentBalance
    Dim _IsOpenForm As Boolean = False
    Public Sub FillInstallmentBalance()
        Try
            Dim dt As New DataTable
            dt = GetDataTable("SP_InstallmentBalance")
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()

            Me.grd.RootTable.Columns("coa_detail_id").Visible = False
            grd.RootTable.Columns("InvoiceAmount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("AdvanceAmount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Installment").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("ReceivedAmount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("BalanceAmount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("InvoiceAmount").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("AdvanceAmount").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Installment").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("ReceivedAmount").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("BalanceAmount").TotalFormatString = "N" & DecimalPointInValue

            grd.RootTable.Columns("InvoiceAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("AdvanceAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Installment").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("ReceivedAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("BalanceAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("Detail_Code").Caption = "Account Code"
            grd.RootTable.Columns("Detail_Title").Caption = "Customer"
            grd.RootTable.Columns("Account_Type").Caption = "Account Type"

            grd.RootTable.Columns("Sub_Sub_Code").Caption = "Account Head Code"
            grd.RootTable.Columns("Sub_Sub_Title").Caption = "Account Head"
            grd.RootTable.Columns("CityName").Caption = "City"
            grd.RootTable.Columns("TerritoryName").Caption = "Area"

            Me.grd.AutoSizeColumns()


            grd.RootTable.Columns.Add("btnReport", Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.TextBox)
            grd.RootTable.Columns("btnReport").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            grd.RootTable.Columns("btnReport").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            grd.RootTable.Columns("btnReport").ButtonText = "Chart Report"
            grd.RootTable.Columns("btnReport").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            grd.RootTable.Columns("btnReport").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            grd.RootTable.Columns("btnReport").Caption = "Action"
            grd.RootTable.Columns.Add("btnLedgerReport", Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.TextBox)
            grd.RootTable.Columns("btnLedgerReport").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            grd.RootTable.Columns("btnLedgerReport").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            grd.RootTable.Columns("btnLedgerReport").ButtonText = "Ledger Report"
            grd.RootTable.Columns("btnLedgerReport").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            grd.RootTable.Columns("btnLedgerReport").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            grd.RootTable.Columns("btnLedgerReport").Caption = "Ledger"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmGrdRptInstallmentBalance_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim lbl As New Label
        lbl.Visible = True
        lbl.Text = "Loading please wait...."
        lbl.Dock = DockStyle.Fill
        Me.Controls.Add(lbl)
        lbl.BringToFront()
        Try
            FillInstallmentBalance()
            _IsOpenForm = True
            'grd_SelectionChanged(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    'Private Sub grd_SelectionChanged(sender As Object, e As EventArgs) Handles grd.SelectionChanged
    '    Try

    '        If Me.grd.RowCount = 0 Then Exit Sub
    '        If _IsOpenForm = False Then Exit Sub
    '        SetChart(Me.grd.GetRow.Cells("coa_detail_id").Value)

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Public Sub ShowChartReport(CustomerCode As Integer)
        Try


            Dim dt As New DataTable
            dt = GetDataTable("SP_InstallmentChartData " & CustomerCode & "")
            dt.AcceptChanges()
            ShowReport("rptInstallmentChart", , , , , , , dt)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ShowLedgerReport(CustomerCode As Integer)
        Try
            AddRptParam("@CustomerId", CustomerCode)
            ShowReport("rptInstallmentLedger")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            frmGrdRptInstallmentBalance_Shown(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If e.Column.Key = "btnReport" Then
                ShowChartReport(Val(Me.grd.GetRow.Cells("coa_detail_id").Value.ToString))
            ElseIf e.Column.Key = "btnLedgerReport" Then
                ShowLedgerReport(Val(Me.grd.GetRow.Cells("coa_detail_id").Value.ToString))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
    '    Try
    '        If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
    '            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
    '            Me.grd.LoadLayoutFile(fs)
    '            fs.Close()
    '            fs.Dispose()
    '        End If
    '        Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Installment Balance Report"
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
End Class