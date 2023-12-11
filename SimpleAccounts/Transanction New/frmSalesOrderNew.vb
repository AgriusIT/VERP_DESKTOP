''14-Dec-2013 R916  Imran Ali               Add comments column
''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction formstxtLastPrice
''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''28-Dec-2013 RM6       Imran Ali           Release 2.1.0.0 Bug
''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
''1-Jan-2014 Tsk:2366   Imran Ali         Slow working load forms
''2-Jan-2014   Tsk:2367    Imran Ali             Calculation Problem
''11-Jan-2014 Task:2373         Imran Ali                Add Columns SubSub Title in Account List on Sales/Purchase
''11-Jan-2014 Task:2374           Imran Ali                Net Amount Sales/Purchase 
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
''03-Feb-2014        Task:2406   Imran Ali    FIELD CHOOSER restriction (Senior Rozgar)
''06-Feb-2014          TASK:M16     Imran Ali   Add New Fields Engine No And Chassis No. on Sales 
''07-Feb-2014        TASK:2417     Imran Ali     Item Filter Customer Wise On Sales Order
''15-Feb-2014 Task:2426 Imran Ali Payment Schedule On Sales Order And Purchase Order
''17-Feb-2014  Task:2427 Imran Ali  Dispatch Detail Repor Problem.
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
''28-Feb-2014  TASK:24445 Imran Ali   Last purchase and sale price show on sale order and purchase order
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
''18-Mar-2014 TASK:2504 Imran Ali Credit Limit on Sales Order
'16-May-2014 Task: 2626 Junaid project column added in sale / purchase and both returns in history tab.
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''23-Aug-2014 Task:2801 Imran Ali Sales Order Update 
''06-Sep-2014 Task:2832 Imran Ali Production Plan Status (Converters)
' 2015-02-20 Changes Against Task# 7 Ali Ansari Add PackQty in Selection 2015-02-20
''Ahmad Sharif: Update Query for Last Price of Same Customer for same item, 06-06-2015
''04-Jun-2015 Task:2015060001 Ali Ansari Regarding Attachements 
'08-Jun-2015  Task#2015060005 to allow all files to attach

''09-Jun-2015 Task# A3-09-06-2015, Ahmad Sharif: Added Key Pres event for some textboxes to take just numeric, dot value
''10-June-2015 Task# A2-10-06-2015 Ahmad Sharif: Add Check on grdSaved to check on double click if row less than zero than exit
''10-June-2015 Task# A1-10-06-2015 Ahmad Sharif: Add Error message when customer not selectd
''10-June-2015 Task# 2015060007 to get latest terms and conditions
'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'08-Aug-2015 Task#08082015 Ahmad Sharif: Add configuration for sending sms with just item qunatity and engine no
'' 22-Jul-2016 TASK-SP22072016 Muhammad Ameen: New field SpecialInstructions to SalesOrder
'' 08-08-2017 TASK: TFS1268 Muhammad Ameen: Allow user to update delivered Sales Order in certain condition.
''TASK TFS1474 Muhammad Ameen on 15-09-2017. Currency rate can not be edited while base currency is selected.
''TASK TFS1648 Muhammad Ameen on 03-11-2017. Item's quantity merging should also look location.
''TASK TFS1764 Muhammad Ameen on 15-11-2017. Addition of new transporter field
''TASK TFS3700 Muhammad Ameen has done to have currency combo editable and reflection of currency rate change should be occured in grid values. Dated 28-06-2018
''TASK TFS3758 Ayesha Rehman : 05-07-2018. Reverse Calculation of Tax not Applied on SO 
''TFS4161 Ayesha Rehman : 09-08-2018 : P QTY: (Should Be Static/ Un-Changeable / Un-Editable on All Screens)
''TFS4689 Ayesha Rehman : 03-10-2018 : Show only relevant Accounts on Transactional screens while User wise COA Configuration.
Imports System.Data.OleDb
'Imports System.Diagnostics
Imports SBDal
Imports SBModel
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports SBUtility.Utility
Imports System.Text
Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions

Public Class frmSalesOrderNew
    ' Change on 23-11-2013  For Multiple Print Vouchers
    Dim dt As DataTable
    Dim Mode As String = "Normal"
    Dim IsEditMode As Boolean = False
    Dim IsFormOpen As Boolean = False
    Dim Email As Email
    Dim IsSalesOrderAnalysis As Boolean = False
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim setVoucherNo As String = String.Empty
    Dim getVoucher_Id As Integer = 0
    Dim Total_Amount As Double = 0D
    Dim setEditMode As Boolean = False
    Dim crpt As New ReportDocument
    Dim Previouse_Amount As Double = 0D
    Dim SchemeQty As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim flgCompanyRights As Boolean = False
    Dim Adjustment As Double = 0D
    Dim TransitPercent As Double = 0D
    Dim WHTaxPercent As Double = 0D
    Dim NetAmount As Double = 0D
    Dim flgLoadItems As Boolean = False
    Dim flgLocationWiseItem As Boolean = False
    Dim flgVehicleIdentificationInfo As Boolean = False 'Task:M16 Added Flag Vehicle Identification
    Dim blnOpenSO As Boolean = False 'Task:2801 Open Sales Order Status
    Dim SalesOrderNo As String
    'Marked Against Task#2015060001 Ali Ansari
    'Dim arrfile As String
    'Marked Against Task#2015060001 Ali Ansari
    'Altered Against Task#2015060001 Ali Ansari
    ' Convert string into List of string for making proper count
    Dim arrFile As List(Of String)
    'Altered Against Task#2015060001 Ali Ansari
    Private _PrivousBalance As Double = 0D
    Private blnDisplayAll As Boolean = False
    Dim BaseCurrencyId As Integer
    Dim BaseCurrencyName As String = String.Empty
    Dim IsRevisionRestrictedFirstTime As Boolean = False
    Dim objArticle As Article
    Dim objArticleDAL As New ArticleDAL
    Dim dalPurchaseInquiryDetail As New PurchaseInquiryDetailDAL
    Dim NotificationDAL As New NotificationTemplatesDAL
    Dim flgSOUpdateAfterDelivery As Boolean = False
    Dim UpdateDeliveredSO As Boolean = False
    Dim flgMargeItem As Boolean = False
    Dim TransporterCommentsSales As Boolean = False
    Dim AllDispatchLocation As Boolean = False
    Public Const DiscountType_Percentage As String = "Percentage" ''TFS2827
    Public Const DiscountType_Flat As String = "Flat" ''TFS2827

    ''TFS3113 : Abubakar Siddiq : This Variable is Added to check ApprovalProcessId ,if it is mapped against the document
    Dim ApprovalProcessId As Integer = 0
    ''TFS3113 : Abubakar Siddiq :End
    Dim Total As Double = 0 ''TFS3330 : Ayesha Rehman : This Total is Added to retain the value of total ,when pack rate changes
    Dim CurrencyRate As Double = 1
    Dim flgExcludeTaxPrice As Boolean = False ''TFS3758
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Dim ItemFilterByName As Boolean = False
    Dim flgRemoveAttachment As Boolean = False ''TFS4652
    Dim EmailTemplate As String = String.Empty
    Dim UsersEmail As List(Of String)
    Dim EmailBody As String = String.Empty
    Dim SalesOrderId As Integer
    Dim AllFields As List(Of String)
    Dim AfterFieldsElement As String = String.Empty
    Dim dtEmail As DataTable
    Dim EmailDAL As New EmailTemplateDAL
    Dim html As StringBuilder
    Dim VendorEmails As String = String.Empty


    Enum GrdEnum
        SerialNo
        LocationId
        ArticleCode
        Item
        ArticleAlias
        Size
        Color
        Unit
        Qty
        PostDiscountPrice  'TFS2827
        Rate
        BaseCurrencyId
        BaseCurrencyRate
        CurrencyId
        CurrencyRate
        CurrencyAmount
        TotalCurrencyAmount
        DiscountId    'TFS2827
        DiscountFactor  'TFS2827
        DiscountValue  'TFS2827
        Total
        CategoryId
        ItemId
        PackQty
        CurrentPrice
        PackPrice
        TradePrice
        SalesTax_Percentage
        TaxAmount 'Task:2374 Added Index
        CurrencyTaxAmount
        SED_Tax_Percent
        SED_Tax_Amount
        CurrencySEDAmount
        TotalAmount 'Task:2374 Added Index
        SchemeQty
        Discount_Percentage
        Freight
        MarketReturns
        PurchasePrice
        Pack_Desc
        ''R916 Added Index Comments
        Comments
        '' End R916
        OtherComments
        Engine_No 'Task:M16 Added Index
        Chassis_No 'Task:M16 Added Index
        BillValueAfterDiscount
        DeliveredQty 'TAsk:2801 Added Index
        PlanedQty 'TAsk:2832 Added Index
        CostPrice
        SalesOrderDetailId
        OuotationDetailId
        QuotationId
        SOQuantity
        SaleOrderType
        TotalQuantity
        BatchNo
        ExpiryDate
        ColumnI
        DeleteButton
    End Enum

    Private Sub frmSalesOrderNew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8 -1-14
            If e.KeyCode = Keys.F4 Then
                If BtnSave.Enabled = True Then
                    SaveToolStripButton_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(Nothing, Nothing)

            End If

            If e.KeyCode = Keys.F5 Then
                BtnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Insert Then
                btnAdd_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.U AndAlso e.Alt Then
                If Me.BtnSave.Text = "&Update" Then
                    If Me.BtnSave.Enabled = False Then
                        RemoveHandler Me.BtnSave.Click, AddressOf Me.SaveToolStripButton_Click
                    End If
                End If
            End If
            If e.KeyCode = Keys.D AndAlso e.Alt Then
                If Me.BtnSave.Text = "&Delete" Then
                    If Me.BtnDelete.Enabled = False Then
                        RemoveHandler Me.BtnDelete.Click, AddressOf Me.DeleteToolStripButton_Click
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmSalesOrderNew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try


            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            If Not getConfigValueByType("TransitInssuranceTax").ToString = "Error" Then
                TransitPercent = Val(getConfigValueByType("TransitInssuranceTax"))
            End If
            If Not getConfigValueByType("WHTax_Percentage").ToString = "Error" Then
                WHTaxPercent = Val(getConfigValueByType("WHTax_Percentage"))
            End If

            If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
                flgLoadItems = getConfigValueByType("LoadAllItemsInSales")
            End If

            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItem = getConfigValueByType("ArticleFilterByLocation")
            End If
            ''Start TFS3758 : Ayesha Rehman
            If Not getConfigValueByType("ExcludeTaxPrice").ToString = "Error" Then
                flgExcludeTaxPrice = Convert.ToBoolean(getConfigValueByType("ExcludeTaxPrice").ToString)
            End If
            ''End TFS3758
            'Task:M16 Added Flag Vehicle Identification Info
            If Not getConfigValueByType("flgVehicleIdentificationInfo").ToString = "Error" Then
                flgVehicleIdentificationInfo = getConfigValueByType("flgVehicleIdentificationInfo")
            Else
                flgVehicleIdentificationInfo = False
            End If
            'End Task:M16

            If Not getConfigValueByType("SOUpdateAfterDelivery").ToString = "Error" Then
                flgSOUpdateAfterDelivery = getConfigValueByType("SOUpdateAfterDelivery")
            End If
            If Not getConfigValueByType("flgMargeItem").ToString = "Error" Then
                flgMargeItem = getConfigValueByType("flgMargeItem")
            Else
                flgMargeItem = False
            End If
            ''Start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161
            'If Not getConfigValueByType("TransporterCommentsSales").ToString = "Error" Then
            '    TransporterCommentsSales = getConfigValueByType("TransporterCommentsSales")
            'Else
            '    TransporterCommentsSales = False
            'End If
            ''TASK TFS4544
            'If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            '    ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            'End If
            ''END TFS4544
            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)
            FillCombo("Vendor")
            FillCombo("Category")
            'FillCombo("Item") Shifted to Refresh Controls
            FillCombo("Company")
            FillCombo("SM")
            FillCombo("Project")
            FillCombo("OrderType")
            FillCombo("OrderStatus")
            FillCombo("Colour")
            FillCombo("TermOfPayments")
            'FillCombo("ArticlePack") R933 Commented
            FillCombo("Currency")
            FillCombo("Transporter")
            FillCombo("Discount Type") 'Task2827
            RefreshControls()
            'Me.cmbVendor.Focus()
            'Me.DisplayRecord() R933 Commented History Data
            IsFormOpen = True

            'If frmModProperty.blnListSeachStartWith = True Then
            '    cmbItem.AutoCompleteMode = Win.AutoCompleteMode.Suggest
            '    cmbItem.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.StartsWith
            'End If

            'If frmModProperty.blnListSeachContains = True Then
            '    cmbItem.AutoCompleteMode = Win.AutoCompleteMode.Suggest
            '    cmbItem.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.Contains
            'End If

            GetSalesOrderAnalysis()
            Get_All(frmModProperty.Tags)
            FillCombo("OutwardExpense")
            'TFS3360
            UltraDropDownSearching(cmbVendor, frmMain.blnListSeachStartWith, frmMain.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmMain.blnListSeachStartWith, frmMain.blnListSeachContains)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)


        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
            If frmModProperty.Tags.Length > 0 Then frmModProperty.Tags = String.Empty ''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
        End Try
    End Sub
    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
        Dim str As String = String.Empty
        'str = "SELECT     Recv.SalesOrderNo, CONVERT(varchar, Recv.SalesOrderDate, 103) AS Date, V.CustomerName, Recv.SalesOrderQty, Recv.SalesOrderAmount, " _
        '       & " Recv.SalesOrderId, Recv.CustomerCode, Recv.Remarks, convert(varchar, Recv.CashPaid) as CashPaid FROM         dbo.SalesOrderMasterTable Recv INNER JOIN dbo.tblCustomer V ON Recv.CustomerCode = V.AccountId"
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.SalesOrderNo, Recv.SalesOrderDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty,  " & _
        '            "Recv.SalesOrderAmount, Recv.SalesOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, PoNo as [Po No], isnull(Recv.SOP_ID,0) as SOP_ID, tblDefEmployee.Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status] " & _
        '            "FROM dbo.SalesOrderMasterTable Recv LEFT OUTER JOIN " & _
        '            "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblDefEmployee ON tblDefEmployee.Employee_ID = Recv.SOP_ID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesOrderNo  WHERE Recv.SalesOrderNo IS NOT NULL AND Recv.LocationId=" & Me.cmbCompany.SelectedValue & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.SalesOrderId in (Select SalesOrderId From SalesOrderDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '            " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.SalesOrderDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        'Task: 2626 Junaid Retrieve 'Name' field from 'tblDefCostCenter'
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.SalesOrderNo, Recv.SalesOrderDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty, " & _
        '            "Recv.SalesOrderAmount, Recv.SalesOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, PoNo as [Po No], isnull(Recv.SOP_ID,0) as SOP_ID, tblDefEmployee.Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], CstCntr.Name " & _
        '            "FROM tblDefCostCenter CstCntr Right Outer Join dbo.SalesOrderMasterTable Recv ON CstCntr.CostCenterID=Recv.CostCenterId LEFT OUTER JOIN " & _
        '            "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblDefEmployee ON tblDefEmployee.Employee_ID = Recv.SOP_ID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesOrderNo  WHERE Recv.SalesOrderNo IS NOT NULL AND Recv.LocationId=" & Me.cmbCompany.SelectedValue & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.SalesOrderId in (Select SalesOrderId From SalesOrderDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '            " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.SalesOrderDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        'End task: 2626
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.SalesOrderNo, Recv.SalesOrderDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty, " & _
        '                    "Recv.SalesOrderAmount, Recv.SalesOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, PoNo as [Po No], isnull(Recv.SOP_ID,0) as SOP_ID, tblDefEmployee.Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], CstCntr.Name, dbo.vwCOADetail.Contact_Email as Email " & _
        '                    "FROM tblDefCostCenter CstCntr Right Outer Join dbo.SalesOrderMasterTable Recv ON CstCntr.CostCenterID=Recv.CostCenterId LEFT OUTER JOIN " & _
        '                    "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblDefEmployee ON tblDefEmployee.Employee_ID = Recv.SOP_ID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesOrderNo  WHERE Recv.SalesOrderNo IS NOT NULL AND Recv.LocationId=" & Me.cmbCompany.SelectedValue & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.SalesOrderId in (Select SalesOrderId From SalesOrderDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '                    " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.SalesOrderDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.SalesOrderNo, Recv.SalesOrderDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty, " & _
        '                  "Recv.SalesOrderAmount, Recv.SalesOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, PoNo as [Po No], isnull(Recv.SOP_ID,0) as SOP_ID, tblDefEmployee.Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], CstCntr.Name, dbo.vwCOADetail.Contact_Email as Email,Recv.OrderStatus " & _
        '                  "FROM tblDefCostCenter CstCntr Right Outer Join dbo.SalesOrderMasterTable Recv ON CstCntr.CostCenterID=Recv.CostCenterId LEFT OUTER JOIN " & _
        '                  "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblDefEmployee ON tblDefEmployee.Employee_ID = Recv.SOP_ID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesOrderNo  WHERE Recv.SalesOrderNo IS NOT NULL AND Recv.LocationId=" & Me.cmbCompany.SelectedValue & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.SalesOrderId in (Select SalesOrderId From SalesOrderDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '                  " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.SalesOrderDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        'Marked Against Task#2015060007
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.SalesOrderNo, Recv.SalesOrderDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty, " & _
        '               "Recv.SalesOrderAmount, Recv.SalesOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, Recv.PoNo as [Po No], isnull(Recv.SOP_ID,0) as SOP_ID, tblDefEmployee.Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], CstCntr.Name, dbo.vwCOADetail.Contact_Email as Email,Recv.OrderStatus, IsNull(Recv.QuotationId,0) as QuotationId, Quot.QuotationNo, IsNull([No Of Attachment],0) as  [No Of Attachment] " & _
        '               "FROM tblDefCostCenter CstCntr Right Outer Join dbo.SalesOrderMasterTable Recv ON CstCntr.CostCenterID=Recv.CostCenterId LEFT OUTER JOIN " & _
        '               "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblDefEmployee ON tblDefEmployee.Employee_ID = Recv.SOP_ID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesOrderNo LEFT OUTER JOIN QuotationMasterTable Quot On Quot.QuotationId = Recv.QuotationId  LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = Recv.SalesOrderId WHERE Recv.SalesOrderNo IS NOT NULL AND Recv.LocationId=" & Me.cmbCompany.SelectedValue & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.SalesOrderId in (Select SalesOrderId From SalesOrderDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '               " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.SalesOrderDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        'Marked Against Task#2015060007


        'Altered Against Task#2015060007 get max terms and conditions
        'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.SalesOrderNo, Recv.SalesOrderDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty, " & _
        '               "Recv.SalesOrderAmount, Recv.SalesOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, Recv.PoNo as [Po No], isnull(Recv.SOP_ID,0) as SOP_ID, tblDefEmployee.Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], CstCntr.Name, dbo.vwCOADetail.Contact_Email as Email,Recv.OrderStatus, IsNull(Recv.QuotationId,0) as QuotationId, Quot.QuotationNo, IsNull([No Of Attachment],0) as  [No Of Attachment],Recv.Terms_And_Condition  " & _
        '               "FROM tblDefCostCenter CstCntr Right Outer Join dbo.SalesOrderMasterTable Recv ON CstCntr.CostCenterID=Recv.CostCenterId LEFT OUTER JOIN " & _
        '               "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblDefEmployee ON tblDefEmployee.Employee_ID = Recv.SOP_ID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesOrderNo LEFT OUTER JOIN QuotationMasterTable Quot On Quot.QuotationId = Recv.QuotationId  LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = Recv.SalesOrderId WHERE Recv.SalesOrderNo IS NOT NULL AND Recv.LocationId=" & Me.cmbCompany.SelectedValue & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.SalesOrderId in (Select SalesOrderId From SalesOrderDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '               " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.SalesOrderDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        ''Altered Against Task#201506007 get max terms and conditions
        'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
        'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.SalesOrderNo, Recv.SalesOrderDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty, " & _
        '                     "Recv.SalesOrderAmount, Recv.SalesOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, Recv.PoNo as [Po No], isnull(Recv.SOP_ID,0) as SOP_ID, tblDefEmployee.Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], CstCntr.Name, dbo.vwCOADetail.Contact_Email as Email,Recv.OrderStatus, IsNull(Recv.QuotationId,0) as QuotationId, Quot.QuotationNo, IsNull([No Of Attachment],0) as  [No Of Attachment],Recv.Terms_And_Condition,recv.UserName,Recv.UpdateUserName as [Modified By]  " & _
        '                     "FROM tblDefCostCenter CstCntr Right Outer Join dbo.SalesOrderMasterTable Recv ON CstCntr.CostCenterID=Recv.CostCenterId LEFT OUTER JOIN " & _
        '                     "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblDefEmployee ON tblDefEmployee.Employee_ID = Recv.SOP_ID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesOrderNo LEFT OUTER JOIN QuotationMasterTable Quot On Quot.QuotationId = Recv.QuotationId  LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = Recv.SalesOrderId WHERE Recv.SalesOrderNo IS NOT NULL AND Recv.LocationId=" & Me.cmbCompany.SelectedValue & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.SalesOrderId in (Select SalesOrderId From SalesOrderDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '                     " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.SalesOrderDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""

        'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
        'rafay modified query
        str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.SalesOrderNo, Recv.SalesOrderDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Pack.Packs, Recv.SalesOrderQty,dbo.tblcurrency.currency_code As currency_code, " & _
                            "Recv.SalesOrderAmount,Recv.AmountUS, Recv.SalesOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, Recv.PONo as [Po No], isnull(Recv.SOP_ID,0) as SOP_ID,Currency.CurrencyRate, tblDefEmployee.Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment," & IIf(flgExcludeTaxPrice = True, "(Recv.SalesOrderAmount + NetInfo.SedTaxAmount  ) As NetAmount ", "(Recv.SalesOrderAmount + NetInfo.SedTaxAmount + NetInfo.TaxAmount ) As NetAmount ") & ",Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], CstCntr.Name, dbo.vwCOADetail.Contact_Email as Email,IsNull(Recv.SaleOrderStatusId,0) as SaleOrderStatusId, SalesOrderStatusTable.OrderStatus as [Order Status], IsNull(Recv.QuotationId,0) as QuotationId, Quot.QuotationNo, IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment],  Recv.Terms_And_Condition,recv.UserName,Recv.UpdateUserName as [Modified By], IsNull(Recv.SaleOrderTypeId,0) as SaleOrderTypeId, SalesOrderTypeTable.SalesOrderTypeName as [Order Type], Recv.SpecialInstructions, IsNull(Recv.DispatchToLocation, 0) As DispatchToLocation, IsNull(Recv.InvoiceToLocation, 0) As InvoiceToLocation, " & _
                            "IsNull(Recv.TechnicalDrawingNumber, 0) As TechnicalDrawingNumber, Recv.TechnicalDrawingDate, Recv.AccountsRemarks, Recv.StoreRemarks, Recv.ProductionRemarks, Recv.ServicesRemarks, Recv.SalesRemarks, Recv.TermOfPayments, IsNull(TransporterId, 0) AS TransporterId " & _
                            " FROM tblDefCostCenter CstCntr Right Outer Join dbo.SalesOrderMasterTable Recv ON CstCntr.CostCenterID=Recv.CostCenterId LEFT OUTER JOIN (Select SalesOrderId, Sum(IsNull(Sz1, 0)) As Packs From SalesOrderDetailTable Group By SalesOrderId) As Pack ON Recv.SalesOrderId = Pack.SalesOrderId Left Outer Join  " & _
                            "(Select Distinct SalesOrderDetailTable.SalesOrderId,CurrencyId,CurrencyRate  From SalesOrderDetailTable) As Currency ON Recv.SalesOrderId = Currency.SalesOrderId LEFT OUTER JOIN  " & _
                            "dbo.tblcurrency ON Currency.CurrencyId = dbo.tblcurrency.currency_id " & _
                            " left outer join (SELECT     SalesOrderDetailtable.SalesOrderId, SUM((ISNULL(SalesTax_Percentage, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0))) AS TaxAmount,SUM((ISNULL(SED_Tax_Percent, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0))) AS SedTaxAmount  FROM         dbo.SalesOrderDetailtable INNER JOIN SalesOrderMasterTable on SalesOrderMasterTable.SalesOrderId = SalesOrderDetailTable.SalesOrderId group by SalesOrderDetailTable.SalesOrderId  ) As NetInfo on Recv.SalesOrderId = NetInfo.SalesOrderId " & _
                            " LEFT OUTER JOIN dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblDefEmployee ON tblDefEmployee.Employee_ID = Recv.SOP_ID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesOrderNo LEFT OUTER JOIN QuotationMasterTable Quot On Quot.QuotationId = Recv.QuotationId  LEFT OUTER JOIN  SalesOrderStatusTable on SalesOrderStatusTable.OrderStatusID = Recv.SaleOrderStatusID LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId,source) Doc_Att on Doc_Att.DocId = Recv.SalesOrderId LEFT OUTER JOIN SalesOrderTypeTable on SalesOrderTypeTable.SaleOrderTypeId = Recv.SaleOrderTypeId WHERE Recv.SalesOrderNo IS NOT NULL AND Recv.LocationId=" & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbCompany.SelectedValue) & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.SalesOrderId in (Select SalesOrderId From SalesOrderDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
                            " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.SalesOrderDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""

        If Me.dtpFrom.Checked = True Then
            str += " AND Recv.SalesOrderDate >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
        End If
        If Me.dtpTo.Checked = True Then
            str += " AND Recv.SalesOrderDate <= Convert(Datetime, N'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
        End If
        If Me.txtSearchDocNo.Text <> String.Empty Then
            str += " AND Recv.SalesOrderNo LIKE '%" & Me.txtSearchDocNo.Text & "%'"
        End If
        If blnDisplayAll = False Then
            If Not Me.cmbSearchLocation.SelectedIndex = -1 Then
                str += " AND Recv.LocationId=" & Me.cmbSearchLocation.SelectedValue
            End If
        End If
        If Me.txtFromAmount.Text <> String.Empty Then
            If Val(Me.txtFromAmount.Text) > 0 Then
                str += " AND Recv.SalesOrderAmount >= " & Val(Me.txtFromAmount.Text) & " "
            End If
        End If
        If Me.txtToAmount.Text <> String.Empty Then
            If Val(Me.txtToAmount.Text) > 0 Then
                str += " AND Recv.SalesOrderAmount <= " & Val(Me.txtToAmount.Text) & ""
            End If
        End If
        If Me.cmbSearchAccount.IsItemInList Then
            If Me.cmbSearchAccount.SelectedRow.Cells(0).Value <> 0 Then
                str += " AND Recv.VendorId = " & Me.cmbSearchAccount.Value
            End If
        End If
        If Me.txtSearchRemarks.Text <> String.Empty Then
            str += " AND Recv.Remarks LIKE '%" & Me.txtSearchRemarks.Text & "%'"
        End If
        If Me.txtPurchaseNo.Text <> String.Empty Then
            str += " AND Recv.SalesOrderNo LIKE  '%" & Me.txtPurchaseNo.Text & "%'"
        End If
        If Me.chkArticleAlias.Checked = True Then
            str += " AND Recv.SalesOrderId In(Select DISTINCT SalesOrderId From SalesOrderDetailTable WHERE ArticleDefId is null And ArticleAliasName !='')"
        End If
        'rafay apply filter to show record on top in grd history when save button is click
        str += " ORDER BY Recv.SalesOrderId DESC"
        ' End If
        FillGridEx(grdSaved, str, True)
        ' Change on 23-11-2013  For Multiple Print Vouchers

        Me.grdSaved.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
        Me.grdSaved.RootTable.Columns("No Of Attachment").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdSaved.RootTable.Columns.Add("Column1")
        Me.grdSaved.RootTable.Columns("Column1").UseHeaderSelector = True
        Me.grdSaved.RootTable.Columns("Column1").ActAsSelector = True
        '-----------------------------------------------------'

        'grdSaved.DataSource = GetDataTable(str)
        'grdSaved.Columns(10).Visible = False
        grdSaved.RootTable.Columns(6).Visible = True
        grdSaved.RootTable.Columns(7).Visible = True
        grdSaved.RootTable.Columns(8).Visible = False
        grdSaved.RootTable.Columns(9).Visible = False
        grdSaved.RootTable.Columns(10).Visible = False
        grdSaved.RootTable.Columns("SaleOrderStatusID").Visible = False
        grdSaved.RootTable.Columns("SaleOrderTypeID").Visible = False
        grdSaved.RootTable.Columns("QuotationId").Visible = False
        grdSaved.RootTable.Columns("SOP_ID").Visible = False
        grdSaved.RootTable.Columns("CostCenterId").Visible = False
        grdSaved.RootTable.Columns("Adj_Flag").Visible = False
        grdSaved.RootTable.Columns("Adjustment").Visible = False
        grdSaved.RootTable.Columns("PO_Date").Visible = False
        grdSaved.RootTable.Columns("Email").Visible = False

        'Altered Against Task#2015060007 hiding terms from grid
        grdSaved.RootTable.Columns("Terms_And_Condition").Visible = False
        'Altered Against Task#2015060007 hiding terms from grid

        grdSaved.RootTable.Columns("EditionalTax_Percentage").Visible = False
        grdSaved.RootTable.Columns("SED_Percentage").Visible = False

        grdSaved.RootTable.Columns("TransporterId").Visible = False

        'Rafay:the foreign currency is add on purchase history
        Dim grdSaved1 As DataTable = GetDataTable(str)
        grdSaved1.Columns("AmountUS").Expression = "IsNull(SalesOrderAmount,0) / (IsNull(CurrencyRate,0))" 'Task:2374 Show Total Amount
        Me.grdSaved.DataSource = grdSaved1
        'Set rounded format
        Me.grdSaved.RootTable.Columns("AmountUS").FormatString = "N" & DecimalPointInValue
        'rafay
        'Task:2759 Set rounded format 
        Me.grdSaved.RootTable.Columns("SalesOrderAmount").FormatString = "N" & DecimalPointInValue
        Me.grdSaved.RootTable.Columns("Adjustment").FormatString = "N" & DecimalPointInValue
        'End Task:2759

        'grdSaved.Columns("EmployeeCode").Visible = False
        'grdSaved.Columns("PoId").Visible = False
        grdSaved.RootTable.Columns(0).Caption = "Doc No"
        grdSaved.RootTable.Columns(1).Caption = "Date"
        grdSaved.RootTable.Columns(2).Caption = "Customer Name"
        'grdSaved.Columns(3).HeaderText = "S-Order"
        grdSaved.RootTable.Columns(4).Caption = "Qty"
        grdSaved.RootTable.Columns(5).Caption = "Currency"
        grdSaved.RootTable.Columns(6).Caption = "Base Value"
        grdSaved.RootTable.Columns(7).Caption = "Original Value"
        'grdSaved.Columns(8).HeaderText = "Employee"

        'Task: 2626 Junaid 'Name' column is modified
        grdSaved.RootTable.Columns("Name").Visible = True
        grdSaved.RootTable.Columns("Name").Caption = "Project Name"
        'END Task: 2626
        grdSaved.RootTable.Columns("SpecialInstructions").Caption = "Special Instructions" ''TASK-SP22-07-2016

        grdSaved.RootTable.Columns(0).Width = 100
        grdSaved.RootTable.Columns(1).Width = 100
        grdSaved.RootTable.Columns(2).Width = 250
        grdSaved.RootTable.Columns(3).Width = 50
        grdSaved.RootTable.Columns(5).Width = 80
        grdSaved.RootTable.Columns(8).Width = 100
        grdSaved.RootTable.Columns(4).Width = 150
        'grdSaved.RowHeadersVisible = False
        Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat
        Me.grdSaved.RootTable.Columns("NetAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS4677
        Me.grdSaved.RootTable.Columns("NetAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS4677
        Me.grdSaved.RootTable.Columns("NetAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum ''TFS4677
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Validate_AddToGrid() Then
            '' FindExistsItem function is added agaisnt TASK TFS1648 on 03-11-2017
            If Not FindExistsItem(Me.cmbItem.Value, Val(Me.txtRate.Text), Me.cmbUnit.Text, Val(Me.txtPackQty.Text), Val(Me.cmbPo.SelectedValue), Val(Me.cmbCategory.SelectedValue)) = True Then
                AddItemToGrid()
            End If
            GetTotal()
            ClearDetailControls()
            cmbItem.Focus()
            'FillCombo("Item")
        End If

    End Sub
    Private Sub RefreshControls()
        IsEditMode = False
        ''TASK TFS4544
        If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
        End If
        ''END TFS4544
        Me.cmbVendor.Enabled = True
        'Rafay:
        ''companyinitials = ""
        'Rafay
        Me.txtRemarks.Enabled = True
        txtPONo.Text = ""
        dtpPODate.Value = Now
        Me.dtpPDate.Value = Now
        dtpPODate.Enabled = True
        Me.txtRate.Text = ""
        Me.txtPackRate.Text = ""
        txtRemarks.Text = ""
        txtPaid.Text = ""
        'txtAmount.Text = ""
        txtTotal.Text = ""
        CurrencyRate = 1
        Me.txtNetTotal.Text = ""    ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        FillCombo("Item")
        cmbVendor.Rows(0).Activate()
        cmbItem.Rows(0).Activate()
        'cmbDispatchTo.Rows(0).Activate()
        'cmbInvoiceTo.Rows(0).Activate()
        'cmbItemWiseDelivery.Rows(0).Activate()
        cmbUnit.SelectedIndex = 0
        txtDiscountValue.Text = ""   '28-03-2018 : Task# TFS2827: Ayesha Rehman: Set these controls text empty on load,new
        Me.cmbDiscountType.SelectedIndex = 1 'TFS2827
        Me.txtPDP.Text = "" 'TFS2827
        'txtTotalQty.Text = ""
        txtBalance.Text = ""
        txtPackQty.Text = 1
        Me.BtnSave.Text = "&Save"
        Me.btnSave1.Text = "&Save"
        ' Mode = "Normal"
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("SO", 6, "SalesOrderMasterTable", "SalesOrderNo")
        Me.cmbPo.Enabled = True
        'FillCombo("SO")
        FillCombo("Quotation")
        FillCombo("Colour")
        FillCombo("TermOfPayments")
        If flgLoadItems = True Then
            Me.DisplayDetail(-1, -1, "All")
        Else
            Me.DisplayDetail(-1)
        End If
        FillOutwardExpense(-1, "SO")
        'DisplayDetail(-1)
        'Me.cmbVendor.Focus()
        GetSecurityRights()
        Me.LnkLoadAll.Enabled = True
        If Not Me.cmbSalesMan.SelectedIndex = -1 Then Me.cmbSalesMan.SelectedIndex = 0
        'FillComboByEdit()
        Me.cmbCompany.Enabled = True
        Me.dtpDeliveryDate.Value = Date.Now
        dtpDeliveryDate.Checked = False
        Me.dtpPDate.Checked = False
        Me.dtpFrom.Value = Date.Now.AddMonths(-1)
        Me.dtpTo.Value = Date.Now
        Me.dtpFrom.Checked = False
        Me.dtpTo.Checked = False
        Me.txtSearchDocNo.Text = String.Empty
        Me.txtFromAmount.Text = String.Empty
        Me.txtToAmount.Text = String.Empty
        Me.txtPurchaseNo.Text = String.Empty
        'Me.cmbSearchAccount.Rows(0).Activate()
        'Me.cmbSearchLocation.SelectedIndex = 0
        Me.txtSearchRemarks.Text = String.Empty
        Me.SplitContainer1.Panel1Collapsed = True
        Me.txtSpecialAdjustment.Text = String.Empty
        'Me.chkPost.Checked = False 'Sale order by default should be post.
        Me.txtSchemeQty.Text = String.Empty
        'DisplayRecord() R933 Commented History Data
        Me.lblPrintStatus.Text = String.Empty
        Me.txtNetBill.Text = String.Empty
        Me.txtCustPONo.Text = String.Empty
        Adjustment = 0D
        Me.cmbOrderStatus.SelectedIndex = 0
        Me.cmbColour.Text = ""
        'Me.cmbColour.SelectedIndex = -1
        If Not Me.cmbQuotation.SelectedIndex = -1 Then Me.cmbQuotation.SelectedIndex = 0
        If Not Me.cmbTransporter.SelectedIndex = -1 Then Me.cmbTransporter.SelectedIndex = 0
        'If Not Me.cmbOrderType.SelectedIndex = -1 Then Me.cmbOrderType.SelectedIndex = 0
        'If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedIndex = 0 'Comment against task:2427
        Me.txtTerms_And_Condition.Text = GetTermsCondition()
        ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
        Me.BtnDelete.Visible = False
        Me.BtnPrint.Visible = False
        Me.BtnEdit.Visible = False
        ''''''''''''''''''''''''''
        Me.chkArticleAlias.Checked = False
        Me.dtpPODate.Focus()
        'Task:2427 CostCenter Selected Company Wise
        If Not Me.cmbCompany.SelectedIndex = -1 Then
            Me.cmbProject.SelectedValue = Val(CType(Me.cmbCompany.SelectedItem, DataRowView).Row.Item("CostCenterId").ToString)
        End If
        'End Task:2427
        'Marked Against Task#2015060001
        'Array.Clear(arrFile, 0, arrFile.Length)
        'Marked Against Task#2015060001 Ali Ansari
        'Altered Against Task#2015060001 Ali Ansari
        'Clear arrfile
        'Marked Against Task#2015060001
        'Array.Clear(arrFile, 0, arrFile.Length)
        'Marked Against Task#2015060001 Ali Ansari
        'Altered Against Task#2015060001 Ali Ansari
        'Clear arrfile
        arrFile = New List(Of String)
        'Altered Against Task#2015060001 Ali Ansari
        'DisplayDetail(-1)
        'Me.cmbVendor.Focus()
        arrFile = New List(Of String)
        'Altered Against Task#2015060001 Ali Ansar
        Me.btnAttachment.Text = "Attachment"
        ''TASK-480
        Me.cmbRevisionNumber.Visible = False
        Me.lblRev.Visible = False
        Me.lnkLblRevisions.Visible = False
        Me.txtPONo.Size = New System.Drawing.Size(177, 21)
        Me.txtTerms_And_Condition.Text = String.Empty
        Me.txtSpecialInstructions.Text = String.Empty
        Me.txtTechnicalDrawingNo.Text = String.Empty
        Me.dtpTechnicalDrawingDate.Value = Date.Now
        Me.dtpTechnicalDrawingDate.Checked = False
        Me.txtAccountsRemarks.Text = String.Empty
        Me.txtStoreRemarks.Text = String.Empty
        Me.txtProductionRemarks.Text = String.Empty
        Me.txtSalesRemarks.Text = String.Empty
        Me.txtServicesRemarks.Text = String.Empty
        Me.cmbTermOfPayments.Text = String.Empty
        Me.UltraTabPageControl4.Enabled = False
        Me.cmbCurrency.SelectedValue = BaseCurrencyId
        Me.txtCurrencyRate.Enabled = False
        Me.cmbCurrency.Enabled = True
        Me.CurrencyRate = 1
        'Abubakar Siddiq : TFS3113 : Enable Approval History button only in Eidt Mode
        If IsEditMode = True Then
            Me.btnApprovalHistory.Visible = True
            Me.btnApprovalHistory.Enabled = True
        Else
            Me.btnApprovalHistory.Visible = False
        End If
        'Abubakar Siddiq : TFS3113 : End
        ''Abubakar Siddiq :TFS3113 :Making Approval Button Enable in Edit Mode
        If Not getConfigValueByType("SalesOrderApproval") = "Error" Then
            ApprovalProcessId = Val(getConfigValueByType("SalesOrderApproval"))
        End If
        If ApprovalProcessId = 0 Then
            Me.chkPost.Visible = True
            Me.chkPost.Enabled = True
        Else
            Me.chkPost.Visible = False
            Me.chkPost.Enabled = False
            Me.chkPost.Checked = False
        End If
        ''Abubakar Siddiq :TFS3113 :End
        ''End TASK-480
    End Sub
    Function GetTermsCondition() As String
        Try
            'Marked Against Task#2015060007
            '            Dim str As String = "Select Terms_And_Condition From SalesOrderMasterTable WHERE SalesOrderId in (Select Max(SalesOrderId) From SalesOrderMasterTable)"
            'Marked Against Task#2015060007
            'Altered Against Task#2015060007 get max terms and conditions
            Dim str As String = "Select Terms_And_Condition From SalesOrderMasterTable WHERE SalesOrderId = (Select Max(SalesOrderId) From SalesOrderMasterTable)"
            'Altered Against Task#2015060007 get max terms and conditions
            Dim dt As New DataTable
            dt = GetDataTable(str)

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub ClearDetailControls()
        'cmbCategory.SelectedIndex = 0
        cmbUnit.SelectedIndex = 0

        Me.cmbColour.Text = ""
        txtQty.Text = ""
        txtRate.Text = ""
        txtTotal.Text = ""
        txtPackQty.Text = 1
        txtSchemeQty.Text = String.Empty
        Me.txtDisc.Text = "" ''29-03-2018   TFS2827   Ayesha Rehamn  
        Me.txtDiscountValue.Text = ""        ''29-03-2018   TFS2827   Ayesha Rehamn
        Me.txtPDP.Text = ""        ''22-05-2018   TFS3330  Ayesha Rehman
        Me.txtTax.Text = ""
        Me.txtPackRate.Text = String.Empty
        Me.txtNetTotal.Text = ""    ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        Me.txtTotalQuantity.Text = ""
        Me.txtPDP.Enabled = True
    End Sub

    Private Function Validate_AddToGrid() As Boolean
        'If Me.cmbVendor.IsItemInList = True Then
        '    If Me.cmbVendor.Rows(0).Activate Then
        '        msg_Error("You must select any customer")
        '        Me.cmbVendor.Focus() : Validate_AddToGrid = False : Exit Function
        '    End If
        'End If
        'Change by murtaza default currency rate(10/26/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/26/2022)
        If Me.cmbItem.IsItemInList = False Then
            ShowErrorMessage("Item not found")
            Return False
        End If
        'Rafay :task Start
        If txtTax.Text = "" Then
            msg_Error("Please select tax value")
            txtTax.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'Rafay:task end

        If cmbItem.ActiveRow.Cells(0).Value <= 0 AndAlso Me.txtArticleAlias.Text = String.Empty Then
            msg_Error("You must select any item")
            cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtQty.Text) < 0 Then
            msg_Error("Quantity must be greater than 0")
            txtQty.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtRate.Text) < 0 Then
            msg_Error("Rate must be greater than 0")
            txtRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'commented because stock validation is not required on Sales order
        'Dim IsMinus As Boolean = True
        ''If CType(Me.cmbItem.SelectedRow, Infragistics.Win.UltraWinGrid.UltraGridRow).Cells("ServiceItem").Value = False Then
        'IsMinus = getConfigValueByType("AllowMinusStock")
        ''End If
        'If Mode = "Normal" Then
        '    If Not IsMinus = True Then
        '        If Val(Me.txtStock.Text) <> Val(txtTotalQuantity.Text) Then

        '            If Val(Me.txtStock.Text) - Val(txtTotalQuantity.Text) <= 0 Then
        '                msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
        '                cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        '            End If
        '        End If
        '    End If
        '    If CType(Me.cmbCategory.SelectedItem, DataRowView).Row.Item("AllowMinusStock").ToString = "False" AndAlso IsMinus = True Then
        '        If Val(Me.txtStock.Text) <> Val(txtTotalQuantity.Text) Then
        '            If Val(Me.txtStock.Text) - Val(txtTotalQuantity.Text) <= 0 Then
        '                msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
        '                cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        '            End If
        '        End If
        '    End If
        'End If

        Validate_AddToGrid = True
    End Function

    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)


        'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtQty.Text) <> 0 AndAlso Val(Me.txtRate.Text) = 0 Then
        '    Me.txtRate.Text = Val(Me.txtTotal.Text) / Val(Me.txtQty.Text)
        'Else
        '    If Val(Me.txtPackQty.Text) = 0 Then
        '        txtPackQty.Text = 1
        '        txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        '    Else
        '        txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        '    End If
        'End If

        'If Val(Me.txtPackQty.Text) = 0 Then
        '    txtPackQty.Text = 1
        '    txtNetTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), DecimalPointInValue)
        'Else
        '    txtNetTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), DecimalPointInValue)
        'End If


        Try

            If IsSalesOrderAnalysis = True Then
                If Val(Me.txtDisc.Text) <> 0 Then
                    Me.txtDisc.TabStop = True
                End If
            End If

            GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtQty.Text) = 0 Then
            '    Me.txtQty.Text = Math.Round((Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)), 2)
            'Else
            '    If Val(Me.txtPackQty.Text) = 0 Then
            '        txtPackQty.Text = 1
            '        txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
            '    Else
            '        txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text), DecimalPointInValue)
            '    End If
            'End If

            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    txtNetTotal.Text = Math.Round((Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), DecimalPointInValue)
            'Else
            '    txtNetTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), DecimalPointInValue)
            'End If

            Me.GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate_LostFocus1(sender As Object, e As EventArgs) Handles txtRate.LostFocus
        Try
            'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtQty.Text) = 0 Then
            '    Me.txtQty.Text = Math.Round((Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)), 2)
            'Else
            '    If Val(Me.txtPackQty.Text) = 0 Then
            '        txtPackQty.Text = 1
            '        txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
            '    Else
            '        txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text), DecimalPointInValue)
            '    End If
            'End If

            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    txtNetTotal.Text = Math.Round((Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), DecimalPointInValue)
            'Else
            '    txtNetTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), DecimalPointInValue)
            'End If

            Me.GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate.TextChanged
        Try
            'Me.GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged, cmbVendor.Enter
        Try
            If Me.cmbUnit.SelectedIndex < 0 Then Me.txtPackQty.Text = String.Empty : Exit Sub
            If Me.cmbUnit.Text = "Loose" Then
                txtPackQty.Text = 1
                txtPackRate.Text = 0
                Me.txtPackQty.Enabled = False
                Me.txtPackQty.TabStop = False
                Me.txtTotalQuantity.Enabled = False
                Me.txtPackRate.Enabled = False
                Me.txtPDP.Enabled = True ''TFS3330
                ClearDetailControlsUponUnit() ''TFS3330 : Reset controls when unit change to loose 
                Me.txtPDP.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString ''TFS3330
                Dim StrSQl As String = "" ''TFS3330 : Ayesha Rehman
                If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then
                    If Me.cmbItem.ActiveRow.Cells(0).Value > 0 Then
                        If getConfigValueByType("ApplyFlatDiscountOnSale").ToString = "False" Then
                            StrSQl = "select discount from tbldefcustomerbasediscounts where articledefid = " & Me.cmbItem.ActiveRow.Cells(0).Value _
                            & " and typeid = " & Me.cmbVendor.ActiveRow.Cells("TypeId").Value & "  and discount > 0 "
                            Dim dtdiscount As DataTable = GetDataTable(StrSQl)
                            If Not dtdiscount Is Nothing Then
                                If dtdiscount.Rows.Count <> 0 Then
                                    Dim disc As Double = 0D
                                    Double.TryParse(dtdiscount.Rows(0)(0).ToString, disc)

                                    Dim price As Double = 0D
                                    Double.TryParse(Me.txtRate.Text, price)
                                    Me.txtRate.Text = price - ((price / 100) * disc)
                                    Me.txtPDP.Text = price - ((price / 100) * disc) ''TFS2827
                                    Me.txtDisc.TabStop = False
                                Else
                                    If IsSalesOrderAnalysis = True Then
                                        Me.txtDisc.TabStop = True
                                    End If
                                End If
                            End If
                        Else

                            StrSQl = "select discount from tbldefcustomerbasediscountsFlat where articledefid = " & Me.cmbItem.ActiveRow.Cells(0).Value _
                                                                        & " and typeid = " & Me.cmbVendor.ActiveRow.Cells("typeid").Value & "  and discount > 0 "
                            Dim dtdiscountFlat As DataTable = GetDataTable(StrSQl)
                            dtdiscountFlat.AcceptChanges()

                            If Not dtdiscountFlat Is Nothing Then
                                If dtdiscountFlat.Rows.Count <> 0 Then

                                    Dim disc As Double = 0D
                                    Double.TryParse(dtdiscountFlat.Rows(0)(0).ToString, disc)

                                    Dim price As Double = 0D
                                    Double.TryParse(Me.txtRate.Text, price)

                                    Dim dblDiscountPercent As Double = 0D
                                    If disc > 0 Then
                                        dblDiscountPercent = (disc / price) * 100
                                        If price > 0 Then
                                            Me.txtRate.Text = Math.Round(price - ((price / 100) * dblDiscountPercent), TotalAmountRounding)
                                            Me.txtPDP.Text = Math.Round(price - ((price / 100) * dblDiscountPercent), TotalAmountRounding)
                                            Me.txtDisc.TabStop = False
                                        Else
                                            Me.txtRate.Text = Math.Round(Val(Me.txtRate.Text), TotalAmountRounding)
                                            Me.txtPDP.Text = Math.Round(Val(Me.txtPDP.Text), TotalAmountRounding)
                                            Me.txtDisc.TabStop = True
                                        End If
                                    Else
                                        dblDiscountPercent = 0
                                        Me.txtDisc.TabStop = True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                Me.txtQty.Text = 1
            Else
                ''Start TFS4161
                If IsPackQtyDisabled = True Then
                    Me.txtPackQty.Enabled = False
                    Me.txtPackQty.TabStop = False
                    Me.txtTotalQuantity.Enabled = False
                    Me.txtPackRate.Enabled = False
                Else
                    Me.txtPackQty.Enabled = True
                    Me.txtPackQty.TabStop = True
                    Me.txtTotalQuantity.Enabled = True
                    Me.txtPackRate.Enabled = True
                End If
                ''End TFS4161
                If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
                    Me.txtPackQty.Text = Math.Round(Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString), TotalAmountRounding)
                    Me.txtPackRate.Text = Math.Round(Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackRate").ToString), TotalAmountRounding) ''TFS1964
                    If txtPackRate.Text = 1 Then
                        txtPackRate.Text = 0
                        'txtRate.Text = 0
                    End If
                End If
                GetDetailTotal()
            End If
            Me.txtStock.Text = Math.Round(Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack"))), TotalAmountRounding)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub ClearDetailControlsUponUnit()
        'cmbCategory.SelectedIndex = 0
        'cmbUnit.SelectedIndex = 0
        Me.cmbColour.Text = ""
        txtQty.Text = ""
        txtRate.Text = ""
        txtTotal.Text = ""
        txtPackQty.Text = 1
        Me.txtTax.Text = ""
        Me.txtPackRate.Text = String.Empty
        Me.txtPDP.Text = ""
        Me.txtDisc.Text = "" ''12-04-2018   TFS2827   Ayesha Rehamn  
        Me.txtDiscountValue.Text = ""        ''12-04-2018   TFS2827   Ayesha Rehamn
        'Me.cmbUM.Text = ""
        Me.txtPDP.Enabled = True ''23-05-2018   TFS3330   Ayesha Rehamn  
    End Sub

    Private Sub AddItemToGrid()
        Try
            'grd.Rows.Add(cmbCategory.Text, cmbItem.Text, cmbUnit.Text, Val(txtQty.Text), Val(txtRate.Text), Val(txtTotal.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Val(Me.txtPackQty.Text), Me.cmbItem.ActiveRow.Cells("Price").Value)

            'Dim dt As DataTable = CType(grd.DataSource, DataTable)
            'Dim dr As DataRow

            'Dim objGridItem As DataGridView
            'Dim intLoopCounter As Integer = 0


            'Dim strCategory As New DataColumn("Category", GetType(String))
            'Dim strItem As New DataColumn("Item", GetType(String))
            'Dim strUnit As New DataColumn("Unit", GetType(String))
            'Dim intQty As New DataColumn("Qty", GetType(Integer))
            'Dim dblRate As New DataColumn("Rate", GetType(Double))
            'Dim dblTotal As New DataColumn("Total", GetType(Double))
            'Dim intCategoryID As New DataColumn("CategoryID", GetType(Integer))
            'Dim intItemID As New DataColumn("ItemID", GetType(Integer))

            'dt.Columns.Add(strCategory)
            'dt.Columns.Add(strItem)
            'dt.Columns.Add(strUnit)
            'dt.Columns.Add(intQty)
            'dt.Columns.Add(dblRate)
            'dt.Columns.Add(dblTotal)
            'dt.Columns.Add(intCategoryID)
            'dt.Columns.Add(intItemID)

            'dr = dt.NewRow
            'dr("Category") = cmbCategory.SelectedText
            'dr("Item") = cmbItem.SelectedText
            'dr("Unit") = cmbUnit.SelectedText
            'dr("Qty") = txtQty.Text
            'dr("Rate") = txtRate.Text
            'dr("Total") = Val(txtTotal.Text)
            ''        dr("CategoryID") = cmbCategory.SelectedValue
            ''        dr("ItemID") = cmbItem.SelectedValue
            'dt.Rows.Add(dr)
            'grd.DataSource = dt
            Dim DefaultTax As Double = 0D
            If IsSalesOrderAnalysis = True Then
                DefaultTax = Val(getConfigValueByType("Default_Tax_Percentage").ToString)
            End If
            Dim dtGrd As DataTable
            dtGrd = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtGrd.NewRow
            drGrd.Item(GrdEnum.SerialNo) = ""
            drGrd.Item(GrdEnum.LocationId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
            If Me.cmbItem.Value > 0 Then
                drGrd.Item(GrdEnum.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
                drGrd.Item(GrdEnum.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
                drGrd.Item(GrdEnum.ItemId) = Me.cmbItem.ActiveRow.Cells(0).Value
                drGrd.Item(GrdEnum.CurrentPrice) = Me.cmbItem.ActiveRow.Cells("Price").Text
                If IsSalesOrderAnalysis = True Then
                    If GST_Applicable = True Then
                        drGrd.Item(GrdEnum.SalesTax_Percentage) = DefaultTax
                    ElseIf FlatRate_Applicable = True Then
                        drGrd.Item(GrdEnum.SalesTax_Percentage) = ((FlatRate / Val(Me.cmbItem.SelectedRow.Cells("Price").Text)) * 100)
                    Else
                        drGrd.Item(GrdEnum.SalesTax_Percentage) = 0
                    End If
                Else
                    drGrd.Item(GrdEnum.SalesTax_Percentage) = Math.Round(Val(Me.txtTax.Text), TotalAmountRounding)
                End If
                drGrd.Item(GrdEnum.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
                drGrd.Item(GrdEnum.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
            Else
                drGrd.Item(GrdEnum.ArticleCode) = String.Empty
                drGrd.Item(GrdEnum.Item) = String.Empty
                drGrd.Item(GrdEnum.ItemId) = 0
                drGrd.Item(GrdEnum.CurrentPrice) = Val(txtRate.Text)
                drGrd.Item(GrdEnum.SalesTax_Percentage) = Val(Me.txtTax.Text)
                drGrd.Item(GrdEnum.PurchasePrice) = 0 'Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
                drGrd.Item(GrdEnum.CostPrice) = 0 'IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
            End If
            drGrd.Item(GrdEnum.ArticleAlias) = Me.txtArticleAlias.Text
            ''Start TFS4343
            drGrd.Item(GrdEnum.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
            drGrd.Item(GrdEnum.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
            ''End TFS4343
            drGrd.Item(GrdEnum.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            drGrd.Item(GrdEnum.Qty) = Val(Me.txtQty.Text)
            drGrd.Item(GrdEnum.Rate) = Val(Me.txtRate.Text)
            drGrd.Item(GrdEnum.Total) = Math.Round(Val(Me.txtTotal.Text), TotalAmountRounding)
            drGrd.Item(GrdEnum.CategoryId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
            drGrd.Item(GrdEnum.PackQty) = Val(Me.txtPackQty.Text)

            drGrd.Item(GrdEnum.PackPrice) = Val(Me.txtPackRate.Text)
            drGrd.Item(GrdEnum.TradePrice) = TradePrice
            'If IsSalesOrderAnalysis = True Then
            '    If GST_Applicable = True Then
            '        drGrd.Item(GrdEnum.SalesTax_Percentage) = DefaultTax
            '    ElseIf FlatRate_Applicable = True Then
            '        drGrd.Item(GrdEnum.SalesTax_Percentage) = ((FlatRate / Val(Me.cmbItem.SelectedRow.Cells("Price").Text)) * 100)
            '    Else
            '        drGrd.Item(GrdEnum.SalesTax_Percentage) = 0
            '    End If
            'Else
            '    drGrd.Item(GrdEnum.SalesTax_Percentage) = Val(Me.txtTax.Text)
            'End If
            drGrd.Item(GrdEnum.SchemeQty) = Val(Me.txtSchemeQty.Text)
            drGrd.Item(GrdEnum.Freight) = Freight_Rate
            drGrd.Item(GrdEnum.Discount_Percentage) = Val(Me.txtDisc.Text)
            drGrd.Item(GrdEnum.MarketReturns) = MarketReturns_Rate
            'drGrd.Item(GrdEnum.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
            drGrd.Item(GrdEnum.Pack_Desc) = Me.cmbUnit.Text.ToString
            drGrd.Item(GrdEnum.Comments) = String.Empty
            drGrd.Item(GrdEnum.OtherComments) = IIf(Me.cmbColour.Text = "", "", Me.cmbColour.Text.ToString)
            drGrd.Item(GrdEnum.TotalQuantity) = Val(Me.txtTotalQuantity.Text)



            drGrd.Item(GrdEnum.CurrencyId) = Me.cmbCurrency.SelectedValue
            If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                drGrd.Item(GrdEnum.CurrencyAmount) = Val(0)
            Else
                drGrd.Item(GrdEnum.CurrencyAmount) = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text), TotalAmountRounding)
            End If
            drGrd.Item(GrdEnum.CurrencyRate) = Val(Me.txtCurrencyRate.Text)
            Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
            If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                drGrd.Item(GrdEnum.BaseCurrencyId) = Val(ConfigCurrencyVal)
                drGrd.Item(GrdEnum.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
            End If
            'drGrd.Item(GrdEnum.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
            ''Start TFS2827
            If Me.cmbDiscountType.Text.Equals(DiscountType_Percentage) AndAlso Val(txtDisc.Text) > 0 Then
                drGrd.Item(GrdEnum.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                drGrd.Item(GrdEnum.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                drGrd.Item(GrdEnum.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
            ElseIf Me.cmbDiscountType.Text.Equals(DiscountType_Flat) AndAlso Val(txtDisc.Text) > 0 Then
                drGrd.Item(GrdEnum.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                drGrd.Item(GrdEnum.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                drGrd.Item(GrdEnum.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
            Else
                drGrd.Item(GrdEnum.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                drGrd.Item(GrdEnum.DiscountFactor) = 0   'TFS2827
                drGrd.Item(GrdEnum.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
            End If
            drGrd.Item(GrdEnum.PostDiscountPrice) = Val(txtPDP.Text)   'TFS2827
            ''EndTFS2827
            'dtGrd.Rows.InsertAt(drGrd, 0)

            ''Start TFS4804
            Dim Str As String = " Select  BatchNo,ExpiryDate  From  StockDetailTable  where BatchNo not in ('','0','xxxx')  And ArticledefId = " & Me.cmbItem.ActiveRow.Cells(0).Value & "  Group by BatchNo,ExpiryDate Having Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0)) > 0  ORDER BY ExpiryDate Asc"
            Dim dt As DataTable = GetDataTable(Str)
            If dt.Rows.Count > 0 Then
                drGrd.Item(GrdEnum.BatchNo) = dt.Rows(0).Item("BatchNo").ToString
                drGrd.Item(GrdEnum.ExpiryDate) = Convert.ToDateTime(dt.Rows(0).Item("ExpiryDate").Date)
            Else
                drGrd.Item(GrdEnum.BatchNo) = "xxxx"
                drGrd.Item(GrdEnum.ExpiryDate) = Convert.ToDateTime(Date.Now.AddMonths(1))
            End If
            ''End TFS4804
            dtGrd.Rows.Add(drGrd)
            TradePrice = 0D
            Freight_Rate = 0D
            MarketReturns_Rate = 0D
            FlatRate = 0D


            'Task 3913 Saad Afzaal move scroll bar at the end when item added into the grid 
            grd.MoveLast()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged
        'If cmbCategory.SelectedIndex > 0 Then
        '    FillCombo("ItemFilter")
        'End If
        Try
            If IsFormOpen = True Then
                'Before against task:2366
                'FillCombo("Item")
                If flgLocationWiseItem = True Then FillCombo("Item")
                'End Task:2366
                If flgLoadItems = True Then
                    If Me.cmbCategory.SelectedIndex = 0 Or Me.cmbCategory.SelectedIndex > 0 Then
                        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                            If Me.grd.RowCount > 0 Then
                                r.BeginEdit()
                                r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
                                r.EndEdit()
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetTotal()

        If Me.grd.RowCount = 0 Then
            txtNetBill.Text = 0
            Exit Sub
        End If
        Me.grd.UpdateData()

        Dim TransitValue As Double = 0D
        Dim MarketReturnsValue As Double = 0D
        Dim WHTaxValue As Double = 0D
        Dim SpecialAdj As Double = 0D
        Dim Disc As Double = 0D
        Dim BillAfterDisc As Double = 0D
        Dim TradeValue As Double = 0D

        Dim SalesOrderAnalysis As Boolean
        If Not getConfigValueByType("SalesOrderAnalysis").ToString = "Error" Then
            SalesOrderAnalysis = Convert.ToBoolean(getConfigValueByType("SalesOrderAnalysis").ToString())
        End If

        If SalesOrderAnalysis = True Then
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                MarketReturnsValue += IIf(r.Cells(GrdEnum.Unit).Value = "Pack", (((r.Cells("Qty").Value * r.Cells("PackQty").Value) + r.Cells("SchemeQty").Value) * r.Cells("MarketReturns").Value), ((r.Cells("Qty").Value + r.Cells("SchemeQty").Value) * r.Cells("MarketReturns").Value))
                Disc += IIf(r.Cells(GrdEnum.Unit).Value = "Loose", (((r.Cells("Qty").Value * r.Cells("CurrentPrice").Value) * r.Cells("Discount_Percentage").Value) / 100), ((((r.Cells("Qty").Value * r.Cells("PackQty").Value) * r.Cells("CurrentPrice").Value) * r.Cells("Discount_Percentage").Value) / 100))
                BillAfterDisc += IIf(r.Cells(GrdEnum.Unit).Value = "Loose", ((r.Cells("Qty").Value * r.Cells("CurrentPrice").Value) - Disc), (((r.Cells("Qty").Value * r.Cells("PackQty").Value) * r.Cells("CurrentPrice").Value) - Disc))
                If Me.rbtAdjFlat.Checked = False Then
                    SpecialAdj += ((BillAfterDisc * Val(Me.txtSpecialAdjustment.Text)) / 100)
                End If
                TradeValue += IIf(r.Cells(GrdEnum.Unit).Value = "Loose", (((r.Cells("Qty").Value * r.Cells("TradePrice").Value) + ((r.Cells("SchemeQty").Value * r.Cells("CurrentPrice").Value) * r.Cells("SalesTax_Percentage").Value) / 100)), ((((r.Cells("Qty").Value * r.Cells("PackQty").Value) * r.Cells("TradePrice").Value) + ((r.Cells("SchemeQty").Value * r.Cells("CurrentPrice").Value) * r.Cells("SalesTax_Percentage").Value) / 100)))
            Next
            If Me.rbtAdjFlat.Checked = True Then
                SpecialAdj = Val(Me.txtSpecialAdjustment.Text)
            End If
            'WHTaxValue = ((Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) * WHTaxPercent) / 100)
            WHTaxValue = ((Val(TradeValue) * WHTaxPercent) / 100)
            TransitValue = ((Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) * TransitPercent) / 100)
            NetAmount = Math.Round((Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("BillValueAfterDiscount"), Janus.Windows.GridEX.AggregateFunction.Sum)) + TransitValue + WHTaxValue) - (SpecialAdj + MarketReturnsValue), TotalAmountRounding)
        Else
            'NetAmount = (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.Total), Janus.Windows.GridEX.AggregateFunction.Sum)))
            NetAmount = Math.Round((Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum))), TotalAmountRounding)
            If Me.rbtAdjFlat.Checked = False Then
                Adjustment = ((NetAmount * Val(Me.txtSpecialAdjustment.Text)) / 100)
            Else
                Adjustment = Val(Me.txtSpecialAdjustment.Text)
            End If
            NetAmount = Math.Round((NetAmount - Adjustment), TotalAmountRounding)
        End If
        ''Start TFS3758
        For Each r As Janus.Windows.GridEX.GridEXRow In grd.GetRows
            If flgExcludeTaxPrice = True Then
                NetAmount += Val(r.Cells("Tax Amount").Value.ToString)
            End If
        Next
        ''End TFS3758
        Me.txtNetBill.Text = Math.Round(NetAmount, TotalAmountRounding)

    End Sub

    Private Sub FillCombo(ByVal strCondition As String)
        Dim str As String


        If strCondition = "Item" Then
            'Marked Against Task# 7 Ali Ansari 
            'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ISNULL(SalePrice,0) as Price, Isnull(PurchasePrice,0) as PurchasePrice, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand] FROM ArticleDefView where Active=1 AND ArticleDefView.SalesItem=1"
            'Marked Against Task# 7 Ali Ansari 
            'Changes Against Task# 7 Ali Ansari Add PackQty in Selection 2015-02-20
            'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,PackQty as PackQty, ISNULL(SalePrice,0) as Price, Isnull(PurchasePrice,0) as PurchasePrice, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand] FROM ArticleDefView where Active=1 AND ArticleDefView.SalesItem=1"
            'Changes Against Task# 7 Ali Ansari Add PackQty in Selection 2015-02-20
            str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,ArticleDefView.ArticleBrandName As Grade,PackQty as PackQty, ISNULL(SalePrice,0) as Price, Isnull(PurchasePrice,0) as PurchasePrice, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand],IsNull(ArticleDefView.Cost_Price,0) as [Cost Price], IsNull(TradePrice,0) as [Trade Price] FROM ArticleDefView where Active=1 AND ArticleDefView.SalesItem=1"
            If flgCompanyRights = True Then
                str += " AND ArticleDefView.CompanyId=" & MyCompanyId
            End If
            If flgLocationWiseItem = True Then
                'Comment against task:2366
                'If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                str += " AND  ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
            End If
            'Task:2417 Filter Item By Customer
            If Me.RadioButton2.Checked = True Then
                If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
                str += " AND MasterId in(Select ArticleDefId From ArticleDefCustomers WHERE CustomerId=N'" & Me.cmbVendor.Value & "')"
            End If
            'End Task:2417
            'else
            'str += " ORDER BY ArticleDefView.SortOrder" Comment against task:24522
            'End If

            ''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
            If ItemSortOrder = True Then
                str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByCode = True Then
                str += " ORDER BY ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByName = True Then
                str += " ORDER BY ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            Else
                str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            End If
            'End Task:2452

            'FillDropDown(cmbItem, str)
            FillUltraDropDown(Me.cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                If ItemFilterByName = True Then
                    rdoName.Checked = True
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                Else
                    If Me.rdoCode.Checked = True Then
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                    Else
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                    End If
                End If
            End If
        ElseIf strCondition = "Category" Then
            'Task#16092015 Load  Locations user wise
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Code from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order"
            'Else
            '    str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            'End If

            'str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
            '       & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Where Active =1 order by sort_order " _
            '       & " Else " _
            '       & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order"

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                    & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order " _
                   & " Else " _
                    & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order"


            FillDropDown(cmbCategory, str, False)

        ElseIf strCondition = "SearchLocation" Then
            str = "Select CompanyId, CompanyName from CompanyDefTable order by 1"
            FillDropDown(Me.cmbSearchLocation, str, False)

        ElseIf strCondition = "ItemFilter" Then
            str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,ArticleDefView.ArticleBrandName As Grade,ISNULL(SalePrice,0) as Price, Isnull(PurchasePrice,0) as PurchasePrice , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand] FROM         ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue & " "
            If flgCompanyRights = True Then
                str += " AND ArticleDefView.CompanyId=" & MyCompanyId
            End If
            str += "  Order By ArticleDefView.SortOrder"
            FillUltraDropDown(cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True

        ElseIf strCondition = "Vendor" Then
            'str = "SELECT     tblCustomer.AccountId AS ID, tblCustomer.CustomerName AS Name, tblListTerritory.TerritoryName AS Territory, tblListCity.CityName AS City,  " & _
            '        "tblListState.StateName AS State, tblCustomer.AccountId AS AcId " & _
            '        "FROM         tblListTerritory INNER JOIN " & _
            '        "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
            '        "tblListState ON tblListCity.StateId = tblListState.StateId INNER JOIN " & _
            '        "tblCustomer ON tblListTerritory.TerritoryId = tblCustomer.Territory"
            'Before against task:2373
            'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
            '                    "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, tblCustomer.Email, tblCustomer.Phone " & _
            '                    "FROM dbo.tblCustomer INNER JOIN " & _
            '                    "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '                    "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '                    "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                    "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                    "WHERE     (dbo.vwCOADetail.account_type = 'Customer')  "
            'Task:2373 Added Column Sub Sub Title
            'Before against task:2504
            'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
            '             "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, tblCustomer.Email, tblCustomer.Phone, vwCOADetail.Sub_Sub_Title " & _
            '             "FROM dbo.tblCustomer INNER JOIN " & _
            '             "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '             "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '             "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '             "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '             "WHERE     (dbo.vwCOADetail.account_type = 'Customer')  "
            'End Task:2373
            'Task:Added Column Credit_Limit
            Dim ShowVendorOnSales As Boolean = False
            Dim ShowMiscAccountsOnSales As Boolean = False

            If Not getConfigValueByType("Show Vendor On Sales") = "Error" Then
                ShowVendorOnSales = CBool(getConfigValueByType("Show Vendor On Sales"))
            End If
            If Not getConfigValueByType("ShowMiscAccountsOnSales") = "Error" Then
                ShowMiscAccountsOnSales = CBool(getConfigValueByType("ShowMiscAccountsOnSales"))
            End If
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_code as [Code],  dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                      "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(tblCustomer.CridtLimt,0) as CreditLimit " & _
                      "FROM dbo.tblCustomer INNER JOIN " & _
                      "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                      "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                      "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                    "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id   " _
                      & " WHERE dbo.vwCOADetail.detail_title Is Not NULL " & IIf(ShowVendorOnSales = True, " AND (dbo.vwCOADetail.account_type in ('Customer','Vendor'))", " AND (dbo.vwCOADetail.account_type in ('Customer'))") & "" _
                             & "" & IIf(ShowMiscAccountsOnSales = True, " OR vwCOADetail.coa_detail_id IN (SELECT  DISTINCT tblCOAMainSubSubDetail.coa_detail_id " & _
                                   "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) ", "") & ""
            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            'If LoginGroup = "Administrator" Then
            If GetMappedUserId() > 0 And getGroupAccountsConfigforSales(Me.Name) And LoginGroup <> "Administrator" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_code as [Code],  dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                     "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(tblCustomer.CridtLimt,0) as CreditLimit " & _
                     "FROM dbo.tblCustomer INNER JOIN " & _
                     "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                     "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                     "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                   "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id   " _
                     & " WHERE ( dbo.vwCOADetail.detail_title Is Not NULL ) "
                str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ") ) "
                ''TFS4689 : Getting Relevent Accounts according to the screen and Configuration
                If ShowVendorOnSales = True Then
                    str += " And dbo.vwCOADetail.account_type in ('Customer','Vendor') "
                Else
                    str += " AND (dbo.vwCOADetail.account_type in ('Customer')) "
                End If
            End If
            ''End TFS3322

            'If getConfigValueByType("ShowMiscAccountsOnSales") = "True" Then
            '    str += " or vwCOADetail.coa_detail_id in (SELECT tblCOAMainSubSubDetail.coa_detail_id " & _
            '           "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) "
            'End If
            'If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
            '    str += " AND (dbo.vwCOADetail.account_type = 'Customer')  "
            'Else
            '    str += " AND (dbo.vwCOADetail.account_type in('Customer','Vendor'))  "
            'End If
            'End Task:2504
            If IsEditMode = False Then
                str += " AND vwCOADetail.Active=1"
            Else
                str += " AND vwCOADetail.Active in(0,1,Null)"
            End If
            str += "order by tblCustomer.Sortorder, vwCOADetail.detail_title"
            FillUltraDropDown(cmbVendor, str)
            If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("TypeId").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" 'Task:2373 Change Caption
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("CreditLimit").Hidden = True 'Tas:2504 Set Hidden Column
            End If
        ElseIf strCondition = "SearchVendor" Then
            Dim ShowVendorOnSales As Boolean = False
            Dim ShowMiscAccountsOnSales As Boolean = False

            If Not getConfigValueByType("Show Vendor On Sales") = "Error" Then
                ShowVendorOnSales = CBool(getConfigValueByType("Show Vendor On Sales"))
            End If
            If Not getConfigValueByType("ShowMiscAccountsOnSales") = "Error" Then
                ShowMiscAccountsOnSales = CBool(getConfigValueByType("ShowMiscAccountsOnSales"))
            End If
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                               "dbo.tblListTerritory.TerritoryName as Territory, vwCOADetail.Contact_Email as Email, vwCOADetail.Contact_Phone as Phone, vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_title " & _
                                               "FROM         dbo.tblCustomer INNER JOIN " & _
                                               "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                               "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                               "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                               "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " _
                                               & " WHERE dbo.vwCOADetail.detail_title Is Not NULL " & IIf(ShowVendorOnSales = True, " AND (dbo.vwCOADetail.account_type in ('Customer','Vendor'))", " AND (dbo.vwCOADetail.account_type in ('Customer'))") & "" _
                                       & "" & IIf(ShowMiscAccountsOnSales = True, " OR vwCOADetail.coa_detail_id IN (SELECT  DISTINCT tblCOAMainSubSubDetail.coa_detail_id " & _
                                   "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) ", "") & ""

            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title"
            FillUltraDropDown(cmbSearchAccount, str)
            If Me.cmbSearchAccount.DisplayLayout.Bands.Count > 0 Then
                Me.cmbSearchAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbSearchAccount.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" 'Task:2373 Change Caption
                Me.cmbSearchAccount.DisplayLayout.Bands(0).Columns("Email").Hidden = True
            End If
        ElseIf strCondition = "SO" Then
            str = "Select SalesID, SalesNo from SalesMasterTable where SalesId not in(select PoId from salesReturnMasterTable)"
            FillDropDown(cmbPo, str)

        ElseIf strCondition = "SOComplete" Then
            str = "Select SalesID, SalesNo from SalesMasterTable "
            FillDropDown(cmbPo, str)
        ElseIf strCondition = "SM" Then
            str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE ISNULL(Sale_Order_Person,0)=1"
            FillDropDown(Me.cmbSalesMan, str)
        ElseIf strCondition = "grdLocation" Then
            'Task#16092015  Load Locations user wise
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Name From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")"
            'Else
            '    str = "Select Location_Id, Location_Name From tblDefLocation"
            'End If
            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & " And Location_Id Is Not Null) " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Me.grd.RootTable.Columns(GrdEnum.LocationId).ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
            End If
        ElseIf strCondition = "Company" Then
            'Before against task:2427
            'str = "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & "  Order By 1"
            'Task:2427 Added Column CostCenterId
            'Task#16092015 Load Companies  user wise
            'If getConfigValueByType("UserwiseCompany").ToString = "True" Then
            '    str = "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId From CompanyDefTable WHERE CompanyName <> '' " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")  Order By 1"
            'Else
            '    str = "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & "  Order By 1"
            'End If
            str = "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & " And CompanyId Is Not Null) " _
                & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable WHERE CompanyName <> '' " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")" _
                & "Else " _
                & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""

            'End Task:2427
            FillDropDown(Me.cmbCompany, str, False)
        ElseIf strCondition = "Project" Then
            str = String.Empty
            FillDropDown(Me.cmbProject, "Select CostCenterId, Name From tblDefCostCenter Where Active=1")
        ElseIf strCondition = "ArticlePack" Then
            Me.cmbUnit.ValueMember = "ArticlePackId"
            Me.cmbUnit.DisplayMember = "PackName"
            Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
        ElseIf strCondition = "Quotation" Then
            ''Start TFS3738 : 04-07-2018 : Query Added Against TFS3738
            ''Start TFS3738 : Ayesha Rehman : 04-07-2018 : Selecting Adjustment ,Adj_flg column from QuotationMasterTable
            FillDropDown(Me.cmbQuotation, "Select IsNull(QuotationId,0) as QuotationId, QuotationNo, IsNull(LocationId,0) as LocationId, IsNull(VendorId,0) as VendorId, IsNull(CostCenterId,0) as CostCenterId, Remarks, ISNULL(SpecialAdjustment,0) as SpecialAdjustment,ISNULL(Adjustment,0) as Adjustment, ISNULL(Adj_Flag,0) as Adj_Flag From QuotationMasterTable  WHERE VendorId=" & IIf(Me.cmbVendor.ActiveRow Is Nothing, 0, Me.cmbVendor.Value) & " And LocationId = " & Me.cmbCompany.SelectedValue & " AND IsNull(Apprved,0)=1 And Status ='Open' Order By IsNull(QuotationId,0) DESC") ''Not In(Select IsNull(QuotationId,0) From SalesOrderMasterTable WHERE IsNull(QuotationId,0) <> 0)
        ElseIf strCondition = "OrderStatus" Then
            FillDropDown(Me.cmbOrderStatus, "Select OrderStatusID,OrderStatus, OrderGroup From SalesOrderStatusTable ORDER BY OrderStatus ASC")
        ElseIf strCondition = "OrderType" Then
        ElseIf strCondition = "Colour" Then
            FillDropDown(Me.cmbColour, "Select Distinct Other_Comments, Other_Comments From SalesOrderDetailTable Where Other_Comments <> '' Order By 1  ASC", False)
            Me.cmbColour.SelectedIndex = -1
        ElseIf strCondition = "ItemDeliverySchedule" Then
            str = String.Empty
            'str = "Select ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription, SalesOrderDetailTable.SalesOrderDetailId, SalesOrderDetailTable.SalesOrderId, (IsNull(SalesOrderDetailTable.Qty, 0)-Sum(IsNull(SOItemDeliverySchedule.ScheduleQty, 0))) As RemainingQty  From ArticleDefTable Inner Join SalesOrderDetailTable On ArticleDefTable.ArticleId = SalesOrderDetailTable.ArticleDefId Left Outer Join SOItemDeliverySchedule On SalesOrderDetailTable.SalesOrderDetailId = SOItemDeliverySchedule.SalesOrderDetailId Where SalesOrderDetailTable.SalesOrderId =" & Me.grdSaved.GetRow.Cells("SalesOrderId").Value & " Group by SOItemDeliverySchedule.SalesOrderDetailId"
            str = "Select SalesOrderDetailTable.SalesOrderDetailId, ArticleDefTable.ArticleDescription, ArticleDefTable.ArticleId, SalesOrderDetailTable.SalesOrderId, SalesOrderDetailTable.Qty From ArticleDefTable Inner Join SalesOrderDetailTable On ArticleDefTable.ArticleId = SalesOrderDetailTable.ArticleDefId Where SalesOrderDetailTable.SalesOrderId =" & Me.grdSaved.GetRow.Cells("SalesOrderId").Value & ""
            FillUltraDropDown(Me.cmbItemWiseDelivery, str, True)
            cmbItemWiseDelivery.Rows(0).Activate()
        ElseIf strCondition = "DispatchToLocation" Then

            FillUltraDropDown(Me.cmbDispatchTo, "Select Distinct PK_Id, CompanyLocation As [Company Location], ContactName As Name, Designation, Mobile, Email, Address, Department, Company From tblCompanyContacts Where CompanyLocation <> '' And RefCompanyId = " & Me.cmbVendor.Value & " Order By 1  ASC")
            Me.cmbDispatchTo.Rows(0).Activate()
            Me.cmbDispatchTo.DisplayLayout.Bands(0).Columns("PK_Id").Hidden = True
            'Me.cmbDispatchTo.SelectedIndex = -1
        ElseIf strCondition = "AllDispatchToLocation" Then

            FillUltraDropDown(Me.cmbDispatchTo, "Select Distinct PK_Id, CompanyLocation As [Company Location], ContactName As Name, Designation, Mobile, Email, Address, Department, Company From tblCompanyContacts Order By 1  ASC")
            Me.cmbDispatchTo.Rows(0).Activate()
            Me.cmbDispatchTo.DisplayLayout.Bands(0).Columns("PK_Id").Hidden = True
            'Me.cmbDispatchTo.SelectedIndex = -1
        ElseIf strCondition = "InvoiceToLocation" Then
            FillUltraDropDown(Me.cmbInvoiceTo, "Select Distinct PK_Id, CompanyLocation As [Company Location], ContactName As Name, Designation, Mobile, Email, Address, Department, Company From tblCompanyContacts Where CompanyLocation <> '' And RefCompanyId = " & Me.cmbVendor.Value & " Order By 1  ASC")
            Me.cmbInvoiceTo.Rows(0).Activate()
            Me.cmbInvoiceTo.DisplayLayout.Bands(0).Columns("PK_Id").Hidden = True
        ElseIf strCondition = "TermOfPayments" Then
            FillDropDown(Me.cmbTermOfPayments, "Select Distinct TermOfPayments, TermOfPayments From SalesOrderMasterTable Where TermOfPayments <> '' Order By 1  ASC", False)
            'Me.cmbInvoiceTo.SelectedIndex = -1
            'FillDropDown(Me.cmbOrderType, "Select SaleOrderTypeId,SalesOrderTypeName, SalesOrderTypeCode, Type_Service, Active From SalesOrderTypetable ORDER BY SalesOrderTypeName ASC")
        ElseIf strCondition = "Currency" Then ''TASK-407
            str = String.Empty
            str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
            FillDropDown(Me.cmbCurrency, str, False)
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
        ElseIf strCondition = "OutwardExpense" Then
            FillUltraDropDown(Me.cmbOutwardExpense, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], sub_sub_title as [Account Head], Account_Type as [Account Type] From vwCOADetail where detail_title <> ''")
            Me.cmbOutwardExpense.Rows(0).Activate()
            If Me.cmbOutwardExpense.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbOutwardExpense.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
            ''TASK TFS1764 
        ElseIf strCondition = "Transporter" Then
            str = "select * from tbldeftransporter where active=1 order by sortorder,2"
            FillDropDown(Me.cmbTransporter, str)
            ''END TFS1764

            '28-03-2018 : Ayesha Rehman : TFS2827 : Filling Combo Discount Type 
        ElseIf strCondition = "Discount Type" Then
            str = "select DiscountID, DiscountType from tblDiscountType "
            FillDropDown(Me.cmbDiscountType, str, False)
        ElseIf strCondition = "grdDiscountType" Then
            str = "select DiscountID, DiscountType from tblDiscountType"
            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns("DiscountId").ValueList.PopulateValueList(dt.DefaultView, "DiscountID", "DiscountType")
            ''End TFS2827
            ''Start TFS4181
        ElseIf strCondition = "grdBatchNo" Then
            str = "Select  BatchNo,BatchNo,ExpiryDate  From  StockDetailTable  where BatchNo not in ('','0','xxxx') Group by BatchNo,ExpiryDate Having (Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0))) > 0 ORDER BY ExpiryDate  desc "
            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns("BatchNo").ValueList.PopulateValueList(dt.DefaultView, "BatchNo", "BatchNo")
            ''End TFS4181
        End If

    End Sub
    Private Sub txtPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaid.TextChanged
        Try
            txtBalance.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(txtPaid.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' This Function is Made to get Adjustment on invoice load from Quotation
    ''' </summary>
    ''' <param name="QuotationId"></param>
    ''' <remarks>TFS3738 : Ayesha Rehman : 07-04-2018 </remarks>
    Public Sub GetAdjustment(ByVal QuotationId As Integer)
        Try
            Me.grd.Update()
            Dim spadj As Double = 0D
            Dim adj As Double = 0D

            spadj = Val(CType(Me.cmbQuotation.SelectedItem, DataRowView).Item("SpecialAdjustment").ToString)
            adj = Val(CType(Me.cmbQuotation.SelectedItem, DataRowView).Item("Adjustment").ToString)
            If spadj > 0 Then
                adj = 0
                For i As Integer = 0 To Me.grd.RowCount - 1
                    adj += (Me.grd.GetRows(i).Cells("Qty").Value * Me.grd.GetRows(i).Cells("PostDiscountPrice").Value) - (((Me.grd.GetRows(i).Cells("Qty").Value * Me.grd.GetRows(i).Cells("PostDiscountPrice").Value) * Me.grd.GetRows(i).Cells("DiscountFactor").Value) / 100)
                Next
                adj = ((adj * spadj) / 100)
            End If
            Me.txtSpecialAdjustment.Text = adj
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function Save() As Boolean
        If Me.chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        Me.grd.UpdateData()
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("SO", 6, "SalesOrderMasterTable", "SalesOrderNo")
        setVoucherNo = Me.txtPONo.Text
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        'Dim TransitPercent As Double = 0D
        'Dim WHTaxPercent As Double = 0D
        'Dim NetAmount As Double = 0D
        Dim TransitValue As Double = 0D
        Dim MarketReturnsValue As Double = 0D
        Dim WHTaxValue As Double = 0D
        Dim SpecialAdj As Double = 0D
        Dim Disc As Double = 0D
        Dim BillAfterDisc As Double = 0D
        Dim TradeValue As Double = 0D

        Dim SalesOrderAnalysis As Boolean
        If Not getConfigValueByType("SalesOrderAnalysis").ToString = "Error" Then
            SalesOrderAnalysis = Convert.ToBoolean(getConfigValueByType("SalesOrderAnalysis").ToString())
        End If
        'If Not GetConfigValue("TransitInssuranceTax").ToString = "Error" Then
        '    TransitPercent = Val(GetConfigValue("TransitInssuranceTax"))
        'End If
        'If Not GetConfigValue("WHTax_Percentage").ToString = "Error" Then
        '    WHTaxPercent = Val(GetConfigValue("WHTax_Percentage"))
        'End If


        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'objCommand.CommandText = "Insert into SalesOrderMasterTable (LocationId,SalesOrderNo,SalesOrderDate,VendorId,SalesOrderQty,SalesOrderAmount, CashPaid, Remarks,UserName, status) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(txtTotalQty.Text) & "," & Val(txtAmount.Text) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "')"

            'objCommand.CommandText = "Insert into SalesOrderMasterTable (LocationId,SalesOrderNo,SalesOrderDate,VendorId,SalesOrderQty,SalesOrderAmount, CashPaid, Remarks,UserName, status) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "')"
            'objCommand.ExecuteNonQuery()
            If SalesOrderAnalysis = True Then
                'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                '    MarketReturnsValue += IIf(r.Cells(GrdEnum.Unit).Value = "Pack", (((r.Cells("Qty").Value * r.Cells("PackQty").Value) + r.Cells("SchemeQty").Value) * r.Cells("MarketReturns").Value), ((r.Cells("Qty").Value + r.Cells("SchemeQty").Value) * r.Cells("MarketReturns").Value))
                '    Disc += IIf(r.Cells(GrdEnum.Unit).Value = "Loose", (((r.Cells("Qty").Value * r.Cells("CurrentPrice").Value) * r.Cells("Discount_Percentage").Value) / 100), ((((r.Cells("Qty").Value * r.Cells("PackQty").Value) * r.Cells("CurrentPrice").Value) * r.Cells("Discount_Percentage").Value) / 100))
                '    BillAfterDisc += IIf(r.Cells(GrdEnum.Unit).Value = "Loose", ((r.Cells("Qty").Value * r.Cells("CurrentPrice").Value) - Disc), (((r.Cells("Qty").Value * r.Cells("PackQty").Value) * r.Cells("CurrentPrice").Value) - Disc))
                '    SpecialAdj += ((BillAfterDisc * Val(Me.txtSpecialAdjustment.Text)) / 100)
                '    TradeValue += IIf(r.Cells(GrdEnum.Unit).Value = "Loose", (((r.Cells("Qty").Value * r.Cells("TradePrice").Value) + ((r.Cells("SchemeQty").Value * r.Cells("CurrentPrice").Value) * r.Cells("SalesTax_Percentage").Value) / 100)), ((((r.Cells("Qty").Value * r.Cells("PackQty").Value) * r.Cells("TradePrice").Value) + ((r.Cells("SchemeQty").Value * r.Cells("CurrentPrice").Value) * r.Cells("SalesTax_Percentage").Value) / 100)))
                'Next
                'WHTaxValue = ((TradeValue * WHTaxPercent) / 100)
                'TransitValue = ((Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("BillValueAfterDiscount"), Janus.Windows.GridEX.AggregateFunction.Sum)) * TransitPercent) / 100)
                'NetAmount = (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("BillValueAfterDiscount"), Janus.Windows.GridEX.AggregateFunction.Sum)) + TransitValue + WHTaxValue) - (SpecialAdj + MarketReturnsValue)
                NetAmount = Math.Round(Val(Me.txtNetBill.Text), TotalAmountRounding)
            Else
                NetAmount = (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.Total), Janus.Windows.GridEX.AggregateFunction.Sum)))
            End If

            'objCommand.CommandText = "Insert into SalesOrderMasterTable (LocationId,SalesOrderNo,SalesOrderDate,VendorId,SalesOrderQty,SalesOrderAmount, CashPaid, Remarks,UserName, status, SpecialAdjustment,Posted, PONo, SOP_ID,Delivery_Date, Adj_Flag, Adjustment,CostCenterId, PO_Date,EditionalTax_Percentage,SED_Percentage, Terms_And_Condition,OrderStatus) values( " _
            '                       & Me.cmbCompany.SelectedValue & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & NetAmount & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', " & Me.cmbSalesMan.SelectedValue & ", " & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", " & IIf(Me.rbtAdjFlat.Checked = True, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", " & Me.cmbProject.SelectedValue & ", " & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & TransitPercent & "," & WHTaxPercent & ",N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "',N'" & Me.cmbOrderStatus.Text.Replace("'", "''") & "') Select @@Identity"
            'objCommand.CommandText = "Insert into SalesOrderMasterTable (LocationId,SalesOrderNo,SalesOrderDate,VendorId,SalesOrderQty,SalesOrderAmount, CashPaid, Remarks,UserName, status, SpecialAdjustment,Posted, PONo, SOP_ID,Delivery_Date, Adj_Flag, Adjustment,CostCenterId, PO_Date,EditionalTax_Percentage,SED_Percentage, Terms_And_Condition,OrderStatus, QuotationId) values( " _
            '                       & Me.cmbCompany.SelectedValue & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & NetAmount & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', " & Me.cmbSalesMan.SelectedValue & ", " & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", " & IIf(Me.rbtAdjFlat.Checked = True, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", " & Me.cmbProject.SelectedValue & ", " & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & TransitPercent & "," & WHTaxPercent & ",N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "',N'" & Me.cmbOrderStatus.Text.Replace("'", "''") & "', " & IIf(Me.cmbQuotation.SelectedIndex = -1, 0, Me.cmbQuotation.SelectedValue) & ") Select @@Identity"
            ''TASK TFS1764 new field of TransporterId on 15-11-2017
            objCommand.CommandText = "Insert into SalesOrderMasterTable(LocationId,SalesOrderNo,SalesOrderDate,VendorId,SalesOrderQty,SalesOrderAmount, CashPaid, Remarks,UserName, status, SpecialAdjustment,Posted, PONo, SOP_ID,Delivery_Date, Adj_Flag, Adjustment,CostCenterId, PO_Date,EditionalTax_Percentage,SED_Percentage, Terms_And_Condition,SaleOrderStatusID, QuotationId, RevisionNumber, SpecialInstructions, TechnicalDrawingNumber, TechnicalDrawingDate, AccountsRemarks, StoreRemarks, ProductionRemarks, ServicesRemarks, SalesRemarks, TermOfPayments, TermOfDelivery, QuotationNo, QuotationDate, POType, UserId, TransporterId, DCStatus) values( " _
                                  & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbCompany.SelectedValue) & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQuantity"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & NetAmount & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', " & Me.cmbSalesMan.SelectedValue & ", " & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", " & IIf(Me.rbtAdjFlat.Checked = True, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", " & Me.cmbProject.SelectedValue & ", " & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & TransitPercent & "," & WHTaxPercent & ",N'" & ReplaceNewLine(Me.txtTerms_And_Condition.Text, False).Replace("'", "''") & "'," & IIf(Me.cmbOrderStatus.SelectedValue = Nothing, 0, Me.cmbOrderStatus.SelectedValue) & ", " & IIf(Me.cmbQuotation.SelectedIndex = -1, 0, Me.cmbQuotation.SelectedValue) & ", 0, N'" & Me.txtSpecialInstructions.Text.Replace("'", "''") & "', " & Val(txtTechnicalDrawingNo.Text) & ", " & IIf(Me.dtpTechnicalDrawingDate.Checked = True, "N'" & Me.dtpTechnicalDrawingDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'", "NULL") & " , '" & Me.txtAccountsRemarks.Text.Replace("'", "''") & "', '" & Me.txtStoreRemarks.Text.Replace("'", "''") & "', '" & Me.txtProductionRemarks.Text.Replace("'", "''") & "', '" & Me.txtServicesRemarks.Text.Replace("'", "''") & "', '" & Me.txtSalesRemarks.Text.Replace("'", "''") & "', '" & Me.cmbTermOfPayments.Text.Replace("'", "''") & "', '" & Me.cmbTermOfDelivery.Text.Replace("'", "''") & "', " & Val(txtQuotationNo.Text) & ", " & IIf(Me.dtpQuotationDate.Checked = True, "N'" & Me.dtpQuotationDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'", "NULL") & ", '" & Me.cmbPOType.Text.Replace("'", "''") & "', " & Val(LoginUserId) & ", " & IIf(Me.cmbTransporter.SelectedValue = Nothing, 0, Me.cmbTransporter.SelectedValue) & ", N'" & EnumStatus.Open.ToString & "' ) Select @@Identity"
            getVoucher_Id = objCommand.ExecuteScalar

            objCommand.CommandText = "insert into tblDefCostCenter(Name,Code,sortorder, CostCenterGroup, Active, OutwardGatepass, DayShift, IsLogical, Amount, SOBudget, SalaryBudget, DepartmentBudget, Contract) values(N'" & txtPONo.Text.Replace("'", "''") & "','" & txtPONo.Text.Replace("'", "''") & "','1', N'', 0, 0, 0, 0,'" & (NetAmount / 100) * 88 & "', 1, 0, 0,0)"
            objCommand.ExecuteNonQuery()


            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdOutwardExpDetail.GetRows
                If Val(r.Cells("Exp_Amount").Value.ToString) <> 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO OutwardExpenseDetailTable(SalesId, AccountId, Exp_Amount,DocType) Values(ident_current('SalesOrderMasterTable'), " & Val(r.Cells("AccountId").Value.ToString) & "," & Val(r.Cells("Exp_Amount").Value) & ",'SO')"
                    objCommand.ExecuteNonQuery()
                End If
            Next


            'objCommand.ExecuteNonQuery()

            'For i = 0 To grd.Rows.Count - 1
            '    If grd.Rows(i).Cells("Qty").Value <> 0 Then
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice) values( " _
            '                                & " ident_current('SalesOrderMasterTable')," & Val(grd.Rows(i).Cells(7).Value) & ",N'" & (grd.Rows(i).Cells(2).Value) & "'," & Val(grd.Rows(i).Cells(3).Value) & ", " _
            '                                & " " & IIf(grd.Rows(i).Cells(2).Value = "Loose", Val(grd.Rows(i).Cells(3).Value), (Val(grd.Rows(i).Cells(3).Value) * Val(grd.Rows(i).Cells(8).Value))) & ", " & Val(grd.Rows(i).Cells(4).Value) & ", " & Val(grd.Rows(i).Cells(8).Value) & " , " & Val(grd.Rows(i).Cells(9).Value) & " ) "

            '        objCommand.ExecuteNonQuery()
            '        'Val(grd.Rows(i).Cells(5).Value)
            '    End If
            'Next
            For i = 0 To grd.RowCount - 1
                'If Val(grd.GetRows(i).Cells("Qty").Value) <> 0 Then
                'objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice,PackPrice, Pack_Desc, Comments) values( " _
                '                        & " ident_current('SalesOrderMasterTable'), " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                'objCommand.ExecuteNonQuery()

                ''R-916 Added Column Comments
                objCommand.CommandText = ""
                'Before against task:M16
                'objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice,PackPrice, Pack_Desc, Comments) values( " _
                '                        & " ident_current('SalesOrderMasterTable'), " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "') "
                'Task:M16 Added Column Engine_No And Chassis_No
                'Before against task:2832
                'objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice,PackPrice, Pack_Desc, Comments, Engine_No, Chassis_No) values( " _
                '                       & " ident_current('SalesOrderMasterTable'), " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                '                       & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "') "
                'Task:2832 Added Field PlanedQty
                'objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice,PackPrice, Pack_Desc, Comments, Engine_No, Chassis_No,DeliveredQty,PlanedQty) values( " _
                '                      & " ident_current('SalesOrderMasterTable'), " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                '                      & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("DeliveredQty").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PlanedQty").Value.ToString) & ") "
                'objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice,PackPrice, Pack_Desc, Comments, Engine_No, Chassis_No,DeliveredQty,PlanedQty,Other_Comments) values( " _
                '                    & " ident_current('SalesOrderMasterTable'), " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                '                    & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("DeliveredQty").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PlanedQty").Value.ToString) & ",N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "') "

                objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice,PackPrice, Pack_Desc, Comments, Engine_No, Chassis_No,DeliveredQty,PlanedQty,Other_Comments,CostPrice, QuotationDetailId,SED_Tax_Percent,SED_Tax_Amount,ArticleAliasName,SaleOrderType, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, SerialNo , DiscountId, DiscountValue, DiscountFactor , PostDiscountPrice , BatchNo , ExpiryDate) values( " _
                                    & " ident_current('SalesOrderMasterTable'), " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & IIf(Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) = 0, "NULL", Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                                    & " " & Val(grd.GetRows(i).Cells(GrdEnum.TotalQuantity).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("DeliveredQty").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PlanedQty").Value.ToString) & ",N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(GrdEnum.OuotationDetailId).Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("SED_Tax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("SED_Tax_Amount").Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("ArticleAliasName").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("SaleOrderType").Value.ToString.Replace("'", "''") & "', " _
                                    & " " & Val(grd.GetRows(i).Cells(GrdEnum.BaseCurrencyId).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.BaseCurrencyRate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.CurrencyId).Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.CurrencyAmount).Value.ToString) & ", N'" & grd.GetRows(i).Cells("SerialNo").Value.ToString.Replace("'", "''") & "'," & Val(grd.GetRows(i).Cells("DiscountId").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("DiscountValue").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("DiscountFactor").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PostDiscountPrice").Value) & " , '" & Val(Me.grd.GetRows(i).Cells(GrdEnum.BatchNo).Value.ToString) & "' , '" & CType(grd.GetRows(i).Cells(GrdEnum.ExpiryDate).Value, Date).ToString("yyyy-M-d h:mm:ss tt") & "' )"

                objCommand.ExecuteNonQuery()
                '' Validate and update quantity in quotation against Purchase Order transaction against a quotation. 14-04-2016 
                'If Val(Me.grd.GetRows(i).Cells(GrdEnum.OuotationDetailId).Value.ToString) > 0 Then
                '    If Me.cmbQuotation.SelectedValue > 0 Then
                '        Dim dt As New DataTable
                '        Dim str As String = "Select IsNull(Qty, 0) As Qty, IsNull(SOQuantity, 0) As SOQuantity From QuotationDetailTable Where QuotationDetailId =" & Val(Me.grd.GetRows(i).Cells(GrdEnum.OuotationDetailId).Value.ToString) & " "
                '        dt = GetDataTable(str)
                '        If dt.Rows.Item(0).Item(0) >= dt.Rows.Item(0).Item(1) + IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) Then
                '            objCommand.CommandText = ""
                '            objCommand.CommandText = "Update QuotationDetailTable Set SOQuantity= ISNULL(SOQuantity, 0) + " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & " Where QuotationDetailId =" & Val(Me.grd.GetRows(i).Cells(GrdEnum.OuotationDetailId).Value.ToString) & ""
                '            objCommand.ExecuteNonQuery()
                '            If dt.Rows.Item(0).Item(0) = dt.Rows.Item(0).Item(1) + IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) Then
                '                objCommand.CommandText = ""
                '                objCommand.CommandText = "Update QuotationMasterTable Set Status ='Close' Where QuotationId = " & Val(Me.grd.GetRows(i).Cells(GrdEnum.QuotationId).Value.ToString) & ""
                '                objCommand.ExecuteNonQuery()
                '            End If

                '        Else
                '            msg_Error("Quantity is larger than available " & dt.Rows.Item(0).Item(0) & " quantity in quotation")
                '            Exit Function
                '            grd.Focus()
                '        End If

                '    End If

                'End If
                'Val(grd.Rows(i).Cells(5).Value)
                'End If
                SaveArticleAlias(Me.grd.GetRows(i), trans)
            Next
            SaveDocument(getVoucher_Id, Me.Name, trans)


            objCommand.CommandText = ""
            objCommand.CommandText = "Update QuotationDetailTable Set SOQuantity=IsNull(SOQuantity,0)+IsNull(SO.SOQty,0),  DeliveredTotalQty=IsNull(DeliveredTotalQty,0)+IsNull(SO.TotalDeliveredQty,0) From QuotationDetailTable,(Select QuotationDetailId, ArticleDefId, SUM(IsNull(Sz1,0)) as SOQty,  SUM(IsNull(Qty,0)) As TotalDeliveredQty From SalesOrderDetailTable WHERE SalesOrderID=" & getVoucher_Id & " AND IsNull(QuotationDetailId,0) <> 0 Group By QuotationDetailId,ArticleDefId) as SO WHERE SO.QuotationDetailId = QuotationDetailTable.QuotationDetailId And SO.ArticleDefId = QuotationDetailTable.ArticleDefId"
            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""
            objCommand.CommandText = "Update QuotationMasterTable Set Status=Case When IsNull(SO.BalanceQty,0) > 0 then 'Open' else 'Close' End  From QuotationMasterTable,(Select QuotationId, SUM(IsNull(Sz1,0)-IsNull(SOQuantity,0)) BalanceQty From QuotationDetailTable WHERE QuotationDetailId in(Select distinct QuotationDetailId From SalesOrderDetailTable WHERE SalesOrderID=" & getVoucher_Id & ")   Group By QuotationId  )  as SO WHERE SO.QuotationId = QuotationMasterTable.QuotationId "
            objCommand.ExecuteNonQuery()

            ''TASK-480 on 14-07-2016
            If getConfigValueByType("EnableDuplicateSalesOrder").ToString.ToUpper = "TRUE" Then
                Call CreateDuplicationSalesOrder(getVoucher_Id, "Save", trans)
            End If
            ''End TASK-480
            trans.Commit()
            Save = True
            setEditMode = False
            Total_Amount = NetAmount 'Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            'InsertVoucher()
            MarketReturnsValue = 0D
            Disc = 0D
            BillAfterDisc = 0D
            SpecialAdj = 0D
            TradeValue = 0D
            WHTaxValue = 0D
            TransitValue = 0D
            NetAmount = 0D
            Dim ValueTable As DataTable = GetSingle(getVoucher_Id)
            '' NotificationDAL.SaveAndSendNotification("Sales Order", "SalesOrderMasterTable", getVoucher_Id, ValueTable, "Sales > Sales Order")

            ' *** New Segment *** By Shahid Rasool *** 10-Jun-2017 ***
            '// Adding notification

            '// Creating new object of Notification configuration dal
            ''// Dal will be used to get users list for the notification 
            'Dim NDal As New NotificationConfigurationDAL

            ''// Creating new object of Agrius Notification class
            'Dim Notification As New AgriusNotifications

            ''// Reference document number
            'Notification.ApplicationReference = getVoucher_Id

            ''// Date of notification
            'Notification.NotificationDate = Now

            ''// Preparing notification title string
            'Notification.NotificationTitle = "New sales order [" & txtPONo.Text & "]  is added with " & grd.RowCount & " items."

            ''// Preparing notification description string
            'Notification.NotificationDescription = "New sales order [" & txtPONo.Text & "] is added by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

            ''// Setting source application as refrence in the notification
            'Notification.SourceApplication = "Sales Order"



            ''// Starting to get users list to add child

            ''// Creating notification detail object list
            'Dim List As New List(Of NotificationDetail)

            ''// Getting users list
            'List = NDal.GetNotificationUsers("Sales Order Created")

            ''// Adding users list in the Notification object of current inquiry
            'Notification.NotificationDetils.AddRange(List)

            ''// Getting and adding user groups list in the Notification object of current inquiry
            'Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("Sales Order Created"))

            ''// Not getting role list because no role is associated at the moment
            ''// We will need this in future and we can use it later
            ''// We can consult to Update function of this class


            ''// ***This segment will be used to save notification in database table

            ''// Creating new list of objects of Agrius Notification
            'Dim NList As New List(Of AgriusNotifications)

            ''// Copying notification object from current sales inquiry to newly defined instance
            ''// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
            'NList.Add(Notification)

            ''// Creating object of Notification DAL
            'Dim GNotification As New NotificationDAL

            ''// Saving notification to database
            'GNotification.AddNotification(NList)

            '// End Adding Notification

            '// End Adding Notification
            ' *** End Segment ***

        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try

        ''insert Activity Log
        SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Sales, Me.txtPONo.Text.Trim, True)
        ''Start TFS3113
        ''insert Approval Log
        SaveApprovalLog(EnumReferenceType.SalesOrder, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Sales Order," & cmbVendor.Text & "", Me.Name, 7)
        ''End TFS3113
        SendSMS()
    End Function
    Sub InsertVoucher()

        Try
            SaveVoucherEntry(GetVoucherTypeId("SV"), GetNextDocNo("SV", 6, "tblVoucher", "voucher_no"), Me.dtpPODate.Value, "", Nothing, Val(Me.cmbVendor.ActiveRow.Cells(0).Text.ToString), getConfigValueByType("SalesCreditAccount"), Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)), Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)), "Both", Me.Name, Me.txtPONo.Text, True)
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try

    End Sub

    Private Function FormValidate() As Boolean
        ''Aashir: 4442 :Added Addidtion verification on SO
        'START

        If getConfigValueByType("AdditionalVerificationOnSO") = "True" Then
            If IsEditMode = True Then
                Dim intCountAttachedFiles As Integer = 0I
                intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                If (arrFile.Count + intCountAttachedFiles) < 1 Then
                    ShowErrorMessage("Please attach at least one attachment to proceed ")
                    FormValidate = False
                    Exit Function
                End If
                
                If Me.txtCustPONo.Text.Length < 1 Then
                    ShowErrorMessage("Please Include PO No")
                    FormValidate = False
                    Exit Function
                End If
            Else

                If arrFile.Count < 1 Then
                    ShowErrorMessage("Please attach at least one attachment to proceed ")
                    FormValidate = False
                    Exit Function
                End If

                If Me.txtCustPONo.Text.Length < 1 Then
                    ShowErrorMessage("Please Include PO No")
                    FormValidate = False
                    Exit Function
                End If
            End If
        End If
        'end
        If txtPONo.Text = "" Then
            msg_Error("Please enter PO No.")
            txtPONo.Focus() : FormValidate = False : Exit Function
        End If
        'If cmbVendor.ActiveRow.Cells(0).Value <= 0 Then
        '    msg_Error("Please select customer")
        '    cmbVendor.Focus() : FormValidate = False : Exit Function
        'End If

        If Not Me.grd.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            cmbItem.Focus() : FormValidate = False : Exit Function
        End If
        'start task 3113 Added by abubakar Siddiq
        If IsEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document is in Approval Process") : Return False : Exit Function
                End If
            End If
        End If
        'end task 3113, Added by Abubakar Siddiq
        'Murtaza Change default currency rate required(10/25/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + " Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : FormValidate = False : Exit Function
        End If
        'Murtaza Change default currency rate required by Waqar bhai(10/25/2022)
        'Task:2801 Verify Order Status
        Dim dtSO As New DataTable
        dtSO = GetDataTable("Select Status From SalesOrderMasterTable WHERE SalesOrderId=" & Val(Me.txtReceivingID.Text) & "")
        If dtSO IsNot Nothing Then
            If dtSO.Rows.Count > 0 Then
                If dtSO.Rows(0).Item(0).ToString <> "Open" Then
                    If msg_Confirm("Do you want to open sales order") = True Then blnOpenSO = True Else blnOpenSO = False
                End If
            End If
        End If
        'End Task:2801

        Dim StockOutConfigration As String = ""
        If Not getConfigValueByType("StockOutConfigration").ToString = "Error" Then ''1596
            StockOutConfigration = getConfigValueByType("StockOutConfigration").ToString
        End If
        'ShowInformationMessage(StockOutConfigration)
        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            If StockOutConfigration.Equals("Required") AndAlso r.Cells(GrdEnum.BatchNo).Value.ToString = String.Empty Then
                msg_Error("Please Enter Value in Batch No")
                Return False
                Exit For
            End If
        Next

        'Task:2415 Validation Engine No and Chassis No.
        If flgVehicleIdentificationInfo = True Then
            If CheckDuplicateEngineNo() = True Then
                ShowErrorMessage("Engine no already exist.")
                Me.grd.Focus()
                Return False
            End If
            If CheckDuplicateChassisNo() = True Then
                ShowErrorMessage("Chassis no already exist.")
                Me.grd.Focus()
                Return False
            End If
        End If
        'End Task:2415

        'Task:2504 Credit Limit Validation
        'If Me.cmbVendor.ActiveRow Is Nothing Then Return False

        '' Task# A1-10-06-2015 Add Error message when customer not selectd
        If Me.cmbVendor.ActiveRow Is Nothing Then
            ShowErrorMessage("Please Select The Customer")
            Me.cmbVendor.Focus()
            Return False
        End If
        ''End Task# A1-10-06-2015

        'If Val(Me.cmbVendor.ActiveRow.Cells("CreditLimit").Value.ToString) > 0 AndAlso Val(Me.txtNetBill.Text) > 0 Then
        '    Dim strSQL As String = "Select coa.coa_detail_id, (isnull(a.Balance,0)+Isnull(so.Amount,0)) as Balance from vwCOADetail COA   LEFT OUTER JOIN (Select coa_detail_id, Isnull(SUM(isnull(debit_amount,0)-isnull(credit_amount,0)) ,0) as Balance From tblVoucherDetail WHERE coa_detail_id=" & Me.cmbVendor.Value & "  Group By coa_detail_id)  a on a.coa_detail_id = coa.coa_detail_id  LEFT OUTER JOIN (SELECT  SO_D.VendorId, IsNull(SUM((ISNULL(SO.Sz1, 0)-ISNULL(SO.DeliveredQty, 0)) * ISNULL(SO.Price, 0)),0) AS Amount  FROM  dbo.SalesOrderDetailTable AS SO INNER JOIN   dbo.SalesOrderMasterTable AS SO_D ON SO.SalesOrderId = SO_D.SalesOrderId WHERE (SO_D.Status = N'Open')  AND SO_D.VendorId=" & Me.cmbVendor.Value & "  Group By SO_D.VendorId) SO on SO.VendorId = coa.coa_detail_id WHERE coa.coa_detail_id=" & Me.cmbVendor.Value & ""
        '    Dim dt As New DataTable
        '    dt = GetDataTable(strSQL)
        '    dt.TableName = "Default"
        '    If dt IsNot Nothing Then
        '        If dt.Rows.Count > 0 AndAlso Val(dt.Rows(0).Item(0).ToString) > 0 Then
        '            If (Val(Me.txtNetBill.Text) + Val(dt.Rows(0).Item(1).ToString)) > Val(Me.cmbVendor.ActiveRow.Cells("CreditLimit").Value.ToString) Then
        '                ShowErrorMessage("Credit Limit Exceeded.")
        '                Me.cmbVendor.Focus()
        '                Return False
        '            End If
        '        End If
        '    End If

        'End If
        'End Task:2504
        If Val(Me.cmbVendor.ActiveRow.Cells("CreditLimit").Value.ToString) > 0 AndAlso Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)) > 0 Then
            Dim strSQL As String = "Select coa.coa_detail_id, (isnull(a.Balance,0)+Isnull(so.Amount,0)+" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)) & ") as Balance from vwCOADetail COA   LEFT OUTER JOIN (Select coa_detail_id, Isnull(SUM(isnull(debit_amount,0)-isnull(credit_amount,0)) ,0) as Balance From tblVoucherDetail WHERE coa_detail_id=" & Me.cmbVendor.Value & "  Group By coa_detail_id)  a on a.coa_detail_id = coa.coa_detail_id  LEFT OUTER JOIN (SELECT  SO_D.VendorId, IsNull(SUM((ISNULL(SO.Sz1, 0)-ISNULL(SO.DeliveredQty, 0)) * ISNULL(SO.Price, 0)),0) AS Amount  FROM  dbo.SalesOrderDetailTable AS SO INNER JOIN   dbo.SalesOrderMasterTable AS SO_D ON SO.SalesOrderId = SO_D.SalesOrderId WHERE  SO.SalesOrderId <> " & Val(Me.txtReceivingID.Text) & " AND (SO_D.Status = N'Open')  AND SO_D.VendorId=" & Me.cmbVendor.Value & "  Group By SO_D.VendorId) SO on SO.VendorId = coa.coa_detail_id WHERE coa.coa_detail_id=" & Me.cmbVendor.Value & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            dt.TableName = "Default"
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 AndAlso Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    If (Val(Me.txtNetBill.Text) + Val(dt.Rows(0).Item(1).ToString)) > Val(Me.cmbVendor.ActiveRow.Cells("CreditLimit").Value.ToString) Then
                        If msg_Confirm("Credit limit of this customer has been exceeded .Do you want to still save the sale order?") = False Then
                            Me.cmbVendor.Focus()
                            Return False
                        End If
                    End If
                End If
            End If
        End If
        Return True

    End Function

    Sub EditRecord()
        Try


            IsEditMode = True
            arrFile = New List(Of String)
            'Mode = "Edit"
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            'Me.FillCombo("SOComplete")
            Me.cmbCurrency.SelectedValue = BaseCurrencyId

            Me.cmbCompany.SelectedValue = grdSaved.GetRow.Cells("LocationId").Value
            txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value.ToString
            dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            txtReceivingID.Text = grdSaved.CurrentRow.Cells("SalesOrderId").Value
            'TODO. ----
            cmbVendor.Value = grdSaved.CurrentRow.Cells("VendorId").Value
            ''R933  Validate Customer Data 

            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Vendor is disable.")
                Exit Sub
            End If
            txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
            txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""
            Me.cmbProject.SelectedValue = Me.grdSaved.GetRow.Cells("CostCenterId").Value
            If IsDBNull(Me.grdSaved.GetRow.Cells("PO_Date").Value) Then
                Me.dtpPDate.Value = Now
                Me.dtpPDate.Checked = False
            Else
                Me.dtpPDate.Value = grdSaved.GetRow.Cells("PO_Date").Value
                Me.dtpPDate.Checked = True
            End If
            If Me.grdSaved.GetRow.Cells("Adj_Flag").Value = False Then
                Me.txtSpecialAdjustment.Text = grdSaved.CurrentRow.Cells("Adjustment").Value & ""
                Me.rbtAdjFlat.Checked = True
            Else
                Me.txtSpecialAdjustment.Text = grdSaved.CurrentRow.Cells("SpecialAdjustment").Value & ""
                Me.rbtAdjPercentage.Checked = True
            End If
            If IsDBNull(grdSaved.CurrentRow.Cells("SOP_ID")) Then
                Me.cmbSalesMan.SelectedValue = 0
            Else
                Me.cmbSalesMan.SelectedValue = grdSaved.CurrentRow.Cells("SOP_ID").Value
            End If
            'Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value
            '
            'Altered Against Task#2015060007 populating grid records
            Me.txtTerms_And_Condition.Text = Me.grdSaved.GetRow.Cells("Terms_And_Condition").Value.ToString
            ''TASK-SP22072016
            Me.txtSpecialInstructions.Text = Me.grdSaved.GetRow.Cells("SpecialInstructions").Value.ToString
            'Altered Against Task#2015060007 populating grid records
            Me.txtCustPONo.Text = Me.grdSaved.GetRow.Cells("PO No").Value.ToString

            'If Me.grdSaved.GetRow.Cells("OrderStatusID").Value.ToString.Length > 0 Then
            Me.cmbOrderStatus.SelectedValue = Val(Me.grdSaved.GetRow.Cells("SaleOrderStatusID").Value.ToString)
            'Me.cmbOrderType.SelectedValue = Val(Me.grdSaved.GetRow.Cells("SaleOrderTypeId").Value.ToString)
            'Else
            'Me.cmbOrderStatus.SelectedIndex = 0
            'End If
            Me.btnAttachment.Text = "Attachment (" & Me.grdSaved.GetRow.Cells("No Of Attachment").Value.ToString & ")"
            Dim dtQuot As New DataTable
            dtQuot = CType(Me.cmbQuotation.DataSource, DataTable)
            dtQuot.AcceptChanges()
            Dim drQuot() As DataRow
            drQuot = dtQuot.Select("QuotationId=" & Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString) & "")
            If drQuot.Length = 0 Then
                Dim drq As DataRow
                drq = dtQuot.NewRow
                drq(0) = Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString)
                drq(1) = Me.grdSaved.GetRow.Cells("QuotationNo").Value.ToString
                drq(2) = Val(Me.grdSaved.GetRow.Cells("LocationId").Value.ToString)
                drq(3) = Val(Me.grdSaved.GetRow.Cells("VendorId").Value.ToString)
                dtQuot.Rows.Add(drq)
                dtQuot.AcceptChanges()
            End If
            Me.cmbQuotation.SelectedValue = Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString)


            Me.txtTechnicalDrawingNo.Text = Val(Me.grdSaved.GetRow.Cells("TechnicalDrawingNumber").Value.ToString)
            If IsDBNull(Me.grdSaved.GetRow.Cells("TechnicalDrawingDate").Value) Then
                Me.dtpTechnicalDrawingDate.Value = Date.Now
                Me.dtpTechnicalDrawingDate.Checked = False
            Else
                Me.dtpTechnicalDrawingDate.Value = Me.grdSaved.GetRow.Cells("TechnicalDrawingDate").Value
                Me.dtpTechnicalDrawingDate.Checked = True
            End If

            Me.txtAccountsRemarks.Text = Me.grdSaved.GetRow.Cells("AccountsRemarks").Value.ToString
            Me.txtStoreRemarks.Text = Me.grdSaved.GetRow.Cells("StoreRemarks").Value.ToString
            Me.txtProductionRemarks.Text = Me.grdSaved.GetRow.Cells("ProductionRemarks").Value.ToString
            Me.txtServicesRemarks.Text = Me.grdSaved.GetRow.Cells("ServicesRemarks").Value.ToString
            Me.txtSalesRemarks.Text = Me.grdSaved.GetRow.Cells("SalesRemarks").Value.ToString
            Me.cmbTermOfPayments.Text = Me.grdSaved.GetRow.Cells("TermOfPayments").Value.ToString
            '        TechnicalDrawingNumber	int	Checked
            'TechnicalDrawingDate	datetime	Checked
            'AccountsRemarks	nvarchar(300)	Checked
            'StoreRemarks	nvarchar(300)	Checked
            'ProductionRemarks	nvarchar(300)	Checked
            'ServicesRemarks	nvarchar(300)	Checked
            'SalesRemarks	nvarchar(300)	Checked
            'If Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString) > 0 Then
            '    Call DisplayDetail(grdSaved.CurrentRow.Cells("QuotationId").Value)
            'Else
            Dim RevisionNumber As Integer = Me.CheckRevisionNumber(grdSaved.CurrentRow.Cells("SalesOrderId").Value)
            If RevisionNumber > 0 Then
                IsRevisionRestrictedFirstTime = True
                Me.FillRevisionCombo(grdSaved.CurrentRow.Cells("SalesOrderId").Value)
                Me.cmbRevisionNumber.Visible = False
                Me.lblRev.Visible = False
                Me.txtPONo.Size = New System.Drawing.Size(95, 21)
                Me.lnkLblRevisions.Visible = True
                Me.lnkLblRevisions.Text = "Rev (" & RevisionNumber & ")"
            Else
                Me.cmbRevisionNumber.Visible = False
                Me.lblRev.Visible = False
                Me.lnkLblRevisions.Visible = False
                Me.txtPONo.Size = New System.Drawing.Size(177, 21)
            End If
            Call DisplayDetail(grdSaved.CurrentRow.Cells("SalesOrderId").Value)
            ' End If
            If IsSalesOrderAnalysis = False Then
                Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.Total), Janus.Windows.GridEX.AggregateFunction.Sum)
            Else
                Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.BillValueAfterDiscount), Janus.Windows.GridEX.AggregateFunction.Sum)
            End If
            If IsDBNull(grdSaved.CurrentRow.Cells("Delivery_Date").Value) Then
                Me.dtpDeliveryDate.Value = Date.Now
                Me.dtpDeliveryDate.Checked = False
            Else
                dtpDeliveryDate.Value = grdSaved.CurrentRow.Cells("Delivery_Date").Value
                Me.dtpDeliveryDate.Checked = True
            End If
            GetTotal()
            Me.BtnSave.Text = "&Update"
            Me.btnSave1.Text = "&Update"
            Me.cmbPo.Enabled = True
            GetSecurityRights()
            Me.chkPost.Checked = Me.grdSaved.GetRow.Cells("Posted").Value
            Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
            Me.cmbCompany.Enabled = False
            Me.lblPrintStatus.Text = "Print Status: " & Me.grdSaved.GetRow.Cells("Print Status").Text.ToString

            If flgDateLock = True Then
                If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dtpPODate.Enabled = False
                Else
                    Me.dtpPODate.Enabled = True
                End If
            Else
                Me.dtpPODate.Enabled = True
            End If
            Dim intCountAttachedFiles As Integer = 0I
            'Altered Against Task# 2015060001 Ali Ansari
            'Get no of attached files
            If Me.BtnSave.Text <> "&Save" Then
                If Me.grdSaved.RowCount > 0 Then
                    intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                    Me.btnAttachment.Text = "Attachment (" & intCountAttachedFiles & ")"
                End If
            End If
            'Altered Against Task# 2015060001 Ali Ansari
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnDelete.Visible = True
            Me.BtnPrint.Visible = True
            ''''''''''''''''''''''''''
            'GetSecurityRights()
            ''TASK-481 on 13-07-2016


            ''Abubakar Siddiq :TFS3113 :Making Approval Button Enable in Edit Mode
            Me.btnApprovalHistory.Visible = True
            Me.btnApprovalHistory.Enabled = True
            ''Abubakar Siddiq :TFS3113 :End


            ''TASK-DS22072016
            DisplayItemDeliveryDetail(-1)
            DisplayItemDeliveryDetail(Me.grdSaved.GetRow.Cells("SalesOrderId").Value)
            FillCombo("ItemDeliverySchedule")
            Me.UltraTabPageControl4.Enabled = True
            'Me.cmbDispatchTo.Value = Val(Me.grdSaved.GetRow.Cells("DispatchToLocation").Value.ToString)
            'Me.cmbInvoiceTo.Value = Val(Me.grdSaved.GetRow.Cells("InvoiceToLocation").Value.ToString)
            ''End TASK-481

            ''TASK TFS1764
            Me.cmbTransporter.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("TransporterId").Value.ToString)
            '' END TASK TFS1764
            FillOutwardExpense(grdSaved.CurrentRow.Cells("SalesOrderId").Value, "SO")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DisplayPODetail(ByVal ReceivingID As Integer)
        Try
            'Dim str As String
            'Dim i As Integer

            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.Price as CurrentPrice,Isnull(TradePrice,0) as TradePrice, Isnull(SalesTax_Percentage,0) as SalesTax_Percentage, ISNULL(SchemeQty,0) as SchemeQty, ISNULL(Discount_Percentage,0) as Discount_Percentage, ISNULL(Freight,0) as Freight, ISNULL(MarketReturns,0) as MarketReturns FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '      & " Where Recv_D.SalesID =" & ReceivingID & ""

            'Dim objCommand As New OleDbCommand
            'Dim objCon As OleDbConnection
            'Dim objDataAdapter As New OleDbDataAdapter
            'Dim objDataSet As New DataSet

            'objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

            'If objCon.State = ConnectionState.Open Then objCon.Close()

            'objCon.Open()
            'objCommand.Connection = objCon
            'objCommand.CommandType = CommandType.Text


            'objCommand.CommandText = str

            'objDataAdapter.SelectCommand = objCommand
            'objDataAdapter.Fill(objDataSet)

            ''grd.Rows.Clear()
            'grd.Rows.Clear()
            'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

            '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9))

            '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

            'Next
            'Dim dtDisplayDetail As New DataTable
            'dtDisplayDetail = GetDataTable(str)
            'dtDisplayDetail.Columns.Add(GrdEnum.BillValueAfterDiscount.ToString, GetType(System.Double))
            'dtDisplayDetail.Columns(GrdEnum.BillValueAfterDiscount).Expression = "(((((Qty * TradePrice) * SalesTax_Percentage)/100) + (((SchemeQty*TradePrice)*SalesTax_Percentage)/100)) + (Freight*(Qty+SchemeQty))) - (((Qty*TradePrice)*Discount_Percentage)/100)"
            'Me.grd.DataSource = Nothing
            'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',((PackQty*Qty)*Price), Qty*Price)"
            'Me.grd.DataSource = dtDisplayDetail
            'ApplyGridSetting()
            'FillCombo("grdLocation")
            'CtrlGrdBar1_Load(Nothing, Nothing)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DisplayDetail(ByVal ReceivingID As Integer, Optional ByVal TypeId As Integer = Nothing, Optional ByVal Condition As String = "")
        Try

            Dim str As String = String.Empty
            'Dim i As Integer
            If Condition = "All" Then
                If getConfigValueByType("LoadAllItemsInSales").ToString = "True" Then
                    'str = " SELECT  Detail.LocationId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription as Item,  " _
                    '   & " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Price, Isnull(Detail.Total,0) as Total,  " _
                    '   & " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [PackQty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as SalesTax_Percentage, ISNULL(Detail.[SchemeQty],0) as [SchemeQty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc  " _
                    '   & " From ArticleDefView  LEFT OUTER JOIN (   " _
                    '   & " SELECT Recv_D.LocationId, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate,   " _
                    '   & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price)) END AS Total,   " _
                    '   & " Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.SalesOrderDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.SalesTax_Percentage,0) as Tax, 0 as savedqty , SchemeQty as [SchemeQty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc  FROM SalesOrderDetailTable Recv_D   " _
                    '   & " Where(Recv_D.SalesOrderID = " & ReceivingID & ")  " _
                    '   & " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId  " _
                    '   & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts " _
                    '   & " WHERE(TypeId = " & IIf(TypeId > 0, TypeId, -1) & "))  CustomerDiscount  On CustomerDiscount.articledefid=articledefview.articleId WHERE ArticleDefView.SalesItem=1 " & IIf(IsEditMode = False, " And ArticleDefView.Active=1", "") & " ORDER By ArticleDefView.SortOrder Asc"
                    'Before against task:2374
                    ''R-916 Added Column Comments
                    'str = " SELECT  Detail.LocationId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription as Item,  " _
                    '  & " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Price, Isnull(Detail.Total,0) as Total,  " _
                    '  & " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [PackQty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as SalesTax_Percentage, ISNULL(Detail.[SchemeQty],0) as [SchemeQty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Detail.Comments  " _
                    '  & " From ArticleDefView  LEFT OUTER JOIN (   " _
                    '  & " SELECT Recv_D.LocationId, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate,   " _
                    '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price)) END AS Total,   " _
                    '  & " Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.SalesOrderDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.SalesTax_Percentage,0) as Tax, 0 as savedqty , SchemeQty as [SchemeQty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments  FROM SalesOrderDetailTable Recv_D   " _
                    '  & " Where(Recv_D.SalesOrderID = " & ReceivingID & ")  " _
                    '  & " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId  " _
                    '  & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts " _
                    '  & " WHERE(TypeId = " & IIf(TypeId > 0, TypeId, -1) & "))  CustomerDiscount  On CustomerDiscount.articledefid=articledefview.articleId WHERE ArticleDefView.SalesItem=1 " & IIf(IsEditMode = False, " And ArticleDefView.Active=1", "") & " ORDER By ArticleDefView.SortOrder Asc"
                    ''End R-916
                    'Task:2374 Added Column Total Amount, Tax Amount 
                    'Before against task:M16
                    'str = " SELECT  Detail.LocationId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription as Item,  " _
                    ' & " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Price, Isnull(Detail.Total,0) as Total,  " _
                    ' & " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [PackQty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float, 0) as [Total Amount], ISNULL(Detail.[SchemeQty],0) as [SchemeQty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Detail.Comments  " _
                    ' & " From ArticleDefView  LEFT OUTER JOIN (   " _
                    ' & " SELECT Recv_D.LocationId, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate,   " _
                    ' & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price)) END AS Total,   " _
                    ' & " Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.SalesOrderDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.SalesTax_Percentage,0) as Tax, 0 as savedqty , SchemeQty as [SchemeQty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments  FROM SalesOrderDetailTable Recv_D   " _
                    ' & " Where(Recv_D.SalesOrderID = " & ReceivingID & ")  " _
                    ' & " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId  " _
                    ' & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts " _
                    ' & " WHERE(TypeId = " & IIf(TypeId > 0, TypeId, -1) & "))  CustomerDiscount  On CustomerDiscount.articledefid=articledefview.articleId WHERE ArticleDefView.SalesItem=1 " & IIf(IsEditMode = False, " And ArticleDefView.Active=1", "") & " ORDER By ArticleDefView.SortOrder Asc"
                    'End Task:2374
                    'Before against task:2801
                    'Task:M16 Added Column Engine_No And Chassis_No
                    'str = " SELECT  Detail.LocationId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription as Item,  " _
                    '& " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Price, Isnull(Detail.Total,0) as Total,  " _
                    '& " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [PackQty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float, 0) as [Total Amount], ISNULL(Detail.[SchemeQty],0) as [SchemeQty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Detail.Comments,Detail.Engine_No, Detail.Chassis_No  " _
                    '& " From ArticleDefView  LEFT OUTER JOIN (   " _
                    '& " SELECT Recv_D.LocationId, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate,   " _
                    '& " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price)) END AS Total,   " _
                    '& " Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.SalesOrderDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.SalesTax_Percentage,0) as Tax, 0 as savedqty , SchemeQty as [SchemeQty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No  FROM SalesOrderDetailTable Recv_D   " _
                    '& " Where(Recv_D.SalesOrderID = " & ReceivingID & ")  " _
                    '& " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId  " _
                    '& " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts " _
                    '& " WHERE(TypeId = " & IIf(TypeId > 0, TypeId, -1) & "))  CustomerDiscount  On CustomerDiscount.articledefid=articledefview.articleId WHERE ArticleDefView.SalesItem=1 " & IIf(IsEditMode = False, " And ArticleDefView.Active=1", "") & " ORDER By ArticleDefView.SortOrder Asc"
                    'End Task:M16
                    'Task:2801 Added Field DeliveredQty
                    'Before against task:2832
                    'str = " SELECT  Detail.LocationId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription as Item,  " _
                    '& " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Price, Isnull(Detail.Total,0) as Total,  " _
                    '& " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [PackQty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float, 0) as [Total Amount], ISNULL(Detail.[SchemeQty],0) as [SchemeQty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Detail.Comments,Detail.Engine_No, Detail.Chassis_No, IsNull(Detail.DeliveredQty,0) as DeliveredQty  " _
                    '& " From ArticleDefView  LEFT OUTER JOIN (   " _
                    '& " SELECT Recv_D.LocationId, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate,   " _
                    '& " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price)) END AS Total,   " _
                    '& " Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.SalesOrderDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.SalesTax_Percentage,0) as Tax, 0 as savedqty , SchemeQty as [SchemeQty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty  FROM SalesOrderDetailTable Recv_D   " _
                    '& " Where(Recv_D.SalesOrderID = " & ReceivingID & ")  " _
                    '& " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId  " _
                    '& " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts " _
                    '& " WHERE(TypeId = " & IIf(TypeId > 0, TypeId, -1) & "))  CustomerDiscount  On CustomerDiscount.articledefid=articledefview.articleId WHERE ArticleDefView.SalesItem=1 " & IIf(IsEditMode = False, " And ArticleDefView.Active=1", "") & " ORDER By ArticleDefView.SortOrder Asc"
                    'End Task:2801
                    'Task:2832 Added PlanedQty Field
                    '  str = " SELECT  Detail.LocationId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription as Item,  " _
                    '& " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Price, Isnull(Detail.Total,0) as Total,  " _
                    '& " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [PackQty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float, 0) as [Total Amount], ISNULL(Detail.[SchemeQty],0) as [SchemeQty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Detail.Comments,Detail.Engine_No, Detail.Chassis_No, IsNull(Detail.DeliveredQty,0) as DeliveredQty, IsNull(Detail.PlanedQty,0) as PlanedQty  " _
                    '& " From ArticleDefView  LEFT OUTER JOIN (   " _
                    '& " SELECT Recv_D.LocationId, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate,   " _
                    '& " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price)) END AS Total,   " _
                    '& " Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.SalesOrderDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.SalesTax_Percentage,0) as Tax, 0 as savedqty , SchemeQty as [SchemeQty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty,IsNull(Recv_D.PlanedQty,0) as PlanedQty  FROM SalesOrderDetailTable Recv_D   " _
                    '& " Where(Recv_D.SalesOrderID = " & ReceivingID & ")  " _
                    '& " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId  " _
                    '& " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts " _
                    '& " WHERE(TypeId = " & IIf(TypeId > 0, TypeId, -1) & "))  CustomerDiscount  On CustomerDiscount.articledefid=articledefview.articleId WHERE ArticleDefView.SalesItem=1 " & IIf(IsEditMode = False, " And ArticleDefView.Active=1", "") & " ORDER By ArticleDefView.SortOrder Asc"
                    'End Task:2832

                    '        str = " SELECT  Detail.LocationId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription as Item,  " _
                    '& " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Price, Isnull(Detail.Total,0) as Total,  " _
                    '& " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [PackQty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as SED_Tax_Percent, Convert(float,0) as SED_Tax_Amount, Convert(float, 0) as [Total Amount], ISNULL(Detail.[SchemeQty],0) as [SchemeQty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Detail.Comments,Detail.[Other Comments],Detail.Engine_No, Detail.Chassis_No,0 as BillValueAfterDiscount, IsNull(Detail.DeliveredQty,0) as DeliveredQty, IsNull(Detail.PlanedQty,0) as PlanedQty, IsNull(ArticleDefView.Cost_Price,0) as CostPrice, 0 as SalesOrderDetailId, 0 As QuotationDetailId, 0 As QuotationId, 0 As SOQuantity  " _
                    '& " From ArticleDefView  LEFT OUTER JOIN (   " _
                    '& " SELECT Recv_D.LocationId, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate,   " _
                    '& " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price)) END AS Total,   " _
                    '& " Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.SalesOrderDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.SalesTax_Percentage,0) as Tax, 0 as savedqty , SchemeQty as [SchemeQty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty,IsNull(Recv_D.PlanedQty,0) as PlanedQty,Recv_D.Other_Comments as [Other Comments]  FROM SalesOrderDetailTable Recv_D   " _
                    '& " Where(Recv_D.SalesOrderID = " & ReceivingID & ")  " _
                    '& " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId  " _
                    '& " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts " _
                    '& " WHERE(TypeId = " & IIf(TypeId > 0, TypeId, -1) & "))  CustomerDiscount  On CustomerDiscount.articledefid=articledefview.articleId WHERE ArticleDefView.SalesItem=1 " & IIf(IsEditMode = False, " And ArticleDefView.Active=1", "") & " ORDER By ArticleDefView.SortOrder Asc"
                    'Ali Faisal : TFS1634 : Altered to set the zero values of columns TotalQty and Currency columns if have null values
                    ''TFS4343 : Edit Query to get Article Size and color
                    str = " SELECT  Detail.LocationId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription as Item, '' as ArticleAliasName, ArticleDefView.ArticleSizeName as Size, ArticleDefView.ArticleColorName as Color, " _
    & " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty,((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as PostDiscountPrice, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Price, IsNull(Detail.BaseCurrencyId,0) BaseCurrencyId, IsNull(Detail.BaseCurrencyRate,0) BaseCurrencyRate, IsNull(Detail.CurrencyId,0) CurrencyId, IsNull(Detail.CurrencyRate,0) CurrencyRate, IsNull(Detail.CurrencyAmount,0) CurrencyAmount,  Convert(float,0) as TotalCurrencyAmount, Isnull(Detail.DiscountId,1) as DiscountId , IsNull(Detail.DiscountFactor, 0) AS DiscountFactor, IsNull(Detail.DiscountValue, 0) As DiscountValue , Isnull(Detail.Total,0) as Total,  " _
    & " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [PackQty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as CurrencyTaxAmount, Convert(float,0) as SED_Tax_Percent, Convert(float,0) as SED_Tax_Amount, Convert(float,0) as CurrencySEDAmount, Convert(float, 0) as [Total Amount], ISNULL(Detail.[SchemeQty],0) as [SchemeQty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Detail.Comments,Detail.[Other Comments],Detail.Engine_No, Detail.Chassis_No,0 as BillValueAfterDiscount, IsNull(Detail.DeliveredQty,0) as DeliveredQty, IsNull(Detail.PlanedQty,0) as PlanedQty, IsNull(ArticleDefView.Cost_Price,0) as CostPrice, IsNull(Detail.SalesOrderDetailId, 0) As SalesOrderDetailId, 0 As QuotationDetailId, 0 As QuotationId, 0 As SOQuantity, '' as SaleOrderType, IsNull(Detail.TotalQuantity,0) TotalQuantity , ISNULL(Detail.[BatchNo], 'xxxx') as [BatchNo], IsNull(Detail.ExpiryDate,DATEADD(Month , 1 , getDate())) as ExpiryDate " _
    & " From ArticleDefView  LEFT OUTER JOIN (   " _
    & " SELECT Recv_D.LocationId, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, Recv_D.CurrencyAmount,   " _
    & "  Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue ,(IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total,   " _
    & " Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.SalesOrderDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.SalesTax_Percentage,0) as Tax, 0 as savedqty , SchemeQty as [SchemeQty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty,IsNull(Recv_D.PlanedQty,0) as PlanedQty,Recv_D.Other_Comments as [Other Comments], IsNull(Recv_D.Qty, 0) As TotalQuantity,Recv_D.ExpiryDate,Recv_D.BatchNo FROM SalesOrderDetailTable Recv_D   " _
    & " Where(Recv_D.SalesOrderID = " & ReceivingID & ")  " _
    & " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId  " _
    & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts " _
    & " WHERE(TypeId = " & IIf(TypeId > 0, TypeId, -1) & "))  CustomerDiscount  On CustomerDiscount.articledefid=articledefview.articleId WHERE ArticleDefView.SalesItem=1 " & IIf(IsEditMode = False, " And ArticleDefView.Active=1", "") & " ORDER By ArticleDefView.SortOrder Asc"
                    'Ali Faisal : TFS1634 : End
                End If
            ElseIf Condition = "Quotation" Then
                ' str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, 0 as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, 0 as Freight, 0 as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, '' as Engine_No, '' as Chassis_No, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, 0 as PlanedQty  FROM dbo.QuotationDetailTable Recv_D INNER JOIN " _
                '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '& " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY Article.SortOrder Asc"

                '   str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, 0 as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, 0 as Freight, 0 as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, '' as Engine_No, '' as Chassis_No, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, 0 as PlanedQty,'' as [Other Comments]  FROM dbo.QuotationDetailTable Recv_D INNER JOIN " _
                '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '& " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY Article.SortOrder Asc"
                '          str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, 0 as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], IsNull(Recv_D.SED_Tax_Percent,0) as SED_Tax_Percent, IsNull(Recv_D.SED_Tax_Amount,0) as SED_Tax_Amount, Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, 0 as Freight, 0 as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments,'' as [Other Comments],   '' as Engine_No, '' as Chassis_No, 0 as BillValueAfterDiscount,IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, 0 as PlanedQty, IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as SalesOrderDetailId, IsNull(Recv_D.QuotationDetailId, 0) As QuotationDetailId, IsNull(Recv_D.QuotationId, 0) As QuotationId, IsNull(Recv_D.SOQuantity, 0) As SOQuantity  FROM dbo.QuotationDetailTable Recv_D INNER JOIN " _
                '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '& " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY Article.SortOrder Asc"

                str = "SELECT Recv_D.SerialNo, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.RequirementDescription as ArticleAliasName,ArticleSizeDefTable.ArticleSizeName as Size, ArticleColorDefTable.ArticleColorName as Color, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.SOQuantity,0)) AS Qty, Recv_D.PostDiscountPrice, Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) AS CurrencyAmount, Convert(float,0) as TotalCurrencyAmount, Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue , " _
               & " (IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total,  " _
               & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, 0 as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], IsNull(Recv_D.SED_Tax_Percent,0) as SED_Tax_Percent, IsNull(Recv_D.SED_Tax_Amount,0) as SED_Tax_Amount, Convert(float,0) as CurrencyTaxAmount, Convert(float,0) as CurrencySEDAmount, Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, 0 as Freight, 0 as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments,'' as [Other Comments],   '' as Engine_No, '' as Chassis_No, 0 as BillValueAfterDiscount,IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, 0 as PlanedQty, IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as SalesOrderDetailId, IsNull(Recv_D.QuotationDetailId, 0) As QuotationDetailId, IsNull(Recv_D.QuotationId, 0) As QuotationId, IsNull(Recv_D.SOQuantity, 0) As SOQuantity, '' as SaleOrderType, IsNull(Recv_D.Qty, 0)-IsNull(Recv_D.DeliveredTotalQty, 0)  As TotalQuantity , '' As BatchNo ,  DATEADD(Month , 1 , getDate()) as ExpiryDate FROM dbo.QuotationDetailTable Recv_D LEFT OUTER JOIN " _
               & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
               & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
               & " Left Outer Join ArticleColorDefTable On Article.ArticleColorId = ArticleColorDefTable.ArticleColorId Left Outer Join ArticleSizeDefTable on Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
               & " Where Recv_D.QuotationId =" & ReceivingID & " AND (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.SOQuantity,0)) > 0 ORDER BY Article.SortOrder Asc"
            ElseIf Condition = "QuotationUpdate" Then
                ' str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, 0 as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, 0 as Freight, 0 as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, '' as Engine_No, '' as Chassis_No, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, 0 as PlanedQty  FROM dbo.QuotationDetailTable Recv_D INNER JOIN " _
                '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '& " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY Article.SortOrder Asc"

                '   str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, 0 as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, 0 as Freight, 0 as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, '' as Engine_No, '' as Chassis_No, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, 0 as PlanedQty,'' as [Other Comments]  FROM dbo.QuotationDetailTable Recv_D INNER JOIN " _
                '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '& " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY Article.SortOrder Asc"
                '          str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, IsNull(Recv_D.SOQuantity, 0) AS Qty, Recv_D.Price,  " _
                '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, 0 as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount],IsNull(Recv_D.SED_Tax_Percent,0) as SED_Tax_Percent,IsNull(Recv_D.SED_Tax_Amount,0) as SED_Tax_Amount, Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, 0 as Freight, 0 as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments,'' as [Other Comments],   '' as Engine_No, '' as Chassis_No, 0 as BillValueAfterDiscount,IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, 0 as PlanedQty, IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as SalesOrderDetailId, IsNull(Recv_D.QuotationDetailId, 0) As QuotationDetailId, IsNull(Recv_D.QuotationId, 0) As QuotationId, IsNull(Recv_D.SOQuantity, 0) As SOQuantity  FROM dbo.QuotationDetailTable Recv_D INNER JOIN " _
                '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '& " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY Article.SortOrder Asc"
                '                str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, '' as ArticleAliasName,Recv_D.ArticleSize AS unit, IsNull(Recv_D.SOQuantity, 0) AS Qty, Recv_D.Price,  " _
                '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, 0 as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount],IsNull(Recv_D.SED_Tax_Percent,0) as SED_Tax_Percent,IsNull(Recv_D.SED_Tax_Amount,0) as SED_Tax_Amount, Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, 0 as Freight, 0 as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments,'' as [Other Comments],   '' as Engine_No, '' as Chassis_No, 0 as BillValueAfterDiscount,IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, 0 as PlanedQty, IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as SalesOrderDetailId, IsNull(Recv_D.QuotationDetailId, 0) As QuotationDetailId, IsNull(Recv_D.QuotationId, 0) As QuotationId, IsNull(Recv_D.SOQuantity, 0) As SOQuantity, '' as SaleOrderType  FROM dbo.QuotationDetailTable Recv_D INNER JOIN " _
                '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '& " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY Article.SortOrder Asc"
            Else
                'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, Isnull(recv_d.TradePrice,0) as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, ISNULL(recv_d.Freight,0) as Freight, ISNULL(Recv_d.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc  FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
                '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '      & " Where Recv_D.SalesOrderID =" & ReceivingID & " ORDER BY Article.SortOrder Asc"
                'Before against task:2374
                ''R-916 Added Column Comments
                'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, Isnull(recv_d.TradePrice,0) as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, ISNULL(recv_d.Freight,0) as Freight, ISNULL(Recv_d.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments  FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
                '    & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '    & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '    & " Where Recv_D.SalesOrderID =" & ReceivingID & " ORDER BY Article.SortOrder Asc"
                ''End R-916
                ''Task:2374 Added Column Total Amount 
                'Before against task:M16
                'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '  & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, Isnull(recv_d.TradePrice,0) as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, ISNULL(recv_d.Freight,0) as Freight, ISNULL(Recv_d.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments  FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
                '  & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '  & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '  & " Where Recv_D.SalesOrderID =" & ReceivingID & " ORDER BY Article.SortOrder Asc"
                'End Task:2374

                'Task:M16 Added Column Engine_No And Chassis_No.
                'Before against task:2801
                'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '  & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, Isnull(recv_d.TradePrice,0) as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, ISNULL(recv_d.Freight,0) as Freight, ISNULL(Recv_d.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No  FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
                '  & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '  & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '  & " Where Recv_D.SalesOrderID =" & ReceivingID & " ORDER BY Article.SortOrder Asc"
                'End Task:M16
                'Task:2801 Added Field DeliveredQty
                'Before against task:2832
                'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '  & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, Isnull(recv_d.TradePrice,0) as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, ISNULL(recv_d.Freight,0) as Freight, ISNULL(Recv_d.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty  FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
                '  & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '  & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '  & " Where Recv_D.SalesOrderID =" & ReceivingID & " ORDER BY Article.SortOrder Asc"
                'End Task:2801.
                'Task:2832 Added PlanedQty Field
                'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, Isnull(recv_d.TradePrice,0) as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, ISNULL(recv_d.Freight,0) as Freight, ISNULL(Recv_d.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, IsNull(Recv_D.PlanedQty,0) as PlanedQty  FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
                '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '& " Where Recv_D.SalesOrderID =" & ReceivingID & " ORDER BY Article.SortOrder Asc"
                'End Task:2832

                '   str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, Isnull(recv_d.TradePrice,0) as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, ISNULL(recv_d.Freight,0) as Freight, ISNULL(Recv_d.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, IsNull(Recv_D.PlanedQty,0) as PlanedQty,Recv_D.Other_Comments as [Other Comments]  FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
                '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '& " Where Recv_D.SalesOrderID =" & ReceivingID & " ORDER BY Article.SortOrder Asc"

                'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                '     & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                '     & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, Isnull(recv_d.TradePrice,0) as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], IsNull(Recv_D.SED_Tax_Percent,0) as SED_Tax_Percent, IsNull(Recv_D.SED_Tax_Amount,0) as SED_Tax_Amount, Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, ISNULL(recv_d.Freight,0) as Freight, ISNULL(Recv_d.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Other_Comments as [Other Comments],Recv_D.Engine_No, Recv_D.Chassis_No, 0 as BillValueAfterDiscount, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, IsNull(Recv_D.PlanedQty,0) as PlanedQty, IsNull(Recv_D.CostPrice,0) as CostPrice, IsNull(Recv_D.SalesOrderDetailId,0) as SalesOrderDetailId, 0 As QuotationDetailId, 0 As QuotationId, 0 As SOQuantity  FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
                '     & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                '     & " Where Recv_D.SalesOrderID =" & ReceivingID & " ORDER BY Article.SortOrder Asc"


                str = "SELECT Recv_D.SerialNo, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleAliasName,ArticleSizeDefTable.ArticleSizeName as Size, ArticleColorDefTable.ArticleColorName as Color, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.PostDiscountPrice, Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float,0) as TotalCurrencyAmount, Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue ,  " _
                     & " (IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
                     & " Article.ArticleGroupId, Recv_D.ArticleDefId, Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, Isnull(recv_d.TradePrice,0) as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as CurrencyTaxAmount, IsNull(Recv_D.SED_Tax_Percent,0) as SED_Tax_Percent, IsNull(Recv_D.SED_Tax_Amount,0) as SED_Tax_Amount, Convert(float,0) as CurrencySEDAmount, Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, ISNULL(recv_d.Freight,0) as Freight, ISNULL(Recv_d.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Other_Comments as [Other Comments],Recv_D.Engine_No, Recv_D.Chassis_No, 0 as BillValueAfterDiscount, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, IsNull(Recv_D.PlanedQty,0) as PlanedQty, IsNull(Recv_D.CostPrice,0) as CostPrice, IsNull(Recv_D.SalesOrderDetailId,0) as SalesOrderDetailId, IsNull(Recv_D.QuotationDetailId,0) As QuotationDetailId, 0 As QuotationId, 0 As SOQuantity, Recv_D.SaleOrderType, IsNull(Recv_D.Qty, 0) As TotalQuantity , IsNull(Recv_D.BatchNo,'xxxx') As BatchNo , IsNull(Recv_D.ExpiryDate,DATEADD(Month , 1 , getDate())) as ExpiryDate FROM dbo.SalesOrderDetailTable Recv_D LEFT OUTER JOIN " _
                     & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                     & " Left Outer Join ArticleColorDefTable On Article.ArticleColorId = ArticleColorDefTable.ArticleColorId Left Outer Join ArticleSizeDefTable on Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
                     & " Where Recv_D.SalesOrderID =" & ReceivingID & " ORDER BY Article.SortOrder Asc"

            End If
            FillOutwardExpense(-1, "SO")
            'Dim objCommand As New OleDbCommand
            'Dim objCon As OleDbConnection
            'Dim objDataAdapter As New OleDbDataAdapter
            'Dim objDataSet As New DataSet

            'objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

            'If objCon.State = ConnectionState.Open Then objCon.Close()

            'objCon.Open()
            'objCommand.Connection = objCon
            'objCommand.CommandType = CommandType.Text


            'objCommand.CommandText = str

            'objDataAdapter.SelectCommand = objCommand
            'objDataAdapter.Fill(objDataSet)

            'grd.Rows.Clear()
            'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

            '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9))

            '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

            'Next
            Dim dtDisplayDetail As New DataTable
            dtDisplayDetail = GetDataTable(str)
            'dtDisplayDetail.Columns.Add(GrdEnum.BillValueAfterDiscount.ToString, GetType(System.Double))
            If IsSalesOrderAnalysis = True Then
                dtDisplayDetail.Columns(GrdEnum.BillValueAfterDiscount).Expression = "IIF(Unit='Loose',((((((Qty * CurrentPrice) * SalesTax_Percentage)/100) + (((SchemeQty*CurrentPrice)*SalesTax_Percentage)/100)) + (Freight*(Qty+SchemeQty))) - (((Qty*CurrentPrice)*Discount_Percentage)/100)  +  (Qty * CurrentPrice)), (((((((Qty * PackQty) * CurrentPrice) * SalesTax_Percentage)/100) + (((SchemeQty * CurrentPrice) * SalesTax_Percentage)/100)) + (Freight * ((Qty * PackQty)+SchemeQty))) - ((((Qty * PackQty) * CurrentPrice) * Discount_Percentage)/100)  +  ((Qty * PackQty)* CurrentPrice)))"
                'dtDisplayDetail.Columns(GrdEnum.Rate).Expression = "CurrentPrice-MarketReturns-IIF(Discount_Percentage=0,0,((CurrentPrice*Discount_Percentage)/100))"
            End If

            'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',((PackQty*Qty)*Price), Qty*Price) + IIF(Unit='Loose', (((SchemeQty * CurrentPrice) * SalesTax_Percentage)/100), ((((SchemeQty * PackQty) *  CurrentPrice) * SalesTax_Percentage)/100))"
            'TASK-408
            'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',(([PackQty]*[Qty])*[Price]), [Qty]*[Price])"
            dtDisplayDetail.Columns("DiscountValue").Expression = "IIF(DiscountId= 1,((IsNull(PostDiscountPrice,0)*IsNull(DiscountFactor,0))/100), IsNull(DiscountFactor,0))" ''TFS2827
            dtDisplayDetail.Columns("Price").Expression = "IIF(DiscountId= 1, IsNull(PostDiscountPrice,0)-((IsNull(PostDiscountPrice,0)*IsNull(DiscountFactor,0))/100), IsNull(PostDiscountPrice,0)-IsNull(DiscountFactor,0))"
            dtDisplayDetail.Columns("Total").Expression = " [TotalQuantity]*[Price]*CurrencyRate"
            dtDisplayDetail.Columns("CurrencyAmount").Expression = " [TotalQuantity]*[Price]"
            dtDisplayDetail.Columns("SED_Tax_Amount").Expression = "((IsNull([SED_Tax_Percent],0)/100)*IsNull([Total],0))"
            dtDisplayDetail.Columns("CurrencySEDAmount").Expression = "((IsNull([SED_Tax_Percent],0)/100)*IsNull([CurrencyAmount],0))"
            ''Start TFS3758 : Ayesha Rehman : 05-07-2018
            If flgExcludeTaxPrice = False Then
                dtDisplayDetail.Columns("CurrencyTaxAmount").Expression = "((IsNull([SalesTax_Percentage],0)/100)*CurrencyAmount)" 'Task:2374 Show Tax Amount
                dtDisplayDetail.Columns("Tax Amount").Expression = "((IsNull([SalesTax_Percentage],0)/100)*Total)" 'Task:2374 Show Tax Amount
                dtDisplayDetail.Columns("Total Amount").Expression = "(IsNull([Total],0) + IsNull([Tax Amount],0)+IsNull([SED_Tax_Amount],0))" 'Task:2374 Show Total Amount
                dtDisplayDetail.Columns("TotalCurrencyAmount").Expression = "(IsNull([CurrencyAmount],0) + IsNull([CurrencyTaxAmount],0)+IsNull([CurrencySEDAmount],0))"
            Else
                dtDisplayDetail.Columns("CurrencyTaxAmount").Expression = "(CurrencyAmount/(IsNull([SalesTax_Percentage],0)+100))* IsNull([SalesTax_Percentage],0) " 'Task:2374 Show Tax Amount
                dtDisplayDetail.Columns("Tax Amount").Expression = "(Total/(IsNull([SalesTax_Percentage],0)+100))* IsNull([SalesTax_Percentage],0)" 'Task:2374 Show Tax Amount
                dtDisplayDetail.Columns("Total Amount").Expression = "(IsNull([Total],0) - IsNull([Tax Amount],0)+IsNull([SED_Tax_Amount],0))" 'Task:2374 Show Total Amount
                dtDisplayDetail.Columns("TotalCurrencyAmount").Expression = "(IsNull([CurrencyAmount],0) - IsNull([CurrencyTaxAmount],0)+IsNull([CurrencySEDAmount],0))"
            End If
            ''End TFS3758
            If dtDisplayDetail.Rows.Count > 0 Then
                If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                    Me.cmbCurrency.Enabled = True
                    If Not Me.cmbCurrency.SelectedIndex = -1 Then
                        Me.cmbCurrency.SelectedValue = 1
                        CurrencyRate = 1
                    End If
                Else
                    'FillCombo("Currency")
                    'Me.cmbCurrency.SelectedValue = Math.Round(Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString), TotalAmountRounding)
                    'CurrencyRate = Math.Round(Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString), TotalAmountRounding)
                    'Me.cmbCurrency.Enabled = False
                    CurrencyRate = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString)
                    Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                    'txtCurrencyRate.Text = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString)
                    '' Being made editable against TASK TFS3493 on 07/06/18
                    Me.cmbCurrency.Enabled = True
                End If
                'cmbCurrency_SelectedIndexChanged(Nothing, Nothing)
                'CurrencyRate = 1
            End If

            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail




            FillCombo("grdLocation")
            'Ayesha Rehman: TFS2827 : 29-Mar-2018 : Fill combo boxes
            FillCombo("grdDiscountType")
            'Ayesha Rehman : TFS2827 : 29-Mar-2018 : End
            ''Ayesha Rehman: TFS4181: 16-08-2018 : Fill combo boxes
            FillCombo("grdBatchNo")
            ''Ayesha Rehman : TFS4181 : 16-08-2018 : End
            ApplyGridSetting()
            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar3_Load(Nothing, Nothing)
            GetSalesOrderAnalysis()
            If flgLoadItems = True Then
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    If Me.grd.RowCount > 0 Then
                        r.BeginEdit()
                        r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
                        r.EndEdit()
                    End If
                Next
            End If
            Dim dtOType As New DataTable
            dtOType.TableName = "Default"
            dtOType.Columns.Add("SaleOrderType", GetType(System.String))
            Dim dr As DataRow
            dr = dtOType.NewRow
            dr(0) = "Supply"
            dtOType.Rows.Add(dr)
            dr = dtOType.NewRow
            dr(0) = "Services"
            dtOType.Rows.Add(dr)
            dr = dtOType.NewRow
            dr(0) = "Out Sourcing"
            dtOType.Rows.Add(dr)
            Me.grd.RootTable.Columns("SaleOrderType").ValueList.PopulateValueList(dtOType.DefaultView, "SaleOrderType", "SaleOrderType")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function Update_Record() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer

        If ApprovalProcessId = 0 Then
            'Start TFS3113 :Abubakar Siddiq
            If Me.chkPost.Visible = False Then
                Me.chkPost.Checked = False
            End If
        Else
            Me.chkPost.Visible = False
        End If
        ''End TFS3113

        'Dim TransitPercent As Double = 0D
        'Dim WHTaxPercent As Double = 0D
        Dim NetAmount As Double = 0D
        Dim TransitValue As Double = 0D
        Dim MarketReturnsValue As Double = 0D
        Dim WHTaxValue As Double = 0D
        Dim SpecialAdj As Double = 0D
        Dim Disc As Double = 0D
        Dim BillAfterDisc As Double = 0D
        Dim TradeValue As Double = 0D

        Dim SalesOrderAnalysis As Boolean
        If Not getConfigValueByType("SalesOrderAnalysis").ToString = "Error" Then
            SalesOrderAnalysis = Convert.ToBoolean(getConfigValueByType("SalesOrderAnalysis").ToString())
        End If
        'If Not GetConfigValue("TransitInssuranceTax").ToString = "Error" Then
        '    TransitPercent = Val(GetConfigValue("TransitInssuranceTax"))
        'End If
        'If Not GetConfigValue("WHTax_Percentage").ToString = "Error" Then
        '    WHTaxPercent = Val(GetConfigValue("WHTax_Percentage"))
        'End If
        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Me.grd.Update()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try

            ServerDate(trans)

            'If getConfigValueByType("EnableDuplicateSalesOrder").ToString.ToUpper = "TRUE" Then
            '    Call CreateDuplicationSalesOrder(Val(Me.txtReceivingID.Text), "Update", trans) 'TASKM2710151
            'End If

            If SalesOrderAnalysis = True Then
                'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                '    MarketReturnsValue += IIf(r.Cells(GrdEnum.Unit).Value = "Pack", (((r.Cells("Qty").Value * r.Cells("PackQty").Value) + r.Cells("SchemeQty").Value) * r.Cells("MarketReturns").Value), ((r.Cells("Qty").Value + r.Cells("SchemeQty").Value) * r.Cells("MarketReturns").Value))
                '    Disc += IIf(r.Cells(GrdEnum.Unit).Value = "Loose", (((r.Cells("Qty").Value * r.Cells("CurrentPrice").Value) * r.Cells("Discount_Percentage").Value) / 100), ((((r.Cells("Qty").Value * r.Cells("PackQty").Value) * r.Cells("CurrentPrice").Value) * r.Cells("Discount_Percentage").Value) / 100))
                '    BillAfterDisc += IIf(r.Cells(GrdEnum.Unit).Value = "Loose", ((r.Cells("Qty").Value * r.Cells("CurrentPrice").Value) - Disc), (((r.Cells("Qty").Value * r.Cells("PackQty").Value) * r.Cells("CurrentPrice").Value) - Disc))
                '    SpecialAdj += ((BillAfterDisc * Val(Me.txtSpecialAdjustment.Text)) / 100)
                '    TradeValue += IIf(r.Cells(GrdEnum.Unit).Value = "Loose", (((r.Cells("Qty").Value * r.Cells("TradePrice").Value) + ((r.Cells("SchemeQty").Value * r.Cells("CurrentPrice").Value) * r.Cells("SalesTax_Percentage").Value) / 100)), ((((r.Cells("Qty").Value * r.Cells("PackQty").Value) * r.Cells("TradePrice").Value) + ((r.Cells("SchemeQty").Value * r.Cells("CurrentPrice").Value) * r.Cells("SalesTax_Percentage").Value) / 100)))
                'Next
                'WHTaxValue = ((TradeValue * WHTaxPercent) / 100)
                'TransitValue = ((Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("BillValueAfterDiscount"), Janus.Windows.GridEX.AggregateFunction.Sum)) * TransitPercent) / 100)
                'NetAmount = (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("BillValueAfterDiscount"), Janus.Windows.GridEX.AggregateFunction.Sum)) + TransitValue + WHTaxValue) - (SpecialAdj + MarketReturnsValue)
                NetAmount = Math.Round(Val(Me.txtNetBill.Text), TotalAmountRounding)
            Else
                NetAmount = (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.Total), Janus.Windows.GridEX.AggregateFunction.Sum)))
            End If

            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'objCon.BeginTransaction()

            ChangeDataSave(Val(Me.txtReceivingID.Text), GetServerDate, trans)




            'objCommand.CommandText = ""
            'objCommand.CommandText = "Update QuotationDetailTable Set SOQuantity=IsNull(SOQuantity,0)-IsNull(SO.SOQty,0), TotalDeliveredQty = IsNull(Qty,0)-IsNull(SO.TotalQty,0) From QuotationDetailTable,(Select QuotationDetailId, ArticleDefId, SUM(IsNull(Sz1,0)) as SOQty , SUM(IsNull(Qty,0)) as TotalQty From SalesOrderDetailTable WHERE SalesOrderID=" & Val(Me.txtReceivingID.Text) & " AND IsNull(QuotationDetailId,0) <> 0 Group By QuotationDetailId,ArticleDefId) as SO WHERE SO.QuotationDetailId = QuotationDetailTable.QuotationDetailId And SO.ArticleDefId = QuotationDetailTable.ArticleDefId"
            'objCommand.ExecuteNonQuery()

            'objCommand.CommandText = "Update SalesOrderMasterTable set SalesOrderNo =N'" & txtPONo.Text & "',SalesOrderDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '& " SalesOrderQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", SalesOrderAmount=" & NetAmount & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', SpecialAdjustment=" & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", Posted=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", PONo=N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', SOP_ID=" & Me.cmbSalesMan.SelectedValue & ", Delivery_Date=" & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Adj_Flag=" & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", Adjustment=" & IIf(Me.rbtAdjFlat.Checked = False, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", CostCenterId=" & Me.cmbProject.SelectedValue & ", PO_Date=" & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", EditionalTax_Percentage=" & TransitPercent & ", SED_Percentage=" & WHTaxPercent & ", Terms_And_Condition=N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "',OrderStatus=N'" & Me.cmbOrderStatus.Text.Replace("'", "''") & "' Where SalesOrderID= " & txtReceivingID.Text & " "

            '   objCommand.CommandText = "Update SalesOrderMasterTable set SalesOrderNo =N'" & txtPONo.Text & "',SalesOrderDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '& " SalesOrderQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", SalesOrderAmount=" & NetAmount & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', SpecialAdjustment=" & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", Posted=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", PONo=N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', SOP_ID=" & Me.cmbSalesMan.SelectedValue & ", Delivery_Date=" & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Adj_Flag=" & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", Adjustment=" & IIf(Me.rbtAdjFlat.Checked = False, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", CostCenterId=" & Me.cmbProject.SelectedValue & ", PO_Date=" & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", EditionalTax_Percentage=" & TransitPercent & ", SED_Percentage=" & WHTaxPercent & ", Terms_And_Condition=N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "',OrderStatus=N'" & Me.cmbOrderStatus.Text.Replace("'", "''") & "',QuotationId=" & IIf(Me.cmbQuotation.SelectedIndex = -1, 0, Me.cmbQuotation.SelectedValue) & " Where SalesOrderID= " & txtReceivingID.Text & " "



            objCommand.CommandText = ""
            objCommand.CommandText = "Update QuotationDetailTable Set SOQuantity=IsNull(SOQuantity,0)-IsNull(SO.SOQty,0), DeliveredTotalQty = IsNull(DeliveredTotalQty,0)-IsNull(SO.TotalDeliveredQty,0) From QuotationDetailTable,(Select QuotationDetailId, ArticleDefId, SUM(IsNull(Sz1,0)) as SOQty, SUM(IsNull(Qty,0)) as TotalDeliveredQty From SalesOrderDetailTable WHERE SalesOrderID=" & Val(Me.txtReceivingID.Text) & " AND IsNull(QuotationDetailId,0) <> 0 Group By QuotationDetailId,ArticleDefId) as SO WHERE SO.QuotationDetailId = QuotationDetailTable.QuotationDetailId And SO.ArticleDefId = QuotationDetailTable.ArticleDefId"
            objCommand.ExecuteNonQuery()


            '" & Val(txtTechnicalDrawingNo.Text) & ", N'" & Me.dtpTechnicalDrawingDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "', '" & Me.txtAccountsRemarks.Text.Replace("'", "''") & "', '" & Me.txtStoreRemarks.Text.Replace("'", "''") & "', '" & Me.txtProductionRemarks.Text.Replace("'", "''") & "', '" & Me.txtServicesRemarks.Text.Replace("'", "''") & "', '" & Me.txtSalesRemarks.Text.Replace("'", "''") & "'
            ''TASK TFS1764 new field of TransporterId on 15-11-2017
            objCommand.CommandText = String.Empty
            objCommand.CommandText = "Update SalesOrderMasterTable set SalesOrderNo =N'" & txtPONo.Text & "',SalesOrderDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
         & " SalesOrderQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQuantity"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", SalesOrderAmount=" & NetAmount & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UpdateUserName=N'" & LoginUserName & "', SpecialAdjustment=" & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", Posted=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", PONo=N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', SOP_ID=" & Me.cmbSalesMan.SelectedValue & ", Delivery_Date=" & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Adj_Flag=" & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", Adjustment=" & IIf(Me.rbtAdjFlat.Checked = False, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", CostCenterId=" & Me.cmbProject.SelectedValue & ", PO_Date=" & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", EditionalTax_Percentage=" & TransitPercent & ", SED_Percentage=" & WHTaxPercent & ", Terms_And_Condition=N'" & ReplaceNewLine(Me.txtTerms_And_Condition.Text, False).Replace("'", "''") & "',SaleOrderStatusId=" & Me.cmbOrderStatus.SelectedValue & ",QuotationId=" & IIf(Me.cmbQuotation.SelectedIndex = -1, 0, Me.cmbQuotation.SelectedValue) & ", RevisionNumber= IsNull(RevisionNumber, 0) + 1, SpecialInstructions = N'" & Me.txtSpecialInstructions.Text.Replace("'", "''") & "', TechnicalDrawingNumber =" & Val(txtTechnicalDrawingNo.Text) & ", TechnicalDrawingDate = " & IIf(Me.dtpTechnicalDrawingDate.Checked = True, "N'" & Me.dtpTechnicalDrawingDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'", "NULL") & ", AccountsRemarks ='" & Me.txtAccountsRemarks.Text.Replace("'", "''") & "', StoreRemarks='" & Me.txtStoreRemarks.Text.Replace("'", "''") & "', ProductionRemarks='" & Me.txtProductionRemarks.Text.Replace("'", "''") & "', ServicesRemarks='" & Me.txtServicesRemarks.Text.Replace("'", "''") & "', SalesRemarks='" & Me.txtSalesRemarks.Text.Replace("'", "''") & "', UserId =  " & Val(LoginUserId) & ", TermOfPayments='" & Me.cmbTermOfPayments.Text.Replace("'", "''") & "', TransporterId= " & IIf(Me.cmbTransporter.SelectedValue = Nothing, 0, Me.cmbTransporter.SelectedValue) & " Where SalesOrderID= " & txtReceivingID.Text & " "

            objCommand.ExecuteNonQuery()
            'Marked Against Task#2015060001 Ali Ansari
            'If arrFile.Length > 0 Then
            '    SaveDocument(Val(txtReceivingID.Text), Me.Name, trans)
            'End If
            'Marked Against Task#2015060001 Ali Ansari

            ''Commented on 27-07-2016
            'objCommand.CommandText = ""
            'objCommand.CommandText = "Delete from SalesOrderDetailTable where SalesOrderID = " & txtReceivingID.Text
            'objCommand.ExecuteNonQuery()

            'For i = 0 To grd.Rows.Count - 1
            '    If grd.Rows(i).Cells("Qty").Value <> 0 Then
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice) values( " _
            '                                & " " & txtReceivingID.Text & " ," & Val(grd.Rows(i).Cells(7).Value) & ",N'" & (grd.Rows(i).Cells(2).Value) & "'," & Val(grd.Rows(i).Cells(3).Value) & ", " _
            '                                & " " & IIf(grd.Rows(i).Cells(2).Value = "Loose", Val(grd.Rows(i).Cells(3).Value), (Val(grd.Rows(i).Cells(3).Value) * Val(grd.Rows(i).Cells(8).Value))) & ", " & Val(grd.Rows(i).Cells(4).Value) & ", " & Val(grd.Rows(i).Cells(8).Value) & "  , " & Val(grd.Rows(i).Cells(9).Value) & ") "

            '        objCommand.ExecuteNonQuery()
            '        'Val(grd.Rows(i).Cells(5).Value)
            '    End If
            'Next

            For i = 0 To grd.RowCount - 1
                ' If Val(grd.GetRows(i).Cells("Qty").Value) <> 0 Then
                'If IsValidToDelete("DeliveryChalanDetailTable", "SO_Detail_ID", Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString)) = True Then
                ''R-916 Added Column Comments
                objCommand.CommandText = ""
                objCommand.CommandText = "SELECT Distinct IsNull(SO_Detail_ID, 0) As SO_Detail_ID from DeliveryChalanDetailTable where SO_Detail_ID ='" & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & "'"
                Dim dt As New DataTable
                Dim adp As New OleDbDataAdapter
                adp.SelectCommand = objCommand
                'adp = New OleDbDataAdapter(strSql, Con)
                adp.Fill(dt)
                dt.AcceptChanges()
                Dim SODetailId As Integer = 0
                If dt.Rows.Count > 0 Then
                    SODetailId = dt.Rows(0).Item(0)
                Else
                    SODetailId = 0
                End If
                Dim IsValidToDelete1 As Boolean = False
                If flgSOUpdateAfterDelivery = False Then
                    IsValidToDelete1 = False 'If IsValidToDelete("SalesMasterTable", "PoID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True AndAlso IsValidToDelete("DeliveryChalanMasterTable", "POId", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True AndAlso IsValidToDelete("PlanDetailTable", "SOID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True Then
                ElseIf flgSOUpdateAfterDelivery = True AndAlso UpdateDeliveredSO = True Then
                    IsValidToDelete1 = True
                    SODetailId = 0
                End If
                If SODetailId = 0 Then

                    objCommand.CommandText = ""
                    'objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice, PackPrice,Pack_Desc) values( " _
                    '                        & " N'" & Val(Me.txtReceivingID.Text) & "', " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                    '                        & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                    'objCommand.ExecuteNonQuery()
                    'Before against task:M16
                    'objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice, PackPrice,Pack_Desc, Comments) values( " _
                    '                       & " N'" & Val(Me.txtReceivingID.Text) & "', " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                    '                       & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "') "
                    'Task:M16 Added Column Engine_No And Chassis_No
                    'objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice, PackPrice,Pack_Desc, Comments, Engine_No, Chassis_No) values( " _
                    '                      & " N'" & Val(Me.txtReceivingID.Text) & "', " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                    '                      & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "') "
                    'End Task:M16
                    'Task:2801 Added Field DeliveredQty
                    'Before against task:2832
                    'objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice, PackPrice,Pack_Desc, Comments, Engine_No, Chassis_No, DeliveredQty) values( " _
                    '                    & " N'" & Val(Me.txtReceivingID.Text) & "', " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                    '                    & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells("DeliveredQty").Value.ToString) & ") "
                    'End Task:2801
                    'TAsk:2832 Added PlanedQty Field
                    'objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice, PackPrice,Pack_Desc, Comments, Engine_No, Chassis_No, DeliveredQty, PlanedQty,Other_Comments) values( " _
                    '                   & " N'" & Val(Me.txtReceivingID.Text) & "', " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                    '                   & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells("DeliveredQty").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PlanedQty").Value.ToString) & ",N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "') "


                    ''Commented on 27-07-2016
                    'objCommand.CommandText = "Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice, PackPrice,Pack_Desc, Comments, Engine_No, Chassis_No, DeliveredQty, PlanedQty,Other_Comments,CostPrice, QuotationDetailId,SED_Tax_Percent,SED_Tax_Amount,ArticleAliasName,SaleOrderType) values( " _
                    '                 & " N'" & Val(Me.txtReceivingID.Text) & "', " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & IIf(Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) = 0, "NULL", Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                    '                 & " " & Val(grd.GetRows(i).Cells(GrdEnum.TotalQuantity).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells("DeliveredQty").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PlanedQty").Value.ToString) & ",N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.OuotationDetailId).Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("SED_Tax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("SED_Tax_Amount").Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("ArticleAliasName").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("SaleOrderType").Value.ToString.Replace("'", "''") & "') Select @@Identity"

                    objCommand.CommandText = "If Exists(Select SalesOrderDetailId From SalesOrderDetailTable Where SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ") Update SalesOrderDetailTable Set SalesOrderId = N'" & Val(Me.txtReceivingID.Text) & "', LocationId = " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ", ArticleDefId = " & IIf(Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) = 0, "NULL", Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)) & ", " _
                                   & "ArticleSize = N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "', Sz1 = " & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", Qty =" & Val(grd.GetRows(i).Cells(GrdEnum.TotalQuantity).Value.ToString) & ", Price = " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", Sz7 =" & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & ", CurrentPrice = " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & " , TradePrice = " & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & ", " _
                                   & " SalesTax_Percentage = " & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", SchemeQty = " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", Discount_Percentage = " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", Freight = " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & ", MarketReturns = " & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", PurchasePrice = " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " _
                                   & " PackPrice = " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", Pack_Desc = N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', Comments =N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', Engine_No = N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', Chassis_No = N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', DeliveredQty = " & Val(Me.grd.GetRows(i).Cells("DeliveredQty").Value.ToString) & ", " _
                                   & " PlanedQty = " & Val(Me.grd.GetRows(i).Cells("PlanedQty").Value.ToString) & " , Other_Comments = N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "', CostPrice = " & Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString) & ", QuotationDetailId = " & Val(grd.GetRows(i).Cells(GrdEnum.OuotationDetailId).Value.ToString) & ", SED_Tax_Percent =   " & Val(Me.grd.GetRows(i).Cells("SED_Tax_Percent").Value.ToString) & ", SED_Tax_Amount = " & Val(Me.grd.GetRows(i).Cells("SED_Tax_Amount").Value.ToString) & ", " _
                                   & " ArticleAliasName = N'" & Me.grd.GetRows(i).Cells("ArticleAliasName").Value.ToString.Replace("'", "''") & "', SaleOrderType = N'" & Me.grd.GetRows(i).Cells("SaleOrderType").Value.ToString.Replace("'", "''") & "' , BaseCurrencyId = " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", BaseCurrencyRate = " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", CurrencyId = " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", CurrencyRate = " & Val(Me.grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & " , CurrencyAmount = " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", SerialNo= N'" & grd.GetRows(i).Cells("SerialNo").Value.ToString.Replace("'", "''") & "', DiscountId = " & Val(grd.GetRows(i).Cells("DiscountId").Value.ToString) & ", DiscountValue = " & Val(Me.grd.GetRows(i).Cells("DiscountValue").Value.ToString) & ", DiscountFactor = " & Val(Me.grd.GetRows(i).Cells("DiscountFactor").Value.ToString) & ", PostDiscountPrice = " & Val(Me.grd.GetRows(i).Cells("PostDiscountPrice").Value) & ", BatchNo = " & Val(Me.grd.GetRows(i).Cells(GrdEnum.BatchNo).Value.ToString) & ", ExpiryDate = '" & CType(grd.GetRows(i).Cells(GrdEnum.ExpiryDate).Value, Date).ToString("yyyy-M-d h:mm:ss tt") & "' Where SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & "" _
                                   & " Else Insert into SalesOrderDetailTable (SalesOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice, PackPrice,Pack_Desc, Comments, Engine_No, Chassis_No, DeliveredQty, PlanedQty,Other_Comments,CostPrice, QuotationDetailId,SED_Tax_Percent,SED_Tax_Amount,ArticleAliasName,SaleOrderType, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, SerialNo , DiscountId, DiscountValue, DiscountFactor , PostDiscountPrice, BatchNo , ExpiryDate) values( " _
                                   & " N'" & Val(Me.txtReceivingID.Text) & "', " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & IIf(Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) = 0, "NULL", Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                                   & " " & Val(grd.GetRows(i).Cells(GrdEnum.TotalQuantity).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " _
                                   & " " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", " _
                                   & " N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells("DeliveredQty").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PlanedQty").Value.ToString) & ", " _
                                   & " N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.OuotationDetailId).Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("SED_Tax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("SED_Tax_Amount").Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("ArticleAliasName").Value.ToString.Replace("'", "''") & "', " _
                                   & " N'" & Me.grd.GetRows(i).Cells("SaleOrderType").Value.ToString.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", N'" & grd.GetRows(i).Cells("SerialNo").Value.ToString.Replace("'", "''") & "'," & Val(grd.GetRows(i).Cells("DiscountId").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("DiscountValue").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("DiscountFactor").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PostDiscountPrice").Value) & ", '" & Val(Me.grd.GetRows(i).Cells(GrdEnum.BatchNo).Value.ToString) & "' , '" & CType(grd.GetRows(i).Cells(GrdEnum.ExpiryDate).Value, Date).ToString("yyyy-M-d h:mm:ss tt") & "') Select @@Identity"

                    Dim obj As Object = objCommand.ExecuteScalar()
                    Dim intSalesOrderDetailId As Integer = 0
                    If Not obj Is DBNull.Value Then
                        intSalesOrderDetailId = Convert.ToInt32(obj)
                    End If
                    obj = Nothing
                    'Val(grd.Rows(i).Cells(5).Value)
                    ' End If

                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update PlanDetailTable Set SODetailId=" & intSalesOrderDetailId & " WHERE SODetailID=" & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & " AND SOId=" & Val(Me.txtReceivingID.Text) & ""
                    objCommand.ExecuteNonQuery()

                    'If Val(Me.grd.GetRows(i).Cells(GrdEnum.OuotationDetailId).Value.ToString) > 0 Then
                    '    If Me.cmbQuotation.SelectedValue > 0 Then
                    '        Dim dt As New DataTable
                    '        Dim str As String = "Select IsNull(Qty, 0) As Qty, IsNull(SOQuantity, 0) As SOQuantity From QuotationDetailTable Where QuotationDetailId =" & Val(Me.grd.GetRows(i).Cells(GrdEnum.OuotationDetailId).Value.ToString) & " "
                    '        dt = GetDataTable(str)
                    '        If dt.Rows.Item(0).Item(0) >= IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) Then
                    '            objCommand.CommandText = ""
                    '            objCommand.CommandText = "Update QuotationDetailTable Set SOQuantity= " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & " Where QuotationDetailId =" & Val(Me.grd.GetRows(i).Cells(GrdEnum.OuotationDetailId).Value.ToString) & ""
                    '            objCommand.ExecuteNonQuery()
                    '            If dt.Rows.Item(0).Item(0) = IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) Then
                    '                objCommand.CommandText = ""
                    '                objCommand.CommandText = "Update QuotationMasterTable Set Status ='Close' Where QuotationId = " & Val(Me.grd.GetRows(i).Cells(GrdEnum.QuotationId).Value.ToString) & ""
                    '                objCommand.ExecuteNonQuery()
                    '            End If
                    '        Else
                    '            msg_Error("Quantity is larger than available " & dt.Rows.Item(0).Item(0) & " quantity in quotation")
                    '            Exit Function
                    '            grd.Focus()
                    '        End If

                    '    End If

                    'End If
                    SaveArticleAlias(Me.grd.GetRows(i), trans)

                Else
                    msg_Error(str_ErrorDependentUpdateRecordFound)
                    Exit Function
                End If
            Next



            objCommand.CommandText = ""
            objCommand.CommandText = "Update QuotationDetailTable Set SOQuantity=IsNull(SOQuantity,0)+IsNull(SO.SOQty,0), DeliveredTotalQty =IsNull(DeliveredTotalQty,0)+IsNull(SO.TQty,0) From QuotationDetailTable,(Select QuotationDetailId, ArticleDefId, SUM(IsNull(Sz1,0)) as SOQty , SUM(IsNull(Qty,0)) as TQty From SalesOrderDetailTable WHERE SalesOrderID=" & Val(Me.txtReceivingID.Text) & " AND IsNull(QuotationDetailId,0) <> 0 Group By QuotationDetailId,ArticleDefId) as SO WHERE SO.QuotationDetailId = QuotationDetailTable.QuotationDetailId And SO.ArticleDefId = QuotationDetailTable.ArticleDefId"
            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""
            objCommand.CommandText = "Update QuotationMasterTable Set Status=Case When IsNull(SO.BalanceQty,0) > 0 then 'Open' else 'Close' End  From QuotationMasterTable,(Select QuotationId, SUM(IsNull(Sz1,0)-IsNull(SOQuantity,0)) BalanceQty From QuotationDetailTable WHERE IsNull(QuotationDetailId,0) in(Select distinct IsNull(QuotationDetailId,0) From SalesOrderDetailTable WHERE SalesOrderID=" & Val(Me.txtReceivingID.Text) & ")   Group By QuotationId  )  as SO WHERE SO.QuotationId = QuotationMasterTable.QuotationId "
            objCommand.ExecuteNonQuery()



            objCommand.CommandText = ""
            objCommand.CommandText = "Delete From OutwardExpenseDetailTable WHERE SalesId= " & Me.txtReceivingID.Text & " AND Doctype='SO'"
            objCommand.ExecuteNonQuery()

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdOutwardExpDetail.GetRows
                If Val(r.Cells("Exp_Amount").Value.ToString) <> 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO OutwardExpenseDetailTable(SalesId, AccountId, Exp_Amount,DocType) Values(ident_current('SalesOrderMasterTable'), " & Val(r.Cells("AccountId").Value.ToString) & "," & Val(r.Cells("Exp_Amount").Value) & ",'SO')"
                    objCommand.ExecuteNonQuery()
                End If
            Next


            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                SaveDocument(Val(txtReceivingID.Text), Me.Name, trans)
            End If
            'Altered Against Task#2015060001 Ali Ansari
            'Task:2801 Update Sale Order Status 
            objCommand.CommandText = ""
            objCommand.CommandText = "Update SalesOrderMasterTable Set Status=Case When SODT.Qty > 0 Then 'Open' Else 'Close' End From SalesOrderMasterTable, (Select SalesOrderId, SUM(IsNull(Sz1,0)-IsNull(DeliveredQty,0)) as Qty From SalesOrderDetailTable WHERE SalesOrderDetailTable.SalesOrderId=" & Val(Me.txtReceivingID.Text) & " Group By SalesOrderId ) SODt WHERE SoDt.SalesOrderId = SalesOrderMasterTable.SalesOrderId AND SalesOrderMasterTable.SalesOrderId=" & Val(Me.txtReceivingID.Text) & ""
            objCommand.ExecuteNonQuery()
            'End Task:2801
            ' SaveDocument(Val(txtReceivingID.Text), Me.Name, trans)
            ''Commented below line as it is being called again here on 21-06-2016
            'objCommand.CommandText = ""
            'objCommand.CommandText = "Update QuotationDetailTable Set SOQuantity=IsNull(SOQuantity,0)+IsNull(SO.SOQty,0), DeliveredTotalQty =IsNull(DeliveredTotalQty,0)+IsNull(SO.DeliveredTotalQuantity,0) From QuotationDetailTable,(Select QuotationDetailId, ArticleDefId, SUM(IsNull(Sz1,0)) as SOQty, SUM(IsNull(Qty, 0)) As DeliveredTotalQuantity From SalesOrderDetailTable WHERE SalesOrderID=" & Val(Me.txtReceivingID.Text) & " AND IsNull(QuotationDetailId,0) <> 0 Group By QuotationDetailId,ArticleDefId) as SO WHERE SO.QuotationDetailId = QuotationDetailTable.QuotationDetailId And SO.ArticleDefId = QuotationDetailTable.ArticleDefId"
            'objCommand.ExecuteNonQuery()

            'objCommand.CommandText = ""
            'objCommand.CommandText = "Update QuotationMasterTable Set Status=Case When IsNull(SO.BalanceQty,0) > 0 then 'Open' else 'Close' End  From QuotationMasterTable,(Select QuotationId, SUM(IsNull(Sz1,0)-IsNull(SOQuantity,0)) BalanceQty From QuotationDetailTable WHERE IsNull(QuotationDetailId,0) in(Select distinct IsNull(QuotationDetailId,0) From SalesOrderDetailTable WHERE SalesOrderID=" & Val(Me.txtReceivingID.Text) & ")   Group By QuotationId  )  as SO WHERE SO.QuotationId = QuotationMasterTable.QuotationId "
            'objCommand.ExecuteNonQuery()
            '' 21-06-2016
            ''Start TASK-480
            If getConfigValueByType("EnableDuplicateSalesOrder").ToString.ToUpper = "TRUE" Then
                Call CreateDuplicationSalesOrder(Val(Me.txtReceivingID.Text), "Update", trans)
            End If
            ''End TASK-480
            trans.Commit()
            Update_Record = True
            'InsertVoucher()
            setVoucherNo = Me.txtPONo.Text
            getVoucher_Id = Me.txtReceivingID.Text
            setEditMode = True
            Total_Amount = NetAmount 'Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            MarketReturnsValue = 0D
            Disc = 0D
            BillAfterDisc = 0D
            SpecialAdj = 0D
            TradeValue = 0D
            WHTaxValue = 0D
            TransitValue = 0D
            NetAmount = 0D
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try

        ''insert Activity Log
        SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, Me.txtPONo.Text.Trim, True)
        ''Start TFS3113
        If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim, Me.Name) Then
            If ValidateApprovalProcessIsInProgressAgain(Me.txtPONo.Text.Trim, Me.Name) = False Then
                SaveApprovalLog(EnumReferenceType.SalesOrder, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Sales Order ," & cmbVendor.Text & "", Me.Name, 7)
            End If
        End If
        ''End TFS3113
        SendSMS()


    End Function
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.BtnSave.Enabled = False Then Exit Sub
        Try
            Me.cmbProject.SelectedIndex = IIf(Me.cmbProject.SelectedIndex > 0, Me.cmbProject.SelectedIndex, 0)

            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            'If flgDateLock = True Then
            '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            If IsDateLock(Me.dtpPODate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If
            If Me.dtpPODate.Value <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("Your can not change this becuase financial year is Close")
                Me.dtpPODate.Focus()
                Exit Sub
            End If

            If FormValidate() Then
                Me.grd.UpdateData()
                If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Save() Then
                        SendAutoEmail()
                        'EmailSave()
                        'msg_Information(str_informSave)
                        RefreshControls()
                        ClearDetailControls()
                        Dim Printing As Boolean
                        Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        If Printing = True Then
                            If msg_Confirm("Do you want to print") = True Then
                                Me.PrintSalesOrderToolStripMenuItem_Click(Nothing, Nothing)
                            End If
                        End If
                        'grd.Rows.Clear()
                        'DisplayRecord() R933 Commented History Data
                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        'Do While BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop
                        '------------------------------------
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop
                    Else
                        Exit Sub 'MsgBox("Record has not been added")
                    End If
                Else
                    'If blnOpenSO = False Then
                    'If Not IsValidToDelete("SalesMasterTable", "PoID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True Or Not IsValidToDelete("DeliveryChalanMasterTable", "PoID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True Then
                    '    msg_Error(str_ErrorDependentUpdateRecordFound)
                    '    Exit Sub
                    'End If
                    ''TASK: TFS1268 Allow user to update Sales Order in certain condition
                    Dim IsValidToDelete1 As Boolean = False
                    If flgSOUpdateAfterDelivery = False Then
                        IsValidToDelete1 = False 'If IsValidToDelete("SalesMasterTable", "PoID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True AndAlso IsValidToDelete("DeliveryChalanMasterTable", "POId", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True AndAlso IsValidToDelete("PlanDetailTable", "SOID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True Then
                    ElseIf flgSOUpdateAfterDelivery = True AndAlso UpdateDeliveredSO = True Then
                        IsValidToDelete1 = True
                    End If

                    If IsValidToDelete1 = False Then
                        If IsValidToDelete("SalesMasterTable", "PoID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True AndAlso IsValidToDelete("DeliveryChalanMasterTable", "POId", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True AndAlso IsValidToDelete("PlanDetailTable", "SOID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True Then

                            If blnOpenSO = False Then
                                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                            End If
                            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                            Me.lblProgress.Text = "Processing Please Wait ..."
                            Me.lblProgress.Visible = True
                            Application.DoEvents()
                            If Update_Record() Then
                                'EmailSave()
                                'msg_Information(str_informUpdate)
                                RefreshControls()
                                ClearDetailControls()
                                'grd.Rows.Clear()
                                'DisplayRecord() R933 Commented History Data
                                If BackgroundWorker2.IsBusy Then Exit Sub
                                BackgroundWorker2.RunWorkerAsync()
                                'Do While BackgroundWorker2.IsBusy
                                '    Application.DoEvents()
                                'Loop
                                '------------------------------------
                                If BackgroundWorker1.IsBusy Then Exit Sub
                                BackgroundWorker1.RunWorkerAsync()
                                'Do While BackgroundWorker1.IsBusy
                                '    Application.DoEvents()
                                'Loop
                            Else
                                Exit Sub 'MsgBox("Record has not been updated")
                            End If
                        Else
                            msg_Error(str_ErrorDependentUpdateRecordFound)
                        End If

                    Else
                        If blnOpenSO = False Then
                            If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                        End If
                        'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                        Me.lblProgress.Text = "Processing Please Wait ..."
                        Me.lblProgress.Visible = True
                        Application.DoEvents()
                        If Update_Record() Then
                            'EmailSave()
                            'msg_Information(str_informUpdate)
                            RefreshControls()
                            ClearDetailControls()
                            Dim Printing As Boolean
                            Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                            If Printing = True Then
                                If msg_Confirm("Do you want to print") = True Then
                                    Me.PrintSalesOrderToolStripMenuItem_Click(Nothing, Nothing)
                                End If
                            End If
                            'grd.Rows.Clear()
                            'DisplayRecord() R933 Commented History Data
                            If BackgroundWorker2.IsBusy Then Exit Sub
                            BackgroundWorker2.RunWorkerAsync()
                            'Do While BackgroundWorker2.IsBusy
                            '    Application.DoEvents()
                            'Loop
                            '------------------------------------
                            If BackgroundWorker1.IsBusy Then Exit Sub
                            BackgroundWorker1.RunWorkerAsync()
                            'Do While BackgroundWorker1.IsBusy
                            '    Application.DoEvents()
                            'Loop
                        Else
                            Exit Sub 'MsgBox("Record has not been updated")
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate("Sales Order")
            If EmailTemplate.Length > 0 Then
                GetAutoEmailData()
                'GetVendorsEmails() ''Commented Against TFS3239
                'FillDataSet()
                UsersEmail = New List(Of String)
                'UsersEmail.Add("adil@agriusit.com")
                ''UsersEmail.Add("ali@agriusit.com")
                If Con.Database.Contains("Remms") Then
                    UsersEmail.Add("r.ejaz@remmsit.com")
                Else
                    UsersEmail.Add("r.ejaz@agriusit.com")
                End If
                FormatStringBuilder(dtEmail)
                'CreateOutLookMail()
                For Each _email As String In UsersEmail
                    CreateOutLookMail(_email)
                    SaveEmailLog(SalesOrderNo, _email, "frmSalesOrderNew", Activity)
                Next
                'SaveCCBCC(CC, BCC)
                'CC = ""
                'BCC = ""
            Else
                ShowErrorMessage("No email template is found for Sales Order.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FormatStringBuilder(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            'Dim dt As DataTable = Me.GetData()

            'Building an HTML string.
            'Dim html As New StringBuilder()

            'Table start.
            'html.Append()
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")

            'Building the Header row.
            html.Append("<tr bgcolor='#58ACFA'>")
            For Each column As DataColumn In dt.Columns
                Dim ColumnName As String = ""
                Dim Pattern = "([a-z?])[_ ]?([A-Z])"
                If column.ColumnName = "SerialNo" Then
                    ColumnName = "Sr#"
                Else
                    ColumnName = Regex.Replace(column.ColumnName, Pattern, "$1 $2")
                End If
                html.Append("<th>")
                html.Append(ColumnName)
                html.Append("</th>")
            Next
            html.Append("</tr>")

            'Building the Data rows.
            For Each row As DataRow In dt.Rows
                If row.Table.Columns.Contains("Alternate") Then
                    If row.Item("Alternate") = "Yes" Then
                        html.Append("<tr bgcolor='#A9F5BC'>")
                        For Each column As DataColumn In dt.Columns
                            html.Append("<td>")
                            html.Append(row(column.ColumnName))
                            html.Append("</td>")
                        Next
                        html.Append("</tr>")
                    Else
                        html.Append("<tr>")
                        For Each column As DataColumn In dt.Columns
                            html.Append("<td>")
                            html.Append(row(column.ColumnName))
                            html.Append("</td>")
                        Next
                        html.Append("</tr>")
                    End If
                Else
                    html.Append("<tr>")
                    For Each column As DataColumn In dt.Columns
                        html.Append("<td>")
                        html.Append(row(column.ColumnName))
                        html.Append("</td>")
                    Next
                    html.Append("</tr>")
                End If
            Next
            'Table end.
            html.Append("</table>")
            html.Append(AfterFieldsElement)
            'html.Append("</body>")
            'html.Append("</html>")

            '     'Append the HTML string to Placeholder.
            'PlaceHolder1.Controls.Add(New Literal() With { _
            '   .Text = html.ToString() _


            'sb.Append("<table border=1 cellspacing=1 cellpadding=1><thead>")
            'For colIndx As Integer = 0 To colIndx < dt.Columns.Count
            '    sb.Append("<th>")
            '    sb.Append(dt.Columns(colIndx).ColumnName)
            '    sb.Append("</th>")
            'Next
            'sb.Append("</thead>")
            ''//add data rows to html table
            'For rowIndx As Integer = 0 To rowIndx < dt.Rows.Count Step 1
            '    sb.Append("<tr>")
            '    For colIndx As Integer = 0 To colIndx < dt.Columns.Count Step 1
            '        sb.Append("<td>")
            '        dt.Rows(rowIndx)(colIndx).ToString()
            '        sb.Append("</td>")
            '    Next
            '    sb.Append("</tr>")
            'Next
            'sb.Append("</table>")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetAutoEmailData()
        Dim Dr As DataRow
        Try
            Dim str As String
            str = "select TOP 1 SalesOrderId, SalesOrderNo, SalesOrderDate from SalesOrderMasterTable order by 1 desc"
            Dim dt1 As DataTable = GetDataTable(str)
            If dt1.Rows.Count > 0 Then
                SalesOrderId = dt1.Rows(0).Item("SalesOrderId")
                SalesOrderNo = dt1.Rows(0).Item("SalesOrderNo")
            End If
            Dim str1 As String
            str1 = "SELECT Recv_D.SerialNo, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleAliasName,ArticleSizeDefTable.ArticleSizeName as Size, ArticleColorDefTable.ArticleColorName as Color, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.PostDiscountPrice, Recv_D.Price, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue ,  " _
                     & " Convert(float,0) as TotalAmount, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, IsNull(Recv_D.Qty, 0) As TotalQuantity FROM dbo.SalesOrderDetailTable Recv_D LEFT OUTER JOIN " _
                     & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                     & " Left Outer Join ArticleColorDefTable On Article.ArticleColorId = ArticleColorDefTable.ArticleColorId Left Outer Join ArticleSizeDefTable on Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
                     & " Where Recv_D.SalesOrderID =" & SalesOrderId & " ORDER BY Article.SortOrder Asc"
            Dim dt As DataTable = GetDataTable(str1)
            For Each Row1 As DataRow In dt.Rows
                Dr = dtEmail.NewRow
                For Each col As String In AllFields
                    If Row1.Table.Columns.Contains(col) Then
                        Dr.Item(col) = Row1.Item(col).ToString
                    End If
                Next
                dtEmail.Rows.Add(Dr)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub GetTemplate(ByVal Title As String)
        Dim Fields As String = String.Empty

        Try
            dtEmail = New DataTable
            'GetEmailData()
            EmailTemplate = EmailDAL.GetTemplate(Title)
            If EmailTemplate.Length > 0 Then
                Dim i, j As Integer
                i = EmailTemplate.IndexOf("<Fields>") + "<Fields>".Length
                j = EmailTemplate.IndexOf("</Fields>") - i

                Dim Searched As String = "</Fields>"
                AfterFieldsElement = EmailTemplate.Substring(EmailTemplate.IndexOf(Searched) + Searched.Length)
                Fields = EmailTemplate.Substring(i, j)
                Dim WOAtTheRate As String = Fields.Replace("@", "")
                Dim WOSpace As String = WOAtTheRate.Replace(" ", "")
                Dim IndexOfFieldElement As Integer = EmailTemplate.IndexOf("<Fields>")
                If IndexOfFieldElement > 0 Then
                    EmailTemplate = EmailTemplate.Remove(IndexOfFieldElement)
                End If
                'EmailTemplate = EmailTemplate.Remove(i, j)
                AllFields = New List(Of String)

                dtEmail.Columns.Clear()
                For Each word As String In WOSpace.Split(",")
                    Dim TrimSpace As String = word.Trim()
                    If Me.grd.RootTable.Columns.Contains(TrimSpace) Then
                        dtEmail.Columns.Add(TrimSpace)
                        AllFields.Add(TrimSpace)
                        'Else
                        '    msg_Error("'" & TrimSpace & "'column does not exist")
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreateOutLookMail(ByVal Email As String)
        Try
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = "Creating New SO: " + SalesOrderNo
            mailItem.To = Email
            Email = String.Empty
            'mailItem.CC = txtCC.Text
            'Me.txtCC.Text = ""
            'mailItem.BCC = txtBCC.Text
            'Me.txtBCC.Text = ""
            'mailItem.Body = html.ToString
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
            EmailBody = html.ToString
            mailItem.Send()
            mailItem = Nothing
            oApp = Nothing
            '     mailItem.Display(mailItem);
            'mailItem.HTMLBody = body + mailItem.HTMLBody;
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        'Dim s As String
        's = "1234-567-890"
        'MsgBox(Microsoft.VisualBasic.Right(s, InStr(1, s, "-") - 2))

        If Me.grd.RowCount > 0 Then
            If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
        End If

        RefreshControls()
    End Sub
    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            EditRecord()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbPo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPo.SelectedIndexChanged

        'Me.DisplayPODetail(Me.cmbPo.SelectedValue)
        'If Me.cmbPo.SelectedIndex > 0 Then
        '    Dim adp As New OleDbDataAdapter
        '    Dim dt As New DataTable
        '    Dim Sql As String = "Select * from SalesMasterTable where SalesId=" & Me.cmbPo.SelectedValue
        '    adp = New OleDbDataAdapter(Sql, Con)
        '    adp.Fill(dt)
        '    'TODO -----
        '    Me.cmbVendor.Value = dt.Rows(0).Item("CustomerCode")
        '    Me.cmbVendor.Enabled = False

        'Else
        '    Me.cmbVendor.Enabled = True
        '    Me.cmbVendor.Rows(0).Activate()
        'End If
    End Sub

    Private Sub cmbItem_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbItem.KeyDown
        Try
            ''TFS1858 : Ayesha Rehman :Item dropdown shall be searchable
            If e.KeyCode = Keys.F1 Then
                If flgCompanyRights = True Then
                    frmItemSearch.CompanyId = MyCompanyId
                End If
                If flgLocationWiseItem = True Then
                    If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                        frmItemSearch.LocationId = Me.cmbCategory.SelectedValue
                    Else
                        frmItemSearch.LocationId = 0
                    End If
                End If

                If Me.RadioButton2.Checked = True Then
                    If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
                    frmItemSearch.VendorId = Me.cmbVendor.Value
                End If
                frmItemSearch.formName = ""
                frmItemSearch.BringToFront()
                frmItemSearch.ShowDialog()
                If frmItemSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbItem.Value = frmItemSearch.ArticleId
                    txtQty.Text = Math.Round(frmItemSearch.Qty, TotalAmountRounding)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    'Private Sub grd_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    With Me.grd.CurrentRow

    '        If Me.grd.CurrentRow.Cells("Unit").Value = "Loose" Then
    '            'txtPackQty.Text = 1
    '            .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value)
    '        Else
    '            .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value) * Val(.Cells("PackQty").Value)
    '        End If
    '        GetTotal()
    '    End With
    'End Sub
    Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        If Me.cmbItem.IsItemInList = False Then
            Me.txtStock.Text = 0
            Exit Sub
        End If
        If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
        'ClearDetailControls()
        If Me.cmbItem.Value > 0 Then
            Me.txtArticleAlias.Enabled = False
        Else
            Me.txtArticleAlias.Enabled = True
            Exit Sub
        End If
        Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
        'Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
        'Me.txtPDP.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString ''TFS2827
        If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
        'Me.cmbVendor.DisplayLayout.Grid.Show( me.cmbVendor.contr)
        Me.txtDisc.TabStop = False

        Dim strSQl As String = String.Empty
        Try
            If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then
                If getConfigValueByType("ApplyFlatDiscountOnSale").ToString = "False" Then
                    strSQl = "select discount from tbldefcustomerbasediscounts where articledefid = " & Me.cmbItem.ActiveRow.Cells(0).Value _
                    & " and typeid = " & Me.cmbVendor.ActiveRow.Cells("TypeId").Value & "  and discount > 0 "
                    Dim dtdiscount As DataTable = GetDataTable(strSQl)
                    If Not dtdiscount Is Nothing Then
                        If dtdiscount.Rows.Count <> 0 Then
                            Dim disc As Double = 0D
                            Double.TryParse(dtdiscount.Rows(0)(0).ToString, disc)

                            Dim price As Double = 0D
                            Double.TryParse(Me.txtRate.Text, price)
                            Me.txtRate.Text = price - ((price / 100) * disc)
                            Me.txtPDP.Text = price - ((price / 100) * disc) ''TFS2827
                            Me.txtDisc.TabStop = False
                        Else
                            If IsSalesOrderAnalysis = True Then
                                Me.txtDisc.TabStop = True
                            End If
                        End If
                    End If
                Else

                    strSQl = "select discount from tbldefcustomerbasediscountsFlat where articledefid = " & Me.cmbItem.ActiveRow.Cells(0).Value _
                                                                & " and typeid = " & Me.cmbVendor.ActiveRow.Cells("typeid").Value & "  and discount > 0 "
                    Dim dtdiscountFlat As DataTable = GetDataTable(strSQl)
                    dtdiscountFlat.AcceptChanges()

                    If Not dtdiscountFlat Is Nothing Then
                        If dtdiscountFlat.Rows.Count <> 0 Then

                            Dim disc As Double = 0D
                            Double.TryParse(dtdiscountFlat.Rows(0)(0).ToString, disc)

                            Dim price As Double = 0D
                            Double.TryParse(Me.txtRate.Text, price)

                            Dim dblDiscountPercent As Double = 0D
                            If disc > 0 Then
                                dblDiscountPercent = (disc / price) * 100
                                If price > 0 Then
                                    Me.txtRate.Text = Math.Round(price - ((price / 100) * dblDiscountPercent), TotalAmountRounding)
                                    Me.txtPDP.Text = Math.Round(price - ((price / 100) * dblDiscountPercent), TotalAmountRounding)
                                    Me.txtDisc.TabStop = False
                                Else
                                    Me.txtRate.Text = Math.Round(Val(Me.txtRate.Text), TotalAmountRounding)
                                    Me.txtPDP.Text = Math.Round(Val(Me.txtPDP.Text), TotalAmountRounding)
                                    Me.txtDisc.TabStop = True
                                End If
                            Else
                                dblDiscountPercent = 0
                                Me.txtDisc.TabStop = True
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Enter
        Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub

    Private Sub cmbVendor_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.cmbVendor.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If

        ''Start TFS3113 : Abubakar Siddiq : 09-04-2018
        If IsEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document is in Approval Process ") : Exit Sub
                End If
            End If
        End If
        ''End TFS3113

        'ValidateDateLock()
        'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
        'If flgDateLock = True Then
        '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
        '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
        '    End If
        'End If
        If IsDateLock(Me.dtpPODate.Value) = True Then
            ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
        End If
        If IsValidToDelete("SalesMasterTable", "PoID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True AndAlso IsValidToDelete("DeliveryChalanMasterTable", "PoID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True AndAlso IsValidToDelete("PlanDetailTable", "SOID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True AndAlso IsValidToDelete("SOItemDeliverySchedule", "SalesOrderId", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True Then

            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

            Try
                'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Dim cm As New OleDbCommand
                Dim objTrans As OleDbTransaction
                If Con.State = ConnectionState.Closed Then Con.Open()
                objTrans = Con.BeginTransaction
                cm.Transaction = objTrans

                cm.Connection = Con
                ''Start TASK-480
                If getConfigValueByType("EnableDuplicateSalesOrder").ToString.ToUpper = "TRUE" Then
                    Call CreateDuplicationSalesOrder(Val(Me.txtReceivingID.Text), "Delete", objTrans)
                End If
                ''End TASK-480

                cm.CommandText = ""
                cm.CommandText = "Update QuotationDetailTable Set SOQuantity=IsNull(SOQuantity,0)-IsNull(SO.SOQty,0), DeliveredTotalQty =IsNull(DeliveredTotalQty,0)-IsNull(SO.TotalQty,0) From QuotationDetailTable,(Select QuotationDetailId, ArticleDefId, SUM(IsNull(Sz1,0)) as SOQty, SUM(IsNull(Qty,0)) as TotalQty From SalesOrderDetailTable WHERE SalesOrderID=" & Val(Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) & " AND IsNull(QuotationDetailId,0) <> 0 Group By QuotationDetailId,ArticleDefId) as SO WHERE SO.QuotationDetailId = QuotationDetailTable.QuotationDetailId And SO.ArticleDefId = QuotationDetailTable.ArticleDefId"
                cm.ExecuteNonQuery()

                cm.CommandText = ""
                cm.CommandText = "Update QuotationMasterTable Set Status='Open' WHERE QuotationId in(Select QuotationId From QuotationDetailTable WHERE IsNull(QuotationDetailId,0) in(Select IsNull(QuotationDetailId,0) From SalesOrderDetailTable WHERE SalesOrderID=" & Val(Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) & "))"
                cm.ExecuteNonQuery()

                cm.CommandText = "delete from SalesOrderDetailTable where SalesOrderid=" & Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString
                cm.Transaction = objTrans
                cm.ExecuteNonQuery()

                cm = New OleDbCommand
                cm.Connection = Con
                cm.CommandText = "delete from SalesOrderMasterTable where SalesOrderid=" & Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString



                cm.Transaction = objTrans
                cm.ExecuteNonQuery()
                objTrans.Commit()

                Me.txtReceivingID.Text = 0

            Catch ex As Exception
                msg_Error("Error occured while deleting record: " & ex.Message)

            Finally
                Con.Close()
                Me.lblProgress.Visible = False
            End Try




            ''insert Activity Log
            SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Sales, grdSaved.CurrentRow.Cells(0).Value.ToString, True)

            Me.grdSaved.CurrentRow.Delete()
            Me.RefreshControls()
        Else
            msg_Error(str_ErrorDependentRecordFound)
        End If

    End Sub

    'Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
    '    ShowReport("SalesOrder", "{SalesOrderMasterTable.SalesOrderId}=" & grdSaved.CurrentRow.Cells("SalesOrderId").Value)

    'End Sub

    'Private Sub grd_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
    '    Me.GetTotal()
    'End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.chkPost.Visible = True
                UpdateDeliveredSO = True
                Me.btnRemoveAttachment.Visible = True ''TFS4652
                Me.btnRemoveAttachment.Enabled = True ''TFS4652
                flgRemoveAttachment = True
                'Me.chkPost.Checked = True
                If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then Me.chkPost.Checked = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmSaleOrder)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                        Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.btnSearchDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                Me.chkPost.Visible = False 'R:M6 Set Visibility off
                Me.chkPost.Checked = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False 'Task:2406 Added Field Chooser Rights
                CtrlGrdBar3.mGridChooseFielder.Enabled = False
                UpdateDeliveredSO = False
                Me.btnRemoveAttachment.Visible = False  ''TFS4652
                Me.btnRemoveAttachment.Enabled = False  ''TFS4652s
                flgRemoveAttachment = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                        Me.btnSearchDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'R:M6 Added Security Rights
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True
                        If (BtnSave.Text = "&Save" Or BtnSave.Text = "Save") Then Me.chkPost.Checked = True
                        'End R:M6
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        CtrlGrdBar3.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update Delivered SO" Then
                        UpdateDeliveredSO = True
                        'End Task:2406
                    ElseIf RightsDt.FormControlName = "Remove Attachments" Then
                        Me.btnRemoveAttachment.Visible = True ''TFS4652
                        Me.btnRemoveAttachment.Enabled = True ''TFS4652
                        flgRemoveAttachment = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try

            ''Task# A2-10-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
            If Me.grdSaved.Row < 0 Then
                Exit Sub
            Else

                Me.cmbVendor.Enabled = True
                Me.txtRemarks.Enabled = True
                EditRecord()
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        ''End Task# A2-10-06-2015
    End Sub

    Private Sub grd_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub
    'Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
    '    If Me.UltraTabControl1.SelectedTab.Index = 0 Then
    '        Me.BtnLoadAll.Visible = False
    '        Me.ToolStripButton1.Visible = False
    '        Me.ToolStripSeparator1.Visible = False
    '    Else
    '        Me.BtnLoadAll.Visible = True
    '        Me.ToolStripButton1.Visible = True
    '        Me.ToolStripSeparator1.Visible = True
    '    End If
    'End Sub
    'Private Sub BtnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoadAll.Click
    '    DisplayRecord("All")
    '    Me.DisplayDetail(-1)
    'End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
        Dim id As Integer = 0

        id = Me.cmbVendor.SelectedRow.Cells(0).Value
        FillCombo("Vendor")
        Me.cmbVendor.Value = id

        id = Me.cmbCategory.SelectedValue
        FillCombo("Category")
        Me.cmbCategory.SelectedValue = id

        id = Me.cmbItem.SelectedRow.Cells(0).Value
        FillCombo("Item")
        Me.cmbItem.Value = id

        id = Me.cmbOrderStatus.SelectedIndex
        FillCombo("OrderStatus")
        Me.cmbOrderStatus.SelectedIndex = id

        id = Me.cmbSalesMan.SelectedIndex
        FillCombo("SM")
        Me.cmbSalesMan.SelectedIndex = id

        id = Me.cmbCurrency.SelectedValue
        FillCombo("Currency")
        Me.cmbCurrency.SelectedValue = id

        FillCombo("grdLocation")
        FillCombo("Colour")
        id = Me.cmbCurrency.SelectedValue
        FillCombo("Currency")
        Me.cmbCurrency.SelectedValue = id

        ''TASK TFS1764
        id = Me.cmbTransporter.SelectedIndex
        FillCombo("Transporter")
        Me.cmbTransporter.SelectedIndex = id
        ''END TASK TFS1764

        GetSalesOrderAnalysis()

        If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            flgCompanyRights = getConfigValueByType("CompanyRights")
        End If

        If Not getConfigValueByType("TransitInssuranceTax").ToString = "Error" Then
            TransitPercent = Val(getConfigValueByType("TransitInssuranceTax"))
        End If
        If Not getConfigValueByType("WHTax_Percentage").ToString = "Error" Then
            WHTaxPercent = Val(getConfigValueByType("WHTax_Percentage"))
        End If


        If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
            flgLoadItems = getConfigValueByType("LoadAllItemsInSales")
        End If

        If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
            flgLocationWiseItem = getConfigValueByType("ArticleFilterByLocation")
        End If


        'Task:M16 Added Flag Vehicle Identification Info
        If Not getConfigValueByType("flgVehicleIdentificationInfo").ToString = "Error" Then
            flgVehicleIdentificationInfo = getConfigValueByType("flgVehicleIdentificationInfo")
        Else
            flgVehicleIdentificationInfo = False
        End If
        'End Task:M16
        ''Start TFS4161
        If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
            IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
        End If
        ''End TFS4161
        ''TASK TFS4544
        If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
        End If
        ''END TFS4544

        If Me.cmbOutwardExpense.ActiveRow.Cells(0).Value Is Nothing Then
            id = 0
        Else
            id = Me.cmbOutwardExpense.ActiveRow.Cells(0).Value
        End If
        FillCombo("OutwardExpense")
        Me.cmbOutwardExpense.Value = id

        Me.lblProgress.Visible = False


    End Sub
    Private Sub LoadAllItems()
        Try
            For Each Row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.cmbItem.Rows
                If Row.Index > 0 Then
                    Row.Selected = True
                    Me.txtQty.Focus()
                    Me.btnAdd_Click(btnAdd, Nothing)
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub LnkLoadAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LnkLoadAll.LinkClicked
        Try
            Me.LoadAllItems()
            'Me.Panel1.Visible = False
            Me.LnkLoadAll.Enabled = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub grd_CellEndEdit1(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '    With Me.grd.CurrentRow

    '        If Me.grd.CurrentRow.Cells("Unit").Value = "Loose" Then
    '            'txtPackQty.Text = 1
    '            .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value)
    '        Else
    '            .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value) * Val(.Cells("PackQty").Value)
    '        End If
    '        GetTotal()
    '    End With
    'End Sub
    Private Sub txtDisc_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDisc.Leave
        Try
            If cmbDiscountType.Text = DiscountType_Percentage Then
                If Not (Val(txtDisc.Text) >= 0 AndAlso Val(txtDisc.Text) <= 100) Then
                    ShowErrorMessage("Enter value according to the discount Type")
                    txtDisc.SelectAll()
                    Me.txtDisc.Focus()
                End If

            ElseIf cmbDiscountType.Text = DiscountType_Flat Then
                If Not (Val(txtDisc.Text) >= 0 AndAlso Val(txtDisc.Text) <= Val(txtPDP.Text)) Then
                    ShowErrorMessage("Enter value according to the discount Type")
                    txtDisc.SelectAll()
                    Me.txtDisc.Focus()
                End If
            End If
            'Dim disc As Double = 0D
            'Double.TryParse(Me.txtDisc.Text.Trim, disc)
            'Dim price As Double = 0D
            'Double.TryParse(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString, price)
            ''If Val(Me.txtPackRate.Text) = 0 Then
            'If Val(disc) <> 0 AndAlso Val(price) <> 0 Then
            '    Me.txtRate.Text = price - ((price / 100) * disc)
            'Else
            '    Me.txtRate.Text = Me.txtRate.Text
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillComboByEdit()
        ''R-916 Commented Code
        'Try
        '    FillCombo("Vendor")
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub
    Private Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                'Before against task:M16
                'If col.Index <> GrdEnum.LocationId AndAlso col.Index <> GrdEnum.Qty AndAlso col.Index <> GrdEnum.Rate AndAlso col.Index <> GrdEnum.TradePrice AndAlso col.Index <> GrdEnum.SalesTax_Percentage AndAlso col.Index <> GrdEnum.SchemeQty AndAlso col.Index <> GrdEnum.Discount_Percentage AndAlso col.Index <> GrdEnum.Freight AndAlso col.Index <> GrdEnum.MarketReturns AndAlso col.Index <> GrdEnum.Comments Then
                'Task:M16 Set Editable Fields Engine_No And Chassis_No
                If col.Index <> GrdEnum.LocationId AndAlso col.Index <> GrdEnum.Qty AndAlso col.Index <> GrdEnum.TotalQuantity AndAlso col.Index <> GrdEnum.Rate AndAlso col.Index <> GrdEnum.TradePrice AndAlso col.Index <> GrdEnum.SalesTax_Percentage AndAlso col.Index <> GrdEnum.SchemeQty AndAlso col.Index <> GrdEnum.Discount_Percentage AndAlso col.Index <> GrdEnum.Freight AndAlso col.Index <> GrdEnum.MarketReturns AndAlso col.Index <> GrdEnum.Comments AndAlso col.Index <> GrdEnum.Engine_No AndAlso col.Index <> GrdEnum.Chassis_No AndAlso col.Index <> GrdEnum.OtherComments AndAlso col.Index <> GrdEnum.SED_Tax_Percent AndAlso col.Index <> GrdEnum.ArticleAlias AndAlso col.Index <> GrdEnum.SaleOrderType AndAlso col.Index <> GrdEnum.CurrencyAmount AndAlso col.Index <> GrdEnum.DiscountId AndAlso col.Index <> GrdEnum.SerialNo Then
                    'End Task:M16
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            If IsPackQtyDisabled = True Then
                Me.grd.RootTable.Columns(GrdEnum.TotalQuantity).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Else
                Me.grd.RootTable.Columns(GrdEnum.TotalQuantity).EditType = Janus.Windows.GridEX.EditType.TextBox
            End If
            If flgLoadItems = False Then
                Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("Unit").Index
                Me.grd.RootTable.Columns("Unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index
                Me.grd.RootTable.Columns("Pack_Desc").Visible = True
                Me.grd.RootTable.Columns("Unit").Visible = False
            Else
                Me.grd.RootTable.Columns("Pack_Desc").Visible = False
                Me.grd.RootTable.Columns("Unit").Visible = True
            End If
            Me.grd.RootTable.Columns("Tax Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Tax Amount").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Tax Amount").TotalFormatString = "N" & TotalAmountRounding
            'Task:M16 Engine_No And Chassis_No Column Enable/Disabled
            If flgVehicleIdentificationInfo = True Then
                Me.grd.RootTable.Columns("Engine_No").Visible = True
                Me.grd.RootTable.Columns("Chassis_No").Visible = True
            Else
                Me.grd.RootTable.Columns("Engine_No").Visible = False
                Me.grd.RootTable.Columns("Chassis_No").Visible = False
            End If
            'End Task:M16
            Me.grd.RootTable.Columns(GrdEnum.DiscountId).Caption = "Discount Type" ''TFS2827
            Me.grd.RootTable.Columns(GrdEnum.DiscountFactor).Caption = "Discount Factor" ''TFS2827
            Me.grd.RootTable.Columns(GrdEnum.DiscountValue).Caption = "Discount value" ''TFS2827
            Me.grd.RootTable.Columns(GrdEnum.PostDiscountPrice).Caption = "PDP" ''TFS2827
            Me.grd.RootTable.Columns(GrdEnum.DiscountValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS2827
            Me.grd.RootTable.Columns(GrdEnum.DiscountFactor).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS2827
            Me.grd.RootTable.Columns(GrdEnum.PostDiscountPrice).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS2827
            Me.grd.RootTable.Columns(GrdEnum.DiscountValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS2827
            Me.grd.RootTable.Columns(GrdEnum.DiscountFactor).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS2827
            Me.grd.RootTable.Columns(GrdEnum.PostDiscountPrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS2827
            Me.grd.RootTable.Columns(GrdEnum.DiscountValue).EditType = Janus.Windows.GridEX.EditType.NoEdit 'TFS2827
            Me.grd.RootTable.Columns(GrdEnum.DiscountFactor).EditType = Janus.Windows.GridEX.EditType.TextBox 'TFS2827
            Me.grd.RootTable.Columns(GrdEnum.PostDiscountPrice).EditType = Janus.Windows.GridEX.EditType.TextBox  'TFS2827
            Me.grd.RootTable.Columns(GrdEnum.Rate).EditType = Janus.Windows.GridEX.EditType.NoEdit 'TFS2827
            'Task:2759 Set Rounded Amount Format
            Me.grd.RootTable.Columns(GrdEnum.Total).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(GrdEnum.Total).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.Total).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.grd.RootTable.Columns("PostDiscountPrice").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("PostDiscountPrice").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("PostDiscountPrice").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns("DiscountValue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("DiscountValue").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("DiscountValue").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns("CurrencyRate").FormatString = "N" & 4
            Me.grd.RootTable.Columns("CurrencyRate").FormatString = "N" & 4
            Me.grd.RootTable.Columns("CurrencyRate").TotalFormatString = "N" & 4

            Me.grd.RootTable.Columns("CurrencyAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("CurrencyAmount").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("CurrencyAmount").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns("TotalCurrencyAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("TotalCurrencyAmount").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("TotalCurrencyAmount").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns(GrdEnum.Rate).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(GrdEnum.Rate).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.Rate).TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.grd.RootTable.Columns(GrdEnum.BillValueAfterDiscount).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(GrdEnum.BillValueAfterDiscount).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.BillValueAfterDiscount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            'End Task:2759
            Me.grd.RootTable.Columns(GrdEnum.DeliveredQty).Visible = False
            'grd.AutoSizeColumns()
            Me.grd.RootTable.Columns("PlanedQty").Visible = False 'Task:2832 PlanedQty Field Hidden
            'Dim bln As Boolean = Convert.ToBoolean(getConfigValueByType("GridFreezColumn").Replace("Error", "False").Replace("''", "False"))
            'If bln = True Then
            '    Me.grd.FrozenColumns = Me.grd.RootTable.Columns("Size").Index
            'Else
            '    Me.grd.FrozenColumns = 0
            'End If

            Dim StockOutConfigration As String = "" ''1596
            If Not getConfigValueByType("StockOutConfigration").ToString = "Error" Then ''1596
                StockOutConfigration = getConfigValueByType("StockOutConfigration").ToString
            End If
            'ShowInformationMessage(StockInConfigration)
            If StockOutConfigration.Equals("Disabled") Then
                Me.grd.RootTable.Columns(GrdEnum.BatchNo).Visible = False
                Me.grd.RootTable.Columns(GrdEnum.ExpiryDate).Visible = False
                Me.grd.RootTable.Columns(GrdEnum.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.NoEdit
                Me.grd.RootTable.Columns(GrdEnum.BatchNo).EditType = Janus.Windows.GridEX.EditType.NoEdit
            ElseIf StockOutConfigration.Equals("Enabled") Then
                Me.grd.RootTable.Columns(GrdEnum.BatchNo).Visible = True
                Me.grd.RootTable.Columns(GrdEnum.ExpiryDate).Visible = True
                Me.grd.RootTable.Columns(GrdEnum.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
                Me.grd.RootTable.Columns(GrdEnum.BatchNo).HasValueList = True
                Me.grd.RootTable.Columns(GrdEnum.BatchNo).LimitToList = False
                Me.grd.RootTable.Columns(GrdEnum.BatchNo).EditType = Janus.Windows.GridEX.EditType.Combo
            Else
                Me.grd.RootTable.Columns(GrdEnum.BatchNo).Visible = True
                Me.grd.RootTable.Columns(GrdEnum.ExpiryDate).Visible = True
                Me.grd.RootTable.Columns(GrdEnum.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
                Me.grd.RootTable.Columns(GrdEnum.BatchNo).HasValueList = True
                Me.grd.RootTable.Columns(GrdEnum.BatchNo).LimitToList = False
                Me.grd.RootTable.Columns(GrdEnum.BatchNo).EditType = Janus.Windows.GridEX.EditType.Combo
            End If

            Me.grd.RootTable.Columns(GrdEnum.ExpiryDate).FormatString = str_DisplayDateFormat

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                If IsEditMode = True Then
                    'If IsValidToDelete("SalesMasterTable", "PoID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True AndAlso IsValidToDelete("DeliveryChalanMasterTable", "POId", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True AndAlso IsValidToDelete("PlanDetailTable", "SOID", Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) = True Then
                    If Val(Me.grd.CurrentRow.Cells(GrdEnum.DeliveredQty).Value.ToString) = Val(0) Then
                        Dim str As String = ""
                        Dim dt As DataTable
                        str = "SELECT DeliveredQty FROM SalesOrderDetailTable WHERE SalesOrderDetailId = " & Val(Me.grd.CurrentRow.Cells(GrdEnum.SalesOrderDetailId).Value) & " AND DeliveredQty IS NOT NULL"
                        dt = GetDataTable(str)
                        If dt.Rows.Count > 0 Then
                            DeleteDetailRow(Val(Me.grd.GetRow.Cells("SalesOrderDetailId").Value.ToString))
                            Me.grd.GetRow.Delete()
                            grd.UpdateData()
                            GetTotal()
                        End If
                    Else
                        msg_Error(str_ErrorDependentRecordFound)
                    End If
                Else
                    Me.grd.GetRow.Delete()
                    grd.UpdateData()
                    GetTotal()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_RowSelected(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            End If
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            If IsSalesOrderAnalysis = True Then
                Dim dt As DataTable = GetCostManagement(Me.cmbItem.Value)
                If dt IsNot Nothing Then
                    TradePrice = dt.Rows(0).Item("TradePrice")
                    Freight_Rate = dt.Rows(0).Item("Freight")
                    MarketReturns_Rate = dt.Rows(0).Item("MarketReturns")
                    GST_Applicable = dt.Rows(0).Item("Gst_Applicable")
                    FlatRate_Applicable = dt.Rows(0).Item("FlatRate_Applicable")
                    FlatRate = dt.Rows(0).Item("FlatRate")
                End If
                Dim dtDiscount As DataTable = GetAnalysisLastDiscount(Me.cmbVendor.Value, Me.cmbItem.Value)
                If dtDiscount IsNot Nothing Then
                    If dtDiscount.Rows.Count > 0 Then
                        Me.txtDisc.Text = Math.Round(dtDiscount.Rows(0).Item(0), TotalAmountRounding)
                    Else
                        Me.txtDisc.Text = 0
                    End If
                Else
                    Me.txtDisc.Text = 0
                End If
                'Dim dtSchemeQty As DataTable = GetAnalysisLastSchemeQty(Me.cmbVendor.Value, Me.cmbItem.Value)
                'If dtSchemeQty IsNot Nothing Then
                '    If dtSchemeQty.Rows.Count > 0 Then
                '        Me.txtSchemeQty.Text = dtSchemeQty.Rows(0).Item(0)
                '    Else
                '        Me.txtSchemeQty.Text = 0
                '    End If
                'Else
                '    Me.txtSchemeQty.Text = 0
                'End If
            Else
                Me.txtDisc.Text = Math.Round(GetLastDiscount(IIf(Me.cmbVendor.IsItemInList = True, Me.cmbVendor.Value, 0), Me.cmbItem.Value), TotalAmountRounding)
            End If
            If IsFormOpen = True Then FillCombo("ArticlePack")
            Me.txtLastPrice.Text = Math.Round(LastPrice(Me.cmbVendor.Value, Me.cmbItem.Value), TotalAmountRounding) 'Task:2445 Call function Last Price and implement
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoName.CheckedChanged, rdoCode.CheckedChanged
        Try
            If Not IsFormOpen = True Then Exit Sub
            If Me.rdoCode.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                ''Return GetSerialNo("SO" + "" & Me.cmbCompany.SelectedValue & "" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "SalesOrderMasterTable", "SalesOrderNo")
                'rafay:task start
                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    Return GetSerialNo("SO1" & "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "SalesOrderMasterTable", "SalesOrderNo")
                Else
                    ''companyinitials = "PK"
                    Return GetNextDocNo("SO" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "SalesOrderMasterTable", "SalesOrderNo")
                End If
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                ''Return GetNextDocNo("SO" & Me.cmbCompany.SelectedValue & "-" & CompanyPrefix & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "SalesOrderMasterTable", "SalesOrderNo")

                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    Return GetSerialNo("SO1" & "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "SalesOrderMasterTable", "SalesOrderNo")
                Else
                    ''companyinitials = "PK"
                    Return GetNextDocNo("SO" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "SalesOrderMasterTable", "SalesOrderNo")
                End If
            Else
                Return GetNextDocNo("SO" + "" & Me.cmbCompany.SelectedValue & "", 6, "SalesOrderMasterTable", "SalesOrderNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnAddNewItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewItem.Click
        Call frmAddItem.ShowDialog()
        Call FillCombo("Item")
    End Sub
    Private Sub cmbVendor_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.ValueChanged
        Try
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            If getConfigValueByType("LoadAllItemsInSales").ToString = "True" Then
                Dim Str As String = "Select CustomerTypes From tblCustomer WHERE AccountId=" & Me.cmbVendor.Value
                Dim dt1 As DataTable = GetDataTable(Str)
                If Not dt1 Is Nothing Then
                    If dt1.Rows.Count > 0 Then
                        Me.DisplayDetail(-1, dt1.Rows(0).Item(0), "All")
                    Else
                        Me.DisplayDetail(-1, -1, "All")
                    End If
                End If
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    If Me.grd.RowCount > 0 Then
                        r.BeginEdit()
                        r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
                        r.EndEdit()
                    End If
                Next
            Else
                'Me.DisplayDetail(-1)
            End If
            CtrlGrdBar1.Email = New SBModel.SendingEmail
            CtrlGrdBar1.Email.ToEmail = Me.cmbVendor.ActiveRow.Cells("Email").Text
            CtrlGrdBar1.Email.Subject = "Sales Order:" + "(" & Me.txtPONo.Text & ")"
            CtrlGrdBar1.Email.Body = String.Empty
            CtrlGrdBar1.Email.DocumentNo = Me.txtPONo.Text
            CtrlGrdBar1.Email.DocumentDate = Me.dtpPODate.Value

            CtrlGrdBar3.Email = New SBModel.SendingEmail
            CtrlGrdBar3.Email.ToEmail = Me.cmbVendor.ActiveRow.Cells("Email").Text
            CtrlGrdBar3.Email.Subject = "Sales Order:" + "(" & Me.txtPONo.Text & ")"
            CtrlGrdBar3.Email.Body = String.Empty
            CtrlGrdBar3.Email.DocumentNo = Me.txtPONo.Text
            CtrlGrdBar3.Email.DocumentDate = Me.dtpPODate.Value

            Dim dt As DataTable = GetDataTable("Select ISNULL(SpecialAdjustment,0) as SpecialAdjustment From SalesOrderMasterTable WHERE SalesOrderId in (Select Max(SalesOrderId) From SalesOrderMasterTable WHERE VendorId=" & Me.cmbVendor.Value & " And SpecialAdjustment <> 0)")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Me.txtSpecialAdjustment.Text = dt.Rows(0).Item(0)
                Else
                    Me.txtSpecialAdjustment.Text = 0
                End If
            End If

            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            Me.txtLastPrice.Text = Math.Round(LastPrice(Me.cmbVendor.Value, Me.cmbItem.Value), TotalAmountRounding)
            FillCombo("DispatchToLocation")
            FillCombo("InvoiceToLocation")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbCompany_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCompany.SelectedIndexChanged
        Try
            If Not IsEditMode = True AndAlso IsFormOpen = True Then Me.RefreshControls()
            'Task:2427 CostCenter Selected Company Wise
            If Not Me.cmbCompany.SelectedIndex = -1 Then
                If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedValue = Val(CType(Me.cmbCompany.SelectedItem, DataRowView).Row.Item("CostCenterId").ToString)
            End If
            'End Task:2427
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub OrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrderToolStripMenuItem.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("SalesOrderNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            ShowReport("SalesOrder", "{SalesOrderMasterTable.SalesOrderId}=" & grdSaved.CurrentRow.Cells("SalesOrderId").Value, , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub QuotationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuotationToolStripMenuItem.Click
        Try
            Dim IsPreviewSaleInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
            Dim newinvoice As Boolean = False
            Dim strCriteria As String = "Nothing"
            Me.Cursor = Cursors.WaitCursor
            Try
                If Me.grdSaved.RowCount = 0 Then Exit Sub
                newinvoice = getConfigValueByType("NewInvoice")
                If newinvoice = True Then
                    str_ReportParam = "@SalesOrderId|" & grdSaved.CurrentRow.Cells("SalesOrderId").Value
                Else

                    str_ReportParam = String.Empty
                    strCriteria = "{SalesOrderDetailTable.SalesOrderId} = " & grdSaved.CurrentRow.Cells("SalesOrderId").Value
                End If

                If IsPreviewSaleInvoice = False Then
                    ShowReport(IIf(newinvoice = False, "SalesOrder", "SalesOrderNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", True, , "New", , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
                Else
                    ShowReport(IIf(newinvoice = False, "SalesOrder", "SalesOrderNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", False, , "New", , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                Me.Cursor = Cursors.Default
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Private Function EmailSave()
        EmailSave = Nothing
        Dim flg As Boolean = False
        If Me.cmbVendor.ActiveRow Is Nothing Then Exit Function

        If IsEmailAlert = True Then
            Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name='frmSaleOrder' AND EmailAlert=1")
            If dtForm.Rows.Count > 0 Then
                flg = True
            Else
                flg = False
            End If
            If flg = True Then
                Email = New Email
                Email.ToEmail = AdminEmail
                Email.CCEmail = String.Empty
                Email.BccEmail = Me.cmbVendor.ActiveRow.Cells("Email").Text.ToString
                Email.Attachment = SourceFile
                Email.Subject = "Sales Order " & setVoucherNo & ""
                Email.Body = "Sales Order " _
                & " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Previouse_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
                Email.Status = "Pending"
                Call New MailSentDAL().Add(Email)
            End If
        End If
        Return EmailSave

    End Function
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            DisplayRecord("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
    '    Try
    '        If Me.SplitContainer1.Panel1Collapsed = True Then
    '            Me.SplitContainer1.Panel1Collapsed = False
    '        Else
    '            Me.SplitContainer1.Panel1Collapsed = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Public Function Get_All(ByVal SalesOrderNo As String)
        Try
            Get_All = Nothing
            If IsFormOpen = True Then
                If SalesOrderNo.Length > 0 Then
                    If Me.grdSaved.RowCount <= 50 Then
                        blnDisplayAll = True
                        Me.btnSearchLoadAll_Click(Nothing, Nothing)
                        blnDisplayAll = False
                    End If
                    Dim flag As Boolean = False
                    flag = Me.grdSaved.FindAll(Me.grdSaved.RootTable.Columns("SalesOrderNo"), Janus.Windows.GridEX.ConditionOperator.Equal, SalesOrderNo)
                    If flag = True Then
                        Me.grdSaved_DoubleClick(Nothing, Nothing)
                    Else
                        Exit Function
                    End If
                End If
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSearchLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchLoadAll.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            DisplayRecord("All")
            Me.DisplayDetail(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnSearchDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDocument.Click
        Try
            If Not Me.cmbSearchAccount.IsItemInList Then
                FillCombo("SearchVendor")
                Me.cmbSearchAccount.Rows(0).Activate()
            Else
                Me.cmbSearchAccount.Rows(0).Activate()
            End If
            If Not Me.cmbSearchLocation.Items.Count > 0 Then
                FillCombo("SearchLocation")
                If Not Me.cmbSearchLocation.SelectedIndex = -1 Then Me.cmbSearchLocation.SelectedIndex = 0
            Else
                If Not Me.cmbSearchLocation.SelectedIndex = -1 Then Me.cmbSearchLocation.SelectedIndex = 0
            End If

            If Me.SplitContainer1.Panel1Collapsed = True Then
                Me.SplitContainer1.Panel1Collapsed = False
            Else
                Me.SplitContainer1.Panel1Collapsed = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSearchDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDelete.Click
        Try
            DeleteToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSearchEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchEdit.Click
        Try
            OpenToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub OrderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrderToolStripMenuItem1.Click
        Try
            OrderToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub QuotationToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuotationToolStripMenuItem1.Click
        Try
            QuotationToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                DisplayRecord()
                CtrlGrdBar1.Visible = False
                CtrlGrdBar3.Visible = False
                CtrlGrdBar2.Visible = True
            Else
                ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                If IsEditMode = False Then Me.BtnDelete.Visible = False
                If IsEditMode = False Then Me.BtnPrint.Visible = False
                Me.BtnEdit.Visible = False
                CtrlGrdBar1.Visible = True
                CtrlGrdBar3.Visible = True
                CtrlGrdBar2.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetSalesOrderAnalysis() As Boolean
        Try
            IsSalesOrderAnalysis = Convert.ToBoolean(getConfigValueByType("SalesOrderAnalysis").ToString)
            If IsSalesOrderAnalysis = True Then
                'Me.grd.RootTable.Columns("TradePrice").Visible = True
                'Me.grd.RootTable.Columns("SalesTax_Percentage").Visible = True
                Me.grd.RootTable.Columns("SchemeQty").Visible = True
                Me.grd.RootTable.Columns("Freight").Visible = True
                'Me.grd.RootTable.Columns("Discount_Percentage").Visible = True
                Me.grd.RootTable.Columns("BillValueAfterDiscount").Visible = True
                Me.grd.RootTable.Columns("MarketReturns").Visible = True
                Me.rbtAdjPercentage.Checked = True
                Me.rbtAdjFlat.Enabled = False
            Else
                'Me.grd.RootTable.Columns("TradePrice").Visible = False
                'Me.grd.RootTable.Columns("SalesTax_Percentage").Visible = False
                Me.grd.RootTable.Columns("SchemeQty").Visible = False
                Me.grd.RootTable.Columns("Freight").Visible = False
                'Me.grd.RootTable.Columns("Discount_Percentage").Visible = False
                Me.grd.RootTable.Columns("BillValueAfterDiscount").Visible = False
                Me.grd.RootTable.Columns("MarketReturns").Visible = False
                Me.rbtAdjPercentage.Enabled = True
                Me.rbtAdjFlat.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAnalysisLastTradePrice(ByVal CustomerCode As Integer, ByVal ItemId As Integer, ByVal Unit As String) As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select ISNULL(Max(b.TradePrice),0) as TradePrice From SalesOrderDetailTable b INNER JOIN SalesOrderMasterTable a ON a.SalesOrderId = b.SalesOrderId WHERE a.VendorId=" & CustomerCode & " AND b.ArticleDefId=" & ItemId & " AND ArticleSize =N'" & Unit & "'"
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAnalysisLastSchemeQty(ByVal CustomerCode As Integer, ByVal ItemId As Integer) As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select b.SchemeQty From SalesOrderDetailTable b INNER JOIN SalesOrderMasterTable a ON a.SalesOrderId = b.SalesOrderId WHERE SalesOrderDetailId In (Select Max(SalesOrderDetailId) From SalesOrderDetailTable WHERE (SchemeQty Is Not Null Or SchemeQty <> 0) Group By ArticleDefId) And a.VendorId=" & CustomerCode & " AND b.ArticleDefId=" & ItemId & ""
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAnalysisLastDiscount(ByVal CustomerCode As Integer, ByVal ItemId As Integer) As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select b.Discount_Percentage From SalesOrderDetailTable b INNER JOIN SalesOrderMasterTable a ON a.SalesOrderId = b.SalesOrderId WHERE SalesOrderDetailId In (Select Max(SalesOrderDetailId) From SalesOrderDetailTable WHERE (Discount_Percentage Is Not Null Or Discount_Percentage <> 0) Group By ArticleDefId) And a.VendorId=" & CustomerCode & " AND b.ArticleDefId=" & ItemId & ""
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub SalesOrderAnalysisToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesOrderAnalysisToolStripMenuItem2.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@SalesOrderId", Me.grdSaved.GetRow.Cells("SalesOrderId").Value)
            ShowReport("rptSalesOrderAnalysis", , , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage("Error occured while showing report: " & ex.Message)
        End Try
    End Sub

    Private Sub PurchaseOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseOrderToolStripMenuItem.Click
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        ShowReport("SalesOrder", "{SalesOrderMasterTable.SalesOrderId}=" & grdSaved.CurrentRow.Cells("SalesOrderId").Value, , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
    End Sub
    Private Sub PuToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PuToolStripMenuItem.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@SalesOrderId", Me.grdSaved.GetRow.Cells("SalesOrderId").Value)
            ShowReport("rptPurchaseOrder", , , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage("Error occured while showing report: " & ex.Message)
        End Try
    End Sub

    Private Sub ProductionPlaningToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductionPlaningToolStripMenuItem.Click
        Try
            frmMain.LoadControl("ProductionPlan")
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ExportFile(ByVal VoucherId As Integer)
        Try
            If IsEmailAlert = True Then
                If IsAttachmentFile = True Then
                    crpt = New ReportDocument
                    If IO.File.Exists(str_ApplicationStartUpPath & "\Reports\SalesOrder.rpt") = False Then Exit Sub
                    crpt.Load(str_ApplicationStartUpPath & "\Reports\SalesOrder.rpt", DBServerName)
                    If DBUserName <> "" Then
                        crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, DBUserName, DBPassword)
                        crpt.DataSourceConnections.Item(0).SetLogon(DBName, DBPassword)
                    Else
                        crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, True)
                    End If

                    Dim ConnectionInfo As New ConnectionInfo
                    With ConnectionInfo
                        .ServerName = DBServerName
                        .DatabaseName = DBName
                        If DBUserName <> "" Then
                            .UserID = DBUserName
                            .Password = DBPassword
                            .IntegratedSecurity = False
                        Else
                            .IntegratedSecurity = True
                        End If
                    End With
                    Dim tbLogOnInfo As New TableLogOnInfo
                    For Each dt As Table In crpt.Database.Tables
                        tbLogOnInfo = dt.LogOnInfo
                        tbLogOnInfo.ConnectionInfo = ConnectionInfo
                        dt.ApplyLogOnInfo(tbLogOnInfo)
                    Next
                    crpt.RecordSelectionFormula = "{SalesOrderMasterTable.SalesOrderId}=" & VoucherId



                    Dim crExportOps As New ExportOptions
                    Dim crDiskOps As New DiskFileDestinationOptions
                    Dim crExportType As New PdfRtfWordFormatOptions


                    If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments\") Then
                        IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments\")
                    Else
                    End If
                    FileName = String.Empty
                    FileName = "Sales Order" & "-" & setVoucherNo & ""
                    SourceFile = String.Empty
                    SourceFile = _FileExportPath & "\" & FileName & ".pdf"
                    crDiskOps.DiskFileName = SourceFile
                    crExportOps = crpt.ExportOptions
                    With crExportOps
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                        .ExportDestinationOptions = crDiskOps
                        .ExportFormatOptions = crExportType
                    End With
                    crpt.Refresh()
                    Try
                        crpt.SetParameterValue("@CompanyName", CompanyTitle)
                        crpt.SetParameterValue("@CompanyAddress", CompanyAddHeader)
                        crpt.SetParameterValue("@ShowHeader", IsCompanyInfo)
                    Catch ex As Exception
                        'IsCompanyInfo = False
                        'CompanyTitle = String.Empty
                        'CompanyAddHeader = String.Empty
                    End Try
                    crpt.Export(crExportOps)

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            EmailSave()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            ExportFile(getVoucher_Id)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SalesOrderAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesOrderAnalysisToolStripMenuItem.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@SalesOrderId", Me.grdSaved.GetRow.Cells("SalesOrderId").Value)
            ShowReport("rptSalesOrderAnalysis", , , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage("Error occured while showing report: " & ex.Message)
        End Try
    End Sub

    Private Sub PurchaseOrderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseOrderToolStripMenuItem1.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@SalesOrderId", Me.grdSaved.GetRow.Cells("SalesOrderId").Value)
            ShowReport("rptPurchaseOrder", , , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage("Error occured while showing report: " & ex.Message)
        End Try
    End Sub
    Private Sub DispatchAdviceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DispatchAdviceToolStripMenuItem.Click
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        ShowReport("SalesOrder", "{SalesOrderMasterTable.SalesOrderId}=" & grdSaved.CurrentRow.Cells("SalesOrderId").Value, , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
    End Sub

    Private Sub DispatchesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DispatchesToolStripMenuItem.Click
        Try
            frmMain.LoadControl("ProductionPlan")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grd_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            Dim DiscountFactor As Double = Me.grd.GetRow.Cells(GrdEnum.DiscountFactor).Value
            Dim DiscountType As Decimal = Me.grd.GetRow.Cells(GrdEnum.DiscountId).Value
            Dim PostDiscountPrice As Decimal = Me.grd.GetRow.Cells(GrdEnum.PostDiscountPrice).Value
            If DiscountFactor <> 0 Then
                If DiscountType = 1 Then
                    If Not (DiscountFactor >= 0 AndAlso DiscountFactor <= 100) Then
                        ShowErrorMessage("Enter value according to the discount Type")
                        grd.CancelCurrentEdit()
                    End If

                ElseIf DiscountType = 2 Then
                    If Not (DiscountFactor >= 0 AndAlso DiscountFactor <= PostDiscountPrice) Then
                        ShowErrorMessage("Enter value according to the discount Type")
                        grd.CancelCurrentEdit()
                    End If
                End If
            End If
            ''Start TFS-3101
            Dim orderQty As Double = Me.grd.GetRow.Cells(GrdEnum.Qty).Value

            Dim DeliveredQty As Double = IIf(IsDBNull(Me.grd.GetRow.Cells(GrdEnum.DeliveredQty).Value), 0, Me.grd.GetRow.Cells(GrdEnum.DeliveredQty).Value)

            If e.Column.Index = GrdEnum.Qty Then
                If orderQty < DeliveredQty Then
                    ShowErrorMessage("Order Quantity can not be less then Delivered quantity")
                    grd.CancelCurrentEdit()
                End If

            End If
            ''End TFS3101

            If grd.RootTable IsNot Nothing Then Me.grd.UpdateData()
            Me.GetGridDetailQtyCalculate(e)
            Me.GetDetailTotal()
            Me.GetTotal()
            'Ali Faisal : TFS1492 : Set currency rate to 1 if base currency in PKR
            If Val(Me.grd.GetRow.Cells(GrdEnum.BaseCurrencyId).Value) = Val(1) Then
                Me.grd.GetRow.Cells(GrdEnum.CurrencyRate).Value = 1
            End If
            'Ali Faisal : TFS1492 : End

            If Not IsDBNull(Me.grd.GetRow.Cells(GrdEnum.BatchNo).Value) Then
                Dim str As String = String.Empty
                str = " Select   ExpiryDate  From  StockDetailTable  where BatchNo not in ('','0','xxxx') And BatchNo ='" & Me.grd.GetRow.Cells(GrdEnum.BatchNo).Value.ToString & "'" _
                     & " And ArticledefId = " & Me.grd.GetRow.Cells(GrdEnum.ItemId).Value & "  And (isnull(InQty, 0) - isnull(OutQty, 0)) > 0 Group by BatchNo,ExpiryDate ORDER BY ExpiryDate  Asc "
                Dim dtExpiry As DataTable = GetDataTable(str)
                If dtExpiry.Rows.Count > 0 Then
                    If IsDBNull(dtExpiry.Rows(0).Item("ExpiryDate")) = False Then
                        grd.GetRow.Cells(GrdEnum.ExpiryDate).Value = CType(dtExpiry.Rows(0).Item("ExpiryDate").ToString, Date)
                    End If
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.RecordsDeleted
        Try
            If grd.RootTable IsNot Nothing Then Me.grd.UpdateData()
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtSpecialAdjustment_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If grd.RootTable IsNot Nothing Then Me.grd.UpdateData()
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetLastDiscount(ByVal CustomerCode As Integer, ByVal ArticleId As Integer) As Double
        Try

            Dim str As String = "Select ISNULL(Max(Discount_Percentage),0) From SalesDetailTable INNER JOIN SalesMasterTable On SalesDetailTable.SalesId = SalesDetailTable.SalesId WHERE CustomerCode=" & CustomerCode & " AND ArticleDefId=" & ArticleId & ""
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return Val(dt.Rows(0).Item(0).ToString)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtTax_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            'If Val(Me.txtPackQty.Text) = 0 Or Val(Me.txtPackQty.Text) = 1 Then
            '    Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + ((Val(Me.txtTotal.Text) * Val(Me.txtTax.Text)) / 100)
            'Else
            '    Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + ((Val(Me.txtPackQty.Text) * Val(Me.txtTotal.Text) * Val(Me.txtTax.Text)) / 100)
            'End If
            ''''''''''''''''''''''''''''''''''
            Me.GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTax_LostFocus1(sender As Object, e As EventArgs) Handles txtTax.LostFocus
        Try

            'If Val(Me.txtPackQty.Text) = 0 Or Val(Me.txtPackQty.Text) = 1 Then
            '    Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + ((Val(Me.txtTotal.Text) * Val(Me.txtTax.Text)) / 100)
            'Else
            '    Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + ((Val(Me.txtPackQty.Text) * Val(Me.txtTotal.Text) * Val(Me.txtTax.Text)) / 100)
            'End If
            ''''''''''''''''''''''''''''''''''
            Me.GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTax.TextChanged

    End Sub

    Private Sub cmbVendor_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbVendor.InitializeLayout

    End Sub

    Private Sub PrintAgreementLatterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintAgreementLatterToolStripMenuItem.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@SalesOrderId", grdSaved.CurrentRow.Cells("SalesOrderId").Value)
            ShowReport("rptAgreementLatter", , , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintAgreementLatterToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintAgreementLatterToolStripMenuItem1.Click
        Try
            PrintAgreementLatterToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rbtAdjFlat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAdjPercentage.CheckedChanged, rbtAdjFlat.CheckedChanged
        Try
            If IsFormOpen = True Then GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") Then
                If DateLock.Lock = True Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Sub ValidateDateLock()
        Try
            Dim dateLock As New SBModel.DateLockBE
            dateLock = DateLockList.Find(AddressOf chkDateLock)
            If dateLock IsNot Nothing Then
                If dateLock.DateLock.ToString.Length > 0 Or Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") < dateLock.DateLock.ToString("yyyy-M-d 00:00:00") Then
                    flgDateLock = True
                Else
                    flgDateLock = False
                End If
            Else
                flgDateLock = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtpPDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpPDate.ValueChanged, dtpDeliveryDate.ValueChanged

    End Sub

    Private Sub PrintSalesOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSalesOrderToolStripMenuItem.Click
        Try
            AddRptParam("@SalesOrderId", Me.grdSaved.GetRow.Cells("SalesOrderId").Value)
            ShowReport("rptSalesOrder", , , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintSalesOrderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSalesOrderToolStripMenuItem1.Click
        Try
            PrintSalesOrderToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pendingCustomerList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pendingCustomerList.Click
        Try
            Dim myForm As New frmPendingCustomerList
            ApplyStyleSheet(myForm)
            If myForm.ShowDialog = Windows.Forms.DialogResult.Yes Then
                Me.cmbVendor.Value = myForm.CustomerId
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow

    End Sub

    Private Sub txtPackRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Val(Me.txtPackQty.Text) = 0 Then
                txtPackQty.Text = 1
                DiscountCalculation() ''TFS2827
                txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            Else
                DiscountCalculation() ''TFS2827
                txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    '''  ''This Sub is Added to change/Lock PDP when Pack Rate is entered 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Ayesha Rehman : 22-05-2018 : TFS3330</remarks>
    Private Sub txtPackRate_Leave(sender As Object, e As EventArgs) Handles txtPackRate.Leave
        Try

            If Val(Me.txtPackRate.Text) > 0 Then
                If getConfigValueByType("Apply40KgRate").ToString = "True" AndAlso Me.cmbUnit.Text <> "Loose" Then
                    'Me.txtRate.Text = (Val(Me.txtPackRate.Text) / 40)
                    Me.txtPDP.Text = Math.Round((Val(Me.txtPackRate.Text) / 40), TotalAmountRounding)
                    Me.txtPDP.Enabled = False
                ElseIf Me.cmbUnit.Text <> "Loose" Then
                    'Me.txtRate.Text = (Val(Me.txtPackRate.Text) / Val(Me.txtPackQty.Text))
                    Me.txtPDP.Text = Math.Round((Val(Me.txtPackRate.Text) / Val(Me.txtPackQty.Text)), TotalAmountRounding)
                    Me.txtPDP.Enabled = False
                End If
            Else
                Me.txtPDP.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackRate_LostFocus1(sender As Object, e As EventArgs) Handles txtPackRate.LostFocus
        Try
            If Val(Me.txtPackQty.Text) = 0 Then
                txtPackQty.Text = 1
                DiscountCalculation() ''TFS2827
                txtTotal.Text = Math.Round((Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), TotalAmountRounding)
            Else
                DiscountCalculation() ''TFS2827
                txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), TotalAmountRounding)
                Total = Val(txtTotal.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackRate.TextChanged
        Try
            If Val(Me.txtPackRate.Text) > 0 Then
                If getConfigValueByType("Apply40KgRate").ToString = "True" AndAlso Me.cmbUnit.Text <> "Loose" Then
                    Me.txtPDP.Text = Math.Round((Val(Me.txtPackRate.Text) / 40), TotalAmountRounding) ''TFS2827
                    '  Me.txtRate.Text = (Val(Me.txtPackRate.Text) / 40) ''TFS3330
                    Me.txtPDP.Enabled = False
                ElseIf Me.cmbUnit.Text <> "Loose" Then
                    Me.txtPDP.Text = Math.Round((Val(Me.txtPackRate.Text) / Val(Me.txtPackQty.Text)), TotalAmountRounding)
                    '  Me.txtRate.Text = (Val(Me.txtPackRate.Text) / Val(Me.txtPackQty.Text)) ''TFS3330
                    Me.txtPDP.Enabled = False
                End If
            Else
                Me.txtPDP.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged
        Try
            'If Me.txtPackRate.Text.Length > 0 AndAlso Val(Me.txtPackRate.Text) > 0 Then
            '    If Me.cmbUnit.Text <> "Loose" Then
            '        Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))

            '    Else

            '        Me.txtRate.Text = Val(Me.txtRate.Text)

            '    End If
            'End If
            If Val(Me.txtPackQty.Text) > 0 Then
                Me.txtTotalQuantity.Text = Math.Round(Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text), TotalAmountRounding)
            Else
                Me.txtTotalQuantity.Text = Math.Round(Val(Me.txtQty.Text), TotalAmountRounding)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_LostFocus1(sender As Object, e As EventArgs) Handles txtQty.LostFocus
        'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtQty.Text) <> 0 AndAlso Val(Me.txtRate.Text) = 0 Then
        '    Me.txtRate.Text = Val(Me.txtTotal.Text) / Val(Me.txtQty.Text)
        'Else
        '    If Val(Me.txtPackQty.Text) = 0 Then
        '        txtPackQty.Text = 1
        '        txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        '    Else
        '        txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        '    End If
        'End If

        'If Val(Me.txtPackQty.Text) = 0 Then
        '    txtPackQty.Text = 1
        '    txtNetTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), DecimalPointInValue)
        'Else
        '    txtNetTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), DecimalPointInValue)
        'End If


        Try

            If IsSalesOrderAnalysis = True Then
                If Val(Me.txtDisc.Text) <> 0 Then
                    Me.txtDisc.TabStop = True
                End If
            End If

            GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Try
            If Val(Me.txtPackQty.Text) > 0 Then
                Me.txtTotalQuantity.Text = Math.Round(Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text), TotalAmountRounding)
            Else
                Me.txtTotalQuantity.Text = Math.Round(Val(Me.txtQty.Text), TotalAmountRounding)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceipt.Click, btnReceipts.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            frmSOCashReceipt._CompanyId = Val(Me.grdSaved.GetRow.Cells("LocationId").Value.ToString)
            frmSOCashReceipt._CostCenterId = Val(Me.grdSaved.GetRow.Cells("CostCenterId").Value.ToString)
            frmSOCashReceipt._CustomerId = Val(Me.grdSaved.GetRow.Cells("VendorId").Value.ToString)
            frmSOCashReceipt._SaleOrderId = Val(Me.grdSaved.GetRow.Cells("SalesOrderId").Value.ToString)
            frmSOCashReceipt._SaleOrderNo = Me.grdSaved.GetRow.Cells("SalesOrderNo").Value.ToString
            frmSOCashReceipt._SaleOrderDate = Me.grdSaved.GetRow.Cells("Date").Value
            frmSOCashReceipt._CustomerName = Me.grdSaved.GetRow.Cells("CustomerName").Value.ToString
            frmSOCashReceipt._NetAmount = Val(Me.grdSaved.GetRow.Cells("SalesOrderAmount").Value.ToString)
            frmSOCashReceipt.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintSelectVouchersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSelectVouchersToolStripMenuItem.Click
        ' Change on 23-11-2013  For Multiple Print Vouchers
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                AddRptParam("@SalesOrderId", r.Cells("SalesOrderId").Value)
                ShowReport("rptSalesOrder", , , , True)
                PrintLog = New SBModel.PrintLogBE
                PrintLog.DocumentNo = r.Cells("SalesOrderNo").Value.ToString
                PrintLog.UserName = LoginUserName
                PrintLog.PrintDateTime = Date.Now
                Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
    ''Commented Against TFS3330
    'Private Sub txtTotal_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotal.Leave
    '    Try


    '        'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtQty.Text) = 0 Then
    '        '    Me.txtQty.Text = Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)
    '        'End If

    '        'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtQty.Text) <> 0 AndAlso Val(Me.txtRate.Text) = 0 Then
    '        '    Me.txtRate.Text = Val(Me.txtTotal.Text) / Val(Me.txtQty.Text)
    '        'End If

    '        'If Val(Me.txtPackQty.Text) = 0 Then
    '        '    txtPackQty.Text = 1
    '        '    txtNetTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
    '        'Else
    '        '    txtNetTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
    '        'End If
    '        If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
    '        If Not Me.cmbItem.ActiveRow.Cells(0).Value > 0 Or Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
    '        GetDetailTotal()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''


    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            'OpenToolStripButton_Click(Nothing, Nothing)
            MapArticleAlias()
            Exit Sub
        End If
        ''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
        'If e.KeyCode = Keys.Delete Then
        '    DeleteToolStripButton_Click(Nothing, Nothing)
        '    Exit Sub
        'End If
    End Sub


    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Delete Then
            If Me.grdSaved.RowCount <= 0 Then Exit Sub
            DeleteToolStripButton_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.F5 Then
            BtnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Try
            If IsFormOpen = True Then FillCombo("Item")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Task:2417 Added Event Filter Item By Customer
    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Try
            If IsFormOpen = True Then
                If Me.RadioButton2.Checked = True Then
                    FillCombo("Item")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Task:2417 Added Event Filter Item By Customer
    Private Sub cmbVendor_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If IsFormOpen = True Then
                If Me.RadioButton2.Checked = True Then
                    If Me.cmbVendor.IsItemInList = False Then Exit Sub
                    FillCombo("Item")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2417
    'Task:2415 Add New Fields Engine No and Chassis No In Sales Module
    Public Function CheckDuplicateEngineNo() As Boolean
        Try
            If Me.grd.RowCount = 0 Then Return False
            For i As Int32 = 0 To Me.grd.RowCount - 1
                For j As Int32 = i + 1 To Me.grd.RowCount - 1
                    If Me.grd.GetRows(j).Cells(GrdEnum.Engine_No).Value.ToString.Length > 0 Then
                        If Me.grd.GetRows(j).Cells(GrdEnum.Engine_No).Value.ToString = Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString Then
                            Return True
                        End If
                    End If
                Next
            Next
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task:2415
    'Task:2415 Add New Fields Engine No and Chassis No In Sales Module
    Public Function CheckDuplicateChassisNo() As Boolean
        Try
            If Me.grd.RowCount = 0 Then Return False
            For i As Int32 = 0 To Me.grd.RowCount - 1
                For j As Int32 = i + 1 To Me.grd.RowCount - 1
                    If Me.grd.GetRows(j).Cells(GrdEnum.Chassis_No).Value.ToString.Length > 0 Then
                        If Me.grd.GetRows(j).Cells(GrdEnum.Chassis_No).Value.ToString = Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString Then
                            Return True
                        End If
                    End If
                Next
            Next
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task:2415
    ''15-Feb-2014 Task:2426 Imran Ali Payment Schedule On Sales Order And Purchase Order
    Private Sub btnReceiptSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceiptSchedule.Click
        Try
            If Me.BtnSave.Text = "&Update" Or Me.BtnSave.Text = "Update" Or Me.UltraTabControl1.SelectedTab.Index = 1 Then
                ApplyStyleSheet(frmPaymentTermsSchedule)
                frmPaymentTermsSchedule.FormName = frmPaymentTermsSchedule.enmOrderType.SO
                frmPaymentTermsSchedule.OrderId = grdSaved.CurrentRow.Cells("SalesOrderId").Value 'Val(Me.txtReceivingID.Text)
                frmPaymentTermsSchedule.OrderNo = grdSaved.CurrentRow.Cells(0).Value.ToString 'Me.txtPONo.Text.ToString
                frmPaymentTermsSchedule.ShowDialog()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnReceiptSchedule1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReceiptSchedule1.Click
        Try
            btnReceiptSchedule_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2426
    ''28-Feb-2014  TASK:24445 Imran Ali   Last purchase and sale price show on sale order and purchase order
    Public Function LastPrice(ByVal AccountId As Integer, ByVal ItemId As Integer) As Double
        Try
            Dim strSQL As String = String.Empty
            ''Ahmad Sharif: Update Query for Last Price of Same Customer for same item, 06-06-2015
            strSQL = "Select Isnull(Max(Price),0) as LastPrice From SalesOrderDetailTable WHERE ArticleDefId=" & ItemId & " AND SalesOrderId In (Select IsNull(Max(SalesOrderId),0) as SalesId From SalesOrderMasterTable WHERE VendorId=" & AccountId & ")"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return Val(dt.Rows(0).Item(0).ToString)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task:2445

#Region "SMS Template Setting"
    Public Function GetSMSParamters() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("@AccountCode")
            str.Add("@AccountTitle")
            str.Add("@DocumentNo")
            str.Add("@DocumentDate")
            str.Add("@OtherDocNo")
            str.Add("@Remarks")
            str.Add("@Amount")
            str.Add("@Quantity")
            str.Add("@DCNo")
            str.Add("@SONo")
            str.Add("@InvParty")
            str.Add("@DetailInformation")
            str.Add("@CompanyName")
            str.Add("@PreviousBalance")
            str.Add("@SIRIUS")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSKey() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("Sale Order")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSMSTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMSTemplate.Click
        Try
            Dim frmSMS As New frmSMSTemplate
            ApplyStyleSheet(frmSMS)
            frmSMS.cmbKey.DataSource = GetSMSKey()
            frmSMS.lstParameters.DataSource = GetSMSParamters()
            frmSMS.Show()
            frmSMS.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub SendSMS()
        Try
            '...................... Send SMS .............................
            If GetSMSConfig("SMS To Location On Sale Order").Enable = True Then
                Dim strSMSBody As String = String.Empty
                Dim objData As DataTable = CType(Me.grd.DataSource, DataTable)
                Dim dt_Loc As DataTable = objData.DefaultView.ToTable("Default", True, "LocationId")
                Dim drData() As DataRow
                For j As Integer = 0 To dt_Loc.Rows.Count - 1
                    strSMSBody = String.Empty
                    strSMSBody += "Sale Order, Doc No: " & Me.txtPONo.Text & ", Doc Date: " & Me.dtpPODate.Value.ToShortDateString & ", Supplier: " & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString & ", Invoice No: " & Me.txtPurchaseNo.Text & ", Remarks:" & Me.txtRemarks.Text & ", "
                    drData = objData.Select("LocationId=" & dt_Loc.Rows(j).Item(0).ToString & "")
                    Dim dblTotalQty As Double = 0D
                    Dim dblTotalAmount As Double = 0D
                    For Each dr As DataRow In drData
                        dblTotalQty += IIf(dr(GrdEnum.Unit).ToString = "Loose", Val(dr(GrdEnum.Qty).ToString), Val(dr(GrdEnum.Qty).ToString) * Val(dr(GrdEnum.PackQty).ToString))
                        strSMSBody += " " & dr(GrdEnum.Item).ToString & ", " & IIf(dr(GrdEnum.Unit).ToString = "Loose", " " & dr(GrdEnum.Qty).ToString & "", "" & (Val(dr(GrdEnum.Qty)).ToString * Val(dr(GrdEnum.PackQty)).ToString) & "") & ", "
                    Next
                    strSMSBody += " Total Qty: " & dblTotalQty & ""
                    Dim LocationPhone() As String = GetLocation(Val(dt_Loc.Rows(j).Item(0).ToString)).Mobile_No.Replace(",", ";").Replace("|", ";").Replace("\", ";").Replace("/", ";").Replace("^", ";").Replace("*", ";").Split(";")
                    If LocationPhone.Length > 0 Then
                        For Each strPhone As String In LocationPhone
                            If strPhone.Length > 0 Then
                                Try
                                    SaveSMSLog(strSMSBody, strPhone, "SMS to Location")
                                Catch ex As Exception
                                    Throw ex
                                End Try
                            End If
                        Next
                    End If
                Next
            End If
            If GetSMSConfig("Sale Order").Enable = True Then
                If msg_Confirm(str_ConfirmSendSMSMessage) = False Then Exit Try
                Dim strDetailMessage As String = String.Empty
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    'Task#08082015 Add configuration for sending sms with just item qunatity and engine no
                    If getConfigValueByType("DeliveryChalanByEnigneNo").ToString = "True" Then
                        If strDetailMessage.Length = 0 Then
                            strDetailMessage = "Qty: " & IIf(r.Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(r.Cells(GrdEnum.Qty).Value.ToString), Val(r.Cells(GrdEnum.Qty).Value.ToString) * Val(r.Cells(GrdEnum.PackQty).Value.ToString))
                        Else
                            strDetailMessage += ", Qty: " & IIf(r.Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(r.Cells(GrdEnum.Qty).Value.ToString), Val(r.Cells(GrdEnum.Qty).Value.ToString) * Val(r.Cells(GrdEnum.PackQty).Value.ToString))
                        End If
                        'End Task#08082015
                    Else
                        If strDetailMessage.Length = 0 Then
                            strDetailMessage = r.Cells(GrdEnum.Item).Value.ToString & ", PackQty: " & Val(r.Cells(GrdEnum.PackQty).Value.ToString) & ", Qty: " & IIf(r.Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(r.Cells(GrdEnum.Qty).Value.ToString), Val(r.Cells(GrdEnum.Qty).Value.ToString) * Val(r.Cells(GrdEnum.PackQty).Value.ToString))
                        Else
                            strDetailMessage += "," & r.Cells(GrdEnum.Item).Value.ToString & ", PackQty: " & Val(r.Cells(GrdEnum.PackQty).Value.ToString) & ", Qty: " & IIf(r.Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(r.Cells(GrdEnum.Qty).Value.ToString), Val(r.Cells(GrdEnum.Qty).Value.ToString) * Val(r.Cells(GrdEnum.PackQty).Value.ToString))
                        End If
                    End If
                Next
                Dim objTemp As New SMSTemplateParameter
                Dim obj As Object = GetSMSTemplate("Sale Order")
                If obj IsNot Nothing Then
                    objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                    Dim strMessage As String = objTemp.SMSTemplate
                    strMessage = strMessage.Replace("@AccountTitle", Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString).Replace("@AccountCode", Me.cmbVendor.ActiveRow.Cells("Code").Value.ToString).Replace("@DocumentNo", Me.txtPONo.Text).Replace("@DocumentDate", Me.dtpPODate.Value.ToShortDateString).Replace("@OtherDoc", Me.txtPurchaseNo.Text).Replace("@Remarks", Me.txtRemarks.Text).Replace("@Amount", grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@Quantity", Me.grd.GetTotal(grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@DCNo", Me.txtPurchaseNo.Text).Replace("@SONo", IIf(Me.cmbPo.SelectedIndex > 0, Me.cmbPo.Text, String.Empty)).Replace("@InvParty", Me.txtPurchaseNo.Text).Replace("@CompanyName", CompanyTitle).Replace("@SIRIUS", "Automated by www.SIRIUS.net").Replace("@PreviousBalance", _PrivousBalance).Replace("@DetailInformation", strDetailMessage).Replace("@Transporter", IIf(Me.cmbTransporter.SelectedIndex > 0, Me.cmbTransporter.Text, "' '"))
                    SaveSMSLog(strMessage, Me.cmbVendor.ActiveRow.Cells("Mobile").Value.ToString, "Sale Order")

                    If GetSMSConfig("Sale Order").EnabledAdmin = True Then
                        For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                            If strMob.Length > 10 Then
                                SaveSMSLog(strMessage, strMob, "Sale Order")
                            End If
                        Next
                    End If
                End If
            End If

            '...................... End Send SMS ................
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    Private Sub cmbQuotation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbQuotation.SelectedIndexChanged
        Try
            If IsFormOpen = False Then Exit Sub
            If IsEditMode = True Then Exit Sub
            If Me.cmbQuotation.SelectedIndex > 0 Then
                Me.cmbVendor.Value = Val(CType(Me.cmbQuotation.SelectedItem, DataRowView).Row.Item("VendorId").ToString)
                Me.cmbCompany.SelectedValue = Val(CType(Me.cmbQuotation.SelectedItem, DataRowView).Row.Item("LocationId").ToString)
                Me.cmbProject.SelectedValue = Val(CType(Me.cmbQuotation.SelectedItem, DataRowView).Row.Item("CostCenterId").ToString)
                Me.txtRemarks.Text = CType(Me.cmbQuotation.SelectedItem, DataRowView).Row.Item("Remarks").ToString
                DisplayDetail(Me.cmbQuotation.SelectedValue, -1, "Quotation")
                GetAdjustment(cmbQuotation.SelectedValue) ''TFS3738
                GetTotal() ''TFS3738
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbVendor_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.Leave
        Try
            If IsFormOpen = False Then Exit Sub
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            _PrivousBalance = GetCurrentBalance(Me.cmbVendor.Value)
            FillCombo("Quotation")
            If Not getConfigValueByType("AllDispatchLocations").ToString = "Error" Then
                AllDispatchLocation = getConfigValueByType("AllDispatchLocations")
            End If
            If AllDispatchLocation = False Then
                FillCombo("DispatchToLocation")
            Else
                FillCombo("AllDispatchToLocation")
            End If
            FillCombo("InvoiceToLocation")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAttachment_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAttachment.ButtonClick
        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            '' OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.Drives.Item(1).Name.ToString
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
            "All files (.)|*.*"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                If Me.BtnSave.Text <> "&Save" Then
                    If Me.grdSaved.RowCount > 0 Then
                        intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                    End If
                End If
                Me.btnAttachment.Text = "Attachment (" & arrFile.Count + intCountAttachedFiles & ")"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, ByVal objTrans As OleDb.OleDbTransaction) As Boolean
        Dim cmd As New OleDbCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()


            Dim objdt As New DataTable
            objdt = GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)

            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString

            If arrFile.Count > 0 Then
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        Dim New_Files As String = intId & "_" & DocId & "_SO_" & Me.dtpPODate.Value.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If


            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        cmd.CommandText = String.Empty
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()
                    Next
                End If
            End If


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function GetVoucherRecord() As DataSet
        Try

            Dim strSQL As String = String.Empty
            Dim ds As New dsVoucherDocumentAttachment
            ds.Tables.Clear()
            strSQL = "SP_SalesOrder " & Val(Me.grdSaved.GetRow.Cells("SalesOrderID").Value.ToString) & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            ds.Tables.Add(dt)
            ds.Tables(0).TableName = "dtSalesOrder"


            strSQL = String.Empty
            strSQL = "Select DocId,FileName,Path,Convert(Image,'') as Attachment_Image From DocumentAttachment WHERE (DocId=" & Me.grdSaved.GetRow.Cells("SalesOrderID").Value & ") AND Source=N'" & Me.Name & "'"
            Dim dtAttach As New DataTable
            dtAttach.TableName = "dtAttachment"
            dtAttach = GetDataTable(strSQL)

            If dtAttach IsNot Nothing Then
                If dtAttach.Rows.Count > 0 Then
                    For Each r As DataRow In dtAttach.Rows
                        r.BeginEdit()
                        If IO.File.Exists(CStr(r("Path").ToString & "\" & r("FileName").ToString)) Then
                            LoadPicture(r, "Attachment_Image", CStr(r("Path").ToString & "\" & r("FileName").ToString))
                        End If
                        r.EndEdit()
                    Next
                End If
            End If

            ds.Tables.Add(dtAttach)
            ds.Tables(1).TableName = "dtAttachment"
            ds.AcceptChanges()

            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub PrintAttachmentSalesOrderDocumentToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintAttachmentSalesOrderDocumentToolStripMenuItem1.Click
        Try
            PrintAttachmentSalesOrderDocumentToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintAttachmentSalesOrderDocumentToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintAttachmentSalesOrderDocumentToolStripMenuItem.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            'AddRptParam("Pm-dtVoucher.Voucher_Id", Me.grdVouchers.GetRow.Cells(0).Value)
            DataSetShowReport("RptSalesOrderDocument", GetVoucherRecord())
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try

            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Me.grdSaved.GetRow.Cells("SalesOrderID").Value.ToString
                frm.RemoveAttachmentForSalesOrder = flgRemoveAttachment
                frm.ShowDialog()
                DisplayRecord()
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''Task# A3-09-06-2015 Added Key Pres event for some textboxes to take just numeric, dot value
    Private Sub txtNUM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotal.KeyPress, txtTax.KeyPress, txtSpecialAdjustment.KeyPress, txtRate.KeyPress, txtDisc.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End Task# A3-09-06-2015

    Private Sub btnOrderType_Click(sender As Object, e As EventArgs) Handles btnOrderStatus.Click
        Try
            ApplyStyleSheet(frmSalesOrderStatus)
            frmSalesOrderStatus.ShowDialog()
            Dim id As Integer = 0I
            id = Me.cmbOrderStatus.SelectedIndex
            FillCombo("OrderStatus")
            Me.cmbOrderStatus.SelectedIndex = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ChangeDataSave(SalesOrderID As Integer, EntryDate As DateTime, trans As OleDb.OleDbTransaction)
        Dim cmd As New OleDbCommand
        cmd.Transaction = trans
        cmd.Connection = trans.Connection

        Try

            cmd.CommandTimeout = 300
            cmd.CommandType = CommandType.Text



            Dim str As String = String.Empty
            str = "INSERT INTO SalesOrderChangeTable(SaleOrderID,SaleOrderDate,Remarks,SaleOrderStatusId,UserID,UserName,EntryDate) " _
                & " VALUES(" & SalesOrderID & ",Convert(DateTime,'" & Me.dtpPODate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & Me.txtRemarks.Text.Replace("'", "''") & "'," & Me.cmbOrderStatus.SelectedValue & "," & LoginUserId & ",N'" & LoginUserName.Replace("'", "''") & "',Convert(datetime,'" & EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102))"

            cmd.CommandText = str
            cmd.ExecuteNonQuery()




        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub
    Private Sub PrintChangeLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintChangeLogToolStripMenuItem.Click
        Try
            If grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@SalesOrderID", Val(Me.grdSaved.GetRow.Cells("SalesOrderId").Value.ToString))
            ShowReport("rptSalesOrderLog")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintSalesOrderLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintSalesOrderLogToolStripMenuItem.Click
        Try
            PrintChangeLogToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnTasks_Click(sender As Object, e As EventArgs) Handles btnTasks.Click
        Try
            If Not grdSaved.GetRow Is Nothing AndAlso grdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = Me.Name
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdSaved.CurrentRow.Cells("SalesOrderNo").Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Sales Order (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()


            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) = False Then
                frmMain.LoadControl("frmConfigurationSystemNew")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Sales
            frmMain.LoadControl("frmConfigurationSystemNew")
            frmSystemConfigurationNew.SelectTab()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintSOWithImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintSOWithImageToolStripMenuItem.Click

        Dim strSQL As String = String.Empty
        strSQL = "SP_SalesOrderPrint " & Val(Me.grdSaved.GetRow.Cells("SalesOrderId").Value.ToString) & " "
        Dim dt As New DataTable
        Try
            dt = GetDataTable(strSQL)
            If dt IsNot Nothing Then
                For Each r As DataRow In dt.Rows
                    r.BeginEdit()
                    If IO.File.Exists(r.Item("ArticlePicture")) Then
                        LoadPicture(r, "ArticleImage", r.Item("ArticlePicture"))
                    End If
                    r.EndEdit()
                Next
            End If
            'dt.AcceptChanges()

            'AddRptParam("@SalesOrderId", Me.grdSaved.GetRow.Cells("SalesOrderId").Value)
            ShowReport("rptSalesOrderImage", , , , , , , dt, , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            MapArticleAlias()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub MapArticleAlias()
        Try
            Me.grd.UpdateData()
            If Me.grd.RowCount = 0 Then Exit Sub
            frmMapArticleAliasOnSalesOrder.GetSalesOrderId = Val(Me.txtReceivingID.Text)
            frmMapArticleAliasOnSalesOrder.GetSalesOrderNo = Me.txtPONo.Text
            frmMapArticleAliasOnSalesOrder.GetSalesOrderDate = Me.dtpPODate.Value
            frmMapArticleAliasOnSalesOrder.GetSalesOrderDetailId = Val(Me.grd.GetRow.Cells("SalesOrderDetailId").Value.ToString)
            frmMapArticleAliasOnSalesOrder.GetArtileId = Val(Me.grd.GetRow.Cells("ArticleDefId").Value.ToString)
            frmMapArticleAliasOnSalesOrder.GetArticleAliasName = Me.grd.GetRow.Cells("ArticleAliasName").Value.ToString
            If frmMapArticleAliasOnSalesOrder.ShowDialog = Windows.Forms.DialogResult.Yes Then

                Me.grd.GetRow.BeginEdit()
                Me.grd.GetRow.Cells("ArticleDefId").Value = frmMapArticleAliasOnSalesOrder.SetArticleDefId
                Me.grd.GetRow.Cells("ArticleCode").Value = frmMapArticleAliasOnSalesOrder.SetArticleCode
                Me.grd.GetRow.Cells("Item").Value = frmMapArticleAliasOnSalesOrder.SetArticleDescription
                Me.grd.GetRow.Cells("ArticleAliasName").Value = frmMapArticleAliasOnSalesOrder.GetArticleAliasName
                Me.grd.GetRow.EndEdit()
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SaveArticleAlias(grdRow As Janus.Windows.GridEX.GridEXRow, trans As OleDb.OleDbTransaction)
        Dim cmd As New OleDb.OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandTimeout = 120

        Try


            cmd.CommandText = ""
            cmd.CommandText = "Select Distinct MasterID  From ArticleDefTable where ArticleId=" & Val(grdRow.Cells("ArticleDefId").Value.ToString) & ""
            Dim intArticleMasterID As Integer = cmd.ExecuteScalar

            Dim dt As New DataTable
            dt = GetDataTable("Select Distinct ArticleAliasName From SalesOrderDetailTable WHERE ArticleDefId=" & Val(grdRow.Cells("ArticleDefId").Value.ToString) & " AND ArticleAliasName <> '' And ArticleAliasName not in (Select ArticleAliasName From ArticleAliasDefTable WHERE ArticleMasterId=" & intArticleMasterID & ") ", trans)
            dt.AcceptChanges()

            Dim currentID As Integer = 0I
            For Each r As DataRow In dt.Rows
                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO ArticleAliasDefTable(ArticleMasterID, ArticleAliasCode,ArticleAliasName,VendorId,Active,SortOrder) VALUES(" & intArticleMasterID & ",N'" & r.Item("ArticleAliasName").ToString.Replace("'", "''") & "',N'" & r.Item("ArticleAliasName").ToString.Replace("'", "''") & "'," & Val(Me.cmbVendor.Value) & ",1,1)Select @@Identity"
                currentID = cmd.ExecuteScalar()
            Next




        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
        End Try
    End Sub
    Public Sub GetDetailTotal()
        Try

            'If Val(Me.txtPackRate.Text) > 0 Then
            '    If getConfigValueByType("Apply40KgRate").ToString = "True" AndAlso Me.cmbUnit.Text <> "Loose" Then
            '        Me.txtRate.Text = (Val(Me.txtPackRate.Text) / 40)
            '    ElseIf Me.cmbUnit.Text <> "Loose" Then
            '        Me.txtRate.Text = (Val(Me.txtPackRate.Text) / Val(Me.txtPackQty.Text))
            '    End If
            'End If

            If Val(Me.txtTotalQuantity.Text) <> 0 AndAlso Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtTotal.Text) = 0 Then
                Me.txtTotal.Text = Math.Round((Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text)), DecimalPointInValue)
            End If
            ''Start TFS3330 : These Lines Are commented Aganist TFS3330
            'If Val(Me.txtTotalQuantity.Text) <> 0 AndAlso Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtRate.Text) = 0 Then
            '    Me.txtRate.Text = Math.Round(Val(Me.txtTotal.Text) / Val(Me.txtTotalQuantity.Text), TotalAmountRounding)
            'End If
            ''End TFS3330
            If Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtTotalQuantity.Text) = 0 Then
                If Not Me.cmbUnit.Text <> "Loose" Then
                    Me.txtQty.Text = Math.Round(Val(Me.txtTotal.Text) / Val(Me.txtRate.Text), TotalAmountRounding)
                    Me.txtTotalQuantity.Text = Math.Round(Val(Me.txtQty.Text), TotalAmountRounding)
                Else
                    If Val(Me.txtPackQty.Text) > 0 Then
                        Me.txtQty.Text = Math.Round((Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)) / Val(Me.txtPackQty.Text), TotalAmountRounding)
                        Me.txtTotalQuantity.Text = Math.Round((Val(Me.txtQty.Text) * Val(Me.txtPackQty.Text)), TotalAmountRounding)
                    Else
                        Me.txtQty.Text = Math.Round(Val(Me.txtTotal.Text) / Val(Me.txtRate.Text), TotalAmountRounding)
                        Me.txtTotalQuantity.Text = Math.Round(Val(Me.txtQty.Text), TotalAmountRounding)
                    End If
                End If
            Else
                Me.txtTotal.Text = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text), TotalAmountRounding)     'Task#26082015
            End If

            Dim dblTaxPercent As Double = 0D
            Double.TryParse(Me.txtTax.Text, dblTaxPercent)
            ''Start TFS3758
            If flgExcludeTaxPrice = True Then
                Me.txtNetTotal.Text = Math.Round(Val(Me.txtTotal.Text), TotalAmountRounding) ''TFS3758
            Else
                Me.txtNetTotal.Text = Math.Round((((dblTaxPercent / 100) * Val(Me.txtTotal.Text)) + Val(Me.txtTotal.Text)), TotalAmountRounding)
            End If
            ''End TFS3758
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTotalQuantity_LostFocus(sender As Object, e As EventArgs)
        Try
            GetTotal()
            If Not Val(Me.txtPackQty.Text) > 0 Then
                Me.txtQty.Text = Math.Round(Val(Me.txtTotalQuantity.Text), TotalAmountRounding)
            End If
            Me.GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub txtTotalQuantity_LostFocus1(sender As Object, e As EventArgs) Handles txtTotalQuantity.LostFocus

        Try
            GetTotal()
            If Not Val(Me.txtPackQty.Text) > 0 Then
                Me.txtQty.Text = Math.Round(Val(Me.txtTotalQuantity.Text), TotalAmountRounding)
            End If
            Me.GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub txtTotalQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtTotalQuantity.TextChanged
        Try
            GetTotal()
            If Not Val(Me.txtPackQty.Text) > 0 Then
                Me.txtQty.Text = Math.Round(Val(Me.txtTotalQuantity.Text), TotalAmountRounding)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDisc_LostFocus1(sender As Object, e As EventArgs) Handles txtDisc.LostFocus
        Try
            'Dim disc As Double = 0D
            'Double.TryParse(Me.txtDisc.Text.Trim, disc)
            'Dim price As Double = 0D
            'Double.TryParse(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString, price)
            'If Val(disc) <> 0 AndAlso Val(price) <> 0 Then
            '    Me.txtRate.Text = price - ((price / 100) * disc)
            'Else
            '    Me.txtRate.Text = txtRate.Text
            'End If
            DiscountCalculation() ''TFS2827
            GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDisc_TextChanged(sender As Object, e As EventArgs) Handles txtDisc.TextChanged

    End Sub

    Private Sub txtDisc_LostFocus(sender As Object, e As EventArgs)
        Try
            'Dim disc As Double = 0D
            'Double.TryParse(Me.txtDisc.Text.Trim, disc)
            'Dim price As Double = 0D
            'Double.TryParse(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString, price)
            'If Val(disc) <> 0 AndAlso Val(price) <> 0 Then
            '    Me.txtRate.Text = price - ((price / 100) * disc)
            'Else
            '    Me.txtRate.Text = txtRate.Text
            'End If
            DiscountCalculation() ''TFS2827
            GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub txtTotal_TextChanged(sender As Object, e As EventArgs) Handles txtTotal.TextChanged
        'Try
        '    '' Below lines are added against TASK TFS3330  : Ayesha Rehman
        '    If Me.cmbUnit.Text = "Pack" Then
        '        If Val(Me.txtTotal.Text) > 0 AndAlso Val(Me.txtTotalQuantity.Text) > 0 Then
        '            Me.txtPDP.Text = Val(Me.txtTotal.Text) / Val(Me.txtTotalQuantity.Text)
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Public Sub GetGridDetailQtyCalculate(ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            Me.grd.UpdateData()
            If e.Column.Index = GrdEnum.Qty Or e.Column.Index = GrdEnum.PackQty Then
                If Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(GrdEnum.TotalQuantity).Value = (Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) * Val(Me.grd.GetRow.Cells(GrdEnum.Qty).Value.ToString))
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                Else
                    Me.grd.GetRow.Cells(GrdEnum.TotalQuantity).Value = Val(Me.grd.GetRow.Cells(GrdEnum.Qty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                End If
            ElseIf e.Column.Index = GrdEnum.TotalQuantity Then
                If Not Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(GrdEnum.Qty).Value = Val(Me.grd.GetRow.Cells(GrdEnum.TotalQuantity).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.Qty).Value
                End If
            End If
            'Me.grd.Refetch()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetGridDetailTotal()
        Try

            Me.grd.UpdateData()
            Me.grd.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            If Val(grd.GetRow.Cells(GrdEnum.TotalQuantity).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(GrdEnum.Rate).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) = 0 Then
                'grd.GetRow.Cells(EnumGridDetail.Total).Value = Math.Round((Val(grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString) * Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString)), DecimalPointInValue)
            End If
            If Val(grd.GetRow.Cells(GrdEnum.TotalQuantity).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(GrdEnum.Rate).Value.ToString) = 0 Then
                Me.txtRate.Text = Math.Round(Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) / Val(grd.GetRow.Cells(GrdEnum.TotalQuantity).Value.ToString), TotalAmountRounding)
            End If
            If Val(grd.GetRow.Cells(GrdEnum.Rate).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(GrdEnum.TotalQuantity).Value.ToString) = 0 Then
                If Not Me.cmbUnit.Text <> "Loose" Then
                    grd.GetRow.Cells(GrdEnum.Qty).Value = Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) / Val(grd.GetRow.Cells(GrdEnum.Rate).Value.ToString)
                    grd.GetRow.Cells(GrdEnum.TotalQuantity).Value = Val(grd.GetRow.Cells(GrdEnum.Qty).Value.ToString)
                Else
                    If Val(grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) > 0 Then
                        grd.GetRow.Cells(GrdEnum.Qty).Value = (Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) / Val(grd.GetRow.Cells(GrdEnum.Rate).Value.ToString)) / Val(grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString)
                        grd.GetRow.Cells(GrdEnum.TotalQuantity).Value = (Val(grd.GetRow.Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString))
                    Else
                        grd.GetRow.Cells(GrdEnum.Qty).Value = Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) / Val(grd.GetRow.Cells(GrdEnum.Rate).Value.ToString)
                        grd.GetRow.Cells(GrdEnum.TotalQuantity).Value = Val(grd.GetRow.Cells(GrdEnum.Qty).Value.ToString)
                    End If
                End If
            End If

            Me.grd.EditMode = Janus.Windows.GridEX.EditMode.EditOff

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub PrintSelectedRevisionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintSelectedRevisionToolStripMenuItem.Click
        Try
            ''TASK-480 on 14-07-2016
            If Not Me.cmbRevisionNumber.SelectedValue Is Nothing Then
                AddRptParam("@RevisionNumber", Val(Me.cmbRevisionNumber.Text)) ''SalesOrderHistoryId
                AddRptParam("@SalesOrderHistoryId", Me.cmbRevisionNumber.SelectedValue) ''SalesOrderHistoryId
                ShowReport("rptSalesOrderHistory", , , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function FillRevisionCombo(ByVal salesOrderId As Integer) As Integer
        Dim str As String = String.Empty
        Try
            str = "Select SalesOrderHistoryId, RevisionNumber FROM SalesOrderHistory Where SalesOrderId =" & salesOrderId & " Order by 1 DESC"
            FillDropDown(Me.cmbRevisionNumber, str, False)
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return dt.Rows.Item(0).Item(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckRevisionNumber(ByVal salesOrderId As Integer) As Integer
        Dim str As String = String.Empty
        Try
            str = "Select Distinct RevisionNumber, RevisionNumber FROM SalesOrderHistory Where SalesOrderId =" & salesOrderId & " Order by 1 DESC"
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return dt.Rows.Item(0).Item(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''TASK-480
    Public Sub DisplayDetailHistory(ByVal revisionNumber As Integer, historyId As Integer)
        Dim str As String = String.Empty
        Try
            ''TFS4343 : Edit Query to get Article Size and color
            str = "SELECT Recv_D.SerialNo, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleAliasName ,ArticleSizeDefTable.ArticleSizeName as Size, ArticleColorDefTable.ArticleColorName as Color, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, Recv_D.CurrencyAmount, Convert(float,0) as TotalCurrencyAmount,  " _
                 & " (IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
                 & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, Isnull(recv_d.TradePrice,0) as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as [Tax Amount], Convert(float,0) as [CurrencyTaxAmount], IsNull(Recv_D.SED_Tax_Percent,0) as SED_Tax_Percent, IsNull(Recv_D.SED_Tax_Amount,0) as SED_Tax_Amount,  Convert(float,0) as [CurrencySEDAmount], Convert(float,0) as [Total Amount], ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, ISNULL(recv_d.Freight,0) as Freight, ISNULL(Recv_d.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Other_Comments as [Other Comments],Recv_D.Engine_No, Recv_D.Chassis_No, 0 as BillValueAfterDiscount, IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, IsNull(Recv_D.PlanedQty,0) as PlanedQty, IsNull(Recv_D.CostPrice,0) as CostPrice, IsNull(Recv_D.SalesOrderDetailId,0) as SalesOrderDetailId, IsNull(Recv_D.QuotationDetailId,0) As QuotationDetailId, 0 As QuotationId, 0 As SOQuantity, Recv_D.SaleOrderType, IsNull(Recv_D.Qty, 0) As TotalQuantity  FROM dbo.SalesOrderDetailHistory Recv_D LEFT OUTER JOIN " _
                 & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId Inner Join " _
                 & " SalesOrderHistory On Recv_D.SalesOrderHistoryId = SalesOrderHistory.SalesOrderHistoryId LEFT OUTER JOIN" _
                 & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
                 & " Left Outer Join ArticleColorDefTable On Article.ArticleColorId = ArticleColorDefTable.ArticleColorId Left Outer Join ArticleSizeDefTable on Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
                 & " Where SalesOrderHistory.RevisionNumber =" & revisionNumber & " And SalesOrderHistory.SalesOrderHistoryId = " & historyId & " ORDER BY Article.SortOrder Asc"
            Dim dtDisplayDetail As New DataTable
            dtDisplayDetail = GetDataTable(str)

            If IsSalesOrderAnalysis = True Then
                dtDisplayDetail.Columns(GrdEnum.BillValueAfterDiscount).Expression = "IIF(Unit='Loose',((((((Qty * CurrentPrice) * SalesTax_Percentage)/100) + (((SchemeQty*CurrentPrice)*SalesTax_Percentage)/100)) + (Freight*(Qty+SchemeQty))) - (((Qty*CurrentPrice)*Discount_Percentage)/100)  +  (Qty * CurrentPrice)), (((((((Qty * PackQty) * CurrentPrice) * SalesTax_Percentage)/100) + (((SchemeQty * CurrentPrice) * SalesTax_Percentage)/100)) + (Freight * ((Qty * PackQty)+SchemeQty))) - ((((Qty * PackQty) * CurrentPrice) * Discount_Percentage)/100)  +  ((Qty * PackQty)* CurrentPrice)))"
            End If
            dtDisplayDetail.Columns("Total").Expression = " [TotalQuantity]*[Price]*CurrencyRate"
            dtDisplayDetail.Columns("CurrencyAmount").Expression = " [TotalQuantity]*[Price]"
            dtDisplayDetail.Columns("Tax Amount").Expression = "((IsNull([SalesTax_Percentage],0)/100)*Total)" 'Task:2374 Show Tax Amount
            dtDisplayDetail.Columns("SED_Tax_Amount").Expression = "((IsNull([SED_Tax_Percent],0)/100)*IsNull([Total],0))"
            dtDisplayDetail.Columns("CurrencyTaxAmount").Expression = "((IsNull([SalesTax_Percentage],0)/100)*CurrencyAmount)" 'Task:2374 Show Tax Amount
            dtDisplayDetail.Columns("CurrencySEDAmount").Expression = "((IsNull([SED_Tax_Percent],0)/100)*IsNull([CurrencyAmount],0))"
            dtDisplayDetail.Columns("Total Amount").Expression = "(IsNull([Total],0) + IsNull([Tax Amount],0)+IsNull([SED_Tax_Amount],0))" 'Task:2374 Show Total Amount
            dtDisplayDetail.Columns("TotalCurrencyAmount").Expression = "(IsNull([CurrencyAmount],0) + IsNull([CurrencyTaxAmount],0)+IsNull([CurrencySEDAmount],0))"
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            If dtDisplayDetail.Rows.Count > 0 Then
                If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                    Me.cmbCurrency.Enabled = True
                    If Not Me.cmbCurrency.SelectedIndex = -1 Then
                        Me.cmbCurrency.SelectedValue = 1

                    End If
                Else
                    'FillCombo("Currency")
                    'Me.cmbCurrency.SelectedValue = Math.Round(Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString), TotalAmountRounding)
                    'CurrencyRate = Math.Round(Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString), TotalAmountRounding)
                    'Me.cmbCurrency.Enabled = False
                    CurrencyRate = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString)
                    Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                    '' Being made editable against TASK TFS3493 on 07/06/18
                    Me.cmbCurrency.Enabled = True
                End If
                cmbCurrency_SelectedIndexChanged(Nothing, Nothing)
                CurrencyRate = 1
            End If



            FillCombo("grdLocation")
            ApplyGridSetting()
            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar3_Load(Nothing, Nothing)
            GetSalesOrderAnalysis()


            If flgLoadItems = True Then
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    If Me.grd.RowCount > 0 Then
                        r.BeginEdit()
                        r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
                        r.EndEdit()
                    End If
                Next
            End If


            Dim dtOType As New DataTable
            dtOType.TableName = "Default"
            dtOType.Columns.Add("SaleOrderType", GetType(System.String))
            Dim dr As DataRow
            dr = dtOType.NewRow
            dr(0) = "Supply"
            dtOType.Rows.Add(dr)
            dr = dtOType.NewRow
            dr(0) = "Services"
            dtOType.Rows.Add(dr)
            dr = dtOType.NewRow
            dr(0) = "Out Sourcing"
            dtOType.Rows.Add(dr)
            Me.grd.RootTable.Columns("SaleOrderType").ValueList.PopulateValueList(dtOType.DefaultView, "SaleOrderType", "SaleOrderType")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''TASK-480 on 14-07-2016
    Private Sub DisplayHistory(ByVal revisionNumber As Integer, ByVal historyId As Integer)
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Try
            str = "SELECT Recv.SalesOrderNo, Recv.SalesOrderDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty, " & _
                           "Recv.SalesOrderAmount, Recv.SalesOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, Recv.PoNo as [Po No], isnull(Recv.SOP_ID,0) as SOP_ID, tblDefEmployee.Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], CstCntr.Name, dbo.vwCOADetail.Contact_Email as Email,IsNull(Recv.SaleOrderStatusId,0) as SaleOrderStatusId, SalesOrderStatusTable.OrderStatus as [Order Status], IsNull(Recv.QuotationId,0) as QuotationId, Quot.QuotationNo, IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment],Recv.Terms_And_Condition,recv.UserName,Recv.UpdateUserName as [Modified By], IsNull(Recv.SaleOrderTypeId,0) as SaleOrderTypeId, SalesOrderTypeTable.SalesOrderTypeName as [Order Type]   " & _
                           "FROM tblDefCostCenter CstCntr Right Outer Join dbo.SalesOrderHistory Recv ON CstCntr.CostCenterID=Recv.CostCenterId LEFT OUTER JOIN " & _
                           "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblDefEmployee ON tblDefEmployee.Employee_ID = Recv.SOP_ID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesOrderNo LEFT OUTER JOIN QuotationMasterTable Quot On Quot.QuotationId = Recv.QuotationId  LEFT OUTER JOIN  SalesOrderStatusTable on SalesOrderStatusTable.OrderStatusID = Recv.SaleOrderStatusID LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = Recv.SalesOrderId LEFT OUTER JOIN SalesOrderTypeTable on SalesOrderTypeTable.SaleOrderTypeId = Recv.SaleOrderTypeId WHERE Recv.SalesOrderNo IS NOT NULL And Recv.RevisionNumber = " & revisionNumber & " And Recv.SalesOrderHistoryId = " & historyId & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            '' dt.Rows.Item(0).Item("LocationId")
            Me.cmbCompany.SelectedValue = Val(dt.Rows.Item(0).Item("LocationId").ToString)
            txtPONo.Text = dt.Rows.Item(0).Item("SalesOrderNo").ToString
            dtpPODate.Value = CType(dt.Rows.Item(0).Item("Date"), Date)
            txtReceivingID.Text = Val(dt.Rows.Item(0).Item("SalesOrderId").ToString)            'TODO. ----
            cmbVendor.Value = Val(dt.Rows.Item(0).Item("VendorId").ToString)
            ''R933  Validate Customer Data 
            'cmbVendor.Value = grdSaved.CurrentRow.Cells("VendorId").Value
            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Vendor is disable.")
                Exit Sub
            End If
            txtRemarks.Text = dt.Rows.Item(0).Item("Remarks").ToString & ""
            txtPaid.Text = dt.Rows.Item(0).Item("CashPaid") & ""
            Me.cmbProject.SelectedValue = Val(dt.Rows.Item(0).Item("CostCenterId").ToString)
            If IsDBNull(dt.Rows.Item(0).Item("PO_Date")) Then
                Me.dtpPDate.Value = Now
                Me.dtpPDate.Checked = False
            Else
                Me.dtpPDate.Value = dt.Rows.Item(0).Item("PO_Date")
                Me.dtpPDate.Checked = True
            End If
            If dt.Rows.Item(0).Item("Adj_Flag") = False Then
                Me.txtSpecialAdjustment.Text = dt.Rows.Item(0).Item("Adjustment") & ""
                Me.rbtAdjFlat.Checked = True
            Else
                Me.txtSpecialAdjustment.Text = dt.Rows.Item(0).Item("SpecialAdjustment") & ""
                Me.rbtAdjPercentage.Checked = True
            End If
            If IsDBNull(dt.Rows.Item(0).Item("SOP_ID")) Then
                Me.cmbSalesMan.SelectedValue = 0
            Else
                Me.cmbSalesMan.SelectedValue = dt.Rows.Item(0).Item("SOP_ID")
            End If
            Me.txtTerms_And_Condition.Text = dt.Rows.Item(0).Item("Terms_And_Condition").ToString
            Me.txtCustPONo.Text = dt.Rows.Item(0).Item("PO No").ToString
            Me.cmbOrderStatus.SelectedValue = Val(dt.Rows.Item(0).Item("SaleOrderStatusID").ToString)
            Me.btnAttachment.Text = "Attachment (" & dt.Rows.Item(0).Item("No Of Attachment").ToString & ")"
            'Dim dtQuot As New DataTable
            'dtQuot = CType(Me.cmbQuotation.DataSource, DataTable)
            'dtQuot.AcceptChanges()
            'Dim drQuot() As DataRow
            'drQuot = dtQuot.Select("QuotationId=" & Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString) & "")
            'If drQuot.Length = 0 Then
            '    Dim drq As DataRow
            '    drq = dtQuot.NewRow
            '    drq(0) = Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString)
            '    drq(1) = Me.grdSaved.GetRow.Cells("QuotationNo").Value.ToString
            '    drq(2) = Val(Me.grdSaved.GetRow.Cells("LocationId").Value.ToString)
            '    drq(3) = Val(Me.grdSaved.GetRow.Cells("VendorId").Value.ToString)
            '    dtQuot.Rows.Add(drq)
            '    dtQuot.AcceptChanges()
            'End If
            Me.cmbQuotation.SelectedValue = Val(dt.Rows.Item(0).Item("QuotationId").ToString)

            If IsSalesOrderAnalysis = False Then
                Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.Total), Janus.Windows.GridEX.AggregateFunction.Sum)
            Else
                Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.BillValueAfterDiscount), Janus.Windows.GridEX.AggregateFunction.Sum)
            End If
            If IsDBNull(dt.Rows.Item(0).Item("Delivery_Date")) Then
                Me.dtpDeliveryDate.Value = Date.Now
                Me.dtpDeliveryDate.Checked = False
            Else
                dtpDeliveryDate.Value = dt.Rows.Item(0).Item("Delivery_Date")
                Me.dtpDeliveryDate.Checked = True
            End If
            GetTotal()

            GetSecurityRights()
            Me.chkPost.Checked = dt.Rows.Item(0).Item("Posted")
            'Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
            Me.lblPrintStatus.Text = "Print Status: " & dt.Rows.Item(0).Item("Print Status").ToString
            Dim intCountAttachedFiles As Integer = 0I
            If Me.BtnSave.Text <> "&Save" Then
                If Me.grdSaved.RowCount > 0 Then
                    intCountAttachedFiles = Val(dt.Rows.Item(0).Item("No Of Attachment").ToString)
                    Me.btnAttachment.Text = "Attachment (" & intCountAttachedFiles & ")"
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lnkLblRevisions_Click(sender As Object, e As EventArgs) Handles lnkLblRevisions.Click
        If lblRev.Visible = False AndAlso cmbRevisionNumber.Visible = False Then
            Me.lblRev.Visible = True
            Me.cmbRevisionNumber.Visible = True
            lnkLblRevisions.Visible = False
        End If
    End Sub

    Private Sub lnkLblRevisions_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkLblRevisions.LinkClicked
        If lblRev.Visible = False AndAlso cmbRevisionNumber.Visible = False Then
            Me.lblRev.Visible = True
            Me.cmbRevisionNumber.Visible = True
            lnkLblRevisions.Visible = False
        End If
    End Sub

    Private Sub cmbRevisionNumber_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbRevisionNumber.SelectedValueChanged
        ''TASK-480
        Try
            If cmbRevisionNumber.SelectedValue > 0 AndAlso IsRevisionRestrictedFirstTime = False Then
                DisplayHistory(Val(cmbRevisionNumber.Text), cmbRevisionNumber.SelectedValue)
                DisplayDetailHistory(Val(cmbRevisionNumber.Text), cmbRevisionNumber.SelectedValue)
            End If
            IsRevisionRestrictedFirstTime = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''TASK-DS22072016
    Private Sub DisplayItemDeliveryDetail(ByVal salesOrderId As Integer)
        Dim Str As String = String.Empty
        Dim dt As New DataTable

        Try
            Str = "Select SOItemDeliverySchedule.SOItemDeliveryScheduleId, ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription, Convert(varchar, SOItemDeliverySchedule.DeliveryDate ,106) as DeliveryDate , " _
                & " SalesOrderDetailTable.SalesOrderDetailId, SalesOrderDetailTable.SalesOrderId, IsNull(SOItemDeliverySchedule.ScheduleQty, 0) As ScheduleQty " _
                & " From ArticleDefTable Inner Join SalesOrderDetailTable On ArticleDefTable.ArticleId = SalesOrderDetailTable.ArticleDefId " _
                & " Inner Join SOItemDeliverySchedule On SalesOrderDetailTable.SalesOrderDetailId = SOItemDeliverySchedule.SalesOrderDetailId Where SOItemDeliverySchedule.SalesOrderId =" & salesOrderId & ""
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            Me.grdDeliverySchedule.DataSource = dt
            Me.grdDeliverySchedule.RootTable.Columns("ArticleDescription").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayItemDeliveryDetailBySaleorderDetail(ByVal salesOrderDetailId As Integer)
        Dim Str As String = String.Empty
        Dim dt As New DataTable

        Try
            Str = "Select SOItemDeliverySchedule.SOItemDeliveryScheduleId, ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription, Convert(varchar, SOItemDeliverySchedule.DeliveryDate ,106) as DeliveryDate , " _
                & " SalesOrderDetailTable.SalesOrderDetailId, SalesOrderDetailTable.SalesOrderId, IsNull(SOItemDeliverySchedule.ScheduleQty, 0) As ScheduleQty " _
                & " From ArticleDefTable Inner Join SalesOrderDetailTable On ArticleDefTable.ArticleId = SalesOrderDetailTable.ArticleDefId " _
                & " Inner Join SOItemDeliverySchedule On SalesOrderDetailTable.SalesOrderDetailId = SOItemDeliverySchedule.SalesOrderDetailId Where SOItemDeliverySchedule.SalesOrderDetailId =" & salesOrderDetailId & ""
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            Me.grdDeliverySchedule.DataSource = dt
            Me.grdDeliverySchedule.RootTable.Columns("ArticleDescription").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbItemWiseDelivery_ValueChanged(sender As Object, e As EventArgs) Handles cmbItemWiseDelivery.ValueChanged
        Try
            If Me.cmbItemWiseDelivery.Value > 0 Then
                Dim remainQty As Double = GetRemainingQty(Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("SalesOrderDetailId").Value.ToString))
                Me.txtRemQty.Text = Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("Qty").Value.ToString) - remainQty
                Me.DisplayItemDeliveryDetailBySaleorderDetail(Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("SalesOrderDetailId").Value.ToString))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub AddItemDelivSchedule()
        '       [SOItemDeliveryScheduleId] [int] IDENTITY(1,1) NOT NULL,
        '[SalesOrderId] [int] NULL,
        '[SalesOrderDetailId] [int] NULL,
        '[ArticleDefId] [int] NULL,
        '[ScheduleQty] [float] NULL,
        '[RemainingQty] [float] NULL,
        '[DeliveryDate] [datetime] NULL,
        Dim str As String = String.Empty
        Dim cmd As New OleDbCommand
        Dim conn As OleDbConnection
        conn = Con
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        conn.Open()
        Dim trans As OleDbTransaction = conn.BeginTransaction
        Try
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans
            cmd.CommandText = "Insert into SOItemDeliverySchedule(SalesOrderId, SalesOrderDetailId, ArticleDefId, ScheduleQty, DeliveryDate) " _
                & " Values(" & Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("SalesOrderId").Value.ToString) & ", " & Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("SalesOrderDetailId").Value.ToString) & ", " & Me.cmbItemWiseDelivery.Value & ", " & Val(Me.txtDeliverQty.Text) & ", N'" & Me.dtpItemDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "' )"
            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub

    Private Sub btnAddItemSchedule_Click(sender As Object, e As EventArgs) Handles btnAddItemSchedule.Click
        Try
            If Val(Me.txtDeliverQty.Text) > Val(Me.txtRemQty.Text) Then
                msg_Error("Quantity should be less than remaining quantity")
                Exit Sub
            ElseIf Val(Me.txtDeliverQty.Text) <= 0 Then
                msg_Error("Quantity is required to be larger than 0")
                Exit Sub
            End If
            AddItemDelivSchedule()
            DisplayItemDeliveryDetailBySaleorderDetail(Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("SalesOrderDetailId").Value.ToString))
            'Me.cmbItemWiseDelivery.Rows(0).Activate()
            Me.txtRemQty.Text = Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("Qty").Value.ToString) - GetRemainingQty(Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("SalesOrderDetailId").Value.ToString))
            Me.txtDeliverQty.Text = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDeliverySchedule_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDeliverySchedule.CellEdited
        Dim scheduleQty As Double = 0D
        Dim remainingQty As Double = 0D

        Try
            If Me.grdDeliverySchedule.GetRow().Cells("ScheduleQty").DataChanged = True Then
                'scheduleQty = Me.grdDeliverySchedule.GetRow.Cells("ScheduleQty").Value
                'remainingQty = Me.grdDeliverySchedule.GetRow.Cells("RemainingQty").Value
                'If Me.cmbItemWiseDelivery.Value = 0 Then
                '    msg_Error("")
                '    Exit Sub
                'End If
                Me.grdDeliverySchedule.UpdateData()
                Dim gridTotalQty As Double = grdDeliverySchedule.GetTotal(Me.grdDeliverySchedule.RootTable.Columns("ScheduleQty"), Janus.Windows.GridEX.AggregateFunction.Sum)
                Dim availableTotalQty As Double = Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("Qty").Value.ToString)
                Dim remaining As Double = Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("Qty").Value.ToString) - GetRemainingQty(Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("SalesOrderDetailId").Value.ToString))
                If gridTotalQty > availableTotalQty Then
                    msg_Error("Entered quantity exceeds available quantity")
                    Exit Sub
                End If
                'If remaining <= 0 Then
                '    msg_Error("No quantity is left to schedule")
                '    DisplayItemDeliveryDetail(Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("SalesOrderDetailId").Value.ToString))
                '    Exit Sub
                'End If

                Me.UpdateScheduleQty(Val(Me.grdDeliverySchedule.GetRow.Cells("SOItemDeliveryScheduleId").Value), Me.grdDeliverySchedule.GetRow.Cells("ScheduleQty").Value)
                DisplayItemDeliveryDetail(Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("SalesOrderDetailId").Value.ToString))
                Me.txtRemQty.Text = Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("Qty").Value.ToString) - GetRemainingQty(Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("SalesOrderDetailId").Value.ToString))
            ElseIf Me.grdDeliverySchedule.GetRow().Cells("DeliveryDate").DataChanged = True Then
                Me.grdDeliverySchedule.UpdateData()
                Me.UpdateItemDelivDate(Me.grdDeliverySchedule.GetRow.Cells("SOItemDeliveryScheduleId").Value, Me.grdDeliverySchedule.GetRow.Cells("DeliveryDate").Value)
                DisplayItemDeliveryDetail(Val(Me.cmbItemWiseDelivery.SelectedRow.Cells("SalesOrderDetailId").Value.ToString))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UpdateItemDelivDate(ByVal id As Integer, ByVal dt As DateTime)
        Dim cmd As New OleDbCommand
        Dim conn As OleDbConnection
        conn = Con
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        conn.Open()
        Dim trans As OleDbTransaction = conn.BeginTransaction
        Try
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans
            cmd.CommandText = "Update SOItemDeliverySchedule Set DeliveryDate = N'" & dt.ToString("yyyy-M-d h:mm:ss tt") & "' Where SOItemDeliveryScheduleId =" & id & ""
            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub
    Private Sub UpdateScheduleQty(ByVal id As Integer, ByVal scheduleQty As Double)
        Dim cmd As New OleDbCommand
        Dim conn As OleDbConnection
        conn = Con
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        conn.Open()
        Dim trans As OleDbTransaction = conn.BeginTransaction
        Try
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans
            cmd.CommandText = "Update SOItemDeliverySchedule Set ScheduleQty = " & Val(scheduleQty) & " Where SOItemDeliveryScheduleId =" & id & ""
            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub
    Private Sub DeleteItemSchedule(ByVal id As Integer)
        Dim cmd As New OleDbCommand
        Dim conn As OleDbConnection
        conn = Con
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        conn.Open()
        Dim trans As OleDbTransaction = conn.BeginTransaction
        Try
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans
            cmd.CommandText = "Delete From SOItemDeliverySchedule Where SOItemDeliveryScheduleId =" & id & ""
            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub

    Private Sub grdDeliverySchedule_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDeliverySchedule.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.DeleteItemSchedule(Val(Me.grdDeliverySchedule.GetRow.Cells("SOItemDeliveryScheduleId").Value.ToString))
                Me.grdDeliverySchedule.GetRow.Delete()
                grdDeliverySchedule.UpdateData()
                'GetTotal()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetRemainingQty(ByVal salesOrderDetailId As Integer) As Double
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Try
            str = "Select SalesOrderDetailId, Sum(IsNull(ScheduleQty, 0)) As ScheduleQty From SOItemDeliverySchedule Where SalesOrderDetailId =" & salesOrderDetailId & " Group by SalesOrderDetailId"
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows.Item(0).Item(1).ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtDeliverQty_TextChanged(sender As Object, e As EventArgs) Handles txtDeliverQty.TextChanged
        'Try
        '    If Val(Me.txtRemQty.Text) > 0 Then
        '        If Val(Me.txtDeliverQty.Text) < Val(Me.txtRemQty.Text) Then
        '            Me.txtRemQty.Text = Val(Me.txtRemQty.Text) - Val(Me.txtDeliverQty.Text)
        '        Else
        '            msg_Error("Entered quantity is larger than remaining quantity")
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtSpecialInstructions_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then
                UpdateSI(Me.txtSpecialInstructions.Text, Val(Me.grdSaved.GetRow.Cells("SalesOrderId").Value.ToString))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub UpdateSI(ByVal SI As String, ByVal SalesOrderId As Integer)
        Dim cmd As New OleDbCommand
        Dim connection As OleDbConnection
        connection = Con
        If connection.State = ConnectionState.Open Then connection.Close()
        connection.Open()
        Dim trans As OleDbTransaction = connection.BeginTransaction
        Try
            cmd.CommandType = CommandType.Text
            cmd.Connection = connection
            cmd.Transaction = trans
            cmd.CommandText = "Update SalesOrderMasterTable Set SpecialInstructions =N'" & SI.ToString & "' Where SalesOrderId = " & SalesOrderId & ""
            cmd.ExecuteNonQuery()
            trans.Commit()
            connection.Close()
        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave1_Click(sender As Object, e As EventArgs) Handles btnSave1.Click
        Try
            SaveToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs)

    End Sub
    Private Sub DeleteDetailRow(ByVal SalesOrderDetailId As Integer)
        Dim connection As New OleDb.OleDbConnection
        Dim cmd As New OleDb.OleDbCommand
        connection = Con
        If connection.State = ConnectionState.Open Then connection.Close()
        connection.Open()
        Dim trans As OleDb.OleDbTransaction = connection.BeginTransaction
        Try
            cmd.CommandType = CommandType.Text
            cmd.Connection = connection
            cmd.Transaction = trans
            cmd.CommandText = "Delete From SalesOrderDetailTable Where SalesOrderDetailId = " & SalesOrderDetailId & ""
            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub

    'Private Function MakeAliasAsAnArticle() As Boolean
    '    Try
    '        FillArticleModel()
    '        objArticleDAL.Add(objArticle)
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Private Sub MakeAliasAsAnArticle()
        Try
            'Detail.PurchaseInquiryDetailId,  Detail.PurchaseInquiryId, Detail.RequirementDescription,  Detail.UnitId, Unit.ArticleUnitName As Unit,  Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, IsNull(Detail.SalesInquiryDetailId, 0)-IsNull(Detail.PurchaseInquiryDetailId, 0)-Detail.ReferenceNo As Code
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
            '    'For I As Integer = 0 To Me.grd.GetCheckedRows.Length
            '    If Val(Row.Cells("QuotationDetailId").Value) > 0 AndAlso Row.Cells("ArticleAliasName").Value.ToString.Length > 0 AndAlso Row.Cells("item").Value.ToString = "" Then
            '        Dim dt As DataTable = dalPurchaseInquiryDetail.GetSingleArticle(Val(Row.Cells("QuotationDetailId").Value))
            '        objArticle = New Article()
            '        Dim objArticleDetail As New ArticleDetail
            '        objArticle.ArticleCode = dt.Rows(0).Item("Code").ToString
            '        objArticle.ArticleDescription = dt.Rows(0).Item("RequirementDescription").ToString
            '        objArticle.ArticleUnitID = Val(dt.Rows(0).Item("UnitId").ToString)
            '        objArticle.ArticleTypeID = Val(dt.Rows(0).Item("ItemTypeId").ToString)
            '        objArticle.ArticleCategoryId = Val(dt.Rows(0).Item("CategoryId").ToString)
            '        objArticle.ArticleLPOID = Val(dt.Rows(0).Item("SubCategoryId").ToString)
            '        objArticle.ArticleGenderID = Val(dt.Rows(0).Item("OriginId").ToString)
            '        objArticle.IsDate = Date.Today
            '        objArticle.ArticleRemarks = ""
            '        objArticle.ArticlePicture = ""
            '        objArticle.HS_Code = ""
            '        ''Detail
            '        objArticle.ArticleDetails = New List(Of ArticleDetail)
            '        objArticleDetail.ArticleCode = dt.Rows(0).Item("Code").ToString
            '        objArticleDetail.ArticleDescription = dt.Rows(0).Item("RequirementDescription").ToString
            '        objArticleDetail.ArticleUnitID = Val(dt.Rows(0).Item("UnitId").ToString)
            '        objArticleDetail.ArticleTypeID = Val(dt.Rows(0).Item("ItemTypeId").ToString)
            '        objArticleDetail.ArticleCategoryId = Val(dt.Rows(0).Item("CategoryId").ToString)
            '        objArticleDetail.ArticleLPOID = Val(dt.Rows(0).Item("SubCategoryId").ToString)
            '        objArticleDetail.ArticleGenderID = Val(dt.Rows(0).Item("OriginId").ToString)
            '        objArticleDetail.IsDate = Date.Today
            '        'objArticleDetail. = ""
            '        'objArticleDetail.ArticlePicture = ""
            '        objArticleDetail.HS_Code = ""
            '        objArticle.ArticleDetails.Add(objArticleDetail)
            '        ''
            '        ''filling Aritcal Location Rank Model
            '        objArticle.ArticalLocationRank = New List(Of ArticalLocationRank)
            '        Dim objALR As ArticalLocationRank
            '        objALR = New ArticalLocationRank
            '        objALR.LocationID = 0
            '        objALR.Rank = ""
            '        ''Increment Reduction
            '        objArticle.IncrementReduction = New IncrementReduction
            '        objArticle.IncrementReduction.IncrementDate = Date.Today
            '        objArticle.IncrementReduction.Stock = 0
            '        objArticle.IncrementReduction.PurchasePriceOld = 0
            '        objArticle.IncrementReduction.SalePriceOld = 0
            '        '' Activity Log
            '        objArticle.ActivityLog = New ActivityLog
            '        objArticle.ActivityLog.ApplicationName = "Sales"
            '        objArticle.ActivityLog.FormCaption = Me.Text
            '        objArticle.ActivityLog.FormName = ""

            '        'TODO : Define Loging User ID
            '        '.ActivityLog.UserID = 1
            '        objArticle.ActivityLog.UserID = LoginUserId
            '        objArticle.ActivityLog.RefNo = dt.Rows(0).Item("Code").ToString
            '        objArticle.ActivityLog.LogDateTime = Date.Now
            '        objArticleDAL.Add(objArticle)
            '        '' Add new item to grid
            '        'Dim filter As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grd.RootTable.Columns("ArticleAliasName"), Janus.Windows.GridEX.ConditionOperator.Contains, Row.Cells("ArticleAliasName").Value.ToString)
            '        'Me.grd.RootTable.FilterCondition = filter

            '    End If
            'Next
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
            Me.grd.UpdateData()
            For I As Integer = 0 To Me.grd.GetCheckedRows.Length - 1
                If Val(Me.grd.GetRows(I).Cells("QuotationDetailId").Value.ToString) > 0 AndAlso Me.grd.GetRows(I).Cells("ArticleAliasName").Value.ToString.Length > 0 AndAlso Me.grd.GetRows(I).Cells("item").Value.ToString = "" Then
                    Dim dt As DataTable = dalPurchaseInquiryDetail.GetSingleArticle(Val(Me.grd.GetRows(I).Cells("QuotationDetailId").Value))
                    If dt.Rows.Count > 0 Then
                        If objArticleDAL.IsAlreadyAliasExists(dt.Rows(0).Item("RequirementDescription").ToString) Then
                            objArticle = New Article()
                            Dim objArticleDetail As New ArticleDetail
                            objArticle.ArticleCode = dt.Rows(0).Item("Code").ToString
                            objArticle.ArticleDescription = dt.Rows(0).Item("RequirementDescription").ToString
                            objArticle.ArticleUnitID = Val(dt.Rows(0).Item("UnitId").ToString)
                            objArticle.ArticleTypeID = Val(dt.Rows(0).Item("ItemTypeId").ToString)
                            objArticle.ArticleCategoryId = Val(dt.Rows(0).Item("CategoryId").ToString)
                            objArticle.ArticleLPOID = Val(dt.Rows(0).Item("SubCategoryId").ToString)
                            objArticle.ArticleGenderID = Val(dt.Rows(0).Item("OriginId").ToString)
                            objArticle.IsDate = Date.Today
                            objArticle.ArticleRemarks = ""
                            objArticle.ArticlePicture = ""
                            objArticle.HS_Code = ""
                            ''Detail
                            objArticle.ArticleDetails = New List(Of ArticleDetail)
                            objArticleDetail.ArticleCode = dt.Rows(0).Item("Code").ToString
                            objArticleDetail.ArticleDescription = dt.Rows(0).Item("RequirementDescription").ToString
                            objArticleDetail.ArticleUnitID = Val(dt.Rows(0).Item("UnitId").ToString)
                            objArticleDetail.ArticleTypeID = Val(dt.Rows(0).Item("ItemTypeId").ToString)
                            objArticleDetail.ArticleCategoryId = Val(dt.Rows(0).Item("CategoryId").ToString)
                            objArticleDetail.ArticleLPOID = Val(dt.Rows(0).Item("SubCategoryId").ToString)
                            objArticleDetail.ArticleGenderID = Val(dt.Rows(0).Item("OriginId").ToString)
                            objArticleDetail.IsDate = Date.Today
                            'objArticleDetail. = ""
                            'objArticleDetail.ArticlePicture = ""
                            objArticleDetail.HS_Code = ""
                            objArticle.ArticleDetails.Add(objArticleDetail)
                            ''
                            ''filling Aritcal Location Rank Model
                            objArticle.ArticalLocationRank = New List(Of ArticalLocationRank)
                            Dim objALR As ArticalLocationRank
                            objALR = New ArticalLocationRank
                            objALR.LocationID = 0
                            objALR.Rank = ""
                            ''Increment Reduction
                            objArticle.IncrementReduction = New IncrementReduction
                            objArticle.IncrementReduction.IncrementDate = Date.Today
                            objArticle.IncrementReduction.Stock = 0
                            objArticle.IncrementReduction.PurchasePriceOld = 0
                            objArticle.IncrementReduction.SalePriceOld = 0
                            '' Activity Log
                            objArticle.ActivityLog = New ActivityLog
                            objArticle.ActivityLog.ApplicationName = "Sales"
                            objArticle.ActivityLog.FormCaption = Me.Text
                            objArticle.ActivityLog.FormName = ""

                            'TODO : Define Loging User ID
                            '.ActivityLog.UserID = 1
                            objArticle.ActivityLog.UserID = LoginUserId
                            objArticle.ActivityLog.RefNo = dt.Rows(0).Item("Code").ToString
                            objArticle.ActivityLog.LogDateTime = Date.Now
                            objArticleDAL.Add(objArticle)
                            Me.grd.GetRows(I).BeginEdit()
                            Me.grd.GetRows(I).Cells("ArticleCode").Value = dt.Rows(0).Item("Code").ToString
                            Me.grd.GetRows(I).Cells("item").Value = dt.Rows(0).Item("RequirementDescription").ToString
                            Me.grd.GetRows(I).Cells("ArticleDefId").Value = ArticleDAL.ArticleIdForArticleAlias
                            ArticleDAL.ArticleIdForArticleAlias = 0
                            Me.grd.GetRows(I).EndEdit()
                            msg_Information("Item '" & dt.Rows(0).Item("RequirementDescription").ToString & "' has been created.")
                        Else
                            Dim dtcreatedItem As DataTable = objArticleDAL.GetCreatedAlias(dt.Rows(0).Item("RequirementDescription").ToString)
                            Me.grd.GetRows(I).BeginEdit()
                            Me.grd.GetRows(I).Cells("ArticleCode").Value = dtcreatedItem.Rows(0).Item("ArticleCode").ToString
                            Me.grd.GetRows(I).Cells("item").Value = dtcreatedItem.Rows(0).Item("ArticleDescription").ToString
                            Me.grd.GetRows(I).Cells("ArticleDefId").Value = Val(dtcreatedItem.Rows(0).Item("ArticleId").ToString)
                            ArticleDAL.ArticleIdForArticleAlias = 0
                            Me.grd.GetRows(I).EndEdit()
                            ShowErrorMessage("Item Already Exist " & vbCrLf & dt.Rows(0).Item("Code").ToString + "-" + dt.Rows(0).Item("RequirementDescription").ToString)
                        End If
                    End If
                ElseIf Me.grd.GetRows(I).Cells("ArticleAliasName").Value.ToString.Length > 0 AndAlso Me.grd.GetRows(I).Cells("item").Value.ToString = "" Then
                    'Dim dt As DataTable = dalPurchaseInquiryDetail.GetSingleArticle(Val(Me.grd.GetRows(I).Cells("QuotationDetailId").Value))
                    If objArticleDAL.IsAlreadyAliasExists(Me.grd.GetRows(I).Cells("ArticleAliasName").Value.ToString) Then
                        objArticle = New Article()
                        Dim objArticleDetail As New ArticleDetail
                        objArticle.ArticleCode = ""
                        objArticle.ArticleDescription = Me.grd.GetRows(I).Cells("ArticleAliasName").Value.ToString
                        objArticle.ArticleUnitID = 0
                        objArticle.ArticleTypeID = 0
                        objArticle.ArticleCategoryId = 0
                        objArticle.ArticleLPOID = 0
                        objArticle.ArticleGenderID = 0
                        objArticle.IsDate = Date.Today
                        objArticle.ArticleRemarks = ""
                        objArticle.ArticlePicture = ""
                        objArticle.HS_Code = ""
                        ''Detail
                        objArticle.ArticleDetails = New List(Of ArticleDetail)
                        objArticleDetail.ArticleCode = ""
                        objArticleDetail.ArticleDescription = Me.grd.GetRows(I).Cells("ArticleAliasName").Value.ToString
                        objArticleDetail.ArticleUnitID = 0
                        objArticleDetail.ArticleTypeID = 0
                        objArticleDetail.ArticleCategoryId = 0
                        objArticleDetail.ArticleLPOID = 0
                        objArticleDetail.ArticleGenderID = 0
                        objArticleDetail.IsDate = Date.Today
                        'objArticleDetail. = ""
                        'objArticleDetail.ArticlePicture = ""
                        objArticleDetail.HS_Code = ""
                        objArticle.ArticleDetails.Add(objArticleDetail)
                        ''
                        ''filling Aritcal Location Rank Model
                        objArticle.ArticalLocationRank = New List(Of ArticalLocationRank)
                        Dim objALR As ArticalLocationRank
                        objALR = New ArticalLocationRank
                        objALR.LocationID = 0
                        objALR.Rank = ""
                        ''Increment Reduction
                        objArticle.IncrementReduction = New IncrementReduction
                        objArticle.IncrementReduction.IncrementDate = Date.Today
                        objArticle.IncrementReduction.Stock = 0
                        objArticle.IncrementReduction.PurchasePriceOld = 0
                        objArticle.IncrementReduction.SalePriceOld = 0
                        '' Activity Log
                        objArticle.ActivityLog = New ActivityLog
                        objArticle.ActivityLog.ApplicationName = "Sales"
                        objArticle.ActivityLog.FormCaption = Me.Text
                        objArticle.ActivityLog.FormName = ""

                        'TODO : Define Loging User ID
                        '.ActivityLog.UserID = 1
                        objArticle.ActivityLog.UserID = LoginUserId
                        objArticle.ActivityLog.RefNo = ""
                        objArticle.ActivityLog.LogDateTime = Date.Now
                        objArticleDAL.Add(objArticle)
                        Me.grd.GetRows(I).BeginEdit()
                        Me.grd.GetRows(I).Cells("ArticleCode").Value = ""
                        Me.grd.GetRows(I).Cells("item").Value = Me.grd.GetRows(I).Cells("ArticleAliasName").Value.ToString
                        Me.grd.GetRows(I).Cells("ArticleDefId").Value = ArticleDAL.ArticleIdForArticleAlias
                        ArticleDAL.ArticleIdForArticleAlias = 0
                        Me.grd.GetRows(I).EndEdit()
                        msg_Information("Item '" & Me.grd.GetRows(I).Cells("ArticleAliasName").Value.ToString & "' has been created.")
                    Else
                        Dim dtcreatedItem As DataTable = objArticleDAL.GetCreatedAlias(Me.grd.GetRows(I).Cells("ArticleAliasName").Value.ToString)
                        Me.grd.GetRows(I).BeginEdit()
                        Me.grd.GetRows(I).Cells("ArticleCode").Value = dtcreatedItem.Rows(0).Item("ArticleCode").ToString
                        Me.grd.GetRows(I).Cells("item").Value = dtcreatedItem.Rows(0).Item("ArticleDescription").ToString
                        Me.grd.GetRows(I).Cells("ArticleDefId").Value = Val(dtcreatedItem.Rows(0).Item("ArticleId").ToString)
                        ArticleDAL.ArticleIdForArticleAlias = 0
                        Me.grd.GetRows(I).EndEdit()
                        ShowErrorMessage("Item Already Exist " & vbCrLf & "" + Me.grd.GetRows(I).Cells("ArticleAliasName").Value.ToString)
                    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                If IsEditMode = True Then
                Else
                    Me.txtCurrencyRate.Text = Math.Round(Convert.ToDouble(dr.Row.Item("CurrencyRate").ToString), 4)
                End If
                ''TASK TFS1474

                ''Below four lines are commented against TASK TFS3700
                If Me.cmbCurrency.SelectedValue = BaseCurrencyId Then
                    Me.txtCurrencyRate.Enabled = False
                Else
                    Me.txtCurrencyRate.Enabled = True

                End If
                ''END TASK TFS1474

                ' R@!    11-Jun-2016 Dollor account
                ' Setting default rate to zero
                If Val(Me.txtCurrencyRate.Text) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If
                '' TASK TFS3700
                If CurrencyRate > 1 Then
                    Me.txtCurrencyRate.Text = CurrencyRate
                End If


                ''END TASK 

                'R@!    11-Jun-2016 Dollor account
                'Code Commented
                ' Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
                ' Added 2 coloumns and changed caption
                Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Caption = "Amount (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns(GrdEnum.TotalCurrencyAmount).Caption = "Total Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Caption = "Tax Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencySEDAmount).Caption = "SED Amount (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns("Total Amount").Caption = "Total Amount (" & Me.BaseCurrencyName & ")"
                'Me.grd.RootTable.Columns("Total Amount").Caption = "Total Amount (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns("Total").Caption = "Total (" & Me.BaseCurrencyName & ")"

                'grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.TotalCurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencySEDAmount).Visible = False


                    If IsEditMode = False Then
                        If Me.grd.RowCount > 0 Then
                            For Each GriDEX As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                                GriDEX.BeginEdit()
                                GriDEX.Cells("CurrencyRate").Value = Val(Me.txtCurrencyRate.Text)
                                GriDEX.Cells("CurrencyId").Value = Me.cmbCurrency.SelectedValue
                                GriDEX.Cells("BaseCurrencyId").Value = BaseCurrencyId
                                GriDEX.Cells("BaseCurrencyRate").Value = 1
                                GriDEX.EndEdit()
                            Next
                        End If
                    End If

                    'Me.grd.RootTable.Columns(EnumGrid.Credit).Visible = False
                Else
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = True
                    'Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.TotalCurrencyAmount).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencySEDAmount).Visible = False
                    If IsEditMode = False Then
                        If Me.grd.RowCount > 0 Then
                            For Each GriDEX As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                                GriDEX.BeginEdit()
                                GriDEX.Cells("CurrencyRate").Value = Val(Me.txtCurrencyRate.Text)
                                GriDEX.Cells("CurrencyId").Value = Me.cmbCurrency.SelectedValue
                                GriDEX.Cells("BaseCurrencyId").Value = BaseCurrencyId
                                GriDEX.Cells("BaseCurrencyRate").Value = 1
                                GriDEX.EndEdit()
                            Next
                        End If
                    End If
                    'Me.grd.RootTable.Columns(EnumGridDetail.Curr = Val(Me.txtCurrencyRate.Text)
                    'Me.grd.RootTable.Columns(EnumGrid.Credit).Visible = True
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetCurrencyRate(ByVal currencyId As Integer) As Double ''TAKS-407
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Dim currencyRate As Double = 0
        Try
            str = " Select CurrencyRate, CurrencyId From tblCurrencyRate Where CurrencyRateId in ( Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId) And CurrencyId = " & currencyId & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                currencyRate = Val(dt.Rows.Item(0).Item(0).ToString)
            End If
            Return currencyRate
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmSalesOrderNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnMakeAliasAnItem_Click(sender As Object, e As EventArgs) Handles btnMakeAliasAnItem.Click
        Try
            MakeAliasAsAnArticle()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetSingle(ByVal SalesOrderID As Integer) As DataTable
        Dim Str As String = ""
        Try
            Str = "SELECT  Recv.SalesOrderNo, Recv.SalesOrderDate, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty, " & _
                        "Recv.SalesOrderAmount, Recv.SalesOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, Recv.PoNo, isnull(Recv.SOP_ID,0) as SOP_ID, tblDefEmployee.Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CstCntr.Name, dbo.vwCOADetail.Contact_Email as Email,IsNull(Recv.SaleOrderStatusId,0) as SaleOrderStatusId, SalesOrderStatusTable.OrderStatus as [Order Status], IsNull(Recv.QuotationId,0) as QuotationId, Quot.QuotationNo, Recv.Terms_And_Condition, recv.UserName, Recv.UpdateUserName, IsNull(Recv.SaleOrderTypeId,0) as SaleOrderTypeId, SalesOrderTypeTable.SalesOrderTypeName as [Order Type], Recv.SpecialInstructions, IsNull(Recv.DispatchToLocation, 0) As DispatchToLocation, IsNull(Recv.InvoiceToLocation, 0) As InvoiceToLocation, " & _
                        "IsNull(Recv.TechnicalDrawingNumber, 0) As TechnicalDrawingNumber, Recv.TechnicalDrawingDate, Recv.AccountsRemarks, Recv.StoreRemarks, Recv.ProductionRemarks, Recv.ServicesRemarks, Recv.SalesRemarks, Recv.TermOfPayments " & _
                        " FROM tblDefCostCenter CstCntr Right Outer Join dbo.SalesOrderMasterTable Recv ON CstCntr.CostCenterID=Recv.CostCenterId LEFT OUTER JOIN " & _
                        "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblDefEmployee ON tblDefEmployee.Employee_ID = Recv.SOP_ID  LEFT OUTER JOIN QuotationMasterTable Quot On Quot.QuotationId = Recv.QuotationId  LEFT OUTER JOIN  SalesOrderStatusTable on SalesOrderStatusTable.OrderStatusID = Recv.SaleOrderStatusID LEFT OUTER JOIN SalesOrderTypeTable on SalesOrderTypeTable.SaleOrderTypeId = Recv.SaleOrderTypeId WHERE Recv.SalesOrderNo IS NOT NULL And Recv.SalesOrderId = " & SalesOrderID & "  "
            Dim dt As DataTable = GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function ReplaceNewLine(ByVal selContent As String, ByVal isReplacingNewLineWithChar As Boolean, Optional ByVal selNewLineStringToUse As String = ".:.myCooLvbNewLine.:.") As String
        Try
            If isReplacingNewLineWithChar Then : Return selContent.Replace(vbNewLine, selNewLineStringToUse)
            Else : Return selContent.Replace(selNewLineStringToUse, vbNewLine)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub FillOutwardExpense(ReceivingID As Integer, Optional DocType As String = "")
        Try
            Dim dtOutwardExpDetail As DataTable = GetDataTable("Select Exp.SalesId, tblDefOutwardAccounts.OutwardAccountId as AccountId,vw.Detail_Title,isnull(Exp_Amount,0) As Exp_Amount From vwCoaDetail vw INNER JOIN tblDefOutwardAccounts on tblDefOutwardAccounts.OutwardAccountId = vw.coa_detail_id LEFT OUTER JOIN (select SalesId,AccountId,Exp_Amount From OutwardExpenseDetailTable WHERE SalesId=" & ReceivingID & " AND DocType='" & DocType.Replace("'", "''") & "') Exp ON Exp.AccountId = vw.coa_detail_id")
            dtOutwardExpDetail.AcceptChanges()
            Me.grdOutwardExpDetail.DataSource = Nothing
            Me.grdOutwardExpDetail.DataSource = dtOutwardExpDetail
            Me.grdOutwardExpDetail.RootTable.Columns("Exp_Amount").FormatString = "N" & DecimalPointInValue
            Me.grdOutwardExpDetail.RootTable.Columns("Exp_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdOutwardExpDetail.RootTable.Columns("Exp_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnAddExp_Click(sender As Object, e As EventArgs) Handles btnAddExp.Click
        Try
            If Me.cmbOutwardExpense.IsItemInList = False Then Exit Sub
            If Me.cmbOutwardExpense.ActiveRow Is Nothing Then Exit Sub
            If Con Is Nothing Then Exit Sub
            If Me.grdOutwardExpDetail.RowCount > 0 Then
                Dim bln As Boolean = Me.grdOutwardExpDetail.Find(Me.grdOutwardExpDetail.RootTable.Columns("AccountId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbOutwardExpense.Value, 0, 1)
                If bln = True Then
                    ShowErrorMessage("Account is already added")
                    Exit Sub
                End If
            End If
            Dim objcon As New OleDbConnection(Con.ConnectionString)
            Dim cmd As New OleDbCommand
            If objcon.State = ConnectionState.Closed Then objcon.Open()
            Dim objtrans As OleDbTransaction = objcon.BeginTransaction
            cmd.Connection = objcon
            cmd.Transaction = objtrans
            cmd.CommandType = CommandType.Text
            cmd.CommandText = String.Empty
            cmd.CommandText = "INSERT INTO tblDefOutwardAccounts(OutwardAccountId,Active) VALUES(" & Me.cmbOutwardExpense.Value & ",1) Select @@Identity"
            cmd.ExecuteNonQuery()
            objtrans.Commit()
            objcon.Close()
            objtrans.Dispose()
            objcon.Dispose()
            Dim dt As DataTable = CType(Me.grdOutwardExpDetail.DataSource, DataTable)
            dt.AcceptChanges()
            Dim dr As DataRow
            dr = dt.NewRow
            If Me.BtnSave.Text <> "&Save" Then
                dr(0) = Val(Me.grdSaved.GetRow.Cells("SalesOrderId").Value.ToString)
            Else
                dr(0) = 0
            End If
            dr(1) = Me.cmbOutwardExpense.Value
            dr(2) = Me.cmbOutwardExpense.ActiveRow.Cells("Account Title").Value.ToString
            dr(3) = 0
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdOutwardExpDetail_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdOutwardExpDetail.ColumnButtonClick
        Try
            Dim objcon As New OleDbConnection(Con.ConnectionString)
            Dim cmd As New OleDbCommand
            If objcon.State = ConnectionState.Closed Then objcon.Open()
            Dim objtrans As OleDbTransaction = objcon.BeginTransaction
            cmd.Connection = objcon
            cmd.Transaction = objtrans
            cmd.CommandType = CommandType.Text
            cmd.CommandText = String.Empty
            cmd.CommandText = "Delete From tblDefOutwardAccounts  WHERE OutwardAccountId=" & Val(grdOutwardExpDetail.GetRow.Cells("AccountId").Value.ToString) & ""
            cmd.ExecuteNonQuery()
            objtrans.Commit()
            objcon.Close()
            objtrans.Dispose()
            objcon.Dispose()
            Me.grdOutwardExpDetail.GetRow.Delete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabPageControl2_Paint(sender As Object, e As PaintEventArgs) Handles UltraTabPageControl2.Paint

    End Sub
    ''' <summary>
    ''' TASK TFS1648 done by Ameen on 03-11-2017
    ''' </summary>
    ''' <param name="ArticleId"></param>
    ''' <param name="Rate"></param>
    ''' <param name="Pack"></param>
    ''' <param name="PackQty"></param>
    ''' <param name="SO_Id"></param>
    ''' <param name="LocationID"></param>
    ''' <returns></returns>
    ''' <remarks> Quantity will be sumed in case item, location, Pack, Unit and Rate are same over addition of new row </remarks>
    Private Function FindExistsItem(ByVal ArticleId As Integer, ByVal Rate As Double, ByVal Pack As String, ByVal PackQty As Double, ByVal SO_Id As Integer, ByVal LocationID As Integer) As Boolean
        Try
            'Task:2432 Added Flg Marge Item
            If flgMargeItem = True Then
                Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
                Dim dr() As DataRow
                dr = dt.Select("ArticleDefId=" & ArticleId & " And Price=" & Val(Rate) & " AND unit='" & Pack & "' AND PackQty=" & Val(PackQty) & " AND LocationId =" & LocationID & " ")
                If dr.Length > 0 Then
                    For Each r As DataRow In dr
                        'If dr(0).ItemArray(0) = r.ItemArray(11) AndAlso dr(0).ItemArray(11) = r.ItemArray(11) AndAlso dr(0).ItemArray(8) = r.ItemArray(8) AndAlso dr(0).ItemArray(5) = r.ItemArray(5) Then
                        '    r.BeginEdit()
                        '    r(6) = Val(dr(0).ItemArray(6)) + Val(Me.txtQty.Text)
                        '    'r("LoadQty") = Val(dr(0).ItemArray(6)) + Val(Me.txtQty.Text)
                        '    r("TotalQty") = r(6)
                        '    r.EndEdit()
                        'End If
                        If dr(0).Item("LocationId") = LocationID AndAlso dr(0).Item("Price") = r.Item("Price") AndAlso dr(0).Item("unit") = r.Item("unit") AndAlso dr(0).Item("PackQty") = r.Item("PackQty") Then
                            If dr(0).Item("unit") = "Pack" Then
                                r.BeginEdit()
                                r("Qty") = Math.Round(Val(dr(0).Item("Qty")) + Val(Me.txtQty.Text), TotalAmountRounding)
                                r("TotalQuantity") = Math.Round(r("Qty") * Val(Me.txtPackQty.Text), TotalAmountRounding)
                                r.EndEdit()
                            Else
                                r.BeginEdit()
                                r("Qty") = Val(dr(0).Item("Qty")) + Val(Me.txtQty.Text)
                                r("TotalQuantity") = r("Qty")
                                r.EndEdit()
                            End If

                        End If
                    Next
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
            'End Task:2432
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbVendor_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbVendor.KeyDown
        Try
            ''TFS1781 : Ayesha Rehman :Added for Selection of Customer or Vendor
            If e.KeyCode = Keys.F1 Then
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    'frmSearchCustomersVendors.rbtCustomers.Checked = True
                    'frmSearchCustomersVendors.rbtVendors.Checked = True
                    'frmSearchCustomersVendors.rbtCustomers.Visible = True
                    'frmSearchCustomersVendors.rbtVendors.Visible = 
                    frmAccountSearch.AccountType = "'Vendor','Customer' "
                Else
                    'frmSearchCustomersVendors.rbtCustomers.Checked = True
                    'frmSearchCustomersVendors.rbtVendors.Checked = False
                    'frmSearchCustomersVendors.rbtCustomers.Visible = True
                    'frmSearchCustomersVendors.rbtVendors.Visible = False
                    frmAccountSearch.AccountType = "'Customer'"
                End If

                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbVendor.Value = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub DiscountCalculation()
        Try
            Dim Rate As Double = 0D


            'Rate = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            'Rate = Val(txtRate.Text)
            Rate = Val(Me.txtPDP.Text)

            If Val(Me.txtDisc.Text) <> 0 AndAlso Rate > 0 Then
                Dim discount As Double = 0D

                If Me.cmbDiscountType.Text.Equals(DiscountType_Percentage) Then
                    discount = (Val(Rate) * Val(Me.txtDisc.Text)) / 100
                    Me.txtDiscountValue.Text = Math.Round(discount, TotalAmountRounding)
                Else
                    discount = Val(Me.txtDisc.Text)
                    Me.txtDiscountValue.Text = Math.Round(discount, TotalAmountRounding)
                End If
                Me.txtRate.Text = Val(Rate) - Math.Round(discount, 2).ToString()

            Else
                Me.txtRate.Text = Rate
                Me.txtDiscountValue.Text = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbDiscountType_Leave(sender As Object, e As EventArgs)
        Try
            DiscountCalculation()
            GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' This Event is Added for Assing the value of PDP to Rate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Ayesha Rehman : 29-03-2017 :TFS2827 </remarks>
    Private Sub txtPDP_Leave(sender As Object, e As EventArgs) Handles txtPDP.Leave
        Try
            txtRate.Text = Val(txtPDP.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' This Event is Added for Assing the value of PDP to Rate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Ayesha Rehman : 29-03-2017 :TFS2827</remarks>
    Private Sub txtPDP_TextChanged(sender As Object, e As EventArgs) Handles txtPDP.TextChanged
        Try
            txtRate.Text = Val(txtPDP.Text)
            DiscountCalculation()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnApprovalHistory_Click(sender As Object, e As EventArgs) Handles btnApprovalHistory.Click
        Try
            frmApprovalHistory.DocumentNo = Me.txtPONo.Text
            frmApprovalHistory.Source = Me.Name
            frmApprovalHistory.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub cmbVendor_LostFocus1(sender As Object, e As EventArgs) Handles cmbVendor.LostFocus
        Try
            If IsFormOpen = True Then
                If Me.RadioButton2.Checked = True Then
                    If Me.cmbVendor.IsItemInList = False Then Exit Sub
                    FillCombo("Item")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSpecialAdjustment_LostFocus1(sender As Object, e As EventArgs) Handles txtSpecialAdjustment.LostFocus
        Try
            If grd.RootTable IsNot Nothing Then Me.grd.UpdateData()
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotal_LostFocus1(sender As Object, e As EventArgs) Handles txtTotal.LostFocus
        Try


            'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtQty.Text) = 0 Then
            '    Me.txtQty.Text = Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)
            'End If

            'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtQty.Text) <> 0 AndAlso Val(Me.txtRate.Text) = 0 Then
            '    Me.txtRate.Text = Val(Me.txtTotal.Text) / Val(Me.txtQty.Text)
            'End If

            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    txtNetTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            'Else
            '    txtNetTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            'End If
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            If Not Me.cmbItem.ActiveRow.Cells(0).Value > 0 Or Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub CtrlGrdBar4_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar4.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar4.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Order New"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar5_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar5.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar5.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Order New"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar6_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar6.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDeliverySchedule.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDeliverySchedule.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdDeliverySchedule.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar6.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Order New"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' This Sub Is Made to Change PDP ,When the Total Amount is Changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TFS3330 : Ayesha Rehman : 23-05-2018</remarks>
    Private Sub txtTotal_Leave(sender As Object, e As EventArgs) Handles txtTotal.Leave, txtTotal.MouseLeave
        Try
            '' Below lines are added against TASK TFS3330  : Ayesha Rehman
            If Me.cmbUnit.Text = "Pack" Then
                If Val(txtTotal.Text) <> Total Then
                    If Val(Me.txtTotal.Text) > 0 AndAlso Val(Me.txtTotalQuantity.Text) > 0 Then
                        Me.txtPDP.Text = Math.Round((Val(Me.txtTotal.Text) / Val(Me.txtTotalQuantity.Text)), TotalAmountRounding)
                        Total = 0
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS3700
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCurrencyRate_TextChanged(sender As Object, e As EventArgs) Handles txtCurrencyRate.TextChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                'Me.txtCurrencyRate.Text = Math.Round(Convert.ToInt32(dr.Row.Item("CurrencyRate").ToString), TotalAmountRounding)
                ''TASK TFS1474

                ''Below four lines are commented against TASK TFS3700
                If Me.cmbCurrency.SelectedValue = BaseCurrencyId Then
                    Me.txtCurrencyRate.Enabled = False
                    Me.txtCurrencyRate.Text = 1
                Else
                    Me.txtCurrencyRate.Enabled = True

                End If
                ''END TASK TFS1474

                ' R@!    11-Jun-2016 Dollor account
                ' Setting default rate to zero
                If Val(Me.txtCurrencyRate.Text) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If

                'R@!    11-Jun-2016 Dollor account
                'Code Commented
                ' Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
                ' Added 2 coloumns and changed caption
                Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Caption = "Amount (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns(GrdEnum.TotalCurrencyAmount).Caption = "Total Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Caption = "Tax Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencySEDAmount).Caption = "SED Amount (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns("Total Amount").Caption = "Total Amount (" & Me.BaseCurrencyName & ")"
                'Me.grd.RootTable.Columns("Total Amount").Caption = "Total Amount (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns("Total").Caption = "Total (" & Me.BaseCurrencyName & ")"

                'grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.TotalCurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencySEDAmount).Visible = False
                    If Me.grd.RowCount > 0 Then
                        For Each GriDEX As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                            GriDEX.BeginEdit()
                            GriDEX.Cells("CurrencyRate").Value = Val(Me.txtCurrencyRate.Text)
                            GriDEX.Cells("CurrencyId").Value = Me.cmbCurrency.SelectedValue
                            GriDEX.Cells("BaseCurrencyId").Value = BaseCurrencyId
                            GriDEX.Cells("BaseCurrencyRate").Value = 1
                            GriDEX.EndEdit()
                        Next
                    End If
                    'Me.grd.RootTable.Columns(EnumGrid.Credit).Visible = False
                Else
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = True
                    'Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.TotalCurrencyAmount).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencySEDAmount).Visible = False
                    'If IsEditMode = False Then
                    If Me.grd.RowCount > 0 Then
                        For Each GriDEX As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                            GriDEX.BeginEdit()
                            GriDEX.Cells("CurrencyRate").Value = Val(Me.txtCurrencyRate.Text)
                            GriDEX.Cells("CurrencyId").Value = Me.cmbCurrency.SelectedValue
                            GriDEX.Cells("BaseCurrencyId").Value = BaseCurrencyId
                            GriDEX.Cells("BaseCurrencyRate").Value = 1
                            GriDEX.EndEdit()
                        Next
                    End If
                    'End If
                    'Me.grd.RootTable.Columns(EnumGridDetail.Curr = Val(Me.txtCurrencyRate.Text)
                    'Me.grd.RootTable.Columns(EnumGrid.Credit).Visible = True
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRemoveAttachment_Click(sender As Object, e As EventArgs) Handles btnRemoveAttachment.Click
        Try
            If Me.BtnSave.Text = "&Update" Then
                arrFile = New List(Of String)
                Me.btnAttachment.Text = "Attachment"
                RemoveAttachmentsFromDirectory(Val(txtReceivingID.Text), Me.Name)
                RemoveAttachments(Val(txtReceivingID.Text), Me.Name)
            Else
                arrFile = New List(Of String)
                Me.btnAttachment.Text = "Attachment"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_Click(sender As Object, e As EventArgs) Handles grd.Click
        Try

            'If Me.grd.RowCount > 0 AndAlso Me.grd.GetRow.Cells(GrdEnum.ItemId).Value IsNot Nothing Then
            '    Dim str As String = ""
            '    str = " Select  BatchNo,BatchNo,ExpiryDate  From  StockDetailTable  where BatchNo not in ('','0','xxxx')  And ArticledefId = " & Me.grd.GetRow.Cells(GrdEnum.ItemId).Value & "  Group by BatchNo,ExpiryDate Having Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0)) > 0  ORDER BY ExpiryDate Asc"
            '    Dim dt As DataTable = GetDataTable(str)
            '    Me.grd.RootTable.Columns(GrdEnum.BatchNo).ValueList.PopulateValueList(dt.DefaultView, "BatchNo", "BatchNo")
            '    If Not dt.Rows.Count > 0 Then
            '        grd.GetRow.Cells(GrdEnum.BatchNo).Value = "xxxx"
            '    Else
            '        If IsDBNull(grd.GetRow.Cells(GrdEnum.BatchNo).Value) Or grd.GetRow.Cells(GrdEnum.BatchNo).Value.ToString = "" Then
            '            grd.GetRow.Cells(GrdEnum.BatchNo).Value = dt.Rows(0).Item("BatchNo").ToString
            '        End If
            '        If dt.Rows.Count > 0 Then
            '            If Not IsDBNull(dt.Rows(0).Item("BatchNo").ToString) Then
            '                str = " Select   ExpiryDate  From  StockDetailTable  where BatchNo not in ('','0','xxxx') And BatchNo ='" & Me.grd.GetRow.Cells(GrdEnum.BatchNo).Value.ToString & "'" _
            '                     & " And ArticledefId = " & Me.grd.GetRow.Cells(GrdEnum.ItemId).Value & "  And (isnull(InQty, 0) - isnull(OutQty, 0)) > 0 Group by BatchNo,ExpiryDate ORDER BY ExpiryDate  Asc "
            '                Dim dtExpiry As DataTable = GetDataTable(str)
            '                If dtExpiry.Rows.Count > 0 Then
            '                    If IsDBNull(dtExpiry.Rows(0).Item("ExpiryDate")) = False Then
            '                        grd.GetRow.Cells(GrdEnum.ExpiryDate).Value = CType(dtExpiry.Rows(0).Item("ExpiryDate").ToString, Date)
            '                    End If
            '                End If
            '            End If
            '        End If
            '    End If
            'End If
            If Me.grd.RowCount > 0 AndAlso Me.grd.GetRow.Cells(GrdEnum.ItemId).Value IsNot Nothing Then
                Dim str As String = ""
                str = " Select  BatchNo,BatchNo,ExpiryDate  From  StockDetailTable  where BatchNo not in ('','0','xxxx')  And ArticledefId = " & Me.grd.GetRow.Cells(GrdEnum.ItemId).Value & "  Group by BatchNo,ExpiryDate Having Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0)) > 0  ORDER BY ExpiryDate Asc"
                Dim dt As DataTable = GetDataTable(str)
                Me.grd.RootTable.Columns(GrdEnum.BatchNo).ValueList.PopulateValueList(dt.DefaultView, "BatchNo", "BatchNo")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class
