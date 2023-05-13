''14-Dec-2013 R916  Imran Ali               Add comments column
''16-Dec-2013 R929  Imran Ali               one time sales return for a day
''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction forms
''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''28-Dec-2013 RM6  Imran Ali                Release 2.1.0.0 Bug
''6-Jan-2014 Task:2369       Imran Ali                Comments layout on ledger
''11-Jan-2014 Task:2373         Imran Ali                Add Columns SubSub Title in Account List on Sales/Purchase
''11-Jan-2014 Task:2374           Imran Ali                Net Amount Sales/Purchase 
''31-Jan-2014     TASK:2401            Imran ali                    Store Issuence Problem 
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms
''03-Feb-2014        Task:2406   Imran Ali    FIELD CHOOSER restriction (Senior Rozgar)
''06-Feb-2014          TASK:M16     Imran Ali   Add New Fields Engine No And Chassis No. on Sales  
''17-Feb-2014  Task:2427 Imran Ali  Dispatch Detail Repor Problem.
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
''20-Feb-2014   TASK:2432 Imran Ali 1 Delivery Chalan Multi Record of Same Item 
''21-Feb-2014 Task:2435   Imran Ali  Update Cost Price In Sales, Sales Return and Store Issuance
''24-Feb-2014 Task:M21 Imran Ali Sales Return Not Save.
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
''31-Mar-2014 TASK:M28 Imran Ali Reload Sales Invoice List
'' Task No 2554 Update The Query Of Sales Return LOAD the Values Of Invoices + Date on Behalf of Customer ComboBox 
''21-Apr-2014 Task:2583 Imran Ali Comment Format On Ledger (Pasha Emb)
'06-May-2014 Task: 2605 JUNAID SHEHZAD Engine No. Filter in Sales Return
'Mughees 7-5-2014 Task No 2608 Update The Code to Get RoundOff Functionality
'15-May-2014 Task: 2626 Junaid project column added in sale / purchase and both returns in history tab.
'' 02-Jul-2014 TASK:2712 Imran Ali Purchase Price Update through Adjustment Average Rate
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''11-Sep-2014 Task:M101 Imran Ali Add new field remarks 
''13-Spe-2014 Task:2843 Imran Ali Restriction Duplicate Egine No/Chassis No In Sales Return/Delivery Chalan
''9-6-2015 TASK96151 IMR Sales Invoice Partial Return

''Task# A1-10-06-2015 Added Key Pres event for some textboxes to take just numeric and dot value
''Task# A2-10-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
''04-Jun-2015 Task:2015060001 Ali Ansari Regarding Attachements 
'08-Jun-2015  Task#2015060005 to allow all files to attach
''10-6-2015 TASKM106151 Sales Return Partially
'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'16-Sep-2015 Task#16092015 Ahmad Sharif: Load Companies and Locations user wise
''11-12-2015 TASKTFS128 Muhammad Ameen: Sales return: Allow user to sort data in products grid also allow to save layout
'' TASK-470 Muhammad Ameen on 01-07-2016: Stock Statement By Pack
''TASK : TFS1263 Muhammad Ameen Sales Return's impact on Sales Order. on 08-08-2017
''TASK : TFS957 Muhammad Ameen Implemented roles based notifications to this form. On 17-08-2017
'' TASK: TFS1357 Converted Qty columns to decimal from double to show end zero after points. Ameen on 22-08-2017 
''TASK : TFS1398 Delivery Chalan's SalesOrderDetailId should be saved to Sales based on it Sales Order should be update from Sales Return.
''TASK TFS1427 User Name should get saved to voucher. Muhammad Ameen on 12-09-2017
''TASK TFS1462 Direct voucher print function should be added. Muhammad Ameen done on 14-09-2017 
''TASK TFS1474 Muhammad Ameen on 15-09-2017. Currency rate can not be edited while base currency is selected.
'' TASK TFS1558 Muhammad Ameen on 02-10-2017 : Price Change Rights
'' TASK TFS1559 Muhammad Ameen on 02-10-2017 : Without Sales Sales Return Rights
'' TASK TFS1648 Muhammad Ameen on 03-11-2017. Quantity will be sumed in case item, location, Pack, Unit and Rate are same over addition of new row
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

Public Class frmSalesReturn
    Implements IGeneral
    Dim dt As DataTable
    Dim Mode As String = "Normal"
    Dim blnEditMode As Boolean = False
    Dim IsFormOpen As Boolean = False
    Dim StockMaster As StockMaster
    Dim StockDetail As StockDetail
    Dim Email As Email
    Dim crpt As New ReportDocument
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim setVoucherNo As String = String.Empty
    Dim Total_Amount As Double = 0D
    Dim getVoucher_Id As Integer = 0
    Dim setEditMode As Boolean = False
    Dim Previouse_Amount As Double = 0D
    Dim TaxAmount As Double = 0
    Dim PrintLog As PrintLogBE
    Dim MarketReturnAccountId As Integer = 0
    Dim flgMarketReturnVoucher As Boolean = False
    Dim flgCompanyRights As Boolean = False
    Dim flgLoadAllItems As Boolean = False
    Dim flgLocationWiseItems As Boolean = False
    Dim StockList As List(Of StockDetail)
    Dim flgOnetimeSalesReturn As Boolean = False
    ''Task:2369 Declare Boolean Variables
    Dim flgCommentCustomerFormat As Boolean = False
    Dim flgCommentArticleFormat As Boolean = False
    Dim flgCommentArticleSizeFormat As Boolean = False
    Dim flgCommentArticleColorFormat As Boolean = False
    Dim flgCommentQtyFormat As Boolean = False
    Dim flgCommentPriceFormat As Boolean = False
    Dim flgCommentRemarksFormat As Boolean = False
    Dim flgCommentEngineNo As Boolean = False
    'End Task:2369
    Dim flgVehicleIdentificationInfo As Boolean = False 'Task:M16 Added Flag Vehicle Identification
    'Marked Against Task#2015060001 Ali Ansari
    'Dim arrfile As String
    'Marked Against Task#2015060001 Ali Ansari
    'Altered Against Task#2015060001 Ali Ansari
    ' Convert string into List of string for making proper count
    Dim arrFile As List(Of String)
    'Altered Against Task#2015060001 Ali Ansari
    Dim flgMargeItem As Boolean = False ''20-Feb-2014   TASK:2432 Imran Ali 1 Delivery Chalan Multi Record of Same Item 
    Dim ItemLoadByCustomer As Boolean = False
    Dim blnUpdateAll As Boolean = False
    Private _Previous_Balance As Double = 0D
    Private blnDisplayAll As Boolean = False
    Dim flgAvgRate As Boolean = False
    Dim BaseCurrencyId As Integer
    Dim BaseCurrencyName As String = String.Empty
    Dim NotificationDAL As New NotificationTemplatesDAL
    Dim IsPreviewInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)

    ''TFS3113 : Abubakar Siddiq : This Variable is Added to check ApprovalProcessId ,if it is mapped against the document
    Dim ApprovalProcessId As Integer = 0
    ''TFS3113 : Abubakar Siddiq :End
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Dim flgItemSearch As Boolean = False
    Dim ItemFilterByName As Boolean = False
    Dim PaymentVoucherToSalesReturn As Boolean = False
    Dim PaymentPost As Boolean = False
    Dim VNo As String = String.Empty
    Dim ExistingVoucherFlg As Boolean = False
    Dim VoucherId As Integer = 0

    Dim EmailTemplate As String = String.Empty
    Dim UsersEmail As List(Of String)
    Dim EmailBody As String = String.Empty
    Dim SalesReturnNo As String
    Dim SalesReturnId As Integer
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
        BatchNo
        Unit
        Qty
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
        CurrentPrice
        PackPrice
        BatchId
        'SampleQty 'Task:2374
        Tax_Percent
        Tax_Amount
        CurrencyTaxAmount
        TotalAmount 'Task:2374 Added Index
        SampleQty 'Task:2374 Change Index Position
        PurchasePrice
        Pack_Desc
        AccountId
        SalesAccountId
        CGSAccountId
        ''Added Index Comments
        Comments
        OtherComments
        Engine_No 'Task:M16 Added Index
        Chassis_No 'Task:M16 Added Index
        CostPrice 'Task:2435 Added Index
        RefSalesDetailId 'TASKM106151 Added Column in Sales Return Detail Record (DisplayDetail(), DisplayPODetail e.g)
        TotalQty ''TASK-408
    End Enum

    Private Sub frmSalesReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try

            If e.KeyCode = Keys.F5 Then
                BtnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Insert Then
                btnAdd_Click(Nothing, Nothing)
            End If

            If e.KeyCode = Keys.F4 Then
                If BtnSave.Enabled = True Then
                    SaveToolStripButton_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(BtnNew, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                PrintToolStripButton_Click(BtnPrint, Nothing)
                Exit Sub
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
    Private Sub frmSalesReturn_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
            Me.lblProgress.Text = "Loading Please wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            'If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            '    flgCompanyRights = getConfigValueByType("CompanyRights")
            'End If
            'If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
            '    flgLoadAllItems = getConfigValueByType("LoadAllItemsInSales")
            'End If

            'If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
            '    flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
            'End If
            ' ''R929 Added Flag OnetimeSalesReturn
            'If Not getConfigValueByType("OnetimeSalesReturn").ToString = "Error" Then
            '    flgOnetimeSalesReturn = getConfigValueByType("OnetimeSalesReturn")
            'End If
            ' ''End R929

            ''Start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161
            ' ''TASK TFS4544
            'If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            '    ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            'End If
            ' ''END TFS4544


            If BackgroundWorker3.IsBusy Then Exit Sub
            BackgroundWorker3.RunWorkerAsync()
            Do While BackgroundWorker3.IsBusy
                Application.DoEvents()
            Loop
            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)
            FillCombo("Category")
            FillCombo("Vendor")
            FillCombo("SM")
            'Ali Faisal : TFS4271 : Call Bill maker and Packing man fillcombos
            FillCombo("BM")
            FillCombo("PM")
            'FillCombo("Item")
            FillCombo("Company")
            FillCombo("SM")
            FillCombo("Project")
            FillCombo("SO")
            FillCombo("Currency")
            'If frmModProperty.blnListSeachStartWith = True Then
            '    cmbItem.AutoCompleteMode = Win.AutoCompleteMode.Suggest
            '    cmbItem.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.StartsWith
            'End If

            'If frmModProperty.blnListSeachContains = True Then
            '    cmbItem.AutoCompleteMode = Win.AutoCompleteMode.Suggest
            '    cmbItem.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.Contains
            'End If
            'R-916 Comment ArticlePack Method
            'FillCombo("ArticlePack")
            'DisplayRecord()
            RefreshControls()
            'Me.DisplayRecord() R933 Commented History Data
            '//This will hide Master grid
            Me.grdSaved.Visible = CType(getConfigValueByType("ShowMasterGrid"), Boolean)
            IsFormOpen = True
            cmbCategory_SelectedIndexChanged(Nothing, Nothing)
            Get_All(frmModProperty.Tags.ToString)
            UltraDropDownSearching(cmbItem, frmMain.blnListSeachStartWith, frmMain.blnListSeachContains)
            UltraDropDownSearching(cmbVendor, frmMain.blnListSeachStartWith, frmMain.blnListSeachContains)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
            If frmModProperty.Tags.Length > 0 Then frmMain.Tags = String.Empty ''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
        End Try
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
        Dim str As String = String.Empty
        'str = "SELECT     Recv.SalesReturnNo, CONVERT(varchar, Recv.SalesReturnDate, 103) AS Date, V.CustomerName, Recv.SalesReturnQty, Recv.SalesReturnAmount, " _
        '       & " Recv.SalesReturnId, Recv.CustomerCode, Recv.Remarks, convert(varchar, Recv.CashPaid) as CashPaid FROM         dbo.SalesReturnMasterTable Recv INNER JOIN dbo.tblCustomer V ON Recv.CustomerCode = V.AccountId"
        If Mode = "Normal" Then

            'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.SalesReturnNo, Recv.SalesReturnDate AS Date, vwCOADetail.detail_title as CustomerName, V.SalesNo, Recv.SalesReturnQty, Recv.SalesReturnAmount, Recv.SalesReturnId,  " & _
            '        " Recv.CustomerCode, EmployeeDefTable.EmployeeName, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId, isnull(Recv.AdjPercent,0) as AdjPercent, isnull(Recv.Adjustment,0) as Adjustment, isnull(Recv.MarketReturns,0) as [Market Returns], ISNULL(Recv.Damage_Budget,0) as Damage_Budget, IsNull(Recv.Post,0) as Post, Isnull(Recv.CostCenterId,0) as CostCenterId, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status] " & _
            '        " FROM  SalesReturnMasterTable Recv INNER JOIN " & _
            '        " vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
            '        " EmployeeDefTable ON Recv.EmployeeCode = EmployeeDefTable.EmployeeId LEFT OUTER JOIN " & _
            '        " SalesMasterTable V ON Recv.POId = V.SalesId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesReturnNo  WHERE Recv.SalesReturnNo IS NOT NULL AND Recv.LocationId = " & Me.cmbCompany.SelectedValue & ""
            'Task: 2626 Junaid Retrieve 'Name' field from 'tblDefCostCenter'
            'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.SalesReturnNo, Recv.SalesReturnDate AS Date, vwCOADetail.detail_title as CustomerName, V.SalesNo, Recv.SalesReturnQty, Recv.SalesReturnAmount, Recv.SalesReturnId," & _
            '        " Recv.CustomerCode, EmployeeDefTable.EmployeeName, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId, isnull(Recv.AdjPercent,0) as AdjPercent, isnull(Recv.Adjustment,0) as Adjustment, isnull(Recv.MarketReturns,0) as [Market Returns], ISNULL(Recv.Damage_Budget,0) as Damage_Budget, IsNull(Recv.Post,0) as Post, Isnull(Recv.CostCenterId,0) as CostCenterId, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], tblDefCostCenter.Name " & _
            '        " FROM tblDefCostCenter Right Outer JOIN SalesReturnMasterTable Recv ON tblDefCostCenter.CostCenterID=Recv.CostCenterId INNER JOIN  " & _
            '        " vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
            '        " EmployeeDefTable ON Recv.EmployeeCode = EmployeeDefTable.EmployeeId LEFT OUTER JOIN " & _
            '        " SalesMasterTable V ON Recv.POId = V.SalesId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesReturnNo  WHERE Recv.SalesReturnNo IS NOT NULL AND Recv.LocationId = " & Me.cmbCompany.SelectedValue & ""
            'End Task: 2626
            'Marked Against Task#2015060006
            'Altered Against Task#2015060006 to add attachement field in query
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.SalesReturnNo, Recv.SalesReturnDate AS Date, vwCOADetail.detail_title as CustomerName, V.SalesNo, Recv.SalesReturnQty, Recv.SalesReturnAmount, Recv.SalesReturnId," & _
            '      " Recv.CustomerCode, EmployeeDefTable.EmployeeName, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId, isnull(Recv.AdjPercent,0) as AdjPercent, isnull(Recv.Adjustment,0) as Adjustment, isnull(Recv.MarketReturns,0) as [Market Returns], ISNULL(Recv.Damage_Budget,0) as Damage_Budget, IsNull(Recv.Post,0) as Post, Isnull(Recv.CostCenterId,0) as CostCenterId, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], tblDefCostCenter.Name,IsNull([No Of Attachment],0) as [No Of Attachment], dbo.vwCOADetail.Contact_Email as Email " & _
            '      " FROM tblDefCostCenter Right Outer JOIN SalesReturnMasterTable Recv ON tblDefCostCenter.CostCenterID=Recv.CostCenterId INNER JOIN  " & _
            '      " vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
            '      " EmployeeDefTable ON Recv.EmployeeCode = EmployeeDefTable.EmployeeId LEFT OUTER JOIN " & _
            '      " SalesMasterTable V ON Recv.POId = V.SalesId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesReturnNo LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.SalesReturnId  WHERE Recv.SalesReturnNo IS NOT NULL AND Recv.LocationId = " & Me.cmbCompany.SelectedValue & ""
            'Altered Against Task#2015060006 to add attachement field in query
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'Rafay:Modified Query to Add currency and AmountUS
            str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.SalesReturnNo, Recv.SalesReturnDate AS Date, V.SalesNo,v.SalesDate, vwCOADetail.detail_title as CustomerName, Pack.Packs, Recv.SalesReturnQty,dbo.tblcurrency.currency_code, Recv.SalesReturnAmount ,Recv.AmountUS, Recv.SalesReturnId, " & _
                              " Recv.CustomerCode, EmployeeDefTable.EmployeeName, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId, isnull(Recv.AdjPercent,0) as AdjPercent, isnull(Recv.Adjustment,0) as Adjustment, isnull(Recv.MarketReturns,0) as [Market Returns], ISNULL(Recv.Damage_Budget,0) as Damage_Budget, IsNull(Recv.Post,0) as Post, Isnull(Recv.CostCenterId,0) as CostCenterId, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], tblDefCostCenter.Name,IsNull([No Of Attachment],0) as [No Of Attachment], dbo.vwCOADetail.Contact_Email as Email ,Recv.UserName as 'User Name',Recv.UpdateUserName as [Modified By], Recv.LocationID, ISNULL(Recv.BillMakerId,0) AS BillMakerId,Currency.CurrencyRate, ISNULL(Recv.PackingManId,0) AS PackingManId " & _
                              " FROM tblDefCostCenter Right Outer JOIN SalesReturnMasterTable Recv ON tblDefCostCenter.CostCenterID=Recv.CostCenterId INNER JOIN  " & _
                              " vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN (Select SalesReturnId, Sum(IsNull(Sz1, 0)) As Packs From SalesReturnDetailTable Group By SalesReturnId) As Pack ON Recv.SalesReturnId = Pack.SalesReturnId LEFT OUTER JOIN " & _
                              " EmployeeDefTable ON Recv.EmployeeCode = EmployeeDefTable.EmployeeId  LEFT OUTER JOIN " & _
                              "(Select Distinct SalesReturnDetailTable.SalesReturnId,CurrencyId,CurrencyRate  From SalesReturnDetailTable) As Currency ON Recv.SalesReturnId = Currency.SalesReturnId LEFT OUTER JOIN  " & _
                              "dbo.tblcurrency ON Currency.CurrencyId = dbo.tblcurrency.currency_id Left Outer Join " & _
                              " SalesMasterTable V ON Recv.POId = V.SalesId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesReturnNo LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.SalesReturnId  WHERE Recv.SalesReturnNo IS NOT NULL " & IIf(blnDisplayAll = True, "", " AND Recv.LocationId = " & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbCompany.SelectedValue) & "") & ""
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            If Me.dtpFrom.Checked = True Then
                str += " AND Recv.SalesReturnDate >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
            End If
            If Me.dtpTo.Checked = True Then
                str += " AND Recv.SalesReturnDate <= Convert(Datetime, N'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
            End If
            If Me.txtSearchDocNo.Text <> String.Empty Then
                str += " AND Recv.SalesReturnNo LIKE '%" & Me.txtSearchDocNo.Text & "%'"
            End If
            'If Not Me.cmbSearchLocation.SelectedIndex = -1 Then
            '    If Me.cmbSearchLocation.SelectedIndex > 0 Then
            '        str += " AND Location.LocationId=" & Me.cmbSearchLocation.SelectedValue
            '    End If
            'End If
            If Not Me.cmbSearchLocation.SelectedIndex = -1 Then
                If Me.cmbSearchLocation.SelectedIndex > 0 Then
                    str += " AND Recv.SalesReturnId In(Select SalesReturnId From SalesReturnDetailTable WHERE LocationId=" & Me.cmbSearchLocation.SelectedValue & ")"
                End If
            End If
            If Me.txtFromAmount.Text <> String.Empty Then
                If Val(Me.txtFromAmount.Text) > 0 Then
                    str += " AND Recv.SalesReturnAmount >= " & Val(Me.txtFromAmount.Text) & " "
                End If
            End If
            If Me.txtToAmount.Text <> String.Empty Then
                If Val(Me.txtToAmount.Text) > 0 Then
                    str += " AND Recv.SalesReturnAmount <= " & Val(Me.txtToAmount.Text) & ""
                End If
            End If
            If Me.cmbSearchAccount.ActiveRow IsNot Nothing Then
                If Me.cmbSearchAccount.SelectedRow.Cells(0).Value <> 0 Then
                    str += " AND Recv.CustomerCode = " & Me.cmbSearchAccount.Value
                End If
            End If
            If Me.txtSearchRemarks.Text <> String.Empty Then
                str += " AND Recv.Remarks LIKE '%" & Me.txtSearchRemarks.Text & "%'"
            End If
            If Me.txtPurchaseNo.Text <> String.Empty Then
                str += " AND V.SalesNo LIKE  '%" & Me.txtPurchaseNo.Text & "%'"
            End If

            str += " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, SalesReturnDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & "  "
            'TASK: 2605 Engine No. Filter here
            If Me.txtSearchEngineNo.Text.Length > 0 Then
                str += " AND Recv.SalesReturnId In (Select SalesReturnId From SalesReturnDetailTable WHERE Engine_No LIKE '%" & Me.txtSearchEngineNo.Text & "%')"
            End If
            'End Task: 2605
            str += " ORDER BY Recv.SalesReturnId DESC"
        End If
        FillGridEx(grdSaved, str, True)
        grdSaved.RootTable.Columns.Add("Column1", Janus.Windows.GridEX.ColumnType.CheckBox, Janus.Windows.GridEX.EditType.CheckBox)
        grdSaved.RootTable.Columns("Column1").UseHeaderSelector = True
        grdSaved.RootTable.Columns("Column1").ActAsSelector = True

        'grdSaved.DataSource = GetDataTable(str)
        grdSaved.RootTable.Columns("SalesReturnId").Visible = False
        grdSaved.RootTable.Columns("CurrencyRate").Visible = False
        'grdSaved.RootTable.Columns(4).Visible = False
        grdSaved.RootTable.Columns("CustomerCode").Visible = False
        Me.grdSaved.RootTable.Columns("Market Returns").Visible = False
        grdSaved.RootTable.Columns("AdjPercent").Caption = "Adj %"
        grdSaved.RootTable.Columns("EmployeeCode").Visible = False
        grdSaved.RootTable.Columns("PoId").Visible = False
        grdSaved.RootTable.Columns("Post").Visible = False
        grdSaved.RootTable.Columns("CostCenterId").Visible = False
        grdSaved.RootTable.Columns("LocationID").Visible = False
        grdSaved.RootTable.Columns("Email").Visible = False
        grdSaved.RootTable.Columns("SalesReturnNo").Caption = "Issue No"
        grdSaved.RootTable.Columns("Date").Caption = "Date"
        grdSaved.RootTable.Columns("SalesNo").Caption = "Invoice No."
        grdSaved.RootTable.Columns("SalesDate").Caption = "Invoice Date"
        grdSaved.RootTable.Columns("CustomerName").Caption = "Customer"
        grdSaved.RootTable.Columns("SalesReturnQty").Caption = "Qty"
        grdSaved.RootTable.Columns("SalesReturnAmount").Caption = "Base Value"
        'rafay:Change name
        grdSaved.RootTable.Columns("currency_code").Caption = "Currency"
        grdSaved.RootTable.Columns("AmountUS").Caption = "Original Value"
        grdSaved.RootTable.Columns("EmployeeName").Caption = "Employee"
        'Rafay:the foreign currency is add on purchase history
        Dim grdSaved1 As DataTable = GetDataTable(str)
        grdSaved1.Columns("AmountUS").Expression = "IsNull(SalesReturnAmount,0) / (IsNull(CurrencyRate,0))" 'Task:2374 Show Total Amount
        Me.grdSaved.DataSource = grdSaved1
        'Set rounded format
        Me.grdSaved.RootTable.Columns("AmountUS").FormatString = "N" & DecimalPointInValue
        'rafay

        'Task: 2626 Junaid
        grdSaved.RootTable.Columns("Name").Visible = True
        grdSaved.RootTable.Columns("Name").Caption = "Project Name"
        'END Task: 2626
        grdSaved.RootTable.Columns("CustomerName").Width = 150
        grdSaved.RootTable.Columns("Remarks").Width = 300
        'grdSaved.RootTable.Columns.Add("Project Name")
        'grdSaved.RowHeadersVisible = False
        'Taks:2759 Set rounded amount format
        grdSaved.RootTable.Columns("SalesReturnAmount").FormatString = "N" & DecimalPointInValue
        grdSaved.RootTable.Columns("Market Returns").FormatString = "N" & DecimalPointInValue
        'End Task:2759
        Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat
        grdSaved.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link

        grdSaved.RootTable.Columns("BillMakerId").Visible = False
        grdSaved.RootTable.Columns("PackingManId").Visible = False
        CtrlGrdBar1_Load(Nothing, Nothing)
        CtrlGrdBar2_Load(Nothing, Nothing)
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Validate_AddToGrid() Then
                ''TASK TFS1648 called FindExistsItem here on 11-03-2017
                If Not FindExistsItem(Me.cmbItem.Value, Val(Me.txtRate.Text), Me.cmbUnit.Text, Val(Me.txtPackQty.Text), Val(Me.cmbPo.SelectedValue), Val(Me.cmbCategory.SelectedValue)) = True Then
                    AddItemToGrid()
                End If
                'AddItemToGrid()
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
        Me.Cursor = Cursors.WaitCursor
        Try
            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''Start TFS4730
            If Not getConfigValueByType("PaymentVoucherToSalesReturn").ToString = "Error" Then
                PaymentVoucherToSalesReturn = Convert.ToBoolean(getConfigValueByType("PaymentVoucherToSalesReturn").ToString)
            End If
            ''End TFS4730
            FillCombo("Item")
            cmbItem.Rows(0).Activate()
            ''END TFS4544
            blnEditMode = False
            'If BtnSave.Text = "&Update" Then
            '    FillCombo("Vendor")
            'End If R933 Commented
            txtPONo.Text = ""
            dtpPODate.Value = Now
            Me.dtpPODate.Enabled = True
            txtRemarks.Text = ""
            txtPaid.Text = ""
            Me.txtRate.Text = ""
            Me.txtPackRate.Text = ""
            'Rafay
            companyinitials = ""
            'Rafay
            'txtAmount.Text = ""
            txtTotal.Text = ""
            'txtTotalQty.Text = ""
            txtBalance.Text = ""
            txtPackQty.Text = 1
            txtSchQty.Text = ""
            cmbUnit.SelectedIndex = 0
            Me.cmbSalesPerson.SelectedIndex = 0
            Me.cmbBillMaker.SelectedIndex = 0
            Me.cmbPackingMan.SelectedIndex = 0
            If Me.cmbVendor.Rows.Count > 0 Then cmbVendor.Rows(0).Activate()
            'Me.cmbVendor.Focus()

            Me.cmbBatchNo.Text = "xxxx"
            Me.BtnSave.Text = "&Save"
            Mode = "Normal"
            Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("SR", 6, "SalesReturnMasterTable", "SalesReturnNo")
            Me.cmbPo.Enabled = True
            Me.cmbCurrency.Enabled = True
            'FillCombo("SO")
            'FillCombo("Item")
            'grd.Rows.Clear()
            'Me.cmbVendor.Focus()

            'FillComboByEdit() R933 Commented
            'Me.LinkLabel1.Visible = True
            'Me.Panel1.Visible = True
            Me.txtAdjPercent.Text = 0
            Me.txtAdjustment.Text = 0
            Me.txtTotalAmount.Text = 0
            Me.cmbCompany.Enabled = True
            Me.cmbVendor.Enabled = True
            FillCombo("SO")
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
            If flgLoadAllItems = True Then
                DisplayDetail(-1, -1, "LoadAll")
                'Me.LinkLabel1.Visible = False
            Else
                'Me.LinkLabel1.Visible = True
                DisplayDetail(-1)
            End If
            'GetSecurityRights()
            'Me.cmbProject.SelectedIndex = 0 'Comment against Task:2427
            Me.dtpFrom.Value = Date.Now.AddMonths(-1)
            Me.dtpTo.Value = Date.Now
            Me.dtpFrom.Checked = False
            Me.dtpTo.Checked = False
            Me.txtSearchDocNo.Text = String.Empty
            'Me.cmbSearchLocation.SelectedIndex = 0
            Me.txtFromAmount.Text = String.Empty
            Me.txtToAmount.Text = String.Empty
            Me.txtPurchaseNo.Text = String.Empty
            'Me.cmbSearchAccount.Rows(0).Activate()
            Me.txtSearchRemarks.Text = String.Empty
            Me.SplitContainer1.Panel1Collapsed = True
            'Me.SplitContainer1.Panel2.Height = 50
            Me.lblPrintStatus.Text = String.Empty
            Me.txtDamageBudget.Text = String.Empty
            GetTotalAmount()
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnEdit.Visible = False
            Me.BtnPrint.Visible = False
            Me.BtnDelete.Visible = False
            'Task:2427 CostCenter Selected Company Wise
            If Not Me.cmbCompany.SelectedIndex = -1 Then
                If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedValue = Val(CType(Me.cmbCompany.SelectedItem, DataRowView).Row.Item("CostCenterId").ToString)
            End If
            'End Task:2427
            blnUpdateAll = False
            Me.btnUpdateAll.Enabled = True
            Me.dtpPODate.Focus()
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
            Me.cmbCurrency.Enabled = True
            Me.txtCurrencyRate.Enabled = False
            'Ali Faisal : TFS2052 : Print error
            IsPreviewInvoice = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
            'Ali Faisal : TFS2052 : End

            'Abubakar Siddiq : TFS2375 : Enable Approval History button only in Eidt Mode
            If blnEditMode = True Then
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            Else
                Me.btnApprovalHistory.Visible = False
            End If
            'Abubakar Siddiq : TFS3113 : End
            ''Abubakar Siddiq :TFS3113 :Making Approval Button Enable in Edit Mode
            If Not getConfigValueByType("SalesReturnApproval") = "Error" Then
                ApprovalProcessId = Val(getConfigValueByType("SalesReturnApproval"))
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
            Me.txtDisc.Text = 0
            '' TASK TFS4730
            If PaymentVoucherToSalesReturn = True Then
                UltraTabControl1.Tabs(1).Visible = True
            Else
                UltraTabControl1.Tabs(1).Visible = False
            End If
            FillPaymentMethod()
            Me.cmbMethod.SelectedIndex = 0
            VoucherDetail(String.Empty)
            Me.txtVoucherNo.Text = GetVoucherNo()
            Me.txtRecAmount.Text = String.Empty
            Me.txtChequeNo.Text = String.Empty
            Me.dtpChequeDate.Value = Now
            Me.cmbMethod.Enabled = True
            Me.txtVoucherNo.Enabled = True
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar2_Load(Nothing, Nothing)
            ''END TASK 
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ClearDetailControls()
        cmbUnit.SelectedIndex = 0
        cmbBatchNo.Text = "xxxx"
        txtQty.Text = ""
        txtRate.Text = ""
        txtTotal.Text = ""
        txtPackQty.Text = 1
        txtSchQty.Text = ""
        Me.txtPackRate.Text = String.Empty
        Me.txtNetTotal.Text = ""
        Me.txtTotalQuantity.Text = String.Empty
    End Sub

    Private Function Validate_AddToGrid() As Boolean
        If cmbItem.ActiveRow.Cells(0).Value <= 0 Then
            msg_Error("Please select an item")
            cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        If Me.cmbBatchNo.Text.Length <= 0 Then
            msg_Error("Please enter batch no")
            txtBatchNo.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/26/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/26/2022)
        If Val(txtQty.Text) < 0 Then
            msg_Error("Quantity is not greater than 0")
            txtQty.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtRate.Text) < 0 Then
            msg_Error("Rate is not greater than 0")
            txtRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        If Val(txtTotalQuantity.Text) <= 0 Then
            msg_Error("Total quantity should be greater than 0")
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


    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRate.LostFocus
        Try
            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
            'Else
            '    txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
            'End If
            Me.GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        ''get the qty in case of pack unit
        If Me.cmbUnit.Text = "Loose" Then
            'txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
            txtPackQty.Text = 1
            Me.txtPackQty.Enabled = False
            Me.txtPackQty.TabStop = False
            Me.txtTotalQuantity.Enabled = False
            Me.txtPackRate.Enabled = False
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
        Try

            'grd.Rows.Add(cmbCategory.Text, cmbItem.Text, Me.cmbBatchNo.ActiveRow.Cells("BatchNo").Value, cmbUnit.Text, txtQty.Text, txtRate.Text, Val(txtTotal.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue, 0)

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
            Dim dtAddItemToGrd As DataTable = CType(Me.grd.DataSource, DataTable)
            dtAddItemToGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtAddItemToGrd.NewRow
            'drGrd.Item(GrdEnum.Category) = Me.cmbCategory.Text
            drGrd.Item(GrdEnum.LocationId) = Val(Me.cmbCategory.SelectedValue)
            drGrd.Item(GrdEnum.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
            drGrd.Item(GrdEnum.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
            drGrd.Item(GrdEnum.BatchNo) = String.Empty 'Me.cmbBatchNo.Text
            If frmItemSearch.PackQty > 1 Then
                Me.cmbUnit.Text = "Pack"
                cmbUnit_SelectedIndexChanged(Nothing, Nothing)
            End If
            drGrd.Item(GrdEnum.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            'drGrd.Item(GrdEnum.Qty) = Val(Me.txtQty.Text)
            drGrd.Item(GrdEnum.Qty) = Convert.ToDecimal(Me.txtQty.Text)
            If flgItemSearch = False Then
                drGrd.Item(GrdEnum.Rate) = Val(Me.txtRate.Text)
            Else
                If Val(txtDisc.Text) <> 0 Then
                    drGrd.Item(GrdEnum.Rate) = Val(Me.txtRate.Text) - ((Val(Me.txtRate.Text) / 100) * Val(txtDisc.Text))
                Else
                    drGrd.Item(GrdEnum.Rate) = Val(Me.txtRate.Text)
                End If
            End If
            drGrd.Item(GrdEnum.Total) = Math.Round(Val(Me.txtTotal.Text), TotalAmountRounding)
            drGrd.Item(GrdEnum.CategoryId) = Me.cmbCategory.SelectedValue
            drGrd.Item(GrdEnum.ItemId) = Val(Me.cmbItem.ActiveRow.Cells(0).Value)
            drGrd.Item(GrdEnum.PackQty) = Val(Me.txtPackQty.Text)
            drGrd.Item(GrdEnum.CurrentPrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value)
            drGrd.Item(GrdEnum.PackPrice) = Val(Me.txtPackRate.Text)
            drGrd.Item(GrdEnum.BatchId) = 0 'Me.cmbBatchNo.ActiveRow.Cells(0).Value
            drGrd.Item(GrdEnum.Tax_Percent) = Val(txtTax.Text)
            drGrd.Item(GrdEnum.Tax_Amount) = 0
            drGrd.Item(GrdEnum.SampleQty) = Val(Me.txtSchQty.Text)
            drGrd.Item(GrdEnum.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
            drGrd.Item(GrdEnum.Pack_Desc) = Me.cmbUnit.Text.ToString
            drGrd.Item(GrdEnum.AccountId) = Val(Me.cmbItem.ActiveRow.Cells("AccountId").Value.ToString)
            drGrd.Item(GrdEnum.SalesAccountId) = Val(Me.cmbItem.ActiveRow.Cells("SalesAccountId").Value.ToString)
            drGrd.Item(GrdEnum.CGSAccountId) = Val(Me.cmbItem.ActiveRow.Cells("CGSAccountId").Value.ToString)
            drGrd.Item(GrdEnum.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost_Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost_Price").Value.ToString))
            'drGrd.Item(GrdEnum.TotalQty) = Val(Me.txtTotalQuantity.Text)
            drGrd.Item(GrdEnum.TotalQty) = Convert.ToDecimal(Me.txtTotalQuantity.Text)

            'R-916 Added Index Of Comments
            drGrd.Item(GrdEnum.Comments) = String.Empty

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
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If col.Index = GrdEnum.CurrencyRate Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            dtAddItemToGrd.Rows.Add(drGrd)
            'dtAddItemToGrd.Rows.InsertAt(drGrd, 0)

            dtAddItemToGrd.AcceptChanges()
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub AddItemToGrid(ByVal ZeroAdd As Boolean)
        If ZeroAdd = True Then
            'grd.Rows.Add(cmbCategory.Text, cmbItem.Text, Me.cmbBatchNo.ActiveRow.Cells("BatchNo").Value, cmbUnit.Text, 0, txtRate.Text, Val(txtTotal.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue)
            Dim dtAddItemToGrd As DataTable = CType(Me.grd.DataSource, DataTable)
            Dim drGrd As DataRow
            drGrd = dtAddItemToGrd.NewRow
            'drGrd.Item(GrdEnum.Category) = Me.cmbCategory.Text
            drGrd.Item(GrdEnum.LocationId) = Val(Me.cmbCategory.SelectedValue)
            drGrd.Item(GrdEnum.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
            drGrd.Item(GrdEnum.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
            drGrd.Item(GrdEnum.BatchNo) = String.Empty 'Me.cmbBatchNo.Text
            drGrd.Item(GrdEnum.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            'drGrd.Item(GrdEnum.Qty) = Val(Me.txtQty.Text)
            drGrd.Item(GrdEnum.Qty) = Convert.ToDecimal(Me.txtQty.Text)

            drGrd.Item(GrdEnum.Rate) = Val(Me.txtRate.Text)
            drGrd.Item(GrdEnum.Total) = Math.Round(Val(Me.txtTotal.Text), TotalAmountRounding)
            drGrd.Item(GrdEnum.CategoryId) = Me.cmbCategory.SelectedValue
            drGrd.Item(GrdEnum.ItemId) = Val(Me.cmbItem.ActiveRow.Cells(0).Value)
            drGrd.Item(GrdEnum.PackQty) = Val(Me.txtPackQty.Text)
            drGrd.Item(GrdEnum.CurrentPrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value)
            drGrd.Item(GrdEnum.PackPrice) = Val(Me.txtPackRate.Text)
            drGrd.Item(GrdEnum.BatchId) = 0 'Me.cmbBatchNo.ActiveRow.Cells(0).Value
            drGrd.Item(GrdEnum.Tax_Percent) = 0
            drGrd.Item(GrdEnum.Tax_Amount) = 0
            drGrd.Item(GrdEnum.SampleQty) = Val(Me.txtSchQty.Text)
            drGrd.Item(GrdEnum.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
            drGrd.Item(GrdEnum.Pack_Desc) = Me.cmbUnit.Text.ToString
            drGrd.Item(GrdEnum.AccountId) = Val(Me.cmbItem.ActiveRow.Cells("AccountId").Value.ToString)
            drGrd.Item(GrdEnum.SalesAccountId) = Val(Me.cmbItem.ActiveRow.Cells("SalesAccountId").Value.ToString)
            drGrd.Item(GrdEnum.CGSAccountId) = Val(Me.cmbItem.ActiveRow.Cells("CGSAccountId").Value.ToString)
            drGrd.Item(GrdEnum.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost_Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost_Price").Value.ToString))
            'R-916 Added Index Of Comments
            drGrd.Item(GrdEnum.Comments) = String.Empty
            dtAddItemToGrd.Rows.Add(drGrd)
            dtAddItemToGrd.AcceptChanges()
            GetTotalAmount()
        End If
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged

        'If cmbCategory.SelectedIndex > 0 Then
        '    'FillCombo("ItemFilter")
        'End If
        'Try
        Try
            If IsFormOpen = True Then
                If flgLocationWiseItems = True Then FillCombo("Item")
                If flgLoadAllItems = True Then
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
        If strCondition = "Item" Then
            'str = "SELECT ArticleDefView.ArticleId as Id, ArticleDefView.ArticleCode Code, ArticleDefView.ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID],Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SubSubId,0) as AccountId, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand] FROM  ArticleDefView "
            'str = "SELECT ArticleDefView.ArticleId as Id, ArticleDefView.ArticleCode Code, ArticleDefView.ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID],Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SubSubId,0) as AccountId,SalesAccountId,CGSAccountId, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand] FROM  ArticleDefView "
            str = "SELECT ArticleDefView.ArticleId as Id, ArticleDefView.ArticleCode Code, ArticleDefView.ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,ArticleDefView.ArticleBrandName As Grade, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID],Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SubSubId,0) as AccountId,SalesAccountId,CGSAccountId, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand],IsNull(Cost_Price,0) as Cost_Price, IsNull(TradePrice,0) as [Trade Price] FROM  ArticleDefView "
            'str += " INNER JOIN vwStock ON vwStock.ArticleId = ArticleDefView.ArticleId"
            str += " where Active=1 And ArticleDefView.SalesItem=1"
            If flgCompanyRights = True Then
                str += " AND ArticleDefview.CompanyId=" & MyCompanyId
            End If
            If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                    str += " AND  ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                    'Else
                    '    str += " ORDER BY ArticleDefView.SortOrder asc" 'Comment Against task:2452
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
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("SalesAccountId").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost_Price").Hidden = True
            Me.cmbItem.Rows(0).Activate()
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
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
            End If
        ElseIf strCondition = "Category" Then
            'str = "Select ArticleGroupID, ArticleGroupName from ArticleGroupDefTable where Active=1"
            'FillDropDown(cmbCategory, str)
            'Task#16092015 Load  user wise locations
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Code from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")  order by sort_order"
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
            FillDropDown(Me.cmbSearchLocation, str)
        ElseIf strCondition = "ItemFilter" Then
            str = "SELECT     ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,ArticleDefView.ArticleBrandName As Grade,SalePrice as Price,  ArticleDefView.SizeRangeID as [Size ID], Isnull(SubSubId,0) as AccountId , SalesAccountId, CGSAccountId, ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand] FROM         ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue & " " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId, "") & "    ORDER BY ArticleDefView.SortOrder"
            FillUltraDropDown(cmbItem, str)
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("SalesAccountId").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True
            Me.cmbItem.Rows(0).Activate()
        ElseIf strCondition = "Vendor" Then
            'str = "SELECT     tblCustomer.AccountId AS ID, tblCustomer.CustomerName AS Name, tblListTerritory.TerritoryName AS Territory, tblListCity.CityName AS City,  " & _
            '        "tblListState.StateName AS State, tblCustomer.AccountId AS AcId " & _
            '        "FROM         tblListTerritory INNER JOIN " & _
            '        "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
            '        "tblListState ON tblListCity.StateId = tblListState.StateId INNER JOIN " & _
            '        "tblCustomer ON tblListTerritory.TerritoryId = tblCustomer.Territory"
            'If GetConfigValue("") = "True" Then
            'Before against task:2373
            'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
            '                    "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as [Type Id], tblCustomer.Email, tblCustomer.Phone " & _
            '                    "FROM dbo.tblCustomer INNER JOIN " & _
            '                    "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '                    "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '                    "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                    "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                    "WHERE   (dbo.vwCOADetail.account_type = 'Customer') "
            'Task:2373
            'Task:2373 Added Column Sub Sub Title
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_Code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                            "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as [Type Id], dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                            "FROM dbo.tblCustomer INNER JOIN " & _
                            "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                            "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                            "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                            "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id WHERE dbo.vwCOADetail.detail_title <> '' "
            If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                str += " AND   (dbo.vwCOADetail.account_type = 'Customer') "
            Else
                str += " AND   (dbo.vwCOADetail.account_type in('Customer','Vendor')) "
            End If
            If getConfigValueByType("ShowMiscAccountsOnSales") = "True" Then
                str += " or vwCOADetail.coa_detail_id in (SELECT tblCOAMainSubSubDetail.coa_detail_id " & _
                       "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) "
            End If
            'End Task:2373
            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            'Else

            'End If
            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            ' If LoginGroup = "Administrator" Then
            If GetMappedUserId() > 0 And getGroupAccountsConfigforSales(Me.Name) And LoginGroup <> "Administrator" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_Code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                          "dbo.tblListTerritory.TerritoryName as Territory,IsNull(tblCustomer.CustomerTypes,0) as [Type Id], dbo.vwCOADetail.Contact_Email as Email, dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                                          "FROM dbo.tblCustomer INNER JOIN " & _
                                          "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                          "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                          "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " _
                                         & "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " _
                                         & " WHERE (dbo.vwCOADetail.detail_title Is Not NULL ) "
                str += " And ( coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ") ) "
                ''TFS4689 : Getting Relevent Accounts according to the screen and Configuration
                If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                    str += " AND   (dbo.vwCOADetail.account_type = 'Customer') "
                Else
                    str += " AND   (dbo.vwCOADetail.account_type in('Customer','Vendor')) "
                End If
            End If
            ''End TFS3322
            If blnEditMode = False Then
                str += " AND vwCOADetail.Active=1"
            Else
                str += " AND vwCOADetail.Active in (0, 1, NULL)"
            End If
            str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title"
            FillUltraDropDown(cmbVendor, str)
            If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Type Id").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" 'Task:2373 Caption Change 
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
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_Code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                           "dbo.tblListTerritory.TerritoryName as Territory, dbo.vwCOADetail.Contact_Email as Email, dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                                           "FROM dbo.tblCustomer INNER JOIN " & _
                                           "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                           "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                           "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " _
                                          & "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " _
                                          & " WHERE dbo.vwCOADetail.detail_title Is Not NULL " & IIf(ShowVendorOnSales = True, " AND (dbo.vwCOADetail.account_type in ('Customer','Vendor'))", " AND (dbo.vwCOADetail.account_type in ('Customer'))") & "" _
                                       & "" & IIf(ShowMiscAccountsOnSales = True, " OR vwCOADetail.coa_detail_id IN (SELECT  DISTINCT tblCOAMainSubSubDetail.coa_detail_id " & _
                                      "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) ", "") & ""
            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            ' If LoginGroup = "Administrator" Then
            If GetMappedUserId() > 0 And getGroupAccountsConfigforSales(Me.Name) And LoginGroup <> "Administrator" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_Code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                          "dbo.tblListTerritory.TerritoryName as Territory, dbo.vwCOADetail.Contact_Email as Email, dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                                          "FROM dbo.tblCustomer INNER JOIN " & _
                                          "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                          "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                          "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " _
                                         & "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " _
                                         & " WHERE (dbo.vwCOADetail.detail_title Is Not NULL ) "
                str += " And ( coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
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

            str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title"
            FillUltraDropDown(cmbSearchAccount, str)
            If Me.cmbSearchAccount.DisplayLayout.Bands.Count > 0 Then
                Me.cmbSearchAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbSearchAccount.DisplayLayout.Bands(0).Columns("Email").Hidden = True
            End If
        ElseIf strCondition = "SO" Then
            ' Task No 2554 Update The Query Of Sales Return LOAD the Values Of Invoices on Behalf Of The customer Selection
            'TASKM106151 Sales Invoice Load
            If cmbVendor.ActiveRow IsNot Nothing Then
                '' TASK : TFS957 Added UserName column to be used to send to sales creator.
                str = "Select SalesID, SalesNo as SalesNo, CustomerCode, EmployeeCode, CostCenterId, LocationId, IsNull(POId, 0) As POId, SalesNo as [Invoice No], UserName from SalesMasterTable where CustomerCode =" & cmbVendor.Value & " And Post = 1 And  SalesId in(Select DISTINCT SalesId From SalesDetailTable where (IsNull(Sz1,0)-IsNull(SalesReturnQty,0)) >= 0) " & IIf(flgCompanyRights = True, " AND LocationId=" & MyCompanyId & "", "") & "  ORDER BY SalesNo DESC"
                cmbPo.DataSource = Nothing 'Task:M28 #ToDo Reload Sales Invoice
                FillDropDown(cmbPo, str)
            End If
            'End TASKM106151
        ElseIf strCondition = "SOComplete" Then
            str = "Select Select SalesID,SalesNo as SalesNo, CustomerCode, EmployeeCode, CostCenterId, LocationId, IsNull(POId, 0) As POId, SalesNo as [Invoice No], UserName from SalesMasterTable " & IIf(flgCompanyRights = True, " WHERE LocationId=" & MyCompanyId & "", "") & " And Post = 1  ORDER BY SalesNo DESC"
            FillDropDown(cmbPo, str)
        ElseIf strCondition = "SM" Then
            str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE isnull(SalePerson,0)=1 AND IsNull(Active,0)=1 "
            FillDropDown(Me.cmbSalesPerson, str)
        ElseIf strCondition = "BM" Then
            str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE isnull(SalePerson,0)=1 AND IsNull(Active,0)=1 "
            FillDropDown(Me.cmbBillMaker, str)
        ElseIf strCondition = "PM" Then
            str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE isnull(SalePerson,0)=1 AND IsNull(Active,0)=1 "
            FillDropDown(Me.cmbPackingMan, str)
        ElseIf strCondition = "grdLocation" Then
            'Task#16092015 Load  user wise locations
            str = String.Empty
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
        ElseIf strCondition = "Company" Then
            str = String.Empty
            'Before against task:2427
            'str = "Select CompanyId, CompanyName From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
            'Task:2427 Added Column CostCenterId
            'Task#16092015 Load  user wise companies
            'If getConfigValueByType("UserwiseCompany").ToString = "True" Then
            '    str = "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId From CompanyDefTable WHERE CompanyName <> ''  " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & "  And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")"
            'Else
            '    str = "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId From CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
            'End If

            str = "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ") " _
                & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable WHERE CompanyName <> '' " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")" _
                & "Else " _
                & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""

            'End Task:2427
            FillDropDown(Me.cmbCompany, str, False)
        ElseIf strCondition = "Project" Then
            str = String.Empty
            FillDropDown(Me.cmbProject, "Select CostCenterId, Name From tblDefCostCenter WHERE Active=1")
        ElseIf strCondition = "ArticlePack" Then
            Me.cmbUnit.ValueMember = "ArticlePackId"
            Me.cmbUnit.DisplayMember = "PackName"
            Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
        ElseIf strCondition = "Currency" Then ''TASK-407
            str = String.Empty
            str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
            FillDropDown(Me.cmbCurrency, str, False)
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
        End If
    End Sub

    Private Sub txtPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaid.TextChanged
        Try
            'txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)
            txtBalance.Text = (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Tax_Percent"), Janus.Windows.GridEX.AggregateFunction.Sum))) - Val(txtPaid.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function Save() As Boolean
        If Me.chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        Me.grd.UpdateData()
        'Dim cmbProject.SelectedValue As Integer = GetCostCenterId(Me.cmbCompany.SelectedValue)
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("SR", 6, "SalesReturnMasterTable", "SalesReturnNo")
        Me.setVoucherNo = Me.txtPONo.Text
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        Dim MarketReturnsRate As Double = 0D
        Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo.Text)
        Dim AccountId As Integer = getConfigValueByType("SalesCreditAccount")
        'Dim SalesTaxAccountId As Integer = getConfigValueByType("SalesTaxCreditAccount")
        Dim SalesTaxAccountId As Integer = 0I
        If Me.cmbCompany.SelectedValue > 0 Then
            Dim str As String = "SELECT SalesTaxAccountId FROM CompanyDefTable WHERE CompanyId = " & cmbCompany.SelectedValue & "  AND SalesTaxAccountId IS NOT NULL"
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                SalesTaxAccountId = dt.Rows(0).Item(0)
            End If
        End If
        If SalesTaxAccountId = 0 Then
            SalesTaxAccountId = Val(getConfigValueByType("SalesTaxCreditAccount").ToString) 'GetConfigValue("SalesTaxCreditAccount")
        End If
        'Dim SalesOrderAnalysis As Boolean = False
        'If Not GetConfigValue("SalesOrderAnalysis").ToString = "Error" Then
        '    SalesOrderAnalysis = Convert.ToBoolean(GetConfigValue("SalesOrderAnalysis").ToString)
        'End If
        'Dim strvoucherNo As String = GetNextDocNo("SV", 6, "tblVoucher", "voucher_no")
        If Not getConfigValueByType("OtherExpAccount").ToString = "Error" Then
            MarketReturnAccountId = CInt(getConfigValueByType("OtherExpAccount"))
        Else
            MarketReturnAccountId = 0
        End If
        Dim flgCgsVoucher As Boolean = False
        If Not getConfigValueByType("CGSVoucher").ToString = "Error" Then
            flgCgsVoucher = getConfigValueByType("CGSVoucher")
        End If
        If Not getConfigValueByType("MarketReturnVoucher").ToString = "Error" Then
            flgMarketReturnVoucher = Convert.ToBoolean(getConfigValueByType("MarketReturnVoucher").ToString)
        Else
            flgMarketReturnVoucher = False
        End If

        Dim InvAccountId As Integer = Val(getConfigValueByType("InvAccountId").ToString) 'GetConfigValue("InvAccountId") 'Inventory Account
        Dim CgsAccountId As Integer = Val(getConfigValueByType("CGSAccountId").ToString)
        Dim GLAccountArticleDepartment As Boolean
        If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
            GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
        Else
            GLAccountArticleDepartment = False
        End If

        gobjLocationId = Me.cmbCompany.SelectedValue


        If AccountId <= 0 Then
            ShowErrorMessage("Sales account is not map.")
            Me.dtpPODate.Focus()
            Return False
        End If
        If Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) <> 0 Then
            If SalesTaxAccountId <= 0 Then
                ShowErrorMessage("Tax account is not map.")
                Me.dtpPODate.Focus()
                Return False
            End If
        End If
        If flgMarketReturnVoucher = True Then
            If MarketReturnAccountId <= 0 Then
                ShowErrorMessage("Market return account is not map.")
                Me.dtpPODate.Focus()
                Return False
            End If
        End If
        If flgCgsVoucher = True Then
            If InvAccountId <= 0 Then
                ShowErrorMessage("Purchase account is not map.")
                Me.dtpPODate.Focus()
                Return False
            ElseIf CgsAccountId <= 0 Then
                ShowErrorMessage("Cost of good sold account is not map.")
                Me.dtpPODate.Focus()
                Return False
            End If
        End If

        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction





        Try
            objCommand.CommandType = CommandType.Text
            'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim SalesOrderId As Integer = 0I
            Dim dt As New DataTable

            Try
                Dim str As String = "Select isnull(POId,0) as POId From SalesMasterTable WHERE SalesNo=N'" & Me.cmbPo.Text & "'"
                dt = GetDataTable(str, trans)
                If dt.Rows.Count > 0 Then
                    SalesOrderId = dt.Rows(0).Item(0)
                End If

                SalesOrderId = Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("POId").ToString)
                'SalesOrderId = GetDataTable("Select isnull(POId,0) as POId From SalesMasterTable WHERE SalesNo=N'" & Me.cmbPo.Text & "'", trans).Rows(0).Item(0)
            Catch ex As Exception
                Throw ex
            End Try



            Dim dblAdjustment As Double = 0D
            dblAdjustment = Math.Abs(((Val(Me.txtAdjPercent.Text) * Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum))) / 100)) + Math.Abs(Val(Me.txtAdjustment.Text))
            objCommand.Transaction = trans
            objCommand.CommandText = "Insert into SalesReturnMasterTable (locationId,SalesReturnNo,SalesReturnDate,CustomerCode,Poid, Employeecode,SalesReturnQty,SalesReturnAmount, CashPaid, Remarks,UserName, Post, AdjPercent, Adjustment, Damage_Budget, CostCenterId, BillMakerId, PackingManId) values( " _
                                    & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & "," & Me.cmbSalesPerson.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) - dblAdjustment) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ", " & Val(Me.txtAdjPercent.Text) & ", " & Val(Me.txtAdjustment.Text) & ", " & Val(Me.txtDamageBudget.Text) & ", " & Me.cmbProject.SelectedValue & ", " & Me.cmbBillMaker.SelectedValue & ", " & Me.cmbPackingMan.SelectedValue & ") Select @@Identity"

            getVoucher_Id = objCommand.ExecuteScalar 'objCommand.ExecuteNonQuery()
            'Before against task:M101
            'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '                           & " cheque_no, cheque_date,post,Source,voucher_code)" _
            '                           & " VALUES(" & gobjLocationId & ", 1,  7 , N'" & Me.txtPONo.Text & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                           & " NULL, NULL, " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "',N'" & Me.txtPONo.Text & "')" _
            '                           & " SELECT @@IDENTITY"
            'TAsk:M101 Added Field Remarks
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
            ''TASK TFS1427 Added User Name 
            objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                       & " cheque_no, cheque_date,post,Source,voucher_code,Remarks, UserName, Posted_UserName)" _
                                       & " VALUES(" & gobjLocationId & ", 1,  7 , N'" & Me.txtPONo.Text & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                       & " NULL, NULL, " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "',N'" & Me.txtPONo.Text & "',N'" & Me.txtRemarks.Text.Replace("'", "''") & "', N'" & LoginUserName & "', " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ")" _
                                       & " SELECT @@IDENTITY"
            'End Task:M101
            lngVoucherMasterId = objCommand.ExecuteScalar

            '***********************
            'Deleting Detail
            '***********************
            objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
            objCommand.ExecuteNonQuery()


            'For i = 0 To grd.Rows.Count - 1
            '    If grd.Rows(i).Cells("Qty").Value <> 0 Then
            '        objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationID, Tax_Percent) values( " _
            '                                & " ident_current('SalesReturnMasterTable'), " & Val(grd.Rows(i).Cells("ItemId").Value) & ", N'" & (grd.Rows(i).Cells("Unit").Value) & "', " & Val(grd.Rows(i).Cells("Qty").Value) & ", " _
            '                                & " " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", Val(grd.Rows(i).Cells("qty").Value), (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value))) & _
            '                                ", " & Val(grd.Rows(i).Cells("rate").Value) & ", " & Val(grd.Rows(i).Cells("PackQty").Value) & " , " & Val(grd.Rows(i).Cells("CurrentPrice").Value) & ",N'" & grd.Rows(i).Cells("BatchNo").Value & _
            '                                " ', " & grd.Rows(i).Cells("BatchID").Value & "," & grd.Rows(i).Cells("LocationID").Value & ", " & Val(grd.Rows(i).Cells("Tax_Percent").Value) & " ) "

            '        objCommand.ExecuteNonQuery()

            Dim LocationType As String = String.Empty
            Dim dblAdj As Double = 0D
            Dim dblNet As Double = 0D
            Dim dblDmgBudget As Double = 0D
            dblDmgBudget = (Val(Me.txtDamageBudget.Text) / Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum))
            dblNet = (((Val(Me.txtAdjPercent.Text) * Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum))) / 100)) - Math.Abs(Val(Me.txtAdjustment.Text))
            dblAdj = Math.Abs(((Val(Me.txtAdjPercent.Text) * Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum))) / 100)) - Math.Abs(Val(Me.txtAdjustment.Text))
            dblAdj = (dblAdj / Me.grd.RecordCount)
            Dim dblMarketReturns As Double = 0D
            Dim CrrStock As Double = 0D
            Dim CostPrice As Double = 0D
            Dim intSalesReturnId As Integer = 0I
            StockList = New List(Of StockDetail)
            For i = 0 To grd.RowCount - 1

                If Val(grd.GetRows(i).Cells("TotalQty").Value) <> 0 Or Val(Me.grd.GetRows(i).Cells("SampleQty").Value) <> 0 Then

                    If GLAccountArticleDepartment = True Then
                        'Before against Task:2390 
                        'AccountId = Val(grd.GetRows(i).Cells("SalesAccountId").Value.ToString)
                        'Task:2390 Change Inventory Account
                        InvAccountId = Val(grd.GetRows(i).Cells("InvAccountId").Value.ToString)
                        'End Task:2390
                        AccountId = Val(grd.GetRows(i).Cells("SalesAccountId").Value.ToString)
                        CgsAccountId = Val(grd.GetRows(i).Cells("CGSAccountId").Value.ToString)
                        'Ali Faisal : TFS753 : Verify the accounts mapping article department wise
                        If flgCgsVoucher = True Then
                            If AccountId <= 0 Then
                                ShowErrorMessage("Article department sales account is not map.")
                                Return False
                            ElseIf CgsAccountId <= 0 Then
                                ShowErrorMessage("Article department cost of goods sold account is not map.")
                                Return False
                            End If
                        End If
                        'Ali Faisal : TFS753 : End
                    End If

                    ''13-Spe-2014 Task:2843 Imran Ali Restriction Duplicate Egine No/Chassis No In Sales Return/Delivery Chalan
                    If flgVehicleIdentificationInfo = True Then
                        objCommand.CommandText = ""
                        objCommand.CommandText = "SELECT dbo.SalesDetailTable.ArticleDefId, SalesDetailTable.Engine_No, SalesDetailTable.Chassis_No  " _
                                                        & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                        & " dbo.SalesDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesDetailTable.ArticleDefId INNER JOIN SalesMasterTable On SalesMasterTable.SalesId = SalesDetailTable.SalesId WHERE dbo.ArticleDefTable.ArticleId <> 0"
                        If Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString.Length > 0 Then
                            objCommand.CommandText += " AND SalesDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString & "'"
                        End If
                        If Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString.Length > 0 Then
                            objCommand.CommandText += " AND SalesDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString & "'"
                        End If
                        Dim dtVehicleIdentificationInfo As New DataTable
                        Dim daVehicleIdentificationInfo As New OleDbDataAdapter(objCommand)
                        daVehicleIdentificationInfo.Fill(dtVehicleIdentificationInfo)

                        objCommand.CommandText = ""
                        objCommand.CommandText = "SELECT dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Engine_No, SalesReturnDetailTable.Chassis_No  " _
                                                        & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                        & " dbo.SalesReturnDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesReturnDetailTable.ArticleDefId INNER JOIN SalesReturnMasterTable On SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId  WHERE dbo.ArticleDefTable.ArticleId <> 0"
                        'If Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString.Length > 0 Then
                        objCommand.CommandText += " AND SalesReturnDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString & "'"
                        'End If
                        If Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString.Length > 0 Then
                            objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString & "'"
                        End If
                        Dim dtSalesReturnVehichleInfo As New DataTable
                        Dim daSalesReturnVehichleInfo As New OleDbDataAdapter(objCommand)
                        daSalesReturnVehichleInfo.Fill(dtSalesReturnVehichleInfo)
                        If dtVehicleIdentificationInfo IsNot Nothing Then
                            'If dtVehicleIdentificationInfo.Rows.Count > 0 Then
                            If dtVehicleIdentificationInfo.Rows.Count > 0 Then
                                If dtSalesReturnVehichleInfo.Rows.Count > dtVehicleIdentificationInfo.Rows.Count Then
                                    If dtVehicleIdentificationInfo.Rows(0).Item("Engine_No").ToString.Length > 0 Or Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString.Length > 0 Then
                                        If Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString = dtVehicleIdentificationInfo.Rows(0).Item("Engine_No").ToString Then
                                            Throw New Exception("Engine no [" & Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString & "] already exists")
                                        End If
                                    End If
                                    If Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString.Length > 0 Or dtVehicleIdentificationInfo.Rows(0).Item("Chassis_No").ToString.Length > 0 Then
                                        If Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString = dtVehicleIdentificationInfo.Rows(0).Item("Chassis_No").ToString Then
                                            Throw New Exception("Chassis no [" & Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString & "] already exists")
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                    'End Task:2843



                    Dim dblPurchasePrice As Double = 0D
                    Dim dblCostPrice As Double = 0D
                    Dim dblCurrencyAmount As Double = 0D

                    Dim StockMasterId As Integer
                    If flgAvgRate = True Then
                        Dim strserviceitem As String = "Select ServiceItem from ArticleDefView where ArticleId = " & Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ""
                        Dim dt2serviceitem As DataTable = GetDataTable(strserviceitem, trans)
                        dt2serviceitem.AcceptChanges()
                        Dim ServiceItem1 As Boolean = Val(dt2serviceitem.Rows(0).Item("ServiceItem").ToString)
                        If ServiceItem1 = False Then
                            Dim strstockmasterstring As String = "Select StockTransId from StockMasterTable where DocNo = '" & Me.cmbPo.Text & "'"
                            Dim dtstockid As DataTable = GetDataTable(strstockmasterstring, trans)
                            If dtstockid.Rows.Count > 0 Then
                                StockMasterId = Val(dtstockid.Rows(0).Item("StockTransId").ToString)
                            End If
                            Dim strdata As String = "Select Cost_Price from StockDetailTable where  StockTransId = " & StockMasterId & " AND ArticleDefId = " & Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ""
                            Dim dtcostprice As DataTable = GetDataTable(strdata, trans)
                            If dtstockid.Rows.Count > 0 Then
                                dblCostPrice = Val(dtcostprice.Rows(0).Item("Cost_Price").ToString)
                            End If
                        End If
                    Else
                        Dim strPriceData() As String = GetRateByItem(Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)).Split(",")

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
                    'End If

                    If flgAvgRate = True Then

                        ''    Try
                        '    objCommand.CommandText = ""
                        '    'Before against task:2712
                        '    'objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))) as Amount  " _
                        '    '                                & " FROM dbo.ArticleDefTable INNER JOIN " _
                        '    '                                & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & grd.GetRows(i).Cells("ArticleDefId").Value & " " _
                        '    '                                & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                        '    'Task:2712 Rounded Amount
                        '    objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, Round(ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))),1) as Amount  " _
                        '                                   & " FROM dbo.ArticleDefTable INNER JOIN " _
                        '                                   & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & grd.GetRows(i).Cells("ArticleDefId").Value & " " _
                        '                                   & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                        '    'End Task:2712
                        '    Dim dtCrrStock As New DataTable
                        '    Dim daCrrStock As New OleDbDataAdapter(objCommand)
                        '    daCrrStock.Fill(dtCrrStock)

                        '    If dtCrrStock IsNot Nothing Then
                        '        If dtCrrStock.Rows.Count > 0 Then
                        '            'Before against task:2401
                        '            'If Val(grd.GetRows(i).Cells("Price").Value) <> 0 Then
                        '            If Val(grd.GetRows(i).Cells("Price").Value) <> 0 AndAlso Val(dtCrrStock.Rows(0).Item("qty").ToString) <> 0 Then
                        '                'End Task:2401
                        '                CrrStock = dtCrrStock.Rows(0).Item(2)
                        '                CostPrice = IIf(Val(dtCrrStock.Rows(0).Item(3).ToString) = 0, 0, dtCrrStock.Rows(0).Item(3) / CrrStock)
                        '            Else
                        '                CostPrice = Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString)
                        '            End If
                        '        Else
                        '            CostPrice = Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString)
                        '        End If
                        '    End If
                        'Catch ex As Exception

                        'End Try



                        'Dim dtLastReturnData As DataTable = GetDataTable("Select IsNull(Rate,0) as Rate, IsNull(OutQty,0) as OutQty, IsNull(StockMasterTable.StockTransId,0) as StockTransId,IsNull(StockTransDetailId,0) as StockTransDetailId From StockDetailTable INNER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefId=" & Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & " AND LEFT(StockMasterTable.DocNo,2) ='SI' AND IsNull(OutQty,0) <> 0 ORDER BY Convert(DateTime,StockMasterTable.DocDate,102),StockDetailTable.StockTransDetailId DESC ", trans)
                        'dtLastReturnData.AcceptChanges()
                        'Dim remainReturnQty As Double = 0D
                        'If dtLastReturnData Is Nothing Then Return 0
                        'Dim dblActualReturn As Double = 0D
                        'Dim dblTotalQty As Double = 0D

                        'If dtLastReturnData.Rows.Count > 0 Then
                        '    For Each r As DataRow In dtLastReturnData.Rows
                        '        If dblTotalQty = Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)))) Then
                        '            Exit For
                        '        End If
                        '        If remainReturnQty > 0 Then
                        '            If Val(r.Item("OutQty").ToString) <= remainReturnQty Then
                        '                dblActualReturn = Val(r.Item("OutQty").ToString)
                        '                remainReturnQty = Val(r.Item("OutQty").ToString) - dblActualReturn
                        '                CostPrice = Val(r.Item("Rate").ToString)
                        '            Else
                        '                'Val(r.Item("OutQty").ToString) >= remainReturnQty 
                        '                dblActualReturn = remainReturnQty
                        '                CostPrice = Val(r.Item("Rate").ToString)
                        '            End If
                        '        Else
                        '            If Val(r.Item("OutQty").ToString) >= Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)))) Then
                        '                dblActualReturn = Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value))))
                        '                CostPrice = Val(r.Item("Rate").ToString)
                        '            Else
                        '                remainReturnQty = (Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)))) - Val(r.Item("OutQty").ToString))
                        '                dblActualReturn = (Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value))))) - remainReturnQty
                        '                CostPrice = Val(r.Item("Rate").ToString)
                        '            End If
                        '        End If

                        '        'End If
                        '        StockDetail = New StockDetail
                        '        StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                        '        StockDetail.LocationId = grd.GetRows(i).Cells("LocationId").Value
                        '        StockDetail.ArticleDefId = grd.GetRows(i).Cells("ArticleDefId").Value
                        '        StockDetail.InQty = dblActualReturn 'IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value))) - remainReturnQty
                        '        StockDetail.OutQty = 0
                        '        StockDetail.Rate = IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)
                        '        StockDetail.InAmount = StockDetail.InQty * StockDetail.Rate  'IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)), (((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)))
                        '        StockDetail.OutAmount = 0
                        '        StockDetail.Remarks = String.Empty
                        '        'Task:M16 Set Values In Engine_No and Chassis_No 
                        '        StockDetail.Engine_No = grd.GetRows(i).Cells("Engine_No").Value.ToString
                        '        StockDetail.Chassis_No = grd.GetRows(i).Cells("Chassis_No").Value.ToString
                        '        'End Task:M16
                        '        StockList.Add(StockDetail)
                        '        dblTotalQty += dblActualReturn
                        '    Next
                        'Else
                        'If dblCostPrice > 0 Then
                        CostPrice = dblCostPrice 'Val(grd.GetRows(i).Cells("CostPrice").Value.ToString)
                        'Else
                        '    CostPrice = dblPurchasePrice
                        'End If
                    Else
                        CostPrice = dblPurchasePrice 'Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString)
                    End If

                    StockDetail = New StockDetail
                    StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                    StockDetail.LocationId = grd.GetRows(i).Cells("LocationId").Value
                    StockDetail.ArticleDefId = grd.GetRows(i).Cells("ArticleDefId").Value
                    ''Commented below row to add TotalQty intead of Qty for TASK-408
                    ''StockDetail.InQty = IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)))
                    StockDetail.InQty = Val(grd.GetRows(i).Cells("TotalQty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value) ''TASK-408 ON 11-06-2016
                    StockDetail.OutQty = 0
                    If flgAvgRate = True Then
                        StockDetail.Rate = CostPrice
                    Else
                        StockDetail.Rate = Val(grd.GetRows(i).Cells("PurchasePrice").Value)
                    End If

                    ''Commented following row for TASK-408 to add TotalQty instead of Qty on 11-06-2016
                    ''StockDetail.InAmount = IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)), (((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)))
                    StockDetail.InAmount = ((Val(grd.GetRows(i).Cells("TotalQty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * StockDetail.Rate)
                    StockDetail.OutAmount = 0
                    StockDetail.Remarks = grd.GetRows(i).Cells("Comments").Value.ToString
                    'Task:M16 Set Values In Engine_No and Chassis_No 
                    StockDetail.Engine_No = grd.GetRows(i).Cells("Engine_No").Value.ToString
                    StockDetail.Chassis_No = grd.GetRows(i).Cells("Chassis_No").Value.ToString
                    'End Task:M16
                    ''Start TASK-470
                    StockDetail.PackQty = Val(grd.GetRows(i).Cells("PackQty").Value.ToString)
                    StockDetail.In_PackQty = Val(grd.GetRows(i).Cells("Qty").Value.ToString)
                    StockDetail.Out_PackQty = 0
                    ''End TASK-470
                    StockList.Add(StockDetail)


                    'End If
                    LocationType = GetDataTable("Select Location_Type From tblDefLocation WHERE Location_Id=" & grd.GetRows(i).Cells("LocationId").Value, trans).Rows(0).Item(0).ToString
                    'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc) values( " _
                    '                        & " ident_current('SalesReturnMasterTable'), " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ", N'" & (grd.GetRows(i).Cells("Unit").Value) & "', " & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                    '                        & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & _
                    '                        ", " & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & _
                    '                        " ', " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(Me.grd.GetRows(i).Cells("SampleQty").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                    'objCommand.ExecuteNonQuery()
                    'Before against task:2435
                    'R-916 Added Column Comments
                    'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc,Comments) values( " _
                    '                     & " ident_current('SalesReturnMasterTable'), " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ", N'" & (grd.GetRows(i).Cells("Unit").Value) & "', " & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                    '                     & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & _
                    '                     ", " & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & _
                    '                     " ', " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(Me.grd.GetRows(i).Cells("SampleQty").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "') "
                    'Task:2435 Added Column CostPrice
                    'Before against task:2527
                    'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc,Comments, CostPrice) values( " _
                    '                    & " ident_current('SalesReturnMasterTable'), " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ", N'" & (grd.GetRows(i).Cells("Unit").Value) & "', " & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                    '                    & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & _
                    '                    ", " & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & _
                    '                    " ', " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(Me.grd.GetRows(i).Cells("SampleQty").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', " & Val(StockDetail.Rate) & ") "
                    'Task:2527 Added Column Engine_No, Chassis_No
                    'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc,Comments, CostPrice, Engine_No, Chassis_No) values( " _
                    '                   & " ident_current('SalesReturnMasterTable'), " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ", N'" & (grd.GetRows(i).Cells("Unit").Value) & "', " & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                    '                   & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & _
                    '                   ", " & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & _
                    '                   " ', " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(Me.grd.GetRows(i).Cells("SampleQty").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', " & Val(StockDetail.Rate) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "') "

                    'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc,Comments, CostPrice, Engine_No, Chassis_No,Other_Comments) values( " _
                    '                 & " ident_current('SalesReturnMasterTable'), " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ", N'" & (grd.GetRows(i).Cells("Unit").Value) & "', " & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                    '                 & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & _
                    '                 ", " & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & _
                    '                 " ', " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(Me.grd.GetRows(i).Cells("SampleQty").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', " & Val(StockDetail.Rate) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "')Select @@Identity "
                    'TASKM106151 Added Column RefSalesDetailId
                    ''TASK : TFS1263 include SalesOrderDetailId to be saved to track Sales Order. ON 08-08-2017
                    objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc,Comments, CostPrice, Engine_No, Chassis_No,Other_Comments, RefSalesDetailId, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, SalesOrderDetailId) values( " _
                                                         & " ident_current('SalesReturnMasterTable'), " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ", N'" & (grd.GetRows(i).Cells("Unit").Value) & "', " & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                                                         & " " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) & _
                                                         ", " & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & _
                                                         " ', " & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(Me.grd.GetRows(i).Cells("SampleQty").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("CostPrice").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("RefSalesDetailId").Value.ToString) & " ," & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ")Select @@Identity "
                    'End TASKM106151


                    objCommand.ExecuteScalar()
                    'Val(grd.Rows(i).Cells(5).Value)


                    If Not flgMarketReturnVoucher = True Then
                        '***********************
                        'Inserting Debit Amount
                        '***********************
                        'Before against task:2369 
                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId) " _
                        '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", 0, N'" & grd.GetRows(i).Cells("item").Value & "(" & Val(grd.GetRows(i).Cells("Qty").Value) & ")', " & grd.GetRows(i).Cells("ArticleDefId").Value & "," & cmbProject.SelectedValue & ")"
                        'objCommand.ExecuteNonQuery()
                        'Task:2369 Change Comments
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, EngineNo, ChassisNo) " _
                                              & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & (Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("Price").Value.ToString) * Val(txtCurrencyRate.Text)) & ", 0, N'" & SetComments(Me.grd.GetRows(i)).Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & "," & cmbProject.SelectedValue & " , " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", 0, " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "')" ''TASK-408 TotalQty instead of Qty
                        '& " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", 0, N'" & SetComments(Me.grd.GetRows(i)).Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & "," & cmbProject.SelectedValue & ")"

                        objCommand.ExecuteNonQuery()

                        '***********************
                        'Inserting Credit Amount
                        '***********************
                        'Before against task:2369
                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, direction, CostCenterId) " _
                        '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ",0, " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", N'" & grd.GetRows(i).Cells("item").Value & "(" & Val(grd.GetRows(i).Cells("Qty").Value) & ")', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & cmbProject.SelectedValue & ")"
                        'objCommand.ExecuteNonQuery()
                        'Task:2369 Change Comments
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, direction, CostCenterId , Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, EngineNo, ChassisNo) " _
                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ",0, " & (Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("Price").Value.ToString) * Val(txtCurrencyRate.Text)) & ", N'" & SetComments(Me.grd.GetRows(i)).Replace("" & Me.cmbVendor.Text & "", "").Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & IIf(cmbProject.SelectedValue = Nothing, 0, Me.cmbProject.SelectedValue) & " , 0, " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "')" ''TASK-408 added TotalQty intead Qty
                        '& " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ",0, " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", N'" & SetComments(Me.grd.GetRows(i)).Replace("" & Me.cmbVendor.Text & "", "").Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & cmbProject.SelectedValue & ")" 

                        objCommand.ExecuteNonQuery()
                        'End Task:2369

                    End If

                    Try



                        'If SalesOrderId > 0 Then

                        '    objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                        '                                                       & " SET  DeliveredQty = (isnull(DeliveredQty,0) -  " & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & "), DeliveredTotalQty = (isnull(DeliveredTotalQty,0) -  " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) & ") " _
                        '                                                       & " WHERE    (SalesOrderId = " & SalesOrderId & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ") AND (SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value) & ")"
                        '    objCommand.ExecuteNonQuery()



                        'End If
                        ''TASK : TFS1398
                        'If Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) > 0 Then
                        '    objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                        '                                                       & " SET  DeliveredQty = (isnull(DeliveredQty,0) -  " & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & "), DeliveredTotalQty = (isnull(DeliveredTotalQty,0) -  " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) & ") " _
                        '                                                       & " WHERE (ArticleDefId = " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ") AND (SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value) & ")"
                        '    objCommand.ExecuteNonQuery()

                        '    ''Set Sales Order Status
                        '    objCommand.CommandText = "Select SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) as DeliveredQty, IsNull(SalesOrderId, 0) As SalesOrderId from SalesOrderDetailTable where SalesOrderId In (Select Distinct SalesOrderId From SalesOrderDetailTable Where SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ") Group By SalesOrderId Having SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) > 0 "
                        '    Dim da As New OleDbDataAdapter(objCommand)
                        '    Dim dtSO As New DataTable
                        '    da.Fill(dtSO)
                        '    If dtSO.Rows.Count > 0 Then
                        '        objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & dtSO.Rows(0).Item("SalesOrderId") & ""
                        '        objCommand.ExecuteNonQuery()
                        '    End If
                        ''
                        'End If
                        If Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) > 0 Then
                            objCommand.CommandText = ""
                            objCommand.CommandText = "Select SUM(isnull(Sz1,0)-isnull(SalesReturnQty , 0)) as RemainingQty , SUM(isnull(Qty,0)-isnull(SalesReturnTotalQty , 0)) as RemainingTotalQty from SalesDetailTable where SaleDetailId = " & Val(Me.grd.GetRows(i).Cells("RefSalesDetailId").Value.ToString) & ""
                            Dim daSales As New OleDbDataAdapter(objCommand)
                            Dim dtSales As New DataTable
                            daSales.Fill(dtSales)

                            If Val(grd.GetRows(i).Cells("Qty").Value.ToString) <= dtSales.Rows(0).Item("RemainingQty") Then
                                objCommand.CommandText = ""
                                objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                                                   & " SET  DeliveredQty = (isnull(DeliveredQty,0) -  " & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & "), DeliveredTotalQty = (isnull(DeliveredTotalQty,0) -  " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) & ") " _
                                                                                   & " WHERE (ArticleDefId = " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ") AND (SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value) & ")"
                                objCommand.ExecuteNonQuery()
                            Else
                                objCommand.CommandText = ""
                                objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                                                  & " SET  DeliveredQty = (isnull(DeliveredQty,0) -  " & Val(dtSales.Rows(0).Item("RemainingQty").ToString) & "), DeliveredTotalQty = (isnull(DeliveredTotalQty,0) -  " & Val(dtSales.Rows(0).Item("RemainingTotalQty").ToString) & ") " _
                                                                                  & " WHERE (ArticleDefId = " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ") AND (SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value) & ")"
                                objCommand.ExecuteNonQuery()
                            End If
                            ''Set Sales Order Status
                            objCommand.CommandText = ""
                            objCommand.CommandText = "Select SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) as DeliveredQty, IsNull(SalesOrderId, 0) As SalesOrderId from SalesOrderDetailTable where SalesOrderId In (Select Distinct SalesOrderId From SalesOrderDetailTable Where SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ") Group By SalesOrderId Having SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) > 0 "
                            Dim da As New OleDbDataAdapter(objCommand)
                            Dim dtSO As New DataTable
                            da.Fill(dtSO)
                            If dtSO.Rows.Count > 0 Then
                                objCommand.CommandText = ""
                                objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & dtSO.Rows(0).Item("SalesOrderId") & ""
                                objCommand.ExecuteNonQuery()
                            End If
                            ''
                        End If


                    Catch ex As Exception

                    End Try

                End If

                Dim str As String = "Select ServiceItem from ArticleDefView where ArticleId = " & grd.GetRows(i).Cells("ArticleDefId").Value & ""
                Dim dt2 As DataTable = GetDataTable(str, trans)
                dt2.AcceptChanges()
                Dim ServiceItem As Boolean = Val(dt2.Rows(0).Item("ServiceItem").ToString)
                If ServiceItem = False Then

                    If flgCgsVoucher = True Then

                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount, debit_amount, comments, CostCenterId, direction, sp_refrence, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, EngineNo, ChassisNo ) " _
                                                                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & CgsAccountId & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * CostPrice & ", 0, N'" & grd.GetRows(i).Cells("Item").Value & " " & "(" & Val(grd.GetRows(i).Cells("TotalQty").Value) & " X " & CostPrice & ")', " & cmbProject.SelectedValue & ", " & grd.GetRows(i).Cells("ArticleDefId").Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' , 0, " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * CostPrice & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "')" ''TASK-408 added TotalQty instead of Qty on 11-06-2016
                        objCommand.ExecuteNonQuery()

                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount, debit_amount,  comments, CostCenterId, direction, sp_refrence , Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, EngineNo, ChassisNo) " _
                                                                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & InvAccountId & ", 0, " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * CostPrice & ", N'" & grd.GetRows(i).Cells("Item").Value & " " & "(" & Val(grd.GetRows(i).Cells("TotalQty").Value) & " X " & CostPrice & ")', " & cmbProject.SelectedValue & ", " & grd.GetRows(i).Cells("ArticleDefId").Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' , " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * CostPrice & ", 0, " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "')" ''TASK-408 added TotalQty instead of Qty on 11-06-2016
                        objCommand.ExecuteNonQuery()
                    End If
                    '''''''''''''''''''''''''''''' Code By Imran Ali 03/06/2013 '''''''''''''''''''''''''''''''''''''''
                End If

            Next

            If flgMarketReturnVoucher = True Then
                If Val(Me.txtTotalAmount.Text) > 0 Then
                    '***********************
                    'Inserting Debit Amount
                    '***********************

                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & Val(Me.txtTotalAmount.Text) & ", 0, 'Damage Budget " & "(Total Qty" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ")', " & cmbProject.SelectedValue & ")" ''TASK-408 added TotalQty instead of Qty on 11-06-2016
                    objCommand.ExecuteNonQuery()

                    '***********************
                    'Inserting Credit Amount
                    '***********************
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ",0, " & ((Val(Me.txtTotalAmount.Text))) & ", 'Damage Budget " & "(Total Qty " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ")', " & cmbProject.SelectedValue & ")" ''TASK-408 added TotalQty instead of Qty on 11-06-2016
                    objCommand.ExecuteNonQuery()
                Else
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                                                              & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ",0, " & Math.Abs(Val(Me.txtTotalAmount.Text)) & ",  'Damage Budget " & "(Total Qty" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ")', " & cmbProject.SelectedValue & ")" ''TASK-408 added TotalQty instead of Qty on 11-06-2016
                    objCommand.ExecuteNonQuery()

                    '***********************
                    'Inserting Credit Amount
                    '***********************
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Math.Abs(Val(Me.txtTotalAmount.Text)) & ",0, 'Damage Budget " & "(Total Qty " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ")', " & cmbProject.SelectedValue & ")" ''TASK-408 added TotalQty instead of Qty on 11-06-2016
                    objCommand.ExecuteNonQuery()
                End If
            End If

            'If Val(Me.txtTax.Text) > 0 Then
            'changes added by murtaza ahmad for currency rate
            If Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) > 0 Then

                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount,debit_amount,  comments, CostCenterId, Currency_Debit_Amount, Currency_Credit_Amount, CurrencyId, CurrencyRate) " _
                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 0, 'Ref Tax Sales Return: " & IIf(Me.cmbPo.SelectedIndex <= 0, Me.txtPONo.Text, CType(Me.cmbPo.SelectedItem, DataRowView).Row.Item("Invoice No").ToString) & "', " & cmbProject.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) / Val(txtCurrencyRate.Text) & ",  0 , " & cmbCurrency.SelectedValue & ", " & Val(txtCurrencyRate.Text) & ")"
                objCommand.ExecuteNonQuery()


                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id,credit_amount, debit_amount, comments, CostCenterId, Currency_Debit_Amount, Currency_Credit_Amount, CurrencyId, CurrencyRate) " _
                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & SalesTaxAccountId & ", 0,  " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 'Ref Tax Sales Return: " & IIf(Me.cmbPo.SelectedIndex <= 0, Me.txtPONo.Text, CType(Me.cmbPo.SelectedItem, DataRowView).Row.Item("Invoice No").ToString) & "', " & cmbProject.SelectedValue & ", 0,  " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) / Val(txtCurrencyRate.Text) & ", " & cmbCurrency.SelectedValue & ", " & Val(txtCurrencyRate.Text) & ")"
                objCommand.ExecuteNonQuery()

            End If

            objCommand.CommandText = "Update SalesReturnMasterTable SET MarketReturns=" & dblMarketReturns & " WHERE SalesReturnId=" & getVoucher_Id
            objCommand.ExecuteNonQuery()


            Call AdjustmentSalesReturn(getVoucher_Id, trans)
            'Adustment Sales Return Invoice.


            objCommand.CommandText = "Select SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & SalesOrderId & " Having SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) > 0 "
            Dim daPOQty As New OleDbDataAdapter(objCommand)
            Dim dtPOQty As New DataTable
            daPOQty.Fill(dtPOQty)
            Dim blnEqual1 As Boolean = True
            If dtPOQty.Rows.Count > 0 Then
                'For Each r As DataRow In dtPOQty.Rows
                'If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
                blnEqual1 = True
            Else
                blnEqual1 = False
                'Exit For
                'End If
                ' Next
            End If
            If blnEqual1 = True Then
                objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & SalesOrderId & ""
                objCommand.ExecuteNonQuery()
            End If

            'TASKM106151 Partially Sales Return Qty Update In Sales Detail Table For Keep Status
            If Not Me.cmbPo.SelectedIndex = -1 And Me.cmbPo.SelectedIndex > 0 Then
                SalesInvoicePartialReturn(getVoucher_Id, Me.cmbPo.SelectedValue, objCommand, trans)
            End If
            'End TASKM106151
            If IsValidate() = True Then
                Call New StockDAL().Add(StockMaster, trans)
            End If
            '' TASK TFS4730
            If PaymentVoucherToSalesReturn = True AndAlso Val(Me.txtRecAmount.Text) > 0 Then
                If Me.cmbDepositAccount.SelectedIndex = 0 Or Me.cmbDepositAccount.SelectedIndex = -1 Then
                    ShowErrorMessage("Payment Account is required.")
                    trans.Rollback()
                    Exit Function
                End If
                SavePaymentVoucher(objCommand, Me.txtVoucherNo.Text)
            End If
            '' END TASK 4730
            trans.Commit()
            Save = True

            SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Sales, Me.txtPONo.Text.Trim, True)
            SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.txtPONo.Text, True)


            ''Start TFS3113
            ''insert Approval Log
            SaveApprovalLog(EnumReferenceType.SalesReturn, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Sales Return ," & cmbVendor.Text & "", Me.Name, 7)
            ''End TFS3113


            'insertvoucher()
            'Call Save1() 'Upgrading Stock ...
            SendSMS()
            setEditMode = False
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            TaxAmount = Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            dblMarketReturns = 0D
            Dim ValueTable As DataTable = GetSingle(getVoucher_Id)
            'NotificationDAL.SaveAndSendNotification("Sales Return", "SalesReturnMasterTable", getVoucher_Id, ValueTable, "Sales > Sales Return")
            '' TASK : TFS957 called this method to send notifications to role based users.
            SendNotification(setVoucherNo, getVoucher_Id, Me.grd.RowCount)
            getVoucher_Id = 0I
        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Function





    Sub InsertVoucher()


    End Sub

    Private Function FormValidate() As Boolean

        If txtPONo.Text = "" Then
            msg_Error("Please enter PO No.")
            txtPONo.Focus() : FormValidate = False : Exit Function
        End If
        If cmbVendor.ActiveRow.Cells(0).Value <= 0 Then
            msg_Error("Please select Customer")
            cmbVendor.Focus() : FormValidate = False : Exit Function
        End If

        If Not Me.grd.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            cmbItem.Focus() : FormValidate = False : Exit Function
        End If
        ''R929 Validation Here, If Invoice Already Entered
        If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
            If flgOnetimeSalesReturn = True Then
                If CheckExistingSalesReturnInvoice(Me.cmbVendor.Value, Me.dtpPODate.Value) > 0 Then
                    'ShowErrorMessage("Sales return for the customer has already been entered.")
                    ShowErrorMessage("sales return of this customer is already entered.") 'R:M6 Change paragraph
                    Me.cmbVendor.Focus()
                    Return False
                End If
            End If
        End If
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

        'start task 3113 Added by abubakar Siddiq
        If blnEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document is in Approval Process") : Return False : Exit Function
                End If
            End If
        End If
        'end task 3113, Added by Abubakar Siddiq
        'Change by murtaza default currency rate(10/25/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : FormValidate = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/25/2022)

        Return True

    End Function
    Sub EditRecord()
        Try
            blnEditMode = True
            'FillCombo("Vendor") R933 Commented
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If blnUpdateAll = False Then
                If Me.grd.RowCount > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                End If
            End If

            RemoveHandler cmbCompany.SelectedIndexChanged, AddressOf Me.cmbCompany_SelectedIndexChanged
            RemoveHandler cmbVendor.Leave, AddressOf Me.cmbVendor_Leave
            RemoveHandler cmbPo.SelectedIndexChanged, AddressOf Me.cmbPo_SelectedIndexChanged

            'Me.FillCombo("SOComplete") R933 Commented

            Me.cmbCompany.SelectedValue = Val(Me.grdSaved.GetRow.Cells("LocationID").Value.ToString)
            txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value.ToString
            dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            txtReceivingID.Text = grdSaved.CurrentRow.Cells("SalesReturnId").Value
            'TODO. ----
            cmbVendor.Value = grdSaved.CurrentRow.Cells("CustomerCode").Value
            ''Start TFS3322 : Uncommented Against TFS3322 : Ayesha Rehman
            'R933 Validate Customer
            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Customer is disable.")
                Exit Sub
            End If
            Me.FillCombo("SO") ''TFS4232
            ''End TFS
            Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value
            'R933 Validate PO
            If cmbPo.SelectedValue Is Nothing Then
                Dim dt As DataTable = CType(Me.cmbPo.DataSource, DataTable) 'New DataTable("PO", "PO")
                'dt.Columns.Add("PoId", GetType(System.Int32))
                'dt.Columns.Add("SalesNo", GetType(System.String))
                'dt.Columns.Add("Invoice No", GetType(System.String), "SalesNo")
                Dim dr As DataRow
                dr = dt.NewRow
                dr(0) = Me.grdSaved.CurrentRow.Cells("PoId").Value
                dr(1) = Me.grdSaved.CurrentRow.Cells("SalesNo").Value.ToString
                dr(7) = Me.grdSaved.CurrentRow.Cells("SalesNo").Value.ToString
                dt.Rows.Add(dr)
                dt.AcceptChanges()
                'Me.cmbPo.DataSource = Nothing
                'Me.cmbPo.ValueMember = "PoId"
                'Me.cmbPo.DisplayMember = "SalesNo"
                'Me.cmbPo.DataSource = dt
            End If
            Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value
            'End R933

            txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
            txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""
            Me.cmbSalesPerson.SelectedValue = grdSaved.CurrentRow.Cells("EmployeeCode").Value.ToString
            Me.cmbBillMaker.SelectedValue = grdSaved.CurrentRow.Cells("BillMakerId").Value.ToString
            Me.cmbPackingMan.SelectedValue = grdSaved.CurrentRow.Cells("PackingManId").Value.ToString
            Call DisplayDetail(grdSaved.CurrentRow.Cells("SalesReturnId").Value)

            Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            Me.txtAdjPercent.Text = grdSaved.CurrentRow.Cells("AdjPercent").Value & ""
            Me.txtAdjustment.Text = grdSaved.CurrentRow.Cells("Adjustment").Value & ""
            Me.txtTotalAmount.Text = grdSaved.CurrentRow.Cells("Market Returns").Value & ""
            Me.txtDamageBudget.Text = grdSaved.CurrentRow.Cells("Damage_Budget").Value
            Me.cmbProject.SelectedValue = grdSaved.CurrentRow.Cells("CostCenterId").Value
            'GetTotal()
            Me.BtnSave.Text = "&Update"
            'Me.cmbPo.Enabled = False
            CtrlGrdBar1_Load(Nothing, Nothing)
            ''Abubakar Siddiq :TFS3113 :Making Approval Button Enable in Edit Mode
            Me.btnApprovalHistory.Visible = True
            Me.btnApprovalHistory.Enabled = True
            ''Abubakar Siddiq :TFS3113 :End


            If Me.cmbPo.SelectedIndex > 0 Then
                Me.cmbVendor.Enabled = False
            Else
                Me.cmbVendor.Enabled = True
            End If

            If getConfigValueByType("AllowChangeSO").ToString.ToUpper = "TRUE" Then
                Me.cmbPo.Enabled = True
            Else
                Me.cmbPo.Enabled = False
            End If


            GetSecurityRights()
            Me.chkPost.Checked = Me.grdSaved.CurrentRow.Cells("Post").Value
            If blnUpdateAll = False Then Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
            Me.lblPrintStatus.Text = "Print Status: " & Me.grdSaved.GetRow.Cells("Print Status").Text.ToString
            GetTotalAmount()
            Me.cmbCompany.Enabled = False
            If Me.cmbPo.SelectedIndex > 0 Then
                Me.cmbVendor.Enabled = False
            Else
                Me.cmbVendor.Enabled = True
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

            'Altered Against Task# 2015060001 Ali Ansari
            'Get no of attached files
            Dim intCountAttachedFiles As Integer = 0I
            If Me.BtnSave.Text <> "&Save" Then
                If Me.grdSaved.RowCount > 0 Then
                    intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                    Me.btnAttachment.Text = "Attachment (" & intCountAttachedFiles & ")"
                End If
            End If
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnDelete.Visible = True
            Me.BtnPrint.Visible = True
            '' TASK TFS4730
            VoucherDetail(Me.txtPONo.Text)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            ''END TASK 

            AddHandler cmbCompany.SelectedIndexChanged, AddressOf Me.cmbCompany_SelectedIndexChanged
            AddHandler cmbVendor.Leave, AddressOf Me.cmbVendor_Leave
            AddHandler cmbPo.SelectedIndexChanged, AddressOf Me.cmbPo_SelectedIndexChanged
            'Mode = "Edit"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DisplayPODetail(ByVal ReceivingID As Integer)
        Try


            Dim str As String
            'Dim i As Integer
            'Before against task:2374
            'str = " SELECT  Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, Isnull(Recv_D.SampleQty,0) as SampleQty, IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float,0) as TaxAmount, isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as SalesAccountId, Recv_D.Comments  FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationID " _
            '      & " Where Recv_D.SalesID =" & ReceivingID & ""
            'Task:2374 Added Column Total Amount And Change Index Position SampleQty
            'Before against task:M16
            'str = " SELECT  Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID,  IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Total Amount],Isnull(Recv_D.SampleQty,0) as SampleQty, isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as SalesAccountId, Recv_D.Comments  FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
            '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationID " _
            '& " Where Recv_D.SalesID =" & ReceivingID & ""
            'End Task:2374
            'Before against task:2435
            'Task:M16 Added Column Engine_No And Chassis_No
            'str = " SELECT  Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID,  IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Total Amount],Isnull(Recv_D.SampleQty,0) as SampleQty, isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as SalesAccountId, Recv_D.Comments, Recv_D.Engine_No,Recv_D.Chassis_No  FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
            '    & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '    & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationID " _
            '    & " Where Recv_D.SalesID =" & ReceivingID & ""
            'End Task:M16
            'Task:2435 Added Column CostPrice
            'str = " SELECT  Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID,  IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Total Amount],Isnull(Recv_D.SampleQty,0) as SampleQty, isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as SalesAccountId, Recv_D.Comments, Recv_D.Engine_No,Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice  FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
            '    & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '    & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationID " _
            '    & " Where Recv_D.SalesID =" & ReceivingID & ""
            'str = " SELECT  Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '   & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '   & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID,  IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Total Amount],Isnull(Recv_D.SampleQty,0) as SampleQty, isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as InvAccountId, Article_Group.SalesAccountId, Article_Group.CGSAccountId,Recv_D.Comments, Recv_D.Engine_No,Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice  FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
            '   & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '   & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationID " _
            '   & " Where Recv_D.SalesID =" & ReceivingID & ""
            '  str = " SELECT  Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID,  IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Total Amount],Isnull(Recv_D.SampleQty,0) as SampleQty, isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as InvAccountId, Article_Group.SalesAccountId, Article_Group.CGSAccountId,Recv_D.Comments, Recv_D.Other_Comments as [Other Comments], Recv_D.Engine_No,Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice  FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
            '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationID " _
            '& " Where (IsNull(Recv_D.Sz1,0)-IsNull(SalesReturnQty,0)) <> 0 AND Recv_D.SalesID =" & ReceivingID & ""


            'str = " SELECT  Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(SalesReturnQty,0)) AS Qty, IsNull(Recv_D.Price,0) as Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float,0) as [Total Currency Amount], " _
            '    & " ((IsNull(Recv_D.Qty,0)-IsNull(SalesReturnTotalQty,0)) * IsNull(Recv_D.Price,0) * Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
            '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID,  IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float,0) as TaxAmount , Convert(float,0) as [Currency Tax Amount], Convert(float,0) as [Total Amount], Isnull(Recv_D.SampleQty,0) as SampleQty, isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as InvAccountId, Article_Group.SalesAccountId, Article_Group.CGSAccountId,Recv_D.Comments, Recv_D.Other_Comments as [Other Comments], Recv_D.Engine_No,Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice,Isnull(Recv_D.SaleDetailID,0) as RefSalesDetailId, IsNull(Recv_D.Qty, 0) - IsNull(Recv_D.SalesReturnTotalQty, 0) As TotalQty  FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
            '    & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '    & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationID " _
            '    & " Where (IsNull(Recv_D.Sz1,0)-IsNull(SalesReturnQty,0)) <> 0 AND Recv_D.SalesID =" & ReceivingID & ""
            ''TASK : TFS1263 Added Column SalesOrderDetailId
            ''TASK : TFS1263 include SalesOrderDetailId to be trieve to track Sales Order. ON 08-08-2017
            '' Below lines are commented against TASK : TFS1357
            'str = " SELECT  Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(SalesReturnQty,0)) AS Qty, IsNull(Recv_D.Price,0) as Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float,0) as [Total Currency Amount], " _
            '   & " ((IsNull(Recv_D.Qty,0)-IsNull(SalesReturnTotalQty,0)) * IsNull(Recv_D.Price,0) * Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
            '   & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID,  IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float,0) as TaxAmount , Convert(float,0) as [Currency Tax Amount], Convert(float,0) as [Total Amount], Isnull(Recv_D.SampleQty,0) as SampleQty, isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as InvAccountId, Article_Group.SalesAccountId, Article_Group.CGSAccountId,Recv_D.Comments, Recv_D.Other_Comments as [Other Comments], Recv_D.Engine_No,Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice,Isnull(Recv_D.SaleDetailID,0) as RefSalesDetailId, IsNull(Recv_D.Qty, 0) - IsNull(Recv_D.SalesReturnTotalQty, 0) As TotalQty, IsNull(Recv_D.SO_Detail_Id, 0) As SalesOrderDetailId FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
            '   & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '   & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationID " _
            '   & " Where (IsNull(Recv_D.Sz1,0)-IsNull(SalesReturnQty,0)) <> 0 AND Recv_D.SalesID =" & ReceivingID & ""
            '' TASK: TFS1357 Converted Qty columns to decimal from double to show end zero after points. Ameen on 22-08-2017 
            str = " SELECT  Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Recv_D.Sz1,0)-IsNull(SalesReturnQty,0)), 1) AS Qty, IsNull(Recv_D.Price,0) as Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float,0) as [Total Currency Amount], " _
               & " ((IsNull(Recv_D.Qty,0)-IsNull(SalesReturnTotalQty,0)) * IsNull(Recv_D.Price,0) * Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
               & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID,  IsNull(Recv_D.TaxPercent,0) as Tax_Percent, Convert(float,0) as TaxAmount , Convert(float,0) as [Currency Tax Amount], Convert(float,0) as [Total Amount], Isnull(Recv_D.SampleQty,0) as SampleQty, isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as InvAccountId, Article_Group.SalesAccountId, Article_Group.CGSAccountId,Recv_D.Comments, Recv_D.Other_Comments as [Other Comments], Recv_D.Engine_No,Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice,Isnull(Recv_D.SaleDetailID,0) as RefSalesDetailId, Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Recv_D.Qty, 0) - IsNull(Recv_D.SalesReturnTotalQty, 0)), 1) As TotalQty, IsNull(Recv_D.SO_Detail_Id, 0) As SalesOrderDetailId FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
               & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
               & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationID " _
               & " Where (IsNull(Recv_D.Sz1,0)-IsNull(SalesReturnQty,0)) <> 0 AND Recv_D.SalesID =" & ReceivingID & ""

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

            'grd.GetRows.Clear()
            'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

            '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)("BatchID"), objDataSet.Tables(0).Rows(0)("LocationID"), objDataSet.Tables(0).Rows(i).Item("Tax_Percent"))

            '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)
            'Next
            'DisplayDetail(-1)
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            'dtDisplayDetail.Columns.Add("TaxAmount", GetType(System.Double))
            dtDisplayDetail.AcceptChanges()
            ''Commented below row against TASK-408 to add TotalQty
            'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack', ((Qty * PackQty)* Price), (Qty * Price))"
            dtDisplayDetail.Columns("Total").Expression = "[TotalQty]*[Price]*[CurrencyRate]" ''TASK-408 on 11-06-2016
            dtDisplayDetail.Columns("TaxAmount").Expression = "((Total*Tax_Percent)/100)"
            dtDisplayDetail.Columns("Total Amount").Expression = "Total+TaxAmount" 'Task:2374 Show Total Amount 
            dtDisplayDetail.Columns("CurrencyAmount").Expression = "([TotalQty]*[Price])"
            dtDisplayDetail.Columns("Currency Tax Amount").Expression = "((CurrencyAmount*Tax_Percent)/100)"
            dtDisplayDetail.Columns("Total Currency Amount").Expression = "CurrencyAmount+[Currency Tax Amount]"
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            If dtDisplayDetail.Rows.Count > 0 Then
                If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                    'Me.cmbCurrency.SelectedValue = Nothing
                    Me.cmbCurrency.Enabled = False
                Else
                    Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                    txtCurrencyRate.Text = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString)
                    Me.cmbCurrency.Enabled = False
                End If
            End If
            ApplyGridSettings()
            GetSecurityRights()
            FillCombo("grdLocation")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayDetail(ByVal ReceivingID As Integer, Optional ByVal TypeId As Integer = -1, Optional ByVal Condition As String = "")
        Try
            Dim str As String = String.Empty
            'Dim i As Integer
            If Condition = "LoadAll" Then
                If getConfigValueByType("LoadAllItemsInSales").ToString = "True" Then
                    'str = " select Isnull(Detail.LocationID,0) as LocationID, art.ArticleCode, art.ArticleDescription as item, ISNULL(Detail.BatchNo,'xxxx') as BatchNo, ISNULL(Detail.Unit,'Loose') as Unit, ISNULL(Detail.Qty,0) as Qty, " _
                    '  & " ((ISNULL(Art.SalePrice,0) - (ISNULL(Art.SalePrice,0) * ISNULL(CustomerDiscount.Discount,0))/100)) as Price, ISNULL(Detail.Total,0) as Total, Art.ArticleGroupId, Art.ArticleId as ArticleDefId, ISNULL(Detail.PackQty,0) as PackQty, " _
                    '  & " ISNULL(Art.SalePrice,0) as CurrentPrice, Isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.BatchId,0) as BatchId, isnull(Detail.SampleQty,0) as SampleQty, ISNULL(Detail.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Isnull(art.SubSubId,0) as SalesAccountId  From ArticleDefView art LEFT OUTER JOIN ( " _
                    '  & " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                    '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                    '  & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.SampleQty,0) as SampleQty, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc  FROM dbo.SalesReturnDetailTable Recv_D INNER JOIN  " _
                    '  & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN  " _
                    '  & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  " _
                    '  & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & " " _
                    '  & " )Detail ON Detail.ArticleDefId = art.ArticleId " _
                    '  & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") CustomerDiscount On CustomerDiscount.ArticleDefId = Art.ArticleId  " _
                    '  & " WHERE Art.SalesItem = 1 " & IIf(blnEditMode = False, " AND Art.Active=1", "") & " ORDER BY Art.SortOrder Asc "
                    'R-916 Added Column Comments
                    'Before againt task:2374
                    'str = " select Isnull(Detail.LocationID,0) as LocationID, art.ArticleCode, art.ArticleDescription as item, ISNULL(Detail.BatchNo,'xxxx') as BatchNo, ISNULL(Detail.Unit,'Loose') as Unit, ISNULL(Detail.Qty,0) as Qty, " _
                    '  & " ((ISNULL(Art.SalePrice,0) - (ISNULL(Art.SalePrice,0) * ISNULL(CustomerDiscount.Discount,0))/100)) as Price, ISNULL(Detail.Total,0) as Total, Art.ArticleGroupId, Art.ArticleId as ArticleDefId, ISNULL(Detail.PackQty,0) as PackQty, " _
                    '  & " ISNULL(Art.SalePrice,0) as CurrentPrice, Isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.BatchId,0) as BatchId, isnull(Detail.SampleQty,0) as SampleQty, ISNULL(Detail.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Isnull(art.SubSubId,0) as SalesAccountId, Detail.Comments  From ArticleDefView art LEFT OUTER JOIN ( " _
                    '  & " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                    '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                    '  & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.SampleQty,0) as SampleQty, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments  FROM dbo.SalesReturnDetailTable Recv_D INNER JOIN  " _
                    '  & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN  " _
                    '  & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  " _
                    '  & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & " " _
                    '  & " )Detail ON Detail.ArticleDefId = art.ArticleId " _
                    '  & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") CustomerDiscount On CustomerDiscount.ArticleDefId = Art.ArticleId  " _
                    '  & " WHERE Art.SalesItem = 1 " & IIf(blnEditMode = False, " AND Art.Active=1", "") & " ORDER BY Art.SortOrder Asc "
                    'Task:2374 Added Column Total Amount And Change Index Position SampleQty
                    'Before against task:M16
                    'str = " select Isnull(Detail.LocationID,0) as LocationID, art.ArticleCode, art.ArticleDescription as item, ISNULL(Detail.BatchNo,'xxxx') as BatchNo, ISNULL(Detail.Unit,'Loose') as Unit, ISNULL(Detail.Qty,0) as Qty, " _
                    '  & " ((ISNULL(Art.SalePrice,0) - (ISNULL(Art.SalePrice,0) * ISNULL(CustomerDiscount.Discount,0))/100)) as Price, ISNULL(Detail.Total,0) as Total, Art.ArticleGroupId, Art.ArticleId as ArticleDefId, ISNULL(Detail.PackQty,0) as PackQty, " _
                    '  & " ISNULL(Art.SalePrice,0) as CurrentPrice, Isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.BatchId,0) as BatchId, ISNULL(Detail.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Total Amount],  isnull(Detail.SampleQty,0) as SampleQty, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Isnull(art.SubSubId,0) as SalesAccountId, Detail.Comments  From ArticleDefView art LEFT OUTER JOIN ( " _
                    '  & " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                    '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                    '  & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.SampleQty,0) as SampleQty, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments  FROM dbo.SalesReturnDetailTable Recv_D INNER JOIN  " _
                    '  & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN  " _
                    '  & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  " _
                    '  & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & " " _
                    '  & " )Detail ON Detail.ArticleDefId = art.ArticleId " _
                    '  & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") CustomerDiscount On CustomerDiscount.ArticleDefId = Art.ArticleId  " _
                    '  & " WHERE Art.SalesItem = 1 " & IIf(blnEditMode = False, " AND Art.Active=1", "") & " ORDER BY Art.SortOrder Asc "
                    'End Task:2374
                    'Before against task:2435
                    'Task:M16 Added Column Engine_No, Chassis_No.
                    'str = " select Isnull(Detail.LocationID,0) as LocationID, art.ArticleCode, art.ArticleDescription as item, ISNULL(Detail.BatchNo,'xxxx') as BatchNo, ISNULL(Detail.Unit,'Loose') as Unit, ISNULL(Detail.Qty,0) as Qty, " _
                    ' & " ((ISNULL(Art.SalePrice,0) - (ISNULL(Art.SalePrice,0) * ISNULL(CustomerDiscount.Discount,0))/100)) as Price, ISNULL(Detail.Total,0) as Total, Art.ArticleGroupId, Art.ArticleId as ArticleDefId, ISNULL(Detail.PackQty,0) as PackQty, " _
                    ' & " ISNULL(Art.SalePrice,0) as CurrentPrice, Isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.BatchId,0) as BatchId, ISNULL(Detail.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Total Amount],  isnull(Detail.SampleQty,0) as SampleQty, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Isnull(art.SubSubId,0) as SalesAccountId, Detail.Comments  From ArticleDefView art LEFT OUTER JOIN ( " _
                    ' & " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                    ' & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                    ' & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.SampleQty,0) as SampleQty, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No  FROM dbo.SalesReturnDetailTable Recv_D INNER JOIN  " _
                    ' & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN  " _
                    ' & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  " _
                    ' & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & " " _
                    ' & " )Detail ON Detail.ArticleDefId = art.ArticleId " _
                    ' & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") CustomerDiscount On CustomerDiscount.ArticleDefId = Art.ArticleId  " _
                    ' & " WHERE Art.SalesItem = 1 " & IIf(blnEditMode = False, " AND Art.Active=1", "") & " ORDER BY Art.SortOrder Asc "
                    'End Task:M16
                    'Task:2435 Added Column CostPrice
                    'Before against task:M21
                    'str = " select Isnull(Detail.LocationID,0) as LocationID, art.ArticleCode, art.ArticleDescription as item, ISNULL(Detail.BatchNo,'xxxx') as BatchNo, ISNULL(Detail.Unit,'Loose') as Unit, ISNULL(Detail.Qty,0) as Qty, " _
                    '& " ((ISNULL(Art.SalePrice,0) - (ISNULL(Art.SalePrice,0) * ISNULL(CustomerDiscount.Discount,0))/100)) as Price, ISNULL(Detail.Total,0) as Total, Art.ArticleGroupId, Art.ArticleId as ArticleDefId, ISNULL(Detail.PackQty,0) as PackQty, " _
                    '& " ISNULL(Art.SalePrice,0) as CurrentPrice, Isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.BatchId,0) as BatchId, ISNULL(Detail.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Total Amount],  isnull(Detail.SampleQty,0) as SampleQty, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Isnull(art.SubSubId,0) as SalesAccountId, Detail.Comments, Isnull(Detail.CostPrice,0) as CostPrice  From ArticleDefView art LEFT OUTER JOIN ( " _
                    '& " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                    '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                    '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.SampleQty,0) as SampleQty, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice  FROM dbo.SalesReturnDetailTable Recv_D INNER JOIN  " _
                    '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN  " _
                    '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  " _
                    '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & " " _
                    '& " )Detail ON Detail.ArticleDefId = art.ArticleId " _
                    '& " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") CustomerDiscount On CustomerDiscount.ArticleDefId = Art.ArticleId  " _
                    '& " WHERE Art.SalesItem = 1 " & IIf(blnEditMode = False, " AND Art.Active=1", "") & " ORDER BY Art.SortOrder Asc "
                    'Task:2435
                    'Task:M21 Chassis_No And Engine_No Field Missing.
                    ' str = " select Isnull(Detail.LocationID,0) as LocationID, art.ArticleCode, art.ArticleDescription as item, ISNULL(Detail.BatchNo,'xxxx') as BatchNo, ISNULL(Detail.Unit,'Loose') as Unit, ISNULL(Detail.Qty,0) as Qty, " _
                    '& " ((ISNULL(Art.SalePrice,0) - (ISNULL(Art.SalePrice,0) * ISNULL(CustomerDiscount.Discount,0))/100)) as Price, ISNULL(Detail.Total,0) as Total, Art.ArticleGroupId, Art.ArticleId as ArticleDefId, ISNULL(Detail.PackQty,0) as PackQty, " _
                    '& " ISNULL(Art.SalePrice,0) as CurrentPrice, Isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.BatchId,0) as BatchId, ISNULL(Detail.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Total Amount],  isnull(Detail.SampleQty,0) as SampleQty, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Isnull(art.SubSubId,0) as SalesAccountId, Detail.Comments, Detail.Engine_No, Detail.Chassis_No, Isnull(Detail.CostPrice,0) as CostPrice  From ArticleDefView art LEFT OUTER JOIN ( " _
                    '& " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                    '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                    '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.SampleQty,0) as SampleQty, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice  FROM dbo.SalesReturnDetailTable Recv_D INNER JOIN  " _
                    '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN  " _
                    '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  " _
                    '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & " " _
                    '& " )Detail ON Detail.ArticleDefId = art.ArticleId " _
                    '& " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") CustomerDiscount On CustomerDiscount.ArticleDefId = Art.ArticleId  " _
                    '& " WHERE Art.SalesItem = 1 " & IIf(blnEditMode = False, " AND Art.Active=1", "") & " ORDER BY Art.SortOrder Asc "
                    '      'End Task:M21

                    '      str = " select Isnull(Detail.LocationID,0) as LocationID, art.ArticleCode, art.ArticleDescription as item, ISNULL(Detail.BatchNo,'xxxx') as BatchNo, ISNULL(Detail.Unit,'Loose') as Unit, ISNULL(Detail.Qty,0) as Qty, " _
                    '& " ((ISNULL(Art.SalePrice,0) - (ISNULL(Art.SalePrice,0) * ISNULL(CustomerDiscount.Discount,0))/100)) as Price, ISNULL(Detail.Total,0) as Total, Art.ArticleGroupId, Art.ArticleId as ArticleDefId, ISNULL(Detail.PackQty,0) as PackQty, " _
                    '& " ISNULL(Art.SalePrice,0) as CurrentPrice, Isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.BatchId,0) as BatchId, ISNULL(Detail.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Total Amount],  isnull(Detail.SampleQty,0) as SampleQty, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Isnull(art.SubSubId,0) as InvAccountId, art.SalesAccountId, art.CGSAccountId, Detail.Comments, Detail.Engine_No, Detail.Chassis_No, Isnull(Detail.CostPrice,0) as CostPrice  From ArticleDefView art LEFT OUTER JOIN ( " _
                    '& " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price,  " _
                    '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,  " _
                    '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.SampleQty,0) as SampleQty, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice  FROM dbo.SalesReturnDetailTable Recv_D INNER JOIN  " _
                    '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN  " _
                    '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  " _
                    '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & " " _
                    '& " )Detail ON Detail.ArticleDefId = art.ArticleId " _
                    '& " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") CustomerDiscount On CustomerDiscount.ArticleDefId = Art.ArticleId  " _
                    '& " WHERE Art.SalesItem = 1 " & IIf(blnEditMode = False, " AND Art.Active=1", "") & " ORDER BY Art.SortOrder Asc "




                    ''TASK : TFS1263 include SalesOrderDetailId to be retrieve to track Sales Order. ON 08-08-2017
                    ''Below lines are commented against TASK: TFS1357
                    '      str = " select Isnull(Detail.LocationID,0) as LocationID, art.ArticleCode, art.ArticleDescription as item, ISNULL(Detail.BatchNo,'xxxx') as BatchNo, ISNULL(Detail.Unit,'Loose') as Unit, ISNULL(Detail.Qty,0) as Qty, " _
                    '& " ((ISNULL(Art.SalePrice,0) - (ISNULL(Art.SalePrice,0) * ISNULL(CustomerDiscount.Discount,0))/100)) as Price, Detail.BaseCurrencyId, Detail.BaseCurrencyRate, Detail.CurrencyId, Detail.CurrencyRate, Detail.CurrencyAmount, Convert(float, 0) As [Total Currency Amount], ISNULL(Detail.Total,0) as Total, Art.ArticleGroupId, Art.ArticleId as ArticleDefId, ISNULL(Detail.PackQty,0) as PackQty, " _
                    '& " ISNULL(Art.SalePrice,0) as CurrentPrice, Isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.BatchId,0) as BatchId, ISNULL(Detail.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Currency Tax Amount], Convert(float,0) as [Total Amount],  isnull(Detail.SampleQty,0) as SampleQty, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Isnull(art.SubSubId,0) as InvAccountId, art.SalesAccountId, art.CGSAccountId, Detail.Comments, Detail.[Other Comments] ,Detail.Engine_No, Detail.Chassis_No, Isnull(Detail.CostPrice,0) as CostPrice, IsNull(Detail.RefSalesDetailId, 0) As RefSalesDetailId, IsNull(Detail.Qty, 0) As TotalQty, IsNull(Detail.SalesOrderDetailId, 0) As SalesOrderDetailId From ArticleDefView art LEFT OUTER JOIN ( " _
                    '& " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, " _
                    '& " (IsNull(Recv_D.Qty, 0) * Recv_D.Price) * (Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total,  " _
                    '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.SampleQty,0) as SampleQty, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.Other_Comments as [Other Comments], IsNull(Recv_D.SalesOrderDetailId, 0) As SalesOrderDetailId  FROM dbo.SalesReturnDetailTable Recv_D INNER JOIN  " _
                    '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN  " _
                    '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  " _
                    '& " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & " " _
                    '& " )Detail ON Detail.ArticleDefId = art.ArticleId " _
                    '& " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") CustomerDiscount On CustomerDiscount.ArticleDefId = Art.ArticleId " _
                    '& " WHERE Art.SalesItem = 1 " & IIf(blnEditMode = False, " AND Art.Active=1", "") & " ORDER BY Art.SortOrder Asc "
                    '' TASK: TFS1357 Converted Qty columns to decimal from double to show end zero after points. Ameen on 22-08-2017 
                    'Ali Faisal : TFS1634 : Altered to set the zero values of columns TotalQty and Currency columns if have null values
                    str = " select Isnull(Detail.LocationID,0) as LocationID, art.ArticleCode, art.ArticleDescription as item, ISNULL(Detail.BatchNo,'xxxx') as BatchNo, ISNULL(Detail.Unit,'Loose') as Unit, Convert(Decimal(18, " & DecimalPointInQty & "), ISNULL(Detail.Qty,0), 1) as Qty, " _
         & " ((ISNULL(Art.SalePrice,0) - (ISNULL(Art.SalePrice,0) * ISNULL(CustomerDiscount.Discount,0))/100)) as Price, IsNull(Detail.BaseCurrencyId,0) BaseCurrencyId, IsNull(Detail.BaseCurrencyRate,0) BaseCurrencyRate, IsNull(Detail.CurrencyId,0) CurrencyId, IsNull(Detail.CurrencyRate,0) CurrencyRate, IsNull(Detail.CurrencyAmount,0) CurrencyAmount, Convert(float, 0) As [Total Currency Amount], ISNULL(Detail.Total,0) as Total, Art.ArticleGroupId, Art.ArticleId as ArticleDefId, ISNULL(Detail.PackQty,0) as PackQty, " _
         & " ISNULL(Art.SalePrice,0) as CurrentPrice, Isnull(Detail.PackPrice,0) as PackPrice, ISNULL(Detail.BatchId,0) as BatchId, ISNULL(Detail.Tax_Percent,0) as Tax_Percent, Convert(float,0) as TaxAmount, Convert(float,0) as [Currency Tax Amount], Convert(float,0) as [Total Amount],  isnull(Detail.SampleQty,0) as SampleQty, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Isnull(art.SubSubId,0) as InvAccountId, art.SalesAccountId, art.CGSAccountId, Detail.Comments, Detail.[Other Comments] ,Detail.Engine_No, Detail.Chassis_No, Isnull(Detail.CostPrice,0) as CostPrice, IsNull(Detail.RefSalesDetailId, 0) As RefSalesDetailId, Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Detail.Qty, 0), 1) As TotalQty, IsNull(Detail.SalesOrderDetailId, 0) As SalesOrderDetailId From ArticleDefView art LEFT OUTER JOIN ( " _
         & " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, " _
         & " (IsNull(Recv_D.Qty, 0) * Recv_D.Price) * (Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total,  " _
         & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.SampleQty,0) as SampleQty, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice, Recv_D.Other_Comments as [Other Comments], IsNull(Recv_D.SalesOrderDetailId, 0) As SalesOrderDetailId ,IsNull(Recv_D.RefSalesDetailId, 0) As RefSalesDetailId  FROM dbo.SalesReturnDetailTable Recv_D INNER JOIN  " _
         & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN  " _
         & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId  " _
         & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & " " _
         & " )Detail ON Detail.ArticleDefId = art.ArticleId " _
         & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") CustomerDiscount On CustomerDiscount.ArticleDefId = Art.ArticleId " _
         & " WHERE Art.SalesItem = 1 " & IIf(blnEditMode = False, " AND Art.Active=1", "") & " ORDER BY Art.SortOrder Asc "
                    'Ali Faisal : TFS1634 : End
                End If
            ElseIf Condition = String.Empty Then
                'str = " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
                '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
                '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.SampleQty,0) as SampleQty, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Convert(float, 0) as TaxAmount, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as SalesAccountId   FROM dbo.SalesReturnDetailTable Recv_D LEFT OUTER JOIN " _
                '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
                '      & " LEFT OUTER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & ""

                'R-916 Added Column Comments
                'Before gainst task:2374
                'str = " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
                '        & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
                '        & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.SampleQty,0) as SampleQty, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Convert(float, 0) as TaxAmount, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as SalesAccountId,Recv_D.Comments   FROM dbo.SalesReturnDetailTable Recv_D LEFT OUTER JOIN " _
                '        & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '        & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
                '        & " LEFT OUTER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & "".
                'Task:2374 Added Column Total Amount And Change Index Position SampleQty
                'Before against task:M16
                '    str = " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
                '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
                '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Convert(float, 0) as TaxAmount, Convert(float,0) as [Total Amount], IsNull(Recv_D.SampleQty,0) as SampleQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as SalesAccountId,Recv_D.Comments   FROM dbo.SalesReturnDetailTable Recv_D LEFT OUTER JOIN " _
                '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
                '& " LEFT OUTER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & ""
                'End Task:2374
                'Before against task:2435
                ''Task:M16 Added Column Engine_No, Chassis_No
                'str = " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
                '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
                '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Convert(float, 0) as TaxAmount, Convert(float,0) as [Total Amount], IsNull(Recv_D.SampleQty,0) as SampleQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as SalesAccountId,Recv_D.Comments,Recv_D.Engine_No, Recv_D.Chassis_No   FROM dbo.SalesReturnDetailTable Recv_D LEFT OUTER JOIN " _
                '    & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '    & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
                '    & " LEFT OUTER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & ""
                'End Task:M16

                'Task:2435 Added Column CostPrice
                'str = " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
                '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
                '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Convert(float, 0) as TaxAmount, Convert(float,0) as [Total Amount], IsNull(Recv_D.SampleQty,0) as SampleQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as SalesAccountId,Recv_D.Comments,Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice   FROM dbo.SalesReturnDetailTable Recv_D LEFT OUTER JOIN " _
                '    & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '    & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
                '    & " LEFT OUTER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & ""
                'End Task:2435

                '  str = " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
                '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
                '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Convert(float, 0) as TaxAmount, Convert(float,0) as [Total Amount], IsNull(Recv_D.SampleQty,0) as SampleQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as InvAccountId,Article_Group.SalesAccountId, Article_Group.CGSAccountId,Recv_D.Comments,Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice   FROM dbo.SalesReturnDetailTable Recv_D LEFT OUTER JOIN " _
                '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
                '& " LEFT OUTER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & ""

                '       str = " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
                '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
                '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Convert(float, 0) as TaxAmount, Convert(float,0) as [Total Amount], IsNull(Recv_D.SampleQty,0) as SampleQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as InvAccountId,Article_Group.SalesAccountId, Article_Group.CGSAccountId,Recv_D.Comments, Recv_D.Other_Comments as [Other Comments],Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice   FROM dbo.SalesReturnDetailTable Recv_D LEFT OUTER JOIN " _
                '& " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '& " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
                '& " LEFT OUTER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & ""
                ''TASK : TFS1263 include SalesOrderDetailId to be retrieve to track Sales Order. ON 08-08-2017
                ''Below line are commented against TASK:22-08-2017
                'str = " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As  CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount , Convert(float, 0) As [Total Currency Amount], " _
                '    & " IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * (Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
                '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Convert(float, 0) as TaxAmount, Convert(float, 0) as [Currency Tax Amount], Convert(float,0) as [Total Amount], IsNull(Recv_D.SampleQty,0) as SampleQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as InvAccountId,Article_Group.SalesAccountId, Article_Group.CGSAccountId,Recv_D.Comments, Recv_D.Other_Comments as [Other Comments],Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice, IsNull(Recv_D.RefSalesDetailId,0) as RefSalesDetailId, IsNull(Recv_D.Qty, 0) As TotalQty, IsNull(Recv_D.SalesOrderDetailId, 0) As SalesOrderDetailId FROM dbo.SalesReturnDetailTable Recv_D LEFT OUTER JOIN " _
                '    & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                '    & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
                '    & " LEFT OUTER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & ""
                '' TASK: TFS1357 Converted Qty columns to decimal from double to show end zero after points. Ameen on 22-08-2017 
                str = " SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Convert(Decimal(18, " & DecimalPointInQty & "), Recv_D.Sz1, 1) AS Qty, Recv_D.Price, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As  CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount , Convert(float, 0) As [Total Currency Amount], " _
                   & " IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * (Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
                   & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Convert(float, 0) as TaxAmount, Convert(float, 0) as [Currency Tax Amount], Convert(float,0) as [Total Amount], IsNull(Recv_D.SampleQty,0) as SampleQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as InvAccountId,Article_Group.SalesAccountId, Article_Group.CGSAccountId,Recv_D.Comments, Recv_D.Other_Comments as [Other Comments],Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice, IsNull(Recv_D.RefSalesDetailId,0) as RefSalesDetailId, Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Recv_D.Qty, 0), 1) As TotalQty, IsNull(Recv_D.SalesOrderDetailId, 0) As SalesOrderDetailId FROM dbo.SalesReturnDetailTable Recv_D LEFT OUTER JOIN " _
                   & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                   & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
                   & " LEFT OUTER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & ReceivingID & ""


            End If
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

            '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo").ToString, objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)("BatchID"), objDataSet.Tables(0).Rows(i)("LocationID"), objDataSet.Tables(0).Rows(i).Item("Tax_Percent"))

            '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

            'Next
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            'dtDisplayDetail.Columns.Add("TaxAmount", GetType(System.Double))
            dtDisplayDetail.AcceptChanges()
            ''Commented below row to add TotalQty against TASK-408 on 11-06-2016
            'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack', ((Qty * PackQty)* Price), (Qty * Price))"
            dtDisplayDetail.Columns("Total").Expression = "([TotalQty]*[Price]*[CurrencyRate])" ''TASK-408
            dtDisplayDetail.Columns("TaxAmount").Expression = "((Total*Tax_Percent)/100)"
            dtDisplayDetail.Columns("Total Amount").Expression = "Total+TaxAmount" 'Task:2370 Show Total Amount
            dtDisplayDetail.Columns("CurrencyAmount").Expression = "([TotalQty]*[Price])"
            dtDisplayDetail.Columns("Currency Tax Amount").Expression = "((CurrencyAmount*Tax_Percent)/100)"
            dtDisplayDetail.Columns("Total Currency Amount").Expression = "CurrencyAmount+[Currency Tax Amount]"
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            If dtDisplayDetail.Rows.Count > 0 Then
                If IsDBNull(dtDisplayDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                    'Me.cmbCurrency.SelectedValue = Nothing
                    Me.cmbCurrency.Enabled = False
                Else
                    Me.cmbCurrency.SelectedValue = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyId").ToString)
                    txtCurrencyRate.Text = Val(dtDisplayDetail.Rows.Item(0).Item("CurrencyRate").ToString)
                    Me.cmbCurrency.Enabled = False
                End If
            End If
            ApplyGridSettings()
            GetSecurityRights()
            FillCombo("grdLocation")
            If flgLoadAllItems = True Then
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    If Me.grd.RowCount > 0 Then
                        r.BeginEdit()
                        r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
                        r.EndEdit()
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function Update_Record() As Boolean
        'If Not Me.chkPost.Checked = True Then
        '    If Me.chkPost.Visible = False Then
        '        Me.chkPost.Checked = False
        '    End If
        'End If

        If ApprovalProcessId = 0 Then
            'Start TFS3113 :Ayesha Rehman
            If Me.chkPost.Visible = False Then
                Me.chkPost.Checked = False
            End If
        Else
            Me.chkPost.Visible = False
        End If
        ''End TFS3113




        Me.grd.UpdateData()
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        'Dim cmbProject.SelectedValue As Integer = GetCostCenterId(Me.cmbCompany.SelectedValue)
        Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo.Text)
        Dim AccountId As Integer = getConfigValueByType("SalesCreditAccount")
        'Dim SalesTaxAccountId As Integer = getConfigValueByType("SalesTaxCreditAccount")
        Dim SalesTaxAccountId As Integer = 0I
        If Me.cmbCompany.SelectedValue > 0 Then
            Dim str As String = "SELECT SalesTaxAccountId FROM CompanyDefTable WHERE CompanyId = " & cmbCompany.SelectedValue & "  AND SalesTaxAccountId IS NOT NULL"
            Dim dt1 As DataTable = GetDataTable(str)
            If dt1.Rows.Count > 0 Then
                SalesTaxAccountId = dt1.Rows(0).Item(0)
            End If
        End If
        If SalesTaxAccountId = 0 Then
            SalesTaxAccountId = Val(getConfigValueByType("SalesTaxCreditAccount").ToString) 'GetConfigValue("SalesTaxCreditAccount")
        End If
        'Dim SalesOrderAnalysis As Boolean = False
        'If Not GetConfigValue("SalesOrderAnalysis").ToString = "Error" Then
        '    SalesOrderAnalysis = Convert.ToBoolean(GetConfigValue("SalesOrderAnalysis").ToString)
        'End If
        If Not getConfigValueByType("OtherExpAccount").ToString = "Error" Then
            MarketReturnAccountId = CInt(getConfigValueByType("OtherExpAccount"))
        Else
            MarketReturnAccountId = 0
        End If
        If Not getConfigValueByType("MarketReturnVoucher").ToString = "Error" Then
            flgMarketReturnVoucher = Convert.ToBoolean(getConfigValueByType("MarketReturnVoucher").ToString)
        Else
            flgMarketReturnVoucher = False
        End If
        Dim flgCgsVoucher As Boolean = False
        If Not getConfigValueByType("CGSVoucher").ToString = "Error" Then
            flgCgsVoucher = getConfigValueByType("CGSVoucher")
        End If
        Dim InvAccountId As Integer = Val(getConfigValueByType("InvAccountId").ToString) 'GetConfigValue("InvAccountId") 'Inventory Account
        Dim CgsAccountId As Integer = Val(getConfigValueByType("CGSAccountId").ToString)
        Dim GLAccountArticleDepartment As Boolean
        If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
            GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
        Else
            GLAccountArticleDepartment = False
        End If
        Dim strVoucherNo As String = String.Empty
        Dim MarketReturnsRate As Double = 0D
        Dim dt As DataTable = GetRecords("SELECT voucher_no   FROM tblVoucher  WHERE voucher_id = " & lngVoucherMasterId & " ")

        If Not dt Is Nothing Then
            If Not dt.Rows.Count = 0 Then
                strVoucherNo = dt.Rows(0)("voucher_no")
            End If
        End If
        gobjLocationId = Me.cmbCompany.SelectedValue

        'Validation on configuration account
        '25-9-2013 by imran lia...
        '
        If AccountId <= 0 Then
            ShowErrorMessage("Sales account is not map.")
            Me.dtpPODate.Focus()
            Return False
        End If
        If Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) <> 0 Then
            If SalesTaxAccountId <= 0 Then
                ShowErrorMessage("Tax account is not map.")
                Me.dtpPODate.Focus()
                Return False
            End If
        End If
        If flgMarketReturnVoucher = True Then
            If MarketReturnAccountId <= 0 Then
                ShowErrorMessage("Market return account is not map.")
                Me.dtpPODate.Focus()
                Return False
            End If
        End If
        If flgCgsVoucher = True Then
            If InvAccountId <= 0 Then
                ShowErrorMessage("Purchase account is not map.")
                Me.dtpPODate.Focus()
                Return False
            ElseIf CgsAccountId <= 0 Then
                ShowErrorMessage("Cost of good sold account is not map.")
                Me.dtpPODate.Focus()
                Return False
            End If
        End If

        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text
            Dim SalesOrderId As Integer = 0I
            Try
                ''SalesOrderId = GetDataTable("Select isnull(POId,0) as POId From SalesMasterTable WHERE SalesNo=" & IIf(Me.cmbPo.SelectedIndex > 0, "N'" & Me.cmbPo.Text & "'", "''"), trans).Rows(0).Item(0)

                If Not Me.cmbPo.SelectedIndex = -1 Then
                    SalesOrderId = Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("POId").ToString)
                End If

                'Start TFS4232
                objCommand.Transaction = trans
                objCommand.CommandText = String.Empty
                objCommand.CommandText = "Select ArticleDefId, Isnull(Sz1,0) as Qty, Isnull(Qty,0) as TotalQty, IsNull(SalesOrderDetailId, 0) As SalesOrderDetailId From SalesReturnDetailTable WHERE SalesReturnId In(Select SalesReturnId From SalesReturnMasterTable WHERE SalesReturnId=" & Val(Me.grdSaved.CurrentRow.Cells("SalesReturnId").Value.ToString) & ") "
                Dim da As New OleDbDataAdapter
                Dim dtRet As New DataTable
                da.SelectCommand = objCommand
                da.Fill(dtRet)
                dtRet.AcceptChanges()

                For Each r As DataRow In dtRet.Rows
                    r.BeginEdit()
                    objCommand.CommandText = String.Empty
                    objCommand.CommandText = "Update SalesOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) + " & r.Item(1) & "), DeliveredTotalQty = abs(Isnull(DeliveredTotalQty,0) + " & r.Item(2) & ") where SalesOrderID = " & SalesOrderId & " and ArticleDefID = " & r.Item(0) & " And SalesOrderDetailId = " & r.Item(3) & ""
                    objCommand.ExecuteNonQuery()
                    r.EndEdit()
                Next
                'End TFS4232

                'objCommand.Transaction = trans
                'objCommand.CommandText = "Select ArticleDefId, Isnull(Qty,0) as Qty From SalesReturnDetailTable WHERE SalesReturnId In(Select SalesReturnId From SalesReturnMasterTable WHERE SalesReturnId=" & Val(Me.txtReceivingID.Text) & ") "
                'Dim da As New OleDbDataAdapter
                'Dim dtRet As New DataTable
                'da.SelectCommand = objCommand
                'da.Fill(dtRet)

                'For Each r As DataRow In dtRet.Rows
                '    r.BeginEdit()
                '    'objCommand.CommandText = "Update SalesOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) + " & r.Item(1) & ") where SalesOrderID = " & SalesOrderId & " and ArticleDefID = " & r.Item(0) & ""
                '    objCommand.CommandText = "Update SalesOrderDetailTable set DeliveredTotalQty = abs(Isnull(DeliveredTotalQty,0) + " & r.Item(1) & ") where SalesOrderID = " & SalesOrderId & " and ArticleDefID = " & r.Item(0) & "" ''TASK-408 replaced DeliveredQty with DeliveredTotalQty

                '    objCommand.ExecuteNonQuery()
                '    r.EndEdit()
                'Next
            Catch ex As Exception

            End Try

            Dim dblAdjustment As Double = 0D
            dblAdjustment = Math.Abs(((Val(Me.txtAdjPercent.Text) * Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum))) / 100)) + Math.Abs(Val(Me.txtAdjustment.Text))
            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            objCommand.CommandText = "Update SalesReturnMasterTable set LocationId=" & gobjLocationId & ", SalesReturnNo =N'" & txtPONo.Text & "',SalesReturnDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',CustomerCode=" & cmbVendor.ActiveRow.Cells(0).Value & ", PoID=" & IIf(Me.cmbPo.SelectedIndex = -1, 0, Me.cmbPo.SelectedValue) & ",EmployeeCode=" & Me.cmbSalesPerson.SelectedValue & ", " _
            & " SalesReturnQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",SalesReturnAmount=" & (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) - dblAdjustment) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UpdateUserName=N'" & LoginUserName & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", AdjPercent=" & Val(Me.txtAdjPercent.Text) & ", Adjustment=" & Val(Me.txtAdjustment.Text) & ", Damage_Budget=" & Val(Me.txtDamageBudget.Text) & ", CostCenterId = " & Me.cmbProject.SelectedValue & ", BillMakerId = " & Me.cmbBillMaker.SelectedValue & ", PackingManId = " & Me.cmbPackingMan.SelectedValue & " Where SalesReturnID= " & txtReceivingID.Text & " " '' TASK-408 replaced Qty with TotalQty on 11-06-2016

            objCommand.ExecuteNonQuery()

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

            'TASKM106151 Partially Sales Return Qty Minus In Sales Detail Table
            If Not Me.cmbPo.SelectedIndex = -1 And Me.cmbPo.SelectedIndex >= 0 Then
                DeleteInvoicePartialReturn(Val(Me.txtReceivingID.Text), Val(Me.cmbPo.SelectedValue), objCommand, trans)
            End If
            'End TASKM106151


            objCommand.CommandText = "Delete from SalesReturnDetailTable where SalesReturnID = " & txtReceivingID.Text
            objCommand.ExecuteNonQuery()



            'If SalesOrderAnalysis = True Then
            '    Try
            '        objCommand.CommandText = "Delete from MarketReturnsDetailTable where SalesReturnID = " & txtReceivingID.Text
            '        objCommand.ExecuteNonQuery()
            '    Catch ex As Exception

            '    End Try
            'End If
            'Before against task:M101
            'objCommand.CommandText = "update tblVoucher set voucher_date=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & "" _
            '                        & "   where voucher_id=" & lngVoucherMasterId
            'TAsk:M101 Added Field Remarks
            ''TASK TFS1427 Added User Name 
            objCommand.CommandText = "update tblVoucher set voucher_date=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", Remarks=N'" & Me.txtRemarks.Text.Replace("'", "''") & "', Posted_UserName= " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & " " _
                                            & "   where voucher_id=" & lngVoucherMasterId
            'End Task:M101
            objCommand.ExecuteNonQuery()

            '***********************
            'Deleting Detail
            '***********************
            objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
            objCommand.ExecuteNonQuery()

            Dim LocationType As String = String.Empty
            Dim dblAdj As Double = 0D
            Dim dblNet As Double = 0D
            Dim dblDmgBudget As Double = 0D
            dblDmgBudget = (Val(Me.txtDamageBudget.Text) / Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) ''TASK-408 replaced Qty with TotalQty on 11-06-2016
            dblNet = (((Val(Me.txtAdjPercent.Text) * Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum))) / 100)) + Math.Abs(Val(Me.txtAdjustment.Text))
            dblAdj = Math.Abs(((Val(Me.txtAdjPercent.Text) * Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum))) / 100)) + Math.Abs(Val(Me.txtAdjustment.Text))
            dblAdj = (dblAdj / Me.grd.RecordCount)
            Dim dblMarketReturns As Double = 0D
            Dim CrrStock As Double = 0D
            Dim CostPrice As Double = 0D
            StockList = New List(Of StockDetail)
            For i = 0 To grd.RowCount - 1
                If Val(grd.GetRows(i).Cells("TotalQty").Value) <> 0 Or Val(Me.grd.GetRows(i).Cells("SampleQty").Value) <> 0 Then

                    If GLAccountArticleDepartment = True Then
                        'Before against task:2390
                        'AccountId = Val(grd.GetRows(i).Cells("SalesAccountId").Value.ToString)
                        'Task:2390 Change Inventory Account
                        InvAccountId = Val(grd.GetRows(i).Cells("InvAccountId").Value.ToString)
                        'End Task:2390
                        AccountId = Val(grd.GetRows(i).Cells("SalesAccountId").Value.ToString)
                        CgsAccountId = Val(grd.GetRows(i).Cells("CGSAccountId").Value.ToString)
                    End If

                    ''13-Spe-2014 Task:2843 Imran Ali Restriction Duplicate Egine No/Chassis No In Sales Return/Delivery Chalan
                    If flgVehicleIdentificationInfo = True Then
                        objCommand.CommandText = ""
                        objCommand.CommandText = "SELECT dbo.SalesDetailTable.ArticleDefId, SalesDetailTable.Engine_No, SalesDetailTable.Chassis_No  " _
                                                        & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                        & " dbo.SalesDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesDetailTable.ArticleDefId INNER JOIN SalesMasterTable On SalesMasterTable.SalesId = SalesDetailTable.SalesId WHERE dbo.ArticleDefTable.ArticleId <> 0"
                        If Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString.Length > 0 Then
                            objCommand.CommandText += " AND SalesDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString & "'"
                        End If
                        If Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString.Length > 0 Then
                            objCommand.CommandText += " AND SalesDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString & "'"
                        End If
                        Dim dtVehicleIdentificationInfo As New DataTable
                        Dim daVehicleIdentificationInfo As New OleDbDataAdapter(objCommand)
                        daVehicleIdentificationInfo.Fill(dtVehicleIdentificationInfo)

                        objCommand.CommandText = ""
                        objCommand.CommandText = "SELECT dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Engine_No, SalesReturnDetailTable.Chassis_No  " _
                                                        & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                        & " dbo.SalesReturnDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesReturnDetailTable.ArticleDefId INNER JOIN SalesReturnMasterTable On SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId  WHERE dbo.ArticleDefTable.ArticleId <> 0 AND SalesReturnNo <> N'" & Me.txtPONo.Text.Replace("'", "''") & "'"
                        'If Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString.Length > 0 Then
                        objCommand.CommandText += " AND SalesReturnDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString & "'"
                        'End If
                        If Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString.Length > 0 Then
                            objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString & "'"
                        End If
                        Dim dtSalesReturnVehichleInfo As New DataTable
                        Dim daSalesReturnVehichleInfo As New OleDbDataAdapter(objCommand)
                        daSalesReturnVehichleInfo.Fill(dtSalesReturnVehichleInfo)
                        If dtVehicleIdentificationInfo IsNot Nothing Then
                            'If dtVehicleIdentificationInfo.Rows.Count > 0 Then
                            If dtVehicleIdentificationInfo.Rows.Count > 0 Then
                                If dtSalesReturnVehichleInfo.Rows.Count > dtVehicleIdentificationInfo.Rows.Count Then
                                    If dtVehicleIdentificationInfo.Rows(0).Item("Engine_No").ToString.Length > 0 Or Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString.Length > 0 Then
                                        If Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString = dtVehicleIdentificationInfo.Rows(0).Item("Engine_No").ToString Then
                                            Throw New Exception("Engine no [" & Me.grd.GetRows(i).Cells(GrdEnum.Engine_No).Value.ToString & "] already exists")
                                        End If
                                    End If
                                    If Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString.Length > 0 Or dtVehicleIdentificationInfo.Rows(0).Item("Chassis_No").ToString.Length > 0 Then
                                        If Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString = dtVehicleIdentificationInfo.Rows(0).Item("Chassis_No").ToString Then
                                            Throw New Exception("Chassis no [" & Me.grd.GetRows(i).Cells(GrdEnum.Chassis_No).Value.ToString & "] already exists")
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                    'objCommand.CommandText = ""
                    'objCommand.CommandText = "Select StockTransDetailId From StockDetailTable Where StockTransId In(Select StockTransId From StockMasterTable WHERE DocNo='" & Me.txtPONo.Text & "')"
                    'Dim intCurrentStockTransDetailId As Integer = objCommand.ExecuteScalar

                    'Dim dtLastReturnData As DataTable = GetDataTable("Select IsNull(Rate,0) as Rate, IsNull(OutQty,0) as OutQty, IsNull(StockMasterTable.StockTransId,0) as StockTransId,IsNull(StockTransDetailId,0) as StockTransDetailId From StockDetailTable INNER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefId=" & Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & " AND LEFT(StockMasterTable.DocNo,2) ='SI' AND IsNull(OutQty,0) <> 0 AND StockTransDetailId <= " & intCurrentStockTransDetailId & "  ORDER BY Convert(DateTime,StockMasterTable.DocDate,102),StockDetailTable.StockTransDetailId DESC ", trans)
                    'dtLastReturnData.AcceptChanges()
                    'Dim remainReturnQty As Double = 0D
                    'If dtLastReturnData Is Nothing Then Return 0
                    'Dim dblActualReturn As Double = 0D
                    'Dim dblTotalQty As Double = 0D

                    'If dtLastReturnData.Rows.Count > 0 Then
                    '    For Each r As DataRow In dtLastReturnData.Rows
                    '        If dblTotalQty = Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)))) Then
                    '            Exit For
                    '        End If
                    '        If remainReturnQty > 0 Then
                    '            If Val(r.Item("OutQty").ToString) <= remainReturnQty Then
                    '                dblActualReturn = Val(r.Item("OutQty").ToString)
                    '                remainReturnQty = Val(r.Item("OutQty").ToString) - dblActualReturn
                    '                CostPrice = Val(r.Item("Rate").ToString)
                    '            Else
                    '                dblActualReturn = remainReturnQty
                    '                CostPrice = Val(r.Item("Rate").ToString)
                    '            End If
                    '        Else
                    '            If Val(r.Item("OutQty").ToString) >= Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)))) Then
                    '                dblActualReturn = Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value))))
                    '                CostPrice = Val(r.Item("Rate").ToString)
                    '            Else
                    '                remainReturnQty = (Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)))) - Val(r.Item("OutQty").ToString))
                    '                dblActualReturn = (Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value))))) - remainReturnQty
                    '                CostPrice = Val(r.Item("Rate").ToString)
                    '            End If
                    '        End If

                    '        'End If
                    '        StockDetail = New StockDetail
                    '        StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                    '        StockDetail.LocationId = grd.GetRows(i).Cells("LocationId").Value
                    '        StockDetail.ArticleDefId = grd.GetRows(i).Cells("ArticleDefId").Value
                    '        StockDetail.InQty = dblActualReturn 'IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value))) - remainReturnQty
                    '        StockDetail.OutQty = 0
                    '        StockDetail.Rate = IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)
                    '        StockDetail.InAmount = StockDetail.InQty * StockDetail.Rate  'IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)), (((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)))
                    '        StockDetail.OutAmount = 0
                    '        StockDetail.Remarks = String.Empty
                    '        'Task:M16 Set Values In Engine_No and Chassis_No 
                    '        StockDetail.Engine_No = grd.GetRows(i).Cells("Engine_No").Value.ToString
                    '        StockDetail.Chassis_No = grd.GetRows(i).Cells("Chassis_No").Value.ToString
                    '        'End Task:M16
                    '        StockList.Add(StockDetail)
                    '        dblTotalQty += dblActualReturn
                    '    Next
                    'Else

                    'CostPrice = GetAvgRateByItem(Val(grd.GetRows(i).Cells("ArticleDefId").Value))
                    Dim dblPurchasePrice As Double = 0D
                    Dim dblCostPrice As Double = 0D
                    Dim dblCurrencyAmount As Double = 0D

                    Dim StockMasterId As Integer
                    If flgAvgRate = True Then
                        Dim strserviceitem As String = "Select ServiceItem from ArticleDefView where ArticleId = " & Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ""
                        Dim dt2serviceitem As DataTable = GetDataTable(strserviceitem, trans)
                        dt2serviceitem.AcceptChanges()
                        Dim ServiceItem1 As Boolean = Val(dt2serviceitem.Rows(0).Item("ServiceItem").ToString)
                        If ServiceItem1 = False Then
                            Dim strstockmasterstring As String = "Select StockTransId from StockMasterTable where DocNo = '" & Me.cmbPo.Text & "'"
                            Dim dtstockid As DataTable = GetDataTable(strstockmasterstring, trans)
                            If dtstockid.Rows.Count > 0 Then
                                StockMasterId = Val(dtstockid.Rows(0).Item("StockTransId").ToString)
                            End If
                            Dim strdata As String = "Select Cost_Price from StockDetailTable where  StockTransId = " & StockMasterId & " AND ArticleDefId = " & Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & ""
                            Dim dtcostprice As DataTable = GetDataTable(strdata, trans)
                            If dtstockid.Rows.Count > 0 Then
                                dblCostPrice = Val(dtcostprice.Rows(0).Item("Cost_Price").ToString)
                            End If
                        End If
                    Else
                        Dim strPriceData() As String = GetRateByItem(Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)).Split(",")

                        If strPriceData.Length > 1 Then
                            dblCostPrice = Val(strPriceData(0).ToString)
                            dblPurchasePrice = Val(strPriceData(1).ToString)
                            dblCurrencyAmount = Val(strPriceData(2).ToString)
                            If dblCostPrice = 0 Then
                                dblCostPrice = dblPurchasePrice
                            End If
                        End If
                    End If

                    'Dim dblPurchasePrice As Double = 0D
                    'Dim dblCostPrice As Double = 0D

                    'Dim strPriceData() As String = GetRateByItem(Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString)).Split(",")

                    'If strPriceData.Length > 1 Then
                    '    dblCostPrice = Val(strPriceData(0).ToString)
                    '    dblPurchasePrice = Val(strPriceData(1).ToString)
                    'End If

                    If flgAvgRate = True Then

                        ''    Try
                        '    objCommand.CommandText = ""
                        '    'Before against task:2712
                        '    'objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))) as Amount  " _
                        '    '                                & " FROM dbo.ArticleDefTable INNER JOIN " _
                        '    '                                & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & grd.GetRows(i).Cells("ArticleDefId").Value & " " _
                        '    '                                & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                        '    'Task:2712 Rounded Amount
                        '    objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, Round(ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))),1) as Amount  " _
                        '                                   & " FROM dbo.ArticleDefTable INNER JOIN " _
                        '                                   & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & grd.GetRows(i).Cells("ArticleDefId").Value & " " _
                        '                                   & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                        '    'End Task:2712
                        '    Dim dtCrrStock As New DataTable
                        '    Dim daCrrStock As New OleDbDataAdapter(objCommand)
                        '    daCrrStock.Fill(dtCrrStock)

                        '    If dtCrrStock IsNot Nothing Then
                        '        If dtCrrStock.Rows.Count > 0 Then
                        '            'Before against task:2401
                        '            'If Val(grd.GetRows(i).Cells("Price").Value) <> 0 Then
                        '            If Val(grd.GetRows(i).Cells("Price").Value) <> 0 AndAlso Val(dtCrrStock.Rows(0).Item("qty").ToString) <> 0 Then
                        '                'End Task:2401
                        '                CrrStock = dtCrrStock.Rows(0).Item(2)
                        '                CostPrice = IIf(Val(dtCrrStock.Rows(0).Item(3).ToString) = 0, 0, dtCrrStock.Rows(0).Item(3) / CrrStock)
                        '            Else
                        '                CostPrice = Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString)
                        '            End If
                        '        Else
                        '            CostPrice = Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString)
                        '        End If
                        '    End If
                        'Catch ex As Exception

                        'End Try



                        'Dim dtLastReturnData As DataTable = GetDataTable("Select IsNull(Rate,0) as Rate, IsNull(OutQty,0) as OutQty, IsNull(StockMasterTable.StockTransId,0) as StockTransId,IsNull(StockTransDetailId,0) as StockTransDetailId From StockDetailTable INNER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefId=" & Val(Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString) & " AND LEFT(StockMasterTable.DocNo,2) ='SI' AND IsNull(OutQty,0) <> 0 ORDER BY Convert(DateTime,StockMasterTable.DocDate,102),StockDetailTable.StockTransDetailId DESC ", trans)
                        'dtLastReturnData.AcceptChanges()
                        'Dim remainReturnQty As Double = 0D
                        'If dtLastReturnData Is Nothing Then Return 0
                        'Dim dblActualReturn As Double = 0D
                        'Dim dblTotalQty As Double = 0D

                        'If dtLastReturnData.Rows.Count > 0 Then
                        '    For Each r As DataRow In dtLastReturnData.Rows
                        '        If dblTotalQty = Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)))) Then
                        '            Exit For
                        '        End If
                        '        If remainReturnQty > 0 Then
                        '            If Val(r.Item("OutQty").ToString) <= remainReturnQty Then
                        '                dblActualReturn = Val(r.Item("OutQty").ToString)
                        '                remainReturnQty = Val(r.Item("OutQty").ToString) - dblActualReturn
                        '                CostPrice = Val(r.Item("Rate").ToString)
                        '            Else
                        '                'Val(r.Item("OutQty").ToString) >= remainReturnQty 
                        '                dblActualReturn = remainReturnQty
                        '                CostPrice = Val(r.Item("Rate").ToString)
                        '            End If
                        '        Else
                        '            If Val(r.Item("OutQty").ToString) >= Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)))) Then
                        '                dblActualReturn = Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value))))
                        '                CostPrice = Val(r.Item("Rate").ToString)
                        '            Else
                        '                remainReturnQty = (Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)))) - Val(r.Item("OutQty").ToString))
                        '                dblActualReturn = (Val(IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value))))) - remainReturnQty
                        '                CostPrice = Val(r.Item("Rate").ToString)
                        '            End If
                        '        End If

                        '        'End If
                        '        StockDetail = New StockDetail
                        '        StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                        '        StockDetail.LocationId = grd.GetRows(i).Cells("LocationId").Value
                        '        StockDetail.ArticleDefId = grd.GetRows(i).Cells("ArticleDefId").Value
                        '        StockDetail.InQty = dblActualReturn 'IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value))) - remainReturnQty
                        '        StockDetail.OutQty = 0
                        '        StockDetail.Rate = IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)
                        '        StockDetail.InAmount = StockDetail.InQty * StockDetail.Rate  'IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)), (((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)))
                        '        StockDetail.OutAmount = 0
                        '        StockDetail.Remarks = String.Empty
                        '        'Task:M16 Set Values In Engine_No and Chassis_No 
                        '        StockDetail.Engine_No = grd.GetRows(i).Cells("Engine_No").Value.ToString
                        '        StockDetail.Chassis_No = grd.GetRows(i).Cells("Chassis_No").Value.ToString
                        '        'End Task:M16
                        '        StockList.Add(StockDetail)
                        '        dblTotalQty += dblActualReturn
                        '    Next
                        'Else
                        'If dblCostPrice > 0 Then
                        CostPrice = dblCostPrice 'Val(grd.GetRows(i).Cells("CostPrice").Value.ToString)
                        'Else
                        '    CostPrice = dblPurchasePrice
                        'End If
                    Else
                        CostPrice = dblPurchasePrice 'Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString)
                    End If
                    StockDetail = New StockDetail
                    StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                    StockDetail.LocationId = grd.GetRows(i).Cells("LocationId").Value
                    StockDetail.ArticleDefId = grd.GetRows(i).Cells("ArticleDefId").Value
                    ''TASK-408 added TotalQty instead of Qty
                    'StockDetail.InQty = IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value), ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)))
                    StockDetail.InQty = Val(grd.GetRows(i).Cells("TotalQty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value.ToString)  ''TASK-408 added TotalQty instead of Qty
                    StockDetail.OutQty = 0
                    If flgAvgRate = True Then
                        StockDetail.Rate = CostPrice
                    Else
                        StockDetail.Rate = Val(grd.GetRows(i).Cells("PurchasePrice").Value)
                    End If

                    ''Commented below row for TASK-408 on 11-06-2016
                    'StockDetail.InAmount = IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", ((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)), (((Val(grd.GetRows(i).Cells("Qty").Value) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * Val(grd.GetRows(i).Cells("PackQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells("PurchasePrice").Value), CostPrice)))
                    StockDetail.InAmount = ((Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) + Val(grd.GetRows(i).Cells("SampleQty").Value)) * StockDetail.Rate)
                    StockDetail.OutAmount = 0
                    StockDetail.Remarks = grd.GetRows(i).Cells("Comments").Value.ToString
                    'Task:M16 Set Values In Engine_No and Chassis_No 
                    StockDetail.Engine_No = grd.GetRows(i).Cells("Engine_No").Value.ToString
                    StockDetail.Chassis_No = grd.GetRows(i).Cells("Chassis_No").Value.ToString
                    'End Task:M16
                    ''Start TASK-470
                    StockDetail.PackQty = Val(grd.GetRows(i).Cells("PackQty").Value.ToString)
                    StockDetail.In_PackQty = Val(grd.GetRows(i).Cells("Qty").Value.ToString)
                    StockDetail.Out_PackQty = 0
                    ''End TASK-470
                    StockList.Add(StockDetail)

                    'End If

                    LocationType = GetDataTable("Select Location_Type From tblDefLocation WHERE Location_Id=" & grd.GetRows(i).Cells("LocationId").Value, trans).Rows(0).Item(0).ToString
                    objCommand.CommandText = ""
                    'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc) values( " _
                    '                         & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                    '                         & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & "', " & grd.GetRows(i).Cells("BatchId").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("SampleQty").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                    'objCommand.ExecuteNonQuery()
                    'Before against task:2435
                    'R916 Added Column Comments
                    'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc, Comments) values( " _
                    '                 & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                    '                 & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & "', " & grd.GetRows(i).Cells("BatchId").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("SampleQty").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "') "
                    'Task:2435 Added Column CostPrice
                    'Before against task:2527 
                    'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc, Comments, CostPrice) values( " _
                    '                & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                    '                & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & "', " & grd.GetRows(i).Cells("BatchId").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("SampleQty").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', " & Val(StockDetail.Rate) & ") "
                    'Task:2527 Added Column Engine_No, Chassis_No
                    'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc, Comments, CostPrice,Engine_No,Chassis_No, Other_Comments) values( " _
                    '                & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                    '                & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & "', " & grd.GetRows(i).Cells("BatchId").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("SampleQty").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', " & Val(StockDetail.Rate) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "') "


                    objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId, ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc, Comments, CostPrice,Engine_No,Chassis_No, Other_Comments,RefSalesDetailId, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, SalesOrderDetailId) values( " _
                                    & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                                    & " " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("Price").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & "', " & grd.GetRows(i).Cells("BatchId").Value & "," & grd.GetRows(i).Cells("LocationID").Value & ", " & Val(grd.GetRows(i).Cells("Tax_Percent").Value) & ", " & Val(grd.GetRows(i).Cells("SampleQty").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString) & ",N'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("CostPrice").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("RefSalesDetailId").Value.ToString) & " ," & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(txtCurrencyRate.Text) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ") "


                    objCommand.ExecuteNonQuery()

                    'Val(grd.Rows(i).Cells(5).Value)
                    '***********************
                    'Inserting Debit Amount
                    '***********************

                    If Not flgMarketReturnVoucher = True Then
                        '***********************
                        'Inserting Debit Amount
                        '***********************
                        'Before against task:2369
                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId) " _
                        '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", 0, N'" & grd.GetRows(i).Cells("item").Value & "(" & Val(grd.GetRows(i).Cells("Qty").Value) & ")', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & cmbProject.SelectedValue & ")"
                        'objCommand.ExecuteNonQuery()
                        'Task:2369 Change Comments
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, EngineNo, ChassisNo) " _
                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("Price").Value) * Val(txtCurrencyRate.Text) & ", 0, N'" & SetComments(Me.grd.GetRows(i)).Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & cmbProject.SelectedValue & ", " & Val(grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", 0, " & Val(grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ",  " & Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "')" ''TASK-408 added TotalQty instead of Pack Qty * Qty or Qty
                        objCommand.ExecuteNonQuery()
                        'End Task:2369

                        '***********************
                        'Inserting Credit Amount
                        '***********************
                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, direction,CostCenterId) " _
                        '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ",0, " & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) * Val(grd.GetRows(i).Cells("Price").Value)) & ", N'" & grd.GetRows(i).Cells("item").Value & "(" & Val(grd.GetRows(i).Cells("Qty").Value) & ")', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & cmbProject.SelectedValue & ")"
                        'objCommand.ExecuteNonQuery()
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, direction,CostCenterId, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, EngineNo, ChassisNo) " _
                                                 & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ",0, " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("Price").Value.ToString) * Val(txtCurrencyRate.Text) & ", N'" & SetComments(Me.grd.GetRows(i)).Replace("" & Me.cmbVendor.Text & "", "").Replace("'", "''") & "', " & grd.GetRows(i).Cells("ArticleDefId").Value & ", " & cmbProject.SelectedValue & ", 0, " & Val(grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ",  " & Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "')"
                        objCommand.ExecuteNonQuery()
                        'End Task:23639

                    End If


                    Try



                        'If SalesOrderId > 0 Then
                        '    ''TASK-408 made updating new DeliveredTotalQty column
                        '    ''TASK : TFS1263  update and track Sales Order. ON 08-08-2017
                        '    objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                        '                                                       & " SET  DeliveredQty = (isnull(DeliveredQty,0) -  " & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & "),  DeliveredTotalQty = (isnull(DeliveredTotalQty,0) -  " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) & ") " _
                        '                                                       & " WHERE     (SalesOrderId = " & SalesOrderId & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ") AND (SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ") "
                        '    objCommand.ExecuteNonQuery()



                        'End If
                        ''TASK : TFS1398
                        'If Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) > 0 Then
                        '    objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                        '                                                       & " SET  DeliveredQty = (isnull(DeliveredQty,0) -  " & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & "), DeliveredTotalQty = (isnull(DeliveredTotalQty,0) -  " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) & ") " _
                        '                                                       & " WHERE (ArticleDefId = " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ") AND (SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value) & ")"
                        '    objCommand.ExecuteNonQuery()

                        '    ''Set Sales Order Status
                        '    objCommand.CommandText = "Select SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) as DeliveredQty, IsNull(SalesOrderId, 0) As SalesOrderId from SalesOrderDetailTable where SalesOrderId In (Select Distinct SalesOrderId From SalesOrderDetailTable Where SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ") Group By SalesOrderId Having SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) > 0 "
                        '    Dim da As New OleDbDataAdapter(objCommand)
                        '    Dim dtSO As New DataTable
                        '    da.Fill(dtSO)
                        '    If dtSO.Rows.Count > 0 Then
                        '        objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & dtSO.Rows(0).Item("SalesOrderId") & ""
                        '        objCommand.ExecuteNonQuery()
                        '    End If
                        '    ''
                        'End If
                        If Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) > 0 Then
                            objCommand.CommandText = ""
                            objCommand.CommandText = "Select SUM(isnull(Sz1,0)-isnull(SalesReturnQty , 0)) as RemainingQty , SUM(isnull(Qty,0)-isnull(SalesReturnTotalQty , 0)) as RemainingTotalQty from SalesDetailTable where SaleDetailId = " & Val(Me.grd.GetRows(i).Cells("RefSalesDetailId").Value.ToString) & ""
                            Dim daSales As New OleDbDataAdapter(objCommand)
                            Dim dtSales As New DataTable
                            daSales.Fill(dtSales)

                            If Val(grd.GetRows(i).Cells("Qty").Value.ToString) <= dtSales.Rows(0).Item("RemainingQty") Then
                                objCommand.CommandText = ""
                                ''Commented Against TFS4232
                                'objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                '                                                   & " SET  DeliveredQty = (" & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & "), DeliveredTotalQty = (" & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) & ") " _
                                '                                                   & " WHERE (ArticleDefId = " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ") AND (SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value) & ")"
                                objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                                                   & " SET  DeliveredQty = (isnull(DeliveredQty,0) - " & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & "), DeliveredTotalQty = (isnull(DeliveredTotalQty,0) - " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) & ") " _
                                                                                   & " WHERE (ArticleDefId = " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ") AND (SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value) & ")"
                                objCommand.ExecuteNonQuery()
                            Else
                                objCommand.CommandText = ""
                                objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                                                  & " SET  DeliveredQty = (isnull(DeliveredQty,0) - " & Val(dtSales.Rows(0).Item("RemainingQty").ToString) & "), DeliveredTotalQty = (isnull(DeliveredTotalQty,0) - " & Val(dtSales.Rows(0).Item("RemainingTotalQty").ToString) & ") " _
                                                                                  & " WHERE (ArticleDefId = " & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ") AND (SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value) & ")"
                                objCommand.ExecuteNonQuery()
                            End If
                            ''Set Sales Order Status
                            objCommand.CommandText = "Select SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) as DeliveredQty, IsNull(SalesOrderId, 0) As SalesOrderId from SalesOrderDetailTable where SalesOrderId In (Select Distinct SalesOrderId From SalesOrderDetailTable Where SalesOrderDetailId = " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ") Group By SalesOrderId Having SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) > 0 "
                            Dim da As New OleDbDataAdapter(objCommand)
                            Dim dtSO As New DataTable
                            da.Fill(dtSO)
                            If dtSO.Rows.Count > 0 Then
                                objCommand.CommandText = ""
                                objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & dtSO.Rows(0).Item("SalesOrderId") & ""
                                objCommand.ExecuteNonQuery()
                            End If
                            ''
                        End If

                    Catch ex As Exception

                    End Try



                End If

                Dim str As String = "Select ServiceItem from ArticleDefView where ArticleId = " & grd.GetRows(i).Cells("ArticleDefId").Value & ""
                Dim dt2 As DataTable = GetDataTable(str, trans)
                dt2.AcceptChanges()
                Dim ServiceItem As Boolean = Val(dt2.Rows(0).Item("ServiceItem").ToString)
                If ServiceItem = False Then

                    If flgCgsVoucher = True Then

                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount, debit_amount, comments, CostCenterId, direction, sp_refrence, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, EngineNo, ChassisNo) " _
                                                                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & CgsAccountId & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * CostPrice & ", 0, N'" & grd.GetRows(i).Cells("Item").Value & " " & "(" & Val(grd.GetRows(i).Cells("TotalQty").Value) & " X " & CostPrice & ")', " & cmbProject.SelectedValue & ", " & grd.GetRows(i).Cells("ArticleDefId").Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' , 0, " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * CostPrice & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ",  " & Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "')" ''TASK-408 added TotalQty instead of Qty
                        objCommand.ExecuteNonQuery()

                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount, debit_amount,  comments, CostCenterId, direction, sp_refrence , Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, EngineNo, ChassisNo) " _
                                                                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & InvAccountId & ", 0, " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * CostPrice & ", N'" & grd.GetRows(i).Cells("Item").Value & " " & "(" & Val(grd.GetRows(i).Cells("TotalQty").Value) & " X " & CostPrice & ")', " & cmbProject.SelectedValue & ", " & grd.GetRows(i).Cells("ArticleDefId").Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' , " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * CostPrice & ", 0, " & Val(grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ",  " & Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "')" ''TASK-408 added TotalQty instead of Qty
                        objCommand.ExecuteNonQuery()
                        'objCommand.ExecuteNonQuery()
                    End If
                    '''''''''''''''''''''''''''''' Code By Imran Ali 03/06/2013 '''''''''''''''''''''''''''''''''''''''
                End If



            Next


            If flgMarketReturnVoucher = True Then
                If Val(Me.txtTotalAmount.Text) > 0 Then
                    '***********************
                    'Inserting Debit Amount
                    '***********************
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & Val(Me.txtTotalAmount.Text) & ", 0, 'Damage Budget " & "(Total Qty" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ")', " & cmbProject.SelectedValue & ")" ''TASK-408 added TotalQty instead of Qty
                    objCommand.ExecuteNonQuery()


                    '***********************
                    'Inserting Credit Amount
                    '***********************
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ",0, " & ((Val(Me.txtTotalAmount.Text))) & ", 'Damage Budget " & "(Total Qty" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ")', " & cmbProject.SelectedValue & ")" ''TASK-408 added TotalQty instead of Qty
                    objCommand.ExecuteNonQuery()

                Else
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments,CostCenterId) " _
                                                              & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ",0, " & Math.Abs(Val(Me.txtTotalAmount.Text)) & ",  'Damage Budget" & "(Total Qty" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ")', " & cmbProject.SelectedValue & " )" ''TASK-408 added TotalQty instead of Qty
                    objCommand.ExecuteNonQuery()
                    '***********************
                    'Inserting Credit Amount
                    '***********************
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Math.Abs(Val(Me.txtTotalAmount.Text)) & ",0, 'Damage Budget " & "(Total Qty" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ")', " & cmbProject.SelectedValue & ")" ''TASK-408 added TotalQty instead of Qty
                    objCommand.ExecuteNonQuery()
                End If
            End If


            If Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) > 0 Then
                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount,debit_amount,  comments, CostCenterId) " _
                                                          & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 0, 'Ref Tax Sales Return: " & IIf(Me.cmbPo.SelectedIndex <= 0, Me.txtPONo.Text, CType(Me.cmbPo.SelectedItem, DataRowView).Row.Item("Invoice No").ToString) & "', " & cmbProject.SelectedValue & ")"
                objCommand.ExecuteNonQuery()

                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount,debit_amount,comments, CostCenterId) " _
                                       & " VALUES(" & lngVoucherMasterId & "," & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & SalesTaxAccountId & ", 0,  " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 'Ref Tax Sales Return: " & IIf(Me.cmbPo.SelectedIndex <= 0, Me.txtPONo.Text, CType(Me.cmbPo.SelectedItem, DataRowView).Row.Item("Invoice No").ToString) & "', " & cmbProject.SelectedValue & ")"
                objCommand.ExecuteNonQuery()

            End If

            objCommand.CommandText = ""
            objCommand.CommandText = "Update SalesReturnMasterTable SET MarketReturns=" & dblMarketReturns & " WHERE SalesReturnId=" & txtReceivingID.Text
            objCommand.ExecuteNonQuery()

            Call AdjustmentSalesReturn(Val(txtReceivingID.Text), trans)
            'Adustment Sales Return Invoice.

            Try


                objCommand.CommandText = "Select SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & SalesOrderId & " Having SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) > 0 "
                Dim daPOQty As New OleDbDataAdapter(objCommand)
                Dim dtPOQty As New DataTable
                daPOQty.Fill(dtPOQty)
                Dim blnEqual1 As Boolean = True
                If dtPOQty.Rows.Count > 0 Then
                    'For Each r As DataRow In dtPOQty.Rows
                    'If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
                    blnEqual1 = True
                Else
                    blnEqual1 = False
                    'Exit For
                    'End If
                    ' Next
                End If
                If blnEqual1 = True Then
                    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & SalesOrderId & ""
                    objCommand.ExecuteNonQuery()
                End If
            Catch ex As Exception

            End Try



            'TASKM106151 Partially Sales Return Qty Update In Sales Detail Table For Keep Status
            If Not Me.cmbPo.SelectedIndex = -1 And Me.cmbPo.SelectedIndex >= 0 Then
                SalesInvoicePartialReturn(Val(Me.txtReceivingID.Text), Val(Me.cmbPo.SelectedValue), objCommand, trans)
            End If
            'End TASKM106151
            If IsValidate() = True Then
                StockMaster.StockTransId = StockTransId(Me.txtPONo.Text, trans)
                Call New StockDAL().Update(StockMaster, trans)
            End If
            '' TASK TFS4730
            If PaymentVoucherToSalesReturn = True AndAlso Val(Me.txtRecAmount.Text) > 0 Then
                If Me.cmbDepositAccount.SelectedIndex = 0 Or Me.cmbDepositAccount.SelectedIndex = -1 Then
                    ShowErrorMessage("Payment Account is required.")
                    trans.Rollback()
                    Exit Function
                End If
                UpdatePaymentVoucher(objCommand, Me.txtVoucherNo.Text)

            End If
            '' END TASK 4730
            trans.Commit()
            Update_Record = True
            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, Me.txtPONo.Text.Trim, True)
            SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, strVoucherNo, True)
            ''Start TFS3113
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim, Me.Name) Then
                If ValidateApprovalProcessIsInProgressAgain(Me.txtPONo.Text.Trim, Me.Name) = False Then
                    SaveApprovalLog(EnumReferenceType.SalesReturn, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Sales Return ," & cmbVendor.Text & "", Me.Name, 7)
                End If
            End If
            ''End TFS3113

            'insertvoucher()
            'Call Update1() 'Upgrading Stock ...
            setEditMode = True
            setVoucherNo = Me.txtPONo.Text
            getVoucher_Id = Me.txtReceivingID.Text
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            TaxAmount = Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            dblMarketReturns = 0D
            SendSMS()
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try
    End Function
    ''R947 Change Private To Public 
    Public Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.BtnSave.Enabled = False Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Me.cmbProject.SelectedIndex = IIf(Me.cmbProject.SelectedIndex > 0, Me.cmbProject.SelectedIndex, 0)
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
                    If Save() Then
                        SendAutoEmail()
                        'EmailSave()
                        'msg_Information(str_informSave)
                        DisplayRecord()
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
                                    Me.CreditNoteToolStripMenuItem_Click(Nothing, Nothing)
                                End If
                                If PrintVoucher = True Then
                                    GetVoucherPrint(Me.txtPONo.Text, Me.Name, BaseCurrencyName, BaseCurrencyId, True)
                                End If
                            End If
                        End If
                        ''END TASK TFS1462
                        RefreshControls()
                        ClearDetailControls()
                        'grd.Rows.Clear()
                        'DisplayRecord() R933 Commented History Data

                        ''Below lines are commented on 14-09-2017
                        'Dim Printing As Boolean
                        'Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        'If Printing = True Then
                        '    If msg_Confirm("Do you want to print") = True Then
                        '        Me.CreditNoteToolStripMenuItem_Click(Nothing, Nothing)
                        '    End If
                        'End If

                        ' '' Made changes to  against TASK TFS1462 on 13-09-2017
                        'Dim Printing As Boolean
                        'Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        'Dim DirectVoucherPrinting As Boolean
                        'DirectVoucherPrinting = Convert.ToBoolean(getConfigValueByType("DirectVoucherPrinting").ToString)
                        'If Printing = True Or DirectVoucherPrinting = True Then
                        '    If msg_Confirm("Do you want to print", Printing, DirectVoucherPrinting) = True Then
                        '        Dim Print1 As Boolean = frmMessages.Print
                        '        Dim PrintVoucher As Boolean = frmMessages.DirectVoucherPrinting
                        '        If Print1 = True Then
                        '            Me.CreditNoteToolStripMenuItem_Click(Nothing, Nothing)
                        '        End If
                        '        If PrintVoucher = True Then
                        '            GetVoucherPrint(Me.txtPONo.Text, Me.Name, BaseCurrencyName, BaseCurrencyId)
                        '        End If
                        '    End If
                        'End If
                        ' ''END TASK TFS1462

                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                    Else
                        Exit Sub
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update_Record() Then
                        Dim Printing As Boolean
                        Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        Dim DirectVoucherPrinting As Boolean
                        DirectVoucherPrinting = Convert.ToBoolean(getConfigValueByType("DirectVoucherPrinting").ToString)
                        If Printing = True Or DirectVoucherPrinting = True Then
                            If msg_Confirm("Do you want to print", Printing, DirectVoucherPrinting) = True Then
                                Dim Print1 As Boolean = frmMessages.Print
                                Dim PrintVoucher As Boolean = frmMessages.DirectVoucherPrinting
                                If Print1 = True Then
                                    Me.CreditNoteToolStripMenuItem_Click(Nothing, Nothing)
                                End If
                                If PrintVoucher = True Then
                                    GetVoucherPrint(Me.txtPONo.Text, Me.Name, BaseCurrencyName, BaseCurrencyId, True)
                                End If
                            End If
                        End If
                        ''END TASK TFS1462
                        RefreshControls()
                        ClearDetailControls()
                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        
                    Else
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate("Sales Return")
            If EmailTemplate.Length > 0 Then
                GetAutoEmailData()
                UsersEmail = New List(Of String)
                'UsersEmail.Add("adil@agriusit.com")
                ''UsersEmail.Add("ali@agriusit.com")
                UsersEmail.Add("h.saeed@agriusit.com")
                ''UsersEmail.Add("Bilal@siriussolution.com")
                FormatStringBuilder(dtEmail)
                For Each _email As String In UsersEmail
                    CreateOutLookMail(_email)
                    SaveEmailLog(SalesReturnNo, _email, "frmSalesReturn", Activity)
                Next
            Else
                ShowErrorMessage("No email template is found for Salaes Return.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FormatStringBuilder(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")
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
            html.Append("</table>")
            html.Append(AfterFieldsElement)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetAutoEmailData()
        Dim Dr As DataRow
        Try
            Dim str As String
            str = "select TOP 1 SalesReturnId, SalesReturnNo, SalesReturnDate from salesreturnmastertable order by 1 desc"
            Dim dt1 As DataTable = GetDataTable(str)
            If dt1.Rows.Count > 0 Then
                SalesReturnId = dt1.Rows(0).Item("SalesReturnId")
                SalesReturnNo = dt1.Rows(0).Item("SalesReturnNo")
            End If
            Dim str1 As String
            str1 = " SELECT Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Convert(Decimal(18, " & DecimalPointInQty & "), Recv_D.Sz1, 1) AS Qty, Recv_D.Price, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount , Convert(float, 0) As [Total Currency Amount], " _
                   & " IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0) * (Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
                   & " Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Convert(float, 0) as TaxAmount, Convert(float, 0) as CurrencyTaxAmount, Convert(float,0) as TotalAmount, IsNull(Recv_D.SampleQty,0) as SampleQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,Recv_D.Comments,Isnull(Recv_D.CostPrice,0) as CostPrice, Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Recv_D.Qty, 0), 1) As TotalQty FROM dbo.SalesReturnDetailTable Recv_D LEFT OUTER JOIN " _
                   & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                   & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
                   & " LEFT OUTER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & SalesReturnId & ""
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
            mailItem.Subject = "Creating New SR: " + SalesReturnNo
            mailItem.To = Email
            Email = String.Empty
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
            EmailBody = html.ToString
            mailItem.Send()
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If

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
        Try
            ''Task# A2-10-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
            If Me.grdSaved.Row < 0 Then
                Exit Sub
            Else
                EditRecord()
                Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        ''End Task# A2-10-06-2015
    End Sub
    Private Sub cmbPo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPo.SelectedIndexChanged
        Try
            If IsFormOpen = True Then

                If Me.BtnSave.Text <> "&Save" Then
                    If Val(Me.grdSaved.GetRow.Cells("POId").Value.ToString) > 0 Then
                        If Me.cmbPo.SelectedIndex > 0 Then
                            If Me.cmbPo.SelectedValue <> (Me.grdSaved.GetRow.Cells("POId").Value.ToString) Then
                                If msg_Confirm("You have changed Sales Invoice [" & CType(Me.cmbPo.SelectedItem, DataRowView).Row.Item("SalesNo").ToString & "], do you want to proceed. ?") = False Then
                                    RemoveHandler cmbPo.SelectedIndexChanged, AddressOf Me.cmbPo_SelectedIndexChanged
                                    Me.cmbPo.SelectedValue = (Me.grdSaved.GetRow.Cells("POId").Value.ToString)
                                    AddHandler cmbPo.SelectedIndexChanged, AddressOf Me.cmbPo_SelectedIndexChanged
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If


                If Me.cmbPo.SelectedIndex > 0 Then
                    Dim adp As New OleDbDataAdapter
                    Me.cmbVendor.Value = CType(Me.cmbPo.SelectedItem, DataRowView).Item("CustomerCode").ToString 'dt.Rows(0).Item("CustomerCode")

                    If IsDBNull(CType(Me.cmbPo.SelectedItem, DataRowView).Item("CustomerCode")) Then
                        Me.cmbSalesPerson.SelectedIndex = 0
                    Else
                        Me.cmbSalesPerson.SelectedValue = CType(Me.cmbPo.SelectedItem, DataRowView).Item("EmployeeCode").ToString 'dt.Rows(0).Item("EmployeeCode")
                    End If
                    'Me.cmbSalesPerson.SelectedValue = dt.Rows(0).Item("EmployeeCode")
                    If IsDBNull(CType(Me.cmbPo.SelectedItem, DataRowView).Item("CostCenterId")) Then
                        Me.cmbProject.SelectedIndex = 0
                    Else
                        Me.cmbProject.SelectedValue = CType(Me.cmbPo.SelectedItem, DataRowView).Item("CostCenterId").ToString 'dt.Rows(0).Item("CostCenterId")
                    End If
                    'Me.cmbProject.SelectedValue = 0
                    Me.cmbVendor.Enabled = False
                    Me.cmbCompany.Enabled = False


                    Me.DisplayPODetail(Me.cmbPo.SelectedValue)
                    'Me.GetTotal()


                    GetTotalAmount()
                Else
                    Me.cmbVendor.Enabled = True
                    Me.cmbCompany.Enabled = True
                    'Me.cmbVendor.Rows(0).Activate() Comment Line Against task:2583
                    'TFS4681: Waqar: Comment this line to avoid reseting of grid on refresh click
                    'If ItemLoadByCustomer = False Then DisplayPODetail(-1)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbItem.KeyDown
        Try
            ''TFS1858 : Ayesha Rehman :Item dropdown shall be searchable
            If e.KeyCode = Keys.F1 Then
                flgItemSearch = True
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
                frmItemSearch.formName = ""
                frmItemSearch.BringToFront()
                frmItemSearch.ShowDialog()
                If frmItemSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbItem.Value = frmItemSearch.ArticleId
                    txtQty.Text = frmItemSearch.Qty
                    txtPackQty.Text = frmItemSearch.PackQty
                    txtDisc.Text = 0
                    txtDisc.Text = frmItemSearch.DiscountFactor
                    cmbItem_Leave(Nothing, Nothing)
                    txtRate_LostFocus(Nothing, Nothing)
                    AddItemToGrid()
                End If
                flgItemSearch = False
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
    '        'GetTotal()
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
            If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
            Dim strSQl As String = String.Empty

            strSQl = String.Empty
            Me.txtDisc.TabStop = True
            Try
                If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then


                    If getConfigValueByType("ApplyFlatDiscountOnSale").ToString = "False" Then
                        Dim strSQl1 As String = String.Empty
                        strSQl1 = "select discount from tbldefcustomerbasediscounts where articledefid = " & Me.cmbItem.ActiveRow.Cells(0).Value & " and typeid = " & Me.cmbVendor.ActiveRow.Cells("Type Id").Value & "  and discount > 0 "
                        Dim dtdiscount As DataTable = GetDataTable(strSQl1)
                        If Not dtdiscount Is Nothing Then
                            If dtdiscount.Rows.Count <> 0 Then
                                Dim disc As Double = 0D
                                Double.TryParse(dtdiscount.Rows(0)(0).ToString, disc)

                                Dim price As Double = 0D
                                Double.TryParse(Me.txtRate.Text, price)

                                Me.txtRate.Text = price - ((price / 100) * disc)
                                Me.txtDisc.TabStop = False
                            End If
                        End If
                    Else
                        strSQl = "select discount from tbldefcustomerbasediscountsFlat where articledefid = " & Me.cmbItem.ActiveRow.Cells(0).Value _
                                              & " and typeid = " & Me.cmbVendor.ActiveRow.Cells("Type Id").Value & "  and discount > 0 "
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
                                        Me.txtRate.Text = price - ((price / 100) * dblDiscountPercent)
                                        Me.txtDisc.TabStop = False
                                    Else
                                        Me.txtRate.Text = Me.txtRate.Text
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
            Con.Close()
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
        If BtnDelete.Enabled = False Then Exit Sub
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        If IsDateLock(Me.dtpPODate.Value) = True Then
            ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
        End If
        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If

        ''Start TFS3113 : Ayesha Rehman : 09-04-2018
        If blnEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document is in Approval Process ") : Exit Sub
                End If
            End If
        End If
        ''End TFS3113


        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.grd.UpdateData()
            Dim cm As New OleDbCommand
            Dim objTrans As OleDbTransaction
            Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, grdSaved.CurrentRow.Cells(0).Value.ToString)
            Dim strVoucherNo As String = String.Empty
            Dim dt As DataTable = GetRecords("SELECT voucher_no   FROM tblVoucher  WHERE voucher_id = " & lngVoucherMasterId & " ")
            If Not dt Is Nothing Then
                If Not dt.Rows.Count = 0 Then
                    strVoucherNo = dt.Rows(0)("voucher_no")
                End If
            End If

            If Con.State = ConnectionState.Closed Then Con.Open()
            objTrans = Con.BeginTransaction


            Dim SalesOrderId As Integer = 0I
            ''TASK : TFS1263 update and track Sales Order. ON 08-08-2017
            'Dim dtPOID As DataTable = GetDataTable("Select isnull(POId,0) as POId From SalesMasterTable WHERE SalesNo=" & IIf(Me.cmbPo.SelectedIndex > 0, "N'" & Me.cmbPo.Text & "'", "''"), objTrans)
            'If dtPOID.Rows.Count > 0 Then
            '    SalesOrderId = dtPOID.Rows(0).Item(0)
            'Else
            '    SalesOrderId = 0
            'End If
            If Not Me.cmbPo.SelectedIndex = -1 Then
                SalesOrderId = Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("POId").ToString)
            End If
            cm.Connection = Con
            cm.Transaction = objTrans

            cm.CommandText = String.Empty
            cm.CommandText = "Select ArticleDefId, Isnull(Sz1,0) as Qty, Isnull(Qty,0) as TotalQty, IsNull(SalesOrderDetailId, 0) As SalesOrderDetailId From SalesReturnDetailTable WHERE SalesReturnId In(Select SalesReturnId From SalesReturnMasterTable WHERE SalesReturnId=" & Val(Me.grdSaved.CurrentRow.Cells("SalesReturnId").Value.ToString) & ") "
            Dim da As New OleDbDataAdapter
            Dim dtRet As New DataTable
            da.SelectCommand = cm
            da.Fill(dtRet)
            dtRet.AcceptChanges()



            For Each r As DataRow In dtRet.Rows
                r.BeginEdit()
                cm.CommandText = String.Empty
                cm.CommandText = "Update SalesOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) + " & r.Item(1) & "), DeliveredTotalQty = abs(Isnull(DeliveredTotalQty,0) + " & r.Item(2) & ") where  ArticleDefID = " & r.Item(0) & " And SalesOrderDetailId = " & r.Item(3) & ""
                cm.ExecuteNonQuery()


                cm.CommandText = String.Empty
                cm.CommandText = "Select SUM(isnull(Qty,0)-isnull(DeliveredQty , 0)) as DeliveredQty, IsNull(SalesOrderId, 0) As SalesOrderId from SalesOrderDetailTable where SalesOrderID In(Select Distinct SalesOrderId From SalesOrderDetailTable Where SalesOrderDetailId = " & r.Item(3) & " ) Group By SalesOrderId Having SUM(isnull(Qty,0)-isnull(DeliveredQty , 0)) > 0 "
                Dim daPOQty As New OleDbDataAdapter(cm)
                Dim dtPOQty As New DataTable
                daPOQty.Fill(dtPOQty)
                dtPOQty.AcceptChanges()

                Dim blnEqual1 As Boolean = True
                If dtPOQty.Rows.Count > 0 Then
                    If dtPOQty.Rows(0).Item(0) > 0 Then
                        blnEqual1 = True
                    Else
                        blnEqual1 = False
                    End If

                    If blnEqual1 = True Then
                        cm.CommandText = String.Empty
                        ''Select Distinct SalesOrderId From SalesOrderDetailTable Where SalesOrderDetailId = " & r.Item(3) & "
                        'cm.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & dtPOQty.Rows(0).Item("SalesOrderId") & ""
                        cm.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & dtPOQty.Rows(0).Item("SalesOrderId") & ""
                        cm.ExecuteNonQuery()
                    Else
                        cm.CommandText = String.Empty
                        ''Select Distinct SalesOrderId From SalesOrderDetailTable Where SalesOrderDetailId = " & r.Item(3) & "
                        'cm.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & dtPOQty.Rows(0).Item("SalesOrderId") & ""
                        cm.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & dtPOQty.Rows(0).Item("SalesOrderId") & ""
                        cm.ExecuteNonQuery()
                    End If
                End If
                r.EndEdit()

            Next
            'TASKM106151 Partially Sales Return Qty Remove In Sales Detail Table
            If Val(Me.grdSaved.GetRow.Cells("POId").Value.ToString) > 0 Then
                DeleteInvoicePartialReturn(Val(Me.grdSaved.CurrentRow.Cells("SalesReturnId").Value.ToString), Val(Me.grdSaved.GetRow.Cells("POId").Value.ToString), cm, objTrans)
            End If
            'End TASKM106151
            'cm.Connection = Con

            cm.CommandText = ""
            cm.CommandText = "Delete From InvoiceAdjustmentTable WHERE VoucherDetailId=" & Val(Me.grdSaved.CurrentRow.Cells("SalesReturnId").Value.ToString) & " AND InvoiceType='Sales Return'"
            cm.ExecuteNonQuery() ''Number of rows effected by delete statement.


            cm.CommandText = String.Empty
            cm.CommandText = "delete from SalesReturnDetailTable where SalesReturnid=" & Val(Me.grdSaved.CurrentRow.Cells("SalesReturnId").Value.ToString) & " "
            'cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            'cm = New OleDbCommand
            'cm.Connection = Con
            cm.CommandText = String.Empty
            cm.CommandText = "delete from SalesReturnMasterTable where SalesReturnid=" & Val(Me.grdSaved.CurrentRow.Cells("SalesReturnId").Value.ToString) & " "
            'cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            'cm = New OleDbCommand
            'cm.Connection = Con
            cm.CommandText = String.Empty
            cm.CommandText = "delete from tblvoucherdetail where voucher_id=" & lngVoucherMasterId
            'cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            'cm = New OleDbCommand
            'cm.Connection = Con
            cm.CommandText = String.Empty
            cm.CommandText = "delete from tblvoucher where voucher_id=" & lngVoucherMasterId
            'cm.Transaction = objTrans
            cm.ExecuteNonQuery()






            StockMaster = New StockMaster
            StockMaster.StockTransId = StockTransId(Me.grdSaved.CurrentRow.Cells(0).Value.ToString, objTrans)
            Call New StockDAL().Delete(StockMaster, objTrans)

            If PaymentVoucherToSalesReturn = True AndAlso VoucherId > 0 Then
                cm.CommandText = String.Empty
                cm.CommandText = "delete from tblvoucherdetail where voucher_id = " & VoucherId & " "
                cm.ExecuteNonQuery()
                cm.CommandText = String.Empty
                cm.CommandText = "delete from tblvoucher where voucher_id = " & VoucherId & ""
                cm.ExecuteNonQuery()
            End If
            '' END TASK 4730
            objTrans.Commit()

            'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
            'msg_Information(str_informDelete)
            Me.txtReceivingID.Text = 0
            SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Sales, grdSaved.CurrentRow.Cells(0).Value.ToString, True)
            SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, strVoucherNo, True)
            'Call Delete() 'Upgrading Stock ...
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
            Me.grdSaved.CurrentRow.Delete()
        Catch ex As Exception
            msg_Error("Error occured while deleting record: " & ex.Message)
        Finally
            Con.Close()
            Me.Cursor = Cursors.Default
        End Try
        Me.RefreshControls()
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'ShowReport("SalesReturn", "{SalesReturnMasterTable.SalesReturnId}=" & grdSaved.CurrentRow.Cells("SalesReturnId").Value)

    End Sub

    'Private Sub grd_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
    '    Me.GetTotal()
    'End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.btnSearchDelete.Enabled = True ''R934 Added Dlete Button
                Me.btnSearchPrint.Enabled = True   ''R934 Added Print Button
                Me.chkPost.Visible = True
                Me.chkPost.Checked = True
                PrintVoucherToolStripMenuItem.Enabled = True
                PrintVoucherToolStripMenuItem1.Enabled = True
                ''TASK TFS1558 ON 02-10-2017
                Me.grd.RootTable.Columns(GrdEnum.Rate).EditType = Janus.Windows.GridEX.EditType.TextBox
                Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).EditType = Janus.Windows.GridEX.EditType.TextBox
                Me.grd.RootTable.Columns(GrdEnum.CurrentPrice).EditType = Janus.Windows.GridEX.EditType.TextBox
                ''END TASK TFS1558
                ''TASK TFS1559
                Me.btnAdd.Enabled = True
                ''END TASK TFS1559
                PaymentPost = True
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
                    CtrlGrdBar1.mGridChooseFielder.Enabled = False ' 'Task:2406 Added Field Chooser Rights
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    PrintVoucherToolStripMenuItem.Enabled = False
                    PrintVoucherToolStripMenuItem1.Enabled = False
                    ''TASK TFS1558 ON 02-10-2017
                    Me.grd.RootTable.Columns(GrdEnum.Rate).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    Me.grd.RootTable.Columns(GrdEnum.CurrentPrice).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    ''END TASK TFS1558
                    ''TASK TFS1559
                    Me.btnAdd.Enabled = False
                    ''END TASK TFS1559
                    PaymentPost = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmSalesReturn)
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
                UserPostingRights = GetUserPostingRights(LoginUserId)
                If UserPostingRights = True Then
                    Me.chkPost.Visible = True
                Else
                    Me.chkPost.Visible = False
                    Me.chkPost.Checked = False
                End If
                GetSecurityByPostingUser(UserPostingRights, BtnSave, BtnDelete)
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.btnSearchDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                Me.btnSearchPrint.Enabled = False
                Me.chkPost.Visible = False
                Me.chkPost.Checked = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                PrintVoucherToolStripMenuItem.Enabled = False
                PrintVoucherToolStripMenuItem1.Enabled = False
                ''TASK TFS1558 ON 02-10-2017
                Me.grd.RootTable.Columns(GrdEnum.Rate).EditType = Janus.Windows.GridEX.EditType.NoEdit
                Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).EditType = Janus.Windows.GridEX.EditType.NoEdit
                Me.grd.RootTable.Columns(GrdEnum.CurrentPrice).EditType = Janus.Windows.GridEX.EditType.NoEdit
                ''END TASK TFS1558
                ''TASK TFS1559
                Me.btnAdd.Enabled = False
                ''END TASK TFS1559
                PaymentPost = False
                For Each Rightsdt As GroupRights In Rights
                    If Rightsdt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rightsdt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                        Me.btnSearchDelete.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        Me.btnSearchPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True
                        If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True
                        'Task:2406 Added Field Chooser Rights
                    ElseIf Rightsdt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Print Voucher" Then
                        PrintVoucherToolStripMenuItem.Enabled = True
                        PrintVoucherToolStripMenuItem1.Enabled = True
                        'End Task:2406
                    ElseIf Rightsdt.FormControlName = "Change Price" Then
                        ''TASK TFS1558 ON 02-10-2017
                        Me.grd.RootTable.Columns(GrdEnum.Rate).EditType = Janus.Windows.GridEX.EditType.TextBox
                        Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).EditType = Janus.Windows.GridEX.EditType.TextBox
                        Me.grd.RootTable.Columns(GrdEnum.CurrentPrice).EditType = Janus.Windows.GridEX.EditType.TextBox
                        ''END TASK TFS1558
                    ElseIf Rightsdt.FormControlName = "Without Sales" Then
                        ''TASK TFS1559
                        Me.btnAdd.Enabled = True
                        ''END TASK TFS1559
                    ElseIf Rightsdt.FormControlName = "Payment Voucher Post" Then
                        PaymentPost = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub UltraTabControl2_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl2.SelectedTabChanged
    '    If Me.UltraTabControl2.SelectedTab.Index = 0 Then
    '        Me.btnLoadAll.Visible = False
    '        Me.ToolStripButton1.Visible = False
    '        Me.ToolStripSeparator1.Visible = False
    '    Else
    '        Me.btnLoadAll.Visible = True
    '        Me.ToolStripButton1.Visible = True
    '        Me.ToolStripSeparator1.Visible = True
    '    End If
    'End Sub
    'Private Sub btnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    Try
    '        DisplayRecord("All")
    '        Me.DisplayDetail(-1)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Me.Cursor = Cursors.WaitCursor
        Try


            If Not getConfigValueByType("AvgRate").ToString = "Error" Then
                flgAvgRate = getConfigValueByType("AvgRate")
            Else
                flgAvgRate = False
            End If
            ''start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161
            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''END TFS4544
            'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
            Dim id As Integer = 0

            id = Me.cmbVendor.SelectedRow.Cells(0).Value
            FillCombo("Vendor")
            Me.cmbVendor.Value = id

            id = Me.cmbSalesMan.SelectedValue
            FillCombo("SM")
            Me.cmbSalesMan.SelectedValue = id

            id = Me.cmbBillMaker.SelectedValue
            FillCombo("BM")
            Me.cmbBillMaker.SelectedValue = id

            id = Me.cmbPackingMan.SelectedValue
            FillCombo("PM")
            Me.cmbPackingMan.SelectedValue = id
            'TFS1392:05-Sep-2017:Rai Haider : Prevent Clear Grid (Grid fill on CmbPo index Changed) 
            'If we partialy Return SalesInvoice then on Update If we refresh grid was fill with remaing Items which are not return 
            'Start TFS
            If Me.BtnSave.Text = "&Save" Then
                id = Me.cmbPo.SelectedValue
                FillCombo("SO")
                Me.cmbPo.SelectedValue = id
            End If
            'End TFS1392

            id = Me.cmbCategory.SelectedValue
            FillCombo("Category")
            Me.cmbCategory.SelectedValue = id

            id = Me.cmbItem.SelectedRow.Cells(0).Value
            FillCombo("Item")
            Me.cmbItem.Value = id

            id = Me.cmbProject.SelectedIndex
            FillCombo("Project")
            Me.cmbProject.SelectedIndex = id

            FillCombo("grdLocation")

            FillCombo("SearchVendor")
            FillCombo("SearchLocation")

            'If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
            '    flgLoadAllItems = getConfigValueByType("LoadAllItemsInSales")
            'End If

            'If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
            '    flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
            'End If
            ' ''R929 Added Flag OnetimeSalesReturn
            'If Not getConfigValueByType("OnetimeSalesReturn").ToString = "Error" Then
            '    flgOnetimeSalesReturn = getConfigValueByType("OnetimeSalesReturn")
            'End If
            ' ''End R929

            If BackgroundWorker3.IsBusy Then Exit Sub
            BackgroundWorker3.RunWorkerAsync()
            Do While BackgroundWorker3.IsBusy
                Application.DoEvents()
            Loop

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub txtDisc_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDisc.Leave
        Try
            Dim disc As Double = 0D
            Double.TryParse(Me.txtDisc.Text.Trim, disc)
            Dim price As Double = 0D
            Double.TryParse(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString, price)
            If Val(Me.txtPackRate.Text) = 0 Then
                If disc > 0 Then
                    Me.txtRate.Text = price - ((price / 100) * disc)
                End If
            Else
                Me.txtRate.Text = Me.txtRate.Text
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs)

    End Sub
    Private Sub CreditNoteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreditNoteToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("SalesReturnNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'Ali Faisal : TFS2052 : Print error
            'Ali Faisal : TFS2280 : Fast print sends all vouchers to print
            AddRptParam("@SalesReturnId", Me.grdSaved.GetRow.Cells("SalesReturnId").Value)
            If IsPreviewInvoice = True Then
                'ShowReport("SalesReturn", "{SalesReturnMasterTable.SalesReturnId}=" & grdSaved.CurrentRow.Cells("SalesReturnId").Value, , , False, , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
                ShowReport("SalesReturn", , , , False, , , , , , , )
            Else
                'ShowReport("SalesReturn", "{SalesReturnMasterTable.SalesReturnId}=" & grdSaved.CurrentRow.Cells("SalesReturnId").Value, , , True, , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
                ShowReport("SalesReturn", , , , True, , , , , , , )
            End If
            'Ali Faisal : TFS2280 : End
            'Ali Faisal : TFS2052 : End
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub InwardGatePassToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InwardGatePassToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = grdSaved.GetRow.Cells("PurchaseReturnNo").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            ShowReport("SalesInwardGatePass", "{SalesReturnMasterTable.SalesReturnId}=" & grdSaved.CurrentRow.Cells("SalesReturnId").Value, , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
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
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Me.Cursor = Cursors.WaitCursor
        Try
            For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.cmbItem.Rows
                If row.Index > 0 Then
                    Me.cmbItem.Focus()
                    row.Selected = True
                    Me.txtQty.Focus()
                    Me.txtQty.Text = 0
                    AddItemToGrid(True)
                    Application.DoEvents()
                End If
            Next
            Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.CloseDropdown)
            'Me.GetTotal()
            Me.ClearDetailControls()
            'Me.LinkLabel1.Visible = False
            'Me.Panel1.Visible = False
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            ''R-916 Added Index Comments for editing ....
            ''Me.grd.AutomaticSort = False
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                'Before against task:M16
                'If col.Index <> GrdEnum.LocationId AndAlso col.Index <> GrdEnum.Qty AndAlso col.Index <> GrdEnum.Rate AndAlso col.Index <> GrdEnum.Tax_Percent AndAlso col.Index <> GrdEnum.SampleQty AndAlso col.Index <> GrdEnum.PurchasePrice AndAlso col.Index <> GrdEnum.Comments Then
                'Task:M16 Set Editable Fields Engine_No And Chassis_No
                If col.Index <> GrdEnum.LocationId AndAlso col.Index <> GrdEnum.Qty AndAlso col.Index <> GrdEnum.TotalQty AndAlso col.Index <> GrdEnum.Tax_Percent AndAlso col.Index <> GrdEnum.SampleQty AndAlso col.Index <> GrdEnum.Comments AndAlso col.Index <> GrdEnum.Engine_No AndAlso col.Index <> GrdEnum.Chassis_No AndAlso col.Index <> GrdEnum.OtherComments AndAlso col.Index <> GrdEnum.CurrencyAmount Then
                    'End Task:M16
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
            If flgLoadAllItems = False Then
                Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("Unit").Index
                Me.grd.RootTable.Columns("Unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index
                Me.grd.RootTable.Columns("Pack_Desc").Visible = True
                Me.grd.RootTable.Columns("Unit").Visible = False
            Else
                Me.grd.RootTable.Columns("Pack_Desc").Visible = False
                Me.grd.RootTable.Columns("Unit").Visible = True
            End If
            Me.grd.RootTable.Columns("SalesAccountId").Visible = False
            'Me.grd.RootTable.Columns("BaseCurrencyId").Visible = False
            'Me.grd.RootTable.Columns("CurrencyId").Visible = False
            Me.grd.RootTable.Columns("SalesAccountId").Visible = False
            Me.grd.RootTable.Columns("SalesAccountId").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Total Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Total Amount").TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Total Amount").FormatString = "N" & TotalAmountRounding
            'Task:M16 Engine_No And Chassis_No Column Enable/Disabled
            If flgVehicleIdentificationInfo = True Then
                Me.grd.RootTable.Columns("Engine_No").Visible = True
                Me.grd.RootTable.Columns("Chassis_No").Visible = True
            Else
                Me.grd.RootTable.Columns("Engine_No").Visible = False
                Me.grd.RootTable.Columns("Chassis_No").Visible = False
            End If
            'End Task:M16
            'Me.grd.AutoSizeColumns()

            'Task:2759 Set Rounded Amount Format
            Me.grd.RootTable.Columns("Total").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Total").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Total").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns("Total Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Total Amount").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Total Amount").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns("TaxAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("TaxAmount").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("TaxAmount").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns(GrdEnum.Rate).FormatString = "N"

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

            StockMaster = New StockMaster
            StockMaster.StockTransId = Convert.ToInt32(GetStockTransId(Me.grdSaved.CurrentRow.Cells(0).Value))
            Return New StockDAL().Delete(StockMaster)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            'Dim transId As Integer = 0
            'transId = Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
            StockMaster = New StockMaster
            StockMaster.StockTransId = 0I 'transId
            StockMaster.DocNo = Me.txtPONo.Text.ToString.Replace("'", "''")
            StockMaster.DocDate = Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt")
            StockMaster.DocType = 4 'Convert.ToInt32(GetStockDocTypeId("SalesReturn").ToString)
            StockMaster.Remaks = Me.txtRemarks.Text.ToString.Replace("'", "''")


            'If Not Me.cmbCompany.SelectedValue Is Nothing Then
            '    StockMaster.Project = Val(CType(Me.cmbCompany.SelectedItem, DataRowView).Row.Item("CostCenterId").ToString) 'GetCostCenterId(Me.cmbCompany.SelectedValue)
            'End If
            StockMaster.Project = Me.cmbProject.SelectedValue
            StockMaster.AccountId = Me.cmbVendor.Value
            StockMaster.StockDetailList = StockList 'New List(Of StockDetail)
            'For Each grdRow As Windows.Forms.DataGridViewRow In Me.grd.Rows
            'For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            '    StockDetail = New StockDetail
            '    StockDetail.StockTransId = transId 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
            '    StockDetail.LocationId = grdRow.Cells("LocationID").Value
            '    StockDetail.ArticleDefId = grdRow.Cells("ArticleDefId").Value
            '    StockDetail.InQty = IIf(grdRow.Cells("Unit").Value = "Loose", Val(grdRow.Cells("Qty").Value) + Val(grdRow.Cells("SampleQty").Value), ((Val(grdRow.Cells("Qty").Value) + Val(grdRow.Cells("SampleQty").Value)) * Val(grdRow.Cells("PackQty").Value)))
            '    StockDetail.OutQty = 0
            '    StockDetail.Rate = Val(grdRow.Cells("Price").Value)
            '    StockDetail.InAmount = IIf(grdRow.Cells("Unit").Value = "Loose", ((Val(grdRow.Cells("Qty").Value) + Val(grdRow.Cells("SampleQty").Value)) * Val(grdRow.Cells("Price").Value)), ((Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value)) * Val(grdRow.Cells("Price").Value)))
            '    StockDetail.OutAmount = 0
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
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub grd_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            GetGridDetailQtyCalculate(e)
            GetTotalAmount()
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
                GetTotalAmount()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub cmbItem_RowSelected(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
    '    Try
    '        If Me.cmbItem.IsItemInList = False Then
    '            Me.txtStock.Text = 0
    '            Exit Sub
    '        End If
    '        If Me.cmbItem.Value Is Nothing Then Exit Sub
    '        Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
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

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Return"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            Dim company As Integer = 0
            If Not Me.cmbCompany.SelectedValue Is Nothing Then
                company = Me.cmbCompany.SelectedValue
            End If
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                'rafay:task start
                If CompanyPrefix = "V-ERP (UAE)" Then
                    companyinitials = "UE"
                    'Return GetNextDocNo("SR" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "SalesReturnMasterTable", "SalesReturnNo") ''
                    Return GetSerialNo("SR" & "" & company.ToString + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "SalesReturnMasterTable", "SalesReturnNo")
                Else
                    companyinitials = "PK"
                    Return GetNextDocNo("SR" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "SalesReturnMasterTable", "SalesReturnNo")
                End If
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    Return GetNextDocNo("SR" & "-" & company.ToString & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "SalesReturnMasterTable", "SalesReturnNo") ''
                    'Return GetSerialNo("SR" & "" & company.ToString + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "SalesReturnMasterTable", "SalesReturnNo")
                Else
                    companyinitials = "PK"
                    Return GetNextDocNo("SR" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "SalesReturnMasterTable", "SalesReturnNo")
                End If
                'Rafay:task End
            Else
                Return GetNextDocNo("SR" & "" & company.ToString, 6, "SalesReturnMasterTable", "SalesReturnNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub txtPONo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPONo.TextChanged

    End Sub

    Private Sub btnAddNewItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewItem.Click
        Call frmAddItem.ShowDialog()
        Call FillCombo("Item")
    End Sub

    Private Sub cmbVendor_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbVendor.KeyDown
        Try
            ''TFS1781 : Ayesha Rehman :Added for Selection of Customer or Vendor
            If e.KeyCode = Keys.F1 Then
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    'frmSearchCustomersVendors.rbtCustomers.Checked = True
                    'frmSearchCustomersVendors.rbtVendors.Checked = True
                    'frmSearchCustomersVendors.rbtCustomers.Visible = True
                    'frmSearchCustomersVendors.rbtVendors.Visible = True
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

    Private Sub cmbVendor_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.Leave
        Try
            ''Task No 2554 Fill Agin The Combo Of Invoice 
            If cmbVendor.ActiveRow Is Nothing Then
                Exit Sub
            End If
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            _Previous_Balance = GetCurrentBalance(Me.cmbVendor.Value)
            FillCombo("SO")
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub
    Private Sub cmbVendor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.ValueChanged
        Try
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            Dim str As String = String.Empty
            If getConfigValueByType("LoadAllItemsInSales").ToString = "True" Then
                ItemLoadByCustomer = True
                str = "Select CustomerTypes From tblCustomer WHERE AccountId=" & Me.cmbVendor.Value

                Dim dt As DataTable = GetDataTable(str)
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.DisplayDetail(-1, dt.Rows(0).Item(0), "LoadAll")
                    Else
                        Me.DisplayDetail(-1, -1, "LoadAll")
                    End If
                End If
                If Me.grd.RowCount = 0 Then Exit Sub
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    If Me.grd.RowCount > 0 Then
                        r.BeginEdit()
                        r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
                        r.EndEdit()
                    End If
                Next
            Else
                ItemLoadByCustomer = False
            End If


            CtrlGrdBar1.Email = New SBModel.SendingEmail
            CtrlGrdBar1.Email.ToEmail = Me.cmbVendor.ActiveRow.Cells("Email").Text
            CtrlGrdBar1.Email.Subject = "Sales Return: " + "(" & Me.txtPONo.Text & ")"
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
            Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name=N'" & Me.Name & "' AND EmailAlert=1")
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
                Email.Subject = "Sales Return " & setVoucherNo & ""
                Email.Body = "Sales Return " _
                & " " & IIf(setEditMode = False, "of amount " & Total_Amount + TaxAmount & " is made", "of amount " & Previouse_Amount & " is updated to " & Total_Amount + TaxAmount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
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
    Public Function Get_All(ByVal SalesReturnNo As String)
        Try
            Get_All = Nothing
            If Not SalesReturnNo.Length > 0 Then Exit Try
            If IsFormOpen = True Then
                If SalesReturnNo.Length > 0 Then
                    '    Dim str As String = "Select * From SalesReturnMasterTable WHERE SalesReturnNo=N'" & SalesReturnNo & "'"
                    '    Dim dt As DataTable = GetDataTable(str)
                    '    If dt IsNot Nothing Then
                    '        If dt.Rows.Count > 0 Then
                    '            Me.txtReceivingID.Text = dt.Rows(0).Item("SalesReturnId")
                    '            Me.txtPONo.Text = dt.Rows(0).Item("SalesReturnNo").ToString
                    '            Me.dtpPODate.Value = dt.Rows(0).Item("SalesReturnDate")
                    '            'Me.cmbCompany.SelectedValue = dt.Rows(0).Item("LocationId")
                    '            Me.cmbVendor.Value = dt.Rows(0).Item("CustomerCode")
                    '            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
                    '            Me.txtPaid.Text = dt.Rows(0).Item("CashPaid")
                    '            Me.cmbPo.SelectedValue = dt.Rows(0).Item("POId")
                    '            Me.cmbSalesMan.SelectedValue = dt.Rows(0).Item("EmployeeCode")
                    '            Me.chkPost.Checked = dt.Rows(0).Item("Post")
                    '            Me.txtAdjPercent.Text = dt.Rows(0).Item("AdjPercent").ToString
                    '            Me.txtAdjustment.Text = dt.Rows(0).Item("Adjustment").ToString
                    '            Me.txtTotalAmount.Text = dt.Rows(0).Item("MarketReturns").ToString
                    '            Me.txtDamageBudget.Text = dt.Rows(0).Item("Damage_Budget").ToString
                    '            Me.cmbCompany.SelectedValue = dt.Rows(0).Item("LocationId").ToString
                    '            If IsDBNull(dt.Rows(0).Item("CostCenterId")) Then
                    '                Me.cmbProject.SelectedIndex = 0
                    '            Else
                    '                Me.cmbProject.SelectedValue = dt.Rows(0).Item("CostCenterId")
                    '            End If
                    '            DisplayDetail(Val(Me.txtReceivingID.Text))
                    '            Me.GetTotalAmount()
                    '            'GetTotal()
                    '            Me.BtnSave.Text = "&Update"
                    '            Me.cmbPo.Enabled = False
                    '            GetSecurityRights()
                    '            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
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

                    '' Task# H08062015  Ahmad Sharif:
                    IsDrillDown = True
                    If Me.grdSaved.RowCount <= 50 Then
                        blnDisplayAll = True
                        Me.btnSearchLoadAll_Click(Nothing, Nothing)
                        blnDisplayAll = False
                    End If
                    Dim flag As Boolean = False
                    flag = Me.grdSaved.Find(Me.grdSaved.RootTable.Columns("SalesReturnNo"), Janus.Windows.GridEX.ConditionOperator.Equal, SalesReturnNo, 0, 1)
                    'If flag = True Then
                    Me.grdSaved_CellDoubleClick(Nothing, Nothing)
                    'Else
                    ' Exit Function
                    ' End If
                    '' End Task# H08062015
                End If
            End If
            'IsDrillDown = False
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSearchLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchLoadAll.Click
        Try
            DisplayRecord("All")
            DisplayDetail(-1)
        Catch ex As Exception
            Throw ex
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
                If Not Me.cmbSearchLocation.SelectedIndex - 1 Then Me.cmbSearchLocation.SelectedIndex = 0
            Else
                If Not Me.cmbSearchLocation.SelectedIndex - 1 Then Me.cmbSearchLocation.SelectedIndex = 0
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
            '' TASK TFS4730
            If blnEditMode = False Then
                cmbVendor.Value = grdSaved.CurrentRow.Cells("CustomerCode").Value
                VoucherDetail(Me.grdSaved.CurrentRow.Cells(0).Value.ToString)
            End If
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

    Private Sub CreditNoteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreditNoteToolStripMenuItem1.Click
        Try
            CreditNoteToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub InwardGatepassToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InwardGatepassToolStripMenuItem1.Click
        Try
            InwardGatePassToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl2_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl2.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                DisplayRecord()
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                Me.BtnDelete.Visible = False
                Me.BtnPrint.Visible = False
            Else
                If blnEditMode = False Then Me.BtnPrint.Visible = False
                If blnEditMode = False Then Me.BtnDelete.Visible = False
                Me.BtnEdit.Visible = False
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
                    If IO.File.Exists(str_ApplicationStartUpPath & "\Reports\SalesReturn.rpt") = False Then Exit Sub
                    crpt.Load(str_ApplicationStartUpPath & "\Reports\SalesReturn.rpt", DBServerName)
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
                    crpt.RecordSelectionFormula = "{SalesReturnMasterTable.SalesReturnId}=" & VoucherId



                    Dim crExportOps As New ExportOptions
                    Dim crDiskOps As New DiskFileDestinationOptions
                    Dim crExportType As New PdfRtfWordFormatOptions


                    If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments\") Then
                        IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments\")
                    Else
                    End If
                    FileName = String.Empty
                    FileName = "Sales Return" & "-" & setVoucherNo & ""
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
    Private Sub txtAdjPercent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdjustment.TextChanged, txtAdjPercent.TextChanged
        Try
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetTotalAmount()
        Try
            Me.txtTotalAmount.Text = 0
            If Me.grd.RowCount = 0 Then Exit Sub
            Me.grd.UpdateData()
            'Dim dblTotalAmount As Double = 0D
            Dim dblAdjPercent As Double = 0D
            Dim dblTotal As Double = 0D
            If Me.grd.RowCount = 0 Then Exit Sub
            For i As Integer = 0 To Me.grd.RowCount - 1
                dblTotal += Me.grd.GetRows(i).Cells("Total").Value
            Next
            ' Double.TryParse(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum), dblTotalAmount)
            Me.txtTotalAmount.Text = Math.Round((dblTotal - ((Val(Me.txtAdjPercent.Text) * Val(dblTotal)) / 100) - Val(Me.txtAdjustment.Text) - Val(Me.txtDamageBudget.Text)), TotalAmountRounding)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub txtDamageBudget_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDamageBudget.TextChanged
        Try
            Me.txtTotalAmount.Text = Math.Round((Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) - ((Val(Me.txtAdjPercent.Text) * Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum))) / 100) - Val(Me.txtAdjustment.Text) - Val(Me.txtDamageBudget.Text)), TotalAmountRounding)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCompany_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCompany.SelectedIndexChanged
        Try
            If IsFormOpen = True AndAlso Not Me.cmbCompany.SelectedIndex = -1 Then
                RefreshControls()
                DisplayRecord()
                'Task:2427 CostCenter Selected Company Wise
                If Not Me.cmbCompany.SelectedIndex = -1 Then
                    If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedValue = Val(CType(Me.cmbCompany.SelectedItem, DataRowView).Row.Item("CostCenterId").ToString)
                End If
                'End Task:2427
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetCostCenterId(ByVal CompanyId As Integer) As Integer
        Try
            Dim str As String
            Dim dt As New DataTable
            Dim adp As New OleDb.OleDbDataAdapter
            str = "select IsNull(CostCenterId,0) as CostCenterId From CompanyDefTable WHERE CompanyId=" & CompanyId & ""
            If Con.State = ConnectionState.Closed Then Con.Open()
            adp = New OleDb.OleDbDataAdapter(str, Con)
            adp.Fill(dt)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0)
                Else
                    Return 0
                End If
            Else : Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
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
            Else
                Me.txtRate.Text = Me.txtRate.Text
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


    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus
        'If Val(Me.txtPackQty.Text) = 0 Then
        '    txtPackQty.Text = 1
        '    txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        'Else
        '    txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
        'End If
        'If IsSalesOrderAnalysis = True Then
        '    If Val(Me.txtDisc.Text) <> 0 Then
        '        Me.txtDisc.TabStop = True
        '    End If
        'End If
        GetDetailTotal()
    End Sub

    Private Sub txtQty_LostFocus1(sender As Object, e As EventArgs) Handles txtQty.LostFocus

    End Sub









    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Try
            'If Val(Me.txtPackQty.Text) > 0 Then
            '    Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            'Else
            '    Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
            'End If
            '' TASK: TFS1357 Converted Qty columns to decimal from double to show end zero after points. Ameen on 22-08-2017 
            If Val(Me.txtPackQty.Text) > 0 AndAlso Me.txtQty.Text <> "" Then
                Me.txtTotalQuantity.Text = Math.Round(Convert.ToDecimal(Me.txtPackQty.Text) * Convert.ToDecimal(Me.txtQty.Text), TotalAmountRounding)
            ElseIf Me.txtQty.Text <> "" Then
                Me.txtTotalQuantity.Text = Convert.ToDecimal(Me.txtQty.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try

            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub

            FillCombo("ArticlePack")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''R929 Added function Check existing sales return invoice
    Public Function CheckExistingSalesReturnInvoice(ByVal CustomerId As Integer, ByVal SalesReturnDate As DateTime) As Integer
        Try
            Dim Query As String = String.Empty
            Dim int As Integer = 0I
            If flgOnetimeSalesReturn = True Then
                Query = "Select Count(*) Cont From SalesReturnMasterTable WHERE CustomerCode=" & CustomerId & " AND (Convert(Varchar, SalesReturnDate, 102)=Convert(DateTime,N'" & SalesReturnDate.ToString("yyyy-M-d 00:00:00") & "',102))"
                If Con.State = ConnectionState.Closed Then Con.Open()
                Dim cmd As New OleDbCommand(Query, Con)
                int = cmd.ExecuteScalar
            End If
            Return int
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    'End R929


    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13
        If e.KeyCode = Keys.F5 Then
            BtnRefresh_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.BtnEdit, Nothing)
            Exit Sub
        End If
        ''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
        'If e.KeyCode = Keys.Delete Then
        '    DeleteToolStripButton_Click(BtnDelete, Nothing)
        '    Exit Sub
        'End If

    End Sub
    ''Task:2369 Added Function Set Comments
    Public Function SetComments(ByVal GridExRow As Janus.Windows.GridEX.GridEXRow) As String
        Try
            Dim Comments As String = String.Empty
            If GridExRow IsNot Nothing Then
                If flgCommentCustomerFormat = True Then
                    Comments += Me.cmbVendor.Text.Replace("'", "''") & ","
                End If
                If flgCommentArticleFormat = True Then
                    Comments += " " & GridExRow.Cells("item").Value.ToString & ","
                End If
                'If flgCommentArticleSizeFormat = True Then
                '    Comments += " " & GridExRow.Cells("Size").Value.ToString & ","
                'End If
                'If flgCommentArticleColorFormat = True Then
                '    Comments += " " & GridExRow.Cells("Color").Value.ToString & ","
                'End If
                If flgCommentQtyFormat = True Then
                    'Before against task:2583
                    'Comments += " " & IIf(Val(GridExRow.Cells("PackQty").Value.ToString) = 0, Val(GridExRow.Cells("Qty").Value.ToString), Val(GridExRow.Cells("Qty").Value.ToString) * Val(GridExRow.Cells("PackQty").Value.ToString))
                    'Task:2583 Changed Format Of Qty And Price
                    ''Below is commented against TASK: TFS1357
                    'Comments += " (" & IIf(GridExRow.Cells("Unit").Value.ToString = "Loose", Val(GridExRow.Cells("Qty").Value.ToString), Val(GridExRow.Cells("Qty").Value.ToString) * Val(GridExRow.Cells("PackQty").Value.ToString))
                    ''TASK : TFS1357 romvoved Val() method to add decimal points
                    Comments += " (" & IIf(GridExRow.Cells("Unit").Value.ToString = "Loose", GridExRow.Cells("Qty").Value.ToString, GridExRow.Cells("Qty").Value.ToString * Val(GridExRow.Cells("PackQty").Value.ToString))

                End If
                If flgCommentPriceFormat = True AndAlso flgCommentQtyFormat = True Then
                    'Task No 2608 Update The One Line Code To Get The RoundOff Functionality
                    'Comments += " X " & Val(GridExRow.Cells("Price").Value.ToString) & ")"
                    Comments += " X " & Math.Round(Val(GridExRow.Cells("Price").Value.ToString), DecimalPointInValue) & ")"
                    'End Task 2608
                    'ElseIf flgCommentPriceFormat = True Then
                    '    Comments += " " & Val(GridExRow.Cells("Price").Value.ToString)
                End If
                If flgCommentRemarksFormat = True Then
                    If Me.txtRemarks.Text.Length > 0 Then Comments += " " & Me.txtRemarks.Text.Replace("'", "''")
                End If

                If flgCommentEngineNo = True Then
                    If GridExRow.Cells(GrdEnum.Engine_No).Value.ToString.Length > 0 Then Comments += " Engin No: " & GridExRow.Cells(GrdEnum.Engine_No).Value.ToString & ","
                End If
            End If

            Comments += " " & GridExRow.Cells("Comments").Value.ToString & ","
            Comments += " " & GridExRow.Cells("Other Comments").Value.ToString

            Dim str As String = Comments.Substring(Comments.LastIndexOf(","))
            If str.Length > 1 Then
                Comments = Comments
            Else
                Comments = Comments.Substring(0, Comments.LastIndexOf(",") - 1)
            End If

            Return Comments
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task:2369
    'Task:2369 Added Configuration Event
    Private Sub BackgroundWorker3_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Try

            ''Task:2369 Get Comments Configurations
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

            If Not getConfigValueByType("CommentEngineNo").ToString = "Error" Then
                flgCommentEngineNo = getConfigValueByType("CommentEngineNo")
            Else
                flgCommentEngineNo = False
            End If

            'End Task:2369

            If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
                flgLoadAllItems = getConfigValueByType("LoadAllItemsInSales")
            End If

            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
            End If
            ''R929 Added Flag OnetimeSalesReturn
            If Not getConfigValueByType("OnetimeSalesReturn").ToString = "Error" Then
                flgOnetimeSalesReturn = getConfigValueByType("OnetimeSalesReturn")
            End If
            ''End R929

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            'Task:M16 Added Flag Vehicle Identification Info
            If Not getConfigValueByType("flgVehicleIdentificationInfo").ToString = "Error" Then
                flgVehicleIdentificationInfo = getConfigValueByType("flgVehicleIdentificationInfo")
            Else
                flgVehicleIdentificationInfo = False
            End If
            'End Task:M16

            'Task:2432 Added Flag Marge Item 
            If Not getConfigValueByType("flgMargeItem").ToString = "Error" Then
                flgMargeItem = getConfigValueByType("flgMargeItem")
            Else
                flgMargeItem = False
            End If
            'End Task:2432
            'Task:2517 Added Flag Average Rate
            If Not getConfigValueByType("AvgRate").ToString = "Error" Then
                flgAvgRate = getConfigValueByType("AvgRate")
            Else
                flgAvgRate = False
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14
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
            str.Add("Sale Return")
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
            If GetSMSConfig("SMS To Location On Sale Return").Enable = True Then
                Dim strSMSBody As String = String.Empty
                Dim objData As DataTable = CType(Me.grd.DataSource, DataTable)
                Dim dt_Loc As DataTable = objData.DefaultView.ToTable("Default", True, "LocationId")
                Dim drData() As DataRow
                For j As Integer = 0 To dt_Loc.Rows.Count - 1
                    strSMSBody = String.Empty
                    strSMSBody += "Sale Return, Doc No: " & Me.txtPONo.Text & ", Doc Date: " & Me.dtpPODate.Value.ToShortDateString & ", Supplier: " & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString & ", Invoice No: " & Me.txtPurchaseNo.Text & ", Remarks:" & Me.txtRemarks.Text & ", "
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
            If GetSMSConfig("Sale Return").Enable = True Then
                If msg_Confirm(str_ConfirmSendSMSMessage) = False Then Exit Try
                Dim strDetailMessage As String = String.Empty
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    If strDetailMessage.Length = 0 Then
                        strDetailMessage = r.Cells(GrdEnum.Item).Value.ToString & ", PackQty: " & Val(r.Cells(GrdEnum.PackQty).Value.ToString) & ", Qty: " & IIf(r.Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(r.Cells(GrdEnum.Qty).Value.ToString), Val(r.Cells(GrdEnum.Qty).Value.ToString) * Val(r.Cells(GrdEnum.PackQty).Value.ToString))
                    Else
                        strDetailMessage += "," & r.Cells(GrdEnum.Item).Value.ToString & ", PackQty: " & Val(r.Cells(GrdEnum.PackQty).Value.ToString) & ", Qty: " & IIf(r.Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(r.Cells(GrdEnum.Qty).Value.ToString), Val(r.Cells(GrdEnum.Qty).Value.ToString) * Val(r.Cells(GrdEnum.PackQty).Value.ToString))
                    End If
                Next
                Dim objTemp As New SMSTemplateParameter
                Dim obj As Object = GetSMSTemplate("Sale Return")
                If obj IsNot Nothing Then
                    objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                    Dim strMessage As String = objTemp.SMSTemplate
                    strMessage = strMessage.Replace("@AccountTitle", Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString).Replace("@AccountCode", Me.cmbVendor.ActiveRow.Cells("Code").Value.ToString).Replace("@DocumentNo", Me.txtPONo.Text).Replace("@DocumentDate", Me.dtpPODate.Value.ToShortDateString).Replace("@OtherDoc", Me.txtPurchaseNo.Text).Replace("@Remarks", Me.txtRemarks.Text).Replace("@Amount", grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@Quantity", Me.grd.GetTotal(grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@DCNo", Me.txtPurchaseNo.Text).Replace("@SONo", IIf(Me.cmbPo.SelectedIndex > 0, Me.cmbPo.Text, String.Empty)).Replace("@InvParty", Me.txtPurchaseNo.Text).Replace("@CompanyName", CompanyTitle).Replace("@PreviousBalance", _Previous_Balance).Replace("@SIRIUS", "Automated by www.SIRIUS.net").Replace("@DetailInformation", strDetailMessage)
                    SaveSMSLog(strMessage, Me.cmbVendor.ActiveRow.Cells("Mobile").Value.ToString, "Sale Return")

                    If GetSMSConfig("Sale Return").EnabledAdmin = True Then
                        For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                            If strMob.Length > 10 Then
                                SaveSMSLog(strMessage, strMob, "Sale Return Invoice")
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
    Private Sub btnUpdateAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateAll.Click
        Try
            blnUpdateAll = True
            Dim blnStatus As Boolean = False
            Me.btnUpdateAll.Enabled = False
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                Me.grdSaved.Row = r.Position
                Me.txtReceivingID.Text = 0I
                EditRecord()
                Call Update_Record()
                blnStatus = True
            Next
            If blnStatus = True Then msg_Information("Records update successful.") : Me.btnUpdateAll.Enabled = True : RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    '  ''9-6-2015 TASK106151 IMR Function For Update Sales Return Qty In Sales Detail Table
    Public Function SalesInvoicePartialReturn(ByVal IntSalesReturnId As Integer, ByVal SalesInvoiceId As Integer, ByVal Command As OleDb.OleDbCommand, ByVal trans As OleDb.OleDbTransaction) As Boolean
        If SalesInvoiceId <= 0 Then Return False
        'Dim objCommand As New OleDbCommand
        Try

            Command.Transaction = trans
            Command.Connection = trans.Connection
            Command.CommandTimeout = 120
            Command.CommandType = CommandType.Text
            Command.CommandText = ""
            Command.CommandText = "Update SalesDetailTable Set SalesDetailTable.SalesReturnQty =(IsNull(SalesDetailTable.SalesReturnQty,0)+IsNull(a.Sz1,0)), SalesDetailTable.SalesReturnTotalQty =(IsNull(SalesDetailTable.SalesReturnTotalQty,0)+IsNull(a.Qty,0)) From SalesDetailTable, SalesReturnDetailTable a, SalesReturnMasterTable b  WHERE  SalesDetailTable.SalesId = b.POId AND a.SalesReturnId = b.SalesReturnId AND a.ArticleDefId = SalesDetailTable.ArticleDefId And a.LocationId = SalesDetailTable.LocationId And a.ArticleSize = SalesDetailTable.ArticleSize AND a.RefSalesDetailId = SalesDetailTable.SaleDetailId AND b.POId=" & SalesInvoiceId & " AND b.SalesReturnId =" & IntSalesReturnId & ""
            Command.ExecuteNonQuery()


            'trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex

        End Try
    End Function
    ''9-6-2015 TASK106151 IMR Function For Remove Sales Return Qty In Sales Detail Table
    Public Function DeleteInvoicePartialReturn(ByVal IntSalesReturnId As Integer, ByVal SalesInvoiceId As Integer, ByVal Command As OleDb.OleDbCommand, ByVal trans As OleDb.OleDbTransaction) As Boolean
        If SalesInvoiceId <= 0 Then Return False
        'Dim objCommand As New OleDbCommand
        Try

            Command.Transaction = trans
            Command.Connection = trans.Connection
            Command.CommandTimeout = 120
            Command.CommandType = CommandType.Text
            Command.CommandText = ""
            Command.CommandText = "Update SalesDetailTable Set SalesDetailTable.SalesReturnQty =(IsNull(SalesDetailTable.SalesReturnQty,0)-IsNull(a.Sz1,0)), SalesDetailTable.SalesReturnTotalQty = (IsNull(SalesDetailTable.SalesReturnTotalQty,0)-IsNull(a.Qty,0)) From SalesDetailTable, SalesReturnDetailTable a, SalesReturnMasterTable b  WHERE  SalesDetailTable.SalesId = b.POId AND a.SalesReturnId = b.SalesReturnId AND a.ArticleDefId = SalesDetailTable.ArticleDefId And a.LocationId = SalesDetailTable.LocationId And a.ArticleSize = SalesDetailTable.ArticleSize AND a.RefSalesDetailId = SalesDetailTable.SaleDetailId AND b.POId=" & SalesInvoiceId & "  AND b.SalesReturnId =" & IntSalesReturnId & ""
            Command.ExecuteNonQuery()


            'trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex

        End Try
    End Function
    'End TASKM96151

    ''Task# A1-10-06-2015 Added Key Pres event for some textboxes to take just numeric and dot value

    ''Task# A1-10-06-2015 Numeric validation on some textboxes
    Private Sub txtNUM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPackRate.KeyPress, txtStock.KeyPress, txtPackQty.KeyPress, txtQty.KeyPress, txtSchQty.KeyPress, txtDisc.KeyPress, txtRate.KeyPress, txtTotal.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End Task# A1-10-06-2015
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

    Private Sub btnAttachment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAttachment.Click
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
    Private Sub grdSaved_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Val(Me.grdSaved.GetRow.Cells("SalesReturnId").Value.ToString)
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Function UpgradeAvgRate(ByVal JanusRow As Janus.Windows.GridEX.GridEXRow, ByVal trans As OleDb.OleDbTransaction) As Boolean
        Dim Cmd As New OleDb.OleDbCommand

        Try
            Dim dtLastReturnData As New DataTable
            Cmd.Connection = trans.Connection
            Cmd.Transaction = trans
            Cmd.CommandType = CommandType.Text
            Cmd.CommandTimeout = 120


        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Protected Sub AdjustmentSalesReturn(DocID As Integer, objTrans As OleDbTransaction)
        Dim cmd As New OleDbCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        cmd.CommandTimeout = 120
        cmd.CommandType = CommandType.Text
        Try

            If Me.cmbPo.SelectedIndex > 0 Then

                cmd.CommandText = ""
                cmd.CommandText = "Delete From InvoiceAdjustmentTable WHERE VoucherDetailID=" & Val(DocID) & " AND InvoiceType='Sales Return'"
                cmd.ExecuteNonQuery() ''Number of rows effected by delete statement.


                Dim dblTaxAmount As Double = 0D
                cmd.CommandText = "Select IsNull(SUM((IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))),0)  as TaxAmount From SalesReturnDetailTable WHERE SalesReturnID=" & DocID & ""
                dblTaxAmount = cmd.CommandText = ""
                dblTaxAmount = cmd.ExecuteScalar

                Dim dblAmount As Double = 0D
                cmd.CommandText = ""
                cmd.CommandText = "Select IsNull(SalesReturnAmount,0)  as SalesReturnAmount From SalesReturnMasterTable WHERE SalesReturnID=" & DocID & ""
                dblAmount = cmd.CommandText = ""
                dblAmount = cmd.ExecuteScalar

                dblAmount = dblAmount + dblTaxAmount
                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO InvoiceAdjustmentTable(DocNo,DocDate,InvoiceId,InvoiceType,VoucherDetailID,coa_detail_id,AdjustmentAmount,Remarks,UserName,EntryDate) " _
                    & " VALUES(N'" & Me.txtPONo.Text.Replace("'", "''") & "',Convert(DateTime,'" & dtpPODate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & IIf(Me.cmbPo.SelectedIndex = -1, 0, Me.cmbPo.SelectedValue) & ", 'Sales Return', " & Val(DocID) & "," & Me.cmbVendor.Value & "," & dblAmount & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName.Replace("'", "''") & "',Convert(DateTime,GetDate(),102))"
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
                    Me.txtQty.Text = Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)
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

    Private Sub txtDisc_TextChanged(sender As Object, e As EventArgs) Handles txtDisc.TextChanged

    End Sub

    Private Sub txtDisc_LostFocus(sender As Object, e As EventArgs) Handles txtDisc.LostFocus

        Dim disc As Double = 0D
        Double.TryParse(Me.txtDisc.Text.Trim, disc)
        Dim price As Double = 0D
        Double.TryParse(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString, price)
        If Val(disc) <> 0 AndAlso Val(price) <> 0 Then
            If disc > 0 Then
                Me.txtRate.Text = price - ((price / 100) * disc)
            End If
        Else
            Me.txtRate.Text = txtRate.Text
        End If
    End Sub

    Private Sub txtTotal_LostFocus(sender As Object, e As EventArgs) Handles txtTotal.LostFocus
        If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
        If Not Me.cmbItem.ActiveRow.Cells(0).Value > 0 Or Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
        GetDetailTotal()
    End Sub

    Private Sub txtTotal_TextChanged(sender As Object, e As EventArgs) Handles txtTotal.TextChanged

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
                If blnEditMode = True Then
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

                grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = False
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTotalAmount).Visible = False
                Else
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyAmount).Visible = True
                    'Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTaxAmount).Visible = True
                    Me.grd.RootTable.Columns(GrdEnum.CurrencyTotalAmount).Visible = True
                    'Ali Faisal : TFS1494 : Currency from history reset to default
                    If blnEditMode = False Then
                        If Me.cmbCurrency.Enabled = False Then
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
    Private Function GetSingle(ByVal SalesReturnId As Integer)
        Dim Str As String = ""
        Try
            Str = "SELECT Recv.SalesReturnNo, Recv.SalesReturnDate, V.SalesNo, v.SalesDate, vwCOADetail.detail_title as CustomerName, Recv.SalesReturnQty, Recv.SalesReturnAmount, Recv.SalesReturnId," & _
                            " Recv.CustomerCode, EmployeeDefTable.EmployeeName, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId, isnull(Recv.AdjPercent,0) as AdjPercent, isnull(Recv.Adjustment,0) as Adjustment, isnull(Recv.MarketReturns,0) as MarketReturns, ISNULL(Recv.Damage_Budget,0) as Damage_Budget, IsNull(Recv.Post,0) as Post, Isnull(Recv.CostCenterId,0) as CostCenterId, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, tblDefCostCenter.Name, dbo.vwCOADetail.Contact_Email as Email ,Recv.UserName, Recv.UpdateUserName, Recv.LocationID " & _
                            " FROM tblDefCostCenter Right Outer JOIN SalesReturnMasterTable Recv ON tblDefCostCenter.CostCenterID=Recv.CostCenterId INNER JOIN  " & _
                            " vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
                            " EmployeeDefTable ON Recv.EmployeeCode = EmployeeDefTable.EmployeeId  LEFT OUTER JOIN " & _
                            " SalesMasterTable V ON Recv.POId = V.SalesId Where Recv.SalesReturnId = " & SalesReturnId & ""
            Dim dt As DataTable = GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' Against task TFS957. Send notification upon new sales return
    ''' </summary>
    ''' <param name="DocNo"></param>
    ''' <param name="SalesReturnId"></param>
    ''' <param name="RowCount"></param>
    ''' <remarks> This method is created to send notifications to role based charactors</remarks>
    Private Sub SendNotification(ByVal DocNo As String, ByVal SalesReturnId As Integer, ByVal RowCount As Integer)
        Try
            ''TASK:
            ' *** New Segment *** By Muhammad Ameen *** 16-Aug-2017 ***
            '// Adding notification

            '// Creating new object of Notification configuration dal
            '// Dal will be used to get users list for the notification 
            Dim NDal As New NotificationConfigurationDAL

            '// Creating new object of Agrius Notification class
            Dim Notification As New AgriusNotifications

            '// Reference document number
            Notification.ApplicationReference = SalesReturnId

            '// Date of notification
            Notification.NotificationDate = Now

            '// Preparing notification title string
            Notification.NotificationTitle = "New Sales Return [" & DocNo & "]  is added with " & RowCount & " items."

            '// Preparing notification description string
            Notification.NotificationDescription = "New Sales Return [" & DocNo & "] is added by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

            '// Setting source application as refrence in the notification
            Notification.SourceApplication = "Sales Return"



            '// Starting to get users list to add child

            '// Creating notification detail object list
            Dim List As New List(Of NotificationDetail)

            '// Getting users list
            List = NDal.GetNotificationUsers("Sales Return Created")

            ''TASK:957 Added role based notifications
            Dim RolesList As List(Of String) = NDal.GetNotificationRoles1("Sales Return Created")
            If RolesList.Count > 0 Then
                'If Not Me.cmbDeliveryChalan.SelectedValue Is Nothing Then
                '    If Me.cmbDeliveryChalan.SelectedValue > 0 Then
                '        If RolesList.Contains("Delivery Chalan Created By") Then
                '            Dim objDetail As New NotificationDetail
                '            objDetail.NotificationUser = New SecurityUser(Val(CType(Me.cmbDeliveryChalan.SelectedItem, DataRowView).Item("UserId").ToString))
                '            List.Add(objDetail)
                '        End If
                '    End If
                'End If
                If Not Me.cmbPo.SelectedValue Is Nothing Then
                    If Me.cmbPo.SelectedValue > 0 Then
                        If RolesList.Contains("Sales Created By") Then
                            Dim objDetail As New NotificationDetail
                            objDetail.NotificationUser = New SecurityUser(GetUserId(CType(Me.cmbPo.SelectedItem, DataRowView).Item("UserName").ToString))
                            List.Add(objDetail)
                        End If
                    End If
                End If
            End If

            ''End TASK:957


            '// Adding users list in the Notification object of current inquiry
            Notification.NotificationDetils.AddRange(List)

            '// Getting and adding user groups list in the Notification object of current inquiry
            Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("Sales Return Created"))

            '// Not getting role list because no role is associated at the moment
            '// We will need this in future and we can use it later
            '// We can consult to Update function of this class


            '// ***This segment will be used to save notification in database table

            '// Creating new list of objects of Agrius Notification
            Dim NList As New List(Of AgriusNotifications)

            '// Copying notification object from current sales inquiry to newly defined instance
            '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
            NList.Add(Notification)

            '// Creating object of Notification DAL
            Dim GNotification As New NotificationDAL

            '// Saving notification to database
            GNotification.AddNotification(NList)

            '// End Adding Notification

            '// End Adding Notification
            ' *** End Segment ***

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SendNotificationUponUpdate(ByVal DocNo As String, ByVal SalesReturnId As Integer, ByVal RowCount As Integer)
        Try
            ''TASK:
            ' *** New Segment *** By Muhammad Ameen *** 16-Aug-2017 ***
            '// Adding notification

            '// Creating new object of Notification configuration dal
            '// Dal will be used to get users list for the notification 
            Dim NDal As New NotificationConfigurationDAL

            '// Creating new object of Agrius Notification class
            Dim Notification As New AgriusNotifications

            '// Reference document number
            Notification.ApplicationReference = SalesReturnId

            '// Date of notification
            Notification.NotificationDate = Now

            '// Preparing notification title string
            Notification.NotificationTitle = " Sales Return [" & DocNo & "]  is updated with " & RowCount & " items."

            '// Preparing notification description string
            Notification.NotificationDescription = " Sales Return [" & DocNo & "] is updated by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

            '// Setting source application as refrence in the notification
            Notification.SourceApplication = "Sales Return"



            '// Starting to get users list to add child

            '// Creating notification detail object list
            Dim List As New List(Of NotificationDetail)

            '// Getting users list
            List = NDal.GetNotificationUsers("Sales Return Created")

            ''TASK:957 Added role based notifications
            Dim RolesList As List(Of String) = NDal.GetNotificationRoles1("Sales Return Created")
            If RolesList.Count > 0 Then
                'If Not Me.cmbDeliveryChalan.SelectedValue Is Nothing Then
                '    If Me.cmbDeliveryChalan.SelectedValue > 0 Then
                '        If RolesList.Contains("Delivery Chalan Created By") Then
                '            Dim objDetail As New NotificationDetail
                '            objDetail.NotificationUser = New SecurityUser(Val(CType(Me.cmbDeliveryChalan.SelectedItem, DataRowView).Item("UserId").ToString))
                '            List.Add(objDetail)
                '        End If
                '    End If
                'End If
                If Not Me.cmbPo.SelectedValue Is Nothing Then
                    If Me.cmbPo.SelectedValue > 0 Then
                        If RolesList.Contains("Sales Changed By") Then
                            Dim objDetail As New NotificationDetail
                            objDetail.NotificationUser = New SecurityUser(GetUserId(CType(Me.cmbPo.SelectedItem, DataRowView).Item("UserName").ToString))
                            List.Add(objDetail)
                        End If
                    End If
                End If
            End If

            ''End TASK:957


            '// Adding users list in the Notification object of current inquiry
            Notification.NotificationDetils.AddRange(List)

            '// Getting and adding user groups list in the Notification object of current inquiry
            Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("Sales Return Created"))

            '// Not getting role list because no role is associated at the moment
            '// We will need this in future and we can use it later
            '// We can consult to Update function of this class


            '// ***This segment will be used to save notification in database table

            '// Creating new list of objects of Agrius Notification
            Dim NList As New List(Of AgriusNotifications)

            '// Copying notification object from current sales inquiry to newly defined instance
            '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
            NList.Add(Notification)

            '// Creating object of Notification DAL
            Dim GNotification As New NotificationDAL

            '// Saving notification to database
            GNotification.AddNotification(NList)

            '// End Adding Notification

            '// End Adding Notification
            ' *** End Segment ***

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'TFS4722: Waqar: Added Print Event with existing print event
    Private Sub PrintVoucherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintVoucherToolStripMenuItem.Click, PrintVoucherToolStripMenuItem1.Click
        Try
            GetVoucherPrint(Me.grdSaved.CurrentRow.Cells("SalesReturnNo").Value.ToString, Me.Name, BaseCurrencyName, BaseCurrencyId)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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
                                r("Qty") = Val(dr(0).Item("Qty")) + Val(Me.txtQty.Text)
                                r("TotalQty") = r("Qty") * Val(Me.txtPackQty.Text)
                                r.EndEdit()
                            Else
                                r.BeginEdit()
                                r("Qty") = Val(dr(0).Item("Qty")) + Val(Me.txtQty.Text)
                                r("TotalQty") = r("Qty")
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
        Catch ex As Exception
            Throw ex
        End Try
    End Function
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
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Return"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetVoucherNo() As String
        Dim docNo As String = String.Empty
        Dim VType As String = String.Empty
        If Me.cmbMethod.SelectedIndex > 0 Then
            ''Start TFS2611
            VType = "BPV" & IIf(getConfigValueByType("CompanyWisePrefix").ToString = "True", Me.cmbCompany.SelectedValue, String.Empty)
        Else
            VType = "CPV" & IIf(getConfigValueByType("CompanyWisePrefix").ToString = "True", Me.cmbCompany.SelectedValue, String.Empty)
            ''End TFS2611
        End If
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
            Else
                Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
                Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
                If Not dr Is Nothing Then
                    If dr("config_Value") = "Monthly" Then
                        Return GetNextDocNo(VType & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
                    Else
                        docNo = GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
                        Return docNo
                    End If
                Else
                    docNo = GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
                    Return docNo
                End If
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMethod.SelectedIndexChanged
        Try
            Dim str As String = String.Empty
            If Me.cmbMethod Is Nothing Then Exit Sub
            str = "If  exists(select Account_Id FROM tblUserAccountRights where UserID = " & LoginUserId & " And Rights = 1 And Account_Id Is Not Null) " _
                    & " select coa_detail_id, detail_title from vwCoaDetail where account_type=N'" & Me.cmbMethod.Text & "' And coa_detail_id in (select Account_Id FROM tblUserAccountRights where UserID = " & LoginUserId & " And Rights = 1) " _
                    & " Else " _
                    & " select coa_detail_id, detail_title from vwCoaDetail where account_type=N'" & Me.cmbMethod.Text & "'"
            FillDropDown(Me.cmbDepositAccount, str, True)
            If Me.cmbMethod.SelectedIndex = 0 Then
                Me.GroupBox7.Visible = False
            Else
                Me.GroupBox7.Visible = True
            End If
            Me.txtVoucherNo.Text = GetVoucherNo()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' TAKS TFS4730
    ''' </summary>
    ''' <remarks></remarks>
    Sub FillPaymentMethod()

        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dr1 As DataRow
        dt.Columns.Add("Id")
        dt.Columns.Add("Name")
        dr = dt.NewRow
        dr1 = dt.NewRow

        dr(0) = Convert.ToInt32(5)
        dr(1) = "Bank"
        dt.Rows.InsertAt(dr, 0)

        dr1(0) = Convert.ToInt32(3)
        dr1(1) = "Cash"
        dt.Rows.InsertAt(dr1, 0)
        Me.cmbMethod.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
        Me.cmbMethod.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
        Me.cmbMethod.DataSource = dt

    End Sub
    Private Function SavePaymentVoucher(ByVal objCommand As OleDb.OleDbCommand, ByVal VoucherNo As String) As Boolean
        Dim VoucherId As Integer = 0
        Try
            objCommand.CommandText = ""
            objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                       & " cheque_no, cheque_date,post,Source,voucher_code, coa_detail_id, Employee_Id, Reference, UserName, other_voucher )" _
                       & " VALUES(" & Me.cmbCompany.SelectedValue & ", 1,  " & Convert.ToInt32(Me.cmbMethod.SelectedValue) & ", N'" & VoucherNo & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                       & " " & IIf(Me.txtChequeNo.Visible = True, "N'" & Me.txtChequeNo.Text.Replace("'", "''") & "'", "NULL") & ", " & IIf(Me.dtpChequeDate.Visible = True, "N'" & dtpChequeDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", '" & IIf(PaymentPost = True, 1, 0) & "' ,N'" & Me.Name & "',N'" & Me.txtPONo.Text.Replace("'", "''") & "', " & Me.cmbDepositAccount.SelectedValue & ", " & IIf(Me.cmbSalesMan.SelectedIndex = -1, 0, Me.cmbSalesMan.SelectedValue) & ", " _
                       & " N'" & Me.txtPONo.Text & " " & Me.cmbVendor.Text.ToString & "', '" & LoginUserName & "', 0  )" _
                       & " SELECT @@IDENTITY"
            VoucherId = objCommand.ExecuteScalar
            objCommand.CommandText = ""
            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                   & " VALUES(" & VoucherId & ", " & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbProject.SelectedValue) & ", " & Me.cmbDepositAccount.SelectedValue & ", 0, " & Math.Round(Val(Me.txtRecAmount.Text), DecimalPointInValue) & ",  N'Payment Ref By " & Me.cmbVendor.Text.Replace("'", "''") & ": " & Me.txtPONo.Text.Replace("'", "''") & ": " & Me.txtRemarks.Text.Replace("'", "''") & "', " & IIf(Me.cmbProject.SelectedValue = Nothing, 0, Me.cmbProject.SelectedValue) & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
            objCommand.ExecuteNonQuery()
            objCommand.CommandText = ""
            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                 & " VALUES(" & VoucherId & ", " & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbProject.SelectedValue) & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ",  " & Math.Round(Val(Me.txtRecAmount.Text), DecimalPointInValue) & ", " & 0 & ",  N'Payment Ref: " & Me.txtPONo.Text.Replace("'", "''") & ": " & Me.txtRemarks.Text.Replace("'", "''") & "', " & IIf(Me.cmbProject.SelectedValue = Nothing, 0, Me.cmbProject.SelectedValue) & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
            objCommand.ExecuteNonQuery()
            objCommand.CommandText = " if exists (select COUNT(*) from VoucherApprovalGroupSetting) " _
                                   & " if not exists (select * from VoucherApprovedLog where voucher_id=" & Val(VoucherId) & ") " _
                                   & " insert into VoucherApprovedLog (Voucher_Id , UserGroupId ,VALstatus,UserId ,UserName,ModificationDate ) values (" & Val(VoucherId) & ",1,'Pending', " & LoginUserId & ",'" & LoginUserName & "',GETDATE())"
            objCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function UpdatePaymentVoucher(ByVal objCommand As OleDb.OleDbCommand, ByVal VoucherNo As String) As Boolean
        Try
            If VoucherId > 0 Then
                objCommand.CommandText = " DELETE FROM tblVoucher WHERE voucher_id = " & VoucherId & ""
                objCommand.ExecuteNonQuery()
                objCommand.CommandText = " DELETE FROM tblVoucherDetail WHERE voucher_id = " & VoucherId & ""
                objCommand.ExecuteNonQuery()
            End If
            'objCommand.CommandText = ""
            'objCommand.CommandText = "UPDATE tblVoucher SET location_id = " & Me.cmbCompany.SelectedValue & ", finiancial_year_id = 1, voucher_type_id = " & Convert.ToInt32(Me.cmbMethod.SelectedValue) & ", " _
            '                    & " voucher_no=N'" & VoucherNo & "', voucher_date = N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '           & " cheque_no = " & IIf(Me.txtChequeNo.Text.Length > 0, "N'" & Me.txtChequeNo.Text.Replace("'", "''") & "'", "NULL") & ", " _
            '           & " cheque_date = " & IIf(Me.GroupBox7.Visible = True, "N'" & dtpChequeDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", " _
            '           & " post = '" & IIf(PaymentPost = True, 1, 0) & "', Source = N'" & Me.Name & "',voucher_code = N'" & Me.txtPONo.Text.Replace("'", "''") & "', " _
            '           & " coa_detail_id = " & Me.cmbDepositAccount.SelectedValue & ", Employee_Id = " & IIf(Me.cmbSalesMan.SelectedIndex = -1, 0, Me.cmbSalesMan.SelectedValue) & ", " _
            '           & " Reference = N'" & Me.txtPONo.Text & " " & Me.cmbVendor.Text.ToString & "', UserName = '" & LoginUserName & "', other_voucher = 0 WHERE voucher_id= " & VoucherId & ""
            objCommand.CommandText = ""
            objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                       & " cheque_no, cheque_date,post,Source,voucher_code, coa_detail_id, Employee_Id, Reference, UserName, other_voucher )" _
                       & " VALUES(" & Me.cmbCompany.SelectedValue & ", 1,  " & Convert.ToInt32(Me.cmbMethod.SelectedValue) & ", N'" & VoucherNo & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                       & " " & IIf(Me.txtChequeNo.Visible = True, "N'" & Me.txtChequeNo.Text.Replace("'", "''") & "'", "NULL") & ", " & IIf(Me.dtpChequeDate.Visible = True, "N'" & dtpChequeDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", '" & IIf(PaymentPost = True, 1, 0) & "' ,N'" & Me.Name & "',N'" & Me.txtPONo.Text.Replace("'", "''") & "', " & Me.cmbDepositAccount.SelectedValue & ", " & IIf(Me.cmbSalesMan.SelectedIndex = -1, 0, Me.cmbSalesMan.SelectedValue) & ", " _
                       & " N'" & Me.txtPONo.Text & " " & Me.cmbVendor.Text.ToString & "', '" & LoginUserName & "', 0  )" _
                       & " SELECT @@IDENTITY"
            VoucherId = objCommand.ExecuteScalar
            'objCommand.CommandText = " DELETE FROM tblVoucherDetail WHERE voucher_id IN (SELECT voucher_id FROM tblVoucher WHERE voucher_no= N'" & VoucherNo & "' )"
            objCommand.CommandText = " DELETE FROM tblVoucherDetail WHERE voucher_id = " & VoucherId & ""

            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""
            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                   & " VALUES(" & VoucherId & ", " & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbProject.SelectedValue) & ", " & Me.cmbDepositAccount.SelectedValue & ", 0, " & Math.Round(Val(Me.txtRecAmount.Text), DecimalPointInValue) & ",  N'Payment Ref By " & Me.cmbVendor.Text.Replace("'", "''") & ": " & Me.txtPONo.Text.Replace("'", "''") & ": " & Me.txtRemarks.Text.Replace("'", "''") & "', " & IIf(Me.cmbProject.SelectedValue = Nothing, 0, Me.cmbProject.SelectedValue) & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
            objCommand.ExecuteNonQuery()
            objCommand.CommandText = ""
            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                 & " VALUES(" & VoucherId & ", " & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbProject.SelectedValue) & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ",  " & Math.Round(Val(Me.txtRecAmount.Text), DecimalPointInValue) & ", " & 0 & ",  N'Payment Ref: " & Me.txtPONo.Text.Replace("'", "''") & ": " & Me.txtRemarks.Text.Replace("'", "''") & "', " & IIf(Me.cmbProject.SelectedValue = Nothing, 0, Me.cmbProject.SelectedValue) & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
            objCommand.ExecuteNonQuery()
            objCommand.CommandText = " if exists (select COUNT(*) from VoucherApprovalGroupSetting) " _
                                   & " if not exists (select * from VoucherApprovedLog where voucher_id=" & Val(VoucherId) & ") " _
                                   & " insert into VoucherApprovedLog (Voucher_Id , UserGroupId ,VALstatus,UserId ,UserName,ModificationDate ) values (" & Val(VoucherId) & ",1,'Pending', " & LoginUserId & ",'" & LoginUserName & "',GETDATE())"
            objCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub VoucherDetail(ByVal SalesNo As String, Optional ByVal cmd As OleDb.OleDbCommand = Nothing)
        Try
            Dim str As String = String.Empty
            Dim dt As DataTable
            str = "SELECT dbo.tblVoucher.voucher_id, dbo.tblVoucher.location_id, dbo.tblVoucher.voucher_code, dbo.tblVoucher.voucher_type_id, dbo.tblVoucher.voucher_no, " _
                  & " dbo.tblVoucherDetail.credit_amount, tblVoucherDetail.debit_amount, dbo.tblVoucherDetail.CostCenterID, tblVoucher.coa_detail_id, tblVoucher.Cheque_No, tblVoucher.Cheque_Date " _
                  & " FROM  dbo.tblVoucher INNER JOIN " _
                  & " dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id " _
                  & " WHERE (dbo.tblVoucher.voucher_type_id = 3 Or dbo.tblVoucher.voucher_type_id=5) AND (tblVoucherDetail.coa_detail_id=" & Me.cmbVendor.Value & ") AND (dbo.tblVoucher.voucher_Code = N'" & SalesNo & "')"
            'If cmd IsNot Nothing Then
            '    SQLHelper.Get_DataTable()
            '    Dim Adop As New OleDb.OleDbDataAdapter()
            '    cmd.CommandText = ""
            '    cmd.CommandText = str
            '    Adop.SelectCommand = cmd
            '    dt = New DataTable()
            '    Adop.Fill(dt)
            '    dt.AcceptChanges()
            'Else
            dt = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Me.cmbMethod.SelectedValue = Convert.ToInt32(dt.Rows(0).ItemArray(3))
                    Me.cmbMethod.Enabled = False
                    Me.cmbDepositAccount.SelectedValue = Convert.ToInt32(dt.Rows(0).ItemArray(8))

                    If Not IsDBNull(dt.Rows(0).ItemArray(9)) Then
                        Me.txtChequeNo.Text = dt.Rows(0).ItemArray(9).ToString
                    Else
                        Me.txtChequeNo.Text = String.Empty
                    End If

                    If Not IsDBNull(dt.Rows(0).ItemArray(10)) Then
                        Me.dtpChequeDate.Value = Convert.ToDateTime(dt.Rows(0).ItemArray(10))
                    Else
                        Me.dtpChequeDate.Value = Date.Today
                    End If

                    Me.txtVoucherNo.Text = dt.Rows(0).ItemArray(4)
                    Me.txtVoucherNo.Enabled = False
                    Me.txtRecAmount.Text = Convert.ToDouble(dt.Rows(0).ItemArray(6))
                    VNo = dt.Rows(0).ItemArray(4)
                    VoucherId = dt.Rows(0).ItemArray(0)
                    'Me.cmbMethod.Enabled = False
                    ExistingVoucherFlg = True
                Else
                    Me.cmbMethod.Enabled = True
                    Me.txtVoucherNo.Enabled = True
                    Me.cmbMethod.SelectedIndex = 0
                    cmbDepositAccount.SelectedIndex = 0
                    Me.txtChequeNo.Text = String.Empty
                    Me.dtpChequeDate.Value = Date.Today
                    VoucherId = 0I
                    VNo = String.Empty
                    Me.txtVoucherNo.Text = String.Empty
                    Me.txtRecAmount.Text = String.Empty
                    ExistingVoucherFlg = False
                End If
            Else
                Me.cmbMethod.Enabled = True
                Me.txtVoucherNo.Enabled = True
                Me.cmbMethod.SelectedIndex = 0
                cmbDepositAccount.SelectedIndex = 0
                Me.txtChequeNo.Text = String.Empty
                Me.dtpChequeDate.Value = Date.Today
                VoucherId = 0I
                VNo = String.Empty
                Me.txtVoucherNo.Text = String.Empty
                Me.txtRecAmount.Text = String.Empty
                ExistingVoucherFlg = False
            End If
        Catch ex As Exception

        End Try
    End Sub




    Private Sub txtRecAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRecAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalAmount_TextChanged(sender As Object, e As EventArgs) Handles txtTotalAmount.TextChanged
        If Val(txtTotalAmount.Text) > 0 Then
            Me.txtRecAmount.Text = txtTotalAmount.Text
        End If
    End Sub

    Private Sub txtRecAmount_TextChanged(sender As Object, e As EventArgs) Handles txtRecAmount.TextChanged
        'If CDec(txtRecAmount.Text) > CDec(Me.txtTotalAmount.Text) Then
        '    msg_Error("You can not enter amount greator than total amount.")
        '    txtRecAmount.Text = Me.txtTotalAmount.Text
        'End If
    End Sub

    Private Sub txtRecAmount_Leave(sender As Object, e As EventArgs) Handles txtRecAmount.Leave
        If CDec(txtRecAmount.Text) > CDec(Me.txtTotalAmount.Text) Then
            msg_Error("You can not enter amount greator than total amount.")
            txtRecAmount.Text = Me.txtTotalAmount.Text
        End If
    End Sub

    Private Sub frmSalesReturn_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    'Change by Murtaza for txtcurrencyrate text change (11/09/2022) 
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
