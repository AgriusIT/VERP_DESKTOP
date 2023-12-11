Imports System.Data.OleDb
Public Class rptStockAccountsReport
    Dim CurrentId As Integer

    Private Sub rptInventoryForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub


    Private Sub DefMainAcc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.BindGrid()
        'Me.FillAllCombos()
        'Me.RefreshForm()    'FillComboBox()
        Me.cmbType.SelectedIndex = 0
        'Me.dtpFrom.Value = Me.dtpFrom.Value.AddMonths(-1)
        Me.cmbPeriod.Text = "Current Month"
        Me.BindGrid()
        Me.GetSecurityRights()

    End Sub

    Sub BindGrid()
        Try
            If Not Me.IsValidate Then Exit Sub

            Me.grdStock.DataSource = Nothing

            Dim strSql As String
            Dim strCol As String = String.Empty
            Dim dtFinal As New DataTable
            Dim dc As DataColumn
            Dim dr As DataRow


            ''Adding First Col in Final Table
            dc = New DataColumn("First", GetType(System.String))
            dc.Caption = String.Empty
            dtFinal.Columns.Add(dc)

            dc = New DataColumn("GTotal", GetType(System.Double))
            dc.Caption = "G.Total"
            dtFinal.Columns.Add(dc)


            strSql = "SELECT     ArticleLpoName FROM ArticleLpoDefTable ORDER BY ArticleLpoName "
            Dim dt As DataTable = GetDataTable(strSql)

            If dt.Rows.Count = 0 Then
                msg_Information("No Record Found")
                Exit Sub
            End If

            ''adding All LPo in Final Table
            For Each r As DataRow In dt.Rows
                If Not dtFinal.Columns.Contains(r.Item(0)) Then
                    dc = New DataColumn(r(0), GetType(System.Double))
                    dtFinal.Columns.Add(dc)
                End If
            Next


            If Me.cmbType.SelectedIndex = 0 Then
                strCol = "SUM(DERIVEDTBL.Stock)"
            Else
                strCol = "SUM(DERIVEDTBL.Price)"
            End If


            ''Get Opening Stock before the from date
            strSql = " SELECT   " & strCol & " AS Col , ArticleLpoDefTable.ArticleLpoName AS ArticleLpoName" _
                    & " FROM         (SELECT     dbo.ArticleDefTable.ArticleId, SUM((ISNULL(Purchase.PurchaseQty, 0) + ISNULL(SalesReturn.SalesReturnQty, 0)) - (ISNULL(Sales.SalesQty, " _
                    & "                            0) + ISNULL(PurchaseReturn.PurchaseReturnQty, 0) + ISNULL(Dispatch.DispatchQty, 0))) AS Stock, dbo.ArticleDefTable.ArticleLPOId,  " _
                    & " SUM((ISNULL(Purchase.Price, 0) + ISNULL(SalesReturn.Price, 0)) - (ISNULL(Sales.Price, 0) + ISNULL(PurchaseReturn.Price, 0) + ISNULL(Dispatch.Price, " _
                    & "   0))) AS Price " _
                    & "            FROM          dbo.ArticleDefTable Left OUter JOIN " _
                    & "                               (SELECT     ISNULL(SUM(dbo.DispatchDetailTable.Qty), 0) AS DispatchQty, dbo.ArticleDefTable.ArticleId ,  SUM(dbo.DispatchDetailTable.Price * isnull(dbo.DispatchDetailTable.Qty, 0)) AS Price " _
                    & "                             FROM          dbo.DispatchDetailTable INNER JOIN " _
                    & "                                                dbo.DispatchMasterTable ON dbo.DispatchDetailTable.DispatchId = dbo.DispatchMasterTable.DispatchId RIGHT OUTER JOIN " _
                    & "                                            dbo.ArticleDefTable ON dbo.DispatchDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId  " _
                    & "                 WHERE      (dbo.DispatchMasterTable.DispatchDate < CONVERT(DATETIME, '" & Me.dtpFrom.Value.Date & " 00:00:00', 102)) " _
                    & "             GROUP BY dbo.ArticleDefTable.ArticleId) Dispatch ON dbo.ArticleDefTable.ArticleId = Dispatch.ArticleId LEFT OUTER JOIN " _
                    & "       (SELECT     ISNULL(SUM(dbo.ReceivingDetailTable.Qty), 0) AS PurchaseQty, dbo.ArticleDefTable.ArticleId, SUM(dbo.ReceivingDetailTable.Price * isnull(dbo.ReceivingDetailTable.Qty, 0)) AS Price " _
                    & "     FROM          dbo.ReceivingDetailTable INNER JOIN " _
                    & "                        dbo.ReceivingMasterTable ON  " _
                    & "                    dbo.ReceivingDetailTable.ReceivingId = dbo.ReceivingMasterTable.ReceivingId RIGHT OUTER JOIN " _
                    & "                dbo.ArticleDefTable ON dbo.ReceivingDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId " _
                    & " WHERE      (dbo.ReceivingMasterTable.ReceivingDate < CONVERT(DATETIME, '" & Me.dtpFrom.Value.Date & " 00:00:00', 102))  AND ArticleDefTable.Active=1 " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId) Purchase ON dbo.ArticleDefTable.ArticleId = Purchase.ArticleId LEFT OUTER JOIN " _
                    & " (SELECT     ISNULL(SUM(dbo.PurchaseReturnDetailTable.Qty), 0) AS PurchaseReturnQty, dbo.ArticleDefTable.ArticleId, SUM(dbo.PurchaseReturnDetailTable.Price * isnull(dbo.PurchaseReturnDetailTable.Qty, 0)) AS Price " _
                    & " FROM          dbo.PurchaseReturnDetailTable INNER JOIN  " _
                    & "                    dbo.PurchaseReturnMasterTable ON  " _
                    & "                dbo.PurchaseReturnDetailTable.PurchaseReturnId = dbo.PurchaseReturnMasterTable.PurchaseReturnId RIGHT OUTER JOIN " _
                    & "            dbo.ArticleDefTable ON dbo.PurchaseReturnDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId " _
                    & " WHERE      (dbo.PurchaseReturnMasterTable.PurchaseReturnDate < CONVERT(DATETIME, '" & Me.dtpFrom.Value.Date & " 00:00:000', 102)) AND ArticleDefTable.Active=1  " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId) PurchaseReturn ON dbo.ArticleDefTable.ArticleId = PurchaseReturn.ArticleId LEFT OUTER JOIN " _
                    & " (SELECT     ISNULL(SUM(dbo.SalesReturnDetailTable.Qty), 0) AS SalesReturnQty, dbo.ArticleDefTable.ArticleId, SUM(dbo.SalesReturnDetailTable.Price * isnull(dbo.SalesReturnDetailTable.Qty, 0)) AS Price " _
                    & " FROM          dbo.SalesReturnDetailTable INNER JOIN " _
                    & "                    dbo.SalesReturnMasterTable ON " _
                    & "                dbo.SalesReturnDetailTable.SalesReturnId = dbo.SalesReturnMasterTable.SalesReturnId RIGHT OUTER JOIN " _
                    & "            dbo.ArticleDefTable ON dbo.SalesReturnDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId " _
                    & " WHERE      (dbo.SalesReturnMasterTable.SalesReturnDate < CONVERT(DATETIME, '" & Me.dtpFrom.Value.Date & " 00:00:00', 102)) AND ArticleDefTable.Active=1 " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId) SalesReturn ON dbo.ArticleDefTable.ArticleId = SalesReturn.ArticleId LEFT OUTER JOIN " _
                    & " (SELECT     ISNULL(SUM(dbo.SalesDetailTable.Qty), 0) AS SalesQty, dbo.ArticleDefTable.ArticleId,  SUM(dbo.SalesDetailTable.Price * isnull(dbo.SalesDetailTable.Qty, 0)) AS Price " _
                    & " FROM          dbo.SalesDetailTable INNER JOIN " _
                    & " dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId RIGHT OUTER JOIN " _
                    & " dbo.ArticleDefTable ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId " _
                    & " WHERE      (dbo.SalesMasterTable.SalesDate < CONVERT(DATETIME, '" & Me.dtpFrom.Value.Date & " 00:00:00', 102)) AND ArticleDefTable.Active=1  " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId) Sales ON dbo.ArticleDefTable.ArticleId = Sales.ArticleId " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId, dbo.ArticleDefTable.ArticleLPOId) DERIVEDTBL Inner join" _
                    & " dbo.ArticleLpoDefTable ON DERIVEDTBL.ArticleLpoId = dbo.ArticleLpoDefTable.ArticleLpoId " _
                    & " GROUP BY ArticleLpoDefTable.ArticleLpoName"

            dt = GetDataTable(strSql)

            dr = dtFinal.NewRow
            dr.Item("First") = "OPENING STOCK"
            dtFinal.Rows.Add(dr)

            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    dr.Item(r.Item("ArticleLpoName")) = r.Item("Col")
                    dr.Item("GTotal") = Val(dr.Item("GTotal").ToString) + Val(dr.Item(r.Item("ArticleLpoName")).ToString)
                Next
            End If


            ''Adding Receving = Purchase + SalesReturn

            If Me.cmbType.SelectedIndex = 0 Then
                strCol = "SUM(DERIVEDTBL.Receiving)"
            Else
                strCol = "SUM(DERIVEDTBL.Price)"
            End If

            strSql = " SELECT  " & strCol & "    AS Col, " _
                    & " dbo.ArticleLpoDefTable.ArticleLpoName " _
                    & " FROM         (SELECT     dbo.ArticleDefTable.ArticleId, dbo.ArticleDefTable.ArticleLpoId, SUM(ISNULL(Purchase.PurchaseQty, 0)  " _
                    & "                                           + ISNULL(SalesReturn.SalesReturnQty, 0)) AS Receiving, " _
                      & "  SUM(ISNULL(Purchase.Price, 0) + ISNULL(SalesReturn.Price, 0)) AS Price " _
                    & "                 FROM          dbo.ArticleDefTable INNER JOIN " _
                    & "                                     dbo.ArticleLpoDefTable ON dbo.ArticleDefTable.ArticleLPOId = dbo.ArticleLpoDefTable.ArticleLpoId LEFT OUTER JOIN " _
                    & "                                      (SELECT     ISNULL(SUM(dbo.ReceivingDetailTable.Qty), 0) AS PurchaseQty, dbo.ArticleDefTable.ArticleId , " _
                                 & "                   SUM(dbo.ReceivingDetailTable.Price * isnull(dbo.ReceivingDetailTable.Qty, 0)) AS Price" _
                    & "                                     FROM          dbo.ReceivingDetailTable INNER JOIN" _
                    & "                                                         dbo.ReceivingMasterTable ON " _
                    & "                                                      dbo.ReceivingDetailTable.ReceivingId = dbo.ReceivingMasterTable.ReceivingId RIGHT OUTER JOIN " _
                    & "                                                   dbo.ArticleDefTable ON dbo.ReceivingDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId " _
                    & "                         WHERE      (dbo.ReceivingMasterTable.ReceivingDate between  CONVERT(DATETIME, '" & Me.dtpFrom.Value.Date & " 00:00:00', 102) and  CONVERT(DATETIME, '" & Me.dtpTo.Value.Date & " 00:00:00', 102)) AND ArticleDefTable.Active=1 " _
                    & "                      GROUP BY dbo.ArticleDefTable.ArticleId) Purchase ON dbo.ArticleDefTable.ArticleId = Purchase.ArticleId LEFT OUTER JOIN " _
                    & "                 (SELECT     ISNULL(SUM(dbo.SalesReturnDetailTable.Qty), 0) AS SalesReturnQty, dbo.ArticleDefTable.ArticleId , SUM(SalesReturnDetailTable.Price * isnull(SalesReturnDetailTable.Qty, 0)) AS Price" _
                    & "                FROM          dbo.SalesReturnDetailTable INNER JOIN " _
                    & "                                    dbo.SalesReturnMasterTable ON  " _
                    & "                                 dbo.SalesReturnDetailTable.SalesReturnId = dbo.SalesReturnMasterTable.SalesReturnId RIGHT OUTER JOIN " _
                    & "                              dbo.ArticleDefTable ON dbo.SalesReturnDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId" _
                    & "    WHERE      (dbo.SalesReturnMasterTable.SalesReturnDate between  CONVERT(DATETIME, '" & Me.dtpFrom.Value.Date & " 00:00:00', 102) and  CONVERT(DATETIME, '" & Me.dtpTo.Value.Date & " 00:00:00', 102)) AND ArticleDefTable.Active=1 " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId) SalesReturn ON dbo.ArticleDefTable.ArticleId = SalesReturn.ArticleId " _
                    & "GROUP BY dbo.ArticleDefTable.ArticleId, dbo.ArticleDefTable.ArticleLpoId) DERIVEDTBL INNER JOIN " _
                    & "dbo.ArticleLpoDefTable ON DERIVEDTBL.ArticleLpoId = dbo.ArticleLpoDefTable.ArticleLpoId " _
                    & "GROUP BY dbo.ArticleLpoDefTable.ArticleLpoName"

            dt = GetDataTable(strSql)

            dr = dtFinal.NewRow
            dr.Item("First") = "Receiving"
            dtFinal.Rows.Add(dr)

            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    dr.Item(r.Item("ArticleLpoName")) = r.Item("Col")
                    dr.Item("GTotal") = Val(dr.Item("GTotal").ToString) + Val(dr.Item(r.Item("ArticleLpoName")).ToString)
                Next
            End If


            If Me.cmbType.SelectedIndex = 1 Then
                ''Adding Price Increase when select Value
                strSql = "SELECT     SUM(IncrementReductionTable.PurchaseNewPrice - IncrementReductionTable.PurchaseOldPrice * IncrementReductionTable.StockQty) AS Col, " _
                        & " ArticleLpoDefTable.ArticleLpoName" _
                        & "  FROM         IncrementReductionTable INNER JOIN" _
                        & "                   ArticleDefTable ON IncrementReductionTable.ArticleDefId = ArticleDefTable.ArticleId INNER JOIN " _
                        & "               ArticleLpoDefTable ON ArticleDefTable.ArticleLPOId = ArticleLpoDefTable.ArticleLpoId " _
                        & " WHERE     (IncrementReductionTable.IncrementReductionDate BETWEEN CONVERT(DATETIME, '" & Me.dtpFrom.Value.Date & " 00:00:00', 102) and  CONVERT(DATETIME, '" & Me.dtpTo.Value.Date & " 00:00:00', 102)) AND ArticleDefTable.Active=1 " _
                        & " AND (IncrementReductionTable.PurchaseNewPrice - IncrementReductionTable.PurchaseOldPrice > 0) " _
                        & " GROUP BY ArticleLpoDefTable.ArticleLpoName"

                dt = GetDataTable(strSql)

                dr = dtFinal.NewRow
                dr.Item("First") = "PRICE INCREASE"
                dtFinal.Rows.Add(dr)

                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        dr.Item(r.Item("ArticleLpoName")) = r.Item("Col")
                        dr.Item("GTotal") = Val(dr.Item("GTotal").ToString) + Val(dr.Item(r.Item("ArticleLpoName")).ToString)
                    Next
                End If
            End If

            ''Calculating Total(1)
            dr = dtFinal.NewRow
            dr.Item("First") = "TOTAL(1)"
            dtFinal.Rows.Add(dr)

            For r As Integer = 0 To dtFinal.Rows.Count - 2
                For colIndex As Integer = 1 To dtFinal.Columns.Count - 1
                    dr.Item(dtFinal.Columns(colIndex).ColumnName) = Val(dr.Item(dtFinal.Columns(colIndex).ColumnName).ToString) + Val(dtFinal.Rows(r)(dtFinal.Columns(colIndex).ColumnName).ToString)
                Next
            Next



            ''Adding Sale
            If Me.cmbType.SelectedIndex = 0 Then
                strCol = "SUM(DERIVEDTBL.SalesQty)"
            Else
                strCol = " SUM(DERIVEDTBL.Price)"
            End If

            strSql = "SELECT     " & strCol & " AS Col, dbo.ArticleLpoDefTable.ArticleLpoName AS ArticleLpoName" _
                    & " FROM         (SELECT     dbo.ArticleDefTable.ArticleId, dbo.ArticleDefTable.ArticleLPOId, SUM(ISNULL(Sales.SalesQty, 0)) AS SalesQty, SUM(ISNULL(Sales.Price, 0)) " _
                    & "                       AS Price " _
                    & " FROM          dbo.ArticleDefTable LEFT OUTER JOIN " _
                    & "                        (SELECT     ISNULL(SUM(dbo.SalesDetailTable.Qty), 0) AS SalesQty, dbo.ArticleDefTable.ArticleId,  " _
                    & "                                             SUM(dbo.SalesDetailTable.Price * isnull(dbo.SalesDetailTable.Qty, 0)) AS Price " _
                    & "                  FROM          dbo.SalesDetailTable INNER JOIN " _
                    & "                                     dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId RIGHT OUTER JOIN " _
                    & "                                 dbo.ArticleDefTable ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId " _
                    & "      WHERE      (dbo.SalesMasterTable.SalesDate CONVERT(DATETIME, '" & Me.dtpFrom.Value.Date & " 00:00:00', 102) and  CONVERT(DATETIME, '" & Me.dtpTo.Value.Date & " 00:00:00', 102)) AND ArticleDefTable.Active=1  " _
                    & "  GROUP BY dbo.ArticleDefTable.ArticleId) Sales ON dbo.ArticleDefTable.ArticleId = Sales.ArticleId " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId, dbo.ArticleDefTable.ArticleLPOId) DERIVEDTBL INNER JOIN " _
                    & " dbo.ArticleLpoDefTable ON DERIVEDTBL.ArticleLPOId = dbo.ArticleLpoDefTable.ArticleLpoId " _
                    & " GROUP BY dbo.ArticleLpoDefTable.ArticleLpoName"


            dr = dtFinal.NewRow
            dr.Item("First") = "SALE"
            dtFinal.Rows.Add(dr)

            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    dr.Item(r.Item("ArticleLpoName")) = r.Item("Col")
                    dr.Item("GTotal") = Val(dr.Item("GTotal").ToString) + Val(dr.Item(r.Item("ArticleLpoName")).ToString)
                Next
            End If



            ''Adding Dispatch 
            If Me.cmbType.SelectedIndex = 0 Then
                strCol = "SUM(DERIVEDTBL.DispatchQty)"
            Else
                strCol = " SUM(DERIVEDTBL.Price)"
            End If
            strSql = "SELECT " & strCol & "     AS Col, dbo.ArticleLpoDefTable.ArticleLpoName AS ArticleLpoName" _
                    & " FROM         (SELECT     dbo.ArticleDefTable.ArticleId, dbo.ArticleDefTable.ArticleLPOId, ISNULL(SUM(Dispatch.DispatchQty), 0) AS DispatchQty,  " _
                    & " ISNULL(SUM(Dispatch.Price), 0) AS Price " _
                    & " FROM          dbo.ArticleDefTable LEFT OUTER JOIN " _
                    & "                        (SELECT     ISNULL(SUM(dbo.DispatchDetailTable.Qty), 0) AS DispatchQty, dbo.ArticleDefTable.ArticleId,  " _
                    & "                                             SUM(dbo.DispatchDetailTable.Price * isnull(dbo.DispatchDetailTable.Qty, 0)) AS Price " _
                    & "                  FROM          dbo.DispatchDetailTable INNER JOIN " _
                    & "                                     dbo.DispatchMasterTable ON dbo.DispatchDetailTable.DispatchId = dbo.DispatchMasterTable.DispatchId RIGHT OUTER JOIN " _
                    & "                                 dbo.ArticleDefTable ON dbo.DispatchDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId  " _
                    & "      WHERE      (dbo.DispatchMasterTable.DispatchDate BETWEEN CONVERT(DATETIME, '" & Me.dtpFrom.Value.Date & " 00:00:00', 102) and  CONVERT(DATETIME, '" & Me.dtpTo.Value.Date & " 00:00:00', 102))  AND ArticleDefTable.Active= 1 " _
                    & "  GROUP BY dbo.ArticleDefTable.ArticleId) Dispatch ON dbo.ArticleDefTable.ArticleId = Dispatch.ArticleId " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId, dbo.ArticleDefTable.ArticleLPOId) DERIVEDTBL INNER JOIN " _
                    & " dbo.ArticleLpoDefTable ON DERIVEDTBL.ArticleLPOId = dbo.ArticleLpoDefTable.ArticleLpoId " _
                    & " GROUP BY dbo.ArticleLpoDefTable.ArticleLpoName"


            dr = dtFinal.NewRow
            dr.Item("First") = "DISPATCH"
            dtFinal.Rows.Add(dr)

            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    dr.Item(r.Item("ArticleLpoName")) = r.Item("Col")
                    dr.Item("GTotal") = Val(dr.Item("GTotal").ToString) + Val(dr.Item(r.Item("ArticleLpoName")).ToString)
                Next
            End If


            If Me.cmbType.SelectedIndex = 1 Then
                ''Adding Price Decrease when select Value
                strSql = "SELECT     SUM(IncrementReductionTable.PurchaseNewPrice - IncrementReductionTable.PurchaseOldPrice * IncrementReductionTable.StockQty) AS Col, " _
                        & " ArticleLpoDefTable.ArticleLpoName" _
                        & "  FROM         IncrementReductionTable INNER JOIN" _
                        & "                   ArticleDefTable ON IncrementReductionTable.ArticleDefId = ArticleDefTable.ArticleId INNER JOIN " _
                        & "               ArticleLpoDefTable ON ArticleDefTable.ArticleLPOId = ArticleLpoDefTable.ArticleLpoId " _
                        & " WHERE     (IncrementReductionTable.IncrementReductionDate BETWEEN CONVERT(DATETIME, '" & Me.dtpFrom.Value.Date & " 00:00:00', 102) and  CONVERT(DATETIME, '" & Me.dtpTo.Value.Date & " 00:00:00', 102)) AND ArticleDefTable.Active=1 " _
                        & " AND (IncrementReductionTable.PurchaseNewPrice - IncrementReductionTable.PurchaseOldPrice < 0) " _
                        & " GROUP BY ArticleLpoDefTable.ArticleLpoName"

                dt = GetDataTable(strSql)

                dr = dtFinal.NewRow
                dr.Item("First") = "PRICE REDUCTION"
                dtFinal.Rows.Add(dr)

                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        dr.Item(r.Item("ArticleLpoName")) = r.Item("Col")
                        dr.Item("GTotal") = Val(dr.Item("GTotal").ToString) + Val(dr.Item(r.Item("ArticleLpoName")).ToString)
                    Next
                End If
            End If

            ''Adding  Closing Stock
            If Me.cmbType.SelectedIndex = 0 Then
                strCol = "SUM(DERIVEDTBL.Stock)"
            Else
                strCol = "SUM(DERIVEDTBL.Price)"
            End If

            ''Get Closing Stock before the from date
            strSql = " SELECT   " & strCol & " AS Col , ArticleLpoDefTable.ArticleLpoName AS ArticleLpoName" _
                    & " FROM         (SELECT     dbo.ArticleDefTable.ArticleId, SUM((ISNULL(Purchase.PurchaseQty, 0) + ISNULL(SalesReturn.SalesReturnQty, 0)) - (ISNULL(Sales.SalesQty, " _
                    & "                            0) + ISNULL(PurchaseReturn.PurchaseReturnQty, 0) + ISNULL(Dispatch.DispatchQty, 0))) AS Stock, dbo.ArticleDefTable.ArticleLPOId,  " _
                    & " SUM((ISNULL(Purchase.Price, 0) + ISNULL(SalesReturn.Price, 0)) - (ISNULL(Sales.Price, 0) + ISNULL(PurchaseReturn.Price, 0) + ISNULL(Dispatch.Price, " _
                    & "   0))) AS Price " _
                    & "            FROM          dbo.ArticleDefTable Left OUter JOIN " _
                    & "                               (SELECT     ISNULL(SUM(dbo.DispatchDetailTable.Qty), 0) AS DispatchQty, dbo.ArticleDefTable.ArticleId ,  SUM(dbo.DispatchDetailTable.Price * isnull(dbo.DispatchDetailTable.Qty, 0)) AS Price " _
                    & "                             FROM          dbo.DispatchDetailTable INNER JOIN " _
                    & "                                                dbo.DispatchMasterTable ON dbo.DispatchDetailTable.DispatchId = dbo.DispatchMasterTable.DispatchId RIGHT OUTER JOIN " _
                    & "                                            dbo.ArticleDefTable ON dbo.DispatchDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId  " _
                    & "                 WHERE      (dbo.DispatchMasterTable.DispatchDate <= CONVERT(DATETIME, '" & Me.dtpTo.Value.Date & " 00:00:00', 102))  AND ArticleDefTable.Active=1 " _
                    & "             GROUP BY dbo.ArticleDefTable.ArticleId) Dispatch ON dbo.ArticleDefTable.ArticleId = Dispatch.ArticleId LEFT OUTER JOIN " _
                    & "       (SELECT     ISNULL(SUM(dbo.ReceivingDetailTable.Qty), 0) AS PurchaseQty, dbo.ArticleDefTable.ArticleId, SUM(dbo.ReceivingDetailTable.Price * isnull(dbo.ReceivingDetailTable.Qty, 0)) AS Price " _
                    & "     FROM          dbo.ReceivingDetailTable INNER JOIN " _
                    & "                        dbo.ReceivingMasterTable ON  " _
                    & "                    dbo.ReceivingDetailTable.ReceivingId = dbo.ReceivingMasterTable.ReceivingId RIGHT OUTER JOIN " _
                    & "                dbo.ArticleDefTable ON dbo.ReceivingDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId " _
                    & " WHERE      (dbo.ReceivingMasterTable.ReceivingDate <= CONVERT(DATETIME, '" & Me.dtpTo.Value.Date & " 00:00:00', 102))  AND ArticleDefTable.Active=1 " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId) Purchase ON dbo.ArticleDefTable.ArticleId = Purchase.ArticleId LEFT OUTER JOIN " _
                    & " (SELECT     ISNULL(SUM(dbo.PurchaseReturnDetailTable.Qty), 0) AS PurchaseReturnQty, dbo.ArticleDefTable.ArticleId, SUM(dbo.PurchaseReturnDetailTable.Price * isnull(dbo.PurchaseReturnDetailTable.Qty, 0)) AS Price " _
                    & " FROM          dbo.PurchaseReturnDetailTable INNER JOIN  " _
                    & "                    dbo.PurchaseReturnMasterTable ON  " _
                    & "                dbo.PurchaseReturnDetailTable.PurchaseReturnId = dbo.PurchaseReturnMasterTable.PurchaseReturnId RIGHT OUTER JOIN " _
                    & "            dbo.ArticleDefTable ON dbo.PurchaseReturnDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId " _
                    & " WHERE      (dbo.PurchaseReturnMasterTable.PurchaseReturnDate <= CONVERT(DATETIME, '" & Me.dtpTo.Value.Date & " 00:00:000', 102)) ANd ArticleDefTable.Active=1 " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId) PurchaseReturn ON dbo.ArticleDefTable.ArticleId = PurchaseReturn.ArticleId LEFT OUTER JOIN " _
                    & " (SELECT     ISNULL(SUM(dbo.SalesReturnDetailTable.Qty), 0) AS SalesReturnQty, dbo.ArticleDefTable.ArticleId, SUM(dbo.SalesReturnDetailTable.Price * isnull(dbo.SalesReturnDetailTable.Qty, 0)) AS Price " _
                    & " FROM          dbo.SalesReturnDetailTable INNER JOIN " _
                    & "                    dbo.SalesReturnMasterTable ON " _
                    & "                dbo.SalesReturnDetailTable.SalesReturnId = dbo.SalesReturnMasterTable.SalesReturnId RIGHT OUTER JOIN " _
                    & "            dbo.ArticleDefTable ON dbo.SalesReturnDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId " _
                    & " WHERE      (dbo.SalesReturnMasterTable.SalesReturnDate <= CONVERT(DATETIME, '" & Me.dtpTo.Value.Date & " 00:00:00', 102)) AND ArticleDefTable.Active=1 " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId) SalesReturn ON dbo.ArticleDefTable.ArticleId = SalesReturn.ArticleId LEFT OUTER JOIN " _
                    & " (SELECT     ISNULL(SUM(dbo.SalesDetailTable.Qty), 0) AS SalesQty, dbo.ArticleDefTable.ArticleId,  SUM(dbo.SalesDetailTable.Price * isnull(dbo.SalesDetailTable.Qty, 0)) AS Price " _
                    & " FROM          dbo.SalesDetailTable INNER JOIN " _
                    & " dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId RIGHT OUTER JOIN " _
                    & " dbo.ArticleDefTable ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId " _
                    & " WHERE      (dbo.SalesMasterTable.SalesDate <= CONVERT(DATETIME, '" & Me.dtpTo.Value.Date & " 00:00:00', 102)) AND ArticleDefTable.Active=1 " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId) Sales ON dbo.ArticleDefTable.ArticleId = Sales.ArticleId " _
                    & " GROUP BY dbo.ArticleDefTable.ArticleId, dbo.ArticleDefTable.ArticleLPOId) DERIVEDTBL Inner join" _
                    & " dbo.ArticleLpoDefTable ON DERIVEDTBL.ArticleLpoId = dbo.ArticleLpoDefTable.ArticleLpoId " _
                    & " GROUP BY ArticleLpoDefTable.ArticleLpoName"

            dt = GetDataTable(strSql)

            dr = dtFinal.NewRow
            dr.Item("First") = "CLOSING STOCK"
            dtFinal.Rows.Add(dr)

            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    dr.Item(r.Item("ArticleLpoName")) = r.Item("Col")
                    dr.Item("GTotal") = Val(dr.Item("GTotal").ToString) + Val(dr.Item(r.Item("ArticleLpoName")).ToString)
                Next
            End If


            ''Adding  Purchase Return
            If Me.cmbType.SelectedIndex = 0 Then
                strCol = "SUM(DERIVEDTBL.Qty)"
            Else
                strCol = "SUM(DERIVEDTBL.Price)"
            End If

            strSql = " SELECT " & strCol & "  AS Col, dbo.ArticleLpoDefTable.ArticleLpoName AS ArticleLpoName" _
                    & " FROM         (SELECT     dbo.ArticleDefTable.ArticleId, dbo.ArticleDefTable.ArticleLPOId, SUM(ISNULL(PurchaseReturn.PurchaseReturnQty, 0)) AS Qty,  " _
                    & "                                           SUM(ISNULL(PurchaseReturn.Price, 0)) AS Price " _
                    & "                 FROM          dbo.ArticleDefTable LEFT OUTER JOIN " _
                    & "                                         (SELECT     ISNULL(SUM(dbo.PurchaseReturnDetailTable.Qty), 0) AS PurchaseReturnQty, dbo.ArticleDefTable.ArticleId,  " _
                    & "                                                               SUM(dbo.PurchaseReturnDetailTable.Price * isnull(dbo.PurchaseReturnDetailTable.Qty, 0)) AS Price " _
                    & "                                     FROM          dbo.PurchaseReturnDetailTable INNER JOIN " _
                    & "                                                         dbo.PurchaseReturnMasterTable ON  " _
                    & "                                                      dbo.PurchaseReturnDetailTable.PurchaseReturnId = dbo.PurchaseReturnMasterTable.PurchaseReturnId RIGHT OUTER JOIN " _
                    & "                                                   dbo.ArticleDefTable ON dbo.PurchaseReturnDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId " _
                    & "                         WHERE      (dbo.PurchaseReturnMasterTable.PurchaseReturnDate < CONVERT(DATETIME, '1/28/2009 00:00:000', 102)) AND ArticleDefTable.Active=1 " _
                    & "                      GROUP BY dbo.ArticleDefTable.ArticleId) PurchaseReturn ON dbo.ArticleDefTable.ArticleId = PurchaseReturn.ArticleId " _
                    & "GROUP BY dbo.ArticleDefTable.ArticleId, dbo.ArticleDefTable.ArticleLPOId) DERIVEDTBL INNER JOIN " _
                    & "dbo.ArticleLpoDefTable ON DERIVEDTBL.ArticleLPOId = dbo.ArticleLpoDefTable.ArticleLpoId " _
                    & "GROUP BY dbo.ArticleLpoDefTable.ArticleLpoName"

            dt = GetDataTable(strSql)

            dr = dtFinal.NewRow
            dr.Item("First") = "PURCHASE RETURN"
            dtFinal.Rows.Add(dr)

            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    dr.Item(r.Item("ArticleLpoName")) = r.Item("Col")
                    dr.Item("GTotal") = Val(dr.Item("GTotal").ToString) + Val(dr.Item(r.Item("ArticleLpoName")).ToString)
                Next
            End If

            ''Calculating Total(2)
            dr = dtFinal.NewRow
            dr.Item("First") = "TOTAL(2)"
            dtFinal.Rows.Add(dr)

            For r As Integer = IIf(Me.cmbType.SelectedIndex = 0, 3, 4) To dtFinal.Rows.Count - 2
                For colIndex As Integer = 1 To dtFinal.Columns.Count - 1
                    dr.Item(dtFinal.Columns(colIndex).ColumnName) = Val(dr.Item(dtFinal.Columns(colIndex).ColumnName).ToString) + Val(dtFinal.Rows(r)(dtFinal.Columns(colIndex).ColumnName).ToString)
                Next
            Next

            ''Calculating Difference of Totals
            dr = dtFinal.NewRow
            dr.Item("First") = "DIFFERENCE"
            dtFinal.Rows.Add(dr)

            Dim rind As Integer = IIf(Me.cmbType.SelectedIndex = 0, 2, 3)

            For colIndex As Integer = 1 To dtFinal.Columns.Count - 1
                dr.Item(dtFinal.Columns(colIndex).ColumnName) = Val(dtFinal.Rows(rind).Item(dtFinal.Columns(colIndex).ColumnName).ToString) - Val(dtFinal.Rows(dtFinal.Rows.Count - 2)(dtFinal.Columns(colIndex).ColumnName).ToString)
            Next

            Me.grdStock.DataSource = dtFinal

            For i As Integer = 1 To Me.grdStock.DisplayLayout.Bands(0).Columns.Count - 1
                Me.grdStock.DisplayLayout.Bands(0).Columns(i).Format = "n"
            Next

            Me.grdStock.Rows(IIf(Me.cmbType.SelectedIndex = 0, 2, 3)).Appearance.BackColor = Color.Aqua
            Me.grdStock.Rows(IIf(Me.cmbType.SelectedIndex = 0, 2, 3)).Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True

            Me.grdStock.Rows(Me.grdStock.Rows.Count - 2).Appearance.BackColor = Color.Aqua
            Me.grdStock.Rows(Me.grdStock.Rows.Count - 2).Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True

            Me.grdStock.Rows(Me.grdStock.Rows.Count - 1).Appearance.BackColor = Color.Yellow
            Me.grdStock.Rows(Me.grdStock.Rows.Count - 1).Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        'If Not Me.uicmbCategory.ActiveRow.Cells(0).Value > 0 Then
        '    msg_Error("Please select category")
        '    Me.uicmbCategory.Focus()
        '    Exit Sub
        'End If

        'If Not Me.uicmbType.ActiveRow.Cells(0).Value > 0 Then
        '    msg_Error("Please select type")
        '    Me.uicmbType.Focus()
        '    Exit Sub
        'End If

        'If Me.uitxtItemCode.Text = "" Then
        '    msg_Error("Please enter valid Item Code")
        '    Me.uitxtItemCode.Focus()
        '    Exit Sub

        'End If

        'If Me.uitxtItemName.Text = "" Then
        '    msg_Error("Please enter valid Item Name")
        '    Me.uitxtItemName.Focus()
        '    Exit Sub
        'End If

        'If Not Me.uicmbColor.ActiveRow.Cells(0).Value > 0 Then
        '    msg_Error("Please select color")
        '    Me.uicmbColor.Focus()
        '    Exit Sub
        'End If

        'If Not Me.uicmbSize.ActiveRow.Cells(0).Value > 0 Then
        '    msg_Error("Please select size")
        '    Me.uicmbSize.Focus()
        '    Exit Sub
        'End If


        ''If Not Me.uitxtPrice.Text > 0 Then
        ''    msg_Error("Please enter valid purchase price")
        ''    Me.uitxtPrice.Focus()
        ''    Exit Sub
        ''End If

        ''If Not Me.uitxtSalePrice.Text > 0 Then
        ''    msg_Error("Please enter valid sale price")
        ''    Me.uitxtSalePrice.Focus()
        ''    Exit Sub
        ''End If

        'If Not Me.uitxtPackQty.Text > 0 Then
        '    msg_Error("Please enter minimum pack quantity of 1")
        '    Me.uitxtPackQty.Focus()
        '    Exit Sub
        'End If

        'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub

        'Dim cm As New OleDbCommand

        'If Con.State = ConnectionState.Closed Then Con.Open()
        'cm.Connection = Con
        'Try
        '    If Me.SaveToolStripButton.Text = "&Save" Or Me.SaveToolStripButton.Text = "&Save" Then
        '        cm.CommandText = "insert into ArticleDefTable(ArticleCode,ArticleDescription,ArticleGroupId,SizeRangeId,ArticleColorId,ArticleTypeId,PurchasePrice,SalePrice,PackQty,StockLevel,Active,SortOrder,IsDate) values('" & Me.uitxtItemCode.Text & "','" & Me.uitxtItemName.Text & "'," & Me.uicmbCategory.ActiveRow.Cells(0).Value & "," & Me.uicmbSize.ActiveRow.Cells(0).Value & "," & Me.uicmbColor.ActiveRow.Cells(0).Value & "," & Me.uicmbType.ActiveRow.Cells(0).Value & "," & Me.uitxtPrice.Text & "," & Me.uitxtSalePrice.Text & "," & Me.uitxtPackQty.Text & "," & Me.uitxtStockLevel.Text & "," & IIf(Me.uichkActive.Checked = True, 1, 0) & "," & Me.uitxtSortOrder.Text & "," & Date.Today & ")"
        '    Else
        '        cm.CommandText = "update ArticleDefTable set ArticleCode='" & Me.uitxtItemCode.Text & "',ArticleDescription='" & Me.uitxtItemName.Text & "', ArticleGroupId='" & Me.uicmbCategory.ActiveRow.Cells(0).Value & "',SizeRangeId=" & Me.uicmbSize.ActiveRow.Cells(0).Value & ",articleColorId=" & Me.uicmbColor.ActiveRow.Cells(0).Value & ",ArticleTypeId=" & Me.uicmbType.ActiveRow.Cells(0).Value & ",PurchasePrice=" & Me.uitxtPrice.Text & ",SalePrice=" & Me.uitxtSalePrice.Text & ",PackQty=" & Me.uitxtPackQty.Text & ",StockLevel=" & Me.uitxtStockLevel.Text & ",Active=" & IIf(Me.uichkActive.Checked = True, 1, 0) & ",SortOrder=" & Me.uitxtSortOrder.Text & " where ArticleId=" & Me.CurrentId
        '    End If
        '    cm.ExecuteNonQuery()
        '    msg_Information(str_informSave)
        '    Me.CurrentId = 0
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'Finally
        '    Con.Close()
        'End Try
        'Me.RefreshForm()
    End Sub






    Private Sub RefreshDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshDataToolStripMenuItem.Click
        Me.BindGrid()
        GetSecurityRights()
    End Sub

    Private Sub ExportToExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        Me.SaveFileDialog1.Filter = "Excel Files|.xls"
        'Me.SaveFileDialog1.DefaultExt = ".xls"
        Me.SaveFileDialog1.FileName = "Category Wise Sales Report"
        Me.SaveFileDialog1.InitialDirectory = "C:\"
        If Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        If Me.SaveFileDialog1.FileName = "" Then Exit Sub

        Me.UltraGridExcelExporter1.Export(Me.grdStock, Me.SaveFileDialog1.FileName) '& ".xls") '"c:\temp.xls")
        System.Diagnostics.Process.Start(Me.SaveFileDialog1.FileName) '& ".xls")
    End Sub

    Private Sub PrintItemLedgerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim FromDate As Date = CDate("01-jan-2008")
        Dim ToDate As Date = Date.Now
        Dim opening As Integer = GetStockOpeningBalance(0, FromDate.Year & "-" & FromDate.Month & "-" & FromDate.Day & " 00:00:00")
        If Not Me.grdStock.Rows.Count > 0 Then msg_Error(str_ErrorNoRecordFound) : Exit Sub
        'Dim fromDate As String = Me.DateTimePicker1.Value.Year & "." & Me.DateTimePicker1.Value.Month & "." & Me.DateTimePicker1.Value.Day
        'Dim ToDate As String = Me.DateTimePicker2.Value.Year & "." & Me.DateTimePicker2.Value.Month & "." & Me.DateTimePicker2.Value.Day
        ' ShowReport("Ledger", "{SP_Rpt_Ledger.coa_detail_id}=" & Me.grdStock.ActiveRow.Cells(0).Value, fromDate, ToDate)
        rptDateRange.ReportName = rptDateRange.ReportList.ItemLedger
        rptDateRange.AccountId = Me.grdStock.ActiveRow.Cells(0).Value
        rptDateRange.ShowDialog()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.BindGrid()
        Me.GetSecurityRights()
    End Sub


    Private Sub GetSecurityRights()
        Try
            Dim dt As DataTable = GetFormRights(EnumForms.rptStockAccountsReport)
            If Not dt Is Nothing Then
                If Not dt.Rows.Count = 0 Then
                    If Me.SaveToolStripButton.Text = "Save" Or Me.SaveToolStripButton.Text = "&Save" Then
                        Me.SaveToolStripButton.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                    Else
                        Me.SaveToolStripButton.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                    End If
                    Me.DeleteToolStripButton.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                    Me.PrintToolStripButton.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                    Me.ContextMenuStrip1.Items("ExportToExcelToolStripMenuItem").Enabled = dt.Rows(0).Item("Export_Rights").ToString
                    Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                End If
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Function IsValidate() As Boolean
        Try
            If Me.dtpFrom.Value > Me.dtpTo.Value Then
                msg_Error("From Date can't be greate than To Date")
                Me.dtpFrom.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Me.grdStock.PrintPreview()
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.DtpFrom.Value = Date.Today
            Me.DtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.DtpFrom.Value = Date.Today.AddDays(-1)
            Me.DtpTo.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.DtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.DtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.DtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.DtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.DtpFrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.DtpTo.Value = Date.Today
        End If
    End Sub
End Class