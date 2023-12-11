Public Class frmRptSalesOrderDetail
    Public SONO As String
    Public ReportName As String = String.Empty
    Public Enum enmReportList
        SOHistory
        SOPlanHistory
    End Enum
    Private Sub frmRptSalesOrderDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim strSQL As String = String.Empty
            If ReportName = String.Empty Or IIf(ReportName = "", 0, ReportName) = enmReportList.SOHistory Then
                strSQL = " SP_SalesOrderDetailHistory '" & SONO & "' "
                Dim dt As DataTable = GetDataTable(strSQL)
                dt.AcceptChanges()
                Me.GridEX1.DataSource = dt
                Me.GridEX1.RetrieveStructure()
                Me.GridEX1.AutoSizeColumns()
                Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Me.GridEX1.RecordNavigator = True
                Me.GridEX1.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.GridEX1.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Else
                strSQL = String.Empty
                strSQL = "SP_SOAndPlanStatus " & Val(SONO) & ""
                Dim dt As DataTable = GetDataTable(strSQL)
                dt.AcceptChanges()
                Me.GridEX1.DataSource = dt
                Me.GridEX1.RetrieveStructure()

                Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Me.GridEX1.RecordNavigator = True
                Me.GridEX1.RootTable.Columns("SalesOrderId").Visible = False
                Me.GridEX1.RootTable.Columns("PlanDetailId").Visible = False

                Me.GridEX1.RootTable.Columns("SO_Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.GridEX1.RootTable.Columns("PlanQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.GridEX1.RootTable.Columns("ProdQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.GridEX1.RootTable.Columns("Dif").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.GridEX1.RootTable.Columns("Delivered").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                Me.GridEX1.RootTable.Columns("SO_Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns("PlanQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns("ProdQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns("Dif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns("Delivered").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.GridEX1.RootTable.Columns("SO_Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns("PlanQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns("ProdQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns("Dif").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns("Delivered").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.GridEX1.RootTable.Columns("SO_Qty").FormatString = "N" & DecimalPointInQty
                Me.GridEX1.RootTable.Columns("PlanQty").FormatString = "N" & DecimalPointInQty
                Me.GridEX1.RootTable.Columns("ProdQty").FormatString = "N" & DecimalPointInQty
                Me.GridEX1.RootTable.Columns("Dif").FormatString = "N" & DecimalPointInQty
                Me.GridEX1.RootTable.Columns("Delivered").FormatString = "N" & DecimalPointInQty

                Me.GridEX1.RootTable.Columns("SO_Qty").TotalFormatString = "N" & DecimalPointInQty
                Me.GridEX1.RootTable.Columns("PlanQty").TotalFormatString = "N" & DecimalPointInQty
                Me.GridEX1.RootTable.Columns("ProdQty").TotalFormatString = "N" & DecimalPointInQty
                Me.GridEX1.RootTable.Columns("Dif").TotalFormatString = "N" & DecimalPointInQty
                Me.GridEX1.RootTable.Columns("Delivered").FormatString = "N" & DecimalPointInQty

                Me.GridEX1.RootTable.Columns("SO Date").FormatString = "dd/MMM/yyyy"
                Me.GridEX1.RootTable.Columns("PlanDate").FormatString = "dd/MMM/yyyy"


                Me.GridEX1.AutoSizeColumns()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub frmRptSalesOrderDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Try

    '        Me.GridEX1.DataSource = GetDataTable("getsodetails")
    '        Me.GridEX1.RetrieveStructure()
    '        Me.GridEX1.AutoSizeColumns()
    '        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
    '        Me.GridEX1.RecordNavigator = True
    '        Me.GridEX1.RootTable.Columns("Order Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.GridEX1.RootTable.Columns("Deliver Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.GridEX1.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Dim grp As New Janus.Windows.GridEX.GridEXGroup
    '        grp.Column = Me.GridEX1.RootTable.Columns(0)
    '        grp.SortOrder = Janus.Windows.GridEX.SortOrder.Descending
    '        Me.GridEX1.RootTable.Groups.Add(grp)
    '        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical)
    '    End Try
    'End Sub


End Class