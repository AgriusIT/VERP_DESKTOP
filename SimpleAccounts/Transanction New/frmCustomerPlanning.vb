''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''22-Feb-2014  TASK:M20 Imran Ali Load Sales Order On Customer Planing
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
''06-Sep-2014 Task:2832 Imran Ali Production Plan Status (Converters)
''TASK TFS2181 done by Muhammad Ameen. Addition of new field of Start Date and made Sales order in detail grid as combo
Imports System.Data.OleDb
Public Class frmCustomerPlanning

    Dim dt As DataTable
    Dim Mode As String = "Normal"
    Dim IsOpenForm As Boolean = False
    Dim IsEditMode As Boolean = False
    Dim FlgStoreIssuanceAgainstPlan As Boolean = False
    Dim flgLocationWiseItems As Boolean = False
    Dim flgLoadAllItems As Boolean = False
    Dim flgCompanyRights As Boolean = False
    Dim PlanDetailId As Integer = 0
    Dim PlanId As Integer = 0
    Dim AvailableQty As Double = 0D
    Enum enmGridDetail
        LocationId
        Item
        Unit
        UnitName
        ArticleAliasName
        Qty
        Rate
        Total
        ItemId
        PackQty
        CurrentPrice
        Pack_Desc
        ProducedQty
        PlanDetailId
        SODetailId
        SOId
        PLevelId
        Comments
        DeleteButton
    End Enum

    Private Sub frmCustomerPlanning_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
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
                BtnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Insert Then
                btnAdd_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmCustomerPlanning_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14 
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        If Not getConfigValueByType("StoreIssuaneDependonProductionPlan").ToString = "Error" Then
            FlgStoreIssuanceAgainstPlan = getConfigValueByType("StoreIssuaneDependonProductionPlan")
        End If
        If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
            flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
        End If
        If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
            flgLoadAllItems = getConfigValueByType("LoadAllItemsInSales")
        End If
        If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            flgCompanyRights = getConfigValueByType("CompanyRights")
        End If
        FillCombo("Customer")
        FillCombo("SO")
        FillCombo("SM")
        FillCombo("Location")
        FillCombo("Category")
        FillCombo("Item")
        FillCombo("ArticlePack")
        FillCombo("TicketProduct")
        IsEditMode = False
        Me.DisplayRecord()
        RefreshControls()
        Me.cmbVendor.Focus()
        IsOpenForm = True
        Me.lblProgress.Visible = False
        Me.Cursor = Cursors.Default

    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub
    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Try


            Dim str As String = String.Empty
            'str = "SELECT     Recv.PlanNo, CONVERT(varchar, Recv.PlanDate, 103) AS Date, V.VendorName, Recv.PlanQty, Recv.PlanAmount, " _
            '       & " Recv.PlanId, Recv.VendorCode, Recv.Remarks, convert(varchar, Recv.CashPaid) as CashPaid FROM         dbo.PlanMasterTable Recv INNER JOIN dbo.tblVendor V ON Recv.VendorCode = V.AccountId"
            If Mode = "Normal" Then
                'Before against task:M20
                'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.PlanNo , Recv.PlanDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PlanQty,  " & _
                '        "Recv.PlanAmount, Recv.PlanId, Recv.CustomerID, Recv.Remarks " & _
                '        "FROM         dbo.PlanMasterTable Recv LEFT OUTER JOIN " & _
                '        "dbo.vwCOADetail ON Recv.CustomerID = dbo.vwCOADetail.coa_detail_id " & _
                '        "ORDER BY Recv.PlanNo DESC"
                'Task:M20 Addd Column PoId and EmployeeCode
                'Before against task:2832 
                'str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.PlanNo , Recv.PlanDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PlanQty,  " & _
                '     "Recv.PlanAmount, Recv.PlanId, Recv.CustomerID, Recv.Remarks, IsNull(Recv.PoId,0) as PoId, Isnull(Recv.EmployeeCode,0) as EmployeeCode " & _
                '     "FROM         dbo.PlanMasterTable Recv LEFT OUTER JOIN " & _
                '     "dbo.vwCOADetail ON Recv.CustomerID = dbo.vwCOADetail.coa_detail_id " & _
                '     "ORDER BY Recv.PlanNo DESC"
                'End Task:M20
                'Task:2832 Added Field SalesOrderNo.
                'TASK TFS1629 Added new column of CompletionDate on 24-10-2017
                ''TASK TFS2181 done by Muhammad Ameen. Addition of new field of Start Date and made Sales order in detail grid as combo
                str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " Recv.PlanNo , Recv.PlanDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PlanQty,  " & _
                    "Recv.PlanAmount, Recv.PlanId, Recv.CustomerID, Recv.Remarks, IsNull(Recv.PoId,0) as PoId,SO.SalesOrderNo, Isnull(Recv.EmployeeCode,0) as EmployeeCode, Recv.CompletionDate , Recv.StartDate " & _
                    "FROM         dbo.PlanMasterTable Recv LEFT OUTER JOIN " & _
                    "dbo.vwCOADetail ON Recv.CustomerID = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN SalesOrderMasterTable SO on SO.SalesOrderId = Recv.PoId " & _
                    "ORDER BY Recv.PlanNo DESC"
                'End Task:2832
            End If
            FillGridEx(grdSaved, str, True)
            'grdSaved.Columns(10).Visible = False
            grdSaved.RootTable.Columns(4).Visible = False
            grdSaved.RootTable.Columns(5).Visible = False
            grdSaved.RootTable.Columns(6).Visible = False
            grdSaved.RootTable.Columns(7).Visible = False
            grdSaved.RootTable.Columns("PoId").Visible = False 'task:M20 Set Hidden Column POId
            grdSaved.RootTable.Columns("EmployeeCode").Visible = False 'task:M20 Set Hidden Column EmployeeCode
            'grdSaved.RootTable.Columns("EmployeeCode").Visible = False
            'grdSaved.RootTable.Columns("PoId").Visible = False

            grdSaved.RootTable.Columns(0).Caption = "Doc No"
            grdSaved.RootTable.Columns(1).Caption = "Date"
            grdSaved.RootTable.Columns(2).Caption = "Customer"
            'grdSaved.RootTable.Columns(3).Caption = "S-Order"
            grdSaved.RootTable.Columns(3).Caption = "Qty"
            grdSaved.RootTable.Columns(4).Caption = "Amount"
            'grdSaved.RootTable.Columns(8).Caption = "Employee"

            grdSaved.RootTable.Columns(0).Width = 100
            grdSaved.RootTable.Columns(1).Width = 150
            grdSaved.RootTable.Columns(2).Width = 250
            grdSaved.RootTable.Columns(3).Width = 50
            grdSaved.RootTable.Columns(4).Width = 80
            grdSaved.RootTable.Columns(7).Width = 100
            grdSaved.RootTable.Columns(5).Width = 150
            ' grdSaved.RowHeadersVisible = False
            Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("CompletionDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("StartDate").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw ex
        End Try
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

        txtPONo.Text = ""
        dtpPODate.Value = Now
        dtpPODate.Enabled = True
        txtRemarks.Text = ""
        cmbUnit.SelectedIndex = 0
        'Me.cmbSalesMan.SelectedIndex = 0
        txtPaid.Text = ""
        'txtAmount.Text = ""
        txtTotal.Text = ""
        cmbVendor.Rows(0).Activate()
        'txtTotalQty.Text = ""
        txtBalance.Text = ""
        txtPackQty.Text = 1
        Me.BtnSave.Text = "&Save"
        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
            Me.txtPONo.Text = GetSerialNo("PN" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "PlanMasterTable", "PlanNo")
        ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
            Me.txtPONo.Text = GetNextDocNo("PN" & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "PlanMasterTable", "PlanNo")
        Else
            Me.txtPONo.Text = GetNextDocNo("PN", 6, "PlanMasterTable", "PlanNo")
        End If
        Me.cmbPo.Enabled = True
        FillCombo("SO")
        'FillCombo("Customer")
        'DisplayRecord()
        'grd.Rows.Clear()
        Me.cmbVendor.Focus()
        'me.cmbVendor
        If flgLoadAllItems = False Then
            Me.DisplayDetail(-1)
        Else
            Me.DisplayDetail(-1, "All")
        End If
        GetAllTickets(-1)
        Me.GetSecurityRights()

        ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
        Me.BtnDelete.Visible = False
        Me.BtnEdit.Visible = False
        Me.BtnPrint.Visible = False
        Me.gbTicket.Enabled = False
        Me.rdoCode.Checked = True
        Me.dtpCompletionDate.Checked = False
        Me.dtpStartDate.Checked = False
    End Sub

    Private Sub ClearDetailControls()
        'cmbCategory.SelectedIndex = 0
        cmbUnit.SelectedIndex = 0
        txtQty.Text = ""
        txtRate.Text = ""
        txtTotal.Text = ""
        txtPackQty.Text = 1
        Me.txtArticleAliasName.Text = ""
    End Sub

    Private Function Validate_AddToGrid() As Boolean
        If cmbItem.ActiveRow.Cells(0).Value <= 0 AndAlso Me.txtArticleAliasName.Text = "" Then
            msg_Error("Please select an item")
            cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtQty.Text) <= 0 Then
            msg_Error("Quantity is not greater than 0")
            txtQty.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtRate.Text) <= 0 Then
            msg_Error("Rate is greater than 0")
            txtRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        Validate_AddToGrid = True
    End Function

    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus
        If Val(Me.txtPackQty.Text) = 0 Then
            txtPackQty.Text = 1
            txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
        Else
            txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
        End If
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        If cmbUnit.SelectedIndex = 0 Then
            txtPackQty.Text = 1
            txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
        Else
            txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
        End If
    End Sub

    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRate.LostFocus
        If Val(Me.txtPackQty.Text) = 0 Then
            txtPackQty.Text = 1
            txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
        Else
            txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
        End If

    End Sub

    Private Sub txtRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate.TextChanged

    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        ''get the qty in case of pack unit
        If Me.cmbUnit.Text = "Loose" Then
            txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
            txtPackQty.Text = 1
            Me.txtPackQty.Enabled = False
            Me.txtPackQty.TabStop = False
        Else
            Me.txtPackQty.Enabled = True
            Me.txtPackQty.TabStop = True
            '    Dim objCommand As New OleDbCommand
            '    Dim objCon As OleDbConnection
            '    Dim objDataAdapter As New OleDbDataAdapter
            '    Dim objDataSet As New DataSet

            '    objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

            '    If objCon.State = ConnectionState.Open Then objCon.Close()

            '    objCon.Open()
            '    objCommand.Connection = objCon
            '    objCommand.CommandType = CommandType.Text


            '    objCommand.CommandText = "Select PackQty from ArticleDefTable where ArticleID = " & cmbItem.ActiveRow.Cells(0).Value

            '    txtPackQty.Text = objCommand.ExecuteScalar()
            Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
            txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
        End If
        If flgLoadAllItems = True Then
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                r.BeginEdit()
                r.Cells("Unit").Value = Me.cmbUnit.Text.ToString
                r.EndEdit()
            Next
        End If

    End Sub

    Private Sub AddItemToGrid()

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
        If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
        Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
        Dim dr As DataRow
        dr = dt.NewRow
        If Me.cmbCategory.SelectedValue Is Nothing Then
            dr(enmGridDetail.LocationId) = 0
        Else
            dr(enmGridDetail.LocationId) = Me.cmbCategory.SelectedValue
        End If
        'dr(enmGridDetail.Item) = Me.cmbItem.Text

        'Dim ConfigValue As String = getConfigValueByType("CostSheetType")
        'If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then
        '    dr(enmGridDetail.Item) = Me.cmbItem.SelectedRow.Cells("MasterArticleDescription").Value.ToString
        '    dr(enmGridDetail.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
        '    dr(enmGridDetail.UnitName) = Me.cmbItem.SelectedRow.Cells("Unit").Value.ToString
        '    dr(enmGridDetail.ArticleAliasName) = Me.txtArticleAliasName.Text
        '    dr(enmGridDetail.Qty) = Val(Me.txtQty.Text)
        '    dr(enmGridDetail.Rate) = Val(Me.txtRate.Text)
        '    dr(enmGridDetail.Total) = Val(Me.txtTotal.Text)
        '    'dr(enmGridDetail.ItemId) = Me.cmbItem.Value
        '    dr(enmGridDetail.ItemId) = Val(Me.cmbItem.SelectedRow.Cells("MasterID").Value.ToString)
        '    dr(enmGridDetail.PackQty) = Val(Me.txtPackQty.Text)
        '    'dr(enmGridDetail.CurrentPrice) = Val(Me.cmbItem.SelectedRow.Cells("Price").Text)
        '    dr(enmGridDetail.CurrentPrice) = Val(Me.cmbItem.SelectedRow.Cells("MasterPurchasePrice").Value.ToString)
        '    dr(enmGridDetail.Pack_Desc) = Me.cmbUnit.Text.ToString
        '    dr(enmGridDetail.PlanDetailId) = 0
        '    dt.Rows.InsertAt(dr, 0)
        'Else
        dr(enmGridDetail.Item) = Me.cmbItem.SelectedRow.Cells("Item").Value.ToString

        dr(enmGridDetail.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
        dr(enmGridDetail.UnitName) = Me.cmbItem.SelectedRow.Cells("Unit").Value.ToString
        dr(enmGridDetail.ArticleAliasName) = Me.txtArticleAliasName.Text
        dr(enmGridDetail.Qty) = Val(Me.txtQty.Text)
        dr(enmGridDetail.Rate) = Val(Me.txtRate.Text)
        dr(enmGridDetail.Total) = Val(Me.txtTotal.Text)
        'dr(enmGridDetail.ItemId) = Me.cmbItem.Value
        dr(enmGridDetail.ItemId) = Val(Me.cmbItem.SelectedRow.Cells("Id").Value.ToString)
        dr(enmGridDetail.PackQty) = Val(Me.txtPackQty.Text)
        'dr(enmGridDetail.CurrentPrice) = Val(Me.cmbItem.SelectedRow.Cells("Price").Text)
        dr(enmGridDetail.CurrentPrice) = Val(Me.cmbItem.SelectedRow.Cells("Price").Value.ToString)
        dr(enmGridDetail.Pack_Desc) = Me.cmbUnit.Text.ToString
        dr(enmGridDetail.PlanDetailId) = 0
        dt.Rows.InsertAt(dr, 0)
        'End If
    End Sub

    'Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged
    '    'If cmbCategory.SelectedIndex > 0 Then
    '    '    FillCombo("ItemFilter")
    '    'End If
    'End Sub

    Private Sub GetTotal()
        'Dim i As Integer
        'Dim dblTotalAmount As Double
        'Dim dblTotalQty As Double
        'For i = 0 To grd.Rows.Count - 1
        '    dblTotalAmount = dblTotalAmount + Val(grd.Rows(i).Cells(6).Value)
        '    dblTotalQty = dblTotalQty + Val(grd.Rows(i).Cells(4).Value)

        'Next
        'txtAmount.Text = dblTotalAmount
        'txtTotalQty.Text = dblTotalQty
        'txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)

        'Me.lblRecordCount.Text = "Total Records: " & Me.grd.RowCount


    End Sub

    Private Sub FillCombo(ByVal strCondition As String)
        Dim str As String

        If strCondition = "Item" Then
            '' Commented below lines to get item from ArticleDefMasterTable.
            'str = "SELECT     ArticleId as Id, ArticleDescription Item, ArticleCode Code, ArticleSizeName as Size, ArticleColorName as Combination,ISNULL(PurchasePrice,0) as Price FROM ArticleDefView  where Active=1"
            str = "SELECT ArticleDefView.ArticleId as Id, ArticleDefView.ArticleCode Code, ArticleDefView.ArticleDescription Item, ArticleDefView.MasterID, ArticleDefTableMaster.ArticleDescription As MasterArticleDescription, ArticleDefTableMaster.ArticleCode As MasterArticleCode, IsNull(ArticleDefTableMaster.PurchasePrice, 0) As MasterPurchasePrice, ArticleDefTableMaster.ArticleCode As MasterArticleCode, ArticleDefView.ArticleSizeName as Size, ArticleDefView.ArticleUnitName As Unit, ArticleDefView.ArticleColorName as Combination, ISNULL(ArticleDefView.PurchasePrice,0) as Price FROM ArticleDefView INNER JOIN ArticleDefTableMaster ON ArticleDefView.MasterID = ArticleDefTableMaster.ArticleId  Where ArticleDefView.Active=1"
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
            'str = "SELECT ArticleId as Id, ArticleDescription Item, ArticleCode Code, ISNULL(PurchasePrice,0) as Price FROM ArticleDefTableMaster where Active=1"

            FillUltraDropDown(Me.cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
        ElseIf strCondition = "Category" Then
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Code from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order"
            'Else
            '    str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            'End If

            'FillDropDown(cmbCategory, str, False)

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                     & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                     & " Else " _
                     & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"


            FillDropDown(cmbCategory, str, False)

        ElseIf strCondition = "ItemFilter" Then
            str = "SELECT ArticleId as Id, ArticleDescription Item, ArticleCode Code, ArticleSizeName as Size, ArticleColorName as Combination,ISNULL(PurchasePrice,0) as Price FROM         ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue
            FillUltraDropDown(cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
        ElseIf strCondition = "Customer" Then
            'str = "SELECT     tblVendor.AccountId AS ID, tblVendor.VendorName AS Name, tblListTerritory.TerritoryName AS Territory, tblListCity.CityName AS City,  " & _
            '        "tblListState.StateName AS State, tblVendor.AccountId AS AcId " & _
            '        "FROM         tblListTerritory INNER JOIN " & _
            '        "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
            '        "tblListState ON tblListCity.StateId = tblListState.StateId INNER JOIN " & _
            '        "tblVendor ON tblListTerritory.TerritoryId = tblVendor.Territory"

            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,  vwCOAdetail.sub_sub_title as [Ac Head], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                "dbo.tblListTerritory.TerritoryName as Territory " & _
                                "FROM         dbo.tblCustomer INNER JOIN " & _
                                "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                "WHERE     (dbo.vwCOADetail.account_type = 'Customer') order by tblCustomer.Sortorder, vwCOADetail.detail_title "

            FillUltraDropDown(cmbVendor, str)
            cmbVendor.Rows(0).Activate()
            If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
        ElseIf strCondition = "SO" Then
            str = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo from SalesOrderMasterTable where SalesOrderMasterTable.VendorId=" & Me.cmbVendor.Value & " And SalesOrderId In(Select SalesOrderId From SalesOrderDetailTable Group By SalesOrderId Having SUM(IsNull(Sz1,0)-ISNULL(PlanedQty,0)) <> 0)"
            FillDropDown(cmbPo, str)

        ElseIf strCondition = "SOComplete" Then
            str = "Select SalesID, SalesNo from SalesMasterTable "
            FillDropDown(cmbPo, str)
        ElseIf strCondition = "SM" Then
            str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee"
            FillDropDown(Me.cmbSalesMan, str)
        ElseIf strCondition = "Location" Then
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Code from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")  order by sort_order"
            'Else
            '    str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            'End If

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                     & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                     & " Else " _
                     & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns(0).ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
        ElseIf strCondition = "ArticlePack" Then
            Me.cmbUnit.ValueMember = "ArticlePackId"
            Me.cmbUnit.DisplayMember = "PackName"
            Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
        ElseIf strCondition = "TicketProduct" Then
            str = "SELECT     ArticleId as Id, ArticleDescription Item, ArticleCode Code, ArticleSizeName as Size, ArticleColorName as Combination,ISNULL(PurchasePrice,0) as Price FROM ArticleDefView where Active=1"
            FillUltraDropDown(Me.cmbTicketProduct, str)
            Me.cmbTicketProduct.Rows(0).Activate()
        End If
    End Sub

    Private Sub txtPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaid.TextChanged
        txtBalance.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum).Text) - Val(txtPaid.Text)
    End Sub

    Private Function Save() As Boolean

        'If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
        '    Me.txtPONo.Text = GetSerialNo("PN" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "PlanMasterTable", "PlanNo")
        'Else
        '    Me.txtPONo.Text = GetNextDocNo("PN", 6, "PlanMasterTable", "PlanNo")
        'End If
        Me.grd.UpdateData()
        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
            Me.txtPONo.Text = GetSerialNo("PN" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "PlanMasterTable", "PlanNo")
        ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
            Me.txtPONo.Text = GetNextDocNo("PN" & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "PlanMasterTable", "PlanNo")
        Else
            Me.txtPONo.Text = GetNextDocNo("PN", 6, "PlanMasterTable", "PlanNo")
        End If


        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer

        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'Before against request no. 934
            'objCommand.CommandText = "Insert into PlanMasterTable (LocationId, PlanNo,PlanDate,CustomerID,PlanQty,PlanAmount, Remarks,UserName,Status) values( " _
            '                        & gobjLocationId & ", '" & txtPONo.Text & "','" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Qty), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",'" & txtRemarks.Text & "','" & LoginUserName & "', '" & EnumStatus.Open.ToString & "')"
            'ReqId-934 Resolve Comma Error
            'Before againt task:M20
            'objCommand.CommandText = "Insert into PlanMasterTable (LocationId, PlanNo,PlanDate,CustomerID,PlanQty,PlanAmount, Remarks,UserName,Status) values( " _
            '                        & gobjLocationId & ", '" & txtPONo.Text & "','" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Qty), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",'" & txtRemarks.Text.Replace("'", "''") & "','" & LoginUserName & "', '" & EnumStatus.Open.ToString & "')"
            'Task:M20 Added Column PoId And EmployeeCode
            ''TASK TFS1629 Added new column of CompletionDate on 24-10-2017
            ''TASK TFS2181 done by Muhammad Ameen. Addition of new field of Start Date and made Sales order in detail grid as combo
            objCommand.CommandText = "Insert into PlanMasterTable (LocationId, PlanNo,PlanDate,CustomerID,PlanQty,PlanAmount, Remarks,UserName,Status, PoId,EmployeeCode, CompletionDate, StartDate) values( " _
                                   & gobjLocationId & ", '" & txtPONo.Text & "','" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Qty), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",'" & txtRemarks.Text.Replace("'", "''") & "','" & LoginUserName & "', '" & EnumStatus.Open.ToString & "', " & IIf(Me.cmbPo.SelectedValue Is Nothing, 0, Me.cmbPo.SelectedValue) & ", " & IIf(Me.cmbSalesMan.SelectedValue Is Nothing, 0, Me.cmbSalesMan.SelectedValue) & ", " & IIf(Me.dtpCompletionDate.Checked = True, "N'" & Me.dtpCompletionDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'", "NULL") & ", " & IIf(Me.dtpStartDate.Checked = True, "N'" & Me.dtpStartDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'", "NULL") & ")Select @@Identity"
            'End Task:M20

            Dim intPlanId As Integer = objCommand.ExecuteScalar()

            objCommand.CommandText = ""

            For i = 0 To grd.RowCount - 1
                'objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into PlanDetailTable (PlanId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice) values( " _
                '                        & " ident_current('PlanMasterTable')," & Val(grd.Rows(i).Cells(8).Value) & ",'" & (grd.Rows(i).Cells(3).Value) & "'," & Val(grd.Rows(i).Cells(4).Value) & ", " _
                '                        & " " & IIf(grd.Rows(i).Cells(3).Value = "Loose", Val(grd.Rows(i).Cells(4).Value), (Val(grd.Rows(i).Cells(4).Value) * Val(grd.Rows(i).Cells(9).Value))) & ", " & Val(grd.Rows(i).Cells(5).Value) & ", " & Val(grd.Rows(i).Cells(9).Value) & " , " & Val(grd.Rows(i).Cells(10).Value) & " ) "

                'objCommand.ExecuteNonQuery()
                'Val(grd.Rows(i).Cells(5).Value)



                objCommand.CommandText = ""
                objCommand.CommandText = "Insert into PlanDetailTable (PlanId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, Pack_Desc,SODetailId, SOId, PLevelId,Comments, ArticleAliasName) values( " _
                                        & " ident_current('PlanMasterTable'), " & Val(grd.GetRows(i).Cells(enmGridDetail.LocationId).Value) & ", " & Val(grd.GetRows(i).Cells(enmGridDetail.ItemId).Value) & ",'" & (grd.GetRows(i).Cells(enmGridDetail.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(enmGridDetail.Qty).Value) & ", " _
                                        & " " & Val(grd.GetRows(i).Cells(enmGridDetail.Qty).Value) & ", " & Val(grd.GetRows(i).Cells(enmGridDetail.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(enmGridDetail.PackQty).Value) & " , " & Val(grd.GetRows(i).Cells(enmGridDetail.CurrentPrice).Value) & ", '" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "'," & Val(grd.GetRows(i).Cells("SODetailId").Value.ToString) & "," & Val(grd.GetRows(i).Cells("SOId").Value.ToString) & "," & Val(grd.GetRows(i).Cells("PLevelID").Value.ToString) & ",N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("ArticleAliasName").Value.ToString.Replace("'", "''") & "')Select @@Identity "
                Dim obj As Object = objCommand.ExecuteNonQuery()

                If FlgStoreIssuanceAgainstPlan = True Then
                    'SavePlanCostSheet(obj, grd.GetRows(i).Cells(enmGridDetail.ItemId).Value, objCommand, trans)
                    Dim strSQL As String = String.Empty
                    strSQL = "SELECT tblCostSheet.ArticleID, (Isnull(tblCostSheet.Qty,0)*isnull(abc.Qty,0)) as Qty, MasterArticleID  FROM dbo.tblCostSheet LEFT OUTER JOIN (SELECT  dbo.PlanDetailTable.ArticleDefId, dbo.ArticleDefView.MasterID, SUM(dbo.PlanDetailTable.Qty) AS Qty  FROM  dbo.PlanDetailTable INNER JOIN  dbo.ArticleDefView ON dbo.PlanDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE dbo.ArticleDefView.ArticleId=" & grd.GetRows(i).Cells(enmGridDetail.ItemId).Value & "  AND PlanDetailTable.PlanId=ident_current('PlanMasterTable') GROUP BY dbo.PlanDetailTable.ArticleDefId, dbo.ArticleDefView.MasterID) abc on abc.MasterId = tblCostSheet.MasterArticleId INNER JOIN ArticleDefView on ArticleDefView.MasterID = tblCostSheet.MasterArticleId WHERE ArticleDefView.ArticleId=" & grd.GetRows(i).Cells(enmGridDetail.ItemId).Value
                    Dim dt As New DataTable
                    dt = GetDataTable(strSQL, trans)
                    If dt IsNot Nothing Then
                        For Each r As DataRow In dt.Rows
                            strSQL = String.Empty
                            strSQL = "INSERT INTO PlanCostSheetDetailTable(PlanId, ArticleDefId, ArticleSize, Price, PlanQty, IssuedQty, CurrentPrice, PackQty, LocationId, ArticleMasterId) Values(ident_current('PlanMasterTable'), " & r.Item("ArticleId") & ", '" & grd.GetRows(i).Cells(enmGridDetail.Unit).Value.ToString & "', " & grd.GetRows(i).Cells(enmGridDetail.Rate).Value & ",  " & r.Item("Qty") & ",0, " & grd.GetRows(i).Cells(enmGridDetail.CurrentPrice).Value & ", " & grd.GetRows(i).Cells(enmGridDetail.PackQty).Value & ",  " & grd.GetRows(i).Cells(enmGridDetail.LocationId).Value & ",   " & Val(r.Item("MasterArticleID").ToString) & ")"
                            objCommand.CommandText = ""
                            objCommand.CommandType = CommandType.Text
                            objCommand.CommandText = strSQL
                            objCommand.ExecuteNonQuery()
                        Next
                    End If
                End If
            Next

            'Task:2832 Update PlanedQty in SalesOrderDetailTable
            If Me.cmbPo.SelectedIndex > 0 Then
                objCommand.CommandText = ""
                'objCommand.CommandText = "Update SalesOrderDetailTable Set PlanedQty=PlanedQty+SoQty From ( SELECT PL_M.PoId, PL_DT.LocationId, PL_DT.ArticleDefId, PL_DT.ArticleSize, PL_DT.Price, SUM(PL_DT.Sz1) AS SOQty FROM dbo.PlanDetailTable AS PL_DT INNER JOIN " _
                '                         & " dbo.PlanMasterTable AS PL_M ON PL_DT.PlanId = PL_M.PlanId WHERE PL_M.PoId=" & Me.cmbPo.SelectedValue & " AND PL_M.PlanId=" & intPlanId & "  GROUP BY PL_M.PoId, PL_DT.LocationId, PL_DT.ArticleDefId, PL_DT.ArticleSize, PL_DT.Price " _
                '                         & " ) Plan_DT,SalesOrderDetailTable  WHERE SalesOrderDetailTable.SalesOrderId = Plan_DT.PoId AND SalesOrderDetailTable.LocationId = Plan_DT.LocationId AND SalesOrderDetailTable.ArticleDefId = Plan_DT.ArticleDefId " _
                '                         & " AND SalesOrderDetailTable.ArticleSize = Plan_DT.ArticleSize AND SalesOrderDetailTable.Price = Plan_DT.Price AND SalesOrderDetailTable.SalesOrderId=" & Me.cmbPo.SelectedValue & " "
                objCommand.CommandText = "Update SalesOrderDetailTable Set PlanedQty=IsNull(PlanedQty,0)+IsNull(SoQty,0) From ( SELECT PL_DT.SOId,PL_DT.SODetailId,SUM(PL_DT.Sz1) AS SOQty FROM dbo.PlanDetailTable AS PL_DT INNER JOIN " _
                                   & " dbo.PlanMasterTable AS PL_M ON PL_DT.PlanId = PL_M.PlanId WHERE PL_DT.SOId=" & Me.cmbPo.SelectedValue & " AND PL_M.PlanId=" & Val(intPlanId) & "  GROUP BY PL_DT.SOId,PL_DT.SODetailId " _
                                   & " ) Plan_DT,SalesOrderDetailTable  WHERE SalesOrderDetailTable.SalesOrderId = Plan_DT.SOId AND SalesOrderDetailTable.SalesOrderDetailId = Plan_DT.SODetailId  " _
                                   & " AND SalesOrderDetailTable.SalesOrderId=" & Me.cmbPo.SelectedValue & ""
                objCommand.ExecuteNonQuery()
            End If
            'End Task:2832
            trans.Commit()
            Save = True
            'InsertVoucher()
        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try

        ''insert Activity Log
        SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Purchase, Me.txtPONo.Text.Trim, True)


    End Function
    Sub InsertVoucher()
        Try
            SaveVoucherEntry(GetVoucherTypeId("SV"), GetNextDocNo("SV", 6, "tblVoucher", "voucher_no"), Me.dtpPODate.Value, "", Nothing, Val(Me.cmbVendor.ActiveRow.Cells(0).Text.ToString), getConfigValueByType("SalesCreditAccount"), Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)), Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)), "Both", Me.Name, Me.txtPONo.Text, True)
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try
    End Sub

    Private Function FormValidate() As Boolean
        Try

            'If txtPONo.Text = "" Then
            '    msg_Error("Please enter PO No.")
            '    txtPONo.Focus() : FormValidate = False : Exit Function
            'End If
            'If cmbVendor.ActiveRow.Cells(0).Value <= 0 Then
            '    msg_Error("Please select Vendor")
            '    cmbVendor.Focus() : FormValidate = False : Exit Function
            'End If
            Me.grd.UpdateData()
            If Not Me.grd.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                cmbItem.Focus() : FormValidate = False : Exit Function
            End If
            If Me.grd.RowCount = 0 Then
                ShowErrorMessage("Record not in grid.")
                Me.grd.Focus()
            End If
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub EditRecord()
        Try
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

            IsEditMode = True
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If

            txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value.ToString
            dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            txtReceivingID.Text = grdSaved.CurrentRow.Cells("PlanId").Value
            'TODO. ----
            cmbVendor.Value = grdSaved.CurrentRow.Cells("CustomerID").Value
            txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
            ' txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""
            Me.cmbSalesMan.SelectedValue = Val(grdSaved.CurrentRow.Cells("EmployeeCode").Value.ToString)
            'Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value


            'Me.FillCombo("SOComplete") Comment against task:M20
            Dim chkPO As DataTable = CType(Me.cmbPo.DataSource, DataTable)
            Dim drChkPO() As DataRow
            drChkPO = chkPO.Select("SalesOrderId='" & Me.grdSaved.CurrentRow.Cells("PoId").Value.ToString & "'")
            If drChkPO.Length = 0 Then
                Dim dt As New DataTable
                dt.Columns.Add("SalesOrderID", GetType(System.Int32))
                dt.Columns.Add("SalesOrderNo", GetType(System.String))
                Dim dr As DataRow
                'Task:2832 Added 0 Index Row 
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) 'Me.grdSaved.CurrentRow.Cells("PoId").Value
                dr(1) = ".... Select Any Value ...." 'Me.grdSaved.CurrentRow.Cells("SalesOrderNo").Value
                dt.Rows.InsertAt(dr, 0)
                'End Task:2832
                dr = dt.NewRow
                dr(0) = Me.grdSaved.CurrentRow.Cells("PoId").Value
                dr(1) = Me.grdSaved.CurrentRow.Cells("SalesOrderNo").Value
                dt.Rows.InsertAt(dr, 1)
                Me.cmbPo.DataSource = dt
                Me.cmbPo.DisplayMember = "SalesOrderNo"
                Me.cmbPo.ValueMember = "SalesOrderId"
                dt = Nothing

            End If
            'End Task:M20
            Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value

            ''TASKT TFS1629
            If Not IsDBNull(Me.grdSaved.CurrentRow.Cells("CompletionDate").Value) Then
                Me.dtpCompletionDate.Value = CType(Me.grdSaved.CurrentRow.Cells("CompletionDate").Value, Date)
                Me.dtpCompletionDate.Checked = True
            Else
                Me.dtpCompletionDate.Checked = False
            End If
            ''END TASK TFS1629

            ''TASK TFS2181 done by Muhammad Ameen. Addition of new field of Start Date.
            If Not IsDBNull(Me.grdSaved.CurrentRow.Cells("StartDate").Value) Then
                Me.dtpStartDate.Value = CType(Me.grdSaved.CurrentRow.Cells("StartDate").Value, Date)
                Me.dtpStartDate.Checked = True
            Else
                Me.dtpStartDate.Checked = False
            End If
            Call DisplayDetail(grdSaved.CurrentRow.Cells("PlanId").Value)
            GetTotal()
            Me.BtnSave.Text = "&Update"

            Me.cmbPo.Enabled = False
            Me.GetSecurityRights()
            GetAllTickets(-1)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnDelete.Visible = True
            Me.BtnPrint.Visible = True
            Me.gbTicket.Enabled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub DisplayPODetail(ByVal ReceivingID As Integer)
    '    Dim str As String
    '    'Dim i As Integer

    '    str = "SELECT  Article_Group.ArticleGroupName AS Category, Recv_D.LocationId, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
    '          & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
    '          & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.Price as CurrentPrice FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
    '          & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
    '          & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
    '          & " Where Recv_D.SalesID =" & ReceivingID & ""

    '    Dim objCommand As New OleDbCommand
    '    Dim objCon As OleDbConnection
    '    Dim objDataAdapter As New OleDbDataAdapter
    '    Dim objDataSet As New DataSet

    '    objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

    '    If objCon.State = ConnectionState.Open Then objCon.Close()

    '    objCon.Open()
    '    objCommand.Connection = objCon
    '    objCommand.CommandType = CommandType.Text


    '    objCommand.CommandText = str

    '    objDataAdapter.SelectCommand = objCommand
    '    objDataAdapter.Fill(objDataSet)

    '    'grd.Rows.Clear()
    '    'For i = 0 To objDataSet.Tables(0).Rows.Count - 1
    '    '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9), objDataSet.Tables(0).Rows(i)(10))
    '    '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
    '    '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)
    '    'Next
    '    Me.grd.DataSource = objDataSet.Tables(0)
    '    Me.grd.AutoSizeColumns()

    'End Sub

    Private Sub DisplayDetail(ByVal ReceivingID As Integer, Optional ByVal Condition As String = "")
        Dim str As String = String.Empty
        'Dim i As Integer

        'str = "SELECT Article_Group.ArticleGroupName AS Category, Recv_D.LocationId, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
        '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
        '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice FROM dbo.PlanDetailTable Recv_D INNER JOIN " _
        '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
        '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
        '      & " Where Recv_D.PlanID =" & ReceivingID & ""
        'If Not Condition = "All" Then
        If Condition = String.Empty Then
            'str = "SELECT Recv_D.LocationId, Article.ArticleDescription AS Item, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Recv_D.Sz7 END AS Total, " _
            '      & " Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc FROM dbo.PlanDetailTable Recv_D INNER JOIN  " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
            '      & " Where Recv_D.PlanID =" & ReceivingID & ""
            'Task:M20 Load Sales Order
            ''//Commented following lines on 04-10-2016
            'str = "SELECT Recv_D.LocationId, Article.ArticleDescription AS Item, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            ' & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Recv_D.Sz7 END AS Total, " _
            ' & " Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,IsNull(Recv_D.ProducedQty,0) as ProducedQty,IsNull(Recv_D.PlanDetailId,0) as PlanDetailId, IsNull(Recv_D.SODetailId,0) as SODetailId, IsNull(Recv_D.SOId,0) as SOId, IsNull(Recv_D.PLevelId,0) as PLevelId,Recv_D.Comments FROM dbo.PlanDetailTable Recv_D INNER JOIN  " _
            ' & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
            ' & " Where Recv_D.PlanID =" & ReceivingID & ""

            str = "SELECT Recv_D.LocationId, Article.ArticleDescription AS Item, Recv_D.ArticleSize AS Unit, ArticleUnitDefTable.ArticleUnitName As [Unit Name], Recv_D.ArticleAliasName, Recv_D.Qty AS Qty, Recv_D.Price as Rate, " _
          & " Recv_D.Price * Recv_D.Qty AS Total, " _
          & " Recv_D.ArticleDefId as ItemId, Recv_D.Sz7 as PackQty, Recv_D.CurrentPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,IsNull(Recv_D.ProducedQty,0) as ProducedQty,IsNull(Recv_D.PlanDetailId,0) as PlanDetailId, IsNull(Recv_D.SODetailId,0) as SODetailId, IsNull(Recv_D.SOId,0) as SOId, IsNull(Recv_D.PLevelId,0) as PLevelId,Recv_D.Comments FROM dbo.PlanDetailTable Recv_D INNER JOIN  " _
          & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId  " _
          & " Where Recv_D.PlanID =" & ReceivingID & ""
        ElseIf Condition = "LoadOrder" Then
            'str = "SELECT Recv_D.LocationId, Article.ArticleDescription AS Item, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '                  & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Recv_D.Sz7 END AS Total, " _
            '                  & " Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN  " _
            '                  & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
            '                  & " Where Recv_D.SalesOrderId =" & ReceivingID & ""
            'End Task:M20
            'str = "SELECT Recv_D.LocationId, Article.ArticleDescription AS Item, Recv_D.ArticleSize AS Unit, (Isnull(Recv_D.Sz1,0)-IsNull(PlanedQty,0)) AS Qty, Recv_D.Price as Rate, " _
            '              & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Recv_D.Sz7 END AS Total, " _
            '              & " Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN  " _
            '              & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
            '              & " Where Recv_D.SalesOrderId =" & ReceivingID & " AND (Isnull(Recv_D.Sz1,0)-IsNull(PlanedQty,0)) > 0"
            ''// Commented below lines to replace item with master item. Done by Ameen on Ahmad sb's recommendation. Dated 04-10-20169
            'str = "SELECT Recv_D.LocationId, Article.ArticleDescription AS Item, Recv_D.ArticleSize AS Unit, (Isnull(Recv_D.Qty,0)-IsNull(PlanedQty,0)) AS Qty, Recv_D.Price as Rate, " _
            '             & " Recv_D.Qty * Recv_D.Price AS Total, " _
            '             & " Recv_D.ArticleDefId as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,0 as ProducedQty, 0 as  PlanDetailId,  IsNull(Recv_D.SalesOrderDetailId,0) as SODetailId, IsNull(Recv_D.SalesOrderId,0) as SOId, 0 as PLevelId, Recv_D.Comments FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN  " _
            '             & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
            '             & " Where Recv_D.SalesOrderId =" & ReceivingID & " AND (Isnull(Recv_D.Sz1,0)-IsNull(PlanedQty,0)) > 0"
            'Dim ConfigValue As String = getConfigValueByType("CostSheetType")
            'If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then
            '    str = "SELECT Recv_D.LocationId, ArticleDefTableMaster.ArticleDescription AS Item, Recv_D.ArticleSize AS Unit, ArticleUnitDefTable.ArticleUnitName As [Unit Name], Recv_D.ArticleAliasName, (Isnull(Recv_D.Qty,0)-IsNull(PlanedQty,0)) AS Qty, Recv_D.Price as Rate, " _
            '            & " Recv_D.Qty * Recv_D.Price AS Total, " _
            '            & " Article.MasterID as ItemId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,0 as ProducedQty, 0 as  PlanDetailId,  IsNull(Recv_D.SalesOrderDetailId,0) as SODetailId, IsNull(Recv_D.SalesOrderId,0) as SOId, 0 as PLevelId, Recv_D.Comments FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN  " _
            '            & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId INNER JOIN " _
            '            & " dbo.ArticleDefTableMaster ON Article.MasterID = dbo.ArticleDefTableMaster.ArticleId LEFT JOIN ArticleUnitDefTable ON dbo.ArticleDefTableMaster.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId " _
            '            & " Where Recv_D.SalesOrderId =" & ReceivingID & " AND (Isnull(Recv_D.Sz1,0)-IsNull(PlanedQty,0)) > 0"
            'Else
            str = "SELECT Recv_D.LocationId, Article.ArticleDescription AS Item, Recv_D.ArticleSize AS Unit, ArticleUnitDefTable.ArticleUnitName As [Unit Name], Recv_D.ArticleAliasName, (Isnull(Recv_D.Qty,0)-IsNull(PlanedQty,0)) AS Qty, Recv_D.Price as Rate, " _
                        & " Recv_D.Qty * Recv_D.Price AS Total, " _
                        & " Article.ArticleId as ItemId, Recv_D.Sz7 as PackQty, Recv_D.CurrentPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,0 as ProducedQty, 0 as  PlanDetailId,  IsNull(Recv_D.SalesOrderDetailId,0) as SODetailId, IsNull(Recv_D.SalesOrderId,0) as SOId, 0 as PLevelId, Recv_D.Comments FROM dbo.SalesOrderDetailTable Recv_D INNER JOIN  " _
                        & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId " _
                        & " LEFT JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId " _
                        & " Where Recv_D.SalesOrderId =" & ReceivingID & " AND (Isnull(Recv_D.Sz1,0)-IsNull(PlanedQty,0)) > 0"
            ' ''End If
        ElseIf Condition = "All" Then
            'str = "Select Isnull(Detail.LocationId,0) as LocationId, vwArt.ArticleDescription as Item, isnull(Detail.Unit,'Loose') as Unit, Isnull(Detail.Qty,0) as Qty, Isnull(vwArt.SalePrice,0) as Rate, Isnull(Detail.Total,0) as Total, vwArt.ArticleId as ItemId, Isnull(vwArt.PackQty,0) as PackQty, Isnull(vwArt.SalePrice,0) as CurrentPrice, Isnull(Detail.Pack_Desc,'Loose') as Pack_Desc From ArticleDefView vwArt " _
            '& " LEFT OUTER JOIN  " _
            '& " (SELECT Recv_D.LocationId, Article.ArticleDescription AS Item, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '      & " Recv_D.ArticleDefId as ItemId, Article.PackQty, Article.SalePrice as CurrentPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc FROM dbo.PlanDetailTable Recv_D INNER JOIN  " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
            '      & " Where Recv_D.PlanID =" & ReceivingID & ") Detail on Detail.ItemId = vwArt.ArticleId WHERE vwArt.SalesItem=1 AND vwArt.Active=1 ORDER BY vwArt.SortOrder ASC"
            ''// Commented below line on 04-10-2016
            '   str = "Select Isnull(Detail.LocationId,0) as LocationId, vwArt.ArticleDescription as Item, isnull(Detail.Unit,'Loose') as Unit, Isnull(Detail.Qty,0) as Qty, Isnull(vwArt.SalePrice,0) as Rate, Isnull(Detail.Total,0) as Total, vwArt.ArticleId as ItemId, Isnull(vwArt.PackQty,0) as PackQty, Isnull(vwArt.SalePrice,0) as CurrentPrice, Isnull(Detail.Pack_Desc,'Loose') as Pack_Desc, IsNull(Detail.ProducedQty,0) as ProducedQty, IsNull(Detail.PlanDetailID,0) as PlanDetailId, 0 as SODetailId, 0 as SOId, 0 as PLevelId, Detail.Comments From ArticleDefView vwArt " _
            '& " LEFT OUTER JOIN  " _
            '& " (SELECT Recv_D.LocationId, Article.ArticleDescription AS Item, Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
            '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
            '      & " Recv_D.ArticleDefId as ItemId, Article.PackQty, Article.SalePrice as CurrentPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,IsNull(Recv_D.ProducedQty,0) as ProducedQty,IsNull(Recv_D.PlanDetailId,0) as PlanDetailId, Recv_D.Comments FROM dbo.PlanDetailTable Recv_D INNER JOIN  " _
            '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId  " _
            '      & " Where Recv_D.PlanID =" & ReceivingID & ") Detail on Detail.ItemId = vwArt.ArticleId WHERE vwArt.SalesItem=1 AND vwArt.Active=1 ORDER BY vwArt.SortOrder ASC"

            str = "Select Isnull(Detail.LocationId,0) as LocationId, vwArt.ArticleDescription As Item, isnull(Detail.Unit,'Loose') as Unit, Detail.ArticleUnitName As [Unit Name], Detail.ArticleAliasName, Isnull(Detail.Qty,0) as Qty, Isnull(vwArt.SalePrice,0) as Rate, Isnull(Detail.Total,0) as Total, vwArt.ArticleId as ItemId, Isnull(vwArt.PackQty,0) as PackQty, Isnull(vwArt.SalePrice,0) as CurrentPrice, Isnull(Detail.Pack_Desc,'Loose') as Pack_Desc, IsNull(Detail.ProducedQty,0) as ProducedQty, IsNull(Detail.PlanDetailID,0) as PlanDetailId, 0 as SODetailId, IsNull(Detail.SOId, 0) AS SOId, 0 as PLevelId, Detail.Comments From ArticleDefView vwArt " _
        & " LEFT OUTER JOIN  " _
        & " (SELECT Recv_D.LocationId, Article.ArticleDescription AS Item, Recv_D.ArticleSize AS Unit, Recv_D.Qty AS Qty, Recv_D.Price as Rate, " _
              & " Recv_D.Qty * Recv_D.Price AS Total, " _
              & " Recv_D.ArticleDefId as ItemId, Article.PackQty, Article.SalePrice as CurrentPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc,IsNull(Recv_D.ProducedQty,0) as ProducedQty,IsNull(Recv_D.PlanDetailId,0) as PlanDetailId, Recv_D.Comments, Recv_D.ArticleAliasName, ArticleUnitDefTable.ArticleUnitName, IsNull(Recv_D.SOId, 0) AS SOId FROM dbo.PlanDetailTable Recv_D INNER JOIN  " _
              & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT JOIN ArticleUnitDefTable ON Article.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId " _
              & " Where Recv_D.PlanID =" & ReceivingID & ") Detail on Detail.ItemId = vwArt.ArticleId WHERE vwArt.SalesItem=1 AND vwArt.Active=1 ORDER BY vwArt.SortOrder ASC"

        End If
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

        'grd.Rows.Clear()
        'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

        '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9))

        '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
        '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

        'Next
        ''// Commented below line to eliminate unit condition as there is only required to multiply Qty with Rate. By Ameen On 04-10-2016
        'objDataSet.Tables(0).Columns("Total").Expression = "IIF(Unit='Loose', (Qty*Rate), ((PackQty*Qty)*Rate))"
        objDataSet.Tables(0).Columns("Total").Expression = "(Qty*Rate)"
        'If objDataSet.Tables(0).Rows.Count > 0 Then
        Me.grd.DataSource = objDataSet.Tables(0)
        'Else
        'Me.grd.DataSource = Nothing
        'End If

        Dim dtPLevel As New DataTable
        dtPLevel = GetDataTable("Select  * From tblDefPlanLevel Union Select 0 as PLevelId, 'N/A' as PLevelName ")
        dtPLevel.AcceptChanges()
        Me.grd.RootTable.Columns("PLevelId").ValueList.PopulateValueList(dtPLevel.DefaultView, "PLevelId", "PLevelName")
        ''TASK TFS2181 done by Muhammad Ameen.  made Sales order in detail grid as combo
        str = "Select SalesOrderId, SalesOrderNo from SalesOrderMasterTable Union Select 0 as SalesOrderId, 'N/A' as SalesOrderNo"
        Dim dtSO As DataTable = GetDataTable(str)
        dtSO.AcceptChanges()
        'Me.grd.RootTable.Columns("SOId").HasValueList = True
        'Me.grd.RootTable.Columns("SOId").EditType = Janus.Windows.GridEX.EditType.Combo
        Me.grd.RootTable.Columns("SOId").ValueList.PopulateValueList(dtSO.DefaultView, "SalesOrderId", "SalesOrderNo")



        Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("Unit").Index
        Me.grd.RootTable.Columns("Unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index

        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
            If col.Index <> enmGridDetail.Qty AndAlso col.Index <> enmGridDetail.Rate AndAlso col.Index <> enmGridDetail.LocationId AndAlso col.Index <> enmGridDetail.PLevelId AndAlso col.Index <> enmGridDetail.Comments AndAlso col.Index <> enmGridDetail.SOId Then
                col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
        Next

        If flgLoadAllItems = True Then
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                r.BeginEdit()
                r.Cells(enmGridDetail.LocationId).Value = Me.cmbCategory.SelectedValue
                r.EndEdit()
            Next
        End If

    End Sub

    Private Function Update_Record() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer

        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'Before against request no. 934
            'objCommand.CommandText = "Update PlanMasterTable set PlanNo ='" & txtPONo.Text & "',PlanDate='" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',CustomerID=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '& " PlanQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Qty), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",PlanAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", Remarks='" & txtRemarks.Text & "',UserName='" & LoginUserName & "', Status='" & EnumStatus.Open.ToString & "' Where PlanID= " & txtReceivingID.Text & " "
            'ReqId-934 Resolve Comma Error
            'Before against task:M20
            '  objCommand.CommandText = "Update PlanMasterTable set PlanNo ='" & txtPONo.Text & "',PlanDate='" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',CustomerID=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '& " PlanQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Qty), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",PlanAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", Remarks='" & txtRemarks.Text.Replace("'", "''") & "',UserName='" & LoginUserName & "', Status='" & EnumStatus.Open.ToString & "' Where PlanID= " & txtReceivingID.Text & " "

            'Task:M20 Added Column PoId And EmployeeCode


            'Task:2832 Update Previous PlanedQty in SalesOrderDetailTable
            If Me.cmbPo.SelectedIndex > 0 Then
                objCommand.CommandText = ""
                'objCommand.CommandText = "Update SalesOrderDetailTable Set PlanedQty=PlanedQty-SoQty From ( SELECT PL_M.PoId, PL_DT.LocationId, PL_DT.ArticleDefId, PL_DT.ArticleSize, PL_DT.Price, SUM(PL_DT.Sz1) AS SOQty FROM dbo.PlanDetailTable AS PL_DT INNER JOIN " _
                '                         & " dbo.PlanMasterTable AS PL_M ON PL_DT.PlanId = PL_M.PlanId WHERE PL_M.PoId=" & Me.cmbPo.SelectedValue & " AND PL_M.PlanId=" & Val(Me.txtReceivingID.Text) & "  GROUP BY PL_M.PoId, PL_DT.LocationId, PL_DT.ArticleDefId, PL_DT.ArticleSize, PL_DT.Price " _
                '                         & " ) Plan_DT,SalesOrderDetailTable  WHERE SalesOrderDetailTable.SalesOrderId = Plan_DT.PoId AND SalesOrderDetailTable.LocationId = Plan_DT.LocationId AND SalesOrderDetailTable.ArticleDefId = Plan_DT.ArticleDefId " _
                '                         & " AND SalesOrderDetailTable.ArticleSize = Plan_DT.ArticleSize AND SalesOrderDetailTable.Price = Plan_DT.Price AND SalesOrderDetailTable.SalesOrderId=" & Me.cmbPo.SelectedValue & " "
                objCommand.CommandText = "Update SalesOrderDetailTable Set PlanedQty=IsNull(PlanedQty,0)-IsNull(SoQty,0) From ( SELECT PL_DT.SOId,PL_DT.SODetailId,SUM(PL_DT.Sz1) AS SOQty FROM dbo.PlanDetailTable AS PL_DT INNER JOIN " _
                                   & " dbo.PlanMasterTable AS PL_M ON PL_DT.PlanId = PL_M.PlanId WHERE PL_DT.SOId=" & Me.cmbPo.SelectedValue & " AND PL_M.PlanId=" & Val(Me.txtReceivingID.Text) & "  GROUP BY PL_DT.SOId,PL_DT.SODetailId " _
                                   & " ) Plan_DT,SalesOrderDetailTable  WHERE SalesOrderDetailTable.SalesOrderId = Plan_DT.SOId AND SalesOrderDetailTable.SalesOrderDetailId = Plan_DT.SODetailId  " _
                                   & " AND SalesOrderDetailTable.SalesOrderId=" & Me.cmbPo.SelectedValue & ""
                objCommand.ExecuteNonQuery()




            End If
            'End Task:2832
            ''TASK TFS1629 Added new column of CompletionDate on 24-10-2017
            objCommand.CommandText = "Update PlanMasterTable set PlanNo ='" & txtPONo.Text & "',PlanDate='" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',CustomerID=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
                & " PlanQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Qty), Janus.Windows.GridEX.AggregateFunction.Sum)) & ",PlanAmount=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGridDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", Remarks='" & txtRemarks.Text.Replace("'", "''") & "',UserName='" & LoginUserName & "', Status='" & EnumStatus.Open.ToString & "', PoId=" & IIf(Me.cmbPo.SelectedValue Is Nothing, 0, Me.cmbPo.SelectedValue) & ", EmployeeCode=" & IIf(Me.cmbSalesMan.SelectedValue Is Nothing, 0, Me.cmbSalesMan.SelectedValue) & ", CompletionDate =" & IIf(Me.dtpCompletionDate.Checked = True, "N'" & Me.dtpCompletionDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'", "NULL") & " , StartDate =" & IIf(Me.dtpStartDate.Checked = True, "N'" & Me.dtpStartDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "'", "NULL") & " Where PlanID= " & txtReceivingID.Text & " "


            objCommand.ExecuteNonQuery()

            ''Commented on 27-07-2016 against PlanTickets
            'objCommand.CommandText = ""
            'objCommand.CommandText = "Delete from PlanDetailTable where PlanID = " & txtReceivingID.Text
            'objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""

            If FlgStoreIssuanceAgainstPlan = True Then
                objCommand.CommandText = ""
                objCommand.CommandText = "Delete From PlanCostSheetDetailTable WHERE PlanCostSheetDetailTable.PlanId=" & txtReceivingID.Text
                objCommand.ExecuteNonQuery()
            End If

            For i = 0 To grd.RowCount - 1
                'objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into PlanDetailTable (PlanId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice) values( " _
                '                        & " " & txtReceivingID.Text & " ," & Val(grd.GetRows(i).Cells(8).Value) & ",'" & (grd.GetRows(i).Cells(3).Value) & "'," & Val(grd.GetRows(i).Cells(4).Value) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells(3).Value = "Loose", Val(grd.GetRows(i).Cells(4).Value), (Val(grd.GetRows(i).Cells(4).Value) * Val(grd.GetRows(i).Cells(9).Value))) & ", " & Val(grd.GetRows(i).Cells(5).Value) & ", " & Val(grd.GetRows(i).Cells(9).Value) & "  , " & Val(grd.GetRows(i).Cells(10).Value) & ") "
                'objCommand.ExecuteNonQuery()
                ''Val(grd.Rows(i).Cells(5).Value)
                'If IsValidToDelete("PlanTickest", "PlanDetailId", Me.grdSaved.CurrentRow.Cells("PlanDetailId").Value.ToString) = True Then

                'End If


                objCommand.CommandText = ""
                objCommand.CommandText = "If Exists(Select IsNull(PlanDetailId, 0) As PlanDetailId From PlanDetailTable Where PlanDetailId = " & Val(grd.GetRows(i).Cells(enmGridDetail.PlanDetailId).Value) & ") Update PlanDetailTable Set PlanId = " & Val(txtReceivingID.Text) & ", LocationId =" & Val(grd.GetRows(i).Cells(enmGridDetail.LocationId).Value) & ", ArticleDefId =" & Val(grd.GetRows(i).Cells(enmGridDetail.ItemId).Value) & ", ArticleSize ='" & (grd.GetRows(i).Cells(enmGridDetail.Unit).Value) & "', Sz1=" & Val(grd.GetRows(i).Cells(enmGridDetail.Qty).Value) & ", " _
                                        & " Qty = " & IIf(grd.GetRows(i).Cells(enmGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(enmGridDetail.Qty).Value), (Val(grd.GetRows(i).Cells(enmGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(enmGridDetail.PackQty).Value))) & ", Price = " & Val(grd.GetRows(i).Cells(enmGridDetail.Rate).Value) & ",  Sz7 = " & Val(grd.GetRows(i).Cells(enmGridDetail.PackQty).Value) & " , CurrentPrice =" & Val(grd.GetRows(i).Cells(enmGridDetail.CurrentPrice).Value) & ", Pack_Desc='" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "', ProducedQty=" & Val(grd.GetRows(i).Cells(enmGridDetail.ProducedQty).Value.ToString) & ", SODetailId=" & Val(grd.GetRows(i).Cells("SODetailId").Value.ToString) & ", SOId=" & Val(grd.GetRows(i).Cells("SOId").Value.ToString) & ", PLevelId=" & Val(grd.GetRows(i).Cells("PLevelID").Value.ToString) & ", Comments =N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', ArticleAliasName =N'" & grd.GetRows(i).Cells("ArticleAliasName").Value.ToString.Replace("'", "''") & "' Where PlanDetailId =" & Val(grd.GetRows(i).Cells(enmGridDetail.PlanDetailId).Value) & " " _
                                        & " Else Insert into PlanDetailTable(PlanId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, Pack_Desc,ProducedQty,SODetailId, SOId, PLevelId,Comments, ArticleAliasName) values( " _
                                        & " " & Val(txtReceivingID.Text) & ", " & Val(grd.GetRows(i).Cells(enmGridDetail.LocationId).Value) & ", " & Val(grd.GetRows(i).Cells(enmGridDetail.ItemId).Value) & ",'" & (grd.GetRows(i).Cells(enmGridDetail.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(enmGridDetail.Qty).Value) & ", " _
                                        & " " & Val(grd.GetRows(i).Cells(enmGridDetail.Qty).Value) & ", " & Val(grd.GetRows(i).Cells(enmGridDetail.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(enmGridDetail.PackQty).Value) & " , " & Val(grd.GetRows(i).Cells(enmGridDetail.CurrentPrice).Value) & ",'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "'," & Val(grd.GetRows(i).Cells(enmGridDetail.ProducedQty).Value.ToString) & "," & Val(grd.GetRows(i).Cells("SODetailId").Value.ToString) & "," & Val(grd.GetRows(i).Cells("SOId").Value.ToString) & "," & Val(grd.GetRows(i).Cells("PLevelID").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("ArticleAliasName").Value.ToString.Replace("'", "''") & "' ) Select @@Identity "
                Dim obj As Object = objCommand.ExecuteScalar()
                Dim intPlanDetailId As Integer = 0
                If Not obj Is DBNull.Value Then
                    intPlanDetailId = Convert.ToInt32(obj)
                End If
                obj = DBNull.Value
                objCommand.CommandText = ""
                objCommand.CommandText = "Update ProductionDetailTable Set PlanDetailId=" & intPlanDetailId & " WHERE PlanDetailId=" & Val(grd.GetRows(i).Cells(enmGridDetail.PlanDetailId).Value) & ""
                objCommand.ExecuteNonQuery()

                ''Commented by Ameen agains PlanTickets Work On 27-07-2016
                'objCommand.CommandText = ""
                'objCommand.CommandText = "Insert into PlanDetailTable(PlanId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, Pack_Desc,ProducedQty,SODetailId, SOId, PLevelId,Comments) values( " _
                '                        & " " & Val(txtReceivingID.Text) & ", " & Val(grd.GetRows(i).Cells(enmGridDetail.LocationId).Value) & ", " & Val(grd.GetRows(i).Cells(enmGridDetail.ItemId).Value) & ",'" & (grd.GetRows(i).Cells(enmGridDetail.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(enmGridDetail.Qty).Value) & ", " _
                '                        & " " & IIf(grd.GetRows(i).Cells(enmGridDetail.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(enmGridDetail.Qty).Value), (Val(grd.GetRows(i).Cells(enmGridDetail.Qty).Value) * Val(grd.GetRows(i).Cells(enmGridDetail.PackQty).Value))) & ", " & Val(grd.GetRows(i).Cells(enmGridDetail.Rate).Value) & ", " & Val(grd.GetRows(i).Cells(enmGridDetail.PackQty).Value) & " , " & Val(grd.GetRows(i).Cells(enmGridDetail.CurrentPrice).Value) & ",'" & Me.grd.GetRows(i).Cells("Pack_Desc").Value.ToString.Replace("'", "''") & "'," & Val(grd.GetRows(i).Cells(enmGridDetail.ProducedQty).Value.ToString) & "," & Val(grd.GetRows(i).Cells("SODetailId").Value.ToString) & "," & Val(grd.GetRows(i).Cells("SOId").Value.ToString) & "," & Val(grd.GetRows(i).Cells("PLevelID").Value.ToString) & ", N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "' )Select @@Identity "
                'Dim intPlanDetailId As Integer = objCommand.ExecuteScalar()

                'objCommand.CommandText = ""
                'objCommand.CommandText = "Update ProductionDetailTable Set PlanDetailId=" & intPlanDetailId & " WHERE PlanDetailId=" & Val(grd.GetRows(i).Cells(enmGridDetail.PlanDetailId).Value) & ""
                'objCommand.ExecuteNonQuery()


                If FlgStoreIssuanceAgainstPlan = True Then
                    'SavePlanCostSheet(Val(txtReceivingID.Text), grd.GetRows(i).Cells(enmGridDetail.ItemId).Value, objCommand, trans)
                    Dim strSQL As String = String.Empty
                    strSQL = "SELECT tblCostSheet.ArticleID, (Isnull(tblCostSheet.Qty,0)*isnull(abc.Qty,0)) as Qty, MasterArticleID  FROM dbo.tblCostSheet LEFT OUTER JOIN (SELECT  dbo.PlanDetailTable.ArticleDefId, dbo.ArticleDefView.MasterID, SUM(dbo.PlanDetailTable.Qty) AS Qty  FROM  dbo.PlanDetailTable INNER JOIN  dbo.ArticleDefView ON dbo.PlanDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE dbo.ArticleDefView.ArticleId=" & grd.GetRows(i).Cells(enmGridDetail.ItemId).Value & "  AND PlanDetailTable.PlanId=" & Val(Me.txtReceivingID.Text) & " GROUP BY dbo.PlanDetailTable.ArticleDefId, dbo.ArticleDefView.MasterID) abc on abc.MasterId = tblCostSheet.MasterArticleId INNER JOIN ArticleDefView on ArticleDefView.MasterID = tblCostSheet.MasterArticleId WHERE ArticleDefView.ArticleId=" & grd.GetRows(i).Cells(enmGridDetail.ItemId).Value
                    Dim dt As New DataTable
                    dt = GetDataTable(strSQL, trans)
                    If dt IsNot Nothing Then
                        For Each r As DataRow In dt.Rows
                            objCommand.CommandText = ""
                            objCommand.CommandText = "INSERT INTO PlanCostSheetDetailTable(PlanId, ArticleDefId, ArticleSize, Price, PlanQty, IssuedQty, CurrentPrice, PackQty, LocationId, ArticleMasterId) Values(" & Val(Me.txtReceivingID.Text) & ", " & r.Item("ArticleId") & ", '" & grd.GetRows(i).Cells(enmGridDetail.Unit).Value.ToString & "', " & grd.GetRows(i).Cells(enmGridDetail.Rate).Value & ",  " & r.Item("Qty") & ",0, " & grd.GetRows(i).Cells(enmGridDetail.CurrentPrice).Value & ", " & grd.GetRows(i).Cells(enmGridDetail.PackQty).Value & ",  " & grd.GetRows(i).Cells(enmGridDetail.LocationId).Value & ",   " & Val(r.Item("MasterArticleID").ToString) & ")"
                            objCommand.ExecuteNonQuery()
                        Next
                    End If
                End If
            Next

            'Task:2832 Update PlanedQty in SalesOrderDetailTable
            If Me.cmbPo.SelectedIndex > 0 Then
                objCommand.CommandText = ""
                'objCommand.CommandText = "Update SalesOrderDetailTable Set PlanedQty=PlanedQty+SoQty From ( SELECT PL_M.PoId, PL_DT.LocationId, PL_DT.ArticleDefId, PL_DT.ArticleSize, PL_DT.Price, SUM(PL_DT.Sz1) AS SOQty FROM dbo.PlanDetailTable AS PL_DT INNER JOIN " _
                '                         & " dbo.PlanMasterTable AS PL_M ON PL_DT.PlanId = PL_M.PlanId WHERE PL_M.PoId=" & Me.cmbPo.SelectedValue & " AND PL_M.PlanId=" & Val(Me.txtReceivingID.Text) & "  GROUP BY PL_M.PoId, PL_DT.LocationId, PL_DT.ArticleDefId, PL_DT.ArticleSize, PL_DT.Price " _
                '                         & " ) Plan_DT,SalesOrderDetailTable  WHERE SalesOrderDetailTable.SalesOrderId = Plan_DT.PoId AND SalesOrderDetailTable.LocationId = Plan_DT.LocationId AND SalesOrderDetailTable.ArticleDefId = Plan_DT.ArticleDefId " _
                '                         & " AND SalesOrderDetailTable.ArticleSize = Plan_DT.ArticleSize AND SalesOrderDetailTable.Price = Plan_DT.Price AND SalesOrderDetailTable.SalesOrderId=" & Me.cmbPo.SelectedValue & " "
                objCommand.CommandText = "Update SalesOrderDetailTable Set PlanedQty=IsNull(PlanedQty,0)+IsNull(SoQty,0) From ( SELECT PL_DT.SOId,PL_DT.SODetailId,SUM(PL_DT.Sz1) AS SOQty FROM dbo.PlanDetailTable AS PL_DT INNER JOIN " _
                                        & " dbo.PlanMasterTable AS PL_M ON PL_DT.PlanId = PL_M.PlanId WHERE PL_DT.SOId=" & Me.cmbPo.SelectedValue & " AND PL_M.PlanId=" & Val(Me.txtReceivingID.Text) & "  GROUP BY PL_DT.SOId,PL_DT.SODetailId " _
                                        & " ) Plan_DT,SalesOrderDetailTable  WHERE SalesOrderDetailTable.SalesOrderId = Plan_DT.SOId AND SalesOrderDetailTable.SalesOrderDetailId = Plan_DT.SODetailId  " _
                                        & " AND SalesOrderDetailTable.SalesOrderId=" & Me.cmbPo.SelectedValue & ""
                objCommand.ExecuteNonQuery()

            End If
            'End Task:2832

            trans.Commit()
            Update_Record = True
            'InsertVoucher()
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try

        ''insert Activity Log
        SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Purchase, Me.txtPONo.Text.Trim, True)




    End Function
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14 
        Me.Cursor = Cursors.WaitCursor
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
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
                    'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14 
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() Then
                        'msg_Information(str_informSave)
                        RefreshControls()
                        ClearDetailControls()
                        'grd.Rows.Clear()
                        'DisplayRecord()
                    Else
                        Exit Sub 'MsgBox("Record has not been added")
                    End If
                Else
                    If IsValidToDelete("DispatchMasterTable", "PlanId", Me.grdSaved.CurrentRow.Cells("PlanId").Value.ToString) = True Then
                        If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                        If Update_Record() Then
                            'msg_Information(str_informUpdate)
                            RefreshControls()
                            ClearDetailControls()
                            'grd.Rows.Clear()
                            'DisplayRecord()
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
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        EditRecord()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub grdSaved_CellDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
        '    Me.cmbVendor.Value = dt.Rows(0).Item("VendorCode")
        '    Me.cmbVendor.Enabled = False

        'Else
        '    Me.cmbVendor.Enabled = True
        '    Me.cmbVendor.Rows(0).Activate()
        'End If
        'Task:M20 Load Sales Order
        Try
            If IsOpenForm = True Then
                If Not Me.cmbPo.SelectedIndex = -1 AndAlso Not Me.cmbPo.SelectedIndex = 0 Then
                    DisplayDetail(Me.cmbPo.SelectedValue, "LoadOrder")
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        'End Task:M20
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

    Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        If Me.cmbItem.IsItemInList = False Then Exit Sub
        'Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
        Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("MasterPurchasePrice").Value.ToString
        Dim ID As Integer = Me.cmbItem.Value
        If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
        'Me.cmbVendor.DisplayLayout.Grid.Show( me.cmbVendor.contr)

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

        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
        If IsDateLock(Me.dtpPODate.Value) = True Then
            ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
        End If
        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        For i As Integer = 0 To Me.grd.RowCount - 1
            If IsValidToDelete("PlanTickets", "PlanDetailId", Me.grd.GetRows(i).Cells("PlanDetailId").Value.ToString) = False Then
                msg_Error("Dependent tickets were found against item (" & Me.grd.GetRows(i).Cells("Item").Value.ToString & "). Please delete them first.")
                Exit Sub
            End If
        Next

        If IsValidToDelete("DispatchMasterTable", "PlanId", Me.grdSaved.CurrentRow.Cells("PlanId").Value.ToString) = True Then
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Me.Cursor = Cursors.WaitCursor
            Try
                Dim cm As New OleDbCommand
                Dim objTrans As OleDbTransaction
                If Con.State = ConnectionState.Closed Then Con.Open()
                objTrans = Con.BeginTransaction
                cm.Connection = Con
                cm.Transaction = objTrans


                'Task:2832 Update  PlanedQty in SalesOrderDetailTable
                If Me.cmbPo.SelectedIndex > 0 Then
                    cm.CommandText = ""
                    'cm.CommandText = "Update SalesOrderDetailTable Set PlanedQty=PlanedQty-SoQty From ( SELECT PL_M.PoId, PL_DT.LocationId, PL_DT.ArticleDefId, PL_DT.ArticleSize, PL_DT.Price, SUM(PL_DT.Sz1) AS SOQty FROM dbo.PlanDetailTable AS PL_DT INNER JOIN " _
                    '                         & " dbo.PlanMasterTable AS PL_M ON PL_DT.PlanId = PL_M.PlanId WHERE PL_M.PoId=" & Me.grdSaved.CurrentRow.Cells("PoId").Value.ToString & " AND PL_M.PlanId=" & Me.grdSaved.CurrentRow.Cells("PlanId").Value.ToString & "  GROUP BY PL_M.PoId, PL_DT.LocationId, PL_DT.ArticleDefId, PL_DT.ArticleSize, PL_DT.Price " _
                    '                         & " ) Plan_DT,SalesOrderDetailTable  WHERE SalesOrderDetailTable.SalesOrderId = Plan_DT.PoId AND SalesOrderDetailTable.LocationId = Plan_DT.LocationId AND SalesOrderDetailTable.ArticleDefId = Plan_DT.ArticleDefId " _
                    '                         & " AND SalesOrderDetailTable.ArticleSize = Plan_DT.ArticleSize AND SalesOrderDetailTable.Price = Plan_DT.Price AND SalesOrderDetailTable.SalesOrderId=" & Me.grdSaved.CurrentRow.Cells("POId").Value.ToString & " "

                    cm.CommandText = "Update SalesOrderDetailTable Set PlanedQty=IsNull(PlanedQty,0)-IsNull(SoQty,0) From ( SELECT PL_DT.SOId,PL_DT.SODetailId,SUM(PL_DT.Sz1) AS SOQty FROM dbo.PlanDetailTable AS PL_DT INNER JOIN " _
                                      & " dbo.PlanMasterTable AS PL_M ON PL_DT.PlanId = PL_M.PlanId WHERE PL_DT.SOId=" & Val(Me.grdSaved.CurrentRow.Cells("PoId").Value.ToString) & " AND PL_M.PlanId=" & Val(Me.grdSaved.CurrentRow.Cells("PlanId").Value.ToString) & "  GROUP BY PL_DT.SOId,PL_DT.SODetailId " _
                                      & " ) Plan_DT,SalesOrderDetailTable  WHERE SalesOrderDetailTable.SalesOrderId = Plan_DT.SOId AND SalesOrderDetailTable.SalesOrderDetailId = Plan_DT.SODetailId  " _
                                      & " AND SalesOrderDetailTable.SalesOrderId=" & Val(Me.grdSaved.CurrentRow.Cells("PoId").Value.ToString) & ""
                    cm.ExecuteNonQuery()
                End If
                'End Task:2832
                cm.CommandText = ""
                cm.CommandText = "delete from PlanDetailTable where Planid=" & Me.grdSaved.CurrentRow.Cells(5).Value.ToString
                cm.ExecuteNonQuery()

                cm.CommandText = ""
                cm.CommandText = "delete from PlanMasterTable where Planid=" & Me.grdSaved.CurrentRow.Cells(5).Value.ToString
                cm.ExecuteNonQuery()

                objTrans.Commit()


                'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14 
                'msg_Information(str_informDelete)
                Me.txtReceivingID.Text = 0

                ''insert Activity Log
                SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Purchase, grdSaved.CurrentRow.Cells(0).Value.ToString, True)

                'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
                Me.grdSaved.CurrentRow.Delete()


                Me.RefreshControls()

            Catch ex As Exception
                msg_Error("Error occured while deleting record: " & ex.Message)

            Finally
                Con.Close()
                Me.Cursor = Cursors.Default
            End Try

        Else
            msg_Error(str_ErrorDependentRecordFound)
        End If

    End Sub
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Me.Cursor = Cursors.WaitCursor
        ShowReport("Plan", "{SP_Plan;1.PlanId}=" & grdSaved.CurrentRow.Cells("PlanId").Value)
        Me.Cursor = Cursors.Default
        'edit by haseeb 

        'If Me.grdSaved.RowCount = 0 Then Exit Sub
        'AddRptParam("@PlanID", Me.grdSaved.CurrentRow.Cells("PlanId").Value)
        'ShowReport("SP_Plan_Items")

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
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmCustomerPlanning)
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
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As SBModel.GroupRights In Rights
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
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub BtnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoadAll.Click
        Me.Cursor = Cursors.WaitCursor
        Me.DisplayRecord("All")
        Me.DisplayDetail(-1)
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click

        Me.Cursor = Cursors.WaitCursor
        'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14 
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        'If Not msg_Confirm(str_ConfirmRefresh) Then Exit Sub
        Dim id As Integer = 0

        id = Me.cmbVendor.SelectedRow.Cells(0).Value
        FillCombo("Customer")
        Me.cmbVendor.Value = id

        id = Me.cmbCategory.SelectedValue
        FillCombo("Category")
        Me.cmbCategory.SelectedValue = id

        id = Me.cmbItem.SelectedRow.Cells(0).Value
        FillCombo("Item")
        Me.cmbItem.Value = id

        If Not getConfigValueByType("StoreIssuaneDependonProductionPlan").ToString = "Error" Then
            FlgStoreIssuanceAgainstPlan = getConfigValueByType("StoreIssuaneDependonProductionPlan")
        End If
        If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
            flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
        End If
        If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
            flgLoadAllItems = getConfigValueByType("LoadAllItemsInSales")
        End If
        If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            flgCompanyRights = getConfigValueByType("CompanyRights")
        End If

        Me.Cursor = Cursors.Default
        Me.lblProgress.Visible = False
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 0 Then
                Me.BtnLoadAll.Visible = False
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                If IsEditMode = False Then Me.BtnPrint.Visible = False
                If IsEditMode = False Then Me.BtnDelete.Visible = False
                Me.BtnPrint.Visible = False
            Else
                Me.BtnLoadAll.Visible = True
                DisplayRecord()
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                Me.BtnPrint.Visible = True
                Me.BtnDelete.Visible = True
                Me.BtnEdit.Visible = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbVendor_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.ValueChanged
        Try

            If IsOpenForm = False Then Exit Sub
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbVendor.IsItemInList = False Then Exit Sub

            'Dim str As String = String.Empty
            'If Me.IsEditMode = False Then
            '    'str = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(Varchar(12), SalesOrderDate, 113) As SalesOrderNo from SalesOrderMasterTable where  status = '" & EnumStatus.Open.ToString & "' and VendorID = " & Me.cmbVendor.ActiveRow.Cells(0).Value & " and Status = '" & EnumStatus.Open.ToString & "'"
            '    str = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(Varchar(12), SalesOrderDate, 113) As SalesOrderNo from SalesOrderMasterTable where  status = '" & EnumStatus.Open.ToString & "' " & IIf(Me.cmbVendor.ActiveRow.Cells(0).Value > 0, "  and VendorID = " & Me.cmbVendor.ActiveRow.Cells(0).Value & "", "")
            '    FillDropDown(cmbPo, str)
            '    'Me.txtFuel.Text = Val(Me.cmbVendor.ActiveRow.Cells(Customer.Fuel).Text)
            '    'Me.txtExpense.Text = Val(Me.cmbVendor.ActiveRow.Cells(Customer.Other_Exp).Text)
            'Else
            'str = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo from SalesOrderMasterTable where VendorID = " & Me.cmbVendor.Value & " AND SalesOrderId In(Select SalesOrderId From SalesOrderDetailTable Group By SalesOrderId Having SUM(IsNull(Sz1,0)-ISNULL(PlanedQty,0)) <> 0)"
            'FillDropDown(cmbPo, str)
            'End If
            If Me.cmbVendor.Value > 0 Then
                FillCombo("SO")
            End If

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

    Public Function SavePlanCostSheet(ByVal PlanId As Integer, ByVal ArticleDefId As Integer, ByVal cmd As OleDbCommand, ByVal trans As OleDb.OleDbTransaction) As Boolean
        Try

            Dim strSQL As String = String.Empty
            strSQL = "SELECT tblCostSheet.ArticleID, (Isnull(tblCostSheet.Qty,0)*isnull(abc.Qty,0)) as Qty, MasterArticleID  FROM dbo.tblCostSheet LEFT OUTER JOIN (SELECT  dbo.PlanDetailTable.ArticleDefId, dbo.ArticleDefView.MasterID, SUM(dbo.PlanDetailTable.Qty) AS Qty  FROM  dbo.PlanDetailTable INNER JOIN  dbo.ArticleDefView ON dbo.PlanDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE dbo.ArticleDefView.ArticleId=" & ArticleDefId & "  AND PlanDetailTable.PlanId=" & PlanId & "  GROUP BY dbo.PlanDetailTable.ArticleDefId, dbo.ArticleDefView.MasterID) abc on abc.MasterId = tblCostSheet.MasterArticleId INNER JOIN ArticleDefView on ArticleDefView.MasterID = tblCostSheet.MasterArticleId WHERE ArticleDefView.ArticleId=" & ArticleDefId
            Dim dt As New DataTable
            dt = GetDataTable(strSQL, trans)
            If dt IsNot Nothing Then
                For Each r As DataRow In dt.Rows

                    strSQL = String.Empty
                    strSQL = "INSERT INTO PlanCostSheetDetailTable(PlanId, ArticleDefId, ArticleSize, Price, PlanQty, IssuedQty, CurrentPrice, PackQty, LocationId, ArticleMasterId) Values(" & PlanId & ", " & r.Item("ArticleId") & ", " & r.Item("Qty") & ",0, " & Val(r.Item("MasterArticleID").ToString) & ")"
                    cmd.CommandText = ""
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = strSQL
                    cmd.Transaction = trans
                    cmd.Connection = trans.Connection
                    cmd.ExecuteNonQuery()

                Next
            End If

            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                If Val(Me.grd.GetRow.Cells("ProducedQty").Value.ToString) = 0 Then
                    DeleteDetailRow(Val(grd.GetRow.Cells(enmGridDetail.PlanDetailId).Value.ToString))
                    Me.grd.GetRow.Delete()
                    Me.grd.UpdateData()
                Else
                    msg_Error("You can't delete this record. because it dependent record exists.")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged
        Try
            If IsOpenForm = True Then
                If flgLoadAllItems = True Then
                    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                        r.BeginEdit()
                        r.Cells(enmGridDetail.LocationId).Value = Me.cmbCategory.SelectedValue
                        r.EndEdit()
                    Next
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.BtnEdit, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(BtnDelete, Nothing)
            Exit Sub
        End If

    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 24-1-14
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

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdSaved.GetRow Is Nothing AndAlso grdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmCustomerPlanning"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdSaved.CurrentRow.Cells(1).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Production planning (" & frmtask.Ref_No & ") "
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
            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Inventory
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles lblQty.Click, lblQty.Click

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtTicketNo.TextChanged

    End Sub

    Private Sub txtRemarks_TextChanged(sender As Object, e As EventArgs) Handles txtRemarks.TextChanged

    End Sub

    Private Sub grd_Click(sender As Object, e As EventArgs) Handles grd.Click
        'Try
        '    PlanDetailId = Me.grd.GetRow.Cells(enmGridDetail.PlanDetailId).Value
        '    PlanId = grdSaved.CurrentRow.Cells("PlanId").Value
        '    Me.cmbTicketProduct.Value = Me.grd.GetRow().Cells(enmGridDetail.ItemId).Value
        '    AvailableQty = Val(Me.grd.GetRow.Cells(enmGridDetail.Qty).Value.ToString)
        '    If PlanDetailId = 0 Then
        '        Me.txtAvailableQty.Text = AvailableQty
        '        Me.GetAllTickets(-1)
        '    Else
        '        Me.txtAvailableQty.Text = AvailableQty - RemainingQty(PlanDetailId)
        '        Me.GetAllTickets(PlanDetailId)
        '    End If
        '    Me.txtTicketNo.Text = GetNextTicket(Me.txtPONo.Text)

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Private Function CreateTicketNo() As String
        Dim ticketNo As String = String.Empty
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                ticketNo = GetSerialNo("TK" + "-" + Microsoft.VisualBasic.Right(Me.dtpTicketDate.Value.Year, 2) + "-", "PlanTickets", "TicketNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                ticketNo = GetNextDocNo("TK" & "-" & Format(Me.dtpTicketDate.Value, "yy") & Me.dtpTicketDate.Value.Month.ToString("00"), 4, "PlanTickets", "TicketNo")
            Else
                ticketNo = GetNextDocNo("TK", 6, "PlanTickets", "TicketNo")
            End If
            Return ticketNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetNextTicket(ByVal PlanNo As String) As String
        Try


            Dim str As String = 0
            'Dim strSql As String = "select  +'" & Prefix & "-'+  replicate('0',(" & Length & " - len(replace(isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ,10))),0)+1,6,0)))) + replace(isnull(max(convert(integer,substring(" & tableName & "." & FieldName & "," & Prefix.Length + 2 & ",10))),0)+1,6,0) from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
            Dim strSql As String
            'select substring(@str, 3, charindex('\', @str, 3) - 3)
            strSql = "select IsNull(Max(Convert(Integer, Right(TicketNo, CHARINDEX('-', REVERSE('-' + TicketNo)) - 1))), 0) from PlanTickets  Where PlanId = " & PlanId & "" ' "
            'Else
            '    strSql = "select  isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ," & Val(Prefix.Length + Length + 1) & "))),0)+1 from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
            Dim dt As New DataTable
            Dim adp As New OleDbDataAdapter
            adp = New OleDbDataAdapter(strSql, New OleDbConnection(Con.ConnectionString))
            adp.Fill(dt)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 AndAlso Not dt.Rows(0).Item(0).ToString = "0" Then
                str = dt.Rows(0).Item(0).ToString
                str = PlanNo & "-" & str + 1
            Else
                str = PlanNo & "-" & 1
            End If
            Return str
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Private Function ValidateTicket() As Boolean
        Try
            If cmbTicketProduct.Value <= 0 Then
                msg_Error("Please select an item")
                Me.cmbTicketProduct.Focus() : ValidateTicket = False : Exit Function
            End If

            If Val(txtTicketQty.Text) <= 0 Then
                msg_Error("Quantity is not greater than 0")
                Me.txtTicketQty.Focus() : ValidateTicket = False : Exit Function
            End If

            If Val(txtTicketQty.Text) > Val(txtAvailableQty.Text) Then
                msg_Error("Quantity is greater than Available")
                Me.txtTicketQty.Focus() : ValidateTicket = False : Exit Function
            End If
            ValidateTicket = True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnAddTicket_Click(sender As Object, e As EventArgs) Handles btnAddTicket.Click
        Try

            If ValidateTicket() = True Then
                SaveTicket()
                GetAllTickets(PlanDetailId)
                Me.txtTicketNo.Text = GetNextTicket(Me.txtPONo.Text)
                Me.txtAvailableQty.Text = AvailableQty - RemainingQty(PlanDetailId)
                Me.txtTicketQty.Text = String.Empty
                'Me.txtTicketNo.Text = CreateTicketNo()
                '    If Me.cmbTicketProduct.ActiveRow Is Nothing Then Exit Sub
                '    Dim dt As DataTable = CType(Me.grdTicket.DataSource, DataTable)
                '    Dim dr As DataRow
                '    dr = dt.NewRow

                '    dt.Rows.InsertAt(dr, 0)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SaveTicket()
        Dim cmd As New OleDbCommand
        Dim conn As OleDbConnection
        conn = Con
        If conn.State = ConnectionState.Open Then conn.Close()
        conn.Open()
        Dim trans As OleDbTransaction = conn.BeginTransaction
        Try
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            cmd.Transaction = trans
            cmd.CommandText = "Insert into PlanTickets(TicketNo, PlanId, PlanDetailId, ArticleId, ProductionStartDate, TicketQuantity) Values(N'" & Me.txtTicketNo.Text & "', " & PlanId & ", " & PlanDetailId & ", " & Me.cmbTicketProduct.Value & ", N'" & Me.dtpTicketDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & Val(Me.txtTicketQty.Text) & " )"
            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try

    End Sub
    Private Sub UpdateTicketQty(ByVal planTicketsId As Integer, ByVal quantity As Double)
        Dim cmd As New OleDbCommand
        Dim conn As OleDbConnection
        conn = Con
        If conn.State = ConnectionState.Open Then conn.Close()
        conn.Open()
        Dim trans As OleDbTransaction = conn.BeginTransaction
        Try
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            cmd.Transaction = trans
            cmd.CommandText = "Update PlanTickets Set TicketQuantity = " & quantity & " Where PlanTicketsId =" & planTicketsId & ""
            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try

    End Sub
    Private Sub GetAllTickets(ByVal planDetailId As Integer)
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Try
            str = "Select PT.PlanTicketsId, PT.TicketNo, PT.PlanDetailId, PT.ArticleId, Article.ArticleDescription As Product, PT.ProductionStartDate, PT.TicketQuantity From PlanTickets PT Left Outer Join ArticleDefTable Article On PT.ArticleId = Article.ArticleId Where PT.PlanDetailId = " & planDetailId & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.grdTicket.DataSource = dt
            Me.grdTicket.RootTable.Columns("ProductionStartDate").FormatString = str_DisplayDateFormat
            'Me.grdTicket.RootTable.Columns("ProductionStartDate").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdTicket.RootTable.Columns("Product").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function RemainingQty(ByVal planDetailId As Integer) As Double
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Try
            str = "Select PlanDetailId, Sum(IsNull(TicketQuantity, 0)) As TicketQuantity From PlanTickets Where PlanDetailId = " & planDetailId & " Group by PlanDetailId"
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return dt.Rows.Item(0).Item(1)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub DeleteTicket(ByVal PlanTicketsId As Integer)
        Dim cmd As New OleDbCommand
        Dim conn As OleDbConnection
        conn = Con
        If conn.State = ConnectionState.Open Then conn.Close()
        conn.Open()
        Dim trans As OleDbTransaction = conn.BeginTransaction
        Try
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            cmd.Transaction = trans
            cmd.CommandText = "Delete From PlanTickets Where PlanTicketsId = " & PlanTicketsId & ""
            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try

    End Sub
    Private Sub DeleteDetailRow(ByVal PlanDetailId As Integer)
        Dim cmd As New OleDbCommand
        Dim conn As OleDbConnection
        conn = Con
        If conn.State = ConnectionState.Open Then conn.Close()
        conn.Open()
        Dim trans As OleDbTransaction = conn.BeginTransaction
        Try
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            cmd.Transaction = trans
            cmd.CommandText = "Delete From PlanDetailTable Where PlanDetailId = " & PlanDetailId & ""
            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try

    End Sub
    Private Sub DeleteTickets(ByVal PlanDetailId As Integer)
        Dim cmd As New OleDbCommand
        Dim conn As OleDbConnection
        conn = Con
        If conn.State = ConnectionState.Open Then conn.Close()
        Dim trans As OleDbTransaction = conn.BeginTransaction
        Try
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            cmd.Transaction = trans
            cmd.CommandText = "Delete From PlanTickets Where PlanDetailId = " & PlanDetailId & ""
            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try

    End Sub

    Private Sub grdTicket_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdTicket.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.DeleteTicket(Val(Me.grdTicket.GetRow.Cells("PlanTicketsId").Value.ToString))
                Me.txtAvailableQty.Text = AvailableQty - RemainingQty(PlanDetailId)
                Me.grdTicket.GetRow.Delete()
                Me.grdTicket.UpdateData()
                Dim grdSumAfterUpate As Double = grdTicket.GetTotal(grdTicket.RootTable.Columns("TicketQuantity"), Janus.Windows.GridEX.AggregateFunction.Sum)
                Me.txtAvailableQty.Text = AvailableQty - grdSumAfterUpate
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdTicket_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdTicket.CellEdited
        Try
            Dim calculatedQty As Double = 0D
            If Me.grdTicket.GetRow.Cells("TicketQuantity").DataChanged = True Then
                Dim grdSumBeforeUpdate As Double = grdTicket.GetTotal(grdTicket.RootTable.Columns("TicketQuantity"), Janus.Windows.GridEX.AggregateFunction.Sum)
                Me.grdTicket.UpdateData()
                Dim grdSumAfterUpate As Double = grdTicket.GetTotal(grdTicket.RootTable.Columns("TicketQuantity"), Janus.Windows.GridEX.AggregateFunction.Sum)
                Dim remainQty As Double = RemainingQty(Val(Me.grdTicket.GetRow.Cells("PlanDetailId").Value.ToString))
                If grdSumAfterUpate > grdSumBeforeUpdate Then
                    calculatedQty = grdSumAfterUpate - grdSumBeforeUpdate
                End If
                If grdSumAfterUpate > AvailableQty Then
                    msg_Error("Entered quantity is larger than available quantity")
                    GetAllTickets(PlanDetailId)
                    Exit Sub
                Else
                    Me.UpdateTicketQty(Val(Me.grdTicket.GetRow.Cells("PlanTicketsId").Value.ToString), Val(Me.grdTicket.GetRow.Cells("TicketQuantity").Value.ToString))
                    GetAllTickets(PlanDetailId)
                    Me.txtAvailableQty.Text = AvailableQty - grdSumAfterUpate
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    'Private Function 

    Private Sub rdoCode_CheckedChanged(sender As Object, e As EventArgs) Handles rdoCode.CheckedChanged
        Try
            If Me.rdoCode.Checked = True Then
                If Not cmbItem.ActiveRow Is Nothing Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoName_CheckedChanged(sender As Object, e As EventArgs) Handles rdoName.CheckedChanged
        Try
            If Me.rdoName.Checked = True Then
                If Not cmbItem.ActiveRow Is Nothing Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
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
            frmMapArticleAliasOnSalesOrder.GetSalesOrderDetailId = Val(Me.grd.GetRow.Cells("PlanDetailId").Value.ToString)
            frmMapArticleAliasOnSalesOrder.GetArtileId = Val(Me.grd.GetRow.Cells("ItemId").Value.ToString)
            frmMapArticleAliasOnSalesOrder.GetArticleAliasName = Me.grd.GetRow.Cells("ArticleAliasName").Value.ToString
            If frmMapArticleAliasOnSalesOrder.ShowDialog = Windows.Forms.DialogResult.Yes Then
                Me.grd.GetRow.BeginEdit()
                Me.grd.GetRow.Cells("ItemId").Value = frmMapArticleAliasOnSalesOrder.SetArticleDefId
                'Me.grd.GetRow.Cells("ArticleCode").Value = frmMapArticleAliasOnSalesOrder.SetArticleCode
                Me.grd.GetRow.Cells("Item").Value = frmMapArticleAliasOnSalesOrder.SetArticleDescription
                Me.grd.GetRow.Cells("ArticleAliasName").Value = frmMapArticleAliasOnSalesOrder.GetArticleAliasName
                Me.grd.GetRow.EndEdit()
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_ColumnHeaderClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnHeaderClick
        Try
            If e.Column.Key = "ArticleAliasName" Then
                MapArticleAlias()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
