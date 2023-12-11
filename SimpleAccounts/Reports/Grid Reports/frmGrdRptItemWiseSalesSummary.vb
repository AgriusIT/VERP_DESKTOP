Public Class frmGrdRptItemWiseSalesSummary
    Enum enmCustomer
        coa_detail_id
        customer
        code
        Director
        SalePerson
        city
        area
        Opening
        Count
    End Enum
    Enum enmItem
        ArticleId
        Item
    End Enum

    Private Sub frmGrdRptItemWiseSalesSummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdRptItemWiseSalesSummary_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Cursor = Cursors.WaitCursor
        Try

            'FillUltraDropDown(Me.cmbCustomer, "Select coa_detail_id, detail_title as [Customer],detail_code as [Code],sub_sub_title as [Account Head],CityName as [City],TerritoryName as [Area] From vwCOADetail WHERE detail_title <> '' AND account_type='Customer' ORDER BY 2 ASC")
            'Me.cmbCustomer.Rows(0).Activate()
            'If Me.cmbCustomer.DisplayLayout.Bands.Count > 0 Then
            '    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
            '    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(1).Width = 300
            '    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(2).Width = 150
            '    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(3).Width = 200
            '    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(4).Width = 150
            '    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(5).Width = 150
            'End If
            Me.DateTimePicker1.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.DateTimePicker2.Value = Date.Now

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub FillGrid(Optional ByVal Condition As String = "")
        Try


            Dim blnShowVendorOnSales As Boolean = False
            blnShowVendorOnSales = Boolean.Parse(getConfigValueByType("Show Vendor On Sales").ToString)

            Dim strSQL As String = String.Empty
            strSQL = "Select ArticleId, " & IIf(Me.rbtItemByCode.Checked = True, " ArticleCode + '|' + Convert(Varchar,ArticleId) as [Code] ", " ArticleDescription + '|' + Convert(Varchar,ArticleId) as [Item]") & " From ArticleDefView WHERE ArticleDescription <> '' AND ArticleId In(select DistInct ArticleDefId From SalesDetailTable) ORDER BY 2 ASC "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)

            strSQL = String.Empty
            'strSQL = "Select vwCoaDetail.coa_detail_id, detail_title as [Customer], detail_code as [Code],  CityName as [City], TerritoryName as [Area], IsNull(OpeningBalance.Opening,0) as Opening From vwCOADetail LEFT OUTER JOIN(Select tblVoucherDetail.coa_detail_id, SUM(IsNull(debit_amount,0)-isnull(credit_amount,0)) as Opening From tblVoucherDetail INNER JOIN tblVoucher On tblVoucher.Voucher_Id = tblVoucherDetail.voucher_Id WHERE (Convert(varchar,tblVoucher.Voucher_Date,102) < Convert(dateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102)) Group By tblVoucherDetail.coa_detail_id) OpeningBalance  On OpeningBalance.coa_detail_id = vwCOAdetail.coa_detail_id WHERE Account_Type " & IIf(blnShowVendorOnSales = False, " ='Customer'", " IN ('Customer','Vendor')") & " And vwCOADetail.coa_detail_id IN(Select DISTINCT CustomerCode From SalesMasterTable) ORDER BY 2 ASC"
            strSQL = "Select vwCoaDetail.coa_detail_id, detail_title as [Customer], detail_code as [Code],  empDirector.Employee_Name as [Director], empSalePerson.Employee_Name as [Sale Person], CityName as [City], TerritoryName as [Area], IsNull(OpeningBalance.Opening,0) as Opening From vwCOADetail LEFT OUTER JOIN tblDefEmployee empDirector on EmpDirector.Employee_Id = vwCOADetail.Director LEFT OUTER JOIN tblDefEmployee empSalePerson on empSalePerson.Employee_Id = vwCOADetail.SaleMan LEFT OUTER JOIN(Select tblVoucherDetail.coa_detail_id, SUM(IsNull(debit_amount,0)-isnull(credit_amount,0)) as Opening From tblVoucherDetail INNER JOIN tblVoucher On tblVoucher.Voucher_Id = tblVoucherDetail.voucher_Id  WHERE (Convert(varchar,tblVoucher.Voucher_Date,102) < Convert(dateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102)) Group By tblVoucherDetail.coa_detail_id) OpeningBalance  On OpeningBalance.coa_detail_id = vwCOAdetail.coa_detail_id WHERE Account_Type " & IIf(blnShowVendorOnSales = False, " ='Customer'", " IN ('Customer','Vendor')") & " And vwCOADetail.coa_detail_id IN(Select DISTINCT CustomerCode From SalesMasterTable) ORDER BY 2 ASC"
            Dim dtCustomer As New DataTable
            dtCustomer = GetDataTable(strSQL)
            If dtCustomer IsNot Nothing Then
                For Each r As DataRow In dt.Rows
                    If Not dtCustomer.Columns.Contains(r.Item(enmItem.Item).ToString) Then
                        dtCustomer.Columns.Add(r.Item(enmItem.ArticleId).ToString, GetType(System.Int32), r.Item(enmItem.ArticleId).ToString)
                        dtCustomer.Columns.Add(r.Item(enmItem.Item).ToString, GetType(System.String))
                        dtCustomer.Columns.Add(r.Item(enmItem.ArticleId).ToString & "^Qty", GetType(System.Double))
                        dtCustomer.Columns.Add(r.Item(enmItem.ArticleId).ToString & "^Amount", GetType(System.Double))
                    End If
                Next
            End If

            dtCustomer.AcceptChanges()
            For Each r As DataRow In dtCustomer.Rows
                For c As Integer = enmCustomer.Count To dtCustomer.Columns.Count - 4 Step 4
                    r.BeginEdit()
                    r(c + 2) = 0
                    r(c + 3) = 0
                    r.EndEdit()
                Next
            Next

            strSQL = String.Empty
            strSQL = " SELECT dbo.SalesDetailTable.ArticleDefId, dbo.SalesMasterTable.CustomerCode, SUM(IsNull(dbo.SalesDetailTable.Qty,0)) AS TotalQty, SUM((ISNULL(dbo.SalesDetailTable.Qty, 0) " _
                    & " * ISNULL(dbo.SalesDetailTable.Price, 0) + (ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0)  " _
                    & " * ISNULL(dbo.SalesDetailTable.Price, 0))) + (ISNULL(dbo.SalesDetailTable.SEDPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0)  " _
                    & " * ISNULL(dbo.SalesDetailTable.Price, 0))) AS TotalAmount FROM  dbo.SalesDetailTable INNER JOIN dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId " _
                    & " WHERE (Convert(Varchar,dbo.SalesMasterTable.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " _
                    & " GROUP BY dbo.SalesDetailTable.ArticleDefId, dbo.SalesMasterTable.CustomerCode"
            Dim dtData As New DataTable
            dtData = GetDataTable(strSQL)
            Dim dr() As DataRow
            If dtData IsNot Nothing Then
                If dtData.Rows.Count > 0 Then
                    Dim i As Integer = 0I
                    For Each r As DataRow In dtCustomer.Rows
                        dr = dtData.Select("CustomerCode=" & r.Item("coa_detail_id").ToString & "")
                        If dr IsNot Nothing Then
                            If dr.Length > 0 Then
                                For Each drFound As DataRow In dr
                                    'If (dtCustomer.Columns.IndexOf(drFound(0)) + 2) > enmCustomer.Count - 1 Then
                                    If Val(dtCustomer.Columns.IndexOf(drFound(0)) + 2) > 1 Then
                                        r.BeginEdit()
                                        r(dtCustomer.Columns.IndexOf(drFound(0)) + 2) = Val(drFound(2).ToString)
                                        r(dtCustomer.Columns.IndexOf(drFound(0)) + 3) = Val(drFound(3).ToString)
                                        r.EndEdit()
                                        i += 1
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If
            End If

            Dim strTotalQty As String = String.Empty
            Dim strTotalAmount As String = String.Empty

            For j As Integer = enmCustomer.Count To dtCustomer.Columns.Count - 4 Step 4
                If strTotalQty.Length > 0 Then
                    strTotalQty += " + [" & dtCustomer.Columns(j + 2).ColumnName & "]"
                Else
                    strTotalQty = "[" & dtCustomer.Columns(j + 2).ColumnName & "]"
                End If
                If strTotalAmount.Length > 0 Then
                    strTotalAmount += " + [" & dtCustomer.Columns(j + 3).ColumnName & "]"
                Else
                    strTotalAmount = "[" & dtCustomer.Columns(j + 3).ColumnName & "]"
                End If
            Next

            dtCustomer.Columns.Add("Total Qty", GetType(System.Double))
            dtCustomer.Columns.Add("Total Amount", GetType(System.Double))
            dtCustomer.Columns.Add("Total Expense", GetType(System.Double))
            dtCustomer.Columns.Add("Net Amount", GetType(System.Double))
            dtCustomer.Columns.Add("Other Adj", GetType(System.Double))
            dtCustomer.Columns.Add("Receipts Adj", GetType(System.Double))
            dtCustomer.Columns.Add("Cash Receipts", GetType(System.Double))
            dtCustomer.Columns.Add("Closing Balance", GetType(System.Double))

            For Each r As DataRow In dtCustomer.Rows
                r.BeginEdit()
                r("Total Qty") = 0
                r("Total Amount") = 0
                r("Total Expense") = 0
                r("Net Amount") = 0
                r("Cash Receipts") = 0
                r("Other Adj") = 0
                r("Receipts Adj") = 0
                r.EndEdit()
            Next

            strSQL = String.Empty
            strSQL = " SELECT CustomerCode, SUM(ISNULL(FuelExpense, 0) + ISNULL(OtherExpense, 0) + ISNULL(Adjustment, 0)) AS [Total Expense] " _
                     & " FROM dbo.SalesMasterTable WHERE (CONVERT(varchar, SalesDate, 102) BETWEEN Convert(datetime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND convert(datetime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102)) GROUP BY CustomerCode HAVING      (SUM(ISNULL(FuelExpense, 0) + ISNULL(OtherExpense, 0) + ISNULL(Adjustment, 0)) <> 0)"
            Dim dtExp As New DataTable
            dtExp = GetDataTable(strSQL)
            If dtExp IsNot Nothing Then
                If dtExp.Rows.Count > 0 Then
                    Dim drFound() As DataRow
                    For Each row As DataRow In dtCustomer.Rows
                        drFound = dtExp.Select("CustomerCode=" & row.Item("coa_detail_id").ToString & "")
                        If drFound IsNot Nothing Then
                            If drFound.Length > 0 Then
                                row.BeginEdit()
                                row("Total Expense") = Val(drFound(0)(1).ToString)
                                row.EndEdit()
                            End If
                        End If
                    Next
                End If
            End If

            strSQL = String.Empty
            strSQL = "SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) AS [Cash Receipts] " _
                    & " FROM dbo.tblVoucher INNER JOIN dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id " _
                    & " WHERE (dbo.tblVoucher.voucher_type_id In(3,5)) AND ( Convert(varchar, tblVoucher.Voucher_Date,102) BETWEEN Convert(datetime,'" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(dateTime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))" _
                    & " GROUP BY dbo.tblVoucherDetail.coa_detail_id "

            Dim dtCash As New DataTable
            dtCash = GetDataTable(strSQL)
            If dtCash IsNot Nothing Then
                If dtCash.Rows.Count > 0 Then
                    Dim drFound() As DataRow
                    For Each row As DataRow In dtCustomer.Rows
                        drFound = dtCash.Select("coa_detail_id=" & row.Item("coa_detail_id").ToString & "")
                        If drFound IsNot Nothing Then
                            If drFound.Length > 0 Then
                                row.BeginEdit()
                                row("Cash Receipts") = Val(drFound(0)(1).ToString)
                                row.EndEdit()
                            End If
                        End If
                    Next
                End If
            End If


            strSQL = String.Empty
            strSQL = "SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0)) AS [Cash Receipts] " _
                    & " FROM dbo.tblVoucher INNER JOIN dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id " _
                    & " WHERE (dbo.tblVoucher.Voucher_type_Id Not In(3,5,7)) AND ( Convert(varchar, tblVoucher.Voucher_Date,102) BETWEEN Convert(datetime,'" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(dateTime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))" _
                    & " GROUP BY dbo.tblVoucherDetail.coa_detail_id "
            Dim dtAdj As New DataTable
            dtAdj = GetDataTable(strSQL)
            If dtAdj IsNot Nothing Then
                If dtAdj.Rows.Count > 0 Then
                    Dim drFound() As DataRow
                    For Each row As DataRow In dtCustomer.Rows
                        drFound = dtAdj.Select("coa_detail_id=" & row.Item("coa_detail_id").ToString & "")
                        If drFound IsNot Nothing Then
                            If drFound.Length > 0 Then
                                row.BeginEdit()
                                row("Other Adj") = Val(drFound(0)(1).ToString)
                                row.EndEdit()
                            End If
                        End If
                    Next
                End If
            End If

            strSQL = String.Empty
            strSQL = "SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) AS [Receipts Adj] " _
                    & " FROM dbo.tblVoucher INNER JOIN dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id " _
                    & " WHERE (dbo.tblVoucher.Voucher_type_Id Not In(3,5,7)) AND ( Convert(varchar, tblVoucher.Voucher_Date,102) BETWEEN Convert(datetime,'" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(dateTime,'" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))" _
                    & " GROUP BY dbo.tblVoucherDetail.coa_detail_id "
            Dim dtAdj1 As New DataTable
            dtAdj1 = GetDataTable(strSQL)
            If dtAdj1 IsNot Nothing Then
                If dtAdj1.Rows.Count > 0 Then
                    Dim drFound() As DataRow
                    For Each row As DataRow In dtCustomer.Rows
                        drFound = dtAdj1.Select("coa_detail_id=" & row.Item("coa_detail_id").ToString & "")
                        If drFound IsNot Nothing Then
                            If drFound.Length > 0 Then
                                row.BeginEdit()
                                row("Receipts Adj") = Val(drFound(0)(1).ToString)
                                row.EndEdit()
                            End If
                        End If
                    Next
                End If
            End If


            dtCustomer.Columns("Total Qty").Expression = strTotalQty.ToString
            dtCustomer.Columns("Total Amount").Expression = strTotalAmount.ToString
            dtCustomer.Columns("Net Amount").Expression = "[Total Amount]-[Total Expense]"
            dtCustomer.Columns("Closing Balance").Expression = "(([Total Amount]+[Opening]+[Other Adj])-([Cash Receipts]+[Receipts Adj]+[Total Expense]))"
            dtCustomer.AcceptChanges()
            Me.grd.DataSource = dtCustomer
            Me.grd.RetrieveStructure()

            ApplyGrightSetting()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyGrightSetting(Optional ByVal Condition As String = "")
        Try
            If Me.grd.RootTable Is Nothing Then Exit Sub
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grd.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            Me.grd.RootTable.ColumnSetRowCount = 1
            Dim objCustomerColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            Dim objColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            Dim objTotalColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            objCustomerColumnSet = Me.grd.RootTable.ColumnSets.Add
            objCustomerColumnSet.ColumnCount = 7
            objCustomerColumnSet.Add(Me.grd.RootTable.Columns(enmCustomer.customer), 0, 0)
            objCustomerColumnSet.Add(Me.grd.RootTable.Columns(enmCustomer.code), 0, 1)
            objCustomerColumnSet.Add(Me.grd.RootTable.Columns(enmCustomer.Director), 0, 2)
            objCustomerColumnSet.Add(Me.grd.RootTable.Columns(enmCustomer.SalePerson), 0, 3)
            objCustomerColumnSet.Add(Me.grd.RootTable.Columns(enmCustomer.city), 0, 4)
            objCustomerColumnSet.Add(Me.grd.RootTable.Columns(enmCustomer.area), 0, 5)
            objCustomerColumnSet.Add(Me.grd.RootTable.Columns(enmCustomer.Opening), 0, 6)

            For c As Integer = enmCustomer.Count To Me.grd.RootTable.Columns.Count - 4 Step 4
                If Me.grd.RootTable.Columns(c).DataMember <> "Total Qty" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "Total Amount" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "Closing Balance" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "Cash Receipts" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "Total Expense" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "Other Adj" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "Net Amount" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "Receipts Adj" Then
                    Me.grd.RootTable.Columns(c).Visible = False
                    Me.grd.RootTable.Columns(c + 2).Caption = "Qty"
                    Me.grd.RootTable.Columns(c + 3).Caption = "Amount"


                    Me.grd.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 3).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                    Me.grd.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

                    Me.grd.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


                    Me.grd.RootTable.Columns(c + 2).FormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 3).FormatString = "N" & DecimalPointInValue

                    Me.grd.RootTable.Columns(c + 2).TotalFormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 3).TotalFormatString = "N" & DecimalPointInValue
                    If Not Me.grd.RootTable.Columns(c + 1).Caption.LastIndexOf("|") = -1 Then
                        Me.grd.RootTable.Columns(c + 1).Caption = Me.grd.RootTable.Columns(c + 1).Caption.Substring(0, Me.grd.RootTable.Columns(c + 1).Caption.LastIndexOf("|"))
                    End If
                    objColumnSet = Me.grd.RootTable.ColumnSets.Add
                    objColumnSet.ColumnCount = 2
                    objColumnSet.Caption = Me.grd.RootTable.Columns(c + 1).Caption
                    objColumnSet.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    objColumnSet.Add(Me.grd.RootTable.Columns(c + 2), 0, 0)
                    objColumnSet.Add(Me.grd.RootTable.Columns(c + 3), 0, 1)
                End If
            Next

            objTotalColumnSet = Me.grd.RootTable.ColumnSets.Add
            objTotalColumnSet.ColumnCount = 8
            objTotalColumnSet.Caption = "Totals"
            objTotalColumnSet.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            objTotalColumnSet.Add(Me.grd.RootTable.Columns("Total Qty"), 0, 0)
            objTotalColumnSet.Add(Me.grd.RootTable.Columns("Total Amount"), 0, 1)
            objTotalColumnSet.Add(Me.grd.RootTable.Columns("Total Expense"), 0, 2)
            objTotalColumnSet.Add(Me.grd.RootTable.Columns("Net Amount"), 0, 3)
            objTotalColumnSet.Add(Me.grd.RootTable.Columns("Other Adj"), 0, 4)
            objTotalColumnSet.Add(Me.grd.RootTable.Columns("Receipts Adj"), 0, 5)
            objTotalColumnSet.Add(Me.grd.RootTable.Columns("Cash Receipts"), 0, 6)
            objTotalColumnSet.Add(Me.grd.RootTable.Columns("Closing Balance"), 0, 7)

            Me.grd.RootTable.Columns("Total Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total Expense").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Net Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Cash Receipts").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Closing Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Other Adj").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Receipts Adj").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            Me.grd.RootTable.Columns("Other Adj").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Expense").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Net Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Cash Receipts").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Closing Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Receipts Adj").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Other Adj").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Expense").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Net Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Cash Receipts").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Closing Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Receipts Adj").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Opening").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Opening").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Opening").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Opening").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Opening").TotalFormatString = "N" & DecimalPointInQty

            Me.grd.RootTable.Columns("Other Adj").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Total Qty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Total Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Total Expense").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Net Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Cash Receipts").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Closing Balance").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Receipts Adj").FormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("Other Adj").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Total Qty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Total Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Total Expense").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Net Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Cash Receipts").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Closing Balance").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Receipts Adj").TotalFormatString = "N" & DecimalPointInValue

            grd.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True
            grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always

            For j As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                Me.grd.RootTable.Columns(j).FilterEditType = Janus.Windows.GridEX.FilterEditType.SameAsEditType
            Next
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Item Wise Sales Summary" & vbCrLf & "Date From: " & Me.DateTimePicker1.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.DateTimePicker1.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class