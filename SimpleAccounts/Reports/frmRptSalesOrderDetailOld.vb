Public Class frmRptSalesOrderDetailOld
    Public SONO As String
    Private Sub frmRptSalesOrderDetailOld_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Me.GridEX1.DataSource = GetDataTable("getsodetails")
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.AutoSizeColumns()
            Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.RecordNavigator = True
            Me.GridEX1.RootTable.Columns("Order Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("Deliver Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Dim grp As New Janus.Windows.GridEX.GridEXGroup
            grp.Column = Me.GridEX1.RootTable.Columns(0)
            grp.SortOrder = Janus.Windows.GridEX.SortOrder.Descending
            Me.GridEX1.RootTable.Groups.Add(grp)
            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class