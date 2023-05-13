Public Class frmGrdRptCharts
    Enum enmReports
        Sales
        Purchase
        Expense
    End Enum
    Dim arrType() As String = {"Sales", "Purchase", "Expense"}

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Type", GetType(System.String))
            Dim dr As DataRow
            For Each objStr As String In arrType
                dr = dt.NewRow
                dr(0) = objStr.ToString
                dt.Rows.Add(dr)
            Next
            dt.AcceptChanges()
            Dim intMonth As Integer = Me.dtpTo.Value.Subtract(Me.dtpFrom.Value).Days
            Dim AddDate As New List(Of DateTime)
            For i As Integer = 0 To intMonth - 1 'cmbmonth.SelectedValue  'Run Loop From 1st to Selected Value
                AddDate.Add(dtpFrom.Value.AddDays(i))
                'dt.Columns.Add(z, GetType(System.Int32), z) 'Add columns
                'dt.Columns.Add(GetMonthName(z).ToString("MMM") & "(" & Me.dtpFrom.Value.Year & ")", GetType(System.Double))    'Go in the Functio of GetMonthName and Select Name and convert into String 
            Next
            Dim strMonth As String = String.Empty
            Dim myMonth As String = String.Empty
            For Each dtDatetime As DateTime In AddDate
                If strMonth <> dtDatetime.Month & "-" & dtDatetime.Year Then
                    myMonth = dtDatetime.Month.ToString & "" & dtDatetime.Year.ToString
                    dt.Columns.Add(myMonth, GetType(System.String), myMonth) 'Add columns
                    dt.Columns.Add(dtDatetime.ToString("MMM") & "-" & dtDatetime.Year, GetType(System.Double))    'Go in the Functio of GetMonthName and Select Name and convert into String 
                End If
                strMonth = dtDatetime.Month & "-" & dtDatetime.Year
            Next

            For Each drow3 As DataRow In dt.Rows
                For j As Integer = 1 To dt.Columns.Count - 2 Step 2
                    drow3.BeginEdit()
                    drow3(j + 1) = 0
                    drow3.EndEdit()
                Next
            Next


            Dim strSQL As String = String.Empty
            strSQL = String.Empty
            'strSQL = "Select 'Sales' as Type, (Convert(Varchar, Month(SalesDate)) + '' + Convert(Varchar, Year(SalesDate))) as [Month], SUM(Isnull(SalesAmount,0)) as SalesAmount From SalesMasterTable WHERE (Convert(Varchar ,SalesDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By  (Convert(Varchar, Month(SalesDate)) + '' + Convert(Varchar, Year(SalesDate)))"
            strSQL = "Select 'Sales' as Type, (Convert(Varchar, Month(Voucher_date)) + '' + Convert(Varchar, Year(Voucher_Date))) as [Month], SUM(Isnull(credit_amount,0)-Isnull(debit_amount,0)) as ExpenseAmount From tblVoucher INNER JOIN tblVoucherDetail on tblVoucherDetail.Voucher_Id = tblVoucher.Voucher_Id WHERE (Convert(Varchar ,Voucher_Date, 102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) and tblvoucherDetail.coa_detail_id in(Select Config_Value from ConfigValuesTable where Config_Type='SalesCreditAccount') Group By  (Convert(Varchar, Month(Voucher_Date)) + '' + Convert(Varchar, Year(Voucher_Date)))"
            Dim dtSales As New DataTable
            dtSales = GetDataTable(strSQL)
            Dim drSales() As DataRow
            For Each r As DataRow In dt.Rows
                drSales = dtSales.Select("Type='" & r("Type") & "'")
                If drSales IsNot Nothing Then
                    If drSales.Length > 0 Then
                        For Each drFound As DataRow In drSales
                            r.BeginEdit()
                            r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next



            strSQL = String.Empty
            'strSQL = "Select 'Purchase' as Type, (Convert(Varchar, Month(ReceivingDate)) + '' + Convert(Varchar, Year(ReceivingDate))) as [Month], SUM(Isnull(ReceivingAmount,0)) as ReceivingAmount From ReceivingMasterTable WHERE (Convert(Varchar ,ReceivingDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By  (Convert(Varchar, Month(ReceivingDate)) + '' + Convert(Varchar, Year(ReceivingDate)))"
            strSQL = "Select 'Purchase' as Type, (Convert(Varchar, Month(Voucher_date)) + '' + Convert(Varchar, Year(Voucher_Date))) as [Month], SUM(Isnull(debit_amount,0)-Isnull(credit_amount,0)) as ExpenseAmount From tblVoucher INNER JOIN tblVoucherDetail on tblVoucherDetail.Voucher_Id = tblVoucher.Voucher_Id WHERE (Convert(Varchar ,Voucher_Date, 102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) and tblvoucherDetail.coa_detail_id in(Select Config_Value from ConfigValuesTable where Config_Type='PurchaseDebitAccount') Group By  (Convert(Varchar, Month(Voucher_Date)) + '' + Convert(Varchar, Year(Voucher_Date)))"
            Dim dtPur As New DataTable
            dtPur = GetDataTable(strSQL)
            Dim drPur() As DataRow
            For Each r As DataRow In dt.Rows
                drPur = dtPur.Select("Type='" & r("Type") & "'")
                If drPur IsNot Nothing Then
                    If drPur.Length > 0 Then
                        For Each drFound As DataRow In drPur
                            r.BeginEdit()
                            r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next

            strSQL = String.Empty
            strSQL = "Select 'Expense' as Type, (Convert(Varchar, Month(Voucher_date)) + '' + Convert(Varchar, Year(Voucher_Date))) as [Month], SUM(Isnull(debit_amount,0)-Isnull(credit_amount,0)) as ExpenseAmount From tblVoucher INNER JOIN tblVoucherDetail on tblVoucherDetail.Voucher_Id = tblVoucher.Voucher_Id WHERE (Convert(Varchar ,Voucher_Date, 102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) and tblvoucherDetail.coa_detail_id in(Select coa_detail_id from vwcoadetail where account_type='Expense') Group By  (Convert(Varchar, Month(Voucher_Date)) + '' + Convert(Varchar, Year(Voucher_Date)))"
            Dim dtExp As New DataTable
            dtExp = GetDataTable(strSQL)
            Dim drExp() As DataRow
            For Each r As DataRow In dt.Rows
                drExp = dtExp.Select("Type='" & r("Type") & "'")
                If drExp IsNot Nothing Then
                    If drExp.Length > 0 Then
                        For Each drFound As DataRow In drExp
                            r.BeginEdit()
                            r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next
            Dim strTotal As String = String.Empty
            For c As Integer = 1 To dt.Columns.Count - 2 Step 2
                If strTotal.Length > 0 Then
                    strTotal += "+" & "[" & dt.Columns(c + 1).ColumnName & "]"
                Else
                    strTotal = "[" & dt.Columns(c + 1).ColumnName & "]"
                End If
            Next
            dt.Columns.Add("Total", GetType(System.Double))
            dt.Columns("Total").Expression = strTotal.ToString
            Me.grdEmployee.DataSource = dt
            Me.grdEmployee.RetrieveStructure()
            Me.grdEmployee.RootTable.Columns("Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployee.RootTable.Columns("Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployee.RootTable.Columns("Total").FormatString = "N"
            ApplyGridSettings()
            Me.grdEmployee.AutoSizeColumns()
            Me.grdEmployee.RootTable.Columns.Add("Show Graph", Janus.Windows.GridEX.ColumnType.Text, Janus.Windows.GridEX.EditType.TextBox)
            Me.grdEmployee.RootTable.Columns("Show Graph").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            Me.grdEmployee.RootTable.Columns("Show Graph").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            Me.grdEmployee.RootTable.Columns("Show Graph").ButtonText = "Show Graph"
            Me.grdEmployee.RootTable.Columns("Show Graph").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grdEmployee.RootTable.Columns("Show Graph").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grdEmployee.RootTable.Columns("Show Graph").Caption = "Report"

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Sub ApplyGridSettings()
        Try
            For c As Integer = 1 To Me.grdEmployee.RootTable.Columns.Count - 2 Step 2
                Me.grdEmployee.RootTable.Columns(c).Visible = False
                Me.grdEmployee.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdEmployee.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdEmployee.RootTable.Columns(c + 1).FormatString = "N"
            Next
            Me.grdEmployee.FrozenColumns = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdEmployee_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdEmployee.ColumnButtonClick
        Try
            If e.Column.Key = "Show Graph" Then
                Dim dt As DataTable = CType(Me.grdEmployee.DataSource, DataTable)
                dt.TableName = "ChartTable"
                Dim dv As New DataView
                dv.Table = dt
                dv.RowFilter = "Type='" & Me.grdEmployee.GetRow.Cells("Type").Value.ToString & "'"
                Dim dr As DataRow
                Dim ds As New dsChart
                For Each r As DataRow In dv.ToTable.Rows
                    For c As Integer = 1 To dt.Columns.Count - 2 Step 2
                        r.BeginEdit()
                        dr = ds.Tables(0).NewRow
                        dr(0) = r(0)
                        dr(1) = dt.Columns(c + 1).ColumnName
                        dr(2) = r(c + 1)
                        r.EndEdit()
                        ds.Tables(0).Rows.Add(dr)
                    Next
                Next
                If Me.rbtLine.Checked = True Then
                    ShowReport("RptChartLine", , , , , , , ds.Tables(0))
                ElseIf Me.rbtBar.Checked = True Then
                    ShowReport("RptChartBar", , , , , , , ds.Tables(0))
                ElseIf Me.rbtPie.Checked = True Then
                    ShowReport("RptChartPie", , , , , , , ds.Tables(0))
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
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
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub rbtLine_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtLine.CheckedChanged
        Try
            If Me.rbtLine.Checked = True Then
                Me.PictureBox1.Image = My.Resources.line_chart256
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtBar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtBar.CheckedChanged
        Try
            If rbtBar.Checked = True Then
                Me.PictureBox1.Image = My.Resources.bar_graph
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtPie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtPie.CheckedChanged
        Try
            If Me.rbtPie.Checked = True Then
                Me.PictureBox1.Image = My.Resources.pie_chart_icon
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployee.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployee.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdEmployee.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Graph Report" & Chr(10) & "Date From:" & dtpFrom.ToString("dd/MMM/yyyy") & " Date To:" & dtpTo.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub
End Class