Public Class frmGrdRptCustomersItemsSummarySales
    Private _DateFrom As DateTime
    Private _DateTo As DateTime
    Private _dt As DataTable
    Private _dtCustData As DataTable
    Private _AccountId As Integer = 0I
    Enum enm
        ArticleId
        ArticleTypeName
        ArticleCode
        ArticleDescription
        Color
        Size
        Count
    End Enum
    Sub getItemData()
        Try
            _dt = GetDataTable("Select ArticleId, ArticleTypeName as Type, ArticleCode, ArticleDescription, ArticleColorName as Color, ArticleSizeName as Size From ArticleDefView")
            _dtCustData = GetDataTable("Select Distinct AccountId as coa_detail_id, tblCustomer.CustomerCode as detail_title From tblCustomer INNER JOIN SalesMasterTable On SalesMasterTable.CustomerCode = AccountId WHERE (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102))  " & IIf(_AccountId > 0, " And AccountId =" & _AccountId & "", "") & "")
            If _dt IsNot Nothing Then
                If _dtCustData IsNot Nothing Then
                    For Each r As DataRow In _dtCustData.Rows
                        If Not _dt.Columns.Contains(r(1)) Then
                            _dt.Columns.Add(r(0), GetType(System.Int16), r(0))
                            _dt.Columns.Add("Qty" & " (" & r(1).ToString & ")", GetType(System.Double))
                            _dt.Columns.Add("SchemeQty" & " (" & r(1).ToString & ")", GetType(System.Double))
                            _dt.Columns.Add("TotalQty" & " (" & r(1).ToString & ")", GetType(System.Double))
                        End If
                    Next
                End If
            End If
            _dt.AcceptChanges()
            Dim dtType As DataTable = GetDataTable("Select Id, AnalysisType, AnalysisDescription From tblDefAnalysisType")
            Dim drType As DataRow
            For Each row As DataRow In dtType.Rows
                drType = _dt.NewRow
                drType(0) = 0
                drType(1) = row(1)
                drType(2) = row(2)
                _dt.Rows.Add(drType)
            Next
            For Each r As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    r.BeginEdit()
                    r(i + 1) = 0
                    r(i + 2) = 0
                    r(i + 3) = 0
                    r.EndEdit()
                Next
            Next
            _dt.AcceptChanges()
            Dim dtSalesData As DataTable = GetDataTable("SELECT dbo.SalesDetailTable.ArticleDefId,dbo.SalesMasterTable.CustomerCode,  SUM(ISNULL(dbo.SalesDetailTable.Qty,0)) AS Qty, SUM(ISNULL(dbo.SalesDetailTable.SampleQty,0)) AS SchemeQty FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode, dbo.SalesDetailTable.ArticleDefId")
            Dim dr() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    dr = dtSalesData.Select("ArticleDefId='" & row.Item("ArticleId") & "' AND CustomerCode='" & row.Item(i) & "'")
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                            row(_dt.Columns.IndexOf(drFound(1)) + 2) = drFound(3)
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = (Val(drFound(2)) + Val(drFound(3)))
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            _dt.AcceptChanges()
            Dim dtDetail As DataTable = GetDataTable("SELECT 'Details' as Id, dbo.SalesMasterTable.CustomerCode, SUM(ISNULL(dbo.SalesDetailTable.Qty,0)) As Qty, SUM(ISNULL(SampleQty,0)) as SchemeQty  FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode") ' Update Sales After Discount
            Dim drDetail() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drDetail = dtDetail.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drDetail.Length > 0 Then
                        For Each drFound As DataRow In drDetail
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 1) = Val(drFound(2))
                            row(_dt.Columns.IndexOf(drFound(1)) + 2) = Val(drFound(3))
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = Val(drFound(2)) + Val(drFound(3))
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtSalesAfterDisc As DataTable = GetDataTable("SELECT 'Bill After Discount Value' as Id, dbo.SalesMasterTable.CustomerCode, SUM((ISNULL(dbo.SalesDetailTable.Qty,0)*ISNULL(CurrentPrice,0))-(((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100)) AS SalesAfterDiscount FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode") ' Update Sales After Discount
            Dim drSalesAfterDisc() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drSalesAfterDisc = dtSalesAfterDisc.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drSalesAfterDisc.Length > 0 Then
                        For Each drFound As DataRow In drSalesAfterDisc
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtSalesTax As DataTable = GetDataTable("SELECT 'Sales Tax Value' as Id, dbo.SalesMasterTable.CustomerCode, SUM((((ISNULL(dbo.SalesDetailTable.Qty,0)+ISNULL(SampleQty,0))*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100) AS SalesTax FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode") ' Update Sales Tax
            Dim drSalesTax() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drSalesTax = dtSalesTax.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drSalesTax.Length > 0 Then
                        For Each drFound As DataRow In drSalesTax
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtFreight As DataTable = GetDataTable("SELECT 'Freight Value' as Id, dbo.SalesMasterTable.CustomerCode, SUM((((ISNULL(dbo.SalesDetailTable.Qty,0)+ISNULL(SampleQty,0)))*ISNULL(Freight,0))) AS Freight FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode") ' Update Freight
            Dim drFreight() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drFreight = dtFreight.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drFreight.Length > 0 Then
                        For Each drFound As DataRow In drFreight
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtWHTax As DataTable = GetDataTable("SELECT 'With Holding Tax Value' as Id, dbo.SalesMasterTable.CustomerCode, SUM((((ISNULL(dbo.SalesDetailTable.Qty,0)*ISNULL(TradePrice,0))+(((ISNULL(SampleQty,0)*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100))*ISNULL(SEDPercent,0))/100) AS WHTax FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode") ' Update With Holding Tax 
            Dim drWHTax() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drWHTax = dtWHTax.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drWHTax.Length > 0 Then
                        For Each drFound As DataRow In drWHTax
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtBillIncTaxFreight As DataTable = GetDataTable("SELECT 'Bill Include Tax/Freight' as Id, dbo.SalesMasterTable.CustomerCode, SUM((ISNULL(dbo.SalesDetailTable.Qty,0)*ISNULL(CurrentPrice,0))-(((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100))+ SUM((((ISNULL(dbo.SalesDetailTable.Qty,0)+ISNULL(SampleQty,0))*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100)+ SUM((((ISNULL(dbo.SalesDetailTable.Qty,0)+ISNULL(SampleQty,0)))*ISNULL(Freight,0)))+SUM((((ISNULL(dbo.SalesDetailTable.Qty,0)*ISNULL(TradePrice,0))+(((ISNULL(SampleQty,0)*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100))*ISNULL(SEDPercent,0))/100) AS BillIncludeTaxFreight FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode") ' Update Bill Include Tax/Freight
            Dim drBillIncTaxFreight() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drBillIncTaxFreight = dtBillIncTaxFreight.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drBillIncTaxFreight.Length > 0 Then
                        For Each drFound As DataRow In drBillIncTaxFreight
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtBiltyCharges As DataTable = GetDataTable("SELECT 'Bilty Charges' as Id, dbo.SalesMasterTable.CustomerCode, SUM(ISNULL(FuelExpense,0)) AS FuelExpense FROM dbo.SalesMasterTable  WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode") ' Update Bilty Charges
            Dim drBiltyCharges() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drBiltyCharges = dtBiltyCharges.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drBiltyCharges.Length > 0 Then
                        For Each drFound As DataRow In drBiltyCharges
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtMarketReturns As DataTable = GetDataTable("SELECT 'Market Returns' as Id, dbo.SalesMasterTable.CustomerCode, SUM((((ISNULL(dbo.SalesDetailTable.Qty,0)+ISNULL(SampleQty,0)))*ISNULL(MarketReturns,0))) AS MarketReturns FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode") ' Update Market Returns
            Dim drMarketReturns() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drMarketReturns = dtMarketReturns.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drMarketReturns.Length > 0 Then
                        For Each drFound As DataRow In drMarketReturns
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtAdj As DataTable = GetDataTable("SELECT 'Special Adjustment' as Id, dbo.SalesMasterTable.CustomerCode, SUM(ISNULL(Adjustment,0)) AS Adjustment FROM dbo.SalesMasterTable  WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode") ' Update Adjustment
            Dim drAdj() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drAdj = dtAdj.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drAdj.Length > 0 Then
                        For Each drFound As DataRow In drAdj
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtTransit As DataTable = GetDataTable("SELECT 'Transit Insurance' as Id, dbo.SalesMasterTable.CustomerCode, SUM(ISNULL(TransitInsurance,0)) AS TransitInsurance FROM dbo.SalesMasterTable  WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode") ' Update Adjustment
            Dim drTransit() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drTransit = dtTransit.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drTransit.Length > 0 Then
                        For Each drFound As DataRow In drTransit
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtNetBill As DataTable = GetDataTable("Select Id, abc.CustomerCode, BillIncludeTaxFreight-ISNULL(Summ.BiltyCharges,0)-ISNULL(Summ.Adjustment,0)+ISNULL(Summ.TransitInsurance,0) From ( SELECT 'NET Bill' as Id, dbo.SalesMasterTable.CustomerCode, SUM((ISNULL(dbo.SalesDetailTable.Qty,0)*ISNULL(CurrentPrice,0))-(((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100))+ SUM((((ISNULL(dbo.SalesDetailTable.Qty,0)+ISNULL(SampleQty,0))*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100)+ SUM((((ISNULL(dbo.SalesDetailTable.Qty,0)+ISNULL(SampleQty,0)))*ISNULL(Freight,0)))+SUM((((ISNULL(dbo.SalesDetailTable.Qty,0)*ISNULL(TradePrice,0))+(((ISNULL(SampleQty,0)*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100))*ISNULL(SEDPercent,0))/100)-SUM(((ISNULL(Qty,0)+ISNULL(SampleQty,0))*ISNULL(MarketReturns,0))) AS BillIncludeTaxFreight FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId  WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode ) abc LEFT OUTER JOIN(Select CustomerCode, SUM(ISNULL(FuelExpense,0)) as BiltyCharges, SUM(ISNULL(Adjustment,0)) as Adjustment, SUM(ISNULL(TransitInsurance,0)) as TransitInsurance From SalesMasterTable WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By CustomerCode) Summ on Summ.CustomerCode = abc.CustomerCode") ' Update Bill Include Tax/Freight
            Dim drNetBill() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drNetBill = dtNetBill.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drNetBill.Length > 0 Then
                        For Each drFound As DataRow In drNetBill
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtDiscount As DataTable = GetDataTable("SELECT 'Discount Value' as Id, dbo.SalesMasterTable.CustomerCode, SUM((((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100)) AS Discount, ((SUM((((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100))/(SUM((ISNULL(Qty,0) * ISNULL(CurrentPrice,0)))-SUM((((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100))))*100) as Dic_Percent FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode") 'Total Discount
            Dim drDiscount() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drDiscount = dtDiscount.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drDiscount.Length > 0 Then
                        For Each drFound As DataRow In drDiscount
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row(_dt.Columns.IndexOf(drFound(1)) + 2) = drFound(3)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtScheme As DataTable = GetDataTable("Select Id, CustomerCode, SUM(Scheme) Scheme, CASE WHEN SUM(Scheme)=0 THEN 0 ELSE ((SUM(Scheme)/SUM(NRVALUE))*100) END as Scheme_Percent From ( " _
                                                    & " SELECT 'Scheme Value' as Id, SalesDetailTable.ArticleDefId, dbo.SalesMasterTable.CustomerCode, (ISNULL(SampleQty,0)*ISNULL(CurrentPrice,0)) AS Scheme, CASE WHEN Art.GST_Applicable=1 THEN (ISNULL(Qty,0)*ISNULL(Cost.CostPrice,0))+(((ISNULL(Qty,0)*ISNULL(Cost.CostPrice,0))*ISNULL(SalesDetailTable.TaxPercent,0))/100)+((ISNULL(SampleQty,0)+ISNULL(QTY,0))*ISNULL(Freight,0))+((ISNULL(SampleQty,0)+ISNULL(Qty,0))*ISNULL(MarketReturns,0)) ELSE (ISNULL(Qty,0)*ISNULL(Cost.CostPrice,0))+(((ISNULL(Qty,0)*ISNULL(Cost.CostPrice,0))*(ISNULL(Art.FlatRate,0)/100)))+((ISNULL(SampleQty,0)+ISNULL(QTY,0))*ISNULL(Freight,0))+((ISNULL(SampleQty,0)+ISNULL(Qty,0))*ISNULL(MarketReturns,0)) end as NRVALUE  FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId  " _
                                                    & " LEFT OUTER JOIN (Select ArticleDefId, ISNULL(PurchaseNewPrice,0) as CostPrice From IncrementReductionTable WHERE Id in(Select Max(Id) From IncrementReductionTable WHERE (Convert(Varchar, IncrementReductionDate,102) < Convert(DateTime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By ArticleDefId)) Cost On Cost.ArticleDefId = SalesDetailTable.ArticleDefId  " _
                                                    & " LEFT OUTER JOIN (Select ArticleId, GST_Applicable, FlatRate_Applicable, FlatRate From ArticleDefTable) Art ON Art.ArticleId = SalesDetailTable.ArticleDefId  " _
                                                    & " WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102))  " _
                                                    & " )abc Group By Id, CustomerCode") 'Total Discount
            Dim drScheme() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drScheme = dtScheme.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drScheme.Length > 0 Then
                        For Each drFound As DataRow In drScheme
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row(_dt.Columns.IndexOf(drFound(1)) + 2) = drFound(3)
                            row.EndEdit()
                        Next
                        End If
                Next
            Next
            Dim dtDistMargin As DataTable = GetDataTable("SELECT 'Distributor Margin' as Id, dbo.SalesMasterTable.CustomerCode, SUM(((ISNULL(Qty,0)*ISNULL(TradePrice,0)) + (((ISNULL(SampleQty,0)*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100)))-SUM(((((ISNULL(Qty,0)*ISNULL(CurrentPrice,0)) -(((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100)))))- SUM((((ISNULL(Qty,0)+ISNULL(SampleQty,0))*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100)- SUM((ISNULL(Qty,0)+ISNULL(SampleQty,0))*ISNULL(Freight,0)) as DistMargin, ((SUM(((ISNULL(Qty,0)*ISNULL(TradePrice,0)) + (((ISNULL(SampleQty,0)*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100))) -  SUM((((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100))-SUM(((((ISNULL(Qty,0)*ISNULL(CurrentPrice,0)) -(((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100)))))- SUM((((ISNULL(Qty,0)+ISNULL(SampleQty,0))*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100)- SUM((ISNULL(Qty,0)+ISNULL(SampleQty,0))*ISNULL(Freight,0))) /(SUM((((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100))-SUM(((((ISNULL(Qty,0)*ISNULL(CurrentPrice,0)) -(((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100)))))- SUM((((ISNULL(Qty,0)+ISNULL(SampleQty,0))*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100)- SUM((ISNULL(Qty,0)+ISNULL(SampleQty,0))*ISNULL(Freight,0)))*100) as DistMargin_Percent   FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.SalesMasterTable.CustomerCode") 'Total Discount
            Dim drDistMargin() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drDistMargin = dtDistMargin.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drDistMargin.Length > 0 Then
                        For Each drFound As DataRow In drDistMargin
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row(_dt.Columns.IndexOf(drFound(1)) + 2) = drFound(3)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtNRValue As DataTable = GetDataTable("Select Id, CustomerCode,  SUM(ISNULL(NRVALUE,0)) as NRValue,  (SUM(ISNULL(NRVALUE,0))/SUM(ISNULL(NetBill,0))*100) as NetBill_Percent  From (  " _
                                                  & "  SELECT 'I 2Value' as Id, SalesDetailTable.ArticleDefId, dbo.SalesMasterTable.CustomerCode, ((ISNULL(Qty,0)+ISNULL(SampleQty,0))*ISNULL(Cost.CostPrice,0))+((((ISNULL(Qty,0)+ISNULL(SampleQty,0))*ISNULL(CurrentPrice,0))*ISNULL(SalesDetailTable.TaxPercent,0))/100)+((ISNULL(SampleQty,0)+ISNULL(QTY,0))*ISNULL(Freight,0))+((ISNULL(SampleQty,0)+ISNULL(Qty,0))*ISNULL(MarketReturns,0)) as NRVALUE, ((ISNULL(dbo.SalesDetailTable.Qty,0)*ISNULL(CurrentPrice,0))-(((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100))+ ((((ISNULL(dbo.SalesDetailTable.Qty,0)+ISNULL(SampleQty,0))*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100)+ ((((ISNULL(dbo.SalesDetailTable.Qty,0)+ISNULL(SampleQty,0)))*ISNULL(Freight,0)))  as NetBill FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId   " _
                                                  & "  LEFT OUTER JOIN (Select ArticleDefId, ISNULL(PurchaseNewPrice,0) as CostPrice From IncrementReductionTable WHERE Id in(Select Max(Id) From IncrementReductionTable WHERE (Convert(Varchar, IncrementReductionDate,102) < Convert(DateTime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By ArticleDefId)) Cost On Cost.ArticleDefId = SalesDetailTable.ArticleDefId  " _
                                                  & "  LEFT OUTER JOIN (Select ArticleId, GST_Applicable, FlatRate_Applicable, FlatRate From ArticleDefTable) Art ON Art.ArticleId = SalesDetailTable.ArticleDefId " _
                                                  & "  WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
                                                  & "  )abc Group By Id, CustomerCode") 'Total Discount
            Dim drNRValue() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drNRValue = dtNRValue.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drNRValue.Length > 0 Then
                        For Each drFound As DataRow In drNRValue
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row(_dt.Columns.IndexOf(drFound(1)) + 2) = drFound(3)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            Dim dtNRValue2 As DataTable = GetDataTable("Select Id, CustomerCode,  ISNULL(SUM(NRVALUE),0) as NRValue,  ((ISNULL(SUM(NRVALUE),0)/ISNULL(SUM(NetBill),0))*100) as NetBill_Percent  From (  " _
                                                  & "  SELECT 'I 2Value Plus With Holding Tax' as Id, SalesDetailTable.ArticleDefId, dbo.SalesMasterTable.CustomerCode, ((ISNULL(Qty,0)+ISNULL(SampleQty,0))*ISNULL(Cost.CostPrice,0))+((((ISNULL(Qty,0)+ISNULL(SampleQty,0))*ISNULL(CurrentPrice,0))*ISNULL(SalesDetailTable.TaxPercent,0))/100)+((ISNULL(SampleQty,0)+ISNULL(QTY,0))*ISNULL(Freight,0))+((ISNULL(SampleQty,0)+ISNULL(Qty,0))*ISNULL(MarketReturns,0))+((((ISNULL(dbo.SalesDetailTable.Qty,0)*ISNULL(TradePrice,0))+(((ISNULL(SampleQty,0)*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100))*ISNULL(SEDPercent,0))/100) as NRVALUE, ((ISNULL(dbo.SalesDetailTable.Qty,0)*ISNULL(CurrentPrice,0))-(((ISNULL(Qty,0)*ISNULL(CurrentPrice,0))*ISNULL(Discount_Percentage,0))/100))+ ((((ISNULL(dbo.SalesDetailTable.Qty,0)+ISNULL(SampleQty,0))*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100)+ ((((ISNULL(dbo.SalesDetailTable.Qty,0)+ISNULL(SampleQty,0)))*ISNULL(Freight,0)))+((((ISNULL(dbo.SalesDetailTable.Qty,0)*ISNULL(TradePrice,0))+(((ISNULL(SampleQty,0)*ISNULL(CurrentPrice,0))*ISNULL(TaxPercent,0))/100))*ISNULL(SEDPercent,0))/100)  as NetBill FROM dbo.SalesMasterTable INNER JOIN dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId   " _
                                                  & "  LEFT OUTER JOIN (Select ArticleDefId, ISNULL(PurchaseNewPrice,0) as CostPrice From IncrementReductionTable WHERE Id in(Select Max(Id) From IncrementReductionTable WHERE (Convert(Varchar, IncrementReductionDate,102) < Convert(DateTime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By ArticleDefId)) Cost On Cost.ArticleDefId = SalesDetailTable.ArticleDefId   " _
                                                  & "  LEFT OUTER JOIN (Select ArticleId, GST_Applicable, FlatRate_Applicable, FlatRate From ArticleDefTable) Art ON Art.ArticleId = SalesDetailTable.ArticleDefId  " _
                                                  & "  WHERE (Convert(Varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "', 102))  " _
                                                  & "  )abc Group By Id, CustomerCode") 'Total Discount
            Dim drNRValue2() As DataRow
            For Each row As DataRow In _dt.Rows
                For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                    drNRValue2 = dtNRValue2.Select("CustomerCode='" & row.Item(i) & "' AND Id='" & row.Item("ArticleCode") & "'")
                    If drNRValue2.Length > 0 Then
                        For Each drFound As DataRow In drNRValue2
                            row.BeginEdit()
                            row(_dt.Columns.IndexOf(drFound(1)) + 3) = drFound(2)
                            row(_dt.Columns.IndexOf(drFound(1)) + 2) = drFound(3)
                            row.EndEdit()
                        Next
                    End If
                Next
            Next
            _dt.AcceptChanges()
            If _AccountId = 0 Then
                _dt.Columns.Add("Total", GetType(System.Double))
                Dim dblTotal As Double = 0D
                For Each rTotal As DataRow In _dt.Rows
                    For i As Integer = enm.Count To _dt.Columns.Count - 3 Step 4
                        dblTotal += rTotal(i + 3)
                    Next
                    rTotal.BeginEdit()
                    rTotal("Total") = dblTotal
                    rTotal.EndEdit()
                    dblTotal = 0D
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            getItemData()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptCustomersItemsSummarySales_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmGrdRptCustomersItemsSummarySales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim Str As String = "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City, isnull(Active,0) as Active FROM dbo.vwCOADetail WHERE (coa_detail_id > 0) "
            ''Start TFS2124
            If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                Str += " AND (Account_Type = 'Customer')  "
            Else
                Str += " AND (Account_Type in('Customer','Vendor'))  "
            End If
            Str += " ORDER BY detail_title"
            ''End TFS2124
            FillUltraDropDown(cmbAccount, Str)
            If Me.cmbAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Active").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("sub_sub_title").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("account_type").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("sub_title").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("main_title").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("main_type").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("main_type").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("detail_title").Header.Caption = "Account Description"
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("detail_code").Header.Caption = "Account Code"
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("detail_title").Width = 250
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("detail_code").Width = 150
            End If
            For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.cmbAccount.Rows
                If row.Index > 0 Then
                    If row.Cells("Active").Value = False Then
                        row.Appearance.BackColor = Color.LightYellow
                    End If
                End If
            Next
            Me.cmbAccount.Rows(0).Activate()
            Me.cmbPeriod.Text = "Current Month"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptCustomersItemsSummarySales_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim lbl As New Label
        Try
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()

            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            If _dt IsNot Nothing Then Me.GridEX1.DataSource = _dt
            Me.GridEX1.RetrieveStructure()

            ApplyGridSettings()
          
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim lbl As New Label
        Try
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()
            _DateFrom = Me.dtpFrom.Value
            _DateTo = Me.dtpTo.Value
            _AccountId = Me.cmbAccount.ActiveRow.Cells(0).Value
            'If _AccountId > 0 Then
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            If _dt IsNot Nothing Then Me.GridEX1.DataSource = _dt
            Me.GridEX1.RetrieveStructure()
            ApplyGridSettings()
            'Else
            'Exit Sub
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub
    Private Sub ApplyGridSettings()
        Try
            Me.GridEX1.RootTable.Columns(0).Visible = False
            Me.GridEX1.RootTable.Columns(1).Visible = False
            For i As Integer = enm.Count To Me.GridEX1.RootTable.Columns.Count - 3 Step 4
                Me.GridEX1.RootTable.Columns(i).Visible = False
                Me.GridEX1.RootTable.Columns(i).EditType = Janus.Windows.GridEX.EditType.TextBox
                Me.GridEX1.RootTable.Columns(i + 1).AllowSort = False
                Me.GridEX1.RootTable.Columns(i + 3).CellStyle.BackColor = Color.Ivory
                Me.GridEX1.RootTable.Columns(i + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(i + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(i + 3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(i + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(i + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(i + 3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(i + 1).FormatString = "N0"
                Me.GridEX1.RootTable.Columns(i + 2).FormatString = "N0"
                Me.GridEX1.RootTable.Columns(i + 3).FormatString = "N0"
            Next
            If _AccountId > 0 Then
                For c As Integer = 0 To enm.Count - 1
                    Me.GridEX1.RootTable.Columns(c).AllowSort = False
                Next
            End If
            Me.GridEX1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
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
            _DateFrom = Me.dtpFrom.Value
            _DateTo = Me.dtpTo.Value
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0
            FillUltraDropDown(Me.cmbAccount, "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City, isnull(Active,0) as Active FROM dbo.vwCOADetail WHERE (coa_detail_id > 0) AND account_type='Customer' order by detail_title")
            If Me.cmbAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Active").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("sub_sub_title").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("account_type").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("sub_title").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("main_title").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("main_type").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("main_type").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("detail_title").Header.Caption = "Account Description"
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("detail_code").Header.Caption = "Account Code"
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("detail_title").Width = 250
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("detail_code").Width = 150
            End If
            For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.cmbAccount.Rows
                If row.Index > 0 Then
                    If row.Cells("Active").Value = False Then
                        row.Appearance.BackColor = Color.LightYellow
                    End If
                End If
            Next
            Me.cmbAccount.ActiveRow.Cells(0).Value = id
            'btnGenerate_Click(Nothing, Nothing)
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Sales Analysis Summary" & Chr(10) & "Date From:" & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & " "

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFrom.ValueChanged, dtpTo.ValueChanged
        Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Sales Analysis Summary" & vbCrLf & "Date From:" & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & " "
    End Sub

    Private Sub GridEX1_LoadingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GridEX1.LoadingRow
        Try
            If Me.GridEX1.RowCount = 0 Then Exit Sub
            If e.Row.Cells(0).Value = 0 Then
                Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowStyle.BackColor = Color.LightCyan
                e.Row.RowStyle = rowStyle
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged, rbtCode.CheckedChanged
        Try
            If Not Me.cmbAccount.IsItemInList = Nothing Then
                If Me.RadioButton1.Checked = True Then
                    Me.cmbAccount.DisplayMember = cmbAccount.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbAccount.DisplayMember = cmbAccount.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class