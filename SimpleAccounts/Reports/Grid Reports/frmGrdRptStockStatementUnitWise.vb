Imports SBModel
Public Class frmGrdRptStockStatementUnitWise
    Public flgSelectedPurchase As Boolean = False
    Public flgSelectedPurchaseReturn As Boolean = False
    Public flgSelectedReceiving As Boolean = False
    Public flgSelectedSales As Boolean = False
    Public flgSelectedSalesReturn As Boolean = False
    Public flgSelectedDispatch As Boolean = False
    Public flgSelectedStoreIssuance As Boolean = False
    Public flgSelectedProduction As Boolean = False

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
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
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar2.mGridPrint.Enabled = True
                CtrlGrdBar2.mGridExport.Enabled = True
                CtrlGrdBar2.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptStockStatementUnitWise)
            Me.Visible = False
            CtrlGrdBar2.mGridPrint.Enabled = False
            CtrlGrdBar2.mGridExport.Enabled = False
            CtrlGrdBar2.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar2.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar2.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar2.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdRptStockStatementUnitWise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Me.cmbPeriod.Text = "Current Month"
            For i As Integer = 0 To Me.ListBox1.Items.Count - 1
                Me.ListBox1.SetSelected(i, True)
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillGrid()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT     dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleSizeName, " _
                     & " dbo.ArticleDefView.ArticleColorName, ArticleDefTable.Remarks, isnull(dbo.ArticleDefView.PackQty,1) as PackQty, Isnull(dbo.ArticleDefView.LargestPackQty,1) as LargestPackQty, ISNULL(Opening.OpeningQty, 0) AS OpeningQty,  " _
                     & " ISNULL(Opening.OpeningValue, 0) AS OpeningValue, ISNULL(Sales.SalesQty, 0) AS SalesQty, ISNULL(Sales.SalesValue, 0) AS SalesValue, ISNULL(Sales.SampleQty,  " _
                     & " 0) AS SampleQty, ISNULL(SalesReturn.SalesReturnQty, 0) AS SalesReturnQty, ISNULL(SalesReturn.SalesReturnValue, 0) AS SalesReturnValue,  " _
                     & " ISNULL(Purchase.PurchaseQty, 0) AS PurchaseQty, ISNULL(Purchase.PurchaseValue, 0) AS PurchaseValue, ISNULL(PurchaseReturn.PurchaseReturnQty, 0)  " _
                     & " AS PurchaseReturnQty, ISNULL(PurchaseReturn.PurchaseReturnValue, 0) AS PurchaseReturnValue, ISNULL(Production.ProductionQty, 0) AS ProductionQty,  " _
                     & " ISNULL(Production.ProductionValue, 0) AS ProductionValue, ISNULL(Dispatch.DispatchQty, 0) AS DispatchQty, ISNULL(Dispatch.DispatchValue, 0) AS DispatchValue,  " _
                     & " ISNULL(Issue.IssueQty, 0) AS IssueQty, ISNULL(Issue.IssueValue, 0) AS IssueValue, ISNULL(Receiving.ReceivingQty, 0) AS ReceivingQty,  " _
                     & " ISNULL(Receiving.ReceivingValue, 0) AS ReceivingValue " _
                     & " FROM  dbo.ArticleDefView LEFT OUTER JOIN " _
                     & " (SELECT dbo.StockDetailTable.ArticleDefId, SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) AS OpeningQty,  " _
                     & " SUM((ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) * ISNULL(dbo.StockDetailTable.Rate, 0)) AS OpeningValue " _
                     & " FROM dbo.StockMasterTable INNER JOIN " _
                     & " dbo.StockDetailTable ON dbo.StockMasterTable.StockTransId = dbo.StockDetailTable.StockTransId " _
                     & " WHERE (Convert(Varchar, DocDate, 102)  <  Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " _
                     & " GROUP BY dbo.StockDetailTable.ArticleDefId) AS Opening ON Opening.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                     & " (SELECT     dbo.SalesDetailTable.ArticleDefId, SUM(ISNULL(dbo.SalesDetailTable.Qty, 0)) AS SalesQty, SUM(ISNULL(dbo.SalesDetailTable.Qty, 0)  " _
                     & "  * ISNULL(dbo.SalesDetailTable.Price, 0)) AS SalesValue, SUM(ISNULL(dbo.SalesDetailTable.SampleQty, 0)) AS SampleQty " _
                     & " FROM          dbo.SalesDetailTable INNER JOIN " _
                     & " dbo.SalesMasterTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId " _
                     & " WHERE (Convert(Varchar, SalesDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
                     & " GROUP BY dbo.SalesDetailTable.ArticleDefId) AS Sales ON Sales.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                     & " (SELECT     dbo.SalesReturnDetailTable.ArticleDefId, SUM(ISNULL(dbo.SalesReturnDetailTable.Qty, 0)) AS SalesReturnQty,  " _
                     & " SUM(ISNULL(dbo.SalesReturnDetailTable.Qty, 0) * ISNULL(dbo.SalesReturnDetailTable.Price, 0)) AS SalesReturnValue " _
                     & " FROM          dbo.SalesReturnMasterTable INNER JOIN " _
                     & " dbo.SalesReturnDetailTable ON dbo.SalesReturnMasterTable.SalesReturnId = dbo.SalesReturnDetailTable.SalesReturnId " _
                     & " WHERE (Convert(Varchar, SalesReturnDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
                     & " GROUP BY dbo.SalesReturnDetailTable.ArticleDefId) AS SalesReturn ON SalesReturn.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                     & " (SELECT     dbo.ReceivingDetailTable.ArticleDefId, SUM(ISNULL(dbo.ReceivingDetailTable.Qty, 0)) AS PurchaseQty, SUM(ISNULL(dbo.ReceivingDetailTable.Qty, 0)  " _
                     & " * ISNULL(dbo.ReceivingDetailTable.Price, 0)) AS PurchaseValue " _
                     & " FROM          dbo.ReceivingDetailTable INNER JOIN " _
                     & " dbo.ReceivingMasterTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId " _
                     & " WHERE      (LEFT(dbo.ReceivingMasterTable.ReceivingNo, 3) = 'Pur') " _
                     & " And (Convert(Varchar, ReceivingDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
                     & " GROUP BY dbo.ReceivingDetailTable.ArticleDefId) AS Purchase ON Purchase.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                     & " (SELECT     ReceivingDetailTable_1.ArticleDefId, SUM(ISNULL(ReceivingDetailTable_1.Qty, 0)) AS ReceivingQty, SUM(ISNULL(ReceivingDetailTable_1.Qty, 0)  " _
                     & " * ISNULL(ReceivingDetailTable_1.Price, 0)) AS ReceivingValue " _
                     & " FROM          dbo.ReceivingDetailTable AS ReceivingDetailTable_1 INNER JOIN " _
                     & " dbo.ReceivingMasterTable AS ReceivingMasterTable_1 ON ReceivingMasterTable_1.ReceivingId = ReceivingDetailTable_1.ReceivingId " _
                     & " WHERE      (LEFT(ReceivingMasterTable_1.ReceivingNo, 3) <> 'Pur') " _
                     & " And (Convert(Varchar, ReceivingMasterTable_1.ReceivingDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
                     & " GROUP BY ReceivingDetailTable_1.ArticleDefId) AS Receiving ON Receiving.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                     & " (SELECT     dbo.PurchaseReturnDetailTable.ArticleDefId, SUM(ISNULL(dbo.PurchaseReturnDetailTable.Qty, 0)) AS PurchaseReturnQty,  " _
                     & " SUM(ISNULL(dbo.PurchaseReturnDetailTable.Qty, 0) * ISNULL(dbo.PurchaseReturnDetailTable.Price, 0)) AS PurchaseReturnValue " _
                     & " FROM dbo.PurchaseReturnDetailTable INNER JOIN " _
                     & " dbo.PurchaseReturnMasterTable ON dbo.PurchaseReturnMasterTable.PurchaseReturnId = dbo.PurchaseReturnDetailTable.PurchaseReturnId " _
                     & " WHERE (Convert(Varchar, PurchaseReturnDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
                     & " GROUP BY dbo.PurchaseReturnDetailTable.ArticleDefId) AS PurchaseReturn ON PurchaseReturn.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                     & " (SELECT     dbo.ProductionDetailTable.ArticledefID, SUM(ISNULL(dbo.ProductionDetailTable.Qty, 0)) AS ProductionQty, SUM(ISNULL(dbo.ProductionDetailTable.Qty, 0) * ISNULL(dbo.ProductionDetailTable.CurrentRate, 0)) AS ProductionValue " _
                     & " FROM          dbo.ProductionDetailTable INNER JOIN " _
                     & " dbo.ProductionMasterTable ON dbo.ProductionMasterTable.Production_ID = dbo.ProductionDetailTable.Production_ID " _
                     & " WHERE (Convert(Varchar, Production_Date, 102) BETWEEN Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
                     & " GROUP BY dbo.ProductionDetailTable.ArticledefID) AS Production ON Production.ArticledefID = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                     & " (SELECT     dbo.DispatchDetailTable.ArticleDefId, SUM(ISNULL(dbo.DispatchDetailTable.Qty, 0)) AS DispatchQty, SUM(ISNULL(dbo.DispatchDetailTable.Qty, 0)  " _
                     & " * ISNULL(dbo.DispatchDetailTable.Price, 0)) AS DispatchValue " _
                     & " FROM          dbo.DispatchDetailTable INNER JOIN " _
                     & " dbo.DispatchMasterTable ON dbo.DispatchMasterTable.DispatchId = dbo.DispatchDetailTable.DispatchId " _
                     & " WHERE      (LEFT(dbo.DispatchMasterTable.DispatchNo, 1) <> 'I') " _
                     & " And (Convert(Varchar, DispatchDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
                     & " GROUP BY dbo.DispatchDetailTable.ArticleDefId) AS Dispatch ON Dispatch.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
                     & " (SELECT     DispatchDetailTable_1.ArticleDefId, SUM(ISNULL(DispatchDetailTable_1.Qty, 0)) AS IssueQty, SUM(ISNULL(DispatchDetailTable_1.Qty, 0)  " _
                     & " * ISNULL(DispatchDetailTable_1.Price, 0)) AS IssueValue " _
                     & " FROM          dbo.DispatchDetailTable AS DispatchDetailTable_1 INNER JOIN " _
                     & " dbo.DispatchMasterTable AS DispatchMasterTable_1 ON DispatchMasterTable_1.DispatchId = DispatchDetailTable_1.DispatchId " _
                     & " WHERE      (LEFT(DispatchMasterTable_1.DispatchNo, 1) = 'I') " _
                     & " And (Convert(Varchar, DispatchDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
                     & " GROUP BY DispatchDetailTable_1.ArticleDefId) AS Issue ON Issue.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN ArticleDefTable on ArticleDefTable.ArticleId = ArticleDefView.ArticleId "

            Dim dt As New DataTable
            dt = GetDataTable(strSQL)

            dt.Columns.Add("OpeningMedium", GetType(System.Double)) 'Pack Qty
            dt.Columns.Add("OpeningLargest", GetType(System.Double)) ' Qty in Kgs
            dt.Columns.Add("OpeningSmallest", GetType(System.Double)) ' Loose Pack (GRM etc)

            dt.Columns.Add("PurchasesMedium", GetType(System.Double)) 'Pack Qty
            dt.Columns.Add("PurchaseLargest", GetType(System.Double)) ' Qty in Kgs
            dt.Columns.Add("PurchaseSmallest", GetType(System.Double)) ' Loose Pack (GRM etc)

            dt.Columns.Add("PurchasesReturnMedium", GetType(System.Double)) 'Pack Qty
            dt.Columns.Add("PurchaseReturnLargest", GetType(System.Double)) ' Qty in Kgs
            dt.Columns.Add("PurchaseReturnSmallest", GetType(System.Double)) ' Loose Pack (GRM etc).

            dt.Columns.Add("ReceivingMedium", GetType(System.Double)) 'Pack Qty
            dt.Columns.Add("ReceivingLargest", GetType(System.Double)) ' Qty in Kgs
            dt.Columns.Add("ReceivingSmallest", GetType(System.Double)) ' Loose Pack (GRM etc)

            dt.Columns.Add("SalesMedium", GetType(System.Double)) 'Pack Qty
            dt.Columns.Add("SalesLargest", GetType(System.Double)) ' Qty in Kgs
            dt.Columns.Add("SalesSmallest", GetType(System.Double)) ' Loose Pack (GRM etc)

            dt.Columns.Add("SalesReturnMedium", GetType(System.Double)) 'Pack Qty
            dt.Columns.Add("SalesReturnLargest", GetType(System.Double)) ' Qty in Kgs
            dt.Columns.Add("SalesReturnSmallest", GetType(System.Double)) ' Loose Pack (GRM etc)

            dt.Columns.Add("DispatchMedium", GetType(System.Double)) 'Pack Qty
            dt.Columns.Add("DispatchLargest", GetType(System.Double)) ' Qty in Kgs
            dt.Columns.Add("DispatchSmallest", GetType(System.Double)) ' Loose Pack (GRM etc)

            dt.Columns.Add("StoreIssuanceMedium", GetType(System.Double)) 'Pack Qty
            dt.Columns.Add("StoreIssuanceLargest", GetType(System.Double)) ' Qty in Kgs
            dt.Columns.Add("StoreIssuanceSmallest", GetType(System.Double)) ' Loose Pack (GRM etc)

            dt.Columns.Add("ProductionMedium", GetType(System.Double)) 'Pack Qty
            dt.Columns.Add("ProductionLargest", GetType(System.Double)) ' Qty in Kgs
            dt.Columns.Add("ProductionSmallest", GetType(System.Double)) ' Loose Pack (GRM etc)

            dt.Columns.Add("ClosingMedium", GetType(System.Double)) 'Pack Qty
            dt.Columns.Add("ClosingLargest", GetType(System.Double)) ' Qty in Kgs
            dt.Columns.Add("ClosingSmallest", GetType(System.Double)) ' Loose Pack (GRM etc)
            dt.Columns.Add("ClosingValue", GetType(System.Double)) ' Loose Pack (GRM etc)

            For Each row As DataRow In dt.Rows
                row.BeginEdit()
                row("OpeningMedium") = Microsoft.VisualBasic.Fix(row("OpeningQty") / row("LargestPackQty"))
                row("OpeningLargest") = Microsoft.VisualBasic.Fix((row("OpeningQty") - (row("OpeningMedium") * row("LargestPackQty"))) / row("PackQty"))
                row("OpeningSmallest") = row("OpeningQty") - ((row("OpeningMedium") * row("LargestPackQty")) + (row("OpeningLargest") * row("PackQty")))

                row("PurchasesMedium") = Microsoft.VisualBasic.Fix(row("PurchaseQty") / row("LargestPackQty"))
                row("PurchaseLargest") = Microsoft.VisualBasic.Fix((row("PurchaseQty") - (row("PurchasesMedium") * row("LargestPackQty"))) / row("PackQty"))
                row("PurchaseSmallest") = row("PurchaseQty") - ((row("PurchasesMedium") * row("LargestPackQty")) + (row("PurchaseLargest") * row("PackQty")))

                row("PurchasesReturnMedium") = Microsoft.VisualBasic.Fix(row("PurchaseReturnQty") / row("LargestPackQty"))
                row("PurchaseReturnLargest") = Microsoft.VisualBasic.Fix((row("PurchaseReturnQty") - (row("PurchasesReturnMedium") * row("LargestPackQty"))) / row("PackQty"))
                row("PurchaseReturnSmallest") = row("PurchaseReturnQty") - ((row("PurchasesReturnMedium") * row("LargestPackQty")) + (row("PurchaseReturnLargest") * row("PackQty")))

                row("ReceivingMedium") = Microsoft.VisualBasic.Fix(row("ReceivingQty") / row("LargestPackQty"))
                row("ReceivingLargest") = Microsoft.VisualBasic.Fix((row("ReceivingQty") - (row("ReceivingMedium") * row("LargestPackQty"))) / row("PackQty"))
                row("ReceivingSmallest") = row("ReceivingQty") - ((row("ReceivingMedium") * row("LargestPackQty")) + (row("ReceivingLargest") * row("PackQty")))

                row("SalesMedium") = Microsoft.VisualBasic.Fix(row("SalesQty") / row("LargestPackQty"))
                row("SalesLargest") = Microsoft.VisualBasic.Fix(row("SalesQty") - ((row("SalesMedium") * row("LargestPackQty"))) / row("PackQty"))
                row("SalesSmallest") = row("SalesQty") - ((row("SalesMedium") * row("LargestPackQty")) + (row("SalesLargest") * row("PackQty")))

                row("SalesReturnMedium") = Microsoft.VisualBasic.Fix(row("SalesReturnQty") / row("LargestPackQty"))
                row("SalesReturnLargest") = Microsoft.VisualBasic.Fix((row("SalesReturnQty") - (row("SalesReturnMedium") * row("LargestPackQty"))) / row("PackQty"))
                row("SalesReturnSmallest") = row("SalesReturnQty") - ((row("SalesReturnMedium") * row("LargestPackQty")) + (row("SalesReturnLargest") * row("PackQty")))

                row("DispatchMedium") = Microsoft.VisualBasic.Fix(row("DispatchQty") / row("LargestPackQty"))
                row("DispatchLargest") = Microsoft.VisualBasic.Fix((row("DispatchQty") - (row("DispatchMedium") * row("LargestPackQty"))) / row("PackQty"))
                row("DispatchSmallest") = row("SalesReturnQty") - ((row("DispatchMedium") * row("LargestPackQty")) + (row("DispatchLargest") * row("PackQty")))


                row("StoreIssuanceMedium") = Microsoft.VisualBasic.Fix(row("IssueQty") / row("LargestPackQty"))
                row("StoreIssuanceLargest") = Microsoft.VisualBasic.Fix((row("IssueQty") - (row("StoreIssuanceMedium") * row("LargestPackQty"))) / row("PackQty"))
                row("StoreIssuanceSmallest") = row("IssueQty") - ((row("StoreIssuanceMedium") * row("LargestPackQty")) + (row("StoreIssuanceLargest") * row("PackQty")))

                row("ProductionMedium") = Microsoft.VisualBasic.Fix(row("ProductionQty") / row("LargestPackQty"))
                row("ProductionLargest") = Microsoft.VisualBasic.Fix((row("ProductionQty") - (row("ProductionMedium") * row("LargestPackQty"))) / row("PackQty"))
                row("ProductionSmallest") = row("ProductionQty") - ((row("ProductionMedium") * row("LargestPackQty")) + (row("ProductionLargest") * row("PackQty")))

                '' Closing Request Against 863
                '' Calculation by row
                row("ClosingMedium") = Microsoft.VisualBasic.Fix(((row("OpeningQty") + row("PurchaseQty") + row("ProductionQty") + row("SalesReturnQty") + row("ReceivingQty")) - (row("SalesQty") + row("PurchaseReturnQty") + row("IssueQty") + row("DispatchQty"))) / row("LargestPackQty"))
                row("ClosingLargest") = Microsoft.VisualBasic.Fix((((row("OpeningQty") + row("PurchaseQty") + row("ProductionQty") + row("SalesReturnQty") + row("ReceivingQty")) - (row("SalesQty") + row("PurchaseReturnQty") + row("IssueQty") + row("DispatchQty"))) - (row("ClosingMedium") * row("LargestPackQty"))) / row("PackQty"))
                row("ClosingSmallest") = ((row("OpeningQty") + row("PurchaseQty") + row("ProductionQty") + row("SalesReturnQty") + row("ReceivingQty")) - (row("SalesQty") + row("PurchaseReturnQty") + row("IssueQty") + row("DispatchQty"))) - ((row("ClosingMedium") * row("LargestPackQty")) + (row("ClosingLargest") * row("PackQty")))

                row.EndEdit()
            Next

            '' Comments lines against request no 863
            'dt.Columns("ClosingMedium").Expression = "((OpeningMedium+PurchasesMedium+ProductionMedium+SalesReturnMedium+ReceivingMedium)-(SalesMedium+PurchasesReturnMedium+StoreIssuanceMedium+DispatchMedium))"
            'dt.Columns("ClosingLargest").Expression = "((OpeningLargest+PurchaseLargest+ProductionLargest+SalesReturnLargest+ReceivingLargest)-(SalesLargest+PurchaseReturnLargest+StoreIssuanceLargest+DispatchLargest))"
            'dt.Columns("ClosingSmallest").Expression = "((OpeningSmallest+PurchaseSmallest+ProductionSmallest+SalesReturnSmallest+ReceivingSmallest)-(SalesSmallest+PurchaseReturnSmallest+StoreIssuanceSmallest+DispatchSmallest))"
            dt.Columns("ClosingValue").Expression = "(OpeningValue+PurchaseValue+ProductionValue+SalesReturnValue+ReceivingValue)-(SalesValue+PurchaseReturnValue+IssueValue+DispatchValue)"

            dt.AcceptChanges()

            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSetting()

            CtrlGrdBar2_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.ToolStripProgressBar1.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Try


            FillGrid()
            Me.ToolStripProgressBar1.Visible = False
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub ApplyGridSetting()
        Try


            Me.grd.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            Me.grd.RootTable.ColumnSetRowCount = 1
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Dim ColumnSetArticle As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSetOpening As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSetPurchase As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSetPurchaseReturn As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSetReceiving As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSetSales As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSetSalesReturn As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSetDispatch As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSetStoreIssuance As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSetProduction As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSetClosing As New Janus.Windows.GridEX.GridEXColumnSet

            For col As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                Me.grd.RootTable.Columns(col).Visible = False
            Next

            Me.grd.RootTable.Columns("ArticleId").Visible = True
            Me.grd.RootTable.Columns("ArticleCode").Visible = True
            Me.grd.RootTable.Columns("ArticleDescription").Visible = True
            Me.grd.RootTable.Columns("ArticleSizeName").Visible = True
            Me.grd.RootTable.Columns("ArticleColorName").Visible = True
            Me.grd.RootTable.Columns("ArticleSizeName").Caption = "Size"
            Me.grd.RootTable.Columns("ArticleColorName").Caption = "Color"
            Me.grd.RootTable.Columns("Remarks").Visible = True


            Me.grd.RootTable.Columns("OpeningMedium").Visible = True
            Me.grd.RootTable.Columns("OpeningLargest").Visible = True
            Me.grd.RootTable.Columns("OpeningSmallest").Visible = True

            Me.grd.RootTable.Columns("PurchasesMedium").Visible = True
            Me.grd.RootTable.Columns("PurchaseLargest").Visible = True
            Me.grd.RootTable.Columns("PurchaseSmallest").Visible = True

            Me.grd.RootTable.Columns("PurchasesReturnMedium").Visible = True
            Me.grd.RootTable.Columns("PurchaseReturnLargest").Visible = True
            Me.grd.RootTable.Columns("PurchaseReturnSmallest").Visible = True

            Me.grd.RootTable.Columns("ReceivingMedium").Visible = True
            Me.grd.RootTable.Columns("ReceivingLargest").Visible = True
            Me.grd.RootTable.Columns("ReceivingSmallest").Visible = True

            Me.grd.RootTable.Columns("SalesMedium").Visible = True
            Me.grd.RootTable.Columns("SalesLargest").Visible = True
            Me.grd.RootTable.Columns("SalesSmallest").Visible = True

            Me.grd.RootTable.Columns("SalesReturnMedium").Visible = True
            Me.grd.RootTable.Columns("SalesReturnLargest").Visible = True
            Me.grd.RootTable.Columns("SalesReturnSmallest").Visible = True

            Me.grd.RootTable.Columns("DispatchMedium").Visible = True
            Me.grd.RootTable.Columns("DispatchLargest").Visible = True
            Me.grd.RootTable.Columns("DispatchSmallest").Visible = True


            Me.grd.RootTable.Columns("StoreIssuanceMedium").Visible = True
            Me.grd.RootTable.Columns("StoreIssuanceLargest").Visible = True
            Me.grd.RootTable.Columns("StoreIssuanceSmallest").Visible = True

            Me.grd.RootTable.Columns("ProductionMedium").Visible = True
            Me.grd.RootTable.Columns("ProductionLargest").Visible = True
            Me.grd.RootTable.Columns("ProductionSmallest").Visible = True



            Me.grd.RootTable.Columns("ClosingMedium").Visible = True
            Me.grd.RootTable.Columns("ClosingLargest").Visible = True
            Me.grd.RootTable.Columns("ClosingSmallest").Visible = True
            Me.grd.RootTable.Columns("ClosingValue").Visible = True


            Me.grd.RootTable.Columns("OpeningSmallest").FormatString = "N2"
            Me.grd.RootTable.Columns("PurchaseSmallest").FormatString = "N2"
            Me.grd.RootTable.Columns("PurchaseReturnSmallest").FormatString = "N2"
            Me.grd.RootTable.Columns("ReceivingSmallest").FormatString = "N2"
            Me.grd.RootTable.Columns("SalesSmallest").FormatString = "N2"
            Me.grd.RootTable.Columns("SalesReturnSmallest").FormatString = "N2"
            Me.grd.RootTable.Columns("DispatchSmallest").FormatString = "N2"
            Me.grd.RootTable.Columns("StoreIssuanceSmallest").FormatString = "N2"
            Me.grd.RootTable.Columns("ProductionSmallest").FormatString = "N2"
            Me.grd.RootTable.Columns("ClosingSmallest").FormatString = "N2"

            Me.grd.RootTable.Columns("OpeningSmallest").TotalFormatString = "N2"
            Me.grd.RootTable.Columns("PurchaseSmallest").TotalFormatString = "N2"
            Me.grd.RootTable.Columns("PurchaseReturnSmallest").TotalFormatString = "N2"
            Me.grd.RootTable.Columns("ReceivingSmallest").TotalFormatString = "N2"
            Me.grd.RootTable.Columns("SalesSmallest").TotalFormatString = "N2"
            Me.grd.RootTable.Columns("SalesReturnSmallest").TotalFormatString = "N2"
            Me.grd.RootTable.Columns("DispatchSmallest").TotalFormatString = "N2"
            Me.grd.RootTable.Columns("StoreIssuanceSmallest").TotalFormatString = "N2"
            Me.grd.RootTable.Columns("ProductionSmallest").TotalFormatString = "N2"
            Me.grd.RootTable.Columns("ClosingSmallest").TotalFormatString = "N2"



            Me.grd.RootTable.Columns("OpeningMedium").Caption = "Largest"
            Me.grd.RootTable.Columns("OpeningLargest").Caption = "Pack"
            Me.grd.RootTable.Columns("OpeningSmallest").Caption = "Loose"

            Me.grd.RootTable.Columns("PurchasesMedium").Caption = "Largest"
            Me.grd.RootTable.Columns("PurchaseLargest").Caption = "Pack"
            Me.grd.RootTable.Columns("PurchaseSmallest").Caption = "Loose"

            Me.grd.RootTable.Columns("PurchasesReturnMedium").Caption = "Largest"
            Me.grd.RootTable.Columns("PurchaseReturnLargest").Caption = "Pack"
            Me.grd.RootTable.Columns("PurchaseReturnSmallest").Caption = "Loose"

            Me.grd.RootTable.Columns("ReceivingMedium").Caption = "Largest"
            Me.grd.RootTable.Columns("ReceivingLargest").Caption = "Pack"
            Me.grd.RootTable.Columns("ReceivingSmallest").Caption = "Loose"

            Me.grd.RootTable.Columns("SalesMedium").Caption = "Largest"
            Me.grd.RootTable.Columns("SalesLargest").Caption = "Pack"
            Me.grd.RootTable.Columns("SalesSmallest").Caption = "Loose"

            Me.grd.RootTable.Columns("SalesReturnMedium").Caption = "Largest"
            Me.grd.RootTable.Columns("SalesReturnLargest").Caption = "Pack"
            Me.grd.RootTable.Columns("SalesReturnSmallest").Caption = "Loose"

            Me.grd.RootTable.Columns("DispatchMedium").Caption = "Largest"
            Me.grd.RootTable.Columns("DispatchLargest").Caption = "Pack"
            Me.grd.RootTable.Columns("DispatchSmallest").Caption = "Loose"


            Me.grd.RootTable.Columns("StoreIssuanceMedium").Caption = "Largest"
            Me.grd.RootTable.Columns("StoreIssuanceLargest").Caption = "Pack"
            Me.grd.RootTable.Columns("StoreIssuanceSmallest").Caption = "Loose"

            Me.grd.RootTable.Columns("ProductionMedium").Caption = "Largest"
            Me.grd.RootTable.Columns("ProductionLargest").Caption = "Pack"
            Me.grd.RootTable.Columns("ProductionSmallest").Caption = "Loose"

            Me.grd.RootTable.Columns("ClosingMedium").Caption = "Largest"
            Me.grd.RootTable.Columns("ClosingLargest").Caption = "Pack"
            Me.grd.RootTable.Columns("ClosingSmallest").Caption = "Loose"

            Me.grd.RootTable.Columns("OpeningMedium").CellStyle.BackColor = Color.LightGray
            Me.grd.RootTable.Columns("OpeningLargest").CellStyle.BackColor = Color.LightGray
            Me.grd.RootTable.Columns("OpeningSmallest").CellStyle.BackColor = Color.LightGray


            Me.grd.RootTable.Columns("ClosingMedium").CellStyle.BackColor = Color.Ivory
            Me.grd.RootTable.Columns("ClosingLargest").CellStyle.BackColor = Color.Ivory
            Me.grd.RootTable.Columns("ClosingSmallest").CellStyle.BackColor = Color.Ivory
            Me.grd.RootTable.Columns("ClosingValue").CellStyle.BackColor = Color.Ivory

            For c As Integer = 26 To Me.grd.RootTable.Columns.Count - 1
                Me.grd.RootTable.Columns(c).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(c).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Next

            'Me.grd.FrozenColumns = 26

            Dim str As String = String.Empty
            For i As Integer = 0 To Me.ListBox1.SelectedItems.Count - 1
                If str.Length > 0 Then
                    str = str & "," & Me.ListBox1.SelectedItems(i)
                Else
                    str = Me.ListBox1.SelectedItems(i)
                End If
            Next

            flgSelectedPurchase = False
            flgSelectedPurchaseReturn = False
            flgSelectedReceiving = False
            flgSelectedSales = False
            flgSelectedSalesReturn = False
            flgSelectedDispatch = False
            flgSelectedStoreIssuance = False
            flgSelectedProduction = False

            For Each strDisplayColumn As String In str.Split(",")
                If strDisplayColumn = "Purchase" Then
                    flgSelectedPurchase = True
                ElseIf strDisplayColumn = "Purchase Return" Then
                    flgSelectedPurchaseReturn = True
                ElseIf strDisplayColumn = "Stock Receiving" Then
                    flgSelectedReceiving = True
                ElseIf strDisplayColumn = "Sales" Then
                    flgSelectedSales = True
                ElseIf strDisplayColumn = "Sales Return" Then
                    flgSelectedSalesReturn = True
                ElseIf strDisplayColumn = "Stock Dispatch" Then
                    flgSelectedDispatch = True
                ElseIf strDisplayColumn = "Store Issuance" Then
                    flgSelectedStoreIssuance = True
                ElseIf strDisplayColumn = "Production" Then
                    flgSelectedProduction = True
                End If
            Next

            ColumnSetArticle = grd.RootTable.ColumnSets.Add()
            ColumnSetArticle.Caption = "Description"
            ColumnSetArticle.ColumnCount = 5
            ColumnSetArticle.Add(Me.grd.RootTable.Columns("ArticleCode"), 0, 0)
            ColumnSetArticle.Add(Me.grd.RootTable.Columns("ArticleDescription"), 0, 1)
            ColumnSetArticle.Add(Me.grd.RootTable.Columns("ArticleSizeName"), 0, 2)
            ColumnSetArticle.Add(Me.grd.RootTable.Columns("ArticleColorName"), 0, 3)
            ColumnSetArticle.Add(Me.grd.RootTable.Columns("Remarks"), 0, 4)



            ColumnSetOpening = Me.grd.RootTable.ColumnSets.Add
            ColumnSetOpening.Caption = "Opening"
            ColumnSetOpening.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ColumnSetOpening.ColumnCount = 3
            ColumnSetOpening.Add(Me.grd.RootTable.Columns("OpeningMedium"), 0, 0)
            ColumnSetOpening.Add(Me.grd.RootTable.Columns("OpeningLargest"), 0, 1)
            ColumnSetOpening.Add(Me.grd.RootTable.Columns("OpeningSmallest"), 0, 2)


            If flgSelectedPurchase = True Then
                ColumnSetPurchase = Me.grd.RootTable.ColumnSets.Add
                ColumnSetPurchase.Caption = "Purchase"
                ColumnSetPurchase.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                ColumnSetPurchase.ColumnCount = 3
                ColumnSetPurchase.Add(Me.grd.RootTable.Columns("PurchasesMedium"), 0, 0)
                ColumnSetPurchase.Add(Me.grd.RootTable.Columns("PurchaseLargest"), 0, 1)
                ColumnSetPurchase.Add(Me.grd.RootTable.Columns("PurchaseSmallest"), 0, 2)
            End If

            If flgSelectedPurchaseReturn = True Then
                ColumnSetPurchaseReturn = Me.grd.RootTable.ColumnSets.Add
                ColumnSetPurchaseReturn.Caption = "Purchase Return"
                ColumnSetPurchaseReturn.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                ColumnSetPurchaseReturn.ColumnCount = 3
                ColumnSetPurchaseReturn.Add(Me.grd.RootTable.Columns("PurchasesReturnMedium"), 0, 0)
                ColumnSetPurchaseReturn.Add(Me.grd.RootTable.Columns("PurchaseReturnLargest"), 0, 1)
                ColumnSetPurchaseReturn.Add(Me.grd.RootTable.Columns("PurchaseReturnSmallest"), 0, 2)
            End If

            If flgSelectedReceiving = True Then
                ColumnSetReceiving = Me.grd.RootTable.ColumnSets.Add
                ColumnSetReceiving.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                ColumnSetReceiving.Caption = "Stock Receiving"
                ColumnSetReceiving.ColumnCount = 3
                ColumnSetReceiving.Add(Me.grd.RootTable.Columns("ReceivingMedium"), 0, 0)
                ColumnSetReceiving.Add(Me.grd.RootTable.Columns("ReceivingLargest"), 0, 1)
                ColumnSetReceiving.Add(Me.grd.RootTable.Columns("ReceivingSmallest"), 0, 2)
            End If

            If flgSelectedDispatch = True Then
                ColumnSetDispatch = Me.grd.RootTable.ColumnSets.Add
                ColumnSetDispatch.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                ColumnSetDispatch.Caption = "Dispatch"
                ColumnSetDispatch.ColumnCount = 3
                ColumnSetDispatch.Add(Me.grd.RootTable.Columns("DispatchMedium"), 0, 0)
                ColumnSetDispatch.Add(Me.grd.RootTable.Columns("DispatchLargest"), 0, 1)
                ColumnSetDispatch.Add(Me.grd.RootTable.Columns("DispatchSmallest"), 0, 2)
            End If

            If flgSelectedStoreIssuance = True Then
                ColumnSetStoreIssuance = Me.grd.RootTable.ColumnSets.Add
                ColumnSetStoreIssuance.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                ColumnSetStoreIssuance.Caption = "Store Issuance"
                ColumnSetStoreIssuance.ColumnCount = 3
                ColumnSetStoreIssuance.Add(Me.grd.RootTable.Columns("StoreIssuanceMedium"), 0, 0)
                ColumnSetStoreIssuance.Add(Me.grd.RootTable.Columns("StoreIssuanceLargest"), 0, 1)
                ColumnSetStoreIssuance.Add(Me.grd.RootTable.Columns("StoreIssuanceSmallest"), 0, 2)
            End If

            If flgSelectedProduction = True Then
                ColumnSetProduction = Me.grd.RootTable.ColumnSets.Add
                ColumnSetProduction.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                ColumnSetProduction.Caption = "Production"
                ColumnSetProduction.ColumnCount = 3
                ColumnSetProduction.Add(Me.grd.RootTable.Columns("ProductionMedium"), 0, 0)
                ColumnSetProduction.Add(Me.grd.RootTable.Columns("ProductionLargest"), 0, 1)
                ColumnSetProduction.Add(Me.grd.RootTable.Columns("ProductionSmallest"), 0, 2)
            End If


            If flgSelectedSales = True Then
                ColumnSetSales = Me.grd.RootTable.ColumnSets.Add
                ColumnSetSales.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                ColumnSetSales.Caption = "Sales"
                ColumnSetSales.ColumnCount = 3
                ColumnSetSales.Add(Me.grd.RootTable.Columns("SalesMedium"), 0, 0)
                ColumnSetSales.Add(Me.grd.RootTable.Columns("SalesLargest"), 0, 1)
                ColumnSetSales.Add(Me.grd.RootTable.Columns("SalesSmallest"), 0, 2)
            End If

            If flgSelectedSalesReturn = True Then
                ColumnSetSales = Me.grd.RootTable.ColumnSets.Add
                ColumnSetSales.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                ColumnSetSales.Caption = "Sales Return"
                ColumnSetSales.ColumnCount = 3
                ColumnSetSales.Add(Me.grd.RootTable.Columns("SalesReturnMedium"), 0, 0)
                ColumnSetSales.Add(Me.grd.RootTable.Columns("SalesReturnLargest"), 0, 1)
                ColumnSetSales.Add(Me.grd.RootTable.Columns("SalesReturnSmallest"), 0, 2)
            End If

            ColumnSetClosing = Me.grd.RootTable.ColumnSets.Add
            ColumnSetClosing.Caption = "Closing"
            ColumnSetClosing.ColumnCount = 4
            ColumnSetClosing.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ColumnSetClosing.Add(Me.grd.RootTable.Columns("ClosingMedium"), 0, 0)
            ColumnSetClosing.Add(Me.grd.RootTable.Columns("ClosingLargest"), 0, 1)
            ColumnSetClosing.Add(Me.grd.RootTable.Columns("ClosingSmallest"), 0, 2)
            ColumnSetClosing.Add(Me.grd.RootTable.Columns("ClosingValue"), 0, 3)

            CtrlGrdBar2_Load(Nothing, Nothing)





            If flgSelectedPurchase = False Then
                Me.grd.RootTable.Columns("PurchasesMedium").Visible = False
                Me.grd.RootTable.Columns("PurchaseLargest").Visible = False
                Me.grd.RootTable.Columns("PurchaseSmallest").Visible = False
            End If
            If flgSelectedPurchaseReturn = False Then
                Me.grd.RootTable.Columns("PurchasesReturnMedium").Visible = False
                Me.grd.RootTable.Columns("PurchaseReturnLargest").Visible = False
                Me.grd.RootTable.Columns("PurchaseReturnSmallest").Visible = False
            End If

            If flgSelectedReceiving = False Then
                Me.grd.RootTable.Columns("ReceivingMedium").Visible = False
                Me.grd.RootTable.Columns("ReceivingLargest").Visible = False
                Me.grd.RootTable.Columns("ReceivingSmallest").Visible = False
            End If

            If flgSelectedSales = False Then
                Me.grd.RootTable.Columns("SalesMedium").Visible = False
                Me.grd.RootTable.Columns("SalesLargest").Visible = False
                Me.grd.RootTable.Columns("SalesSmallest").Visible = False
            End If

            If flgSelectedSalesReturn = False Then
                Me.grd.RootTable.Columns("SalesReturnMedium").Visible = False
                Me.grd.RootTable.Columns("SalesReturnLargest").Visible = False
                Me.grd.RootTable.Columns("SalesReturnSmallest").Visible = False
            End If

            If flgSelectedDispatch = False Then
                Me.grd.RootTable.Columns("DispatchMedium").Visible = False
                Me.grd.RootTable.Columns("DispatchLargest").Visible = False
                Me.grd.RootTable.Columns("DispatchSmallest").Visible = False
            End If

            If flgSelectedStoreIssuance = False Then
                Me.grd.RootTable.Columns("StoreIssuanceMedium").Visible = False
                Me.grd.RootTable.Columns("StoreIssuanceLargest").Visible = False
                Me.grd.RootTable.Columns("StoreIssuanceSmallest").Visible = True
            End If

            If flgSelectedProduction = False Then
                Me.grd.RootTable.Columns("ProductionMedium").Visible = False
                Me.grd.RootTable.Columns("ProductionLargest").Visible = False
                Me.grd.RootTable.Columns("ProductionSmallest").Visible = False
            End If
            grd.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.Button2_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdRptStockStatementUnitWise_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & vbCrLf & "Stock Statement Unit Wise" & vbCrLf & "Date From:" & Me.dtpFromDate.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpToDate.Value.ToString("dd/MMM/yyyy") & " "

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class