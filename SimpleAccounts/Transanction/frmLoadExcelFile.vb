Imports System.Data.OleDb
Imports SBModel
Imports SBDal
Imports System
Imports System.Linq

Public Class frmLoadExcelFile
    Dim isloaded As Boolean
    'Dim VoucherNo As String = GetVoucherNo()
    Dim CurrencyId As Integer
    Dim CurrencyRate As Double
    Dim TransactionDate As String
    Dim CustomerId As Integer
    Dim MainId As Integer
    Dim DetailId As Integer
    Dim title As String
    Dim dt As New DataTable

    Private Structure GridColumns
        Public Const TRANS_DATE As String = "Reg_Date"
        Public Const CATEGORY As String = "Category"
        Public Const SERVICE As String = "Service"
        Public Const SUB_SERVICE As String = "Sub_Service"
        Public Const CUSTOMER As String = "Customers"
        Public Const AMOUNT As String = "Amount"
        Public Const CURRENCY As String = "Currency"
        
    End Structure

    Dim UniqueCustomerNames(10) As String
    Dim UniqueCustomersAdded As Integer = 0

    Private Sub ExtractCustomersFromDT(ByVal dt As DataTable)
        Dim RowCount As Integer = dt.Rows.Count

        If RowCount + Me.UniqueCustomersAdded > Me.UniqueCustomerNames.Length Then
            ReDim Preserve Me.UniqueCustomerNames(Me.UniqueCustomerNames.Length + RowCount + 10)
        End If

        For Each row As DataRow In dt.Rows
            Me.UniqueCustomerNames(Me.UniqueCustomersAdded) = row.Item(GridColumns.CUSTOMER)
            Me.UniqueCustomersAdded += 1
        Next

        'MsgBox(Me.UniqueCustomerNames.ToArray().ToString)
        MsgBox(Me.UniqueCustomerNames(70))
    End Sub


    Private Function ProcessImportedData(ByVal dt As DataTable) As Boolean
        Dim trans As OleDbTransaction

        Try

            Dim lngVoucherMasterId As Integer
            Dim objCon As OleDbConnection
            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            Dim cmd As New OleDbCommand

            cmd.Connection = objCon
            trans = objCon.BeginTransaction     '>>>>> Begin Transaction
            cmd.Transaction = trans

            Dim dtUniqueDates As DataTable = dt.DefaultView.ToTable(True, GridColumns.TRANS_DATE)

            'Dim CustomerName As String
            Dim FilterExpression As String
            Dim TotalAmount As Double = 0.0

            For Each row As DataRow In dtUniqueDates.Rows
                TransactionDate = row.Item(GridColumns.TRANS_DATE)
                'MsgBox(TransactionDate)

                Dim dtUniqueCustomers As DataTable = dt.DefaultView.ToTable(True, GridColumns.CUSTOMER)
                Dim dtUniqueTransactionCurrencies As DataTable = dt.DefaultView.ToTable(True, GridColumns.CURRENCY)

                For Each cust As DataRow In dtUniqueCustomers.Rows
                    'If IsDBNull(cust.Item(GridColumns.CUSTOMER)) = True Then
                    '    cust.Item(GridColumns.CUSTOMER) = ""
                    'End If
                    For Each Curr As DataRow In dtUniqueTransactionCurrencies.Rows
                        FilterExpression = GridColumns.TRANS_DATE & " = #" & TransactionDate & "# AND " _
                                                     & GridColumns.CUSTOMER & " = '" & cust.Item(GridColumns.CUSTOMER) & "'" _
                                                     & " AND " & GridColumns.CURRENCY & " = '" & Curr.Item(GridColumns.CURRENCY) & "'"

                        Dim DataForVoucher() As DataRow
                        DataForVoucher = dt.Select(FilterExpression)

                        If DataForVoucher.Length > 0 Then
                            'Start transaction

                            'Dim VoucherNo As String = GetVoucherNo(TransactionDate)
                            'Dim dtVoucher As DataTable
                            Dim VNO As String
                            Dim Voucher As String = ""
                            Dim dtVoucher As New DataTable
                            cmd.CommandText = "Select ISNULL(Max(Right(voucher_no,5)),0) as Voucher  From tblVoucher WHERE LEFT(voucher_no,6)='SV" + "-" + Microsoft.VisualBasic.Right(Convert.ToDateTime(TransactionDate).Year, 2) + "-'"
                            Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmd)
                            da.Fill(dtVoucher)
                            Voucher = dtVoucher.Rows(0).Item("Voucher")
                            Voucher = Voucher + 1
                            VNO = CStr("SV" + "-" + Microsoft.VisualBasic.Right(Convert.ToDateTime(TransactionDate).Year, 2) + "-" + Microsoft.VisualBasic.Right("00000" + CStr(Voucher), 5))


                            'Insert voucher master entry
                            cmd.CommandText = ""
                            cmd.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                                   & " cheque_no, cheque_date,post,Source,voucher_code, Employee_Id, Remarks, UserName)" _
                                                   & " VALUES(0, 1,  7 , N'" & VNO & "', N'" & Convert.ToDateTime(TransactionDate).ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                                   & " NULL, NULL, 1,N'" & Me.Name & "',N'" & VNO & "', NULL, N'" & Me.txtRemarks.Text.Replace("'", "''") & "', '" & LoginUserName & "')" _
                                                   & " SELECT @@IDENTITY"
                            lngVoucherMasterId = cmd.ExecuteScalar
                            'MsgBox("Voucher Master Inserted")

                            'Insert voucher details entries
                            For Each dr As DataRow In DataForVoucher
                                'insert voucher detail entries
                                If ValidateCustomer(cust.Item(GridColumns.CUSTOMER)) = False Then
                                    ShowErrorMessage(" Customer '" & cust.Item(GridColumns.CUSTOMER) & "' is not defined in Chart Of Account. Please define it before importing data")
                                    trans.Rollback()
                                    Return False
                                End If

                                If ValidateCurrency(Curr.Item(GridColumns.CURRENCY)) = False Then
                                    ShowErrorMessage("Currency " & Curr.Item(GridColumns.CURRENCY) & " does not exists. Please define it before importing data")
                                    trans.Rollback()
                                    Return False
                                End If

                                cmd.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, Currency_debit_amount, Currency_Credit_Amount, CurrencyId, CurrencyRate, Currency_Symbol) " _
                                                & " VALUES(" & lngVoucherMasterId & ", " & cmbCompany.SelectedValue & ", " & CustomerId & ", " & dr.Item(GridColumns.AMOUNT) * CurrencyRate & ", 0," & dr.Item(GridColumns.AMOUNT) & ", 0, " & CurrencyId & ", " & CurrencyRate & ",'" & Curr.Item(GridColumns.CURRENCY) & "')"
                                cmd.ExecuteNonQuery()
                                cmd.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, Currency_debit_amount, Currency_Credit_Amount, CurrencyId, CurrencyRate, Currency_Symbol) " _
                                                                          & " VALUES(" & lngVoucherMasterId & ", " & cmbCompany.SelectedValue & ", " & DetailId & ", " & 0 & ",  " & dr.Item(GridColumns.AMOUNT) * CurrencyRate & ", 0 , " & dr.Item(GridColumns.AMOUNT) & ", " & CurrencyId & ", " & CurrencyRate & ",'" & Curr.Item(GridColumns.CURRENCY) & "')"
                                cmd.ExecuteNonQuery()
                            Next
                        End If

                    Next  'For Each Curr
                Next  'For Each cust

            Next  ' For Each row 

            trans.Commit()

            msg_Information("The data has been loaded successfully")
            Me.Close()
            ' MsgBox("Total Amount = " & TotalAmount)

        Catch ex As Exception
            Try
                trans.Rollback()
            Catch ex2 As Exception
                Throw ex2
            End Try

            Throw ex
        End Try



    End Function
    Private Sub frmLoadExcelFile_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            isloaded = True
            Dim str As String
            str = "SELECT * FROM CompanyDefTable"
            FillDropDown(Me.cmbCompany, str, False)
            Dim i As Integer

            Dim dt1 As New DataTable
            Dim uniqueDate As New DataTable
            Dim DTCloned As Boolean = False

            'Dim dt As New DataTable

            For i = 0 To frmRevenueDataImport.grd.GetRows.Length - 1
                'Create connection string to Excel work book
                Dim excelConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & frmRevenueDataImport.grd.GetRows(i).Cells("PathName").Value.ToString & ";Extended Properties=Excel 12.0;"
                'Create Connection to Excel work book
                Dim excelConnection As New OleDb.OleDbConnection(excelConnectionString)
                'Create OleDbCommand to fetch data from Excel
                If excelConnection.State = ConnectionState.Closed Then excelConnection.Open()
                Dim cmd As New OleDb.OleDbCommand("Select Format([DATE],'dd-MMM-yyyy')  as Reg_Date, [CATEGORY], [SERVICE],[SUB_SERVICE], [CUSTOMERS], [AMOUNT], [CURRENCY] from [Sheet1$]", excelConnection)

                Dim objdataadapter As New OleDb.OleDbDataAdapter
                objdataadapter.SelectCommand = cmd
                objdataadapter.Fill(dt1)

                Dim count As Integer = dt.Rows.Count
                dt = dt1.Clone
                'DTCloned = True
                'End If
                dt.Merge(dt1)
                
            Next

            grd.DataSource = dt
            grd.AutoSizeColumns()

            'Check if customer name is missing on one or more rows
            Dim DefaultAccountInAbsenceOfCustomer As String = getConfigValueByType("DefaultAccountInPlaceCustomer")
            GetTitleName(DefaultAccountInAbsenceOfCustomer)
            For Each row As DataRow In dt.Rows
                If IsDBNull(row.Item(GridColumns.CUSTOMER)) = True Then
                    If DefaultAccountInAbsenceOfCustomer = "Error" Then
                        'Configuration Value was not found
                        ShowErrorMessage("Default account used when a customer is not present is not configured in System Configuration (Sales Configuration)")
                        Exit Sub
                    End If

                    row.Item(GridColumns.CUSTOMER) = title
                End If
            Next
            
            Dim grdGroupBy As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns(GridColumns.TRANS_DATE))
            grdGroupBy.GroupPrefix = String.Empty
            Me.grd.RootTable.Groups.Add(grdGroupBy)
            Dim grdGroupBy1 As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns(GridColumns.CUSTOMER))
            grdGroupBy1.GroupPrefix = String.Empty
            Me.grd.RootTable.Groups.Add(grdGroupBy1)
            Dim grdGroupBy2 As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns(GridColumns.CURRENCY))
            grdGroupBy2.GroupPrefix = String.Empty
            Me.grd.RootTable.Groups.Add(grdGroupBy2)

            Me.grd.RootTable.Columns(GridColumns.AMOUNT).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(GridColumns.TRANS_DATE).FormatString = str_DisplayDateFormat
            'GetVoucherNo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message & vbCrLf & "Please check column names are valid")
        End Try
    End Sub

    Private Function GetTitleName(ByVal id As Integer) As Boolean
        Try
            Dim str As String
            str = "select detail_title from vwCOADetail where coa_detail_id = " & id & ""
            Dim dt As DataTable
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                title = dt.Rows(0).Item("detail_title")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Function GetVoucherNo(ByVal TransDate As String) As String

        Dim docNo As String = String.Empty
        Dim VType As String = String.Empty
        Try
            If isloaded = True Then
                If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                    Return GetSerialNo("SV" + "-" + Microsoft.VisualBasic.Right(Convert.ToDateTime(TransDate).Year, 2) + "-", "tblVoucher", "voucher_no")
                Else
                    Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
                    Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
                    If Not dr Is Nothing Then
                        If dr("config_Value") = "Monthly" Then
                            Return GetNextDocNo("SV" & "-" & Format(TransDate, "yy") & Convert.ToDateTime(TransDate).Month.ToString("00"), 4, "tblVoucher", "voucher_no")
                        Else
                            docNo = GetNextDocNo("SV", 6, "tblVoucher", "voucher_no")
                            Return docNo
                        End If
                    Else
                        docNo = GetNextDocNo("SV", 6, "tblVoucher", "voucher_no")
                        Return docNo
                    End If
                    Return ""
                End If
                'Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Function to get Values from datatable separated by comma's 
    Private Function GetCommaSeparatedValues(ByVal dt As DataTable) As String
        Dim ReturnValue As String = ""
        Dim Counter As Integer = 0

        For Each row As DataRow In dt.Rows
            If Counter = 0 Then
                ReturnValue = "('" & row.Item(GridColumns.CUSTOMER) & "')"
            Else
                ReturnValue += ", ('" & row.Item(GridColumns.CUSTOMER) & "')"
            End If
            Counter += 1
        Next

        Return ReturnValue

    End Function

    Private Function GetUndefinedCustomerNames(ByVal dt As DataTable) As String
        Try
            Dim objCon As OleDbConnection
            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            Dim cmd As New OleDbCommand

            cmd.Connection = objCon

            Dim Qry As String
            Const TEMP_TABLE_NAME As String = "TempRevenueDataImport" '"#UniqueCustomers"


            Qry = "TRUNCATE TABLE " & TEMP_TABLE_NAME
            cmd.CommandText = Qry
            cmd.ExecuteNonQuery()

            'Insert unique customer names found in Excel file in temp table
            Dim Values As String = Me.GetCommaSeparatedValues(dt)
            Qry = "INSERT INTO " & TEMP_TABLE_NAME & " (Name) VALUES " & Values
            cmd.CommandText = Qry
            cmd.ExecuteNonQuery()

            'Find out which customers were not found in detail level account
            Dim dtCustomersNotFound As DataTable
            Qry = "SELECT t.Name FROM " & TEMP_TABLE_NAME & " t LEFT OUTER JOIN tblCOAMainSubSubDetail d ON t.Name = d.detail_title WHERE d.detail_title IS NULL"

            dtCustomersNotFound = GetDataTable(Qry)

            objCon.Close()
            Return ModGlobel.GetRowValuesIntoString(dt, GridColumns.CUSTOMER)

        Catch ex As Exception
            'Throw ex
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Private Function ValidateCustomer2(ByVal dtUniqueCustomerNameas As DataTable) As Boolean


        Dim UndefinedCustomerNames = Me.GetUndefinedCustomerNames(dtUniqueCustomerNameas)

        If UndefinedCustomerNames <> "" Then
            MsgBox(UndefinedCustomerNames)
        End If

    End Function

    Private Function ValidateSubService(ByVal dt As DataTable) As Boolean
        Try
            Dim FilterExpression As String = ""
            Dim objCon As OleDbConnection
            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon

            Dim SubSubId As Integer
            Dim dtSubService As New DataTable

            Dim SubServiceValue As String

            Dim dtUniqueSubService As DataTable = dt.DefaultView.ToTable(True, GridColumns.SUB_SERVICE)
            For Each SubService As DataRow In dtUniqueSubService.Rows
                'Check the possibility that value may be empty or null
                SubServiceValue = SubService.Item(GridColumns.SUB_SERVICE)
                If IsDBNull(SubService.Item(GridColumns.SUB_SERVICE)) = True Then
                    ShowErrorMessage("A value in service column is missing or empty")
                    Return False
                Else
                    SubServiceValue = SubService.Item(GridColumns.SUB_SERVICE)
                End If


                cmd.CommandText = "select coa_Detail_Id from tblCOAMainSubSubDetail where detail_title = '" & SubServiceValue & "'"
                Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmd)
                da.Fill(dtSubService)
                If dtSubService.Rows.Count > 0 Then
                    DetailId = dtSubService.Rows(0).Item("coa_Detail_Id")
                    'Return True
                Else

                    FilterExpression = GridColumns.SUB_SERVICE & " = '" & SubServiceValue & "'"

                    Dim DataForService() As DataRow
                    DataForService = dt.Select(FilterExpression)

                    Dim Service As String = ""
                    For Each dr As DataRow In DataForService
                        Service = dr.Item(GridColumns.SERVICE)
                    Next
                    Dim dtService As New DataTable
                    cmd.CommandText = "select main_sub_sub_id from tblCOAMainSubSub where sub_sub_title = '" & Service & "'"
                    Dim da1 As OleDbDataAdapter = New OleDbDataAdapter(cmd)
                    da1.Fill(dtService)
                    SubSubId = dtService.Rows(0).Item("main_sub_sub_id")

                    cmd.CommandText = "INSERT INTO [tblcoamainsubsubdetail]([main_sub_sub_id], [detail_title]) " & _
                                        "VALUES( " & SubSubId & ", N'" & SubServiceValue & "')" & _
                                        " SELECT @@IDENTITY"
                    DetailId = cmd.ExecuteScalar()

                    cmd.CommandText = "INSERT INTO [SubServiceMapping]([SubService_Name], [Detail_Id]) " & _
                                "VALUES( N'" & SubServiceValue & "', " & DetailId & ")"
                End If
            Next

            Return True

        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    Private Function ValidateCustomer(ByVal CustomerName As String) As Boolean
        Try

            Dim str As String
            Dim i As Integer
            If CustomerName <> "" Then
                str = "select coa_detail_id from vwCOADetail where detail_title = '" & CustomerName & "'"
                Dim dt As DataTable
                dt = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    CustomerId = dt.Rows(0).Item("coa_detail_id")
                    Return True
                Else
                    Return False
                    'Exit Function
                End If
            Else
                CustomerId = Val(getConfigValueByType("DefaultAccountInPlaceCustomer"))
                If CustomerId = 0 Then
                    ShowErrorMessage("Default account used when a customer is not present is not configured in System Configuration (Sales Configuration)")
                    Exit Function
                End If
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function ValidateDate(ByVal dt As DataTable) As Boolean
        Try
            Dim objCon As OleDbConnection
            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon


            Dim dtUniqueDate As DataTable = dt.DefaultView.ToTable(True, GridColumns.TRANS_DATE)

            Dim DateValue As String

            For Each UDate As DataRow In dtUniqueDate.Rows
                'Check the possibility that value may be empty or null
                If IsDBNull(UDate.Item(GridColumns.TRANS_DATE)) = True Then
                    ShowErrorMessage("A value in Date column is missing or empty")
                    Return False
                Else
                    DateValue = UDate.Item(GridColumns.TRANS_DATE)
                End If
            Next
            Return True

        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function


    Private Function ValidateEmptyCurrency(ByVal dt As DataTable) As Boolean
        Try
            Dim objCon As OleDbConnection
            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon


            Dim dtUniqueCurrency As DataTable = dt.DefaultView.ToTable(True, GridColumns.CURRENCY)

            Dim DateValue As String

            For Each Curr As DataRow In dtUniqueCurrency.Rows
                'Check the possibility that value may be empty or null
                If IsDBNull(Curr.Item(GridColumns.CURRENCY)) = True Then
                    ShowErrorMessage("A value in Currency column is missing or empty")
                    Return False
                Else
                    DateValue = Curr.Item(GridColumns.CURRENCY)
                End If
            Next
            Return True

        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function


    Private Function ValidateAmount(ByVal dt As DataTable) As Boolean
        Try
            Dim objCon As OleDbConnection
            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon


            Dim dtUniqueAmount As DataTable = dt.DefaultView.ToTable(True, GridColumns.AMOUNT)

            Dim AmountValue As String

            For Each Amount As DataRow In dtUniqueAmount.Rows
                'Check the possibility that value may be empty or null
                If IsDBNull(Amount.Item(GridColumns.AMOUNT)) = True Then
                    ShowErrorMessage("A value in Amount column is missing or empty")
                    Return False
                Else
                    AmountValue = Amount.Item(GridColumns.AMOUNT)
                End If
            Next
            Return True

        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Private Function ValidateCategory(ByVal dt As DataTable) As Boolean
        Try
            Dim objCon As OleDbConnection
            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon


            Dim dtUniqueCategory As DataTable = dt.DefaultView.ToTable(True, GridColumns.CATEGORY)

            Dim SubId As Integer
            Dim dtCategory As New DataTable
            Dim CategoryValue As String

            For Each Cat As DataRow In dtUniqueCategory.Rows
                'Check the possibility that value may be empty or null
                If IsDBNull(Cat.Item(GridColumns.CATEGORY)) = True Then
                    ShowErrorMessage("A value in category column is missing or empty")
                    Return False
                Else
                    CategoryValue = Cat.Item(GridColumns.CATEGORY)
                End If

                cmd.CommandText = "select main_sub_id from tblCOAMainSub where sub_title = '" & CategoryValue & "'"
                Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmd)

                da.Fill(dtCategory)

                If dtCategory.Rows.Count > 0 Then
                    SubId = dtCategory.Rows(0).Item("main_sub_id")
                    'Return True
                Else
                    Dim Temp As String

                    Temp = getConfigValueByType("MainAccountforRevenueImport")
                    If Temp = "Error" Then
                        'Error finding value of configuration
                        ShowErrorMessage("Main level account is required to define missing category." & vbCrLf & "Please ensure this account is configured in System Configuration >> Accounts Configuration")
                        Return False
                    End If

                    MainId = Val(Temp)  'Convert string to number
                    cmd.CommandText = "INSERT INTO [tblCOAMainSub]([coa_main_id], [sub_title]) " & _
                                    "VALUES(" & MainId & ", N'" & Cat.Item(GridColumns.CATEGORY) & "')" & _
                                    " SELECT @@IDENTITY"
                    Dim NewSubId As Integer = cmd.ExecuteScalar

                    'Insert missing category in category mapping table
                    cmd.CommandText = "INSERT INTO [CategoryMapping]([Category_Name], [SubId]) " & _
                                    "VALUES(N'" & Cat.Item(GridColumns.CATEGORY) & "', " & NewSubId & ")"
                    cmd.ExecuteNonQuery()
                End If
            Next

            Return True

        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Private Function ValidateService(ByVal dt As DataTable) As Boolean
        Try
            Dim FilterExpression As String
            Dim objCon As OleDbConnection

            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()

            Dim cmd As New OleDbCommand
            cmd.Connection = objCon

            Dim SubSubId As Integer
            Dim SubId As Integer
            Dim dtService As New DataTable

            Dim ServiceValue As String

            Dim dtUniqueService As DataTable = dt.DefaultView.ToTable(True, GridColumns.SERVICE)
            For Each Service As DataRow In dtUniqueService.Rows
                'Check the possibility that value may be empty or null

                If IsDBNull(Service.Item(GridColumns.SERVICE)) = True Then
                    ShowErrorMessage("A value in service column is missing or empty")
                    Return False
                Else
                    ServiceValue = Service.Item(GridColumns.SERVICE)
                End If

                cmd.CommandText = "select main_sub_sub_id from vwCOADetail where sub_sub_title = '" & ServiceValue & "'"
                Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmd)
                da.Fill(dtService)
                If dtService.Rows.Count > 0 Then
                    SubSubId = dtService.Rows(0).Item("main_sub_sub_id")
                    'Return True
                Else
                    FilterExpression = GridColumns.SERVICE & " = '" & ServiceValue & "'"

                    Dim DataForCategory() As DataRow
                    DataForCategory = dt.Select(FilterExpression)

                    Dim Category As String = ""
                    For Each dr As DataRow In DataForCategory
                        Category = dr.Item(GridColumns.CATEGORY)
                    Next
                    Dim dtCategory As New DataTable
                    cmd.CommandText = "select main_sub_id from tblCOAMainSub where sub_title = '" & Category & "'"
                    Dim da1 As OleDbDataAdapter = New OleDbDataAdapter(cmd)
                    da1.Fill(dtCategory)
                    SubId = dtCategory.Rows(0).Item("main_sub_id")

                    cmd.CommandText = "INSERT INTO [tblCOAMainSubSub]([main_sub_id], [sub_sub_title]) " & _
                                        "VALUES( " & SubId & ", N'" & Service.Item(GridColumns.SERVICE) & "')" & _
                                        " SELECT @@IDENTITY"
                    Dim NewSubSubId As Integer = cmd.ExecuteScalar()

                    cmd.CommandText = "INSERT INTO [ServiceMapping]([Service_Name], [SubSubId]) " & _
                                "VALUES( N'" & Service.Item(GridColumns.SERVICE) & "', " & NewSubSubId & ")"
                    cmd.ExecuteNonQuery()
                End If
            Next

            Return True

        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function



    Private Function ValidateCurrency(ByVal Currency As String) As Boolean
        Try
            Dim str As String
            str = "Select tblCurrency.currency_id as currency_id,  IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId where tblCurrency.currency_code = N'" & Currency & "'"
            Dim dt As DataTable
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                CurrencyId = dt.Rows(0).Item("currency_id")
                CurrencyRate = dt.Rows(0).Item("CurrencyRate")
                Return True
            Else
                'ShowErrorMessage("Currency " & Currency & "not found")
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function ValidateData() As Boolean
        'Validates the Excel data

        'If ValidateCustomer() = False Then Exit Function

    End Function

    Private Sub btnImportData_Click(sender As Object, e As EventArgs) Handles btnImportData.Click

        'Dim trans As OleDbTransaction
        Try
            'Validate Dates if they exist or not. If Not Show Error Msg
            If ValidateDate(dt) = False Then
                Exit Sub
            End If

            'Validate categories if they exist or not. Defeine them if not
            If ValidateCategory(dt) = False Then
                Exit Sub
            End If

            'Validate Empty Currencies if they exist or not. Defeine them if not
            If ValidateEmptyCurrency(dt) = False Then
                Exit Sub
            End If

            'Validate service names if they exist or not. Defeine them if not
            If ValidateService(dt) = False Then
                Exit Sub
            End If

            'Validate sub-service names if they exist or not. Defeine them if not
            If ValidateSubService(dt) = False Then
                Exit Sub
            End If

            'Validate Amounts if they exist or not. If Not Show Error Msg
            If ValidateAmount(dt) = False Then
                Exit Sub
            End If

            ProcessImportedData(dt)

            'Dim i As Integer
            'Dim CustomerId As Integer
            'Dim DetailId As Integer
            'Dim SubSubId As Integer
            'Dim SubId As Integer
            'Dim objCommand As New OleDbCommand
            'Dim objCon As OleDbConnection
            'objCon = Con
            'If objCon.State = ConnectionState.Open Then objCon.Close()
            'objCon.Open()
            'objCommand.Connection = objCon
            'trans = objCon.BeginTransaction
            'objCommand.Transaction = trans
            'Dim VoucherNo As String = GetVoucherNo()
            'Dim lngVoucherMasterId As Integer
            'For i = 0 To grd.GetRows.Length - 1

            '    'Check if Detail Id exists in SUB_SERVICE_MAPPING Table
            '    Dim str1 As String
            '    str1 = "select DetailId from SubServiceMapping where SubService_Name = '" & grd.GetRows(i).Cells(GridColumns.TRANS_DATE).Value.ToString & "'"
            '    Dim dt1 As DataTable
            '    dt1 = GetDataTable(str1)

            '    'Checking if DataTable having anything to show against that query.
            '    If dt1.Rows.Count > 0 Then

            '        'If query return some record then assign that if to detailid variable
            '        DetailId = dt1.Rows(0).Item("DetailId")
            '    Else

            '        'Check COA_DETAIL Table there exists any Record Regard that  Detail Title
            '        Dim Str11 As String
            '        Str11 = "select coa_detail_id from vwCOADetail where detail_title = " & Val(grd.GetRows(i).Cells(GridColumns.SUB_SERVICE).Value.ToString)
            '        Dim dt11 As DataTable
            '        dt11 = GetDataTable(Str11)

            '        'Checking if DataTable having anything to show against that query.
            '        If dt11.Rows.Count > 0 Then

            '            'If Record exists in COA_Detail_Table then get its Id and assign to variable
            '            DetailId = dt11.Rows(0).Item("coa_detail_id")

            '            'Insert Query to add same id and title in Mapping Table
            '            objCommand.CommandText = "INSERT INTO [SubServiceMapping]([SubService_Name], [DetailId] " & _
            '                     "VALUES( N'" & Val(grd.GetRows(i).Cells(GridColumns.SUB_SERVICE).Value.ToString) & "', " & DetailId & ")"
            '            objCommand.ExecuteNonQuery()

            '            'If Coa_Detail_Id not Exists in COA_Detail_Table
            '        Else

            '            'To Insert Detail Values in Table we have to know about its SUB_SUB_ID
            '            Dim str2 As String
            '            str2 = "select SubSubId from ServiceMapping where Service_Name = " & Val(grd.GetRows(i).Cells(GridColumns.SERVICE).Value.ToString)
            '            Dim dt2 As DataTable
            '            dt2 = GetDataTable(str2)
            '            If dt2 IsNot Nothing Then
            '                'If SUb_SUb_Id is received from ServiceMapping Table Then Assign it to a variable on which we can enter a row of detail
            '                SubSubId = dt2.Rows(0).Item("SubSubId")
            '                'Inserting data in COA_DEtail_Table
            '                objCommand.CommandText = "INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title], Active) " & _
            '                            "VALUES( " & SubSubId & ", NULL, N'" & Val(grd.GetRows(i).Cells(GridColumns.SUB_SERVICE).Value.ToString) & "'," & 1 & ")SELECT @@Identity"
            '                Dim detailidmap As Integer = objCommand.ExecuteScalar()
            '                'Inserting Data in SubServiceMapping Table
            '                objCommand.CommandText = "INSERT INTO [SubServiceMapping]([SubService_Name], [DetailId] " & _
            '                     "VALUES( N'" & Val(grd.GetRows(i).Cells(GridColumns.SUB_SERVICE).Value.ToString) & "', " & detailidmap & ")"
            '                objCommand.ExecuteNonQuery()
            '            Else

            '                'If there is no record on Service mapping related to selected service title
            '                Dim str21 As String
            '                str21 = "select main_sub_sub_id from vwCOADetail where Sub_Sub_Title = " & Val(grd.GetRows(i).Cells(GridColumns.SERVICE).Value.ToString)
            '                Dim dt21 As DataTable
            '                dt21 = GetDataTable(str21)
            '                If dt21 IsNot Nothing Then
            '                    SubSubId = dt21.Rows(0).Item("main_sub_sub_id")
            '                    objCommand.CommandText = "INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title], Active) " & _
            '                                "VALUES( " & SubSubId & ", NULL, N'" & Val(grd.GetRows(i).Cells(GridColumns.SUB_SERVICE).Value.ToString) & "'," & 1 & ")SELECT @@Identity"
            '                    Dim detailidmap1 As Integer = objCommand.ExecuteNonQuery()
            '                    objCommand.CommandText = "INSERT INTO [SubServiceMapping]([SubService_Name], [DetailId] " & _
            '                     "VALUES( N'" & Val(grd.GetRows(i).Cells(GridColumns.SUB_SERVICE).Value.ToString) & "', " & detailidmap1 & ")"
            '                    objCommand.ExecuteNonQuery()

            '                Else
            '                    'checking if this CategoryName Exists in CategoryMapping
            '                    Dim str3 As String
            '                    str3 = "select SubId from CategoryMapping where Category_Name = " & Val(grd.GetRows(i).Cells(GridColumns.CATEGORY).Value.ToString)
            '                    Dim dt3 As DataTable
            '                    dt3 = GetDataTable(str3)
            '                    If dt3 IsNot Nothing Then
            '                        SubId = dt3.Rows(0).Item("SubId")
            '                        'If exists in CategoryMapping Then Insert it into SubSubMain Table && into ServiceMapping Table
            '                        objCommand.CommandText = "INSERT INTO [tblCOAMainSubSub]([main_sub_id], [sub_sub_code], [sub_sub_title]) " & _
            '                                                    "VALUES( " & SubId & ", NULL, N'" & Val(grd.GetRows(i).Cells(GridColumns.SERVICE).Value.ToString) & "')"

            '                        Dim TempSubSubId As Integer = objCommand.ExecuteScalar()
            '                        'Execute Upper Query and get it in a variable to insert same id in ServiceMapping
            '                        objCommand.CommandText = "INSERT INTO [ServiceMapping]([Service_Name], [SubSubId] " & _
            '                                                    "VALUES( N'" & Val(grd.GetRows(i).Cells(GridColumns.SERVICE).Value.ToString) & "', " & TempSubSubId & ")"

            '                        objCommand.ExecuteNonQuery()
            '                        ''After Inserting values in SubSubMain Table we have to insert detail id against that subsubid
            '                        objCommand.CommandText = "INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title], Active) " & _
            '                                                    "VALUES( " & TempSubSubId & ", NULL, N'" & Val(grd.GetRows(i).Cells(GridColumns.SUB_SERVICE).Value.ToString) & "'," & 1 & ")SELECT @@Identity"

            '                        Dim detail_Id As Integer = objCommand.ExecuteNonQuery()
            '                        'Insertion in SubServiceMapping table
            '                        objCommand.CommandText = "INSERT INTO [SubServiceMapping]([SubService_Name], [DetailId] " & _
            '                                                "VALUES( N'" & Val(grd.GetRows(i).Cells(GridColumns.SUB_SERVICE).Value.ToString) & "', " & detail_Id & ")"

            '                        objCommand.ExecuteNonQuery()

            '                    Else
            '                        Dim str31 As String
            '                        str31 = "select main_sub_id from vwCOADetail where Sub_Title = " & Val(grd.GetRows(i).Cells(GridColumns.CATEGORY).Value.ToString)
            '                        Dim dt31 As DataTable
            '                        dt31 = GetDataTable(str31)
            '                        If dt31 IsNot Nothing Then
            '                            SubId = dt31.Rows(0).Item("main_sub_id")
            '                            objCommand.CommandText = "INSERT INTO [tblCOAMainSubSub]([main_sub_id], [sub_sub_code], [sub_sub_title]) " & _
            '                             "VALUES( " & SubId & ", NULL, N'" & Val(grd.GetRows(i).Cells(GridColumns.SERVICE).Value.ToString) & "')"

            '                            Dim TempSubSubId As Integer = objCommand.ExecuteScalar()

            '                            objCommand.CommandText = "INSERT INTO [ServiceMapping]([Service_Name], [SubSubId] " & _
            '                                                    "VALUES( N'" & Val(grd.GetRows(i).Cells(GridColumns.SERVICE).Value.ToString) & "', " & TempSubSubId & ")"

            '                            objCommand.ExecuteNonQuery()

            '                            objCommand.CommandText = "INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title], Active) " & _
            '                                    "VALUES( " & TempSubSubId & ", NULL, N'" & Val(grd.GetRows(i).Cells(GridColumns.SUB_SERVICE).Value.ToString) & "'," & 1 & ")SELECT @@Identity"

            '                            Dim detail_Id As Integer = objCommand.ExecuteNonQuery()

            '                            objCommand.CommandText = "INSERT INTO [SubServiceMapping]([SubService_Name], [DetailId] " & _
            '                                                    "VALUES( N'" & Val(grd.GetRows(i).Cells(GridColumns.SUB_SERVICE).Value.ToString) & "', " & detail_Id & ")"

            '                            objCommand.ExecuteNonQuery()
            '                        Else
            '                            ShowErrorMessage("Account " & Val(grd.GetRows(i).Cells(GridColumns.CATEGORY).Value.ToString) & " Not Exists")
            '                        End If
            '                    End If
            '                End If
            '            End If
            '        End If
            '    End If
            '    Dim str4 As String
            '    str4 = "Select tblCurrency.currency_id as currency_id,  IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId where tblCurrency.currency_code = N'" & Val(grd.GetRows(i).Cells(GridColumns.CURRENCY).Value.ToString) & "'"
            '    Dim dt4 As DataTable
            '    dt4 = GetDataTable(str4)
            '    If dt4 IsNot Nothing Then
            '        CurrencyId = dt4.Rows(0).Item("currency_id")
            '        CurrencyRate = dt4.Rows(0).Item("CurrencyRate")
            '    Else
            '        ShowErrorMessage("This Currency does not exists")
            '    End If
            '    objCommand.CommandText = ""
            '    'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '    '                       & " cheque_no, cheque_date,Source,voucher_code, Employee_Id, Remarks, UserName)" _
            '    '                       & " VALUES(0, 1,  7 , N'" & VoucherNo & "', N'" & grd.GetRows(i).Cells(GridColumns.TRANS_DATE).Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '    '                       & " NULL, NULL,N'" & Me.Name & "',N'" & VoucherNo & "', NULL, N'" & Me.txtRemarks.Text.Replace("'", "''") & "', '" & LoginUserName & "')" _
            '    '                       & " SELECT @@IDENTITY"
            '    'lngVoucherMasterId = objCommand.ExecuteScalar


            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, Currency_debit_amount, Currency_Credit_Amount, CurrencyId, CurrencyRate, Currrency_Symbol) " _
            '                            & " VALUES(" & lngVoucherMasterId & ", " & cmbCompany.SelectedValue & ", " & CustomerId & ", " & Val(grd.GetRows(i).Cells(GridColumns.AMOUNT).Value.ToString) * CurrencyRate & ", 0," & Val(grd.GetRows(i).Cells(GridColumns.AMOUNT).Value.ToString) & ", 0, " & CurrencyId & ", " & CurrencyRate & "," & Val(grd.GetRows(i).Cells(GridColumns.CURRENCY).Value.ToString) & ")"
            '    objCommand.ExecuteNonQuery()
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, Currency_debit_amount, Currency_Credit_Amount, CurrencyId, CurrencyRate, Currrency_Symbol) " _
            '                                              & " VALUES(" & lngVoucherMasterId & ", " & cmbCompany.SelectedValue & ", " & DetailId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells(GridColumns.AMOUNT).Value.ToString) * CurrencyRate & ", 0 , " & Val(grd.GetRows(i).Cells(GridColumns.AMOUNT).Value.ToString) & ", " & CurrencyId & ", " & CurrencyRate & "," & Val(grd.GetRows(i).Cells(GridColumns.CURRENCY).Value.ToString) & ")"
            '    objCommand.ExecuteNonQuery()
            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
            '    '& " VALUES(" & lngVoucherMasterId & ", " & 0 & ", " & CustomerId & ", " & Val(grd.GetRows(i).Cells(GridColumns.AMOUNT).Value.ToString) & ", 0, N'" & CName & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
            '    'objCommand.ExecuteNonQuery()
            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
            '    '                          & " VALUES(" & lngVoucherMasterId & ", " & 0 & ", " & DetailId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells(grdDetail.Rate).Value.ToString) & ",'" & txtRemarks.Text & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
            '    'objCommand.ExecuteNonQuery()
            'Next
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

   
End Class


