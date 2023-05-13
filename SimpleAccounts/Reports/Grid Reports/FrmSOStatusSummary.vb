Public Class FrmSOStatusSummary
    Public Acid As Int16

    Sub FillGrid(Optional Condition As String = "")
        Try

            Dim strsql As String = String.Empty
            If Condition = "" Then
                strsql = " SELECT dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id, SUM(dbo.SalesOrderMasterTable.SalesOrderQty) AS SaleOrderQty, " _
                & " SUM(dbo.SalesMasterTable.SalesQty) AS SaleQty, SUM(dbo.SalesOrderMasterTable.SalesOrderQty - dbo.SalesMasterTable.SalesQty) AS Pending,  " _
                & " dbo.SalesOrderMasterTable.Status,dbo.SalesOrderMasterTable.OrderType as Type " _
                & " FROM dbo.SalesMasterTable INNER JOIN " _
                & " dbo.SalesOrderMasterTable ON dbo.SalesMasterTable.CustomerCode = dbo.SalesOrderMasterTable.VendorId INNER JOIN " _
                & "  dbo.vwCOADetail ON dbo.SalesOrderMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id  " _
               & " GROUP BY dbo.vwCOADetail.detail_title, dbo.SalesOrderMasterTable.Status,dbo.SalesOrderMasterTable.OrderType, dbo.vwCOADetail.coa_detail_id " _
               & " HAVING (dbo.SalesOrderMasterTable.Status = N'OPEN')"
            ElseIf Condition = "PO" Then

                strsql = " SELECT dbo.vwCOADetail.detail_title, dbo.vwCOADetail.coa_detail_id, SUM(dbo.PurchaseOrderMasterTable.SalesOrderQty) AS SaleOrderQty, " _
               & " SUM(dbo.ReceivingMasterTable.ReceivingQty) AS SaleQty, SUM(dbo.PurchaseOrderMasterTable.PurchaseOrderQty - dbo.ReceivingMasterTable.ReceivingQty) AS Pending,  " _
               & " dbo.PurchaseOrderMasterTable.Status,dbo.PurchaseOrderMasterTable.OrderType as Type " _
               & " FROM dbo.ReceivingMasterTable INNER JOIN " _
               & " dbo.PurchaseOrderMasterTable ON dbo.ReceivingMasterTable.Vendor = dbo.PurchaseOrderMasterTable.VendorId INNER JOIN " _
               & "  dbo.vwCOADetail ON dbo.PurchaseOrderMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id  " _
              & " GROUP BY dbo.vwCOADetail.detail_title, dbo.PurchaseOrderMasterTable.Status,dbo.PurchaseOrderMasterTable.OrderType, dbo.vwCOADetail.coa_detail_id " _
              & " HAVING (dbo.PurchaseOrderMasterTable.Status = N'OPEN')"

            End If
            Dim dt As DataTable = GetDataTable(strsql)
            Me.GrdSOSummary.DataSource = dt
            Me.GrdSOSummary.RetrieveStructure()
            Me.GrdSOSummary.RootTable.Columns(1).Visible = False
            Me.GrdSOSummary.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GrdSOSummary_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdSOSummary.DoubleClick
        Try
            Acid = GrdSOSummary.GetRow.Cells(1).Value
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Close()
        End Try
    End Sub
    Private Sub FrmSOStatusSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillGrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class