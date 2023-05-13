''14-Dec-2013 R916  Imran Ali               Add comments column
''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction forms
''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''28-Dec-2013 RM6       Imran Ali           Release 2.1.0.0 Bug
''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
''11-Jan-2014      Task:2372  Imran Ali           Bug Generate In Release 2.1.0.2  
''11-Jan-2014 Task:2373         Imran Ali                Add Columns SubSub Title in Account List on Sales/Purchase
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
''03-Feb-2014        Task:2406   Imran Ali    FIELD CHOOSER restriction (Senior Rozgar)
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
''31-Mar-2014 Task:2528 Imran Ali  Quotation Security right problem 
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''12-May-2014 Task:20150502 Ali Ansari Regarding Adding Approved Checkbox in Quotation Master
''04-Jun-2015 Task:2015060001 Ali Ansari Regarding Attachements 
'08-Jun-2015  Task#2015060005 to allow all files to attach
''10-June-2015 Task# 2015060007 to get latest terms and conditions
''10-June-2015 Task# 2015060008 to remove non pictures from report with attachements
'12_jun-2015 Task#A1 12-06-2015 Ahmad Sharif: Add Check on grdSaved to check on double click if row less than zero than exit
'12-jun-2015 Task#A2 12-06-2015 Ahmad Sharif: Numeric validation on some textboxes
'12-jun-2015 Task#A3 12-06-2015 Ahmad Sharif: Check Vendor exist in combox list or not
''176-2015 TASKM176152 Imran Ali Specification Document Print
'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'07-Aug-2015 Task#07082015 Ahmad Sharif add new fields in design, save,update and show all those fields
'11-Aug-2015 Task#11082015 Delete item from grd  (Ahmad Sharif)
'16-Sep-2015 Task#16092015 Ahmad Sharif: Load Companies and Locations user wise
''TASK-480-Muhammad Ameen-12-07-2016: Save history of SO and Quot and update new field revision number in master on each update
'' TASK TFS1592 Ayesha Rehman on 19-10-2017 Future date entry should be rights based
'' TASK TFS1798 Ayesha Rehman on 29-12-2017 Salesman reference on quotation
'' TASK TFS2375 Ayesha Rehman on 19-02-2018 Checked by Options on all document sreens of Purchase
''TFS2827 Discount Factor implementation on Quotation on 28-03-2018
''TFS3113 : Ayesha Rehman : 24-04-2018 Approval Hierarchy on SalesModule
''TASK TFS3758 Ayesha Rehman : 05-07-2018. Reverse Calculation of Tax on Quotation Screen
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
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop

Public Class frmQoutationNew
    ' Change on 23-11-2013  For Multiple Print Vouchers
    Dim dt As DataTable
    Dim Mode As String = "Normal"
    Dim IsEditMode As Boolean = False
    Dim IsFormOpen As Boolean = False
    Dim Email As Email
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
    Dim flgLocationWiseItem As Boolean = False
    Dim SerialNoIncludingcharacter As Boolean = False
    'Marked Against Task#2015060001 Ali Ansari
    'Dim arrfile As String
    'Marked Against Task#2015060001 Ali Ansari
    'Altered Against Task#2015060001 Ali Ansari
    ' Convert string into List of string for making proper count
    Dim arrFile As List(Of String)
    'Altered Against Task#2015060001 Ali Ansari
    Dim BaseCurrencyId As Integer
    Dim BaseCurrencyName As String = String.Empty
    Dim IsCurrencyEdit As Boolean = False
    Dim IsNotCurrencyRateToAll As Boolean = False
    Dim IsRevisionRestrictedFirstTime As Boolean = False
    Dim EmailDAL As New EmailTemplateDAL
    Dim html As StringBuilder
    Dim VendorEmails As String = String.Empty
    Dim dtEmail As DataTable
    Dim AllFields As List(Of String)
    Dim EmailTemplate As String = String.Empty
    Dim AfterFieldsElement As String = String.Empty
    Dim MasterDAL As New PurchaseInquiryDAL
    Dim NotificationDAL As New NotificationTemplatesDAL
    Dim TermsAndConditionsId As Integer = 0I
    'Code Edit for task 1592 future date rights
    Dim IsDateChangeAllowed As Boolean = False
    Public Const DiscountType_Percentage As String = "Percentage" ''TFS2827
    Public Const DiscountType_Flat As String = "Flat" ''TFS2827
    Dim ApprovalProcessId As Integer = 0 ''TFS3113
    Dim flgExcludeTaxPrice As Boolean = False ''TFS3758
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Dim ItemFilterByName As Boolean = False
    Enum GrdEnum
        SerialNo
        LocationId
        ArticleCode
        Item
        RequirementDescription
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
        SalesTax_Percentage
        SalesTax_Amount
        CurrencySalesTaxAmount
        SED_Tax_Percent
        SED_Tax_Amount
        CurrencySEDAmount
        Net_Amount
        SchemeQty
        Discount_Percentage
        PurchasePrice
        Pack_Desc
        ''R916 Added Index Comments
        Comments
        '' End R916
        'BillValueAfterDiscount
        'New Columns added in grd by ahmad
        ItemDescription
        Brand
        Specification
        ItemRegistrationNo
        TradePrice
        TenderSrNo
        CostPrice
        QuotationId
        QuotationDetailId
        SOQuantity
        TotalQty
        PurchaseInquiryDetailId
        VendorQuotationDetailId
        HeadArticleId
        PurchaseInquiryId
        Alternate
        IBrand
        PartNo
        Warranty
        LeadTime
        State
        'Item_Info
    End Enum

    Private Sub frmQoutationNew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8 -1-14
            If e.KeyCode = Keys.F4 Then
                If BtnSave.Enabled = True Then
                    SaveToolStripButton_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
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
    Private Sub frmQoutationNew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim lblLoading As New Label
        lblLoading.Text = "Loading please wait..."
        lblLoading.Visible = True
        lblLoading.Location = New Point(600, 200)
        Me.UltraTabPageControl2.Controls.Add(lblLoading)
        lblLoading.BringToFront()
        Threading.Thread.Sleep(50)
        Try


            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            If Not getConfigValueByType("SerialNoIncludingcharacter").ToString = "Error" Then
                SerialNoIncludingcharacter = getConfigValueByType("SerialNoIncludingcharacter")
            End If
            If Not getConfigValueByType("TransitInssuranceTax").ToString = "Error" Then
                TransitPercent = Val(getConfigValueByType("TransitInssuranceTax"))
            End If
            If Not getConfigValueByType("WHTax_Percentage").ToString = "Error" Then
                WHTaxPercent = Val(getConfigValueByType("WHTax_Percentage"))
            End If

            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItem = getConfigValueByType("ArticleFilterByLocation")
            End If
            ''Start TFS3758 : Ayesha Rehman
            If Not getConfigValueByType("ExcludeTaxPrice").ToString = "Error" Then
                flgExcludeTaxPrice = Convert.ToBoolean(getConfigValueByType("ExcludeTaxPrice").ToString)
            End If
            ''End TFS3758
            ''start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161
            ' ''TASK TFS4544
            'If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            '    ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            'End If
            ' ''END TFS4544
            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)
            FillCombo("Vendor")
            FillCombo("Category")
            'FillCombo("Item")
            FillCombo("Company")
            FillCombo("SM")
            FillCombo("Project")
            FillCombo("TermsCondition")
            'Task#07082015 Fill combo box with existed item descriptions (Ahmad Sharif)
            FillCombo("ItemDescriptions")
            'FillCombo("RevisionNumber")

            'End Task#07082015
            FillCombo("Discount Type") 'Task2827
            'FillCombo("ArticlePack") R933 Commented
            RefreshControls()
            'Me.cmbVendor.Focus()
            'Me.DisplayRecord() R933 Commented History Data
            IsFormOpen = True
            'GetSalesOrderAnalysis()

            Get_All(frmModProperty.Tags)

            'TFS3360
            UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbVendor, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

            lblLoading.Visible = False

            'Code Written By Rizwan Asif
            MultiQuotationPrint() '


            'End of Code Written by Rasif4
            FillCombo("Currency")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            If frmModProperty.Tags.Length > 0 Then frmModProperty.Tags = String.Empty ''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
        End Try
    End Sub
    Public Sub MultiQuotationPrint()
        Try
            If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\Reports\Custom Prints\Quotation") Then
                IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\Reports\Custom Prints\Quotation")
            End If
            Dim strfileName() As String = IO.Directory.GetFiles(str_ApplicationStartUpPath & "\Reports\Custom Prints\Quotation\", "*.rpt", IO.SearchOption.AllDirectories)
            If strfileName.Length > 0 Then
                For Each FileName As String In strfileName
                    Dim toolStrip As New ToolStripMenuItem()
                    toolStrip.Name = IO.Path.GetFileName(FileName).Split(".")(0)
                    toolStrip.Text = IO.Path.GetFileName(FileName).Split(".")(0)
                    Me.CustomPrintsToolStripMenuItem.DropDownItems.Add(toolStrip)
                    Me.CustomPrintToolStripMenuItem.DropDownItems.Add(toolStrip)
                    AddHandler toolStrip.Click, AddressOf CustomPrintsToolStripMenuItem_Click
                Next
            Else
                Me.mnuLine.Visible = False
                Me.CustomPrintsToolStripMenuItem.Visible = False
                Me.CustomPrintToolStripMenuItem.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
        Dim str As String = String.Empty
        'str = "SELECT     Recv.QuotationNo, CONVERT(varchar, Recv.QuotationDate, 103) AS Date, V.CustomerName, Recv.SalesOrderQty, Recv.SalesOrderAmount, " _
        '       & " Recv.QuotationId, Recv.CustomerCode, Recv.Remarks, convert(varchar, Recv.CashPaid) as CashPaid FROM         dbo.QuotationMasterTable Recv INNER JOIN dbo.tblCustomer V ON Recv.CustomerCode = V.AccountId"
        'Marked by Ali Ansari for task#2015052
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.QuotationNo, Recv.QuotationDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty,  " & _
        '            "Recv.SalesOrderAmount, Recv.QuotationId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, PoNo as [Po No], Recv.NewCustomer, '' as Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull([No Of Attachment],0) as [No Of Attachment] " & _
        '            "FROM dbo.QuotationMasterTable Recv LEFT OUTER JOIN " & _
        '            "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.QuotationNo LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.QuotationId WHERE Recv.QuotationNo IS NOT NULL AND Recv.LocationId=" & Me.cmbCompany.SelectedValue & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.QuotationId in (Select QuotationId From QuotationDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '            " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.QuotationDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        ''Marked by Ali Ansari for task#2015052
        ''Marked by Ali Ansari for task#2015060006
        'Altered by Ali Ansari for task#2015052

        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.QuotationNo, Recv.QuotationDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty,  " & _
        '                    "Recv.SalesOrderAmount, Recv.QuotationId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, PoNo as [Po No], Recv.NewCustomer, '' as Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull([No Of Attachment],0) as [No Of Attachment], ISNULL(Recv.Apprved,0) as Approved " & _
        '                    "FROM dbo.QuotationMasterTable Recv LEFT OUTER JOIN " & _
        '                    "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.QuotationNo LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.QuotationId WHERE Recv.QuotationNo IS NOT NULL AND Recv.LocationId=" & Me.cmbCompany.SelectedValue & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.QuotationId in (Select QuotationId From QuotationDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '                    " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.QuotationDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        'Marked By Ali ansari for task#2015060007 for terms and condition trouble shoot
        'Altered by Ali Ansari for task#2015052
        ''Marked by Ali Ansari for task#2015060006
        'Altered by Ali Ansari for task#2015060006 for saving attachements
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.QuotationNo, Recv.QuotationDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty,  " & _
        '                  "Recv.SalesOrderAmount, Recv.QuotationId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, PoNo as [Po No], Recv.NewCustomer, '' as Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull([No Of Attachment],0) as [No Of Attachment], ISNULL(Recv.Apprved,0) as Approved " & _
        '                  "FROM dbo.QuotationMasterTable Recv LEFT OUTER JOIN " & _
        '                  "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.QuotationNo LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.QuotationId WHERE Recv.QuotationNo IS NOT NULL AND Recv.LocationId=" & Me.cmbCompany.SelectedValue & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.QuotationId in (Select QuotationId From QuotationDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '                  " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.QuotationDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        'Altered by Ali Ansari for task#2015060006 for saving attachements
        'Marked By Ali ansari for task#2015060007 for terms and condition trouble shoot
        'Altered By Ali ansari for task#2015060007 for terms and condition trouble shoot
        'Altered by Ali Ansari for task#2015060006 for saving attachements
        'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.QuotationNo, Recv.QuotationDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty,  " & _
        '                  "Recv.SalesOrderAmount, Recv.QuotationId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, PoNo as [Po No], Recv.NewCustomer, '' as Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull([No Of Attachment],0) as [No Of Attachment], ISNULL(Recv.Apprved,0)as Approved,recv.Terms_And_Condition " & _
        '                  "FROM dbo.QuotationMasterTable Recv LEFT OUTER JOIN " & _
        '                  "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.QuotationNo LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.QuotationId WHERE Recv.QuotationNo IS NOT NULL AND Recv.LocationId=" & Me.cmbCompany.SelectedValue & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.QuotationId in (Select QuotationId From QuotationDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '                  " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.QuotationDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
        'Altered by Ali Ansari for task#2015060006 for saving attachements
        'Altered By Ali ansari for task#2015060007 for terms and condition trouble shoot

        'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms

        'Task#07082015 add columns in query (Ahmad Sharif)
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.QuotationNo, Recv.QuotationDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty,  " & _
        '                          "Recv.SalesOrderAmount, Recv.QuotationId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, PoNo as [Po No], Recv.NewCustomer, '' as Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull([No Of Attachment],0) as [No Of Attachment], ISNULL(Recv.Apprved,0)as Approved,recv.Terms_And_Condition,recv.username as 'User Name', Recv.VerifiedBy as [Verified By] " & _
        '                          ",Recv.CustomerPhone as [Customer Mobile],Recv.CustomerAddress as [Customer Address],Recv.Approved_User as [Approved User]  " & _
        '                          "FROM dbo.QuotationMasterTable Recv LEFT OUTER JOIN " & _
        '                          "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.QuotationNo LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.QuotationId WHERE Recv.QuotationNo IS NOT NULL AND Recv.LocationId=" & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbCompany.SelectedValue) & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.QuotationId in (Select QuotationId From QuotationDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '                          " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.QuotationDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.QuotationNo, Recv.QuotationDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Pack.Packs, Recv.SalesOrderQty,  " & _
        '                          "Recv.SalesOrderAmount, Recv.QuotationId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, PoNo as [Po No], Recv.NewCustomer, '' as Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull([No Of Attachment],0) as [No Of Attachment], ISNULL(Recv.Apprved,0)as Approved,recv.Terms_And_Condition,recv.username as 'User Name', Recv.VerifiedBy as [Verified By] " & _
        '                          ",Recv.CustomerPhone as [Customer Mobile],Recv.CustomerAddress as [Customer Address],Recv.Approved_User as [Approved User]  " & _
        '                        "FROM dbo.QuotationMasterTable Recv LEFT OUTER JOIN (SELECT QuotationId, Sum(IsNull(Sz1, 0)) As Packs From QuotationDetailTable Group By QuotationId) As Pack ON Recv.QuotationId = Pack.QuotationId LEFT OUTER JOIN " & _
        '                          "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.QuotationNo LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.QuotationId WHERE Recv.QuotationNo IS NOT NULL AND Recv.LocationId=" & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbCompany.SelectedValue) & " " & IIf(Me.cmbSearchLocation.SelectedIndex = -1, "", " AND Recv.QuotationId in (Select QuotationId From QuotationDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
        '                          " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.QuotationDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""

        'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
        'TFS# 956: Insert Terms and Conditions ids in History Grid by Ali Faisal on 19-June-2017
        ''TFS1798 Added column EmpoyeeId
        ''TFS1674 Added column ManualSerialNo
        str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.QuotationNo, Recv.QuotationDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Pack.Packs, Recv.SalesOrderQty,  " & _
                                  "Recv.SalesOrderAmount, Recv.QuotationId, Recv.VendorId, Recv.ManualSerialNo, Recv.Subject, Recv.EmployeeId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, PoNo as [Po No], Recv.NewCustomer, tblDefEmployee.Employee_Name as Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull([No Of Attachment],0) as [No Of Attachment], ISNULL(Recv.Apprved,0)as Approved,recv.Terms_And_Condition,recv.username as 'User Name', Recv.VerifiedBy as [Verified By] " & _
                                  ",Recv.CustomerPhone as [Inco Term],Recv.CustomerAddress as [Payment Term],Recv.Approved_User as [Approved User],IsNull(Recv.TermsAndConditionsId,0) As TermsAndConditionsId, SalesInquiryMaster.SalesInquiryNo , SalesInquiryMaster.CustomerInquiryNo " & _
                                "FROM dbo.QuotationMasterTable Recv LEFT OUTER JOIN (SELECT QuotationId, Sum(IsNull(Sz1, 0)) As Packs From QuotationDetailTable Group By QuotationId) As Pack ON Recv.QuotationId = Pack.QuotationId LEFT OUTER JOIN " & _
                                  "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
                                  "tblDefEmployee ON Recv.EmployeeId = tblDefEmployee.Employee_Id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.QuotationNo LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.QuotationId " & _
                                    " LEFT OUTER JOIN SalesInquiryMaster ON Recv.SalesInquiryId = SalesInquiryMaster.SalesInquiryId " & _
                                    " WHERE Recv.QuotationNo IS NOT NULL AND Recv.LocationId=" & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbCompany.SelectedValue) & " " & IIf(Me.cmbSearchLocation.SelectedValue = 0, "", " AND Recv.QuotationId in (Select QuotationId From QuotationDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")") & " " & _
                                  " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.QuotationDate,102) > Convert(Datetime, N'" & ClosingDate & "',102))") & ""
        'TFS# 956: End
        If Me.dtpFrom.Checked = True Then
            str += " AND Recv.QuotationDate >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
        End If
        If Me.dtpTo.Checked = True Then
            str += " AND Recv.QuotationDate <= Convert(Datetime, N'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
        End If
        If Me.txtSearchDocNo.Text <> String.Empty Then
            str += " AND Recv.QuotationNo LIKE '%" & Me.txtSearchDocNo.Text & "%'"
        End If
        If Me.cmbSearchLocation.SelectedValue > 0 Then
            str += " AND Recv.LocationId=" & Me.cmbSearchLocation.SelectedValue
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
            str += " AND Recv.QuotationNo LIKE  '%" & Me.txtPurchaseNo.Text & "%'"
        End If
        If Me.txtCustomerInquiryNo.Text <> String.Empty Then
            str += " AND SalesInquiryMaster.CustomerInquiryNo LIKE  '%" & Me.txtCustomerInquiryNo.Text & "%'"
        End If
        ''Start OTC Requirement : Ayesha Rehman
        If Me.txtInquiryNo.Text <> String.Empty Then
            str += " AND SalesInquiryMaster.SalesInquiryNo LIKE  '%" & Me.txtInquiryNo.Text & "%'"
        End If
        ''End 
        str += " ORDER BY Recv.QuotationNo DESC"
        ' End If
        FillGridEx(grdSaved, str, True)
        ' Change on 23-11-2013  For Multiple Print Vouchers
        Me.grdSaved.RootTable.Columns.Add("Column1")
        Me.grdSaved.RootTable.Columns("Column1").UseHeaderSelector = True
        Me.grdSaved.RootTable.Columns("Column1").ActAsSelector = True
        '-----------------------------------------------------'
        'grdSaved.DataSource = GetDataTable(str)
        'grdSaved.Columns(10).Visible = False
        grdSaved.RootTable.Columns(6).Visible = False
        grdSaved.RootTable.Columns(7).Visible = False
        grdSaved.RootTable.Columns(8).Visible = False
        grdSaved.RootTable.Columns(9).Visible = False
        grdSaved.RootTable.Columns(10).Visible = False

        grdSaved.RootTable.Columns("Terms_And_Condition").Visible = False
        grdSaved.RootTable.Columns("EmployeeId").Visible = True ''TFS1798
        grdSaved.RootTable.Columns("ManualSerialNo").Visible = True ''TFS1674
        grdSaved.RootTable.Columns("NewCustomer").Visible = True
        grdSaved.RootTable.Columns("CostCenterId").Visible = False
        grdSaved.RootTable.Columns("Adj_Flag").Visible = False
        grdSaved.RootTable.Columns("Adjustment").Visible = False
        grdSaved.RootTable.Columns("PO_Date").Visible = False
        grdSaved.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
        'Task:2759 Set rounded amount format
        Me.grdSaved.RootTable.Columns("SalesOrderAmount").FormatString = "N" & DecimalPointInValue
        Me.grdSaved.RootTable.Columns("Adjustment").FormatString = "N" & DecimalPointInValue
        'End Task:2759

        grdSaved.RootTable.Columns("EditionalTax_Percentage").Visible = False
        grdSaved.RootTable.Columns("SED_Percentage").Visible = False


        'Task#07082015 hides the column in grdSaved (Ahmad Sharif)
        'Me.grdSaved.RootTable.Columns("Customer Mobile").Visible = False
        'Me.grdSaved.RootTable.Columns("Customer Address").Visible = False
        'Me.grdSaved.RootTable.Columns("Item Description").Visible = False
        'Me.grdSaved.RootTable.Columns("Brand").Visible = False
        'Me.grdSaved.RootTable.Columns("Item Specs").Visible = False
        'Me.grdSaved.RootTable.Columns("Item Reg. No").Visible = False
        'Me.grdSaved.RootTable.Columns("Trade Price").Visible = False
        'Me.grdSaved.RootTable.Columns("Item Information").Visible = False
        'End Task#07082015


        'grdSaved.Columns("EmployeeCode").Visible = False
        'grdSaved.Columns("PoId").Visible = False
        grdSaved.RootTable.Columns(0).Caption = "Doc No"
        grdSaved.RootTable.Columns(1).Caption = "Date"
        grdSaved.RootTable.Columns(2).Caption = "Customer Name"
        'grdSaved.Columns(3).HeaderText = "S-Order"
        grdSaved.RootTable.Columns(4).Caption = "Qty"
        grdSaved.RootTable.Columns(5).Caption = "Amount"
        'grdSaved.Columns(8).HeaderText = "Employee"

        grdSaved.RootTable.Columns(0).Width = 100
        grdSaved.RootTable.Columns(1).Width = 100
        grdSaved.RootTable.Columns(2).Width = 250
        grdSaved.RootTable.Columns(3).Width = 50
        grdSaved.RootTable.Columns(5).Width = 80
        grdSaved.RootTable.Columns(8).Width = 100
        grdSaved.RootTable.Columns(4).Width = 150
        'grdSaved.RowHeadersVisible = False
        Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat
        'TFS# 956: Hide Terms and Conditions Ids in History Grid by Ali Faisal on 19-June-2017
        Me.grdSaved.RootTable.Columns("TermsAndConditionsId").Visible = False
        'TFS# 956: End
        Me.grdSaved.RootTable.Columns("EmployeeId").Visible = False
        Me.grdSaved.RootTable.Columns("LocationId").Visible = False
    End Sub



    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Validate_AddToGrid() Then
            AddItemToGrid()
            GetTotal()
            ClearDetailControls()
            cmbItem.Focus()
            'FillCombo("Item")
        End If

    End Sub
    Private Sub RefreshControls()


        'Task 1592 Ayesha Rehman Removing the ErrorProvider on btnNew
        ''TASK TFS4544
        If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
        End If
        ''END TFS4544

        If Not getConfigValueByType("SerialNoIncludingcharacter").ToString = "Error" Then
            SerialNoIncludingcharacter = getConfigValueByType("SerialNoIncludingcharacter")
        End If

        FillCombo("Item")
        ErrorProvider1.Clear()
        IsEditMode = False
        Me.cmbVendor.Enabled = True
        Me.txtRemarks.Enabled = True
        txtPONo.Text = ""
        dtpPODate.Value = Now
        Me.dtpPDate.Value = Now
        If LoginGroup = "Administrator" Then
            dtpPODate.Enabled = True
        End If
        txtManualSerialNo.Text = "" ''TFS1674
        txtRemarks.Text = ""
        txtSubject.Text = ""
        txtPaid.Text = ""
        'txtAmount.Text = ""
        txtTotal.Text = ""
        txtDiscountValue.Text = ""   '28-03-2018 : Task# TFS2827: Ayesha Rehman: Set these controls text empty on load,new
        Me.cmbDiscountType.SelectedIndex = 1 '28-03-2018 : Task# TFS2827: Ayesha Rehman: Set these controls text empty on load,new
        Me.txtPDP.Text = "" ''TFS2827
        Me.txtNetTotal.Text = ""    ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        cmbVendor.Rows(0).Activate()
        cmbItem.Rows(0).Activate()
        cmbUnit.SelectedIndex = 0
        cmbSalesMan.SelectedIndex = 0 ''TFS1798
        'txtTotalQty.Text = ""
        txtBalance.Text = ""
        txtPackQty.Text = 1
        Me.BtnSave.Text = "&Save"
        ' Mode = "Normal"
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("SO", 6, "QuotationMasterTable", "QuotationNo")
        Me.cmbPo.Enabled = True
        'FillCombo("SO")
        Me.DisplayDetail(-1)
        'Marked Against Task#2015060001
        'Array.Clear(arrFile, 0, arrFile.Length)
        'Marked Against Task#2015060001 Ali Ansari
        'Altered Against Task#2015060001 Ali Ansari
        'Clear arrfile
        'Clear Attached file records
        arrFile = New List(Of String)
        Me.btnAttachment.Text = "Attachment (" & arrFile.Count & ")"
        'Altered Against Task#2015060001 Ali Ansri
        'Array.Clear(arrFile, 0, arrFile.Length)

        'Altered Against Task#2015060001 Ali Ansari
        'DisplayDetail(-1)
        'Me.cmbVendor.Focus()
        GetSecurityRights()
        Me.LnkLoadAll.Enabled = True
        'If Not Me.cmbSalesMan.SelectedIndex = -1 Then Me.cmbSalesMan.SelectedIndex = 0
        Me.txtNewCustomer.Text = String.Empty
        'FillComboByEdit()
        ''Start TFS3113
        'Ayesha Rehman : TFS2375 : Enable Approval History button only in Eidt Mode
        If IsEditMode = True Then
            Me.btnApprovalHistory.Visible = True
            Me.btnApprovalHistory.Enabled = True
        Else
            Me.btnApprovalHistory.Visible = False
        End If
        'Ayesha Rehman : TFS2375 : End
        ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
        If Not getConfigValueByType("SalesQuotationApproval") = "Error" Then
            ApprovalProcessId = Val(getConfigValueByType("SalesQuotationApproval"))
        End If
        If ApprovalProcessId = 0 Then
            Me.chkPost.Visible = True
            Me.chkPost.Enabled = True
            Me.ChkApproved.Visible = True
            Me.ChkApproved.Enabled = True

        Else
            Me.chkPost.Visible = False
            Me.chkPost.Enabled = False
            Me.chkPost.Checked = False
            Me.ChkApproved.Visible = False
            Me.ChkApproved.Enabled = False
            Me.ChkApproved.Checked = False
        End If
        ''Ayesha Rehman :TFS2375 :End
        ''End TFS3113

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
        Me.chkPost.Checked = False
        Me.txtSchemeQty.Text = String.Empty
        'DisplayRecord() R933 Commented History Data
        Me.lblPrintStatus.Text = String.Empty
        Me.txtNetBill.Text = String.Empty
        Me.txtCustPONo.Text = String.Empty
        Adjustment = 0D
        If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedIndex = 0
        Me.txtTerms_And_Condition.Text = GetTermsCondition()


        'Task#07082015 Reset Controls (Ahmad Sharif)
        Me.txtCustomerMobile.SelectedIndex = 0
        Me.txtCustomerAddress.SelectedIndex = 0
        Me.chkAddItemDescription.Checked = False
        Me.txtBrand.Text = String.Empty
        Me.txtSpecs.Text = String.Empty
        Me.txtItemRegNo.Text = String.Empty
        Me.cmbItemDescription.Text = String.Empty
        Me.txtTradePrice.Text = String.Empty
        'End Task#07082015

        ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
        Me.BtnDelete.Visible = False
        Me.BtnPrint.Visible = False
        Me.BtnEdit.Visible = False
        ''''''''''''''''''''''''''
        'Altered by Ali Ansari for task#2015052
        Me.ChkApproved.Checked = False
        'Altered by Ali Ansari for task#2015052
        'Altered Against Task#2015060001 Ali Ansri
        'Clear Attached file records
        'arrFile = New List(Of String)
        'Me.btnAttachment.Text = "Attachment (" & arrFile.Count & ")"
        'Altered Against Task#2015060001 Ali Ansri
        ''TASK-481 on 13-07-2016
        Me.cmbRevisionNumber.Visible = False
        Me.lblRev.Visible = False
        Me.lnkLblRevisions.Visible = False
        Me.txtPONo.Size = New System.Drawing.Size(174, 21)
        Me.cmbCurrency.SelectedValue = BaseCurrencyId
        Me.cmbCurrency.Enabled = True
        ''End TASK-481
        'TFS# 956: Reset value in Text box by Ali Faisal on 16-June-2017
        Me.txtArticleAlias.Text = ""
        txtPartNo.Text = ""
        txtIBrand.Text = ""
        Me.txtWarranty.Text = 0
        Me.txtLeadTime.Text = 0
        Me.cmbState.SelectedIndex = 0
        Me.txtArticleAlias.Enabled = True
        'Aashir : TFS3760 Set text to save as template
        SaveAsTemplateToolStripMenuItem.Text = "Save as &template"
        SaveAsTemplateToolStripMenuItem.Tag = String.Empty
        'TFS# 956: End
        If Not Me.cmbTermCondition.SelectedIndex = -1 Then Me.cmbTermCondition.SelectedIndex = 0
        Me.dtpPODate.Focus()
        'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
        txtItemPackRate.Text = ""

    End Sub
    Function GetTermsCondition() As String
        Try
            'Marked Against Task#2015060007
            'Dim str As String = "Select Terms_And_Condition From QuotationMasterTable WHERE QuotationId in (Select Max(QuotationId) From QuotationMasterTable)"
            'Marked Against Task#2015060007
            'Altered Against Task#2015060007 get max terms and conditions
            Dim str As String = "Select Terms_And_Condition From QuotationMasterTable WHERE QuotationId = (Select Max(QuotationId) From QuotationMasterTable)"
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
        txtQty.Text = ""
        txtRate.Text = ""
        txtTotal.Text = ""
        txtTotalQuantity.Text = ""
        txtIBrand.Text = ""
        txtPartNo.Text = ""
        txtWarranty.Text = 0
        txtLeadTime.Text = 0
        cmbState.SelectedIndex = 0
        txtPackQty.Text = 1
        txtSchemeQty.Text = String.Empty
        Me.txtDisc.Text = "" ''28-03-2018   TFS2827   Ayesha Rehamn  
        Me.txtDiscountValue.Text = ""        ''28-03-2018   TFS2827   Ayesha Rehamn  
        Me.txtTax.Text = ""
        Me.txtPackRate.Text = String.Empty
        Me.txtNetTotal.Text = ""    ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        txtItemPackRate.Text = "" 'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
    End Sub

    Private Function Validate_AddToGrid() As Boolean
        'If Me.cmbVendor.IsItemInList = True Then
        '    If Me.cmbVendor.Rows(0).Activate Then
        '        msg_Error("You must select any customer")
        '        Me.cmbVendor.Focus() : Validate_AddToGrid = False : Exit Function
        '    End If
        'End If
        'If cmbItem.ActiveRow.Cells(0).Value <= 0 Then
        '    msg_Error("You must select any item")
        '    cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        'End If
        'TFS# 956: Add condition for Article Alias on Item add in grid by Ali Faisal on 16-June-2017
        If cmbItem.ActiveRow.Cells(0).Value <= 0 AndAlso Me.txtArticleAlias.Text = "" Then
            msg_Error("You must select any item")
            cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'TFS# 956: End
        If Val(txtQty.Text) < 0 Then
            msg_Error("Quantity must be greater than 0")
            txtQty.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtRate.Text) < 0 Then
            msg_Error("Rate must be greater than 0")
            txtRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        If Val(txtTotalQuantity.Text) < 0 Then
            msg_Error("Total Quantity is required more than 0")
            txtTotalQuantity.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        Validate_AddToGrid = True
    End Function

    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus, txtWarranty.LostFocus, txtLeadTime.LostFocus, txtPartNo.LostFocus, txtIBrand.LostFocus

        'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtQty.Text) <> 0 AndAlso Val(Me.txtRate.Text) = 0 Then
        '    Me.txtRate.Text = Val(Me.txtTotal.Text) / Val(Me.txtQty.Text)
        'Else
        '    If Val(Me.txtPackQty.Text) = 0 Then
        '        txtPackQty.Text = 1
        '        txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
        '    Else
        '        txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
        '    End If
        'End If


        'If Val(Me.txtPackQty.Text) = 0 Then
        '    txtPackQty.Text = 1
        '    txtNetTotal.Text = Val(txtQty.Text) * Val(txtRate.Text) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
        'Else
        '    txtNetTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
        'End If
        Try

            Me.GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRate.LostFocus
        Try
            ' Before ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            'Else
            '    txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            'End If

            '' After ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            'If Val(Me.txtPackQty.Text) = 0 Or Val(Me.txtPackQty.Text) = 1 Then
            '    Me.txtTotal.Text = Val(Me.txtQty.Text) * Val(Me.txtRate.Text)
            'Else
            '    Me.txtTotal.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text) * Val(Me.txtRate.Text)
            'End If


            'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtQty.Text) = 0 Then
            '    Me.txtQty.Text = Math.Round((Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)), 2)
            'Else
            '    If Val(Me.txtPackQty.Text) = 0 Then
            '        txtPackQty.Text = 1
            '        txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
            '    Else
            '        txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
            '    End If
            'End If

            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    txtNetTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            'Else
            '    txtNetTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            'End If
            GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate.TextChanged
        Try
            Me.txtTotal.Text = Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged, cmbState.SelectedIndexChanged
        If Me.cmbUnit.SelectedIndex < 0 Then Me.txtPackQty.Text = String.Empty : Exit Sub
        If Me.cmbUnit.Text = "Loose" Then
            txtPackQty.Text = 1
            Me.txtPackQty.Enabled = False
            Me.txtPackQty.TabStop = False
            Me.txtTotalQuantity.Enabled = False
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))
            'Me.txtPDP.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackRate").ToString) ''Commented Against TFS4161
            Me.txtPDP.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString()
        Else
            ''Start TFS4161 : Ayesha Rehman : 09-08-2018 : Disable Pack Quantity if configuration (Disable Pack Qty) is on 
            If IsPackQtyDisabled = True Then
                Me.txtPackQty.Enabled = False
                Me.txtPackQty.TabStop = False
                Me.txtTotalQuantity.Enabled = False
            Else
                Me.txtPackQty.Enabled = True
                Me.txtPackQty.TabStop = True
                Me.txtTotalQuantity.Enabled = True
            End If
            ''End TFS4161
            If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
                Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                Me.txtItemPackRate.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackRate").ToString)
                Me.txtPDP.Text = (Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackRate").ToString)) / (Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString))
            End If
            GetDetailTotal()
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))
        End If
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
            Dim dtGrd As DataTable
            dtGrd = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtGrd.NewRow
            drGrd.Item(GrdEnum.SerialNo) = 0
            drGrd.Item(GrdEnum.LocationId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
            ''Start TFS4150 : Commented to get the actual name of pack in Unit
            'drGrd.Item(GrdEnum.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            drGrd.Item(GrdEnum.Unit) = Me.cmbUnit.Text.ToString
            ''End TFS4150
            drGrd.Item(GrdEnum.Qty) = Val(Me.txtQty.Text)
            drGrd.Item(GrdEnum.Rate) = Val(Me.txtRate.Text)
            drGrd.Item(GrdEnum.Total) = Val(Me.txtTotal.Text)
            drGrd.Item(GrdEnum.CategoryId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
            'TFS# 956: Add Article Alias in detail grid in requirment description column by Ali Faisal on 19-June-2017
            If Me.cmbItem.Value > 0 Then
                drGrd.Item(GrdEnum.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
                drGrd.Item(GrdEnum.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
                drGrd.Item(GrdEnum.ItemId) = Me.cmbItem.ActiveRow.Cells(0).Value
                drGrd.Item(GrdEnum.CurrentPrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Text)
                drGrd.Item(GrdEnum.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
            Else
                drGrd.Item(GrdEnum.ArticleCode) = String.Empty
                drGrd.Item(GrdEnum.Item) = String.Empty
                drGrd.Item(GrdEnum.ItemId) = 0
                drGrd.Item(GrdEnum.CurrentPrice) = Val(Me.txtRate.Text)
                drGrd.Item(GrdEnum.PurchasePrice) = 0
            End If

            If Not Me.txtArticleAlias.Text = "" Then
                drGrd.Item(GrdEnum.RequirementDescription) = Me.txtArticleAlias.Text
                Me.txtArticleAlias.Text = ""
            End If

            'TFS# 956: End
            drGrd.Item(GrdEnum.PackQty) = Val(Me.txtPackQty.Text)

            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            drGrd.Item(GrdEnum.PackPrice) = Val(Me.txtItemPackRate.Text)
            'drGrd.Item(GrdEnum.TradePrice) = TradePrice
            drGrd.Item(GrdEnum.SalesTax_Percentage) = Val(Me.txtTax.Text)
            drGrd.Item(GrdEnum.SchemeQty) = Val(Me.txtSchemeQty.Text)
            'drGrd.Item(GrdEnum.Freight) = Freight_Rate
            drGrd.Item(GrdEnum.Discount_Percentage) = Val(Me.txtDisc.Text)
            'drGrd.Item(GrdEnum.MarketReturns) = MarketReturns_Rate

            drGrd.Item(GrdEnum.Pack_Desc) = Me.cmbUnit.Text.ToString
            drGrd.Item(GrdEnum.Comments) = String.Empty

            'Task#15092015 add columns in enum and also add in grd by ahmad
            drGrd.Item(GrdEnum.ItemDescription) = Me.cmbItemDescription.Text
            drGrd.Item(GrdEnum.Brand) = Me.txtBrand.Text
            drGrd.Item(GrdEnum.Specification) = Me.txtSpecs.Text
            drGrd.Item(GrdEnum.ItemRegistrationNo) = Me.txtItemRegNo.Text
            drGrd.Item(GrdEnum.TradePrice) = Val(Me.txtTradePrice.Text)
            drGrd.Item(GrdEnum.TenderSrNo) = Me.txtTenderSrNo.Text
            drGrd.Item(GrdEnum.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
            drGrd.Item(GrdEnum.TotalQty) = Val(Me.txtTotalQuantity.Text)


            drGrd.Item(GrdEnum.CurrencyId) = Me.cmbCurrency.SelectedValue
            If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                drGrd.Item(GrdEnum.CurrencyAmount) = Val(0)
            Else
                drGrd.Item(GrdEnum.CurrencyAmount) = Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text)
            End If
            drGrd.Item(GrdEnum.CurrencyRate) = Val(Me.txtCurrencyRate.Text)
            Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
            If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                drGrd.Item(GrdEnum.BaseCurrencyId) = Val(ConfigCurrencyVal)
                drGrd.Item(GrdEnum.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
            End If
            drGrd.Item(GrdEnum.PurchaseInquiryDetailId) = 0
            drGrd.Item(GrdEnum.VendorQuotationDetailId) = 0
            drGrd.Item(GrdEnum.HeadArticleId) = 0
            drGrd.Item(GrdEnum.PurchaseInquiryId) = 0

            'End Task#15092015 
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
            drGrd.Item(GrdEnum.IBrand) = Me.txtIBrand.Text
            drGrd.Item(GrdEnum.PartNo) = Me.txtPartNo.Text
            drGrd.Item(GrdEnum.Warranty) = Me.txtWarranty.Text
            drGrd.Item(GrdEnum.LeadTime) = Me.txtLeadTime.Text
            drGrd.Item(GrdEnum.State) = Me.cmbState.Text
            'dtGrd.Rows.InsertAt(drGrd, 0)

            'Task 3913 Saad Afzaal change InsertAt to Add function to maintain sequence of items which add in the grid
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
        Try
            If IsFormOpen = True Then
                If flgLocationWiseItem = True Then FillCombo("Item")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub FillCombo(ByVal strCondition As String)
        Dim str As String


        If strCondition = "Item" Then
            'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ISNULL(SalePrice,0) as Price, Isnull(PurchasePrice,0) as PurchasePrice, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand] FROM ArticleDefView where Active=1 AND ArticleDefView.SalesItem=1"
            str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleDefView.ArticleBrandName As Grade,ISNULL(SalePrice,0) as Price, Isnull(PurchasePrice,0) as PurchasePrice, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand],IsNull(ArticleDefView.Cost_Price,0) as [Cost Price], IsNull(TradePrice,0) as [Trade Price] FROM ArticleDefView where Active=1 AND ArticleDefView.SalesItem=1"
            If flgCompanyRights = True Then
                str += " AND ArticleDefView.CompanyId=" & MyCompanyId
            End If
            If flgLocationWiseItem = True Then
                If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                    str += " AND  ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                Else
                    str += " ORDER BY ArticleDefView.SortOrder"
                End If
            End If

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
                    If rdoName.Checked = True Then
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                    Else
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
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

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

            FillDropDown(cmbCategory, str, False)

        ElseIf strCondition = "SearchLocation" Then
            str = "Select CompanyId, CompanyName from CompanyDefTable order by 1"
            FillDropDown(Me.cmbSearchLocation, str, True)

        ElseIf strCondition = "ItemFilter" Then
            str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,ArticleDefView.ArticleBrandName As Grade,ISNULL(SalePrice,0) as Price, Isnull(PurchasePrice,0) as PurchasePrice, ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand] FROM         ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue & " "
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
            'Task:1798 Added Column Sales Man
            Dim ShowVendorOnSales As Boolean = False
            Dim ShowMiscAccountsOnSales As Boolean = False
            If Not getConfigValueByType("Show Vendor On Sales") = "Error" Then
                ShowVendorOnSales = CBool(getConfigValueByType("Show Vendor On Sales"))
            End If
            If Not getConfigValueByType("ShowMiscAccountsOnSales") = "Error" Then
                ShowMiscAccountsOnSales = CBool(getConfigValueByType("ShowMiscAccountsOnSales"))
            End If
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                             "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, tblCustomer.SaleMan, tblCustomer.Email, tblCustomer.Phone, vwCOADetail.Sub_Sub_Title " & _
                             "FROM dbo.tblCustomer LEFT OUTER JOIN " & _
                             "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                             "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId LEFT OUTER JOIN " & _
                             "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                             "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                             "WHERE dbo.vwCOADetail.detail_title Is Not NULL " & IIf(ShowVendorOnSales = True, " AND (dbo.vwCOADetail.account_type in ('Customer','Vendor'))", " AND (dbo.vwCOADetail.account_type in ('Customer'))") & "" _
                             & "" & IIf(ShowMiscAccountsOnSales = True, " OR vwCOADetail.coa_detail_id IN (SELECT  DISTINCT tblCOAMainSubSubDetail.coa_detail_id " & _
                                   "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) ", "") & ""
            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            ' If LoginGroup = "Administrator" Then
            If GetMappedUserId() > 0 And getGroupAccountsConfigforSales(Me.Name) And LoginGroup <> "Administrator" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                            "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, tblCustomer.SaleMan, tblCustomer.Email, tblCustomer.Phone, vwCOADetail.Sub_Sub_Title " & _
                            "FROM dbo.tblCustomer LEFT OUTER JOIN " & _
                            "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                            "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId LEFT OUTER JOIN " & _
                            "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                            "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                            "WHERE (dbo.vwCOADetail.detail_title Is Not NULL) "
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
            'End Task:2373

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
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("SaleMan").Hidden = False
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" 'Task:2373 Change Caption
            End If
            'Task:1798 Added to fill SalesMan DropDown 
        ElseIf strCondition = "SM" Then
            str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE SalePerson <> 0 And Active = 1" ''TASKTFS75 added and set active =1
            FillDropDown(Me.cmbSalesMan, str)
        ElseIf strCondition = "SearchVendor" Then
            Dim ShowVendorOnSales As Boolean = False
            Dim ShowMiscAccountsOnSales As Boolean = False
            If Not getConfigValueByType("Show Vendor On Sales") = "Error" Then
                ShowVendorOnSales = CBool(getConfigValueByType("Show Vendor On Sales"))
            End If
            If Not getConfigValueByType("ShowMiscAccountsOnSales") = "Error" Then
                ShowMiscAccountsOnSales = CBool(getConfigValueByType("ShowMiscAccountsOnSales"))
            End If
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                           "dbo.tblListTerritory.TerritoryName as Territory, tblCustomer.Email, tblCustomer.Phone " & _
                                           "FROM         dbo.tblCustomer INNER JOIN " & _
                                           "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                           "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                           "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                           "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                        "WHERE dbo.vwCOADetail.detail_title Is Not NULL " & IIf(ShowVendorOnSales = True, " And (dbo.vwCOADetail.account_type in ('Customer','Vendor'))", " And (dbo.vwCOADetail.account_type in ('Customer'))") & "" _
                             & "" & IIf(ShowMiscAccountsOnSales = True, " OR vwCOADetail.coa_detail_id IN (SELECT  DISTINCT tblCOAMainSubSubDetail.coa_detail_id " & _
                                   "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) ", "") & ""

            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            'If LoginGroup = "Administrator" Then
            If GetMappedUserId() > 0 And getGroupAccountsConfigforSales(Me.Name) And LoginGroup <> "Administrator" Then
                str += " OR coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= 14) " _
                       & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]=14) " _
                       & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]=14) " _
                       & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]=14) "
            End If
            ''End TFS3322

            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title"
            FillUltraDropDown(cmbSearchAccount, str)
            If Me.cmbSearchAccount.DisplayLayout.Bands.Count > 0 Then
                Me.cmbSearchAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbSearchAccount.DisplayLayout.Bands(0).Columns("Email").Hidden = True
            End If
        ElseIf strCondition = "SO" Then
            str = "Select SalesID, SalesNo from SalesMasterTable where SalesId not in(select PoId from salesReturnMasterTable)"
            FillDropDown(cmbPo, str)

        ElseIf strCondition = "SOComplete" Then
            str = "Select SalesID, SalesNo from SalesMasterTable "
            FillDropDown(cmbPo, str)
            'ElseIf strCondition = "SM" Then
            '    str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE ISNULL(Sale_Order_Person,0)=1"
            '    FillDropDown(Me.cmbSalesMan, str)
        ElseIf strCondition = "grdLocation" Then
            'Task#16092015Load  Locations user wise
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Name from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order"
            'Else
            '    str = "Select Location_Id, Location_Name From tblDefLocation"
            'End If

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Me.grd.RootTable.Columns(GrdEnum.LocationId).ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
            End If
        ElseIf strCondition = "Company" Then
            'Task#16092015  Load Companies user wise
            'If getConfigValueByType("UserwiseCompany").ToString = "True" Then
            '    str = "Select CompanyId, CompanyName From CompanyDefTable WHERE CompanyName <> ''  " & IIf(flgCompanyRights = True, " and CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")  Order By 1"
            'Else
            '    str = "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & "  Order By 1"
            'End If

            ' Commented because of wrong query     " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & "

            str = "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & " And CompanyId Is Not Null) " _
                           & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable WHERE CompanyName <> '' And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")" _
                           & "Else " _
                           & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""

            FillDropDown(Me.cmbCompany, str, False)
        ElseIf strCondition = "Project" Then
            str = String.Empty
            FillDropDown(Me.cmbProject, "If  exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " and ISNULL(CostCentre_Id, 0) > 0) " _
                    & "Select CostCenterID, Name from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") order by SortOrder " _
                    & "Else " _
                    & "Select CostCenterID, Name from tblDefCostCenter where Active = 1 order by SortOrder")
        ElseIf strCondition = "ArticlePack" Then
            Me.cmbUnit.ValueMember = "ArticlePackId"
            Me.cmbUnit.DisplayMember = "PackName"
            Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
        ElseIf strCondition = "ItemDescriptions" Then
            FillDropDown(Me.cmbItemDescription, "select ItemDescription,ItemDescription from quotationmastertable where ItemDescription <> ''", False)
        ElseIf strCondition = "TermsCondition" Then
            FillDropDown(Me.cmbTermCondition, "select * From tblTermsAndConditionType", True)
        ElseIf strCondition = "RevisionNumber" Then
            str = "Select Distinct RevisionNumber, RevisionNumber FROM QuotationHistory"
            FillDropDown(Me.cmbRevisionNumber, str, False)
        ElseIf strCondition = "Currency" Then ''TASK-407
            str = String.Empty
            str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
            FillDropDown(Me.cmbCurrency, str, False)
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
            '28-03-2018 : Ayesha Rehman : TFS2827 : Filling Combo Discount Type 
        ElseIf strCondition = "Discount Type" Then
            str = "select DiscountID, DiscountType from tblDiscountType "
            FillDropDown(Me.cmbDiscountType, str, False)
        ElseIf strCondition = "grdDiscountType" Then
            str = "select DiscountID, DiscountType from tblDiscountType"
            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns("DiscountId").ValueList.PopulateValueList(dt.DefaultView, "DiscountID", "DiscountType")
            ''End TFS2827
        End If
    End Sub
    Private Sub txtPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaid.TextChanged
        Try
            txtBalance.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(txtPaid.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function Save() As Boolean

        If chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        Me.grd.UpdateData()
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("SO", 6, "QuotationMasterTable", "QuotationNo")
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
            'objCommand.CommandText = "Insert into QuotationMasterTable (LocationId,QuotationNo,QuotationDate,VendorId,SalesOrderQty,SalesOrderAmount, CashPaid, Remarks,UserName, status) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(txtTotalQty.Text) & "," & Val(txtAmount.Text) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "')"

            'objCommand.CommandText = "Insert into QuotationMasterTable (LocationId,QuotationNo,QuotationDate,VendorId,SalesOrderQty,SalesOrderAmount, CashPaid, Remarks,UserName, status) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "')"
            'objCommand.ExecuteNonQuery()

            NetAmount = (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.Total), Janus.Windows.GridEX.AggregateFunction.Sum)))
            'Marked by Ali Ansari for task#2015052
            '            objCommand.CommandText = "Insert into QuotationMasterTable (LocationId,QuotationNo,QuotationDate,VendorId,SalesOrderQty,SalesOrderAmount, CashPaid, Remarks,UserName, status, SpecialAdjustment,Posted, PONo, NewCustomer,Delivery_Date, Adj_Flag, Adjustment,CostCenterId, PO_Date,EditionalTax_Percentage,SED_Percentage, Terms_And_Condition) values( " _
            '                                  & Me.cmbCompany.SelectedValue & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & NetAmount & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', N'" & Me.txtNewCustomer.Text.Replace("'", "''") & "', " & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", " & IIf(Me.rbtAdjFlat.Checked = True, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", " & Me.cmbProject.SelectedValue & ", " & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & TransitPercent & "," & WHTaxPercent & ",N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "') Select @@Identity"
            '          getVoucher_Id = objCommand.ExecuteScalar 'objCommand.ExecuteNonQuery()
            'Marked by Ali Ansari for task#2015052
            'Altered by Ali Ansari for task#2015052

            'objCommand.CommandText = "Insert into QuotationMasterTable (LocationId,QuotationNo,QuotationDate,VendorId,SalesOrderQty,SalesOrderAmount, CashPaid, Remarks,UserName, status, SpecialAdjustment,Posted, PONo, NewCustomer,Delivery_Date, Adj_Flag, Adjustment,CostCenterId, PO_Date,EditionalTax_Percentage,SED_Percentage, Terms_And_Condition,Apprved) values( " _
            '                       & Me.cmbCompany.SelectedValue & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & NetAmount & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', N'" & Me.txtNewCustomer.Text.Replace("'", "''") & "', " & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", " & IIf(Me.rbtAdjFlat.Checked = True, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", " & Me.cmbProject.SelectedValue & ", " & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & TransitPercent & "," & WHTaxPercent & ",N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "', " & IIf(Me.ChkApproved.Checked = True, 1, 0) & ") Select @@Identity"
            'TFS# 956: Insert Terms and Conditions and their details in QuotationTermsDetails table by Ali Faisal on 19-June-2017
            'objCommand.CommandText = "Insert into QuotationMasterTable(LocationId,QuotationNo,QuotationDate,VendorId,SalesOrderQty,SalesOrderAmount, CashPaid, Remarks,UserName, status, SpecialAdjustment,Posted, PONo, NewCustomer,Delivery_Date, Adj_Flag, Adjustment,CostCenterId, PO_Date,EditionalTax_Percentage,SED_Percentage, Terms_And_Condition,Apprved,CustomerPhone,CustomerAddress,Approved_User, RevisionNumber) values( " _
            '                       & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbCompany.SelectedValue) & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & NetAmount & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', N'" & Me.txtNewCustomer.Text.Replace("'", "''") & "', " & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", " & IIf(Me.rbtAdjFlat.Checked = True, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", " & Me.cmbProject.SelectedValue & ", " & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & TransitPercent & "," & WHTaxPercent & ", N'" & ReplaceNewLine(Me.txtTerms_And_Condition.Text, False).Replace("'", "''") & "', " & IIf(Me.ChkApproved.Checked = True, 1, 0) & ",N'" & Me.txtCustomerMobile.Text.Replace("'", "''").ToString & "',N'" & Me.txtCustomerAddress.Text.Replace("'", "''").ToString & "'," & IIf(ChkApproved.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & ", 0) Select @@Identity"
            '& IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbCompany.SelectedValue) & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & NetAmount & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', N'" & Me.txtNewCustomer.Text.Replace("'", "''") & "', " & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", " & IIf(Me.rbtAdjFlat.Checked = True, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", " & Me.cmbProject.SelectedValue & ", " & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & TransitPercent & "," & WHTaxPercent & ",N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "', " & IIf(Me.ChkApproved.Checked = True, 1, 0) & ",N'" & Me.txtCustomerMobile.Text.Replace("'", "''").ToString & "',N'" & Me.txtCustomerAddress.Text.Replace("'", "''").ToString & "'," & IIf(ChkApproved.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & ", 0) Select @@Identity"
            ''TFS1798 Added column EmployeeId
            ''TFS1674 Added column ManualSerialNo
            objCommand.CommandText = "Insert into QuotationMasterTable(LocationId,QuotationNo,QuotationDate,VendorId,SalesOrderQty,SalesOrderAmount, CashPaid, Remarks,UserName, status, SpecialAdjustment,Posted, PONo, NewCustomer,Delivery_Date, Adj_Flag, Adjustment,CostCenterId, PO_Date,EditionalTax_Percentage,SED_Percentage, Terms_And_Condition,Apprved,CustomerPhone,CustomerAddress,Approved_User, RevisionNumber,TermsAndConditionsId,EmployeeId,ManualSerialNo, Subject) values( " _
                                   & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbCompany.SelectedValue) & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & NetAmount & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', N'" & Me.txtNewCustomer.Text.Replace("'", "''") & "', " & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", " & IIf(Me.rbtAdjFlat.Checked = True, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", " & Me.cmbProject.SelectedValue & ", " & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & TransitPercent & "," & WHTaxPercent & ", N'" & ReplaceNewLine(Me.txtTerms_And_Condition.Text, False).Replace("'", "''") & "', " & IIf(Me.ChkApproved.Checked = True, 1, 0) & ",N'" & Me.txtCustomerMobile.Text.Replace("'", "''").ToString & "',N'" & Me.txtCustomerAddress.Text.Replace("'", "''").ToString & "'," & IIf(ChkApproved.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & ", 0," & Me.cmbTermCondition.SelectedValue & "," & Me.cmbSalesMan.SelectedValue & ", N'" & txtManualSerialNo.Text & "', N'" & txtSubject.Text & "') Select @@Identity"
            getVoucher_Id = objCommand.ExecuteScalar
            If Me.grdTerms.RowCount > 0 Then
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdTerms.GetDataRows
                    objCommand.CommandText = "Insert into QuotationTermsDetails (TermId,QuotationId,Heading,Details) Values (" & Me.cmbTermCondition.SelectedValue & ",ident_current('QuotationMasterTable'),N'" & row.Cells("Heading").Value.ToString & "',N'" & row.Cells("Details").Value.ToString & "')"
                    objCommand.ExecuteNonQuery()
                Next
            End If
            'TFS# 956: End

            'objCommand.ExecuteNonQuery()
            'Altered by Ali Ansari for task#2015052
            'Marked Against Task#2015060001 Ali Ansari
            'If arrFile.Length > 0 Then
            '    SaveDocument(getVoucher_Id, Me.Name, trans)
            'End If
            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                SaveDocument(getVoucher_Id, Me.Name, trans)
            End If
            'Altered Against Task#2015060001 Ali Ansari

            ''For i = 0 To grd.Rows.Count - 1
            '    If grd.Rows(i).Cells("Qty").Value <> 0 Then
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "Insert into QuotationDetailTable (QuotationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice) values( " _
            '                                & " ident_current('QuotationMasterTable')," & Val(grd.Rows(i).Cells(7).Value) & ",N'" & (grd.Rows(i).Cells(2).Value) & "'," & Val(grd.Rows(i).Cells(3).Value) & ", " _
            '                                & " " & IIf(grd.Rows(i).Cells(2).Value = "Loose", Val(grd.Rows(i).Cells(3).Value), (Val(grd.Rows(i).Cells(3).Value) * Val(grd.Rows(i).Cells(8).Value))) & ", " & Val(grd.Rows(i).Cells(4).Value) & ", " & Val(grd.Rows(i).Cells(8).Value) & " , " & Val(grd.Rows(i).Cells(9).Value) & " ) "

            '        objCommand.ExecuteNonQuery()
            '        'Val(grd.Rows(i).Cells(5).Value)
            '    End If
            'Next
            For i = 0 To grd.RowCount - 1
                'If Val(grd.GetRows(i).Cells("Qty").Value) <> 0 Then
                'objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into QuotationDetailTable (QuotationId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice,PackPrice, Pack_Desc, Comments) values( " _
                '                        & " ident_current('QuotationMasterTable'), " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                'objCommand.ExecuteNonQuery()

                ''R-916 Added Column Comments
                objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into QuotationDetailTable (QuotationId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, SalesTax_Percentage, SchemeQty, Discount_Percentage,PurchasePrice,PackPrice, Pack_Desc, Comments,ItemDescription,BrandName,Specification,RegistrationNo,TradePrice,TenderSrNo) values( " _
                '                        & " ident_current('QuotationMasterTable'), " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "','" & Me.grd.GetRows(i).Cells(GrdEnum.ItemDescription).Value.ToString.Replace("'", "''") & "','" & Me.grd.GetRows(i).Cells(GrdEnum.Brand).Value.ToString.Replace("'", "''") & "','" & Me.grd.GetRows(i).Cells(GrdEnum.Specification).Value.ToString.Replace("'", "''") & "','" & Me.grd.GetRows(i).Cells(GrdEnum.ItemRegistrationNo).Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & ",'" & Me.grd.GetRows(i).Cells(GrdEnum.TenderSrNo).Value.ToString.Replace("'", "''") & "') "


                objCommand.CommandText = "Insert into QuotationDetailTable (QuotationId, LocationId, ArticleDefId, ArticleSize, Sz1, Qty, Price, Sz7, CurrentPrice, SalesTax_Percentage, SchemeQty, Discount_Percentage,PurchasePrice,PackPrice, Pack_Desc, Comments,ItemDescription,BrandName,Specification,RegistrationNo,TradePrice,TenderSrNo,CostPrice,SED_Tax_Percent, SED_Tax_Amount,SOQuantity, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, RequirementDescription, PurchaseInquiryDetailId, VendorQuotationDetailId, HeadArticleId, SerialNo, PurchaseInquiryId, DiscountId, DiscountValue, DiscountFactor , PostDiscountPrice, IBrand, PartNo, Warranty, LeadTime, State) values( " _
                                      & " ident_current('QuotationMasterTable'), " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                                      & " " & Val(grd.GetRows(i).Cells(GrdEnum.TotalQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "','" & Me.grd.GetRows(i).Cells(GrdEnum.ItemDescription).Value.ToString.Replace("'", "''") & "','" & Me.grd.GetRows(i).Cells(GrdEnum.Brand).Value.ToString.Replace("'", "''") & "','" & Me.grd.GetRows(i).Cells(GrdEnum.Specification).Value.ToString.Replace("'", "''") & "','" & Me.grd.GetRows(i).Cells(GrdEnum.ItemRegistrationNo).Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & ",'" & Me.grd.GetRows(i).Cells(GrdEnum.TenderSrNo).Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("SED_Tax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("SED_Tax_Amount").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("SOQuantity").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("RequirementDescription").Value.ToString.Replace("'", "''") & "', " & IIf(Val(Me.grd.GetRows(i).Cells("PurchaseInquiryDetailId").Value.ToString) = 0, "NULL", Val(Me.grd.GetRows(i).Cells("PurchaseInquiryDetailId").Value.ToString)) & ", " & Val(Me.grd.GetRows(i).Cells("VendorQuotationDetailId").Value.ToString) & ", " & IIf(Val(Me.grd.GetRows(i).Cells("HeadArticleId").Value.ToString) = 0, "NULL", Val(Me.grd.GetRows(i).Cells("HeadArticleId").Value.ToString)) & ", N'" & Me.grd.GetRows(i).Cells("SerialNo1").Value.ToString.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells("PurchaseInquiryId").Value.ToString) & "," & Val(grd.GetRows(i).Cells("DiscountId").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("DiscountValue").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("DiscountFactor").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PostDiscountPrice").Value.ToString) & ", '" & Me.grd.GetRows(i).Cells("IBrand").Value.ToString & "', '" & Me.grd.GetRows(i).Cells("PartNo").Value.ToString & "', '" & Me.grd.GetRows(i).Cells("Warranty").Value.ToString & "', '" & Me.grd.GetRows(i).Cells("LeadTime").Value.ToString & "', '" & Me.grd.GetRows(i).Cells("State").Value.ToString & "')"


                'objCommand.CommandText = "Insert into QuotationDetailTable (QuotationId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, SalesTax_Percentage, SchemeQty, Discount_Percentage,PurchasePrice,PackPrice, Pack_Desc, Comments,ItemDescription,BrandName,Specification,RegistrationNo,TradePrice,TenderSrNo,Pack_40Kg_Weight) values( " _
                '                        & " ident_current('QuotationMasterTable'), " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                '                        & " " & Val(Me.grd.GetRows(i).Cells(GrdEnum.TotalQty).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("ItemDescription").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Brand").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Specification").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("ItemRegistrationNo").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("TradePrice").Value.ToString.Replace("'", "''")) & ",'" & Me.grd.GetRow(i).Cells("TenderSrNo").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells(GrdEnum.Pack_40Kg_Weight).Value) & ") "
                objCommand.ExecuteNonQuery()
                'Val(grd.Rows(i).Cells(5).Value)
                'End If
            Next
            '' Start TASK-480 on 12-05-2016
            If getConfigValueByType("EnableDuplicateQuotation").ToString.ToUpper = "TRUE" Then
                Call CreateDuplicationQuotation(getVoucher_Id, "Save", trans)
            End If
            ''END TASK-480
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
            NotificationDAL.SaveAndSendNotification("Quotation", "QuotationMasterTable", getVoucher_Id, ValueTable, "Sales > Quotation")
        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try

        ''insert Activity Log
        SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Sales, Me.txtPONo.Text.Trim, True)
        ''Start TFS3113
        ''Start TFS2375
        ''insert Approval Log
        SaveApprovalLog(EnumReferenceType.SalesQuotation, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, " Sales Quotation," & cmbVendor.Text & "", Me.Name)
        ''End TFS2375
        ''End TFS3113

    End Function


    Sub InsertVoucher()

        Try
            SaveVoucherEntry(GetVoucherTypeId("SV"), GetNextDocNo("SV", 6, "tblVoucher", "voucher_no"), Me.dtpPODate.Value, "", Nothing, Val(Me.cmbVendor.ActiveRow.Cells(0).Text.ToString), getConfigValueByType("SalesCreditAccount"), Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)), Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)), "Both", Me.Name, Me.txtPONo.Text, True)
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try

    End Sub

    Private Function FormValidate() As Boolean

        If txtPONo.Text = "" Then
            msg_Error("Please enter PO No.")
            txtPONo.Focus() : FormValidate = False : Exit Function
        End If

        ''Task#A3 12-06-2015 Check Vendor exist in combox list or not
        If Not Me.cmbVendor.IsItemInList Then
            msg_Error("Please select Vendor")
            Me.cmbVendor.Focus() : FormValidate = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/25/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : FormValidate = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/25/2022)
        If Me.cmbVendor.Text = String.Empty Then
            msg_Error("Please select Vendor")
            Me.cmbVendor.Focus() : FormValidate = False : Exit Function
        End If
        ''End Task#A3 12-06-2015

        'If cmbVendor.ActiveRow.Cells(0).Value <= 0 Then
        '    msg_Error("Please select customer")
        '    cmbVendor.Focus() : FormValidate = False : Exit Function
        'End If

        If Not Me.grd.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            cmbItem.Focus() : FormValidate = False : Exit Function
        End If
        ''Start TFS2988
        If IsEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document is in Approval Process") : Return False : Exit Function
                End If
            End If
        End If
        ''End TFS2988

        Return True

    End Function

    Sub EditRecord()
        Try


            IsEditMode = True
            'Mode = "Edit"
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            'Me.FillCombo("SOComplete")
            Me.cmbCompany.SelectedValue = grdSaved.GetRow.Cells("LocationId").Value
            txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value.ToString
            Me.GetSecurityRights()
            'Task 1592
            If grdSaved.CurrentRow.Cells(1).Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
                dtpPODate.MaxDate = dtpPODate.Value.AddMonths(3)
                dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            Else
                dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            End If
            txtReceivingID.Text = grdSaved.CurrentRow.Cells("QuotationId").Value
            'TODO. ----
            ''R933  Validate Customer Data 
            cmbVendor.Value = grdSaved.CurrentRow.Cells("VendorId").Value
            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Vendor is disable.")
                Exit Sub
            End If
            'Terms_And_Condition
            'Task#2015060007
            ''Start TFS3113
            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
            Me.btnApprovalHistory.Visible = True
            Me.btnApprovalHistory.Enabled = True
            ''Ayesha Rehman :TFS2375 :End
            ''End TFS3113
            If Not Me.cmbTermCondition.SelectedIndex = -1 Then
                Me.cmbTermCondition.SelectedIndex = 0
            End If
            Me.txtTerms_And_Condition.Text = grdSaved.CurrentRow.Cells("Terms_And_Condition").Value.ToString
            'Task#2015060007

            'TFS# 956: Get Terms and Conditions and their details from QuotationTermsDetails table in Edit Mode by Ali Faisal on 19-June-2017
            TermsAndConditionsId = Val(Me.grdSaved.CurrentRow.Cells("TermsAndConditionsId").Value.ToString)
            Me.cmbTermCondition.SelectedValue = TermsAndConditionsId
            Dim str As String = ""
            Dim dt As DataTable
            str = "Select * from QuotationTermsDetails Where TermId=" & Me.grdSaved.CurrentRow.Cells("TermsAndConditionsId").Value.ToString & " And QuotationId = " & Me.txtReceivingID.Text & ""
            dt = GetDataTable(str)
            Me.grdTerms.DataSource = dt
            Me.grdTerms.RetrieveStructure()
            ApplyTermsGridSettings()
            Me.grdTerms.RootTable.Columns("QuotationId").Visible = False
            'TFS# 956: End
            Me.txtNewCustomer.Text = grdSaved.CurrentRow.Cells("NewCustomer").Value.ToString
            Me.txtSubject.Text = grdSaved.CurrentRow.Cells("Subject").Value.ToString
            txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
            txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""
            ''Start TFS1798 :Added By Ayesha Rehman 
            If (grdSaved.CurrentRow.Cells("EmployeeId").Value) Is Nothing OrElse IsDBNull(grdSaved.CurrentRow.Cells("EmployeeId").Value) Then
                Me.cmbSalesMan.SelectedValue = 0
            Else
                Me.cmbSalesMan.SelectedValue = grdSaved.CurrentRow.Cells("EmployeeId").Value
            End If
            ''End TFS1798

            ''TFS1674 :Added By Ayesha Rehman 
            Me.txtManualSerialNo.Text = grdSaved.CurrentRow.Cells("ManualSerialNo").Value.ToString
            ''End TFS1674

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
            'If IsDBNull(grdSaved.CurrentRow.Cells("SOP_ID")) Then
            '    Me.cmbSalesMan.SelectedValue = 0
            'Else
            '    Me.cmbSalesMan.SelectedValue = grdSaved.CurrentRow.Cells("SOP_ID").Value
            'End If
            'Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value

            Me.txtCustPONo.Text = Me.grdSaved.GetRow.Cells("PO No").Value.ToString
            Call DisplayDetail(grdSaved.CurrentRow.Cells("QuotationId").Value)
            'Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.BillValueAfterDiscount), Janus.Windows.GridEX.AggregateFunction.Sum)
            Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.Total), Janus.Windows.GridEX.AggregateFunction.Sum)
            If IsDBNull(grdSaved.CurrentRow.Cells("Delivery_Date").Value) Then
                Me.dtpDeliveryDate.Value = Date.Now
                Me.dtpDeliveryDate.Checked = False
            Else
                dtpDeliveryDate.Value = grdSaved.CurrentRow.Cells("Delivery_Date").Value
                Me.dtpDeliveryDate.Checked = True
            End If

            GetTotal()
            Me.BtnSave.Text = "&Update"
            Me.cmbPo.Enabled = True
            'GetSecurityRights()
            '//Start TASK # 1592
            '24-OCT-2017: Ayesha Rehman: If user dont have update rights then btnsave should not be enable true
            If BtnSave.Enabled = True Then
                If grdSaved.CurrentRow.Cells(1).Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
                    Me.BtnSave.Enabled = False
                    ErrorProvider1.SetError(Me.Label2, "Future Date can not be edit")
                    ErrorProvider1.BlinkRate = 1000
                    ErrorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
                Else
                    Me.BtnSave.Enabled = True
                    ErrorProvider1.Clear()
                End If
            End If
            'End Task # 1592
            'Altered by Ali Ansari for task#2015052
            Me.chkPost.Checked = Me.grdSaved.GetRow.Cells("Posted").Value
            Me.ChkApproved.Checked = Me.grdSaved.GetRow.Cells("Approved").Value
            Me.ChkApproved.Visible = True
            'Altered by Ali Ansari for task#2015052
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
                If LoginGroup = "Administrator" Then
                    Me.dtpPODate.Enabled = True
                End If
            End If

            'Altered Against Task# 2015060001 Ali Ansari
            'Get no of attached files
            Dim intCountAttachedFiles As Integer = 0I
            If Me.BtnSave.Text <> "&Save" Then
                If Me.grdSaved.RowCount > 0 Then
                    intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                    Me.btnAttachment.Text = "Attachment (" & intCountAttachedFiles & ")"
                End If
            End If
            'Altered Against Task# 2015060001 Ali Ansari
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form

            'Task#07082015 Edit Item Description and New Customer information (Ahmad Sharif)

            Me.txtCustomerMobile.Text = Me.grdSaved.CurrentRow.Cells("Inco Term").Value.ToString
            Me.txtCustomerAddress.Text = Me.grdSaved.CurrentRow.Cells("Payment Term").Value.ToString

            'If Convert.ToBoolean(Me.grdSaved.CurrentRow.Cells("Item Information").Value.ToString) = True Then
            '    Me.chkAddItemDescription.Checked = True
            '    Me.GroupBox7.Visible = True
            '    Me.txtBrand.Text = Me.grdSaved.CurrentRow.Cells("Brand").Value.ToString
            '    Me.txtSpecs.Text = Me.grdSaved.CurrentRow.Cells("Item Specs").Value.ToString
            '    Me.txtItemRegNo.Text = Me.grdSaved.CurrentRow.Cells("Item Reg. No").Value.ToString
            '    Me.txtTradePrice.Text = Me.grdSaved.CurrentRow.Cells("Trade Price").Value.ToString
            '    Me.cmbItemDescription.Text = Me.grdSaved.CurrentRow.Cells("Item Description").Value.ToString
            'Else
            '    Me.chkAddItemDescription.Checked = False
            'End If

            'End Task#07082015

            Me.BtnDelete.Visible = True
            Me.BtnPrint.Visible = True
            ''TASK-481 on 13-07-2016
            Dim RevisionNumber As Integer = Me.CheckRevisionNumber(grdSaved.CurrentRow.Cells("QuotationId").Value)
            If RevisionNumber > 0 Then
                IsRevisionRestrictedFirstTime = True
                Me.FillRevisionCombo(grdSaved.CurrentRow.Cells("QuotationId").Value)
                Me.cmbRevisionNumber.Visible = False
                Me.lblRev.Visible = False
                Me.txtPONo.Size = New System.Drawing.Size(98, 21)
                Me.lnkLblRevisions.Visible = True
                Me.lnkLblRevisions.Text = "Rev (" & RevisionNumber & ")"
            Else
                Me.cmbRevisionNumber.Visible = False
                Me.lblRev.Visible = False
                Me.lnkLblRevisions.Visible = False
                Me.txtPONo.Size = New System.Drawing.Size(174, 21)
            End If
            ''End TASK-481
            ''''''''''''''''''''''''''

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
            'If Condition = "All" Then
            'If getConfigValueByType("LoadAllItemsInSales").ToString = "True" Then
            '    'str = " SELECT  Detail.LocationId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription as Item,  " _
            '    '   & " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Price, Isnull(Detail.Total,0) as Total,  " _
            '    '   & " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [PackQty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as SalesTax_Percentage, ISNULL(Detail.[SchemeQty],0) as [SchemeQty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc  " _
            '    '   & " From ArticleDefView  LEFT OUTER JOIN (   " _
            '    '   & " SELECT Recv_D.LocationId, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate,   " _
            '    '   & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price)) END AS Total,   " _
            '    '   & " Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.SalesOrderDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.SalesTax_Percentage,0) as Tax, 0 as savedqty , SchemeQty as [SchemeQty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc  FROM QuotationDetailTable Recv_D   " _
            '    '   & " Where(Recv_D.QuotationId = " & ReceivingID & ")  " _
            '    '   & " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId  " _
            '    '   & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts " _
            '    '   & " WHERE(TypeId = " & IIf(TypeId > 0, TypeId, -1) & "))  CustomerDiscount  On CustomerDiscount.articledefid=articledefview.articleId WHERE ArticleDefView.SalesItem=1 " & IIf(IsEditMode = False, " And ArticleDefView.Active=1", "") & " ORDER By ArticleDefView.SortOrder Asc"

            '    ''R-916 Added Column Comments
            '    str = " SELECT  Detail.LocationId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription as Item,  " _
            '      & " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Price, Isnull(Detail.Total,0) as Total,  " _
            '      & " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [PackQty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as SalesTax_Percentage, ISNULL(Detail.[SchemeQty],0) as [SchemeQty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Detail.Comments  " _
            '      & " From ArticleDefView  LEFT OUTER JOIN (   " _
            '      & " SELECT Recv_D.LocationId, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate,   " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price)) END AS Total,   " _
            '      & " Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.SalesOrderDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.SalesTax_Percentage,0) as Tax, 0 as savedqty , SchemeQty as [SchemeQty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments  FROM QuotationDetailTable Recv_D   " _
            '      & " Where(Recv_D.QuotationId = " & ReceivingID & ")  " _
            '      & " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId  " _
            '      & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts " _
            '      & " WHERE(TypeId = " & IIf(TypeId > 0, TypeId, -1) & "))  CustomerDiscount  On CustomerDiscount.articledefid=articledefview.articleId WHERE ArticleDefView.SalesItem=1 " & IIf(IsEditMode = False, " And ArticleDefView.Active=1", "") & " ORDER By ArticleDefView.SortOrder Asc"
            '    ''End R-916
            'End If
            'Else
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
            '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice, Isnull(recv_d.TradePrice,0) as TradePrice, Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, ISNULL(recv_d.Freight,0) as Freight, ISNULL(Recv_d.MarketReturns,0) as MarketReturns, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc  FROM dbo.QuotationDetailTable Recv_D INNER JOIN " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '      & " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY Article.SortOrder Asc"

            ''R-916 Added Column Comments
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
            '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
            '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice,Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.ItemDescription ,Recv_D.BrandName as Brand, Recv_D.Specification, Recv_D.RegistrationNo as ItemRegistrationNo, IsNull(Recv_D.TradePrice,0) as TradePrice, Recv_D.TenderSrNo  FROM dbo.QuotationDetailTable Recv_D INNER JOIN " _
            '    & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '    & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '    & " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY Article.SortOrder Asc"
            ''End R-916
            'End If


            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As TotalCurrencyAmount, " _
            ' & "  (IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total,  " _
            ' & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice,Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as SalesTaxAmount, Convert(float,0) as CurrencySalesTaxAmount, IsNull(SED_Tax_Percent,0) as [SED_Tax_Percent], IsNull(SED_Tax_Amount,0) as [SED_Tax_Amount], Convert(float,0) as CurrencySEDAmount, Convert(float,0) as Net_Amount, ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.ItemDescription ,Recv_D.BrandName as Brand, Recv_D.Specification, Recv_D.RegistrationNo as ItemRegistrationNo, IsNull(Recv_D.TradePrice,0) as TradePrice, Recv_D.TenderSrNo,IsNull(Recv_D.CostPrice,0) as CostPrice, ISNULL(Recv_D.QuotationId, 0) As QuotationId, ISNULL(Recv_D.QuotationDetailId, 0) As QuotationDetailId, ISNULL(Recv_D.SOQuantity, 0) As SOQuantity, IsNull(Recv_D.Qty, 0) As TotalQty, Recv_D.RequirementDescription, IsNull(Recv_D.PurchaseInquiryDetailId, 0) As PurchaseInquiryDetailId,  IsNull(Recv_D.VendorQuotationDetailId, 0) As VendorQuotationDetailId, Recv_D.HeadArticleId As HeadArticleId FROM dbo.QuotationDetailTable Recv_D INNER JOIN " _
            ' & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            ' & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            ' & " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY Article.SortOrder Asc"
            ''Start TFS3695 : Ayesha Rehman : 28-06-2018 ''Query changed (Article.SortOrder) against TFS3695
            ''Start TFS3695 : Ayesha Rehman : 28-06-2018 ''Query changed Recv_D.SerialNo against TFS3695
            ''Commented Agianst TFS4333 : Error Occured While Updating
            'str = "SELECT CAST(Case when Recv_D.SerialNo IS Null OR Recv_D.SerialNo = '' then 0 else Recv_D.SerialNo end AS Numeric(10,0)) As SerialNo1, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.RequirementDescription, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty,CASE WHEN IsNull(Recv_D.PostDiscountPrice, 0) = 0 THEN IsNULL(Article.SalePrice,0) ELSE IsNull(Recv_D.PostDiscountPrice, 0) END AS PostDiscountPrice , Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyId, 0)=1 Then 1 When IsNull(Recv_D.CurrencyRate, 0) = 0  Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As TotalCurrencyAmount, Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue ," _
            ' & "  (IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * Case When IsNull(Recv_D.CurrencyId, 0)=1 Then 1 When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total,  " _
            ' & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice,Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as SalesTaxAmount, Convert(float,0) as CurrencySalesTaxAmount, IsNull(SED_Tax_Percent,0) as [SED_Tax_Percent], IsNull(SED_Tax_Amount,0) as [SED_Tax_Amount], Convert(float,0) as CurrencySEDAmount, Convert(float,0) as Net_Amount, ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.ItemDescription ,Recv_D.BrandName as Brand, Recv_D.Specification, Recv_D.RegistrationNo as ItemRegistrationNo, IsNull(Recv_D.TradePrice,0) as TradePrice, Recv_D.TenderSrNo,IsNull(Recv_D.CostPrice,0) as CostPrice, ISNULL(Recv_D.QuotationId, 0) As QuotationId, ISNULL(Recv_D.QuotationDetailId, 0) As QuotationDetailId, ISNULL(Recv_D.SOQuantity, 0) As SOQuantity, IsNull(Recv_D.Qty, 0) As TotalQty, Recv_D.PurchaseInquiryDetailId,  IsNull(Recv_D.VendorQuotationDetailId, 0) As VendorQuotationDetailId, Recv_D.HeadArticleId As HeadArticleId, IsNull(Recv_D.PurchaseInquiryId, 0) As PurchaseInquiryId, Convert(bit,Recv_D.Alternate) As Alternate FROM dbo.QuotationDetailTable Recv_D LEFT OUTER JOIN " _
            ' & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            ' & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            ' & " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY 1  Asc "

            str = "SELECT " & IIf(SerialNoIncludingcharacter = True, "Case when Recv_D.SerialNo IS Null OR Recv_D.SerialNo = '' then Convert(nvarchar, 0) else Recv_D.SerialNo end As SerialNo1", "Case when Recv_D.SerialNo = '' OR Recv_D.SerialNo IS Null  then 0 else CONVERT(decimal(17,3),Recv_D.SerialNo) end  As SerialNo1") & ", Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.RequirementDescription, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty,CASE WHEN IsNull(Recv_D.PostDiscountPrice, 0) = 0 THEN IsNULL(Article.SalePrice,0) ELSE IsNull(Recv_D.PostDiscountPrice, 0) END AS PostDiscountPrice , Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyId, 0)=1 Then 1 When IsNull(Recv_D.CurrencyRate, 0) = 0  Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As TotalCurrencyAmount, Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue ," _
             & "  (IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * Case When IsNull(Recv_D.CurrencyId, 0)=1 Then 1 When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total,  " _
             & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice,Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as SalesTaxAmount, Convert(float,0) as CurrencySalesTaxAmount, IsNull(SED_Tax_Percent,0) as [SED_Tax_Percent], IsNull(SED_Tax_Amount,0) as [SED_Tax_Amount], Convert(float,0) as CurrencySEDAmount, Convert(float,0) as Net_Amount, ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.ItemDescription ,Recv_D.BrandName as Brand, Recv_D.Specification, Recv_D.RegistrationNo as ItemRegistrationNo, IsNull(Recv_D.TradePrice,0) as TradePrice, Recv_D.TenderSrNo,IsNull(Recv_D.CostPrice,0) as CostPrice, ISNULL(Recv_D.QuotationId, 0) As QuotationId, ISNULL(Recv_D.QuotationDetailId, 0) As QuotationDetailId, ISNULL(Recv_D.SOQuantity, 0) As SOQuantity, IsNull(Recv_D.Qty, 0) As TotalQty, Recv_D.PurchaseInquiryDetailId,  IsNull(Recv_D.VendorQuotationDetailId, 0) As VendorQuotationDetailId, Recv_D.HeadArticleId As HeadArticleId, IsNull(Recv_D.PurchaseInquiryId, 0) As PurchaseInquiryId, Convert(bit,Recv_D.Alternate) As Alternate, Recv_D.IBrand, Recv_D.PartNo, Recv_D.Warranty, Recv_D.LeadTime, Recv_D.State  FROM dbo.QuotationDetailTable Recv_D LEFT OUTER JOIN " _
             & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
             & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
             & " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY 1  Asc "


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
            'If IsSalesOrderAnalysis = True Then
            '    dtDisplayDetail.Columns(GrdEnum.BillValueAfterDiscount).Expression = "IIF(Unit='Loose',((((((Qty * CurrentPrice) * SalesTax_Percentage)/100) + (((SchemeQty*CurrentPrice)*SalesTax_Percentage)/100)) + (Freight*(Qty+SchemeQty))) - (((Qty*CurrentPrice)*Discount_Percentage)/100)  +  (Qty * CurrentPrice)), (((((((Qty * PackQty) * CurrentPrice) * SalesTax_Percentage)/100) + (((SchemeQty * CurrentPrice) * SalesTax_Percentage)/100)) + (Freight * ((Qty * PackQty)+SchemeQty))) - ((((Qty * PackQty) * CurrentPrice) * Discount_Percentage)/100)  +  ((Qty * PackQty)* CurrentPrice)))"
            '    'dtDisplayDetail.Columns(GrdEnum.Rate).Expression = "CurrentPrice-MarketReturns-IIF(Discount_Percentage=0,0,((CurrentPrice*Discount_Percentage)/100))"
            'End If
            'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',((PackQty*Qty)*Price), Qty*Price) + IIF(Unit='Loose', (((SchemeQty * CurrentPrice) * SalesTax_Percentage)/100), ((((SchemeQty * PackQty) *  CurrentPrice) * SalesTax_Percentage)/100))"
            'TASK-408
            'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',((IsNull([PackQty],0)*IsNull([Qty],0))*IsNull([Price],0)), IsNull([Qty],0)*IsNull([Price],0))"
            dtDisplayDetail.Columns("DiscountValue").Expression = "IIF(DiscountId= 1,((IsNull(PostDiscountPrice,0)*IsNull(DiscountFactor,0))/100), IsNull(DiscountFactor,0))" ''TFS2827
            dtDisplayDetail.Columns("Price").Expression = "IIF(DiscountId= 1, IsNull(PostDiscountPrice,0)-((IsNull(PostDiscountPrice,0)*IsNull(DiscountFactor,0))/100), IsNull(PostDiscountPrice,0)-IsNull(DiscountFactor,0))"
            dtDisplayDetail.Columns("Total").Expression = "IsNull([TotalQty],0)*IsNull([Price],0)*IsNull([CurrencyRate], 0)"
            dtDisplayDetail.Columns("CurrencyAmount").Expression = "IsNull([TotalQty],0)*IsNull([Price],0)"
            dtDisplayDetail.Columns("SED_Tax_Amount").Expression = "((IsNull([SED_Tax_Percent],0)/100)*IsNull([Total],0))"
            dtDisplayDetail.Columns("CurrencySEDAmount").Expression = "((IsNull([SED_Tax_Percent],0)/100)*IsNull([CurrencyAmount],0))"
            ''Start TFS3758 : Ayesha Rehman : 05-07-2018
            If flgExcludeTaxPrice = False Then
                dtDisplayDetail.Columns("SalesTaxAmount").Expression = "((IsNull([SalesTax_Percentage],0)/100)*IsNull([Total],0))"
                dtDisplayDetail.Columns("CurrencySalesTaxAmount").Expression = "((IsNull([SalesTax_Percentage],0)/100)*IsNull([CurrencyAmount],0))"
                dtDisplayDetail.Columns("Net_Amount").Expression = "(IsNull([Total],0)+IsNull([SalesTaxAmount],0)+IsNull([SED_Tax_Amount],0))"
                dtDisplayDetail.Columns("TotalCurrencyAmount").Expression = "(IsNull([CurrencyAmount],0)+IsNull([CurrencySalesTaxAmount],0)+IsNull([CurrencySEDAmount],0))"
            Else
                dtDisplayDetail.Columns("SalesTaxAmount").Expression = "((IsNull([Total],0)/(IsNull([SalesTax_Percentage],0)+100)) * IsNull([SalesTax_Percentage],0))"
                dtDisplayDetail.Columns("CurrencySalesTaxAmount").Expression = "((IsNull([CurrencyAmount],0)/(IsNull([SalesTax_Percentage],0)+100)) *IsNull([SalesTax_Percentage],0))"
                dtDisplayDetail.Columns("Net_Amount").Expression = "(IsNull([Total],0)-IsNull([SalesTaxAmount],0)+IsNull([SED_Tax_Amount],0))"
                dtDisplayDetail.Columns("TotalCurrencyAmount").Expression = "(IsNull([CurrencyAmount],0)-IsNull([CurrencySalesTaxAmount],0)+IsNull([CurrencySEDAmount],0))"
            End If
            ''End TFS3758
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail

            
            FillCombo("grdLocation")
            'Ayesha Rehman: TFS2827 : 28-Mar-2018 : Fill combo boxes
            FillCombo("grdDiscountType")
            'Ayesha Rehman : TFS2827 : 28-Mar-2018 : End
            ApplyGridSetting()
            CtrlGrdBar3_Load(Nothing, Nothing)
            'GetSalesOrderAnalysis()
            If dtDisplayDetail.Rows.Count > 0 Then
                If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                    'Me.cmbCurrency.SelectedValue = Nothing

                    Me.cmbCurrency.Enabled = False
                Else
                    'IsCurrencyEdit = True
                    'IsNotCurrencyRateToAll = True
                    FillCombo("Currency")
                    Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                    Me.txtCurrencyRate.Text = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString)
                    Me.cmbCurrency.Enabled = False
                End If
                'cmbCurrency_SelectedIndexChanged(Nothing, Nothing)
            End If

            'If flgLoadItems = True Then
            '    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            '        If Me.grd.RowCount > 0 Then
            '            r.BeginEdit()
            '            r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
            '            r.EndEdit()
            '        End If
            '    Next
            'End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function Update_Record() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer


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



            NetAmount = (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.Total), Janus.Windows.GridEX.AggregateFunction.Sum)))

            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'Marked by Ali Ansari for task#2015052
            '            objCommand.CommandText = "Update QuotationMasterTable set QuotationNo =N'" & txtPONo.Text & "',QuotationDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '           & " SalesOrderQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", SalesOrderAmount=" & NetAmount & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', SpecialAdjustment=" & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", Posted=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", PONo=N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', NewCustomer=N'" & Me.txtNewCustomer.Text.Replace("'", "''") & "', Delivery_Date=" & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Adj_Flag=" & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", Adjustment=" & IIf(Me.rbtAdjFlat.Checked = False, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", CostCenterId=" & Me.cmbProject.SelectedValue & ", PO_Date=" & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", EditionalTax_Percentage=" & TransitPercent & ", SED_Percentage=" & WHTaxPercent & ", Terms_And_Condition=N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "' Where QuotationId= " & txtReceivingID.Text & " "
            'Marked by Ali Ansari for task#2015052

            'Altered by Ali Ansari for task#2015052
            'objCommand.CommandText = "Update QuotationMasterTable set QuotationNo =N'" & txtPONo.Text & "',QuotationDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '& " SalesOrderQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", SalesOrderAmount=" & NetAmount & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', SpecialAdjustment=" & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", Posted=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", PONo=N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', NewCustomer=N'" & Me.txtNewCustomer.Text.Replace("'", "''") & "', Delivery_Date=" & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Adj_Flag=" & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", Adjustment=" & IIf(Me.rbtAdjFlat.Checked = False, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", CostCenterId=" & Me.cmbProject.SelectedValue & ", PO_Date=" & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", EditionalTax_Percentage=" & TransitPercent & ", SED_Percentage=" & WHTaxPercent & ", Terms_And_Condition=N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "', Apprved=" & IIf(Me.ChkApproved.Checked = True, 1, 0) & ",CustomerPhone=N'" & Me.txtCustomerMobile.Text.Replace("'", "''").ToString & "',CustomerAddress=N'" & Me.txtCustomerAddress.Text.Replace("'", "''").ToString & "',ItemDescription=N'" & Me.cmbItemDescription.Text.Replace("'", "''").ToString & "',BrandName=N'" & Me.txtBrand.Text.Replace("'", "''").ToString & "',Specification=N'" & Me.txtSpecs.Text.Replace("'", "''").ToString & "',RegistrationNo=N'" & Me.txtItemRegNo.Text.Replace("'", "''").ToString & "',TradePrice=" & Me.txtTradePrice.Text.Replace("'", "''").ToString & ",Item_Info=" & IIf(Me.chkAddItemDescription.Checked = True, 1, 0) & " Where QuotationId= " & txtReceivingID.Text & " "
            'Altered by Ali Ansari for task#2015052 
            'Task#08092015 query altered by ahmad shairf, add verified column
            'If getConfigValueByType("EnableDuplicateQuotation").ToString.ToUpper = "TRUE" Then
            '    Call CreateDuplicationQuotation(Val(txtReceivingID.Text), "Update", trans) 'TASKM2710151
            'End If
            objCommand.CommandText = String.Empty
            'objCommand.CommandText = "Update QuotationMasterTable set QuotationNo =N'" & txtPONo.Text & "',QuotationDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            ' & " SalesOrderQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", SalesOrderAmount=" & NetAmount & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',VerifiedBy=N'" & LoginUserName & "', SpecialAdjustment=" & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", Posted=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", PONo=N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', NewCustomer=N'" & Me.txtNewCustomer.Text.Replace("'", "''") & "', Delivery_Date=" & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Adj_Flag=" & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", Adjustment=" & IIf(Me.rbtAdjFlat.Checked = False, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", CostCenterId=" & Me.cmbProject.SelectedValue & ", PO_Date=" & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", EditionalTax_Percentage=" & TransitPercent & ", SED_Percentage=" & WHTaxPercent & ", Terms_And_Condition=N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "', Apprved=" & IIf(Me.ChkApproved.Checked = True, 1, 0) & ",CustomerPhone=N'" & Me.txtCustomerMobile.Text.Replace("'", "''").ToString & "',CustomerAddress=N'" & Me.txtCustomerAddress.Text.Replace("'", "''").ToString & "' Where QuotationId= " & txtReceivingID.Text & " "
            'TFS# 956: Update Terms and Conditions and their details in QuotationTermsDetails table by Ali Faisal on 19-June-2017
            ' objCommand.CommandText = "Update QuotationMasterTable set QuotationNo =N'" & txtPONo.Text & "',QuotationDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '& " SalesOrderQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", SalesOrderAmount=" & NetAmount & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',VerifiedBy=N'" & LoginUserName & "', SpecialAdjustment=" & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", Posted=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", PONo=N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', NewCustomer=N'" & Me.txtNewCustomer.Text.Replace("'", "''") & "', Delivery_Date=" & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Adj_Flag=" & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", Adjustment=" & IIf(Me.rbtAdjFlat.Checked = False, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", CostCenterId=" & Me.cmbProject.SelectedValue & ", PO_Date=" & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", EditionalTax_Percentage=" & TransitPercent & ", SED_Percentage=" & WHTaxPercent & ", Terms_And_Condition=N'" & ReplaceNewLine(Me.txtTerms_And_Condition.Text, False).Replace("'", "''") & "', Apprved=" & IIf(Me.ChkApproved.Checked = True, 1, 0) & ",CustomerPhone=N'" & Me.txtCustomerMobile.Text.Replace("'", "''").ToString & "',CustomerAddress=N'" & Me.txtCustomerAddress.Text.Replace("'", "''").ToString & "' " & IIf(Me.ChkApproved.Checked = True, ",Approved_User=N'" & LoginUserName.ToString.Replace("'", "''") & "'", ",Approved_User=Null") & ", RevisionNumber= IsNull(RevisionNumber, 0) + 1 Where QuotationId= " & txtReceivingID.Text & " "
            ' objCommand.ExecuteNonQuery()
            ''TFS1798 
            ''TFS1674
            objCommand.CommandText = "Update QuotationMasterTable set QuotationNo =N'" & txtPONo.Text & "',QuotationDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ",ManualSerialNo=N'" & txtManualSerialNo.Text & "',EmployeeId=" & Val(cmbSalesMan.SelectedValue.ToString) & ", " _
           & " SalesOrderQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", SalesOrderAmount=" & NetAmount & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',VerifiedBy=N'" & LoginUserName & "', SpecialAdjustment=" & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", Posted=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", PONo=N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', NewCustomer=N'" & Me.txtNewCustomer.Text.Replace("'", "''") & "', Delivery_Date=" & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Adj_Flag=" & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", Adjustment=" & IIf(Me.rbtAdjFlat.Checked = False, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", CostCenterId=" & Me.cmbProject.SelectedValue & ", PO_Date=" & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", EditionalTax_Percentage=" & TransitPercent & ", SED_Percentage=" & WHTaxPercent & ", Terms_And_Condition=N'" & ReplaceNewLine(Me.txtTerms_And_Condition.Text, False).Replace("'", "''") & "', Apprved=" & IIf(Me.ChkApproved.Checked = True, 1, 0) & ",CustomerPhone=N'" & Me.txtCustomerMobile.Text.Replace("'", "''").ToString & "',CustomerAddress=N'" & Me.txtCustomerAddress.Text.Replace("'", "''").ToString & "' " & IIf(Me.ChkApproved.Checked = True, ",Approved_User=N'" & LoginUserName.ToString.Replace("'", "''") & "'", ",Approved_User=Null") & ", RevisionNumber= IsNull(RevisionNumber, 0) + 1 ,TermsAndConditionsId = " & Me.cmbTermCondition.SelectedValue & ",Subject=N'" & Me.txtSubject.Text.Replace("'", "''").ToString & "' Where QuotationId= " & txtReceivingID.Text & " "
            objCommand.ExecuteNonQuery()
            If Me.grdTerms.RowCount > 0 Then
                'TFS# 934: First delete the detail record then save them by Ali Faisal on 15-June-2017
                objCommand.CommandText = "Delete from QuotationTermsDetails Where TermId= " & TermsAndConditionsId & " And QuotationId= " & txtReceivingID.Text & ""
                objCommand.ExecuteNonQuery()
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdTerms.GetDataRows
                    objCommand.CommandText = "Insert into QuotationTermsDetails (TermId,QuotationId,Heading,Details) Values (" & Me.cmbTermCondition.SelectedValue & "," & txtReceivingID.Text & ",N'" & row.Cells("Heading").Value.ToString & "',N'" & row.Cells("Details").Value.ToString & "')"
                    objCommand.ExecuteNonQuery()
                Next
            End If
            'TFS# 956: End
            'Marked Against Task#2015060001 Ali Ansari
            'If arrFile.Length > 0 Then
            '    SaveDocument(Val(txtReceivingID.Text), Me.Name, trans)
            'End If
            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                SaveDocument(Val(txtReceivingID.Text), Me.Name, trans)
            End If
            'Altered Against Task#2015060001 Ali Ansari
            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from QuotationDetailTable where QuotationId = " & txtReceivingID.Text
            objCommand.ExecuteNonQuery()

            'For i = 0 To grd.Rows.Count - 1
            '    If grd.Rows(i).Cells("Qty").Value <> 0 Then
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "Insert into QuotationDetailTable (QuotationId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice) values( " _
            '                                & " " & txtReceivingID.Text & " ," & Val(grd.Rows(i).Cells(7).Value) & ",N'" & (grd.Rows(i).Cells(2).Value) & "'," & Val(grd.Rows(i).Cells(3).Value) & ", " _
            '                                & " " & IIf(grd.Rows(i).Cells(2).Value = "Loose", Val(grd.Rows(i).Cells(3).Value), (Val(grd.Rows(i).Cells(3).Value) * Val(grd.Rows(i).Cells(8).Value))) & ", " & Val(grd.Rows(i).Cells(4).Value) & ", " & Val(grd.Rows(i).Cells(8).Value) & "  , " & Val(grd.Rows(i).Cells(9).Value) & ") "

            '        objCommand.ExecuteNonQuery()
            '        'Val(grd.Rows(i).Cells(5).Value)
            '    End If
            'Next

            For i = 0 To grd.RowCount - 1
                ' If Val(grd.GetRows(i).Cells("Qty").Value) <> 0 Then

                ''R-916 Added Column Comments
                objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into QuotationDetailTable (QuotationId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, TradePrice, SalesTax_Percentage, SchemeQty, Discount_Percentage, Freight, MarketReturns, PurchasePrice, PackPrice,Pack_Desc) values( " _
                '                        & " N'" & Val(Me.txtReceivingID.Text) & "', " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Freight).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.MarketReturns).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                'objCommand.ExecuteNonQuery()
                'objCommand.CommandText = "Insert into QuotationDetailTable (QuotationId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,SalesTax_Percentage, SchemeQty, Discount_Percentage,PurchasePrice, PackPrice,Pack_Desc, Comments) values( " _
                '                       & " N'" & Val(Me.txtReceivingID.Text) & "', " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                '                       & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "') "

                'objCommand.CommandText = "Insert into QuotationDetailTable (QuotationId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,SalesTax_Percentage, SchemeQty, Discount_Percentage,PurchasePrice, PackPrice,Pack_Desc, Comments,ItemDescription,BrandName,Specification,RegistrationNo,TradePrice,TenderSrNo) values( " _
                '                                       & " N'" & Val(Me.txtReceivingID.Text) & "', " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                '                                       & " " & Val(Me.grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("ItemDescription").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Brand").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Specification").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("ItemRegistrationNo").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("TradePrice").Value.ToString.Replace("'", "''")) & ",N'" & Me.grd.GetRow(i).Cells("TenderSrNo").Value.ToString.Replace("'", "''") & "' ) "

                objCommand.CommandText = "Insert into QuotationDetailTable (QuotationId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,SalesTax_Percentage, SchemeQty, Discount_Percentage,PurchasePrice, PackPrice,Pack_Desc, Comments,ItemDescription,BrandName,Specification,RegistrationNo,TradePrice,TenderSrNo,CostPrice,SED_Tax_Percent,SED_Tax_Amount,SOQuantity, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, RequirementDescription, PurchaseInquiryDetailId, VendorQuotationDetailId, HeadArticleId, SerialNo, PurchaseInquiryId, Alternate, DiscountId, DiscountValue, DiscountFactor , PostDiscountPrice, IBrand, PartNo, Warranty, LeadTime, State) values( " _
                                                      & " N'" & Val(Me.txtReceivingID.Text) & "', " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                                                      & " " & Val(grd.GetRows(i).Cells(GrdEnum.TotalQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & "," & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("ItemDescription").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Brand").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Specification").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("ItemRegistrationNo").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("TradePrice").Value.ToString.Replace("'", "''")) & ",N'" & Me.grd.GetRow(i).Cells("TenderSrNo").Value.ToString.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString) & " ," & Val(Me.grd.GetRows(i).Cells("SED_Tax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("SED_Tax_Amount").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("SOQuantity").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", '" & Me.grd.GetRows(i).Cells("RequirementDescription").Value.ToString.Replace("'", "''") & "', " & IIf(Val(Me.grd.GetRows(i).Cells("PurchaseInquiryDetailId").Value.ToString) = 0, "NULL", Val(Me.grd.GetRows(i).Cells("PurchaseInquiryDetailId").Value.ToString)) & ", " & Val(Me.grd.GetRows(i).Cells("VendorQuotationDetailId").Value.ToString) & ", " & IIf(Val(Me.grd.GetRows(i).Cells("HeadArticleId").Value.ToString) = 0, "NULL", Val(Me.grd.GetRows(i).Cells("HeadArticleId").Value.ToString)) & ", N'" & Me.grd.GetRows(i).Cells("SerialNo1").Value.ToString.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells("PurchaseInquiryId").Value.ToString) & ", " & IIf(Me.grd.GetRow(i).Cells(GrdEnum.Alternate).Value.ToString = "True", 1, 0) & "," & Val(grd.GetRows(i).Cells("DiscountId").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("DiscountValue").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("DiscountFactor").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PostDiscountPrice").Value) & ", '" & Me.grd.GetRows(i).Cells("IBrand").Value.ToString & "', '" & Me.grd.GetRows(i).Cells("PartNo").Value.ToString & "', '" & Me.grd.GetRows(i).Cells("Warranty").Value.ToString & "', '" & Me.grd.GetRows(i).Cells("LeadTime").Value.ToString & "', '" & Me.grd.GetRows(i).Cells("State").Value.ToString & "') Select @@Identity"
                'objCommand.ExecuteNonQuery()
                Dim QuotationDetailId As Integer = objCommand.ExecuteScalar()
                '' Set new QuotationDetailId against old one in SalesOrderDetailTable by Ameen
                If Val(Me.grd.GetRows(i).Cells(GrdEnum.SOQuantity).Value.ToString) > 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = " Update SalesOrderDetailTable Set QuotationDetailId =" & QuotationDetailId & " Where QuotationDetailId = " & Val(Me.grd.GetRows(i).Cells(GrdEnum.QuotationDetailId).Value.ToString) & ""
                    objCommand.ExecuteNonQuery()
                End If


                'Val(grd.Rows(i).Cells(5).Value)
                ' End If
            Next
            '' Start TASK-480 on 12-05-2016
            If getConfigValueByType("EnableDuplicateQuotation").ToString.ToUpper = "TRUE" Then
                Call CreateDuplicationQuotation(Val(txtReceivingID.Text), "Update", trans)
            End If
            ''END TASK-480
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
        ''Start TFS2989
        If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim, Me.Name) Then
            If ValidateApprovalProcessIsInProgressAgain(Me.txtPONo.Text.Trim, Me.Name) = False Then
                SaveApprovalLog(EnumReferenceType.SalesQuotation, Val(txtReceivingID.Text), Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Sales Quotation ," & cmbVendor.Text & "", Me.Name)
            End If
        End If
        ''End TFS2989
        ''End TFS3113

    End Function
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.ButtonClick
        If Me.BtnSave.Enabled = False Then Exit Sub
        Try
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
                ShowErrorMessage("Your can not change this becuase financial year is closed")
                Me.dtpPODate.Focus()
                Exit Sub
            End If

            If FormValidate() Then
                Me.grd.UpdateData()
                If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub

                    If Save() Then
                        'EmailSave()
                        'msg_Information(str_informSave)
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


                        '' Add by Mohsin on 18 Sep 2017

                        ' NOTIFICATION STARTS HERE FOR SAVE

                        ' *** New Segment *** 
                        '// Adding notification

                        '// Creating new object of Notification configuration dal
                        '// Dal will be used to get users list for the notification 
                        Dim NDal1 As New NotificationConfigurationDAL
                        Dim objmod1 As New VouchersMaster
                        '// Creating new object of Agrius Notification class
                        objmod1.Notification = New AgriusNotifications

                        '// Reference document number
                        objmod1.Notification.ApplicationReference = Me.txtPONo.Text

                        '// Date of notification
                        objmod1.Notification.NotificationDate = Now

                        '// Preparing notification title string
                        objmod1.Notification.NotificationTitle = "Quotation number [" & Me.txtPONo.Text & "]  is created."

                        '// Preparing notification description string
                        objmod1.Notification.NotificationDescription = "Quotation number [" & Me.txtPONo.Text & "] is created by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                        '// Setting source application as refrence in the notification
                        objmod1.Notification.SourceApplication = "Quotation"



                        '// Starting to get users list to add child

                        '// Creating notification detail object list
                        Dim List1 As New List(Of NotificationDetail)

                        '// Getting users list
                        List1 = NDal1.GetNotificationUsers("Quotation Created")

                        '// Adding users list in the Notification object of current inquiry
                        objmod1.Notification.NotificationDetils.AddRange(List1)

                        '// Getting and adding user groups list in the Notification object of current inquiry
                        objmod1.Notification.NotificationDetils.AddRange(NDal1.GetNotificationGroups("Quotation Created"))

                        '// Not getting role list because no role is associated at the moment
                        '// We will need this in future and we can use it later
                        '// We can consult to Update function of this class


                        '// ***This segment will be used to save notification in database table

                        '// Creating new list of objects of Agrius Notification
                        Dim NList1 As New List(Of AgriusNotifications)

                        '// Copying notification object from current sales inquiry to newly defined instance
                        '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
                        NList1.Add(objmod1.Notification)

                        '// Creating object of Notification DAL
                        Dim GNotification1 As New NotificationDAL

                        '// Saving notification to database
                        GNotification1.AddNotification(NList1)

                        '// End Adding Notification

                        '// End Adding Notification
                        ' *** End Segment ***
                    Else
                        Exit Sub 'MsgBox("Record has not been added")
                    End If
                Else
                    'If IsValidToDelete("SalesOrderMasterTable", "QuotationId", Me.grdSaved.CurrentRow.Cells("QuotationId").Value.ToString) = True Then
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
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

                        '' Add by Mohsin on 18 Sep 2017

                        ' NOTIFICATION STARTS HERE FOR UPDATE

                        ' *** New Segment *** 
                        '// Adding notification

                        '// Creating new object of Notification configuration dal
                        '// Dal will be used to get users list for the notification 
                        Dim NDal1 As New NotificationConfigurationDAL
                        Dim objmod1 As New VouchersMaster
                        '// Creating new object of Agrius Notification class
                        objmod1.Notification = New AgriusNotifications

                        '// Reference document number
                        objmod1.Notification.ApplicationReference = Me.txtPONo.Text

                        '// Date of notification
                        objmod1.Notification.NotificationDate = Now

                        '// Preparing notification title string
                        objmod1.Notification.NotificationTitle = "Quotation number [" & Me.txtPONo.Text & "]  is changed."

                        '// Preparing notification description string
                        objmod1.Notification.NotificationDescription = "Quotation number [" & Me.txtPONo.Text & "] is changed by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                        '// Setting source application as refrence in the notification
                        objmod1.Notification.SourceApplication = "Quotation"



                        '// Starting to get users list to add child

                        '// Creating notification detail object list
                        Dim List1 As New List(Of NotificationDetail)

                        '// Getting users list
                        List1 = NDal1.GetNotificationUsers("Quotation Changed")

                        '// Adding users list in the Notification object of current inquiry
                        objmod1.Notification.NotificationDetils.AddRange(List1)

                        '// Getting and adding user groups list in the Notification object of current inquiry
                        objmod1.Notification.NotificationDetils.AddRange(NDal1.GetNotificationGroups("Quotation Changed"))

                        '// Not getting role list because no role is associated at the moment
                        '// We will need this in future and we can use it later
                        '// We can consult to Update function of this class


                        '// ***This segment will be used to save notification in database table

                        '// Creating new list of objects of Agrius Notification
                        Dim NList1 As New List(Of AgriusNotifications)

                        '// Copying notification object from current sales inquiry to newly defined instance
                        '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
                        NList1.Add(objmod1.Notification)

                        '// Creating object of Notification DAL
                        Dim GNotification1 As New NotificationDAL

                        '// Saving notification to database
                        GNotification1.AddNotification(NList1)

                        '// End Adding Notification

                        '// End Adding Notification
                        ' *** End Segment ***

                    Else
                        Exit Sub 'MsgBox("Record has not been updated")
                    End If
                    'Else
                    '    msg_Error(str_ErrorDependentUpdateRecordFound)
                    'End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
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
                frmItemSearch.formName = ""
                frmItemSearch.BringToFront()
                frmItemSearch.ShowDialog()
                If frmItemSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbItem.Value = frmItemSearch.ArticleId
                    txtQty.Text = frmItemSearch.Qty
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
        'ClearDetailControls() ''Commented To Retain the values of Pack Qty
        Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
        ''Commented Agaisnt TFS4159
        'Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
        'Me.txtPDP.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString ''TFS2827
        ''End TFS4159
        If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
        QoutationItemID = Me.cmbItem.ActiveRow.Cells(0).Value
        'Me.cmbVendor.DisplayLayout.Grid.Show( me.cmbVendor.contr)
        Me.txtDisc.TabStop = False
        Try
            If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then

                Dim strSQl As String = "select discount from tbldefcustomerbasediscounts where articledefid = " & Me.cmbItem.ActiveRow.Cells(0).Value _
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
                    End If
                End If
            End If
            If Me.cmbItem.Value > 0 Then
                Me.txtArticleAlias.Enabled = False
            Else
                Me.txtArticleAlias.Enabled = True
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Enter
        Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub

    Private Sub cmbVendor_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.Enter
        Me.cmbVendor.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
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
        ''Start TFS2988
        If IsEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document is in Approval Process") : Exit Sub
                End If
            End If
        End If
        ''End TFS2988
        If IsValidToDelete("SalesOrderMasterTable", "QuotationId", Me.grdSaved.CurrentRow.Cells("QuotationId").Value.ToString) = True Then

            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

            Try
                Dim cm As New OleDbCommand
                Dim objTrans As OleDbTransaction
                If Con.State = ConnectionState.Closed Then Con.Open()
                objTrans = Con.BeginTransaction
                cm.Connection = Con
                '' Start TASK-480 on 12-05-2016
                If getConfigValueByType("EnableDuplicateQuotation").ToString.ToUpper = "TRUE" Then
                    Call CreateDuplicationQuotation(Val(txtReceivingID.Text), "Delete", objTrans)
                End If
                ''END TASK-480
                'Before against task:2372 
                'cm.CommandText = "delete from QuotationDetailTable where SalesOrderid=" & Me.grdSaved.CurrentRow.Cells(5).Value.ToString
                'Task:2372 Change FieldId 
                cm.CommandText = "delete from QuotationDetailTable where QuotationId=" & Me.grdSaved.CurrentRow.Cells("QuotationId").Value.ToString
                'End Task:2372
                cm.Transaction = objTrans
                cm.ExecuteNonQuery()

                'cm = New OleDbCommand
                'cm.Connection = Con

                'cm.CommandText = "delete from QuotationMasterTable where QuotationId=" & Me.grdSaved.CurrentRow.Cells(5).Value.ToString

                'TFS# 956: Delete Terms and Conditions and their details from QuotationTermsDetails table by Ali Faisal on 19-June-2017
                cm = New OleDbCommand
                cm.Connection = Con
                cm.CommandText = "Delete QuotationTermsDetails Where TermId= " & TermsAndConditionsId & " And QuotationId= " & txtReceivingID.Text & ""
                cm.Transaction = objTrans
                cm.ExecuteNonQuery()
                'TFS# 956: End

                'cm.Transaction = objTrans
                'cm.ExecuteNonQuery()

                cm = New OleDbCommand
                cm.Connection = Con

                cm.CommandText = "delete from QuotationMasterTable where QuotationId=" & Me.grdSaved.CurrentRow.Cells("QuotationId").Value.ToString
                'cm.CommandText = "delete from QuotationMasterTable where QuotationId=" & Me.grdSaved.CurrentRow.Cells(5).Value.ToString
                cm.Transaction = objTrans
                cm.ExecuteNonQuery()

                '' Delete from documentattachment
                cm = New OleDbCommand
                cm.Connection = Con
                cm.CommandText = "delete from DocumentAttachment where DocId=" & Val(Me.grdSaved.CurrentRow.Cells(5).Value.ToString) & " And Source =N'" & Me.Name & "'"
                cm.Transaction = objTrans
                cm.ExecuteNonQuery()
                objTrans.Commit()

                Me.txtReceivingID.Text = 0
                '' Add by Mohsin on 18 Sep 2017

                ' NOTIFICATION STARTS HERE FOR DELETE'

                ' *** New Segment *** 
                '// Adding notification

                '// Creating new object of Notification configuration dal
                '// Dal will be used to get users list for the notification 
                Dim NDal1 As New NotificationConfigurationDAL
                Dim objmod1 As New VouchersMaster
                '// Creating new object of Agrius Notification class
                objmod1.Notification = New AgriusNotifications

                '// Reference document number
                objmod1.Notification.ApplicationReference = Me.txtPONo.Text

                '// Date of notification
                objmod1.Notification.NotificationDate = Now

                '// Preparing notification title string
                objmod1.Notification.NotificationTitle = "Quotation number [" & Me.txtPONo.Text & "]  is deleted."

                '// Preparing notification description string
                objmod1.Notification.NotificationDescription = "Quotation number [" & Me.txtPONo.Text & "] is deleted by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                '// Setting source application as refrence in the notification
                objmod1.Notification.SourceApplication = "Quotation"



                '// Starting to get users list to add child

                '// Creating notification detail object list
                Dim List1 As New List(Of NotificationDetail)

                '// Getting users list
                List1 = NDal1.GetNotificationUsers("Quotation Deleted")

                '// Adding users list in the Notification object of current inquiry
                objmod1.Notification.NotificationDetils.AddRange(List1)

                '// Getting and adding user groups list in the Notification object of current inquiry
                objmod1.Notification.NotificationDetils.AddRange(NDal1.GetNotificationGroups("Quotation Deleted"))

                '// Not getting role list because no role is associated at the moment
                '// We will need this in future and we can use it later
                '// We can consult to Update function of this class


                '// ***This segment will be used to save notification in database table

                '// Creating new list of objects of Agrius Notification
                Dim NList1 As New List(Of AgriusNotifications)

                '// Copying notification object from current sales inquiry to newly defined instance
                '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
                NList1.Add(objmod1.Notification)

                '// Creating object of Notification DAL
                Dim GNotification1 As New NotificationDAL

                '// Saving notification to database
                GNotification1.AddNotification(NList1)

                '// End Adding Notification

                '// End Adding Notification
                ' *** End Segment ***
            Catch ex As Exception
                msg_Error("Error occured while deleting record: " & ex.Message)

            Finally
                Con.Close()
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
    '    ShowReport("SalesOrder", "{QuotationMasterTable.QuotationId}=" & grdSaved.CurrentRow.Cells("QuotationId").Value)

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
                Me.btn_History.Enabled = True
                If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then Me.chkPost.Checked = True
                'Task:1592 Added Future Date Rights
                IsDateChangeAllowed = True
                dtpPODate.MaxDate = Date.Today.AddMonths(3)
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
                Me.ChkApproved.Visible = False
                Me.btn_History.Enabled = False
                CtrlGrdBar3.mGridExport.Enabled = False
                CtrlGrdBar4.mGridExport.Enabled = False ''TFS1823
                'Task:1592 Added Future Date Rights
                IsDateChangeAllowed = False
                DateChange(False)
                CtrlGrdBar3.mGridChooseFielder.Enabled = False ' Task:2406 Added Field Chooser Rights
                CtrlGrdBar4.mGridChooseFielder.Enabled = False
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
                    ElseIf RightsDt.FormControlName = "Item History" Then
                        Me.btn_History.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'R:M6 Added Security Rights
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True
                        If (BtnSave.Text = "&Save" Or BtnSave.Text = "Save") Then Me.chkPost.Checked = True
                        'End R:M6
                        'Task:1592 Added Future Date Rights
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar3.mGridExport.Enabled = True
                        CtrlGrdBar4.mGridExport.Enabled = True ''TFS1823
                    ElseIf RightsDt.FormControlName = "Future Transaction" Then
                        IsDateChangeAllowed = True
                        DateChange(True)
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Approved" Then
                        Me.ChkApproved.Visible = True
                        If (BtnSave.Text = "&Save" Or BtnSave.Text = "Save") Then Me.ChkApproved.Checked = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar3.mGridChooseFielder.Enabled = True
                        CtrlGrdBar4.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    'Task:1592 Added Future Date Rights
    Private Sub DateChange(ByRef IsDateChangeAllowed As Boolean)
        Try
            If IsDateChangeAllowed = False Then
                dtpPODate.MaxDate = DateTime.Now.ToString("yyyy/M/d 23:59:59")
            Else
                dtpPODate.MaxDate = Date.Today.AddMonths(3)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'END TASk:1592
    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        ''Task#A1 12-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
        Try
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
        ''End Task#A1 12-06-2015
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
        'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        Try

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
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
            id = Me.cmbCurrency.SelectedValue
            FillCombo("Currency")
            Me.cmbCurrency.SelectedValue = id

            id = Me.cmbTermCondition.SelectedIndex
            FillCombo("TermsCondition")
            Me.cmbTermCondition.SelectedIndex = id

            FillCombo("ItemDescriptions")

            FillCombo("grdLocation")
            ''TFS1798 
            id = Me.cmbSalesMan.SelectedIndex
            FillCombo("SM")
            Me.cmbSalesMan.SelectedIndex = id
            ''EndTFS1798
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

            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItem = getConfigValueByType("ArticleFilterByLocation")
            End If
            ''start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161
            'Altered Against Task#2015060001 Ali Ansri
            'Clear Attached file records
            '        arrFile = New List(Of String)
            '       Me.btnAttachment.Text = "Attachment (" & arrFile.Count & ")"
            'Altered Against Task#2015060001 Ali Ansri
            Me.lblDeliveryDate.Visible = False
            Me.lblProgress.Visible = False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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

            'Dim price As Double = 0D
            'Double.TryParse(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString, price)
            'If Val(Me.txtPackRate.Text) = 0 Then
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
                If col.Index <> GrdEnum.LocationId AndAlso col.Index <> GrdEnum.Qty AndAlso col.Index <> GrdEnum.TotalQty AndAlso col.Index <> GrdEnum.Rate AndAlso col.Index <> GrdEnum.PostDiscountPrice AndAlso col.Index <> GrdEnum.SalesTax_Percentage AndAlso col.Index <> GrdEnum.SchemeQty AndAlso col.Index <> GrdEnum.Discount_Percentage AndAlso col.Index <> GrdEnum.Comments AndAlso col.Index <> GrdEnum.ItemDescription AndAlso col.Index <> GrdEnum.Brand AndAlso col.Index <> GrdEnum.TradePrice AndAlso col.Index <> GrdEnum.TenderSrNo AndAlso col.Index <> GrdEnum.Specification AndAlso col.Index <> GrdEnum.ItemRegistrationNo AndAlso col.Index <> GrdEnum.SED_Tax_Percent AndAlso col.Index <> GrdEnum.SerialNo AndAlso col.Index <> GrdEnum.RequirementDescription AndAlso col.Index <> GrdEnum.DiscountId AndAlso col.Index <> GrdEnum.DiscountFactor AndAlso col.Index <> GrdEnum.IBrand AndAlso col.Index <> GrdEnum.PartNo AndAlso col.Index <> GrdEnum.Warranty AndAlso col.Index <> GrdEnum.LeadTime AndAlso col.Index <> GrdEnum.State Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            ''Start TFS4161
            If IsPackQtyDisabled = True Then
                Me.grd.RootTable.Columns(GrdEnum.TotalQty).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Else
                Me.grd.RootTable.Columns(GrdEnum.TotalQty).EditType = Janus.Windows.GridEX.EditType.TextBox
            End If
            ''End TFS4161
            Me.grd.RootTable.Columns("Pack_Desc").Visible = False
            Me.grd.RootTable.Columns("Unit").Visible = True

            'Task:2759 Set rounded amount format
            Me.grd.RootTable.Columns(GrdEnum.Total).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.Total).TotalFormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.Total).FormatString = "N" & DecimalPointInValue
            'Me.grd.RootTable.Columns(GrdEnum.BillValueAfterDiscount).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(GrdEnum.Total).FormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns(GrdEnum.Total).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            'Me.grd.RootTable.Columns(GrdEnum.BillValueAfterDiscount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            Me.grd.RootTable.Columns(GrdEnum.Total).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            'Dim DT As DataTable = Me.grd.DataSource
            'End Task:2759


            Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).TotalFormatString = "N"

            Me.grd.RootTable.Columns(GrdEnum.Net_Amount).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.Net_Amount).TotalFormatString = "N"

            Me.grd.RootTable.Columns(GrdEnum.Rate).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.Rate).TotalFormatString = "N"

            Me.grd.RootTable.Columns("PostDiscountPrice").FormatString = "N"

            Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).TotalFormatString = "N"
            'grd.AutoSizeColumns()
            'Dim bln As Boolean = Convert.ToBoolean(getConfigValueByType("GridFreezColumn").Replace("Error", "False").Replace("''", "False"))
            'If bln = True Then
            '    Me.grd.FrozenColumns = Me.grd.RootTable.Columns("Size").Index
            'Else
            '    Me.grd.FrozenColumns = 0
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.grd.GetRow.Delete()
                grd.UpdateData()
                GetTotal()
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
            'If IsSalesOrderAnalysis = True Then
            '    Dim dt As DataTable = GetCostManagement(Me.cmbItem.Value)
            '    If dt IsNot Nothing Then
            '        TradePrice = dt.Rows(0).Item("TradePrice")
            '        Freight_Rate = dt.Rows(0).Item("Freight")
            '        MarketReturns_Rate = dt.Rows(0).Item("MarketReturns")
            '        GST_Applicable = dt.Rows(0).Item("Gst_Applicable")
            '        FlatRate_Applicable = dt.Rows(0).Item("FlatRate_Applicable")
            '        FlatRate = dt.Rows(0).Item("FlatRate")
            '    End If
            '    Dim dtDiscount As DataTable = GetAnalysisLastDiscount(Me.cmbVendor.Value, Me.cmbItem.Value)
            '    If dtDiscount IsNot Nothing Then
            '        If dtDiscount.Rows.Count > 0 Then
            '            Me.txtDisc.Text = dtDiscount.Rows(0).Item(0)
            '        Else
            '            Me.txtDisc.Text = 0
            '        End If
            '    Else
            '        Me.txtDisc.Text = 0
            '    End If
            '    'Dim dtSchemeQty As DataTable = GetAnalysisLastSchemeQty(Me.cmbVendor.Value, Me.cmbItem.Value)
            '    'If dtSchemeQty IsNot Nothing Then
            '    '    If dtSchemeQty.Rows.Count > 0 Then
            '    '        Me.txtSchemeQty.Text = dtSchemeQty.Rows(0).Item(0)
            '    '    Else
            '    '        Me.txtSchemeQty.Text = 0
            '    '    End If
            '    'Else
            '    '    Me.txtSchemeQty.Text = 0
            '    'End If
            'Else
            Me.txtDisc.Text = GetLastDiscount(IIf(Me.cmbVendor.IsItemInList = True, Me.cmbVendor.Value, 0), Me.cmbItem.Value)
            'End If
            FillCombo("ArticlePack")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCode.CheckedChanged
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
    Private Sub CtrlGrdBar3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Quotation"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar4.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar4.txtGridTitle.Text = CompanyTitle & Chr(10) & "Quotation"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            Dim DocPrefix As String = GetPrefix(Me.Name)
            If DocPrefix.Length > 0 Then
                If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                    Return GetSerialNo(DocPrefix + "" & Me.cmbCompany.SelectedValue & "" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "QuotationMasterTable", "QuotationNo")
                ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                    Return GetNextDocNo(DocPrefix & Me.cmbCompany.SelectedValue & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "QuotationMasterTable", "QuotationNo")
                Else
                    Return GetNextDocNo(DocPrefix + "" & Me.cmbCompany.SelectedValue & "", 6, "QuotationMasterTable", "QuotationNo")
                End If
            Else
                If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                    Return GetSerialNo("QUT" + "" & Me.cmbCompany.SelectedValue & "" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "QuotationMasterTable", "QuotationNo")
                ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                    Return GetNextDocNo("QUT" & Me.cmbCompany.SelectedValue & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "QuotationMasterTable", "QuotationNo")
                Else
                    Return GetNextDocNo("QUT" + "" & Me.cmbCompany.SelectedValue & "", 6, "QuotationMasterTable", "QuotationNo")
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnAddNewItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewItem.Click
        Try
            Call frmAddItem.ShowDialog()
            Call FillCombo("Item")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub cmbVendor_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.ValueChanged
        Try
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            'If getConfigValueByType("LoadAllItemsInSales").ToString = "True" Then
            '    Dim Str As String = "Select CustomerTypes From tblCustomer WHERE AccountId=" & Me.cmbVendor.Value
            '    Dim dt1 As DataTable = GetDataTable(Str)
            '    If Not dt1 Is Nothing Then
            '        If dt1.Rows.Count > 0 Then
            '            Me.DisplayDetail(-1, dt1.Rows(0).Item(0), "All")
            '        Else
            '            Me.DisplayDetail(-1, -1, "All")
            '        End If
            '    End If
            '    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            '        If Me.grd.RowCount > 0 Then
            '            r.BeginEdit()
            '            r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
            '            r.EndEdit()
            '        End If
            '    Next
            'Else
            '    'Me.DisplayDetail(-1)
            'End If
            ''TFS1798 
            If Not IsEditMode = True Then
                If Val(Me.cmbVendor.ActiveRow.Cells("SaleMan").Value.ToString) > 0 Then
                    Me.cmbSalesMan.SelectedValue = Val(Me.cmbVendor.ActiveRow.Cells("SaleMan").Value.ToString)
                    If Me.cmbSalesMan.SelectedValue Is Nothing Then
                        If Me.cmbSalesMan.SelectedIndex = -1 Then Me.cmbSalesMan.SelectedIndex = 0
                    End If
                Else
                    If Not Me.cmbSalesMan.SelectedValue Is Nothing Then
                        Me.cmbSalesMan.SelectedIndex = 0
                    End If
                End If
            End If

            'CtrlGrdBar1.Email = New SBModel.SendingEmail
            'CtrlGrdBar1.Email.ToEmail = Me.cmbVendor.ActiveRow.Cells("Email").Text
            'CtrlGrdBar1.Email.Subject = "Sales Order:" + "(" & Me.txtPONo.Text & ")"
            'CtrlGrdBar1.Email.Body = String.Empty
            'CtrlGrdBar1.Email.DocumentNo = Me.txtPONo.Text
            'CtrlGrdBar1.Email.DocumentDate = Me.dtpPODate.Value

            Dim dt As DataTable = GetDataTable("Select ISNULL(SpecialAdjustment,0) as SpecialAdjustment From QuotationMasterTable WHERE QuotationId in (Select Max(QuotationId) From QuotationMasterTable WHERE VendorId=" & Me.cmbVendor.Value & " And SpecialAdjustment <> 0)")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Me.txtSpecialAdjustment.Text = dt.Rows(0).Item(0)
                Else
                    Me.txtSpecialAdjustment.Text = 0
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbCompany_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCompany.SelectedIndexChanged
        Try
            If Not IsEditMode = True Then Me.RefreshControls()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub OrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("QuotationNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            ShowReport("rptQuotation", "{QuotationMasterTable.QuotationId}=" & grdSaved.CurrentRow.Cells("QuotationId").Value)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub QuotationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPrint.ButtonClick, BtnPrint.ButtonClick
        Try
            Dim IsPreviewSaleInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
            Dim newinvoice As Boolean = False
            Dim strCriteria As String = "Nothing"
            Me.Cursor = Cursors.WaitCursor
            Try
                If Me.grdSaved.RowCount = 0 Then Exit Sub
                newinvoice = getConfigValueByType("NewInvoice")
                If newinvoice = True Then
                    str_ReportParam = "@QuotationId|" & grdSaved.CurrentRow.Cells("QuotationId").Value
                Else
                    str_ReportParam = String.Empty
                    strCriteria = "{QuotationDetailTable.QuotationId} = " & grdSaved.CurrentRow.Cells("QuotationId").Value
                End If

                If IsPreviewSaleInvoice = False Then
                    ShowReport(IIf(newinvoice = False, "rptQuotation", "rptQuotationNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", True, , "New")
                Else
                    ShowReport(IIf(newinvoice = False, "rptQuotation", "rptQuotationNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", False, , "New")
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
    Public Function Get_All(ByVal QuotationNo As String)
        Try
            Get_All = Nothing
            If IsFormOpen = True Then
                If QuotationNo.Length > 0 Then
                    Dim str As String = "Select * From QuotationMasterTable WHERE QuotationNo=N'" & QuotationNo & "'"
                    Dim dt As DataTable = GetDataTable(str)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then



                            Me.txtReceivingID.Text = dt.Rows(0).Item("QuotationId")
                            Me.txtPONo.Text = dt.Rows(0).Item("QuotationNo")
                            Me.dtpPODate.Value = dt.Rows(0).Item("QuotationDate")
                            Me.cmbCompany.SelectedValue = dt.Rows(0).Item("LocationId")
                            Me.cmbVendor.Value = dt.Rows(0).Item("VendorId")
                            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks")
                            Me.txtPaid.Text = dt.Rows(0).Item("CashPaid")
                            Me.txtNewCustomer.Text = dt.Rows(0).Item("NewCustomer")
                            If Convert.ToBoolean(dt.Rows(0).Item("Adj_Flag").ToString) = False Then
                                Me.txtSpecialAdjustment.Text = Val(dt.Rows(0).Item("Adjustment").ToString)
                                Me.rbtAdjFlat.Checked = True
                            Else
                                Me.txtSpecialAdjustment.Text = Val(dt.Rows(0).Item("SpecialAdjustment").ToString)
                                Me.rbtAdjPercentage.Checked = True
                            End If
                            If IsDBNull(dt.Rows(0).Item("PO_Date")) Then
                                Me.dtpPDate.Value = Now
                                Me.dtpPDate.Checked = False
                            Else
                                Me.dtpPDate.Value = dt.Rows(0).Item("PO_Date")
                                Me.dtpPDate.Checked = True
                            End If
                            If IsDBNull(dt.Rows(0).Item("CostCenterId")) Then
                                Me.cmbProject.SelectedIndex = 0
                            Else
                                Me.cmbProject.SelectedValue = dt.Rows(0).Item("CostCenterId")
                            End If
                            DisplayDetail(dt.Rows(0).Item("QuotationId"))
                            GetTotal()
                            If IsDBNull(dt.Rows(0).Item("Delivery_Date")) Then
                                Me.dtpDeliveryDate.Value = Date.Now
                                Me.dtpDeliveryDate.Checked = False
                            Else
                                dtpDeliveryDate.Value = dt.Rows(0).Item("Delivery_Date")
                                Me.dtpDeliveryDate.Checked = True
                            End If
                            'If IsDBNull(dt.Rows(0).Item("SOP_ID")) Then
                            '    Me.cmbSalesMan.SelectedValue = 0
                            'Else
                            '    Me.cmbSalesMan.SelectedValue = dt.Rows(0).Item("SOP_ID")
                            'End If
                            Me.BtnSave.Text = "&Update"
                            Me.cmbPo.Enabled = False
                            GetSecurityRights()
                            ''Start TFS3113
                            Me.btnApprovalHistory.Visible = True
                            Me.btnApprovalHistory.Enabled = True
                            ''End TFS3113
                            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                            Me.cmbCompany.Enabled = False
                            IsDrillDown = True
                            Me.cmbVendor.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)

                            If flgDateLock = True Then
                                If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
                                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                                    Me.dtpPODate.Enabled = False
                                Else
                                    Me.dtpPODate.Enabled = True
                                End If
                            Else
                                If LoginGroup = "Administrator" Then
                                    Me.dtpPODate.Enabled = True
                                End If
                            End If


                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                End If
                IsDrillDown = False
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
    Private Sub OrderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            OrderToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub QuotationToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            ElseIf e.Tab.Index = 0 Then
                ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                If IsEditMode = False Then Me.BtnDelete.Visible = False
                If IsEditMode = False Then Me.BtnPrint.Visible = False
                Me.BtnEdit.Visible = False
                'Aashir:TFS3760 Displau tamplates on template tab
            ElseIf e.Tab.Index = 2 Then
                Me.DisplayQuotationTemplates()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetSalesOrderAnalysis() As Boolean
        Try

            'Me.grd.RootTable.Columns("TradePrice").Visible = False
            'Me.grd.RootTable.Columns("SalesTax_Percentage").Visible = False
            Me.grd.RootTable.Columns("SchemeQty").Visible = False
            'Me.grd.RootTable.Columns("Freight").Visible = False 'Comment against Task:2528 
            'Me.grd.RootTable.Columns("Discount_Percentage").Visible = False
            'Me.grd.RootTable.Columns("BillValueAfterDiscount").Visible = False 'Comment against Task:2528 
            'Me.grd.RootTable.Columns("MarketReturns").Visible = False 'Comment against Task:2528 
            Me.rbtAdjPercentage.Enabled = True
            Me.rbtAdjFlat.Enabled = True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAnalysisLastTradePrice(ByVal CustomerCode As Integer, ByVal ItemId As Integer, ByVal Unit As String) As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select ISNULL(Max(b.TradePrice),0) as TradePrice From QuotationDetailTable b INNER JOIN QuotationMasterTable a ON a.QuotationId = b.QuotationId WHERE a.VendorId=" & CustomerCode & " AND b.ArticleDefId=" & ItemId & " AND ArticleSize =N'" & Unit & "'"
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
            str = "Select b.SchemeQty From QuotationDetailTable b INNER JOIN QuotationMasterTable a ON a.QuotationId = b.QuotationId WHERE SalesOrderDetailId In (Select Max(SalesOrderDetailId) From QuotationDetailTable WHERE (SchemeQty Is Not Null Or SchemeQty <> 0) Group By ArticleDefId) And a.VendorId=" & CustomerCode & " AND b.ArticleDefId=" & ItemId & ""
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
            str = "Select b.Discount_Percentage From QuotationDetailTable b INNER JOIN QuotationMasterTable a ON a.QuotationId = b.QuotationId WHERE SalesOrderDetailId In (Select Max(SalesOrderDetailId) From QuotationDetailTable WHERE (Discount_Percentage Is Not Null Or Discount_Percentage <> 0) Group By ArticleDefId) And a.VendorId=" & CustomerCode & " AND b.ArticleDefId=" & ItemId & ""
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
    Private Sub SalesOrderAnalysisToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@QuotationId", Me.grdSaved.GetRow.Cells("QuotationId").Value)
            ShowReport("rptSalesOrderAnalysis")
        Catch ex As Exception
            ShowErrorMessage("Error occured while showing report: " & ex.Message)
        End Try
    End Sub

    Private Sub PurchaseOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        ShowReport("SalesOrder", "{QuotationMasterTable.QuotationId}=" & grdSaved.CurrentRow.Cells("QuotationId").Value)
    End Sub
    Private Sub PuToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@QuotationId", Me.grdSaved.GetRow.Cells("QuotationId").Value)
            ShowReport("rptPurchaseOrder")
        Catch ex As Exception
            ShowErrorMessage("Error occured while showing report: " & ex.Message)
        End Try
    End Sub

    Private Sub ProductionPlaningToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
                    crpt.RecordSelectionFormula = "{QuotationMasterTable.QuotationId}=" & VoucherId



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

    Private Sub SalesOrderAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@QuotationId", Me.grdSaved.GetRow.Cells("QuotationId").Value)
            ShowReport("rptSalesOrderAnalysis")
        Catch ex As Exception
            ShowErrorMessage("Error occured while showing report: " & ex.Message)
        End Try
    End Sub

    Private Sub PurchaseOrderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@QuotationId", Me.grdSaved.GetRow.Cells("QuotationId").Value)
            ShowReport("rptPurchaseOrder")
        Catch ex As Exception
            ShowErrorMessage("Error occured while showing report: " & ex.Message)
        End Try
    End Sub
    Private Sub DispatchAdviceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        ShowReport("SalesOrder", "{QuotationMasterTable.QuotationId}=" & grdSaved.CurrentRow.Cells("QuotationId").Value)
    End Sub

    Private Sub DispatchesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            frmMain.LoadControl("ProductionPlan")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grd_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            If grd.RootTable IsNot Nothing Then Me.grd.UpdateData()
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

    Private Sub txtTax_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTax.LostFocus
        Try
            ' Before ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            'Else
            '    txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            'End If

            ' After ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total

            'Commented out 17-05-2016
            'If Val(Me.txtPackQty.Text) = 0 Or Val(Me.txtPackQty.Text) = 1 Then
            '    Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + ((Val(Me.txtTotal.Text) * Val(Me.txtTax.Text)) / 100)
            'Else
            '    Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + ((Val(Me.txtPackQty.Text) * Val(Me.txtTotal.Text) * Val(Me.txtTax.Text)) / 100)
            'End If
            ''''''''''''''''''''''''''''''''''
            GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTax.TextChanged

    End Sub

    Private Sub cmbVendor_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbVendor.InitializeLayout

    End Sub

    Private Sub PrintAgreementLatterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@QuotationId", grdSaved.CurrentRow.Cells("QuotationId").Value)
            ShowReport("rptAgreementLatter")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintAgreementLatterToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
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

    Private Sub dtpPDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpPDate.ValueChanged

    End Sub

    Private Sub PrintSalesOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            AddRptParam("@QuotationId", Me.grdSaved.GetRow.Cells("QuotationId").Value)
            ShowReport("rptSalesOrder")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintSalesOrderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            PrintSalesOrderToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pendingCustomerList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

    Private Sub grd_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs)

    End Sub

    Private Sub txtPackRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Val(Me.txtPackQty.Text) = 0 Then
                txtPackQty.Text = 1
                DiscountCalculation()
                txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            Else
                DiscountCalculation()
                txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackRate.TextChanged
        Try
            If Me.txtPackRate.Text.Length > 0 AndAlso (Me.txtPackRate.Text) > 0 Then
                If Me.cmbUnit.Text <> "Loose" Then
                    Me.txtPDP.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
                    Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged
        Try

            If Me.txtPackRate.Text.Length > 0 AndAlso Val(Me.txtPackRate.Text) > 0 Then
                If Me.cmbUnit.Text <> "Loose" Then
                    Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
                Else
                    Me.txtRate.Text = Val(Me.txtRate.Text)
                End If
                DiscountCalculation() ''TFS2827
            End If
            If Val(Me.txtPackQty.Text) > 0 Then
                Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            Else
                Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
            End If
            'If Me.txtPackRate.Text.Length > 0 AndAlso (Me.txtPackRate.Text) > 0 Then
            '    If Me.cmbUnit.Text <> "Loose" Then
            '        Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
            '    End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged, txtWarranty.TextChanged, txtLeadTime.TextChanged, txtPartNo.TextChanged, txtIBrand.TextChanged
        Try
            If Val(Me.txtPackQty.Text) > 0 Then
                Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            Else
                Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
            End If
            'If Me.txtPackRate.Text.Length > 0 AndAlso (Me.txtPackRate.Text) > 0 Then
            '    If Me.cmbUnit.Text <> "Loose" Then
            '        Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
            '    End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            frmSOCashReceipt._CompanyId = Val(Me.grdSaved.GetRow.Cells("LocationId").Value.ToString)
            frmSOCashReceipt._CostCenterId = Val(Me.grdSaved.GetRow.Cells("CostCenterId").Value.ToString)
            frmSOCashReceipt._CustomerId = Val(Me.grdSaved.GetRow.Cells("VendorId").Value.ToString)
            frmSOCashReceipt._SaleOrderId = Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString)
            frmSOCashReceipt._SaleOrderNo = Me.grdSaved.GetRow.Cells("QuotationNo").Value.ToString
            frmSOCashReceipt._SaleOrderDate = Me.grdSaved.GetRow.Cells("Date").Value
            frmSOCashReceipt._CustomerName = Me.grdSaved.GetRow.Cells("CustomerName").Value.ToString
            frmSOCashReceipt._NetAmount = Val(Me.grdSaved.GetRow.Cells("SalesOrderAmount").Value.ToString)
            frmSOCashReceipt.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintSelectVouchersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Change on 23-11-2013  For Multiple Print Vouchers
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                AddRptParam("@QuotationId", r.Cells("QuotationId").Value)
                ShowReport("rptSalesOrder", , , , True)
                PrintLog = New SBModel.PrintLogBE
                PrintLog.DocumentNo = r.Cells("QuotationNo").Value.ToString
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
    Private Sub txtTotal_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotal.LostFocus, txtTotal.Leave
        Try
            'Dim db As Double = 0D
            'Dim tax As Double = 0D
            'If Val(Me.txtTotal.Text) > 0 And Val(Me.txtQty.Text) > 0 Then
            '    If Val(Me.txtPackQty.Text) = 0 Or Val(Me.txtPackQty.Text) = 1 Then
            '        db = Val(Me.txtTotal.Text) / Val(Me.txtQty.Text)
            '        Me.txtRate.Text = Math.Round(db, 2).ToString()
            '        If Val(Me.txtTax.Text) > 0 Then
            '            tax = (Val(Me.txtTotal.Text) * Val(Me.txtTax.Text)) / 100
            '            Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + Math.Round(tax, 2).ToString()
            '        Else
            '            Me.txtNetTotal.Text = Val(Me.txtTotal.Text)
            '        End If
            '    Else
            '        db = Val(Me.txtPackQty.Text) * Val(Me.txtTotal.Text) / Val(Me.txtQty.Text)
            '        Me.txtRate.Text = Math.Round(db, 2).ToString()
            '        If Val(Me.txtTax.Text) > 0 Then
            '            tax = (Val(Me.txtTotal.Text) * Val(Me.txtTax.Text)) / 100
            '            Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + Math.Round(tax, 2).ToString()
            '        Else
            '            Me.txtNetTotal.Text = Val(Me.txtTotal.Text)
            '        End If
            '    End If
            'Else
            '    If Val(Me.txtPackQty.Text) = 0 Or Val(Me.txtPackQty.Text) = 1 Then
            '        db = Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)
            '        Me.txtQty.Text = Math.Round(db, 2).ToString()
            '        If Val(Me.txtTax.Text) > 0 Then
            '            tax = (Val(Me.txtTotal.Text) * Val(Me.txtTax.Text)) / 100
            '            Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + Math.Round(tax, 2).ToString()
            '        Else
            '            Me.txtNetTotal.Text = Val(Me.txtTotal.Text)
            '        End If
            '    Else
            '        db = Val(Me.txtPackQty.Text) * Val(Me.txtTotal.Text) / Val(Me.txtQty.Text)
            '        Me.txtRate.Text = Math.Round(db, 2).ToString()
            '        If Val(Me.txtTax.Text) > 0 Then
            '            tax = (Val(Me.txtTotal.Text) * Val(Me.txtTax.Text)) / 100
            '            Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + Math.Round(tax, 2).ToString()
            '        Else
            '            Me.txtNetTotal.Text = Val(Me.txtTotal.Text)
            '        End If
            '    End If
            'End If
            'Task:2367 Change Total
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            If Not Me.cmbItem.ActiveRow.Cells(0).Value > 0 Or Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            ''Commented out 17-05-2016
            If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtQty.Text) = 0 Then
                Me.txtQty.Text = Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)
            End If

            If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtQty.Text) <> 0 AndAlso Val(Me.txtRate.Text) = 0 Then
                Me.txtRate.Text = Val(Me.txtTotal.Text) / Val(Me.txtQty.Text)
            End If

            If Val(Me.txtPackQty.Text) = 0 Then
                txtPackQty.Text = 1
                txtNetTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            Else
                txtNetTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            End If

            GetDetailTotal()
            'end Task:2367
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited
        'Try
        '    Dim DiscountFactor As Double = Me.grd.GetRow.Cells(GrdEnum.DiscountFactor).Value
        '    Dim DiscountType As Decimal = Me.grd.GetRow.Cells(GrdEnum.DiscountId).Value
        '    Dim PostDiscountPrice As Decimal = Me.grd.GetRow.Cells(GrdEnum.PostDiscountPrice).Value
        '    If DiscountFactor <> 0 Then
        '        If DiscountType = 1 Then
        '            If Not (DiscountFactor >= 0 AndAlso DiscountFactor <= 100) Then
        '                ShowErrorMessage("Enter value according to the discount Type")
        '                grd.CancelCurrentEdit()
        '            End If

        '        ElseIf DiscountType = 2 Then
        '            If Not (DiscountFactor >= 0 AndAlso DiscountFactor <= PostDiscountPrice) Then
        '                ShowErrorMessage("Enter value according to the discount Type")
        '                grd.CancelCurrentEdit()
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    'Task#11082015 Delete item from grd  (Ahmad Sharif)
    Private Sub grd_ColumnButtonClick1(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            'If e.Column.Key = "Delete" Then
            Me.grd.GetRow.Delete()
            grd.UpdateData()
            GetTotal()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#11082015
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''


    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.BtnEdit, Nothing)
            Exit Sub
        End If
        ''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
        If e.KeyCode = Keys.Delete Then
            Me.grd_ColumnButtonClick1(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.BtnEdit, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdSaved.RowCount <= 0 Then Exit Sub
            DeleteToolStripButton_Click(BtnDelete, Nothing)
            Exit Sub
        End If
    End Sub
    Private Sub SelectedQuotationPrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectedQuotationPrintToolStripMenuItem.Click
        Try

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows

                Dim IsPreviewSaleInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
                Dim newinvoice As Boolean = False
                Dim strCriteria As String = "Nothing"
                Me.Cursor = Cursors.WaitCursor
                Try
                    If Me.grdSaved.RowCount = 0 Then Exit Sub
                    newinvoice = getConfigValueByType("NewInvoice")
                    If newinvoice = True Then
                        'str_ReportParam = "@QuotationId|" & r.Cells("QuotationId").Value
                        'strCriteria = String.Empty
                        AddRptParam("@QuotationId", r.Cells("QuotationId").Value)
                        ShowReport("rptQuotationNew" & r.Cells("LocationId").Value)
                        'Else
                        '    str_ReportParam = String.Empty
                        '    strCriteria = "{QuotationDetailTable.QuotationId} = " & r.Cells("QuotationId").Value
                    End If

                    'ShowReport(IIf(newinvoice = False, "rptQuotation", "rptQuotationNew") & r.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", True, , "New")

                Catch ex As Exception
                    Throw ex
                Finally
                    Me.Cursor = Cursors.Default
                End Try

            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintAttachmentQuotationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintAttachmentQuotationToolStripMenuItem.Click
        Try


            DataSetShowReport("rptQuotationAttachment", GetVoucherRecord())

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetVoucherRecord() As DataSet
        Try

            Dim strSQL As String = String.Empty
            Dim ds As New dsQuotation
            ds.Tables.Clear()
            strSQL = "SP_Quotation " & Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString) & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            ds.Tables.Add(dt)
            ds.Tables(0).TableName = "dtQuotation"


            strSQL = String.Empty
            'strSQL = "Select DocId,FileName,Path,Convert(Image,'') as Attachment_Image From DocumentAttachment WHERE (DocId=" & Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString) & ") AND Source=N'" & Me.Name & "'"
            strSQL = "Select DocId,FileName,Path,Convert(Image,'') as Attachment_Image,1 as pic From DocumentAttachment  where right(filename,3) in ('BMP','DIB','RLE','JPG','JPEG','JPE','JFIF','GIF','TIF','TIFF','PNG')  and  DocId=" & Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString) & " AND Source=N'" & Me.Name & "'  "

            Dim dtAttach As New DataTable
            dtAttach.TableName = "dtAttachment"
            dtAttach = GetDataTable(strSQL)

            If dtAttach IsNot Nothing Then
                If dtAttach.Rows.Count > 0 Then
                    For Each r As DataRow In dtAttach.Rows
                        r.BeginEdit()
                        If r.Item("PIC").ToString = "1" Then
                            If IO.File.Exists(CStr(r("Path").ToString & "\" & r("FileName").ToString)) Then
                                LoadPicture(r, "Attachment_Image", CStr(r("Path").ToString & "\" & r("FileName").ToString))
                            End If
                            r.EndEdit()
                        End If
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

    Private Sub PrintQuotationQtyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintQuotationQtyToolStripMenuItem.Click
        Try
            Dim IsPreviewSaleInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
            Dim newinvoice As Boolean = False
            Dim strCriteria As String = "Nothing"
            Me.Cursor = Cursors.WaitCursor
            Try
                If Me.grdSaved.RowCount = 0 Then Exit Sub
                newinvoice = getConfigValueByType("NewInvoice")
                If newinvoice = True Then
                    str_ReportParam = "@QuotationId|" & grdSaved.CurrentRow.Cells("QuotationId").Value
                Else
                    str_ReportParam = String.Empty
                    strCriteria = "{QuotationDetailTable.QuotationId} = " & grdSaved.CurrentRow.Cells("QuotationId").Value
                End If

                If IsPreviewSaleInvoice = False Then
                    ShowReport(IIf(newinvoice = False, "rptQuotation", "rptQuotationQtyNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", True, , "New")
                Else
                    ShowReport(IIf(newinvoice = False, "rptQuotation", "rptQuotationQtyNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", False, , "New")
                End If
            Catch ex As Exception
                Throw ex
            Finally
                Me.Cursor = Cursors.Default
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintQuotationTaxToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintQuotationTaxToolStripMenuItem.Click
        Try
            Dim IsPreviewSaleInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
            Dim newinvoice As Boolean = False
            Dim strCriteria As String = "Nothing"
            Me.Cursor = Cursors.WaitCursor
            Try
                If Me.grdSaved.RowCount = 0 Then Exit Sub
                newinvoice = getConfigValueByType("NewInvoice")
                If newinvoice = True Then
                    str_ReportParam = "@QuotationId|" & grdSaved.CurrentRow.Cells("QuotationId").Value
                Else
                    str_ReportParam = String.Empty
                    strCriteria = "{QuotationDetailTable.QuotationId} = " & grdSaved.CurrentRow.Cells("QuotationId").Value
                End If

                If IsPreviewSaleInvoice = False Then
                    ShowReport(IIf(newinvoice = False, "rptQuotation", "rptQuotationTaxNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", True, , "New")
                Else
                    ShowReport(IIf(newinvoice = False, "rptQuotation", "rptQuotationTaxNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", False, , "New")
                End If
            Catch ex As Exception
                Throw ex
            Finally
                Me.Cursor = Cursors.Default
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub SelectedQuotationPrintToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectedQuotationPrintToolStripMenuItem1.Click
        Try
            SelectedQuotationPrintToolStripMenuItem_Click(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintAttachmentQuotationToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintAttachmentQuotationToolStripMenuItem1.Click
        Try
            PrintAttachmentQuotationToolStripMenuItem_Click(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintQuotationQtyToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintQuotationQtyToolStripMenuItem1.Click
        Try
            PrintQuotationQtyToolStripMenuItem_Click(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintQuotationTaxToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintQuotationTaxToolStripMenuItem1.Click
        Try
            PrintQuotationTaxToolStripMenuItem_Click(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAttachment_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachment.ButtonClick

        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            'Marked Against Task#2015060005 
            'OpenFileDialog1.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG"
            'Marked Against Task#2015060005 

            '            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG"
            'Altered Against Task#2015060006 to make all files attachement physible
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
           "All files (*.*)|*.*"
            'Altered Against Task#2015060006 to make all files attachement physible
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                'Altered Against Task#2015060001 Ali Ansari
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                'Altered Against Task#2015060001 Ali Ansari

                If Me.BtnSave.Text <> "&Save" Then
                    If Me.grdSaved.RowCount > 0 Then
                        intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                    End If
                End If


                'Marked Against Task#2015060001 Ali Ansari
                'Me.btnAttachment.Text = "Attachment (" & arrFile.Length + intCountAttachedFiles & ")"
                'Marked Against Task#2015060001 Ali Ansari
                'Altered Against Task#2015060001 Ali Ansari
                Me.btnAttachment.Text = "Attachment (" & arrFile.Count + intCountAttachedFiles & ")"
                'Altered Against Task#2015060001 Ali Ansari
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
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                'Altered Against Task#2015060001 Ali Ansari
                'Marked Against Task#2015060001 Ali Ansari
                '            If arrFile.Length > 0 Then
                'Marked Against Task#2015060001 Ali Ansari
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        Dim New_Files As String = intId & "_" & DocId & "_" & Me.dtpPODate.Value.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
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

    Private Sub grdSaved_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString)
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub CustomPrintsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@QuotationID", Me.grdSaved.GetRow.Cells("QuotationId").Value)
            DataSetShowReport(CType(sender, ToolStripMenuItem).Name.ToString, GetVoucherRecord(), , "\Reports\Custom Prints\Quotation\")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''Task#A2 12-06-2015 Numeric validation on some textboxes
    Private Sub txtNUM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStock.KeyPress, txtPackQty.KeyPress, txtQty.KeyPress, txtSchemeQty.KeyPress, txtDisc.KeyPress, txtRate.KeyPress, txtTotal.KeyPress, txtTax.KeyPress, txtNetTotal.KeyPress, txtTradePrice.KeyPress, txtSpecialAdjustment.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End Task#A2 12-06-2015

    ''176-2015 TASKM176152 Imran Ali Specification Document Print
    Private Sub PrintQuotationItemSpecificationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintQuotationItemSpecificationToolStripMenuItem.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@QuotationId", Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString))
            ShowReport("rptQuotationItemSpecifications")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintQuotationItemSpecificationToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintQuotationItemSpecificationToolStripMenuItem1.Click
        Try
            PrintQuotationItemSpecificationToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End TASKM176152
    Private Sub btnCostSheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCostSheet.Click
        Try
            frmMain.LoadControl("defineCostSheet")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#07082015 For adding item description (Ahmad Sharif) 
    Private Sub chkAddItemDescription_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAddItemDescription.CheckedChanged
        Try
            If Me.chkAddItemDescription.Checked = True Then
                Me.GroupBox7.Visible = True
            Else
                Me.GroupBox7.Visible = False
                Me.txtBrand.Text = String.Empty
                Me.txtSpecs.Text = String.Empty
                Me.txtTradePrice.Text = String.Empty
                Me.cmbItemDescription.Text = String.Empty
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#07082015 
    Private Sub cmbTermCondition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTermCondition.SelectedIndexChanged
        Try
            If IsFormOpen = False Then Exit Sub
            If Not Me.cmbTermCondition.SelectedItem Is Nothing Then
                Me.txtTerms_And_Condition.Text = ""
                Me.txtTerms_And_Condition.Text = CType(Me.cmbTermCondition.SelectedItem, DataRowView).Row.Item("Term_Condition").ToString()
                'TFS# 956: Add terms and conditions details in grid of selected terms type in combo by Ali Faisal on 19-June-2017
                Dim str As String = ""
                Dim dt As DataTable
                str = "Select * from tblTermsAndConditionDetail Where TermId=" & Me.cmbTermCondition.SelectedValue & ""
                dt = GetDataTable(str)
                Me.grdTerms.UpdateData()
                Me.grdTerms.DataSource = Nothing
                Me.grdTerms.DataSource = dt
                Me.grdTerms.RetrieveStructure()
                ApplyTermsGridSettings()
                'TFS# 956: End
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'TFS# 956: Apply grid settings for terms and conditions details by Ali Faisal on 19-June-2017
    Public Sub ApplyTermsGridSettings()
        Try
            Me.grdTerms.RootTable.Columns("Id").Visible = False
            Me.grdTerms.RootTable.Columns("TermId").Visible = False
            If Me.grdTerms.RootTable.Columns.Contains("Delete") = False Then
                Me.grdTerms.RootTable.Columns.Add("Delete")
                Me.grdTerms.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdTerms.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdTerms.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdTerms.RootTable.Columns("Delete").Key = "Delete"
                Me.grdTerms.RootTable.Columns("Delete").Caption = "Action"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'TFS# 956: End
    Private Function IsQUTExitsInSO() As Boolean
        Dim Result As Boolean = False
    End Function

    Private Sub PrintDuplicateQuotationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintDuplicateQuotationToolStripMenuItem.Click
        Try
            AddRptParam("@QuotationID", Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString))
            ShowReport("rptQuotationDuplicate")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            AddRptParam("@QuotationID", Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString))
            ShowReport("rptQuotationDuplicate")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTotalQuantity_LostFocus(sender As Object, e As EventArgs) Handles txtTotalQuantity.LostFocus
        Try
            GetTotal()
            If Val(Me.txtPackQty.Text) <= 1 Then
                Me.txtQty.Text = Val(Me.txtTotalQuantity.Text)
            End If

            GetDetailTotal()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtTotalQuantity.TextChanged
        Try
            'GetTotal()
            If Not Val(Me.txtPackQty.Text) > 0 Then
                Me.txtQty.Text = Val(Me.txtTotalQuantity.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetDetailTotal()
        Try
            'Task#26082015 by Ahmad Sharif
            If Val(Me.txtPackRate.Text) > 0 Then
                'If getConfigValueByType("Apply40KgRate").ToString = "True" Then
                '    Me.txtRate.Text = (Val(Me.txtPackRate.Text) / 40)
                'Else
                Me.txtRate.Text = (Val(Me.txtPackRate.Text) / Val(Me.txtPackQty.Text))
                'End If
            End If
            'End Task#26082015

            If Val(Me.txtTotalQuantity.Text) <> 0 AndAlso Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtTotal.Text) = 0 Then
                Me.txtTotal.Text = Math.Round((Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text)), DecimalPointInValue)
            End If
            If Val(Me.txtTotalQuantity.Text) <> 0 AndAlso Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtRate.Text) = 0 Then
                Me.txtRate.Text = Val(Me.txtTotal.Text) / Val(Me.txtTotalQuantity.Text)
            End If
            If Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtTotalQuantity.Text) = 0 Then
                If Not Me.cmbUnit.Text <> "Loose" Then
                    Me.txtQty.Text = Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)
                    Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
                Else
                    If Val(Me.txtPackQty.Text) > 0 Then
                        Me.txtQty.Text = (Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)) / Val(Me.txtPackQty.Text)
                        Me.txtTotalQuantity.Text = (Val(Me.txtQty.Text) * Val(Me.txtPackQty.Text))
                    Else
                        Me.txtQty.Text = Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)
                        Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
                    End If
                End If
            Else
                Me.txtTotal.Text = Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text)     'Task#26082015
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

    Private Sub txtDisc_LostFocus(sender As Object, e As EventArgs) Handles txtDisc.LostFocus
        Try
            ''If getConfigValueByType("Apply40KgRate").ToString = "True" Then Me.txtDisc.Text = 0 : Exit Sub
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
                    Me.txtDiscountValue.Text = discount
                Else
                    discount = Val(Me.txtDisc.Text)
                    Me.txtDiscountValue.Text = discount
                End If
                Me.txtRate.Text = Val(Rate) - Math.Round(discount, 2).ToString()

            Else
                Me.txtRate.Text = Rate
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetGridDetailQtyCalculate(ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            Me.grd.UpdateData()
            If e.Column.Index = GrdEnum.Qty Or e.Column.Index = GrdEnum.PackQty Then
                If Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value = (Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) * Val(Me.grd.GetRow.Cells(GrdEnum.Qty).Value.ToString))
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                Else
                    Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value = Val(Me.grd.GetRow.Cells(GrdEnum.Qty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                End If
            ElseIf e.Column.Index = GrdEnum.TotalQty Then
                If Not Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(GrdEnum.Qty).Value = Val(Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.Qty).Value
                End If
            ElseIf e.Column.Index = GrdEnum.PackPrice Then
                If Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(GrdEnum.Rate).Value = (Val(Me.grd.GetRow.Cells(GrdEnum.PackPrice).Value.ToString) / Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString))
                End If
            End If
            'Me.grd.Refetch()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_CellUpdated_1(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
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

            Me.GetGridDetailQtyCalculate(e)
            Me.GetGridDetailTotal()
            Me.GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalQuantity_Leave(sender As Object, e As EventArgs) Handles txtTotalQuantity.Leave
        Try
            GetTotal()
            If Not Val(Me.txtPackQty.Text) > 0 Then
                Me.txtQty.Text = Val(Me.txtTotalQuantity.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetGridDetailTotal()
        Try

            Me.grd.UpdateData()
            Me.grd.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            If Val(grd.GetRow.Cells(GrdEnum.TotalQty).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(GrdEnum.Rate).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) = 0 Then
                'grd.GetRow.Cells(EnumGridDetail.Total).Value = Math.Round((Val(grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString) * Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString)), DecimalPointInValue)
            End If
            If Val(grd.GetRow.Cells(GrdEnum.TotalQty).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(GrdEnum.Rate).Value.ToString) = 0 Then
                Me.txtRate.Text = Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) / Val(grd.GetRow.Cells(GrdEnum.TotalQty).Value.ToString)
            End If
            If Val(grd.GetRow.Cells(GrdEnum.Rate).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(GrdEnum.TotalQty).Value.ToString) = 0 Then
                If Not Me.cmbUnit.Text <> "Loose" Then
                    grd.GetRow.Cells(GrdEnum.Qty).Value = Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) / Val(grd.GetRow.Cells(GrdEnum.Rate).Value.ToString)
                    grd.GetRow.Cells(GrdEnum.TotalQty).Value = Val(grd.GetRow.Cells(GrdEnum.Qty).Value.ToString)
                Else
                    If Val(grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) > 0 Then
                        grd.GetRow.Cells(GrdEnum.Qty).Value = (Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) / Val(grd.GetRow.Cells(GrdEnum.Rate).Value.ToString)) / Val(grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString)
                        grd.GetRow.Cells(GrdEnum.TotalQty).Value = (Val(grd.GetRow.Cells(GrdEnum.Qty).Value.ToString) * Val(grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString))
                    Else
                        grd.GetRow.Cells(GrdEnum.Qty).Value = Val(grd.GetRow.Cells(GrdEnum.Total).Value.ToString) / Val(grd.GetRow.Cells(GrdEnum.Rate).Value.ToString)
                        grd.GetRow.Cells(GrdEnum.TotalQty).Value = Val(grd.GetRow.Cells(GrdEnum.Qty).Value.ToString)
                    End If
                End If
            End If

            Me.grd.EditMode = Janus.Windows.GridEX.EditMode.EditOff

        Catch ex As Exception
            Throw ex
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

        'If SalesOrderAnalysis = True Then
        '    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
        '        MarketReturnsValue += IIf(r.Cells(GrdEnum.Unit).Value = "Pack", (((r.Cells("Qty").Value * r.Cells("PackQty").Value) + r.Cells("SchemeQty").Value) * r.Cells("MarketReturns").Value), ((r.Cells("Qty").Value + r.Cells("SchemeQty").Value) * r.Cells("MarketReturns").Value))
        '        Disc += IIf(r.Cells(GrdEnum.Unit).Value = "Loose", (((r.Cells("Qty").Value * r.Cells("CurrentPrice").Value) * r.Cells("Discount_Percentage").Value) / 100), ((((r.Cells("Qty").Value * r.Cells("PackQty").Value) * r.Cells("CurrentPrice").Value) * r.Cells("Discount_Percentage").Value) / 100))
        '        BillAfterDisc += IIf(r.Cells(GrdEnum.Unit).Value = "Loose", ((r.Cells("Qty").Value * r.Cells("CurrentPrice").Value) - Disc), (((r.Cells("Qty").Value * r.Cells("PackQty").Value) * r.Cells("CurrentPrice").Value) - Disc))
        '        If Me.rbtAdjFlat.Checked = False Then
        '            SpecialAdj += ((BillAfterDisc * Val(Me.txtSpecialAdjustment.Text)) / 100)
        '        End If
        '        TradeValue += IIf(r.Cells(GrdEnum.Unit).Value = "Loose", (((r.Cells("Qty").Value * r.Cells("TradePrice").Value) + ((r.Cells("SchemeQty").Value * r.Cells("CurrentPrice").Value) * r.Cells("SalesTax_Percentage").Value) / 100)), ((((r.Cells("Qty").Value * r.Cells("PackQty").Value) * r.Cells("TradePrice").Value) + ((r.Cells("SchemeQty").Value * r.Cells("CurrentPrice").Value) * r.Cells("SalesTax_Percentage").Value) / 100)))
        '    Next
        '    If Me.rbtAdjFlat.Checked = True Then
        '        SpecialAdj = Val(Me.txtSpecialAdjustment.Text)
        '    End If
        '    'WHTaxValue = ((Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) * WHTaxPercent) / 100)
        '    WHTaxValue = ((Val(TradeValue) * WHTaxPercent) / 100)
        '    TransitValue = ((Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) * TransitPercent) / 100)
        '    NetAmount = (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("BillValueAfterDiscount"), Janus.Windows.GridEX.AggregateFunction.Sum)) + TransitValue + WHTaxValue) - (SpecialAdj + MarketReturnsValue)
        'Else
        NetAmount = (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.Net_Amount), Janus.Windows.GridEX.AggregateFunction.Sum))) ''TFS3758 , Get NetAmount instead of Total
        If Me.rbtAdjFlat.Checked = False Then
            Adjustment = ((NetAmount * Val(Me.txtSpecialAdjustment.Text)) / 100)
        Else
            Adjustment = Val(Me.txtSpecialAdjustment.Text)
        End If
        NetAmount = Math.Round((NetAmount - Adjustment), TotalAmountRounding) ''TFS3738
        'End If
        ''Start TFS3758
        For Each r As Janus.Windows.GridEX.GridEXRow In grd.GetRows
            If flgExcludeTaxPrice = True Then
                NetAmount += Val(r.Cells(GrdEnum.SalesTax_Amount).Value.ToString)
            End If
        Next
        ''End TFS3758
        Me.txtNetBill.Text = Math.Round(NetAmount, TotalAmountRounding) ''TFS3738

    End Sub

    Private Sub btnPrintRevision_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Function FillRevisionCombo(ByVal quotationId As Integer) As Integer
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Try
            str = "Select QuotationHistoryId, RevisionNumber FROM QuotationHistory Where QuotationId =" & quotationId & " ORDER BY 1 DESC"
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
    Public Function CheckRevisionNumber(ByVal quotationId As Integer) As Integer
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Try
            str = "Select Distinct RevisionNumber, RevisionNumber FROM QuotationHistory Where QuotationId =" & quotationId & " ORDER BY 1 DESC"
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows.Item(0).Item(0).ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub DisplayDetailHistory(ByVal RevisionId As Integer, ByVal HitoryId As Integer)
        Try

            Dim str As String = String.Empty

            str = "SELECT " & IIf(SerialNoIncludingcharacter = True, "Case when Recv_D.SerialNo IS Null OR Recv_D.SerialNo = '' then Convert(nvarchar, 0) else Recv_D.SerialNo end As SerialNo1", "Case when Recv_D.SerialNo = '' OR Recv_D.SerialNo IS Null  then 0 else CONVERT(decimal(17,3),Recv_D.SerialNo) end  As SerialNo1") & ", Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.RequirementDescription, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price As PostDiscountPrice , Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyId, 0)= 1 Then 1 When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As TotalCurrencyAmount, 1 as DiscountId , 0 AS DiscountFactor, 0 As DiscountValue , " _
             & "   (IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * Case When IsNull(Recv_D.CurrencyId, 0)= 1 Then 1 When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total,  " _
             & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice,Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as SalesTaxAmount, Convert(float,0) as CurrencySalesTaxAmount, IsNull(SED_Tax_Percent,0) as [SED_Tax_Percent], IsNull(SED_Tax_Amount,0) as [SED_Tax_Amount], Convert(float,0) as CurrencySEDAmount, Convert(float,0) as Net_Amount, ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.ItemDescription ,Recv_D.BrandName as Brand, Recv_D.Specification, Recv_D.RegistrationNo as ItemRegistrationNo, IsNull(Recv_D.TradePrice,0) as TradePrice, Recv_D.TenderSrNo,IsNull(Recv_D.CostPrice,0) as CostPrice, ISNULL(Recv_D.QuotationId, 0) As QuotationId, ISNULL(Recv_D.QuotationDetailId, 0) As QuotationDetailId, ISNULL(Recv_D.SOQuantity, 0) As SOQuantity, IsNull(Recv_D.Qty, 0) As TotalQty, IsNull(Recv_D.PurchaseInquiryDetailId, 0) As PurchaseInquiryDetailId,  IsNull(Recv_D.VendorQuotationDetailId, 0) As VendorQuotationDetailId,  IsNull(Recv_D.HeadArticleId, 0) As HeadArticleId, IsNull(Recv_D.PurchaseInquiryId, 0) As PurchaseInquiryId, Convert(bit,Recv_D.Alternate) As Alternate FROM dbo.QuotationDetailHistory Recv_D LEFT OUTER JOIN " _
             & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
             & " dbo.QuotationHistory History ON Recv_D.QuotationHistoryId = History.QuotationHistoryId LEFT OUTER JOIN " _
             & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
             & " Where History.RevisionNumber =" & RevisionId & " And History.QuotationHistoryId = " & HitoryId & " ORDER BY 1 Asc"

            Dim dtDisplayDetail As New DataTable
            dtDisplayDetail = GetDataTable(str)
            'dtDisplayDetail.Columns("Total").Expression = "IsNull([TotalQty],0)*IsNull([Price],0)"
            'dtDisplayDetail.Columns("SalesTaxAmount").Expression = "((IsNull([SalesTax_Percentage],0)/100)*IsNull([Total],0))"
            'dtDisplayDetail.Columns("SED_Tax_Amount").Expression = "((IsNull([SED_Tax_Percent],0)/100)*IsNull([Total],0))"
            'dtDisplayDetail.Columns("Net_Amount").Expression = "(IsNull([Total],0)+IsNull([SalesTaxAmount],0)+IsNull([SED_Tax_Amount],0))"
            dtDisplayDetail.Columns("Total").Expression = "IsNull([TotalQty],0)*IsNull([Price],0)*IsNull(CurrencyRate, 0)"
            dtDisplayDetail.Columns("CurrencyAmount").Expression = "IsNull([TotalQty],0)*IsNull([Price],0)"
            dtDisplayDetail.Columns("SalesTaxAmount").Expression = "((IsNull([SalesTax_Percentage],0)/100)*IsNull([Total],0))"
            dtDisplayDetail.Columns("CurrencySalesTaxAmount").Expression = "((IsNull([SalesTax_Percentage],0)/100)*IsNull([CurrencyAmount],0))"
            dtDisplayDetail.Columns("SED_Tax_Amount").Expression = "((IsNull([SED_Tax_Percent],0)/100)*IsNull([Total],0))"
            dtDisplayDetail.Columns("CurrencySEDAmount").Expression = "((IsNull([SED_Tax_Percent],0)/100)*IsNull([CurrencyAmount],0))"
            dtDisplayDetail.Columns("Net_Amount").Expression = "(IsNull([Total],0)+IsNull([SalesTaxAmount],0)+IsNull([SED_Tax_Amount],0))"
            dtDisplayDetail.Columns("TotalCurrencyAmount").Expression = "(IsNull([CurrencyAmount],0)+IsNull([CurrencySalesTaxAmount],0)+IsNull([CurrencySEDAmount],0))"

            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            If dtDisplayDetail.Rows.Count > 0 Then
                If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                    'Me.cmbCurrency.SelectedValue = Nothing
                    Me.cmbCurrency.Enabled = False
                Else
                    IsCurrencyEdit = True
                    Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                    Me.txtCurrencyRate.Text = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString)
                    Me.cmbCurrency.Enabled = False
                End If
                'cmbCurrency_SelectedIndexChanged(Nothing, Nothing)
            End If
            FillCombo("grdLocation")
            ApplyGridSetting()
            CtrlGrdBar3_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DisplayHistory(ByVal revisionNumber As Integer, ByVal historyId As Integer)
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Try
            str = "SELECT  Recv.QuotationNo, Recv.QuotationDate AS Date, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty,  " & _
                               "Recv.SalesOrderAmount, Recv.QuotationId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, PoNo as [Po No], Recv.NewCustomer, '' as Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull([No Of Attachment],0) as [No Of Attachment], ISNULL(Recv.Apprved,0)as Approved,recv.Terms_And_Condition,recv.username as 'User Name', Recv.VerifiedBy as [Verified By] " & _
                               ",Recv.CustomerPhone as [Inco Term],Recv.CustomerAddress as [Payment Term],Recv.Approved_User as [Approved User]  " & _
                               "FROM dbo.QuotationHistory Recv LEFT OUTER JOIN " & _
                               "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.QuotationNo LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.QuotationId WHERE Recv.QuotationNo IS NOT NULL And Recv.RevisionNumber =" & revisionNumber & " And Recv.QuotationHistoryId = " & historyId & " "
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.cmbCompany.SelectedValue = Val(dt.Rows.Item(0).Item("LocationId").ToString)
            txtPONo.Text = dt.Rows.Item(0).Item("QuotationNo").ToString
            dtpPODate.Value = CType(dt.Rows.Item(0).Item("Date"), Date)
            txtReceivingID.Text = Val(dt.Rows.Item(0).Item("QuotationId").ToString)
            cmbVendor.Value = Val(dt.Rows.Item(0).Item("VendorId").ToString)
            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Vendor is disable.")
                Exit Sub
            End If
            Me.txtTerms_And_Condition.Text = dt.Rows.Item(0).Item("Terms_And_Condition").ToString
            Me.txtNewCustomer.Text = dt.Rows.Item(0).Item("NewCustomer").ToString
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
            'If IsDBNull(grdSaved.CurrentRow.Cells("SOP_ID")) Then
            '    Me.cmbSalesMan.SelectedValue = 0
            'Else
            '    Me.cmbSalesMan.SelectedValue = grdSaved.CurrentRow.Cells("SOP_ID").Value
            'End If
            'Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value

            Me.txtCustPONo.Text = dt.Rows.Item(0).Item("PO No").ToString
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
            Me.ChkApproved.Checked = dt.Rows.Item(0).Item("Approved")
            'Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
            'Me.cmbCompany.Enabled = False
            Me.lblPrintStatus.Text = "Print Status: " & dt.Rows.Item(0).Item("Print Status").ToString

            'If flgDateLock = True Then
            '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
            '        'ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '        Me.dtpPODate.Enabled = False
            '    Else
            '        Me.dtpPODate.Enabled = True
            '    End If
            'Else
            '    Me.dtpPODate.Enabled = True
            'End If

            'Dim intCountAttachedFiles As Integer = 0I
            'If Me.BtnSave.Text <> "&Save" Then
            '    If Me.grdSaved.RowCount > 0 Then
            '        intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
            '        Me.btnAttachment.Text = "Attachment (" & intCountAttachedFiles & ")"
            '    End If
            'End If
            Me.txtCustomerMobile.Text = dt.Rows.Item(0).Item("Inco Term").ToString
            Me.txtCustomerAddress.Text = dt.Rows.Item(0).Item("Payment Term").ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintSelectedRevisionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintSelectedRevisionToolStripMenuItem.Click
        Try
            ''TASK-480 on 14-07-2016
            If Not Me.cmbRevisionNumber.SelectedValue Is Nothing Then
                AddRptParam("@RevisionNumber", Val(Me.cmbRevisionNumber.Text))
                AddRptParam("@QuotationHistoryId", Me.cmbRevisionNumber.SelectedValue)
                ShowReport("rptQuotationHistory")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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

    Private Sub cmbItem_ValueChanged(sender As Object, e As EventArgs) Handles cmbItem.ValueChanged
        Try
            If chkAddItemDescription.Checked = True Then
                Me.txtBrand.Text = String.Empty
                Me.txtSpecs.Text = String.Empty
                Me.txtItemRegNo.Text = String.Empty
                Me.cmbItemDescription.Text = String.Empty
                Me.txtTradePrice.Text = String.Empty
                Me.txtTenderSrNo.Text = String.Empty
            Else : Exit Sub
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

    Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                Me.txtCurrencyRate.Text = dr.Row.Item("CurrencyRate").ToString

                ' R@!    11-Jun-2016 Dollor account
                ' Setting default rate to zero
                If Val(Me.txtCurrencyRate.Text) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If
                If Me.cmbCurrency.SelectedValue = BaseCurrencyId Then
                    Me.txtCurrencyRate.Enabled = False
                Else
                    Me.txtCurrencyRate.Enabled = True

                End If
                'R@!    11-Jun-2016 Dollor account
                'Code Commented
                ' Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
                ' Added 2 coloumns and changed caption
                Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Caption = "Amount (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns(GrdEnum.TotalCurrencyAmount).Caption = "Total Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencySalesTaxAmount).Caption = "Tax Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencySEDAmount).Caption = "SED Amount (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns("Net_Amount").Caption = "Net Amount (" & Me.BaseCurrencyName & ")"
                'Me.grd.RootTable.Columns("Total Amount").Caption = "Total Amount (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns("Total").Caption = "Total (" & Me.BaseCurrencyName & ")"

                'grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.TotalCurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencySalesTaxAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencySEDAmount).Visible = False

                    'Me.grd.RootTable.Columns(EnumGrid.Credit).Visible = False
                Else
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = True
                    'Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.TotalCurrencyAmount).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencySalesTaxAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencySEDAmount).Visible = False
                    If IsEditMode = False Then
                        If Me.grd.RowCount > 0 Then
                            For Each GriDEX As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                                GriDEX.Cells("CurrencyRate").Value = Val(Me.txtCurrencyRate.Text)
                                GriDEX.Cells("CurrencyId").Value = Me.cmbCurrency.SelectedValue
                                GriDEX.Cells("BaseCurrencyId").Value = BaseCurrencyId
                                GriDEX.Cells("BaseCurrencyRate").Value = 1
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

    Private Sub btnEmailQuotation_Click(sender As Object, e As EventArgs) Handles btnEmailQuotation.Click
        Try
            'GetTemplate(Label14.Text)
            'If EmailTemplate.Length > 0 Then
            '    GetEmailData()
            '    GetVendorsEmails()
            '    FormatStringBuilder(dtEmail)
            IsQuotationReportExportToPDF = True
            'AddRptParam("@QuotationId", Val(txtReceivingID.Text))
            'ShowReport("rptQuotationNew" & r.Cells("LocationId").Value)
            'ShowReport("rptQuotationNew1")
            PrintQuotationQtyToolStripMenuItem_Click(sender, e)
            CreateOutLookMail()
            IsQuotationReportExportToPDF = False
            'Else
            'msg_Error("No email template is found for comparison statement.")
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
                    'If Me.grdItems.RootTable.Columns.Contains(TrimSpace) Then
                    dtEmail.Columns.Add(TrimSpace)
                    AllFields.Add(TrimSpace)
                    'Else
                    '    msg_Error("'" & TrimSpace & "'column does not exist")
                    'End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetEmailData()
        'dtEmail = New DataTable
        Dim Dr As DataRow
        Try

            'For Each Row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
            '    Dr = dtEmail.NewRow
            '    For Each col As String In AllFields
            '        If Row.Table.Columns.Contains(col) Then
            '            Dr.Item(col) = Row.Cells(col).Value.ToString
            '        End If
            '    Next
            '    dtEmail.Rows.Add(Dr)
            'Next

            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetCheckedRows
                'Dim dt As DataTable = InquiryComparisonStatementDAL.EditRecord(Val(Row.Cells("InquiryComparisonStatementId").Value.ToString))
                Dim PurchaseInquiryDetailId As Integer = 0
                If Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString) > 0 Then
                    PurchaseInquiryDetailId = Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString)
                Else
                    PurchaseInquiryDetailId = Val(Row.Cells("HeadArticleId").Value.ToString)
                End If
                'If InquiryComparisonStatementDAL.IsPurchaseInquiryExists(PurchaseInquiryDetailId) = True Then
                'Dim dt As DataTable = InquiryComparisonStatementDAL.EmailSalesQuotation(PurchaseInquiryDetailId)

                For Each Row1 As DataRow In dt.Rows
                    Dr = dtEmail.NewRow
                    For Each col As String In AllFields
                        If Row1.Table.Columns.Contains(col) Then
                            Dr.Item(col) = Row1.Item(col).ToString
                        End If
                    Next
                    dtEmail.Rows.Add(Dr)
                Next
                'Else
                'msg_Information("No Sales Quotation exists against selected row.")
                'End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetVendorsEmails()
        Try
            Me.grdSaved.UpdateData()
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                If VendorEmails.Length > 0 Then
                    VendorEmails += "; " & Row.Cells("Email").Value.ToString & ""
                Else
                    VendorEmails = "" & Row.Cells("Email").Value.ToString & ""
                End If
            Next
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
                Dim Pattern = "([a-z?])[_ ]?([A-Z])"
                Dim ColumnName = Regex.Replace(column.ColumnName, Pattern, "$1 $2")
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
    Private Sub CreateOutLookMail()
        Try

            Dim oApp As Outlook.Application = New Outlook.Application

            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = ""
            mailItem.To = Me.cmbVendor.ActiveRow.Cells("Email").Value.ToString
            VendorEmails = String.Empty
            'mailItem.CC = txtCC.Text
            'Me.txtCC.Text = ""
            'mailItem.BCC = txtBCC.Text
            'Me.txtBCC.Text = ""
            'mailItem.Body = html.ToString
            mailItem.Subject = Me.txtPONo.Text
            'If IO.Directory.Exists(PDFPath) Then
            mailItem.Attachments.Add(PDFPath, Outlook.OlAttachmentType.olEmbeddeditem, Nothing, "PDF")
            'End If
            Dim dtAttachments As DataTable = MasterDAL.GetAttachments(Me.Name, Val(Me.txtReceivingID.Text))
            If dtAttachments.Rows.Count > 0 Then
                For Each Row As DataRow In dtAttachments.Rows
                    'Dim byte1() As Byte = Row.Item("Attachment_Image")
                    'Dim St As MemoryStream = New MemoryStream(byte1)
                    'Dim Attachment As New System.Net.Mail.Attachment()
                    mailItem.Attachments.Add(Row.Item("Path").ToString & "\" & Row.Item("FileName").ToString, Outlook.OlAttachmentType.olEmbeddeditem, Nothing, "Picture")
                Next
            End If
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            mailItem.HTMLBody = "" + mailItem.HTMLBody
            mailItem = Nothing
            oApp = Nothing

            '     mailItem.Display(mailItem);
            'mailItem.HTMLBody = body + mailItem.HTMLBody;
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function GetSingle(ByVal QuotationId As Integer) As DataTable
        Dim Str As String = ""
        Try
            Str = "SELECT Recv.QuotationNo, Recv.QuotationDate, dbo.vwCOADetail.detail_title AS CustomerName, Recv.SalesOrderQty,  " & _
                                "Recv.SalesOrderAmount, Recv.QuotationId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, ISNULL(Recv.LocationId,0) as LocationId, ISNULL(Recv.SpecialAdjustment,0) as SpecialAdjustment, Recv.PoNo, Recv.NewCustomer, '' as Employee_Name, ISNULL(Recv.Posted,0) as Posted, Recv.Delivery_Date, Isnull(Recv.Adj_Flag,0) as Adj_Flag, Isnull(Recv.Adjustment,0) as Adjustment, Isnull(Recv.CostCenterID,0) as CostCenterId, Recv.PO_Date,  ISNULL(Recv.EditionalTax_Percentage,0) as EditionalTax_Percentage, ISNULL(Recv.SED_Percentage,0) as SED_Percentage, ISNULL(Recv.Apprved,0)as Approved, recv.Terms_And_Condition, recv.UserName, Recv.VerifiedBy" & _
                                ",Recv.CustomerPhone, Recv.CustomerAddress, Recv.Approved_User " & _
                                "FROM dbo.QuotationMasterTable Recv LEFT OUTER JOIN " & _
                                "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
                                "tblDefEmployee ON Recv.EmployeeId = tblDefEmployee.Employee_Id WHERE Recv.QuotationId = " & QuotationId & ""
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
    'TFS# 956: Column key set as Delete and to remove the selected row from Terms grid by Ali Faisal on 19-June-2017
    Private Sub grdTerms_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdTerms.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                Me.grdTerms.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles btn_History.Click
        Try
            frmHistorysalespurchase.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbVendor_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbVendor.KeyDown
        Try
            ''TFS1781 : Ayesha Rehman :Added for Selection of Customer or Vendor
            If e.KeyCode = Keys.F1 Then

                frmSearchCustomersVendors.rbtCustomers.Checked = True
                frmSearchCustomersVendors.rbtVendors.Checked = True
                frmSearchCustomersVendors.rbtCustomers.Visible = True
                frmSearchCustomersVendors.rbtVendors.Visible = True

                frmSearchCustomersVendors.BringToFront()
                frmSearchCustomersVendors.ShowDialog()
                If frmSearchCustomersVendors.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbVendor.Value = frmSearchCustomersVendors.SelectedAccountId
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub cmbVendor_Leave(sender As Object, e As EventArgs)
        ''Added for TFS1798
        Try
            If Val(Me.cmbVendor.ActiveRow.Cells("SaleMan").Value.ToString) > 0 Then
                Me.cmbSalesMan.SelectedValue = Val(Me.cmbVendor.ActiveRow.Cells("SaleMan").Value.ToString)
                If Me.cmbSalesMan.SelectedValue Is Nothing Then
                    If Me.cmbSalesMan.SelectedIndex = -1 Then Me.cmbSalesMan.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub PrintQuotationWithImageToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintQuotationWithImageToolStripMenuItem1.Click
        Dim strSQL As String = String.Empty
        strSQL = "SP_QuotationPrint " & Val(Me.grdSaved.GetRow.Cells("QuotationId").Value.ToString) & " "
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
            ShowReport("rptquotationWithImage", , , , , , , dt, , , , )
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbDiscountType_Leave(sender As Object, e As EventArgs) Handles cmbDiscountType.Leave
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
    ''' <remarks>Ayesha Rehman : 28-03-2017 :TFS2827 </remarks>
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
    ''' <remarks>Ayesha Rehman : 28-03-2017 :TFS2827</remarks>
    Private Sub txtPDP_TextChanged(sender As Object, e As EventArgs) Handles txtPDP.TextChanged
        Try
            txtRate.Text = Val(txtPDP.Text)
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
    ''Start TFS3738 :  Ayesha Rehman : 03-07-2018
    Private Sub txtSpecialAdjustment_LostFocus1(sender As Object, e As EventArgs) Handles txtSpecialAdjustment.LostFocus
        Try
            If grd.RootTable IsNot Nothing Then Me.grd.UpdateData()
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End TFS3738
    ''' <summary>
    ''' Aashir : TFS3760: Save Template
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveTemplate() As Boolean

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        If chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        Me.grd.UpdateData()
        Me.txtPONo.Text = GetDocumentNo()
        setVoucherNo = Me.txtPONo.Text
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        Dim TransitValue As Double = 0D
        Dim MarketReturnsValue As Double = 0D
        Dim WHTaxValue As Double = 0D
        Dim SpecialAdj As Double = 0D
        Dim Disc As Double = 0D
        Dim BillAfterDisc As Double = 0D
        Dim TradeValue As Double = 0D

        objCon = Con
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            txtPONo.Text = GetNextDocNo("Template", 3, "QuotationTemplateMasterTable", "QuotationNo")
            Dim intReceivingId As Integer = 0I

            If Val(SaveAsTemplateToolStripMenuItem.Tag.ToString) > 0 Then

                intReceivingId = Val(SaveAsTemplateToolStripMenuItem.Tag.ToString)
                objCommand.CommandText = "Update QuotationTemplateMasterTable set LocationId=" & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbCompany.SelectedValue) & ", Remarks='" & Me.txtRemarks.Text & "' where QuotationId=" & intReceivingId
                objCommand.ExecuteNonQuery()

            Else

                NetAmount = (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.Total), Janus.Windows.GridEX.AggregateFunction.Sum)))
                objCommand.CommandText = "Insert into QuotationTemplateMasterTable(LocationId,QuotationNo,QuotationDate,VendorId,SalesOrderQty,SalesOrderAmount, CashPaid, Remarks,UserName, status, SpecialAdjustment,Posted, PONo, NewCustomer,Delivery_Date, Adj_Flag, Adjustment,CostCenterId, PO_Date,EditionalTax_Percentage,SED_Percentage, Terms_And_Condition,Apprved,CustomerPhone,CustomerAddress,Approved_User, RevisionNumber,TermsAndConditionsId,EmployeeId,ManualSerialNo, Subject) values( " _
                                       & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbCompany.SelectedValue) & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & NetAmount & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & IIf(Me.rbtAdjPercentage.Checked = True, Val(Me.txtSpecialAdjustment.Text), 0) & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtCustPONo.Text.Replace("'", "''") & "', N'" & Me.txtNewCustomer.Text.Replace("'", "''") & "', " & IIf(Me.dtpDeliveryDate.Checked = True, "N'" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & IIf(Me.rbtAdjFlat.Checked = True, 0, 1) & ", " & IIf(Me.rbtAdjFlat.Checked = True, Adjustment, Val(Me.txtSpecialAdjustment.Text)) & ", " & Me.cmbProject.SelectedValue & ", " & IIf(Me.dtpPDate.Checked = True, "N'" & Me.dtpPDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " & TransitPercent & "," & WHTaxPercent & ", N'" & ReplaceNewLine(Me.txtTerms_And_Condition.Text, False).Replace("'", "''") & "', " & IIf(Me.ChkApproved.Checked = True, 1, 0) & ",N'" & Me.txtCustomerMobile.Text.Replace("'", "''").ToString & "',N'" & Me.txtCustomerAddress.Text.Replace("'", "''").ToString & "'," & IIf(ChkApproved.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & ", 0," & Me.cmbTermCondition.SelectedValue & "," & Me.cmbSalesMan.SelectedValue & ", N'" & txtManualSerialNo.Text & "', N'" & txtSubject.Text & "') Select @@Identity"
                getVoucher_Id = objCommand.ExecuteScalar
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                intReceivingId = getVoucher_Id
                If Me.grdTerms.RowCount > 0 Then
                    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdTerms.GetDataRows
                        objCommand.CommandText = "Insert into QuotationTermsDetails (TermId,QuotationId,Heading,Details) Values (" & Me.cmbTermCondition.SelectedValue & ",ident_current('QuotationMasterTable'),N'" & row.Cells("Heading").Value.ToString & "',N'" & row.Cells("Details").Value.ToString & "')"
                        objCommand.ExecuteNonQuery()
                    Next
                End If
            End If

            objCommand.CommandText = "delete from QuotationTemplateDetailTable where QuotationId=" & intReceivingId
            objCommand.ExecuteNonQuery()

            If arrFile.Count > 0 Then
                SaveDocument(getVoucher_Id, Me.Name, trans)
            End If

            For i = 0 To grd.RowCount - 1

                objCommand.CommandText = ""

                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                objCommand.CommandText = "Insert into QuotationTemplateDetailTable (QuotationId, LocationId, ArticleDefId, ArticleSize, Sz1, Qty, Price, Sz7, CurrentPrice, SalesTax_Percentage, SchemeQty, Discount_Percentage,PurchasePrice,PackPrice, Pack_Desc, Comments,ItemDescription,BrandName,Specification,RegistrationNo,TradePrice,TenderSrNo,CostPrice,SED_Tax_Percent, SED_Tax_Amount,SOQuantity, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, RequirementDescription, PurchaseInquiryDetailId, VendorQuotationDetailId, HeadArticleId, SerialNo, PurchaseInquiryId, DiscountId, DiscountValue, DiscountFactor , PostDiscountPrice) values( " _
                                      & " " & intReceivingId & ", " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value.ToString) & ",  " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString) & ", " _
                                      & " " & Val(grd.GetRows(i).Cells(GrdEnum.TotalQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(GrdEnum.SalesTax_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.SchemeQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Discount_Percentage).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PurchasePrice).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "','" & Me.grd.GetRows(i).Cells(GrdEnum.ItemDescription).Value.ToString.Replace("'", "''") & "','" & Me.grd.GetRows(i).Cells(GrdEnum.Brand).Value.ToString.Replace("'", "''") & "','" & Me.grd.GetRows(i).Cells(GrdEnum.Specification).Value.ToString.Replace("'", "''") & "','" & Me.grd.GetRows(i).Cells(GrdEnum.ItemRegistrationNo).Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells(GrdEnum.TradePrice).Value.ToString) & ",'" & Me.grd.GetRows(i).Cells(GrdEnum.TenderSrNo).Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("SED_Tax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("SED_Tax_Amount").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("SOQuantity").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("RequirementDescription").Value.ToString.Replace("'", "''") & "', " & IIf(Val(Me.grd.GetRows(i).Cells("PurchaseInquiryDetailId").Value.ToString) = 0, "NULL", Val(Me.grd.GetRows(i).Cells("PurchaseInquiryDetailId").Value.ToString)) & ", " & Val(Me.grd.GetRows(i).Cells("VendorQuotationDetailId").Value.ToString) & ", " & IIf(Val(Me.grd.GetRows(i).Cells("HeadArticleId").Value.ToString) = 0, "NULL", Val(Me.grd.GetRows(i).Cells("HeadArticleId").Value.ToString)) & ", N'" & Me.grd.GetRows(i).Cells("SerialNo1").Value.ToString.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells("PurchaseInquiryId").Value.ToString) & "," & Val(grd.GetRows(i).Cells("DiscountId").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("DiscountValue").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("DiscountFactor").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PostDiscountPrice").Value) & ")"

                objCommand.ExecuteNonQuery()

            Next
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            'If getConfigValueByType("EnableDuplicateQuotation").ToString.ToUpper = "TRUE" Then
            '    Call CreateDuplicationQuotation(getVoucher_Id, "Save", trans)
            'End If
            trans.Commit()
            SaveTemplate = True
            setEditMode = False
            Total_Amount = NetAmount
            MarketReturnsValue = 0D
            Disc = 0D
            BillAfterDisc = 0D
            SpecialAdj = 0D
            TradeValue = 0D
            WHTaxValue = 0D
            TransitValue = 0D
            NetAmount = 0D
            Dim ValueTable As DataTable = GetSingle(getVoucher_Id)
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            'NotificationDAL.SaveAndSendNotification("Quotation", "QuotationMasterTable", getVoucher_Id, ValueTable, "Sales > Quotation")
            Me.lblProgress.Visible = False
        Catch ex As Exception
            trans.Rollback()
            SaveTemplate = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)

            objCommand = Nothing
        End Try
    End Function
    ''' <summary>
    ''' Aashir: TFS3760 Save Template on Save Button Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SaveAsTemplateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsTemplateToolStripMenuItem.Click
        If SaveTemplate() = True Then
            If Val(SaveAsTemplateToolStripMenuItem.Tag.ToString) > 0 Then
                msg_Information("Template Updated")
            Else
                msg_Information("Template Saved")
            End If
            RefreshControls()
            ClearDetailControls()
            DisplayDetail(-1)

        End If
    End Sub
    ''' <summary>
    ''' Aashir TFS3760 Display Templates
    ''' </summary>
    ''' <remarks></remarks>
    Sub DisplayQuotationTemplates()
        Try
            Dim str As String = String.Empty
            str = "SELECT QuotationId, QuotationNo as TemplateNo, QuotationDate, Remarks FROM QuotationTemplateMasterTable ORDER BY QuotationId DESC"
            FillGridEx(grdTemplates, str, True)
            Me.grdTemplates.RootTable.Columns("QuotationId").Visible = False
            Me.grdTemplates.RootTable.Columns("QuotationDate").FormatString = "dd/MMM/yyyy"
            Me.grdTemplates.AutoSizeColumns()
        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' Aashir:TFS3760: Display Template Detail
    ''' </summary>
    ''' <param name="ReceivingID"></param>
    ''' <param name="TypeId"></param>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Sub DisplayQuotationTemplatesDetail(ByVal ReceivingID As Integer, Optional ByVal TypeId As Integer = Nothing, Optional ByVal Condition As String = "")
        Try

            Dim str As String = String.Empty

            'str = "SELECT CAST(Case when Recv_D.SerialNo IS Null OR Recv_D.SerialNo = '' then 0 else Recv_D.SerialNo end AS Numeric(10,0)) As SerialNo1, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.RequirementDescription, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty,CASE WHEN IsNull(Article.SalePrice,0) = 0 THEN IsNULL(Recv_D.PostDiscountPrice, 0) ELSE IsNull(Article.SalePrice,0) END AS PostDiscountPrice , Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyId, 0)=1 Then 1 When IsNull(Recv_D.CurrencyRate, 0) = 0  Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As TotalCurrencyAmount, Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue ," _
            ' & "  (IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * Case When IsNull(Recv_D.CurrencyId, 0)=1 Then 1 When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total,  " _
            ' & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice,Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as SalesTaxAmount, Convert(float,0) as CurrencySalesTaxAmount, IsNull(SED_Tax_Percent,0) as [SED_Tax_Percent], IsNull(SED_Tax_Amount,0) as [SED_Tax_Amount], Convert(float,0) as CurrencySEDAmount, Convert(float,0) as Net_Amount, ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.ItemDescription ,Recv_D.BrandName as Brand, Recv_D.Specification, Recv_D.RegistrationNo as ItemRegistrationNo, IsNull(Recv_D.TradePrice,0) as TradePrice, Recv_D.TenderSrNo,IsNull(Recv_D.CostPrice,0) as CostPrice, ISNULL(Recv_D.QuotationId, 0) As QuotationId, ISNULL(Recv_D.QuotationDetailId, 0) As QuotationDetailId, ISNULL(Recv_D.SOQuantity, 0) As SOQuantity, IsNull(Recv_D.Qty, 0) As TotalQty, Recv_D.PurchaseInquiryDetailId,  IsNull(Recv_D.VendorQuotationDetailId, 0) As VendorQuotationDetailId, Recv_D.HeadArticleId As HeadArticleId, IsNull(Recv_D.PurchaseInquiryId, 0) As PurchaseInquiryId, Convert(bit,Recv_D.Alternate) As Alternate FROM dbo.QuotationTemplateDetailTable Recv_D LEFT OUTER JOIN " _
            ' & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            ' & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            ' & " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY 1  Asc "

            'Start Task 3913 Saad Afzaal change query get updated price of item from articledefpacktable

            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            str = "SELECT " & IIf(SerialNoIncludingcharacter = True, "Case when Recv_D.SerialNo IS Null OR Recv_D.SerialNo = '' then Convert(nvarchar, 0) else Recv_D.SerialNo end As SerialNo1", "Case when Recv_D.SerialNo = '' OR Recv_D.SerialNo IS Null  then 0 else CONVERT(decimal(17,3),Recv_D.SerialNo) end  As SerialNo1") & ", Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.RequirementDescription, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, CASE WHEN IsNull(articledefpacktable.PackRate,0) = 0 THEN IsNULL(Recv_D.PostDiscountPrice, 0) ELSE IsNull(articledefpacktable.PackRate,0)/IsNull(Recv_D.Sz7,0) END AS PostDiscountPrice , CASE WHEN IsNull(articledefpacktable.PackRate,0) = 0 THEN IsNULL(Recv_D.Price, 0) ELSE IsNull(articledefpacktable.PackRate,0) END AS Price , IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyId, 0)=1 Then 1 When IsNull(Recv_D.CurrencyRate, 0) = 0  Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As TotalCurrencyAmount, Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue ," _
             & "  (IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * Case When IsNull(Recv_D.CurrencyId, 0)=1 Then 1 When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total,  " _
             & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(recv_d.PackPrice,0) as PackPrice,Isnull(recv_d.SalesTax_Percentage,0) as SalesTax_Percentage, Convert(float,0) as SalesTaxAmount, Convert(float,0) as CurrencySalesTaxAmount, IsNull(SED_Tax_Percent,0) as [SED_Tax_Percent], IsNull(SED_Tax_Amount,0) as [SED_Tax_Amount], Convert(float,0) as CurrencySEDAmount, Convert(float,0) as Net_Amount, ISNULL(recv_d.SchemeQty,0) as SchemeQty, ISNULL(recv_d.Discount_Percentage,0) as Discount_Percentage, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.ItemDescription ,Recv_D.BrandName as Brand, Recv_D.Specification, Recv_D.RegistrationNo as ItemRegistrationNo, IsNull(Recv_D.TradePrice,0) as TradePrice, Recv_D.TenderSrNo,IsNull(Recv_D.CostPrice,0) as CostPrice, ISNULL(Recv_D.QuotationId, 0) As QuotationId, ISNULL(Recv_D.QuotationDetailId, 0) As QuotationDetailId, ISNULL(Recv_D.SOQuantity, 0) As SOQuantity, IsNull(Recv_D.Qty, 0) As TotalQty, Recv_D.PurchaseInquiryDetailId,  IsNull(Recv_D.VendorQuotationDetailId, 0) As VendorQuotationDetailId, Recv_D.HeadArticleId As HeadArticleId, IsNull(Recv_D.PurchaseInquiryId, 0) As PurchaseInquiryId, Convert(bit,Recv_D.Alternate) As Alternate FROM dbo.QuotationTemplateDetailTable Recv_D LEFT OUTER JOIN " _
             & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
             & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId Left Outer Join articledefpacktable ON Article.MasterID = articledefpacktable.ArticleMasterId And  Recv_D.ArticleSize = articledefpacktable.PackName" _
             & " Where Recv_D.QuotationId =" & ReceivingID & " ORDER BY 1  Asc "

            'End Task

            Dim dtDisplayDetail As New DataTable
            dtDisplayDetail = GetDataTable(str)

            dtDisplayDetail.Columns("DiscountValue").Expression = "IIF(DiscountId= 1,((IsNull(PostDiscountPrice,0)*IsNull(DiscountFactor,0))/100), IsNull(DiscountFactor,0))" ''TFS2827
            dtDisplayDetail.Columns("Price").Expression = "IIF(DiscountId= 1, IsNull(PostDiscountPrice,0)-((IsNull(PostDiscountPrice,0)*IsNull(DiscountFactor,0))/100), IsNull(PostDiscountPrice,0)-IsNull(DiscountFactor,0))"
            dtDisplayDetail.Columns("Total").Expression = "IsNull([TotalQty],0)*IsNull([Price],0)*IsNull([CurrencyRate], 0)"
            dtDisplayDetail.Columns("CurrencyAmount").Expression = "IsNull([TotalQty],0)*IsNull([Price],0)"
            dtDisplayDetail.Columns("SalesTaxAmount").Expression = "((IsNull([SalesTax_Percentage],0)/100)*IsNull([Total],0))"
            dtDisplayDetail.Columns("CurrencySalesTaxAmount").Expression = "((IsNull([SalesTax_Percentage],0)/100)*IsNull([CurrencyAmount],0))"
            dtDisplayDetail.Columns("SED_Tax_Amount").Expression = "((IsNull([SED_Tax_Percent],0)/100)*IsNull([Total],0))"
            dtDisplayDetail.Columns("CurrencySEDAmount").Expression = "((IsNull([SED_Tax_Percent],0)/100)*IsNull([CurrencyAmount],0))"
            dtDisplayDetail.Columns("Net_Amount").Expression = "(IsNull([Total],0)+IsNull([SalesTaxAmount],0)+IsNull([SED_Tax_Amount],0))"
            dtDisplayDetail.Columns("TotalCurrencyAmount").Expression = "(IsNull([CurrencyAmount],0)+IsNull([CurrencySalesTaxAmount],0)+IsNull([CurrencySEDAmount],0))"

            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail

            If dtDisplayDetail.Rows.Count > 0 Then
                If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then

                    Me.cmbCurrency.Enabled = False
                Else

                    FillCombo("Currency")
                    Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                    Me.cmbCurrency.Enabled = False
                End If
                cmbCurrency_SelectedIndexChanged(Nothing, Nothing)
            End If
            FillCombo("grdLocation")

            FillCombo("grdDiscountType")

            ApplyGridSetting()
            CtrlGrdBar3_Load(Nothing, Nothing)
            Me.SaveAsTemplateToolStripMenuItem.Text = "Update Qoutation template" ' & Me.grdTemplates.CurrentRow.Cells("TemplateName").Value.ToString
            SaveAsTemplateToolStripMenuItem.Tag = ReceivingID

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub
    ''' <summary>
    ''' Aashir: TFS3760: fill Quotation grid on Double Click of template grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdTemplates_DoubleClick(sender As Object, e As EventArgs) Handles grdTemplates.DoubleClick
        Try
            If grdTemplates.RowCount > 0 AndAlso grdTemplates.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                If Me.grd.GetRows.Length > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                End If

                Me.DisplayQuotationTemplatesDetail(grdTemplates.CurrentRow.Cells("QuotationId").Value)
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnLoadTemplate_Click(sender As Object, e As EventArgs) Handles btnLoadTemplate.Click
        grdTemplates_DoubleClick(Me, Nothing)
    End Sub
    ''' <summary>
    ''' Aashir: TFS3760 :Delete selected template on Delete button Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDeleteTemplate_Click(sender As Object, e As EventArgs) Handles btnDeleteTemplate.Click
        If Not Me.grdTemplates.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If

        If IsValidToDelete("SalesOrderMasterTable", "QuotationId", Me.grdTemplates.CurrentRow.Cells("QuotationId").Value.ToString) = True Then

            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

            Try
                Dim cm As New OleDbCommand
                Dim objTrans As OleDbTransaction
                If Con.State = ConnectionState.Closed Then Con.Open()
                objTrans = Con.BeginTransaction
                cm.Connection = Con

                cm.CommandText = "delete from QuotationTemplateDetailTable where QuotationId=" & Me.grdTemplates.CurrentRow.Cells("QuotationId").Value.ToString

                cm.Transaction = objTrans
                cm.ExecuteNonQuery()

                cm = New OleDbCommand
                cm.Connection = Con

                cm.CommandText = "delete from QuotationTemplateMasterTable where QuotationId=" & Me.grdTemplates.CurrentRow.Cells("QuotationId").Value.ToString
                cm.Transaction = objTrans
                cm.ExecuteNonQuery()

                objTrans.Commit()

                Me.txtReceivingID.Text = 0

            Catch ex As Exception
                msg_Error("Error occured while deleting record: " & ex.Message)

            Finally
                Con.Close()
            End Try

            Me.grdTemplates.CurrentRow.Delete()
            msg_Information("Template Deleted")

            Me.RefreshControls()
        Else
            msg_Error(str_ErrorDependentRecordFound)
        End If

    End Sub

    Private Sub PrintDuplicateQuotationToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintDuplicateQuotationToolStripMenuItem1.Click

        Try
            PrintDuplicateQuotationToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintSelectedRevisionToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintSelectedRevisionToolStripMenuItem1.Click
        Try
            PrintSelectedRevisionToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintQuotationWithToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintQuotationWithToolStripMenuItem.Click
        Try
            PrintQuotationWithImageToolStripMenuItem1_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub SalesInquiry_Click(sender As Object, e As EventArgs) Handles SalesInquiry.Click
        Try
            frmSearchSalesInquiry.flag = True
            frmSearchSalesInquiry.ShowDialog()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Sub AddToGridFromSalesInquiry(ByVal Row As Janus.Windows.GridEX.GridEXRow)
        Dim dt As New DataTable
        Try
            dt = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item(GrdEnum.SerialNo) = Row.Cells("SerialNo").Value.ToString
            dr.Item(GrdEnum.LocationId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
            ''Start TFS4150 : Commented to get the actual name of pack in Unit
            'dr.Item(GrdEnum.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            dr.Item(GrdEnum.Unit) = cmbUnit.SelectedValue
            ''End TFS4150
            dr.Item(GrdEnum.Qty) = Val(Row.Cells("Qty").Value.ToString)
            'dr.Item(GrdEnum.Rate) = Val(Me.txtRate.Text)
            'dr.Item(GrdEnum.Total) = Val(Me.txtTotal.Text)
            dr.Item(GrdEnum.CategoryId) = Val(Row.Cells("CategoryId").Value.ToString)
            'TFS# 956: Add Article Alias in detail grid in requirment description column by Ali Faisal on 19-June-2017
            dr.Item(GrdEnum.ArticleCode) = Row.Cells("Code").Value.ToString
                dr.Item(GrdEnum.Item) = Row.Cells("ArticleDescription").Value.ToString
                dr.Item(GrdEnum.ItemId) = Val(Row.Cells("ArticleId").Value.ToString)
            dr.Item(GrdEnum.RequirementDescription) = Row.Cells("RequirementDescription").Value.ToString
            
            
            'TFS# 956: End
            dr.Item(GrdEnum.PackQty) = Val(Me.txtPackQty.Text)

            dr.Item(GrdEnum.SalesTax_Percentage) = Val(Me.txtTax.Text)
            dr.Item(GrdEnum.Pack_Desc) = Me.cmbUnit.Text.ToString
            dr.Item(GrdEnum.Comments) = Row.Cells("Comments").Value.ToString

            'Task#15092015 add columns in enum and also add in grd by ahmad
            dr.Item(GrdEnum.ItemDescription) = Me.cmbItemDescription.Text
            dr.Item(GrdEnum.Brand) = Me.txtBrand.Text
            dr.Item(GrdEnum.Specification) = Me.txtSpecs.Text
            dr.Item(GrdEnum.ItemRegistrationNo) = Me.txtItemRegNo.Text
            dr.Item(GrdEnum.TradePrice) = Val(Me.txtTradePrice.Text)
            dr.Item(GrdEnum.TenderSrNo) = Me.txtTenderSrNo.Text
            dr.Item(GrdEnum.TotalQty) = Val(Me.txtTotalQuantity.Text)


            dr.Item(GrdEnum.CurrencyId) = Me.cmbCurrency.SelectedValue
            dr.Item(GrdEnum.CurrencyRate) = Val(Me.txtCurrencyRate.Text)
            Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
            If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                dr.Item(GrdEnum.BaseCurrencyId) = Val(ConfigCurrencyVal)
                dr.Item(GrdEnum.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
            End If
            dr.Item(GrdEnum.PurchaseInquiryDetailId) = 0
            dr.Item(GrdEnum.VendorQuotationDetailId) = 0
            dr.Item(GrdEnum.HeadArticleId) = 0
            dr.Item(GrdEnum.PurchaseInquiryId) = 0
            'dr.Item(GrdEnum.Alternate) = 0
            dr.Item("Warranty") = Me.txtWarranty.Text
            dr.Item("LeadTime") = Me.txtLeadTime.Text
            dr.Item("State") = Me.cmbState.Text
            dt.Rows.Add(dr)
            'End Task#15092015 
            ''Start TFS2827
            
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub txtCurrencyRate_TextChanged(sender As Object, e As EventArgs) Handles txtCurrencyRate.TextChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                If Me.cmbCurrency.SelectedValue = BaseCurrencyId Then
                    Me.txtCurrencyRate.Enabled = False
                Else
                    Me.txtCurrencyRate.Enabled = True

                End If
                If Val(Me.txtCurrencyRate.Text) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If

                'Me.grd.RootTable.Columns("CurrencyAmount").Caption = "Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"
                'Me.grd.RootTable.Columns(EnumGridDetail.CurrencyTaxAmount).Caption = "Tax Amount (" & Me.cmbCurrency.Text & ")"

                'grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    'Me.grd.RootTable.Columns(EnumGrid.CurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = False
                    'Me.grd.RootTable.Columns(EnumGridDetail.CurrencyTaxAmount).Visible = False
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
                Else
                    'Me.grd.RootTable.Columns(EnumGrid.CurrencyAmount).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = True
                    'Me.grd.RootTable.Columns(EnumGridDetail.CurrencyTaxAmount).Visible = False
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
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Change by Murtaza for txtcurrencyrate text change (11/09/2022)
End Class
