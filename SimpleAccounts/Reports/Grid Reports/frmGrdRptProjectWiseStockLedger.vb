Public Class frmGrdRptProjectWiseStockLedger
    Private _dv As New DataView
    Private _Project As Integer
    Private _Location As Integer
    Private _IsOpenedForm As Boolean = False
    Private _DateFrom As DateTime
    Private _DateTo As DateTime

    Private Sub frmGrdRptProjectWiseStockLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmGrdRptProjectWiseStockLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            FillDropDown(Me.cmbProject, "Select CostCenterId, Name as CostCenter From tblDefCostCenter WHERE Active=1 Order By 2", True)
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

    Private Sub cmbProject_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProject.SelectedIndexChanged
        Try
            If _IsOpenedForm = True Then
                _Project = Me.cmbProject.SelectedValue
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
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
            Dim str As String = String.Empty
            str = "SP_ProjectWiseStockLedger '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "'"
            Dim dt As DataTable = GetDataTable(str)

            dt.Columns.Add("StockQty", GetType(System.Double))
            dt.Columns("StockQty").Expression = "InQty-OutQty"

            dt.Columns.Add("BalanceQty", GetType(System.Double))
            dt.Columns.Add("Balance", GetType(System.Double))
            dt.Columns("Balance").Expression = "BalanceQty*Rate"
            dt.TableName = "Stock Ledger"
            _dv.Table = dt

            Dim strFilter As String = String.Empty
            strFilter = " [Article Description] IS NOT NULL "
            If _Project > 0 Then
                strFilter += " AND CostCenterId='" & _Project & "'"
            End If
            If _Location > 0 Then
                strFilter += " AND Location_Id='" & _Location & "'"
            Else
                strFilter += " AND Lcation_Id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")"
            End If
            _dv.RowFilter = strFilter

        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub
    Private Sub FillGrid()
        Try
            Me.GridEX1.DataSource = Nothing
            If _dv IsNot Nothing Then Me.GridEX1.DataSource = _dv
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns("CostCenterId").Visible = False
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

            Me.GridEX1.RootTable.Columns("Rate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Rate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.GridEX1.RootTable.Columns("InAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("inAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.GridEX1.RootTable.Columns("OutAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("OutAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.GridEX1.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            GridEX1.RootTable.Columns("InQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("OutQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            GridEX1.RootTable.Columns("InAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("OutAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.GridEX1.RootTable.Columns("InQty").FormatString = "N2"
            Me.GridEX1.RootTable.Columns("OutQty").FormatString = "N2"
            Me.GridEX1.RootTable.Columns("BalanceQty").FormatString = "N2"
            Dim grpProject As New Janus.Windows.GridEX.GridEXGroup(Me.GridEX1.RootTable.Columns("Project"))
            Me.GridEX1.RootTable.Groups.Add(grpProject)
            Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Expanded
            Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            Me.GridEX1.RootTable.Columns("InQty").TotalFormatString = "N2"
            Me.GridEX1.RootTable.Columns("OutQty").TotalFormatString = "N2"
            Me.GridEX1.RootTable.Columns("BalanceQty").TotalFormatString = "N2"
            Dim i As Integer = 0
            Dim TotalQty As Double = 0
            Dim dr() As DataRow
            Dim strProject As String = String.Empty
            Dim dv As DataView = CType(Me.GridEX1.DataSource, DataView)
            For Each Row As DataRow In dv.Table.Rows
                If Not strProject.ToString = Row("Project").ToString Then
                    dr = dv.Table.Select("Project='" & Row("Project").ToString & "'")
                    TotalQty = 0
                    For Each r As DataRow In dr
                        TotalQty += (Val(r(10)) - Val(r(12))) 'InQty - OutQty
                        r.BeginEdit()
                        r("BalanceQty") = TotalQty
                        r.EndEdit()
                        strProject = Row("Project")
                        i += 1
                    Next
                    Row.AcceptChanges()
                End If
            Next
            Me.GridEX1.AutoSizeColumns()
            Me.CtrlGrdBar2_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
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
            Dim str As String
            str = "select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ""
            Dim dtlocation As DataTable
            dtlocation = GetDataTable(str)
            If IsDBNull(dtlocation.Rows(0).Item("Location_Id")) = True Then
                ShowErrorMessage("You Don't have rights to any location, Please Contact to the Authorized Person")
                Exit Sub
            End If
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
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
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading, Please Wait"
            Me.Controls.Add(lbl)
            lbl.BringToFront()

            _Project = Me.cmbProject.SelectedValue
            _Location = Me.cmbLocations.SelectedValue
            _DateFrom = Me.dtpFrom.Value
            _DateTo = Me.dtpTo.Value
            Dim str As String
            str = "select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ""
            Dim dtlocation As DataTable
            dtlocation = GetDataTable(str)
            If IsDBNull(dtlocation.Rows(0).Item("Location_Id")) = True Then
                ShowErrorMessage("You Don't have rights to any location, Please Contact to the Authorized Person")
                Exit Sub
            End If
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data :" & ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0
            id = Me.cmbProject.SelectedValue
            Me.cmbProject.DataSource = Nothing
            FillDropDown(Me.cmbProject, "Select CostCenterId, Name as CostCenter From tblDefCostCenter WHERE Active=1 Order By 2", True)
            Me.cmbProject.SelectedValue = id

            id = Me.cmbLocations.SelectedValue
            Me.cmbLocations.DataSource = Nothing
            FillDropDown(Me.cmbLocations, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order", True)
            Me.cmbLocations.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub dtpFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFrom.ValueChanged, dtpTo.ValueChanged
        Try
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & vbCrLf & "Stock Ledger  By Location" & vbCrLf & "Date From: " & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & vbCrLf & "Stock Ledger  By Location" & vbCrLf & "Date From: " & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class