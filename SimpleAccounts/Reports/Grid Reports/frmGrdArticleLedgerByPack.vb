''27-Dec-2013 R:M-2   Imran Ali             Rounding Figure
''29-Dec-2013 Task:2359  Imran Ali  software Problem to Mr.Aziz
''11-Feb-2014     TASKM18 Imran Ali Sorting Out
''11-Jun-2015 Task# 3-11-06-2015 Ahmad Sharif: Fill items combox ,Add search by code and by name
''22-6-2015 TASKM226151 Imran Ali Runnting Total Problem Fixed
Imports SBModel
Imports System.Data
Public Class frmGrdArticleLedgerByPack
    Dim IsOpenedForm As Boolean = False

    Private _ItemList As New List(Of SBModel.ArticleList)

    Private Sub frmGrdArticleLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdArticleLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            'FillGridEx(Me.grdArticles, "Select ArticleID, ArticleCode, ArticleDescription, ArticeColorName as Color, ArticleSizeName as Size From ArticleDefView WHERE Active=1", True)
            Me.cmbPeriod.Text = "Current Month"
            '_ItemList = GetItemList()
            'Me.lstItems.ListItem.DataSource = _ItemList
            'Me.lstItems.ListItem.DisplayMember = "ArticleDescription"
            'Me.lstItems.ListItem.ValueMember = "ArticleId"
            'FillDropDown(Me.ComboBox1, "Select ArticleId, ArticleDescription From ArticleDefView WHERE Active=1")

            ''Task# 3-11-06-2015 fill combo box of itmes with ArticleId, ArticleDescription and ArticleCode
            FillUltraDropDown(Me.cmbItems, "Select ArticleId, ArticleDescription,ArticleCode From ArticleDefView WHERE Active=1") ''Task# 3-11-06-2015 fill cmbItems combo box 
            Me.cmbItems.Rows(0).Activate()      'active first row of combo box of items

            Me.cmbItems.DisplayLayout.Bands(0).Columns("ArticleId").Hidden = True       'Hide ArticleId in combo box of items
            ''End Task# 3-11-06-2015

            FillDropDown(Me.cmbLocation, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order")

            IsOpenedForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub grdArticles_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try




        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFrom.Value = Date.Today
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-1)
                Me.dtpTo.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpTo.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Public Function SearchItemList(ByVal Article As SBModel.ArticleList) As Boolean
    '    Try
    '        If Article.ArticleDescription.StartsWith(Me.txtItemDesc.Text) Then
    '            Return True
    '        Else
    '            Return False
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    'Private Sub txtItemDesc_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtItemDesc.KeyDown

    'End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            'Me.lstItems.ListItem.DataSource = GetItemList()
            'Me.lstItems.ListItem.DisplayMember = "ArticleDescription"
            'Me.lstItems.ListItem.ValueMember = "ArticleId"
            '_ItemList = GetItemList()

            Dim id As Integer = 0I
            'id = Me.ComboBox1.SelectedIndex
            'FillDropDown(Me.ComboBox1, "Select ArticleId, ArticleDescription From ArticleDefView WHERE Active=1")
            'Me.ComboBox1.SelectedIndex = id

            ''Task# 3-11-06-2015 Add Fill cmbItems combox
            Dim idi As Integer = 0I
            idi = Me.cmbItems.Value
            'FillUltraDropDown(Me.cmbItems, "Select ArticleId, ArticleDescription,ArticleCode From ArticleDefView WHERE Active=1")
            FillUltraDropDown(Me.cmbItems, "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination,  ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand] FROM ArticleDefView where Active=1")
            Me.cmbItems.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            Me.cmbItems.Value = idi
            ''End Task# 3-11-06-2015

            id = Me.cmbLocation.SelectedIndex
            FillDropDown(Me.cmbLocation, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order")
            Me.cmbLocation.SelectedIndex = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub txtItemDesc_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Try
    '        If Me.txtItemDesc.Text = String.Empty Then

    '            Me.lstItems.ListItem.DataSource = _ItemList
    '            Me.lstItems.ListItem.DisplayMember = "ArticleDescription"
    '            Me.lstItems.ListItem.ValueMember = "ArticleId"
    '        Else
    '            Dim Article_List As List(Of SBModel.ArticleList) = _ItemList.FindAll(AddressOf SearchItemList)
    '            Me.lstItems.ListItem.DataSource = Article_List
    '            Me.lstItems.ListItem.DisplayMember = "ArticleDescription"
    '            Me.lstItems.ListItem.ValueMember = "ArticleId"
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try
            'If Me.ComboBox1.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select any item.")
            '    Me.ComboBox1.Focus()
            '    Exit Sub
            'End If
            Dim str As String
            str = "select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ""
            Dim dtlocation As DataTable
            dtlocation = GetDataTable(str)
            If IsDBNull(dtlocation.Rows(0).Item("Location_Id")) = True Then
                ShowErrorMessage("You Don't have rights to any location, Please Contact to the Authorized Person")
                Exit Sub
            End If
            ''Task# 3-11-06-2015 check if active row less than zero, then display error message to select item
            If Me.cmbItems.ActiveRow.Cells(0).Value < 0 Then
                ShowErrorMessage("Please select any item")
                Me.cmbItems.Focus()
                Exit Sub
            End If
            ''End Task# 3-11-06-2015

            Dim strQuery As String = String.Empty
            'strQuery = "SP_ItemLedger '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', " & Me.ComboBox1.SelectedValue & ""
            'strQuery = "SP_ItemLedger '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', " & Me.cmbItems.Value & ""          '''Task# 3-11-06-2015 Pass parameters to SP_ItemLedger


            'strQuery = "Select DISTINCT Detail.Location_Name as Location, Detail.SalesNo, Detail.SalesDate, Detail.Party,vwCOADetail.Detail_Title as [Party Name], ArticleId as ArticleDefId,ArticleCode, ArticleDescription, ArticleColorName, ArticleSizeName, Detail.Unit,isnull(Detail.PackQty,0) as Pack,  Round(Isnull(Detail.Price,0),3) as Price, isnull(Detail.Out_Qty,0) as Out_Qty, Isnull(Detail.Out_Price,0) as Out_Price, Isnull(Detail.In_Qty,0) as In_Qty, Isnull(Detail.In_Price,0) as In_Price, Isnull(opening.Opening_Qty,0) as OpeningQty, Isnull(opening.Opening_Amount,0)  as OpeningAmount, Detail.LocationId, Isnull(Detail.StockTransDetailId,0) as StockTransDetailId From ArticleDefView " _
            '        & " LEFT OUTER JOIN ( " _
            '        & " Select DISTINCT abc.SalesNo, abc.SalesDate,abc.Party, abc.ArticleDefId,abc.Pack_Desc as Unit, abc.PackQty, Price, abc.Out_Qty, abc.Out_Price, abc.In_Qty, abc.In_Price, abc.LocationId, Loc.Location_Name, abc.StockTransDetailId " _
            '        & " From ( " _
            '        & " SELECT     dbo.ReceivingMasterTable.ReceivingNo as SalesNo, dbo.ReceivingMasterTable.ReceivingDate as SalesDate, dbo.ReceivingDetailTable.ArticleDefId, StockDt.Rate as Price,0 AS Out_Qty,  " _
            '        & "                       0 AS Out_Price, dbo.ReceivingDetailTable.Qty AS In_Qty, StockDt.Rate AS In_Price,ReceivingDetailTable.LocationId, ReceivingMasterTable.VendorId as Party, Isnull(ReceivingDetailTable.Pack_Desc,ReceivingDetailTable.ArticleSize) as Pack_Desc, Isnull(ReceivingDetailTable.Sz1,0) as PackQty, StockDt.StockTransDetailId " _
            '        & " FROM         dbo.ReceivingMasterTable INNER JOIN " _
            '        & "   dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId " _
            '        & "                      INNER JOIN StockMasterTable Stock on Stock.DocNo = ReceivingMasterTable.ReceivingNo " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = ReceivingDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId AND StockDt.InQty = ReceivingDetailTable.Qty " _
            '        & " WHERE (Convert(Varchar,ReceivingMasterTable.ReceivingDate,102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND ReceivingDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND ReceivingDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & "  " _
            '        & " Union All " _
            '        & " SELECT     dbo.SalesReturnMasterTable.SalesReturnNo, dbo.SalesReturnMasterTable.SalesReturnDate, dbo.SalesReturnDetailTable.ArticleDefId, StockDt.Rate as Price, 0 AS Out_Qty,  " _
            '        & "                       0 AS Out_Price, dbo.SalesReturnDetailTable.Qty AS In_Qty, StockDt.Rate AS In_Price, SalesReturnDetailTable.LocationId, SalesReturnMasterTable.CustomerCode as Party, Isnull(SalesReturnDetailTable.Pack_Desc,SalesReturnDetailTable.ArticleSize) as Pack_Desc, Isnull(SalesReturnDetailTable.Sz1,0) as PackQty , StockDt.StockTransDetailId " _
            '        & " FROM         dbo.SalesReturnMasterTable INNER JOIN " _
            '        & "                       dbo.SalesReturnDetailTable ON dbo.SalesReturnMasterTable.SalesReturnId = dbo.SalesReturnDetailTable.SalesReturnId " _
            '        & "                                             INNER JOIN StockMasterTable Stock on Stock.DocNo = SalesReturnMasterTable.SalesReturnNo " _
            '        & "                       INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = SalesReturnDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId AND StockDt.inQty = SalesReturnDetailTable.Qty " _
            '        & " WHERE (Convert(Varchar,SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND SalesReturnDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND SalesReturnDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT   Distinct  dbo.SalesMasterTable.SalesNo, dbo.SalesMasterTable.SalesDate, dbo.SalesDetailTable.ArticleDefId, StockDt.Rate as Price,dbo.SalesDetailTable.Qty AS Out_Qty,  " _
            '        & "                      StockDt.Rate AS Out_Price, 0 AS In_Qty, 0 AS In_Price, SalesDetailTable.LocationId, SalesMasterTable.CustomerCode as Party, Isnull(SalesDetailTable.Pack_Desc,SalesDetailTable.ArticleSize) as Pack_Desc, Isnull(SalesDetailTable.Sz1,0) as PackQty, StockDt.StockTransDetailId " _
            '        & " FROM         dbo.SalesMasterTable INNER JOIN " _
            '        & "                       dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId " _
            '        & "                       INNER JOIN StockMasterTable Stock on Stock.DocNo = SalesMasterTable.SalesNo " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = SalesDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId AND StockDt.OutQty = SalesDetailTable.Qty " _
            '        & " WHERE (Convert(Varchar,SalesMasterTable.SalesDate,102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND SalesDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND SalesDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT     dbo.ProductionMasterTable.Production_No, dbo.ProductionMasterTable.Production_Date, dbo.ProductionDetailTable.ArticleDefId, StockDt.Rate as Price, 0 AS Out_Qty,  " _
            '        & "                       0 AS Out_Price, dbo.ProductionDetailTable.Qty AS In_Qty, StockDt.Rate AS In_Price, ProductionDetailTable.Location_Id, 0 as Party, Isnull(ProductionDetailTable.Pack_Desc,ProductionDetailTable.ArticleSize) as Pack_Desc, Isnull(ProductionDetailTable.Sz1,0) as PackQty, StockDt.StockTransDetailId " _
            '        & " FROM         dbo.ProductionMasterTable INNER JOIN " _
            '        & "                      dbo.ProductionDetailTable ON dbo.ProductionMasterTable.Production_Id = dbo.ProductionDetailTable.Production_Id " _
            '        & "                                             INNER JOIN StockMasterTable Stock on Stock.DocNo = ProductionMasterTable.Production_No " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = ProductionDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId and StockDt.InQty = ProductionDetailTable.Qty " _
            '        & " WHERE (Convert(Varchar,ProductionMasterTable.Production_Date,102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND ProductionDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND ProductionDetailTable.Location_Id=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT     dbo.PurchaseReturnMasterTable.PurchaseReturnNo, dbo.PurchaseReturnMasterTable.PurchaseReturnDate, dbo.PurchaseReturnDetailTable.ArticleDefId, StockDt.Rate as Price, dbo.PurchaseReturnDetailTable.Qty AS Out_Qty,  " _
            '        & "                       StockDt.Rate AS Out_Price, 0 AS In_Qty, 0 AS In_Price, PurchaseReturnDetailTable.LocationId, PurchaseReturnMasterTable.VendorId as Party,Isnull(PurchaseReturnDetailTable.Pack_Desc,PurchaseReturnDetailTable.ArticleSize) as Pack_Desc, Isnull(PurchaseReturnDetailTable.Sz1,0) as PackQty, StockDt.StockTransDetailId " _
            '        & " FROM         dbo.PurchaseReturnMasterTable INNER JOIN " _
            '        & "                       dbo.PurchaseReturnDetailTable ON dbo.PurchaseReturnMasterTable.PurchaseReturnId = dbo.PurchaseReturnDetailTable.PurchaseReturnId " _
            '        & "                                                                   INNER JOIN StockMasterTable Stock on Stock.DocNo = PurchaseReturnMasterTable.PurchaseReturnNo " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = PurchaseReturnDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId and StockDt.OutQty = PurchaseReturnDetailTable.Qty " _
            '        & " WHERE (Convert(Varchar,PurchaseReturnMasterTable.PurchaseReturnDate,102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND PurchaseReturnDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND PurchaseReturnDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT     dbo.DispatchMasterTable.DispatchNo, dbo.DispatchMasterTable.DispatchDate, dbo.DispatchDetailTable.ArticleDefId, StockDt.Rate as Price,dbo.DispatchDetailTable.Qty AS Out_Qty,  " _
            '        & "                       StockDt.Rate AS Out_Price, 0 AS In_Qty, 0 AS In_Price, DispatchDetailTable.LocationId, 0 as Party, Isnull(DispatchDetailTable.Pack_Desc,DispatchDetailTable.ArticleSize) as Pack_Desc, Isnull(DispatchDetailTable.Sz1,0) as PackQty, StockDt.StockTransDetailId " _
            '        & " FROM         dbo.DispatchMasterTable INNER JOIN " _
            '        & "                      dbo.DispatchDetailTable ON dbo.DispatchMasterTable.DispatchId = dbo.DispatchDetailTable.DispatchId " _
            '        & "                                                                                         INNER JOIN StockMasterTable Stock on Stock.DocNo = DispatchMasterTable.DispatchNo " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = DispatchDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId and StockDt.OutQty = DispatchDetailTable.Qty " _
            '        & " WHERE (Convert(Varchar,DispatchMasterTable.DispatchDate,102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND DispatchDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND DispatchDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT     dbo.StockAdjustmentMaster.Doc_no, dbo.StockAdjustmentMaster.Doc_Date, dbo.StockAdjustmentDetail.Artical_id, StockDt.Rate as Price, abs(dbo.StockAdjustmentDetail.Qty) AS Out_Qty,  " _
            '        & "                        StockDt.Rate AS Out_Price, 0 AS In_Qty, 0 AS In_Price, StockAdjustmentDetail.Location_Id,0 as Party, Isnull(StockAdjustmentDetail.Pack_Desc,StockAdjustmentDetail.ArticalSize) as Pack_Desc, Isnull(StockAdjustmentDetail.S1,0) as PackQty, StockDt.StockTransDetailId " _
            '        & " FROM         dbo.StockAdjustmentMaster INNER JOIN " _
            '        & "                      dbo.StockAdjustmentDetail ON dbo.StockAdjustmentMaster.SA_id = dbo.StockAdjustmentDetail.SA_id " _
            '        & "                                                                                                               INNER JOIN StockMasterTable Stock on Stock.DocNo = StockAdjustmentMaster.Doc_No " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = StockAdjustmentDetail.Artical_id AND Stock.StockTransId = StockDt.StockTransId and StockDt.OutQty = StockAdjustmentDetail.Qty " _
            '        & "            WHERE(dbo.StockAdjustmentDetail.Qty < 0) " _
            '        & " AND (Convert(Varchar,StockAdjustmentMaster.Doc_Date,102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND StockAdjustmentDetail.Artical_Id=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockAdjustmentDetail.Location_Id=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT     dbo.StockAdjustmentMaster.Doc_no, dbo.StockAdjustmentMaster.Doc_Date, dbo.StockAdjustmentDetail.Artical_id, StockDt.Rate as Price, 0 AS Out_Qty,  " _
            '        & "                       0 AS Out_Price, abs(dbo.StockAdjustmentDetail.Qty) AS In_Qty,StockDt.Rate AS In_Price, StockAdjustmentDetail.Location_Id, 0 as Party, Isnull(StockAdjustmentDetail.Pack_Desc,StockAdjustmentDetail.ArticalSize) as Pack_Desc, Isnull(StockAdjustmentDetail.S1,0) as PackQty, StockDt.StockTransDetailId " _
            '        & " FROM         dbo.StockAdjustmentMaster INNER JOIN " _
            '        & "                       dbo.StockAdjustmentDetail ON dbo.StockAdjustmentMaster.SA_id = dbo.StockAdjustmentDetail.SA_id " _
            '        & "                                                                                                                                     INNER JOIN StockMasterTable Stock on Stock.DocNo = StockAdjustmentMaster.Doc_No" _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = StockAdjustmentDetail.Artical_id AND Stock.StockTransId = StockDt.StockTransId and StockDt.InQty = StockAdjustmentDetail.Qty " _
            '        & "            WHERE(dbo.StockAdjustmentDetail.Qty > 0) " _
            '        & " AND (Convert(Varchar,StockAdjustmentMaster.Doc_Date,102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND StockAdjustmentDetail.Artical_Id=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockAdjustmentDetail.Location_Id=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "             Union All " _
            '        & " SELECT   dbo.WarrantyClaimMasterTable.DocNo, dbo.WarrantyClaimMasterTable.DocDate, dbo.WarrantyClaimDetailTable.ArticleDefId, StockDt.Rate as Price,dbo.WarrantyClaimDetailTable.Qty AS Out_Qty,  " _
            '        & "                      StockDt.Rate AS Out_Price, 0 AS In_Qty, 0 AS In_Price, WarrantyClaimDetailTable.LocationId, WarrantyClaimMasterTable.CustomerCode as Party, Isnull(WarrantyClaimDetailTable.PackDesc,WarrantyClaimDetailTable.ArticleSize) as Pack_Desc, Isnull(WarrantyClaimDetailTable.Sz1,0) as PackQty, StockDt.StockTransDetailId " _
            '        & " FROM         dbo.WarrantyClaimMasterTable INNER JOIN " _
            '        & "                      dbo.WarrantyClaimDetailTable ON dbo.WarrantyClaimMasterTable.DocId = dbo.WarrantyClaimDetailTable.DocId " _
            '        & "                        INNER JOIN StockMasterTable Stock on Stock.DocNo = WarrantyClaimMasterTable.DocNo " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = WarrantyClaimDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId AND StockDt.OutQty = WarrantyClaimDetailTable.Qty " _
            '        & " WHERE (Convert(Varchar,WarrantyClaimMasterTable.DocDate,102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND WarrantyClaimDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND WarrantyClaimDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & " ) abc " _
            '        & " LEFT OUTER JOIN tblDefLocation Loc On Loc.Location_Id = abc.LocationId " _
            '        & " WHERE abc.ArticleDefId=" & Me.cmbItems.Value & " " _
            '        & " )Detail " _
            '        & " on Detail.ArticleDefId = ArticleDefView.ArticleId " _
            '        & " LEFT OUTER JOIN vwCOADetail on vwCOADetail.coa_detail_id = detail.party " _
            '        & " LEFT OUTER JOIN ( " _
            '        & " Select  ArticleDefId, Sum(Isnull(In_Qty,0)-Isnull(Out_Qty,0)) as Opening_Qty, SUM(((Isnull(In_Qty,0)*Isnull(In_Price,0))-(Isnull(Out_Qty,0)*Isnull(In_Price,0)))) as Opening_Amount  " _
            '        & " From ( " _
            '        & " SELECT     dbo.SalesMasterTable.SalesNo, dbo.SalesMasterTable.SalesDate, dbo.SalesDetailTable.ArticleDefId, SalesDetailTable.Price, dbo.SalesDetailTable.Qty AS Out_Qty,  " _
            '        & "                       StockDt.Rate AS Out_Price, 0 AS In_Qty, 0 AS In_Price, SalesDetailTable.LocationId " _
            '        & " FROM         dbo.SalesMasterTable INNER JOIN " _
            '        & "                      dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId " _
            '        & "                      INNER JOIN StockMasterTable Stock on Stock.DocNo = SalesMasterTable.SalesNo " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = SalesDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId and StockDt.OutQty = SalesDetailTable.Qty " _
            '        & " AND (Convert(Varchar,SalesMasterTable.SalesDate,102) < Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND SalesDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND SalesDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & "                      " _
            '        & "            Union All " _
            '        & " SELECT     dbo.SalesReturnMasterTable.SalesReturnNo, dbo.SalesReturnMasterTable.SalesReturnDate, dbo.SalesReturnDetailTable.ArticleDefId, SalesReturnDetailTable.Price, 0 AS Out_Qty,  " _
            '        & "                       0 AS Out_Price, dbo.SalesReturnDetailTable.Qty AS In_Qty, StockDt.Rate AS In_Price, SalesReturnDetailTable.LocationId " _
            '        & " FROM         dbo.SalesReturnMasterTable INNER JOIN " _
            '        & "                       dbo.SalesReturnDetailTable ON dbo.SalesReturnMasterTable.SalesReturnId = dbo.SalesReturnDetailTable.SalesReturnId " _
            '        & "                       INNER JOIN StockMasterTable Stock on Stock.DocNo = SalesReturnMasterTable.SalesReturnNo " _
            '        & "                       INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = SalesReturnDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId and StockDt.InQty = SalesReturnDetailTable.Qty " _
            '        & " AND (Convert(Varchar,SalesReturnMasterTable.SalesReturnDate,102) < Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND SalesReturnDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND SalesReturnDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT     dbo.PurchaseReturnMasterTable.PurchaseReturnNo, dbo.PurchaseReturnMasterTable.PurchaseReturnDate, dbo.PurchaseReturnDetailTable.ArticleDefId, PurchaseReturnDetailTable.Price,dbo.PurchaseReturnDetailTable.Qty AS Out_Qty,  " _
            '        & "                        StockDt.Rate AS Out_Price, 0 AS In_Qty, 0 AS In_Price, PurchaseReturnDetailTable.LocationId " _
            '        & " FROM         dbo.PurchaseReturnMasterTable INNER JOIN " _
            '        & "                      dbo.PurchaseReturnDetailTable ON dbo.PurchaseReturnMasterTable.PurchaseReturnId = dbo.PurchaseReturnDetailTable.PurchaseReturnId " _
            '        & "                       INNER JOIN StockMasterTable Stock on Stock.DocNo = PurchaseReturnMasterTable.PurchaseReturnNo   " _
            '        & "                                           INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = PurchaseReturnDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId and StockDt.OutQty = PurchaseReturnDetailTable.Qty " _
            '        & " AND (Convert(Varchar,PurchaseReturnMasterTable.PurchaseReturnDate,102) < Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND PurchaseReturnDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND PurchaseReturnDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT     dbo.ReceivingMasterTable.ReceivingNo, dbo.ReceivingMasterTable.ReceivingDate, dbo.ReceivingDetailTable.ArticleDefId, ReceivingDetailTable.Price,0 AS Out_Qty, " _
            '        & "                        0 AS Out_Price, dbo.ReceivingDetailTable.Qty AS In_Qty, Stockdt.Rate AS In_Price, ReceivingDetailTable.LocationId " _
            '        & " FROM         dbo.ReceivingMasterTable INNER JOIN " _
            '        & "                      dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId " _
            '        & "                       INNER JOIN StockMasterTable Stock on Stock.DocNo = ReceivingMasterTable.ReceivingNo " _
            '        & "                                             INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = ReceivingDetailTAble.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId and StockDt.InQty = ReceivingDetailTable.Qty " _
            '        & " AND (Convert(Varchar,ReceivingMasterTable.ReceivingDate,102) < Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND ReceivingDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND ReceivingDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT     dbo.ProductionMasterTable.Production_No, dbo.ProductionMasterTable.Production_Date, dbo.ProductionDetailTable.ArticleDefId, ProductionDetailTable.CurrentRate, 0 AS Out_Qty,  " _
            '        & "                       0 AS Out_Price, dbo.ProductionDetailTable.Qty AS In_Qty, StockDt.Rate AS In_Price, ProductionDetailTable.Location_Id " _
            '        & " FROM         dbo.ProductionMasterTable INNER JOIN " _
            '        & "                      dbo.ProductionDetailTable ON dbo.ProductionMasterTable.Production_Id = dbo.ProductionDetailTable.Production_Id " _
            '        & "                       INNER JOIN StockMasterTable Stock on Stock.DocNo = ProductionMasterTable.Production_No " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = ProductionDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId and StockDt.InQty = ProductionDetailTable.Qty " _
            '        & " AND (Convert(Varchar,ProductionMasterTable.Production_Date,102) < Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND ProductionDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND ProductionDetailTable.Location_Id=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT     dbo.DispatchMasterTable.DispatchNo, dbo.DispatchMasterTable.DispatchDate, dbo.DispatchDetailTable.ArticleDefId, DispatchDetailTable.Price,dbo.DispatchDetailTable.Qty AS Out_Qty,  " _
            '        & "                       StockDt.Rate AS Out_Price, 0 AS In_Qty, 0 AS In_Price, DispatchDetailTable.LocationId " _
            '        & " FROM         dbo.DispatchMasterTable INNER JOIN " _
            '        & "                      dbo.DispatchDetailTable ON dbo.DispatchMasterTable.DispatchId = dbo.DispatchDetailTable.DispatchId " _
            '        & "                       INNER JOIN StockMasterTable Stock on Stock.DocNo = DispatchMasterTable.DispatchNo " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = DispatchDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId and StockDt.OutQty = DispatchDetailTable.Qty " _
            '        & " AND (Convert(Varchar,DispatchMasterTable.DispatchDate,102) < Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND DispatchDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND DispatchDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT     dbo.StockAdjustmentMaster.Doc_no, dbo.StockAdjustmentMaster.Doc_Date, dbo.StockAdjustmentDetail.Artical_id, StockAdjustmentDetail.Price, abs(dbo.StockAdjustmentDetail.Qty) AS Out_Qty,  " _
            '        & "                       StockDt.Rate AS Out_Price, 0 AS In_Qty, 0 AS In_Price, StockAdjustmentDetail.Location_Id " _
            '        & " FROM         dbo.StockAdjustmentMaster INNER JOIN " _
            '        & "                      dbo.StockAdjustmentDetail ON dbo.StockAdjustmentMaster.SA_id = dbo.StockAdjustmentDetail.SA_id " _
            '        & "                       INNER JOIN StockMasterTable Stock on Stock.DocNo = StockAdjustmentMaster.Doc_No " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = StockAdjustmentDetail.Artical_Id AND Stock.StockTransId = StockDt.StockTransId and StockDt.OutQty = StockAdjustmentDetail.Qty " _
            '        & "            WHERE(dbo.StockAdjustmentDetail.Qty < 0) " _
            '        & " AND (Convert(Varchar,StockAdjustmentMaster.doc_Date,102) < Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND StockAdjustmentDetail.Artical_Id=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockAdjustmentDetail.Location_Id=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT     dbo.StockAdjustmentMaster.Doc_no, dbo.StockAdjustmentMaster.Doc_Date, dbo.StockAdjustmentDetail.Artical_id, StockAdjustmentDetail.Price, 0 AS Out_Qty,  " _
            '        & "                       0 AS Out_Price, abs(dbo.StockAdjustmentDetail.Qty) AS In_Qty,StockDt.Rate AS In_Price, StockAdjustmentDetail.Location_Id " _
            '        & " FROM         dbo.StockAdjustmentMaster INNER JOIN " _
            '        & "                      dbo.StockAdjustmentDetail ON dbo.StockAdjustmentMaster.SA_id = dbo.StockAdjustmentDetail.SA_id " _
            '        & "                                             INNER JOIN StockMasterTable Stock on Stock.DocNo = StockAdjustmentMaster.Doc_No " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = StockAdjustmentDetail.Artical_Id AND Stock.StockTransId = StockDt.StockTransId and StockDt.InQty = StockAdjustmentDetail.Qty " _
            '        & "            WHERE(dbo.StockAdjustmentDetail.Qty > 0) " _
            '        & " AND (Convert(Varchar,StockAdjustmentMaster.doc_Date,102) < Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND StockAdjustmentDetail.Artical_Id=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND StockAdjustmentDetail.Location_Id=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & "            Union All " _
            '        & " SELECT     dbo.WarrantyClaimMasterTable.DocNo, dbo.WarrantyClaimMasterTable.DocDate, dbo.WarrantyClaimDetailTable.ArticleDefId, StockDt.Rate as Price, abs(dbo.WarrantyClaimDetailTable.Qty) AS Out_Qty,  " _
            '        & " StockDt.Rate AS Out_Price, 0 AS In_Qty,0 AS In_Price, WarrantyClaimDetailTable.LocationId " _
            '        & " FROM         dbo.WarrantyClaimMasterTable INNER JOIN " _
            '        & " dbo.WarrantyClaimDetailTable ON dbo.WarrantyClaimMasterTable.DocId = dbo.WarrantyClaimDetailTable.DocId " _
            '        & " INNER JOIN StockMasterTable Stock on Stock.DocNo = WarrantyClaimMasterTable.DocNo " _
            '        & "                      INNER JOIN StockDetailTable StockDt on StockDt.ArticleDefId = WarrantyClaimDetailTable.ArticleDefId AND Stock.StockTransId = StockDt.StockTransId and StockDt.InQty = WarrantyClaimDetailTable.Qty " _
            '        & "            WHERE(dbo.WarrantyClaimDetailTable.Qty <> 0) " _
            '        & " AND (Convert(Varchar,WarrantyClaimMasterTable.DocDate,102) < Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND WarrantyClaimDetailTable.ArticleDefId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND WarrantyClaimDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '        & " ) abc1 " _
            '        & " WHERE (Convert(Varchar,abc1.SalesDate,102) < Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " _
            '        & " AND abc1.ArticleDefId=" & Me.cmbItems.Value & " " _
            '        & " Group By abc1.ArticleDefId " _
            '        & " ) opening " _
            '        & " on opening.ArticleDefId = ArticleDefView.ArticleId " _
            '        & "            WHERE(Opening_Qty <> 0 Or Out_Qty <> 0 Or In_Qty <> 0) ORDER BY  " _
            '        & "    Detail.SalesDate Asc "

            ''22-6-2015 TASKM226151 Imran Ali Runnting Total Problem Fixed
            'strQuery = "Select * From (SELECT 'Opening' AS DocNo, Convert(DateTime, '" & Me.dtpFrom.Value.AddDays(-1) & "',102) AS DocDate, 'OP' AS StockDocType, 'Opening Stock' AS Remarks, '' AS ProjectName, '' AS detail_code, '' AS detail_title, " _
            '& " '' AS Location_name, dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  " _
            '& " dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleUnitName, dbo.ArticleDefView.PackQty, '' AS Comments, '' AS Engine_No, '' AS Chassis_No, 0 AS Rate, IsNull(SUM(IsNull(dbo.StockDetailTable.In_PackQty,0)-IsNull(dbo.StockDetailTable.Out_PackQty,0)),0) as Pack_Stock, " _
            '& " Case When SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) <0 then 0 else SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) End AS In_Qty,  " _
            '& " Case When SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) <0 then 0 else SUM((ISNULL(dbo.StockDetailTable.Rate, 0) * ISNULL(dbo.StockDetailTable.Pack_Qty, 0))  * ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) End AS In_Amount,  " _
            '& " Abs(Case When SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) > 0 then 0 else SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) End) AS Out_Qty,  " _
            '& " Abs(Case When SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) > 0 then 0 else SUM((ISNULL(dbo.StockDetailTable.Rate, 0)*ISNULL(dbo.StockDetailTable.Pack_Qty, 0)) * ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) End) AS Out_Amount,0 AS Qty_Balance, 0 AS Amount_Balance, 0 as StockTransDetailId " _
            '& " FROM dbo.StockDetailTable INNER JOIN " _
            '& " dbo.StockMasterTable ON dbo.StockDetailTable.StockTransId = dbo.StockMasterTable.StockTransId INNER JOIN " _
            '& " dbo.tblDefLocation ON dbo.StockDetailTable.LocationId = dbo.tblDefLocation.location_id INNER JOIN " _
            '& " dbo.ArticleDefView ON dbo.StockDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
            '& " dbo.tblDefCostCenter ON dbo.StockMasterTable.Project = dbo.tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
            '& " dbo.Stock_Document_Type ON dbo.StockMasterTable.DocType = dbo.Stock_Document_Type.StockDocTypeId LEFT OUTER JOIN " _
            '& " dbo.tblDefLocation AS tblDefLocation_1 ON dbo.StockMasterTable.Account_Id = tblDefLocation_1.location_id LEFT OUTER JOIN " _
            '& " dbo.vwCOADetail ON dbo.StockMasterTable.Account_Id = dbo.vwCOADetail.coa_detail_id " _
            '& "  WHERE   (Convert(varchar,dbo.StockMasterTable.DocDate,102) < Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND  (dbo.ArticleDefView.ArticleId =" & Me.cmbItems.Value & ") " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND dbo.StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & " " _
            '& " GROUP BY dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName,  " _
            '& " dbo.ArticleDefView.ArticleUnitName, dbo.ArticleDefView.PackQty, dbo.ArticleDefView.ArticleId  " _
            '& " Union All " _
            '& " SELECT   dbo.StockMasterTable.DocNo, dbo.StockMasterTable.DocDate, dbo.Stock_Document_Type.StockDocType, dbo.StockMasterTable.Remarks,  " _
            '& " dbo.tblDefCostCenter.Name AS ProjectName,  " _
            '& " CASE WHEN tblDefLocation_1.location_name <> '' THEN tblDefLocation_1.location_code ELSE dbo.vwCOADetail.detail_code END AS detail_code,  " _
            '& " CASE WHEN tblDefLocation_1.location_name <> '' THEN tblDefLocation_1.location_name ELSE dbo.vwCOADetail.detail_title END AS detail_title,  " _
            '& " dbo.tblDefLocation.location_name, dbo.ArticleDefView.ArticleId,dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  " _
            '& " dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleUnitName, dbo.ArticleDefView.PackQty, dbo.StockDetailTable.Remarks AS Comments,  " _
            '& " dbo.StockDetailTable.Engine_No, dbo.StockDetailTable.Chassis_No, dbo.StockDetailTable.Rate, (IsNull(dbo.StockDetailTable.In_PackQty,0)-IsNull(dbo.StockDetailTable.Out_PackQty,0)) as Pack_Qty, IsNull(dbo.StockDetailTable.InQty,0) AS In_Qty,  " _
            '& " IsNull(dbo.StockDetailTable.InQty,0) * (IsNull(dbo.StockDetailTable.Rate,0)*IsNull(dbo.StockDetailTable.Pack_Qty,0)) AS In_Amount, IsNull(dbo.StockDetailTable.OutQty,0) AS Out_Qty,  " _
            '& " IsNull(dbo.StockDetailTable.OutQty,0) * (IsNull(dbo.StockDetailTable.Rate*dbo.StockDetailTable.Pack_Qty,0)) AS Out_Amount, 0 AS Qty_Balance, 0 AS Amount_Balance, dbo.StockDetailTable.StockTransDetailId " _
            '& " FROM dbo.StockDetailTable INNER JOIN " _
            '& " dbo.StockMasterTable ON dbo.StockDetailTable.StockTransId = dbo.StockMasterTable.StockTransId INNER JOIN " _
            '& " dbo.tblDefLocation ON dbo.StockDetailTable.LocationId = dbo.tblDefLocation.location_id INNER JOIN " _
            '& " dbo.ArticleDefView ON dbo.StockDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
            '& " dbo.tblDefCostCenter ON dbo.StockMasterTable.Project = dbo.tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
            '& "  dbo.Stock_Document_Type ON dbo.StockMasterTable.DocType = dbo.Stock_Document_Type.StockDocTypeId LEFT OUTER JOIN " _
            '& " dbo.tblDefLocation AS tblDefLocation_1 ON dbo.StockMasterTable.Account_Id = tblDefLocation_1.location_id LEFT OUTER JOIN " _
            '& " dbo.vwCOADetail ON dbo.StockMasterTable.Account_Id = dbo.vwCOADetail.coa_detail_id " _
            '& " WHERE (Convert(varchar,dbo.StockMasterTable.DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)) AND (dbo.ArticleDefView.ArticleId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND dbo.StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "") & ")) a Order By a.DocDate ASC "
            'End TaskM226151

            ''TASK TFS4540 Additon of three new columns of In_PackQty, Out_PackQty and Pack_Balance_Qty on 17-09-2018

            strQuery = "Select * From (SELECT 'Opening' AS DocNo, Convert(DateTime, '" & Me.dtpFrom.Value.AddDays(-1) & "',102) AS DocDate, 'OP' AS StockDocType, 'Opening Stock' AS Remarks, '' AS ProjectName, '' AS detail_code, '' AS detail_title, " _
           & " '' AS Location_name, dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  " _
           & " dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleUnitName, dbo.ArticleDefView.PackQty, '' AS Comments, '' AS Engine_No, '' AS Chassis_No, 0 AS Rate, IsNull(SUM(IsNull(dbo.StockDetailTable.In_PackQty,0)-IsNull(dbo.StockDetailTable.Out_PackQty,0)),0) as Pack_Stock, " _
                      & " Case When SUM(ISNULL(dbo.StockDetailTable.In_PackQty, 0) - ISNULL(dbo.StockDetailTable.Out_PackQty, 0)) <0 then 0 else SUM(ISNULL(dbo.StockDetailTable.In_PackQty, 0) - ISNULL(dbo.StockDetailTable.Out_PackQty, 0)) End AS In_PackQty, " _
           & " Abs(Case When SUM(ISNULL(dbo.StockDetailTable.In_PackQty, 0) - ISNULL(dbo.StockDetailTable.Out_PackQty, 0)) > 0 then 0 else SUM(ISNULL(dbo.StockDetailTable.In_PackQty, 0) - ISNULL(dbo.StockDetailTable.Out_PackQty, 0)) End) AS Out_PackQty, " _
           & " Case When SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) <0 then 0 else SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) End AS In_Qty,  " _
           & " SUM(ISNULL(dbo.StockDetailTable.Rate, 0)  * ISNULL(dbo.StockDetailTable.InQty, 0)) AS In_Amount,  " _
           & " Abs(Case When SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) > 0 then 0 else SUM(ISNULL(dbo.StockDetailTable.InQty, 0) - ISNULL(dbo.StockDetailTable.OutQty, 0)) End) AS Out_Qty, " _
           & " SUM(ISNULL(dbo.StockDetailTable.Rate, 0) * ISNULL(dbo.StockDetailTable.OutQty, 0)) AS Out_Amount,0 AS Qty_Balance, 0 AS Pack_Balance_Qty, 0 AS Amount_Balance, 0 as StockTransDetailId " _
           & " FROM dbo.StockDetailTable INNER JOIN " _
           & " dbo.StockMasterTable ON dbo.StockDetailTable.StockTransId = dbo.StockMasterTable.StockTransId INNER JOIN " _
           & " dbo.tblDefLocation ON dbo.StockDetailTable.LocationId = dbo.tblDefLocation.location_id INNER JOIN " _
           & " dbo.ArticleDefView ON dbo.StockDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
           & " dbo.tblDefCostCenter ON dbo.StockMasterTable.Project = dbo.tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
           & " dbo.Stock_Document_Type ON dbo.StockMasterTable.DocType = dbo.Stock_Document_Type.StockDocTypeId LEFT OUTER JOIN " _
           & " dbo.tblDefLocation AS tblDefLocation_1 ON dbo.StockMasterTable.Account_Id = tblDefLocation_1.location_id LEFT OUTER JOIN " _
           & " dbo.vwCOADetail ON dbo.StockMasterTable.Account_Id = dbo.vwCOADetail.coa_detail_id " _
           & "  WHERE   (Convert(varchar,dbo.StockMasterTable.DocDate,102) < Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND  (dbo.ArticleDefView.ArticleId =" & Me.cmbItems.Value & ") " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND dbo.StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "AND StockDetailTable.LocationId IN (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")") & " " _
           & " GROUP BY dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName,  " _
           & " dbo.ArticleDefView.ArticleUnitName, dbo.ArticleDefView.PackQty, dbo.ArticleDefView.ArticleId  " _
           & " Union All " _
           & " SELECT   dbo.StockMasterTable.DocNo, dbo.StockMasterTable.DocDate, dbo.Stock_Document_Type.StockDocType, dbo.StockMasterTable.Remarks,  " _
           & " dbo.tblDefCostCenter.Name AS ProjectName,  " _
           & " CASE WHEN tblDefLocation_1.location_name <> '' THEN tblDefLocation_1.location_code ELSE dbo.vwCOADetail.detail_code END AS detail_code,  " _
           & " CASE WHEN tblDefLocation_1.location_name <> '' THEN tblDefLocation_1.location_name ELSE dbo.vwCOADetail.detail_title END AS detail_title,  " _
           & " dbo.tblDefLocation.location_name, dbo.ArticleDefView.ArticleId,dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleColorName,  " _
           & " dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleUnitName, dbo.ArticleDefView.PackQty, dbo.StockDetailTable.Remarks AS Comments,  " _
           & " dbo.StockDetailTable.Engine_No, dbo.StockDetailTable.Chassis_No, dbo.StockDetailTable.Rate, (IsNull(dbo.StockDetailTable.In_PackQty,0)-IsNull(dbo.StockDetailTable.Out_PackQty,0)) as Pack_Qty, IsNull(dbo.StockDetailTable.In_PackQty,0) AS In_PackQty, IsNull(dbo.StockDetailTable.Out_PackQty,0) AS Out_PackQty, IsNull(dbo.StockDetailTable.InQty,0) AS In_Qty,  " _
           & " IsNull(dbo.StockDetailTable.InQty,0) * (IsNull(dbo.StockDetailTable.Rate,0)) AS In_Amount, IsNull(dbo.StockDetailTable.OutQty,0) AS Out_Qty, " _
           & " IsNull(dbo.StockDetailTable.OutQty,0)* (IsNull(dbo.StockDetailTable.Rate,0)) AS Out_Amount, 0 AS Qty_Balance, 0 AS Pack_Balance_Qty, 0 AS Amount_Balance, dbo.StockDetailTable.StockTransDetailId " _
           & " FROM dbo.StockDetailTable INNER JOIN " _
           & " dbo.StockMasterTable ON dbo.StockDetailTable.StockTransId = dbo.StockMasterTable.StockTransId INNER JOIN " _
           & " dbo.tblDefLocation ON dbo.StockDetailTable.LocationId = dbo.tblDefLocation.location_id INNER JOIN " _
           & " dbo.ArticleDefView ON dbo.StockDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
           & " dbo.tblDefCostCenter ON dbo.StockMasterTable.Project = dbo.tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
           & "  dbo.Stock_Document_Type ON dbo.StockMasterTable.DocType = dbo.Stock_Document_Type.StockDocTypeId LEFT OUTER JOIN " _
           & " dbo.tblDefLocation AS tblDefLocation_1 ON dbo.StockMasterTable.Account_Id = tblDefLocation_1.location_id LEFT OUTER JOIN " _
           & " dbo.vwCOADetail ON dbo.StockMasterTable.Account_Id = dbo.vwCOADetail.coa_detail_id " _
           & " WHERE (Convert(varchar,dbo.StockMasterTable.DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)) AND (dbo.ArticleDefView.ArticleId=" & Me.cmbItems.Value & " " & IIf(Me.cmbLocation.SelectedIndex > 0, " AND dbo.StockDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "", "AND StockDetailTable.LocationId IN (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")") & ")) a Order By a.DocDate ASC "

            Dim dt As New DataTable
            dt = GetDataTable(strQuery)
            dt.AcceptChanges()


            ''22-6-2015 TASKM226151 Imran Ali Runnting Total Problem Fixed
            'dt.Columns.Add("In_Amount", GetType(System.Double))
            'dt.Columns.Add("Out_Amount", GetType(System.Double))
            'dt.Columns.Add("Qty_Balance", GetType(System.Double))
            'dt.Columns.Add("Amount_Balance", GetType(System.Double))


            'dt.Columns("In_Amount").Expression = "(In_Qty*In_Price)"
            'dt.Columns("Out_Amount").Expression = "(Out_Qty*Out_Price)"



            'Dim dv As New DataView
            'dt.TableName = "ItemLedger"
            'dv.Table = dt
            'If Me.ComboBox1.SelectedIndex > 0 Then
            '    dv.RowFilter = " ArticleDefId In (" & Me.ComboBox1.SelectedValue & ")"
            'End If
            'Dim dtData As New DataTable
            'dtData = dv.ToTable(True)
            'dtData.AcceptChanges()


            'Dim dr As DataRow
            'If dtData.Rows.Count > 0 Then

            'dr = dtData.NewRow
            'dr("SalesNo") = "Opening"
            'dr("SalesDate") = Me.dtpFrom.Value.AddDays(-1)
            'dr("ArticleDefId") = dtData.Rows(0).Item("ArticleDefId")
            'dr("ArticleCode") = dtData.Rows(0).Item("ArticleCode")
            'dr("ArticleDescription") = dtData.Rows(0).Item("ArticleDescription")
            'dr("ArticleSizeName") = dtData.Rows(0).Item("ArticleSizeName")
            'dr("ArticleColorName") = dtData.Rows(0).Item("ArticleColorName")
            'If dtData.Rows(0).Item("OpeningQty") > 0 Then
            '    dr("In_Qty") = dtData.Rows(0).Item("OpeningQty")
            '    dr("Out_Qty") = 0
            'Else
            '    dr("Out_Qty") = dtData.Rows(0).Item("OpeningQty")
            '    dr("In_Qty") = 0
            'End If

            'If dtData.Rows(0).Item("OpeningAmount") > 0 Then
            '    dr("In_Amount") = dtData.Rows(0).Item("OpeningAmount")
            '    dr("Out_Amount") = 0
            'Else
            '    dr("Out_Amount") = dtData.Rows(0).Item("OpeningAmount")
            '    dr("In_Amount") = 0
            'End If
            'dtData.Rows.InsertAt(dr, 0)

            'End If
            'End TaskM226151

            Dim dblQtyBalance As Double = 0D
            Dim dblAmtBalance As Double = 0D
            Dim dblPackQtyBalance As Double = 0D
            For Each r As DataRow In dt.Rows
                dblQtyBalance += r.Item("In_Qty") - r.Item("Out_Qty")
                dblPackQtyBalance += r.Item("In_PackQty") - r.Item("Out_PackQty")
                dblAmtBalance += r.Item("In_Amount") - r.Item("Out_Amount")

                r.BeginEdit()
                r("Qty_Balance") = dblQtyBalance
                r("Pack_Balance_Qty") = dblPackQtyBalance
                r("Amount_Balance") = dblAmtBalance
                r.EndEdit()
                'dblQtyBalance += dblQtyBalance
                'dblAmtBalance += dblAmtBalance
            Next

            'Dim dv1 As New DataView
            'dtData.TableName = "ItemLedger"
            'dv1.Table = dtData

            'Dim strFilter As String = String.Empty

            'strFilter = "SalesNo <> ''"


            'If Not Me.cmbLocation.SelectedIndex = -1 Then
            '    If Me.cmbLocation.SelectedIndex > 0 Then '
            '        strFilter += " AND LocationId=" & Me.cmbLocation.SelectedValue & ""
            '    End If
            'End If
            'dv1.RowFilter = strFilter
            'dv1.Sort = "SalesDate ASC" 'TaskLM18 Set Sorting By Date
            dt.AcceptChanges()
            Me.GridEX1.DataSource = dt
            GridEX1.RetrieveStructure()

            'GridEX1.RootTable.Columns("In_Price").Visible = False
            'GridEX1.RootTable.Columns("Out_Price").Visible = False
            'GridEX1.RootTable.Columns("OpeningAmount").Visible = False
            'GridEX1.RootTable.Columns("OpeningQty").Visible = False
            GridEX1.RootTable.Columns("ArticleId").Visible = False
            'GridEX1.RootTable.Columns("LocationId").Visible = False
            'GridEX1.RootTable.Columns("Party").Visible = False
            GridEX1.RootTable.Columns("StockTransDetailId").Visible = False
            GridEX1.RootTable.Columns("In_PackQty").Visible = False
            GridEX1.RootTable.Columns("Out_PackQty").Visible = False

            GridEX1.RootTable.Columns("DocDate").FormatString = "dd/MMM/yyyy"
            GridEX1.RootTable.Columns("DocDate").Caption = "Doc Date"
            GridEX1.RootTable.Columns("DocNo").Caption = "Doc No"
            GridEX1.RootTable.Columns("StockDocType").Caption = "Doc Type"


            GridEX1.RootTable.Columns("ArticleSizeName").Caption = "Size"
            GridEX1.RootTable.Columns("ArticleColorName").Caption = "Color"
            GridEX1.RootTable.Columns("ArticleUnitName").Caption = "Unit"


            GridEX1.RootTable.Columns("In_Qty").Caption = "Recv Stock"
            GridEX1.RootTable.Columns("Out_Qty").Caption = "Out Stock"

            GridEX1.RootTable.Columns("In_Amount").Caption = "Recv Stock Val"
            GridEX1.RootTable.Columns("Out_Amount").Caption = "Out Stock Val"
            GridEX1.RootTable.Columns("Pack_Balance_Qty").Caption = "Pack Bal Qty"


            GridEX1.RootTable.Columns("In_Qty").Visible = True
            GridEX1.RootTable.Columns("Out_Qty").Visible = True
            GridEX1.RootTable.Columns("Qty_Balance").Visible = True
            GridEX1.RootTable.Columns("Amount_Balance").Visible = True
            GridEX1.RootTable.Columns("In_Amount").Visible = True
            GridEX1.RootTable.Columns("Out_Amount").Visible = True
            GridEX1.RootTable.Columns("Pack_Balance_Qty").Visible = True


            ''R:M-2 Set Rounding figurate format
            ''Task:2359 --------------------------------------------------------------------
            GridEX1.RootTable.Columns("In_Qty").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Out_Qty").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Qty_Balance").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Amount_Balance").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("In_Amount").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Out_Amount").FormatString = "N" & DecimalPointInValue

            GridEX1.RootTable.Columns("In_Qty").TotalFormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Out_Qty").TotalFormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Qty_Balance").TotalFormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Amount_Balance").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("In_Amount").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Out_Amount").TotalFormatString = "N" & DecimalPointInValue
            'End R:M-2
            GridEX1.RootTable.Columns("Pack_Stock").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Pack_Stock").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Pack_Stock").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Pack_Stock").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'GridEX1.RootTable.Columns("In_Amount").Position = GridEX1.RootTable.Columns("In_Qty").Index + 1
            'GridEX1.RootTable.Columns("Out_Amount").Position = GridEX1.RootTable.Columns("Out_Qty").Index + 2
            'GridEX1.RootTable.Columns("Amount_Balance").Position = GridEX1.RootTable.Columns("Qty_Balance").Index + 1

            ''TASK TFS4540
            'GridEX1.RootTable.Columns("Out_Amount").Position = GridEX1.RootTable.Columns("Pack_Balance_Qty").Index + 1

            GridEX1.RootTable.Columns("Pack_Balance_Qty").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Pack_Balance_Qty").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Pack_Balance_Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Pack_Balance_Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Pack_Balance_Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("In_PackQty").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("In_PackQty").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("In_PackQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("In_PackQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("In_PackQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Out_PackQty").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Out_PackQty").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Out_PackQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Out_PackQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Out_PackQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            ''END TASK TFS4540
            GridEX1.RootTable.Columns("Pack_Stock").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("In_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Out_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'GridEX1.RootTable.Columns("Amount_Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("In_Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("Out_Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'GridEX1.RootTable.Columns("Qty_Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum



            GridEX1.RootTable.Columns("In_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Out_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Amount_Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("In_Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Out_Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Qty_Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far


            GridEX1.RootTable.Columns("In_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Out_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Amount_Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("In_Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Out_Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Qty_Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            For c As Integer = 0 To Me.GridEX1.RootTable.Columns.Count - 1
                Me.GridEX1.RootTable.Columns(c).AllowSort = False
            Next
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.GridEX1.AutoSizeColumns()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Item Ledger by Pack" & Chr(10) & "From Date: " & dtpFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpTo.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''Task# 3-11-06-2015 add radion button event for search by Code
    Private Sub rbtByCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnByCode.CheckedChanged
        Try
            If IsOpenedForm = False Then
                Exit Sub
            End If

            If Me.rbtnByCode.Checked = True Then
                Me.cmbItems.DisplayMember = Me.cmbItems.DisplayLayout.Bands(0).Columns("ArticleCode").Key.ToString

            Else
                Me.cmbItems.DisplayMember = Me.cmbItems.DisplayLayout.Bands(0).Columns("ArticleDescription").Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Emd Task# 3-11-06-2015

    ''Task# 3-11-06-2015 add radion button event for search by Name
    Private Sub rbtByName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnByName.CheckedChanged
        Try
            If IsOpenedForm = False Then
                Exit Sub
            End If

            If Me.rbtnByName.Checked = True Then
                Me.cmbItems.DisplayMember = Me.cmbItems.DisplayLayout.Bands(0).Columns("ArticleDescription").Key.ToString
            Else
                Me.cmbItems.DisplayMember = Me.cmbItems.DisplayLayout.Bands(0).Columns("ArticleCode").Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End Task# 3-11-06-2015

End Class