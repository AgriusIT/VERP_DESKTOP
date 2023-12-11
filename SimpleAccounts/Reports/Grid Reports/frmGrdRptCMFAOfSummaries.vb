Public Class frmGrdRptCMFAOfSummaries
    Private Sub frmGrdRptCMFAOfSummaries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.dtpDateFrom.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpDateTo.Value = Date.Now
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            FillGrid()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillGrid()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SP_CMFAOFSummaries '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "','" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "'"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grd.RootTable.Columns("DocId").Visible = False
            Me.grd.RootTable.Columns("Detail_Code").Visible = False
            Me.grd.RootTable.Columns("OPEX_Sale_Percent").Caption = "Opex %"
            Me.grd.RootTable.Columns("Opex_Value").Caption = "Opex"
            Me.grd.RootTable.Columns("ProjectedExpAmount").Caption = "Projecred Expense Amount"
            Me.grd.RootTable.Columns("Contribution_PercentAge").Caption = "Contribution %"
            Me.grd.RootTable.Columns("CompanyName").Caption = "Company"

            Me.grd.RootTable.Columns("WHT %").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("WHT %").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("WHT").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("WHT").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("GST %").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("GST %").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("GST").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("GST").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Contribution_Percentage").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Contribution_Percentage").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Contribution").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Contribution").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OPEX_Sale_Percent").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OPEX_Sale_Percent").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Opex_Value").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Opex_Value").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ProjectedExpAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ProjectedExpAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Net Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Net Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Client Budget").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Client Budget").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grd.RootTable.Columns("Client Budget").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Net Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("ProjectedExpAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Opex_Value").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Contribution").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("GST").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("WHT").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("Client Budget").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Net Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("ProjectedExpAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Opex_Value").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Contribution").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("GST").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("WHT").FormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("Client Budget").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Net Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("ProjectedExpAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Opex_Value").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Contribution").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("GST").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("WHT").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("Contribution_Percentage").FormatString = "N0"
            Me.grd.RootTable.Columns("Date").FormatString = "dd/MMM/yyyy"

            Me.grd.RootTable.Columns("Job Starting Date").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("Expected Job Complition").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("Verification Job Complition").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("Tentative Invoice Date").FormatString = "dd/MMM/yyyy"

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "CMFA Detail History" & Chr(10) & "From Date: " & dtpDateFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpDateTo.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class