Public Class frmRptGrdStockInOutDetail
    Dim dt As New DataTable
    Dim _DateFrom As DateTime
    Dim _DateTo As DateTime
    Dim _LocationId As Integer = 0
    Dim dtItemData As New DataTable
    Dim IsFormLoaded As Boolean = False
    Dim _ArticleDefId As Integer = 0
    Enum enmStockDetail
        ArticleDefId
        ArticleTypeName
        ArticleCode
        ArticleDescription
        ArticleColorName
        ArticleSizeName
        Opening
        InQty
        OutQty
        NetQty
    End Enum
    Private Sub filgrid()
        Me.Cursor = Cursors.WaitCursor

        Try

            'If BackgroundWorker1.IsBusy Then Exit Sub
            'BackgroundWorker1.RunWorkerAsync()
            'Do While BackgroundWorker1.IsBusy
            '    Application.DoEvents()
            'Loop
            GetData()
            grd.DataSource = dt
            grd.RetrieveStructure()
            gridset()
            CtrlGrdBar1_Load(Nothing, Nothing)
            'Dim ProjectGroup As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Project"))
            'ProjectGroup.GroupPrefix = String.Empty
            'Me.grd.RootTable.Groups.Add(ProjectGroup)
            'Dim TotalNullProjectQty As Double = 0D
            'Dim TotalQty As Double = 0D
            'Dim i As Integer = 0
            'dt = CType(Me.grd.DataSource, DataTable)
            'Dim Project As String = String.Empty
            'Dim drProjectFilter() As DataRow
            'Dim dr() As DataRow
            'For Each row As DataRow In dt.DefaultView.Table.Rows  '1st Loop
            '    If Not Project <> row.Item("Project").ToString Then
            '        'TotalNullProjectQty += row.Item("InQty") - row.Item("OutQty")
            '        'row.BeginEdit()
            '        'row.Item("NetQty") = TotalNullProjectQty
            '        'row.EndEdit()
            '    Else
            '        TotalNullProjectQty = 0
            '        TotalQty = 0
            '        drProjectFilter = dt.Select("Project='" & row.Item("Project") & "'")
            '        For Each r As DataRow In drProjectFilter '2nd Loop
            '            dr = dt.Select("Project='" & r.Item("Project") & "' AND StockTransDetailId=" & r.Item("StockTransDetailId") & "")
            '            TotalQty += r.Item("InQty") - r.Item("OutQty")
            '            r.BeginEdit()
            '            r.Item("NetQty") = TotalQty
            '            r.EndEdit()
            '            Project = r.Item("Project")
            '            i += 1
            '        Next
            '    End If
            '    row.AcceptChanges()
            'Next
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub filcombe()
        Try
            Dim str As String
            str = " If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order "
            FillDropDown(cmblocation, str)
            'Dim dt As DataTable
            'dt = GetDataTable(str)
            'cmblocation.DataSource = dt
            'cmblocation.ValueMember = "location_id"
            'cmblocation.DisplayMember = "location_name"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmRptGrdStockInOutDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lbl As New Label
        Try

            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()
            GetSecurityRights()

            Me.cmbPeriod.Text = "Current Month"
            Call filcombe()
            Me.cmblocation.SelectedIndex = 0


            _DateFrom = Me.dtpfrom.Value
            _DateTo = Me.dtpto.Value
            _LocationId = Me.cmblocation.SelectedValue
            'RemoveHandler grd.SelectionChanged, AddressOf grd_SelectionChanged

            filgrid()
            IsFormLoaded = True
            Me.SplitContainer1.Panel2Collapsed = True

            'FillItemData()

        Catch ex As Exception
            ShowErrorMessage("Error occured while loading data: " & ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub
    'Waqar Raza Added this Rights SUB............
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.PrintToolStripButton.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmRptGrdStockStatement)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.PrintToolStripButton.Text = "Print Item History" Then
                            Me.PrintToolStripButton.Enabled = dt.Rows(0).Item("Print Item History_Rights").ToString()
                        End If
                    End If
                End If
            Else
                Me.Visible = False
                Me.PrintToolStripButton.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Print Item History" Then
                        If Me.PrintToolStripButton.Text = "&Print Item History" Then PrintToolStripButton.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                       
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim lbl As New Label
        Try
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()

            _DateFrom = Me.dtpfrom.Value
            _DateTo = Me.dtpto.Value
            _LocationId = Me.cmblocation.SelectedValue

            filgrid()
            'FillItemData()

        Catch ex As Exception
            ShowErrorMessage("Error occured while loading data: " & ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub
    Private Sub gridset()
        Try

            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grd.RootTable.Columns(enmStockDetail.NetQty).Caption = "Balance Qty"
            For Each col As Janus.Windows.GridEX.GridEXColumn In grd.RootTable.Columns
                If col.Index = enmStockDetail.ArticleDefId Then
                    col.Visible = False
                End If
            Next

            For Each col As Janus.Windows.GridEX.GridEXColumn In grd.RootTable.Columns
                If col.Index = enmStockDetail.Opening Or col.Index = enmStockDetail.InQty Or col.Index = enmStockDetail.OutQty Or col.Index = enmStockDetail.NetQty Then
                    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.FormatString = "N2"
                    col.TotalFormatString = "N2"
                End If
            Next
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(str_ApplicationStartUpPath & "\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New System.IO.FileStream(str_ApplicationStartUpPath & "\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Stock In Out Detail " & vbCrLf & "Date From:" & Me.dtpfrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpto.Value.ToString("dd/MMM/yyyy") & " "
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpfrom.Value = Date.Today
            Me.dtpto.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpfrom.Value = Date.Today.AddDays(-1)
            Me.dtpto.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpfrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpto.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpfrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpto.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpfrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpto.Value = Date.Today
        End If
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            'Dim str As String
            'str = "SP_StockInOutDetail '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', " & _LocationId & ""
            'dt = GetDataTable(str)
            'dt.Columns.Add(enmStockDetail.NetQty.ToString, GetType(System.Double))
            'dt.Columns(enmStockDetail.NetQty).Expression = "(Opening +(InQty-OutQty))"
            'dt.AcceptChanges()
        Catch ex As Exception
            ShowErrorMessage("Error occured while loading data: " & ex.Message)
        End Try
    End Sub
    Private Sub GetData()
        Try
            Dim str As String
            str = "SP_StockInOutDetail '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', " & _LocationId & ", " & LoginUserId & ""
            dt = GetDataTable(str)
            dt.Columns.Add(enmStockDetail.NetQty.ToString, GetType(System.Double))
            dt.Columns(enmStockDetail.NetQty).Expression = "(Opening +(InQty-OutQty))"
            dt.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetItemData()
        Try
            'If Me.grd.RowCount = 0 Then Exit Sub
            Dim str As String = String.Empty
            str = "SP_ItemHistory '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', " & _LocationId & ", " & _ArticleDefId & ", " & LoginUserId & ""
            dtItemData = GetDataTable(str)
            dtItemData.Columns.Add("BalanceQty", GetType(System.Double))
            Dim i As Integer = 0
            Dim dblTotalOpeningQty As Double = 0D
            Dim dblTotalQty As Double = 0D
            For Each row As DataRow In dtItemData.Rows
                dblTotalQty += (row("InQty") - row("OutQty"))
                row.BeginEdit()
                If i = 0 Then
                    row("BalanceQty") = dblTotalQty
                Else
                    row("BalanceQty") = dblTotalQty
                End If

                i += 1
                row.EndEdit()
            Next
            dtItemData.AcceptChanges()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try

            ''If Me.grd.RowCount = 0 Then Exit Sub
            'Dim str As String = String.Empty
            'str = "SP_ItemHistory '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', " & _LocationId & ", " & _ArticleDefId & ""
            'dtItemData = GetDataTable(str)
            'dtItemData.Columns.Add("BalanceQty", GetType(System.Double))
            'Dim i As Integer = 0
            'Dim dblTotalOpeningQty As Double = 0D
            'Dim dblTotalQty As Double = 0D
            'For Each row As DataRow In dtItemData.Rows
            '    dblTotalQty += (row("InQty") - row("OutQty"))
            '    row.BeginEdit()
            '    If i = 0 Then
            '        row("BalanceQty") = dblTotalQty
            '    Else
            '        row("BalanceQty") = dblTotalQty
            '    End If

            '    i += 1
            '    row.EndEdit()
            'Next
            'dtItemData.AcceptChanges()

        Catch ex As Exception
            ShowErrorMessage("Error occured while loading data: " & ex.Message)
        End Try
    End Sub
    'Private Sub FillItemData()
    '    Try

    '        If BackgroundWorker2.IsBusy Then Exit Sub
    '        BackgroundWorker2.RunWorkerAsync()
    '        Do While BackgroundWorker2.IsBusy
    '            Application.DoEvents()
    '        Loop

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Private Sub grd_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.SelectionChanged
        Try
            If Not IsFormLoaded = True Then Exit Sub
            'If Me.grdItemHistory.RowCount = 0 Then Exit Sub
            If Me.SplitContainer1.Panel2Collapsed = True Then
                Me.SplitContainer1.Panel2Collapsed = False
            End If
            'If IsFormLoaded = True Then
            _DateFrom = Me.dtpfrom.Value
            _DateTo = Me.dtpto.Value
            _ArticleDefId = Me.grd.GetRow.Cells(enmStockDetail.ArticleDefId).Value
            GetItemData()

            grdItemHistory.DataSource = Nothing
            Me.grdItemHistory.DataSource = dtItemData
            Me.grdItemHistory.RetrieveStructure()
            Me.grdItemHistory.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdItemHistory.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdItemHistory.RootTable.Columns(0).Visible = False
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdItemHistory.RootTable.Columns
                If col.Index = 8 Or col.Index = 9 Or col.Index = 10 Then
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.FormatString = "N2"
                    col.TotalFormatString = "N2"
                End If
                If col.Index = 8 Or col.Index = 9 Then
                    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                End If
            Next
            For i As Integer = 0 To Me.grdItemHistory.RootTable.Columns.Count - 1
                Me.grdItemHistory.RootTable.Columns(i).AllowSort = False
            Next
            Me.grdItemHistory.AutoSizeColumns()
            'End If
        Catch ex As Exception
            ShowErrorMessage("Error occured while selection changed: " & ex.Message)
        End Try
    End Sub
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            GetCrystalReportRights()
            AddRptParam("FromDate", Me.dtpfrom.Value)
            AddRptParam("ToDate", Me.dtpto.Value)
            ShowReport("crptItemHistory", , , , False, , , dtItemData)
        Catch ex As Exception
            ShowErrorMessage("Error occurd while showing report: " & ex.Message)
        End Try
    End Sub
    Private Sub dtpfrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpfrom.ValueChanged, dtpto.ValueChanged
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Stock In Out Detail " & vbCrLf & "Date From:" & Me.dtpfrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpto.Value.ToString("dd/MMM/yyyy") & " "
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click

    End Sub
End Class