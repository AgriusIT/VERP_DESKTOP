Imports SBDal
Imports SBModel
Imports System.Data
Public Class frmGrdProductionPlaning
    Enum enmProductionPlan
        Id = 0
        ArticleId = 1
        ArticleCode = 2
        ArticleDescription = 3
        ArticleSizeName = 4
        ArticleColorName = 5
        Opening = 6
        Production = 7
        DeliveredToProduction = 8
        Balance = 9
        BalaceAfterDispatch = 10
        TotalDispatch = 11
        Count = 12
    End Enum
    Dim VoucherFormat As String = String.Empty
    Private Function GetProduction() As DataTable
        Try
            Dim dt As DataTable = GetDataTable("SP_ProductionPlaning")
            If dt IsNot Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetPendingOrders() As DataTable
        Try
            Dim dt As DataTable
            If VoucherFormat = "Yearly" Then
                dt = GetDataTable("Select SalesOrderId, Convert(Varchar, Convert(int, RIGHT(SalesOrderNo,5))) +'-'+ Convert(varchar, ISNULL(tblCustomer.CustomerCode,'')) as SalesOrderNo From SalesOrderMasterTable LEFT OUTER JOIN tblCustomer On tblCustomer.AccountId = SalesOrderMasterTable.VendorId WHERE Status='Open' AND ISNULL(Posted,0) <> 1")
            ElseIf VoucherFormat = "Monthly" Then
                dt = GetDataTable("Select SalesOrderId, Convert(Varchar, Convert(int, RIGHT(SalesOrderNo,4))) +'-'+ ISNULL(tblCustomer.CustomerCode,'') as SalesOrderNo From SalesOrderMasterTable LEFT OUTER JOIN tblCustomer On tblCustomer.AccountId = SalesOrderMasterTable.VendorId WHERE Status='Open' AND ISNULL(Posted,0) <> 1")
            ElseIf VoucherFormat = "Normal" Then
                dt = GetDataTable("Select SalesOrderId, Convert(Varchar, Convert(int, RIGHT(SalesOrderNo,6))) +'-'+ ISNULL(tblCustomer.CustomerCode,'') as SalesOrderNo From SalesOrderMasterTable LEFT OUTER JOIN tblCustomer On tblCustomer.AccountId = SalesOrderMasterTable.VendorId WHERE Status='Open' AND ISNULL(Posted,0) <> 1")
            Else
                dt = GetDataTable("Select SalesOrderId, Convert(Varchar, Convert(int, RIGHT(SalesOrderNo,6))) +'-'+ ISNULL(tblCustomer.CustomerCode,'') as SalesOrderNo From SalesOrderMasterTable LEFT OUTER JOIN tblCustomer On tblCustomer.AccountId = SalesOrderMasterTable.VendorId WHERE Status='Open' AND ISNULL(Posted,0) <> 1")
            End If
            If (dt IsNot Nothing) Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function FillGrid()
        Try
            FillGrid = Nothing
            'Me.GridEX1.DataSource = GetProduction()
            Dim dt As DataTable = GetProduction() 'CType(Me.GridEX1.DataSource, DataTable)
            dt.Columns.Add(enmProductionPlan.Balance.ToString, GetType(System.Double))
            dt.Columns.Add(enmProductionPlan.BalaceAfterDispatch.ToString, GetType(System.Double))
            dt.Columns.Add(enmProductionPlan.TotalDispatch.ToString, GetType(System.Double))
            For Each Row As DataRow In Me.GetPendingOrders.Rows
                If Not Me.GetPendingOrders.Columns.Contains(Row(1)) Then
                    dt.Columns.Add(Row(0), GetType(System.Int16), Row(0))
                    dt.Columns.Add(Row(1), GetType(System.Double))
                End If
            Next
            dt.AcceptChanges()
            For Each r As DataRow In dt.Rows
                For i As Integer = enmProductionPlan.Count + 1 To dt.Columns.Count - 1 Step 2
                    r.BeginEdit()
                    r(i) = 0
                    r.EndEdit()
                Next
            Next
            dt.AcceptChanges()
            Dim dtData As DataTable = GetDataTable("Select SalesOrderDetailTable.SalesOrderId, ArticleDefId, ISNULL(SUM((ISNULL(Qty,0)-ISNULL(DeliveredQty,0))+ISNULL(SchemeQty,0)),0) as Qty From SalesOrderDetailTable INNER JOIN SalesOrderMasterTable On SalesOrderMasterTable.SalesOrderId= SalesOrderDetailTable.SalesOrderId WHERE Status='Open' Group By ArticleDefId, SalesOrderDetailTable.SalesOrderId Having (SUM((ISNULL(Qty,0)-ISNULL(DeliveredQty,0))+ISNULL(SchemeQty,0))) > 0 ")
            Dim dr() As DataRow
            Dim ContColumns As Integer = 0
            For Each Row As DataRow In dt.Rows
                dr = dtData.Select("ArticleDefId=" & Row(enmProductionPlan.Id) & "")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drfound As DataRow In dr
                            Row.BeginEdit()
                            Row(dt.Columns.IndexOf(drfound(0)) + 1) = drfound(2)
                            Row.EndEdit()
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()
            dt.Columns(enmProductionPlan.Balance).Expression = "((Opening+Production)-Delivered)"
            Dim strTotalOrder As String = String.Empty
            For col As Integer = enmProductionPlan.Count + 1 To dt.Columns.Count - 1 Step 2
                If strTotalOrder.Length > 1 Then
                    strTotalOrder = "[" & dt.Columns(col).ColumnName & "]" & "+" & strTotalOrder
                Else
                    strTotalOrder = "[" & dt.Columns(col).ColumnName & "]"
                End If
            Next
            dt.Columns(enmProductionPlan.TotalDispatch).Expression = strTotalOrder.ToString
            dt.Columns(enmProductionPlan.BalaceAfterDispatch).Expression = "Balance-TotalDispatch"
            dt.AcceptChanges()
            GridEX1.DataSource = dt
            Me.GridEX1.RetrieveStructure()
            For col As Integer = enmProductionPlan.Count To Me.GridEX1.RootTable.Columns.Count - 2 Step 2
                Me.GridEX1.RootTable.Columns(col).Visible = False
                Me.GridEX1.RootTable.Columns(col + 1).AllowSort = False
            Next
            Me.GridEX1.RootTable.Columns(enmProductionPlan.Balance).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.GridEX1.RootTable.Columns(enmProductionPlan.BalaceAfterDispatch).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.GridEX1.RootTable.Columns(enmProductionPlan.TotalDispatch).EditType = Janus.Windows.GridEX.EditType.TextBox
            ApplyGridSettings()
            CtrlGrdBar1_Load(Nothing, Nothing)
            Return FillGrid
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmGrdProductionPlaning_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmGrdProductionPlaning_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            VoucherFormat = GetConfigValue("VoucherNo").ToString
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ApplyGridSettings()
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                If col.Index <> enmProductionPlan.Id AndAlso col.Index <> enmProductionPlan.ArticleId AndAlso col.Index <> enmProductionPlan.ArticleCode AndAlso col.Index <> enmProductionPlan.ArticleDescription AndAlso col.Index <> enmProductionPlan.ArticleColorName AndAlso col.Index <> enmProductionPlan.ArticleSizeName AndAlso col.Index <> enmProductionPlan.Count Then
                    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    col.FormatString = "N0"
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.TotalFormatString = "N0"
                End If
            Next
            Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.GridEX1.RootTable.Columns(0).Visible = False
            Me.GridEX1.RootTable.Columns(1).Visible = False
            Me.GridEX1.AutoSizeColumns()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim lbl As New Label
        Try
            lbl.Visible = True
            lbl.AutoSize = False
            lbl.Text = "Loading please wait ...."
            Application.DoEvents()
            lbl.Dock = DockStyle.Fill
            Me.Controls.Add(lbl)
            lbl.BringToFront()
            'If BackgroundWorker1.IsBusy Then Exit Sub
            'BackgroundWorker1.RunWorkerAsync()
            'Do While BackgroundWorker1.IsBusy
            '    Application.DoEvents()
            'Loop
            FillGrid()
        Catch ex As Exception
        Finally
            lbl.Visible = False
            Me.Controls.Remove(lbl)
        End Try
    End Sub

    'Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
    '    Try
    '        FillGrid()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub frmGrdProductionPlaning_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim lbl As New Label
        Try
            lbl.AutoSize = False
            lbl.Text = "Loading..."
            lbl.Dock = DockStyle.Fill
            Me.Controls.Add(lbl)
            lbl.BringToFront()
            'If BackgroundWorker1.IsBusy Then Exit Sub
            'BackgroundWorker1.RunWorkerAsync()
            'Do While BackgroundWorker1.IsBusy
            '    Application.DoEvents()
            'Loop
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage("Error occured while loading data." & ex.Message)
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Stock Position"

            'Me.CtrlGrdBar1.txtGridTitle.Text = "Min And Max Price Sales Detail"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub
End Class