''27-Dec-2013 R:M-2   Imran Ali             Rounding Figure
''29-Dec-2013 Task:2359  Imran Ali  software Problem to Mr.Aziz
''11-Feb-2014     TASKM18 Imran Ali Sorting Out
''11-Jun-2015 Task# 3-11-06-2015 Ahmad Sharif: Fill items combox ,Add search by code and by name
''22-6-2015 TASKM226151 Imran Ali Runnting Total Problem Fixed
''20-10-2015 TASK1191015 Muhammad Ameen: Added new columns(Combination, Size, Pack, Price, UOM e.t.c) to Combobox on Item Ledger.

Imports SBModel
Imports System.Data
Public Class frmGrdStockMovement
    Dim IsOpenedForm As Boolean = False

    Private _ItemList As New List(Of SBModel.ArticleList)

    Private Sub frmGrdArticleLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmGrdArticleLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Me.cmbPeriod.Text = "Current Month"
            IsOpenedForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub grdArticles_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
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
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdArticleLedger)
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

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try  

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try
            ''End Task# 3-11-06-2015
            Dim strQuery As String = String.Empty
            strQuery = "StockMovementReport '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00.000") & "', '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "'"

            Dim dt As New DataTable
            dt = GetDataTable(strQuery)
            dt.Columns("ClosingQuantity").Expression = "((OpeningQuantity+ReceiveQuantity)-IssuedQuantity)"
            dt.Columns("ClosingValue").Expression = "((OpeningValue+ReceiveValue)-IssuanceValue)"
            Me.GridEX1.DataSource = dt
            GridEX1.RetrieveStructure()


            GridEX1.RootTable.Columns("Item Code").Caption = "Item Code"
            GridEX1.RootTable.Columns("OpeningQuantity").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("OpeningValue").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("ReceiveQuantity").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("ReceiveValue").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("IssuedQuantity").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("IssuanceValue").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("ClosingQuantity").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("ClosingValue").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'GridEX1.RootTable.Columns("Out_Qty").Caption = "Out Stock"

            'GridEX1.RootTable.Columns("In_Amount").Caption = "Recv Stock Val"
            'GridEX1.RootTable.Columns("Out_Amount").Caption = "Out Stock Val"


            For c As Integer = 0 To Me.GridEX1.RootTable.Columns.Count - 1
                Me.GridEX1.RootTable.Columns(c).AllowSort = False
            Next
            CtrlGrdBar1_Load(Nothing, Nothing)
            'Me.GridEX1.AutoSizeColumns()


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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Item Ledger" & Chr(10) & "From Date: " & dtpFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpTo.Value.ToString("dd-MM-yyyy").ToString & ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class