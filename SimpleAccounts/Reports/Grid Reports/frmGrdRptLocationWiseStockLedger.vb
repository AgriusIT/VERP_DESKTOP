Imports SBModel
Public Class frmGrdRptLocationWiseStockLedger
    Private _dv As New DataView
    Private _Project As Integer
    Private _Location As Integer
    Private _IsOpenedForm As Boolean = False
    Private _DateFrom As DateTime
    Private _DateTo As DateTime

    Private Sub frmGrdRptLocationWiseStockLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmGrdRptProjectWiseStockLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Me.cmbPeriod.Text = "Current Month"
            FillDropDown(Me.cmbProject, "Select CostCenterId, Name as CostCenter From tblDefCostCenter Order By 2", True)
            FillDropDown(Me.cmbLocations, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order", True)
            _DateFrom = Me.dtpFrom.Value
            _DateTo = Me.dtpTo.Value
            _IsOpenedForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

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

    'Private Sub cmbProject_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If _IsOpenedForm = True Then
    '            _Project = Me.cmbProject.SelectedValue
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub cmbLocations_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLocations.SelectedIndexChanged
        Try
            If _IsOpenedForm = True Then
                _Location = Me.cmbLocations.SelectedValue
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try



        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub
    Private Sub FillGrid()
        Try


            Dim str As String = String.Empty
            str = "SP_LocationWiseStockLedger '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', " & Me.cmbLocations.SelectedValue & ", " & Me.cmbProject.SelectedValue & "," & LoginUserId & ""
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            'dt.Columns.Add("BalanceQty", GetType(System.Double))
            'dt.TableName = "Stock Ledger"
            '_dv.Table = dt
            'If _Project > 0 Then
            '    _dv.RowFilter = "CostCenterId='" & _Project & "'"
            'End If
            'If _Location > 0 Then
            '    _dv.RowFilter = "Location_Id='" & _Location & "'"
            'End If
            'Me.GridEX1.DataSource = Nothing
            'If _dv IsNot Nothing Then
            Me.GridEX1.DataSource = dt
            Me.GridEX1.RetrieveStructure()
            'Me.GridEX1.RootTable.Columns("ArticleId").Visible = False
            Me.GridEX1.RootTable.Columns("ArticleId").Visible = False
            Me.GridEX1.RootTable.Columns("Location_Id").Visible = False
            Me.GridEX1.RootTable.Columns("BalanceQty").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.GridEX1.RootTable.Columns("InQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("OutQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("InQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("OutQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("BalanceQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("BalanceQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("InQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("OutQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("BalanceQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("InQty").FormatString = "N2"
            Me.GridEX1.RootTable.Columns("OutQty").FormatString = "N2"
            Me.GridEX1.RootTable.Columns("BalanceQty").FormatString = "N2"
            'Dim grpLocation As New Janus.Windows.GridEX.GridEXGroup(Me.GridEX1.RootTable.Columns("Location"))
            'Me.GridEX1.RootTable.Groups.Add(grpLocation)
            Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Expanded
            Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            Me.GridEX1.RootTable.Columns("InQty").TotalFormatString = "N2"
            Me.GridEX1.RootTable.Columns("OutQty").TotalFormatString = "N2"
            Me.GridEX1.RootTable.Columns("BalanceQty").TotalFormatString = "N2"
            'Dim i As Integer = 0
            'Dim TotalQty As Double = 0
            'Dim dr() As DataRow
            'Dim strProject As String = String.Empty
            'Dim dv As DataView = CType(Me.GridEX1.DataSource, DataView)
            'For Each Row As DataRow In dv.Table.Rows
            '    If Not strProject = Row("ArticleId").ToString & "" & IIf(Me.cmbLocations.SelectedIndex > 0, " " & Row("Location_Id") & "", "") & "" Then
            '        dr = dv.Table.Select("ArticleId='" & Row("ArticleId").ToString & "' " & IIf(Me.cmbLocations.SelectedIndex > 0, " AND Location_Id='" & Me.cmbLocations.SelectedValue & "'", "") & " ")
            '        TotalQty = 0
            '        For Each r As DataRow In dr
            '            TotalQty += (Val(r(8)) - Val(r(9))) 'InQty - OutQty
            '            r.BeginEdit()
            '            r("BalanceQty") = TotalQty
            '            r.EndEdit()
            '            strProject = Row("ArticleId").ToString & "" & IIf(Me.cmbLocations.SelectedIndex > 0, " " & Row("Location_Id") & "", "") & ""
            '            i += 1
            '        Next
            '        Row.AcceptChanges()
            '    End If
            'Next
            For c As Integer = 0 To Me.GridEX1.RootTable.Columns.Count - 1
                Me.GridEX1.RootTable.Columns(c).AllowSort = True
            Next
            Me.GridEX1.RootTable.Columns("Doc Date").FormatString = "dd/MMM/yyyy"
            'Dim sorkey As New Janus.Windows.GridEX.GridEXSortKey(Me.GridEX1.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.SortOrder.Ascending)
            'Me.GridEX1.RootTable.SortKeys.Add(sorkey)
            Me.GridEX1.AutoSizeColumns()
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptProjectWiseStockLedger_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim lbl As New Label
        Try
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading, Please Wait"
            Me.Controls.Add(lbl)
            lbl.BringToFront()
            'If BackgroundWorker1.IsBusy Then Exit Sub
            'BackgroundWorker1.RunWorkerAsync()
            'Do While BackgroundWorker1.IsBusy
            '    Application.DoEvents()
            'Loop
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data :" & ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim lbl As New Label
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Me.lblProgress.BackColor = Color.LightYellow
            Application.DoEvents()

            Dim str As String
            str = "select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ""
            Dim dtlocation As DataTable
            dtlocation = GetDataTable(str)
            If IsDBNull(dtlocation.Rows(0).Item("Location_Id")) = True Then
                ShowErrorMessage("You Don't have rights to any location, Please Contact to the Authorized Person")
                Exit Sub
            End If

            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()
            '_Project = Me.cmbProject.SelectedValue
            '_Location = Me.cmbLocations.SelectedValue
            '_DateFrom = Me.dtpFrom.Value
            '_DateTo = Me.dtpTo.Value

            'If BackgroundWorker1.IsBusy Then Exit Sub
            'BackgroundWorker1.RunWorkerAsync()
            'Do While BackgroundWorker1.IsBusy
            '    Application.DoEvents()
            'Loop
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data :" & ex.Message)
        Finally
            lbl.Visible = False
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0
            'id = Me.cmbProject.SelectedValue
            'Me.cmbProject.DataSource = Nothing
            'FillDropDown(Me.cmbProject, "Select CostCenterId, Name as CostCenter From tblDefCostCenter Order By 2", True)
            'Me.cmbProject.SelectedValue = id

            id = Me.cmbLocations.SelectedValue
            Me.cmbLocations.DataSource = Nothing
            FillDropDown(Me.cmbLocations, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order", True)
            Me.cmbLocations.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Stock Ledger  By Location" & vbCrLf & "Date From: " & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & ""


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub dtpFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFrom.ValueChanged, dtpTo.ValueChanged
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Stock Ledger  By Location" & vbCrLf & "Date From: " & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class