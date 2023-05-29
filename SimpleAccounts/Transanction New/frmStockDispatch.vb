''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction forms
''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''1-Jan-2014 Tsk:2366   Imran Ali         Slow working load forms
''21-Jan-2014   TASK:2388             Imran Ali                  Service Item Check Not Working Properly  
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms 
''03-Feb-2014        Task:2406   Imran Ali    FIELD CHOOSER restriction (Senior Rozgar)
''07-Feb-2014             TASK:2416     Imran Ali       Minus Stock Allowed Work Not Properly
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
'22-5-2014 Task 2644 JUNAID Add New Fields Engine No And Chassis No In Detail Record in Stock Dispatch/Stock Receiving
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''10-June-2015 Task# A1-10-06-2015 Ahmd Sharif: Added Key Pres event for some textboxes to take just numeric and dot value
''10-June-2015 Task# A2-10-06-2015 Ahmad Sharif: Add Check on grdSaved to check on double click if row less than zero than exit
'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'16-Sep-2015 Task#16092015 Ahmad Sharif: Load Companies and Locations user wise
''19-9-2015 TASK18 Imran Ali: Validation On Stock If Less Than 1
''TASK-470 Muhammad Ameen on 01-07-2016: Stock Statement Report By Pack
''TASK TFS1417 Muhammad Ameen on 12-09-2017 : Security rights to opt Sale Price or Cost Price on Stock Dispatch and Stock Receive.
''TASK TFS3776 Muhammad Ameen has done on 05-07-2018: Delete and update should be handled in case of bulk stock transfer.
''TFS4161 Ayesha Rehman : 09-08-2018 : P QTY: (Should Be Static/ Un-Changeable / Un-Editable on All Screens)
''TFS4347 Ayesha Rehman : 20-08-2018 : Addition of color and size fields to detail grid in Stock Dispatch 
Imports SBDal
Imports SBDal.StockDAL
Imports SBDal.StockDocTypeDAL
Imports SBDal.UtilityDAL
Imports SBModel
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports SBUtility.Utility
Imports System.Data.SqlClient

Public Class frmStockDispatch
    Implements IGeneral

    Dim dt As DataTable
    Dim Mode As String = "Normal"
    Dim IsFormOpen As Boolean = False
    Dim StockMaster As StockMaster
    Dim StockDetail As StockDetail
    Dim StockReceivingMaster As StockReceivingMaster
    Dim POID As Integer = 0
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim setVoucherNo As String = String.Empty
    Dim getVoucher_Id As Integer = 0
    Dim setEditMode As Boolean = False
    Dim Total_Amount As Double = 0D
    Dim crpt As New ReportDocument
    Dim Email As Email
    Dim Previouse_Amount As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim flgLocationWiseItem As Boolean = False
    Dim flgVehicleIdentificationInfo As Boolean = False 'Task:2644 Added Flag Vehicle Identification
    Dim blnUpdateAll As Boolean = False
    Dim flgCompanyRights As Boolean = False
    Dim StoreIssuanceDependonProductionPlan As Boolean = False
    Dim IsEditMode As Boolean = False
    Dim _objTrans As OleDb.OleDbTransaction = Nothing
    Dim ItemHierarchy As String = "" 'Ali Faisal : TFS1376 : Add Item Hierarchy
    Dim ShowSalePrice As Boolean = False
    Dim IsBulkStockTransfer As Boolean = False
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Dim ItemFilterByName As Boolean = False
    Enum grdEnm
        LocationId
        ArticleCode
        Item
        Size ''TFS4347
        Color ''TFS4347
        BatchNo
        ExpiryDate
        Origin
        Unit
        Qty
        Rate
        SalePrice
        Total
        CategoryId
        ItemId
        PackQty
        CurrentPrice
        BatchId
        Pack_Desc
        Engine_No
        Chassis_No
        ArticleMasterId
        TotalQuantity
        IncoTerm
        CommercialPrice
    End Enum
    Private Enum EnumGridSaved
        DispatchId
        LocationId
        DispatchNo
        DispatchDate
        Location
        VendorId
        PurchaseOrderID
        PartyInvoiceNo
        PartySlipNo
        DispatchQty
        DispatchAmount
        CashPaid
        Remarks
        UserName
        RefDocument
        ReceivingId
        Printed
        PlanId
        PlanTicketId
        IsBulkStockTransferred
        CustomerID
    End Enum

    Private Sub frmStockDispatch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

            If e.KeyCode = Keys.F2 Then
                frmBulkStockTransfer.ShowDialog()
                DisplayRecord()
            End If
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            'If e.KeyCode = Keys.F1 Then
            '    frmStockTransferItemsList.LocationId = cmbCategory.SelectedValue
            '    frmStockTransferItemsList.BringToFront()
            '    frmStockTransferItemsList.ShowDialog()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmStockDispatch_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 
        Try

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            ' ''TASK TFS4544
            'If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            '    ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            'End If
            ' ''END TFS4544

            txtFrom.Text = gobjLocationId
            'GetSecurityRights()
            FillCombo("Vendor")
            'FillCombo("Item") Moved to RefreshControls
            FillCombo("CostSheetItem")
            FillCombo("Customer")
            FillCombo("Transporter")
            FillCombo("Currency")
            'FillCombo("ArticlePack") R933 Commented
            RefreshControls()
            FillCombo("Category")
            'Me.DisplayRecord()
            '//This will hide Master grid
            Me.grdSaved.Visible = CType(getConfigValueByType("ShowMasterGrid"), Boolean)
            IsFormOpen = True
            ''Task:2366 Added Location Wise Filter Configuration
            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItem = getConfigValueByType("ArticleFilterByLocation")
            End If
            ''End Task:2366
            'Task:2644 Added Flag Vehicle Identification Info
            If Not getConfigValueByType("flgVehicleIdentificationInfo").ToString = "Error" Then
                flgVehicleIdentificationInfo = getConfigValueByType("flgVehicleIdentificationInfo")
            Else
                flgVehicleIdentificationInfo = False
            End If
            'End Task:2644
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If


            If Not getConfigValueByType("StoreIssuaneDependonProductionPlan").ToString = "Error" Then
                StoreIssuanceDependonProductionPlan = getConfigValueByType("StoreIssuaneDependonProductionPlan")
            End If
            ''start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161
            'Ali Faisal : TFS1376 : Add Item Hierarchy Configuration
            If getConfigValueByType("CostSheetType").ToString = "Standard Cost Sheet" Then
                ItemHierarchy = getConfigValueByType("CostSheetType")
            Else
                ItemHierarchy = getConfigValueByType("CostSheetType")
            End If
            'Ali Faisal : TFS1376 : End

            'TFS3360
            UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
        Dim str As String = String.Empty
        'str = "SELECT     Recv.DispatchNo, Recv.DispatchDate AS Date, vwCOADetail.detail_title as CustomerName, V.PurchaseOrderNo, Recv.DispatchQty, Recv.DispatchAmount, Recv.DispatchId,  " & _
        '        "Recv.CustomerCode, EmployeeDefTable.EmployeeName, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId " & _
        '        "FROM         DispatchMasterTable Recv INNER JOIN " & _
        '        "vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
        '        "EmployeeDefTable ON Recv.EmployeeCode = EmployeeDefTable.EmployeeId LEFT OUTER JOIN " & _
        '        "PurchaseOrderMasterTable V ON Recv.POId = V.PurchaseOrderId " & _
        '        "ORDER BY Recv.DispatchNo DESC"

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
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & " DispatchId, LocationId, DispatchNo, DispatchDate,tblDefLocation.Location_code, VendorId, PurchaseOrderID, " _
            ' & " PartyInvoiceNo, PartySlipNo, DispatchQty, DispatchAmount, CashPaid,  Remarks, UserName, RefDocument, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status] " _
            ' & "  FROM dbo.DispatchMasterTable inner join tblDefLocation on DispatchMasterTable.VendorID = tblDefLocation.location_ID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = DispatchNo  " _
            ' & " Where (DispatchNo like 'GP%' OR DispatchNo like 'DN%') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, DispatchDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " order by 1 desc"


            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            ' str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & " DispatchId, DispatchMasterTable.LocationId, DispatchNo, DispatchDate,tblDefLocation.Location_code, DispatchMasterTable.VendorId, DispatchMasterTable.PurchaseOrderID, " _
            '& " DispatchMasterTable.PartyInvoiceNo, DispatchMasterTable.PartySlipNo, DispatchQty, DispatchAmount, DispatchMasterTable.CashPaid,  DispatchMasterTable.Remarks, DispatchMasterTable.UserName, DispatchMasterTable.RefDocument, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],DispatchMasterTable.PlanId,PlanMasterTable.PlanNo  " _
            '& "  FROM dbo.DispatchMasterTable inner join tblDefLocation on DispatchMasterTable.VendorID = tblDefLocation.location_ID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = DispatchNo LEFT OUTER JOIN PlanMasterTable on PlanMasterTable.PlanId = DispatchMasterTable.PlanId  " _
            '& " Where (DispatchNo like 'GP%' OR DispatchNo like 'DN%') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, DispatchDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " order by 1 desc"
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            ' str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & " DispatchId, DispatchMasterTable.LocationId, DispatchNo, DispatchDate,tblDefLocation.Location_code, DispatchMasterTable.VendorId, DispatchMasterTable.PurchaseOrderID, " _
            '& " DispatchMasterTable.PartyInvoiceNo, DispatchMasterTable.PartySlipNo, DispatchQty, DispatchAmount, DispatchMasterTable.CashPaid,  DispatchMasterTable.Remarks, DispatchMasterTable.UserName, DispatchMasterTable.RefDocument, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],DispatchMasterTable.PlanId,PlanMasterTable.PlanNo,DispatchMasterTable.username as 'User Name'  " _
            '& "  FROM dbo.DispatchMasterTable inner join tblDefLocation on DispatchMasterTable.VendorID = tblDefLocation.location_ID LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = DispatchNo LEFT OUTER JOIN PlanMasterTable on PlanMasterTable.PlanId = DispatchMasterTable.PlanId  " _
            '& " Where (DispatchNo like 'GP%' OR DispatchNo like 'DN%') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, DispatchDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " order by 1 desc"
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            ' 'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & " DispatchId, DispatchMasterTable.LocationId, DispatchNo, DispatchDate,tblDefLocation.Location_code, DispatchMasterTable.VendorId, DispatchMasterTable.PurchaseOrderID, " _
                       & " DispatchMasterTable.PartyInvoiceNo, DispatchMasterTable.PartySlipNo, DispatchQty, DispatchAmount, DispatchMasterTable.CashPaid,  DispatchMasterTable.Remarks, DispatchMasterTable.UserName, DispatchMasterTable.RefDocument, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],DispatchMasterTable.PlanId,PlanMasterTable.PlanNo,DispatchMasterTable.username as 'User Name', IsNull(PlanTicketId, 0) As PlanTicketId, IsNull(DispatchMasterTable.IsBulkStockTransferred, 0) AS IsBulkStockTransferred,IsNull(DispatchMasterTable.CustomerID,0) as CustomerID,IsNull(DispatchMasterTable.QuoteReference,'') as QuoteReference,IsNull(DispatchMasterTable.TrackingNo,'') as TrackingNo,ISNULL(DispatchMasterTable.TransporterId,0) as TransporterId,ISNULL(DispatchMasterTable.PaymentTerm,'') as PaymentTerm,ISNULL(DispatchMasterTable.CreditDays,'') as CreditDays,ISNULL(DispatchMasterTable.IncoTermSite,'') as IncoTermSite,ISNULL(DispatchMasterTable.currency_id,0) as currency_id " _
                       & "  FROM dbo.DispatchMasterTable inner join tblDefLocation on DispatchMasterTable.VendorID = tblDefLocation.location_ID LEFT JOIN tblCustomer TC ON TC.CustomerID=DispatchMasterTable.CustomerId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = DispatchNo LEFT OUTER JOIN PlanMasterTable on PlanMasterTable.PlanId = DispatchMasterTable.PlanId LEFT Join tblcurrency ON tblcurrency.currency_id= DispatchMasterTable.currency_id  " _
                       & " Where (DispatchNo like 'GP%' OR DispatchNo like 'DN%') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, DispatchDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " order by 1 desc"
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms

        End If

        FillGridEx(grdSaved, str, True)
        Me.grdSaved.RootTable.Columns.Add("Column1")
        Me.grdSaved.RootTable.Columns("Column1").UseHeaderSelector = True
        Me.grdSaved.RootTable.Columns("Column1").ActAsSelector = True
        Me.grdSaved.RootTable.Columns("Column1").Width = 50

        grdSaved.RootTable.Columns("DispatchId").Visible = False
        grdSaved.RootTable.Columns("LocationId").Visible = False
        grdSaved.RootTable.Columns("VendorId").Visible = False
        grdSaved.RootTable.Columns("PurchaseOrderID").Visible = False
        grdSaved.RootTable.Columns("PartyInvoiceNo").Visible = False
        grdSaved.RootTable.Columns("PartySlipNo").Visible = False
        grdSaved.RootTable.Columns("CashPaid").Visible = False
        grdSaved.RootTable.Columns("UserName").Visible = False
        grdSaved.RootTable.Columns("RefDocument").Visible = False
        grdSaved.RootTable.Columns("PlanTicketId").Visible = False 'PlanTicketId

        grdSaved.RootTable.Columns("IsBulkStockTransferred").Visible = False

        grdSaved.RootTable.Columns("DispatchNo").Caption = "Doc No"
        grdSaved.RootTable.Columns("DispatchDate").Caption = "Doc Date"
        grdSaved.RootTable.Columns("Remarks").Caption = "Remarks"
        grdSaved.RootTable.Columns("DispatchQty").Caption = "Qty"
        grdSaved.RootTable.Columns("DispatchAmount").Caption = "Amount"

        'Task:2759 Set rounded format
        grdSaved.RootTable.Columns("DispatchAmount").FormatString = "N" & DecimalPointInValue
        'end task:2759

        'grdSaved.Columns(0).Width = 100
        'grdSaved.Columns(1).Width = 100
        'grdSaved.Columns(2).Width = 100
        'grdSaved.Columns(3).Width = 200
        'grdSaved.Columns(8).Width = 200
        Me.grdSaved.RootTable.Columns("DispatchDate").FormatString = str_DisplayDateFormat

    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Validate_AddToGrid() Then
            AddItemToGrid()
            'GetTotal()
            grd.MoveFirst()
            grd_Click(Nothing, Nothing)
            ClearDetailControls()
            cmbItem.Focus()
            'FillCombo("Item")
        End If
    End Sub
    Private Sub RefreshControls()
        Try
            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''END TFS4544
            FillCombo("Item")
            cmbItem.Rows(0).Activate()
            txtPONo.Text = ""
            dtpPODate.Value = Now
            Me.dtpPODate.Enabled = True
            txtRemarks.Text = ""
            txtQuoteRef.Text = ""
            txtTrackingNo.Text = ""
            txtincotermsite.Text = ""
            txtcommercialprice.Text = 0
            txtpaymentterm.Text = ""
            txtcreditday.Text = ""
            txtPaid.Text = ""
            'txtAmount.Text = ""
            txtTotal.Text = ""
            'txtTotalQty.Text = ""
            txtBalance.Text = ""
            Me.txtQty.Text = ""
            Me.txtRate.Text = ""
            Me.txtTotalQuantity.Text = ""
            txtPackQty.Text = 1
            Me.BtnSave.Text = "&Save"
            Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("DN", 6, "DispatchMasterTable", "DispatchNo")
            Me.txtReceivingNo.Text = GetDocumentNo1()  'GetNextDocNo("SRN", 6, "ReceivingMasterTable", "ReceivingNo")
            Me.FillLocationCombo()
            'FillCombo("SO")'R933 Commented
            'DisplayRecord() R933 Commented History Data
            'grd.Rows.Clear()
            cmbUnit.SelectedIndex = 0
            Me.cmbLocation.Enabled = True
            Me.cmbLocation.Focus()
            Me.cmbLocation.DroppedDown = True
            GetSecurityRights()
            If ShowCostPriceRights = True Then
                Me.pnlCost.Visible = True
                Me.pnlCost.BringToFront()
                Me.pnlSale.Visible = False
                Me.pnlSale.SendToBack()
            Else
                Me.pnlSale.Visible = True
                Me.pnlSale.BringToFront()
                Me.pnlCost.Visible = False
                Me.pnlCost.SendToBack()
            End If
            'If Me.cmbBatchNo.Value = Nothing Then
            '    Me.cmbBatchNo.Enabled = False
            'Else
            '    Me.cmbBatchNo.Enabled = True
            'End If
            IsEditMode = False
            FillCombo("Plan")
            FillCombo("Tickets")
            Me.cmbUnitCostSheet.SelectedIndex = 0
            If Not Me.cmbTicket.SelectedIndex = -1 Then Me.cmbTicket.SelectedIndex = 0
            DisplayPODetail(-1)
            DisplayDetail(-1)
            Me.SplitContainer1.Panel1Collapsed = True
            Me.Button1.Enabled = True
            Me.lblPrintStatus.Text = String.Empty
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnEdit.Visible = False
            Me.BtnDelete.Visible = False
            Me.BtnPrint.Visible = False
            blnUpdateAll = False
            Me.btnUpdateAll.Enabled = True
            IsBulkStockTransfer = False
            '''''''''''''''''''''''''''
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ClearDetailControls()
        'cmbCategory.SelectedIndex = 0
        cmbUnit.SelectedIndex = 0
        txtQty.Text = ""
        txtRate.Text = ""
        txtTotal.Text = ""
        txtPackQty.Text = 1
    End Sub
    Private Function Validate_AddToGrid() As Boolean
        Try
            'Before against task:2388
            'Dim IsMinus As Boolean = getConfigValueByType("AllowMinusStock")
            'Task:2388 Added Validate Service item
            Dim IsMinus As Boolean = True
            If CType(Me.cmbItem.SelectedRow, Infragistics.Win.UltraWinGrid.UltraGridRow).Cells("ServiceItem").Value = False Then
                IsMinus = getConfigValueByType("AllowMinusStock")

            End If
            'End Task:2388
            If Mode = "Normal" Then
                If Not IsMinus = True Then
                    'If Val(Me.txtStock.Text) <> Val(Me.txtTotalQuantity.Text) Then
                    '    If Val(Me.txtStock.Text) - Val(txtTotalQuantity.Text) <= 0 Then
                    ''TASK TFS1490
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
                If CType(Me.cmbCategory.SelectedItem, DataRowView).Row.Item("AllowMinusStock").ToString = "False" AndAlso IsMinus = True Then

                    'If Val(Me.txtStock.Text) <> Val(Me.txtTotalQuantity.Text) Then
                    '    If Val(Me.txtStock.Text) - Val(txtTotalQuantity.Text) <= 0 Then
                    ''TASK TFS1490
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
            End If

            If cmbItem.ActiveRow.Cells(0).Value <= 0 Then
                msg_Error("Please select any item")
                cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
            End If

            If Me.cmbBatchNo.Text = String.Empty Then
                msg_Error("Please select a Batch No")
                Me.cmbBatchNo.Focus() : Return False
            End If

            If Val(txtQty.Text) < 0 Then
                msg_Error("Quantity must be greater than 0")
                txtQty.Focus() : Validate_AddToGrid = False : Exit Function
            End If


            'If Val(txtRate.Text) <= 0 Then
            '    msg_Error("Rate must be greater than 0")
            '    txtRate.Focus() : Validate_AddToGrid = False : Exit Function
            'End If

            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                Me.cmbLocation.Enabled = True
            Else
                Me.cmbLocation.Enabled = False
            End If
            Validate_AddToGrid = True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Validate_AddToGridForPopUp(ByVal ArticleId As Integer, ByVal Qty As Double, ByVal Price As Double, ByVal SalePrice As Double, ByVal PackValue As Integer, ByVal PackQty As Double, ByVal Stock As Double) As Boolean
        Try
            cmbItem.Value = ArticleId
            txtQty.Text = Qty
            txtRate.Text = Price
            txtSalePrice.Text = SalePrice
            txtStock.Text = Stock
            cmbUnit.SelectedIndex = IIf(PackValue <> 1, 1, 0)
            If Me.cmbUnit.Text = "Loose" Then
                Me.txtPackQty.Text = 1
            Else
                Me.txtPackQty.Text = PackQty
            End If
            Dim IsMinus As Boolean = True
            If CType(Me.cmbItem.SelectedRow, Infragistics.Win.UltraWinGrid.UltraGridRow).Cells("ServiceItem").Value = False Then
                IsMinus = getConfigValueByType("AllowMinusStock")

            End If
            'End Task:2388
            If Mode = "Normal" Then
                If Not IsMinus = True Then
                    'If Val(Me.txtStock.Text) <> Val(Me.txtTotalQuantity.Text) Then
                    '    If Val(Me.txtStock.Text) - Val(txtTotalQuantity.Text) <= 0 Then
                    ''TASK TFS1490
                    If Me.cmbUnit.Text = "Loose" Then
                        If Val(Me.txtStock.Text) <> Val(Me.txtQty.Text) Then
                            If Val(Me.txtStock.Text) - Val(txtQty.Text) <= 0 Then
                                msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
                                cmbItem.Focus() : Validate_AddToGridForPopUp = False : Exit Function
                            End If
                        End If
                    Else
                        If Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, "Loose")) <> Val(Me.txtTotalQuantity.Text) Or Val(Me.txtStock.Text) <> Val(Me.txtQty.Text) Then
                            If Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, "Loose")) - Val(txtTotalQuantity.Text) < 0 Or Val(Me.txtStock.Text) - Val(Me.txtQty.Text) < 0 Then
                                msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
                                Validate_AddToGridForPopUp = False : Exit Function
                            End If
                        End If
                    End If


                End If
                If CType(Me.cmbCategory.SelectedItem, DataRowView).Row.Item("AllowMinusStock").ToString = "False" AndAlso IsMinus = True Then

                    'If Val(Me.txtStock.Text) <> Val(Me.txtTotalQuantity.Text) Then
                    '    If Val(Me.txtStock.Text) - Val(txtTotalQuantity.Text) <= 0 Then
                    ''TASK TFS1490
                    If Me.cmbUnit.Text = "Loose" Then
                        If Val(Me.txtStock.Text) <> Val(Me.txtQty.Text) Then
                            If Val(Me.txtStock.Text) - Val(txtQty.Text) <= 0 Then
                                msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
                                cmbItem.Focus() : Validate_AddToGridForPopUp = False : Exit Function
                            End If
                        End If
                    Else
                        If Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, "Loose")) <> Val(Me.txtTotalQuantity.Text) Or Val(Me.txtStock.Text) <> Val(Me.txtQty.Text) Then
                            If Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, "Loose")) - Val(txtTotalQuantity.Text) < 0 Or Val(Me.txtStock.Text) - Val(Me.txtQty.Text) < 0 Then
                                msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
                                Validate_AddToGridForPopUp = False : Exit Function
                            End If
                        End If
                    End If

                End If
            End If

            If cmbItem.ActiveRow.Cells(0).Value <= 0 Then
                msg_Error("Please select any item")
                cmbItem.Focus() : Validate_AddToGridForPopUp = False : Exit Function
            End If


            If Val(txtQty.Text) = 0 Then
                msg_Error("Quantity must be greater than 0")
                txtQty.Focus() : Validate_AddToGridForPopUp = False : Me.cmbItem.Rows(0).Activate() : Exit Function

            End If
            If Val(txtQty.Text) < 0 Then
                msg_Error("Quantity Should not be negative")
                txtQty.Focus() : Validate_AddToGridForPopUp = False : Me.cmbItem.Rows(0).Activate() : Exit Function

            End If


            'If Val(txtRate.Text) <= 0 Then
            '    msg_Error("Rate must be greater than 0")
            '    txtRate.Focus() : Validate_AddToGrid = False : Exit Function
            'End If

            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                Me.cmbLocation.Enabled = True
            Else
                Me.cmbLocation.Enabled = False
            End If
            Validate_AddToGridForPopUp = True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus, txtPackQty.LostFocus
        'If Val(Me.txtPackQty.Text) = 0 Then
        '    txtPackQty.Text = 1
        '    txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        'Else
        '    txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        'End If
    End Sub
    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        If Val(txtPackQty.Text) > 0 Then
            Me.txtTotalQuantity.Text = Val(txtPackQty.Text) * Val(txtQty.Text)
        Else
            Me.txtPackQty.Text = 1
            Me.txtTotalQuantity.Text = Val(txtQty.Text)
        End If
        If Val(txtRate.Text) > 0 Then
            Me.txtTotal.Text = Val(txtTotalQuantity.Text) * Val(txtRate.Text)
        End If
        If Val(txtSalePrice.Text) > 0 Then
            Me.txtSaleTotal.Text = Val(txtTotalQuantity.Text) * Val(txtSalePrice.Text)
        End If
    End Sub
    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRate.LostFocus
        'If Val(Me.txtPackQty.Text) = 0 Then
        '    txtPackQty.Text = 1
        '    txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        'Else
        '    txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        'End If

    End Sub

    Private Sub txtRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate.TextChanged
        Try
            Me.txtTotal.Text = Val(txtTotalQuantity.Text) * Val(txtRate.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        ''get the qty in case of pack unit
        If Me.cmbUnit.SelectedIndex < 0 Then Me.txtPackQty.Text = String.Empty : Exit Sub
        If Me.cmbUnit.Text = "Loose" Then
            'txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
            txtPackQty.Text = 1
            Me.txtPackQty.ReadOnly = True
            Me.txtPackQty.TabStop = False
            Me.txtTotalQuantity.ReadOnly = True
        Else
            If IsPackQtyDisabled = True Then
                Me.txtPackQty.TabStop = False
                Me.txtPackQty.ReadOnly = True
                Me.txtTotalQuantity.ReadOnly = True
            Else
                Me.txtPackQty.TabStop = True
                Me.txtPackQty.ReadOnly = False
                Me.txtTotalQuantity.ReadOnly = False
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


            'objCommand.CommandText = "Select PackQty from ArticleDefTable where ArticleID = " & cmbItem.ActiveRow.Cells(0).Value

            'txtPackQty.Text = objCommand.ExecuteScalar()
            If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
                Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                Me.txtRate.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackRate").ToString) / Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
            End If
            'txtTotal.Text = Math.Round(((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
        End If
        Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))

    End Sub
    Private Sub AddItemToGrid()
        'grd.Rows.Add(cmbCategory.Text, cmbItem.Text, Me.cmbBatchNo.Text, cmbUnit.Text, txtQty.Text, txtRate.Text, Val(txtTotal.Text), cmbCategory.SelectedValue, cmbItem.ActiveRow.Cells(0).Value, Me.txtPackQty.Text, Me.cmbItem.ActiveRow.Cells("Price").Value, Me.cmbBatchNo.Value, Me.cmbCategory.SelectedValue)

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
            Dim dtGrd As DataTable
            dtGrd = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtGrd.NewRow
            'drGrd.Item(grdEnm.Category) = Me.cmbCategory.Text
            drGrd.Item(grdEnm.LocationId) = Me.cmbCategory.SelectedValue
            drGrd.Item(grdEnm.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text
            drGrd.Item(grdEnm.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text
            If Me.cmbBatchNo.SelectedRow.Index > 0 Then
                drGrd.Item(grdEnm.BatchNo) = Me.cmbBatchNo.Text
            End If
            ''Start TFS4347
            drGrd.Item(grdEnm.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
            drGrd.Item(grdEnm.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
            ''End TFS4347
            drGrd.Item(grdEnm.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            drGrd.Item(grdEnm.Qty) = Val(Me.txtQty.Text)
            drGrd.Item(grdEnm.Rate) = Val(Me.txtRate.Text)
            drGrd.Item(grdEnm.SalePrice) = Val(Me.txtSalePrice.Text)
            If pnlCost.Visible = True Then
                drGrd.Item(grdEnm.Total) = Val(Me.txtTotal.Text)
            Else
                drGrd.Item(grdEnm.Total) = Val(Me.txtSaleTotal.Text)
            End If
            drGrd.Item(grdEnm.ExpiryDate) = Convert.ToDateTime(Date.Now.AddMonths(1)) ''TFS4181
            drGrd.Item(grdEnm.CategoryId) = Me.cmbCategory.SelectedValue
            drGrd.Item(grdEnm.ItemId) = Me.cmbItem.ActiveRow.Cells(0).Value
            drGrd.Item(grdEnm.PackQty) = Val(Me.txtPackQty.Text)
            'Ali Faisal : TFS1376 : 24-Aug-2017
            drGrd.Item(grdEnm.CurrentPrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            'Ali Faisal : TFS1376 : 24-Aug-2017 : End
            ''Value Entered Against TFS2739
            drGrd.Item(grdEnm.BatchId) = 1
            drGrd.Item(grdEnm.Pack_Desc) = Me.cmbUnit.Text.ToString
            drGrd.Item(grdEnm.ArticleMasterId) = Val(Me.cmbCostSheetItems.Value.ToString)
            drGrd.Item(grdEnm.TotalQuantity) = Val(Me.txtTotalQuantity.Text)
            drGrd.Item("IncoTerm") = Me.txtincoterm.Text
            drGrd.Item("CommercialPrice") = Me.txtcommercialprice.Text

            dtGrd.Rows.InsertAt(drGrd, 0)
            dtGrd.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' This Function is Added to Add Items from PopUp to the Grid
    ''' </summary>
    ''' <remarks>TFS3777 : Ayesah Rehman : 23-07-2018</remarks>
    Public Sub LoadItemFromPopup(ByVal ArticleId As Integer, ByVal Qty As Double, ByVal Price As Double, ByVal SalePrice As Double, ByVal PackValue As Integer, ByVal PackQty As Double, ByVal Stock As Double)
        Try

            cmbItem.Value = ArticleId
            txtQty.Text = Qty
            txtRate.Text = Price
            txtSalePrice.Text = SalePrice
            cmbUnit.SelectedIndex = IIf(PackValue <> 1, 1, 0)
            If Me.cmbUnit.Text = "Loose" Then
                Me.txtPackQty.Text = 1
            Else
                Me.txtPackQty.Text = PackQty
            End If
            Dim dtGrd As DataTable
            dtGrd = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtGrd.NewRow
            'drGrd.Item(grdEnm.Category) = Me.cmbCategory.Text
            drGrd.Item(grdEnm.LocationId) = Me.cmbCategory.SelectedValue
            drGrd.Item(grdEnm.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text
            drGrd.Item(grdEnm.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text
            If Me.cmbBatchNo.SelectedRow.Index > 0 Then
                drGrd.Item(grdEnm.BatchNo) = Me.cmbBatchNo.Text
            End If
            drGrd.Item(grdEnm.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            drGrd.Item(grdEnm.Qty) = Val(Me.txtQty.Text)
            drGrd.Item(grdEnm.Rate) = Val(Me.txtRate.Text)
            drGrd.Item(grdEnm.SalePrice) = Val(Me.txtSalePrice.Text)
            If pnlCost.Visible = True Then
                drGrd.Item(grdEnm.Total) = Val(Me.txtTotal.Text)
            Else
                drGrd.Item(grdEnm.Total) = Val(Me.txtSaleTotal.Text)
            End If
            drGrd.Item(grdEnm.CategoryId) = Me.cmbCategory.SelectedValue
            drGrd.Item(grdEnm.ItemId) = Me.cmbItem.ActiveRow.Cells(0).Value
            drGrd.Item(grdEnm.PackQty) = Val(Me.txtPackQty.Text)
            'Ali Faisal : TFS1376 : 24-Aug-2017
            drGrd.Item(grdEnm.CurrentPrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            'Ali Faisal : TFS1376 : 24-Aug-2017 : End
            ''Value Entered Against TFS2739
            drGrd.Item(grdEnm.BatchId) = 1
            drGrd.Item(grdEnm.Pack_Desc) = Me.cmbUnit.Text.ToString
            drGrd.Item(grdEnm.ArticleMasterId) = Val(Me.cmbCostSheetItems.Value.ToString)
            drGrd.Item(grdEnm.TotalQuantity) = Val(Me.txtTotalQuantity.Text)
            dtGrd.Rows.InsertAt(drGrd, 0)
            dtGrd.AcceptChanges()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged
        Try
            'Before against task:2366
            'If IsFormOpen = True Then FillCombo("Item")
            If IsFormOpen = True Then
                If flgLocationWiseItem = True Then FillCombo("Item")
            End If
            ''End Task:2366

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub GetTotal()
    '    Dim i As Integer
    '    Dim dblTotalAmount As Double
    '    Dim dblTotalQty As Double
    '    For i = 0 To grd.Rows.Count - 1
    '        dblTotalAmount = dblTotalAmount + Val(grd.Rows(i).Cells(6).Value)
    '        dblTotalQty = dblTotalQty + Val(grd.Rows(i).Cells(4).Value)
    '    Next
    '    txtAmount.Text = dblTotalAmount
    '    txtTotalQty.Text = dblTotalQty
    '    txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)
    '    Me.lblRecordCount.Text = "Total Records: " & Me.grd.RowCount
    'End Sub
    Private Sub FillCombo(ByVal strCondition As String)
        Dim str As String = String.Empty
        If strCondition = "Item" Then
            'Before against task:2388
            'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,ISNULL(PurchasePrice,0) as Price , ArticleDefView.SizeRangeID as [Size ID] FROM         ArticleDefView where Active=1"
            'Task:2388 Added Column ServiceItem
            'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,ISNULL(PurchasePrice,0) as Price , ArticleDefView.SizeRangeID as [Size ID], Isnull(ServiceItem,0) as ServiceItem FROM         ArticleDefView where Active=1"
            'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,ISNULL(PurchasePrice,0) as Price ,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], Isnull(ServiceItem,0) as ServiceItem FROM         ArticleDefView where Active=1"
            'Ali Faisal : Get Cost Price in Combo on 13-Feb-2017
            If Not getConfigValueByType("AvgRate") = "True" Then
                str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,ISNULL(PurchasePrice,0) as Price ,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], Isnull(ServiceItem,0) as ServiceItem, IsNull(SalePrice, 0) As [Sale Price] FROM         ArticleDefView where Active=1"
            Else
                str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,Case When ISNULL(Cost_Price,0) > 0 Then ISNULL(Cost_Price,0) Else ISNULL(PurchasePrice,0) End as Price ,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], Isnull(ServiceItem,0) as ServiceItem, IsNull(SalePrice, 0) As [Sale Price] FROM         ArticleDefView where Active=1"
            End If
            'End Task:2388
            'Comment against task:2366
            'If getConfigValueByType("ArticleFilterByLocation") = "True" Then
            'If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
            If flgLocationWiseItem = True Then
                str += " AND ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                'Else
                '    str += str
            End If
            'End Task:2366
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

            'End If
            FillUltraDropDown(Me.cmbItem, str)
            'FillDropDown(cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True 'Task:2388 Service Item Hiden 
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
                If ShowCostPriceRights = True Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Sale Price").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = False
                Else
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Sale Price").Hidden = False
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = True
                End If
            End If
        ElseIf strCondition = "Category" Then
            'Task#16092015 Load  Locations user wise
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation WHERE Location_Id <> " & IIf(Me.cmbLocation.SelectedIndex > 0, Me.cmbLocation.SelectedValue, 0) & " and Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order"
            'Else
            '    str = "Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation WHERE Location_Id <> " & IIf(Me.cmbLocation.SelectedIndex > 0, Me.cmbLocation.SelectedValue, 0) & " order by sort_order"
            'End If

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & " and ISNULL(Location_Id, 0) > 0)  " _
                    & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") AND Location_ID !=" & Me.cmbLocation.SelectedValue & " order by sort_order " _
                    & " Else " _
                    & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation WHERE Location_ID !=" & Me.cmbLocation.SelectedValue & " order by sort_order"


            FillDropDown(cmbCategory, str, False)
            'FillDropDown(Me.cmbUnitCostSheet, str, False)
        ElseIf strCondition = "ItemFilter" Then
            'Before against task:2388
            'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,PurchasePrice as Price, ArticleDefView.SizeRangeID as [Size ID] FROM         ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue
            'Task:2388 Added Column ServiceItem
            str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination,PurchasePrice as Price, ArticleDefView.SizeRangeID as [Size ID], Isnull(ServiceItem,0) as ServiceItem, IsNull(SalePrice, 0) As SalePrice FROM         ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue
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
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True 'Task:2388 Service Item Hidden
            If ShowCostPriceRights = True Then
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Sale Price").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = False
            Else
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Sale Price").Hidden = False
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = True
            End If
        ElseIf strCondition = "grdLocation" Then
            'Task#16092015 Load Locations user wise
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock From tblDefLocation WHERE Location_Id <> " & IIf(Me.cmbLocation.SelectedIndex > 0, Me.cmbLocation.SelectedValue, 0) & " and Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") "
            'Else
            '    str = "Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock From tblDefLocation WHERE Location_Id <> " & IIf(Me.cmbLocation.SelectedIndex > 0, Me.cmbLocation.SelectedValue, 0) & ""
            'End If

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & " and ISNULL(Location_Id, 0) > 0) " _
                    & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") AND Location_ID !=" & IIf(Me.cmbLocation.SelectedIndex > 0, Me.cmbLocation.SelectedValue, 0) & " order by sort_order " _
                    & " Else " _
                    & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation WHERE Location_ID !=" & IIf(Me.cmbLocation.SelectedIndex > 0, Me.cmbLocation.SelectedValue, 0) & " order by sort_order"


            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns(grdEnm.LocationId).ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")

            'ElseIf strCondition = "CostSheetItem" Then
            '    str = "SELECT ArticleId as Id, ArticleDescription Item, ArticleCode Code, PurchasePrice as Price  FROM  ArticleDefTableMaster where Active=1 and ArticleId in (select distinct masterarticleID from tblcostsheet) "
            '    'FillDropDown(cmbItem, str)
            '    FillUltraDropDown(Me.cmbCostSheetItems, str)
            '    Me.cmbCostSheetItems.Rows(0).Activate()
            '    Me.cmbCostSheetItems.DisplayLayout.Bands(0).Columns(0).Hidden = True
        ElseIf strCondition = "Ticket" Then
            'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
            str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Article.ArticleDescription As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join AllocationMaster ON Ticket.PlanTicketsId = AllocationMaster.TicketID Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And AllocationMaster.Status = 1"
            FillDropDown(cmbTicket, str)
        ElseIf strCondition = "Tickets" Then
            'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
            str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Article.ArticleDescription As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join AllocationMaster ON Ticket.PlanTicketsId = AllocationMaster.TicketID Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where AllocationMaster.Status = 1"
            FillDropDown(cmbTicket, str)
        ElseIf strCondition = "ArticlePack" Then
            Me.cmbUnit.ValueMember = "ArticlePackId"
            Me.cmbUnit.DisplayMember = "PackName"
            Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
        ElseIf strCondition = "CostSheetItem" Then

            ''Before against task:2412
            ''str = "SELECT ArticleId as Id, ArticleDescription Item, ArticleCode Code, " & IIf(StoreIssuanceDependonProductionPlan = True, " Isnull(PL.qty,0)  as [Plan Qty] ", "0  as [Plan Qty]") & ", PurchasePrice as Price, isnull(PackQty,0) as PackQty  FROM  ArticleDefTableMaster  "
            ''Task:2412 Change Validation On Plan No'
            'str = "SELECT ArticleId as Id, ArticleDescription Item, ArticleCode Code, " & IIf(Me.cmbPlan.SelectedIndex > 0, " Isnull(PL.qty,0)  as [Plan Qty] ", "0  as [Plan Qty]") & ", PurchasePrice as Price, isnull(PackQty,0) as PackQty  FROM  ArticleDefTableMaster  "

            ''Before against task:2412
            ''If StoreIssuanceDependonProductionPlan = True Then
            'If Me.cmbPlan.SelectedIndex > 0 Then
            '    'End Task:2412
            '    If Not Me.cmbPlan.SelectedIndex = -1 Then
            '        'str += " LEFT OUTER JOIN (SELECT  ArticleDefView.MasterId as ArticleDefId, SUM(ISNULL(Qty, 0)) AS Qty FROM   dbo.PlanDetailTable INNER JOIN ArticleDefView on ArticleDefView.ArticleId = PlanDetailTable.ArticleDefId WHERE PlanId=" & Me.cmbPlan.SelectedValue & " GROUP BY PlanDetailTable.ArticleDefId,ArticleDefView.MasterId) PL on PL.ArticleDefId =  ArticleDefTableMaster.ArticleId"
            '        str += " LEFT OUTER JOIN (SELECT  ArticleDefView.MasterId as ArticleDefId, SUM(ISNULL(Qty, 0)) AS Qty FROM   dbo.PlanDetailTable INNER JOIN ArticleDefView on ArticleDefView.ArticleId = PlanDetailTable.ArticleDefId WHERE PlanId=" & Me.cmbPlan.SelectedValue & " GROUP BY PlanDetailTable.ArticleDefId,ArticleDefView.MasterId Having SUM(ISNULL(Qty, 0)) <> 0) PL on PL.ArticleDefId =  ArticleDefTableMaster.ArticleId"
            '    End If
            'End If
            'str += " where Active=1  and ArticleId in (select distinct masterarticleID from tblcostsheet)"
            'If flgCompanyRights = True Then
            '    str += " AND ArticleDefTableMaster.ArticleGroupId IN(Select ArticleGroupId From ArticleGroupDefTable WHERE SubSubId in (Select main_sub_sub_id From tblCOAMainSubSub WHERE CompanyId=" & MyCompanyId & "))"
            'End If
            ''Before against task:2412
            ''If StoreIssuanceDependonProductionPlan = True Then
            ''If Not Me.cmbPlan.SelectedIndex < 0 Then
            'If Me.cmbPlan.SelectedIndex > 0 Then
            '    str += " AND ArticleId In (Select DISTINCT MasterId From PlanMasterTable INNER JOIN PlanDetailTable On PlanMasterTable.PlanId = PlanDetailTable.PlanId INNER JOIN ArticleDefView on ArticleDefView.ArticleId = PlanDetailTable.ArticleDefId WHERE PlanMasterTable.PlanId=" & Me.cmbPlan.SelectedValue & ")"
            'End If
            ''End If
            ''End Task:2412
            ''FillDropDown(cmbItem, str)
            'FillUltraDropDown(Me.cmbCostSheetItems, str)
            'Me.cmbCostSheetItems.Rows(0).Activate()
            'Me.cmbCostSheetItems.DisplayLayout.Bands(0).Columns(0).Hidden = True
            'Me.cmbCostSheetItems.DisplayLayout.Bands(0).Columns("PackQty").Hidden = True
            'Before against task:2412
            'str = "SELECT ArticleId as Id, ArticleDescription Item, ArticleCode Code, " & IIf(StoreIssuanceDependonProductionPlan = True, " Isnull(PL.qty,0)  as [Plan Qty] ", "0  as [Plan Qty]") & ", PurchasePrice as Price, isnull(PackQty,0) as PackQty  FROM  ArticleDefTableMaster  "
            'Task:2412 Change Validation On Plan No'
            str = "SELECT ArticleId as Id, ArticleDescription Item, ArticleCode Code, " & IIf(Me.cmbPlan.SelectedIndex > 0, " Isnull(PL.qty,0)  as [Plan Qty] ", "0  as [Plan Qty]") & ", PurchasePrice as Price, isnull(PackQty,0) as PackQty  FROM  ArticleDefTableMaster  "

            'Before against task:2412
            'If StoreIssuanceDependonProductionPlan = True Then
            If Me.cmbPlan.SelectedIndex > 0 Then
                'End Task:2412
                If Not Me.cmbPlan.SelectedIndex = -1 Then
                    'str += " LEFT OUTER JOIN (SELECT  ArticleDefView.MasterId as ArticleDefId, SUM(ISNULL(Qty, 0)) AS Qty FROM   dbo.PlanDetailTable INNER JOIN ArticleDefView on ArticleDefView.ArticleId = PlanDetailTable.ArticleDefId WHERE PlanId=" & Me.cmbPlan.SelectedValue & " GROUP BY PlanDetailTable.ArticleDefId,ArticleDefView.MasterId) PL on PL.ArticleDefId =  ArticleDefTableMaster.ArticleId"
                    str += " LEFT OUTER JOIN (SELECT  ArticleDefView.MasterId as ArticleDefId, SUM(ISNULL(Qty, 0)) AS Qty FROM   dbo.PlanDetailTable INNER JOIN ArticleDefView on ArticleDefView.ArticleId = PlanDetailTable.ArticleDefId WHERE PlanId=" & Me.cmbPlan.SelectedValue & " GROUP BY PlanDetailTable.ArticleDefId,ArticleDefView.MasterId Having SUM(ISNULL(Qty, 0)) <> 0) PL on PL.ArticleDefId =  ArticleDefTableMaster.ArticleId"
                End If
            End If
            str += " where Active=1  and ArticleId in (select distinct masterarticleID from tblcostsheet)"
            If flgCompanyRights = True Then
                str += " AND ArticleDefTableMaster.ArticleGroupId IN(Select ArticleGroupId From ArticleGroupDefTable WHERE SubSubId in (Select main_sub_sub_id From tblCOAMainSubSub WHERE CompanyId=" & MyCompanyId & "))"
            End If
            'Before against task:2412
            'If StoreIssuanceDependonProductionPlan = True Then
            'If Not Me.cmbPlan.SelectedIndex < 0 Then
            If Me.cmbPlan.SelectedIndex > 0 Then
                str += " AND ArticleId In (Select DISTINCT MasterId From PlanMasterTable INNER JOIN PlanDetailTable On PlanMasterTable.PlanId = PlanDetailTable.PlanId INNER JOIN ArticleDefView on ArticleDefView.ArticleId = PlanDetailTable.ArticleDefId WHERE PlanMasterTable.PlanId=" & Me.cmbPlan.SelectedValue & ")"
            End If
            'End If
            'End Task:2412
            'FillDropDown(cmbItem, str)
            FillUltraDropDown(Me.cmbCostSheetItems, str)
            Me.cmbCostSheetItems.Rows(0).Activate()
            Me.cmbCostSheetItems.DisplayLayout.Bands(0).Columns(0).Hidden = True
            Me.cmbCostSheetItems.DisplayLayout.Bands(0).Columns("PackQty").Hidden = True
        ElseIf strCondition = "Plan" Then
            str = "Select PlanId, PlanNo From PlanmasterTable " & IIf(IsEditMode = False, " WHERE Status='Open'", "") & "  Order By PlanNo Desc"
            FillDropDown(Me.cmbPlan, str)
            ''TFS
        ElseIf strCondition = "BatchNo" Then
            str = "Select  DISTINCT BatchNo, BatchNo From  StockDetailTable  WHERE BatchNo NOT IN ('', '0','xxxx') AND LocationId=" & Val(Me.cmbCategory.SelectedValue) & ""
            If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                If flgLocationWiseItem = True Then
                    ''str += " AND LocationId=" & Me.cmbCategory.SelectedValue & " "
                End If
            End If
            If cmbItem.SelectedRow.Index > 0 Then
                str += " And ArticledefId=" & Me.cmbItem.Value & " "
            End If
            str += "  Group By BatchNo Having Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0)) > 0 ORDER BY StockDetailTable.BatchNo ASC"
            FillUltraDropDown(Me.cmbBatchNo, str)
            Me.cmbBatchNo.DisplayLayout.Bands(0).Columns(0).Hidden = True
            cmbBatchNo.Rows(0).Activate()
            ''Start TFS4181
            'ElseIf strCondition = "grdBatchNo" Then
            'str = "Select  BatchNo,BatchNo,ExpiryDate, Origin  From  StockDetailTable  where BatchNo not in ('','0','xxxx') Group by BatchNo,ExpiryDate, Origin Having (Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0))) > 0 ORDER BY ExpiryDate  desc "
            'Dim dt As DataTable = GetDataTable(str)
            'Me.grd.RootTable.Columns(grdEnm.BatchNo).ValueList.PopulateValueList(dt.DefaultView, "BatchNo", "BatchNo")
            ''End TFS4181
            'Changes for customer dropdown (01/16/2023)
        ElseIf strCondition = "Customer" Then
            str = "select CustomerID,CustomerName from tblCustomer where ISNULL(Active,0)=1"
            FillUltraDropDown(Me.cmbCustomer1, str)
            Me.cmbCustomer1.Rows(0).Activate()
            Me.cmbCustomer1.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
            Me.cmbCustomer1.DisplayLayout.Bands(0).Columns("BatchNo").Hidden = True
            'Changes for customer dropdown (01/16/2023)
        ElseIf strCondition = "Transporter" Then
            str = "select * from tbldeftransporter where active=1 order by sortorder,2"
            FillUltraDropDown(Me.cmbTransporter, str)
            Me.cmbTransporter.Rows(0).Activate()
        ElseIf strCondition = "Currency" Then
            str = "select * from tblcurrency"
            FillUltraDropDown(Me.cmbcurrency, str)
            Me.cmbcurrency.Rows(0).Activate()
        ElseIf strCondition = "GrdOrigin" Then
            str = "select CountryName, CountryName From tblListCountry Where Active = 1"
            Dim dtCountry As DataTable = GetDataTable(str)
            dtCountry.AcceptChanges()
            Me.grd.RootTable.Columns("Origin").ValueList.PopulateValueList(dtCountry.DefaultView, "CountryName", "CountryName")
        End If



    End Sub

    Private Sub txtPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaid.TextChanged
        'txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)
        txtBalance.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(txtPaid.Text)
    End Sub

    Private Function Save() As Boolean

        Me.grd.UpdateData()
        Me.txtPONo.Text = GetDocumentNo()  'GetNextDocNo("DN", 6, "DispatchMasterTable", "DispatchNo")
        setVoucherNo = Me.txtPONo.Text
        Dim StockTransfer As Boolean = Convert.ToBoolean(getConfigValueByType("StockTransferFromDispatch").ToString)
        Dim blnCheckCurrentStockByItem As Boolean = False
        If Not getConfigValueByType("CheckCurrentStockByItem").ToString = "Error" Then
            blnCheckCurrentStockByItem = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)
        End If
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer

        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        'Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo.Text)
        'Dim AccountId As Integer = GetConfigValue("PurchaseDebitAccount")
        'Dim strvoucherNo As String = GetNextDocNo("PV", 6, "tblVoucher", "voucher_no")


        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try

            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'Before against request no. 934
            'objCommand.CommandText = "Insert into DispatchMasterTable (locationId,DispatchNo,DispatchDate,DispatchQty,DispatchAmount, CashPaid, Remarks,UserName, VendorID, RefDocument) values( " _
            '                        & Me.cmbLocation.SelectedValue & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text & "',N'" & LoginUserName & "', " & Me.cmbLocation.SelectedValue & ", N'" & Me.txtReceivingNo.Text.ToString & "') Select @@Identity"
            'ReqId-934 Resolve Comma Error
            'objCommand.CommandText = "Insert into DispatchMasterTable (locationId,DispatchNo,DispatchDate,DispatchQty,DispatchAmount, CashPaid, Remarks,UserName, VendorID, RefDocument) values( " _
            '                      & Me.cmbLocation.SelectedValue & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', " & Me.cmbLocation.SelectedValue & ", N'" & Me.txtReceivingNo.Text.ToString & "') Select @@Identity"
            'POID = objCommand.ExecuteScalar()
            objCommand.CommandText = "Insert into DispatchMasterTable (locationId,DispatchNo,DispatchDate,DispatchQty,DispatchAmount, CashPaid, Remarks,UserName, VendorID, RefDocument,PlanId,Issued, PlanTicketId ,CustomerId, QuoteReference,TrackingNo,TransporterId,PaymentTerm,CreditDays,IncoTermSite,currency_id, IsBulkStockTransferred) values( " _
                                 & Me.cmbLocation.SelectedValue & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQuantity"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', " & Me.cmbLocation.SelectedValue & ", N'" & Me.txtReceivingNo.Text.ToString & "'," & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ", 1 ," & IIf(Me.cmbTicket.SelectedIndex = -1, 0, Me.cmbTicket.SelectedValue) & "," & Me.cmbCustomer1.Value & ", N'" & txtQuoteRef.Text & "', N'" & txtTrackingNo.Text & "'," & Me.cmbTransporter.Value & ",'" & Me.txtpaymentterm.Text & "','" & Me.txtcreditday.Text & "','" & Me.txtincotermsite.Text & "'," & Me.cmbcurrency.Value & ",1) Select @@Identity"
            POID = objCommand.ExecuteScalar()
            getVoucher_Id = POID
            objCommand.CommandText = ""
            For i = 0 To grd.RowCount - 1
                objCommand.CommandText = ""
                If blnCheckCurrentStockByItem = True Then
                    CheckCurrentStockByItem(Me.grd.GetRows(i).Cells(grdEnm.ItemId).Value, Val(Me.grd.GetRows(i).Cells(grdEnm.TotalQuantity).Value), Me.grd, Me.txtPONo.Text, trans)
                End If
                'task 2644 Check to duplicate Engine or Chesis No
                'Dim dtDispatchDetail As DataTable = GetDataTable("SELECT * FROM DispatchDetailTable WHERE (Engine_No <> '' OR Chassis_No <> '')", trans)
                ''TASK 2644 Check duplication of Engine no and chesis no then Insert Record
                'For j As Integer = 0 To dtDispatchDetail.Rows.Count - 1
                '    'If Not IsDBNull(dtDispatchDetail.Rows(j).Item("Engine_No").ToString) Then
                '    'End If
                '    If dtDispatchDetail.Rows(j).Item("Engine_No").ToString.Length > 0 Or Me.grd.GetRows(i).Cells("Engine No").Value.ToString.Length > 0 Then
                '        If dtDispatchDetail.Rows(j).Item("Engine_No").ToString = Me.grd.GetRows(i).Cells("Engine No").Value.ToString Or dtDispatchDetail.Rows(j).Item("Chesis_No").ToString = Me.grd.GetRows(i).Cells("Chesis No").ToString Then
                '            Throw New Exception("Engine no [" & Me.grd.GetRows(i).Cells("Engine No").Value.ToString & "] already exists")
                '        End If
                '    End If

                '    objCommand.CommandText = "Insert into DispatchDetailTable (DispatchId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Pack_Desc, Engine_No, Chesis_No) values( " _
                '            & " ident_current('DispatchMasterTable')," & Val(grd.GetRows(i).Cells(grdEnm.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(grdEnm.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(grdEnm.Qty).Value) & ", " _
                '            & " " & IIf(grd.GetRows(i).Cells(grdEnm.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(grdEnm.Qty).Value), (Val(grd.GetRows(i).Cells(grdEnm.Qty).Value) * Val(grd.GetRows(i).Cells(grdEnm.PackQty).Value))) & ", " & Val(grd.GetRows(i).Cells(grdEnm.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(grdEnm.PackQty).Value) & " , " & Val(grd.GetRows(i).Cells(grdEnm.CurrentPrice).Value) & " , N'" & grd.GetRows(i).Cells(grdEnm.BatchNo).Value & "'," & grd.GetRows(i).Cells(grdEnm.BatchId).Value & "," & grd.GetRows(i).Cells(grdEnm.LocationId).Value & ", N'" & grd.GetRows(i).Cells(grdEnm.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("Engine No").Value.ToString & "', N'" & grd.GetRows(i).Cells("Chesis No").Value.ToString & "')"

                '    objCommand.ExecuteNonQuery()
                '    ' end task 2644   
                'Next
                '
                'Val(grd.Rows(i).Cells(5).Value)


                Dim strDisp As String = String.Empty
                strDisp = "SELECT * FROM DispatchDetailTable WHERE (Engine_No <> '' OR Chassis_No <> '') "
                If Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Length > 0 Then
                    strDisp += " And Engine_No=N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "'"
                End If
                If Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Length > 0 Then
                    strDisp += " And Chassis_No=N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "'"
                End If
                Dim dtDispatchDetail As DataTable = GetDataTable(strDisp, trans)
                'TASK 2644 Check duplication of Engine no and chesis no then Insert Record
                'If Not IsDBNull(dtDispatchDetail.Rows(j).Item("Engine_No").ToString) Then
                'End If
                If dtDispatchDetail.Rows.Count > 0 Then
                    If Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Length > 0 Then
                        If dtDispatchDetail.Rows(0).Item("Engine_No").ToString = Me.grd.GetRows(i).Cells("Engine_No").Value.ToString Then
                            Throw New Exception("Engine no [" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString & "] already exists")
                        End If
                    End If
                    If Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Length > 0 Then
                        If dtDispatchDetail.Rows(0).Item("Chassis_No").ToString = Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString Then
                            Throw New Exception("Chassis No [" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString & "] already exists")
                        End If
                    End If
                End If
                'objCommand.CommandText = "Insert into DispatchDetailTable (DispatchId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Pack_Desc, Engine_No, Chassis_No) values( " _
                '        & " ident_current('DispatchMasterTable')," & Val(grd.GetRows(i).Cells(grdEnm.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(grdEnm.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(grdEnm.Qty).Value) & ", " _
                '        & " " & IIf(grd.GetRows(i).Cells(grdEnm.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(grdEnm.Qty).Value), (Val(grd.GetRows(i).Cells(grdEnm.Qty).Value) * Val(grd.GetRows(i).Cells(grdEnm.PackQty).Value))) & ", " & Val(grd.GetRows(i).Cells(grdEnm.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(grdEnm.PackQty).Value) & " , " & Val(grd.GetRows(i).Cells(grdEnm.CurrentPrice).Value) & " , N'" & grd.GetRows(i).Cells(grdEnm.BatchNo).Value & "'," & grd.GetRows(i).Cells(grdEnm.BatchId).Value & "," & grd.GetRows(i).Cells(grdEnm.LocationId).Value & ", N'" & grd.GetRows(i).Cells(grdEnm.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "' )"

                'objCommand.ExecuteNonQuery()
                ' end task 2644   
                objCommand.CommandText = "Insert into DispatchDetailTable (DispatchId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Pack_Desc, Engine_No, Chassis_No, SalePrice,ExpiryDate,IncoTerm,CommercialPrice, Origin) values( " _
                       & " ident_current('DispatchMasterTable')," & Val(grd.GetRows(i).Cells(grdEnm.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(grdEnm.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(grdEnm.Qty).Value) & ", " _
                       & " " & Val(grd.GetRows(i).Cells(grdEnm.TotalQuantity).Value) & ", " & Val(grd.GetRows(i).Cells(grdEnm.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(grdEnm.PackQty).Value) & " , " & Val(grd.GetRows(i).Cells(grdEnm.CurrentPrice).Value) & " , N'" & grd.GetRows(i).Cells(grdEnm.BatchNo).Value & "'," & grd.GetRows(i).Cells(grdEnm.BatchId).Value & "," & grd.GetRows(i).Cells(grdEnm.LocationId).Value & ", N'" & grd.GetRows(i).Cells(grdEnm.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells(grdEnm.SalePrice).Value) & "," & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value.ToString = "", "NULL", "Convert(DateTime,N'" & CDate(IIf(Me.grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value.ToString = "", Date.Now, Me.grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", N'" & Me.grd.GetRows(i).Cells("incoterm").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("CommercialPrice").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Origin").Value.ToString.Replace("'", "''") & "')"

                objCommand.ExecuteNonQuery()




                If Me.cmbPlan.SelectedIndex > 0 Then
                    'End Task:2412
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update PlanCostSheetDetailTable Set IssuedQty=IssuedQty+" & Val(grd.GetRows(i).Cells(grdEnm.TotalQuantity).Value.ToString) & " WHERE PlanCostSheetDetailTable.PlanId=" & IIf(Me.cmbPlan.SelectedIndex - 1, 0, Me.cmbPlan.SelectedValue) & " AND PlanCostSheetDetailTable.ArticleDefId=" & Val(grd.GetRows(i).Cells(grdEnm.ItemId).Value.ToString) & " AND PlanCostSheetDetailTable.ArticleMasterId=" & Val(grd.GetRows(i).Cells(grdEnm.ArticleMasterId).Value.ToString)
                    objCommand.ExecuteNonQuery()
                End If






            Next


            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
            '                           & " cheque_no, cheque_date,post,Source,voucher_code)" _
            '                           & " VALUES(" & gobjLocationId  & ", 1,  6 , N'" & strvoucherNo & "', N'" & Me.dtpPODate.Value & "', '" _
            '                           & "NULL" & "', N'" & Nothing & "', 0,N'" & Me.Name & "',N'" & Me.txtPONo.Text & "')" _
            '                           & " SELECT @@IDENTITY"

            'objCommand.Transaction = trans

            'lngVoucherMasterId = objCommand.ExecuteScalar

            ''***********************
            ''Deleting Detail
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId

            'objCommand.Transaction = trans

            'objCommand.ExecuteNonQuery()

            ''***********************
            ''Inserting Debit Amount
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & Val(Me.txtAmount.Text) & ", 0)"

            'objCommand.Transaction = trans
            'objCommand.ExecuteNonQuery()

            ''***********************
            ''Inserting Credit Amount
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtAmount.Text) & ")"
            'objCommand.Transaction = trans
            'objCommand.ExecuteNonQuery()



            If Me.cmbPlan.SelectedIndex > 0 Then
                'End Task:2412
                objCommand.CommandText = ""
                objCommand.CommandText = "SELECT ISNULL(SUM(ISNULL(PlanQty, 0) - ISNULL(IssuedQty, 0)), 0) AS Qty FROM  dbo.PlanCostSheetDetailTable WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ""
                Dim objDa As New OleDbDataAdapter
                Dim objDt As New DataTable
                objDa.SelectCommand = objCommand
                objDa.Fill(objDt)
                If objDt IsNot Nothing Then
                    If objDt.Rows.Count > 0 Then
                        If objDt.Rows(0).Item(0) <> 0 Then
                            objCommand.CommandText = ""
                            objCommand.CommandText = "Update PlanMasterTable Set Status=N'" & EnumStatus.Open.ToString & "' WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                            objCommand.ExecuteNonQuery()
                        Else
                            objCommand.CommandText = ""
                            objCommand.CommandText = "Update PlanMasterTable Set Status=N'" & EnumStatus.Close.ToString & "' WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                            objCommand.ExecuteNonQuery()
                        End If
                    Else
                        objCommand.CommandText = ""
                        objCommand.CommandText = "Update PlanMasterTable Set Status=N'" & EnumStatus.Close.ToString & "' WHERE PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                        objCommand.ExecuteNonQuery()
                    End If
                End If
            End If

            _objTrans = trans

            If IsValidate() = True Then
                StockMaster.DocNo = Me.txtPONo.Text
                Call New StockDAL().Add(StockMaster, trans)
            End If

            If StockTransfer = True Then
                FillModel("StockTransferFromDispatch")
                Call New SBDal.StockReceivingDAL().Add(StockReceivingMaster, trans)
            End If

            If StockTransfer = True Then
                Me.txtReceivingNo.Text = StockReceivingMaster.ReceivingNo
                FillModel("StockReceiving")
                Call New StockDAL().Add(StockReceivingMaster.StockMaster, trans) 'Upgrading Stock ...
            End If


            trans.Commit()
            'If StockTransfer = True Then
            '    FillModel("StockTransferFromDispatch")
            '    Call New SBDal.StockReceivingDAL().Add(StockReceivingMaster)
            'End If
            Save = True
            Try
                ''insert Activity Log
                SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.StockDispatch, Me.txtPONo.Text.Trim)
            Catch ex As Exception

            End Try
            'insertvoucher()
            'Call Save1() 'Upgrading Stock ...
            'If StockTransfer = True Then
            '    FillModel("StockReceiving")
            '    Call New StockDAL().Add(StockReceivingMaster.StockMaster) 'Upgrading Stock ...
            'End If
            setEditMode = False
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)


            'Ali Faisal : TFS857 : Add notification on Stock Dispatch by Ali Faisal on 30-Aug-2017

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
            objmod.Notification.NotificationTitle = "Stock dispatched from location [" & Me.cmbCategory.Text & "] to location [" & cmbLocation.Text & "] against doc#: " & Me.txtPONo.Text & " "

            '// Preparing notification description string
            objmod.Notification.NotificationDescription = "Stock dispatched from location [" & Me.cmbCategory.Text & "] to [" & cmbLocation.Text & "] against doc# [" & Me.txtPONo.Text & "] of Qty [" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "] saved by " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

            '// Setting source application as refrence in the notification
            objmod.Notification.SourceApplication = "Stock Dispatch"



            '// Starting to get users list to add child

            '// Creating notification detail object list
            Dim List As New List(Of NotificationDetail)

            '// Getting users list
            List = NDal.GetNotificationUsers("Dispatch Created")

            '// Adding users list in the Notification object of current inquiry
            objmod.Notification.NotificationDetils.AddRange(List)

            '// Getting and adding user groups list in the Notification object of current inquiry
            objmod.Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("Dispatch Created"))

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

            'Ali Faisal : TFS857 : Add notification on Stock Dispatch by Ali Faisal on 30-Aug-2017

        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try
        ' Me.RefreshControls() R933 Commented
    End Function

    Sub InsertVoucher()

        Try
            'SaveVoucherEntry(GetVoucherTypeId("SV"), GetNextDocNo("SV", 6, "tblVoucher", "voucher_no"), Me.dtpPODate.Value, "", Nothing, GetConfigValue("DispatchCreditAccount"), Val(Me.cmbVendor.ActiveRow.Cells(0).Text.ToString), Val(Me.txtAmount.Text), Val(Me.txtAmount.Text), "Both", Me.Name, Me.txtPONo.Text, True)
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try

    End Sub

    Private Function FormValidate() As Boolean

        If txtPONo.Text = "" Then
            msg_Error("Please enter Dispatch No.")
            txtPONo.Focus() : FormValidate = False : Exit Function
        End If
        If Me.cmbLocation.SelectedIndex <= 0 Then
            msg_Error("Please select Location")
            cmbLocation.Focus() : FormValidate = False : Exit Function
        End If

        If gobjLocationId = Me.cmbLocation.SelectedValue Then
            msg_Error("Please select different location")
            Me.cmbLocation.Focus() : FormValidate = False : Exit Function
        End If

        If Not Me.grd.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            cmbItem.Focus() : FormValidate = False : Exit Function
        End If

        'Task:2644 Validation Engine No and Chassis No.
        Me.grd.UpdateData()

        Dim StockOutConfigration As String = "" ''1596
        ''Start Task 1596
        If Not getConfigValueByType("StockOutConfigration").ToString = "Error" Then ''1596
            StockOutConfigration = getConfigValueByType("StockOutConfigration").ToString
        End If
        'ShowInformationMessage(StockOutConfigration)
        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            If StockOutConfigration.Equals("Required") AndAlso r.Cells(grdEnm.BatchNo).Value.ToString = String.Empty Then
                msg_Error("Please Enter Value in Batch No")
                Return False
                Exit For
            End If
        Next
        'End Task:1596
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

        'End Task:2644

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
            'txtReceivingID.Text = grdSaved.CurrentRow.Cells("DispatchId").Value
            ''TODO. ----
            'cmbVendor.Value = grdSaved.CurrentRow.Cells(3).Value

            'txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
            'txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""
            ''Me.cmbSalesMan.SelectedValue = grdSaved.CurrentRow.Cells("EmployeeCode").Value.ToString
            'Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value
            'Call DisplayDetail(grdSaved.CurrentRow.Cells("DispatchId").Value)
            'GetTotal()
            'Me.SaveToolStripButton.Text = "&Update"
            'Me.cmbPo.Enabled = False


            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If blnUpdateAll = False Then
                If Me.grd.RowCount > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                End If
            End If
            IsEditMode = True
            'Me.FillCombo("SOComplete") 'R933 Commented 
            txtPONo.Text = grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchNo).Value
            Me.txtReceivingNo.Text = grdSaved.GetRow.Cells(EnumGridSaved.RefDocument).Text.ToString
            dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchDate).Value, Date)
            txtDispatchID.Text = grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchId).Value
            Me.cmbLocation.SelectedValue = grdSaved.CurrentRow.Cells(EnumGridSaved.VendorId).Value 'cmbVendor.FindStringExact((grdSaved.CurrentRow.Cells(3).Value))
            'Me.cmbCustomer.Value = grdSaved.CurrentRow.Cells(EnumGridSaved.CustomerID).Value
            'cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PurchaseOrderid").Value
            txtRemarks.Text = grdSaved.CurrentRow.Cells(EnumGridSaved.Remarks).Value & ""
            txtPaid.Text = grdSaved.CurrentRow.Cells(EnumGridSaved.CashPaid).Value & ""
            IsBulkStockTransfer = CBool(grdSaved.CurrentRow.Cells("IsBulkStockTransferred").Value)
            DisplayDetail(grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchId).Value.ToString)
            Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns(grdEnm.Total), Janus.Windows.GridEX.AggregateFunction.Sum)
            Me.cmbCustomer1.Value = grdSaved.CurrentRow.Cells("CustomerID").Value
            txtQuoteRef.Text = grdSaved.CurrentRow.Cells("QuoteReference").Value
            txtTrackingNo.Text = grdSaved.CurrentRow.Cells("TrackingNo").Value
            txtincotermsite.Text = grdSaved.CurrentRow.Cells("IncoTermSite").Value
            txtpaymentterm.Text = grdSaved.CurrentRow.Cells("PaymentTerm").Value
            txtcreditday.Text = grdSaved.CurrentRow.Cells("CreditDays").Value
            Me.cmbTransporter.Value = grdSaved.CurrentRow.Cells("TransporterId").Value
            Me.cmbcurrency.Value = grdSaved.CurrentRow.Cells("Currency_id").Value
            'GetTotal()
            'Me.cmbPo.Enabled = False
            Me.cmbLocation.Enabled = False
            Me.BtnSave.Text = "&Update"
            GetSecurityRights()
            Me.Button1.Enabled = False
            Me.cmbLocation.Enabled = False
            If blnUpdateAll = False Then Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
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
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnPrint.Visible = True
            Me.BtnDelete.Visible = True
            ''''''''''''''''''''''''''
            'FillModel()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DisplayPODetail(ByVal ReceivingID As Integer, Optional ByVal Condition As String = "")
        Try

            Dim str As String = String.Empty
            'Dim i As Integer
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, 'xxxx' as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '      & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, 0 as BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc " _
            '      & " FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '      & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
            If Condition = String.Empty Then
                ''TASK-408 replace Pack Qty * Qty * Rate or Qty * Rate with TotalQty * Rate As Total 
                str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, ArticleSizeDefTable.ArticleSizeName as Size, ArticleColorDefTable.ArticleColorName as Color, 'xxxx' as BatchNo,getDate() as ExpiryDate,'' as Origin, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, 0 as SalePrice, " _
                   & " (IsNull(Recv_D.Qty, 0)*IsNull(Recv_D.Price, 0)) AS Total, " _
                   & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.Price as CurrentPrice, 0 as BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc , '' as Engine_No, '' as Chassis_No, Article.MasterID as ArticleMasterId, ISNULL(Recv_D.Qty, 0) As TotalQuantity " _
                   & " FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
                   & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                   & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
                   & " Left Outer Join ArticleColorDefTable On Article.ArticleColorId = ArticleColorDefTable.ArticleColorId Left Outer Join ArticleSizeDefTable on Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
                   & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""
            ElseIf Condition = "Plan" Then
                str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item ,Article.ArticleSizeName as Size, Article.ArticleColorName as Color, 'xxxx' as BatchNo,getDate() as ExpiryDate,'' as Origin, Recv_D.ArticleSize AS unit, (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) AS Qty, Recv_D.Price as Rate, 0 as SalePrice, " _
                                           & " CASE WHEN recv_d.articlesize = 'Loose' THEN Convert(float, ((Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) * Recv_D.Price)) ELSE Convert(float,(((Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0))  * Article.PackQty) * Recv_D.Price)) END AS Total, " _
                                           & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.PackQty, Recv_D.Price as CurrentPrice, 0 as BatchID, Recv_D.ArticleSize as Pack_Desc, '' as Engine_No,'' as Chassis_No,Article.MasterID as ArticleMasterId, (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) As TotalQuantity FROM dbo.PlanCostSheetDetailTable Recv_D INNER JOIN " _
                                           & " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId  LEFT OUTER JOIN ArticleColorDefTable On ArticleColorDefTable.ArticleColorId = Article.ArticleColorId " _
                                           & " LEFT OUTER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = Recv_D.ArticleMasterId LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId " _
                                           & " Where Recv_D.PlanId =" & Me.cmbPlan.SelectedValue & " AND (Isnull(Recv_D.PlanQty,0)-isnull(IssuedQty,0)) <> 0  AND Recv_D.PlanId in (Select PlanId From PlanDetailTable) "
            End If
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, 'xxxx' as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '  & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, 0 as BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Engine_No as [Engine No], Chesis_No as [Chesis No] " _
            '  & " FROM dbo.PurchaseOrderDetailTable Recv_D INNER JOIN " _
            '  & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '  & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '  & " Where Recv_D.PurchaseOrderID =" & ReceivingID & ""


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
            'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

            '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)("BatchID"))

            '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

            'Next
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            dtDisplayDetail.AcceptChanges()
            ''Commented below row against TASK-408 to add TotalQty instead Qty
            ''dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',((PackQty*Qty)*Rate), (Qty*Rate))"
            dtDisplayDetail.Columns("Total").Expression = "[TotalQuantity]*[Rate]"
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            ''Start TFS4181
            'Me.grd.RootTable.Columns(grdEnm.BatchNo).HasValueList = True
            'Me.grd.RootTable.Columns(grdEnm.BatchNo).LimitToList = False
            'Me.grd.RootTable.Columns(grdEnm.BatchNo).EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grd.RootTable.Columns("Origin").HasValueList = True
            Me.grd.RootTable.Columns("Origin").LimitToList = False
            Me.grd.RootTable.Columns("Origin").EditType = Janus.Windows.GridEX.EditType.Combo
            ''End TFS4181
            ApplyGridSettings()
            FillCombo("grdLocation")
            ''Ayesha Rehman: TFS4181: 16-08-2018 : Fill combo boxes
            'FillCombo("grdBatchNo")
            ''Ayesha Rehman : TFS4181 : 16-08-2018 : End
            FillCombo("GrdOrigin")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub DisplayDetail(ByVal ReceivingID As Integer)
        Try
            Dim str As String = String.Empty
            'Dim i As Integer
            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item,Recv_D.BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '      & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice,Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc FROM dbo.DispatchDetailTable Recv_D INNER JOIN " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '      & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '      & " Where Recv_D.DispatchID =" & ReceivingID & ""

            'task 2644 Bind Engine No and Chesis No in grid
            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item,Recv_D.BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '  & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice,Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No as [Engine_No], Recv_D.Chassis_No as [Chassis_No] FROM dbo.DispatchDetailTable Recv_D INNER JOIN " _
            '  & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '  & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '  & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '  & " Where Recv_D.DispatchID =" & ReceivingID & ""
            'end task 2644

            ''Commented below against TASK-408 on 13-06-2016 to add TotalQty instead of Qty
            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item,Recv_D.BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            ' & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            ' & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice,Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No as [Engine_No], Recv_D.Chassis_No as [Chassis_No], Article.MasterID as ArticleMasterId, IsNull(Recv_D.Qty, 0) As TotalQuantity FROM dbo.DispatchDetailTable Recv_D INNER JOIN " _
            ' & " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            ' & " tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            ' & " Where Recv_D.DispatchID =" & ReceivingID & ""
            ''' TASK TFS1417 Set rights based SalePrice on 12-09-2017
            ''TFS4347 : Ayesha Rehman : Added Column Size and Color
            '  str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleSizeName as Size, Article.ArticleColorName as Color, Recv_D.BatchNo,Recv_D.ExpiryDate, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, IsNull(Recv_D.SalePrice,0) SalePrice," _
            '& " (IsNull(Recv_D.Qty, 0)* " & IIf(ShowCostPriceRights = True, "IsNull(Recv_D.Price, 0)", "IsNull(Recv_D.SalePrice, 0)") & ") AS Total, " _
            '& " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,IsNull(Recv_D.CurrentPrice,0) CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No as [Engine_No], Recv_D.Chassis_No as [Chassis_No], Article.MasterID as ArticleMasterId, IsNull(Recv_D.Qty, 0) As TotalQuantity FROM dbo.DispatchDetailTable Recv_D INNER JOIN " _
            '& " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '& " tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '& " Where Recv_D.DispatchID =" & ReceivingID & ""

            '   str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleSizeName as Size, Article.ArticleColorName as Color, Recv_D.BatchNo,Recv_D.ExpiryDate, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, IsNull(Recv_D.SalePrice,0) SalePrice," _
            '& " (IsNull(Recv_D.Qty, 0)* " & IIf(ShowCostPriceRights = True, "IsNull(Recv_D.Price, 0)", "IsNull(Recv_D.SalePrice, 0)") & ") AS Total, " _
            '& " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,IsNull(Recv_D.CurrentPrice,0) CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No as [Engine_No], Recv_D.Chassis_No as [Chassis_No], Article.MasterID as ArticleMasterId, IsNull(Recv_D.Qty, 0) As TotalQuantity FROM dbo.DispatchDetailTable Recv_D INNER JOIN " _
            '& " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '& " tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '& " Where Recv_D.DispatchID =" & ReceivingID & ""

            str = "SELECT DDT.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleSizeName as Size, Article.ArticleColorName as Color, DDT.BatchNo,DDT.ExpiryDate,DDT.Origin, " _
            & "DDT.ArticleSize AS unit, DDT.Sz1 AS Qty, DDT.Price as Rate, IsNull(DDT.SalePrice,0) SalePrice, (IsNull(DDT.Qty, 0)* IsNull(DDT.Price, 0)) AS Total," _
            & "Article.ArticleGroupId as CategoryId, DDT.ArticleDefId as ItemId,DDT.Sz7 as PackQty,IsNull(DDT.CurrentPrice,0) CurrentPrice, DDT.BatchID," _
            & "Isnull(DDT.Pack_Desc,DDT.ArticleSize) as Pack_Desc, DDT.Engine_No as [Engine_No], DDT.Chassis_No as [Chassis_No], Article.MasterID as ArticleMasterId, " _
            & "IsNull(DDT.Qty, 0) As TotalQuantity,DDT.IncoTerm,Article.HS_Code,DDT.CommercialPrice  " _
            & "FROM DispatchMasterTable DMT " _
            & "JOIN DispatchDetailTable DDT ON DMT.DispatchId=DDt.DispatchId " _
            & "JOIN  ArticleDefView Article ON DDT.ArticleDefId = Article.ArticleId " _
            & "LEFT JOIN  tblDefLocation TDF ON DDT.LocationID = TDF.Location_ID  " _
            & " Where DDT.DispatchID =" & ReceivingID & ""

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
            'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

            '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)("BatchID"), objDataSet.Tables(0).Rows(i)("LocationID"))

            '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

            'Next
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            dtDisplayDetail.AcceptChanges()
            ''Commented below row for TASK-408 instead of Qty
            ''dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',((PackQty*Qty)*Rate), (Qty*Rate))"
            dtDisplayDetail.Columns("Total").Expression = "[TotalQuantity]*" & IIf(ShowCostPriceRights = True, "Rate", "SalePrice") & ""
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            ''Start TFS4181
            'Me.grd.RootTable.Columns(grdEnm.BatchNo).HasValueList = True
            'Me.grd.RootTable.Columns(grdEnm.BatchNo).LimitToList = False
            'Me.grd.RootTable.Columns(grdEnm.BatchNo).EditType = Janus.Windows.GridEX.EditType.Combo

            ''End TFS4181
            ''Start TFS4181
            Me.grd.RootTable.Columns(grdEnm.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
            Me.grd.RootTable.Columns(grdEnm.ExpiryDate).FormatString = str_DisplayDateFormat
            ''End TFS4181
            Me.grd.RootTable.Columns(grdEnm.Origin).HasValueList = True
            Me.grd.RootTable.Columns(grdEnm.Origin).LimitToList = False
            Me.grd.RootTable.Columns(grdEnm.Origin).EditType = Janus.Windows.GridEX.EditType.Combo
            ApplyGridSettings()
            FillCombo("grdLocation")
            ''Ayesha Rehman: TFS4181: 16-08-2018 : Fill combo boxes
            'FillCombo("grdBatchNo")
            'FillCombo("")
            ''Ayesha Rehman : TFS4181 : 16-08-2018 : End
            FillCombo("GrdOrigin")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Ali Faisal : TFS1376 : 24-Aug-2017 : Commented by Ali because its not working against ticket
    'Private Sub DisplayAllocationDetail(ByVal TicketID As Integer)
    '    Try
    '        Dim str As String = String.Empty


    '        str = "SELECT 1 As LocationID, Article.ArticleCode, Article.ArticleDescription AS item, '' As BatchNo, '' As unit, IsNull(Recv_D.Quantity, 0) AS Qty, 0 as Rate, " _
    '      & " 0 AS Total, " _
    '      & " Article.ArticleGroupId as CategoryId, Recv_D.ProductID as ItemId, 0 as PackQty, 0 As CurrentPrice, 0 As BatchID, '' as Pack_Desc, '' As [Engine_No], '' as [Chassis_No], Article.MasterID as ArticleMasterId, IsNull(Recv_D.Quantity, 0) As TotalQuantity FROM AllocationDetail Recv_D INNER JOIN AllocationMaster ON Recv_D.Master_Allocation_ID = AllocationMaster.Master_Allocation_ID INNER JOIN " _
    '      & " dbo.ArticleDefView Article ON Recv_D.ProductID = Article.ArticleId " _
    '      & " Where AllocationMaster.TicketID =" & TicketID & ""

    '        Dim dtDisplayDetail As DataTable = GetDataTable(str)
    '        dtDisplayDetail.AcceptChanges()
    '        'dtDisplayDetail.Columns("Total").Expression = "[TotalQuantity]*[Rate]"
    '        Me.grd.DataSource = Nothing
    '        Me.grd.DataSource = dtDisplayDetail
    '        ApplyGridSettings()
    '        FillCombo("grdLocation")
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Ali Faisal : TFS1376 : 24-Aug-2017 : End

    Private Function Update_Record() As Boolean


        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer


        'Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo.Text)
        POID = Me.grdSaved.GetRow.Cells(EnumGridSaved.DispatchId).Value.ToString
        'Dim AccountId As Integer = GetConfigValue("PurchaseDebitAccount")
        Dim StockTransfer As Boolean = Convert.ToBoolean(getConfigValueByType("StockTransferFromDispatch").ToString)
        Dim blnCheckCurrentStockByItem As Boolean = False
        If Not getConfigValueByType("CheckCurrentStockByItem").ToString = "Error" Then
            blnCheckCurrentStockByItem = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)
        End If
        If StockTransfer = False Or IsBulkStockTransfer = False Then
            If IsValidToDelete("ReceivingMasterTable", "PurchaseOrderID", Me.grdSaved.GetRow.Cells("DispatchId").Value.ToString) = False Then
                msg_Error(str_ErrorDependentUpdateRecordFound)
                Exit Function
            End If
        End If
        'If IsValidToDelete("Receivin")
        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'Before against request no . 934
            'objCommand.CommandText = "Update DispatchMasterTable set DispatchNo =N'" & txtPONo.Text & "',DispatchDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & Me.cmbLocation.SelectedValue & ", " _
            '& " DispatchQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",DispatchAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text & "',UserName=N'" & LoginUserName & "', LocationId=" & Me.cmbLocation.SelectedValue & ", RefDocument=N'" & Me.txtReceivingNo.Text.ToString & "'  Where DispatchID= " & Me.txtDispatchID.Text & " "
            'ReqId-934 Resolve Comma Error
            '
            'objCommand.CommandText = "Update DispatchMasterTable set DispatchNo =N'" & txtPONo.Text & "',DispatchDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & Me.cmbLocation.SelectedValue & ", " _
            '& " DispatchQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",DispatchAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', LocationId=" & Me.cmbLocation.SelectedValue & ", RefDocument=N'" & Me.txtReceivingNo.Text.ToString & "'  Where DispatchID= " & Me.txtDispatchID.Text & " "
            objCommand.CommandText = "Update DispatchMasterTable set DispatchNo =N'" & txtPONo.Text & "',DispatchDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & Me.cmbLocation.SelectedValue & ", " _
            & " DispatchQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQuantity"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",DispatchAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', LocationId=" & Me.cmbLocation.SelectedValue & ", RefDocument=N'" & Me.txtReceivingNo.Text.ToString & "',PlanId=" & IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue) & ", PlanTicketId = " & IIf(Me.cmbTicket.SelectedIndex = -1, 0, Me.cmbTicket.SelectedValue) & ", CustomerId = " & Me.cmbCustomer1.Value & ", TransporterId = " & Me.cmbTransporter.Value & ", TrackingNo=N'" & Me.txtTrackingNo.Text.ToString & "', QuoteReference=N'" & Me.txtQuoteRef.Text.ToString & "', PaymentTerm=N'" & Me.txtpaymentterm.Text.ToString & "', CreditDays=N'" & Me.txtcreditday.Text.ToString & "', IncoTermSite=N'" & Me.txtincotermsite.Text.ToString & "', currency_id = " & Val(Me.cmbcurrency.Value) & "  Where DispatchID= " & Me.txtDispatchID.Text & " "

            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from DispatchDetailTable where DispatchID = " & txtDispatchID.Text
            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""

            For i = 0 To grd.RowCount - 1
                'Task:2644 Validation Engine No And ChassisNo
                If blnCheckCurrentStockByItem = True Then
                    CheckCurrentStockByItem(Me.grd.GetRows(i).Cells(grdEnm.ItemId).Value, Val(Me.grd.GetRows(i).Cells(grdEnm.TotalQuantity).Value.ToString), Me.grd, Me.txtPONo.Text, trans)
                End If

                Dim strDisp As String = String.Empty
                strDisp = "SELECT * FROM DispatchDetailTable WHERE (Engine_No <> '' OR Chassis_No <> '') AND DispatchId <> " & Val(grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchId).Value.ToString) & ""
                If Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Length > 0 Then
                    strDisp += " And Engine_No=N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "'"
                End If
                If Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Length > 0 Then
                    strDisp += " And Chassis_No=N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "'"
                End If
                Dim dtDispatchDetail As DataTable = GetDataTable(strDisp, trans)
                'TASK 2644 Check duplication of Engine no and chesis no then Insert Record
                'If Not IsDBNull(dtDispatchDetail.Rows(j).Item("Engine_No").ToString) Then
                'End If
                If dtDispatchDetail.Rows.Count > 0 Then
                    If Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Length > 0 Then
                        If dtDispatchDetail.Rows(0).Item("Engine_No").ToString = Me.grd.GetRows(i).Cells("Engine_No").Value.ToString Then
                            Throw New Exception("Engine no [" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString & "] already exists")
                        End If
                    End If
                    If Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Length > 0 Then
                        If dtDispatchDetail.Rows(0).Item("Chassis_No").ToString = Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString Then
                            Throw New Exception("Chassis No [" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString & "] already exists")
                        End If
                    End If
                End If
                'End Task:2644

                objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into DispatchDetailTable (DispatchId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice, BatchNo, BatchID,LocationID) values( " _
                '                        & " " & txtDispatchID.Text & " ," & Val(grd.GetRows(i).Cells(8).Value) & ",N'" & (grd.GetRows(i).Cells(3).Value) & "'," & Val(grd.GetRows(i).Cells(4).Value) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells(3).Value = "Loose", Val(grd.GetRows(i).Cells(4).Value), (Val(grd.GetRows(i).Cells(4).Value) * Val(grd.GetRows(i).Cells(9).Value))) & ", " & Val(grd.GetRows(i).Cells(5).Value) & ", " & Val(grd.GetRows(i).Cells(9).Value) & "  , " & Val(grd.GetRows(i).Cells(10).Value) & ", N'" & grd.GetRows(i).Cells(2).Value & "'," & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationID").Value & " ) "
                ''TASK TFS1417. added new coloumn of SalePrice on 12-09-2017
                'TFS4724: Waqar: Added Expiry Date to remove insertion Error
                objCommand.CommandText = "Insert into DispatchDetailTable (DispatchId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Pack_Desc,Engine_No,Chassis_No, SalePrice, ExpiryDate,IncoTerm,CommercialPrice, Origin) values( " _
                                                       & " " & txtDispatchID.Text & " ," & Val(grd.GetRows(i).Cells(grdEnm.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(grdEnm.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(grdEnm.Qty).Value) & ", " _
                                                       & " " & Val(grd.GetRows(i).Cells(grdEnm.TotalQuantity).Value) & ", " & Val(grd.GetRows(i).Cells(grdEnm.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(grdEnm.PackQty).Value) & " , " & Val(grd.GetRows(i).Cells(grdEnm.CurrentPrice).Value) & " , N'" & grd.GetRows(i).Cells(grdEnm.BatchNo).Value & "'," & grd.GetRows(i).Cells(grdEnm.BatchId).Value & "," & grd.GetRows(i).Cells(grdEnm.LocationId).Value & ",N'" & grd.GetRows(i).Cells(grdEnm.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "',N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells(grdEnm.SalePrice).Value) & "," & IIf(grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value.ToString = "", "''", "Convert(DateTime,N'" & CDate(IIf(Me.grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value.ToString = "", Date.Now, Me.grd.GetRows(i).Cells(grdEnm.ExpiryDate).Value)).ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ", N'" & Me.grd.GetRows(i).Cells("incoterm").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("CommercialPrice").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Origin").Value.ToString.Replace("'", "''") & "')"

                objCommand.ExecuteNonQuery()
                'Val(grd.Rows(i).Cells(5).Value)

            Next


            'objCommand.CommandText = ""
            'objCommand.CommandText = "update tblVoucher set voucher_date=N'" & dtpPODate.Value & "'" _
            '                        & "   where voucher_id=" & lngVoucherMasterId

            'objCommand.Transaction = trans

            'objCommand.ExecuteNonQuery()

            ''***********************
            ''Deleting Detail
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId

            'objCommand.Transaction = trans

            'objCommand.ExecuteNonQuery()

            ''***********************
            ''Inserting Debit Amount
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & Val(Me.txtAmount.Text) & ", 0)"

            'objCommand.Transaction = trans
            'objCommand.ExecuteNonQuery()

            ''***********************
            ''Inserting Credit Amount
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbVendor.ActiveRow.Cells(0).Value & ", " & 0 & ",  " & Val(Me.txtAmount.Text) & ")"
            'objCommand.Transaction = trans
            'objCommand.ExecuteNonQuery()



            _objTrans = trans

            If IsValidate() = True Then
                Call New StockDAL().Update(StockMaster, trans)
            End If

            If StockTransfer = True Or IsBulkStockTransfer = True Then
                FillModel("StockTransferFromDispatch")
                Call New SBDal.StockReceivingDAL().Update(StockReceivingMaster, trans)
            End If

            If StockTransfer = True Or IsBulkStockTransfer = True Then
                FillModel("StockReceiving")
                Call New StockDAL().Update(StockReceivingMaster.StockMaster, trans) 'Upgrading Stock ...
            End If
            trans.Commit()
            'If StockTransfer = True Then
            '    FillModel("StockTransferFromDispatch")
            '    Call New SBDal.StockReceivingDAL().Update(StockReceivingMaster)
            'End If


            Update_Record = True
            'insertvoucher()
            'Call Update1() 'Upgrading Stock ....
            'If StockTransfer = True Then
            '    FillModel("StockReceiving")
            '    Call New StockDAL().Update(StockReceivingMaster.StockMaster) 'Upgrading Stock ....
            'End If
            setVoucherNo = Me.txtPONo.Text
            getVoucher_Id = txtDispatchID.Text
            setEditMode = True
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)

            'Ali Faisal : TFS857 : Add notification on Stock Dispatch by Ali Faisal on 30-Aug-2017

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
            objmod.Notification.NotificationTitle = "Stock dispatched from location [" & Me.cmbCategory.Text & "] to location [" & cmbLocation.Text & "] against doc#: " & Me.txtPONo.Text & " "

            '// Preparing notification description string
            objmod.Notification.NotificationDescription = "Stock dispatch [" & Me.txtPONo.Text & "] of Qty [" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "] is modified by " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

            '// Setting source application as refrence in the notification
            objmod.Notification.SourceApplication = "Stock Dispatch"



            '// Starting to get users list to add child

            '// Creating notification detail object list
            Dim List As New List(Of NotificationDetail)

            '// Getting users list
            List = NDal.GetNotificationUsers("Dispatch Changed")

            '// Adding users list in the Notification object of current inquiry
            objmod.Notification.NotificationDetils.AddRange(List)

            '// Getting and adding user groups list in the Notification object of current inquiry
            objmod.Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("Dispatch Changed"))

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

            'Ali Faisal : TFS857 : Add notification on Stock Dispatch by Ali Faisal on 30-Aug-2017
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try
        SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.StockDispatch, Me.txtPONo.Text.Trim)
    End Function
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
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
            'If Me.dtpPODate.Value <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
            '    ShowErrorMessage("Your can not change this becuase financial year has been closed")
            '    Me.dtpPODate.Focus()
            '    Exit Sub
            'End If
            If FormValidate() Then
                Me.grd.UpdateData()
                If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then

                    'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() Then
                        'msg_Information(str_informSave)
                        RefreshControls()
                        ClearDetailControls()
                        'grd.Rows.Clear()
                        'DisplayRecord() R933 Commented History Data

                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop
                        '--------------------------------------
                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        'Do While BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop

                    Else
                        Exit Sub 'MsgBox("Record has not been added")
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub

                    'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Update_Record() Then
                        ' msg_Information(str_informUpdate)
                        RefreshControls()
                        ClearDetailControls()
                        'grd.Rows.Clear()
                        'DisplayRecord() R933 Commented History Data


                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop
                        '--------------------------------------
                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        'Do While BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop

                    Else
                        Exit Sub 'MsgBox("Record has not been updated")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
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
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_CellDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            ''Task# A2-10-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
            If Me.grdSaved.Row < 0 Then
                Exit Sub
            Else
                EditRecord()
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        ''End Task# A2-10-06-2015

    End Sub

    Private Sub cmbPo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' Me.DisplayPODetail(Me.cmbPo.SelectedValue)
        'If Me.cmbPo.SelectedIndex > 0 Then
        '    Dim adp As New OleDbDataAdapter
        '    Dim dt As New DataTable
        '    Dim Sql As String = "SELECT     dbo.PurchaseOrderMasterTable.VendorId, dbo.vwCOADetail.detail_title FROM         dbo.PurchaseOrderMasterTable INNER JOIN                       dbo.vwCOADetail ON dbo.PurchaseOrderMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id where PurchaseOrderMasterTable.PurchaseOrderId=" & Me.cmbPo.SelectedValue
        '    adp = New OleDbDataAdapter(Sql, Con)
        '    adp.Fill(dt)

        '    If Not dt.Rows.Count > 0 Then
        '        'Me.cmbVendor.Enabled = True : Me.cmbVendor.Rows(0).Activate()
        '    Else
        '        Me.cmbVendor.Value = dt.Rows(0).Item("VendorId").ToString
        '        Me.cmbVendor.Enabled = False
        '    End If
        '    GetTotal()
        'Else
        '    'Me.cmbVendor.Enabled = True
        '    'Me.cmbVendor.Rows(0).Activate()
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
            'GetTotal()
        End With
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbItem_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbItem.KeyDown
        Try
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
                    txtRate.Text = frmItemSearch.Rate
                    cmbUnit.SelectedIndex = IIf(frmItemSearch.PackName <> "Loose", 1, 0)
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
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            Me.txtSalePrice.Text = Me.cmbItem.ActiveRow.Cells("Sale Price").Value.ToString
            If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
            'If Me.cmbBatchNo.SelectedRow.Index > 0 Then
            '    txtStock.Text = Convert.ToDouble(GetStockByBatch(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack"), Me.cmbBatchNo.Text))
            'End If
            'Me.cmbVendor.DisplayLayout.Grid.Show( me.cmbVendor.contr)

            ''Start TFS2739 :commented against TFS2739 :Ayesha Rehman

            'Dim strSQl As String = String.Empty

            ''If GetConfigValue("WithSizeRange") = "False" Then
            ''    strSQl = "SELECT Stock, BatchNo FROM         dbo.vw_Batch_Stock WHERE     (NOT (Stock = 0))and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            'If getConfigValueByType("WithSizeRange") = "True" Then
            '    strSQl = "SELECT     ISNULL(a.Stock, 0) AS Stock, PurchaseBatchTable.BatchNo AS [Batch No], PurchaseBatchTable.BatchID" _
            '            & " FROM         SizeRangeTable INNER JOIN" _
            '            & "                   PurchaseBatchTable ON SizeRangeTable.BatchID = PurchaseBatchTable.BatchID LEFT OUTER JOIN " _
            '            & "                    (SELECT     * " _
            '            & "   FROM vw_Batch_Stock " _
            '            & "                WHERE      articleID = " & Me.cmbItem.Value & ") a ON PurchaseBatchTable.BatchID = a.BatchID " _
            '            & "WHERE(SizeRangeTable.SizeID = " & IIf(Val(Me.cmbItem.ActiveRow.Cells("Size ID").Value.ToString) > 0, Me.cmbItem.ActiveRow.Cells("Size ID").Value, 0) & ") "
            'Else
            '    strSQl = "SELECT Stock, BatchNo as [Batch No], BatchID  FROM  dbo.vw_Batch_Stock WHERE     (NOT (Stock = 0))and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            'End If

            ''FillUltraDropDown(cmbBatchNo, strSQl, False)

            'cmbBatchNo.DataSource = Nothing

            'Dim objCommand As New OleDbCommand
            'Dim objDataAdapter As New OleDbDataAdapter
            'Dim objDataSet As New DataSet

            'If Con.State = ConnectionState.Open Then Con.Close()

            'Con.Open()
            'objCommand.Connection = Con
            'objCommand.CommandType = CommandType.Text


            'objCommand.CommandText = strSQl


            'Dim dt As New DataTable
            'Dim dr As DataRow

            'objDataAdapter.SelectCommand = objCommand
            'objDataAdapter.Fill(dt)

            'dr = dt.NewRow
            'dr.Item(1) = "xxxx"
            'dr.Item(2) = 0
            'dr.Item(0) = 0
            'dt.Rows.Add(dr)

            'cmbBatchNo.DataSource = dt
            ''cmbBatchNo.DisplayLayout.Bands(0).Columns("BatchID").Hidden = True
            'cmbBatchNo.ValueMember = "BatchID"
            ''cmbBatchNo.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            'cmbBatchNo.DisplayMember = "Batch No" 'dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName"        
            'cmbBatchNo.DisplayLayout.Bands(0).Columns("BatchID").Hidden = True
            'Me.cmbBatchNo.Rows(0).Activate()
            'Con.Close()
            Me.cmbBatchNo.Enabled = True
            ''End TFS2739
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            cmbUnit_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Enter
        Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub
    Private Sub cmbVendor_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.cmbVendor.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
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
        Dim StockTransfer As Boolean = Convert.ToBoolean(getConfigValueByType("StockTransferFromDispatch").ToString)
        If StockTransfer = False Or IsBulkStockTransfer = False Then
            If IsValidToDelete("ReceivingMasterTable", "PurchaseOrderID", Me.grdSaved.GetRow.Cells("DispatchId").Value.ToString) = False Then
                msg_Error(str_ErrorDependentUpdateRecordFound)
                Exit Sub
            End If
        End If
        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
        'Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, grdSaved.CurrentRow.Cells(0).Value.ToString)
        Try

            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim cm As New OleDbCommand
            Dim objTrans As OleDbTransaction
            If Con.State = ConnectionState.Closed Then Con.Open()
            objTrans = Con.BeginTransaction
            cm.Connection = Con
            cm.CommandText = "delete from DispatchDetailTable where Dispatchid=" & Me.grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchId).Value.ToString
            cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            cm = New OleDbCommand
            cm.Connection = Con
            cm.CommandText = "delete from DispatchMasterTable where Dispatchid=" & Me.grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchId).Value.ToString

            cm.Transaction = objTrans
            cm.ExecuteNonQuery()


            StockMaster = New StockMaster
            StockMaster.StockTransId = Convert.ToInt32(StockTransId(Me.grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchNo).Value, objTrans))
            Call New StockDAL().Delete(StockMaster, objTrans)

            If StockTransfer = True Or IsBulkStockTransfer = True Then
                'FillModel("StockTransferFromDispatch")
                Dim dt As DataTable = GetReceivingId(Me.grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchId).Value, objTrans)
                dt.AcceptChanges()
                ''Below conditions are applied against TASK TFS4031 as it throws exception in case returned value is empty. Done on 31-07-18 by Amin
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Call New SBDal.StockReceivingDAL().DeleteDispatch(Val(dt.Rows(0).Item(0).ToString), objTrans)
                        StockMaster.StockTransId = Convert.ToInt32(StockTransId(dt.Rows(0).Item(1).ToString, objTrans))
                        Call New StockDAL().Delete(StockMaster, objTrans)
                    End If
                End If
            End If
            objTrans.Commit()
            'Call Delete() 'Upgrading Stock ...
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 27-1-14 
            'Me.grdSaved.CurrentRow.Delete()
            'If GetConfigValue("StockTransferFromDispatch").ToString = "True" Then
            '    StockMaster = New StockMaster
            '    StockMaster.StockTransId = Convert.ToInt32(GetStockTransId(Me.grdSaved.CurrentRow.Cells(EnumGridSaved.RefDocument).Value))
            '    Call New StockDAL().Delete(StockMaster)
            'End If
            ' msg_Information(str_informDelete)
            Me.txtDispatchID.Text = 0

        Catch ex As Exception
            msg_Error("Error occured while deleting record: " & ex.Message)

        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try

        SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.StockDispatch, grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchNo).Value.ToString)
        Me.grdSaved.CurrentRow.Delete()
        Me.RefreshControls()
    End Sub

    Private Sub cmbVendor_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then
        '    Me.FillCombo("SOSpecific")
        'Else
        '    Me.FillCombo("SO")
        'End If

    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("DispatchNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            str_ReportParam = "@DocNo|" & Me.grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchId).Value & ""
            ShowReport("rptStockDispatchPrint")
            ShowReport("DispatchCOMMERCIALINVOICE")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    'Private Sub grd_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
    '    Me.GetTotal()
    'End Sub
    Sub FillLocationCombo()
        'FillDropDown(Me.cmbLocation, "select location_id,location_code from tbldeflocation where location_id!=" & gobjLocationId, True)
        Try
            Dim Str As String = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                    & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                    & " Else " _
                    & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
            FillDropDown(Me.cmbLocation, Str, True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.tsbAssign1.Enabled = True
                Me.tsbConfig.Enabled = True
                ShowSalePrice = False
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmStockDispatch)
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
                'GetSecurityByPostingUser(UserPostingRights, BtnSave, BtnDelete)
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.btnSearchDelete.Enabled = False
                Me.btnSearchPrint.Enabled = False
                Me.BtnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                CtrlGrdBar3.mGridPrint.Enabled = False
                CtrlGrdBar3.mGridExport.Enabled = False
                CtrlGrdBar3.mGridChooseFielder.Enabled = False
                Me.tsbAssign1.Enabled = False
                Me.tsbConfig.Enabled = False
                ShowSalePrice = False
                'Task:2406 Added Field Chooser Rights
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
                        CtrlGrdBar3.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        CtrlGrdBar3.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        CtrlGrdBar3.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    ElseIf RightsDt.FormControlName = "Task" Then
                        Me.tsbAssign1.Enabled = True
                    ElseIf RightsDt.FormControlName = "Config" Then
                        Me.tsbConfig.Enabled = True
                    ElseIf RightsDt.FormControlName = "Show Sale Price" Then
                        ShowSalePrice = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    'Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
    '    If Me.UltraTabControl1.SelectedTab.Index = 0 Then
    '        Me.BtnLoadAll.Visible = False
    '    Else
    '        Me.BtnLoadAll.Visible = True
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
        ''TASK TFS4544
        If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
        End If
        ''END TFS4544
        Dim id As Integer = 0

        'FillCombo("Vendor")

        id = Me.cmbCategory.SelectedValue
        FillCombo("Category")
        Me.cmbCategory.SelectedValue = id

        id = Me.cmbItem.Value
        FillCombo("Item")
        Me.cmbItem.Value = id

        FillCombo("grdLocation")

        id = Me.cmbCostSheetItems.Value
        FillCombo("CostSheetItem")
        Me.cmbCostSheetItems.Value = id

        'id = Me.cmbUnitCostSheet.SelectedValue
        'FillCombo("Category")
        'Me.cmbUnitCostSheet.SelectedValue = id

        ''Task:2366 Added Location Wise Filter Configuration
        If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
            flgLocationWiseItem = getConfigValueByType("ArticleFilterByLocation")
        End If
        ''End Task:2366
        'Task:2644 Added Flag Vehicle Identification Info
        If Not getConfigValueByType("flgVehicleIdentificationInfo").ToString = "Error" Then
            flgVehicleIdentificationInfo = getConfigValueByType("flgVehicleIdentificationInfo")
        Else
            flgVehicleIdentificationInfo = False
        End If
        If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            flgCompanyRights = getConfigValueByType("CompanyRights")
        End If

        If Not getConfigValueByType("StoreIssuaneDependonProductionPlan").ToString = "Error" Then
            StoreIssuanceDependonProductionPlan = getConfigValueByType("StoreIssuaneDependonProductionPlan")
        End If
        'End Task:2644

        ''start TFS4161
        If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
            IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
        End If
        ''End TFS4161
        Me.lblProgress.Visible = False

    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If col.Index <> grdEnm.LocationId AndAlso col.Index <> grdEnm.Qty AndAlso col.Index <> grdEnm.TotalQuantity AndAlso col.Index <> grdEnm.Rate AndAlso col.Index <> grdEnm.Engine_No AndAlso col.Index <> grdEnm.Chassis_No Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            ''Start TFS4161
            If IsPackQtyDisabled = True Then
                Me.grd.RootTable.Columns(grdEnm.TotalQuantity).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Else
                Me.grd.RootTable.Columns(grdEnm.TotalQuantity).EditType = Janus.Windows.GridEX.EditType.TextBox
            End If
            ''End TFS4161
            Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("Unit").Index
            Me.grd.RootTable.Columns("Unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index
            'Task:2759 Set Rounded Format
            Me.grd.RootTable.Columns(grdEnm.Total).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdEnm.Total).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            'End Task:2759
            'Ali Faisal : TFS1376 : 24-Aug-2017 : Decimal piont in value setting
            Me.grd.RootTable.Columns(grdEnm.CurrentPrice).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdEnm.Rate).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdEnm.SalePrice).FormatString = "N" & DecimalPointInValue
            ''TASK TFS1417 
            If ShowCostPriceRights = True Then
                Me.grd.RootTable.Columns(grdEnm.Rate).Visible = True
                Me.grd.RootTable.Columns(grdEnm.SalePrice).Visible = False
            Else
                Me.grd.RootTable.Columns(grdEnm.Rate).Visible = False
                Me.grd.RootTable.Columns(grdEnm.SalePrice).Visible = True

            End If
            '' End TASK TFS1417 
            'Ali Faisal : TFS1376 : 24-Aug-2017 : End
            ''Start Task 1596 : Ayesha Rehman
            Dim StockOutConfigration As String = "" ''1596
            If Not getConfigValueByType("StockOutConfigration").ToString = "Error" Then ''1596
                StockOutConfigration = getConfigValueByType("StockOutConfigration").ToString
            End If
            'ShowInformationMessage(StockInConfigration)
            If StockOutConfigration.Equals("Disabled") Then
                Me.grd.RootTable.Columns(grdEnm.BatchNo).Visible = False
                Me.grd.RootTable.Columns(grdEnm.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.NoEdit
                Me.grd.RootTable.Columns(grdEnm.BatchNo).EditType = Janus.Windows.GridEX.EditType.NoEdit
            ElseIf StockOutConfigration.Equals("Enabled") Then
                Me.grd.RootTable.Columns(grdEnm.BatchNo).Visible = True
                Me.grd.RootTable.Columns(grdEnm.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
                'Me.grd.RootTable.Columns(grdEnm.BatchNo).EditType = Janus.Windows.GridEX.EditType.Combo
            Else
                Me.grd.RootTable.Columns(grdEnm.BatchNo).Visible = True
                Me.grd.RootTable.Columns(grdEnm.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
                'Me.grd.RootTable.Columns(grdEnm.BatchNo).EditType = Janus.Windows.GridEX.EditType.Combo
            End If
            Me.grd.RootTable.Columns(grdEnm.Origin).Visible = True
            Me.grd.RootTable.Columns(grdEnm.Origin).EditType = Janus.Windows.GridEX.EditType.Combo
            ''End Task 1596
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            StockMaster = New StockMaster
            StockMaster.StockTransId = Convert.ToInt32(GetStockTransId(Me.grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchNo).Value))
            Return New StockDAL().Delete(StockMaster)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            If Condition = String.Empty Then
                Dim transId As Integer = 0
                transId = Convert.ToInt32(StockTransId(Me.txtPONo.Text, _objTrans).ToString)
                StockMaster = New StockMaster
                StockMaster.StockTransId = transId
                StockMaster.DocNo = Me.txtPONo.Text.ToString.Replace("'", "''")
                StockMaster.DocDate = Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt")
                StockMaster.DocType = 5 'Convert.ToInt32(GetStockDocTypeId("Dispatch").ToString)
                StockMaster.Remaks = Me.txtRemarks.Text.ToString.Replace("'", "''")
                ''StockMaster.CustomerID = Me.cmbCustomer1.Value
                StockMaster.AccountId = Me.cmbLocation.SelectedValue
                StockMaster.StockDetailList = New List(Of StockDetail)
                For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    StockDetail = New StockDetail
                    StockDetail.StockTransId = transId 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                    StockDetail.LocationId = grdRow.Cells("LocationID").Value
                    StockDetail.ArticleDefId = grdRow.Cells("ItemId").Value
                    StockDetail.Remarks = String.Empty
                    StockDetail.InQty = 0
                    StockDetail.OutQty = Val(grdRow.Cells("TotalQuantity").Value) ''TASK-408
                    StockDetail.Rate = 0
                    StockDetail.InAmount = 0
                    StockDetail.OutAmount = 0  ''TASK-408     
                    ''Start TASK-470 on 01-07-2016
                    StockDetail.PackQty = Val(grdRow.Cells("PackQty").Value.ToString)
                    StockDetail.Out_PackQty = Val(grdRow.Cells("Qty").Value.ToString)
                    StockDetail.In_PackQty = 0
                    ''End TASK-470
                    ''Start TASK-TFS1596
                    StockDetail.BatchNo = grdRow.Cells("BatchNo").Value.ToString
                    ''End TASK-1596
                    ''Start TASK-TFS4181
                    'TFS4724: Waqar Addded this code for Removing conversion error from date to null
                    If grdRow.Cells("ExpiryDate").Value.ToString <> "" Then
                        StockDetail.ExpiryDate = grdRow.Cells("ExpiryDate").Value.ToString
                    End If
                    ''End TASK-4181
                    StockDetail.Origin = grdRow.Cells("Origin").Value.ToString
                    StockMaster.StockDetailList.Add(StockDetail)
                Next
            ElseIf Condition = "StockReceiving" Then
                Dim transRecvId As Integer = 0
                transRecvId = Convert.ToInt32(StockTransId(Me.txtReceivingNo.Text, _objTrans).ToString)
                StockReceivingMaster.StockMaster = New StockMaster
                StockReceivingMaster.StockMaster.StockTransId = transRecvId
                StockReceivingMaster.StockMaster.DocNo = Me.txtReceivingNo.Text.ToString.Replace("'", "''")
                StockReceivingMaster.StockMaster.DocDate = Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt")
                StockReceivingMaster.StockMaster.DocType = 1 'Convert.ToInt32(GetStockDocTypeId("GRN").ToString)
                StockReceivingMaster.StockMaster.Remaks = Me.txtRemarks.Text
                StockReceivingMaster.StockMaster.StockDetailList = New List(Of StockDetail)
                For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    StockDetail = New StockDetail
                    StockDetail.StockTransId = transRecvId 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                    StockDetail.LocationId = Me.cmbLocation.SelectedValue
                    StockDetail.ArticleDefId = grdRow.Cells("ItemId").Value
                    StockDetail.InQty = Val(grdRow.Cells("TotalQuantity").Value) ''TASK-408
                    StockDetail.OutQty = 0
                    StockDetail.Rate = 0
                    StockDetail.InAmount = 0  ''TASK-408     
                    StockDetail.OutAmount = 0
                    StockDetail.Remarks = String.Empty 'grdRow.Cells("Comments").Value.ToString
                    ''Start TASK-470 on 01-07-2016
                    StockDetail.PackQty = Val(grdRow.Cells("PackQty").Value.ToString)
                    StockDetail.In_PackQty = Val(grdRow.Cells("Qty").Value.ToString)
                    StockDetail.Out_PackQty = 0
                    ''End TASK-470

                    ''Start TASK-TFS1596
                    StockDetail.BatchNo = grdRow.Cells("BatchNo").Value.ToString
                    ''End TASK-1596
                    ''Start TASK-TFS4181
                    'TFS4724: Waqar Addded this code for Removing conversion error from date to null
                    If grdRow.Cells("ExpiryDate").Value.ToString <> "" Then
                        StockDetail.ExpiryDate = grdRow.Cells("ExpiryDate").Value.ToString
                    End If
                    ''End TASK-4181
                    StockDetail.Origin = grdRow.Cells("Origin").Value.ToString
                    StockReceivingMaster.StockMaster.StockDetailList.Add(StockDetail)
                Next
            ElseIf Condition = "StockTransferFromDispatch" Then
                StockReceivingMaster = New StockReceivingMaster
                StockReceivingMaster.ReceivingId = 0
                ' StockReceivingMaster.LocationId = Me.cmbLocation.SelectedValue ''Commented Against TFS4345 : Setting from location in Stock Receiving 
                StockReceivingMaster.LocationId = Me.grd.CurrentRow.Cells("LocationID").Value
                StockReceivingMaster.ReceivingNo = Me.txtReceivingNo.Text
                StockReceivingMaster.ReceivingDate = Me.dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt")
                ' StockReceivingMaster.VendorId = Me.cmbLocation.SelectedValue ''Commented Against TFS4345  : Setting from location in Stock Receiving 
                StockReceivingMaster.VendorId = Me.grd.CurrentRow.Cells("LocationID").Value
                'StockReceivingMaster.CustomerID = Me.grd.CurrentRow.Cells("CustomerID").Value
                StockReceivingMaster.PurchaseOrderID = POID
                StockReceivingMaster.PartyInvoiceNo = String.Empty
                StockReceivingMaster.PartySlipNo = String.Empty
                StockReceivingMaster.ReceivingQty = Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQuantity"), Janus.Windows.GridEX.AggregateFunction.Sum)
                StockReceivingMaster.ReceivingAmount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                StockReceivingMaster.CashPaid = Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                StockReceivingMaster.Remarks = Me.txtRemarks.Text.ToString
                StockReceivingMaster.UserName = LoginUserName
                StockReceivingMaster.IGPNo = String.Empty
                StockReceivingMaster.DcNo = String.Empty
                StockReceivingMaster.Post = True
                StockReceivingMaster.DcDate = Nothing
                StockReceivingMaster.Driver_Name = String.Empty
                StockReceivingMaster.Vehicle_No = String.Empty
                StockReceivingMaster.vendor_invoice_no = String.Empty
                StockReceivingMaster.CostCenterId = 0
                StockReceivingMaster.StockReceivingDetail = New List(Of StockReceivingDetail)
                Dim StockReceivingList As StockReceivingDetail
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    StockReceivingList = New StockReceivingDetail
                    StockReceivingList.ReceivingId = 0
                    'StockReceivingList.LocationId = r.Cells("LocationID").Value
                    StockReceivingList.LocationId = Me.cmbLocation.SelectedValue
                    StockReceivingList.FromLocationId = r.Cells("LocationID").Value
                    StockReceivingList.ArticleDefId = r.Cells("ItemId").Value
                    StockReceivingList.ArticleSize = IIf(r.Cells("Unit").Value.ToString <> "Loose", "Pack", r.Cells("Unit").Value.ToString)
                    StockReceivingList.Sz1 = r.Cells("Qty").Value
                    StockReceivingList.Sz2 = 0
                    StockReceivingList.Sz3 = 0
                    StockReceivingList.Sz4 = 0
                    StockReceivingList.Sz5 = 0
                    StockReceivingList.Sz6 = 0
                    StockReceivingList.Sz7 = r.Cells("PackQty").Value
                    StockReceivingList.Qty = Val(r.Cells("TotalQuantity").Value.ToString)
                    StockReceivingList.Price = 0
                    StockReceivingList.CurrentPrice = 0
                    StockReceivingList.BatchNo = r.Cells("BatchNo").Value.ToString
                    StockReceivingList.BatchID = r.Cells("BatchId").Value
                    StockReceivingList.ReceivedQty = Val(r.Cells("TotalQuantity").Value.ToString)
                    StockReceivingList.RejectedQty = 0
                    StockReceivingList.TaxPercent = 0
                    StockReceivingList.Pack_Desc = r.Cells("Pack_Desc").Value.ToString
                    If r.Cells("ExpiryDate").Value.ToString <> "" Then
                        StockReceivingList.ExpiryDate = r.Cells("ExpiryDate").Value.ToString
                    End If
                    ''End TASK-4181
                    StockReceivingList.Origin = r.Cells("Origin").Value.ToString
                    StockReceivingMaster.StockReceivingDetail.Add(StockReceivingList)
                Next
            End If
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
                Call New StockDAL().Add(StockMaster)
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
            If IsValidate() = True Then
                Call New StockDAL().Update(StockMaster)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub grd_Click(sender As Object, e As EventArgs) Handles grd.Click
        Try
            If Me.grd.RowCount > 0 AndAlso Me.grd.GetRow.Cells(grdEnm.ItemId).Value IsNot Nothing Then
                Dim str As String = ""
                str = " Select  BatchNo,BatchNo,ExpiryDate,Origin  From  StockDetailTable  where BatchNo not in ('','0','xxxx')  And ArticledefId = " & Me.grd.GetRow.Cells(grdEnm.ItemId).Value & " And LocationId = " & Val(Me.grd.GetRow.Cells(grdEnm.LocationId).Value.ToString) & " Group by BatchNo,ExpiryDate,Origin Having Sum(isnull(InQty, 0)) - Sum(isnull(OutQty, 0)) > 0  ORDER BY ExpiryDate Asc"
                Dim dt As DataTable = GetDataTable(str)
                ''Me.grd.RootTable.Columns(grdEnm.BatchNo).ValueList.PopulateValueList(dt.DefaultView, "BatchNo", "BatchNo")
                If Not dt.Rows.Count > 0 Then
                    grd.GetRow.Cells(grdEnm.BatchNo).Value = "xxxx"
                Else
                    If IsDBNull(grd.GetRow.Cells(grdEnm.BatchNo).Value) Or grd.GetRow.Cells(grdEnm.BatchNo).Value.ToString = "" Then
                        grd.GetRow.Cells(grdEnm.BatchNo).Value = dt.Rows(0).Item("BatchNo").ToString
                    End If
                End If
                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0).Item("BatchNo").ToString) Then
                        str = " Select   ExpiryDate, Origin  From  StockDetailTable  where BatchNo not in ('','0','xxxx') And BatchNo ='" & Me.grd.GetRow.Cells(grdEnm.BatchNo).Value.ToString & "'" _
                             & " And ArticledefId = " & Me.grd.GetRow.Cells(grdEnm.ItemId).Value & "  And LocationId = " & Val(Me.grd.GetRow.Cells(grdEnm.LocationId).Value.ToString) & "  And (isnull(InQty, 0) - isnull(OutQty, 0)) > 0 Group by BatchNo,ExpiryDate,Origin ORDER BY ExpiryDate  Asc "
                        Dim dtExpiry As DataTable = GetDataTable(str)
                        If dtExpiry.Rows.Count > 0 Then
                            If IsDBNull(dtExpiry.Rows(0).Item("ExpiryDate")) = False Then
                                grd.GetRow.Cells("ExpiryDate").Value = CType(dtExpiry.Rows(0).Item("ExpiryDate").ToString, Date)
                            End If
                            grd.GetRow.Cells("Origin").Value = dtExpiry.Rows(0).Item("Origin").ToString
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub GridBarUserControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
    '            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
    '            Me.grd.LoadLayoutFile(fs)
    '            fs.Close()
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
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
            Me.cmbItem.LimitToList = True
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            Else
                If cmbItem.Value Is Nothing Then Exit Sub
                Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            End If
            FillCombo("ArticlePack")
            FillCombo("BatchNo") ''TFS2739 :Ayesha Rehman : 19-03-2018
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCode.CheckedChanged, rdoName.CheckedChanged
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
    Private Sub cmbCostSheetItems_RowSelected(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbCostSheetItems.RowSelected
        Try
            If Me.cmbCostSheetItems.ActiveRow Is Nothing Then Exit Sub
            Me.txtPlanQty.Text = Val(Me.cmbCostSheetItems.ActiveRow.Cells("Plan Qty").Text.ToString)
            Me.txtCostSheetQty.Text = Val(Me.txtPlanQty.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Try
        '    Dim Dt As New DataTable
        '    Dim Da As OleDb.OleDbDataAdapter
        '    Dim str As String
        '    str = "SELECT a.CostSheetID, a.ArticleID, a.Qty, a.MasterArticleID, b.PurchasePrice FROM tblCostSheet a INNER JOIN ArticleDefTable b ON a.ArticleID = b.ArticleId WHERE a.MasterArticleID =" & Me.cmbCostSheetItems.Value
        '    '" & Me.cmbCostSheetItems.Value
        '    If Not Con.State = 1 Then Con.Open()
        '    Da = New OleDb.OleDbDataAdapter(str, Con)
        '    Da.Fill(Dt)
        '    Dt.AcceptChanges()
        '    If Dt.Rows.Count > 0 Then
        '        For Each Row As DataRow In Dt.Rows
        '            Me.cmbCategory.SelectedValue = Me.cmbCostSheetLocation.SelectedValue
        '            Me.cmbItem.Value = Row.Item("ArticleID").ToString
        '            Me.cmbItem_Leave(Nothing, Nothing)
        '            Me.txtQty.Text = 1
        '            Me.txtRate_LostFocus(Nothing, Nothing)
        '            Me.txtRate.Text = Row.Item("PurchasePrice").ToString
        '            Me.txtQty.Text = Val(Row.Item("Qty").ToString) * Val(Me.txtCostSheetQty.Text)
        '            Me.btnAdd_Click(Nothing, Nothing)
        '            Me.txtRate.Text = Val(0)
        '            Application.DoEvents()
        '        Next
        '        Me.Button1.Enabled = False
        '    Else
        '        Me.Button1.Enabled = True
        '    End If
        '    Con.Close()
        '    Dt = Nothing
        '    Da = Nothing
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try



        Me.Cursor = Cursors.WaitCursor
        Try
            '' Request No 871 ...
            '' 18-11-2013 By Imran Ali
            '' Batch Wise Cost Sheet  ''

            If Not Me.cmbCostSheetItems.IsItemInList Then Exit Sub
            Dim dtData As DataTable = CType(Me.grd.DataSource, DataTable)
            dtData.TableName = "StockIssue"
            If SBUtility.Utility.GetFilterDataFromDataTable(dtData, "[ArticleMasterId]='" & Me.cmbCostSheetItems.Value & "'").ToTable("StockIssue").Rows.Count > 0 Then
                ShowErrorMessage("Already Exists")
                Me.cmbCostSheetItems.Focus()
                Exit Sub
            End If
            'Before against task:2412
            'If StoreIssuanceDependonProductionPlan = True Then
            'Task:2412 Change Validation 
            If Me.cmbPlan.SelectedIndex > 0 Then
                'End Task:2412
                If Me.cmbCostSheetItems.SelectedRow.Cells(0).Value = 0 AndAlso Me.cmbPlan.SelectedIndex <> 0 Then
                    If Me.cmbPlan.SelectedIndex > 0 Then
                        DisplayPODetail(Me.cmbPlan.SelectedValue, "Plan")
                        Exit Sub
                    End If
                Else
                    If Val(Me.txtPlanQty.Text) > 0 Then
                        If Val(Me.txtPlanQty.Text) < Val(Me.txtCostSheetQty.Text) Then
                            ShowErrorMessage("Issue qty is grater than the plan qty.")
                            Me.txtCostSheetQty.Focus()
                            Exit Sub
                        End If
                    End If

                    Dim Dt As New DataTable
                    Dim Da As OleDb.OleDbDataAdapter
                    Dim str As String
                    '' Add New Column of Article Size in this query against request no 871
                    str = "SELECT a.CostSheetID, a.ArticleID, a.Qty, a.MasterArticleID, Convert(float,b.PurchasePrice) PurchasePrice, a.ArticleSize FROM tblCostSheet a INNER JOIN ArticleDefTable b ON a.ArticleID = b.ArticleId WHERE a.MasterArticleID =" & Me.cmbCostSheetItems.Value & ""
                    '" & Me.cmbCostSheetItems.Value
                    'Ali Faisal : TFS1376 : Apply configuration on Load button click
                    If Not ItemHierarchy = "Standard Cost Sheet" Then
                        str += " And a.ParentId <> 0"
                    End If
                    'Ali Faisal : TFS1376 : End
                    If Not Con.State = 1 Then Con.Open()
                    Da = New OleDb.OleDbDataAdapter(str, Con)
                    Da.Fill(Dt)
                    Dt.AcceptChanges()



                    If Dt.Rows.Count > 0 Then
                        For Each Row As DataRow In Dt.Rows
                            Me.cmbCategory.SelectedValue = Me.cmbCategory.SelectedValue
                            Me.cmbItem.Value = Row.Item("ArticleID").ToString
                            Me.cmbItem_Leave(Nothing, Nothing)
                            Me.txtQty.Text = 1
                            Me.txtRate.Text = Val(Row.Item("PurchasePrice").ToString)
                            'Me.txtQty.Text = IIf(Me.cmbUnitCostSheet.Text = "Loose", ((Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)) * Val(Me.txtCostSheetQty.Text)), (Val(Row.Item("Qty").ToString) * Val(Me.txtCostSheetQty.Text)) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
                            If Me.cmbUnitCostSheet.Text = "Batch" Then ''
                                If Val(Me.txtCostSheetQty.Text) <> 0 Then
                                    If Not Me.cmbUnitCostSheet.Text = "Batch" Then
                                        Me.txtQty.Text = Val(Me.txtCostSheetQty.Text) * Val(Row.Item("Qty").ToString)
                                        ''IIf(Me.cmbUnitCostSheet.Text = "Loose", ((Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)) * Val(Me.txtCostSheetQty.Text)), (Val(Row.Item("Qty").ToString) * Val(Me.txtCostSheetQty.Text)) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
                                    Else
                                        Me.txtQty.Text = Val(Me.txtCostSheetQty.Text) * (Val(Row.Item("Qty").ToString) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
                                    End If
                                End If
                            ElseIf Me.cmbUnitCostSheet.Text = "Loose" Then
                                If Val(Me.txtCostSheetQty.Text) = 0 Then
                                    Me.txtCostSheetQty.Text = 1
                                End If
                                If Val(Me.txtCostSheetQty.Text) <> 0 Then
                                    If Row.Item("ArticleSize").ToString = "Loose" Then
                                        Me.txtQty.Text = Val(Me.txtCostSheetQty.Text) * Val(Row.Item("Qty").ToString)
                                        ''IIf(Me.cmbUnitCostSheet.Text = "Loose", ((Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)) * Val(Me.txtCostSheetQty.Text)), (Val(Row.Item("Qty").ToString) * Val(Me.txtCostSheetQty.Text)) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
                                    Else
                                        Me.txtQty.Text = (Val(Me.txtCostSheetQty.Text) * (Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)))
                                    End If
                                End If
                            End If
                            Me.txtRate_LostFocus(Nothing, Nothing)


                            str = String.Empty
                            str = "SELECT ArticleDefId, ArticleMasterId, SUM(ISNULL(PlanQty, 0) - ISNULL(IssuedQty, 0)) AS BalanceQty FROM dbo.PlanCostSheetDetailTable WHERE ArticleMasterId=" & Row.Item("MasterArticleID").ToString & " AND ArticleDefId=" & Row.Item("ArticleId").ToString & " AND PlanId=" & Me.cmbPlan.SelectedValue & "  GROUP BY ArticleDefId, ArticleMasterId HAVING      (SUM(ISNULL(PlanQty, 0) - ISNULL(IssuedQty, 0)) <> 0) "
                            Dim dtBAL As New DataTable
                            dtBAL = GetDataTable(str)
                            dtBAL.AcceptChanges()
                            If dtBAL IsNot Nothing Then
                                If dtBAL.Rows.Count > 0 Then
                                    If Row.Item("ArticleID").ToString = dtBAL.Rows(0).Item("ArticleDefId").ToString Then
                                        If Val(Me.txtQty.Text) > Val(dtBAL.Rows(0).Item("BalanceQty").ToString) Then
                                            Me.txtQty.Text = Val(dtBAL.Rows(0).Item("BalanceQty").ToString)
                                        Else
                                            Me.txtQty.Text = Val(Me.txtQty.Text)
                                        End If
                                        Me.btnAdd_Click(Nothing, Nothing)
                                        Me.txtRate.Text = Val(0)
                                        Application.DoEvents()
                                    End If
                                End If
                            End If



                        Next
                        'If StoreIssuanceDependonProductionPlan = False Then Me.Button1.Enabled = False
                    Else
                        Me.Button1.Enabled = True
                    End If
                End If
            Else

                If Val(Me.txtPlanQty.Text) > 0 Then
                    If Val(Me.txtPlanQty.Text) < Val(Me.txtCostSheetQty.Text) Then
                        ShowErrorMessage("Issue qty is grater than the plan qty.")
                        Me.txtCostSheetQty.Focus()
                        Exit Sub
                    End If
                End If

                Dim Dt As New DataTable
                Dim Da As OleDb.OleDbDataAdapter
                Dim str As String
                '' Add New Column of Article Size in this query against request no 871
                str = "SELECT a.CostSheetID, a.ArticleID, a.Qty, a.MasterArticleID, b.PurchasePrice, a.ArticleSize FROM tblCostSheet a INNER JOIN ArticleDefTable b ON a.ArticleID = b.ArticleId WHERE a.MasterArticleID =" & Me.cmbCostSheetItems.Value & ""
                '" & Me.cmbCostSheetItems.Value
                'Ali Faisal : TFS1376 : Apply configuration on Load button click
                If Not ItemHierarchy = "Standard Cost Sheet" Then
                    str += " And a.ParentId <> 0"
                End If
                'Ali Faisal : TFS1376 : End
                If Not Con.State = 1 Then Con.Open()
                Da = New OleDb.OleDbDataAdapter(str, Con)
                Da.Fill(Dt)
                Dt.AcceptChanges()
                If Dt.Rows.Count > 0 Then
                    For Each Row As DataRow In Dt.Rows
                        Me.cmbCategory.SelectedValue = Me.cmbCategory.SelectedValue
                        Me.cmbItem.Value = Row.Item("ArticleID").ToString
                        Me.cmbItem_Leave(Nothing, Nothing)
                        Me.txtQty.Text = 1

                        'Get Cost Price
                        Dim dtprice As New DataTable
                        dtprice = GetCostPriceForRawMaterial(Val(Row.Item("ArticleID").ToString))
                        dtprice.AcceptChanges()

                        Dim dblCostPrice As Decimal = 0D
                        Dim dblPurchasePrice As Decimal = 0D
                        Dim dblPrice As Decimal = 0D
                        If dtprice.Rows.Count > 0 Then
                            dblCostPrice = Val(dtprice.Rows(0).Item("CostPrice").ToString)
                            dblPurchasePrice = Val(dtprice.Rows(0).Item("PurchasePrice").ToString)
                        End If
                        If getConfigValueByType("AvgRate").ToString = "True" Then
                            dblPrice = dblCostPrice
                        Else
                            dblPrice = dblPurchasePrice
                        End If
                        If dblPrice = 0 Then
                            dblPrice = Val(Row("PurchasePrice").ToString)
                        End If
                        'Me.txtRate.Text = Row.Item("PurchasePrice").ToString
                        Me.txtRate.Text = dblPrice
                        If Me.cmbUnitCostSheet.Text = "Batch" Then
                            If Val(Me.txtCostSheetQty.Text) <> 0 Then
                                If Me.cmbUnitCostSheet.Text = "Batch" Then
                                    Me.txtQty.Text = Val(Me.txtCostSheetQty.Text) * Val(Row.Item("Qty").ToString)
                                    ''IIf(Me.cmbUnitCostSheet.Text = "Loose", ((Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)) * Val(Me.txtCostSheetQty.Text)), (Val(Row.Item("Qty").ToString) * Val(Me.txtCostSheetQty.Text)) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
                                Else
                                    Me.txtQty.Text = Val(Me.txtCostSheetQty.Text) * (Val(Row.Item("Qty").ToString) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
                                End If
                            End If
                        ElseIf Me.cmbUnitCostSheet.Text = "Loose" Then
                            If Val(Me.txtCostSheetQty.Text) = 0 Then
                                Me.txtCostSheetQty.Text = 1
                            End If
                            If Val(Me.txtCostSheetQty.Text) <> 0 Then
                                If Row.Item("ArticleSize").ToString = "Loose" Then
                                    Me.txtQty.Text = Val(Me.txtCostSheetQty.Text) * Val(Row.Item("Qty").ToString)
                                    ''IIf(Me.cmbUnitCostSheet.Text = "Loose", ((Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)) * Val(Me.txtCostSheetQty.Text)), (Val(Row.Item("Qty").ToString) * Val(Me.txtCostSheetQty.Text)) * Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString))
                                Else
                                    Me.txtQty.Text = (Val(Me.txtCostSheetQty.Text) * (Val(Row.Item("Qty").ToString) / Val(Me.cmbCostSheetItems.ActiveRow.Cells("PackQty").Value.ToString)))
                                End If
                            End If
                        End If
                        Me.txtRate_LostFocus(Nothing, Nothing)
                        Me.btnAdd_Click(Nothing, Nothing)
                        Me.txtRate.Text = Val(0)
                        Application.DoEvents()
                    Next
                    'If StoreIssuanceDependonProductionPlan = False Then Me.Button1.Enabled = False
                Else
                    Me.Button1.Enabled = True
                End If
            End If

            Con.Close()
            dt = Nothing
            'Da = Nothing

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try


    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load, CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Stock Dispatch"
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Stock Dispatch"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("DN" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "DispatchMasterTable", "DispatchNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("DN" & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "DispatchMasterTable", "DispatchNo")
            Else
                Return GetNextDocNo("DN", 6, "DispatchMasterTable", "DispatchNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Function GetDocumentNo1() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("SRN" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "ReceivingMasterTable", "ReceivingNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("SRN" & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "ReceivingMasterTable", "ReceivingNo")
            Else
                Return GetNextDocNo("SRN", 6, "ReceivingMasterTable", "ReceivingNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnAddNewItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewItem.Click
        Call frmAddItem.ShowDialog()
        Call FillCombo("Item")
    End Sub
    Public Function Get_All(ByVal DispatchNo As String)
        Try
            Get_All = Nothing
            If IsFormOpen = True Then
                If DispatchNo.Length > 0 Then
                    Dim str As String = "Select * From DispatchMasterTable WHERE DispatchNo=N'" & DispatchNo & "'"
                    Dim dt As DataTable = GetDataTable(str)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then


                            'IsEditMode = True
                            'FillCombo("Vendor")
                            'FillCombo("CostCenter")
                            Me.txtDispatchID.Text = dt.Rows(0).Item("DispatchId")
                            Me.txtPONo.Text = dt.Rows(0).Item("DispatchNo").ToString
                            Me.dtpPODate.Value = dt.Rows(0).Item("DispatchDate")
                            'Me.cmbVendor.Value = dt.Rows(0).Item("VendorId")
                            Me.cmbLocation.SelectedValue = dt.Rows(0).Item("VendorId")
                            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
                            Me.txtPaid.Text = dt.Rows(0).Item("CashPaid")
                            Me.txtReceivingNo.Text = dt.Rows(0).Item("RefDocument").ToString
                            'Me.cmbPo.SelectedValue = dt.Rows(0).Item("PurchaseOrderID")
                            'Me.chkPost.Checked = dt.Rows(0).Item("Post")
                            'Me.txtVhNo.Text = dt.Rows(0).Item("Vehicle_No")
                            'Me.txtDriverName.Text = dt.Rows(0).Item("Driver_Name")
                            'If Not IsDBNull(dt.Rows(0).Item("Dcdate")) Then
                            '    Me.dtpDcDate.Value = dt.Rows(0).Item("Dcdate")
                            '    Me.dtpDcDate.Checked = True
                            'Else
                            '    Me.dtpDcDate.Value = Date.Today
                            '    Me.dtpDcDate.Checked = False
                            'End If
                            'Me.txtInvoiceNo.Text = dt.Rows(0).Item("Vendor_Invoice_No")
                            'Me.cmbProject.SelectedValue = dt.Rows(0).Item("CostCenterId")
                            DisplayDetail(dt.Rows(0).Item("DispatchId"))
                            'If Me.cmbPo.SelectedValue > 0 Then
                            '    Me.cmbVendor.Enabled = False
                            'Else
                            '    Me.cmbVendor.Enabled = True
                            'End If
                            Me.BtnSave.Text = "&Update"
                            Me.GetSecurityRights()
                            Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
                            'VoucherDetail(dt.Rows(0).Item("ReceivingId"))
                            IsDrillDown = True
                            Me.cmbLocation.DroppedDown = False



                            If flgDateLock = True Then
                                If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
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
            DisplayRecord("All")
            Me.DisplayDetail(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Try
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

    Private Sub btnSearchPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPrint.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("DispatchNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'str_ReportParam = "@DocNo|" & Me.grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchId).Value & ""
            AddRptParam("@DocNo", Me.grdSaved.CurrentRow.Cells(EnumGridSaved.DispatchId).Value)
            ShowReport("rptStockDispatchPrint")
        Catch ex As Exception

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
                ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Else
                If setEditMode = False Then Me.BtnDelete.Visible = False
                If setEditMode = False Then Me.BtnPrint.Visible = False
                Me.BtnEdit.Visible = False
                ''''''''''''''''''''''''''
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
                    If IO.File.Exists(str_ApplicationStartUpPath & "\Reports\rptStockDispatchPrint.rpt") = False Then Exit Sub
                    crpt.Load(str_ApplicationStartUpPath & "\Reports\rptStockDispatchPrint.rpt", DBServerName)
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
                    'crpt.RecordSelectionFormula = "{DispatchMasterTable.DispatchId}=" & VoucherId



                    Dim crExportOps As New ExportOptions
                    Dim crDiskOps As New DiskFileDestinationOptions
                    Dim crExportType As New PdfRtfWordFormatOptions


                    If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments\") Then
                        IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments\")
                    Else
                    End If
                    FileName = String.Empty
                    FileName = "Stock Dispatch" & "-" & setVoucherNo & ""
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
                    crpt.SetParameterValue("@DocNo", VoucherId)
                    crpt.Export(crExportOps)

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function EmailSave()
        EmailSave = Nothing
        Dim flg As Boolean = False
        'If Me.cmbVendor.ActiveRow Is Nothing Then Exit Function
        If IsEmailAlert = True Then
            Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name='frmStockDispatch' AND EmailAlert=1")
            If dtForm.Rows.Count > 0 Then
                flg = True
            Else
                flg = False
            End If
            If flg = True Then
                Email = New Email
                Email.ToEmail = AdminEmail
                Email.CCEmail = String.Empty
                Email.BccEmail = String.Empty 'Me.cmbVendor.ActiveRow.Cells("Email").Text.ToString
                Email.Attachment = SourceFile
                Email.Subject = "Stock Dispatch " & setVoucherNo & ""
                Email.Body = "Stock Dispatch " _
                & " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Previouse_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
                Email.Status = "Pending"
                Call New MailSentDAL().Add(Email)
            End If
        End If
        Return EmailSave

    End Function

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            ExportFile(getVoucher_Id)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            EmailSave()
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


    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
        ''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
        'If e.KeyCode = Keys.Delete Then
        '    DeleteToolStripButton_Click(Nothing, Nothing)
        '    Exit Sub
        'End If
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 27-1-14
        If e.KeyCode = Keys.F5 Then
            BtnRefresh_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            'Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdSaved.RowCount <= 0 Then Exit Sub
            DeleteToolStripButton_Click(Nothing, Nothing)
            ' Exit Sub
        End If
    End Sub
    'Task:2644 Add New Fields Engine No and Chassis No In Sales Module
    Public Function CheckDuplicateEngineNo() As Boolean
        Try
            If Me.grd.RowCount = 0 Then Return False
            For i As Int32 = 0 To Me.grd.RowCount - 1
                For j As Int32 = i + 1 To Me.grd.RowCount - 1
                    If Me.grd.GetRows(j).Cells(grdEnm.Engine_No).Value.ToString.Length > 0 Then
                        If Me.grd.GetRows(j).Cells(grdEnm.Engine_No).Value.ToString = Me.grd.GetRows(i).Cells(grdEnm.Engine_No).Value.ToString Then
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
    'End Task:2644
    'Task:2644 Add New Fields Engine No and Chassis No In Sales Module
    Public Function CheckDuplicateChassisNo() As Boolean
        Try
            If Me.grd.RowCount = 0 Then Return False
            For i As Int32 = 0 To Me.grd.RowCount - 1
                For j As Int32 = i + 1 To Me.grd.RowCount - 1
                    If Me.grd.GetRows(j).Cells(grdEnm.Chassis_No).Value.ToString.Length > 0 Then
                        If Me.grd.GetRows(j).Cells(grdEnm.Chassis_No).Value.ToString = Me.grd.GetRows(i).Cells(grdEnm.Chassis_No).Value.ToString Then
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
    'End Task:2644
    Private Sub cmbLocation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLocation.SelectedIndexChanged
        Try
            If IsFormOpen = False Then Exit Sub
            FillCombo("Category")
            FillCombo("grdLocation")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmbPlan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPlan.SelectedIndexChanged
        Try

            If IsFormOpen = True Then
                'Before against task:2412
                'If StoreIssuanceDependonProductionPlan = True Then
                'Task:2412 Change Validation 
                If Not Me.cmbPlan.SelectedIndex = -1 Then
                    'End Task:2412
                    FillCombo("CostSheetItem")
                    FillCombo("Ticket")
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    ''Task# A1-10-06-2015 Added Key Pres event for some textboxes to take just numeric and dot value
    Private Sub txtNUM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPlanQty.KeyPress, txtCostSheetQty.KeyPress, txtStock.KeyPress, txtPackQty.KeyPress, txtQty.KeyPress, txtRate.KeyPress, txtTotal.KeyPress, txtSalePrice.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End Task# A1-10-06-2015

    Private Sub txtPackQty_TextChanged(sender As Object, e As EventArgs) Handles txtPackQty.TextChanged
        Try
            If Val(txtPackQty.Text) > 0 AndAlso Val(txtQty.Text) > 0 Then
                Me.txtTotalQuantity.Text = Val(txtPackQty.Text) * Val(txtQty.Text)
            ElseIf Val(txtRate.Text) > 0 Then
                Me.txtTotal.Text = Val(Me.txtTotalQuantity.Text) * Val(txtRate.Text)
            End If
            If Val(txtSalePrice.Text) > 0 Then
                Me.txtTotal.Text = Val(Me.txtTotalQuantity.Text) * Val(txtSalePrice.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtTotalQuantity.TextChanged
        Try
            Me.txtTotal.Text = Val(txtTotalQuantity.Text) * Val(txtRate.Text)
            Me.txtSaleTotal.Text = Val(txtTotalQuantity.Text) * Val(txtSalePrice.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetGridDetailQtyCalculate(ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            Me.grd.UpdateData()
            If e.Column.Index = grdEnm.Qty Or e.Column.Index = grdEnm.PackQty Then
                If Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(grdEnm.TotalQuantity).Value = (Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) * Val(Me.grd.GetRow.Cells(grdEnm.Qty).Value.ToString))
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                Else
                    Me.grd.GetRow.Cells(grdEnm.TotalQuantity).Value = Val(Me.grd.GetRow.Cells(grdEnm.Qty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                End If
            ElseIf e.Column.Index = grdEnm.TotalQuantity Then
                If Not Val(Me.grd.GetRow.Cells(grdEnm.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(grdEnm.Qty).Value = Val(Me.grd.GetRow.Cells(grdEnm.TotalQuantity).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.Qty).Value
                End If
            End If
            'Me.grd.Refetch()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            If e.Column.Key = "Qty" Then
                Dim Stock As Boolean
                Dim str As String
                str = "SELECT AllowMinusStock from tbldefLocation where location_id = " & cmbCategory.SelectedValue & ""
                Dim dt As DataTable
                dt = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    Stock = dt.Rows(0).Item("AllowMinusStock")
                End If
                If Stock = False Then
                    If Val(grd.GetRow.Cells(grdEnm.Qty).Value) > Convert.ToDouble(GetStockById(Me.grd.CurrentRow.Cells(grdEnm.ItemId).Value, Me.grd.CurrentRow.Cells(grdEnm.LocationId).Value, Me.grd.CurrentRow.Cells(grdEnm.Unit).Value.ToString)) Then
                        ShowErrorMessage("Quantity can not be greater then AvailableStock")
                        grd.CancelCurrentEdit()
                    ElseIf Val(grd.GetRow.Cells(grdEnm.Qty).Value) <= 0 Then
                        ShowErrorMessage("Quantity should be greater than zero")
                        grd.CancelCurrentEdit()
                    End If
                End If
            End If

            If Not IsDBNull(Me.grd.GetRow.Cells(grdEnm.BatchNo).Value) Then
                Dim str As String = String.Empty
                str = " Select   ExpiryDate, Origin  From  StockDetailTable  where BatchNo not in ('','0','xxxx') And BatchNo ='" & Me.grd.GetRow.Cells(grdEnm.BatchNo).Value.ToString & "'" _
                     & " And ArticledefId = " & Me.grd.GetRow.Cells(grdEnm.ItemId).Value & "  And LocationId = " & Val(Me.grd.GetRow.Cells(grdEnm.LocationId).Value.ToString) & "  And (isnull(InQty, 0) - isnull(OutQty, 0)) > 0 Group by BatchNo,ExpiryDate,Origin ORDER BY ExpiryDate  Asc "
                Dim dtExpiry As DataTable = GetDataTable(str)
                If dtExpiry.Rows.Count > 0 Then
                    If IsDBNull(dtExpiry.Rows(0).Item("ExpiryDate")) = False Then
                        grd.GetRow.Cells("ExpiryDate").Value = CType(dtExpiry.Rows(0).Item("ExpiryDate").ToString, Date)
                    End If
                    grd.GetRow.Cells("Origin").Value = dtExpiry.Rows(0).Item("Origin").ToString
                End If
            End If
            GetGridDetailQtyCalculate(e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicket_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTicket.SelectedIndexChanged
        Try
            'DisplayAllocationDetail(Val(Me.cmbTicket.SelectedValue))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtSalePrice_TextChanged(sender As Object, e As EventArgs) Handles txtSalePrice.TextChanged
        Try
            Me.txtSaleTotal.Text = Val(txtTotalQuantity.Text) * Val(txtSalePrice.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' This function is added to get Cost Price w.r.t Batch No
    ''' </summary>
    ''' <param name="ItemId"></param>
    ''' <param name="BatchNo"></param>
    ''' <returns></returns>
    ''' <remarks>Ayesha Rehman : TFS2739 : Lot wise rate for costing on Dispatch</remarks>
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


    Private Sub cmbBatchNo_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbBatchNo.RowSelected
        Try
            If Me.cmbBatchNo.SelectedRow.Index > 0 Then
                If getConfigValueByType("AvgRate") = "True" AndAlso getConfigValueByType("CostImplementationLotWiseOnStockMovement") = "True" Then
                    If Convert.ToDouble(GetItemRateByBatch(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbBatchNo.Text)) > 0 Then
                        txtRate.Text = Convert.ToDouble(GetItemRateByBatch(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbBatchNo.SelectedRow.Cells(1).Value.ToString))
                    Else
                        txtRate.Text = Val(txtRate.Text.ToString)
                    End If
                End If
            End If
            'If Me.cmbBatchNo.SelectedRow.Index > 0 Then
            '    txtStock.Text = Convert.ToDouble(GetStockByBatch(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack"), Me.cmbBatchNo.Text))
            'Else
            '    txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    'Private Sub cmbBatchNo_TextChanged(sender As Object, e As EventArgs) Handles cmbBatchNo.TextChanged
    '    Try
    '        If getConfigValueByType("AvgRate") = "True" AndAlso getConfigValueByType("CostImplementationLotWiseOnStockMovement") = "True" Then
    '            If Convert.ToDouble(GetItemRateByBatch(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbBatchNo.Text)) > 0 Then
    '                txtRate.Text = Convert.ToDouble(GetItemRateByBatch(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbBatchNo.Text))
    '            Else
    '                txtRate.Text = Val(txtRate.Text.ToString)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub


    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Stock Dispatch"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class
