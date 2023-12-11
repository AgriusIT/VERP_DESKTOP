Public Class rptGrdEachDaysWorking
    Enum enmDateWiseWorkingRpt
        SummaryDate
        Dispatch
        Ret
        Ret_Per_age
        Unsold
        PreviouseBal
        OffTake
        Payables
        Special_Ret
        MC
        Pertorl
        ToolTax
        Adjustment
        CashPaid
        ShortCash
        OtherAdjustment
        Balance
        Sampling
        CustomerCode
        ReturnMC
        SalesMC
    End Enum
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
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
    End Sub
    Private Sub rptGrdEachDaysWorking_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.cmbPeriod.Text = "Current Month"
        FillUltraDropDown(Me.cmbCustomer, "Select coa_detail_id, detail_title as [Customer], detail_code as [Account Code], TerritoryName as [Area],CustomerType as [Type],Contact_Phone as Phone, Contact_Mobile as Mobile From vwCOADetail WHERE detail_title Is Not Null AND Account_Type='Customer'")
        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
        Me.cmbCustomer.Rows(0).Activate()
        SetDtOnGrid()
    End Sub
    Public Sub FillGrid()
        Try
            Dim Opening As Double = 0D
            Dim strOpn As String = String.Empty
            Dim dtOpening As DataTable
            If Me.grd.RowCount > 0 Then Me.grd.DataSource = Nothing
            If grd.DataSource Is Nothing Then
                SetDtOnGrid()
            End If
            Dim dt As DataTable = CType(grd.DataSource, DataTable)

            strOpn = "Select ISNULL(SUM(debit_amount-credit_amount),0) as Opening From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id WHERE (Convert(varchar, Voucher_Date, 102) < Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND tblVoucherDetail.coa_detail_id=" & Me.cmbCustomer.Value
            dtOpening = GetDataTable(strOpn)
            If dtOpening IsNot Nothing Then
                If dtOpening.Rows.Count > 0 Then
                    Opening = dtOpening.Rows(0).Item(0)
                Else
                    Opening = 0
                End If
            Else
                Opening = 0
            End If



            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item(enmDateWiseWorkingRpt.SummaryDate) = Me.dtpFromDate.Value
            dr.Item(enmDateWiseWorkingRpt.Dispatch) = 0
            dr.Item(enmDateWiseWorkingRpt.Ret) = 0
            'dr.Item(enmDateWiseWorkingRpt.Ret_Per_age) = 0
            dr.Item(enmDateWiseWorkingRpt.Unsold) = 0
            dr.Item(enmDateWiseWorkingRpt.PreviouseBal) = Opening
            ' dr.Item(enmDateWiseWorkingRpt.OffTake) = 0
            'dr.Item(enmDateWiseWorkingRpt.Payables) = 0
            dr.Item(enmDateWiseWorkingRpt.Special_Ret) = 0
            'dr.Item(enmDateWiseWorkingRpt.MC) = 0
            dr.Item(enmDateWiseWorkingRpt.Pertorl) = 0
            dr.Item(enmDateWiseWorkingRpt.ToolTax) = 0
            dr.Item(enmDateWiseWorkingRpt.Adjustment) = 0
            dr.Item(enmDateWiseWorkingRpt.CashPaid) = 0
            'dr.Item(enmDateWiseWorkingRpt.ShortCash) = 0
            'dr.Item(enmDateWiseWorkingRpt.Balance) = 0
            dr.Item(enmDateWiseWorkingRpt.OtherAdjustment) = 0
            dr.Item(enmDateWiseWorkingRpt.Sampling) = 0
            dr.Item(enmDateWiseWorkingRpt.CustomerCode) = Me.cmbCustomer.Value
            dr.Item(enmDateWiseWorkingRpt.ReturnMC) = 0
            dr.Item(enmDateWiseWorkingRpt.SalesMC) = 0
            dt.Rows.InsertAt(dr, 0)

            Dim intDays As Integer = Me.dtpToDate.Value.Subtract(Me.dtpFromDate.Value).Days
            For i As Integer = 0 To intDays - 1
                dr = dt.NewRow
                dr.Item(enmDateWiseWorkingRpt.SummaryDate) = Me.dtpFromDate.Value.AddDays(Me.grd.RowCount)
                dr.Item(enmDateWiseWorkingRpt.Dispatch) = 0
                dr.Item(enmDateWiseWorkingRpt.Ret) = 0
                'dr.Item(enmDateWiseWorkingRpt.Ret_Per_age) = 0
                dr.Item(enmDateWiseWorkingRpt.Unsold) = 0
                dr.Item(enmDateWiseWorkingRpt.PreviouseBal) = 0
                'dr.Item(enmDateWiseWorkingRpt.OffTake) = 0
                'dr.Item(enmDateWiseWorkingRpt.Payables) = 0
                dr.Item(enmDateWiseWorkingRpt.Special_Ret) = 0
                'dr.Item(enmDateWiseWorkingRpt.MC) = 0
                dr.Item(enmDateWiseWorkingRpt.Pertorl) = 0
                dr.Item(enmDateWiseWorkingRpt.ToolTax) = 0
                dr.Item(enmDateWiseWorkingRpt.Adjustment) = 0
                dr.Item(enmDateWiseWorkingRpt.CashPaid) = 0
                dr.Item(enmDateWiseWorkingRpt.OtherAdjustment) = 0
                'dr.Item(enmDateWiseWorkingRpt.ShortCash) = 0
                'dr.Item(enmDateWiseWorkingRpt.Balance) = 0
                dr.Item(enmDateWiseWorkingRpt.Sampling) = 0
                dr.Item(enmDateWiseWorkingRpt.CustomerCode) = Me.cmbCustomer.Value
                dr.Item(enmDateWiseWorkingRpt.ReturnMC) = 0
                dr.Item(enmDateWiseWorkingRpt.SalesMC) = 0
                dt.Rows.Add(dr)
            Next

            dt.AcceptChanges()

      

            'For Each row As DataRow In dt.Rows
            '    'Dim drOpn() As DataRow
            '    strOpn = String.Empty
            '    strOpn = "Select ISNULL(SUM(IsNull(debit_amount,0)-IsNull(credit_amount,0)),0) as Opening From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id WHERE (Convert(varchar, Voucher_Date, 102) < Convert(Datetime, '" & CDate(row.Item(0)).ToString("yyyy-M-d 00:00:00") & "', 102)) AND tblVoucherDetail.coa_detail_id=" & Me.cmbCustomer.Value
            '    Dim dtOpn As DataTable = GetDataTable(strOpn)
            '    dtOpn.AcceptChanges()
            '    If dtOpn IsNot Nothing Then
            '        'Dim searchDate As DateTime = row.Item(0)
            '        'drOpn = dtOpn.Select("SalesDate='" & searchDate.ToString("MM/dd/yyyy") & "'")
            '        'If drOpn.Length > 0 Then
            '        'For Each r As DataRow In drOpn
            '        row.BeginEdit()
            '        row.Item(enmDateWiseWorkingRpt.PreviouseBal) = Val(dtOpn.Rows(0).Item(0).ToString)
            '        row.EndEdit()
            '        'Next
            '        'End If
            '    End If
            'Next
            'dt.AcceptChanges()

            'Dispatch 
            Dim drDisp() As DataRow
            Dim strDisp As String = "Select Convert(varchar, a.SalesDate, 101) as SalesDate, ISNULL(SUM((ISNULL(b.Qty,0)*ISNULL(b.CurrentPrice,0))+((IsNull(TaxPercent,0)/100)*(ISNULL(b.Qty,0)*ISNULL(b.CurrentPrice,0)))+((IsNull(SEDPercent,0)/100)*(ISNULL(b.Qty,0)*ISNULL(b.CurrentPrice,0)))),0) as Dispatch From SalesMasterTable a INNER JOIN SalesDetailTable b On a.SalesId = b.SalesId WHERE (Convert(varchar, a.SalesDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00 ") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59 ") & "', 102)) AND CustomerCode=" & Me.cmbCustomer.Value & " GROUP BY Convert(Varchar, a.SalesDate,101) "
            Dim dtDisp As DataTable = GetDataTable(strDisp)
            dtDisp.AcceptChanges()
            If Not dtDisp Is Nothing Then
                For Each row As DataRow In dt.Rows
                    Dim searchDate As DateTime = row.Item(0)
                    drDisp = dtDisp.Select("SalesDate='" & searchDate.ToString("MM/dd/yyyy") & "'")
                    If drDisp.Length > 0 Then
                        For Each r As DataRow In drDisp
                            row.BeginEdit()
                            row.Item(enmDateWiseWorkingRpt.Dispatch) = r(1)
                            row.EndEdit()
                        Next
                    End If
                Next
            End If

            dt.AcceptChanges()
            'Return 
            Dim drRet() As DataRow
            Dim strRet As String = "Select Convert(varchar, a.SalesReturnDate, 101) as SalesReturnDate, ISNULL(SUM((ISNULL(b.Qty,0)*ISNULL(b.CurrentPrice,0)) + ((IsNull(Tax_Percent,0)/100) *(ISNULL(b.Qty,0)*ISNULL(b.CurrentPrice,0)))),0) as Returns From SalesReturnMasterTable a INNER JOIN SalesReturnDetailTable b On a.SalesReturnId = b.SalesReturnId LEFT OUTER JOIN tblDefLocation Lc On Lc.Location_Id = b.LocationId WHERE (Convert(varchar, a.SalesReturnDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00 ") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59 ") & "', 102)) AND a.CustomerCode=" & Me.cmbCustomer.Value & " AND lc.Location_Type='Damage' GROUP BY Convert(Varchar, a.SalesReturnDate,101) "
            Dim dtRet As DataTable = GetDataTable(strRet)
            dtRet.AcceptChanges()
            If Not dtRet Is Nothing Then
                For Each row As DataRow In dt.Rows
                    Dim searchDate As DateTime = row.Item(0)
                    drRet = dtRet.Select("SalesReturnDate='" & searchDate.ToString("MM/dd/yyyy") & "'")
                    If drRet.Length > 0 Then
                        For Each r As DataRow In drRet
                            row.BeginEdit()
                            row.Item(enmDateWiseWorkingRpt.Ret) = r(1)
                            row.EndEdit()
                        Next
                    End If
                Next
            End If

            dt.AcceptChanges()
            'Return 
            Dim drRetUnsold() As DataRow
            Dim strRetUnsold As String = "Select Convert(varchar, a.SalesReturnDate, 101) as SalesReturnDate, ISNULL(SUM((ISNULL(b.Qty,0)*ISNULL(b.CurrentPrice,0)) + ((IsNull(Tax_Percent,0)/100) *(ISNULL(b.Qty,0)*ISNULL(b.CurrentPrice,0)))),0) as Returns From SalesReturnMasterTable a INNER JOIN SalesReturnDetailTable b On a.SalesReturnId = b.SalesReturnId LEFT OUTER JOIN tblDefLocation Lc On Lc.Location_Id = b.LocationId WHERE (Convert(varchar, a.SalesReturnDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00 ") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59 ") & "', 102)) AND a.CustomerCode=" & Me.cmbCustomer.Value & " AND lc.Location_Type <> 'Damage' GROUP BY Convert(Varchar, a.SalesReturnDate, 101) "
            Dim dtRetUnsold As DataTable = GetDataTable(strRetUnsold)
            dtRetUnsold.AcceptChanges()
            If Not dtRetUnsold Is Nothing Then
                For Each row As DataRow In dt.Rows
                    Dim searchDate As DateTime = row.Item(0)
                    drRetUnsold = dtRetUnsold.Select("SalesReturnDate='" & searchDate.ToString("MM/dd/yyyy") & "'")
                    If drRetUnsold.Length > 0 Then
                        For Each r As DataRow In drRetUnsold
                            row.BeginEdit()
                            row.Item(enmDateWiseWorkingRpt.Unsold) = r(1)
                            row.EndEdit()
                        Next
                    End If
                Next
            End If
            dt.AcceptChanges()
            'Expenses
            Dim drExp() As DataRow
            Dim strExp As String = "Select Convert(varchar, SalesDate, 101) as SalesDate, SUM(FuelExpense) as Petrol, SUM(Adjustment) as Adjustment, SUM(OtherExpense) as ToolTax, SUM(ISNULL(MarketComm.MC,0)) as MC, SUM(ISNULL(MarketComm.SampleQty,0)) as Sampling From SalesMasterTable LEFT OUTER JOIN(Select a.SalesId, SUM(((ISNULL(b.CurrentPrice,0)-ISNULL(b.Price,0))* b.Qty)) as MC, SUM(ISNULL(b.SampleQty,0)) as SampleQty From SalesMasterTable a INNER JOIN SalesDetailTable b on a.SalesId = b.SalesId WHERE a.CustomerCode=" & Me.cmbCustomer.Value & " Group by a.SalesId)as MarketComm On MarketComm.SalesId = SalesMasterTable.SalesId  WHERE (Convert(varchar, SalesDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND  Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) AND CustomerCode=" & Me.cmbCustomer.Value & " Group By Convert(varchar, SalesDate, 101) "
            Dim dtExp As DataTable = GetDataTable(strExp)
            dtExp.AcceptChanges()
            If Not dtExp Is Nothing Then
                For Each row As DataRow In dt.Rows
                    Dim searchDate As DateTime = row.Item(0)
                    drExp = dtExp.Select("SalesDate='" & searchDate.ToString("MM/dd/yyyy") & "'")
                    If drExp.Length > 0 Then
                        For Each r As DataRow In drExp
                            row.BeginEdit()
                            row.Item(enmDateWiseWorkingRpt.Pertorl) = r(1) 'Petrol Expense
                            row.Item(enmDateWiseWorkingRpt.Adjustment) = r(2) 'Adjustment
                            row.Item(enmDateWiseWorkingRpt.ToolTax) = r(3) ' ToolTax
                            row.Item(enmDateWiseWorkingRpt.SalesMC) = r(4) 'Market Commision 
                            row.Item(enmDateWiseWorkingRpt.Sampling) = r(5) 'Market Commision 
                            row.EndEdit()
                        Next
                    End If
                Next
            End If

            dt.AcceptChanges()
            'Cash Paid 
            Dim drCS() As DataRow
            Dim strCs As String = "Select Convert(varchar, a.Voucher_Date, 101) as Voucher_Date, ISNULL(SUM(ISNULL(b.credit_amount,0)),0) as CashPaid From tblVoucher a INNER JOIN tblVoucherDetail b On a.Voucher_Id = b.Voucher_Id WHERE (Convert(varchar, a.Voucher_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00 ") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59 ") & "', 102)) AND voucher_type_id in(1,3,5,null) AND b.coa_detail_id=" & Me.cmbCustomer.Value & "  AND IsNull(b.Credit_Amount,0) > 0  GROUP BY Convert(Varchar, a.Voucher_Date,101) "
            Dim dtCs As DataTable = GetDataTable(strCs)
            dtCs.AcceptChanges()
            If Not dtCs Is Nothing Then
                For Each row As DataRow In dt.Rows
                    Dim searchDate As DateTime = row.Item(0)
                    drCS = dtCs.Select("Voucher_Date='" & searchDate.ToString("MM/dd/yyyy") & "'")
                    If drCS.Length > 0 Then
                        For Each r As DataRow In drCS
                            row.BeginEdit()
                            row.Item(enmDateWiseWorkingRpt.CashPaid) = r(1)
                            row.EndEdit()
                        Next
                    End If
                Next
            End If
            dt.AcceptChanges()
            Dim drDs() As DataRow
            Dim strDs As String = "Select Convert(varchar, a.Voucher_Date, 101) as Voucher_Date, ISNULL(SUM(ISNULL(b.debit_amount,0)),0) as OtherAdustment From tblVoucher a INNER JOIN tblVoucherDetail b On a.Voucher_Id = b.Voucher_Id WHERE (Convert(varchar, a.Voucher_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00 ") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59 ") & "', 102)) AND b.coa_detail_id=" & Me.cmbCustomer.Value & "  AND IsNull(b.debit_amount,0) > 0 AND voucher_type_id in(1,2,4,null) GROUP BY Convert(Varchar, a.Voucher_Date,101) "
            Dim dtDs As DataTable = GetDataTable(strDs)
            dtDs.AcceptChanges()
            If Not dtDs Is Nothing Then
                For Each row As DataRow In dt.Rows
                    Dim searchDate As DateTime = row.Item(0)
                    drDs = dtDs.Select("Voucher_Date='" & searchDate.ToString("MM/dd/yyyy") & "'")
                    If drDs.Length > 0 Then
                        For Each r As DataRow In drDs
                            row.BeginEdit()
                            row.Item(enmDateWiseWorkingRpt.OtherAdjustment) = r(1)
                            row.EndEdit()
                        Next
                    End If
                Next
            End If
            dt.AcceptChanges()
            'Return MC
            Dim drRetMC() As DataRow
            Dim strRetMc As String = "Select Convert(varchar, a.SalesReturnDate,101) as SalesReturnDate, SUM(((b.CurrentPrice-b.Price)*b.Qty)) as ReturnMC From SalesReturnMasterTable a INNER JOIN SalesReturnDetailTable b On a.SalesReturnId = b.SalesReturnId WHERE (Convert(varchar, a.SalesReturnDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00 ") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59 ") & "', 102)) AND a.CustomerCode=" & Me.cmbCustomer.Value & "  GROUP BY Convert(Varchar, a.SalesReturnDate,101) "
            Dim dtRetnMc As DataTable = GetDataTable(strRetMc)
            dtRetnMc.AcceptChanges()
            If Not dtRetnMc Is Nothing Then
                For Each row As DataRow In dt.Rows
                    Dim searchDate As DateTime = row.Item(0)
                    drRetMC = dtRetnMc.Select("SalesReturnDate='" & searchDate.ToString("MM/dd/yyyy") & "'")
                    If drRetMC.Length > 0 Then
                        For Each r As DataRow In drRetMC
                            row.BeginEdit()
                            row.Item(enmDateWiseWorkingRpt.ReturnMC) = r(1)
                            row.EndEdit()
                        Next
                    End If
                Next
            End If
            dt.AcceptChanges()

            'Finally Calculation below ............................

            Dim openBal As Double = 0
            Dim j As Integer = 0
            For Each row As DataRow In dt.Rows
                row.BeginEdit()
                If j > 0 Then
                    row.Item(enmDateWiseWorkingRpt.PreviouseBal) = openBal
                End If
                row.EndEdit()
                openBal = dt.Rows(j).Item(enmDateWiseWorkingRpt.Balance)
                j = j + 1
            Next

            grd.ColumnAutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.ColumnHeader

            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Me.cmbCustomer.IsItemInList = False Then Exit Sub
            If Me.cmbCustomer.ActiveRow Is Nothing Then
                ShowErrorMessage("Invalid Customer")
                Me.cmbCustomer.Focus()
                Exit Sub
            End If
            If Me.cmbCustomer.Value = 0 Then
                ShowErrorMessage("Please select any customer/dealer account")
                Me.cmbCustomer.Focus()
                Exit Sub
            End If
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub SetDtOnGrid()
        Try
            Dim dt As New DataTable
            dt.Columns.Add("SummaryDate", GetType(Date))
            dt.Columns.Add("Dispatch", GetType(System.Double))
            dt.Columns.Add("Returns", GetType(System.Double))
            dt.Columns.Add("Ret_Per_Age", GetType(System.Double))
            dt.Columns.Add("UnSold", GetType(System.Double))
            dt.Columns.Add("PreviouseBal", GetType(System.Double))
            dt.Columns.Add("OffTake", GetType(System.Double))
            dt.Columns.Add("Payables", GetType(System.Double))
            dt.Columns.Add("Special_Ret", GetType(System.Double))
            dt.Columns.Add("MC", GetType(System.Double))
            dt.Columns.Add("Petrol", GetType(System.Double))
            dt.Columns.Add("ToolTax", GetType(System.Double))
            dt.Columns.Add("Adjustment", GetType(System.Double))
            dt.Columns.Add("CashPaid", GetType(System.Double))
            dt.Columns.Add("ShortCash", GetType(System.Double))
            dt.Columns.Add("OtherAdjustment", GetType(System.Double))
            dt.Columns.Add("Balance", GetType(System.Double))
            dt.Columns.Add("Sampling", GetType(System.Double))
            dt.Columns.Add("CustomerCode", GetType(System.Int32))
            dt.Columns.Add("ReturnMC", GetType(System.Double))
            dt.Columns.Add("SalesMC", GetType(System.Double))
            Me.grd.DataSource = dt
            Me.grd.RootTable.Columns(0).FormatString = "dd/MMM/yyyy"

           
            dt.Columns("MC").Expression = "(SalesMC-ReturnMC)"
            dt.Columns("Payables").Expression = "((PreviouseBal + Dispatch) - (Returns + UnSold))"
            dt.Columns("Ret_Per_Age").Expression = "IIF(Dispatch=0,0, ((Returns * 100)/Dispatch))"
            dt.Columns("OffTake").Expression = "((IsNull(Dispatch,0)-IsNull(Returns,0))-IsNull(UnSold,0))"
            dt.Columns("ShortCash").Expression = "((IsNull(ToolTax,0)+IsNull(Adjustment,0)+IsNull(Petrol,0)+IsNull(Special_ret,0)+IsNull(CashPaid,0)+ISNULL(MC,0)) - IsNull(OffTake,0))"
            dt.Columns("Balance").Expression = "(IsNull(Payables,0)+IsNull(OtherAdjustment,0) -(ISNULL(MC,0)+IsNull(ToolTax,0)+IsNull(Adjustment,0)+IsNull(Petrol,0)+IsNull(Special_ret,0)+IsNull(CashPaid,0))) "

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class