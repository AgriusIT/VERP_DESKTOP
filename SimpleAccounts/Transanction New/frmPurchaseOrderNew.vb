'' 10-Dec-2013 By Imran Ali Tax on Purchase order
'' 14-Dec-2013 ReqId-928  Imran Ali            Add Comments column on Item Detail  in purchase order 
''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction forms
''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''19-Dec-2013 R:945 , TaskId:2338        M Ijaz Javed           By default pack rupees
''25-Dec-2013 R:913           Imran Ali           Add Purchase order posting rights
''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
''28-Dec-2013 RM6 Imran Ali            Release 2.1.0.0 Bug
''2-Jan-2014   Tsk:2367    Imran Ali             Calculation Problem
''11-Jan-2014 Task:2373         Imran Ali                Add Columns SubSub Title in Account List on Sales/Purchase
''11-Jan-2014 Task:2374           Imran Ali                Net Amount Sales/Purchase 
''23-Jan-2014 TASK:2392              Imran Ali                 Duplicate PO History
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
''03-Feb-2014        Task:2406   Imran Ali    FIELD CHOOSER restriction (Senior Rozgar)
''15-Feb-2014 Task:2426 Imran Ali Payment Schedule On Sales Order And Purchase Order
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
''28-Feb-2014  TASK:24445 Imran Ali   Last purchase and sale price show on sale order and purchase order
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
'' 6-June-2014 TASK:2673 Imran Ali CMFA Document Developement (Ravi)
''18-June-2014 TASK:2695 Imran Ali CMFA Load On Purchase Order
''11-Jul-2014 Task:2734 IMRAN ALI Ehancement CMFA Document
''16-Jul-2014 TASK:2744 Imran Ali Problem Facing Record not in grid and cmfa Document Attachment On CMFA Document (Ravi)
''16-Jul-2014 TASK:2746 Imran Ali Cash Request Less On PO Validation (Ravi)
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''25-Sep-2014 Task:2856 Imran Ali Change scenario of cmfa on purchase (ravi) 
'2015-02-20 Changes Against Task# 7 Ali Ansari Add PackQty in Selection 
''Ahmad Sharif:Update Query for Last Price of Same Customer for same item, 06-06-2015
'2015-06-08 Task# 2015060006 Save Attachements Ali Ansari
'16-Jun-2015 Task#1-16062015 Ahmad Sharif: Add PO Type,Stock Dispatch Status
'17-Jun-2015 Task#1-17062015 Ahmad Sharif: Add two columns in quries
'18-Jun-2015 Task#118062015 Ahmad Sharif: Add Try Catch in RefreshControl Sub
'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'16-Sep-2015 Task#16092015 Ahmad Sharif: Load Companies and Locations user wise
''19-9-2015 TASK24 Imran Ali: Add Field UOM In Grid Detail.
''19-9-2015 TASK15 Imran Ali: Posted Purchase Demand 
''19-9-2015 TASK22 Imran Ali: Validation On Price And Quantity
''13-Nov-2015 TASK-TFS-51 Apply Additional Tax
''16-11-2015 TASK-TFS-60: Purchase demand should be loaded separately on PO as it is entered.
''TASK TFS1474 Muhammad Ameen on 15-09-2017. Currency rate can not be edited while base currency is selected.
'' TASK TFS1592 Ayesha Rehman on 19-10-2017 Future date entry should be rights based
'' TASK TFS2377 Ayesha Rehman on 26-02-2018PO Print Based on Checked And Posted Doc
''TFS2988 Ayesha Rehman : 09-04-2018 : If document is approved from one stage then it should not change able for previous stage
''TFS2989 Ayesha Rehman : 10-04-2018 : If document is rejected from one stage then it should open for previous stage for approval
''TASK TFS3493 Muhammad Ameen made currencty combo editable in edit mode so that user can change currency type and also changes should be applied according to currency in grid detail.
''TFS4161 Ayesha Rehman : 09-08-2018 : P QTY: (Should Be Static/ Un-Changeable / Un-Editable on All Screens)
''TFS4689 Ayesha Rehman : 03-10-2018 : Show only relevant Accounts on Transactional screens while User wise COA Configuration.
''TFS4705 Ayesha Rehman : 08-10-2018 : Pack Unit & Pack Size issue on PO, GRN and Purchase.
Imports System.Data.OleDb
Imports System.Data.SqlClient
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

Public Class frmPurchaseOrderNew
    ' Change on 23-11-2013  For Multiple Print Vouchers
    Dim dt As DataTable
    Dim Mode As String = "Normal"
    Dim IsEditMode As Boolean = False
    Dim IsFormLoaded As Boolean = False
    Dim Email As Email
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim setVoucherNo As String = String.Empty
    Dim getVoucher_Id As Integer = 0
    Dim Total_Amount As Double = 0D
    Dim setEditMode As Boolean = False
    Dim crpt As New ReportDocument
    Dim Previouse_Amount As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim flgCompanyRights As Boolean = False
    Dim flgCurrenyonOpenLC As Boolean = False
    Dim taxamnt As Double = 0D     ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
    'Marked Against Task#2015060001 Ali Ansari
    'Dim arrfile As String
    'Marked Against Task#2015060001 Ali Ansari
    'Altered Against Task#2015060001 Ali Ansari
    ' Convert string into List of string for making proper count
    Dim arrFile As List(Of String)
    'Altered Against Task#2015060001 Ali Ansari
    '' ReqId-899 Added New Enum TaxPercent, TaxAmount
    Dim PurchaseDemandID As Integer = 0I
    Dim PurchaseDemandDetailId As Integer = 0
    Dim strComments As String = String.Empty
    Dim BaseCurrencyId As Integer = 0
    Dim BaseCurrencyName As String = String.Empty
    Dim IsSOLoad As Boolean = False
    Dim EmailDAL As New EmailTemplateDAL
    Dim html As StringBuilder
    Dim VendorEmails As String = String.Empty
    Dim dtEmail As DataTable
    Dim AllFields As List(Of String)
    Dim EmailTemplate As String = String.Empty
    Dim AfterFieldsElement As String = String.Empty
    Dim NotificationDAL As New NotificationTemplatesDAL
    Dim LoadAllRecords As Boolean = False
    'Code Edit for task 1592 future date rights
    Dim IsDateChangeAllowed As Boolean = False
    Public Shared Rate As Double
    ''TFS2375 : Ayesha Rehman : This Variable is Added to check ApprovalProcessId ,if it is mapped against the document
    Dim ApprovalProcessId As Integer = 0
    ''TFS2375 : Ayesha Rehman :End
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Dim CurrencyRate As Double = 1
    Dim ItemFilterByName As Boolean = False
    Dim dsEmail As DataSet
    Dim UsersEmail As List(Of String)
    Dim EmailBody As String = String.Empty
    Dim PurchaseOrderNo As String
    Dim PurchaseOrderId As Integer
    Enum GrdEnum
        'Category
        SerialNo
        LocationId
        ArticleCode
        Item
        'TASK24 Added Index
        Color
        Size
        UOM
        'END TASK24
        Unit
        Warranty
        Status
        Qty
        CurrentPrice
        RateDiscPercent
        Rate
        BaseCurrencyId
        BaseCurrencyRate
        CurrencyId
        CurrencyRate
        CurrencyAmount
        CurrencyTotalAmount
        Total
        TaxPercent
        TaxAmount
        CurrencyTaxAmount
        AdTax_Percent
        AdTax_Amount
        CurrencyAdTaxAmount
        TotalAmount 'Task:2374 Added Index
        CategoryId
        ItemId
        PackQty
        'CurrentPrice
        PackPrice
        Pack_Desc
        ''ReqId-928 Added Index of Comments
        Comments
        PurchaseDemandId
        PurchaseDemandDetailId
        PurchaseOrderDetailId
        TotalQty
        ButtonDelete
    End Enum

    Private Sub frmPurchaseOrderNew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 9 -1-14
            If e.KeyCode = Keys.F4 Then
                If BtnSave.Enabled = True Then
                    SaveToolStripButton_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                PrintToolStripButton_Click(Nothing, Nothing)
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
    Private Sub frmPurchaseOrderNew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try


            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            If Not getConfigValueByType("CurrencyonOpenLC").ToString = "Error" Then
                flgCurrenyonOpenLC = Convert.ToBoolean(getConfigValueByType("CurrencyonOpenLC").ToString)
            End If
            ''start TFS4161
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            If Not getConfigValueByType("PurchaseDiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("PurchaseDiablePackQuantity").ToString)
            End If
            ''End TFS4161
            ' ''TASK TFS4544
            'If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            '    ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            'End If
            ' ''END TFS4544
            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)
            FillCombo("Category")
            'FillCombo("Item")
            FillCombo("Vendor")
            'FillCombo("Currency")
            FillCombo("LC") 'R933 Call LC List
            FillCombo("CostCenter")
            'rafay
            FillCombo("Status")
            'rafay
            FillCombo("CMFADoc") 'Task:2673 Call DropDown function for CMFA Document
            FillCombo("Demand")
            FillCombo("InwardExpense")
            'FillCombo("ArticlePack") 'R933 Commented
            'FillCombo("LC")

            'Task#1-16062015 fill stock dispatch status in combo box 
            FillCombo("StockDispatchStatus")
            FillCombo("TermsCondition")
            Me.cmbPOType.SelectedIndex = 0
            'End Task#1-16062015

            'Ali FAisal :TFS1442 : Fill drop down for Payment terms
            FillCombo("PaymentTerms")
            'Ali FAisal :TFS1442 : End
            IsFormLoaded = True
            RefreshControls()

            'Me.cmbVendor.Focus()
            'Me.DisplayRecord() R933 Commented History Data

            Get_All(frmModProperty.Tags)
            'If frmModProperty.blnListSeachStartWith = True Then
            '    cmbItem.AutoCompleteMode = Win.AutoCompleteMode.Suggest
            '    cmbItem.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.StartsWith
            'End If

            'If frmModProperty.blnListSeachContains = True Then
            '    cmbItem.AutoCompleteMode = Win.AutoCompleteMode.Suggest
            '    cmbItem.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.Contains
            'End If

            'TFS3360
            UltraDropDownSearching(cmbVendor, frmMain.blnListSeachStartWith, frmMain.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmMain.blnListSeachStartWith, frmMain.blnListSeachContains)

            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
            '04-Aug-2017: Task TFS1152: Rai Haider: Verify Check box value in system configuration(purchase) for Disable/Enable Rate Editing
            'Start Task:
            If getConfigValueByType("VisiblerateonPO") = "True" Then
                Me.txtRate.Enabled = True
            Else
                Me.txtRate.Enabled = False
            End If
            'End Task:TFS1152


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            If frmModProperty.Tags.Length > 0 Then frmModProperty.Tags = String.Empty ''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
        End Try
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub
    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
        Dim str As String = String.Empty
        'str = "SELECT     Recv.PurchaseOrderNo, CONVERT(varchar, Recv.PurchaseOrderDate, 103) AS Date, V.VendorName, Recv.PurchaseOrderQty, Recv.PurchaseOrderAmount, " _
        '       & " Recv.PurchaseOrderId, Recv.VendorCode, Recv.Remarks, convert(varchar, Recv.CashPaid) as CashPaid FROM         dbo.PurchaseOrderMasterTable Recv INNER JOIN dbo.tblVendor V ON Recv.VendorCode = V.AccountId"
        If Mode = "Normal" Then
            'Before against request no. 913
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseOrderNo, Recv.PurchaseOrderDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseOrderQty,  " & _
            '        "Recv.PurchaseOrderAmount, Recv.PurchaseOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date " & _
            '        "FROM dbo.PurchaseOrderMasterTable Recv INNER JOIN  " & _
            '        "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN(Select DISTINCT PurchaseOrderId, LocationId From PurchaseOrderDetailTable) as Location On Location.PurchaseOrderId = Recv.PurchaseOrderId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseOrderNo WHERE Recv.PurchaseOrderNo IS NOT NULL " & _
            '        " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseOrderDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'Before against Task:2392 Duplicate PO History
            'R:913 Added Column Post
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseOrderNo, Recv.PurchaseOrderDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseOrderQty,  " & _
            '       "Recv.PurchaseOrderAmount, Recv.PurchaseOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date,Case When Isnull(Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post " & _
            '       "FROM dbo.PurchaseOrderMasterTable Recv INNER JOIN  " & _
            '       "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN(Select DISTINCT PurchaseOrderId, LocationId From PurchaseOrderDetailTable) as Location On Location.PurchaseOrderId = Recv.PurchaseOrderId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseOrderNo WHERE Recv.PurchaseOrderNo IS NOT NULL " & _
            '       " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseOrderDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'End R:913

            ''23-Jan-2014 TASK:2392              Imran Ali                 Duplicate PO History
            'Before against task:2673
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseOrderNo, Recv.PurchaseOrderDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseOrderQty,  " & _
            '      "Recv.PurchaseOrderAmount, Recv.PurchaseOrderId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date,Case When Isnull(Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post " & _
            '      "FROM dbo.PurchaseOrderMasterTable Recv INNER JOIN  " & _
            '      "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN(Select DISTINCT PurchaseOrderId From PurchaseOrderDetailTable) as Location On Location.PurchaseOrderId = Recv.PurchaseOrderId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseOrderNo WHERE Recv.PurchaseOrderNo IS NOT NULL " & _
            '      " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseOrderDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'End Task:2392
            'Task:2673 Added Field RefCMFADocId
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseOrderNo, Recv.PurchaseOrderDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseOrderQty,  " & _
            '      "Recv.PurchaseOrderAmount, Recv.PurchaseOrderId, Recv.VendorId, Recv.Remarks, Recv.Terms_And_Condition as Terms, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date,Case When Isnull(Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post, IsNull(Recv.RefCMFADocId,0) as RefCMFADocId, (CMFAMasterTable.DocNo + '~' + Convert(Varchar,CMFAMasterTable.DocDate,102)) [CMFA Doc No]" & _
            '      "FROM dbo.PurchaseOrderMasterTable Recv INNER JOIN  " & _
            '      "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN CMFAMasterTable On CMFAMasterTable.DocId = Recv.RefCMFADocId LEFT OUTER JOIN(Select DISTINCT PurchaseOrderId From PurchaseOrderDetailTable) as Location On Location.PurchaseOrderId = Recv.PurchaseOrderId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseOrderNo WHERE Recv.PurchaseOrderNo IS NOT NULL " & _
            '      " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseOrderDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'End Task:2673
            'Marked against Task#201506006 add attachement
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseOrderNo, Recv.PurchaseOrderDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseOrderQty,  " & _
            '                  " Recv.PurchaseOrderAmount, Recv.PurchaseOrderId, Recv.VendorId, Recv.Remarks, Recv.Terms_And_Condition as Terms, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date,Case When Isnull(Recv.Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post, IsNull(Recv.RefCMFADocId,0) as RefCMFADocId, (CMFAMasterTable.DocNo + '~' + Convert(Varchar,CMFAMasterTable.DocDate,102)) [CMFA Doc No], dbo.vwCOADetail.Contact_Email as Email,Recv.LocationId,IsNull(Recv.CostCenterId,0) as CostCenterId,IsNull(Recv.PurchaseDemandId,0) as PurchaseDemandId, PDemand.PurchaseDemandNo " & _
            '                  " FROM dbo.PurchaseOrderMasterTable Recv INNER JOIN  " & _
            '                  " dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN PurchaseDemandMasterTable PDemand on PDemand.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN CMFAMasterTable On CMFAMasterTable.DocId = Recv.RefCMFADocId LEFT OUTER JOIN(Select DISTINCT PurchaseOrderId From PurchaseOrderDetailTable) as Location On Location.PurchaseOrderId = Recv.PurchaseOrderId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseOrderNo WHERE Recv.PurchaseOrderNo IS NOT NULL " & _
            '                  " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseOrderDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'Marked against Task#201506006 add attachement
            'Altered against Task#201506006 add attachement
            'Task#117062015 add local po/stock dispatch status two columns in query
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseOrderNo, Recv.PurchaseOrderDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseOrderQty,  " & _
            '                  " Recv.PurchaseOrderAmount, Recv.PurchaseOrderId, Recv.VendorId, Recv.Remarks, Recv.Terms_And_Condition as Terms, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date,Case When Isnull(Recv.Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post, IsNull(Recv.RefCMFADocId,0) as RefCMFADocId, (CMFAMasterTable.DocNo + '~' + Convert(Varchar,CMFAMasterTable.DocDate,102)) [CMFA Doc No], dbo.vwCOADetail.Contact_Email as Email,Recv.LocationId,IsNull(Recv.CostCenterId,0) as CostCenterId,IsNull(Recv.PurchaseDemandId,0) as PurchaseDemandId, PDemand.PurchaseDemandNo,Recv.POType,Recv.POStockDispatchStatus, IsNull([No Of Attachment],0) as [No Of Attachment] " & _
            '                  " FROM dbo.PurchaseOrderMasterTable Recv INNER JOIN  " & _
            '                  " dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN PurchaseDemandMasterTable PDemand on PDemand.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN CMFAMasterTable On CMFAMasterTable.DocId = Recv.RefCMFADocId LEFT OUTER JOIN(Select DISTINCT PurchaseOrderId From PurchaseOrderDetailTable) as Location On Location.PurchaseOrderId = Recv.PurchaseOrderId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseOrderNo  LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.PurchaseOrderId WHERE Recv.PurchaseOrderNo IS NOT NULL " & _
            '                  " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseOrderDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            ''End Task#117062015
            'Altered against Task#201506006 add attachement
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseOrderNo, Recv.PurchaseOrderDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseOrderQty,  " & _
            '                              " Recv.PurchaseOrderAmount, Recv.PurchaseOrderId, Recv.VendorId, Recv.Remarks, Recv.Terms_And_Condition as Terms, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date,Case When Isnull(Recv.Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post, IsNull(Recv.RefCMFADocId,0) as RefCMFADocId, (CMFAMasterTable.DocNo + '~' + Convert(Varchar,CMFAMasterTable.DocDate,102)) [CMFA Doc No], dbo.vwCOADetail.Contact_Email as Email,Recv.LocationId,IsNull(Recv.CostCenterId,0) as CostCenterId,IsNull(Recv.PurchaseDemandId,0) as PurchaseDemandId, PDemand.PurchaseDemandNo,Recv.POType,Recv.POStockDispatchStatus, IsNull([No Of Attachment],0) as [No Of Attachment],Recv.UserName as 'User Name',Recv.UpdateUserName as [Modified By] " & _
            '                              " FROM dbo.PurchaseOrderMasterTable Recv INNER JOIN  " & _
            '                              " dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN PurchaseDemandMasterTable PDemand on PDemand.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN CMFAMasterTable On CMFAMasterTable.DocId = Recv.RefCMFADocId LEFT OUTER JOIN(Select DISTINCT PurchaseOrderId From PurchaseOrderDetailTable) as Location On Location.PurchaseOrderId = Recv.PurchaseOrderId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseOrderNo  LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.PurchaseOrderId  LEFT OUTER JOIN (SELECT DISTINCT PayTypeId, OrderId FROM tblPaymentSchedule WHERE (OrderId = )) AS PaySch ON PaySch.OrderId = Recv.PurchaseOrderId  WHERE Recv.PurchaseOrderNo IS NOT NULL " & _
            '                              " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseOrderDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms

            'Ali FAisal :TFS1442 : Altered to get PayTypeId column in history
            str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseOrderNo, Recv.PurchaseOrderDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseOrderQty,dbo.tblcurrency.currency_code," & _
                                          " Recv.PurchaseOrderAmount,Recv.AmountUS, Recv.PurchaseOrderId, Recv.VendorId, Recv.Remarks, Recv.Terms_And_Condition as Terms, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date,Case When Isnull(Recv.Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post, IsNull(Recv.RefCMFADocId,0) as RefCMFADocId, (CMFAMasterTable.DocNo + '~' + Convert(Varchar,CMFAMasterTable.DocDate,102)) [CMFA Doc No], dbo.vwCOADetail.Contact_Email as Email,Recv.LocationId,IsNull(Recv.CostCenterId,0) as CostCenterId,IsNull(Recv.PurchaseDemandId,0) as PurchaseDemandId, PDemand.PurchaseDemandNo,Recv.POType,Recv.POStockDispatchStatus, IsNull([No Of Attachment],0) as [No Of Attachment],Recv.UserName as 'User Name',Recv.UpdateUserName as [Modified By] , IsNull(Recv.PayTypeId,0) PayTypeId" & _
                                          " FROM dbo.PurchaseOrderMasterTable Recv inner join  " & _
                                          "dbo.tblcurrency ON Recv.CurrencyType = dbo.tblcurrency.currency_id left outer join" & _
                                          " dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN PurchaseDemandMasterTable PDemand on PDemand.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN CMFAMasterTable On CMFAMasterTable.DocId = Recv.RefCMFADocId LEFT OUTER JOIN(Select DISTINCT PurchaseOrderId From PurchaseOrderDetailTable) as Location On Location.PurchaseOrderId = Recv.PurchaseOrderId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseOrderNo  LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.PurchaseOrderId WHERE Recv.PurchaseOrderNo IS NOT NULL " & _
                                          " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseOrderDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'Ali FAisal :TFS1442 : End
            If flgCompanyRights = True Then
                str += " AND Recv.LocationId=" & MyCompanyId
            End If
            If Me.dtpFrom.Checked = True Then
                str += " AND Recv.PurchaseOrderDate >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
            End If
            If Me.dtpTo.Checked = True Then
                str += " AND Recv.PurchaseOrderDate <= Convert(Datetime, N'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
            End If
            If Me.txtSearchDocNo.Text <> String.Empty Then
                str += " AND Recv.PurchaseOrderNo LIKE '%" & Me.txtSearchDocNo.Text & "%'"
            End If
            If Not Me.cmbSearchLocation.SelectedIndex = -1 Then
                If Me.cmbSearchLocation.SelectedIndex > 0 Then
                    str += " AND Location.LocationId=" & Me.cmbSearchLocation.SelectedValue
                End If
            End If
            If Me.txtFromAmount.Text <> String.Empty Then
                If Val(Me.txtFromAmount.Text) > 0 Then
                    str += " AND Recv.PurchaseOrderAmount >= " & Val(Me.txtFromAmount.Text) & " "
                End If
            End If
            If Me.txtToAmount.Text <> String.Empty Then
                If Val(Me.txtToAmount.Text) > 0 Then
                    str += " AND Recv.PurchaseOrderAmount <= " & Val(Me.txtToAmount.Text) & ""
                End If
            End If
            If Me.cmbSearchAccount.ActiveRow IsNot Nothing Then
                If Me.cmbSearchAccount.SelectedRow.Cells(0).Value <> 0 Then
                    str += " AND Recv.VendorId = " & Me.cmbSearchAccount.Value
                End If
            End If
            If Me.txtSearchRemarks.Text <> String.Empty Then
                str += " AND Recv.Remarks LIKE '%" & Me.txtSearchRemarks.Text & "%'"
            End If
            If Me.txtPurchaseNo.Text <> String.Empty Then
                str += " AND Recv.PurchaseOrderNo LIKE  '%" & Me.txtPurchaseNo.Text & "%'"
            End If
            If LoadAllRecords = False Then
                If Not LoginGroup = "Administrator" Then
                    str += " AND Recv.UserName LIKE  '%" & LoginUserName & "%'"
                End If
            End If

            str += " ORDER BY Recv.PurchaseOrderNo DESC"
        End If
        Me.CtrlGrdBar2_Load(Nothing, Nothing)
        Me.CtrlGrdBar1_Load(Nothing, Nothing)
        FillGridEx(grdSaved, str, True)
        ' Change on 23-11-2013  For Multiple Print Vouchers
        Me.grdSaved.RootTable.Columns.Add("Column1")
        Me.grdSaved.RootTable.Columns("Column1").UseHeaderSelector = True
        Me.grdSaved.RootTable.Columns("Column1").ActAsSelector = True
        '-----------------------------------------------'
        'grdSaved.Columns(10).Visible = False
        grdSaved.RootTable.Columns("LCId").Visible = False
        grdSaved.RootTable.Columns("LocationId").Visible = False
        Me.grdSaved.RootTable.Columns("CostCenterId").Visible = False
        grdSaved.RootTable.Columns("RefCMFADocId").Visible = False 'Task:2673 Set Hidden Column
        'grdSaved.RootTable.Columns("EmployeeCode").Visible = False
        'grdSaved.RootTable.Columns("PoId").Visible = False

        Me.grdSaved.RootTable.Columns("POStockDispatchStatus").Visible = False
        Me.grdSaved.RootTable.Columns("PayTypeId").Visible = False
        Me.grdSaved.RootTable.Columns("PurchaseOrderId").Visible = False
        Me.grdSaved.RootTable.Columns("VendorId").Visible = False
        Me.grdSaved.RootTable.Columns("Terms").Visible = False

        grdSaved.RootTable.Columns("PurchaseOrderNo").Caption = "Doc No"
        grdSaved.RootTable.Columns("Date").Caption = "Date"
        grdSaved.RootTable.Columns("VendorName").Caption = "Vendor"
        'grdSaved.RootTable.Columns(3).Caption = "S-Order"
        grdSaved.RootTable.Columns("PurchaseOrderQty").Caption = "Qty"
        grdSaved.RootTable.Columns("PurchaseOrderAmount").Caption = "Base Value"
        grdSaved.RootTable.Columns("currency_code").Caption = "Currency"
        grdSaved.RootTable.Columns("AmountUS").Caption = "Original Value"
        grdSaved.RootTable.Columns("Remarks").Caption = "Remarks"
        grdSaved.RootTable.Columns("Status").Caption = "Status"
        grdSaved.RootTable.Columns("LCDoc_No").Caption = "LC No"
        grdSaved.RootTable.Columns("CurrencyType").Visible = False
        grdSaved.RootTable.Columns("CurrencyRate").Visible = False
        grdSaved.RootTable.Columns("Receiving_Date").Visible = False
        grdSaved.RootTable.Columns("Email").Visible = False
        Me.grdSaved.RootTable.Columns("PurchaseDemandId").Visible = False

        'Rafay:the foreign currency is add on purchase history
        Dim grdSaved1 As DataTable = GetDataTable(str)
        grdSaved1.Columns("AmountUS").Expression = "IsNull(PurchaseOrderAmount,0) / (IsNull(CurrencyRate,0))" 'Task:2374 Show Total Amount
        Me.grdSaved.DataSource = grdSaved1
        'Set rounded format
        Me.grdSaved.RootTable.Columns("AmountUS").FormatString = "N" & DecimalPointInValue
        'rafay


        'grdSaved.RootTable.Columns(8).HeaderText = "Employee"
        'Dim grdSaved1 As DataTable = GetDataTable(str)
        'grdSaved1.Columns("AmountUS").Expression = "IsNull(PurchaseOrderAmount,0) / (IsNull(CurrencyRate,0))" 'Task:2374 Show Total Amount
        'Me.grdSaved.DataSource = grdSaved1
        ''Set rounded format
        'Me.grdSaved.RootTable.Columns("AmountUS").FormatString = "N" & DecimalPointInValue
        ''rafay

        grdSaved.RootTable.Columns(0).Width = 100
        grdSaved.RootTable.Columns(1).Width = 100
        grdSaved.RootTable.Columns(2).Width = 250
        grdSaved.RootTable.Columns(3).Width = 50
        grdSaved.RootTable.Columns(5).Width = 80
        ' grdSaved.RootTable.Columns(8).Width = 100
        grdSaved.RootTable.Columns(4).Width = 150
        'grdSaved.RowHeadersVisible = False
        Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat

        'Task:2759 Set rounded amount format
        Me.grdSaved.RootTable.Columns("PurchaseOrderAmount").FormatString = "N" & DecimalPointInValue
        'End Task
        Me.grdSaved.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link

    End Sub

    Public Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        If Validate_AddToGrid() Then
            AddItemToGrid()
            GetTotal()
            'GetDiscountedPrice()
            ClearDetailControls()
            cmbItem.Focus()
            'FillCombo("Item")
        End If
    End Sub
    Private Sub RefreshControls()
        'Task#118062015 Try catch missing
        Try
            'Task 1592 Ayesha Rehman Removing the ErrorProvider on btnNew
            ErrorProvider1.Clear()
            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''END TFS4544
            FillCombo("Item")
            IsEditMode = False
            txtPONo.Text = ""
            dtpPODate.Value = Now
            Me.dtpPODate.Enabled = True
            txtRemarks.Text = ""
            txtPaid.Text = ""
            'Rafay:TAsk Start
            companyinitials = ""
            'rafay:task End
            Me.txtRate.Text = ""
            Me.txtPackRate.Text = ""
            'txtAmount.Text = ""
            'txtTotal.Text = "" 'Before ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            Me.txtTotal.Text = 0 'After ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            'txtTotalQty.Text = ""
            txtBalance.Text = ""
            txtPackQty.Text = 1
            Me.txtQty.Text = 1 ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            Me.txtRate.Text = 0 ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            Me.BtnSave.Text = "&Save"
            Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("PO", 6, "PurchaseOrderMasterTable", "PurchaseOrderNo")
            Me.cmbPo.Enabled = True
            'FillCombo("SO") 'R933 Commented
            'FillCombo("LC") R933 Commented
            cmbUnit.SelectedIndex = 0
            cmbStatus.SelectedIndex = 0
            txtWarranty.Text = 1
            cmbVendor.Rows(0).Activate()

            cmbItem.Rows(0).Activate()
            Me.cmbPurchaseDemand.Rows(0).Activate()
            Me.cmbLC.Rows(0).Activate()
            'Array.Clear(arrFile, 0, arrFile.Length)
            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari
            'Clear arrfile
            arrFile = New List(Of String)
            'Altered Against Task#2015060001 Ali Ansari
            DisplayDetail(-1)
            FillInwardExpense(-1, "PO")
            'grd.Rows.Clear()
            'Me.cmbVendor.Focus() comment against request no. RM6
            Me.GetSecurityRights()

            'Ayesha Rehman : TFS2375 : Enable Approval History button only in Eidt Mode
            If IsEditMode = True Then
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            Else
                Me.btnApprovalHistory.Visible = False
            End If
            'Ayesha Rehman : TFS2375 : End

            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
            If Not getConfigValueByType("PurchaseOrderApproval") = "Error" Then
                ApprovalProcessId = getConfigValueByType("PurchaseOrderApproval")
            Else
                ApprovalProcessId = 0
            End If
            If ApprovalProcessId = 0 Then
                Me.chkPost.Visible = True
                Me.chkPost.Enabled = True

            Else
                Me.chkPost.Visible = False
                Me.chkPost.Enabled = False
                Me.chkPost.Checked = False
            End If
            ''Ayesha Rehman :TFS2375 :End

            'FillComboByEdit() R933 Commented
            Me.dtpFrom.Value = Date.Now.AddMonths(-1)
            Me.dtpTo.Value = Date.Now
            Me.dtpFrom.Checked = False
            Me.dtpTo.Checked = False
            Me.txtSearchDocNo.Text = String.Empty
            'Me.cmbSearchLocation.SelectedIndex = 0
            Me.txtFromAmount.Text = String.Empty
            Me.txtToAmount.Text = String.Empty
            Me.txtPurchaseNo.Text = String.Empty
            ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            ' Before Me.txtTaxPercent.Text = String.Empty
            ' After
            'Rafay
            Me.txtTax.Text = ""
            '''''''''''''''''''''''''''''''''''''''''''''''
            'Me.cmbSearchAccount.Rows(0).Activate()
            Me.txtSearchRemarks.Text = String.Empty
            Me.SplitContainer1.Panel1Collapsed = True
            'DisplayRecord()
            Me.lblPrintStatus.Text = String.Empty
            FillCombo("CMFADoc")
            FillCombo("Demand")
            Me.txtCMFAAmount.Text = String.Empty
            Me.txtPOAmountAgainstCMFA.Text = String.Empty
            Me.txtCMFADiff.Text = String.Empty
            taxamnt = 0D
            'rafay
            FillCombo("Status")
            FillCombo("Currency")
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
            Me.cmbCurrency.Enabled = True
            Me.txtCurrencyRate.Enabled = False
            'Me.cmbCurrency.SelectedIndex = 0
            'Me.txtCurrencyRate.Text = String.Empty
            'If flgCurrenyonOpenLC = True Then
            '    Me.grpCurrency.Visible = True
            '    If Not Me.cmbCurrency.SelectedIndex = -1 Then Me.cmbCurrency.SelectedIndex = 1 ''19-Dec-2013 R:945 , TaskId:2338        M Ijaz Javed           By default pack rupees
            '    Me.txtCurrencyRate.Text = 1     ''19-Dec-2013 R:945 , TaskId:2338        M Ijaz Javed           By default pack rupees
            'Else
            '    Me.grpCurrency.Visible = False
            'End If
            Me.txtTerms_And_Condition.Text = GetTermsCondition()
            'Clear Attached file records
            arrFile = New List(Of String)
            Me.btnAttachment.Text = "Attachment (" & arrFile.Count & ")"
            'Altered Against Task#2015060001 Ali Ansri
            'Array.Clear(arrFile, 0, arrFile.Length)

            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnDelete.Visible = False
            Me.BtnPrint.Visible = False
            Me.BtnEdit.Visible = False
            '''''''''''''''''''''''''''

            Me.btnImports.Visible = False       'Task#117062015 invisible import button

            Me.cmbPOType.SelectedIndex = 0                  'Task#1-16062015 reset to "Local PO" by default
            Me.cmbStockDispatchStatus.Rows(0).Activate()    'Task#1-16062015 reset cmbStockDispatchStatus

            Me.dtpPODate.Focus() 'RM6 Set Focus
            PurchaseDemandID = 0I
            IsSOLoad = False
            'Rai HAIDER:TFS1315:11-Aug-2017 : Curent Rate,Discount & Project On PO reset on Save & New Button 
            'Start Task
            Me.txtCurrentRate.Text = String.Empty
            Me.txtDisc.Text = String.Empty
            Me.cmbProject.SelectedIndex = 0
            ''End Task
            If getConfigValueByType("VisiblerateonPO") = "True" Then
                Me.txtRate.Enabled = True
            Else
                Me.txtRate.Enabled = False
            End If
            If Not Me.cmbPaymentTypes.SelectedIndex = -1 Then
                Me.cmbPaymentTypes.SelectedIndex = 0

                Me.CtrlGrdBar2_Load(Nothing, Nothing)
                Me.CtrlGrdBar1_Load(Nothing, Nothing)

            End If
            ''TFS4354: Aashir :Set text to save as template
            SaveAsTemplateToolStripMenuItem.Text = "Save as &Template"
            SaveAsTemplateToolStripMenuItem.Tag = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
        'End Task#118062015

    End Sub
    '' ReqId-899 Reset TaxPercent TextBox
    Private Sub ClearDetailControls()
        'cmbCategory.SelectedIndex = 0
        cmbUnit.SelectedIndex = 0
        cmbStatus.SelectedIndex = 0
        txtWarranty.Text = 1
        txtQty.Text = 0
        txtRate.Text = ""
        Me.txtNetTotal.Text = 0  ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        'Me.txtTaxPercent.Text = String.Empty ' Before ReqId-954
        'Rafay
        Me.txtTax.Text = "" ' After ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        txtTotal.Text = 0
        txtPackQty.Text = 1
        Me.txtPackRate.Text = String.Empty
        Me.txtDisc.Text = String.Empty
        Me.txtTotalQuantity.Text = String.Empty
        Me.txtCurrentRate.Text = String.Empty     'Rai HAIDER:TFS1315:11-Aug-2017 : Curent Rate,Rate & Discount (Text Boxes) reset on Add to Grid Button 'Start Task
        Me.txtRate.Text = String.Empty
        Me.txtDisc.Text = String.Empty            'End Task
    End Sub
    Private Function Validate_AddToGrid() As Boolean

        If Me.cmbItem.IsItemInList = False Then
            msg_Error("Item not found")
            Me.cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/26/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/26/2022)
        If Me.cmbItem.ActiveRow Is Nothing Then
            msg_Error("Invalide item")
            Me.cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If cmbItem.ActiveRow.Cells(0).Value <= 0 Then
            msg_Error("Please select an item")
            cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtQty.Text) <= 0 Then
            msg_Error("Quantity is not greater than 0")
            txtQty.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtRate.Text) <= 0 Then
            msg_Error("Rate is not greater than 0")
            txtRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        If Val(txtTotalQuantity.Text) <= 0 Then
            msg_Error("Total Quantity should be larger than 0")
            txtTotalQuantity.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'Rafay :task Start
        If txtTax.Text = "" Then
            msg_Error("Please select tax value ")
            txtTax.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'Rafay:task end

        Validate_AddToGrid = True
    End Function

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress, txtRate.KeyPress, txtToAmount.KeyPress, txtTotal.KeyPress, txtTax.KeyPress, txtPackRate.KeyPress, txtPackQty.KeyPress, txtWarranty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus, txtWarranty.LostFocus
        Try
            'Task:2367 Change Total
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
            '    txtNetTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTaxPercent.Text)) / 100)
            'Else
            '    txtNetTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTaxPercent.Text)) / 100)
            'End If
            'End Task:2367
            'If IsSalesOrderAnalysis = True Then
            '    If Val(Me.txtDisc.Text) <> 0 Then
            '        Me.txtDisc.TabStop = True
            '    End If
            'End If

            GetDetailTotal()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged, txtWarranty.TextChanged
        Try
            'If Me.txtPackRate.Text.Length > 0 AndAlso (Me.txtPackRate.Text) > 0 Then
            '    If Me.cmbUnit.Text <> "Loose" Then
            '        Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
            '    End If
            'End If
            If Val(Me.txtPackQty.Text) > 0 Then
                Me.txtTotalQuantity.Text = Math.Round(Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text), TotalAmountRounding)
            Else
                Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRate.LostFocus
        Try


            'Task:2367 Change Total

            'If Val(Me.txtRate.Text) > 0 Then
            '    If Val(Me.txtCurrentRate.Text) > 0 Then
            '        If Val(Me.txtDiscPercent.Text) > 0 Then
            '            If Val(Me.txtCurrentRate.Text) > Val(Me.txtRate.Text) Then
            '                Me.txtDiscPercent.Text = ((Val(Me.txtCurrentRate.Text) - Val(Me.txtRate.Text)) / Val(Me.txtCurrentRate.Text)) * 100
            '            Else
            '                Me.txtDiscPercent.Text = 0
            '            End If
            '        End If
            '    End If
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
            'End Task:2367
            Me.GetDetailTotal()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged, cmbStatus.SelectedIndexChanged
        ''get the qty in case of pack unit
        If Me.cmbUnit.Text = "Loose" Then
            txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), TotalAmountRounding)
            txtPackQty.Text = 1
            Me.txtPackQty.Enabled = False
            Me.txtPackQty.TabStop = False
            Me.txtTotalQuantity.Enabled = False
            Me.txtPackRate.Enabled = False
        Else
            ''Start TSF4161
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
            'Dim objCommand As New OleDbCommand
            'Dim objCon As OleDbConnection
            'Dim objDataAdapter As New OleDbDataAdapter
            'Dim objDataSet As New DataSet

            'objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

            'If objCon.State = ConnectionState.Open Then objCon.Close()

            'objCon.Open()
            'objCommand.Connection = objCon
            'objCommand.CommandType = CommandType.Text


            'objCommand.CommandText = "Select PackQty from ArticleDefTable where ArticleID = " & cmbItem.ActiveRow.Cells(0).Value

            'txtPackQty.Text = objCommand.ExecuteScalar()
            If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
                Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
            End If
            txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)), TotalAmountRounding)

        End If
        Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))

    End Sub
    '' ReqId-899 Added new field of TaxPercent and TaxAmount
    Public Sub AddItemToGrid()
        Try

            Dim dtGrd As DataTable
            dtGrd = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtGrd.NewRow
            'drGrd.Item(GrdEnum.Category) = Me.cmbCategory.Text
            drGrd.Item(GrdEnum.SerialNo) = ""
            drGrd.Item(GrdEnum.LocationId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
            drGrd.Item(GrdEnum.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
            drGrd.Item(GrdEnum.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
            'TASK24 Add to grid this new fields
            drGrd.Item(GrdEnum.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Text.ToString
            drGrd.Item(GrdEnum.Size) = Me.cmbItem.ActiveRow.Cells("Size").Text.ToString
            drGrd.Item(GrdEnum.UOM) = Me.cmbItem.ActiveRow.Cells("UOM").Text.ToString
            'End TASK24
            drGrd.Item(GrdEnum.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            drGrd.Item(GrdEnum.Warranty) = Val(txtWarranty.Text)
            drGrd.Item(GrdEnum.Status) = Me.cmbStatus.Text.ToString
            drGrd.Item(GrdEnum.Qty) = Val(Me.txtQty.Text)
            drGrd.Item(GrdEnum.Rate) = Val(Me.txtRate.Text)
            ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            ' Before 
            'drGrd.Item(GrdEnum.Total) = Me.txtTotal.Text
            'After
            drGrd.Item(GrdEnum.Total) = Val(Me.txtNetTotal.Text)
            ''''''''''''''''''''''''''''''
            drGrd.Item(GrdEnum.TaxPercent) = Val(Me.txtTax.Text)
            drGrd.Item(GrdEnum.TaxAmount) = Val(taxamnt)
            drGrd.Item(GrdEnum.CategoryId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
            drGrd.Item(GrdEnum.ItemId) = Me.cmbItem.ActiveRow.Cells(0).Value
            drGrd.Item(GrdEnum.PackQty) = Val(Me.txtPackQty.Text)
            drGrd.Item(GrdEnum.CurrentPrice) = Val(Me.txtCurrentRate.Text) 'Me.cmbItem.ActiveRow.Cells("Price").Text
            drGrd.Item(GrdEnum.RateDiscPercent) = Val(Me.txtDisc.Text)
            drGrd.Item(GrdEnum.PackPrice) = Val(Me.txtPackRate.Text)
            drGrd.Item("Pack_Desc") = Me.cmbUnit.Text.ToString
            drGrd.Item(GrdEnum.PurchaseDemandId) = PurchaseDemandID
            drGrd.Item(GrdEnum.PurchaseDemandDetailId) = PurchaseDemandDetailId
            ''ReqId-928 Added Field of Comments
            drGrd.Item(GrdEnum.Comments) = strComments
            ''TASK-408
            drGrd.Item(GrdEnum.TotalQty) = Val(Me.txtTotalQuantity.Text)
            ''END TASK-408
            '' End ReqId-928
            If Me.cmbPurchaseDemand.SelectedRow.Index > 0 Then drGrd.Item(GrdEnum.PurchaseDemandId) = Me.cmbPurchaseDemand.Value
            'dtGrd.Rows.InsertAt(drGrd, 0)

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

            dtGrd.Rows.Add(drGrd)
            dtGrd.AcceptChanges()
            taxamnt = 0D

            'Task 3913 Saad Afzaal move scroll bar at the end when item added into the grid 
            grd.MoveLast()

            'grd.Rows.Add(cmbCategory.Text, Me.cmbCategory.SelectedValue, cmbItem.Text, cmbUnit.Text, txtQty.Text, txtRate.Text, Val(txtTotal.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value)

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


        Catch ex As Exception
            Throw ex
        End Try


    End Sub
    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged
        'If cmbCategory.SelectedIndex > 0 Then
        '    FillCombo("ItemFilter")
        'End If
        Try
            If IsFormLoaded = True Then FillCombo("Item")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetTotal()
        Try
            'Dim i As Integer
            'Dim dblTotalAmount As Double
            'Dim dblTotalQty As Double
            ''For i = 0 To grd.Rows.Count - 1
            'For i = 0 To grd.RowCount - 1
            '    dblTotalAmount = dblTotalAmount + Val(grd.GetRows(i).Cells(6).Value)
            '    dblTotalQty = dblTotalQty + Val(grd.GetRows(i).Cells(4).Value)
            'Next
            'txtAmount.Text = dblTotalAmount
            'txtTotalQty.Text = dblTotalQty
            'txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)
            'Me.lblRecordCount.Text = "Total Records: " & Me.grd.RowCount

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetDiscountedPrice()
        Try

            Me.grd.UpdateData()
            Dim dblCurrentRate As Double = 0D
            Dim dblDiscPercent As Double = 0D
            Dim dblActualPrice As Double = 0D

            dblCurrentRate = Val(Me.grd.GetRow.Cells(GrdEnum.CurrentPrice).Value.ToString)
            dblDiscPercent = Val(Me.grd.GetRow.Cells(GrdEnum.RateDiscPercent).Value.ToString)
            If dblDiscPercent > 0 Then
                If dblCurrentRate > 0 Then
                    dblActualPrice = Val(dblDiscPercent / 100) * dblCurrentRate
                    Me.grd.GetRow.Cells(GrdEnum.Rate).Value = dblCurrentRate - dblActualPrice
                Else
                    dblActualPrice = Val(Me.grd.GetRow.Cells(GrdEnum.Rate).Value.ToString)
                    Me.grd.GetRow.Cells(GrdEnum.Rate).Value = dblActualPrice
                    Me.grd.GetRow.Cells(GrdEnum.RateDiscPercent).Value = 0
                End If
            Else
                If Val(Me.grd.GetRow.Cells(GrdEnum.CurrentPrice).Value.ToString) > Val(Me.grd.GetRow.Cells(GrdEnum.Rate).Value.ToString) Then
                    dblActualPrice = Val(Me.grd.GetRow.Cells(GrdEnum.CurrentPrice).Value.ToString)
                    Me.grd.GetRow.Cells(GrdEnum.Rate).Value = dblActualPrice
                Else
                    dblActualPrice = Val(Me.grd.GetRow.Cells(GrdEnum.Rate).Value.ToString)
                    Me.grd.GetRow.Cells(GrdEnum.Rate).Value = dblActualPrice
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillCombo(ByVal strCondition As String)
        Dim str As String
        If strCondition = "Item" Then
            'Marked Against Task# 7 Ali Ansari 
            'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ISNULL(PurchasePrice,0) as Price, ArticleDefView.SortOrder FROM  ArticleDefView where Active=1 "
            'Marked Against Task# 7 Ali Ansari 
            'Changes Against Task# 7 Ali Ansari Add PackQty in Selection 2015-02-20
            'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,PackQty as PackQty, ISNULL(PurchasePrice,0) as Price, ArticleDefView.SortOrder FROM  ArticleDefView where Active=1 "
            str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleColorName as Combination,ArticleUnitName as UOM,ArticleSizeName as Size,ArticleDefView.ArticleBrandName As Grade, PackQty as PackQty, ISNULL(PurchasePrice,0) as Price, ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model, ArticleDefView.SortOrder FROM  ArticleDefView where Active=1 "

            'Changes Against Task# 7 Ali Ansari Add PackQty in Selection 2015-02-20
            If flgCompanyRights = True Then
                str += " AND ArticleDefView.CompanyId=" & MyCompanyId
            End If
            If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                    str += " AND ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                    'Else
                    '    str += " ORDER BY ArticleDefView.SortOrder" Comment against task:2452
                End If
            End If

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
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                If ItemFilterByName = True Then
                    Me.rdoName.Checked = True
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

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

            FillDropDown(cmbCategory, str, False)

        ElseIf strCondition = "SearchLocation" Then
            str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            FillDropDown(Me.cmbSearchLocation, str, True)

        ElseIf strCondition = "ItemFilter" Then
            str = "SELECT  ArticleId as Id,  ArticleCode Code, ArticleDescription Item, ArticleColorName as Combination,ArticleSizeName as Size,  ArticleUnitName as UOM, PurchasePrice as Price FROM         ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue & ""
            If flgCompanyRights = True Then
                str += " AND ArticleDefView.CompanyId=" & MyCompanyId
            End If
            FillUltraDropDown(cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
        ElseIf strCondition = "Vendor" Then
            'str = "SELECT     tblVendor.AccountId AS ID, tblVendor.VendorName AS Name, tblListTerritory.TerritoryName AS Territory, tblListCity.CityName AS City,  " & _
            '        "tblListState.StateName AS State, tblVendor.AccountId AS AcId " & _
            '        "FROM         tblListTerritory INNER JOIN " & _
            '        "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
            '        "tblListState ON tblListCity.StateId = tblListState.StateId INNER JOIN " & _
            '        "tblVendor ON tblListTerritory.TerritoryId = tblVendor.Territory"
            'Before against task:2373
            'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
            '                    "dbo.tblListTerritory.TerritoryName as Territory, tblVendor.Email, tblVendor.Phone " & _
            '                    "FROM dbo.tblVendor INNER JOIN " & _
            '                    "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '                    "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '                    "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                    "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                    "WHERE (dbo.vwCOADetail.account_type = 'Vendor') "
            'Before gainst task:2637
            'Task:2373 Added Column Sub Sub Title
            'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
            '                 "dbo.tblListTerritory.TerritoryName as Territory, tblVendor.Email, tblVendor.Phone, vwCOADetail.Sub_Sub_Title " & _
            '                 "FROM dbo.tblVendor INNER JOIN " & _
            '                 "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '                 "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '                 "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                 "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                 "WHERE (dbo.vwCOADetail.account_type = 'Vendor') "
            'End Task:2373
            ''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                          "dbo.tblListTerritory.TerritoryName as Territory, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                          "FROM dbo.tblVendor INNER JOIN " & _
                          "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                          "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                          "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                          "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id "
            If getConfigValueByType("Show Customer On Purchase") = "True" Then
                str += "WHERE  (dbo.vwCOADetail.account_type in('Vendor','Customer')) "
            Else
                str += "WHERE  (dbo.vwCOADetail.account_type = 'Vendor') "
            End If
            'End Task:2637
            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            'If LoginGroup = "Administrator" Then
            If GetMappedUserId() > 0 And getGroupAccountsConfigforPurchase(Me.Name) And LoginGroup <> "Administrator" Then ''TFS4689
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                     "dbo.tblListTerritory.TerritoryName as Territory, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                     "FROM dbo.tblVendor INNER JOIN " & _
                     "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                     "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                     "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                     "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id WHERE  (dbo.vwCOADetail.detail_title Is Not NULL ) "
                str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ")) "
                ''Start TFS4689 :  Ayesha Rehman : 02-10-2018
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    str += " And vwCOADetail.account_type in ('Customer' , 'Vendor') "
                Else
                    str += " And vwCOADetail.account_type = 'Vendor' "
                End If
                ''End TFS4689
            End If
            ''End TFS3322
            If IsEditMode = False Then
                str += " AND vwCOADetail.Active=1"
            Else
                str += " AND vwCOADetail.Active in(0,1,Null)"
            End If
            str += " order by tblVendor.Sortorder, vwCOADetail.detail_title "
            FillUltraDropDown(cmbVendor, str)
            If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" 'Task:2373 Change Caption
            End If
        ElseIf strCondition = "SearchVendor" Then
            'Before against task:2637
            'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
            '                               "dbo.tblListTerritory.TerritoryName as Territory, tblVendor.Email, tblVendor.Phone " & _
            '                               "FROM  dbo.tblVendor INNER JOIN " & _
            '                               "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '                               "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '                               "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                               "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                               "WHERE  (dbo.vwCOADetail.account_type = 'Vendor') AND dbo.vwCOADetail.detail_title Is Not NULL "
            ''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                                       "dbo.tblListTerritory.TerritoryName as Territory, dbo.vwCOADetail.Contact_Email as Email, dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile,vwCOADetail.Sub_Sub_Title " & _
                                                       "FROM  dbo.tblVendor INNER JOIN " & _
                                                       "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                                       "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                                       "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                                       "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id "
            If Not getConfigValueByType("Show Customer On Purchase") = "True" Then
                str += "WHERE  (dbo.vwCOADetail.account_type = 'Vendor') AND dbo.vwCOADetail.detail_title Is Not NULL "
            Else
                str += "WHERE  (dbo.vwCOADetail.account_type IN ('Vendor','Customer')) AND dbo.vwCOADetail.detail_title Is Not NULL "
            End If
            'End Task:2637
            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            str += " order by tblVendor.Sortorder, vwCOADetail.detail_title"
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
        ElseIf strCondition = "SM" Then
            str = "Select EmployeeID, EmployeeName  + ' - ' + employeeCode as EmployeeName from EmployeeDefTable"
            FillDropDown(Me.cmbSalesMan, str)
        ElseIf strCondition = "grdLocation" Then
            'Task#16092015 Load Locations user wise
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Name From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")"
            'Else
            '    str = "Select Location_Id, Location_Name From tblDefLocation"
            'End If

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns(GrdEnum.LocationId).ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
        ElseIf strCondition = "LC" Then
            '30-Aug-17:Rai Haider:TFS1392 :LC always Enable on PO Screen
            'If IsEditMode = True Then
            str = "Select LCdoc_Id, LCdoc_No, LCdoc_Date, Bank, LCdoc_Type,CostCenter From tblLetterOfCredit WHERE Active=1"
            'Else
            'str = "Select LCdoc_Id, LCdoc_No, LCdoc_Date, Bank, LCdoc_Type,CostCenter From tblLetterOfCredit WHERE Active=1 " & IIf(Me.cmbVendor.ActiveRow Is Nothing, "", " AND VendorId=" & Me.cmbVendor.Value & "") & ""
            'End If
            FillUltraDropDown(Me.cmbLC, str, True)
            If Me.cmbLC.DisplayLayout.Bands.Count > 0 Then
                Me.cmbLC.DisplayLayout.Bands(0).Columns("LCdoc_Id").Hidden = True
                Me.cmbLC.DisplayLayout.Bands(0).Columns("CostCenter").Hidden = True
                Me.cmbLC.DisplayLayout.Bands(0).Columns("LCdoc_No").Header.Caption = "Doc No"
                Me.cmbLC.DisplayLayout.Bands(0).Columns("LCdoc_Date").Header.Caption = "Do Date"
                Me.cmbLC.DisplayLayout.Bands(0).Columns("LCdoc_Type").Header.Caption = "Type"
            End If
            'End Task :TFS1392
        ElseIf strCondition = "Currency" Then
            str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
            FillDropDown(Me.cmbCurrency, str, False)
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
            'rafay:Fill combo box status
        ElseIf strCondition = "Status" Then
            str = "Select Id,Status from tblStatus"
            FillDropDown(Me.cmbStatus, str, False)
            'Fillcombo in grd 
        ElseIf strCondition = "GrdStatus" Then
            str = "select Id, Status from tblStatus"
            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.grd.RootTable.Columns("Status").ValueList.PopulateValueList(dt.DefaultView, "Id", "Status")

            'rafay End
        ElseIf strCondition = "ArticlePack" Then
            Me.cmbUnit.ValueMember = "ArticlePackId"
            Me.cmbUnit.DisplayMember = "PackName"
            Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
            ''TASK:2673 Fill Dropdown for CMFA Document
        ElseIf strCondition = "CMFADoc" Then
            Dim strQuery As String = String.Empty
            'Before against task:2856
            'strQuery = "SELECT DISTINCT CMFA.DocId, CMFA.DocNo + '~' + CONVERT(Varchar, CONVERT(Varchar, CMFA.DocDate, 102)) AS DocNo, CMFADT.VendorId, CMFA.DocDate, COA.detail_code,COA.detail_title FROM dbo.CMFAMasterTable AS CMFA INNER JOIN dbo.CMFADetailTable AS CMFADT ON CMFA.DocId = CMFADT.DocId LEFT OUTER JOIN dbo.vwCOADetail AS COA ON CMFADT.VendorId = COA.coa_detail_id WHERE IsNull(Status,0)=1 AND IsNull(Approved,0)=1 AND CMFADT.VendorId=" & Me.cmbVendor.Value & ""
            'End Task:2673
            'Task:2856 changed query 
            strQuery = "SELECT CMFA.DocId, CMFA.DocNo + '~' + CONVERT(Varchar, CONVERT(Varchar, CMFA.DocDate, 102)) AS DocNo, IsNull(PO.POAmount,0) as POAmount, (IsNull(TotalAmount,0)+IsNull(Tax_Amount,0))  as CMFA_Amount FROM dbo.CMFAMasterTable  AS CMFA LEFT OUTER JOIN(Select DocId, SUM( (IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) as Tax_Amount From CMFADetailTable Group By DocId) CMFA_Dt On CMFA_Dt.DocId = CMFA.DocId LEFT OUTER JOIN (Select IsNull(RefCMFADocId,0) as RefCMFADocId, SUM((IsNull(Qty,0)* IsNull(Price,0))+((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)* IsNull(Price,0)))) as [POAmount] From PurchaseOrderDetailTable PO_DT INNER JOIN PurchaseOrderMasterTable PO On PO.PurchaseOrderId = PO_DT.PurchaseOrderId WHERE RefCMFADocId <> 0 AND PO.PurchaseOrderId <> " & Val(Me.txtReceivingID.Text) & " Group By IsNull(RefCMFADocId,0)) PO On PO.RefCMFADocId = CMFA.DocId  WHERE IsNull(Approved,0)=1"
            'Task:2856
            FillDropDown(Me.cmbCMFADoc, strQuery)
        ElseIf strCondition = "CostCenter" Then
            FillDropDown(Me.cmbProject, "Select * From tblDefCostCenter ORDER BY Name ASC")
        ElseIf strCondition = "Demand" Then
            'FillDropDown(Me.cmbPurchaseDemand, "Select PurchaseDemandId, PurchaseDemandNo From PurchaseDemandMasterTable WHERE Status <> 'Close'")

            'Filling            'Task#1-16062015 END  Stock dispatch status in combox
            'TASK15 Filter Posted Purchase Demand.
            'FillDropDown(Me.cmbPurchaseDemand, "Select PurchaseDemandId, PurchaseDemandNo, IsNull(VendorId,0) as VendorId, Remarks, PurchaseDemandDate  From PurchaseDemandMasterTable WHERE Status <> 'Close' AND IsNull(Post,0)=1 AND VendorID=" & Me.cmbVendor.Value & " Order BY PurchaseDemandNo DESC")
            'End TASK15
            FillUltraDropDown(Me.cmbPurchaseDemand, "Select PurchaseDemandId, PurchaseDemandNo, IsNull(VendorId,0) as VendorId, Remarks, PurchaseDemandDate, IsNull(CostCenterId, 0) As CostCenterId  From PurchaseDemandMasterTable WHERE Status <> 'Close' AND IsNull(Post,0)=1 Order BY PurchaseDemandNo DESC")
        ElseIf strCondition = "StockDispatchStatus" Then
            FillUltraDropDown(Me.cmbStockDispatchStatus, "Select StockDispatchStatusID,StockDispatchStatusName from tblStockDispatchStatus")
            Me.cmbStockDispatchStatus.Rows(0).Activate()        'Active first value 
            Me.cmbStockDispatchStatus.DisplayLayout.Bands(0).Columns("StockDispatchStatusID").Hidden = True     'Hiding the "StockDispatchStatusID" Column
            Me.cmbStockDispatchStatus.DisplayLayout.Bands(0).Columns("StockDispatchStatusName").Header.Caption = "Stock Dispatch Status"    'Setting the Header Caption of "StockDispatchStatusName" column in combo box
            'End Task#1-16062015
        ElseIf strCondition = "InwardExpense" Then
            FillUltraDropDown(Me.cmbInwardExpense, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], sub_sub_title as [Account Head], Account_Type as [Account Type] From vwCOADetail where detail_title <> ''")
            Me.cmbInwardExpense.Rows(0).Activate()
            If Me.cmbInwardExpense.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbInwardExpense.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
        ElseIf strCondition = "TermsCondition" Then
            FillDropDown(Me.cmbTermCondition, "select * From tblTermsAndConditionType", True)
            'Ali FAisal :TFS1442 : Fill drop down for Payment Terms
        ElseIf strCondition = "PaymentTerms" Then
            FillDropDown(Me.cmbPaymentTypes, "SELECT Id, TermName FROM tblPaymentTerms WHERE Active = 1", True)
            'Ali FAisal :TFS1442 : End
        End If
    End Sub
    Private Sub txtPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaid.TextChanged
        Try
            txtBalance.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(txtPaid.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''ReqId-899 Added new field TaxPercent, TaxAmount
    Private Function Save() As Boolean
        'R:913
        If Me.chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        Me.grd.UpdateData()
        'End R:913
        'Task:2673 Validation CMFA Document
        Dim strCMFA As String = String.Empty
        Dim objDt As New DataTable
        If Me.cmbCMFADoc.SelectedIndex > 0 Then
            strCMFA = "Select CMFADetailTable.ArticleDefId, SUM(IsNull(Sz1,0)) as Qty, SUM(IsNull(POQty,0)) as PoQty From CMFADetailTable WHERE DocId=" & Me.cmbCMFADoc.SelectedValue & " Group By CMFADetailTable.ArticleDefId Having SUM(IsNull(Sz1,0)-IsNull(POQty,0)) <> 0"
            objDt = GetDataTable(strCMFA)
        End If
        'End Task:2673
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("PO", 6, "PurchaseOrderMasterTable", "PurchaseOrderNo")
        setVoucherNo = Me.txtPONo.Text
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        gobjLocationId = MyCompanyId
        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'Before against request no. 913
            'objCommand.CommandText = "Insert into PurchaseOrderMasterTable (LocationId, PurchaseOrderNo,PurchaseOrderDate,VendorId,PurchaseOrderQty,PurchaseOrderAmount, CashPaid, Remarks,UserName, Status, LCID, CurrencyType, CurrencyRate, Receiving_Date, Terms_And_Condition) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.ToString.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & Val(Me.cmbLC.Value) & ", " & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", " & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "') Select @@Identity"
            'R:913 Added Column Post
            objCommand.CommandText = ""
            ' Before Against Task:2673 
            'objCommand.CommandText = "Insert into PurchaseOrderMasterTable (LocationId, PurchaseOrderNo,PurchaseOrderDate,VendorId,PurchaseOrderQty,PurchaseOrderAmount, CashPaid, Remarks,UserName, Status, LCID, CurrencyType, CurrencyRate, Receiving_Date, Terms_And_Condition,Post) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.ToString.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & Val(Me.cmbLC.Value) & ", " & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", " & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ") Select @@Identity"
            'End R:913
            'Task:2673 Added Field RefCMFADocId
            'objCommand.CommandText = "Insert into PurchaseOrderMasterTable (LocationId, PurchaseOrderNo,PurchaseOrderDate,VendorId,PurchaseOrderQty,PurchaseOrderAmount, CashPaid, Remarks,UserName, Status, LCID, CurrencyType, CurrencyRate, Receiving_Date, Terms_And_Condition,Post, RefCMFADocId,CostCenterId,PurchaseDemandId) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.ToString.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & Val(Me.cmbLC.Value) & ", " & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", " & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ", " & IIf(Me.cmbCMFADoc.SelectedIndex = -1, 0, Me.cmbCMFADoc.SelectedValue) & "," & Me.cmbProject.SelectedValue & "," & Me.cmbPurchaseDemand.SelectedValue & ") Select @@Identity"
            'getVoucher_Id = objCommand.ExecuteScalar 'objCommand.ExecuteNonQuery()
            'End Task:2673
            'objCommand.CommandText = ""

            'Task#117062015 add two columns POType and POStockDispatchStatus in insert query
            'Ali Faisal : TFS1300 : Add new column of Inward Expenses Charges 
            objCommand.CommandText = "Insert into PurchaseOrderMasterTable (LocationId, PurchaseOrderNo,PurchaseOrderDate,VendorId,PurchaseOrderQty,PurchaseOrderAmount, CashPaid, Remarks,UserName, Status, LCID, CurrencyType, CurrencyRate, Receiving_Date, Terms_And_Condition,Post, RefCMFADocId,CostCenterId,POType,POStockDispatchStatus,TotalInwardExpenses,PurchaseDemandId,PayTypeId) values( " _
                                  & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.ToString.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & Val(Me.cmbLC.Value) & ", " & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", " & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & ReplaceNewLine(Me.txtTerms_And_Condition.Text, False).Replace("'", "''") & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ", " & IIf(Me.cmbCMFADoc.SelectedIndex = -1, 0, Me.cmbCMFADoc.SelectedValue) & "," & Me.cmbProject.SelectedValue & ",N'" & Me.cmbPOType.SelectedItem & "',N'" & Me.cmbStockDispatchStatus.Value & "','" & Me.grdInwardExpDetail.GetTotal(Me.grdInwardExpDetail.RootTable.Columns("Exp_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) & "'," & Me.cmbPurchaseDemand.Value & "," & Me.cmbPaymentTypes.SelectedValue & ") Select @@Identity"

            'Ali Faisal : TFS1300 : End
            'End Task#117062015
            getVoucher_Id = objCommand.ExecuteScalar 'objCommand.ExecuteNonQuery()
            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                SaveDocument(getVoucher_Id, Me.Name, trans)
            End If
            For i = 0 To grd.RowCount - 1
                '' ReqId-928 Added Column Comments in Query
                'Task:2673 Validate If PO Qty Exceeded From CMFA Qty
                'Dim objDR() As DataRow
                'If Me.cmbCMFADoc.SelectedIndex > 0 Then
                '    objDR = objDt.Select("ArticleDefId=" & Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString & "")
                '    If objDR IsNot Nothing Then
                '        If objDR.Length > 0 Then
                '            If Val(objDR(0).ItemArray(1).ToString) < (Val(objDR(0).Item(2).ToString) + Val(Me.grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString)) Then
                '                Throw New Exception(" [" & Me.grd.GetRows(i).Cells(GrdEnum.Item).Value.ToString & "] PO qty exceeded from CMFA qty.")
                '            End If
                '        End If
                '    End If
                'End If
                'End Task:2673
                objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into PurchaseOrderDetailTable (PurchaseOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,PackPrice, Pack_Desc,TaxPercent, Comments) values( " _
                '                        & " ident_current('PurchaseOrderMasterTable'), " & Val(grd.GetRows(i).Cells(GrdEnum.LocationId).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value) & "," & Val(Me.grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & grd.GetRows(i).Cells(GrdEnum.Pack_Desc).Value.ToString.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "') "
                'objCommand.CommandText = "Insert into PurchaseOrderDetailTable (PurchaseOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,PackPrice, Pack_Desc,TaxPercent, Comments,DemandID) values( " _
                '                        & " ident_current('PurchaseOrderMasterTable'), " & Val(grd.GetRows(i).Cells(GrdEnum.LocationId).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value) & "," & Val(Me.grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & grd.GetRows(i).Cells(GrdEnum.Pack_Desc).Value.ToString.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "'," & Val(grd.GetRows(i).Cells("PurchaseDemandId").Value.ToString) & ") "

                'TASK-TFS-51 Added Fields AdTax_Percent, AdTax_Amount
                objCommand.CommandText = "Insert into PurchaseOrderDetailTable (PurchaseOrderId, LocationId, ArticleDefId,ArticleSize,Warranty,Status, Sz1,Qty,Price,Sz7,CurrentPrice,PackPrice, Pack_Desc,TaxPercent, Comments,DemandID, AdTax_Percent, AdTax_Amount, PurchaseDemandDetailId, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, SerialNo) values( " _
                                    & " ident_current('PurchaseOrderMasterTable'), " & Val(grd.GetRows(i).Cells(GrdEnum.LocationId).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value) & "',N'" & (grd.GetRows(i).Cells("Warranty").Value) & "',N'" & (grd.GetRows(i).Cells("Status").Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", " _
                                    & " " & Val(grd.GetRows(i).Cells(GrdEnum.TotalQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value) & "," & Val(Me.grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & grd.GetRows(i).Cells(GrdEnum.Pack_Desc).Value.ToString.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "'," & Val(grd.GetRows(i).Cells("PurchaseDemandId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PurchaseDemandDetailId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ", " & Val(grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", N'" & grd.GetRows(i).Cells("SerialNo").Value.ToString.Replace("'", "''") & "') "
                'END TASK-TFS-51
                objCommand.ExecuteNonQuery()
                ''End ReqId-928
                'Val(grd.Rows(i).Cells(5).Value)


                '' 14-11-2016
                objCommand.CommandText = "UPDATE  PurchaseDemandDetailTable " _
                                               & " SET DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", DeliveredTotalQty= IsNull(DeliveredTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(GrdEnum.TotalQty).Value) & " " _
                                               & " WHERE     (PurchaseDemandId = " & Val(grd.GetRows(i).Cells(GrdEnum.PurchaseDemandId).Value) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ") And (PurchaseDemandDetailId =" & Val(grd.GetRows(i).Cells("PurchaseDemandDetailId").Value.ToString) & ")  "
                objCommand.ExecuteNonQuery()
                '' 14-11-2016


            Next


            objCommand.CommandText = "Select DISTINCT ISNULL(DemandID,0) as DemandId From PurchaseOrderDetailTable WHERE PurchaseOrderId=ident_current('PurchaseOrderMasterTable') AND ISNULL(Qty,0) <> 0"
            Dim dtPO As New DataTable
            Dim daPO As New OleDbDataAdapter(objCommand)
            daPO.Fill(dtPO)
            If dtPO IsNot Nothing Then
                If dtPO.Rows.Count > 0 Then
                    For Each row As DataRow In dtPO.Rows

                        'objCommand.CommandText = "Select SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & row("SO_ID") & " Having SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) > 0 "
                        objCommand.CommandText = "Select isnull(Sz1,0)-isnull(DeliveredQty , 0) as DeliveredQty from PurchaseDemandDetailTable where PurchaseDemandId = " & row("DemandId") & " And isnull(Sz1,0)-isnull(DeliveredQty , 0) > 0 "

                        Dim daPOQty As New OleDbDataAdapter(objCommand)
                        Dim dtPOQty As New DataTable
                        daPOQty.Fill(dtPOQty)
                        Dim blnEqual1 As Boolean = True
                        If dtPOQty.Rows.Count > 0 Then
                            'For Each r As DataRow In dtPOQty.Rows
                            'If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
                            blnEqual1 = False
                            'Exit For
                            'End If
                            ' Next
                        End If
                        If blnEqual1 = True Then
                            objCommand.CommandText = "Update PurchaseDemandMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where PurchaseDemandId = " & row("DemandId") & ""
                            objCommand.ExecuteNonQuery()
                        Else
                            objCommand.CommandText = "Update PurchaseDemandMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where PurchaseDemandId = " & row("DemandId") & ""
                            objCommand.ExecuteNonQuery()
                        End If
                    Next
                End If
            End If




            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdInwardExpDetail.GetRows
                If Val(r.Cells("Exp_Amount").Value.ToString) <> 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO InwardExpenseDetailTable(PurchaseId, AccountId, Exp_Amount,DocType) Values(ident_current('PurchaseOrderMasterTable'), " & Val(r.Cells("AccountId").Value.ToString) & ", " & Val(r.Cells("Exp_Amount").Value) & ",'PO')"
                    objCommand.ExecuteNonQuery()
                End If
            Next

            'Task:2673 Update POQty And Set Status Open Or Close
            If Me.cmbCMFADoc.SelectedIndex > 0 Then

                objCommand.CommandText = ""
                objCommand.CommandText = "UPDATE CMFADetailTable SET POQty = IsNull(a.Qty,0) " _
                 & " FROM (SELECT dbo.PurchaseOrderMasterTable.VendorId, dbo.PurchaseOrderMasterTable.RefCMFADocId, SUM(dbo.PurchaseOrderDetailTable.Sz1) AS Qty, " _
                 & " dbo.PurchaseOrderDetailTable.ArticleDefId,dbo.PurchaseOrderDetailTable.ArticleSize " _
                 & " FROM dbo.PurchaseOrderDetailTable INNER JOIN " _
                 & " dbo.PurchaseOrderMasterTable ON dbo.PurchaseOrderDetailTable.PurchaseOrderId = dbo.PurchaseOrderMasterTable.PurchaseOrderId " _
                 & " WHERE(ISNULL(dbo.PurchaseOrderMasterTable.RefCMFADocId, 0)=" & Me.cmbCMFADoc.SelectedValue & ") " _
                 & " GROUP BY dbo.PurchaseOrderMasterTable.VendorId, dbo.PurchaseOrderMasterTable.RefCMFADocId, dbo.PurchaseOrderDetailTable.ArticleDefId,dbo.PurchaseOrderDetailTable.ArticleSize) a " _
                 & " WHERE " _
                 & " a.ArticleDefId = CMFADetailTable.ArticleDefId " _
                 & " AND CMFADetailTable.DocId = a.RefCMFADocId  " _
                 & " AND CMFADetailTable.VendorId = a.VendorId " _
                 & " AND CMFADetailTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
                objCommand.ExecuteNonQuery()

                objCommand.CommandText = ""
                objCommand.CommandText = "UPDATE CMFAMasterTable SET Status=CASE WHEN IsNull(a.BalanceQty,0) <=0 Then 0 ELSE 1 end From( " _
                           & " SELECT dbo.CMFAMasterTable.DocId, SUM(ISNULL(dbo.CMFADetailTable.Sz1, 0) - ISNULL(dbo.CMFADetailTable.POQty, 0)) AS BalanceQty " _
                           & " FROM dbo.CMFADetailTable INNER JOIN " _
                           & " dbo.CMFAMasterTable ON dbo.CMFADetailTable.DocId = dbo.CMFAMasterTable.DocId WHERE dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & " " _
                           & " GROUP BY dbo.CMFAMasterTable.DocId) a " _
                           & " WHERE(a.DocId = CMFAMasterTable.DocId) AND dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
                objCommand.ExecuteNonQuery()

            End If
            'End Task:2673


            'If Val(Me.grd.GetRows(i).Cells("PurchaseDemandId").Value.ToString) > 0 Then

            'objCommand.CommandText = ""
            ''objCommand.CommandText = "UPDATE PurchaseDemandDetailTable Set PurchaseDemandDetailTable.DeliveredQty=IsNull(PurchaseDemandDetailTable.DeliveredQty,0)+IsNull(PurchaseOrderDetailTable.Sz1,0) From PurchaseDemandDetailTable, PurchaseOrderDetailTable, PurchaseOrderMasterTable WHERE PurchaseDemandDetailTable.ArticleDefId = PurchaseOrderDetailTable.ArticleDefId And PurchaseDemandDetailTable.ArticleSize = PurchaseOrderDetailTable.ArticleSize And PurchaseOrderMasterTable.PurchaseOrderId = PurchaseOrderDetailTable.PurchaseOrderId And PurchaseOrderDetailTable.DemandId = PurchaseDemandDetailTable.PurchaseDemandId AND PurchaseOrderDetailTable.PurchaseDemandDetailId = PurchaseDemandDetailTable.PurchaseDemandDetailId AND PurchaseDemandDetailTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=ident_current('PurchaseOrderMasterTable') And IsNull(DemandId,0) <> 0) AND PurchaseOrderDetailTable.PurchaseOrderId=ident_current('PurchaseOrderMasterTable')"

            ' ''Replaced Sz1 with Qty against TASK-408 
            'objCommand.CommandText = "UPDATE PurchaseDemandDetailTable Set PurchaseDemandDetailTable.DeliveredQty=IsNull(PurchaseDemandDetailTable.DeliveredQty,0)+IsNull(PurchaseOrderDetailTable.Sz1,0), PurchaseDemandDetailTable.DeliveredTotalQty=IsNull(PurchaseDemandDetailTable.DeliveredTotalQty,0)+IsNull(PurchaseOrderDetailTable.Qty,0) From PurchaseDemandDetailTable, PurchaseOrderDetailTable, PurchaseOrderMasterTable WHERE PurchaseDemandDetailTable.ArticleDefId = PurchaseOrderDetailTable.ArticleDefId And PurchaseDemandDetailTable.ArticleSize = PurchaseOrderDetailTable.ArticleSize And PurchaseOrderMasterTable.PurchaseOrderId = PurchaseOrderDetailTable.PurchaseOrderId And PurchaseOrderDetailTable.DemandId = PurchaseDemandDetailTable.PurchaseDemandId AND PurchaseOrderDetailTable.PurchaseDemandDetailId = PurchaseDemandDetailTable.PurchaseDemandDetailId AND PurchaseDemandDetailTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=ident_current('PurchaseOrderMasterTable') And IsNull(DemandId,0) <> 0) AND PurchaseOrderDetailTable.PurchaseOrderId=ident_current('PurchaseOrderMasterTable')"
            'objCommand.ExecuteNonQuery()

            'objCommand.CommandText = ""
            'objCommand.CommandText = "Update PurchaseDemandMasterTable Set PurchaseDemandMasterTable.Status=Case When IsNull(DemandDt.BalanceQty,0) > 0 Then 'Open' Else 'Close' End  From PurchaseDemandMasterTable, (Select PurchaseDemandId, SUM(IsNull(Sz1,0)-IsNull(DeliveredQty,0)) as BalanceQty From PurchaseDemandDetailTable WHERE PurchaseDemandDetailTable.PurchaseDemandId In(Select DemandId From PurchaseOrderDetailTable where PurchaseOrderId=ident_current('PurchaseOrderMasterTable') And IsNull(DemandId,0) <> 0)  Group by PurchaseDemandId) DemandDt  WHERE DemandDt.PurchaseDemandId = PurchaseDemandMasterTable.PurchaseDemandId AND PurchaseDemandMasterTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=ident_current('PurchaseOrderMasterTable') And IsNull(DemandId,0) <> 0)"
            ''Replaced Sz1 with Qty against TASK-408
            'objCommand.CommandText = "Update PurchaseDemandMasterTable Set PurchaseDemandMasterTable.Status=Case When IsNull(DemandDt.BalanceQty,0) > 0 Then 'Open' Else 'Close' End  From PurchaseDemandMasterTable, (Select PurchaseDemandId, SUM(IsNull(Sz1,0)-IsNull(DeliveredQty,0)) as BalanceQty From PurchaseDemandDetailTable WHERE PurchaseDemandDetailTable.PurchaseDemandId In(Select DemandId From PurchaseOrderDetailTable where PurchaseOrderId=ident_current('PurchaseOrderMasterTable') And IsNull(DemandId,0) <> 0)  Group by PurchaseDemandId) DemandDt  WHERE DemandDt.PurchaseDemandId = PurchaseDemandMasterTable.PurchaseDemandId AND PurchaseDemandMasterTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=ident_current('PurchaseOrderMasterTable') And IsNull(DemandId,0) <> 0)"
            'objCommand.ExecuteNonQuery()

            'UpdateStatus(objCommand)


            'Ali FAisal :TFS1442 : Insert data in scheduled payments
            If Me.cmbPaymentTypes.SelectedIndex > 0 Then
                objCommand.CommandText = ""
                objCommand.CommandText = "Delete From tblPaymentSchedule WHERE OrderId=" & getVoucher_Id & ""
                objCommand.ExecuteNonQuery()
                objCommand.CommandText = ""
                Dim str As String = "SELECT Id, TermDays FROM tblPaymentTerms WHERE Id = " & Me.cmbPaymentTypes.SelectedValue & ""
                Dim dtDays As DataTable = GetDataTable(str)
                Dim Days As Integer = 0I
                If dtDays.Rows.Count > 0 Then
                    Days = Convert.ToInt32(dtDays.Rows(0).Item(1))
                End If
                objCommand.CommandText = "INSERT INTO tblPaymentSchedule(PayTypeId ,SchDate, OrderId, OrderType, Amount, InitialSchDate, PaymentStatus) VALUES(" & Me.cmbPaymentTypes.SelectedValue & ", N'" & Me.dtpPODate.Value.AddDays(Days) & "', " & getVoucher_Id & ", N'PO', " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", N'" & Me.dtpPODate.Value.AddDays(Days) & "', 'UnPaid')"
                objCommand.ExecuteNonQuery()
            End If
            'Ali FAisal :TFS1442 : Ens

            trans.Commit()
            Save = True
            'InsertVoucher()
            setEditMode = False
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try
        UpdateStatus()
        ''insert Activity Log
        SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Purchase, Me.txtPONo.Text.Trim, True)
        ''Start TFS2375
        ''insert Approval Log
        SaveApprovalLog(EnumReferenceType.PurchaseOrder, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Purchase Order ," & cmbVendor.Text & "", Me.Name, 0)
        ''End TFS2375
        SendSMS()
        Dim ValueTable As DataTable = GetSingle(getVoucher_Id)
        NotificationDAL.SaveAndSendNotification("Purchase Order", "PurchaseOrderMasterTable", getVoucher_Id, ValueTable, "Purchase > Purchase Order")
    End Function
    Sub InsertVoucher()
        Try
            SaveVoucherEntry(GetVoucherTypeId("SV"), GetNextDocNo("SV", 6, "tblVoucher", "voucher_no"), Me.dtpPODate.Value, "", Nothing, Val(Me.cmbVendor.ActiveRow.Cells(0).Text.ToString), getConfigValueByType("SalesCreditAccount"), Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum), Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum), "Both", Me.Name, Me.txtPONo.Text, True)
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try

    End Sub

    Private Function FormValidate() As Boolean

        If txtPONo.Text = "" Then
            msg_Error("Please enter PO No.")
            txtPONo.Focus() : FormValidate = False : Exit Function
        End If
        If cmbVendor.ActiveRow.Cells(0).Value <= 0 Then
            msg_Error("Please select Vendor")
            cmbVendor.Focus() : FormValidate = False : Exit Function
        End If

        If Not Me.grd.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            cmbItem.Focus() : FormValidate = False : Exit Function
        End If


        'If ValidateCMFATotalAmount() = True Then
        '    Return True
        'Else
        '    Return False
        'End If

        If Me.cmbCMFADoc.SelectedIndex > 0 Then
            If Val(Me.txtCMFAAmount.Text) < (Val(Me.txtPOAmountAgainstCMFA.Text) + Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) Then
                ShowErrorMessage("PO amount exceeded from CMFA amount.")
                Me.cmbCMFADoc.Focus()
                Return False
            End If
        End If

        'If cmbCurrency.SelectedIndex > 1 AndAlso Val(txtCurrencyRate.Text) <> 1 Then
        '    msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
        'End If
        'Change by murtaza default currency rate(10/26/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : FormValidate = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/26/2022)

        'TASK22 Validation On Price And Qty
        For Each jRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            If (Val(jRow.Cells("Price").Value.ToString) = 0 Or Val(jRow.Cells("Qty").Value.ToString) = 0) Then
                msg_Error("Rate or Qty is not greater than 0")
                Return False
                Exit For
            End If
        Next
        ''Start TFS2988
        If IsEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document Can Not be Changed ,because Approval Process is in Progress") : Return False : Exit Function
                End If
            End If
        End If
        ''End TFS2988
        Return True

    End Function
    Public Function ValidateCMFATotalAmount() As Boolean
        Try
            Dim strSQL As String = String.Empty
            Dim objDt As New DataTable
            If BtnSave.Text = "&Save" Then
                If Me.cmbCMFADoc.SelectedIndex > 0 Then
                    'Before against task:2734
                    'strSQL = "Select (SUM(IsNull(Qty,0)*IsNull(Price,0))) Amount From CMFADetailTable INNER JOIN CMFAMasterTable On CMFAMasterTable.DocId = CMFADetailTable.DocId  LEFT OUTER JOIN(Select RefCMFADocId, (IsNull(Qty,0)*IsNull(Price,0)) as POAmount From PurchaseOrderDetailTable INNER JOIN PurchaseOrderMasterTable On PurchaseOrderMasterTable.PurchaseOrderId = PurchaseOrderDetailTable.PurchaseOrderId WHERE RefCMFADocId=N'" & Me.cmbCMFADoc.SelectedValue & "') PO On PO.RefCMFADocId = CMFAMasterTable.DocId WHERE CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
                    'Task:2734 Change Query
                    'Task:2746 Add Cash Request Total Amount
                    strSQL = "Select DocId, ISNULL(ProjectedExpAmount,0) as Amount, IsNull(PO.POAmount,0) as POAmount, IsNull(Req.Total_Amount,0) as Total_Amount From CMFAMasterTable LEFT OUTER JOIN(Select RefCMFADocId, Sum(IsNull(PurchaseOrderAmount,0)) as POAmount From PurchaseOrderMasterTable  WHERE RefCMFADocId=N'" & Me.cmbCMFADoc.SelectedValue & "' Group by RefCMFADocId) PO On PO.RefCMFADocId = CMFAMasterTable.DocId LEFT OUTER JOIN (SELECT CMFADocId, SUM(Total_Amount) AS Total_Amount FROM dbo.CashRequestHead WHERE CMFADocId=" & Me.cmbCMFADoc.SelectedValue & " AND IsNull(Approved,0)=1 GROUP BY CMFADocId) Req On Req.CMFADocId = CMFAMasterTable.DocId WHERE CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
                    'End Task:2746
                    'End Task:2734
                End If
            Else
                If Me.cmbCMFADoc.SelectedIndex > 0 Then
                    'Before against task:2734
                    'strSQL = "Select (SUM(IsNull(Qty,0)*IsNull(Price,0)))-SUM(IsNull(POAmount,0)) Amount From CMFADetailTable INNER JOIN CMFAMasterTable On CMFAMasterTable.DocId = CMFADetailTable.DocId LEFT OUTER JOIN(Select RefCMFADocId, (IsNull(Qty,0)*IsNull(Price,0)) as POAmount From PurchaseOrderDetailTable INNER JOIN PurchaseOrderMasterTable On PurchaseOrderMasterTable.PurchaseOrderId = PurchaseOrderDetailTable.PurchaseOrderId WHERE PurchaseOrderNo <> N'" & Me.txtPONo.Text & "') PO On PO.RefCMFADocId = CMFAMasterTable.DocId  WHERE CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
                    'Task:2734 Change Query
                    'Task:2746 Add Cash Request Total Amount
                    strSQL = "Select DocId, ISNULL(ProjectedExpAmount,0)  as Amount,IsNull(PO.POAmount,0) as POAmount, IsNull(Req.Total_Amount,0) as Total_Amount  From CMFAMasterTable LEFT OUTER JOIN(Select RefCMFADocId, Sum(IsNull(PurchaseOrderAmount,0)) as POAmount From PurchaseOrderMasterTable  WHERE RefCMFADocId=N'" & Me.cmbCMFADoc.SelectedValue & "' AND PurchaseOrderNo <> N'" & Me.txtPONo.Text & "' Group by RefCMFADocId) PO On PO.RefCMFADocId = CMFAMasterTable.DocId LEFT OUTER JOIN (SELECT CMFADocId, SUM(Total_Amount) AS Total_Amount FROM dbo.CashRequestHead WHERE CMFADocId=" & Me.cmbCMFADoc.SelectedValue & " AND IsNull(Approved,0)=1 GROUP BY CMFADocId) Req On Req.CMFADocId = CMFAMasterTable.DocId WHERE CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
                    'End Task:2746
                    'End Task:2734
                End If
            End If
            If strSQL.Length > 0 Then
                objDt = GetDataTable(strSQL)
                If Val(objDt.Rows(0).Item(0).ToString) > 0 Then
                    'Task:2746 Less Cash Request Total Amount
                    If (Val(objDt.Rows(0).Item(1).ToString) - Val(objDt.Rows(0).Item(3).ToString)) < Val(objDt.Rows(0).Item(2).ToString) + Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)) Then
                        'End Task:2746
                        ShowErrorMessage("PO amount exceeded from CMFA amount.")
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If

            'If Val(Me.txtCMFAAmount.Text) < Val( Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)) Then
            '    ShowErrorMessage("PO amount exceeded from CMFA amount.")
            'End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Sub EditRecord()
        Try

            IsEditMode = True
            'FillCombo("Vendor") 'R933 Commented 
            'FillCombo("LC")'R933 Commented
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            'Me.FillCombo("SOComplete") 'R933 Commented
            Me.BtnSave.Text = "&Update"
            txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value.ToString
            Me.GetSecurityRights()
            'Task 1592
            If grdSaved.CurrentRow.Cells(1).Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
                dtpPODate.MaxDate = dtpPODate.Value.AddMonths(3)
                dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            Else
                dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            End If
            txtReceivingID.Text = grdSaved.CurrentRow.Cells("PurchaseOrderId").Value
            'TODO. ----
            cmbVendor.Value = grdSaved.CurrentRow.Cells("VendorId").Value
            ''R933  Validate Vendor Data 
            cmbVendor.Value = grdSaved.CurrentRow.Cells("VendorId").Value
            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Vendor is disable.")
                Exit Sub
            End If

            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
            If Not getConfigValueByType("PurchaseOrderApproval") = "Error" Then
                ApprovalProcessId = getConfigValueByType("PurchaseOrderApproval")
            End If
            If ApprovalProcessId = 0 Then
                Me.btnApprovalHistory.Visible = False
                Me.btnApprovalHistory.Enabled = False
            Else
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
                Me.chkPost.Visible = False
            End If
            ''Ayesha Rehman :TFS2375 :End

            '26-Feb-2018: Task TFS2377: Ayesha Rehman: Verify Check box value in system configuration(purchase) for Disable/Enable Print Button
            'Start Task:
            If getConfigValueByType("POPrintAfterApproval") = "True" Then
                If GetDocumentApprovalStatus(txtPONo.Text) Then
                    Me.BtnPrint.Enabled = True
                    Me.btnSearchPrint.Enabled = True
                Else
                    Me.BtnPrint.Enabled = False
                    Me.btnSearchPrint.Enabled = False
                End If
                ''Aashir: TFS: Enable print button in edit mode  
                'Else
                '    'Me.BtnPrint.Enabled = False
                '    'Me.btnSearchPrint.Enabled = False
            End If
            'End Task:TFS2377

            txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
            txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""
            'Me.cmbSalesMan.SelectedValue = grdSaved.CurrentRow.Cells("EmployeeCode").Value.ToString
            'Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value
            'Me.cmbCurrency.SelectedValue = Me.grdSaved.GetRow.Cells("CurrencyType").Text.ToString
            'Me.txtCurrencyRate.Text = Me.grdSaved.GetRow.Cells("CurrencyRate").Text.ToString
            If IsDBNull(Me.grdSaved.GetRow.Cells("Receiving_Date").Value) Then
                Me.dtpReceivingDate.Value = Now
                Me.dtpReceivingDate.Checked = False
            Else
                Me.dtpReceivingDate.Value = Me.grdSaved.GetRow.Cells("Receiving_Date").Value
                Me.dtpReceivingDate.Checked = True
            End If
            Call DisplayDetail(grdSaved.CurrentRow.Cells("PurchaseOrderId").Value)
            FillInwardExpense(grdSaved.CurrentRow.Cells("PurchaseOrderId").Value, "PO")
            Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            Me.cmbLC.Value = grdSaved.CurrentRow.Cells("LcId").Value.ToString

            'Task:2673 Check Status CMFA Document
            Me.cmbProject.SelectedValue = Val(Me.grdSaved.GetRow.Cells("CostCenterId").Value.ToString)
            If Val(Me.grdSaved.GetRow.Cells("RefCMFADocId").Value.ToString) > 0 Then
                Dim objDt As DataTable = CType(Me.cmbCMFADoc.DataSource, DataTable)
                objDt.AcceptChanges()
                objDt.TableName = "Default"
                Dim dv As New DataView
                dv.Table = objDt
                dv.RowFilter = "DocId=" & Me.grdSaved.GetRow.Cells("RefCMFADocId").Value.ToString & ""

                If Not dv.ToTable.Rows.Count > 0 Then
                    Dim dr As DataRow
                    dr = objDt.NewRow
                    dr(0) = Me.grdSaved.GetRow.Cells("RefCMFADocId").Value.ToString
                    dr(1) = Me.grdSaved.GetRow.Cells("CMFA Doc No").Value.ToString
                    dr(2) = Me.cmbVendor.Value
                    objDt.Rows.Add(dr)
                    objDt.AcceptChanges()
                End If
            End If

            Me.CtrlGrdBar2_Load(Nothing, Nothing)
            Me.CtrlGrdBar1_Load(Nothing, Nothing)

            'If Val(Me.grdSaved.GetRow.Cells("PurchaseDemandId").Value.ToString) > 0 Then
            '    Dim dtDemand As New DataTable
            '    dtDemand = DirectCast(Me.cmbPurchaseDemand.DataSource, DataTable)
            '    dtDemand.AcceptChanges()
            '    Dim objRow() As DataRow = dtDemand.Select("PurchaseDemandId=" & Val(Me.grdSaved.GetRow.Cells("PurchaseDemandId").Value.ToString) & "", String.Empty)
            '    If objRow IsNot Nothing Then
            '        If Not objRow.Length > 0 Then
            '            Dim drDemand As DataRow
            '            drDemand = dtDemand.NewRow
            '            drDemand(0) = Val(Me.grdSaved.GetRow.Cells("PurchaseDemandId").Value.ToString)
            '            drDemand(1) = Me.grdSaved.GetRow.Cells("PurchaseDemandNo").Value.ToString
            '            dtDemand.Rows.Add(drDemand)
            '            dtDemand.AcceptChanges()
            '        End If
            '    End If
            'End If
            ''RemoveHandler cmbPurchaseDemand.SelectedIndexChanged, AddressOf cmbPurchaseDemand_SelectedIndexChanged
            'Me.cmbPurchaseDemand.SelectedValue = Val(Me.grdSaved.GetRow.Cells("PurchaseDemandId").Value.ToString)
            'RemoveHandler cmbCMFADoc.SelectedIndexChanged, AddressOf cmbCMFADoc_SelectedIndexChanged
            Me.cmbCMFADoc.SelectedValue = Me.grdSaved.GetRow.Cells("RefCMFADocId").Value.ToString
            'AddHandler cmbCMFADoc.SelectedIndexChanged, AddressOf cmbCMFADoc_SelectedIndexChanged
            'End Taks:2673
            'AddHandler cmbPurchaseDemand.SelectedIndexChanged, AddressOf cmbPurchaseDemand_SelectedIndexChanged
            GetTotal()
            Me.BtnSave.Text = "&Update"
            Me.cmbPo.Enabled = False
            'Me.GetSecurityRights()
            'R:913 Retrieve Post Value
            If grdSaved.CurrentRow.Cells("Post").Value.ToString = "UnPost" Then
                Me.chkPost.Checked = False
            Else
                Me.chkPost.Checked = True
            End If
            'End R:913
            Me.txtTerms_And_Condition.Text = Me.grdSaved.GetRow.Cells("Terms").Value.ToString
            Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
            Me.lblPrintStatus.Text = "Print Status: " & Me.grdSaved.GetRow.Cells("Print Status").Text.ToString
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


            'Task#117062015 set cmbPOType and cmbStockDispatchStatus values
            If IsDBNull(grdSaved.CurrentRow.Cells("POType").Value) Then
                Me.cmbPOType.SelectedIndex = 0
            Else
                Me.cmbPOType.Text = grdSaved.CurrentRow.Cells("POType").Value.ToString
            End If

            If IsDBNull(grdSaved.CurrentRow.Cells("POStockDispatchStatus").Value) Then
                Me.cmbStockDispatchStatus.Rows(0).Activate()
            Else
                Me.cmbStockDispatchStatus.Text = grdSaved.CurrentRow.Cells("POStockDispatchStatus").Value.ToString
            End If
            'End Task#117062015

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

            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnDelete.Visible = True
            Me.BtnPrint.Visible = True
            '''''''''''''''''''''''''

            Me.btnImports.Visible = True        'Task#117062015 visible import button
            Me.cmbPaymentTypes.SelectedValue = Me.grdSaved.GetRow.Cells("PayTypeId").Value

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 26-Feb-2018: Task TFS2377: Ayesha Rehman: Added A function to get the Approval Status of the Document 
    ''' </summary>
    ''' <param name="DocumentNo"></param>
    ''' <returns></returns>
    ''' <remarks>26-Feb-2018: Task TFS2377: Ayesha Rehman</remarks>
    Private Function GetDocumentApprovalStatus(ByVal DocumentNo As String) As Boolean
        Dim str As String = ""
        Dim Status As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            str = " Select [Status] from ApprovalHistory where DocumentNo ='" & DocumentNo & "'"
            Status = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            trans.Commit()
            If Status = EnumApprovalMasterStatus.Approved.ToString Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Private Sub DisplayPODetail(ByVal ReceivingID As Integer)
        Dim str As String = String.Empty
        'Dim i As Integer
        'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
        '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,Isnull(Recv_D.TaxPercent,0) as TaxPercent, 0 as TaxAmount, " _
        '      & " Article.ArticleGroupId,Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
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

        'grd.Rows.Clear()
        'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

        '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9))

        '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
        '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

        'Next
        'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(DeliveredQty,0)) AS Qty, Recv_D.CurrentPrice as Price, " _
        '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.CurrentPrice ELSE ((Recv_D.Sz1 * Recv_D.CurrentPrice) * Article.PackQty) END AS Total, Convert(Float,0) as TaxPercent, 0 as TaxAmount, Convert(float,0) as [Total Amount],  " _
        '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, isnull(Recv_D.CurrentPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments,IsNull(Recv_D.PurchaseDemandId,0) as PurchaseDemandId  FROM dbo.PurchaseDemandDetailTable Recv_D INNER JOIN  " _
        '& " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
        '& " Where(Recv_D.PurchaseDemandId = " & ReceivingID & ") AND (IsNull(Recv_D.Sz1,0)-IsNull(DeliveredQty,0)) > 0"

        'TASK24 Added Field Color, Size, UOM
        '  str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM,Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(DeliveredQty,0)) AS Qty, Recv_D.CurrentPrice as Price, " _
        '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.CurrentPrice ELSE ((Recv_D.Sz1 * Recv_D.CurrentPrice) * Article.PackQty) END AS Total, Convert(Float,0) as TaxPercent, 0 as TaxAmount, Convert(float,0) as [Total Amount],  " _
        '  & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, isnull(Recv_D.CurrentPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments,IsNull(Recv_D.PurchaseDemandId,0) as PurchaseDemandId  FROM dbo.PurchaseDemandDetailTable Recv_D INNER JOIN  " _
        '& " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
        '  & " Where(Recv_D.PurchaseDemandId = " & ReceivingID & ") AND (IsNull(Recv_D.Sz1,0)-IsNull(DeliveredQty,0)) > 0"
        'END TASK24
        'TASK-TFS-51 Added Fields AdTax_Percent, AdTax_Amount
        'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM,Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(DeliveredQty,0)) AS Qty, Recv_D.CurrentPrice as Price, " _
        '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.CurrentPrice ELSE ((Recv_D.Sz1 * Recv_D.CurrentPrice) * Article.PackQty) END AS Total, Convert(Float,0) as TaxPercent, 0 as TaxAmount, 0 as AdTax_Percent, 0 as AdTax_Amount, Convert(float,0) as [Total Amount],  " _
        '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, isnull(Recv_D.CurrentPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments,IsNull(Recv_D.PurchaseDemandId,0) as PurchaseDemandId,  Recv_D.PurchaseDemandDetailId,0 as PurchaseOrderDetailId  FROM dbo.PurchaseDemandDetailTable Recv_D INNER JOIN  " _
        '& " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
        '& " Where(Recv_D.PurchaseDemandId = " & ReceivingID & ") AND (IsNull(Recv_D.Sz1,0)-IsNull(DeliveredQty,0)) > 0"
        'END TASKT-TFS-51
        str = "SELECT '' As SerialNo, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM,Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(DeliveredQty,0)) AS Qty, Recv_D.CurrentPrice,Case When IsNull(Recv_D.CurrentPrice,0) > IsNull(Recv_D.CurrentPrice,0) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Recv_D.CurrentPrice,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent, Recv_D.CurrentPrice as Price, 0 As BaseCurrencyId, 0 As BaseCurrencyRate, 0 As CurrencyId, 0 As CurrencyRate, 0 As CurrencyAmount, 0 As CurrencyTotalAmount, " _
& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.CurrentPrice ELSE ((Recv_D.Sz1 * Recv_D.CurrentPrice) * Article.PackQty) END AS Total, Convert(Float,0) as TaxPercent, 0 as TaxAmount, 0 as CurrencyTaxAmount, 0 as AdTax_Percent, 0 as AdTax_Amount, 0 as CurrencyAdTaxAmount, Convert(float,0) as TotalAmount,  " _
& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, isnull(Recv_D.CurrentPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments,IsNull(Recv_D.PurchaseDemandId,0) as PurchaseDemandId,  Recv_D.PurchaseDemandDetailId,0 as PurchaseOrderDetailId, IsNull(Recv_D.Qty, 0) - IsNull(Recv_D.DeliveredTotalQty, 0) As TotalQty  FROM dbo.PurchaseDemandDetailTable Recv_D INNER JOIN  " _
& " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
& " Where(Recv_D.PurchaseDemandId = " & ReceivingID & ") AND (IsNull(Recv_D.Sz1,0)-IsNull(DeliveredQty,0)) > 0"
        'Dim dtDisplayPODetail As New DataTable
        'dtDisplayPODetail = GetDataTable(str)
        'Me.grd.DataSource = Nothing
        'dtDisplayPODetail.Columns("Total").Expression = "IIF(Unit='Pack', ((PackQty*Qty)*Price), Qty*Price)"
        'Me.grd.DataSource = dtDisplayPODetail
        'ApplyGridSetting()
        'FillCombo("grdLocation")
        'DisplayDetail(-1)
        Dim dtDisplayDetail As New DataTable
        dtDisplayDetail = GetDataTable(str)
        dtDisplayDetail.AcceptChanges()
        'dtDisplayDetail.Columns("TaxAmount").Expression = "((IsNull(TaxPercent,0)/100)*IsNull(Total,0))"
        'dtDisplayDetail.Columns("Total Amount").Expression = "IsNull(Total,0)+IsNull(TaxAmount,0)"

        'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',((PackQty*Qty)*Price), Qty*Price)"
        'dtDisplayDetail.Columns("Total").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)"
        'dtDisplayDetail.Columns("TaxAmount").Expression = "((TaxPercent/100)*Total)"
        ''TASK-TFS-51 Added Fields AdTax_Percent, AdTax_Amount
        'dtDisplayDetail.Columns("AdTax_Amount").Expression = "((IsNull(AdTax_Percent,0)/100)*Total)"
        'dtDisplayDetail.Columns("Total Amount").Expression = "([Total]+([TaxAmount]+[AdTax_Amount]))" 'Task:2374 Show Total Amount
        'END TASK-TFS-51
        dtDisplayDetail.Columns("Total").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)* CurrencyRate"
        dtDisplayDetail.Columns("CurrencyAmount").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)"
        dtDisplayDetail.Columns("TaxAmount").Expression = "((TaxPercent/100)*Total)"
        dtDisplayDetail.Columns("CurrencyTaxAmount").Expression = "(((CurrencyAmount) * IsNull(TaxPercent,0))/100)"
        'TASK-TFS-51 Set Exparession For Additonal Tax
        dtDisplayDetail.Columns("AdTax_Amount").Expression = "((IsNull(AdTax_Percent,0)/100)*Total)"
        dtDisplayDetail.Columns("CurrencyAdTaxAmount").Expression = "(((CurrencyAmount) * IsNull(AdTax_Percent,0))/100)"
        dtDisplayDetail.Columns("TotalAmount").Expression = "([Total]+([TaxAmount]+[AdTax_Amount]))" 'Task:2374 Show Total Amount
        dtDisplayDetail.Columns("CurrencyTotalAmount").Expression = "IsNull([CurrencyAmount],0) + (IsNull([CurrencyTaxAmount],0)+IsNull([CurrencyAdTaxAmount],0))"



        If dtDisplayDetail IsNot Nothing Then
            If dtDisplayDetail.Rows.Count > 0 Then
                For Each dRow As DataRow In dtDisplayDetail.Rows

                    Dim dtData As DataTable = CType(Me.grd.DataSource, DataTable)
                    dtData.AcceptChanges()

                    If Not GetFilterDataFromDataTable(dtData, "ArticleDefId=" & Val(dRow.Item("ArticleDefId").ToString) & " AND PurchaseDemandId=" & Val(dRow.Item("PurchaseDemandId").ToString) & " And Unit='" & dRow.Item("Unit").ToString & "' AND PurchaseDemandDetailId=" & Val(dRow.Item("PurchaseDemandDetailId").ToString) & "").Count > 0 Then
                        Me.cmbCategory.SelectedValue = Val(dRow.Item("LocationId").ToString)
                        Me.cmbItem.Value = Val(dRow.Item("ArticleDefId").ToString)
                        Me.cmbUnit.Text = dRow.Item("unit").ToString
                        Me.txtPackQty.Text = Val(dRow.Item("PackQty").ToString)
                        Me.txtQty.Text = Val(dRow.Item("Qty").ToString)
                        Me.txtTotalQuantity.Text = Val(dRow.Item("TotalQty").ToString)
                        Me.txtRate.Text = IIf(Val(dRow.Item("CurrentPrice").ToString) = 0, 1, Val(dRow.Item("CurrentPrice").ToString))
                        PurchaseDemandID = Val(dRow.Item("PurchaseDemandId").ToString)
                        PurchaseDemandDetailId = Val(dRow.Item("PurchaseDemandDetailId").ToString)
                        strComments = dRow.Item("Comments").ToString
                        Me.btnAdd_Click(Nothing, Nothing)
                        PurchaseDemandID = 0I
                        PurchaseDemandDetailId = 0
                        strComments = String.Empty
                    End If
                Next
            End If
        End If


        'Ameen uncomment below section
        'Me.grd.DataSource = Nothing
        'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',((PackQty*Qty)*Price), Qty*Price)"
        'dtDisplayDetail.Columns("TaxAmount").Expression = "((TaxPercent/100)*Total)"
        'dtDisplayDetail.Columns("Total Amount").Expression = "Total+TaxAmount" 'Task:2374 Show Total Amount
        'Me.grd.DataSource = dtDisplayDetail
        'ApplyGridSetting()
        'FillCombo("grdLocation")

    End Sub
    '' ReqId-899 Added New Field TaxPercent, TaxAmount In Query
    Private Sub DisplayDetail(ByVal ReceivingID As Integer, Optional ByVal Condition As String = "")
        Dim str As String = String.Empty
        'Dim i As Integer

        'str = "SELECT     Article_Group.ArticleGroupName AS Category, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
        '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
        '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, isnull(Article_Group.ArticleGroupId,0) ArticleGroupId  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
        '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
        '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
        '      & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
        If Condition = String.Empty Then
            'Before against task:2374
            '' ReqId-928 Added Column Comments
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '     & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.TaxPercent,0) as TaxPercent, 0 as TaxAmount, " _
            '     & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, isnull(Recv_D.PackPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
            '     & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '     & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
            '' End ReqId-928
            'Task:3274 Added Column Total Amount 
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.TaxPercent,0) as TaxPercent, 0 as TaxAmount, Convert(float,0) as [Total Amount], " _
            '  & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, isnull(Recv_D.PackPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
            '  & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '  & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '  & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
            'End Task:2374
            ' str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.TaxPercent,0) as TaxPercent, 0 as TaxAmount, Convert(float,0) as [Total Amount], " _
            '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, isnull(Recv_D.PackPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, IsNull(Recv_D.DemandId,0) as PurchaseDemandId  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
            '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '& " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
            'TASK24 Added Field Color, Size, UOM
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.TaxPercent,0) as TaxPercent, 0 as TaxAmount, Convert(float,0) as [Total Amount], " _
            '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, isnull(Recv_D.PackPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, IsNull(Recv_D.DemandId,0) as PurchaseDemandId  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
            '    & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId " _
            '    & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
            'TASK-TFS-51 Added Fields AdTax_Percent, AdTax_Amount
            ' TASK-TFS-60 PurchaseDemandDetailId Field added 
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.TaxPercent,0) as TaxPercent, 0 as TaxAmount, IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent, IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount,Convert(float,0) as [Total Amount], " _
            '  & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, isnull(Recv_D.PackPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, IsNull(Recv_D.DemandId,0) as PurchaseDemandId, Recv_D.PurchaseDemandDetailId,IsNull(Recv_D.PurchaseOrderDetailId,0) as PurchaseOrderDetailId  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
            '  & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId " _
            '  & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
            str = "SELECT Recv_D.SerialNo, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit,Recv_D.Warranty AS Warranty,Recv_D.Status AS Status, Recv_D.Sz1 AS Qty, Recv_D.CurrentPrice,  Case When IsNull(Recv_D.CurrentPrice,0) > IsNull(Price,0) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent,Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As CurrencyTotalAmount, " _
          & " (IsNull(Recv_D.Qty, 0)*IsNull(Recv_D.Price, 0)* Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, Isnull(Recv_D.TaxPercent,0) as TaxPercent, 0 as TaxAmount, Convert(float, 0) As CurrencyTaxAmount, IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent, IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount,  Convert(float, 0) As CurrencyAdTaxAmount, Convert(float,0) as TotalAmount, " _
          & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, isnull(Recv_D.PackPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, IsNull(Recv_D.DemandId,0) as PurchaseDemandId, Recv_D.PurchaseDemandDetailId, IsNull(Recv_D.PurchaseOrderDetailId,0) as PurchaseOrderDetailId, IsNull(Recv_D.Qty, 0) As TotalQty  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
          & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId " _
          & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
        ElseIf Condition = "LoadSalesOrder" Then
            str = String.Empty
            'Before against Task:2374
            '' ReqId-928 Added Column Comments
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Isnull(Recv_D.PurchasePrice,0) as Price, " _
            '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.SalesTax_Percentage,0) as TaxPercent, 0 as TaxAmount, " _
            '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, Isnull(Recv_D.PurchasePrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments  FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
            '    & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '    & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '    & " Where Recv_D.SalesOrderID =" & ReceivingID & ""
            '' End ReqId-928
            'Task:2374 Added Column Total Amount
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Isnull(Recv_D.PurchasePrice,0) as Price, " _
            '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.SalesTax_Percentage,0) as TaxPercent, 0 as TaxAmount, Convert(float,0) as [Total Amount], " _
            '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, Isnull(Recv_D.PurchasePrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments  FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
            '    & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '    & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '    & " Where Recv_D.SalesOrderID =" & ReceivingID & ""
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Isnull(Recv_D.PurchasePrice,0) as Price, " _
            '   & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.SalesTax_Percentage,0) as TaxPercent, 0 as TaxAmount, Convert(float,0) as [Total Amount], " _
            '   & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, Isnull(Recv_D.PurchasePrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments, 0 as PurchaseDemandId  FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
            '   & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '   & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '   & " Where Recv_D.SalesOrderID =" & ReceivingID & ""
            'TASK 24 Added Field Color, Size, UOM
            ' str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Isnull(Recv_D.PurchasePrice,0) as Price, " _
            '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.SalesTax_Percentage,0) as TaxPercent, 0 as TaxAmount, Convert(float,0) as [Total Amount], " _
            '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, Isnull(Recv_D.PurchasePrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments, 0 as PurchaseDemandId  FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
            '& " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
            '    & " Where Recv_D.SalesOrderID =" & ReceivingID & ""
            'TASK-TFS-51 Added Fields AdTax_Percent, AdTax_Amount
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Isnull(Recv_D.PurchasePrice,0) as Price, " _
            '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.SalesTax_Percentage,0) as TaxPercent, 0 as TaxAmount, 0 as AdTax_Percent,0 as AdTax_Amount, Convert(float,0) as [Total Amount], " _
            '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, Isnull(Recv_D.PurchasePrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments, 0 as PurchaseDemandId, Recv_D.PurchaseDemandDetailId,0 as PurchaseOrderDetailId   FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
            '    & " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
            '    & " Where Recv_D.SalesOrderID =" & ReceivingID & ""
            ' Below lines are commented against TASK TFS4646 ON 17-10-18 to get rate and tax from vendor quotation. Done by Muhammad Amin 
            'str = "SELECT Recv_D.SerialNo, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Isnull(Recv_D.PurchasePrice,0) as CurrentPrice, Case When IsNull(Recv_D.CurrentPrice,0) > IsNull(Price,0) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent,Isnull(Recv_D.Price,0) as Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As CurrencyTotalAmount, " _
            ' & " (IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, Isnull(Recv_D.SalesTax_Percentage,0) as TaxPercent, 0 as TaxAmount, Convert(float, 0) As CurrencyTaxAmount, 0 as AdTax_Percent, 0 as AdTax_Amount, Convert(float, 0) As CurrencyAdTaxAmount, Convert(float,0) as TotalAmount, " _
            ' & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,  Isnull(Recv_D.PackPrice,0) as PackPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments, 0 as PurchaseDemandId, 0 As PurchaseDemandDetailId, 0 as PurchaseOrderDetailId, IsNull(Recv_D.Qty, 0) As TotalQty FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
            ' & " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
            ' & " Where Recv_D.SalesOrderID =" & ReceivingID & ""
            'FillInwardExpense(-1, "PO")
            'Waqar: 12/DEC/2018: Added these Lines to get Customer Name, Currency Rate and Type
            str = "SELECT VendorQuotationMaster.VendorId FROM VendorQuotationMaster RIGHT OUTER JOIN VendorQuotationDetail ON VendorQuotationMaster.VendorQuotationId = VendorQuotationDetail.VendorQuotationId RIGHT OUTER JOIN QuotationDetailTable ON VendorQuotationDetail.VendorQuotationDetailId = QuotationDetailTable.VendorQuotationDetailId RIGHT OUTER JOIN QuotationMasterTable ON QuotationDetailTable.QuotationId = QuotationMasterTable.QuotationId RIGHT OUTER JOIN SalesOrderMasterTable ON QuotationMasterTable.QuotationId = SalesOrderMasterTable.QuotationId WHERE SalesOrderMasterTable.SalesOrderId =  " & ReceivingID & ""
            Dim dt As DataTable
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                cmbVendor.Value = dt.Rows(0).Item("VendorId")
            End If
            ' TASK TFS4646 ON 17-10-18 to get rate and tax from vendor quotation. Done by Muhammad Amin 
            str = "SELECT Recv_D.SerialNo, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Isnull(Recv_D.PurchasePrice,0) as CurrentPrice, Case When IsNull(Recv_D.CurrentPrice,0) > IsNull(Recv_D.Price,0) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Recv_D.Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent, CASE WHEN ISNULL(QuotationDetailTable.VendorQuotationDetailId, 0) > 0 AND ISNULL(VendorQuotationDetail.Price, 0) > 0 THEN ISNULL(VendorQuotationDetail.Price, 0) ELSE IsNull(Recv_D.Price, 0) END as Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As CurrencyTotalAmount, " _
          & " (IsNull(Recv_D.Qty, 0) * CASE WHEN ISNULL(QuotationDetailTable.VendorQuotationDetailId, 0) > 0 AND ISNULL(VendorQuotationDetail.Price, 0) > 0 THEN ISNULL(VendorQuotationDetail.Price, 0) ELSE IsNull(Recv_D.Price, 0) END * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, CASE WHEN ISNULL(QuotationDetailTable.VendorQuotationDetailId, 0) > 0 AND ISNULL(VendorQuotationDetail.SalesTaxPer, 0) > 0 THEN ISNULL(VendorQuotationDetail.SalesTaxPer, 0) ELSE  ISNULL(Recv_D.SalesTax_Percentage,0) END AS TaxPercent, 0 as TaxAmount, Convert(float, 0) As CurrencyTaxAmount, 0 as AdTax_Percent, 0 as AdTax_Amount, Convert(float, 0) As CurrencyAdTaxAmount, Convert(float,0) as TotalAmount, " _
             & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,  Isnull(Recv_D.PackPrice,0) as PackPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments, 0 as PurchaseDemandId, 0 As PurchaseDemandDetailId, 0 as PurchaseOrderDetailId, IsNull(Recv_D.Qty, 0) As TotalQty FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
             & " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
          & " LEFT OUTER JOIN QuotationDetailTable ON Recv_D.QuotationDetailId = QuotationDetailTable.QuotationDetailId " _
          & " LEFT OUTER JOIN VendorQuotationDetail ON QuotationDetailTable.VendorQuotationDetailId = VendorQuotationDetail.VendorQuotationDetailId " _
             & " Where Recv_D.SalesOrderID =" & ReceivingID & ""
            FillInwardExpense(-1, "PO")

        ElseIf Condition = "LoadCMFADocument" Then
            'str = " SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.POQty,0)) AS Qty, Isnull(Recv_D.Price,0) as Price,  " _
            '     & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, 0 as TaxPercent, 0 as TaxAmount, Convert(float,0) as [Total Amount], " _
            '     & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, Isnull(Recv_D.Current_Price,0) as CurrentPrice, 0 as PackPrice, Isnull(Recv_D.PackDesc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments, 0 as PurchaseDemandId  FROM dbo.CMFADetailTable Recv_D INNER JOIN  " _
            '     & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN  " _
            '     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '     & " Where Recv_D.DocId =" & ReceivingID & " AND Recv_D.VendorId=" & Me.cmbVendor.Value & ""

            'TASK24 Added Field Color, Size, UOM
            'str = " SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.POQty,0)) AS Qty, Isnull(Recv_D.Price,0) as Price,  " _
            '     & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, 0 as TaxPercent, 0 as TaxAmount, Convert(float,0) as [Total Amount], " _
            '     & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, Isnull(Recv_D.Current_Price,0) as CurrentPrice, 0 as PackPrice, Isnull(Recv_D.PackDesc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments, 0 as PurchaseDemandId  FROM dbo.CMFADetailTable Recv_D INNER JOIN  " _
            ' & " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
            '     & " Where Recv_D.DocId =" & ReceivingID & " AND Recv_D.VendorId=" & Me.cmbVendor.Value & ""
            'END TASK24
            'TASK-TFS-51 Added Fields AdTax_Percent, AdTax_Amount
            'str = " SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.POQty,0)) AS Qty, Isnull(Recv_D.Price,0) as Price,  " _
            '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, 0 as TaxPercent, 0 as TaxAmount, 0 AS AdTax_Percent,0 AS AdTax_Amount, Convert(float,0) as [Total Amount], " _
            '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, Isnull(Recv_D.Current_Price,0) as CurrentPrice, 0 as PackPrice, Isnull(Recv_D.PackDesc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments, 0 as PurchaseDemandId,0 as PurchaseOrderDetailId   FROM dbo.CMFADetailTable Recv_D INNER JOIN  " _
            '    & " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
            '    & " Where Recv_D.DocId =" & ReceivingID & " AND Recv_D.VendorId=" & Me.cmbVendor.Value & ""
            str = " SELECT '' As SerialNo, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.POQty,0)) AS Qty,  Isnull(Recv_D.Current_Price,0) as CurrentPrice, Case When IsNull(Recv_D.CurrentPrice,0) > IsNull(Recv_D.Price,0) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Recv_D.Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent, Isnull(Recv_D.Price,0) as Price, 0 As BaseCurrencyId, 0 As BaseCurrencyRate, 0 As CurrencyId, 0 As CurrencyRate, 0 As CurrencyAmount, 0 As CurrencyTotalAmount, " _
        & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, 0 as TaxPercent, 0 as TaxAmount, 0 As CurrencyTaxAmount, 0 AS AdTax_Percent, 0 AS AdTax_Amount, 0 As CurrencyAdTaxAmount, Convert(float,0) as TotalAmount, " _
        & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, 0 as PackPrice, Isnull(Recv_D.PackDesc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments, 0 as PurchaseDemandId,0 as PurchaseOrderDetailId, IsNull(Recv_D.Qty, 0) As TotalQty FROM dbo.CMFADetailTable Recv_D INNER JOIN  " _
        & " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
        & " Where Recv_D.DocId =" & ReceivingID & " AND Recv_D.VendorId=" & Me.cmbVendor.Value & ""
            FillInwardExpense(-1, "PO")
        End If

        'END TASK-TFS-51
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

        '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(10), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9))

        '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
        '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

        'Next
        Dim dtDisplayDetail As New DataTable
        dtDisplayDetail = GetDataTable(str)
        Me.grd.DataSource = Nothing
        'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',((PackQty*Qty)*Price), Qty*Price)"
        'dtDisplayDetail.Columns("Total").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)"
        dtDisplayDetail.Columns("Total").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)* CurrencyRate"
        dtDisplayDetail.Columns("CurrencyAmount").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)"
        dtDisplayDetail.Columns("TaxAmount").Expression = "((TaxPercent/100)*Total)"
        dtDisplayDetail.Columns("CurrencyTaxAmount").Expression = "(((CurrencyAmount) * IsNull(TaxPercent,0))/100)"
        'TASK-TFS-51 Set Exparession For Additonal Tax
        dtDisplayDetail.Columns("AdTax_Amount").Expression = "((IsNull(AdTax_Percent,0)/100)*Total)"
        dtDisplayDetail.Columns("CurrencyAdTaxAmount").Expression = "(((CurrencyAmount) * IsNull(AdTax_Percent,0))/100)"
        dtDisplayDetail.Columns("TotalAmount").Expression = "([Total]+([TaxAmount]+[AdTax_Amount]))" 'Task:2374 Show Total Amount
        dtDisplayDetail.Columns("CurrencyTotalAmount").Expression = "IsNull([CurrencyAmount],0) + (IsNull([CurrencyTaxAmount],0)+IsNull([CurrencyAdTaxAmount],0))"
        'END TASK-TFS-51
        Me.grd.DataSource = dtDisplayDetail
        If dtDisplayDetail.Rows.Count > 0 Then
            If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                '' Being made editable against TASK TFS3493 on 07/06/18
                Me.cmbCurrency.Enabled = True
                If Not Me.cmbCurrency.SelectedIndex = -1 Then
                    Me.cmbCurrency.SelectedValue = 1

                End If
            Else
                'IsCurrencyEdit = True
                'IsNotCurrencyRateToAll = True
                'FillCombo("Currency")
                CurrencyRate = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString)
                Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                '' Being made editable against TASK TFS3493 on 07/06/18
                Me.cmbCurrency.Enabled = True
            End If
            cmbCurrency_SelectedIndexChanged(Nothing, Nothing)
            CurrencyRate = 1
        End If
        ApplyGridSetting()
        FillCombo("grdLocation")
        FillCombo("GrdStatus")


    End Sub
    '' ReqId-899 Added New Field of TaxPercent, TaxAmount
    Private Sub FillInwardExpense(ReceivingID As Integer, Optional DocType As String = "")
        Try
            'Dim dtInwardExpDetail As DataTable = GetDataTable("Select IsNull(PurchaseId,0) as PurchaseId, IsNull(coa.coa_detail_id,0)  as AccountId ,coa.detail_title, IsNull(Exp_Amount,0) as Exp_Amount From tblDefInwardAccounts INNER JOIN vwCOADetail coa on coa.coa_detail_id = tblDefInwardAccounts.InwardAccountId LEFT OUTER JOIN (select PurchaseId, AccountId,Exp_Amount From InwardExpenseDetailTable WHERE PurchaseId=" & ReceivingID & " AND DocType=N'" & DocType & "') Exp_D  ON Exp_D.AccountId = COA.coa_detail_id")
            Dim dtInwardExpDetail As DataTable = GetDataTable("Select Exp.PurchaseId, tblDefInwardAccounts.InwardAccountId as AccountId,vw.Detail_Title,IsNull(Exp_Amount,0) as Exp_Amount From vwCoaDetail vw INNER JOIN tblDefInwardAccounts on tblDefInwardAccounts.InwardAccountId = vw.coa_detail_id LEFT OUTER JOIN (select PurchaseId,AccountId,Exp_Amount From InwardExpenseDetailTable WHERE PurchaseId=" & ReceivingID & " AND DocType=N'" & DocType & "') Exp ON Exp.AccountId = vw.coa_detail_id")
            Me.grdInwardExpDetail.DataSource = dtInwardExpDetail
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function Update_Record() As Boolean
        If ApprovalProcessId = 0 Then
            'R:913 added Feasibility Post Rights
            If Me.chkPost.Visible = False Then
                Me.chkPost.Checked = False
            End If
            'End R:970
        Else
            Me.chkPost.Visible = False
        End If
        'Task:2673 Validtion CMFA Document
        Dim strCMFA As String = String.Empty
        Dim objDt As New DataTable
        If Me.cmbCMFADoc.SelectedIndex > 0 Then
            strCMFA = "Select CMFADetailTable.ArticleDefId, SUM(IsNull(Sz1,0)) as Qty, SUM(IsNull(POQty,0)-IsNull(PQty,0)) as PoQty From CMFADetailTable LEFT OUTER JOIN(Select ArticleDefId, IsNull(Sz1,0) as PQty From PurchaseOrderDetailTable WHERE PurchaseOrderId IN(Select PurchaseOrderId From PurchaseOrderMasterTable WHERE PurchaseOrderNo=N'" & Me.txtPONo.Text & "' AND RefCMFADocId=" & Me.cmbCMFADoc.SelectedValue & ")) PO On Po.ArticleDefId = CMFADetailTable.ArticleDefId  WHERE DocId=" & Me.cmbCMFADoc.SelectedValue & "   Group By CMFADetailTable.ArticleDefId "
            objDt = GetDataTable(strCMFA)
        End If
        'End Task:2673
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        gobjLocationId = MyCompanyId
        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'objCommand.CommandText = "Update PurchaseOrderMasterTable set PurchaseOrderNo =N'" & txtPONo.Text & "',PurchaseOrderDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '& " PurchaseOrderQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",PurchaseOrderAmount=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text & "',UserName=N'" & LoginUserName & "', LCID=" & Val(Me.cmbLC.Value) & ", CurrencyType=" & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & ", CurrencyRate=" & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", Receiving_Date=" & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Terms_And_Condition=N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "'  Where PurchaseOrderID= " & txtReceivingID.Text & " "
            ''ReqId-928 Solve Comma Error on Remarks
            'Before against request no. 913
            'objCommand.CommandText = "Update PurchaseOrderMasterTable set PurchaseOrderNo =N'" & txtPONo.Text & "',PurchaseOrderDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '           & " PurchaseOrderQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",PurchaseOrderAmount=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', LCID=" & Val(Me.cmbLC.Value) & ", CurrencyType=" & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & ", CurrencyRate=" & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", Receiving_Date=" & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Terms_And_Condition=N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "'  Where PurchaseOrderID= " & txtReceivingID.Text & " "
            'R:913 Added Column Post
            objCommand.CommandText = ""
            'Before against task:2673
            'objCommand.CommandText = "Update PurchaseOrderMasterTable set PurchaseOrderNo =N'" & txtPONo.Text & "',PurchaseOrderDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '           & " PurchaseOrderQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",PurchaseOrderAmount=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', LCID=" & Val(Me.cmbLC.Value) & ", CurrencyType=" & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & ", CurrencyRate=" & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", Receiving_Date=" & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Terms_And_Condition=N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & "  Where PurchaseOrderID= " & txtReceivingID.Text & " "
            'Task:2673 Added Field RefCMFADocId
            'Ali Faisal : TFS1300 : Add new column of inward expenses
            objCommand.CommandText = "Update PurchaseOrderMasterTable set PurchaseOrderNo =N'" & txtPONo.Text & "',PurchaseOrderDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
                       & " PurchaseOrderQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",PurchaseOrderAmount=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UpdateUserName=N'" & LoginUserName & "', LCID=" & Val(Me.cmbLC.Value) & ", CurrencyType=" & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & ", CurrencyRate=" & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", Receiving_Date=" & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Terms_And_Condition=N'" & ReplaceNewLine(Me.txtTerms_And_Condition.Text, False).Replace("'", "''") & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ",RefCMFADocId=" & IIf(Me.cmbCMFADoc.SelectedIndex = -1, 0, Me.cmbCMFADoc.SelectedValue) & ",CostCenterId=" & Me.cmbProject.SelectedValue & ",POType='" & Me.cmbPOType.SelectedItem.ToString & "',POStockDispatchStatus='" & Me.cmbStockDispatchStatus.Value & "',TotalInwardExpenses = '" & Me.grdInwardExpDetail.GetTotal(Me.grdInwardExpDetail.RootTable.Columns("Exp_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) & "',PayTypeId = " & Me.cmbPaymentTypes.SelectedValue & "  Where PurchaseOrderID= " & txtReceivingID.Text & ""
            'Ali Faisal : TFS1300 : End
            'End Task:2673
            objCommand.ExecuteNonQuery()


            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                SaveDocument(Val(txtReceivingID.Text), Me.Name, trans)
            End If

            'objCommand.CommandText = ""
            'objCommand.CommandText = "UPDATE PurchaseDemandDetailTable Set PurchaseDemandDetailTable.DeliveredQty=IsNull(PurchaseDemandDetailTable.DeliveredQty,0)-IsNull(PurchaseOrderDetailTable.Sz1,0), PurchaseDemandDetailTable.DeliveredTotalQty=IsNull(PurchaseDemandDetailTable.DeliveredTotalQty,0)-IsNull(PurchaseOrderDetailTable.Qty,0) From PurchaseDemandDetailTable, PurchaseOrderDetailTable, PurchaseOrderMasterTable WHERE PurchaseDemandDetailTable.ArticleDefId = PurchaseOrderDetailTable.ArticleDefId And PurchaseDemandDetailTable.ArticleSize = PurchaseOrderDetailTable.ArticleSize And PurchaseOrderMasterTable.PurchaseOrderId = PurchaseOrderDetailTable.PurchaseOrderId And PurchaseOrderDetailTable.DemandId = PurchaseDemandDetailTable.PurchaseDemandId And PurchaseOrderDetailTable.PurchaseDemandDetailId = PurchaseDemandDetailTable.PurchaseDemandDetailId AND PurchaseDemandDetailTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable WHERE PurchaseOrderId=" & Val(Me.txtReceivingID.Text) & " And IsNull(DemandId,0) <> 0) AND PurchaseOrderDetailTable.PurchaseOrderID=" & Val(txtReceivingID.Text) & ""
            'objCommand.ExecuteNonQuery()

            'objCommand.CommandText = ""
            'objCommand.CommandText = "Update PurchaseDemandMasterTable Set PurchaseDemandMasterTable.Status=Case When IsNull(DemandDt.BalanceQty,0) > 0 Then 'Open' Else 'Close' End  From PurchaseDemandMasterTable, (Select PurchaseDemandId, SUM(IsNull(Sz1,0)-IsNull(DeliveredQty,0)) as BalanceQty From PurchaseDemandDetailTable WHERE PurchaseDemandDetailTable.PurchaseDemandId In(Select DemandId From PurchaseOrderDetailTable where PurchaseOrderId=" & Val(Me.txtReceivingID.Text) & " And IsNull(DemandId,0) <> 0)  Group by PurchaseDemandId) DemandDt  WHERE DemandDt.PurchaseDemandId = PurchaseDemandMasterTable.PurchaseDemandId AND PurchaseDemandMasterTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=" & Val(Me.txtReceivingID.Text) & " And IsNull(DemandId,0) <> 0)"
            'objCommand.ExecuteNonQuery()

            objCommand.CommandText = String.Empty
            objCommand.CommandText = "SELECT  ISNULL(Sz1,0) as Qty, IsNull(ArticleDefID, 0) As ArticleDefID, ISNULL(DemandId,0) AS DemandId, IsNull(Qty,0) as TotalQty, IsNull(PurchaseDemandDetailId, 0) As PurchaseDemandDetailId FROM PurchaseOrderDetailTable WHERE  PurchaseOrderId = " & Me.grdSaved.CurrentRow.Cells("PurchaseOrderId").Value & ""
            Dim da As New OleDbDataAdapter(objCommand)
            Dim dtSavedItems As New DataTable
            da.Fill(dtSavedItems)
            dtSavedItems.AcceptChanges()


            If dtSavedItems.Rows.Count > 0 Then

                For Each r As DataRow In dtSavedItems.Rows
                    objCommand.CommandText = String.Empty
                    objCommand.CommandText = "Update PurchaseDemandDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & "), DeliveredTotalQty= abs(IsNull(DeliveredTotalQty,0) - " & r.Item(3) & ") where PurchaseDemandId = " & Val(r.Item("DemandId")) & " and ArticleDefID = " & r.Item(1) & " And PurchaseDemandDetailId = " & r.Item(4) & " "      'Task#29082015
                    objCommand.ExecuteNonQuery()
                Next
            End If



            objCommand.CommandText = String.Empty
            objCommand.CommandText = "Select distinct isnull(DemandId,0) as DemandId From PurchaseOrderDetailTable where PurchaseOrderId=" & Val(Me.grdSaved.CurrentRow.Cells("PurchaseOrderId").Value.ToString)
            Dim dt As New DataTable
            Dim da1 As New OleDbDataAdapter(objCommand)
            da1.Fill(dt)
            dt.AcceptChanges()
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        objCommand.CommandText = String.Empty
                        objCommand.CommandText = "Update PurchaseDemandMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where PurchaseDemandId = " & Val(r.Item("DemandId").ToString) & ""
                        objCommand.ExecuteNonQuery()
                    Next
                End If
            End If





            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from PurchaseOrderDetailTable where PurchaseOrderID = " & txtReceivingID.Text
            objCommand.ExecuteNonQuery()

            'objCommand.CommandText = ""

            'For i = 0 To grd.Rows.Count - 1
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "Insert into PurchaseOrderDetailTable (PurchaseOrderId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice) values( " _
            '                            & " " & txtReceivingID.Text & " ," & Val(grd.Rows(i).Cells(8).Value) & ",N'" & (grd.Rows(i).Cells(3).Value) & "'," & Val(grd.Rows(i).Cells(4).Value) & ", " _
            '                            & " " & IIf(grd.Rows(i).Cells(3).Value = "Loose", Val(grd.Rows(i).Cells(4).Value), (Val(grd.Rows(i).Cells(4).Value) * Val(grd.Rows(i).Cells(9).Value))) & ", " & Val(grd.Rows(i).Cells(5).Value) & ", " & Val(grd.Rows(i).Cells(9).Value) & "  , " & Val(grd.Rows(i).Cells(10).Value) & ") "

            '    objCommand.ExecuteNonQuery()
            For i = 0 To grd.RowCount - 1
                '' ReqId-928 Added Column Comments in Query
                'Task:2673 Validate If PO Qty Exceeded From CMFA Qty
                'Dim objDR() As DataRow
                'If Me.cmbCMFADoc.SelectedIndex > 0 Then
                '    objDR = objDt.Select("ArticleDefId=" & Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString & "")
                '    If objDR IsNot Nothing Then
                '        If objDR.Length > 0 Then
                '            If Val(objDR(0).ItemArray(1).ToString) < (Val(objDR(0).Item(2).ToString) + Val(Me.grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString)) Then
                '                Throw New Exception(" [" & Me.grd.GetRows(i).Cells(GrdEnum.Item).Value.ToString & "] PO qty exceeded from CMFA qty.")
                '            End If
                '        End If
                '    End If
                'End If
                'End Task:2673

                objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into PurchaseOrderDetailTable (PurchaseOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice,PackPrice, Pack_Desc,TaxPercent, Comments,DemandId) values( " _
                '                        & " " & txtReceivingID.Text & ", " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value) & " ," & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value) & ", " & Val(Me.grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & grd.GetRows(i).Cells(GrdEnum.Pack_Desc).Value.ToString.Replace("'", "''") & "',  " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("PurchaseDemandId").Value.ToString) & ") "


                'TASK-TFS-51 Added Fields AdTax_Percent, AdTax_Amount
                'objCommand.CommandText = "Insert into PurchaseOrderDetailTable (PurchaseOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice,PackPrice, Pack_Desc,TaxPercent, Comments,DemandId,AdTax_Percent,AdTax_Amount, PurchaseDemandDetailId, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, SerialNo) values( " _
                '                       & " " & txtReceivingID.Text & ", " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value) & " ," & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", " _
                '                       & " " & Val(grd.GetRows(i).Cells(GrdEnum.TotalQty).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value) & ", " & Val(Me.grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & grd.GetRows(i).Cells(GrdEnum.Pack_Desc).Value.ToString.Replace("'", "''") & "',  " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("PurchaseDemandId").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PurchaseDemandDetailId").Value.ToString) & ", N'" & grd.GetRows(i).Cells("SerialNo").Value.ToString.Replace("'", "''") & "') Select @@Identity"
                objCommand.CommandText = "Insert into PurchaseOrderDetailTable (PurchaseOrderId, LocationId, ArticleDefId,ArticleSize,Warranty,Status, Sz1,Qty,Price,Sz7,CurrentPrice,PackPrice, Pack_Desc,TaxPercent, Comments,DemandID, AdTax_Percent, AdTax_Amount, PurchaseDemandDetailId, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, SerialNo) values( " _
                                  & " " & txtReceivingID.Text & ", " & Val(grd.GetRows(i).Cells(GrdEnum.LocationId).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value) & "',N'" & (grd.GetRows(i).Cells("Warranty").Value) & "',N'" & (grd.GetRows(i).Cells("Status").Value.ToString) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", " _
                                  & " " & Val(grd.GetRows(i).Cells(GrdEnum.TotalQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value) & "," & Val(Me.grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & grd.GetRows(i).Cells(GrdEnum.Pack_Desc).Value.ToString.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "'," & Val(grd.GetRows(i).Cells("PurchaseDemandId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PurchaseDemandDetailId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", N'" & grd.GetRows(i).Cells("SerialNo").Value.ToString.Replace("'", "''") & "') Select @@Identity"
                'END TASK-TFS-51

                'END TASK-TFS-51


                Dim intPurchaseOrderDetailId As Integer = objCommand.ExecuteScalar()
                '' End ReqId-928
                'Val(grd.Rows(i).Cells(5).Value)

                objCommand.CommandText = ""
                objCommand.CommandText = "Update ReceivingNoteDetailTable Set PurchaseOrderDetailId=" & intPurchaseOrderDetailId & " WHERE PurchaseOrderDetailID=" & Val(Me.grd.GetRows(i).Cells("PurchaseOrderDetailID").Value.ToString) & ""
                objCommand.ExecuteNonQuery()



                '' 14-11-2016
                objCommand.CommandText = "UPDATE  PurchaseDemandDetailTable " _
                                               & " SET DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", DeliveredTotalQty= IsNull(DeliveredTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(GrdEnum.TotalQty).Value) & " " _
                                               & " WHERE     (PurchaseDemandId = " & Val(grd.GetRows(i).Cells(GrdEnum.PurchaseDemandId).Value) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ") And (PurchaseDemandDetailId =" & Val(grd.GetRows(i).Cells("PurchaseDemandDetailId").Value.ToString) & ")  "
                objCommand.ExecuteNonQuery()
                '' 14-11-2016


            Next


            objCommand.CommandText = "Select DISTINCT ISNULL(DemandID,0) as DemandId From PurchaseOrderDetailTable WHERE PurchaseOrderId=ident_current('PurchaseOrderMasterTable') AND ISNULL(Qty,0) <> 0"
            Dim dtPO As New DataTable
            Dim daPO As New OleDbDataAdapter(objCommand)
            daPO.Fill(dtPO)
            If dtPO IsNot Nothing Then
                If dtPO.Rows.Count > 0 Then
                    For Each row As DataRow In dtPO.Rows

                        'objCommand.CommandText = "Select SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & row("SO_ID") & " Having SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) > 0 "
                        objCommand.CommandText = "Select isnull(Sz1,0)-isnull(DeliveredQty , 0) as DeliveredQty from PurchaseDemandDetailTable where PurchaseDemandId = " & row("DemandId") & " And isnull(Sz1,0)-isnull(DeliveredQty , 0) > 0 "

                        Dim daPOQty As New OleDbDataAdapter(objCommand)
                        Dim dtPOQty As New DataTable
                        daPOQty.Fill(dtPOQty)
                        Dim blnEqual1 As Boolean = True
                        If dtPOQty.Rows.Count > 0 Then
                            'For Each r As DataRow In dtPOQty.Rows
                            'If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
                            blnEqual1 = False
                            'Exit For
                            'End If
                            ' Next
                        End If
                        If blnEqual1 = True Then
                            objCommand.CommandText = "Update PurchaseDemandMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where PurchaseDemandId = " & row("DemandId") & ""
                            objCommand.ExecuteNonQuery()
                        Else
                            objCommand.CommandText = "Update PurchaseDemandMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where PurchaseDemandId = " & row("DemandId") & ""
                            objCommand.ExecuteNonQuery()
                        End If
                    Next
                End If
            End If


            objCommand.CommandText = ""
            objCommand.CommandText = "Delete From InwardExpenseDetailTable WHERE PurchaseId=" & Val(Me.txtReceivingID.Text) & " AND Doctype='PO'"
            objCommand.ExecuteNonQuery()

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdInwardExpDetail.GetRows
                If Val(r.Cells("Exp_Amount").Value.ToString) <> 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO InwardExpenseDetailTable(PurchaseId, AccountId, Exp_Amount,DocType) Values(" & Val(Me.txtReceivingID.Text) & ", " & Val(r.Cells("AccountId").Value.ToString) & ", " & Val(r.Cells("Exp_Amount").Value.ToString) & ",'PO')"
                    objCommand.ExecuteNonQuery()
                End If
            Next


            'Task:2673 Update POQty And Set Status Open Or Close
            If Me.cmbCMFADoc.SelectedIndex > 0 Then

                objCommand.CommandText = ""
                objCommand.CommandText = "UPDATE CMFADetailTable SET POQty = IsNull(a.Qty,0) " _
                 & " FROM (SELECT dbo.PurchaseOrderMasterTable.VendorId, dbo.PurchaseOrderMasterTable.RefCMFADocId, SUM(dbo.PurchaseOrderDetailTable.Sz1) AS Qty, " _
                 & " dbo.PurchaseOrderDetailTable.ArticleDefId, dbo.PurchaseOrderDetailTable.ArticleSize " _
                 & " FROM dbo.PurchaseOrderDetailTable INNER JOIN " _
                 & " dbo.PurchaseOrderMasterTable ON dbo.PurchaseOrderDetailTable.PurchaseOrderId = dbo.PurchaseOrderMasterTable.PurchaseOrderId " _
                 & " WHERE(ISNULL(dbo.PurchaseOrderMasterTable.RefCMFADocId, 0)=" & Me.cmbCMFADoc.SelectedValue & ") " _
                 & " GROUP BY dbo.PurchaseOrderMasterTable.VendorId, dbo.PurchaseOrderMasterTable.RefCMFADocId, dbo.PurchaseOrderDetailTable.ArticleDefId, dbo.PurchaseOrderDetailTable.ArticleSize) a " _
                 & " WHERE " _
                 & " a.ArticleDefId = CMFADetailTable.ArticleDefId " _
                 & " AND CMFADetailTable.DocId = a.RefCMFADocId  " _
                 & " AND CMFADetailTable.VendorId = a.VendorId " _
                 & " AND CMFADetailTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
                objCommand.ExecuteNonQuery()

                objCommand.CommandText = ""
                objCommand.CommandText = "UPDATE CMFAMasterTable SET Status=CASE WHEN a.BalanceQty <=0 Then 0 ELSE 1 end From( " _
                           & " SELECT dbo.CMFAMasterTable.DocId, SUM(ISNULL(dbo.CMFADetailTable.Sz1, 0) - ISNULL(dbo.CMFADetailTable.POQty, 0)) AS BalanceQty " _
                           & " FROM dbo.CMFADetailTable INNER JOIN " _
                           & " dbo.CMFAMasterTable ON dbo.CMFADetailTable.DocId = dbo.CMFAMasterTable.DocId WHERE dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & " " _
                           & " GROUP BY dbo.CMFAMasterTable.DocId) a " _
                           & " WHERE(a.DocId = CMFAMasterTable.DocId) AND dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
                objCommand.ExecuteNonQuery()

            End If
            'End Task:2673


            'If Me.cmbPurchaseDemand.SelectedIndex > 0 Then

            objCommand.CommandText = ""

            'objCommand.CommandText = "UPDATE PurchaseDemandDetailTable Set PurchaseDemandDetailTable.DeliveredQty=IsNull(PurchaseDemandDetailTable.DeliveredQty,0)+IsNull(PurchaseOrderDetailTable.Sz1,0) From PurchaseDemandDetailTable, PurchaseOrderDetailTable, PurchaseOrderMasterTable WHERE PurchaseDemandDetailTable.ArticleDefId = PurchaseOrderDetailTable.ArticleDefId And PurchaseDemandDetailTable.ArticleSize = PurchaseOrderDetailTable.ArticleSize And PurchaseOrderMasterTable.PurchaseOrderId = PurchaseOrderDetailTable.PurchaseOrderId And PurchaseOrderDetailTable.DemandId = PurchaseDemandDetailTable.PurchaseDemandId And PurchaseOrderDetailTable.PurchaseDemandDetailId = PurchaseDemandDetailTable.PurchaseDemandDetailId AND PurchaseDemandDetailTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=" & Val(Me.txtReceivingID.Text) & " And IsNull(DemandId,0) <> 0)  AND PurchaseOrderDetailTable.PurchaseOrderID=" & Val(txtReceivingID.Text) & ""
            '' Replaced Sz1 with Qty to set TotalQty into Qty against TASK-408
            'TFS3516: Waqar Raza: Commented this line because it was saving the Deliverd Qty on PurchaseDemandDetailTable after Adding it with Qty of Purchase Order Screen
            'Start TFS3516
            'objCommand.CommandText = "UPDATE PurchaseDemandDetailTable Set PurchaseDemandDetailTable.DeliveredQty=IsNull(PurchaseDemandDetailTable.DeliveredQty,0)+IsNull(PurchaseOrderDetailTable.Sz1,0), PurchaseDemandDetailTable.DeliveredTotalQty=IsNull(PurchaseDemandDetailTable.DeliveredTotalQty,0)+IsNull(PurchaseOrderDetailTable.Qty,0) From PurchaseDemandDetailTable, PurchaseOrderDetailTable, PurchaseOrderMasterTable WHERE PurchaseDemandDetailTable.ArticleDefId = PurchaseOrderDetailTable.ArticleDefId And PurchaseDemandDetailTable.ArticleSize = PurchaseOrderDetailTable.ArticleSize And PurchaseOrderMasterTable.PurchaseOrderId = PurchaseOrderDetailTable.PurchaseOrderId And PurchaseOrderDetailTable.DemandId = PurchaseDemandDetailTable.PurchaseDemandId And PurchaseOrderDetailTable.PurchaseDemandDetailId = PurchaseDemandDetailTable.PurchaseDemandDetailId AND PurchaseDemandDetailTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=" & Val(Me.txtReceivingID.Text) & " And IsNull(DemandId,0) <> 0)  AND PurchaseOrderDetailTable.PurchaseOrderID=" & Val(txtReceivingID.Text) & ""
            'objCommand.CommandText = "UPDATE PurchaseDemandDetailTable Set PurchaseDemandDetailTable.DeliveredQty=IsNull(PurchaseOrderDetailTable.Sz1,0), PurchaseDemandDetailTable.DeliveredTotalQty=IsNull(PurchaseOrderDetailTable.Qty,0) From PurchaseDemandDetailTable, PurchaseOrderDetailTable, PurchaseOrderMasterTable WHERE PurchaseDemandDetailTable.ArticleDefId = PurchaseOrderDetailTable.ArticleDefId And PurchaseDemandDetailTable.ArticleSize = PurchaseOrderDetailTable.ArticleSize And PurchaseOrderMasterTable.PurchaseOrderId = PurchaseOrderDetailTable.PurchaseOrderId And PurchaseOrderDetailTable.DemandId = PurchaseDemandDetailTable.PurchaseDemandId And PurchaseOrderDetailTable.PurchaseDemandDetailId = PurchaseDemandDetailTable.PurchaseDemandDetailId AND PurchaseDemandDetailTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=" & Val(Me.txtReceivingID.Text) & " And IsNull(DemandId,0) <> 0)  AND PurchaseOrderDetailTable.PurchaseOrderID=" & Val(txtReceivingID.Text) & ""
            ''End TFS3516:
            'objCommand.ExecuteNonQuery()

            'objCommand.CommandText = ""
            ' ''Replaced Sz1 with Qty for TASK-408
            'objCommand.CommandText = "Update PurchaseDemandMasterTable Set PurchaseDemandMasterTable.Status=Case When IsNull(DemandDt.BalanceQty,0) > 0 Then 'Open' Else 'Close' End  From PurchaseDemandMasterTable, (Select PurchaseDemandId, SUM(IsNull(Sz1,0)-IsNull(DeliveredQty,0)) as BalanceQty From PurchaseDemandDetailTable WHERE PurchaseDemandDetailTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=" & Val(Me.txtReceivingID.Text) & " And IsNull(DemandId,0) <> 0) Group by PurchaseDemandId) DemandDt  WHERE DemandDt.PurchaseDemandId = PurchaseDemandMasterTable.PurchaseDemandId AND PurchaseDemandMasterTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=" & Val(Me.txtReceivingID.Text) & " And IsNull(DemandId,0) <> 0)"
            'objCommand.ExecuteNonQuery()

            'End If

            'Ali FAisal :TFS1442 : Insert scheduled payment
            If Me.cmbPaymentTypes.SelectedIndex > 0 Then
                objCommand.CommandText = ""
                objCommand.CommandText = "Delete From tblPaymentSchedule WHERE OrderId=" & txtReceivingID.Text & ""
                objCommand.ExecuteNonQuery()
                objCommand.CommandText = ""
                Dim str As String = "SELECT Id, TermDays FROM tblPaymentTerms WHERE Id = " & Me.cmbPaymentTypes.SelectedValue & ""
                Dim dtDays As DataTable = GetDataTable(str)
                Dim Days As Integer = 0I
                If dtDays.Rows.Count > 0 Then
                    Days = Convert.ToInt32(dtDays.Rows(0).Item(1))
                End If
                objCommand.CommandText = "INSERT INTO tblPaymentSchedule(PayTypeId, SchDate, OrderId, OrderType, Amount, InitialSchDate, PaymentStatus) VALUES(" & Me.grdSaved.GetRow.Cells("PayTypeId").Value & ", N'" & Me.dtpPODate.Value.AddDays(Days) & "', " & txtReceivingID.Text & ", N'PO', " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", N'" & Me.dtpPODate.Value.AddDays(Days) & "', 'UnPaid')"
                objCommand.ExecuteNonQuery()
            End If
            'Ali FAisal :TFS1442 : End

            trans.Commit()
            Update_Record = True
            'InsertVoucher()
            getVoucher_Id = Me.txtReceivingID.Text
            setVoucherNo = Me.txtPONo.Text
            setEditMode = True
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try
        UpdateStatusInEdit(Val(txtReceivingID.Text))
        ''insert Activity Log
        SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Purchase, Me.txtPONo.Text.Trim, True)
        ''Start TFS2989
        If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim, Me.Name) Then
            If ValidateApprovalProcessIsInProgressAgain(Me.txtPONo.Text.Trim, Me.Name) = False Then
                SaveApprovalLog(EnumReferenceType.PurchaseOrder, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Purchase Order ," & cmbVendor.Text & "", Me.Name, 0)
            End If
        End If
        ''End TFS2989
        SendSMS()
    End Function

    Public Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.ButtonClick
        If Me.BtnSave.Enabled = False Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
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

                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()

                    If Save() Then
                        SendAutoEmail()
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
                        '-------------------------------------
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop

                    Else
                        Exit Sub 'MsgBox("Record has not been added")
                    End If
                Else
                    If IsValidToDelete("ReceivingMasterTable", "PurchaseOrderID", Me.grdSaved.CurrentRow.Cells("PurchaseOrderId").Value.ToString, "ReceivingNo", "SRN", 3) = True And IsValidToDelete("ReceivingNoteDetailTable", "PO_ID", Me.grdSaved.CurrentRow.Cells("PurchaseOrderId").Value.ToString) = True Then
                        If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
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
                            '-------------------------------------
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
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate("Purchase Order")
            If EmailTemplate.Length > 0 Then
                GetAutoEmailData()
                'GetVendorsEmails() ''Commented Against TFS3239
                'FillDataSet()
                UsersEmail = New List(Of String)
                'UsersEmail.Add("adil@agriusit.com")
                ''UsersEmail.Add("ali@agriusit.com")
                UsersEmail.Add("h.saeed@agriusit.com")
                ''UsersEmail.Add("a.rafay@agriusit.com")
                FormatStringBuilder(dtEmail)
                'CreateOutLookMail()
                For Each _email As String In UsersEmail
                    CreateOutLookMail(_email)
                    SaveEmailLog(PurchaseOrderNo, _email, "frmPurchaseOrderNew", Activity)
                Next
                'SaveCCBCC(CC, BCC)
                'CC = ""
                'BCC = ""
            Else
                ShowErrorMessage("No email template is found for Purchase Order.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetAutoEmailData()
        Dim Dr As DataRow
        Try
            Dim str As String
            str = "select TOP 1 PurchaseOrderId, PurchaseOrderNo, PurchaseOrderDate from PurchaseOrderMasterTable order by 1 desc"
            Dim dt1 As DataTable = GetDataTable(str)
            If dt1.Rows.Count > 0 Then
                PurchaseOrderId = dt1.Rows(0).Item("PurchaseOrderId")
                PurchaseOrderNo = dt1.Rows(0).Item("PurchaseOrderNo")
            End If
            Dim str1 As String
            str1 = "SELECT Recv_D.SerialNo,tblVendor.VendorName,Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.CurrentPrice,  Case When IsNull(Recv_D.CurrentPrice,0) > IsNull(Price,0) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent,Recv_D.Price, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As CurrencyTotalAmount, " _
          & " (IsNull(Recv_D.Qty, 0)*IsNull(Recv_D.Price, 0)* Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, Isnull(Recv_D.TaxPercent,0) as TaxPercent, 0 as TaxAmount, Convert(float, 0) As CurrencyTaxAmount, IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent, IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount,  Convert(float, 0) As CurrencyAdTaxAmount, Convert(float,0) as TotalAmount, " _
          & " Recv_D.Sz7 as PackQty, isnull(Recv_D.PackPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, IsNull(Recv_D.Qty, 0) As TotalQty  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
          & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId inner join" _
          & " PurchaseOrderMasterTable ON Recv_D.PurchaseOrderId = PurchaseOrderMasterTable.PurchaseOrderId left outer join" _
          & " tblVendor ON PurchaseOrderMasterTable.VendorId = tblVendor.AccountId " _
          & " Where Recv_D.PurchaseOrderID =" & PurchaseOrderId & ""
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

    'Private Sub CreateOutLookMail(Optional ByVal _AutoEmail As Boolean = False, Optional ByVal DocumentNo As String = "")
    '    Try
    '        Dim oApp As Outlook.Application = New Outlook.Application
    '        Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
    '        mailItem.Subject = PurchaseOrderNo
    '        mailItem.To = VendorEmails
    '        mailItem.Importance = Outlook.OlImportance.olImportanceNormal
    '        If _AutoEmail = False Then
    '            mailItem.Display(mailItem)
    '            mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
    '            EmailBody = html.ToString

    '        Else
    '            mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
    '            EmailBody = html.ToString
    '            mailItem.Send()
    '        End If
    '        mailItem = Nothing
    '        oApp = Nothing
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub FillDataSet()
        Dim Dr As DataRow
        Dim SalesInquiryNo As String = String.Empty
        Dim PreviousSalesInquiryNo As String = String.Empty
        Dim dtEmailNew As DataTable
        Try
            dsEmail = New DataSet()
            For Each Row As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                SalesInquiryNo = Row.Cells("SalesInquiryNo").Value.ToString
                If SalesInquiryNo = PreviousSalesInquiryNo Then
                Else
                    PreviousSalesInquiryNo = SalesInquiryNo
                    dtEmailNew = New DataTable(SalesInquiryNo)
                    For Each Colum As DataColumn In dtEmail.Columns
                        dtEmailNew.Columns.Add(Colum.ColumnName)
                    Next
                    If dsEmail.Tables.Contains(SalesInquiryNo) = False Then
                        dsEmail.Tables.Add(dtEmailNew)
                    End If
                End If
                Dr = dtEmailNew.NewRow
                For Each col As String In AllFields
                    If Row.Table.Columns.Contains(col) Then
                        Dr.Item(col) = Row.Cells(col).Value.ToString
                    End If
                Next
                dsEmail.Tables(SalesInquiryNo).Rows.Add(Dr)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.Cursor = Cursors.WaitCursor
        'Dim s As String
        's = "1234-567-890"
        'MsgBox(Microsoft.VisualBasic.Right(s, InStr(1, s, "-") - 2))

        If Me.grd.RowCount > 0 Then
            If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
        End If

        RefreshControls()
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSaved_CellDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        'Task#117062015 
        Try
            If Me.grdSaved.Row < 0 Then
                Exit Sub
            Else
                EditRecord()
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        'End Task#117062015
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
        '    Me.cmbVendor.Value = dt.Rows(0).Item("VendorCode")
        '    Me.cmbVendor.Enabled = False

        'Else
        '    Me.cmbVendor.Enabled = True
        '    Me.cmbVendor.Rows(0).Activate()
        'End If
    End Sub

    Private Sub grd_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        With Me.grd.CurrentRow

            If Me.grd.CurrentRow.Cells("Unit").Value = "Loose" Then
                'txtPackQty.Text = 1
                .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value)
            Else
                .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value) * Val(.Cells("PackQty").Value)
            End If
            GetTotal()
        End With
    End Sub

    Private Sub cmbItem_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbItem.KeyDown
        Try
            ''TFS1858 : Ayesha Rehman :Item dropdown shall be searchable
            If e.KeyCode = Keys.F1 Then
                If flgCompanyRights = True Then
                    frmItemSearch.CompanyId = MyCompanyId
                End If
                If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                    If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                        frmItemSearch.LocationId = Me.cmbCategory.SelectedValue
                    Else
                        frmItemSearch.LocationId = 0
                    End If
                End If

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

    Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            End If
            'ClearDetailControls() ''Commented Against TFS4705
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            Me.txtCurrentRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            'Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            If Val(txtDisc.Text) > 0 Then
                If Val(Me.txtCurrentRate.Text) > 0 Then
                    Me.txtRate.Text = Math.Round(Val(txtCurrentRate.Text) - (Val(Me.txtDisc.Text) / 100) * Val(Me.txtCurrentRate.Text), TotalAmountRounding)
                Else
                    Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
                End If
            Else
                Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            End If
            'If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1 ' Before  ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            Me.txtQty.Text = 1 ' After ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            Me.txtLastPrice.Text = Math.Round(LastPrice(Me.cmbVendor.Value, Me.cmbItem.Value), TotalAmountRounding)
            'Me.cmbVendor.DisplayLayout.Grid.Show( me.cmbVendor.contr)
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
        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        ''Start TFS2988 : Ayesha Rehman : 09-04-2018
        If IsEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document Can Not be Changed ,because Approval Process is in Progress") : Exit Sub
                End If
            End If
        End If
        ''End TFS2988
        If IsValidToDelete("ReceivingMasterTable", "PurchaseOrderID", Me.grdSaved.CurrentRow.Cells("PurchaseOrderId").Value.ToString, "ReceivingNo", "SRN", 3) = True And IsValidToDelete("ReceivingNoteDetailTable", "PO_ID", Me.grdSaved.CurrentRow.Cells("PurchaseOrderId").Value.ToString) = True Then

            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Me.Cursor = Cursors.WaitCursor
            Try
                'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Dim cm As New OleDbCommand
                Dim objTrans As OleDbTransaction
                If Con.State = ConnectionState.Closed Then Con.Open()
                objTrans = Con.BeginTransaction




                'Task:2673 Update POQty And Set Status Open Or Close
                If Me.cmbCMFADoc.SelectedIndex > 0 Then

                    cm.CommandText = ""
                    cm.Connection = Con
                    cm.Transaction = objTrans
                    cm.CommandText = "UPDATE CMFADetailTable SET POQty = IsNull(POQty,0)-IsNull(a.Qty,0) " _
                     & " FROM (SELECT dbo.PurchaseOrderMasterTable.VendorId, dbo.PurchaseOrderMasterTable.RefCMFADocId, SUM(dbo.PurchaseOrderDetailTable.Sz1) AS Qty, " _
                     & " dbo.PurchaseOrderDetailTable.ArticleDefId, dbo.PurchaseOrderDetailTable.ArticleSize " _
                     & " FROM dbo.PurchaseOrderDetailTable INNER JOIN " _
                     & " dbo.PurchaseOrderMasterTable ON dbo.PurchaseOrderDetailTable.PurchaseOrderId = dbo.PurchaseOrderMasterTable.PurchaseOrderId " _
                     & " WHERE(ISNULL(dbo.PurchaseOrderMasterTable.RefCMFADocId, 0)=" & Me.cmbCMFADoc.SelectedValue & ") AND (PurchaseOrderDetailTable.PurchaseOrderId=" & Me.grdSaved.CurrentRow.Cells("PurchaseOrderId").Value.ToString & ") " _
                     & " GROUP BY dbo.PurchaseOrderMasterTable.VendorId, dbo.PurchaseOrderMasterTable.RefCMFADocId, dbo.PurchaseOrderDetailTable.ArticleDefId, dbo.PurchaseOrderDetailTable.ArticleSize) a " _
                     & " WHERE " _
                     & " a.ArticleDefId = CMFADetailTable.ArticleDefId " _
                     & " AND CMFADetailTable.DocId = a.RefCMFADocId  " _
                     & " AND CMFADetailTable.VendorId = a.VendorId " _
                     & " AND (CMFADetailTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ")"
                    cm.ExecuteNonQuery()

                    cm.CommandText = ""
                    cm.Connection = Con
                    cm.Transaction = objTrans
                    cm.CommandText = "UPDATE CMFAMasterTable SET Status=CASE WHEN a.BalanceQty <=0 Then 0 ELSE 1 end From( " _
                               & " SELECT dbo.CMFAMasterTable.DocId, SUM(ISNULL(dbo.CMFADetailTable.Sz1, 0) - ISNULL(dbo.CMFADetailTable.POQty, 0)) AS BalanceQty " _
                               & " FROM dbo.CMFADetailTable INNER JOIN " _
                               & " dbo.CMFAMasterTable ON dbo.CMFADetailTable.DocId = dbo.CMFAMasterTable.DocId WHERE dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & " " _
                               & " GROUP BY dbo.CMFAMasterTable.DocId) a " _
                               & " WHERE(a.DocId = CMFAMasterTable.DocId) AND dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
                    cm.ExecuteNonQuery()

                End If
                'End Task:2673


                'If Me.cmbPurchaseDemand.SelectedIndex > 0 Then

                'cm.Connection = Con
                'cm.Transaction = objTrans
                'cm.CommandText = ""
                'cm.CommandText = "UPDATE PurchaseDemandDetailTable Set PurchaseDemandDetailTable.DeliveredQty=IsNull(PurchaseDemandDetailTable.DeliveredQty,0)-IsNull(PurchaseOrderDetailTable.Sz1,0),  PurchaseDemandDetailTable.DeliveredTotalQty=IsNull(PurchaseDemandDetailTable.DeliveredTotalQty,0)-IsNull(PurchaseOrderDetailTable.Qty,0) From PurchaseDemandDetailTable, PurchaseOrderDetailTable, PurchaseOrderMasterTable WHERE PurchaseDemandDetailTable.ArticleDefId = PurchaseOrderDetailTable.ArticleDefId And PurchaseDemandDetailTable.ArticleSize = PurchaseOrderDetailTable.ArticleSize And PurchaseOrderMasterTable.PurchaseOrderId = PurchaseOrderDetailTable.PurchaseOrderId And PurchaseOrderDetailTable.DemandId = PurchaseDemandDetailTable.PurchaseDemandId And PurchaseOrderDetailTable.PurchaseDemandDetailId = PurchaseDemandDetailTable.PurchaseDemandDetailId AND PurchaseDemandDetailTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=" & Val(Me.grdSaved.GetRow.Cells("PurchaseOrderId").Value.ToString) & " And IsNull(DemandId,0) <> 0)  AND PurchaseOrderDetailTable.PurchaseOrderID=" & Val(Me.grdSaved.GetRow.Cells("PurchaseOrderId").Value.ToString) & ""
                'cm.ExecuteNonQuery()

                'cm.CommandText = ""
                'cm.Connection = Con
                'cm.Transaction = objTrans
                'cm.CommandText = ""
                'cm.CommandText = "Update PurchaseDemandMasterTable Set PurchaseDemandMasterTable.Status=Case When IsNull(DemandDt.BalanceQty,0) > 0 Then 'Open' Else 'Close' End  From PurchaseDemandMasterTable, (Select PurchaseDemandId, SUM(IsNull(Sz1,0)-IsNull(DeliveredQty,0)) as BalanceQty From PurchaseDemandDetailTable WHERE PurchaseDemandDetailTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=" & Val(Me.grdSaved.GetRow.Cells("PurchaseOrderId").Value.ToString) & " And IsNull(DemandId,0) <> 0)  Group by PurchaseDemandId) DemandDt  WHERE DemandDt.PurchaseDemandId = PurchaseDemandMasterTable.PurchaseDemandId AND PurchaseDemandMasterTable.PurchaseDemandId In(Select Distinct DemandId From PurchaseOrderDetailTable where PurchaseOrderId=" & Val(Me.grdSaved.GetRow.Cells("PurchaseOrderId").Value.ToString) & " And IsNull(DemandId,0) <> 0)"
                'cm.ExecuteNonQuery()

                'End If

                cm.CommandText = String.Empty
                cm.Connection = Con
                cm.Transaction = objTrans
                cm.CommandText = "SELECT  ISNULL(Sz1,0) as Qty, IsNull(ArticleDefID, 0) As ArticleDefID, ISNULL(DemandId,0) AS DemandId, IsNull(Qty,0) as TotalQty, IsNull(PurchaseDemandDetailId, 0) As PurchaseDemandDetailId FROM PurchaseOrderDetailTable WHERE  PurchaseOrderId = " & Me.grdSaved.CurrentRow.Cells("PurchaseOrderId").Value & ""
                Dim da As New OleDbDataAdapter(cm)
                Dim dtSavedItems As New DataTable
                da.Fill(dtSavedItems)
                dtSavedItems.AcceptChanges()


                If dtSavedItems.Rows.Count > 0 Then

                    For Each r As DataRow In dtSavedItems.Rows
                        cm.CommandText = String.Empty
                        cm.Connection = Con
                        cm.Transaction = objTrans
                        cm.CommandText = "Update PurchaseDemandDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & "), DeliveredTotalQty= abs(IsNull(DeliveredTotalQty,0) - " & r.Item(3) & ") where PurchaseDemandId = " & Val(r.Item("DemandId")) & " and ArticleDefID = " & r.Item(1) & " And PurchaseDemandDetailId = " & r.Item(4) & " "      'Task#29082015
                        cm.ExecuteNonQuery()
                    Next
                End If



                cm.CommandText = String.Empty
                cm.Connection = Con
                cm.Transaction = objTrans
                cm.CommandText = "Select distinct isnull(DemandId,0) as DemandId From PurchaseOrderDetailTable where PurchaseOrderId=" & Val(Me.grdSaved.CurrentRow.Cells("PurchaseOrderId").Value.ToString)
                Dim dt As New DataTable
                Dim da1 As New OleDbDataAdapter(cm)
                da1.Fill(dt)
                dt.AcceptChanges()
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        For Each r As DataRow In dt.Rows
                            cm.CommandText = String.Empty
                            cm.Connection = Con
                            cm.Transaction = objTrans
                            cm.CommandText = "Update PurchaseDemandMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where PurchaseDemandId = " & Val(r.Item("DemandId").ToString) & ""
                            cm.ExecuteNonQuery()
                        Next
                    End If
                End If




                cm.Connection = Con
                cm.CommandText = "delete from PurchaseOrderDetailTable where PurchaseOrderid=" & Me.grdSaved.CurrentRow.Cells("PurchaseOrderID").Value.ToString
                cm.Transaction = objTrans
                cm.ExecuteNonQuery()

                cm = New OleDbCommand
                cm.Connection = Con
                cm.CommandText = "delete from PurchaseOrderMasterTable where PurchaseOrderid=" & Me.grdSaved.CurrentRow.Cells("PurchaseOrderID").Value.ToString

                cm.Transaction = objTrans

                cm.ExecuteNonQuery()
                objTrans.Commit()
                'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                'msg_Information(str_informDelete)
                'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
                Me.grdSaved.CurrentRow.Delete()

                Me.txtReceivingID.Text = 0


            Catch ex As Exception
                msg_Error("Error occured while deleting record: " & ex.Message)

            Finally
                Con.Close()
                Me.Cursor = Cursors.Default
                Me.lblProgress.Visible = False
            End Try



            ''insert Activity Log
            SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Purchase, grdSaved.CurrentRow.Cells(0).Value.ToString, True)


            Me.RefreshControls()

        Else

            msg_Error(str_ErrorDependentRecordFound)
        End If


    End Sub
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.ButtonClick
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("PurchaseOrderNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            ShowReport("PurchaseOrder", "{PurchaseOrderMasterTable.PurchaseOrderId}=" & grdSaved.CurrentRow.Cells("PurchaseOrderId").Value, , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grd_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        Me.GetTotal()
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.chkPost.Visible = True 'R:913 Set Post View On
                If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True 'R:M6 Set Default Checked
                Me.ToolStripButton2.Enabled = True
                'Task 1592 Apply future date rights
                IsDateChangeAllowed = True
                dtpPODate.MaxDate = Date.Today.AddMonths(3)
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmPurchaseOrder)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                'UserPriceAllowedRights = GetUserPriceAllowedRights(LoginUserId)
                'If UserPriceAllowedRights = True Then
                '    Me.pnlRateHidden.Visible = True
                '    Me.grd.RootTable.Columns("Price").Visible = True
                '    Me.grd.RootTable.Columns("Total").Visible = True
                'Else
                '    Me.pnlRateHidden.Visible = False
                '    Me.grd.RootTable.Columns("Price").Visible = False
                '    Me.grd.RootTable.Columns("Total").Visible = False
                'End If
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.btnSearchDelete.Enabled = False
                Me.btnSearchPrint.Enabled = False
                Me.BtnPrint.Enabled = False
                'Task 1592 Apply future date rights
                IsDateChangeAllowed = False
                DateChange(False)
                Me.ToolStripButton2.Enabled = False
                'Me.pnlRateHidden.Visible = False
                Me.grd.RootTable.Columns("Price").Visible = False
                Me.grd.RootTable.Columns("Total").Visible = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar2.mGridExport.Enabled = False ''TFS1823
                Me.chkPost.Visible = False 'R:913 Set Post View Off
                If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False 'Task:2406 Added Field Chooser Rights
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
                        Me.btnSearchPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        CtrlGrdBar2.mGridExport.Enabled = True  ''TFS1823
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True 'R:913 
                        If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True 'R:M6 Set Default Checked 
                    ElseIf RightsDt.FormControlName = "Price Allow" Then
                        ' Me.pnlRateHidden.Visible = True
                        Me.grd.RootTable.Columns("Price").Visible = True
                        Me.grd.RootTable.Columns("Total").Visible = True
                        'Task:1592 Added Future Date Rights
                    ElseIf RightsDt.FormControlName = "Future Transaction" Then
                        IsDateChangeAllowed = True
                        DateChange(True)
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    ElseIf RightsDt.FormControlName = "Load All" Then
                        Me.ToolStripButton2.Enabled = True
                    ElseIf RightsDt.FormControlName = "Service Items" Then
                        Me.rbtService.Checked = True
                        AllItems()
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
    Private Sub TableLayoutPanel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub
    'Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
    '    If Me.UltraTabControl1.SelectedTab.Index = 0 Then
    '        Me.BtnLoaddAll.Visible = False
    '        Me.ToolStripButton1.Visible = False
    '        Me.ToolStripSeparator1.Visible = False
    '    Else
    '        Me.BtnLoaddAll.Visible = True
    '        Me.ToolStripButton1.Visible = True
    '        Me.ToolStripSeparator1.Visible = True
    '    End If
    'End Sub
    'Private Sub BtnLoaddAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoaddAll.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    DisplayRecord("All")
    '    Me.DisplayDetail(-1)
    '    Me.Cursor = Cursors.Default
    'End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 
            'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''END TFS4544
            Dim id As Integer = 0

            id = Me.cmbVendor.SelectedRow.Cells(0).Value
            FillCombo("Vendor")
            Me.cmbVendor.Value = id

            id = Me.cmbPo.SelectedValue
            FillCombo("SO")
            Me.cmbPo.SelectedValue = id

            id = Me.cmbCategory.SelectedValue
            FillCombo("Category")
            Me.cmbCategory.SelectedValue = id

            id = Me.cmbItem.SelectedRow.Cells(0).Value
            FillCombo("Item")
            Me.cmbItem.Value = id

            FillCombo("grdLocation")

            id = Me.cmbCurrency.SelectedIndex
            FillCombo("Currency")
            Me.cmbCurrency.SelectedIndex = id

            'Rafay
            id = Me.cmbStatus.SelectedIndex
            FillCombo("Status")
            Me.cmbStatus.SelectedIndex = id
            'rafay

            ''R933 Call LC List
            id = Me.cmbCurrency.SelectedIndex
            FillCombo("LC")
            Me.cmbCurrency.SelectedIndex = id
            'End R933 
            id = Me.cmbProject.SelectedIndex
            FillCombo("CostCenter")
            Me.cmbProject.SelectedIndex = id

            id = Me.cmbPurchaseDemand.Value
            FillCombo("Demand")
            Me.cmbPurchaseDemand.Value = id


            If Me.cmbInwardExpense.ActiveRow Is Nothing Then
                Me.cmbInwardExpense.Rows(0).Activate()
            End If

            id = Me.cmbInwardExpense.ActiveRow.Cells(0).Value
            FillCombo("InwardExpense")
            Me.cmbInwardExpense.Value = id

            id = Me.cmbTermCondition.SelectedIndex
            FillCombo("TermsCondition")
            Me.cmbTermCondition.SelectedIndex = id

            id = Me.cmbPaymentTypes.SelectedIndex
            FillCombo("PaymentTerms")
            Me.cmbPaymentTypes.SelectedIndex = id
            ''start TFS4161
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            If Not getConfigValueByType("PurchaseDiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("PurchaseDiablePackQuantity").ToString)
            End If
            ''End TFS4161
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try

    End Sub
    Private Sub lnkVendorsItem_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
            Dim Str As String = "SELECT ArticleDefTable.ArticleId FROM ArticleDefTable INNER JOIN ArticleDefVendors ON ArticleDefTable.MasterID = ArticleDefVendors.ArticleDefId WHERE ArticleDefVendors.VendorId=" & Me.cmbVendor.Value & ""
            Dim dt As DataTable = GetDataTable(Str)
            If dt.Rows.Count < 1 Then Exit Sub
            For Each Row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.cmbItem.Rows
                If Row.Index > 0 Then
                    Me.cmbItem.Focus()
                    Row.Selected = True
                    Me.txtQty.Focus()
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If Row.Cells(0).Value = dt.Rows(i).Item("ArticleId") Then
                            AddItemToGrid()
                            Application.DoEvents()
                        End If
                    Next
                End If
            Next
            Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.CloseDropdown)
            Me.GetTotal()
            Me.ClearDetailControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub AllItems()
        Try
            If IsFormLoaded = True Then
                If Me.rbtAll.Checked = True Then
                    FillCombo("Item")
                ElseIf Me.rbtService.Checked = True Then
                    Dim Str As String = "SELECT ArticleId AS Id, ArticleCode AS Code, ArticleDescription AS Item, ArticleColorName AS Combination, ArticleUnitName AS UOM, ArticleSizeName AS Size, PackQty, PurchasePrice AS Price, ArticleCompanyName AS Catagory, ArticleLpoName AS Model, SortOrder FROM ArticleDefView WHERE (Active = 1) AND (ServiceItem = 1)"
                    FillUltraDropDown(Me.cmbItem, Str)
                    Me.cmbItem.Rows(0).Activate()
                Else
                    Dim Str As String = "SELECT ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleCode AS Code, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.PurchasePrice AS Price FROM  ArticleDefView INNER JOIN  ArticleDefVendors ON ArticleDefView.MasterID = ArticleDefVendors.ArticleDefId WHERE (ArticleDefView.Active = 1) AND (ArticleDefVendors.VendorId=" & Me.cmbVendor.Value & ")"
                    'FillDropDown(cmbItem, str)
                    FillUltraDropDown(Me.cmbItem, Str)
                    Me.cmbItem.Rows(0).Activate()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbVendor_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbVendor.KeyDown
        Try
            ''TFS1781 : Ayesha Rehman :Added for Selection of Customer or Vendor
            If e.KeyCode = Keys.F1 Then
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    'frmSearchCustomersVendors.rbtCustomers.Checked = True
                    'frmSearchCustomersVendors.rbtVendors.Checked = True
                    'frmSearchCustomersVendors.rbtCustomers.Visible = True
                    'frmSearchCustomersVendors.rbtVendors.Visible = True
                    frmAccountSearch.AccountType = "'Vendor','Customer' "
                Else
                    'frmSearchCustomersVendors.rbtCustomers.Checked = False
                    'frmSearchCustomersVendors.rbtVendors.Checked = True
                    'frmSearchCustomersVendors.rbtVendors.Visible = True
                    frmAccountSearch.AccountType = "'Vendor'"
                End If

                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbVendor.Value = frmAccountSearch.SelectedAccountId
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub cmbVendor_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.Leave
        Try
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.rbtVendor.Checked = True Then AllItems()
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            'FillCombo("Demand")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rbtAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAll.CheckedChanged, rbtVendor.CheckedChanged, rbtService.CheckedChanged
        Try
            AllItems()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillComboByEdit()
        Try
            If IsFormLoaded = True Then
                FillCombo("Vendor")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '' ReqId-899 Set Property Editable Column TaxAmount
    Private Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            '' ReqId-928 Added Index For Editable Comments Field
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If col.Index <> GrdEnum.LocationId AndAlso col.Index <> GrdEnum.Qty AndAlso col.Index <> GrdEnum.Warranty AndAlso col.Index <> GrdEnum.Status AndAlso col.Index <> GrdEnum.Rate AndAlso col.Index <> GrdEnum.TaxPercent AndAlso col.Index <> GrdEnum.Comments AndAlso col.Index <> GrdEnum.AdTax_Percent AndAlso col.Index <> GrdEnum.CurrentPrice AndAlso col.Index <> GrdEnum.RateDiscPercent AndAlso col.Index <> GrdEnum.SerialNo Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            '' End ReqId-928
            ''Start TFS4161
            If IsPackQtyDisabled = True Then
                Me.grd.RootTable.Columns(GrdEnum.TotalQty).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Else
                Me.grd.RootTable.Columns(GrdEnum.TotalQty).EditType = Janus.Windows.GridEX.EditType.TextBox
            End If
            'Rafay:Task Start:Edit UOM field in enum grd
            Me.grd.RootTable.Columns(GrdEnum.UOM).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grd.RootTable.Columns(GrdEnum.Status).EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grd.RootTable.Columns("Status").HasValueList = True
            'Rafay:Task
            ''End TFS4161
            'grd.AutoSizeColumns()
            Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("Unit").Index
            Me.grd.RootTable.Columns("Unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index

            Me.grd.RootTable.Columns("TotalAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("TotalAmount").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("TotalAmount").TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            'Me.grd.RootTable.Columns("TaxAmount").TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            'Me.grd.RootTable.Columns("TaxAmount").FormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns(GrdEnum.CurrentPrice).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.CurrentPrice).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(GrdEnum.CurrentPrice).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.CurrentPrice).TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.PackPrice).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.Rate).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.Rate).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(GrdEnum.Rate).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.Rate).TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.PackPrice).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).FormatString = "N" & 4
            Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).FormatString = "N" & 4
            Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).TotalFormatString = "N" & 4
            Me.grd.RootTable.Columns(GrdEnum.Total).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.Total).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(GrdEnum.Total).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.Total).TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).FormatString = "N" ''TFS3496
            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).TotalFormatString = "N" & TotalAmountRounding ''TFS3496

            '' TASK TFS done on 19-11-2018
            Me.grd.RootTable.Columns("CurrencyAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("CurrencyAmount").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("CurrencyAmount").TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("CurrencyTotalAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("CurrencyTotalAmount").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("CurrencyTotalAmount").TotalFormatString = "N" & TotalAmountRounding
            '' END TASK TFS

            Dim bln As Boolean = Convert.ToBoolean(getConfigValueByType("GridFreezColumn").Replace("Error", "False").Replace("''", "False"))
            If bln = True Then
                Me.grd.FrozenColumns = Me.grd.RootTable.Columns("Size").Index
            Else
                Me.grd.FrozenColumns = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            Me.GetGridDetailQtyCalculate(e)
            Me.GetGridDetailTotal()
            GetTotal()
            If e.Column.Index = GrdEnum.Rate Then
                Me.grd.GetRow.Cells("CurrentPrice").Value = Val(Me.grd.GetRow.Cells("Price").Value.ToString)
                Me.grd.GetRow.Cells("RateDiscPercent").Value = 0
            End If
            GetDiscountedPrice()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.grd.GetRow.Delete()
                grd.UpdateData()
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
            Else
                If Me.cmbItem.Value Is Nothing Then Exit Sub
                Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
                Me.txtCurrentRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
                FillCombo("ArticlePack")
                Me.txtLastPrice.Text = Math.Round(LastPrice(Me.cmbVendor.Value, Me.cmbItem.Value), TotalAmountRounding) 'Task:2445 Call Last Price Function  and implement
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCode.CheckedChanged
        Try
            If Not IsFormLoaded = True Then Exit Sub
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
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Purchase Order"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                '  Return GetSerialNo("PO" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "PurchaseOrderMasterTable", "PurchaseOrderNo")
                'rafay:task start
                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    Return GetSerialNo("PO" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "PurchaseOrderMasterTable", "PurchaseOrderNo")
                Else
                    companyinitials = "PK"
                    Return GetNextDocNo("PO" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "PurchaseOrderMasterTable", "PurchaseOrderNo")
                End If
                ' Return GetNextDocNo(PreFix & CompanyPrefix & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "ReceivingMasterTable", "ReceivingNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                'Return GetNextDocNo("PO" & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "PurchaseOrderMasterTable", "PurchaseOrderNo")
                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    Return GetSerialNo("PO" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "PurchaseOrderMasterTable", "PurchaseOrderNo")
                Else
                    companyinitials = "PK"
                    Return GetNextDocNo("PO" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "PurchaseOrderMasterTable", "PurchaseOrderNo")
                End If
                'Rafay :Task End
            Else
                Return GetNextDocNo("PO", 6, "PurchaseOrderMasterTable", "PurchaseOrderNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnAddNewitem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewitem.Click
        Call frmAddItem.ShowDialog()
        Call FillCombo("Item")
    End Sub
    Public Sub cmbVendor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.ValueChanged
        Try
            If Not IsFormLoaded = True Then Exit Sub
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            FillCombo("LC")
            FillCombo("CMFADoc")
            'FillCombo("Demand")
            CtrlGrdBar1.Email = New SBModel.SendingEmail
            CtrlGrdBar1.Email.ToEmail = Me.cmbVendor.ActiveRow.Cells("Email").Text
            CtrlGrdBar1.Email.Subject = "Purchase Order: " + "(" & Me.txtPONo.Text & ")"
            CtrlGrdBar1.Email.DocumentNo = Me.txtPONo.Text
            CtrlGrdBar1.Email.DocumentDate = Me.dtpPODate.Value


            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            Me.txtLastPrice.Text = Math.Round(LastPrice(Me.cmbVendor.Value, Me.cmbItem.Value), TotalAmountRounding)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function EmailSave()
        EmailSave = Nothing
        Dim flg As Boolean = False
        If Me.cmbVendor.ActiveRow Is Nothing Then Exit Function

        If IsEmailAlert = True Then
            Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name='frmPurchase' AND EmailAlert=1")
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
                Email.Subject = "Purchase Order " & setVoucherNo & ""
                Email.Body = "Purchase Order " _
                & " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Previouse_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
                Email.Status = "Pending"
                Call New MailSentDAL().Add(Email)
            End If
        End If
        Return EmailSave

    End Function
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
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            DisplayRecord("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Get_All(ByVal PurchaseOrderNo As String)
        Try
            Get_All = Nothing
            If IsFormLoaded = True Then
                If PurchaseOrderNo.Length > 0 Then
                    Dim str As String = "Select * From PurchaseOrderMasterTable WHERE PurchaseOrderNo=N'" & PurchaseOrderNo & "'"
                    Dim dt As DataTable = GetDataTable(str)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then
                            IsEditMode = True
                            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
                            If Not getConfigValueByType("PurchaseOrderApproval") = "Error" Then
                                ApprovalProcessId = getConfigValueByType("PurchaseOrderApproval")
                            End If
                            If ApprovalProcessId = 0 Then
                                Me.btnApprovalHistory.Visible = False
                                Me.btnApprovalHistory.Enabled = False
                            Else
                                Me.btnApprovalHistory.Visible = True
                                Me.btnApprovalHistory.Enabled = True
                                Me.chkPost.Visible = False
                            End If
                            ''Ayesha Rehman :TFS2375 :End

                            Me.txtReceivingID.Text = dt.Rows(0).Item("PurchaseOrderId")
                            Me.txtPONo.Text = dt.Rows(0).Item("PurchaseOrderNo")
                            Me.dtpPODate.Value = dt.Rows(0).Item("PurchaseOrderDate")
                            'Me.cmbCompany.SelectedValue = dt.Rows(0).Item("LocationId")
                            Me.cmbVendor.Value = dt.Rows(0).Item("VendorId")
                            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks")
                            Me.txtPaid.Text = dt.Rows(0).Item("CashPaid")

                            'If IsDBNull(dt.Rows(0).Item("CurrencyType")) Then
                            '    Me.cmbCurrency.SelectedIndex = 0
                            'Else
                            '    cmbCurrency.SelectedValue = dt.Rows(0).Item("CurrencyType")
                            'End If

                            'If IsDBNull(dt.Rows(0).Item("CurrencyRate")) Then
                            '    Me.cmbCurrency.SelectedIndex = 0
                            'Else
                            '    cmbCurrency.SelectedValue = dt.Rows(0).Item("CurrencyRate")
                            'End Ifl

                            DisplayDetail(dt.Rows(0).Item("PurchaseOrderId"))
                            GetTotal()
                            Me.BtnSave.Text = "&Update"
                            Me.cmbPo.Enabled = False
                            GetSecurityRights()
                            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                            'Me.cmbCompany.Enabled = False
                            IsDrillDown = True
                            Me.cmbVendor.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)

                            If IsDBNull(dt.Rows(0).Item("Receiving_Date")) Then
                                Me.dtpReceivingDate.Value = Now
                                Me.dtpReceivingDate.Checked = False
                            Else
                                Me.dtpReceivingDate.Value = dt.Rows(0).Item("Receiving_Date")
                                Me.dtpReceivingDate.Checked = True
                            End If

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
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            LoadAllRecords = True
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
    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                LoadAllRecords = False
                DisplayRecord()
            ElseIf e.Tab.Index = 0 Then
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                If IsEditMode = False Then Me.BtnDelete.Visible = False
                If IsEditMode = False Then Me.BtnPrint.Visible = False
                Me.BtnEdit.Visible = False
            ElseIf e.Tab.Index = 2 Then
                Me.DisplayPurchaseOrderTemplate()
                ''''''''''''''''''''''''''
            End If

            Me.CtrlGrdBar2_Load(Nothing, Nothing)
            Me.CtrlGrdBar1_Load(Nothing, Nothing)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ExportFile(ByVal VoucherId As Integer)
        Try
            If IsEmailAlert = True Then
                If IsAttachmentFile = True Then
                    crpt = New ReportDocument
                    If IO.File.Exists(str_ApplicationStartUpPath & "\Reports\PurchaseOrder.rpt") = False Then Exit Sub
                    crpt.Load(str_ApplicationStartUpPath & "\Reports\PurchaseOrder.rpt", DBServerName)
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
                    crpt.RecordSelectionFormula = "{PurchaseOrderMasterTable.PurchaseOrderId}=" & VoucherId



                    Dim crExportOps As New ExportOptions
                    Dim crDiskOps As New DiskFileDestinationOptions
                    Dim crExportType As New PdfRtfWordFormatOptions


                    If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments\") Then
                        IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments\")
                    Else
                    End If
                    FileName = String.Empty
                    FileName = "Purchase Order" & "-" & setVoucherNo & ""
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
            'ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            Dim frm As New frmLetterCredit
            frm.Size = New Size(850, 700)
            frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.WindowState = FormWindowState.Normal
            frm.StartPosition = FormStartPosition.CenterParent
            frm.ShowDialog()
            Dim id As Integer = 0
            id = Me.cmbLC.Value
            FillCombo("LC")
            Me.cmbLC.Value = id
            Exit Sub
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
                If dateLock.DateLock.ToString.Length > 0 Then
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
    Private Sub btnLoadSalesOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadSalesOrder.Click
        Try
            Dim frm As New frmSalesOrderList
            If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                If Me.BtnSave.Text = "&Update" Then
                    RefreshControls()
                Else
                    DisplayDetail(-1)
                End If
                IsSOLoad = True
                DisplayDetail(frm.ReceivingID, "LoadSalesOrder")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetTermsCondition() As String
        Try

            Dim str As String = "Select Terms_And_Condition From PurchaseOrderMasterTable WHERE PurchaseOrderId in (Select Max(PurchaseOrderId) From PurchaseOrderMasterTable)"
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

    Private Sub txtPackRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPackRate.LostFocus
        Try
            If Val(Me.txtPackQty.Text) = 0 Then
                txtPackQty.Text = 1
                txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), TotalAmountRounding)
            Else
                txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text), TotalAmountRounding)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackRate.TextChanged
        Try
            If Val(Me.txtPackRate.Text) > 0 Then
                If getConfigValueByType("Apply40KgRate").ToString = "True" AndAlso Me.cmbUnit.Text <> "Loose" Then
                    Me.txtRate.Text = (Val(Me.txtPackRate.Text) / 40)
                ElseIf Me.cmbUnit.Text <> "Loose" Then
                    Me.txtRate.Text = (Val(Me.txtPackRate.Text) / Val(Me.txtPackQty.Text))
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged
        Try
            'If Me.txtPackRate.Text.Length > 0 AndAlso (Me.txtPackRate.Text) > 0 Then
            '    If Me.cmbUnit.Text <> "Loose" Then
            '        Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
            '    End If
            'End If
            If Me.txtPackRate.Text.Length > 0 AndAlso Val(Me.txtPackRate.Text) > 0 Then
                If Me.cmbUnit.Text <> "Loose" Then
                    Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))

                Else

                    Me.txtRate.Text = Val(Me.txtRate.Text)

                End If
            End If
            If Val(Me.txtPackQty.Text) > 0 Then
                Me.txtTotalQuantity.Text = Math.Round(Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text), TotalAmountRounding)
            Else
                Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintSelectedVouchersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSelectedVouchersToolStripMenuItem.Click
        ' Change on 23-11-2013  For Multiple Print Vouchers
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                ShowReport("PurchaseOrder", "{PurchaseOrderMasterTable.PurchaseOrderId}=" & r.Cells("PurchaseOrderId").Value, , , True)
                PrintLog = New SBModel.PrintLogBE
                PrintLog.DocumentNo = r.Cells("PurchaseOrderNo").Value.ToString
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
    Private Sub txtTotal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotal.Leave
        Try
            'Commented against task:2367
            'Dim dbl As Double = 0.0
            'If Val(Me.txtTotal.Text) > 0 And Val(Me.txtQty.Text) > 0 Then
            '    dbl = Me.txtTotal.Text / Me.txtQty.Text
            '    Me.txtRate.Text = Math.Round(dbl, 2).ToString()
            'Else
            '    dbl = Me.txtTotal.Text / Me.txtRate.Text
            '    Me.txtQty.Text = Math.Round(dbl, 2).ToString()
            'End If


            'Task:2367 Change Total
            ''/// 18-06-16
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
            ''/// 18-06-16
            'End Task:2367

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
    Private Sub txtTaxPercent_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTax.Leave, txtTax.Enter
        Try
            If Val(Me.txtTax.Text) > 0 Then
                taxamnt = Math.Round((Me.txtTotal.Text * Me.txtTax.Text) / 100, TotalAmountRounding)
                Me.txtNetTotal.Text = Math.Round(Me.txtTotal.Text + taxamnt, TotalAmountRounding)
            ElseIf Me.txtTax.Text = String.Empty Then
                taxamnt = 0D
                Me.txtNetTotal.Text = Math.Round(Val(Me.txtTotal.Text), TotalAmountRounding)
                Exit Sub
            Else
                Me.txtNetTotal.Text = Math.Round(Val(Me.txtTotal.Text), TotalAmountRounding)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    '''''''''''''''''''''''''''''''
    ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
    Private Sub txtTaxPercent_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTax.LostFocus
        Try
            'If Val(Me.txtTax.Text) > 0 Then
            '    Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + ((Val(Me.txtTotal.Text) * Val(Me.txtTax.Text)) / 100)
            'Else
            '    Me.txtNetTotal.Text = Val(Me.txtTotal.Text)
            'End If
            Me.GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_Error(sender As Object, e As Janus.Windows.GridEX.ErrorEventArgs) Handles grd.Error
        Try
            e.DisplayErrorMessage = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
            If e.KeyCode = Keys.F2 Then
                OpenToolStripButton_Click(Me.BtnEdit, Nothing)
                Exit Sub
            ElseIf e.KeyCode = Keys.F1 Then
                frmRateConversion.formname = Me.Name
                Rate = grd.CurrentRow.Cells("CurrentPrice").Value.ToString
                frmRateConversion.ShowDialog()
            End If
            ''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
            'If e.KeyCode = Keys.Delete Then
            '    DeleteToolStripButton_Click(BtnDelete, Nothing)
            '    Exit Sub
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdSaved.RowCount <= 0 Then Exit Sub
            DeleteToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
    ''15-Feb-2014 Task:2426 Imran Ali Payment Schedule On Sales Order And Purchase Order
    Private Sub btnPaymentSchedule1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentSchedule1.Click
        Try
            Me.btnPaymentSchedule_Click(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPaymentSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentSchedule.Click
        Try
            If Me.BtnSave.Text = "&Update" Or Me.BtnSave.Text = "Update" Or Me.UltraTabControl1.SelectedTab.Index = 1 Then
                ApplyStyleSheet(frmPaymentTermsSchedule)
                frmPaymentTermsSchedule.FormName = frmPaymentTermsSchedule.enmOrderType.PO
                frmPaymentTermsSchedule.OrderId = grdSaved.CurrentRow.Cells("PurchaseOrderId").Value 'Val(Me.txtReceivingID.Text)
                frmPaymentTermsSchedule.OrderNo = grdSaved.CurrentRow.Cells(0).Value.ToString 'Me.txtPONo.Text.ToString
                frmPaymentTermsSchedule.ShowDialog()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2426
    ''28-Feb-2014  TASK:24445 Imran Ali   Last purchase and sale price show on sale order and purchase order
    Public Function LastPrice(ByVal AccountId As Integer, ByVal ItemId As Integer) As Double
        Try
            Dim strSQL As String = String.Empty
            ''Ahmad Sharif: Update Query for Last Price of Same vendor for same item, 06-06-2015
            strSQL = "Select Isnull(Max(Price),0) as LastPrice From PurchaseOrderDetailTable WHERE ArticleDefId=" & ItemId & " AND PurchaseOrderId In (Select IsNull(Max(PurchaseOrderId),0) as PurchaseOrderId From PurchaseOrderMasterTable WHERE VendorId=" & AccountId & ")"
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
    ''18-June-2014 TASK:2695 Imran Ali CMFA Load On Purchase Order
    'End Task:2445
    'Task:26733 Load CMFA Document From User
    'Private Sub cmbCMFADoc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCMFADoc.SelectedIndexChanged
    '    'Try
    '    '    If IsFormLoaded = True Then
    '    '        If Not Me.cmbCMFADoc.SelectedIndex = -1 Then
    '    '            DisplayDetail(Me.cmbCMFADoc.SelectedValue, "LoadCMFADocument")
    '    '        End If
    '    '    Else
    '    '        Exit Sub
    '    '    End If
    '    'Catch ex As Exception
    '    '    ShowErrorMessage(ex.Message)
    '    'End Try
    'End Sub
    'End Task:2673

    'Task:2673 Added Function For PO Qty Exceeded From CMFA Qty 
    Public Sub ChkCMFAQty(ByVal ArticleDefId As Integer, ByVal DocId As Int32, ByVal Qty As Double, Optional ByVal trans As OleDbTransaction = Nothing)
        Try

            Dim objDt As New DataTable
            If Me.BtnSave.Text = "&Save" Then
                objDt = GetDataTable("Select CMFADetailTable.ArticleDefId, SUM(IsNull(Sz1,0)) as Qty, SUM(IsNull(POQty,0)) as PoQty From CMFADetailTable WHERE DocId=" & DocId & " Group By CMFADetailTable.ArticleDefId")
            Else
                objDt = GetDataTable("Select CMFADetailTable.ArticleDefId, SUM(IsNull(Sz1,0)) as Qty, SUM(IsNull(POQty,0)) as PoQty From CMFADetailTable WHERE CMFADetailTable.ArticleDefId=" & ArticleDefId & " AND DocId=" & DocId & " Group By CMFADetailTable.ArticleDefId") ')
            End If
            If objDt IsNot Nothing Then
                If objDt.Rows.Count > 0 Then

                    If Val(objDt.Rows(0).Item(1).ToString) < (Val(objDt.Rows(0).Item(2).ToString) + Qty) Then
                        Throw New Exception(" " & Me.grd.GetRow.Cells(GrdEnum.Item).Value.ToString & " PO Qty Exceeded From CMFA Qty.")
                    End If

                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'End Task:2673


    ''18-June-2014 TASK:2695 Imran Ali CMFA Load On Purchase Order
    Private Sub btnCMFA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCMFA.Click
        Try
            ApplyStyleSheet(frmCMFAPurchaseOrder)
            frmCMFAPurchaseOrder.ShowDialog()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2695

    Private Sub cmbCMFADoc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCMFADoc.SelectedIndexChanged
        Try

            If IsFormLoaded = True Then
                If Me.cmbCMFADoc.SelectedIndex > 0 Then
                    Me.txtCMFAAmount.Text = Val(CType(Me.cmbCMFADoc.SelectedItem, DataRowView).Row.Item("CMFA_Amount").ToString)
                    Me.txtPOAmountAgainstCMFA.Text = Val(CType(Me.cmbCMFADoc.SelectedItem, DataRowView).Row.Item("POAmount").ToString)
                    Me.txtCMFADiff.Text = Val(Me.txtCMFAAmount.Text) - Val(Me.txtPOAmountAgainstCMFA.Text)
                Else
                    Me.txtCMFAAmount.Text = String.Empty
                    Me.txtPOAmountAgainstCMFA.Text = String.Empty
                    Me.txtCMFADiff.Text = String.Empty
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
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
            str.Add("@SIRIUS")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSKey() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("Purchase Order")
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
            If GetSMSConfig("SMS To Location On Purchase Order").Enable = True Then
                Dim strSMSBody As String = String.Empty
                Dim objData As DataTable = CType(Me.grd.DataSource, DataTable)
                Dim dt_Loc As DataTable = objData.DefaultView.ToTable("Default", True, "LocationId")
                Dim drData() As DataRow
                For j As Integer = 0 To dt_Loc.Rows.Count - 1
                    strSMSBody = String.Empty
                    strSMSBody += "Purchase Order, Doc No: " & Me.txtPONo.Text & ", Doc Date: " & Me.dtpPODate.Value.ToShortDateString & ", Supplier: " & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString & ", Invoice No: " & Me.txtPurchaseNo.Text & ", Remarks:" & Me.txtRemarks.Text & ", "
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
            If GetSMSConfig("Purchase Order").Enable = True Then
                If msg_Confirm(str_ConfirmSendSMSMessage) = False Then Exit Try
                Dim strDetailMessage As String = String.Empty
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    If strDetailMessage.Length = 0 Then
                        strDetailMessage = r.Cells(GrdEnum.Item).Value.ToString & ", Qty: " & IIf(r.Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(r.Cells(GrdEnum.Qty).Value.ToString), Val(r.Cells(GrdEnum.Qty).Value.ToString) * Val(r.Cells(GrdEnum.PackQty).Value.ToString))
                    Else
                        strDetailMessage += "," & r.Cells(GrdEnum.Item).Value.ToString & ", Qty: " & IIf(r.Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(r.Cells(GrdEnum.Qty).Value.ToString), Val(r.Cells(GrdEnum.Qty).Value.ToString) * Val(r.Cells(GrdEnum.PackQty).Value.ToString))
                    End If
                Next
                Dim objTemp As New SMSTemplateParameter
                Dim obj As Object = GetSMSTemplate("Purchase Order")
                If obj IsNot Nothing Then
                    objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                    Dim strMessage As String = objTemp.SMSTemplate
                    strMessage = strMessage.Replace("@AccountTitle", Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString).Replace("@AccountCode", Me.cmbVendor.ActiveRow.Cells("Code").Value.ToString).Replace("@DocumentNo", Me.txtPONo.Text).Replace("@DocumentDate", Me.dtpPODate.Value.ToShortDateString).Replace("@OtherDoc", Me.txtDcNo.Text).Replace("@Remarks", Me.txtRemarks.Text).Replace("@Amount", grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@Quantity", Me.grd.GetTotal(grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@DCNo", Me.txtDcNo.Text).Replace("@SONo", IIf(Me.cmbPo.SelectedIndex > 0, Me.cmbPo.Text, String.Empty)).Replace("@InvParty", Me.txtPurchaseNo.Text).Replace("@CompanyName", CompanyTitle).Replace("@SIRIUS", "Automated by www.SIRIUS.net").Replace("@DetailInformation", strDetailMessage)
                    SaveSMSLog(strMessage, Me.cmbVendor.ActiveRow.Cells("Mobile").Value.ToString, "Purchase Order")

                    If GetSMSConfig("Sale Invoice").EnabledAdmin = True Then
                        For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                            If strMob.Length > 10 Then
                                SaveSMSLog(strMessage, strMob, "Purchase Order")
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

    Private Sub btnPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayment.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            frmSOCashReceipt._CompanyId = Val(Me.grdSaved.GetRow.Cells("LocationId").Value.ToString)
            frmSOCashReceipt._CostCenterId = Val(Me.grdSaved.GetRow.Cells("CostCenterId").Value.ToString)
            frmSOCashReceipt._CustomerId = Val(Me.grdSaved.GetRow.Cells("VendorId").Value.ToString)
            frmSOCashReceipt._SaleOrderId = Val(Me.grdSaved.GetRow.Cells("PurchaseOrderId").Value.ToString)
            frmSOCashReceipt._SaleOrderNo = Me.grdSaved.GetRow.Cells("PurchaseOrderNo").Value.ToString
            frmSOCashReceipt._SaleOrderDate = Me.grdSaved.GetRow.Cells("Date").Value
            frmSOCashReceipt._CustomerName = Me.grdSaved.GetRow.Cells("VendorName").Value.ToString
            frmSOCashReceipt._NetAmount = Val(Me.grdSaved.GetRow.Cells("PurchaseOrderAmount").Value.ToString)
            frmSOCashReceipt.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub cmbPurchaseDemand_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPurchaseDemand.SelectedIndexChanged
    '    Try
    '        If Me.cmbPurchaseDemand.SelectedIndex <= 0 Then Exit Sub
    '        DisplayPODetail(Me.cmbPurchaseDemand.SelectedValue)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Me.cmbPurchaseDemand.SelectedRow.Index <= 0 Then Exit Sub
            'If Val(CType(Me.cmbPurchaseDemand.SelectedRow., DataRowView).Row.Item("VendorId").ToString) > 0 Then
            If Val(Me.cmbPurchaseDemand.SelectedRow.Cells("VendorId").Value.ToString) > 0 Then
                Me.cmbVendor.Value = Val(Me.cmbPurchaseDemand.SelectedRow.Cells("VendorId").Value.ToString)             'Val(CType(Me.cmbPurchaseDemand.Value, DataRowView).Row.Item("VendorId").ToString)
                Me.txtRemarks.Text = Me.cmbPurchaseDemand.SelectedRow.Cells("Remarks").Value.ToString      'CType(Me.cmbPurchaseDemand.Value, DataRowView).Row.Item("Remarks").ToString
                Me.dtpPODate.Value = Me.cmbPurchaseDemand.SelectedRow.Cells("PurchaseDemandDate").Value.ToString ' CType(Me.cmbPurchaseDemand.Value, DataRowView).Row.Item("PurchaseDemandDate").ToString
                Me.cmbProject.SelectedValue = Val(Me.cmbPurchaseDemand.SelectedRow.Cells("CostCenterId").Value.ToString)
            End If
            DisplayPODetail(Me.cmbPurchaseDemand.Value)
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
    'Altered Against Task#2015060006 to save attachement
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
                frm._VoucherId = Val(Me.grdSaved.GetRow.Cells("PurchaseOrderId").Value.ToString)
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Task#1-16062015 add new status event for adding new status
    Private Sub btnAddStockDispatchStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddStockDispatchStatus.Click
        Try
            Dim frm As New frmImportDispatchStatus
            frm.Size = New Size(370, 230)
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.WindowState = FormWindowState.Normal
            frm.StartPosition = FormStartPosition.CenterParent
            frm.ShowDialog()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#1-16062015 add event for ADD LC
    Private Sub AddLCToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddLCToolStripMenuItem.Click, AddLCToolStripMenuItem1.Click
        Try
            Dim frm As New frmLetterCredit
            frm.Size = New Size(750, 530)
            frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.WindowState = FormWindowState.Normal
            frm.StartPosition = FormStartPosition.CenterParent
            frm.ShowDialog()
            Dim id As Integer = 0
            id = Me.cmbLC.Value
            FillCombo("LC")
            Me.cmbLC.Value = id
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'add event for PO Invoice performa
    Private Sub POInvoicePerformaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POInvoicePerformaToolStripMenuItem.Click, PerformaInvoiceToolStripMenuItem.Click

        Try
            Dim frm As New frmPOInvoicePerforma
            'frm.Tag = Me.txtPONo.Text.ToString
            frm.Tag = Me.grdSaved.CurrentRow.Cells("PurchaseOrderNo").Value.ToString
            frm.Size = New Size(700, 500)
            frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.WindowState = FormWindowState.Normal
            frm.StartPosition = FormStartPosition.CenterParent
            frm.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#1-16062015

    Private Sub btnAddExp_Click(sender As Object, e As EventArgs) Handles btnAddExp.Click
        Try

            If Me.cmbInwardExpense.IsItemInList = False Then Exit Sub
            If Me.cmbInwardExpense.ActiveRow Is Nothing Then Exit Sub
            If Con Is Nothing Then Exit Sub
            If Me.grdInwardExpDetail.RowCount > 0 Then
                Dim bln As Boolean = Me.grdInwardExpDetail.Find(Me.grdInwardExpDetail.RootTable.Columns("AccountId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbInwardExpense.Value, 0, 1)
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
            cmd.CommandText = "INSERT INTO tblDefInwardAccounts(InwardAccountId,Active) VALUES(" & Me.cmbInwardExpense.Value & ",1) Select @@Identity"

            cmd.ExecuteNonQuery()
            objtrans.Commit()

            objcon.Close()
            objtrans.Dispose()
            objcon.Dispose()

            Dim dt As DataTable = CType(Me.grdInwardExpDetail.DataSource, DataTable)
            dt.AcceptChanges()
            Dim dr As DataRow
            dr = dt.NewRow
            If Me.BtnSave.Text <> "&Save" Then
                'Ali Faisal : TFS1300 : Column name id filled wrong
                dr(0) = Val(Me.grdSaved.GetRow.Cells("PurchaseOrderId").Value.ToString)
                'Ali Faisal : TFS1300 : End
            Else
                dr(0) = 0
            End If
            dr(1) = Me.cmbInwardExpense.Value
            dr(2) = Me.cmbInwardExpense.ActiveRow.Cells("Account Title").Value.ToString
            dr(3) = 0
            dt.Rows.Add(dr)
            dt.AcceptChanges()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdInwardExpDetail_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdInwardExpDetail.ColumnButtonClick
        Try

            If Not IsValidToDeleteAccount() Then
                ShowErrorMessage("Dependent Record Exist Against This Account ,Can't be deleted")
                Exit Sub
            End If
            Dim objcon As New OleDbConnection(Con.ConnectionString)
            Dim cmd As New OleDbCommand

            If objcon.State = ConnectionState.Closed Then objcon.Open()
            Dim objtrans As OleDbTransaction = objcon.BeginTransaction
            cmd.Connection = objcon
            cmd.Transaction = objtrans
            cmd.CommandType = CommandType.Text

            cmd.CommandText = String.Empty
            cmd.CommandText = "Delete From tblDefInwardAccounts  WHERE InwardAccountId=" & Val(grdInwardExpDetail.GetRow.Cells("AccountId").Value.ToString) & ""

            cmd.ExecuteNonQuery()
            objtrans.Commit()

            objcon.Close()
            objtrans.Dispose()
            objcon.Dispose()

            Me.grdInwardExpDetail.GetRow.Delete()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Thsi Function is made to insure that ,dependent accounts cannot be deleted
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Ayesha Rehman : 02-05-2018 : TFS3142</remarks>
    Private Function IsValidToDeleteAccount() As Boolean
        Try
            Dim str As String = "Select AccountId from InwardExpenseDetailTable where DocType !='PO' "
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    If Val(grdInwardExpDetail.GetRow.Cells("AccountId").Value.ToString) = Val(r.Item("AccountId").ToString) Then
                        Return False
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function


    Private Sub grd_RecordsDeleted(sender As Object, e As EventArgs) Handles grd.RecordsDeleted
        Try
            GetDiscountedPrice()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtDiscPercent_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDisc.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscPercent_LostFocus(sender As Object, e As EventArgs) Handles txtDisc.LostFocus
        Dim disc As Double = 0D
        Double.TryParse(Me.txtDisc.Text.Trim, disc)
        Dim price As Double = 0D
        'Double.TryParse(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString, price)
        Double.TryParse(Me.txtCurrentRate.Text, price)
        If Val(disc) <> 0 AndAlso Val(price) <> 0 Then
            If disc > 0 Then
                Me.txtRate.Text = price - ((price / 100) * disc)
            End If
        Else
            Me.txtRate.Text = txtRate.Text
        End If
        Me.GetDetailTotal()
    End Sub

    Private Sub txtCurrentRate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCurrentRate.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                frmRateConversion.formname = Me.Name
                frmRateConversion.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCurrentRate_LostFocus(sender As Object, e As EventArgs) Handles txtCurrentRate.LostFocus
        Try
            Me.GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub txtCurrentRate_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentRate.TextChanged
        Try
            If Val(Me.txtCurrentRate.Text) > 0 Then
                If Val(Me.txtDisc.Text) > 0 Then
                    Me.txtRate.Text = Math.Round(Val(Me.txtCurrentRate.Text) - ((Val(Me.txtDisc.Text) / 100) * Val(Me.txtCurrentRate.Text)), TotalAmountRounding)
                Else
                    Me.txtRate.Text = Val(Me.txtCurrentRate.Text)
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintPOWithArticleImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintPOWithArticleImageToolStripMenuItem.Click
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Dim dt As New DataTable
        Dim sql As String = String.Empty
        Try

            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Dim POIs As String = String.Empty

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                'If POIs = String.Empty Then
                '    POIs = r.Cells("PurchaseOrderId").Value.ToString
                'Else
                '    POIs += "," & r.Cells("PurchaseOrderId").Value.ToString
                'End If
                sql = "SP_RptPurchaseOrder " & Val(r.Cells("PurchaseOrderId").Value.ToString) & ""
                dt = GetDataTable(sql)
                dt.AcceptChanges()
                For Each dr As DataRow In dt.Rows
                    dr.BeginEdit()
                    If IO.File.Exists(dr.Item("ArticlePicture")) Then
                        LoadPicture(dr, "ArticleImage", dr.Item("ArticlePicture"))
                    End If
                    dr.EndEdit()
                Next

                PrintLog = New SBModel.PrintLogBE
                PrintLog.DocumentNo = r.Cells("PurchaseOrderNo").Value.ToString
                PrintLog.UserName = LoginUserName
                PrintLog.PrintDateTime = Date.Now
                Call SBDal.PrintLogDAL.PrintLog(PrintLog)
                ShowReport("PurchaseOrderWithArticleImage", , , , True, , , dt)
            Next
            'sql = "Select POM.PurchaseOrderId, POM.PurchaseOrderNo, POM.PurchaseOrderDate, POM.Remarks, " _
            ' & " POM.UserName, ADW.ArticleDescription, ADW.ArticleUnitName, ADW.ArticlePicture, Convert(image, '') As ArticleImage, PurchaseDemandMasterTable.PurchaseDemandNo, " _
            ' & " POD.Sz1, POD.Sz7,POD.Qty, POD.CurrentPrice, POD.Price, POD.TaxPercent, vwCOADetail.detail_title, vwCOADetail.Contact_Phone, vwCOADetail.Contact_Address  " _
            ' & " From PurchaseOrderMasterTable As POM Inner Join PurchaseOrderDetailTable As POD On POM.PurchaseOrderId = POD.PurchaseOrderId Left Outer Join" _
            ' & " ArticleDefView As ADW ON POD.ArticleDefId = ADW.ArticleId Inner Join vwCOADetail ON POM.VendorId = vwCOADetail.AccountId Left Outer Join PurchaseDemandMasterTable" _
            ' & " ON PurchaseDemandMasterTable.PurchaseDemandId = POM.PurchaseDemandId Where POM.PurchaseOrderId In(" & POIs.ToString & ") "
            'sql = "SP_RptPurchaseOrder " & Val(r.Cells("PurchaseOrderNo").Value.ToString) & ""
            'dt = GetDataTable(sql)
            'dt.AcceptChanges()
            'For Each dr As DataRow In dt.Rows
            '    dr.BeginEdit()
            '    If IO.File.Exists(dr.Item("ArticlePicture")) Then
            '        LoadPicture(dr, "ArticleImage", dr.Item("ArticlePicture"))
            '    End If
            '    dr.EndEdit()
            'Next
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = r.Cells("PurchaseOrderNo").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'Next
            'ShowReport("PurchaseOrderWithArticleImage", , , , , , , dt, )
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = r.Cells("PurchaseOrderNo").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub btnSearchPrint_ButtonClick(sender As Object, e As EventArgs) Handles btnSearchPrint.ButtonClick
        Try
            PrintToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintPOWithImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintPOWithImageToolStripMenuItem.Click
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Dim dt As New DataTable
        Dim sql As String = String.Empty
        Try

            If Me.grdSaved.RowCount = 0 Then Exit Sub

            sql = "SP_RptPurchaseOrder " & Val(Me.grdSaved.GetRow.Cells("PurchaseOrderId").Value.ToString) & ""
            dt = GetDataTable(sql)
            dt.AcceptChanges()
            For Each dr As DataRow In dt.Rows
                dr.BeginEdit()
                If IO.File.Exists(dr.Item("ArticlePicture")) Then
                    LoadPicture(dr, "ArticleImage", dr.Item("ArticlePicture"))
                End If
                dr.EndEdit()
            Next

            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = Me.grdSaved.GetRow.Cells("PurchaseOrderNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            ShowReport("PurchaseOrderWithArticleImage", , , , False, , , dt)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbPurchaseDemand_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If Me.cmbPurchaseDemand.SelectedRow.Index > 0 Then
                'Dim DT As New DataTable = Me.cmbPurchaseDemand.DataSource

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Function PDCostCentre(ByVal PD As String)
    '    Try

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Private Sub cmbPurchaseDemand_ValueChanged(sender As Object, e As EventArgs) Handles cmbPurchaseDemand.ValueChanged
        Try
            Me.cmbProject.SelectedValue = Val(Me.cmbPurchaseDemand.SelectedRow.Cells("CostCenterId").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintPOWithArticleImageToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintPOWithArticleImageToolStripMenuItem1.Click
        Try
            PrintPOWithImageToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
                Me.txtTotal.Text = Math.Round((Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text)), TotalAmountRounding)
            End If
            If Val(Me.txtTotalQuantity.Text) <> 0 AndAlso Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtRate.Text) = 0 Then
                Me.txtRate.Text = Math.Round(Val(Me.txtTotal.Text) / Val(Me.txtTotalQuantity.Text), TotalAmountRounding)
            End If
            If Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtTotalQuantity.Text) = 0 Then
                If Not Me.cmbUnit.Text <> "Loose" Then
                    Me.txtQty.Text = Math.Round(Val(Me.txtTotal.Text) / Val(Me.txtRate.Text), TotalAmountRounding)
                    Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
                Else
                    If Val(Me.txtPackQty.Text) > 0 Then
                        Me.txtQty.Text = Math.Round((Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)) / Val(Me.txtPackQty.Text), TotalAmountRounding)
                        Me.txtTotalQuantity.Text = Math.Round((Val(Me.txtQty.Text) * Val(Me.txtPackQty.Text)), TotalAmountRounding)
                    Else
                        Me.txtQty.Text = Math.Round(Val(Me.txtTotal.Text) / Val(Me.txtRate.Text), TotalAmountRounding)
                        Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
                    End If
                End If
            Else
                Me.txtTotal.Text = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text), TotalAmountRounding)    'Task#26082015
            End If

            Dim dblTaxPercent As Double = 0D
            Double.TryParse(Me.txtTax.Text, dblTaxPercent)
            Me.txtNetTotal.Text = Math.Round((((dblTaxPercent / 100) * Val(Me.txtTotal.Text)) + Val(Me.txtTotal.Text)), TotalAmountRounding)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTotalQuantity_LostFocus(sender As Object, e As EventArgs) Handles txtTotalQuantity.LostFocus
        Me.GetDetailTotal()
    End Sub

    Private Sub txtTotalQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtTotalQuantity.TextChanged
        Try
            If Not Val(Me.txtPackQty.Text) > 0 Then
                Me.txtQty.Text = Val(Me.txtTotalQuantity.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate_TextChanged(sender As Object, e As EventArgs) Handles txtRate.TextChanged

    End Sub

    Private Sub txtDisc_TextChanged(sender As Object, e As EventArgs) Handles txtDisc.TextChanged
        Try
            If Val(Me.txtDisc.Text) > 0 Then
                If Val(Me.txtCurrentRate.Text) > 0 Then
                    Me.txtRate.Text = Math.Round(Val(Me.txtCurrentRate.Text) - (Val(txtDisc.Text) / 100) * Val(Me.txtCurrentRate.Text), TotalAmountRounding)
                Else
                    Me.txtRate.Text = Val(Me.txtCurrentRate.Text)
                End If
            Else
                Me.txtRate.Text = Val(Me.txtCurrentRate.Text)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTotal_LostFocus(sender As Object, e As EventArgs) Handles txtTotal.LostFocus
        If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
        If Not Me.cmbItem.ActiveRow.Cells(0).Value > 0 Or Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
        GetDetailTotal()
    End Sub

    Private Sub txtTotal_TextChanged(sender As Object, e As EventArgs) Handles txtTotal.TextChanged

    End Sub

    Private Sub txtTax_TextChanged(sender As Object, e As EventArgs) Handles txtTax.TextChanged

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
                'If getConfigValueByType("Apply40KgRate").ToString = "False" Then
                If Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(GrdEnum.Rate).Value = (Val(Me.grd.GetRow.Cells(GrdEnum.PackPrice).Value.ToString) / Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString))
                End If
                'Else
                '    If Val(Me.grd.GetRow.Cells(GrdEnum.PackPrice).Value.ToString) > 0 Then
                '        Me.grd.GetRow.Cells(GrdEnum.Rate).Value = (Val(Me.grd.GetRow.Cells(GrdEnum.PackPrice).Value.ToString) / 40)
                '    End If
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetGridDetailTotal()
        Try
            Me.grd.UpdateData()
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

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''Written down this funtion by Ameen to update Purchase demand status against purchase order on 31-08-2016
    Private Sub UpdateStatus()
        Dim Str As String = String.Empty
        Dim dt As New DataTable
        Dim Close As Boolean = False
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        objCon = Con
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Try
            Str = "Select PurchaseDemandId, IsNull(Sz1, 0) - IsNull(DeliveredQty, 0) As RemainingQty From PurchaseDemandDetailTable Where PurchaseDemandId In(Select Distinct DemandID FROM PurchaseOrderDetailTable Where PurchaseOrderId = ident_current('PurchaseOrderMasterTable'))"
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            For Each dr As DataRow In dt.Rows
                If dr(1) > 0 Then
                    Close = False
                    Exit For
                Else
                    Close = True
                End If
            Next
            If Close = True Then
                objCommand.Connection = objCon
                Dim trans As OleDbTransaction = objCon.BeginTransaction
                Try
                    objCommand.Transaction = trans
                    objCommand.CommandType = CommandType.Text
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update PurchaseDemandMasterTable Set PurchaseDemandMasterTable.Status='Close' Where PurchaseDemandId =" & dt.Rows.Item(0).Item(0) & ""
                    objCommand.ExecuteNonQuery()
                    trans.Commit()
                Catch ex As Exception
                    trans.Rollback()
                    Throw ex
                End Try
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub UpdateStatusInEdit(ByVal PurchaseOrderId As Integer)
        Dim Str As String = String.Empty
        Dim dt As New DataTable
        Dim Close As Boolean = False
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        objCon = Con
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Try
            Str = "Select PurchaseDemandId, IsNull(Sz1, 0) - IsNull(DeliveredQty, 0) As RemainingQty From PurchaseDemandDetailTable Where PurchaseDemandId In(Select Distinct DemandID FROM PurchaseOrderDetailTable Where PurchaseOrderId = " & PurchaseOrderId & ")"
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            For Each dr As DataRow In dt.Rows
                If dr(1) > 0 Then
                    Close = False
                    Exit For
                Else
                    Close = True
                End If
            Next
            If Close = True Then
                objCommand.Connection = objCon
                Dim trans As OleDbTransaction = objCon.BeginTransaction
                Try
                    objCommand.Transaction = trans
                    objCommand.CommandType = CommandType.Text
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update PurchaseDemandMasterTable Set PurchaseDemandMasterTable.Status='Close' Where PurchaseDemandId =" & dt.Rows.Item(0).Item(0) & ""
                    objCommand.ExecuteNonQuery()
                    trans.Commit()
                Catch ex As Exception
                    trans.Rollback()
                    Throw ex
                End Try
            End If
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
                If Me.cmbCurrency.SelectedValue = BaseCurrencyId Then
                    Me.txtCurrencyRate.Enabled = False
                Else
                    Me.txtCurrencyRate.Enabled = True
                End If
                ''END TASK TFS1474
                If Val(Me.txtCurrencyRate.Text) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If
                ''
                If CurrencyRate > 1 Then
                    Me.txtCurrencyRate.Text = CurrencyRate
                End If
                ''
                Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Caption = "Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns("TotalAmount").Caption = "Total Amount (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns("Total").Caption = "Total (" & Me.BaseCurrencyName & ")"

                Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Caption = "Base Currency Rate (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Caption = "Tax Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyTotalAmount).Caption = "Total Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyAdTaxAmount).Caption = "AdTax Amount (" & Me.cmbCurrency.Text & ")"

                grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = False
                    'Me.grd.RootTable.Columns(grdEnm.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTotalAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAdTaxAmount).Visible = False

                    ''TASK TFS3493 Addition of application of base currency values to grid upon currency change.
                    If IsSOLoad = False Then
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
                    '' End TASK TFS3493
                Else
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = True
                    'Me.grd.RootTable.Columns(grdEnm.BaseCurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTotalAmount).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAdTaxAmount).Visible = True
                    ''Commented below line against TASK TFS3493
                    'If IsEditMode = False AndAlso IsSOLoad = False Then
                    ''TASK TFS3493 removed IsEditMode condition from below if statement
                    If IsSOLoad = False Then
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

    Private Sub btnEmailPurchaseOrder_Click(sender As Object, e As EventArgs) Handles btnEmailPurchaseOrder.Click
        Try
            GetTemplate(lblHeader.Text)
            If EmailTemplate.Length > 0 Then
                GetEmailData()
                GetVendorsEmails()
                FormatStringBuilder(dtEmail)
                'CreateOutLookMail()
            Else
                msg_Error("No email template is found for Purchase Order.")
            End If
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

            'For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetCheckedRows
            'Dim dt As DataTable = 
            'Dim PurchaseInquiryDetailId As Integer = 0
            'If Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString) > 0 Then
            '    PurchaseInquiryDetailId = Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString)
            'Else
            '    PurchaseInquiryDetailId = Val(Row.Cells("HeadArticleId").Value.ToString)
            'End If
            Dim dt As DataTable = ShowEmail(Val(txtReceivingID.Text))

            For Each Row1 As DataRow In dt.Rows
                Dr = dtEmail.NewRow
                For Each col As String In AllFields
                    If Row1.Table.Columns.Contains(col) Then
                        Dr.Item(col) = Row1.Item(col).ToString
                    End If
                Next
                dtEmail.Rows.Add(Dr)
            Next
            'Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetVendorsEmails()
        Try
            'Me.grdSaved.UpdateData()
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            If VendorEmails.Length > 0 Then
                VendorEmails += "; " & Me.cmbVendor.ActiveRow.Cells("Email").Text & ""
            Else
                VendorEmails = Me.cmbVendor.ActiveRow.Cells("Email").Text
            End If
            'Next
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
    Private Sub CreateOutLookMail(ByVal Email As String)
        Try
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = "Creating New PO: " + PurchaseOrderNo
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
    Private Function ShowEmail(ByVal ReceivingID As Integer) As DataTable
        Dim str As String = String.Empty
        Try
            str = "SELECT Recv_D.SerialNo, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.CurrentPrice,  Case When IsNull(Recv_D.CurrentPrice,0) > IsNull(Price,0) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent,Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As CurrencyTotalAmount, " _
          & " (IsNull(Recv_D.Qty, 0)*IsNull(Recv_D.Price, 0)* Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, Isnull(Recv_D.TaxPercent,0) as TaxPercent, 0 as TaxAmount, Convert(float, 0) As CurrencyTaxAmount, IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent, IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount,  Convert(float, 0) As CurrencyAdTaxAmount, Convert(float,0) as TotalAmount, " _
          & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, isnull(Recv_D.PackPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, IsNull(Recv_D.DemandId,0) as PurchaseDemandId, Recv_D.PurchaseDemandDetailId, IsNull(Recv_D.PurchaseOrderDetailId,0) as PurchaseOrderDetailId, IsNull(Recv_D.Qty, 0) As TotalQty  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
          & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId " _
          & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
            Dim dtDisplayDetail As New DataTable
            dtDisplayDetail = GetDataTable(str)
            dtDisplayDetail.Columns("Total").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)* CurrencyRate"
            dtDisplayDetail.Columns("CurrencyAmount").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)"
            dtDisplayDetail.Columns("TaxAmount").Expression = "((TaxPercent/100)*Total)"
            dtDisplayDetail.Columns("CurrencyTaxAmount").Expression = "(((CurrencyAmount) * IsNull(TaxPercent,0))/100)"
            'TASK-TFS-51 Set Exparession For Additonal Tax
            dtDisplayDetail.Columns("AdTax_Amount").Expression = "((IsNull(AdTax_Percent,0)/100)*Total)"
            dtDisplayDetail.Columns("CurrencyAdTaxAmount").Expression = "(((CurrencyAmount) * IsNull(AdTax_Percent,0))/100)"
            dtDisplayDetail.Columns("TotalAmount").Expression = "([Total]+([TaxAmount]+[AdTax_Amount]))" 'Task:2374 Show Total Amount
            dtDisplayDetail.Columns("CurrencyTotalAmount").Expression = "IsNull([CurrencyAmount],0) + (IsNull([CurrencyTaxAmount],0)+IsNull([CurrencyAdTaxAmount],0))"
            Return dtDisplayDetail
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetSingle(ByVal PurchaseOrderId As Integer) As DataTable
        Dim Str As String = ""
        Try
            Str = "SELECT Recv.PurchaseOrderNo, Recv.PurchaseOrderDate, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseOrderQty,  " & _
                                    " Recv.PurchaseOrderAmount, Recv.PurchaseOrderId, Recv.VendorId, Recv.Remarks, Recv.Terms_And_Condition, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date, Case When Isnull(Recv.Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post, IsNull(Recv.RefCMFADocId,0) as RefCMFADocId, (CMFAMasterTable.DocNo + '~' + Convert(Varchar,CMFAMasterTable.DocDate,102)) [CMFA Doc No], dbo.vwCOADetail.Contact_Email As Email, Recv.LocationId, IsNull(Recv.CostCenterId,0) as CostCenterId, IsNull(Recv.PurchaseDemandId,0) as PurchaseDemandId, PDemand.PurchaseDemandNo, Recv.POType, Recv.POStockDispatchStatus, Recv.UserName, Recv.UpdateUserName " & _
                                    " FROM dbo.PurchaseOrderMasterTable Recv INNER JOIN  " & _
                                    " dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN PurchaseDemandMasterTable PDemand on PDemand.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN CMFAMasterTable On CMFAMasterTable.DocId = Recv.RefCMFADocId LEFT OUTER JOIN(Select DISTINCT PurchaseOrderId From PurchaseOrderDetailTable) as Location On Location.PurchaseOrderId = Recv.PurchaseOrderId WHERE Recv.PurchaseOrderNo IS NOT NULL And Recv.PurchaseOrderId = " & PurchaseOrderId & ""
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

    Private Sub cmbTermCondition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTermCondition.SelectedIndexChanged
        Try
            If Not Me.cmbTermCondition.SelectedItem Is Nothing Then
                Me.txtTerms_And_Condition.Text = CType(Me.cmbTermCondition.SelectedItem, DataRowView).Row.Item("Term_Condition").ToString()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Ayesha Rehman : TFS2375 : Show Approval History of the current Document
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnApprovalHistory_Click(sender As Object, e As EventArgs) Handles btnApprovalHistory.Click
        Try
            frmApprovalHistory.DocumentNo = Me.txtPONo.Text
            frmApprovalHistory.Source = Me.Name
            frmApprovalHistory.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Purchase Order"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCurrencyRate_TextChanged(sender As Object, e As EventArgs) Handles txtCurrencyRate.TextChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                'Me.txtCurrencyRate.Text = dr.Row.Item("CurrencyRate").ToString
                ''TASK TFS1474
                If Me.cmbCurrency.SelectedValue = BaseCurrencyId Then
                    Me.txtCurrencyRate.Enabled = False
                Else
                    Me.txtCurrencyRate.Enabled = True
                End If
                ''END TASK TFS1474
                If Val(Me.txtCurrencyRate.Text) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If
                Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Caption = "Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns("TotalAmount").Caption = "Total Amount (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns("Total").Caption = "Total (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Caption = "Base Currency Rate (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Caption = "Tax Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyTotalAmount).Caption = "Total Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyAdTaxAmount).Caption = "AdTax Amount (" & Me.cmbCurrency.Text & ")"
                grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = False
                    'Me.grd.RootTable.Columns(grdEnm.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTotalAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAdTaxAmount).Visible = False
                    ''TASK TFS3493 Addition of application of base currency values to grid upon currency change.
                    If IsSOLoad = False Then
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
                    '' End TASK TFS3493
                Else
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = True
                    'Me.grd.RootTable.Columns(grdEnm.BaseCurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTotalAmount).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAdTaxAmount).Visible = True
                    ''Commented below line against TASK TFS3493
                    'If IsEditMode = False AndAlso IsSOLoad = False Then
                    ''TASK TFS3493 removed IsEditMode condition from below if statement
                    If IsSOLoad = False Then
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
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function SaveTemplate() As Boolean
        If Me.chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        Me.grd.UpdateData()
        Dim strCMFA As String = String.Empty
        Dim objDt As New DataTable
        If Me.cmbCMFADoc.SelectedIndex > 0 Then
            strCMFA = "Select CMFADetailTable.ArticleDefId, SUM(IsNull(Sz1,0)) as Qty, SUM(IsNull(POQty,0)) as PoQty From CMFADetailTable WHERE DocId=" & Me.cmbCMFADoc.SelectedValue & " Group By CMFADetailTable.ArticleDefId Having SUM(IsNull(Sz1,0)-IsNull(POQty,0)) <> 0"
            objDt = GetDataTable(strCMFA)
        End If

        Me.txtPONo.Text = GetNextDocNo("Template", 3, "PurchaseOrderTemplatemasterTable", "PurchaseOrderNo")
        setVoucherNo = Me.txtPONo.Text
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        gobjLocationId = MyCompanyId
        objCon = Con

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans

            objCommand.CommandText = ""
            Dim intReceivingId As Integer = 0I
            If Val(SaveAsTemplateToolStripMenuItem.Tag.ToString) > 0 Then
                intReceivingId = Val(SaveAsTemplateToolStripMenuItem.Tag.ToString)
                objCommand.CommandText = "Update PurchaseOrderTemplateMasterTable set LocationId = " & gobjLocationId & " , Remarks = " & txtRemarks.Text.ToString.Replace("'", "''") & " where PurchaseOrderId = " & intReceivingId & ""
            Else
                objCommand.CommandText = "Insert into PurchaseOrderTemplateMasterTable (LocationId, PurchaseOrderNo,PurchaseOrderDate,VendorId,PurchaseOrderQty,PurchaseOrderAmount, CashPaid, Remarks,UserName, Status, LCID, CurrencyType, CurrencyRate, Receiving_Date, Terms_And_Condition,Post, RefCMFADocId,CostCenterId,POType,POStockDispatchStatus,TotalInwardExpenses,PurchaseDemandId,PayTypeId) values( " _
                                  & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.ToString.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & Val(Me.cmbLC.Value) & ", " & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", " & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & ReplaceNewLine(Me.txtTerms_And_Condition.Text, False).Replace("'", "''") & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ", " & IIf(Me.cmbCMFADoc.SelectedIndex = -1, 0, Me.cmbCMFADoc.SelectedValue) & "," & Me.cmbProject.SelectedValue & ",N'" & Me.cmbPOType.SelectedItem & "',N'" & Me.cmbStockDispatchStatus.Value & "','" & Me.grdInwardExpDetail.GetTotal(Me.grdInwardExpDetail.RootTable.Columns("Exp_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) & "'," & Me.cmbPurchaseDemand.Value & "," & Me.cmbPaymentTypes.SelectedValue & ") Select @@Identity"

                getVoucher_Id = objCommand.ExecuteScalar
            End If

            objCommand.CommandText = "delete from PurchaseOrderTemplateDetailTable where PurchaseOrderId=" & intReceivingId
            objCommand.ExecuteNonQuery()


            If arrFile.Count > 0 Then
                SaveDocument(getVoucher_Id, Me.Name, trans)
            End If
            For i = 0 To grd.RowCount - 1

                objCommand.CommandText = ""

                objCommand.CommandText = "Insert into PurchaseOrderTemplateDetailTable (PurchaseOrderId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,PackPrice, Pack_Desc,TaxPercent, Comments,DemandID, AdTax_Percent, AdTax_Amount, PurchaseDemandDetailId, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, SerialNo) values( " _
                                    & " ident_current('PurchaseOrderTemplateMasterTable'), " & Val(grd.GetRows(i).Cells(GrdEnum.LocationId).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", " _
                                    & " " & Val(grd.GetRows(i).Cells(GrdEnum.TotalQty).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value) & "," & Val(Me.grd.GetRows(i).Cells(GrdEnum.PackPrice).Value.ToString) & ", N'" & grd.GetRows(i).Cells(GrdEnum.Pack_Desc).Value.ToString.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "'," & Val(grd.GetRows(i).Cells("PurchaseDemandId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PurchaseDemandDetailId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", N'" & grd.GetRows(i).Cells("SerialNo").Value.ToString.Replace("'", "''") & "') "

                objCommand.ExecuteNonQuery()

                'objCommand.CommandText = "UPDATE  PurchaseDemandDetailTable " _
                '                               & " SET DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", DeliveredTotalQty= IsNull(DeliveredTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(GrdEnum.TotalQty).Value) & " " _
                '                               & " WHERE     (PurchaseDemandId = " & Val(grd.GetRows(i).Cells(GrdEnum.PurchaseDemandId).Value) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ") And (PurchaseDemandDetailId =" & Val(grd.GetRows(i).Cells("PurchaseDemandDetailId").Value.ToString) & ")  "
                'objCommand.ExecuteNonQuery()



            Next


            'objCommand.CommandText = "Select DISTINCT ISNULL(DemandID,0) as DemandId From PurchaseOrderTemplateDetailTable WHERE PurchaseOrderId=ident_current('PurchaseOrderTemplateMasterTable') AND ISNULL(Qty,0) <> 0"
            'Dim dtPO As New DataTable
            'Dim daPO As New OleDbDataAdapter(objCommand)
            'daPO.Fill(dtPO)
            'If dtPO IsNot Nothing Then
            '    If dtPO.Rows.Count > 0 Then
            '        For Each row As DataRow In dtPO.Rows


            '            objCommand.CommandText = "Select isnull(Sz1,0)-isnull(DeliveredQty , 0) as DeliveredQty from PurchaseDemandTemplateDetailTable where PurchaseDemandId = " & row("DemandId") & " And isnull(Sz1,0)-isnull(DeliveredQty , 0) > 0 "

            '            Dim daPOQty As New OleDbDataAdapter(objCommand)
            '            Dim dtPOQty As New DataTable
            '            daPOQty.Fill(dtPOQty)
            '            Dim blnEqual1 As Boolean = True
            '            If dtPOQty.Rows.Count > 0 Then

            '                blnEqual1 = False

            '            End If
            '            If blnEqual1 = True Then
            '                objCommand.CommandText = "Update PurchaseDemandTemplateMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where PurchaseDemandId = " & row("DemandId") & ""
            '                objCommand.ExecuteNonQuery()
            '            Else
            '                objCommand.CommandText = "Update PurchaseDemandTemplateMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where PurchaseDemandId = " & row("DemandId") & ""
            '                objCommand.ExecuteNonQuery()
            '            End If
            '        Next
            '    End If
            'End If




            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdInwardExpDetail.GetRows
            '    If Val(r.Cells("Exp_Amount").Value.ToString) <> 0 Then
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO InwardExpenseDetailTable(PurchaseId, AccountId, Exp_Amount,DocType) Values(ident_current('PurchaseOrderMasterTable'), " & Val(r.Cells("AccountId").Value.ToString) & ", " & Val(r.Cells("Exp_Amount").Value) & ",'PO')"
            '        objCommand.ExecuteNonQuery()
            '    End If
            'Next


            'If Me.cmbCMFADoc.SelectedIndex > 0 Then

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "UPDATE CMFADetailTable SET POQty = IsNull(a.Qty,0) " _
            '     & " FROM (SELECT dbo.PurchaseOrderMasterTable.VendorId, dbo.PurchaseOrderMasterTable.RefCMFADocId, SUM(dbo.PurchaseOrderDetailTable.Sz1) AS Qty, " _
            '     & " dbo.PurchaseOrderDetailTable.ArticleDefId,dbo.PurchaseOrderDetailTable.ArticleSize " _
            '     & " FROM dbo.PurchaseOrderDetailTable INNER JOIN " _
            '     & " dbo.PurchaseOrderMasterTable ON dbo.PurchaseOrderDetailTable.PurchaseOrderId = dbo.PurchaseOrderMasterTable.PurchaseOrderId " _
            '     & " WHERE(ISNULL(dbo.PurchaseOrderMasterTable.RefCMFADocId, 0)=" & Me.cmbCMFADoc.SelectedValue & ") " _
            '     & " GROUP BY dbo.PurchaseOrderMasterTable.VendorId, dbo.PurchaseOrderMasterTable.RefCMFADocId, dbo.PurchaseOrderDetailTable.ArticleDefId,dbo.PurchaseOrderDetailTable.ArticleSize) a " _
            '     & " WHERE " _
            '     & " a.ArticleDefId = CMFADetailTable.ArticleDefId " _
            '     & " AND CMFADetailTable.DocId = a.RefCMFADocId  " _
            '     & " AND CMFADetailTable.VendorId = a.VendorId " _
            '     & " AND CMFADetailTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
            '    objCommand.ExecuteNonQuery()

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "UPDATE CMFAMasterTable SET Status=CASE WHEN IsNull(a.BalanceQty,0) <=0 Then 0 ELSE 1 end From( " _
            '               & " SELECT dbo.CMFAMasterTable.DocId, SUM(ISNULL(dbo.CMFADetailTable.Sz1, 0) - ISNULL(dbo.CMFADetailTable.POQty, 0)) AS BalanceQty " _
            '               & " FROM dbo.CMFADetailTable INNER JOIN " _
            '               & " dbo.CMFAMasterTable ON dbo.CMFADetailTable.DocId = dbo.CMFAMasterTable.DocId WHERE dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & " " _
            '               & " GROUP BY dbo.CMFAMasterTable.DocId) a " _
            '               & " WHERE(a.DocId = CMFAMasterTable.DocId) AND dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
            '    objCommand.ExecuteNonQuery()

            'End If

            'If Me.cmbPaymentTypes.SelectedIndex > 0 Then
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "Delete From tblPaymentSchedule WHERE OrderId=" & getVoucher_Id & ""
            '    objCommand.ExecuteNonQuery()
            '    objCommand.CommandText = ""
            '    Dim str As String = "SELECT Id, TermDays FROM tblPaymentTerms WHERE Id = " & Me.cmbPaymentTypes.SelectedValue & ""
            '    Dim dtDays As DataTable = GetDataTable(str)
            '    Dim Days As Integer = 0I
            '    If dtDays.Rows.Count > 0 Then
            '        Days = Convert.ToInt32(dtDays.Rows(0).Item(1))
            '    End If
            '    objCommand.CommandText = "INSERT INTO tblPaymentSchedule(PayTypeId ,SchDate, OrderId, OrderType, Amount, InitialSchDate, PaymentStatus) VALUES(" & Me.cmbPaymentTypes.SelectedValue & ", N'" & Me.dtpPODate.Value.AddDays(Days) & "', " & getVoucher_Id & ", N'PO', " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", N'" & Me.dtpPODate.Value.AddDays(Days) & "', 'UnPaid')"
            '    objCommand.ExecuteNonQuery()
            'End If


            trans.Commit()
            SaveTemplate = True

            setEditMode = False
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
        Catch ex As Exception
            trans.Rollback()
            SaveTemplate = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try
        'UpdateStatus()

        'SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Purchase, Me.txtPONo.Text.Trim, True)

        'SaveApprovalLog(EnumReferenceType.PurchaseOrder, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Purchase Order ," & cmbVendor.Text & "", Me.Name, 0)

        'SendSMS()
        'Dim ValueTable As DataTable = GetSingle(getVoucher_Id)
        'NotificationDAL.SaveAndSendNotification("Purchase Order", "PurchaseOrderMasterTable", getVoucher_Id, ValueTable, "Purchase > Purchase Order")
    End Function

    Private Sub SaveAsTemplateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsTemplateToolStripMenuItem.Click
        If Me.BtnSave.Enabled = False Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Try

            If IsDateLock(Me.dtpPODate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If
            If Me.dtpPODate.Value <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("Your can not change this becuase financial year is closed")
                Me.dtpPODate.Focus()
                Exit Sub
            End If

            Me.grd.UpdateData()

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            If SaveTemplate() Then

                If Val(SaveAsTemplateToolStripMenuItem.Tag.ToString) > 0 Then
                    msg_Information("Template Updated")
                Else
                    msg_Information("Template Saved")
                End If
                RefreshControls()
                ClearDetailControls()

                If BackgroundWorker2.IsBusy Then Exit Sub
                BackgroundWorker2.RunWorkerAsync()

                If BackgroundWorker1.IsBusy Then Exit Sub
                BackgroundWorker1.RunWorkerAsync()
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Sub DisplayPurchaseOrderTemplate()
        Dim str As String
        str = " SELECT PurchaseOrderTemplateMasterTable.PurchaseOrderId, PurchaseOrderTemplateMasterTable.PurchaseOrderNo As [PO No], vwCOADetail.detail_title AS Vendor " _
              & " FROM PurchaseOrderTemplateMasterTable LEFT OUTER JOIN vwCOADetail ON PurchaseOrderTemplateMasterTable.VendorId = vwCOADetail.coa_detail_id Order By PurchaseOrderTemplateMasterTable.PurchaseOrderId DESC"
        FillGridEx(GridEX2, str, True)
        Me.GridEX2.RootTable.Columns("PurchaseOrderId").Visible = False
        Me.GridEX2.AutoSizeColumns()
    End Sub

    Sub DisplayPurchaseOrderTemplateDetail(ByVal ReceivingID As Integer)
        Dim str As String = String.Empty

        'If Condition = String.Empty Then

        str = "SELECT Recv_D.SerialNo, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.CurrentPrice,  Case When IsNull(Recv_D.CurrentPrice,0) > IsNull(Price,0) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent,Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As CurrencyTotalAmount, " _
      & " (IsNull(Recv_D.Qty, 0)*IsNull(Recv_D.Price, 0)* Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, Isnull(Recv_D.TaxPercent,0) as TaxPercent, 0 as TaxAmount, Convert(float, 0) As CurrencyTaxAmount, IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent, IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount,  Convert(float, 0) As CurrencyAdTaxAmount, Convert(float,0) as TotalAmount, " _
      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, isnull(Recv_D.PackPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, IsNull(Recv_D.DemandId,0) as PurchaseDemandId, Recv_D.PurchaseDemandDetailId, IsNull(Recv_D.PurchaseOrderDetailId,0) as PurchaseOrderDetailId, IsNull(Recv_D.Qty, 0) As TotalQty  FROM dbo.PurchaseOrderTemplateDetailTable Recv_D INNER JOIN " _
      & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId " _
      & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
        'ElseIf Condition = "LoadSalesOrder" Then
        '    str = String.Empty

        '    str = "SELECT Recv_D.SerialNo, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Isnull(Recv_D.PurchasePrice,0) as CurrentPrice, Case When IsNull(Recv_D.CurrentPrice,0) > IsNull(Price,0) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent,Isnull(Recv_D.Price,0) as Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As CurrencyTotalAmount, " _
        '     & " (IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, Isnull(Recv_D.SalesTax_Percentage,0) as TaxPercent, 0 as TaxAmount, Convert(float, 0) As CurrencyTaxAmount, 0 as AdTax_Percent, 0 as AdTax_Amount, Convert(float, 0) As CurrencyAdTaxAmount, Convert(float,0) as TotalAmount, " _
        '     & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,  Isnull(Recv_D.PackPrice,0) as PackPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments, 0 as PurchaseDemandId, 0 As PurchaseDemandDetailId, 0 as PurchaseOrderDetailId, IsNull(Recv_D.Qty, 0) As TotalQty FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN " _
        '     & " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
        '     & " Where Recv_D.SalesOrderID =" & ReceivingID & ""
        '    FillInwardExpense(-1, "PO")

        'ElseIf Condition = "LoadCMFADocument" Then

        '    str = " SELECT '' As SerialNo, Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.POQty,0)) AS Qty,  Isnull(Recv_D.Current_Price,0) as CurrentPrice, Case When IsNull(Recv_D.CurrentPrice,0) > IsNull(Price,0) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent, Isnull(Recv_D.Price,0) as Price, 0 As BaseCurrencyId, 0 As BaseCurrencyRate, 0 As CurrencyId, 0 As CurrencyRate, 0 As CurrencyAmount, 0 As CurrencyTotalAmount, " _
        '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, 0 as TaxPercent, 0 as TaxAmount, 0 As CurrencyTaxAmount, 0 AS AdTax_Percent, 0 AS AdTax_Amount, 0 As CurrencyAdTaxAmount, Convert(float,0) as TotalAmount, " _
        '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, 0 as PackPrice, Isnull(Recv_D.PackDesc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments, 0 as PurchaseDemandId,0 as PurchaseOrderDetailId, IsNull(Recv_D.Qty, 0) As TotalQty FROM dbo.CMFADetailTable Recv_D INNER JOIN  " _
        '& " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
        '& " Where Recv_D.DocId =" & ReceivingID & " AND Recv_D.VendorId=" & Me.cmbVendor.Value & ""
        '    FillInwardExpense(-1, "PO")
        'End If
        Dim dtDisplayDetail As New DataTable
        dtDisplayDetail = GetDataTable(str)
        Me.grd.DataSource = Nothing

        dtDisplayDetail.Columns("Total").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)* CurrencyRate"
        dtDisplayDetail.Columns("CurrencyAmount").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)"
        dtDisplayDetail.Columns("TaxAmount").Expression = "((TaxPercent/100)*Total)"
        dtDisplayDetail.Columns("CurrencyTaxAmount").Expression = "(((CurrencyAmount) * IsNull(TaxPercent,0))/100)"

        dtDisplayDetail.Columns("AdTax_Amount").Expression = "((IsNull(AdTax_Percent,0)/100)*Total)"
        dtDisplayDetail.Columns("CurrencyAdTaxAmount").Expression = "(((CurrencyAmount) * IsNull(AdTax_Percent,0))/100)"
        dtDisplayDetail.Columns("TotalAmount").Expression = "([Total]+([TaxAmount]+[AdTax_Amount]))"
        dtDisplayDetail.Columns("CurrencyTotalAmount").Expression = "IsNull([CurrencyAmount],0) + (IsNull([CurrencyTaxAmount],0)+IsNull([CurrencyAdTaxAmount],0))"

        Me.grd.DataSource = dtDisplayDetail
        If dtDisplayDetail.Rows.Count > 0 Then
            If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then

                Me.cmbCurrency.Enabled = True
                If Not Me.cmbCurrency.SelectedIndex = -1 Then
                    Me.cmbCurrency.SelectedValue = 1

                End If
            Else

                CurrencyRate = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString)
                Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                Me.cmbCurrency.Enabled = True

            End If
            cmbCurrency_SelectedIndexChanged(Nothing, Nothing)
            CurrencyRate = 1
        End If
        ApplyGridSetting()
        Me.SaveAsTemplateToolStripMenuItem.Text = "Update template"
        SaveAsTemplateToolStripMenuItem.Tag = ReceivingID


    End Sub

    Private Sub GridEX2_DoubleClick(sender As Object, e As EventArgs) Handles GridEX2.DoubleClick
        Try
            If Me.GridEX2.Row < 0 Then
                Exit Sub
            Else
                DisplayPurchaseOrderTemplateDetail(GridEX2.CurrentRow.Cells("PurchaseOrderId").Value)
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        If Not Me.GridEX2.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If

        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

        Try
            Dim cm As New OleDbCommand
            Dim objTrans As OleDbTransaction
            If Con.State = ConnectionState.Closed Then Con.Open()
            objTrans = Con.BeginTransaction
            cm.Connection = Con

            cm.CommandText = "delete from PurchaseOrderTemplateDetailTable where PurchaseOrderId=" & Me.GridEX2.CurrentRow.Cells("PurchaseOrderId").Value.ToString

            cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            cm = New OleDbCommand
            cm.Connection = Con

            cm.CommandText = "delete from PurchaseOrderTemplatemasterTable where PurchaseOrderId=" & Me.GridEX2.CurrentRow.Cells("PurchaseOrderId").Value.ToString
            cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            objTrans.Commit()

            Me.txtReceivingID.Text = 0

        Catch ex As Exception
            msg_Error("Error occured while deleting record: " & ex.Message)

        Finally
            Con.Close()
        End Try

        Me.GridEX2.CurrentRow.Delete()
        msg_Information("Template Deleted")

        Me.RefreshControls()
    End Sub

    Private Sub cmbPOType_Click(sender As Object, e As EventArgs) Handles cmbPOType.Click

    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged

    End Sub
End Class
