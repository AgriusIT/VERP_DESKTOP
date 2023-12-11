''14-Dec-2013 R916   Imran Ali               Add comments column
''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction forms
''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''19-Dec-2013 R:945 , TaskId:2338        M Ijaz Javed           By default pack rupeestxtTotalQuantity
''28-Dec-2013 RM6 Imran Ali              Release 2.1.0.0 Bug
''1-Jan-2014 Tsk:2366   Imran Ali         Slow working load forms
''2-Jan-2014   Tsk:2367    Imran Ali             Calculation Problem
''11-Jan-2014 Task:2373         Imran Ali                Add Columns SubSub Title in Account List on Sales/Purchase
''11-Jan-2014 Task:2374           Imran Ali                Net Amount Sales/Purchase 
''21-Jan-2014   TASK:2388             Imran Ali                  Service Item Check Not Working Properly
'22-Jan-2014  Task:2391       Imran Ali              Avrage Rate Re-Calculation in ERP
''31-Jan-2014     TASK:2401            Imran ali                    Store Issuence Problem 
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
''03-Feb-2014        Task:2406   Imran Ali    FIELD CHOOSER restriction (Senior Rozgar)
''07-Feb-2014             TASK:2416     Imran Ali       Minus Stock Allowed Work Not Properly
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
'' Task No 2554 Update The Query Of Purchase Return LOAD the Values Of Invoices + Date on Behalf of Vendors ComboBox 
''21-Apr-2014 Task:2583 Imran Ali Comment Format On Ledger (Pasha Emb)
'Mughees 7-5-2014 Task No 2608 Update The Code to Get RoundOff Functionality
'15-May-2014 Task: 2626 Junaid project column added in sale / purchase and both returns in history tab.
''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
''02-Jul-2014 Task:2711 Imran Ali Enabled Purchase Invoice List in Purchase Return (Ravi)
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''15-Aug-2014 TAsk:2784 Imran Ali Purchase account mapping at front end (Ravi)
''11-Sep-2014 Task:M101 Imran Ali Add new field remarks 
'08-Jun-2015  Task#2015060005 to allow all files to attach
'10-6-2015 TASKM106151 Imran Ali Partially Purchase Invoice Return
'12-Jun-2015 Task#A1 12-06-2015 Ahmad Sharif: Add Check on grdSaved to check on double click if row less than zero than exit
'12-Jun-2015 Task#A2 12-06-2015 Ahmad Sharif: Numeric validation on some textboxes
'12-Jun-2015 Task#A3 12-06-2015 Ahmad Sharif: Check Vendor exist in combox list or not
'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'16-Sep-2015 Task#16092015 Ahmad Sharif: Load Companies and Locations user wise
''13-Nov-2015 'TASK-TFS-51 Added Fields AdTax_Percent, AdTax_Amount
'' TASK-470 Muhammad Ameen on 01-07-2016: Stock Statement Report By Pack
'' TASK: TFS1357 Converted Qty columns to decimal from double to show end zero after points. Ameen on 22-08-2017 
''TASK TFS1427 User Name should be included in voucher.
''TASK TFS1474 Muhammad Ameen on 15-09-2017. Currency rate can not be edited while base currency is selected.
'' TASK TFS1592 Ayesha Rehman on 19-10-2017 Future date entry should be rights based
''TFS2988 Ayesha Rehman : 09-04-2018 : If document is approved from one stage then it should not change able for previous stage
''TFS2989 Ayesha Rehman : 10-04-2018 : If document is rejected from one stage then it should open for previous stage for approval
''TFS4161 Ayesha Rehman : 09-08-2018 : P QTY: (Should Be Static/ Un-Changeable / Un-Editable on All Screens)
''TFS4689 Ayesha Rehman : 03-10-2018 : Show only relevant Accounts on Transactional screens while User wise COA Configuration.
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports SBDal.StockDAL
Imports SBDal.StockDocTypeDAL
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
Imports System.Text

Public Class frmPurchaseReturn
    Implements IGeneral
    Dim dt As DataTable
    Dim IsFormOpend As Boolean = False
    Dim Mode As String = "Normal"
    Dim blEditMode As Boolean = False
    Dim StockMaster As StockMaster
    Dim StockDetail As StockDetail
    Dim Email As Email
    Dim crpt As New ReportDocument
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim setVoucherNo As String = String.Empty
    Dim setEditMode As Boolean = False
    Dim Total_Amount As Double = 0D
    Dim GetPurchaseReturnId As Integer = 0
    Dim Previouse_Amount As Double = 0D
    Dim TaxAmount As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim flgSelectProject As Boolean = False
    Dim flgCompanyRights As Boolean = False
    Dim flgCurrenyonOpenLC As Boolean = False
    Dim StockList As List(Of StockDetail)
    Private flgLocationWiseItems As Boolean = False 'Task:2366 Declare Location Wise Flag Variable
    ''Task:2376 Declare Boolean Variables
    Dim flgCommentCustomerFormat As Boolean = False
    Dim flgCommentArticleFormat As Boolean = False
    Dim flgCommentArticleSizeFormat As Boolean = False
    Dim flgCommentArticleColorFormat As Boolean = False
    Dim flgCommentQtyFormat As Boolean = False
    Dim flgCommentPriceFormat As Boolean = False
    Dim flgCommentRemarksFormat As Boolean = False
    Dim flgPurchaseAccountFrontEnd As Boolean = False
    Dim blnUpdateAll As Boolean = False
    'Marked Against Task#2015060001 Ali Ansari
    'Dim arrfile As String
    'Marked Against Task#2015060001 Ali Ansari
    'Altered Against Task#2015060001 Ali Ansari
    ' Convert string into List of string for making proper count
    Dim arrFile As List(Of String)
    'Altered Against Task#2015060001 Ali Ansari
    'End Task:2376
    Private blnDisplayAll As Boolean = False
    Dim BaseCurrencyId As Integer = 0
    Dim BaseCurrencyName As String = String.Empty
    Dim AvailableStockInLoose As Double = 0D
    Dim AvailableStockInPack As Double = 0D
    Dim AvailableStock As Double = 0D
    Dim StockChecked As Boolean = False
    Dim NotificationDAL As New NotificationTemplatesDAL
    Dim IsPreviewInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
    'Code Edit for task 1592 future date rights
    Dim IsDateChangeAllowed As Boolean = False
    ''TFS2375 : Ayesha Rehman : This Variable is Added to check ApprovalProcessId ,if it is mapped against the document
    Dim ApprovalProcessId As Integer = 0
    ''TFS2375 : Ayesha Rehman :End
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Dim ItemFilterByName As Boolean = False

    Dim EmailTemplate As String = String.Empty
    Dim UsersEmail As List(Of String)
    Dim EmailBody As String = String.Empty
    Dim PurchaseReturnNo As String
    Dim PurchaseReturnId As Integer
    Dim AllFields As List(Of String)
    Dim AfterFieldsElement As String = String.Empty
    Dim dtEmail As DataTable
    Dim EmailDAL As New EmailTemplateDAL
    Dim html As StringBuilder
    Dim VendorEmails As String = String.Empty
    Enum GrdEnum
        LocationId
        ArticleCode
        Item
        AlternativeItem
        BatchNo
        Unit
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
        CategoryId
        ItemId
        PackQty
        'CurrentPrice
        PackPrice
        BatchId
        Tax_Percent
        Tax_Amount
        CurrencyTaxAmount
        AdTax_Percent
        AdTax_Amount
        CurrencyAdTaxAmount
        TotalAmount 'Task:2374 Addd Index
        Transportation_Charges
        Pack_Desc
        AccountId
        ''R-916 Added Index Comments
        Comments
        Cost_Price
        RefPurchaseDetail 'TaskM106151 Added Column In Purchase Detail Record(DisplayDetail(),DisplayPODetail() e.g)
        TotalQty          ''TASK-408 
        AlternativeItemId
    End Enum

    Private Sub frmPurchaseReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
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
    Private Sub frmPurchaseReturn_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            'Task:2376 Get Comments Configurations
            If BackgroundWorker3.IsBusy Then Exit Sub
            BackgroundWorker3.RunWorkerAsync()
            Do While BackgroundWorker3.IsBusy
                Application.DoEvents()
            Loop
            'End Task:2376
            'If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            '    flgCompanyRights = getConfigValueByType("CompanyRights")
            'End If

            'If Not getConfigValueByType("CurrencyonOpenLC").ToString = "Error" Then
            '    flgCurrenyonOpenLC = Convert.ToBoolean(getConfigValueByType("CurrencyonOpenLC").ToString)
            'End If
            ' ''Task:2366 Added Location Wise Filter Configuration
            'If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
            '    flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
            'End If
            ''End Task:2366
            ' ''TASK TFS4544
            'If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            '    ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            'End If
            ' ''END TFS4544
            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)
            FillCombo("Category")
            FillCombo("Vendor")
            'FillCombo("Item")
            FillCombo("SM")
            FillCombo("CostCenter")
            FillCombo("Company")
            FillCombo("Currency")
            FillCombo("PurchaseAccount")
            FillCombo("SO")
            If Not getConfigValueByType("PurchaseAccountFrontEnd").ToString = "Error" Then
                flgPurchaseAccountFrontEnd = getConfigValueByType("PurchaseAccountFrontEnd")
            End If
            ''start TFS4161
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            If Not getConfigValueByType("PurchaseDiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("PurchaseDiablePackQuantity").ToString)
            End If
            ''End TFS4161
            'R-916 Comment ArticlePack Method
            'FillCombo("ArticlePack")
            RefreshControls()
            'If frmModProperty.blnListSeachStartWith = True Then
            '    cmbItem.AutoCompleteMode = Win.AutoCompleteMode.Suggest
            '    cmbItem.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.StartsWith
            'End If

            'If frmModProperty.blnListSeachContains = True Then
            '    cmbItem.AutoCompleteMode = Win.AutoCompleteMode.Suggest
            '    cmbItem.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.Contains
            'End If
            'Me.cmbVendor.Focus()
            'DisplayRecord()
            '//This will hide Master grid
            'Me.DisplayRecord() R933 Commented History Data
            Me.grdSaved.Visible = CType(getConfigValueByType("ShowMasterGrid"), Boolean)
            Me.IsFormOpend = True
            Get_All(frmModProperty.Tags)
            'TFS3360
            UltraDropDownSearching(cmbVendor, frmMain.blnListSeachStartWith, frmMain.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmMain.blnListSeachStartWith, frmMain.blnListSeachContains)
            UltraDropDownSearching(cmbPurchaseAc, frmMain.blnListSeachStartWith, frmMain.blnListSeachContains)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
            If frmModProperty.Tags.Length > 0 Then frmModProperty.Tags = String.Empty ''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
        End Try
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub

    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
        Dim str As String = String.Empty

        'str = "SELECT     Recv.PurchaseReturnNo, Recv.PurchaseReturnDate AS Date, vwCOADetail.detail_title as CustomerName, V.PurchaseOrderNo, Recv.PurchaseReturnQty, Recv.PurchaseReturnAmount, Recv.PurchaseReturnId,  " & _
        '        "Recv.CustomerCode, EmployeeDefTable.EmployeeName, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId " & _
        '        "FROM         PurchaseReturnMasterTable Recv INNER JOIN " & _
        '        "vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
        '        "EmployeeDefTable ON Recv.EmployeeCode = EmployeeDefTable.EmployeeId LEFT OUTER JOIN " & _
        '        "PurchaseOrderMasterTable V ON Recv.POId = V.PurchaseOrderId " & _
        '        "ORDER BY Recv.PurchaseReturnNo DESC"

        'FillGrid(grdSaved, str)
        'grdSaved.Columns(10).Visible = False
        ''grdSaved.Columns(4).Visible = False
        'grdSaved.Columns(6).Visible = False
        'grdSaved.Columns(7).Visible = False
        'grdSaved.Columns("EmployeeCode").Visible = False
        'grdSaved.Columns("PoId").Visible = False

        'grdSaved.Columns(0).HeaderText = "Issue No"
        'grdSaved.Columns(1).HeaderText = "Date"
        'grdSaved.Columns(2).HeaderText = "Customer"
        'grdSaved.Columns(3).HeaderText = "S-Order"
        'grdSaved.Columns(4).HeaderText = "Qty"
        'grdSaved.Columns(5).HeaderText = "Amount"
        'grdSaved.Columns(8).HeaderText = "Employee"

        'grdSaved.Columns(0).Width = 100
        'grdSaved.Columns(1).Width = 100
        'grdSaved.Columns(2).Width = 150
        'grdSaved.Columns(4).Width = 50
        'grdSaved.Columns(5).Width = 80
        'grdSaved.Columns(8).Width = 100
        'grdSaved.Columns(9).Width = 150
        'grdSaved.RowHeadersVisible = False
        If Mode = "Normal" Then
            'str = "SELECT " & IIf(strCondition.ToString = "All", "", " Top 50") & "   Recv.PurchaseReturnNo, Recv.PurchaseReturnDate AS Date, dbo.ReceivingMasterTable.ReceivingNo, V.detail_title, Recv.PurchaseReturnQty, " & _
            '        "Recv.PurchaseReturnAmount, Recv.PurchaseReturnId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
            '        "Recv.PurchaseOrderID, IsNull(Recv.Post,0) As Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], ISNULL(Recv.CostCenterId,0) as CostCenterID, isnull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate " & _
            '        "FROM dbo.PurchaseReturnMasterTable Recv INNER JOIN " & _
            '        "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
            '        "dbo.ReceivingMasterTable ON Recv.PurchaseOrderID = dbo.ReceivingMasterTable.ReceivingId  LEFT OUTER JOIN (Select DISTINCT PurchaseReturnId,LocationId From PurchaseReturnDetailTable) as Location On Location.PurchaseReturnId = Recv.PurchaseReturnId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseReturnNo  WHERE Recv.PurchaseReturnNo Is Not NULL  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseReturnDate, 102) >  Convert(Datetime, '" & ClosingDate & "',102))") & " "
            'Task: 2626 Junaid Retrieve 'Name' field from 'tblDefCostCenter'
            'Before against task:2784
            'str = "SELECT " & IIf(strCondition.ToString = "All", "", " Top 50") & "   Recv.PurchaseReturnNo, Recv.PurchaseReturnDate AS Date, dbo.ReceivingMasterTable.ReceivingNo, V.detail_title, Recv.PurchaseReturnQty, " & _
            '        "Recv.PurchaseReturnAmount, Recv.PurchaseReturnId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
            '        "Recv.PurchaseOrderID, IsNull(Recv.Post,0) As Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], ISNULL(Recv.CostCenterId,0) as CostCenterID, isnull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, tblDefCostCenter.Name " & _
            '        "FROM tblDefCostCenter Right Outer JOIN dbo.PurchaseReturnMasterTable Recv ON tblDefCostCenter.CostCenterID=Recv.CostCenterId INNER JOIN " & _
            '        "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
            '        "dbo.ReceivingMasterTable ON Recv.PurchaseOrderID = dbo.ReceivingMasterTable.ReceivingId  LEFT OUTER JOIN (Select DISTINCT PurchaseReturnId,LocationId From PurchaseReturnDetailTable) as Location On Location.PurchaseReturnId = Recv.PurchaseReturnId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseReturnNo  WHERE Recv.PurchaseReturnNo Is Not NULL  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseReturnDate, 102) >  Convert(Datetime, '" & ClosingDate & "',102))") & " "
            'End task 2626
            'Task:2784 Added Field PurchaseAcId
            'str = "SELECT " & IIf(strCondition.ToString = "All", "", " Top 50") & "   Recv.PurchaseReturnNo, Recv.PurchaseReturnDate AS Date, dbo.ReceivingMasterTable.ReceivingNo, V.detail_title, Recv.PurchaseReturnQty, " & _
            '       "Recv.PurchaseReturnAmount, Recv.PurchaseReturnId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
            '       "Recv.PurchaseOrderID, IsNull(Recv.Post,0) As Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], ISNULL(Recv.CostCenterId,0) as CostCenterID, isnull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, tblDefCostCenter.Name, IsNull(Recv.PurchaseAcId,0) as PurchaseAcId " & _
            '       "FROM tblDefCostCenter Right Outer JOIN dbo.PurchaseReturnMasterTable Recv ON tblDefCostCenter.CostCenterID=Recv.CostCenterId INNER JOIN " & _
            '       "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
            '       "dbo.ReceivingMasterTable ON Recv.PurchaseOrderID = dbo.ReceivingMasterTable.ReceivingId  LEFT OUTER JOIN (Select DISTINCT PurchaseReturnId,LocationId From PurchaseReturnDetailTable) as Location On Location.PurchaseReturnId = Recv.PurchaseReturnId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseReturnNo  WHERE Recv.PurchaseReturnNo Is Not NULL  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseReturnDate, 102) >  Convert(Datetime, '" & ClosingDate & "',102))") & " "
            'Maerked Against Task#2015060006
            'str = "SELECT " & IIf(strCondition.ToString = "All", "", " Top 50") & "   Recv.PurchaseReturnNo, Recv.PurchaseReturnDate AS Date, dbo.ReceivingMasterTable.ReceivingNo, V.detail_title, Recv.PurchaseReturnQty, " & _
            '     "Recv.PurchaseReturnAmount, Recv.PurchaseReturnId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
            '     "IsNull(Recv.PurchaseOrderID,0) as PurchaseOrderID, IsNull(Recv.Post,0) As Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], ISNULL(Recv.CostCenterId,0) as CostCenterID, isnull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, tblDefCostCenter.Name, IsNull(Recv.PurchaseAcId,0) as PurchaseAcId, V.Contact_Email as Email " & _
            '     "FROM tblDefCostCenter Right Outer JOIN dbo.PurchaseReturnMasterTable Recv ON tblDefCostCenter.CostCenterID=Recv.CostCenterId INNER JOIN " & _
            '     "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
            '     "dbo.ReceivingMasterTable ON Recv.PurchaseOrderID = dbo.ReceivingMasterTable.ReceivingId  LEFT OUTER JOIN (Select DISTINCT PurchaseReturnId,LocationId From PurchaseReturnDetailTable) as Location On Location.PurchaseReturnId = Recv.PurchaseReturnId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseReturnNo  WHERE Recv.PurchaseReturnNo Is Not NULL  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseReturnDate, 102) >  Convert(Datetime, '" & ClosingDate & "',102))") & " "
            'Maerked Against Task#2015060006
            'Altered Against Task#2015060006 Ali Ansari to add attachement ids 
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'str = "SELECT " & IIf(strCondition.ToString = "All", "", " Top 50") & "   Recv.PurchaseReturnNo, Recv.PurchaseReturnDate AS Date, dbo.ReceivingMasterTable.ReceivingNo, V.detail_title, Recv.PurchaseReturnQty, " & _
            '    "Recv.PurchaseReturnAmount, Recv.PurchaseReturnId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
            '    "IsNull(Recv.PurchaseOrderID,0) as PurchaseOrderID, IsNull(Recv.Post,0) As Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], ISNULL(Recv.CostCenterId,0) as CostCenterID, isnull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, tblDefCostCenter.Name, IsNull(Recv.PurchaseAcId,0) as PurchaseAcId, V.Contact_Email as Email, IsNull([No Of Attachment],0) as  [No Of Attachment] " & _
            '    "FROM tblDefCostCenter Right Outer JOIN dbo.PurchaseReturnMasterTable Recv ON tblDefCostCenter.CostCenterID=Recv.CostCenterId INNER JOIN " & _
            '    "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
            '    "dbo.ReceivingMasterTable ON Recv.PurchaseOrderID = dbo.ReceivingMasterTable.ReceivingId  LEFT OUTER JOIN (Select DISTINCT PurchaseReturnId,LocationId From PurchaseReturnDetailTable) as Location On Location.PurchaseReturnId = Recv.PurchaseReturnId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseReturnNo  LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = Recv.PurchaseReturnid  WHERE Recv.PurchaseReturnNo Is Not NULL  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseReturnDate, 102) >  Convert(Datetime, '" & ClosingDate & "',102))") & " "
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            str = "SELECT " & IIf(strCondition.ToString = "All", "", " Top 50") & "   Recv.PurchaseReturnNo, Recv.PurchaseReturnDate AS Date,dbo.ReceivingMasterTable.ReceivingId, dbo.ReceivingMasterTable.ReceivingNo, dbo.ReceivingMasterTable.ReceivingDate, V.detail_title, dbo.tblcurrency.currency_code As currency_code, " & _
              "Recv.PurchaseReturnAmount, Recv.AmountUS, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
              "IsNull(Recv.PurchaseOrderID,0) as PurchaseOrderID, IsNull(Recv.Post,0) As Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], ISNULL(Recv.CostCenterId,0) as CostCenterID, isnull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.PurchaseReturnQty, Recv.PurchaseReturnId, tblDefCostCenter.Name, IsNull(Recv.PurchaseAcId,0) as PurchaseAcId, V.Contact_Email as Email, IsNull([No Of Attachment],0) as  [No Of Attachment],Recv.UserName as 'User Name',Recv.UpdateUserName as [Modified By] " & _
              "FROM tblDefCostCenter Right Outer JOIN dbo.PurchaseReturnMasterTable Recv ON tblDefCostCenter.CostCenterID=Recv.CostCenterId INNER JOIN " & _
              "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
              " dbo.tblcurrency ON Recv.CurrencyType = dbo.tblcurrency.currency_id LEFT OUTER JOIN " & _
              "dbo.ReceivingMasterTable ON Recv.PurchaseOrderID = dbo.ReceivingMasterTable.ReceivingId  LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseReturnNo  LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = Recv.PurchaseReturnid  WHERE Recv.PurchaseReturnNo Is Not NULL  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseReturnDate, 102) >  Convert(Datetime, '" & ClosingDate & "',102))") & " "
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            If blnDisplayAll = False Then
                If flgCompanyRights = True Then
                    str += " AND Recv.LocationId=" & MyCompanyId
                End If
            End If
            If Me.dtpFrom.Checked = True Then
                str += " AND Recv.PurchaseReturnDate >= Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
            End If
            If Me.dtpTo.Checked = True Then
                str += " AND Recv.PurchaseReturnDate <= Convert(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
            End If
            If Me.txtSearchDocNo.Text <> String.Empty Then
                str += " AND Recv.PurchaseReturnNo LIKE '%" & Me.txtSearchDocNo.Text & "%'"
            End If
            If Not Me.cmbSearchLocation.SelectedIndex = -1 Then
                If Me.cmbSearchLocation.SelectedIndex <> 0 Then
                    str += " AND Recv.PurchaseReturnID In(Select PurchaseReturnID From PurchaseReturnDetailTable WHERE LocationID=" & Me.cmbSearchLocation.SelectedValue & ")"
                End If
            End If
            If Me.txtFromAmount.Text <> String.Empty Then
                If Val(Me.txtFromAmount.Text) > 0 Then
                    str += " AND Recv.PurchaseReturnAmount >= " & Val(Me.txtFromAmount.Text) & " "
                End If
            End If
            If Me.txtToAmount.Text <> String.Empty Then
                If Val(Me.txtToAmount.Text) > 0 Then
                    str += " AND Recv.PurchaseReturnAmount <= " & Val(Me.txtToAmount.Text) & ""
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
                str += " AND Recv.ReceivingNo LIKE  '%" & Me.txtPurchaseNo.Text & "%'"
            End If
            str += " order by Recv.PurchaseReturnId desc"
        End If

        FillGridEx(grdSaved, str, True)
        Me.grdSaved.RootTable.Columns.Add("Selector1")
        Me.grdSaved.RootTable.Columns("Selector1").UseHeaderSelector = True
        Me.grdSaved.RootTable.Columns("Selector1").ActAsSelector = True
        Me.grdSaved.RootTable.Columns("Selector1").Width = 50
        'Rafay Start
        grdSaved.RootTable.Columns(2).Visible = False
        grdSaved.RootTable.Columns(6).Visible = True 'Currency name
        grdSaved.RootTable.Columns(7).Visible = True 'basevalue (total Amount PKR)
        grdSaved.RootTable.Columns(8).Visible = True  'original value(total amount US$)
        grdSaved.RootTable.Columns(9).Visible = False
        grdSaved.RootTable.Columns(11).Visible = False
        grdSaved.RootTable.Columns(12).Visible = False
        'rafay End
        grdSaved.RootTable.Columns("CurrencyType").Visible = False
        grdSaved.RootTable.Columns("CurrencyRate").Visible = False
        grdSaved.RootTable.Columns("Post").Visible = False
        Me.grdSaved.RootTable.Columns("CostCenterId").Visible = False
        grdSaved.RootTable.Columns("PurchaseAcId").Visible = False
        grdSaved.RootTable.Columns("Email").Visible = False
        '' Rafay:the foreign currency is add on purchase history
        Dim grdSaved1 As DataTable = GetDataTable(str)
        grdSaved1.Columns("AmountUS").Expression = "IsNull(PurchaseReturnAmount,0) / (IsNull(CurrencyRate,0))" 'Task:2374 Show Total Amount
        Me.grdSaved.DataSource = grdSaved1
        'Set rounded format
        Me.grdSaved.RootTable.Columns("AmountUS").FormatString = "N" & DecimalPointInValue
        Me.grdSaved.RootTable.Columns("PurchaseReturnAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        grdSaved.RootTable.Columns(0).Caption = "Doc No"
        grdSaved.RootTable.Columns(1).Caption = "Doc Date"
        grdSaved.RootTable.Columns(3).Caption = "Inv No"
        grdSaved.RootTable.Columns(4).Caption = "Inv Date"
        grdSaved.RootTable.Columns(5).Caption = "Vendor Name"
        grdSaved.RootTable.Columns(6).Caption = "Currency"
        grdSaved.RootTable.Columns(7).Caption = "Base Value"
        grdSaved.RootTable.Columns(8).Caption = "Original Value"
        grdSaved.RootTable.Columns(11).Caption = "Cash Paid"
        grdSaved.RootTable.Columns(10).Caption = "Remarks"
        'Rafay:Task Start: Set rounded amount format
        Me.grdSaved.RootTable.Columns("PurchaseReturnAmount").FormatString = "N" & DecimalPointInValue
        Me.grdSaved.RootTable.Columns("PurchaseReturnAmount").FormatString = "N" & TotalAmountRounding
        ''Me.grdSaved.RootTable.Columns("PurchaseReturnAmount").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
        'rafay End task
        'Task: 2626 Junaid
        grdSaved.RootTable.Columns("Name").Visible = True
        grdSaved.RootTable.Columns("Name").Caption = "Project Name"
        'END Task: 2626
        Me.grdSaved.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
        grdSaved.RootTable.Columns(0).Width = 100
        grdSaved.RootTable.Columns(1).Width = 100
        grdSaved.RootTable.Columns(3).Width = 100
        grdSaved.RootTable.Columns(5).Width = 200
        grdSaved.RootTable.Columns(10).Width = 200

        Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat
        'Task:2759 Set rounded amount format
        Me.grdSaved.RootTable.Columns("PurchaseReturnAmount").FormatString = "N" & DecimalPointInValue
        'End task:2759

    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Validate_AddToGrid() Then
                AddItemToGrid()
                'Me.cmbItem.ActiveRow.Cells("Stock").Value = Val(Me.cmbItem.ActiveRow.Cells("Stock").Value) - Val(Me.txtQty.Text)
                'GetTotal()
                ClearDetailControls()
                cmbItem.Focus()
                Me.cmbCurrency.Enabled = False
                'FillCombo("Item")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub RefreshControls()
        'Task 1592 Ayesha Rehman Removing the ErrorProvider on btnNew
        ErrorProvider1.Clear()
        ''TASK TFS4544
        If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
        End If
        ''END TFS4544
        blEditMode = False
        If Me.BtnSave.Text = "&Update" Then
            FillCombo("Vendor")
        End If
        FillCombo("Item")
        cmbItem.Rows(0).Activate()
        txtPONo.Text = ""
        dtpPODate.Value = Now
        txtRemarks.Text = ""
        txtPaid.Text = ""
        'txtAmount.Text = ""
        txtTotal.Text = ""
        'txtTotalQty.Text = ""
        txtBalance.Text = ""
        txtPackQty.Text = 1
        'RAfay:task Start
        ''companyinitials = ""
        'Rafay:task end
        Me.BtnSave.Text = "&Save"
        'Me.txtPONo.Text = GetNextDocNo("PR", 6, "PurchaseReturnMasterTable", "PurchaseReturnNo")
        Me.txtPONo.Text = GetDocumentNo()
        'FillCombo("SO")
        'FillCombo("Item") 'R933 Commented
        Me.cmbPo.Enabled = True
        cmbUnit.SelectedIndex = 0
        Me.cmbSalesMan.SelectedIndex = 0
        cmbVendor.Rows(0).Activate()

        'grd.Rows.Clear()
        'Me.cmbVendor.Focus()
        GetSecurityRights()

        'Ayesha Rehman : TFS2375 : Enable Approval History button only in Eidt Mode
        If blEditMode = True Then
            Me.btnApprovalHistory.Visible = True
            Me.btnApprovalHistory.Enabled = True
        Else
            Me.btnApprovalHistory.Visible = False
        End If
        'Ayesha Rehman : TFS2375 : End

        ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
        ApprovalProcessId = getConfigValueByType("PurchaseReturnApproval")
        If ApprovalProcessId > 0 Then
            Me.chkPost.Visible = False
            Me.chkPost.Enabled = False
            Me.chkPost.Checked = False
        End If
        ''Ayesha Rehman :TFS2375 :End

        If Me.cmbBatchNo.Value = Nothing Then
            Me.cmbBatchNo.Enabled = False
        Else
            Me.cmbBatchNo.Enabled = True
        End If
        'FillComboByEdit() R933 Commented
        Me.dtpFrom.Value = Date.Now.AddMonths(-1)
        Me.dtpTo.Value = Date.Now
        Me.dtpFrom.Checked = False
        Me.dtpTo.Checked = False
        Me.txtSearchDocNo.Text = String.Empty
        'Me.cmbSearchLocation.SelectedIndex = 0
        If flgSelectProject = True Then
            If Not Me.cmbCostCenter.SelectedIndex = -1 Then Me.cmbCostCenter.SelectedIndex = Me.cmbCostCenter.SelectedIndex
        Else
            If Not Me.cmbCostCenter.SelectedIndex = -1 Then Me.cmbCostCenter.SelectedIndex = 0
        End If
        Me.txtFromAmount.Text = String.Empty
        Me.txtToAmount.Text = String.Empty
        Me.txtPurchaseNo.Text = String.Empty
        'Me.cmbSearchAccount.Rows(0).Activate()
        Me.txtSearchRemarks.Text = String.Empty
        Me.SplitContainer1.Panel1Collapsed = True
        FillCombo("SO")
        FillCombo("Company")
        DisplayDetail(-1)
        Me.lblPrintStatus.Text = String.Empty
        Me.dtpPODate.Enabled = True
        Me.cmbCurrency.SelectedIndex = 0
        Me.txtCurrencyRate.Text = 1

        Me.txtCurrencyRate.Enabled = False
        'If flgCurrenyonOpenLC = True Then
        '    Me.grpCurrency.Visible = True
        'Else
        '    Me.grpCurrency.Visible = False
        'End If

        'If flgCurrenyonOpenLC = True Then
        '    Me.grpCurrency.Visible = True
        '    If Not Me.cmbCurrency.SelectedIndex = -1 Then Me.cmbCurrency.SelectedIndex = 1 ''19-Dec-2013 R:945 , TaskId:2338        M Ijaz Javed           By default pack rupees
        '    Me.txtCurrencyRate.Text = 1     ''19-Dec-2013 R:945 , TaskId:2338        M Ijaz Javed           By default pack rupees
        'Else
        '    Me.grpCurrency.Visible = False
        'End If
        Me.cmbCurrency.SelectedValue = BaseCurrencyId
        Me.cmbCurrency.Enabled = True
        If flgPurchaseAccountFrontEnd = False Then
            Me.cmbPurchaseAc.Enabled = False
        Else
            Me.cmbPurchaseAc.Enabled = True
        End If

        'Clear Attached file records
        arrFile = New List(Of String)
        Me.btnAttachment.Text = "Attachment (" & arrFile.Count & ")"
        'Altered Against Task#2015060001 Ali Ansri
        'Array.Clear(arrFile, 0, arrFile.Length)

        ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
        Me.BtnEdit.Visible = False
        Me.BtnDelete.Visible = False
        Me.BtnPrint.Visible = False
        If Me.cmbPurchaseAc.ActiveRow IsNot Nothing Then Me.cmbPurchaseAc.Rows(0).Activate()
        blnUpdateAll = False
        Me.btnUpdateAll.Enabled = True
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Me.dtpPODate.Focus() 'RM6 Set Focus
        Me.txtRate.Text = ""
        Me.txtPackRate.Text = ""
    End Sub
    Private Sub ClearDetailControls()
        'cmbCategory.SelectedIndex = 0
        cmbUnit.SelectedIndex = 0
        txtQty.Text = ""
        txtRate.Text = ""
        txtTotal.Text = ""
        txtPackQty.Text = 1
        txtTotalQuantity.Text = ""
        Me.txtPackRate.Text = String.Empty
        Me.txtNetTotal.Text = ""
    End Sub

    Private Function Validate_AddToGrid() As Boolean
        Try

            'Before against task:2388
            'Dim IsMinus As Boolean = getConfigValueByType("AllowMinusStock")
            'Task:2388 Added Validate Service item
            Dim IsMinus As Boolean = True
            If Me.cmbItem.ActiveRow Is Nothing Then Return False
            If Me.cmbItem.ActiveRow.Cells(0).Value = 0 Then Return False
            If CType(Me.cmbItem.SelectedRow, Infragistics.Win.UltraWinGrid.UltraGridRow).Cells("ServiceItem").Value = False Then
                IsMinus = getConfigValueByType("AllowMinusStock")
            End If
            'End Task:2388
            If Mode = "Normal" Then

                If Not IsMinus Then
                    'Comment against task:2416
                    '    If Val(Me.txtStock.Text) <= 0 Then
                    '        ShowErrorMessage("Stock does not exist against this item...")
                    '        Me.txtQty.Focus() : Validate_AddToGrid = False : Exit Function
                    '        Return False
                    '    End If
                    'Else
                    'Task:2416
                    'TASK18 Validation On Stock
                    ''Commented Against TFS4042 : Ayesha Rehman : 30-07-2018
                    ' ''If Val(Me.txtStock.Text) <> Val(IIf(Val(txtPackQty.Text) = 0, Val(Me.txtQty.Text), Val(txtPackQty.Text)) * Val(Me.txtQty.Text)) Then
                    ' ''    If Val(Me.txtStock.Text) - IIf(Val(txtPackQty.Text) = 0, Val(Me.txtQty.Text), Val(txtPackQty.Text)) * Val(Me.txtQty.Text) <= 0 Then
                    ' ''        If MsgBox("Stock does not exist against this item..." & (Chr(13)) & "Do u want to add this item...?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, LoginUserName) = MsgBoxResult.No Then
                    ' ''            cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
                    ' ''        Else
                    ' ''            Return True
                    ' ''        End If
                    ' ''    End If
                    ' ''End If
                    If Me.cmbUnit.Text = "Loose" Then
                        If Val(Me.txtStock.Text) <> Val(Me.txtQty.Text) Then
                            If Val(Me.txtStock.Text) - Val(txtQty.Text) <= 0 Then
                                msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
                                cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
                            End If
                        End If
                    Else
                        If Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, "Loose")) <> Val(Me.txtTotalQuantity.Text) Or Val(Me.txtStock.Text) <> Val(Me.txtQty.Text) Then
                            If Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, "Loose")) - Val(txtTotalQuantity.Text) < 0 Or Val(Me.txtStock.Text) - Val(Me.txtQty.Text) < 0 Then
                                msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
                                Validate_AddToGrid = False : Exit Function
                            End If
                        End If
                    End If
                    ''End TFS4042


                    'END TASK18

                    'If Val(Me.txtStock.Text) < IIf(Val(txtPackQty.Text) = 0, 1, Val(txtPackQty.Text)) * Val(txtQty.Text) Then
                    '    If MsgBox("Stock is not enough, you want to add against this item..." & (Chr(13)) & "Do u want to add this item...?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, LoginUserName) = MsgBoxResult.No Then
                    '        txtQty.Focus() : Validate_AddToGrid = False : Exit Function
                    '    End If
                    'End If

                End If
                ''Start TFS4042
                If CType(Me.cmbCategory.SelectedItem, DataRowView).Row.Item("AllowMinusStock").ToString = "False" AndAlso IsMinus = True Then

                    If Me.cmbUnit.Text = "Loose" Then
                        If Val(Me.txtStock.Text) <> Val(Me.txtQty.Text) Then
                            If Val(Me.txtStock.Text) - Val(txtQty.Text) <= 0 Then
                                msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
                                cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
                            End If
                        End If
                    Else
                        If Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, "Loose")) <> Val(Me.txtTotalQuantity.Text) Or Val(Me.txtStock.Text) <> Val(Me.txtQty.Text) Then
                            If Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, "Loose")) - Val(txtTotalQuantity.Text) < 0 Or Val(Me.txtStock.Text) - Val(Me.txtQty.Text) < 0 Then
                                msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
                                Validate_AddToGrid = False : Exit Function
                            End If
                        End If
                    End If

                End If
                ''End TFS4042
            End If

            If cmbItem.ActiveRow.Cells(0).Value <= 0 Then
                msg_Error("Please select an item")
                cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
            End If
            UserPriceAllowedRights = GetUserPriceAllowedRights(LoginUserId)
            If UserPriceAllowedRights = True Then
                If Val(txtQty.Text) <= 0 Then
                    msg_Error("Quantity is not greater than 0")
                    txtQty.Focus() : Validate_AddToGrid = False : Exit Function
                End If

                If Val(txtRate.Text) <= 0 Then
                    msg_Error("Rate is not greater than 0")
                    txtRate.Focus() : Validate_AddToGrid = False : Exit Function
                End If
                If Val(txtTotalQuantity.Text) <= 0 Then
                    msg_Error("Total Quantity is required more than 0")
                    txtTotalQuantity.Focus() : Validate_AddToGrid = False : Exit Function
                End If
            End If
            'Rafay :task Start
            If txtTax.Text = "" Then
                msg_Error("Please select tax value ")
                txtTax.Focus() : Validate_AddToGrid = False : Exit Function
            End If
            'Rafay:task end
            'Change by murtaza default currency rate(10/26/2022) 
            If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
                msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
                txtCurrencyRate.Focus() : Validate_AddToGrid = False : Exit Function
            End If
            'Change by murtaza default currency rate(10/26/2022)
            Validate_AddToGrid = True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus
        'If Val(Me.txtPackQty.Text) = 0 Then
        '    txtPackQty.Text = 1
        '    'txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
        '    txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue) 'Task:2759 Set Rounded Amount
        'Else
        '    'txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
        '    txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)), DecimalPointInValue) 'Task:2759 Set Rounded Amount
        'End If
        'If IsSalesOrderAnalysis = True Then
        '    If Val(Me.txtDisc.Text) <> 0 Then
        '        Me.txtDisc.TabStop = True
        '    End If
        'End If
        GetDetailTotal()
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Try
            'If Me.txtPackRate.Text.Length > 0 AndAlso (Me.txtPackRate.Text) > 0 Then
            '    If Me.cmbUnit.Text <> "Loose" Then
            '        Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
            '    End If
            'End If
            ''Commented against TASK : TFS1357
            'If Val(Me.txtPackQty.Text) > 0 Then
            '    Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            'Else
            '    Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
            'End If
            ''TASK : TFS1357 Replaced Val() function with Convert.ToDecimal to retain last zero after points. Ameen on 22-08-2017
            If Val(Me.txtPackQty.Text) > 0 AndAlso Me.txtQty.Text <> "" Then
                Me.txtTotalQuantity.Text = Math.Round(Convert.ToDecimal(Me.txtPackQty.Text) * Convert.ToDecimal(Me.txtQty.Text), TotalAmountRounding)
            ElseIf Me.txtQty.Text <> "" Then
                Me.txtTotalQuantity.Text = Convert.ToDecimal(Me.txtQty.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRate.LostFocus
        Try
            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1

            '    txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue) 'Task:2759 Set Rounded Amount
            'Else

            '    txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)), DecimalPointInValue) 'Task:2759 Set Rounded Amount
            'End If
            GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
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
    Private Sub AddItemToGrid()

        'grd.Rows.Add(cmbCategory.Text, cmbItem.ActiveRow.Cells("Item").Value.ToString, txtBatchNo.Text, cmbUnit.Text, txtQty.Text, txtRate.Text, Val(txtTotal.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue, 0)

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

        Try
            Dim dtAddItemToGrd As DataTable = CType(Me.grd.DataSource, DataTable)
            dtAddItemToGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtAddItemToGrd.NewRow
            'drGrd.Item(GrdEnum.Category) = Me.cmbCategory.Text
            drGrd.Item(GrdEnum.LocationId) = Val(Me.cmbCategory.SelectedValue)
            drGrd.Item(GrdEnum.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
            drGrd.Item(GrdEnum.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
            drGrd.Item(GrdEnum.BatchNo) = Me.cmbBatchNo.Text
            If frmItemSearch.PackQty > 1 Then
                Me.cmbUnit.Text = "Pack"
                cmbUnit_SelectedIndexChanged(Nothing, Nothing)
            End If
            drGrd.Item(GrdEnum.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            'drGrd.Item(GrdEnum.Qty) = Val(Me.txtQty.Text)
            drGrd.Item(GrdEnum.Qty) = Convert.ToDecimal(Me.txtQty.Text)

            drGrd.Item(GrdEnum.Rate) = Val(Me.txtRate.Text)
            drGrd.Item(GrdEnum.Total) = Val(Me.txtTotal.Text)
            drGrd.Item(GrdEnum.CategoryId) = Me.cmbCategory.SelectedValue
            drGrd.Item(GrdEnum.ItemId) = Val(Me.cmbItem.ActiveRow.Cells(0).Value)
            drGrd.Item(GrdEnum.PackQty) = Val(Me.txtPackQty.Text)
            ''Commented Against TFS3037
            ''drGrd.Item(GrdEnum.CurrentPrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value)
            drGrd.Item(GrdEnum.CurrentPrice) = Val(Me.txtCurrentRate.Text) ''TFS3037
            drGrd.Item(GrdEnum.RateDiscPercent) = Val(Me.txtDiscPercent.Text)
            drGrd.Item(GrdEnum.PackPrice) = Val(Me.txtPackRate.Text)
            drGrd.Item(GrdEnum.BatchId) = Me.cmbBatchNo.ActiveRow.Cells(0).Value
            drGrd.Item(GrdEnum.Tax_Percent) = 0
            drGrd.Item(GrdEnum.Tax_Amount) = 0
            drGrd.Item(GrdEnum.Pack_Desc) = Me.cmbUnit.Text.ToString
            drGrd.Item(GrdEnum.AccountId) = Val(Me.cmbItem.ActiveRow.Cells("AccountId").Value.ToString)
            drGrd.Item(GrdEnum.Cost_Price) = Val(Me.cmbItem.ActiveRow.Cells("Cost_Price").Value.ToString)
            'R-916 Added Index Comments
            drGrd.Item(GrdEnum.Comments) = String.Empty
            'drGrd.Item(GrdEnum.TotalQty) = Val(txtTotalQuantity.Text)
            drGrd.Item(GrdEnum.TotalQty) = Convert.ToDecimal(txtTotalQuantity.Text)


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

            'Task 3913 Saad Afzaal change InsertAt to Add function to maintain sequence of items which add in the grid
            dtAddItemToGrd.Rows.Add(drGrd)
            'dtAddItemToGrd.Rows.InsertAt(drGrd, 0)


            'Task 3913 Saad Afzaal move scroll bar at the end when item added into the grid 
            grd.MoveLast()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged

        'If cmbCategory.SelectedIndex > 0 Then

        '    'FillCombo("ItemFilter")
        'End If
        Try
            'Before against task:2366
            'If IsFormOpend = True Then FillCombo("Item")
            If IsFormOpend = True Then
                If flgLocationWiseItems = True Then FillCombo("Item")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub GetTotal()
    '    Dim i As Integer
    '    Dim dblTotalAmount As Double
    '    Dim dblTotalQty As Double
    '    Dim dblTotalTax As Double
    '    For i = 0 To grd.Rows.Count - 1
    '        dblTotalAmount = dblTotalAmount + Val(grd.Rows(i).Cells("total").Value)
    '        dblTotalQty = dblTotalQty + Val(grd.Rows(i).Cells("Qty").Value)
    '        dblTotalTax = dblTotalTax + ((Val(grd.Rows(i).Cells("Total").Value) * Val(grd.Rows(i).Cells("Tax_Percent").Value)) / 100)
    '    Next
    '    txtAmount.Text = dblTotalAmount
    '    txtTotalQty.Text = dblTotalQty
    '    txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)
    '    txtTax.Text = dblTotalTax
    '    Me.lblRecordCount.Text = "Total Records: " & Me.grd.RowCount
    'End Sub

    Private Sub FillCombo(ByVal strCondition As String)
        Dim str As String


        If strCondition = "Item" Or strCondition = "ItemFilter" Then
            'Before against task:2388
            'str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination,Isnull(PurchasePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], Isnull(SubSubId,0) as AccountId FROM ArticleDefView where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & " "
            'Task:2388 Added Column ServiceItem
            'str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination,Isnull(PurchasePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], Isnull(SubSubId,0) as AccountId, Isnull(ServiceItem,0) as ServiceItem, ArticleDefView.SortOrder FROM ArticleDefView where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & " "
            'str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination,Isnull(PurchasePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], Isnull(SubSubId,0) as AccountId, Isnull(ServiceItem,0) as ServiceItem, ArticleDefView.SortOrder, IsNull(ArticleDefView.Cost_Price,0) as Cost_Price FROM ArticleDefView where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & ""
            str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination,Isnull(PurchasePrice,0) as Price,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model ,ArticleDefView.ArticleBrandName As Grade,ArticleDefView.SizeRangeID as [Size ID], Isnull(SubSubId,0) as AccountId, Isnull(ServiceItem,0) as ServiceItem, ArticleDefView.SortOrder, IsNull(ArticleDefView.Cost_Price,0) as Cost_Price FROM ArticleDefView where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & ""
            'End Task:2388
            'str = "SELECT  dbo.ArticleDefView.ArticleId AS Id, dbo.ArticleDefView.ArticleCode AS Code, dbo.ArticleDefView.ArticleDescription AS Item, "
            'If Not Me.cmbCategory.SelectedValue > 0 Then
            '    str = str + " dbo.ArticleDefView.ArticleSizeName AS Size, dbo.ArticleDefView.ArticleColorName AS Combination, dbo.ArticleDefView.PurchasePrice AS Price, " & _
            '          " ArticleDefView.SizeRangeID as [Size ID] FROM dbo.ArticleDefView  " & _
            '          " WHERE   (dbo.ArticleDefView.Active = 1)"
            'Else
            '    str = str + " dbo.ArticleDefView.ArticleSizeName AS Size, dbo.ArticleDefView.ArticleColorName AS Combination, dbo.ArticleDefView.PurchasePrice AS Price, " & _
            '         " ArticleDefView.SizeRangeID as [Size ID] FROM dbo.ArticleDefView " & _
            '         " WHERE (dbo.ArticleDefView.Active = 1) " 'And ArticleGroupID = " & cmbCategory.SelectedValue
            'End If
            'Comment against task:2366
            'If getConfigValueByType("ArticleFilterByLocation") = "True" Then
            '    If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
            If flgLocationWiseItems = True Then
                str += " AND  ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                'Else
                '    str += str
            End If
            'End Task:2366
            'End If
            If Me.rbtVendor.Checked = True Then
                str = str + " and ArticleDefView.MasterId in (select ArticleDefId from ArticleDefVendors where ArticleDefVendors.VendorId=" & Me.cmbVendor.Value & ") "
            End If
            'str += " ORDER By ArticleDefView.SortOrder Asc "
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
            Me.cmbItem.DataSource = Nothing
            FillUltraDropDown(Me.cmbItem, str)
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True 'Task:2388 Column Hidden ServiceItem
            Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost_Price").Hidden = True
            Me.cmbItem.Rows(0).Activate()
            If ItemFilterByName = True Then
                Me.rdoName.Checked = True
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            Else
                If Me.RdoCode.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        ElseIf strCondition = "Category" Then
            'Task#16092015 load user wise locations
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
            'Before against task:2388
            'str = "SELECT     ArticleId as Id, ArticleDescription Item, ArticleCode Code, ArticleSizeName as Size, ArticleColorName as Combination,PurchasePrice as Price, Isnull(SubSubId,0) as AccountId FROM         ArticleDefView where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & "  and ArticleGroupID = " & cmbCategory.SelectedValue
            'Task:2388 Added Column ServiceItem
            str = "SELECT     ArticleId as Id, ArticleDescription Item, ArticleCode Code, ArticleSizeName as Size, ArticleColorName as Combination,PurchasePrice as Price,ArticleDefView.ArticleBrandName As Grade, Isnull(SubSubId,0) as AccountId, IsNull(ServiceItem,0) as ServiceItem , IsNull(ArticleDefView.Cost_Price,0) as Cost_Price FROM         ArticleDefView where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & "  and ArticleGroupID = " & cmbCategory.SelectedValue
            'End Task:2388
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
            FillUltraDropDown(cmbItem, str)
            Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True 'Task:2388 Column Hidden ServiceItem
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost_Price").Hidden = True
            Me.cmbItem.Rows(0).Activate()
        ElseIf strCondition = "Vendor" Then
            'str = "SELECT     tblCustomer.AccountId AS ID, tblCustomer.CustomerName AS Name, tblListTerritory.TerritoryName AS Territory, tblListCity.CityName AS City,  " & _
            '        "tblListState.StateName AS State, tblCustomer.AccountId AS AcId " & _
            '        "FROM         tblListTerritory INNER JOIN " & _
            '        "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
            '        "tblListState ON tblListCity.StateId = tblListState.StateId INNER JOIN " & _
            '        "tblCustomer ON tblListTerritory.TerritoryId = tblCustomer.Territory"
            'Before against task:2373
            'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
            '                    "dbo.tblListTerritory.TerritoryName as Territory, tblVendor.Email, tblVendor.Phone " & _
            '                    "FROM         dbo.tblVendor INNER JOIN " & _
            '                    "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '                    "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '                    "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                    "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                    "WHERE  (dbo.vwCOADetail.account_type = 'Vendor') "
            'Comment before against task:2637
            'Task:2373 Added Column Sub Sub Title
            'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
            '             "dbo.tblListTerritory.TerritoryName as Territory, tblVendor.Email, tblVendor.Phone, vwCOADetail.Sub_Sub_Title " & _
            '             "FROM         dbo.tblVendor INNER JOIN " & _
            '             "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '             "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '             "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '             "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '             "WHERE  (dbo.vwCOADetail.account_type = 'Vendor') "
            'End Task:2373


            ''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                         "dbo.tblListTerritory.TerritoryName as Territory, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                         "FROM         dbo.tblVendor INNER JOIN " & _
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
                       "FROM         dbo.tblVendor INNER JOIN " & _
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
            If blEditMode = False Then
                str += " AND vwCOADetail.Active=1"
            Else
                str += " AND vwCOADetail.Active in(0,1,NULL)"
            End If
            str += " order by tblVendor.Sortorder, vwCOADetail.detail_title"
            FillUltraDropDown(cmbVendor, str)
            If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" 'Task:2373 Change Caption
            End If
        ElseIf strCondition = "SearchVendor" Then
        'Before against task:2637
        'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
        '                               "dbo.tblListTerritory.TerritoryName as Territory, tblVendor.Email,tblVendor.Phone " & _
        '                               "FROM dbo.tblVendor INNER JOIN " & _
        '                               "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
        '                               "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
        '                               "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
        '                               "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
        '                               "WHERE  (dbo.vwCOADetail.account_type = 'Vendor') AND dbo.vwCOADetail.detail_title Is Not NULL "
        ''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
        str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                          "dbo.tblListTerritory.TerritoryName as Territory, tblVendor.Email,tblVendor.Phone " & _
                                          "FROM dbo.tblVendor INNER JOIN " & _
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
        '' Task No 2554 Update The Query Of Purchase Return LOAD the Values Of Invoices + Date on Behalf of Vendors ComboBox 
        'str = "Select ReceivingID, ReceivingNo +' - '+ convert (varchar(12),ReceivingDate)  as  ReceivingNo, VendorId from ReceivingMasterTable where Vendorid= " & cmbVendor.Value & "and ReceivingId not in(select PurchaseOrderId from PurchaseReturnMasterTable) AND LEFT(ReceivingNo,2) <> 'SR' ORDER BY ReceivingNo DESC"
        'TaskM106151 Load Purchase Invoice
            str = "Select ReceivingID, ReceivingNo as  ReceivingNo, VendorId from ReceivingMasterTable where Vendorid= " & cmbVendor.Value & " And  ReceivingId in(Select DISTINCT ReceivingId From ReceivingDetailTable WHERE (IsNull(Sz1,0)-IsNull(PurchaseReturnQty,0)) > 0) AND LEFT(ReceivingNo,2) <> 'SR' And Post = 1 ORDER BY ReceivingNo DESC"
        Me.cmbPo.DataSource = Nothing
        FillDropDown(cmbPo, str)
        'End TaskM106151

        ElseIf strCondition = "SOComplete" Then
        str = "Select ReceivingID, ReceivingNo, VendorId from ReceivingMasterTable WHERE LEFT(ReceivingNo,2) <> 'SR' And Post = 1 ORDER BY ReceivingNo DESC"
        Me.cmbPo.DataSource = Nothing
        FillDropDown(cmbPo, str)
        ElseIf strCondition = "SM" Then
        str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE IsNull(Active,0)=1"
        'str = "Select CompanyId, CompanyName From CompanyDefTable"
        FillDropDown(Me.cmbSalesMan, str)
        ElseIf strCondition = "grdLocation" Then
        'Task#16092015 load user wise locations
        'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
        '    str = "Select Location_Id, Location_Name From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") "
        'Else
        '    str = "Select Location_Id, Location_Name From tblDefLocation"
        'End If

        str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                 & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                 & " Else " _
                 & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"


        Dim dt As DataTable = GetDataTable(str)
        Me.grd.RootTable.Columns(GrdEnum.LocationId).ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
        ElseIf strCondition = "CostCenter" Then
        FillDropDown(Me.cmbCostCenter, "If  exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " and ISNULL(CostCentre_Id, 0) > 0) " _
                & "Select CostCenterID, Name from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") order by SortOrder " _
                & "Else " _
                & "Select CostCenterID, Name from tblDefCostCenter where Active = 1 order by SortOrder")
        'ElseIf strCondition = "Currency" Then
        '    str = "Select currency_id, Currency_Code From tblCurrency"
        '    FillDropDown(Me.cmbCurrency, str)
        ElseIf strCondition = "ArticlePack" Then
        Me.cmbUnit.ValueMember = "ArticlePackId"
        Me.cmbUnit.DisplayMember = "PackName"
        Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
        ElseIf strCondition = "PurchaseAccount" Then
        FillUltraDropDown(Me.cmbPurchaseAc, "Select coa_detail_id, detail_title,detail_code From vwCOADetail where detail_title <> ''")
        Me.cmbPurchaseAc.Rows(0).Activate()
        If Me.cmbPurchaseAc.DisplayLayout.Bands(0).Columns.Count > 0 Then
            Me.cmbPurchaseAc.DisplayLayout.Bands(0).Columns(0).Hidden = True
            Me.cmbPurchaseAc.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Account"
            Me.cmbPurchaseAc.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Code"
            Me.cmbPurchaseAc.DisplayLayout.Bands(0).Columns(1).Width = 250
            Me.cmbPurchaseAc.DisplayLayout.Bands(0).Columns(2).Width = 150
        End If
        ElseIf strCondition = "Currency" Then ''TASK-407
        str = String.Empty
        str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
        FillDropDown(Me.cmbCurrency, str, False)
        Me.cmbCurrency.SelectedValue = BaseCurrencyId
        ElseIf strCondition = "Company" Then
        str = "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & " And CompanyId Is Not Null) " _
            & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId from CompanyDefTable WHERE CompanyName <> '' " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")" _
            & "Else " _
            & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
        FillDropDown(Me.cmbCompany, str, False)
        End If
    End Sub

    Private Sub txtPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaid.TextChanged
        txtBalance.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(txtPaid.Text)
    End Sub
    Public Function GetItemRateByBatch(ByVal ItemId As Integer, ByVal BatchNo As String) As Double

        Dim str As String = ""
        Dim dt As DataTable
        Try
            str = "Select Rate From  StockDetailTable  where BatchNo  <> '' "
            str += " And ArticledefId=" & ItemId & " And BatchNo = '" & BatchNo & " ' ORDER BY StockTransDetailId Desc"
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return Convert.ToDouble(dt.Rows(0).Item("Rate").ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function Save() As Boolean

        'Validation on Configuration Request No 826
        'by Imran Ali 25-9-2013
        '

        If Me.chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        Me.grd.UpdateData()
        setEditMode = False
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("PR", 6, "PurchaseReturnMasterTable", "PurchaseReturnNo")
        Me.setVoucherNo = Me.txtPONo.Text
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        gobjLocationId = MyCompanyId

        Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo.Text)

        Dim CostPrice As Double = 0D
        Dim AccountId As Integer = 0I 'Val(getConfigValueByType("PurchaseDebitAccount").ToString) 'GetConfigValue("PurchaseDebitAccount")
        Dim PurchaseTaxAccountId As Integer = Val(getConfigValueByType("PurchaseTaxDebitAccountId").ToString) 'GetConfigValue("PurchaseTaxDebitAccountId")
        Dim intAdditionalTaxAcId As Integer = Val(getConfigValueByType("AdditionalTaxAcId").ToString)
        Dim flgAvgRate As Boolean = Convert.ToBoolean(getConfigValueByType("AvgRate").ToString)
        Dim blnCheckCurrentStockByItem As Boolean = False
        If Not getConfigValueByType("CheckCurrentStockByItem").ToString = "Error" Then
            blnCheckCurrentStockByItem = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)
        End If
        'If AccountId <= 0 Then
        '    ShowErrorMessage("Purchase account is not map.")
        '    Me.dtpPODate.Focus()
        '    Return False
        'End If

        If (Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) > 0 Then
            If PurchaseTaxAccountId <= 0 Then
                ShowErrorMessage("Tax account is not map.")
                Return False
            End If
        End If

        Dim GLAccountArticleDepartment As Boolean
        If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
            GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
        Else
            GLAccountArticleDepartment = False
        End If
        'Dim strvoucherNo As String = GetNextDocNo("PV", 6, "tblVoucher", "voucher_no")
        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'objCommand.CommandText = "Insert into PurchaseReturnMasterTable (LocationId,PurchaseReturnNo,PurchaseReturnDate,VendorId,PurchaseOrderId, PurchaseReturnQty,PurchaseReturnAmount, CashPaid, Remarks,UserName, Post, CostCenterId, CurrencyType, CurrencyRate) values( " _
            '                        & gobjLocationId & ", '" & txtPONo.Text & "','" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",'" & txtRemarks.Text & "','" & LoginUserName & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ", " & Me.cmbCostCenter.SelectedValue & "," & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ") Select @@Identity"
            'R-916 Solve Comma Error on Remarks
            'before against task:2784 
            'objCommand.CommandText = "Insert into PurchaseReturnMasterTable (LocationId,PurchaseReturnNo,PurchaseReturnDate,VendorId,PurchaseOrderId, PurchaseReturnQty,PurchaseReturnAmount, CashPaid, Remarks,UserName, Post, CostCenterId, CurrencyType, CurrencyRate) values( " _
            '                        & gobjLocationId & ", '" & txtPONo.Text & "','" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",'" & txtRemarks.Text.Replace("'", "''") & "','" & LoginUserName & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ", " & Me.cmbCostCenter.SelectedValue & "," & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ") Select @@Identity"
            'Task:2784 Added Field PurchaseAcId
            objCommand.CommandText = "Insert into PurchaseReturnMasterTable (LocationId,PurchaseReturnNo,PurchaseReturnDate,VendorId,PurchaseOrderId, PurchaseReturnQty,PurchaseReturnAmount, CashPaid, Remarks,UserName, Post, CostCenterId, CurrencyType, CurrencyRate,PurchaseAcId) values( " _
                                                & Val(Me.cmbCompany.SelectedValue) & ", '" & txtPONo.Text & "','" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",'" & txtRemarks.Text.Replace("'", "''") & "','" & LoginUserName & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ", " & Me.cmbCostCenter.SelectedValue & "," & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & "," & IIf(Me.cmbPurchaseAc.Enabled = True, Me.cmbPurchaseAc.Value, "NULL") & ") Select @@Identity"
            'End Task:2784
            GetPurchaseReturnId = objCommand.ExecuteScalar ' objCommand.ExecuteNonQuery()
            'Before against task:M101
            'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '                           & " cheque_no, cheque_date,post,Source,voucher_code)" _
            '                           & " VALUES(" & gobjLocationId & ", 1,  6 , '" & Me.txtPONo.Text & "', '" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                           & " NULL,  NULL, " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,'" & Me.Name & "','" & Me.txtPONo.Text & "')" _
            '                           & " SELECT @@IDENTITY"
            'Task:M101 Added Field Remarks
            'TASK TFS1427 Added UserName and Posted_UserName columns
            objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                     & " cheque_no, cheque_date,post,Source,voucher_code,Remarks, UserName, Posted_UserName)" _
                                     & " VALUES(" & Val(Me.cmbCompany.SelectedValue) & ", 1,  6 , '" & Me.txtPONo.Text & "', '" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                     & " NULL,  NULL, " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,'" & Me.Name & "','" & Me.txtPONo.Text & "','" & Me.txtRemarks.Text.Replace("'", "''") & "', N'" & LoginUserName & "', " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ")" _
                                     & " SELECT @@IDENTITY"
            'End Task:M101

            lngVoucherMasterId = objCommand.ExecuteScalar

            If arrFile.Count > 0 Then
                SaveDocument(GetPurchaseReturnId, Me.Name, trans)
            End If


            '***********************
            'Deleting Detail
            '***********************
            objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
            objCommand.ExecuteNonQuery()


            StockList = New List(Of StockDetail)
            For i = 0 To grd.RowCount - 1

                If blnCheckCurrentStockByItem = True Then
                    CheckCurrentStockByItem(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value, Val(Me.grd.GetRows(i).Cells(GrdEnum.TotalQty).Value.ToString), Me.grd, , trans)
                End If
                Dim dblPurchasePrice As Double = 0D
                Dim dblCostPrice As Double = 0D
                Dim dblCurrencyAmount As Double = 0D
                Dim StockMasterId As String = ""
                Dim strdata As String
                If flgAvgRate = True Then

                    Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
                    strdata = "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0) as BalanceQty, SUM((IsNull(InAmount,0))-(IsNull(OutAmount,0))) as BalanceAmount, IsNull(SUM(IsNull(InQty, 0) - IsNull(OutQty, 0)), 0) AS CheckStock From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefID=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)) & "  AND StockMasterTable.DocDate < '" & dtpPODate.Value & "'  Group By ArticleDefId "

                    'Dim dblCostPrice As Double = 0D
                    Dim dtLastestPriceData As New DataTable
                    dtLastestPriceData = GetDataTable(strData, trans)
                    dtLastestPriceData.AcceptChanges()

                    If dtLastestPriceData.Rows.Count > 0 Then
                        If Val(dtLastestPriceData.Rows(0).Item(1).ToString) > 0 Then
                            dblCostPrice = Val(Val(dtLastestPriceData.Rows(0).Item(2).ToString) / Val(dtLastestPriceData.Rows(0).Item(1).ToString))
                        End If
                    End If
                Else
                    Dim strPriceData() As String = GetRateByItem(IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString))).Split(",")
                    If strPriceData.Length > 1 Then
                        dblCostPrice = Val(strPriceData(0).ToString)
                        dblPurchasePrice = Val(strPriceData(1).ToString)
                        dblCurrencyAmount = Val(strPriceData(2).ToString)
                        If dblCostPrice = 0 Then
                            dblCostPrice = dblPurchasePrice
                        End If
                    End If
                End If

                If flgAvgRate = True And getConfigValueByType("CostImplementationLotWiseOnStockMovement") = "True" Then 'Set Status Avg Rate Task:2517
                    If Convert.ToDouble(GetItemRateByBatch(IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)), IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)))) > 0 Then
                        CostPrice = Convert.ToDouble(GetItemRateByBatch(IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)), IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString))))
                    Else
                        'CostPrice = CostPrice ''Commented Against TFS3528 : Ayesha Rehman
                        CostPrice = dblCostPrice
                    End If
                ElseIf flgAvgRate = True Then
                    'Tsk:2517 Set Cost Price Of Purchase Price
                    'If dblCostPrice > 0 Then
                    CostPrice = dblCostPrice 'Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString)
                    'currencyamount = dblCurrencyAmount
                    'Else
                    '    CostPrice = dblPurchasePrice
                    '    'currencyamount = dblCurrencyAmount
                    'End If
                Else
                    'CostPrice = IIf(Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString) <= 0, Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString), Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString))
                    CostPrice = dblPurchasePrice 'Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString)
                    'CurrencyAmount = dblCurrencyAmount
                    'End Task:2517
                End If

                '********************************
                ' Update Purchase Price By Imran
                '********************************
                Dim CrrStock As Double = 0D
                Dim PurchasePriceNew As Double = 0D

                If GLAccountArticleDepartment = True Then
                    AccountId = Val(grd.GetRows(i).Cells("PurchaseAccountId").Value.ToString)
                ElseIf flgPurchaseAccountFrontEnd = True Then
                    AccountId = Me.cmbPurchaseAc.Value
                Else
                    AccountId = Val(getConfigValueByType("PurchaseDebitAccount").ToString)
                End If
                Dim str As String = "Select ServiceItem from ArticleDefView where ArticleId = " & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), grd.GetRows(i).Cells("ArticleDefId").Value) & ""
                Dim dt1 As DataTable = GetDataTable(str, trans)
                dt1.AcceptChanges()
                Dim ServiceItem As Boolean = Val(dt1.Rows(0).Item("ServiceItem").ToString)
                If ServiceItem = True Then
                    Dim str1 As String = "Select CGSAccountId from ArticleDefView where ArticleId = " & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), grd.GetRows(i).Cells("ArticleDefId").Value) & ""
                    Dim dt2 As DataTable = GetDataTable(str1, trans)
                    dt2.AcceptChanges()
                    AccountId = Val(dt2.Rows(0).Item("CGSAccountId").ToString)
                    'AccountId = "Select ServiceItem from ArticleDefView where ArticleId = " & grd.GetRows(i).Cells("ItemId").Value & ""
                End If
                If flgAvgRate = True Then
                    Try

                        'objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))) as Amount  " _
                        '                        & " FROM dbo.ArticleDefTable INNER JOIN " _
                        '                        & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & grd.GetRows(i).Cells("ArticleDefId").Value & " " _
                        '                        & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                        'Dim dtCrrStock As New DataTable
                        'Dim daCrrStock As New OleDbDataAdapter(objCommand)
                        'daCrrStock.Fill(dtCrrStock)
                        'If dtCrrStock IsNot Nothing Then
                        '    If dtCrrStock.Rows.Count > 0 Then
                        '        'Before against task:2401
                        '        'If Val(grd.GetRows(i).Cells("Price").Value) <> 0 Then
                        '        If Val(grd.GetRows(i).Cells("Price").Value) <> 0 AndAlso Val(dtCrrStock.Rows(0).Item("qty").ToString) <> 0 Then
                        '            'End Task:2401
                        '            'CrrStock = dtCrrStock.Rows(0).Item(2)
                        '            'PurchasePriceNew = ((dtCrrStock.Rows(0).Item(3))) / CrrStock
                        '            PurchasePriceNew = Val(grd.GetRows(i).Cells("Price").Value) + Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)
                        '        Else
                        '            PurchasePriceNew = Val(grd.GetRows(i).Cells("Price").Value) + Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)
                        '        End If
                        '    Else
                        '        PurchasePriceNew = Val(grd.GetRows(i).Cells("Price").Value) + Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)
                        '    End If
                        'End If

                    Catch ex As Exception

                    End Try
                End If


                StockDetail = New StockDetail
                StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                StockDetail.LocationId = grd.GetRows(i).Cells("LocationID").Value
                StockDetail.ArticleDefId = IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), grd.GetRows(i).Cells("ArticleDefId").Value)
                StockDetail.InQty = 0
                StockDetail.OutQty = Val(Me.grd.GetRow(i).Cells("TotalQty").Value.ToString)
                'StockDetail.OutQty = IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value)))
                'StockDetail.Rate = IIf(PurchasePriceNew = 0, Val(grd.GetRows(i).Cells("Price").Value.ToString), PurchasePriceNew)
                StockDetail.Rate = CostPrice
                StockDetail.InAmount = 0
                'StockDetail.OutAmount = IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", (Val(grd.GetRows(i).Cells("Qty").Value) * IIf(PurchasePriceNew = 0, Val(grd.GetRows(i).Cells("Price").Value.ToString), PurchasePriceNew)), ((Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value)) * IIf(PurchasePriceNew = 0, Val(grd.GetRows(i).Cells("Price").Value.ToString), PurchasePriceNew)))
                'StockDetail.OutAmount = IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("Price").Value.ToString)), ((Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value)) * Val(grd.GetRows(i).Cells("Price").Value.ToString)))
                StockDetail.OutAmount = Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * StockDetail.Rate

                StockDetail.Remarks = grd.GetRows(i).Cells("Comments").Value.ToString
                'StockDetail.CostPrice = CostPrice
                '' Start TASK-470 on 01-07-2016
                StockDetail.PackQty = Val(grd.GetRows(i).Cells("PackQty").Value.ToString)
                StockDetail.Out_PackQty = Val(grd.GetRows(i).Cells("Qty").Value.ToString)
                StockDetail.In_PackQty = 0
                ''End TASK-470
                StockList.Add(StockDetail)


                objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into PurchaseReturnDetailTable (PurchaseReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo,BatchID,LocationID, Tax_Percent,PackPrice, Transportation_Charges, Pack_Desc) Values ( " _
                '                        & " ident_current('PurchaseReturnMasterTable')," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " _
                '                        & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",'" & grd.GetRows(i).Cells("BatchNo").Value & _
                '                        "', " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", '" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "')"

                'R-916 Added Column Comments
                'objCommand.CommandText = "Insert into PurchaseReturnDetailTable (PurchaseReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo,BatchID,LocationID, Tax_Percent,PackPrice, Transportation_Charges, Pack_Desc, Comments) Values ( " _
                '                       & " ident_current('PurchaseReturnMasterTable')," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                '                       & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " _
                '                       & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",'" & grd.GetRows(i).Cells("BatchNo").Value & _
                '                       "', " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", '" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', '" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "')"

                'TaskM106151 Added Column RefPurchaseDetailId
                'objCommand.CommandText = "Insert into PurchaseReturnDetailTable (PurchaseReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo,BatchID,LocationID, Tax_Percent,PackPrice, Transportation_Charges, Pack_Desc, Comments,RefPurchaseDetailId) Values ( " _
                '                       & " ident_current('PurchaseReturnMasterTable')," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                '                       & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " _
                '                       & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",'" & grd.GetRows(i).Cells("BatchNo").Value & _
                '                       "', " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", '" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', '" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("RefPurchaseDetailId").Value.ToString) & ")"
                'End TaskM106151

                'TASK-TFS-51 Added Fields AdTax_Percent and AdTax_Amount
                objCommand.CommandText = "Insert into PurchaseReturnDetailTable (PurchaseReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo,BatchID,LocationID, Tax_Percent,PackPrice, Transportation_Charges, Pack_Desc, Comments,RefPurchaseDetailId,AdTax_Percent, AdTax_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, AlternativeItem, AlternativeItemId) Values ( " _
                                       & " ident_current('PurchaseReturnMasterTable')," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                                       & " " & Val(grd.GetRows(i).Cells("TotalQty").Value) & ", " _
                                       & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",'" & grd.GetRows(i).Cells("BatchNo").Value & _
                                       "', " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", '" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', '" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("RefPurchaseDetailId").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", '" & Me.grd.GetRows(i).Cells("AlternativeItem").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) & ")"
                'END TASK-TFS-51


                objCommand.ExecuteNonQuery()
                'Val(grd.Rows(i).Cells(5).Value)

                '***********************
                'Inserting Debit Amount
                '***********************
                'Before against task:2376
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction,CostCenterId) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", 0, '" & grd.GetRows(i).Cells("item").Value & "(" & Val(grd.GetRows(i).Cells("Qty").Value) & ")', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ")"
                'Before against task:2391
                ''Task:2376 Change Comments Layouts
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction,CostCenterId) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", 0, '" & SetComments(Me.grd.GetRows(i)).Replace("" & Me.cmbVendor.Text & ",", "").Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ")"
                ''End Task:2376
                'Task:2391 Added column ArticleDefId
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction,CostCenterId, ArticleDefId, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value) * Val(grd.GetRows(i).Cells("Price").Value) * Val(txtCurrencyRate.Text) & ", 0, '" & SetComments(Me.grd.GetRows(i)).Replace("" & Me.cmbVendor.Text & ",", "").Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ", " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value) * Val(grd.GetRows(i).Cells("Price").Value) & ", 0, " & Val(grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ")"
                'End Task:2391
                objCommand.ExecuteNonQuery()

                '***********************
                'Inserting Credit Amount
                '***********************
                'Before against task:2376
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", '" & grd.GetRows(i).Cells("item").Value & "(" & Val(grd.GetRows(i).Cells("Qty").Value) & ")', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ")"
                'Before against task:2391
                'Task:2376 Change Comments Layouts
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", '" & SetComments(Me.grd.GetRows(i)).Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ")"
                'End Task:2376
                'Task:2391 Added Column ArticleDefId
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId, ArticleDefId, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                                                        & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("TotalQty").Value) * CostPrice & ", '" & SetComments(Me.grd.GetRows(i)).Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ", " & Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ", 0, " & (Val(grd.GetRows(i).Cells("TotalQty").Value) * CostPrice) / Val(txtCurrencyRate.Text) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ")"
                'End Task:2391
                objCommand.ExecuteNonQuery()


                If flgAvgRate = True Then
                    If CostPrice - (Val(grd.GetRows(i).Cells("Price").Value.ToString) * Val(txtCurrencyRate.Text)) <> 0 Then
                        Dim str1 As String = "Select CGSAccountId from ArticleDefView where ArticleId = " & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), grd.GetRows(i).Cells("ArticleDefId").Value) & ""
                        Dim dt2 As DataTable = GetDataTable(str1, trans)
                        dt2.AcceptChanges()

                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId,ArticleDefId, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(dt2.Rows(0).Item("CGSAccountId").ToString) & ", " & (CostPrice - (Val(grd.GetRows(i).Cells("Price").Value.ToString) * Val(txtCurrencyRate.Text))) * Val(grd.GetRows(i).Cells("TotalQty").Value) & ", 0, '" & SetComments(Me.grd.GetRows(i)).Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ", " & Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ", " & (Val(grd.GetRows(i).Cells("TotalQty").Value) * (CostPrice / Val(txtCurrencyRate.Text)) - Val(grd.GetRows(i).Cells("Price").Value.ToString)) & ", 0, " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ")"
                        'End Task:2391
                        objCommand.ExecuteNonQuery()
                    End If

                End If
                ''''''''''''''''''''''''''''''''' Price Update '''''''''''''''''''''''''''''''''''''''
                'If flgAvgRate = True Then

                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "UPDATE ArticleDefTableMaster Set PurchasePrice=" & (PurchasePriceNew - Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)) & " WHERE ArticleId in (Select MasterId From ArticleDefTable WHERE ArticleId=" & grd.GetRows(i).Cells("ArticleDefId").Value & ")"
                '    objCommand.ExecuteNonQuery()


                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "UPDATE ArticleDefTable Set PurchasePrice=" & (PurchasePriceNew - Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)) & " WHERE ArticleId=" & grd.GetRows(i).Cells("ArticleDefId").Value & ""
                '    objCommand.ExecuteNonQuery()

                '    objCommand.CommandText = ""
                '    objCommand.CommandText = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, '" & Me.dtpPODate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102))  AND a.ArticleDefId=" & grd.GetRows(i).Cells("ArticleDefId").Value & ""
                '    Dim dtRate As New DataTable
                '    Dim daRate As New OleDbDataAdapter(objCommand)
                '    daRate.Fill(dtRate)

                '    objCommand.CommandText = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & " "
                '    Dim dtSalesItem As New DataTable
                '    Dim daSalesItem As New OleDbDataAdapter(objCommand)
                '    daSalesItem.Fill(dtSalesItem)

                '    If dtSalesItem.Rows.Count > 0 Then
                '        If Not dtRate Is Nothing Then
                '            If dtRate.Rows.Count > 0 Then
                '                objCommand.CommandText = "Update IncrementReductionTable Set PurchaseNewPrice=" & (PurchasePriceNew - Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)) & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, (PurchasePriceNew - Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString))) & "  WHERE ArticleDefId=" & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, '" & Me.dtpPODate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102)) "
                '                objCommand.ExecuteNonQuery()
                '            Else
                '                objCommand.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice) " _
                '                & " Values('" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ",  " & Val(0) & ", " & Val(0) & ", " & (PurchasePriceNew - Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)) & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, (PurchasePriceNew - Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString))) & ")"
                '                objCommand.ExecuteNonQuery()
                '            End If
                '        End If
                '    End If
                'Else
                '    'Apply Current Rate 
                '    objCommand.CommandText = "UPDATE ArticleDefTable Set PurchasePrice=" & Val(grd.GetRows(i).Cells("Price").Value.ToString) & " WHERE ArticleId=" & grd.GetRows(i).Cells("ArticleDefId").Value & ""
                '    objCommand.ExecuteNonQuery()

                '    objCommand.CommandText = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, '" & Me.dtpPODate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102))  AND a.ArticleDefId=" & grd.GetRows(i).Cells("ArticleDefId").Value & ""
                '    Dim dtRate As New DataTable
                '    Dim daRate As New OleDbDataAdapter(objCommand)
                '    daRate.Fill(dtRate)

                '    objCommand.CommandText = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & " "
                '    Dim dtSalesItem As New DataTable
                '    Dim daSalesItem As New OleDbDataAdapter(objCommand)
                '    daSalesItem.Fill(dtSalesItem)

                '    If dtSalesItem.Rows.Count > 0 Then
                '        If Not dtRate Is Nothing Then
                '            If dtRate.Rows.Count > 0 Then
                '                objCommand.CommandText = "Update IncrementReductionTable Set PurchaseNewPrice=" & Val(grd.GetRows(i).Cells("Price").Value.ToString) & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(grd.GetRows(i).Cells("Price").Value.ToString)) & "  WHERE ArticleDefId=" & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, '" & Me.dtpPODate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102)) "
                '                objCommand.ExecuteNonQuery()
                '            Else
                '                objCommand.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice) " _
                '                & " Values('" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ",  " & Val(0) & ", " & Val(0) & ", " & Val(grd.GetRows(i).Cells("Price").Value.ToString) & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(grd.GetRows(i).Cells("Price").Value.ToString)) & ")"
                '                objCommand.ExecuteNonQuery()
                '            End If
                '        End If
                '    End If
                'End If




                If Val(grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) > 0 Then
                    Dim strBudget As String
                    Dim dtBudget As DataTable
                    strBudget = "SELECT ISNULL(SOBudget,0) as SOBudget, Amount from tbldefCostCenter where CostCenterID = " & cmbCostCenter.SelectedValue & ""
                    dtBudget = GetDataTable(strBudget)
                    If dtBudget.Rows.Count > 0 Then
                        If dtBudget.Rows(0).Item(0) = "True" Then
                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction,CostCenterId, ArticleDefId) " _
                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", 0, 'Ref:Additional Tax Against " & Me.cmbVendor.Text.Replace("'", "''") & ", " & Me.txtPONo.Text.Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & 1 & ", " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ")"
                            objCommand.ExecuteNonQuery()



                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId,ArticleDefId) " _
                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & intAdditionalTaxAcId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", 'Ref:Additional Tax Againt " & Me.txtPONo.Text.Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & 1 & ", " & Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ")"
                            objCommand.ExecuteNonQuery()
                        Else
                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction,CostCenterId, ArticleDefId) " _
                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", 0, 'Ref:Additional Tax Against " & Me.cmbVendor.Text.Replace("'", "''") & ", " & Me.txtPONo.Text.Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ", " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ")"
                            objCommand.ExecuteNonQuery()



                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId,ArticleDefId) " _
                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & intAdditionalTaxAcId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", 'Ref:Additional Tax Againt " & Me.txtPONo.Text.Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ", " & Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ")"
                            objCommand.ExecuteNonQuery()
                        End If
                    Else
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction,CostCenterId, ArticleDefId) " _
                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", 0, 'Ref:Additional Tax Against " & Me.cmbVendor.Text.Replace("'", "''") & ", " & Me.txtPONo.Text.Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ", " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ")"
                        objCommand.ExecuteNonQuery()



                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId,ArticleDefId) " _
                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & intAdditionalTaxAcId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", 'Ref:Additional Tax Againt " & Me.txtPONo.Text.Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ", " & Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ")"
                        objCommand.ExecuteNonQuery()
                    End If
                End If



            Next

            'If (Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) > 0 Then

            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 0, 'Ref Purchase Return Tax:" & Me.txtPONo.Text & "', " & Me.cmbCostCenter.SelectedValue & " )"
            '    objCommand.ExecuteNonQuery()


            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & PurchaseTaxAccountId & ", 0, " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 'Ref Purchase Return Tax:" & Me.txtPONo.Text & "', " & Me.cmbCostCenter.SelectedValue & ")"
            '    objCommand.ExecuteNonQuery()


            'End If

            If (Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) > 0 Then
                'Insert Debit Amount
                Dim strBudget As String
                Dim dtBudget As DataTable
                strBudget = "SELECT ISNULL(SOBudget,0) as SOBudget, Amount from tbldefCostCenter where CostCenterID = " & cmbCostCenter.SelectedValue & ""
                dtBudget = GetDataTable(strBudget)
                If dtBudget.Rows.Count > 0 Then
                    If dtBudget.Rows(0).Item(0) = "True" Then
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, CurrencyId, CurrencyRate, Currency_Debit_Amount,Currency_Credit_Amount) " _
                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 0, 'Ref Purchase Return Tax:" & Me.txtPONo.Text & "', " & 1 & ", " & cmbCurrency.SelectedValue & ", " & Val(txtCurrencyRate.Text) & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("CurrencyTaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 0)"
                        objCommand.ExecuteNonQuery()

                        'Insert Credit Amount
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, CurrencyId, CurrencyRate, Currency_Debit_Amount,Currency_Credit_Amount) " _
                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & PurchaseTaxAccountId & ", 0, " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 'Ref Purchase Return Tax:" & Me.txtPONo.Text & "', " & 1 & ", " & cmbCurrency.SelectedValue & ", " & Val(txtCurrencyRate.Text) & ", 0, " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("CurrencyTaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ")"
                        objCommand.ExecuteNonQuery()
                    Else
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, CurrencyId, CurrencyRate, Currency_Debit_Amount,Currency_Credit_Amount) " _
                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 0, 'Ref Purchase Return Tax:" & Me.txtPONo.Text & "', " & Me.cmbCostCenter.SelectedValue & ", " & cmbCurrency.SelectedValue & ", " & Val(txtCurrencyRate.Text) & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("CurrencyTaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 0)"
                        objCommand.ExecuteNonQuery()

                        'Insert Credit Amount
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, CurrencyId, CurrencyRate, Currency_Debit_Amount,Currency_Credit_Amount) " _
                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & PurchaseTaxAccountId & ", 0, " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 'Ref Purchase Return Tax:" & Me.txtPONo.Text & "', " & Me.cmbCostCenter.SelectedValue & ", " & cmbCurrency.SelectedValue & ", " & Val(txtCurrencyRate.Text) & ", 0, " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("CurrencyTaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ")"
                        objCommand.ExecuteNonQuery()
                    End If
                Else
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, CurrencyId, CurrencyRate, Currency_Debit_Amount,Currency_Credit_Amount) " _
                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 0, 'Ref Purchase Return Tax:" & Me.txtPONo.Text & "', " & Me.cmbCostCenter.SelectedValue & ", " & cmbCurrency.SelectedValue & ", " & Val(txtCurrencyRate.Text) & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("CurrencyTaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 0)"
                    objCommand.ExecuteNonQuery()

                    'Insert Credit Amount
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, CurrencyId, CurrencyRate, Currency_Debit_Amount,Currency_Credit_Amount) " _
                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & PurchaseTaxAccountId & ", 0, " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 'Ref Purchase Return Tax:" & Me.txtPONo.Text & "', " & Me.cmbCostCenter.SelectedValue & ", " & cmbCurrency.SelectedValue & ", " & Val(txtCurrencyRate.Text) & ", 0, " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("CurrencyTaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ")"
                    objCommand.ExecuteNonQuery()
                End If
            End If
            Call AdjustmentPurchaseReturn(Val(GetPurchaseReturnId), trans)

            If Not Me.cmbPo.SelectedIndex = -1 And Me.cmbPo.SelectedIndex >= 0 Then
                SalesInvoicePartialReturn(Val(GetPurchaseReturnId), Val(Me.cmbPo.SelectedValue), objCommand, trans)
            End If
            If IsValidate() = True Then
                Call New StockDAL().Add(StockMaster, trans)
            End If
            trans.Commit()
            Save = True
            SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Purchase, Me.txtPONo.Text.Trim, True)
            SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.txtPONo.Text, True)
            ''Start TFS2375
            ''insert Approval Log
            SaveApprovalLog(EnumReferenceType.PurchaseReturn, GetPurchaseReturnId, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Purchase Return," & cmbVendor.Text & "", Me.Name, 6)
            ''End TFS2375
            'Call Save1() 'Upgrading Stock ...
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            TaxAmount = Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            SendSMS()
            Dim ValueTable As DataTable = GetSingle(GetPurchaseReturnId)
            NotificationDAL.SaveAndSendNotification("Purchase Return", "PurchaseReturnMasterTable", GetPurchaseReturnId, ValueTable, "Purchase > Purchase Return")
        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Function


    Sub InsertVoucher()

        Try
            'SaveVoucherEntry(GetVoucherTypeId("SV"), GetNextDocNo("SV", 6, "tblVoucher", "voucher_no"), Me.dtpPODate.Value, "", Nothing, GetConfigValue("PurchaseReturnCreditAccount"), Val(Me.cmbVendor.ActiveRow.Cells(0).Text.ToString), Val(Me.txtAmount.Text), Val(Me.txtAmount.Text), "Both", Me.Name, Me.txtPONo.Text, True)
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try

    End Sub

    Private Function FormValidate() As Boolean

        If txtPONo.Text = "" Then
            msg_Error("Please enter PO No.")
            txtPONo.Focus() : FormValidate = False : Exit Function
        End If
        Me.cmbVendor.Refresh()

        ''Task#A3 12-06-2015 Check Vendor exist in combox list or not
        If Not Me.cmbVendor.IsItemInList Then
            msg_Error("Please select Vendor")
            Me.cmbVendor.Focus() : FormValidate = False : Exit Function
        End If

        If Me.cmbVendor.Text = String.Empty Then
            msg_Error("Please select Vendor")
            Me.cmbVendor.Focus() : FormValidate = False : Exit Function
        End If
        ''End Task#A3 12-06-2015

        If cmbVendor.ActiveRow.Cells(0).Value <= 0 Then
            msg_Error("Please select Vendor")
            cmbVendor.Focus() : FormValidate = False : Exit Function
        End If

        If Not Me.grd.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            cmbItem.Focus() : FormValidate = False : Exit Function
        End If
        ''Start TFS2988
        If blEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document is in Approval Process") : Return False : Exit Function
                End If
            End If
        End If
        ''End TFS2988
        'Change by murtaza default currency rate(10/26/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : FormValidate = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/26/2022)
        Return True

    End Function

    Sub EditRecord()

        'If Not Me.grdSaved.RowCount > 0 Then Exit Sub
        'If Me.grd.RowCount > 0 Then
        '    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
        'End If

        'txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value.ToString
        'dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
        'txtReceivingID.Text = grdSaved.CurrentRow.Cells("ReceivingId").Value
        ''TODO. ----
        'cmbVendor.Value = grdSaved.CurrentRow.Cells(3).Value

        'txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
        'txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""
        ''Me.cmbSalesMan.SelectedValue = grdSaved.CurrentRow.Cells("EmployeeCode").Value.ToString
        'Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value
        'Call DisplayDetail(grdSaved.CurrentRow.Cells("ReceivingId").Value)
        'GetTotal()
        Me.BtnSave.Text = "&Update"
        'Me.SaveToolStripButton.Text = "&Update"
        'Me.cmbPo.Enabled = False
        If blnUpdateAll = False Then
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
        End If
        blEditMode = True

        ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
        Me.btnApprovalHistory.Visible = True
        Me.btnApprovalHistory.Enabled = True
        ''Ayesha Rehman :TFS2375 :End

        'FillCombo("Vendor")
        cmbVendor.Value = grdSaved.CurrentRow.Cells("VendorId").Value 'cmbVendor.FindStringExact((grdSaved.CurrentRow.Cells(3).Value))
        ''R933 Validate Vendor 
        txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value
        Me.GetSecurityRights()

        ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
        ApprovalProcessId = getConfigValueByType("PurchaseReturnApproval")
        If ApprovalProcessId = 0 Then
            Me.btnApprovalHistory.Visible = False
            Me.btnApprovalHistory.Enabled = False
        Else
            Me.btnApprovalHistory.Visible = True
            Me.btnApprovalHistory.Enabled = True
            Me.chkPost.Visible = False
        End If
        ''Ayesha Rehman :TFS2375 :End
        'Task 1592
        If grdSaved.CurrentRow.Cells(1).Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
            dtpPODate.MaxDate = dtpPODate.Value.AddMonths(3)
            dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
        Else
            dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
        End If
        txtReceivingID.Text = grdSaved.CurrentRow.Cells("PurchaseReturnId").Value
        RemoveHandler cmbPo.SelectedIndexChanged, AddressOf cmbPo_SelectedIndexChanged
        FillCombo("SO")
        Dim dt As New DataTable
        dt = CType(Me.cmbPo.DataSource, DataTable)
        dt.AcceptChanges()
        Dim drFound() As DataRow
        drFound = dt.Select("ReceivingId=" & Me.grdSaved.CurrentRow.Cells("PurchaseOrderID").Value & " AND VendorId=" & Me.grdSaved.GetRow.Cells("VendorId").Value & "")
        'cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PurchaseOrderID").Value
        'R933 Validate PO
        If drFound.Length <= 0 Then
            If Me.grdSaved.CurrentRow.Cells("ReceivingNo").Value.ToString.Length > 0 Then
                'dt.Columns.Add("PurchaseOrderID", GetType(System.Int32))
                'dt.Columns.Add("ReceivingNo", GetType(System.String))
                Dim dr As DataRow
                dr = dt.NewRow
                dr(0) = Me.grdSaved.CurrentRow.Cells("PurchaseOrderid").Value
                dr(1) = Me.grdSaved.CurrentRow.Cells("ReceivingNo").Value.ToString
                dr(2) = Me.grdSaved.CurrentRow.Cells("VendorId").Value.ToString
                dt.Rows.Add(dr)
                dt.AcceptChanges()
                'Me.cmbPo.DataSource = Nothing
                'Me.cmbPo.ValueMember = "PurchaseOrderID"
                'Me.cmbPo.DisplayMember = "ReceivingNo"
                'Me.cmbPo.DataSource = dt
            End If
        End If
        Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PurchaseOrderID").Value
        'End R933
        AddHandler cmbPo.SelectedIndexChanged, AddressOf cmbPo_SelectedIndexChanged

        If Me.cmbVendor.ActiveRow Is Nothing Then
            ShowErrorMessage("Vendor is disable.")
            Exit Sub
        End If

        Me.cmbCostCenter.SelectedValue = Me.grdSaved.GetRow.Cells("CostCenterId").Value
        txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
        txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""


        Me.cmbCurrency.SelectedValue = Me.grdSaved.GetRow.Cells("CurrencyType").Text.ToString
        Me.txtCurrencyRate.Text = Me.grdSaved.GetRow.Cells("CurrencyRate").Text.ToString

        If flgPurchaseAccountFrontEnd = True Then
            Me.cmbPurchaseAc.Enabled = True
            Me.cmbPurchaseAc.Value = Me.grdSaved.GetRow.Cells("PurchaseAcId").Text.ToString
        End If


        Call DisplayDetail(grdSaved.CurrentRow.Cells("PurchaseReturnId").Value)
        Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
        'GetTotal()
        'Me.cmbPo.Enabled = False 'Comment against task:2711
        Me.BtnSave.Text = "&Update"
        'GetSecurityRights()
        Me.chkPost.Checked = grdSaved.CurrentRow.Cells("Post").Value
        If blnUpdateAll = False Then Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
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

        If Me.cmbPo.SelectedIndex > 0 Then
            Me.cmbVendor.Enabled = False
        Else
            Me.cmbVendor.Enabled = True
        End If

        If getConfigValueByType("AllowChangePO").ToString.ToUpper = "TRUE" Then
            Me.cmbPo.Enabled = True
        Else
            Me.cmbPo.Enabled = False
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
        ''Hide Buttons Edit,Delete and Print on Load Form
        Me.BtnDelete.Visible = True
        Me.BtnPrint.Visible = True


    End Sub

    Private Sub DisplayPODetail(ByVal ReceivingID As Integer)
        Try


            Dim str As String
            'Dim i As Integer

            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, (Isnull(Recv_D.Price,0)) as Price, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float, 0) as TaxAmount, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,Isnull(Article_Group.SubSubId,0) as PurchaseAccountId   FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_ID = Recv_D.LocationID " _
            '      & " Where Recv_D.ReceivingID =" & ReceivingID & ""

            'R-916 Added Column Comments
            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, (Isnull(Recv_D.Price,0)) as Price, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float, 0) as TaxAmount, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments   FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_ID = Recv_D.LocationID " _
            '      & " Where Recv_D.ReceivingID =" & ReceivingID & ""
            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, (Isnull(Recv_D.Price,0)) as Price, " _
            '                 & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '                 & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float, 0) as TaxAmount,0 as Total, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments   FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
            '                 & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '                 & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_ID = Recv_D.LocationID " _
            '                 & " Where Recv_D.ReceivingID =" & ReceivingID & ""

            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) AS Qty, (Isnull(Recv_D.Price,0)) as Price, " _
            '               & " CASE WHEN recv_d.articlesize = 'Loose' THEN ((IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) * IsNull(Recv_D.Price,0)) ELSE (((IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) * IsNull(Recv_D.Price,0)) * IsNull(Article.PackQty,0)) END AS Total, " _
            '               & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float, 0) as TaxAmount,0 as Total, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments   FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
            '               & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '               & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_ID = Recv_D.LocationID " _
            '               & " Where Recv_D.ReceivingID =" & ReceivingID & " AND (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) > 0"
            'TaskM106151 Added Column RefPurchaseDetailId
            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) AS Qty, (Isnull(Recv_D.Price,0)) as Price, " _
            '   & " CASE WHEN recv_d.articlesize = 'Loose' THEN ((IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) * IsNull(Recv_D.Price,0)) ELSE (((IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) * IsNull(Recv_D.Price,0)) * IsNull(Article.PackQty,0)) END AS Total, " _
            '   & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float, 0) as TaxAmount,0 as Total, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments, IsNull(Recv_D.ReceivingDetailId,0) as RefPurchaseDetailId   FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
            '   & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '   & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_ID = Recv_D.LocationID " _
            '   & " Where Recv_D.ReceivingID =" & ReceivingID & " AND (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) > 0"
            'End TaskM106151

            'TASK-TFS-51 Added Fields AdTax_Percent And AdTax_Amount
            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) AS Qty, (Isnull(Recv_D.Price,0)) as Price, " _
            ' & " CASE WHEN recv_d.articlesize = 'Loose' THEN ((IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) * IsNull(Recv_D.Price,0)) ELSE (((IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) * IsNull(Recv_D.Price,0)) * IsNull(Article.PackQty,0)) END AS Total, " _
            ' & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float, 0) as TaxAmount,IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent, IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount,  0 as [Total Amount], Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments, IsNull(Recv_D.ReceivingDetailId,0) as RefPurchaseDetailId   FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
            ' & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            ' & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_ID = Recv_D.LocationID " _
            ' & " Where Recv_D.ReceivingID =" & ReceivingID & " AND (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) > 0"
            'End TASK-TFS-51
            '
            '' Commented against TASK-408 on 13-06-2016
            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) AS Qty,Recv_D.CurrentPrice, Case When IsNull(Recv_D.CurrentPrice,0) > (IsNull(Price,0)) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent, (Isnull(Recv_D.Price,0)) as Price, " _
            '     & " CASE WHEN recv_d.articlesize = 'Loose' THEN ((IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) * IsNull(Recv_D.Price,0)) ELSE (((IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) * IsNull(Recv_D.Price,0)) * IsNull(Article.PackQty,0)) END AS Total, " _
            '     & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float, 0) as TaxAmount,IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent, IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount,  0 as [Total Amount], Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments, 0 as Cost_Price, IsNull(Recv_D.ReceivingDetailId,0) as RefPurchaseDetailId,  (IsNull(Recv_D.Qty,0)-IsNull(Recv_D.PurchaseReturnTotalQty,0)) As TotalQty FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
            '     & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_ID = Recv_D.LocationID " _
            '     & " Where Recv_D.ReceivingID =" & ReceivingID & " AND (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) > 0"

            ''Commented below line on 22-08-2017 against TASK : TFS1357
            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) AS Qty,Recv_D.CurrentPrice, Case When IsNull(Recv_D.CurrentPrice,0) > (IsNull(Price,0)) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent, (Isnull(Recv_D.Price,0)) as Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(Float, 0) As CurrencyTotalAmount, " _
            '     & " ((IsNull(Recv_D.Qty,0)-IsNull(Recv_D.PurchaseReturnTotalQty,0)) * IsNull(Recv_D.Price,0) *  Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
            '     & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float, 0) as TaxAmount, Convert(float, 0) as CurrencyTaxAmount, IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent, IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount, Convert(float, 0) as CurrencyAdTaxAmount,  0 as [Total Amount], Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments, 0 as Cost_Price, IsNull(Recv_D.ReceivingDetailId,0) as RefPurchaseDetailId,  (IsNull(Recv_D.Qty,0)-IsNull(Recv_D.PurchaseReturnTotalQty,0)) As TotalQty FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
            '     & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_ID = Recv_D.LocationID " _
            '     & " Where Recv_D.ReceivingID =" & ReceivingID & " AND (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) > 0"


            '' TASK: TFS1357 Converted Qty columns to decimal from double to show end zero after points. Ameen on 22-08-2017 
            str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, ISNULL(Recv_D.AlternativeItem,'') AS AlternativeItem  , isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)), 1) AS Qty, Recv_D.CurrentPrice, Case When IsNull(Recv_D.CurrentPrice,0) > (IsNull(Price,0)) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent, (Isnull(Recv_D.Price,0)) as Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(Float, 0) As CurrencyTotalAmount, " _
               & " ((IsNull(Recv_D.Qty,0)-IsNull(Recv_D.PurchaseReturnTotalQty,0)) * IsNull(Recv_D.Price,0) *  Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
               & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float, 0) as TaxAmount, Convert(float, 0) as CurrencyTaxAmount, IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent, IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount, Convert(float, 0) as CurrencyAdTaxAmount,  0 as [Total Amount], Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments, 0 as Cost_Price, IsNull(Recv_D.ReceivingDetailId,0) as RefPurchaseDetailId,  Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Recv_D.Qty,0)-IsNull(Recv_D.PurchaseReturnTotalQty,0)), 1) As TotalQty, ISNULL(Recv_D.AlternativeItemId,0) AS AlternativeItemId FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
               & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
               & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_ID = Recv_D.LocationID " _
               & " Where Recv_D.ReceivingID =" & ReceivingID & " AND (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.PurchaseReturnQty,0)) > 0"

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

            '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)("BatchID"), objDataSet.Tables(0).Rows(i)("LocationID"), objDataSet.Tables(0).Rows(i).Item("Tax_Percent"))

            '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

            'Next
            Dim dtDisplayDetail As DataTable = GetDataTable(str)

            Dim IsMinus As Boolean = True
            'If CType(Me.cmbItem.SelectedRow, Infragistics.Win.UltraWinGrid.UltraGridRow).Cells("ServiceItem").Value = False Then
            IsMinus = getConfigValueByType("AllowMinusStock")
            'End If
            If IsMinus = False Then
                If dtDisplayDetail.Rows.Count > 0 Then
                    For Each dr As DataRow In dtDisplayDetail.Rows
                        dr.BeginEdit()
                        AvailableStock = Convert.ToDouble(GetStockById(IIf(Val(dr.Item("AlternativeItemId")) <> 0, Val(dr.Item("AlternativeItemId")), dr.Item("ArticleDefId")), dr.Item("LocationId"), IIf(dr.Item("unit").ToString = "Loose", "Loose", "Pack")))
                        If AvailableStock < 0 Then
                            ShowErrorMessage("Stock is negative against item " & dr.Item("Item").ToString & " ")
                            dr.Delete()
                        Else
                            If dr.Item("unit").ToString = "Loose" Then
                                If Val(dr.Item("TotalQty")) > AvailableStock Then
                                    If msg_Confirm("Stock is not enough against " & dr.Item("Item").ToString & ". Do you want to load available Qty in stock as partial Qty?") = False Then
                                        Exit Sub
                                    Else
                                        StockChecked = True
                                        dr.Item("TotalQty") = AvailableStock
                                        dr.Item("Qty") = AvailableStock
                                    End If
                                End If
                            Else
                                If Val(dr.Item("Qty")) > AvailableStock Then
                                    If msg_Confirm("Stock is not enough. Do you want to load available Qty in stock as partial Qty?") = False Then
                                        Exit Sub
                                    Else
                                        StockChecked = True
                                        dr.Item("Qty") = AvailableStock
                                    End If
                                End If

                            End If
                            dr.EndEdit()
                            AvailableStock = 0
                        End If
                    Next
                End If
            End If
            'dtDisplayDetail.Columns.Add("TaxAmount", GetType(System.Double))
            dtDisplayDetail.AcceptChanges()
            'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack', ((Qty * PackQty)* Price), (Qty * Price))"
            'dtDisplayDetail.Columns("Total").Expression = "IsNull(TotalQty, 0)*IsNull(Price, 0)"
            'dtDisplayDetail.Columns("TaxAmount").Expression = "((Total*Tax_Percent)/100)"
            'dtDisplayDetail.Columns("AdTax_Amount").Expression = "((Total*IsNull(AdTax_Percent,0))/100)"
            'dtDisplayDetail.Columns("Total Amount").Expression = "(Total + ([TaxAmount]+[AdTax_Amount]))"

            dtDisplayDetail.Columns("Total").Expression = "(IsNull(TotalQty, 0) * IsNull(Price, 0) * CurrencyRate)"
            dtDisplayDetail.Columns("TaxAmount").Expression = "((Total*Tax_Percent)/100)"
            dtDisplayDetail.Columns("AdTax_Amount").Expression = "((Total*IsNull(AdTax_Percent,0))/100)"
            dtDisplayDetail.Columns("Total Amount").Expression = "(Total + ([TaxAmount]+[AdTax_Amount]))" 'Task:2374 Show Total Amount
            dtDisplayDetail.Columns("CurrencyAmount").Expression = "(IsNull(TotalQty, 0) * IsNull(Price, 0))"
            dtDisplayDetail.Columns("CurrencyTaxAmount").Expression = "((CurrencyAmount*Tax_Percent)/100)"
            dtDisplayDetail.Columns("CurrencyAdTaxAmount").Expression = "((CurrencyAmount*IsNull(AdTax_Percent,0))/100)"
            dtDisplayDetail.Columns("CurrencyTotalAmount").Expression = "(CurrencyAmount + ([CurrencyTaxAmount]+[CurrencyAdTaxAmount]))"

            'dtDisplayDetail.Columns("Total").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)* CurrencyAmount"
            'dtDisplayDetail.Columns("CurrencyAmount").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)"
            ''dtDisplayDetail.Columns("TaxAmount").Expression = "((IIF(Unit='Pack', ((Isnull(PackQty,0)*IsNull(Qty,0))*(IsNull(Rate,0)-IsNull(Discount_Price,0))), (IsNull(Qty,0)*(IsNull(Rate,0)-IsNull(Discount_Price,0))))*IsNull(TaxPercent,0))/100)"
            ''dtDisplayDetail.Columns("TaxAmount").Expression = "((((IsNull(TotalQty,0)* IsNull(Rate,0))-IsNull(Discount_Price,0)) * IsNull(TaxPercent,0))/100)"
            'dtDisplayDetail.Columns("TaxAmount").Expression = "((((Total)-IsNull(Discount_Price,0)) * IsNull(TaxPercent,0))/100)"

            'dtDisplayDetail.Columns("CurrencyTaxAmount").Expression = "((((CurrencyAmount)-IsNull(Discount_Price,0)) * IsNull(TaxPercent,0))/100)"
            ''TASK-TFS-51 Set Expression for AdTax Amount
            ''dtDisplayDetail.Columns("AdTax_Amount").Expression = "((IIF(Unit='Pack', ((Isnull(PackQty,0)*IsNull(Qty,0))*(IsNull(Rate,0)-IsNull(Discount_Price,0))), (IsNull(Qty,0)*(IsNull(Rate,0)-IsNull(Discount_Price,0))))*IsNull(AdTax_Percent,0))/100)"
            ''dtDisplayDetail.Columns("AdTax_Amount").Expression = "((((IsNull(TotalQty,0)* IsNull(Rate,0))-IsNull(Discount_Price,0)) * IsNull(AdTax_Percent,0))/100)"
            'dtDisplayDetail.Columns("AdTax_Amount").Expression = "((((Total)-IsNull(Discount_Price,0)) * IsNull(AdTax_Percent,0))/100)"
            'dtDisplayDetail.Columns("CurrencyAdTaxAmount").Expression = "((((CurrencyAmount)-IsNull(Discount_Price,0)) * IsNull(AdTax_Percent,0))/100)"

            'dtDisplayDetail.Columns("Total Amount").Expression = "IsNull([Total],0) + (IsNull([TaxAmount],0)+IsNull([AdTax_Amount],0))" 'Task:2374 Show Total Amount
            'dtDisplayDetail.Columns("TotalCurrencyAmount").Expression = "IsNull([CurrencyAmount],0) + (IsNull([CurrencyTaxAmount],0)+IsNull([CurrencyAdTaxAmount],0))"

            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            If dtDisplayDetail.Rows.Count > 0 Then
                If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                    'Me.cmbCurrency.SelectedValue = Nothing
                    'Me.cmbCurrency.Enabled = False
                Else
                    Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                    txtCurrencyRate.Text = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString)
                    Me.cmbCurrency.Enabled = False
                End If
            End If
            ApplyGridSettings()
            FillCombo("grdLocation")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub DisplayDetail(ByVal ReceivingID As Integer)
        Try


            Dim str As String
            'Dim i As Integer

            'str = "SELECT Isnull(Recv_D.LocationID,0) as LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price) ELSE ((Recv_D.Sz1 * Recv_D.Price) * Article.PackQty) END AS Total, " _
            '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice,Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, " _
            '      & " IsNull(Recv_D.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId  FROM dbo.PurchaseReturnDetailTable Recv_D INNER JOIN " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '      & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '      & " Where Recv_D.PurchaseReturnID =" & ReceivingID & ""
            'Before against task:2375
            'R-916 Added Column Comments
            'str = "SELECT Isnull(Recv_D.LocationID,0) as LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '     & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price) ELSE ((Recv_D.Sz1 * Recv_D.Price) * Article.PackQty) END AS Total, " _
            '     & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice,Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, " _
            '     & " IsNull(Recv_D.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments  FROM dbo.PurchaseReturnDetailTable Recv_D INNER JOIN " _
            '     & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '     & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '     & " Where Recv_D.PurchaseReturnID =" & ReceivingID & ""
            'Task:2375 Added Column Total Amount
            'str = "SELECT Isnull(Recv_D.LocationID,0) as LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '& " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price) ELSE ((Recv_D.Sz1 * Recv_D.Price) * Article.PackQty) END AS Total, " _
            '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice,Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, " _
            '& " IsNull(Recv_D.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Total Amount], Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments, IsNull(Recv_D.Cost_Price,0) as Cost_Price  FROM dbo.PurchaseReturnDetailTable Recv_D INNER JOIN " _
            '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '& " Where Recv_D.PurchaseReturnID =" & ReceivingID & ""
            'TaskM106151 Added Column RefPurchaseDetailId
            '     str = "SELECT Isnull(Recv_D.LocationID,0) as LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '& " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price) ELSE ((Recv_D.Sz1 * Recv_D.Price) * Article.PackQty) END AS Total, " _
            '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice,Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, " _
            '& " IsNull(Recv_D.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Total Amount], Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments, IsNull(Recv_D.Cost_Price,0) as Cost_Price, IsNull(Recv_D.RefPurchaseDetailId,0) as RefPurchaseDetailId  FROM dbo.PurchaseReturnDetailTable Recv_D INNER JOIN " _
            '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '& " Where Recv_D.PurchaseReturnID =" & ReceivingID & ""
            'End TaskM106151
            'End Task:2375
            'TASK-TFS-51 Added Field AdTax_Percent And AdTax_Amount
            'str = "SELECT Isnull(Recv_D.LocationID,0) as LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price) ELSE ((Recv_D.Sz1 * Recv_D.Price) * Article.PackQty) END AS Total, " _
            '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice,Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, " _
            '    & " IsNull(Recv_D.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent,IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount, Convert(float,0) as [Total Amount], Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments, IsNull(Recv_D.Cost_Price,0) as Cost_Price, IsNull(Recv_D.RefPurchaseDetailId,0) as RefPurchaseDetailId  FROM dbo.PurchaseReturnDetailTable Recv_D INNER JOIN " _
            '    & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '    & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '    & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '    & " Where Recv_D.PurchaseReturnID =" & ReceivingID & ""
            'END TASK-TFS-51

            ''Commented below lines against TASK: TFS1357 ON 22-08-2017
            ' str = "SELECT Isnull(Recv_D.LocationID,0) as LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.CurrentPrice, Case When IsNull(Recv_D.CurrentPrice,0) > (IsNull(Price,0)) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent, Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(Float, 0) As CurrencyTotalAmount,  " _
            '& " ((IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0)) * Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
            '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, " _
            '& " IsNull(Recv_D.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount,  Convert(float,0) as CurrencyTaxAmount, IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent,IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount,  Convert(float,0) as CurrencyAdTaxAmount, Convert(float,0) as [Total Amount], Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments, IsNull(Recv_D.Cost_Price,0) as Cost_Price, IsNull(Recv_D.RefPurchaseDetailId,0) as RefPurchaseDetailId, IsNull(Recv_D.Qty, 0) As TotalQty  FROM dbo.PurchaseReturnDetailTable Recv_D INNER JOIN " _
            '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '& " Where Recv_D.PurchaseReturnID =" & ReceivingID & ""
            '' TASK: TFS1357 Converted Qty columns to decimal from double to show end zero after points. Ameen on 22-08-2017 
            str = "SELECT Isnull(Recv_D.LocationID,0) as LocationID, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.AlternativeItem, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Convert(Decimal(18, " & DecimalPointInQty & "), Recv_D.Sz1, 1) AS Qty, Recv_D.CurrentPrice, Case When IsNull(Recv_D.CurrentPrice,0) > (IsNull(Price,0)) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent, Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(Float, 0) As CurrencyTotalAmount,  " _
          & " ((IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0)) * Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
          & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, " _
          & " IsNull(Recv_D.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount,  Convert(float,0) as CurrencyTaxAmount, IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent,IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount,  Convert(float,0) as CurrencyAdTaxAmount, Convert(float,0) as [Total Amount], Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, Recv_D.Comments, IsNull(Recv_D.Cost_Price,0) as Cost_Price, IsNull(Recv_D.RefPurchaseDetailId,0) as RefPurchaseDetailId, Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Recv_D.Qty, 0), 1) As TotalQty, Recv_D.AlternativeItemId  FROM dbo.PurchaseReturnDetailTable Recv_D INNER JOIN " _
          & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
          & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
          & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
          & " Where Recv_D.PurchaseReturnID =" & ReceivingID & ""




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

            '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)("BatchID"), objDataSet.Tables(0).Rows(i)("LocationID"), objDataSet.Tables(0).Rows(i).Item("Tax_Percent"))

            '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

            'Next
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            'dtDisplayDetail.Columns.Add("TaxAmount", GetType(System.Double))
            dtDisplayDetail.AcceptChanges()
            'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack', ((Qty * PackQty)* Price), (Qty * Price))"
            dtDisplayDetail.Columns("Total").Expression = "(IsNull(TotalQty, 0) * IsNull(Price, 0) * CurrencyRate)"
            dtDisplayDetail.Columns("TaxAmount").Expression = "((Total*Tax_Percent)/100)"
            dtDisplayDetail.Columns("AdTax_Amount").Expression = "((Total*IsNull(AdTax_Percent,0))/100)"
            dtDisplayDetail.Columns("Total Amount").Expression = "(Total + ([TaxAmount]+[AdTax_Amount]))" 'Task:2374 Show Total Amount
            dtDisplayDetail.Columns("CurrencyAmount").Expression = "(IsNull(TotalQty, 0) * IsNull(Price, 0))"
            dtDisplayDetail.Columns("CurrencyTaxAmount").Expression = "((CurrencyAmount*Tax_Percent)/100)"
            dtDisplayDetail.Columns("CurrencyAdTaxAmount").Expression = "((CurrencyAmount*IsNull(AdTax_Percent,0))/100)"
            dtDisplayDetail.Columns("CurrencyTotalAmount").Expression = "(CurrencyAmount + ([CurrencyTaxAmount]+[CurrencyAdTaxAmount]))"

            'dtDisplayDetail.Columns("Total").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)* CurrencyAmount"
            'dtDisplayDetail.Columns("CurrencyAmount").Expression = "IsNull(TotalQty,0)*IsNull(Price,0)"
            ''dtDisplayDetail.Columns("TaxAmount").Expression = "((IIF(Unit='Pack', ((Isnull(PackQty,0)*IsNull(Qty,0))*(IsNull(Rate,0)-IsNull(Discount_Price,0))), (IsNull(Qty,0)*(IsNull(Rate,0)-IsNull(Discount_Price,0))))*IsNull(TaxPercent,0))/100)"
            ''dtDisplayDetail.Columns("TaxAmount").Expression = "((((IsNull(TotalQty,0)* IsNull(Rate,0))-IsNull(Discount_Price,0)) * IsNull(TaxPercent,0))/100)"
            'dtDisplayDetail.Columns("TaxAmount").Expression = "((((Total)-IsNull(Discount_Price,0)) * IsNull(TaxPercent,0))/100)"

            'dtDisplayDetail.Columns("CurrencyTaxAmount").Expression = "((((CurrencyAmount)-IsNull(Discount_Price,0)) * IsNull(TaxPercent,0))/100)"
            ''TASK-TFS-51 Set Expression for AdTax Amount
            ''dtDisplayDetail.Columns("AdTax_Amount").Expression = "((IIF(Unit='Pack', ((Isnull(PackQty,0)*IsNull(Qty,0))*(IsNull(Rate,0)-IsNull(Discount_Price,0))), (IsNull(Qty,0)*(IsNull(Rate,0)-IsNull(Discount_Price,0))))*IsNull(AdTax_Percent,0))/100)"
            ''dtDisplayDetail.Columns("AdTax_Amount").Expression = "((((IsNull(TotalQty,0)* IsNull(Rate,0))-IsNull(Discount_Price,0)) * IsNull(AdTax_Percent,0))/100)"
            'dtDisplayDetail.Columns("AdTax_Amount").Expression = "((((Total)-IsNull(Discount_Price,0)) * IsNull(AdTax_Percent,0))/100)"
            'dtDisplayDetail.Columns("CurrencyAdTaxAmount").Expression = "((((CurrencyAmount)-IsNull(Discount_Price,0)) * IsNull(AdTax_Percent,0))/100)"

            'dtDisplayDetail.Columns("Total Amount").Expression = "IsNull([Total],0) + (IsNull([TaxAmount],0)+IsNull([AdTax_Amount],0))" 'Task:2374 Show Total Amount
            'dtDisplayDetail.Columns("TotalCurrencyAmount").Expression = "IsNull([CurrencyAmount],0) + (IsNull([CurrencyTaxAmount],0)+IsNull([CurrencyAdTaxAmount],0))"

            ' Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            If dtDisplayDetail.Rows.Count > 0 Then
                If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                    'Me.cmbCurrency.SelectedValue = Nothing
                    Me.cmbCurrency.Enabled = False
                Else
                    Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                    Me.cmbCurrency.Enabled = False
                End If
            End If

            ApplyGridSettings()
            FillCombo("grdLocation")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function Update_Record() As Boolean
        'Validation on Configuration Request No 826
        'by Imran Ali 25-9-2013
        '' 
        'If Not Me.chkPost.Checked = True Then
        '    If Me.chkPost.Visible = False Then
        '        Me.chkPost.Checked = False
        '    End If
        'End If
        If ApprovalProcessId = 0 Then
            ''Start TFS2375
            If Me.chkPost.Visible = False Then
                Me.chkPost.Checked = False
            End If
            ''End TFS2375
        Else
            Me.chkPost.Visible = False
        End If
        setEditMode = True
        setVoucherNo = Me.txtPONo.Text
        GetPurchaseReturnId = Me.txtReceivingID.Text
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo.Text)
        Dim strVoucherNo As String = String.Empty
        Dim dt As DataTable = GetRecords("SELECT voucher_no   FROM tblVoucher  WHERE voucher_id = " & lngVoucherMasterId & " ")
        If Not dt Is Nothing Then
            If Not dt.Rows.Count = 0 Then
                strVoucherNo = dt.Rows(0)("voucher_no")
            End If
        End If
        gobjLocationId = MyCompanyId

        Dim CostPrice As Double = 0D
        Dim AccountId As Integer = 0I 'Val(getConfigValueByType("PurchaseDebitAccount").ToString) 'GetConfigValue("PurchaseDebitAccount")
        Dim PurchaseTaxAccountId As Integer = Val(getConfigValueByType("PurchaseTaxDebitAccountId").ToString) 'GetConfigValue("PurchaseTaxDebitAccountId")
        Dim blnCheckCurrentStockByItem As Boolean = False
        If Not getConfigValueByType("CheckCurrentStockByItem").ToString = "Error" Then
            blnCheckCurrentStockByItem = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)
        End If
        'If AccountId <= 0 Then
        '    ShowErrorMessage("Purchase account is not map.")
        '    Me.dtpPODate.Focus()
        '    Return False
        'End If

        If (Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) > 0 Then
            If PurchaseTaxAccountId <= 0 Then
                ShowErrorMessage("Tax account is not map.")
                Return False
            End If
        End If

        Dim flgAvgRate As Boolean = Convert.ToBoolean(getConfigValueByType("AvgRate").ToString)
        Dim intAdditionalTaxAcId As Integer = Val(getConfigValueByType("AdditionalTaxAcId").ToString)
        Dim GLAccountArticleDepartment As Boolean
        If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
            GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
        Else
            GLAccountArticleDepartment = False
        End If
        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()

        Dim cmd As New OleDbCommand
        cmd.Connection = Con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT Isnull(Qty,0) as Qty, ArticleDefID, Isnull(Price,0) as Rate FROM PurchaseReturnDetailTable WHERE  PurchaseReturnId = " & Me.txtReceivingID.Text & ""
        Dim da As New OleDbDataAdapter(cmd)
        Dim dtSavedItems As New DataTable
        da.Fill(dtSavedItems)

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'objCommand.CommandText = "Update PurchaseReturnMasterTable set PurchaseReturnNo ='" & txtPONo.Text & "',PurchaseReturnDate='" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", PurchaseOrderId=" & Me.cmbPo.SelectedValue & ", " _
            '& " PurchaseReturnQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",PurchaseReturnAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks='" & txtRemarks.Text & "',UserName='" & LoginUserName & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", CostCenterId=" & Me.cmbCostCenter.SelectedValue & ", CurrencyType=" & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & ", CurrencyRate=" & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & " Where PurchaseReturnID= " & txtReceivingID.Text & " "

            'R-916 Solve Comma error on remarks
            objCommand.CommandText = "Update PurchaseReturnMasterTable set LocationId ='" & Val(Me.cmbCompany.SelectedValue) & "', PurchaseReturnNo ='" & txtPONo.Text & "',PurchaseReturnDate='" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", PurchaseOrderId=" & Me.cmbPo.SelectedValue & ", " _
            & " PurchaseReturnQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",PurchaseReturnAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks='" & txtRemarks.Text.Replace("'", "''") & "',UpdateUserName='" & LoginUserName & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", CostCenterId=" & Me.cmbCostCenter.SelectedValue & ", CurrencyType=" & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & ", CurrencyRate=" & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", PurchaseAcId=" & IIf(Me.cmbPurchaseAc.Enabled = True, Me.cmbPurchaseAc.Value, "NULL") & " Where PurchaseReturnID= " & txtReceivingID.Text & " "
            objCommand.ExecuteNonQuery()

            'TASKM106151 Remove Purchase Return Qty In Purchase Detail Table
            If Not Me.cmbPo.SelectedIndex = -1 And Me.cmbPo.SelectedIndex >= 0 Then
                DeleteInvoicePartialReturn(Val(txtReceivingID.Text), Val(Me.cmbPo.SelectedValue), objCommand, trans)
            End If
            'End Task

            objCommand.CommandText = "Delete from PurchaseReturnDetailTable where PurchaseReturnID = " & txtReceivingID.Text
            objCommand.ExecuteNonQuery()
            'Before against task:M101
            'objCommand.CommandText = "update tblVoucher set voucher_date='" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & "" _
            '                        & "   where voucher_id=" & lngVoucherMasterId
            'TAsk:M101 Added Field Remarks
            'TASK TFS1427 Added UserName column
            objCommand.CommandText = "update tblVoucher set location_id = '" & Val(Me.cmbCompany.SelectedValue) & "', voucher_date='" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", Remarks='" & Me.txtRemarks.Text.Replace("'", "''") & "', Posted_UserName = " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & " " _
                                    & "   where voucher_id=" & lngVoucherMasterId
            'End Task:M101
            objCommand.ExecuteNonQuery()


            If arrFile.Count > 0 Then
                SaveDocument(txtReceivingID.Text, Me.Name, trans)
            End If


            '***********************
            'Deleting Detail
            '***********************
            objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
            objCommand.ExecuteNonQuery()


            StockList = New List(Of StockDetail)
            For i = 0 To grd.RowCount - 1

                If blnCheckCurrentStockByItem = True Then
                    CheckCurrentStockByItem(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value, Val(Me.grd.GetRows(i).Cells(GrdEnum.TotalQty).Value), Me.grd, Me.txtPONo.Text, trans)
                End If
                Dim strdata As String
                Dim dblPurchasePrice As Double = 0D
                Dim dblCostPrice As Double = 0D
                Dim dblCurrencyAmount As Double = 0D
                Dim StockMasterId As Integer
                If flgAvgRate = True Then

                    Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
                    strdata = "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0) as BalanceQty, SUM((IsNull(InAmount,0))-(IsNull(OutAmount,0))) as BalanceAmount, IsNull(SUM(IsNull(InQty, 0) - IsNull(OutQty, 0)), 0) AS CheckStock From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefID=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)) & "  AND StockMasterTable.DocDate < '" & Me.dtpPODate.Value & "' Group By ArticleDefId "

                    'Dim dblCostPrice As Double = 0D
                    Dim dtLastestPriceData As New DataTable
                    dtLastestPriceData = GetDataTable(strData, trans)
                    dtLastestPriceData.AcceptChanges()

                    If dtLastestPriceData.Rows.Count > 0 Then
                        If Val(dtLastestPriceData.Rows(0).Item(1).ToString) > 0 Then
                            dblCostPrice = Val(Val(dtLastestPriceData.Rows(0).Item(2).ToString) / Val(dtLastestPriceData.Rows(0).Item(1).ToString))
                        End If
                    End If

                Else

                    Dim strPriceData() As String = GetRateByItem(IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString))).Split(",")

                    If strPriceData.Length > 1 Then
                        dblCostPrice = Val(strPriceData(0).ToString)
                        dblPurchasePrice = Val(strPriceData(1).ToString)
                        dblCurrencyAmount = Val(strPriceData(2).ToString)
                        If dblCostPrice = 0 Then
                            dblCostPrice = dblPurchasePrice
                        End If
                    End If
                End If
                'Dim strPriceData() As String = GetRateByItem(Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)).Split(",")

                'If strPriceData.Length > 1 Then
                '    dblCostPrice = Val(strPriceData(0).ToString)
                '    dblPurchasePrice = Val(strPriceData(1).ToString)
                '    dblCurrencyAmount = Val(strPriceData(2).ToString)
                '    If dblCostPrice = 0 Then
                '        dblCostPrice = dblPurchasePrice
                '    End If
                'End If



                If flgAvgRate = True And getConfigValueByType("CostImplementationLotWiseOnStockMovement") = "True" Then 'Set Status Avg Rate Task:2517
                    If Convert.ToDouble(GetItemRateByBatch(IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)), IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)))) > 0 Then
                        CostPrice = Convert.ToDouble(GetItemRateByBatch(IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)), IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString))))
                    Else
                        'CostPrice = CostPrice ''Commented Against TFS3528 : Ayesha Rehman
                        CostPrice = dblCostPrice
                    End If
                ElseIf flgAvgRate = True Then
                    'Tsk:2517 Set Cost Price Of Purchase Price
                    'If dblCostPrice > 0 Then
                    CostPrice = dblCostPrice 'Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString)
                    'currencyamount = dblCurrencyAmount
                    'Else
                    '    CostPrice = dblPurchasePrice
                    '    'currencyamount = dblCurrencyAmount
                    'End If
                Else
                    'CostPrice = IIf(Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString) <= 0, Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString), Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString))
                    CostPrice = dblPurchasePrice 'Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString)
                    'CurrencyAmount = dblCurrencyAmount
                    'End Task:2517
                End If
                '********************************
                ' Update Purchase Price By Imran
                '********************************
                Dim CrrStock As Double = 0D
                Dim PurchasePriceNew As Double = 0D
                If GLAccountArticleDepartment = True Then
                    AccountId = Val(grd.GetRows(i).Cells("PurchaseAccountId").Value.ToString)
                ElseIf flgPurchaseAccountFrontEnd = True Then
                    AccountId = Me.cmbPurchaseAc.Value
                Else
                    AccountId = Val(getConfigValueByType("PurchaseDebitAccount").ToString)
                End If
                Dim str As String = "Select ServiceItem from ArticleDefView where ArticleId = " & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), grd.GetRows(i).Cells("ArticleDefId").Value) & ""
                Dim dt1 As DataTable = GetDataTable(str, trans)
                dt1.AcceptChanges()
                Dim ServiceItem As Boolean = Val(dt1.Rows(0).Item("ServiceItem").ToString)
                If ServiceItem = True Then
                    Dim str1 As String = "Select CGSAccountId from ArticleDefView where ArticleId = " & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), grd.GetRows(i).Cells("ArticleDefId").Value) & ""
                    Dim dt2 As DataTable = GetDataTable(str1, trans)
                    dt2.AcceptChanges()
                    AccountId = Val(dt2.Rows(0).Item("CGSAccountId").ToString)
                    'AccountId = "Select ServiceItem from ArticleDefView where ArticleId = " & grd.GetRows(i).Cells("ItemId").Value & ""
                End If
                If flgAvgRate = True Then
                    Try

                        '    objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))) as Amount  " _
                        '                           & " FROM dbo.ArticleDefTable INNER JOIN " _
                        '                           & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & grd.GetRows(i).Cells("ArticleDefId").Value & " AND StockDetailTable.StockTransId IN(Select StockTransId From StockMasterTable WHERE DocNo <> '" & Me.txtPONo.Text.Replace("'", "''") & "')  " _
                        '                           & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                        '    Dim dtCrrStock As New DataTable
                        '    Dim daCrrStock As New OleDbDataAdapter(objCommand)
                        '    daCrrStock.Fill(dtCrrStock)
                        '    If dtCrrStock IsNot Nothing Then
                        '        If dtCrrStock.Rows.Count > 0 Then
                        '            'Before against task:2401
                        '            'If Val(grd.GetRows(i).Cells("Price").Value) <> 0 Then
                        '            If Val(grd.GetRows(i).Cells("Price").Value) <> 0 AndAlso Val(dtCrrStock.Rows(0).Item("qty").ToString) <> 0 Then
                        '                'End Task:2401
                        '                'CrrStock = dtCrrStock.Rows(0).Item(2)
                        '                'PurchasePriceNew = ((dtCrrStock.Rows(0).Item(3))) / CrrStock
                        '                PurchasePriceNew = Val(grd.GetRows(i).Cells("Price").Value) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)
                        '            Else
                        '                PurchasePriceNew = Val(grd.GetRows(i).Cells("Price").Value) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)
                        '            End If
                        '        Else
                        '            PurchasePriceNew = Val(grd.GetRows(i).Cells("Price").Value) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)
                        '        End If
                        '    End If

                    Catch ex As Exception

                    End Try
                End If


                StockDetail = New StockDetail
                StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                StockDetail.LocationId = grd.GetRows(i).Cells("LocationID").Value
                StockDetail.ArticleDefId = IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), grd.GetRows(i).Cells("ArticleDefId").Value)
                StockDetail.InQty = 0
                ''TASK-408
                StockDetail.OutQty = Val(Me.grd.GetRow(i).Cells("TotalQty").Value.ToString)
                StockDetail.Rate = CostPrice
                StockDetail.InAmount = 0
                StockDetail.OutAmount = Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * StockDetail.Rate
                StockDetail.Remarks = grd.GetRows(i).Cells("Comments").Value.ToString
                'StockDetail.CostPrice = IIf(CostPrice = 0, Val(Me.grd.GetRow(i).Cells("Price").Value.ToString), CostPrice)
                '' Start TASK-470 on 01-07-2016
                StockDetail.PackQty = Val(grd.GetRows(i).Cells("PackQty").Value.ToString)
                StockDetail.Out_PackQty = Val(grd.GetRows(i).Cells("Qty").Value.ToString)
                StockDetail.In_PackQty = 0
                ''End TASK-470
                StockList.Add(StockDetail)

                objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into PurchaseReturnDetailTable (PurchaseReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice,BatchNo,BatchID,LocationID, Tax_Percent, PackPrice, Transportation_Charges, Pack_Desc) Values ( " _
                '                        & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " _
                '                        & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",'" & grd.GetRows(i).Cells("BatchNo").Value & _
                '                        "' , " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", '" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "')"

                'R-916 Added Column Comments
                'objCommand.CommandText = "Insert into PurchaseReturnDetailTable (PurchaseReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice,BatchNo,BatchID,LocationID, Tax_Percent, PackPrice, Transportation_Charges, Pack_Desc, Comments) Values ( " _
                '                       & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                '                       & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " _
                '                       & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",'" & grd.GetRows(i).Cells("BatchNo").Value & _
                '                       "' , " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", '" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', '" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "')"


                'TaskM106151 Added Column RefPurchaseDetailId
                'objCommand.CommandText = "Insert into PurchaseReturnDetailTable (PurchaseReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice,BatchNo,BatchID,LocationID, Tax_Percent, PackPrice, Transportation_Charges, Pack_Desc, Comments,RefPurchaseDetailId) Values ( " _
                '                       & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                '                       & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " _
                '                       & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",'" & grd.GetRows(i).Cells("BatchNo").Value & _
                '                       "' , " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", '" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', '" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("RefPurchaseDetailId").Value.ToString) & ")Select @@Identity"

                'TASK-TFS-51 Added Fields AdTax_Percent And AdTax_Amount
                objCommand.CommandText = "Insert into PurchaseReturnDetailTable (PurchaseReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice,BatchNo,BatchID,LocationID, Tax_Percent, PackPrice, Transportation_Charges, Pack_Desc, Comments,RefPurchaseDetailId,AdTax_Percent,AdTax_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, AlternativeItem, AlternativeItemId) Values ( " _
                                      & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                                      & " " & Val(grd.GetRows(i).Cells("TotalQty").Value) & ", " _
                                      & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",'" & grd.GetRows(i).Cells("BatchNo").Value & _
                                      "' , " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", '" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', '" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("RefPurchaseDetailId").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", '" & Me.grd.GetRows(i).Cells("AlternativeItem").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) & ")Select @@Identity"
                'END TASK-TFS-51

                Dim intPurchaseDetailId As Integer = objCommand.ExecuteScalar()


                'Val(grd.Rows(i).Cells(5).Value)

                '***********************
                'Inserting Debit Amount
                '***********************
                'Before against task:2376
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", 0, '" & grd.GetRows(i).Cells("item").Value & "(" & Val(grd.GetRows(i).Cells("Qty").Value) & ")', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ")"
                'Before against task:23391
                'Task:2376 Change Comments Layouts
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", 0, '" & SetComments(Me.grd.GetRows(i)).Replace("" & Me.cmbVendor.Text & ",", "").Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ")"
                'End Task:2376
                '
                'Task:2391 Added Column ArticleDefId
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId,ArticleDefId, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value) * Val(grd.GetRows(i).Cells("Price").Value) * Val(txtCurrencyRate.Text) & ", 0, '" & SetComments(Me.grd.GetRows(i)).Replace("" & Me.cmbVendor.Text & ",", "").Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ", " & Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value) * Val(grd.GetRows(i).Cells("Price").Value) & ", 0, " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ")"
                'End Task:2391

                objCommand.ExecuteNonQuery()

                '***********************
                'Inserting Credit Amount
                '***********************
                'Before against task:2376
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", '" & grd.GetRows(i).Cells("item").Value & "(" & Val(grd.GetRows(i).Cells("Qty").Value) & ")', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ")"
                'Before against task:2391
                'Task:2376 Change Comments Layouts
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", '" & SetComments(Me.grd.GetRows(i)).Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ")"
                'End Task:2376
                'Task:2391 Added Column ArticleDefId
                
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId, ArticleDefId, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                                                        & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("TotalQty").Value) * CostPrice & ", '" & SetComments(Me.grd.GetRows(i)).Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ", " & Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ", 0, " & (Val(grd.GetRows(i).Cells("TotalQty").Value) * CostPrice) / Val(txtCurrencyRate.Text) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ")"
                'End Task:2391
                objCommand.ExecuteNonQuery()


                If flgAvgRate = True Then
                    If CostPrice - (Val(grd.GetRows(i).Cells("Price").Value.ToString) * Val(txtCurrencyRate.Text)) <> 0 Then
                        Dim str1 As String = "Select CGSAccountId from ArticleDefView where ArticleId = " & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), grd.GetRows(i).Cells("ArticleDefId").Value) & ""
                        Dim dt2 As DataTable = GetDataTable(str1, trans)
                        dt2.AcceptChanges()

                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId,ArticleDefId, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(dt2.Rows(0).Item("CGSAccountId").ToString) & ", " & (CostPrice - (Val(grd.GetRows(i).Cells("Price").Value.ToString) * Val(txtCurrencyRate.Text))) * Val(grd.GetRows(i).Cells("TotalQty").Value) & ", 0, '" & SetComments(Me.grd.GetRows(i)).Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ", " & Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ", " & (Val(grd.GetRows(i).Cells("TotalQty").Value) * (CostPrice / Val(txtCurrencyRate.Text)) - Val(grd.GetRows(i).Cells("Price").Value.ToString)) & ", 0, " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ")"
                        'End Task:2391
                        objCommand.ExecuteNonQuery()
                    End If

                End If
               
                '''''''''''''''''''''''''''' Price Update ''''''''''''''''''''''''''''''''''
                'If flgAvgRate = True Then

                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "UPDATE ArticleDefTableMaster Set PurchasePrice=" & (PurchasePriceNew - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)) & " WHERE ArticleId in (Select MasterId From ArticleDefTable WHERE ArticleId=" & grd.GetRows(i).Cells("ArticleDefId").Value & ")"
                '    objCommand.ExecuteNonQuery()


                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "UPDATE ArticleDefTable Set PurchasePrice=" & (PurchasePriceNew - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)) & " WHERE ArticleId=" & grd.GetRows(i).Cells("ArticleDefId").Value & ""
                '    objCommand.ExecuteNonQuery()

                '    objCommand.CommandText = ""
                '    objCommand.CommandText = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, '" & Me.dtpPODate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102))  AND a.ArticleDefId=" & grd.GetRows(i).Cells("ArticleDefId").Value & ""
                '    Dim dtRate As New DataTable
                '    Dim daRate As New OleDbDataAdapter(objCommand)
                '    daRate.Fill(dtRate)

                '    objCommand.CommandText = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " "
                '    Dim dtSalesItem As New DataTable
                '    Dim daSalesItem As New OleDbDataAdapter(objCommand)
                '    daSalesItem.Fill(dtSalesItem)

                '    If dtSalesItem.Rows.Count > 0 Then
                '        If Not dtRate Is Nothing Then
                '            If dtRate.Rows.Count > 0 Then
                '                objCommand.CommandText = "Update IncrementReductionTable Set PurchaseNewPrice=" & (PurchasePriceNew - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)) & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, (PurchasePriceNew - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString))) & "  WHERE ArticleDefId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, '" & Me.dtpPODate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102)) "
                '                objCommand.ExecuteNonQuery()
                '            Else
                '                objCommand.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice) " _
                '                & " Values('" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ",  " & Val(0) & ", " & Val(0) & ", " & (PurchasePriceNew - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString)) & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, (PurchasePriceNew - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString))) & ")"
                '                objCommand.ExecuteNonQuery()
                '            End If
                '        End If
                '    End If
                'Else
                '    'Apply Current Rate 
                '    objCommand.CommandText = "UPDATE ArticleDefTable Set PurchasePrice=" & Val(grd.GetRows(i).Cells("Price").Value.ToString) & " WHERE ArticleId=" & grd.GetRows(i).Cells("ArticleDefId").Value & ""
                '    objCommand.ExecuteNonQuery()

                '    objCommand.CommandText = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, '" & Me.dtpPODate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102))  AND a.ArticleDefId=" & grd.GetRows(i).Cells("ArticleDefId").Value & ""
                '    Dim dtRate As New DataTable
                '    Dim daRate As New OleDbDataAdapter(objCommand)
                '    daRate.Fill(dtRate)

                '    objCommand.CommandText = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " "
                '    Dim dtSalesItem As New DataTable
                '    Dim daSalesItem As New OleDbDataAdapter(objCommand)
                '    daSalesItem.Fill(dtSalesItem)

                '    If dtSalesItem.Rows.Count > 0 > 0 Then
                '        If Not dtRate Is Nothing Then
                '            If dtRate.Rows.Count > 0 Then
                '                objCommand.CommandText = "Update IncrementReductionTable Set PurchaseNewPrice=" & Val(grd.GetRows(i).Cells("Price").Value.ToString) & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(grd.GetRows(i).Cells("Price").Value.ToString)) & "  WHERE ArticleDefId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, '" & Me.dtpPODate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102)) "
                '                objCommand.ExecuteNonQuery()
                '            Else
                '                objCommand.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice) " _
                '                & " Values('" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ",  " & Val(0) & ", " & Val(0) & ", " & Val(grd.GetRows(i).Cells("Price").Value.ToString) & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(grd.GetRows(i).Cells("Price").Value.ToString)) & ")"
                '                objCommand.ExecuteNonQuery()
                '            End If
                '        End If
                '    End If
                'End If




                If Val(grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) > 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction,CostCenterId, ArticleDefId) " _
                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", 0, 'Ref:Additional Tax Against " & Me.cmbVendor.Text.Replace("'", "''") & ", " & Me.txtPONo.Text.Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ", " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ")"
                    objCommand.ExecuteNonQuery()



                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId,ArticleDefId) " _
                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & intAdditionalTaxAcId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ", 'Ref:Additional Tax Againt " & Me.txtPONo.Text.Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & Me.cmbCostCenter.SelectedValue & ", " & Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ")"
                    objCommand.ExecuteNonQuery()
                End If


            Next

            If (Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) > 0 Then
                'Insert Debit Amount
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, CurrencyId, CurrencyRate, Currency_Debit_Amount,Currency_Credit_Amount) " _
                                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 0, 'Ref Purchase Return Tax:" & Me.txtPONo.Text & "', " & Me.cmbCostCenter.SelectedValue & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("CurrencyTaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 0)"
                objCommand.ExecuteNonQuery()

                'Insert Credit Amount
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, CurrencyId, CurrencyRate, Currency_Debit_Amount,Currency_Credit_Amount) " _
                                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & PurchaseTaxAccountId & ", 0, " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 'Ref Purchase Return Tax:" & Me.txtPONo.Text & "', " & Me.cmbCostCenter.SelectedValue & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ", 0, " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("CurrencyTaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ")"
                objCommand.ExecuteNonQuery()

            End If



            Call AdjustmentPurchaseReturn(Val(Me.txtReceivingID.Text), trans)

            'TASKM106151 Update Purchase Return Qty In Purchase Detail Table
            If Not Me.cmbPo.SelectedIndex = -1 And Me.cmbPo.SelectedIndex >= 0 Then
                SalesInvoicePartialReturn(Val(GetPurchaseReturnId), Val(Me.cmbPo.SelectedValue), objCommand, trans)
            End If
            'END TASKM106151
            If IsValidate() = True Then
                StockMaster.StockTransId = StockTransId(Me.txtPONo.Text, trans)
                Call New StockDAL().Update(StockMaster, trans)
            End If
            trans.Commit()
            Update_Record = True
            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Purchase, Me.txtPONo.Text.Trim, True)
            SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, strVoucherNo, True)

            ''Start TFS2989
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim, Me.Name) Then
                If ValidateApprovalProcessIsInProgressAgain(Me.txtPONo.Text.Trim, Me.Name) = False Then
                    SaveApprovalLog(EnumReferenceType.PurchaseReturn, GetPurchaseReturnId, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Purchase Return," & cmbVendor.Text & "", Me.Name, 6)
                End If
            End If
            ''End TFS2989

            'Call Update1() 'Upgrading Stock ...
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            TaxAmount = Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            SendSMS()
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try
    End Function
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
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
                ShowErrorMessage("Can not change! financial year is closed")
                Me.dtpPODate.Focus()
                Exit Sub
            End If
            If FormValidate() Then
                Me.grd.UpdateData()
                'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
                If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()

                    If Save() Then
                        DisplayRecord()
                        SendAutoEmail()
                        '' Made changes to  against TASK TFS1462 on 13-09-2017
                        Dim Printing As Boolean
                        Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        Dim DirectVoucherPrinting As Boolean
                        DirectVoucherPrinting = Convert.ToBoolean(getConfigValueByType("DirectVoucherPrinting").ToString)
                        If Printing = True Or DirectVoucherPrinting = True Then
                            If msg_Confirm("Do you want to print", Printing, DirectVoucherPrinting) = True Then
                                Dim Print1 As Boolean = frmMessages.Print
                                Dim PrintVoucher As Boolean = frmMessages.DirectVoucherPrinting
                                If Print1 = True Then
                                    Me.PurchaseReturnToolStripMenuItem_Click(Nothing, Nothing)
                                End If
                                If PrintVoucher = True Then
                                    GetVoucherPrint(Me.txtPONo.Text, Me.Name, BaseCurrencyName, BaseCurrencyId, True)
                                End If
                            End If
                        End If
                        ''END TASK TFS1462




                        flgSelectProject = True
                        'msg_Information(str_informSave)
                        RefreshControls()
                        ClearDetailControls()
                        'grd.Rows.Clear()
                        'DisplayRecord() R933 Commented History Data
                        '' Below lines of code are commented against TASK TFS1462
                        'Dim Printing As Boolean
                        'Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        'If Printing = True Then
                        '    If msg_Confirm("Do you want to print") = True Then
                        '        Me.PurchaseReturnToolStripMenuItem_Click(Nothing, Nothing)
                        '    End If
                        'End If



                        If Me.BackgroundWorker2.IsBusy Then Exit Sub
                        Me.BackgroundWorker2.RunWorkerAsync()
                        'Do While Me.BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop


                        If Me.BackgroundWorker1.IsBusy Then Exit Sub
                        Me.BackgroundWorker1.RunWorkerAsync()
                        'Do While Me.BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop

                    Else
                        Exit Sub 'MsgBox("Record has not been added")

                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()

                    If Update_Record() Then
                        'EmailSave()

                        'DisplayRecord()

                        '' Made changes to  against TASK TFS1462 on 13-09-2017
                        Dim Printing As Boolean
                        Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        Dim DirectVoucherPrinting As Boolean
                        DirectVoucherPrinting = Convert.ToBoolean(getConfigValueByType("DirectVoucherPrinting").ToString)
                        If Printing = True Or DirectVoucherPrinting = True Then
                            If msg_Confirm("Do you want to print", Printing, DirectVoucherPrinting) = True Then
                                Dim Print1 As Boolean = frmMessages.Print
                                Dim PrintVoucher As Boolean = frmMessages.DirectVoucherPrinting
                                If Print1 = True Then
                                    Me.PurchaseReturnToolStripMenuItem_Click(Nothing, Nothing)
                                End If
                                If PrintVoucher = True Then
                                    GetVoucherPrint(Me.txtPONo.Text, Me.Name, BaseCurrencyName, BaseCurrencyId, True)
                                End If
                            End If
                        End If
                        ''END TASK TFS1462


                        flgSelectProject = True
                        'msg_Information(str_informUpdate)
                        RefreshControls()
                        ClearDetailControls()
                        'grd.Rows.Clear()
                        'DisplayRecord() R933 History Data

                        ''Below lines of code are commented against TASK TFS1462
                        'Dim Printing As Boolean
                        'Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        'If Printing = True Then
                        '    If msg_Confirm("Do you want to print") = True Then
                        '        Me.PurchaseReturnToolStripMenuItem_Click(Nothing, Nothing)
                        '    End If
                        'End If



                        If Me.BackgroundWorker2.IsBusy Then Exit Sub
                        Me.BackgroundWorker2.RunWorkerAsync()
                        'Do While Me.BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop


                        If Me.BackgroundWorker1.IsBusy Then Exit Sub
                        Me.BackgroundWorker1.RunWorkerAsync()
                        'Do While Me.BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop

                    Else
                        Exit Sub 'MsgBox("Record has not been updated")
                    End If
                End If
            End If
            setVoucherNo = String.Empty
            Total_Amount = 0

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate("Purchase Return")
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
                ''UsersEmail.Add("Bilal@siriussolution.com")
                FormatStringBuilder(dtEmail)
                'CreateOutLookMail()
                For Each _email As String In UsersEmail
                    CreateOutLookMail(_email)
                    SaveEmailLog(PurchaseReturnNo, _email, "frmPurchaseReturn", Activity)
                Next
                'SaveCCBCC(CC, BCC)
                'CC = ""
                'BCC = ""
            Else
                ShowErrorMessage("No email template is found for Purchase Return.")
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
            str = "select TOP 1 PurchaseReturnId, PurchaseReturnNo, PurchaseReturnDate from purchasereturnmastertable order by 1 desc"
            Dim dt1 As DataTable = GetDataTable(str)
            If dt1.Rows.Count > 0 Then
                PurchaseReturnId = dt1.Rows(0).Item("PurchaseReturnId")
                PurchaseReturnNo = dt1.Rows(0).Item("PurchaseReturnNo")
            End If
            Dim str1 As String
            str1 = "SELECT Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Convert(Decimal(18, " & DecimalPointInQty & "), Recv_D.Sz1, 1) AS Qty, Recv_D.CurrentPrice, Case When IsNull(Recv_D.CurrentPrice,0) > (IsNull(Price,0)) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent, Recv_D.Price, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount,  " _
          & " ((IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0)) * Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
          & " Recv_D.Sz7 as PackQty,Isnull(Recv_D.PackPrice,0) as PackPrice, " _
          & " IsNull(Recv_D.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount,  Convert(float,0) as CurrencyTaxAmount, IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent,IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount,  Convert(float,0) as CurrencyAdTaxAmount, Convert(float,0) as TotalAmount, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, IsNull(Recv_D.Cost_Price,0) as Cost_Price, Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Recv_D.Qty, 0), 1) As TotalQty  FROM dbo.PurchaseReturnDetailTable Recv_D INNER JOIN " _
          & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
          & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
          & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
          & " Where Recv_D.PurchaseReturnID =" & PurchaseReturnId & ""
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
            mailItem.Subject = "Creating New PR: " + PurchaseReturnNo
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

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        'Dim s As String
        's = "1234-567-890"
        'MsgBox(Microsoft.VisualBasic.Right(s, InStr(1, s, "-") - 2))
        Try


            Me.Cursor = Cursors.WaitCursor
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            flgSelectProject = False

            RefreshControls()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            EditRecord()
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSaved_CellDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        ''Task#A1 12-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
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
        ''End Task#A1 12-06-2015

    End Sub
    Private Sub cmbPo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPo.SelectedIndexChanged
        If IsFormOpend = True Then


            If Me.BtnSave.Text <> "&Save" Then
                If Val(Me.grdSaved.GetRow.Cells("PurchaseOrderID").Value.ToString) > 0 Then
                    If Me.cmbPo.SelectedIndex > 0 Then
                        If Me.cmbPo.SelectedValue <> (Me.grdSaved.GetRow.Cells("PurchaseOrderID").Value.ToString) Then
                            If msg_Confirm("You have changed Purchase [" & CType(Me.cmbPo.SelectedItem, DataRowView).Row.Item("ReceivingNo").ToString & "], do you want to proceed. ?") = False Then
                                RemoveHandler cmbPo.SelectedIndexChanged, AddressOf Me.cmbPo_SelectedIndexChanged
                                Me.cmbPo.SelectedValue = (Me.grdSaved.GetRow.Cells("PurchaseOrderID").Value.ToString)
                                AddHandler cmbPo.SelectedIndexChanged, AddressOf Me.cmbPo_SelectedIndexChanged
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

            If Me.cmbPo.SelectedIndex > 0 Then
                'If Me.grdSaved.RowCount > 0 Then
                '    If Val(Me.grdSaved.GetRow.Cells("PurchaseOrderID").Value.ToString) <> Me.cmbPo.SelectedValue Then
                '        If Me.BtnSave.Text = "&Update" Then
                '            If msg_Confirm("Do you want to load, Purchase Invoice.") = False Then Exit Sub
                '        End If
                '    Else
                '        Exit Sub
                '    End If
                'End If
                Me.DisplayPODetail(Me.cmbPo.SelectedValue)
                'Dim adp As New OleDbDataAdapter
                'Dim dt As New DataTable
                'Dim Sql As String = "SELECT     dbo.ReceivingMasterTable.VendorId, dbo.vwCOADetail.detail_title FROM         dbo.ReceivingMasterTable INNER JOIN                       dbo.vwCOADetail ON dbo.ReceivingMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id where ReceivingMasterTable.ReceivingId=" & Me.cmbPo.SelectedValue
                'adp = New OleDbDataAdapter(Sql, Con)
                'adp.Fill(dt)

                'If Not dt.Rows.Count > 0 Then
                'Me.cmbVendor.Enabled = True : Me.cmbVendor.Rows(0).Activate()
                'Else
                Me.cmbVendor.Value = Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("VendorId").ToString) 'dt.Rows(0).Item("VendorId").ToString
                'Me.cmbVendor.Enabled = False 'Comment against task:2711
                'End If

            Else
                Me.cmbVendor.Enabled = True
                'Me.cmbVendor.Rows(0).Activate()
            End If
        End If
        'Me.GetTotal()
    End Sub

    Private Sub cmbItem_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbItem.KeyDown

        Try
            ''TFS1858 : Ayesha Rehman :Item dropdown shall be searchable
            If e.KeyCode = Keys.F1 Then
                If flgLocationWiseItems = True Then
                    frmItemSearch.LocationId = Me.cmbCategory.SelectedValue
                Else
                    frmItemSearch.LocationId = 0
                End If

                If Me.rbtVendor.Checked = True Then
                    If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
                    frmItemSearch.VendorId = Me.cmbVendor.Value
                End If

                frmItemSearch.BringToFront()
                frmItemSearch.ShowDialog()
                If frmItemSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbItem.Value = frmItemSearch.ArticleId
                    txtQty.Text = frmItemSearch.Qty
                    txtPackQty.Text = frmItemSearch.PackQty
                    cmbItem_Leave(Nothing, Nothing)
                    txtRate_LostFocus(Nothing, Nothing)
                    AddItemToGrid()
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
        Try

            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            End If
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            ClearDetailControls()
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            Me.txtCurrentRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString ''TFS3037
            'Me.txtStock.Text = Me.cmbItem.ActiveRow.Cells("Stock").Value.ToString
            'Me.txtBatchNo.Text = Me.cmbItem.ActiveRow.Cells("BatchNo").Value.ToString
            If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1

            'Me.cmbVendor.DisplayLayout.Grid.Show( me.cmbVendor.contr)
            Dim strSQL As String = String.Empty
            'If GetConfigValue("WithSizeRange") = "False" Then
            '    strSQl = "SELECT Stock, BatchNo as [Batch No] FROM  dbo.vw_Batch_Stock WHERE     (NOT (Stock = 0))and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            '    Me.cmbBatchNo.LimitToList = False
            If getConfigValueByType("WithSizeRange") = "True" Then
                strSQL = "SELECT     ISNULL(a.Stock, 0) AS Stock, PurchaseBatchTable.BatchNo as [Batch No], PurchaseBatchTable.BatchID" _
                        & " FROM         SizeRangeTable INNER JOIN" _
                        & "                   PurchaseBatchTable ON SizeRangeTable.BatchID = PurchaseBatchTable.BatchID LEFT OUTER JOIN " _
                        & "                    (SELECT     * " _
                        & "   FROM vw_Batch_Stock " _
                        & "                WHERE      articleID = " & Me.cmbItem.Value & ") a ON PurchaseBatchTable.BatchID = a.BatchID " _
                        & " WHERE(SizeRangeTable.SizeID = " & IIf(Val(Me.cmbItem.ActiveRow.Cells("Size ID").Value.ToString) > 0, Me.cmbItem.ActiveRow.Cells("Size ID").Value, 0) & ") "
            Else
                strSQL = "SELECT Stock, BatchNo as [Batch No],BatchID  FROM         dbo.vw_Batch_Stock_ByLocation WHERE     (NOT (Stock = 0))and LocationId = " & Me.cmbCategory.SelectedValue & " and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
                'strSQL = "SELECT Stock, BatchNo as [Batch No], BatchID FROM  dbo.vw_Batch_Stock WHERE     (NOT (Stock = 0))and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            End If
            Dim dt As DataTable = GetDataTable(strSQL)
            cmbBatchNo.DataSource = dt
            cmbBatchNo.ValueMember = "BatchID"
            cmbBatchNo.DisplayMember = "Batch No"
            cmbBatchNo.DisplayLayout.Bands(0).Columns("BatchNo").Hidden = True
            cmbBatchNo.DisplayLayout.Bands(0).Columns("BatchID").Hidden = True

            Dim dr As DataRow = dt.NewRow
            dr.Item(1) = "xxxx"
            dr.Item(2) = 0
            dr.Item(0) = 0
            dt.Rows.Add(dr)
            If Me.cmbBatchNo.Rows.Count > 0 Then
                Me.cmbBatchNo.Rows(0).Activate()
            End If
            Con.Close()
            Me.cmbBatchNo.Enabled = True
        Catch ex As Exception
            Throw ex
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
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        If IsDateLock(Me.dtpPODate.Value) = True Then
            ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
        End If
        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        ''Start TFS2988 : Ayesha Rehman : 09-04-2018
        If blEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document is in Approval Process ") : Exit Sub
                End If
            End If
        End If
        ''End TFS2988
        Dim flgAvgRate As Boolean = Convert.ToBoolean(getConfigValueByType("AvgRate").ToString)
        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

        Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, grdSaved.CurrentRow.Cells(0).Value.ToString)
        Dim strVoucherNo As String = String.Empty
        Dim dt As DataTable = GetRecords("SELECT voucher_no   FROM tblVoucher  WHERE voucher_id = " & lngVoucherMasterId & " ")
        dt.AcceptChanges()
        If Not dt Is Nothing Then
            If Not dt.Rows.Count = 0 Then
                strVoucherNo = dt.Rows(0)("voucher_no")
            End If
        End If

        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim cmd As New OleDbCommand
        cmd.Connection = Con
        cmd.CommandType = CommandType.Text

        cmd.CommandText = String.Empty
        cmd.CommandText = "SELECT Isnull(Qty,0) as Qty, ArticleDefID, Isnull(Price,0) as Rate FROM PurchaseReturnDetailTable WHERE  PurchaseReturnId = '" & Val(Me.grdSaved.GetRow.Cells("PurchaseReturnId").Value.ToString) & "'"
        Dim da As New OleDbDataAdapter(cmd)
        Dim dtSavedItems As New DataTable
        da.Fill(dtSavedItems)
        dtSavedItems.AcceptChanges()
        Me.Cursor = Cursors.WaitCursor
        Try

            'Dim cm As New OleDbCommand
            Dim objTrans As OleDbTransaction
            If Con.State = ConnectionState.Closed Then Con.Open()
            objTrans = Con.BeginTransaction
            cmd.Connection = Con
            cmd.Transaction = objTrans
            '********************************
            ' Update Purchase Price By Imran
            '********************************
            'For i As Integer = 0 To dtSavedItems.Rows.Count - 1
            '    Dim CrrStock As Double = 0D
            '    Dim PurchasePriceNew As Double = 0D
            '    If flgAvgRate = True Then
            '        'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14 

            '        Me.lblProgress.Text = "Processing Please Wait ..."
            '        Me.lblProgress.Visible = True
            '        Application.DoEvents()

            '        Try
            '            cm.Connection = Con
            '            cm.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, dbo.ArticleDefTable.PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))) " _
            '                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
            '                                    & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId INNER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE dbo.ArticleDefTable.ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " AND DocNo <> '" & grdSaved.CurrentRow.Cells(0).Value.ToString.Replace("'", "''") & "' " _
            '                                    & " GROUP BY dbo.StockDetailTable.ArticleDefId, dbo.ArticleDefTable.PurchasePrice "

            '            cm.Transaction = objTrans
            '            Dim dtCrrStock As New DataTable
            '            Dim daCrrStock As New OleDbDataAdapter
            '            daCrrStock.SelectCommand = cm
            '            daCrrStock.Fill(dtCrrStock)
            '            If dtCrrStock IsNot Nothing Then
            '                If dtCrrStock.Rows.Count > 0 Then
            '                    If Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) <> 0 Then
            '                        CrrStock = Val(dtCrrStock.Rows(0).Item(2).ToString)
            '                        PurchasePriceNew = IIf(Val(dtCrrStock.Rows(0).Item(3).ToString) + CrrStock = 0, 0, dtCrrStock.Rows(0).Item(3).ToString / CrrStock)
            '                    End If
            '                End If
            '            End If

            '        Catch ex As Exception

            '        End Try


            '        cm.CommandText = ""
            '        cm.CommandText = "UPDATE ArticleDefTableMaster Set PurchasePrice=" & PurchasePriceNew & " WHERE ArticleId in (Select MasterId From ArticleDefTable WHERE ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ")"
            '        cm.ExecuteNonQuery()


            '        cm.CommandText = ""
            '        cm.CommandText = "UPDATE ArticleDefTable Set PurchasePrice=" & PurchasePriceNew & " WHERE ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ""
            '        cm.ExecuteNonQuery()

            '        cm.CommandText = ""
            '        cm.CommandText = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, '" & Me.dtpPODate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102))  AND a.ArticleDefId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ""
            '        Dim dtRate As New DataTable
            '        Dim daRate As New OleDbDataAdapter(cm)
            '        daRate.Fill(dtRate)

            '        cm.CommandText = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " "
            '        Dim dtSalesItem As New DataTable
            '        Dim daSalesItem As New OleDbDataAdapter(cm)
            '        daSalesItem.Fill(dtSalesItem)

            '        If dtSalesItem.Rows.Count > 0 Then
            '            If Not dtRate Is Nothing Then
            '                If dtRate.Rows.Count > 0 Then
            '                    cm.CommandText = "Update IncrementReductionTable Set PurchaseNewPrice=" & PurchasePriceNew & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, PurchasePriceNew) & "  WHERE ArticleDefId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, '" & Me.dtpPODate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102)) "
            '                    cm.ExecuteNonQuery()
            '                Else
            '                    cm.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice) " _
            '                    & " Values('" & Me.dtpPODate.Value.Date & "', " & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ",  " & Val(0) & ", " & Val(0) & ", " & PurchasePriceNew & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, PurchasePriceNew) & ")"
            '                    cm.ExecuteNonQuery()
            '                End If
            '            End If
            '        End If

            '    Else
            '        'Apply Current Rate 
            '        cm.Connection = Con
            '        cm.CommandText = "UPDATE ArticleDefTable Set PurchasePrice=" & Val(dtSavedItems.Rows(i).Item("Rate").ToString) & " WHERE ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ""
            '        cm.Transaction = objTrans
            '        cm.ExecuteNonQuery()

            '        cm.Connection = Con
            '        cm.CommandText = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, '" & Me.dtpPODate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102))  AND a.ArticleDefId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ""
            '        Dim dtRate As New DataTable
            '        Dim daRate As New OleDbDataAdapter(cm)
            '        daRate.Fill(dtRate)

            '        cm.Connection = Con
            '        cm.CommandText = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " "
            '        Dim dtSalesItem As New DataTable
            '        Dim daSalesItem As New OleDbDataAdapter(cm)
            '        daSalesItem.Fill(dtSalesItem)

            '        If dtSalesItem.Rows.Count > 0 Then
            '            If Not dtRate Is Nothing Then
            '                If dtRate.Rows.Count > 0 Then
            '                    cm.Connection = Con
            '                    cm.CommandText = "Update IncrementReductionTable Set PurchaseNewPrice=" & Val(dtSavedItems.Rows(i).Item("Rate").ToString) & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(dtSavedItems.Rows(i).Item("Rate").ToString)) & "  WHERE ArticleDefId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, '" & Me.dtpPODate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102)) "
            '                    cm.Transaction = objTrans
            '                    cm.ExecuteNonQuery()
            '                Else
            '                    cm.Connection = Con
            '                    cm.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice) " _
            '                    & " Values('" & Me.dtpPODate.Value.Date & "', " & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ",  " & Val(0) & ", " & Val(0) & ", " & Val(dtSavedItems.Rows(i).Item("Rate").ToString) & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(dtSavedItems.Rows(i).Item("Rate").ToString)) & ")"
            '                    cm.Transaction = objTrans
            '                    cm.ExecuteNonQuery()
            '                End If
            '            End If
            '        End If
            '    End If
            'Next

            'TASKM106151 Remove Purchase Return Qty In Purchase Detail Table
            If Val(Me.grdSaved.GetRow.Cells("PurchaseOrderId").Value.ToString) > 0 Then
                DeleteInvoicePartialReturn(Val(Me.grdSaved.CurrentRow.Cells("PurchaseReturnId").Value.ToString), Val(Me.grdSaved.GetRow.Cells("PurchaseOrderId").Value.ToString), cmd, objTrans)
            End If
            'End TaSKM106151



            cmd.CommandText = ""
            cmd.CommandText = "Delete From InvoiceAdjustmentTable WHERE VoucherDetailID=" & Val(Me.grdSaved.CurrentRow.Cells("PurchaseReturnId").Value.ToString) & " AND InvoiceType='Purchase Return'"
            cmd.ExecuteNonQuery() ''Number of rows effected by delete statement.


            'cmd.Connection = Con
            cmd.CommandText = String.Empty
            cmd.CommandText = "delete from PurchaseReturnDetailTable where PurchaseReturnid=" & Val(Me.grdSaved.CurrentRow.Cells("PurchaseReturnId").Value.ToString)
            'cmd.Transaction = objTrans
            cmd.ExecuteNonQuery()

            'cmd = New OleDbCommand
            'cmd.Connection = Con
            cmd.CommandText = String.Empty
            cmd.CommandText = "delete from PurchaseReturnMasterTable where PurchaseReturnid=" & Val(Me.grdSaved.CurrentRow.Cells("PurchaseReturnId").Value.ToString)
            'cmd.Transaction = objTrans
            cmd.ExecuteNonQuery()

            'cm = New OleDbCommand
            'cm.Connection = Con
            'cm.CommandText = "delete from SalesReturnMasterTable where SalesReturnid=" & Me.grdSaved.CurrentRow.Cells(6).Value.ToString
            'cm.Transaction = objTrans
            'cm.ExecuteNonQuery()

            'cmd = New OleDbCommand
            'cmd.Connection = Con
            cmd.CommandText = String.Empty
            cmd.CommandText = "delete from tblvoucherdetail where voucher_id=" & lngVoucherMasterId
            'cmd.Transaction = objTrans
            cmd.ExecuteNonQuery()

            'cmd = New OleDbCommand
            'cmd.Connection = Con
            cmd.CommandText = String.Empty
            cmd.CommandText = "delete from tblvoucher where voucher_id=" & lngVoucherMasterId
            'cmd.Transaction = objTrans
            cmd.ExecuteNonQuery()
            StockMaster = New StockMaster
            StockMaster.StockTransId = Convert.ToInt32(StockTransId(Me.grdSaved.CurrentRow.Cells(0).Value, objTrans).ToString)
            Call New StockDAL().Delete(StockMaster, objTrans)

            objTrans.Commit()

            'Call Delete() 'Upgrading Stock ...

            'msg_Information(str_informDelete)
            Me.txtReceivingID.Text = 0

            SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Purchase, Me.grdSaved.CurrentRow.Cells(0).Value, True)
            SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, strVoucherNo, True)


        Catch ex As Exception
            msg_Error("Error occured while deleting record: " & ex.Message)
        Finally
            Con.Close()
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
        Me.RefreshControls()
    End Sub
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        'ShowReport("PurchaseReturn", "{PurchaseReturnMasterTable.PurchaseReturnId}=" & grdSaved.CurrentRow.Cells(6).Value)
    End Sub
    'Private Sub grd_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
    '    Me.GetTotal()
    'End Sub
    Private Sub RdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdoCode.CheckedChanged
        If Me.IsFormOpend = True Then
            ItemFilterByName = False
            'Me.FillCombo("Item") commented against TASK TFS4544
            If Me.RdoCode.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            End If
        End If
    End Sub

    Private Sub rdoName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoName.CheckedChanged
        If Me.IsFormOpend = True Then
            ItemFilterByName = False
            '' Me.FillCombo("Item") commented against TASK TFS4544
            If Me.RdoCode.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            End If
        End If
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.btnSearchDelete.Enabled = True ''R934 Added Dlete Button
                Me.btnSearchPrint.Enabled = True  ''R934 Added Print Button
                Me.chkPost.Visible = True
                If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True
                'Task 1592 Apply future date rights
                IsDateChangeAllowed = True
                dtpPODate.MaxDate = Date.Today.AddMonths(3)
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.btnSearchDelete.Enabled = False
                    Me.BtnPrint.Enabled = False
                    Me.btnSearchPrint.Enabled = False
                    Me.btnSearchDelete.Enabled = False ''R934 Added Dlete Button
                    Me.btnSearchPrint.Enabled = False  ''R934 Added Print Button
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Me.PrintVoucherToolStripMenuItem.Visible = False
                    Me.PrintVoucherToolStripMenuItem1.Visible = False
                    'Task 1592 Apply future date rights
                    IsDateChangeAllowed = False
                    DateChange(False)
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmPurchaseReturn)
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
                        Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString ''R934 Added Dlete Button
                        Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString  ''R934 Added Print Button
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                UserPostingRights = GetUserPostingRights(LoginUserId)
                If UserPostingRights = True Then
                    Me.chkPost.Visible = True
                Else
                    Me.chkPost.Visible = False
                    If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = False
                End If
                UserPriceAllowedRights = GetUserPriceAllowedRights(LoginUserId)
                If UserPriceAllowedRights = True Then
                    Me.pnlRateHidden.Visible = True
                    Me.grd.RootTable.Columns("Price").Visible = True
                    Me.grd.RootTable.Columns("Total").Visible = True
                Else
                    Me.pnlRateHidden.Visible = False
                    Me.grd.RootTable.Columns("Price").Visible = False
                    Me.grd.RootTable.Columns("Total").Visible = False
                End If
                GetSecurityByPostingUser(UserPostingRights, BtnSave, BtnDelete)
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.btnSearchDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                Me.btnSearchPrint.Enabled = False
                Me.btnSearchPrint.Enabled = False
                Me.btnSearchEdit.Enabled = False
                Me.chkPost.Visible = False
                'Task 1592 Apply future date rights
                IsDateChangeAllowed = False
                DateChange(False)
                If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar2.mGridExport.Enabled = False ''TFS1823
                Me.pnlRateHidden.Visible = False
                Me.grd.RootTable.Columns("Price").Visible = False
                Me.grd.RootTable.Columns("Total").Visible = False
                Me.btnSearchDelete.Enabled = False  ''R934 Added Dlete Button
                Me.btnSearchPrint.Enabled = False   ''R934 Added Print Button
                CtrlGrdBar1.mGridChooseFielder.Enabled = False 'Task:2406 Added Field Chooser Rights
                Me.PrintVoucherToolStripMenuItem.Visible = False
                Me.PrintVoucherToolStripMenuItem1.Visible = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        Me.btnSearchEdit.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                        Me.btnSearchDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        Me.btnSearchPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                        'Task:1592 Added Future Date Rights
                    ElseIf RightsDt.FormControlName = "Future Transaction" Then
                        IsDateChangeAllowed = True
                        DateChange(True)
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        CtrlGrdBar2.mGridExport.Enabled = True  ''TFS1823
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True
                        If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True
                    ElseIf RightsDt.FormControlName = "Price Allow" Then
                        Me.pnlRateHidden.Visible = True
                        Me.grd.RootTable.Columns("Price").Visible = True
                        Me.grd.RootTable.Columns("Total").Visible = True
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    ElseIf RightsDt.FormControlName = "Print Voucher" Then
                        PrintVoucherToolStripMenuItem.Enabled = True
                        PrintVoucherToolStripMenuItem1.Enabled = True
                        Me.PrintVoucherToolStripMenuItem.Visible = True
                        Me.PrintVoucherToolStripMenuItem1.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
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
    Private Sub SummaryOfPurchaseReturnToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryOfPurchaseReturnToolStripMenuItem1.Click
        frmMain.LoadControl("rptPurchasereturn")
    End Sub
    'Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
    '    If Me.UltraTabControl1.SelectedTab.Index = 0 Then
    '        Me.BtnLoadAll.Visible = False
    '        Me.ToolStripButton1.Visible = False
    '        ToolStripSeparator1.Visible = False
    '    Else
    '        Me.BtnLoadAll.Visible = True
    '        Me.ToolStripButton1.Visible = True
    '        ToolStripSeparator1.Visible = True
    '    End If
    'End Sub
    Private Sub BtnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14 
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''END TFS4544
            'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
            Dim id As Integer = 0
            'Task:2376 Get Comments Configurations
            If Me.BackgroundWorker3.IsBusy Then Exit Sub
            Me.BackgroundWorker3.RunWorkerAsync()
            Do While Me.BackgroundWorker3.IsBusy
                Application.DoEvents()
            Loop
            'End Task:2376
            id = Me.cmbVendor.SelectedRow.Cells(0).Value
            FillCombo("Vendor")
            Me.cmbVendor.Value = id

            id = Me.cmbSalesMan.SelectedValue
            FillCombo("SM")
            Me.cmbSalesMan.SelectedValue = id

            id = Me.cmbPo.SelectedValue
            FillCombo("SO")
            Me.cmbPo.SelectedValue = id

            id = Me.cmbCategory.SelectedValue
            FillCombo("Category")
            Me.cmbCategory.SelectedValue = id

            id = Me.cmbItem.SelectedRow.Cells(0).Value
            FillCombo("Item")
            Me.cmbItem.Value = id

            id = Me.cmbCurrency.SelectedIndex
            FillCombo("Currency")
            Me.cmbCurrency.SelectedIndex = id


            FillCombo("grdLocation")

            FillCombo("SearchVendor")

            FillCombo("SearchLocation")


            FillCombo("CostCenter")

            id = Me.cmbCompany.SelectedValue
            FillCombo("Company")
            Me.cmbCompany.SelectedValue = id
            ' ''Task:2366 Added Location Wise Filter Configuration
            'If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
            '    flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
            'End If
            ''End Task:2366

            id = Me.cmbPurchaseAc.ActiveRow.Cells(0).Value
            FillCombo("PurchaseAccount")
            Me.cmbPurchaseAc.Value = id

            If Not getConfigValueByType("PurchaseAccountFrontEnd").ToString = "Error" Then
                flgPurchaseAccountFrontEnd = getConfigValueByType("PurchaseAccountFrontEnd")
            End If
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

    Private Sub chkPost_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPost.CheckedChanged

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
                            Me.cmbBatchNo.Rows(0).Activate()
                            AddItemToGrid()
                            Application.DoEvents()
                        End If
                    Next
                End If
            Next
            Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.CloseDropdown)
            'Me.GetTotal()
            Me.ClearDetailControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rbtAllItem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAllItem.CheckedChanged, rbtVendor.CheckedChanged
        Try
            AllItems()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub AllItems()
        Try
            If IsFormOpend = True Then
                'If Me.rbtAllItem.Checked = True Then
                FillCombo("Item")
                'Else
                '    If Me.RdoCode.Checked = True Then
                '        Dim Str As String = "SELECT ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleCode AS Code, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.PurchasePrice AS Price FROM  ArticleDefView INNER JOIN  ArticleDefVendors ON ArticleDefView.MasterID = ArticleDefVendors.ArticleDefId WHERE (ArticleDefView.Active = 1) AND (ArticleDefVendors.VendorId=" & Me.cmbVendor.Value & ")"
                '    Else

                '    End If
                '    'FillDropDown(cmbItem, str)
                '    FillUltraDropDown(Me.cmbItem, Str)
                '    Me.cmbItem.Rows(0).Activate()
                'End If
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
            ''Task No 2554 for CheckinG the Value Of Combo Box 
            If Me.cmbVendor.ActiveRow Is Nothing Then
                Exit Sub
            End If
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.rbtVendor.Checked = True Then AllItems()
            ''Taask No 2554 Append One Line Code for Fiiling the Invoice Based Combo Box 
            FillCombo("SO")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PurchaseReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseReturnToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("PurchaseReturnNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            If IsPreviewInvoice = True Then
                ShowReport("PurchaseReturn", IIf(Val(grdSaved.CurrentRow.Cells("PurchaseReturnID").Value.ToString) > 0, "{PurchaseReturnMasterTable.PurchaseReturnId}=" & Val(Me.txtReceivingID.Text), "{PurchaseReturnMasterTable.PurchaseReturnId}=" & Val(grdSaved.CurrentRow.Cells("PurchaseReturnID").Value.ToString)), , , True, , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            Else
                ShowReport("PurchaseReturn", IIf(Val(grdSaved.CurrentRow.Cells("PurchaseReturnID").Value.ToString) > 0, "{PurchaseReturnMasterTable.PurchaseReturnId}=" & Val(Me.txtReceivingID.Text), "{PurchaseReturnMasterTable.PurchaseReturnId}=" & Val(grdSaved.CurrentRow.Cells("PurchaseReturnID").Value.ToString)), , , False, , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub OutwardGatePassToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutwardGatePassToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = grdSaved.GetRow.Cells("PurchaseReturnNo").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'ShowReport("rptOutwardGatePass", IIf(Val(grdSaved.CurrentRow.Cells("PurchaseReturnID").Value.ToString) > 0, "{PurchaseReturnMasterTable.PurchaseReturnId}=" & Val(grdSaved.CurrentRow.Cells("PurchaseReturnID").Value.ToString), "{PurchaseReturnMasterTable.PurchaseReturnId}=" & Val(grdSaved.CurrentRow.Cells("PurchaseReturnID").Value.ToString)), , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            ''TFS4328 : Ayesha Rehman : 29-08-2018
            AddRptParam("@ReceivingId", Val(grdSaved.CurrentRow.Cells("ReceivingId").Value.ToString))
            ShowReport("rptOutwardGatePass", , , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub FillComboByEdit()
        Try
            FillCombo("Vendor")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            'R-916 Added Index Comments for edit 
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If col.Index <> GrdEnum.LocationId AndAlso col.Index <> GrdEnum.Qty AndAlso col.Index <> GrdEnum.TotalQty AndAlso col.Index <> GrdEnum.TotalQty AndAlso col.Index <> GrdEnum.Rate AndAlso col.Index <> GrdEnum.Tax_Percent AndAlso col.Index <> GrdEnum.Transportation_Charges AndAlso col.Index <> GrdEnum.Comments AndAlso col.Index <> GrdEnum.AdTax_Percent AndAlso col.Index <> GrdEnum.CurrentPrice AndAlso col.Index <> GrdEnum.RateDiscPercent AndAlso col.Index <> GrdEnum.CurrencyAmount Then
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
            Me.grd.RootTable.Columns(GrdEnum.Pack_Desc).Position = Me.grd.RootTable.Columns(GrdEnum.Unit).Index
            Me.grd.RootTable.Columns(GrdEnum.Unit).Position = Me.grd.RootTable.Columns(GrdEnum.Pack_Desc).Index
            'Task:2374 Total Amount's Column Forming 
            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).TotalFormatString = "N" & TotalAmountRounding  ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            'End Task:2375
            'Me.grd.AutoSizeColumns()

            'Task:2759 Set Rounded Amount Format
            Me.grd.RootTable.Columns(GrdEnum.Total).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(GrdEnum.Total).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.Total).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(GrdEnum.TotalAmount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration


            Me.grd.RootTable.Columns(GrdEnum.Rate).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.CurrentPrice).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.Cost_Price).FormatString = "N"
            Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).FormatString = "N"
            'Me.grd.RootTable.Columns(GrdEnum.Tax_Amount).FormatString = "N" & DecimalPointInValue
            'Me.grd.RootTable.Columns(GrdEnum.Tax_Amount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            'End Task:2759

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

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            StockMaster = New StockMaster
            StockMaster.StockTransId = Convert.ToInt32(GetStockTransId(Me.grdSaved.CurrentRow.Cells("PurchaseReturnNo").Value).ToString)
            Return New StockDAL().Delete(StockMaster)

        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Dim transId As Integer = 0
            'transId = Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
            StockMaster = New StockMaster
            StockMaster.StockTransId = 0 'transId
            StockMaster.DocNo = Me.txtPONo.Text.ToString.Replace("'", "''")
            StockMaster.DocDate = Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt")
            StockMaster.DocType = 2 'Convert.ToInt32(GetStockDocTypeId("GRNReturn").ToString)
            StockMaster.Remaks = Me.txtRemarks.Text.ToString.Replace("'", "''")
            StockMaster.Project = Me.cmbCostCenter.SelectedValue
            StockMaster.AccountId = Me.cmbVendor.Value
            StockMaster.StockDetailList = StockList 'New List(Of StockDetail)
            'For Each grdRow As Windows.Forms.DataGridViewRow In Me.grd.Rows
            'For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            '    StockDetail = New StockDetail
            '    StockDetail.StockTransId = transId 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
            '    StockDetail.LocationId = grdRow.Cells("LocationID").Value
            '    StockDetail.ArticleDefId = grdRow.Cells("ArticleDefId").Value
            '    StockDetail.InQty = 0
            '    StockDetail.OutQty = IIf(grdRow.Cells("Unit").Value = "Loose", Val(grdRow.Cells("Qty").Value), (Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value)))
            '    StockDetail.Rate = Val(grdRow.Cells("Price").Value)
            '    StockDetail.InAmount = 0
            '    StockDetail.OutAmount = IIf(grdRow.Cells("Unit").Value = "Loose", (Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("Price").Value)), ((Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value)) * Val(grdRow.Cells("Price").Value)))
            '    StockDetail.Remarks = String.Empty
            '    StockMaster.StockDetailList.Add(StockDetail)
            'Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If IsValidate() = True Then
                Return New StockDAL().Add(StockMaster)
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
            If IsValidate() = True Then
                Return New StockDAL().Update(StockMaster)
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub GridBarUserControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            ''Start TFS4042 : Ayesha Rehman : 30-07-2018
            If e.Column.Key = "Qty" Then
                Dim IsMinus As Boolean = True
                IsMinus = getConfigValueByType("AllowMinusStock")
                If Mode = "Normal" Then

                    If Not IsMinus Then

                        If Me.grd.CurrentRow.Cells(GrdEnum.Pack_Desc).Value.ToString = "Loose" Then
                            If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(GrdEnum.ItemId).Value, Me.grd.CurrentRow.Cells(GrdEnum.LocationId).Value)) <> grd.GetRow.Cells(GrdEnum.Qty).Value Then
                                If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(GrdEnum.ItemId).Value, Me.grd.CurrentRow.Cells(GrdEnum.LocationId).Value)) - grd.GetRow.Cells(GrdEnum.Qty).Value <= 0 Then
                                    msg_Error(Me.grd.CurrentRow.Cells(GrdEnum.Item).Value.ToString & str_ErrorStockNotEnough)
                                    grd.CancelCurrentEdit()
                                End If
                            End If
                        Else
                            If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(GrdEnum.ItemId).Value, Me.grd.CurrentRow.Cells(GrdEnum.LocationId).Value, "Loose")) <> Me.grd.CurrentRow.Cells(GrdEnum.TotalQty).Value Or Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(GrdEnum.ItemId).Value, Me.grd.CurrentRow.Cells(GrdEnum.LocationId).Value)) <> grd.GetRow.Cells(GrdEnum.Qty).Value Then
                                If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(GrdEnum.ItemId).Value, Me.grd.CurrentRow.Cells(GrdEnum.LocationId).Value, "Loose")) - Me.grd.CurrentRow.Cells(GrdEnum.TotalQty).Value < 0 Or Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(GrdEnum.ItemId).Value, Me.grd.CurrentRow.Cells(GrdEnum.LocationId).Value)) - grd.GetRow.Cells(GrdEnum.Qty).Value < 0 Then
                                    msg_Error(Me.grd.CurrentRow.Cells(GrdEnum.Item).Value.ToString & str_ErrorStockNotEnough)
                                    grd.CancelCurrentEdit()
                                End If
                            End If
                        End If
                        ''End TFS4042
                    End If
                    ''Start TFS4042
                    If CType(Me.cmbCategory.SelectedItem, DataRowView).Row.Item("AllowMinusStock").ToString = "False" AndAlso IsMinus = True Then

                        If Me.grd.CurrentRow.Cells(GrdEnum.Pack_Desc).Value.ToString = "Loose" Then
                            If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(GrdEnum.ItemId).Value, Me.grd.CurrentRow.Cells(GrdEnum.LocationId).Value)) <> grd.GetRow.Cells(GrdEnum.Qty).Value Then
                                If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(GrdEnum.ItemId).Value, Me.grd.CurrentRow.Cells(GrdEnum.LocationId).Value)) - grd.GetRow.Cells(GrdEnum.Qty).Value <= 0 Then
                                    msg_Error(Me.grd.CurrentRow.Cells(GrdEnum.Item).Value.ToString & str_ErrorStockNotEnough)
                                    grd.CancelCurrentEdit()
                                End If
                            End If
                        Else
                            If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(GrdEnum.ItemId).Value, Me.grd.CurrentRow.Cells(GrdEnum.LocationId).Value, "Loose")) <> Me.grd.CurrentRow.Cells(GrdEnum.TotalQty).Value Or Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(GrdEnum.ItemId).Value, Me.grd.CurrentRow.Cells(GrdEnum.LocationId).Value)) <> grd.GetRow.Cells(GrdEnum.Qty).Value Then
                                If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(GrdEnum.ItemId).Value, Me.grd.CurrentRow.Cells(GrdEnum.LocationId).Value, "Loose")) - Me.grd.CurrentRow.Cells(GrdEnum.TotalQty).Value < 0 Or Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(GrdEnum.ItemId).Value, Me.grd.CurrentRow.Cells(GrdEnum.LocationId).Value)) - grd.GetRow.Cells(GrdEnum.Qty).Value < 0 Then
                                    msg_Error(Me.grd.CurrentRow.Cells(GrdEnum.Item).Value.ToString & str_ErrorStockNotEnough)
                                    grd.CancelCurrentEdit()
                                End If
                            End If
                        End If

                    End If
                    ''End TFS4042
                End If
                ''End TFS4042
            End If
            GetGridDetailQtyCalculate(e)
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
    'Private Sub grd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.Leave
    '    Try
    '        Me.grd.UpdateOnLeave = True
    '        Me.grd.UpdateMode = Janus.Windows.GridEX.UpdateMode.CellUpdate
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub cmbItem_RowSelected(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
                'Else
                '    If Me.cmbItem.Value Is Nothing Then Exit Sub
            End If
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            FillCombo("ArticlePack")
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Purchase Return"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try

            Dim PreFix As String = ""
            Dim CompanyWisePrefix As Boolean = False
            CompanyWisePrefix = Convert.ToBoolean(getConfigValueByType("ShowCompanyWisePrefix").ToString)
            If CompanyWisePrefix = True Then
                PreFix = "PR" & Me.cmbCompany.SelectedValue & "-"
            Else
                PreFix = "PR-"
            End If
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                'rafay:task start
                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    Return GetSerialNo("PR" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "PurchaseReturnMasterTable", "PurchaseReturnNo")
                    'Return GetNextDocNo(PreFix & Format(Me.dtpPODate.Value, "yy"), 4, "PurchaseReturnMasterTable", "PurchaseReturnNo")
                Else
                    ''companyinitials = "PK"
                    Return GetNextDocNo(PreFix & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "PurchaseReturnMasterTable", "PurchaseReturnNo")
                End If
                ' Return GetNextDocNo(PreFix & CompanyPrefix & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "PurchaseReturnMasterTable", "PurchaseReturnNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    Return GetSerialNo("PR" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "PurchaseReturnMasterTable", "PurchaseReturnNo")
                    'Return GetNextDocNo(PreFix & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "PurchaseReturnMasterTable", "PurchaseReturnNo")
                Else
                    ''companyinitials = "PK"
                    Return GetNextDocNo(PreFix & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "PurchaseReturnMasterTable", "PurchaseReturnNo")
                End If
                'Rafay : Task End
            Else
                Return GetNextDocNo(PreFix, 6, "PurchaseReturnMasterTable", "PurchaseReturnNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'rafay End

    Private Sub btnAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItem.Click
        Try
            frmAddItem.ShowDialog()
            FillCombo("Item")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmbVendor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.ValueChanged
        Try
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            CtrlGrdBar1.Email = New SBModel.SendingEmail
            CtrlGrdBar1.Email.ToEmail = Me.cmbVendor.ActiveRow.Cells("Email").Text
            CtrlGrdBar1.Email.Subject = "Purchase Return: " + "(" & Me.txtPONo.Text & ")"
            CtrlGrdBar1.Email.DocumentNo = Me.txtPONo.Text
            CtrlGrdBar1.Email.DocumentDate = Me.dtpPODate.Value
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function EmailSave()
        EmailSave = Nothing
        Dim flg As Boolean = False
        If Me.cmbVendor.ActiveRow Is Nothing Then Exit Function

        If IsEmailAlert = True Then
            Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name='" & Me.Name & "' AND EmailAlert=1")
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
                Email.Subject = "Purchase Return " & setVoucherNo & ""
                Email.Body = "Purchase Return " _
                & "  " & IIf(setEditMode = False, "of amount " & Total_Amount + TaxAmount & " is made", "of amount " & Previouse_Amount & " is updated to " & Total_Amount + TaxAmount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
                Email.Status = "Pending"
                Call New MailSentDAL().Add(Email)
            End If
        End If
        Return EmailSave

    End Function
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            DisplayRecord("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Get_All(ByVal PurchaseReturnNo As String)
        Try
            Get_All = Nothing
            If Not PurchaseReturnNo.Length > 0 Then Exit Try
            If IsFormOpend = True Then
                If PurchaseReturnNo.Length > 0 Then
                    Dim str As String = "Select * From PurchaseReturnMasterTable WHERE PurchaseReturnNo='" & PurchaseReturnNo & "'"
                    Dim dt As DataTable = GetDataTable(str)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then


                            Me.txtReceivingID.Text = dt.Rows(0).Item("PurchaseReturnId")
                        End If
                    End If
                    '            Me.txtPONo.Text = dt.Rows(0).Item("PurchaseReturnNo").ToString
                    '            Me.dtpPODate.Value = dt.Rows(0).Item("PurchaseReturnDate")
                    '            'Me.cmbCompany.SelectedValue = dt.Rows(0).Item("LocationId")
                    '            Me.cmbVendor.Value = dt.Rows(0).Item("VendorId")
                    '            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
                    '            Me.txtPaid.Text = Val(dt.Rows(0).Item("CashPaid").ToString)
                    '            Me.cmbPo.SelectedValue = dt.Rows(0).Item("PurchaseOrderid")
                    '            Me.chkPost.Checked = dt.Rows(0).Item("Post")
                    '            Me.cmbCostCenter.SelectedValue = Me.grdSaved.GetRow.Cells("CostCenterId").Value

                    '            If IsDBNull(dt.Rows(0).Item("CurrencyType")) Then
                    '                Me.cmbCurrency.SelectedIndex = 0
                    '            Else
                    '                cmbCurrency.SelectedValue = dt.Rows(0).Item("CurrencyType")
                    '            End If

                    '            If IsDBNull(dt.Rows(0).Item("CurrencyRate")) Then
                    '                Me.cmbCurrency.SelectedIndex = 0
                    '            Else
                    '                cmbCurrency.SelectedValue = dt.Rows(0).Item("CurrencyRate")
                    '            End If

                    '            DisplayDetail(Val(Me.txtReceivingID.Text))
                    '            'GetTotal()
                    '            Me.BtnSave.Text = "&Update"
                    '            Me.cmbPo.Enabled = False
                    '            GetSecurityRights()
                    '            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab

                    '            If flgDateLock = True Then
                    '                If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
                    '                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    '                    Me.dtpPODate.Enabled = False
                    '                Else
                    '                    Me.dtpPODate.Enabled = True
                    '                End If
                    '            Else
                    '                Me.dtpPODate.Enabled = True
                    '            End If

                    '            'Me.cmbCompany.Enabled = False
                    '            IsDrillDown = True
                    '            Me.cmbVendor.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)
                    '        Else
                    '            Exit Function
                    '        End If
                    '    Else
                    '        Exit Function
                    '    End If
                    'End If

                    ''Task# H08062015  Ahmad Sharif:
                    IsDrillDown = True
                    If Me.grdSaved.RowCount <= 50 Then
                        blnDisplayAll = True
                        Me.btnSearchLoadAll_Click(Nothing, Nothing)
                        blnDisplayAll = False
                    End If

                    Dim flag As Boolean = False
                    flag = Me.grdSaved.FindAll(Me.grdSaved.RootTable.Columns("PurchaseReturnNo"), Janus.Windows.GridEX.ConditionOperator.Equal, PurchaseReturnNo)

                    If flag = True Then
                        Me.grdSaved_CellDoubleClick(Nothing, Nothing)
                    Else
                        Exit Function
                    End If
                    '' End Task# H08062015
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

    Private Sub btnSearchEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchEdit.Click
        Try
            OpenToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DebitNoteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DebitNoteToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("PurchaseReturnNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            ShowReport("PurchaseReturn", "{PurchaseReturnMasterTable.PurchaseReturnId}=" & Val(grdSaved.CurrentRow.Cells("PurchaseReturnID").Value.ToString), , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub OutwardGatepassToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutwardGatepassToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = grdSaved.GetRow.Cells("PurchaseReturnNo").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'ShowReport("rptOutwardGatePass", "{PurchaseReturnMasterTable.PurchaseReturnId}=" & Val(grdSaved.CurrentRow.Cells("PurchaseReturnID").Value.ToString), , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            ''TFS4328 : Ayesha Rehman : 29-08-2018
            AddRptParam("@ReceivingId", Val(grdSaved.CurrentRow.Cells("ReceivingId").Value.ToString))
            ShowReport("rptOutwardGatePass", , , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnSearchDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDelete.Click
        Try
            DeleteToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                DisplayRecord()
                ''Hide Buttons Edit,Delete and Print on Load Form
                Me.BtnDelete.Visible = True
                Me.BtnPrint.Visible = True
            Else
                Me.BtnEdit.Visible = False
                If blEditMode = False Then Me.BtnDelete.Visible = False
                If blEditMode = False Then Me.BtnPrint.Visible = False
                ''''''''''''''''''''''''''''''''''''''''''''''''''''
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ExportFile(ByVal VoucherId As Integer)
        Try
            If IsEmailAlert = True Then
                If IsAttachmentFile = True Then
                    crpt = New ReportDocument
                    If IO.File.Exists(str_ApplicationStartUpPath & "\Reports\PurchaseReturn.rpt") = False Then Exit Sub
                    crpt.Load(str_ApplicationStartUpPath & "\Reports\PurchaseReturn.rpt", DBServerName)
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
                    crpt.RecordSelectionFormula = "{PurchaseReturnMasterTable.PurchaseReturnId}=" & VoucherId



                    Dim crExportOps As New ExportOptions
                    Dim crDiskOps As New DiskFileDestinationOptions
                    Dim crExportType As New PdfRtfWordFormatOptions


                    If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments\") Then
                        IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments\")
                    Else
                    End If
                    FileName = String.Empty
                    FileName = "Purchase Return" & "-" & setVoucherNo & ""
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
            ExportFile(GetPurchaseReturnId)
        Catch ex As Exception

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

    Private Sub txtPackRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPackRate.LostFocus
        Try
            If Val(Me.txtPackQty.Text) = 0 Then
                txtPackQty.Text = 1
                'txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
                txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), TotalAmountRounding)
            Else
                txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)), TotalAmountRounding)
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

    Private Sub grd_Error(sender As Object, e As Janus.Windows.GridEX.ErrorEventArgs) Handles grd.Error
        Try
            e.DisplayErrorMessage = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.BtnEdit, Nothing)
            Exit Sub
        End If
        'Task:2404 Delete Record Problem In Transaction Forms
        'If e.KeyCode = Keys.Delete Then
        '    DeleteToolStripButton_Click(BtnDelete, Nothing)
        '    Exit Sub
        'End If
        If e.KeyCode = Keys.F5 Then
            BtnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    ''Task:2376 Added Function Set Comments
    Public Function SetComments(ByVal GridExRow As Janus.Windows.GridEX.GridEXRow) As String
        Try
            Dim Comments As String = String.Empty
            If GridExRow IsNot Nothing Then
                If flgCommentCustomerFormat = True Then
                    Comments += Me.cmbVendor.Text.Replace("'", "''") & ","
                End If
                If flgCommentArticleFormat = True Then
                    Comments += " " & GridExRow.Cells(GrdEnum.Item).Value.ToString & ","
                End If
                If flgCommentQtyFormat = True Then
                    'Comment against task:2583
                    'Comments += " " & IIf(Val(GridExRow.Cells(GrdEnum.PackQty).Value.ToString) = 0, Val(GridExRow.Cells(GrdEnum.Qty).Value.ToString), Val(GridExRow.Cells(GrdEnum.Qty).Value.ToString) * Val(GridExRow.Cells(GrdEnum.PackQty).Value.ToString))
                    'Task:2583 Changed Format Price And Qty
                    ''Commented below line on 22-08-2017 against TASK : TFS1357
                    'Comments += " (" & IIf(GridExRow.Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(GridExRow.Cells(GrdEnum.Qty).Value.ToString), Val(GridExRow.Cells(GrdEnum.Qty).Value.ToString) * Val(GridExRow.Cells(GrdEnum.PackQty).Value.ToString))
                    Comments += " (" & IIf(GridExRow.Cells(GrdEnum.Unit).Value.ToString = "Loose", GridExRow.Cells(GrdEnum.Qty).Value.ToString, GridExRow.Cells(GrdEnum.Qty).Value.ToString * GridExRow.Cells(GrdEnum.PackQty).Value.ToString)

                End If
                If flgCommentPriceFormat = True AndAlso flgCommentQtyFormat = True Then
                    'Comments += " X " & Val(GridExRow.Cells(GrdEnum.Rate).Value.ToString)
                    'Task No 2608 Updating for getting round Off Figures
                    'Comments += " X " & Val(GridExRow.Cells(GrdEnum.Rate).Value.ToString) & ")"
                    Comments += " X " & Math.Round(Val(GridExRow.Cells(GrdEnum.Rate).Value.ToString), DecimalPointInValue) & ")"
                    'End Task 2608
                    'ElseIf flgCommentPriceFormat = True Then
                    '    Comments += " " & Val(GridExRow.Cells(GrdEnum.Rate).Value.ToString) & ","
                    'End Task:2583
                End If
                If flgCommentRemarksFormat = True Then
                    If Me.txtRemarks.Text.Length > 0 Then Comments += " " & Me.txtRemarks.Text.Replace("'", "''") & ","
                End If
            End If

            Comments += " " & GridExRow.Cells(GrdEnum.Comments).Value.ToString
            If Comments.Contains(",") Then
                Dim str As String = Comments.Substring(Comments.LastIndexOf(","))
                If str.Length > 1 Then
                    Comments = Comments
                Else
                    Comments = Comments.Substring(0, Comments.LastIndexOf(",") - 1)
                End If
            End If

            Return Comments
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task:2376

    Private Sub BackgroundWorker3_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Try

            ''Task:2376 Get Comments Configurations
            If Not getConfigValueByType("CommentCustomerFormat").ToString = "Error" Then
                flgCommentCustomerFormat = Convert.ToBoolean(getConfigValueByType("CommentCustomerFormat").ToString)
            Else
                flgCommentCustomerFormat = False
            End If
            If Not getConfigValueByType("CommentArticleFormat").ToString = "Error" Then
                flgCommentArticleFormat = Convert.ToBoolean(getConfigValueByType("CommentArticleFormat").ToString)
            Else
                flgCommentArticleFormat = False
            End If
            If Not getConfigValueByType("CommentArticleSizeFormat").ToString = "Error" Then
                flgCommentArticleSizeFormat = Convert.ToBoolean(getConfigValueByType("CommentArticleSizeFormat").ToString)
            Else
                flgCommentArticleSizeFormat = False
            End If
            If Not getConfigValueByType("CommentArticleColorFormat").ToString = "Error" Then
                flgCommentArticleColorFormat = Convert.ToBoolean(getConfigValueByType("CommentArticleColorFormat").ToString)
            Else
                flgCommentArticleColorFormat = False
            End If
            If Not getConfigValueByType("CommentQtyFormat").ToString = "Error" Then
                flgCommentQtyFormat = Convert.ToBoolean(getConfigValueByType("CommentQtyFormat").ToString)
            Else
                flgCommentQtyFormat = False
            End If
            If Not getConfigValueByType("CommentPriceFormat").ToString = "Error" Then
                flgCommentPriceFormat = Convert.ToBoolean(getConfigValueByType("CommentPriceFormat").ToString)
            Else
                flgCommentPriceFormat = False
            End If
            If Not getConfigValueByType("CommentRemarksFormat").ToString = "Error" Then
                flgCommentRemarksFormat = Convert.ToBoolean(getConfigValueByType("CommentRemarksFormat").ToString)
            Else
                flgCommentRemarksFormat = False
            End If
            'End Task:2376
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            If Not getConfigValueByType("CurrencyonOpenLC").ToString = "Error" Then
                flgCurrenyonOpenLC = Convert.ToBoolean(getConfigValueByType("CurrencyonOpenLC").ToString)
            End If
            ''Task:2366 Added Location Wise Filter Configuration
            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
            End If
            'End Task:2366

            If Not getConfigValueByType("PurchaseAccountFrontEnd").ToString = "Error" Then
                flgPurchaseAccountFrontEnd = getConfigValueByType("PurchaseAccountFrontEnd")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.BtnEdit, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdSaved.RowCount <= 0 Then Exit Sub
            DeleteToolStripButton_Click(BtnDelete, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.F5 Then
            BtnRefresh_Click(Nothing, Nothing)
        End If
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
            str.Add("Purchase Return")
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
            If GetSMSConfig("SMS To Location On Purchase Return").Enable = True Then
                Dim strSMSBody As String = String.Empty
                Dim objData As DataTable = CType(Me.grd.DataSource, DataTable)
                Dim dt_Loc As DataTable = objData.DefaultView.ToTable("Default", True, "LocationId")
                Dim drData() As DataRow
                For j As Integer = 0 To dt_Loc.Rows.Count - 1
                    strSMSBody = String.Empty
                    strSMSBody += "Purchase Return, Doc No: " & Me.txtPONo.Text & ", Doc Date: " & Me.dtpPODate.Value.ToShortDateString & ", Supplier: " & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString & ", Invoice No: " & Me.txtPurchaseNo.Text & ", Remarks:" & Me.txtRemarks.Text & ", "
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
            If GetSMSConfig("Purchase Return").Enable = True Then
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
                Dim obj As Object = GetSMSTemplate("Purchase Return")
                If obj IsNot Nothing Then
                    objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                    Dim strMessage As String = objTemp.SMSTemplate
                    strMessage = strMessage.Replace("@AccountTitle", Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString).Replace("@AccountCode", Me.cmbVendor.ActiveRow.Cells("Code").Value.ToString).Replace("@DocumentNo", Me.txtPONo.Text).Replace("@DocumentDate", Me.dtpPODate.Value.ToShortDateString).Replace("@OtherDoc", Me.txtPurchaseNo.Text).Replace("@Remarks", Me.txtRemarks.Text).Replace("@Amount", grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@Quantity", Me.grd.GetTotal(grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@DCNo", Me.txtPurchaseNo.Text).Replace("@SONo", IIf(Me.cmbPo.SelectedIndex > 0, Me.cmbPo.Text, String.Empty)).Replace("@InvParty", Me.txtPurchaseNo.Text).Replace("@CompanyName", CompanyTitle).Replace("@SIRIUS", "Automated by www.siriussolution.com").Replace("@DetailInformation", strDetailMessage)
                    SaveSMSLog(strMessage, Me.cmbVendor.ActiveRow.Cells("Mobile").Value.ToString, "Purchase Return")

                    If GetSMSConfig("Purchase Return").EnabledAdmin = True Then
                        For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                            If strMob.Length > 10 Then
                                SaveSMSLog(strMessage, strMob, "Purchase Return")
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

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnUpdateAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateAll.Click
        Try
            blnUpdateAll = True
            Me.btnUpdateAll.Enabled = False
            Dim blnStatus As Boolean = False
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                Me.grdSaved.Row = r.Position
                EditRecord()
                If Update_Record() = True Then
                    blnStatus = True
                Else
                    blnStatus = False
                End If
            Next
            If blnStatus = True Then msg_Information("Records update successful.") : Me.btnUpdateAll.Enabled = True : RefreshControls()
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

            'If arrFile.Length > 0 Then
            If arrFile.Count > 0 Then
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
                frm._VoucherId = Me.grdSaved.GetRow.Cells("PurchaseReturnId").Value.ToString
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''9-6-2015 TASK96151 IMR Sales Invoice Partial Return
    Public Function SalesInvoicePartialReturn(ByVal IntPurchaseReturnId As Integer, ByVal SalesInvoiceId As Integer, ByVal Command As OleDb.OleDbCommand, ByVal trans As OleDb.OleDbTransaction) As Boolean
        If SalesInvoiceId <= 0 Then Return False
        'Dim objCommand As New OleDbCommand
        Try

            Command.Transaction = trans
            Command.Connection = trans.Connection
            Command.CommandTimeout = 120
            Command.CommandType = CommandType.Text
            Command.CommandText = ""
            Command.CommandText = "Update ReceivingDetailTable Set ReceivingDetailTable.PurchaseReturnQty =(IsNull(ReceivingDetailTable.PurchaseReturnQty,0)+IsNull(a.Sz1,0)), ReceivingDetailTable.PurchaseReturnTotalQty =(IsNull(ReceivingDetailTable.PurchaseReturnTotalQty,0)+IsNull(a.Qty,0)) From ReceivingDetailTable, PurchaseReturnDetailTable a, PurchaseReturnMasterTable b  WHERE  ReceivingDetailTable.ReceivingId = b.PurchaseOrderId AND a.PurchaseReturnId = b.PurchaseReturnId AND a.ArticleDefId = ReceivingDetailTable.ArticleDefId And a.LocationId = ReceivingDetailTable.LocationId And a.ArticleSize = ReceivingDetailTable.ArticleSize AND a.RefPurchaseDetailId = ReceivingDetailTable.ReceivingDetailId AND b.PurchaseOrderId=" & SalesInvoiceId & " AND b.PurchaseReturnId =" & IntPurchaseReturnId & ""
            Command.ExecuteNonQuery()


            'trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex

        End Try
    End Function
    ''9-6-2015 TASK96151 IMR Sales Invoice Partial Return
    Public Function DeleteInvoicePartialReturn(ByVal IntPurchaseReturnId As Integer, ByVal SalesInvoiceId As Integer, ByVal Command As OleDb.OleDbCommand, ByVal trans As OleDb.OleDbTransaction) As Boolean
        If SalesInvoiceId <= 0 Then Return False
        'Dim objCommand As New OleDbCommand
        Try

            Command.Transaction = trans
            Command.Connection = trans.Connection
            Command.CommandTimeout = 120
            Command.CommandType = CommandType.Text
            Command.CommandText = ""
            Command.CommandText = "Update ReceivingDetailTable Set ReceivingDetailTable.PurchaseReturnQty =(IsNull(ReceivingDetailTable.PurchaseReturnQty,0)-IsNull(a.Sz1,0)), ReceivingDetailTable.PurchaseReturnTotalQty =(IsNull(ReceivingDetailTable.PurchaseReturnTotalQty,0)-IsNull(a.Qty,0)) From ReceivingDetailTable, PurchaseReturnDetailTable a, PurchaseReturnMasterTable b  WHERE  ReceivingDetailTable.ReceivingId = b.PurchaseOrderId AND a.PurchaseReturnId = b.PurchaseReturnId AND a.ArticleDefId = ReceivingDetailTable.ArticleDefId And a.LocationId = ReceivingDetailTable.LocationId And a.ArticleSize = ReceivingDetailTable.ArticleSize AND a.RefPurchaseDetailId = ReceivingDetailTable.ReceivingDetailId AND b.PurchaseOrderId=" & SalesInvoiceId & "  AND b.PurchaseReturnId =" & IntPurchaseReturnId & ""
            Command.ExecuteNonQuery()


            'trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex

        End Try
    End Function
    'End TASKM96151

    ''Task#A2 12-06-2015 Numeric validation on some textboxes
    Private Sub txtNUM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPackRate.KeyPress, txtStock.KeyPress, txtPackQty.KeyPress, txtQty.KeyPress, txtRate.KeyPress, txtTotal.KeyPress, txtCurrencyRate.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End Task#A2 12-06-2015

    Protected Sub AdjustmentPurchaseReturn(DocID As Integer, objTrans As OleDbTransaction)
        Dim cmd As New OleDbCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        cmd.CommandTimeout = 120
        cmd.CommandType = CommandType.Text
        Try

            If Me.cmbPo.SelectedIndex > 0 Then

                cmd.CommandText = ""
                cmd.CommandText = "Delete From InvoiceAdjustmentTable WHERE VoucherDetailID=" & Val(DocID) & " AND InvoiceType='Purchase Return'"
                cmd.ExecuteNonQuery() ''Number of rows effected by delete statement.


                Dim dblTaxAmount As Double = 0D
                cmd.CommandText = "Select IsNull(SUM((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))),0)  as TaxAmount From PurchaseReturnDetailTable WHERE PurchaseReturnID=" & DocID & ""
                dblTaxAmount = cmd.CommandText = ""
                dblTaxAmount = cmd.ExecuteScalar

                Dim dblAmount As Double = 0D
                cmd.CommandText = ""
                cmd.CommandText = "Select IsNull(PurchaseReturnAmount,0)  as PurchaseReturnAmount From PurchaseReturnMasterTable WHERE PurchaseReturnID=" & DocID & ""
                dblAmount = cmd.CommandText = ""
                dblAmount = cmd.ExecuteScalar

                dblAmount = dblAmount + dblTaxAmount
                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO InvoiceAdjustmentTable(DocNo,DocDate,InvoiceId,InvoiceType,VoucherDetailID,coa_detail_id,AdjustmentAmount,Remarks,UserName,EntryDate) " _
                    & " VALUES(N'" & Me.txtPONo.Text.Replace("'", "''") & "',Convert(DateTime,'" & dtpPODate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & IIf(Me.cmbPo.SelectedIndex = -1, 0, Me.cmbPo.SelectedValue) & ", 'Purchase Return', " & Val(DocID) & "," & Me.cmbVendor.Value & "," & dblAmount & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName.Replace("'", "''") & "',Convert(DateTime,GetDate(),102))"
                cmd.ExecuteReader()
                'objTrans.Commit()
                cmd.Dispose()
            Else
                Exit Sub
            End If

        Catch ex As Exception
            objTrans.Rollback()
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
    Private Sub txtCurrentRate_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentRate.TextChanged
        Try
            If Val(Me.txtCurrentRate.Text) > 0 Then
                If Val(Me.txtDiscPercent.Text) > 0 Then
                    Me.txtRate.Text = Math.Round(Val(Me.txtCurrentRate.Text) - ((Val(Me.txtDiscPercent.Text) / 100) * Val(Me.txtCurrentRate.Text)), TotalAmountRounding)
                Else
                    Me.txtRate.Text = Val(Me.txtCurrentRate.Text)
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtDiscPercent_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscPercent.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtDiscPercent_TextChanged(sender As Object, e As EventArgs) Handles txtDiscPercent.TextChanged
        Try
            If Val(Me.txtDiscPercent.Text) > 0 Then
                If Val(Me.txtCurrentRate.Text) > 0 Then
                    Me.txtRate.Text = Math.Round(Val(Me.txtCurrentRate.Text) - (Val(txtDiscPercent.Text) / 100) * Val(Me.txtCurrentRate.Text), TotalAmountRounding)
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
                Me.txtTotal.Text = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text), TotalAmountRounding)     'Task#26082015
            End If

            Dim dblTaxPercent As Double = 0D
            Double.TryParse(Me.txtTax.Text, dblTaxPercent)
            Me.txtNetTotal.Text = Math.Round((((dblTaxPercent / 100) * Val(Me.txtTotal.Text)) + Val(Me.txtTotal.Text)), TotalAmountRounding)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTotalQuantity_LostFocus(sender As Object, e As EventArgs) Handles txtTotalQuantity.LostFocus
        GetDetailTotal()
    End Sub

    Private Sub txtTotalQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtTotalQuantity.TextChanged
        Try
            If Not Val(Me.txtPackQty.Text) > 0 Then
                Me.txtQty.Text = Val(Me.txtTotalQuantity.Text)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtRate_TextChanged(sender As Object, e As EventArgs) Handles txtRate.TextChanged

    End Sub

    Private Sub txtTotal_TextChanged(sender As Object, e As EventArgs) Handles txtTotal.TextChanged
        If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
        If Not Me.cmbItem.ActiveRow.Cells(0).Value > 0 Or Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
        GetDetailTotal()
    End Sub

    Private Sub txtTax_LostFocus(sender As Object, e As EventArgs) Handles txtTax.LostFocus
        GetDetailTotal()
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
            End If
            'Me.grd.Refetch()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                If blEditMode = True Then
                Else
                    Me.txtCurrencyRate.Text = Math.Round(Convert.ToDouble(dr.Row.Item("CurrencyRate").ToString), 4)
                End If

                If cmbPo.SelectedValue > 0 Then
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
                Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Caption = "Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns("Total Amount").Caption = "Total Amount (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns("Total").Caption = "Total (" & Me.BaseCurrencyName & ")"

                Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Caption = "Base Currency Rate (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Caption = "Tax Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyTotalAmount).Caption = "Total Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyAdTaxAmount).Caption = "AdTax Amount (" & Me.cmbCurrency.Text & ")"

                grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTotalAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAdTaxAmount).Visible = False
                Else
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = True
                    'Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTotalAmount).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAdTaxAmount).Visible = True
                    If Me.grd.RowCount > 0 Then
                        For Each GriDEX As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                            GriDEX.Cells("CurrencyRate").Value = Val(Me.txtCurrencyRate.Text)
                            GriDEX.Cells("CurrencyId").Value = Me.cmbCurrency.SelectedValue
                            GriDEX.Cells("BaseCurrencyId").Value = BaseCurrencyId
                            GriDEX.Cells("BaseCurrencyRate").Value = 1
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetCurrencyRate(ByVal currencyId As Integer) As Double
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Dim currencyRate As Double = 0
        Try
            str = " Select CurrencyRate, CurrencyId From tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId) And CurrencyId = " & currencyId & ""
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
    Private Function GetSingle(ByVal PurchaseReturnId As Integer)
        Dim Str As String = ""
        Try
            Str = " SELECT Recv.PurchaseReturnNo, Recv.PurchaseReturnDate, dbo.ReceivingMasterTable.ReceivingNo, dbo.ReceivingMasterTable.ReceivingDate, V.detail_title, Recv.PurchaseReturnQty, " & _
              "Recv.PurchaseReturnAmount, Recv.PurchaseReturnId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
              "IsNull(Recv.PurchaseOrderID,0) as PurchaseOrderID, IsNull(Recv.Post,0) As Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, ISNULL(Recv.CostCenterId,0) as CostCenterID, isnull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, tblDefCostCenter.Name, IsNull(Recv.PurchaseAcId,0) as PurchaseAcId, V.Contact_Email as Email, Recv.UserName, Recv.UpdateUserName " & _
              "FROM tblDefCostCenter Right Outer JOIN dbo.PurchaseReturnMasterTable Recv ON tblDefCostCenter.CostCenterID=Recv.CostCenterId INNER JOIN " & _
              "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
              "dbo.ReceivingMasterTable ON Recv.PurchaseOrderID = dbo.ReceivingMasterTable.ReceivingId  WHERE Recv.PurchaseReturnNo Is Not NULL And Recv.PurchaseReturnId = " & PurchaseReturnId & " "
            Dim dt As DataTable = GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub PrintVoucherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintVoucherToolStripMenuItem.Click, PrintVoucherToolStripMenuItem1.Click
        Try
            GetVoucherPrint(Me.grdSaved.CurrentRow.Cells("PurchaseReturnNo").Value.ToString, Me.Name, BaseCurrencyName, BaseCurrencyId)
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
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Purchase Return"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCompany.SelectedIndexChanged
        Try
            If IsFormOpend = True Then
                Me.txtPONo.Text = GetDocumentNo()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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

                Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Caption = "Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Caption = "Tax Amount (" & Me.cmbCurrency.Text & ")"

                'grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = False
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
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = False
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
