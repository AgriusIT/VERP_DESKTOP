Public Class frmGrdRptSOPlanStatusDetail

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub FilGrid()
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Select SalesOrderMasterTable.SalesOrderId, IsNull(PlanDt.PlanDetailId,0) as PlanDetailId, SalesOrderNo as [SO No], SalesOrderDate as [SO Date], '' as  [Status], vwCOADetail.detail_title as [Customer],ArticleDefView.ArticleDescription as [Description], PlanDt.PlanNo, PlanDt.PlanDate, tblDefPlanLevel.PLevelName as [Level], PlanDt.Comments, IsNull(SalesOrderDetailTable.Qty,0) as SO_Qty,(IsNull(DC.DC_Qty,0)+IsNull(Inv.Inv_Qty,0)) as [Delivered],  (IsNull(SalesOrderDetailTable.Qty,0)-(IsNull(DC.DC_Qty,0)+IsNull(Inv.Inv_Qty,0))) as [Balance],  IsNull(PlanDt.PlanQty,0) as PlanQty, IsNull(ProdDt.ProdQty,0) as ProdQty, (IsNull(PlanDt.PlanQty,0)-IsNull(ProdDt.ProdQty,0)) as Dif  From SalesOrderMasterTable INNER JOIN SalesOrderDetailTable on SalesOrderMasterTable.SalesOrderId = SalesOrderDetailTable.SalesOrderId LEFT OUTER JOIN vwCOADetail on vwCOADetail.coa_detail_id = SalesOrderMasterTable.VendorId INNER JOIN ArticleDefView on ArticleDefView.ArticleId = SalesOrderDetailTable.ArticleDefId " _
                    & " LEFT OUTER JOIN (Select PlanNo, PlanDate, SODetailId, SOId, Qty as PlanQty, CustomerId,PlanDetailId,PLevelID,ArticleDefId,Comments From PlanMasterTable INNER JOIN PlanDetailTable on PlanDetailTable.PlanId = PlanMasterTable.PlanId WHERE IsNull(SODetailId,0) <> 0 AND IsNull(SOID,0) <> 0) as " _
                    & " PLanDt on PlanDt.SODetailId = SalesOrderDetailTable.SalesOrderDetailId AND PlanDt.ArticleDefId = SalesOrderDetailTable.ArticleDefId " _
                    & " AND PlanDt.SOId = SalesOrderDetailTable.SalesOrderId AND PlanDt.CustomerId = SalesOrderMasterTable.VendorId " _
                    & " LEFT OUTER JOIN (Select PlanDetailId,  SUM(IsNull(Qty,0)) as ProdQty From ProductionMasterTable INNER JOIN ProductionDetailTable on ProductionDetailTable.Production_Id = ProductionMasterTable.Production_Id WHERE IsNull(PlanDetailId,0) <> 0 Group By  PlanDetailId) as " _
                    & " ProdDt on ProdDt.PlanDetailId = PlanDt.PlanDetailId " _
                    & " LEFT OUTER JOIN tblDefPlanLevel on tblDefPlanLevel.PLevelID = PlanDt.PLevelID " _
                    & " LEFT OUTER JOIN (Select IsNull(SO_ID,0) as SO_ID,ArticleDefId, SUM(Qty) as DC_Qty From DeliveryChalanDetailTable WHERE IsNull(SO_ID,0) <> 0 Group By IsNull(SO_ID,0), ArticleDefId) as DC on DC.SO_ID = SalesOrderDetailTable.SalesOrderId AND DC.ArticleDefID = SalesOrderDetailTable.ArticleDefID " _
                    & " LEFT OUTER JOIN (Select IsNull(SO_ID,0) as SO_ID,ArticleDefId, SUM(Qty) as Inv_Qty From SalesDetailTable WHERE IsNull(SO_ID,0) <> 0  AND IsNull(SO_ID,0) not in(Select Distinct IsNull(SO_ID,0) as SO_ID From DeliveryChalanDetailTable) Group By IsNull(SO_ID,0), ArticleDefId) as Inv on Inv.SO_ID = SalesOrderDetailTable.SalesOrderId AND Inv.ArticleDefID = SalesOrderDetailTable.ArticleDefID "
            strSQL += " WHERE IsNull(SalesOrderMasterTable.SalesOrderId,0) <> 0"
            If Me.dtpFromDate.Checked = True Then
                strSQL += " AND (Convert(varchar,SalesOrderMasterTable.SalesOrderDate,102) >= Convert(dateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)) "
            End If
            If Me.dtpToDate.Checked = True Then
                strSQL += " AND (Convert(varchar,SalesOrderMasterTable.SalesOrderDate,102) <= Convert(dateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) "
            End If
            If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then
                strSQL += " AND dbo.SalesOrderMasterTable.VendorID=" & Me.cmbVendor.Value & ""
            End If
            Dim dtData As New DataTable
            dtData = GetDataTable(strSQL)

            dtData.Columns("Status").Expression = "IIF( IsNull([Delivered],0) >= IsNull([SO_Qty],0),'Closed','Open') "
            dtData.AcceptChanges()
            Me.grdReport.DataSource = dtData
            Me.grdReport.RetrieveStructure()
            'SO_Qty
            'PlanQty
            'ProdQty
            'Dif
            Me.grdReport.RootTable.Columns("SalesOrderID").Visible = False
            Me.grdReport.RootTable.Columns("PlanDetailId").Visible = False

            Me.grdReport.RootTable.Columns("SO_Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("PlanQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("ProdQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Dif").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Delivered").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdReport.RootTable.Columns("SO_Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("PlanQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("ProdQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Dif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Delivered").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdReport.RootTable.Columns("SO_Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("PlanQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("ProdQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Dif").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Delivered").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdReport.RootTable.Columns("SO_Qty").FormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("PlanQty").FormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("ProdQty").FormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("Dif").FormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("Delivered").FormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInQty

            Me.grdReport.RootTable.Columns("SO_Qty").TotalFormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("PlanQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("ProdQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("Dif").TotalFormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("Delivered").TotalFormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInQty

            Me.grdReport.RootTable.Columns("SO Date").FormatString = "dd/MMM/yyyy"
            Me.grdReport.RootTable.Columns("PlanDate").FormatString = "dd/MMM/yyyy"


            CtrlGrdBar2_Load(Nothing, Nothing)
            Me.grdReport.AutoSizeColumns()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            FilGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub frmGrdRptSOPlanStatusDetail_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.dtpFromDate.Value = Date.Now
            Me.dtpToDate.Value = Date.Now
            Me.dtpFromDate.Checked = False
            Me.dtpToDate.Checked = False
            FillCombo()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombo()
        Try
            FillUltraDropDown(Me.cmbVendor, "Select coa_detail_id, detail_title as [Name],detail_code as [Code], Account_Type as [Ac Type] From vwCOADetail WHERE Account_Type in('Customer') AND detail_title <> '' Order By Detail_title asc")
            Me.cmbVendor.Rows(0).Activate()
            Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbVendor.Value
            FillCombo()
            Me.cmbVendor.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdReport.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdReport.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grdReport.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class