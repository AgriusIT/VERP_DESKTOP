''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction forms
''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''11-Jan-2014 Task:2373         Imran Ali                Add Columns SubSub Title in Account List on Sales/Purchase
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms  
''03-Feb-2014        Task:2406   Imran Ali    FIELD CHOOSER restriction (Senior Rozgar)
''06-Feb-2014          TASK:M16     Imran Ali   Add New Fields Engine No And Chassis No. on Sales  
''07-Feb-2014             TASK:2416     Imran Ali       Minus Stock Allowed Work Not Properly
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing\
''20-Feb-2014   TASK:2432 Imran Ali 1 Delivery Chalan Multi Record of Same Item     
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
'31-Mar-2014 Task:M28 Imran Ali Send Email With Attachment Problem
'Task No 2598 Mughees 29-4-2014 creating and setteling the newly adde field of Commnets 
'7-May-2014 Task:2606 JUNAID SHEHZAD Check Engine No and Invoice Detail in Delievery Chalan
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''22-Aug-2014 Task:2796 Imran Ali DCQtyExceedAgainstSales
''13-Spe-2014 Task:2842 Imran Ali Restriction Duplicate Egine No/Chassis No In Sales Return/Delivery Chalan
'08-Jun-2015  Task#2015060005 to allow all files to attach
''10-Jun-2015 Task# A3-10-06-2015, Ahmad Sharif: Added Key Pres event for some textboxes to take just numeric and dot value
''10-June-2015 Task# A2-10-06-2015 Ahmad Sharif: Add Check on grdSaved to check on double click if row less than zero than exit
''10-June-2015 Task# A1-10-06-2015 Ahmad Sharif: Check Vendor exist in combox list or not
'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'04-Aug-2015 Task#04082015 Ahmad Sharif: Fix error on deletion delivery challan
'08-Aug-2015 Task#08082015 Ahmad Sharif: Add configuration for sending sms with just item qunatity and engine no
'16-Sep-2015 Task#16092015 Ahmad Sharif: Load Companies and Locations user wise
'10-Nov-2015 TSK-1115-00033 Muhammad Ameen:	In case Delivery Chalan Item Stock is 1 then it does not load.
'' TASK TFS1378 Muhammad Ameen on 13-10-2017. Stock impact should go in case  configuration flag is on.
''TASK TFS1648 Muhammad Ameen on 03-11-2017. Quantity will be sumed in case item, location, Pack, Unit and Rate are same over addition of new row.
''TASK TFS3324 Muhammad Ameen done on 31-05-2018. In case an item is logical then its cost sheet items should be effected in stock.
''TFS3520 Ayesha Rehman on 13-06-2018. Sales Order status issue
''TFS4161 Ayesha Rehman : 09-08-2018 : P QTY: (Should Be Static/ Un-Changeable / Un-Editable on All Screens)
''TFS4689 Ayesha Rehman : 03-10-2018 : Show only relevant Accounts on Transactional screens while User wise COA Configuration.
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports SBDal.StockDAL
Imports SBDal.StockDocTypeDAL
Imports System.Data.OleDb
'Imports System.Speech.Synt
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win
'Imports ControlExenders

Public Class frmDeliveryChalan
    Implements IGeneral
    Enum Customer
        Id
        Name
        Code
        State
        City
        Territory
        ExpiryDate
        Discount
        Other_Exp
        Fuel
        TransitInsurance
        Credit_Limit
        Type
        Email
        PhoneNo
        Mobile
        SubSubTitle
    End Enum
    Enum EnumGridDetail
        LocationID
        ArticleCode
        Item
        AlternativeItem
        Size
        Color
        Unit
        OrderQty
        DeliverQty
        Qty
        RemainingQty
        PostDiscountPrice  'TFS2827
        Price
        BaseCurrencyId
        BaseCurrencyRate
        CurrencyId
        CurrencyRate
        CurrencyAmount
        Total
        DiscountId    'TFS2827
        DiscountFactor  'TFS2827
        DiscountValue  'TFS2827
        GroupID
        ArticleID
        PackQty
        CurrentPrice
        PackPrice
        DeliveryDetailID
        TradePrice
        Tax
        SED
        SavedQty
        SampleQty
        Discount_Percentage
        Freight
        MarketReturns
        SO_ID
        UM
        PurchasePrice
        NetBill
        Pack_Desc
        Engine_No 'Task:M16 Added Index
        Chassis_No 'Task:M16 Added Index
        BatchID
        BatchNo
        ExpiryDate
        Origin
        BardanaDeduction
        OtherDeduction
        AfterDeductionQty
        Comments '2598 Added new field of Enum for Comments in grid  
        OtherComments
        DeliveredQty
        Stock
        CostPrice
        DeliveryId
        SalesOrderDetailId
        Gross_Weights
        Tray_Weights
        Net_Weight
        TotalQty
        LogicalItem
        AdditionalItem 'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
        AlternativeItemId
        DeleteButton
    End Enum
    Dim dt As DataTable
    Dim IsFormOpend As Boolean = False
    Dim IsEditMode As Boolean = False
    Dim ExistingBalance As Double = 0D
    Dim Mode As String = "Normal"
    Dim blnOrderQtyExceed As String
    ' Dim Spech As New SpeechSynthesizer
    Dim InvId As Integer = 0
    Dim SelectCategory As Integer
    Dim SelectBarcodes As Integer
    Dim SelectVendor As Integer
    Dim SelectDeliveryMan As Integer
    Dim SelectCompany As Integer
    Dim SelectTrans As Integer
    Dim CostId As Integer
    Dim FuelExpAccount As Integer
    Dim AdjustmentExpAccount As Integer
    Dim OtherExpAccount As Integer
    Dim EditCustomerListOnDelivery As String
    Dim StockMaster As StockMaster
    Dim StockDetail As StockDetail
    Dim VNo As String = String.Empty
    Dim ExistingVoucherFlg As Boolean = False
    Dim VoucherId As Integer = 0
    Dim Email As Email
    Dim TradePrice As Double = 0
    Dim DeliveryTax_Percentage As Double = 0
    Dim SchemeQty As Double = 0
    Dim Discount_Percentage As Double = 0
    Dim Freight As Double = 0
    Dim MarketReturns As Double = 0D
    Dim IsSalesOrderAnalysis As Boolean = False 'Task: 2606 Sales Analysis check
    Dim StockList As List(Of StockDetail)       'Task: 2606 StockList
    Dim IsDeliveryOrderAnalysis As Boolean = False
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim setVoucherNo As String = String.Empty
    Dim crpt As New ReportDocument
    Dim Total_Amount As Double = 0D
    Dim setEditMode As Boolean = False
    Dim getVoucher_Id As Integer = 0
    Dim companyId As Integer = 0
    Dim Previouse_Amount As Double = 0D
    'Dim ServicesItem As String = 0D
    Dim TransitInssuranceTax As Double = 0D
    Dim WHTax As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim CompanyBasePrefix As Boolean = False
    Dim flgMultipleSalesOrder As Boolean = False
    Dim flgLoadAllItems As Boolean = False
    Dim flgLocationWiseItems As Boolean = False
    Dim flgVehicleIdentificationInfo As Boolean = False 'Task:M16 Added Flag Vehicle Identification
    Dim flgMargeItem As Boolean = False ''20-Feb-2014   TASK:2432 Imran Ali 1 Delivery Chalan Multi Record of Same Item     
    Dim flgAvgRate As Boolean = False 'Task:2606 Added Flag AvgRate
    Dim strOtherComments As String = String.Empty
    Dim strComments As String = String.Empty
    'Marked Against Task#2015060001 Ali Ansari
    'Dim arrfile As String
    'Marked Against Task#2015060001 Ali Ansari
    'Altered Against Task#2015060001 Ali Ansari
    ' Convert string into List of string for making proper count
    Dim arrFile As List(Of String)
    'Altered Against Task#2015060001 Ali Ansari
    Dim blnTradePriceExceed As Boolean = False
    Private _Previous_Balance As Double = 0D
    Private _dblSEDTaxPercent As Double = 0D
    'TASK-408
    Dim SalesOrderDetailId As Integer = 0D
    Dim AvailableStockInLoose As Double = 0D
    Dim AvailableStockInPack As Double = 0D
    Dim AvailableStock As Double = 0D
    Dim AvailableStock1 As Double = 0D
    Dim StockChecked As Boolean = False
    Dim BaseCurrencyId As Integer
    Dim BaseCurrencyName As String = String.Empty
    Dim NotificationDAL As New NotificationTemplatesDAL
    Public ModelId As Integer = 0
    'Dim StockList As List(Of StockDetail)
    Dim OrderQty As Double = 0D
    Dim DeliverQty As Double = 0D
    Dim RemainingQty As Double = 0D
    Dim SOQty As Double = 0D ''TFS2825
    'Dim flgLoadItemAfterDeliveredOnDC As Boolean = False ''TFS2825
    Public Const DiscountType_Percentage As String = "Percentage" ''TFS2827
    Public Const DiscountType_Flat As String = "Flat" ''TFS2827

    Dim SOBatchNo As String = String.Empty
    Dim SOExpiryDate As DateTime = Date.Now.AddMonths(1)
    Dim SOOrigin As String = String.Empty
    ''TFS3113 : Abubakar Siddiq : This Variable is Added to check ApprovalProcessId ,if it is mapped against the document
    Dim ApprovalProcessId As Integer = 0
    ''TFS3113 : Abubakar Siddiq :End
    Dim flgSOSeparateClosure As Boolean = False ''TFS3520 : Ayesha Rehman
    Dim CurrencyRate As Double = 1
    Dim IsSOLoaded As Boolean = False
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Dim ItemFilterByName As Boolean = False
    Dim IsSO As Boolean = False
    Private Sub frmDelivery_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13
            If e.KeyCode = Keys.S AndAlso e.Control Then
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
            If e.KeyCode = Keys.F5 Then
                BtnRefresh_Click(BtnPrint, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.Insert Then

                btnAdd_Click(BtnNew, Nothing)
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

    Private Sub frmDeliveryChalan_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try
            Me.gbJobCard.Visible = False
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
                flgLoadAllItems = getConfigValueByType("LoadAllItemsInSales")
            End If

            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
            End If

            If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
                flgLoadAllItems = getConfigValueByType("LoadAllItemsInSales")
            End If

            If Not getConfigValueByType("ArticleFilterByLocation") = "Error" Then
                flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
            End If

            If Not getConfigValueByType("TransitInssuranceTax").ToString = "Error" Then
                TransitInssuranceTax = Val(getConfigValueByType("TransitInssuranceTax").ToString)
            Else
                TransitInssuranceTax = 0
            End If

            If Not getConfigValueByType("WHTax_Percentage").ToString = "Error" Then
                WHTax = Val(getConfigValueByType("WHTax_Percentage").ToString)
            Else
                WHTax = 0
            End If

            'TAsk:2795 Set ByRef Variable blnOrderQtyExceed
            If Not getConfigValueByType("OrderQtyExceedAgainstDeliveryChalan").ToString = "Error" Then
                blnOrderQtyExceed = getConfigValueByType("OrderQtyExceedAgainstDeliveryChalan").ToString
            Else
                blnOrderQtyExceed = False
            End If
            'End Task:2796

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

            If Not getConfigValueByType("MultipleSalesOrder").ToString = "Error" Then
                flgMultipleSalesOrder = Convert.ToBoolean(getConfigValueByType("MultipleSalesOrder").ToString)
            Else
                flgMultipleSalesOrder = False
            End If
            ' ''Start TFS2825
            'If Not getConfigValueByType("LoadItemAfterDeliveredOnDC").ToString = "Error" Then
            '    flgLoadItemAfterDeliveredOnDC = Convert.ToBoolean(getConfigValueByType("LoadItemAfterDeliveredOnDC").ToString)
            'Else
            '    flgLoadItemAfterDeliveredOnDC = False
            'End If
            ' ''End TFS2825

            ''Start TFS3520
            If Not getConfigValueByType("SeparateClosureOfSODC") = "Error" Then
                flgSOSeparateClosure = getConfigValueByType("SeparateClosureOfSODC")
            Else
                flgSOSeparateClosure = False
            End If

            If flgSOSeparateClosure = True Then
                btnLoadSO.Visible = True
                Me.pnlSO.Visible = False
                Me.pnlSOMove.Location = New Point(15, 52)
            Else
                btnLoadSO.Visible = False
                Me.pnlSO.Visible = True
                Me.pnlSOMove.Location = New Point(15, 88)
            End If
            ''End TFS3520
            ''Start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161

            If getConfigValueByType("BarcodeEnabled") = "True" Then
                Mode = "BarcodeEnabled"
                barcodingPanel.Visible = True
                pnlSimple.TabStop = False
                pnlSimple.Enabled = False
                Me.txtBarcode.Focus()
            Else
                barcodingPanel.Visible = False
            End If
            '' Setting Account ids

            ''TASK TFS4544
            'If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            '    ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            'End If
            ''END TFS4544
            FuelExpAccount = Val(getConfigValueByType("FuelExpAccount").ToString)
            AdjustmentExpAccount = Val(getConfigValueByType("AdjustmentExpAccount").ToString)
            OtherExpAccount = Val(getConfigValueByType("OtherExpAccount").ToString)
            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)
            '' End setting account ids
            FillCombo("Category")
            'FillCombo("Barcodes")
            FillCombo("Vendor")
            FillCombo("SM")
            FillCombo("Transporter")
            'FillCombo("Item")
            FillCombo("Company")
            FillCombo("UM")
            FillCombo("Colour")
            FillCombo("ArticlePack")
            FillCombo("Currency")
            FillCombo("Discount Type") 'Task2827
            'Me.DisplayRecord()
            'ServicesItem = GetConfigValue("ServiceItem").ToString
            Me.IsFormOpend = True

            'If frmModProperty.blnListSeachStartWith = True Then
            '    cmbItem.AutoCompleteMode = Win.AutoCompleteMode.Suggest
            '    cmbItem.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.StartsWith
            'End If

            'If frmModProperty.blnListSeachContains = True Then
            '    cmbItem.AutoCompleteMode = Win.AutoCompleteMode.Suggest
            '    cmbItem.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.Contains
            'End If
            'Me.DisplayRecord() R933 Commented History Data
            '//This will hide Master grid
            Me.grdSaved.Visible = CType(getConfigValueByType("ShowMasterGrid"), Boolean)
            RefreshControls()
            GetDeliveryOrderAnalysis()

            'If Not GetConfigValue("Company-Based-Prefix").ToString = "Error" Then
            '    CompanyBasePrefix = Convert.ToBoolean(GetConfigValue("Company-Based-Prefix").ToString)
            'End If
            'cmbCategory_SelectedIndexChanged(Nothing, Nothing)
            'txtTransitInsurancePercentage.Text = TransitInssuranceTax
            'Me.txtWHTaxPercent.Text = WHTax

            Get_All(frmModProperty.Tags)
            'TFS3360
            UltraDropDownSearching(cmbVendor, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
            If frmModProperty.Tags.Length > 0 Then frmModProperty.Tags = String.Empty ''17-Feb-2014  Task:2427 Imran Ali  Dispatch Detail Repor Problem.
        End Try
    End Sub

    Private Sub DisplayRecord(Optional ByVal StrCondition As String = "")
        Try
            'Dim ServiceItem As String = GetConfigValue("ServiceItem").ToString
            Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
            Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
            Dim str As String = String.Empty
            'rafay :modified query to add PO_NO in history grid
            'rafay :modified query to add Amount US and currency column in history grid
            str = "SELECT " & IIf(StrCondition.ToString = "All", "", "Top 50") & "   Recv.DeliveryNo, Recv.DeliveryDate AS Date,V.SalesOrderNo, V.SalesOrderDate, vwCOADetail.detail_title as CustomerName, Pack.Packs, (Recv.DeliveryQty) As DeliveryQty,dbo.tblcurrency.currency_code As currency_code, Recv.DeliveryAmount,Recv.AmountUS, Recv.DeliveryId, Recv.Arrival_Time , Recv.Departure_Time, " & _
                          "Recv.CustomerCode, tbldefEmployee.Employee_Name, Recv.Remarks,Recv.PO_NO,Currency.CurrencyRate, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId, Recv.BiltyNo,isnull(Recv.TransporterId ,0) as TransporterId, Recv.LocationId, Recv.FuelExpense as Fuel, Recv.OtherExpense as Expense ,recv.Adjustment,  isnull(recv.TransitInsurance,0) as TransitInsurance, IsNull(Recv.Post,0) as Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end As Status, ISNULL(Recv.ServiceItemDelivery,0) as ServiceItemDelivery, Recv.DcDate, ISNULL(Recv.Delivered,0) as Delivered, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], vwCOADetail.Contact_Email as Email, IsNull([No Of Attachment],0) as  [No Of Attachment], Recv.Driver_Name, Recv.Vehicle_No,Recv.Other_Company,Recv.UserName as 'User Name', Recv.UpdateUserName as [Modified By], IsNull(Recv.JobCardId, 0) As JobCardId " & _
                          "FROM DeliveryChalanMasterTable Recv INNER JOIN " & _
                          "vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN (Select DeliveryId, Sum(IsNull(Sz7, 0)) As Packs From DeliveryChalanDetailTable Group By DeliveryId) As Pack ON Recv.DeliveryId = Pack.DeliveryId LEFT OUTER JOIN " & _
                          "tblDefEmployee ON Recv.EmployeeCode = tblDefEmployee.Employee_Id LEFT OUTER JOIN " & _
                          "(Select Distinct DeliveryChalanDetailTable.DeliveryId,CurrencyId,CurrencyRate  From DeliveryChalanDetailTable) As Currency ON Recv.DeliveryId = Currency.DeliveryId LEFT OUTER JOIN  " & _
                          "dbo.tblcurrency ON Currency.CurrencyId = dbo.tblcurrency.currency_id Left Outer Join " & _
                          "SalesOrderMasterTable V ON Recv.POId = V.SalesOrderID LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = Recv.DeliveryID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.DeliveryNo " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, DeliveryDate, 102) > Convert(Datetime, N'" & ClosingDate.ToString("yyyy-M-d 23:59:59") & "', 102))") & " "
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            If Me.dtpFrom.Checked = True Then
                str += " AND Recv.DeliveryDate >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
            End If
            If Me.dtpTo.Checked = True Then
                str += " AND Recv.DeliveryDate <= Convert(Datetime, N'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
            End If
            If Me.txtSearchDocNo.Text <> String.Empty Then
                str += " AND Recv.DeliveryNo LIKE '%" & Me.txtSearchDocNo.Text & "%'"
            End If
            If Me.cmbSearchLocation.SelectedIndex <> 0 Then
                If Not Me.cmbSearchLocation.SelectedValue Is Nothing Then
                    str += " AND Recv.LocationId=" & Me.cmbSearchLocation.SelectedValue
                End If
            End If
            If Me.txtFromAmount.Text <> String.Empty Then
                If Val(Me.txtFromAmount.Text) > 0 Then
                    str += " AND Recv.DeliveryAmount >= " & Val(Me.txtFromAmount.Text) & " "
                End If
            End If
            If Me.txtToAmount.Text <> String.Empty Then
                If Val(Me.txtToAmount.Text) > 0 Then
                    str += " AND Recv.DeliveryAmount <= " & Val(Me.txtToAmount.Text) & ""
                End If
            End If
            If cmbSearchAccount.ActiveRow IsNot Nothing Then
                If Me.cmbSearchAccount.SelectedRow.Cells(0).Value <> 0 Then
                    str += " AND Recv.CustomerCode = " & Me.cmbSearchAccount.Value
                End If
            End If
            If Me.txtSearchRemarks.Text <> String.Empty Then
                str += " AND Recv.Remarks LIKE '%" & Me.txtSearchRemarks.Text & "%'"
            End If
            If Me.txtPurchaseNo.Text <> String.Empty Then
                str += " AND SalesOrderMasterTable.SalesOrderNo LIKE  '%" & Me.txtPurchaseNo.Text & "%'"
            End If
            If Me.txtSearchEnginNo.Text.Length > 0 Then
                str += " AND Recv.DeliveryId in(Select DeliveryId From DeliveryChalanDetailTable WHERE  Engine_No LIKE '%" & Me.txtSearchEnginNo.Text & "%')"
            End If
            If Me.txtChassisNo.Text.Length > 0 Then
                str += " AND Recv.DeliveryId in(Select DeliveryId From DeliveryChalanDetailTable WHERE  Chassis_No LIKE '%" & Me.txtChassisNo.Text & "%')"
            End If
            'rafay apply filter to show record on top in grd history when save button is click
            str += " ORDER BY Recv.DeliveryId DESC"
            'End If
            Me.grdSaved.DataSource = GetDataTable(str)
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            'FillGrid(grdSaved, str)
            grdSaved.RootTable.Columns("DeliveryId").Visible = False
            'grdSaved.Columns(4).Visible = False
            grdSaved.RootTable.Columns("CurrencyRate").Visible = False
            grdSaved.RootTable.Columns("CustomerCode").Visible = False
            grdSaved.RootTable.Columns("EmployeeCode").Visible = False
            grdSaved.RootTable.Columns("PoId").Visible = False
            grdSaved.RootTable.Columns("TransporterId").Visible = False
            grdSaved.RootTable.Columns("BiltyNo").Visible = False
            grdSaved.RootTable.Columns("LocationId").Visible = False
            grdSaved.RootTable.Columns("Adjustment").Visible = False
            grdSaved.RootTable.Columns("Post").Visible = False
            grdSaved.RootTable.Columns("Email").Visible = False
            grdSaved.RootTable.Columns("Other_Company").Visible = False
            grdSaved.RootTable.Columns("currency_code").Visible = True
            'grdSaved.RootTable.Columns("ServiceItemDelivery").Visible = False
            grdSaved.RootTable.Columns("DeliveryNo").Caption = "Issue No"
            grdSaved.RootTable.Columns("Date").Caption = "Date"
            grdSaved.RootTable.Columns("Arrival_Time").Caption = "In Time"
            grdSaved.RootTable.Columns("Departure_Time").Caption = "Out Time"
            grdSaved.RootTable.Columns("SalesOrderNo").Caption = "S-Order"
            grdSaved.RootTable.Columns("SalesOrderDate").Caption = "Date"
            grdSaved.RootTable.Columns("CustomerName").Caption = "Customer"
            grdSaved.RootTable.Columns("DeliveryQty").Caption = "Qty"
            grdSaved.RootTable.Columns("currency_code").Caption = "Currency"
            grdSaved.RootTable.Columns("AmountUS").Caption = "Orignal Value"
            grdSaved.RootTable.Columns("DeliveryAmount").Caption = "Base Value"
            grdSaved.RootTable.Columns("Employee_Name").Caption = "Employee"
            grdSaved.RootTable.Columns("CustomerName").Width = 200
            grdSaved.RootTable.Columns("Remarks").Width = 200
            'Rafay:Task Start:
            'Rafay:the foreign currency is add on purchase history
            Dim grdSaved1 As DataTable = GetDataTable(str)
            grdSaved1.Columns("AmountUS").Expression = "IsNull(DeliveryAmount,0) / (IsNull(CurrencyRate,0))" 'Task:2374 Show Total Amount
            Me.grdSaved.DataSource = grdSaved1
            'Set rounded format
            Me.grdSaved.RootTable.Columns("AmountUS").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("DeliveryAmount").FormatString = "N" & DecimalPointInValue
            'rafay
            'Rafay
            grdSaved.RootTable.Columns("PO_NO").Width = 150
            Me.grdSaved.RootTable.Columns("Expense").Visible = False
            Me.grdSaved.RootTable.Columns("Fuel").Visible = False
            Me.grdSaved.RootTable.Columns("Adjustment").Visible = False
            Me.grdSaved.RootTable.Columns("TransitInsurance").Visible = False
            Me.grdSaved.RootTable.Columns("DcDate").Visible = False
            Me.grdSaved.RootTable.Columns("ServiceItemDelivery").Visible = False
            Me.grdSaved.RootTable.Columns("JobCardId").Visible = False
            Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdSaved.RootTable.Columns("Packs").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("DeliveryQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("DeliveryAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Expense").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Fuel").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Adjustment").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Packs").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("DeliveryQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("DeliveryAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Expense").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Fuel").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Adjustment").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("TransitInsurance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Packs").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("DeliveryQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("DeliveryAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Expense").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Fuel").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Adjustment").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("TransitInsurance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("Arrival_Time").FormatString = "hh:mm:ss tt"
            Me.grdSaved.RootTable.Columns("Departure_Time").FormatString = "hh:mm:ss tt"
            'Task:2759 set Rounded amount format
            grdSaved.RootTable.Columns("DeliveryAmount").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Expense").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Fuel").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Adjustment").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("TransitInsurance").FormatString = "N" & DecimalPointInValue
            'end Task:2759
            CtrlGrdBar2_Load_1(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            'If Not sender.GetType.Name.ToString = "UltraCombo" Then
            If Not Validate_AddToGrid() Then
                If Not Mode = "Normal" Then Me.txtBarcode.Focus()
                Exit Sub
            End If
            'End If
            'If Not GetConfigValue("ServiceItem").ToString = "True" Then
            'Task:2432 Added Flg MargItem
            'If FindExistsItem(Me.cmbItem.Value, Val(Me.txtRate.Text), Me.cmbUnit.Text, Val(Me.txtPackQty.Text), Val(Me.cmbPo.SelectedValue), Val(Me.cmbCategory.SelectedValue)) = False Then
            AddItemToGrid()
            'End If
            'If Not FindExistsItem(Me.cmbItem.Value, Val(Me.txtRate.Text), Me.cmbUnit.Text, Val(Me.txtPackQty.Text), Val(Me.cmbPo.SelectedValue)) = True Then
            'Else
            '    AddItemToGrid()
            'End If
            'Else
            'AddItemToGrid()
            'End If

            'End If
            'End Task:2432
            'End If
            ' Me.cmbItem.ActiveRow.Cells("Stock").Value = Val(Me.cmbItem.ActiveRow.Cells("Stock").Value) - Val(Me.txtQty.Text)
            GetTotal()
            ClearDetailControls()
            If Me.Mode = "Normal" Then cmbItem.Focus()
            'FillCombo("Item")







        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RefreshControls()
        Try

            Me.IsEditMode = False
            'If Me.BtnSave.Text = "&Update" Then
            '    FillCombo("Vendor")
            'End If 'R933 Commented
            IsSOLoaded = False
            Me.txtAdjustment.Text = String.Empty
            Me.pnlSimple.Height = Val(pnlSimple.Tag)
            'Marked Against Task#2015060001
            'Array.Clear(arrFile, 0, arrFile.Length)
            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari
            'Clear arrfile
            'Clear Attached file records
            SOBatchNo = String.Empty
            SOExpiryDate = Date.Now.AddMonths(1)
            SOOrigin = String.Empty
            arrFile = New List(Of String)
            Me.btnAttachment.Text = "Attachment (" & arrFile.Count & ")"
            'Altered Against Task#2015060001 Ali Ansri
            'Array.Clear(arrFile, 0, arrFile.Length)
            Me.CurrencyRate = 1
            Me.btnAttachment.Text = "Attachment"
            'TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            'Rafay
            companyinitials = ""
            'Rafay
            'END TFS4544
            FillCombo("Item")
            If Me.Mode = "Normal" Then
                Me.dtpPODate.Focus()
                txtPONo.Text = ""
                dtpPODate.Value = Now
                txtRemarks.Text = ""
                'Rafay
                txtPO.Text = ""
                'Rafay
                txtPaid.Text = ""
                'txtAmount.Text = ""
                txtTotal.Text = ""
                'txtTotalQty.Text = ""
                txtBalance.Text = ""
                txtPackQty.Text = 1
                'Me.txtExpense.Text = 0
                'Me.txtFuel.Text = 0
                Me.uitxtBiltyNo.Text = ""
                Me.txtDriverName.Text = String.Empty
                Me.txtVehicleNo.Text = String.Empty
                Me.txtSearchEnginNo.Text = String.Empty
                Me.txtChassisNo.Text = String.Empty
                Me.dtpArrivalTime.Value = Date.Now
                Me.dtpDepartureTime.Value = Date.Now
                Me.dtpDepartureTime.Checked = False
                Me.BtnSave.Text = "&Save"
                Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("SI" & Me.cmbCompany.SelectedValue, 6, "DeliveryChalanMasterTable", "DeliveryNo")
                Me.ExistingBalance = 0D
                'FillCombo("SO")
                FillCombo("UM")
                Me.cmbColour.Text = ""
                'If Me.cmbCategory.SelectedValue > 0 Then
                '    FillCombo("ItemFilter")
                'Else
                '    FillCombo("Item")
                'End If
                Me.cmbCompany.Enabled = True
                Me.cmbPo.Enabled = True
                ''Commented Against TFS4339
                ' If Me.cmbSalesMan.SelectedIndex = 0 Then Me.cmbSalesMan.SelectedIndex = 0 Else Me.cmbSalesMan.SelectedIndex = Me.cmbSalesMan.SelectedIndex
                Me.cmbSalesMan.SelectedIndex = 0
                Me.cmbTransporter.SelectedIndex = 0
                ''End TFS4339
                cmbUnit.SelectedIndex = 0
                txtDiscountValue.Text = ""   '12-04-2018 : Task# TFS2827: Ayesha Rehman: Set these controls text empty on load,new
                Me.cmbDiscountType.SelectedIndex = 1 'TFS2827
                Me.txtPDP.Text = "" 'TFS2827
                If Me.cmbVendor.Rows.Count > 0 Then cmbVendor.Rows(0).Activate()
                If Me.cmbItem.Rows.Count > 0 Then cmbItem.Rows(0).Activate() ''TFS3330
                '            grd1.Rows.Clear()
                '
                'Me.cmbSalesMan.Focus()
                'Me.cmbSalesMan.DroppedDown = True
                'Me.cmbVendor.Focus()
                'me.cmbVendor
                Me.cmbVendor.Enabled = True
                If cmbPo.Items.Count > 0 Then Me.cmbPo.SelectedIndex = 0
                'Me.LinkLabel1.Enabled = True
                'If Not GetConfigValue("ServiceItem").ToString = "True" Then
                If flgLoadAllItems = True Then
                    Me.DisplayDetail(-1, -1, "LoadAll")
                    'Me.LinkLabel1.Visible = False
                Else
                    'Me.LinkLabel1.Visible = True
                    'Me.LinkLabel1.Enabled = True
                    Me.DisplayDetail(-1)
                End If
                'Else
                '    Me.DisplayDetail(-1)
                'End If
                'GetDeliveryOrderAnalysis()
                Me.GetSecurityRights()
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
                Me.SplitContainer2.Panel1Collapsed = True
                Me.CtrlGrdBar1_Load(Nothing, Nothing)
                CtrlGrdBar2_Load(Nothing, Nothing)
                CtrlGrdBar2_Load_1(Nothing, Nothing)
                'Me.txtAddTransitInsurance.Text = 0
                'Me.txtTransitInsurancePercentage.Text = TransitInssuranceTax
                Me.dtpPODate.Enabled = True
                FillCombo("Other_Company")
                FillCombo("Colour")
                cmbOtherCompany.Text = String.Empty
            Else
                Me.cmbCompany.Enabled = True
                Me.cmbPo.Enabled = True
                txtPONo.Text = ""
                dtpPODate.Value = Now
                txtRemarks.Text = ""
                'Rafay
                txtRemarks.Text = ""
                cmbUnit.SelectedIndex = 0
                txtDiscountValue.Text = ""   '12-04-2018 : Task# TFS2827: Ayesha Rehman: Set these controls text empty on load,new
                Me.cmbDiscountType.SelectedIndex = 1 'TFS2827
                Me.txtPDP.Text = "" 'TFS2827
                If Me.cmbSalesMan.SelectedIndex > 0 Then Me.cmbSalesMan.SelectedIndex = 1
                If Me.cmbItem.Rows.Count > 0 Then cmbItem.Rows(0).Activate() ''TFS3330
                Me.cmbPo.Enabled = True
                txtPaid.Text = ""
                'txtAmount.Text = ""
                txtTotal.Text = ""
                'txtTotalQty.Text = ""
                txtBalance.Text = ""
                txtPackQty.Text = 1
                Me.uitxtBiltyNo.Text = ""
                Me.BtnSave.Text = "&Save"
                Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("SI" & Me.cmbCompany.SelectedValue, 6, "DeliveryChalanMasterTable", "DeliveryNo")
                Me.ExistingBalance = 0D
                'Me.txtAddTransitInsurance.Text = 0
                'Me.txtTransitInsurancePercentage.Text = 0
                If Me.cmbVendor.Rows.Count > 1 Then Me.cmbVendor.Rows(1).Activate()
                If Me.cmbItem.Rows.Count > 0 Then cmbItem.Rows(0).Activate() ''TFS3330
                ''Commented Against TFs4339
                'If Me.cmbTransporter.Items.Count > 1 Then Me.cmbTransporter.SelectedIndex = 1
                Me.cmbTransporter.SelectedIndex = 0
                Me.txtBarcode.Focus()
                'DisplayRecord()
                'If Not GetConfigValue("ServiceItem").ToString = "True" Then
                If flgLoadAllItems = True Then
                    Me.DisplayDetail(-1, -1, "LoadAll")
                    'Me.LinkLabel1.Visible = False
                Else
                    'Me.LinkLabel1.Visible = True
                    'Me.LinkLabel1.Enabled = True
                    Me.DisplayDetail(-1)
                End If
                'Else
                '    Me.DisplayDetail(-1)
                'End If
                'GetDeliveryOrderAnalysis()
                Me.CtrlGrdBar1_Load(Nothing, Nothing)
                CtrlGrdBar2_Load(Nothing, Nothing)
                CtrlGrdBar2_Load_1(Nothing, Nothing)
                'Me.Mode = "Normal"
            End If
            If flgMultipleSalesOrder = True Then
                Me.btnLoad.Visible = True
            Else
                Me.btnLoad.Visible = False
            End If
            'EditCustomerListOnDelivery = getConfigValueByType("EditCustomerListOnDelivery").ToString
            'If IsEditMode = True Then
            '    If EditCustomerListOnDelivery = "False" Then
            '        Me.cmbVendor.Enabled = True
            '    Else
            '        Me.cmbVendor.Enabled = False
            '    End If
            'End If
            If Me.cmbBatchNo.Value = Nothing Then
                Me.cmbBatchNo.Enabled = False
            Else
                Me.cmbBatchNo.Enabled = True
            End If
            ComboFillByEdit()
            '---------------- Receipt Voucher ---------------
            'If GetConfigValue("ReceiptVoucherOnDelivery").ToString = "True" Then
            '    Me.btnReceipt.Visible = True
            'Else
            '    Me.btnReceipt.Visible = False
            'End If
            'Me.SplitContainer1.Panel2Collapsed = True
            'FillPaymentMethod()
            '' Me.cmbMethod.SelectedIndex = 0
            'Me.cmbMethod.Enabled = True
            'Me.txtChequeNo.Text = String.Empty
            'Me.dtpChequeDate.Value = Date.Today
            'Me.txtRecAmount.Text = String.Empty
            'Me.txtVoucherNo.Text = GetVoucherNo()
            'VoucherDetail(String.Empty)
            '-------------------------------------------------
            Me.lblPrintStatus.Text = String.Empty

            Me.txtJobCardId.Text = ""
            Me.txtJobCardNo.Text = ""
            Me.dtpJobCardDate.Value = Date.Now
            Me.txtLeft.Text = ""
            Me.txtJobCardCustomer.Text = ""
            Me.txtVehicle.Text = ""
            Me.txtRegistrationNo.Text = ""
            ModelId = 0
            'Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnEdit.Visible = False
            Me.BtnDelete.Visible = False
            Me.BtnPrint.Visible = False
            Me.StockChecked = False
            '''''''''''''''''''''''''''''''''''''''''''''''''''
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
            Me.cmbCurrency.Enabled = True
            ApplyGridSettings()
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
            Me.cmbCurrency.Enabled = True
            Me.gbJobCard.Visible = False
            Me.txtRate.Text = ""
            Me.txtPackRate.Text = ""

            'Abubakar Siddiq : TFS2375 : Enable Approval History button only in Eidt Mode
            If IsEditMode = True Then
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            Else
                Me.btnApprovalHistory.Visible = False
            End If
            'Abubakar Siddiq : TFS3113 : End
            ''Abubakar Siddiq :TFS3113 :Making Approval Button Enable in Edit Mode
            If Not getConfigValueByType("DeliveryChallanApproval") = "Error" Then
                ApprovalProcessId = Val(getConfigValueByType("DeliveryChallanApproval"))
            End If
            If ApprovalProcessId = 0 Then
                Me.chkPost.Visible = True
                Me.chkPost.Enabled = True

                Me.chkDelivered.Visible = True
                Me.chkDelivered.Enabled = True
            Else
                Me.chkPost.Visible = False
                Me.chkPost.Enabled = False
                Me.chkPost.Checked = False

                Me.chkDelivered.Visible = False
                Me.chkDelivered.Enabled = False
                Me.chkDelivered.Checked = False
            End If
            ''Abubakar Siddiq :TFS3113 :End

            If Not getConfigValueByType("AvgRate").ToString = "Error" Then
                flgAvgRate = getConfigValueByType("AvgRate")
            Else
                flgAvgRate = False
            End If
            ''Start TFS3520
            If Not getConfigValueByType("SeparateClosureOfSODC") = "Error" Then
                flgSOSeparateClosure = getConfigValueByType("SeparateClosureOfSODC")
            Else
                flgSOSeparateClosure = False
            End If

            If flgSOSeparateClosure = True Then
                btnLoadSO.Visible = True
                Me.pnlSO.Visible = False
                Me.pnlSOMove.Location = New Point(15, 52)
            Else
                btnLoadSO.Visible = False
                Me.pnlSO.Visible = True
                Me.pnlSOMove.Location = New Point(15, 88)
            End If
            ''End TFS3520



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ClearDetailControls()
        cmbCategory.SelectedIndex = 0
        cmbUnit.SelectedIndex = 0
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

    Private Function Validate_AddToGrid() As Boolean
        If cmbItem.ActiveRow.Cells(0).Value <= 0 Then
            msg_Error("Please select an item")
            cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'If Not GetConfigValue("ServiceItem").ToString = "True" Then
        If Val(txtQty.Text) <= 0 Then
            ' ''Start TFS2825
            'If Not flgLoadItemAfterDeliveredOnDC Then
            msg_Error("Quantity is not greater than 0")
            txtQty.Focus() : Validate_AddToGrid = False : Exit Function
            'End If
            ' ''End TFS2825
        End If
        'Change by murtaza default currency rate(10/26/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'Change by murtaza default currency rate(10/26/2022)
        'Else
        '    If Val(txtServiceQty.Text) <= 0 Then
        '        msg_Error("Service Quantity must be greater than 0")
        '        txtServiceQty.Focus() : Validate_AddToGrid = False : Exit Function
        '    End If
        'End If

        If Val(txtRate.Text) < 0 Then
            msg_Error("Rate is not greater than 0")
            txtRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        If Val(txtTotalQuantity.Text) < 0 Then
            msg_Error("Total Quantity is required more than 0")
            txtTotalQuantity.Focus() : Validate_AddToGrid = False : Exit Function
        End If


        Dim flag As Boolean = False
        If blnTradePriceExceed = True Then
            If cmbItem.ActiveRow.Cells("Trade Price").Value > Val(txtRate.Text.Trim) Then
                If Not msg_Confirm("Sale price is less than trade price") Then
                    flag = True
                    cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
                End If
            End If
        End If
        If flag = False Then
            If cmbItem.ActiveRow.Cells("PurchasePrice").Value > Val(txtRate.Text.Trim) Then
                If Not msg_Confirm("Sale price is less than purchase price" & (Chr(13)) & "Do u want to add this item?") = True Then
                    cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
                End If
            End If
        End If
        'Before against task:2388
        'Dim IsMinus As Boolean = getConfigValueByType("AllowMinusStock")
        'Task:2388 Added Validate Service item
        If StockChecked = False Then
            Dim IsMinus As Boolean = True
            If CType(Me.cmbItem.SelectedRow, Infragistics.Win.UltraWinGrid.UltraGridRow).Cells("ServiceItem").Value = False Then
                IsMinus = getConfigValueByType("AllowMinusStock")
            End If
            Dim totalQuantity As Double = 0D
            If Me.cmbUnit.Text = "Pack" Then
                totalQuantity = Val(Me.txtQty.Text)
            Else
                totalQuantity = Math.Round(Val(Me.txtTotalQuantity.Text), TotalAmountRounding)
            End If

            'Dim Sum As Double = 0
            ''If dt IsNot Nothing Then
            'If Me.grd.RowCount > 0 Then
            '    For i As Integer = 0 To Me.grd.RowCount - 1
            '        Sum += (Me.grd.GetRows(i).Cells("Qty").Value)
            '    Next
            '    If Sum + Val(Me.txtQty.Text) > Val(Me.txtStock.Text) Then
            '        msg_Error("Entered Quantity Amount is greater than Stock")
            '        Validate_AddToGrid = False : Exit Function
            '    End If
            'End If

            'End If



            If Val(txtQty.Text) > Val(txtStock.Text) Then
                If Mode = "Normal" Then
                    If Not IsMinus = True Then
                        If Val(Me.txtStock.Text) <> Val(Me.txtTotalQuantity.Text) Then
                            If Val(Me.txtStock.Text) - Val(totalQuantity) < 0 Then
                                msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
                                cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
                            End If
                        End If

                    End If
                    If Not Me.cmbCategory.SelectedIndex = -1 Then
                        If CType(Me.cmbCategory.SelectedItem, DataRowView).Row.Item("AllowMinusStock").ToString = "False" AndAlso IsMinus = True Then
                            '--
                            If Val(Me.txtStock.Text) <> Val(Me.txtTotalQuantity.Text) Then
                                If Val(Me.txtStock.Text) - Val(totalQuantity) < 0 Then
                                    msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
                                    cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
                                End If
                            End If

                        End If
                    End If
                End If
            End If
        End If
        Validate_AddToGrid = True
    End Function
    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus
        'If Not GetConfigValue("ServiceItem").ToString = "True" Then

        'If Val(Me.txtPackQty.Text) = 0 Then
        '    txtPackQty.Text = 1
        '    txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
        'Else
        '    txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
        'End If


        'Else
        'If cmbUnit.SelectedIndex = 0 Then
        '    txtPackQty.Text = 1
        '    txtTotal.Text = Val(txtServiceQty.Text) * Val(txtRate.Text)
        'Else
        '    txtTotal.Text = Val(txtServiceQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
        'End If
        'End If
        Try
            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            'Else
            '    txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
            'End If
            If IsDeliveryOrderAnalysis = True Then
                If Val(Me.txtDisc.Text) <> 0 Then
                    Me.txtDisc.TabStop = True
                End If
            End If
            GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub
    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRate.LostFocus
        'If Not GetConfigValue("ServiceItem").ToString = "True" Then
        'If cmbUnit.SelectedIndex = 0 Then
        '    txtPackQty.Text = 1
        '    txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
        'Else
        '    txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
        'End If



        'If Val(Me.txtPackQty.Text) = 0 Then
        '    txtPackQty.Text = 1
        '    txtTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
        'Else
        '    txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100)
        'End If


        'Else
        'If cmbUnit.SelectedIndex = 0 Then
        '    txtPackQty.Text = 1
        '    txtTotal.Text = Val(txtServiceQty.Text) * Val(txtRate.Text)
        'Else
        '    txtTotal.Text = Val(txtServiceQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
        'End If
        'End If
        Me.GetDetailTotal()
    End Sub

    Private Sub txtRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate.TextChanged
        Try
            'If Val(Me.txtPackQty.Text) > 0 Then
            '    Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            'Else
            '    Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
            'End If
            'txtTotal.Text = (Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        ''get the qty in case of pack unit
        'If Not GetConfigValue("ServiceItem").ToString = "True" Then
        'FillCombo("")
        If Me.cmbUnit.SelectedIndex < 0 Then Me.txtPackQty.Text = String.Empty : Exit Sub

        If Me.cmbUnit.Text = "Loose" Then
            txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
            txtPackQty.Text = 1
            txtPackQty.TabStop = False
            txtPackQty.Enabled = False
            Me.txtTotalQuantity.Enabled = False
            Me.txtPackRate.Enabled = False
            Me.txtPDP.Enabled = True ''TFS3330
            ClearDetailControlsUponUnit() ''TFS3330 : Reset controls when unit change to loose 
            If cmbItem.ActiveRow IsNot Nothing Then
            Me.txtPDP.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString ''TFS2827
            End If
            Dim StrSQl As String = "" ''TFS3330 : Ayesha Rehman
            Dim CustomerId As Integer = 0
            If cmbVendor.ActiveRow IsNot Nothing Then
                CustomerId = Me.cmbVendor.ActiveRow.Cells(0).Value
            End If
            If CustomerId > 0 Then
                If Me.cmbItem.ActiveRow.Cells(0).Value > 0 Then
                    If getConfigValueByType("ApplyFlatDiscountOnSale").ToString = "False" Then
                        StrSQl = "select discount from tbldefcustomerbasediscounts where articledefid = " & Me.cmbItem.ActiveRow.Cells(0).Value _
                        & " and typeid = " & Me.cmbVendor.ActiveRow.Cells("typeid").Value & "  and discount > 0 "

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
                                If IsDeliveryOrderAnalysis = True Then
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
                                        Me.txtRate.Text = price - ((price / 100) * dblDiscountPercent)
                                        Me.txtPDP.Text = price - ((price / 100) * dblDiscountPercent) ''TFS2827
                                        Me.txtDisc.TabStop = False
                                    Else
                                        Me.txtRate.Text = Me.txtRate.Text
                                        Me.txtPDP.Text = Me.txtPDP.Text
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
            If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
            'Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))
        Else
            'Dim objCommand As New OleDbCommand
            'Dim objCon As OleDbConnection
            'Dim objDataAdapter As New OleDbDataAdapter
            'Dim objDataSet As New DataSet

            'objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

            'If objCon.State = ConnectionState.Open Then objCon.Close()

            'objCon.Open()
            'objCommand.Connection = objCon
            'objCommand.CommandType = CommandType.Text


            'objCommand.CommandText = "Select PackQty from ArticleDefTable where ArticleID = " & cmbItem.Value 'cmbItem.ActiveRow.Cells(0).Value

            'txtPackQty.Text = objCommand.ExecuteScalar()
            If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
                Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
                Me.txtPackRate.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackRate").ToString) ''TFS3330
                If txtPackRate.Text = 1 Then
                    txtPackRate.Text = 0
                    'txtRate.Text = 0
                End If
            End If
            'If Not Me.cmbUnit.SelectedItem Is Nothing Then
            '    Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
            'End If
            'txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
            txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), TotalAmountRounding)
            ''Start TFS4161
            If IsPackQtyDisabled = True Then
                txtPackQty.TabStop = False
                txtPackQty.Enabled = False
                Me.txtTotalQuantity.Enabled = False
                Me.txtPackRate.Enabled = False
            Else
            txtPackQty.TabStop = True
            txtPackQty.Enabled = True
            Me.txtTotalQuantity.Enabled = True
            Me.txtPackRate.Enabled = True
            End If
            ''Start TFS4161
            'End If
            'Else
            '    If cmbUnit.SelectedIndex = 0 Then
            '        txtTotal.Text = Val(txtServiceQty.Text) * Val(txtRate.Text)
            '        txtPackQty.Text = 1
            '        txtPackQty.TabStop = False
            '        txtPackQty.Enabled = False
            '    Else
            '        Dim objCommand As New OleDbCommand
            '        Dim objCon As OleDbConnection
            '        Dim objDataAdapter As New OleDbDataAdapter
            '        Dim objDataSet As New DataSet

            '        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

            '        If objCon.State = ConnectionState.Open Then objCon.Close()

            '        objCon.Open()
            '        objCommand.Connection = objCon
            '        objCommand.CommandType = CommandType.Text


            '        objCommand.CommandText = "Select PackQty from ArticleDefTable where ArticleID = " & cmbItem.Value 'cmbItem.ActiveRow.Cells(0).Value

            '        txtPackQty.Text = objCommand.ExecuteScalar()

            '        'txtTotal.Text = Val(txtServiceQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)

            '        txtPackQty.TabStop = True
            '        txtPackQty.Enabled = True
            '    End If
            'Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))
        End If
        If cmbItem.ActiveRow IsNot Nothing Then
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))
        End If
    End Sub
    Private Sub AddItemToGrid()
        'If Not Me.cmbPo.SelectedIndex > 0 Then
        'If Not Val(Me.txtTax.Text) > 0 Then
        If IsDeliveryOrderAnalysis = True Then
            If GST_Applicable = True Then
                If DeliveryTax_Percentage = 0 Then DeliveryTax_Percentage = Val(getConfigValueByType("Default_Tax_Percentage").ToString)
            ElseIf FlatRate_Applicable = True Then
                If DeliveryTax_Percentage = 0 Then DeliveryTax_Percentage = ((FlatRate / Val(Me.cmbItem.SelectedRow.Cells("Price").Text)) * 100)
            End If
        Else
            DeliveryTax_Percentage = Val(Me.txtTax.Text)
        End If
        'Else
        'DeliveryTax_Percentage = Val(Me.txtTax.Text)
        'End If
        ' If (Val(Me.txtStock.Text) >= 0 Or Val(Me.txtStock.Text)) <= 0 Then

        Dim qty1 As Double = 0D
        Dim totalQty As Double = 0D
        Dim stockQty As Double = 0D
        Dim qtyLrgThnStock As Double = 0D
        Dim frstTotalQty As Double = 0D
        Dim scndTotalQty As Double = 0D

        Dim frstQty As Double = 0D
        Dim scndQty As Double = 0D
        Dim packQty As Double = 0D
        Dim qty As Double = 0D
        Dim totalQuantity As Double = 0D
        Dim dtGrid As DataTable = CType(Me.grd.DataSource, DataTable)
        dtGrid.AcceptChanges()
        Dim drNewItem As DataRow
        If Me.cmbUnit.Text = "Pack" Then
            totalQuantity = Val(Me.txtQty.Text)
        Else
            totalQuantity = Math.Round(Val(Me.txtTotalQuantity.Text), TotalAmountRounding)
        End If

        If Me.cmbUnit.Text = "Pack" Then
            totalQuantity = Val(Me.txtQty.Text)
        Else
            totalQuantity = Math.Round(Val(Me.txtTotalQuantity.Text), TotalAmountRounding)
        End If
        'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
        Dim flgAddtionalItem As Boolean = False

        If Val(totalQuantity) > Val(Me.txtStock.Text) AndAlso Val(Me.txtStock.Text) > 0 Then
            If Me.cmbUnit.Text = "Pack" Then
                qty = Math.Round(Val(Me.txtTotalQuantity.Text) - (Val(Me.txtStock.Text) * Val(Me.txtPackQty.Text)), TotalAmountRounding)
                qtyLrgThnStock = Math.Round(Val(Me.txtTotalQuantity.Text) - (Val(Me.txtStock.Text) * Val(Me.txtPackQty.Text)), TotalAmountRounding)
            Else
                qty = Math.Round(Val(Me.txtTotalQuantity.Text) - Val(Me.txtStock.Text), TotalAmountRounding)
                qtyLrgThnStock = Math.Round(Val(Me.txtTotalQuantity.Text) - Val(Me.txtStock.Text), TotalAmountRounding)
            End If
            'qty = Val(Me.txtTotalQuantity.Text) - Val(Me.txtStock.Text)
            'qtyLrgThnStock = Val(Me.txtTotalQuantity.Text) - Val(Me.txtStock.Text)
            frstTotalQty = Math.Round(Val(Me.txtTotalQuantity.Text) - qtyLrgThnStock, TotalAmountRounding)
            scndTotalQty = qtyLrgThnStock
            packQty = Val(Me.txtPackQty.Text)
            frstQty = frstTotalQty / packQty
            scndQty = scndTotalQty / packQty
            qty1 = Val(Me.txtQty.Text)
            stockQty = Val(Me.txtStock.Text)
            Me.cmbItem.ActiveRow.Cells("Stock").Value = 0
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            If IsEditMode = False AndAlso flgSOSeparateClosure = True Then
                If msg_Confirm("Do you want to add new row for qty more than stock?") = True Then
                    flgAddtionalItem = True
                Else
                    flgAddtionalItem = False
                End If
            End If
        ElseIf Val(Me.txtStock.Text) > 0 Then
            Me.cmbItem.ActiveRow.Cells("Stock").Value = Math.Round(Me.cmbItem.ActiveRow.Cells("Stock").Value - Val(Me.txtTotalQuantity.Text), TotalAmountRounding)
        Else
            qty = Val(Me.txtQty.Text)
        End If
        If Val(OrderQty) = Val(0) Then
            OrderQty = qty
        End If
        If msg_Confirm("Do you want to load Segregated Qty?") = False Then
            If StockChecked = True Then

                'grd.AddItem(cmbCategory.Text, cmbItem.ActiveRow.Cells("Item").Value.ToString, Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value, cmbUnit.Text, IIf(Val(Me.txtStock.Text) < Val(txtQty.Text), Me.txtStock.Text, txtQty.Text), txtRate.Text, Val(txtTotal.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, "0", "0", Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue, 0)
                drNewItem = dtGrid.NewRow()
                'drNewItem(EnumGridDetail.Category) = cmbCategory.Text
                drNewItem(EnumGridDetail.LocationID) = IIf(Me.cmbCategory.SelectedIndex = -1, 0, cmbCategory.SelectedValue)
                drNewItem(EnumGridDetail.ArticleCode) = cmbItem.ActiveRow.Cells("Code").Text.ToString
                drNewItem(EnumGridDetail.Item) = cmbItem.ActiveRow.Cells("Item").Value.ToString
                drNewItem(EnumGridDetail.BatchNo) = "xxxx"  'String.Empty 'Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value.ToString ''TFS1596
                drNewItem(EnumGridDetail.ExpiryDate) = Me.SOExpiryDate  'Convert.ToDateTime(Date.Now.AddMonths(1)) ''TFS4181
                drNewItem("Origin") = Me.SOOrigin
                drNewItem(EnumGridDetail.Unit) = IIf(cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
                drNewItem(EnumGridDetail.OrderQty) = OrderQty
                drNewItem(EnumGridDetail.DeliverQty) = DeliverQty
                drNewItem(EnumGridDetail.Qty) = Val(txtQty.Text) ''IIf(Val(Me.txtStock.Text) < Val(txtQty.Text), Me.txtStock.Text, txtQty.Text)
                drNewItem(EnumGridDetail.RemainingQty) = RemainingQty
                drNewItem(EnumGridDetail.Price) = IIf(txtRate.Text.Trim = "", 0, txtRate.Text)
                ''Start TFS2827
                If Me.cmbDiscountType.Text.Equals(DiscountType_Percentage) AndAlso Val(txtDisc.Text) > 0 Then
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                ElseIf Me.cmbDiscountType.Text.Equals(DiscountType_Flat) AndAlso Val(txtDisc.Text) > 0 Then
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                Else
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountFactor) = 0   'TFS2827
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                End If
                drNewItem(EnumGridDetail.PostDiscountPrice) = Val(txtPDP.Text)   'TFS2827
                ''EndTFS2827
                'drNewItem(EnumGridDetail.Total) = Val(txtTotal.Text)
                'drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue
                drNewItem(EnumGridDetail.ArticleID) = cmbItem.ActiveRow.Cells(0).Value
                drNewItem(EnumGridDetail.PackQty) = Val(Me.txtPackQty.Text)
                drNewItem(EnumGridDetail.CurrentPrice) = Me.cmbItem.ActiveRow.Cells("Price").Value
                drNewItem(EnumGridDetail.PackPrice) = Val(txtPackRate.Text)
                drNewItem(EnumGridDetail.DeliveryDetailID) = 0
                drNewItem(EnumGridDetail.SalesOrderDetailId) = SalesOrderDetailId ''TASK-408
                drNewItem(EnumGridDetail.SavedQty) = 0
                drNewItem(EnumGridDetail.BatchID) = 0 'Me.cmbBatchNo.Value
                drNewItem(EnumGridDetail.TradePrice) = TradePrice
                drNewItem(EnumGridDetail.Tax) = DeliveryTax_Percentage '0
                drNewItem(EnumGridDetail.SED) = _dblSEDTaxPercent
                drNewItem(EnumGridDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
                drNewItem(EnumGridDetail.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
                'drNewItem(EnumGridDetail.ServiceQty) = Val(Me.txtServiceQty.Text)
                drNewItem(EnumGridDetail.SampleQty) = SchemeQty
                drNewItem(EnumGridDetail.Discount_Percentage) = Val(txtDisc.Text) 'Discount_Percentage
                drNewItem(EnumGridDetail.Freight) = Freight
                drNewItem(EnumGridDetail.MarketReturns) = MarketReturns
                drNewItem(EnumGridDetail.SO_ID) = Val(Me.cmbPo.SelectedValue)
                drNewItem(EnumGridDetail.UM) = Me.cmbUM.Text.Replace("'", "''")
                drNewItem(EnumGridDetail.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
                drNewItem(EnumGridDetail.Pack_Desc) = Me.cmbUnit.Text.ToString
                drNewItem(EnumGridDetail.Stock) = Val(Me.txtStock.Text)
                drNewItem(EnumGridDetail.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
                drNewItem(EnumGridDetail.OtherComments) = IIf(strOtherComments = String.Empty, Me.cmbColour.Text, strOtherComments)
                drNewItem(EnumGridDetail.Comments) = strComments
                drNewItem(EnumGridDetail.Gross_Weights) = 0
                drNewItem(EnumGridDetail.Tray_Weights) = 0
                drNewItem(EnumGridDetail.Net_Weight) = 0
                drNewItem(EnumGridDetail.TotalQty) = Val(Me.txtTotalQuantity.Text)

                drNewItem(EnumGridDetail.CurrencyId) = Me.cmbCurrency.SelectedValue
                If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    drNewItem(EnumGridDetail.CurrencyAmount) = Val(0)
                Else
                    drNewItem(EnumGridDetail.CurrencyAmount) = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text), TotalAmountRounding)
                End If
                drNewItem(EnumGridDetail.CurrencyRate) = Val(Me.txtCurrencyRate.Text)
                Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
                If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                    drNewItem(EnumGridDetail.BaseCurrencyId) = Val(ConfigCurrencyVal)
                    drNewItem(EnumGridDetail.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
                End If
                drNewItem(EnumGridDetail.LogicalItem) = Me.cmbItem.ActiveRow.Cells("LogicalItem").Value
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                drNewItem(EnumGridDetail.AdditionalItem) = 0


                dtGrid.Rows.Add(drNewItem)
            End If
            If Val(Me.txtStock.Text) > 0 AndAlso Val(totalQuantity) > Val(Me.txtStock.Text) Then

                'grd.AddItem(cmbCategory.Text, cmbItem.ActiveRow.Cells("Item").Value.ToString, Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value, cmbUnit.Text, IIf(Val(Me.txtStock.Text) < Val(txtQty.Text), Me.txtStock.Text, txtQty.Text), txtRate.Text, Val(txtTotal.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, "0", "0", Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue, 0)
                drNewItem = dtGrid.NewRow()
                'drNewItem(EnumGridDetail.Category) = cmbCategory.Text
                drNewItem(EnumGridDetail.LocationID) = IIf(Me.cmbCategory.SelectedIndex = -1, 0, cmbCategory.SelectedValue)
                drNewItem(EnumGridDetail.ArticleCode) = cmbItem.ActiveRow.Cells("Code").Text.ToString
                drNewItem(EnumGridDetail.Item) = cmbItem.ActiveRow.Cells("Item").Value.ToString
                drNewItem(EnumGridDetail.BatchNo) = "xxxx"  'String.Empty 'Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value.ToString
                drNewItem(EnumGridDetail.ExpiryDate) = Me.SOExpiryDate  'Convert.ToDateTime(Date.Now.AddMonths(1)) ''TFS4181
                drNewItem("Origin") = Me.SOOrigin
                drNewItem(EnumGridDetail.Unit) = IIf(cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
                drNewItem(EnumGridDetail.OrderQty) = OrderQty
                drNewItem(EnumGridDetail.DeliverQty) = DeliverQty
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                If flgAddtionalItem = True AndAlso flgSOSeparateClosure = True Then
                    drNewItem(EnumGridDetail.Qty) = frstQty
                ElseIf flgAddtionalItem = False AndAlso flgSOSeparateClosure = False Then
                    drNewItem(EnumGridDetail.Qty) = frstQty
                Else
                    drNewItem(EnumGridDetail.Qty) = Val(txtQty.Text)
                End If
                ''IIf(Val(Me.txtStock.Text) < Val(txtQty.Text), Me.txtStock.Text, txtQty.Text)
                drNewItem(EnumGridDetail.RemainingQty) = RemainingQty
                drNewItem(EnumGridDetail.Price) = IIf(txtRate.Text.Trim = "", 0, txtRate.Text)
                ''Start TFS2827
                If Me.cmbDiscountType.Text.Equals(DiscountType_Percentage) AndAlso Val(txtDisc.Text) > 0 Then
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                ElseIf Me.cmbDiscountType.Text.Equals(DiscountType_Flat) AndAlso Val(txtDisc.Text) > 0 Then
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                Else
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountFactor) = 0   'TFS2827
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                End If
                drNewItem(EnumGridDetail.PostDiscountPrice) = Val(txtPDP.Text)   'TFS2827
                ''EndTFS2827

                'drNewItem(EnumGridDetail.Total) = Val(txtTotal.Text)
                'drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue
                drNewItem(EnumGridDetail.ArticleID) = cmbItem.ActiveRow.Cells(0).Value
                drNewItem(EnumGridDetail.PackQty) = Val(Me.txtPackQty.Text)
                drNewItem(EnumGridDetail.CurrentPrice) = Me.cmbItem.ActiveRow.Cells("Price").Value
                drNewItem(EnumGridDetail.PackPrice) = Val(Me.txtPackRate.Text)
                drNewItem(EnumGridDetail.DeliveryDetailID) = 0
                drNewItem(EnumGridDetail.SalesOrderDetailId) = SalesOrderDetailId ''TASK-408
                drNewItem(EnumGridDetail.SavedQty) = 0
                drNewItem(EnumGridDetail.BatchID) = 0 'Me.cmbBatchNo.Value
                drNewItem(EnumGridDetail.TradePrice) = TradePrice
                drNewItem(EnumGridDetail.Tax) = DeliveryTax_Percentage '0
                drNewItem(EnumGridDetail.SED) = _dblSEDTaxPercent
                drNewItem(EnumGridDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
                drNewItem(EnumGridDetail.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
                'drNewItem(EnumGridDetail.ServiceQty) = Val(Me.txtServiceQty.Text)
                drNewItem(EnumGridDetail.SampleQty) = SchemeQty
                drNewItem(EnumGridDetail.Discount_Percentage) = Val(txtDisc.Text) 'Discount_Percentage
                drNewItem(EnumGridDetail.Freight) = Freight
                drNewItem(EnumGridDetail.MarketReturns) = MarketReturns
                drNewItem(EnumGridDetail.SO_ID) = Val(Me.cmbPo.SelectedValue)
                drNewItem(EnumGridDetail.UM) = Me.cmbUM.Text.Replace("'", "''")
                drNewItem(EnumGridDetail.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
                drNewItem(EnumGridDetail.Pack_Desc) = Me.cmbUnit.Text.ToString
                drNewItem(EnumGridDetail.Stock) = Val(Me.txtStock.Text)
                drNewItem(EnumGridDetail.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
                drNewItem(EnumGridDetail.OtherComments) = IIf(strOtherComments = String.Empty, Me.cmbColour.Text, strOtherComments)
                drNewItem(EnumGridDetail.Comments) = strComments
                drNewItem(EnumGridDetail.Gross_Weights) = 0
                drNewItem(EnumGridDetail.Tray_Weights) = 0
                drNewItem(EnumGridDetail.Net_Weight) = 0
                drNewItem(EnumGridDetail.TotalQty) = frstTotalQty ''Val(Me.txtTotalQuantity.Text)
                ''27-03-2017
                drNewItem(EnumGridDetail.CurrencyId) = Me.cmbCurrency.SelectedValue
                If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    drNewItem(EnumGridDetail.CurrencyAmount) = Val(0)
                Else
                    drNewItem(EnumGridDetail.CurrencyAmount) = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text), TotalAmountRounding)
                End If
                drNewItem(EnumGridDetail.CurrencyRate) = Val(Me.txtCurrencyRate.Text)
                Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
                If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                    drNewItem(EnumGridDetail.BaseCurrencyId) = Val(ConfigCurrencyVal)
                    drNewItem(EnumGridDetail.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
                End If

                drNewItem(EnumGridDetail.CurrencyId) = Me.cmbCurrency.SelectedValue
                If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    drNewItem(EnumGridDetail.CurrencyAmount) = Val(0)
                Else
                    drNewItem(EnumGridDetail.CurrencyAmount) = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text), TotalAmountRounding)
                End If
                drNewItem(EnumGridDetail.LogicalItem) = Me.cmbItem.ActiveRow.Cells("LogicalItem").Value
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                drNewItem(EnumGridDetail.AdditionalItem) = 0
                dtGrid.Rows.Add(drNewItem)
            End If
            If Val(Me.txtStock.Text) > 0 AndAlso Val(totalQuantity) <= Val(Me.txtStock.Text) Then
                'grd.AddItem(cmbCategory.Text, cmbItem.ActiveRow.Cells("Item").Value.ToString, Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value, cmbUnit.Text, IIf(Val(Me.txtStock.Text) < Val(txtQty.Text), Me.txtStock.Text, txtQty.Text), txtRate.Text, Val(txtTotal.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, "0", "0", Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue, 0)
                drNewItem = dtGrid.NewRow()
                'drNewItem(EnumGridDetail.Category) = cmbCategory.Text
                drNewItem(EnumGridDetail.LocationID) = IIf(Me.cmbCategory.SelectedIndex = -1, 0, cmbCategory.SelectedValue)
                drNewItem(EnumGridDetail.ArticleCode) = cmbItem.ActiveRow.Cells("Code").Text.ToString
                drNewItem(EnumGridDetail.Item) = cmbItem.ActiveRow.Cells("Item").Value.ToString
                drNewItem(EnumGridDetail.BatchNo) = "xxxx"  'String.Empty 'Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value.ToString
                drNewItem(EnumGridDetail.ExpiryDate) = Me.SOExpiryDate  'Convert.ToDateTime(Date.Now.AddMonths(1)) ''TFS4181
                drNewItem("Origin") = Me.SOOrigin
                drNewItem(EnumGridDetail.Unit) = IIf(cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
                drNewItem(EnumGridDetail.OrderQty) = OrderQty
                drNewItem(EnumGridDetail.DeliverQty) = DeliverQty
                drNewItem(EnumGridDetail.Qty) = IIf(Val(Me.txtStock.Text) < Val(txtQty.Text), Me.txtStock.Text, txtQty.Text)
                drNewItem(EnumGridDetail.RemainingQty) = RemainingQty
                drNewItem(EnumGridDetail.Price) = IIf(txtRate.Text.Trim = "", 0, txtRate.Text)
                ''Start TFS2827
                If Me.cmbDiscountType.Text.Equals(DiscountType_Percentage) AndAlso Val(txtDisc.Text) > 0 Then
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                ElseIf Me.cmbDiscountType.Text.Equals(DiscountType_Flat) AndAlso Val(txtDisc.Text) > 0 Then
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                Else
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountFactor) = 0   'TFS2827
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                End If
                drNewItem(EnumGridDetail.PostDiscountPrice) = Val(txtPDP.Text)   'TFS2827
                ''EndTFS2827
                'drNewItem(EnumGridDetail.Total) = Val(txtTotal.Text)
                'drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue
                drNewItem(EnumGridDetail.ArticleID) = cmbItem.ActiveRow.Cells(0).Value
                drNewItem(EnumGridDetail.PackQty) = Val(Me.txtPackQty.Text)
                drNewItem(EnumGridDetail.CurrentPrice) = Me.cmbItem.ActiveRow.Cells("Price").Value
                drNewItem(EnumGridDetail.PackPrice) = Val(Me.txtPackRate.Text)
                drNewItem(EnumGridDetail.DeliveryDetailID) = 0
                drNewItem(EnumGridDetail.SalesOrderDetailId) = SalesOrderDetailId ''TASK-408
                drNewItem(EnumGridDetail.SavedQty) = 0
                drNewItem(EnumGridDetail.BatchID) = 0 'Me.cmbBatchNo.Value
                drNewItem(EnumGridDetail.TradePrice) = TradePrice
                drNewItem(EnumGridDetail.Tax) = DeliveryTax_Percentage '0
                drNewItem(EnumGridDetail.SED) = _dblSEDTaxPercent
                drNewItem(EnumGridDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
                drNewItem(EnumGridDetail.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
                'drNewItem(EnumGridDetail.ServiceQty) = Val(Me.txtServiceQty.Text)
                drNewItem(EnumGridDetail.SampleQty) = SchemeQty
                drNewItem(EnumGridDetail.Discount_Percentage) = Val(txtDisc.Text) 'Discount_Percentage
                drNewItem(EnumGridDetail.Freight) = Freight
                drNewItem(EnumGridDetail.MarketReturns) = MarketReturns
                drNewItem(EnumGridDetail.SO_ID) = Val(Me.cmbPo.SelectedValue)
                drNewItem(EnumGridDetail.UM) = Me.cmbUM.Text.Replace("'", "''")
                drNewItem(EnumGridDetail.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
                drNewItem(EnumGridDetail.Pack_Desc) = Me.cmbUnit.Text.ToString
                drNewItem(EnumGridDetail.Stock) = Val(Me.txtStock.Text)
                drNewItem(EnumGridDetail.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
                drNewItem(EnumGridDetail.OtherComments) = IIf(strOtherComments = String.Empty, Me.cmbColour.Text, strOtherComments)
                drNewItem(EnumGridDetail.Comments) = strComments
                drNewItem(EnumGridDetail.Gross_Weights) = 0
                drNewItem(EnumGridDetail.Tray_Weights) = 0
                drNewItem(EnumGridDetail.Net_Weight) = 0
                drNewItem(EnumGridDetail.TotalQty) = Val(Me.txtTotalQuantity.Text)

                drNewItem(EnumGridDetail.CurrencyId) = Me.cmbCurrency.SelectedValue
                If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    drNewItem(EnumGridDetail.CurrencyAmount) = Val(0)
                Else
                    drNewItem(EnumGridDetail.CurrencyAmount) = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text), TotalAmountRounding)
                End If
                drNewItem(EnumGridDetail.CurrencyRate) = Val(Me.txtCurrencyRate.Text)
                Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
                If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                    drNewItem(EnumGridDetail.BaseCurrencyId) = Val(ConfigCurrencyVal)
                    drNewItem(EnumGridDetail.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
                End If
                drNewItem(EnumGridDetail.LogicalItem) = Me.cmbItem.ActiveRow.Cells("LogicalItem").Value
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                drNewItem(EnumGridDetail.AdditionalItem) = 0
                dtGrid.Rows.Add(drNewItem)
            End If

            'If Val(Me.txtQty.Text) > Val(Me.txtStock.Text) AndAlso Val(Me.txtStock.Text) > 0 Then
            '    qty = Val(Me.txtQty.Text) - Val(Me.txtStock.Text)
            '    Me.cmbItem.ActiveRow.Cells("Stock").Value = 0
            'ElseIf Val(Me.txtStock.Text) > 0 Then
            '    Me.cmbItem.ActiveRow.Cells("Stock").Value = Me.cmbItem.ActiveRow.Cells("Stock").Value - Val(Me.txtQty.Text)
            'Else
            '    qty = Val(Me.txtQty.Text)
            'End If



            'If Me.cmbBatchNo.ActiveRow.Cells("Stock").Value < Val(Me.txtQty.Text) Then
            '    Dim dt As DataTable = CType(Me.cmbBatchNo.DataSource, DataTable)
            '    For i As Integer = 0 To dt.Rows.Count - 2
            '        If Me.cmbBatchNo.ActiveRow.Index <> i Then
            '            If dt.Rows(i)("Stock") > 0 And dt.Rows(i)("Stock") < qty Then
            '                'grd.AddItem(cmbCategory.Text, cmbItem.ActiveRow.Cells("Item").Value.ToString, dt.Rows(i)("Batch No").ToString, cmbUnit.Text, dt.Rows(i)("Stock"), txtRate.Text, Val(dt.Rows(i)("Stock")) * Val(txtRate.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, "0", "0", Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue, 0)
            '                drNewItem = dtGrid.NewRow()
            '                'drNewItem(EnumGridDetail.Category) = cmbCategory.Text
            '                drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue
            '                drNewItem(EnumGridDetail.ArticleCode) = cmbItem.ActiveRow.Cells("Code").Text.ToString
            '                drNewItem(EnumGridDetail.Item) = cmbItem.ActiveRow.Cells("Item").Value.ToString
            '                drNewItem(EnumGridDetail.BatchNo) = String.Empty 'Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value
            '                drNewItem(EnumGridDetail.Unit) = cmbUnit.Text
            '                drNewItem(EnumGridDetail.Qty) = qty
            '                drNewItem(EnumGridDetail.Price) = IIf(txtRate.Text.Trim = "", 0, txtRate.Text)
            '                'drNewItem(EnumGridDetail.Total) = Val(txtTotal.Text)
            '                'drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue
            '                drNewItem(EnumGridDetail.ArticleID) = cmbItem.ActiveRow.Cells(0).Value
            '                drNewItem(EnumGridDetail.PackQty) = Val(Me.txtPackQty.Text)
            '                drNewItem(EnumGridDetail.CurrentPrice) = Me.cmbItem.ActiveRow.Cells("Price").Value
            '                drNewItem(EnumGridDetail.DeliveryDetailID) = 0
            '                drNewItem(EnumGridDetail.SavedQty) = 0
            '                drNewItem(EnumGridDetail.BatchID) = 0 'Me.cmbBatchNo.Value
            '                drNewItem(EnumGridDetail.TradePrice) = TradePrice
            '                drNewItem(EnumGridDetail.Tax) = DeliveryTax_Percentage
            '                drNewItem(EnumGridDetail.SED) = 0
            '                drNewItem(EnumGridDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
            '                drNewItem(EnumGridDetail.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
            '                'drNewItem(EnumGridDetail.ServiceQty) = Val(Me.txtServiceQty.Text)
            '                drNewItem(EnumGridDetail.SampleQty) = SchemeQty
            '                drNewItem(EnumGridDetail.Discount_Percentage) = Val(txtDisc.Text) '0Discount_Percentage
            '                drNewItem(EnumGridDetail.Freight) = Freight
            '                drNewItem(EnumGridDetail.MarketReturns) = MarketReturns
            '                drNewItem(EnumGridDetail.SO_ID) = Val(Me.cmbPo.SelectedValue)
            '                drNewItem(EnumGridDetail.UM) = Me.cmbUM.Text.Replace("'", "''")
            '                drNewItem(EnumGridDetail.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
            '                dtGrid.Rows.Add(drNewItem)
            '                qty = qty - dt.Rows(i)("Stock")
            '                dt.Rows(i)("Stock") = 0
            '            ElseIf dt.Rows(i)("Stock") > 0 Then
            '                'grd.AddItem(cmbCategory.Text, Me.cmbItem.ActiveRow.Cells("Code").Text, cmbItem.ActiveRow.Cells("Item").Value.ToString, dt.Rows(i)("Batch No").ToString, cmbUnit.Text, qty, txtRate.Text, qty * Val(txtRate.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, "0", "0", Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue, 0)
            '                'grd.AddItem(cmbCategory.Text, Me.cmbItem.ActiveRow.Cells("Code").Text, cmbItem.ActiveRow.Cells("Item").Value.ToString, dt.Rows(i)("Batch No").ToString, cmbUnit.Text, qty, txtRate.Text, qty * Val(txtRate.Text), cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, "0", "0", Me.cmbBatchNo.Value)
            '                drNewItem = dtGrid.NewRow()
            '                'drNewItem(EnumGridDetail.Category) = cmbCategory.Text
            '                drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue
            '                drNewItem(EnumGridDetail.ArticleCode) = cmbItem.ActiveRow.Cells("Code").Text.ToString
            '                drNewItem(EnumGridDetail.Item) = cmbItem.ActiveRow.Cells("Item").Value.ToString
            '                drNewItem(EnumGridDetail.BatchNo) = String.Empty 'Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value
            '                drNewItem(EnumGridDetail.Unit) = cmbUnit.Text
            '                drNewItem(EnumGridDetail.Qty) = qty
            '                drNewItem(EnumGridDetail.Price) = IIf(txtRate.Text.Trim = "", 0, txtRate.Text)
            '                'drNewItem(EnumGridDetail.Total) = Val(txtTotal.Text)
            '                'drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue
            '                drNewItem(EnumGridDetail.ArticleID) = cmbItem.ActiveRow.Cells(0).Value
            '                drNewItem(EnumGridDetail.PackQty) = Val(Me.txtPackQty.Text)
            '                drNewItem(EnumGridDetail.CurrentPrice) = Me.cmbItem.ActiveRow.Cells("Price").Value
            '                drNewItem(EnumGridDetail.DeliveryDetailID) = 0
            '                drNewItem(EnumGridDetail.SavedQty) = 0
            '                drNewItem(EnumGridDetail.BatchID) = 0 'Me.cmbBatchNo.Value
            '                drNewItem(EnumGridDetail.TradePrice) = TradePrice
            '                drNewItem(EnumGridDetail.Tax) = DeliveryTax_Percentage
            '                drNewItem(EnumGridDetail.SED) = 0
            '                drNewItem(EnumGridDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
            '                drNewItem(EnumGridDetail.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
            '                'drNewItem(EnumGridDetail.ServiceQty) = Val(Me.txtServiceQty.Text)
            '                drNewItem(EnumGridDetail.SampleQty) = SchemeQty
            '                drNewItem(EnumGridDetail.Discount_Percentage) = Val(txtDisc.Text) 'Discount_Percentage
            '                drNewItem(EnumGridDetail.Freight) = Freight
            '                drNewItem(EnumGridDetail.MarketReturns) = MarketReturns
            '                drNewItem(EnumGridDetail.SO_ID) = Val(Me.cmbPo.SelectedValue)
            '                drNewItem(EnumGridDetail.UM) = Me.cmbUM.Text.Replace("'", "''")
            '                drNewItem(EnumGridDetail.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
            '                dtGrid.Rows.Add(drNewItem)
            '                dt.Rows(i)("Stock") = dt.Rows(i)("Stock") - qty
            '                qty = 0
            '            End If
            '        End If
            '    Next


            If qty > 0 AndAlso qtyLrgThnStock <= 0 Then
                '    'grd.AddItem(cmbCategory.Text, cmbItem.ActiveRow.Cells("Item").Value.ToString, dt.Rows(dt.Rows.Count - 1)("Batch No").ToString, cmbUnit.Text, qty, txtRate.Text, IIf(cmbUnit.Text = "Pack", qty * Val(txtRate.Text) * Val(txtPackQty.Text), qty * Val(txtRate.Text)), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, "0", "0", Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue, 0)
                drNewItem = dtGrid.NewRow()
                'drNewItem(EnumGridDetail.Category) = cmbCategory.Text
                drNewItem(EnumGridDetail.LocationID) = IIf(cmbCategory.SelectedIndex = -1, 0, cmbCategory.SelectedValue)
                drNewItem(EnumGridDetail.ArticleCode) = cmbItem.ActiveRow.Cells("Code").Text.ToString
                drNewItem(EnumGridDetail.Item) = cmbItem.ActiveRow.Cells("Item").Value.ToString
                drNewItem(EnumGridDetail.BatchNo) = "xxxx"  'String.Empty 'Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value.ToString
                drNewItem(EnumGridDetail.ExpiryDate) = Me.SOExpiryDate  'Convert.ToDateTime(Date.Now.AddMonths(1)) ''TFS4181
                drNewItem("Origin") = Me.SOOrigin
                drNewItem(EnumGridDetail.Unit) = IIf(cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString) 'cmbUnit.Text
                drNewItem(EnumGridDetail.OrderQty) = OrderQty
                drNewItem(EnumGridDetail.DeliverQty) = DeliverQty
                drNewItem(EnumGridDetail.Qty) = qty
                drNewItem(EnumGridDetail.RemainingQty) = RemainingQty
                drNewItem(EnumGridDetail.Price) = IIf(txtRate.Text.Trim = "", 0, txtRate.Text)

                ''Start TFS2827
                If Me.cmbDiscountType.Text.Equals(DiscountType_Percentage) AndAlso Val(txtDisc.Text) > 0 Then
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                ElseIf Me.cmbDiscountType.Text.Equals(DiscountType_Flat) AndAlso Val(txtDisc.Text) > 0 Then
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                Else
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountFactor) = 0   'TFS2827
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                End If
                drNewItem(EnumGridDetail.PostDiscountPrice) = Val(txtPDP.Text)   'TFS2827
                ''EndTFS2827

                'drNewItem(EnumGridDetail.Total) = Val(txtTotal.Text)
                'drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue
                drNewItem(EnumGridDetail.ArticleID) = cmbItem.ActiveRow.Cells(0).Value
                drNewItem(EnumGridDetail.PackQty) = Val(Me.txtPackQty.Text)
                drNewItem(EnumGridDetail.CurrentPrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value)
                drNewItem(EnumGridDetail.PackPrice) = Val(Me.txtPackRate.Text)
                drNewItem(EnumGridDetail.DeliveryDetailID) = 0
                drNewItem(EnumGridDetail.SalesOrderDetailId) = SalesOrderDetailId ''TASK-408
                drNewItem(EnumGridDetail.SavedQty) = 0
                drNewItem(EnumGridDetail.BatchID) = 0 'Me.cmbBatchNo.Value
                drNewItem(EnumGridDetail.TradePrice) = TradePrice
                drNewItem(EnumGridDetail.Tax) = DeliveryTax_Percentage
                drNewItem(EnumGridDetail.SED) = 0
                drNewItem(EnumGridDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
                drNewItem(EnumGridDetail.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
                'drNewItem(EnumGridDetail.ServiceQty) = Val(Me.txtServiceQty.Text)
                drNewItem(EnumGridDetail.SampleQty) = SchemeQty
                drNewItem(EnumGridDetail.Discount_Percentage) = Val(txtDisc.Text)
                drNewItem(EnumGridDetail.Freight) = Freight
                drNewItem(EnumGridDetail.MarketReturns) = MarketReturns
                drNewItem(EnumGridDetail.SO_ID) = Val(Me.cmbPo.SelectedValue)
                drNewItem(EnumGridDetail.UM) = Me.cmbUM.Text.Replace("'", "''")
                drNewItem(EnumGridDetail.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
                drNewItem(EnumGridDetail.Pack_Desc) = Me.cmbUnit.Text.ToString
                drNewItem(EnumGridDetail.Stock) = Val(Me.txtStock.Text)
                drNewItem(EnumGridDetail.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
                drNewItem(EnumGridDetail.OtherComments) = strOtherComments
                drNewItem(EnumGridDetail.Comments) = strComments
                drNewItem(EnumGridDetail.TotalQty) = Val(Me.txtTotalQuantity.Text)

                drNewItem(EnumGridDetail.CurrencyId) = Me.cmbCurrency.SelectedValue
                If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    drNewItem(EnumGridDetail.CurrencyAmount) = Val(0)
                Else
                    drNewItem(EnumGridDetail.CurrencyAmount) = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text), TotalAmountRounding)
                End If
                drNewItem(EnumGridDetail.CurrencyRate) = Val(Me.txtCurrencyRate.Text)
                Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
                If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                    drNewItem(EnumGridDetail.BaseCurrencyId) = Val(ConfigCurrencyVal)
                    drNewItem(EnumGridDetail.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
                End If
                drNewItem(EnumGridDetail.LogicalItem) = Me.cmbItem.ActiveRow.Cells("LogicalItem").Value
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                drNewItem(EnumGridDetail.AdditionalItem) = 0
                dtGrid.Rows.Add(drNewItem)
            End If

            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            If qty > 0 AndAlso qtyLrgThnStock > 0 AndAlso flgAddtionalItem = True AndAlso flgSOSeparateClosure = True Then
                '    'grd.AddItem(cmbCategory.Text, cmbItem.ActiveRow.Cells("Item").Value.ToString, dt.Rows(dt.Rows.Count - 1)("Batch No").ToString, cmbUnit.Text, qty, txtRate.Text, IIf(cmbUnit.Text = "Pack", qty * Val(txtRate.Text) * Val(txtPackQty.Text), qty * Val(txtRate.Text)), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, "0", "0", Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue, 0)
                drNewItem = dtGrid.NewRow()
                'drNewItem(EnumGridDetail.Category) = cmbCategory.Text
                drNewItem(EnumGridDetail.LocationID) = IIf(Me.cmbCategory.SelectedIndex = -1, 0, cmbCategory.SelectedValue)
                drNewItem(EnumGridDetail.ArticleCode) = cmbItem.ActiveRow.Cells("Code").Text.ToString
                drNewItem(EnumGridDetail.Item) = cmbItem.ActiveRow.Cells("Item").Value.ToString
                drNewItem(EnumGridDetail.BatchNo) = "xxxx"  'String.Empty 'Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value.ToString
                drNewItem(EnumGridDetail.ExpiryDate) = Me.SOExpiryDate  'Convert.ToDateTime(Date.Now.AddMonths(1)) ''TFS4181
                drNewItem("Origin") = Me.SOOrigin
                drNewItem(EnumGridDetail.Unit) = IIf(cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString) 'cmbUnit.Text
                drNewItem(EnumGridDetail.OrderQty) = OrderQty
                drNewItem(EnumGridDetail.DeliverQty) = DeliverQty
                drNewItem(EnumGridDetail.Qty) = scndQty ''qty
                drNewItem(EnumGridDetail.RemainingQty) = RemainingQty
                drNewItem(EnumGridDetail.Price) = IIf(txtRate.Text.Trim = "", 0, txtRate.Text)
                ''Start TFS2827
                If Me.cmbDiscountType.Text.Equals(DiscountType_Percentage) AndAlso Val(txtDisc.Text) > 0 Then
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                ElseIf Me.cmbDiscountType.Text.Equals(DiscountType_Flat) AndAlso Val(txtDisc.Text) > 0 Then
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                Else
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountFactor) = 0   'TFS2827
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                End If
                drNewItem(EnumGridDetail.PostDiscountPrice) = Val(txtPDP.Text)   'TFS2827
                ''EndTFS2827
                'drNewItem(EnumGridDetail.Total) = Val(txtTotal.Text)
                'drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue
                drNewItem(EnumGridDetail.ArticleID) = cmbItem.ActiveRow.Cells(0).Value
                drNewItem(EnumGridDetail.PackQty) = Val(Me.txtPackQty.Text)
                drNewItem(EnumGridDetail.CurrentPrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value)
                drNewItem(EnumGridDetail.PackPrice) = Val(Me.txtPackRate.Text)
                drNewItem(EnumGridDetail.DeliveryDetailID) = 0
                drNewItem(EnumGridDetail.SalesOrderDetailId) = SalesOrderDetailId ''TASK-408
                drNewItem(EnumGridDetail.SavedQty) = 0
                drNewItem(EnumGridDetail.BatchID) = 0 'Me.cmbBatchNo.Value
                drNewItem(EnumGridDetail.TradePrice) = TradePrice
                drNewItem(EnumGridDetail.Tax) = DeliveryTax_Percentage
                drNewItem(EnumGridDetail.SED) = 0
                drNewItem(EnumGridDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
                drNewItem(EnumGridDetail.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
                'drNewItem(EnumGridDetail.ServiceQty) = Val(Me.txtServiceQty.Text)
                drNewItem(EnumGridDetail.SampleQty) = SchemeQty
                drNewItem(EnumGridDetail.Discount_Percentage) = Val(txtDisc.Text)
                drNewItem(EnumGridDetail.Freight) = Freight
                drNewItem(EnumGridDetail.MarketReturns) = MarketReturns
                drNewItem(EnumGridDetail.SO_ID) = Val(Me.cmbPo.SelectedValue)
                drNewItem(EnumGridDetail.UM) = Me.cmbUM.Text.Replace("'", "''")
                drNewItem(EnumGridDetail.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
                drNewItem(EnumGridDetail.Pack_Desc) = Me.cmbUnit.Text.ToString
                drNewItem(EnumGridDetail.Stock) = Val(Me.txtStock.Text)
                drNewItem(EnumGridDetail.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
                drNewItem(EnumGridDetail.OtherComments) = strOtherComments
                drNewItem(EnumGridDetail.Comments) = strComments
                drNewItem(EnumGridDetail.TotalQty) = scndTotalQty ''Val(Me.txtTotalQuantity.Text)

                drNewItem(EnumGridDetail.CurrencyId) = Me.cmbCurrency.SelectedValue
                If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    drNewItem(EnumGridDetail.CurrencyAmount) = Val(0)
                Else
                    drNewItem(EnumGridDetail.CurrencyAmount) = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text), TotalAmountRounding)
                End If
                drNewItem(EnumGridDetail.CurrencyRate) = Val(Me.txtCurrencyRate.Text)
                Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
                If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                    drNewItem(EnumGridDetail.BaseCurrencyId) = Val(ConfigCurrencyVal)
                    drNewItem(EnumGridDetail.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
                End If

                drNewItem(EnumGridDetail.LogicalItem) = Me.cmbItem.ActiveRow.Cells("LogicalItem").Value
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                drNewItem(EnumGridDetail.AdditionalItem) = 1
                dtGrid.Rows.Add(drNewItem)
            End If
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            If qty > 0 AndAlso qtyLrgThnStock > 0 AndAlso flgAddtionalItem = False AndAlso flgSOSeparateClosure = False Then
                '    'grd.AddItem(cmbCategory.Text, cmbItem.ActiveRow.Cells("Item").Value.ToString, dt.Rows(dt.Rows.Count - 1)("Batch No").ToString, cmbUnit.Text, qty, txtRate.Text, IIf(cmbUnit.Text = "Pack", qty * Val(txtRate.Text) * Val(txtPackQty.Text), qty * Val(txtRate.Text)), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, "0", "0", Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue, 0)
                drNewItem = dtGrid.NewRow()
                'drNewItem(EnumGridDetail.Category) = cmbCategory.Text
                drNewItem(EnumGridDetail.LocationID) = IIf(Me.cmbCategory.SelectedIndex = -1, 0, cmbCategory.SelectedValue)
                drNewItem(EnumGridDetail.ArticleCode) = cmbItem.ActiveRow.Cells("Code").Text.ToString
                drNewItem(EnumGridDetail.Item) = cmbItem.ActiveRow.Cells("Item").Value.ToString
                drNewItem(EnumGridDetail.BatchNo) = "xxxx"  'String.Empty 'Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value.ToString
                drNewItem(EnumGridDetail.ExpiryDate) = Me.SOExpiryDate  'Convert.ToDateTime(Date.Now.AddMonths(1)) ''TFS4181
                drNewItem("Origin") = Me.SOOrigin
                drNewItem(EnumGridDetail.Unit) = IIf(cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString) 'cmbUnit.Text
                drNewItem(EnumGridDetail.OrderQty) = OrderQty
                drNewItem(EnumGridDetail.DeliverQty) = DeliverQty
                drNewItem(EnumGridDetail.Qty) = scndQty ''qty
                drNewItem(EnumGridDetail.RemainingQty) = RemainingQty
                drNewItem(EnumGridDetail.Price) = IIf(txtRate.Text.Trim = "", 0, txtRate.Text)
                ''Start TFS2827
                If Me.cmbDiscountType.Text.Equals(DiscountType_Percentage) AndAlso Val(txtDisc.Text) > 0 Then
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                ElseIf Me.cmbDiscountType.Text.Equals(DiscountType_Flat) AndAlso Val(txtDisc.Text) > 0 Then
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                Else
                    drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drNewItem(EnumGridDetail.DiscountFactor) = 0   'TFS2827
                    drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                End If
                drNewItem(EnumGridDetail.PostDiscountPrice) = Val(txtPDP.Text)   'TFS2827
                ''EndTFS2827
                'drNewItem(EnumGridDetail.Total) = Val(txtTotal.Text)
                'drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue
                drNewItem(EnumGridDetail.ArticleID) = cmbItem.ActiveRow.Cells(0).Value
                drNewItem(EnumGridDetail.PackQty) = Val(Me.txtPackQty.Text)
                drNewItem(EnumGridDetail.CurrentPrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value)
                drNewItem(EnumGridDetail.PackPrice) = Val(Me.txtPackRate.Text)
                drNewItem(EnumGridDetail.DeliveryDetailID) = 0
                drNewItem(EnumGridDetail.SalesOrderDetailId) = SalesOrderDetailId ''TASK-408
                drNewItem(EnumGridDetail.SavedQty) = 0
                drNewItem(EnumGridDetail.BatchID) = 0 'Me.cmbBatchNo.Value
                drNewItem(EnumGridDetail.TradePrice) = TradePrice
                drNewItem(EnumGridDetail.Tax) = DeliveryTax_Percentage
                drNewItem(EnumGridDetail.SED) = 0
                drNewItem(EnumGridDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
                drNewItem(EnumGridDetail.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
                'drNewItem(EnumGridDetail.ServiceQty) = Val(Me.txtServiceQty.Text)
                drNewItem(EnumGridDetail.SampleQty) = SchemeQty
                drNewItem(EnumGridDetail.Discount_Percentage) = Val(txtDisc.Text)
                drNewItem(EnumGridDetail.Freight) = Freight
                drNewItem(EnumGridDetail.MarketReturns) = MarketReturns
                drNewItem(EnumGridDetail.SO_ID) = Val(Me.cmbPo.SelectedValue)
                drNewItem(EnumGridDetail.UM) = Me.cmbUM.Text.Replace("'", "''")
                drNewItem(EnumGridDetail.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
                drNewItem(EnumGridDetail.Pack_Desc) = Me.cmbUnit.Text.ToString
                drNewItem(EnumGridDetail.Stock) = Val(Me.txtStock.Text)
                drNewItem(EnumGridDetail.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
                drNewItem(EnumGridDetail.OtherComments) = strOtherComments
                drNewItem(EnumGridDetail.Comments) = strComments
                drNewItem(EnumGridDetail.TotalQty) = scndTotalQty ''Val(Me.txtTotalQuantity.Text)

                drNewItem(EnumGridDetail.CurrencyId) = Me.cmbCurrency.SelectedValue
                If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    drNewItem(EnumGridDetail.CurrencyAmount) = Val(0)
                Else
                    drNewItem(EnumGridDetail.CurrencyAmount) = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text), TotalAmountRounding)
                End If
                drNewItem(EnumGridDetail.CurrencyRate) = Val(Me.txtCurrencyRate.Text)
                Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
                If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                    drNewItem(EnumGridDetail.BaseCurrencyId) = Val(ConfigCurrencyVal)
                    drNewItem(EnumGridDetail.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
                End If

                drNewItem(EnumGridDetail.LogicalItem) = Me.cmbItem.ActiveRow.Cells("LogicalItem").Value
                drNewItem(EnumGridDetail.AdditionalItem) = 1
                dtGrid.Rows.Add(drNewItem)
            End If
            'End If

        Else
            'If qtyLrgThnStock > 0 Or flgAddtionalItem = True Then
            '    ShowErrorMessage("Stock is negative against item " & cmbItem.ActiveRow.Cells("Code").Text.ToString & " ")
            '    Exit Sub
            'End If
            Dim checkqty As Integer = txtQty.Text
            For strQty As Integer = 1 To checkqty
                Dim drGrd As DataRow
                drGrd = dtGrid.NewRow
                'grd.AddItem(cmbCategory.Text, cmbItem.ActiveRow.Cells("Item").Value.ToString, Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value, cmbUnit.Text, IIf(Val(Me.txtStock.Text) < Val(txtQty.Text), Me.txtStock.Text, txtQty.Text), txtRate.Text, Val(txtTotal.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, "0", "0", Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue, 0)
                ''drNewItem = dtGrid.NewRow()
                'drNewItem(EnumGridDetail.Category) = cmbCategory.Text
                drGrd(EnumGridDetail.LocationID) = IIf(Me.cmbCategory.SelectedIndex = -1, 0, cmbCategory.SelectedValue)
                drGrd(EnumGridDetail.ArticleCode) = cmbItem.ActiveRow.Cells("Code").Text.ToString
                drGrd(EnumGridDetail.Item) = cmbItem.ActiveRow.Cells("Item").Value.ToString
                drGrd(EnumGridDetail.BatchNo) = "xxxx"  'String.Empty 'Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value.ToString ''TFS1596
                drGrd(EnumGridDetail.ExpiryDate) = Me.SOExpiryDate  'Convert.ToDateTime(Date.Now.AddMonths(1)) ''TFS4181
                drGrd("Origin") = Me.SOOrigin
                drGrd(EnumGridDetail.Unit) = IIf(cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
                drGrd(EnumGridDetail.OrderQty) = OrderQty
                drGrd(EnumGridDetail.DeliverQty) = DeliverQty
                drGrd(EnumGridDetail.Qty) = Val(1) ''IIf(Val(Me.txtStock.Text) < Val(txtQty.Text), Me.txtStock.Text, txtQty.Text)
                drGrd(EnumGridDetail.RemainingQty) = RemainingQty
                drGrd(EnumGridDetail.Price) = IIf(txtRate.Text.Trim = "", 0, txtRate.Text)
                ''Start TFS2827
                If Me.cmbDiscountType.Text.Equals(DiscountType_Percentage) AndAlso Val(txtDisc.Text) > 0 Then
                    drGrd(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drGrd(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drGrd(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                ElseIf Me.cmbDiscountType.Text.Equals(DiscountType_Flat) AndAlso Val(txtDisc.Text) > 0 Then
                    drGrd(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drGrd(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                    drGrd(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
                Else
                    drGrd(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                    drGrd(EnumGridDetail.DiscountFactor) = 0   'TFS2827
                    drGrd(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                End If
                drGrd(EnumGridDetail.PostDiscountPrice) = Val(txtPDP.Text)   'TFS2827
                ''EndTFS2827
                'drNewItem(EnumGridDetail.Total) = Val(txtTotal.Text)
                'drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue
                drGrd(EnumGridDetail.ArticleID) = cmbItem.ActiveRow.Cells(0).Value
                drGrd(EnumGridDetail.PackQty) = Val(Me.txtPackQty.Text)
                drGrd(EnumGridDetail.CurrentPrice) = Me.cmbItem.ActiveRow.Cells("Price").Value
                drGrd(EnumGridDetail.PackPrice) = Val(txtPackRate.Text)
                drGrd(EnumGridDetail.DeliveryDetailID) = 0
                drGrd(EnumGridDetail.SalesOrderDetailId) = SalesOrderDetailId ''TASK-408
                drGrd(EnumGridDetail.SavedQty) = 0
                drGrd(EnumGridDetail.BatchID) = 0 'Me.cmbBatchNo.Value
                drGrd(EnumGridDetail.TradePrice) = TradePrice
                drGrd(EnumGridDetail.Tax) = DeliveryTax_Percentage '0
                drGrd(EnumGridDetail.SED) = _dblSEDTaxPercent
                drGrd(EnumGridDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
                drGrd(EnumGridDetail.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
                'drNewItem(EnumGridDetail.ServiceQty) = Val(Me.txtServiceQty.Text)
                drGrd(EnumGridDetail.SampleQty) = SchemeQty
                drGrd(EnumGridDetail.Discount_Percentage) = Val(txtDisc.Text) 'Discount_Percentage
                drGrd(EnumGridDetail.Freight) = Freight
                drGrd(EnumGridDetail.MarketReturns) = MarketReturns
                drGrd(EnumGridDetail.SO_ID) = Val(Me.cmbPo.SelectedValue)
                drGrd(EnumGridDetail.UM) = Me.cmbUM.Text.Replace("'", "''")
                drGrd(EnumGridDetail.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
                drGrd(EnumGridDetail.Pack_Desc) = Me.cmbUnit.Text.ToString
                drGrd(EnumGridDetail.Stock) = Val(Me.txtStock.Text)
                drGrd(EnumGridDetail.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
                drGrd(EnumGridDetail.OtherComments) = IIf(strOtherComments = String.Empty, Me.cmbColour.Text, strOtherComments)
                drGrd(EnumGridDetail.Comments) = strComments
                drGrd(EnumGridDetail.Gross_Weights) = 0
                drGrd(EnumGridDetail.Tray_Weights) = 0
                drGrd(EnumGridDetail.Net_Weight) = 0
                drGrd(EnumGridDetail.TotalQty) = Val(1)

                drGrd(EnumGridDetail.CurrencyId) = Me.cmbCurrency.SelectedValue
                If Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    drGrd(EnumGridDetail.CurrencyAmount) = Val(0)
                Else
                    drGrd(EnumGridDetail.CurrencyAmount) = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text), TotalAmountRounding)
                End If
                drGrd(EnumGridDetail.CurrencyRate) = Val(Me.txtCurrencyRate.Text)
                Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
                If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                    drGrd(EnumGridDetail.BaseCurrencyId) = Val(ConfigCurrencyVal)
                    drGrd(EnumGridDetail.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
                End If
                drGrd(EnumGridDetail.LogicalItem) = Me.cmbItem.ActiveRow.Cells("LogicalItem").Value
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                drGrd(EnumGridDetail.AdditionalItem) = 0


                'dtGrid.Rows.Add(drNewItem)
                dtGrid.Rows.Add(drGrd)
                checkqty = checkqty - 1
            Next
        End If
        TradePrice = 0D
        Freight_Rate = 0D
        MarketReturns_Rate = 0D
        FlatRate = 0D
        OrderQty = 0D
        DeliverQty = 0D
        RemainingQty = 0D


        'Task 3913 Saad Afzaal move scroll bar at the end when item added into the grid 
        grd.MoveLast()

    End Sub
    Private Sub AddItemToGrid(ByVal ZeroQty As Boolean)
        Try

            'If Not Val(Me.txtTax.Text) <> 0 Then
            If IsDeliveryOrderAnalysis = True Then
                If GST_Applicable = True Then
                    If DeliveryTax_Percentage = 0 Then DeliveryTax_Percentage = Val(getConfigValueByType("Default_Tax_Percentage").ToString)
                ElseIf FlatRate_Applicable = True Then
                    If DeliveryTax_Percentage = 0 Then DeliveryTax_Percentage = ((FlatRate / Val(Me.cmbItem.SelectedRow.Cells("Price").Text)) * 100)
                End If
            Else
                DeliveryTax_Percentage = Val(Me.txtTax.Text)
            End If
            'Else
            'End If

            Dim qty As Double = 0D
            Dim dtGrid As DataTable = CType(Me.grd.DataSource, DataTable)
            Dim drNewItem As DataRow

            drNewItem = dtGrid.NewRow()
            'drNewItem(EnumGridDetail.Category) = cmbCategory.Text
            drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue 'cmbCategory.Text
            drNewItem(EnumGridDetail.ArticleCode) = cmbItem.ActiveRow.Cells("Code").Text.ToString
            drNewItem(EnumGridDetail.Item) = cmbItem.ActiveRow.Cells("Item").Value.ToString
            If Me.cmbBatchNo.Text.Trim = "" Then
                drNewItem(EnumGridDetail.BatchNo) = "xxxx"
                drNewItem(EnumGridDetail.BatchID) = 0
            Else
                drNewItem(EnumGridDetail.BatchNo) = "xxxx" 'Me.cmbBatchNo.ActiveRow.Cells("Batch No").Value
                drNewItem(EnumGridDetail.BatchID) = 0 'Me.cmbBatchNo.Value
            End If
            drNewItem(EnumGridDetail.ExpiryDate) = Me.SOExpiryDate  'Convert.ToDateTime(Date.Now.AddMonths(1)) ''TFS4181
            drNewItem("Origin") = Me.SOOrigin
            drNewItem(EnumGridDetail.Unit) = IIf(cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString) 'cmbUnit.Text
            drNewItem(EnumGridDetail.Qty) = IIf(txtQty.Text.Trim = "", 0, txtQty.Text)
            If ZeroQty = True Then
                drNewItem(EnumGridDetail.Qty) = 0
            End If
            If Val(OrderQty) = Val(0) Then
                OrderQty = IIf(txtQty.Text.Trim = "", 0, txtQty.Text)
            End If
            drNewItem(EnumGridDetail.OrderQty) = OrderQty
            drNewItem(EnumGridDetail.DeliverQty) = DeliverQty
            drNewItem(EnumGridDetail.RemainingQty) = RemainingQty
            drNewItem(EnumGridDetail.Price) = IIf(txtRate.Text.Trim = "", 0, txtRate.Text)
            ''Start TFS2827
            If Me.cmbDiscountType.Text.Equals(DiscountType_Percentage) AndAlso Val(txtDisc.Text) > 0 Then
                drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
            ElseIf Me.cmbDiscountType.Text.Equals(DiscountType_Flat) AndAlso Val(txtDisc.Text) > 0 Then
                drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
                drNewItem(EnumGridDetail.DiscountFactor) = Val(txtDisc.Text)   'TFS2827
            Else
                drNewItem(EnumGridDetail.DiscountValue) = Val(txtDiscountValue.Text) 'DiscountValue
                drNewItem(EnumGridDetail.DiscountFactor) = 0   'TFS2827
                drNewItem(EnumGridDetail.DiscountId) = Me.cmbDiscountType.SelectedValue  'TFS2827
            End If
            drNewItem(EnumGridDetail.PostDiscountPrice) = Val(txtPDP.Text)   'TFS2827
            ''EndTFS2827
            'drNewItem(EnumGridDetail.LocationID) = cmbCategory.SelectedValue
            drNewItem(EnumGridDetail.ArticleID) = cmbItem.ActiveRow.Cells(0).Value
            drNewItem(EnumGridDetail.PackQty) = Val(Me.txtPackQty.Text)
            drNewItem(EnumGridDetail.CurrentPrice) = Me.cmbItem.ActiveRow.Cells("Price").Value
            drNewItem(EnumGridDetail.DeliveryDetailID) = 0
            drNewItem(EnumGridDetail.SavedQty) = 0
            drNewItem(EnumGridDetail.TradePrice) = TradePrice
            drNewItem(EnumGridDetail.Tax) = DeliveryTax_Percentage
            drNewItem(EnumGridDetail.SED) = _dblSEDTaxPercent
            drNewItem(EnumGridDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
            drNewItem(EnumGridDetail.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
            'drNewItem(EnumGridDetail.ServiceQty) = Val(Me.txtServiceQty.Text)
            drNewItem(EnumGridDetail.SampleQty) = SchemeQty
            drNewItem(EnumGridDetail.Discount_Percentage) = Val(Me.txtDisc.Text)
            drNewItem(EnumGridDetail.Freight) = Freight
            drNewItem(EnumGridDetail.SO_ID) = Val(Me.cmbPo.SelectedValue)
            drNewItem(EnumGridDetail.UM) = Me.cmbUM.Text.Replace("'", "''")
            drNewItem(EnumGridDetail.PurchasePrice) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
            drNewItem(EnumGridDetail.Pack_Desc) = Me.cmbUnit.Text.ToString
            drNewItem(EnumGridDetail.Stock) = Val(Me.txtStock.Text)
            drNewItem(EnumGridDetail.CostPrice) = IIf(Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString) = 0, Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString), Val(Me.cmbItem.ActiveRow.Cells("Cost Price").Value.ToString))
            drNewItem(EnumGridDetail.Comments) = strComments
            drNewItem(EnumGridDetail.OtherComments) = IIf(strOtherComments = String.Empty, Me.cmbColour.Text, strOtherComments)
            drNewItem(EnumGridDetail.TotalQty) = Val(Me.txtTotalQuantity.Text)
            dtGrid.Rows.Add(drNewItem)

            TradePrice = 0D
            Freight_Rate = 0D
            MarketReturns_Rate = 0D
            FlatRate = 0D
            strComments = String.Empty
            strComments = String.Empty
            OrderQty = 0D
            DeliverQty = 0D
            RemainingQty = 0D

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged
        Try
            If Me.IsFormOpend = True Then
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
    Private Sub GetTotal()
        Try

            If Me.grd.RootTable Is Nothing Then Exit Sub
            Me.grd.UpdateData()
            Dim i As Integer
            Dim dblTotalAmount As Double = 0
            Dim dblTotalTax As Double = 0
            Dim dblTotalQty As Double = 0
            Dim dblTotalSEDTax As Double = 0
            Dim dblSchemeTaxValue As Double = 0D
            Dim dblTotalMarketReturns As Double = 0D
            Dim dblTotalFreight As Double = 0D
            For i = 0 To grd.RecordCount - 1
                'dblTotalAmount = dblTotalAmount + Val(grd1.Rows(i).Cells("total").Value)
                'dblTotalQty = dblTotalQty + Val(grd1.Rows(i).Cells("Qty").Value)

                If Val(grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) > 0 Then
                    If Not IsDeliveryOrderAnalysis = True Then
                        dblTotalTax = dblTotalTax + (Val(grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) / 100) * Val(grd.GetRows(i).Cells(EnumGridDetail.Total).Value)
                    Else
                        dblTotalTax = dblTotalTax + (Val(grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) / 100) * (Val(grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value) * IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) + Val(grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value)), ((Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) + Val(grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value))))
                    End If
                End If
                If Val(grd.GetRows(i).Cells(EnumGridDetail.SED).Value) > 0 Then
                    'dblTotalSEDTax = dblTotalSEDTax + (((Me.grd.GetRows(i).Cells(EnumGridDetail.Total).Value * Me.grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) / 100) * Me.grd.GetRows(i).Cells(EnumGridDetail.SED).Value) / 100
                    If Not IsDeliveryOrderAnalysis = True Then
                        dblTotalSEDTax = dblTotalSEDTax + ((Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Total).Value) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SED).Value)) / 100)
                    Else
                        dblSchemeTaxValue = IIf(Val(grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) = 0, 0, (Val(grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) / 100) * (Val(grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value)))
                        dblTotalSEDTax = dblTotalSEDTax + (Val(grd.GetRows(i).Cells(EnumGridDetail.SED).Value) / 100) * (dblSchemeTaxValue + (Val(grd.GetRows(i).Cells(EnumGridDetail.TradePrice).Value) * IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value)), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)))))
                    End If
                End If

                dblTotalMarketReturns += IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", ((Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) + Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value)) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.MarketReturns).Value)), (((Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) + Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value)) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.MarketReturns).Value)))
                dblTotalFreight += IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", ((Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) + Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value)) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Freight).Value)), (((Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) + Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value)) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Freight).Value)))
            Next
            'txtTotalQty.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(EnumGridDetail.Qty), Janus.Windows.GridEX.AggregateFunction.Sum))
            'txtBalance.Text = Val(txtAmount.Text)
            'txtTaxTotal.Text = dblTotalTax
            'If IsDeliveryOrderAnalysis = False Then
            '    txtAmount.Text = (((Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(EnumGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(txtAdjustment.Text) + Val(txtSEDTaxAmount.Text)) + Val(Me.txtAddTransitInsurance.Text)) - Val(Me.txtFuel.Text)) - Val(Me.txtExpense.Text)
            'ElseIf IsDeliveryOrderAnalysis = True Then
            '    Me.txtSEDTaxAmount.Text = Val(dblTotalSEDTax)
            '    Me.txtExpense.Text = dblTotalMarketReturns
            '    txtTransitInsurancePercentage_TextChanged(Nothing, Nothing)
            '    txtAmount.Text = (((((Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(EnumGridDetail.NetBill), Janus.Windows.GridEX.AggregateFunction.Sum))) - Val(txtAdjustment.Text)) + Val(txtSEDTaxAmount.Text)) + Val(Me.txtAddTransitInsurance.Text)) - Val(Me.txtFuel.Text)) - Val(Me.txtExpense.Text)
            '    'Me.txtAddTransitInsurance.Text = Math.Round(((Val(Me.txtTransitInsurancePercentage.Text) / 100) * (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(EnumGridDetail.NetBill), Janus.Windows.GridEX.AggregateFunction.Sum)) + Val(Me.txtSEDTaxAmount.Text))), 2)
            '    'ElseIf ServicesItem = "True" Then
            '    '    txtAmount.Text = (((Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(EnumGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(txtAdjustment.Text) + Val(txtSEDTaxAmount.Text)) + Val(Me.txtAddTransitInsurance.Text)) - Val(Me.txtFuel.Text)) - Val(Me.txtExpense.Text)
            'End If

            'If Me.grd.RowCount = 0 Then
            '    Me.txtAmount.Text = 0
            'End If

        Catch ex As Exception

        End Try

    End Sub
    Public Sub FillCombo(ByVal strCondition As String)
        Dim str As String = String.Empty

        'select * from tbldeftransporter where active=1
        If strCondition = "ItemFilter" Then
            'If Me.RdoCode.Checked = True Then
            '    str = "SELECT     ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleCode AS Code, ArticleDefView.ArticleDescription AS Item, "
            'Else
            '    str = "SELECT     ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleCode AS Code, "
            'End If

            ''If Not Me.cmbCategory.SelectedValue > 0 Then
            'str = str + " ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.DeliveryPrice AS Price, " & _
            '      "ArticleDefView.PurchasePrice, vw_articlestock.Stock  , ArticleDefView.SizeRangeID as [Size ID]   FROM         ArticleDefView, vw_articlestock " & _
            '      "WHERE     ArticleDefView.ArticleId = vw_articlestock.ArticleId and (ArticleDefView.Active = 1)"

            ''Else

            ''    str = str + " ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.DeliveryPrice AS Price, " & _
            ''         "ArticleDefView.PurchasePrice , vw_articlestock.Stock , ArticleDefView.SizeRangeID as [Size ID] FROM         ArticleDefView, vw_articlestock  " & _
            ''         "WHERE       ArticleDefView.ArticleId = vw_articlestock.ArticleId and (ArticleDefView.Active = 1) And ArticleGroupID = " & cmbCategory.SelectedValue

            ''End If
        ElseIf strCondition = "Item" Or strCondition = "ItemFilter" Then
            'Comment against task:2388
            'If getConfigValueByType("StockViewOnSales").ToString = "True" Then
            '    str = "SP_ProductList"
            '    FillUltraDropDown(Me.cmbItem, str)
            '    Me.cmbItem.Rows(0).Activate()
            '    Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            '    Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
            '    If rdoName.Checked = True Then
            '        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            '    Else
            '        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            '    End If
            'Else
            'Before against task:2388
            'str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock, Isnull(PurchasePrice,0) as PurchasePrice FROM ArticleDefView where Active=1 AND SalesItem=1"
            'Task:2388 Added Column ServiceItem
            'str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock, Isnull(PurchasePrice,0) as PurchasePrice, Isnull(ServiceItem,0) as ServiceItem, ArticleDefView.SortOrder, ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand] FROM ArticleDefView where Active=1 AND SalesItem=1"
            str = "SELECT ArticleDefView.ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleModel.Name As Model,ArticleDefView.ArticleBrandName As Grade, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock, Isnull(PurchasePrice,0) as PurchasePrice, Isnull(ServiceItem,0) as ServiceItem, ArticleDefView.SortOrder, ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand],IsNull(ArticleDefView.Cost_Price,0) as [Cost Price] , IsNull(TradePrice,0) as [Trade Price], ISNULL(ArticleDefView.LogicalItem, 0) AS LogicalItem FROM ArticleDefView Left Outer Join (Select ArticleId, ArticleModelList.ModelId, tblDefModelList.Name From ArticleModelList Inner Join tblDefModelList ON  ArticleModelList.ModelId = tblDefModelList.ModelId) As ArticleModel On  ArticleDefView.ArticleId=ArticleModel.ArticleId where Active=1 AND SalesItem=1"
            'End Task:2388
            If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                    str += " AND ArticleDefView.ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                End If
            End If
            If ModelId > 0 Then
                'If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                str += " AND ArticleDefView.ArticleId In (Select ArticleId From ArticleModelList WHERE ModelId=" & ModelId & ")"
                'End If
            End If
            If Me.rbtCustomer.Checked = True Then
                If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
                str += " AND MasterId in(Select ArticleDefId From ArticleDefCustomers WHERE CustomerId=N'" & Me.cmbVendor.Value & "')"
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

            FillUltraDropDown(Me.cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True 'Task:2388 ServiceItem Column Hidden
            Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True

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

        ElseIf strCondition = "Transporter" Then
            str = "select * from tbldeftransporter where active=1 order by sortorder,2"
            FillDropDown(Me.cmbTransporter, str)

        ElseIf strCondition = "Barcodes" Then
            'Before against task:2388
            'str = "SELECT     ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleCode AS Code, "
            'str = str + " ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.SalePrice AS Price, " & _
            '                      " ArticleDefView.PurchasePrice, vw_articlestock.Stock  , ArticleDefView.SizeRangeID as [Size ID]   FROM         ArticleDefView, vw_articlestock " & _
            '                      " WHERE     ArticleDefView.ArticleId = vw_articlestock.ArticleId and (ArticleDefView.Active = 1)"
            'Task:2388 Added Column ServiceItem
            str = "SELECT     ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleDescription AS Item, "
            str = str + " ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.SalePrice AS Price, " & _
                                  " ArticleDefView.PurchasePrice, vw_articlestock.Stock  , ArticleDefView.SizeRangeID as [Size ID], Isnull(ServiceItem,0) as ServiceItem, ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand], IsNull(ArticleDefView.Cost_Price,0) as [Cost Price]   FROM         ArticleDefView, vw_articlestock " & _
                                  " WHERE     ArticleDefView.ArticleId = vw_articlestock.ArticleId and (ArticleDefView.Active = 1)"

            FillUltraDropDown(Me.cmbCodes, str)
            If Me.cmbCodes.Rows.Count > 0 Then Me.cmbCodes.Rows(0).Activate() : Me.cmbCodes.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True : Me.cmbCodes.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True 'Task:2388 ServiceItem Column Hidden

        ElseIf strCondition = "Category" Then
            'str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            'Task#16092015 Load User wise locations 
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Code,Mobile_No,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order"
            'Else
            '    str = "Select Location_Id, Location_Code,Mobile_No,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
            'End If

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"


            FillDropDown(cmbCategory, str, False)


        ElseIf strCondition = "SearchLocation" Then
            str = "Select CompanyId, CompanyName from CompanyDefTable order by 1"
            FillDropDown(Me.cmbSearchLocation, str, True)


            'ElseIf strCondition = "ItemFilter" Then
            '    str = "SELECT     ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,DeliveryPrice as Price,Stock FROM         ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue
            '    FillUltraDropDown(cmbItem, str)
            '    Me.cmbItem.Rows(0).Activate()
        ElseIf strCondition = "Vendor" Then
            'str = "SELECT     tblCustomer.AccountId AS ID, tblCustomer.CustomerName AS Name, tblListTerritory.TerritoryName AS Territory, tblListCity.CityName AS City,  " & _
            '        "tblListState.StateName AS State, tblCustomer.AccountId AS AcId " & _
            '        "FROM         tblListTerritory INNER JOIN " & _
            '        "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
            '        "tblListState ON tblListCity.StateId = tblListState.StateId INNER JOIN " & _
            '        "tblCustomer ON tblListTerritory.TerritoryId = tblCustomer.Territory"

            If getConfigValueByType("Show Vendor On Sales") = "True" Then
                'Before against task:2373
                'str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                '                    "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone " & _
                '                    "FROM  tblCustomer LEFT OUTER JOIN " & _
                '                    "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                '                    "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                '                    "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                '                    "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                '                    "WHERE (vwCOADetail.account_type in( 'Customer','Vendor' )) and  vwCOADetail.coa_detail_id is not  null "
                'Task:2373 Added Column Sub Sub Title
                str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name,vwCOADetail.detail_code as [Code], tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                  "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                                  "FROM  tblCustomer LEFT OUTER JOIN " & _
                                  "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                  "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                  "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                  "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                  "WHERE (vwCOADetail.account_type in( 'Customer','Vendor' )) and  vwCOADetail.coa_detail_id is not  null "
                'End Task:2373
            Else
                'Before against task:2373
                'str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                '                                   "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone " & _
                '                                   "FROM         tblCustomer LEFT OUTER JOIN " & _
                '                                   "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                '                                   "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                '                                   "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                '                                   "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                '                                   "WHERE (vwCOADetail.account_type='Customer') and  vwCOADetail.coa_detail_id is not  null "
                'Task:2373 Added Column Sub Sub Title
                str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name,vwCOADetail.detail_code as [Code], tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                  "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile,  vwCOADetail.Sub_Sub_Title " & _
                                                  "FROM         tblCustomer LEFT OUTER JOIN " & _
                                                  "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                  "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                  "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                  "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                  "WHERE (vwCOADetail.account_type='Customer') and  vwCOADetail.coa_detail_id is not  null "
                'End Task:2373
            End If
            If getConfigValueByType("ShowMiscAccountsOnSales") = "True" Then
                str += " or vwCOADetail.coa_detail_id in (SELECT tblCOAMainSubSubDetail.coa_detail_id " & _
                       "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) "
            End If
            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            'If LoginGroup = "Administrator" Then
            If GetMappedUserId() > 0 And getGroupAccountsConfigforSales(Me.Name) And LoginGroup <> "Administrator" Then
                str = "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name,vwCOADetail.detail_code as [Code], tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                  "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile,  vwCOADetail.Sub_Sub_Title " & _
                                                  "FROM         tblCustomer LEFT OUTER JOIN " & _
                                                  "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                  "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                  "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                  "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                  "WHERE ( vwCOADetail.coa_detail_id is not  null ) "
                str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ")) "
                ''TFS4689 : Getting Relevent Accounts according to the screen and Configuration
                If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                    str += " AND   (dbo.vwCOADetail.account_type = 'Customer') "
                Else
                    str += " AND   (dbo.vwCOADetail.account_type in('Customer','Vendor')) "
            End If
                End If
            ''End TFS3322
            If IsEditMode = False Then
                str += " AND vwCOADetail.Active=1"
            Else
                str += " AND vwCOADetail.Active in(0,1,NULL)"
            End If
            str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title "
            FillUltraDropDown(cmbVendor, str)
            If cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
                'Task:2373 Column Format
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.SubSubTitle).Width = 200
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.SubSubTitle).Header.Caption = "Ac Head"
                'end Task:2373
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
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                           "dbo.tblListTerritory.TerritoryName as Territory, tblCustomer.Email,tblCustomer.Phone,tblCustomer.Mobile  " & _
                                           "FROM dbo.tblCustomer INNER JOIN " & _
                                           "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                           "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                           "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                           "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " _
                                              & " WHERE dbo.vwCOADetail.detail_title Is Not NULL " & IIf(ShowVendorOnSales = True, " AND (dbo.vwCOADetail.account_type in ('Customer','Vendor'))", " AND (dbo.vwCOADetail.account_type in ('Customer'))") & "" _
                                       & "" & IIf(ShowMiscAccountsOnSales = True, " OR vwCOADetail.coa_detail_id IN (SELECT  DISTINCT tblCOAMainSubSubDetail.coa_detail_id " & _
                                      "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) ", "") & ""
            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            'If LoginGroup = "Administrator" Then
            If GetMappedUserId() > 0 And getGroupAccountsConfigforSales(Me.Name) And LoginGroup <> "Administrator" Then
                str = "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name,vwCOADetail.detail_code as [Code], tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                  "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile,  vwCOADetail.Sub_Sub_Title " & _
                                                  "FROM         tblCustomer LEFT OUTER JOIN " & _
                                                  "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                  "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                  "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                  "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                  "WHERE ( vwCOADetail.coa_detail_id is not  null ) "
                str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ")) "
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
            'str = "Select SalesOrderID, SalesOrderNo from SalesOrderMasterTable where SalesOrderID not in(select PoId from DeliveryChalanMasterTable) AND Status='Open' AND IsNull(Posted,0)=1 ORDER BY SalesOrderId DESC"
            'If flgLoadItemAfterDeliveredOnDC Then
            '    str = "Select SalesOrderID, SalesOrderNo, IsNull(TransporterId, 0) AS TransporterId from SalesOrderMasterTable where IsNull(Posted,0)=1 AND VendorID=" & Me.cmbVendor.Value & " And LocationId = " & Me.cmbCompany.SelectedValue & " ORDER BY SalesOrderId DESC"
            'Else
            ''Commented Against TFS3326
            ' str = "Select SalesOrderID, SalesOrderNo, IsNull(TransporterId, 0) AS TransporterId from SalesOrderMasterTable where IsNull(Posted,0)=1 AND Status='Open'  AND VendorID=" & Me.cmbVendor.Value & " And LocationId = " & Me.cmbCompany.SelectedValue & " ORDER BY SalesOrderId DESC"
            'End If
            ''TFS3520 : Ayesha Rehman : 14-06-2018
            ''Filling SO combo According to Configuration
            If flgSOSeparateClosure = True Then
                str = "Select SalesOrderID, SalesOrderNo, IsNull(TransporterId, 0) AS TransporterId , SOP_ID, PONo , Remarks from SalesOrderMasterTable where IsNull(Posted,0)=1 AND ( Status='Open' Or ISNULL(DCStatus, 'Open')='Open' ) AND VendorID=" & Me.cmbVendor.Value & " And LocationId = " & Me.cmbCompany.SelectedValue & " ORDER BY SalesOrderId DESC"
            Else
                str = "Select SalesOrderID, SalesOrderNo, IsNull(TransporterId, 0) AS TransporterId , SOP_ID, PONo ,Remarks from SalesOrderMasterTable where IsNull(Posted,0)=1 AND Status='Open'  AND VendorID=" & Me.cmbVendor.Value & " And LocationId = " & Me.cmbCompany.SelectedValue & " ORDER BY SalesOrderId DESC"
            End If
            FillDropDown(cmbPo, str)

        ElseIf strCondition = "SOComplete" Then
            str = "Select SalesOrderID, SalesOrderNo, IsNull(TransporterId, 0) AS TransporterId from SalesOrderMasterTable "
            FillDropDown(cmbPo, str)

        ElseIf strCondition = "SM" Then

            str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE SalePerson <> '0' And Active = 1 " ''TASKTFS75 added and set active =1
            FillDropDown(Me.cmbSalesMan, str)

        ElseIf strCondition = "Company" Then
            'Task#16092015 load user wise companies
            'If getConfigValueByType("UserwiseCompany").ToString = "True" Then
            '    str = "Select CompanyId, CompanyName from CompanyDefTable Where CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")"
            'Else
            '    str = "Select CompanyId, CompanyName from CompanyDefTable"
            'End If


            str = "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ") " _
                           & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable WHERE CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")" _
                           & "Else " _
                           & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable "

            FillDropDown(Me.cmbCompany, str, False)

        ElseIf strCondition = "grdLocation" Then
            'str = "Select Location_Id, Location_Name From tblDefLocation"
            'Task#16092015 Load user wise locations
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Name,Mobile_No,IsNull(AllowMinusStock,0) as AllowMinusStock From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")"
            'Else
            '    str = "Select Location_Id, Location_Name,Mobile_No,IsNull(AllowMinusStock,0) as AllowMinusStock From tblDefLocation"
            'End If

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

            Dim dt As DataTable = GetDataTable(str)
            Dim objSize As New Size

            'Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Name")
            Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
        ElseIf strCondition = "grdSO" Then
            str = "Select SalesOrderID, SalesOrderNo from SalesOrderMasterTable Union Select 0, ''"
            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns("So_Id").ValueList.PopulateValueList(dt.DefaultView, "SalesOrderID", "SalesOrderNo")
        ElseIf strCondition = "UM" Then
            str = "Select DISTINCT UOM, UOM From DeliveryChalanDetailTable WHERE UOM <> '' ORDER BY 1 ASC"
            FillDropDown(Me.cmbUM, str, False)
        ElseIf strCondition = "Colour" Then
            str = "Select DISTINCT Other_Comments, Other_Comments From DeliveryChalanDetailTable WHERE Other_Comments <> '' ORDER BY 1 ASC"
            FillDropDown(Me.cmbColour, str, False)
            Me.cmbColour.SelectedIndex = -1
        ElseIf strCondition = "grdUM" Then
            str = "Select DISTINCT UOM, UOM From DeliveryChalanDetailTable WHERE UOM <> '' ORDER BY 1 ASC "
            Dim dtUM As New DataTable
            dtUM = GetDataTable(str)
            Me.grd.RootTable.Columns("UOM").ValueList.PopulateValueList(dtUM.DefaultView, "UOM", "UOM")
            'ElseIf strCondition = "grdColour" Then
            '    str = "Select DISTINCT Colour, Colour From DeliveryChalanDetailTable WHERE Colour <> '' ORDER BY 1 ASC "
            '    Dim dtUM As New DataTable
            '    dtUM = GetDataTable(str)
            '    Me.grd.RootTable.Columns("ArticlePackColour").ValueList.PopulateValueList(dtUM.DefaultView, "Colour", "Colour")
        ElseIf strCondition = "ArticlePack" Then
            Me.cmbUnit.ValueMember = "ArticlePackId"
            Me.cmbUnit.DisplayMember = "PackName"
            Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
        ElseIf strCondition = "Other_Company" Then
            FillDropDown(Me.cmbOtherCompany, "Select Other_Company,Other_Company From DeliveryChalanMasterTable where other_company <> ''", False)
        ElseIf strCondition = "Currency" Then ''TASK-407
            str = String.Empty
            str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
            FillDropDown(Me.cmbCurrency, str, False)
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
        ElseIf strCondition = "grdEngine" Then
            str = "SELECT Engine_No, Engine_No, Sum(InQty) InQty, Sum(OutQty) OutQty, Sum(InQty)-Sum(OutQty) Qty FROM StockDetailTable WHERE IsNull(Engine_No, '') <> '' GROUP BY Engine_No, Chassis_No HAVING Sum(InQty)-Sum(OutQty) > 0"
            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns("Engine_No").ValueList.PopulateValueList(dt.DefaultView, "Engine_No", "Engine_No")
        ElseIf strCondition = "grdChassis" Then
            str = "SELECT Chassis_No, Chassis_No, Sum(InQty) InQty, Sum(OutQty) OutQty, Sum(InQty)-Sum(OutQty) Qty FROM StockDetailTable WHERE IsNull(Engine_No, '') <> '' GROUP BY Engine_No, Chassis_No HAVING Sum(InQty)-Sum(OutQty) > 0"
            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns("Chassis_No").ValueList.PopulateValueList(dt.DefaultView, "Chassis_No", "Chassis_No")
            '12-04-2018 : Ayesha Rehman : TFS2827 : Filling Combo Discount Type 
        ElseIf strCondition = "Discount Type" Then
            str = "select DiscountID, DiscountType from tblDiscountType "
            FillDropDown(Me.cmbDiscountType, str, False)
        ElseIf strCondition = "grdDiscountType" Then
            str = "select DiscountID, DiscountType from tblDiscountType"
            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns(EnumGridDetail.DiscountId).ValueList.PopulateValueList(dt.DefaultView, "DiscountID", "DiscountType")
            ''End TFS2827
                ''Start TFS4181
        ElseIf strCondition = "grdBatchNo" Then
            str = "Select  BatchNo,BatchNo,ExpiryDate,Origin  From  StockDetailTable  where BatchNo not in ('','0','xxxx') Group by BatchNo,ExpiryDate,Origin Having (Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0))) > 0 ORDER BY ExpiryDate  desc "
                Dim dt As DataTable = GetDataTable(str)
                Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).ValueList.PopulateValueList(dt.DefaultView, "BatchNo", "BatchNo")
            ''End TFS4181
        ElseIf strCondition = "GrdOrigin" Then
            str = "select CountryName, CountryName From tblListCountry Where Active = 1"
            Dim dtCountry As DataTable = GetDataTable(str)
            dtCountry.AcceptChanges()
            Me.grd.RootTable.Columns("Origin").ValueList.PopulateValueList(dtCountry.DefaultView, "CountryName", "CountryName")
        End If
    End Sub

    'Private Sub txtPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaid.TextChanged
    '    txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)
    'End Sub

    Private Function Save() As Boolean
        If Me.chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        If Me.chkDelivered.Visible = False Then
            Me.chkDelivered.Checked = False
        End If
        CostId = GetCostCenterId(Me.cmbCompany.SelectedValue)
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("SI" & Me.cmbCompany.SelectedValue, 6, "DeliveryChalanMasterTable", "DeliveryNo")
        setVoucherNo = Me.txtPONo.Text
        'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer

        Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo.Text)
        ' Dim AccountId As Integer = GetConfigValue("SalesCreditAccount")
        'Dim DeliveryTaxId As Integer = GetConfigValue("SalesTaxCreditAccount")
        'Dim SEDAccountId As Integer = Val(GetConfigValue("SEDAccountId").ToString)
        'Dim InsuranceAccountId As Integer = Val(GetConfigValue("TransitInsuranceAccountId").ToString)
        'Dim ReceiptVoucherFlg As String = GetConfigValue("ReceiptVoucherOnSales").ToString
        'Dim VoucherNo As String = GetVoucherNo()
        'Dim ServiceItem As String = GetConfigValue("ServiceItem").ToString
        'Dim IsDiscountVoucher As Boolean = Convert.ToBoolean(GetConfigValue("DiscountVoucherOnDelivery").ToString)
        'Dim DeliveryDiscountAccount As Integer = Val(GetConfigValue("DeliveryDiscountAccount").ToString)
        'Dim DiscountedPrice As Double = 0
        'Dim strvoucherNo As String = GetNextDocNo("SV", 6, "tblVoucher", "voucher_no")
        Dim CurrentBalance As Double = CDbl(GetAccountBalance(Me.cmbVendor.ActiveRow.Cells(0).Value))
        Dim blnCheckCurrentStockByItem As Boolean = False
        If Not getConfigValueByType("CheckCurrentStockByItem").ToString = "Error" Then
            blnCheckCurrentStockByItem = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)
        End If
        ''TASK TFS1378
        Dim DCStockImpact As Boolean = False
        If Not getConfigValueByType("DCStockImpact").ToString = "Error" Then
            DCStockImpact = Convert.ToBoolean(getConfigValueByType("DCStockImpact").ToString)
        End If
        ''END TFS1378
        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'Rafay
            objCommand.CommandText = "Insert into DeliveryChalanMasterTable (LocationId,DeliveryNo,DeliveryDate,CustomerCode,Poid, Employeecode,DeliveryQty,DeliveryAmount, CashPaid, Remarks,PO_NO,UserName,PreviousBalance,TransporterId,BiltyNo,FuelExpense, OtherExpense, Adjustment, CostCenterId, Post,  DcDate, Delivered, TransitInsurance, Driver_Name, Vehicle_No,Other_Company,Status ,Arrival_Time,Departure_Time, JobCardId, UserId) values( " _
                          & IIf(Me.cmbCompany.SelectedValue = Nothing, 0, Me.cmbCompany.SelectedValue) & ", N'" & txtPONo.Text.Replace("'", "''") & "' ,N' " & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & " '," & cmbVendor.ActiveRow.Cells(0).Value & "," & IIf(Me.cmbPo.SelectedIndex = -1, 0, Me.cmbPo.SelectedValue) & ", " & IIf(Me.cmbSalesMan.SelectedIndex = -1, 0, Me.cmbSalesMan.SelectedValue) & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(EnumGridDetail.Qty), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(EnumGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(Me.grd.GetTotal(grd.RootTable.Columns(EnumGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & txtPO.Text.Replace("'", "''") & "',N'" & LoginUserName.Replace("'", "''") & "'," & CurrentBalance & "," & Me.cmbTransporter.SelectedValue & ",N'" & Me.uitxtBiltyNo.Text.Replace("'", "''") & "', 0,0," & Val(Me.txtAdjustment.Text) & ", " & CostId & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ", N'" & Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(Me.chkDelivered.Checked = True, 1, 0) & ", 0, '" & Me.txtDriverName.Text.Replace("'", "''") & "', N'" & Me.txtVehicleNo.Text.Replace("'", "''") & "', N'" & Me.cmbOtherCompany.Text.Replace("'", "''") & "','Open',N'" & dtpArrivalTime.Value.ToString("h:mm:ss tt") & "', " & IIf(Me.dtpDepartureTime.Checked = True, "Convert(Datetime,'" & Me.dtpDepartureTime.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)", "NULL") & ", " & Val(Me.txtJobCardId.Text) & " , " & Val(LoginUserId) & " )" _
                          & " SELECT @@IDENTITY"
            InvId = objCommand.ExecuteScalar
            getVoucher_Id = InvId
            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                SaveDocument(getVoucher_Id, Me.Name, trans)
            End If

            objCommand.CommandText = ""
            StockList = New List(Of StockDetail)
            For i = 0 To grd.GetRows.Length - 1

                If blnTradePriceExceed = True Then
                    If Val(Me.grd.GetRows(i).Cells("TradePrice").Value.ToString) > Val(Me.grd.GetRows(i).Cells("Rate").Value.ToString) Then

                        Throw New Exception("Sale price is less than trade price.")

                    End If
                End If

                If blnCheckCurrentStockByItem = True Then
                    CheckCurrentStockByItem(Me.grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value, IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Unit).Value.ToString = "Loose", Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value), Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)), Me.grd, , trans)
                End If

                If flgVehicleIdentificationInfo = True Then

                    Dim Engine_No As String = ""
                    Dim Chassis_No As String = ""
                    If Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
                        Engine_No = Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Substring(4)
                    End If
                    If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                        Chassis_No = Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Substring(4)
                    End If
                    objCommand.CommandText = ""
                    objCommand.CommandText = "SELECT dbo.DeliveryChalanDetailTable.ArticleDefId, Stuff(DeliveryChalanDetailTable.Engine_No, 1, 4, '') As Engine_No, Stuff(DeliveryChalanDetailTable.Chassis_No, 1, 4, '') As Chassis_No " _
                                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                    & " dbo.DeliveryChalanDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.DeliveryChalanDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"
                    If Engine_No.Length > 0 Then
                        objCommand.CommandText += " AND Stuff(DeliveryChalanDetailTable.Engine_No, 1, 4, '') =N'" & Engine_No & "'"
                    End If
                    'Dim dt As DataTable = GetDataTable(objCommand.CommandText)
                    'If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                    '    objCommand.CommandText += " AND DeliveryChalanDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
                    'End If
                    Dim dtVehicleEngineNoIdentificationInfo As New DataTable
                    Dim daVehicleEngineNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                    daVehicleEngineNoIdentificationInfo.Fill(dtVehicleEngineNoIdentificationInfo)

                    ''Retrieving Chasis No
                    objCommand.CommandText = ""
                    objCommand.CommandText = "SELECT dbo.DeliveryChalanDetailTable.ArticleDefId, Stuff(DeliveryChalanDetailTable.Engine_No, 1, 4, '') As Engine_No, Stuff(DeliveryChalanDetailTable.Chassis_No, 1, 4, '') As Chassis_No " _
                                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                    & " dbo.DeliveryChalanDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.DeliveryChalanDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"
                    'If Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
                    '    objCommand.CommandText += " AND DeliveryChalanDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "'"
                    'End If
                    If Chassis_No.Length > 0 Then
                        objCommand.CommandText += " AND Stuff(DeliveryChalanDetailTable.Chassis_No, 1, 4, '') =N'" & Chassis_No & "'"
                    End If
                    Dim dtVehicleChasisNoIdentificationInfo As New DataTable
                    Dim daVehicleChasisNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                    daVehicleChasisNoIdentificationInfo.Fill(dtVehicleChasisNoIdentificationInfo)

                    'Task:2606 Engine_No and Chassis No Data From Sales Return
                    ''Retrieving Return Engine No
                    objCommand.CommandText = ""
                    objCommand.CommandText = "SELECT dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Engine_No, SalesReturnDetailTable.Chassis_No  " _
                                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                    & " dbo.SalesReturnDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesReturnDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"

                    objCommand.CommandText += " AND Stuff(SalesReturnDetailTable.Engine_No, 1, 4, '')=N'" & Engine_No & "'"


                    'If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                    '    objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
                    'End If
                    Dim dtSalesReturnVehichleEngineNoInfo As New DataTable
                    Dim daSalesReturnVehichleEngineNoInfo As New OleDbDataAdapter(objCommand)
                    daSalesReturnVehichleEngineNoInfo.Fill(dtSalesReturnVehichleEngineNoInfo)

                    ''Retrieving Return Chasis No
                    objCommand.CommandText = ""
                    objCommand.CommandText = "SELECT dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Engine_No, SalesReturnDetailTable.Chassis_No  " _
                                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                    & " dbo.SalesReturnDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesReturnDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"

                    objCommand.CommandText += " AND Stuff(SalesReturnDetailTable.Chassis_No, 1, 4, '')=N'" & Chassis_No & "'"


                    'If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                    '    objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
                    'End If
                    Dim dtSalesReturnVehichleChasisNoInfo As New DataTable
                    Dim daSalesReturnVehichleChasisNoInfo As New OleDbDataAdapter(objCommand)
                    daSalesReturnVehichleChasisNoInfo.Fill(dtSalesReturnVehichleChasisNoInfo)

                    If dtVehicleEngineNoIdentificationInfo IsNot Nothing Then
                        If dtVehicleEngineNoIdentificationInfo.Rows.Count > 0 Then
                            If dtVehicleEngineNoIdentificationInfo.Rows.Count > dtSalesReturnVehichleEngineNoInfo.Rows.Count Then
                                If dtVehicleEngineNoIdentificationInfo.Rows(0).Item("Engine_No").ToString.Length > 0 Or Engine_No.Length > 0 Then
                                    If Engine_No = dtVehicleEngineNoIdentificationInfo.Rows(0).Item("Engine_No").ToString Then
                                        Throw New Exception("Engine no [" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "] already exists")
                                    End If
                                End If
                            End If
                        End If
                    End If

                    If dtVehicleChasisNoIdentificationInfo IsNot Nothing Then
                        If dtVehicleChasisNoIdentificationInfo.Rows.Count > 0 Then
                            If dtVehicleChasisNoIdentificationInfo.Rows.Count > dtSalesReturnVehichleChasisNoInfo.Rows.Count Then
                                If dtVehicleChasisNoIdentificationInfo.Rows(0).Item("Chassis_No").ToString.Length > 0 Or Chassis_No.Length > 0 Then
                                    If Chassis_No = dtVehicleChasisNoIdentificationInfo.Rows(0).Item("Chassis_No").ToString Then
                                        Throw New Exception("Chassis no [" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "] already exists")
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                '    ''Retrieving Engine No
                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "SELECT dbo.DeliveryChalanDetailTable.ArticleDefId, DeliveryChalanDetailTable.Engine_No, DeliveryChalanDetailTable.Chassis_No  " _
                '                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                '                                    & " dbo.DeliveryChalanDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.DeliveryChalanDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"
                '    If Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
                '        objCommand.CommandText += " AND DeliveryChalanDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "'"
                '    End If


                '    Dim dtVehicleEngineNoIdentificationInfo As New DataTable
                '    Dim daVehicleEngineNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                '    daVehicleEngineNoIdentificationInfo.Fill(dtVehicleEngineNoIdentificationInfo)

                '    ''Retrieving Chasis No
                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "SELECT dbo.DeliveryChalanDetailTable.ArticleDefId, DeliveryChalanDetailTable.Engine_No, DeliveryChalanDetailTable.Chassis_No  " _
                '                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                '                                    & " dbo.DeliveryChalanDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.DeliveryChalanDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"


                '    If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                '        objCommand.CommandText += " AND DeliveryChalanDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
                '    End If
                '    Dim dtVehicleChasisNoIdentificationInfo As New DataTable
                '    Dim daVehicleChasisNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                '    daVehicleChasisNoIdentificationInfo.Fill(dtVehicleChasisNoIdentificationInfo)

                '    'Task:2606 Engine_No and Chassis No Data From Sales Return
                '    ''Retrieving Return Engine No
                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "SELECT dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Engine_No, SalesReturnDetailTable.Chassis_No  " _
                '                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                '                                    & " dbo.SalesReturnDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesReturnDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"

                '    objCommand.CommandText += " AND SalesReturnDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "'"




                '    Dim dtSalesReturnVehichleEngineNoInfo As New DataTable
                '    Dim daSalesReturnVehichleEngineNoInfo As New OleDbDataAdapter(objCommand)
                '    daSalesReturnVehichleEngineNoInfo.Fill(dtSalesReturnVehichleEngineNoInfo)

                '    ''Retrieving Return Chasis No
                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "SELECT dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Engine_No, SalesReturnDetailTable.Chassis_No  " _
                '                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                '                                    & " dbo.SalesReturnDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesReturnDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"

                '    objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"


                '    Dim dtSalesReturnVehichleChasisNoInfo As New DataTable
                '    Dim daSalesReturnVehichleChasisNoInfo As New OleDbDataAdapter(objCommand)
                '    daSalesReturnVehichleChasisNoInfo.Fill(dtSalesReturnVehichleChasisNoInfo)

                '    If dtVehicleEngineNoIdentificationInfo IsNot Nothing Then
                '        If dtVehicleEngineNoIdentificationInfo.Rows.Count > 0 Then
                '            If dtVehicleEngineNoIdentificationInfo.Rows.Count > dtSalesReturnVehichleEngineNoInfo.Rows.Count Then
                '                If dtVehicleEngineNoIdentificationInfo.Rows(0).Item("Engine_No").ToString.Length > 0 Or Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
                '                    If Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString = dtVehicleEngineNoIdentificationInfo.Rows(0).Item("Engine_No").ToString Then
                '                        Throw New Exception("Engine no [" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "] already exists")
                '                    End If
                '                End If
                '            End If
                '        End If
                '    End If

                '    If dtVehicleChasisNoIdentificationInfo IsNot Nothing Then
                '        If dtVehicleChasisNoIdentificationInfo.Rows.Count > 0 Then
                '            If dtVehicleChasisNoIdentificationInfo.Rows.Count > dtSalesReturnVehichleChasisNoInfo.Rows.Count Then
                '                If dtVehicleChasisNoIdentificationInfo.Rows(0).Item("Chassis_No").ToString.Length > 0 Or Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                '                    If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString = dtVehicleChasisNoIdentificationInfo.Rows(0).Item("Chassis_No").ToString Then
                '                        Throw New Exception("Chassis no [" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "] already exists")
                '                    End If
                '                End If
                '            End If
                '        End If
                '    End If
                'End If

                'End Task: 2606
                'End Task:2415




                'Before against task:2598
                'Task:M16 Added Column Engine_No and Chassis_No
                'objCommand.CommandText = "Insert into DeliveryChalanDetailTable (DeliveryId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, BatchNo, BatchID,LocationId, TaxPercent, SampleQty, SEDPercent, TradePrice, Discount_Percentage, Freight,MarketReturns,SO_ID, UOM, PurchasePrice,Pack_Desc, Engine_No, Chassis_No) values( " _
                '                    & " ident_current('DeliveryChalanMasterTable')," & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGridDetail.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", " _
                '                    & " " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value))) & ", " _
                '                    & Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value) & ", " & Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value) & " , " & Val(grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value) & ",N'" & grd.GetRows(i).Cells(EnumGridDetail.BatchNo).Value & "', " _
                '                    & grd.GetRows(i).Cells(EnumGridDetail.BatchID).Value & ", " & grd.GetRows(i).Cells(EnumGridDetail.LocationID).Value & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Tax).Value.ToString = "", 0, grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) & "," & Val(grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value.ToString()) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SED).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TradePrice).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Discount_Percentage).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Freight).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.MarketReturns).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) & ", N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.UM).Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.PurchasePrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "') "
                'End Task:M16
                'Task:2598 Added Field Comments
                'objCommand.CommandText = "Insert into DeliveryChalanDetailTable (DeliveryId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, BatchNo, BatchID,LocationId, TaxPercent, SampleQty, SEDPercent, TradePrice, Discount_Percentage, Freight,MarketReturns,SO_ID, UOM, PurchasePrice,Pack_Desc, Engine_No, Chassis_No,Comments, ExpiryDate) values( " _
                '                   & " ident_current('DeliveryChalanMasterTable')," & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGridDetail.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", " _
                '                   & " " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value))) & ", " _
                '                   & Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value) & ", " & Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value) & " , " & Val(grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value) & ",N'" & grd.GetRows(i).Cells(EnumGridDetail.BatchNo).Value & "', " _
                '                   & grd.GetRows(i).Cells(EnumGridDetail.BatchID).Value & ", " & grd.GetRows(i).Cells(EnumGridDetail.LocationID).Value & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Tax).Value.ToString = "", 0, grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) & "," & Val(grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value.ToString()) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SED).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TradePrice).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Discount_Percentage).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Freight).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.MarketReturns).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) & ", N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.UM).Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.PurchasePrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', " & IIf(grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value.ToString = "", "NULL", "Convert(DateTime,N'" & CDate(IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value.ToString = "", Date.Now, Me.grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value)).ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ") "
                'End Task:2598


                ''TASK TFS1378 ON 13-10-2017


                Dim CostPrice As Double = 0D
                Dim CrrStock As Double = 0D
                Dim DiscountedPrice As Double = 0
                DiscountedPrice = Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value - Me.grd.GetRows(i).Cells(EnumGridDetail.Price).Value
                Dim dblPurchasePrice As Double = 0D
                Dim dblCostPrice As Double = 0D
                Dim strPriceData() As String = GetRateByItem(Val(Me.grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value.ToString)).Split(",")
                If strPriceData.Length > 1 Then
                    dblCostPrice = Val(strPriceData(0).ToString)
                    dblPurchasePrice = Val(strPriceData(1).ToString)
                End If

                If flgAvgRate = True Then
                    If dblCostPrice > 0 Then
                        CostPrice = dblCostPrice
                    Else
                        CostPrice = dblPurchasePrice
                    End If
                Else
                    CostPrice = dblPurchasePrice
                End If
                If getConfigValueByType("DCStockImpact") = "True" Then
                    Dim IsLogicalItemStockMade As Boolean = False
                    If CType(Val(grd.GetRows(i).Cells("LogicalItem").Value.ToString), Boolean) = True Then
                        ''Below line is commented on 31-05-2018 against TASK TFS3324
                        'FillModelOfStockDetail(grd.GetRows(i).Cells("ArticleDefId").Value, grd.GetRows(i).Cells("LocationId").Value, Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) + Val(grd.GetRows(i).Cells("Sample Qty").Value.ToString), grd.GetRows(i).Cells("Comments").Value.ToString)
                        IsLogicalItemStockMade = GetItemsOfLogicalItem(grd.GetRows(i).Cells("ArticleDefId").Value, grd.GetRows(i).Cells("LocationId").Value, Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) + Val(grd.GetRows(i).Cells("Sample Qty").Value.ToString), grd.GetRows(i).Cells("Comments").Value.ToString, objCommand)
                    End If
                    If CType(Val(grd.GetRows(i).Cells("LogicalItem").Value.ToString), Boolean) = False Or IsLogicalItemStockMade = False Then
                        StockDetail = New StockDetail
                        StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                        StockDetail.LocationId = grd.GetRows(i).Cells("LocationId").Value
                        StockDetail.ArticleDefId = grd.GetRows(i).Cells("ArticleDefId").Value
                        StockDetail.InQty = 0
                        StockDetail.OutQty = Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)
                        ''Commmented Against TFS3589 : Ayesha Rehman : 25-06-2018 :  rate on run time is not saved in item ledger or stock statement
                        ' StockDetail.Rate = IIf(CostPrice = 0, Val(grd.GetRows(i).Cells(EnumGridDetail.PurchasePrice).Value), CostPrice)
                        StockDetail.Rate = Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value.ToString)
                        StockDetail.InAmount = 0
                        ''Commmented Against TFS3589 : Ayesha Rehman : 25-06-2018 :  rate on run time is not saved in item ledger or stock statement
                        'StockDetail.OutAmount = ((Val(grd.GetRows(i).Cells("TotalQty").Value) + Val(grd.GetRows(i).Cells("Sample Qty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells(EnumGridDetail.PurchasePrice).Value), CostPrice))
                        StockDetail.OutAmount = ((Val(grd.GetRows(i).Cells("TotalQty").Value) + Val(grd.GetRows(i).Cells("Sample Qty").Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value.ToString))
                        StockDetail.Remarks = grd.GetRows(i).Cells("Comments").Value.ToString
                        StockDetail.Engine_No = grd.GetRows(i).Cells("Engine_No").Value.ToString
                        StockDetail.Chassis_No = grd.GetRows(i).Cells("Chassis_No").Value.ToString
                        ''Start TFS4181
                        StockDetail.BatchNo = grd.GetRows(i).Cells(EnumGridDetail.BatchNo).Value.ToString
                        ''End TFS4181
                        ''Start TASK-TFS4181
                        StockDetail.ExpiryDate = CType(grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value, Date).ToString("yyyy-M-d h:mm:ss tt")
                        ''End TASK-4181
                        StockDetail.Origin = grd.GetRows(i).Cells("Origin").Value.ToString
                        CostPrice = StockDetail.Rate
                        StockDetail.PackQty = Val(grd.GetRows(i).Cells("Pack Qty").Value.ToString)
                        StockDetail.Out_PackQty = Val(grd.GetRows(i).Cells("Qty").Value.ToString)
                        StockDetail.In_PackQty = 0
                        StockList.Add(StockDetail)
                    End If
                End If
                '' END TASK TFS1378 ON 13-10-2017
                'objCommand.CommandText = "Insert into DeliveryChalanDetailTable (DeliveryId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, BatchNo, BatchID,LocationId, TaxPercent, SampleQty, SEDPercent, TradePrice, Discount_Percentage, Freight,MarketReturns,SO_ID, UOM, PurchasePrice,Pack_Desc, Engine_No, Chassis_No,Comments, ExpiryDate,Other_Comments) values( " _
                '                   & " ident_current('DeliveryChalanMasterTable')," & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGridDetail.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", " _
                '                   & " " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value))) & ", " _
                '                   & Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value) & ", " & Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value) & " , " & Val(grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value) & ",N'" & grd.GetRows(i).Cells(EnumGridDetail.BatchNo).Value & "', " _
                '                   & grd.GetRows(i).Cells(EnumGridDetail.BatchID).Value & ", " & grd.GetRows(i).Cells(EnumGridDetail.LocationID).Value & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Tax).Value.ToString = "", 0, grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) & "," & Val(grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value.ToString()) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SED).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TradePrice).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Discount_Percentage).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Freight).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.MarketReturns).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) & ", N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.UM).Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.PurchasePrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', " & IIf(grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value.ToString = "", "NULL", "Convert(DateTime,N'" & CDate(IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value.ToString = "", Date.Now, Me.grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value)).ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "') "

                ''TASK TFS1496 addition column of PackPrice
                objCommand.CommandText = "Insert into DeliveryChalanDetailTable(DeliveryId, ArticleDefId,ArticleSize , Sz1,Qty,Price,Sz7,CurrentPrice, BatchNo, BatchID,LocationId, TaxPercent, SampleQty, SEDPercent, TradePrice, Discount_Percentage, Freight,MarketReturns,SO_ID, UOM, PurchasePrice,Pack_Desc, Engine_No, Chassis_No,Comments, ExpiryDate,Other_Comments,CostPrice, SO_Detail_ID , Gross_Weights,Tray_Weights,Net_Weights, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, PackPrice ,PerviouslyDeliveredQty, BardanaDeduction, OtherDeduction , DiscountId, DiscountValue, DiscountFactor , PostDiscountPrice, Origin, AlternativeItem, AlternativeItemId) values( " _
                                   & " ident_current('DeliveryChalanMasterTable')," & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGridDetail.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", " _
                                   & " " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TotalQty).Value) & ", " _
                                   & Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value) & ", " & Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value) & " , " & Val(grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value) & ",N'" & grd.GetRows(i).Cells(EnumGridDetail.BatchNo).Value & "', " _
                                   & grd.GetRows(i).Cells(EnumGridDetail.BatchID).Value & ", " & grd.GetRows(i).Cells(EnumGridDetail.LocationID).Value & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Tax).Value.ToString = "", 0, grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) & "," & Val(grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value.ToString()) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SED).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TradePrice).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Discount_Percentage).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Freight).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.MarketReturns).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) & ", N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.UM).Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.PurchasePrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', " & IIf(grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value.ToString = "", "NULL", "Convert(DateTime,N'" & CDate(IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value.ToString = "", Date.Now, Me.grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value)).ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & " , " & Val(Me.grd.GetRows(i).Cells("Gross_Weights").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Tray_Weights").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("Net_Weights").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.DeliverQty).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.BardanaDeduction).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.OtherDeduction).Value.ToString) & "," & Val(grd.GetRows(i).Cells(EnumGridDetail.DiscountId).Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.DiscountValue).Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.DiscountFactor).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.PostDiscountPrice).Value) & ",N'" & grd.GetRows(i).Cells("Origin").Value & "',N'" & (grd.GetRows(i).Cells("AlternativeItem").Value) & "'," & Val(grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) & ") "
                objCommand.ExecuteNonQuery()

                'Val(grd.Rows(i).Cells(5).Value)
                'update date Delivery Order Detail and Delivery Order Master table


                If Mode = "Normal" Then
                    ''TFS3520 : Ayesha Rehman : 14-06-2018
                    ''DCQty and Delivered Qty changes According to Configuration of Separate Closure 
                    If flgSOSeparateClosure = True Then
                        If flgMultipleSalesOrder = False Then
                            objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                    & " SET DCQty = isnull(DCQty,0) +  " & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) & "), DCTotalQty= IsNull(DCTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TotalQty).Value) & " " _
                                                    & " WHERE     (SalesOrderID = " & IIf(Me.cmbPo.SelectedIndex = -1, 0, Me.cmbPo.SelectedValue) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ") And (SalesOrderDetailId =" & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ")  "
                            objCommand.ExecuteNonQuery()
                        Else
                            If Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) > 0 Then
                                objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                       & " SET              DCQty = isnull(DCQty,0) +  " & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) & "), DCTotalQty= IsNull(DCTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TotalQty).Value) & " " _
                                                       & " WHERE     (SalesOrderID = " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ") And (SalesOrderDetailId =" & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ")"
                                objCommand.ExecuteNonQuery()
                            End If
                        End If
                    Else
                        If flgMultipleSalesOrder = False Then
                            objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                    & " SET DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) & "), DeliveredTotalQty= IsNull(DeliveredTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TotalQty).Value) & " " _
                                                    & " WHERE     (SalesOrderID = " & IIf(Me.cmbPo.SelectedIndex = -1, 0, Me.cmbPo.SelectedValue) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ") And (SalesOrderDetailId =" & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ")  "
                            objCommand.ExecuteNonQuery()
                        Else
                            If Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) > 0 Then
                                objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                       & " SET              DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) & "), DeliveredTotalQty= IsNull(DeliveredTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TotalQty).Value) & " " _
                                                       & " WHERE     (SalesOrderID = " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ") And (SalesOrderDetailId =" & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ")"
                                objCommand.ExecuteNonQuery()
                            End If
                        End If
                    End If
                End If
                'objCommand.CommandText = ""
                'objCommand.CommandText = "Update QuotationDetailTable Set DeliveredQty =IsNull(DeliveredQty,0)+IsNull(SO.SOQty,0) From QuotationDetailTable,(Select QuotationDetailId, ArticleDefId, SUM(IsNull(Sz1,0)) as SOQty From SalesOrderDetailTable WHERE SalesOrderID=" & getVoucher_Id & " AND IsNull(QuotationDetailId,0) <> 0 Group By QuotationDetailId,ArticleDefId) as SO WHERE SO.QuotationDetailId = QuotationDetailTable.QuotationDetailId And SO.ArticleDefId = QuotationDetailTable.ArticleDefId"
                'objCommand.ExecuteNonQuery()

                'objCommand.CommandText = ""
                'objCommand.CommandText = "Update QuotationMasterTable Set Status=Case When IsNull(SO.BalanceQty,0) > 0 then 'Open' else 'Close' End  From QuotationMasterTable,(Select QuotationId, SUM(IsNull(Sz1,0)-IsNull(SOQuantity,0)) BalanceQty From QuotationDetailTable WHERE QuotationDetailId in(Select distinct QuotationDetailId From SalesOrderDetailTable WHERE SalesOrderID=" & getVoucher_Id & ")   Group By QuotationId  )  as SO WHERE SO.QuotationId = QuotationMasterTable.QuotationId "
                'objCommand.ExecuteNonQuery()


                ''==================================================================================
                '***********************
                'Inserting Debit Amount
                '***********************
                ' objCommand = New OleDbCommand
                ' objCommand.Connection = Con
                'If Not ServiceItem = "True" Then
                'If Val(grd.GetRows(i).Cells("Qty").Value) <> 0 Then
                '    If IsDiscountVoucher = False Then

                '        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                '        '                      & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & IIf(grd1.Rows(i).Cells("Unit").Value = "Loose", Val(grd1.Rows(i).Cells("Qty").Value) * Val(grd1.Rows(i).Cells("rate").Value), (Val(grd1.Rows(i).Cells("Qty").Value) * Val(grd1.Rows(i).Cells("PackQty").Value)) * Val(grd1.Rows(i).Cells("rate").Value)) & ", 0, N'" & grd1.Rows(i).Cells("item").Value & "(" & Val(grd1.Rows(i).Cells("Qty").Value) & ")' )"

                '        '********************* Change For Cost Center ********************************
                '        ' By Imran Ali 26-01-2012
                '        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                '        '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value)) & ", 0, N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ")' )"
                '        If Not IsDeliveryOrderAnalysis = True Then
                '            objCommand.CommandText = ""
                '            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction) " _
                '                                                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value)) & ", 0, N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & " X " & Math.Round(Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), 2) & ")', " & CostId & ", " & grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value & ")"


                '            'objCommand.Transaction = trans
                '            objCommand.ExecuteNonQuery()

                '            '***********************
                '            'Inserting Credit Amount
                '            '***********************
                '            'objCommand = New OleDbCommand
                '            ' objCommand.Connection = Con
                '            objCommand.CommandText = ""

                '            '******************* Change For Cost Center ******************
                '            ' By Imran Ali
                '            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                '            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value)) & ", N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ")' )"
                '            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction) " _
                '                                   & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value)) & ", N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & " X " & Math.Round(Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), 2) & ")', " & CostId & ", " & grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value & ")"

                '            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                '            '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("rate").Value)) + Val(Me.txtTaxTotal.Text), ((Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value)) * Val(grd.Rows(i).Cells("rate").Value)) + Val(Me.txtTaxTotal.Text)) & ", N'" & grd.Rows(i).Cells("item").Value & "(" & Val(grd.Rows(i).Cells("Qty").Value) & ")' )"
                '            ' objCommand.Transaction = trans
                '            objCommand.ExecuteNonQuery()
                '        Else
                '            ''''''''''''''''''' In Case Delivery Order Analysis ----------------------------------------------
                '            objCommand.CommandText = ""
                '            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction) " _
                '                                                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.NetBill).Value) - IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Tax).Value = 0, 0, (Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) / 100) * IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Unit).Text = "Loose", ((Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value + Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) * Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value), (((Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value * Me.grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value) + Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) * Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value))) & ", 0, N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & " X " & Math.Round(Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), 2) & ")', " & CostId & ", " & grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value & ")"


                '            'objCommand.Transaction = trans
                '            objCommand.ExecuteNonQuery()

                '            '***********************
                '            'Inserting Credit Amount
                '            '***********************
                '            'objCommand = New OleDbCommand
                '            ' objCommand.Connection = Con
                '            objCommand.CommandText = ""

                '            '******************* Change For Cost Center ******************
                '            ' By Imran Ali
                '            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                '            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value)) & ", N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ")' )"
                '            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction) " _
                '                                   & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.NetBill).Value) - IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Tax).Value = 0, 0, (Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) / 100) * IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Unit).Text = "Loose", ((Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value + Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) * Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value), (((Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value * Me.grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value) + Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) * Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value))) & ", N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & " X " & Math.Round(Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), 2) & ")', " & CostId & ", " & grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value & ")"

                '            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                '            '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("rate").Value)) + Val(Me.txtTaxTotal.Text), ((Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value)) * Val(grd.Rows(i).Cells("rate").Value)) + Val(Me.txtTaxTotal.Text)) & ", N'" & grd.Rows(i).Cells("item").Value & "(" & Val(grd.Rows(i).Cells("Qty").Value) & ")' )"
                '            ' objCommand.Transaction = trans
                '            objCommand.ExecuteNonQuery()

                '            '''''''''''''''''''''' Includ Discount Voucher ''''''''''''''''''''''''''''''''''''''''''
                '            objCommand.CommandText = ""
                '            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction) " _
                '                                                                                      & " VALUES(" & lngVoucherMasterId & ", 1, " & Val(DeliveryDiscountAccount) & ", " & IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Unit).Text = "Loose", (((Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value)) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Discount_Percentage).Value)) / 100), ((((Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value)) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Discount_Percentage).Value)) / 100)) & ", 0, N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & " X " & Math.Round(Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), 2) & ")', " & CostId & ", " & grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value & ")"
                '            objCommand.ExecuteNonQuery()

                '        End If


                '    Else

                '        objCommand.CommandText = ""
                '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction) " _
                '                                                                                  & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value)) & ", 0, N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & " X " & Math.Round(Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), 2) & ")', " & CostId & ", " & grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value & ")"
                '        objCommand.ExecuteNonQuery()

                '        objCommand.CommandText = ""
                '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction) " _
                '                                                                                  & " VALUES(" & lngVoucherMasterId & ", 1, " & Val(DeliveryDiscountAccount) & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * DiscountedPrice, (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * DiscountedPrice) & ", 0, N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & " X " & Math.Round(Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), 2) & ")', " & CostId & ", " & grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value & ")"
                '        objCommand.ExecuteNonQuery()

                '        objCommand.CommandText = ""
                '        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction) " _
                '                                                          & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value)) & ", N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & " X " & Math.Round(Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), 2) & ")', " & CostId & ", " & grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value & ")"
                '        objCommand.ExecuteNonQuery()

                '    End If
                'End If
                'Else
                '    '***********************
                '    'Inserting Debit Amount
                '    '***********************
                '    If IsDiscountVoucher = False Then
                '        If Not IsDeliveryOrderAnalysis = True Then
                '            'objCommand.CommandText = ""
                '            ''objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                '            ''                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value)) & ", 0, N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ")' )"

                '            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
                '            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value)) & ", 0, N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) & ")', " & CostId & ")"


                '            'objCommand.ExecuteNonQuery()

                '            ''***********************
                '            ''Inserting Credit Amount
                '            ''***********************
                '            'objCommand.CommandText = ""

                '            ''objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                '            ''                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", 0,  " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value)) & ", N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ")' )"

                '            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                '            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", 0,  " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value)) & ", N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) & ")', " & CostId & ")"

                '            ''objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                '            ''                      & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("rate").Value)) + Val(Me.txtTaxTotal.Text), ((Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value)) * Val(grd.Rows(i).Cells("rate").Value)) + Val(Me.txtTaxTotal.Text)) & ", N'" & grd.Rows(i).Cells("item").Value & "(" & Val(grd.Rows(i).Cells("Qty").Value) & ")' )"

                '            'objCommand.ExecuteNonQuery()

                '            'If UpdateVoucherEntry(7, txtPONo.Text, Me.dtpPODate.Value, "", Nothing, AccountId, Val(Me.cmbVendor.ActiveRow.Cells(0).Text.ToString), Val(Me.txtAmount.Text), Val(Me.txtAmount.Text), "Both", Me.Name, Me.txtPONo.Text, True) = False Then
                '            '    MsgBox("An error occured while updating voucher record could not updated")
                '            '    trans.Rollback()
                '            '    Update_Record = False
                '            '    Exit Function
                '            'End If
                '        Else
                '            objCommand.CommandText = ""
                '            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                '                                                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.NetBill).Value) - IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Tax).Value = 0, 0, (Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) / 100) * IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Unit).Text = "Loose", ((Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value + Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) * Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value), (((Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value * Me.grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value) + Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) * Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value))) & ", 0, N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & " X " & Math.Round(Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), 2) & ")', " & CostId & ")"


                '            'objCommand.Transaction = trans
                '            objCommand.ExecuteNonQuery()

                '            '***********************
                '            'Inserting Credit Amount
                '            '***********************
                '            'objCommand = New OleDbCommand
                '            ' objCommand.Connection = Con
                '            objCommand.CommandText = ""

                '            '******************* Change For Cost Center ******************
                '            ' By Imran Ali
                '            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                '            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value)) & ", N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ")' )"
                '            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
                '                                   & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.NetBill).Value) - IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Tax).Value = 0, 0, (Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) / 100) * IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Unit).Text = "Loose", ((Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value + Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) * Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value), (((Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value * Me.grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value) + Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) * Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value))) & ", N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & " X " & Math.Round(Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), 2) & ")', " & CostId & ")"

                '            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                '            '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & IIf(grd.Rows(i).Cells("Unit").Value = "Loose", (Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("rate").Value)) + Val(Me.txtTaxTotal.Text), ((Val(grd.Rows(i).Cells("Qty").Value) * Val(grd.Rows(i).Cells("PackQty").Value)) * Val(grd.Rows(i).Cells("rate").Value)) + Val(Me.txtTaxTotal.Text)) & ", N'" & grd.Rows(i).Cells("item").Value & "(" & Val(grd.Rows(i).Cells("Qty").Value) & ")' )"
                '            ' objCommand.Transaction = trans
                '            objCommand.ExecuteNonQuery()

                '            '''''''''''''''''''''' Includ Discount Voucher ''''''''''''''''''''''''''''''''''''''''''
                '            objCommand.CommandText = ""
                '            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                '                                                                                      & " VALUES(" & lngVoucherMasterId & ", 1, " & Val(DeliveryDiscountAccount) & ", " & IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.Unit).Text = "Loose", (((Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value)) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Discount_Percentage).Value)) / 100), ((((Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value)) * Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Discount_Percentage).Value)) / 100)) & ", 0, N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & " X " & Math.Round(Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), 2) & ")', " & CostId & ")"
                '            objCommand.ExecuteNonQuery()
                '        End If
                '        'Else
                '        '    objCommand.CommandText = ""
                '        '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
                '        '                                                     & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value)) & ", 0, N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) & ")', " & CostId & ")"

                '        '    objCommand.ExecuteNonQuery()

                '        '    objCommand.CommandText = ""
                '        '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
                '        '                                                     & " VALUES(" & lngVoucherMasterId & ", 1, " & Val(DeliveryDiscountAccount) & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) * DiscountedPrice, (Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * DiscountedPrice) & ", 0, N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) & ")', " & CostId & ")"

                '        '    objCommand.ExecuteNonQuery()

                '        '    objCommand.CommandText = ""
                '        '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                '        '                                                  & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", 0,  " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value), (Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) * Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value)) & ", N'" & grd.GetRows(i).Cells(EnumGridDetail.Item).Value & "  " & Me.grd.GetRows(i).Cells(EnumGridDetail.Size).Value & " " & "(" & Val(grd.GetRows(i).Cells(EnumGridDetail.ServiceQty).Value) & ")', " & CostId & ")"
                '        '    objCommand.ExecuteNonQuery()

                '        'End If
                '    End If
                'End If
            Next

            '    If flgMultipleSalesOrder = False Then
            '        objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
            '                                & " SET              DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) & ") " _
            '                                & " WHERE     (SalesOrderID = " & Me.cmbPo.SelectedValue & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ")"
            '        objCommand.ExecuteNonQuery()
            '    Else
            '        If Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) > 0 Then
            '            objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
            '                                   & " SET              DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) & ") " _
            '                                   & " WHERE     (SalesOrderID = " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ")"
            '            objCommand.ExecuteNonQuery()
            '        End If
            '    End If


            'objCommand.CommandText = ""
            'objCommand.CommandText = "Update QuotationDetailTable Set DeliveredQty =IsNull(DeliveredQty,0)+IsNull(SO.SOQty,0) From QuotationDetailTable,(Select QuotationDetailId, ArticleDefId, SUM(IsNull(Sz1,0)) as SOQty From SalesOrderDetailTable WHERE SalesOrderID=" & getVoucher_Id & " AND IsNull(QuotationDetailId,0) <> 0 Group By QuotationDetailId,ArticleDefId) as SO WHERE SO.QuotationDetailId = QuotationDetailTable.QuotationDetailId And SO.ArticleDefId = QuotationDetailTable.ArticleDefId"
            'objCommand.ExecuteNonQuery()

            'objCommand.CommandText = ""
            'objCommand.CommandText = "Update QuotationMasterTable Set Status=Case When IsNull(SO.BalanceQty,0) > 0 then 'Open' else 'Close' End  From QuotationMasterTable,(Select QuotationId, SUM(IsNull(Sz1,0)-IsNull(SOQuantity,0)) BalanceQty From QuotationDetailTable WHERE QuotationDetailId in(Select distinct QuotationDetailId From SalesOrderDetailTable WHERE SalesOrderID=" & getVoucher_Id & ")   Group By QuotationId  )  as SO WHERE SO.QuotationId = QuotationMasterTable.QuotationId "
            'objCommand.ExecuteNonQuery()


            '***********************
            '01-Feb-2011    Added for tax calculation
            '***********************
            'If Val(Me.txtTaxTotal.Text) > 0 Then

            '    'objCommand.CommandText = ""
            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
            '    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.txtTaxTotal.Text) & ", 0, 'Tax Ref: " & Me.txtPONo.Text & "' )"

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.txtTaxTotal.Text) & ", 0, 'Tax Ref: " & Me.txtPONo.Text & "', " & CostId & " )"
            '    'objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
            '    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & DeliveryTaxId & ", " & 0 & ",  " & Val(Me.txtTaxTotal.Text) & ", 'Tax Ref: " & Me.txtPONo.Text & "' )"
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
            '                                         & " VALUES(" & lngVoucherMasterId & ", 1, " & DeliveryTaxId & ", " & 0 & ",  " & Val(Me.txtTaxTotal.Text) & ", 'Tax Ref By " & Me.cmbVendor.Text & ": " & Me.txtPONo.Text & "', " & CostId & " )"


            '    ' objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()

            'End If
            ''
            ''SED Tax Apply
            ''
            ''01-07-2012 
            'If Val(Me.txtSEDTaxAmount.Text) > 0 Then

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.txtSEDTaxAmount.Text) & ", 0, 'W.H Tax Ref: " & Me.txtPONo.Text & "', " & CostId & " )"

            '    'objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
            '    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtFuel.Text) & ", 'Fuel Ref: " & Me.txtPONo.Text & "' )"

            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
            '                         & " VALUES(" & lngVoucherMasterId & ", 1, " & Val(SEDAccountId) & ", " & 0 & ",  " & Val(Me.txtSEDTaxAmount.Text) & ", 'W.H Tax Ref By " & Me.cmbVendor.Text & ": " & Me.txtPONo.Text & "', " & CostId & " )"
            '    ' objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            'End If

            'If Val(Me.txtAddTransitInsurance.Text) > 0 Then

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.txtAddTransitInsurance.Text) & ", 0, 'Transit Insurace Ref: " & Me.txtPONo.Text & "', " & CostId & " )"

            '    'objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
            '    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtFuel.Text) & ", 'Fuel Ref: " & Me.txtPONo.Text & "' )"

            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
            '                         & " VALUES(" & lngVoucherMasterId & ", 1, " & Val(InsuranceAccountId) & ", " & 0 & ",  " & Val(Me.txtAddTransitInsurance.Text) & ", 'Transit Insurace Ref By " & Me.cmbVendor.Text & ": " & Me.txtPONo.Text & "', " & CostId & " )"
            '    ' objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            'End If

            ''***********************

            ''***********************
            ''06-Dec-2011    Added for Fuel, Other Expense, Adjustment
            ''***********************


            ' ''Fuel
            'If Val(Me.txtFuel.Text) > 0 Then

            '    'objCommand.CommandText = ""
            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
            '    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.FuelExpAccount & ", " & Val(Me.txtFuel.Text) & ", 0, 'Fuel Ref: " & Me.txtPONo.Text & "' )"

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.FuelExpAccount & ", " & Val(Me.txtFuel.Text) & ", 0, 'Fuel Ref By " & Me.cmbVendor.Text & ": " & Me.txtPONo.Text & "', " & CostId & " )"

            '    'objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
            '    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtFuel.Text) & ", 'Fuel Ref: " & Me.txtPONo.Text & "' )"

            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
            '                         & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtFuel.Text) & ", 'Fuel Ref: " & Me.txtPONo.Text & "', " & CostId & " )"
            '    ' objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()

            'End If
            ' ''end Fuel


            ' ''Other Exp
            'If Val(Me.txtExpense.Text) > 0 Then

            '    'objCommand.CommandText = ""
            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
            '    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.OtherExpAccount & ", " & Val(Me.txtExpense.Text) & ", 0, 'Expense Ref: " & Me.txtPONo.Text & "' )"

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.OtherExpAccount & ", " & Val(Me.txtExpense.Text) & ", 0, 'Expense Ref By " & Me.cmbVendor.Text & ": " & Me.txtPONo.Text & "', " & CostId & " )"

            '    'objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
            '    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtExpense.Text) & ", 'Expense Ref: " & Me.txtPONo.Text & "' )"

            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
            '                         & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtExpense.Text) & ", 'Expense Ref: " & Me.txtPONo.Text & "', " & CostId & " )"
            '    ' objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()

            'End If
            ' ''end Other Exp

            ' ''Adjustment
            'If Val(Me.txtAdjustment.Text) > 0 Then

            '    'objCommand.CommandText = ""
            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
            '    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.AdjustmentExpAccount & ", " & Val(Me.txtAdjustment.Text) & ", 0, 'Adjustment Ref: " & Me.txtPONo.Text & "' )"

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.AdjustmentExpAccount & ", " & Val(Me.txtAdjustment.Text) & ", 0, 'Adjustment Ref By " & Me.cmbVendor.Text & ": " & Me.txtPONo.Text & "', " & CostId & " )"

            '    'objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
            '    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtAdjustment.Text) & ", 'Adjustment Ref: " & Me.txtPONo.Text & "' )"
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
            '                                         & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtAdjustment.Text) & ", 'Adjustment Ref: " & Me.txtPONo.Text & "', " & CostId & " )"

            '    ' objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()
            'ElseIf Val(Me.txtAdjustment.Text) < 0 Then
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.AdjustmentExpAccount & ", 0, " & Math.Abs(Val(Me.txtAdjustment.Text)) & ", 'Adjustment Ref By " & Me.cmbVendor.Text & ": " & Me.txtPONo.Text & "', " & CostId & " )"

            '    'objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
            '    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtAdjustment.Text) & ", 'Adjustment Ref: " & Me.txtPONo.Text & "' )"
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
            '                                         & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & Math.Abs(Val(Me.txtAdjustment.Text)) & ", 0, 'Adjustment Ref: " & Me.txtPONo.Text & "', " & CostId & " )"

            '    ' objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()

            'End If
            ' ''end Adjustment

            ''***********************

            ''
            '' Receipt Voucher Master Information 
            'If ReceiptVoucherFlg = "True" AndAlso Val(Me.txtRecAmount.Text) <> 0 Then
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '                               & " cheque_no, cheque_date,post,Source,voucher_code, coa_detail_id)" _
            '                               & " VALUES(" & Me.cmbCompany.SelectedValue & ", 1,  " & Convert.ToInt32(Me.cmbMethod.SelectedValue) & ", N'" & VoucherNo & "', N'" & Me.dtpPODate.Value & "', " _
            '                               & " " & IIf(Me.txtChequeNo.Visible = True, "N'" & Me.txtChequeNo.Text & "'", "NULL") & ", " & IIf(Me.dtpChequeDate.Visible = True, "N'" & dtpChequeDate.Value & "'", "NULL") & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ",N'" & Me.Name & "',N'" & Me.txtPONo.Text & "', " & Me.cmbDepositAccount.SelectedValue & ")" _
            '                               & " SELECT @@IDENTITY"

            '    'objCommand.Transaction = trans
            '    lngVoucherMasterId = objCommand.ExecuteScalar


            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
            '                           & " VALUES(" & lngVoucherMasterId & ", " & Me.cmbCompany.SelectedValue & ", " & Me.cmbDepositAccount.SelectedValue & ", " & Val(Me.txtRecAmount.Text) & ", 0, 'Receipt Ref By " & Me.cmbVendor.Text & ": " & Me.txtPONo.Text & "', " & CostId & " )"

            '    'objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            '    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
            '    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtAdjustment.Text) & ", 'Adjustment Ref: " & Me.txtPONo.Text & "' )"
            '    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
            '                                         & " VALUES(" & lngVoucherMasterId & ", " & Me.cmbCompany.SelectedValue & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtRecAmount.Text) & ", 'Receipt Ref: " & Me.txtPONo.Text & "', " & CostId & " )"

            '    ' objCommand.Transaction = trans
            '    objCommand.ExecuteNonQuery()


            'End If


            If Mode = "Normal" Then
                ''TFS3520 : Ayesha Rehman : 14-06-2018
                ''DCStatus and Delivered Status changes According to Configuration of Separate Closure
                If flgSOSeparateClosure = True Then
                    If flgMultipleSalesOrder = False Then
                        If Me.cmbPo.SelectedIndex > 0 Then
                            objCommand.CommandText = "Select IsNull(Sz1,0) as Qty , isnull(DCQty , 0) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                            Dim da As New OleDbDataAdapter(objCommand)
                            Dim dt As New DataTable
                            da.Fill(dt)
                            Dim blnEqual As Boolean = True

                            If dt.Rows.Count > 0 Then
                                For Each r As DataRow In dt.Rows
                                    If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
                                        blnEqual = False
                                        Exit For
                                    End If
                                Next
                            End If
                            ''Below lines are commented on 06-04-2018 against TASK TFS2942
                            'If blnEqual = True Then
                            '    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                            '    objCommand.ExecuteNonQuery()
                            'Else
                            '    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                            '    objCommand.ExecuteNonQuery()
                            'End If
                            If blnEqual = True Then
                                objCommand.CommandText = "Update SalesOrderMasterTable set DCStatus = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                                objCommand.ExecuteNonQuery()
                            Else
                                objCommand.CommandText = "Update SalesOrderMasterTable set DCStatus = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                                objCommand.ExecuteNonQuery()
                            End If
                        End If
                    Else

                        objCommand.CommandText = "Select DISTINCT ISNULL(SO_ID,0) as SO_ID From DeliveryChalanDetailTable WHERE DeliveryID=ident_current('DeliveryChalanMasterTable') AND ISNULL(Qty,0) <> 0"
                        Dim dtPO As New DataTable
                        Dim daPO As New OleDbDataAdapter(objCommand)
                        daPO.Fill(dtPO)
                        If dtPO IsNot Nothing Then
                            If dtPO.Rows.Count > 0 Then
                                For Each row As DataRow In dtPO.Rows

                                    'objCommand.CommandText = "Select SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & row("SO_ID") & " Having SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) > 0 "
                                    objCommand.CommandText = "Select isnull(Sz1,0)-isnull(DCQty , 0) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & row("SO_ID") & " And isnull(Sz1,0)-isnull(DCQty , 0) > 0 "

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
                                    ''Below lines are commented on 06-04-2018 against TASK TFS2942
                                    'If blnEqual1 = True Then
                                    '    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
                                    '    objCommand.ExecuteNonQuery()
                                    'Else
                                    '    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
                                    '    objCommand.ExecuteNonQuery()
                                    'End If
                                    If blnEqual1 = True Then
                                        objCommand.CommandText = "Update SalesOrderMasterTable set DCStatus = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
                                        objCommand.ExecuteNonQuery()
                                    Else
                                        objCommand.CommandText = "Update SalesOrderMasterTable set DCStatus = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
                                        objCommand.ExecuteNonQuery()
                                    End If
                                Next
                            End If
                        End If
                    End If
                Else
                    If flgMultipleSalesOrder = False Then
                        If Me.cmbPo.SelectedIndex > 0 Then
                            objCommand.CommandText = "Select IsNull(Sz1,0) as Qty , isnull(DeliveredQty , 0) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                            Dim da As New OleDbDataAdapter(objCommand)
                            Dim dt As New DataTable
                            da.Fill(dt)
                            Dim blnEqual As Boolean = True

                            If dt.Rows.Count > 0 Then
                                For Each r As DataRow In dt.Rows
                                    If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
                                        blnEqual = False
                                        Exit For
                                    End If
                                Next
                            End If
                            If blnEqual = True Then
                                objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                                objCommand.ExecuteNonQuery()
                            Else
                                objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                                objCommand.ExecuteNonQuery()
                            End If
                        End If
                    Else

                        objCommand.CommandText = "Select DISTINCT ISNULL(SO_ID,0) as SO_ID From DeliveryChalanDetailTable WHERE DeliveryID=ident_current('DeliveryChalanMasterTable') AND ISNULL(Qty,0) <> 0"
                        Dim dtPO As New DataTable
                        Dim daPO As New OleDbDataAdapter(objCommand)
                        daPO.Fill(dtPO)
                        If dtPO IsNot Nothing Then
                            If dtPO.Rows.Count > 0 Then
                                For Each row As DataRow In dtPO.Rows

                                    'objCommand.CommandText = "Select SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & row("SO_ID") & " Having SUM(isnull(Sz1,0)-isnull(DeliveredQty , 0)) > 0 "
                                    objCommand.CommandText = "Select isnull(Sz1,0)-isnull(DeliveredQty , 0) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & row("SO_ID") & " And isnull(Sz1,0)-isnull(DeliveredQty , 0) > 0 "

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
                                        objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
                                        objCommand.ExecuteNonQuery()
                                    Else
                                        objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
                                        objCommand.ExecuteNonQuery()
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If
            End If
            ''Start TFS3326 : Ayesha Rehman : 31-May-2018
            'TFS3520 : Ayesha Rehman : 14-06-2018
            If flgSOSeparateClosure = True Then
                If Mode = "Normal" Then
                    If flgMultipleSalesOrder = False Then
                        objCommand.CommandText = "Select [Status] from SalesOrderMasterTable where SalesOrderId = " & Me.cmbPo.SelectedValue & ""
                        Dim daStatus As New OleDbDataAdapter(objCommand)
                        Dim dtStatus As New DataTable
                        daStatus.Fill(dtStatus)
                        If dtStatus.Rows.Count > 0 Then
                            If dtStatus.Rows(0).Item("Status").ToString.Equals(EnumStatus.Close.ToString) Then
                                objCommand.CommandText = "Update DeliveryChalanMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where DeliveryId = ident_current('DeliveryChalanMasterTable') "
                                objCommand.ExecuteNonQuery()
                            End If
                        End If
                    Else
                        objCommand.CommandText = "Select DISTINCT ISNULL(SO_ID,0) as SO_ID From DeliveryChalanDetailTable WHERE DeliveryID=ident_current('DeliveryChalanMasterTable') AND ISNULL(Qty,0) <> 0"
                        Dim dtPO As New DataTable
                        Dim daPO As New OleDbDataAdapter(objCommand)
                        daPO.Fill(dtPO)
                        If dtPO IsNot Nothing Then
                            If dtPO.Rows.Count > 0 Then
                                For Each row As DataRow In dtPO.Rows
                                    objCommand.CommandText = "Select [Status] from SalesOrderMasterTable where SalesOrderId= " & row("SO_ID") & " "
                                    Dim daStatus As New OleDbDataAdapter(objCommand)
                                    Dim dtStatus As New DataTable
                                    daStatus.Fill(dtStatus)
                                    If dtStatus.Rows.Count > 0 Then
                                        If dtStatus.Rows(0).Item("Status").ToString.Equals(EnumStatus.Close.ToString) Then
                                            objCommand.CommandText = "Update DeliveryChalanMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where DeliveryId = ident_current('DeliveryChalanMasterTable') "
                                            objCommand.ExecuteNonQuery()
                                        End If
                                    End If
                                Next
                            End If
                        End If

                    End If
                End If
            End If
            ''End TFS3326 : Ayesha Rehman
            '' TASK TFS1378
            If DCStockImpact = True Then
                FillModel()
                Call New StockDAL().Add(StockMaster, trans)
            End If
            '' END TFS1378
            trans.Commit()
            Save = True
            If Mode = "Normal" Then
                SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Sales, Me.txtPONo.Text.Trim, True)
                SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.txtPONo.Text, True)
            End If

            If Not Mode = "Normal" Then
                Me.BackgroundWorker1.RunWorkerAsync()
            End If
            'Call Save1() 'Upgrading Stock here ...
            setEditMode = False
            'Total_Amount = Val(Me.txtAmount.Text) 'Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)

            '...................... Send SMS .............................

            If GetSMSConfig("SMS To Location On Delivery Chalan").Enable = True Then
                Dim strSMSBody As String = String.Empty
                Dim objData As DataTable = CType(Me.grd.DataSource, DataTable)
                Dim dt_Loc As DataTable = objData.DefaultView.ToTable("Default", True, "LocationId")
                Dim drData() As DataRow
                For j As Integer = 0 To dt_Loc.Rows.Count - 1

                    strSMSBody = String.Empty
                    'Rafay
                    strSMSBody += "Delivery Chalan, Doc No: " & Me.txtPONo.Text & ", Doc Date: " & Me.dtpPODate.Value.ToShortDateString & ", Customer: " & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString & ", Bilty No: " & Me.uitxtBiltyNo.Text & ", Remarks:" & Me.txtRemarks.Text & ",PO_NO:" & Me.txtPO.Text & ", "
                    drData = objData.Select("LocationId=" & dt_Loc.Rows(j).Item(0).ToString & "")
                    Dim dblTotalQty As Double = 0D
                    Dim dblTotalAmount As Double = 0D
                    Dim strEngineNo As String = String.Empty
                    For Each dr As DataRow In drData
                        dblTotalQty += IIf(dr(EnumGridDetail.Unit).ToString = "Loose", Val(dr(EnumGridDetail.Qty).ToString), Val(dr(EnumGridDetail.Qty).ToString) * Val(dr(EnumGridDetail.PackQty).ToString))
                        strSMSBody += " " & dr(EnumGridDetail.Item).ToString & ", " & IIf(dr(EnumGridDetail.Unit).ToString = "Loose", " " & dr(EnumGridDetail.Qty).ToString & "", "" & (Val(dr(EnumGridDetail.Qty)).ToString * Val(dr(EnumGridDetail.PackQty)).ToString) & "") & ", "
                        If strEngineNo.Length = 0 Then
                            strEngineNo = " " & dr(EnumGridDetail.Engine_No).ToString & ""
                        Else
                            strEngineNo += " ," & dr(EnumGridDetail.Engine_No).ToString & ""
                        End If
                    Next
                    strSMSBody += " Total Qty: " & dblTotalQty & ""
                    If strEngineNo.Length > 0 Then
                        strSMSBody += "Engine No: " & strEngineNo & ""
                    End If
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

            If GetSMSConfig("Delivery Chalan").Enable = True Then
                If msg_Confirm(str_ConfirmSendSMSMessage) = False Then Exit Try
                Dim strDetailMessage As String = String.Empty
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    'Task#08082015 Add configuration for sending sms with just item qunatity and engine no
                    If getConfigValueByType("DeliveryChalanByEnigneNo").ToString = "True" Then
                        If strDetailMessage.Length = 0 Then
                            'Marked  by Ali Ansari for removing item qty
                            'strDetailMessage = "Item Qty: " & IIf(r.Cells(EnumGridDetail.Unit).Value.ToString = "Loose", Val(r.Cells(EnumGridDetail.Qty).Value.ToString), Val(r.Cells(EnumGridDetail.Qty).Value.ToString) * Val(r.Cells(EnumGridDetail.PackQty).Value.ToString)) & IIf(r.Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0, ", Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString, "")
                            'Marked  by Ali Ansari for removing item qty
                            'Altered  by Ali Ansari for removing item qty
                            strDetailMessage = "Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString

                            'Altered  by Ali Ansari for removing item qty
                        Else
                            'Marked  by Ali Ansari for removing item qty
                            'strDetailMessage += ", Item Qty: " & IIf(r.Cells(EnumGridDetail.Unit).Value.ToString = "Loose", Val(r.Cells(EnumGridDetail.Qty).Value.ToString), Val(r.Cells(EnumGridDetail.Qty).Value.ToString) * Val(r.Cells(EnumGridDetail.PackQty).Value.ToString)) & IIf(r.Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0, ", Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString, "")
                            'Marked  by Ali Ansari for removing item qty
                            'Altered  by Ali Ansari for removing item qty
                            strDetailMessage += ", Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString
                            'Altered  by Ali Ansari for removing item qty
                        End If
                        'End Task#08082015
                    Else
                        If strDetailMessage.Length = 0 Then
                            'strDetailMessage = r.Cells(EnumGridDetail.Item).Value.ToString & ", Qty: " & IIf(r.Cells(EnumGridDetail.Unit).Value.ToString = "Loose", Val(r.Cells(EnumGridDetail.Qty).Value.ToString), Val(r.Cells(EnumGridDetail.Qty).Value.ToString) * Val(r.Cells(EnumGridDetail.PackQty).Value.ToString)) & IIf(r.Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0, ", Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString, "")
                            strDetailMessage = r.Cells(EnumGridDetail.Item).Value.ToString & ", PackQty: " & Val(r.Cells(EnumGridDetail.PackQty).Value.ToString) & ", Item Qty: " & IIf(r.Cells(EnumGridDetail.Unit).Value.ToString = "Loose", Val(r.Cells(EnumGridDetail.Qty).Value.ToString), Val(r.Cells(EnumGridDetail.Qty).Value.ToString) * Val(r.Cells(EnumGridDetail.PackQty).Value.ToString)) & IIf(r.Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0, ", Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString, "")
                        Else
                            'strDetailMessage += "," & r.Cells(EnumGridDetail.Item).Value.ToString & ", Qty: " & IIf(r.Cells(EnumGridDetail.Unit).Value.ToString = "Loose", Val(r.Cells(EnumGridDetail.Qty).Value.ToString), Val(r.Cells(EnumGridDetail.Qty).Value.ToString) * Val(r.Cells(EnumGridDetail.PackQty).Value.ToString)) & IIf(r.Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0, ", Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString, "")
                            strDetailMessage += "," & r.Cells(EnumGridDetail.Item).Value.ToString & ", PackQty: " & Val(r.Cells(EnumGridDetail.PackQty).Value.ToString) & ", Item Qty: " & IIf(r.Cells(EnumGridDetail.Unit).Value.ToString = "Loose", Val(r.Cells(EnumGridDetail.Qty).Value.ToString), Val(r.Cells(EnumGridDetail.Qty).Value.ToString) * Val(r.Cells(EnumGridDetail.PackQty).Value.ToString)) & IIf(r.Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0, ", Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString, "")
                        End If
                    End If

                Next
                Dim objTemp As New SMSTemplateParameter
                Dim obj As Object = GetSMSTemplate("Delivery Chalan")
                If obj IsNot Nothing Then
                    objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                    Dim strMessage As String = objTemp.SMSTemplate
                    strMessage = strMessage.Replace("@AccountTitle", Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString).Replace("@AccountCode", Me.cmbVendor.ActiveRow.Cells("Code").Value.ToString).Replace("@DocumentNo", Me.txtPONo.Text).Replace("@DocumentDate", Me.dtpPODate.Value.ToShortDateString).Replace("@OtherDoc", Me.uitxtBiltyNo.Text).Replace("@Remarks", Me.txtRemarks.Text).Replace("@Amount", Me.grd.GetTotal(grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@Quantity", Me.grd.GetTotal(grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@CompanyName", CompanyTitle).Replace("@SIRIUS", "Automated by www.siriussolution.com").Replace("@DetailInformation", strDetailMessage).Replace("@BiltyNo", Me.uitxtBiltyNo.Text).Replace("@DriverName", Me.txtDriverName.Text).Replace("@PreviousBalance", _Previous_Balance).Replace("@VehicleNo", Me.txtVehicleNo.Text).Replace("@Transporter", CStr(IIf(Me.cmbTransporter.SelectedIndex > 0, Me.cmbTransporter.Text, "")))
                    SaveSMSLog(strMessage, Me.cmbVendor.ActiveRow.Cells("Mobile").Value.ToString, "Delivery Chalan")

                    If GetSMSConfig("Delivery Chalan").EnabledAdmin = True Then
                        For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                            If strMob.Length > 10 Then
                                SaveSMSLog(strMessage, strMob, "Delivery Chalan")
                            End If
                        Next
                    End If
                End If
            End If
            '...................... End Send SMS ................


            ''Start TFS3113
            ''insert Approval Log
            SaveApprovalLog(EnumReferenceType.DeliveryChallan, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Delivery Chalan," & cmbVendor.Text & "", Me.Name, 7)
            ''End TFS3113

            Dim ValueTable As DataTable = GetSingle(getVoucher_Id)
            NotificationDAL.SaveAndSendNotification("Delivery Chalan", "DeliveryChalanMasterTable", getVoucher_Id, ValueTable, "Sales > Delivery Chalan")
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
            'SaveVoucherEntry(GetVoucherTypeId("SV"), GetNextDocNo("SV", 6, "tblVoucher", "voucher_no"), Me.dtpPODate.Value, "", Nothing, GetConfigValue("DeliveryCreditAccount"), Val(Me.cmbVendor.ActiveRow.Cells(0).Text.ToString), Val(Me.txtAmount.Text), Val(Me.txtAmount.Text), "Both", Me.Name, Me.txtPONo.Text, True)
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try

    End Sub

    Private Function FormValidate() As Boolean


        If txtPONo.Text = "" Then
            msg_Error("You must enter Invoice No.")
            txtPONo.Focus() : FormValidate = False : Exit Function
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


        If Me.Mode = "Normal" Then

            'If Me.cmbSalesMan.SelectedIndex <= 0 Then
            'msg_Error("You must select Delivery Person")
            '  Me.cmbSalesMan.Focus() : FormValidate = False : Exit Function
            '   End If

            ''Task# A1-10-06-2015 Check Vendor exist in combox list or not
            If Not Me.cmbVendor.IsItemInList Then
                ShowErrorMessage("Please must select the Customer name")
                Me.cmbVendor.Focus()
                Return False
            End If

            If Me.cmbVendor.Text = String.Empty Then
                ShowErrorMessage("Please must select the Customer name")
                Me.cmbVendor.Focus()
                Return False
            End If
            ''End Task# A1-10-06-2015

            If cmbVendor.ActiveRow.Cells(0).Value <= 0 Then
                msg_Error("You must Select Customer")
                cmbVendor.Focus() : FormValidate = False : Exit Function
            End If




            If Not IsDBNull(cmbVendor.ActiveRow.Cells("ExpiryDate").Value) Then
                If cmbVendor.ActiveRow.Cells("ExpiryDate").Value < Me.dtpPODate.Value Then
                    If Not msg_Confirm("Selected Customer has been expired" & (Chr(13)) & "Do you want to save?") = True Then
                        cmbVendor.Focus() : FormValidate = False : Exit Function
                    Else
                        Return True
                    End If
                End If
            End If
            If Val(cmbVendor.ActiveRow.Cells("Limit").Value.ToString) > 0 Then
                Dim CurrentBalance As Integer = GetAccountBalance(Me.cmbVendor.ActiveRow.Cells(0).Value)

                If CurrentBalance + Val(Me.txtTotal.Text) > Val(cmbVendor.ActiveRow.Cells("Limit").Value.ToString) Then
                    msg_Error("Credit Limit Of Customer is Over ")
                    cmbVendor.Focus() : FormValidate = False : Exit Function
                End If
            End If

            ' If Me.cmbTransporter.SelectedIndex <= 0 Then
            'msg_Error("You must select Transportor")
            'Me.cmbTransporter.Focus() : FormValidate = False : Exit Function
            'End If
        End If

        If Not Me.grd.RecordCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            cmbItem.Focus() : FormValidate = False : Exit Function
        End If
        'Murtaza Change default currency rate required by Waqar bhai(10/25/2022) 
        If cmbCurrency.SelectedValue <> BaseCurrencyId AndAlso Val(txtCurrencyRate.Text) = 1 Then
            msg_Error(cmbCurrency.Text + "Currency Rate cannot be 1")
            txtCurrencyRate.Focus() : FormValidate = False : Exit Function
        End If
        'Murtaza Change default currency rate required(10/25/2022)


        'Dim CurrentBalance As Integer = GetAccountBalance(Me.cmbVendor.ActiveRow.Cells(0).Value)
        'Comment against task:2388

        'Dim IsMinus As Boolean = getConfigValueByType("AllowMinusStock")

        'If IsMinus = False Then Return True
        'End Task:2388
        'Dim cmd As New OleDbCommand
        'cmd.CommandType = CommandType.Text
        'cmd.Connection = Con
        'Dim da As New OleDbDataAdapter(cmd)
        'Dim dt As New DataTable
        'If Con.State = ConnectionState.Closed Then Con.Open()


        'cmd.CommandText = "SELECT Stock , articleid , BatchNo    FROM         dbo.vw_Batch_Stock "
        'da.Fill(dt)
        Me.grd.UpdateData()
        If Me.IsEditMode = False Then
            If Me.cmbPo.SelectedIndex > 0 Then
                ' If dt.Rows.Count > 0 Then
                'Dim dv As DataView = dt.DefaultView
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    'dv.RowFilter = "articleid = " & r.Cells(EnumGridDetail.ArticleID).Value & " and BatchNo = N'" & r.Cells(EnumGridDetail.BatchNo).Value & "'"
                    'dv.RowFilter = "articleid = " & r.Cells(EnumGridDetail.ArticleID).Value & ""
                    'If dv.Count > 0 Then
                    'If r.Cells("Qty").Value <= 0 Then
                    '    msg_Error("Qty must be greate than 0")
                    '    Return False
                    'End If

                    If r.Cells("Rate").Value <= 0 Then
                        msg_Error("Price is not greate than 0")
                        Return False
                    End If

                    'If r.Cells("Qty").Value > dv(0)("Stock") Then
                    '    msg_Error("item : " & r.Cells("Item").Value & " out of stock. available is " & dv(0)("Stock"))
                    '    r.Selected = True
                    '    Return False
                    'End If
                    'End If
                Next
                'End If
            End If
        Else

            If Me.cmbPo.SelectedIndex > 0 Then
                'If dt.Rows.Count > 0 Then
                'Dim dv As DataView = dt.DefaultView
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    'dv.RowFilter = "articleid = " & r.Cells("articledefid").Value & " and BatchNo = N'" & r.Cells("Batch No").Value & "'"
                    'dv.RowFilter = "articleid = " & r.Cells("articledefid").Value & ""
                    'If dv.Count > 0 Then
                    'If r.Cells("Qty").Value <= 0 Then
                    '    msg_Error("Qty must be greate than 0")
                    '    Return False
                    'End If

                    If r.Cells("Rate").Value <= 0 Then
                        msg_Error("Price is not greate than 0")
                        Return False
                    End If

                    'If r.Cells("Qty").Value > r.Cells("SavedQty").Value + dv(0)("Stock") Then
                    '    msg_Error("item : " & r.Cells("Item").Value & " out of stock. available is " & dv(0)("Stock"))
                    '    r.Selected = True
                    '    Return False
                    'End If
                    'End If
                Next
                'End If
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



        'Task:2796 Apply Validation On Order Qty Exceed.
        If blnOrderQtyExceed = True Then
            If Me.cmbPo.SelectedIndex > 0 Then
                Dim objDt As New DataTable
                For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    objDt = GetDataTable("SELECT SO_DT.ArticleDefId, SUM(ISNULL(SO_DT.Qty,0)) AS Qty, SUM(IsNull(DC_DT.Qty,0))  as DTQty FROM dbo.SalesOrderDetailTable AS SO_DT LEFT OUTER JOIN dbo.DeliveryChalanMasterTable AS DC ON SO_DT.SalesOrderId = DC.POId LEFT OUTER JOIN (SELECT DeliveryId, ArticleDefId, IsNull(Qty, 0) AS Qty FROM DeliveryChalanDetailTable " & IIf(Me.BtnSave.Text = "&Update", " WHERE DeliveryId <> " & Val(Me.txtReceivingID.Text) & "", "") & ") DC_DT ON DC_DT.DeliveryId = DC.DeliveryId AND  DC_DT.ArticleDefID = SO_DT.ArticleDefId WHERE SO_DT.SalesOrderId=" & Row.Cells(EnumGridDetail.SO_ID).Value & " GROUP BY SO_DT.ArticleDefId, DC.POId")
                    If objDt IsNot Nothing Then
                        If objDt.Rows.Count > 0 Then
                            For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                                For Each r As DataRow In objDt.Rows
                                    If grdRow.Cells(EnumGridDetail.ArticleID).Value = Val(r.Item("ArticleDefId").ToString) Then
                                        If (Val(grdRow.Cells(EnumGridDetail.Qty).Value.ToString) + Val(r.Item("DTQty").ToString)) > Val(r.Item("Qty").ToString) Then
                                            ShowErrorMessage("Order [" & grdRow.Cells(EnumGridDetail.Item).Value.ToString & "] qty exceed")
                                            grd.Focus()
                                            Return False
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
        Dim StockOutConfigration As String = "" ''1596
        ''Start Task 1596
        If Not getConfigValueByType("StockOutConfigration").ToString = "Error" Then ''1596
            StockOutConfigration = getConfigValueByType("StockOutConfigration").ToString
        End If
        'ShowInformationMessage(StockOutConfigration)
        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            If StockOutConfigration.Equals("Required") AndAlso r.Cells(EnumGridDetail.BatchNo).Value.ToString = String.Empty Then
                msg_Error("Please Enter Value in Batch No")
                Return False
                Exit For
            End If
        Next
        'End Task:1596

        Return True

    End Function
    Sub EditRecord()
        Try
            If Not Me.grdSaved.RecordCount > 0 Then Exit Sub
            If Me.grd.RecordCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.IsEditMode = True
            Me.BtnSave.Text = "&Update"
            'Me.Mode = "Edit"
            'FillCombo("Vendor")'R933 Commented
            ' Me.FillCombo("SOComplete")
            txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value.ToString
            dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            '  dtpArrivalTime.Value = CType(grdSaved.CurrentRow.Cells("Arrival_Time").Value, Date)
            ' dtpDepartureTime.Value = IIf(grdSaved.CurrentRow.Cells("Departure_Time").Value = DBNull, Date.Now, CType(grdSaved.CurrentRow.Cells("Departure_Time").Value, Date))

            If IsDBNull(Me.grdSaved.GetRow.Cells("Arrival_Time").Value) Then
                Me.dtpArrivalTime.Value = Date.Now
            Else
                Me.dtpArrivalTime.Value = Me.grdSaved.GetRow.Cells("Arrival_Time").Value
            End If


            If IsDBNull(Me.grdSaved.GetRow.Cells("Departure_Time").Value) Then
                Me.dtpDepartureTime.Value = Date.Now
                Me.dtpDepartureTime.Checked = False
            Else
                Me.dtpDepartureTime.Value = Me.grdSaved.GetRow.Cells("Departure_Time").Value
                Me.dtpDepartureTime.Checked = True
            End If
            txtReceivingID.Text = grdSaved.CurrentRow.Cells("DeliveryId").Value
            'TODO. ----
            cmbVendor.Value = grdSaved.CurrentRow.Cells("CustomerCode").Value
            'R933 Validate Customer
            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Customer is disable.")
                Exit Sub
            End If
            cmbCompany.SelectedValue = grdSaved.CurrentRow.Cells("LocationId").Value
            cmbCompany.Enabled = False
            txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value.ToString
            'Rafay
            txtPO.Text = grdSaved.CurrentRow.Cells("PO_NO").Value.ToString
            txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value.ToString
            Me.uitxtBiltyNo.Text = grdSaved.CurrentRow.Cells("BiltyNo").Value.ToString
            Me.cmbSalesMan.SelectedValue = grdSaved.CurrentRow.Cells("EmployeeCode").Value.ToString
            Me.txtVehicleNo.Text = Me.grdSaved.GetRow.Cells("Vehicle_No").Value.ToString
            Me.txtDriverName.Text = Me.grdSaved.GetRow.Cells("Driver_Name").Value.ToString
            Me.btnAttachment.Text = "Attachment (" & Me.grdSaved.GetRow.Cells("No Of Attachment").Value.ToString & ")"
            'Dim chkPO As DataTable = CType(Me.cmbPo.DataSource, DataTable)
            'Dim drChkPO() As DataRow
            'drChkPO = chkPO.Select("SalesOrderNo=N'" & Me.grdSaved.CurrentRow.Cells("SalesOrderNo").Value.ToString & "'")
            Me.cmbPo.SelectedValue = Val(Me.grdSaved.GetRow.Cells("POID").Value.ToString)
            Me.IsSO = False
            If Val(Me.grdSaved.GetRow.Cells("POID").Value.ToString) > 0 Then
                Me.IsSO = True
            End If
            If cmbPo.SelectedValue Is Nothing Then
                Dim dt As New DataTable
                dt = CType(Me.cmbPo.DataSource, DataTable)
                If Not dt Is Nothing Then
                    dt.AcceptChanges()
                    'dt.Columns.Add("SalesOrderID", GetType(System.Int32))
                    'dt.Columns.Add("SalesOrderNo", GetType(System.String))
                    Dim dr As DataRow
                    dr = dt.NewRow
                    dr(0) = Me.grdSaved.CurrentRow.Cells("PoId").Value
                    dr(1) = Me.grdSaved.CurrentRow.Cells("SalesOrderNo").Value
                    dt.Rows.Add(dr)
                    dt.AcceptChanges()
                    'Me.cmbPo.DataSource = dt
                    'Me.cmbPo.DisplayMember = "SalesOrderNo"
                    'Me.cmbPo.ValueMember = "SalesOrderID"
                    'dt = Nothing
                    Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value
                End If
            End If
            Me.cmbTransporter.SelectedValue = Me.grdSaved.CurrentRow.Cells("TransporterId").Value
            'Me.txtExpense.Text = Me.grdSaved.CurrentRow.Cells("Expense").Value.ToString
            'Me.txtFuel.Text = Me.grdSaved.CurrentRow.Cells("Fuel").Value.ToString
            'Me.txtAddTransitInsurance.Text = Me.grdSaved.CurrentRow.Cells("TransitInsurance").Value.ToString
            Try
                Me.txtAdjustment.Text = Val(Me.grdSaved.CurrentRow.Cells("Adjustment").Value.ToString())
            Catch ex As Exception
            End Try
            Me.cmbOtherCompany.Text = Me.grdSaved.GetRow.Cells("Other_Company").Value.ToString
            Me.BtnSave.Text = "&Update"
            Call DisplayDetail(grdSaved.CurrentRow.Cells("DeliveryId").Value)
            GetTotal()
            If Val(grdSaved.CurrentRow.Cells("JobCardId").Value.ToString) > 0 Then
                GetJobCard(Val(grdSaved.CurrentRow.Cells("JobCardId").Value.ToString))
                Me.gbJobCard.Visible = True
            Else
                Me.gbJobCard.Visible = False
            End If
            'Me.ExistingBalance = Val(Me.txtAmount.Text)
            'Me.BtnSave.Text = "&Update"d
            'Me.LinkLabel1.Enabled = False
            Me.cmbPo.Enabled = False
            If Me.cmbPo.SelectedValue > 0 Then
                Me.cmbVendor.Enabled = False
            Else
                Me.cmbVendor.Enabled = True
            End If
            If flgMultipleSalesOrder = False Then
                If getConfigValueByType("AllowChangeSO").ToString.ToUpper = "TRUE" Then
                    Me.cmbPo.Enabled = True
                Else
                    Me.cmbPo.Enabled = False
                End If
            End If

            ''Abubakar Siddiq :TFS3113 :Making Approval Button Enable in Edit Mode
            Me.btnApprovalHistory.Visible = True
            Me.btnApprovalHistory.Enabled = True
            ''Abubakar Siddiq :TFS3113 :End

            Me.chkDelivered.Checked = Me.grdSaved.GetRow.Cells("Delivered").Value
            'txtTransitInsurancePercentage_TextChanged(Nothing, Nothing)
            'If Val(Me.txtAddTransitInsurance.Text) <> 0 Then
            '    Me.txtTransitInsurancePercentage.Text = Math.Round((Val(Me.txtAddTransitInsurance.Text) / (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(EnumGridDetail.NetBill), Janus.Windows.GridEX.AggregateFunction.Sum)) + Val(Me.txtSEDTaxAmount.Text))) * 100, 3)
            'End If
            If Not Mode = "Normal" Then Me.txtBarcode.Focus()
            Me.GetSecurityRights()
            Me.chkPost.Checked = grdSaved.CurrentRow.Cells("Post").Value
            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
            Me.CtrlGrdBar2_Load(Nothing, Nothing)
            CtrlGrdBar2_Load_1(Nothing, Nothing)
            'VoucherDetail(Me.txtPONo.Text)
            'Previouse_Amount = Val(Me.txtAmount.Text)
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
            'Altered Against Task# 2015060001 Ali Ansari
            'Get no of attached files
            Dim intCountAttachedFiles As Integer = 0I
            If Me.BtnSave.Text <> "&Save" Then
                If Me.grdSaved.RowCount > 0 Then
                    intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                    Me.btnAttachment.Text = "Attachment (" & intCountAttachedFiles & ")"
                End If
            End If
            ''Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnDelete.Visible = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayPODetail(ByVal ReceivingID As Integer)
        Dim str As String
        Dim i As Integer



        'str = "SELECT tblDefLocation.Location_code AS Category, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 - Isnull(DeliveredQty , 0) AS Qty, Recv_D.Price, " _
        '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price  ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price END AS Total, " _
        '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty ,Recv_D.Price as CurrentPrice,'xxxx' as BatchNo, Recv_D.LocationId  FROM SalesOrderDetailTable Recv_D INNER JOIN " _
        '      & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
        '      & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id " _
        '      & " Where Recv_D.SalesOrderID =" & ReceivingID & " AND Recv_D.Sz1 - Isnull(DeliveredQty , 0) > 0 "
        'Before against task:M16
        'str = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS item,  Recv_D.ArticleSize AS unit, Recv_D.Sz1 - Isnull(DeliveredQty , 0) AS Qty, Recv_D.Price, " _
        '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price  ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price END AS Total, " _
        '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty ,Recv_D.Price as CurrentPrice, 'xxxx' as BatchNo, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax, (ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as SampleQty, ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, 0 as So_Id, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc  FROM SalesOrderDetailTable Recv_D INNER JOIN " _
        '      & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
        '      & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderID = Recv_D.SalesOrderID" _
        '      & " Where Recv_D.SalesOrderID =" & ReceivingID & " AND Recv_D.Sz1 - Isnull(DeliveredQty , 0) > 0 "
        'Task:M16 Added Column Engine_No and Chassis_No.
        'str = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS item,  Recv_D.ArticleSize AS unit, Recv_D.Sz1 - Isnull(DeliveredQty , 0) AS Qty, Recv_D.Price, " _
        '     & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price  ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price END AS Total, " _
        '     & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty ,Recv_D.Price as CurrentPrice, 'xxxx' as BatchNo, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax, (ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as SampleQty, ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, 0 as So_Id, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No  FROM SalesOrderDetailTable Recv_D INNER JOIN " _
        '     & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
        '     & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderID = Recv_D.SalesOrderID" _
        '     & " Where Recv_D.SalesOrderID =" & ReceivingID & " AND Recv_D.Sz1 - Isnull(DeliveredQty , 0) > 0 "
        'End Task:M16

        'str = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS item,  Recv_D.ArticleSize AS unit, Recv_D.Sz1 - Isnull(DeliveredQty , 0) AS Qty, Recv_D.Price, " _
        '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price  ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price END AS Total, " _
        '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty ,Recv_D.Price as CurrentPrice, 'xxxx' as BatchNo, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax, (ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as SampleQty, ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, 0 as So_Id, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, '' as Comments, 0 as DeliveredQty  FROM SalesOrderDetailTable Recv_D INNER JOIN " _
        '    & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
        '    & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderID = Recv_D.SalesOrderID" _
        '    & " Where Recv_D.SalesOrderID =" & ReceivingID & " AND Recv_D.Sz1 - Isnull(DeliveredQty , 0) > 0 "
        'str = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS item,  Recv_D.ArticleSize AS unit, Recv_D.Sz1 - Isnull(DeliveredQty , 0) AS Qty, Recv_D.Price, " _
        '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price  ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price END AS Total, " _
        '  & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty ,Recv_D.Price as CurrentPrice,ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax, (ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as SampleQty, ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, 0 as So_Id, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, 0 as BatchId, 'xxxx' as BatchNo, Convert(DateTime,Null) as ExpiryDate, '' as Comments, 0 as DeliveredQty FROM SalesOrderDetailTable Recv_D INNER JOIN " _
        '  & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
        '  & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderID = Recv_D.SalesOrderID" _
        '  & " Where Recv_D.SalesOrderID =" & ReceivingID & " AND Recv_D.Sz1 - Isnull(DeliveredQty , 0) > 0 "


        'str = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS item,  Recv_D.ArticleSize AS unit, Recv_D.Sz1 - Isnull(DeliveredQty , 0) AS Qty, Recv_D.Price, " _
        '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price  ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price END AS Total, " _
        '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty ,Recv_D.Price as CurrentPrice,ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax, (ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as SampleQty, ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, 0 as So_Id, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, 0 as BatchId, 'xxxx' as BatchNo, Convert(DateTime,Null) as ExpiryDate, Recv_D.Comments as Comments, Recv_D.Other_Comments as [Other Comments], 0 as DeliveredQty FROM SalesOrderDetailTable Recv_D INNER JOIN " _
        '& " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
        '& " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderID = Recv_D.SalesOrderID" _
        '& " Where Recv_D.SalesOrderID =" & ReceivingID & " AND Recv_D.Sz1 - Isnull(DeliveredQty , 0) > 0 "


        'str = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS item,Article.ArticleSizeName as Size, Article.ArticleColorName as Color,  Recv_D.ArticleSize AS unit, Recv_D.Sz1 - Isnull(DeliveredQty , 0) AS Qty, Recv_D.Price as Rate, " _
        '& " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price  ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty - isnull(Recv_D.DeliveredQty,0)  * Recv_D.Price END AS Total, " _
        '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as [Pack Qty] ,Recv_D.Price as CurrentPrice,0 as DeliveryDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax,  Convert(float,0) as SED, Convert(float,0) as savedqty,(ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, 0 as So_Id, '' as UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0.0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, 0 as BatchId, 'xxxx' as BatchNo, Convert(DateTime,Null) as ExpiryDate, Recv_D.Comments as Comments, Recv_D.Other_Comments as [Other Comments], 0 as DeliveredQty, IsNull(Stock.CurrStock,0) as Stock FROM SalesOrderDetailTable Recv_D INNER JOIN " _
        '& " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
        '& " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderID = Recv_D.SalesOrderID  LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId  " _
        '& " Where Recv_D.SalesOrderID =" & ReceivingID & " AND Recv_D.Sz1 - Isnull(DeliveredQty , 0) > 0 "
        ''TASK TFS1496 addition column of PackPrice


        ''Task2827 : Added Column of Discount and PostDiscountPrice in Query for Discount factor Implementation
        'If Not flgLoadItemAfterDeliveredOnDC Then
        'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
        If flgSOSeparateClosure = False Then
            str = "SELECT Recv_D.LocationId, Article.ArticleId, Article.ArticleCode as Code, Article.ArticleDescription AS item, Recv_D.AlternativeItem, Article.ArticleSizeName as Size, Article.ArticleColorName as Color,  Recv_D.ArticleSize AS unit, ISNULL(Recv_D.Sz1,0) OrderQty, ISNULL(DeliveredQty,0) DeliverQty, (ISNULL(Recv_D.Sz1,0) - Isnull(DeliveredQty , 0)) AS Qty, ISNULL(Recv_D.Sz1,0) - ISNULL(DeliveredQty,0) RemainingQty,Recv_D.PostDiscountPrice, Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, " _
            & " ((IsNull(Recv_D.Qty, 0) * Recv_D.Price * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) - isnull(Recv_D.DeliveredTotalQty,0)  * Recv_D.Price * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total,Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue, " _
            & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as [Pack Qty] ,Recv_D.Price as CurrentPrice, IsNull(Recv_D.PackPrice, 0) as PackPrice, 0 as DeliveryDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax,  Convert(float,IsNull(Recv_D.SED_Tax_Percent,0)) as SED, Convert(float,0) as savedqty,(ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, 0 as So_Id, '' as UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0.0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, 0 as BatchId, ISNULL(Recv_D.BatchNo, 0) as BatchNo, ISNULL(Recv_D.ExpiryDate, 0) as ExpiryDate, ISNULL(Recv_D.Origin, '') as Origin, 0 AS [Bardana Deduction] , 0 AS [Other Deduction], 0 AS [After Deduction Qty], Recv_D.Comments as Comments, Recv_D.Other_Comments as [Other Comments], 0 as DeliveredQty, IsNull(Stock.CurrStock,0) as Stock, IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as DeliveryId, ISNULL(Recv_D.SalesOrderDetailId, 0) As SalesOrderDetailId,(IsNull(Recv_D.Qty,0)- IsNull(Recv_D.DeliveredTotalQty,0)) as TotalQty, ISNULL(Article.LogicalItem, 0) AS LogicalItem, 0 AS AdditionalItem, Recv_D.AlternativeItemId  FROM SalesOrderDetailTable Recv_D INNER JOIN " _
            & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderID = Recv_D.SalesOrderID  LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId  " _
            & " Where Recv_D.SalesOrderID =" & ReceivingID & " AND (IsNull(Recv_D.Sz1,0) - Isnull(DeliveredQty,0)) > 0 ORDER BY ISNULL(Recv_D.SalesOrderDetailId, 0)"
        Else
            str = "SELECT Recv_D.LocationId, Article.ArticleId, Article.ArticleCode as Code, Article.ArticleDescription AS item, Recv_D.AlternativeItem, Article.ArticleSizeName as Size, Article.ArticleColorName as Color,  Recv_D.ArticleSize AS unit, ISNULL(Recv_D.Sz1,0) OrderQty, ISNULL(DCQty,0) DeliverQty, (ISNULL(Recv_D.Sz1,0) - Isnull(DCQty , 0)) AS Qty, ISNULL(Recv_D.Sz1,0) - ISNULL(DCQty,0) RemainingQty,Recv_D.PostDiscountPrice, Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, " _
            & " ((IsNull(Recv_D.Qty, 0) * Recv_D.Price * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) - isnull(Recv_D.DeliveredTotalQty,0)  * Recv_D.Price * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total,Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue, " _
            & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as [Pack Qty] ,Recv_D.Price as CurrentPrice, IsNull(Recv_D.PackPrice, 0) as PackPrice, 0 as DeliveryDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax,  Convert(float,IsNull(Recv_D.SED_Tax_Percent,0)) as SED, Convert(float,0) as savedqty,(ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, 0 as So_Id, '' as UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0.0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, 0 as BatchId, ISNULL(Recv_D.BatchNo, 0) as BatchNo, ISNULL(Recv_D.ExpiryDate, 0) as ExpiryDate, ISNULL(Recv_D.Origin, '') as Origin, 0 AS [Bardana Deduction] , 0 AS [Other Deduction], 0 AS [After Deduction Qty], Recv_D.Comments as Comments, Recv_D.Other_Comments as [Other Comments], 0 as DeliveredQty, IsNull(Stock.CurrStock,0) as Stock, IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as DeliveryId, ISNULL(Recv_D.SalesOrderDetailId, 0) As SalesOrderDetailId,(IsNull(Recv_D.Qty,0)- IsNull(Recv_D.DeliveredTotalQty,0)) as TotalQty, ISNULL(Article.LogicalItem, 0) AS LogicalItem, 0 AS AdditionalItem, Recv_D.AlternativeItemId  FROM SalesOrderDetailTable Recv_D INNER JOIN " _
            & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderID = Recv_D.SalesOrderID  LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId  " _
            & " Where Recv_D.SalesOrderID =" & ReceivingID & " AND (IsNull(Recv_D.Sz1,0) - Isnull(DeliveredQty,0)) > 0 ORDER BY ISNULL(Recv_D.SalesOrderDetailId, 0)"
        End If
        '   Else
        '   str = "SELECT Recv_D.LocationId, Article.ArticleId, Article.ArticleCode as Code, Article.ArticleDescription AS item,Article.ArticleSizeName as Size, Article.ArticleColorName as Color,  Recv_D.ArticleSize AS unit, ISNULL(Recv_D.Sz1,0) OrderQty, ISNULL(DeliveredQty,0) DeliverQty, (ISNULL(Recv_D.Sz1,0) - Isnull(DeliveredQty , 0)) AS Qty, ISNULL(Recv_D.Sz1,0) - ISNULL(DeliveredQty,0) RemainingQty, Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, " _
        '& " ((IsNull(Recv_D.Qty, 0) * Recv_D.Price * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) - isnull(Recv_D.DeliveredTotalQty,0)  * Recv_D.Price * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
        '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as [Pack Qty] ,Recv_D.Price as CurrentPrice, IsNull(Recv_D.PackPrice, 0) as PackPrice, 0 as DeliveryDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax,  Convert(float,IsNull(Recv_D.SED_Tax_Percent,0)) as SED, Convert(float,0) as savedqty,(ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, 0 as So_Id, '' as UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0.0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, 0 as BatchId, 'xxxx' as BatchNo, Convert(DateTime,Null) as ExpiryDate, 0 AS [Bardana Deduction] , 0 AS [Other Deduction], 0 AS [After Deduction Qty], Recv_D.Comments as Comments, Recv_D.Other_Comments as [Other Comments], 0 as DeliveredQty, IsNull(Stock.CurrStock,0) as Stock, IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as DeliveryId, ISNULL(Recv_D.SalesOrderDetailId, 0) As SalesOrderDetailId,(IsNull(Recv_D.Qty,0)- IsNull(Recv_D.DeliveredTotalQty,0)) as TotalQty  FROM SalesOrderDetailTable Recv_D INNER JOIN " _
        '& " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
        '& " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderID = Recv_D.SalesOrderID  LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId  " _
        '& " Where Recv_D.SalesOrderID =" & ReceivingID & " "
        '   End If


        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet

        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon
        objCommand.CommandType = CommandType.Text


        objCommand.CommandText = str

        objDataAdapter.SelectCommand = objCommand
        objDataAdapter.Fill(objDataSet)
        Dim IsMinus As Boolean = True
        'If CType(Me.cmbItem.SelectedRow, Infragistics.Win.UltraWinGrid.UltraGridRow).Cells("ServiceItem").Value = False Then

        Dim isLocationWiseMinusStock As Boolean

        IsMinus = getConfigValueByType("AllowMinusStock")
        'End If


        If objDataSet.Tables(0).Rows.Count > 0 Then
            For Each dr As DataRow In objDataSet.Tables(0).Rows
                dr.BeginEdit()
                AvailableStock = Convert.ToDouble(GetStockById(dr.Item("ArticleDefId"), dr.Item("LocationId"), IIf(dr.Item("unit").ToString = "Loose", "Loose", "Pack")))
                'AvailableStock1 = Convert.ToDouble(GetStockById(dr.Item("ArticleDefId"), dr.Item("LocationId"), IIf(dr.Item("unit").ToString = "Loose", "Loose", "Pack")))
                If AvailableStock < 0 AndAlso IsMinus = False Then   'Task Saad Afzaal check if stock is negative and configuration is also off then item can not add
                    ShowErrorMessage("Stock is negative against item " & dr.Item("Item").ToString & " ")
                    dr.Delete()
                    'Task Saad Afzaal Comment against below code becuase uneccesary code that we do not need
                    'Else
                    '    If dr.Item("unit").ToString = "Loose" Then
                    '        If Val(dr.Item("TotalQty")) > AvailableStock Then
                    '            If msg_Confirm("Stock is not enough against item " & dr.Item("Item").ToString & " . Do you want to load available Qty in stock as partial Qty?") = False Then
                    '                Exit Sub
                    '            Else
                    '                StockChecked = True
                    '                dr.Item("TotalQty") = AvailableStock
                    '                dr.Item("Qty") = AvailableStock
                    '            End If
                    '        End If
                    '    Else
                    '        If Val(dr.Item("Qty")) > AvailableStock Then
                    '            If msg_Confirm("Stock is not enough. Do you want to load available Qty in stock as partial Qty?") = False Then
                    '                Exit Sub
                    '            Else
                    '                StockChecked = True
                    '                dr.Item("Qty") = AvailableStock
                    '            End If
                    '        End If

                    '    End If

                    dr.EndEdit()
                    AvailableStock = 0

                ElseIf AvailableStock < 0 AndAlso IsMinus = True Then

                    isLocationWiseMinusStock = CheckLocationWiseMinusStock(dr.Item("LocationId"))

                    If isLocationWiseMinusStock = False Then

                        ShowErrorMessage("Stock is negative against item " & dr.Item("Item").ToString & " ")
                        dr.Delete()
                        dr.EndEdit()
                        AvailableStock = 0

                    End If

                End If
            Next
        End If

        'objDataSet.Tables(0).Columns(EnumGridDetail.RemainingQty).Expression = "OrderQty - DeliverQty - Qty"
        objDataSet.AcceptChanges()

        If flgMultipleSalesOrder = False Then
            Me.DisplayDetail(-1)
        End If

        If objDataSet.Tables(0).Rows.Count > 0 Then
            If IsDBNull(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyId")) Or Val(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                'Me.cmbCurrency.SelectedValue = Nothing
                Me.cmbCurrency.Enabled = False
            Else
                'IsCurrencyEdit = True
                'IsNotCurrencyRateToAll = True
                FillCombo("Currency")
                Me.CurrencyRate = Val(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyRate").ToString)
                Me.cmbCurrency.SelectedValue = Val(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyId").ToString)
                Me.cmbCurrency.Enabled = False
            End If
            cmbCurrency_SelectedIndexChanged(Nothing, Nothing)
        End If

        For i = 0 To objDataSet.Tables(0).Rows.Count - 1
            _dblSEDTaxPercent = 0D
            ''selecting the category
            'Me.cmbCategory.SelectedValue = Val(objDataSet.Tables(0).Rows(i)("LocationId").ToString)
            'Me.cmbCategory_SelectedIndexChanged(Nothing, Nothing)
            ''selecting the item
            Me.cmbCategory.SelectedValue = Val(objDataSet.Tables(0).Rows(i)("LocationId").ToString)
            If Val(objDataSet.Tables(0).Rows(i)("CurrencyId").ToString) = 0 Then
                Me.cmbCurrency.Enabled = False
            Else
                Me.cmbCurrency.SelectedValue = objDataSet.Tables(0).Rows(i)("CurrencyId")
                Me.txtCurrencyRate.Text = objDataSet.Tables(0).Rows(i)("CurrencyRate")
                Me.cmbCurrency.Enabled = False
            End If
            Me.cmbItem.Value = Val(objDataSet.Tables(0).Rows(i)("ArticleDefId").ToString)
            Me.cmbItem_Leave(Nothing, Nothing)

            ''selecting batch no
            If objDataSet.Tables(0).Rows(i)("BatchNo") = 0 Then
            Else
                Me.cmbBatchNo.Text = objDataSet.Tables(0).Rows(i)("BatchNo")
                Me.cmbBatchNo_Leave(Nothing, Nothing)
            End If

            ''selecting unit
            Me.cmbUnit.Text = objDataSet.Tables(0).Rows(i)("Pack_Desc")
            Me.cmbUnit_SelectedIndexChanged(Nothing, Nothing)
            ''selecting qty
            'SOQty = Val(objDataSet.Tables(0).Rows(i)("Qty").ToString) ''TFS2825
            Me.txtQty.Text = Val(objDataSet.Tables(0).Rows(i)("Qty").ToString)
            Me.txtQty_LostFocus(Nothing, Nothing)


            'Me.txtQty_TextChanged(Nothing, Nothing)
            ''selecing rate
            Me.txtRate.Text = Val(objDataSet.Tables(0).Rows(i)("Rate").ToString)
            Me.txtPDP.Text = objDataSet.Tables(0).Rows(i)("PostDiscountPrice") ''TFS2827
            Me.txtTax.Text = Val(objDataSet.Tables(0).Rows(i)("Tax").ToString)

            If Me.cmbUnit.Text <> "Loose" Then
                Dim q As Integer = Val(Me.txtTotalQuantity.Text)

                '  txtPackQty.Text = Val(objDataSet.Tables(0).Rows(i)("Pack Qty"))
                Dim q1 As Integer = Val(Me.txtTotalQuantity.Text)

                'TASK-408
                Me.txtTotalQuantity.Text = Val(objDataSet.Tables(0).Rows(i)("TotalQty").ToString)
                'Me.txtTotalQuantity_LostFocus(Nothing, Nothing)
                'Me.txtTotal.Text = Val(Me.txtRate.Text) * (Val(objDataSet.Tables(0).Rows(i)("Pack Qty").ToString) * Val(Me.txtQty.Text))
                Me.txtTotal.Text = Math.Round(Val(Me.txtRate.Text) * Val(Me.txtTotalQuantity.Text), TotalAmountRounding)


            Else
                Me.txtTotal.Text = Math.Round(Val(Me.txtRate.Text) * Val(Me.txtQty.Text), TotalAmountRounding)
                txtPackQty.Text = "1"
            End If
            'new line added by haseeb 6-oct-2016
            txtPackQty.Text = Val(objDataSet.Tables(0).Rows(i)("Pack Qty"))
            'end

            SOBatchNo = objDataSet.Tables(0).Rows(i)("BatchNo").ToString
            SOExpiryDate = objDataSet.Tables(0).Rows(i)("ExpiryDate")
            SOOrigin = objDataSet.Tables(0).Rows(i)("Origin")

            'Ali Faisal : TFS1360 : 23-Aug-2017 : Getting Correct Total Qty from SO
            Me.txtTotalQuantity.Text = Val(objDataSet.Tables(0).Rows(i)("TotalQty").ToString)
            'Ali Faisal : TFS1360 : 23-Aug-2017 : End
            'Me.txtStock.Text = Val(objDataSet.Tables(0).Rows(i).Item("Stock").ToString)
            'Me.cmbCategory.SelectedValue = Val(objDataSet.Tables(0).Rows(i)("LocationId").ToString)
            TradePrice = Val(objDataSet.Tables(0).Rows(i)("TradePrice").ToString)
            DeliveryTax_Percentage = Val(objDataSet.Tables(0).Rows(i)("Tax").ToString)
            SchemeQty = Val(objDataSet.Tables(0).Rows(i)("Sample Qty").ToString)
            Discount_Percentage = Val(objDataSet.Tables(0).Rows(i)("Discount_Percentage").ToString)
            Freight = Val(objDataSet.Tables(0).Rows(i)("Freight").ToString)
            MarketReturns = Val(objDataSet.Tables(0).Rows(i)("MarketReturns").ToString)
            Me.txtDisc.Text = Val(Discount_Percentage)
            Me.txtDisc.Text = objDataSet.Tables(0).Rows(i)("DiscountFactor")  ''TFS2827
            Me.cmbDiscountType.SelectedValue = objDataSet.Tables(0).Rows(i)("DiscountId") ''TFS2827
            Me.txtDiscountValue.Text = objDataSet.Tables(0).Rows(i)("DiscountValue") ''TFS2827
            strOtherComments = objDataSet.Tables(0).Rows(i)("Other Comments").ToString
            strComments = objDataSet.Tables(0).Rows(i)("Comments").ToString
            _dblSEDTaxPercent = Val(objDataSet.Tables(0).Rows(i)("SED").ToString)
            SalesOrderDetailId = Val(objDataSet.Tables(0).Rows(i)("SalesOrderDetailId").ToString)
            OrderQty = Val(objDataSet.Tables(0).Rows(i)("OrderQty").ToString)
            DeliverQty = Val(objDataSet.Tables(0).Rows(i)("DeliverQty").ToString)
            RemainingQty = Val(objDataSet.Tables(0).Rows(i)("RemainingQty").ToString)
            Me.btnAdd_Click(Nothing, Nothing)
            _dblSEDTaxPercent = 0D
            TradePrice = 0
            DeliveryTax_Percentage = 0
            SchemeQty = 0
            Discount_Percentage = 0
            Freight = 0
            MarketReturns = 0
            strOtherComments = String.Empty
            strComments = String.Empty
            SOBatchNo = String.Empty
            SOExpiryDate = Date.Now.AddMonths(1)
            SOOrigin = String.Empty
            '  grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)(3))
            'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

        Next

        If GetConfigValue("ServiceItem").ToString = "True" Then
            Me.grd.RootTable.Columns(EnumGridDetail.Color).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.Size).Visible = False
        Else
            Me.grd.RootTable.Columns(EnumGridDetail.Color).Visible = True
            Me.grd.RootTable.Columns(EnumGridDetail.Size).Visible = True
        End If
        ''Start TFS4181
        Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).LimitToList = False
        Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).EditType = Janus.Windows.GridEX.EditType.Combo
        ''End TFS4181
        ''Start TFS4181
        Me.grd.RootTable.Columns(EnumGridDetail.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
        Me.grd.RootTable.Columns(EnumGridDetail.ExpiryDate).FormatString = str_DisplayDateFormat
        ''End TFS4181
        Me.grd.RootTable.Columns("Origin").HasValueList = True
        Me.grd.RootTable.Columns("Origin").LimitToList = False
        Me.grd.RootTable.Columns("Origin").EditType = Janus.Windows.GridEX.EditType.Combo
        ''Ayesha Rehman: TFS4181: 16-08-2018 : Fill combo boxes
        FillCombo("grdBatchNo")
        ''Ayesha Rehman : TFS4181 : 16-08-2018 : End
        FillCombo("GrdOrigin")
        Me.grd.RootTable.Columns("Stock").Visible = False
        Me.grd.RootTable.Columns("DeliveryId").Visible = False
        Me.grd.RootTable.Columns("DeliveryDetailId").Visible = False
        Me.grd.RootTable.Columns("DeliverQty").Caption = "Delivered Qty"

        Me.grd.RootTable.Columns(EnumGridDetail.OrderQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.OrderQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.OrderQty).FormatString = "N" & DecimalPointInQty

        Me.grd.RootTable.Columns(EnumGridDetail.DeliverQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.DeliverQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.DeliverQty).FormatString = "N" & DecimalPointInQty

        Me.grd.RootTable.Columns(EnumGridDetail.RemainingQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.RemainingQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.RemainingQty).FormatString = "N" & DecimalPointInQty

    End Sub
    Private Sub DisplayDetail(ByVal ReceivingID As Integer, Optional ByVal TypeId As Integer = -1, Optional ByVal Condition As String = "")
        Dim str As String = String.Empty
        Dim i As Integer = 0
        'str = "SELECT tblDefLocation.location_code AS Location, Article.ArticleDescription AS Item, ArticleSizeDefTable.ArticleSizeName AS Size, ArticleColorDefTable.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
        '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price) * Article.PackQty) END AS Total, " _
        '      & " Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Recv_D.CurrentPrice, isnull(Recv_D.BatchNo,'xxxx') as [Batch No], Recv_D.DeliveryDetailId, Recv_D.BatchID, Recv_D.LocationId,isnull(Recv_D.TaxPercent,0) as Tax, 0 as savedqty , SampleQty as [Sample Qty] FROM DeliveryChalanDetailTable Recv_D INNER JOIN " _
        '      & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId inner JOIN " _
        '      & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id inner join " _
        '      & " articledeftable on articledeftable.articleid = recv_d.articleDefId INNER JOIN " _
        '      & " ArticleSizeDefTable ON ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId INNER JOIN " _
        '      & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId " _
        '      & " Where Recv_D.DeliveryID =" & ReceivingID
        If Condition = "LoadAll" Then
            If getConfigValueByType("LoadAllItemsInSales").ToString = "True" Then
                'Before against task:M16
                'str = "SELECT  Detail.LocationId, ArticleDefView.ArticleCode as Code, ArticleDefView.ArticleDescription as Item, ArticleDefView.ArticleSizeName as Size, ArticleDefView.ArticleColorName as Color, " _
                '      & " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Rate, Isnull(Detail.Total,0) as Total,  " _
                '      & " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [Pack Qty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, ISNULL(Detail.[Batch No], 'xxxx') as [Batch No],  ISNULL(Detail.DeliveryDetailId,0) as DeliveryDetailId, ISNULL(Detail.BatchId, 0) as BatchId, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as Tax, ISNULL(Detail.SED,0) As SED, ISNULL(Detail.SavedQty,0) as SavedQty, ISNULL(Detail.[Sample Qty],0) as [Sample Qty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, " _
                '      & " ISNULL(Detail.SO_ID,0) as SO_ID, Detail.UOM, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc From ArticleDefView  LEFT OUTER JOIN ( " _
                '      & " SELECT tblDefLocation.Location_Id as LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, ArticleSizeDefTable.ArticleSizeName AS Size, ArticleColorDefTable.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate,  " _
                '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price) * Article.PackQty) END AS Total,  " _
                '      & " Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice, isnull(Recv_D.BatchNo,'xxxx') as [Batch No], Recv_D.DeliveryDetailId, Recv_D.BatchID, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.TaxPercent,0) as Tax, ISNULL(Recv_D.SEDPercent,0) As SED, 0 as savedqty , SampleQty as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(Recv_D.SO_ID,0) as SO_Id, Recv_D.UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc FROM DeliveryChalanDetailTable Recv_D INNER JOIN  " _
                '      & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId inner JOIN  " _
                '      & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id inner join  " _
                '      & " articledeftable on articledeftable.articleid = recv_d.articleDefId INNER JOIN  " _
                '      & " ArticleSizeDefTable ON ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId INNER JOIN  " _
                '      & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId  " _
                '      & " Where(Recv_D.DeliveryID = " & ReceivingID & ") " _
                '      & " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId " _
                '      & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") " _
                '      & " CustomerDiscount On CustomerDiscount.ArticleDefId = ArticleDefView.ArticleId " _
                '      & " WHERE(ArticleDefView.SalesItem = 1 " & IIf(IsEditMode = False, " AND ArticleDefView.Active=1", "") & ") " _
                '      & " ORDER BY ArticleDefView.SortOrder Asc"
                'Task:M16 Added Column Engine_No And Chassis_No
                'str = "SELECT  Detail.LocationId, ArticleDefView.ArticleCode as Code, ArticleDefView.ArticleDescription as Item, ArticleDefView.ArticleSizeName as Size, ArticleDefView.ArticleColorName as Color, " _
                '      & " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Rate, Isnull(Detail.Total,0) as Total,  " _
                '      & " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [Pack Qty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, ISNULL(Detail.[Batch No], 'xxxx') as [Batch No],  ISNULL(Detail.DeliveryDetailId,0) as DeliveryDetailId, ISNULL(Detail.BatchId, 0) as BatchId, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as Tax, ISNULL(Detail.SED,0) As SED, ISNULL(Detail.SavedQty,0) as SavedQty, ISNULL(Detail.[Sample Qty],0) as [Sample Qty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, " _
                '      & " ISNULL(Detail.SO_ID,0) as SO_ID, Detail.UOM, Isnull(Detail.PurchasePrice,0) as PurchasePrice, Detail.Pack_Desc, Detail.Engine_No, Detail.Chassis_No From ArticleDefView  LEFT OUTER JOIN ( " _
                '      & " SELECT tblDefLocation.Location_Id as LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, ArticleSizeDefTable.ArticleSizeName AS Size, ArticleColorDefTable.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate,  " _
                '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price) * Article.PackQty) END AS Total,  " _
                '      & " Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice, isnull(Recv_D.BatchNo,'xxxx') as [Batch No], Recv_D.DeliveryDetailId, Recv_D.BatchID, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.TaxPercent,0) as Tax, ISNULL(Recv_D.SEDPercent,0) As SED, 0 as savedqty , SampleQty as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(Recv_D.SO_ID,0) as SO_Id, Recv_D.UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No FROM DeliveryChalanDetailTable Recv_D INNER JOIN  " _
                '      & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId inner JOIN  " _
                '      & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id inner join  " _
                '      & " articledeftable on articledeftable.articleid = recv_d.articleDefId INNER JOIN  " _
                '      & " ArticleSizeDefTable ON ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId INNER JOIN  " _
                '      & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId  " _
                '      & " Where(Recv_D.DeliveryID = " & ReceivingID & ") " _
                '      & " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId " _
                '      & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") " _
                '      & " CustomerDiscount On CustomerDiscount.ArticleDefId = ArticleDefView.ArticleId " _
                '      & " WHERE(ArticleDefView.SalesItem = 1 " & IIf(IsEditMode = False, " AND ArticleDefView.Active=1", "") & ") " _
                '      & " ORDER BY ArticleDefView.SortOrder Asc"
                ''End Task:M16
                '2598
                'str = "SELECT  Detail.LocationId, ArticleDefView.ArticleCode as Code, ArticleDefView.ArticleDescription as Item, ArticleDefView.ArticleSizeName as Size, ArticleDefView.ArticleColorName as Color, " _
                '      & " 'Loose' as Unit, isnull(Detail.Qty,0) as Qty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Rate, Isnull(Detail.Total,0) as Total,  " _
                '      & " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [Pack Qty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice,  ISNULL(Detail.DeliveryDetailId,0) as DeliveryDetailId, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as Tax, ISNULL(Detail.SED,0) As SED, ISNULL(Detail.SavedQty,0) as SavedQty, ISNULL(Detail.[Sample Qty],0) as [Sample Qty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, " _
                '      & " ISNULL(Detail.SO_ID,0) as SO_ID, Detail.UOM, Isnull(Detail.PurchasePrice,0) as PurchasePrice, IsNull(Detail.NetBill,0) as NetBill, Detail.Pack_Desc, Detail.Engine_No, Detail.Chassis_No,  ISNULL(Detail.BatchId, 0) as BatchId, ISNULL(Detail.[Batch No], 'xxxx') as [Batch No], Detail.ExpiryDate, Detail.Comments, IsNull(Detail.DeliveredQty,0) as DeliveredQty From ArticleDefView  LEFT OUTER JOIN ( " _
                '      & " SELECT tblDefLocation.Location_Id as LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, ArticleSizeDefTable.ArticleSizeName AS Size, ArticleColorDefTable.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.Price as Rate,  " _
                '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price) * Article.PackQty) END AS Total,  " _
                '      & " Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice,  Recv_D.DeliveryDetailId,  ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.TaxPercent,0) as Tax, ISNULL(Recv_D.SEDPercent,0) As SED, 0 as savedqty , SampleQty as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(Recv_D.SO_ID,0) as SO_Id, Recv_D.UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No ,Recv_D.BatchID, isnull(Recv_D.BatchNo,'xxxx') as [Batch No], Recv_D.ExpiryDate, Recv_D.Comments , IsNull(Recv_D.DeliveredQty,0) as DeliveredQty FROM DeliveryChalanDetailTable Recv_D INNER JOIN  " _
                '      & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId inner JOIN  " _
                '      & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id inner join  " _
                '      & " articledeftable on articledeftable.articleid = recv_d.articleDefId INNER JOIN  " _
                '      & " ArticleSizeDefTable ON ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId INNER JOIN  " _
                '      & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId  " _
                '      & " Where(Recv_D.DeliveryID = " & ReceivingID & ") " _
                '      & " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId " _
                '      & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") " _
                '      & " CustomerDiscount On CustomerDiscount.ArticleDefId = ArticleDefView.ArticleId " _
                '      & " WHERE(ArticleDefView.SalesItem = 1 " & IIf(IsEditMode = False, " AND ArticleDefView.Active=1", "") & ") " _
                '      & " ORDER BY ArticleDefView.SortOrder Asc"
                'End Task:M16

                ''TASK TFS1496 addition column of PackPrice
                ''Task TFS1807 Addtion of columns
                ''Task2827 : Added Column of Discount and PostDiscountPrice in Query for Discount factor Implementation
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                str = "SELECT  Detail.LocationId, ArticleDefView.ArticleCode as Code, ArticleDefView.ArticleDescription as Item, Recv_D.AlternativeItem, ArticleDefView.ArticleSizeName as Size, ArticleDefView.ArticleColorName as Color, " _
                 & " 'Loose' as Unit, isnull(Detail.Qty,0) AS OrderQty, 0 As DeliverQty, isnull(Detail.Qty,0) as Qty, isnull(Detail.Qty,0)- Qty - DeliverQty AS RemainingQty, ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as PostDiscountPrice , ((IsNULL(ArticleDefView.SalePrice,0) - (IsNULL(ArticleDefView.SalePrice,0)*Isnull(CustomerDiscount.Discount,0))/100)) as Rate, IsNull(Detail.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Detail.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Detail.CurrencyId, 0) As CurrencyId, IsNull(Detail.CurrencyRate, 0) As CurrencyRate, IsNull(Detail.CurrencyAmount, 0) As CurrencyAmount, Isnull(Detail.Total,0) as Total, Isnull(Detail.DiscountId,1) as DiscountId , IsNull(Detail.DiscountFactor, 0) AS DiscountFactor, IsNull(Detail.DiscountValue, 0) As DiscountValue,  " _
                 & " ArticleDefView.ArticleGroupId,ArticleDefView.ArticleId as ArticleDefId, ISNULL(Detail.[Pack Qty],0) as [Pack Qty], IsNULL(ArticleDefView.SalePrice,0) as CurrentPrice, ISNULL(Detail.PackPrice,0) as PackPrice,  ISNULL(Detail.DeliveryDetailId,0) as DeliveryDetailId, ISNULL(Detail.TradePrice,0) as TradePrice, ISNULL(Detail.Tax,0) as Tax, ISNULL(Detail.SED,0) As SED, ISNULL(Detail.SavedQty,0) as SavedQty, ISNULL(Detail.[Sample Qty],0) as [Sample Qty], ISNULL(Detail.Discount_Percentage,0) as Discount_Percentage, ISNULL(Detail.Freight,0) as Freight, ISNULL(Detail.MarketReturns,0) As MarketReturns, " _
                 & " ISNULL(Detail.SO_ID,0) as SO_ID, Detail.UOM,Isnull(Detail.PurchasePrice,0) as PurchasePrice, IsNull(Detail.NetBill,0) as NetBill, Detail.Pack_Desc, Detail.Engine_No, Detail.Chassis_No,  ISNULL(Detail.BatchId, 0) as BatchId, ISNULL(Detail.[Batch No], 'xxxx') as [Batch No], IsNull(Detail.ExpiryDate,DATEADD(Month , 1 , getDate())) as ExpiryDate,ISNULL(Detail.Origin, '') as Origin, ISNULL(Detail.BardanaDeduction, 0) AS [Bardana Deduction], ISNULL(Detail.OtherDeduction, 0) AS [Other Deduction], 0 AS [After Deduction Qty], Detail.Comments, Detail.[Other Comments], IsNull(Detail.DeliveredQty,0) as DeliveredQty, 0.0 as Stock,IsNull(Detail.CostPrice,0) as CostPrice, 0 as DeliveryID, IsNull(Detail.SalesOrderDetailId,0) SalesOrderDetailId,IsNull(Detail.Gross_Weights, 0) As Gross_Weights,IsNull(Detail.Tray_Weights, 0) As Tray_Weights ,IsNull(Detail.Net_Weights, 0) As Net_Weights ,  IsNull(Detail.TotalQty,0) TotalQty, ISNULL(ArticleDefView.LogicalItem, 0) AS LogicalItem, 0 As AdditionalItem   From ArticleDefView  LEFT OUTER JOIN ( " _
                 & " SELECT tblDefLocation.Location_Id as LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, ArticleSizeDefTable.ArticleSizeName AS Size, ArticleColorDefTable.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty,Recv_D.PostDiscountPrice,  Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount  " _
                 & " (Recv_D.Qty * Recv_D.Price * (Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End)) AS Total,Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue ,  " _
                 & " Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty],Isnull(Recv_D.CurrentPrice,0) as CurrentPrice,  Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.DeliveryDetailId,  ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.TaxPercent,0) as Tax, ISNULL(Recv_D.SEDPercent,0) As SED, 0 as savedqty , SampleQty as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0)  as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(Recv_D.SO_ID,0) as SO_Id, Recv_D.UOM,Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No ,Recv_D.BatchID, isnull(Recv_D.BatchNo,'xxxx') as [Batch No], Recv_D.ExpiryDate, isnull(Recv_D.Origin,'') as Origin, Recv_D.Comments , IsNull(Recv_D.DeliveredQty,0) as DeliveredQty, Recv_D.Other_Comments as [Other Comments], IsNull(Article.Cost_Price,0) as CostPrice, Recv_D.DeliveryID, ISNULL(Recv_D.SO_Detail_ID, 0) As SalesOrderDetailId ,IsNull(Recv_D.Gross_Weights, 0) As Gross_Weights,IsNull(Recv_D.Tray_Weights, 0) As Tray_Weights ,IsNull(Recv_D.Net_Weights, 0) As Net_Weights , IsNull(Recv_D.Qty, 0) As TotalQty, ISNULL(Recv_D.BardanaDeduction, 0) AS BardanaDeduction , ISNULL(Recv_D.OtherDeduction, 0) AS OtherDeduction, Recv_D.AlternativeItemId FROM DeliveryChalanDetailTable Recv_D INNER JOIN  " _
                 & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId inner JOIN  " _
                 & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id inner join  " _
                 & " articledeftable on articledeftable.articleid = recv_d.articleDefId INNER JOIN  " _
                 & " ArticleSizeDefTable ON ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId INNER JOIN  " _
                 & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId  " _
                 & " Where(Recv_D.DeliveryID = " & ReceivingID & ") " _
                 & " ) Detail ON Detail.ArticleDefId = ArticleDefView.ArticleId " _
                 & " LEFT OUTER JOIN (SELECT ArticleDefId, Discount from tblDefCustomerBaseDiscounts WHERE TypeID=" & IIf(TypeId > 0, TypeId, -1) & ") " _
                 & " CustomerDiscount On CustomerDiscount.ArticleDefId = ArticleDefView.ArticleId " _
                 & " WHERE(ArticleDefView.SalesItem = 1 " & IIf(IsEditMode = False, " AND ArticleDefView.Active=1", "") & ") " _
                 & " ORDER BY ArticleDefView.SortOrder Asc"

            End If
        ElseIf Condition = String.Empty Then
            'Before against task:M16
            ''Task2827 : Added Column of Discount and PostDiscountPrice in Query for Discount factor Implementation
            ''Start TFS3793 : Ayesha Rehman : 04-07-2018
            ''Commented Against TFS3793 : Getting Exact Order qty,Qty,Delivered qty and Remaining qty
            '' Key :: Order Qty = SalesOrderQty , Delivered qty = PreviouslyDelivered + Delivered qty , Qty = OrderQty - DeliveredQty , Remaining = Order - Delivered - Qty
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, Article.ArticleSizeName AS Size, Article.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, ISNULL(SalesOrderDetailTable.Sz1,0) As OrderQty, ISNULL(Recv_D.PerviouslyDeliveredQty,0) AS DeliverQty, Isnull(Recv_D.Sz1 , 0) AS Qty, ISNULL(SalesOrderDetailTable.Sz1,0) -  ISNULL(Recv_D.PerviouslyDeliveredQty,0) - Isnull(Recv_D.Sz1 , 0)  AS RemainingQty,Recv_D.PostDiscountPrice, Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, " _
            '          & " (Recv_D.Qty * Recv_D.Price *(Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End)) AS Total,Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue , " _
            '          & " Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty], ISNULL(Recv_D.CurrentPrice,0) as CurrentPrice,  ISNULL(Recv_D.PackPrice,0) as PackPrice, Recv_D.DeliveryDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.TaxPercent,0) as Tax, ISNULL(Recv_D.SEDPercent,0) As SED, Convert(float, 0) as savedqty , SampleQty as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(So_ID,0) as So_Id, Recv_D.UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0.0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, Recv_D.BatchID, isnull(Recv_D.BatchNo,'xxxx') as [Batch No],Recv_D.ExpiryDate, ISNULL(Recv_D.BardanaDeduction, 0) AS [Bardana Deduction] , ISNULL(Recv_D.OtherDeduction, 0) AS [Other Deduction], 0 AS [After Deduction Qty], Recv_D.Comments, Recv_D.Other_Comments as [Other Comments], IsNull(Recv_D.DeliveredQty,0) as DeliveredQty,IsNull(Stock.CurrStock,0) as Stock, IsNull(Recv_D.CostPrice,0) as CostPrice, Recv_D.DeliveryID, ISNULL(Recv_D.SO_Detail_ID, 0) As SalesOrderDetailId,IsNull(Recv_D.Gross_Weights, 0) As Gross_Weights,IsNull(Recv_D.Tray_Weights, 0) As Tray_Weights ,IsNull(Recv_D.Net_Weights, 0) As Net_Weights , IsNull(Recv_D.Qty, 0) as TotalQty, ISNULL(Article.LogicalItem, 0) AS LogicalItem  FROM DeliveryChalanDetailTable Recv_D INNER JOIN " _
            '          & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId inner JOIN " _
            '          & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id LEFT OUTER JOIN SalesOrderDetailTable ON Recv_D.SO_Detail_ID = SalesOrderDetailTable.SalesOrderDetailId LEFT OUTER JOIN (Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId " _
            '          & " Where Recv_D.DeliveryID =" & ReceivingID & " ORDER BY Recv_D.DeliveryDetailId Asc"
            ''Commented Against TFS3793
            'str = " SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, Article.ArticleSizeName AS Size, Article.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, ISNULL(SalesOrderDetailTable.Sz1,0) As OrderQty, " _
            '      & " ISNULL(Recv_D.DeliveredQty,0) DeliverQty, ISNULL(SalesOrderDetailTable.Sz1,0) - Isnull(Recv_D.DeliveredQty , 0) AS Qty, (ISNULL(SalesOrderDetailTable.Sz1,0) - ISNULL(Recv_D.DeliveredQty,0) - (ISNULL(Recv_D.Sz1,0) - Isnull(Recv_D.DeliveredQty , 0))) As RemainingQty,Recv_D.PostDiscountPrice, Recv_D.Price as Rate, " _
            '      & " IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount,  (Recv_D.Qty * Recv_D.Price *(Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End)) AS Total,Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue ,  Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty], ISNULL(Recv_D.CurrentPrice,0) as CurrentPrice,  ISNULL(Recv_D.PackPrice,0) as PackPrice, Recv_D.DeliveryDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.TaxPercent,0) as Tax, ISNULL(Recv_D.SEDPercent,0) As SED, Convert(float, 0) as savedqty , SampleQty as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(So_ID,0) as So_Id, Recv_D.UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0.0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, Recv_D.BatchID, isnull(Recv_D.BatchNo,'xxxx') as [Batch No],Recv_D.ExpiryDate, ISNULL(Recv_D.BardanaDeduction, 0) AS [Bardana Deduction] , ISNULL(Recv_D.OtherDeduction, 0) AS [Other Deduction], " _
            '      & " 0 AS [After Deduction Qty], Recv_D.Comments, Recv_D.Other_Comments as [Other Comments], IsNull(Recv_D.DeliveredQty,0) as DeliveredQty,IsNull(Stock.CurrStock,0) as Stock, IsNull(Recv_D.CostPrice,0) as CostPrice, Recv_D.DeliveryID, ISNULL(Recv_D.SO_Detail_ID, 0) As SalesOrderDetailId,IsNull(Recv_D.Gross_Weights, 0) As Gross_Weights,IsNull(Recv_D.Tray_Weights, 0) As Tray_Weights ,IsNull(Recv_D.Net_Weights, 0) As Net_Weights , IsNull(Recv_D.Qty, 0) as TotalQty, ISNULL(Article.LogicalItem, 0) AS LogicalItem  FROM DeliveryChalanDetailTable Recv_D INNER JOIN  ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId inner JOIN " _
            '      & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id LEFT OUTER JOIN SalesOrderDetailTable ON Recv_D.SO_Detail_ID = SalesOrderDetailTable.SalesOrderDetailId LEFT OUTER JOIN (Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId " _
            '      & " Where Recv_D.DeliveryID = " & ReceivingID & " ORDER BY Recv_D.DeliveryDetailId Asc "
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            str = " SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, Recv_D.AlternativeItem, Article.ArticleSizeName AS Size, Article.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, ISNULL(SalesOrderDetailTable.Sz1,0) As OrderQty,( ISNULL(Recv_D.PerviouslyDeliveredQty,0) + ISNULL(Recv_D.DeliveredQty,0)) As DeliverQty, " _
                              & " ISNULL(Recv_D.Sz1,0) AS Qty, " _
                              & " ISNULL(SalesOrderDetailTable.Sz1,0) - (ISNULL(Recv_D.DeliveredQty,0) + ISNULL(Recv_D.PerviouslyDeliveredQty,0)) - (ISNULL(SalesOrderDetailTable.Sz1,0) - (Isnull(Recv_D.PerviouslyDeliveredQty, 0)+ ISNULL(Recv_D.DeliveredQty,0)))As RemainingQty, " _
                  & " Recv_D.PostDiscountPrice, Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount,  (Recv_D.Qty * Recv_D.Price *(Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End)) AS Total,Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue ,  Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty], ISNULL(Recv_D.CurrentPrice,0) as CurrentPrice,  ISNULL(Recv_D.PackPrice,0) as PackPrice, Recv_D.DeliveryDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.TaxPercent,0) as Tax, ISNULL(Recv_D.SEDPercent,0) As SED, Convert(float, 0) as savedqty , SampleQty as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(So_ID,0) as So_Id, Recv_D.UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0.0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, Recv_D.BatchID, isnull(Recv_D.BatchNo,'xxxx') as [Batch No],IsNUll(Recv_D.ExpiryDate,DATEADD(Month , 1 , getDate())) as ExpiryDate ,isnull(Recv_D.Origin,'') as Origin, ISNULL(Recv_D.BardanaDeduction, 0) AS [Bardana Deduction] , ISNULL(Recv_D.OtherDeduction, 0) AS [Other Deduction],  0 AS [After Deduction Qty], Recv_D.Comments, Recv_D.Other_Comments as [Other Comments], IsNull(Recv_D.DeliveredQty,0) as DeliveredQty,IsNull(Stock.CurrStock,0) as Stock, IsNull(Recv_D.CostPrice,0) as CostPrice, Recv_D.DeliveryID, ISNULL(Recv_D.SO_Detail_ID, 0) As SalesOrderDetailId,IsNull(Recv_D.Gross_Weights, 0) As Gross_Weights,IsNull(Recv_D.Tray_Weights, 0) As Tray_Weights ,IsNull(Recv_D.Net_Weights, 0) As Net_Weights , IsNull(Recv_D.Qty, 0) as TotalQty, ISNULL(Article.LogicalItem, 0) AS LogicalItem, 0 As AdditionalItem, Recv_D.AlternativeItemId  FROM DeliveryChalanDetailTable Recv_D INNER JOIN  ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId inner JOIN  tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id LEFT OUTER JOIN SalesOrderDetailTable ON Recv_D.SO_Detail_ID = SalesOrderDetailTable.SalesOrderDetailId LEFT OUTER JOIN (Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId  " _
                              & " Where Recv_D.DeliveryID = " & ReceivingID & " ORDER BY Recv_D.DeliveryDetailId Asc "

        ElseIf Condition = "PO" Then
            ''Task2827 : Added Column of Discount and PostDiscountPrice in Query for Discount factor Implementation
            str = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS item,Article.ArticleSizeName as Size, Recv_D.AlternativeItem, Article.ArticleColorName as Color,  Recv_D.ArticleSize AS unit, ISNULL(Recv_D.Sz1,0) As OrderQty, ISNULL(DeliveredQty,0) DeliverQty, ISNULL(Recv_D.Sz1,0) - Isnull(DeliveredQty , 0) AS Qty, (OrderQty - DeliverQty - Qty) As RemainingQty, Recv_D.PostDiscountPrice , Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, " _
            & " (((IsNull(Recv_D.Qty, 0) * Recv_D.Price*(Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End)) - isnull(Recv_D.DeliveredTotalQty,0))  * Recv_D.Price*(Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End)) AS Total,Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue , " _
            & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as [Pack Qty] ,Recv_D.Price as CurrentPrice, IsNull(Recv_D.PackPrice, 0) as PackPrice, 0 as DeliveryDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax,  Convert(float,null) as SED, Convert(float,null) as savedqty,(ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, 0 as So_Id, '' as UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0.0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, 0 as BatchId, isnull(Recv_D.BatchNo,'xxxx') as [Batch No],IsNUll(Recv_D.ExpiryDate,DATEADD(Month , 1 , getDate())) as ExpiryDate,isnull(Recv_D.Origin,'') as Origin, 0 AS [Bardana Deduction] , 0 AS [Other Deduction], 0 AS [After Deduction Qty], Recv_D.Comments as Comments, Recv_D.Other_Comments as [Other Comments], 0 as DeliveredQty,IsNull(Stock.CurrStock,0) as Stock, IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as DeliveryID,  ISNULL(Recv_D.SalesOrderDetailId, 0) As SalesOrderDetailId, IsNull(Recv_D.Qty, 0)-IsNull(Recv_D.DeliveredTotalQty, 0) as TotalQty, ISNULL(Article.LogicalItem, 0) AS LogicalItem, 0 As AdditionalItem, Recv_D.AlternativeItemId  FROM SalesOrderDetailTable Recv_D INNER JOIN " _
            & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderID = Recv_D.SalesOrderID LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId   " _
            & " Where Recv_D.SalesOrderID =" & ReceivingID & " AND Recv_D.Sz1 - Isnull(DeliveredQty , 0) > 0 "

        End If

        'str = "SELECT     Article_Group.ArticleGroupName AS Category, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
        '     & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price  ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
        '     & " Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.DeliveryDetailId, Recv_D.BatchID FROM DeliveryChalanDetailTable Recv_D INNER JOIN " _
        '     & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
        '     & " tblDefLocation Location_id ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
        '     & " Where Recv_D.DeliveryID =" & ReceivingID & ""

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet

        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon
        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = str

        objDataAdapter.SelectCommand = objCommand
        objDataAdapter.Fill(objDataSet)

        'If Not IsDeliveryOrderAnalysis = True Then
        '    objDataSet.Tables(0).Columns(EnumGridDetail.NetBill).Expression = "IIF(CurrentPrice=0,0,IIF(Unit='Loose',(((((((Qty + [Sample Qty]) * CurrentPrice) * Tax)/100) + (Freight * (Qty+[Sample Qty]))) - (((Qty * CurrentPrice) * Discount_Percentage)/100)) + (Qty  * CurrentPrice)),   ((((((((Qty  * [Pack Qty]) + [Sample Qty]) * CurrentPrice) * Tax)/100) + (Freight * ((Qty  * [Pack Qty]) + [Sample Qty]))) - ((((Qty * [Pack Qty]) * CurrentPrice) * Discount_Percentage)/100)) + ((Qty * [Pack Qty]) * CurrentPrice))))"
        '    objDataSet.Tables(0).Columns(EnumGridDetail.Total).Expression = "iif(Unit = 'Loose' , isnull(Qty,0) * isnull(Rate,0) , isnull(Qty,0) * isnull(Rate,0) * isnull([Pack Qty],0)) "
        '    'ElseIf ServicesItem = "True" Then
        '    '    objDataSet.Tables(0).Columns(EnumGridDetail.Total).Expression = "iif(Unit = 'Loose' , (ServiceQty * Rate) , ((ServiceQty * [Pack Qty])*Rate))"
        'Else
        '    objDataSet.Tables(0).Columns(EnumGridDetail.NetBill).Expression = "IIF(CurrentPrice=0,0,IIF(Unit='Loose',(((((((Qty + [Sample Qty]) * CurrentPrice) * Tax)/100) + (Freight * (Qty+[Sample Qty]))) - (((Qty * CurrentPrice) * Discount_Percentage)/100)) + (Qty  * CurrentPrice)),   ((((((((Qty  * [Pack Qty]) + [Sample Qty]) * CurrentPrice) * Tax)/100) + (Freight * ((Qty  * [Pack Qty]) + [Sample Qty]))) - ((((Qty * [Pack Qty]) * CurrentPrice) * Discount_Percentage)/100)) + ((Qty * [Pack Qty]) * CurrentPrice))))"
        '    objDataSet.Tables(0).Columns(EnumGridDetail.Total).Expression = "iif(Unit = 'Loose' , isnull(Qty,0) * isnull(Rate,0) , isnull(Qty,0) * isnull(Rate,0) * isnull([Pack Qty],0)) "
        'End If

        If Not IsDeliveryOrderAnalysis = True Then

            'objDataSet.Tables(0).Columns(EnumGridDetail.NetBill).Expression = "IIF(IsNull(CurrentPrice,0)=0,0,IIF(Unit='Loose',(((((((IsNull(Qty,0) + IsNull([Sample Qty],0)) * IsNull(CurrentPrice,0)) * IsNull(Tax,0))/100) + (IsNull(Freight,0) * (IsNull(Qty,0)+IsNull([Sample Qty],0)))) - (((IsNull(Qty,0) * IsNull(CurrentPrice,0)) * IsNull(Discount_Percentage,0))/100)) + (IsNull(Qty,0)  * IsNull(CurrentPrice,0))),   ((((((((IsNull(Qty,0)  * IsNull([Pack Qty],0)) + IsNull([Sample Qty],0)) * IsNull(CurrentPrice,0)) * IsNull(Tax,0))/100) + (IsNull(Freight,0) * ((IsNull(Qty,0)  * IsNull([Pack Qty],0)) + IsNull([Sample Qty],0)))) - ((((IsNull(Qty,0) * IsNull([Pack Qty],0)) * IsNull(CurrentPrice,0)) * IsNull(Discount_Percentage,0))/100)) + ((IsNull(Qty,0) * IsNull([Pack Qty],0)) * IsNull(CurrentPrice,0)))))"
            ''Commented against task multi currency implementation on 30-12-2016 by Ameen
            'objDataSet.Tables(0).Columns(EnumGridDetail.NetBill).Expression = "IIF(IsNull(CurrentPrice,0)=0,0,(((((((IsNull(TotalQty,0) + IsNull([Sample Qty],0)) * IsNull(CurrentPrice,0)) * IsNull(Tax,0))/100) + (IsNull(Freight,0) * (IsNull(TotalQty,0)+IsNull([Sample Qty],0)))) - (((IsNull(TotalQty,0) * IsNull(CurrentPrice,0)) * IsNull(Discount_Percentage,0))/100)) + (IsNull(TotalQty,0)  * IsNull(CurrentPrice,0))))"
            objDataSet.Tables(0).Columns(EnumGridDetail.DiscountValue).Expression = "IIF(DiscountId= 1,((IsNull(PostDiscountPrice,0)*IsNull(DiscountFactor,0))/100), IsNull(DiscountFactor,0))" ''TFS2827
            objDataSet.Tables(0).Columns(EnumGridDetail.Price).Expression = "IsNull(PostDiscountPrice,0)"
            objDataSet.Tables(0).Columns(EnumGridDetail.CurrencyAmount).Expression = "IsNull(TotalQty,0) * IsNull(PostDiscountPrice,0)" ''IIF(DiscountId= 1, IsNull(PostDiscountPrice,0)-((IsNull(PostDiscountPrice,0)*IsNull(DiscountFactor,0))/100), IsNull(PostDiscountPrice,0)-IsNull(DiscountFactor,0))"
            objDataSet.Tables(0).Columns(EnumGridDetail.NetBill).Expression = "IIF(IsNull(CurrentPrice,0)=0,0,(((((((IsNull(TotalQty,0) + IsNull([Sample Qty],0)) * IsNull(CurrentPrice,0) * CurrencyRate) * IsNull(Tax,0))/100) + (IsNull(Freight,0) * (IsNull(TotalQty,0)+IsNull([Sample Qty],0)))) - (((IsNull(TotalQty,0) * IsNull(CurrentPrice,0)*CurrencyRate) * IsNull(Discount_Percentage,0))/100)) + (IsNull(TotalQty,0)  * IsNull(CurrentPrice,0)*CurrencyRate)))"
            'objDataSet.Tables(0).Columns(EnumGridDetail.Total).Expression = "iif(Unit = 'Loose' , isnull(Qty,0) * isnull(Rate,0) , isnull(Qty,0) * isnull(Rate,0) * isnull([Pack Qty],0)) "
            ''Commented against task multi currency implementation on 30-12-2016 by Ameen
            'objDataSet.Tables(0).Columns(EnumGridDetail.Total).Expression = "IsNull(TotalQty,0) * IsNull(Rate,0)"   'Task#28082015
            objDataSet.Tables(0).Columns(EnumGridDetail.Total).Expression = "IsNull(TotalQty,0) * IsNull(Rate,0) * CurrencyRate"
            'objDataSet.Tables(0).Columns(EnumGridDetail.Pack_40Kg_Weight).Expression = "IsNull(TotalQty,0) / 40"   'Task#28082015
            'ElseIf ServicesItem = "True" Then
            '    objDataSet.Tables(0).Columns(EnumGridDetail.Total).Expression = "iif(Unit = 'Loose' , (ServiceQty * Rate) , ((ServiceQty * [Pack Qty])*Rate))"
        Else
            ''Commented against TASK-408 on 13-06-2016 by Ameen
            'objDataSet.Tables(0).Columns(EnumGridDetail.NetBill).Expression = "IIF(CurrentPrice=0,0,(((((((TotalQty + [Sample Qty]) * CurrentPrice) * Tax)/100) + (Freight * (TotalQty+[Sample Qty]))) - (((TotalQty * CurrentPrice) * Discount_Percentage)/100)) + (TotalQty  * CurrentPrice)))"
            objDataSet.Tables(0).Columns(EnumGridDetail.DiscountValue).Expression = "IIF(DiscountId= 1,((IsNull(PostDiscountPrice,0)*IsNull(DiscountFactor,0))/100), IsNull(DiscountFactor,0))" ''TFS2827
            objDataSet.Tables(0).Columns(EnumGridDetail.Price).Expression = "IsNull(PostDiscountPrice,0)"
            objDataSet.Tables(0).Columns(EnumGridDetail.CurrencyAmount).Expression = "IsNull(TotalQty,0) * IsNull(PostDiscountPrice,0)"   ''IIF(DiscountId= 1, IsNull(PostDiscountPrice,0)-((IsNull(PostDiscountPrice,0)*IsNull(DiscountFactor,0))/100), IsNull(PostDiscountPrice,0)-IsNull(DiscountFactor,0))"
            objDataSet.Tables(0).Columns(EnumGridDetail.NetBill).Expression = "IIF(CurrentPrice=0,0,(((((((TotalQty + [Sample Qty]) * CurrentPrice * CurrencyRate) * Tax)/100) + (Freight * (TotalQty+[Sample Qty]))) - (((TotalQty * CurrentPrice * CurrencyRate) * Discount_Percentage)/100)) + (TotalQty  * CurrentPrice * CurrencyRate)))"

            'objDataSet.Tables(0).Columns(EnumGridDetail.Total).Expression = "iif(Unit = 'Loose' , isnull(Qty,0) * isnull(Rate,0) , isnull(Qty,0) * isnull(Rate,0) * isnull([Pack Qty],0)) "
            ''Commented against TASK-408 on 13-06-2016 by Ameen
            'objDataSet.Tables(0).Columns(EnumGridDetail.Total).Expression = "IsNull(TotalQty,0) * IsNull(Rate,0)"   'Task#28082015
            objDataSet.Tables(0).Columns(EnumGridDetail.Total).Expression = "IsNull(TotalQty,0) * IsNull(Rate,0)*CurrencyRate"   'Task#28082015

            'objDataSet.Tables(0).Columns(EnumGridDetail.Pack_40Kg_Weight).Expression = "IsNull(TotalQty,0) / 40"    'Task#28082015
        End If
        If IsSO = True Then
            objDataSet.Tables(0).Columns(EnumGridDetail.RemainingQty).Expression = "OrderQty - DeliverQty - Qty"
        End If

        objDataSet.Tables(0).Columns("After Deduction Qty").Expression = "(isnull(TotalQty,0)-(IsNull([Bardana Deduction], 0)+ISNULL([Other Deduction], 0)))"
        'If Not Me.cmbPo.SelectedIndex > 0 Then
        '    objDataSet.Tables(0).Columns(EnumGridDetail.OrderQty).Expression = "Qty"
        'End If



        Me.grd.DataSource = objDataSet.Tables(0)



        Me.grd.RetrieveStructure()

        Me.grd.RootTable.Columns(EnumGridDetail.LocationID).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.LocationID).LimitToList = True
        Me.grd.RootTable.Columns(EnumGridDetail.LocationID).EditType = Janus.Windows.GridEX.EditType.Combo

        Me.grd.RootTable.Columns(EnumGridDetail.SO_ID).Visible = False

        Me.grd.RootTable.Columns(EnumGridDetail.SO_ID).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.SO_ID).LimitToList = True
        Me.grd.RootTable.Columns(EnumGridDetail.SO_ID).EditType = Janus.Windows.GridEX.EditType.Combo



        ''
        Me.grd.RootTable.Columns(EnumGridDetail.Engine_No).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.Engine_No).LimitToList = True
        Me.grd.RootTable.Columns(EnumGridDetail.Engine_No).EditType = Janus.Windows.GridEX.EditType.Combo

        Me.grd.RootTable.Columns(EnumGridDetail.Chassis_No).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.Chassis_No).LimitToList = True
        Me.grd.RootTable.Columns(EnumGridDetail.Chassis_No).EditType = Janus.Windows.GridEX.EditType.Combo

        ''Start TFS2827
        Me.grd.RootTable.Columns(EnumGridDetail.DiscountId).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.DiscountId).LimitToList = True
        Me.grd.RootTable.Columns(EnumGridDetail.DiscountId).EditType = Janus.Windows.GridEX.EditType.Combo
        ''End TFS2827

        ''
        Me.grd.RootTable.Columns("DeliveredQty").Visible = False
        Me.grd.RootTable.Columns("Stock").Visible = False
        Me.grd.RootTable.Columns("DeliveryId").Visible = False
        Me.grd.RootTable.Columns("DeliveryDetailId").Visible = False
        Me.grd.RootTable.Columns("CostPrice").Visible = False
        Me.grd.RootTable.Columns("DeliverQty").Caption = "Delivered Qty"

        Me.grd.RootTable.Columns(EnumGridDetail.OrderQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.OrderQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.OrderQty).FormatString = "N" & DecimalPointInQty

        Me.grd.RootTable.Columns(EnumGridDetail.DeliverQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.DeliverQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.DeliverQty).FormatString = "N" & DecimalPointInQty

        Me.grd.RootTable.Columns(EnumGridDetail.RemainingQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.RemainingQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.RemainingQty).FormatString = "N" & DecimalPointInQty

        'Me.grd.RootTable.Columns(EnumGridDetail.UM).HasValueList = True
        'Me.grd.RootTable.Columns(EnumGridDetail.UM).LimitToList = True
        'Me.grd.RootTable.Columns(EnumGridDetail.UM).EditType = Janus.Windows.GridEX.EditType.Combo

        ''Start TFS4181
        Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).LimitToList = True
        Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).EditType = Janus.Windows.GridEX.EditType.Combo
        ''End TFS4181
        ''Start TFS4181
        Me.grd.RootTable.Columns(EnumGridDetail.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
        Me.grd.RootTable.Columns(EnumGridDetail.ExpiryDate).FormatString = str_DisplayDateFormat
        ''End TFS4181
        Me.grd.RootTable.Columns("Origin").HasValueList = True
        Me.grd.RootTable.Columns("Origin").LimitToList = True
        Me.grd.RootTable.Columns("Origin").EditType = Janus.Windows.GridEX.EditType.Combo
        FillCombo("grdLocation")
        'FillCombo("grdUM")
        FillCombo("grdSO")

        ''DATED 02-03-2018
        FillCombo("grdEngine")
        FillCombo("grdChassis")
        ''

        'Ayesha Rehman: TFS2827 : 13-04-2018 : Fill combo boxes
        FillCombo("grdDiscountType")
        'Ayesha Rehman : TFS2827 : 13-04-2018 : End
        ''Ayesha Rehman: TFS4181: 16-08-2018 : Fill combo boxes
        FillCombo("grdBatchNo")
        ''Ayesha Rehman : TFS4181 : 16-08-2018 : End
        FillCombo("GrdOrigin")
        ApplyGridSettings()
        CtrlGrdBar2_Load_1(Nothing, Nothing)
        If objDataSet.Tables(0).Rows.Count > 0 Then
            If IsDBNull(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyId")) Or Val(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                'Me.cmbCurrency.SelectedValue = Nothing
                Me.cmbCurrency.Enabled = False
            Else
                'IsCurrencyEdit = True
                'IsNotCurrencyRateToAll = True
                FillCombo("Currency")
                Me.CurrencyRate = Val(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyRate").ToString)
                Me.cmbCurrency.SelectedValue = Val(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyId").ToString)
                Me.cmbCurrency.Enabled = False
            End If
            'cmbCurrency_SelectedIndexChanged(Nothing, Nothing)
        End If
        'grd1.Rows.Clear()
        'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

        '    grd1.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)(10), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)("BatchID"), objDataSet.Tables(0).Rows(i)("LocationID"), objDataSet.Tables(0).Rows(i)("Tax"))

        '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
        '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

        'Next

        'TODO: defin expression of columns
        'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

        '    With Me.grd1.Rows(i)

        '        If Me.grd1.Rows(i).Cells("Unit").Value = "Loose" Then
        '            'txtPackQty.Text = 1
        '            .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value)
        '        Else
        '            .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value) * Val(.Cells("PackQty").Value)
        '        End If

        '    End With
        'Next
        GetTotal()
        GetDeliveryOrderAnalysis()
        Me.grd.RootTable.Columns(EnumGridDetail.Color).Visible = True
        Me.grd.RootTable.Columns(EnumGridDetail.SED).Visible = True
        Me.grd.RootTable.Columns(EnumGridDetail.Size).Visible = True
        'Me.grd.RootTable.Columns(EnumGridDetail.ServiceQty).Visible = False
        Me.txtQty.Visible = True
        Me.grd.RootTable.Columns(EnumGridDetail.SampleQty).Visible = True
        '2598
        Me.grd.RootTable.Columns(EnumGridDetail.Comments).Visible = True
        If flgLoadAllItems = True Then
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                If Me.grd.RowCount > 0 Then
                    r.BeginEdit()
                    r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
                    r.EndEdit()
                End If
            Next
        End If
        If flgLoadAllItems = True Then
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                If Me.grd.RowCount > 0 Then
                    r.BeginEdit()
                    r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
                    r.EndEdit()
                End If
            Next
        End If
        ApplyGridSettings()
    End Sub
    Private Function Update_Record() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer

        CostId = GetCostCenterId(Me.cmbCompany.SelectedValue)

        Dim CurrentBalance As Double = CDbl(GetAccountBalance(Me.cmbVendor.ActiveRow.Cells(0).Value)) - Me.ExistingBalance
        Dim blnCheckCurrentStockByItem As Boolean = False
        If Not getConfigValueByType("CheckCurrentStockByItem").ToString = "Error" Then
            blnCheckCurrentStockByItem = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)
        End If
        ''TASK TFS1378
        Dim DCStockImpact As Boolean = False
        If Not getConfigValueByType("DCStockImpact").ToString = "Error" Then
            DCStockImpact = Convert.ToBoolean(getConfigValueByType("DCStockImpact").ToString)
        End If
        ''END TASK TFS1378
        objCon = Con

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim cmd As New OleDbCommand
        cmd.Connection = objCon
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT ISNULL(Sz1,0) as Qty, ISNULL(SampleQty,0) as SampleQty, IsNull(ArticleDefID, 0) As ArticleDefID, ISNULL(SO_ID,0) as SO_ID,ISNULL(Qty,0) as TotalQty, IsNull(SO_Detail_ID, 0) As SO_Detail_ID FROM DeliveryChalanDetailTable WHERE  DeliveryId = " & Me.txtReceivingID.Text & ""
        Dim da As New OleDbDataAdapter(cmd)
        Dim dtSavedItems As New DataTable
        da.Fill(dtSavedItems)

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'Rafay
            objCommand.CommandText = "Update DeliveryChalanMasterTable set DeliveryNo =N'" & txtPONo.Text & "',DeliveryDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',CustomerCode=" & cmbVendor.ActiveRow.Cells(0).Value & ", PoID=" & Val(Me.cmbPo.SelectedValue) & ",EmployeeCode=" & cmbSalesMan.SelectedValue & ", " _
            & " DeliveryQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(EnumGridDetail.TotalQty), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", DeliveryAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(EnumGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',PO_NO=N'" & txtPO.Text.Replace("'", "''") & "',UpdateUserName=N'" & LoginUserName.Replace("'", "''") & "', TransporterId=" & Me.cmbTransporter.SelectedValue & ",BiltyNo=N'" & Me.uitxtBiltyNo.Text.Replace("'", "''") & "',  PreviousBalance = " & CurrentBalance & ", FuelExpense=0, OtherExpense=0, adjustment = " & Val(Me.txtAdjustment.Text) & ", CostCenterId=" & CostId & ", Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", Delivered=" & IIf(Me.chkDelivered.Checked = True, 1, 0) & ", TransitInsurance=0, Driver_Name='" & Me.txtDriverName.Text.Replace("'", "''") & "', Vehicle_No='" & Me.txtVehicleNo.Text.Replace("'", "''") & "',Other_Company=N'" & Me.cmbOtherCompany.Text.Replace("'", "''") & "'  ,Arrival_Time=N'" & dtpArrivalTime.Value.ToString("yyyy-M-d h:mm:ss tt") & "',Departure_Time=" & IIf(Me.dtpDepartureTime.Checked = True, "Convert(Datetime,'" & Me.dtpDepartureTime.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)", "NULL") & ",  JobCardId= " & Val(Me.txtJobCardId.Text) & ", UserId= " & Val(LoginUserId) & " Where DeliveryID= " & txtReceivingID.Text & " "
            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from DeliveryChalanDetailTable where DeliveryID = " & txtReceivingID.Text
            objCommand.ExecuteNonQuery()
            ''TFS3520 : Ayesha Rehman : 14-06-2018
            ''Updating DC and Delivered Qty separately on the basis of SOSeparateClosure configuration
            If flgSOSeparateClosure = True Then
                If dtSavedItems.Rows.Count > 0 Then
                    If flgMultipleSalesOrder = False Then
                        If Me.cmbPo.SelectedIndex > 0 Then
                            For Each r As DataRow In dtSavedItems.Rows
                                objCommand.CommandText = String.Empty
                                objCommand.CommandText = "Update SalesOrderDetailTable set DCQty = abs(Isnull(DCQty,0) - " & r.Item(0) & "), DeliveredSchemeQty=abs(Isnull(DeliveredSchemeQty,0) - " & r.Item(1) & "), DCTotalQty=abs(ISNULL(DCTotalQty,0) - " & r.Item(4) & ") where SalesOrderID = " & Me.cmbPo.SelectedValue & " and ArticleDefID = " & r.Item(2) & " And SalesOrderDetailId = " & r.Item(5) & ""
                                objCommand.ExecuteNonQuery()
                            Next
                        End If
                    Else
                        For Each r As DataRow In dtSavedItems.Rows
                            objCommand.CommandText = String.Empty
                            objCommand.CommandText = "Update SalesOrderDetailTable set DCQty = abs(Isnull(DCQty,0) - " & r.Item(0) & "), DeliveredSchemeQty=abs(Isnull(DeliveredSchemeQty,0) - " & r.Item(1) & "), DCTotalQty=abs(ISNULL(DCTotalQty,0) - " & r.Item(4) & ") where SalesOrderID = " & r("SO_ID") & " and ArticleDefID = " & r.Item(2) & " And SalesOrderDetailId = " & r.Item(5) & " "
                            objCommand.ExecuteNonQuery()
                        Next
                    End If
                End If
            Else
                If dtSavedItems.Rows.Count > 0 Then
                    If flgMultipleSalesOrder = False Then
                        If Me.cmbPo.SelectedIndex > 0 Then
                            For Each r As DataRow In dtSavedItems.Rows
                                objCommand.CommandText = String.Empty
                                objCommand.CommandText = "Update SalesOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & "), DeliveredSchemeQty=abs(Isnull(DeliveredSchemeQty,0) - " & r.Item(1) & "), DeliveredTotalQty=abs(ISNULL(DeliveredTotalQty,0) - " & r.Item(4) & ") where SalesOrderID = " & Me.cmbPo.SelectedValue & " and ArticleDefID = " & r.Item(2) & " And SalesOrderDetailId = " & r.Item(5) & ""
                                objCommand.ExecuteNonQuery()
                            Next
                        End If
                    Else
                        For Each r As DataRow In dtSavedItems.Rows
                            objCommand.CommandText = String.Empty
                            objCommand.CommandText = "Update SalesOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & "), DeliveredSchemeQty=abs(Isnull(DeliveredSchemeQty,0) - " & r.Item(1) & "), DeliveredTotalQty=abs(ISNULL(DeliveredTotalQty,0) - " & r.Item(4) & ") where SalesOrderID = " & r("SO_ID") & " and ArticleDefID = " & r.Item(2) & " And SalesOrderDetailId = " & r.Item(5) & " "
                            objCommand.ExecuteNonQuery()
                        Next
                    End If
                End If
            End If

            objCommand.CommandText = ""
            StockList = New List(Of StockDetail)
            For i = 0 To grd.GetRows.Length - 1
                If blnCheckCurrentStockByItem = True Then
                    CheckCurrentStockByItem(Me.grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value, Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TotalQty).Value), Me.grd, , trans)
                End If

                ''13-Spe-2014 Task:2843 Imran Ali Restriction Duplicate Egine No/Chassis No In Sales Return/Delivery Chalan
                'Task:2415 Check Duplicate Engine No.
                If blnTradePriceExceed = True Then
                    If Val(Me.grd.GetRows(i).Cells("TradePrice").Value.ToString) < Val(Me.grd.GetRows(i).Cells("Rate").Value.ToString) Then
                        ' If Not msg_Confirm("Sales Price is greater than trade price" & (Chr(13)) & "Do u want to add this item?") = True Then
                        Throw New Exception("Sale price is less than trade price.")
                        'End If
                    End If
                End If
                If flgVehicleIdentificationInfo = True Then
                    Dim Engine_No As String = ""
                    Dim Chassis_No As String = ""
                    If Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
                        Engine_No = Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Substring(4)
                    End If
                    If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                        Chassis_No = Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Substring(4)
                    End If
                    objCommand.CommandText = ""
                    objCommand.CommandText = "SELECT dbo.DeliveryChalanDetailTable.ArticleDefId, Stuff(DeliveryChalanDetailTable.Engine_No, 1, 4, '') As Engine_No, Stuff(DeliveryChalanDetailTable.Chassis_No, 1, 4, '') As Chassis_No " _
                                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                    & " dbo.DeliveryChalanDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.DeliveryChalanDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"
                    If Engine_No.Length > 0 Then
                        objCommand.CommandText += " AND Stuff(DeliveryChalanDetailTable.Engine_No, 1, 4, '') =N'" & Engine_No & "'"
                    End If
                    'Dim dt As DataTable = GetDataTable(objCommand.CommandText)
                    'If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                    '    objCommand.CommandText += " AND DeliveryChalanDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
                    'End If
                    Dim dtVehicleEngineNoIdentificationInfo As New DataTable
                    Dim daVehicleEngineNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                    daVehicleEngineNoIdentificationInfo.Fill(dtVehicleEngineNoIdentificationInfo)

                    ''Retrieving Chasis No
                    objCommand.CommandText = ""
                    objCommand.CommandText = "SELECT dbo.DeliveryChalanDetailTable.ArticleDefId, Stuff(DeliveryChalanDetailTable.Engine_No, 1, 4, '') As Engine_No, Stuff(DeliveryChalanDetailTable.Chassis_No, 1, 4, '') As Chassis_No " _
                                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                    & " dbo.DeliveryChalanDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.DeliveryChalanDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"
                    'If Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
                    '    objCommand.CommandText += " AND DeliveryChalanDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "'"
                    'End If
                    If Chassis_No.Length > 0 Then
                        objCommand.CommandText += " AND Stuff(DeliveryChalanDetailTable.Chassis_No, 1, 4, '') =N'" & Chassis_No & "'"
                    End If
                    Dim dtVehicleChasisNoIdentificationInfo As New DataTable
                    Dim daVehicleChasisNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                    daVehicleChasisNoIdentificationInfo.Fill(dtVehicleChasisNoIdentificationInfo)

                    'Task:2606 Engine_No and Chassis No Data From Sales Return
                    ''Retrieving Return Engine No
                    objCommand.CommandText = ""
                    objCommand.CommandText = "SELECT dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Engine_No, SalesReturnDetailTable.Chassis_No  " _
                                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                    & " dbo.SalesReturnDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesReturnDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"

                    objCommand.CommandText += " AND Stuff(SalesReturnDetailTable.Engine_No, 1, 4, '')=N'" & Engine_No & "'"


                    'If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                    '    objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
                    'End If
                    Dim dtSalesReturnVehichleEngineNoInfo As New DataTable
                    Dim daSalesReturnVehichleEngineNoInfo As New OleDbDataAdapter(objCommand)
                    daSalesReturnVehichleEngineNoInfo.Fill(dtSalesReturnVehichleEngineNoInfo)

                    ''Retrieving Return Chasis No
                    objCommand.CommandText = ""
                    objCommand.CommandText = "SELECT dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Engine_No, SalesReturnDetailTable.Chassis_No  " _
                                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                    & " dbo.SalesReturnDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesReturnDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"

                    objCommand.CommandText += " AND Stuff(SalesReturnDetailTable.Chassis_No, 1, 4, '')=N'" & Chassis_No & "'"


                    'If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                    '    objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
                    'End If
                    Dim dtSalesReturnVehichleChasisNoInfo As New DataTable
                    Dim daSalesReturnVehichleChasisNoInfo As New OleDbDataAdapter(objCommand)
                    daSalesReturnVehichleChasisNoInfo.Fill(dtSalesReturnVehichleChasisNoInfo)

                    If dtVehicleEngineNoIdentificationInfo IsNot Nothing Then
                        If dtVehicleEngineNoIdentificationInfo.Rows.Count > 0 Then
                            If dtVehicleEngineNoIdentificationInfo.Rows.Count > dtSalesReturnVehichleEngineNoInfo.Rows.Count Then
                                If dtVehicleEngineNoIdentificationInfo.Rows(0).Item("Engine_No").ToString.Length > 0 Or Engine_No.Length > 0 Then
                                    If Engine_No = dtVehicleEngineNoIdentificationInfo.Rows(0).Item("Engine_No").ToString Then
                                        Throw New Exception("Engine no [" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "] already exists")
                                    End If
                                End If
                            End If
                        End If
                    End If

                    If dtVehicleChasisNoIdentificationInfo IsNot Nothing Then
                        If dtVehicleChasisNoIdentificationInfo.Rows.Count > 0 Then
                            If dtVehicleChasisNoIdentificationInfo.Rows.Count > dtSalesReturnVehichleChasisNoInfo.Rows.Count Then
                                If dtVehicleChasisNoIdentificationInfo.Rows(0).Item("Chassis_No").ToString.Length > 0 Or Chassis_No.Length > 0 Then
                                    If Chassis_No = dtVehicleChasisNoIdentificationInfo.Rows(0).Item("Chassis_No").ToString Then
                                        Throw New Exception("Chassis no [" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "] already exists")
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If



                ''TASK TFS1378 ON 13-10-2017


                Dim CostPrice As Double = 0D
                Dim CrrStock As Double = 0D
                Dim DiscountedPrice As Double = 0
                DiscountedPrice = Me.grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value - Me.grd.GetRows(i).Cells(EnumGridDetail.Price).Value
                Dim dblPurchasePrice As Double = 0D
                Dim dblCostPrice As Double = 0D
                Dim strPriceData() As String = GetRateByItem(Val(Me.grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value.ToString)).Split(",")
                If strPriceData.Length > 1 Then
                    dblCostPrice = Val(strPriceData(0).ToString)
                    dblPurchasePrice = Val(strPriceData(1).ToString)
                End If

                If flgAvgRate = True Then
                    If dblCostPrice > 0 Then
                        CostPrice = dblCostPrice
                    Else
                        CostPrice = dblPurchasePrice
                    End If
                Else
                    CostPrice = dblPurchasePrice
                End If
                If getConfigValueByType("DCStockImpact") = "True" Then
                    ''TASK TFS3324
                    Dim IsLogicalItemStockMade As Boolean = False
                    If CType(grd.GetRows(i).Cells("LogicalItem").Value, Boolean) = True Then
                        ''Below line is commented on 31-05-2018 against TASK TFS3324
                        'FillModelOfStockDetail(grd.GetRows(i).Cells("ArticleDefId").Value, grd.GetRows(i).Cells("LocationId").Value, Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) + Val(grd.GetRows(i).Cells("Sample Qty").Value.ToString), grd.GetRows(i).Cells("Comments").Value.ToString)
                        IsLogicalItemStockMade = GetItemsOfLogicalItem(grd.GetRows(i).Cells("ArticleDefId").Value, grd.GetRows(i).Cells("LocationId").Value, Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) + Val(grd.GetRows(i).Cells("Sample Qty").Value.ToString), grd.GetRows(i).Cells("Comments").Value.ToString, objCommand)
                    End If
                    If CType(grd.GetRows(i).Cells("LogicalItem").Value, Boolean) = False Or IsLogicalItemStockMade = False Then
                        StockDetail = New StockDetail
                        StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                        StockDetail.LocationId = grd.GetRows(i).Cells("LocationId").Value
                        StockDetail.ArticleDefId = grd.GetRows(i).Cells("ArticleDefId").Value
                        StockDetail.InQty = 0
                        StockDetail.OutQty = Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)
                        ''Commmented Against TFS3589 : Ayesha Rehman : 25-06-2018 :  rate on run time is not saved in item ledger or stock statement
                        ' StockDetail.Rate = IIf(CostPrice = 0, Val(grd.GetRows(i).Cells(EnumGridDetail.PurchasePrice).Value), CostPrice)
                        StockDetail.Rate = Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value.ToString)
                        StockDetail.InAmount = 0
                        ''Commmented Against TFS3589 : Ayesha Rehman : 25-06-2018 :  rate on run time is not saved in item ledger or stock statement
                        ' StockDetail.OutAmount = ((Val(grd.GetRows(i).Cells("TotalQty").Value) + Val(grd.GetRows(i).Cells("Sample Qty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells(EnumGridDetail.PurchasePrice).Value), CostPrice))
                        StockDetail.OutAmount = ((Val(grd.GetRows(i).Cells("TotalQty").Value) + Val(grd.GetRows(i).Cells("Sample Qty").Value)) * Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value.ToString))
                        StockDetail.Remarks = grd.GetRows(i).Cells("Comments").Value.ToString
                        StockDetail.Engine_No = grd.GetRows(i).Cells("Engine_No").Value.ToString
                        StockDetail.Chassis_No = grd.GetRows(i).Cells("Chassis_No").Value.ToString
                        ''Start TFS4181
                        StockDetail.BatchNo = grd.GetRows(i).Cells(EnumGridDetail.BatchNo).Value.ToString
                        ''End TFS4181
                        ''Start TASK-TFS4181
                        StockDetail.ExpiryDate = CType(grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value, Date).ToString("yyyy-M-d h:mm:ss tt")
                        ''End TASK-4181
                        StockDetail.Origin = grd.GetRows(i).Cells("Origin").Value.ToString
                        CostPrice = StockDetail.Rate
                        StockDetail.PackQty = Val(grd.GetRows(i).Cells("Pack Qty").Value.ToString)
                        StockDetail.Out_PackQty = Val(grd.GetRows(i).Cells("Qty").Value.ToString)
                        StockDetail.In_PackQty = 0
                        StockList.Add(StockDetail)
                    End If
                    ''END TASK TFS3324
                End If
                '' END TASK TFS1378 ON 13-10-2017

                ''Retrieving Engine No
                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "SELECT dbo.DeliveryChalanDetailTable.ArticleDefId, DeliveryChalanDetailTable.Engine_No, DeliveryChalanDetailTable.Chassis_No  " _
                '                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                '                                    & " dbo.DeliveryChalanDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.DeliveryChalanDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"
                '    If Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
                '        objCommand.CommandText += " AND DeliveryChalanDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "'"
                '    End If
                '    'If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                '    '    objCommand.CommandText += " AND DeliveryChalanDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
                '    'End If
                '    Dim dtVehicleEngineNoIdentificationInfo As New DataTable
                '    Dim daVehicleEngineNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                '    daVehicleEngineNoIdentificationInfo.Fill(dtVehicleEngineNoIdentificationInfo)

                '    ''Retrieving Chasis No
                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "SELECT dbo.DeliveryChalanDetailTable.ArticleDefId, DeliveryChalanDetailTable.Engine_No, DeliveryChalanDetailTable.Chassis_No  " _
                '                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                '                                    & " dbo.DeliveryChalanDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.DeliveryChalanDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"
                '    'If Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
                '    '    objCommand.CommandText += " AND DeliveryChalanDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "'"
                '    'End If
                '    If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                '        objCommand.CommandText += " AND DeliveryChalanDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
                '    End If
                '    Dim dtVehicleChasisNoIdentificationInfo As New DataTable
                '    Dim daVehicleChasisNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                '    daVehicleChasisNoIdentificationInfo.Fill(dtVehicleChasisNoIdentificationInfo)

                '    'Task:2606 Engine_No and Chassis No Data From Sales Return
                '    ''Retrieving Return Engine No
                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "SELECT dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Engine_No, SalesReturnDetailTable.Chassis_No  " _
                '                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                '                                    & " dbo.SalesReturnDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesReturnDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"

                '    objCommand.CommandText += " AND SalesReturnDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "'"


                '    'If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                '    '    objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
                '    'End If
                '    Dim dtSalesReturnVehichleEngineNoInfo As New DataTable
                '    Dim daSalesReturnVehichleEngineNoInfo As New OleDbDataAdapter(objCommand)
                '    daSalesReturnVehichleEngineNoInfo.Fill(dtSalesReturnVehichleEngineNoInfo)

                '    ''Retrieving Return Chasis No
                '    objCommand.CommandText = ""
                '    objCommand.CommandText = "SELECT dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Engine_No, SalesReturnDetailTable.Chassis_No  " _
                '                                    & " FROM dbo.ArticleDefTable INNER JOIN " _
                '                                    & " dbo.SalesReturnDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesReturnDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"

                '    objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"


                '    'If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                '    '    objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
                '    'End If
                '    Dim dtSalesReturnVehichleChasisNoInfo As New DataTable
                '    Dim daSalesReturnVehichleChasisNoInfo As New OleDbDataAdapter(objCommand)
                '    daSalesReturnVehichleChasisNoInfo.Fill(dtSalesReturnVehichleChasisNoInfo)

                '    If dtVehicleEngineNoIdentificationInfo IsNot Nothing Then
                '        If dtVehicleEngineNoIdentificationInfo.Rows.Count > 0 Then
                '            If dtVehicleEngineNoIdentificationInfo.Rows.Count > dtSalesReturnVehichleEngineNoInfo.Rows.Count Then
                '                If dtVehicleEngineNoIdentificationInfo.Rows(0).Item("Engine_No").ToString.Length > 0 Or Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
                '                    If Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString = dtVehicleEngineNoIdentificationInfo.Rows(0).Item("Engine_No").ToString Then
                '                        Throw New Exception("Engine no [" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "] already exists")
                '                    End If
                '                End If
                '            End If
                '        End If
                '    End If

                '    If dtVehicleChasisNoIdentificationInfo IsNot Nothing Then
                '        If dtVehicleChasisNoIdentificationInfo.Rows.Count > 0 Then
                '            If dtVehicleChasisNoIdentificationInfo.Rows.Count > dtSalesReturnVehichleChasisNoInfo.Rows.Count Then
                '                If dtVehicleChasisNoIdentificationInfo.Rows(0).Item("Chassis_No").ToString.Length > 0 Or Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                '                    If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString = dtVehicleChasisNoIdentificationInfo.Rows(0).Item("Chassis_No").ToString Then
                '                        Throw New Exception("Chassis no [" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "] already exists")
                '                    End If
                '                End If
                '            End If
                '        End If
                '    End If
                'End If

                'End Task: 2606
                'End Task:2415
                'End Task:2842



                objCommand.CommandText = "Insert into DeliveryChalanDetailTable(DeliveryId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID, LocationId, TaxPercent, SampleQty, SEDPercent,TradePrice, Discount_Percentage, Freight, MarketReturns,SO_ID, UOM, PurchasePrice,Pack_Desc,Engine_No, Chassis_No, Comments,DeliveredQty,ExpiryDate,Other_Comments,CostPrice, SO_Detail_ID, Gross_Weights,Tray_Weights,Net_Weights, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencyAmount, PackPrice, PerviouslyDeliveredQty, BardanaDeduction, OtherDeduction, DiscountId, DiscountValue, DiscountFactor , PostDiscountPrice, Origin, AlternativeItem, AlternativeItemId) values( " _
                                 & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGridDetail.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value.ToString) & ", " _
                                 & " " & Val(Me.grd.GetRows(i).Cells("TotalQty").Value.ToString) & ",  " _
                                 & Val(grd.GetRows(i).Cells(EnumGridDetail.Price).Value.ToString) & ", " & Val(grd.GetRows(i).Cells(EnumGridDetail.PackQty).Value.ToString) & " , " & Val(grd.GetRows(i).Cells(EnumGridDetail.CurrentPrice).Value.ToString) & ",N'" & grd.GetRows(i).Cells(EnumGridDetail.BatchNo).Value & "', " _
                              & grd.GetRows(i).Cells(EnumGridDetail.BatchID).Value & " , " & grd.GetRows(i).Cells(EnumGridDetail.LocationID).Value & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.Tax).Value.ToString = "", 0, grd.GetRows(i).Cells(EnumGridDetail.Tax).Value) & ", " & Val(grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value.ToString()) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SED).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TradePrice).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Discount_Percentage).Value) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Freight).Value) & "," & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.MarketReturns).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) & ",N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.UM).Text.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.PurchasePrice).Value.ToString) & ", N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("DeliveredQty").Value.ToString) & ", " & IIf(grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value.ToString = "", "NULL", "Convert(DateTime,N'" & CDate(IIf(Me.grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value.ToString = "", Date.Now, Me.grd.GetRows(i).Cells(EnumGridDetail.ExpiryDate).Value)).ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", N'" & grd.GetRows(i).Cells("Other Comments").Value.ToString.Replace("'", "''") & "'," & Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString) & " , " & Val(Me.grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & " , " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Gross_Weights).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Tray_Weights).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.Net_Weight).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("CurrencyAmount").Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells("PackPrice").Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.DeliverQty).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.BardanaDeduction).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.OtherDeduction).Value.ToString) & "," & Val(grd.GetRows(i).Cells(EnumGridDetail.DiscountId).Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.DiscountValue).Value.ToString) & "," & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.DiscountFactor).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.PostDiscountPrice).Value) & ",N'" & grd.GetRows(i).Cells("Origin").Value & "',N'" & (grd.GetRows(i).Cells("AlternativeItem").Value) & "'," & Val(grd.GetRows(i).Cells("AlternativeItemId").Value.ToString) & ")Select @@Identity "

                Dim intDeliveryChalanId As Integer = objCommand.ExecuteScalar()

                objCommand.CommandText = ""
                objCommand.CommandText = "Update SalesDetailTable Set DeliveryChalanDetailID=" & intDeliveryChalanId & " WHERE DeliveryChalanDetailId=" & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.DeliveryDetailID).Value.ToString) & ""
                objCommand.ExecuteNonQuery()
                ''TFS3520 : Ayesha Rehman : 14-06-2018
                ''Updating DC Qty and Delivered qty in SalesOrderMasterTable on the basis of SOSeparateClosure 
                If flgSOSeparateClosure = True Then
                    If flgMultipleSalesOrder = False Then
                        objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                & " SET DCQty = isnull(DCQty,0) +  " & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) & "), DCTotalQty= IsNull(DCTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TotalQty).Value) & " " _
                                                & " WHERE     (SalesOrderID = " & Me.cmbPo.SelectedValue & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ") And (SalesOrderDetailId =" & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ")  "
                        objCommand.ExecuteNonQuery()
                    Else
                        If Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) > 0 Then
                            objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                   & " SET              DCQty = isnull(DCQty,0) +  " & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) & "), DCTotalQty= IsNull(DCTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TotalQty).Value) & " " _
                                                   & " WHERE     (SalesOrderID = " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ") And (SalesOrderDetailId =" & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ")"
                            objCommand.ExecuteNonQuery()
                        End If
                    End If
                Else
                    If flgMultipleSalesOrder = False Then
                        objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                & " SET DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) & "), DeliveredTotalQty= IsNull(DeliveredTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TotalQty).Value) & " " _
                                                & " WHERE     (SalesOrderID = " & Me.cmbPo.SelectedValue & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ") And (SalesOrderDetailId =" & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ")  "
                        objCommand.ExecuteNonQuery()
                    Else
                        If Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) > 0 Then
                            objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                   & " SET              DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells(EnumGridDetail.Qty).Value) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SampleQty).Value) & "), DeliveredTotalQty= IsNull(DeliveredTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.TotalQty).Value) & " " _
                                                   & " WHERE     (SalesOrderID = " & Val(Me.grd.GetRows(i).Cells(EnumGridDetail.SO_ID).Value) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(EnumGridDetail.ArticleID).Value) & ") And (SalesOrderDetailId =" & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ")"
                            objCommand.ExecuteNonQuery()
                        End If
                    End If
                End If
            Next
            ''TFS3520 : Ayesha Rehman : 14-06-2018
            ''Updating DC Status and SO Status in SalesOrderMasterTable on the basis of SOSeparateClosure 
            If flgSOSeparateClosure = True Then
                If flgMultipleSalesOrder = False Then
                    If Me.cmbPo.SelectedIndex > 0 Then
                        objCommand.CommandText = "Select IsNull(Sz1,0) as Qty , isnull(DCQty, 0) as DeliveredQty, Isnull(DeliveredSchemeQty,0) as DeliveredSchemeQty  from SalesOrderDetailTable where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                        da.SelectCommand = objCommand
                        Dim dt As New DataTable
                        da.Fill(dt)
                        Dim blnEqual As Boolean = True
                        If dt.Rows.Count > 0 Then
                            For Each r As DataRow In dt.Rows
                                If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
                                    blnEqual = False
                                    Exit For
                                End If
                            Next
                        End If
                        ''Below lines are commented against TASK TFS2942
                        'If blnEqual = True Then
                        '    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                        '    objCommand.ExecuteNonQuery()
                        'Else
                        '    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                        '    objCommand.ExecuteNonQuery()
                        'End If
                        If blnEqual = True Then
                            objCommand.CommandText = "Update SalesOrderMasterTable set DCStatus = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                            objCommand.ExecuteNonQuery()
                        Else
                            objCommand.CommandText = "Update SalesOrderMasterTable set DCStatus = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                            objCommand.ExecuteNonQuery()
                        End If
                    End If

                Else

                    objCommand.CommandText = "Select DISTINCT ISNULL(SO_ID,0) as SO_ID From DeliveryChalanDetailTable WHERE DeliveryID=" & Val(Me.txtReceivingID.Text) & " AND ISNULL(Qty,0) <> 0"
                    Dim dtPO As New DataTable
                    Dim daPO As New OleDbDataAdapter(objCommand)
                    daPO.Fill(dtPO)
                    If dtPO IsNot Nothing Then
                        If dtPO.Rows.Count > 0 Then
                            For Each row As DataRow In dtPO.Rows

                                objCommand.CommandText = "Select isnull(Sz1,0)-isnull(DCQty, 0) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & row("SO_ID") & "  And isnull(Sz1,0)-isnull(DCQty , 0) > 0 "
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
                                ''Below lines are commented against TASK TFS2942
                                'If blnEqual1 = True Then
                                '    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
                                '    objCommand.ExecuteNonQuery()
                                'Else
                                '    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
                                '    objCommand.ExecuteNonQuery()
                                'End If
                                If blnEqual1 = True Then
                                    objCommand.CommandText = "Update SalesOrderMasterTable set DCStatus = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
                                    objCommand.ExecuteNonQuery()
                                Else
                                    objCommand.CommandText = "Update SalesOrderMasterTable set DCStatus = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
                                    objCommand.ExecuteNonQuery()
                                End If
                            Next
                        End If
                    End If
                End If
            Else
                If flgMultipleSalesOrder = False Then
                    If Me.cmbPo.SelectedIndex > 0 Then
                        objCommand.CommandText = "Select IsNull(Sz1,0) as Qty , isnull(DeliveredQty, 0) as DeliveredQty, Isnull(DeliveredSchemeQty,0) as DeliveredSchemeQty  from SalesOrderDetailTable where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                        da.SelectCommand = objCommand
                        Dim dt As New DataTable
                        da.Fill(dt)
                        Dim blnEqual As Boolean = True
                        If dt.Rows.Count > 0 Then
                            For Each r As DataRow In dt.Rows
                                If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
                                    blnEqual = False
                                    Exit For
                                End If
                            Next
                        End If
                        If blnEqual = True Then
                            objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                            objCommand.ExecuteNonQuery()
                        Else
                            objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Me.cmbPo.SelectedValue & ""
                            objCommand.ExecuteNonQuery()
                        End If
                    End If

                Else

                    objCommand.CommandText = "Select DISTINCT ISNULL(SO_ID,0) as SO_ID From DeliveryChalanDetailTable WHERE DeliveryID=" & Val(Me.txtReceivingID.Text) & " AND ISNULL(Qty,0) <> 0"
                    Dim dtPO As New DataTable
                    Dim daPO As New OleDbDataAdapter(objCommand)
                    daPO.Fill(dtPO)
                    If dtPO IsNot Nothing Then
                        If dtPO.Rows.Count > 0 Then
                            For Each row As DataRow In dtPO.Rows

                                objCommand.CommandText = "Select isnull(Sz1,0)-isnull(DeliveredQty, 0) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & row("SO_ID") & "  And isnull(Sz1,0)-isnull(DeliveredQty , 0) > 0 "
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
                                    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
                                    objCommand.ExecuteNonQuery()
                                Else
                                    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & row("SO_ID") & ""
                                    objCommand.ExecuteNonQuery()
                                End If
                            Next
                        End If
                    End If
                End If
            End If


            'Task:2801 Update Sale Order Status 
            objCommand.CommandText = ""
            objCommand.CommandText = "Update DeliveryChalanMasterTable Set Status=Case When IsNull(SODT.Qty,0) > 0 Then 'Open' Else 'Close' End From DeliveryChalanMasterTable, (Select DeliveryId, SUM(IsNull(Sz1,0)-IsNull(DeliveredQty,0)) as Qty From DeliveryChalanDetailTable WHERE DeliveryChalanDetailTable.DeliveryId=" & Val(Me.txtReceivingID.Text) & " Group By DeliveryId ) SODt WHERE SoDt.DeliveryId = DeliveryChalanMasterTable.DeliveryId AND DeliveryChalanMasterTable.DeliveryId=" & Val(Me.txtReceivingID.Text) & ""
            objCommand.ExecuteNonQuery()
            'End Task:2801

            SaveDocument(Val(Me.txtReceivingID.Text), Me.Name, trans)
            '' TASK TFS1378
            If DCStockImpact = True Then
                FillModel()
                StockMaster.StockTransId = StockTransId(Me.txtPONo.Text, trans)
                Call New StockDAL().Update(StockMaster, trans)
            End If
            '' END TFS1378
            trans.Commit()
            Update_Record = True

            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, Me.txtPONo.Text.Trim, True)
            SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, Me.txtPONo.Text.Trim, True)

            ''Start TFS3113
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim, Me.Name) Then
                If ValidateApprovalProcessIsInProgressAgain(Me.txtPONo.Text.Trim, Me.Name) = False Then
                    SaveApprovalLog(EnumReferenceType.DeliveryChallan, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Delivery Chalan," & cmbVendor.Text & "", Me.Name, 7)
                End If
            End If
            ''End TFS3113


            setEditMode = True
            getVoucher_Id = Me.txtReceivingID.Text
            setVoucherNo = Me.txtPONo.Text
            Total_Amount = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(EnumGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) 'Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)


            '...................... Send SMS .............................
            If GetSMSConfig("SMS To Location On Delivery Chalan").Enable = True Then
                Dim strSMSBody As String = String.Empty
                Dim objData As DataTable = CType(Me.grd.DataSource, DataTable)
                Dim dt_Loc As DataTable = objData.DefaultView.ToTable("Default", True, "LocationId")
                Dim drData() As DataRow
                For j As Integer = 0 To dt_Loc.Rows.Count - 1
                    strSMSBody = String.Empty
                    'Rafay
                    strSMSBody += "Delivery Chalan, Doc No: " & Me.txtPONo.Text & ", Doc Date: " & Me.dtpPODate.Value.ToShortDateString & ", Customer: " & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString & ", Bilty No: " & Me.uitxtBiltyNo.Text & ", Remarks:" & Me.txtRemarks.Text & ",PO_NO:" & Me.txtPO.Text & ", "
                    drData = objData.Select("LocationId=" & dt_Loc.Rows(j).Item(0).ToString & "")
                    Dim dblTotalQty As Double = 0D
                    Dim dblTotalAmount As Double = 0D
                    Dim strEngineNo As String = String.Empty

                    For Each dr As DataRow In drData
                        dblTotalQty += IIf(dr(EnumGridDetail.Unit).ToString = "Loose", Val(dr(EnumGridDetail.Qty).ToString), Val(dr(EnumGridDetail.Qty).ToString) * Val(dr(EnumGridDetail.PackQty).ToString))
                        strSMSBody += " " & dr(EnumGridDetail.Item).ToString & ", " & IIf(dr(EnumGridDetail.Unit).ToString = "Loose", " " & dr(EnumGridDetail.Qty).ToString & "", "" & (Val(dr(EnumGridDetail.Qty)).ToString * Val(dr(EnumGridDetail.PackQty)).ToString) & "") & ", "
                        If strEngineNo.Length > 0 Then
                            strEngineNo += " ," & dr(EnumGridDetail.Engine_No).ToString
                        Else
                            strEngineNo = " " & dr(EnumGridDetail.Engine_No).ToString
                        End If
                    Next
                    strSMSBody += " Total Qty: " & dblTotalQty & ""
                    If strEngineNo.Length > 0 Then
                        strSMSBody += " Engine No:  " & strEngineNo.ToString
                    End If
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

            If GetSMSConfig("Delivery Chalan").Enable = True Then
                If msg_Confirm(str_ConfirmSendSMSMessage) = False Then Exit Try
                Dim strDetailMessage As String = String.Empty
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    'Task#08082015 Add configuration for sending sms with just item qunatity and engine no
                    If getConfigValueByType("DeliveryChalanByEnigneNo").ToString = "True" Then
                        If strDetailMessage.Length = 0 Then
                            'strDetailMessage = "Item Qty: " & IIf(r.Cells(EnumGridDetail.Unit).Value.ToString = "Loose", Val(r.Cells(EnumGridDetail.Qty).Value.ToString), Val(r.Cells(EnumGridDetail.Qty).Value.ToString) * Val(r.Cells(EnumGridDetail.PackQty).Value.ToString)) & IIf(r.Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0, ", Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString, "")
                            strDetailMessage = "Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString

                        Else
                            strDetailMessage += ", Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString
                        End If
                        'End Task#08082015
                    Else
                        If strDetailMessage.Length = 0 Then
                            strDetailMessage = r.Cells(EnumGridDetail.Item).Value.ToString & ", PackQty: " & Val(r.Cells(EnumGridDetail.PackQty).Value.ToString) & ", Qty: " & IIf(r.Cells(EnumGridDetail.Unit).Value.ToString = "Loose", Val(r.Cells(EnumGridDetail.Qty).Value.ToString), Val(r.Cells(EnumGridDetail.Qty).Value.ToString) * Val(r.Cells(EnumGridDetail.PackQty).Value.ToString)) & IIf(r.Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0, ", Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString, "")
                        Else
                            strDetailMessage += "," & r.Cells(EnumGridDetail.Item).Value.ToString & ",  PackQty: " & Val(r.Cells(EnumGridDetail.PackQty).Value.ToString) & ",Qty: " & IIf(r.Cells(EnumGridDetail.Unit).Value.ToString = "Loose", Val(r.Cells(EnumGridDetail.Qty).Value.ToString), Val(r.Cells(EnumGridDetail.Qty).Value.ToString) * Val(r.Cells(EnumGridDetail.PackQty).Value.ToString)) & IIf(r.Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0, ", Engine No: " & r.Cells(EnumGridDetail.Engine_No).Value.ToString, "")
                        End If
                    End If
                Next
                Dim objTemp As New SMSTemplateParameter
                Dim obj As Object = GetSMSTemplate("Delivery Chalan")
                If obj IsNot Nothing Then
                    objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                    Dim strMessage As String = objTemp.SMSTemplate
                    strMessage = strMessage.Replace("@AccountTitle", Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString).Replace("@AccountCode", Me.cmbVendor.ActiveRow.Cells("Code").Value.ToString).Replace("@DocumentNo", Me.txtPONo.Text).Replace("@DocumentDate", Me.dtpPODate.Value.ToShortDateString).Replace("@OtherDoc", Me.uitxtBiltyNo.Text).Replace("@Remarks", Me.txtRemarks.Text).Replace("@Amount", Me.grd.GetTotal(grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@Quantity", Me.grd.GetTotal(grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@CompanyName", CompanyTitle).Replace("@SIRIUS", "Automated by www.siriussolution.com").Replace("@PreviousBalance", _Previous_Balance).Replace("@DetailInformation", strDetailMessage).Replace("@BiltyNo", Me.uitxtBiltyNo.Text).Replace("@DriverName", Me.txtDriverName.Text).Replace("@VehicleNo", Me.txtVehicleNo.Text).Replace("@Transporter", CStr(IIf(Me.cmbTransporter.SelectedIndex > 0, Me.cmbTransporter.Text, "")))
                    SaveSMSLog(strMessage, Me.cmbVendor.ActiveRow.Cells("Mobile").Value.ToString, "Delivery Chalan")

                    If GetSMSConfig("Delivery Chalan").EnabledAdmin = True Then
                        For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                            If strMob.Length > 10 Then
                                SaveSMSLog(strMessage, strMob, "Delivery Chalan")
                            End If
                        Next
                    End If
                End If
            End If

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
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True


                    If getConfigValueByType("DCDependentonSO").ToString = "True" AndAlso Me.cmbPo.SelectedIndex > 0 Then
                        If Save() Then
                            RefreshControls()
                            ClearDetailControls()

                            If BackgroundWorker2.IsBusy Then Exit Sub
                            BackgroundWorker2.RunWorkerAsync()


                            If BackgroundWorker3.IsBusy Then Exit Sub
                            BackgroundWorker3.RunWorkerAsync()
                            Do While BackgroundWorker3.IsBusy
                                Application.DoEvents()
                            Loop

                        Else

                            Exit Sub 'MsgBox("Record has not been added")

                        End If
                    ElseIf getConfigValueByType("DCDependentonSO").ToString = "False" Then
                        If Save() Then
                            RefreshControls()
                            ClearDetailControls()

                            If BackgroundWorker2.IsBusy Then Exit Sub
                            BackgroundWorker2.RunWorkerAsync()


                            If BackgroundWorker3.IsBusy Then Exit Sub
                            BackgroundWorker3.RunWorkerAsync()
                            Do While BackgroundWorker3.IsBusy
                                Application.DoEvents()
                            Loop

                        Else
                            Exit Sub 'MsgBox("Record has not been added")

                        End If
                    Else
                        Throw New Exception("Delivery Challan can not be made with out any Sales Order.")
                    End If
                    'If Save() Then
                    '    RefreshControls()
                    '    ClearDetailControls()

                    '    If BackgroundWorker2.IsBusy Then Exit Sub
                    '    BackgroundWorker2.RunWorkerAsync()


                    '    If BackgroundWorker3.IsBusy Then Exit Sub
                    '    BackgroundWorker3.RunWorkerAsync()
                    '    Do While BackgroundWorker3.IsBusy
                    '        Application.DoEvents()
                    '    Loop

                    'Else
                    '    Exit Sub 'MsgBox("Record has not been added")
                    'End If

                Else
                    If IsValidToDelete("SalesMasterTable", "DeliveryChalanId", Me.grdSaved.CurrentRow.Cells("DeliveryId").Value.ToString) = True Then
                        If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                        Me.lblProgress.Text = "Processing Please Wait ..."
                        Me.lblProgress.Visible = True
                        If Update_Record() Then

                            RefreshControls()
                            ClearDetailControls()

                            If BackgroundWorker2.IsBusy Then Exit Sub
                            BackgroundWorker2.RunWorkerAsync()


                            If BackgroundWorker3.IsBusy Then Exit Sub
                            BackgroundWorker3.RunWorkerAsync()
                            'Do While BackgroundWorker3.IsBusy
                            '    Application.DoEvents()
                            'Loop

                        Else
                            Exit Sub 'MsgBox("Record has not been updated")
                        End If
                    Else
                        msg_Error(str_ErrorDependentUpdateRecordFound)
                        Exit Sub
                    End If

                End If

                ' NOTIFICATION STARTS HERE - ADDED BY MOHSIN 14-9-2017 '

                ' *** New Segment *** 
                '// Adding notification

                '// Creating new object of Notification configuration dal
                '// Dal will be used to get users list for the notification 
                Dim NDal As New NotificationConfigurationDAL
                Dim objmod As New VouchersMaster
                '// Creating new object of Agrius Notification class
                objmod.Notification = New AgriusNotifications

                '// Reference document number
                objmod.Notification.ApplicationReference = Me.txtPONo.Text

                '// Date of notification
                objmod.Notification.NotificationDate = Now

                '// Preparing notification title string
                objmod.Notification.NotificationTitle = "New Delivery Chalan number [" & Me.txtPONo.Text & "]  is saved with " & Me.txtTotalQuantity.Text & " Total Quantity."

                '// Preparing notification description string
                objmod.Notification.NotificationDescription = "New Delivery Chalan number [" & Me.txtPONo.Text & "] is saved by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                '// Setting source application as refrence in the notification
                objmod.Notification.SourceApplication = "Delivery Chalan"



                '// Starting to get users list to add child

                '// Creating notification detail object list
                Dim List As New List(Of NotificationDetail)

                '// Getting users list
                List = NDal.GetNotificationUsers("Delivery Chalan Created")

                '// Adding users list in the Notification object of current inquiry
                objmod.Notification.NotificationDetils.AddRange(List)

                '// Getting and adding user groups list in the Notification object of current inquiry
                objmod.Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("Delivery Chalan"))

                '// Not getting role list because no role is associated at the moment
                '// We will need this in future and we can use it later
                '// We can consult to Update function of this class


                '// ***This segment will be used to save notification in database table

                '// Creating new list of objects of Agrius Notification
                Dim NList As New List(Of AgriusNotifications)

                '// Copying notification object from current sales inquiry to newly defined instance
                '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
                NList.Add(objmod.Notification)

                '// Creating object of Notification DAL
                Dim GNotification As New NotificationDAL

                '// Saving notification to database
                GNotification.AddNotification(NList)

                '// End Adding Notification

                '// End Adding Notification
                ' *** End Segment ***

            End If



        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grd.RecordCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.cmbSalesMan.SelectedIndex = 0
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

            '  Add notification on Delivery Chalan Modification by Mohsin on 14-Sep-2017

            ' *** New Segment *** 
            '// Adding notification

            '// Creating new object of Notification configuration dal
            '// Dal will be used to get users list for the notification 
            Dim NDal As New NotificationConfigurationDAL
            Dim objmod As New VouchersMaster
            '// Creating new object of Agrius Notification class
            objmod.Notification = New AgriusNotifications

            '// Reference document number
            objmod.Notification.ApplicationReference = Me.txtPONo.Text

            '// Date of notification
            objmod.Notification.NotificationDate = Now

            '// Preparing notification title string
            objmod.Notification.NotificationTitle = "Delivery Chalan number [" & Me.txtPONo.Text & "]  is modified."

            '// Preparing notification description string
            objmod.Notification.NotificationDescription = "Delivery Chalan number [" & Me.txtPONo.Text & "] is modified by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

            '// Setting source application as refrence in the notification
            objmod.Notification.SourceApplication = "Delivery Chalan"



            '// Starting to get users list to add child

            '// Creating notification detail object list
            Dim List As New List(Of NotificationDetail)

            '// Getting users list
            List = NDal.GetNotificationUsers("Delivery Chalan changed")


            '// Adding users list in the Notification object of current inquiry
            objmod.Notification.NotificationDetils.AddRange(List)

            '// Getting and adding user groups list in the Notification object of current inquiry
            objmod.Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("Delivery Chalan changed"))

            '// Not getting role list because no role is associated at the moment
            '// We will need this in future and we can use it later
            '// We can consult to Update function of this class


            '// ***This segment will be used to save notification in database table

            '// Creating new list of objects of Agrius Notification
            Dim NList As New List(Of AgriusNotifications)

            '// Copying notification object from current sales inquiry to newly defined instance
            '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
            NList.Add(objmod.Notification)

            '// Creating object of Notification DAL
            Dim GNotification As New NotificationDAL

            '// Saving notification to database
            GNotification.AddNotification(NList)

            '// End Adding Notification

            '// End Adding Notification
            ' *** End Segment ***



        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
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

    Private Sub grdSaved_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try

            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Me.grdSaved.GetRow.Cells("DeliveryID").Value.ToString
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub grdSaved_CellDoubleClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdSaved.RowDoubleClick
        Try
            ''Task# A2-10-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
            If Me.grdSaved.Row < 0 Then
                Exit Sub
            Else
                EditRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        ''End Task# A2-10-06-2015
    End Sub
    Private Sub cmbPo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPo.SelectedIndexChanged
        If Not flgMultipleSalesOrder = True Then
            If Me.cmbPo.SelectedIndex <= 0 Then Exit Sub

            If Me.BtnSave.Text <> "&Save" Then
                If flgMultipleSalesOrder = False Then
                    If Val(Me.grdSaved.GetRow.Cells("POId").Value.ToString) > 0 Then
                        If Me.cmbPo.SelectedIndex > 0 Then
                            If Me.cmbPo.SelectedValue <> (Me.grdSaved.GetRow.Cells("POId").Value.ToString) Then
                                If msg_Confirm("You have changed Sales Order [" & CType(Me.cmbPo.SelectedItem, DataRowView).Row.Item("SalesOrderNo").ToString & "], do you want to proceed. ?") = False Then
                                    RemoveHandler cmbPo.SelectedIndexChanged, AddressOf Me.cmbPo_SelectedIndexChanged
                                    Me.cmbPo.SelectedValue = (Me.grdSaved.GetRow.Cells("POId").Value.ToString)
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
            'rafay
            Me.txtPO.Text = CType(Me.cmbPo.SelectedItem, DataRowView).Row.Item("PONo").ToString
            'rafay
            Me.txtRemarks.Text = CType(Me.cmbPo.SelectedItem, DataRowView).Row.Item("Remarks").ToString
            ' Me.txtRemarks.Text = GetDeliveryRemarks(Me.cmbPo.SelectedValue)
            ''TASK TFS1764
            Me.cmbTransporter.SelectedValue = CType(cmbPo.SelectedItem, DataRowView).Item("TransporterId")
            ''END TASK TFS1764
            Me.cmbSalesMan.SelectedValue = Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("SOP_ID").ToString)

            Me.DisplayPODetail(Me.cmbPo.SelectedValue)
            'If Me.cmbPo.SelectedIndex > 0 Then
            '    Dim adp As New OleDbDataAdapter
            '    Dim dt As New DataTable
            '    Dim Sql As String = "Select * from SalesOrderMasterTable where SalesOrderID=" & Me.cmbPo.SelectedValue
            '    adp = New OleDbDataAdapter(Sql, Con)
            '    adp.Fill(dt)
            '    'TODO -----
            '    Me.cmbVendor.Value = dt.Rows(0).Item("VendorId")
            '    Me.cmbVendor.Enabled = False

            'Else
            '    Me.cmbVendor.Enabled = True
            '    If Me.cmbVendor.Rows.Count > 0 Then Me.cmbVendor.Rows(0).Activate()
            'End If

            Me.GetTotal()
            GetAdjustment(Me.cmbPo.SelectedValue)
        Else
            Exit Sub
        End If

    End Sub

    Private Sub cmbItem_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbItem.KeyDown
        Try
            ''TFS1858 : Ayesha Rehman :Item dropdown shall be searchable
            If e.KeyCode = Keys.F1 Then

                If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                    If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                        frmItemSearch.LocationId = Me.cmbCategory.SelectedValue
                    Else
                        frmItemSearch.LocationId = 0
                    End If
                End If
                If ModelId > 0 Then
                    'If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                    frmItemSearch.ModelId = ModelId

                    'End If
                End If
                If Me.rbtCustomer.Checked = True Then
                    If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
                    frmItemSearch.VendorId = Me.cmbVendor.Value
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

    Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        Try
            Me.cmbBatchNo.LimitToList = True
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            'Me.txtStock.Text = UiGetStockDataTable(Me.cmbItem.Value)
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            Me.txtPDP.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString ''TFS2827
            If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
            'Else
            'Me.txtQty.Text = 0
            'If Val(Me.txtServiceQty.Text) <= 0 Then Me.txtServiceQty.Text = 1
            'End If
            Dim strSQl As String = String.Empty

            Dim objCommand As New OleDbCommand
            Dim objDataAdapter As New OleDbDataAdapter
            Dim objDataSet As New DataSet

            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            'objCommand.CommandText = strSQl


            Dim dt As New DataTable
            'Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            'objDataAdapter.Fill(dt)

            'dr = dt.NewRow
            'dr.Item(1) = "xxxx"
            'dr.Item(0) = 0
            'dr.Item(2) = 0
            'dt.Rows.Add(dr)

            'cmbBatchNo.DisplayMember = "Batch No"
            'cmbBatchNo.ValueMember = "BatchID"
            'cmbBatchNo.DataSource = dt

            'cmbBatchNo.DisplayLayout.Bands(0).Columns("BatchId").Hidden = True

            Con.Close()

            'Dim i As Integer
            'For i = 0 To Me.cmbBatchNo.Rows.Count - 2
            '    Dim Gi As Integer
            '    For Gi = 0 To Me.grd.GetRows.Length - 1
            '        If Me.cmbBatchNo.Rows(i).Cells("Batch No").Value = Me.grd.GetRow(Gi).Cells(EnumGridDetail.BatchNo).Value AndAlso Me.grd.GetRow(Gi).Cells(EnumGridDetail.DeliveryDetailID).Value.ToString = "0" Then
            '            Me.cmbBatchNo.Rows(i).Cells("Stock").Value = Me.cmbBatchNo.Rows(i).Cells("Stock").Value - Me.grd.GetRow(Gi).Cells(EnumGridDetail.Qty).Value
            '        End If
            '    Next
            '    'me.cmbBatchNo.Rows(i).Cells("BatchNo)
            'Next
            'Me.cmbBatchNo.Rows(0).Activate()


            objCommand.CommandText = " SELECT   Price" _
                                 & " FROM DeliveryChalanDetailTable" _
                                 & " WHERE     DeliveryDetailId =" _
                                 & " (SELECT     MAX(DeliveryDetailID) " _
                                 & "   FROM DeliveryChalanDetailTable, DeliveryChalanMasterTable " _
                                 & " WHERE(DeliveryChalanDetailTable.Deliveryid = DeliveryChalanMasterTable.DeliveryId) " _
                                 & " and DeliveryChalanMasterTable.CustomerCode=" & Val(Me.cmbVendor.ActiveRow.Cells(0).Value) & " and ArticleDefID =" & Me.cmbItem.ActiveRow.Cells(0).Value _
                                 & " )"

            objDataAdapter.Fill(objDataSet)


            If objDataSet.Tables(0).Rows.Count > 0 Then
                Me.txtLastPrice.Text = objDataSet.Tables(0).Rows(0)(0)
            Else
                Me.txtLastPrice.Text = 0
            End If

            ''calculate customer base discont
            Me.txtDisc.TabStop = True
            Try
                If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then

                    If getConfigValueByType("ApplyFlatDiscountOnSale").ToString = "False" Then
                        strSQl = "select discount from tbldefcustomerbasediscounts where articledefid = " & Me.cmbItem.ActiveRow.Cells(0).Value _
                        & " and typeid = " & Me.cmbVendor.ActiveRow.Cells("typeid").Value & "  and discount > 0 "

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
                                If IsDeliveryOrderAnalysis = True Then
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
                                        Me.txtRate.Text = price - ((price / 100) * dblDiscountPercent)
                                        Me.txtPDP.Text = price - ((price / 100) * dblDiscountPercent) ''TFS2827
                                        Me.txtDisc.TabStop = False
                                    Else
                                        Me.txtRate.Text = Me.txtRate.Text
                                        Me.txtPDP.Text = Me.txtPDP.Text
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

            End Try


            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing


        Catch ex As Exception
            '  cmbItem.Focus()
            ShowErrorMessage(ex.Message)

        End Try

        'If Not Me.cmbBatchNo.Rows.Count > 0 Then Me.cmbBatchNo.LimitToList = False
        'Me.txtStock.Text = GetItemStock(Me.cmbItem.ActiveRow.Cells(0).Value.ToString)
        'Me.cmbVendor.DisplayLayout.Grid.Show( me.cmbVendor.contr)
        'Me.cmbBatchNo.Enabled = True
    End Sub
    Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Enter
        Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub

    Private Sub cmbVendor_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
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


        ''Start TFS3113 : Ayesha Rehman : 09-04-2018
        If IsEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document is in Approval Process ") : Exit Sub
                End If
            End If
        End If
        ''End TFS3113


        If BtnDelete.Enabled = False Then Exit Sub
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        If IsDateLock(Me.dtpPODate.Value) = True Then
            ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
        End If
        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If IsValidToDelete("SalesMasterTable", "DeliveryChalanId", Me.grdSaved.CurrentRow.Cells("DeliveryId").Value.ToString) = False Then msg_Error(str_ErrorDependentRecordFound) : Exit Sub




        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            'Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, grdSaved.CurrentRow.Cells(0).Value.ToString)

            'Dim strVoucherNo As String = String.Empty
            'Dim dt As DataTable = GetRecords("SELECT voucher_no   FROM tblVoucher  WHERE voucher_id = " & lngVoucherMasterId & " ")

            'If Not dt Is Nothing Then
            '    If Not dt.Rows.Count = 0 Then
            '        strVoucherNo = dt.Rows(0)("voucher_no")
            '    End If
            'End If
            Dim cmd As New OleDbCommand
            Dim objTrans As OleDbTransaction
            If Con.State = ConnectionState.Closed Then Con.Open()

            objTrans = Con.BeginTransaction     'Task#04082015 Error Fix: Move objTrans object upward above the cmd object (Ahmad Sharif)

            'Dim cmd As New OleDbCommand
            cmd.Connection = Con
            cmd.Transaction = objTrans          'Task#04082015 Error Fix: cmd doesn't initialized with transaction, so exception thrown on deletion of delivery challan (Ahmad Sharif)
            cmd.CommandType = CommandType.Text

            cmd.CommandText = String.Empty
            cmd.CommandText = "SELECT  ISNULL(Sz1,0) as Qty, ISNULL(SampleQty,0) as SampleQty, IsNull(ArticleDefID, 0) As ArticleDefID, ISNULL(SO_ID,0) AS SO_ID,IsNull(Qty,0) as TotalQty, IsNull(SO_Detail_ID, 0) As SO_Detail_ID FROM DeliveryChalanDetailTable WHERE  DeliveryId = " & Me.grdSaved.CurrentRow.Cells("DeliveryId").Value & ""
            Dim da As New OleDbDataAdapter(cmd)
            Dim dtSavedItems As New DataTable
            da.Fill(dtSavedItems)
            dtSavedItems.AcceptChanges()
            ''TFS3520 : Ayesha Rehman :  14-06-2018
            ''Updating DC qty ,DC Status and Delivered Qty , Status in SalesOrderMaster Table on the basis of SOSeparateClosure configuration
            If flgSOSeparateClosure = True Then
                If dtSavedItems.Rows.Count > 0 Then
                    If flgMultipleSalesOrder = False Then
                        For Each r As DataRow In dtSavedItems.Rows
                            cmd.CommandText = String.Empty
                            cmd.CommandText = "Update SalesOrderDetailTable set DCQty = abs(Isnull(DCQty,0) - " & r.Item(0) & "), DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) - " & r.Item(1) & "),DCTotalQty= abs(IsNull(DCTotalQty,0) - " & r.Item(4) & ") where SalesOrderID = " & Me.grdSaved.CurrentRow.Cells("PoId").Value & " and ArticleDefID = " & r.Item(2) & " And SalesOrderDetailId = " & r.Item(5) & " "    'Task#29082015
                            cmd.ExecuteNonQuery()
                        Next
                    Else
                        For Each r As DataRow In dtSavedItems.Rows
                            cmd.CommandText = String.Empty
                            cmd.CommandText = "Update SalesOrderDetailTable set DCQty = abs(Isnull(DCQty,0) - " & r.Item(0) & "), DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) - " & r.Item(1) & "),DCTotalQty= abs(IsNull(DCTotalQty,0) - " & r.Item(4) & ") where SalesOrderID = " & Val(r.Item("SO_ID")) & " and ArticleDefID = " & r.Item(2) & " And SalesOrderDetailId = " & r.Item(5) & " "      'Task#29082015
                            cmd.ExecuteNonQuery()
                        Next
                    End If
                End If


                If flgMultipleSalesOrder = False Then
                    cmd.CommandText = String.Empty
                    cmd.CommandText = "Update SalesOrderMasterTable set DCStatus = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Val(Me.grdSaved.CurrentRow.Cells("PoId").Value.ToString) & ""
                    cmd.ExecuteNonQuery()
                    '  End If
                Else
                    cmd.CommandText = String.Empty
                    cmd.CommandText = "Select distinct isnull(so_id,0) as so_id From DeliveryChalanDetailTable where DeliveryId=" & Val(Me.grdSaved.CurrentRow.Cells("DeliveryId").Value.ToString)
                    Dim dt As New DataTable
                    Dim da1 As New OleDbDataAdapter(cmd)
                    da1.Fill(dt)
                    dt.AcceptChanges()
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then
                            For Each r As DataRow In dt.Rows
                                cmd.CommandText = String.Empty
                                cmd.CommandText = "Update SalesOrderMasterTable set DCStatus = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Val(r.Item("SO_ID").ToString) & ""
                                cmd.ExecuteNonQuery()
                            Next
                        End If
                    End If
                End If
            Else
                If dtSavedItems.Rows.Count > 0 Then
                    If flgMultipleSalesOrder = False Then
                        For Each r As DataRow In dtSavedItems.Rows
                            cmd.CommandText = String.Empty
                            cmd.CommandText = "Update SalesOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & "), DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) - " & r.Item(1) & "),DeliveredTotalQty= abs(IsNull(DeliveredTotalQty,0) - " & r.Item(4) & ") where SalesOrderID = " & Me.grdSaved.CurrentRow.Cells("PoId").Value & " and ArticleDefID = " & r.Item(2) & " And SalesOrderDetailId = " & r.Item(5) & " "    'Task#29082015
                            cmd.ExecuteNonQuery()
                        Next
                    Else
                        For Each r As DataRow In dtSavedItems.Rows
                            cmd.CommandText = String.Empty
                            cmd.CommandText = "Update SalesOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & "), DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) - " & r.Item(1) & "),DeliveredTotalQty= abs(IsNull(DeliveredTotalQty,0) - " & r.Item(4) & ") where SalesOrderID = " & Val(r.Item("SO_ID")) & " and ArticleDefID = " & r.Item(2) & " And SalesOrderDetailId = " & r.Item(5) & " "      'Task#29082015
                            cmd.ExecuteNonQuery()
                        Next
                    End If
                End If


                If flgMultipleSalesOrder = False Then
                    cmd.CommandText = String.Empty
                    cmd.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Val(Me.grdSaved.CurrentRow.Cells("PoId").Value.ToString) & ""
                    cmd.ExecuteNonQuery()
                    '  End If
                Else
                    cmd.CommandText = String.Empty
                    cmd.CommandText = "Select distinct isnull(so_id,0) as so_id From DeliveryChalanDetailTable where DeliveryId=" & Val(Me.grdSaved.CurrentRow.Cells("DeliveryId").Value.ToString)
                    Dim dt As New DataTable
                    Dim da1 As New OleDbDataAdapter(cmd)
                    da1.Fill(dt)
                    dt.AcceptChanges()
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then
                            For Each r As DataRow In dt.Rows
                                cmd.CommandText = String.Empty
                                cmd.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Val(r.Item("SO_ID").ToString) & ""
                                cmd.ExecuteNonQuery()
                            Next
                        End If
                    End If
                End If
            End If



            'objTrans = Con.BeginTransaction
            'cmd.Connection = Con
            cmd.CommandText = String.Empty
            cmd.CommandText = "delete from DeliveryChalanDetailTable where Deliveryid=" & Me.grdSaved.CurrentRow.Cells("DeliveryId").Value.ToString
            'cmd.Transaction = objTrans
            cmd.ExecuteNonQuery()

            'cmd = New OleDbCommand
            'cmd.Connection = Con
            cmd.CommandText = String.Empty
            cmd.CommandText = "delete from DeliveryChalanMasterTable where Deliveryid=" & Me.grdSaved.CurrentRow.Cells("DeliveryId").Value.ToString

            'cm.Transaction = objTrans
            cmd.ExecuteNonQuery()

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

            'cm = New OleDbCommand
            'cm.Connection = Con
            'cm.Transaction = objTrans

            ''make reversal of saved items in Delivery order detail table against poid

            '' Add by Mohsin on 14 Sep 2017

            ' NOTIFICATION STARTS HERE FOR UPDATE - ADDED BY MOHSIN 14-9-2017 '

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
            objmod1.Notification.NotificationTitle = "Delivery chalan number [" & Me.txtPONo.Text & "]  is deleted."

            '// Preparing notification description string
            objmod1.Notification.NotificationDescription = "Delivery chalan number [" & Me.txtPONo.Text & "] is deleted by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

            '// Setting source application as refrence in the notification
            objmod1.Notification.SourceApplication = "Delivery Chalan"



            '// Starting to get users list to add child

            '// Creating notification detail object list
            Dim List1 As New List(Of NotificationDetail)

            '// Getting users list
            List1 = NDal1.GetNotificationUsers("Delivery Chalan Deleted")

            '// Adding users list in the Notification object of current inquiry
            objmod1.Notification.NotificationDetils.AddRange(List1)

            '// Getting and adding user groups list in the Notification object of current inquiry
            objmod1.Notification.NotificationDetils.AddRange(NDal1.GetNotificationGroups("Delivery Chalan"))

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
            ''Start TFS3466 : Ayesha Rehman : 06-06-2018 : Delivery challan stock impact delete issue
            StockMaster = New StockMaster
            StockMaster.StockTransId = Convert.ToInt32(StockTransId(grdSaved.CurrentRow.Cells(0).Value, objTrans))
            Call New StockDAL().Delete(StockMaster, objTrans)
            ''End TFS3466 

            objTrans.Commit()
            'Call Delete() 'Upgrading Stock here ...

            SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Sales, Me.grdSaved.CurrentRow.Cells(0).Value, True)
            SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, Me.grdSaved.CurrentRow.Cells(0).Value, True)




        Catch ex As Exception
            msg_Error("Error occured while deleting record: " & ex.Message)
        Finally
            Me.lblProgress.Visible = False

            Con.Close()
            Me.Cursor = Cursors.Default
        End Try

        Voucher_Delete()
        msg_Information(str_informDelete)
        Me.txtReceivingID.Text = 0
        Me.RefreshControls()
        Me.DisplayRecord()

    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click

        'ShowReport("DeliveryInvoice", "{DeliveryChalanMasterTable.DeliveryId}=" & grdSaved.CurrentRow.Cells("DeliveryId").Value, "Nothing", "Nothing", True)
    End Sub

    Private Sub grd_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        Me.GetTotal()
    End Sub


    Private Sub RdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdoCode.CheckedChanged
        If Not Me.IsFormOpend = True Then Exit Sub 'Me.FillCombo("Item")
        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
    End Sub

    Private Sub rdoName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoName.CheckedChanged
        'If Me.IsFormOpend = True Then Me.FillCombo("Item")
        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim IsPreviewDeliveryInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
        Dim newinvoice As Boolean = False
        Dim strCriteria As String = "Nothing"
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("DeliveryNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            newinvoice = getConfigValueByType("NewInvoice")
            If newinvoice = True Then
                str_ReportParam = "@DeliveryID|" & grdSaved.CurrentRow.Cells("DeliveryId").Value
            Else
                str_ReportParam = String.Empty
                strCriteria = "{DeliveryChalanDetailTable.DeliveryId} = " & grdSaved.CurrentRow.Cells("DeliveryId").Value
            End If
            If IsPreviewDeliveryInvoice = False Then
                ShowReport(IIf(newinvoice = False, "DeliveryInvoice", "DeliveryInvoiceNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", True, , "New", , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            Else
                ShowReport(IIf(newinvoice = False, "DeliveryInvoice", "DeliveryInvoiceNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", False, , "New", , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub PrintGatePToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim IsPreviewDeliveryInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
        Dim newinvoice As Boolean = False
        Dim strCriteria As String = "Nothing"
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = grdSaved.GetRow.Cells("PurchaseReturnNo").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            newinvoice = getConfigValueByType("NewInvoice")
            If newinvoice = True Then
                str_ReportParam = "@DeliveryID|" & grdSaved.CurrentRow.Cells("DeliveryId").Value
            Else
                str_ReportParam = String.Empty
                strCriteria = "{DeliveryChalanDetailTable.DeliveryId} = " & grdSaved.CurrentRow.Cells("DeliveryId").Value
            End If
            If IsPreviewDeliveryInvoice = False Then
                ShowReport(IIf(newinvoice = False, "GatePass", "GatePassNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", True, , "New", , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            Else
                ShowReport(IIf(newinvoice = False, "GatePass", "GatePassNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", False, , "New", , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub PrintListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintListToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim frmPrintList As New frmPrintInvoice
            ApplyStyleSheet(frmPrintList)
            frmPrintList.ShowDialog()
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.btnSearchDelete.Enabled = True ''R934 Added Dlete Button
                Me.btnSearchPrint.Enabled = True ''R934 Added Print Button
                Me.chkPost.Visible = True
                blnTradePriceExceed = False
                If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True
                Me.chkDelivered.Visible = True
                If Me.BtnSave.Text = "&Save" Then Me.chkDelivered.Checked = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.btnSearchDelete.Enabled = False
                    Me.BtnPrint.Enabled = False
                    Me.PrintListToolStripMenuItem.Enabled = False
                    PrintListToolStripMenuItem.Enabled = False
                    Me.btnSearchDelete.Enabled = False ''R934 Added Dlete Button
                    Me.btnSearchPrint.Enabled = False  ''R934 Added Print Button
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmSales)
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
                        Me.PrintListToolStripMenuItem.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        PrintListToolStripMenuItem.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString ''R934 Added Dlete Button
                        Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString ''R934 Added Print Button
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                'UserPostingRights = GetUserPostingRights(LoginUserId)
                'If UserPostingRights = True Then
                '    Me.chkPost.Visible = True
                '    Me.chkDelivered.Visible = True
                'Else
                '    Me.chkPost.Visible = False
                '    If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = False
                '    Me.chkDelivered.Visible = False
                'End If
                'UserPriceAllowedRights = GetUserPriceAllowedRights(LoginUserId)
                'If UserPriceAllowedRights = True Then
                '    Me.Panel2.Visible = True
                '    Me.grd.RootTable.Columns(EnumGridDetail.Price).Visible = True
                '    Me.grd.RootTable.Columns(EnumGridDetail.Total).Visible = True
                '    Me.chkDelivered.Visible = True
                'Else
                '    Me.Panel2.Visible = False
                '    Me.grd.RootTable.Columns(EnumGridDetail.Price).Visible = False
                '    Me.grd.RootTable.Columns(EnumGridDetail.Total).Visible = False
                '    Me.chkDelivered.Visible = False
                'End If
                'GetSecurityByPostingUser(UserPostingRights, BtnSave, BtnDelete)
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.btnSearchDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                Me.chkPost.Visible = False
                Me.chkDelivered.Visible = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                Me.btnSearchDelete.Enabled = False ''R934 Added Dlete Button
                Me.btnSearchPrint.Enabled = False  ''R934 Added Print Button
                CtrlGrdBar1.mGridChooseFielder.Enabled = False 'Task:2406 Added Field Chooser Rights
                blnTradePriceExceed = False
                If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = False
                If Me.BtnSave.Text = "&Save" Then Me.chkDelivered.Checked = False
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
                        Me.btnSearchPrint.Enabled = True  ''R934 Added Print Button
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True
                        If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True
                    ElseIf RightsDt.FormControlName = "Price Allow" Then
                        Me.Panel2.Visible = True
                        Me.grd.RootTable.Columns("Rate").Visible = True
                        Me.grd.RootTable.Columns("Total").Visible = True
                    ElseIf RightsDt.FormControlName = "Delivered" Then
                        Me.chkDelivered.Visible = True
                        If Me.BtnSave.Text = "&Save" Then Me.chkDelivered.Checked = True
                    ElseIf RightsDt.FormControlName = "Trade Price Exceed" Then
                        blnTradePriceExceed = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub





    Private Sub cmbBatchNo_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBatchNo.Enter
        Me.cmbBatchNo.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.Dropdown)
    End Sub
    Private Sub cmbBatchNo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBatchNo.Leave
        Try
            Me.txtStock.Text = Me.cmbBatchNo.ActiveRow.Cells("Stock").Value.ToString
        Catch ex As Exception
        End Try
    End Sub
    Private Sub cmbBatchNo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBatchNo.ValueChanged
        If Not Me.cmbBatchNo.ActiveRow Is Nothing Then
            Me.txtStock.Text = Me.cmbBatchNo.ActiveRow.Cells("Stock").Value
        End If
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
    'Task:2417 Added Event Item Filter By Customer
    Private Sub cmbVendor_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.LostFocus
        Try
            If IsFormOpend = True Then
                If Me.rbtCustomer.Checked = True Then
                    FillCombo("Item")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2417
    Private Sub cmbVendor_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.ValueChanged
        Try

            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            _Previous_Balance = GetCurrentBalance(Me.cmbVendor.Value)
            Dim Str As String
            'If Me.IsEditMode = False Then
            '    Str = "Select SalesOrderID, SalesOrderNo from SalesOrderMasterTable where  status = N'" & EnumStatus.Open.ToString & "' and VendorID = " & Me.cmbVendor.ActiveRow.Cells(0).Value & " and Status = N'" & EnumStatus.Open.ToString & "'"
            '    FillDropDown(cmbPo, Str)
            '    'Me.txtFuel.Text = Val(Me.cmbVendor.ActiveRow.Cells(Customer.Fuel).Text)
            '    'Me.txtExpense.Text = Val(Me.cmbVendor.ActiveRow.Cells(Customer.Other_Exp).Text)
            'Else
            '    Str = "Select SalesOrderID, SalesOrderNo from SalesOrderMasterTable where VendorID = " & Me.cmbVendor.ActiveRow.Cells(0).Value & ""
            '    FillDropDown(cmbPo, Str)
            'End If
            FillCombo("SO")
            If Me.rbtCustomer.Checked = True Then
                If IsFormOpend = True Then FillCombo("Item")
            End If
            'If IsFormOpend = True Then
            'If Not GetConfigValue("ServiceItem").ToString = "True" Then
            If flgLoadAllItems = True Then
                Str = "Select CustomerTypes From tblCustomer WHERE AccountId=" & Me.cmbVendor.Value
                Dim dt As DataTable = GetDataTable(Str)
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.DisplayDetail(-1, dt.Rows(0).Item(0), "LoadAll")
                    Else
                        Me.DisplayDetail(-1, -1, "LoadAll")
                    End If
                End If
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    If Me.grd.RowCount > 0 Then
                        r.BeginEdit()
                        r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
                        r.EndEdit()
                    End If
                Next
            End If
            'End If
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar2_Load(Nothing, Nothing)
            CtrlGrdBar2_Load_1(Nothing, Nothing)
            CtrlGrdBar1.Email = New SBModel.SendingEmail
            CtrlGrdBar1.Email.ToEmail = Me.cmbVendor.ActiveRow.Cells("Email").Text
            CtrlGrdBar1.Email.Subject = "Delivery: " + "(" & Me.txtPONo.Text & ")"
            CtrlGrdBar1.Email.DocumentNo = Me.txtPONo.Text
            CtrlGrdBar1.Email.DocumentDate = Me.dtpPODate.Value
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPackQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged
        Try

            'If Me.txtPackRate.Text.Length > 0 AndAlso Val(Me.txtPackRate.Text) > 0 Then
            'If Me.cmbUnit.Text <> "Loose" Then
            '    Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))

            'Else

            '    Me.txtRate.Text = Val(Me.txtRate.Text)

            'End If
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
    Private Sub cmbCompany_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCompany.SelectedIndexChanged
        If Not IsEditMode = True AndAlso IsFormOpend = True Then
            Me.RefreshControls()
            Me.DisplayRecord()
        End If
        'companyId = Me.cmbCompany.SelectedValue 'Comment againstt Task:M28
        If Not Me.cmbCompany.SelectedIndex = -1 Then companyId = Me.cmbCompany.SelectedValue 'Task:M28 #TODO Nothing Status
    End Sub
    Private Sub txtBarcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBarcode.KeyDown, cmbCodes.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.ErrorProvider1.Clear()
                lblError.Text = String.Empty
                If Me.cmbCodes.Focused Then
                    If Me.cmbCodes.ActiveRow.Index = 0 Then Exit Sub
                    Me.txtBarcode.Text = Me.cmbCodes.ActiveRow.Cells("Code").Value
                    Me.cmbCodes.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.CloseDropdown)
                End If

                If Me.cmbItem.IsItemInList(Me.txtBarcode.Text) Then
                    'Me.cmbCategory.SelectedValue = objDataSet.Tables(0).Rows(i)("ArticleGroupId")
                    'Me.cmbCategory_SelectedIndexChanged(Nothing, Nothing)

                    ''selecting the item
                    ' Me.cmbItem.Value = Me.txtBarcode.Text
                    Me.cmbItem.Text = Me.txtBarcode.Text
                    'me.cmbItem.ActiveRow=me.cmbItem.Rows(me.cmbItem.S               'Me.cmbItem.Text = Me.txtBarcode.Text
                    'Me.cmbItem.Select()

                    'Me.cmbItem.SelectedRow=me
                    Me.cmbItem_Leave(Nothing, Nothing)

                    ''selecting batch no
                    ' Me.cmbBatchNo.Text = objDataSet.Tables(0).Rows(i)("BatchNo")
                    Me.cmbBatchNo_Leave(Nothing, Nothing)

                    ''selecting unit
                    Me.cmbUnit.SelectedIndex = 0
                    Me.cmbUnit_SelectedIndexChanged(Nothing, Nothing)

                    ''selecting qty
                    Me.txtQty.Text = 1
                    txtPackQty.Text = "1"
                    Me.txtQty_LostFocus(Nothing, Nothing)
                    'Me.txtQty_TextChanged(Nothing, Nothing)

                    ''selecing rate
                    'Me.txtRate.Text = objDataSet.Tables(0).Rows(i)(4)

                    Me.txtTotal.Text = Math.Round(Val(Me.txtRate.Text) * Val(Me.txtQty.Text), TotalAmountRounding)

                    Me.btnAdd_Click(Nothing, Nothing)


                    'lblError.Text = "Code Found"
                    Me.txtBarcode.Text = String.Empty
                    Me.txtBarcode.Focus()

                Else
                    Me.txtBarcode.Focus()
                    Me.txtBarcode.SelectAll()
                    lblError.Text = "Invalid code"
                    Me.ErrorProvider1.SetError(Me.txtBarcode, "Invalid Code")
                    'Spech.SelectVoice("Error")
                    'Spech.SpeakAsync("Try again")

                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
        End Try
    End Sub

    Private Sub cmbCodes_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCodes.Leave
    End Sub

    Private Sub cmbCodes_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCodes.Enter
        Me.cmbCodes.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        SaveToolStripButton_Click(Me, Nothing)
    End Sub

    Private Sub RefreshItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshItemsToolStripMenuItem.Click
        Try
            FillCombo("Item")
            FillCombo("Barcodes")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        str_ReportParam = "@DeliveryID|" & InvId
        Dim newinvoice As Boolean = False
        Try
            newinvoice = getConfigValueByType("NewInvoice")
        Catch ex As Exception
            newinvoice = False
        End Try
        ShowReport(IIf(newinvoice = False, "DeliveryInvoice", "DeliveryInvoiceNew") & grdSaved.CurrentRow.Cells("LocationId").Value, "Nothing", "Nothing", "Nothing", True, , "New", , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
    End Sub


    Private Sub SummaryOfDeliveryInvoicesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmMain.LoadControl("rptSumOfInv")
    End Sub

    Private Sub SummaryOfDeliveryInvoicesToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryOfSalesInvoicesToolStripMenuItem2.Click
        frmMain.LoadControl("rptSumOfInv")
    End Sub

    Private Sub ReciveablesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReciveablesToolStripMenuItem1.Click
        ShowReport("AgeingReceivable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)
    End Sub

    Private Sub CatageoryWiseDeliveryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CatageoryWiseSaleToolStripMenuItem1.Click
        frmMain.LoadControl("rptCategoryWiseDeliveryReport")
    End Sub

    Private Sub AreaWiseDeliveryReportToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AreaWiseSaleReportToolStripMenuItem1.Click
        frmMain.LoadControl("rptAreaProdDelivery")
    End Sub

    Private Sub ItemWiseDeliveryReToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmMain.LoadControl("ItemWiseDeliveryrpt")
    End Sub

    Private Sub ItemWiseDeliveryReToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub ItemWiseDeliveryReToolStripMenuItem_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemWiseSaleReToolStripMenuItem.Click
        frmMain.LoadControl("itemWiseRpt")
    End Sub
    Private Sub grd_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub
    Private Sub ToolStripButton2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        '    frmMain.LoadControl("AddCustomer")
        Dim CustId As Integer = 0
        CustId = Me.cmbVendor.Value
        ''Task 3461: Aashir: Edited Becuase Shortcut link account add option was not working 
        'FrmAddCustomers.FormType = "Customer"
        'FrmAddCustomers.ShowDialog()
        frmMain.LoadControl("AddCustomer")

        Me.FillCombo("Vendor")
        Me.cmbVendor.Value = CustId
    End Sub

    Private Sub CustomerTypeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerTypeToolStripMenuItem.Click
        frmMain.LoadControl("CustomerType")
    End Sub

    Private Sub PrepareGrid()
        Try
            Me.DisplayDetail(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub ApplyGridSettings(Optional ByVal condition As String = "") Implements IGeneral.ApplyGridSettings
    '    Try
    '        If getConfigValueByType("WeighbridgeDC").ToString.ToUpper = "TRUE" Then

    '            Me.grd.RootTable.Columns(EnumGridDetail.Gross_Weights).Visible = True
    '            Me.grd.RootTable.Columns(EnumGridDetail.Tray_Weights).Visible = True
    '            Me.grd.RootTable.Columns(EnumGridDetail.Net_Weight).Visible = True

    '        Else

    '            Me.grd.RootTable.Columns(EnumGridDetail.Gross_Weights).Visible = False
    '            Me.grd.RootTable.Columns(EnumGridDetail.Tray_Weights).Visible = False
    '            Me.grd.RootTable.Columns(EnumGridDetail.Net_Weight).Visible = False

    '        End If

    '        Me.grd.AutomaticSort = False
    '        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
    '        Me.grd.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
    '        Me.grd.RootTable.Columns(EnumGridDetail.ArticleID).Visible = False
    '        Me.grd.RootTable.Columns(EnumGridDetail.BatchID).Visible = False
    '        Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).Visible = True
    '        Me.grd.RootTable.Columns(EnumGridDetail.CurrentPrice).Visible = False
    '        Me.grd.RootTable.Columns(EnumGridDetail.GroupID).Visible = False
    '        Me.grd.RootTable.Columns(EnumGridDetail.LocationID).Visible = True
    '        Me.grd.RootTable.Columns(EnumGridDetail.LocationID).Caption = "Location"
    '        Me.grd.RootTable.Columns(EnumGridDetail.TradePrice).Caption = "Trade Price"
    '        Me.grd.RootTable.Columns(EnumGridDetail.SO_ID).Caption = "Delivery Order"
    '        Me.grd.RootTable.Columns(EnumGridDetail.DeliveryDetailID).Visible = False
    '        Me.grd.RootTable.Columns(EnumGridDetail.PackQty).Visible = False
    '        Me.grd.RootTable.Columns(EnumGridDetail.SavedQty).Visible = False
    '        Me.grd.RootTable.Columns(EnumGridDetail.Discount_Percentage).Visible = False
    '        Me.grd.RootTable.Columns(EnumGridDetail.PurchasePrice).Visible = False
    '        Me.grd.RootTable.Columns(EnumGridDetail.Pack_Desc).Visible = True
    '        Me.grd.RootTable.Columns(EnumGridDetail.Unit).Visible = True
    '        Me.grd.RootTable.Columns(EnumGridDetail.Pack_Desc).Caption = "Unit"
    '        Me.grd.RootTable.Columns("DeliveredQty").Visible = False
    '        Me.grd.RootTable.Columns(EnumGridDetail.SalesOrderDetailId).Visible = False    ''TASK-408

    '        Me.grd.RootTable.Columns(EnumGridDetail.BaseCurrencyId).Visible = False
    '        Me.grd.RootTable.Columns(EnumGridDetail.BaseCurrencyRate).Visible = False
    '        Me.grd.RootTable.Columns(EnumGridDetail.CurrencyId).Visible = False

    '        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
    '        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed


    '        Me.grd.RootTable.Columns(EnumGridDetail.Qty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.Price).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.SampleQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        'task 2598
    '        ''Me.grd.RootTable.Columns(EnumGridDetail.Comments).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.Tax).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.SED).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        'Me.grd.RootTable.Columns(EnumGridDetail.ServiceQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.Total).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.Freight).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.NetBill).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.TradePrice).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.Discount_Percentage).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns("CurrencyAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


    '        Me.grd.RootTable.Columns(EnumGridDetail.Price).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.Qty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grd.RootTable.Columns(EnumGridDetail.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.SED).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.SampleQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grd.RootTable.Columns(EnumGridDetail.SampleQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        'Me.grd.RootTable.Columns(EnumGridDetail.ServiceQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        'Me.grd.RootTable.Columns(EnumGridDetail.ServiceQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grd.RootTable.Columns(EnumGridDetail.Total).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grd.RootTable.Columns(EnumGridDetail.Total).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.TradePrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns(EnumGridDetail.Discount_Percentage).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

    '        'Me.grd.RootTable.Columns(EnumGridDetail.Freight).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grd.RootTable.Columns(EnumGridDetail.Freight).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

    '        Me.grd.RootTable.Columns(EnumGridDetail.MarketReturns).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grd.RootTable.Columns(EnumGridDetail.MarketReturns).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

    '        Me.grd.RootTable.Columns(EnumGridDetail.NetBill).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grd.RootTable.Columns(EnumGridDetail.NetBill).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

    '        'Me.grd.RootTable.Columns(EnumGridDetail.Tax).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grd.RootTable.Columns(EnumGridDetail.Tax).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

    '        Me.grd.RootTable.Columns(EnumGridDetail.TotalQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grd.RootTable.Columns(EnumGridDetail.TotalQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns("CurrencyAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grd.RootTable.Columns("CurrencyAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

    '        'Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).Position = EnumGridDetail.Unit
    '        For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
    '            'Before against task:M16
    '            'If c.Index <> EnumGridDetail.LocationID AndAlso c.Index <> EnumGridDetail.Price AndAlso c.Index <> EnumGridDetail.Tax AndAlso c.Index <> EnumGridDetail.SampleQty AndAlso c.Index <> EnumGridDetail.SED AndAlso c.Index <> EnumGridDetail.TradePrice AndAlso c.Index <> EnumGridDetail.Discount_Percentage AndAlso c.Index <> EnumGridDetail.Freight AndAlso c.Index <> EnumGridDetail.MarketReturns Then
    '            'Task:M16 Set Editable Fields Engine_No And Chassis_No
    '            'If c.Index <> EnumGridDetail.LocationID AndAlso c.Index <> EnumGridDetail.Price AndAlso c.Index <> EnumGridDetail.Tax AndAlso c.Index <> EnumGridDetail.SampleQty AndAlso c.Index <> EnumGridDetail.SED AndAlso c.Index <> EnumGridDetail.TradePrice AndAlso c.Index <> EnumGridDetail.Discount_Percentage AndAlso c.Index <> EnumGridDetail.Freight AndAlso c.Index <> EnumGridDetail.MarketReturns AndAlso c.Index <> EnumGridDetail.Engine_No AndAlso c.Index <> EnumGridDetail.Chassis_No Then
    '            'End Task:M16


    '            If c.Index <> EnumGridDetail.LocationID AndAlso c.Index <> EnumGridDetail.Price AndAlso c.Index <> EnumGridDetail.Tax AndAlso c.Index <> EnumGridDetail.SampleQty AndAlso c.Index <> EnumGridDetail.SED AndAlso c.Index <> EnumGridDetail.TradePrice AndAlso c.Index <> EnumGridDetail.Discount_Percentage AndAlso c.Index <> EnumGridDetail.Freight AndAlso c.Index <> EnumGridDetail.MarketReturns AndAlso c.Index <> EnumGridDetail.Engine_No AndAlso c.Index <> EnumGridDetail.Chassis_No AndAlso c.Index <> EnumGridDetail.Comments AndAlso c.Index <> EnumGridDetail.BatchNo AndAlso c.Index <> EnumGridDetail.ExpiryDate AndAlso c.Index <> EnumGridDetail.OtherComments AndAlso c.Index <> EnumGridDetail.TotalQty AndAlso c.Index <> EnumGridDetail.Gross_Weights AndAlso c.Index <> EnumGridDetail.Tray_Weights AndAlso c.Index <> EnumGridDetail.Net_Weight Then
    '                c.EditType = Janus.Windows.GridEX.EditType.NoEdit
    '            End If
    '        Next
    '        Me.grd.RootTable.Columns(EnumGridDetail.Price).EditType = Janus.Windows.GridEX.EditType.TextBox
    '        Me.grd.RootTable.Columns(EnumGridDetail.Qty).EditType = Janus.Windows.GridEX.EditType.TextBox
    '        Me.grd.RootTable.Columns(EnumGridDetail.SampleQty).EditType = Janus.Windows.GridEX.EditType.TextBox
    '        '2598
    '        Me.grd.RootTable.Columns(EnumGridDetail.Comments).EditType = Janus.Windows.GridEX.EditType.TextBox

    '        Me.grd.RootTable.Columns(EnumGridDetail.Comments).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far




    '        Me.grd.RootTable.Columns.Add(EnumGridDetail.DeleteButton).EditType = Janus.Windows.GridEX.EditType.NoEdit
    '        Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).Key = "Delete"
    '        Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).Caption = "Action"
    '        Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
    '        Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
    '        Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).ButtonText = "Delete"
    '        If Me.grd.RootTable.Columns.Contains("Column1") = False Then
    '            Me.grd.RootTable.Columns.Add("Column1")
    '            Me.grd.RootTable.Columns("Column1").Key = "Column1"
    '            Me.grd.RootTable.Columns("Column1").ActAsSelector = True
    '            Me.grd.RootTable.Columns("Column1").UseHeaderSelector = True
    '        End If

    '        Me.grd.RootTable.Columns(EnumGridDetail.SO_ID).EditType = Janus.Windows.GridEX.EditType.Combo
    '        If flgLoadAllItems = False Then
    '            Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("Unit").Index
    '            Me.grd.RootTable.Columns("Unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index
    '            Me.grd.RootTable.Columns("Pack_Desc").Visible = True
    '            Me.grd.RootTable.Columns("Unit").Visible = False
    '        Else
    '            Me.grd.RootTable.Columns("Pack_Desc").Visible = False
    '            Me.grd.RootTable.Columns("Unit").Visible = True
    '        End If
    '        'Task:M16 Engine_No And Chassis_No Column Enable/Disabled
    '        If flgVehicleIdentificationInfo = True Then
    '            Me.grd.RootTable.Columns("Engine_No").Visible = True
    '            Me.grd.RootTable.Columns("Chassis_No").Visible = True
    '        Else
    '            Me.grd.RootTable.Columns("Engine_No").Visible = False
    '            Me.grd.RootTable.Columns("Chassis_No").Visible = False
    '        End If
    '        'End Task:M16


    '        'Task:2759 Set Rounded Format

    '        Me.grd.RootTable.Columns(EnumGridDetail.Total).FormatString = "N" & DecimalPointInValue
    '        Me.grd.RootTable.Columns(EnumGridDetail.Total).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

    '        Me.grd.RootTable.Columns(EnumGridDetail.NetBill).FormatString = "N" & DecimalPointInValue
    '        Me.grd.RootTable.Columns(EnumGridDetail.NetBill).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
    '        Me.grd.RootTable.Columns("CurrencyAmount").FormatString = "N" & DecimalPointInValue
    '        Me.grd.RootTable.Columns("CurrencyAmount").TotalFormatString = "N" & TotalAmountRounding
    '        Dim bln As Boolean = Convert.ToBoolean(getConfigValueByType("GridFreezColumn").Replace("Error", "False").Replace("''", "False"))
    '        If bln = True Then
    '            Me.grd.FrozenColumns = Me.grd.RootTable.Columns("Size").Index
    '        Else
    '            Me.grd.FrozenColumns = 0
    '        End If
    '        'End Task:2759
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub ApplyGridSettings(Optional ByVal condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If getConfigValueByType("WeighbridgeDC").ToString.ToUpper = "TRUE" Then
                Me.grd.RootTable.Columns(EnumGridDetail.Gross_Weights).Visible = True
                Me.grd.RootTable.Columns(EnumGridDetail.Tray_Weights).Visible = True
                Me.grd.RootTable.Columns(EnumGridDetail.Net_Weight).Visible = True

            Else

                Me.grd.RootTable.Columns(EnumGridDetail.Gross_Weights).Visible = False
                Me.grd.RootTable.Columns(EnumGridDetail.Tray_Weights).Visible = False
                Me.grd.RootTable.Columns(EnumGridDetail.Net_Weight).Visible = False

            End If

            Me.grd.AutomaticSort = False
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.RootTable.Columns(EnumGridDetail.ArticleID).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.BatchID).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).Visible = True
            Me.grd.RootTable.Columns(EnumGridDetail.CurrentPrice).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.GroupID).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.LocationID).Visible = True
            Me.grd.RootTable.Columns(EnumGridDetail.LocationID).Caption = "Location"
            Me.grd.RootTable.Columns(EnumGridDetail.TradePrice).Caption = "Trade Price"
            Me.grd.RootTable.Columns(EnumGridDetail.SO_ID).Caption = "Delivery Order"
            'Me.grd.RootTable.Columns(EnumGridDetail.CurrencyRate).Caption = "Currency Rate"
            'Me.grd.RootTable.Columns(EnumGridDetail.CurrencyAmount).Caption = "Currency Amount"
            Me.grd.RootTable.Columns(EnumGridDetail.DeliveryDetailID).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.PackQty).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.SavedQty).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.Discount_Percentage).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.PurchasePrice).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.Pack_Desc).Visible = True
            Me.grd.RootTable.Columns(EnumGridDetail.Unit).Visible = True
            Me.grd.RootTable.Columns(EnumGridDetail.Pack_Desc).Caption = "Unit"
            Me.grd.RootTable.Columns("DeliveredQty").Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.SalesOrderDetailId).Visible = False    ''TASK-408
            Me.grd.RootTable.Columns(EnumGridDetail.PackPrice).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.BaseCurrencyId).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.BaseCurrencyRate).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.CurrencyId).Visible = False
            Me.grd.RootTable.Columns(EnumGridDetail.LogicalItem).Visible = False
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            Me.grd.RootTable.Columns(EnumGridDetail.AdditionalItem).Visible = False

            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grd.RootTable.Columns(EnumGridDetail.DiscountId).Caption = "Discount Type" ''TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.DiscountFactor).Caption = "Discount Factor" ''TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.DiscountValue).Caption = "Discount value" ''TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.PostDiscountPrice).Caption = "PDP" ''TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.DiscountValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.DiscountFactor).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.PostDiscountPrice).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.DiscountValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.DiscountFactor).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.PostDiscountPrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far ''TFS2827


            Me.grd.RootTable.Columns(EnumGridDetail.Qty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.Price).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.SampleQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'task 2598
            ''Me.grd.RootTable.Columns(EnumGridDetail.Comments).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.Tax).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.SED).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grd.RootTable.Columns(EnumGridDetail.ServiceQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.Total).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.Freight).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.NetBill).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.CurrencyAmount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.TradePrice).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.Discount_Percentage).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grd.RootTable.Columns(EnumGridDetail.Price).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.Qty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(EnumGridDetail.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.SED).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.SampleQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(EnumGridDetail.SampleQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grd.RootTable.Columns(EnumGridDetail.ServiceQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grd.RootTable.Columns(EnumGridDetail.ServiceQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(EnumGridDetail.Total).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(EnumGridDetail.Total).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.TradePrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.Discount_Percentage).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            'Me.grd.RootTable.Columns(EnumGridDetail.Freight).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(EnumGridDetail.Freight).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(EnumGridDetail.MarketReturns).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(EnumGridDetail.MarketReturns).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(EnumGridDetail.NetBill).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(EnumGridDetail.NetBill).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(EnumGridDetail.CurrencyAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(EnumGridDetail.CurrencyAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            'Me.grd.RootTable.Columns(EnumGridDetail.Tax).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(EnumGridDetail.Tax).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(EnumGridDetail.TotalQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(EnumGridDetail.TotalQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            'Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).Position = EnumGridDetail.Unit
            For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                'Before against task:M16
                'If c.Index <> EnumGridDetail.LocationID AndAlso c.Index <> EnumGridDetail.Price AndAlso c.Index <> EnumGridDetail.Tax AndAlso c.Index <> EnumGridDetail.SampleQty AndAlso c.Index <> EnumGridDetail.SED AndAlso c.Index <> EnumGridDetail.TradePrice AndAlso c.Index <> EnumGridDetail.Discount_Percentage AndAlso c.Index <> EnumGridDetail.Freight AndAlso c.Index <> EnumGridDetail.MarketReturns Then
                'Task:M16 Set Editable Fields Engine_No And Chassis_No
                'If c.Index <> EnumGridDetail.LocationID AndAlso c.Index <> EnumGridDetail.Price AndAlso c.Index <> EnumGridDetail.Tax AndAlso c.Index <> EnumGridDetail.SampleQty AndAlso c.Index <> EnumGridDetail.SED AndAlso c.Index <> EnumGridDetail.TradePrice AndAlso c.Index <> EnumGridDetail.Discount_Percentage AndAlso c.Index <> EnumGridDetail.Freight AndAlso c.Index <> EnumGridDetail.MarketReturns AndAlso c.Index <> EnumGridDetail.Engine_No AndAlso c.Index <> EnumGridDetail.Chassis_No Then
                'End Task:M16


                If c.Index <> EnumGridDetail.LocationID AndAlso c.Index <> EnumGridDetail.Price AndAlso c.Index <> EnumGridDetail.Tax AndAlso c.Index <> EnumGridDetail.SampleQty AndAlso c.Index <> EnumGridDetail.SED AndAlso c.Index <> EnumGridDetail.TradePrice AndAlso c.Index <> EnumGridDetail.Discount_Percentage AndAlso c.Index <> EnumGridDetail.Freight AndAlso c.Index <> EnumGridDetail.MarketReturns AndAlso c.Index <> EnumGridDetail.Engine_No AndAlso c.Index <> EnumGridDetail.Chassis_No AndAlso c.Index <> EnumGridDetail.Comments AndAlso c.Index <> EnumGridDetail.BatchNo AndAlso c.Index <> EnumGridDetail.ExpiryDate AndAlso c.Index <> EnumGridDetail.Origin AndAlso c.Index <> EnumGridDetail.OtherComments AndAlso c.Index <> EnumGridDetail.TotalQty AndAlso c.Index <> EnumGridDetail.Gross_Weights AndAlso c.Index <> EnumGridDetail.Tray_Weights AndAlso c.Index <> EnumGridDetail.Net_Weight AndAlso c.Index <> EnumGridDetail.CurrencyRate AndAlso c.Index <> EnumGridDetail.CurrencyAmount AndAlso c.Index <> EnumGridDetail.BardanaDeduction AndAlso c.Index <> EnumGridDetail.OtherDeduction AndAlso c.Index <> EnumGridDetail.DiscountId Then
                    c.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            ''Start TFS4161
            If IsPackQtyDisabled = True Then
                Me.grd.RootTable.Columns(EnumGridDetail.TotalQty).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Else
                Me.grd.RootTable.Columns(EnumGridDetail.TotalQty).EditType = Janus.Windows.GridEX.EditType.TextBox
            End If
            ''End TFS4161
            Me.grd.RootTable.Columns(EnumGridDetail.DiscountValue).EditType = Janus.Windows.GridEX.EditType.NoEdit 'TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.DiscountFactor).EditType = Janus.Windows.GridEX.EditType.TextBox 'TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.PostDiscountPrice).EditType = Janus.Windows.GridEX.EditType.TextBox  'TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.Price).EditType = Janus.Windows.GridEX.EditType.NoEdit 'TFS2827
            Me.grd.RootTable.Columns(EnumGridDetail.DiscountId).EditType = Janus.Windows.GridEX.EditType.Combo  'TFS2827
            'Me.grd.RootTable.Columns(EnumGridDetail.Price).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grd.RootTable.Columns(EnumGridDetail.Qty).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grd.RootTable.Columns(EnumGridDetail.SampleQty).EditType = Janus.Windows.GridEX.EditType.TextBox
            '2598
            Me.grd.RootTable.Columns(EnumGridDetail.Comments).EditType = Janus.Windows.GridEX.EditType.TextBox
            'Rafay :task Start :Edit uom field
            Me.grd.RootTable.Columns(EnumGridDetail.UM).EditType = Janus.Windows.GridEX.EditType.TextBox
            'Rafay:Task End
            Me.grd.RootTable.Columns(EnumGridDetail.Comments).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(EnumGridDetail.Price).FormatString = "N"
            Me.grd.RootTable.Columns(EnumGridDetail.PostDiscountPrice).FormatString = "N"
            Me.grd.RootTable.Columns(EnumGridDetail.TradePrice).FormatString = "N"
            Me.grd.RootTable.Columns(EnumGridDetail.PurchasePrice).FormatString = "N"
            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add(EnumGridDetail.DeleteButton).EditType = Janus.Windows.GridEX.EditType.NoEdit
                Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).Key = "Delete"
                Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).Caption = "Action"
                Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns(EnumGridDetail.DeleteButton).ButtonText = "Delete"
            End If
            If Me.grd.RootTable.Columns.Contains("Column1") = False Then
                Me.grd.RootTable.Columns.Add("Column1")
                Me.grd.RootTable.Columns("Column1").Key = "Column1"
                Me.grd.RootTable.Columns("Column1").ActAsSelector = True
                Me.grd.RootTable.Columns("Column1").UseHeaderSelector = True
            End If


            Me.grd.RootTable.Columns(EnumGridDetail.SO_ID).EditType = Janus.Windows.GridEX.EditType.Combo
            If flgLoadAllItems = False Then
                Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("Unit").Index
                Me.grd.RootTable.Columns("Unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index
                Me.grd.RootTable.Columns("Pack_Desc").Visible = True
                Me.grd.RootTable.Columns("Unit").Visible = False
            Else
                Me.grd.RootTable.Columns("Pack_Desc").Visible = False
                Me.grd.RootTable.Columns("Unit").Visible = True
            End If
            'Task:M16 Engine_No And Chassis_No Column Enable/Disabled
            If flgVehicleIdentificationInfo = True Then
                Me.grd.RootTable.Columns("Engine_No").Visible = True
                Me.grd.RootTable.Columns("Chassis_No").Visible = True
            Else
                Me.grd.RootTable.Columns("Engine_No").Visible = False
                Me.grd.RootTable.Columns("Chassis_No").Visible = False
            End If
            'End Task:M16
            ''Start Task 1596 : Ayesha Rehman
            Dim StockOutConfigration As String = "" ''1596
            If Not getConfigValueByType("StockOutConfigration").ToString = "Error" Then ''1596
                StockOutConfigration = getConfigValueByType("StockOutConfigration").ToString
            End If
            If StockOutConfigration.Equals("Disabled") Then
                Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).Visible = False
                Me.grd.RootTable.Columns(EnumGridDetail.ExpiryDate).Visible = False
                Me.grd.RootTable.Columns(EnumGridDetail.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.NoEdit
                Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).EditType = Janus.Windows.GridEX.EditType.NoEdit
                Me.grd.RootTable.Columns("Origin").Visible = False
                Me.grd.RootTable.Columns("Origin").EditType = Janus.Windows.GridEX.EditType.NoEdit
            ElseIf StockOutConfigration.Equals("Enabled") Then
                Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).Visible = True
                Me.grd.RootTable.Columns(EnumGridDetail.ExpiryDate).Visible = True
                Me.grd.RootTable.Columns(EnumGridDetail.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
                Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).EditType = Janus.Windows.GridEX.EditType.Combo
                Me.grd.RootTable.Columns("Origin").Visible = True
                Me.grd.RootTable.Columns("Origin").EditType = Janus.Windows.GridEX.EditType.Combo
            Else
                Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).Visible = True
                Me.grd.RootTable.Columns(EnumGridDetail.ExpiryDate).Visible = True
                Me.grd.RootTable.Columns(EnumGridDetail.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
                Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).EditType = Janus.Windows.GridEX.EditType.Combo
                Me.grd.RootTable.Columns("Origin").Visible = True
                Me.grd.RootTable.Columns("Origin").EditType = Janus.Windows.GridEX.EditType.Combo
            End If
            ''End Task 1596
            ''Start TFS1807
            Me.grd.RootTable.Columns(EnumGridDetail.OrderQty).FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns(EnumGridDetail.OrderQty).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(EnumGridDetail.OrderQty).TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(EnumGridDetail.DeliverQty).FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns(EnumGridDetail.DeliverQty).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(EnumGridDetail.DeliverQty).TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(EnumGridDetail.Qty).FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns(EnumGridDetail.Qty).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(EnumGridDetail.Qty).TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(EnumGridDetail.RemainingQty).FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns(EnumGridDetail.RemainingQty).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(EnumGridDetail.RemainingQty).TotalFormatString = "N" & TotalAmountRounding
            ''End TFS1807
            'Task:2759 Set Rounded Format

            Me.grd.RootTable.Columns("Total").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Total").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("Total").TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.grd.RootTable.Columns("PostDiscountPrice").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("PostDiscountPrice").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("PostDiscountPrice").TotalFormatString = "N" & TotalAmountRounding

            'Me.grd.RootTable.Columns("Price").FormatString = "N" & DecimalPointInValue
            'Me.grd.RootTable.Columns("Price").FormatString = "N" & TotalAmountRounding
            'Me.grd.RootTable.Columns("Price").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns("DiscountValue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("DiscountValue").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("DiscountValue").TotalFormatString = "N" & TotalAmountRounding

            'Me.grd.RootTable.Columns("TaxAmount").FormatString = "N" & DecimalPointInValue
            'Me.grd.RootTable.Columns("TaxAmount").FormatString = "N" & TotalAmountRounding
            'Me.grd.RootTable.Columns("TaxAmount").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns("CurrencyRate").FormatString = "N" & 4
            Me.grd.RootTable.Columns("CurrencyRate").FormatString = "N" & 4
            Me.grd.RootTable.Columns("CurrencyRate").TotalFormatString = "N" & 4

            Me.grd.RootTable.Columns("NetBill").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("NetBill").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("NetBill").TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            Me.grd.RootTable.Columns("CurrencyAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("CurrencyAmount").FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns("CurrencyAmount").TotalFormatString = "N" & TotalAmountRounding
            'Me.grd.RootTable.Columns("CurrencyTotalAmount").FormatString = "N" & DecimalPointInValue
            'Me.grd.RootTable.Columns("CurrencyTotalAmount").FormatString = "N" & TotalAmountRounding
            'Me.grd.RootTable.Columns("CurrencyTotalAmount").TotalFormatString = "N" & TotalAmountRounding
            Dim bln As Boolean = Convert.ToBoolean(getConfigValueByType("GridFreezColumn").Replace("Error", "False").Replace("''", "False"))
            If bln = True Then
                Me.grd.FrozenColumns = Me.grd.RootTable.Columns("Size").Index
            Else
                Me.grd.FrozenColumns = 0
            End If

            'End Task:2759
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try

            Dim DiscountFactor As Double = Me.grd.GetRow.Cells(EnumGridDetail.DiscountFactor).Value
            Dim DiscountType As Decimal = Me.grd.GetRow.Cells(EnumGridDetail.DiscountId).Value
            Dim PostDiscountPrice As Decimal = Me.grd.GetRow.Cells(EnumGridDetail.PostDiscountPrice).Value
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
            ''Start TFS-3040
            Dim orderQty As Double = Me.grd.GetRow.Cells(EnumGridDetail.OrderQty).Value
            Dim qty As Double = Me.grd.GetRow.Cells(EnumGridDetail.Qty).Value
            Dim DeliveredQty As Double = Me.grd.GetRow.Cells(EnumGridDetail.DeliverQty).Value
            Dim SalesOrderId As Integer = Me.grd.GetRow.Cells(EnumGridDetail.SO_ID).Value
            If e.Column.Index = EnumGridDetail.Qty Then
                If qty < 0 Then
                    ShowErrorMessage("Quantity can not be negative")
                    grd.CancelCurrentEdit()
                ElseIf SalesOrderId > 0 Then
                    If orderQty < (qty + DeliveredQty) Then
                        If blnOrderQtyExceed Then ''TFS4360
                            ShowErrorMessage("Quantity can not be grater then order quantity")
                            grd.CancelCurrentEdit()
                        End If
                    End If
                End If
            End If
            ''End TFS3040
            If e.Column.Key = "Qty" Then
                Dim IsMinus As Boolean = True
                IsMinus = getConfigValueByType("AllowMinusStock")
                If Mode = "Normal" Then

                    If Not IsMinus Then

                        If Me.grd.CurrentRow.Cells(EnumGridDetail.Pack_Desc).Value.ToString = "Loose" Then
                            If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(EnumGridDetail.ArticleID).Value, Me.grd.CurrentRow.Cells(EnumGridDetail.LocationID).Value)) <> grd.GetRow.Cells(EnumGridDetail.Qty).Value Then
                                If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(EnumGridDetail.ArticleID).Value, Me.grd.CurrentRow.Cells(EnumGridDetail.LocationID).Value)) - grd.GetRow.Cells(EnumGridDetail.Qty).Value <= 0 Then
                                    msg_Error(Me.grd.CurrentRow.Cells(EnumGridDetail.Item).Value.ToString & str_ErrorStockNotEnough)
                                    grd.CancelCurrentEdit()
                                End If
                            End If
                        Else
                            If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(EnumGridDetail.ArticleID).Value, Me.grd.CurrentRow.Cells(EnumGridDetail.LocationID).Value, "Loose")) <> Me.grd.CurrentRow.Cells(EnumGridDetail.TotalQty).Value Or Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(EnumGridDetail.ArticleID).Value, Me.grd.CurrentRow.Cells(EnumGridDetail.LocationID).Value)) <> grd.GetRow.Cells(EnumGridDetail.Qty).Value Then
                                If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(EnumGridDetail.ArticleID).Value, Me.grd.CurrentRow.Cells(EnumGridDetail.LocationID).Value, "Loose")) - Me.grd.CurrentRow.Cells(EnumGridDetail.TotalQty).Value < 0 Or Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(EnumGridDetail.ArticleID).Value, Me.grd.CurrentRow.Cells(EnumGridDetail.LocationID).Value)) - grd.GetRow.Cells(EnumGridDetail.Qty).Value < 0 Then
                                    msg_Error(Me.grd.CurrentRow.Cells(EnumGridDetail.Item).Value.ToString & str_ErrorStockNotEnough)
                                    grd.CancelCurrentEdit()
                                End If
                            End If
                        End If
                        ''End TFS4358
                    End If
                    ''Start TFS4358
                    If CType(Me.cmbCategory.SelectedItem, DataRowView).Row.Item("AllowMinusStock").ToString = "False" AndAlso IsMinus = True Then

                        If Me.grd.CurrentRow.Cells(EnumGridDetail.Pack_Desc).Value.ToString = "Loose" Then
                            If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(EnumGridDetail.ArticleID).Value, Me.grd.CurrentRow.Cells(EnumGridDetail.LocationID).Value)) <> grd.GetRow.Cells(EnumGridDetail.Qty).Value Then
                                If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(EnumGridDetail.ArticleID).Value, Me.grd.CurrentRow.Cells(EnumGridDetail.LocationID).Value)) - grd.GetRow.Cells(EnumGridDetail.Qty).Value <= 0 Then
                                    msg_Error(Me.grd.CurrentRow.Cells(EnumGridDetail.Item).Value.ToString & str_ErrorStockNotEnough)
                                    grd.CancelCurrentEdit()
                                End If
                            End If
                        Else
                            If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(EnumGridDetail.ArticleID).Value, Me.grd.CurrentRow.Cells(EnumGridDetail.LocationID).Value, "Loose")) <> Me.grd.CurrentRow.Cells(EnumGridDetail.TotalQty).Value Or Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(EnumGridDetail.ArticleID).Value, Me.grd.CurrentRow.Cells(EnumGridDetail.LocationID).Value)) <> grd.GetRow.Cells(EnumGridDetail.Qty).Value Then
                                If Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(EnumGridDetail.ArticleID).Value, Me.grd.CurrentRow.Cells(EnumGridDetail.LocationID).Value, "Loose")) - Me.grd.CurrentRow.Cells(EnumGridDetail.TotalQty).Value < 0 Or Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(EnumGridDetail.ArticleID).Value, Me.grd.CurrentRow.Cells(EnumGridDetail.LocationID).Value)) - grd.GetRow.Cells(EnumGridDetail.Qty).Value < 0 Then
                                    msg_Error(Me.grd.CurrentRow.Cells(EnumGridDetail.Item).Value.ToString & str_ErrorStockNotEnough)
                                    grd.CancelCurrentEdit()
                                End If
                            End If
                        End If

                    End If
                    ''End TFS4358
                End If
                ''End TFS4358
            End If
            GetGridDetailQtyCalculate(e)    'Task#25082015
            GetGridDetailTotal()            'Task#25082015
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub grd_RecordsDeleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.RecordsDeleted
        Try

            GetGridDetailQtyCalculate(e)    'Task#25082015
            GetGridDetailTotal()            'Task#25082015
            Me.GetTotal()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            StockMaster = New StockMaster
            StockMaster.StockTransId = Convert.ToInt32(GetStockTransId(grdSaved.CurrentRow.Cells(0).Value))
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
            StockMaster.DocNo = Me.txtPONo.Text
            StockMaster.DocDate = Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt")
            StockMaster.DocType = 3 'Convert.ToInt32(GetStockDocTypeId("Delivery").ToString)
            StockMaster.Remaks = Me.txtRemarks.Text
            StockMaster.Project = Me.cmbCompany.SelectedValue
            StockMaster.AccountId = Me.cmbVendor.Value
            StockMaster.StockDetailList = StockList
            'For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            '    If Val(grdRow.Cells("TotalQty").Value) > 0 Then
            '        StockDetail = New StockDetail
            '        StockDetail.StockTransId = transId 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
            '        StockDetail.LocationId = grdRow.Cells("LocationId").Value
            '        StockDetail.ArticleDefId = grdRow.Cells("ArticleDefId").Value
            '        StockDetail.InQty = 0
            '        'StockDetail.OutQty = IIf(grdRow.Cells("Unit").Value = "Loose", Val(grdRow.Cells("Qty").Value) + Val(grdRow.Cells("Sample Qty").Value), ((Val(grdRow.Cells("Qty").Value) + Val(grdRow.Cells("Sample Qty").Value)) * Val(grdRow.Cells("Pack Qty").Value)))
            '        StockDetail.OutQty = Val(grdRow.Cells("TotalQty").Value.ToString) + Val(grdRow.Cells("Sample Qty").Value.ToString)
            '        StockDetail.Rate = Val(grdRow.Cells("Rate").Value)
            '        StockDetail.InAmount = 0
            '        'StockDetail.OutAmount = IIf(grdRow.Cells("Unit").Value = "Loose", ((Val(grdRow.Cells("Qty").Value) + Val(grdRow.Cells("Sample Qty").Value)) * Val(grdRow.Cells("Rate").Value)), (((Val(grdRow.Cells("Qty").Value) + Val(grdRow.Cells("Sample Qty").Value)) * Val(grdRow.Cells("Pack Qty").Value)) * Val(grdRow.Cells("Rate").Value)))
            '        StockDetail.OutAmount = ((Val(grdRow.Cells("TotalQty").Value) + Val(grdRow.Cells("Sample Qty").Value)) * Val(grdRow.Cells("Rate").Value))
            '        ''TASK-470 On 01-07-2016
            '        StockDetail.PackQty = Val(grdRow.Cells("Pack Qty").Value.ToString)
            '        StockDetail.Out_PackQty = Val(grdRow.Cells("Qty").Value.ToString)
            '        StockDetail.In_PackQty = 0
            '        ''End TASK-407
            '        StockDetail.Remarks = String.Empty
            '        StockMaster.StockDetailList.Add(StockDetail)
            '    End If
            'Next
        Catch ex As Exception
            Throw ex
        End Try
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
    Sub GetAllRecords()

    End Sub


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
    Private Sub txtAdjustment_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdjustment.LostFocus
        Try
            If IsFormOpend = True Then GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl2_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl2.SelectedTabChanged
        'If Me.UltraTabControl2.SelectedTab.Index = 0 Then
        '    Me.btnLoadAll.Visible = False
        '    Me.ToolStripButton1.Visible = False
        '    ToolStripSeparator1.Visible = False
        'Else
        '    Me.btnLoadAll.Visible = True
        '    Me.ToolStripButton1.Visible = True
        '    ToolStripSeparator1.Visible = True
        'End If
        Try
            If e.Tab.Index = 1 Then
                DisplayRecord()
                '' Hide Buttons Edit,Delete and Print on Load Form
                Me.BtnDelete.Visible = True
                Me.BtnPrint.Visible = True
            Else
                Me.BtnEdit.Visible = False
                If IsEditMode = False Then Me.BtnDelete.Visible = False
                If IsEditMode = False Then Me.BtnPrint.Visible = False
                '''''''''''''''''''''''''''''''''''''''''''''''''''''
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub TextBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDisc.Leave, cmbVendor.Enter
        Try
            'Dim disc As Double = 0D
            'Double.TryParse(Me.txtDisc.Text.Trim, disc)

            'Dim price As Double = 0D
            'Double.TryParse(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString, price)

            'Me.txtRate.Text = price - ((price / 100) * disc)
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
        Catch ex As Exception

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
            DiscountCalculation() ''TFS3330
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BalanceSheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BalanceSheetToolStripMenuItem.Click
        rptDateRange.ReportName = rptDateRange.ReportList.rptBSFomated
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub
    'Private Sub btnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    DisplayRecord("All")
    '    Me.DisplayDetail(-1)
    '    Me.Cursor = Cursors.Default
    'End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        Try

            'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
            'TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            'END TFS4544
            'R-974 Ehtisham ul Haq user friendly system modification on 24-1-14 
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim id As Integer
            id = 0

            id = Me.cmbVendor.Value
            FillCombo("Vendor")
            Me.cmbVendor.Value = id

            RemoveHandler cmbPo.SelectedIndexChanged, AddressOf cmbPo_SelectedIndexChanged
            id = Me.cmbPo.SelectedValue
            FillCombo("SO")
            Me.cmbPo.SelectedValue = id
            AddHandler cmbPo.SelectedIndexChanged, AddressOf cmbPo_SelectedIndexChanged

            id = Me.cmbCompany.SelectedValue
            RemoveHandler cmbCompany.SelectedIndexChanged, AddressOf cmbCompany_SelectedIndexChanged
            FillCombo("Company")
            AddHandler cmbCompany.SelectedIndexChanged, AddressOf cmbCompany_SelectedIndexChanged
            Me.cmbCompany.SelectedValue = id

            id = Me.cmbSalesMan.SelectedValue
            FillCombo("SM")
            Me.cmbSalesMan.SelectedValue = id

            id = Me.cmbTransporter.SelectedValue
            FillCombo("Transporter")
            Me.cmbTransporter.SelectedValue = id

            id = Me.cmbCategory.SelectedValue
            FillCombo("Category")
            Me.cmbCategory.SelectedValue = id

            id = Me.cmbCodes.Value
            FillCombo("Barcodes")
            Me.cmbCodes.Value = id

            id = Me.cmbItem.Value
            FillCombo("Item")
            Me.cmbItem.Value = id
            FillCombo("Colour")
            id = Me.cmbCurrency.SelectedValue
            FillCombo("Currency")
            Me.cmbCurrency.SelectedValue = id

            '' Setting Account ids
            FuelExpAccount = Val(getConfigValueByType("FuelExpAccount").ToString)
            AdjustmentExpAccount = Val(getConfigValueByType("AdjustmentExpAccount").ToString)
            OtherExpAccount = Val(getConfigValueByType("OtherExpAccount").ToString)
            '' End setting account ids
            If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
                flgLoadAllItems = getConfigValueByType("LoadAllItemsInSales")
            End If

            If Not getConfigValueByType("ArticleFilterByLocation") = "Error" Then
                flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
            End If

            'TAsk:2795 Set ByRef Variable blnOrderQtyExceed
            If Not getConfigValueByType("OrderQtyExceedAgainstDeliveryChalan").ToString = "Error" Then
                blnOrderQtyExceed = Val(getConfigValueByType("OrderQtyExceedAgainstDeliveryChalan").ToString)
            Else
                blnOrderQtyExceed = False
            End If
            'End Task:2796

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

            FillCombo("grdLocation")
            FillCombo("grdSO")
            GetDeliveryOrderAnalysis()

            If Not getConfigValueByType("Company-Based-Prefix").ToString = "Error" Then
                CompanyBasePrefix = Convert.ToBoolean(getConfigValueByType("Company-Based-Prefix").ToString)
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
            If Not getConfigValueByType("MultipleSalesOrder").ToString = "Error" Then
                flgMultipleSalesOrder = Convert.ToBoolean(getConfigValueByType("MultipleSalesOrder").ToString)
            Else
                flgMultipleSalesOrder = False
            End If

            If flgMultipleSalesOrder = True Then
                Me.btnLoad.Visible = True
            Else
                Me.btnLoad.Visible = False
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try

    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.LoadAllItems()
            'Me.LinkLabel1.Enabled = False
            Me.pnlSimple.Height = 80
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub LoadAllItems()
        Try
            For Each Row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.cmbItem.Rows
                If Row.Index > 0 Then
                    Me.cmbItem.Focus()
                    Row.Selected = True
                    Me.txtQty.Focus()
                    'Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
                    AddItemToGrid(True)
                    Application.DoEvents()
                End If
            Next
            Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.CloseDropdown)
            GetTotal()
            ClearDetailControls()
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Sub
    Function GetCostCenterId(ByVal CompanyId As Integer) As Integer
        Try
            Dim str As String
            Dim dt As New DataTable
            Dim adp As New OleDb.OleDbDataAdapter
            str = "select IsNull(CostCenterId,0) as CostCenterId From CompanyDefTable WHERE CompanyId=" & CompanyId & ""
            adp = New OleDb.OleDbDataAdapter(str, Con)
            adp.Fill(dt)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0)
                'Else
                '    Return 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetDeliveryRemarks(ByVal SOID As Integer) As String
        Try
            Dim str As String
            Dim dt As New DataTable
            Dim adp As New OleDb.OleDbDataAdapter
            str = "Select Remarks From SalesOrderMasterTable WHERE SalesOrderID= " & SOID & ""
            adp = New OleDb.OleDbDataAdapter(str, Con)
            adp.Fill(dt)
            Return dt.Rows(0).Item(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub FillComboByEdit()
        Try
            If IsEditMode = True Then
                FillCombo("Vendor")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords1(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub
    Private Sub GridBarUserControl2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs1 As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs1)
                fs1.Flush()
                fs1.Close()
                fs1.Dispose()
            End If
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
                GetTotal()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ComboFillByEdit()
        Try
            FillCombo("Vendor")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Delivery Chalan"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                ' Return GetSerialNo(IIf(CompanyBasePrefix = False & IIf(Me.GetPrefix(IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue)).Length = 0, "SI" & Me.cmbCompany.SelectedValue & "-" & Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year & "-", "DeliveryChalanMasterTable", "DeliveryNo")
                'If CompanyBasePrefix = True AndAlso GetPrefix(IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue)).Length > 0 Then
                '    Return GetSerialNo("" & GetPrefix(Me.cmbCompany.SelectedValue) & "" & "-", "DeliveryChalanMasterTable", "DeliveryNo")
                'Else
                'Return GetSerialNo("DC" & Me.cmbCompany.SelectedValue & "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "DeliveryChalanMasterTable", "DeliveryNo")
                'End If
                'Rafay:Task Start
                If CompanyPrefix = "V-ERP (UAE)" Then
                    '                    companyinitials = "UE"
                    '  Return GetNextDocNo("DC" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "DeliveryChalanMasterTable", "DeliveryNo")
                    Return GetNextDocNo("DC1" & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "DeliveryChalanMasterTable", "DeliveryNo")

                    'Return GetSerialNo("DC" & "-" & companyinitials & "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "DeliveryChalanMasterTable", "DeliveryNo")
                Else
                    companyinitials = "PK"
                    Return GetNextDocNo("DC" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "DeliveryChalanMasterTable", "DeliveryNo")
                End If

            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                '' Return GetNextDocNo("DC" & Me.cmbCompany.SelectedValue & "-" & CompanyPrefix & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "DeliveryChalanMasterTable", "DeliveryNo")
                If CompanyPrefix = "V-ERP (UAE)" Then
                    ' companyinitials = "UE"
                    '  Return GetNextDocNo("DC" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "DeliveryChalanMasterTable", "DeliveryNo")
                    Return GetSerialNo("DC1" & "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "DeliveryChalanMasterTable", "DeliveryNo")
                Else
                    companyinitials = "PK"
                    Return GetNextDocNo("DC" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "DeliveryChalanMasterTable", "DeliveryNo")
                End If
                'rafay:task end
            Else
                Return GetNextDocNo("DC" & Me.cmbCompany.SelectedValue, 6, "DeliveryChalanMasterTable", "DeliveryNo")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnAddNewItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewItem.Click
        Call frmAddItem.ShowDialog()
        If Not DialogResult = Windows.Forms.DialogResult.OK Then Call FillCombo("Item")
    End Sub
    Private Sub CtrlGrdBar2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Delivery Chalan"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function FindExistsItem(ByVal ArticleId As Integer, ByVal Rate As Double, ByVal Pack As String, ByVal PackQty As Double, ByVal SO_Id As Integer, ByVal LocationID As Integer) As Boolean
        Try
            'Task:2432 Added flg Marge Item.
            If flgMargeItem = True Then
                Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
                Dim dr() As DataRow
                ''TASK TFS1648 also added condition of locationId
                dr = dt.Select("ArticleDefId=" & ArticleId & " And Rate=" & Val(Rate) & " AND Unit='" & Pack & "' AND [Pack Qty]=" & Val(PackQty) & " AND SO_ID=" & Val(Me.cmbPo.SelectedValue) & " And UOM='" & Me.cmbUM.Text.Replace("'", "''") & "' AND LocationId =" & LocationID & "")
                If dr.Length > 0 Then
                    For Each r As DataRow In dr
                        If dr(0).ItemArray(0) = LocationID AndAlso dr(0).ItemArray(11) = r.ItemArray(11) AndAlso dr(0).ItemArray(8) = r.ItemArray(8) AndAlso dr(0).ItemArray(5) = r.ItemArray(5) Then
                            If dr(0).ItemArray(5) = "Pack" Then
                                r.BeginEdit()
                                r(6) = Val(dr(0).ItemArray(6)) + Val(Me.txtQty.Text)
                                r("TotalQty") = r(6) * Val(Me.txtPackQty.Text)

                                r.EndEdit()
                            Else
                                r.BeginEdit()
                                r(6) = Val(dr(0).ItemArray(6)) + Val(Me.txtQty.Text)
                                r("TotalQty") = r(6)
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
    Private Sub cmbItem_RowSelected(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            End If
            If Not getConfigValueByType("StockViewOnDelivery").ToString = "True" Then
                If Me.cmbItem.Value Is Nothing Then Exit Sub
                If IsDeliveryOrderAnalysis = True Then
                    Dim dt As DataTable = GetCostManagement(Me.cmbItem.Value)
                    If dt IsNot Nothing Then
                        TradePrice = dt.Rows(0).Item("TradePrice")
                        Freight_Rate = dt.Rows(0).Item("Freight")
                        MarketReturns_Rate = dt.Rows(0).Item("MarketReturns")
                        GST_Applicable = dt.Rows(0).Item("Gst_Applicable")
                        FlatRate_Applicable = dt.Rows(0).Item("FlatRate_Applicable")
                        FlatRate = dt.Rows(0).Item("FlatRate")
                        Freight = Freight_Rate
                        MarketReturns = MarketReturns_Rate
                    End If
                    Dim dtDiscount As DataTable = GetAnalysisLastDiscount(Me.cmbVendor.Value, Me.cmbItem.Value)
                    If dtDiscount IsNot Nothing Then
                        If dtDiscount.Rows.Count > 0 Then
                            Me.txtDisc.Text = dtDiscount.Rows(0).Item(0)
                        Else
                            Me.txtDisc.Text = 0
                        End If
                    Else
                        txtDisc.Text = 0
                    End If
                Else
                    txtDisc.Text = GetLastDiscount(IIf(Me.cmbItem.IsItemInList = True, Me.cmbVendor.Value, 0), Me.cmbItem.Value)
                End If
            Else
                txtDisc.Text = GetLastDiscount(IIf(Me.cmbItem.IsItemInList = True, Me.cmbVendor.Value, 0), Me.cmbItem.Value)
            End If
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            ''Start TFS4804
            Dim Str As String = " Select  BatchNo,ExpiryDate,Origin  From  StockDetailTable  where BatchNo not in ('','0','xxxx')  And ArticledefId = " & Me.cmbItem.ActiveRow.Cells(0).Value & "  Group by BatchNo,ExpiryDate,Origin Having Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0)) > 0  ORDER BY ExpiryDate Asc"
            Dim dtBatchNo As DataTable = GetDataTable(Str)
            If dtBatchNo.Rows.Count > 0 Then
                Me.SOBatchNo = dtBatchNo.Rows(0).Item("BatchNo").ToString
                Me.SOExpiryDate = Convert.ToDateTime(dtBatchNo.Rows(0).Item("ExpiryDate").Date)
                Me.SOOrigin = dtBatchNo.Rows(0).Item("Origin").ToString
            Else
                Me.SOBatchNo = "xxxx"
                Me.SOExpiryDate = Convert.ToDateTime(Date.Now.AddMonths(1))
                Me.SOOrigin = ""
            End If
            ''End TFS4804
            FillCombo("ArticlePack")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub btnReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.grd.RowCount = 0 Then Exit Sub
    '        If getConfigValueByType("ReceiptVoucherOnDelivery").ToString = "True" Then
    '            Me.SplitContainer1.Panel2Collapsed = False
    '        Else
    '            Me.SplitContainer1.Panel2Collapsed = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
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

        'Me.cmbMethod.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
        'Me.cmbMethod.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
        'Me.cmbMethod.DataSource = dt

    End Sub
    Function GetVoucherNo() As String
        Dim docNo As String = String.Empty
        Dim VType As String = String.Empty
        'If Me.cmbMethod.SelectedIndex > 0 Then
        '    VType = "BRV"
        'Else
        '    VType = "CRV"
        'End If
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
    'Private Sub cmbMethod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim str As String = String.Empty
    '        If Me.cmbMethod Is Nothing Then Exit Sub
    '        If GetConfigValue("ReceiptVoucherOnDelivery").ToString = "True" Then
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
    'Private Sub VoucherDetail(ByVal DeliveryNo As String)
    '    Try
    '        Dim str As String = String.Empty
    '        str = "SELECT dbo.tblVoucher.voucher_id, dbo.tblVoucher.location_id, dbo.tblVoucher.voucher_code, dbo.tblVoucher.voucher_type_id, dbo.tblVoucher.voucher_no, " _
    '              & " dbo.tblVoucherDetail.credit_amount, tblVoucherDetail.debit_amount, dbo.tblVoucherDetail.CostCenterID, tblVoucher.coa_detail_id, tblVoucher.Cheque_No, tblVoucher.Cheque_Date " _
    '              & " FROM  dbo.tblVoucher INNER JOIN " _
    '              & " dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id " _
    '              & " WHERE (dbo.tblVoucher.voucher_type_id = 3 Or dbo.tblVoucher.voucher_type_id=5) AND (tblVoucherDetail.coa_detail_id=" & Me.cmbVendor.Value & ") AND (dbo.tblVoucher.voucher_code = N'" & DeliveryNo & "')"
    '        Dim dt As DataTable = GetDataTable(str)
    '        If dt IsNot Nothing Then
    '            If dt.Rows.Count > 0 Then
    '                Me.cmbMethod.SelectedValue = Convert.ToInt32(dt.Rows(0).ItemArray(3))
    '                Me.cmbDepositAccount.SelectedValue = Convert.ToInt32(dt.Rows(0).ItemArray(8))

    '                If Not IsDBNull(dt.Rows(0).ItemArray(9)) Then
    '                    Me.txtChequeNo.Text = dt.Rows(0).ItemArray(9).ToString
    '                Else
    '                    Me.txtChequeNo.Text = String.Empty
    '                End If

    '                If Not IsDBNull(dt.Rows(0).ItemArray(10)) Then
    '                    Me.dtpChequeDate.Value = Convert.ToDateTime(dt.Rows(0).ItemArray(10))
    '                Else
    '                    Me.dtpChequeDate.Value = Date.Today
    '                End If

    '                Me.txtVoucherNo.Text = dt.Rows(0).ItemArray(4)
    '                Me.txtRecAmount.Text = Convert.ToDouble(dt.Rows(0).ItemArray(5))
    '                VNo = dt.Rows(0).ItemArray(4)
    '                VoucherId = dt.Rows(0).ItemArray(0)
    '                'Me.cmbMethod.Enabled = False
    '                ExistingVoucherFlg = True
    '            Else
    '                ExistingVoucherFlg = False
    '            End If
    '        Else
    '            ExistingVoucherFlg = False
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Function EmailSave()
        EmailSave = Nothing
        Dim flg As Boolean = False
        If Me.cmbVendor.ActiveRow Is Nothing Then Exit Function

        If IsEmailAlert = True Then
            Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name='frmDelivery' AND EmailAlert=1")
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
                Email.Subject = "Delivery Invoice " & setVoucherNo & ""
                Email.Body = "Delivery Invoice " _
                & " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Previouse_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
                Email.Status = "Pending"
                Call New MailSentDAL().Add(Email)
            End If
        End If
        Return EmailSave

    End Function
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
    'Public Function GetDetail(ByVal Voucher_No As String) As Integer
    '    Try
    '        Dim str As String = "Select * From DeliveryChalanMasterTable WHERE DeliveryNo=N'" & Voucher_No & "'"
    '        Dim dt As DataTable = GetDataTable(str)
    '        Dim DeliveryId As Integer = 0
    '        If dt IsNot Nothing Then
    '            If dt.Rows.Count > 0 Then
    '                DeliveryId = dt.Rows(0).Item(0)
    '                Me.cmbCompany.SelectedValue = Convert.ToInt32(dt.Rows(0).Item("LocationId"))
    '            End If
    '        End If
    '        DisplayRecord("All")
    '        For i As Integer = 0 To Me.grdSaved.RowCount - 1
    '            If DeliveryId = Me.grdSaved.GetRows(i).Cells("DeliveryId").Value Then
    '                If Me.grdSaved.GetRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
    '                    If Not Me.grdSaved.RecordCount > 0 Then Exit Function
    '                    Me.IsEditMode = True
    '                    FillCombo("Vendor")
    '                    ' Me.FillCombo("SOComplete")
    '                    txtPONo.Text = grdSaved.GetRows(i).Cells(0).Value.ToString
    '                    dtpPODate.Value = CType(grdSaved.GetRows(i).Cells(1).Value, Date)
    '                    txtReceivingID.Text = grdSaved.GetRows(i).Cells("DeliveryId").Value
    '                    'TODO. ----
    '                    cmbVendor.Value = grdSaved.GetRows(i).Cells("CustomerCode").Value
    '                    cmbCompany.SelectedValue = grdSaved.GetRows(i).Cells("LocationId").Value
    '                    cmbCompany.Enabled = False
    '                    Me.chkPost.Checked = grdSaved.GetRows(i).Cells("Post").Value
    '                    txtRemarks.Text = grdSaved.GetRows(i).Cells("Remarks").Value & ""
    '                    txtPaid.Text = grdSaved.GetRows(i).Cells("CashPaid").Value & ""
    '                    Me.uitxtBiltyNo.Text = grdSaved.GetRows(i).Cells("BiltyNo").Value & ""
    '                    Me.cmbSalesMan.SelectedValue = grdSaved.GetRows(i).Cells("EmployeeCode").Value.ToString
    '                    Me.cmbPo.SelectedValue = Me.grdSaved.GetRows(i).Cells("PoId").Value
    '                    Me.cmbTransporter.SelectedValue = Me.grdSaved.GetRows(i).Cells("TransporterId").Value
    '                    Me.txtExpense.Text = Me.grdSaved.GetRows(i).Cells("Expense").Value.ToString
    '                    Me.txtFuel.Text = Me.grdSaved.GetRows(i).Cells("Fuel").Value.ToString
    '                    Try
    '                        Me.txtAdjustment.Text = Val(Me.grdSaved.GetRows(i).Cells("Adjustment").Value.ToString())
    '                    Catch ex As Exception
    '                    End Try
    '                    Call DisplayDetail(grdSaved.GetRows(i).Cells("DeliveryId").Value)
    '                    GetTotal()
    '                    Me.ExistingBalance = Val(Me.txtAmount.Text)
    '                    Me.BtnSave.Text = "&Update"
    '                    Me.LinkLabel1.Enabled = False
    '                    Me.cmbPo.Enabled = False
    '                    If Me.cmbPo.SelectedValue > 0 Then
    '                        Me.cmbVendor.Enabled = False
    '                    Else
    '                        Me.cmbVendor.Enabled = True
    '                    End If
    '                    'Customer Edit List flag here... 
    '                    If Me.cmbPo.SelectedIndex = 0 Then
    '                        If EditCustomerListOnDelivery = "False" Then
    '                            Me.cmbVendor.Enabled = True
    '                        Else
    '                            Me.cmbVendor.Enabled = False
    '                        End If
    '                    End If
    '                    If Not Mode = "Normal" Then Me.txtBarcode.Focus()
    '                    Me.GetSecurityRights()
    '                    Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
    '                    Me.CtrlGrdBar1_Load(Nothing, Nothing)
    '                    VoucherDetail(Me.txtPONo.Text)
    '                    Exit Function
    '                End If
    '            End If
    '        Next

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Public Sub Voucher_Delete()
        Dim lngVoucherId As Integer = GetVoucherId(Me.Name, grdSaved.CurrentRow.Cells(0).Value.ToString)
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
    Public Function Get_All(ByVal DeliveryNo As String)
        Try
            Get_All = Nothing
            If IsFormOpend = True Then
                If DeliveryNo.Length > 0 Then
                    Dim str As String = "Select * From DeliveryChalanMasterTable WHERE DeliveryNo=N'" & DeliveryNo & "'"
                    Dim dt As DataTable = GetDataTable(str)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then




                            Me.IsEditMode = True

                            FillCombo("Vendor")
                            Me.txtReceivingID.Text = dt.Rows(0).Item("DeliveryId")
                            Me.txtPONo.Text = dt.Rows(0).Item("DeliveryNo")
                            Me.dtpPODate.Value = dt.Rows(0).Item("DeliveryDate")
                            Me.cmbVendor.Value = dt.Rows(0).Item("CustomerCode")
                            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks")
                            'Rafay
                            Me.txtPO.Text = dt.Rows(0).Item("PO_NO")
                            Me.txtPaid.Text = dt.Rows(0).Item("CashPaid")
                            Me.cmbPo.SelectedValue = dt.Rows(0).Item("POId")
                            Me.cmbSalesMan.SelectedValue = dt.Rows(0).Item("EmployeeCode")
                            Me.chkPost.Checked = dt.Rows(0).Item("Post")
                            Me.cmbCompany.SelectedValue = dt.Rows(0).Item("LocationId")
                            Me.cmbTransporter.SelectedValue = dt.Rows(0).Item("TransporterId")
                            Me.uitxtBiltyNo.Text = dt.Rows(0).Item("BiltyNo")
                            'Me.txtExpense.Text = Convert.ToInt32(dt.Rows(0).Item("OtherExpense"))
                            'Me.txtFuel.Text = Convert.ToInt32(dt.Rows(0).Item("FuelExpense"))
                            Me.txtAdjustment.Text = Convert.ToInt32(dt.Rows(0).Item("Adjustment"))
                            DisplayDetail(dt.Rows(0).Item("DeliveryId"))
                            GetTotal()
                            'Me.ExistingBalance = Val(Me.txtAmount.Text)
                            Me.BtnSave.Text = "&Update"
                            'Me.LinkLabel1.Enabled = False
                            Me.cmbPo.Enabled = False
                            If Me.cmbPo.SelectedValue > 0 Then
                                Me.cmbVendor.Enabled = False
                            Else
                                Me.cmbVendor.Enabled = True
                            End If
                            'Customer Edit List flag here... 
                            If Me.cmbPo.SelectedIndex = 0 Then
                                If EditCustomerListOnDelivery = "False" Then
                                    Me.cmbVendor.Enabled = True
                                Else
                                    Me.cmbVendor.Enabled = False
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

                            If Not Mode = "Normal" Then Me.txtBarcode.Focus()
                            Me.GetSecurityRights()
                            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
                            Me.CtrlGrdBar1_Load(Nothing, Nothing)
                            ''Abubakar Siddiq :TFS3113 :Making Approval Button Enable in Edit Mode
                            Me.btnApprovalHistory.Visible = True
                            Me.btnApprovalHistory.Enabled = True
                            ''Abubakar Siddiq :TFS3113 :End
                            'VoucherDetail(dt.Rows(0).Item("DeliveryId"))
                            IsDrillDown = True
                            Me.cmbVendor.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)
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
        Try
            DisplayDetail(-1)
            DisplayRecord("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSearchDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDelete.Click
        Try
            DeleteToolStripButton_Click(Nothing, Nothing)


            '...................... End Send SMS ................
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintSelectedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            PrintToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintGatepToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            PrintGatePToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintListToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintListToolStripMenuItem1.Click
        Try
            PrintListToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetDeliveryOrderAnalysis() As Boolean
        Try
            IsDeliveryOrderAnalysis = Convert.ToBoolean(getConfigValueByType("SalesOrderAnalysis").ToString)
            If IsDeliveryOrderAnalysis = True Then
                Me.grd.RootTable.Columns(EnumGridDetail.TradePrice).Visible = False
                Me.grd.RootTable.Columns(EnumGridDetail.Freight).Visible = False
                Me.grd.RootTable.Columns(EnumGridDetail.MarketReturns).Visible = False
                Me.grd.RootTable.Columns(EnumGridDetail.NetBill).Visible = True
            Else
                Me.grd.RootTable.Columns(EnumGridDetail.TradePrice).Visible = False
                Me.grd.RootTable.Columns(EnumGridDetail.Freight).Visible = False
                Me.grd.RootTable.Columns(EnumGridDetail.MarketReturns).Visible = False
                Me.grd.RootTable.Columns(EnumGridDetail.NetBill).Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ExportFile(ByVal VoucherId As Integer)
        Try
            If IsEmailAlert = True Then
                If IsAttachmentFile = True Then
                    crpt = New ReportDocument
                    If IO.File.Exists(str_ApplicationStartUpPath & "\Reports\DeliveryInvoiceNew" & companyId & ".rpt") = False Then Exit Sub
                    crpt.Load(str_ApplicationStartUpPath & "\Reports\DeliveryInvoiceNew" & companyId & ".rpt", DBServerName)
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


                    If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments") Then
                        IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments")
                    Else
                    End If
                    FileName = String.Empty
                    FileName = "Delivery Invoice" & "-" & setVoucherNo & ""
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
                    crpt.SetParameterValue("@DeliveryID", VoucherId)
                    crpt.Export(crExportOps)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            ExportFile(getVoucher_Id)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BackgroundWorker3_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Try
            EmailSave()
        Catch ex As Exception
            ' Throw ex
        End Try
    End Sub
    Public Function GetAnalysisLastSchemeQty(ByVal CustomerCode As Integer, ByVal ItemId As Integer) As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select b.SchemeQty From SalesOrderDetailTable b INNER JOIN SalesOrderMasterTable a ON a.SalesOrderID = b.SalesOrderID WHERE DeliveryOrderDetailId In (Select Max(DeliveryOrderDetailId) From SalesOrderDetailTable WHERE (SchemeQty Is Not Null Or SchemeQty <> 0) Group By ArticleDefId) And a.VendorId=" & CustomerCode & " AND b.ArticleDefId=" & ItemId & ""
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
            str = "Select ISNULL(b.Discount_Percentage,0) as Discount_Percentage From SalesOrderDetailTable b INNER JOIN SalesOrderMasterTable a ON a.SalesOrderID = b.SalesOrderID WHERE SalesOrderDetailId In (Select Max(SalesOrderDetailId) From SalesOrderDetailTable WHERE (Discount_Percentage Is Not Null Or Discount_Percentage <> 0) Group By ArticleDefId) And a.VendorId=" & CustomerCode & " AND b.ArticleDefId=" & ItemId & ""
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
    'Private Sub txtTransitInsurancePercentage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Not IsFormOpend = True Then Exit Sub
    '        If Me.grd.RootTable Is Nothing Then Exit Sub
    '        Me.grd.UpdateData()
    '        If Val(Me.txtAmount.Text) <> 0 Then
    '            Me.txtAddTransitInsurance.Text = Math.Round(((Val(Me.txtTransitInsurancePercentage.Text) / 100) * (Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(EnumGridDetail.NetBill), Janus.Windows.GridEX.AggregateFunction.Sum)) + Val(Me.txtSEDTaxAmount.Text))), 2)
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub txtRemarks_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRemarks.Validating
        Try
            SpellChecker(Me.txtRemarks)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DeliveryChalanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeliveryChalanToolStripMenuItem.Click
        Dim IsPreviewDeliveryInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
        Dim newinvoice As Boolean = False
        Dim strCriteria As String = "Nothing"
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = grdSaved.GetRow.Cells("PurchaseReturnNo").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            newinvoice = getConfigValueByType("NewInvoice")
            If newinvoice = True Then
                str_ReportParam = "@DeliveryID|" & grdSaved.CurrentRow.Cells("DeliveryId").Value
            Else
                str_ReportParam = String.Empty
                strCriteria = "{DeliveryChalanDetailTable.DeliveryId} = " & grdSaved.CurrentRow.Cells("DeliveryId").Value
            End If
            If IsPreviewDeliveryInvoice = False Then
                ShowReport(IIf(newinvoice = False, "DeliveryChalan", "DeliveryChalanNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", True, , "New", , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            Else
                ShowReport(IIf(newinvoice = False, "DeliveryChalan", "DeliveryChalanNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", False, , "New", , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub DeliveryChalanToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeliveryChalanToolStripMenuItem1.Click
        Try
            DeliveryChalanToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetAdjustment(ByVal SalesOrderID As Integer)
        Try
            Me.grd.Update()
            Dim spadj As Double = 0D
            Dim adj As Double = 0D
            Dim dt As DataTable = GetDataTable("Select ISNULL(SpecialAdjustment,0) as SpecialAdjustment From SalesOrderMasterTable WHERE SalesOrderID=" & SalesOrderID)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    spadj = dt.Rows(0).Item(0)
                End If
            End If
            For i As Integer = 0 To Me.grd.RowCount - 1
                adj += (Me.grd.GetRows(i).Cells("Qty").Value * Me.grd.GetRows(i).Cells("CurrentPrice").Value) - (((Me.grd.GetRows(i).Cells("Qty").Value * Me.grd.GetRows(i).Cells("CurrentPrice").Value) * Me.grd.GetRows(i).Cells("Discount_Percentage").Value) / 100)
            Next
            adj = ((adj * spadj) / 100)
            Me.txtAdjustment.Text = adj
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetPrefix(ByVal CompanyId As Integer) As String
        Try
            GetPrefix = Nothing
            Dim str As String = "Select Prefix From CompanyDefTable WHERE CompanyId=" & CompanyId
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    GetPrefix = dt.Rows(0).Item(0).ToString
                End If
            End If

            If GetPrefix.ToString.Length > 1 Then
                Return GetPrefix
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub rbtAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAll.CheckedChanged, rbtCustomer.CheckedChanged
        Try
            If IsFormOpend = True Then FillCombo("Item")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CommercialBillToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim IsPreviewDeliveryInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
        Dim newinvoice As Boolean = False
        Dim strCriteria As String = "Nothing"
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = grdSaved.GetRow.Cells("PurchaseReturnNo").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            newinvoice = getConfigValueByType("NewInvoice")
            If newinvoice = True Then
                str_ReportParam = "@DeliveryID|" & grdSaved.CurrentRow.Cells("DeliveryId").Value
            Else
                str_ReportParam = String.Empty
                strCriteria = "{DeliveryChalanDetailTable.DeliveryId} = " & grdSaved.CurrentRow.Cells("DeliveryId").Value
            End If
            If IsPreviewDeliveryInvoice = False Then
                ShowReport(IIf(newinvoice = False, "CommercialInvoice", "CommercialInvoiceNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", True, , "New")
            Else
                ShowReport(IIf(newinvoice = False, "CommercialInvoice", "CommercialInvoiceNew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", False, , "New")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub CommercialInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            CommercialBillToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtTax_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTax.LostFocus
        Try
            If cmbUnit.SelectedIndex = 0 Then
                txtPackQty.Text = 1
                txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), TotalAmountRounding)
            Else
                txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), TotalAmountRounding)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetLastDiscount(ByVal CustomerCode As Integer, ByVal ArticleId As Integer) As Double
        Try

            Dim str As String = "Select ISNULL(Max(Discount_Percentage),0) From DeliveryChalanDetailTable INNER JOIN DeliveryChalanMasterTable On DeliveryChalanDetailTable.DeliveryId = DeliveryChalanDetailTable.DeliveryId WHERE CustomerCode=" & CustomerCode & " AND ArticleDefId=" & ArticleId & ""
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
    Private Sub txtFuel_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If IsFormOpend = True Then GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub txtExpense_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExpense.LostFocus
    '    Try
    '        If IsFormOpend = True Then GetTotal()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            If Me.cmbPo.SelectedIndex <= 0 Then Exit Sub
            Dim ii As Integer = 0
            For ii = 0 To grd.RowCount - 1
                If Me.cmbPo.SelectedValue = Val(grd.GetRows(ii).Cells(EnumGridDetail.SO_ID).Value.ToString) Then
                    msg_Error("This SO has been already loaded.")
                    Exit Sub
                End If
            Next
            Me.txtRemarks.Text = GetDeliveryRemarks(Me.cmbPo.SelectedValue)

            ''TASK TFS1764
            Me.cmbTransporter.SelectedValue = IIf(CType(cmbPo.SelectedItem, DataRowView).Item("TransporterId") > 0, CType(cmbPo.SelectedItem, DataRowView).Item("TransporterId"), Me.cmbTransporter.SelectedValue)
            ''END TASK TFS1764
            ''Strat TFS4339
            Me.cmbSalesMan.SelectedValue = IIf(Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("SOP_ID").ToString) > 0, Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("SOP_ID").ToString), Me.cmbSalesMan.SelectedValue)
            ''End TFS4339
            Me.DisplayPODetail(Me.cmbPo.SelectedValue)
            'If Me.cmbPo.SelectedIndex > 0 Then
            '    Dim adp As New OleDbDataAdapter
            '    Dim dt As New DataTable
            '    Dim Sql As String = "Select * from SalesOrderMasterTable where SalesOrderID=" & Me.cmbPo.SelectedValue
            '    adp = New OleDbDataAdapter(Sql, Con)
            '    adp.Fill(dt)
            '    'TODO -----
            '    Me.cmbVendor.Value = dt.Rows(0).Item("VendorId")
            '    Me.cmbVendor.Enabled = False

            'Else
            '    Me.cmbVendor.Enabled = True
            '    If Me.cmbVendor.Rows.Count > 0 Then Me.cmbVendor.Rows(0).Activate()
            'End If
            Me.GetTotal()
            GetAdjustment(Me.cmbPo.SelectedValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtExpense_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If IsFormOpend = True Then GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

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

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown

        If e.KeyCode = Keys.F1 Then
            If Val(grd.CurrentRow.Cells("ArticleDefId").Value) > 0 Then
                ''Dim frmRelItems As New frmRelatedItems()
                'frmRelatedItems.formname = Me.Name
                'frmRelatedItems.ShowDialog()
                ''frmRelatedItems.formname = "frmReceivingNote"
                Dim frmRelItems As New frmRelatedItems(Me.Name)
                frmRelItems.ShowDialog()
            End If
        End If
        ''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
        'If e.KeyCode = Keys.Delete Then
        '    DeleteToolStripButton_Click(BtnDelete, Nothing)
        '    Exit Sub
        'End If
    End Sub
    ''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
    'If e.KeyCode = Keys.Delete Then
    '    DeleteToolStripButton_Click(BtnDelete, Nothing)
    '    Exit Sub
    'End If
    'Task:2415 Add New Fields Engine No and Chassis No In Sales Module
    Public Function CheckDuplicateEngineNo() As Boolean
        Try
            If Me.grd.RowCount = 0 Then Return False
            For i As Int32 = 0 To Me.grd.RowCount - 1
                For j As Int32 = i + 1 To Me.grd.RowCount - 1
                    If Me.grd.GetRows(j).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
                        If Me.grd.GetRows(j).Cells(EnumGridDetail.Engine_No).Value.ToString = Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString Then
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
                    If Me.grd.GetRows(j).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
                        If Me.grd.GetRows(j).Cells(EnumGridDetail.Chassis_No).Value.ToString = Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString Then
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
            str.Add("@DetailInformation")
            str.Add("@BiltyNo")
            str.Add("@DriverName")
            str.Add("@VehicleNo")
            str.Add("@Transporter")
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
            str.Add("Delivery Chalan")
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

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Private Sub PrintAttachmentDeliveryChalanToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintAttachmentDeliveryChalanToolStripMenuItem1.Click
        Try
            PrintAttachmentDeliveryChalanToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetVoucherRecord() As DataSet
        Try

            Dim strSQL As String = String.Empty
            Dim ds As New dsVoucherDocumentAttachment
            ds.Tables.Clear()
            strSQL = "SP_DeliveryChalan " & Val(Me.grdSaved.GetRow.Cells("DeliveryId").Value.ToString) & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            ds.Tables.Add(dt)
            ds.Tables(0).TableName = "dtDeliveryChalan"


            strSQL = String.Empty
            strSQL = "Select DocId,FileName,Path,Convert(Image,'') as Attachment_Image From DocumentAttachment WHERE (DocId=" & Me.grdSaved.GetRow.Cells("DeliveryId").Value & ") AND Source=N'" & Me.Name & "'"
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
                        Dim New_Files As String = intId & "_" & DocId & "_DC" & Me.cmbCompany.SelectedValue & "_" & Me.dtpPODate.Value.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
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
    Private Sub PrintAttachmentDeliveryChalanToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintAttachmentDeliveryChalanToolStripMenuItem.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            'AddRptParam("Pm-dtVoucher.Voucher_Id", Me.grdVouchers.GetRow.Cells(0).Value)
            DataSetShowReport("RptSaleInvoiceDocument", GetVoucherRecord())
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnAttachment_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAttachment.ButtonClick

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
    Private Sub DOPrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DOPrintToolStripMenuItem.Click
        Dim IsPreviewDeliveryInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
        Dim newinvoice As Boolean = False
        Dim strCriteria As String = "Nothing"
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = grdSaved.GetRow.Cells("PurchaseReturnNo").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            newinvoice = getConfigValueByType("NewInvoice")
            If newinvoice = True Then
                str_ReportParam = "@DeliveryID|" & grdSaved.CurrentRow.Cells("DeliveryId").Value
            Else
                str_ReportParam = String.Empty
                strCriteria = "{DeliveryChalanDetailTable.DeliveryId} = " & grdSaved.CurrentRow.Cells("DeliveryId").Value
            End If
            If IsPreviewDeliveryInvoice = False Then
                ShowReport(IIf(newinvoice = False, "rptDO", "rptDONew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", True, , "New", , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            Else
                ShowReport(IIf(newinvoice = False, "rptDO", "rptDONew") & grdSaved.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", False, , "New", , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub DOPrintToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DOPrintToolStripMenuItem1.Click
        Try
            DOPrintToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''Task# A1-10-06-2015 Added Key Pres event for some textboxes to take just numeric and dot value
    Private Sub txtNUM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPackRate.KeyPress, txtPackQty.KeyPress, txtQty.KeyPress, txtRate.KeyPress, txtStock.KeyPress, txtRate.KeyPress, txtTotal.KeyPress, txtDisc.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End Task# A1-10-06-2015

    Private Sub DeliveryChalanItemSpecificationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeliveryChalanItemSpecificationToolStripMenuItem.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@DeliveryChalanId", Val(Me.grdSaved.GetRow.Cells("DeliveryId").Value.ToString))
            ShowReport("DeliveryChalanItemSpecification")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DeliveryChalanItemSpecificationToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeliveryChalanItemSpecificationToolStripMenuItem1.Click
        Try
            DeliveryChalanItemSpecificationToolStripMenuItem_Click(Nothing, Nothing)
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
            ''Start TFS3330 : These Lines Are commented Aganist TFS3330
            'If Val(Me.txtTotalQuantity.Text) <> 0 AndAlso Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtRate.Text) = 0 Then
            '    Me.txtRate.Text = Math.Round(Val(Me.txtTotal.Text) / Val(Me.txtTotalQuantity.Text), TotalAmountRounding)
            'End If
            ''End TFS3330
            If Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtTotalQuantity.Text) = 0 Then
                If Not Me.cmbUnit.Text <> "Loose" Then
                    ' ''Start TFS2825
                    'If flgLoadItemAfterDeliveredOnDC Then
                    '    If SOQty = 0 Then
                    '        Me.txtQty.Text = Val(SOQty)
                    '    Else
                    Me.txtQty.Text = Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)
                    'End If
                    'End If
                    ''End TFS2825
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



            'Dim dblTaxPercent As Double = 0D
            'Double.TryParse(Me.txtTax.Text, dblTaxPercent)
            'Me.txtNetTotal.Text = (((dblTaxPercent / 100) * Val(Me.txtTotal.Text)) + Val(Me.txtTotal.Text))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Public Sub GetGridDetailTotal()
    '    Try
    '        Me.grd.UpdateData()
    '        Me.grd.EditMode = Janus.Windows.GridEX.EditMode.EditOn
    '        If Val(grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) = 0 Then
    '            'grd.GetRow.Cells(EnumGridDetail.Total).Value = Math.Round((Val(grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString) * Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString)), DecimalPointInValue)
    '        End If
    '        If Val(grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString) = 0 Then
    '            Me.txtRate.Text = Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) / Val(grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString)
    '        End If
    '        If Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString) = 0 Then
    '            If Not Me.cmbUnit.Text <> "Loose" Then
    '                grd.GetRow.Cells(EnumGridDetail.Qty).Value = Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) / Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString)
    '                grd.GetRow.Cells(EnumGridDetail.TotalQty).Value = Val(grd.GetRow.Cells(EnumGridDetail.Qty).Value.ToString)
    '            Else
    '                If Val(grd.GetRow.Cells(EnumGridDetail.PackQty).Value.ToString) > 0 Then
    '                    grd.GetRow.Cells(EnumGridDetail.Qty).Value = (Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) / Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString)) / Val(grd.GetRow.Cells(EnumGridDetail.PackQty).Value.ToString)
    '                    grd.GetRow.Cells(EnumGridDetail.TotalQty).Value = (Val(grd.GetRow.Cells(EnumGridDetail.Qty).Value.ToString) * Val(grd.GetRow.Cells(EnumGridDetail.PackQty).Value.ToString))
    '                Else
    '                    grd.GetRow.Cells(EnumGridDetail.Qty).Value = Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) / Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString)
    '                    grd.GetRow.Cells(EnumGridDetail.TotalQty).Value = Val(grd.GetRow.Cells(EnumGridDetail.Qty).Value.ToString)
    '                End If
    '            End If
    '        End If

    '        Me.grd.EditMode = Janus.Windows.GridEX.EditMode.EditOff

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Task#25082015 for numbers only by Ahmad Sharif
    Private Sub txtTotalQuantity_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalQuantity.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtTotalQuantity_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalQuantity.LostFocus
        Try
            GetTotal()
            If Not Val(Me.txtPackQty.Text) > 0 Then
                Me.txtQty.Text = Val(Me.txtTotalQuantity.Text)
            End If
            GetDetailTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#25082015
    Private Sub txtTotalQuantity_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalQuantity.TextChanged
        Try
            GetTotal()
            If Not Val(Me.txtPackQty.Text) > 0 Then
                Me.txtQty.Text = Val(Me.txtTotalQuantity.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetGridDetailQtyCalculate(ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            Me.grd.UpdateData()
            If e.Column.Index = EnumGridDetail.Qty Or e.Column.Index = EnumGridDetail.PackQty Then
                If Val(Me.grd.GetRow.Cells(EnumGridDetail.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(EnumGridDetail.TotalQty).Value = (Val(Me.grd.GetRow.Cells(EnumGridDetail.PackQty).Value.ToString) * Val(Me.grd.GetRow.Cells(EnumGridDetail.Qty).Value.ToString))
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                Else
                    Me.grd.GetRow.Cells(EnumGridDetail.TotalQty).Value = Val(Me.grd.GetRow.Cells(EnumGridDetail.Qty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                End If
            ElseIf e.Column.Index = EnumGridDetail.TotalQty Then
                If Not Val(Me.grd.GetRow.Cells(EnumGridDetail.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(EnumGridDetail.Qty).Value = Val(Me.grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString)
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
            If Val(grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) = 0 Then
                'grd.GetRow.Cells(EnumGridDetail.Total).Value = Math.Round((Val(grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString) * Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString)), DecimalPointInValue)
            End If

            If Val(grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString) = 0 Then
                Me.txtRate.Text = Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) / Val(grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString)
            End If

            If Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) <> 0 AndAlso Val(grd.GetRow.Cells(EnumGridDetail.TotalQty).Value.ToString) = 0 Then
                If Not Me.cmbUnit.Text <> "Loose" Then
                    grd.GetRow.Cells(EnumGridDetail.Qty).Value = Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) / Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString)
                    grd.GetRow.Cells(EnumGridDetail.TotalQty).Value = Val(grd.GetRow.Cells(EnumGridDetail.Qty).Value.ToString)
                Else
                    If Val(grd.GetRow.Cells(EnumGridDetail.PackQty).Value.ToString) > 0 Then
                        grd.GetRow.Cells(EnumGridDetail.Qty).Value = (Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) / Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString)) / Val(grd.GetRow.Cells(EnumGridDetail.PackQty).Value.ToString)
                        grd.GetRow.Cells(EnumGridDetail.TotalQty).Value = (Val(grd.GetRow.Cells(EnumGridDetail.Qty).Value.ToString) * Val(grd.GetRow.Cells(EnumGridDetail.PackQty).Value.ToString))
                    Else
                        grd.GetRow.Cells(EnumGridDetail.Qty).Value = Val(grd.GetRow.Cells(EnumGridDetail.Total).Value.ToString) / Val(grd.GetRow.Cells(EnumGridDetail.Price).Value.ToString)
                        grd.GetRow.Cells(EnumGridDetail.TotalQty).Value = Val(grd.GetRow.Cells(EnumGridDetail.Qty).Value.ToString)
                    End If
                End If
            End If

            Me.grd.EditMode = Janus.Windows.GridEX.EditMode.EditOff

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        Try
            'If Val(Me.txtPackQty.Text) > 0 Then
            '    Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            'Else
            '    Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
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

    Private Sub grd_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited
        Try

            If flgVehicleIdentificationInfo = True Then
                Select Case e.Column.Key
                    Case "Qty"
                        Dim str As String
                        str = "SELECT Engine_No, Chassis_No, Sum(InQty) InQty, Sum(OutQty) OutQty, Sum(InQty)-Sum(OutQty) Qty FROM StockDetailTable WHERE Engine_No <> '' And ArticleDefId = " & Val(Me.grd.GetRow.Cells("ArticleDefId").Value.ToString) & " GROUP BY Engine_No, Chassis_No HAVING Sum(InQty)-Sum(OutQty) > 0"
                        Dim dt As DataTable = GetDataTable(str)
                        If dt.Rows.Count > 0 Then
                            If Val(grd.GetRow.Cells("TotalQty").Value) > dt.Rows(0).Item("Qty") Then
                                ShowErrorMessage("This Qty not exists in Stock ")
                                grd.CancelCurrentEdit()
                                Exit Sub
                            End If
                        End If
                End Select
            End If

            If Not IsDBNull(Me.grd.GetRow.Cells(EnumGridDetail.BatchNo).Value) Then
                Dim str As String = String.Empty
                str = " Select   ExpiryDate, Origin  From  StockDetailTable  where BatchNo not in ('','0','xxxx') And BatchNo ='" & Me.grd.GetRow.Cells(EnumGridDetail.BatchNo).Value.ToString & "'" _
                     & " And ArticledefId = " & Me.grd.GetRow.Cells(EnumGridDetail.ArticleID).Value & "  And LocationId = " & Val(Me.grd.GetRow.Cells(EnumGridDetail.LocationID).Value.ToString) & "  And (isnull(InQty, 0) - isnull(OutQty, 0)) > 0 Group by BatchNo,ExpiryDate,Origin ORDER BY ExpiryDate  Asc "
                Dim dtExpiry As DataTable = GetDataTable(str)
                If dtExpiry.Rows.Count > 0 Then
                    If IsDBNull(dtExpiry.Rows(0).Item("ExpiryDate")) = False Then
                        grd.GetRow.Cells(EnumGridDetail.ExpiryDate).Value = CType(dtExpiry.Rows(0).Item("ExpiryDate").ToString, Date)
                        grd.GetRow.Cells("Origin").Value = dtExpiry.Rows(0).Item("Origin").ToString
                    End If
                End If
            End If

            Dim Gross_weight As Decimal = IIf(grd.GetRow.Cells(EnumGridDetail.Gross_Weights).Value Is Nothing, 0, grd.GetRow.Cells(EnumGridDetail.Gross_Weights).Value)
            Dim Tray_weight As Decimal = IIf(grd.GetRow.Cells(EnumGridDetail.Tray_Weights).Value Is Nothing, 0, grd.GetRow.Cells(EnumGridDetail.Tray_Weights).Value)

            If Gross_weight <> 0 AndAlso Tray_weight <> 0 Then
                grd.GetRow.Cells(EnumGridDetail.Net_Weight).Value = Gross_weight - Tray_weight
                grd.GetRow.Cells(EnumGridDetail.TotalQty).Value = Gross_weight - Tray_weight
                grd.GetRow.Cells(EnumGridDetail.Qty).Value = Gross_weight - Tray_weight

            End If
            Dim CostPrice As Double = 0D
            Dim dblPurchasePrice As Double = 0D
            Dim dblCostPrice As Double = 0D

            Dim strPriceData() As String = GetRateByItem(Val(Me.grd.GetRow.Cells(EnumGridDetail.ArticleID).Value.ToString)).Split(",")

            If strPriceData.Length > 1 Then
                dblCostPrice = Val(strPriceData(0).ToString)
                dblPurchasePrice = Val(strPriceData(1).ToString)
                If dblCostPrice = 0 Then
                    dblCostPrice = dblPurchasePrice
                End If
            End If

            If flgAvgRate = True And getConfigValueByType("CostImplementationLotWiseOnStockMovement") = "True" Then
                If Convert.ToDouble(GetItemRateByBatch(Val(Me.grd.GetRow.Cells(EnumGridDetail.ArticleID).Value.ToString), Me.grd.GetRow.Cells(EnumGridDetail.BatchNo).Value.ToString)) > 0 Then
                    Me.grd.GetRow.Cells("CostPrice").Value = Convert.ToDouble(GetItemRateByBatch(Val(Me.grd.GetRow.Cells(EnumGridDetail.ArticleID).Value.ToString), Me.grd.GetRow.Cells(EnumGridDetail.BatchNo).Value.ToString))
                Else
                    Me.grd.GetRow.Cells("CostPrice").Value = dblPurchasePrice
                End If
            ElseIf flgAvgRate = True Then

                If dblCostPrice > 0 Then
                    Me.grd.GetRow.Cells("CostPrice").Value = dblCostPrice
                Else
                    Me.grd.GetRow.Cells("CostPrice").Value = dblPurchasePrice
                End If
            Else
                'CostPrice = IIf(Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString) <= 0, Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString), Val(Me.grd.GetRows(i).Cells("CostPrice").Value.ToString))
                Me.grd.GetRow.Cells("CostPrice").Value = dblCostPrice 'Val(Me.grd.GetRows(i).Cells("PurchasePrice").Value.ToString)
                'End Task:2517
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    'Private Sub PreventDuplicateEngineNoAndChasisNo()
    '    If flgVehicleIdentificationInfo = True Then

    '        ''Retrieving Engine No
    '        objCommand.CommandText = ""
    '        objCommand.CommandText = "SELECT dbo.DeliveryChalanDetailTable.ArticleDefId, DeliveryChalanDetailTable.Engine_No, DeliveryChalanDetailTable.Chassis_No  " _
    '                                        & " FROM dbo.ArticleDefTable INNER JOIN " _
    '                                        & " dbo.DeliveryChalanDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.DeliveryChalanDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"
    '        If Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
    '            objCommand.CommandText += " AND DeliveryChalanDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "'"
    '        End If
    '        'If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
    '        '    objCommand.CommandText += " AND DeliveryChalanDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
    '        'End If
    '        Dim dtVehicleEngineNoIdentificationInfo As New DataTable
    '        Dim daVehicleEngineNoIdentificationInfo As New OleDbDataAdapter(objCommand)
    '        daVehicleEngineNoIdentificationInfo.Fill(dtVehicleEngineNoIdentificationInfo)

    '        ''Retrieving Chasis No
    '        objCommand.CommandText = ""
    '        objCommand.CommandText = "SELECT dbo.DeliveryChalanDetailTable.ArticleDefId, DeliveryChalanDetailTable.Engine_No, DeliveryChalanDetailTable.Chassis_No  " _
    '                                        & " FROM dbo.ArticleDefTable INNER JOIN " _
    '                                        & " dbo.DeliveryChalanDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.DeliveryChalanDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"
    '        'If Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
    '        '    objCommand.CommandText += " AND DeliveryChalanDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "'"
    '        'End If
    '        If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
    '            objCommand.CommandText += " AND DeliveryChalanDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
    '        End If
    '        Dim dtVehicleChasisNoIdentificationInfo As New DataTable
    '        Dim daVehicleChasisNoIdentificationInfo As New OleDbDataAdapter(objCommand)
    '        daVehicleChasisNoIdentificationInfo.Fill(dtVehicleChasisNoIdentificationInfo)

    '        'Task:2606 Engine_No and Chassis No Data From Sales Return
    '        ''Retrieving Return Engine No
    '        objCommand.CommandText = ""
    '        objCommand.CommandText = "SELECT dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Engine_No, SalesReturnDetailTable.Chassis_No  " _
    '                                        & " FROM dbo.ArticleDefTable INNER JOIN " _
    '                                        & " dbo.SalesReturnDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesReturnDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"

    '        objCommand.CommandText += " AND SalesReturnDetailTable.Engine_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "'"


    '        'If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
    '        '    objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
    '        'End If
    '        Dim dtSalesReturnVehichleEngineNoInfo As New DataTable
    '        Dim daSalesReturnVehichleEngineNoInfo As New OleDbDataAdapter(objCommand)
    '        daSalesReturnVehichleEngineNoInfo.Fill(dtSalesReturnVehichleEngineNoInfo)

    '        ''Retrieving Return Chasis No
    '        objCommand.CommandText = ""
    '        objCommand.CommandText = "SELECT dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Engine_No, SalesReturnDetailTable.Chassis_No  " _
    '                                        & " FROM dbo.ArticleDefTable INNER JOIN " _
    '                                        & " dbo.SalesReturnDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.SalesReturnDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId <> 0"

    '        objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"


    '        'If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
    '        '    objCommand.CommandText += " AND SalesReturnDetailTable.Chassis_No=N'" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "'"
    '        'End If
    '        Dim dtSalesReturnVehichleChasisNoInfo As New DataTable
    '        Dim daSalesReturnVehichleChasisNoInfo As New OleDbDataAdapter(objCommand)
    '        daSalesReturnVehichleChasisNoInfo.Fill(dtSalesReturnVehichleChasisNoInfo)

    '        If dtVehicleEngineNoIdentificationInfo IsNot Nothing Then
    '            If dtVehicleEngineNoIdentificationInfo.Rows.Count > 0 Then
    '                If dtVehicleEngineNoIdentificationInfo.Rows.Count > dtSalesReturnVehichleEngineNoInfo.Rows.Count Then
    '                    If dtVehicleEngineNoIdentificationInfo.Rows(0).Item("Engine_No").ToString.Length > 0 Or Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString.Length > 0 Then
    '                        If Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString = dtVehicleEngineNoIdentificationInfo.Rows(0).Item("Engine_No").ToString Then
    '                            Throw New Exception("Engine no [" & Me.grd.GetRows(i).Cells(EnumGridDetail.Engine_No).Value.ToString & "] already exists")
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If

    '        If dtVehicleChasisNoIdentificationInfo IsNot Nothing Then
    '            If dtVehicleChasisNoIdentificationInfo.Rows.Count > 0 Then
    '                If dtVehicleChasisNoIdentificationInfo.Rows.Count > dtSalesReturnVehichleChasisNoInfo.Rows.Count Then
    '                    If dtVehicleChasisNoIdentificationInfo.Rows(0).Item("Chassis_No").ToString.Length > 0 Or Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString.Length > 0 Then
    '                        If Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString = dtVehicleChasisNoIdentificationInfo.Rows(0).Item("Chassis_No").ToString Then
    '                            Throw New Exception("Chassis no [" & Me.grd.GetRows(i).Cells(EnumGridDetail.Chassis_No).Value.ToString & "] already exists")
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    'End Sub

    Private Sub cmbItem_FilterCellValueChanged(sender As Object, e As Win.UltraWinGrid.FilterCellValueChangedEventArgs) Handles cmbItem.FilterCellValueChanged

    End Sub
    'Private Sub AllowLoadPartialPO()
    '    Dim IsMinus As Boolean = True
    '    'If CType(Me.cmbItem.SelectedRow, Infragistics.Win.UltraWinGrid.UltraGridRow).Cells("ServiceItem").Value = False Then
    '    IsMinus = getConfigValueByType("AllowMinusStock")
    '    'End If
    '    If IsMinus = False Then
    '        If objDataSet.Tables(0).Rows.Count > 0 Then
    '            For Each dr As DataRow In objDataSet.Tables(0).Rows
    '                dr.BeginEdit()
    '                AvailableStock = Convert.ToDouble(GetStockById(dr.Item("ArticleId"), dr.Item("LocationId"), IIf(dr.Item("unit").ToString = "Loose", "Loose", "Pack")))
    '                If dr.Item("unit").ToString = "Loose" Then
    '                    If Val(dr.Item("TotalQty")) > AvailableStock Then
    '                        If msg_Confirm("Stock is not enough. Do you want to load available Qty in stock as partial Qty?") = False Then
    '                            Exit Sub
    '                        Else
    '                            StockChecked = True
    '                            dr.Item("TotalQty") = AvailableStock
    '                        End If
    '                    End If
    '                Else
    '                    If Val(dr.Item("Qty")) > AvailableStock Then
    '                        If msg_Confirm("Stock is not enough. Do you want to load available Qty in stock as partial Qty?") = False Then
    '                            Exit Sub
    '                        Else
    '                            StockChecked = True
    '                            dr.Item("Qty") = AvailableStock
    '                        End If
    '                    End If

    '                End If
    '                dr.EndEdit()
    '                AvailableStock = 0
    '            Next
    '        End If
    '    End If
    'End Sub

    Private Function GetSingle(ByVal DeliveryChalanId As Integer)
        Dim Str As String = String.Empty
        Try
            Str = "SELECT Recv.DeliveryNo, Recv.DeliveryDate, V.SalesOrderNo, V.SalesOrderDate, vwCOADetail.detail_title as CustomerName,  Recv.DeliveryQty, Recv.DeliveryAmount, Recv.DeliveryId, Recv.Arrival_Time , Recv.Departure_Time,  " & _
                          "Recv.CustomerCode, tbldefEmployee.Employee_Name, Recv.Remarks,PO_NO, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId, Recv.BiltyNo, isnull(Recv.TransporterId ,0) as TransporterId, Recv.LocationId, Recv.FuelExpense, Recv.OtherExpense, recv.Adjustment,  isnull(recv.TransitInsurance,0) as TransitInsurance, IsNull(Recv.Post,0) as Post, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end As Status, ISNULL(Recv.ServiceItemDelivery,0) as ServiceItemDelivery, Recv.DcDate, ISNULL(Recv.Delivered,0) as Delivered, vwCOADetail.Contact_Email as Email, Recv.Driver_Name, Recv.Vehicle_No, Recv.Other_Company, Recv.UserName as 'User Name', Recv.UpdateUserName " & _
                          "FROM DeliveryChalanMasterTable Recv INNER JOIN " & _
                          "vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
                          "tblDefEmployee ON Recv.EmployeeCode = tblDefEmployee.Employee_Id LEFT OUTER JOIN " & _
                          "SalesOrderMasterTable V ON Recv.POId = V.SalesOrderID  where recv.DeliveryId =" & DeliveryChalanId & ""
            Dim dt As DataTable = GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub PrintSelectiveRowsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintSelectiveRowsToolStripMenuItem.Click
        Try
            If Me.grd.GetCheckedRows.Length > 0 Then
                Dim DeliveryDetailIds As String = ""
                For Each dr As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
                    If DeliveryDetailIds.Length > 0 Then
                        DeliveryDetailIds += "," & dr.Cells("DeliveryDetailId").Value.ToString
                    Else
                        DeliveryDetailIds = dr.Cells("DeliveryDetailId").Value.ToString

                    End If
                Next
                'AddRptParam("@DeliveryID", grdSaved.CurrentRow.Cells("DeliveryId").Value)
                'AddRptParam("@DelliveryDetailIds", DeliveryDetailIds)
                Dim dt As DataTable = GetSelectedRows(grdSaved.CurrentRow.Cells("DeliveryId").Value, DeliveryDetailIds)
                ShowReport("rptSelectiveDeliveryChalan", , , , , , , dt, , , , , , , )
            Else
                msg_Error("No row is selected")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetSelectedRows(ByVal DeliveryId As Integer, ByVal DeliveryDetailIds As String)
        Dim Str As String = ""
        Try
            Str = " SELECT DeliverychalanMasterTable.DeliveryNo, DeliverychalanMasterTable.DeliveryDate, DeliverychalanDetailTable.Qty, DeliverychalanDetailTable.Price, " _
              & " ISNULL(DeliverychalanDetailTable.TaxPercent, 0) AS TaxPercent, ArticleDefTable.ArticleDescription, tblCustomer.Address, tblCustomer.Phone, tblListCity.CityName, " _
              & " DeliverychalanMasterTable.PreviousBalance, vwCOADetail.detail_title, DeliverychalanMasterTable.UserName, dbo.tblDefTransporter.TransporterName, " _
              & " DeliverychalanMasterTable.FuelExpense, DeliverychalanMasterTable.OtherExpense, tblCustomer.Comments, DeliverychalanMasterTable.Remarks,DeliverychalanMasterTable.PO_NO " _
              & " dbo.ArticleUnitDefTable.ArticleUnitName, DeliverychalanDetailTable.Sz7, DeliverychalanDetailTable.Sz1, dbo.ArticleSizeDefTable.ArticleSizeName, " _
              & " tblCustomer.CUSTOMERNTNNO, tblCustomer.CUSTOMERSALESTAXNO, ISNULL(DeliverychalanDetailTable.SEDPercent, 0) AS SED, ArticleDefTable.ArticleCode, " _
              & " dbo.ArticleColorDefTable.ArticleColorName, DeliverychalanMasterTable.POId, dbo.SalesOrderMasterTable.PONo, dbo.SalesOrderMasterTable.PO_Date " _
              & " FROM dbo.ArticleSizeDefTable RIGHT OUTER JOIN " _
              & " dbo.DeliveryChalanDetailTable AS DeliverychalanDetailTable INNER JOIN " _
              & " dbo.DeliveryChalanMasterTable AS DeliverychalanMasterTable ON DeliverychalanDetailTable.DeliveryId = DeliverychalanMasterTable.DeliveryId INNER JOIN " _
              & " dbo.ArticleDefTable AS ArticleDefTable ON DeliverychalanDetailTable.ArticleDefId = ArticleDefTable.ArticleId Left Outer Join " _
              & "  dbo.ArticleColorDefTable ON ArticleDefTable.ArticleColorId = dbo.ArticleColorDefTable.ArticleColorId LEFT OUTER JOIN " _
              & " dbo.SalesOrderMasterTable ON DeliverychalanMasterTable.POId = dbo.SalesOrderMasterTable.SalesOrderId ON " _
              & " dbo.ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId LEFT OUTER JOIN " _
              & " dbo.ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = dbo.ArticleUnitDefTable.ArticleUnitId LEFT OUTER JOIN " _
              & " dbo.tblDefTransporter ON DeliverychalanMasterTable.TransporterId = dbo.tblDefTransporter.TransporterId LEFT OUTER JOIN " _
              & " dbo.vwCOADetail AS vwCOADetail ON DeliverychalanMasterTable.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
              & " dbo.tblListTerritory AS tblListTerritory LEFT OUTER JOIN " _
              & " dbo.tblListCity AS tblListCity ON tblListTerritory.CityId = tblListCity.CityId Left Outer Join " _
              & " dbo.tblCustomer AS tblCustomer ON tblListTerritory.TerritoryId = tblCustomer.Territory ON vwCOADetail.coa_detail_id = tblCustomer.AccountId " _
              & " Where DeliverychalanDetailTable.DeliveryId = " & DeliveryId & " And DeliverychalanDetailTable.DeliveryDetailId IN(" & DeliveryDetailIds & " ) " _
              & " order by DeliverychalanDetailTable.deliverydetailid "
            Dim dt As DataTable = GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
        Try
            If IsSOLoaded = True Then Exit Sub
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                If IsEditMode = True Then
                Else
                    Me.txtCurrencyRate.Text = Math.Round(Convert.ToDouble(dr.Row.Item("CurrencyRate").ToString), 4)
                End If

                ' R@!    11-Jun-2016 Dollor account
                ' Setting default rate to zero
                If Val(Me.txtCurrencyRate.Text) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If

                'R@!    11-Jun-2016 Dollor account
                'Code Commented
                ' Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
                ' Added 2 coloumns and changed caption


                ''TASK TFS3781
                If CurrencyRate > 1 Then
                    Me.txtCurrencyRate.Text = CurrencyRate
                End If
                ''
                ''
                ''

                If Me.grd.DataSource Is Nothing Then Exit Sub
                Me.grd.RootTable.Columns(EnumGridDetail.CurrencyAmount).Caption = "Amount (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns(EnumGridDetail.CurrencyRate).Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"

                'Me.grd.RootTable.Columns(EnumGridDetail.TotalCurrencyAmount).Caption = "Total Amount (" & Me.cmbCurrency.Text & ")"
                'Me.grd.RootTable.Columns(EnumGridDetail.CurrencySalesTaxAmount).Caption = "Tax Amount (" & Me.cmbCurrency.Text & ")"
                'Me.grd.RootTable.Columns(EnumGridDetail.CurrencySEDAmount).Caption = "SED Amount (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns("NetBill").Caption = "Net Amount (" & Me.BaseCurrencyName & ")"
                'Me.grd.RootTable.Columns("Total Amount").Caption = "Total Amount (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns("Total").Caption = "Total (" & Me.BaseCurrencyName & ")"

                'grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(EnumGridDetail.CurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(EnumGridDetail.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(EnumGridDetail.CurrencyRate).Visible = False
                    'Me.grd.RootTable.Columns(EnumGridDetail.TotalCurrencyAmount).Visible = False
                    'Me.grd.RootTable.Columns(EnumGridDetail.CurrencySalesTaxAmount).Visible = False
                    'Me.grd.RootTable.Columns(EnumGridDetail.CurrencySEDAmount).Visible = False

                    'Me.grd.RootTable.Columns(EnumGrid.Credit).Visible = False
                Else
                    Me.grd.RootTable.Columns(EnumGridDetail.CurrencyAmount).Visible = True
                    'Me.grd.RootTable.Columns(GrdEnum.BaseCurrencyRate).Visible = True
                    Me.grd.RootTable.Columns(EnumGridDetail.CurrencyRate).Visible = True
                    'Me.grd.RootTable.Columns(EnumGridDetail.TotalCurrencyAmount).Visible = True
                    'Me.grd.RootTable.Columns(EnumGridDetail.CurrencySalesTaxAmount).Visible = False
                    'Me.grd.RootTable.Columns(EnumGridDetail.CurrencySEDAmount).Visible = False
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

    Private Sub cmbItem_InitializeLayout(sender As Object, e As Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbItem.InitializeLayout
        Try
            Dim Layout As UltraGridLayout = e.Layout
            Dim ov As UltraGridOverride = Layout.Override
            ov.HeaderClickAction = HeaderClickAction.SortMulti
            ov.AllowRowFiltering = DefaultableBoolean.True
            'ov.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Equals
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_TextChanged(sender As Object, e As EventArgs) Handles cmbItem.TextChanged
        Try
            Me.cmbItem.DisplayLayout.Bands(0).ColumnFilters.ClearAllFilters()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnJobCard_Click(sender As Object, e As EventArgs) Handles btnJobCard.Click
        Try
            Dim frmJobCards As New frmLoadJobCards
            frmJobCards.IsDeliveryChalan = True
            frmJobCards.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetJobCard(ByVal JobCardId As Integer) As DataTable
        Dim dtJobCard As DataTable
        Try
            Dim strJobCard As String = " SELECT Card.JobCardID, Card.JobCardNo, Card.JobCardDate, tblVehicleInfo.RegistrationNo, Card.LiftID, tblDefCostCenter.Name AS LiftName, tblDefModelList.Name As Model, ArticleColorDefTable.ArticleColorName As Color, tblVehicleInfo.EngineNo, tblVehicleInfo.ChessisNo, tblVehicleInfo.DOP, " _
           & " tblCompanyContacts.ContactName, TblCompanyContacts.Mobile, Card.Remarks, TblCompanyContacts.Address " _
           & " FROM tblJobCard AS Card INNER JOIN " _
           & " tblVehicleInfo ON Card.VehicleID = tblVehicleInfo.VahicleID INNER JOIN " _
           & " tblCompanyContacts ON tblVehicleInfo.CompanyContactID = TblCompanyContacts.PK_Id LEFT JOIN " _
           & " tblDefModelList ON tblVehicleInfo.ModelID = tblDefModelList.ModelId LEFT JOIN " _
           & " ArticleColorDefTable ON tblVehicleInfo.ColorID = ArticleColorDefTable.ArticleColorId LEFT OUTER JOIN " _
           & " tblDefCostCenter ON Card.LiftID = tblDefCostCenter.CostCenterID Where JobCardId =" & JobCardId & " "
            dtJobCard = GetDataTable(strJobCard)
            If dtJobCard.Rows.Count > 0 Then
                Me.txtJobCardId.Text = Val(dtJobCard.Rows(0).Item("JobCardID").ToString)
                Me.txtJobCardNo.Text = dtJobCard.Rows(0).Item("JobCardNo").ToString
                Me.dtpJobCardDate.Value = dtJobCard.Rows(0).Item("JobCardDate")
                Me.txtLeft.Text = dtJobCard.Rows(0).Item("LiftName").ToString
                Me.txtJobCardCustomer.Text = dtJobCard.Rows(0).Item("ContactName").ToString
                Me.txtVehicle.Text = dtJobCard.Rows(0).Item("Model").ToString
                Me.txtRegistrationNo.Text = dtJobCard.Rows(0).Item("RegistrationNo").ToString
            End If
            Return dtJobCard
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    '''  ''This Sub is Added to change/Lock PDP when Pack Rate is entered 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Ayesha Rehman : 23-05-2018 : TFS3330</remarks>
    Private Sub txtPackRate_Leave(sender As Object, e As EventArgs) Handles txtPackRate.Leave
        Try

            If Val(Me.txtPackRate.Text) > 0 Then
                If getConfigValueByType("Apply40KgRate").ToString = "True" AndAlso Me.cmbUnit.Text <> "Loose" Then
                    'Me.txtRate.Text = (Val(Me.txtPackRate.Text) / 40) ''Commented Aginst TFS3330
                    Me.txtPDP.Text = Math.Round((Val(Me.txtPackRate.Text) / 40), TotalAmountRounding)
                    Me.txtPDP.Enabled = False
                ElseIf Me.cmbUnit.Text <> "Loose" Then
                    'Me.txtRate.Text = (Val(Me.txtPackRate.Text) / Val(Me.txtPackQty.Text)) ''Commented Aginst TFS3330
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
    Private Sub txtPackRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPackRate.LostFocus
        Try
            If Val(Me.txtPackQty.Text) = 0 Then
                txtPackQty.Text = 1
                DiscountCalculation() ''TFS2827
                txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), TotalAmountRounding)
            Else
                DiscountCalculation() ''TFS2827
                txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTax.Text)) / 100), TotalAmountRounding)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPackRate_TextChanged(sender As Object, e As EventArgs) Handles txtPackRate.TextChanged
        Try
            If Val(Me.txtPackRate.Text) > 0 Then
                If getConfigValueByType("Apply40KgRate").ToString = "True" AndAlso Me.cmbUnit.Text <> "Loose" Then
                    Me.txtPDP.Text = Math.Round((Val(Me.txtPackRate.Text) / 40), TotalAmountRounding) ''TFS2827
                    '  Me.txtRate.Text = (Val(Me.txtPackRate.Text) / 40) ''Commented Against TFS3330
                ElseIf Me.cmbUnit.Text <> "Loose" Then
                    Me.txtPDP.Text = Math.Round((Val(Me.txtPackRate.Text) / Val(Me.txtPackQty.Text)), TotalAmountRounding)
                    '  Me.txtRate.Text = (Val(Me.txtPackRate.Text) / Val(Me.txtPackQty.Text)) ''Commented Against TFS3330
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ' ''' <summary>
    ' ''' TASK TFS1648 done by Ameen on 03-11-2017
    ' ''' </summary>
    ' ''' <param name="ArticleId"></param>
    ' ''' <param name="Rate"></param>
    ' ''' <param name="Pack"></param>
    ' ''' <param name="PackQty"></param>
    ' ''' <param name="SO_Id"></param>
    ' ''' <param name="LocationID"></param>
    ' ''' <returns></returns>
    ' ''' <remarks> Quantity will be sumed in case item, location, Pack, Unit and Rate are same over addition of new row </remarks>
    'Private Function FindExistsItem(ByVal ArticleId As Integer, ByVal Rate As Double, ByVal Pack As String, ByVal PackQty As Double, ByVal SO_Id As Integer, ByVal LocationID As Integer) As Boolean
    '    Try
    '        'Task:2432 Added Flg Marge Item
    '        If flgMargeItem = True Then
    '            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
    '            Dim dr() As DataRow
    '            dr = dt.Select("ArticleDefId=" & ArticleId & " And Price=" & Val(Rate) & " AND unit='" & Pack & "' AND PackQty=" & Val(PackQty) & " AND LocationId =" & LocationID & " ")
    '            If dr.Length > 0 Then
    '                For Each r As DataRow In dr
    '                    'If dr(0).ItemArray(0) = r.ItemArray(11) AndAlso dr(0).ItemArray(11) = r.ItemArray(11) AndAlso dr(0).ItemArray(8) = r.ItemArray(8) AndAlso dr(0).ItemArray(5) = r.ItemArray(5) Then
    '                    '    r.BeginEdit()
    '                    '    r(6) = Val(dr(0).ItemArray(6)) + Val(Me.txtQty.Text)
    '                    '    'r("LoadQty") = Val(dr(0).ItemArray(6)) + Val(Me.txtQty.Text)
    '                    '    r("TotalQty") = r(6)
    '                    '    r.EndEdit()
    '                    'End If
    '                    If dr(0).ItemArray("LocationId") = LocationID AndAlso dr(0).ItemArray("Price") = r.ItemArray("Price") AndAlso dr(0).ItemArray("unit") = r.ItemArray("unit") AndAlso dr(0).ItemArray("PackQty") = r.ItemArray("PackQty") Then
    '                        If dr(0).ItemArray("unit") = "Pack" Then
    '                            r.BeginEdit()
    '                            r("Qty") = Val(dr(0).ItemArray("Qty")) + Val(Me.txtQty.Text)
    '                            r("TotalQty") = r("Qty") * Val(Me.txtPackQty.Text)
    '                            r.EndEdit()
    '                        Else
    '                            r.BeginEdit()
    '                            r("Qty") = Val(dr(0).ItemArray("Qty")) + Val(Me.txtQty.Text)
    '                            r("TotalQty") = r("Qty")
    '                            r.EndEdit()
    '                        End If

    '                    End If
    '                Next
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        Else
    '            Return False
    '        End If
    '        'End Task:2432
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Private Sub btnLoadSO_Click(sender As Object, e As EventArgs) Handles btnLoadSO.Click
        Try
            Dim frmSOAndDC As New frmSOPopup(, CType(Me.grd.DataSource, DataTable), True)
            frmSOAndDC.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub LoadSO(ByVal DT As DataTable, ByVal CustomerId As Integer, ByVal SalesOrderId As Integer)

        'Task Saad Afzaal check config value of AllowMinusStock
        Dim IsMinus As Boolean = getConfigValueByType("AllowMinusStock")

        Dim isLocationWiseMinusStock As Boolean
        Dim dtBaseSource As DataTable = CType(grd.DataSource, DataTable)
        Me.cmbVendor.Value = CustomerId
        Me.cmbPo.SelectedValue = SalesOrderId

        Me.txtRemarks.Text = GetDeliveryRemarks(Me.cmbPo.SelectedValue)
        ''TASK TFS1764
        Me.cmbTransporter.SelectedValue = CType(cmbPo.SelectedItem, DataRowView).Item("TransporterId")
        ''END TASK TFS1764
        Me.cmbSalesMan.SelectedValue = Val(CType(Me.cmbPo.SelectedItem, DataRowView).Item("SOP_ID").ToString)
        Dim ii As Integer = 0
        If flgMultipleSalesOrder = True Then
            If grd.RowCount > 0 Then
                For ii = 0 To grd.RowCount - 1
                    'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                    If Me.cmbPo.SelectedValue = Val(grd.GetRows(ii).Cells(EnumGridDetail.SO_ID).Value.ToString) Then
                        'msg_Error("This SO has been already loaded.")
                        Exit Sub
                    End If
                Next
                DT.Merge(dtBaseSource)
                Me.grd.DataSource = DT
            Else
                Me.grd.DataSource = DT
            End If
        Else
            If grd.RowCount > 0 Then
                For ii = 0 To grd.RowCount - 1
                    'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                    If Me.cmbPo.SelectedValue = Val(grd.GetRows(ii).Cells(EnumGridDetail.SO_ID).Value.ToString) Then
                        'msg_Error("This SO has been already loaded.")
                        Exit Sub
                    End If
                Next
                Me.grd.DataSource = DT
            Else
                Me.grd.DataSource = DT
            End If
        End If
        'Task Saad Afzaal check if item stock is in negative then its restricted and not to add in item grid

        For Each dr As DataRow In DT.Rows
            IsSOLoaded = True

            dr.BeginEdit()
            AvailableStock = Convert.ToDouble(GetStockById(dr.Item("ArticleDefId"), dr.Item("LocationId"), IIf(dr.Item("unit").ToString = "Loose", "Loose", "Pack")))

            If AvailableStock < 0 AndAlso IsMinus = False Then   'Task Saad Afzaal check if stock is negative and configuration is also off then item can not add
                ShowErrorMessage("Stock is negative against item " & dr.Item("Item").ToString & " ")
                dr.Delete()
                dr.EndEdit()
                AvailableStock = 0

            ElseIf AvailableStock < 0 AndAlso IsMinus = True Then

                isLocationWiseMinusStock = CheckLocationWiseMinusStock(dr.Item("LocationId"))

                If isLocationWiseMinusStock = False Then

                    ShowErrorMessage("Stock is negative against item " & dr.Item("Item").ToString & " ")
                    dr.Delete()
                    dr.EndEdit()
                    AvailableStock = 0

                End If

            End If

            ''TASK TFS3781
            'If DT.Rows.Count > 0 Then
            If IsDBNull(dr.Item("CurrencyId")) Or Val(dr.Item("CurrencyId").ToString) = 0 Then
                'Me.cmbCurrency.SelectedValue = Nothing
                Me.cmbCurrency.Enabled = False
            Else
                'IsCurrencyEdit = True
                'IsNotCurrencyRateToAll = True
                FillCombo("Currency")
                Me.CurrencyRate = Math.Round(Convert.ToInt32(dr.Item("CurrencyRate").ToString), 4)
                Me.txtCurrencyRate.Text = Me.CurrencyRate
                Me.cmbCurrency.SelectedValue = Val(dr.Item("CurrencyId").ToString)
                Me.cmbCurrency.Enabled = False
            End If
            'cmbCurrency_SelectedIndexChanged(Nothing, Nothing)
            'End If

            ''END TASK TFS3781
        Next

        DT.AcceptChanges()

        Me.grd.DataSource = DT
        Me.grd.RetrieveStructure()

        Me.grd.RootTable.Columns(EnumGridDetail.LocationID).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.LocationID).LimitToList = True
        Me.grd.RootTable.Columns(EnumGridDetail.LocationID).EditType = Janus.Windows.GridEX.EditType.Combo

        Me.grd.RootTable.Columns(EnumGridDetail.SO_ID).Visible = False

        Me.grd.RootTable.Columns(EnumGridDetail.SO_ID).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.SO_ID).LimitToList = True
        Me.grd.RootTable.Columns(EnumGridDetail.SO_ID).EditType = Janus.Windows.GridEX.EditType.Combo



        ''
        Me.grd.RootTable.Columns(EnumGridDetail.Engine_No).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.Engine_No).LimitToList = True
        Me.grd.RootTable.Columns(EnumGridDetail.Engine_No).EditType = Janus.Windows.GridEX.EditType.Combo

        Me.grd.RootTable.Columns(EnumGridDetail.Chassis_No).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.Chassis_No).LimitToList = True
        Me.grd.RootTable.Columns(EnumGridDetail.Chassis_No).EditType = Janus.Windows.GridEX.EditType.Combo
        ''

        ''Start TFS2827
        Me.grd.RootTable.Columns(EnumGridDetail.DiscountId).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.DiscountId).LimitToList = True
        Me.grd.RootTable.Columns(EnumGridDetail.DiscountId).EditType = Janus.Windows.GridEX.EditType.Combo
        ''End TFS2827
        ''Start TFS4181
        Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).HasValueList = True
        Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).LimitToList = False
        Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).EditType = Janus.Windows.GridEX.EditType.Combo
        ''End TFS4181
        ''Start TFS4181
        Me.grd.RootTable.Columns(EnumGridDetail.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
        Me.grd.RootTable.Columns(EnumGridDetail.ExpiryDate).FormatString = str_DisplayDateFormat
        ''End TFS4181
        Me.grd.RootTable.Columns("Origin").HasValueList = True
        Me.grd.RootTable.Columns("Origin").LimitToList = False
        Me.grd.RootTable.Columns("Origin").EditType = Janus.Windows.GridEX.EditType.Combo

        Me.grd.RootTable.Columns("DeliveredQty").Visible = False
        Me.grd.RootTable.Columns("Stock").Visible = False
        Me.grd.RootTable.Columns("DeliveryId").Visible = False
        Me.grd.RootTable.Columns("DeliveryDetailId").Visible = False
        Me.grd.RootTable.Columns("CostPrice").Visible = False
        Me.grd.RootTable.Columns("DeliverQty").Caption = "Delivered Qty"

        Me.grd.RootTable.Columns(EnumGridDetail.OrderQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.OrderQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.OrderQty).FormatString = "N" & DecimalPointInQty

        Me.grd.RootTable.Columns(EnumGridDetail.DeliverQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.DeliverQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.DeliverQty).FormatString = "N" & DecimalPointInQty

        Me.grd.RootTable.Columns(EnumGridDetail.RemainingQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.RemainingQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grd.RootTable.Columns(EnumGridDetail.RemainingQty).FormatString = "N" & DecimalPointInQty

        'Me.grd.RootTable.Columns(EnumGridDetail.UM).HasValueList = True
        'Me.grd.RootTable.Columns(EnumGridDetail.UM).LimitToList = True
        'Me.grd.RootTable.Columns(EnumGridDetail.UM).EditType = Janus.Windows.GridEX.EditType.Combo



        FillCombo("grdLocation")
        'FillCombo("grdUM")
        FillCombo("grdSO")

        ''DATED 02-03-2018
        FillCombo("grdEngine")
        FillCombo("grdChassis")
        ''

        'Ayesha Rehman: TFS2827 : 13-04-2018 : Fill combo boxes
        FillCombo("grdDiscountType")
        'Ayesha Rehman : TFS2827 : 13-04-2018 : End

        ''Ayesha Rehman: TFS4181: 16-08-2018 : Fill combo boxes
        FillCombo("grdBatchNo")
        ''Ayesha Rehman : TFS4181 : 16-08-2018 : End
        FillCombo("GrdOrigin")
        ApplyGridSettings()
        'If DT.Rows.Count > 0 Then
        '    If IsDBNull(DT.Rows.Item(0).Item("CurrencyId")) Or Val(DT.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
        '        'Me.cmbCurrency.SelectedValue = Nothing
        '        Me.cmbCurrency.Enabled = False
        '    Else
        '        'IsCurrencyEdit = True
        '        'IsNotCurrencyRateToAll = True
        '        FillCombo("Currency")
        '        Me.CurrencyRate = Val(DT.Rows.Item(0).Item("CurrencyRate").ToString)
        '        Me.cmbCurrency.SelectedValue = Val(DT.Rows.Item(0).Item("CurrencyId").ToString)
        '        Me.cmbCurrency.Enabled = False
        '    End If
        '    'cmbCurrency_SelectedIndexChanged(Nothing, Nothing)
        'End If
        'grd1.Rows.Clear()
        'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

        '    grd1.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)(10), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)("BatchID"), objDataSet.Tables(0).Rows(i)("LocationID"), objDataSet.Tables(0).Rows(i)("Tax"))

        '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
        '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

        'Next

        'TODO: defin expression of columns
        'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

        '    With Me.grd1.Rows(i)

        '        If Me.grd1.Rows(i).Cells("Unit").Value = "Loose" Then
        '            'txtPackQty.Text = 1
        '            .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value)
        '        Else
        '            .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value) * Val(.Cells("PackQty").Value)
        '        End If

        '    End With
        'Next
        GetTotal()
        GetDeliveryOrderAnalysis()
        Me.grd.RootTable.Columns(EnumGridDetail.Color).Visible = True
        Me.grd.RootTable.Columns(EnumGridDetail.SED).Visible = True
        Me.grd.RootTable.Columns(EnumGridDetail.Size).Visible = True
        'Me.grd.RootTable.Columns(EnumGridDetail.ServiceQty).Visible = False
        Me.txtQty.Visible = True
        Me.grd.RootTable.Columns(EnumGridDetail.SampleQty).Visible = True
        '2598
        Me.grd.RootTable.Columns(EnumGridDetail.Comments).Visible = True
        If flgLoadAllItems = True Then
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                If Me.grd.RowCount > 0 Then
                    r.BeginEdit()
                    r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
                    r.EndEdit()
                End If
            Next
        End If
        If flgLoadAllItems = True Then
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                If Me.grd.RowCount > 0 Then
                    r.BeginEdit()
                    r.Cells("LocationId").Value = Me.cmbCategory.SelectedValue
                    r.EndEdit()
                End If
            Next
        End If
        ApplyGridSettings()
    End Sub


    Private Sub txtDisc_LostFocus(sender As Object, e As EventArgs) Handles txtDisc.LostFocus
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

        End Try
    End Sub

    Private Sub txtDisc_TextChanged(sender As Object, e As EventArgs) Handles txtDisc.TextChanged
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


    Private Sub CtrlGrdBar2_Load_1(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Delivery Chalan"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub





    Private Sub CtrlGrdBar1_Load_1(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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
    ''' <summary>
    ''' TASK TFS3324 done on 31-05-2018
    ''' </summary>
    ''' <param name="ArticleID"></param>
    ''' <param name="LocationId"></param>
    ''' <param name="Qty"></param>
    ''' <param name="Remarks"></param>
    ''' <param name="Command"></param>
    ''' <returns></returns>
    ''' <remarks> Stock should be effected with cost sheet items of a logical item in case it is a logical item</remarks>
    Private Function GetItemsOfLogicalItem(ByVal ArticleID As Integer, ByVal LocationId As Integer, ByVal Qty As Double, ByVal Remarks As String, ByVal Command As OleDbCommand) As Boolean
        Try
            Dim dtItems As New DataTable
            Dim dataAdapter As OleDbDataAdapter
            Dim Query As String = " SELECT tblCostSheet.ArticleID AS ArticleId, Case When IsNull(tblCostSheet.CostPrice,0)=0 Then IsNull(ArticleDefTable.PurchasePrice,0.0) Else IsNull(tblCostSheet.CostPrice,0.0) End AS [Purchase Price], IsNull(ArticleDefTable.SalePrice,0.0) AS [Sale Price], tblCostSheet.Total_Qty As Qty, " _
                       & " ISNULL(tblCostSheet.Qty,0) as TotalQty FROM tblCostSheet INNER JOIN ArticleDefTable ON tblCostSheet.ArticleID = ArticleDefTable.ArticleId WHERE tblCostSheet.MasterArticleID IN (SELECT DISTINCT MasterID FROM ArticleDefTable WHERE ArticleId = " & ArticleID & ")"
            Command.CommandText = Query
            dataAdapter = New OleDbDataAdapter()
            dataAdapter.SelectCommand = Command
            dataAdapter.Fill(dtItems)
            dtItems.AcceptChanges()
            If dtItems.Rows.Count > 0 Then
                For Each Row As DataRow In dtItems.Rows
                    If Val(Row.Item("TotalQty").ToString) > 0 Then
                        StockDetail = New StockDetail
                        StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                        StockDetail.LocationId = LocationId
                        StockDetail.ArticleDefId = Row.Item("ArticleId")
                        StockDetail.InQty = 0
                        If IsSalesOrderAnalysis = True Then
                            StockDetail.OutQty = Val(Row.Item("TotalQty").ToString) * Qty
                        Else
                            StockDetail.OutQty = Val(Row.Item("TotalQty").ToString) * Qty
                        End If
                        StockDetail.Rate = Val(Row.Item("Purchase Price").ToString)
                        StockDetail.InAmount = 0
                        StockDetail.OutAmount = Val(Row.Item("Purchase Price").ToString) * StockDetail.OutQty
                        StockDetail.Remarks = Remarks
                        StockDetail.Engine_No = ""
                        StockDetail.Chassis_No = ""
                        StockDetail.PackQty = Val(Row.Item("Qty").ToString)
                        StockDetail.Out_PackQty = Val(Row.Item("Qty").ToString) * Qty
                        StockDetail.In_PackQty = 0
                        StockDetail.BatchNo = ""
                        StockList.Add(StockDetail)
                    End If
                Next
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub grd_Click(sender As Object, e As EventArgs) Handles grd.Click
        Try
            If Me.grd.RowCount > 0 AndAlso Me.grd.GetRow.Cells(EnumGridDetail.ArticleID).Value IsNot Nothing Then
                If flgVehicleIdentificationInfo = True Then
                    Dim str1 As String
                    str1 = "SELECT Engine_No, Chassis_No, Sum(InQty) InQty, Sum(OutQty) OutQty, Sum(InQty)-Sum(OutQty) Qty FROM StockDetailTable WHERE Engine_No <> '' And ArticleDefId = " & Val(Me.grd.GetRow.Cells("ArticleDefId").Value.ToString) & " GROUP BY Engine_No, Chassis_No HAVING Sum(InQty)-Sum(OutQty) > 0"
                    Dim dt2 As DataTable = GetDataTable(str1)
                    If dt2.Rows.Count > 0 Then
                        If Val(grd.GetRow.Cells("TotalQty").Value) > dt2.Rows(0).Item("Qty") Then
                            ShowErrorMessage("This Qty not exists in Stock ")
                            grd.CancelCurrentEdit()
                            Exit Sub
                        End If
                    End If
                End If
                Dim Str As String = ""
                Str = "SELECT Engine_No, Chassis_No, Sum(InQty) InQty, Sum(OutQty) OutQty, Sum(InQty)-Sum(OutQty) Qty FROM StockDetailTable WHERE Engine_No <> '' And ArticleDefId = " & Val(Me.grd.GetRow.Cells("ArticleDefId").Value.ToString) & "  And LocationId = " & Val(Me.grd.GetRow.Cells(EnumGridDetail.LocationID).Value.ToString) & " GROUP BY Engine_No, Chassis_No HAVING Sum(InQty)-Sum(OutQty) > 0"
                Dim dt As DataTable = GetDataTable(Str)
                Me.grd.RootTable.Columns("Engine_No").ValueList.PopulateValueList(dt.DefaultView, "Engine_No", "Engine_No")
                Str = "SELECT Engine_No, Chassis_No, Sum(InQty) InQty, Sum(OutQty) OutQty, Sum(InQty)-Sum(OutQty) Qty FROM StockDetailTable WHERE Engine_No <> '' And ArticleDefId = " & Val(Me.grd.GetRow.Cells("ArticleDefId").Value.ToString) & "  And LocationId = " & Val(Me.grd.GetRow.Cells(EnumGridDetail.LocationID).Value.ToString) & " GROUP BY Engine_No, Chassis_No HAVING Sum(InQty)-Sum(OutQty) > 0"
                Dim dt1 As DataTable = GetDataTable(Str)
                Me.grd.RootTable.Columns("Chassis_No").ValueList.PopulateValueList(dt1.DefaultView, "Chassis_No", "Chassis_No")
            End If
            If Me.grd.RowCount > 0 AndAlso Me.grd.GetRow.Cells(EnumGridDetail.ArticleID).Value IsNot Nothing Then
                Dim str As String = ""
                str = " Select  BatchNo,BatchNo,ExpiryDate,Origin  From  StockDetailTable  where BatchNo not in ('','0','xxxx')  And ArticledefId = " & Me.grd.GetRow.Cells(EnumGridDetail.ArticleID).Value & "  And LocationId = " & Val(Me.grd.GetRow.Cells(EnumGridDetail.LocationID).Value.ToString) & " Group by BatchNo,ExpiryDate,Origin Having Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0)) > 0  ORDER BY ExpiryDate Asc"
                Dim dt As DataTable = GetDataTable(str)
                Me.grd.RootTable.Columns(EnumGridDetail.BatchNo).ValueList.PopulateValueList(dt.DefaultView, "BatchNo", "BatchNo")
                If Not dt.Rows.Count > 0 Then
                    grd.GetRow.Cells(EnumGridDetail.BatchNo).Value = "xxxx"
                Else
                    If IsDBNull(grd.GetRow.Cells(EnumGridDetail.BatchNo).Value) Or grd.GetRow.Cells(EnumGridDetail.BatchNo).Value.ToString = "" Then
                        grd.GetRow.Cells(EnumGridDetail.BatchNo).Value = dt.Rows(0).Item("BatchNo").ToString
                    End If
                End If
                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0).Item("BatchNo").ToString) Then
                        str = " Select   ExpiryDate, Origin  From  StockDetailTable  where BatchNo not in ('','0','xxxx') And BatchNo ='" & Me.grd.GetRow.Cells(EnumGridDetail.BatchNo).Value.ToString & "'" _
                             & " And ArticledefId = " & Me.grd.GetRow.Cells(EnumGridDetail.ArticleID).Value & "  And LocationId = " & Val(Me.grd.GetRow.Cells(EnumGridDetail.LocationID).Value.ToString) & "  And (isnull(InQty, 0) - isnull(OutQty, 0)) > 0 Group by BatchNo,ExpiryDate,Origin ORDER BY ExpiryDate  Asc "
                        Dim dtExpiry As DataTable = GetDataTable(str)
                        If dtExpiry.Rows.Count > 0 Then
                            If IsDBNull(dtExpiry.Rows(0).Item("ExpiryDate")) = False Then
                                grd.GetRow.Cells(EnumGridDetail.ExpiryDate).Value = CType(dtExpiry.Rows(0).Item("ExpiryDate").ToString, Date)
                                grd.GetRow.Cells("Origin").Value = dtExpiry.Rows(0).Item("Origin").ToString
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow
        Try
            Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
            If e.Row.Cells(EnumGridDetail.AdditionalItem).Text <> "" AndAlso e.Row.Cells(EnumGridDetail.AdditionalItem).Text = "1" Then
                rowStyle.BackColor = Color.LightGray
                e.Row.RowStyle = rowStyle
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDeliveryChalan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label66_Click(sender As Object, e As EventArgs) Handles Label66.Click

    End Sub

    Private Sub grdSaved_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSaved.FormattingRow

    End Sub

    Private Sub grd_ControlAdded(sender As Object, e As ControlEventArgs) Handles grd.ControlAdded

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
                Me.grd.RootTable.Columns(EnumGridDetail.CurrencyAmount).Caption = "Amount (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns(EnumGridDetail.CurrencyRate).Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"
                'Me.grd.RootTable.Columns(EnumGridDetail.CurrencyTaxAmount).Caption = "Tax Amount (" & Me.cmbCurrency.Text & ")"                 
                'grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(EnumGridDetail.CurrencyAmount).Visible = False
                    Me.grd.RootTable.Columns(EnumGridDetail.BaseCurrencyRate).Visible = False
                    Me.grd.RootTable.Columns(EnumGridDetail.CurrencyRate).Visible = False
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
                    Me.grd.RootTable.Columns(EnumGridDetail.CurrencyAmount).Visible = True
                    Me.grd.RootTable.Columns(EnumGridDetail.CurrencyRate).Visible = True
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
