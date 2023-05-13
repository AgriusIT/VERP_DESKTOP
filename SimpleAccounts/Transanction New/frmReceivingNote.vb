'' 10-Dec-2013 ReqId-899 By Imran Tax on Purchase order
'' 12-Dec-2013 ReqId-915 Imran Ali error at add item in purchase 
''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction forms
''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''19-Dec-2013 R:945 , TaskId:2338        M Ijaz Javed           By default pack rupees
''11-Jan-2014 Task:2373         Imran Ali                Add Columns SubSub Title in Account List on Sales/Purchase
''31-Jan-2014     TASK:2401            Imran ali                    Store Issuence Problem 
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
''03-Feb-2014        Task:2406   Imran Ali    FIELD CHOOSER restriction (Senior Rozgar)
''03-Feb-2014          Task:2407    Imran Ali       unit of measurement on GRN and purchase window.
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
'08-Jun-2015  Task#2015060005 to allow all files to attach
''10-Jun-02015 Task# A2-10-06-2015 Ahmad Sharif: Added Key Pres event for some textboxes to take just numeric and dot value
''10-Jun-2015 Task# A1-10-06-2015 Ahmad Sharif: Add Check on grdSaved to check on double click if row less than zero than exit
'22-Jul-2015 Task# 201507018 For Allowing multiple multiple purchase order configration Ali Ansari
''19-9-2015 TASK16 Imran Ali: Load PO Against Posted Purchase Order.
''13-Nov-2015 'TASK-TFS-51 Added Fields AdTax_Percent, AdTax_Amount
''TASK : TFS1261 Muhammad Ameen on 15-08-2017. Purchase Order LC should be loaded to GRG then said GRN should be loaded to Import Document.
'' TASK TFS1592 Ayesha Rehman on 25-10-2017 Future date entry should be rights based
'' TASK TFS2375 Ayesha Rehman on 26-02-2018 Approval Hierarchy for All Transactional documents 
''TFS2988 Ayesha Rehman : 09-04-2018 : If document is approved from one stage then it should not change able for previous stage
''TFS2989 Ayesha Rehman : 10-04-2018 : If document is rejected from one stage then it should open for previous stage for approval
''TFS4161 Ayesha Rehman : 09-08-2018 : P QTY: (Should Be Static/ Un-Changeable / Un-Editable on All Screens)
''TFS4689 Ayesha Rehman : 03-10-2018 : Show only relevant Accounts on Transactional screens while User wise COA Configuration.
''TFS4705 Ayesha Rehman : 08-10-2018 : Pack Unit & Pack Size issue on PO, GRN and Purchase.
Imports System.Data.OleDb
Imports SBModel
Imports SBDal
Imports SBDal.UtilityDAL
Imports SBDal.StockDAL
Imports SBDal.StockDocTypeDAL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports SBUtility.Utility

Public Class frmReceivingNote

    'Rq_Id = 828, Discount on Purchase, Change on 18-Sep-13
    Implements IGeneral
    Dim dt As DataTable
    Dim intRecvNoteId As Integer = 0
    Dim IsEditMode As Boolean = False
    Dim Mode = "Normal"
    Dim IsFormLoaded As Boolean = False
    Dim StockMaster As StockMaster
    Dim StockDetail As StockDetail
    Dim RecQtyBeforePOAccess As Double
    Dim Email As Email
    Dim VNo As String = String.Empty
    Dim ExistingVoucherFlg As Boolean = False
    Dim VoucherId As Integer = 0
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim setVoucherNo As String = String.Empty
    Dim getVoucher_Id As Integer = 0
    Dim Total_Amount As Double = 0D
    Dim setEditMode As Boolean = False
    Dim crpt As New ReportDocument
    Dim Previouse_Amount As Double = 0D
    'Marked Against Task#2015060001 Ali Ansari
    'Dim arrfile As String
    'Marked Against Task#2015060001 Ali Ansari
    'Altered Against Task#2015060001 Ali Ansari
    ' Convert string into List of string for making proper count
    Dim arrFile As New List(Of String)
    'Altered Against Task#2015060001 Ali Ansari
    Dim TaxAmount As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim flgCurrenyonOpenLC As Boolean = False
    Dim flgSelectProject As Boolean = False
    Dim flgCompanyRights As Boolean = False
    Dim flgTransaportationChargesVoucher As Boolean = False
    Dim objRateList As List(Of Double)
    Dim StockList As List(Of StockDetail)
    Dim IsLoaded As Boolean = False
    Dim FlgPO As Boolean = False
    Dim dblRate As Double = 0D
    Dim dblCurrentRate As Double = 0D
    Dim dblRateDiscPercent As Double = 0D
    Dim dblTaxPercent As Double = 0D
    Dim dblAdTaxPercent As Double = 0D
    Dim strComments As String = String.Empty
    Dim NotificationDAL As New NotificationTemplatesDAL
    Dim flgAvgRate As Boolean = False
    ''TFS2375 : Ayesha Rehman : This Variable is Added to check ApprovalProcessId ,if it is mapped against the document
    Dim ApprovalProcessId As Integer = 0
    ''TFS2375 : Ayesha Rehman :End
    'Code Edit for future date rights
    Dim IsDateChangeAllowed As Boolean = False
    Dim flgAddItemForMall As Boolean = False ''TFS3762 
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Dim blnOrderQtyExceed As Boolean = False 'Task:4362 declare variable for order qty exceed against GRN
    Dim ItemFilterByName As Boolean = False
    Dim GRNStockImpact As Boolean = False
    Dim BaseCurrencyId As Integer = 0
    Dim BaseCurrencyName As String = String.Empty
    Dim CurrencyRate As Double = 1
    Dim ClosingDate As String

    Enum grdEnm
        LocationId
        'ArticleID 'Task# 201507018 For adding Article id for validation Ali Ansari
        ArticleCode
        Item
        AlternativeItem
        Color
        Size
        Uom 'Task :2407 added index
        Unit
        X_Tray_Weights
        X_Gross_Weights
        X_Net_Weights
        Y_Gross_Weights
        Y_Tray_Weights
        Y_Net_Weights
        ReceivedQty
        RejectedQty
        Qty
        GrossQty
        Column1
        Column2
        Column3
        TotalQty ''TASK-408
        VendorQty
        Deduction
        VendorNetQty
        CurrentPrice
        RateDiscPercent
        Price
        Total
        TaxPercent
        TaxAmount
        AdTax_Percent
        AdTax_Amount
        TotalAmount
        Transportation_Charges
        Custom_Charges
        Discount_Price
        ArticleGroupId
        ArticleDefId
        PackQty
        'CurrentPrice
        PackPrice
        BatchId
        ExpiryDate
        BatchNo
        Origin
        Comments
        Pack_Desc
        AccountId
        PO_ID
        PurchaseOrderDetailId
        ReceivingNoteDetailId
        ButtonDelete
        'Change by Murtaza for txtcurrencyrate text change (11/09/2022)
        CurrencyAmount
        BaseCurrencyRate
        CurrencyRate
        'Change by Murtaza for txtcurrencyrate text change (11/09/2022)
        AlternativeItemId
    End Enum

    Private Sub frmReceivingNote_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

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
    Private Sub frmReceivingNote_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
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
            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)
            If Not getConfigValueByType("CurrencyonOpenLC").ToString = "Error" Then
                flgCurrenyonOpenLC = Convert.ToBoolean(getConfigValueByType("CurrencyonOpenLC").ToString)
            End If

            If Not getConfigValueByType("TransaportationChargesVoucher").ToString = "Error" Then
                flgTransaportationChargesVoucher = getConfigValueByType("TransaportationChargesVoucher")
            End If
            If Not getConfigValueByType("AddItemForMall").ToString = "Error" Then
                flgAddItemForMall = Convert.ToBoolean(getConfigValueByType("AddItemForMall").ToString)
            End If
            ClosingDate = getConfigValueByType("EndOfDate").ToString
            ''start TFS4161
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            If Not getConfigValueByType("PurchaseDiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("PurchaseDiablePackQuantity").ToString)
            End If
            ''End TFS4161

            'TAsk:4362 Set ByRef Variable blnOrderQtyExceed
            If Not getConfigValueByType("OrderQtyExceedAgainstGRN").ToString = "Error" Then
                blnOrderQtyExceed = getConfigValueByType("OrderQtyExceedAgainstGRN").ToString
            Else
                blnOrderQtyExceed = False
            End If
            'End Task:4362
            ' ''TASK TFS4544
            'If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            '    ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            'End If
            ' ''END TFS4544
            FillCombo("Category")
            FillCombo("Vendor")
            FillCombo("CostCenter")
            FillCombo("Item")
            FillCombo("SM")
            FillCombo("Currency")
            FillCombo("TransportationVendor")
            FillCombo("CustomVendor")
            FillCombo("LC")
            'FillCombo("ArticlePack") R933 Commented
            'FillCombo("Company")
            'DisplayRecord()
            Dim dtColSettings As DataTable = GetDataTable("Select * From InventoryColumnSettings")
            dtColSettings.AcceptChanges()
            If (dtColSettings.Rows.Count > 0 _
                ) Then

                Me.lblColumn1.Text = dtColSettings.Rows(0)("Column1").ToString()
                Me.lblColumn2.Text = dtColSettings.Rows(0)("Column2").ToString()
                Me.lblColumn3.Text = dtColSettings.Rows(0)("Column3").ToString()
                Me.txtColumn1.Enabled = dtColSettings.Rows(0)("Column1Visibility")
                Me.txtColumn2.Enabled = dtColSettings.Rows(0)("Column2Visibility")
                Me.txtColumn3.Enabled = dtColSettings.Rows(0)("Column3Visibility")
            End If
            'If frmModProperty.blnListSeachStartWith = True Then
            '    cmbItem.AutoCompleteMode = Win.AutoCompleteMode.Suggest
            '    cmbItem.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.StartsWith
            'End If

            'If frmModProperty.blnListSeachContains = True Then
            '    cmbItem.AutoCompleteMode = Win.AutoCompleteMode.Suggest
            '    cmbItem.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.Contains
            'End If
            RefreshControls()
            'Me.cmbVendor.Focus()
            Me.dtpPODate.Focus()
            'DisplayDetail(-1)
            'Me.DisplayRecord() R933 Commented History Data
            '//This will hide Master grid
            Me.grdSaved.Visible = CType(getConfigValueByType("ShowMasterGrid"), Boolean)
            IsFormLoaded = True
            Get_All(frmModProperty.Tags)
            'TFS3360
            UltraDropDownSearching(cmbVendor, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbTransportationVendor, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbCustom, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
            If frmModProperty.Tags.Length > 0 Then frmModProperty.Tags = String.Empty ''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
        End Try
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If GetConfigValue("LoadMultiPO").ToString = "True" Then
                FlgPO = True
                btnLoad.Enabled = True
                IsLoaded = True
            Else
                FlgPO = False
                btnLoad.Enabled = False
                IsLoaded = False
            End If
            ApplyGridSettings()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
        Dim str As String = String.Empty

        'str = "SELECT     Recv.ReceivingNo, Recv.ReceivingDate AS Date, vwCOADetail.detail_title as CustomerName, V.PurchaseOrderNo, Recv.ReceivingQty, Recv.ReceivingAmount, Recv.ReceivingNoteId,  " & _
        '        "Recv.CustomerCode, EmployeeDefTable.EmployeeName, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId " & _
        '        "FROM         ReceivingNoteMasterTable Recv INNER JOIN " & _
        '        "vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
        '        "EmployeeDefTable ON Recv.EmployeeCode = EmployeeDefTable.EmployeeId LEFT OUTER JOIN " & _
        '        "PurchaseOrderMasterTable V ON Recv.POId = V.PurchaseOrderId " & _
        '        "ORDER BY Recv.ReceivingNo DESC"

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
            'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.ReceivingNo, Recv.ReceivingDate AS Date, dbo.PurchaseOrderMasterTable.PurchaseOrderNo, V.detail_title, Recv.ReceivingQty, " & _
            '        "Recv.ReceivingAmount, Recv.ReceivingNoteId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
            '        "Recv.PurchaseOrderID, Recv.DcNo, Isnull(Recv.Total_Discount_Amount,0) as Total_Discount_Amount, IsNull(Recv.Post, 0) as Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, Recv.Vehicle_No, Recv.Driver_Name, Recv.DcDate,  Recv.Vendor_Invoice_No, Isnull(Recv.CostCenterId,0) as CostCenterId, Recv.IGPNo, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], isnull(Recv.CurrencyType,0) as CurrencyType, isnull(Recv.CurrencyRate,0) as CurrencyRate, ISNULL(Recv.Transportation_Vendor,0) as Transportation_Vendor " & _
            '        "FROM dbo.ReceivingNoteMasterTable Recv INNER JOIN " & _
            '        "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
            '        "dbo.PurchaseOrderMasterTable ON Recv.PurchaseOrderID = dbo.PurchaseOrderMasterTable.PurchaseOrderId LEFT OUTER JOIN (Select DISTINCT ReceivingNoteId, LocationId From ReceivingNoteDetailTable) as Location On Location.ReceivingNoteId = Recv.ReceivingNoteId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.ReceivingNo  " _
            '       & " where Recv.ReceivingNo like 'GRN%' " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.ReceivingDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
            'Marked Against Task#2015060006  to display attachements
            'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.ReceivingNo, Recv.ReceivingDate AS Date, dbo.PurchaseOrderMasterTable.PurchaseOrderNo, V.detail_title, Recv.ReceivingQty, " & _
            '        "Recv.ReceivingAmount, Recv.ReceivingNoteId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
            '        "Recv.PurchaseOrderID, Recv.DcNo, Isnull(Recv.Total_Discount_Amount,0) as Total_Discount_Amount, IsNull(Recv.Post, 0) as Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, Recv.Vehicle_No, Recv.Driver_Name, Recv.DcDate,  Recv.Vendor_Invoice_No, Isnull(Recv.CostCenterId,0) as CostCenterId, Recv.IGPNo, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], isnull(Recv.CurrencyType,0) as CurrencyType, isnull(Recv.CurrencyRate,0) as CurrencyRate, ISNULL(Recv.Transportation_Vendor,0) as Transportation_Vendor, V.Contact_Email as Email " & _
            '        "FROM dbo.ReceivingNoteMasterTable Recv INNER JOIN " & _
            '        "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
            '        "dbo.PurchaseOrderMasterTable ON Recv.PurchaseOrderID = dbo.PurchaseOrderMasterTable.PurchaseOrderId LEFT OUTER JOIN (Select DISTINCT ReceivingNoteId, LocationId From ReceivingNoteDetailTable) as Location On Location.ReceivingNoteId = Recv.ReceivingNoteId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.ReceivingNo  " _
            '   & " where Recv.ReceivingNo like 'GRN%' " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.ReceivingDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
            'Altered Against Task#2015060006  to display attachements
            'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.ReceivingNo, Recv.ReceivingDate AS Date, dbo.PurchaseOrderMasterTable.PurchaseOrderNo, V.detail_title, Recv.ReceivingQty, " & _
            '        "Recv.ReceivingAmount, Recv.ReceivingNoteId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
            '        "Recv.PurchaseOrderID, Recv.DcNo, Isnull(Recv.Total_Discount_Amount,0) as Total_Discount_Amount, IsNull(Recv.Post, 0) as Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, Recv.Vehicle_No, Recv.Driver_Name, Recv.DcDate,  Recv.Vendor_Invoice_No, Isnull(Recv.CostCenterId,0) as CostCenterId, Recv.IGPNo, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], isnull(Recv.CurrencyType,0) as CurrencyType, isnull(Recv.CurrencyRate,0) as CurrencyRate, ISNULL(Recv.Transportation_Vendor,0) as Transportation_Vendor, V.Contact_Email as Email,IsNull([No Of Attachment],0) as [No Of Attachment],Recv.Purchaseorderid as PO_ID,Recv.UpdateUserName as [Modified By] " & _
            '        "FROM dbo.ReceivingNoteMasterTable Recv INNER JOIN " & _
            '        "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
            '        " dbo.PurchaseOrderMasterTable ON Recv.PurchaseOrderID = dbo.PurchaseOrderMasterTable.PurchaseOrderId "
            '' TASK TFS1261 Added new column LCId which concerns for Import document.
            'Rafay :Modified qyery to add currency and foreing amount
            'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.ReceivingNo, Recv.ReceivingDate AS Date, dbo.PurchaseOrderMasterTable.PurchaseOrderNo, V.detail_title, Recv.ReceivingQty, " & _
            '      "Recv.ReceivingAmount, Recv.ReceivingNoteId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
            '      "Recv.PurchaseOrderID, Recv.DcNo, Recv.Arrival_Time as [In time], Recv.Departure_Time as [Out Time], Isnull(Recv.Total_Discount_Amount,0) as Total_Discount_Amount, IsNull(Recv.Post, 0) as Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, Recv.Vehicle_No, Recv.Driver_Name, Recv.DcDate,  Recv.Vendor_Invoice_No, Isnull(Recv.CostCenterId,0) as CostCenterId, Recv.IGPNo, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], isnull(Recv.CurrencyType,0) as CurrencyType, isnull(Recv.CurrencyRate,0) as CurrencyRate, ISNULL(Recv.Transportation_Vendor,0) as Transportation_Vendor, V.Contact_Email as Email,IsNull([No Of Attachment],0) as [No Of Attachment],Recv.Purchaseorderid as PO_ID,Recv.UpdateUserName as [Modified By], IsNull(Recv.LCId, 0) As LCId , tblDefCostCenter.Name as CostCenter " & _
            '      "FROM dbo.ReceivingNoteMasterTable Recv INNER JOIN " & _
            '      "vwCOADetail V ON Recv.VendorId = V.coa_detail_id Left Outer Join tblDefCostCenter on Recv.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN " & _
            '      " dbo.PurchaseOrderMasterTable ON Recv.PurchaseOrderID = dbo.PurchaseOrderMasterTable.PurchaseOrderId "
            str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.ReceivingNo, Recv.ReceivingDate AS Date, dbo.PurchaseOrderMasterTable.PurchaseOrderNo, V.detail_title,dbo.tblcurrency.currency_code As currency_code, " & _
                  "Recv.ReceivingAmount,Recv.AmountUS, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
                  "Recv.PurchaseOrderID, Recv.DcNo, Recv.Arrival_Time as [In time], Recv.Departure_Time as [Out Time], Isnull(Recv.Total_Discount_Amount,0) as Total_Discount_Amount, IsNull(Recv.Post, 0) as Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, Recv.Vehicle_No, Recv.Driver_Name, Recv.DcDate,  Recv.Vendor_Invoice_No, Isnull(Recv.CostCenterId,0) as CostCenterId, Recv.IGPNo, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], Recv.ReceivingQty,Recv.ReceivingNoteId, isnull(Recv.CurrencyType,0) as CurrencyType, isnull(Recv.CurrencyRate,0) as CurrencyRate, ISNULL(Recv.Transportation_Vendor,0) as Transportation_Vendor, ISNULL(Recv.Custom_Vendor,0) as Custom_Vendor, V.Contact_Email as Email,IsNull([No Of Attachment],0) as [No Of Attachment],Recv.Purchaseorderid as PO_ID,Recv.UpdateUserName as [Modified By], IsNull(Recv.LCId, 0) As LCId , tblDefCostCenter.Name as CostCenter " & _
                  "FROM dbo.ReceivingNoteMasterTable Recv INNER JOIN " & _
                  " dbo.tblcurrency ON Recv.CurrencyType = dbo.tblcurrency.currency_id LEFT OUTER JOIN " & _
                  "vwCOADetail V ON Recv.VendorId = V.coa_detail_id Left Outer Join tblDefCostCenter on Recv.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN " & _
                  " dbo.PurchaseOrderMasterTable ON Recv.PurchaseOrderID = dbo.PurchaseOrderMasterTable.PurchaseOrderId "
            If cmbSearchLocation.SelectedIndex > 0 Then
                str = str & " LEFT OUTER JOIN (Select DISTINCT ReceivingNoteId, LocationId From ReceivingNoteDetailTable) as Location On Location.ReceivingNoteId = Recv.ReceivingNoteId "
            End If

            str = str & " LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.ReceivingNo  LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = recv.ReceivingNoteId  " & _
                    " where Recv.ReceivingNo like 'GRN%' " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.ReceivingDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
            'Marked Against Task#2015060006  to display attachements
            If flgCompanyRights = True Then
                str += " AND Recv.LocationId=" & MyCompanyId
            End If
            If Me.dtpFrom.Checked = True Then
                str += " AND Recv.ReceivingDate >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
            End If
            If Me.dtpTo.Checked = True Then
                str += " AND Recv.ReceivingDate <= Convert(Datetime, N'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
            End If
            If Me.txtSearchDocNo.Text <> String.Empty Then
                str += " AND Recv.ReceivingNo LIKE '%" & Me.txtSearchDocNo.Text & "%'"
            End If
            If Not Me.cmbSearchLocation.SelectedIndex = -1 Then
                If Me.cmbSearchLocation.SelectedIndex <> 0 Then
                    str += " AND Location.LocationId=" & Me.cmbSearchLocation.SelectedValue
                End If
            End If
            If Me.txtFromAmount.Text <> String.Empty Then
                If Val(Me.txtFromAmount.Text) > 0 Then
                    str += " AND Recv.ReceivingAmount >= " & Val(Me.txtFromAmount.Text) & " "
                End If
            End If
            If Me.txtToAmount.Text <> String.Empty Then
                If Val(Me.txtToAmount.Text) > 0 Then
                    str += " AND Recv.ReceivingAmount <= " & Val(Me.txtToAmount.Text) & ""
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
                str += " AND PurchaseOrderMasterTable.PurchaseOrderNo LIKE  '%" & Me.txtPurchaseNo.Text & "%'"
            End If
            str += "  order by Recv.ReceivingNo desc"
        End If

        FillGridEx(grdSaved, str, True)
        grdSaved.RootTable.Columns(4).Visible = True 'rafay currency
        grdSaved.RootTable.Columns(5).Visible = True 'rafay receivingamount
        grdSaved.RootTable.Columns(6).Visible = True 'rafay Amount US
        grdSaved.RootTable.Columns(7).Visible = False
        grdSaved.RootTable.Columns(9).Visible = False
        grdSaved.RootTable.Columns(10).Visible = False
        'Rafay:the foreign currency is add on purchase history
        Dim grdSaved1 As DataTable = GetDataTable(str)
        grdSaved1.Columns("AmountUS").Expression = "IsNull(ReceivingAmount,0) / (IsNull(CurrencyRate,0))" 'Task:2374 Show Total Amount
        Me.grdSaved.DataSource = grdSaved1
        'Set rounded format
        Me.grdSaved.RootTable.Columns("AmountUS").FormatString = "N" & DecimalPointInValue
        'rafay
        'Task:2759 Set rounded amount format.
        Me.grdSaved.RootTable.Columns("ReceivingAmount").FormatString = "N" & DecimalPointInValue
        Me.grdSaved.RootTable.Columns("Total_Discount_Amount").FormatString = "N" & DecimalPointInValue
        'End Task:2759

        Me.grdSaved.RootTable.Columns("Total_Discount_Amount").Visible = False
        grdSaved.RootTable.Columns("CurrencyType").Visible = False
        grdSaved.RootTable.Columns("ReceivingQty").Visible = False
        grdSaved.RootTable.Columns("ReceivingNoteId").Visible = False
        grdSaved.RootTable.Columns("CurrencyRate").Visible = False
        grdSaved.RootTable.Columns("Post").Visible = False
        grdSaved.RootTable.Columns("Vehicle_No").Visible = False
        grdSaved.RootTable.Columns("Driver_Name").Visible = False
        grdSaved.RootTable.Columns("CostCenterId").Visible = False
        Me.grdSaved.RootTable.Columns("Transportation_Vendor").Visible = False
        Me.grdSaved.RootTable.Columns("Custom_Vendor").Visible = False
        Me.grdSaved.RootTable.Columns("Email").Visible = False
        Me.grdSaved.RootTable.Columns("LCId").Visible = False
        grdSaved.RootTable.Columns(0).Caption = "Doc No"
        grdSaved.RootTable.Columns(1).Caption = "Doc Date"
        grdSaved.RootTable.Columns(2).Caption = "PO No"
        grdSaved.RootTable.Columns(3).Caption = "Vendor Name"
        'Rafay
        grdSaved.RootTable.Columns(4).Caption = "Currency"
        grdSaved.RootTable.Columns(5).Caption = "Base Value"
        grdSaved.RootTable.Columns(6).Caption = "Original Value"
        'RAfay
        grdSaved.RootTable.Columns(9).Caption = "Cash Paid"
        grdSaved.RootTable.Columns(8).Caption = "Remarks"
        grdSaved.RootTable.Columns("DcNo").Caption = "Dc No"
        grdSaved.RootTable.Columns("DcDate").Caption = "Dc Date"
        Me.grdSaved.RootTable.Columns("Vendor_Invoice_No").Caption = "Invoice No"
        Me.grdSaved.RootTable.Columns("IGPNo").Caption = "IGP No"
        grdSaved.RootTable.Columns(0).Width = 100
        grdSaved.RootTable.Columns(1).Width = 100
        grdSaved.RootTable.Columns(2).Width = 100
        grdSaved.RootTable.Columns(3).Width = 200
        grdSaved.RootTable.Columns(8).Width = 200
        Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat
        Me.grdSaved.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
        Me.CtrlGrdBar2_Load(Nothing, Nothing)

    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Validate_AddToGrid() = False Then Exit Sub
        If Not FindExistsItem(Val(Me.cmbItem.Value), Val(Me.txtPackQty.Text), Val(Me.cmbCategory.SelectedValue), strComments) = True Then
            'dblRate = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            AddItemToGrid()
        End If


        'AddItemToGrid()
        'GetTotal()
        ClearDetailControls()
        cmbItem.Focus()
        'FillCombo("Item")
        'End If
    End Sub
    Private Sub RefreshControls()
        Try
            'Task 1592 Ayesha Rehman Removing the ErrorProvider on btnNew
            ErrorProvider1.Clear()
            IsEditMode = False
            'If Me.BtnSave.Text = "&Update" Then
            '    FillCombo("Vendor")
            'End If 'R933 Commented
            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''END TFS4544

            ''TASK TFS1378
            If Not getConfigValueByType("GRNStockImpact").ToString = "Error" Then
                GRNStockImpact = Convert.ToBoolean(getConfigValueByType("GRNStockImpact").ToString)
            End If
            ''END TASK TFS
            txtPONo.Text = ""
            dtpPODate.Value = Now
            Me.dtpDcDate.Value = Now
            Me.dtpPODate.Enabled = True
            Me.dtpDcDate.Checked = False
            txtRemarks.Text = ""
            txtPaid.Text = ""
            'txtAmount.Text = ""
            'txtTotal.Text = ""
            'txtTotalQty.Text = ""
            txtBalance.Text = ""
            txtPackQty.Text = 1
            'Me.txtTax.Text = ""
            txtVhNo.Text = ""
            txtDriverName.Text = ""
            'Me.txtPackRate.Text = ""
            Me.txtInvoiceNo.Text = String.Empty
            Me.BtnSave.Text = "&Save"
            Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("Pur", 6, "ReceivingNoteMasterTable", "ReceivingNo")
            'FillCombo("Batch") R933 Commented
            'FillCombo("SO") 'R933 Commented
            'FillCombo("Item") 'R933 Commented
            'FillCombo("CostCenter")
            cmbUnit.SelectedIndex = 0
            'rafay
            companyinitials = ""
            'rafay
            Me.cmbSalesMan.SelectedIndex = 0
            cmbVendor.Rows(0).Activate()
            cmbLC.Rows(0).Activate()
            Me.cmbTransportationVendor.Rows(0).Activate()
            Me.cmbCustom.Rows(0).Activate()
            FillCombo("Item")
            cmbItem.Rows(0).Activate()
            Me.cmbPo.Enabled = True
            Me.cmbVendor.Enabled = True
            ''grd.Rows.Clear()
            'DisplayDetail(-1)
            'DisplayPODetail(-1)
            'Me.cmbVendor.Focus()
            dblRate = 0D
            Me.dtpPODate.Focus()
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
            If Not getConfigValueByType("GRNApproval") = "Error" Then
                ApprovalProcessId = Val(getConfigValueByType("GRNApproval"))
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

            'grd.Rows.Clear()
            DisplayDetail(-1)
            DisplayPODetail(-1)
            If Me.cmbBatchNo.Value = Nothing Then
                Me.cmbBatchNo.Enabled = False
            Else
                Me.cmbBatchNo.Enabled = True
            End If
            'FillComboByEdit() R933 Commented
            If flgSelectProject = True Then
                If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedIndex = Me.cmbProject.SelectedIndex
            Else
                If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedIndex = 0
            End If
            Me.txtIGPNo.Text = String.Empty
            dblTaxPercent = 0D
            '-------------- Payment Voucher --------------
            Me.SplitContainer1.Panel2Collapsed = True
            FillPaymentMethod()
            'Me.cmbMethod.SelectedIndex = 0
            'Me.cmbMethod.Enabled = True
            'Me.txtChequeNo.Text = String.Empty
            'Me.dtpChequeDate.Value = Date.Today
            'Me.txtRecAmount.Text = String.Empty
            'Me.txtVoucherNo.Text = GetVoucherNo()
            VoucherDetail(String.Empty)
            'If Not Me.cmbCompany.SelectedIndex = -1 Then Me.cmbCompany.SelectedIndex = 0
            '----------------------------------------------
            Me.dtpFrom.Value = Date.Now.AddMonths(-1)
            Me.dtpTo.Value = Date.Now
            Me.dtpFrom.Checked = False
            Me.dtpTo.Checked = False
            Me.dtpArrivalTime.Value = Date.Now
            Me.dtpDepartureTime.Value = Date.Now
            Me.dtpDepartureTime.Checked = False
            Me.txtSearchDocNo.Text = String.Empty
            'Me.cmbSearchLocation.SelectedIndex = 0
            Me.txtFromAmount.Text = String.Empty
            Me.txtToAmount.Text = String.Empty
            Me.txtPurchaseNo.Text = String.Empty
            'Me.cmbSearchAccount.Rows(0).Activate()
            Me.txtSearchRemarks.Text = String.Empty
            Me.SplitContainer2.Panel1Collapsed = True
            Me.lblPrintStatus.Text = String.Empty

            If flgCurrenyonOpenLC = True Then
                Me.grpCurrency.Visible = True
                If Not Me.cmbCurrency.SelectedIndex = -1 Then Me.cmbCurrency.SelectedValue = BaseCurrencyId ''19-Dec-2013 R:945 , TaskId:2338        M Ijaz Javed           By default pack rupees
                Me.txtCurrencyRate.Text = 1     ''19-Dec-2013 R:945 , TaskId:2338        M Ijaz Javed           By default pack rupees
            Else
                Me.grpCurrency.Visible = False
            End If
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
            'If flgTransaportationChargesVoucher = False Then
            '    Me.GroupBox7.Visible = False
            'Else
            '    Me.GroupBox7.Visible = True
            'End If
            'Clear Attached file records
            arrFile = New List(Of String)
            Me.btnAttachment.Text = "Attachment (" & arrFile.Count & ")"
            'Altered Against Task#2015060001 Ali Ansri
            'Array.Clear(arrFile, 0, arrFile.Length)
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnEdit.Visible = False
            Me.BtnPrint.Visible = False
            Me.BtnDelete.Visible = False
            '''''''''''''''''''''''''''''
            Me.btnLoad.Visible = True



        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ClearDetailControls()
        'cmbCategory.SelectedIndex = 0
        cmbUnit.SelectedIndex = 0
        If cmbBatchNo.Rows.Count > 0 Then cmbBatchNo.Rows(0).Activate()
        txtQty.Text = ""
        'txtRate.Text = ""
        'txtTotal.Text = ""
        txtPackQty.Text = 1
        Me.txtGrossQuantity.Text = ""
        Me.txtColumn1.Text = String.Empty
        Me.txtColumn2.Text = String.Empty
        Me.txtColumn3.Text = String.Empty
        Me.txtTotalQuantity.Text = String.Empty

        'Me.txtTax.Text = ""
        'Me.txtPackRate.Text = ""
    End Sub

    Private Function Validate_AddToGrid() As Boolean
        If cmbItem.Value <= 0 Then
            msg_Error("Please select an item")
            cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/26/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/26/2022)
        'If Me.cmbBatchNo.Text = String.Empty Then
        '    msg_Error("Please enter batch no")
        '    Me.cmbBatchNo.Focus() : Validate_AddToGrid = False : Exit Function
        'End If
        'UserPriceAllowedRights = GetUserPriceAllowedRights(LoginUserId)
        'If UserPriceAllowedRights = True Then
        '    If Val(txtQty.Text) <= 0 Then
        '        msg_Error("Quantity is not greater than 0")
        '        txtQty.Focus() : Validate_AddToGrid = False : Exit Function
        '    End If

        If Val(txtGrossQuantity.Text) <= 0 Then
            msg_Error("Total Quantity is required more than 0")
            txtGrossQuantity.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        Validate_AddToGrid = True
    End Function
    'Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus
    '    If Val(Me.txtPackQty.Text) = 0 Then
    '        txtPackQty.Text = 1
    '        txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
    '    Else
    '        txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
    '    End If
    'End Sub

    'Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If Val(Me.txtPackQty.Text) = 0 Then
    '            txtPackQty.Text = 1
    '            txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
    '        Else
    '            txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub txtRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate.TextChanged
    '    Try
    '        Try
    '            If Me.txtPackQty.Text.Length > 0 Then
    '                If Me.txtRate.Text.Length > 0 Then
    '                    Me.txtPackRate.Text = Val(Me.txtPackQty.Text) * Val(Me.txtRate.Text)
    '                End If
    '            End If
    '        Catch ex As Exception
    '            ShowErrorMessage(ex.Message)
    '        End Try
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        ''get the qty in case of pack unit
        If Me.cmbUnit.Text = "Loose" Then
            'txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
            txtPackQty.Text = 1
            Me.txtPackQty.Enabled = False
            Me.txtPackQty.TabStop = False
            Me.txtGrossQuantity.Enabled = False
        Else
            ''Start TFS4161
            If IsPackQtyDisabled = True Then
                Me.txtPackQty.Enabled = False
                Me.txtPackQty.TabStop = False
                Me.txtGrossQuantity.Enabled = False
            Else
                Me.txtPackQty.Enabled = True
                Me.txtPackQty.TabStop = True
                Me.txtGrossQuantity.Enabled = True
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

            'objCommand.CommandText = "Select PackQty from ArticleDefTable where ArticleID = " & cmbItem.Value
            'txtPackQty.Text = objCommand.ExecuteScalar()
            If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
                Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
            End If
            'txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text))

        End If
        Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))

    End Sub

    Private Sub AddItemToGrid()
        Try
            'Task against Req No 828
            'By Imran Ali 18-9-2013

            'Dim objcommand As New OleDbCommand
            'objcommand.Connection = Con
            'objcommand.CommandType = CommandType.Text
            'objcommand.CommandText = "SELECT COUNT(BatchNo) AS BatchNo  FROM PurchaseBatchTable WHERE     BatchNo = N'" & Me.cmbBatchNo.Text.Trim & "'"
            'If Con.State = ConnectionState.Closed Then Con.Open()
            'If objcommand.ExecuteScalar = 0 Then
            '    If msg_Confirm("The batch No you entered is new. " & Environment.NewLine & "Do you want to add new batch No?") Then
            '        Dim frm As New frmDefBatch
            '        frm.Source = frmDefBatch.FormSource.FromPurchase
            '        frm.uitxtBatch.Text = Me.cmbBatchNo.Text.Trim
            '        frm.dtpStart.Value = DateTime.Now
            '        frm.dtpStart.Enabled = False
            '        frm.dtpEnd.Checked = True
            '        frm.uitxtBatch.Enabled = False
            '        frm.dtpEnd.Focus()

            '        If frm.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
            '    Else
            '        Exit Sub
            '    End If
            'End If

            'grd.Rows.Add(cmbCategory.Text, cmbItem.Text, Me.cmbBatchNo.Text, cmbUnit.Text, txtQty.Text, 0, txtQty.Text, txtRate.Text, Val(txtTotal.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue)
            'grd.Rows.Add(cmbCategory.Text, cmbItem.Text, Me.cmbItem.ActiveRow.Cells("Combination").Value, Me.cmbItem.ActiveRow.Cells("Size").Value, Me.cmbBatchNo.Text, cmbUnit.Text, txtQty.Text, 0, txtQty.Text, txtRate.Text, Val(txtTotal.Text), 0, cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue)
            'grd.Rows.Add(cmbCategory.Text, cmbItem.ActiveRow.Cells("Item").Text, Me.cmbItem.ActiveRow.Cells("Combination").Value, Me.cmbItem.ActiveRow.Cells("Size").Value, Me.cmbBatchNo.Text, cmbUnit.Text, txtQty.Text, 0, txtQty.Text, txtRate.Text, Val(txtTotal.Text), 0, cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue)

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
            Dim dtGrd As DataTable
            dtGrd = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            If msg_Confirm("Do you want to load Segregated Qty?") = False Then
                Dim drGrd As DataRow
                drGrd = dtGrd.NewRow
                'drGrd.Item(grdEnm.Category) = Me.cmbCategory.Text
                drGrd.Item(grdEnm.LocationId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
                drGrd.Item(grdEnm.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
                drGrd.Item(grdEnm.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
                drGrd.Item(grdEnm.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Text.ToString
                drGrd.Item(grdEnm.Size) = Me.cmbItem.ActiveRow.Cells("Size").Text.ToString
                drGrd.Item(grdEnm.Uom) = Me.cmbItem.ActiveRow.Cells("Unit").Text.ToString 'Task:2407 Added Field Of Uom
                drGrd.Item(grdEnm.BatchNo) = "xxxx" 'Me.cmbBatchNo.Text
                drGrd.Item(grdEnm.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
                drGrd.Item(grdEnm.PackQty) = Val(Me.txtPackQty.Text)
                drGrd.Item(grdEnm.ReceivedQty) = Val(Me.txtQty.Text) '' TASK-408
                drGrd.Item(grdEnm.RejectedQty) = Val(0)
                drGrd.Item(grdEnm.Qty) = Val(0) 'Val(Me.txtQty.Text)`````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````
                drGrd.Item(grdEnm.GrossQty) = Val(Me.txtGrossQuantity.Text) ''TASK-408
                drGrd.Item(grdEnm.Column1) = Val(Me.txtColumn1.Text)
                drGrd.Item(grdEnm.Column2) = Val(Me.txtColumn2.Text)
                drGrd.Item(grdEnm.Column3) = Val(Me.txtColumn3.Text)
                drGrd.Item(grdEnm.TotalQty) = Val(Me.txtTotalQuantity.Text) ''TASK-408
                drGrd.Item(grdEnm.VendorQty) = Val(Me.txtTotalQuantity.Text) ''TASK-408
                drGrd.Item(grdEnm.Price) = IIf(dblRate = 0, Val(txtRate.Text), dblRate) 'Val(Me.txtRate.Text)
                drGrd.Item(grdEnm.Total) = 0 'Val(Me.txtTotal.Text)
                drGrd.Item(grdEnm.Transportation_Charges) = 0
                drGrd.Item(grdEnm.Custom_Charges) = 0
                drGrd.Item(grdEnm.Discount_Price) = 0 'Set Value in Discount Price Column
                drGrd.Item(grdEnm.ArticleGroupId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
                drGrd.Item(grdEnm.ArticleDefId) = Me.cmbItem.ActiveRow.Cells(0).Value
                drGrd.Item(grdEnm.PackPrice) = 0 'Val(Me.txtPackRate.Text)
                drGrd.Item(grdEnm.CurrentPrice) = IIf(dblCurrentRate = 0, Val(txtRate.Text), dblCurrentRate) 'Val(Me.txtRate.Text) 'Val(Me.cmbItem.ActiveRow.Cells("Price").Text)
                drGrd.Item(grdEnm.RateDiscPercent) = dblRateDiscPercent
                drGrd.Item(grdEnm.PackPrice) = 0 'Val(Me.txtPackRate.Text)
                drGrd.Item(grdEnm.BatchId) = 0 'Me.cmbBatchNo.Value
                drGrd.Item(grdEnm.ExpiryDate) = Convert.ToDateTime(Date.Now.AddMonths(1))
                drGrd.Item(grdEnm.TaxPercent) = dblTaxPercent
                drGrd.Item(grdEnm.Comments) = strComments
                drGrd.Item(grdEnm.PO_ID) = Val(Me.cmbPo.SelectedValue)
                If Me.cmbPo.Tag IsNot Nothing Then
                    drGrd.Item(grdEnm.PurchaseOrderDetailId) = Val(Me.cmbPo.Tag)
                Else
                    drGrd.Item(grdEnm.PurchaseOrderDetailId) = 0
                End If
                drGrd.Item(grdEnm.Pack_Desc) = Me.cmbUnit.Text.ToString
                drGrd.Item(grdEnm.AccountId) = Val(Me.cmbItem.ActiveRow.Cells("AccountId").Value.ToString)
                drGrd.Item(grdEnm.X_Tray_Weights) = 0
                drGrd.Item(grdEnm.X_Gross_Weights) = 0
                'drGrd.Item(grdEnm.X_Net_Weights) = 0
                drGrd.Item(grdEnm.Y_Tray_Weights) = 0
                drGrd.Item(grdEnm.Y_Gross_Weights) = 0
                drGrd.Item(grdEnm.AdTax_Percent) = dblAdTaxPercent
                drGrd.Item(grdEnm.Deduction) = 0
                drGrd.Item("CurrencyId") = Me.cmbCurrency.SelectedValue
                If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    drGrd.Item("CurrencyAmount") = Val(0)
                Else
                    drGrd.Item("CurrencyAmount") = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text) * Val(Me.txtCurrencyRate.Text), TotalAmountRounding)
                End If
                drGrd.Item("CurrencyRate") = Val(Me.txtCurrencyRate.Text)
                Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
                If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                    drGrd.Item("BaseCurrencyId") = Val(ConfigCurrencyVal)
                    drGrd.Item("BaseCurrencyRate") = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
                End If
                ' drGrd.Item(grdEnm.Y_Net_Weights) = 0
                'dtGrd.Rows.InsertAt(drGrd, 0)
                'Task 3913 Saad Afzaal change InsertAt to Add function to maintain sequence of items which add in the grid
                dtGrd.Rows.Add(drGrd)
            Else
                Dim checkqty As Integer = txtQty.Text
                For strQty As Integer = 1 To checkqty
                    Dim drGrd As DataRow
                    drGrd = dtGrd.NewRow
                    'drGrd.Item(grdEnm.Category) = Me.cmbCategory.Text
                    drGrd.Item(grdEnm.LocationId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
                    drGrd.Item(grdEnm.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
                    drGrd.Item(grdEnm.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
                    drGrd.Item(grdEnm.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Text.ToString
                    drGrd.Item(grdEnm.Size) = Me.cmbItem.ActiveRow.Cells("Size").Text.ToString
                    drGrd.Item(grdEnm.Uom) = Me.cmbItem.ActiveRow.Cells("Unit").Text.ToString 'Task:2407 Added Field Of Uom
                    drGrd.Item(grdEnm.BatchNo) = "xxxx" 'Me.cmbBatchNo.Text
                    drGrd.Item(grdEnm.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
                    drGrd.Item(grdEnm.PackQty) = Val(Me.txtPackQty.Text)
                    drGrd.Item(grdEnm.ReceivedQty) = Val(1) '' TASK-408
                    drGrd.Item(grdEnm.RejectedQty) = Val(0)
                    drGrd.Item(grdEnm.Qty) = Val(0) 'Val(Me.txtQty.Text)`````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````
                    drGrd.Item(grdEnm.GrossQty) = Val(1) ''TASK-408
                    drGrd.Item(grdEnm.Column1) = Val(Me.txtColumn1.Text)
                    drGrd.Item(grdEnm.Column2) = Val(Me.txtColumn2.Text)
                    drGrd.Item(grdEnm.Column3) = Val(Me.txtColumn3.Text)
                    drGrd.Item(grdEnm.TotalQty) = Val(1) ''TASK-408
                    drGrd.Item(grdEnm.VendorQty) = Val(1) ''TASK-408
                    drGrd.Item(grdEnm.Price) = IIf(dblRate = 0, Val(txtRate.Text), dblRate) 'Val(Me.txtRate.Text)
                    drGrd.Item(grdEnm.Total) = 0 'Val(Me.txtTotal.Text)
                    drGrd.Item(grdEnm.Transportation_Charges) = 0
                    drGrd.Item(grdEnm.Custom_Charges) = 0
                    drGrd.Item(grdEnm.Discount_Price) = 0 'Set Value in Discount Price Column
                    drGrd.Item(grdEnm.ArticleGroupId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
                    drGrd.Item(grdEnm.ArticleDefId) = Me.cmbItem.ActiveRow.Cells(0).Value
                    drGrd.Item(grdEnm.PackPrice) = 0 'Val(Me.txtPackRate.Text)
                    drGrd.Item(grdEnm.CurrentPrice) = IIf(dblCurrentRate = 0, Val(txtRate.Text), dblCurrentRate) 'Val(Me.txtRate.Text) 'Val(Me.cmbItem.ActiveRow.Cells("Price").Text)
                    drGrd.Item(grdEnm.RateDiscPercent) = dblRateDiscPercent
                    drGrd.Item(grdEnm.PackPrice) = 0 'Val(Me.txtPackRate.Text)
                    drGrd.Item(grdEnm.BatchId) = 0 'Me.cmbBatchNo.Value
                    drGrd.Item(grdEnm.ExpiryDate) = Convert.ToDateTime(Date.Now.AddMonths(1))
                    drGrd.Item(grdEnm.TaxPercent) = dblTaxPercent
                    drGrd.Item(grdEnm.Comments) = strComments
                    drGrd.Item(grdEnm.PO_ID) = Val(Me.cmbPo.SelectedValue)
                    If Me.cmbPo.Tag IsNot Nothing Then
                        drGrd.Item(grdEnm.PurchaseOrderDetailId) = Val(Me.cmbPo.Tag)
                    Else
                        drGrd.Item(grdEnm.PurchaseOrderDetailId) = 0
                    End If
                    drGrd.Item(grdEnm.Pack_Desc) = Me.cmbUnit.Text.ToString
                    drGrd.Item(grdEnm.AccountId) = Val(Me.cmbItem.ActiveRow.Cells("AccountId").Value.ToString)
                    drGrd.Item(grdEnm.X_Tray_Weights) = 0
                    drGrd.Item(grdEnm.X_Gross_Weights) = 0
                    'drGrd.Item(grdEnm.X_Net_Weights) = 0
                    drGrd.Item(grdEnm.Y_Tray_Weights) = 0
                    drGrd.Item(grdEnm.Y_Gross_Weights) = 0
                    drGrd.Item(grdEnm.AdTax_Percent) = dblAdTaxPercent
                    drGrd.Item(grdEnm.Deduction) = 0
                    drGrd.Item("CurrencyId") = Me.cmbCurrency.SelectedValue
                    If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                        drGrd.Item("CurrencyAmount") = Val(0)
                    Else
                        drGrd.Item("CurrencyAmount") = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text) * Val(Me.txtCurrencyRate.Text), TotalAmountRounding)
                    End If
                    drGrd.Item("CurrencyRate") = Val(Me.txtCurrencyRate.Text)
                    Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
                    If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                        drGrd.Item("BaseCurrencyId") = Val(ConfigCurrencyVal)
                        drGrd.Item("BaseCurrencyRate") = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
                    End If
                    ' drGrd.Item(grdEnm.Y_Net_Weights) = 0
                    'dtGrd.Rows.InsertAt(drGrd, 0)
                    'Task 3913 Saad Afzaal change InsertAt to Add function to maintain sequence of items which add in the grid
                    dtGrd.Rows.Add(drGrd)
                    checkqty = checkqty - 1
                Next
            End If


            'Dim drGrd As DataRow
            'drGrd = dtGrd.NewRow
            ''drGrd.Item(grdEnm.Category) = Me.cmbCategory.Text
            'drGrd.Item(grdEnm.LocationId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
            'drGrd.Item(grdEnm.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
            'drGrd.Item(grdEnm.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
            'drGrd.Item(grdEnm.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Text.ToString
            'drGrd.Item(grdEnm.Size) = Me.cmbItem.ActiveRow.Cells("Size").Text.ToString
            'drGrd.Item(grdEnm.Uom) = Me.cmbItem.ActiveRow.Cells("Unit").Text.ToString 'Task:2407 Added Field Of Uom
            'drGrd.Item(grdEnm.BatchNo) = "xxxx" 'Me.cmbBatchNo.Text
            'drGrd.Item(grdEnm.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            'drGrd.Item(grdEnm.PackQty) = Val(Me.txtPackQty.Text)
            'drGrd.Item(grdEnm.ReceivedQty) = Val(Me.txtQty.Text) '' TASK-408
            'drGrd.Item(grdEnm.RejectedQty) = Val(0)
            'drGrd.Item(grdEnm.Qty) = Val(0) 'Val(Me.txtQty.Text)`````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````
            'drGrd.Item(grdEnm.GrossQty) = Val(Me.txtGrossQuantity.Text) ''TASK-408
            'drGrd.Item(grdEnm.Column1) = Val(Me.txtColumn1.Text)
            'drGrd.Item(grdEnm.Column2) = Val(Me.txtColumn2.Text)
            'drGrd.Item(grdEnm.Column3) = Val(Me.txtColumn3.Text)
            'drGrd.Item(grdEnm.TotalQty) = Val(Me.txtTotalQuantity.Text) ''TASK-408
            'drGrd.Item(grdEnm.VendorQty) = Val(Me.txtTotalQuantity.Text) ''TASK-408
            'drGrd.Item(grdEnm.Price) = IIf(dblRate = 0, Val(txtRate.Text), dblRate) 'Val(Me.txtRate.Text)
            'drGrd.Item(grdEnm.Total) = 0 'Val(Me.txtTotal.Text)
            'drGrd.Item(grdEnm.Transportation_Charges) = 0
            'drGrd.Item(grdEnm.Discount_Price) = 0 'Set Value in Discount Price Column
            'drGrd.Item(grdEnm.ArticleGroupId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
            'drGrd.Item(grdEnm.ArticleDefId) = Me.cmbItem.ActiveRow.Cells(0).Value
            'drGrd.Item(grdEnm.PackPrice) = 0 'Val(Me.txtPackRate.Text)
            'drGrd.Item(grdEnm.CurrentPrice) = IIf(dblCurrentRate = 0, Val(txtRate.Text), dblCurrentRate) 'Val(Me.txtRate.Text) 'Val(Me.cmbItem.ActiveRow.Cells("Price").Text)
            'drGrd.Item(grdEnm.RateDiscPercent) = dblRateDiscPercent
            'drGrd.Item(grdEnm.PackPrice) = 0 'Val(Me.txtPackRate.Text)
            'drGrd.Item(grdEnm.BatchId) = 0 'Me.cmbBatchNo.Value
            'drGrd.Item(grdEnm.ExpiryDate) = Convert.ToDateTime(Date.Now.AddMonths(1))
            'drGrd.Item(grdEnm.TaxPercent) = dblTaxPercent
            'drGrd.Item(grdEnm.Comments) = strComments
            'drGrd.Item(grdEnm.PO_ID) = Val(Me.cmbPo.SelectedValue)
            'If Me.cmbPo.Tag IsNot Nothing Then
            '    drGrd.Item(grdEnm.PurchaseOrderDetailId) = Val(Me.cmbPo.Tag)
            'Else
            '    drGrd.Item(grdEnm.PurchaseOrderDetailId) = 0
            'End If
            'drGrd.Item(grdEnm.Pack_Desc) = Me.cmbUnit.Text.ToString
            'drGrd.Item(grdEnm.AccountId) = Val(Me.cmbItem.ActiveRow.Cells("AccountId").Value.ToString)
            'drGrd.Item(grdEnm.X_Tray_Weights) = 0
            'drGrd.Item(grdEnm.X_Gross_Weights) = 0
            ''drGrd.Item(grdEnm.X_Net_Weights) = 0
            'drGrd.Item(grdEnm.Y_Tray_Weights) = 0
            'drGrd.Item(grdEnm.Y_Gross_Weights) = 0
            'drGrd.Item(grdEnm.AdTax_Percent) = dblAdTaxPercent
            'drGrd.Item(grdEnm.Deduction) = 0
            'drGrd.Item("CurrencyId") = Me.cmbCurrency.SelectedValue
            'If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
            '    drGrd.Item("CurrencyAmount") = Val(0)
            'Else
            '    drGrd.Item("CurrencyAmount") = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text) * Val(Me.txtCurrencyRate.Text), TotalAmountRounding)
            'End If
            'drGrd.Item("CurrencyRate") = Val(Me.txtCurrencyRate.Text)
            'Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
            'If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
            '    drGrd.Item("BaseCurrencyId") = Val(ConfigCurrencyVal)
            '    drGrd.Item("BaseCurrencyRate") = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
            'End If
            '' drGrd.Item(grdEnm.Y_Net_Weights) = 0
            ''dtGrd.Rows.InsertAt(drGrd, 0)
            ''Task 3913 Saad Afzaal change InsertAt to Add function to maintain sequence of items which add in the grid
            'dtGrd.Rows.Add(drGrd)
            dtGrd.AcceptChanges()
            GetDiscountedPrice()
            'txtDiscount_LostFocus(Nothing, Nothing)
            dblAdTaxPercent = 0D
            dblTaxPercent = 0D
            dblRate = 0D
            'Task 3913 Saad Afzaal move scroll bar at the end when item added into the grid 
            grd.MoveLast()
        Catch ex As Exception
            Throw ex
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
    ''' <summary>
    ''' A new function is made to add items in grid From Item Search Screen
    ''' </summary>
    ''' <remarks>TFS3762 : Ayesha Rehman : 12-07-2018</remarks>
    Public Sub AddItemToGridFromItemSearch()
        Try
            FillCombo("Item")
            cmbItem.Value = frmItemSearch.ArticleId
            txtQty.Text = frmItemSearch.Qty
            Me.txtPackQty.Text = frmItemSearch.PackQty
            'Ali Faisal : TFS4271 : Total qty add from item search popup
            Me.txtTotalQuantity.Text = frmItemSearch.TotalQty
            txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString ''TFS3762
            cmbUnit.SelectedIndex = IIf(frmItemSearch.PackName <> "Loose", 1, 0) ''TFS3762

            Dim dtGrd As DataTable
            dtGrd = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtGrd.NewRow

            drGrd.Item(grdEnm.LocationId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
            drGrd.Item(grdEnm.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
            drGrd.Item(grdEnm.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
            drGrd.Item(grdEnm.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Text.ToString
            drGrd.Item(grdEnm.Size) = Me.cmbItem.ActiveRow.Cells("Size").Text.ToString
            drGrd.Item(grdEnm.Uom) = Me.cmbItem.ActiveRow.Cells("Unit").Text.ToString 'Task:2407 Added Field Of Uom
            drGrd.Item(grdEnm.BatchNo) = "xxxx" 'Me.cmbBatchNo.Text
            drGrd.Item(grdEnm.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            drGrd.Item(grdEnm.PackQty) = Val(Me.txtPackQty.Text)
            drGrd.Item(grdEnm.ReceivedQty) = Val(Me.txtQty.Text) '' TASK-408
            drGrd.Item(grdEnm.RejectedQty) = Val(0)
            drGrd.Item(grdEnm.Qty) = Val(0) 'Val(Me.txtQty.Text)`````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````
            drGrd.Item(grdEnm.GrossQty) = Val(Me.txtGrossQuantity.Text) ''TASK-408
            drGrd.Item(grdEnm.Column1) = Val(Me.txtColumn1.Text)
            drGrd.Item(grdEnm.Column2) = Val(Me.txtColumn2.Text)
            drGrd.Item(grdEnm.Column3) = Val(Me.txtColumn3.Text)
            drGrd.Item(grdEnm.TotalQty) = Val(Me.txtTotalQuantity.Text) ''TASK-408
            drGrd.Item(grdEnm.VendorQty) = Val(Me.txtTotalQuantity.Text) ''TASK-408
            drGrd.Item(grdEnm.Price) = IIf(dblRate = 0, Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString), dblRate) 'Val(Me.txtRate.Text)
            drGrd.Item(grdEnm.Total) = 0 'Val(Me.txtTotal.Text)
            drGrd.Item(grdEnm.Transportation_Charges) = 0
            drGrd.Item(grdEnm.Custom_Charges) = 0
            drGrd.Item(grdEnm.Discount_Price) = 0 'Set Value in Discount Price Column
            drGrd.Item(grdEnm.ArticleGroupId) = IIf(Me.cmbCategory.SelectedValue = Nothing, 0, Me.cmbCategory.SelectedValue)
            drGrd.Item(grdEnm.ArticleDefId) = Me.cmbItem.ActiveRow.Cells(0).Value
            drGrd.Item(grdEnm.PackPrice) = 0 'Val(Me.txtPackRate.Text)
            drGrd.Item(grdEnm.CurrentPrice) = IIf(dblCurrentRate = 0, Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString), dblCurrentRate) 'Val(Me.txtRate.Text) 'Val(Me.cmbItem.ActiveRow.Cells("Price").Text)
            drGrd.Item(grdEnm.RateDiscPercent) = dblRateDiscPercent
            drGrd.Item(grdEnm.PackPrice) = 0 'Val(Me.txtPackRate.Text)
            drGrd.Item(grdEnm.BatchId) = 0 'Me.cmbBatchNo.Value
            drGrd.Item(grdEnm.ExpiryDate) = Convert.ToDateTime(Date.Now.AddMonths(1))
            drGrd.Item(grdEnm.TaxPercent) = dblTaxPercent
            drGrd.Item(grdEnm.Comments) = strComments
            drGrd.Item(grdEnm.PO_ID) = Val(Me.cmbPo.SelectedValue)
            If Me.cmbPo.Tag IsNot Nothing Then
                drGrd.Item(grdEnm.PurchaseOrderDetailId) = Val(Me.cmbPo.Tag)
            Else
                drGrd.Item(grdEnm.PurchaseOrderDetailId) = 0
            End If
            drGrd.Item(grdEnm.Pack_Desc) = Me.cmbUnit.Text.ToString
            drGrd.Item(grdEnm.AccountId) = Val(Me.cmbItem.ActiveRow.Cells("AccountId").Value.ToString)
            drGrd.Item(grdEnm.X_Tray_Weights) = 0
            drGrd.Item(grdEnm.X_Gross_Weights) = 0
            'drGrd.Item(grdEnm.X_Net_Weights) = 0
            drGrd.Item(grdEnm.Y_Tray_Weights) = 0
            drGrd.Item(grdEnm.Y_Gross_Weights) = 0
            drGrd.Item(grdEnm.AdTax_Percent) = dblAdTaxPercent
            drGrd.Item(grdEnm.Deduction) = 0

            ' drGrd.Item(grdEnm.Y_Net_Weights) = 0
            'dtGrd.Rows.InsertAt(drGrd, 0)
            'Task 3913 Saad Afzaal change InsertAt to Add function to maintain sequence of items which add in the grid
            dtGrd.Rows.Add(drGrd)
            dtGrd.AcceptChanges()
            GetDiscountedPrice()
            'txtDiscount_LostFocus(Nothing, Nothing)
            dblAdTaxPercent = 0D
            dblTaxPercent = 0D
            dblRate = 0D
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
            If IsFormLoaded = True Then
                If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                    FillCombo("Item")
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
    '    Dim dblPurchaseTax As Double
    '    For i = 0 To grd.Rows.Count - 1
    '        dblTotalAmount = dblTotalAmount + Val(grd.Rows(i).Cells("total").Value)
    '        dblTotalQty = dblTotalQty + Val(grd.Rows(i).Cells("Qty").Value)
    '        dblPurchaseTax = dblPurchaseTax + ((Val(grd.Rows(i).Cells("Total").Value) * Val(grd.Rows(i).Cells("TaxPercent").Value)) / 100)
    '    Next
    '    txtAmount.Text = dblTotalAmount
    '    txtTotalQty.Text = dblTotalQty
    '    txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)
    '    Me.txtPercent.Text = dblPurchaseTax
    '    Me.lblRecordCount.Text = "Total Records: " & Me.grd.RowCount
    'End Sub
    ''ReqId-915 Added field of accountid in Item query
    Private Sub FillCombo(ByVal strCondition As String)
        Dim str As String
        Dim strVendorWiseItemSecurity As String = getConfigValueByType("PurchaseItemsVendorsWise").ToString
        If strCondition = "Item" Or strCondition = "ItemFilter" Then
            str = "SELECT DISTINCT ArticleDefView.ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit,ArticleDefView.ArticleBrandName As Grade, ISNULL(PurchasePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], Location.Ranks as Rake, Isnull(ArticleDefView.SubSubId,0) as AccountId,ArticleDefView.SortOrder  FROM ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId where Active=1"
            ''Start TFS3762 : Not Adding filters in Case Configuration "AddItemForMall" = true
            If flgAddItemForMall = False Then
                If flgCompanyRights = True Then
                    str += " AND ArticleDefView.CompanyId=" & MyCompanyId
                End If
                If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                    If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                        str += " AND ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                    Else
                        str += str
                    End If
                End If
                If Me.rbtVendor.Checked = True Then
                    str = str + " AND ArticleDefView.MasterId In (Select ArticleDefId From ArticleDefVendors WHERE VendorId=" & Me.cmbVendor.Value & ") "
                End If
            End If
            ''End TFS3762
            'str += " ORDER BY ArticleDefView.SortOrder Asc"
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
            Me.cmbItem.Rows(0).Activate()
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                If ItemFilterByName = True Then
                    Me.RbtByName.Checked = True
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                Else
                    If Me.RbtByCode.Checked = True Then
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                    Else
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                    End If
                End If
            End If

        ElseIf strCondition = "Category" Then
            'Task#16092015 Load Locations user wise
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Code from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order"
            'Else
            '    str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            'End If
            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock, IsNull(comments,'') AS comments from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock, IsNull(comments,'') AS comments from tblDefLocation Where Active = 1 order by sort_order"

            FillDropDown(cmbCategory, str, False)

        ElseIf strCondition = "SearchLocation" Then
            str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            FillDropDown(Me.cmbSearchLocation, str, True)

            'str = "Select ArticleGroupID, ArticleGroupName from ArticleGroupDefTable where Active=1"
            'FillDropDown(cmbCategory, str)

        ElseIf strCondition = "ItemFilter1" Then
            str = "SELECT     ArticleId as Id, ArticleDescription Item, ArticleCode Code, ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit,ArticleDefView.ArticleBrandName As Grade, PurchasePrice as Price , ArticleDefView.SizeRangeID as [Size ID], Isnull(ArticleDefView.SubSubId,0) as AccountId FROM         ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue
            FillUltraDropDown(cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
        ElseIf strCondition = "Vendor" Then
            'str = "SELECT     tblCustomer.AccountId AS ID, tblCustomer.CustomerName AS Name, tblListTerritory.TerritoryName AS Territory, tblListCity.CityName AS City,  " & _
            '        "tblListState.StateName AS State, tblCustomer.AccountId AS AcId " & _
            '        "FROM         tblListTerritory INNER JOIN " & _
            '        "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
            '        "tblListState ON tblListCity.StateId = tblListState.StateId INNER JOIN " & _
            '        "tblCustomer ON tblListTerritory.TerritoryId = tblCustomer.Territory"
            If getConfigValueByType("Show Customer On Purchase") = "True" Then
                'Before against task:2373
                'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                '                    "dbo.tblListTerritory.TerritoryName as Territory,dbo.vwCOADetail.account_type as Type, tblVendor.Email, tblVendor.Phone " & _
                '                    "FROM         dbo.tblVendor INNER JOIN " & _
                '                    "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                '                    "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                '                    "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                '                    "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                '                    "WHERE     (dbo.vwCOADetail.account_type in ('Vendor','Customer')) "
                'TAsk:2373 Added Column Sub Sub Tile
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                 "dbo.tblListTerritory.TerritoryName as Territory,dbo.vwCOADetail.account_type as Type, vwCOADetail.Contact_Email as Email, vwCOADetail.Contact_Phone as Phone, vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                                 "FROM         dbo.tblVendor INNER JOIN " & _
                                 "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                 "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                 "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                 "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                 "WHERE     (dbo.vwCOADetail.account_type in ('Vendor','Customer')) "
                'End Task:2373
                If flgCompanyRights = True Then
                    str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                End If
            Else
                'Befreo against Task:2373 
                'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                '                                 "dbo.tblListTerritory.TerritoryName as Territory,dbo.vwCOADetail.account_type as Type,tblVendor.Email, tblVendor.Phone " & _
                '                                 "FROM dbo.tblVendor INNER JOIN " & _
                '                                 "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                '                                 "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                '                                 "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                '                                 "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                '                                 "WHERE     (dbo.vwCOADetail.account_type ='Vendor') "
                'Task:2373 Added Column Sub Sub Title
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                             "dbo.tblListTerritory.TerritoryName as Territory,dbo.vwCOADetail.account_type as Type,vwCOADetail.Contact_Email as Email, vwCOADetail.Contact_Phone as Phone, vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                                             "FROM dbo.tblVendor INNER JOIN " & _
                                             "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                             "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                             "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                             "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                             "WHERE     (dbo.vwCOADetail.account_type ='Vendor') "
                'End Task:2373
                If flgCompanyRights = True Then
                    str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                End If
            End If
            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            'If LoginGroup = "Administrator" Then
            If GetMappedUserId() > 0 And getGroupAccountsConfigforPurchase(Me.Name) And LoginGroup <> "Administrator" Then ''TFS4689
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                     "dbo.tblListTerritory.TerritoryName as Territory,dbo.vwCOADetail.account_type as Type,vwCOADetail.Contact_Email as Email, vwCOADetail.Contact_Phone as Phone, vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                                     "FROM dbo.tblVendor INNER JOIN " & _
                                     "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                     "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                     "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                     "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                     "WHERE      (dbo.vwCOADetail.detail_title Is Not NULL ) "
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
                str += " AND vwCOADetail.Active in (0,1,NULL)"
            End If
            str += " order by tblVendor.Sortorder, vwCOADetail.detail_title "
            FillUltraDropDown(cmbVendor, str)
            If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" ' Task:2373 Change Caption
            End If
        ElseIf strCondition = "TransportationVendor" Then
            'If getConfigValueByType("Show Customer On Purchase") = "True" Then
            '    str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
            '                        "dbo.tblListTerritory.TerritoryName as Territory,dbo.vwCOADetail.account_type as Type, tblVendor.Email, tblVendor.Phone " & _
            '                        "FROM         dbo.tblVendor INNER JOIN " & _
            '                        "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '                        "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '                        "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                        "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                        "WHERE     (dbo.vwCOADetail.account_type in ('Vendor','Customer')) "
            '    If flgCompanyRights = True Then
            '        str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            '    End If
            'Else
            '    str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
            '                                     "dbo.tblListTerritory.TerritoryName as Territory,dbo.vwCOADetail.account_type as Type,tblVendor.Email, tblVendor.Phone " & _
            '                                     "FROM dbo.tblVendor INNER JOIN " & _
            '                                     "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '                                     "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '                                     "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                                     "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                                     "WHERE     (dbo.vwCOADetail.account_type ='Vendor') "
            '    If flgCompanyRights = True Then
            '        str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            '    End If
            'End If
            'If IsEditMode = False Then
            '    str += " AND vwCOADetail.Active=1"
            'Else
            '    str += " AND vwCOADetail.Active in (0,1,NULL)"
            'End If
            ''If Me.cmbVendor.IsItemInList = True Then
            ''    str += " AND vwCOADetail.coa_detail_id <> " & Me.cmbVendor.Value & ""
            ''End If
            'str += " order by tblVendor.Sortorder, vwCOADetail.detail_title "
            'FillUltraDropDown(Me.cmbTransportationVendor, str)
            'If Me.cmbTransportationVendor.DisplayLayout.Bands.Count > 0 Then
            '    Me.cmbTransportationVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
            '    Me.cmbTransportationVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
            'End If

            'chaning against request no 829 by imran ali set query for transporter
            str = "Select ISNULL([AccountId],0) as AccountId, [TransporterName],[TransporterId] From tblDefTransporter where Active=1 and Custom=0"
            FillUltraDropDown(Me.cmbTransportationVendor, str)
            If Me.cmbTransportationVendor.DisplayLayout.Bands.Count > 0 Then
                Me.cmbTransportationVendor.DisplayLayout.Bands(0).Columns("TransporterId").Hidden = True
                Me.cmbTransportationVendor.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbTransportationVendor.DisplayLayout.Bands(0).Columns("TransporterName").Width = 150
            End If
            Me.cmbTransportationVendor.Rows(0).Activate()

        ElseIf strCondition = "CustomVendor" Then
            str = "Select ISNULL([AccountId],0) as AccountId, [TransporterName],[TransporterId] From tblDefTransporter where Active=1 and Custom=1"
            FillUltraDropDown(Me.cmbCustom, str)
            If Me.cmbCustom.DisplayLayout.Bands.Count > 0 Then
                Me.cmbCustom.DisplayLayout.Bands(0).Columns("TransporterId").Hidden = True
                Me.cmbCustom.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbCustom.DisplayLayout.Bands(0).Columns("TransporterName").Width = 150
            End If
            Me.cmbCustom.Rows(0).Activate()
        ElseIf strCondition = "SearchVendor" Then
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                           "dbo.tblListTerritory.TerritoryName as Territory, tblVendor.Email, tblVendor.Phone, tblVendor.Mobile, vwCOADetail.Sub_Sub_Title as [Ac Head] " & _
                                           "FROM  dbo.tblVendor INNER JOIN " & _
                                           "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                           "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                           "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                           "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                           "WHERE  (dbo.vwCOADetail.account_type = 'Vendor') AND dbo.vwCOADetail.detail_title Is Not NULL "
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
            'str = "Select PurchaseOrderID, PurchaseOrderNo + ' ~ ' + Convert(Varchar(12), PurchaseOrderDate, 113) as PurchaseOrderNo, DcNo,Remarks from PurchaseOrderMasterTable where PurchaseorderId not in(select PurchaseOrderId from ReceivingNoteMasterTable)"
            'TASK16 Filter Posted Purchase Order
            'str = "Select PurchaseOrderID, PurchaseOrderNo + ' ~ ' + Convert(Varchar(12), PurchaseOrderDate, 113) as PurchaseOrderNo, DcNo,Remarks from PurchaseOrderMasterTable where IsNull(Post,0)=1 AND Status='Open' AND PurchaseorderId not in(select PurchaseOrderId from ReceivingNoteMasterTable) Order By PurchaseOrderID DESC "
            str = "Select PurchaseOrderID, PurchaseOrderNo + ' ~ ' + Convert(Varchar(12), PurchaseOrderDate, 113) as PurchaseOrderNo, DcNo,Remarks, IsNull(CostCenterId, 0) As CostCenterId, IsNull(LCId, 0) As LCId, CurrencyType, CurrencyRate from PurchaseOrderMasterTable where VendorID=" & Me.cmbVendor.Value & " AND  IsNull(Post,0)=1 AND Status='Open' Order By PurchaseOrderID DESC "
            Me.cmbPo.DataSource = Nothing
            FillDropDown(cmbPo, str)
            'ElseIf strCondition = "Batch" Then
            '    str = "Select BatchID, BatchNo from PurchaseBatchTable order by SortOrder "
            '    FillDropDown(Me.cmbBatch, str)
        ElseIf strCondition = "SOSpecific" Then
            'str = "Select PurchaseOrderID, PurchaseOrderNo, DcNo,Remarks from PurchaseOrderMasterTable where PurchaseorderId not in(select PurchaseOrderId from ReceivingNoteMasterTable) and vendorid=" & Me.cmbVendor.Value & ""
            'TASK16 Filter Posted Purchase Order
            str = "Select PurchaseOrderID, PurchaseOrderNo, DcNo,Remarks, IsNull(CostCenterId, 0) As CostCenterId, IsNull(LCId, 0) As LCId, CurrencyType, CurrencyRate from PurchaseOrderMasterTable where IsNull(Post,0)=1 AND PurchaseorderId not in(select PurchaseOrderId from ReceivingNoteMasterTable) and vendorid=" & Me.cmbVendor.Value & ""
            FillDropDown(cmbPo, str)

        ElseIf strCondition = "SOComplete" Then
            'str = "Select PurchaseOrderID, PurchaseOrderNo, DcNo,Remarks  from PurchaseOrderMasterTable "
            'TASK16 Filter Posted Purchase Order
            str = "Select PurchaseOrderID, PurchaseOrderNo, DcNo,Remarks, IsNull(CostCenterId, 0) As CostCenterId, IsNull(LCId, 0) As LCId, CurrencyType, CurrencyRate from PurchaseOrderMasterTable WHERE IsNull(Post,0)=1 "
            FillDropDown(cmbPo, str)
        ElseIf strCondition = "SM" Then
            str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee"
            FillDropDown(Me.cmbSalesMan, str)
        ElseIf strCondition = "grdLocation" Then
            'Task#16092015 Load Locations user wise
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
            Me.grd.RootTable.Columns(grdEnm.LocationId).ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
        ElseIf strCondition = "GrdOrigin" Then
            str = "select CountryName, CountryName From tblListCountry Where Active = 1"
            Dim dtCountry As DataTable = GetDataTable(str)
            dtCountry.AcceptChanges()
            Me.grd.RootTable.Columns("Origin").ValueList.PopulateValueList(dtCountry.DefaultView, "CountryName", "CountryName")
        ElseIf strCondition = "CostCenter" Then
            str = "If  exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " and ISNULL(CostCentre_Id, 0) > 0) " _
                    & "Select CostCenterID, Name from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") order by SortOrder " _
                    & "Else " _
                    & "Select CostCenterID, Name from tblDefCostCenter where Active = 1 order by SortOrder"
            FillDropDown(Me.cmbProject, str)

        ElseIf strCondition = "Currency" Then
            str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
            FillDropDown(Me.cmbCurrency, str)
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
            'ElseIf strCondition = "Company" Then
            '    str = "Select CompanyId, CompanyName From CompanyDefTable"
            '    FillDropDown(Me.cmbCompany, str, False)
        ElseIf strCondition = "LCPO" Then
            str = "Select PurchaseOrderID, PurchaseOrderNo + ' ~ ' + Convert(Varchar(12), PurchaseOrderDate, 113) as PurchaseOrderNo, DcNo,Remarks, IsNull(CostCenterId, 0) As CostCenterId, CurrencyType, CurrencyRate from PurchaseOrderMasterTable where LCId=" & Me.cmbLC.Value & " AND  IsNull(Post,0)=1 AND Status='Open' Order By PurchaseOrderID DESC "
            Me.cmbPo.DataSource = Nothing
            FillDropDown(cmbPo, str)
        ElseIf strCondition = "ArticlePack" Then
            Me.cmbUnit.ValueMember = "ArticlePackId"
            Me.cmbUnit.DisplayMember = "PackName"
            Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
            '' TASK TFS1261 Added new combo LC which concerns for Import document.
        ElseIf strCondition = "LC" Then
            'If IsEditMode = True Then
            '    str = "Select LCdoc_Id, LCdoc_No, LCdoc_Date, Bank, LCdoc_Type,CostCenter From tblLetterOfCredit WHERE VendorId=" & Me.cmbVendor.Value & ""
            'Else
            '    str = "Select LCdoc_Id, LCdoc_No, LCdoc_Date, Bank, LCdoc_Type,CostCenter From tblLetterOfCredit WHERE Active=1 " & IIf(Me.cmbVendor.ActiveRow Is Nothing, "", " AND VendorId=" & Me.cmbVendor.Value & "") & ""
            'End If
            str = "Select LCdoc_Id, LCdoc_No, LCdoc_Date, Bank, LCdoc_Type, CostCenter From tblLetterOfCredit WHERE Active=1 "
            FillUltraDropDown(Me.cmbLC, str)
            If Me.cmbLC.DisplayLayout.Bands.Count > 0 Then
                Me.cmbLC.DisplayLayout.Bands(0).Columns("LCdoc_Id").Hidden = True
                Me.cmbLC.DisplayLayout.Bands(0).Columns("CostCenter").Hidden = True
                Me.cmbLC.DisplayLayout.Bands(0).Columns("LCdoc_No").Header.Caption = "Doc No"
                Me.cmbLC.DisplayLayout.Bands(0).Columns("LCdoc_Date").Header.Caption = "Do Date"
                Me.cmbLC.DisplayLayout.Bands(0).Columns("LCdoc_Type").Header.Caption = "Type"
            End If
        End If
    End Sub
    Private Sub txtPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaid.TextChanged
        txtBalance.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(txtPaid.Text)
    End Sub

    Private Function Save() As Boolean
        If Me.chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        If Not getConfigValueByType("AvgRate").ToString = "Error" Then
            flgAvgRate = getConfigValueByType("AvgRate")
        End If
        Me.grd.UpdateData()
        'Dim PurchaseTaxAccountId As Integer = Val(getConfigValueByType("PurchaseTaxDebitAccountId").ToString) 'GetConfigValue("PurchaseTaxDebitAccountId").ToString
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("Pur", 6, "ReceivingNoteMasterTable", "ReceivingNo")
        'Dim PaymentVoucherFlg As String = getConfigValueByType("PaymentVoucherOnPurchase").ToString 'GetConfigValue("PaymentVoucherOnPurchase").ToString
        'Dim VoucherNo As String = GetVoucherNo()
        'setVoucherNo = Me.txtPONo.Text
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        gobjLocationId = MyCompanyId
        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        'Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo.Text)
        'Dim AccountId As Integer = Val(getConfigValueByType("PurchaseDebitAccount").ToString) 'GetConfigValue("PurchaseDebitAccount")
        'Dim flgAvgRate As Boolean = Convert.ToBoolean(getConfigValueByType("AvgRate").ToString) 'GetConfigValue("PurchaseDebitAccount")
        ''Dim strvoucherNo As String = GetNextDocNo("PV", 6, "tblVoucher", "voucher_no")

        'If AccountId <= 0 Then
        '    ShowErrorMessage("Purchase account is not map.")
        '    Me.dtpPODate.Focus()
        '    Return False
        'End If
        'If Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) <> 0 Then
        '    If PurchaseTaxAccountId <= 0 Then
        '        ShowErrorMessage("Purchase tax account is not map.")
        '        Me.dtpPODate.Focus()
        '        Return False
        '    End If
        'End If
        'Dim GLAccountArticleDepartment As Boolean
        'If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
        '    GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
        'Else
        '    GLAccountArticleDepartment = False
        'End If
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'objCommand.CommandText = "Insert into ReceivingNoteMasterTable (locationId,ReceivingNo,ReceivingDate,VendorId,PurchaseOrderId, ReceivingQty,ReceivingAmount, CashPaid, Remarks,UserName, DcNo, Post, Vehicle_No, Driver_Name, dcDate, Vendor_Invoice_No) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(txtTotalQty.Text) & "," & Val(txtAmount.Text) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text & "',N'" & LoginUserName & "', N'" & Me.txtDcNo.Text & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtVhNo.Text & "', N'" & Me.txtDriverName.Text & "', N'" & Me.dtpDcDate.Value & "', N'" & Me.txtInvoiceNo.Text.Replace("'","''") & "')"
            'objCommand.CommandText = "Insert into ReceivingNoteMasterTable (locationId,ReceivingNo,ReceivingDate,VendorId,PurchaseOrderId, ReceivingQty,ReceivingAmount, CashPaid, Remarks,UserName, DcNo, Post, Vehicle_No, Driver_Name, dcDate, Vendor_Invoice_No, CostCenterId, IGPNo, CurrencyType, CurrencyRate) values( " _
            '                       & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text & "',N'" & LoginUserName & "', N'" & Me.txtDcNo.Text & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtVhNo.Text & "', N'" & Me.txtDriverName.Text & "', " & IIf(Me.dtpDcDate.Checked = True, "N'" & Me.dtpDcDate.Value & "'", "NULL") & ", N'" & Me.txtInvoiceNo.Text.Replace("'","''") & "', " & Me.cmbProject.SelectedValue & ", N'" & Me.txtIGPNo.Text & "', " & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ") Select @@Identity"


            'objCommand.CommandText = "Insert into ReceivingNoteMasterTable (locationId,ReceivingNo,ReceivingDate,VendorId,PurchaseOrderId, ReceivingQty,ReceivingAmount, CashPaid, Remarks,UserName, DcNo, Post, Vehicle_No, Driver_Name, dcDate, Vendor_Invoice_No, CostCenterId, IGPNo, CurrencyType, CurrencyRate, Transportation_Vendor) values( " _
            '                       & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text & "',N'" & LoginUserName & "', N'" & Me.txtDcNo.Text & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtVhNo.Text & "', N'" & Me.txtDriverName.Text & "', " & IIf(Me.dtpDcDate.Checked = True, "N'" & Me.dtpDcDate.Value & "'", "NULL") & ", N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ", N'" & Me.txtIGPNo.Text & "', " & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", " & IIf(Me.cmbTransportationVendor.ActiveRow Is Nothing, 0, Me.cmbTransportationVendor.Value) & ") Select @@Identity"

            'Chaning against request no 828 by imran ali 18-09-2013 add column of total discount amount
            objCommand.CommandText = ""
            'objCommand.CommandText = "Insert into ReceivingNoteMasterTable (locationId,ReceivingNo,ReceivingDate,VendorId,PurchaseOrderId, ReceivingQty,ReceivingAmount, CashPaid, Remarks,UserName, DcNo, Post, Vehicle_No, Driver_Name, dcDate, Vendor_Invoice_No, CostCenterId, IGPNo, CurrencyType, CurrencyRate, Transportation_Vendor, Total_Discount_Amount) values( " _
            '                       & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & Me.txtDcNo.Text.Replace("'", "''") & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtVhNo.Text.Replace("'", "''") & "', N'" & Me.txtDriverName.Text.Replace("'", "''") & "', " & IIf(Me.dtpDcDate.Checked = True, "N'" & Me.dtpDcDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ", N'" & Me.txtIGPNo.Text.Replace("'", "''") & "', 0,0, " & IIf(Me.cmbTransportationVendor.ActiveRow Is Nothing, 0, Me.cmbTransportationVendor.Value) & ", 0) Select @@Identity"


            'objCommand.ExecuteNonQuery()
            '' TASK TFS1261 Added new column LCId which concerns for Import document.
            '' TFS2576 : Ayesha Rehman : GRN Status problem ,Added 'Status' a new column in ReceivingNoteMasterTable
            objCommand.CommandText = "Insert into ReceivingNoteMasterTable (locationId,ReceivingNo,ReceivingDate,VendorId,PurchaseOrderId, ReceivingQty,ReceivingAmount, CashPaid, Remarks,UserName, DcNo, Post, Vehicle_No, Driver_Name, dcDate, Vendor_Invoice_No, CostCenterId, IGPNo, CurrencyType, CurrencyRate, Transportation_Vendor, Total_Discount_Amount,Arrival_Time,Departure_Time, LCId , Status, Custom_Vendor) values( " _
                                & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & Me.txtDcNo.Text.Replace("'", "''") & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.txtVhNo.Text.Replace("'", "''") & "', N'" & Me.txtDriverName.Text.Replace("'", "''") & "', " & IIf(Me.dtpDcDate.Checked = True, "N'" & Me.dtpDcDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', " & IIf(Me.cmbProject.SelectedIndex = -1, 0, Me.cmbProject.SelectedValue) & ", N'" & Me.txtIGPNo.Text.Replace("'", "''") & "', " & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", " & IIf(Me.cmbTransportationVendor.ActiveRow Is Nothing, 0, Me.cmbTransportationVendor.Value) & ", 0,Convert(DateTime,'" & Me.dtpArrivalTime.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & IIf(Me.dtpDepartureTime.Checked = True, "Convert(Datetime,'" & Me.dtpDepartureTime.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)", "NULL") & ", " & IIf(Me.cmbLC.ActiveRow Is Nothing, 0, Me.cmbLC.Value) & ", N'" & EnumStatus.Open.ToString & "', " & IIf(Me.cmbCustom.ActiveRow Is Nothing, 0, Me.cmbCustom.Value) & ") Select @@Identity"
            getVoucher_Id = objCommand.ExecuteScalar 'objCommand.ExecuteNonQuery()
            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                SaveDocument(getVoucher_Id, Me.Name, trans)
            End If
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '                           & " cheque_no, cheque_date,post,Source,voucher_code)" _
            '                           & " VALUES(" & gobjLocationId & ", 1,  6 , N'" & Me.txtPONo.Text & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                           & " NULL , NULL, " & IIf(Me.chkPost.Checked = True, 1, 0) & " ,'frmPurchase',N'" & Me.txtPONo.Text & "')" _
            '                           & " SELECT @@IDENTITY"

            'lngVoucherMasterId = objCommand.ExecuteScalar


            '***********************
            'Deleting Detail
            '***********************
            'objCommand.CommandText = ""
            'objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId


            'objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""
            Dim strSQL As String = String.Empty
            Dim BatchID As Integer = 0
            StockList = New List(Of StockDetail)
            For i = 0 To grd.RowCount - 1


                '********************************
                ' Update Purchase Price By Imran
                '********************************
                'Dim CrrStock As Double = 0D
                'Dim StockDetail.Rate As Double = 0D
                'If GLAccountArticleDepartment = True Then
                '    AccountId = Val(grd.GetRows(i).Cells("PurchaseAccountId").Value.ToString)
                'End If
                'If flgAvgRate = True Then
                '    Try

                '        objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))) as Amount  " _
                '                                & " FROM dbo.ArticleDefTable INNER JOIN " _
                '                                & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & grd.GetRows(i).Cells("ItemId").Value & " " _
                '                                & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                '        Dim dtCrrStock As New DataTable
                '        Dim daCrrStock As New OleDbDataAdapter(objCommand)
                '        daCrrStock.Fill(dtCrrStock)
                '        If dtCrrStock IsNot Nothing Then
                '            If dtCrrStock.Rows.Count > 0 Then
                '                If Val(grd.GetRows(i).Cells("rate").Value.ToString) <> 0 Then
                '                    'CrrStock = dtCrrStock.Rows(0).Item(2) + IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value)))
                '                    'StockDetail.Rate = ((dtCrrStock.Rows(0).Item(3)) + IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) * (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString))) / CrrStock
                '                    StockDetail.Rate = Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)
                '                Else
                '                    StockDetail.Rate = Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)
                '                End If
                '            Else
                '                StockDetail.Rate = Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)
                '            End If
                '        End If
                '    Catch ex As Exception

                '    End Try
                'End If

                'StockDetail = New StockDetail
                'StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                'StockDetail.LocationId = grd.GetRows(i).Cells("LocationID").Value
                'StockDetail.ArticleDefId = grd.GetRows(i).Cells("ItemId").Value
                'StockDetail.InQty = IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value)))
                'StockDetail.OutQty = 0
                'StockDetail.Rate = IIf(StockDetail.Rate = 0, Val(grd.GetRows(i).Cells("Rate").Value.ToString), StockDetail.Rate)
                'StockDetail.InAmount = IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * IIf(StockDetail.Rate = 0, Val(grd.GetRows(i).Cells("Rate").Value.ToString), StockDetail.Rate)), ((Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value)) * IIf(StockDetail.Rate = 0, Val(grd.GetRows(i).Cells("Rate").Value.ToString), StockDetail.Rate)))
                'StockDetail.OutAmount = 0
                'StockDetail.Remarks = String.Empty
                'StockList.Add(StockDetail)

                'If Me.cmbPo.SelectedIndex > 0 Then

                'strSQL = "select BatchID from PurchaseBatchTable where BatchNo = N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & "'"
                'Dim dt As DataTable = GetDataTable(strSQL, trans)

                'If dt.Rows.Count = 0 Then
                '    If msg_Confirm("The batch No you entered is new. " & Environment.NewLine & "Do you want to add new batch no...?") Then

                '        objCommand.CommandText = "insert into PurchaseBatchTable(BatchNo,StartDate, EndDate, Comments,sortorder) values(N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Trim.Replace("'", "''") & "', GetDate(), NULL , '',0" & ") Select @@Identity"
                '        BatchID = objCommand.ExecuteScalar()

                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationId, ReceivedQty, RejectedQty) values( " _
                '            & " ident_current('ReceivingNoteMasterTable')," & Val(grd.Rows(i).Cells("ItemId").Value) & ",N'" & (grd.Rows(i).Cells("Unit").Value) & "'," & Val(grd.Rows(i).Cells("Qty").Value) & ", " _
                '            & " " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", Val(grd.Rows(i).Cells("qty").Value), (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value))) & ", " & Val(grd.Rows(i).Cells("rate").Value) & ", " & Val(grd.Rows(i).Cells("PackQty").Value) & " , " & Val(grd.Rows(i).Cells("CurrentPrice").Value) & ",N'" & grd.Rows(i).Cells("BatchNo").Value & " '," & BatchID & ",N'" & grd.Rows(i).Cells("LocationId").Value & " ', " & grd.Rows(i).Cells("ReceivedQty").Value & ", " & grd.Rows(i).Cells("RejectedQty").Value & " ) "

                '**********************************Update Bellow Purchase Tax Debit Account  08-02-2012 by Imran Ali *************************
                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationId, ReceivedQty, RejectedQty, TaxPercent, Comments, Transportation_Charges, PackPrice) values( " _
                '                                        & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '                                        & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & BatchID & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ") "

                'Chaning against request no 828 by imran ali 18-09-2013 add column Discount Price
                ''Before against request no . 934
                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges, PackPrice, Discount_Price, Pack_Desc) values( " _
                '                                       & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '                                       & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & BatchID & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", " & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                ''ReqId-934 Resolve Comma Error
                'objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges, PackPrice, Discount_Price, Pack_Desc) values( " _
                '                                       & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '                                       & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & "'," & BatchID & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", " & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "


                objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges, PackPrice, Discount_Price, Pack_Desc,X_Tray_Weights,X_Gross_Weights,X_Net_Weights,Y_Gross_Weights,Y_Tray_Weights,Y_Net_Weights,PO_ID) values( " _
                '                                       & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '                                       & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & "'," & BatchID & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", " & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("X_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PO_ID").Value.ToString) & ") "

                'TASK-TFS-51 Added Fields AdTax_Percent, AdTax_Amount
                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges, PackPrice, Discount_Price, Pack_Desc,X_Tray_Weights,X_Gross_Weights,X_Net_Weights,Y_Gross_Weights,Y_Tray_Weights,Y_Net_Weights,PO_ID,AdTax_Percent, AdTax_Amount) values( " _
                '                                     & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '                                     & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & "'," & BatchID & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", " & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("X_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PO_ID").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ") "
                'END TASK-TFS-51
                ''TASK TFS1378
                Dim strData As String = "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0)+" & (Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)) & "  as BalanceQty, SUM((IsNull(InAmount,0))-(IsNull(OutAmount,0))) + " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) + Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) * (Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * (Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString))) & " BalanceAmount From StockDetailTable INNER JOIN StockMasterTable ON StockDetailTable.StockTransId = StockMasterTable.StockTransId WHERE ArticleDefID=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & " AND IsNull(Rate,0) <> 0 AND StockMasterTable.DocDate < '" & Me.dtpPODate.Value & "' Group By ArticleDefId "

                Dim dblCostPrice As Double = 0D
                Dim dtLastestPriceData As New DataTable
                dtLastestPriceData = GetDataTable(strData, trans)
                dtLastestPriceData.AcceptChanges()

                If dtLastestPriceData.Rows.Count > 0 Then
                    'If Val(dtLastestPriceData.Rows(0).Item(1).ToString) > 0 Then
                    If flgAvgRate = True Then
                        dblCostPrice = (Val(dtLastestPriceData.Rows(0).Item(2).ToString) / Val(dtLastestPriceData.Rows(0).Item(1).ToString))
                    Else
                        dblCostPrice = (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) + Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) 'Val(grd.GetRows(i).Cells("Rate").Value.ToString)
                    End If
                    'If dblCostPrice = 0 Then
                    '    dblCostPrice = (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) 'Val(grd.GetRows(i).Cells("Rate").Value.ToString)
                    'End If
                    'Else
                    '    dblCostPrice = (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) 'Val(grd.GetRows(i).Cells("Rate").Value.ToString)
                    'End If
                Else
                    dblCostPrice = (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) + Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) 'Val(grd.GetRows(i).Cells("Rate").Value.ToString)
                End If
                Dim strserviceitem As String = "Select ServiceItem from ArticleDefView where ArticleId = " & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ""
                Dim dt2serviceitem As DataTable = GetDataTable(strserviceitem, trans)
                dt2serviceitem.AcceptChanges()
                Dim ServiceItem1 As Boolean = Val(dt2serviceitem.Rows(0).Item("ServiceItem").ToString)
                If ServiceItem1 = False Then
                    StockDetail = New StockDetail
                    StockDetail.StockTransId = 0
                    StockDetail.LocationId = grd.GetRows(i).Cells("LocationID").Value
                    StockDetail.ArticleDefId = IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString))
                    StockDetail.InQty = Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)
                    StockDetail.OutQty = 0
                    'Change Val(txtCurrencyRate.Text) BY murtaza (10272022)
                    StockDetail.Rate = (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) + Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) * Val(txtCurrencyRate.Text)
                    StockDetail.CostPrice = dblCostPrice '* Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString)
                    StockDetail.InAmount = Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * StockDetail.Rate
                    StockDetail.OutAmount = 0
                    StockDetail.Remarks = grd.GetRows(i).Cells("Comments").Value.ToString
                    StockDetail.PackQty = Val(grd.GetRows(i).Cells("PackQty").Value.ToString)
                    StockDetail.In_PackQty = Val(grd.GetRows(i).Cells("Qty").Value.ToString)
                    StockDetail.Out_PackQty = 0
                    StockDetail.BatchNo = grd.GetRows(i).Cells("BatchNo").Value.ToString
                    StockDetail.ExpiryDate = CType(grd.GetRows(i).Cells("ExpiryDate").Value, Date).ToString("yyyy-M-d h:mm:ss tt")
                    StockDetail.Origin = grd.GetRows(i).Cells("Origin").Value.ToString
                    StockList.Add(StockDetail)
                    '' END TASK TFS1378
                End If
                objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges, PackPrice, Discount_Price, Pack_Desc,X_Tray_Weights,X_Gross_Weights,X_Net_Weights,Y_Gross_Weights,Y_Tray_Weights,Y_Net_Weights,PO_ID,AdTax_Percent, AdTax_Amount,PurchaseOrderDetailId,Gross_Quantity,Vendor_Total_Quantity,Column1,Column2,Column3, OtherDeduction, IsStockImpacted, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, Origin, Custom_Charges, AlternativeItem, AlternativeItemId) values( " _
                                                   & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                                                   & " " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & "'," & BatchID & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & "," & IIf(grd.GetRows(i).Cells("ExpiryDate").Value.ToString = "", "NULL", "Convert(DateTime,N'" & CDate(IIf(Me.grd.GetRows(i).Cells("ExpiryDate").Value.ToString = "", Date.Now, Me.grd.GetRows(i).Cells("ExpiryDate").Value)).ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("X_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PO_ID").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PurchaseOrderDetailId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Gross_Qty").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Vendor_Qty").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Column1").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Column2").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Column3").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Deduction").Value.ToString) & ", " & IIf(GRNStockImpact = True, 1, 0) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ",N'" & grd.GetRows(i).Cells("Origin").Value.ToString.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) & ",N'" & (grd.GetRows(i).Cells("AlternativeItem").Value) & "'," & Val(grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) & ") " ''TASK-408 Added TotalQty value here 
                '& " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & "'," & BatchID & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", " & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("X_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PO_ID").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PurchaseOrderDetailId").Value.ToString) & ") "



                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges, PackPrice, Discount_Price, Pack_Desc,X_Tray_Weights,X_Gross_Weights,X_Net_Weights,Y_Gross_Weights,Y_Tray_Weights,Y_Net_Weights,PO_ID,AdTax_Percent, AdTax_Amount,PurchaseOrderDetailId) values( " _
                '                                   & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '                                   & " " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & "'," & BatchID & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", " & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("X_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PO_ID").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PurchaseOrderDetailId").Value.ToString) & ") " ''TASK-408 Added TotalQty value here 
                ''& " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & "'," & BatchID & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ", " & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("X_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PO_ID").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PurchaseOrderDetailId").Value.ToString) & ") "



                objCommand.ExecuteNonQuery()
                '        Else
                '            strSQL = "select BatchID from PurchaseBatchTable where BatchNo = N'" & grd.GetRows(i).Cells("BatchNo").Value & "'"
                '            dt = GetDataTable(strSQL, trans)
                '            Dim Batch_Id As Integer = 0I
                '            If dt IsNot Nothing Then
                '                If dt.Rows.Count > 0 Then
                '                    Batch_Id = dt.Rows(0).Item(0)
                '                End If
                '            End If

                '            'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty) values( " _
                '            '          & " ident_current('ReceivingNoteMasterTable')," & Val(grd.Rows(i).Cells("ItemId").Value) & ",N'" & (grd.Rows(i).Cells("Unit").Value) & "'," & Val(grd.Rows(i).Cells("Qty").Value) & ", " _
                '            '          & " " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", Val(grd.Rows(i).Cells("qty").Value), (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value))) & ", " & Val(grd.Rows(i).Cells("rate").Value) & ", " & Val(grd.Rows(i).Cells("PackQty").Value) & " , " & Val(grd.Rows(i).Cells("CurrentPrice").Value) & ",N'" & grd.Rows(i).Cells("BatchNo").Value & " '," & dt.Rows(0).Item(0).ToString & ",N'" & grd.Rows(i).Cells("LocationId").Value & " ', " & grd.Rows(i).Cells("ReceivedQty").Value & ", " & grd.Rows(i).Cells("RejectedQty").Value & " ) "
                '            '**********************************Update Bellow Purchase Tax Debit Account  08-02-2012 by Imran Ali *************************
                '            'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges,PackPrice) values( " _
                '            '          & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '            '          & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & Batch_Id & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ",N'" & CDate(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value).ToString("yyyy-M-d h:mm:ss tt") & "', N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ") "

                '            'changing against request no 828 by imran ali 18-09-2013 add column of Discount_Price
                '            'Before against request no. 934 
                '            'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges,PackPrice, Discount_Price, Pack_Desc) values( " _
                '            '          & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '            '          & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & Batch_Id & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & "," & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                '            'ReqId-934 Resolve Comma Error
                '            objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges,PackPrice, Discount_Price, Pack_Desc) values( " _
                '                     & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '                     & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & " '," & Batch_Id & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & "," & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "

                '        End If
                '    Else
                '        strSQL = "select BatchID from PurchaseBatchTable where BatchNo = N'" & grd.GetRows(i).Cells("BatchNo").Value & "'"
                '        dt = GetDataTable(strSQL, trans)
                '        Dim Batch_Id As Integer = 0I
                '        If dt IsNot Nothing Then
                '            If dt.Rows.Count > 0 Then
                '                Batch_Id = dt.Rows(0).Item(0)
                '            End If
                '        End If

                '        'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty) values( " _
                '        '              & " ident_current('ReceivingNoteMasterTable')," & Val(grd.Rows(i).Cells("ItemId").Value) & ",N'" & (grd.Rows(i).Cells("Unit").Value) & "'," & Val(grd.Rows(i).Cells("Qty").Value) & ", " _
                '        '              & " " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", Val(grd.Rows(i).Cells("qty").Value), (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value))) & ", " & Val(grd.Rows(i).Cells("rate").Value) & ", " & Val(grd.Rows(i).Cells("PackQty").Value) & " , " & Val(grd.Rows(i).Cells("CurrentPrice").Value) & ",N'" & grd.Rows(i).Cells("BatchNo").Value & " '," & dt.Rows(0).Item(0).ToString & ",N'" & grd.Rows(i).Cells("LocationId").Value & " ', " & grd.Rows(i).Cells("ReceivedQty").Value & ", " & grd.Rows(i).Cells("RejectedQty").Value & " ) "

                '        'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges,PackPrice) values( " _
                '        '              & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '        '              & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & Batch_Id & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ,N'" & CDate(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value).ToString("yyyy-M-d h:mm:ss tt") & "', N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ") "

                '        'changing against request no 828 by imran ali 18-9-2013 add column of Discount_Price
                '        'Before against request no. 934
                '        'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges,PackPrice, Discount_Price, Pack_Desc) values( " _
                '        '              & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '        '              & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & Batch_Id & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " , " & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                '        'ReqId-934 Resolve Comma Error
                '        objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges,PackPrice, Discount_Price, Pack_Desc) values( " _
                '                      & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '                      & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & " '," & Batch_Id & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " , " & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                '    End If

                'Else
                '    strSQL = "select BatchID from PurchaseBatchTable where BatchNo = N'" & grd.GetRows(i).Cells("BatchNo").Value & "'"
                '    dt = GetDataTable(strSQL, trans)
                '    Dim Batch_Id As Integer = 0I
                '    If dt IsNot Nothing Then
                '        If dt.Rows.Count > 0 Then
                '            Batch_Id = dt.Rows(0).Item(0)
                '        End If
                '    End If
                '    'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationId, ReceivedQty, RejectedQty) values( " _
                '    '                   & " ident_current('ReceivingNoteMasterTable')," & Val(grd.Rows(i).Cells("ItemId").Value) & ",N'" & (grd.Rows(i).Cells("Unit").Value) & "'," & Val(grd.Rows(i).Cells("Qty").Value) & ", " _
                '    '                   & " " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", Val(grd.Rows(i).Cells("qty").Value), (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value))) & ", " & Val(grd.Rows(i).Cells("rate").Value) & ", " & Val(grd.Rows(i).Cells("PackQty").Value) & " , " & Val(grd.Rows(i).Cells("CurrentPrice").Value) & ",N'" & grd.Rows(i).Cells("BatchNo").Value & " '," & dt.Rows(0).Item(0).ToString & ",N'" & grd.Rows(i).Cells("LocationId").Value & " ', " & grd.Rows(i).Cells("ReceivedQty").Value & ", " & grd.Rows(i).Cells("RejectedQty").Value & " ) "

                '    'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges, PackPrice) values( " _
                '    '                   & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '    '                   & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & Batch_Id & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ,N'" & CDate(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value).ToString("yyyy-M-d h:mm:ss tt") & "',N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ") "

                '    'changing against request no 828 by imran ali 18-9-2013 add column of discount_Price
                '    'Before against request no. 934
                '    'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges, PackPrice, Discount_Price, Pack_Desc) values( " _
                '    '                   & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '    '                   & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & Batch_Id & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ," & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ",N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                '    'ReqId-934 Resolve Comma Error
                '    objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID,LocationId, ReceivedQty, RejectedQty, TaxPercent, ExpiryDate, Comments, Transportation_Charges, PackPrice, Discount_Price, Pack_Desc) values( " _
                '                       & " ident_current('ReceivingNoteMasterTable')," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '                       & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & " '," & Batch_Id & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ," & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ",N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "


                'End If


                'objCommand.ExecuteNonQuery()
                'Val(grd.Rows(i).Cells(5).Value)
                'update date Purchase Order Detail table
                'Marked against Task#201507018 Ali Ansari
                'objCommand.CommandText = ""
                'objCommand.CommandText = "UPDATE    PurchaseOrderDetailTable" _
                '                        & " SET              DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & "" _
                '                        & " WHERE     (PurchaseOrderId = " & IIf(FlgPO = False, IIf(Me.cmbPo.SelectedIndex = -1, 0, cmbPo.SelectedValue), Val(grd.GetRows(i).Cells("PO_ID").Value.ToString)) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells("ItemId").Value) & ")"

                'objCommand.ExecuteNonQuery()

                'Altered against Task#201507018 Ali Ansari for updating multiple PO if multi flag is on


                If ServiceItem1 = False Then
                    'Altered against Task#201507018 Ali Ansari for updating multiple PO if multi flag is on
                    If Val(grd.GetRows(i).Cells("rate").Value.ToString) > 0 Then
                        If flgAvgRate = True Then

                            objCommand.CommandText = ""
                            objCommand.CommandText = "UPDATE ArticleDefTableMaster Set PurchasePrice=" & (StockDetail.Rate - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) + Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", Cost_Price=" & StockDetail.CostPrice & " WHERE ArticleId in (Select MasterId From ArticleDefTable WHERE ArticleId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ")"
                            objCommand.ExecuteNonQuery()


                            objCommand.CommandText = ""
                            objCommand.CommandText = "UPDATE ArticleDefTable Set PurchasePrice=" & (StockDetail.Rate - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) + Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", Cost_Price=" & StockDetail.CostPrice & " WHERE ArticleId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ""
                            objCommand.ExecuteNonQuery()

                            objCommand.CommandText = ""
                            objCommand.CommandText = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, N'" & Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") & "', 102))  AND a.ArticleDefId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ""
                            Dim dtRate As New DataTable
                            Dim daRate As New OleDbDataAdapter(objCommand)
                            daRate.Fill(dtRate)

                            objCommand.CommandText = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & Val(grd.GetRows(i).Cells("ArticleDefId").Value) & " "
                            Dim dtSalesItem As New DataTable
                            Dim daSalesItem As New OleDbDataAdapter(objCommand)
                            daSalesItem.Fill(dtSalesItem)

                            If Not dtSalesItem Is Nothing Then
                                If Not dtRate Is Nothing Then
                                    If dtRate.Rows.Count > 0 Then
                                        objCommand.CommandText = "Update IncrementReductionTable Set PurchaseNewPrice=" & (StockDetail.Rate - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) + Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, (StockDetail.Rate - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString))) & "  WHERE ArticleDefId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, N'" & Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) "
                                        objCommand.ExecuteNonQuery()
                                    Else
                                        objCommand.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice) " _
                                        & " Values(N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ",  " & Val(0) & ", " & Val(0) & ", " & (StockDetail.Rate - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) + Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, (StockDetail.Rate - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) + Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString))) & ")"
                                        objCommand.ExecuteNonQuery()
                                    End If
                                End If
                            End If
                        Else
                            'Apply Current Rate 
                            objCommand.CommandText = "UPDATE ArticleDefTable Set PurchasePrice=" & Val(grd.GetRows(i).Cells("Rate").Value.ToString) & ", Cost_Price=" & StockDetail.CostPrice & " WHERE ArticleId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ""
                            objCommand.ExecuteNonQuery()

                            objCommand.CommandText = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, N'" & Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") & "', 102))  AND a.ArticleDefId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ""
                            Dim dtRate As New DataTable
                            Dim daRate As New OleDbDataAdapter(objCommand)
                            daRate.Fill(dtRate)

                            objCommand.CommandText = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & " "
                            Dim dtSalesItem As New DataTable
                            Dim daSalesItem As New OleDbDataAdapter(objCommand)
                            daSalesItem.Fill(dtSalesItem)

                            If Not dtSalesItem Is Nothing Then
                                If Not dtRate Is Nothing Then
                                    If dtRate.Rows.Count > 0 Then
                                        objCommand.CommandText = "Update IncrementReductionTable Set PurchaseNewPrice=" & Val(grd.GetRows(i).Cells("Rate").Value.ToString) & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(grd.GetRows(i).Cells("Rate").Value.ToString)) & "  WHERE ArticleDefId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, N'" & Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) "
                                        objCommand.ExecuteNonQuery()
                                    Else
                                        objCommand.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice) " _
                                        & " Values(N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ",  " & Val(0) & ", " & Val(0) & ", " & Val(grd.GetRows(i).Cells("Rate").Value.ToString) & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(grd.GetRows(i).Cells("Rate").Value.ToString)) & ")"
                                        objCommand.ExecuteNonQuery()
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                '***********************
                'Inserting Debit Amount
                '***********************
                'objCommand.CommandText =0 ""
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & Val(grd.Rows(i).Cells("qty").Value) * Val(grd.Rows(i).Cells("rate").Value) & ", 0, N'" & grd.Rows(i).Cells("item").Value & "(" & Val(grd.Rows(i).Cells("Qty").Value) & ")')"


                'objCommand.CommandText = ""
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, sp_refrence, CostCenterId, direction) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", "" & Val(grd.GetRows(i).Cells("qty").Value.ToString) * (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & "", "" & (Val(grd.GetRows(i).Cells("PackQty").Value) * Val(grd.GetRows(i).Cells("Qty").Value.ToString)) * Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & "") & ",0, N'" & grd.GetRows(i).Cells("item").Value & " " & Me.grd.GetRows(i).Cells("Size").Value & "  (" & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & " X " & " " & Me.grd.GetRows(i).Cells("rate").Value - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ") " & " " & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', N'" & Me.txtInvoiceNo.Text.Replace("'", "''").Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ", " & grd.GetRows(i).Cells("ItemId").Value & " )"
                'objCommand.ExecuteNonQuery()

                ''***********************
                ''Inserting Credit Amounts
                ''***********************
                ''objCommand.CommandText = ""
                ''objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                ''                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(grd.Rows(i).Cells("qty").Value) * Val(grd.Rows(i).Cells("rate").Value) & ", N'" & grd.Rows(i).Cells("item").Value & "(" & Val(grd.Rows(i).Cells("Qty").Value) & ")')"

                'objCommand.CommandText = ""
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, sp_refrence, CostCenterId, direction) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", 0, " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", "" & Val(grd.GetRows(i).Cells("qty").Value.ToString) * (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & "", "" & (Val(grd.GetRows(i).Cells("PackQty").Value) * Val(grd.GetRows(i).Cells("Qty").Value.ToString)) * (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & "") & ", N'" & grd.GetRows(i).Cells("item").Value & " " & Me.grd.GetRows(i).Cells("Size").Value & "  (" & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & " X " & " " & Me.grd.GetRows(i).Cells("rate").Value - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ") " & " " & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', N'" & Me.txtInvoiceNo.Text.Replace("'", "''").Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & "," & grd.GetRows(i).Cells("ItemId").Value & ")"
                'objCommand.ExecuteNonQuery()


                'If flgTransaportationChargesVoucher = True Then
                '    If Not Me.cmbTransportationVendor.ActiveRow Is Nothing Then
                '        If Me.cmbTransportationVendor.Value > 0 Then
                '            If Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) <> 0 Then
                '                '************************************
                '                'Transportation Charges Voucher Debit
                '                '************************************
                '                objCommand.CommandText = ""
                '                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, sp_refrence, CostCenterId, direction) " _
                '                                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", "" & Val(grd.GetRows(i).Cells("qty").Value.ToString) * Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & "", "" & (Val(grd.GetRows(i).Cells("PackQty").Value) * Val(grd.GetRows(i).Cells("Qty").Value.ToString)) * Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & "") & ", 0,N'" & grd.GetRows(i).Cells("item").Value & " " & Me.grd.GetRows(i).Cells("Size").Value & "  (" & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & " X " & " " & Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ") " & " " & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ", " & grd.GetRows(i).Cells("ItemId").Value & ")"

                '                objCommand.ExecuteNonQuery()

                '                '************************************
                '                'Transportation Charges Voucher Credit
                '                '************************************
                '                objCommand.CommandText = ""
                '                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, sp_refrence, CostCenterId, direction) " _
                '                                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbTransportationVendor.Value & ", 0," & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", "" & Val(grd.GetRows(i).Cells("qty").Value.ToString) * Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & "", "" & (Val(grd.GetRows(i).Cells("PackQty").Value) * Val(grd.GetRows(i).Cells("Qty").Value.ToString)) * Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & "") & ", N'" & grd.GetRows(i).Cells("item").Value & " " & Me.grd.GetRows(i).Cells("Size").Value & "  (" & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & " X " & " " & Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ") " & " " & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ", " & grd.GetRows(i).Cells("ItemId").Value & ")"

                '                objCommand.ExecuteNonQuery()
                '            End If
                '        End If
                '    End If
                'End If


                'If FlgPO = False Then
                '    If Not Me.cmbPo.SelectedIndex = -1 Then
                '        If Me.cmbPo.SelectedIndex > 0 Then
                '            objCommand.CommandText = ""
                '            objCommand.CommandText = "UPDATE    PurchaseOrderDetailTable" _
                '                                    & " SET              DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & "" _
                '                                    & " WHERE     (PurchaseOrderId = " & IIf(Me.cmbPo.SelectedIndex = -1, 0, cmbPo.SelectedValue) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells("ArticleDefID").Value) & ")"

                '            objCommand.ExecuteNonQuery()
                '        End If
                '    End If
                'Else
                '    'If Val(Me.grd.GetRows(i).Cells(grdEnm.PO_ID).Value) > 0 Then

                '    If Val(Me.grd.GetRows(i).Cells("PO_ID").Value) > 0 Then
                '        objCommand.CommandText = ""
                '        objCommand.CommandText = "UPDATE  PurchaseOrderDetailTable " _
                '                               & " SET              DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells("Qty").Value) & " " _
                '                               & " WHERE     (PurchaseOrderId = " & Val(Me.grd.GetRows(i).Cells.Item("PO_ID").Value) & " AND PurchaseOrderDetailId=" & Val(Me.grd.GetRows(i).Cells.Item("PurchaseOrderDetalId").Value) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells.Item("articledefid").Value) & ")"
                '        objCommand.ExecuteNonQuery()
                '    End If
                'End If


            Next


            'If Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) > 0 Then
            '    'Inserting Tax Debit With Purchase Tax Account
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, sp_refrence, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & PurchaseTaxAccountId & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",  " & 0 & ", 'Tax Ref: " & Me.txtPONo.Text & "', N'" & Me.txtInvoiceNo.Text.Replace("'", "''").Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ")"
            '    objCommand.ExecuteNonQuery()

            '    'Inserting Tax Credit With Vendor Account
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, sp_refrence, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 'Tax Ref: " & Me.txtPONo.Text & "', N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ")"
            '    objCommand.ExecuteNonQuery()

            'End If


            ' Receipt Voucher Master Information 
            'If PaymentVoucherFlg = "True" AndAlso Val(Me.txtRecAmount.Text) <> 0 Then
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '                               & " post,Source,voucher_code, coa_detail_id, Cheque_No, Cheque_Date)" _
            '                               & " VALUES(" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", 1, " & Convert.ToInt32(Me.cmbMethod.SelectedValue) & ", N'" & VoucherNo & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                               & " " & IIf(Me.chkPost.Checked = True, 1, 0) & ",'frmPurchase',N'" & Me.txtPONo.Text & "', " & Me.cmbDepositAccount.SelectedValue & "," & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.txtChequeNo.Text.Replace("'", "''") & "'") & ", " & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.dtpChequeDate.Value.ToString("yyyy-M-d h:mmm:ss tt") & "'") & ")" _
            '                               & " SELECT @@IDENTITY"

            '    'objCommand.Transaction = trans
            '    lngVoucherMasterId = objCommand.ExecuteScalar


            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId,Cheque_No,Cheque_Date) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.txtRecAmount.Text) & ", 0, 'Payment Ref: " & Me.txtPONo.Text & "', " & Me.cmbProject.SelectedValue & ", " & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.txtChequeNo.Text.Replace("'", "''") & "'") & ", " & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.dtpChequeDate.Value.ToString("yyyy-M-d h:mmm:ss tt") & "'") & ")"

            '    'objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
            '    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtAdjustment.Text) & ", 'Adjustment Ref: " & Me.txtPONo.Text & "' )"

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, Cheque_No,Cheque_Date) " _
            '                                         & " VALUES(" & lngVoucherMasterId & "," & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbDepositAccount.SelectedValue & ", " & 0 & ",  " & Val(Me.txtRecAmount.Text) & ", 'Payment Ref: " & Me.txtPONo.Text & "', " & Me.cmbProject.SelectedValue & ", " & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.txtChequeNo.Text.Replace("'", "''") & "'") & ", " & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.dtpChequeDate.Value.ToString("yyyy-M-d h:mmm:ss tt") & "'") & " )"

            '    ' objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            'End If


            'If FlgPO = False Then
            '    If Me.cmbPo.SelectedIndex > 0 Then
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "Select IsNull(Sz1,0) as Qty , isnull(DeliveredQty,0) as DeliveredQty  from PurchaseOrderDetailTable where PurchaseOrderID = " & Me.cmbPo.SelectedValue & ""
            '        Dim da As New OleDbDataAdapter(objCommand)
            '        Dim dt As New DataTable
            '        da.Fill(dt)
            '        dt.AcceptChanges()
            '        Dim blnEqual As Boolean = True

            '        If dt.Rows.Count > 0 Then
            '            For Each r As DataRow In dt.Rows
            '                If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
            '                    blnEqual = False
            '                    Exit For
            '                End If
            '            Next
            '        End If

            '        If blnEqual = True Then
            '            objCommand.CommandText = ""
            '            objCommand.CommandText = "Update PurchaseOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where PurchaseOrderID = " & Me.cmbPo.SelectedValue & ""
            '            objCommand.ExecuteNonQuery()
            '        End If
            '    End If
            'Else
            '    Dim dtItem As DataTable = CType(Me.grd.DataSource, DataTable)
            '    dtItem.AcceptChanges()
            '    Dim objrows() As DataRow = dtItem.Select("PO_ID <> 0")
            '    If objrows IsNot Nothing Then
            '        For Each r As DataRow In objrows
            '            If Val(r.Item("PO_ID").ToString) > 0 Then
            '                objCommand.CommandText = "Select SUM(IsNull(Sz1,0)) as Qty , SUM(isnull(DeliveredQty,0)) as DeliveredQty from PurchaseOrderDetailTable where PurchaseOrderID = " & Val(r.Item("PO_ID").ToString) & ""
            '                Dim da As New OleDbDataAdapter(objCommand)
            '                da.SelectCommand = objCommand
            '                Dim dt As New DataTable
            '                da.Fill(dt)
            '                dt.AcceptChanges()
            '                Dim blnEqual As Boolean = True
            '                If dt.Rows.Count > 0 Then
            '                    For Each objrow As DataRow In dt.Rows
            '                        If objrow.Item(0) <> objrow.Item(1) AndAlso objrow.Item(0) > objrow.Item(1) Then
            '                            blnEqual = False
            '                            Exit For
            '                        End If
            '                    Next
            '                End If
            '                If blnEqual = True Then
            '                    objCommand.CommandText = ""
            '                    objCommand.CommandText = "Update PurchaseOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where PurchaseOrderID = " & Val(r.Item("PO_ID").ToString) & ""
            '                    objCommand.ExecuteNonQuery()
            '                Else
            '                    objCommand.CommandText = ""
            '                    objCommand.CommandText = "Update PurchaseOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where PurchaseOrderID = " & Val(r.Item("PO_ID").ToString) & ""
            '                    objCommand.ExecuteNonQuery()
            '                End If
            '            End If
            '        Next
            '    End If
            'End If



            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdInwardExpDetail.GetRows
            '    If Val(r.Cells("Exp_Amount").Value.ToString) <> 0 Then
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO InwardExpenseDetailTable(PurchaseId, AccountId, Exp_Amount) Values(ident_current('ReceivingNoteMasterTable'), " & Val(r.Cells("AccountId").Value.ToString) & ", " & Val(r.Cells("Exp_Amount").Value) & ")"
            '        objCommand.ExecuteNonQuery()


            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
            '                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(Me.cmbVendor.Value) & ", " & 0 & ",  " & Val(r.Cells("Exp_Amount").Value.ToString) & ", N'" & r.Cells("detail_title").Value.ToString & " Ref: " & Me.txtPONo.Text & "', " & Me.cmbProject.SelectedValue & " )"

            '        ' objCommand.Transaction = trans
            '        objCommand.ExecuteNonQuery()



            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
            '                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(r.Cells("AccountId").Value.ToString) & ",   " & Val(r.Cells("Exp_Amount").Value.ToString) & "," & 0 & ", N'" & r.Cells("detail_title").Value.ToString & " Ref: " & Me.txtPONo.Text & "', " & Me.cmbProject.SelectedValue & " )"

            '        ' objCommand.Transaction = trans
            '        objCommand.ExecuteNonQuery()
            '    End If
            'Next

            UpdatePOStatus(getVoucher_Id, trans)
            ''TASK TFS1378
            If GRNStockImpact = True Then
                FillModel()
                Call New StockDAL().Add(StockMaster, trans)
            End If
            ''END TASK TFS1378
            trans.Commit()
            Save = True
            ''insert Activity Log
            SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Purchase, Me.txtPONo.Text.Trim, True)
            SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.txtPONo.Text, True)

            ''Start TFS2375
            ''insert Approval Log
            SaveApprovalLog(EnumReferenceType.GRN, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Good Receving Note ," & cmbVendor.Text & "", Me.Name, 0)
            ''End TFS2375

            ''InsertVoucher()
            'Call Save1() 'Upgrading Stock ...
            setEditMode = False
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            Dim ValueTable As DataTable = GetSingle(getVoucher_Id)
            NotificationDAL.SaveAndSendNotification("Goods Receiving Note", "ReceivingNoteMasterTable", getVoucher_Id, ValueTable, "Purchase > Goods Receiving Note ")
        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try
    End Function




    Sub InsertVoucher()

        Try
            SaveVoucherEntry(GetVoucherTypeId("SV"), GetNextDocNo("SV", 6, "tblVoucher", "voucher_no"), Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt"), "", Nothing, GetConfigValue("ReceivingCreditAccount"), Val(Me.cmbVendor.ActiveRow.Cells(0).Text.ToString), Val(Me.txtAmount.Text), Val(Me.txtAmount.Text), "Both", Me.Name, Me.txtPONo.Text, True)
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try

    End Sub

    Private Function FormValidate() As Boolean

        If txtPONo.Text = "" Then
            msg_Error("Please enter PO No.")
            txtPONo.Focus() : FormValidate = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/26/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : FormValidate = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/26/2022)

        If cmbVendor.ActiveRow.Cells(0).Value <= 0 Then
            msg_Error("Please select Vendor")
            cmbVendor.Focus() : FormValidate = False : Exit Function
        End If

        If Not Me.grd.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            cmbItem.Focus() : FormValidate = False : Exit Function
        End If

        If getConfigValueByType("PurchaseAllowedWithPO") = "True" Then
            If Me.cmbPo.SelectedIndex = 0 Then
                msg_Error("Please select PO")
                Me.cmbPo.Focus()
                Return False
            End If
        End If
        'Task:2796 Apply Validation On Order Qty Exceed.
        If blnOrderQtyExceed = True Then
            If Me.cmbPo.SelectedIndex > 0 Then
                Dim objDt As New DataTable
                For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    If IsEditMode = True Then
                        objDt = GetDataTable("SELECT PO_DT.ArticleDefId, SUM(ISNULL(PO_DT.Qty,0)) AS Qty, SUM(IsNull(RN_DT.Qty,0))  as RNQty , IsNull(DeliveredQty, 0) As DeliveredQty , PO_DT.PurchaseOrderDetailId FROM dbo.PurchaseOrderDetailTable AS PO_DT Inner JOIN dbo.ReceivingNoteDetailTable AS RN ON PO_DT.PurchaseOrderId  = RN.PO_Id Inner JOIN (SELECT ReceivingNoteId , ArticleDefId, IsNull(ReceivedQty, 0) AS Qty FROM ReceivingNoteDetailTable " & IIf(Me.BtnSave.Text = "&Update", " WHERE ReceivingNoteId = " & Val(Me.txtReceivingID.Text) & "", "") & " ) RN_DT ON RN_DT.ReceivingNoteId = RN.ReceivingNoteId AND  RN_DT.ArticleDefID = PO_DT.ArticleDefId WHERE PO_DT.PurchaseOrderId = " & Row.Cells(grdEnm.PO_ID).Value & " " & IIf(Me.BtnSave.Text = "&Update", " And PO_DT.PurchaseOrderDetailId = " & Row.Cells(grdEnm.PurchaseOrderDetailId).Value & "", "") & " GROUP BY PO_DT.ArticleDefId, RN.PO_Id,PO_DT.DeliveredQty , PO_DT.PurchaseOrderDetailId")
                    Else
                        objDt = GetDataTable("SELECT PO_DT.ArticleDefId, SUM(ISNULL(PO_DT.Qty,0)) AS Qty, SUM(IsNull(RN_DT.Qty,0))  as RNQty , IsNull(DeliveredQty, 0) As DeliveredQty , PO_DT.PurchaseOrderDetailId FROM dbo.PurchaseOrderDetailTable AS PO_DT Left Outer JOIN dbo.ReceivingNoteDetailTable AS RN ON PO_DT.PurchaseOrderId  = RN.PO_Id Left Outer JOIN (SELECT ReceivingNoteId , ArticleDefId, IsNull(ReceivedQty, 0) AS Qty FROM ReceivingNoteDetailTable " & IIf(Me.BtnSave.Text = "&Update", " WHERE ReceivingNoteId = " & Val(Me.txtReceivingID.Text) & "", "") & " ) RN_DT ON RN_DT.ReceivingNoteId = RN.ReceivingNoteId AND  RN_DT.ArticleDefID = PO_DT.ArticleDefId WHERE PO_DT.PurchaseOrderId = " & Row.Cells(grdEnm.PO_ID).Value & " " & IIf(Me.BtnSave.Text = "&Update", " And PO_DT.PurchaseOrderDetailId = " & Row.Cells(grdEnm.PurchaseOrderDetailId).Value & "", "") & " GROUP BY PO_DT.ArticleDefId, RN.PO_Id,PO_DT.DeliveredQty, PO_DT.PurchaseOrderDetailId")
                    End If
                    If objDt IsNot Nothing Then
                        If objDt.Rows.Count > 0 Then
                            For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                                For Each r As DataRow In objDt.Rows
                                    If grdRow.Cells(grdEnm.ArticleDefId).Value = Val(r.Item("ArticleDefId").ToString) AndAlso grdRow.Cells(grdEnm.PurchaseOrderDetailId).Value = Val(r.Item("PurchaseOrderDetailId").ToString) Then
                                        If IsEditMode Then
                                            If (Val(r.Item("DeliveredQty").ToString) + Val(grdRow.Cells(grdEnm.ReceivedQty).Value) - Val(r.Item("RNQty").ToString)) > Val(r.Item("Qty").ToString) Then
                                                ShowErrorMessage("Order [" & grdRow.Cells(grdEnm.Item).Value.ToString & "] qty exceed")
                                                grd.Focus()
                                                Return False
                                            End If
                                        Else
                                            If (Val(r.Item("DeliveredQty").ToString) + Val(grdRow.Cells(grdEnm.ReceivedQty).Value)) > Val(r.Item("Qty").ToString) Then
                                                ShowErrorMessage("Order [" & grdRow.Cells(grdEnm.Item).Value.ToString & "] qty exceed")
                                                grd.Focus()
                                                Return False
                                            End If
                                        End If
                                    End If
                                Next
                            Next
                        End If
                    End If
                Next
            End If
        End If
        'End Task:2796
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
    Sub EditRecord()
        Try
            'If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            'If Me.grd.RowCount > 0 Then
            '    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            'End If

            'txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value.ToString
            'dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            'txtReceivingNoteID.Text = grdSaved.CurrentRow.Cells("ReceivingNoteId").Value
            ''TODO. ----
            'cmbVendor.Value = grdSaved.CurrentRow.Cells(3).Value

            'txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
            'txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""
            ''Me.cmbSalesMan.SelectedValue = grdSaved.CurrentRow.Cells("EmployeeCode").Value.ToString
            'Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value
            'Call DisplayDetail(grdSaved.CurrentRow.Cells("ReceivingNoteId").Value)
            'GetTotal()
            'Me.SaveToolStripButton.Text = "&Update"
            'Me.cmbPo.Enabled = False
            Me.BtnSave.Text = "&Update"
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            IsEditMode = True
            'FillCombo("Vendor") 'R933 Commented
            'FillCombo("CostCenter") 'R933 Commented
            'Me.FillCombo("SOComplete")
            txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value
            Me.GetSecurityRights()

            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
            If Not getConfigValueByType("GRNApproval") = "Error" Then
                ApprovalProcessId = getConfigValueByType("GRNApproval")
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

            'Task 1592
            If grdSaved.CurrentRow.Cells(1).Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
                dtpPODate.MaxDate = dtpPODate.Value.AddMonths(3)
                dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            Else
                dtpPODate.MaxDate = System.DateTime.MaxValue.AddYears(-2)
                dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            End If
            txtReceivingID.Text = grdSaved.CurrentRow.Cells("ReceivingNoteId").Value
            cmbVendor.Value = grdSaved.CurrentRow.Cells("VendorId").Value 'cmbVendor.FindStringExact((grdSaved.CurrentRow.Cells(3).Value))
            ''R933  Validate Vendor Data 
            'cmbVendor.Value = grdSaved.CurrentRow.Cells("VendorId").Value
            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Vendor is disable.")
                Exit Sub
            End If
            cmbLC.Value = grdSaved.CurrentRow.Cells("LCId").Value
            RemoveHandler Me.cmbPo.SelectedIndexChanged, AddressOf Me.cmbPo_SelectedIndexChanged
            cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PurchaseOrderID").Value
            'R933 Validate PO
            If cmbPo.SelectedValue Is Nothing Then
                Dim dt As DataTable = CType(Me.cmbPo.DataSource, DataTable)
                dt.AcceptChanges()
                Dim dr As DataRow
                dr = dt.NewRow
                dr(0) = Me.grdSaved.CurrentRow.Cells("PurchaseOrderId").Value
                dr(1) = Me.grdSaved.CurrentRow.Cells("PurchaseOrderNo").Value
                dt.Rows.Add(dr)
                dt.AcceptChanges()
                Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PurchaseOrderID").Value
            End If
            'End R933
            txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
            Me.chkPost.Checked = Me.grdSaved.CurrentRow.Cells("Post").Value
            txtDcNo.Text = grdSaved.CurrentRow.Cells("DcNo").Value & ""
            txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""
            Me.txtDriverName.Text = grdSaved.GetRow.Cells("Driver_Name").Value.ToString
            Me.txtVhNo.Text = grdSaved.GetRow.Cells("Vehicle_No").Value.ToString
            If Not IsDBNull(grdSaved.GetRow.Cells("DcDate").Value) Then
                Me.dtpDcDate.Value = grdSaved.GetRow.Cells("DcDate").Value
            Else
                Me.dtpDcDate.Value = Now
                Me.dtpDcDate.Checked = False
            End If

            If Not IsDBNull(grdSaved.GetRow.Cells("In Time").Value) Then
                Me.dtpArrivalTime.Value = grdSaved.GetRow.Cells("In Time").Value
            Else
                Me.dtpArrivalTime.Value = Now
            End If

            If Not IsDBNull(grdSaved.GetRow.Cells("Out Time").Value) Then
                Me.dtpDepartureTime.Value = grdSaved.GetRow.Cells("Out Time").Value
                Me.dtpDepartureTime.Checked = True
            Else
                Me.dtpDepartureTime.Value = Now
                Me.dtpDepartureTime.Checked = False
            End If

            'Me.txtDiscount.Text = Me.grdSaved.GetRow.Cells("Total_Discount_Amount").Value.ToString
            Me.txtInvoiceNo.Text = Me.grdSaved.GetRow.Cells("Vendor_Invoice_No").Value.ToString
            Me.cmbProject.SelectedValue = Val(Me.grdSaved.GetRow.Cells("CostCenterId").Value.ToString)
            Me.txtIGPNo.Text = Me.grdSaved.GetRow.Cells("IGPNo").Text.ToString
            Call DisplayDetail(grdSaved.CurrentRow.Cells("ReceivingNoteId").Value)
            Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            Me.cmbCurrency.SelectedValue = Me.grdSaved.GetRow.Cells("CurrencyType").Text.ToString
            Me.txtCurrencyRate.Text = Me.grdSaved.GetRow.Cells("CurrencyRate").Text.ToString
            If Me.cmbTransportationVendor.IsItemInList = True Then
                Me.cmbTransportationVendor.Value = Me.grdSaved.GetRow.Cells("Transportation_Vendor").Value
            End If
            If Me.cmbCustom.IsItemInList = True Then
                Me.cmbCustom.Value = Me.grdSaved.GetRow.Cells("Custom_Vendor").Value
            End If
            'GetTotal()
            If Me.cmbPo.SelectedValue > 0 Then
                Me.cmbVendor.Enabled = False
            Else
                Me.cmbVendor.Enabled = True
            End If

            If FlgPO = False Then
                If getConfigValueByType("AllowChangePO").ToString.ToUpper = "TRUE" Then
                    Me.cmbPo.Enabled = True
                Else
                    Me.cmbPo.Enabled = False
                End If
            End If

            Me.BtnSave.Text = "&Update"
            'Me.GetSecurityRights()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
            VoucherDetail(Me.txtPONo.Text)
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
            '''''''''''''''''''''''''''
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        Finally
            AddHandler Me.cmbPo.SelectedIndexChanged, AddressOf Me.cmbPo_SelectedIndexChanged
        End Try
    End Sub
    '' ReqId-899 Added field TaxPercent From Purchase Order Detail Table
    Private Sub DisplayPODetail(ByVal ReceivingNoteID As Integer)
        Try
            Dim i As Integer = 0I

            Dim str As String

            str = "SELECT IsNull(Recv_D.LocationId,0) as LocationId,Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.AlternativeItem, ArticleColorDefTable.ArticleColorName AS Color,dbo.ArticleSizeDefTable.ArticleSizeName AS Size, Uom.ArticleUnitName as Uom, Recv_D.ArticleSize AS unit, 0 as X_Tray_Weights,0 as X_Gross_Weights, 0 as X_Net_Weights,0 as Y_Gross_Weights, 0 as Y_Tray_Weights, 0 as Y_Net_Weights, Convert(float, (IsNull(Recv_D.Sz1,0) - Isnull(DeliveredQty , 0))) AS ReceivedQty , Convert(float, 0) AS RejectedQty, Convert(float, (IsNull(Recv_D.Sz1,0) - Isnull(Recv_D.DeliveredQty , 0))) AS Qty, (IsNull(Recv_D.Qty, 0) - Isnull(Recv_D.ReceivedTotalQty , 0)) AS Gross_Qty, ISNULL(NULL,0) as Column1, ISNULL(NULL,0) as Column2,  ISNULL(NULL,0) as Column3, (IsNull(Recv_D.Qty, 0) - Isnull(Recv_D.ReceivedTotalQty , 0)) As TotalQty, (IsNull(Recv_D.Qty, 0) - Isnull(Recv_D.ReceivedTotalQty , 0)) As Vendor_Qty, Convert(Decimal(18, " & DecimalPointInQty & "), 0) AS Deduction, Convert(Decimal(18, " & DecimalPointInQty & "), 0) as VendorNetQty, Recv_D.CurrentPrice,  Case When IsNull(Recv_D.CurrentPrice,0) > (IsNull(Price,0)) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent, Recv_D.Price as Rate,   " _
               & " Convert(float, (Recv_D.Qty * Recv_D.Price)) - Convert(float, (isnull(ReceivedTotalQty,0) * Recv_D.Price)) AS Total, IsNull(Recv_D.TaxPercent,0) As TaxPercent, 0 as TaxAmount, IsNull(Recv_D.AdTax_Percent,0) as AdTax_Percent, IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount, 0.0 as [Total Amount], 0 as Transportation_Charges, 0 as Custom_Vendor, 0 as Discount_Price, " _
               & " Article.ArticleGroupId, Recv_D.ArticleDefId as ArticleDefId, Recv_D.Sz7 as PackQty,0 as PackPrice, 0 as BatchId, getDate() as ExpiryDate,  '' as BatchNo ,'' as Origin, Recv_D.Comments, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc  , Isnull(Article_Group.SubSubId,0) as PurchaseAccountId,Recv_D.purchaseorderid as PO_ID, IsNull(Recv_D.PurchaseOrderDetailId,0) as PurchaseOrderDetailId, 0 as ReceivingNoteDetailId, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float, 0) As CurrencyTotalAmount, Recv_D.AlternativeItemId  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
               & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
               & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
               & " LEFT OUTER JOIN  dbo.ArticleColorDefTable ON Article.ArticleColorId = dbo.ArticleColorDefTable.ArticleColorId " _
               & " LEFT OUTER JOIN ArticleSizeDefTable ON Article.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId LEFT OUTER JOIN ArticleUnitDefTable Uom on Uom.ArticleUnitId = Article.ArticleUnitId " _
               & " Where Recv_D.PurchaseOrderID =" & ReceivingNoteID & " and  Recv_D.Sz1 - Isnull(DeliveredQty , 0) > 0" ''

            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            Dim objDataAdapter As New OleDbDataAdapter
            Dim objDataSet As New DataTable

            objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

            If objCon.State = ConnectionState.Open Then objCon.Close()

            objCon.Open()
            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = str

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(objDataSet)


            'Altered against Task# 201507018 

            If FlgPO = False Then
                Me.DisplayDetail(-1)
            End If

            'Altered against Task# 201507018 
            'objDataSet.Tables(0).Columns("Qty").Expression = "isnull(ReceivedQty,0) - isnull(RejectedQty,0)"
            'objDataSet.Tables(0).Columns("X_Net_Weights").Expression = "(IsNull(X_Gross_Weights,0)-IsNull(X_Tray_Weights,0))"
            'objDataSet.Tables(0).Columns("Y_Net_Weights").Expression = "(IsNull(Y_Gross_Weights,0)-IsNull(Y_Tray_Weights,0))"
            'objDataSet.Tables(0).Columns("AdTax_Amount").Expression = "((((IsNull(TotalQty,0)* IsNull(Rate,0))-IsNull(Discount_Price,0)) * IsNull(AdTax_Percent,0))/100)"
            objDataSet.Columns("Qty").Expression = "isnull(ReceivedQty,0) - isnull(RejectedQty,0)"
            objDataSet.Columns("Total").Expression = "IsNull(Vendor_Qty,0) * IsNull(Rate,0)"
            objDataSet.Columns("X_Net_Weights").Expression = "(IsNull(X_Gross_Weights,0)-IsNull(X_Tray_Weights,0))"
            objDataSet.Columns("Y_Net_Weights").Expression = "(IsNull(Y_Gross_Weights,0)-IsNull(Y_Tray_Weights,0))"
            objDataSet.Columns("AdTax_Amount").Expression = "((((IsNull(Total,0)* IsNull(Rate,0))-IsNull(Discount_Price,0)) * IsNull(AdTax_Percent,0))/100)"
            'objDataSet.Columns("AfterDeductionQty").Expression = "(isnull(TotalQty,0)-(IsNull(BardanaDeduction, 0)+ISNULL(OtherDeduction, 0)))"
            'objDataSet.Tables(0).Columns("AdTax_Amount").Expression = "((IIF(Unit='Pack', ((Isnull(PackQty,0)*IsNull(Qty,0))*(IsNull(Rate,0)-IsNull(Discount_Price,0))), (IsNull(Qty,0)*(IsNull(Rate,0)-IsNull(Discount_Price,0))))*IsNull(AdTax_Percent,0))/100)"
            objDataSet.Columns("Total Amount").Expression = "([Total]+([TaxAmount]+[AdTax_Amount]))"
            objDataSet.Columns("Vendor_Qty").Expression = "(IsNull(TotalQty, 0)-IsNull(Deduction, 0))"
            'objDataSet.Tables(0).Columns("Total").Expression = "[Qty] * [Price]"
            For i = 0 To objDataSet.Rows.Count - 1
                '   grd.Rows.Add(Me.cmbCategory.Text, objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), Me.cmbCategory.SelectedValue)

                ''selecting the item
                Me.cmbItem.Value = Val(objDataSet.Rows(i)("ArticleDefId").ToString)
                Me.cmbItem_Leave(Nothing, Nothing)

                ''selecting batch no
                Me.cmbBatchNo.Text = objDataSet.Rows(i)("BatchNo").ToString
                'Me.cmbBatchNo_Leave(Nothing, Nothing)

                ''selecting unit
                Me.cmbUnit.Text = objDataSet.Rows(i)("Pack_Desc").ToString ''TFS4582/TFS4705 : 04-10-2018 : Entering Unit Text According to the pack description
                Me.cmbUnit_SelectedIndexChanged(Nothing, Nothing)

                Me.txtQty.Text = Val(objDataSet.Rows(i)("Qty").ToString)
                txtPackQty.Text = Val(objDataSet.Rows(i)("PackQty").ToString)
                txtGrossQuantity.Text = Val(objDataSet.Rows(i)("Gross_Qty").ToString)

                ' Me.txtQty_LostFocus(Nothing, Nothing)
                ' Me.txtQty_TextChanged(Nothing, Nothing)
                ''selecing rate
                '  Me.txtRate.Text = objDataSet.Tables(0).Rows(i)(6)

                '*************** Chnage Calculation 17-01-2012 By Imran Ali
                ' Me.txtTotal.Text = Val(Me.txtRate.Text) * Val(Me.txtQty.Text)
                If Me.cmbUnit.Text = "Pack" Then
                    '     Me.txtTotal.Text = Val(Me.txtRate.Text) * (Val(objDataSet.Tables(0).Rows(i)(3)) * Val(objDataSet.Tables(0).Rows(i)(8)))
                Else
                    '    Me.txtTotal.Text = Val(Me.txtRate.Text) * Val(Me.txtQty.Text)
                End If
                'Me.cmbCurrency.SelectedValue = Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("CurrencyType").ToString)
                'Rafay commented this ;line because it show error input string was not in a correct format 
                ' Me.txtCurrencyRate.Text = Math.Round(Convert.ToInt32(CType(Me.cmbPo.SelectedItem, DataRowView).Item("CurrencyRate").ToString), 4)
                Me.txtCurrencyRate.Text = CType(Me.cmbPo.SelectedItem, DataRowView).Item("CurrencyRate").ToString
                'Rafay task end
                Me.cmbCategory.SelectedValue = Val(objDataSet.Rows(i)("LocationId").ToString) 'TASKM169152 Set LocationId add to grid.
                dblRate = Val(objDataSet.Rows(i)("Rate").ToString)
                dblCurrentRate = Val(objDataSet.Rows(i)("CurrentPrice").ToString)
                dblRateDiscPercent = Val(objDataSet.Rows(i)("RateDiscPercent").ToString)
                dblTaxPercent = Val(objDataSet.Rows(i)("TaxPercent").ToString)
                dblAdTaxPercent = Val(objDataSet.Rows(i)("AdTax_Percent").ToString)
                strComments = objDataSet.Rows(i)("Comments").ToString
                Me.cmbPo.Tag = Val(objDataSet.Rows(i)("PurchaseOrderDetailId").ToString)
                Me.btnAdd_Click(Nothing, Nothing)
                Me.cmbPo.Tag = String.Empty
                strComments = String.Empty
                dblTaxPercent = 0D
                dblAdTaxPercent = 0D
                dblRate = 0D
                dblCurrentRate = 0D
                dblRateDiscPercent = 0D


            Next
            'Dim dtDisplayDetail As DataTable = GetDataTable(str)
            'dtDisplayDetail.Columns.Add("TaxAmount", GetType(System.Double))
            'dtDisplayDetail.AcceptChanges()
            'dtDisplayDetail.Columns("Qty").Expression = "ReceivedQty-RejectedQty"
            'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack', ((PackQty*Qty)*Rate), (Qty*Rate))"
            'dtDisplayDetail.Columns("TaxAmount").Expression = "((IIF(Unit='Pack', ((PackQty*Qty)*(Rate-Discount_Price)), (Qty*(Rate-Discount_Price)))*TaxPercent)/100)"
            'dtDisplayDetail.Columns("Qty").Expression = "IsNull(ReceivedQty,0)-IsNull(RejectedQty,0)"
            'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack', ((IsNull(PackQty,0)*IsNull(Qty,0))*IsNull(Rate,0)), (IsNull(Qty,0)*IsNull(Rate,0)))"
            'dtDisplayDetail.Columns("TaxAmount").Expression = "((IIF(Unit='Pack', ((Isnull(PackQty,0)*IsNull(Qty,0))*(IsNull(Rate,0)-IsNull(Discount_Price,0))), (IsNull(Qty,0)*(IsNull(Rate,0)-IsNull(Discount_Price,0))))*IsNull(TaxPercent,0))/100)"
            'Me.grd.DataSource = Nothing
            'Me.grd.DataSource = dtDisplayDetail
            '    ApplyGridSettings()
            'FillCombo("grdLocation")

            'Me.CtrlGrdBar1_Load(Nothing, Nothing)
            If Me.cmbPo.SelectedIndex > 0 Then
                Me.cmbProject.SelectedValue = Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("CostCenterId").ToString)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayDetail(ByVal ReceivingNoteID As Integer)
        Try
            Dim str As String
            'Dim i As Integer
            'str = "SELECT tblDefLocation.Location_code AS Category, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, IsNull(Recv_D.ReceivedQty,0) As ReceivedQty, IsNull(Recv_D.RejectedQty,0) as RejectedQty, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice,isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.BatchID, recv_D.LocationId as LocationId FROM dbo.ReceivingNoteDetailTable Recv_D INNER JOIN " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId Inner JOIN " _
            '      & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id " _
            '      & " Where Recv_D.ReceivingNoteID =" & ReceivingNoteID & ""

            ''************************* Add 2 Columns Color And Size + Unit here.... By Imran 07-02-2012 12:33:00 PM
            'str = "SELECT  Recv_D.LocationId AS LocationId, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName AS Color,ArticleSizeDefTable.ArticleSizeName AS Size, Recv_D.ArticleSize AS unit, ISNULL(Recv_D.ReceivedQty, 0) " _
            '      & "        AS ReceivedQty, ISNULL(Recv_D.RejectedQty, 0) AS RejectedQty, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, IsNull(Recv_D.TaxPercent,0) As TaxPercent, 0.0 as TaxAmount, CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, Article.ArticleGroupId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 AS PackQty,  Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.ExpiryDate, getDate()) as ExpiryDate, ISNULL(Recv_D.BatchNo, 'xxxx') AS BatchNo, Recv_D.Comments  " _
            '      & " FROM   ReceivingNoteDetailTable Recv_D INNER JOIN " _
            '      & "        ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId INNER JOIN " _
            '      & "        tblDefLocation ON Recv_D.LocationId = tblDefLocation.location_id LEFT OUTER JOIN " _
            '      & "        ArticleColorDefTable ON Article.ArticleColorId = ArticleColorDefTable.ArticleColorId  LEFT OUTER JOIN " _
            '      & "        ArticleSizeDefTable ON Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
            '      & " Where Recv_D.ReceivingNoteID =" & ReceivingNoteID & ""

            'changing against request no 828 by imran ali add column of discount_price
            'str = "SELECT  Recv_D.LocationId AS LocationId, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName AS Color,ArticleSizeDefTable.ArticleSizeName AS Size, Recv_D.ArticleSize AS unit, ISNULL(Recv_D.ReceivedQty, 0) " _
            '    & "        AS ReceivedQty, ISNULL(Recv_D.RejectedQty, 0) AS RejectedQty, Recv_D.Sz1 AS Qty, Isnull(Recv_D.Price,0)+ISNULL(Recv_D.Discount_Price,0) as Rate, IsNull(Recv_D.TaxPercent,0) As TaxPercent, 0.0 as TaxAmount, CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, ISNULL(Recv_D.Discount_Price,0) as Discount_Price,  Article.ArticleGroupId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 AS PackQty,  Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.ExpiryDate, getDate()) as ExpiryDate, ISNULL(Recv_D.BatchNo, 'xxxx') AS BatchNo, Recv_D.Comments, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId   " _
            '    & " FROM   ReceivingNoteDetailTable Recv_D INNER JOIN " _
            '    & "        ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId INNER JOIN " _
            '    & "        tblDefLocation ON Recv_D.LocationId = tblDefLocation.location_id LEFT OUTER JOIN " _
            '    & "        ArticleColorDefTable ON Article.ArticleColorId = ArticleColorDefTable.ArticleColorId  LEFT OUTER JOIN " _
            '    & "        ArticleSizeDefTable ON Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN ArticleGroupDefTable Article_Group on Article_Group.ArticleGroupId = Article.ArticleGroupId " _
            '    & " Where Recv_D.ReceivingNoteID =" & ReceivingNoteID & ""

            '   str = "SELECT  Recv_D.LocationId AS LocationId, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName AS Color,ArticleSizeDefTable.ArticleSizeName AS Size, Recv_D.ArticleSize AS unit, ISNULL(Recv_D.ReceivedQty, 0) " _
            '& "        AS ReceivedQty, ISNULL(Recv_D.RejectedQty, 0) AS RejectedQty, Recv_D.Sz1 AS Qty, Isnull(Recv_D.Price,0)+ISNULL(Recv_D.Discount_Price,0) as Rate, IsNull(Recv_D.TaxPercent,0) As TaxPercent, 0.0 as TaxAmount, CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, ISNULL(Recv_D.Discount_Price,0) as Discount_Price,  Article.ArticleGroupId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 AS PackQty,  Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.ExpiryDate, getDate()) as ExpiryDate, ISNULL(Recv_D.BatchNo, 'xxxx') AS BatchNo, Recv_D.Comments, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId   " _
            '& " FROM   ReceivingNoteDetailTable Recv_D INNER JOIN " _
            '& "        ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId INNER JOIN " _
            '& "        tblDefLocation ON Recv_D.LocationId = tblDefLocation.location_id LEFT OUTER JOIN " _
            '& "        ArticleColorDefTable ON Article.ArticleColorId = ArticleColorDefTable.ArticleColorId  LEFT OUTER JOIN " _
            '& "        ArticleSizeDefTable ON Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN ArticleGroupDefTable Article_Group on Article_Group.ArticleGroupId = Article.ArticleGroupId " _
            '& " Where Recv_D.ReceivingNoteID =" & ReceivingNoteID & ""


            ''03-Feb-2014          Task:2407    Imran Ali       unit of measurement on GRN and purchase window.
            '   str = "SELECT  Recv_D.LocationId AS LocationId, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName AS Color,ArticleSizeDefTable.ArticleSizeName AS Size,Uom.ArticleUnitName as Uom, Recv_D.ArticleSize AS unit, ISNULL(Recv_D.ReceivedQty, 0) " _
            '& "        AS ReceivedQty, ISNULL(Recv_D.RejectedQty, 0) AS RejectedQty, Recv_D.Sz1 AS Qty, Isnull(Recv_D.Price,0)+ISNULL(Recv_D.Discount_Price,0) as Rate, IsNull(Recv_D.TaxPercent,0) As TaxPercent, 0.0 as TaxAmount, CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, ISNULL(Recv_D.Discount_Price,0) as Discount_Price,  Article.ArticleGroupId, Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 AS PackQty,  Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.ExpiryDate, getDate()) as ExpiryDate, ISNULL(Recv_D.BatchNo, 'xxxx') AS BatchNo, Recv_D.Comments, isnull(Po_ID,0) as PO_ID,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId   " _
            '& " FROM   ReceivingNoteDetailTable Recv_D INNER JOIN " _
            '& "        ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId INNER JOIN " _
            '& "        tblDefLocation ON Recv_D.LocationId = tblDefLocation.location_id LEFT OUTER JOIN " _
            '& "        ArticleColorDefTable ON Article.ArticleColorId = ArticleColorDefTable.ArticleColorId  LEFT OUTER JOIN " _
            '& "        ArticleSizeDefTable ON Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN ArticleGroupDefTable Article_Group on Article_Group.ArticleGroupId = Article.ArticleGroupId LEFT OUTER JOIN ArticleUnitDefTable Uom on Uom.ArticleUnitId = Article.ArticleUnitId " _
            '& " Where Recv_D.ReceivingNoteID =" & ReceivingNoteID & ""
            'End Task:2407

            'str = "SELECT  Recv_D.LocationId AS LocationId ,Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName AS Color,ArticleSizeDefTable.ArticleSizeName AS Size,Uom.ArticleUnitName as Uom, Recv_D.ArticleSize AS unit, ISNULL(Recv_D.ReceivedQty, 0) " _
            '         & "        AS ReceivedQty, ISNULL(Recv_D.RejectedQty, 0) AS RejectedQty, Recv_D.Sz1 AS Qty, Isnull(Recv_D.Price,0)+ISNULL(Recv_D.Discount_Price,0) as Rate, IsNull(Recv_D.TaxPercent,0) As TaxPercent, 0.0 as TaxAmount, CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, ISNULL(Recv_D.Discount_Price,0) as Discount_Price,  Article.ArticleGroupId, Recv_D.ArticleDefId as ArticleDefId, Recv_D.Sz7 AS PackQty,  Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.ExpiryDate, getDate()) as ExpiryDate, ISNULL(Recv_D.BatchNo, 'xxxx') AS BatchNo, Recv_D.Comments,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, isnull(Po_ID,0) as PO_ID   " _
            '         & " FROM   ReceivingNoteDetailTable Recv_D INNER JOIN " _
            '         & "        ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId INNER JOIN " _
            '         & "        tblDefLocation ON Recv_D.LocationId = tblDefLocation.location_id LEFT OUTER JOIN " _
            '         & "        ArticleColorDefTable ON Article.ArticleColorId = ArticleColorDefTable.ArticleColorId  LEFT OUTER JOIN " _
            '         & "        ArticleSizeDefTable ON Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN ArticleGroupDefTable Article_Group on Article_Group.ArticleGroupId = Article.ArticleGroupId LEFT OUTER JOIN ArticleUnitDefTable Uom on Uom.ArticleUnitId = Article.ArticleUnitId " _
            '         & " Where Recv_D.ReceivingNoteID =" & ReceivingNoteID & ""
            'Before TASK-TFS-51 
            'str = "SELECT  IsNull(Recv_D.LocationId,0) AS LocationId ,Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName AS Color,ArticleSizeDefTable.ArticleSizeName AS Size,Uom.ArticleUnitName as Uom, Recv_D.ArticleSize AS unit, IsNull(Recv_D.X_Tray_Weights,0) as X_Tray_Weights, IsNull(Recv_D.X_Gross_Weights,0) as X_Gross_Weights,  IsNull(Recv_D.X_Net_Weights,0) as X_Net_Weights,  IsNull(Recv_D.Y_Gross_Weights,0) as Y_Gross_Weights, IsNull(Recv_D.Y_Tray_Weights,0) as Y_Tray_Weights, IsNull(Recv_D.Y_Net_Weights,0) as Y_Net_Weights, ISNULL(Recv_D.ReceivedQty, 0) " _
            '       & "        AS ReceivedQty, ISNULL(Recv_D.RejectedQty, 0) AS RejectedQty, Recv_D.Sz1 AS Qty, Isnull(Recv_D.Price,0)+ISNULL(Recv_D.Discount_Price,0) as Rate, IsNull(Recv_D.TaxPercent,0) As TaxPercent, 0.0 as TaxAmount, CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, ISNULL(Recv_D.Discount_Price,0) as Discount_Price,  Article.ArticleGroupId, Recv_D.ArticleDefId as ArticleDefId, Recv_D.Sz7 AS PackQty,  Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.ExpiryDate, getDate()) as ExpiryDate, ISNULL(Recv_D.BatchNo, 'xxxx') AS BatchNo, Recv_D.Comments,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, isnull(Po_ID,0) as PO_ID   " _
            '       & " FROM   ReceivingNoteDetailTable Recv_D INNER JOIN " _
            '       & "        ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId INNER JOIN " _
            '       & "        tblDefLocation ON Recv_D.LocationId = tblDefLocation.location_id LEFT OUTER JOIN " _
            '       & "        ArticleColorDefTable ON Article.ArticleColorId = ArticleColorDefTable.ArticleColorId  LEFT OUTER JOIN " _
            '       & "        ArticleSizeDefTable ON Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN ArticleGroupDefTable Article_Group on Article_Group.ArticleGroupId = Article.ArticleGroupId LEFT OUTER JOIN ArticleUnitDefTable Uom on Uom.ArticleUnitId = Article.ArticleUnitId " _
            '       & " Where Recv_D.ReceivingNoteID =" & ReceivingNoteID & ""
            'TASK-TFS-51 Added Fields AdTax_Percent, AdTax_Amount
            'str = "SELECT  IsNull(Recv_D.LocationId,0) AS LocationId ,Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName AS Color,ArticleSizeDefTable.ArticleSizeName AS Size,Uom.ArticleUnitName as Uom, Recv_D.ArticleSize AS unit, IsNull(Recv_D.X_Tray_Weights,0) as X_Tray_Weights, IsNull(Recv_D.X_Gross_Weights,0) as X_Gross_Weights,  IsNull(Recv_D.X_Net_Weights,0) as X_Net_Weights,  IsNull(Recv_D.Y_Gross_Weights,0) as Y_Gross_Weights, IsNull(Recv_D.Y_Tray_Weights,0) as Y_Tray_Weights, IsNull(Recv_D.Y_Net_Weights,0) as Y_Net_Weights, ISNULL(Recv_D.ReceivedQty, 0) " _
            '     & "        AS ReceivedQty, ISNULL(Recv_D.RejectedQty, 0) AS RejectedQty, Recv_D.Sz1 AS Qty, Isnull(Recv_D.Price,0)+ISNULL(Recv_D.Discount_Price,0) as Rate, CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price)) ELSE Convert(float, ((Recv_D.Sz1  * Article.PackQty) * Recv_D.Price)) END AS Total, IsNull(Recv_D.TaxPercent,0) As TaxPercent, 0.0 as TaxAmount, IsNull(Recv_D.AdTax_Percent,0) As AdTax_Percent, IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount, 0.0 as [Total Amount], Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges, ISNULL(Recv_D.Discount_Price,0) as Discount_Price,  Article.ArticleGroupId, Recv_D.ArticleDefId as ArticleDefId, Recv_D.Sz7 AS PackQty,  Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.ExpiryDate, getDate()) as ExpiryDate, ISNULL(Recv_D.BatchNo, 'xxxx') AS BatchNo, Recv_D.Comments,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, isnull(Recv_D.Po_ID,0) as PO_ID, IsNull(Recv_D.PurchaseOrderDetailId,0) as PurchaseOrderDetailId, IsNull(Recv_D.ReceivingDetailID,0) as ReceivingNoteDetailId   " _
            '     & " FROM   ReceivingNoteDetailTable Recv_D INNER JOIN " _
            '     & "        ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId INNER JOIN " _
            '     & "        tblDefLocation ON Recv_D.LocationId = tblDefLocation.location_id LEFT OUTER JOIN " _
            '     & "        ArticleColorDefTable ON Article.ArticleColorId = ArticleColorDefTable.ArticleColorId  LEFT OUTER JOIN " _
            '     & "        ArticleSizeDefTable ON Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN ArticleGroupDefTable Article_Group on Article_Group.ArticleGroupId = Article.ArticleGroupId LEFT OUTER JOIN ArticleUnitDefTable Uom on Uom.ArticleUnitId = Article.ArticleUnitId " _
            '     & " Where Recv_D.ReceivingNoteID =" & ReceivingNoteID & ""
            'END TASK-TFS-51


            str = "SELECT  IsNull(Recv_D.LocationId,0) AS LocationId ,Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.AlternativeItem, ArticleColorDefTable.ArticleColorName AS Color,ArticleSizeDefTable.ArticleSizeName AS Size,Uom.ArticleUnitName as Uom, Recv_D.ArticleSize AS unit, IsNull(Recv_D.X_Tray_Weights,0) as X_Tray_Weights, IsNull(Recv_D.X_Gross_Weights,0) as X_Gross_Weights,  IsNull(Recv_D.X_Net_Weights,0) as X_Net_Weights,  IsNull(Recv_D.Y_Gross_Weights,0) as Y_Gross_Weights, IsNull(Recv_D.Y_Tray_Weights,0) as Y_Tray_Weights, IsNull(Recv_D.Y_Net_Weights,0) as Y_Net_Weights, ISNULL(Recv_D.ReceivedQty, 0) " _
             & "        AS ReceivedQty, ISNULL(Recv_D.RejectedQty, 0) AS RejectedQty, Recv_D.Sz1 AS Qty, IsNull(Recv_D.Gross_Quantity,0) AS Gross_Qty, ISNULL(Recv_D.Column1,0) as Column1, ISNULL(Recv_D.Column2,0) as Column2,  ISNULL(Recv_D.Column3,0) as Column3, Convert(Decimal(18, " & DecimalPointInQty & "),IsNull(Recv_D.Qty, 0), 1) As TotalQty, IsNull(Recv_D.Vendor_Total_Quantity,0) as Vendor_Qty,  Convert(Decimal(18, " & DecimalPointInQty & "), IsNull(Recv_D.OtherDeduction, 0)) as Deduction,  Convert(Decimal(18, " & DecimalPointInQty & "), 0) as VendorNetQty, Recv_D.CurrentPrice, Case When IsNull(Recv_D.CurrentPrice,0) > (IsNull(Price,0)+ISNULL(Recv_D.Discount_Price,0)) then ((IsNull(Recv_D.CurrentPrice,0)-IsNull(Price,0)+ISNULL(Recv_D.Discount_Price,0))/IsNull(Recv_D.CurrentPrice,0))*100 else 0 end as RateDiscPercent, Isnull(Recv_D.Price,0)+ISNULL(Recv_D.Discount_Price,0) as Rate, Convert(float, IsNull(Recv_D.Qty, 0) * IsNull(Recv_D.Price, 0)) AS Total, IsNull(Recv_D.TaxPercent,0) As TaxPercent, 0.0 as TaxAmount, IsNull(Recv_D.AdTax_Percent,0) As AdTax_Percent, IsNull(Recv_D.AdTax_Amount,0) as AdTax_Amount, 0.0 as [Total Amount], Isnull(Recv_D.Transportation_Charges,0) as Transportation_Charges , Isnull(Recv_D.Custom_Charges,0) as Custom_Charges, ISNULL(Recv_D.Discount_Price,0) as Discount_Price,  Article.ArticleGroupId, Recv_D.ArticleDefId as ArticleDefId, Recv_D.Sz7 AS PackQty,  Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, ISNULL(Recv_D.ExpiryDate, getDate()) as ExpiryDate, ISNULL(Recv_D.BatchNo, 'xxxx') AS BatchNo,ISNULL(Recv_D.Origin, '') as Origin, Recv_D.Comments,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as PurchaseAccountId, isnull(Recv_D.Po_ID,0) as PO_ID, IsNull(Recv_D.PurchaseOrderDetailId,0) as PurchaseOrderDetailId, IsNull(Recv_D.ReceivingDetailID,0) as ReceivingNoteDetailId, Recv_D.BaseCurrencyId, Recv_D.BaseCurrencyRate, Recv_D.CurrencyId, Recv_D.CurrencyRate, Recv_D.CurrencyAmount, Recv_D.AlternativeItemId " _
             & " FROM   ReceivingNoteDetailTable Recv_D INNER JOIN " _
             & "        ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
             & "        tblDefLocation ON Recv_D.LocationId = tblDefLocation.location_id LEFT OUTER JOIN " _
             & "        ArticleColorDefTable ON Article.ArticleColorId = ArticleColorDefTable.ArticleColorId  LEFT OUTER JOIN " _
             & "        ArticleSizeDefTable ON Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN ArticleGroupDefTable Article_Group on Article_Group.ArticleGroupId = Article.ArticleGroupId LEFT OUTER JOIN ArticleUnitDefTable Uom on Uom.ArticleUnitId = Article.ArticleUnitId " _
             & " Where Recv_D.ReceivingNoteID =" & ReceivingNoteID & ""

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
            'objDataSet.Tables(0).Columns("Qty").Expression = "ReceivedQty - RejectedQty"
            ''objDataSet.Tables(0).Columns("Total").Expression = "Qty * Price"
            'For i = 0 To objDataSet.Tables(0).Rows.Count - 1
            '    'grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo").ToString, objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)("BatchID"), objDataSet.Tables(0).Rows(i)("LocationId"))
            '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(grdEnm.Category), objDataSet.Tables(0).Rows(i)(grdEnm.Item), objDataSet.Tables(0).Rows(i)(grdEnm.Color), objDataSet.Tables(0).Rows(i)(grdEnm.Size), objDataSet.Tables(0).Rows(i)(grdEnm.BatchNo).ToString, objDataSet.Tables(0).Rows(i)(grdEnm.Unit), objDataSet.Tables(0).Rows(i)(grdEnm.ReceivedQty), objDataSet.Tables(0).Rows(i)(grdEnm.RejectedQty), objDataSet.Tables(0).Rows(i)(grdEnm.Qty), objDataSet.Tables(0).Rows(i)(grdEnm.Price), objDataSet.Tables(0).Rows(i)(grdEnm.Total), objDataSet.Tables(0).Rows(i)(grdEnm.TaxPercent), objDataSet.Tables(0).Rows(i)(grdEnm.ArticleGroupId), objDataSet.Tables(0).Rows(i)(grdEnm.ArticleDefId), objDataSet.Tables(0).Rows(i)(grdEnm.PackQty), objDataSet.Tables(0).Rows(i)(grdEnm.CurrentPrice), objDataSet.Tables(0).Rows(i)(grdEnm.BatchId), objDataSet.Tables(0).Rows(i)(grdEnm.LocationId))
            '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)
            'Next
            ''//grd.Columns("Category").HeaderText = "Location"





            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            'dtDisplayDetail.Columns.Add("TaxAmount", GetType(System.Double))
            dtDisplayDetail.AcceptChanges()

            dtDisplayDetail.Columns("Qty").Expression = "IsNull(ReceivedQty,0)-IsNull(RejectedQty,0)"
            'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack', ((IsNull(PackQty,0)*IsNull(Qty,0))*IsNull(Rate,0)), (IsNull(Qty,0)*IsNull(Rate,0)))"
            'dtDisplayDetail.Columns("Total").Expression = "IsNull(TotalQty,0) * IsNull(Rate,0)"
            dtDisplayDetail.Columns("Total").Expression = "IsNull(Vendor_Qty,0) * IsNull(Rate,0)"

            'dtDisplayDetail.Columns("TaxAmount").Expression = "((IIF(Unit='Pack', ((Isnull(PackQty,0)*IsNull(Qty,0))*(IsNull(Rate,0)-IsNull(Discount_Price,0))), (IsNull(Qty,0)*(IsNull(Rate,0)-IsNull(Discount_Price,0))))*IsNull(TaxPercent,0))/100)"
            'dtDisplayDetail.Columns("AdTax_Amount").Expression = "((IIF(Unit='Pack', ((Isnull(PackQty,0)*IsNull(Qty,0))*(IsNull(Rate,0)-IsNull(Discount_Price,0))), (IsNull(Qty,0)*(IsNull(Rate,0)-IsNull(Discount_Price,0))))*IsNull(AdTax_Percent,0))/100)"
            dtDisplayDetail.Columns("TaxAmount").Expression = "((((IsNull(TotalQty,0)* IsNull(Rate,0))-IsNull(Discount_Price,0)) * IsNull(TaxPercent,0))/100)"
            dtDisplayDetail.Columns("AdTax_Amount").Expression = "((((IsNull(TotalQty,0)* IsNull(Rate,0))-IsNull(Discount_Price,0)) * IsNull(AdTax_Percent,0))/100)"
            dtDisplayDetail.Columns("Total Amount").Expression = "([Total]+([TaxAmount]+[AdTax_Amount]))"
            dtDisplayDetail.Columns("X_Net_Weights").Expression = "(IsNull(X_Gross_Weights,0)-IsNull(X_Tray_Weights,0))"
            dtDisplayDetail.Columns("Y_Net_Weights").Expression = "(IsNull(Y_Gross_Weights,0)-IsNull(Y_Tray_Weights,0))"
            'objDataSet.Tables(0).Columns("AdTax_Amount").Expression = "((IIF(Unit='Pack', ((Isnull(PackQty,0)*IsNull(Qty,0))*(IsNull(Rate,0)-IsNull(Discount_Price,0))), (IsNull(Qty,0)*(IsNull(Rate,0)-IsNull(Discount_Price,0))))*IsNull(AdTax_Percent,0))/100)"
            dtDisplayDetail.Columns("Total Amount").Expression = "([Total]+([TaxAmount]+[AdTax_Amount]))"
            dtDisplayDetail.Columns("VendorNetQty").Expression = "(IsNull(Vendor_Qty,0)-(IsNull(Deduction,0)))"
            'dtDisplayDetail.Columns("Deduction").Expression = "(IsNull(Vendor_Qty,0)!>IsNull(Deduction, 0))"
            'dtDisplayDetail.Columns("AfterDeductionQty").Expression = "(isnull(TotalQty,0)-(IsNull(BardanaDeduction, 0)+ISNULL(OtherDeduction, 0)))"


            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            ApplyGridSettings()
            'If getConfigValueByType("ItemExpiryDateOnPurchase").ToString = "True" Then Me.grd.RootTable.Columns(grdEnm.ExpiryDate).Visible = True Else Me.grd.RootTable.Columns(grdEnm.ExpiryDate).Visible = False
            FillCombo("grdLocation")
            FillCombo("GrdOrigin")



            'Dim InwardExpId As Integer = 0I
            'If Not getConfigValueByType("InwardExpHeadAcId").ToString = "Error" Then
            '    InwardExpId = getConfigValueByType("InwardExpHeadAcId").ToString
            'End If
            'Dim dtInwardExpDetail As DataTable = GetDataTable("Select Exp.PurchaseId, vw.coa_detail_id as AccountId , vw.Detail_Title, isnull(Exp_Amount,0) as Exp_Amount From vwCoaDetail vw LEFT OUTER JOIN (select PurchaseId, AccountId, Exp_Amount From InwardExpenseDetailTable WHERE PurchaseId=" & ReceivingNoteID & ") Exp ON Exp.AccountId = vw.coa_detail_id WHERE  vw.main_sub_sub_id=" & InwardExpId)
            'Me.grdInwardExpDetail.DataSource = dtInwardExpDetail

            Me.CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function Update_Record() As Boolean
        If ApprovalProcessId = 0 Then
            ''Uncommented Against TFS2375
            If Me.chkPost.Visible = False Then
                Me.chkPost.Checked = False
            End If
            ''EndTFS2375
        Else
            Me.chkPost.Visible = False
        End If
        'Dim PurchaseTaxAccountId As Integer = Val(getConfigValueByType("PurchaseTaxDebitAccountId").ToString) 'GetConfigValue("PurchaseTaxDebitAccountId").ToString
        'Dim PaymentVoucherFlg As String = getConfigValueByType("PaymentVoucherOnPurchase").ToString 'GetConfigValue("PaymentVoucherOnPurchase").ToString
        'Dim flgAvgRate As Boolean = Convert.ToBoolean(getConfigValueByType("AvgRate").ToString) 'GetConfigValue("PurchaseDebitAccount")
        'Dim VoucherNo As String = GetVoucherNo()


        If Not getConfigValueByType("AvgRate").ToString = "Error" Then
            flgAvgRate = getConfigValueByType("AvgRate")
        End If
        ''END TASK TFS1378


        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        gobjLocationId = MyCompanyId
        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Dim cmd As New OleDbCommand
        cmd.Connection = Con
        cmd.CommandType = CommandType.Text
        'cmd.CommandText = "SELECT IsNull(Sz1,0) as Qty, ArticleDefID, IsNull(PO_ID,0) as PO_ID,IsNull(PurchaseOrderDetailId,0) as PurchaseOrderDetailId FROM ReceivingNoteDetailTable WHERE  ReceivingNoteID = " & Me.txtReceivingID.Text & ""
        'Dim da As New OleDbDataAdapter(cmd)
        'Dim dtSavedItems As New DataTable
        'da.Fill(dtSavedItems)

        'Dim lngVoucherMasterId As Integer = GetVoucherId("frmPurchase", Me.txtPONo.Text)

        'Dim strVoucherNo As String = String.Empty
        'Dim dt As DataTable = GetRecords("SELECT voucher_no   FROM tblVoucher  WHERE voucher_id = " & lngVoucherMasterId & " ")

        'If Not dt Is Nothing Then
        '    If Not dt.Rows.Count = 0 Then
        '        strVoucherNo = dt.Rows(0)("voucher_no")
        '    End If
        'End If
        'Dim AccountId As Integer = Val(getConfigValueByType("PurchaseDebitAccount").ToString) 'GetConfigValue("PurchaseDebitAccount")

        'If AccountId <= 0 Then
        '    ShowErrorMessage("Purchase account is not map.")
        '    Me.dtpPODate.Focus()
        '    Return False
        'End If
        'If Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) <> 0 Then
        '    If PurchaseTaxAccountId <= 0 Then
        '        ShowErrorMessage("Purchase tax account is not map.")
        '        Me.dtpPODate.Focus()
        '        Return False
        '    End If
        'End If
        'Dim GLAccountArticleDepartment As Boolean
        'If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
        '    GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
        'Else
        '    GLAccountArticleDepartment = False
        'End If
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text

            objCommand.Transaction = trans

            'Dim transId As Integer = 0
            'transId = Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
            'objCon.BeginTransaction()

            'objCommand.CommandText = "Update ReceivingNoteMasterTable set LocationId=" & gobjLocationId & ", ReceivingNo =N'" & txtPONo.Text & "',ReceivingDate=N'" & dtpPODate.Value & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", PurchaseOrderId=" & IIf(Me.cmbPo.SelectedIndex > 0, Me.cmbPo.SelectedValue, 0) & ", " _
            '& " ReceivingQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",ReceivingAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text & "',UserName=N'" & LoginUserName & "', DcNo=N'" & Me.txtDcNo.Text & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", Vehicle_No=N'" & Me.txtVhNo.Text & "', Driver_Name=N'" & Me.txtDriverName.Text & "', DcDate=" & IIf(Me.dtpDcDate.Checked = True, "N'" & dtpDcDate.Value & "'", "NULL") & ", Vendor_Invoice_No=N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', CostCenterId=" & Me.cmbProject.SelectedValue & ", IGPNo=N'" & Me.txtIGPNo.Text & "', CurrencyType=" & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & ", CurrencyRate=" & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", Transportation_Vendor=" & IIf(Me.cmbTransportationVendor.ActiveRow Is Nothing, 0, Me.cmbTransportationVendor.Value) & "  Where ReceivingNoteID= " & txtReceivingNoteID.Text & " "

            ' Commented by Rai Shahid 14-Sep-15
            ' 'Changing against request no 828 by imran ali add column of total discount amount 
            ' objCommand.CommandText = "Update ReceivingNoteMasterTable set LocationId=" & gobjLocationId & ", ReceivingNo =N'" & txtPONo.Text & "',ReceivingDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", PurchaseOrderId=" & IIf(Me.cmbPo.SelectedIndex > 0, Me.cmbPo.SelectedValue, 0) & ", " _
            '& " ReceivingQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",ReceivingAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UpdateUserName=N'" & LoginUserName & "', DcNo=N'" & Me.txtDcNo.Text.Replace("'", "''") & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", Vehicle_No=N'" & Me.txtVhNo.Text.Replace("'", "''") & "', Driver_Name=N'" & Me.txtDriverName.Text.Replace("'", "''") & "', DcDate=" & IIf(Me.dtpDcDate.Checked = True, "N'" & dtpDcDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Vendor_Invoice_No=N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', CostCenterId=" & Me.cmbProject.SelectedValue & ", IGPNo=N'" & Me.txtIGPNo.Text.Replace("'", "''") & "', Transportation_Vendor=" & IIf(Me.cmbTransportationVendor.ActiveRow Is Nothing, 0, Me.cmbTransportationVendor.Value) & "   Where ReceivingNoteID= " & txtReceivingID.Text & " "

            'Change by Rai Shahid 14-Sep-15: PO and GRN removes automatically
            ' objCommand.CommandText = "Update ReceivingNoteMasterTable set LocationId=" & gobjLocationId & ", ReceivingNo =N'" & txtPONo.Text & "',ReceivingDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", PurchaseOrderId=" & IIf(Me.cmbPo.SelectedIndex > 0, Me.cmbPo.SelectedValue, 0) & ", " _
            '& " ReceivingQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",ReceivingAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UpdateUserName=N'" & LoginUserName & "', DcNo=N'" & Me.txtDcNo.Text.Replace("'", "''") & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", Vehicle_No=N'" & Me.txtVhNo.Text.Replace("'", "''") & "', Driver_Name=N'" & Me.txtDriverName.Text.Replace("'", "''") & "', DcDate=" & IIf(Me.dtpDcDate.Checked = True, "N'" & dtpDcDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Vendor_Invoice_No=N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', CostCenterId=" & Me.cmbProject.SelectedValue & ", IGPNo=N'" & Me.txtIGPNo.Text.Replace("'", "''") & "', Transportation_Vendor=" & IIf(Me.cmbTransportationVendor.ActiveRow Is Nothing, 0, Me.cmbTransportationVendor.Value) & "   Where ReceivingNoteID= " & txtReceivingID.Text & " "
            '' TASK TFS1261 Added new column LCId which concerns for Import document.

            objCommand.CommandText = ""
            objCommand.CommandText = "Update ReceivingNoteMasterTable set LocationId=" & gobjLocationId & ", ReceivingNo =N'" & txtPONo.Text & "',ReceivingDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", PurchaseOrderId=" & IIf(Me.cmbPo.SelectedIndex > 0, Me.cmbPo.SelectedValue, 0) & ", " _
         & " ReceivingQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",ReceivingAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UpdateUserName=N'" & LoginUserName & "', DcNo=N'" & Me.txtDcNo.Text.Replace("'", "''") & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", Vehicle_No=N'" & Me.txtVhNo.Text.Replace("'", "''") & "', Driver_Name=N'" & Me.txtDriverName.Text.Replace("'", "''") & "', DcDate=" & IIf(Me.dtpDcDate.Checked = True, "N'" & dtpDcDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Vendor_Invoice_No=N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', CostCenterId=" & Me.cmbProject.SelectedValue & ", IGPNo=N'" & Me.txtIGPNo.Text.Replace("'", "''") & "', CurrencyType=" & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & ", CurrencyRate=" & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", Transportation_Vendor=" & IIf(Me.cmbTransportationVendor.ActiveRow Is Nothing, 0, Me.cmbTransportationVendor.Value) & ", Custom_Vendor=" & IIf(Me.cmbCustom.ActiveRow Is Nothing, 0, Me.cmbCustom.Value) & ",Arrival_Time=Convert(DateTime,'" & Me.dtpArrivalTime.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102), Departure_Time=" & IIf(Me.dtpDepartureTime.Checked = True, "Convert(Datetime,'" & Me.dtpDepartureTime.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)", "NULL") & ", LCId = " & IIf(Me.cmbLC.ActiveRow Is Nothing, 0, Me.cmbLC.Value) & "   Where ReceivingNoteID= " & txtReceivingID.Text & " "
            objCommand.ExecuteNonQuery()
            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari

            UpdatePOStatus(Val(Me.txtReceivingID.Text), trans, "Delete")

            If arrFile.Count > 0 Then
                SaveDocument(Val(txtReceivingID.Text), Me.Name, trans)
            End If

            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from ReceivingNoteDetailTable where ReceivingNoteID = " & txtReceivingID.Text
            objCommand.ExecuteNonQuery()

            'objCommand.CommandText = ""

            'objCommand.CommandText = "update tblVoucher set voucher_date=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & " " _
            '                        & "   where voucher_id=" & lngVoucherMasterId

            'objCommand.ExecuteNonQuery()

            '***********************
            'Deleting Detail
            '***********************
            'objCommand.CommandText = ""
            'objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId

            'objCommand.ExecuteNonQuery()



            ''make reversal of saved items in sale order detail table against poid
            'If dtSavedItems.Rows.Count > 0 Then
            '    For Each r As DataRow In dtSavedItems.Rows
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "Update PurchaseOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & ") where PurchaseOrderID = " & IIf(FlgPO = False, Me.cmbPo.SelectedValue, Val(r.Item(2).ToString)) & " and ArticleDefID = " & Val(r.Item(1).ToString) & " " & IIf(Val(r.Item("PurchaseOrderDetailId").ToString) > 0, " AND PurchaseOrderDetailId=" & Val(r.Item("PurchaseOrderDetailId").ToString) & "", "") & ""
            '        objCommand.ExecuteNonQuery()
            '    Next
            'End If

            Dim strSQL As String = String.Empty
            Dim BatchID As Integer = 0
            StockList = New List(Of StockDetail)
            For i = 0 To grd.RowCount - 1


                '********************************
                ' Update Purchase Price By Imran
                '********************************
                'Dim CrrStock As Double = 0D
                'Dim StockDetail.Rate As Double = 0D
                'If GLAccountArticleDepartment = True Then
                '    AccountId = Val(grd.GetRows(i).Cells("PurchaseAccountId").Value.ToString)
                'End If
                'If flgAvgRate = True Then
                '    Try
                '        objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))) as Amount  " _
                '                                & " FROM dbo.ArticleDefTable INNER JOIN " _
                '                                & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & grd.GetRows(i).Cells("ItemId").Value & " AND StockDetailTable.StockTransId IN(Select StockTransId From StockMasterTable WHERE DocNo <> N'" & Me.txtPONo.Text.Replace("'", "''") & "') " _
                '                                & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                '        Dim dtCrrStock As New DataTable
                '        Dim daCrrStock As New OleDbDataAdapter(objCommand)
                '        daCrrStock.Fill(dtCrrStock)
                '        If dtCrrStock IsNot Nothing Then
                '            If dtCrrStock.Rows.Count > 0 Then
                '                If Val(grd.GetRows(i).Cells("rate").Value.ToString) <> 0 Then
                '                    'CrrStock = dtCrrStock.Rows(0).Item(2) + IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value)))
                '                    'StockDetail.Rate = ((dtCrrStock.Rows(0).Item(3)) + IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) * (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString))) / CrrStock
                '                    StockDetail.Rate = Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)
                '                Else
                '                    StockDetail.Rate = Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)
                '                End If
                '            Else
                '                StockDetail.Rate = Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)
                '            End If
                '        End If


                '    Catch ex As Exception

                '    End Try
                'End If

                'StockDetail = New StockDetail
                'StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                'StockDetail.LocationId = grd.GetRows(i).Cells("LocationID").Value
                'StockDetail.ArticleDefId = grd.GetRows(i).Cells("ItemId").Value
                'StockDetail.InQty = IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("Qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value)))
                'StockDetail.OutQty = 0
                'StockDetail.Rate = IIf(StockDetail.Rate = 0, Val(grd.GetRows(i).Cells("Rate").Value.ToString), StockDetail.Rate)
                'StockDetail.InAmount = IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * IIf(StockDetail.Rate = 0, Val(grd.GetRows(i).Cells("Rate").Value.ToString), StockDetail.Rate)), ((Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value)) * IIf(StockDetail.Rate = 0, Val(grd.GetRows(i).Cells("Rate").Value.ToString), StockDetail.Rate)))
                'StockDetail.OutAmount = 0
                'StockDetail.Remarks = String.Empty
                'StockList.Add(StockDetail)


                'If Me.cmbPo.SelectedIndex > 0 Then
                '    strSQL = "select BatchID from PurchaseBatchTable where BatchNo = N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & "'"
                '    Dim dt As DataTable = GetDataTable(strSQL, trans)
                '    If dt.Rows.Count = 0 Then
                '        If msg_Confirm("The batch No you entered is new. " & Environment.NewLine & "Do you want to add new batch no...?") Then

                '            objCommand.CommandText = "insert into PurchaseBatchTable(BatchNo,StartDate, EndDate, Comments,sortorder) values(N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Trim.Replace("'", "''") & "', GetDate(), NULL , '',0" & ") Select @@Identity"

                '            BatchID = objCommand.ExecuteScalar()



                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty) values( " _
                '            & " " & txtReceivingNoteID.Text & "," & Val(grd.Rows(i).Cells("ItemId").Value) & ",N'" & (grd.Rows(i).Cells("Unit").Value) & "'," & Val(grd.Rows(i).Cells("Qty").Value) & ", " _
                '            & " " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", Val(grd.Rows(i).Cells("qty").Value), (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value))) & ", " & Val(grd.Rows(i).Cells("rate").Value) & ", " & Val(grd.Rows(i).Cells("PackQty").Value) & " , " & Val(grd.Rows(i).Cells("CurrentPrice").Value) & ",N'" & grd.Rows(i).Cells("BatchNo").Value & " '," & BatchID & " ,N'" & grd.Rows(i).Cells("LocationId").Value & " ', " & grd.Rows(i).Cells("ReceivedQty").Value & ", " & grd.Rows(i).Cells("RejectedQty").Value & ") "


                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate, Comments, Transportation_Charges, PackPrice) values( " _
                '           & " " & txtReceivingNoteID.Text & "," & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '           & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & BatchID & " ,N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ",N'" & CDate(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value).ToString("yyyy-M-d h:mm:ss tt") & "', N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value) & ") "

                'Changing against request no 828 by imran ali 18-9-2013 add column of discount price
                ''Before against request no. 934
                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate, Comments, Transportation_Charges, PackPrice,Discount_Price) values( " _
                '           & " " & txtReceivingNoteID.Text & "," & Val(grd.GetRows(i).Cells("ItemId").Value.ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '           & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & BatchID & " ,N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ",N'" & CDate(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value).ToString("yyyy-M-d h:mm:ss tt") & "', N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ") "
                'ReqId-934 Resolve Comma Error

                ''TASK TFS1378
                'Dim strData As String = "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0)+" & (Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)) & "  as BalanceQty, SUM((IsNull(Rate,0)*IsNull(InQty,0))-(IsNull(Rate,0)*IsNull(OutQty,0))) + " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) * (Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)) & " BalanceAmount From StockDetailTable WHERE ArticleDefID=" & Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & " AND IsNull(Rate,0) <> 0 Group By ArticleDefId "
                Dim strData As String '= "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0)+" & (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value)))) & "  as BalanceQty, SUM((IsNull(InAmount,0))-(IsNull(OutAmount,0))) + " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) * (IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value)))) & " BalanceAmount From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefID=" & Val(Me.grd.GetRows(i).Cells("ItemId").Value.ToString) & " AND StockMasterTable.DocNo <> '" & Me.txtPONo.Text & "' AND IsNull(Rate,0) <> 0 Group By ArticleDefId "
                Dim str As String = "Select * from stockmastertable where docno = '" & Me.txtPONo.Text & "'"
                Dim dtdocdata As DataTable = GetDataTable(str)
                If GRNStockImpact = True AndAlso dtdocdata.Rows.Count > 0 Then
                    strData = "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0)+" & (Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)) & "  as BalanceQty, SUM((IsNull(InAmount,0))-(IsNull(OutAmount,0))) + " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) + Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) * (Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * (Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString))) & " as BalanceAmount From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefID=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & " AND StockMasterTable.DocNo <> '" & Me.txtPONo.Text & "' AND IsNull(Rate,0) <> 0  AND StockMasterTable.DocDate <= '" & Me.dtpPODate.Value & "' Group By ArticleDefId "
                Else
                    'rafay Error show hua tha adeeb ko obect reference is not set 
                    strData = "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0)+" & (Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)) & "  as BalanceQty, SUM((IsNull(InAmount,0))-(IsNull(OutAmount,0))) + " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) + Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) * (Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * (Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString))) & " as BalanceAmount From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefID=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & "  AND StockMasterTable.DocDate <= '" & Me.dtpPODate.Value & "' Group By ArticleDefId "
                End If
                'Dim strData As String = "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0)+" & (Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)) & " as BalanceQty, SUM((IsNull(InAmount,0))-(IsNull(OutAmount,0))) + " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) * (Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)) * (Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString)) & " as BalanceAmount, IsNull(SUM(IsNull(InQty, 0) - IsNull(OutQty, 0)), 0) AS CheckStock From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefID=" & Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & " AND StockMasterTable.DocDate > '" & ClosingDate & "' AND StockMasterTable.DocNo <> '" & Me.txtPONo.Text & "' AND IsNull(Rate,0) <> 0 Group By ArticleDefId "
                If GRNStockImpact = False Then
                    strData = "Select ArticleDefID, IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0) as BalanceQty, SUM((IsNull(InAmount,0))-(IsNull(OutAmount,0))) as BalanceAmount, IsNull(SUM(IsNull(InQty, 0) - IsNull(OutQty, 0)), 0) AS CheckStock From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefID=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & " AND IsNull(Rate,0) <> 0 Group By ArticleDefId "
                End If
                Dim dblCostPrice As Double = 0D
                Dim dtLastestPriceData As New DataTable
                dtLastestPriceData = GetDataTable(strData, trans)
                dtLastestPriceData.AcceptChanges()
                'rafay Waqar bhai Added this condition because cost price value should be nan 
                If dtLastestPriceData.Rows.Count > 0 Then
                    If Val(dtLastestPriceData.Rows(0).Item(1).ToString) > 0 Then
                        If flgAvgRate = True Then
                            dblCostPrice = (Val(dtLastestPriceData.Rows(0).Item(2).ToString) / Val(dtLastestPriceData.Rows(0).Item(1).ToString))
                        Else
                            dblCostPrice = (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) + Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) 'Val(grd.GetRows(i).Cells("Rate").Value.ToString)
                        End If
                        'If dblCostPrice = 0 Then
                        '    dblCostPrice = (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) 'Val(grd.GetRows(i).Cells("Rate").Value.ToString)
                        'End If
                        'Else
                        '    dblCostPrice = (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) 'Val(grd.GetRows(i).Cells("Rate").Value.ToString)
                        'End If
                    Else
                        dblCostPrice = (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) + Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) 'Val(grd.GetRows(i).Cells("Rate").Value.ToString)
                    End If
                End If
                'rafay
                Dim strserviceitem As String = "Select ServiceItem from ArticleDefView where ArticleId = " & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ""
                Dim dt2serviceitem As DataTable = GetDataTable(strserviceitem, trans)
                dt2serviceitem.AcceptChanges()
                Dim ServiceItem1 As Boolean = Val(dt2serviceitem.Rows(0).Item("ServiceItem").ToString)
                If ServiceItem1 = False Then
                    StockDetail = New StockDetail
                    StockDetail.StockTransId = 0
                    StockDetail.LocationId = grd.GetRows(i).Cells("LocationID").Value
                    StockDetail.ArticleDefId = IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString))
                    StockDetail.InQty = Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)
                    StockDetail.OutQty = 0
                    StockDetail.Rate = (Val(grd.GetRows(i).Cells("rate").Value.ToString) + Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) + Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) * Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString)
                    StockDetail.CostPrice = dblCostPrice '* Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString)
                    StockDetail.InAmount = Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * StockDetail.Rate '* Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString)
                    StockDetail.OutAmount = 0
                    StockDetail.Remarks = grd.GetRows(i).Cells("Comments").Value.ToString
                    StockDetail.PackQty = Val(grd.GetRows(i).Cells("PackQty").Value.ToString)
                    StockDetail.In_PackQty = Val(grd.GetRows(i).Cells("Qty").Value.ToString)
                    StockDetail.Out_PackQty = 0
                    StockDetail.BatchNo = grd.GetRows(i).Cells("BatchNo").Value.ToString
                    StockDetail.ExpiryDate = CType(grd.GetRows(i).Cells("ExpiryDate").Value, Date).ToString("yyyy-M-d h:mm:ss tt")
                    StockDetail.Origin = grd.GetRows(i).Cells("Origin").Value.ToString
                    StockList.Add(StockDetail)
                    '' END TASK TFS1378
                End If

                objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate, Comments, Transportation_Charges, PackPrice,Discount_Price,X_Tray_Weights,X_Gross_Weights,X_Net_Weights,Y_Gross_Weights,Y_Tray_Weights,Y_Net_Weights,PO_ID) values( " _
                '           & " " & txtReceivingID.Text & "," & Val(grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '           & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & " '," & BatchID & " ,N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ",N'" & CDate(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value).ToString("yyyy-M-d h:mm:ss tt") & "', N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PO_ID").Value.ToString) & ") "


                'TASK-TFS-51 Added Fields AdTax_Percent, AdTax_Amount
                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate, Comments, Transportation_Charges, PackPrice,Discount_Price,X_Tray_Weights,X_Gross_Weights,X_Net_Weights,Y_Gross_Weights,Y_Tray_Weights,Y_Net_Weights,PO_ID,AdTax_Percent,AdTax_Amount) values( " _
                '         & " " & txtReceivingID.Text & "," & Val(grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '         & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & " '," & BatchID & " ,N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ",N'" & CDate(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value).ToString("yyyy-M-d h:mm:ss tt") & "', N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PO_ID").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & ") "
                'END TASK-TFS-51

                objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate, Comments, Transportation_Charges, PackPrice,Discount_Price,X_Tray_Weights,X_Gross_Weights,X_Net_Weights,Y_Gross_Weights,Y_Tray_Weights,Y_Net_Weights,PO_ID,AdTax_Percent,AdTax_Amount,PurchaseOrderDetailId,Gross_Quantity,Vendor_Total_Quantity,Column1,Column2,Column3, OtherDeduction, IsStockImpacted, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, Origin, Custom_Charges, AlternativeItem, AlternativeItemId) values( " _
                        & " " & txtReceivingID.Text & "," & Val(grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                        & " " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & " '," & BatchID & " ,N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & "," & IIf(grd.GetRows(i).Cells("ExpiryDate").Value.ToString = "", "NULL", "Convert(DateTime,N'" & CDate(IIf(Me.grd.GetRows(i).Cells("ExpiryDate").Value.ToString = "", Date.Now, Me.grd.GetRows(i).Cells("ExpiryDate").Value)).ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PO_ID").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & "," & Val(grd.GetRows(i).Cells("PurchaseOrderDetailId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Gross_Qty").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Vendor_Qty").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Column1").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Column2").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Column3").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Deduction").Value.ToString) & ", " & IIf(GRNStockImpact = True, 1, 0) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ",N'" & grd.GetRows(i).Cells("Origin").Value.ToString.Replace("'", "''") & " ', " & Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) & ",N'" & (grd.GetRows(i).Cells("AlternativeItem").Value) & "'," & Val(grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) & ") Select @@Identity" '' TASK-408 added TotalQty value here
                '& " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & " '," & BatchID & " ,N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & ",N'" & CDate(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value).ToString("yyyy-M-d h:mm:ss tt") & "', N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("X_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Gross_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Tray_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("Y_Net_Weights").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("PO_ID").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Percent").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells("AdTax_Amount").Value.ToString) & "," & Val(grd.GetRows(i).Cells("PurchaseOrderDetailId").Value.ToString) & ") Select @@Identity"

                Dim intReceivingNoteId As Integer = objCommand.ExecuteScalar()

                objCommand.CommandText = ""
                objCommand.CommandText = "Update ReceivingDetailTable Set ReceivingNoteDetailId=" & intReceivingNoteId & " WHERE ReceivingNoteDetailId=" & Val(Me.grd.GetRows(i).Cells("ReceivingNoteDetailId").Value.ToString) & ""
                objCommand.ExecuteNonQuery()


                '        Else
                'strSQL = "select BatchID from PurchaseBatchTable where BatchNo = N'" & grd.GetRows(i).Cells("BatchNo").Value & "'"
                'dt = GetDataTable(strSQL, trans)
                'Dim Batch_Id As Integer = 0I
                'If dt IsNot Nothing Then
                '    If dt.Rows.Count > 0 Then
                '        Batch_Id = dt.Rows(0).Item(0)
                '    End If
                'End If

                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty) values( " _
                '          & " " & txtReceivingNoteID.Text & "," & Val(grd.Rows(i).Cells("ItemId").Value) & ",N'" & (grd.Rows(i).Cells("Unit").Value) & "'," & Val(grd.Rows(i).Cells("Qty").Value) & ", " _
                '          & " " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", Val(grd.Rows(i).Cells("qty").Value), (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value))) & ", " & Val(grd.Rows(i).Cells("rate").Value) & ", " & Val(grd.Rows(i).Cells("PackQty").Value) & " , " & Val(grd.Rows(i).Cells("CurrentPrice").Value) & ",N'" & grd.Rows(i).Cells("BatchNo").Value & " '," & dt.Rows(0).Item(0).ToString & " ,N'" & grd.Rows(i).Cells("LocationId").Value & " ', " & grd.Rows(i).Cells("ReceivedQty").Value & ", " & grd.Rows(i).Cells("RejectedQty").Value & ") "

                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate, Comments, Transportation_Charges,PackPrice) values( " _
                '          & " " & txtReceivingNoteID.Text & "," & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '          & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & Batch_Id & " ,N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ,N'" & CDate(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value).ToString("yyyy-M-d h:mm:ss tt") & "',N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ") "

                'changing against request no 828 by imran ali 18-09-2013 add column of Discount_Price
                'Before against request no. 934
                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate, Comments, Transportation_Charges,PackPrice, Discount_Price) values( " _
                '                                     & " " & txtReceivingNoteID.Text & "," & Val(grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '                                     & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & Batch_Id & " ,N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ," & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ",N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ") "
                'ReqId-934 Resolve Comma Error
                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate, Comments, Transportation_Charges,PackPrice, Discount_Price) values( " _
                '                                     & " " & txtReceivingNoteID.Text & "," & Val(grd.GetRows(i).Cells("ArticleDefId").Value.ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '                                     & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & " '," & Batch_Id & " ,N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ," & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ",N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ") "


                '        End If
                '    Else
                'strSQL = "select BatchID from PurchaseBatchTable where BatchNo = N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & "'"
                'dt = GetDataTable(strSQL, trans)
                'Dim Batch_Id As Integer = 0I
                'If dt IsNot Nothing Then
                '    If dt.Rows.Count > 0 Then
                '        Batch_Id = dt.Rows(0).Item(0)
                '    End If
                'End If
                ''objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty) values( " _
                ''              & " " & txtReceivingNoteID.Text & "," & Val(grd.Rows(i).Cells("ItemId").Value) & ",N'" & (grd.Rows(i).Cells("Unit").Value) & "'," & Val(grd.Rows(i).Cells("Qty").Value) & ", " _
                ''              & " " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", Val(grd.Rows(i).Cells("qty").Value), (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value))) & ", " & Val(grd.Rows(i).Cells("rate").Value) & ", " & Val(grd.Rows(i).Cells("PackQty").Value) & " , " & Val(grd.Rows(i).Cells("CurrentPrice").Value) & ",N'" & grd.Rows(i).Cells("BatchNo").Value & " '," & dt.Rows(0).Item(0).ToString & ",N'" & grd.Rows(i).Cells("LocationId").Value & " ', " & grd.Rows(i).Cells("ReceivedQty").Value & ", " & grd.Rows(i).Cells("RejectedQty").Value & " ) "

                ''objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate, Comments, Transportation_Charges,PackPrice) values( " _
                ''              & " " & txtReceivingNoteID.Text & "," & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                ''              & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & Batch_Id & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ,N'" & CDate(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value).ToString("yyyy-M-d h:mm:ss tt") & "', N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "'," & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ") "

                ''Changing Against Request No 828 by imran ali add column of discount price
                ''Before against request no . 934
                ''objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate, Comments, Transportation_Charges,PackPrice, Discount_Price, Pack_Desc) values( " _
                ''              & " " & txtReceivingNoteID.Text & "," & Val(grd.GetRows(i).Cells("ItemId").Value.ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                ''              & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & Batch_Id & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ," & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "'," & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                ' ''ReqId-934 Resolve Comma Error
                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate, Comments, Transportation_Charges,PackPrice, Discount_Price, Pack_Desc) values( " _
                '              & " " & txtReceivingNoteID.Text & "," & Val(grd.GetRows(i).Cells("ItemId").Value.ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '              & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & " '," & Batch_Id & ",N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & ", " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ," & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "'," & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "

                '    End If

                'Else
                'strSQL = "select BatchID from PurchaseBatchTable where BatchNo = N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString & "'"
                'dt = GetDataTable(strSQL, trans)
                'Dim Batch_Id As Integer = 0I
                'If dt IsNot Nothing Then
                '    If dt.Rows.Count > 0 Then
                '        Batch_Id = dt.Rows(0).Item(0)
                '    End If
                'End If

                ''objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty) values( " _
                ''                   & " " & txtReceivingNoteID.Text & "," & Val(grd.Rows(i).Cells("ItemId").Value) & ",N'" & (grd.Rows(i).Cells("Unit").Value) & "'," & Val(grd.Rows(i).Cells("Qty").Value) & ", " _
                ''                   & " " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", Val(grd.Rows(i).Cells("qty").Value), (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value))) & ", " & Val(grd.Rows(i).Cells("rate").Value) & ", " & Val(grd.Rows(i).Cells("PackQty").Value) & " , " & Val(grd.Rows(i).Cells("CurrentPrice").Value) & ",N'" & grd.Rows(i).Cells("BatchNo").Value & " '," & dt.Rows(0).Item(0).ToString & " ,N'" & grd.Rows(i).Cells("LocationId").Value & " ', " & grd.Rows(i).Cells("ReceivedQty").Value & ", " & grd.Rows(i).Cells("RejectedQty").Value & "  ) "

                ''objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate,Comments, Transportation_Charges, PackPrice) values( " _
                ''                                       & " " & txtReceivingNoteID.Text & "," & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                ''                                       & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & Batch_Id & " ,N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & " , " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ,N'" & CDate(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value).ToString("yyyy-M-d h:mm:ss tt") & "',N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ") "

                ''Changing against request no 828 by imran ali add column of discount price
                ''Before against ReqId-934
                ''objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate,Comments, Transportation_Charges, PackPrice, Discount_Price, Pack_Desc) values( " _
                ''                                       & " " & txtReceivingNoteID.Text & "," & Val(grd.GetRows(i).Cells("ItemId").Value.ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                ''                                       & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & " '," & Batch_Id & " ,N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & " , " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ," & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ",N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "
                ''ReqId-934 Resolve Comma Error
                'objCommand.CommandText = "Insert into ReceivingNoteDetailTable (ReceivingNoteId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice,BatchNo, BatchID, LocationId, ReceivedQty, RejectedQty, TaxPercent,ExpiryDate,Comments, Transportation_Charges, PackPrice, Discount_Price, Pack_Desc) values( " _
                '                                       & " " & txtReceivingNoteID.Text & "," & Val(grd.GetRows(i).Cells("ItemId").Value.ToString) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & ", " _
                '                                       & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value.ToString), (Val(grd.GetRows(i).Cells("Qty").Value.ToString) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & " '," & Batch_Id & " ,N'" & grd.GetRows(i).Cells("LocationId").Value & " ', " & grd.GetRows(i).Cells("ReceivedQty").Value & ", " & grd.GetRows(i).Cells("RejectedQty").Value & " , " & Val(grd.GetRows(i).Cells("TaxPercent").Value.ToString) & " ," & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value Is DBNull.Value, Now.ToString("yyyy-M-d h:mm:ss tt"), grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ",N'" & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "') "

                'End If

                'objCommand.ExecuteNonQuery()
                ''Val(grd.Rows(i).Cells(5).Value)

                'update date Purchase Order Detail table
                'objCommand.CommandText = ""
                'objCommand.CommandText = "UPDATE    PurchaseOrderDetailTable" _
                '                        & " SET              DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & "" _
                '                        & " WHERE  PurchaseOrderDetailTable.Qty <> 0  AND (PurchaseOrderId = " & IIf(FlgPO = False, IIf(Me.cmbPo.SelectedIndex = -1, 0, Me.cmbPo.SelectedValue), Val(grd.GetRows(i).Cells("PO_ID").Value.ToString)) & ") " & IIf(FlgPO = False, "", " AND PurchaseDetailOrderId = " & Val(grd.GetRows(i).Cells("PurchaseOrderDetailId").Value.ToString) & "") & " AND (ArticleDefId = " & Val(grd.GetRows(i).Cells("ArticleDefID").Value.ToString) & ")"
                'objCommand.ExecuteNonQuery()
                If ServiceItem1 = False Then
                    If flgAvgRate = True Then

                        objCommand.CommandText = ""
                        objCommand.CommandText = "UPDATE ArticleDefTableMaster Set PurchasePrice=" & (StockDetail.Rate - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) + Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", Cost_Price=" & StockDetail.CostPrice & " WHERE ArticleId in (Select MasterId From ArticleDefTable WHERE ArticleId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ")"
                        objCommand.ExecuteNonQuery()


                        objCommand.CommandText = ""
                        objCommand.CommandText = "UPDATE ArticleDefTable Set PurchasePrice=" & (StockDetail.Rate - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) + Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", Cost_Price=" & StockDetail.CostPrice & " WHERE ArticleId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ""
                        objCommand.ExecuteNonQuery()

                        objCommand.CommandText = ""
                        objCommand.CommandText = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, N'" & Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") & "', 102))  AND a.ArticleDefId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ""
                        Dim dtRate As New DataTable
                        Dim daRate As New OleDbDataAdapter(objCommand)
                        daRate.Fill(dtRate)

                        objCommand.CommandText = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & " "
                        Dim dtSalesItem As New DataTable
                        Dim daSalesItem As New OleDbDataAdapter(objCommand)
                        daSalesItem.Fill(dtSalesItem)

                        If Not dtSalesItem Is Nothing Then
                            If Not dtRate Is Nothing Then
                                If dtRate.Rows.Count > 0 Then
                                    objCommand.CommandText = "Update IncrementReductionTable Set PurchaseNewPrice=" & (StockDetail.Rate - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) + Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, (StockDetail.Rate - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) + Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString))) & "  WHERE ArticleDefId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, N'" & Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) "
                                    objCommand.ExecuteNonQuery()
                                Else
                                    objCommand.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice) " _
                                    & " Values(N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ",  " & Val(0) & ", " & Val(0) & ", " & (StockDetail.Rate - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) + Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, (StockDetail.Rate - Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) - Val(grd.GetRows(i).Cells("Custom_Charges").Value.ToString) + Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString))) & ")"
                                    objCommand.ExecuteNonQuery()
                                End If
                            End If
                        End If
                    Else
                        'Apply Current Rate 
                        objCommand.CommandText = "UPDATE ArticleDefTable Set PurchasePrice=" & Val(grd.GetRows(i).Cells("Rate").Value.ToString) & ", Cost_Price=" & StockDetail.CostPrice & " WHERE ArticleId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), grd.GetRows(i).Cells("ItemId").Value) & ""
                        objCommand.ExecuteNonQuery()

                        objCommand.CommandText = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, N'" & Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") & "', 102))  AND a.ArticleDefId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), grd.GetRows(i).Cells("ItemId").Value) & ""
                        Dim dtRate As New DataTable
                        Dim daRate As New OleDbDataAdapter(objCommand)
                        daRate.Fill(dtRate)

                        objCommand.CommandText = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & " "
                        Dim dtSalesItem As New DataTable
                        Dim daSalesItem As New OleDbDataAdapter(objCommand)
                        daSalesItem.Fill(dtSalesItem)

                        If Not dtSalesItem Is Nothing Then
                            If Not dtRate Is Nothing Then
                                If dtRate.Rows.Count > 0 Then
                                    objCommand.CommandText = "Update IncrementReductionTable Set PurchaseNewPrice=" & Val(grd.GetRows(i).Cells("Rate").Value.ToString) & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(grd.GetRows(i).Cells("Rate").Value.ToString)) & "  WHERE ArticleDefId=" & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, N'" & Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) "
                                    objCommand.ExecuteNonQuery()
                                Else
                                    objCommand.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice) " _
                                    & " Values(N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) <> 0, Val(Me.grd.GetRows(i).Cells("AlternativeItemId").Value.ToString), Val(Me.grd.GetRows(i).Cells("ArticleDefId").Value.ToString)) & ",  " & Val(0) & ", " & Val(0) & ", " & Val(grd.GetRows(i).Cells("Rate").Value.ToString) & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(grd.GetRows(i).Cells("Rate").Value.ToString)) & ")"
                                    objCommand.ExecuteNonQuery()
                                End If
                            End If
                        End If
                    End If
                End If

                ''***********************
                ''Inserting Debit Amount
                ''***********************
                ''objCommand.CommandText = ""
                ''objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                ''                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & Val(grd.Rows(i).Cells("qty").Value) * Val(grd.Rows(i).Cells("rate").Value) & ", 0, N'" & grd.Rows(i).Cells("item").Value & "(" & Val(grd.Rows(i).Cells("Qty").Value) & ")')"

                'objCommand.CommandText = ""
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, sp_refrence, CostCenterId, direction) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", "" & Val(grd.GetRows(i).Cells("qty").Value.ToString) * (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & "", "" & (Val(grd.GetRows(i).Cells("PackQty").Value) * Val(grd.GetRows(i).Cells("Qty").Value.ToString)) * (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & "") & ", 0, N'" & grd.GetRows(i).Cells("item").Value & " " & Me.grd.GetRows(i).Cells("Size").Value & "  (" & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & " X " & " " & Me.grd.GetRows(i).Cells("rate").Value - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ") " & " " & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ", " & grd.GetRows(i).Cells("ItemId").Value & ")"

                'objCommand.ExecuteNonQuery()

                ''***********************
                ''Inserting Credit Amount
                ''***********************
                ''objCommand.CommandText = ""
                ''objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                ''                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(grd.Rows(i).Cells("qty").Value) * Val(grd.Rows(i).Cells("rate").Value) & ",N'" & grd.Rows(i).Cells("item").Value & "(" & Val(grd.Rows(i).Cells("Qty").Value) & ")' )"

                'objCommand.CommandText = ""
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, sp_refrence, CostCenterId, direction) " _
                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", 0, " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", "" & Val(grd.GetRows(i).Cells("qty").Value.ToString) * (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & "", "" & (Val(grd.GetRows(i).Cells("PackQty").Value) * Val(grd.GetRows(i).Cells("Qty").Value.ToString)) * (Val(grd.GetRows(i).Cells("rate").Value.ToString) - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString)) & "") & ", N'" & grd.GetRows(i).Cells("item").Value & " " & Me.grd.GetRows(i).Cells("Size").Value & "  (" & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & " X " & " " & Me.grd.GetRows(i).Cells("rate").Value - Val(Me.grd.GetRows(i).Cells("Discount_Price").Value.ToString) & ") " & " " & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ", " & grd.GetRows(i).Cells("ItemId").Value & ")"

                'objCommand.ExecuteNonQuery()


                'If flgTransaportationChargesVoucher = True Then
                '    If Not cmbTransportationVendor.ActiveRow Is Nothing Then
                '        If Me.cmbTransportationVendor.Value > 0 Then
                '            If Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) <> 0 Then
                '                '************************************
                '                'Transportation Charges Voucher Debit
                '                '************************************
                '                objCommand.CommandText = ""
                '                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, sp_refrence, CostCenterId, direction) " _
                '                                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & AccountId & ", " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", "" & Val(grd.GetRows(i).Cells("qty").Value.ToString) * Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & "", "" & (Val(grd.GetRows(i).Cells("PackQty").Value) * Val(grd.GetRows(i).Cells("Qty").Value.ToString)) * Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & "") & ", 0,N'" & grd.GetRows(i).Cells("item").Value & " " & Me.grd.GetRows(i).Cells("Size").Value & "  (" & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & " X " & " " & Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ") " & " " & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ", " & Val(grd.GetRows(i).Cells("ItemId").Value) & ")"

                '                objCommand.ExecuteNonQuery()

                '                '************************************
                '                'Transportation Charges Voucher Credit
                '                '************************************
                '                objCommand.CommandText = ""
                '                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, sp_refrence, CostCenterId, direction) " _
                '                                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbTransportationVendor.Value & ", 0," & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", "" & Val(grd.GetRows(i).Cells("qty").Value.ToString) * Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & "", "" & (Val(grd.GetRows(i).Cells("PackQty").Value) * Val(grd.GetRows(i).Cells("Qty").Value.ToString)) * Val(grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & "") & ", N'" & grd.GetRows(i).Cells("item").Value & " " & Me.grd.GetRows(i).Cells("Size").Value & "  (" & Val(grd.GetRows(i).Cells("Qty").Value.ToString) & " X " & " " & Val(Me.grd.GetRows(i).Cells("Transportation_Charges").Value.ToString) & ") " & " " & grd.GetRows(i).Cells("Comments").Text.Replace("'", "''") & "', N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ", " & grd.GetRows(i).Cells("ItemId").Value & ")"

                '                objCommand.ExecuteNonQuery()
                '            End If
                '        End If
                '    End If
                'End If
            Next

            'If Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) > 0 Then
            '    'Inserting Tax Debit With Purchase Tax Account
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, sp_refrence, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & PurchaseTaxAccountId & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",  " & 0 & ", 'Tax Ref: " & Me.txtPONo.Text & "', N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ")"
            '    objCommand.ExecuteNonQuery()

            '    'Inserting Tax Credit With Vendor Account
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, sp_refrence, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", 'Tax Ref: " & Me.txtPONo.Text & "', N'" & Me.txtInvoiceNo.Text.Replace("'", "''") & "', " & Me.cmbProject.SelectedValue & ")"
            '    objCommand.ExecuteNonQuery()

            'End If

            'If PaymentVoucherFlg = "True" Then
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "Delete From tblVoucherDetail WHERE Voucher_Id=" & VoucherId
            '    objCommand.ExecuteNonQuery()
            'End If

            'If PaymentVoucherFlg = "True" AndAlso Val(Me.txtRecAmount.Text) <> 0 Then

            '    If Not ExistingVoucherFlg = True Then

            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '                                   & " cheque_no, cheque_date,post,Source,voucher_code,coa_detail_id)" _
            '                                   & " VALUES(" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", 1,  " & Convert.ToInt32(Me.cmbMethod.SelectedValue) & ", N'" & VoucherNo & "', N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                                   & " " & IIf(Me.txtChequeNo.Visible = True, "N'" & Me.txtChequeNo.Text.Replace("'", "''") & "'", "NULL") & ", " & IIf(Me.dtpChequeDate.Visible = True, "N'" & dtpChequeDate.Value.ToString("yyyy-M-d h:mmm:ss tt") & "'", "NULL") & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ",'frmPurchase',N'" & Me.txtPONo.Text & "', " & Me.cmbDepositAccount.SelectedValue & ")" _
            '                                   & " SELECT @@IDENTITY"

            '        'objCommand.Transaction = trans
            '        lngVoucherMasterId = objCommand.ExecuteScalar


            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, cheque_no, cheque_date) " _
            '                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.txtRecAmount.Text) & ", 0, 'Payment Ref: " & Me.txtPONo.Text & "', " & Me.cmbProject.SelectedValue & "," & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.txtChequeNo.Text.Replace("'", "''") & "'") & ", " & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.dtpChequeDate.Value.ToString("yyyy-M-d h:mmm:ss tt") & "'") & " )"

            '        'objCommand.Transaction = trans
            '        objCommand.ExecuteNonQuery()


            '        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
            '        '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtAdjustment.Text) & ", 'Adjustment Ref: " & Me.txtPONo.Text & "' )"
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, cheque_no,cheque_date) " _
            '                                             & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbDepositAccount.SelectedValue & ", " & 0 & ",  " & Val(Me.txtRecAmount.Text) & ", 'Payment Ref: " & Me.txtPONo.Text & "', " & Me.cmbProject.SelectedValue & "," & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.txtChequeNo.Text.Replace("'", "''") & "'") & ", " & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.dtpChequeDate.Value.ToString("yyyy-M-d h:mmm:ss tt") & "'") & " )"

            '        ' objCommand.Transaction = trans
            '        objCommand.ExecuteNonQuery()

            '    Else

            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "Update tblVoucher Set Voucher_Type_Id=" & Convert.ToInt32(Me.cmbMethod.SelectedValue) & ", Voucher_No=N'" & Me.txtVoucherNo.Text & "', Voucher_Date=N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Cheque_No=" & IIf(Me.txtChequeNo.Visible = True, "N'" & Me.txtChequeNo.Text.Replace("'", "''") & "'", "NULL") & ", Cheque_Date=" & IIf(Me.dtpChequeDate.Visible = True, "N'" & Me.dtpChequeDate.Value.ToString("yyyy-M-d h:mmm:ss tt") & "'", "NULL") & ", coa_detail_id=" & Me.cmbDepositAccount.SelectedValue & " WHERE Voucher_Id=" & VoucherId
            '        objCommand.ExecuteNonQuery()

            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId,cheque_no,cheque_date) " _
            '                               & " VALUES(" & VoucherId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.txtRecAmount.Text) & ", 0, 'Payment Ref: " & Me.txtPONo.Text & "', " & Me.cmbProject.SelectedValue & " ," & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.txtChequeNo.Text.Replace("'", "''") & "'") & ", " & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.dtpChequeDate.Value.ToString("yyyy-M-d h:mmm:ss tt") & "'") & ")"

            '        'objCommand.Transaction = trans
            '        objCommand.ExecuteNonQuery()


            '        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
            '        '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtAdjustment.Text) & ", 'Adjustment Ref: " & Me.txtPONo.Text & "' )"
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, cheque_no,cheque_date) " _
            '                                             & " VALUES(" & VoucherId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Me.cmbDepositAccount.SelectedValue & ", " & 0 & ",  " & Val(Me.txtRecAmount.Text) & ", 'Payment Ref: " & Me.txtPONo.Text & "', " & Me.cmbProject.SelectedValue & "," & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.txtChequeNo.Text.Replace("'", "''") & "'") & ", " & IIf(Me.txtChequeNo.Text = "", "NULL", "N'" & Me.dtpChequeDate.Value.ToString("yyyy-M-d h:mmm:ss tt") & "'") & " )"

            '        ' objCommand.Transaction = trans
            '        objCommand.ExecuteNonQuery()
            '    End If
            'End If
            'If FlgPO = False Then
            '    If Me.cmbPo.SelectedIndex > 0 Then
            '        objCommand.CommandText = "Select IsNull(Sz1,0) as Qty , isnull(DeliveredQty,0) as DeliveredQty from PurchaseOrderDetailTable where PurchaseOrderID = " & Me.cmbPo.SelectedValue & ""
            '        da.SelectCommand = objCommand
            '        Dim dt As New DataTable
            '        da.Fill(dt)
            '        dt.AcceptChanges()
            '        Dim blnEqual As Boolean = True
            '        If dt.Rows.Count > 0 Then
            '            For Each r As DataRow In dt.Rows
            '                If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
            '                    blnEqual = False
            '                    Exit For
            '                End If
            '            Next
            '        End If
            '        If blnEqual = True Then
            '            objCommand.CommandText = ""
            '            objCommand.CommandText = "Update PurchaseOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where PurchaseOrderID = " & Me.cmbPo.SelectedValue & ""
            '            objCommand.ExecuteNonQuery()
            '        Else
            '            objCommand.CommandText = ""
            '            objCommand.CommandText = "Update PurchaseOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where PurchaseOrderID = " & Me.cmbPo.SelectedValue & ""
            '            objCommand.ExecuteNonQuery()
            '        End If
            '    End If

            'Else
            '    Dim dtItem As DataTable = CType(Me.grd.DataSource, DataTable)
            '    dtItem.AcceptChanges()
            '    Dim objrows() As DataRow = dtItem.Select("PO_ID <> 0")
            '    If objrows IsNot Nothing Then
            '        For Each r As DataRow In objrows
            '            If Val(r.Item("PO_ID").ToString) > 0 Then
            '                objCommand.CommandText = "Select Sum(IsNull(Sz1,0)) as Qty , Sum(isnull(DeliveredQty,0)) as DeliveredQty from PurchaseOrderDetailTable where PurchaseOrderID = " & Val(r.Item("PO_ID").ToString) & ""
            '                da.SelectCommand = objCommand
            '                Dim dt As New DataTable
            '                da.Fill(dt)
            '                dt.AcceptChanges()
            '                Dim blnEqual As Boolean = True
            '                If dt.Rows.Count > 0 Then
            '                    For Each objrow As DataRow In dt.Rows
            '                        If objrow.Item(0) <> objrow.Item(1) AndAlso objrow.Item(0) > objrow.Item(1) Then
            '                            blnEqual = False
            '                            Exit For
            '                        End If
            '                    Next
            '                End If
            '                If blnEqual = True Then
            '                    objCommand.CommandText = ""
            '                    objCommand.CommandText = "Update PurchaseOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where PurchaseOrderID = " & Val(r.Item("PO_ID").ToString) & ""
            '                    objCommand.ExecuteNonQuery()
            '                Else
            '                    objCommand.CommandText = ""
            '                    objCommand.CommandText = "Update PurchaseOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where PurchaseOrderID = " & Val(r.Item("PO_ID").ToString) & ""
            '                    objCommand.ExecuteNonQuery()
            '                End If
            '            End If
            '        Next
            '    End If
            'End If
            'objCommand.CommandText = ""
            'objCommand.CommandText = "Delete From InwardExpenseDetailTable WHERE PurchaseId=" & Val(Me.txtReceivingNoteID.Text)
            'objCommand.ExecuteNonQuery()


            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdInwardExpDetail.GetRows
            '    If Val(r.Cells("Exp_Amount").Value.ToString) <> 0 Then
            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO InwardExpenseDetailTable(PurchaseId, AccountId, Exp_Amount) Values(" & Val(Me.txtReceivingNoteID.Text) & ", " & Val(r.Cells("AccountId").Value.ToString) & ", " & Val(r.Cells("Exp_Amount").Value.ToString) & ")"
            '        objCommand.ExecuteNonQuery()


            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
            '                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(Me.cmbVendor.Value) & ", " & 0 & ",  " & Val(r.Cells("Exp_Amount").Value.ToString) & ", N'" & r.Cells("detail_title").Value.ToString & " Ref: " & Me.txtPONo.Text & "', " & Me.cmbProject.SelectedValue & " )"

            '        ' objCommand.Transaction = trans
            '        objCommand.ExecuteNonQuery()



            '        objCommand.CommandText = ""
            '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
            '                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(r.Cells("AccountId").Value.ToString) & ",   " & Val(r.Cells("Exp_Amount").Value.ToString) & "," & 0 & ", N'" & r.Cells("detail_title").Value.ToString & " Ref: " & Me.txtPONo.Text & "', " & Me.cmbProject.SelectedValue & " )"

            '        ' objCommand.Transaction = trans
            '        objCommand.ExecuteNonQuery()
            '    End If
            'Next
            UpdatePOStatus(Val(Me.txtReceivingID.Text), trans, String.Empty)
            ''TASK TFS1378
            If GRNStockImpact = True Then
                FillModel()
                StockMaster.StockTransId = StockTransId(Me.txtPONo.Text, trans)
                Call New StockDAL().Update(StockMaster, trans)
            End If
            ''END TASK TFS1378
            trans.Commit()
            Update_Record = True

            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Purchase, Me.txtPONo.Text.Trim, True)
            SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, Me.txtPONo.Text, True)

            ''Start TFS2989
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim, Me.Name) Then
                If ValidateApprovalProcessIsInProgressAgain(Me.txtPONo.Text.Trim, Me.Name) = False Then
                    SaveApprovalLog(EnumReferenceType.GRN, Val(Me.txtReceivingID.Text), Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Good Receving Note ," & cmbVendor.Text & "", Me.Name, 0)
                End If
            End If
            ''End TFS2989

            'insertvoucher()
            'Call Update1() 'Upgrading Stock ...
            setVoucherNo = Me.txtPONo.Text
            getVoucher_Id = Me.txtReceivingID.Text
            setEditMode = True
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) + Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try
    End Function

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.BtnSave.Enabled = False Then Exit Sub
        'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
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
            If Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("Your can not change this becuase financial year is closed")
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
                        EmailSave()
                        flgSelectProject = True
                        'msg_Information(str_informSave)
                        SendBrandedSMS()
                        RefreshControls()
                        ClearDetailControls()
                        'grd.Rows.Clear()
                        'DisplayRecord() R933 Commented History Data


                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        'Do While BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop
                        '---------------------------------
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop

                        '' Add by Mohsin on 14 Sep 2017

                        ' NOTIFICATION STARTS HERE FOR SAVE

                        ' *** New Segment *** 
                        '// Adding notification

                        '// Creating new object of Notification configuration dal
                        '// Dal will be used to get users list for the notification 
                        Dim NDal1 As New NotificationConfigurationDAL
                        Dim objmod1 As New VouchersMaster
                        '// Creating new object of Gluon Notification class
                        objmod1.Notification = New AgriusNotifications

                        '// Reference document number
                        objmod1.Notification.ApplicationReference = Me.txtPONo.Text

                        '// Date of notification
                        objmod1.Notification.NotificationDate = Now

                        '// Preparing notification title string
                        objmod1.Notification.NotificationTitle = "Goods Receiving Note number [" & Me.txtPONo.Text & "]  is created."

                        '// Preparing notification description string
                        objmod1.Notification.NotificationDescription = "Goods Receiving Note number [" & Me.txtPONo.Text & "] is created by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                        '// Setting source application as refrence in the notification
                        objmod1.Notification.SourceApplication = Me.Name



                        '// Starting to get users list to add child

                        '// Creating notification detail object list
                        Dim List1 As New List(Of NotificationDetail)

                        '// Getting users list
                        List1 = NDal1.GetNotificationUsers("Goods Receiving Note Created")

                        '// Adding users list in the Notification object of current inquiry
                        objmod1.Notification.NotificationDetils.AddRange(List1)

                        '// Getting and adding user groups list in the Notification object of current inquiry
                        objmod1.Notification.NotificationDetils.AddRange(NDal1.GetNotificationGroups("Goods Receiving Note"))

                        '// Not getting role list because no role is associated at the moment
                        '// We will need this in future and we can use it later
                        '// We can consult to Update function of this class


                        '// ***This segment will be used to save notification in database table

                        '// Creating new list of objects of Gluon Notification
                        Dim NList1 As New List(Of AgriusNotifications)

                        '// Copying notification object from current sales inquiry to newly defined instance
                        '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Gluon Notification
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
                    If Not Me.grdSaved.GetRow Is Nothing Then
                        intRecvNoteId = Me.grdSaved.CurrentRow.Cells("ReceivingNoteId").Value.ToString
                    End If
                    If Not IsValidToDelete("ReceivingMasterTable", "ReceivingNoteId", intRecvNoteId.ToString) = True Then
                        msg_Error(str_ErrorDependentUpdateRecordFound)
                        Exit Sub
                    End If
                    'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update_Record() Then
                        EmailSave()
                        flgSelectProject = True
                        'msg_Information(str_informUpdate)
                        SendBrandedSMS()
                        RefreshControls()
                        ClearDetailControls()
                        'grd.Rows.Clear()
                        'DisplayRecord() R933 Commented History Data

                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        'Do While BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop
                        '---------------------------------
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop

                        '' Add by Mohsin on 14 Sep 2017

                        ' NOTIFICATION STARTS HERE FOR UPDATE

                        ' *** New Segment *** 
                        '// Adding notification

                        '// Creating new object of Notification configuration dal
                        '// Dal will be used to get users list for the notification 
                        Dim NDal1 As New NotificationConfigurationDAL
                        Dim objmod1 As New VouchersMaster
                        '// Creating new object of Gluon Notification class
                        objmod1.Notification = New AgriusNotifications

                        '// Reference document number
                        objmod1.Notification.ApplicationReference = Me.txtPONo.Text

                        '// Date of notification
                        objmod1.Notification.NotificationDate = Now

                        '// Preparing notification title string
                        objmod1.Notification.NotificationTitle = "Goods Receiving Note number [" & Me.txtPONo.Text & "]  is updated."

                        '// Preparing notification description string
                        objmod1.Notification.NotificationDescription = "Goods Receiving Note number [" & Me.txtPONo.Text & "] is updated by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                        '// Setting source application as refrence in the notification
                        objmod1.Notification.SourceApplication = Me.Name



                        '// Starting to get users list to add child

                        '// Creating notification detail object list
                        Dim List1 As New List(Of NotificationDetail)

                        '// Getting users list
                        List1 = NDal1.GetNotificationUsers("Goods Receiving Note Changed")

                        '// Adding users list in the Notification object of current inquiry
                        objmod1.Notification.NotificationDetils.AddRange(List1)

                        '// Getting and adding user groups list in the Notification object of current inquiry
                        objmod1.Notification.NotificationDetils.AddRange(NDal1.GetNotificationGroups("Goods Receiving Note"))

                        '// Not getting role list because no role is associated at the moment
                        '// We will need this in future and we can use it later
                        '// We can consult to Update function of this class


                        '// ***This segment will be used to save notification in database table

                        '// Creating new list of objects of Gluon Notification
                        Dim NList1 As New List(Of AgriusNotifications)

                        '// Copying notification object from current sales inquiry to newly defined instance
                        '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Gluon Notification
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

                End If

            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            'Dim s As String
            's = "1234-567-890"
            'MsgBox(Microsoft.VisualBasic.Right(s, InStr(1, s, "-") - 2))
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            flgSelectProject = False

            RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
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

        Try
            ''Task# A1-10-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
            If Me.grdSaved.Row < 0 Then
                Exit Sub
            Else
                EditRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        ''End Task# A1-10-06-2015
    End Sub

    Private Sub cmbPo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPo.SelectedIndexChanged
        If Me.cmbPo.SelectedIndex <= 0 Then Exit Sub
        If IsLoaded = True Then
            Exit Sub
        Else
            Me.cmbCurrency.SelectedValue = Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("CurrencyType").ToString)
            Me.txtCurrencyRate.Text = Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("CurrencyRate").ToString)
            If FlgPO = False Then
                If Me.BtnSave.Text <> "&Save" Then
                    If Val(Me.grdSaved.GetRow.Cells("PurchaseOrderID").Value.ToString) > 0 Then
                        If Me.cmbPo.SelectedIndex > 0 Then
                            If Me.cmbPo.SelectedValue <> (Me.grdSaved.GetRow.Cells("PurchaseOrderID").Value.ToString) Then
                                If msg_Confirm("You have changed PO [" & CType(Me.cmbPo.SelectedItem, DataRowView).Row.Item("PurchaseOrderNo").ToString & "], do you want to proceed. ?") = False Then
                                    RemoveHandler cmbPo.SelectedIndexChanged, AddressOf Me.cmbPo_SelectedIndexChanged
                                    Me.cmbPo.SelectedValue = (Me.grdSaved.GetRow.Cells("PurchaseOrderID").Value.ToString)
                                    AddHandler cmbPo.SelectedIndexChanged, AddressOf Me.cmbPo_SelectedIndexChanged
                                    Exit Sub
                                End If
                            Else
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If
            Me.DisplayPODetail(Me.cmbPo.SelectedValue)
            Me.txtRemarks.Text = CType(Me.cmbPo.SelectedItem, DataRowView).Item("Remarks").ToString 'GetPurchaseRemarks(Me.cmbPo.SelectedValue).ToString
            Me.txtDcNo.Text = CType(Me.cmbPo.SelectedItem, DataRowView).Item("DcNo").ToString 'GetPODcNo(Me.cmbPo.SelectedValue).ToString
            Me.cmbProject.SelectedValue = Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("CostCenterId").ToString)
            Me.cmbLC.Value = Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("LCId").ToString)

        End If


        '  Me.DisplayPODetail(Me.cmbPo.SelectedValue)
        'If Me.cmbPo.SelectedIndex > 0 Then
        '    Dim adp As New OleDbDataAdapter
        '    Dim dt As New DataTable
        '    Dim Sql As String = "SELECT     dbo.PurchaseOrderMasterTable.VendorId, dbo.vwCOADetail.detail_title FROM         dbo.PurchaseOrderMasterTable INNER JOIN                       dbo.vwCOADetail ON dbo.PurchaseOrderMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id where PurchaseOrderMasterTable.PurchaseOrderId=" & Me.cmbPo.SelectedValue
        '    adp = New OleDbDataAdapter(Sql, Con)
        '    adp.Fill(dt)

        '    If Not dt.Rows.Count > 0 Then
        '        Me.cmbVendor.Enabled = True : Me.cmbVendor.Rows(0).Activate()
        '    Else
        '        Me.cmbVendor.Value = dt.Rows(0).Item("VendorId").ToString
        '        Me.cmbVendor.Enabled = False
        '    End If
        'GetTotal()
        'Else
        'Me.cmbVendor.Enabled = True
        'Me.cmbVendor.Rows(0).Activate()
        'End If
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
                If Me.rbtVendor.Checked = True Then
                    If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
                    frmItemSearch.VendorId = Me.cmbVendor.Value
                End If
                ''Start TFS3762
                If flgAddItemForMall = True Then
                    frmItemSearch.formName = Me.Name
                End If
                If Not Me.cmbCategory.SelectedIndex = -1 Then
                    frmItemSearch.LocationComments = CType(Me.cmbCategory.SelectedItem, DataRowView).Item("comments").ToString()
                End If
                ''End TFS3762
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
    'Private Sub grd_CellEndEdit(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited

    '    With Me.grd.CurrentRow
    '        Me.grd.UpdateData()
    '        '.Cells("Qty").Value = Val(.Cells("ReceivedQty").Value) - Val(.Cells("RejectedQty").Value)
    '        'If Val(Me.grd.CurrentRow.Cells("RejectedQty").Value) > Val(Me.grd.CurrentRow.Cells("ReceivedQty").Value) Then
    '        '    ShowErrorMessage("Rejected Qty Grater Than Received Qty")
    '        '    ' e.Cancel = True
    '        '    Me.grd.CurrentRow.Cells("RejectedQty").Value = 0
    '        '    .Cells("Qty").Value = Val(.Cells("ReceivedQty").Value) - Val(.Cells("RejectedQty").Value)
    '        'End If

    '        'If Me.grd.CurrentRow.Cells("Unit").Value = "Loose" Then
    '        '    'txtPackQty.Text = 1
    '        '    .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value)
    '        'Else
    '        '    .Cells("Total").Value = (Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value) * Val(.Cells("PackQty").Value))
    '        'End If
    '        ''If Not grd.DataSource Is Nothing Then CType(grd.DataSource, DataTable).AcceptChanges()
    '        'txtDiscount_LostFocus(Nothing, Nothing)
    '    End With
    '    'GetTotal()

    'End Sub
    Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            End If
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            'ClearDetailControls() ''Commented Against TFS4705
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            'Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
            Dim strSQl As String = String.Empty

            'If GetConfigValue("WithSizeRange") = "False" Then
            '    strSQl = "SELECT Stock, BatchNo as [Batch No] FROM  dbo.vw_Batch_Stock WHERE     (NOT (Stock = 0))and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            '    Me.cmbBatchNo.LimitToList = False
            'If GetConfigValue("WithSizeRange") = "True" Then
            '    strSQl = "SELECT     ISNULL(a.Stock, 0) AS Stock, PurchaseBatchTable.BatchNo as [Batch No], PurchaseBatchTable.BatchID" _
            '            & " FROM         SizeRangeTable INNER JOIN" _
            '            & "                   PurchaseBatchTable ON SizeRangeTable.BatchID = PurchaseBatchTable.BatchID LEFT OUTER JOIN " _
            '            & "                    (SELECT     * " _
            '            & "   FROM vw_Batch_Stock " _
            '            & "                WHERE      articleID = " & Me.cmbItem.Value & ") a ON PurchaseBatchTable.BatchID = a.BatchID " _
            '            & "WHERE(SizeRangeTable.SizeID = " & IIf(Val(Me.cmbItem.ActiveRow.Cells("Size ID").Value.ToString) > 0, Me.cmbItem.ActiveRow.Cells("Size ID").Value, 0) & ") "
            'Else
            '    strSQl = "SELECT Stock, BatchNo as [Batch No],BatchID FROM  dbo.vw_Batch_Stock WHERE     (NOT (Stock = 0))and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            'End If

            'Dim dt As DataTable = GetDataTable(strSQl)
            'cmbBatchNo.DataSource = dt
            'cmbBatchNo.ValueMember = "BatchID"
            'cmbBatchNo.DisplayMember = "Batch No"
            'cmbBatchNo.DisplayLayout.Bands(0).Columns("BatchNo").Hidden = True
            'cmbBatchNo.DisplayLayout.Bands(0).Columns("BatchID").Hidden = True

            'Dim dr As DataRow = dt.NewRow
            'dr.Item(1) = ""
            'dr.Item(2) = 0
            'dr.Item(0) = 0
            'dt.Rows.Add(dr)

            'If Me.cmbBatchNo.Rows.Count > 0 Then
            '    Me.cmbBatchNo.Rows(0).Activate()
            'End If
            'Me.cmbBatchNo.Enabled = True
        Catch ex As Exception
            msg_Error(ex.Message)
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
        '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt"))) Then
        '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
        '    End If
        'End If
        If IsDateLock(Me.dtpPODate.Value) = True Then
            ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
        End If
        Dim flgAvgRate As Boolean = Convert.ToBoolean(getConfigValueByType("AvgRate").ToString)
        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If Not IsValidToDelete("ReceivingMasterTable", "ReceivingNoteId", Me.grdSaved.CurrentRow.Cells("ReceivingNoteId").Value.ToString) = True Then
            msg_Error(str_ErrorDependentRecordFound)
            Exit Sub
        End If

        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim cmd As New OleDbCommand
        cmd.Connection = Con
        cmd.CommandType = CommandType.Text
        'cmd.CommandText = "SELECT Isnull(Sz1,0) as Qty, ArticleDefID, Isnull(Price,0) as Price,IsNull(PO_ID,0) as PO_ID, IsNull(PurchaseOrderDetailId,0) as PurchaseOrderDetailId FROM ReceivingNoteDetailTable WHERE  ReceivingNoteID = " & Me.grdSaved.CurrentRow.Cells("ReceivingNoteID").Value.ToString & ""
        'Dim da As New OleDbDataAdapter(cmd)
        'Dim dtSavedItems As New DataTable
        'da.Fill(dtSavedItems)

        'Dim lngVoucherMasterId As Integer = GetVoucherId("frmPurchase", grdSaved.CurrentRow.Cells(0).Value.ToString)

        'Dim strvoucherno As String = String.Empty
        'Dim dt As DataTable = GetRecords("select voucher_no   from tblvoucher  where voucher_id = " & lngVoucherMasterId & " ")

        'If Not dt Is Nothing Then
        '    If Not dt.Rows.Count = 0 Then
        '        strvoucherno = dt.Rows(0)("voucher_no")
        '    End If
        'End If
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As OleDbTransaction
        objTrans = Con.BeginTransaction
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim cm As New OleDbCommand


            '********************************
            ' Update Purchase Price By Imran
            '********************************
            'For i As Integer = 0 To dtSavedItems.Rows.Count - 1
            '    Dim CrrStock As Double = 0D
            '    Dim StockDetail.Rate As Double = 0D
            '    If flgAvgRate = True Then

            '        Try
            '            cm.Connection = Con
            '            cm.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, dbo.ArticleDefTable.PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))) " _
            '                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
            '                                    & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId INNER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE dbo.ArticleDefTable.ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " AND DocNo <> N'" & grdSaved.CurrentRow.Cells(0).Value.ToString.Replace("'", "''") & "' " _
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
            '                        StockDetail.Rate = IIf(Val(dtCrrStock.Rows(0).Item(3).ToString) + CrrStock = 0, 0, dtCrrStock.Rows(0).Item(3).ToString / CrrStock)
            '                    End If
            '                End If
            '            End If

            '        Catch ex As Exception

            '        End Try


            '        cm.CommandText = ""
            '        cm.CommandText = "UPDATE ArticleDefTableMaster Set PurchasePrice=" & StockDetail.Rate & " WHERE ArticleId in (Select MasterId From ArticleDefTable WHERE ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ")"
            '        cm.ExecuteNonQuery()


            '        cm.CommandText = ""
            '        cm.CommandText = "UPDATE ArticleDefTable Set PurchasePrice=" & StockDetail.Rate & " WHERE ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ""
            '        cm.ExecuteNonQuery()

            '        cm.CommandText = ""
            '        cm.CommandText = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', 102))  AND a.ArticleDefId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ""
            '        Dim dtRate As New DataTable
            '        Dim daRate As New OleDbDataAdapter(cm)
            '        daRate.Fill(dtRate)

            '        cm.CommandText = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " "
            '        Dim dtSalesItem As New DataTable
            '        Dim daSalesItem As New OleDbDataAdapter(cm)
            '        daSalesItem.Fill(dtSalesItem)

            '        If Not dtSalesItem Is Nothing Then
            '            If Not dtRate Is Nothing Then
            '                If dtRate.Rows.Count > 0 Then
            '                    cm.CommandText = "Update IncrementReductionTable Set PurchaseNewPrice=" & StockDetail.Rate & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, StockDetail.Rate) & "  WHERE ArticleDefId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', 102)) "
            '                    cm.ExecuteNonQuery()
            '                Else
            '                    cm.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice) " _
            '                    & " Values(N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ",  " & Val(0) & ", " & Val(0) & ", " & StockDetail.Rate & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, StockDetail.Rate) & ")"
            '                    cm.ExecuteNonQuery()
            '                End If
            '            End If
            '        End If

            '    Else
            '        'Apply Current Rate 
            '        cm.Connection = Con
            '        cm.CommandText = "UPDATE ArticleDefTable Set PurchasePrice=" & Val(dtSavedItems.Rows(i).Item("Price").ToString) & " WHERE ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ""
            '        cm.Transaction = objTrans
            '        cm.ExecuteNonQuery()

            '        cm.Connection = Con
            '        cm.Transaction = objTrans
            '        cm.CommandText = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', 102))  AND a.ArticleDefId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ""
            '        Dim dtRate As New DataTable
            '        Dim daRate As New OleDbDataAdapter(cm)
            '        daRate.Fill(dtRate)

            '        cm.Connection = Con
            '        cm.Transaction = objTrans
            '        cm.CommandText = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " "
            '        Dim dtSalesItem As New DataTable
            '        Dim daSalesItem As New OleDbDataAdapter(cm)
            '        daSalesItem.Fill(dtSalesItem)

            '        If Not dtSalesItem Is Nothing Then
            '            If Not dtRate Is Nothing Then
            '                If dtRate.Rows.Count > 0 Then
            '                    cm.Connection = Con
            '                    cm.CommandText = "Update IncrementReductionTable Set PurchaseNewPrice=" & Val(dtSavedItems.Rows(i).Item("Price").ToString) & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(dtSavedItems.Rows(i).Item("Price").ToString)) & "  WHERE ArticleDefId=" & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', 102)) "
            '                    cm.Transaction = objTrans
            '                    cm.ExecuteNonQuery()
            '                Else
            '                    cm.Connection = Con
            '                    cm.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice) " _
            '                    & " Values(N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & Val(dtSavedItems.Rows(i).Item("ArticleDefId").ToString) & ",  " & Val(0) & ", " & Val(0) & ", " & Val(dtSavedItems.Rows(i).Item("Price").ToString) & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(dtSavedItems.Rows(i).Item("Price").ToString)) & ")"
            '                    cm.Transaction = objTrans
            '                    cm.ExecuteNonQuery()
            '                End If
            '            End If
            '        End If
            '    End If
            'Next
            UpdatePOStatus(Val(Me.grdSaved.CurrentRow.Cells("ReceivingNoteID").Value.ToString), objTrans, "Delete")


            cm.Connection = Con
            cm.CommandText = "delete from ReceivingNoteDetailTable where ReceivingNoteID=" & Me.grdSaved.CurrentRow.Cells("ReceivingNoteID").Value.ToString
            cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            cm = New OleDbCommand
            cm.Connection = Con
            cm.CommandText = "delete from ReceivingNoteMasterTable where ReceivingNoteID=" & Me.grdSaved.CurrentRow.Cells("ReceivingNoteID").Value.ToString

            cm.Transaction = objTrans
            cm.ExecuteNonQuery()


            'cm = New OleDbCommand
            'cm.Connection = Con
            'cm.CommandText = "delete from tblvoucherdetail where voucher_id=" & lngVoucherMasterId

            'cm.Transaction = objTrans
            'cm.ExecuteNonQuery()

            'cm = New OleDbCommand
            'cm.Connection = Con
            'cm.CommandText = "delete from tblvoucher where voucher_id=" & lngVoucherMasterId

            'cm.Transaction = objTrans
            'cm.ExecuteNonQuery()

            ''make reversal of saved items in sale order detail table against poid
            'If dtSavedItems.Rows.Count > 0 Then
            '    For Each r As DataRow In dtSavedItems.Rows
            '        cm.CommandText = "Update PurchaseOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & ") where PurchaseOrderID = " & IIf(FlgPO = False, Me.grdSaved.CurrentRow.Cells("PurchaseOrderID").Value, Val(r.Item("PO_ID").ToString)) & " and ArticleDefID = " & r.Item(1) & " " & IIf(Val(r.Item("PurchaseOrderDetailId").ToString) > 0, " AND PurchaseOrderDetailId=" & Val(r.Item("PurchaseOrderDetailId").ToString) & "", "") & ""
            '        cm.ExecuteNonQuery()
            '    Next
            'End If

            'If FlgPO = False Then
            '    cm.CommandText = ""
            '    cm.CommandText = "Update PurchaseOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where PurchaseOrderID = " & Me.grdSaved.CurrentRow.Cells("PurchaseOrderID").Value & ""
            '    cm.ExecuteNonQuery()
            'Else
            '    Dim intpoid As Integer = 0I
            '    For Each r As DataRow In dtSavedItems.Rows
            '        If Not Val(r.Item("PO_ID").ToString) = intpoid Then
            '            cm.CommandText = ""
            '            cm.CommandText = "Update PurchaseOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where PurchaseOrderID = " & Val(r("PO_ID").ToString) & ""
            '            cm.ExecuteNonQuery()
            '            intpoid = Val(r.Item("PO_ID").ToString)
            '        End If
            '    Next
            'End If
            ''TASK TFS2143 done on 19-01-2018. When GRN is deleled then its stock should be impacted.
            StockMaster = New StockMaster
            StockMaster.StockTransId = Convert.ToInt32(StockTransId(grdSaved.CurrentRow.Cells(0).Value, objTrans))
            Call New StockDAL().Delete(StockMaster, objTrans)

            objTrans.Commit()
            'Call Delete() 'Upgrading Stock ...
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 27-1-14 



            '' Add by Mohsin on 15 Sep 2017

            ' NOTIFICATION STARTS HERE - ADDED BY MOHSIN 14-9-2017 '

            ' *** New Segment *** 
            '// Adding notification

            '// Creating new object of Notification configuration dal
            '// Dal will be used to get users list for the notification 
            Dim NDal As New NotificationConfigurationDAL
            Dim objmod As New VouchersMaster
            '// Creating new object of Gluon Notification class
            objmod.Notification = New AgriusNotifications

            '// Reference document number
            objmod.Notification.ApplicationReference = Me.txtPONo.Text

            '// Date of notification
            objmod.Notification.NotificationDate = Now

            '// Preparing notification title string
            objmod.Notification.NotificationTitle = "Goods Receiving Note number [" & Me.txtPONo.Text & "]  is deleted."

            '// Preparing notification description string
            objmod.Notification.NotificationDescription = "Goods Receiving Note number [" & Me.txtPONo.Text & "] is deleted by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

            '// Setting source application as refrence in the notification
            objmod.Notification.SourceApplication = Me.Name



            '// Starting to get users list to add child

            '// Creating notification detail object list
            Dim List As New List(Of NotificationDetail)

            '// Getting users list
            List = NDal.GetNotificationUsers("GRN Created")

            '// Adding users list in the Notification object of current inquiry
            objmod.Notification.NotificationDetils.AddRange(List)

            '// Getting and adding user groups list in the Notification object of current inquiry
            objmod.Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("GRN"))

            '// Not getting role list because no role is associated at the moment
            '// We will need this in future and we can use it later
            '// We can consult to Update function of this class


            '// ***This segment will be used to save notification in database table

            '// Creating new list of objects of Gluon Notification
            Dim NList As New List(Of AgriusNotifications)

            '// Copying notification object from current sales inquiry to newly defined instance
            '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Gluon Notification
            NList.Add(objmod.Notification)

            '// Creating object of Notification DAL
            Dim GNotification As New NotificationDAL

            '// Saving notification to database
            GNotification.AddNotification(NList)

            '// End Adding Notification

            '// End Adding Notification
            ' *** End Segment ***

        Catch ex As Exception
            objTrans.Rollback()
            msg_Error("Error occured while deleting record: " & ex.Message)
        Finally
            Con.Close()
            Me.Cursor = Cursors.Default
        End Try
        'Voucher_Delete() 'Payment Delete Voucher 
        SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Purchase, Me.grdSaved.CurrentRow.Cells(0).Value.ToString, True)
        SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, Me.grdSaved.CurrentRow.Cells(0).Value.ToString, True)
        Me.grdSaved.CurrentRow.Delete()
        'msg_Information(str_informDelete)
        Me.txtReceivingID.Text = 0
        Me.RefreshControls()


    End Sub
    Private Sub cmbVendor_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.ValueChanged
        Try
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            Dim Str As String = String.Empty
            'If Me.IsEditMode = False Then
            '    ' Str = "Select SalesOrderID, SalesOrderNo from SalesOrderMasterTable where salesorderId not in(select PoId from salesMasterTable) and VendorID = " & Me.cmbVendor.ActiveRow.Cells(0).Value & " and Status = N'" & EnumStatus.Open.ToString & "'"
            '    Str = "Select PurchaseOrderID, PurchaseOrderNo + ' ~ ' + Convert(Varchar(12), PurchaseOrderDate, 113) as PurchaseOrderNo,DcNo,Remarks from PurchaseOrderMasterTable where  VendorID = " & Me.cmbVendor.ActiveRow.Cells(0).Value & " and Status = N'" & EnumStatus.Open.ToString & "'"
            '    FillDropDown(cmbPo, Str)
            'Else
            '    'Str = "Select SalesOrderID, SalesOrderNo from SalesOrderMasterTable where VendorID = " & Me.cmbVendor.ActiveRow.Cells(0).Value & ""
            '    Str = " Select PurchaseOrderID, PurchaseOrderNo + ' ~ ' + Convert(Varchar(12), PurchaseOrderDate, 113) as PurchaseOrderNo,DcNo,Remarks  from PurchaseOrderMasterTable where  vendorid=" & Me.cmbVendor.Value & ""
            '    FillDropDown(cmbPo, Str)
            'End If
            FillCombo("LC")
            FillCombo("SO")
            CtrlGrdBar1.Email = New SBModel.SendingEmail
            CtrlGrdBar1.Email.ToEmail = Me.cmbVendor.ActiveRow.Cells("Email").Text
            CtrlGrdBar1.Email.Subject = "Purchase: " + "(" & Me.txtPONo.Text & ")"
            CtrlGrdBar1.Email.Body = String.Empty
            CtrlGrdBar1.Email.DocumentNo = Me.txtPONo.Text
            CtrlGrdBar1.Email.DocumentDate = Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        'Me.GetTotal()
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
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
                    Me.BtnPrint.Enabled = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    'Task 1592 Apply future date rights
                    IsDateChangeAllowed = False
                    DateChange(False)
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmPurchase)
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
                    End If
                End If
                UserPostingRights = GetUserPostingRights(LoginUserId)
                If UserPostingRights = True Then
                    Me.chkPost.Visible = True
                    Me.grd.RootTable.Columns("ReceivedQty").Visible = True
                    'Me.grd.RootTable.Columns("RejectedQty").Visible = True
                    InspectionReportToolStripMenuItem.Enabled = True
                    PurchaseReturnReportToolStripMenuItem.Enabled = True
                Else
                    Me.chkPost.Visible = False
                    If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = False
                    Me.grd.RootTable.Columns("ReceivedQty").Visible = False
                    Me.grd.RootTable.Columns("RejectedQty").Visible = False
                    InspectionReportToolStripMenuItem.Enabled = False
                    PurchaseReturnReportToolStripMenuItem.Enabled = False
                End If
                UserPriceAllowedRights = GetUserPriceAllowedRights(LoginUserId)
                'If UserPriceAllowedRights = True Then
                '    Me.pnlRateHidden.Visible = True
                '    Me.grd.RootTable.Columns("Rate").Visible = True
                '    Me.grd.RootTable.Columns("Total").Visible = True
                'Else
                '    Me.pnlRateHidden.Visible = False
                '    Me.grd.RootTable.Columns("Rate").Visible = False
                '    Me.grd.RootTable.Columns("Total").Visible = False
                'End If
                GetSecurityByPostingUser(UserPostingRights, BtnSave, BtnDelete)
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'Task 1592 Apply future date rights
                IsDateChangeAllowed = False
                DateChange(False)
                'Me.pnlRateHidden.Visible = False
                Me.chkPost.Visible = False
                If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar2.mGridExport.Enabled = False ''TFS1823
                'Me.pnlRateHidden.Visible = False
                'Me.grd.RootTable.Columns("Rate").Visible = False
                'Me.grd.RootTable.Columns("Total").Visible = False
                InspectionReportToolStripMenuItem.Enabled = False
                PurchaseReturnReportToolStripMenuItem.Enabled = False
                Me.grd.RootTable.Columns("ReceivedQty").Visible = False
                Me.grd.RootTable.Columns("RejectedQty").Visible = False
                'Task:2395 Added Security Print
                PurchaseReturnReportToolStripMenuItem.Enabled = False
                InspectionReportToolStripMenuItem.Enabled = False
                InwordGatePToolStripMenuItem.Enabled = False
                RejectionReportToolStripMenuItem.Enabled = False
                InspectionReportToolStripMenuItem1.Enabled = False
                InwardGatepassToolStripMenuItem.Enabled = False
                'End Task:2395
                CtrlGrdBar1.mGridChooseFielder.Enabled = False  'Task:2406 Added Field Chooser Rights
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
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        CtrlGrdBar2.mGridExport.Enabled = True ''TFS1823
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True
                        If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True
                    ElseIf RightsDt.FormControlName = "Print Inward Gatepass" Then
                        InwordGatePToolStripMenuItem.Enabled = True
                        InwardGatepassToolStripMenuItem.Enabled = True

                        'Task:1592 Added Future Date Rights
                    ElseIf RightsDt.FormControlName = "Future Transaction" Then
                        IsDateChangeAllowed = True
                        DateChange(True)
                    ElseIf RightsDt.FormControlName = "Print Inspection Report" Then
                        InspectionReportToolStripMenuItem.Enabled = True
                        InspectionReportToolStripMenuItem1.Enabled = True

                    ElseIf RightsDt.FormControlName = "Print Rejection Report" Then
                        PurchaseReturnReportToolStripMenuItem.Enabled = True
                        RejectionReportToolStripMenuItem.Enabled = True
                        'End Task:2395
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
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
    Private Sub cmbItem_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbItem.KeyUp
        If e.KeyCode = Keys.ShiftKey Then
            If Me.cmbItem.DisplayMember = "Item" Then
                Me.cmbItem.DisplayMember = "Code"
            Else
                Me.cmbItem.DisplayMember = "Item"

            End If
        End If
    End Sub
    Private Sub ListOfItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListOfItemToolStripMenuItem.Click
        frmModProperty.ShowForm("ListOfItems")
    End Sub
    Private Sub SummaryOfPurchaesInvoicesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryOfPurchaesInvoicesToolStripMenuItem.Click
        'frmModProperty.ShowForm("SummaryOfPurchaseInvoices")
        rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfPurchaseInvoices
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub
    Private Sub PayablesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PayablesToolStripMenuItem.Click
        frmModProperty.ShowForm("rptPayables")
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        'frmModProperty.ShowForm("AddVendors")
        Dim CustId As Integer = 0
        CustId = Me.cmbVendor.Value

        ''Task 3461: Aashir: Edited Becuase Shortcut link account add option was not working 
        'FrmAddCustomers.FormType = "Vendor"
        'FrmAddCustomers.ShowDialog()
        frmModProperty.ShowForm("AddVendor")

        Me.FillCombo("Vendor")
        Me.cmbVendor.Value = CustId
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
    '    Me.Cursor = Cursors.WaitCursor
    '    DisplayRecord("All")
    '    Me.DisplayDetail(-1)
    '    Me.Cursor = Cursors.Hand
    'End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
        'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
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

        id = Me.cmbProject.SelectedValue
        FillCombo("CostCenter")
        Me.cmbProject.SelectedValue = id

        id = Me.cmbTransportationVendor.Value
        FillCombo("TransportationVendor")
        Me.cmbTransportationVendor.Value = id

        id = Me.cmbCustom.Value
        FillCombo("CustomVendor")
        Me.cmbCustom.Value = id

        FillCombo("grdLocation")
        FillCombo("GrdOrigin")
        FillCombo("SearchVendor")
        FillCombo("SearchLocation")

        If Not getConfigValueByType("CurrencyonOpenLC").ToString = "Error" Then
            flgCurrenyonOpenLC = Convert.ToBoolean(getConfigValueByType("CurrencyonOpenLC").ToString)
        End If

        If Not getConfigValueByType("TransaportationChargesVoucher").ToString = "Error" Then
            flgTransaportationChargesVoucher = getConfigValueByType("TransaportationChargesVoucher")
        End If

        If GetConfigValue("LoadMultiPO").ToString = "True" Then
            Me.btnLoad.Enabled = True
        Else
            Me.btnLoad.Enabled = False
        End If

        ''START TFS3876
        If Not getConfigValueByType("AddItemForMall").ToString = "Error" Then
            flgAddItemForMall = Convert.ToBoolean(getConfigValueByType("AddItemForMall").ToString)
        End If
        ''End TFS3876

        ''Start TFS4161
        'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
        If Not getConfigValueByType("PurchaseDiablePackQuantity").ToString = "Error" Then
            IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("PurchaseDiablePackQuantity").ToString)
        End If
        ''End TFS4161
        If flgCurrenyonOpenLC = True Then
            Me.grpCurrency.Visible = True
        Else
            Me.grpCurrency.Visible = False
        End If

        id = Me.cmbCurrency.SelectedValue
        FillCombo("Currency")
        Me.cmbCurrency.SelectedValue = id

        'id = Me.cmbCompany.SelectedIndex
        'FillCombo("Company")
        'Me.cmbCompany.SelectedIndex = id
        Me.lblProgress.Visible = False
        'DisplayDetail(Val(Me.txtReceivingID.Text))
        DisplayDetail(-1)

    End Sub
    Function GetPurchaseRemarks(ByVal POID As Integer) As String
        Try
            Dim str As String
            Dim dt As New DataTable
            Dim adp As OleDb.OleDbDataAdapter
            str = "Select Remarks From PurchaseOrderMasterTable WHERE PurchaseOrderId=" & POID & ""
            adp = New OleDb.OleDbDataAdapter(str, Con)
            adp.Fill(dt)
            Return dt.Rows(0).Item(0).ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetPODcNo(ByVal POID As Integer) As String
        Try
            Dim str As String
            Dim dt As New DataTable
            Dim adp As OleDb.OleDbDataAdapter
            str = "Select DcNo From PurchaseOrderMasterTable WHERE PurchaseOrderId=" & POID & ""
            adp = New OleDb.OleDbDataAdapter(str, Con)
            adp.Fill(dt)
            Return dt.Rows(0).Item(0).ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub InspectionReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InspectionReportToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@ReceivingNoteId", Me.grdSaved.GetRow.Cells(6).Value)
            ShowReport("goodsincpectonreportsreceived", , , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub PurchaseReturnReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseReturnReportToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@ReceivingNoteId", Me.grdSaved.GetRow.Cells(6).Value)
            ShowReport("purchasereturnnewreceived", , , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub InwordGatePToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InwordGatePToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@ReceivingNoteId", Me.grdSaved.GetRow.Cells(6).Value)
            ShowReport("inwardgatepassreceived", , , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PurchaseInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("ReceivingNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            ShowReport("PurchaseInvoice", "{ReceivingNoteMasterTable.ReceivingNoteId}=" & grdSaved.CurrentRow.Cells(6).Value, , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
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
            'Me.GetTotal()
            Me.ClearDetailControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rbtAllItem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAllItem.CheckedChanged, rbtVendor.CheckedChanged
        Try
            If Me.IsFormLoaded = True Then
                '' FillCombo("Item") Commented against TASK TFS4544
                ItemFilterByName = False
                If Me.RbtByCode.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub AllItems()
    '    Try
    '        If IsFormLoaded = True Then
    '            If Me.rbtAllItem.Checked = True Then
    '                FillCombo("Item")
    '            Else
    '                Dim Str As String = "SELECT ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleCode AS Code, ArticleDefView.ArticleDescription AS Item,  ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.PurchasePrice AS Price, ArticleDefView.SizeRangeId AS [Size ID]FROM ArticleDefView INNER JOIN  ArticleDefVendors ON ArticleDefView.MasterID = ArticleDefVendors.ArticleDefId  WHERE (ArticleDefView.Active = 1) AND (ArticleDefVendors.VendorId=" & Me.cmbVendor.Value & ")"
    '                'FillDropDown(cmbItem, str)
    '                FillUltraDropDown(Me.cmbItem, Str)
    '                Me.cmbItem.Rows(0).Activate()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Private Sub cmbVendor_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.Leave
        Try
            If Me.IsFormLoaded = True AndAlso Me.rbtVendor.Checked = True Then
                If Me.cmbVendor.IsItemInList = False Then Exit Sub
                FillCombo("Item")
            End If

            'If Me.cmbVendor.IsItemInList = True Then
            '    FillCombo("TransportationVendor")
            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RbtByName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtByName.CheckedChanged, RbtByCode.CheckedChanged
        Try
            If Me.IsFormLoaded = True Then
                ItemFilterByName = False
                'FillCombo("Item")
                If Me.RbtByCode.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        Catch ex As Exception

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


            If getConfigValueByType("WeighbridgeGRN").ToString.ToUpper = "TRUE" Then

                Me.grd.RootTable.Columns(grdEnm.X_Tray_Weights).Visible = True
                Me.grd.RootTable.Columns(grdEnm.X_Gross_Weights).Visible = True
                Me.grd.RootTable.Columns(grdEnm.X_Net_Weights).Visible = True
                Me.grd.RootTable.Columns(grdEnm.Y_Gross_Weights).Visible = True
                Me.grd.RootTable.Columns(grdEnm.Y_Tray_Weights).Visible = True
                Me.grd.RootTable.Columns(grdEnm.Y_Net_Weights).Visible = True
            Else

                Me.grd.RootTable.Columns(grdEnm.X_Tray_Weights).Visible = False
                Me.grd.RootTable.Columns(grdEnm.X_Gross_Weights).Visible = False
                Me.grd.RootTable.Columns(grdEnm.X_Net_Weights).Visible = False
                Me.grd.RootTable.Columns(grdEnm.Y_Gross_Weights).Visible = False
                Me.grd.RootTable.Columns(grdEnm.Y_Tray_Weights).Visible = False
                Me.grd.RootTable.Columns(grdEnm.Y_Net_Weights).Visible = False

            End If

            Me.grd.RootTable.Columns(grdEnm.Qty).Visible = False
            Me.grd.RootTable.Columns(grdEnm.X_Tray_Weights).Caption = "V Tray Wts"
            Me.grd.RootTable.Columns(grdEnm.X_Gross_Weights).Caption = "V Gross Wts"
            Me.grd.RootTable.Columns(grdEnm.X_Net_Weights).Caption = "V Net Wts"
            Me.grd.RootTable.Columns(grdEnm.Y_Gross_Weights).Caption = "F Gross Wts"
            Me.grd.RootTable.Columns(grdEnm.Y_Tray_Weights).Caption = "F Tray Wts"
            Me.grd.RootTable.Columns(grdEnm.Y_Net_Weights).Caption = "F Net Wts"

            Me.grd.RootTable.Columns(grdEnm.X_Tray_Weights).Caption = "V Tray Wts"
            Me.grd.RootTable.Columns(grdEnm.X_Gross_Weights).Caption = "V Gross Wts"
            Me.grd.RootTable.Columns(grdEnm.X_Net_Weights).Caption = "V Net Wts"
            Me.grd.RootTable.Columns(grdEnm.Y_Gross_Weights).Caption = "F Gross Wts"
            Me.grd.RootTable.Columns(grdEnm.Y_Tray_Weights).Caption = "F Tray Wts"
            Me.grd.RootTable.Columns(grdEnm.Y_Net_Weights).Caption = "F Net Wts"

            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If col.Index <> grdEnm.LocationId AndAlso col.Index <> grdEnm.ReceivedQty AndAlso col.Index <> grdEnm.Qty AndAlso col.Index <> grdEnm.VendorQty AndAlso col.Index <> grdEnm.Deduction AndAlso col.Index <> grdEnm.GrossQty AndAlso col.Index <> grdEnm.RejectedQty AndAlso col.Index <> grdEnm.Price AndAlso col.Index <> grdEnm.TaxPercent AndAlso col.Index <> grdEnm.ExpiryDate AndAlso col.Index <> grdEnm.Origin AndAlso col.Index <> grdEnm.BatchNo AndAlso col.Index <> grdEnm.Comments AndAlso col.Index <> grdEnm.Transportation_Charges AndAlso col.Index <> grdEnm.Custom_Charges AndAlso col.Index <> grdEnm.PackPrice AndAlso col.Index <> grdEnm.X_Tray_Weights AndAlso col.Index <> grdEnm.X_Gross_Weights AndAlso col.Index <> grdEnm.Y_Gross_Weights AndAlso col.Index <> grdEnm.Y_Tray_Weights AndAlso col.Index <> grdEnm.AdTax_Percent AndAlso col.Index <> grdEnm.CurrentPrice AndAlso col.Index <> grdEnm.RateDiscPercent AndAlso col.Index <> grdEnm.Column1 AndAlso col.Index <> grdEnm.Column2 AndAlso col.Index <> grdEnm.Column3 Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            ''Start TFS4161
            If IsPackQtyDisabled = True Then
                Me.grd.RootTable.Columns(grdEnm.TotalQty).EditType = Janus.Windows.GridEX.EditType.NoEdit
                Me.grd.RootTable.Columns(grdEnm.VendorQty).EditType = Janus.Windows.GridEX.EditType.NoEdit
                Me.grd.RootTable.Columns(grdEnm.GrossQty).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Else
                Me.grd.RootTable.Columns(grdEnm.TotalQty).EditType = Janus.Windows.GridEX.EditType.TextBox
                Me.grd.RootTable.Columns(grdEnm.VendorQty).EditType = Janus.Windows.GridEX.EditType.TextBox
                Me.grd.RootTable.Columns(grdEnm.GrossQty).EditType = Janus.Windows.GridEX.EditType.TextBox
            End If
            'Rafay : task start : add this line to edit uom field edit
            Me.grd.RootTable.Columns(grdEnm.Uom).EditType = Janus.Windows.GridEX.EditType.TextBox
            'Rafay: Task End
            ''End TFS4161
            'For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdInwardExpDetail.RootTable.Columns
            '    If col.Index <> 3 Then
            '        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            '    End If
            'Next

            Me.grd.RootTable.Columns("CurrencyRate").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grd.RootTable.Columns("CurrencyRate").FormatString = "N" & 4
            Me.grd.RootTable.Columns("CurrencyRate").FormatString = "N" & 4
            Me.grd.RootTable.Columns("CurrencyRate").TotalFormatString = "N" & 4
            Me.grd.RootTable.Columns("CurrencyAmount").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grd.RootTable.Columns("CurrencyAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("CurrencyAmount").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("CurrencyAmount").TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Rate").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Rate").TotalFormatString = "N" & TotalAmountRounding
            'Task:2759 Set rounded amount format
            Me.grd.RootTable.Columns(grdEnm.Total).FormatString = "N" & DecimalPointInValue
            'Me.grd.RootTable.Columns(grdEnm.TaxAmount).FormatString = "N" & DecimalPointInValue ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            Me.grd.RootTable.Columns(grdEnm.Total).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(grdEnm.Total).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            'Me.grd.RootTable.Columns(grdEnm.TaxAmount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            'End Task:2759
            Me.grd.RootTable.Columns("VendorNetQty").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("VendorNetQty").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("Unit").Index
            Me.grd.RootTable.Columns("Unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index
            'Me.grd.AutoSizeColumns()
            Dim bln As Boolean = Convert.ToBoolean(getConfigValueByType("GridFreezColumn").Replace("Error", "False").Replace("''", "False"))
            If bln = True Then
                Me.grd.FrozenColumns = grdEnm.Size
            Else
                Me.grd.FrozenColumns = 0
            End If
            Dim dtColSettings As DataTable = GetDataTable("Select * From InventoryColumnSettings")
            dtColSettings.AcceptChanges()
            If (dtColSettings.Rows.Count > 0 _
                AndAlso grd.DataSource IsNot Nothing) Then

                grd.RootTable.Columns(grdEnm.Column1).Caption = dtColSettings.Rows(0)("Column1").ToString()
                grd.RootTable.Columns("Column1").Visible = dtColSettings.Rows(0)("Column1Visibility")


                grd.RootTable.Columns("Column2").Caption = dtColSettings.Rows(0)("Column2").ToString()
                grd.RootTable.Columns("Column2").Visible = dtColSettings.Rows(0)("Column2Visibility")

                grd.RootTable.Columns("Column3").Caption = dtColSettings.Rows(0)("Column3").ToString()
                grd.RootTable.Columns("Column3").Visible = dtColSettings.Rows(0)("Column3Visibility")


            Else
                If (grd.DataSource IsNot Nothing) Then
                    grd.RootTable.Columns("Column1").Visible = False
                    grd.RootTable.Columns("Column2").Visible = False
                    grd.RootTable.Columns("Column3").Visible = False
                End If
            End If
            grd.RootTable.Columns("ExpiryDate").Visible = True
            Me.grd.RootTable.Columns("Origin").HasValueList = True
            Me.grd.RootTable.Columns("Origin").LimitToList = False
            Me.grd.RootTable.Columns("Origin").EditType = Janus.Windows.GridEX.EditType.Combo
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

            Dim transId As Integer = 0
            transId = Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)

            StockMaster = New StockMaster
            StockMaster.StockTransId = transId
            StockMaster.DocNo = Me.txtPONo.Text.ToString.Replace("'", "''")
            StockMaster.DocDate = Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt")
            StockMaster.DocType = 1 'Convert.ToInt32(GetStockDocTypeId("GRN").ToString)
            StockMaster.Remaks = Me.txtRemarks.Text.ToString.Replace("'", "''")
            StockMaster.Project = Me.cmbProject.SelectedValue
            StockMaster.AccountId = Me.cmbVendor.Value
            StockMaster.StockDetailList = StockList 'New List(Of StockDetail)
            Dim i As Integer = 0
            'For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            '    StockDetail = New StockDetail
            '    StockDetail.StockTransId = transId 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
            '    StockDetail.LocationId = grdRow.Cells("LocationID").Value
            '    StockDetail.ArticleDefId = grdRow.Cells("ItemId").Value
            '    StockDetail.InQty = IIf(grdRow.Cells("Unit").Value = "Loose", Val(grdRow.Cells("Qty").Value), (Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value)))
            '    StockDetail.OutQty = 0
            '    StockDetail.Rate = IIf(objRateList.Count = 0, Val(grdRow.Cells("Rate").Value.ToString), objRateList(i))
            '    StockDetail.InAmount = IIf(grdRow.Cells("Unit").Value = "Loose", (Val(grdRow.Cells("Qty").Value) * IIf(objRateList.Count = 0, Val(grdRow.Cells("Rate").Value.ToString), objRateList(i))), ((Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value)) * IIf(objRateList.Count = 0, Val(grdRow.Cells("Rate").Value.ToString), objRateList(i))))
            '    StockDetail.OutAmount = 0
            '    StockDetail.Remarks = String.Empty
            '    StockMaster.StockDetailList.Add(StockDetail)
            '    i += 1
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
    Private Function POAccess(ByVal ArticleId As Integer, ByVal PONo As String) As Double
        Try
            Dim str As String = String.Empty
            Dim dt As DataTable
            Dim POAccessFlg As Boolean
            POAccessFlg = IIf(getConfigValueByType("POAccessOnPurchase").ToString = "Error" Or getConfigValueByType("POAccessOnPurchase").ToString = "", False, getConfigValueByType("POAccessOnPurchase").ToString)
            If POAccessFlg = True Then
                str = " Select PONo, ArticleDefId, Qty, RecQty " _
                      & " From( " _
                      & " SELECT dbo.PurchaseOrderMasterTable.PurchaseOrderNo as PONo, dbo.PurchaseOrderDetailTable.ArticleDefId, SUM(ISNULL(dbo.PurchaseOrderDetailTable.Qty, 0))  " _
                      & " AS Qty, Sum(Isnull(Rec.Qty,0)) as RecQty " _
                      & " FROM dbo.PurchaseOrderMasterTable INNER JOIN " _
                      & " dbo.PurchaseOrderDetailTable ON dbo.PurchaseOrderMasterTable.PurchaseOrderId = dbo.PurchaseOrderDetailTable.PurchaseOrderId " _
                      & " LEFT OUTER JOIN " _
                      & " (SELECT dbo.ReceivingNoteMasterTable.PurchaseOrderID, dbo.ReceivingNoteDetailTable.ArticleDefId, SUM(ISNULL(dbo.ReceivingNoteDetailTable.Qty, 0))AS Qty " _
                      & " FROM dbo.ReceivingNoteMasterTable INNER JOIN dbo.ReceivingNoteDetailTable ON dbo.ReceivingNoteMasterTable.ReceivingNoteId = dbo.ReceivingNoteDetailTable.ReceivingNoteId " _
                      & " GROUP BY dbo.ReceivingNoteMasterTable.PurchaseOrderID, dbo.ReceivingNoteDetailTable.ArticleDefId) Rec ON  " _
                      & " Rec.PurchaseOrderID = PurchaseOrderMasterTable.PurchaseOrderId And Rec.ArticleDefId = PurchaseOrderDetailTable.ArticleDefId " _
                      & " GROUP BY dbo.PurchaseOrderMasterTable.PurchaseOrderNo, dbo.PurchaseOrderDetailTable.ArticleDefId  " _
                      & " ) " _
                      & " Abc " _
                      & " WHERE PONo=N'" & PONo & "' AND ArticleDefId=N'" & ArticleId & "' " _
                      & " ORDER BY 1,2 Asc "
                dt = GetDataTable(str)
                dt.Columns.Add("Balance", GetType(System.Double))
                dt.Columns("Balance").Expression = "Qty-RecQty"
                If dt.Rows.Count > 0 Then
                    If Val(Me.grd.GetRow.Cells("ReceivedQty").Value) > Val(dt.Rows(0).Item("Qty")) Then
                        If msg_Confirm("Receiving qty grater than Purchase order qty. " + vbCrLf + " Are you sure! you want to Received Qty") = True Then
                            Me.grd.GetRow.Cells("ReceivedQty").Value = Me.grd.GetRow.Cells("ReceivedQty").Value
                        Else
                            Me.grd.GetRow.Cells("ReceivedQty").Value = 0
                            Dim RowCol As New Janus.Windows.GridEX.GridEXFormatStyle
                            RowCol.BackColor = Color.AntiqueWhite
                            Me.grd.GetRows(Me.grd.GetRow.Cells("ReceivedQty").Row.RowIndex).RowStyle = RowCol
                            Me.grd.Focus()
                        End If
                    End If
                Else
                    Return 0
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub grd_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            Me.grd.UpdateData()
            GetGridDetailQtyCalculate(e)
            GetGridDetailTotal()
            If e.Column.Index = grdEnm.Price Then
                Me.grd.GetRow.Cells("CurrentPrice").Value = Val(Me.grd.GetRow.Cells("Rate").Value.ToString)
                Me.grd.GetRow.Cells("RateDiscPercent").Value = 0
            End If
            GetDiscountedPrice()
            If (e.Column.Index = grdEnm.X_Tray_Weights Or e.Column.Index = grdEnm.X_Gross_Weights Or e.Column.Index = grdEnm.Y_Gross_Weights Or e.Column.Index = grdEnm.Y_Tray_Weights) Then
                GetFinalWeights()
            End If
            With grd.CurrentRow
                POAccess(.Cells("ArticleDefId").Value, Me.cmbPo.Text)
                'txtDiscount_LostFocus(Nothing, Nothing)
            End With

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub cmbItem_RowSelected(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
    '    Try
    '        If Me.cmbItem.IsItemInList = False Then
    '            Me.txtStock.Text = 0
    '            Exit Sub
    '        Else
    '            If Me.cmbItem.Value Is Nothing Then Exit Sub
    '            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Receiving Note"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'rafay:task end
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                ' Return GetSerialNo("GRN" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "ReceivingNoteMasterTable", "ReceivingNo")
                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    Return GetSerialNo("GRN" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "ReceivingNoteMasterTable", "ReceivingNo")
                Else
                    companyinitials = "PK"
                    Return GetNextDocNo("GRN" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "ReceivingNoteMasterTable", "ReceivingNo")
                End If
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                ''Return GetNextDocNo("GRN" & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "ReceivingNoteMasterTable", "ReceivingNo")
                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    Return GetSerialNo("GRN" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "ReceivingNoteMasterTable", "ReceivingNo")
                Else
                    companyinitials = "PK"
                    Return GetNextDocNo("GRN" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "ReceivingNoteMasterTable", "ReceivingNo")
                End If
                'Rafay:Task End
            Else
                Return GetNextDocNo("GRN", 6, "ReceivingNoteMasterTable", "ReceivingNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnAddNewItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewItem.Click
        Call frmAddItem.ShowDialog()
        Call FillCombo("Item")
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
                Email.Subject = "Goods Receiving Note " & setVoucherNo & ""
                Email.Body = "Goods Receiving Note " _
                & " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Previouse_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By Softbeats ERP System"
                Email.Status = "Pending"
                Call New MailSentDAL().Add(Email)
            End If
        End If
        Return EmailSave

    End Function
    Private Sub btnPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            If getConfigValueByType("PaymentVoucherOnPurchase").ToString = "True" Then
                'If Me.SplitContainer1.Panel2Collapsed = True Then
                '    Me.SplitContainer1.Panel2Collapsed = False
                'End If
                'Me.GroupBox5.Visible = True
            Else
                'Me.GroupBox5.Visible = False
            End If
            If Me.SplitContainer1.Panel2Collapsed = True Then
                Me.SplitContainer1.Panel2Collapsed = False
            Else
                Me.SplitContainer1.Panel2Collapsed = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillPaymentMethod()

        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dr1 As DataRow
        dt.Columns.Add("Id")
        dt.Columns.Add("Name")
        dr = dt.NewRow
        dr1 = dt.NewRow

        dr(0) = Convert.ToInt32(4)
        dr(1) = "Bank"
        dt.Rows.InsertAt(dr, 0)

        dr1(0) = Convert.ToInt32(2)
        dr1(1) = "Cash"
        dt.Rows.InsertAt(dr1, 0)

        'Me.cmbMethod.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
        'Me.cmbMethod.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
        'Me.cmbMethod.DataSource = dt

    End Sub
    Private Sub VoucherDetail(ByVal PurchaseNo As String)
        Try
            Dim str As String = String.Empty
            str = "SELECT dbo.tblVoucher.voucher_id, dbo.tblVoucher.location_id, dbo.tblVoucher.voucher_code, dbo.tblVoucher.voucher_type_id, dbo.tblVoucher.voucher_no, " _
                  & " dbo.tblVoucherDetail.credit_amount, tblVoucherDetail.debit_amount, dbo.tblVoucherDetail.CostCenterID, tblVoucher.coa_detail_id, tblVoucher.Cheque_No, tblVoucher.Cheque_Date " _
                  & " FROM  dbo.tblVoucher INNER JOIN " _
                  & " dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id " _
                  & " WHERE (dbo.tblVoucher.voucher_type_id = 2 Or dbo.tblVoucher.voucher_type_id=4) AND (tblVoucherDetail.coa_detail_id=" & Me.cmbVendor.Value & ") AND (dbo.tblVoucher.voucher_code = N'" & PurchaseNo & "')"
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                '    If dt.Rows.Count > 0 Then
                '        Me.cmbMethod.SelectedValue = Convert.ToInt32(dt.Rows(0).ItemArray(3))
                '        Me.cmbDepositAccount.SelectedValue = Convert.ToInt32(dt.Rows(0).ItemArray(8))

                '        If Not IsDBNull(dt.Rows(0).ItemArray(9)) Then
                '            Me.txtChequeNo.Text = dt.Rows(0).ItemArray(9).ToString
                '        Else
                '            Me.txtChequeNo.Text = String.Empty
                '        End If

                '        If Not IsDBNull(dt.Rows(0).ItemArray(10)) Then
                '            Me.dtpChequeDate.Value = Convert.ToDateTime(dt.Rows(0).ItemArray(10))
                '        Else
                '            Me.dtpChequeDate.Value = Date.Today
                '        End If

                '        Me.txtVoucherNo.Text = dt.Rows(0).ItemArray(4)
                '        Me.txtRecAmount.Text = Convert.ToDouble(dt.Rows(0).ItemArray(6))
                '        VNo = dt.Rows(0).ItemArray(4)
                '        VoucherId = dt.Rows(0).ItemArray(0)
                '        'Me.cmbMethod.Enabled = False
                '        ExistingVoucherFlg = True
                '    Else
                '        Me.cmbMethod.SelectedIndex = 0
                '        cmbDepositAccount.SelectedIndex = 0
                '        Me.txtChequeNo.Text = String.Empty
                '        Me.dtpChequeDate.Value = Date.Today
                '        VoucherId = 0I
                '        VNo = String.Empty
                '        Me.txtVoucherNo.Text = String.Empty
                '        Me.txtRecAmount.Text = String.Empty
                '        ExistingVoucherFlg = False
                '    End If
                'Else
                '    Me.cmbMethod.SelectedIndex = 0
                '    cmbDepositAccount.SelectedIndex = 0
                '    Me.txtChequeNo.Text = String.Empty
                '    Me.dtpChequeDate.Value = Date.Today
                '    VoucherId = 0I
                '    VNo = String.Empty
                '    Me.txtVoucherNo.Text = String.Empty
                '    Me.txtRecAmount.Text = String.Empty
                ExistingVoucherFlg = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Function GetVoucherNo() As String
        Dim docNo As String = String.Empty
        Dim VType As String = String.Empty
        'If Me.cmbMethod.SelectedIndex > 0 Then
        '    VType = "BPV"
        'Else
        '    VType = "CPV"
        'End If
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
            Else
                Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
                Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
                If Not dr Is Nothing Then
                    If dr("config_Value") = "Monthly" Then
                        Return GetNextDocNo(VType & "-" & Format(Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt"), "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
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
    'Private Sub cmbMethod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim str As String = String.Empty
    '        If Me.cmbMethod Is Nothing Then Exit Sub
    '        If getConfigValueByType("PaymentVoucherOnPurchase").ToString = "True" Then
    '            str = "select coa_detail_id,detail_title from vwCoaDetail where account_type=N'" & Me.cmbMethod.Text & "'"
    '            FillDropDown(Me.cmbDepositAccount, str, True)

    '            If Me.cmbMethod.SelectedIndex = 0 Then
    '                Me.txtChequeNo.Visible = False
    '                Me.dtpChequeDate.Visible = False
    '            Else
    '                Me.txtChequeNo.Visible = True
    '                Me.dtpChequeDate.Visible = True
    '            End If
    '            Me.txtVoucherNo.Text = GetVoucherNo()
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
    '    Try
    '        If Me.SplitContainer2.Panel1Collapsed = True Then
    '            Me.SplitContainer2.Panel1Collapsed = False
    '        Else
    '            Me.SplitContainer2.Panel1Collapsed = True
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
    Public Sub Voucher_Delete()

        Dim lngVoucherId As Integer = GetVoucherId("frmPurchase", grdSaved.CurrentRow.Cells(0).Value.ToString)
        Dim cm As New OleDbCommand
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As OleDbTransaction
        objTrans = Con.BeginTransaction
        Try


            cm = New OleDbCommand
            cm.Connection = Con
            cm.CommandText = "delete from tblvoucherdetail where voucher_id=" & lngVoucherId

            cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            cm = New OleDbCommand
            cm.Connection = Con
            cm.CommandText = "delete from tblvoucher where voucher_id=" & lngVoucherId

            cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            objTrans.Commit()

        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub
    Public Function Get_All(ByVal ReceivingNo As String)
        Try
            Get_All = Nothing
            If IsFormLoaded = True Then
                If ReceivingNo.Length > 0 Then
                    Dim str As String = "Select * From ReceivingNoteMasterTable WHERE ReceivingNo=N'" & ReceivingNo & "'"
                    Dim dt As DataTable = GetDataTable(str)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then



                            IsEditMode = True
                            'FillCombo("Vendor")
                            'FillCombo("CostCenter")
                            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
                            If Not getConfigValueByType("GRNApproval") = "Error" Then
                                ApprovalProcessId = getConfigValueByType("GRNApproval")
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
                            Me.txtReceivingID.Text = dt.Rows(0).Item("ReceivingNoteId")
                            intRecvNoteId = Val(Me.txtReceivingID.Text)
                            Me.txtPONo.Text = dt.Rows(0).Item("ReceivingNo")
                            Me.dtpPODate.Value = dt.Rows(0).Item("ReceivingDate")
                            Me.cmbVendor.Value = dt.Rows(0).Item("VendorId")
                            If Me.cmbVendor.Value Is Nothing Then
                                ShowErrorMessage("Vendor is disabled.")
                                Return Nothing
                            End If
                            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks")
                            Me.txtPaid.Text = dt.Rows(0).Item("CashPaid")
                            Me.cmbPo.SelectedValue = dt.Rows(0).Item("PurchaseOrderid")
                            Me.chkPost.Checked = dt.Rows(0).Item("Post")
                            Me.txtVhNo.Text = dt.Rows(0).Item("Vehicle_No").ToString
                            Me.txtDriverName.Text = dt.Rows(0).Item("Driver_Name").ToString
                            If Not IsDBNull(dt.Rows(0).Item("Dcdate")) Then
                                Me.dtpDcDate.Value = dt.Rows(0).Item("Dcdate")
                                Me.dtpDcDate.Checked = True
                            Else
                                Me.dtpDcDate.Value = Date.Today
                                Me.dtpDcDate.Checked = False
                            End If
                            Me.txtInvoiceNo.Text = dt.Rows(0).Item("Vendor_Invoice_No").ToString
                            Me.cmbProject.SelectedValue = dt.Rows(0).Item("CostCenterId").ToString
                            Me.txtIGPNo.Text = dt.Rows(0).Item("IGPNo").ToString
                            'If Not IsDBNull(dt.Rows(0).Item("Total_Discount_Amount")) Then
                            '    Me.txtDiscount.Text = Val(dt.Rows(0).Item("Total_Discount_Amount"))
                            'Else
                            '    Me.txtDiscount.Text = String.Empty
                            'End If

                            'If IsDBNull(dt.Rows(0).Item("CurrencyType")) Then
                            '    Me.cmbCurrency.SelectedIndex = 0
                            'Else
                            '    cmbCurrency.SelectedValue = dt.Rows(0).Item("CurrencyType")
                            'End If

                            If IsDBNull(dt.Rows(0).Item("Transportation_Vendor")) Then
                                Me.cmbTransportationVendor.Rows(0).Activate()
                            Else
                                Me.cmbTransportationVendor.Value = dt.Rows(0).Item("Transportation_Vendor")
                            End If

                            If IsDBNull(dt.Rows(0).Item("Custom_Vendor")) Then
                                Me.cmbCustom.Rows(0).Activate()
                            Else
                                Me.cmbCustom.Value = dt.Rows(0).Item("Custom_Vendor")
                            End If

                            'If IsDBNull(dt.Rows(0).Item("CurrencyRate")) Then
                            '    Me.cmbCurrency.SelectedIndex = 0
                            'Else
                            '    cmbCurrency.SelectedValue = dt.Rows(0).Item("CurrencyRate")
                            'End If

                            DisplayDetail(Val(Me.txtReceivingID.Text))
                            DisplayRecord()
                            If Me.cmbPo.SelectedValue > 0 Then
                                Me.cmbVendor.Enabled = False
                            Else
                                Me.cmbVendor.Enabled = True
                            End If
                            Me.BtnSave.Text = "&Update"
                            Me.GetSecurityRights()
                            Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
                            VoucherDetail(Me.txtPONo.Text)
                            IsDrillDown = True
                            Me.cmbVendor.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)
                            Dim flag As Boolean = False
                            flag = CBool(Me.grdSaved.FindAll(Me.grdSaved.RootTable.Columns("ReceivingNo"), Janus.Windows.GridEX.ConditionOperator.Equal, ReceivingNo))
                            Dim intCountAttachedFiles As Integer = 0I
                            If Me.BtnSave.Text <> "&Save" Then
                                If Me.grdSaved.RowCount > 0 Then
                                    intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                                    Me.btnAttachment.Text = "Attachment (" & intCountAttachedFiles & ")"
                                End If
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

    Private Sub btnSearchDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDocument.Click
        Try
            If Not Me.cmbSearchLocation.Items.Count > 0 Then
                FillCombo("SearchLocation")
                If Not Me.cmbSearchLocation.SelectedIndex = -1 Then Me.cmbSearchLocation.SelectedIndex = 0
            Else
                If Not Me.cmbSearchLocation.SelectedIndex = -1 Then Me.cmbSearchLocation.SelectedIndex = 0
            End If
            If Not Me.cmbSearchAccount.IsItemInList Then
                FillCombo("SearchVendor")
                Me.cmbSearchAccount.Rows(0).Activate()
            Else
                Me.cmbSearchAccount.Rows(0).Activate()
            End If
            FillCombo("SearchCostCenter")

            If Me.SplitContainer2.Panel1Collapsed = True Then
                Me.SplitContainer2.Panel1Collapsed = False
            Else
                Me.SplitContainer2.Panel1Collapsed = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
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

    Private Sub PurchaseInvoiceToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            PurchaseInvoiceToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub InwardGatepassToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InwardGatepassToolStripMenuItem.Click
        Try
            InwordGatePToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub InspectionReportToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InspectionReportToolStripMenuItem1.Click
        Try
            InspectionReportToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RejectionReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RejectionReportToolStripMenuItem.Click
        Try
            PurchaseReturnReportToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                DisplayRecord()
            Else
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                If IsEditMode = False Then Me.BtnDelete.Visible = False
                If IsEditMode = False Then Me.BtnPrint.Visible = False
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''
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
                    If IO.File.Exists(str_ApplicationStartUpPath & "\Reports\inwardgatepassreceived.rpt") = False Then Exit Sub
                    crpt.Load(str_ApplicationStartUpPath & "\Reports\inwardgatepassreceived.rpt", DBServerName)
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

                    Dim crExportOps As New ExportOptions
                    Dim crDiskOps As New DiskFileDestinationOptions
                    Dim crExportType As New PdfRtfWordFormatOptions

                    If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments\") Then
                        IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments\")
                    Else
                    End If
                    FileName = String.Empty
                    FileName = "Goods Receiving Note" & "-" & setVoucherNo & ""
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
                    'crpt.Refresh()

                    Try
                        crpt.SetParameterValue("@ReceivingNoteId", VoucherId)
                        crpt.SetParameterValue("@CompanyName", CompanyTitle)
                        crpt.SetParameterValue("@CompanyAddress", CompanyAddHeader)
                        crpt.SetParameterValue("@ShowHeader", IsCompanyInfo)
                    Catch ex As Exception
                        'IsCompanyInfo = False
                        'CompanyTitle = String.Empty
                        'CompanyAddHeader = String.Empty
                    End Try
                    crpt.Export(crExportOps)

                    'crpt.Close()
                    'crpt.Dispose()
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

    Private Sub txtRemarks_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRemarks.Validating
        Try
            SpellChecker(Me.txtRemarks)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Receiving Note"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") Then
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
    'Private Sub txtPackQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged
    '    Try
    '        If Me.txtPackRate.Text.Length > 0 AndAlso (Me.txtPackRate.Text) > 0 Then
    '            If Me.cmbUnit.Text <> "Loose" Then
    '                Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtPackRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If Val(Me.txtPackQty.Text) = 0 Then
    '            txtPackQty.Text = 1
    '            txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
    '        Else
    '            txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtPackRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.txtPackRate.Text.Length > 0 AndAlso (Me.txtPackRate.Text) > 0 Then
    '            If Me.cmbUnit.Text <> "Loose" Then
    '                Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
    '    Try
    '        If Me.txtPackRate.Text.Length > 0 AndAlso (Me.txtPackRate.Text) > 0 Then
    '            If Me.cmbUnit.Text <> "Loose" Then
    '                Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'Private Sub txtDiscount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.grd.RowCount = 0 Then Exit Sub
    '        Dim dblTotalAmount As Double = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
    '        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
    '            r.BeginEdit()
    '            If Val(r.Cells("Qty").Value) <> 0 AndAlso Val(r.Cells("Rate").Value) <> 0 Then
    '                r.Cells("Discount_Price").Value = (Math.Round((((Val(r.Cells(grdEnm.Total).Value) / dblTotalAmount)) * Val(Me.txtDiscount.Text)) / IIf(r.Cells(grdEnm.Unit).Value.ToString = "Loose", Val(r.Cells(grdEnm.Qty).Value), (Val(r.Cells(grdEnm.Qty).Value) * Val(r.Cells(grdEnm.PackQty).Value))), 2))
    '            Else
    '                r.Cells("Discount_Price").Value = 0
    '            End If
    '            r.EndEdit()
    '        Next
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub cmbItem_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            'If Me.cmbItem.Value Is Nothing Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            FillCombo("ArticlePack")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.BtnEdit, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.F1 Then
            If Val(grd.CurrentRow.Cells("ArticleDefId").Value) > 0 Then

                ''Dim frmRelItems As New frmRelatedItems()
                'frmRelatedItems.formname = Me.Name
                'frmRelatedItems.ShowDialog()
                ''frmRelatedItems.formname = "frmReceivingNote"

                Dim frmRelItems As New frmRelatedItems(Me.Name)
                frmRelItems.ShowDialog()


            End If
            Exit Sub
        End If
        ''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
        'If e.KeyCode = Keys.Delete Then
        '    DeleteToolStripButton_Click(BtnDelete, Nothing)
        '    Exit Sub
        'End If

    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
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
            str.Add("@RejectedQuantity")
            str.Add("@DetailInformation")
            str.Add("@CompanyName")
            str.Add("@Softbeats")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSKey() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("Receiving Note")
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
#End Region
    Public Sub SendBrandedSMS()
        Try
            '...................... Send SMS .............................
            If GetSMSConfig("SMS To Location On Receiving Note").Enable = True Then
                Dim strSMSBody As String = String.Empty
                Dim objData As DataTable = CType(Me.grd.DataSource, DataTable)
                Dim dt_Loc As DataTable = objData.DefaultView.ToTable("Default", True, "LocationId")
                Dim drData() As DataRow
                For j As Integer = 0 To dt_Loc.Rows.Count - 1
                    strSMSBody = String.Empty
                    strSMSBody += "Goods Receiving Note, Doc No: " & Me.txtPONo.Text & ", Doc Date: " & Me.dtpPODate.Value.ToShortDateString & ", Supplier: " & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString & ", Invoice No: " & Me.txtInvoiceNo.Text & ", Remarks:" & Me.txtRemarks.Text & ", "
                    drData = objData.Select("LocationId=" & dt_Loc.Rows(j).Item(0).ToString & "")
                    Dim dblTotalQty As Double = 0D
                    Dim dblTotalAmount As Double = 0D
                    For Each dr As DataRow In drData
                        dblTotalQty += IIf(dr(grdEnm.Unit).ToString = "Loose", Val(dr(grdEnm.Qty).ToString), Val(dr(grdEnm.Qty).ToString) * Val(dr(grdEnm.PackQty).ToString))
                        strSMSBody += " " & dr(grdEnm.Item).ToString & ", " & IIf(dr(grdEnm.Unit).ToString = "Loose", " " & dr(grdEnm.Qty).ToString & "", "" & (Val(dr(grdEnm.Qty)).ToString * Val(dr(grdEnm.PackQty)).ToString) & "") & ", "
                    Next
                    strSMSBody += " Total Qty: " & dblTotalQty & ""
                    If Val(dt_Loc.Rows(j).Item(0).ToString) = 0 Then Exit For
                    Dim LocationPhone() As String = GetLocation(Val(dt_Loc.Rows(j).Item(0).ToString)).Mobile_No.Replace(",", ";").Replace("|", ";").Replace("\", ";").Replace("/", ";").Replace("^", ";").Replace("*", ";").Split(";")
                    'End If
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
            If GetSMSConfig("Receiving Note").Enable = True Then
                If msg_Confirm(str_ConfirmSendSMSMessage) = False Then Exit Try
                Dim strDetailMessage As String = String.Empty
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    If strDetailMessage.Length = 0 Then
                        strDetailMessage = r.Cells(grdEnm.Item).Value.ToString & ", Rejected Qty: " & IIf(r.Cells(grdEnm.Unit).Value.ToString = "Loose", Val(r.Cells(grdEnm.RejectedQty).Value.ToString), Val(r.Cells(grdEnm.RejectedQty).Value.ToString) * Val(r.Cells(grdEnm.PackQty).Value.ToString)) & ", Qty: " & IIf(r.Cells(grdEnm.Unit).Value.ToString = "Loose", Val(r.Cells(grdEnm.Qty).Value.ToString), Val(r.Cells(grdEnm.Qty).Value.ToString) * Val(r.Cells(grdEnm.PackQty).Value.ToString))
                    Else
                        strDetailMessage += "," & r.Cells(grdEnm.Item).Value.ToString & ", Rejected Qty: " & IIf(r.Cells(grdEnm.Unit).Value.ToString = "Loose", Val(r.Cells(grdEnm.RejectedQty).Value.ToString), Val(r.Cells(grdEnm.RejectedQty).Value.ToString) * Val(r.Cells(grdEnm.PackQty).Value.ToString)) & ", Qty: " & IIf(r.Cells(grdEnm.Unit).Value.ToString = "Loose", Val(r.Cells(grdEnm.Qty).Value.ToString), Val(r.Cells(grdEnm.Qty).Value.ToString) * Val(r.Cells(grdEnm.PackQty).Value.ToString))
                    End If
                Next
                Dim objTemp As New SMSTemplateParameter
                Dim obj As Object = GetSMSTemplate("Receiving Note")
                If obj IsNot Nothing Then
                    objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                    Dim strMessage As String = objTemp.SMSTemplate
                    strMessage = strMessage.Replace("@AccountTitle", Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString).Replace("@AccountCode", Me.cmbVendor.ActiveRow.Cells("Code").Value.ToString).Replace("@DocumentNo", Me.txtPONo.Text).Replace("@DocumentDate", Me.dtpPODate.Value.ToShortDateString).Replace("@OtherDoc", Me.txtInvoiceNo.Text).Replace("@Remarks", Me.txtRemarks.Text).Replace("@Amount", Me.grd.GetTotal(grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@Quantity", Me.grd.GetTotal(grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@CompanyName", CompanyTitle).Replace("@Softbeats", "Automated by www.softbeats.net").Replace("@RejectedQuantity", Me.grd.GetTotal(Me.grd.RootTable.Columns("RejectedQty"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@DetailInformation", strDetailMessage)
                    SaveSMSLog(strMessage, Me.cmbVendor.ActiveRow.Cells("Mobile").Value.ToString, "Good Receiving Note")
                    If GetSMSConfig("Receiving Note").EnabledAdmin = True Then
                        For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                            If strMob.Length > 10 Then
                                SaveSMSLog(strMessage, strMob, "Receiving Note")
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

    ''Task# A2-10-06-2015 Added Key Pres event for some textboxes to take just numeric and dot value
    Private Sub txtNUM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStock.KeyPress, txtPackQty.KeyPress, txtQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End Task# A2-10-06-2015

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
                        Dim New_Files As String = intId & "_" & DocId & "_" & Me.dtVoucherDate.Value.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
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
                frm._VoucherId = Me.grdSaved.GetRow.Cells("ReceivingNoteId").Value.ToString
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click

        Try
            If Me.cmbPo.SelectedIndex <= 0 Then Exit Sub
            Dim i As Integer = 0I
            For i = 0 To Me.grd.RowCount - 1
                If cmbPo.SelectedValue = Val(Me.grd.GetRows(i).Cells("PO_ID").Value.ToString) Then
                    msg_Error("This PO has been already loaded")
                    Exit Sub
                End If
            Next

            'Dim i As Integer = 0I
            'Dim str As String = String.Empty
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, ArticleColorDefTable.ArticleColorName AS Color,dbo.ArticleSizeDefTable.ArticleSizeName AS Size, Uom.ArticleUnitName as Uom, Recv_D.ArticleSize AS unit,  Convert(float, (Recv_D.Sz1 - Isnull(DeliveredQty , 0))) AS ReceivedQty, Convert(float, 0) as RejectedQty, Convert(float, 0) AS Qty, Recv_D.Price as Rate, IsNull(Recv_D.TaxPercent,0) As TaxPercent, 0 as TaxAmount,  " _
            '           & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, (Recv_D.Sz1 * Recv_D.Price))  - Convert(float, (isnull(DeliveredQty,0) * Recv_D.Price))  ELSE Convert(float, ((Recv_D.Sz1 * Recv_D.Price) * Article.PackQty))  - Convert(float, (isnull(DeliveredQty,0) * Recv_D.Price)) END  AS Total,  0 as Transportation_Charges, 0 as Discount_Price, " _
            '           & " Article.ArticleGroupId, Recv_D.ArticleDefId as ArticleDefId, Recv_D.Sz7 as PackQty, Recv_D.Price as CurrentPrice, 0 as PackPrice, 0 as BatchId, getDate() as ExpiryDate,  '' as BatchNo , Recv_D.Comments,Recv_D.purchaseorderid as PO_ID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc  , Isnull(Article_Group.SubSubId,0) as PurchaseAccountId  FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
            '           & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '           & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '           & " LEFT OUTER JOIN  dbo.ArticleColorDefTable ON Article.ArticleColorId = dbo.ArticleColorDefTable.ArticleColorId " _
            '           & " LEFT OUTER JOIN ArticleSizeDefTable ON Article.SizeRangeId = dbo.ArticleSizeDefTable.ArticleSizeId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId LEFT OUTER JOIN ArticleUnitDefTable Uom on Uom.ArticleUnitId = Article.ArticleUnitId " _
            '           & " Where Recv_D.PurchaseOrderID =" & cmbPo.SelectedValue & " and  Recv_D.Sz1 - Isnull(DeliveredQty , 0) > 0"
            ''
            'Dim objCommand As New OleDbCommand
            'Dim objCon As OleDbConnection
            'Dim objDataAdapter As New OleDbDataAdapter
            'Dim objDataSet As New DataSet

            'objCon = Con

            'If objCon.State = ConnectionState.Open Then objCon.Close()

            'objCon.Open()
            'objCommand.Connection = objCon
            'objCommand.CommandType = CommandType.Text


            'objCommand.CommandText = str

            'objDataAdapter.SelectCommand = objCommand
            'objDataAdapter.Fill(objDataSet)

            'objDataSet.Tables(0).Columns("Qty").Expression = "isnull(ReceivedQty,0) - isnull(RejectedQty,0)"

            'For i = 0 To objDataSet.Tables(0).Rows.Count - 1


            '    ''selecting the item
            '    Me.cmbItem.Value = objDataSet.Tables(0).Rows(i)("ArticleDefId")
            '    Me.cmbItem_Leave(Nothing, Nothing)

            '    ''selecting batch no
            '    'Me.cmbBatchNo.Text = objDataSet.Tables(0).Rows(i)("Batch No")
            '    'Me.cmbBatchNo_Leave(Nothing, Nothing)

            '    ''selecting unit
            '    Me.cmbUnit.Text = objDataSet.Tables(0).Rows(i)("Unit")
            '    Me.cmbUnit_SelectedIndexChanged(Nothing, Nothing)

            '    ''selecting qty
            '    Me.txtQty.Text = objDataSet.Tables(0).Rows(i)("Qty")

            '    'Me.txtQty_LostFocus(Nothing, Nothing)
            '    ''Me.txtQty_TextChanged(Nothing, Nothing)
            '    ' ''selecing rate
            '    'Me.txtRate.Text = objDataSet.Tables(0).Rows(i)("Rate")
            '    'If Me.cmbUnit.Text <> "Loose" Then
            '    '    txtPackQty.Text = Val(objDataSet.Tables(0).Rows(i)("Pack Qty"))
            '    '    Me.txtTotal.Text = Val(Me.txtRate.Text) * (Val(objDataSet.Tables(0).Rows(i)("Pack Qty")) * Val(Me.txtQty.Text))
            '    'Else
            '    '    Me.txtTotal.Text = Val(Me.txtRate.Text) * Val(Me.txtQty.Text)
            '    '    txtPackQty.Text = "1"
            '    'End If

            '    'TradePrice = objDataSet.Tables(0).Rows(i)("TradePrice")
            '    'SalesTax_Percentage = objDataSet.Tables(0).Rows(i)("Tax")
            '    'SchemeQty = objDataSet.Tables(0).Rows(i)("Sample Qty")
            '    'Discount_Percentage = objDataSet.Tables(0).Rows(i)("Discount_Percentage")
            '    'Freight = objDataSet.Tables(0).Rows(i)("Freight")
            '    'MarketReturns = objDataSet.Tables(0).Rows(i)("MarketReturns")
            '    'Me.txtDisc.Text = Val(Discount_Percentage)
            '    'LoadQty = objDataSet.Tables(0).Rows(i)("LoadQty")
            '    'Me.btnAdd_Click(Nothing, Nothing)

            '    'TradePrice = 0
            '    'SalesTax_Percentage = 0
            '    'SchemeQty = 0
            '    'Discount_Percentage = 0
            '    'Freight = 0
            '    'MarketReturns = 0
            '    ''  grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)(3))

            '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)
            '    Me.btnAdd_Click(Nothing, Nothing)
            'Next

            'Me.GetTotal()
            'GetAdjustment(Me.cmbPo.SelectedValue)
            DisplayPODetail(Me.cmbPo.SelectedValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Function FindExistsItem(ByVal ArticleDefID As String, ByVal PackQty As Double, ByVal PO_Id As Double, Comments As String) As Boolean
        Try
            'Task:2432 Added flg Marge Item.

            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
            Dim dr() As DataRow

            dr = dt.Select("ArticleDefId='" & ArticleDefID & "' AND PackQty=" & Val(PackQty) & " And LocationId = " & Val(cmbCategory.SelectedValue) & " AND Rate=" & Val(0) & " AND Comments='" & Comments.ToString.Replace("'", "''") & "'")

            If dr.Length > 0 Then
                For Each r As DataRow In dr
                    If dr(0).ItemArray(0) = r.ItemArray(0) AndAlso dr(0).ItemArray(1) = r.ItemArray(1) AndAlso dr(0).ItemArray(2) = r.ItemArray(2) Then
                        r.BeginEdit()
                        r("ReceivedQty") = Val(r("ReceivedQty")) + Val(Me.txtQty.Text)
                        r.EndEdit()
                    End If
                Next
                Return True
            Else
                Return False
            End If
            'End Task:2432
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub GetFinalWeights()
        Try
            Me.grd.UpdateData()
            If Val(Me.grd.GetRow.Cells("Y_Gross_Weights").Value.ToString) < Val(Me.grd.GetRow.Cells("Y_Tray_Weights").Value.ToString) Then
                ShowErrorMessage("Y Gross Weights Less Then Tray Weights.")
                Exit Sub
            End If
            Me.grd.GetRow.Cells("ReceivedQty").Value = Val(Me.grd.GetRow.Cells("Y_Net_Weights").Value.ToString)
            ''Me.grd.GetRow.Cells("Qty").Value = Val(Me.grd.GetRow.Cells("ReceivedQty").Value.ToString)
            Me.grd.GetRow.Cells("Gross_Qty").Value = (Val(Me.grd.GetRow.Cells("Qty").Value.ToString) * Val(Me.grd.GetRow.Cells("PackQty").Value))
            Me.grd.GetRow.Cells("TotalQty").Value = Val(Me.grd.GetRow.Cells("Gross_Qty").Value)
            Me.grd.GetRow.Cells("Vendor_Qty").Value = Val(Me.grd.GetRow.Cells("TotalQty").Value)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub




    Public Sub UpdatePOStatus(ReceivingNoteId As Integer, trans As OleDbTransaction, Optional Status As String = "")
        Dim objCommand As New OleDbCommand
        Try


            objCommand.Connection = trans.Connection
            objCommand.Transaction = trans
            objCommand.CommandTimeout = 300
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = ""
            ''Commented below to replace Sz1 with TotalQty to update Purchase Order against Receiving Quantity against TASK-408
            ''TASK-473 replaced Sz1 with ReceivedQty to update PO status on it dated 11-07-2016
            objCommand.CommandText = "Update PurchaseOrderDetailTable Set DeliveredQty=IsNull(DeliveredQty,0)  " & IIf(Status = "", "+", "-") & "  IsNull(GRN_D.ReceivedQty,0), ReceivedTotalQty=IsNull(PurchaseOrderDetailTable.ReceivedTotalQty,0)  " & IIf(Status = "", "+", "-") & "  IsNull(GRN_D.Qty,0) From PurchaseOrderDetailTable, ReceivingNoteDetailTable  GRN_D, ReceivingNoteMasterTable GRN WHERE GRN.ReceivingNoteID=" & ReceivingNoteId & " AND  GRN.ReceivingNoteId = GRN_D.ReceivingNoteId " ''TASK-408 added TotalQty feature
            objCommand.CommandText += " AND GRN_D.ArticleDefId = PurchaseOrderDetailTable.ArticleDefId"
            objCommand.CommandText += " AND GRN_D.PO_ID = PurchaseOrderDetailTable.PurchaseOrderId "
            objCommand.CommandText += " AND GRN_D.PurchaseOrderDetailId = PurchaseOrderDetailTable.PurchaseOrderDetailId "

            objCommand.ExecuteNonQuery()

            If FlgPO = True Then
                objCommand.CommandText = ""
                objCommand.CommandText = "Select DISTINCT IsNull(PO_ID,0) as PO_ID From ReceivingNoteDetailTable WHERE ReceivingNoteId=" & ReceivingNoteId & " AND IsNull(PO_ID,0)  <> 0"
            Else
                objCommand.CommandText = ""
                objCommand.CommandText = "Select DISTINCT IsNull(PurchaseOrderID,0) as PO_ID From ReceivingNoteMasterTable WHERE ReceivingNoteId=" & ReceivingNoteId & ""
            End If
            Dim da As New OleDbDataAdapter
            Dim dt As New DataTable
            da.SelectCommand = objCommand
            da.Fill(dt)
            dt.AcceptChanges()

            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    objCommand.CommandText = ""
                    ''Replaced Qty with Sz1 on 11-07-2016 
                    objCommand.CommandText = "Update PurchaseOrderMasterTable Set Status=Case When IsNull(PO_D.RemainingQty,0) > 0 then 'Open' ELSE 'Close' END From PurchaseOrderMasterTable, (Select PurchaseOrderId, IsNull(SUM(IsNull(Sz1,0)-IsNull(DeliveredQty,0)),0) as RemainingQty From PurchaseOrderDetailTable WHERE PurchaseOrderId=" & Val(r.Item("PO_ID").ToString) & " Group By PurchaseOrderId ) PO_D WHERE PO_D.PurchaseOrderID = PurchaseOrderMasterTable.PurchaseOrderId AND PurchaseOrderMasterTable.PurchaseOrderId=" & Val(r.Item("PO_ID").ToString) & ""
                    objCommand.ExecuteNonQuery()

                Next
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub
    'Private Sub txtTaxDeductPercent_TextChanged(sender As Object, e As EventArgs) Handles txtTaxDeductPercent.TextChanged
    '    Try
    '        If Me.grd.RowCount = 0 Then
    '            Me.txtTaxDeductAmount.Text = 0
    '        Else
    '            Dim dblTax As Double = Me.grd.GetTotal(Me.grd.RootTable.Columns(grdEnm.TaxAmount), Janus.Windows.GridEX.AggregateFunction.Sum)
    '            Dim lngTaxPercent As Long = Val(Me.txtTaxDeductPercent.Text)
    '            If lngTaxPercent > 0 Then
    '                Me.txtTaxDeductAmount.Text = (lngTaxPercent / 100) * dblTax
    '            Else
    '                Me.txtTaxDeductAmount.Text = 0
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Public Sub GetDiscountedPrice()
        Try

            Me.grd.UpdateData()
            Dim dblCurrentRate As Double = 0D
            Dim dblDiscPercent As Double = 0D
            Dim dblActualPrice As Double = 0D

            dblCurrentRate = Val(Me.grd.GetRow.Cells(grdEnm.CurrentPrice).Value.ToString)
            dblDiscPercent = Val(Me.grd.GetRow.Cells(grdEnm.RateDiscPercent).Value.ToString)
            If dblDiscPercent > 0 Then
                If dblCurrentRate > 0 Then
                    dblActualPrice = Val(dblDiscPercent / 100) * dblCurrentRate
                    Me.grd.GetRow.Cells(grdEnm.Price).Value = dblCurrentRate - dblActualPrice
                Else
                    dblActualPrice = Val(Me.grd.GetRow.Cells(grdEnm.Price).Value.ToString)
                    Me.grd.GetRow.Cells(grdEnm.Price).Value = dblActualPrice
                    Me.grd.GetRow.Cells(grdEnm.RateDiscPercent).Value = 0
                End If
            Else
                If Val(Me.grd.GetRow.Cells(grdEnm.CurrentPrice).Value.ToString) > Val(Me.grd.GetRow.Cells(grdEnm.Price).Value.ToString) Then
                    dblActualPrice = Val(Me.grd.GetRow.Cells(grdEnm.CurrentPrice).Value.ToString)
                    Me.grd.GetRow.Cells(grdEnm.Price).Value = dblActualPrice
                Else
                    dblActualPrice = Val(Me.grd.GetRow.Cells(grdEnm.Price).Value.ToString)
                    Me.grd.GetRow.Cells(grdEnm.Price).Value = dblActualPrice
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
                    Me.txtRate.Text = Val(Me.txtCurrentRate.Text) - ((Val(Me.txtDiscPercent.Text) / 100) * Val(Me.txtCurrentRate.Text))
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
                    Me.txtRate.Text = Val(Me.txtCurrentRate.Text) - (Val(txtDiscPercent.Text) / 100) * Val(Me.txtCurrentRate.Text)
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

    Private Sub grd_Error(sender As Object, e As Janus.Windows.GridEX.ErrorEventArgs) Handles grd.Error
        Try
            e.DisplayErrorMessage = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_LostFocus(sender As Object, e As EventArgs) Handles txtPackQty.LostFocus
        'If Val(Me.txtPackQty.Text) > 0 AndAlso Val(txtQty.Text) > 0 Then
        '    Me.txtGrossQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
        'Else
        '    Me.txtGrossQuantity.Text = Val(Me.txtQty.Text)
        'End If
        GetTotalQuantity()
    End Sub
    Private Sub GetTotalQuantity()
        'If Val(Me.txtPackQty.Text) > 0 AndAlso Val(txtQty.Text) > 0 Then
        '    ''Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
        '    Me.txtGrossQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
        'Else
        '    Me.txtGrossQuantity.Text = Val(Me.txtQty.Text)
        'End If
        Me.txtTotalQuantity.Text = Val(Me.txtGrossQuantity.Text) - (Val(Me.txtColumn1.Text) + Val(Me.txtColumn2.Text) + Val(Me.txtColumn3.Text))
    End Sub
    Private Sub txtPackQty_TextChanged(sender As Object, e As EventArgs) Handles txtPackQty.TextChanged
        If Val(Me.txtPackQty.Text) > 0 AndAlso Val(txtQty.Text) > 0 Then
            ''Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            Me.txtGrossQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
        Else
            Me.txtGrossQuantity.Text = Val(Me.txtQty.Text)
        End If
        GetTotalQuantity()
    End Sub

    Private Sub txtQty_LostFocus(sender As Object, e As EventArgs) Handles txtQty.LostFocus
        'If Val(Me.txtPackQty.Text) > 0 AndAlso Val(txtQty.Text) > 0 Then
        '    Me.txtGrossQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
        'Else
        '    Me.txtGrossQuantity.Text = Val(Me.txtQty.Text)
        'End If
        If Val(Me.txtPackQty.Text) > 0 AndAlso Val(txtQty.Text) > 0 Then
            ''Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            Me.txtGrossQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
        Else
            Me.txtGrossQuantity.Text = Val(Me.txtQty.Text)
        End If
        GetTotalQuantity()
    End Sub
    ''TASK TFS2235 Removed txtGrossQuantity field from this pool. done by Muhammad Ameen
    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        Try
            'If Val(Me.txtPackQty.Text) > 0 AndAlso Val(txtQty.Text) > 0 Then
            '    Me.txtGrossQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            'Else
            '    Me.txtGrossQuantity.Text = Val(Me.txtQty.Text)
            'End If
            If Val(Me.txtPackQty.Text) > 0 AndAlso Val(txtQty.Text) > 0 Then
                ''Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
                Me.txtGrossQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            Else
                Me.txtGrossQuantity.Text = Val(Me.txtQty.Text)
            End If
            GetTotalQuantity()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetGridDetailQtyCalculate(ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            Me.grd.UpdateData()
            'If (e.Column.Index = grdEnm.ReceivedQty Or e.Column.Index = grdEnm.RejectedQty Or e.Column.Index = grdEnm.Qty Or e.Column.Index = grdEnm.PackQty) Then
            '    If Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) > 1 Then
            '        Me.grd.GetRow.Cells(grdEnm.TotalQty).Value = (Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) * Val(Me.grd.GetRow.Cells(grdEnm.Qty).Value.ToString))
            '        'Me.grd.GetRow.Cells(grdEnm.LoadQty).Value = Me.grd.GetRow.Cells(grdEnm.TotalQty).Value
            '    Else
            '        Me.grd.GetRow.Cells(grdEnm.TotalQty).Value = Val(Me.grd.GetRow.Cells(grdEnm.Qty).Value.ToString)
            '        'Me.grd.GetRow.Cells(grdEnm.LoadQty).Value = Me.grd.GetRow.Cells(grdEnm.TotalQty).Value
            '    End If
            'ElseIf e.Column.Index = grdEnm.TotalQty Then
            '    If Not Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) > 2 Then
            '        Me.grd.GetRow.Cells(grdEnm.ReceivedQty).Value = (Val(Me.grd.GetRow.Cells(grdEnm.RejectedQty).Value.ToString) + Val(Me.grd.GetRow.Cells(grdEnm.TotalQty).Value.ToString))
            '        'Me.grd.GetRow.Cells(grdEnm.LoadQty).Value = Me.grd.GetRow.Cells(grdEnm.Qty).Value
            '    End If
            'ElseIf e.Column.Index = grdEnm.PackPrice Then
            '    'If getConfigValueByType("Apply40KgRate").ToString = "False" Then
            '    If Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) > 1 Then
            '        Me.grd.GetRow.Cells(grdEnm.Price).Value = (Val(Me.grd.GetRow.Cells(grdEnm.PackPrice).Value.ToString) / Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString))
            '    End If
            '    'Else
            '    '    If Val(Me.grd.GetRow.Cells(grdEnm.PackPrice).Value.ToString) > 0 Then
            '    '        Me.grd.GetRow.Cells(grdEnm.Price).Value = (Val(Me.grd.GetRow.Cells(grdEnm.PackPrice).Value.ToString) / 40)
            '    '    End If
            '    'End If
            'End If

            If (e.Column.Index = grdEnm.ReceivedQty Or e.Column.Index = grdEnm.RejectedQty Or e.Column.Index = grdEnm.Qty Or e.Column.Index = grdEnm.PackQty) Then
                Me.grd.GetRow.Cells(grdEnm.GrossQty).Value = (IIf(Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) = 0 Or Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) = 1, 1, Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString)) * Val(Me.grd.GetRow.Cells(grdEnm.Qty).Value.ToString))
                Me.grd.GetRow.Cells(grdEnm.TotalQty).Value = Val(Me.grd.GetRow.Cells(grdEnm.GrossQty).Value.ToString) - (Val(Me.grd.GetRow.Cells(grdEnm.Column1).Value.ToString) + Val(Me.grd.GetRow.Cells(grdEnm.Column2).Value.ToString) + Val(Me.grd.GetRow.Cells(grdEnm.Column3).Value.ToString))
                Me.grd.GetRow.Cells(grdEnm.VendorQty).Value = Val(Me.grd.GetRow.Cells(grdEnm.TotalQty).Value.ToString)
                'Me.grd.GetRow.Cells(grdEnm.LoadQty).Value = Me.grd.GetRow.Cells(grdEnm.TotalQty).Value
                'ElseIf e.Column.Index = grdEnm.TotalQty Then
                '    If Not Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) > 2 Then
                '        Me.grd.GetRow.Cells(grdEnm.ReceivedQty).Value = (Val(Me.grd.GetRow.Cells(grdEnm.RejectedQty).Value.ToString) + Val(Me.grd.GetRow.Cells(grdEnm.TotalQty).Value.ToString))
                '        'Me.grd.GetRow.Cells(grdEnm.LoadQty).Value = Me.grd.GetRow.Cells(grdEnm.Qty).Value
                '        Me.grd.GetRow.Cells(grdEnm.VendorQty).Value = Me.grd.GetRow.Cells(grdEnm.TotalQty).Value
                '    End If
            ElseIf e.Column.Index = grdEnm.PackPrice Then
                'If getConfigValueByType("Apply40KgRate").ToString = "False" Then
                If Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(grdEnm.Price).Value = (Val(Me.grd.GetRow.Cells(grdEnm.PackPrice).Value.ToString) / Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString))
                End If
                'Else
                '    If Val(Me.grd.GetRow.Cells(grdEnm.PackPrice).Value.ToString) > 0 Then
                '        Me.grd.GetRow.Cells(grdEnm.Price).Value = (Val(Me.grd.GetRow.Cells(grdEnm.PackPrice).Value.ToString) / 40)
                '    End If
                'End If
            ElseIf (e.Column.Index = grdEnm.GrossQty Or e.Column.Index = grdEnm.Column1 Or e.Column.Index = grdEnm.Column2 Or e.Column.Index = grdEnm.Column3) Then
                Me.grd.GetRow.Cells(grdEnm.TotalQty).Value = Val(Me.grd.GetRow.Cells(grdEnm.GrossQty).Value.ToString) - (Val(Me.grd.GetRow.Cells(grdEnm.Column1).Value.ToString) + Val(Me.grd.GetRow.Cells(grdEnm.Column2).Value.ToString) + Val(Me.grd.GetRow.Cells(grdEnm.Column3).Value.ToString))
                Me.grd.GetRow.Cells(grdEnm.VendorQty).Value = Val(Me.grd.GetRow.Cells(grdEnm.TotalQty).Value.ToString)
            ElseIf e.Column.Index = grdEnm.Deduction Then
                'If getConfigValueByType("Apply40KgRate").ToString = "False" Then
                If Val(Me.grd.GetRow.Cells(grdEnm.Deduction).Value.ToString) > Val(Me.grd.GetRow.Cells(grdEnm.VendorQty).Value.ToString) Then
                    ShowErrorMessage("You can not enter more than vendor quantity.")
                    Me.grd.GetRow.Cells(grdEnm.Deduction).Value = Me.grd.GetRow.Cells(grdEnm.VendorQty).Value
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetGridDetailTotal()
        Try

            Me.grd.UpdateData()
            Me.grd.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            If Val(grd.GetRow.Cells(grdEnm.TotalQty).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(grdEnm.Price).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(grdEnm.Total).Value.ToString) = 0 Then
                'grd.GetRow.Cells(grdEnm.Total).Value = Math.Round((Val(grd.GetRow.Cells(grdEnm.TotalQty).Value.ToString) * Val(grd.GetRow.Cells(grdEnm.Price).Value.ToString)), DecimalPointInValue)
            End If
            If Val(grd.GetRow.Cells(grdEnm.TotalQty).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(grdEnm.Total).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(grdEnm.Price).Value.ToString) = 0 Then
                'Me.txtRate.Text = Val(grd.GetRow.Cells(grdEnm.Total).Value.ToString) / Val(grd.GetRow.Cells(grdEnm.TotalQty).Value.ToString)
            End If
            If Val(grd.GetRow.Cells(grdEnm.Price).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(grdEnm.Total).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(grdEnm.TotalQty).Value.ToString) = 0 Then
                If Not Me.cmbUnit.Text <> "Loose" Then
                    grd.GetRow.Cells(grdEnm.ReceivedQty).Value = Val(grd.GetRow.Cells(grdEnm.Total).Value.ToString) / Val(grd.GetRow.Cells(grdEnm.Price).Value.ToString)
                    grd.GetRow.Cells(grdEnm.TotalQty).Value = Val(grd.GetRow.Cells(grdEnm.Qty).Value.ToString)
                Else
                    If Val(grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) > 0 Then
                        grd.GetRow.Cells(grdEnm.ReceivedQty).Value = (Val(grd.GetRow.Cells(grdEnm.Total).Value.ToString) / Val(grd.GetRow.Cells(grdEnm.Price).Value.ToString)) / Val(grd.GetRow.Cells(grdEnm.PackQty).Value.ToString)
                        grd.GetRow.Cells(grdEnm.TotalQty).Value = (Val(grd.GetRow.Cells(grdEnm.Qty).Value.ToString) * Val(grd.GetRow.Cells(grdEnm.PackQty).Value.ToString))
                    Else
                        grd.GetRow.Cells(grdEnm.ReceivedQty).Value = Val(grd.GetRow.Cells(grdEnm.Total).Value.ToString) / Val(grd.GetRow.Cells(grdEnm.Price).Value.ToString)
                        grd.GetRow.Cells(grdEnm.TotalQty).Value = Val(grd.GetRow.Cells(grdEnm.Qty).Value.ToString)
                    End If
                End If
            End If

            Me.grd.EditMode = Janus.Windows.GridEX.EditMode.EditOff

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function GetSingle(ByVal ReceivingNoteId As Integer)
        Dim Str As String = ""
        Try
            Str = "SELECT  Recv.ReceivingNo, Recv.ReceivingDate, dbo.PurchaseOrderMasterTable.PurchaseOrderNo, V.detail_title, Recv.ReceivingQty, " & _
             "Recv.ReceivingAmount, Recv.ReceivingNoteId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
             "Recv.PurchaseOrderID, Recv.DcNo, Recv.Arrival_Time, Recv.Departure_Time, Isnull(Recv.Total_Discount_Amount,0) as Total_Discount_Amount, IsNull(Recv.Post, 0) as Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, Recv.Vehicle_No, Recv.Driver_Name, Recv.DcDate,  Recv.Vendor_Invoice_No, Isnull(Recv.CostCenterId,0) as CostCenterId, Recv.IGPNo, isnull(Recv.CurrencyType,0) as CurrencyType, isnull(Recv.CurrencyRate,0) as CurrencyRate, ISNULL(Recv.Transportation_Vendor,0) as Transportation_Vendor, ISNULL(Recv.Custom_Vendor,0) as Custom_Vendor, V.Contact_Email as Email, Recv.Purchaseorderid as PO_ID, Recv.UpdateUserName " & _
             "FROM dbo.ReceivingNoteMasterTable Recv INNER JOIN " & _
             "vwCOADetail V ON Recv.VendorId = V.coa_detail_id LEFT OUTER JOIN " & _
             " dbo.PurchaseOrderMasterTable ON Recv.PurchaseOrderID = dbo.PurchaseOrderMasterTable.PurchaseOrderId Where Recv.ReceivingNoteId = " & ReceivingNoteId & " "
            Dim dt As DataTable = GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbLC_ValueChanged(sender As Object, e As EventArgs) Handles cmbLC.ValueChanged
        Try
            'If Not cmbLC.ActiveRow Is Nothing AndAlso Me.cmbLC.Value > 0 Then
            '    FillCombo("LCPO")
            'Else
            '    FillCombo("SO")
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''TASK TFS2235 made code commented and also made this field readonly. done by Muhammad Ameen
    Private Sub txtTotalQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtTotalQuantity.TextChanged
        'Try

        '    If (Not Val(Me.txtPackQty.Text) > 1 AndAlso Not (Val(Me.txtColumn1.Text) + Val(Me.txtColumn2.Text) + Val(Me.txtColumn3.Text)) > 1) Then
        '        Me.txtGrossQuantity.Text = Val(Me.txtTotalQuantity.Text)
        '    End If

        'Catch ex As Exception

        'End Try
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
    ''Included this event against TASK TFS2235. D
    Private Sub txtGrossQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtGrossQuantity.TextChanged
        Try
            Me.txtTotalQuantity.Text = Val(Me.txtGrossQuantity.Text) - (Val(Me.txtColumn1.Text) + Val(Me.txtColumn2.Text) + Val(Me.txtColumn3.Text))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtColumn1_TextChanged(sender As Object, e As EventArgs) Handles txtColumn1.TextChanged
        Try
            GetTotalQuantity()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtColumn2_TextChanged(sender As Object, e As EventArgs) Handles txtColumn2.TextChanged
        Try
            GetTotalQuantity()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtColumn3_TextChanged(sender As Object, e As EventArgs) Handles txtColumn3.TextChanged
        Try
            GetTotalQuantity()
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

    Private Sub grdSaved_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSaved.FormattingRow

    End Sub

    Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
        Try
            If Me.cmbCurrency.SelectedIndex > 0 Then
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
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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
                Me.grd.RootTable.Columns(grdEnm.CurrencyAmount).Caption = "Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(grdEnm.CurrencyRate).Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"
                'Me.grd.RootTable.Columns(grdEnm.CurrencyTaxAmount).Caption = "Tax Amount (" & Me.cmbCurrency.Text & ")"                 
                'grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(grdEnm.CurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(grdEnm.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(grdEnm.CurrencyRate).Visible = False
                    'Me.grd.RootTable.Columns(grdEnm.CurrencyTaxAmount).Visible = False
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
                    Me.grd.RootTable.Columns(grdEnm.CurrencyAmount).Visible = True
                    Me.grd.RootTable.Columns(grdEnm.CurrencyRate).Visible = True
                    'Me.grd.RootTable.Columns(grdEnm.CurrencyTaxAmount).Visible = False
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
End Class
