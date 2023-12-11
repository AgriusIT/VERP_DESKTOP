Public Class frmRptInvoiceAging
    Implements IGeneral
    Dim CustomerReceiveables As Double
    Dim Received As Double
    Dim NotDue As Double
    Dim Balance As Double
    Dim CredditBaalance As Double
    Dim dt1 As DataTable
    Dim NetAmount As Double
    Dim CreditLimit As Double
    Dim NotDuedt As DataTable

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns("voucher_no").Caption = "Invoice No"
            Me.grd.RootTable.Columns("voucher_date").Caption = "Invoice Date"
            Me.grd.RootTable.Columns("voucher_date").FormatString = str_DisplayDateFormat
            Me.grd.RootTable.Columns("Amount").Caption = "Invoice Amount"
            Me.grd.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("DueAmount").Caption = "Due Amount"
            Me.grd.RootTable.Columns("DueAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("DueAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("DueAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("DueDate").Caption = "Due Date"
            Me.grd.RootTable.Columns("DueDate").FormatString = str_DisplayDateFormat
            Me.grd.RootTable.Columns("Adj_CL").Caption = "Adj wrt CL"
            Me.grd.RootTable.Columns("Adj_CL").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Adj_CL").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Adj_CL").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("RemDue").Caption = "Received Amount"
            Me.grd.RootTable.Columns("RemDue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("RemDue").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("RemDue").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("NotDue").Caption = "Remaining Due"
            Me.grd.RootTable.Columns("NotDue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("NotDue").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("NotDue").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("NotDueAmount").Caption = "Not Due"
            Me.grd.RootTable.Columns("NotDueAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("NotDueAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("NotDueAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "HeadCostCenter" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
                'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
                'Start TFS3412
                lstHeadCostCenter.DeSelect()
                'End TFS3412
            ElseIf Condition = "CostCenter" Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1")
                'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
                'Start TFS3412
                lstCostCenter.DeSelect()
                'End TFS3412
            ElseIf Condition = "Company" Then
                FillUltraDropDown(Me.cmbCompany, "SELECT ISNULL(CompanyId,0) CompanyId, CompanyName FROM CompanyDefTable ORDER BY CompanyId ASC", True)
            ElseIf Condition = "Customer" Then
                FillUltraDropDown(Me.cmbCustomer, "SELECT coa_detail_id as id, detail_title AS Name, detail_code AS Code, Contact_Mobile AS Mobile FROM vwCOADetail WHERE (account_type = 'Customer') AND (coa_detail_id IS NOT NULL) AND (Active = 1) ORDER BY Name", True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If cmbCustomer.Value = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.cmbPeriod.SelectedIndex = 0
            Me.cmbPeriod.Text = "Current Month"
            FillCombos("HeadCostCenter")
            FillCombos("CostCenter")
            FillCombos("Company")
            FillCombos("Customer")
            Me.UltraTabControl1.Tabs(0).Selected = True
            If Me.cmbCompany.Rows.Count > 0 Then cmbCompany.Rows(0).Activate()
            If Me.cmbCustomer.Rows.Count > 0 Then cmbCustomer.Rows(0).Activate()
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmRptInvoiceAging_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Invoice Aging" & Chr(10) & "Customer Name:" & cmbCustomer.Text & Chr(10) & "From Date : " & Me.dtpFromDate.Value.ToString("dd-MM-yyyy") & " To Date : " & Me.dtpToDate.Value.ToString("dd-MM-yyyy") & Chr(10) & "Opening Balance:" & lblOpeningBalance.Text & Chr(10) & "Credit Limit:" & lblCreditLimit.Text & Chr(10) & "Remaining CL:" & lblRemainingCreditLimit.Text & Chr(10) & "Credit Days:" & lblCreditDays.Text
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            If IsValidate() = False Then
                ShowErrorMessage("Please select an Account")
                Exit Sub
            End If
            If GetInfo() = False Then
                ShowErrorMessage("There is some issue fetching Opening Balance")
                Exit Sub
            End If

            If GetFirstRecord() = False Then
                ShowErrorMessage("There is some issue fetching Due Amount")
                Exit Sub
            End If
            If dt1.Rows.Count > 0 Then
                GetInvoiceAging(dt1)
            Else
                ShowErrorMessage("No Record Found")
                grd.DataSource = Nothing
                Me.UltraTabControl1.Tabs(1).Selected = True
                Exit Sub
            End If

            If NotDuedt.Rows.Count > 0 Or dt1.Rows.Count > 0 Then
                ApplyGridSettings()
            End If
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.UltraTabControl1.Tabs(1).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function GetInfo() As Boolean
        Try
            Dim str As String
            Dim dt As DataTable
            str = "SELECT SUM(ISNULL(tblVoucherDetail.debit_amount, 0)) - SUM(ISNULL(tblVoucherDetail.credit_amount, 0)) AS Balance FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.voucher_id = tblVoucher.voucher_id WHERE " & IIf(cmbCustomer.Value = 0, "", " tblVoucherDetail.coa_detail_id = " & cmbCustomer.Value & " AND") & " tblVoucher.voucher_date < '" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "'"
            dt = GetDataTable(str)
            CustomerReceiveables = Val(dt.Rows(0).Item("Balance").ToString)
            lblOpeningBalance.Text = CustomerReceiveables
            Dim str1 As String
            Dim dt1 As DataTable
            str1 = "SELECT SUM(ISNULL(tblVoucherDetail.debit_amount, 0)) - SUM(ISNULL(tblVoucherDetail.credit_amount, 0)) AS Balance " & _
                  "FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.voucher_id = tblVoucher.voucher_id INNER JOIN vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id WHERE " & IIf(cmbCustomer.Value = 0, "", " tblVoucherDetail.coa_detail_id = " & cmbCustomer.Value & " AND") & "" & IIf(cmbCompany.Value = 0, "", " tblVoucherDetail.Location_id = " & cmbCompany.Value & " AND") & " tblVoucher.voucher_date + CreditDays > '" & dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' " & IIf(lstCostCenter.SelectedIDs = "", "", "AND tblVoucherDetail.CostCenterID IN (" & lstCostCenter.SelectedIDs & ")") & ""
            dt1 = GetDataTable(str1)
            NotDue = Val(dt1.Rows(0).Item("Balance").ToString)
            Dim str3 As String
            str3 = "select ISNULL(CridtLimt, 0) as CridtLimt, ISNULL(CreditDays, 0) as CreditDays from tblCustomer where AccountId = " & cmbCustomer.Value
            Dim Creditdt As DataTable
            Creditdt = GetDataTable(str3)
            If Creditdt.Rows.Count > 0 Then
                CreditLimit = Val(Creditdt.Rows(0).Item("CridtLimt").ToString)
                Dim CreditDays As Integer = Val(Creditdt.Rows(0).Item("CreditDays").ToString)
                lblCreditLimit.Text = CreditLimit
                lblCreditDays.text = CreditDays

            Else
                lblCreditDays.text = 0
                lblCreditLimit.Text = 0
            End If
            Dim str4 As String
            Dim Receiveablesdt As DataTable
            str4 = "SELECT SUM(ISNULL(tblVoucherDetail.credit_amount, 0)) AS Balance FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.voucher_id = tblVoucher.voucher_id WHERE " & IIf(cmbCustomer.Value = 0, "", " tblVoucherDetail.coa_detail_id = " & cmbCustomer.Value & " AND") & " tblVoucher.voucher_date BETWEEN '" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' AND '" & dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "'"
            Receiveablesdt = GetDataTable(str4)
            Received = Val(Receiveablesdt.Rows(0).Item("Balance").ToString)
            lblReceivables.Text = Received
            If Received > 0 Then
                NetAmount = Received - CustomerReceiveables
            Else
                NetAmount = CustomerReceiveables
            End If

            Balance = NetAmount ' - NotDue
            CreditLimit = Balance + CreditLimit
            'NetAmount = Received - CustomerReceiveables
            'Balance = NetAmount
            'CredditBaalance = CreditLimit
            Return True
        Catch ex As Exception
            Return False
            Throw ex
        End Try
    End Function

    Private Function GetFirstRecord() As Boolean
        Try
            Dim str As String
            Dim dt As DataTable
            str = "SELECT  tblVoucher.voucher_no, tblVoucher.voucher_date, SUM(ISNULL(tblVoucherDetail.debit_amount, 0)) - SUM(ISNULL(tblVoucherDetail.credit_amount, 0)) AS Amount, 0 as DueAmount, (tblVoucher.voucher_date + CreditDays) as DueDate, 0 as Adj_CL,0 as RemDue, 0 as NotDue " & _
                  "FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.voucher_id = tblVoucher.voucher_id INNER JOIN vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id where tblVoucherDetail.coa_detail_id = " & cmbCustomer.Value & " AND tblVoucher.voucher_date + CreditDays BETWEEN '" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' AND '" & dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' " & IIf(lstCostCenter.SelectedIDs = "", "", "AND tblVoucherDetail.CostCenterID IN (" & lstCostCenter.SelectedIDs & ")") & " AND tblVoucher.voucher_no like '%SI%' Group by tblVoucher.voucher_date, tblVoucher.voucher_no, vwCOADetail.CreditDays Order by tblVoucher.voucher_date asc"
            dt = GetDataTable(str)
            dt1 = dt.Clone
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    If Balance > 0 Then
                        If Balance > Val(row.Item("Amount").ToString) Then
                            row.BeginEdit()
                            row.Item("DueAmount") = Val(row.Item("Amount").ToString)
                            row.Item("RemDue") = Val(row.Item("Amount").ToString)
                            row.EndEdit()
                            Balance = Balance - Val(row.Item("Amount").ToString)
                            'CreditLimit = CreditLimit - Val(row.Item("Amount").ToString)
                        Else
                            row.BeginEdit()
                            row.Item("DueAmount") = Val(row.Item("Amount").ToString)
                            row.Item("RemDue") = Balance
                            row.EndEdit()
                            Balance = Balance - Val(row.Item("Amount").ToString)
                            'CreditLimit = CreditLimit - Val(row.Item("Amount").ToString)
                        End If
                        dt1.ImportRow(row)
                    Else
                        row.BeginEdit()
                        row.Item("DueAmount") = Val(row.Item("Amount").ToString)
                        row.Item("RemDue") = 0
                        row.EndEdit()
                        dt1.ImportRow(row)
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            Return False
            Throw ex
        End Try
    End Function


    Private Function GetInvoiceAging(ByVal dt As DataTable) As Boolean
        Try
            Dim Agingdt As DataTable
            Agingdt = dt.Clone
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    'If NetAmount > Val(row.Item("DueAmount").ToString) Then
                    '    row.BeginEdit()
                    '    row.Item("RemDue") = Val(row.Item("DueAmount").ToString)
                    '    row.Item("NotDue") = Val(row.Item("DueAmount").ToString) - Val(row.Item("Adj_CL").ToString)
                    '    row.EndEdit()
                    'End If
                    If CreditLimit > Val(row.Item("DueAmount").ToString) Then
                        row.BeginEdit()
                        row.Item("Adj_CL") = Val(row.Item("DueAmount").ToString)
                        row.Item("NotDue") = Val(row.Item("DueAmount").ToString) - Val(row.Item("Adj_CL").ToString)
                        row.EndEdit()
                        CreditLimit = CreditLimit - Val(row.Item("DueAmount").ToString)
                        lblRemainingCreditLimit.Text = CreditLimit
                    Else
                        row.BeginEdit()
                        row.Item("DueAmount") = Val(row.Item("Amount").ToString) - CreditLimit
                        If CreditLimit <> 0 Then
                            row.Item("Adj_CL") = CreditLimit - Val(row.Item("RemDue").ToString)
                        Else
                            row.Item("Adj_CL") = 0
                        End If
                        row.Item("NotDue") = Val(row.Item("Amount").ToString) - Val(row.Item("Adj_CL").ToString) - Val(row.Item("RemDue").ToString)
                        row.EndEdit()
                        CreditLimit = 0
                        lblRemainingCreditLimit.Text = CreditLimit
                    End If
                    Agingdt.ImportRow(row)
                Next


                NotDuedt = Agingdt.Copy
                NotDuedt.Columns.Add("NotDueAmount")
                For Each row As DataRow In NotDuedt.Rows
                    row.BeginEdit()
                    row.Item("NotDueAmount") = 0
                    row.EndEdit()
                Next
                Dim str1 As String = "SELECT  tblVoucher.voucher_no, tblVoucher.voucher_date, SUM(ISNULL(tblVoucherDetail.debit_amount, 0)) - SUM(ISNULL(tblVoucherDetail.credit_amount, 0)) AS Amount, 0 as DueAmount, (tblVoucher.voucher_date + CreditDays) as DueDate, 0 as Adj_CL,0 as RemDue, 0 as NotDue, SUM(ISNULL(tblVoucherDetail.debit_amount, 0)) - SUM(ISNULL(tblVoucherDetail.credit_amount, 0)) AS NotDueAmount " & _
                  "FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.voucher_id = tblVoucher.voucher_id INNER JOIN vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id where tblVoucherDetail.coa_detail_id = " & cmbCustomer.Value & " AND tblVoucher.voucher_date + CreditDays > '" & dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "'" & IIf(lstCostCenter.SelectedIDs = "", "", "AND tblVoucherDetail.CostCenterID IN (" & lstCostCenter.SelectedIDs & ")") & " AND tblVoucher.voucher_no like '%SI%' Group by tblVoucher.voucher_date, tblVoucher.voucher_no, vwCOADetail.CreditDays"
                Dim dt1 As DataTable
                dt1 = GetDataTable(str1)
                For Each row1 As DataRow In dt1.Rows
                    NotDuedt.ImportRow(row1)
                Next
                grd.DataSource = NotDuedt
                grd.RetrieveStructure()
                If chkIncludeInvoices.Checked = False Then
                    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                        If row.Cells("NotDue").Value = 0 Then
                            grd.GetRow.Delete()
                            grd.UpdateData()
                        End If
                    Next
                End If
            Else
                grd.DataSource = Nothing
            End If
            Return True
        Catch ex As Exception
            Return False
            Throw ex
        End Try
    End Function

    Private Sub cmbPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriod.SelectedIndexChanged
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class