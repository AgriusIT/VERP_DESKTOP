''05-Aug-2014 Task:2769 Imran Ali Add new report CMFA Summary (Ravi)
Public Class frmGrdRptCMFASummary

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
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            If Condition = "Customer" Then
                FillUltraDropDown(Me.cmbVendor, "Select coa_detail_id, detail_title, detail_code From vwCOADetail WHERE Account_Type='Customer' AND detail_title <> ''")
                If Me.cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(1).Width = 300
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(2).Width = 150
                End If
                Me.cmbVendor.Rows(0).Activate()
            ElseIf Condition = "Employee" Then
                Dim strSQL As String = "Select User_Id, User_Name, User_Code From tblUser"
                Dim dtUser As New DataTable
                dtUser = GetDataTable(strSQL)
                If dtUser IsNot Nothing Then
                    If dtUser.Rows.Count > 0 Then
                        For Each r As DataRow In dtUser.Rows
                            r.BeginEdit()
                            r.Item("User_Name") = Decrypt(r.Item("User_Name").ToString)
                            r.Item("User_Code") = Decrypt(r.Item("User_Code").ToString)
                            r.EndEdit()
                        Next
                    End If
                End If
                Dim dr As DataRow
                dr = dtUser.NewRow
                dr(0) = Convert.ToInt32(0)
                dr(1) = ".... Select Any Value ...."
                dr(2) = String.Empty
                dtUser.Rows.InsertAt(dr, 0)
                dtUser.AcceptChanges()
                'FillDropDown(Me.cmbEmployee, "Select User_Id, User_Name, User_Code From tblUser")
                Me.cmbUser.ValueMember = "User_Id"
                Me.cmbUser.DisplayMember = "User_Name"
                Me.cmbUser.DataSource = dtUser
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0I
            id = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombos("Customer")
            Me.cmbVendor.Value = id
            id = Me.cmbUser.SelectedIndex
            FillCombos("Employee")
            Me.cmbUser.SelectedIndex = id

            'Me.txtDocFrom.Text = "CMFA-" & Date.Now.ToString("yy") & "-"
            'Me.txtDocTo.Text = "CMFA-" & Date.Now.ToString("yy") & "-"

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try

            FillGrid()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillGrid()
        Try

            Dim strSQL As String = String.Empty
            strSQL = "SP_CMFASummary '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "'"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.TableName = "Default"
            dt.Columns.Add("Net_Amount", GetType(System.Double))
            dt.Columns.Add("Contribution", GetType(System.Double))
            dt.Columns.Add("Cont_Percentage", GetType(System.Double))
            dt.Columns("Diff").Expression = "((Projected_Amount-Request_Amount)-PO_Amount)"
            dt.Columns("Net_Amount").Expression = "(ApprovedBudget - ((WHTaxPercent/100)*(ApprovedBudget + ((TaxPercent/100)*ApprovedBudget))))"
            dt.Columns("Contribution").Expression = "(Net_Amount-Projected_Amount)"
            dt.Columns("Cont_Percentage").Expression = "((Contribution/Net_Amount)*100)"
            Dim dv As New DataView
            dv.Table = dt

            dv.RowFilter = " DocNo <> ''"
            If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then
                dv.RowFilter += " AND CustomerCode ='" & Me.cmbVendor.Value & "'"
            End If
            If Me.cmbUser.SelectedIndex > 0 Then
                dv.RowFilter += " AND UserId=" & Me.cmbUser.SelectedValue & ""
            End If
            If Me.txtDocFrom.Text.Length > 1 Then
                dv.RowFilter += " AND DocNo >= '" & Me.txtDocFrom.Text.Replace("'", "''") & "'"
            End If
            If Me.txtDocTo.Text.Length > 1 Then
                dv.RowFilter += " AND DocNo <= '" & Me.txtDocTo.Text.Replace("'", "''") & "'"
            End If


            For Each r As DataRow In dt.Rows
                r.BeginEdit()
                r.Item("User_Name") = Decrypt(r.Item("User_Name").ToString)
                r.EndEdit()
            Next

            Me.grd.DataSource = dv.ToTable
            Me.grd.RetrieveStructure()
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

            Me.grd.RootTable.Columns("UserId").Visible = False
            Me.grd.RootTable.Columns("TaxPercent").Visible = False
            Me.grd.RootTable.Columns("WHTaxPercent").Visible = False
            Me.grd.RootTable.Columns("OPEX_Sale_Percent").Visible = False
            Me.grd.RootTable.Columns("ApprovedBudget").Visible = True
            Me.grd.RootTable.Columns("TotalAmount").Visible = False
            Me.grd.RootTable.Columns("CustomerCode").Visible = False
            Me.grd.RootTable.Columns("DocId").Visible = False
            Me.grd.RootTable.Columns("Net_Amount").Visible = False
            Me.grd.RootTable.Columns("DocDate").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("User_Name").Caption = "User"
            Me.grd.RootTable.Columns("Projected_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Request_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Expense_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("PO_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Sales_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Pur_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Contribution").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Diff").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("ApprovedBudget").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("Projected_Amount").Caption = "Projected"
            Me.grd.RootTable.Columns("Request_Amount").Caption = "Cash Request"
            Me.grd.RootTable.Columns("Expense_Amount").Caption = "Expense"
            Me.grd.RootTable.Columns("PO_Amount").Caption = "Purchase Order"
            Me.grd.RootTable.Columns("Sales_Amount").Caption = "Sales"
            Me.grd.RootTable.Columns("Pur_Amount").Caption = "Purchase"
            Me.grd.RootTable.Columns("Detail_Title").Caption = "Customer"
            Me.grd.RootTable.Columns("Cont_Percentage").Caption = "Cont %"


            Me.grd.RootTable.Columns("Projected_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Request_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Expense_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PO_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Sales_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Pur_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Contribution").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Cont_Percentage").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Projected_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Request_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Expense_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PO_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Sales_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Pur_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Contribution").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Cont_Percentage").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Projected_Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Request_Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Expense_Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("PO_Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Sales_Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Pur_Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Contribution").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Cont_Percentage").FormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("Projected_Amount").TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Request_Amount").TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Expense_Amount").TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("PO_Amount").TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Sales_Amount").TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Pur_Amount").TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Contribution").TotalFormatString = "N" & TotalAmountRounding
            'Me.grd.RootTable.Columns("Cont_Percentage").TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Diff").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Diff").TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Diff").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Diff").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ApprovedBudget").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("ApprovedBudget").TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("ApprovedBudget").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ApprovedBudget").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            CtrlGrdBar2_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
            Me.grd.FrozenColumns = 6
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try

            Me.cmbPeriod.Text = "Current Month"
            Me.txtDocFrom.Text = String.Empty
            Me.txtDocTo.Text = String.Empty
            If Not Me.cmbUser.SelectedIndex = -1 Then Me.cmbUser.SelectedIndex = 0
            If Not Me.cmbVendor.ActiveRow Is Nothing Then Me.cmbVendor.Rows(0).Activate()
            Me.dtpFromDate.Focus()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptCMFASummary_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            Me.cmbPeriod.Text = "Current Month"
            Me.txtDocFrom.Text = String.Empty '"CMFA-" & Date.Now.ToString("yy") & "-"
            Me.txtDocTo.Text = String.Empty '"CMFA-" & Date.Now.ToString("yy") & "-"
            FillCombos("Customer")
            FillCombos("Employee")
            If Not Me.cmbUser.SelectedIndex = -1 Then Me.cmbUser.SelectedIndex = 0
            If Not Me.cmbVendor.ActiveRow Is Nothing Then Me.cmbVendor.Rows(0).Activate()
            Me.dtpFromDate.Focus()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "CMFA Summary" & Chr(10) & "From Date: " & dtpFromDate.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpToDate.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub
End Class