Public Class frmGrdSalemansDemandDetail
    Public Enum enmCustomers
        Id
        Code
        Title
        Count
    End Enum
    Private Sub frmGrdDispatchDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            'FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillGrid(Optional ByVal Condition As String = "")
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Select convert(varchar, coa_detail_id) as coa_detail_id, detail_code, detail_title From vwCOADetail  WHERE vwCOADetail.Account_Type='Customer'  ORDER BY 1 ASC"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)

            '-------------------------------- Products ---------------------------
            If Me.rbtItemName.Checked = True Then
                strSQL = "Select ArticleId, ArticleDescription, Isnull(SalePrice,0) as SalePrice  From ArticleDefView WHERE SalesItem=1 AND Active=1 ORDER BY ArticleDefView.SortOrder Asc"
            Else
                strSQL = "Select ArticleDefTable.ArticleId, ArticleDefTableMaster.ArticleCode, Isnull(ArticleDefTableMaster.SalePrice,0) as SalePrice From ArticleDefTable INNER JOIN ArticleDefTableMaster On ArticleDefTableMaster.ArticleId = ArticleDefTable.MasterId INNER JOIN ArticleGroupDefTable ON  ArticleGroupDefTable.ArticleGroupId = ArticleDefTableMaster.ArticleGroupId WHERE ArticleGroupDefTable.SalesItem=1 AND ArticleDefTable.Active=1 ORDER BY ArticleDefTable.SortOrder Asc"
            End If
            Dim dtArt As New DataTable
            dtArt = GetDataTable(strSQL)
            Dim i As Integer = 1
            For Each r As DataRow In dtArt.Rows
                If Not dtArt.Columns.Contains(r(1)) Then
                    dt.Columns.Add(r(0), GetType(System.String), r(0))
                    dt.Columns.Add(r(1) & "^" & i, GetType(System.Double))
                    dt.Columns.Add(r(2) & "^" & i, GetType(System.Double), r(2))
                End If
                i += 1
            Next
            For Each r As DataRow In dt.Rows
                For c As Integer = enmCustomers.Count To dt.Columns.Count - 3 Step 3
                    r.BeginEdit()
                    r(c + 1) = 0
                    r.EndEdit()
                Next
            Next

            '------------------------------------------------------------- Transactions -----------------------------------------
            strSQL = "Select convert(varchar, VendorId) as CustomerCode, ArticleDefId, SUM(Qty) as Qty From SalesOrderMasterTable INNER JOIN SalesOrderDetailTable ON SalesOrderMasterTable.SalesOrderId = SalesOrderDetailTable.SalesOrderId WHERE (Convert(Varchar, SalesOrderMasterTable.SalesOrderDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By VendorId, ArticleDefId"
            Dim dtData As New DataTable
            dtData = GetDataTable(strSQL)
            Dim dr() As DataRow
            For Each r As DataRow In dt.Rows
                dr = dtData.Select("CustomerCode='" & r(0) & "'")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            r.BeginEdit()
                            r(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()
            dt.Columns.Add("Total", GetType(System.Double))
            dt.Columns.Add("Amount", GetType(System.Double))
            Dim strTotal As String = String.Empty
            For c As Integer = enmCustomers.Count To dt.Columns.Count - 3 Step 3
                If strTotal.Length > 0 Then
                    strTotal = strTotal & "+" & "[" & dt.Columns(c + 1).ColumnName & "]"
                Else
                    strTotal = "[" & dt.Columns(c + 1).ColumnName & "]"
                End If
            Next
            Dim strTotalAmount As String = String.Empty
            For c As Integer = enmCustomers.Count To dt.Columns.Count - 3 Step 3
                If strTotalAmount.Length > 0 Then
                    strTotalAmount = strTotalAmount & "+" & "[" & dt.Columns(c + 1).ColumnName & "]*[" & dt.Columns(c + 2).ColumnName & "]"
                Else
                    strTotalAmount = "[" & dt.Columns(c + 1).ColumnName & "]*[" & dt.Columns(c + 2).ColumnName & "]"
                End If
            Next
            dt.Columns("Total").Expression = strTotal.ToString
            dt.Columns("Amount").Expression = strTotalAmount.ToString
            dt.TableName = "DispatchDetail"
            Dim dv As New DataView
            dv.Table = dt

            Me.grd.DataSource = dv.ToTable
            Me.grd.RetrieveStructure()
            ApplyGridSetting()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns(1).Caption = "Account Code"
            Me.grd.RootTable.Columns(2).Caption = "Account Description"
            For c As Integer = enmCustomers.Count To Me.grd.RootTable.Columns.Count - 3 Step 3
                Me.grd.RootTable.Columns(c).Visible = False
                Me.grd.RootTable.Columns(c + 2).Visible = False
                Me.grd.RootTable.Columns(c + 1).Caption = Microsoft.VisualBasic.Left(Me.grd.RootTable.Columns(c + 1).Caption, Me.grd.RootTable.Columns(c + 1).Caption.LastIndexOf("^") - 1 + 1)
                Me.grd.RootTable.Columns(c + 1).AllowSort = False
                'Me.grd.RootTable.Columns(c + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Next
            Me.grd.RootTable.Columns("Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Amount").FormatString = "N"
            Me.grd.RootTable.Columns("Amount").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.FrozenColumns = enmCustomers.Count
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub grd_LoadingRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.LoadingRow
    '    Try
    '        If Me.grd.RowCount = 0 Then Exit Sub
    '        If e.Row.Cells(0).Value = "0A" Then
    '            Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
    '            rowStyle.BackColor = Color.LightCyan
    '            e.Row.RowStyle = rowStyle
    '        ElseIf e.Row.Cells(0).Value = "0B" Then
    '            Dim rowStyle1 As New Janus.Windows.GridEX.GridEXFormatStyle
    '            rowStyle1.BackColor = Color.LightCyan
    '            e.Row.RowStyle = rowStyle1
    '        ElseIf e.Row.Cells(0).Value = "0C" Then
    '            Dim rowStyle2 As New Janus.Windows.GridEX.GridEXFormatStyle
    '            rowStyle2.BackColor = Color.LightCyan
    '            e.Row.RowStyle = rowStyle2
    '        ElseIf e.Row.Cells(0).Value = "0D" Then
    '            Dim rowStyle3 As New Janus.Windows.GridEX.GridEXFormatStyle
    '            rowStyle3.BackColor = Color.Ivory
    '            e.Row.RowStyle = rowStyle3
    '        ElseIf e.Row.Cells(0).Value = "0E" Then
    '            Dim rowStyle4 As New Janus.Windows.GridEX.GridEXFormatStyle
    '            rowStyle4.BackColor = Color.Honeydew
    '            e.Row.RowStyle = rowStyle4
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
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
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdDispatchDetail_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Back Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            FillGrid()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try

            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = "Issued Sales Certificate Report" & Chr(10) & CompanyTitle & Chr(10) & " From: " & Me.dtpFromDate.Value.Date.ToString("dd/MMM/yyyy") & " To: " & Me.dtpToDate.Value.Date.ToString("dd/MMM/yyyy")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class