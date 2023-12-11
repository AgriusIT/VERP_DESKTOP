''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction forms
''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''1-Jan-2014 Tsk:2366   Imran Ali         Slow working load forms
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms  
''03-Feb-2014        Task:2406   Imran Ali    FIELD CHOOSER restriction (Senior Rozgar)
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
'22-5-2014 Task 2644 JUNAID Add New Fields Engine No And Chassis No In Detail Record in Stock Dispatch/Stock Receiving
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''10-June-2015 Task# A2-10-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
''10-June-2015 Task# A1-10-06-2015 Added Key Pres event for some textboxes to take just numeric and dot value
'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'16-Sep-2015 Task#16092015 Ahmad Sharif: Load Companies and Locations user wise
''TASK-470 Muhammad Ameen on 01-07-2016: Stock Statement Report By Pack
''TASK TFS1417 Muhammad Ameen on 12-09-2017 : Security rights to opt Sale Price or Cost Price on Stock Dispatch and Stock Receive.
'' TASK TFS1412 Muhammad Ameen on 20-09-2017 : Rejected quantity should be added to dispatch location. 
''TFS4161 Ayesha Rehman : 09-08-2018 : P QTY: (Should Be Static/ Un-Changeable / Un-Editable on All Screens)
''TFS4347 Ayesha Rehman : 20-08-2018 : Addition of color and size fields to detail grid in Stock Receive 

Imports SBDal
Imports SBDal.StockDAL
Imports SBDal.StockDocTypeDAL
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Public Class frmStockReceive
    Implements IGeneral
    Dim dt As DataTable
    Dim Mode = "Normal"
    Dim IsFormOpen As Boolean = False
    Dim StockMaster As StockMaster
    Dim StockDetail As StockDetail
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
    Dim ReceivedQty As Double = 0
    Dim RejectedQty As Double = 0
    Dim DamagedQty As Double = 0
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Dim ItemFilterByName As Boolean = False
    Enum grdEnm
        LocationId
        ArticleCode
        Item
        Size ''TFS4347
        Color ''TFS4347
        BatchNo
        Unit
        Qty
        TotalQuantity
        ReceivedQty
        RejectedQty
        DamagedQty
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
        RejectReason
        RejectRemarks
        DamageReason
        DamageRemarks
        Remarks
        DispatchDetailId
        FromLocationId
    End Enum

    Private Sub frmStockReceive_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmStockReceive_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 

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

        FillCombo("Vendor")
        FillCombo("SM")
        'FillCombo("SO") R933 Commented
        FillCombo("Category")
        'FillCombo("Item")
        FillCombo("SearchLocation")
        'FillCombo("ArticlePack") 'R933 Commented
        RefreshControls()
        Me.cmbLocation.Focus()
        'Me.DisplayRecord()
        '//This will hide Master grid
        Me.grdSaved.Visible = CType(getConfigValueByType("ShowMasterGrid"), Boolean)
        IsFormOpen = True
        Get_All(frmModProperty.Tags)

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
        'TFS3360
        UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
        'End Task:2644
        ''start TFS4161
        If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
            IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
        End If
        ''End TFS4161
        Me.lblProgress.Visible = False
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub
    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Try

            Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
            Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
            Dim str As String = String.Empty

            'str = "SELECT     Recv.ReceivingNo, Recv.ReceivingDate AS Date, vwCOADetail.detail_title as CustomerName, V.PurchaseOrderNo, Recv.ReceivingQty, Recv.ReceivingAmount, Recv.ReceivingId,  " & _
            '        "Recv.CustomerCode, EmployeeDefTable.EmployeeName, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId " & _
            '        "FROM         ReceivingMasterTable Recv INNER JOIN " & _
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
                'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
                'str = "SELECT  DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   Recv.ReceivingNo, Recv.ReceivingDate AS Date, DispatchMasterTable.DispatchNo, tblDefLocation.Location_Code  as detail_title, Recv.ReceivingQty, " & _
                '        "Recv.ReceivingAmount, Recv.ReceivingId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
                '        "Recv.PurchaseOrderID, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status] " & _
                '        "FROM         dbo.ReceivingMasterTable Recv " & _
                '        " LEFT OUTER JOIN " & _
                '        " DispatchMasterTable ON Recv.PurchaseOrderID = DispatchMasterTable.DispatchID " _
                '        & " Left Outer Join tblDefLocation on Recv.vendorID = tblDefLocation.location_id  INNER JOIN ReceivingDetailTable Recv_d ON Recv_d.ReceivingId = Recv.ReceivingId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.ReceivingNo  where (Recv.ReceivingNo like 'SR%' OR Recv.ReceivingNo like 'SRN%') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.ReceivingDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
                'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
                'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
                str = "SELECT  DISTINCT " & IIf(strCondition.ToString = "All", "", "Top 50") & "   Recv.ReceivingNo, Recv.ReceivingDate AS Date, DispatchMasterTable.DispatchNo, tblDefLocation.Location_Code  as detail_title, Recv.ReceivingQty, " & _
                        "Recv.ReceivingAmount, Recv.ReceivingId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid,  " & _
                        "Recv.PurchaseOrderID, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],recv.username as 'User Name', IsNull(Recv.TicketID, 0) As TicketID " & _
                        "FROM         dbo.ReceivingMasterTable Recv " & _
                        " LEFT OUTER JOIN " & _
                        " DispatchMasterTable ON Recv.PurchaseOrderID = DispatchMasterTable.DispatchID " _
                        & " Left Outer Join tblDefLocation on Recv.vendorID = tblDefLocation.location_id  INNER JOIN ReceivingDetailTable Recv_d ON Recv_d.ReceivingId = Recv.ReceivingId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.ReceivingNo  where (Recv.ReceivingNo like 'SR%' OR Recv.ReceivingNo like 'SRN%') " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.ReceivingDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & ""
                'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
                If Me.dtpFrom.Checked = True Then
                    str += " AND Recv.DispatchDate >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
                End If
                If Me.dtpTo.Checked = True Then
                    str += " AND Recv.ReceivingDate <= Convert(Datetime, N'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
                End If
                If Me.txtSearchDocNo.Text <> String.Empty Then
                    str += " AND Recv.ReceivingNo LIKE '%" & Me.txtSearchDocNo.Text & "%'"
                End If
                If Me.cmbSearchLocation.SelectedIndex <> 0 Then
                    str += " AND Recv_d.LocationId=" & Me.cmbSearchLocation.SelectedValue
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
                'If Me.cmbSearchCostCenter.SelectedIndex <> 0 Then
                '    str += " AND Recv.PurchaseOrderID = " & Me.cmbSearchCostCenter.SelectedValue
                'End If
                If Me.txtSearchRemarks.Text <> String.Empty Then
                    str += " AND Recv.Remarks LIKE '%" & Me.txtSearchRemarks.Text & "%'"
                End If
                If Me.txtPurchaseNo.Text <> String.Empty Then
                    str += " AND DispatchMasterTable.DispatchNo LIKE  '%" & Me.txtPurchaseNo.Text & "%'"
                End If
                str += "  order by 1 desc"

            End If
            FillGridEx(grdSaved, str, True)
            grdSaved.RootTable.Columns.Add("Selected")
            grdSaved.RootTable.Columns("Selected").ActAsSelector = True
            grdSaved.RootTable.Columns("Selected").UseHeaderSelector = True

            grdSaved.RootTable.Columns(4).Visible = False
            grdSaved.RootTable.Columns(5).Visible = False
            grdSaved.RootTable.Columns(6).Visible = False
            grdSaved.RootTable.Columns(7).Visible = False
            grdSaved.RootTable.Columns(9).Visible = False
            grdSaved.RootTable.Columns(10).Visible = False
            grdSaved.RootTable.Columns("TicketID").Visible = False
            grdSaved.RootTable.Columns(0).Caption = "Doc No"
            grdSaved.RootTable.Columns(1).Caption = "Doc Date"
            grdSaved.RootTable.Columns(2).Caption = "GP No"
            grdSaved.RootTable.Columns(3).Caption = "Location"
            grdSaved.RootTable.Columns(9).Caption = "Cash Paid"
            grdSaved.RootTable.Columns(8).Caption = "Remarks"

            grdSaved.RootTable.Columns(0).Width = 100
            grdSaved.RootTable.Columns(1).Width = 100
            grdSaved.RootTable.Columns(2).Width = 100
            grdSaved.RootTable.Columns(3).Width = 200
            grdSaved.RootTable.Columns(8).Width = 200
            grdSaved.RootTable.Columns("Selected").Width = 50
            'Task:2759 Set rounded format
            Me.grdSaved.RootTable.Columns("ReceivingAmount").FormatString = "N" & DecimalPointInValue
            'End Task:2759
            Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Validate_AddToGrid() Then
            AddItemToGrid()
            'GetTotal()
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
            txtPaid.Text = ""
            'txtAmount.Text = ""
            txtTotal.Text = ""
            'txtTotalQty.Text = ""
            txtBalance.Text = ""
            txtPackQty.Text = 1
            txtQty.Text = ""
            txtTotalQuantity.Text = ""
            txtRate.Text = ""
            Me.BtnSave.Text = "&Save"
            FillCombo("SO")
            FillCombo("Tickets")
            cmbUnit.SelectedIndex = 0
            Me.cmbSalesMan.SelectedIndex = 0
            'Dim ChangeDocNo As Boolean = Convert.ToBoolean(GetConfigValue("ChangeDOCNo").ToString)
            'If ChangeDocNo = True Then
            '    Me.txtPONo.Text = GetDocumentNo()
            'Else
            '    Me.txtPONo.Text = GetNextDocNo("SRN", 6, "ReceivingMasterTable", "ReceivingNo")
            'End If
            Me.txtPONo.Text = GetDocumentNo()
            Me.cmbPo.Enabled = True
            cmbLocation.Enabled = True
            Me.cmbLocation.SelectedValue = Convert.ToInt32(0)
            'grd.Rows.Clear()
            Me.cmbPo.Focus()
            Me.cmbPo.DroppedDown = True
            GetSecurityRights()
            ''TASK TFS1417 ON 12-09-2017
            If ShowCostPriceRights = True Then
                Me.pnlSale.Visible = False
                Me.pnlCost.Visible = True
            Else
                Me.pnlSale.Visible = True
                Me.pnlCost.Visible = False
            End If
            ''End TASK TFS1417
            If Me.cmbBatchNo.Value = Nothing Then
                Me.cmbBatchNo.Enabled = False
            Else
                Me.cmbBatchNo.Enabled = True
            End If
            Me.dtpFrom.Value = Date.Now.AddMonths(-1)
            Me.dtpTo.Value = Date.Now
            Me.dtpFrom.Checked = False
            Me.dtpTo.Checked = False
            Me.txtSearchDocNo.Text = String.Empty
            Me.cmbSearchLocation.SelectedIndex = 0
            Me.txtFromAmount.Text = String.Empty
            Me.txtToAmount.Text = String.Empty
            Me.txtPurchaseNo.Text = String.Empty
            'Me.cmbSearchCostCenter.SelectedIndex = 0
            Me.txtSearchRemarks.Text = String.Empty
            Me.SplitContainer1.Panel1Collapsed = True
            'DisplayRecord() R933 Commented History Data
            Me.lblPrintStatus.Text = String.Empty
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnEdit.Visible = False
            Me.BtnPrint.Visible = False
            Me.BtnDelete.Visible = False
            blnUpdateAll = False
            Me.btnUpdateAll.Enabled = True
            DisplayDetail(-1)
            ''''''''''''''''''''''''''''
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ClearDetailControls()
        'cmbCategory.SelectedIndex = 0
        cmbUnit.SelectedIndex = 0
        cmbBatchNo.Text = "xxxx"
        txtTotalQuantity.Text = ""
        txtRate.Text = ""
        txtTotal.Text = ""
        txtPackQty.Text = 1
    End Sub

    Private Function Validate_AddToGrid() As Boolean
        If cmbItem.ActiveRow.Cells(0).Value <= 0 Then
            msg_Error("Please select an item")
            cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Me.cmbBatchNo.Text.Length <= 0 Then
            msg_Error("Please enter batch no")
            cmbBatchNo.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtTotalQuantity.Text) <= 0 Then
            msg_Error("Quantity is not greater than 0")
            txtTotalQuantity.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtRate.Text) < 0 Then
            msg_Error("Rate is not greater than 0")
            txtRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        Validate_AddToGrid = True
    End Function





    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Val(Me.txtPackQty.Text) = 0 Then
        '    txtPackQty.Text = 1
        '    txtTotal.Text = Math.Round(Val(txtTotalQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        'Else
        '    txtTotal.Text = Math.Round(((Val(txtTotalQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
        'End If
        'If Val(Me.txtTotalQuantity.Text) > 0 Then
        '    Me.txtTotal.Text = Val(Me.txtTotalQuantity.Text) * Val(Me.txtRate.Text)
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
            'txtTotal.Text = Math.Round(Val(txtTotalQuantity.Text) * Val(txtRate.Text), DecimalPointInValue)
            txtPackQty.Text = 1
            Me.txtPackQty.Enabled = False
            Me.txtPackQty.TabStop = False
        Else
            ''Start TFS4161
            If IsPackQtyDisabled = True Then
                Me.txtPackQty.Enabled = False
                Me.txtPackQty.TabStop = False
                Me.txtTotalQuantity.Enabled = False
            Else
                Me.txtPackQty.Enabled = True
                Me.txtPackQty.TabStop = True
                Me.txtTotalQuantity.Enabled = True
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
            'txtTotal.Text = Math.Round((Val(txtTotalQuantity.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text), DecimalPointInValue)


        End If

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
            drGrd.Item(grdEnm.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text
            drGrd.Item(grdEnm.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text
            drGrd.Item(grdEnm.BatchNo) = Me.cmbBatchNo.Text
            drGrd.Item(grdEnm.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            drGrd.Item(grdEnm.Qty) = Val(Me.txtQty.Text)
            drGrd.Item(grdEnm.Rate) = Val(Me.txtRate.Text)
            drGrd.Item(grdEnm.SalePrice) = Val(Me.txtSalePrice.Text)
            ''Start TFS4347
            drGrd.Item(grdEnm.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value
            drGrd.Item(grdEnm.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value
            ''End TFS4347
            'drGrd.Item(grdEnm.Total) = Val(Me.txtTotal.Text)
            ''TASK TFS1417
            If pnlCost.Visible = True Then
                drGrd.Item(grdEnm.Total) = Val(Me.txtTotal.Text)
            Else
                drGrd.Item(grdEnm.Total) = Val(Me.txtSaleTotal.Text)
            End If
            ''End TFS1417
            drGrd.Item(grdEnm.CategoryId) = Me.cmbCategory.SelectedValue
            drGrd.Item(grdEnm.ItemId) = Me.cmbItem.ActiveRow.Cells(0).Value
            drGrd.Item(grdEnm.PackQty) = Val(Me.txtPackQty.Text)
            drGrd.Item(grdEnm.CurrentPrice) = Me.cmbItem.ActiveRow.Cells("Price").Text
            drGrd.Item(grdEnm.BatchId) = Me.cmbBatchNo.Value
            drGrd.Item(grdEnm.LocationId) = Me.cmbCategory.SelectedValue
            drGrd.Item(grdEnm.Pack_Desc) = Me.cmbUnit.Text.ToString
            drGrd.Item(grdEnm.TotalQuantity) = Val(Me.txtTotalQuantity.Text)
            drGrd.Item(grdEnm.ReceivedQty) = Val(Me.txtTotalQuantity.Text)
            drGrd.Item(grdEnm.RejectedQty) = Val(0)
            drGrd.Item(grdEnm.DamagedQty) = Val(0)
            drGrd.Item(grdEnm.Remarks) = ""
            drGrd.Item(grdEnm.RejectReason) = ""
            drGrd.Item(grdEnm.RejectRemarks) = ""
            drGrd.Item(grdEnm.DamageReason) = ""
            drGrd.Item(grdEnm.DamageRemarks) = ""
            drGrd.Item(grdEnm.DispatchDetailId) = Val(0)
            drGrd.Item(grdEnm.FromLocationId) = Val(0)
            dtGrd.Rows.InsertAt(drGrd, 0)
            dtGrd.AcceptChanges()
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
    '        dblTotalAmount = dblTotalAmount + Val(grd.Rows(i).Cells("total").Value)
    '        dblTotalQty = dblTotalQty + Val(grd.Rows(i).Cells("Qty").Value)
    '    Next
    '    txtAmount.Text = dblTotalAmount
    '    txtTotalQty.Text = dblTotalQty
    '    txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)

    '    Me.lblRecordCount.Text = "Total Records: " & Me.grd.RowCount


    'End Sub

    Private Sub FillCombo(ByVal strCondition As String)
        Dim str As String

        If strCondition = "Item" Then
            'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item,  ArticleSizeName as Size, ArticleColorName as Combination,ISNULL(PurchasePrice,0) as Price, ArticleDefView.SizeRangeID as [Size ID] FROM         ArticleDefView where Active=1"
            str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item,  ArticleSizeName as Size, ArticleColorName as Combination,ISNULL(PurchasePrice,0) as Price,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], IsNull(SalePrice, 0) As [Sale Price] FROM         ArticleDefView where Active=1"
            If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                'Comment against Task:2366
                'If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                If flgLocationWiseItem = True Then
                    str += " AND  ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                Else
                    str += str
                End If
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
            'FillDropDown(cmbItem, str)
            FillUltraDropDown(Me.cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                ''TASK TFS1417
                If ShowCostPriceRights = True Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Sale Price").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = False
                Else
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Sale Price").Hidden = False
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = True
                End If
                ''END TASK TFS1417
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
            FillDropDown(Me.cmbSearchLocation, str, True)

        ElseIf strCondition = "ItemFilter" Then
            str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item,  ArticleSizeName as Size, ArticleColorName as Combination,PurchasePrice as Price , ArticleDefView.SizeRangeID as [Size ID], IsNull(SalePrice, 0) As SalePrice FROM         ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue
            FillUltraDropDown(cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            ''TASK TFS1417
            If ShowCostPriceRights = True Then
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Sale Price").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = False
            Else
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Sale Price").Hidden = False
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = True
            End If
            ''END TASK TFS1417
        ElseIf strCondition = "Vendor" Then
            'str = "SELECT     tblCustomer.AccountId AS ID, tblCustomer.CustomerName AS Name, tblListTerritory.TerritoryName AS Territory, tblListCity.CityName AS City,  " & _
            '        "tblListState.StateName AS State, tblCustomer.AccountId AS AcId " & _
            '        "FROM         tblListTerritory INNER JOIN " & _
            '        "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
            '        "tblListState ON tblListCity.StateId = tblListState.StateId INNER JOIN " & _
            '        "tblCustomer ON tblListTerritory.TerritoryId = tblCustomer.Territory"

            'str = "Select  Location_ID as Id , Location_Code as Name from tblDefLocation  "
            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
            FillDropDown(Me.cmbLocation, str, True)

        ElseIf strCondition = "SO" Then
            str = "Select DispatchID, DispatchNo, VendorID, DispatchDate, IsNull(LocationId, 0) As LocationId from DispatchMasterTable where DispatchID not in(select PurchaseOrderId from ReceivingMasterTable where (ReceivingNo like 'SR%' Or ReceivingNo like 'SRN%') ) and (DispatchNo like 'GP%' Or DispatchNo like 'DN%')"
            cmbPo.DataSource = Nothing
            FillDropDown(cmbPo, str)

        ElseIf strCondition = "SOSpecific" Then
            str = "Select DispatchID, DispatchNo, VendorID, DispatchDate, IsNull(LocationId, 0) As LocationId  from DispatchMasterTable where DispatchID not in(select PurchaseOrderId from ReceivingMasterTable where (ReceivingNo like 'SR%' OR ReceivingNo like 'SRN%')) and (DispatchNo like 'GP%' Or DispatchNo like 'DN%') and vendorid=" & Me.cmbLocation.SelectedValue & ""
            FillDropDown(cmbPo, str)

        ElseIf strCondition = "SOComplete" Then
            str = "Select DispatchID, DispatchNo, VendorID, DispatchDate, IsNull(LocationId, 0) As LocationId  from DispatchMasterTable where (DispatchNo like 'GP%' OR DispatchNo like 'DN%') "
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
                      & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")  order by sort_order " _
                      & " Else " _
                      & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation  order by sort_order"


            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns(grdEnm.LocationId).ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
        ElseIf strCondition = "ArticlePack" Then
            Me.cmbUnit.ValueMember = "ArticlePackId"
            Me.cmbUnit.DisplayMember = "PackName"
            Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
        ElseIf strCondition = "Tickets" Then
            'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
            str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Article.ArticleDescription As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join AllocationMaster ON Ticket.PlanTicketsId = AllocationMaster.TicketID Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where AllocationMaster.Status = 1"
            FillDropDown(cmbTicket, str)
        ElseIf strCondition = "RejectReason" Then
            'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
            str = "Select distinct RejectReason, RejectReason From ReceivingDetailTable Where RejectReason <> '' "
            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns("RejectReason").ValueList.PopulateValueList(dt.DefaultView, "RejectReason", "RejectReason")
        ElseIf strCondition = "DamageReason" Then
            'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
            str = "Select distinct DamageReason, DamageReason From ReceivingDetailTable Where DamageReason <> '' "
            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns("DamageReason").ValueList.PopulateValueList(dt.DefaultView, "DamageReason", "DamageReason")
        End If
    End Sub

    Private Sub txtPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaid.TextChanged
        'txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)
        txtBalance.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(txtPaid.Text)
    End Sub

    Private Function Save() As Boolean
        Me.grd.UpdateData()
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("SRN", 6, "ReceivingMasterTable", "ReceivingNo")
        setVoucherNo = Me.txtPONo.Text
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
            'objCommand.CommandText = "Insert into ReceivingMasterTable (locationId,ReceivingNo,ReceivingDate,VendorId,PurchaseOrderId, ReceivingQty,ReceivingAmount, CashPaid, Remarks,UserName) values( " _
            '                        & Me.cmbLocation.SelectedValue & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbLocation.SelectedValue & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text & "',N'" & LoginUserName & "') select @@Identity"
            'ReqId-934 Resolve comma error
            objCommand.CommandText = "Insert into ReceivingMasterTable(locationId,ReceivingNo,ReceivingDate,VendorId,PurchaseOrderId, ReceivingQty, ReceivingAmount, CashPaid, Remarks,UserName, TicketID) values( " _
                                    & Me.cmbLocation.SelectedValue & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbLocation.SelectedValue & "," & Me.cmbPo.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQuantity"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "', " & IIf(Me.cmbTicket.SelectedIndex = -1, 0, Me.cmbTicket.SelectedValue) & ") select @@Identity"

            getVoucher_Id = objCommand.ExecuteScalar 'objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""


            For i = 0 To grd.RowCount - 1

                objCommand.CommandText = ""
                'Task:2644 Validation Engine No And ChassisNo
                Dim strDisp As String = String.Empty
                strDisp = "SELECT * FROM ReceivingDetailTable WHERE (Engine_No <> '' OR Chassis_No <> '')"
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
                'End Task:2644
                'Val(grd.Rows(i).Cells(5).Value)
                '' TAKS TFS1417 added new column of SalePrice on 12-09-2017
                ''TASK TFS1412 Added new column of FromLocationId
                objCommand.CommandText = "Insert into ReceivingDetailTable (ReceivingId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, Pack_Desc, Engine_No,Chassis_No, RejectedQty, DamagedQty, RejectRemarks, RejectReason, DamageRemarks, DamageReason, DispatchDetailId, ReceivedQty, SalePrice, FromLocationId) values( " _
                                         & " Ident_Current('ReceivingMasterTable')," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                                         & " " & Val(grd.GetRows(i).Cells("TotalQuantity").Value) & ", " & Val(grd.GetRows(i).Cells("rate").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & "'," & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationId").Value & ",N'" & grd.GetRows(i).Cells(grdEnm.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "' " _
                                         & " , " & Val(grd.GetRows(i).Cells("RejectedQty").Value) & ", " & Val(grd.GetRows(i).Cells("DamagedQty").Value) & ",N'" & (grd.GetRows(i).Cells("RejectRemarks").Value) & "', N'" & (grd.GetRows(i).Cells("RejectReason").Value) & "', N'" & (grd.GetRows(i).Cells("DamageRemarks").Value) & "', N'" & (grd.GetRows(i).Cells("DamageReason").Value) & "', " & Val(grd.GetRows(i).Cells("DispatchDetailId").Value.ToString) & ", " & Val(grd.GetRows(i).Cells("ReceivedQty").Value) & ", " & Val(grd.GetRows(i).Cells("SalePrice").Value) & ", " & Val(grd.GetRows(i).Cells("FromLocationId").Value) & ") "

                objCommand.ExecuteNonQuery()

                If Val(grd.GetRows(i).Cells("DispatchDetailId").Value.ToString) > 0 AndAlso Val(grd.GetRows(i).Cells("RejectedQty").Value.ToString) > 0 Then
                    objCommand.CommandText = "Update DispatchDetailTable Set RejectedQty = IsNull(RejectedQty, 0) + " & Val(grd.GetRows(i).Cells("RejectedQty").Value) & " Where DispatchDetailId = " & Val(grd.GetRows(i).Cells("DispatchDetailId").Value) & ""
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

            '***********************
            'Deleting Detail
            '***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId

            'objCommand.Transaction = trans

            'objCommand.ExecuteNonQuery()

            '***********************
            'Inserting Debit Amount
            '***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & Val(Me.txtAmount.Text) & ", 0)"

            'objCommand.Transaction = trans
            'objCommand.ExecuteNonQuery()

            '***********************
            'Inserting Credit Amount
            '***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbLocation.SelectedValue & ", " & 0 & ",  " & Val(Me.txtAmount.Text) & ")"
            'objCommand.Transaction = trans
            'objCommand.ExecuteNonQuery()
            If IsValidate() = True Then
                Call New StockDAL().Add(StockMaster, trans)
            End If
            trans.Commit()
            Save = True


            'insertvoucher()
            'Call Save1() 'Upgrading Stock ...
            setEditMode = False
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try
        ''insert Activity Log
        SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.StockReceive, Me.txtPONo.Text.Trim, True)
    End Function

    Sub InsertVoucher()

        Try
            'SaveVoucherEntry(GetVoucherTypeId("SV"), GetNextDocNo("SV", 6, "tblVoucher", "voucher_no"), Me.dtpPODate.Value, "", Nothing, GetConfigValue("ReceivingCreditAccount"), Val(Me.cmbLocation.ActiveRow.Cells(0).Text.ToString), Val(Me.txtAmount.Text), Val(Me.txtAmount.Text), "Both", Me.Name, Me.txtPONo.Text, True)
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try

    End Sub

    Private Function FormValidate() As Boolean

        If txtPONo.Text = "" Then
            msg_Error("Please enter PO No.")
            txtPONo.Focus() : FormValidate = False : Exit Function
        End If

        If cmbLocation.SelectedIndex <= 0 Then
            msg_Error("Please select Location")
            cmbLocation.Focus() : FormValidate = False : Exit Function
        End If

        If Not Me.grd.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            cmbItem.Focus() : FormValidate = False : Exit Function
        End If

        'Task:2644 Validation Engine No and Chassis No.
        Me.grd.UpdateData()
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

        'If Not Me.grdSaved.RowCount > 0 Then Exit Sub
        'If Me.grd.RowCount > 0 Then
        '    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
        'End If

        'txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value.ToString
        'dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
        'txtReceivingID.Text = grdSaved.CurrentRow.Cells("ReceivingId").Value
        ''TODO. ----
        'cmbLocation.Value = grdSaved.CurrentRow.Cells(3).Value

        'txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
        'txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""
        ''Me.cmbSalesMan.SelectedValue = grdSaved.CurrentRow.Cells("EmployeeCode").Value.ToString
        'Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value
        'Call DisplayDetail(grdSaved.CurrentRow.Cells("ReceivingId").Value)
        'GetTotal()
        'Me.SaveToolStripButton.Text = "&Update"
        'Me.cmbPo.Enabled = False

        If blnUpdateAll = False Then
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
        End If
        'Me.FillCombo("SOComplete") 'R933 Commented Dispatch List Data
        txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value
        dtpPODate.Value = CType(grdSaved.CurrentRow.Cells("Date").Value, Date)
        txtReceivingID.Text = grdSaved.CurrentRow.Cells("ReceivingID").Value
        cmbLocation.SelectedValue = grdSaved.CurrentRow.Cells("VendorId").Value 'cmbLocation.FindStringExact((grdSaved.CurrentRow.Cells(3).Value))
        cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PurchaseOrderid").Value
        cmbTicket.SelectedValue = Me.grdSaved.CurrentRow.Cells("TicketID").Value
        ''R933 Validate Dispatch Data
        If cmbPo.SelectedValue Is Nothing Then



            Dim dt As DataTable = CType(Me.cmbPo.DataSource, DataTable)
            dt.AcceptChanges()
            'Dim dt As New DataTable("PO", "POP")
            'dt.Columns.Add("PurchaseOrderID", GetType(System.Int32))
            'dt.Columns.Add("DispatchNo", GetType(System.String))
            Dim dr As DataRow
            dr = dt.NewRow
            dr(0) = Val(Me.grdSaved.CurrentRow.Cells("PurchaseOrderid").Value.ToString)
            dr(1) = Me.grdSaved.CurrentRow.Cells("DispatchNo").Value
            dr(2) = Val(Me.grdSaved.GetRow.Cells("VendorID").Value.ToString)
            dt.Rows.Add(dr)
            dt.AcceptChanges()
            'Me.cmbPo.DataSource = Nothing
            'Me.cmbPo.ValueMember = "PurchaseOrderID"
            'Me.cmbPo.DisplayMember = "DispatchNo"
            'Me.cmbPo.DataSource = dt
            Me.cmbPo.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("PurchaseOrderid").Value.ToString)
            Me.cmbLocation.SelectedValue = Val(grdSaved.CurrentRow.Cells("VendorId").Value.ToString)
        End If
        ''End R933
        txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
        txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""

        Call DisplayDetail(grdSaved.CurrentRow.Cells("ReceivingID").Value)
        Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns(grdEnm.Total), Janus.Windows.GridEX.AggregateFunction.Sum)
        'GetTotal()
        Me.cmbPo.Enabled = False
        Me.cmbLocation.Enabled = False
        Me.BtnSave.Text = "&Update"
        GetSecurityRights()
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
        Me.BtnDelete.Visible = True
        Me.BtnPrint.Visible = True
        ''''''''''''''''''''''''''

    End Sub

    Private Sub DisplayPODetail(ByVal ReceivingID As Integer)
        Try
            Dim str As String
            'Dim i As Integer

            'str = "SELECT Recv.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '      & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc  FROM dbo.DispatchDetailTable Recv_D INNER JOIN " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId INNER JOIN DispatchMasterTable Recv ON Recv.DispatchId = Recv_D.DispatchId LEFT OUTER JOIN " _
            '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '      & " Where Recv_D.DispatchID =" & ReceivingID & ""

            ''Commented below against Task-408 on 13-06-2016
            'str = "SELECT Recv.LocationId,  Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '     & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '     & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, Recv.Remarks, IsNull(Recv_D.Qty, 0) As TotalQuantity FROM dbo.DispatchDetailTable Recv_D INNER JOIN " _
            '     & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId INNER JOIN DispatchMasterTable Recv ON Recv.DispatchId = Recv_D.DispatchId LEFT OUTER JOIN " _
            '     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '     & " Where Recv_D.DispatchID =" & ReceivingID & ""
            ''TASK TFS1412 Added new column of FromLocationId
            ''TFS4347 : Ayesha Rehman : Added Column Size and Color
            str = "SELECT Recv.LocationId,  Article.ArticleCode, Article.ArticleDescription AS item,ArticleSizeDefTable.ArticleSizeName As Size, ArticleColorDefTable.ArticleColorName As Color , Recv_D.BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, IsNull(Recv_D.Qty, 0) As TotalQuantity, IsNull(Recv_D.Qty, 0) As ReceivedQty, IsNull(Recv_D.RejectedQty, 0) As RejectedQty, 0 As DamagedQty, Recv_D.Price as Rate, IsNull(Recv_D.SalePrice, 0) As SalePrice, " _
               & " (IsNull(Recv_D.Qty, 0)* " & IIf(ShowCostPriceRights = True, "IsNull(Recv_D.Price, 0)", "IsNull(Recv_D.SalePrice, 0)") & ") AS Total, " _
               & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.Price as CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, '' As RejectReason, '' As RejectRemarks, '' As DamageReason,  '' As DamageRemarks,  Recv.Remarks, IsNull(Recv_D.DispatchDetailId, 0) As DispatchDetailId, IsNull(Recv_D.LocationId, 0) As FromLocationId FROM dbo.DispatchDetailTable Recv_D INNER JOIN " _
               & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId INNER JOIN DispatchMasterTable Recv ON Recv.DispatchId = Recv_D.DispatchId LEFT OUTER JOIN " _
               & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
               & " Left Outer Join ArticleColorDefTable On Article.ArticleColorId = ArticleColorDefTable.ArticleColorId Left Outer Join ArticleSizeDefTable on Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
               & " Where Recv_D.DispatchID =" & ReceivingID & ""

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

            '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo"), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)("BatchID").ToString, objDataSet.Tables(0).Rows(i)("LocationId").ToString)

            '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

            'Next
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            dtDisplayDetail.AcceptChanges()
            ''Commented below row for TASK-408 to add TotalQty instead of Qty
            '' dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',((PackQty*Qty)*Rate), (Qty*Rate))"
            'dtDisplayDetail.Columns("Total").Expression = "[TotalQuantity]*[Rate]" ''" & IIf(ShowCostPriceRights = True, "Rate", "SalePrice") & "
            dtDisplayDetail.Columns("Total").Expression = "[TotalQuantity]* " & IIf(ShowCostPriceRights = True, "Rate", "SalePrice") & "" ''" & IIf(ShowCostPriceRights = True, "Rate", "SalePrice") & "

            'dtDisplayDetail.Columns("ReceivedQty").Expression = "[TotalQuantity]-[RejectedQty]-[DamagedQty]"
            If dtDisplayDetail.Rows.Count > 0 Then
                Me.txtRemarks.Text = dtDisplayDetail.Rows.Item(0).Item("Remarks").ToString
            End If
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            FillCombo("grdLocation")
            FillCombo("RejectReason")
            FillCombo("DamageReason")
            ApplyGridSettings()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayDetail(ByVal ReceivingID As Integer)
        Try

            Dim str As String
            'Dim i As Integer

            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '      & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '      & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '      & " Where Recv_D.ReceivingID =" & ReceivingID & ""
            'task 2644 bind engine no and chesis no in grid
            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '  & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, ReceivingDetailTable.Engine_No as [Engine No], ReceivingDetailTable.Chesis_No as [Chesis No] FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
            '  & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '  & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '  & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '  & " Where Recv_D.ReceivingID =" & ReceivingID & ""
            'end task 2644

            ''Commented below against TASK-408 on 13-06-2016
            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            ' & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            ' & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No as [Engine_No], Recv_D.Chassis_No as [Chassis_No], '' as Remarks , IsNull(Recv_D.Qty, 0) As TotalQuantity  FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
            ' & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            ' & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            ' & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            ' & " Where Recv_D.ReceivingID =" & ReceivingID & ""
            ''TASK TFS1412 Added new column of FromLocationId
            ''TFS4347 : Ayesha Rehman : Added Column Size and Color
            str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item,ArticleSizeDefTable.ArticleSizeName As Size, ArticleColorDefTable.ArticleColorName As Color, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, IsNull(Recv_D.Qty, 0) As TotalQuantity, IsNull(Recv_D.ReceivedQty, 0) As ReceivedQty, IsNull(Recv_D.RejectedQty, 0) As RejectedQty, IsNull(Recv_D.DamagedQty, 0) As DamagedQty, Recv_D.Price as Rate, ISNULL(Recv_D.SalePrice, 0) As SalePrice,  " _
           & " (IsNull(Recv_D.Qty, 0)* " & IIf(ShowCostPriceRights = True, "IsNull(Recv_D.Price, 0)", "IsNull(Recv_D.SalePrice, 0)") & ") AS Total, " _
           & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No as [Engine_No], Recv_D.Chassis_No as [Chassis_No],  Recv_D.RejectReason, Recv_D.RejectRemarks, Recv_D.DamageReason, Recv_D.DamageRemarks,  '' as Remarks, IsNull(Recv_D.DispatchDetailId, 0) As DispatchDetailId, IsNull(Recv_D.FromLocationId, 0) As FromLocationId FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
           & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
           & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
           & " Left Outer Join ArticleColorDefTable On Article.ArticleColorId = ArticleColorDefTable.ArticleColorId Left Outer Join ArticleSizeDefTable on Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
           & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
           & " Where Recv_D.ReceivingID =" & ReceivingID & ""

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

            '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)("BatchNo").ToString, objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)("BatchID").ToString, objDataSet.Tables(0).Rows(i)("LocationID"))

            '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
            '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

            'Next
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            dtDisplayDetail.AcceptChanges()
            ''Commeted below row for TASK-408 to add TotalQty instead of Qty
            ''dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',((PackQty*Qty)*Rate), (Qty*Rate))"
            'dtDisplayDetail.Columns("Total").Expression = "[TotalQuantity]*[Rate]" ''" & IIf(ShowCostPriceRights = True, "Rate", "SalePrice") & "
            dtDisplayDetail.Columns("Total").Expression = "[TotalQuantity]* " & IIf(ShowCostPriceRights = True, "Rate", "SalePrice") & "" ''" & IIf(ShowCostPriceRights = True, "Rate", "SalePrice") & "

            'dtDisplayDetail.Columns("ReceivedQty").Expression = "[TotalQuantity]-[RejectedQty]-[DamagedQty]"
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            FillCombo("grdLocation")
            FillCombo("RejectReason")
            FillCombo("DamageReason")
            ApplyGridSettings()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayAllocationDetail(ByVal TicketID As Integer)
        Try
            Dim str As String = String.Empty

            'str = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '          & " (IsNull(Recv_D.Qty, 0)*IsNull(Recv_D.Price, 0)) AS Total, " _
            '          & " Article.ArticleGroupId as CategoryId, Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Recv_D.BatchID, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No as [Engine_No], Recv_D.Chassis_No as [Chassis_No], '' as Remarks , IsNull(Recv_D.Qty, 0) As TotalQuantity  FROM dbo.ReceivingDetailTable Recv_D INNER JOIN " _
            '          & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '          & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
            '          & " INNER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID " _
            '          & " Where Recv_D.ReceivingID =" & ReceivingID & ""
            ''TFS4347 : Ayesha Rehman : Added Column Size and Color
            str = "SELECT 1 As LocationID, Article.ArticleCode, Article.ArticleDescription AS item,ArticleSizeDefTable.ArticleSizeName As Size, ArticleColorDefTable.ArticleColorName As Color , '' As BatchNo, '' As unit, IsNull(Recv_D.Quantity, 0) AS Qty, 0 as Rate, 0 As SalePrice, " _
          & " 0 AS Total, " _
          & " Article.ArticleGroupId as CategoryId, Recv_D.ProductID as ItemId, 0 as PackQty, 0 As CurrentPrice, 0 As BatchID, '' as Pack_Desc, '' As [Engine_No], '' as [Chassis_No], '' as Remarks, IsNull(Recv_D.Quantity, 0) As TotalQuantity FROM AllocationDetail Recv_D INNER JOIN AllocationMaster ON Recv_D.Master_Allocation_ID = AllocationMaster.Master_Allocation_ID INNER JOIN " _
          & " dbo.ArticleDefTable Article ON Recv_D.ProductID = Article.ArticleId LEFT OUTER JOIN " _
          & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
          & " Left Outer Join ArticleColorDefTable On Article.ArticleColorId = ArticleColorDefTable.ArticleColorId Left Outer Join ArticleSizeDefTable on Article.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
          & " Where AllocationMaster.TicketID =" & TicketID & ""

            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            dtDisplayDetail.AcceptChanges()
            'dtDisplayDetail.Columns("Total").Expression = "[TotalQuantity]*[Rate]"
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtDisplayDetail
            ApplyGridSettings()
            FillCombo("grdLocation")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function Update_Record() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer

        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        'Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtPONo.Text)
        'Dim strVoucherNo As String = String.Empty
        'Dim dt As DataTable = GetRecords("SELECT voucher_no   FROM tblVoucher  WHERE voucher_id = " & lngVoucherMasterId & " ")
        'If Not dt Is Nothing Then
        '    If Not dt.Rows.Count = 0 Then
        '        strVoucherNo = dt.Rows(0)("voucher_no")
        '    End If
        'End If
        'Dim AccountId As Integer = GetConfigValue("PurchaseDebitAccount")
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try

            objCommand.Connection = objCon
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'Before against request no. 934
            'objCommand.CommandText = "Update ReceivingMasterTable set ReceivingNo =N'" & txtPONo.Text & "',ReceivingDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbLocation.SelectedValue & ", PurchaseOrderId=" & Me.cmbPo.SelectedValue & ", " _
            '& " ReceivingQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",ReceivingAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text & "',UserName=N'" & LoginUserName & "', LocationID=" & Me.cmbLocation.SelectedValue & " Where ReceivingID= " & txtReceivingID.Text & " "
            'ReqId-934 Resolve Comma Error
            objCommand.CommandText = "Update ReceivingMasterTable set ReceivingNo =N'" & txtPONo.Text & "',ReceivingDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbLocation.SelectedValue & ", PurchaseOrderId=" & Me.cmbPo.SelectedValue & ", " _
          & " ReceivingQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQuantity"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",ReceivingAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', LocationID=" & Me.cmbLocation.SelectedValue & ", TicketID=" & IIf(Me.cmbTicket.SelectedIndex = -1, 0, Me.cmbTicket.SelectedValue) & " Where ReceivingID= " & txtReceivingID.Text & " "


            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from ReceivingDetailTable where ReceivingID = " & txtReceivingID.Text
            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""

            For i = 0 To grd.RowCount - 1
                objCommand.CommandText = ""
                'Before against request no. 934
                'objCommand.CommandText = "Insert into ReceivingDetailTable (ReceivingId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, Pack_Desc) values( " _
                '                         & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                '                         & " " & IIf(grd.GetRows(i).Cells("Unit").Value = "Loose", Val(grd.GetRows(i).Cells("qty").Value), (Val(grd.GetRows(i).Cells("Qty").Value) * Val(grd.GetRows(i).Cells("PackQty").Value))) & ", " & Val(grd.GetRows(i).Cells("rate").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value & "'," & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationId").Value & ",N'" & grd.GetRows(i).Cells(grdEnm.Pack_Desc).Value.ToString.Replace("'", "''") & "') "
                'ReqId-934 Resolve Comma Error

                'Task:2644 Validation Engine No And ChassisNo
                Dim strDisp As String = String.Empty
                strDisp = "SELECT * FROM ReceivingDetailTable WHERE (Engine_No <> '' OR Chassis_No <> '') AND ReceivingId <> " & grdSaved.CurrentRow.Cells(6).Value & ""
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

                If Val(grd.GetRows(i).Cells("DispatchDetailId").Value) > 0 AndAlso Val(grd.GetRows(i).Cells("RejectedQty").Value) > 0 Then
                    objCommand.CommandText = "Update DispatchDetailTable Set RejectedQty = IsNull(RejectedQty, 0) - " & Val(grd.GetRows(i).Cells("RejectedQty").Value) & " Where DispatchDetailId = " & Val(grd.GetRows(i).Cells("DispatchDetailId").Value) & ""
                    objCommand.ExecuteNonQuery()
                End If
                ''TASK TFS1412 Added new column of FromLocationId
                objCommand.CommandText = "Insert into ReceivingDetailTable (ReceivingId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID,LocationID, Pack_Desc, Engine_No,Chassis_No, RejectedQty, DamagedQty, RejectRemarks, RejectReason, DamageRemarks, DamageReason, DispatchDetailId, ReceivedQty, SalePrice, FromLocationId) values( " _
                                         & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells("ItemId").Value) & ",N'" & (grd.GetRows(i).Cells("Unit").Value) & "'," & Val(grd.GetRows(i).Cells("Qty").Value) & ", " _
                                         & " " & Val(grd.GetRows(i).Cells(grdEnm.TotalQuantity).Value) & ", " & Val(grd.GetRows(i).Cells("rate").Value) & ", " & Val(grd.GetRows(i).Cells("PackQty").Value) & " , " & Val(grd.GetRows(i).Cells("CurrentPrice").Value) & ",N'" & grd.GetRows(i).Cells("BatchNo").Value.ToString.Replace("'", "''") & "'," & grd.GetRows(i).Cells("BatchID").Value & "," & grd.GetRows(i).Cells("LocationId").Value & ",N'" & grd.GetRows(i).Cells(grdEnm.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Engine_No").Value.ToString.Replace("'", "''") & "', N'" & Me.grd.GetRows(i).Cells("Chassis_No").Value.ToString.Replace("'", "''") & "', " _
                                         & " " & Val(grd.GetRows(i).Cells("RejectedQty").Value) & ", " & Val(grd.GetRows(i).Cells("DamagedQty").Value) & ", N'" & (grd.GetRows(i).Cells("RejectRemarks").Value) & "', N'" & (grd.GetRows(i).Cells("RejectReason").Value) & "', N'" & (grd.GetRows(i).Cells("DamageRemarks").Value) & "', N'" & (grd.GetRows(i).Cells("DamageReason").Value) & "', " & Val(grd.GetRows(i).Cells("DispatchDetailId").Value) & ", " & Val(grd.GetRows(i).Cells("ReceivedQty").Value) & ", " & Val(grd.GetRows(i).Cells("SalePrice").Value) & ", " & Val(grd.GetRows(i).Cells("FromLocationId").Value) & ") "
                objCommand.ExecuteNonQuery()
                If Val(grd.GetRows(i).Cells("DispatchDetailId").Value) > 0 AndAlso Val(grd.GetRows(i).Cells("RejectedQty").Value) > 0 Then
                    objCommand.CommandText = "Update DispatchDetailTable Set RejectedQty = IsNull(RejectedQty, 0) + " & Val(grd.GetRows(i).Cells("RejectedQty").Value) & " Where DispatchDetailId = " & Val(grd.GetRows(i).Cells("DispatchDetailId").Value) & ""
                    objCommand.ExecuteNonQuery()
                End If
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

            '***********************
            'Inserting Credit Amount
            ''***********************
            'objCommand = New OleDbCommand
            'objCommand.Connection = Con
            'objCommand.CommandText = ""
            'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
            '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.cmbLocation.SelectedValue & ", " & 0 & ",  " & Val(Me.txtAmount.Text) & ")"
            'objCommand.Transaction = trans
            'objCommand.ExecuteNonQuery()
            If IsValidate() = True Then
                StockMaster.StockTransId = StockTransId(Me.txtPONo.Text, trans)
                Call New StockDAL().Update(StockMaster, trans)
            End If

            trans.Commit()
            Update_Record = True
            'insertvoucher()
            'Call Update1() 'Upgranding Stock ...
            setVoucherNo = Me.txtPONo.Text
            getVoucher_Id = Me.txtReceivingID.Text
            setEditMode = True
            Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try
        SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.StockReceive, Me.txtPONo.Text.Trim, True)
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
                ShowErrorMessage("Your can not change this becuase financial year is closed")
                Me.dtpPODate.Focus()
                Exit Sub
            End If
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
                        'DisplayRecord()R933 Commented History Data

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
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'Dim s As String
            's = "1234-567-890"
            'MsgBox(Microsoft.VisualBasic.Right(s, InStr(1, s, "-") - 2))
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If

            RefreshControls()

        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            EditRecord()
        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSaved_CellDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        ''Task# A2-10-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
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
        ''End Task# A2-10-06-2015

    End Sub

    Private Sub cmbPo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPo.SelectedIndexChanged

        If Me.cmbPo.SelectedIndex > 0 Then
            Me.DisplayPODetail(Me.cmbPo.SelectedValue)

            'Dim adp As New OleDbDataAdapter
            'Dim dt As New DataTable
            'Dim Sql As String = "SELECT     dbo.PurchaseOrderMasterTable.VendorId, dbo.vwCOADetail.detail_title FROM         dbo.PurchaseOrderMasterTable INNER JOIN                       dbo.vwCOADetail ON dbo.PurchaseOrderMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id where PurchaseOrderMasterTable.PurchaseOrderId=" & Me.cmbPo.SelectedValue
            'adp = New OleDbDataAdapter(Sql, Con)
            'adp.Fill(dt)

            ''Commented Against TFS4345 : Not getting the right (from location) of Stock
            ''Me.cmbLocation.SelectedValue = CType(Me.cmbPo.SelectedItem, DataRowView).Item("VendorID")
            If Me.grd.RowCount > 0 Then
                Me.cmbLocation.SelectedValue = Me.grd.CurrentRow.Cells(grdEnm.FromLocationId).Value
            End If
            Me.cmbLocation.Enabled = False


            'If Not dt.Rows.Count > 0 Then
            '    Me.cmbLocation.Enabled = True : Me.cmbLocation.SelectedIndex = 0
            'Else
            '    Me.cmbLocation.SelectedValue = dt.Rows(0).Item("VendorId").ToString
            '    Me.cmbLocation.Enabled = False
            'End If
            'GetTotal()
        Else
            Me.cmbLocation.Enabled = True
            'Me.cmbLocation.Rows(0).Activate()
        End If
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
            Else
                Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            End If
            Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            Me.txtSalePrice.Text = Me.cmbItem.ActiveRow.Cells("Sale Price").Value.ToString
            If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
            'Me.cmbLocation.DisplayLayout.Grid.Show( me.cmbLocation.contr)

            Dim strSQl As String = String.Empty

            'If GetConfigValue("WithSizeRange") = "False" Then
            '    strSQl = "SELECT Stock, BatchNo FROM         dbo.vw_Batch_Stock WHERE     (NOT (Stock = 0))and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            If getConfigValueByType("WithSizeRange") = "True" Then
                strSQl = "SELECT     ISNULL(a.Stock, 0) AS Stock, PurchaseBatchTable.BatchNo AS [Batch No] , PurchaseBatchTable.BatchID" _
                        & " FROM         SizeRangeTable INNER JOIN" _
                        & "                   PurchaseBatchTable ON SizeRangeTable.BatchID = PurchaseBatchTable.BatchID LEFT OUTER JOIN " _
                        & "                    (SELECT     * " _
                        & "   FROM vw_Batch_Stock " _
                        & "                WHERE      articleID = " & Me.cmbItem.Value & ") a ON PurchaseBatchTable.BatchID = a.BatchID " _
                        & "WHERE(SizeRangeTable.SizeID = " & IIf(Val(Me.cmbItem.ActiveRow.Cells("Size ID").Value.ToString) > 0, Me.cmbItem.ActiveRow.Cells("Size ID").Value, 0) & ") "
            Else
                strSQl = "SELECT Stock, BatchNo as [Batch No], BatchID FROM  dbo.vw_Batch_Stock WHERE     (NOT (Stock = 0))and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            End If

            'FillUltraDropDown(cmbBatchNo, strSQl, False)

            cmbBatchNo.DataSource = Nothing

            Dim objCommand As New OleDbCommand
            Dim objDataAdapter As New OleDbDataAdapter
            Dim objDataSet As New DataSet

            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSQl


            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)

            dr = dt.NewRow
            dr.Item(1) = "xxxx"
            dr.Item(2) = 0
            dr.Item(0) = 0
            dt.Rows.Add(dr)


            cmbBatchNo.DisplayMember = "Batch No"
            cmbBatchNo.ValueMember = "BatchID"
            'dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName"
            'cmbBatchNo.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            cmbBatchNo.DataSource = dt
            cmbBatchNo.DisplayLayout.Bands(0).Columns("BatchID").Hidden = True
            Me.cmbBatchNo.Rows(0).Activate()
            Con.Close()
            Me.cmbBatchNo.Enabled = True
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            cmbUnit_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Enter
        Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
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
        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

        'Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, grdSaved.CurrentRow.Cells(0).Value.ToString)

        'Dim strVoucherNo As String = String.Empty
        'Dim dt As DataTable = GetRecords("SELECT voucher_no   FROM tblVoucher  WHERE voucher_id = " & lngVoucherMasterId & " ")

        'If Not dt Is Nothing Then
        '    If Not dt.Rows.Count = 0 Then
        '        strVoucherNo = dt.Rows(0)("voucher_no")
        '    End If
        'End If


        'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Dim objTrans As OleDbTransaction
        If Con.State = ConnectionState.Closed Then Con.Open()
        objTrans = Con.BeginTransaction
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim cm As New OleDbCommand
            For i As Integer = 0 To Me.grd.RowCount - 1
                If Val(grd.GetRows(i).Cells("DispatchDetailId").Value) > 0 AndAlso Val(grd.GetRows(i).Cells("RejectedQty").Value) > 0 Then
                    'objCommand.CommandText = "Update DispatchDetailTable Set RejectedQty = IsNull(RejectedQty, 0) + " & Val(grd.GetRows(i).Cells("RejectedQty").Value) & " Where DispatchDetailId = " & Val(grd.GetRows(i).Cells("DispatchDetailId").Value) & ""
                    'objCommand.ExecuteNonQuery()
                    cm = New OleDbCommand
                    cm.Connection = Con
                    cm.CommandText = " Update DispatchDetailTable Set RejectedQty = IsNull(RejectedQty, 0) - " & Val(grd.GetRows(i).Cells("RejectedQty").Value) & " Where DispatchDetailId = " & Val(grd.GetRows(i).Cells("DispatchDetailId").Value) & ""
                    cm.Transaction = objTrans
                    cm.ExecuteNonQuery()
                End If
            Next
            cm.Connection = Con
            cm.CommandText = "delete from ReceivingDetailTable where Receivingid=" & Me.grdSaved.CurrentRow.Cells("ReceivingID").Value.ToString
            cm.Transaction = objTrans
            cm.ExecuteNonQuery()




            cm = New OleDbCommand
            cm.Connection = Con
            cm.CommandText = "delete from ReceivingMasterTable where Receivingid=" & Me.grdSaved.CurrentRow.Cells("ReceivingID").Value.ToString

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


            StockMaster = New StockMaster
            StockMaster.StockTransId = Convert.ToInt32(StockTransId(Me.grdSaved.CurrentRow.Cells(0).Value, objTrans))
            Call New StockDAL().Delete(StockMaster, objTrans)

            objTrans.Commit()
            'Call Delete() 'Upgrading Stock ...
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 27-1-14 
            'msg_Information(str_informDelete)
            Me.txtReceivingID.Text = 0
            SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.StockReceive, Me.grdSaved.CurrentRow.Cells(0).Value.ToString, True)
            Me.RefreshControls()
            Me.grdSaved.CurrentRow.Delete()
        Catch ex As Exception
            objTrans.Rollback()
            msg_Error("Error occured while deleting record: " & ex.Message)
        Finally
            Con.Close()
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try

    End Sub
    Private Sub cmbLocation_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Me.cmbLocation.ActiveRow.Cells(0).Value > 0 Then
        '    Me.FillCombo("SOSpecific")
        'Else
        '    Me.FillCombo("SO")
        'End If
    End Sub
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("ReceivingNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            AddRptParam("@ReceivingId", Me.grdSaved.GetRow.Cells("ReceivingID").Value)
            ShowReport("StockReceiving")
        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
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
                Me.tsbAssign1.Enabled = True
                Me.tsbConfig.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmStockReceive)
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
                Me.tsbAssign1.Enabled = False
                Me.tsbConfig.Enabled = False 'Task:2406 Added Field Chooser Rights
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
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    ElseIf RightsDt.FormControlName = "Config" Then
                        Me.tsbConfig.Enabled = True
                    ElseIf RightsDt.FormControlName = "Task" Then
                        Me.tsbAssign1.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
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
        Me.Cursor = Cursors.WaitCursor
        Try
            'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''END TFS4544
            Dim id As Integer = 0
            id = Me.cmbLocation.SelectedValue
            FillCombo("Vendor")
            Me.cmbLocation.SelectedValue = id

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

            FillCombo("grdLocation")

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
            ''start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161
        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If col.Index <> grdEnm.LocationId AndAlso col.Index <> grdEnm.Rate AndAlso col.Index <> grdEnm.Engine_No AndAlso col.Index <> grdEnm.Chassis_No AndAlso col.Index <> grdEnm.ReceivedQty AndAlso col.Index <> grdEnm.RejectedQty AndAlso col.Index <> grdEnm.DamagedQty AndAlso col.Index <> grdEnm.RejectRemarks AndAlso col.Index <> grdEnm.RejectReason AndAlso col.Index <> grdEnm.DamageRemarks AndAlso col.Index <> grdEnm.DamageReason Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next

            Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("unit").Index
            Me.grd.RootTable.Columns("unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index
            'Task:2759 Set Rounded Amount Format
            Me.grd.RootTable.Columns(grdEnm.Total).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdEnm.Total).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            Dim Style As New Janus.Windows.GridEX.GridEXFormatStyle
            Style.BackColor = Color.LightYellow
            Me.grd.RootTable.Columns(grdEnm.ReceivedQty).CellStyle = Style
            Style = New Janus.Windows.GridEX.GridEXFormatStyle
            Style.BackColor = Color.LightYellow
            Me.grd.RootTable.Columns(grdEnm.RejectedQty).CellStyle = Style
            Style = New Janus.Windows.GridEX.GridEXFormatStyle
            Style.BackColor = Color.LightYellow
            Me.grd.RootTable.Columns(grdEnm.DamagedQty).CellStyle = Style
            Me.grd.RootTable.Columns(grdEnm.Total).TotalFormatString = "N" & TotalAmountRounding
            ''TASK TFS1417 
            If ShowCostPriceRights = True Then
                Me.grd.RootTable.Columns(grdEnm.Rate).Visible = True
                Me.grd.RootTable.Columns(grdEnm.SalePrice).Visible = False
            Else
                Me.grd.RootTable.Columns(grdEnm.Rate).Visible = False
                Me.grd.RootTable.Columns(grdEnm.SalePrice).Visible = True
            End If
            '' End TASK TFS1417 
            'End Task:2759
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
            StockMaster = New StockMaster
            StockMaster.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
            StockMaster.DocNo = Me.txtPONo.Text.ToString.Replace("'", "''")
            StockMaster.DocDate = Me.dtpPODate.Value.Date
            StockMaster.DocType = Convert.ToInt32(GetStockDocTypeId("GRN").ToString)
            StockMaster.Remaks = Me.txtRemarks.Text.ToString.Replace("'", "''")
            StockMaster.AccountId = Me.cmbLocation.SelectedValue
            StockMaster.StockDetailList = New List(Of StockDetail)
            For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                StockDetail = New StockDetail
                StockDetail.StockTransId = Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                StockDetail.LocationId = grdRow.Cells("LocationID").Value
                StockDetail.ArticleDefId = grdRow.Cells("ItemId").Value
                'StockDetail.InQty = Val(grdRow.Cells("TotalQuantity").Value) ''TASK-408
                StockDetail.InQty = Val(grdRow.Cells("ReceivedQty").Value) ''TASK-408
                StockDetail.OutQty = 0
                StockDetail.Rate = Val(grdRow.Cells("Rate").Value)
                'StockDetail.Rate = 'GetAvgRateByItem(Val(grdRow.Cells("ItemId").Value)) 'Val(grdRow.Cells("Rate").Value)
                'StockDetail.InAmount = (Val(grdRow.Cells("TotalQuantity").Value) * Val(StockDetail.Rate)) ''TASK-408
                StockDetail.InAmount = (Val(grdRow.Cells("ReceivedQty").Value) * Val(StockDetail.Rate)) ''TASK-408
                StockDetail.OutAmount = 0
                StockDetail.Remarks = String.Empty 'grdRow.Cells("Comments").Value.ToString
                ''TASK-470 on 01-07-2016
                StockDetail.PackQty = Val(grdRow.Cells("PackQty").Value.ToString)
                StockDetail.In_PackQty = Val(grdRow.Cells("Qty").Value.ToString)
                StockDetail.Out_PackQty = 0
                ''End TASK-470
                StockMaster.StockDetailList.Add(StockDetail)
                ''TASK TFS1412 Rejected quantity should be added to dispatch location
                If grdRow.Cells("FromLocationId").Value > 0 AndAlso grdRow.Cells("RejectedQty").Value > 0 Then
                    StockDetail = New StockDetail
                    StockDetail.StockTransId = Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                    StockDetail.LocationId = grdRow.Cells("FromLocationId").Value
                    StockDetail.ArticleDefId = grdRow.Cells("ItemId").Value
                    'StockDetail.InQty = Val(grdRow.Cells("TotalQuantity").Value) ''TASK-408
                    StockDetail.InQty = Val(grdRow.Cells("RejectedQty").Value) ''TASK-408
                    StockDetail.OutQty = 0
                    StockDetail.Rate = Val(grdRow.Cells("Rate").Value)
                    'StockDetail.Rate = 'GetAvgRateByItem(Val(grdRow.Cells("ItemId").Value)) 'Val(grdRow.Cells("Rate").Value)
                    'StockDetail.InAmount = (Val(grdRow.Cells("TotalQuantity").Value) * Val(StockDetail.Rate)) ''TASK-408
                    StockDetail.InAmount = (Val(grdRow.Cells("RejectedQty").Value) * Val(StockDetail.Rate)) ''TASK-408
                    StockDetail.OutAmount = 0
                    StockDetail.Remarks = String.Empty 'grdRow.Cells("Comments").Value.ToString
                    ''TASK-470 on 01-07-2016
                    StockDetail.PackQty = Val(grdRow.Cells("PackQty").Value.ToString)
                    StockDetail.In_PackQty = Val(grdRow.Cells("Qty").Value.ToString)
                    StockDetail.Out_PackQty = 0
                    ''End TASK-470
                    StockMaster.StockDetailList.Add(StockDetail)
                End If
                ''END TASK TFS1412
            Next
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

    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label15.Click

    End Sub
    Private Sub cmbItem_RowSelected(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            Else
                If Me.cmbItem.Value Is Nothing Then Exit Sub
                Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            End If
            FillCombo("ArticlePack")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCode.CheckedChanged, rdoName.CheckedChanged
        Try
            If Not IsFormOpen = True Then Exit Sub
            If rdoCode.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabPageControl1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles UltraTabPageControl1.Paint

    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Stock receive"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
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
    Public Function Get_All(ByVal ReceivingNo As String)
        Try
            Get_All = Nothing
            If IsFormOpen = True Then
                If ReceivingNo.Length > 0 Then
                    Dim str As String = "Select * From ReceivingMasterTable WHERE ReceivingNo=N'" & ReceivingNo & "'"
                    Dim dt As DataTable = GetDataTable(str)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then



                            'IsEditMode = True
                            'FillCombo("Vendor")
                            'FillCombo("CostCenter")
                            Me.txtReceivingID.Text = dt.Rows(0).Item("ReceivingId")
                            Me.txtPONo.Text = dt.Rows(0).Item("ReceivingNo")
                            Me.dtpPODate.Value = dt.Rows(0).Item("ReceivingDate")
                            'Me.cmbVendor.Value = dt.Rows(0).Item("VendorId")
                            Me.cmbLocation.SelectedValue = dt.Rows(0).Item("VendorId")
                            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks")
                            Me.txtPaid.Text = dt.Rows(0).Item("CashPaid")
                            Me.cmbPo.SelectedValue = dt.Rows(0).Item("PurchaseOrderid")
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
                            DisplayDetail(dt.Rows(0).Item("ReceivingId"))
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
            PrintToolStripButton_Click(Nothing, Nothing)
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
    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                DisplayRecord()
                ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Else
                If setEditMode = False Then Me.BtnDelete.Visible = False
                If setEditMode = False Then Me.BtnPrint.Visible = False
                Me.BtnEdit.Visible = False
                '''''''''''''''''''''''''''''''''
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
                    If IO.File.Exists(str_ApplicationStartUpPath & "\Reports\StockReceiving.rpt") = False Then Exit Sub
                    crpt.Load(str_ApplicationStartUpPath & "\Reports\StockReceiving.rpt", DBServerName)
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
                    FileName = "Stock Receiving" & "-" & setVoucherNo & ""
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
                    crpt.SetParameterValue("@ReceivingID", VoucherId)
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
            Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name='frmStockReceive' AND EmailAlert=1")
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
                Email.Subject = "Stock Receiving " & setVoucherNo & ""
                Email.Body = "Stock Receiving " _
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

        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdSaved.RowCount <= 0 Then Exit Sub
            DeleteToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.F5 Then
            BtnRefresh_Click(Nothing, Nothing)
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

    Private Sub btnUpdateAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateAll.Click
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

    ''Task# A1-10-06-2015 Added Key Pres event for some textboxes to take just numeric and dot value
    Private Sub txtNUM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStock.KeyPress, txtTotalQuantity.KeyPress, txtPackQty.KeyPress
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
        'Try
        '    If Val(txtPackQty.Text) > 0 AndAlso Val(txtQty.Text) > 0 Then
        '        Me.txtTotalQty.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
        '        If Val(Me.txtRate.Text) > 0 Then
        '            Me.txtTotal.Text = Val(Me.txtTotalQty.Text) * Val(Me.txtRate.Text)
        '        End If
        '    Else
        '        Me.txtTotalQty.Text = Val(Me.txtQty.Text)
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtQty_LostFocus(sender As Object, e As EventArgs) Handles txtQty.LostFocus
        'If Val(Me.txtPackQty.Text) = 0 Then
        '    txtPackQty.Text = 1
        '    txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
        'Else
        '    txtTotal.Text = Math.Round((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text), DecimalPointInValue)
        'End If
        If Val(Me.txtPackQty.Text) = 0 Then
            Me.txtPackQty.Text = 1
            Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
        Else
            Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
        End If
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        Try
            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    txtTotal.Text = Math.Round(Val(txtQty.Text) * Val(txtRate.Text), DecimalPointInValue)
            'Else
            '    txtTotal.Text = Math.Round((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text), DecimalPointInValue)
            'End If
            'If Val(Me.txtPackQty.Text) = 0 Then
            '    Me.txtPackQty.Text = 1
            '    Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
            'Else
            '    Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            '    If Val(Me.txtRate.Text) > 0 Then
            '        Me.txtTotal.Text = Val(Me.txtTotalQuantity.Text) * Val(txtRate.Text)
            '    End If
            'End If
            If Val(txtPackQty.Text) > 0 Then
                Me.txtTotalQuantity.Text = Val(txtPackQty.Text) * Val(txtQty.Text)
            Else
                Me.txtPackQty.Text = 1
                Me.txtTotalQuantity.Text = Val(txtQty.Text)
            End If
            If Val(txtRate.Text) > 0 Then
                Me.txtTotal.Text = Val(txtTotalQuantity.Text) * Val(txtRate.Text)
            End If
            ''TASK TFS1417
            If Val(txtSalePrice.Text) > 0 Then
                Me.txtSaleTotal.Text = Val(txtTotalQuantity.Text) * Val(txtSalePrice.Text)
            End If
            ''End Task TFS1417
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTotalQty_LostFocus(sender As Object, e As EventArgs) Handles txtTotalQuantity.LostFocus
        'If Val(txtRate.Text) > 0 Then
        '    Me.txtTotal.Text = Val(Me.txtTotalQty.Text) * Val(Me.txtRate.Text)

        'End If
    End Sub

    Private Sub txtTotalQty_TextChanged(sender As Object, e As EventArgs) Handles txtTotalQuantity.TextChanged
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
            GetGridDetailQtyCalculate(e)
            If e.Column.Index = grdEnm.ReceivedQty Or e.Column.Index = grdEnm.RejectedQty Or e.Column.Index = grdEnm.DamagedQty Then
                Dim MergedQty As Double = (Val(Me.grd.GetRow.Cells("ReceivedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("RejectedQty").Value.ToString) + Val(Me.grd.GetRow.Cells("DamagedQty").Value.ToString))
                If MergedQty > Val(Me.grd.GetRow.Cells("TotalQuantity").Value.ToString) Then
                    msg_Error("Values exceeded to total quantity.")
                    If e.Column.Index = grdEnm.ReceivedQty Then
                        Me.grd.GetRow.Cells("ReceivedQty").Value = ReceivedQty
                    End If
                    If e.Column.Index = grdEnm.RejectedQty Then
                        Me.grd.GetRow.Cells("RejectedQty").Value = RejectedQty
                    End If
                    If e.Column.Index = grdEnm.DamagedQty Then
                        Me.grd.GetRow.Cells("DamagedQty").Value = DamagedQty
                    End If
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicket_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTicket.SelectedIndexChanged
        Try
            DisplayAllocationDetail(Val(Me.cmbTicket.SelectedValue))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellValueChanged
        Try
            If Not e.Column Is Nothing Then
                ReceivedQty = Me.grd.GetRow.Cells("ReceivedQty").Value
                RejectedQty = Me.grd.GetRow.Cells("RejectedQty").Value
                DamagedQty = Me.grd.GetRow.Cells("DamagedQty").Value
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSalePrice_TextChanged(sender As Object, e As EventArgs) Handles txtSalePrice.TextChanged
        Try
            Me.txtSaleTotal.Text = Val(txtTotalQuantity.Text) * Val(txtSalePrice.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow

    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Stock receive"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class

