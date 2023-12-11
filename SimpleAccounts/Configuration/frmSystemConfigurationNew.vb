''16-Dec-2013  R-929   Imran Ali  one time sales return for a day
''29-Dec-2013 R:979   Imran Ali     PO SELECTION AFTER INWARD
''29-Dec-2013 Tsk:2359        Imran Ali     software Problem to Mr.Aziz
''6-Jan-2014 Task:2369          Imran Ali          Comments layout on ledger
''15-Jan-2014 Task:2376           Imran Ali          Purchase Comments In Ledger
''30-Jan-2014       TASK:2400       Imran Ali            Attach Multi Files In Voucher Entry 
''06-Feb-2014          TASK:M16     Imran Ali   Add New Fields Engine No And Chassis No. on Sales  
''20-Feb-2014   TASK:2432 Imran Ali 1 Delivery Chalan Multi Record of Same Item     
''26-Feb-2014  Task:2442   Imran Ali   4-bill no. DC no. shown on ledger report of party.
''28-Feb-2014 Task:2446   Imran Ali   1-DC No. and ENGINE No. field on customer ledger
''03-Mar-2014 TASK:2451  Imran Ali  4-ALPHABETIC order of items in sale and purchase window 
''13-Mar-2014 TASK:2488 Imran Ali Sales Certificate In ERP
'4-4-14 Task No.2537  Append The Leave Event for newly added combobox of Sale/Purchase  account tax deduction AC id
'17-Apr-2014 Task:2576 Imran Ali Branded SMS Configuration.
''Added Controls for Branded SMS config on designer
''29-Apr-2014 TASK:2595 Imran Ali Monthly Total Worked Process
'Task No 2608 Mughees  append the new two lines of code for new configration
''10-May-2014 TASK:2623 Imran Ali Slot Late Time Implement On Employee Salaries Detail Report (Shop and Save)
'Hide Leave Encashment NumberDropdown Control On Desig
''27-May-2014 TASK:2660 Imran Ali Claim Account Configuration
''20-June-2014 TASK:2701 IMRAN ALI Expense Entry on CMFA Document(Ravi)
''25-June-2014 TASK:2702 IMRAN ALI CMFA Load on Sales Invoice (RAVI)
''25-June-2014 TASK:2703 IMRAN ALI Enhancement In CMFA (RAVI)
''09-Jul-2014 TASK:2728 IMRAN ALI Comments layout on ledger (Ravi)
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''03-Aug-2014 Task:2767 Imran Ali Change Unit Option On Sales (ZR Traders)
''15-Aug-2014 Task:2784 Imran Ali Purchase account mapping at front end (Ravi)
''22-Aug-2014 Task:2795 Imran Ali Order Qty not exceed against delivery chalan/Sales
''19-Sep-2014 Taks:2847 Imran Ali Total Amount Wise Invoice Based Voucher
'02-Jul-2015 Task# 201507004 Ali Ansari Add Start Attendance Period and Total Leaves Allow
''14-Jul-2015 Task# 201507021 Ali Ansari Add Load Multi PO Check Configration
'23-Jul-2015 Task#23072015 Ahmad Sharif Add Configuration in Sales Configurations for Email Alert Due Invoices
'03-Aug-2015 Task#03082015 Ahmad Sharif Configuration for Email Attendance Report
'16-Sep-2015 Task#16092015 Ahmad Sharif Add Configuration for User wise Company and location
''12-11-2015 TASK-TFS-51 Additional Tax Account Configure For Apply at purchase
'' 08-08-2017 TASK : TFS1268 Allow user to update  delivered Sales Order  in certain condition.
''10-08-2017 : TFS1265 : Muhammad Ameen added MemoRemarks which are configuration based to be saved to voucher detail from Payment, Expense and Receipt. on 10-08-2017
'' TASK TFS1574 Muhammad Ameen: Stock impact on GRN and Delivery Chalan. ON 13-10-2017
''TFS1596 Lot/Batch wise Stock Management on 30-11-2017
'19-Mar-2018 : TFS2737 : Ayesha Rehman : Configuration for Lot wise rate for costing
''TASK TFS3538 Muhammad Ameen done on 13-06-18 to make configuration whether to choose old salary sheet print or new one.
'25-June-2018 : TFS3520 : Ayesha Rehman : Configuration for "Separate Closer of SO wrt DC and Sales"
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Public Class frmSystemConfigurationNew
    Implements IGeneral
    Dim ConfigDataTable As DataTable
    Dim ConfigList As List(Of ConfigSystem)
    Dim ConfigSaleAccount As ConfigSystem
    Dim DefaultAccountInPlaceCustomer As ConfigSystem
    Dim MainAccountforRevenueImport As ConfigSystem
    Dim ConfigEmailInvoiceDue As ConfigSystem
    Dim ConfigPurchaseAccount As ConfigSystem
    Dim ConfigStoreIssuanceAccount As ConfigSystem
    Dim ConfigSaleTaxAccount As ConfigSystem
    Dim ConfigPurchaseTaxAccount As ConfigSystem
    Dim ConfigTaxReceiveableAccount As ConfigSystem
    Dim ConfigTaxPayableAccount As ConfigSystem
    Dim ConfigFuelExpAccount As ConfigSystem
    Dim ConfigAdjExpAccount As ConfigSystem
    Dim ConfigOtherExpAccount As ConfigSystem
    Dim ConfigVoucherFormatAccount As ConfigSystem
    Dim ConfigVendorOnSaleAccount As ConfigSystem
    Dim ConfigCustomerOnPurchaseAccount As ConfigSystem
    Dim ConfigPurchaseAllowedPOAccount As ConfigSystem
    Dim ConfigCompanyNameHeaderAccount As ConfigSystem
    Dim ConfigCompanyAddressHeaderAccount As ConfigSystem
    Dim ConfigShowCompanyAddress As ConfigSystem
    Dim ConfigSalaryAccount As ConfigSystem
    Dim ConfigSalaryPayableAccount As ConfigSystem
    Dim ConfigEndOfDate As ConfigSystem
    Dim ConfigFastPrinting As ConfigSystem
    Dim ConfigItemFilterByLocation As ConfigSystem
    Dim ConfigStockViewOnSale As ConfigSystem
    Dim ConfigAllowMinusStock As ConfigSystem
    Dim ConfigStockTransfer As ConfigSystem
    Dim LoadAllItemInSale As ConfigSystem
    Dim StoreIssuenceWithProduction As ConfigSystem
    Dim PLAccount As ConfigSystem
    Dim ExchangeGainLossAccount As ConfigSystem
    Dim PreviouseRecordShow As ConfigSystem
    Dim ChangeDocNo As ConfigSystem
    Dim ShowMasterGrid As ConfigSystem
    Dim NewSecurityRights As ConfigSystem
    Dim ExpiryDate As ConfigSystem
    Dim SEDAccount As ConfigSystem
    Dim SIRIUSPartner As ConfigSystem
    Dim SIRIUSPartnerName As ConfigSystem
    Dim ReceiptVocheronSale As ConfigSystem
    Dim EmailAlert As ConfigSystem
    Dim SMSWithEngine As ConfigSystem
    Dim DefaultEmailId As ConfigSystem
    Dim AdminEmailId As ConfigSystem
    Dim ServiceItem As ConfigSystem
    Dim DefaultReminder As ConfigSystem
    Dim PaymentVoucherOnPurchase As ConfigSystem
    Dim StockDispatchOnProduction As ConfigSystem
    Dim PreviewInvoice As ConfigSystem
    Dim DiscountAllowed As ConfigSystem
    Dim SalesDiscountAccount As ConfigSystem
    Dim SalesOrderAnalysis As ConfigSystem
    Dim EmailAttachment As ConfigSystem
    Dim TransitInsurance As ConfigSystem
    Dim FileExport_Path As ConfigSystem
    Dim EnableAutoUpdate As ConfigSystem
    Dim TransitInssuranceTax As ConfigSystem
    Dim WHTax As ConfigSystem
    Dim DefaultTax As ConfigSystem
    Dim BackupDBPath As ConfigSystem
    Dim EmployeePicPath As ConfigSystem
    Dim ArticlePicPath As ConfigSystem
    Dim AccountHeadReadOnly As ConfigSystem
    Dim MarketReturnVoucherOnSalesReturn As ConfigSystem
    Dim ArticleShowImageOnStoreIssuance As ConfigSystem
    Dim AssetPricturePath As ConfigSystem
    Dim CompanyPrefix As ConfigSystem
    Dim MultipleSalesOrder As ConfigSystem
    Dim ProductionVoucher As ConfigSystem
    Dim InwardExpense As ConfigSystem
    Dim CurrencyonOpenLC As ConfigSystem
    Dim ExpenseChargeToCustomer As ConfigSystem
    Dim flgReminder As ConfigSystem
    Dim flgInvoiceWiseTaxPercent As ConfigSystem
    Dim CompanyRights As ConfigSystem
    Dim MenuRights As ConfigSystem
    Dim CostOfProduction As ConfigSystem
    Dim CompanyWisePrefixOnVoucher As ConfigSystem
    Dim CostOfSaleVoucher As ConfigSystem
    Dim ShowVendorOnDeliveryChalan As ConfigSystem
    Dim InvAccount As ConfigSystem
    Dim CGSAccount As ConfigSystem
    Dim StoreIssuenceVoucher As ConfigSystem
    Dim PrintVoucherLog As ConfigSystem
    Dim PrintVoucherCountLimit As ConfigSystem
    '20-July-2017: Task TFS1084: Waqar Raza: Added for sales history load quntity.
    'Start Task:
    Dim HistoryLoadQuantity As ConfigSystem
    'End Task:
    Dim ConversionTitle As ConfigSystem
    Dim ConversionFactor As ConfigSystem
    Dim WorkingDays As ConfigSystem
    Dim AttendanceEmail As ConfigSystem
    Dim AttendanceBasedSalary As ConfigSystem
    Dim dt As DataTable
    Dim StoreIssuaneDependonProductionPlan As ConfigSystem
    Dim EnableChequeDetailOnVoucherEntry As ConfigSystem
    Dim flgApplyAvgRate As ConfigSystem
    Dim TransportationCharges As ConfigSystem
    Dim flgBarcodeEnabled As ConfigSystem
    Dim TaxExcludePrice As ConfigSystem
    Dim GrossSalaryCalculationByFormula As ConfigSystem
    Dim GrossSalaryFormula As ConfigSystem
    Dim LeaveEncashment As ConfigSystem
    Dim AttendancePeriod As ConfigSystem 'Added to save Attendance Period Ali Ansari Task# 201507004
    Dim LoadMultiPO As ConfigSystem 'Added to save Attendance Period Ali Ansari Task# 201507004
    Dim ErrorMessage As String = String.Empty
    Dim key As String
    Dim IsOpenForm As Boolean = False
    Dim IsChangedValue As Object
    Dim UserwiseCompany As ConfigSystem
    Dim UserwiseLocation As ConfigSystem
    Dim _strImagePath As String = String.Empty
    Public ScreenName As String = String.Empty
    Dim OTWorkingDays As ConfigSystem
    Dim OTSalaryFactorPercentage As ConfigSystem
    Dim OTNormalDayMultiplier As ConfigSystem
    Dim OTOffDayMultiplier As ConfigSystem
    Dim ProFactPercentage As ConfigSystem
    Dim SalFactPercentage As ConfigSystem
    Dim VendorDifferenceQty As ConfigSystem
    Dim RetentionAccount As ConfigSystem
    Dim MobilizationAccount As ConfigSystem
    ''TASK TFS1927
    Dim WastedStockAccount As ConfigSystem
    Dim ScrappedStockAccount As ConfigSystem
    Dim SaleTaxDeductionAcId As ConfigSystem
    '' END TASK TFS1927
    '18-Jan-2018: Task TFS2236: Ayesha Rehman: Added For Showing Misc Accounts On Sales.
    'Start Task:
    Dim ConfigShowMiscAccountsOnSales As ConfigSystem
    'End Task:
    '18-Jan-2018: Task TFS2825: Ayesha Rehman:  SO load on DC also show Delivered Qty items configuration based.
    'Start Task:
    Dim ConfigLoadItemAfterDeliveredOnDC As ConfigSystem
    'End Task:

    ''TASK TFS3538
    Dim NewSalarySheetPrint As ConfigSystem
    ''END TASK TFS3538
    Enum enmScreen
        Accounts
        CashManagement
        Sales
        Purchase
        Production
        Inventory
        CRM
        HumanResource
        Assets
        ImportDocuments
        SiteManagement
        Reports
        Admistrator
        Utility
        Email
    End Enum


    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty
            Select Case Condition
                Case "SalesAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbSalesAccount, strSQL, True)
                    'TASK-TFS-51  Fill Additional Tax Account
                Case "AdditionalTax"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbAdditionalTaxAccount, strSQL, True)
                    'END TASK-TFS-51
                Case "DefaultAccountInPlaceCustomer"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbDefaultAccountInPlaceCustomer, strSQL, True)
                Case "MainAccountforRevenueImport"
                    strSQL = "SELECT  coa_main_id,  main_title +' - '+ main_code as main_title, main_type FROM         dbo.tblCOAMain order by 2"
                    FillDropDown(Me.cmbMainAccountforRevenueImport, strSQL, True)
                Case "PurchaseAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc "
                    FillDropDown(Me.cmbPurchaseAccount, strSQL, True)
                Case "CostProduction"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc "
                    FillDropDown(Me.cmbCostOfProduction, strSQL, True)
                Case "CGSAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY  detail_title Asc"
                    FillDropDown(Me.cmbStoreIssuanceAccount, strSQL, True)
                Case "SalesTaxAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbSalesTaxAccount, strSQL, True)
                Case "PurchaseTaxAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbPurchaseTaxAccount, strSQL, True)
                Case "ReceiveableTax"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbTaxReceiveableAccount, strSQL, True)
                Case "PayableTax"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbTaxPayableAccount, strSQL, True)
                Case "FuelExpAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbFuelExpAccount, strSQL, True)
                Case "AdjustmentExpAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbAdjustmentExpAccount, strSQL, True)
                Case "OtherExpAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbOtherExpAccount, strSQL, True)
                Case "SalaryAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbSalaryAccount, strSQL, True)
                Case "SalaryPayableAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbSalaryPayableAccount, strSQL, True)
                Case "PLAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbPLAccountId, strSQL, True)
                Case "ExchangeGainLossAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbExchangeGainLossAccount, strSQL, True)
                Case "SEDAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbSEDAccount, strSQL, True)
                Case "Email"
                    strSQL = "select EmailId as ID , Email as [Account Description] From TblDefEmail where Email is not null "
                    FillDropDown(Me.cmbDefaultEmail, strSQL, True)
                Case "SalesDiscount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbSalesDiscAccount, strSQL, True)
                Case "TransitInsurance"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbTransitInsuranceAccount, strSQL, True)
                Case "InwardExpense"
                    strSQL = "Select main_sub_sub_id, sub_sub_title From tblCOAMainSubSub Order By 2 Asc"
                    FillDropDown(Me.cmbInwardExpenseHeadAccount, strSQL)
                Case "InvAccount"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbInvAccount, strSQL, True)
                Case "CostOfGoodSoldccount"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbCGS, strSQL, True)
                Case "CylinderStockAccount"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbCylinderStockAccount, strSQL, True)
                    'Task No 2537 Append The Condition For SaleTaxDeductionACID to Fill The Newly Added Combo Box
                Case "SaleTaxDeductionAcId"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbSaleTaxDeductioAcId, strSQL, True)
                    'Task No 2537 Append The Condition For PurchaseTaxDeductionACID to Fill The Newly Added Combo Box
                Case "PurchaseTaxDeductionAcId"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.CmbPurchaseTaxIDeductionAccountNo, strSQL, True)
                    'Task:2660 Claim Account DropDown Filled.
                Case "ClaimAccount"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbClaimAccount, strSQL, True)
                    'Task:2660
                    'Task:2701 Configure Filldropdown CMFA Expense Account HEAD
                Case "CMFAExpAcHead"
                    strSQL = String.Empty
                    strSQL = "SELECT main_sub_sub_Id, sub_sub_title, sub_sub_code From tblCOAMainSubsub WHERE Account_Type='Expense'"
                    FillDropDown(Me.cmbCMFAExpAccountHead, strSQL)
                Case "AdditionalCost"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbAdditionalCostAccount, strSQL, True)
                    'Task No 2537 Append The Condition For SaleTaxDeductionACID to Fill The Newly Added Combo Box
                Case "CustomDuty"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbCustomDutyAccount, strSQL, True)
                    'Task No 2537 Append The Condition For PurchaseTaxDeductionACID to Fill The Newly Added Combo Box
                Case "SalesTax"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbLCSalesTaxAccount, strSQL, True)
                    'Task:2660 Claim Account DropDown Filled.
                Case "AdditionalSalesTax"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbAdditionalSalesTaxAccount, strSQL, True)
                    'Task:2660
                    'Task:2701 Configure Filldropdown CMFA Expense Account HEAD
                Case "AdvanceIncome"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbAdvanceIncomeTaxAccount, strSQL)
                Case "ExciseDuty"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbExciseDutyAccount, strSQL)
                Case "EmployeeHeadAccount"
                    FillDropDown(Me.cmbEmployeeHeadAccountId, "select distinct main_sub_sub_id, Sub_sub_title, sub_sub_code From vwCOADetail ORder by sub_sub_title ASC")
                Case "EmpDeparmentAcHead"
                    FillDropDown(Me.cmbEmpDeptHeadAccountId, "Select DISTINCT main_sub_Id, sub_title, sub_code from vwCOADetail where sub_title <> ''")
                    ''
                Case "EmpDeparmentAcHead"
                    FillDropDown(Me.cmbEmpDeptHeadAccountId, "Select DISTINCT main_sub_Id, sub_title, sub_code from vwCOADetail where sub_title <> ''")
                    '' TASK-407
                Case "Currency"
                    'FillDropDown(Me.cmbEmpDeptHeadAccountId, "Select DISTINCT main_sub_Id, sub_title, sub_code from vwCOADetail where sub_title <> ''")
                    FillDropDown(Me.cmbCurrency, "Select Distinct currency_id, currency_code From tblCurrency")
                    ''END TASK-407
                Case "VendorDifferenceQty"
                    'FillDropDown(Me.cmbEmpDeptHeadAccountId, "Select DISTINCT main_sub_Id, sub_title, sub_code from vwCOADetail where sub_title <> ''")
                    FillDropDown(Me.cmbVDQty, "SELECT coa_detail_id,detail_title FROM tblCOAMainSubSubDetail")
                Case "RetentionAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc "
                    FillDropDown(Me.cmbRetentionAccount, strSQL, True)
                Case "MobilizationAccount"
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc "
                    FillDropDown(Me.cmbMobilizationAccount, strSQL, True)

                    ''TASK TFS1927. Addition of two new account combos for waseted and scrapped items on decomposition screen.
                Case "WastedStockAccount"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbWastedStockAccount, strSQL, True)
                Case "ScrappedStockAccount"
                    strSQL = String.Empty
                    strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                    FillDropDown(Me.cmbScrappedStockAccount, strSQL, True)
                    ''Start TASK TFS2302
                Case "Agent"
                    FillDropDown(Me.cmbAgent, "select main_sub_sub_id, sub_sub_title +' - '+ sub_sub_code from tblCoaMainsubSub order by 2")
                Case "Dealer"
                    FillDropDown(Me.cmbDealer, "select main_sub_sub_id, sub_sub_title +' - '+ sub_sub_code from tblCoaMainsubSub order by 2")
                Case "Investor"
                    FillDropDown(Me.cmbInvestor, "select main_sub_sub_id, sub_sub_title +' - '+ sub_sub_code from tblCoaMainsubSub order by 2")
                Case "Seller"
                    FillDropDown(Me.cmbSeller, "select main_sub_sub_id, sub_sub_title +' - '+ sub_sub_code from tblCoaMainsubSub order by 2")
                Case "Buyer"
                    FillDropDown(Me.cmbBuyer, "select main_sub_sub_id, sub_sub_title +' - '+ sub_sub_code from tblCoaMainsubSub order by 2")
                Case "CommissionAccount"
                    FillDropDown(Me.cmbCommisionAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")

                    ''TFS2797 Saba Shabbir added Sale, Purchase and investment Accounts Configurations
                Case "PropertySalesAccount"
                    FillDropDown(Me.cmbSaleAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "PropertyPurchaseAccount"
                    FillDropDown(Me.cmbSysPuchaseAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "InvestmentBookingAccount"
                    FillDropDown(Me.cmbInvestmentAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")

                Case "ProfitExpenseAccount"
                    FillDropDown(Me.cmbProfitExpenseAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")

                    ''END TASK TFS2302

                    ''Start TASK TFS2375
                Case "AccountsApproval"
                    FillDropDown(Me.cmbAccountsApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "QuotationApproval"
                    FillDropDown(Me.cmbQuotationApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "SalesApproval"
                    FillDropDown(Me.cmbSalesApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "PurchaseApproval"
                    FillDropDown(Me.cmbPurchaseApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "ProductionApproval"
                    FillDropDown(Me.cmbProductionApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "PurchaseDemandApproval"
                    FillDropDown(Me.cmbPurchaseDemandApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "PurchaseOrderApproval"
                    FillDropDown(Me.cmbPurchaseOrderApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "PurchaseReturnApproval"
                    FillDropDown(Me.cmbPurchaseReturnApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "GRNApproval"
                    FillDropDown(Me.cmbGRNApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "PurchaseInquiryApproval"
                    FillDropDown(Me.cmbPurchaseInquiryApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "VendorQuotationApproval"
                    FillDropDown(Me.cmbVendorQuotationApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "VoucherApproval"
                    FillDropDown(Me.cmbVoucherEntryApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "PaymentApproval"
                    FillDropDown(Me.cmbPaymentApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "ReceiptApproval"
                    FillDropDown(Me.cmbReceiptApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "ExpenseApproval"
                    FillDropDown(Me.cmbExpenseApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "ActivityFeedBackApproval"
                    FillDropDown(Me.cmbActivityFeedBackApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "ActivityFeedBackApproval"
                    FillDropDown(Me.cmbActivityFeedBackApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                    ''END TASK TFS2375
                    'Start task 3113 Added By Ayesha Rehman
                Case "SalesInquiryApproval"
                    FillDropDown(Me.cmbSalesInquiry, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "SalesQuotationApproval"
                    FillDropDown(Me.cmbSalesQuotation, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "SalesOrderApproval"
                    FillDropDown(Me.cmbSalesOrder, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "DeliveryChallanApproval"
                    FillDropDown(Me.cmbDeliveryChallan, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "SalesInvoiceApproval"
                    FillDropDown(Me.cmbSalesInvoice, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "SalesReturnApproval"
                    FillDropDown(Me.cmbSalesReturn, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "CashRequestApproval"
                    FillDropDown(Me.cmbCashRequestApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                Case "EmployeeLoanRequestApproval"
                    FillDropDown(Me.cmbEmployeeLoanRequestApproval, "Select ApprovalProcessId , Title from ApprovalProcess where Active = 1 order by SortOrder ")
                    'End task 3113 Added by Ayesha Rehman
            End Select

        Catch ex As Exception

        End Try
    End Sub
    Public Function GetComboData() As DataTable
        Try

            Dim strSQL = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
            dt = GetDataTable(strSQL)
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            ConfigList = New List(Of ConfigSystem)
            If Me.DtpStartDate.Checked = True Then
                AttendancePeriod = New ConfigSystem
                AttendancePeriod.Config_Type = "Attendance_Period"
                AttendancePeriod.Config_Value = Me.DtpStartDate.Value.Date.ToString("yyyy-M-d h:mm:ss tt")
                ConfigList.Add(AttendancePeriod)
            End If
            ConfigSaleAccount = New ConfigSystem
            ConfigSaleAccount.Config_Type = "SalesCreditAccount"
            ConfigSaleAccount.Config_Value = Me.cmbSalesAccount.SelectedValue
            ConfigList.Add(ConfigSaleAccount)

            'Task#25012018 Default Account In Place of Customer Account
            DefaultAccountInPlaceCustomer = New ConfigSystem
            DefaultAccountInPlaceCustomer.Config_Type = "DefaultAccountInPlaceCustomer"
            DefaultAccountInPlaceCustomer.Config_Value = Me.cmbDefaultAccountInPlaceCustomer.SelectedValue
            ConfigList.Add(DefaultAccountInPlaceCustomer)

            'Task#06022018 MainAccountforRevenueImport
            MainAccountforRevenueImport = New ConfigSystem
            MainAccountforRevenueImport.Config_Type = "MainAccountforRevenueImport"
            MainAccountforRevenueImport.Config_Value = Me.cmbMainAccountforRevenueImport.SelectedValue
            ConfigList.Add(MainAccountforRevenueImport)

            'Task#23072015 Fill Model for EmailAlertDueInvoice Configuration
            ConfigEmailInvoiceDue = New ConfigSystem
            ConfigEmailInvoiceDue.Config_Type = "EmailAlertDueInvoice"
            ConfigEmailInvoiceDue.Config_Value = Me.chkEmailAlertDueInvoice.Checked
            ConfigList.Add(ConfigEmailInvoiceDue)
            'End Task#23072015

            ConfigPurchaseAccount = New ConfigSystem
            ConfigPurchaseAccount.Config_Type = "PurchaseDebitAccount"
            ConfigPurchaseAccount.Config_Value = Me.cmbPurchaseAccount.SelectedValue
            ConfigList.Add(ConfigPurchaseAccount)

            ConfigStoreIssuanceAccount = New ConfigSystem
            ConfigStoreIssuanceAccount.Config_Type = "StoreIssuenceAccount"
            ConfigStoreIssuanceAccount.Config_Value = Me.cmbStoreIssuanceAccount.SelectedValue
            ConfigList.Add(ConfigStoreIssuanceAccount)


            ConfigSaleTaxAccount = New ConfigSystem
            ConfigSaleTaxAccount.Config_Type = "SalesTaxCreditAccount"
            ConfigSaleTaxAccount.Config_Value = Me.cmbSalesTaxAccount.SelectedValue
            ConfigList.Add(ConfigSaleTaxAccount)

            ConfigPurchaseTaxAccount = New ConfigSystem
            ConfigPurchaseTaxAccount.Config_Type = "PurchaseTaxDebitAccountId"
            ConfigPurchaseTaxAccount.Config_Value = Me.cmbPurchaseTaxAccount.SelectedValue
            ConfigList.Add(ConfigPurchaseTaxAccount)

            ConfigTaxReceiveableAccount = New ConfigSystem
            ConfigTaxReceiveableAccount.Config_Type = "TaxreceiveableACid"
            ConfigTaxReceiveableAccount.Config_Value = Me.cmbTaxReceiveableAccount.SelectedValue
            ConfigList.Add(ConfigTaxReceiveableAccount)

            ConfigTaxPayableAccount = New ConfigSystem
            ConfigTaxPayableAccount.Config_Type = "taxpayableACid"
            ConfigTaxPayableAccount.Config_Value = Me.cmbTaxPayableAccount.SelectedValue
            ConfigList.Add(ConfigTaxPayableAccount)

            ConfigFuelExpAccount = New ConfigSystem
            ConfigFuelExpAccount.Config_Type = "FuelExpAccount"
            ConfigFuelExpAccount.Config_Value = Me.cmbFuelExpAccount.SelectedValue
            ConfigList.Add(ConfigFuelExpAccount)

            ConfigAdjExpAccount = New ConfigSystem
            ConfigAdjExpAccount.Config_Type = "AdjustmentExpAccount"
            ConfigAdjExpAccount.Config_Value = Me.cmbAdjustmentExpAccount.SelectedValue
            ConfigList.Add(ConfigAdjExpAccount)

            'Task#16092015 Fill Model with User wise Company & Location Config
            UserwiseCompany = New ConfigSystem
            UserwiseCompany.Config_Type = "UserwiseCompany"
            UserwiseCompany.Config_Value = Convert.ToString(Me.chkUserwiseCompany.Checked)
            ConfigList.Add(UserwiseCompany)

            UserwiseLocation = New ConfigSystem
            UserwiseLocation.Config_Type = "UserwiseLocation"
            UserwiseLocation.Config_Value = Convert.ToString(Me.chkUserwiseLocation.Checked)
            ConfigList.Add(UserwiseLocation)
            'End Task#16092015

            ConfigOtherExpAccount = New ConfigSystem
            ConfigOtherExpAccount.Config_Type = "OtherExpAccount"
            ConfigOtherExpAccount.Config_Value = Me.cmbOtherExpAccount.SelectedValue
            ConfigList.Add(ConfigOtherExpAccount)

            ConfigVoucherFormatAccount = New ConfigSystem
            ConfigVoucherFormatAccount.Config_Type = "VoucherNo"
            If Me.cmbVoucherFormat.Text = "Normal" Then
                ConfigVoucherFormatAccount.Config_Value = "Normal"
            ElseIf Me.cmbVoucherFormat.Text = "Monthly" Then
                ConfigVoucherFormatAccount.Config_Value = "Monthly"
            ElseIf Me.cmbVoucherFormat.Text = "Yearly" Then
                ConfigVoucherFormatAccount.Config_Value = "Yearly"
            End If
            ConfigList.Add(ConfigVoucherFormatAccount)

            ConfigVendorOnSaleAccount = New ConfigSystem
            ConfigVendorOnSaleAccount.Config_Type = "Show Vendor On Sales"
            ConfigVendorOnSaleAccount.Config_Value = IIf(Me.chkShowVendorOnSales.Checked = True, "True", "False")
            ConfigList.Add(ConfigVendorOnSaleAccount)

            ConfigCustomerOnPurchaseAccount = New ConfigSystem
            ConfigCustomerOnPurchaseAccount.Config_Type = "Show Customer On Purchase"
            ConfigCustomerOnPurchaseAccount.Config_Value = IIf(Me.chkShowCustomerOnPurchase.Checked = True, "True", "False")
            ConfigList.Add(ConfigCustomerOnPurchaseAccount)

            ''TFS2377 :Ayesha Rehman : PO Print Based on Checked And Posted Doc
            ConfigCustomerOnPurchaseAccount = New ConfigSystem
            ConfigCustomerOnPurchaseAccount.Config_Type = "POPrintAfterApproval"
            ConfigCustomerOnPurchaseAccount.Config_Value = IIf(Me.chkPOPrintAfterApproval.Checked = True, "True", "False")
            ConfigList.Add(ConfigCustomerOnPurchaseAccount)
            ''TFS2377 :Ayesha Rehman :End

            ConfigPurchaseAllowedPOAccount = New ConfigSystem
            ConfigPurchaseAllowedPOAccount.Config_Type = "PurchaseAllowedWithPO"
            ConfigPurchaseAllowedPOAccount.Config_Value = IIf(Me.chkPurchaseAllowedWithPO.Checked = True, "True", "False")
            ConfigList.Add(ConfigPurchaseAllowedPOAccount)

            ConfigCompanyNameHeaderAccount = New ConfigSystem
            ConfigCompanyNameHeaderAccount.Config_Type = "CompanyNameHeader"
            ConfigCompanyNameHeaderAccount.Config_Value = Me.txtCompanyName.Text
            ConfigList.Add(ConfigCompanyNameHeaderAccount)

            ConfigCompanyAddressHeaderAccount = New ConfigSystem
            ConfigCompanyAddressHeaderAccount.Config_Type = "CompanyAddressHeader"
            ConfigCompanyAddressHeaderAccount.Config_Value = Me.txtCompanyAddress.Text
            ConfigList.Add(ConfigCompanyAddressHeaderAccount)

            ConfigShowCompanyAddress = New ConfigSystem
            ConfigShowCompanyAddress.Config_Type = "ShowCompanyAddressOnPageHeader"
            ConfigShowCompanyAddress.Config_Value = IIf(Me.chkShowCompanyAddress.Checked = True, "True", "False")
            ConfigList.Add(ConfigShowCompanyAddress)


            ConfigSalaryAccount = New ConfigSystem
            ConfigSalaryAccount.Config_Type = "SalariesAccountId"
            ConfigSalaryAccount.Config_Value = Me.cmbSalaryAccount.SelectedValue
            ConfigList.Add(ConfigSalaryAccount)

            ConfigSalaryPayableAccount = New ConfigSystem
            ConfigSalaryPayableAccount.Config_Type = "SalariesPayableAccountId"
            ConfigSalaryPayableAccount.Config_Value = Me.cmbSalaryPayableAccount.SelectedValue
            ConfigList.Add(ConfigSalaryPayableAccount)

            If Me.dtpEndOfDate.Checked = True Then
                ConfigEndOfDate = New ConfigSystem
                ConfigEndOfDate.Config_Type = "EndOfDate"
                'Change Due to Date Formate on 19-Sep-2013 by Ijaz
                ConfigEndOfDate.Config_Value = Me.dtpEndOfDate.Value.Date.ToString("yyyy-M-d h:mm:ss tt")
                ConfigList.Add(ConfigEndOfDate)
            End If

            'ConfigFastPrinting = New ConfigSystem
            'ConfigFastPrinting.Config_Type = "FastPrinting"
            'ConfigFastPrinting.Config_Value = Convert.ToString(Me.chkFastPrinting.Checked)
            'ConfigList.Add(ConfigFastPrinting)

            ConfigItemFilterByLocation = New ConfigSystem
            ConfigItemFilterByLocation.Config_Type = "ArticleFilterByLocation"
            ConfigItemFilterByLocation.Config_Value = Convert.ToString(Me.chkItemFilterByLocation.Checked)
            ConfigList.Add(ConfigItemFilterByLocation)

            ConfigStockViewOnSale = New ConfigSystem
            ConfigStockViewOnSale.Config_Type = "StockViewOnSale"
            ConfigStockViewOnSale.Config_Value = Convert.ToString(Me.chkStockViewOnSales.Checked)
            ConfigList.Add(ConfigStockViewOnSale)

            ConfigAllowMinusStock = New ConfigSystem
            ConfigAllowMinusStock.Config_Type = "AllowMinusStock"
            ConfigAllowMinusStock.Config_Value = Convert.ToString(Me.chkAllowMinusStock.Checked)
            ConfigList.Add(ConfigAllowMinusStock)

            ConfigStockTransfer = New ConfigSystem
            ConfigStockTransfer.Config_Type = "StockTransferFromDispatch"
            ConfigStockTransfer.Config_Value = Convert.ToString(Me.chkStockDispatchTransfer.Checked)
            ConfigList.Add(ConfigStockTransfer)

            StoreIssuenceWithProduction = New ConfigSystem
            StoreIssuenceWithProduction.Config_Type = "StoreIssuenceWithProduction"
            StoreIssuenceWithProduction.Config_Value = Convert.ToString(Me.chkStoreIssuenceWithProduction.Checked)
            ConfigList.Add(StoreIssuenceWithProduction)

            LoadAllItemInSale = New ConfigSystem
            LoadAllItemInSale.Config_Type = "LoadAllItemsInSales"
            LoadAllItemInSale.Config_Value = Convert.ToString(Me.chkLoadItemGridSales.Checked)
            ConfigList.Add(LoadAllItemInSale)

            PLAccount = New ConfigSystem
            PLAccount.Config_Type = "PLAccountId"
            PLAccount.Config_Value = Convert.ToString(Me.cmbPLAccountId.SelectedValue)
            ConfigList.Add(PLAccount)

            ExchangeGainLossAccount = New ConfigSystem
            ExchangeGainLossAccount.Config_Type = "ExchangeGainLossAccount"
            ExchangeGainLossAccount.Config_Value = Convert.ToString(Me.cmbExchangeGainLossAccount.SelectedValue)
            ConfigList.Add(ExchangeGainLossAccount)

            PreviouseRecordShow = New ConfigSystem
            PreviouseRecordShow.Config_Type = "PreviouseRecordShow"
            PreviouseRecordShow.Config_Value = Convert.ToString(Me.chkPreviouseRecordShow.Checked)
            ConfigList.Add(PreviouseRecordShow)

            ChangeDocNo = New ConfigSystem
            ChangeDocNo.Config_Type = "ChangeDocNo"
            ChangeDocNo.Config_Value = Convert.ToString(Me.chkChangeDocumentNo.Checked)
            ConfigList.Add(ChangeDocNo)

            ShowMasterGrid = New ConfigSystem
            ShowMasterGrid.Config_Type = "ShowMasterGrid"
            ShowMasterGrid.Config_Value = Convert.ToString(Me.chkShowMasterGrid.Checked)
            ConfigList.Add(ShowMasterGrid)

            NewSecurityRights = New ConfigSystem
            NewSecurityRights.Config_Type = "NewSecurityRights"
            NewSecurityRights.Config_Value = Convert.ToString(Me.chkNewSecurityRights.Checked)
            ConfigList.Add(NewSecurityRights)

            ExpiryDate = New ConfigSystem
            ExpiryDate.Config_Type = "ItemExpiryDateOnPurchase"
            ExpiryDate.Config_Value = Convert.ToString(Me.chkExpiryDate.Checked)
            ConfigList.Add(ExpiryDate)

            SEDAccount = New ConfigSystem
            SEDAccount.Config_Type = "SEDAccountId"
            SEDAccount.Config_Value = Convert.ToString(Me.cmbSEDAccount.SelectedValue)
            ConfigList.Add(SEDAccount)

            SIRIUSPartner = New ConfigSystem
            SIRIUSPartner.Config_Type = "SoftbeatsPartner"
            SIRIUSPartner.Config_Value = Convert.ToString(Me.chkSIRIUSPartner.Checked)
            ConfigList.Add(SIRIUSPartner)

            SIRIUSPartnerName = New ConfigSystem
            SIRIUSPartnerName.Config_Type = "SoftbeatsPartnerName"
            SIRIUSPartnerName.Config_Value = Convert.ToString(Me.txtPartnerName.Text.ToString)
            ConfigList.Add(SIRIUSPartnerName)

            EmailAlert = New ConfigSystem
            EmailAlert.Config_Type = "EmailAlert"
            EmailAlert.Config_Value = Convert.ToString(Me.chkEmailAlert.Checked)
            ConfigList.Add(EmailAlert)

            ReceiptVocheronSale = New ConfigSystem
            ReceiptVocheronSale.Config_Type = "ReceiptVoucherOnSales"
            ReceiptVocheronSale.Config_Value = Convert.ToString(Me.chkReceiptVoucherOnSales.Checked)
            ConfigList.Add(ReceiptVocheronSale)
            '''''''''''''''''''''''''''
            ''''''''''''DefaultEmailId'''''''''''''''
            '''''''''''''''''''''''''''
            '''''''''''''''''''''''''''
            DefaultEmailId = New ConfigSystem
            DefaultEmailId.Config_Type = "DefaultEmailId"
            DefaultEmailId.Config_Value = Convert.ToString(Me.cmbDefaultEmail.SelectedValue)
            ConfigList.Add(DefaultEmailId)

            ServiceItem = New ConfigSystem
            ServiceItem.Config_Type = "ServiceItem"
            ServiceItem.Config_Value = Convert.ToString(Me.chkServiceItem.Checked)
            ConfigList.Add(ServiceItem)

            DefaultReminder = New ConfigSystem
            DefaultReminder.Config_Type = "DefaultReminder"
            DefaultReminder.Config_Value = Convert.ToString(Me.nudDefaultReminder.Value)
            ConfigList.Add(DefaultReminder)

            PaymentVoucherOnPurchase = New ConfigSystem
            PaymentVoucherOnPurchase.Config_Type = "PaymentVoucherOnPurchase"
            PaymentVoucherOnPurchase.Config_Value = Convert.ToString(Me.chkPaymentVoucherOnPurchase.Checked)
            ConfigList.Add(PaymentVoucherOnPurchase)

            StockDispatchOnProduction = New ConfigSystem
            StockDispatchOnProduction.Config_Type = "StockDispatchOnProduction"
            StockDispatchOnProduction.Config_Value = Convert.ToString(Me.chkStockDispatchOnProduction.Checked)
            ConfigList.Add(StockDispatchOnProduction)

            PreviewInvoice = New ConfigSystem
            PreviewInvoice.Config_Type = "PreviewInvoice"
            PreviewInvoice.Config_Value = Convert.ToString(Me.chkPreviewInvoice.Checked)
            ConfigList.Add(PreviewInvoice)

            DiscountAllowed = New ConfigSystem
            DiscountAllowed.Config_Type = "DiscountVoucherOnSale"
            DiscountAllowed.Config_Value = Convert.ToString(Me.chkDiscountVoucher.Checked)
            ConfigList.Add(DiscountAllowed)

            SalesDiscountAccount = New ConfigSystem
            SalesDiscountAccount.Config_Type = "SalesDiscountAccount"
            SalesDiscountAccount.Config_Value = Convert.ToString(Me.cmbSalesDiscAccount.SelectedValue)
            ConfigList.Add(SalesDiscountAccount)

            SalesOrderAnalysis = New ConfigSystem
            SalesOrderAnalysis.Config_Type = "SalesOrderAnalysis"
            SalesOrderAnalysis.Config_Value = Convert.ToString(Me.chkSalesOrderAnalysis.Checked)
            ConfigList.Add(SalesOrderAnalysis)

            AdminEmailId = New ConfigSystem
            AdminEmailId.Config_Type = "AdminEmailId"
            AdminEmailId.Config_Value = Convert.ToString(Me.txtAdminEmail.Text)
            ConfigList.Add(AdminEmailId)
            '''''''''''''''''''''''''''''''''''
            '''''''''''''''EmailAttachment''''''''''''''''''''
            '''''''''''''''''''''''''''''''''''
            EmailAttachment = New ConfigSystem
            EmailAttachment.Config_Type = "EmailAttachment"
            EmailAttachment.Config_Value = Convert.ToString(Me.chkAttachments.Checked)
            ConfigList.Add(EmailAttachment)

            TransitInsurance = New ConfigSystem
            TransitInsurance.Config_Type = "TransitInsuranceAccountId"
            TransitInsurance.Config_Value = Convert.ToString(Me.cmbTransitInsuranceAccount.SelectedValue)
            ConfigList.Add(TransitInsurance)

            TransitInsurance = New ConfigSystem
            TransitInsurance.Config_Type = "TransitInsuranceAccountId"
            TransitInsurance.Config_Value = Convert.ToString(Me.cmbTransitInsuranceAccount.SelectedValue)
            ConfigList.Add(TransitInsurance)
            ''''''''''''''''''''
            '''''''''''''''''
            '''''''''''''''''
            ''''''''''''''''
            FileExport_Path = New ConfigSystem
            FileExport_Path.Config_Type = "FileExportPath"
            FileExport_Path.Config_Value = Convert.ToString(Me.txtFileExportPath.Text)
            ConfigList.Add(FileExport_Path)

            EnableAutoUpdate = New ConfigSystem
            EnableAutoUpdate.Config_Type = "EnableAutoUpdate"
            EnableAutoUpdate.Config_Value = Convert.ToString(Me.chkAutoUpdate.Checked)
            ConfigList.Add(EnableAutoUpdate)

            TransitInssuranceTax = New ConfigSystem
            TransitInssuranceTax.Config_Type = "TransitInssuranceTax"
            TransitInssuranceTax.Config_Value = Convert.ToString(Me.txtTransitInssuranceTax.Text)
            ConfigList.Add(TransitInssuranceTax)

            WHTax = New ConfigSystem
            WHTax.Config_Type = "WHTax_Percentage"
            WHTax.Config_Value = Convert.ToString(Me.txtWHTax.Text)
            ConfigList.Add(WHTax)


            DefaultTax = New ConfigSystem
            DefaultTax.Config_Type = "Default_Tax_Percentage"
            DefaultTax.Config_Value = Convert.ToString(Me.txtDefaultTax.Text)
            ConfigList.Add(DefaultTax)

            BackupDBPath = New ConfigSystem
            BackupDBPath.Config_Type = "BackupDBPath"
            BackupDBPath.Config_Value = Convert.ToString(Me.txtBackupDBPath.Text)
            ConfigList.Add(BackupDBPath)

            EmployeePicPath = New ConfigSystem
            EmployeePicPath.Config_Type = "EmployeePicturePath"
            EmployeePicPath.Config_Value = Convert.ToString(Me.txtEmployeePicturePath.Text)
            ConfigList.Add(EmployeePicPath)

            ArticlePicPath = New ConfigSystem
            ArticlePicPath.Config_Type = "ArticlePicturePath"
            ArticlePicPath.Config_Value = Convert.ToString(Me.txtArticlePicturePath.Text)
            ConfigList.Add(ArticlePicPath)

            AccountHeadReadOnly = New ConfigSystem
            AccountHeadReadOnly.Config_Type = "AccountHeadReadOnly"
            AccountHeadReadOnly.Config_Value = Convert.ToString(Me.chkAccountHeadReadonly.Checked)
            ConfigList.Add(AccountHeadReadOnly)


            MarketReturnVoucherOnSalesReturn = New ConfigSystem
            MarketReturnVoucherOnSalesReturn.Config_Type = "MarketReturnVoucher"
            MarketReturnVoucherOnSalesReturn.Config_Value = Convert.ToString(Me.chkMarketReturnVoucher.Checked)
            ConfigList.Add(MarketReturnVoucherOnSalesReturn)

            CompanyPrefix = New ConfigSystem
            CompanyPrefix.Config_Type = "Company-Based-Prefix"
            CompanyPrefix.Config_Value = Convert.ToString(Me.chkCompanyPrefix.Checked)
            ConfigList.Add(CompanyPrefix)

            ArticleShowImageOnStoreIssuance = New ConfigSystem
            ArticleShowImageOnStoreIssuance.Config_Type = "ArticleShowImageOnStoreIssuance"
            ArticleShowImageOnStoreIssuance.Config_Value = Convert.ToString(Me.chkArticleShowImage.Checked)
            ConfigList.Add(ArticleShowImageOnStoreIssuance)


            AssetPricturePath = New ConfigSystem
            AssetPricturePath.Config_Type = "AssetPicturePath"
            AssetPricturePath.Config_Value = Convert.ToString(Me.txtAssetPicturePath.Text)
            ConfigList.Add(AssetPricturePath)


            MultipleSalesOrder = New ConfigSystem
            MultipleSalesOrder.Config_Type = "MultipleSalesOrder"
            MultipleSalesOrder.Config_Value = Convert.ToString(Me.chkMultipleSalesOrder.Checked)
            ConfigList.Add(MultipleSalesOrder)

            ProductionVoucher = New ConfigSystem
            ProductionVoucher.Config_Type = "ProductionVoucher"
            ProductionVoucher.Config_Value = Convert.ToString(Me.chkProductionVoucher.Checked)
            ConfigList.Add(ProductionVoucher)

            InwardExpense = New ConfigSystem
            InwardExpense.Config_Type = "InwardExpHeadAcId"
            InwardExpense.Config_Value = Convert.ToString(Me.cmbInwardExpenseHeadAccount.SelectedValue)
            ConfigList.Add(InwardExpense)


            CurrencyonOpenLC = New ConfigSystem
            CurrencyonOpenLC.Config_Type = "CurrencyonOpenLC"
            CurrencyonOpenLC.Config_Value = Convert.ToString(Me.chkCurrency.Checked)
            ConfigList.Add(CurrencyonOpenLC)


            ExpenseChargeToCustomer = New ConfigSystem
            ExpenseChargeToCustomer.Config_Type = "ExpenseChargeToCustomer"
            ExpenseChargeToCustomer.Config_Value = Convert.ToString(Me.chkExpenseChargeToCustomer.Checked)
            ConfigList.Add(ExpenseChargeToCustomer)

            flgReminder = New ConfigSystem
            flgReminder.Config_Type = "Reminder"
            flgReminder.Config_Value = Convert.ToString(Me.chkReminder.Checked)
            ConfigList.Add(flgReminder)


            flgInvoiceWiseTaxPercent = New ConfigSystem
            flgInvoiceWiseTaxPercent.Config_Type = "InvoiceWiseTaxPercent"
            flgInvoiceWiseTaxPercent.Config_Value = Convert.ToString(Me.chkInvoiceWiseTax.Checked)
            ConfigList.Add(flgInvoiceWiseTaxPercent)

            CompanyRights = New ConfigSystem
            CompanyRights.Config_Type = "CompanyRights"
            CompanyRights.Config_Value = Convert.ToString(Me.chkUserCompanyRights.Checked)
            ConfigList.Add(CompanyRights)

            MenuRights = New ConfigSystem
            MenuRights.Config_Type = "MenuRights"
            MenuRights.Config_Value = Convert.ToString(Me.chkMenuRights.Checked)
            ConfigList.Add(MenuRights)

            CostOfProduction = New ConfigSystem
            CostOfProduction.Config_Type = "StoreCreditAccount"
            CostOfProduction.Config_Value = Convert.ToString(Me.cmbCostOfProduction.SelectedValue)
            ConfigList.Add(CostOfProduction)

            CostOfSaleVoucher = New ConfigSystem
            CostOfSaleVoucher.Config_Type = "CGSVoucher"
            CostOfSaleVoucher.Config_Value = Convert.ToString(Me.chkCostofsalevoucher.Checked)
            ConfigList.Add(CostOfSaleVoucher)


            ShowVendorOnDeliveryChalan = New ConfigSystem
            ShowVendorOnDeliveryChalan.Config_Type = "ShowVendorOnDeliveryChalan"
            ShowVendorOnDeliveryChalan.Config_Value = Convert.ToString(Me.chkShowVendorOnDeliveryChalan.Checked)
            ConfigList.Add(ShowVendorOnDeliveryChalan)

            CGSAccount = New ConfigSystem
            CGSAccount.Config_Type = "CGSAccountId"
            CGSAccount.Config_Value = Convert.ToString(Me.cmbCGS.SelectedValue)
            ConfigList.Add(CGSAccount)

            InvAccount = New ConfigSystem
            InvAccount.Config_Type = "InvAccountId"
            InvAccount.Config_Value = Convert.ToString(Me.cmbInvAccount.SelectedValue)
            ConfigList.Add(InvAccount)


            StoreIssuenceVoucher = New ConfigSystem
            StoreIssuenceVoucher.Config_Type = "StoreIssuenceVoucher"
            StoreIssuenceVoucher.Config_Value = Convert.ToString(Me.chkStoreIssuenceVoucher.Checked)
            ConfigList.Add(StoreIssuenceVoucher)


            PrintVoucherLog = New ConfigSystem
            PrintVoucherLog.Config_Type = "PrintLog"
            PrintVoucherLog.Config_Value = Convert.ToString(Me.chkSalemanVoucherPrintLog.Checked)
            ConfigList.Add(PrintVoucherLog)


            PrintVoucherCountLimit = New ConfigSystem
            PrintVoucherCountLimit.Config_Type = "PrintCount"
            PrintVoucherCountLimit.Config_Value = Convert.ToString(Me.txtSalemanVoucherPrintCount.Text)
            ConfigList.Add(PrintVoucherCountLimit)
            '20-July-2017: Task TFS1084: Waqar Raza: Added for sales history load quntity.
            'Start Task:
            HistoryLoadQuantity = New ConfigSystem
            HistoryLoadQuantity.Config_Type = "SalesHistoryLoadQuantity"
            HistoryLoadQuantity.Config_Value = Convert.ToString(Me.txtHistoryLoadQuantity.Text)
            ConfigList.Add(HistoryLoadQuantity)
            'End Task:

            '09-Jan-2018: Task TFS2075 Waqar Raza: Added for showing Conversion Title on Purchase and Purchase Order.
            'Start Task:
            ConversionTitle = New ConfigSystem
            ConversionTitle.Config_Type = "ConversionTitle"
            ConversionTitle.Config_Value = Me.txtConversionTitle.Text
            ConfigList.Add(ConversionTitle)

            ConversionFactor = New ConfigSystem
            ConversionFactor.Config_Type = "ConversionFactor"
            ConversionFactor.Config_Value = Convert.ToString(Me.txtConversionFactor.Text)
            ConfigList.Add(ConversionFactor)
            'End Task:

            WorkingDays = New ConfigSystem
            WorkingDays.Config_Type = "EnabledAttendanceEmailAlert"
            WorkingDays.Config_Value = Convert.ToString(Me.txtWorkingDays.Text)
            ConfigList.Add(WorkingDays)

            WorkingDays = New ConfigSystem
            WorkingDays.Config_Type = "VendorQuotation"
            WorkingDays.Config_Value = txtVendorQuotationDocPrefix.Text
            ConfigList.Add(WorkingDays)

            'Task#03082015 Configuration for Email Attendance Report (Ahmad Sharif)
            AttendanceEmail = New ConfigSystem
            AttendanceEmail.Config_Type = "EnabledAttendanceEmailAlert"
            AttendanceEmail.Config_Value = Convert.ToString(Me.chkAttendanceEmailAlert.Checked)
            ConfigList.Add(AttendanceEmail)

            CompanyWisePrefixOnVoucher = New ConfigSystem
            CompanyWisePrefixOnVoucher.Config_Type = "CompanyWisePrefix"
            CompanyWisePrefixOnVoucher.Config_Value = Convert.ToString(Me.chkCompanyWisePrefixOnVoucher.Checked)
            ConfigList.Add(CompanyWisePrefixOnVoucher)
            'End Task#03082015


            AttendanceBasedSalary = New ConfigSystem
            AttendanceBasedSalary.Config_Type = "AttendanceBasedSalary"
            AttendanceBasedSalary.Config_Value = Convert.ToString(Me.chkAttendanceBaseSalary.Checked)
            ConfigList.Add(AttendanceBasedSalary)


            StoreIssuaneDependonProductionPlan = New ConfigSystem
            StoreIssuaneDependonProductionPlan.Config_Type = "StoreIssuaneDependonProductionPlan"
            StoreIssuaneDependonProductionPlan.Config_Value = Convert.ToString(Me.chkStoreIssuanceDependonProductionPlan.Checked)
            ConfigList.Add(StoreIssuaneDependonProductionPlan)

            EnableChequeDetailOnVoucherEntry = New ConfigSystem
            EnableChequeDetailOnVoucherEntry.Config_Type = "EnableChequeDetailOnVoucherEntry"
            EnableChequeDetailOnVoucherEntry.Config_Value = Convert.ToString(Me.chkChequeDetailEnable.Checked)
            ConfigList.Add(EnableChequeDetailOnVoucherEntry)


            'Task#08082015 On Delivery Challan SMS send with Engine no (Ahmad Sharif)
            SMSWithEngine = New ConfigSystem
            SMSWithEngine.Config_Type = "DeliveryChalanByEnigneNo"
            SMSWithEngine.Config_Value = Convert.ToBoolean(Me.chkSMSWithEngineNo.Checked)
            ConfigList.Add(SMSWithEngine)
            'End Task#08082015

            TransportationCharges = New ConfigSystem
            TransportationCharges.Config_Type = "TransaportationChargesVoucher"
            TransportationCharges.Config_Value = Convert.ToString(Me.chkTransporterCharges.Checked)
            ConfigList.Add(TransportationCharges)


            flgApplyAvgRate = New ConfigSystem
            flgApplyAvgRate.Config_Type = "AvgRate"
            flgApplyAvgRate.Config_Value = Convert.ToString(Me.chkAvgRate.Checked)
            ConfigList.Add(flgApplyAvgRate)

            flgBarcodeEnabled = New ConfigSystem
            flgBarcodeEnabled.Config_Type = "BarcodeEnabled"
            flgBarcodeEnabled.Config_Value = Convert.ToString(Me.chkBarcodeEnabled.Checked)
            ConfigList.Add(flgBarcodeEnabled)

            TaxExcludePrice = New ConfigSystem
            TaxExcludePrice.Config_Type = "ExcludeTaxPrice"
            TaxExcludePrice.Config_Value = Convert.ToString(Me.chkTaxExcludePrice.Checked)
            ConfigList.Add(TaxExcludePrice)

            GrossSalaryCalculationByFormula = New ConfigSystem
            GrossSalaryCalculationByFormula.Config_Type = "GrossSalaryCalcByFormula"
            GrossSalaryCalculationByFormula.Config_Value = Convert.ToString(Me.chkGrossSalaryCalc.Checked)
            ConfigList.Add(GrossSalaryCalculationByFormula)

            GrossSalaryFormula = New ConfigSystem
            GrossSalaryFormula.Config_Type = "GrossSalaryFormula"
            GrossSalaryFormula.Config_Value = Convert.ToString(Me.txtGrossSalaryFormula.Text)
            ConfigList.Add(GrossSalaryFormula)


            ''''''''''''''''''
            LoadMultiPO = New ConfigSystem
            LoadMultiPO.Config_Type = "LoadMultiPO"
            LoadMultiPO.Config_Value = Convert.ToString(Me.chkLoadMultiplePO.Checked)
            ConfigList.Add(LoadMultiPO)

            OTWorkingDays = New ConfigSystem
            OTWorkingDays.Config_Type = "OverTimeWorkingDays"
            OTWorkingDays.Config_Value = Convert.ToString(Me.txtOTWorkingDays.Text)
            ConfigList.Add(OTWorkingDays)

            OTSalaryFactorPercentage = New ConfigSystem
            OTSalaryFactorPercentage.Config_Type = "OverTimeSalaryFactorPercentage"
            OTSalaryFactorPercentage.Config_Value = Convert.ToString(Me.txtOTSFPercentage.Text)
            ConfigList.Add(OTSalaryFactorPercentage)

            OTNormalDayMultiplier = New ConfigSystem
            OTNormalDayMultiplier.Config_Type = "OverTimeNormalDayMultiplier"
            OTNormalDayMultiplier.Config_Value = Convert.ToString(Me.txtOTNormalMultiplier.Text)
            ConfigList.Add(OTNormalDayMultiplier)

            OTOffDayMultiplier = New ConfigSystem
            OTOffDayMultiplier.Config_Type = "OverTimeOffDayMultiplier"
            OTOffDayMultiplier.Config_Value = Convert.ToString(Me.txtOTOffMultiplier.Text)
            ConfigList.Add(OTOffDayMultiplier)

            ProFactPercentage = New ConfigSystem
            ProFactPercentage.Config_Type = "ProvidentFactorPercentage"
            ProFactPercentage.Config_Value = Convert.ToString(Me.txtProvidentPercentage.Text)
            ConfigList.Add(ProFactPercentage)

            SalFactPercentage = New ConfigSystem
            SalFactPercentage.Config_Type = "SalaryFactorPercentage"
            SalFactPercentage.Config_Value = Convert.ToString(Me.txtProvidentSFPercentage.Text)
            ConfigList.Add(SalFactPercentage)
            ''03-05-2017
            SalFactPercentage = New ConfigSystem
            SalFactPercentage.Config_Type = "CustomerRepeatedCount"
            SalFactPercentage.Config_Value = Convert.ToString(Me.txtRepeatedCustomerCount.Text)
            ConfigList.Add(SalFactPercentage)
            ''03-05-2017

            VendorDifferenceQty = New ConfigSystem
            VendorDifferenceQty.Config_Type = "VendorDifferenceQty"
            VendorDifferenceQty.Config_Value = Me.cmbVDQty.SelectedValue
            ConfigList.Add(VendorDifferenceQty)

            RetentionAccount = New ConfigSystem
            RetentionAccount.Config_Type = "RetentionAccount"
            RetentionAccount.Config_Value = Me.cmbRetentionAccount.SelectedValue
            ConfigList.Add(RetentionAccount)

            MobilizationAccount = New ConfigSystem
            MobilizationAccount.Config_Type = "MobilizationAccount"
            MobilizationAccount.Config_Value = Me.cmbMobilizationAccount.SelectedValue
            ConfigList.Add(MobilizationAccount)

            ''TASK TFS1927
            WastedStockAccount = New ConfigSystem
            WastedStockAccount.Config_Type = "WastedStockAccount"
            WastedStockAccount.Config_Value = Convert.ToString(Me.cmbWastedStockAccount.SelectedValue)
            ConfigList.Add(CGSAccount)

            ScrappedStockAccount = New ConfigSystem
            ScrappedStockAccount.Config_Type = "ScrappedStockAccount"
            ScrappedStockAccount.Config_Value = Convert.ToString(Me.cmbScrappedStockAccount.SelectedValue)
            ConfigList.Add(ScrappedStockAccount)
            ''END TASK TFS1927
            SalesDiscountAccount = New ConfigSystem
            SalesDiscountAccount.Config_Type = "SaleTaxDeductionAcId"
            SalesDiscountAccount.Config_Value = Convert.ToString(Me.cmbSaleTaxDeductioAcId.SelectedValue)
            ConfigList.Add(SaleTaxDeductionAcId)

            ''Start TFS2236
            ConfigShowMiscAccountsOnSales = New ConfigSystem
            ConfigShowMiscAccountsOnSales.Config_Type = "ShowMiscAccountsOnSales"
            ConfigShowMiscAccountsOnSales.Config_Value = IIf(Me.chkShowMiscAccountsOnSales.Checked = True, "True", "False")
            ConfigList.Add(ConfigShowMiscAccountsOnSales)
            ''END TASK TFS2236

            ''Start TFS2825
            ConfigLoadItemAfterDeliveredOnDC = New ConfigSystem
            ConfigLoadItemAfterDeliveredOnDC.Config_Type = "LoadItemAfterDeliveredOnDC"
            ConfigLoadItemAfterDeliveredOnDC.Config_Value = IIf(Me.chkLoadItemAfterDeliveredOnDC.Checked = True, "True", "False")
            ConfigList.Add(ConfigLoadItemAfterDeliveredOnDC)
            ''END TASK TFS2825

            ''TASK TFS3538
            NewSalarySheetPrint = New ConfigSystem
            NewSalarySheetPrint.Config_Type = "NewSalarySheetPrint"
            NewSalarySheetPrint.Config_Value = Convert.ToString(Me.chkNewSalarySheetPrint.Checked)
            ConfigList.Add(NewSalarySheetPrint)
            '' END TASK TFS3538

        Catch ex As Exception
            ErrorMessage = ex.Message
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            'ConfigDataTable = New SBDal.ConfigSystemDAL().GetAllRecords

            'haseeb..... Edit start
            Me.chkWeighbridgeOnGRN.Checked = Convert.ToBoolean(getConfigValueByType("WeighbridgeGRN").ToString)
            Me.chkWeighbridgePurchase.Checked = Convert.ToBoolean(getConfigValueByType("WeighbridgePurchase").ToString)
            Me.chkWeighbridgeDC.Checked = Convert.ToBoolean(getConfigValueByType("WeighbridgeDC").ToString)
            Me.chkWeighbridgeSaleOrder.Checked = Convert.ToBoolean(getConfigValueByType("WeighbridgeSaleOrder").ToString)
            'end

            Me.cmbSalesAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesCreditAccount").ToString))
            Me.cmbDefaultAccountInPlaceCustomer.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("DefaultAccountInPlaceCustomer").ToString))
            Me.cmbMainAccountforRevenueImport.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("MainAccountforRevenueImport").ToString))
            Me.cmbPurchaseAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseDebitAccount").ToString))
            Me.cmbStoreIssuanceAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("StoreIssuenceAccount").ToString))
            Me.cmbSalesTaxAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesTaxCreditAccount").ToString))
            Me.cmbPurchaseTaxAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseTaxDebitAccountId").ToString))
            Me.CmbPurchaseTaxIDeductionAccountNo.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseTaxDeductionAcId").ToString)) ''TFS2221
            Me.cmbTaxReceiveableAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("TaxreceiveableACid").ToString))
            Me.cmbTaxPayableAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("taxpayableACid").ToString))
            Me.cmbFuelExpAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("FuelExpAccount").ToString))
            Me.cmbAdjustmentExpAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AdjustmentExpAccount").ToString))
            Me.cmbOtherExpAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("OtherExpAccount").ToString))
            Me.cmbTransitInsuranceAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("TransitInsuranceAccountId").ToString))
            Me.cmbVoucherFormat.Text = getConfigValueByType("VoucherNo").ToString
            Me.chkShowVendorOnSales.Checked = IIf(getConfigValueByType("Show Vendor On Sales") = "False", 0, 1)
            Me.chkShowCustomerOnPurchase.Checked = IIf(getConfigValueByType("Show Customer On Purchase") = "False", 0, 1)
            Me.chkPOPrintAfterApproval.Checked = IIf(getConfigValueByType("POPrintAfterApproval") = "False", 0, 1) ''TFS2377
            Me.chkPurchaseAllowedWithPO.Checked = IIf(getConfigValueByType("PurchaseAllowedWithPO") = "False", 0, 1)
            Me.txtCompanyName.Text = getConfigValueByType("CompanyNameHeader").ToString
            Me.txtCompanyAddress.Text = getConfigValueByType("CompanyAddressHeader").ToString
            Me.chkShowCompanyAddress.Checked = IIf(getConfigValueByType("ShowCompanyAddressOnPageHeader") = "True", 1, 0)
            Me.cmbSalaryAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalariesAccountId").ToString))
            Me.cmbSalaryPayableAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalariesPayableAccountId").ToString))
            Me.dtpEndOfDate.Value = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
            'Me.chkFastPrinting.Checked = Convert.ToBoolean(getConfigValueByType("FastPrinting").ToString)
            Me.chkItemFilterByLocation.Checked = Convert.ToBoolean(getConfigValueByType("ArticleFilterByLocation").ToString)
            Me.chkStockViewOnSales.Checked = Convert.ToBoolean(getConfigValueByType("StockViewOnSale").ToString)
            Me.chkAllowMinusStock.Checked = Convert.ToBoolean(getConfigValueByType("AllowMinusStock").ToString)
            Me.chkStockDispatchTransfer.Checked = Convert.ToBoolean(getConfigValueByType("StockTransferFromDispatch").ToString)
            Me.chkStoreIssuenceWithProduction.Checked = Convert.ToBoolean(getConfigValueByType("StoreIssuenceWithProduction").ToString)
            Me.chkLoadItemGridSales.Checked = Convert.ToBoolean(getConfigValueByType("LoadAllItemsInSales").ToString)
            Me.cmbPLAccountId.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PLAccountId").ToString))
            Me.cmbExchangeGainLossAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ExchangeGainLossAccount").ToString))
            Me.chkPreviouseRecordShow.Checked = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
            Me.chkChangeDocumentNo.Checked = Convert.ToBoolean(getConfigValueByType("ChangeDocNo").ToString)
            Me.chkShowMasterGrid.Checked = Convert.ToBoolean(getConfigValueByType("ShowMasterGrid").ToString)
            Me.chkNewSecurityRights.Checked = Convert.ToBoolean(getConfigValueByType("NewSecurityRights").ToString)
            Me.chkExpiryDate.Checked = Convert.ToBoolean(getConfigValueByType("ItemExpiryDateOnPurchase").ToString)
            Me.cmbSEDAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SEDAccountId").ToString))
            Me.chkSIRIUSPartner.Checked = Convert.ToBoolean(getConfigValueByType("SoftbeatsPartner").ToString)
            Me.txtPartnerName.Text = Convert.ToString(getConfigValueByType("SoftbeatsPartnerName").ToString)
            Me.chkReceiptVoucherOnSales.Checked = Convert.ToBoolean(getConfigValueByType("ReceiptVoucherOnSales").ToString)
            Me.chkEmailAlert.Checked = Convert.ToBoolean(getConfigValueByType("EmailAlert").ToString)
            Me.cmbDefaultEmail.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("DefaultEmailId").ToString))
            Me.txtAdminEmail.Text = getConfigValueByType("AdminEmailId").ToString
            Me.chkServiceItem.Checked = Convert.ToBoolean(getConfigValueByType("ServiceItem").ToString)
            Me.nudDefaultReminder.Value = Convert.ToInt32(Val(getConfigValueByType("DefaultReminder").ToString))
            Me.nudSMSScheduleTime.Value = Convert.ToInt32(Val(getConfigValueByType("SMSScheduleTime").ToString))
            Me.chkPaymentVoucherOnPurchase.Checked = Convert.ToBoolean(getConfigValueByType("PaymentVoucherOnPurchase").ToString)
            Me.chkStockDispatchOnProduction.Checked = Convert.ToBoolean(getConfigValueByType("StockDispatchOnProduction").ToString)
            Me.chkPreviewInvoice.Checked = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
            Me.chkSalesOrderAnalysis.Checked = Convert.ToBoolean(getConfigValueByType("SalesOrderAnalysis").ToString)
            Me.chkDiscountVoucher.Checked = Convert.ToBoolean(getConfigValueByType("DiscountVoucherOnSale").ToString)
            Me.cmbSalesDiscAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesDiscountAccount").ToString))
            Me.chkAttachments.Checked = Convert.ToBoolean(getConfigValueByType("EmailAttachment").ToString)
            Me.txtFileExportPath.Text = Convert.ToString(getConfigValueByType("FileExportPath").ToString)
            Me.chkAutoUpdate.Checked = Convert.ToBoolean(getConfigValueByType("EnableAutoUpdate").ToString.Replace("Error", "True"))
            Me.txtTransitInssuranceTax.Text = getConfigValueByType("TransitInssuranceTax").ToString
            Me.txtWHTax.Text = getConfigValueByType("WHTax_Percentage").ToString
            Me.txtDefaultTax.Text = getConfigValueByType("Default_Tax_Percentage").ToString
            Me.txtBackupDBPath.Text = getConfigValueByType("BackupDBPath").ToString
            Me.txtEmployeePicturePath.Text = getConfigValueByType("EmployeePicturePath").ToString
            Me.txtArticlePicturePath.Text = getConfigValueByType("ArticlePicturePath").ToString
            Me.chkAccountHeadReadonly.Checked = Convert.ToBoolean(getConfigValueByType("AccountHeadReadOnly").ToString)
            Me.chkMarketReturnVoucher.Checked = Convert.ToBoolean(getConfigValueByType("MarketReturnVoucher").ToString)
            Me.chkCompanyPrefix.Checked = Convert.ToBoolean(getConfigValueByType("Company-Based-Prefix").ToString)
            Me.chkArticleShowImage.Checked = Convert.ToBoolean(getConfigValueByType("ArticleShowImageOnStoreIssuance").ToString)
            Me.txtAssetPicturePath.Text = getConfigValueByType("AssetPicturePath").ToString
            Me.chkMultipleSalesOrder.Checked = Convert.ToBoolean(getConfigValueByType("MultipleSalesOrder").ToString)
            Me.chkProductionVoucher.Checked = Convert.ToBoolean(getConfigValueByType("ProductionVoucher").ToString)
            Me.cmbInwardExpenseHeadAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("InwardExpHeadAcId").ToString))
            Me.chkCurrency.Checked = Convert.ToBoolean(getConfigValueByType("CurrencyonOpenLC").ToString)
            Me.chkExpenseChargeToCustomer.Checked = Convert.ToBoolean(getConfigValueByType("ExpenseChargeToCustomer").ToString)
            Me.chkReminder.Checked = Convert.ToBoolean(getConfigValueByType("Reminder").ToString)
            Me.chkInvoiceWiseTax.Checked = Convert.ToBoolean(getConfigValueByType("InvoiceWiseTaxPercent").ToString)
            Me.chkUserCompanyRights.Checked = Convert.ToBoolean(getConfigValueByType("CompanyRights").ToString)
            Me.chkMenuRights.Checked = Convert.ToBoolean(getConfigValueByType("MenuRights").ToString)
            Me.cmbCostOfProduction.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("StoreCreditAccount").ToString))
            Me.chkCostofsalevoucher.Checked = Convert.ToBoolean(getConfigValueByType("CGSVoucher").ToString)
            Me.chkShowVendorOnDeliveryChalan.Checked = Convert.ToBoolean(getConfigValueByType("ShowVendorOnDeliveryChalan").ToString)
            Me.cmbCGS.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CGSAccountId").ToString))
            Me.cmbInvAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("InvAccountId").ToString))
            Me.chkStoreIssuenceVoucher.Checked = Convert.ToBoolean(getConfigValueByType("StoreIssuenceVoucher").ToString)
            Me.chkStoreIssuanceDependonProductionPlan.Checked = Convert.ToBoolean(getConfigValueByType("StoreIssuaneDependonProductionPlan").ToString)
            Me.chkSalemanVoucherPrintLog.Checked = Convert.ToBoolean(getConfigValueByType("PrintLog").ToString)
            Me.txtSalemanVoucherPrintCount.Text = Convert.ToInt32(Val(getConfigValueByType("PrintCount").ToString))
            '20-July-2017: Task TFS1084: Waqar Raza: Added for sales history load quntity.
            'Start Task:
            Me.txtHistoryLoadQuantity.Text = Convert.ToInt32(Val(getConfigValueByType("SalesHistoryLoadQuantity").ToString))
            'End Task
            '09-Jan-2018: Task TFS2075: Waqar Raza: Added for showing Conversion Title on Purchase and Purchase Order.
            'Start Task:
            Me.txtConversionTitle.Text = IIf(getConfigValueByType("ConversionTitle").ToString = "NULL", "", getConfigValueByType("ConversionTitle").ToString)
            Me.txtConversionFactor.Text = Val(getConfigValueByType("ConversionFactor").ToString)
            'End Task
            Me.chkAttendanceBaseSalary.Checked = Convert.ToBoolean(getConfigValueByType("AttendanceBasedSalary").ToString)
            Me.txtWorkingDays.Text = Convert.ToInt32(Val(getConfigValueByType("Working_Days").ToString))
            Me.txtVendorQuotationDocPrefix.Text = IIf(getConfigValueByType("VendorQuotation").ToString = "NULL", "", getConfigValueByType("VendorQuotation").ToString)
            Me.chkChequeDetailEnable.Checked = Convert.ToBoolean(getConfigValueByType("EnableChequeDetailOnVoucherEntry").ToString)
            Me.chkTransporterCharges.Checked = Convert.ToBoolean(getConfigValueByType("TransaportationChargesVoucher").ToString)
            Me.chkAvgRate.Checked = Convert.ToBoolean(getConfigValueByType("AvgRate").ToString)
            Me.txtAttendanceDbPath.Text = getConfigValueByType("AlternateAttendanceDBPath").ToString
            Me.chkBarcodeEnabled.Checked = Convert.ToBoolean(getConfigValueByType("BarcodeEnabled").ToString)
            Me.chkTaxExcludePrice.Checked = Convert.ToBoolean(getConfigValueByType("ExcludeTaxPrice").ToString)
            Me.chkGrossSalaryCalc.Checked = Convert.ToBoolean(getConfigValueByType("GrossSalaryCalcByFormula").ToString)
            Me.txtGrossSalaryFormula.Text = getConfigValueByType("GrossSalaryFormula").ToString
            Me.cmbCylinderStockAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CylinderStockAccount").ToString))
            If Not getConfigValueByType("CostSheetType").ToString = "Error" Then
                Me.cmbCostSheetType.Text = getConfigValueByType("CostSheetType").ToString
            End If
            ''Start Task: TFS1596 : Ayesha Rehman: Added to Retain the last values added
            If Not getConfigValueByType("StockInConfigration").ToString = "Error" Then
                Me.cmbStockIn.Text = getConfigValueByType("StockInConfigration").ToString
            End If
            If Not getConfigValueByType("StockOutConfigration").ToString = "Error" Then
                Me.cmbStockOut.Text = getConfigValueByType("StockOutConfigration").ToString
            End If
            ''End TFS1596
            Me.chkClinderVoucher.Checked = Convert.ToBoolean(getConfigValueByType("CylinderVoucher").ToString)
            Me.chkAutoChequebook.Checked = Convert.ToBoolean(getConfigValueByType("EnableAutoChequeBook").ToString)
            Me.chkGLAccountArticleDepartment.Checked = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment").ToString)
            Me.chkOnetimesalereturn.Checked = Convert.ToBoolean(getConfigValueByType("OnetimeSalesReturn").ToString) 'R-919 Retrive OnetimeSalesretun's value
            Me.chkAutoLoadPO.Checked = Convert.ToBoolean(getConfigValueByType("AutoLoadPO").ToString) 'R-979 Retrive Auto Load PO's value
            'Task:2369 Comment Layout Configuration's Values Retrieve
            Me.chkCommentCustomerFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentCustomerFormat").ToString)
            Me.chkCommentArticleDescriptionFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentArticleFormat").ToString)
            Me.chkCommentArticleSizeFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentArticleSizeFormat").ToString)
            Me.chkCommentColorFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentArticleColorFormat").ToString)
            Me.chkCommentQtyFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentQtyFormat").ToString)
            Me.chkCommentPriceFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentPriceFormat").ToString)
            Me.chkCommentsRemarksFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentRemarksFormat").ToString)
            'End Task:2369
            'Task:2376 Comment Layout Configuration's Values Retrieve
            Me.chkCommentsVendorFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentVendorFormat").ToString)
            Me.chkPurchaseCommentArticleDescriptionFormat.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseCommentArticleFormat").ToString)
            Me.chkPurchaseCommentArticleSizeFormat.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseCommentArticleSizeFormat").ToString)
            Me.chkPurchaseCommentArticleColorFormat.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseCommentArticleColorFormat").ToString)
            Me.chkPurchaseCommentQtyFormat.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseCommentQtyFormat").ToString)
            Me.chkPurchaseCommentPriceFormat.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseCommentPriceFormat").ToString)
            Me.chkPurchaseCommentRemarksFormat.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseCommentRemarksFormat").ToString)
            'End Task:2376
            Me.txtFilesAttachmentPath.Text = getConfigValueByType("FileAttachmentPath").ToString 'Task:2400 Attach Multi Files In Voucher Entry
            Me.chkVehicleIdentificationInfo.Checked = Convert.ToBoolean(getConfigValueByType("flgVehicleIdentificationInfo").ToString) 'Task:M16 Get Vehicle Identification Info Value
            Me.chkMargeItem.Checked = Convert.ToBoolean(getConfigValueByType("flgMargeItem").ToString) ''20-Feb-2014   TASK:2432 Imran Ali 1 Delivery Chalan Multi Record of Same Item   
            Me.chkCommentInvoiceNo.Checked = Convert.ToBoolean(getConfigValueByType("CommentsInvNoOnPurchase").ToString) ''26-Feb-2014  Task:2442   Imran Ali   4-bill no. DC no. shown on ledger report of party.
            Me.chkCommentsDcNo.Checked = Convert.ToBoolean(getConfigValueByType("CommentsDCNoOnPurchase").ToString) ''26-Feb-2014  Task:2442   Imran Ali   4-bill no. DC no. shown on ledger report of party.
            Me.chkCommentSalesDCNo.Checked = Convert.ToBoolean(getConfigValueByType("CommentSalesDCNo").ToString) ''Task:2446 Retrive Data Comment Sales DC No
            Me.chkCommentEngineNo.Checked = Convert.ToBoolean(getConfigValueByType("CommentEngineNo").ToString) ''Task:2446 Retrive Data Comment Sales Engine No
            ''03-Mar-2014 TASK:2451  Imran Ali  4-ALPHABETIC order of items in sale and purchase window 
            Me.rbtItemSortOrder.Checked = Convert.ToBoolean(getConfigValueByType("ItemSortOrder").ToString)
            Me.rbtItemSortOrderByCode.Checked = Convert.ToBoolean(getConfigValueByType("ItemSortOrderByCode").ToString)
            Me.rbtItemSortOrderByName.Checked = Convert.ToBoolean(getConfigValueByType("ItemSortOrderByName").ToString)
            Me.chkItemAscending.Checked = Convert.ToBoolean(getConfigValueByType("ItemAscending").ToString)
            Me.chkItemDescending.Checked = Convert.ToBoolean(getConfigValueByType("ItemDescending").ToString)

            Me.rbtAcSortOrder.Checked = Convert.ToBoolean(getConfigValueByType("AcSortOrder").ToString)
            Me.rbtAcSortOrderByCode.Checked = Convert.ToBoolean(getConfigValueByType("AcSortOrderByCode").ToString)
            Me.rbtAcSortOrderByName.Checked = Convert.ToBoolean(getConfigValueByType("AcSortOrderByName").ToString)
            Me.chkAcAscending.Checked = Convert.ToBoolean(getConfigValueByType("AcAscending").ToString)
            Me.chkAcDescending.Checked = Convert.ToBoolean(getConfigValueByType("AcDescending").ToString)
            ''13-Mar-2014 TASK:2488 Imran Ali Sales Certificate In ERP
            Me.txtSaleCertificatePrefix.Text = getConfigValueByType("SaleCertificatePreFix").ToString
            'End Task:2488
            'End Task:2451

            'Task:2576 Get Enabled Branded SMS And Mask Value
            Me.chkBrandedSMS.Checked = Convert.ToBoolean(getConfigValueByType("EnabledBrandedSMS").ToString)
            Me.txtBrandedSMSMask.Text = Decrypt(getConfigValueByType("BrandedSMSMask").ToString)
            Me.txtBrandedSMSUser.Text = getConfigValueByType("BrandedSMSUser").ToString
            Me.txtBrandedSMSPassword.Text = Decrypt(getConfigValueByType("BrandedSMSPassword").ToString)
            'End Task:2576

            'Task:2595 Get Default Working Hours Configuration
            Me.nudDefaultWorkingHours.Value = Val(getConfigValueByType("DefaultWorkingHours").ToString)
            Me.nudLeaveEncashment.Value = Val(getConfigValueByType("LeaveEncashment").ToString)
            'Task No 2608  append the new two lines of code for new configration
            Me.nudAmount.Value = Val(getConfigValueByType("DecimalPointInValue").ToString)
            Me.nudQty.Value = Val(getConfigValueByType("DecimalPointInQty").ToString)
            'End Task 2608
            Me.cmbClaimAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ClaimAccountId").ToString)) 'Task:2660 Get Claim Account Id
            'Task:2701 Retrive CMFA Exp Account Head
            Me.cmbCMFAExpAccountHead.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CMFAExpAccountHead").ToString))
            ''TASK-407
            'If Not getConfigValueByType("Currency").ToString.ToUpper = "NULL" AndAlso Not getConfigValueByType("Currency").ToString = "" AndAlso Not getConfigValueByType("Currency").ToString.ToUpper = "ERROR" Then
            If IsCurrencyTransaction() = True Then
                Me.cmbCurrency.Enabled = False
                Me.cmbCurrency.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("Currency").ToString))
            Else
                Me.cmbCurrency.Enabled = True

            End If
            ''End TASK-407

            'End Task:2701
            'Task:2702 Get CMFADocuent Config On/Off
            Me.chkCMFADocumentOnSales.Checked = Convert.ToBoolean(getConfigValueByType("CMFADocumentOnSales").ToString)
            'end Task:2702
            'Task:2703 Get CMFADocumentAttachPath
            Me.txtCMFADocumentAttachmentPath.Text = getConfigValueByType("CMFADocumentAttachmentPath").ToString
            'End Task:2703

            'Task:2728 Get Configs CommentsChequeNo,CommentsChequeDate,CommentsPartyName
            Me.chkCommentsChequeNo.Checked = Convert.ToBoolean(getConfigValueByType("CommentsChequeNo").ToString)
            Me.chkCommentsChequeDate.Checked = Convert.ToBoolean(getConfigValueByType("CommentsChequeDate").ToString)
            Me.chkCommentsPartyName.Checked = Convert.ToBoolean(getConfigValueByType("CommentsPartyName").ToString)
            'End Task:2728

            ''Start TFS3520 : Ayesha Rehman : 13-06-2018 : Get Configs for SeparateClosureOfSODC
            Me.chkSeparateClosureOfSODC.Checked = Convert.ToBoolean(getConfigValueByType("SeparateClosureOfSODC").ToString)
            ''End TFS3520

            'Task#03082015 (Ahmad Sharif)
            Me.chkAttendanceEmailAlert.Checked = Convert.ToBoolean(getConfigValueByType("EnabledAttendanceEmailAlert").ToString)
            Me.chkCompanyWisePrefixOnVoucher.Checked = Convert.ToBoolean(getConfigValueByType("CompanyWisePrefix").ToString)
            'End Task#03082015
            ''Start TFS3330 : Ayesha Rehman :  Config Value of 'ApplyFlatDiscountOnSale' was Missing , not get before
            Me.chkApplyFlatDiscountOnSale.Checked = Convert.ToBoolean(getConfigValueByType("ApplyFlatDiscountOnSale").ToString)
            ''End TFS3330
            'Task 3291 Saad Afzaal get config AllowChangePO on load and refresh button'

            Me.chkAllowChangePO.Checked = Convert.ToBoolean(getConfigValueByType("AllowChangePO").ToString.Replace("Error", "False").Replace("''", "False"))

            'Task#08082015 (Ahmad Sharif)
            Me.chkSMSWithEngineNo.Checked = Convert.ToBoolean(getConfigValueByType("DeliveryChalanByEnigneNo").ToString)
            'End Task#08082015

            'Task:2762 Get Config Total Amount rounding
            Me.nudTotalAmountRounding.Value = Val(getConfigValueByType("TotalAmountRounding").ToString)
            'End TAsk:2762
            ''03-Aug-2014 Task:2767 Imran Ali Change Unit Option On Sales (ZR Traders)
            Me.chkLoadAllItemPackQty.Checked = Convert.ToBoolean(getConfigValueByType("LoadAllItemPackQty").ToString)
            'End Task:2767
            Me.chkPurchaseAccountFrontEnd.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseAccountFrontEnd").ToString) 'Task:2784 Get Value Purchase Account Front End 

            'Task:2795 Get Configuration Order Qty Exceed
            Me.chkOrderQtyExceedDeliveryChalan.Checked = Convert.ToBoolean(getConfigValueByType("OrderQtyExceedAgainstDeliveryChalan").ToString)
            Me.chkOrderQtyExceedSales.Checked = Convert.ToBoolean(getConfigValueByType("OrderQtyExceedAgainstSales").ToString)
            Me.chkDCQtyExceedSales.Checked = Convert.ToBoolean(getConfigValueByType("DCQtyExceedAgainstSales").ToString)
            'End Task:2795
            ''19-Sep-2014 Taks:2847 Imran Ali Total Amount Wise Invoice Based Voucher
            Me.chkTotalAmountWiseInvoiceBasedVoucher.Checked = Convert.ToBoolean(getConfigValueByType("TotalAmountWiseInvoiceBasedVoucher").ToString)
            'End Task:2847
            Me.chkCGAccountOnStoreIssuance.Checked = Convert.ToBoolean(getConfigValueByType("CGAccountOnStoreIssuance").ToString)
            Me.chkActivityLogShow.Checked = Convert.ToBoolean(getConfigValueByType("ActivityLogShowOnHomePage").ToString)

            'Add Print Checkbox value Task#20150503
            Me.ChkDirectPrinting.Checked = Convert.ToBoolean(getConfigValueByType("Print").ToString)
            'Add Print Checkbox value Task#20150503
            Me.chkDCDetailLocationSMS.Checked = Convert.ToBoolean(getConfigValueByType("DCDetailLocationSMS").ToString)
            If getConfigValueByType("DNSHostForSMS").ToString <> "Error" Then
                Me.txtDNSHost.Text = getConfigValueByType("DNSHostForSMS").ToString
                'Else
                '    Me.txtDNSHost.Text = System.Net.Dns.GetHostName.ToString
            End If
            Me.chkCheckCurrentStockByItem.Checked = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)
            Me.chkCheckCurrentStockByLocation.Checked = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByLocation").ToString)
            Me.chkAutoArticleCode.Checked = Convert.ToBoolean(getConfigValueByType("chkAutoArticleCode").ToString)
            Me.chkUpdatePostedVoucher.Checked = Convert.ToBoolean(getConfigValueByType("UpdatePostedVoucher").ToString)
            Me.cmbAdditionalCostAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AdditionalCostAccountId").ToString))
            Me.cmbCustomDutyAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CustomDutyAccountId").ToString))
            Me.cmbLCSalesTaxAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("LCSalesTaxAccountId").ToString))
            Me.cmbAdditionalSalesTaxAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AdditionalSalesTaxAccountId").ToString))
            Me.cmbAdvanceIncomeTaxAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AdvanceIncomeTaxAccountId").ToString))
            Me.cmbExciseDutyAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ExciseDutyAccountId").ToString))
            Me.txtAdminMobile.Text = getConfigValueByType("AdminMobileNo").ToString
            strAdminMobileNo = Me.txtAdminMobile.Text
            Me.chkAutoBreakAttendance.Checked = Convert.ToBoolean(getConfigValueByType("AutoBreakAttendance").ToString)

            Me.chkBiltyNo.Checked = Convert.ToBoolean(getConfigValueByType("BiltyCommentsSales").ToString)
            Me.chkTransporter.Checked = Convert.ToBoolean(getConfigValueByType("TransporterCommentsSales").ToString)
            Me.chkDeductionWHTaxOnTotal.Checked = Convert.ToBoolean(getConfigValueByType("DeductionWHTaxOnTotal").ToString)
            Me.chkCashBankOptionDetail.Checked = Convert.ToBoolean(getConfigValueByType("CashAccountOptionForDetail").ToString)

            Me.cmbEmployeeHeadAccountId.SelectedValue = Val(getConfigValueByType("EmployeeHeadAccountId").ToString)
            Me.rbtSimpleEmpAc.Checked = Convert.ToBoolean(getConfigValueByType("EmpSimpleAccountHead").ToString)
            Me.rbtDeptEmpAc.Checked = Convert.ToBoolean(getConfigValueByType("EmpDepartmentAccountHead").ToString)
            Me.cmbEmpDeptHeadAccountId.SelectedValue = Val(getConfigValueByType("EmployeeDeptHeadAccountId").ToString)
            Me.ndArticleCodeLength.Value = Val(getConfigValueByType("ArticleCodePrefixLength").ToString)
            If Me.rbtSimpleEmpAc.Checked = True Then
                Me.lblAccountLevel.Text = "Sub Sub Account From Chart of Account"
                Me.cmbEmpDeptHeadAccountId.Visible = True
                Me.cmbEmpDeptHeadAccountId.Visible = False
            ElseIf rbtDeptEmpAc.Checked = True Then
                Me.lblAccountLevel.Text = "Sub Account From Chart of Account"
                Me.cmbEmpDeptHeadAccountId.Visible = False
                Me.cmbEmpDeptHeadAccountId.Visible = True
            Else
                Me.lblAccountLevel.Text = "Sub Sub Account From Chart of Account"
                Me.cmbEmpDeptHeadAccountId.Visible = True
                Me.cmbEmpDeptHeadAccountId.Visible = False
            End If

            'Task#21082015 by Ahmad Sharif
            Me.chkApply40KgPackRate.Checked = Convert.ToBoolean(getConfigValueByType("Apply40KgRate").ToString)
            'End Task#21082015
            ''Added on 24-03-2017
            Me.chkAssociateItems.Checked = Convert.ToBoolean(getConfigValueByType("AssociateItems").ToString)
            ''End

            ''TFS2737  :Ayesha Rehman : 19-03-2018
            Me.chkCostImplementationLotWiseOnStockMovement.Checked = Convert.ToBoolean(getConfigValueByType("CostImplementationLotWiseOnStockMovement").ToString)
            ''End

            Me.ChkDateWiseAverageRate.Checked = Convert.ToBoolean(getConfigValueByType("DateWiseAverageRate").ToString)

            Me.rbtListSearchStartWith.Checked = Convert.ToBoolean(getConfigValueByType("ListSearchStartWith"))
            Me.rbtListSearchContains.Checked = Convert.ToBoolean(getConfigValueByType("ListSearchContains"))
            Me.chkAllowPaymentZeroBalance.Checked = Convert.ToBoolean(getConfigValueByType("AllowPaymentZeroBalance"))
            'Altered Against Task#201507004 to Get Values of Start Period and Total Leaves Days Ali Ansari
            If GetConfigValue("Attendance_Period").ToString = "" Then
                DtpStartDate.Value = Date.Now
                DtpStartDate.Checked = False
            Else
                Me.DtpStartDate.Value = Convert.ToDateTime(getConfigValueByType("Attendance_Period").ToString)
            End If
            Me.TxtTotalLeaves.Text = Val(getConfigValueByType("Leave_Days").ToString)
            'Altered Against Task#201507004 to Get Values of Start Period and Total Leaves Days Ali Ansari

            Me.chkLoadMultiplePO.Checked = Convert.ToBoolean(getConfigValueByType("LoadMultiPO"))

            'Task#23072015 Add Email Alert Due Invoice Configuration
            Me.chkEmailAlertDueInvoice.Checked = Convert.ToBoolean(getConfigValueByType("EmailAlertDueInvoice"))

            If getConfigValueByType("DayOff").ToString <> "Error" Then
                Dim str() As String = getConfigValueByType("DayOff").ToString.Split(",")
                If str.Length > 0 Then
                    For Each obj As String In str
                        For i As Integer = 0 To Me.cmbDayOff.Items.Count - 1
                            If Me.cmbDayOff.Items(i).ToString = obj.ToString Then
                                Me.cmbDayOff.SetItemChecked(i, True)
                            End If
                        Next
                    Next
                End If
            End If

            'End Task#23072015
            'Task#16092015 display setting of config
            Me.chkUserwiseCompany.Checked = Convert.ToBoolean(getConfigValueByType("UserwiseCompany"))
            Me.chkUserwiseLocation.Checked = Convert.ToBoolean(getConfigValueByType("UserwiseLocation"))
            'End Task#16092015
            Me.chkEnabledDuplicateVoucherLog.Checked = Convert.ToBoolean(getConfigValueByType("EnabledDuplicateVoucherLog"))
            'TASK-TFS-51  get Additional Tax Account Id
            Me.cmbAdditionalTaxAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AdditionalTaxAcId").ToString))
            'Ameen
            _CompanyLogoPath = getConfigValueByType("CompanyLogoConfiguration")

            Dim dt As DataTable = GetPicture("CompanyLogoConfiguration")

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    If IO.File.Exists(dt.Rows.Item(0).Item(1).ToString) Then
                        dt.Rows.Item(0).BeginEdit()
                        LoadPicture(dt.Rows.Item(0), "LogoImage", dt.Rows.Item(0).Item(1).ToString)
                        dt.Rows.Item(0).EndEdit()
                        dt.AcceptChanges()
                        'Dim bytes() As Byte = dt.Rows.Item(0).Item(3)
                        'Dim ms As New MemoryStream(bytes.Length)
                        'ms.Write(bytes, 0, bytes.Length)
                        'Me.pbCompanyLogo.Image = Image.FromStream(ms)
                        Me.pbCompanyLogo.ImageLocation = dt.Rows.Item(0).Item(1).ToString
                        Me.pbCompanyLogo.Update()

                    Else
                        Me.pbCompanyLogo.Image = Nothing
                    End If
                Else
                    Me.pbCompanyLogo.Image = Nothing
                End If
            End If


            'End TASK-TFS-51
            Me.chkApplyCostSheetRateOnProduction.Checked = Convert.ToBoolean(getConfigValueByType("ApplyCostSheetRateOnProduction").ToString.Replace("Error", "False").Replace("''", "False"))
            Me.chkLoadMultiChalanOnSale.Checked = Convert.ToBoolean(getConfigValueByType("LoadMultiChalanOnSale").ToString.Replace("Error", "False").Replace("''", "False"))
            Me.chkFreezColumn.Checked = Convert.ToBoolean(getConfigValueByType("GridFreezColumn").Replace("Error", "False").Replace("''", "False"))
            Me.cmbSMSLanguage.Text = getConfigValueByType("SMSLanguage").ToString.Replace("Error", "English").Replace("''", "English")
            Me.chkApplyDefaultWorkingHoursOnOverTime.Checked = Convert.ToBoolean(getConfigValueByType("ApplyDefaultWorkingHoursOnOverTime").ToString.Replace("Error", "False").Replace("''", "False"))
            Me.chkAllowAddZeroPriceOnPurchase.Checked = Convert.ToBoolean(getConfigValueByType("AllowAddZeroPriceOnPurchase").ToString.Replace("Error", "False").Replace("''", "False"))
            Me.chkAllowBelowRetailPrice.Checked = Convert.ToBoolean(getConfigValueByType("AllowBelowRetailPrice").ToString.Replace("''", "False").Replace("Error", "False"))

            'Ameen
            Me.txtBacklocation.Text = getConfigValueByType("DatabaseBackup").ToString
            Dim cbValues As String = String.Empty
            Dim day As String = String.Empty
            Dim suitableTime As String = String.Empty
            cbValues = getConfigValueByType("BackupScheduleDays").ToString()
            If cbValues.Length > 0 Then
                Dim arday() As String = cbValues.Split("|")
                If arday.Length > 5 Then
                    'Mon&False|Tue&True|Wed&True|Thu&True|Fri&True|Sat&True|Sun&True
                    cbMon.Checked = Convert.ToBoolean(arday(0).Trim.Substring(4))
                    cbTue.Checked = Convert.ToBoolean(arday(1).Trim.Substring(4))
                    cbWed.Checked = Convert.ToBoolean(arday(2).Trim.Substring(4))
                    cbThu.Checked = Convert.ToBoolean(arday(3).Trim.Substring(4))
                    cbFri.Checked = Convert.ToBoolean(arday(4).Trim.Substring(4))
                    cbSta.Checked = Convert.ToBoolean(arday(5).Trim.Substring(4))
                    cbSun.Checked = Convert.ToBoolean(arday(6).Trim.Substring(4))
                End If
                suitableTime = getConfigValueByType("BackupSuitableTime").ToString()
            End If
            If suitableTime.Length > 0 Then
                If suitableTime.StartsWith("Any") Then
                    rbAnyTime.Checked = True
                    'dtpStartat.Value = CType(suitableTime.Substring(3), Date)
                Else
                    Dim specificTime() As String = suitableTime.Split("|")
                    If specificTime.Length > 0 Then
                        dtpStartat.Value = Convert.ToDateTime(specificTime(0).Trim.Substring(3).ToString())  'CType(specificTime(0).Trim.Substring(2), Date)
                        dtpEndat.Value = Convert.ToDateTime(specificTime(1).Trim.Substring(0).ToString())  'CType(specificTime(1).Trim.Substring(2), Date)
                        rbSpecificTime.Checked = True
                    End If
                End If
            End If
            'End by Ameen
            'End Task#23072015
            'Task#16092015 display setting of config
            chkEnabledSizeCombinationCodeArticleCode.Checked = Convert.ToBoolean(getConfigValueByType("EnabledSizeCombinationCodeOnArticleCode").ToString.Replace("Error", "False").Replace("''", "False"))
            chkApplyAdjustmentFuelExpTotal.Checked = Convert.ToBoolean(getConfigValueByType("ApplyAdjustmentFuelExpTotal").ToString.Replace("Error", "False").Replace("''", "False"))
            Me.chkEnableDuplicateQuotation.Checked = Convert.ToBoolean(getConfigValueByType("EnableDuplicateQuotation").ToString)
            Me.chkEnableDuplicateSalesOrder.Checked = Convert.ToBoolean(getConfigValueByType("EnableDuplicateSalesOrder").ToString)
            Me.chkWeightWiseImport.Checked = Convert.ToBoolean(getConfigValueByType("ImportWieghtWiseCalculation").ToString)
            'TFS1859:Rai Haider:Added Check to allow duplicate mobile no in leads Information 
            If Not getConfigValueByType("DuplicateMobileInLeadsInfo").ToString = "Error" Then
                Me.chkDuplicateMobile.Checked = Convert.ToBoolean(getConfigValueByType("DuplicateMobileInLeadsInfo").ToString)
            End If
            'End TFS1859:06-Dec-17
            If Not getConfigValueByType("AllDispatchLocations").ToString = "Error" Then
                Me.chkAllDispatchLocations.Checked = Convert.ToBoolean(getConfigValueByType("AllDispatchLocations").ToString)
            End If
            If Not Me.chkHelpnSupport Is Nothing Then
                Me.chkHelpnSupport.Checked = Convert.ToBoolean(getConfigValueByType("HelpnSupportPanel").ToString)
            End If
            If Not Me.chkRackonSalesItemLoad Is Nothing Then
                Me.chkRackonSalesItemLoad.Checked = Convert.ToBoolean(getConfigValueByType("RackonSalesItemLoad").ToString)
            End If
            '12-July-2017: Task # TFS1042: Waqar Raza: Added to Check Name Radio Button on Sales Item Drop Down for HFR.
            'Start TSF1042:
            If Not Me.chkItemFilterByName Is Nothing Then
                Me.chkItemFilterByName.Checked = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            'Énd TSF1042:
            '25-July-2017 TFS1045 : Waqar Raza: To Check If checkbox is checked then show Customer On User Wise for FHR
            'Start TFS1045
            If Not Me.chkUserWiseCustomer Is Nothing Then
                Me.chkUserWiseCustomer.Checked = Convert.ToBoolean(getConfigValueByType("UserWiseCustomer").ToString)
            End If
            'End TFS1045

            '30-May-2018 TFS2211 : Waqar Raza: Added this configuration to Adjust Bardana on POS Screen for Humaeyon Welchem
            'Start TFS2211
            If Not Me.chkBardanaAdjustmentOnPOS Is Nothing Then
                Me.chkBardanaAdjustmentOnPOS.Checked = Convert.ToBoolean(getConfigValueByType("BardanaAdjustmentOnPOS").ToString)
            End If
            'End TFS2211


            '08-Aug-2017 TFS1152 : Rai Haider: To Check If checkbox is checked then show Rate for edit On PO screen
            'Start TFS1152
            If Not getConfigValueByType("VisiblerateonPO").ToString = "Error" Then
                Me.chkVisiblerateonPO.Checked = Convert.ToBoolean(getConfigValueByType("VisiblerateonPO").ToString)
            End If
            'End TFS1152
            Me.chkDCBasedonSO.Checked = Convert.ToBoolean(getConfigValueByType("DCDependentonSO").ToString)
            Me.chkSalaryPercentage.Checked = Convert.ToBoolean(getConfigValueByType("SalaryGenerationPercentageBased").ToString)
            Me.txtOTWorkingDays.Text = Convert.ToInt32(Val(getConfigValueByType("OverTimeWorkingDays").ToString))
            Me.txtOTSFPercentage.Text = Convert.ToInt32(Val(getConfigValueByType("OverTimeSalaryFactorPercentage").ToString))
            Me.txtOTNormalMultiplier.Text = Convert.ToInt32(Val(getConfigValueByType("OverTimeNormalDayMultiplier").ToString))
            Me.txtOTOffMultiplier.Text = Convert.ToInt32(Val(getConfigValueByType("OverTimeOffDayMultiplier").ToString))
            If getConfigValueByType("OverTimeBasedOnWorkingDays").ToString <> "Error" Then
                Me.chkOTBasedOnWorkingDays.Checked = Convert.ToBoolean(getConfigValueByType("OverTimeBasedOnWorkingDays").ToString)
            End If
            'Me.chkAfterAllowancesAbsentDeduction.Checked = Convert.ToBoolean(getConfigValueByType("AfterAllownacesandAbsentDeduction").ToString)
            'Me.chkAfterAbsentDeduction.Checked = Convert.ToBoolean(getConfigValueByType("AfterAbsentDeduction").ToString)
            'Me.txtProvidentPercentage.Text = Convert.ToInt32(Val(getConfigValueByType("ProvidentFactorPercentage").ToString))
            'Me.txtProvidentSFPercentage.Text = Convert.ToInt32(Val(getConfigValueByType("SalaryFactorPercentage").ToString))
            If Not getConfigValueByType("AfterAllownacesandAbsentDeduction").ToString = "Error" Then
                Me.chkAfterAllowancesAbsentDeduction.Checked = Convert.ToBoolean(getConfigValueByType("AfterAllownacesandAbsentDeduction").ToString)
            End If
            If Not getConfigValueByType("AfterAllownacesandAbsentDeduction").ToString = "Error" Then
                Me.chkAfterAbsentDeduction.Checked = Convert.ToBoolean(getConfigValueByType("AfterAbsentDeduction").ToString)
            End If
            Me.txtProvidentPercentage.Text = Convert.ToInt32(Val(getConfigValueByType("ProvidentFactorPercentage").ToString))
            Me.txtProvidentSFPercentage.Text = Convert.ToInt32(Val(getConfigValueByType("SalaryFactorPercentage").ToString))
            Me.txtRepeatedCustomerCount.Text = Convert.ToInt32(Val(getConfigValueByType("CustomerRepeatedCount").ToString))

            Me.cmbRetentionAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("RetentionAccount").ToString))
            Me.cmbMobilizationAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("MobilizationAccount").ToString))

            ''TASK TFS1268 Allow user to update delivered SALES ORDER in certain condition. on 08-08-2017
            If Not getConfigValueByType("SOUpdateAfterDelivery").ToString = "Error" Then
                Me.chkSOUpdateAfterDelivery.Checked = Convert.ToBoolean(getConfigValueByType("SOUpdateAfterDelivery").ToString)
            End If
            ''10-08-2017 : TFS1265 : Muhammad Ameen added Memo Remarks which are configuration based to be saved to voucher detail from Payment, Expense and Receipt. on 10-08-2017
            If Not getConfigValueByType("MemoRemarks").ToString = "Error" Then
                Me.chkMemoRemarks.Checked = Convert.ToBoolean(getConfigValueByType("MemoRemarks").ToString)
            End If
            ''End TASK: TFS1265
            Me.chkChangeVoucherType.Checked = Convert.ToBoolean(getConfigValueByType("ChangeVoucherType").ToString) ''Added By Ayesha Rehman : Missing Before 3411
            ''TASK TFS1458 And TFS1462
            If Not getConfigValueByType("DirectVoucherPrinting").ToString = "Error" Then
                Me.chkDirectVoucherPrinting.Checked = Convert.ToBoolean(getConfigValueByType("DirectVoucherPrinting").ToString)
            End If
            '' END TASK TFS1458 And TFS1462
            ''TASK TFS1490
            If Not getConfigValueByType("BagStock").ToString = "Error" Then
                Me.chkBagStock.Checked = Convert.ToBoolean(getConfigValueByType("BagStock").ToString)
            End If
            '' END TASK TFS1490
            ''TASK TFS1378
            If Not getConfigValueByType("DCStockImpact").ToString = "Error" Then
                Me.cbDCStockImpact.Checked = Convert.ToBoolean(getConfigValueByType("DCStockImpact").ToString)
                ''Start TFS4745
                If Me.cbDCStockImpact.Checked = True Then
                    Me.cbDCStockImpact.Enabled = False
                Else
                    Me.cbDCStockImpact.Enabled = True
                End If
                ''End TFS4745
            End If
            '' END TASK TFS1378
            ''TASK TFS1378
            If Not getConfigValueByType("GRNStockImpact").ToString = "Error" Then
                Me.cbGRNStockImpact.Checked = Convert.ToBoolean(getConfigValueByType("GRNStockImpact").ToString)
                If Me.cbGRNStockImpact.Checked = True Then
                    Me.cbGRNStockImpact.Enabled = False
                Else
                    Me.cbGRNStockImpact.Enabled = True
                End If
            End If
            '' END TASK TFS1378
            ''LoadMultiGRN

            If Not getConfigValueByType("LoadMultiGRN").ToString = "Error" Then
                Me.chkLoadMultiGRN.Checked = Convert.ToBoolean(getConfigValueByType("LoadMultiGRN").ToString)
            End If

            ''TASK TFS1782. New configuration is added to allow user to get to save duplicate account name on Main account, Sub account, Sub Sub account and detail account.
            If Not getConfigValueByType("DuplicateAccountName").ToString = "Error" Then
                Me.chkDuplicateAccountName.Checked = Convert.ToBoolean(getConfigValueByType("DuplicateAccountName").ToString)
            End If
            ''END

            Me.cmbVDQty.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("VendorDifferenceQty").ToString))

            ''TASK TFS1927 
            If Not getConfigValueByType("WastedStockAccount").ToString = "Error" Then
                Me.cmbWastedStockAccount.SelectedValue = Val(getConfigValueByType("WastedStockAccount").ToString)
            End If
            If Not getConfigValueByType("ScrappedStockAccount").ToString = "Error" Then
                Me.cmbScrappedStockAccount.SelectedValue = Val(getConfigValueByType("ScrappedStockAccount").ToString)
            End If
            ''END TFS1927
            Me.cmbSaleTaxDeductioAcId.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SaleTaxDeductionAcId").ToString))
            'Ali Faisal : TFS2017 : Add config for PO generation from CS
            If Not getConfigValueByType("POFromCS").ToString = "Error" Then
                Me.chkPOFromCS.Checked = Convert.ToBoolean(getConfigValueByType("POFromCS").ToString)
            End If
            'Ali Faisal : TFS2017 : End
            Me.chkShowMiscAccountsOnSales.Checked = IIf(getConfigValueByType("ShowMiscAccountsOnSales") = "False", 0, 1) ''TFS2236
            Me.chkLoadItemAfterDeliveredOnDC.Checked = IIf(getConfigValueByType("LoadItemAfterDeliveredOnDC") = "False", 0, 1) ''TFS2825
            '' TASK TFS2985 ON 09-04-2018
            If Not getConfigValueByType("IssueCompleteRawMaterialForStage").ToString = "Error" Then
                Me.chkIssueCompleteRawMaterialForStage.Checked = Convert.ToBoolean(getConfigValueByType("IssueCompleteRawMaterialForStage").ToString)
            End If
            ''END TASK TFS22985
            ''START TASK TFS2302
            Me.cmbAgent.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AgentSubSub").ToString))
            Me.cmbDealer.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("DealerSubSub").ToString))
            Me.cmbInvestor.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("InvestorSubSub").ToString))
            Me.cmbSeller.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SellerSubSub").ToString))
            Me.cmbBuyer.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("BuyerSubSub").ToString))
            ''TFS2689  Saba Shabbir added drop down in property configuration
            Me.cmbCommisionAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CommissionAccount").ToString))

            ''TFS2797 Saba Shabbir added Sales and Purchase Accounts Configurations
            Me.cmbSaleAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PropertySalesAccount").ToString))
            Me.cmbSysPuchaseAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PropertyPurchaseAccount").ToString))
            Me.cmbInvestmentAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("InvestmentBookingAccount").ToString))
            Me.cmbProfitExpenseAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ProfitExpenseAccount").ToString))
            ''END TASK TFS2302

            ' '' TASK TFS2383
            'If Not getConfigValueByType("ShowIdentRelatedFields").ToString = "Error" Then
            '    Me.chkShowIdentRelatedFields.Checked = Convert.ToBoolean(getConfigValueByType("ShowIdentRelatedFields").ToString)
            'End If
            ' '' END TASK TFS2383

            'Task 2613
            If Not getConfigValueByType("ShowCompanyWisePrefix").ToString = "Error" Then
                Me.checkShowCompanyWisePrefix.Checked = Convert.ToBoolean(getConfigValueByType("ShowCompanyWisePrefix").ToString)
            End If
            'End Task 2613



            '' TASK TFS3538
            If Not getConfigValueByType("NewSalarySheetPrint").ToString = "Error" Then
                Me.chkNewSalarySheetPrint.Checked = Convert.ToBoolean(getConfigValueByType("NewSalarySheetPrint").ToString)
            End If
            '' END TASK TFS3538

            ''START TASK TFS2375

            ''TASK TFS3111
            If Not getConfigValueByType("RestrictEntryInParentDetailAC").ToString = "Error" Then
                Me.chkRestrictEntryInParentDetailAC.Checked = Convert.ToBoolean(getConfigValueByType("RestrictEntryInParentDetailAC").ToString)
            End If
            '' END TASK TFS3111
            Me.cmbAccountsApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AccountsApproval").ToString))
            Me.cmbSalesApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesApproval").ToString))
            Me.cmbPurchaseApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseApproval").ToString))
            Me.cmbProductionApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ProductionApproval").ToString))
            Me.cmbQuotationApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("QuotationApproval").ToString))
            Me.cmbPurchaseDemandApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseDemandApproval").ToString))
            Me.cmbPurchaseOrderApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseOrderApproval").ToString))
            Me.cmbPurchaseReturnApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseReturnApproval").ToString))
            Me.cmbPurchaseInquiryApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PurchaseInquiryApproval").ToString))
            Me.cmbGRNApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("GRNApproval").ToString))
            Me.cmbVendorQuotationApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("VendorQuotationApproval").ToString))
            Me.cmbActivityFeedBackApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ActivityFeedBackApproval").ToString))
            Me.cmbVoucherEntryApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("VoucherEntryApproval").ToString))
            Me.cmbPaymentApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PaymentApproval").ToString))
            Me.cmbReceiptApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ReceiptApproval").ToString))
            Me.cmbExpenseApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ExpenseApproval").ToString))
            ''END TASK TFS2375
            'start task 3113
            Me.cmbSalesInquiry.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesInquiryApproval").ToString))
            Me.cmbSalesQuotation.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesQuotationApproval").ToString))
            Me.cmbSalesOrder.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesOrderApproval").ToString))
            Me.cmbDeliveryChallan.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("DeliveryChallanApproval").ToString))
            Me.cmbSalesInvoice.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesInvoiceApproval").ToString))
            Me.cmbSalesReturn.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SalesReturnApproval").ToString))
            Me.cmbCashRequestApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CashRequestApproval").ToString))
            Me.cmbEmployeeLoanRequestApproval.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("EmployeeLoanRequestApproval").ToString))
            'end task 3113

        Catch ex As Exception
            ErrorMessage = ex.Message
            Throw ex
        End Try
    End Sub



    Public Function GetConfigByType(ByVal strType As String) As String
        Try
            Return GetFilterDataFromDataTable(Me.ConfigDataTable, "[Config_Type]='" & strType & "'").ToTable("Config").Rows(0).Item("config_value").ToString()

        Catch ex As Exception
            ErrorMessage = ex.Message
            Throw ex
        End Try
    End Function
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtOldPassword.Visible = False
            Me.txtNewPassword.Visible = False
            Me.lblOld.Visible = False
            Me.lblNew.Visible = False
            Me.txtOldPassword.Text = ""
            Me.txtNewPassword.Text = ""
            ''TASK TFS2302
            Dim PropertyTab As String = getConfigValueByType("PropertyTabEnabled").ToString()
            If PropertyTab = "True" Then
                Me.UltraTabControl1.Tabs("Property").Visible = True
            Else
                Me.UltraTabControl1.Tabs("Property").Visible = False
            End If
            ''END TASK TFS2302
            GetSecurityRightsForBackup()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        If IsValidate() = True Then
            Me.lblPrograssbar1.Text = ""
            Me.lblPrograssbar1.Text = "Please wait"
            Do Until Me.PrograssBar1.Value >= 95
                Me.PrograssBar1.Value = Me.PrograssBar1.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(10)
            Loop
            If New SBDal.ConfigSystemDAL().SaveConfigSys(ConfigList) Then
                Return True
            End If
        Else
            Return False
        End If
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    Private Sub frmSystemConfigurationNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Me.btnSave.Enabled = True
        Try
            FillCombos("SalesAccount")
            FillCombos("DefaultAccountInPlaceCustomer")
            FillCombos("MainAccountforRevenueImport")
            FillCombos("PurchaseAccount")
            FillCombos("CGSAccount")
            FillCombos("CostProduction")
            FillCombos("SalesTaxAccount")
            FillCombos("PurchaseTaxAccount")
            FillCombos("ReceiveableTax")
            FillCombos("PayableTax")
            FillCombos("FuelExpAccount")
            FillCombos("AdjustmentExpAccount")
            FillCombos("OtherExpAccount")
            FillCombos("SalaryAccount")
            FillCombos("SalaryPayableAccount")
            FillCombos("PLAccount")
            FillCombos("ExchangeGainLossAccount")
            FillCombos("SEDAccount")
            FillCombos("Email")
            'FillCombos("AdminEmail")
            FillCombos("SalesDiscount")
            FillCombos("TransitInsurance")
            FillCombos("InwardExpense")
            FillCombos("InvAccount")
            FillCombos("CostOfGoodSoldccount")
            FillCombos("CylinderStockAccount")
            'TaskID No 2537 Append Newly Added two combo controls of SaleDeductionTaxAccountID & PurchaseDeductionTaxAccountID to fill these Combos
            FillCombos("SaleTaxDeductionAcId")
            FillCombos("PurchaseTaxDeductionAcId")
            FillCombos("ClaimAccount") 'Task:2660 Get Accounts List
            FillCombos("CMFAExpAcHead") 'Task:2701 Configured CMFA Exp Combobox 
            FillCombos("ServerToSMS")
            FillCombos("AdditionalCost")
            FillCombos("CustomDuty")
            FillCombos("SalesTax")
            FillCombos("AdditionalSalesTax")
            FillCombos("AdvanceIncome")
            FillCombos("ExciseDuty")
            FillCombos("EmployeeHeadAccount")
            FillCombos("EmpDeparmentAcHead")
            FillCombos("AdditionalTax")
            FillCombos("Currency")
            FillCombos("VendorDifferenceQty")
            FillCombos("RetentionAccount")
            FillCombos("MobilizationAccount")
            FillCombos("WastedStockAccount")
            FillCombos("ScrappedStockAccount")

            ''START TASK TFS2302
            FillCombos("Agent")
            FillCombos("Dealer")
            FillCombos("Investor")
            FillCombos("Buyer")
            FillCombos("Seller")
            FillCombos("CommissionAccount")
            ''TFS2797 Saba shabbir added Sales, Purchase and investment Accounts Configurations
            FillCombos("PropertySalesAccount")
            FillCombos("PropertyPurchaseAccount")
            FillCombos("InvestmentBookingAccount")
            FillCombos("ProfitExpenseAccount")
            ''END TASK TFS2302

            ''START TASK TFS2375
            FillCombos("AccountsApproval")
            FillCombos("QuotationApproval")
            FillCombos("SalesApproval")
            FillCombos("PurchaseApproval")
            FillCombos("PurchaseDemandApproval")
            FillCombos("PurchaseOrderApproval")
            FillCombos("PurchaseReturnApproval")
            FillCombos("GRNApproval")
            FillCombos("VoucherApproval")
            FillCombos("PaymentApproval")
            FillCombos("ReceiptApproval")
            FillCombos("ExpenseApproval")
            FillCombos("PurchaseInquiryApproval")
            FillCombos("VendorQuotationApproval")
            FillCombos("ProductionApproval")
            FillCombos("ActivityFeedBackApproval")
            ''END TASK TFS2375
            ''Start TFS3113
            FillCombos("SalesInquiryApproval")
            FillCombos("SalesQuotationApproval")
            FillCombos("SalesOrderApproval")
            FillCombos("DeliveryChallanApproval")
            FillCombos("SalesInvoiceApproval")
            FillCombos("SalesReturnApproval")
            FillCombos("CashRequestApproval")
            FillCombos("EmployeeLoanRequestApproval")
            ''EndTFS3113

            GetAllRecords()
            ReSetControls()

            IsOpenForm = True
            'Me.PrograssBar1.Visible = False
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading" & ex.Message)
            Me.btnSave.Enabled = False
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try

            FillCombos("SalesAccount")
            FillCombos("PurchaseAccount")
            FillCombos("CGSAccount")
            FillCombos("CostProduction")
            FillCombos("SalesTaxAccount")
            FillCombos("PurchaseTaxAccount")
            FillCombos("ReceiveableTax")
            FillCombos("PayableTax")
            FillCombos("FuelExpAccount")
            FillCombos("AdjustmentExpAccount")
            FillCombos("OtherExpAccount")
            FillCombos("SalaryAccount")
            FillCombos("SalaryPayableAccount")
            FillCombos("PLAccount")
            FillCombos("ExchangeGainLossAccount")
            FillCombos("SEDAccount")
            FillCombos("Email")
            'FillCombos("AdminEmail")
            FillCombos("SalesDiscount")
            FillCombos("TransitInsurance")
            FillCombos("InwardExpense")
            FillCombos("InvAccount")
            FillCombos("CostOfGoodSoldccount")
            FillCombos("CMFAExpAcHead") 'Task:2701 Reseting CMFA Exp Combo
            FillCombos("ServerToSMS")
            FillCombos("AdditionalCost")
            FillCombos("CustomDuty")
            FillCombos("SalesTax")
            FillCombos("AdditionalSalesTax")
            FillCombos("AdvanceIncome")
            FillCombos("ExciseDuty")
            FillCombos("EmployeeHeadAccount")
            FillCombos("EmpDeparmentAcHead")
            FillCombos("AdditionalTax")
            FillCombos("PurchaseApproval")
            FillCombos("PurchaseDemandApproval")
            FillCombos("PurchaseOrderApproval")
            FillCombos("PurchaseReturnApproval")
            FillCombos("GRNApproval")
            FillCombos("PurchaseInquiryApproval")
            FillCombos("VendorQuotationApproval")
            FillCombos("VoucherApproval")
            FillCombos("PaymentApproval")
            FillCombos("ReceiptApproval")
            FillCombos("ExpenseApproval")
            FillCombos("ActivityFeedBackApproval")

            'start task 3113
            FillCombos("SalesInquiryApproval")
            FillCombos("SalesQuotationApproval")
            FillCombos("SalesOrderApproval")
            FillCombos("DeliveryChallanApproval")
            FillCombos("SalesInvoiceApproval")
            FillCombos("SalesReturnApproval")
            FillCombos("CashRequestApproval")
            FillCombos("EmployeeLoanRequestApproval")
            'end task 3113

            ''TFS2797 Saba Shabbir added Sales and Purchase Accounts Configurations
            'FillCombos("SysPurchaseAccount")
            'FillCombos("SaleAccount")
            'FillCombos("InvestmentAccount")
            IsOpenForm = True
            getConfigValueList()
            GetAllRecords()
            GetSecurityRightsForBackup()
            GetSecurityRights()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.WaitCursor
        Me.PrograssBar1.Visible = True
        Try
            If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
            Me.PrograssBar1.Value = 0
            If Save() = True Then
                Me.lblPrograssbar1.Text = ""
                Me.lblPrograssbar1.Text = "Successfully configured system"
                Do Until Me.PrograssBar1.Value >= 99
                    Me.PrograssBar1.Value = Me.PrograssBar1.Value + 1
                    Application.DoEvents()
                Loop
                msg_Information(str_informSave)
                If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                    ConfigRights(LoginUserId)
                    Dim str As String = "Select * From tblRights"
                    Dim dt As DataTable = GetDataTable(str)
                    If Not dt Is Nothing Then
                        If dt.Rows.Count = 0 Then
                            frmUserGroup.ShowDialog()
                        End If
                    End If
                End If
                Me.PrograssBar1.Visible = False
            Else
                ShowErrorMessage("Error! Please Try Again")
            End If

            Try
                IsEmailAttachment()
                EmailAlter()
                AdminEmails()
                ShowHeaderCompany()
                FileExportPath()
                getConfigValueList()
                GroupRights = New SBDal.GroupRightsBL().GetRights(LoginUserId)
                _EmployeePicPath = getConfigValueByType("EmployeePicturePath").ToString
                _ArticlePicPath = getConfigValueByType("ArticlePicturePath").ToString
                _BackupDBPath = getConfigValueByType("BackupDBPath").ToString
                Dim dt As New DataTable
                If Not IO.File.Exists(str_ApplicationStartUpPath & "\AutoUpdate.Xml") = True Then
                    dt.TableName = "AutoUpdate"
                    dt.Columns.Add("EnableAutoUpdate", GetType(System.String))
                    Dim dr1 As DataRow
                    dr1 = dt.NewRow
                    dr1(0) = False
                    dt.Rows.InsertAt(dr1, 0)
                    dt.WriteXml(str_ApplicationStartUpPath & "\AutoUpdate.Xml")
                    dt.Dispose()
                End If
                dt.TableName = "AutoUpdate"
                dt.Columns.Add("EnableAutoUpdate", GetType(System.String))
                dt.ReadXml(str_ApplicationStartUpPath & "\AutoUpdate.Xml")
                dt.Rows(0).Delete()
                Dim dr As DataRow
                dr = dt.NewRow
                dr(0) = Me.chkAutoUpdate.Checked
                dt.Rows.InsertAt(dr, 0)
                dt.WriteXml(str_ApplicationStartUpPath & "\AutoUpdate.Xml")
                dt.Dispose()
            Catch ex As Exception


            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            ShowReport("rptSystemConfiguration", "Select * From ConfigValuesTable")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                'Me.btnsave.Enabled = True
                'Me.btnDelete.Enabled = True
                'Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                'Me.btnsave.Enabled = False
                'Me.btnDelete.Enabled = False
                'Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                        'ElseIf Rights.Item(i).FormControlName = "Save" Then
                        '    If Me.btnsave.Text = "&Save" Then btnsave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Update" Then
                        '    If Me.btnsave.Text = "&Update" Then btnsave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        'Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        'Me.btnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim frm As New FrmEmailconfig
            frm.SplitContainer1.Panel1Collapsed = True
            frm.Size = New Size(565, 479)
            frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            frm.StartPosition = FormStartPosition.CenterScreen
            ApplyStyleSheet(frm)
            frm.ShowDialog()
            FillCombos("Email")
            SaveConfiguration("DefaultEmailId", Me.cmbDefaultEmail.SelectedValue)
            'FillCombos("AdminEmail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtFileExportPath.Text = FolderBrowserDialog1.SelectedPath
                SaveConfiguration("FileExportPath", Me.txtFileExportPath.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            GetComboData()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnBackupDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackupDB.Click
        Try
            If Me.FolderBrowserDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtBackupDBPath.Text = FolderBrowserDialog2.SelectedPath
                SaveConfiguration("BackupDBPath", Me.txtBackupDBPath.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEmpPicPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpPicPath.Click
        Try
            If Me.FolderBrowserDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtEmployeePicturePath.Text = FolderBrowserDialog2.SelectedPath
                SaveConfiguration("EmployeePicturePath", Me.txtEmployeePicturePath.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnArticlePicturePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArticlePicturePath.Click
        Try
            If Me.FolderBrowserDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtArticlePicturePath.Text = FolderBrowserDialog2.SelectedPath
                SaveConfiguration("ArticlePicturePath", Me.txtArticlePicturePath.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAssetPicturePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssetPicturePath.Click
        Try
            If Me.FolderBrowserDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtAssetPicturePath.Text = FolderBrowserDialog2.SelectedPath
                SaveConfiguration("AssetPicturePath", Me.txtAssetPicturePath.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAlternativeDBPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlternativeDBPath.Click
        Try
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "Microsoft Access|*.*mdb"
            If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtAttendanceDbPath.Text = OpenFileDialog1.FileName
                SaveConfiguration("AlternateAttendanceDBPath", Me.txtAttendanceDbPath.Text.ToString)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function SaveConfiguration(ByVal KeyType As String, ByVal KeyValue As String, Optional ByVal CreateNewKey As Boolean = False) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim Cmd As New SqlCommand
        Cmd.Connection = Con
        Cmd.Transaction = trans
        Try
            Dim strSQL As String = String.Empty

            If CreateNewKey = True Then

                strSQL = "IF NOT EXISTS(SELECT * From ConfigValuesTable  WHERE Config_Type='" & KeyType & "' ) " _
                        & " Insert into ConfigValuesTable(config_id, config_Type, config_Value, Comments, IsActive) Select Max(config_id) + 1, '" & KeyType & "', '" & KeyValue & "', '', 1 from ConfigValuesTable " _
                        & "Else " _
                        & " UPDATE ConfigValuesTable SET Config_Value=N'" & KeyValue & "' WHERE Config_Type=N'" & KeyType & "'"

            Else

                strSQL = "UPDATE ConfigValuesTable SET Config_Value=N'" & KeyValue & "' WHERE Config_Type=N'" & KeyType & "'"

            End If

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strSQL
            Cmd.ExecuteNonQuery()
            trans.Commit()

            SaveActivityLog("Configuration", Me.Text.ToString, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, KeyType, , , , , Me.Name.ToString)
            key = KeyType
            Dim config As ConfigSystem = objConfigValueList.Find(AddressOf GetObj)

            objConfigValueList.Remove(config)
            Dim AddConfig As New ConfigSystem
            AddConfig.Config_Type = KeyType.ToString
            AddConfig.Config_Value = KeyValue.ToString
            If config IsNot Nothing Then
                If config.Comments IsNot Nothing Then
                    AddConfig.Comments = config.Comments
                Else
                    AddConfig.Comments = Nothing
                End If
                AddConfig.IsActive = config.IsActive
            End If

            objConfigValueList.Add(AddConfig)
            key = String.Empty
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetObj(ByVal Config As ConfigSystem) As Boolean
        Try
            If Config.Config_Type = key Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SavePicture(Optional ByVal KeyType As String = "", Optional ByVal KeyPath As String = "") As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim dt As New DataTable
        Dim strSQL As String = String.Empty
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim Cmd As New SqlCommand
        Cmd.Connection = Con
        Cmd.Transaction = trans
        Try
            Dim selectPic As String = "Select PictureId, PicturePath, PictureType FROM tblPictures Where PictureType='" & KeyType.Trim.Replace("'", "''") & "' "
            dt = GetDataTable(selectPic)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    strSQL = "Update tblPictures Set PicturePath =N'" & KeyPath.Trim.Replace("'", "''") & "', PictureType= N'" & KeyType.Trim.Replace("'", "''") & "' Where PictureType='" & KeyType.Trim.Replace("'", "''") & "' "
                Else
                    strSQL = " Insert into tblPictures(PicturePath, PictureType) Values(N'" & KeyPath & "', N'" & KeyType & "') "
                End If

            End If

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strSQL
            Cmd.ExecuteNonQuery()
            trans.Commit()

            'SaveActivityLog("Configuration", Me.Text.ToString, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, KeyType, , , , , Me.Name.ToString)
            'key = KeyType
            'Dim config As ConfigSystem = objConfigValueList.Find(AddressOf GetObj)

            'objConfigValueList.Remove(config)
            'Dim AddConfig As New ConfigSystem
            'AddConfig.Config_Type = KeyType.ToString
            'AddConfig.Config_Value = KeyValue.ToString
            'If config IsNot Nothing Then
            '    If config.Comments IsNot Nothing Then
            '        AddConfig.Comments = config.Comments
            '    Else
            '        AddConfig.Comments = Nothing
            '    End If
            '    AddConfig.IsActive = config.IsActive
            'End If

            'objConfigValueList.Add(AddConfig)
            'key = String.Empty
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetPicture(Optional ByVal KeyType As String = "", Optional ByVal KeyPath As String = "") As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim dt As New DataTable
        Dim strSQL As String = String.Empty

        Dim Cmd As New SqlCommand
        Cmd.Connection = Con

        Try
            Dim selectPic As String = "Select PictureId, PicturePath, PictureType, Convert(Image, Null) As LogoImage FROM tblPictures Where PictureType='" & KeyType.Trim.Replace("'", "''") & "' "
            dt = GetDataTable(selectPic)
            Return dt
        Catch ex As Exception

            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    ''R:979 Added Auto Load PO's Checkbox Event
    ''Task:2369 Added Comment Layout's Checkbox Event e.g chkCommentsCustomerForamt, chkCommentsArticleFormat, chkCommentsArticleSizeFormat, chkCommentsArticleColorFormat, chkCommentsQtyFormat, chkCommentsPriceFormat, chkCommentRemarksFormat
    'Task:2376 Added CheckedChange Event for Comments Layout 
    ''Task:2446 Added Event chkCommentDCNo, chkCommentEngineNo
    ''task:2451 Added Event chkItemSortOrder.CheckedChanged, chkItemSortOrderByCode.CheckedChanged, chkItemSortOrderByName.CheckedChanged  chkAcSortOrder.CheckedChanged, chkAcSortOrderByCode.CheckedChanged, chkAcSortOrderByName.CheckedChanged
    ''Task:2575 Added Event Checked Change Of Branded SMS
    ''Task:2702 Added Event For Config On/Off  CMFA Document Load On Sales Invoices 
    ''TASK:2728 Added Event for Config On/off CommentsPartyName,CommentsChequeNo,CommentsChequeDate
    ''03-Aug-2014 Task:2767 Imran Ali Change Unit Option On Sales (ZR Traders)
    ''Task:2784 Added event PurchaseAccountFrontEnd.
    'Task#23072015 Add configuration chkEmailAlertDueInvoice for due invoices
    'Task#08082015 add smswithengine no checkbox (Ahmad Sharif)
    '25-July-2017 : TFS1045 : Waqar Raza : Add UserWiseCustomerList CheckBox to show User Wise Customer 
    '04-Aug-2017 : TFS1152 : Rai Haider : Add VisiblerateonPO CheckBox to show Rate for edit on PO screen
    ''10-08-2017 : TFS1265 : Muhammad Ameen added Memo Remarks which are configuration based to be saved to voucher detail from Payment, Expense and Receipt. on 10-08-2017
    '26-Feb-2018 : TFS2377 : Ayesha Rehman : PO Print Based on Checked And Posted Doc
    '19-Mar-2018 : TFS2737 : Ayesha Rehman : Configuration for Lot wise rate for costing
    '13-June-2018 : TFS3520 : Ayesha Rehman : Separate Closure of SO wrt DC and Sales
    Private Sub chkPreviouseRecordShow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPreviouseRecordShow.CheckedChanged, chkAccountHeadReadonly.CheckedChanged, chkInvoiceWiseTax.CheckedChanged, chkChequeDetailEnable.CheckedChanged, chkShowVendorOnSales.CheckedChanged, chkStockViewOnSales.CheckedChanged, chkLoadItemGridSales.CheckedChanged, chkReceiptVoucherOnSales.CheckedChanged, chkServiceItem.CheckedChanged, chkDiscountVoucher.CheckedChanged, chkSalesOrderAnalysis.CheckedChanged, chkMarketReturnVoucher.CheckedChanged, chkCompanyPrefix.CheckedChanged, chkMultipleSalesOrder.CheckedChanged, chkExpenseChargeToCustomer.CheckedChanged, chkCostofsalevoucher.CheckedChanged, chkShowVendorOnDeliveryChalan.CheckedChanged, chkSalemanVoucherPrintLog.CheckedChanged, chkTaxExcludePrice.CheckedChanged, chkShowCustomerOnPurchase.CheckedChanged, chkPaymentVoucherOnPurchase.CheckedChanged, chkExpiryDate.CheckedChanged, chkPurchaseAllowedWithPO.CheckedChanged, chkCurrency.CheckedChanged, chkTransporterCharges.CheckedChanged, chkItemFilterByLocation.CheckedChanged, chkStockDispatchTransfer.CheckedChanged, chkStoreIssuenceWithProduction.CheckedChanged, chkArticleShowImage.CheckedChanged, chkStockDispatchOnProduction.CheckedChanged, chkProductionVoucher.CheckedChanged, chkStoreIssuenceVoucher.CheckedChanged, chkStoreIssuanceDependonProductionPlan.CheckedChanged, chkAvgRate.CheckedChanged, chkAttachments.CheckedChanged, chkEmailAlert.CheckedChanged, chkClinderVoucher.CheckedChanged, chkAutoChequebook.CheckedChanged, chkStockViewOnSales.CheckedChanged, chkGLAccountArticleDepartment.CheckedChanged, chkOnetimesalereturn.CheckedChanged, chkAutoLoadPO.CheckedChanged, chkCommentCustomerFormat.CheckedChanged, chkCommentArticleDescriptionFormat.CheckedChanged, chkCommentArticleSizeFormat.CheckedChanged, chkCommentColorFormat.CheckedChanged, chkCommentQtyFormat.CheckedChanged, chkCommentPriceFormat.CheckedChanged, chkCommentsRemarksFormat.CheckedChanged, _
        chkCommentsVendorFormat.CheckedChanged, chkPurchaseCommentArticleDescriptionFormat.CheckedChanged, chkPurchaseCommentArticleSizeFormat.CheckedChanged, chkPurchaseCommentArticleColorFormat.CheckedChanged, chkPurchaseCommentQtyFormat.CheckedChanged, chkPurchaseCommentPriceFormat.CheckedChanged, chkPurchaseCommentRemarksFormat.CheckedChanged, chkVehicleIdentificationInfo.CheckedChanged, _
        chkMargeItem.CheckedChanged, chkCommentsDcNo.CheckedChanged, chkCommentInvoiceNo.CheckedChanged, chkCommentSalesDCNo.CheckedChanged, chkCommentEngineNo.CheckedChanged, chkUserCompanyRights.CheckedChanged, chkSIRIUSPartner.CheckedChanged, chkShowMasterGrid.CheckedChanged, chkShowCompanyAddress.CheckedChanged, chkReminder.CheckedChanged, chkPreviewInvoice.CheckedChanged, chkNewSecurityRights.CheckedChanged, chkMenuRights.CheckedChanged, chkGrossSalaryCalc.CheckedChanged, chkChangeDocumentNo.CheckedChanged, chkBrandedSMS.CheckedChanged, chkBarcodeEnabled.CheckedChanged, chkAutoUpdate.CheckedChanged, chkAllowMinusStock.CheckedChanged, _
        chkCMFADocumentOnSales.CheckedChanged, _
        chkCommentsPartyName.CheckedChanged, _
        chkCommentsChequeNo.CheckedChanged, _
        chkCommentsChequeDate.CheckedChanged, _
        chkPOPrintAfterApproval.CheckedChanged, _
        chkLoadAllItemPackQty.CheckedChanged, chkPurchaseAccountFrontEnd.CheckedChanged, chkOrderQtyExceedDeliveryChalan.CheckedChanged, chkOrderQtyExceedSales.CheckedChanged, chkDCQtyExceedSales.CheckedChanged, chkTotalAmountWiseInvoiceBasedVoucher.CheckedChanged, chkCGAccountOnStoreIssuance.CheckedChanged, chkActivityLogShow.CheckedChanged, chkDCDetailLocationSMS.CheckedChanged, chkCheckCurrentStockByItem.CheckedChanged, chkCheckCurrentStockByLocation.CheckedChanged, chkAutoArticleCode.CheckedChanged, chkUpdatePostedVoucher.CheckedChanged, chkTransporter.CheckedChanged, chkBiltyNo.CheckedChanged, chkDeductionWHTaxOnTotal.CheckedChanged, chkCashBankOptionDetail.CheckedChanged, chkChangeVoucherType.CheckedChanged, ChkDirectPrinting.CheckedChanged, chkAllowPaymentZeroBalance.CheckedChanged, chkApplyFlatDiscountOnSale.CheckedChanged, chkEmailAlertDueInvoice.CheckedChanged, chkLoadMultiplePO.CheckedChanged, chkCompanyWisePrefixOnVoucher.CheckedChanged, chkSMSWithEngineNo.CheckedChanged, chkApply40KgPackRate.CheckedChanged, chkUserwiseCompany.CheckedChanged, chkUserwiseLocation.CheckedChanged,
        chkWeighbridgePurchase.CheckedChanged, chkWeighbridgeOnGRN.CheckedChanged, chkEnabledDuplicateVoucherLog.CheckedChanged, chkApplyCostSheetRateOnProduction.CheckedChanged, chkLoadMultiChalanOnSale.CheckedChanged, chkFreezColumn.CheckedChanged, chkAllowAddZeroPriceOnPurchase.CheckedChanged, chkAllowChangePO.CheckedChanged, chkAllowChangeSO.CheckedChanged, chkEnabledSizeCombinationCodeArticleCode.CheckedChanged, chkApplyAdjustmentFuelExpTotal.CheckedChanged, chkEnableDuplicateQuotation.CheckedChanged, chkEnableDuplicateSalesOrder.CheckedChanged, chkWeighbridgeDC.CheckedChanged, chkWeighbridgeSaleOrder.CheckedChanged, chkHelpnSupport.CheckedChanged, chkDCBasedonSO.CheckedChanged, chkAssociateItems.CheckedChanged, chkOTBasedOnWorkingDays.CheckedChanged, chkSalaryPercentage.CheckedChanged, chkApplyDefaultWorkingHoursOnOverTime.CheckedChanged, chkAfterAllowancesAbsentDeduction.CheckedChanged, chkAfterAbsentDeduction.CheckedChanged, chkRackonSalesItemLoad.CheckedChanged, chkUserWiseCustomer.CheckedChanged, chkWeightWiseImport.CheckedChanged, chkVisiblerateonPO.CheckedChanged, _
        chkSOUpdateAfterDelivery.CheckedChanged, chkMemoRemarks.CheckedChanged, chkLoadMultiGRN.CheckedChanged, chkDirectVoucherPrinting.CheckedChanged, chkBagStock.CheckedChanged, cbGRNStockImpact.CheckedChanged, cbDCStockImpact.CheckedChanged, chkDuplicateAccountName.CheckedChanged, chkDuplicateMobile.CheckedChanged, chkPOFromCS.CheckedChanged, chkAllDispatchLocations.CheckedChanged, chkShowMiscAccountsOnSales.CheckedChanged, checkShowCompanyWisePrefix.CheckedChanged, _
        chkCostImplementationLotWiseOnStockMovement.CheckedChanged, _
        chkLoadItemAfterDeliveredOnDC.CheckedChanged, chkIssueCompleteRawMaterialForStage.CheckedChanged, chkRestrictEntryInParentDetailAC.CheckedChanged, chkBardanaAdjustmentOnPOS.CheckedChanged, chkNewSalarySheetPrint.CheckedChanged, chkSeparateClosureOfSODC.CheckedChanged, ChkDateWiseAverageRate.CheckedChanged, chkItemFilterByName.CheckedChanged ''TFS2825
        Try
            If IsOpenForm = False Then Exit Sub
            Dim chk As Windows.Forms.CheckBox
            chk = CType(sender, CheckBox)

            ''TASK TFS1574 Delivery Chalan Impact on stock.
            If chk.Tag = "DCStockImpact" AndAlso chk.Checked = True Then
                If CheckOpenDCs() Then
                    msg_Information("Opened DCs are found. Please close them first. ")
                    cbDCStockImpact.Checked = False
                    Exit Sub
                End If
                ''TASK TFS4745
                cbDCStockImpact.Enabled = False
                ''END TASK TFS4745
            End If
            ''END TASK TFS1574
            ''TASK TFS1574 GRN Impact on stock.
            If chk.Tag = "GRNStockImpact" AndAlso chk.Checked = True Then
                If CheckOpenGRNs() Then
                    msg_Information("Opened GRNs are found. Please close them first. ")
                    cbGRNStockImpact.Checked = False
                    DisplayOpenedGRNs()
                    Exit Sub
                End If
                ''TASK TFS4709
                cbGRNStockImpact.Enabled = False
                ''END TASK TFS4709
            End If
            ''END TASK TFS1574

            If chk.Tag.ToString.Length > 0 Then SaveConfiguration(chk.Tag, chk.Checked)
            'SaveConfiguration(KeyType, KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    ''R-929 Added Checkbox OnetimeSalesReturn
    ''Task:2488 Added Sale Certificate Event
    ''Task:2576 Added Event Of Branded SMS
    'Marked Against Task# 20150704 Ali Ansari to enable Leave Button Click
    'Private Sub btnVoucherFormat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoucherFormat.Click, btnSalaryExpAccount.Click, btnSalaryPayableAccount.Click, btnProfitandLossAccount.Click, btnWorkingDays.Click, btnSalesAccount.Click, btnSalesTaxAccount.Click, btnTaxPayableAccount.Click, btnFuelExpAccount.Click, btnAdjustmentExp.Click, btnOtherExpAcc.Click, btnWHTaxAcc.Click, btnTransitInsurance.Click, btnSalesDiscountAcc.Click, btnAdditionalTax.Click, btnWHTPercentage.Click, btnDefaultTax.Click, btnSalesmanVoucherPrintLimit.Click, btnPurchaseAcc.Click, btnPurchseTaxAcc.Click, btnTaxReceivableAcc.Click, btnInwardExpHeadAcc.Click, btnStoreIssuanceAcc.Click, btnCostofProductionAcc.Click, btnInventoryAcc.Click, btnCGSAcc.Click, btnAdminEmail.Click, btnCylinderStockAccount.Click, btnSaleCertificatePrefix.Click, btnBrandedSMSMask.Click, btnBrandedSMSPassword.Click, btnBrandedSMSUser.Click, btnCompanyAddress.Click, btnCompanyName.Click, btnCompanyLabelName.Click, btnGrossSalaryFormula.Click, Button5.Click
    'Marked Against Task# 20150704 Ali Ansari to enable Leave Button Click
    'Altered Against Task# 20150704 Ali Ansari to enable Leave and Start Attendance Date Button Click
    Private Sub btnVoucherFormat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoucherFormat.Click, btnProfitandLossAccount.Click, btnExchangeGainLossAccount.Click, btnSalesAccount.Click, btnSalesTaxAccount.Click, btnTaxPayableAccount.Click, btnFuelExpAccount.Click, btnAdjustmentExp.Click, btnOtherExpAcc.Click, btnWHTaxAcc.Click, btnTransitInsurance.Click, btnSalesDiscountAcc.Click, btnAdditionalTax.Click, btnWHTPercentage.Click, btnDefaultTax.Click, btnSalesmanVoucherPrintLimit.Click, btnPurchaseAcc.Click, btnPurchseTaxAcc.Click, btnTaxReceivableAcc.Click, btnInwardExpHeadAcc.Click, btnStoreIssuanceAcc.Click, btnCostofProductionAcc.Click, btnInventoryAcc.Click, btnCGSAcc.Click, btnAdminEmail.Click, btnCylinderStockAccount.Click, btnSaleCertificatePrefix.Click, btnBrandedSMSMask.Click, btnBrandedSMSPassword.Click, btnBrandedSMSUser.Click, btnCompanyAddress.Click, btnCompanyName.Click, btnCompanyLabelName.Click, Button5.Click, btnOTWorkingDays.Click, btnOTSFPercentage.Click, btnOTNormalMultiplier.Click, btnOTOffMultiplier.Click, btnCostSheetType.Click, btnSalaryExpAccount.Click, btnWorkingDays.Click, BtnStartPeriod.Click, btnSalaryPayableAccount.Click, btnProvidentSFPercentage.Click, btnProvidentPercentage.Click, BtnLeaves.Click, btnGrossSalaryFormula.Click, btnRepeatedCustomerCount.Click, btnHistoryLoadQuantity.Click, btnStockOutConfigration.Click, btnStockInConfigration.Click, btnConversionTitle.Click, btnConversionFactor.Click, btnDefualtAccountInPlaceCustomer.Click, btnExchangeGainLossAccount.Click, btnMainAccountforRevenueImport.Click ', BtnLoadMultiPO.Click ''TFS1596
        'Altered Against Task# 20150704 Ali Ansari to enable Leave Button Click
        Try
            If IsOpenForm = False Then Exit Sub
            Dim btn As Windows.Forms.Button
            btn = CType(sender, Button)
            Dim KeyType As String = String.Empty
            Dim KeyValue As String = String.Empty
            Select Case btn.Name
                Case btnCostSheetType.Name
                    KeyType = "CostSheetType"
                    KeyValue = Me.cmbCostSheetType.Text
                Case btnWorkingDays.Name
                    KeyType = "Working_Days"
                    KeyValue = Me.txtWorkingDays.Text
                Case btnVendorQuotation.Name
                    KeyType = "VendorQuotation"
                    KeyValue = Me.txtVendorQuotationDocPrefix.Text
                    'Altered by Ali Ansari against Task# 201507004 to Save Start Attendance Period and Total Leave Days
                Case BtnStartPeriod.Name
                    If DtpStartDate.Checked = True Then
                        KeyType = "Attendance_Period"
                        KeyValue = Me.DtpStartDate.Value
                    End If
                Case BtnLeaves.Name
                    KeyType = "Leave_Days"
                    KeyValue = Me.TxtTotalLeaves.Text
                    'Altered by Ali Ansari against Task# 201507004 to Save Start Attendance Period and Total Leave Days




                Case btnAdditionalTax.Name
                    KeyType = "TransitInssuranceTax"
                    KeyValue = Me.txtTransitInssuranceTax.Text
                Case btnWHTPercentage.Name
                    KeyType = "WHTax_Percentage"
                    KeyValue = Me.txtWHTax.Text
                Case btnDefaultTax.Name
                    KeyType = "Default_Tax_Percentage"
                    KeyValue = Me.txtDefaultTax.Text
                Case btnSalesmanVoucherPrintLimit.Name
                    KeyType = "PrintCount"
                    KeyValue = Me.txtSalemanVoucherPrintCount.Text

                    '20-July-2017: Task TFS1084: Waqar Raza: Added for sales history load quntity.
                    'Start Task:
                Case btnHistoryLoadQuantity.Name
                    KeyType = "SalesHistoryLoadQuantity"
                    KeyValue = Me.txtHistoryLoadQuantity.Text
                    'End Task:

                    '09-Jan-2018: Task TFS2075: Waqar Raza: Added for Showing Conversion Title on Purchase and Purchase Order.
                    'Start Task:
                Case btnConversionTitle.Name
                    KeyType = "ConversionTitle"
                    KeyValue = Me.txtConversionTitle.Text

                Case btnConversionFactor.Name
                    KeyType = "ConversionFactor"
                    KeyValue = Val(Me.txtConversionFactor.Text)
                    'End Task:


                Case btnCompanyName.Name
                    KeyType = "CompanyNameHeader"
                    KeyValue = Me.txtCompanyName.Text
                Case btnCompanyAddress.Name
                    KeyType = "CompanyAddressHeader"
                    KeyValue = Me.txtCompanyAddress.Text
                Case btnCompanyLabelName.Name
                    KeyType = "SoftbeatsPartnerName"
                    KeyValue = Me.txtPartnerName.Text
                Case btnGrossSalaryFormula.Name
                    KeyType = "GrossSalaryFormula"
                    KeyValue = Me.txtGrossSalaryFormula.Text

                    '----------------Email Configuration

                Case btnAdminEmail.Name
                    KeyType = "AdminEmailId"
                    KeyValue = Me.txtAdminEmail.Text

                    ''13-Mar-2014 TASK:2488 Imran Ali Sales Certificate In ERP
                Case btnSaleCertificatePrefix.Name
                    KeyType = "SaleCertificatePreFix"
                    KeyValue = Me.txtSaleCertificatePrefix.Text

                    'Case btnCylinderStockAccount.Name
                    '    KeyType = btnCylinderStockAccount.Tag
                    '    KeyValue = Me.cmbCylinderStockAccount.SelectedValue
                    'Task:2576 Added Case For Branded SMS Mask
                Case btnBrandedSMSMask.Name
                    KeyType = "BrandedSMSMask" 'TODO Access
                    KeyValue = Encrypt(Me.txtBrandedSMSMask.Text)
                Case btnBrandedSMSUser.Name
                    KeyType = "BrandedSMSUser"
                    KeyValue = Me.txtBrandedSMSUser.Text
                Case btnBrandedSMSPassword.Name
                    KeyType = "BrandedSMSPassword"
                    KeyValue = Encrypt(Me.txtBrandedSMSPassword.Text)
                    'End Task:2576
                Case Me.btnDNSHost.Name
                    KeyType = "DNSHostForSMS"
                    KeyValue = Me.txtDNSHost.Text
                Case Me.Button5.Name
                    KeyType = "AdminMobileNo"
                    KeyValue = Me.txtAdminMobile.Text

                    'Case BtnLoadMultiPO.Name
                    '    KeyType = "LoadMultiPO"
                    '    KeyValue = chkLoadMultiplePO.Checked
                Case Me.btnOTWorkingDays.Name
                    KeyType = "OverTimeWorkingDays"
                    KeyValue = Me.txtOTWorkingDays.Text
                Case Me.btnOTSFPercentage.Name
                    KeyType = "OverTimeSalaryFactorPercentage"
                    KeyValue = Me.txtOTSFPercentage.Text
                Case Me.btnOTNormalMultiplier.Name
                    KeyType = "OverTimeNormalDayMultiplier"
                    KeyValue = Me.txtOTNormalMultiplier.Text
                Case Me.btnOTOffMultiplier.Name
                    KeyType = "OverTimeOffDayMultiplier"
                    KeyValue = Me.txtOTOffMultiplier.Text
                Case Me.btnProvidentPercentage.Name
                    KeyType = "ProvidentFactorPercentage"
                    KeyValue = Me.txtProvidentPercentage.Text
                Case Me.btnProvidentSFPercentage.Name
                    KeyType = "SalaryFactorPercentage"
                    KeyValue = Me.txtProvidentSFPercentage.Text
                Case Me.btnRepeatedCustomerCount.Name
                    KeyType = "CustomerRepeatedCount"
                    KeyValue = Me.txtRepeatedCustomerCount.Text
                Case Me.btnStockInConfigration.Name  ''TFS1596
                    KeyType = "StockInConfigration"
                    KeyValue = Me.cmbStockIn.Text
                Case Me.btnStockInConfigration.Name  ''TFS1596
                    KeyType = "StockOutConfigration"
                    KeyValue = Me.cmbStockOut.Text
                    ''TASK TFS1927
                Case Me.btnWastedStockAccount.Name
                    KeyType = "WastedStockAccount"
                    KeyValue = Me.cmbWastedStockAccount.SelectedValue.ToString
                Case Me.btnScrappedStockAccount.Name
                    KeyType = "ScrappedStockAccount"
                    KeyValue = Me.cmbScrappedStockAccount.SelectedValue.ToString
                    ''END TASK TFS1927
            End Select
            SaveConfiguration(KeyType, KeyValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Task:2595 Added Event Leav Encashment
    Private Sub nudDefaultReminder_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudDefaultReminder.ValueChanged, nudDefaultWorkingHours.ValueChanged, nudLeaveEncashment.ValueChanged, ndArticleCodeLength.ValueChanged
        Try
            If IsOpenForm = False Then Exit Sub
            Dim nud As NumericUpDown = CType(sender, NumericUpDown)
            'SaveConfiguration("DefaultReminder", Me.nudDefaultReminder.Value) 'Comment against 2595
            SaveConfiguration(nud.Tag, nud.Value) 'Task:2595 Changed Value
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSalaryAccount_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSalaryAccount.Enter, cmbSalaryPayableAccount.Enter, cmbPLAccountId.Enter, cmbExchangeGainLossAccount.Enter, cmbTransitInsuranceAccount.Enter, cmbTaxReceiveableAccount.Enter, cmbTaxPayableAccount.Enter, cmbStoreIssuanceAccount.Enter, cmbSEDAccount.Enter, cmbSalesTaxAccount.Enter, cmbSalesDiscAccount.Enter, cmbSalesAccount.Enter, cmbPurchaseTaxAccount.Enter, cmbPurchaseAccount.Enter, cmbInwardExpenseHeadAccount.Enter, cmbInvAccount.Enter, cmbFuelExpAccount.Enter, cmbDefaultEmail.Enter, cmbCylinderStockAccount.Enter, cmbCGS.Enter, cmbAdjustmentExpAccount.Enter, cmbVoucherFormat.Enter, cmbClaimAccount.Enter, cmbCMFAExpAccountHead.Enter, cmbLCSalesTaxAccount.Enter, cmbExciseDutyAccount.Enter, cmbCustomDutyAccount.Enter, cmbAdvanceIncomeTaxAccount.Enter, cmbAdditionalSalesTaxAccount.Enter, cmbAdditionalCostAccount.Enter, cmbEmployeeHeadAccountId.Enter, cmbEmpDeptHeadAccountId.Enter, cmbCostOfProduction.Enter, cmbSaleTaxDeductioAcId.Enter, CmbPurchaseTaxIDeductionAccountNo.Enter, cmbOtherExpAccount.Enter, cmbCurrency.Enter, cmbRetentionAccount.Enter, cmbMobilizationAccount.Enter, cmbVDQty.Enter, cmbStockOut.Enter, cmbStockIn.Enter, cmbWastedStockAccount.Enter, cmbScrappedStockAccount.FontChanged, cmbExchangeGainLossAccount.Enter, cmbDefaultAccountInPlaceCustomer.Enter, cmbMainAccountforRevenueImport.Enter, cmbSeller.Enter, cmbInvestor.Enter, cmbDealer.Enter, cmbBuyer.Enter, cmbCommisionAccount.Enter, cmbSaleAccount.Enter, cmbSysPuchaseAccount.Enter, cmbInvestmentAccount.Enter, cmbAgent.Enter, cmbAccountsApproval.Enter, cmbProductionApproval.Enter, cmbPurchaseApproval.Enter, cmbQuotationApproval.Enter, cmbSalesApproval.Enter, cmbPurchaseDemandApproval.Enter, cmbPurchaseInquiryApproval.Enter, cmbPurchaseOrderApproval.Enter, cmbPurchaseReturnApproval.Enter, cmbVendorQuotationApproval.Enter, cmbGRNApproval.Enter, cmbVoucherEntryApproval.Enter, cmbPaymentApproval.Enter, cmbReceiptApproval.Enter, cmbExpenseApproval.Enter, cmbActivityFeedBackApproval.Enter, cmbProfitExpenseAccount.Enter, _
       cmbSalesInquiry.Enter, cmbSalesOrder.Enter, cmbSalesQuotation.Enter, cmbDeliveryChallan.Enter, cmbSalesInvoice.Enter, cmbSalesReturn.Enter, _
       cmbCashRequestApproval.Enter, cmbEmployeeLoanRequestApproval.Enter
        Try
            Dim cmb As ComboBox = CType(sender, ComboBox)
            If cmb.Items.Count > 0 Then
                If cmb.SelectedValue IsNot Nothing Then IsChangedValue = cmb.SelectedValue Else IsChangedValue = -1
            Else
                IsChangedValue = -1
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2660
    'Tsk:2359 Added NumericUpDropdown Event
    Private Sub nudAmount_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudAmount.ValueChanged, nudQty.ValueChanged, nudTotalAmountRounding.ValueChanged, nudSMSScheduleTime.ValueChanged
        Try
            If IsOpenForm = False Then Exit Sub
            Dim nud As NumericUpDown = CType(sender, NumericUpDown) 'Set Cast Type
            SaveConfiguration(nud.Tag.ToString, nud.Value.ToString) 'Save Configuration
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim OpenDirectory As New FolderBrowserDialog
            If OpenDirectory.ShowDialog = Windows.Forms.DialogResult.OK Then
                If IO.Directory.Exists(OpenDirectory.SelectedPath) Then
                    Me.txtFilesAttachmentPath.Text = OpenDirectory.SelectedPath
                    SaveConfiguration("FileAttachmentPath", Me.txtFilesAttachmentPath.Text.ToString)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''03-Mar-2014 TASK:2451  Imran Ali  4-ALPHABETIC order of items in sale and purchase window 
    Private Sub chkAcAscending_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAcAscending.CheckedChanged, _
    chkItemDescending.CheckedChanged, chkAcAscending.CheckedChanged, chkAcDescending.CheckedChanged, rbtAcSortOrder.CheckedChanged, rbtAcSortOrderByCode.CheckedChanged, rbtAcSortOrderByName.CheckedChanged, _
    rbtItemSortOrder.CheckedChanged, rbtItemSortOrderByCode.CheckedChanged, rbtItemSortOrderByName.CheckedChanged, rbtSimpleEmpAc.CheckedChanged, rbtDeptEmpAc.CheckedChanged, rbtListSearchContains.CheckedChanged, rbtListSearchStartWith.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            Dim rbt As RadioButton = CType(sender, RadioButton)
            Select Case rbt.Name
                Case Me.rbtSimpleEmpAc.Name
                    If Me.rbtSimpleEmpAc.Checked = True Then
                        Me.cmbEmpDeptHeadAccountId.Visible = False
                        Me.cmbEmployeeHeadAccountId.Visible = True
                        Me.lblAccountLevel.Text = "Sub Sub Account From Chart of Accout"
                    Else
                        Me.cmbEmpDeptHeadAccountId.Visible = True
                        Me.cmbEmployeeHeadAccountId.Visible = False
                        Me.lblAccountLevel.Text = "Sub Account From Chart of Account"
                    End If
                Case Me.rbtDeptEmpAc.Name
                    If Me.rbtDeptEmpAc.Checked = True Then
                        Me.cmbEmployeeHeadAccountId.Visible = False
                        Me.cmbEmpDeptHeadAccountId.Visible = True
                        Me.lblAccountLevel.Text = "Sub Account From Chart of Account"
                    Else
                        Me.cmbEmployeeHeadAccountId.Visible = True
                        Me.cmbEmpDeptHeadAccountId.Visible = False
                        Me.lblAccountLevel.Text = "Sub Sub Account From Chart of Accout"
                    End If
            End Select
            SaveConfiguration(rbt.Tag.ToString(), rbt.Checked)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2451


    'Task No.2537  Append The Leave Event for newly added combobox of account tax deduction AC id
    'Private Sub cmbSaleTaxDeductioAcId_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSaleTaxDeductioAcId.Leave, cmbSalaryPayableAccount.Leave, cmbPLAccountId.Leave, cmbTransitInsuranceAccount.Leave, cmbTaxReceiveableAccount.Leave, cmbTaxPayableAccount.Leave, cmbStoreIssuanceAccount.Leave, cmbSEDAccount.Leave, cmbSalesTaxAccount.Leave, cmbSalesDiscAccount.Leave, cmbSalesAccount.Leave, cmbPurchaseTaxAccount.Leave, cmbPurchaseAccount.Leave, cmbOtherExpAccount.Leave, cmbInwardExpenseHeadAccount.Leave, cmbInvAccount.Leave, cmbFuelExpAccount.Leave, cmbDefaultEmail.Leave, cmbCylinderStockAccount.Leave, cmbCostOfProduction.Leave, cmbCGS.Leave, cmbAdjustmentExpAccount.Leave, cmbVoucherFormat.Leave, CmbPurchaseTaxIDeductionAccountNo.Leave
    '    Try
    '        If IsOpenForm = False Then Exit Sub
    '        Dim cmb As Windows.Forms.ComboBox = CType(sender, Windows.Forms.ComboBox)
    '        If cmb.SelectedIndex = -1 Then Exit Sub
    '        If cmb.SelectedValue IsNot Nothing Then
    '            If IsChangedValue.ToString <> cmb.SelectedValue.ToString Then SaveConfiguration(cmb.Tag, cmb.SelectedValue)
    '        Else
    '            If IsChangedValue.ToString <> cmb.Text.ToString Then If cmb.Text.Length > 0 Then SaveConfiguration(cmb.Tag, cmb.Text)
    '        End If
    '        IsChangedValue = 0
    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub cmbSaleTaxDeductioAcId_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSalaryPayableAccount.Enter, cmbPLAccountId.Enter, cmbTransitInsuranceAccount.Enter, cmbTaxReceiveableAccount.Enter, cmbTaxPayableAccount.Enter, cmbStoreIssuanceAccount.Enter, cmbSEDAccount.Enter, cmbSalesTaxAccount.Enter, cmbSalesDiscAccount.Enter, cmbSalesAccount.Enter, cmbPurchaseTaxAccount.Enter, cmbPurchaseAccount.Enter, cmbInwardExpenseHeadAccount.Enter, cmbInvAccount.Enter, cmbFuelExpAccount.Enter, cmbDefaultEmail.Enter, cmbCylinderStockAccount.Enter, cmbCGS.Enter, cmbAdjustmentExpAccount.Enter, cmbVoucherFormat.Enter
    '    Try

    '        Dim cmb As ComboBox = CType(sender, ComboBox)
    '        If cmb.Items.Count > 0 Then
    '            IsChangedValue = cmb.SelectedValue
    '        Else
    '            IsChangedValue = 0
    '        End If

    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Task No.2537  Append The Leave Event for newly added combobox of Purchase account tax deduction AC id


    Private Sub CmbPurchaseTaxIDeductionAccountNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbPurchaseTaxIDeductionAccountNo.Leave, cmbSaleTaxDeductioAcId.Leave, cmbPLAccountId.Leave, cmbExchangeGainLossAccount.Leave, cmbTransitInsuranceAccount.Leave, cmbTaxReceiveableAccount.Leave, cmbTaxPayableAccount.Leave, cmbStoreIssuanceAccount.Leave, cmbSEDAccount.Leave, cmbSalesTaxAccount.Leave, cmbSalesDiscAccount.Leave, cmbSalesAccount.Leave, cmbPurchaseTaxAccount.Leave, cmbPurchaseAccount.Leave, cmbOtherExpAccount.Leave, cmbInwardExpenseHeadAccount.Leave, cmbInvAccount.Leave, cmbFuelExpAccount.Leave, cmbDefaultEmail.Leave, cmbCylinderStockAccount.Leave, cmbCostOfProduction.Leave, cmbCGS.Leave, cmbAdjustmentExpAccount.Leave, cmbVoucherFormat.Leave, txtTransitInssuranceTax.Leave, cmbLCSalesTaxAccount.Leave, cmbExciseDutyAccount.Leave, cmbCustomDutyAccount.Leave, cmbCMFAExpAccountHead.Leave, cmbClaimAccount.Leave, cmbAdvanceIncomeTaxAccount.Leave, cmbAdditionalSalesTaxAccount.Leave, cmbAdditionalCostAccount.Leave, cmbAdditionalTaxAccount.Leave, cmbSMSLanguage.Leave, cmbCurrency.Leave, cmbCostSheetType.Leave, cmbSalaryAccount.Leave, cmbSalaryPayableAccount.Leave, cmbEmployeeHeadAccountId.Leave, cmbEmpDeptHeadAccountId.Leave, cmbRetentionAccount.Leave, cmbMobilizationAccount.Leave, cmbVDQty.Leave, cmbStockOut.Leave, cmbStockIn.Leave, cmbWastedStockAccount.Leave, cmbScrappedStockAccount.Leave, cmbExchangeGainLossAccount.Leave, cmbDefaultAccountInPlaceCustomer.Leave, cmbMainAccountforRevenueImport.Leave, cmbSeller.Leave, cmbInvestor.Leave, cmbDealer.Leave, cmbBuyer.Leave, cmbCommisionAccount.Leave, cmbSaleAccount.Leave, cmbSysPuchaseAccount.Leave, cmbInvestmentAccount.Leave, cmbAgent.Leave, cmbAccountsApproval.Leave, cmbProductionApproval.Leave, cmbPurchaseApproval.Leave, cmbQuotationApproval.Leave, cmbSalesApproval.Leave, cmbPurchaseDemandApproval.Leave, cmbPurchaseInquiryApproval.Leave, cmbPurchaseOrderApproval.Leave, cmbPurchaseReturnApproval.Leave, cmbVendorQuotationApproval.Leave, cmbGRNApproval.Leave, cmbVoucherEntryApproval.Leave, cmbPaymentApproval.Leave, cmbReceiptApproval.Leave, cmbExpenseApproval.Leave, cmbActivityFeedBackApproval.Leave, cmbProfitExpenseAccount.Leave, _
                cmbSalesInquiry.Leave, cmbSalesOrder.Leave, cmbSalesQuotation.Leave, cmbDeliveryChallan.Leave, cmbSalesInvoice.Leave, cmbSalesReturn.Leave, _
                cmbCashRequestApproval.Leave, cmbEmployeeLoanRequestApproval.Leave

        Try
            If IsOpenForm = False Then Exit Sub
            Dim cmb As Windows.Forms.ComboBox = CType(sender, Windows.Forms.ComboBox)
            If cmb.SelectedIndex = -1 Then Exit Sub

            If cmb.SelectedValue IsNot Nothing Then
                If IsChangedValue.ToString <> cmb.SelectedValue.ToString Then SaveConfiguration(cmb.Tag, cmb.SelectedValue)
            Else
                If IsChangedValue.ToString <> cmb.Text.ToString Then If cmb.Text.Length > 0 Then SaveConfiguration(cmb.Tag, cmb.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub
    'Private Sub CmbPurchaseTaxIDeductionAccountNo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbPurchaseTaxIDeductionAccountNo.Enter, cmbSaleTaxDeductioAcId.Enter, cmbSalaryPayableAccount.Enter, cmbPLAccountId.Enter, cmbTransitInsuranceAccount.Enter, cmbTaxReceiveableAccount.Enter, cmbTaxPayableAccount.Enter, cmbStoreIssuanceAccount.Enter, cmbSEDAccount.Enter, cmbSalesTaxAccount.Enter, cmbSalesDiscAccount.Enter, cmbSalesAccount.Enter, cmbPurchaseTaxAccount.Enter, cmbPurchaseAccount.Enter, cmbOtherExpAccount.Enter, cmbInwardExpenseHeadAccount.Enter, cmbInvAccount.Enter, cmbFuelExpAccount.Enter, cmbDefaultEmail.Enter, cmbCylinderStockAccount.Enter, cmbCostOfProduction.Enter, cmbCGS.Enter, cmbAdjustmentExpAccount.Enter, cmbVoucherFormat.Enter
    '    Try
    '        Dim cmb As ComboBox = CType(sender, ComboBox)
    '        If cmb.Items.Count > 0 Then
    '            IsChangedValue = cmb.SelectedValue
    '        Else
    '            IsChangedValue = 0
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            If Me.FolderBrowserDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtCMFADocumentAttachmentPath.Text = FolderBrowserDialog2.SelectedPath
                SaveConfiguration("CMFADocumentAttachmentPath", Me.txtCMFADocumentAttachmentPath.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDNSHost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDNSHost.Click
        Try
            Dim frmD As New frmDomains
            ApplyStyleSheet(frmD)
            If frmD.ShowDialog = Windows.Forms.DialogResult.Yes Then
                Me.txtDNSHost.Text = frmD._HostName.ToString
                SaveConfiguration("DNSHostForSMS", Me.txtDNSHost.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbDayOff_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDayOff.LostFocus
        Try
            Dim str As String = String.Empty
            For i As Integer = 0 To Me.cmbDayOff.CheckedItems.Count - 1
                str += cmbDayOff.CheckedItems(i).ToString & ","
            Next
            If Not str = String.Empty Then
                str = str.Substring(0, str.LastIndexOf(","))
            End If
            SaveConfiguration(Me.cmbDayOff.Tag, str)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBrowseCompanyLogo_Click(sender As Object, e As EventArgs) Handles btnBrowseCompanyLogo.Click
        Try
            'Task#1 13-Jun-2015 Check if Path greater then zero then if path doesn't exist then create path first
            If _CompanyLogoPath.Length > 0 Then
                If Not System.IO.Directory.Exists(_CompanyLogoPath) Then
                    System.IO.Directory.CreateDirectory(_CompanyLogoPath)
                End If
            End If
            'End Task#1 13-Jun-2015

            If Not IO.Directory.Exists(_CompanyLogoPath) Then
                ShowErrorMessage("Folder not exist")
                Me.btnBrowseCompanyLogo.Focus()
                Exit Sub
            End If

            Me.OpenFileDialog2.Filter = "Image File |*.*png"
            If OpenFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                If System.IO.File.Exists(Me.OpenFileDialog2.FileName) Then
                    _strImagePath = _CompanyLogoPath & "\" + OpenFileDialog2.FileName.Replace(OpenFileDialog2.FileName, "CompanyLogo.png") '_CompanyLogoPath & "\" +
                    Me.pbCompanyLogo.ImageLocation = OpenFileDialog2.FileName

                    SavePicture("CompanyLogoConfiguration", _strImagePath)
                    If Not _strImagePath = String.Empty Then
                        If Not pbCompanyLogo Is Nothing Then
                            Try
                                'Dim DirInfo As IO.DirectoryInfo = New IO.DirectoryInfo(_CompanyLogoPath)
                                'Dim FolderSecurity As New Security.AccessControl.DirectorySecurity
                                'FolderSecurity.AddAccessRule(New Security.AccessControl.FileSystemAccessRule(Environment.UserDomainName & "\\" & Environment.UserName, Security.AccessControl.FileSystemRights.FullControl, Security.AccessControl.AccessControlType.Allow))
                                'DirInfo.SetAccessControl(FolderSecurity)


                                If IO.File.Exists(_strImagePath) Then
                                    IO.File.Delete(_strImagePath)

                                    pbCompanyLogo.Image.Save(_strImagePath, System.Drawing.Imaging.ImageFormat.Png)

                                Else
                                    'pbCompanyLogo.Image
                                    If Not pbCompanyLogo.Image Is Nothing Then
                                        pbCompanyLogo.Image.Save(_strImagePath, System.Drawing.Imaging.ImageFormat.Png)
                                    End If
                                End If

                            Catch ex As Exception
                                ShowErrorMessage(ex.Message)


                            End Try
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
        End Try
    End Sub
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            If Me.FolderBrowserDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtBacklocation.Text = FolderBrowserDialog2.SelectedPath
                SaveConfiguration("DatabaseBackup", Me.txtBacklocation.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cbMon_CheckedChanged(sender As Object, e As EventArgs) Handles cbWed.CheckedChanged, cbTue.CheckedChanged, cbThu.CheckedChanged, cbSun.CheckedChanged, cbSta.CheckedChanged, cbMon.CheckedChanged, cbFri.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim strValues As String = String.Empty

            strValues += "Mon^" & cbMon.Checked & "|"
            strValues += "Tue^" & cbTue.Checked & "|"
            strValues += "Wed^" & cbWed.Checked & "|"
            strValues += "Thu^" & cbThu.Checked & "|"
            strValues += "Fri^" & cbFri.Checked & "|"
            strValues += "Sat^" & cbSta.Checked & "|"
            strValues += "Sun^" & cbSun.Checked

            If chk.Tag.ToString.Length > 0 Then SaveConfiguration(chk.Tag, strValues)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rbAnyTime_CheckedChanged(sender As Object, e As EventArgs) Handles rbSpecificTime.CheckedChanged, rbAnyTime.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            Dim rb As RadioButton = CType(sender, RadioButton)
            Dim strValues As String = String.Empty
            Select Case rb.Name
                Case rbAnyTime.Name
                    strValues += "Any^10:00 AM"
                Case rbSpecificTime.Name
                    strValues += "St^" & dtpStartat.Value.ToShortTimeString() & "|"
                    strValues += dtpEndat.Value.ToShortTimeString()
            End Select
            If rb.Tag.ToString.Length > 0 Then SaveConfiguration(rb.Tag, strValues)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub frmSystemConfigurationNew1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            SelectTab()
            SetStartEndDate()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub SetStartEndDate()
        Try
            Me.dtpStartat.Format = DateTimePickerFormat.Custom
            Me.dtpStartat.ShowUpDown = True
            Me.dtpStartat.CustomFormat = "hh:mm:ss tt"
            Me.dtpEndat.Format = DateTimePickerFormat.Custom
            Me.dtpEndat.ShowUpDown = True
            Me.dtpEndat.CustomFormat = "hh:mm:ss tt"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub dtpStartat_ValueChanged(sender As Object, e As EventArgs) Handles dtpStartat.ValueChanged, dtpEndat.ValueChanged
        Try
            If IsOpenForm = False Then Exit Sub
            Dim dtp As DateTimePicker = CType(sender, DateTimePicker)
            Dim strValues As String = String.Empty

            strValues += "St^" & dtpStartat.Value.ToShortTimeString() & "|"
            strValues += dtpEndat.Value.ToShortTimeString()

            If dtp.Tag.ToString.Length > 0 Then SaveConfiguration(dtp.Tag, strValues)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                'DatabaseBackupPassword
                Dim enc As String = Encrypt(Me.txtPassword.Text.ToString())
                Dim IsPassword = GetConfigValue("DatabaseBackupPassword").ToString
                If IsPassword.ToString.Length > 0 Then
                    msg_Error("Password has already been set. You can change it by providing old one is required")
                Else
                    SaveConfiguration("DatabaseBackupPassword", enc)
                    msg_Information("Password has been created successfully")
                    If Me.lblLinkChange.Visible = False Then
                        Me.lblLinkChange.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Public Sub SelectTab()
        Try
            If IsOpenForm = False Then Exit Sub
            If ScreenName = String.Empty Then Exit Sub
            If ScreenName = enmScreen.Accounts Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0)
            ElseIf ScreenName = enmScreen.ImportDocuments Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1)
            ElseIf ScreenName = enmScreen.Sales Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(2)
            ElseIf ScreenName = enmScreen.Purchase Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(3)
            ElseIf ScreenName = enmScreen.Inventory Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(4)
            ElseIf ScreenName = enmScreen.Admistrator Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(5)
            ElseIf ScreenName = enmScreen.Email Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(6)
            ElseIf ScreenName = enmScreen.Utility Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(7)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UltraTabPageControl8_Paint(sender As Object, e As PaintEventArgs) Handles UltraTabPageControl8.Paint

    End Sub

    Private Sub txtNewPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNewPassword.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                'DatabaseBackupPassword
                Dim enc As String = Encrypt(Me.txtOldPassword.Text.ToString())
                Dim IsPassword As String = GetConfigValue("DatabaseBackupPassword").ToString

                If IsPassword.ToString.Length > 0 AndAlso IsPassword <> "Error" AndAlso Not enc.Equals(IsPassword) Then
                    msg_Error("Old password you have entered is not correct")
                Else
                    Dim NewEncrypt As String = Encrypt(Me.txtNewPassword.Text.ToString())

                    SaveConfiguration("DatabaseBackupPassword", NewEncrypt, True)
                    msg_Information("New password has been created successfully")
                    GetSecurityRightsForBackup()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblLinkChange_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblLinkChange.LinkClicked
        Try
            If Me.txtOldPassword.Visible = False AndAlso Me.txtNewPassword.Visible = False AndAlso Me.lblOld.Visible = False AndAlso Me.lblNew.Visible = False Then
                Me.txtOldPassword.Visible = True
                Me.txtNewPassword.Visible = True
                Me.lblOld.Visible = True
                Me.lblNew.Visible = True
            Else
                Me.txtOldPassword.Visible = False
                Me.txtNewPassword.Visible = False
                Me.lblOld.Visible = False
                Me.lblNew.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblLinkChange_DoubleClick(sender As Object, e As EventArgs) Handles lblLinkChange.DoubleClick
        Try
            Me.txtOldPassword.Visible = False
            Me.txtNewPassword.Visible = False
            Me.lblOld.Visible = False
            Me.lblNew.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRightsForBackup()
        Try

            Me.gbPassword.Visible = False
            Me.gbPassword.Enabled = False
            Me.txtOldPassword.Visible = False
            Me.txtNewPassword.Visible = False
            Me.gbBackupLocation.Location = New Point(19, 213)

            If Rights Is Nothing Then

                Exit Sub
            End If



            For Each RightstDt As GroupRights In Rights
                If RightstDt.FormControlName = "Backup Password Change" Then
                    Dim Pass As String = getConfigValueByType("DatabaseBackupPassword").ToString()

                    If Pass.Length > 0 AndAlso Pass <> "Error" Then
                        Pass = Decrypt(Pass)
                        Me.txtPassword.Text = Pass
                    Else
                        Me.txtPassword.Text = ""
                    End If

                    Me.gbPassword.Visible = True
                    Me.gbPassword.Enabled = True
                    Me.gbBackupLocation.Location = New Point(19, 266)

                    Me.txtPassword.Visible = True
                    Me.txtPassword.Enabled = False
                    Me.txtOldPassword.Visible = False
                    Me.txtNewPassword.Visible = False
                    Me.lblOld.Visible = False
                    Me.lblNew.Visible = False
                    Me.lblLinkChange.Visible = True

                    'Else
                    '    Me.txtPassword.Visible = True
                    '    Me.txtPassword.Text = ""
                    '    Me.txtPassword.Enabled = True
                    '    Me.txtOldPassword.Visible = False
                    '    Me.txtNewPassword.Visible = False
                    '    Me.lblOld.Visible = False
                    '    Me.lblNew.Visible = False
                    '    Me.lblLinkChange.Visible = False
                    '    Me.gbBackupLocation.Location = New Point(19, 266)

                    'Me.txtPassword.Enabled = True
                    'Me.txtOldPassword.Enabled = True
                    'Me.txtNewPassword.Enabled = True
                    'Me.lblOld.Enabled = True
                    'Me.lblNew.Enabled = True
                Else
                    'Me.gbPassword.Visible = False
                    'Me.gbPassword.Enabled = False
                    'Me.txtOldPassword.Visible = False
                    'Me.txtNewPassword.Visible = False
                    'Me.gbBackupLocation.Location = New Point(19, 213)
                    'Me.txtPassword.Visible = False
                    'Me.txtOldPassword.Visible = False
                    'Me.txtNewPassword.Visible = False
                    'Me.lblOld.Visible = False
                    'Me.lblNew.Visible = False



                    'Me.txtPassword.Enabled = False
                    'Me.txtOldPassword.Enabled = False
                    'Me.txtNewPassword.Enabled = False
                    'Me.lblOld.Enabled = False
                    'Me.lblNew.Enabled = False



                End If
                If RightstDt.FormControlName = "View" Then

                End If
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If Asc(e.KeyChar) = Keys.Space Then
            MessageBox.Show("Space is not allowed")
        End If
        'If (e.KeyChar) = "" Then
        '    MessageBox.Show("Space is not allowed")
        '    e.Handled = True
        'End If
    End Sub



    Private Sub cmbDayOff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDayOff.SelectedIndexChanged
        Try
            Dim str As String = String.Empty
            For i As Integer = 0 To Me.cmbDayOff.CheckedItems.Count - 1
                str += cmbDayOff.CheckedItems(i).ToString & ","
            Next
            If Not str = String.Empty Then
                str = str.Substring(0, str.LastIndexOf(","))
            End If
            SaveConfiguration(Me.cmbDayOff.Tag, str)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCostSheetType_Click(sender As Object, e As EventArgs) Handles btnCostSheetType.Click

    End Sub


    'Private Sub cmbVDQty_Enter(sender As Object, e As EventArgs) Handles cmbVDQty.Enter
    '    Try
    '        Dim cmb As ComboBox = CType(sender, ComboBox)
    '        If cmb.Items.Count > 0 Then
    '            If cmb.SelectedValue IsNot Nothing Then IsChangedValue = cmb.SelectedValue Else IsChangedValue = -1
    '        Else
    '            IsChangedValue = -1
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub cmbVDQty_Leave(sender As Object, e As EventArgs)
    '    Try
    '        If IsOpenForm = False Then Exit Sub
    '        Dim cmb As Windows.Forms.ComboBox = CType(sender, Windows.Forms.ComboBox)
    '        If cmb.SelectedIndex = -1 Then Exit Sub

    '        If cmb.SelectedValue IsNot Nothing Then
    '            If IsChangedValue.ToString <> cmb.SelectedValue.ToString Then SaveConfiguration(cmb.Tag, cmb.SelectedValue)
    '        Else
    '            If IsChangedValue.ToString <> cmb.Text.ToString Then If cmb.Text.Length > 0 Then SaveConfiguration(cmb.Tag, cmb.Text)
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub cmbVDQty_Leave_1(sender As Object, e As EventArgs)

    '    Try
    '        If IsOpenForm = False Then Exit Sub
    '        Dim cmb As Windows.Forms.ComboBox = CType(sender, Windows.Forms.ComboBox)
    '        If cmb.SelectedIndex = -1 Then Exit Sub

    '        If cmb.SelectedValue IsNot Nothing Then
    '            If IsChangedValue.ToString <> cmb.SelectedValue.ToString Then SaveConfiguration(cmb.Tag, cmb.SelectedValue)
    '        Else
    '            If IsChangedValue.ToString <> cmb.Text.ToString Then If cmb.Text.Length > 0 Then SaveConfiguration(cmb.Tag, cmb.Text)
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub
    ''' <summary>
    ''' TASK TFS1378
    ''' </summary>
    ''' <remarks> Check opened delivery chalans</remarks>
    Private Function CheckOpenDCs() As Boolean
        Dim checkOpen As String = ""
        Try
            checkOpen = "SELECT IsNull(DeliveryId, 0) As DeliveryId FROM DeliveryChalanMasterTable WHERE Status ='Open' "
            Dim dtCheckOpen As DataTable = GetDataTable(checkOpen)
            If dtCheckOpen.Rows.Count > 0 AndAlso dtCheckOpen.Rows(0).Item(0) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS1378
    ''' </summary>
    ''' <remarks> Check opened delivery chalans</remarks>
    Private Function CheckOpenGRNs() As Boolean
        Dim checkOpen As String = ""
        Try
            ''Below line is commented on 08-02-2017
            'checkOpen = "SELECT IsNull(ReceivingNoteId, 0) As ReceivingNoteId FROM ReceivingNoteMasterTable WHERE ReceivingNoteId NOT IN (SELECT DISTINCT CASE WHEN IsNull(ReceivingDetail.ReceivingNoteId, 0) > 0 THEN IsNull(ReceivingDetail.ReceivingNoteId, 0) ELSE IsNull(Receiving.ReceivingNoteId, 0) END As ReceivingNoteId From ReceivingDetailTable AS ReceivingDetail INNER JOIN ReceivingMasterTable AS Receiving ON ReceivingDetail.ReceivingId = Receiving.ReceivingId) AND ReceivingNoteId NOT IN (SELECT DISTINCT CASE WHEN IsNull(LCDetailTable.ReceivingNoteId, 0) > 0 THEN IsNull(LCDetailTable.ReceivingNoteId, 0) ELSE IsNull(LCMasterTable.ReceivingNoteId, 0) END As ReceivingNoteId From LCMasterTable INNER JOIN LCDetailTable ON LCMasterTable.LCId = LCDetailTable.LCId)"
            checkOpen = "SELECT IsNull(ReceivingNoteId, 0) As ReceivingNoteId FROM ReceivingNoteMasterTable WHERE ReceivingNoteId NOT IN (SELECT DISTINCT CASE WHEN IsNull(ReceivingDetail.ReceivingNoteId, 0) > 0 THEN IsNull(ReceivingDetail.ReceivingNoteId, 0) ELSE IsNull(Receiving.ReceivingNoteId, 0) END As ReceivingNoteId From ReceivingDetailTable AS ReceivingDetail INNER JOIN ReceivingMasterTable AS Receiving ON ReceivingDetail.ReceivingId = Receiving.ReceivingId) AND ReceivingNoteId NOT IN (SELECT DISTINCT CASE WHEN IsNull(LCDetailTable.ReceivingNoteId, 0) > 0 THEN IsNull(LCDetailTable.ReceivingNoteId, 0) ELSE IsNull(LCMasterTable.ReceivingNoteId, 0) END As ReceivingNoteId From LCMasterTable INNER JOIN LCDetailTable ON LCMasterTable.LCId = LCDetailTable.LCId) AND ReceivingNo NOT IN(SELECT DocNo FROM StockMasterTable)"
            Dim dtCheckOpen As DataTable = GetDataTable(checkOpen)
            If dtCheckOpen.Rows.Count > 0 AndAlso dtCheckOpen.Rows(0).Item(0) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS2273
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DisplayOpenedGRNs() As Boolean
        Dim checkOpen As String = ""
        Try
            checkOpen = "SELECT ReceivingNo FROM ReceivingNoteMasterTable WHERE ReceivingNoteId NOT IN (SELECT DISTINCT CASE WHEN IsNull(ReceivingDetail.ReceivingNoteId, 0) > 0 THEN IsNull(ReceivingDetail.ReceivingNoteId, 0) ELSE IsNull(Receiving.ReceivingNoteId, 0) END As ReceivingNoteId From ReceivingDetailTable AS ReceivingDetail INNER JOIN ReceivingMasterTable AS Receiving ON ReceivingDetail.ReceivingId = Receiving.ReceivingId) AND ReceivingNoteId NOT IN (SELECT DISTINCT CASE WHEN IsNull(LCDetailTable.ReceivingNoteId, 0) > 0 THEN IsNull(LCDetailTable.ReceivingNoteId, 0) ELSE IsNull(LCMasterTable.ReceivingNoteId, 0) END As ReceivingNoteId From LCMasterTable INNER JOIN LCDetailTable ON LCMasterTable.LCId = LCDetailTable.LCId) AND ReceivingNo NOT IN(SELECT DocNo FROM StockMasterTable) "
            Dim dtCheckOpen As DataTable = GetDataTable(checkOpen)
            Dim _List As List(Of String) = dtCheckOpen.AsEnumerable().Select(Function(dr) dr.Field(Of String)(0)).ToList
            'Dim _List As List(Of String) = dtCheckOpen.Rows.Cast(Of DataRow).Select(Function(dr) dr(0).ToString).ToList
            Dim _Message = String.Join(Environment.NewLine, _List)
            MsgBox(_Message, MsgBoxStyle.Information, "Following GRNs are opened. Please close them.")
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub lnkAddAccount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkAddAccount.LinkClicked
        frmMiscAccountsonSales.ShowDialog()
    End Sub



    Private Sub UltraTabPageControl3_Paint(sender As Object, e As PaintEventArgs) Handles UltraTabPageControl3.Paint

    End Sub
End Class