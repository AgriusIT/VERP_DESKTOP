Public Class RptGrdTopCustomers
    Dim ObjDa As OleDb.OleDbDataAdapter
    Private Sub BindingDataTopCustomer()
        If Me.txtTop.Text = "" Then
            Me.txtTop.Focus()
            Exit Sub
        End If
        Dim totalAmount As Integer
        Dim totAmount As Integer
        Dim ByQtyValue As String = String.Empty
        Dim itemFilter
        If Me.CheckBox1.Checked = True Then
            itemFilter = ""
        Else
            itemFilter = " AND SalesDetailTable.ArticleDefId=" & Me.cmbItems.SelectedValue
        End If
        If Me.RadioButton1.Checked = True Then
            ByQtyValue = "Qty"
        Else
            ByQtyValue = "Value"
        End If
        If Me.RadioButton2.Checked = True Then
            ByQtyValue = "Value"
        Else
            ByQtyValue = "Qty"
        End If
        Dim ObjDt As New DataTable
        Try
            'Dim FilterItem As String
            'If Me.CheckBox1.Checked = True Then
            '    FilterItem = ""
            'Else
            '    FilterItem = " " & Me.cmbItems.SelectedValue
            'End If
            ' Dim SqlStr As String = "TopCustomers '" & IIf(Me.RadioButton1.Checked, "Qty", "Value") & "'"
            Dim strSQL As String = "SELECT TOP " & Me.txtTop.Text & " vwCOADetail.detail_title, " & _
" case when '" & ByQtyValue & "'= 'Qty'  then  " & _
"            SUM(ISNULL(SalesDetailTable.Qty, 0)) " & _
" else " & _
"            SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0)) " & _
"            End " & _
" as Value, 0 as Contribution " & _
" FROM SalesDetailTable INNER JOIN vwCOADetail INNER JOIN " & _
"                      SalesMasterTable ON vwCOADetail.coa_detail_id = SalesMasterTable.CustomerCode ON  " & _
"                      SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN " & _
"                      ArticleDefTable ON SalesDetailTable.ArticleDefId = ArticleDefTable.ArticleId " & _
" WHERE (Convert(varchar, SalesMasterTable.SalesDate,102) BetWeen Convert(Datetime, '" & Me.dtpDateFrom.Value & "', 102) AND Convert(Datetime, '" & Me.dtpDateTo.Value & "', 102)) AND ArticleDefTable.Active=1 "
            strSQL = strSQL & "" & itemFilter & " GROUP BY vwCOADetail.detail_title ORDER BY 2 DESC "


            ObjDa = New OleDb.OleDbDataAdapter(strSQL, Con)
            ObjDa.Fill(ObjDt)
            Me.grdTopCustomer.DataSource = ObjDt
            Me.grdTopCustomer.RetrieveStructure()

            Me.grdTopCustomer.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True

            Me.grdTopCustomer.RootTable.Columns(0).Caption = "Customer Name"
            Me.grdTopCustomer.RootTable.Columns(1).Caption = "Value"
            Me.grdTopCustomer.RootTable.Columns(2).Caption = "Contribution"

            Me.grdTopCustomer.RootTable.Columns(0).Width = 350
            Me.grdTopCustomer.RootTable.Columns(1).Width = 100
            Me.grdTopCustomer.RootTable.Columns(2).Width = 100


            For i As Integer = 0 To grdTopCustomer.RecordCount - 1
                totalAmount = Val(totalAmount) + Val(grdTopCustomer.GetRows(i).Cells(1).Value)
            Next
            totAmount = totalAmount
            For Each r As Janus.Windows.GridEX.GridEXRow In grdTopCustomer.GetRows
                r.BeginEdit()
                r.Cells("Contribution").Value = ((Val(r.Cells(1).Value * 100)) / Val(totAmount))
                r.EndEdit()
            Next
            Me.grdTopCustomer.RootTable.Columns(1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdTopCustomer.RootTable.Columns(2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdTopCustomer.RootTable.Columns(1).FormatString = "N"
            Me.grdTopCustomer.RootTable.Columns(2).FormatString = "N"
            Me.grdTopCustomer.RootTable.Columns(1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdTopCustomer.RootTable.Columns(2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdTopCustomer.RootTable.Columns(1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdTopCustomer.RootTable.Columns(2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum




        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            ObjDt = Nothing
        End Try
    End Sub

    Private Sub RptGrdTopCustomers_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub RptGrdStoreIssuenceSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dtpDateFrom.Value = Now.ToShortDateString
        Me.dtpDateTo.Value.ToShortDateString()
        Me.txtTop.Text = "10"
        Me.RadioButton2.Checked = True
        Me.dtpDateFrom.Value = Now.ToShortDateString
        Me.dtpDateTo.Value = Now.ToShortDateString
        Me.cmbPeriod.Text = "Current Month"
        BindingItems()
    End Sub
    Private Sub BindingItems()
        Dim sqlstr As String = "Select ArticleId, ArticleDescription From ArticleDefTable WHERE Active=1 "
        FillDropDown(Me.cmbItems, sqlstr, True)
    End Sub
    Private Sub bntGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntGenerate.Click
        BindingDataTopCustomer()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHeader.Click

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0
            id = Me.cmbItems.SelectedValue
            Dim sqlstr As String = "Select ArticleId, ArticleDescription From ArticleDefTable WHERE Active=1 "
            FillDropDown(Me.cmbItems, sqlstr, True)
            Me.cmbItems.SelectedValue = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Me.CtrlGrdBar1.txtGridTitle.Text = "Top Customer"
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpDateFrom.Value = Date.Today
            Me.dtpDateTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpDateFrom.Value = Date.Today.AddDays(-1)
            Me.dtpDateTo.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpDateFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpDateTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpDateFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpDateTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpDateFrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpDateTo.Value = Date.Today
        End If
    End Sub

    Private Sub dtpDateFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateFrom.ValueChanged

    End Sub

    Private Sub txtTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTop.TextChanged

    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged

    End Sub
End Class