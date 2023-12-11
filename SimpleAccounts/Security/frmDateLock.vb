'22-Jan-2014  Task:2391       Imran Ali              Avrage Rate Re-Calculation in ERP
'25-Jun-2015 Task#125062015     Ahmad Sharif    Add Radio buttons for fixed and relevant date lock, set Properties,show all records

Imports System
Imports System.Data
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports CrystalDecisions.Shared
Public Class frmDateLock
    Implements IGeneral
    Dim DateLock As DateLockBE
    Dim _DateLockId As Integer = 0I
    'Dim MyFromDate, MyToDate As DateTime


    'Dim dt As DataTable
    'Dim IsFormOpend As Boolean = False
    'Dim IsEditMode As Boolean = False
    'Dim ExistingBalance As Double = 0D
    'Dim Mode As String = "Normal"
    '' Dim Spech As New SpeechSynthesizer

    'Dim InvId As Integer = 0
    'Dim SelectCategory As Integer
    'Dim SelectBarcodes As Integer
    'Dim SelectVendor As Integer
    'Dim SelectSaleMan As Integer
    'Dim SelectCompany As Integer
    'Dim SelectTrans As Integer
    'Dim CostId As Integer
    'Dim FuelExpAccount As Integer
    'Dim AdjustmentExpAccount As Integer
    'Dim OtherExpAccount As Integer
    'Dim EditCustomerListOnSale As String
    'Dim StockMaster As StockMaster
    'Dim StockDetail As StockDetail
    'Dim VNo As String = String.Empty
    'Dim ExistingVoucherFlg As Boolean = False
    'Dim VoucherId As Integer = 0
    'Dim Email As Email
    'Dim TradePrice As Double = 0
    'Dim SalesTax_Percentage As Double = 0
    'Dim SchemeQty As Double = 0
    'Dim Discount_Percentage As Double = 0
    'Dim Freight As Double = 0
    'Dim MarketReturns As Double = 0D
    'Dim IsSalesOrderAnalysis As Boolean = False
    'Dim SourceFile As String = String.Empty
    'Dim FileName As String = String.Empty
    'Dim setVoucherNo As String = String.Empty
    'Dim crpt As New ReportDocument
    'Dim Total_Amount As Double = 0D
    'Dim setEditMode As Boolean = False
    'Dim getVoucher_Id As Integer = 0
    'Dim companyId As Integer = 0
    'Dim Previouse_Amount As Double = 0D
    ''Dim ServicesItem As String = 0D
    'Dim TransitInssuranceTax As Double = 0D
    'Dim WHTax As Double = 0D
    'Dim PrintLog As PrintLogBE
    'Dim CompanyBasePrefix As Boolean = False
    'Dim flgMultipleSalesOrder As Boolean = False
    'Dim flgLoadAllItems As Boolean = False
    'Dim DefaultTax As Double = 0D
    'Dim flgCompanyRights As Boolean = False
    'Dim LoadQty As Double = 0D
    'Public flgAutoInvoiceGenerate As Boolean = False
    'Public DeliveryChalanId As Integer = 0I
    'Dim Adjustment As Double = 0D
    'Dim flgCgsVoucher As Boolean = False
    'Dim flgPrintLog As Boolean = False
    'Dim flgLocationWiseItems As Boolean = False
    'Dim _RefDocId As Integer = 0I
    'Dim _RefDocNo As String = String.Empty
    'Dim StockList As List(Of StockDetail)
    'Dim flgExcludeTaxPrice As Boolean = False
    ' ''Task:2369 Declare Boolean Variables
    'Dim flgCommentCustomerFormat As Boolean = False
    'Dim flgCommentArticleFormat As Boolean = False
    'Dim flgCommentArticleSizeFormat As Boolean = False
    'Dim flgCommentArticleColorFormat As Boolean = False
    'Dim flgCommentQtyFormat As Boolean = False
    'Dim flgCommentPriceFormat As Boolean = False
    'Dim flgCommentRemarksFormat As Boolean = False
    ''End Task:2369
    'Dim CreditSales As Boolean = True 'Task:2380 Added Flag Credit Sales
    'Dim dtpPODate As DateTime
    'Dim strRemarks As String = String.Empty
    'Dim VendorName As String = String.Empty
    'Enum EnumGridDetail
    '    'Category
    '    LocationID
    '    ArticleCode
    '    Item
    '    Size
    '    Color
    '    Unit
    '    Qty
    '    'ServiceQty
    '    Price
    '    Total
    '    GroupID
    '    ArticleID
    '    PackQty
    '    CurrentPrice
    '    PackPrice
    '    SaleDetailID
    '    BatchID
    '    'LocationID
    '    TradePrice
    '    Tax
    '    TaxAmount 'Task:2374 Added Index
    '    SED
    '    TotalAmount 'Task:2374 Added Index
    '    SavedQty
    '    SampleQty
    '    Discount_Percentage
    '    Freight
    '    MarketReturns
    '    SO_ID
    '    BatchNo
    '    LoadQty
    '    PurchasePrice
    '    NetBill
    '    Comments
    '    Pack_Desc
    '    AccountId
    '    SalesTax
    '    SalesExcTax
    '    SEDTax
    '    DeleteButton
    'End Enum
    'Enum enmMaster
    '    SalesId
    '    LocationId
    '    SalesNo
    '    SalesDate
    '    SalesTime
    '    CustomerCode
    '    EmployeeCode
    '    POId
    '    SalesQty
    '    SalesAmount
    '    CashPaid
    '    Balance
    '    Remarks
    '    UserName
    '    TransporterId
    '    BiltyNo
    '    PreviousBalance
    '    InvoiceDiscount
    '    FuelExpense
    '    OtherExpense
    '    Adjustment
    '    CostCenterId
    '    Post
    '    ServiceItemSale
    '    DeliveryDate
    '    Delivered
    '    TransitInsurance
    '    DeliveryChalanId
    '    DcNo
    '    Adj_Flag
    '    Adj_Percentage
    '    RefDocId
    '    Detail_Title
    'End Enum
    Private Sub frmDateLock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If DateLockDAL.getDateLockId(Me.dtpDateLock.Value) > 0 Then
                Me.btnDateUnLock.Enabled = True
            Else
                Me.btnDateUnLock.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New DateLockDAL().Delete(DateLock) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            DateLock = New DateLockBE
            DateLock.DateLockId = _DateLockId 'DateLockDAL.getDateLockId(Me.dtpDateLock.Value)
            DateLock.DateLock = Me.dtpDateLock.Value
            DateLock.Lock = IIf(Condition = "UnLock", False, True)
            DateLock.EntryDate = Date.Now
            DateLock.Username = LoginUserName
            'Task#125062015 Set Properties of _DateLockType and _NoOfDays
            If rbtnFixed.Checked = True Then    'checking radio button selection & set the property 
                DateLock.DateLockType = Me.rbtnFixed.Text
            Else
                DateLock.DateLockType = Me.rbtnRelevant.Text
            End If

            DateLock.NoOfDays = Me.txtNUpDown.Value
            'End Task#125062015


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Task#125062015 binding the all the record in grid (grdHistory)
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Try

            Dim dt As New DataTable
            dt = GetDataTable("select DateLockId,Username,DateLock_Type,NoOfDays,DateLock,Lock from tblDateLock Order By DateLockId DESC")   'Passing the query to global function GetDataTable()
            dt.AcceptChanges()

            Me.grdHistory.DataSource = dt       'setting dt (datatable) to grdHistory as datasource
            Me.grdHistory.RetrieveStructure()   'setting grdHistory to RetrieveStructure() for customizing grdHistory


            Me.grdHistory.RootTable.Columns("DateLockId").Visible = False   'hide the column from grdHistory
            'Displaying the columns of grdHistory
            Me.grdHistory.RootTable.Columns("Username").Visible = True
            Me.grdHistory.RootTable.Columns("DateLock_Type").Visible = True
            Me.grdHistory.RootTable.Columns("NoOfDays").Visible = True
            Me.grdHistory.RootTable.Columns("DateLock").Visible = True
            Me.grdHistory.RootTable.Columns("Lock").Visible = True

            'changing the captions of the headers in grdHistory
            Me.grdHistory.RootTable.Columns("DateLock").Caption = "Date"
            Me.grdHistory.RootTable.Columns("DateLock_Type").Caption = "Type"
            Me.grdHistory.RootTable.Columns("NoOfDays").Caption = "Days"
            Me.grdHistory.RootTable.Columns("DateLock").FormatString = "dd/MMM/yyyy"

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    'Task#125062015
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            _DateLockId = 0I
            Me.rbtnFixed.Checked = True
            'Me.dtpDateLock.Enabled = True
            Me.txtNUpDown.Value = 0
            'Me.btnDateUnLock.Enabled = False
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            FillModel()
            If New DateLockDAL().Add(DateLock) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            FillModel()
            If New DateLockDAL().Update(DateLock) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    
    
    Private Sub dtpDateLock_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDateLock.ValueChanged
        Try
            If DateLockDAL.getDateLockId(Me.dtpDateLock.Value) > 0 Then
                Me.btnDateUnLock.Enabled = True
            Else
                Me.btnDateUnLock.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try

            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Date.Now.ToString("yyyy-M-d 00:00:00") Then
                If DateLock.Lock = True Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Private Sub bgwDateLock_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Try

            'Dim dateLock As New SBModel.DateLockBE
            'dateLock = SBDal.DateLockDAL.GetAllDateLock.Find(AddressOf chkDateLock)
            'If dateLock IsNot Nothing Then
            '    If dateLock.DateLock.ToString.Length > 0 Then
            '        flgDateLock = True
            '    Else
            '        flgDateLock = False
            '    End If
            'Else
            '    flgDateLock = False
            'End If
            Dim strQry As String = "Select Config_Value, Isnull(IsActive,0) as IsActive From ConfigValuesTable WHERE Config_Type='DateLock'"
            Dim dt As New DataTable
            dt = GetDataTable(strQry)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)) Then
                        MyDateLock = Date.Now
                        flgDateLock = False
                    Else
                        MyDateLock = dt.Rows(0).Item(0)
                        flgDateLock = Convert.ToBoolean(dt.Rows(0).Item(1).ToString)
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    'Task:2391 Avrage Rate Re-Calculation in ERP
    'Public Sub ReGenerateAVGRate(ByVal FromDate As DateTime, ByVal ToDate As DateTime)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    Dim objTrans As OleDb.OleDbTransaction = Con.BeginTransaction
    '    Dim objCmd As New OleDb.OleDbCommand
    '    Try

    '        Dim strSQL As String = "Select ArticleId, (Isnull(Amount,0)/Isnull(Qty,0)) CurrentAvgRate From( " _
    '                      & " SELECT dbo.ArticleDefView.ArticleId, (ISNULL(Opening.Amount, 0) + ISNULL(Detail.Amount, 0)) Amount, (ISNULL(Opening.Qty, 0) + ISNULL(Detail.Qty, 0)) AS Qty " _
    '                      & " FROM  dbo.ArticleDefView LEFT OUTER JOIN " _
    '                      & " (SELECT dbo.StockDetailTable.ArticleDefId, SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) AS Qty,  " _
    '                      & " SUM(ISNULL(dbo.StockDetailTable.Rate, 0) * (ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0))) AS Amount " _
    '                      & " FROM dbo.StockMasterTable INNER JOIN " _
    '                      & " dbo.StockDetailTable ON dbo.StockMasterTable.StockTransId = dbo.StockDetailTable.StockTransId " _
    '                      & " WHERE(Convert(Varchar, dbo.StockMasterTable.DocDate, 102) < Convert(DateTime, '" & FromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " _
    '                      & " GROUP BY dbo.StockDetailTable.ArticleDefId) AS Opening ON Opening.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
    '                      & " (SELECT StockDetailTable_1.ArticleDefId, SUM(ISNULL(StockDetailTable_1.InQty, 0)) AS Qty,  " _
    '                      & " SUM(ISNULL(StockDetailTable_1.Rate, 0) * (ISNULL(StockDetailTable_1.InQty, 0))) AS Amount " _
    '                      & " FROM dbo.StockMasterTable AS StockMasterTable_1 INNER JOIN " _
    '                      & " dbo.StockDetailTable AS StockDetailTable_1 ON StockMasterTable_1.StockTransId = StockDetailTable_1.StockTransId " _
    '                      & " WHERE  (CONVERT(Varchar, StockMasterTable_1.DocDate, 102) BETWEEN CONVERT(DateTime, '" & FromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(DateTime, '" & ToDate.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
    '                      & " GROUP BY StockDetailTable_1.ArticleDefId) AS Detail ON Detail.ArticleDefId = dbo.ArticleDefView.ArticleId " _
    '                      & " ) c where (c.Amount <> 0 Or c.Qty <> 0) "

    '        Dim objAvgRate As DataTable = GetDataTable(strSQL, objTrans)
    '        If objAvgRate IsNot Nothing Then
    '            objCmd.Connection = Con
    '            objCmd.Transaction = objTrans

    '            For Each objRow As DataRow In objAvgRate.Rows

    '                objCmd.CommandText = ""
    '                objCmd.CommandType = CommandType.Text
    '                ''Update Voucher Sales And Store Issuence 
    '                objCmd.CommandText = "Update tblVoucherDetail Set credit_Amount =" & Val(objRow(1).ToString) & " WHERE ArticleDefId=" & Val(objRow(0).ToString) & " And coa_detail_id in (Select Isnull(SubSubId,0) as AccountId From ArticleGroupDefTable WHERE Isnull(SubSubId,0) <> 0) And Voucher_Id IN(Select Voucher_Id From tblVoucher WHERE (Convert(Varchar, Voucher_date, 102) BETWEEN Convert(DateTime, '" & FromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND  Convert(DateTime, '" & ToDate.ToString("yyyy-M-d 23:59:59") & "', 102))) AND credit_Amount <> 0 "
    '                objCmd.ExecuteNonQuery()

    '                objCmd.CommandText = "Update tblVoucherDetail Set debit_Amount =" & Val(objRow(1).ToString) & " WHERE ArticleDefId=" & Val(objRow(0).ToString) & " And coa_detail_id in (Select Isnull(Config_Value,0) as Config_Value From ConfigValuesTable WHERE Config_Type ='CGSAccountId') And Voucher_Id IN(Select Voucher_Id From tblVoucher WHERE (Convert(Varchar, Voucher_date, 102) BETWEEN Convert(DateTime, '" & FromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND  Convert(DateTime, '" & ToDate.ToString("yyyy-M-d 23:59:59") & "', 102))) AND debit_Amount <> 0 "
    '                objCmd.ExecuteNonQuery()
    '                ''End 


    '                ''Update Voucher Sales Return
    '                objCmd.CommandText = "Update tblVoucherDetail Set debit_Amount =" & Val(objRow(1).ToString) & " WHERE ArticleDefId=" & Val(objRow(0).ToString) & " And coa_detail_id in (Select Isnull(SubSubId,0) as AccountId From ArticleGroupDefTable WHERE Isnull(SubSubId,0) <> 0) And Voucher_Id IN(Select Voucher_Id From tblVoucher WHERE (Convert(Varchar, Voucher_date, 102) BETWEEN Convert(DateTime, '" & FromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND  Convert(DateTime, '" & ToDate.ToString("yyyy-M-d 23:59:59") & "', 102))) AND debit_Amount <> 0 "
    '                objCmd.ExecuteNonQuery()

    '                objCmd.CommandText = "Update tblVoucherDetail Set credit_Amount =" & Val(objRow(1).ToString) & " WHERE ArticleDefId=" & Val(objRow(0).ToString) & " And coa_detail_id in (Select Isnull(Config_Value,0) as Config_Value From ConfigValuesTable WHERE Config_Type ='CGSAccountId') And Voucher_Id IN(Select Voucher_Id From tblVoucher WHERE (Convert(Varchar, Voucher_date, 102) BETWEEN Convert(DateTime, '" & FromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND  Convert(DateTime, '" & ToDate.ToString("yyyy-M-d 23:59:59") & "', 102))) AND credit_Amount <> 0 "
    '                objCmd.ExecuteNonQuery()
    '                'End


    '                'Update Stock 
    '                objCmd.CommandText = "Update StockDetailTable Set Rate =" & Val(objRow(1).ToString) & ", InAmount=InQty*" & Val(objRow(1).ToString) & ", OutAmount=OutQty*" & Val(objRow(1).ToString) & "   WHERE ArticleDefId=" & Val(objRow(0).ToString) & " And StockTransId IN (Select StockTransId From StockMasterTable WHERE (Convert(varchar, DocDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpDateLock.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & ToDate.ToString("yyyy-M-d 23:59:59") & "', 102)) AND (Left(DocNo,2)='SI' Or Left(DocNo,1)='I' Or Left(DocNo,2)='SR')  AND Left(DocNo,3) <> 'SRN') "
    '                objCmd.ExecuteNonQuery()
    '                'End 

    '            Next
    '            objTrans.Commit()
    '        End If


    '    Catch ex As Exception
    '        objTrans.Rollback()
    '        Throw ex
    '    Finally
    '        Con.Close()
    '    End Try
    'End Sub
    'Task:2391 Avrage Rate Re-Calculation in ERP
    'Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
    '    Try
    '        ReGenerateAVGRate(MyFromDate, MyToDate)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
    '    Try
    '        'ShowInformationMessage("")
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    ''22-Jan-2014  Task:2391       Imran Ali              Avrage Rate Re-Calculation in ERP
    'Private Function Update_Record() As Boolean
    '    Dim objCommand As New OleDbCommand
    '    Dim objCon As New OleDbConnection
    '    Dim trans As OleDbTransaction = Nothing


    '    Try



    '        Dim VendorId As Integer
    '        Dim InvAccountId As Integer = Val(getConfigValueByType("InvAccountId").ToString) 'GetConfigValue("InvAccountId") 'Inventory Account
    '        Dim CgsAccountId As Integer = Val(getConfigValueByType("CGSAccountId").ToString) 'GetConfigValue("CGSAccountId") 'Cost Of Good Sold Account
    '        Dim AccountId As Integer = Val(getConfigValueByType("SalesCreditAccount").ToString) 'GetConfigValue("SalesCreditAccount")
    '        Dim SalesTaxId As Integer = Val(getConfigValueByType("SalesTaxCreditAccount").ToString) 'GetConfigValue("SalesTaxCreditAccount")
    '        Dim SEDAccountId As Integer = Val(getConfigValueByType("SEDAccountId").ToString) 'Val(GetConfigValue("SEDAccountId").ToString)
    '        Dim InsuranceAccountId As Integer = Val(getConfigValueByType("TransitInsuranceAccountId").ToString) 'Val(GetConfigValue("TransitInsuranceAccountId").ToString)
    '        FuelExpAccount = Val(getConfigValueByType("FuelExpAccount").ToString)
    '        AdjustmentExpAccount = Val(getConfigValueByType("AdjustmentExpAccount").ToString)
    '        OtherExpAccount = Val(getConfigValueByType("OtherExpAccount").ToString)
    '        Dim IsDiscountVoucher As Boolean = Convert.ToBoolean(getConfigValueByType("DiscountVoucherOnSale").ToString) 'Convert.ToBoolean(GetConfigValue("DiscountVoucherOnSale").ToString)
    '        Dim SalesDiscountAccount As Integer = Val(getConfigValueByType("SalesDiscountAccount").ToString) 'Val(GetConfigValue("SalesDiscountAccount").ToString)
    '        Dim GLAccountArticleDepartment As Boolean
    '        If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
    '            GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
    '        Else
    '            GLAccountArticleDepartment = False
    '        End If
    '        Dim lngVoucherMasterId As Integer = 0I
    '        'Validtion on Configuration Account Id 
    '        '25-9-2013 by imran ali....

    '        If flgCgsVoucher = True Then
    '            If InvAccountId <= 0 Then
    '                ShowErrorMessage("Purchase account is not map.")
    '                Me.dtpDateLock.Focus()
    '                Return False
    '            ElseIf CgsAccountId <= 0 Then
    '                ShowErrorMessage("Cost of good sold account is not map.")
    '                Me.dtpDateLock.Focus()
    '                Return False
    '            End If
    '        End If
    '        If AccountId <= 0 Then
    '            ShowErrorMessage("Sales account is not map.")
    '            Me.dtpDateLock.Focus()
    '            Return False
    '        End If

    '        Dim ReceiptVoucherFlg As String = Convert.ToString(getConfigValueByType("ReceiptVoucherOnSales").ToString) 'GetConfigValue("ReceiptVoucherOnSales").ToString
    '        Dim VoucherNo As String = GetVoucherNo()
    '        Dim DiscountedPrice As Double = 0
    '        Dim CurrentBalance As Double = CDbl(GetAccountBalance(VendorId)) - Me.ExistingBalance
    '        Dim ExpenseChargeToCustomer As Boolean
    '        'If Not GetConfigValue("ExpenseChargeToCustomer").ToString = "Error" Then
    '        ExpenseChargeToCustomer = Convert.ToBoolean(getConfigValueByType("ExpenseChargeToCustomer").ToString) 'Convert.ToBoolean(GetConfigValue("ExpenseChargeToCustomer").ToString)
    '        Dim flgExcludeTaxPrice As Boolean = Convert.ToBoolean(getConfigValueByType("ExcludeTaxPrice").ToString)

    '        Dim strQuery As String = "Select StockTransId, DocNo, DocDate From StockMasterTable WHERE LEFT(DocNo,2)='SI' AND (Convert(Varchar, DocDate, 102) BETWEEN Convert(DateTime, '" & MyFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & MyToDate.ToString("yyyy-M-d 23:59:59") & "', 102)) ORDER BY (Convert(varchar, DocDate, 102)) ASC"
    '        Dim dtStock As New DataTable
    '        dtStock = GetDataTable(strQuery)

    '        Dim strSQL As String = "Select Isnull(SalesId,0) as SalesId, Isnull(LocationId,0) as LocationId, SalesNo, SalesDate, SalesTime, Isnull(CustomerCode,0) as CustomerCode, Isnull(EmployeeCode,0) as EmployeeCode, Isnull(POId,0) as POId, Isnull(SalesQty,0) as SalesQty, Isnull(SalesAmount,0) as SalesAmount, Isnull(CashPaid,0) as CashPaid, Isnull(Balance,0) as Balane, Remarks, UserName, Isnull(TransporterId,0) as TransporterId, BiltyNo, Isnull(PreviousBalance,0) as PreviousBalance, IsNull(InvoiceDiscount,0) as InvoiceDiscount, Isnull(FuelExpense,0) as FuelExpense, Isnull(OtherExpense,0) as OtherExpense, Isnull(Adjustment,0) as Adjustment, Isnull(CostCenterId,0) as CostCenterId, Isnull(Post,0) as Post, Isnull(ServiceItemSale,0) as ServiceItemSale, DeliveryDate, Isnull(Delivered,0) as Delivered, Isnull(TransitInsurance,0) as TransitIncurance, Isnull(DeliveryChalanId,0) as DeliveryChalanId, DcNo, Isnull(Adj_Flag,0) as Adj_Flag, Isnull(Adj_Percentage,0) as Adj_Percentage, IsNull(RefDocId,0) as RefDocId, vw.detail_title From SalesMasterTable INNER JOIN vwCOADetail vw On vw.coa_detail_id = SalesMasterTable.CustomerCode WHERE (Convert(Varchar, SalesDate, 102) BETWEEN Convert(DateTime, '" & MyFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & MyToDate.ToString("yyyy-M-d 23:59:59") & "', 102)) ORDER BY (Convert(varchar,SalesDate,102)) ASC"
    '        Dim dtInv As DataTable = GetDataTable(strSQL)
    '        dtInv.TableName = "Invoice"
    '        If dtInv IsNot Nothing Then
    '            For Each objMasterRow As DataRow In dtInv.Rows

    '                objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
    '                If objCon.State = ConnectionState.Open Then objCon.Close()
    '                objCon.Open()

    '                Dim cmd As New OleDbCommand
    '                cmd.Connection = objCon
    '                trans = objCon.BeginTransaction

    '                objCommand.Connection = objCon
    '                objCommand.Transaction = trans
    '                objCommand.CommandType = CommandType.Text

    '                strRemarks = objMasterRow(enmMaster.Remarks).ToString
    '                VendorName = objMasterRow(enmMaster.Detail_Title).ToString
    '                Dim transId As Integer = 0
    '                If GetFilterDataFromDataTable(dtStock, "[DocNo]='" & objMasterRow.Item(enmMaster.SalesNo).ToString & "'").ToTable("StockTrans").Rows.Count < 1 Then
    '                    transId = 0
    '                Else
    '                    transId = GetFilterDataFromDataTable(dtStock, "[DocNo]='" & objMasterRow.Item(enmMaster.SalesNo).ToString & "'").ToTable("StockTrans").Rows(0).Item("StockTransId").ToString()
    '                End If

    '                transId = transId
    '                StockMaster = New StockMaster
    '                StockMaster.StockTransId = transId
    '                StockMaster.DocNo = objMasterRow.Item(enmMaster.SalesNo).ToString.Replace("'", "''")
    '                StockMaster.DocDate = objMasterRow.Item(enmMaster.SalesDate)
    '                StockMaster.DocType = 3 'Convert.ToInt32(GetStockDocTypeId("Sales").ToString)
    '                StockMaster.Remaks = objMasterRow.Item(enmMaster.Remarks).ToString.Replace("'", "''")
    '                StockMaster.Project = Val(objMasterRow.Item(enmMaster.CostCenterId).ToString)



    '                strSQL = String.Empty
    '                strSQL = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, ArticleSizeDefTable.ArticleSizeName AS Size, ArticleColorDefTable.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
    '                                      & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price) * Article.PackQty) END AS Total, " _
    '                                      & " Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty], ISNULL(Recv_D.CurrentPrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice,  Recv_D.SaleDetailId, Recv_D.BatchID, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.TaxPercent,0) as Tax, Convert(float,0) as [Tax Amount], ISNULL(Recv_D.SEDPercent,0) As SED, Convert(float,0) as [Total Amount],0 as savedqty , SampleQty as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(So_ID,0) as So_Id, isnull(Recv_D.BatchNo,'') as [Batch No], isnull(Recv_D.LoadQty,0) as LoadQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0 as NetBill, Recv_D.Comments, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(ArticleGroupDefTable.SubSubId,0) as SalesAccountId,  ((Isnull(TaxPercent,0)/100) * (Isnull(Qty,0) * Isnull(Price,0))) SalesTax, (((Isnull(Qty,0) * Isnull(Price,0))/(Isnull(TaxPercent,0)+100))*Isnull(TaxPercent,0)) SalesExcTax, ((Isnull(SEDPercent,0)/100) * (Isnull(Qty,0) * Isnull(Price,0))) SEDTax    FROM SalesDetailTable Recv_D INNER JOIN " _
    '                                      & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId inner JOIN " _
    '                                      & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id inner join " _
    '                                      & " articledeftable on articledeftable.articleid = recv_d.articleDefId INNER JOIN " _
    '                                      & " ArticleSizeDefTable ON ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId INNER JOIN " _
    '                                      & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId LEFT OUTER JOIN ArticleGroupDefTable on ArticleGroupDefTable.ArticleGroupId = Article.ArticleGroupId " _
    '                                      & " Where Recv_D.SalesID =" & Val(objMasterRow.Item(enmMaster.SalesId).ToString) & " ORDER BY Recv_D.SaleDetailId Asc"
    '                Dim dtData As DataTable = GetDataTable(strSQL, trans)
    '                dtData.TableName = "Detail"
    '                If dtData IsNot Nothing Then

    '                    Dim dtVoucher As DataTable = GetDataTable("SELECT Isnull(Voucher_ID,0) as Voucher_Id FROM tblVoucher where source='frmSales' and voucher_code='" & objMasterRow.Item(enmMaster.SalesNo).ToString & "'", trans)
    '                    If dtVoucher IsNot Nothing Then
    '                        If dtVoucher.Rows.Count > 0 Then
    '                            lngVoucherMasterId = Val(dtVoucher.Rows(0).Item(0).ToString) 'GetVoucherId("frmSales", objMasterRow(enmMaster.SalesNo).ToString, trans)
    '                        Else
    '                            lngVoucherMasterId = 0I
    '                        End If
    '                    End If

    '                    '***********************
    '                    'Deleting Detail
    '                    '***********************
    '                    objCommand.CommandText = ""
    '                    objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
    '                    objCommand.ExecuteNonQuery()

    '                    StockList = New List(Of StockDetail)
    '                    For Each grd As DataRow In dtData.Rows

    '                        Dim chkPost As Boolean = True
    '                        'Dim i As Integer
    '                        gobjLocationId = MyCompanyId
    '                        dtpPODate = objMasterRow.Item(enmMaster.SalesDate)
    '                        VendorId = objMasterRow.Item(enmMaster.CustomerCode)
    '                        'Val(objMasterRow(enmMaster.CostCenterId).ToString) = GetCostCenterId(Me.cmbCompany.SelectedValue)
    '                        'Dim ServiceItem As String = GetConfigValue("ServiceItem").ToString



    '                        'objCommand.CommandType = CommandType.Text
    '                        'objCommand.CommandText = "SELECT ISNULL(Qty,0) as Qty, ISNULL(SampleQty,0) as SampleQty, ArticleDefID, ISNULL(SO_ID,0) as SO_ID FROM SalesDetailTable WHERE  SalesId = " & Val(objMasterRow(enmMaster.SalesId).ToString) & " AND ISNULL(Qty,0) <> 0"
    '                        'Dim da As New OleDbDataAdapter(objCommand)
    '                        'Dim dtSavedItems As New DataTable
    '                        'da.Fill(dtSavedItems)









    '                        ''==========================================

    '                        'objCommand.CommandText = ""
    '                        'objCommand.CommandText = "Delete from SalesDetailTable where SalesID = " & Val(objMasterRow(enmMaster.SalesId).ToString)
    '                        'objCommand.ExecuteNonQuery()

    '                        ''==========================================


    '                        ''==========================================

    '                        ''make reversal of saved items in sale order detail table against poid
    '                        'If dtSavedItems.Rows.Count > 0 Then
    '                        '    If flgMultipleSalesOrder = False Then
    '                        '        If Not Me.cmbPo.SelectedIndex = -1 Then
    '                        '            For Each r As DataRow In dtSavedItems.Rows
    '                        '                objCommand.CommandText = "Update SalesOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & "), DeliveredSchemeQty=abs(Isnull(DeliveredSchemeQty,0) - " & r.Item(1) & ") where SalesOrderID = " & Me.cmbPo.SelectedValue & " and ArticleDefID = " & r.Item(2) & ""
    '                        '                objCommand.ExecuteNonQuery()
    '                        '            Next
    '                        '        End If
    '                        '    Else
    '                        '        For Each r As DataRow In dtSavedItems.Rows
    '                        '            objCommand.CommandText = "Update SalesOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & "), DeliveredSchemeQty=abs(Isnull(DeliveredSchemeQty,0) - " & r.Item(1) & ") where SalesOrderID = " & r("SO_ID") & " and ArticleDefID = " & r.Item(2) & ""
    '                        '            objCommand.ExecuteNonQuery()
    '                        '        Next
    '                        '    End If
    '                        'End If


    '                        'If dtSavedItems.Rows.Count > 0 Then
    '                        '    If DeliveryChalanId > 0 Then
    '                        '        For Each r As DataRow In dtSavedItems.Rows
    '                        '            objCommand.CommandText = "Update DeliveryChalanDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & "), DeliveredSchemeQty=abs(Isnull(DeliveredSchemeQty,0) - " & r.Item(1) & ") where DeliveryId = " & DeliveryChalanId & " and ArticleDefID = " & r.Item(2) & ""
    '                        '            objCommand.ExecuteNonQuery()
    '                        '        Next
    '                        '    End If
    '                        'End If


    '                        'objCommand.CommandText = ""


    '                        DiscountedPrice = Val(grd.Item(EnumGridDetail.CurrentPrice).ToString) - Val(grd.Item(EnumGridDetail.Price))

    '                        If GLAccountArticleDepartment = True Then
    '                            'Before against task:2390
    '                            'AccountId = Val(grd.Item("SalesAccountId").ToString)
    '                            'Task:2390 Change Inventory Account
    '                            InvAccountId = Val(grd.Item("SalesAccountId").ToString)
    '                            'End Task:2390
    '                        End If

    '                        Dim CostPrice As Double = 0D
    '                        Dim CrrStock As Double = 0D
    '                        If flgCgsVoucher = True Then
    '                            objCommand.CommandText = ""
    '                            objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(Convert(float, SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0)))) AS qty, ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))) as Amount  " _
    '                                                            & " FROM dbo.ArticleDefTable INNER JOIN " _
    '                                                            & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & " AND StockTransId in (Select StockTransId From StockMasterTable WHERE DocNo <> '" & objMasterRow(enmMaster.SalesNo).ToString.Replace("'", "''") & "')" _
    '                                                            & " GROUP BY dbo.StockDetailTable.ArticleDefId "
    '                            Dim dtCrrStock As New DataTable
    '                            Dim daCrrStock As New OleDbDataAdapter(objCommand)
    '                            daCrrStock.Fill(dtCrrStock)

    '                            If dtCrrStock IsNot Nothing Then
    '                                If dtCrrStock.Rows.Count > 0 Then
    '                                    If Val(grd.Item("rate").ToString) <> 0 Then
    '                                        CrrStock = dtCrrStock.Rows(0).Item(2)
    '                                        CostPrice = IIf(dtCrrStock.Rows(0).Item(3) + CrrStock = 0, 0, dtCrrStock.Rows(0).Item(3) / CrrStock)
    '                                    Else
    '                                        CostPrice = Val(grd.Item("PurchasePrice").ToString)
    '                                    End If
    '                                Else
    '                                    CostPrice = Val(grd.Item("PurchasePrice").ToString)
    '                                End If
    '                            End If
    '                        End If

    '                        If (Val(grd.Item("Qty").ToString.ToString) > 0 Or Val(grd.Item("Sample Qty").ToString.ToString) > 0) Then
    '                            StockDetail = New StockDetail
    '                            StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
    '                            StockDetail.LocationId = grd.Item("LocationId").ToString
    '                            StockDetail.ArticleDefId = grd.Item("ArticleDefId").ToString
    '                            StockDetail.InQty = 0
    '                            If IsSalesOrderAnalysis = True Then
    '                                StockDetail.OutQty = IIf(grd.Item("Unit").ToString = "Loose", Val(grd.Item("Qty").ToString) + Val(grd.Item("Sample Qty").ToString), ((Val(grd.Item("Qty").ToString) + Val(grd.Item("Sample Qty").ToString)) * Val(grd.Item("Pack Qty").ToString)))
    '                            Else
    '                                StockDetail.OutQty = IIf(grd.Item("Unit").ToString = "Loose", Val(grd.Item("Qty").ToString) + Val(grd.Item("Sample Qty").ToString), ((Val(grd.Item("Qty").ToString) + Val(grd.Item("Sample Qty").ToString)) * Val(grd.Item("Pack Qty").ToString)))
    '                            End If
    '                            StockDetail.Rate = IIf(CostPrice = 0, Val(grd.Item(EnumGridDetail.PurchasePrice).ToString), CostPrice)
    '                            StockDetail.InAmount = 0
    '                            StockDetail.OutAmount = IIf(grd.Item("Unit").ToString = "Loose", ((Val(grd.Item("Qty").ToString) + Val(grd.Item("Sample Qty").ToString)) * IIf(CostPrice = 0, Val(grd.Item(EnumGridDetail.PurchasePrice).ToString), CostPrice)), (((Val(grd.Item("Qty").ToString) + Val(grd.Item("Sample Qty").ToString)) * Val(grd.Item("Pack Qty").ToString)) * IIf(CostPrice = 0, Val(grd.Item(EnumGridDetail.PurchasePrice).ToString), CostPrice)))
    '                            StockDetail.Remarks = String.Empty
    '                            StockList.Add(StockDetail)
    '                        End If


    '                        'objCommand.CommandText = ""

    '                        'objCommand.CommandText = "Insert into SalesDetailTable (SalesId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID, LocationId, TaxPercent, SampleQty, SEDPercent,TradePrice, Discount_Percentage, Freight, MarketReturns,SO_ID,LoadQty, PurchasePrice, PackPrice, Comments,Pack_Desc) values( " _
    '                        '                     & " " & Val(objMasterRow(enmMaster.SalesId).ToString) & " ," & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ",'" & (grd.Item(EnumGridDetail.Unit).ToString) & "'," & Val(grd.Item(EnumGridDetail.Qty).ToString) & ", " _
    '                        '                     & " " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString))) & ", " _
    '                        '                     & Val(grd.Item(EnumGridDetail.Price).ToString) & ", " & Val(grd.Item(EnumGridDetail.PackQty).ToString) & " , " & Val(grd.Item(EnumGridDetail.CurrentPrice).ToString) & ",'" & grd.Item(EnumGridDetail.BatchNo).ToString.Replace("'", "''") & "', " _
    '                        '                     & grd.Item(EnumGridDetail.BatchID).ToString & " , " & grd.Item(EnumGridDetail.LocationID).ToString & ", " & IIf(grd.Item(EnumGridDetail.Tax).ToString = "", 0, grd.Item(EnumGridDetail.Tax).ToString) & ", " & Val(grd.Item(EnumGridDetail.SampleQty).ToString()) & ", " & Val(grd.Item(EnumGridDetail.SED).ToString) & ", " & Val(grd.Item(EnumGridDetail.TradePrice).ToString) & ", " & Val(grd.Item(EnumGridDetail.Discount_Percentage).ToString) & ", " & Val(grd.Item(EnumGridDetail.Freight).ToString) & "," & Val(grd.Item(EnumGridDetail.MarketReturns).ToString) & ", " & Val(grd.Item(EnumGridDetail.SO_ID).ToString) & ", " & Val(grd.Item(EnumGridDetail.LoadQty).ToString) & "," & Val(grd.Item("PurchasePrice").ToString) & ", " & Val(grd.Item(EnumGridDetail.PackPrice).ToString) & ", '" & grd.Item(EnumGridDetail.Comments).ToString.Replace("'", "''") & "', '" & grd.Item(EnumGridDetail.Pack_Desc).ToString.Replace("'", "''") & "') "
    '                        'objCommand.ExecuteNonQuery()


    '                        'Val(grd.Rows(i).Cells(5).ToString)
    '                        'Update SO and SO Detail
    '                        'If Val(grd.Item("Qty").ToString) <> 0 Or Val(grd.Item("Sample Qty").ToString) <> 0 Then
    '                        '    If flgMultipleSalesOrder = False Then
    '                        '        If Not Me.cmbPo.SelectedIndex = -1 Then
    '                        '            objCommand.CommandText = "UPDATE SalesOrderDetailTable " _
    '                        '                                                    & " SET              DeliveredQty = isnull(DeliveredQty,0) + " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString))) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(grd.Item(EnumGridDetail.SampleQty).ToString) & ") " _
    '                        '                                                   & " WHERE     (SalesOrderId = " & Me.cmbPo.SelectedValue & ") AND (ArticleDefId = " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                        '            objCommand.ExecuteNonQuery()

    '                        '            'Else
    '                        '        End If
    '                        '    Else
    '                        '        If Val(grd.Item(EnumGridDetail.SO_ID).ToString) > 0 Then
    '                        '            objCommand.CommandText = "UPDATE SalesOrderDetailTable " _
    '                        '                                                   & " SET              DeliveredQty = isnull(DeliveredQty,0) + " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString))) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(grd.Item(EnumGridDetail.SampleQty).ToString) & ") " _
    '                        '                                                  & " WHERE     (SalesOrderId = " & Val(grd.Item(EnumGridDetail.SO_ID).ToString) & ") AND (ArticleDefId = " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                        '            objCommand.ExecuteNonQuery()
    '                        '        End If
    '                        '    End If
    '                        '    If DeliveryChalanId > 0 Then
    '                        '        objCommand.CommandText = "UPDATE DeliveryChalanDetailTable " _
    '                        '                                                & " SET              DeliveredQty = isnull(DeliveredQty,0) + " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString))) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(grd.Item(EnumGridDetail.SampleQty).ToString) & ") " _
    '                        '                                               & " WHERE     (DeliveryId = " & DeliveryChalanId & ") AND (ArticleDefId = " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                        '        objCommand.ExecuteNonQuery()
    '                        '        'Else
    '                        '    End If
    '                        'End If

    '                        'If Not ServiceItem = "True" Then
    '                        If Val(grd.Item("Qty").ToString) <> 0 Or Val(grd.Item("Sample Qty").ToString) <> 0 Then
    '                            If IsDiscountVoucher = False Then
    '                                If IsSalesOrderAnalysis = False Then
    '                                    '***********************
    '                                    'Inserting Debit Amount
    '                                    '***********************
    '                                    objCommand.CommandText = ""
    '                                    'Task:2391 Added Column ArticleDefId
    '                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, Direction, sp_refrence,ArticleDefId) " _
    '                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.Price).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.Price).ToString)) & ", 0, '" & SetComments(grd).Replace("" & VendorName & ",", "").Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & "," & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & " )"
    '                                    'End Task:2391
    '                                    objCommand.ExecuteNonQuery()

    '                                    '***********************
    '                                    'Inserting Credit Amount
    '                                    '***********************


    '                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
    '                                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", 0,  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.Price).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.Price).ToString)) & ", '" & grd.Item(EnumGridDetail.Item).ToString & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & ")' )"
    '                                    If flgExcludeTaxPrice = False Then
    '                                        objCommand.CommandText = ""
    '                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", 0,  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.Price).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.Price).ToString)) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                        'End Task:2391
    '                                        objCommand.ExecuteNonQuery()
    '                                        'End Task:2369
    '                                    Else

    '                                        'Task:2391 Added Column ArticleDefId
    '                                        objCommand.CommandText = ""
    '                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                                                                 & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString)) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                        'End Task:2391
    '                                        objCommand.ExecuteNonQuery()
    '                                    End If

    '                                Else
    '                                    objCommand.CommandText = ""

    '                                    'Task:2391 Added Column ArticleDefId
    '                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                                                                                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Val(grd.Item(EnumGridDetail.NetBill).ToString) - IIf(grd.Item(EnumGridDetail.Tax).ToString = 0, 0, (Val(grd.Item(EnumGridDetail.Tax).ToString) / 100) * IIf(grd.Item(EnumGridDetail.Unit).Text = "Loose", ((grd.Item(EnumGridDetail.Qty).ToString + grd.Item(EnumGridDetail.SampleQty).ToString) * grd.Item(EnumGridDetail.CurrentPrice).ToString), (((grd.Item(EnumGridDetail.Qty).ToString * grd.Item(EnumGridDetail.PackQty).ToString) + grd.Item(EnumGridDetail.SampleQty).ToString) * grd.Item(EnumGridDetail.CurrentPrice).ToString))) & ", 0, '" & SetComments(grd).Replace("" & VendorName & ",", "").Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                    'End Task:2391
    '                                    'objCommand.Transaction = trans
    '                                    objCommand.ExecuteNonQuery()

    '                                    '***********************
    '                                    'Inserting Credit Amount
    '                                    '***********************
    '                                    'objCommand = New OleDbCommand
    '                                    ' objCommand.Connection = Con


    '                                    '******************* Change For Cost Center ******************
    '                                    ' By Imran Ali
    '                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.Price).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.Price).ToString)) & ", '" & grd.Item(EnumGridDetail.Item).ToString & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & ")' )"
    '                                    If flgExcludeTaxPrice = False Then
    '                                        objCommand.CommandText = ""
    '                                        'Task:2391 Added Column ArticleDefId
    '                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & Val(grd.Item(EnumGridDetail.NetBill).ToString) - IIf(grd.Item(EnumGridDetail.Tax).ToString = 0, 0, (Val(grd.Item(EnumGridDetail.Tax).ToString) / 100) * IIf(grd.Item(EnumGridDetail.Unit).Text = "Loose", ((grd.Item(EnumGridDetail.Qty).ToString + grd.Item(EnumGridDetail.SampleQty).ToString) * grd.Item(EnumGridDetail.CurrentPrice).ToString), (((grd.Item(EnumGridDetail.Qty).ToString * grd.Item(EnumGridDetail.PackQty).ToString) + grd.Item(EnumGridDetail.SampleQty).ToString) * grd.Item(EnumGridDetail.CurrentPrice).ToString))) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                        'End Task:2391
    '                                        objCommand.ExecuteNonQuery()
    '                                        'End Task:2369

    '                                    Else

    '                                        'Task:2391 Added Column ArticleDefId
    '                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                                                                                 & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString)) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                        'End Task:2391
    '                                        objCommand.ExecuteNonQuery()
    '                                        'End Task:2369



    '                                    End If



    '                                    '''''''''''''''''''''' Includ Discount Voucher ''''''''''''''''''''''''''''''''''''''''''

    '                                    If DiscountedPrice > 0 Then
    '                                        objCommand.CommandText = ""

    '                                        'Task:2391 Added Column ArticleDefId
    '                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                                                                                 & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(SalesDiscountAccount) & ", " & IIf(grd.Item(EnumGridDetail.Unit).Text = "Loose", (((Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.CurrentPrice).ToString)) * Val(grd.Item(EnumGridDetail.Discount_Percentage).ToString)) / 100), ((((Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.CurrentPrice).ToString)) * Val(grd.Item(EnumGridDetail.Discount_Percentage).ToString)) / 100)) & ", 0, '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "'," & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                        'End Task:2391
    '                                        'End Task:2369
    '                                        objCommand.ExecuteNonQuery()
    '                                    End If
    '                                End If

    '                            Else

    '                                objCommand.CommandText = ""

    '                                'Task:2391 Added Column ArticleDefId
    '                                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                          & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.Price).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.Price).ToString)) & ",0,'" & SetComments(grd).Replace("" & VendorName & ",", "").Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                'End Task:2391
    '                                'End Task:2369
    '                                objCommand.ExecuteNonQuery()

    '                                If DiscountedPrice > 0 Then


    '                                    objCommand.CommandText = ""
    '                                    'Before against task:2391
    '                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction,sp_refrence) " _
    '                                    '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(SalesDiscountAccount) & ", " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * DiscountedPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * DiscountedPrice) & ", 0, '" & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & Math.Round((Val(grd.Item(EnumGridDetail.Price).ToString)), 2) & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "')"
    '                                    'Task:2391 Added Column ArticleDefId
    '                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction,sp_refrence,ArticleDefId) " _
    '                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(SalesDiscountAccount) & ", " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * DiscountedPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * DiscountedPrice) & ", 0, '" & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & Math.Round((Val(grd.Item(EnumGridDetail.Price).ToString)), 2) & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                    'End Task:2391
    '                                    objCommand.ExecuteNonQuery()

    '                                    objCommand.CommandText = ""

    '                                    'Task:2391 Added Column ArticleDefId
    '                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", 0," & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * DiscountedPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * DiscountedPrice) & ",'Ref Discount On Sales: " & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & (DiscountedPrice) & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                    'End Task:2391
    '                                    objCommand.ExecuteNonQuery()

    '                                    If flgExcludeTaxPrice = False Then
    '                                        objCommand.CommandText = ""

    '                                        'Task:2391 added column articledefid
    '                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
    '                                                                                          & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.CurrentPrice).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.CurrentPrice).ToString)) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                        'End Task:2391
    '                                        objCommand.ExecuteNonQuery()
    '                                    Else

    '                                        'Task:2391 Added column articledefid
    '                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString)) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                        'End Task:2391
    '                                        objCommand.ExecuteNonQuery()


    '                                    End If
    '                                    'objCommand.ExecuteNonQuery()

    '                                Else
    '                                    If flgExcludeTaxPrice = False Then
    '                                        objCommand.CommandText = ""

    '                                        'Task:2391 Added Column ArticleDefId
    '                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                                    & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.Price).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.Price).ToString)) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                        'End Task:2391
    '                                        objCommand.ExecuteNonQuery()
    '                                    Else
    '                                        objCommand.CommandText = ""

    '                                        'Task:2391 Added column ArticleDefId
    '                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString)) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                        'End Task:2391
    '                                        objCommand.ExecuteNonQuery()
    '                                        'End Task:2369


    '                                    End If
    '                                    'objCommand.ExecuteNonQuery()
    '                                End If
    '                            End If

    '                            ''''''''''''''''''' Preparing Cost Of Sale Voucher ''''''''''''''''''''''''''''''
    '                            ''''''''''''''''''' Preparing Cost Of Sale Voucher ''''''''''''''''''''''''''''''
    '                            If flgCgsVoucher = True Then
    '                                objCommand.CommandText = ""
    '                                'Before against Task:2391                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence) " _
    '                                '                                                                          & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & CgsAccountId & ", " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * CostPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * CostPrice) & ", 0, '" & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & CostPrice & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "')"
    '                                'Task:2391 Added column ArticleDefId
    '                                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & CgsAccountId & ", " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * CostPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * CostPrice) & ", 0, '" & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & CostPrice & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                'End Task:2391
    '                                objCommand.ExecuteNonQuery()

    '                                objCommand.CommandText = ""
    '                                'Before against task:2391
    '                                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence) " _
    '                                '                                                                          & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & InvAccountId & ", 0, " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * CostPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * CostPrice) & ", '" & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & CostPrice & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "')"\
    '                                'Task:2391 Added column ArticleDefId
    '                                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
    '                                                                                                          & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & InvAccountId & ", 0, " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * CostPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * CostPrice) & ", '" & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & CostPrice & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
    '                                'End Task:2391
    '                                objCommand.ExecuteNonQuery()
    '                            End If
    '                            '''''''''''''''''''''''''''''' Code By Imran Ali 03/06/2013 '''''''''''''''''''''''''''''''''''''''
    '                            'End If
    '                        End If

    '                    Next
    '                End If
    '                '***********************
    '                '01-Feb-2011    Added for tax calculation
    '                '***********************
    '                Dim objIncTax As Object = dtData.Compute("SUM(SalesTax)", "")
    '                Dim objExcTax As Object = dtData.Compute("SUM(SalesExcTax)", "")
    '                Dim objSEDTax As Object = dtData.Compute("SUM(SEDTax)", "")

    '                If objIncTax > 0 Then
    '                    If flgExcludeTaxPrice = False Then
    '                        objCommand.CommandText = ""
    '                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
    '                        '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & Val(Me.txtTaxTotal.Text) & ", 0, 'Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"
    '                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
    '                        '                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Val(objIncTax) & ", 0, 'Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"


    '                        ''objCommand.Transaction = trans
    '                        'objCommand.ExecuteNonQuery()
    '                        'End If
    '                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                        '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & SalesTaxId & ", " & 0 & ",  " & Val(Me.txtTaxTotal.Text) & ", 'Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
    '                                             & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & SalesTaxId & ", " & 0 & ",  " & Val(objIncTax) & ", 'Tax Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

    '                        ' objCommand.Transaction = trans
    '                        objCommand.ExecuteNonQuery()

    '                    Else

    '                        'objCommand.CommandText = ""
    '                        ''objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
    '                        ''                       & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & Val(Me.txtTaxTotal.Text) & ", 0, 'Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"
    '                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
    '                        '                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Val(objExcTax) & ", 0, 'Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"


    '                        ''objCommand.Transaction = trans
    '                        'objCommand.ExecuteNonQuery()
    '                        'End If
    '                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                        '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & SalesTaxId & ", " & 0 & ",  " & Val(Me.txtTaxTotal.Text) & ", 'Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
    '                                             & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & SalesTaxId & ", " & 0 & ",  " & Val(objExcTax) & ", 'Tax Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

    '                        ' objCommand.Transaction = trans
    '                        objCommand.ExecuteNonQuery()
    '                    End If
    '                End If


    '                '
    '                'SED Tax Apply 01-07-2012
    '                '


    '                If objSEDTax > 0 Then


    '                    objCommand.CommandText = ""
    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
    '                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Val(objSEDTax) & ", 0, 'W.H Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

    '                    'objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()


    '                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
    '                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(SEDAccountId) & ", " & 0 & ",  " & Val(objSEDTax) & ", 'W.H Tax Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
    '                    ' objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()

    '                End If


    '                If Val(objMasterRow(enmMaster.TransitInsurance).ToString) > 0 Then

    '                    objCommand.CommandText = ""
    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId,sp_refrence) " _
    '                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Val(objMasterRow(enmMaster.TransitInsurance).ToString) & ", 0, 'Transit Insurace Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

    '                    'objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()


    '                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
    '                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(InsuranceAccountId) & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.TransitInsurance).ToString) & ", 'Transit Insurace Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
    '                    ' objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()


    '                End If
    '                '***********************

    '                '***********************
    '                '06-Dec-2011    Added for Fuel, Other Expense, Adjustment
    '                '***********************


    '                ''Fuel
    '                If Val(objMasterRow(enmMaster.FuelExpense).ToString) > 0 Then

    '                    objCommand.CommandText = ""
    '                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
    '                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.FuelExpAccount & ", " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 0, 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
    '                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.FuelExpAccount & ", " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 0, 'Fuel Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
    '                    'objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()
    '                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
    '                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
    '                    ' objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()

    '                End If


    '                '---------------------------------- Fuel Expense Credit  
    '                If ExpenseChargeToCustomer = True Then
    '                    'If Val(objMasterRow(enmMaster.FuelExpense).ToString) < 0 Then
    '                    objCommand.CommandText = ""
    '                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
    '                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.FuelExpAccount & ", " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 0, 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
    '                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Math.Abs(Val(objMasterRow(enmMaster.FuelExpense).ToString)) & ", 0, 'Fuel Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
    '                    'objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()
    '                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
    '                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.FuelExpAccount & ", " & 0 & ",  " & Math.Abs(Val(objMasterRow(enmMaster.FuelExpense).ToString)) & ", 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
    '                    ' objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()

    '                    'End If
    '                End If

    '                ''end Fuel


    '                ''Other Exp
    '                If Val(objMasterRow(enmMaster.OtherExpense).ToString) > 0 Then

    '                    objCommand.CommandText = ""
    '                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
    '                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.OtherExpAccount & ", " & Val(objMasterRow(enmMaster.OtherExpense).ToString) & ", 0, 'Expense Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
    '                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.OtherExpAccount & ", " & Val(objMasterRow(enmMaster.OtherExpense).ToString) & ", 0, 'Expense Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"


    '                    'objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()
    '                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.OtherExpense).ToString) & ", 'Expense Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
    '                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.OtherExpense).ToString) & ", 'Expense Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
    '                    ' objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()

    '                End If
    '                '--------------------------  Credit Other Expense 
    '                If ExpenseChargeToCustomer = True Then
    '                    If Val(objMasterRow(enmMaster.OtherExpense).ToString) < 0 Then

    '                        objCommand.CommandText = ""
    '                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
    '                        '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.OtherExpAccount & ", " & Val(objMasterRow(enmMaster.OtherExpense).ToString) & ", 0, 'Expense Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
    '                                                               & " VALUES(" & lngVoucherMasterId & "," & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Math.Abs(Val(objMasterRow(enmMaster.OtherExpense).ToString)) & ", 0, 'Expense Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"


    '                        'objCommand.Transaction = trans
    '                        objCommand.ExecuteNonQuery()
    '                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                        '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.OtherExpense).ToString) & ", 'Expense Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
    '                                             & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.OtherExpAccount & ", " & 0 & ",  " & Math.Abs(Val(objMasterRow(enmMaster.OtherExpense).ToString)) & ", 'Expense Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
    '                        ' objCommand.Transaction = trans
    '                        objCommand.ExecuteNonQuery()

    '                    End If
    '                End If
    '                ''end Other Exp


    '                ''Adjustment
    '                If Val(objMasterRow(enmMaster.Adjustment).ToString) > 0 Then

    '                    objCommand.CommandText = ""
    '                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
    '                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.AdjustmentExpAccount & ", " & Val(objMasterRow(enmMaster.Adjustment).ToString) & ", 0, 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
    '                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.AdjustmentExpAccount & ", " & IIf(objMasterRow(enmMaster.Adj_Flag).ToString = True, Val(objMasterRow(enmMaster.Adjustment).ToString), Adjustment) & ", 0, 'Adjustment Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

    '                    'objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()
    '                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.Adjustment).ToString) & ", 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
    '                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & 0 & ",  " & IIf(objMasterRow(enmMaster.Adj_Flag).ToString = True, Val(objMasterRow(enmMaster.Adjustment).ToString), Adjustment) & ", 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
    '                    ' objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()

    '                ElseIf Val(objMasterRow(enmMaster.Adjustment).ToString) < 0 Then
    '                    objCommand.CommandText = ""
    '                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
    '                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.AdjustmentExpAccount & ", " & Val(objMasterRow(enmMaster.Adjustment).ToString) & ", 0, 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
    '                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.AdjustmentExpAccount & ", 0, " & Math.Abs(IIf(objMasterRow(enmMaster.Adj_Flag).ToString = True, Val(objMasterRow(enmMaster.Adjustment).ToString), Adjustment)) & ", 'Adjustment Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

    '                    'objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()
    '                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.Adjustment).ToString) & ", 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

    '                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
    '                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Math.Abs(IIf(objMasterRow(enmMaster.Adj_Flag).ToString = True, Val(objMasterRow(enmMaster.Adjustment).ToString), Adjustment)) & ", 0, 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
    '                    ' objCommand.Transaction = trans
    '                    objCommand.ExecuteNonQuery()

    '                End If
    '                ''end Adjustment

    '                '***********************

    '                ' Receipt Voucher Master Information 
    '                'If ReceiptVoucherFlg = "True" Then
    '                '    objCommand.CommandText = ""
    '                '    objCommand.CommandText = "Delete From tblVoucherDetail WHERE Voucher_Id=" & VoucherId
    '                '    objCommand.ExecuteNonQuery()
    '                'End If
    '                'If ReceiptVoucherFlg = "True" AndAlso Val(Me.txtRecAmount.Text) <> 0 Then
    '                '    If Not ExistingVoucherFlg = True Then

    '                '        objCommand.CommandText = ""
    '                '        objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
    '                '                                   & " cheque_no, cheque_date,post,Source,voucher_code, coa_detail_id, Employee_Id)" _
    '                '                                   & " VALUES(" & Me.cmbCompany.SelectedValue & ", 1,  " & Convert.ToInt32(Me.cmbMethod.SelectedValue) & ", '" & VoucherNo & "', '" & Me.dtpPODate.ToString("yyyy-M-d h:mm:ss tt") & "', " _
    '                '                                   & " " & IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", "NULL") & ", " & IIf(Me.dtpChequeDate.Visible = True, "'" & dtpChequeDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ",'" & Me.Name & "','" & objMasterRow(enmMaster.SalesNo).ToString & "', " & Me.cmbDepositAccount.SelectedValue & ", " & IIf(Me.cmbSalesMan.SelectedIndex = -1, 0, Me.cmbSalesMan.SelectedValue) & ")" _
    '                '                                   & " SELECT @@IDENTITY"

    '                '        'objCommand.Transaction = trans
    '                '        lngVoucherMasterId = objCommand.ExecuteScalar


    '                '        objCommand.CommandText = ""
    '                '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
    '                '                               & " VALUES(" & lngVoucherMasterId & ", " & Me.cmbCompany.SelectedValue & ", " & Me.cmbDepositAccount.SelectedValue & ", " & Val(Me.txtRecAmount.Text) & ", 0, 'Receipt Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

    '                '        'objCommand.Transaction = trans
    '                '        objCommand.ExecuteNonQuery()


    '                '        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                '        '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.Adjustment).ToString) & ", 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"
    '                '        objCommand.CommandText = ""
    '                '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
    '                '                                             & " VALUES(" & lngVoucherMasterId & ", " & Me.cmbCompany.SelectedValue & ", " & VendorId & ", " & 0 & ",  " & Val(Me.txtRecAmount.Text) & ", 'Receipt Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

    '                '        ' objCommand.Transaction = trans
    '                '        objCommand.ExecuteNonQuery()

    '                '    Else

    '                '        objCommand.CommandText = ""
    '                '        objCommand.CommandText = "Update tblVoucher Set Voucher_Type_Id=" & Convert.ToInt32(Me.cmbMethod.SelectedValue) & ", Voucher_No='" & Me.txtVoucherNo.Text & "',  Voucher_Date='" & Me.dtpPODate.ToString("yyyy-M-d h:mm:ss tt") & "', Cheque_No=" & IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", "NULL") & ", Cheque_Date=" & IIf(Me.dtpChequeDate.Visible = True, "'" & Me.dtpChequeDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", coa_detail_id=" & Me.cmbDepositAccount.SelectedValue & ", Employee_Id=" & IIf(Me.cmbSalesMan.SelectedIndex = -1, 0, Me.cmbSalesMan.SelectedValue) & " WHERE Voucher_Id=" & VoucherId
    '                '        objCommand.ExecuteNonQuery()

    '                '        objCommand.CommandText = ""
    '                '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
    '                '                               & " VALUES(" & VoucherId & ", " & Me.cmbCompany.SelectedValue & ", " & Me.cmbDepositAccount.SelectedValue & ", " & Val(Me.txtRecAmount.Text) & ", 0, 'Receipt Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

    '                '        'objCommand.Transaction = trans
    '                '        objCommand.ExecuteNonQuery()


    '                '        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
    '                '        '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.Adjustment).ToString) & ", 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"
    '                '        objCommand.CommandText = ""
    '                '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
    '                '                                             & " VALUES(" & VoucherId & ", " & Me.cmbCompany.SelectedValue & ", " & VendorId & ", " & 0 & ",  " & Val(Me.txtRecAmount.Text) & ", 'Receipt Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

    '                '        ' objCommand.Transaction = trans
    '                '        objCommand.ExecuteNonQuery()

    '                '    End If
    '                'End If


    '                'If flgMultipleSalesOrder = False Then
    '                '    If Me.cmbPo.SelectedIndex > 0 Then
    '                '        objCommand.CommandText = "Select Qty , isnull(DeliveredQty, 0) as DeliveredQty, Isnull(DeliveredSchemeQty,0) as DeliveredSchemeQty  from SalesOrderDetailTable where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
    '                '        da.SelectCommand = objCommand
    '                '        Dim dt As New DataTable
    '                '        da.Fill(dt)
    '                '        Dim blnEqual As Boolean = True
    '                '        If dt.Rows.Count > 0 Then
    '                '            For Each r As DataRow In dt.Rows
    '                '                If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
    '                '                    blnEqual = False
    '                '                    Exit For
    '                '                End If
    '                '            Next
    '                '        End If
    '                '        If blnEqual = True Then
    '                '            objCommand.CommandText = "Update SalesOrderMasterTable set Status = '" & EnumStatus.Close.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
    '                '            objCommand.ExecuteNonQuery()
    '                '        Else
    '                '            objCommand.CommandText = "Update SalesOrderMasterTable set Status = '" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
    '                '            objCommand.ExecuteNonQuery()
    '                '        End If
    '                '    End If

    '                'Else

    '                '    objCommand.CommandText = "Select DISTINCT ISNULL(SO_ID,0) as SO_ID From SalesDetailTable WHERE SalesID=" & Val(Val(objMasterRow(enmMaster.SalesId).ToString)) & " AND ISNULL(Qty,0) <> 0"
    '                '    Dim dtPO As New DataTable
    '                '    Dim daPO As New OleDbDataAdapter(objCommand)
    '                '    daPO.Fill(dtPO)
    '                '    If dtPO IsNot Nothing Then
    '                '        If dtPO.Rows.Count > 0 Then
    '                '            For Each row As DataRow In dtPO.Rows

    '                '                objCommand.CommandText = "Select SUM(isnull(Qty,0)-isnull(DeliveredQty , 0)) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & row("SO_ID") & " Having SUM(isnull(Qty,0)-isnull(DeliveredQty , 0)) > 0 "
    '                '                Dim daPOQty As New OleDbDataAdapter(objCommand)
    '                '                Dim dtPOQty As New DataTable
    '                '                daPOQty.Fill(dtPOQty)
    '                '                Dim blnEqual1 As Boolean = True
    '                '                If dtPOQty.Rows.Count > 0 Then
    '                '                    'For Each r As DataRow In dtPOQty.Rows
    '                '                    'If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
    '                '                    blnEqual1 = False
    '                '                    'Exit For
    '                '                    'End If
    '                '                    ' Next
    '                '                End If
    '                '                If blnEqual1 = True Then
    '                '                    objCommand.CommandText = "Update SalesOrderMasterTable set Status = '" & EnumStatus.Close.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
    '                '                    objCommand.ExecuteNonQuery()
    '                '                Else
    '                '                    objCommand.CommandText = "Update SalesOrderMasterTable set Status = '" & EnumStatus.Open.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
    '                '                    objCommand.ExecuteNonQuery()
    '                '                End If
    '                '            Next
    '                '        End If
    '                '    End If
    '                'End If





    '                'If DeliveryChalanId > 0 Then
    '                '    objCommand.CommandText = "Select isnull(Qty,0) as Qty, isnull(DeliveredQty, 0) as DeliveredQty, Isnull(DeliveredSchemeQty,0) as DeliveredSchemeQty  from DeliveryChalanDetailTable where DeliveryId = " & DeliveryChalanId & ""
    '                '    da.SelectCommand = objCommand
    '                '    Dim dt As New DataTable
    '                '    da.Fill(dt)
    '                '    Dim blnEqual As Boolean = True
    '                '    If dt.Rows.Count > 0 Then
    '                '        For Each r As DataRow In dt.Rows
    '                '            If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
    '                '                blnEqual = False
    '                '                Exit For
    '                '            End If
    '                '        Next
    '                '    End If
    '                '    If blnEqual = True Then
    '                '        objCommand.CommandText = "Update DeliveryChalanMasterTable set Status = '" & EnumStatus.Close.ToString & "' where DeliveryId = " & DeliveryChalanId & ""
    '                '        objCommand.ExecuteNonQuery()
    '                '    Else
    '                '        objCommand.CommandText = "Update DeliveryChalanMasterTable set Status = '" & EnumStatus.Open.ToString & "' where DeliveryId = " & DeliveryChalanId & ""
    '                '        objCommand.ExecuteNonQuery()
    '                '    End If
    '                'End If
    '                StockMaster.StockDetailList = StockList
    '                Call New StockDAL().UpdateByTrans(StockMaster, trans)
    '                trans.Commit()
    '                Update_Record = True
    '                SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, objMasterRow(enmMaster.SalesNo).ToString.Trim, True)
    '                SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, objMasterRow(enmMaster.SalesNo).ToString.Trim, True)
    '            Next

    '            'insertvoucher()
    '            'Upgrading Stock here ...
    '            'setEditMode = True
    '            'getVoucher_Id = Val(objMasterRow(enmMaster.SalesId).ToString)
    '            'setVoucherNo = objMasterRow(enmMaster.SalesNo).ToString
    '            'Total_Amount = Val(Me.txtAmount.Text) 'Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
    '        End If
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Update_Record = False
    '        Throw ex
    '    Finally
    '        objCon.Close()
    '    End Try
    'End Function
    ''22-Jan-2014  Task:2391       Imran Ali              Avrage Rate Re-Calculation in ERP
    'Function GetVoucherNo() As String
    '    Dim docNo As String = String.Empty
    '    Dim VType As String = String.Empty
    '    Dim i As Integer = 0
    '    If i > 0 Then
    '        VType = "BRV"
    '    Else
    '        VType = "CRV"
    '    End If
    '    Try
    '        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
    '            Return GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(dtpPODate.Year, 2) + "-", "tblVoucher", "voucher_no")
    '        Else
    '            Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
    '            Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
    '            If Not dr Is Nothing Then
    '                If dr("config_Value") = "Monthly" Then
    '                    Return GetNextDocNo(VType & "-" & Format(dtpPODate, "yy") & dtpPODate.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
    '                Else
    '                    docNo = GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
    '                    Return docNo
    '                End If
    '            Else
    '                docNo = GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
    '                Return docNo
    '            End If
    '            Return ""
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    ''22-Jan-2014  Task:2391       Imran Ali              Avrage Rate Re-Calculation in ERP
    'Public Function SetComments(ByVal GridExRow As DataRow) As String
    '    Try
    '        Dim Comments As String = String.Empty
    '        If GridExRow IsNot Nothing Then
    '            If flgCommentCustomerFormat = True Then
    '                Comments += VendorName.Replace("'", "''") & ","
    '            End If
    '            If flgCommentArticleFormat = True Then
    '                Comments += " " & GridExRow.Item(EnumGridDetail.Item).ToString & ","
    '            End If
    '            If flgCommentArticleSizeFormat = True Then
    '                Comments += " " & GridExRow.Item(EnumGridDetail.Size).ToString & ","
    '            End If
    '            If flgCommentArticleColorFormat = True Then
    '                Comments += " " & GridExRow.Item(EnumGridDetail.Color).ToString & ","
    '            End If
    '            If flgCommentQtyFormat = True Then
    '                Comments += " " & IIf(Val(GridExRow.Item(EnumGridDetail.PackQty).ToString) = 0, Val(GridExRow.Item(EnumGridDetail.Qty).ToString), Val(GridExRow.Item(EnumGridDetail.Qty).ToString) * Val(GridExRow.Item(EnumGridDetail.PackQty).ToString))
    '            End If
    '            If flgCommentPriceFormat = True AndAlso flgCommentQtyFormat = True Then
    '                Comments += " X " & Val(GridExRow.Item(EnumGridDetail.Price).ToString)
    '            ElseIf flgCommentPriceFormat = True Then
    '                Comments += " " & Val(GridExRow.Item(EnumGridDetail.Price).ToString) & ","
    '            End If
    '            If flgCommentRemarksFormat = True Then
    '                Comments += " " & strRemarks.Replace("'", "''")
    '            End If
    '        End If

    '        Return Comments
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    ''End Task:2390

    'Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
    '    Try
    '        If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
    '            flgCompanyRights = getConfigValueByType("CompanyRights")
    '        End If

    '        If Not getConfigValueByType("CGSVoucher").ToString = "Error" Then
    '            flgCgsVoucher = getConfigValueByType("CGSVoucher")
    '        End If

    '        DefaultTax = Val(getConfigValueByType("Default_Tax_Percentage").ToString)

    '        If Not getConfigValueByType("TransitInssuranceTax").ToString = "Error" Then
    '            TransitInssuranceTax = Val(getConfigValueByType("TransitInssuranceTax").ToString)
    '        Else
    '            TransitInssuranceTax = 0
    '        End If

    '        If Not getConfigValueByType("WHTax_Percentage").ToString = "Error" Then
    '            WHTax = Val(getConfigValueByType("WHTax_Percentage").ToString)
    '        Else
    '            WHTax = 0
    '        End If
    '        If Not getConfigValueByType("Company-Based-Prefix").ToString = "Error" Then
    '            CompanyBasePrefix = Convert.ToBoolean(getConfigValueByType("Company-Based-Prefix").ToString)
    '        End If
    '        If Not getConfigValueByType("MultipleSalesOrder").ToString = "Error" Then
    '            flgMultipleSalesOrder = Convert.ToBoolean(getConfigValueByType("MultipleSalesOrder").ToString)
    '        Else
    '            flgMultipleSalesOrder = False
    '        End If
    '        ''Task:2369 Get Comments Configurations
    '        If Not getConfigValueByType("CommentCustomerFormat").ToString = "Error" Then
    '            flgCommentCustomerFormat = Convert.ToBoolean(getConfigValueByType("CommentCustomerFormat").ToString)
    '        Else
    '            flgCommentCustomerFormat = False
    '        End If
    '        If Not getConfigValueByType("CommentArticleFormat").ToString = "Error" Then
    '            flgCommentArticleFormat = Convert.ToBoolean(getConfigValueByType("CommentArticleFormat").ToString)
    '        Else
    '            flgCommentArticleFormat = False
    '        End If
    '        If Not getConfigValueByType("CommentArticleSizeFormat").ToString = "Error" Then
    '            flgCommentArticleSizeFormat = Convert.ToBoolean(getConfigValueByType("CommentArticleSizeFormat").ToString)
    '        Else
    '            flgCommentArticleSizeFormat = False
    '        End If
    '        If Not getConfigValueByType("CommentArticleColorFormat").ToString = "Error" Then
    '            flgCommentArticleColorFormat = Convert.ToBoolean(getConfigValueByType("CommentArticleColorFormat").ToString)
    '        Else
    '            flgCommentArticleColorFormat = False
    '        End If
    '        If Not getConfigValueByType("CommentQtyFormat").ToString = "Error" Then
    '            flgCommentQtyFormat = Convert.ToBoolean(getConfigValueByType("CommentQtyFormat").ToString)
    '        Else
    '            flgCommentQtyFormat = False
    '        End If
    '        If Not getConfigValueByType("CommentPriceFormat").ToString = "Error" Then
    '            flgCommentPriceFormat = Convert.ToBoolean(getConfigValueByType("CommentPriceFormat").ToString)
    '        Else
    '            flgCommentPriceFormat = False
    '        End If
    '        If Not getConfigValueByType("CommentRemarksFormat").ToString = "Error" Then
    '            flgCommentRemarksFormat = Convert.ToBoolean(getConfigValueByType("CommentRemarksFormat").ToString)
    '        Else
    '            flgCommentRemarksFormat = False
    '        End If

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'Private Sub frmDateLock_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '    Try
    '        'If Me.BackgroundWorker2.IsBusy Then Exit Sub
    '        'Me.BackgroundWorker2.RunWorkerAsync()
    '        'Do While Me.BackgroundWorker2.IsBusy
    '        '    Application.DoEvents()
    '        'Loop

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Public Sub SetLockDate()
        Try
            'Dim objConfig As SBModel.ConfigSystem = objConfigValueList.Find(AddressOf FilterConfig)
            'If objConfig IsNot Nothing Then
            '    If IsDBNull(objConfig.Config_Value) Then
            '        MyDateLock = Date.Now
            '        flgDateLock = False
            '    Else
            '        MyDateLock = CDate(objConfig.Config_Value)
            '        flgDateLock = Convert.ToBoolean(objConfig.IsActive)
            '    End If
            'End If
            GetDateLock()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function FilterConfig(ByVal Config As SBModel.ConfigSystem) As Boolean
        Try
            If Config.Config_Type = "DateLock" Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Task#125062015 Add 'Relevant' Radio button event 
    Private Sub rbtnRelevant_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnRelevant.CheckedChanged
        Try
            If rbtnRelevant.Checked = True Then     'if Relevant radio button is active, then txtNUpDown control will be enabled and dtpDateLock will be disabled
                Me.txtNUpDown.Enabled = True
                Me.dtpDateLock.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#125062015 Add 'Fixed' Radio button event 
    Private Sub rbtnFixed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnFixed.CheckedChanged
        Try
            If rbtnFixed.Checked = True Then
                Me.txtNUpDown.Enabled = False
                Me.dtpDateLock.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#125062015 Displaying all the records in grdHistory on Form Shown event
    Private Sub frmDateLock_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            GetAllRecords()     'calling the Sub GetAllRecords() for displaying records in grdHistory
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#125062015

    
    
    Private Sub btnLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLock.Click

        Try
            If _DateLockId > 0 Then
                If Update1() = True Then
                    '    'msg_Information("Date Locked Successfully")
                    '    If bgwDateLock.IsBusy Then Exit Sub
                    '    bgwDateLock.RunWorkerAsync()
                    '    Do While bgwDateLock.IsBusy
                    '        Application.DoEvents()
                    '    Loop
                    'Task:2391 Re Generate Avg Rate
                    getConfigValueList()
                    SetLockDate()
                    'Update_Record()
                    'End Task:2391
                    msg_Information("Date Locked Successfully")
                    DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            Else
                If Save() = True Then
                    'msg_Information("Date Locked Successfully")
                    'If bgwDateLock.IsBusy Then Exit Sub
                    'bgwDateLock.RunWorkerAsync()
                    'Do While bgwDateLock.IsBusy
                    '    Application.DoEvents()
                    'Loop
                    'Task:2391 Re Generate Avg Rate
                    getConfigValueList()
                    SetLockDate()
                    msg_Information("Date Locked Successfully")
                    DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                    'GetAllRecords()
                End If
            End If

            'Update_Record()
            'End Task:2391 


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDateUnLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDateUnLock.Click
        Try
            'DateLock = New DateLockBE
            'DateLock.DateLockId = DateLockDAL.getDateLockId(Me.dtpDateLock.Value)
            'DateLock.DateLock = Me.dtpDateLock.Value
            'DateLock.Lock = False
            'DateLock.EntryDate = Date.Now
            'DateLock.Username = LoginUserName
            If _DateLockId > 0 Then
                FillModel("UnLock")
                If New DateLockDAL().Update(DateLock) = True Then
                    'msg_Information("Date UnLocked Successfully")
                    'If bgwDateLock.IsBusy Then Exit Sub
                    'bgwDateLock.RunWorkerAsync()
                    'Do While bgwDateLock.IsBusy
                    '    Application.DoEvents()
                    'Loop

                    ''Task:2391 Re Generate Avg Rate
                    'MyFromDate = Me.dtpDateLock.Value
                    'MyToDate = Now
                    'Update_Record()
                    ''End Task:2391 
                    getConfigValueList()
                    SetLockDate()
                    ReSetControls()
                    msg_Information("Date UnLocked Successfully")
                    DialogResult = Windows.Forms.DialogResult.Yes
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdHistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdHistory.DoubleClick
        Try
            If Me.grdHistory.RowCount = 0 Then Exit Sub
            'DateLockId,Username,DateLock_Type,NoOfDays,DateLock,Lock
            _DateLockId = Val(Me.grdHistory.GetRow.Cells("DateLockId").Value.ToString)
            If Not IsDBNull(Me.grdHistory.GetRow.Cells("DateLock").Value) Then
                Me.dtpDateLock.Value = Me.grdHistory.GetRow.Cells("DateLock").Value.ToString
            Else
                Me.dtpDateLock.Value = Date.Now
            End If

            If Not IsDBNull(Me.grdHistory.GetRow.Cells("DateLock_Type").Value) Then
                If Me.grdHistory.GetRow.Cells("DateLock_Type").Value.ToString = "Fixed" Then
                    Me.rbtnFixed.Checked = True
                    Me.rbtnRelevant.Checked = False
                Else
                    Me.rbtnFixed.Checked = False
                    Me.rbtnRelevant.Checked = True
                End If
            Else
                Me.rbtnFixed.Checked = True
            End If

            If IsDBNull(Me.grdHistory.GetRow.Cells("NoOfDays").Value) Then
                Me.txtNUpDown.Value = 1
            Else
                Me.txtNUpDown.Value = Me.grdHistory.GetRow.Cells("NoOfDays").Value
            End If
            'Me.btnLock.Enabled = False
            'Me.btnDateUnLock.Enabled = True
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class