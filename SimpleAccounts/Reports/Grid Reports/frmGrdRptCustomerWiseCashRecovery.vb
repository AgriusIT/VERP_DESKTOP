Public Class frmGrdRptCustomerWiseCashRecovery

    Enum enmCustomer
        coa_detail_id
        Customer
        Code
        CustomerType
        CustomerCode
        City
        OpeningBalance
        Count
    End Enum
    Private _DateFrom As DateTime
    Private _DateTo As DateTime

    Public Sub FillCombo()
        Try

            FillUltraDropDown(Me.cmbCustomer, "Select vwCOADetail.coa_detail_id, detail_title as [Customer], detail_code as Code, Account_type as Type, Sub_Sub_Title as [Account Head], Contact_Mobile as Mobile, CityName as City From vwCOADetail  where detail_title <> '' and account_type in('Customer')")
            Me.cmbCustomer.Rows(0).Activate()
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillGridWeekly()
        Try
            Dim dtData As New DataTable
            Dim mydt As New DataTable
            dtData = GetDataTable("Select vwCOADetail.coa_detail_id, vwCOADetail.detail_title as [Customer], vwCOADetail.detail_code as Code,CustomerType,CustomerCode, CityName as City, IsNull(OpeningBalance,0) as OpeningBalance From vwCOADetail LEFT OUTER JOIN(Select tblVoucherdetail.coa_detail_id, SUM(debit_amount-credit_amount) as OpeningBalance From tblVoucherDetail INNER JOIN tblVoucher on tblVoucher.voucher_id = tblVoucherdetail.voucher_id where (Convert(varchar,Voucher_Date,102) < Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)) Group by tblvoucherdetail.coa_detail_id) Op on Op.coa_detail_id = vwCOADetail.coa_detail_id WHERE vwcoadetail.Detail_title <> '' " & IIf(Me.cmbCustomer.Value > 0, "  AND vwcoadetail.coa_detail_id=" & Me.cmbCustomer.Value & "", "") & " and account_type='Customer'  Order By vwcoadetail.detail_title ASC")
            dtData.AcceptChanges()
            Dim i As Integer = 0I



            'While Me.dtpFromDate.Value.AddDays(i).Date <= Me.dtpToDate.Value.Date
            '    If Not dtData.Columns.Contains(Me.dtpFromDate.Value.AddDays(i).Date) Then
            '        dtData.Columns.Add(Me.dtpFromDate.Value.AddDays(i).Date, GetType(System.DateTime))
            '        dtData.Columns.Add(i & "^" & "Sale", GetType(System.Double))
            '        dtData.Columns.Add(i & "^" & "Recovery", GetType(System.Double))
            '    End If
            '    i += 1
            'End While

            'Task#09092015 Weekly, Monthly show by ahmad sharif

            'Dim days As Integer = 0I
            'Dim totalWeek As Double = 0I
            'Dim ts As TimeSpan = Me.dtpToDate.Value.Subtract(Me.dtpFromDate.Value)
            'days = Convert.ToInt32(ts.Days) + 2 'alwasy add 2 in days,because start and end date missing
            'totalWeek = DateDiff("w", Me.dtpFromDate.Value, Me.dtpToDate.Value)     'return no of weeks b/w two dates
            ''totalWeek = days / 7
            'Dim remDays As Integer = (days Mod 7)
            Dim weekNo As Integer = 1
            'Dim d As Integer = (Convert.ToInt32(totalWeek) * 7)

            If Me.rbtnDaily.Checked = True Then
                While Me.dtpFromDate.Value.AddDays(i).Date <= Me.dtpToDate.Value.Date
                    If Not dtData.Columns.Contains(Me.dtpFromDate.Value.AddDays(i).Date) Then
                        dtData.Columns.Add(Me.dtpFromDate.Value.AddDays(i).Date, GetType(System.DateTime))
                        dtData.Columns.Add(i & "^" & "Sale", GetType(System.Double))
                        dtData.Columns.Add(i & "^" & "Recovery", GetType(System.Double))
                    End If
                    i += 1
                End While
            ElseIf Me.rbtnWeekly.Checked = True Then
                i = 0
                Dim k As Integer = 0I

                mydt = New DataTable
                mydt.Columns.Add("WeekNo", GetType(System.String))
                mydt.Columns.Add("From", GetType(DateTime))
                mydt.Columns.Add("To", GetType(DateTime))

                While Me.dtpFromDate.Value.AddDays(i).Date < Me.dtpToDate.Value.Date
                    Dim row As DataRow
                    If i = 0 Then
                        If Not dtData.Columns.Contains(Me.dtpFromDate.Value.AddDays(i).Date) Then
                            row = mydt.NewRow
                            'dtData.Columns.Add("W-" & weekNo & "( " & Me.dtpFromDate.Value.AddDays(i).Date & " to " & IIf(Me.dtpFromDate.Value.AddDays(i + 6).Date < Me.dtpToDate.Value.Date, Me.dtpFromDate.Value.AddDays(i + 6).Date, Me.dtpToDate.Value.Date) & " )", GetType(System.DateTime))
                            'dtData.Columns.Add(Me.dtpFromDate.Value.AddDays(i).Date & " | " & IIf(Me.dtpFromDate.Value.AddDays(i + 6).Date < Me.dtpToDate.Value.Date, Me.dtpFromDate.Value.AddDays(i + 6).Date, Me.dtpToDate.Value.Date), GetType(System.DateTime))
                            dtData.Columns.Add("Week-" & weekNo & "(" & Me.dtpFromDate.Value.AddDays(i).Date & " | " & IIf(Me.dtpFromDate.Value.AddDays(i + 6).Date < Me.dtpToDate.Value.Date, Me.dtpFromDate.Value.AddDays(i + 6).Date, Me.dtpToDate.Value.Date) & ")", GetType(System.DateTime))
                            dtData.Columns.Add(k & "^" & "Sale", GetType(System.Double))
                            dtData.Columns.Add(k & "^" & "Recovery", GetType(System.Double))
                            row(0) = "Week-" & k
                            row(1) = Me.dtpFromDate.Value.AddDays(i).Date.ToString("dd/MMM/yyyy")
                            row(2) = IIf(Me.dtpFromDate.Value.AddDays(i + 6).Date < Me.dtpToDate.Value.Date, Me.dtpFromDate.Value.AddDays(i + 6).Date.ToString("dd/MMM/yyyy"), Me.dtpToDate.Value.Date.ToString("dd/MMM/yyyy"))
                            mydt.Rows.Add(row)
                            mydt.AcceptChanges()
                        End If
                    Else
                        If Not dtData.Columns.Contains(Me.dtpFromDate.Value.AddDays(i).Date) Then
                            row = mydt.NewRow
                            'dtData.Columns.Add("W-" & weekNo & "( " & Me.dtpFromDate.Value.AddDays(i + 1).Date & " to " & IIf(Me.dtpFromDate.Value.AddDays(i + 6).Date < Me.dtpToDate.Value.Date, Me.dtpFromDate.Value.AddDays(i + 6).Date, Me.dtpToDate.Value.Date) & " )", GetType(System.DateTime))
                            'dtData.Columns.Add(Me.dtpFromDate.Value.AddDays(i + 1).Date & " | " & IIf(Me.dtpFromDate.Value.AddDays(i + 6).Date < Me.dtpToDate.Value.Date, Me.dtpFromDate.Value.AddDays(i + 6).Date, Me.dtpToDate.Value.Date), GetType(System.DateTime))
                            dtData.Columns.Add("Week-" & weekNo & "(" & Me.dtpFromDate.Value.AddDays(i + 1).Date & " | " & IIf(Me.dtpFromDate.Value.AddDays(i + 6).Date < Me.dtpToDate.Value.Date, Me.dtpFromDate.Value.AddDays(i + 6).Date, Me.dtpToDate.Value.Date) & ")", GetType(System.DateTime))
                            dtData.Columns.Add(k & "^" & "Sale", GetType(System.Double))
                            dtData.Columns.Add(k & "^" & "Recovery", GetType(System.Double))
                            row(0) = "Week-" & k
                            row(1) = Me.dtpFromDate.Value.AddDays(i).Date.ToString("dd/MMM/yyyy")
                            row(2) = IIf(Me.dtpFromDate.Value.AddDays(i + 6).Date < Me.dtpToDate.Value.Date, Me.dtpFromDate.Value.AddDays(i + 6).Date.ToString("dd/MMM/yyyy"), Me.dtpToDate.Value.Date.ToString("dd/MMM/yyyy"))
                            mydt.Rows.Add(row)
                            mydt.AcceptChanges()
                        End If
                    End If
                    i += 6
                    k += 1
                End While

            Else
                'i = 0I
                'Dim k As Integer = 0I
                'While Me.dtpFromDate.Value.AddDays(i).Date < Me.dtpToDate.Value.Date
                '    Dim myDate As Date = Me.dtpFromDate.Value.AddDays(i).Date
                '    Dim daysInMonth As Integer = Date.DaysInMonth(myDate.Year, myDate.Month)
                '    Dim lastDateOfMonth As Date = New Date(myDate.Year, myDate.Month, daysInMonth)

                '    If i = 0 Then
                '        If Not dtData.Columns.Contains(Me.dtpFromDate.Value.AddDays(i).Date) Then
                '            dtData.Columns.Add(Me.dtpFromDate.Value.AddDays(i).Date & " | " & IIf(Me.dtpToDate.Value.Date > lastDateOfMonth, lastDateOfMonth, Me.dtpToDate.Value.Date), GetType(System.DateTime))
                '            dtData.Columns.Add(k & "^" & "Sale", GetType(System.Double))
                '            dtData.Columns.Add(k & "^" & "Recovery", GetType(System.Double))
                '        End If
                '    Else
                '        If Not dtData.Columns.Contains(Me.dtpFromDate.Value.AddDays(i).Date) Then
                '            dtData.Columns.Add(Me.dtpFromDate.Value.AddDays(i + 1).Date & " | " & IIf(Me.dtpToDate.Value.Date > lastDateOfMonth, lastDateOfMonth, Me.dtpToDate.Value.Date), GetType(System.DateTime))
                '            dtData.Columns.Add(k & "^" & "Sale", GetType(System.Double))
                '            dtData.Columns.Add(k & "^" & "Recovery", GetType(System.Double))
                '        End If
                '    End If

                '    i += 1
                '    k += 1

                '    Me.dtpFromDate.Value = lastDateOfMonth.Date
                'End While
            End If

            'End Task#09092015

            dtData.AcceptChanges()
            For Each r As DataRow In dtData.Rows
                r.BeginEdit()
                For c As Integer = enmCustomer.Count To dtData.Columns.Count - 3 Step 3
                    r(c + 1) = 0
                    r(c + 2) = 0
                Next
                r.EndEdit()
            Next
            dtData.AcceptChanges()
            Dim strTotalSales As String = String.Empty
            Dim strTotalReceiving As String = String.Empty
            For c As Integer = enmCustomer.Count To dtData.Columns.Count - 3 Step 3
                If strTotalSales.Length > 0 Then
                    strTotalSales += "+" & "[" & dtData.Columns(c + 1).ColumnName.ToString & "]" & ""
                Else
                    strTotalSales = "[" & dtData.Columns(c + 1).ColumnName.ToString & "]" & ""
                End If
                If strTotalReceiving.Length > 0 Then
                    strTotalReceiving += "+" & "[" & dtData.Columns(c + 2).ColumnName.ToString & "]" & ""
                Else
                    strTotalReceiving = "[" & dtData.Columns(c + 2).ColumnName.ToString & "]" & ""
                End If
            Next
            Dim dt As New DataTable
            'dt = GetDataTable("SELECT ISNULL(Sale.SalesAmount, 0) + ISNULL(Tax.SalesTax, 0) AS InvoiceAmount, Recv.ReceivedAmount, Sale.CustomerCode, Sale.SaleDate " _
            '  & " FROM (SELECT CONVERT(DateTime, CONVERT(varchar, SalesMasterTable.SalesDate, 102), 102) AS SaleDate, SUM(ISNULL(SalesAmount, 0)) AS SalesAmount, CustomerCode " _
            '  & " FROM dbo.SalesMasterTable  " _
            '  & " WHERE (Convert(Varchar,SalesMasterTable.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
            '  & " GROUP BY CONVERT(DateTime, CONVERT(varchar, SalesDate, 102), 102), CustomerCode) AS Sale LEFT OUTER JOIN  " _
            '  & " (SELECT  SalesMasterTable_1.CustomerCode, CONVERT(dateTime, CONVERT(varchar, SalesMasterTable_1.SalesDate, 102), 102) AS SalesDate,  " _
            '  & " SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)))  " _
            '  & " AS SalesTax FROM dbo.SalesDetailTable INNER JOIN dbo.SalesMasterTable AS SalesMasterTable_1 ON dbo.SalesDetailTable.SalesId = SalesMasterTable_1.SalesId   WHERE (Convert(Varchar,SalesMasterTable_1.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
            '  & " GROUP BY SalesMasterTable_1.CustomerCode, CONVERT(dateTime, CONVERT(varchar, SalesMasterTable_1.SalesDate, 102), 102)  " _
            '  & " HAVING (SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0))) <> 0))  " _
            '  & " AS Tax ON Sale.CustomerCode = Tax.CustomerCode AND Tax.SalesDate = Sale.SaleDate LEFT OUTER JOIN " _
            '  & " (SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) AS ReceivedAmount, CONVERT(DateTime, CONVERT(Varchar,  " _
            '  & " dbo.tblVoucher.voucher_date, 102), 102) AS ReceivingDate FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id WHERE (Convert(Varchar,Voucher_Date,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND tblVoucherDetail.coa_detail_id=" & Me.cmbCustomer.Value & "", "") & " " _
            '  & " GROUP BY dbo.tblVoucherDetail.coa_detail_id, CONVERT(DateTime, CONVERT(Varchar, dbo.tblVoucher.voucher_date, 102), 102)) AS Recv ON  " _
            '  & " Recv.coa_detail_id = Sale.CustomerCode And Recv.ReceivingDate = Sale.SaleDate ORDER BY Sale.SaleDate ")


            'Task#10192015 split date columns by ahmad
            'Dim dateSplit() As String = Nothing
            'i = 1
            'Dim splitedDates As New DataTable
            'splitedDates.Columns.Add("WeekNo", GetType(System.String))
            'splitedDates.Columns.Add("From", GetType(DateTime))
            'splitedDates.Columns.Add("To", GetType(DateTime))

            'For Each col As DataColumn In dtData.Columns
            '    If col.Caption <> "coa_detail_id" AndAlso col.Caption <> "Customer" AndAlso col.Caption <> "CustomerCode" AndAlso col.Caption <> "Code" AndAlso col.Caption <> "CustomerType" AndAlso col.Caption <> "City" AndAlso col.Caption <> "OpeningBalance" AndAlso col.Caption <> "Net Sales" AndAlso col.Caption <> "Total Receiving" AndAlso col.Caption <> "Balance" AndAlso col.Caption <> "0^Sale" AndAlso col.Caption <> "0^Recovery" Then
            '        dateSplit = col.Caption.Trim.Split("|")
            '        Dim spRow As DataRow
            '        spRow = splitedDates.NewRow
            '        spRow(0) = "Week-" & i
            '        spRow(1) = dateSplit(0).ToString
            '        spRow(2) = dateSplit(1).ToString
            '        splitedDates.Rows.Add(spRow)
            '        splitedDates.AcceptChanges()
            '        i += 1
            '    End If
            'Next
            'splitedDates.AcceptChanges()
            'End Task#10192015

            'dt = GetDataTable("SELECT ISNULL(Sale.SalesAmount, 0) + ISNULL(Tax.SalesTax, 0) AS InvoiceAmount, 0 as ReceivedAmount, Sale.CustomerCode, Sale.SaleDate " _
            '                & " FROM (SELECT CONVERT(DateTime, CONVERT(varchar, SalesMasterTable.SalesDate, 102), 102) AS SaleDate, SUM(ISNULL(SalesAmount, 0)) AS SalesAmount, CustomerCode " _
            '                & " FROM dbo.SalesMasterTable  " _
            '                & " WHERE (Convert(Varchar,SalesMasterTable.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
            '                & " GROUP BY CONVERT(DateTime, CONVERT(varchar, SalesDate, 102), 102), CustomerCode) AS Sale LEFT OUTER JOIN  " _
            '                & " (SELECT  SalesMasterTable_1.CustomerCode, CONVERT(dateTime, CONVERT(varchar, SalesMasterTable_1.SalesDate, 102), 102) AS SalesDate,  " _
            '                & " SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)))  " _
            '                & " AS SalesTax FROM dbo.SalesDetailTable INNER JOIN dbo.SalesMasterTable AS SalesMasterTable_1 ON dbo.SalesDetailTable.SalesId = SalesMasterTable_1.SalesId   WHERE (Convert(Varchar,SalesMasterTable_1.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
            '                & " GROUP BY SalesMasterTable_1.CustomerCode, CONVERT(dateTime, CONVERT(varchar, SalesMasterTable_1.SalesDate, 102), 102)  " _
            '                & " HAVING (SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0))) <> 0))  " _
            '                & " AS Tax ON Sale.CustomerCode = Tax.CustomerCode AND Tax.SalesDate = Sale.SaleDate  ORDER BY Sale.SaleDate ")
            'dt.AcceptChanges()
            'For Each dRow As DataRow In dtData.Rows
            '    dRow.BeginEdit()
            '    Dim dr() As DataRow = dt.Select("CustomerCode=" & Val(dRow.Item("coa_detail_id").ToString) & "")
            '    If dr IsNot Nothing Then
            '        For Each drFound As DataRow In dr
            '            If drFound IsNot Nothing Then
            '                dRow.Item(dtData.Columns.IndexOf(drFound(3)) + 1) = Val(drFound(0).ToString)
            '                'dRow.Item(dtData.Columns.IndexOf(drFound(3)) + 2) = Val(drFound(1).ToString)
            '            End If
            '        Next
            '    End If
            '    dRow.EndEdit()
            'Next


            'Dim dtRecv As New DataTable
            'dtRecv = GetDataTable("SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) AS ReceivedAmount, CONVERT(DateTime, CONVERT(Varchar,  " _
            '                    & " dbo.tblVoucher.voucher_date, 102), 102) AS ReceivingDate FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id WHERE (Convert(Varchar,Voucher_Date,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND tblVoucherDetail.coa_detail_id=" & Me.cmbCustomer.Value & "", "") & " " _
            '                    & " GROUP BY dbo.tblVoucherDetail.coa_detail_id, CONVERT(DateTime, CONVERT(Varchar, dbo.tblVoucher.voucher_date, 102), 102) ")
            'dtRecv.AcceptChanges()


            'For Each dRow As DataRow In dtData.Rows
            '    dRow.BeginEdit()
            '    Dim dr() As DataRow = dtRecv.Select("coa_detail_id=" & Val(dRow.Item("coa_detail_id").ToString) & "")
            '    If dr IsNot Nothing Then
            '        For Each drFound As DataRow In dr
            '            If drFound IsNot Nothing Then
            '                dRow.Item(dtData.Columns.IndexOf(drFound(2)) + 2) = Val(drFound(1).ToString)
            '            End If
            '        Next
            '    End If
            '    dRow.EndEdit()
            'Next

            'For Each drow As DataRow In dtData.Rows
            '    drow.BeginEdit()
            '    Dim dr() As DataRow = dtRecv.Select("coa_detail_id=" & Val(drow.Item("coa_detail_id").ToString) & "")
            '    If dr IsNot Nothing Then
            '        For Each drFound As DataRow In dr
            '            If drFound IsNot Nothing Then
            '                drow.Item(dtData.Columns.IndexOf(drFound(2)) + 2) = Val(drFound(1).ToString)
            '            End If
            '        Next
            '    End If
            '    drow.EndEdit()
            'Next

            'Checking here Weekly or Monthly Report View
            If Me.rbtnWeekly.Checked = True Then

                For Each dr As DataRow In dtData.Rows
                    For c As Integer = enmCustomer.Count To dtData.Columns.Count - 3 Step 3

                        'Dim mydates() As Object = dtData.Columns(c).ColumnName.ToString.Split("|")
                        'Me.dtpFromDate.Value = mydates(0)
                        'Me.dtpToDate.Value = mydates(1)

                        'Task#10092015 split Dates from dtData data table by ahmad sharif

                        Dim temp() As Object = dtData.Columns(c).ColumnName.ToString.Split("|")
                        'temp(0)  =   Week-1 ( 1/10/2015 
                        'temp(1)  =   9/10/2015 )
                        Dim tempStr1 As String = String.Empty
                        Dim tempStr2 As String = String.Empty
                        tempStr1 = temp(0)      'Week-1 ( 1/10/2015 
                        tempStr2 = temp(1)      ' 9/10/2015 )

                        Dim temp2() As Object = tempStr1.Split("(")
                        'temp2(0) = Week-1 
                        'temp2(1) =   1/10/2015 
                        Dim tempStr3 As String = String.Empty
                        Dim tempStr4 As String = String.Empty
                        tempStr3 = temp2(0)     'Week-1 
                        tempStr4 = temp2(1)     ' 1/10/2015 

                        Dim temp3() As Object = tempStr2.Split(")")
                        Dim tempStr5 As String = String.Empty
                        Dim tempStr6 As String = String.Empty
                        tempStr5 = temp3(0)     ' 9/10/2015 
                        tempStr6 = temp3(1)

                        Me.dtpFromDate.Value = CDate(tempStr4)
                        Me.dtpToDate.Value = CDate(tempStr5)
                        'End Task#10092015

                        'Here is Query of Sales 
                        Dim dtWs As DataTable = GetDataTable("SELECT ISNULL(Sale.SalesAmount, 0) + ISNULL(Tax.SalesTax, 0) AS InvoiceAmount, 0 as ReceivedAmount, Sale.CustomerCode " _
                               & " FROM (SELECT SUM(ISNULL(SalesAmount, 0)) AS SalesAmount, CustomerCode " _
                               & " FROM dbo.SalesMasterTable  " _
                               & " WHERE CustomerCode=" & dr.ItemArray(enmCustomer.coa_detail_id) & " and (Convert(Varchar,SalesMasterTable.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
                               & " GROUP BY  CustomerCode) AS Sale LEFT OUTER JOIN  " _
                               & " (SELECT  SalesMasterTable_1.CustomerCode,  " _
                               & " SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)))  " _
                               & " AS SalesTax FROM dbo.SalesDetailTable INNER JOIN dbo.SalesMasterTable AS SalesMasterTable_1 ON dbo.SalesDetailTable.SalesId = SalesMasterTable_1.SalesId   WHERE CustomerCode=" & dr.ItemArray(enmCustomer.coa_detail_id) & " and (Convert(Varchar,SalesMasterTable_1.SalesDate,102) BETWEEN Convert(DateTime,'" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
                               & " GROUP BY SalesMasterTable_1.CustomerCode  " _
                               & " HAVING (SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0))) <> 0))  " _
                               & " AS Tax ON Sale.CustomerCode = Tax.CustomerCode ")
                        dtWs.AcceptChanges()


                        'Here is Query Of Receiving/Recovery 
                        Dim dtweeklyRecv As DataTable = GetDataTable("SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) AS ReceivedAmount  " _
                                  & "  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id WHERE tblVoucherdetail.coa_detail_id=" & dr.ItemArray(enmCustomer.coa_detail_id) & " and (Convert(Varchar,Voucher_Date,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND tblVoucherDetail.coa_detail_id=" & Me.cmbCustomer.Value & "", "") & " AND tblVoucher.voucher_type_id in(3,5) " _
                                  & " GROUP BY dbo.tblVoucherDetail.coa_detail_id ")
                        dtweeklyRecv.AcceptChanges()

                        If dtWs.Rows.Count > 0 Then
                            dr.Item(c + 1) = Val(dtWs.Rows(0).Item("InvoiceAmount").ToString)
                        End If

                        If dtweeklyRecv.Rows.Count > 0 Then
                            dr.Item(c + 2) = Val(dtweeklyRecv.Rows(0).Item("ReceivedAmount").ToString)
                        End If
                    Next
                Next
            End If
            dtData.AcceptChanges()
            dtData.Columns.Add("Net Sales", GetType(System.Double))
            dtData.Columns.Add("Total Receiving", GetType(System.Double))
            dtData.Columns.Add("Balance", GetType(System.Double))
            dtData.Columns("Net Sales").Expression = strTotalSales.ToString
            dtData.Columns("Total Receiving").Expression = strTotalReceiving.ToString
            dtData.Columns("Balance").Expression = "(([Net Sales]+[OpeningBalance])-[Total Receiving])"
            dtData.AcceptChanges()
            Me.grdReport.DataSource = dtData
            Me.grdReport.RetrieveStructure()
            grdReport.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grdReport.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdReport.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdReport.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdReport.RootTable.ColumnSetRowCount = 1
            ColumnSet = Me.grdReport.RootTable.ColumnSets.Add
            ColumnSet.ColumnCount = 6
            ColumnSet.Caption = "Customer Detail"
            ColumnSet.Add(Me.grdReport.RootTable.Columns("Customer"), 0, 0)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("Code"), 0, 1)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("CustomerType"), 0, 2)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("CustomerCode"), 0, 3)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("City"), 0, 4)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("OpeningBalance"), 0, 5)
            Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
            For c As Integer = enmCustomer.Count To Me.grdReport.RootTable.Columns.Count - 3 Step 3
                If Me.grdReport.RootTable.Columns(c).DataMember <> "Net Sales" AndAlso Me.grdReport.RootTable.Columns(c).DataMember <> "Total Receiving" AndAlso Me.grdReport.RootTable.Columns(c).DataMember <> "Balance" Then
                    Me.grdReport.RootTable.Columns(c + 1).Caption = "Sales"
                    Me.grdReport.RootTable.Columns(c + 2).Caption = "Recovery"
                    ColumnSet1 = Me.grdReport.RootTable.ColumnSets.Add
                    ColumnSet1.ColumnCount = 2
                    ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    ColumnSet1.Caption = Me.grdReport.RootTable.Columns(c).Caption
                    ColumnSet1.Add(Me.grdReport.RootTable.Columns(c + 1), 0, 0)
                    ColumnSet1.Add(Me.grdReport.RootTable.Columns(c + 2), 0, 1)
                    grdReport.RootTable.Columns(c).FormatString = "dd/MMM/yyyy"
                    Me.grdReport.RootTable.Columns(c + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grdReport.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grdReport.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdReport.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdReport.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdReport.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdReport.RootTable.Columns(c + 1).FormatString = "N" & DecimalPointInValue
                    Me.grdReport.RootTable.Columns(c + 2).FormatString = "N" & DecimalPointInValue
                    Me.grdReport.RootTable.Columns(c + 1).TotalFormatString = "N" & DecimalPointInValue
                    Me.grdReport.RootTable.Columns(c + 2).TotalFormatString = "N" & DecimalPointInValue
                End If
            Next

            Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdReport.RootTable.ColumnSetRowCount = 1
            ColumnSet2 = Me.grdReport.RootTable.ColumnSets.Add
            ColumnSet2.ColumnCount = 3
            ColumnSet2.Add(Me.grdReport.RootTable.Columns("Net Sales"), 0, 0)
            Me.grdReport.RootTable.Columns("Total Receiving").Caption = "Total Recovery"
            ColumnSet2.Add(Me.grdReport.RootTable.Columns("Total Receiving"), 0, 1)
            ColumnSet2.Add(Me.grdReport.RootTable.Columns("Balance"), 0, 2)
            Me.grdReport.RootTable.Columns("OpeningBalance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Net Sales").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Total Receiving").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("OpeningBalance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Net Sales").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Total Receiving").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("OpeningBalance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Net Sales").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Total Receiving").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("OpeningBalance").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Net Sales").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Total Receiving").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("OpeningBalance").TotalFormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Net Sales").TotalFormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Total Receiving").TotalFormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInValue
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdReport.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillGridMonthly()
        Try
            Dim dtData As New DataTable
            Dim mydt As New DataTable
            dtData = GetDataTable("Select vwCOADetail.coa_detail_id, vwCOADetail.detail_title as [Customer], vwCOADetail.detail_code as Code,CustomerType,CustomerCode, CityName as City, IsNull(OpeningBalance,0) as OpeningBalance From vwCOADetail LEFT OUTER JOIN(Select tblVoucherdetail.coa_detail_id, SUM(debit_amount-credit_amount) as OpeningBalance From tblVoucherDetail INNER JOIN tblVoucher on tblVoucher.voucher_id = tblVoucherdetail.voucher_id where (Convert(varchar,Voucher_Date,102) < Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)) Group by tblvoucherdetail.coa_detail_id) Op on Op.coa_detail_id = vwCOADetail.coa_detail_id WHERE vwcoadetail.Detail_title <> '' " & IIf(Me.cmbCustomer.Value > 0, "  AND vwcoadetail.coa_detail_id=" & Me.cmbCustomer.Value & "", "") & " and account_type='Customer'  Order By vwcoadetail.detail_title ASC")
            dtData.AcceptChanges()
            Dim i As Integer = 0I
            Dim tempValue As Integer = 0I

            Dim weekNo As Integer = 1
            Dim k As Integer = 0I

            While Me.dtpFromDate.Value.AddDays(i).Date < Me.dtpToDate.Value.Date
                Dim myDate As Date = Me.dtpFromDate.Value.AddDays(i).Date
                Dim daysInMonth As Integer = Date.DaysInMonth(myDate.Year, myDate.Month)
                Dim lastDateOfMonth As Date = New Date(myDate.Year, myDate.Month, daysInMonth)
                Dim MName As String = myDate.ToString("MMM")
                Dim Year As Integer = myDate.Year
                Dim month_year As String = String.Empty
                month_year = MName & "-" & Year

                If i = 0 Then
                    If Not dtData.Columns.Contains(Me.dtpFromDate.Value.AddDays(i).Date) Then
                        dtData.Columns.Add(month_year & Environment.NewLine & " (" & Me.dtpFromDate.Value.AddDays(i).Date & " | " & IIf(Me.dtpToDate.Value.Date > lastDateOfMonth, lastDateOfMonth, Me.dtpToDate.Value.Date) & ")", GetType(System.DateTime))
                        dtData.Columns.Add(k & "^" & "Sale", GetType(System.Double))
                        dtData.Columns.Add(k & "^" & "Recovery", GetType(System.Double))
                    End If
                Else
                    i = 0
                    If Not dtData.Columns.Contains(Me.dtpFromDate.Value.AddDays(i).Date) Then
                        dtData.Columns.Add(month_year & Environment.NewLine & " (" & Me.dtpFromDate.Value.AddDays(i + 1).Date & " | " & IIf(Me.dtpToDate.Value.Date > lastDateOfMonth, lastDateOfMonth, Me.dtpToDate.Value.Date) & ")", GetType(System.DateTime))
                        dtData.Columns.Add(k & "^" & "Sale", GetType(System.Double))
                        dtData.Columns.Add(k & "^" & "Recovery", GetType(System.Double))
                    End If
                End If

                i += 1
                k += 1
                Me.dtpFromDate.Value = lastDateOfMonth.Date
            End While


            dtData.AcceptChanges()
            For Each r As DataRow In dtData.Rows
                r.BeginEdit()
                For c As Integer = enmCustomer.Count To dtData.Columns.Count - 3 Step 3
                    r(c + 1) = 0
                    r(c + 2) = 0
                Next
                r.EndEdit()
            Next
            dtData.AcceptChanges()
            Dim strTotalSales As String = String.Empty
            Dim strTotalReceiving As String = String.Empty
            For c As Integer = enmCustomer.Count To dtData.Columns.Count - 3 Step 3
                If strTotalSales.Length > 0 Then
                    strTotalSales += "+" & "[" & dtData.Columns(c + 1).ColumnName.ToString & "]" & ""
                Else
                    strTotalSales = "[" & dtData.Columns(c + 1).ColumnName.ToString & "]" & ""
                End If
                If strTotalReceiving.Length > 0 Then
                    strTotalReceiving += "+" & "[" & dtData.Columns(c + 2).ColumnName.ToString & "]" & ""
                Else
                    strTotalReceiving = "[" & dtData.Columns(c + 2).ColumnName.ToString & "]" & ""
                End If
            Next

            'Dim dt As New DataTable
            ''Here is Sales Query
            'dt = GetDataTable("SELECT ISNULL(Sale.SalesAmount, 0) + ISNULL(Tax.SalesTax, 0) AS InvoiceAmount, 0 as ReceivedAmount, Sale.CustomerCode, Sale.SaleDate " _
            '                & " FROM (SELECT CONVERT(DateTime, CONVERT(varchar, SalesMasterTable.SalesDate, 102), 102) AS SaleDate, SUM(ISNULL(SalesAmount, 0)) AS SalesAmount, CustomerCode " _
            '                & " FROM dbo.SalesMasterTable  " _
            '                & " WHERE (Convert(Varchar,SalesMasterTable.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
            '                & " GROUP BY CONVERT(DateTime, CONVERT(varchar, SalesDate, 102), 102), CustomerCode) AS Sale LEFT OUTER JOIN  " _
            '                & " (SELECT  SalesMasterTable_1.CustomerCode, CONVERT(dateTime, CONVERT(varchar, SalesMasterTable_1.SalesDate, 102), 102) AS SalesDate,  " _
            '                & " SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)))  " _
            '                & " AS SalesTax FROM dbo.SalesDetailTable INNER JOIN dbo.SalesMasterTable AS SalesMasterTable_1 ON dbo.SalesDetailTable.SalesId = SalesMasterTable_1.SalesId   WHERE (Convert(Varchar,SalesMasterTable_1.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
            '                & " GROUP BY SalesMasterTable_1.CustomerCode, CONVERT(dateTime, CONVERT(varchar, SalesMasterTable_1.SalesDate, 102), 102)  " _
            '                & " HAVING (SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0))) <> 0))  " _
            '                & " AS Tax ON Sale.CustomerCode = Tax.CustomerCode AND Tax.SalesDate = Sale.SaleDate  ORDER BY Sale.SaleDate ")
            'dt.AcceptChanges()
            'For Each dRow As DataRow In dtData.Rows
            '    dRow.BeginEdit()
            '    Dim dr() As DataRow = dt.Select("CustomerCode=" & Val(dRow.Item("coa_detail_id").ToString) & "")
            '    If dr IsNot Nothing Then
            '        For Each drFound As DataRow In dr
            '            If drFound IsNot Nothing Then
            '                dRow.Item(dtData.Columns.IndexOf(drFound(3)) + 1) = Val(drFound(0).ToString)
            '            End If
            '        Next
            '    End If
            '    dRow.EndEdit()
            'Next

            ''Here is Recovery/Receiving Query
            'Dim dtRecv As New DataTable
            'dtRecv = GetDataTable("SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) AS ReceivedAmount, CONVERT(DateTime, CONVERT(Varchar,  " _
            '                    & " dbo.tblVoucher.voucher_date, 102), 102) AS ReceivingDate FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id WHERE (Convert(Varchar,Voucher_Date,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND tblVoucherDetail.coa_detail_id=" & Me.cmbCustomer.Value & "", "") & " " _
            '                    & " GROUP BY dbo.tblVoucherDetail.coa_detail_id, CONVERT(DateTime, CONVERT(Varchar, dbo.tblVoucher.voucher_date, 102), 102) ")
            'dtRecv.AcceptChanges()


            'For Each dRow As DataRow In dtData.Rows
            '    dRow.BeginEdit()
            '    Dim dr() As DataRow = dtRecv.Select("coa_detail_id=" & Val(dRow.Item("coa_detail_id").ToString) & "")
            '    If dr IsNot Nothing Then
            '        For Each drFound As DataRow In dr
            '            If drFound IsNot Nothing Then
            '                dRow.Item(dtData.Columns.IndexOf(drFound(2)) + 2) = Val(drFound(1).ToString)
            '            End If
            '        Next
            '    End If
            '    dRow.EndEdit()
            'Next

            'Checking here Weekly or Monthly Report View
            If Me.rbtnMonthly.Checked = True Then

                For Each dr As DataRow In dtData.Rows
                    For c As Integer = enmCustomer.Count To dtData.Columns.Count - 3 Step 3

                        'Dim mydates() As Object = dtData.Columns(c).ColumnName.ToString.Split("|")
                        'Me.dtpFromDate.Value = mydates(0)
                        'Me.dtpToDate.Value = mydates(1)

                        'Task#10092015 split Dates from dtData data table by ahmad sharif

                        Dim temp() As Object = dtData.Columns(c).ColumnName.ToString.Split("(")
                        ''temp(0)  =   'Sep-2015
                        ''temp(1)  =   ' 9/10/2015 | 9/30/2015 )
                        Dim tempStr1 As String = String.Empty
                        Dim tempStr2 As String = String.Empty
                        tempStr1 = temp(0)      'Sep-2015
                        tempStr2 = temp(1)      ' 9/10/2015 | 9/30/2015 )

                        Dim temp2() As Object = tempStr2.Split("|")
                        ''temp2(0) = Week-1 
                        ''temp2(1) =   1/10/2015 
                        Dim tempStr3 As String = String.Empty
                        Dim tempStr4 As String = String.Empty
                        tempStr3 = temp2(0)     '9/10/2015
                        tempStr4 = temp2(1)     ' 9/30/2015 ) 

                        Dim temp3() As Object = tempStr4.Split(")")
                        Dim tempStr5 As String = String.Empty
                        Dim tempStr6 As String = String.Empty
                        tempStr5 = temp3(0)     ' 9/30/2015
                        tempStr6 = temp3(1)     ' ""

                        Me.dtpFromDate.Value = CDate(tempStr3)
                        Me.dtpToDate.Value = CDate(tempStr5)
                        'End Task#10092015

                        'Here is Query of Sales 

                        Dim dtWs As DataTable = GetDataTable("SELECT ISNULL(Sale.SalesAmount, 0) + ISNULL(Tax.SalesTax, 0) AS InvoiceAmount, 0 as ReceivedAmount, Sale.CustomerCode " _
                               & " FROM (SELECT SUM(ISNULL(SalesAmount, 0)) AS SalesAmount, CustomerCode " _
                               & " FROM dbo.SalesMasterTable  " _
                               & " WHERE CustomerCode=" & dr.Item(enmCustomer.coa_detail_id) & " and (Convert(Varchar,SalesMasterTable.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
                               & " GROUP BY  CustomerCode) AS Sale LEFT OUTER JOIN  " _
                               & " (SELECT  SalesMasterTable_1.CustomerCode,  " _
                               & " SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)))  " _
                               & " AS SalesTax FROM dbo.SalesDetailTable INNER JOIN dbo.SalesMasterTable AS SalesMasterTable_1 ON dbo.SalesDetailTable.SalesId = SalesMasterTable_1.SalesId   WHERE CustomerCode=" & dr.Item(enmCustomer.coa_detail_id) & " and (Convert(Varchar,SalesMasterTable_1.SalesDate,102) BETWEEN Convert(DateTime,'" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
                               & " GROUP BY SalesMasterTable_1.CustomerCode  " _
                               & " HAVING (SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0))) <> 0))  " _
                               & " AS Tax ON Sale.CustomerCode = Tax.CustomerCode ")
                        dtWs.AcceptChanges()


                        'Here is Query Of Receiving/Recovery 
                        Dim dtweeklyRecv As DataTable = GetDataTable("SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) AS ReceivedAmount  " _
                                  & "  FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id WHERE tblVoucherdetail.coa_detail_id=" & dr.Item(enmCustomer.coa_detail_id) & " and (Convert(Varchar,Voucher_Date,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND tblVoucherDetail.coa_detail_id=" & Me.cmbCustomer.Value & "", "") & "  AND tblVoucher.voucher_type_id in(3,5) " _
                                  & " GROUP BY dbo.tblVoucherDetail.coa_detail_id ")
                        dtweeklyRecv.AcceptChanges()

                        If dtWs.Rows.Count > 0 Then
                            dr.Item(c + 1) = Val(dtWs.Rows(0).Item("InvoiceAmount").ToString)
                        End If

                        If dtweeklyRecv.Rows.Count > 0 Then
                            dr.Item(c + 2) = Val(dtweeklyRecv.Rows(0).Item("ReceivedAmount").ToString)
                        End If
                    Next
                Next
            End If
            dtData.AcceptChanges()
            dtData.Columns.Add("Net Sales", GetType(System.Double))
            dtData.Columns.Add("Total Receiving", GetType(System.Double))
            dtData.Columns.Add("Balance", GetType(System.Double))
            dtData.Columns("Net Sales").Expression = strTotalSales.ToString
            dtData.Columns("Total Receiving").Expression = strTotalReceiving.ToString
            dtData.Columns("Balance").Expression = "(([Net Sales]+[OpeningBalance])-[Total Receiving])"
            dtData.AcceptChanges()
            Me.grdReport.DataSource = dtData
            Me.grdReport.RetrieveStructure()
            grdReport.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grdReport.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdReport.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdReport.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdReport.RootTable.ColumnSetRowCount = 1
            ColumnSet = Me.grdReport.RootTable.ColumnSets.Add
            ColumnSet.ColumnCount = 6
            ColumnSet.Caption = "Customer Detail"
            ColumnSet.Add(Me.grdReport.RootTable.Columns("Customer"), 0, 0)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("Code"), 0, 1)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("CustomerType"), 0, 2)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("CustomerCode"), 0, 3)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("City"), 0, 4)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("OpeningBalance"), 0, 5)
            Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
            For c As Integer = enmCustomer.Count To Me.grdReport.RootTable.Columns.Count - 3 Step 3
                If Me.grdReport.RootTable.Columns(c).DataMember <> "Net Sales" AndAlso Me.grdReport.RootTable.Columns(c).DataMember <> "Total Receiving" AndAlso Me.grdReport.RootTable.Columns(c).DataMember <> "Balance" Then
                    Me.grdReport.RootTable.Columns(c + 1).Caption = "Sales"
                    Me.grdReport.RootTable.Columns(c + 2).Caption = "Recovery"
                    ColumnSet1 = Me.grdReport.RootTable.ColumnSets.Add
                    ColumnSet1.ColumnCount = 2
                    ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    ColumnSet1.Caption = Me.grdReport.RootTable.Columns(c).Caption
                    ColumnSet1.Add(Me.grdReport.RootTable.Columns(c + 1), 0, 0)
                    ColumnSet1.Add(Me.grdReport.RootTable.Columns(c + 2), 0, 1)
                    grdReport.RootTable.Columns(c).FormatString = "dd/MMM/yyyy"
                    Me.grdReport.RootTable.Columns(c + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grdReport.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grdReport.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdReport.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdReport.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdReport.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdReport.RootTable.Columns(c + 1).FormatString = "N" & DecimalPointInValue
                    Me.grdReport.RootTable.Columns(c + 2).FormatString = "N" & DecimalPointInValue
                    Me.grdReport.RootTable.Columns(c + 1).TotalFormatString = "N" & DecimalPointInValue
                    Me.grdReport.RootTable.Columns(c + 2).TotalFormatString = "N" & DecimalPointInValue
                End If
            Next

            Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdReport.RootTable.ColumnSetRowCount = 1
            ColumnSet2 = Me.grdReport.RootTable.ColumnSets.Add
            ColumnSet2.ColumnCount = 3
            ColumnSet2.Add(Me.grdReport.RootTable.Columns("Net Sales"), 0, 0)
            Me.grdReport.RootTable.Columns("Total Receiving").Caption = "Total Recovery"
            ColumnSet2.Add(Me.grdReport.RootTable.Columns("Total Receiving"), 0, 1)
            ColumnSet2.Add(Me.grdReport.RootTable.Columns("Balance"), 0, 2)
            Me.grdReport.RootTable.Columns("OpeningBalance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Net Sales").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Total Receiving").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("OpeningBalance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Net Sales").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Total Receiving").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("OpeningBalance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Net Sales").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Total Receiving").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("OpeningBalance").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Net Sales").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Total Receiving").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("OpeningBalance").TotalFormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Net Sales").TotalFormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Total Receiving").TotalFormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInValue
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdReport.AutoSizeColumns()
            'Me.grdReport.Width = 100
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillGrid()
        Try
            Dim dtData As New DataTable
            dtData = GetDataTable("Select vwCOADetail.coa_detail_id, vwCOADetail.detail_title as [Customer], vwCOADetail.detail_code as Code,CustomerType,CustomerCode, CityName as City, IsNull(OpeningBalance,0) as OpeningBalance From vwCOADetail LEFT OUTER JOIN(Select tblVoucherdetail.coa_detail_id, SUM(debit_amount-credit_amount) as OpeningBalance From tblVoucherDetail INNER JOIN tblVoucher on tblVoucher.voucher_id = tblVoucherdetail.voucher_id where (Convert(varchar,Voucher_Date,102) < Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)) Group by tblvoucherdetail.coa_detail_id) Op on Op.coa_detail_id = vwCOADetail.coa_detail_id WHERE vwcoadetail.Detail_title <> '' " & IIf(Me.cmbCustomer.Value > 0, "  AND vwcoadetail.coa_detail_id=" & Me.cmbCustomer.Value & "", "") & " and account_type='Customer'  Order By vwcoadetail.detail_title ASC")
            dtData.AcceptChanges()
            Dim i As Integer = 0I



            'While Me.dtpFromDate.Value.AddDays(i).Date <= Me.dtpToDate.Value.Date
            '    If Not dtData.Columns.Contains(Me.dtpFromDate.Value.AddDays(i).Date) Then
            '        dtData.Columns.Add(Me.dtpFromDate.Value.AddDays(i).Date, GetType(System.DateTime))
            '        dtData.Columns.Add(i & "^" & "Sale", GetType(System.Double))
            '        dtData.Columns.Add(i & "^" & "Recovery", GetType(System.Double))
            '    End If
            '    i += 1
            'End While

            'Task#09092015 Weekly, Monthly show by ahmad sharif

            If Me.rbtnDaily.Checked = True Then
                While Me.dtpFromDate.Value.AddDays(i).Date <= Me.dtpToDate.Value.Date
                    If Not dtData.Columns.Contains(Me.dtpFromDate.Value.AddDays(i).Date) Then
                        dtData.Columns.Add(Me.dtpFromDate.Value.AddDays(i).Date, GetType(System.DateTime))
                        dtData.Columns.Add(i & "^" & "Sale", GetType(System.Double))
                        dtData.Columns.Add(i & "^" & "Recovery", GetType(System.Double))
                    End If
                    i += 1
                End While
            ElseIf Me.rbtnWeekly.Checked = True Then
                FillGridWeekly()
                Exit Sub
                '    Dim days As Integer = 0I
                '    Dim totalWeek As Double = 0I
                '    Dim ts As TimeSpan = Me.dtpToDate.Value.Subtract(Me.dtpFromDate.Value)
                '    days = Convert.ToInt32(ts.Days) + 2 'alwasy add 2 in days,because start and end date missing
                '    'totalWeek = days / 7
                '    totalWeek = DateDiff("w", Me.dtpFromDate.Value, Me.dtpToDate.Value)
                '    Dim remDays As Integer = (days Mod 7)
                '    Dim weekNo As Integer = 1



                '    If days > 0 Then
                '        If totalWeek > 0 Then
                '            For j As Integer = 1 To Convert.ToInt32(totalWeek)
                '                If Not dtData.Columns.Contains("Week-" & weekNo) Then
                '                    dtData.Columns.Add("Week-" & weekNo, GetType(System.String))
                '                    dtData.Columns.Add(j & "^" & "Sale", GetType(System.Double))
                '                    dtData.Columns.Add(j & "^" & "Recovery", GetType(System.Double))
                '                End If
                '                'j += 1
                '                weekNo += 1
                '            Next

                '            If remDays > 0 Then
                '                Dim d As Integer = (Convert.ToInt32(totalWeek) * 7)

                '                While Me.dtpFromDate.Value.AddDays(d).Date <= Me.dtpToDate.Value.Date
                '                    If Not dtData.Columns.Contains(Me.dtpFromDate.Value.AddDays(d).Date) Then
                '                        dtData.Columns.Add(Me.dtpFromDate.Value.AddDays(d).Date, GetType(System.DateTime))
                '                        dtData.Columns.Add(d & "^" & "Sale", GetType(System.Double))
                '                        dtData.Columns.Add(d & "^" & "Recovery", GetType(System.Double))
                '                    End If
                '                    d += 1
                '                End While
                '            End If

                '        Else

                '        End If
                '    Else

                '    End If
            Else
                FillGridMonthly()
                Exit Sub
            End If

            ''End Task#09092015



            dtData.AcceptChanges()
            For Each r As DataRow In dtData.Rows
                r.BeginEdit()
                For c As Integer = enmCustomer.Count To dtData.Columns.Count - 3 Step 3
                    r(c + 1) = 0
                    r(c + 2) = 0
                Next
                r.EndEdit()
            Next
            dtData.AcceptChanges()
            Dim strTotalSales As String = String.Empty
            Dim strTotalReceiving As String = String.Empty
            For c As Integer = enmCustomer.Count To dtData.Columns.Count - 3 Step 3
                If strTotalSales.Length > 0 Then
                    strTotalSales += "+" & "[" & dtData.Columns(c + 1).ColumnName.ToString & "]" & ""
                Else
                    strTotalSales = "[" & dtData.Columns(c + 1).ColumnName.ToString & "]" & ""
                End If
                If strTotalReceiving.Length > 0 Then
                    strTotalReceiving += "+" & "[" & dtData.Columns(c + 2).ColumnName.ToString & "]" & ""
                Else
                    strTotalReceiving = "[" & dtData.Columns(c + 2).ColumnName.ToString & "]" & ""
                End If
            Next
            Dim dt As New DataTable
            'dt = GetDataTable("SELECT ISNULL(Sale.SalesAmount, 0) + ISNULL(Tax.SalesTax, 0) AS InvoiceAmount, Recv.ReceivedAmount, Sale.CustomerCode, Sale.SaleDate " _
            '  & " FROM (SELECT CONVERT(DateTime, CONVERT(varchar, SalesMasterTable.SalesDate, 102), 102) AS SaleDate, SUM(ISNULL(SalesAmount, 0)) AS SalesAmount, CustomerCode " _
            '  & " FROM dbo.SalesMasterTable  " _
            '  & " WHERE (Convert(Varchar,SalesMasterTable.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
            '  & " GROUP BY CONVERT(DateTime, CONVERT(varchar, SalesDate, 102), 102), CustomerCode) AS Sale LEFT OUTER JOIN  " _
            '  & " (SELECT  SalesMasterTable_1.CustomerCode, CONVERT(dateTime, CONVERT(varchar, SalesMasterTable_1.SalesDate, 102), 102) AS SalesDate,  " _
            '  & " SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)))  " _
            '  & " AS SalesTax FROM dbo.SalesDetailTable INNER JOIN dbo.SalesMasterTable AS SalesMasterTable_1 ON dbo.SalesDetailTable.SalesId = SalesMasterTable_1.SalesId   WHERE (Convert(Varchar,SalesMasterTable_1.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
            '  & " GROUP BY SalesMasterTable_1.CustomerCode, CONVERT(dateTime, CONVERT(varchar, SalesMasterTable_1.SalesDate, 102), 102)  " _
            '  & " HAVING (SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0))) <> 0))  " _
            '  & " AS Tax ON Sale.CustomerCode = Tax.CustomerCode AND Tax.SalesDate = Sale.SaleDate LEFT OUTER JOIN " _
            '  & " (SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) AS ReceivedAmount, CONVERT(DateTime, CONVERT(Varchar,  " _
            '  & " dbo.tblVoucher.voucher_date, 102), 102) AS ReceivingDate FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id WHERE (Convert(Varchar,Voucher_Date,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND tblVoucherDetail.coa_detail_id=" & Me.cmbCustomer.Value & "", "") & " " _
            '  & " GROUP BY dbo.tblVoucherDetail.coa_detail_id, CONVERT(DateTime, CONVERT(Varchar, dbo.tblVoucher.voucher_date, 102), 102)) AS Recv ON  " _
            '  & " Recv.coa_detail_id = Sale.CustomerCode And Recv.ReceivingDate = Sale.SaleDate ORDER BY Sale.SaleDate ")



            dt = GetDataTable("SELECT ISNULL(Sale.SalesAmount, 0) + ISNULL(Tax.SalesTax, 0) AS InvoiceAmount, 0 as ReceivedAmount, Sale.CustomerCode, Sale.SaleDate " _
                            & " FROM (SELECT CONVERT(DateTime, CONVERT(varchar, SalesMasterTable.SalesDate, 102), 102) AS SaleDate, SUM(ISNULL(SalesAmount, 0)) AS SalesAmount, CustomerCode " _
                            & " FROM dbo.SalesMasterTable  " _
                            & " WHERE (Convert(Varchar,SalesMasterTable.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
                            & " GROUP BY CONVERT(DateTime, CONVERT(varchar, SalesDate, 102), 102), CustomerCode) AS Sale LEFT OUTER JOIN  " _
                            & " (SELECT  SalesMasterTable_1.CustomerCode, CONVERT(dateTime, CONVERT(varchar, SalesMasterTable_1.SalesDate, 102), 102) AS SalesDate,  " _
                            & " SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)))  " _
                            & " AS SalesTax FROM dbo.SalesDetailTable INNER JOIN dbo.SalesMasterTable AS SalesMasterTable_1 ON dbo.SalesDetailTable.SalesId = SalesMasterTable_1.SalesId   WHERE (Convert(Varchar,SalesMasterTable_1.SalesDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND CustomerCode=" & Me.cmbCustomer.Value & "", "") & " " _
                            & " GROUP BY SalesMasterTable_1.CustomerCode, CONVERT(dateTime, CONVERT(varchar, SalesMasterTable_1.SalesDate, 102), 102)  " _
                            & " HAVING (SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0))) <> 0))  " _
                            & " AS Tax ON Sale.CustomerCode = Tax.CustomerCode AND Tax.SalesDate = Sale.SaleDate  ORDER BY Sale.SaleDate ")
            dt.AcceptChanges()
            For Each dRow As DataRow In dtData.Rows
                dRow.BeginEdit()
                Dim dr() As DataRow = dt.Select("CustomerCode=" & Val(dRow.Item("coa_detail_id").ToString) & "")
                If dr IsNot Nothing Then
                    For Each drFound As DataRow In dr
                        If drFound IsNot Nothing Then
                            dRow.Item(dtData.Columns.IndexOf(drFound(3)) + 1) = Val(drFound(0).ToString)
                            'dRow.Item(dtData.Columns.IndexOf(drFound(3)) + 2) = Val(drFound(1).ToString)
                        End If
                    Next
                End If
                dRow.EndEdit()
            Next


            Dim dtRecv As New DataTable
            dtRecv = GetDataTable("SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(ISNULL(dbo.tblVoucherDetail.credit_amount, 0)) AS ReceivedAmount, CONVERT(DateTime, CONVERT(Varchar,  " _
                                & " dbo.tblVoucher.voucher_date, 102), 102) AS ReceivingDate FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id WHERE (Convert(Varchar,Voucher_Date,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(Me.cmbCustomer.Value > 0, "  AND tblVoucherDetail.coa_detail_id=" & Me.cmbCustomer.Value & "", "") & "  AND tblVoucher.voucher_type_id in(3,5) " _
                                & " GROUP BY dbo.tblVoucherDetail.coa_detail_id, CONVERT(DateTime, CONVERT(Varchar, dbo.tblVoucher.voucher_date, 102), 102) ")
            dtRecv.AcceptChanges()


            For Each dRow As DataRow In dtData.Rows
                dRow.BeginEdit()
                Dim dr() As DataRow = dtRecv.Select("coa_detail_id=" & Val(dRow.Item("coa_detail_id").ToString) & "")
                If dr IsNot Nothing Then
                    For Each drFound As DataRow In dr
                        If drFound IsNot Nothing Then
                            dRow.Item(dtData.Columns.IndexOf(drFound(2)) + 2) = Val(drFound(1).ToString)
                        End If
                    Next
                End If
                dRow.EndEdit()
            Next



            dtData.AcceptChanges()
            dtData.Columns.Add("Net Sales", GetType(System.Double))
            dtData.Columns.Add("Total Receiving", GetType(System.Double))
            dtData.Columns.Add("Balance", GetType(System.Double))
            dtData.Columns("Net Sales").Expression = strTotalSales.ToString
            dtData.Columns("Total Receiving").Expression = strTotalReceiving.ToString
            dtData.Columns("Balance").Expression = "(([Net Sales]+[OpeningBalance])-[Total Receiving])"
            dtData.AcceptChanges()
            Me.grdReport.DataSource = dtData
            Me.grdReport.RetrieveStructure()
            grdReport.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grdReport.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdReport.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdReport.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdReport.RootTable.ColumnSetRowCount = 1
            ColumnSet = Me.grdReport.RootTable.ColumnSets.Add
            ColumnSet.ColumnCount = 6
            ColumnSet.Caption = "Customer Detail"
            ColumnSet.Add(Me.grdReport.RootTable.Columns("Customer"), 0, 0)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("Code"), 0, 1)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("CustomerType"), 0, 2)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("CustomerCode"), 0, 3)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("City"), 0, 4)
            ColumnSet.Add(Me.grdReport.RootTable.Columns("OpeningBalance"), 0, 5)
            Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
            For c As Integer = enmCustomer.Count To Me.grdReport.RootTable.Columns.Count - 3 Step 3
                If Me.grdReport.RootTable.Columns(c).DataMember <> "Net Sales" AndAlso Me.grdReport.RootTable.Columns(c).DataMember <> "Total Receiving" AndAlso Me.grdReport.RootTable.Columns(c).DataMember <> "Balance" Then
                    Me.grdReport.RootTable.Columns(c + 1).Caption = "Sales"
                    Me.grdReport.RootTable.Columns(c + 2).Caption = "Recovery"
                    ColumnSet1 = Me.grdReport.RootTable.ColumnSets.Add
                    ColumnSet1.ColumnCount = 2
                    ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    ColumnSet1.Caption = Me.grdReport.RootTable.Columns(c).Caption
                    ColumnSet1.Add(Me.grdReport.RootTable.Columns(c + 1), 0, 0)
                    ColumnSet1.Add(Me.grdReport.RootTable.Columns(c + 2), 0, 1)
                    grdReport.RootTable.Columns(c).FormatString = "dd/MMM/yyyy"
                    Me.grdReport.RootTable.Columns(c + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grdReport.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grdReport.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdReport.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdReport.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdReport.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdReport.RootTable.Columns(c + 1).FormatString = "N" & DecimalPointInValue
                    Me.grdReport.RootTable.Columns(c + 2).FormatString = "N" & DecimalPointInValue
                    Me.grdReport.RootTable.Columns(c + 1).TotalFormatString = "N" & DecimalPointInValue
                    Me.grdReport.RootTable.Columns(c + 2).TotalFormatString = "N" & DecimalPointInValue
                End If
            Next

            Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdReport.RootTable.ColumnSetRowCount = 1
            ColumnSet2 = Me.grdReport.RootTable.ColumnSets.Add
            ColumnSet2.ColumnCount = 3
            ColumnSet2.Add(Me.grdReport.RootTable.Columns("Net Sales"), 0, 0)
            Me.grdReport.RootTable.Columns("Total Receiving").Caption = "Total Recovery"
            ColumnSet2.Add(Me.grdReport.RootTable.Columns("Total Receiving"), 0, 1)
            ColumnSet2.Add(Me.grdReport.RootTable.Columns("Balance"), 0, 2)
            Me.grdReport.RootTable.Columns("OpeningBalance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Net Sales").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Total Receiving").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("OpeningBalance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Net Sales").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Total Receiving").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("OpeningBalance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Net Sales").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Total Receiving").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("OpeningBalance").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Net Sales").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Total Receiving").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("OpeningBalance").TotalFormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Net Sales").TotalFormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Total Receiving").TotalFormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInValue
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdReport.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmGrdRptCustomerWiseCashRecovery_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.dtpFromDate.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpToDate.Value = Date.Now
            FillCombo()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.cmbCustomer.IsItemInList = False Then Exit Sub
            If Me.cmbCustomer.ActiveRow Is Nothing Then Exit Sub
            _DateFrom = Me.dtpFromDate.Value
            _DateTo = Me.dtpToDate.Value
            FillGrid()
            Me.dtpFromDate.Value = _DateFrom
            Me.dtpToDate.Value = _DateTo
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            If Me.cmbCustomer.IsItemInList = False Or Me.cmbCustomer.ActiveRow Is Nothing Then
                Me.cmbCustomer.Rows(0).Activate()

            End If
            id = Me.cmbCustomer.ActiveRow.Cells(0).Value
            FillCombo()
            Me.cmbCustomer.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdReport.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdReport.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdReport.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Customer Cash Recovery Report" & Chr(10) & "Date From: " & Me.dtpFromDate.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpToDate.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class