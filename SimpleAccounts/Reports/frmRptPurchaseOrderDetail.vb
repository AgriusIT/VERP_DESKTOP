Public Class frmRptPurchaseOrderDetail
    Public POId As String
    Private Sub frmRptPurchaseOrderDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim str As String = String.Empty
            str = "SP_PurchaseOrderHistory '" & POId & "'"
            Dim Dt As DataTable = GetDataTable(str)
            Dt.AcceptChanges()
            Me.grdPO.DataSource = Dt
            Me.grdPO.RetrieveStructure()
            Me.grdPO.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdPO.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdPO.AutoSizeColumns()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class