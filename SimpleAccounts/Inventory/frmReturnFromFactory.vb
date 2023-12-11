''TASK TFS3839 Muhammad Amin has developed this form and its associated DAL and Model classes. Dated 12-07-2018
'' TASK TFS4845 Location should be dropdown in grid and should be displayed location name instead of location code. Done on 29-10-2018 by Muhammad Amin 

Imports SBModel
Imports SBDal
Imports SBDal.StockDocTypeDAL
Public Class frmReturnFromFactory
    Implements IGeneral

    Public ID As Integer = 0
    Dim ObjDAL As ReturnFromFactoryMasterDAL
    Dim ObjDetailDAL As ReturnFromFactoryDetailDAL
    Dim ObjModel As ReturnFromFactoryMasterBE
    Dim IsEditMode As Boolean = False
    Dim IsPosted As Boolean = False
    Dim IsFormOpened As Boolean = False
    Structure Detail
        Public Shared ID As String = "ID"
        Public Shared ReturnFromFactoryId As String = "ReturnFromFactoryId"
        Public Shared LocationId As String = "LocationId"
        'Public Shared Location As String = "Location"
        Public Shared ArticleId As String = "ArticleId"
        Public Shared Article As String = "Article"
        Public Shared AlternateArticleId As String = "AlternateArticleId"
        Public Shared AlternateArticle As String = "AlternateArticle"
        Public Shared Unit As String = "Unit"
        Public Shared Rate As String = "Rate"
        Public Shared Sz1 As String = "Sz1"
        Public Shared Sz7 As String = "Sz7"
        Public Shared Qty As String = "Qty"
        Public Shared Comments As String = "Comments"
    End Structure

    Public Sub New(ByVal DoHaveSaveRights As Boolean, ByVal DoHavePrintRight As Boolean, ByVal DoHaveExportRights As Boolean, ByVal DoHaveFieldChooserRights As Boolean)
        Try
            ' This call is required by the designer.
            InitializeComponent()
            IsFormOpened = True
            ObjDetailDAL = New ReturnFromFactoryDetailDAL()
            ObjDAL = New ReturnFromFactoryMasterDAL()
            FillCombos("Party")
            FillCombos("Location")
            FillCombos("Article")
            FillCombos("Unit")
            ReSetControls()
            Me.btnSave.Text = "Save"
            If DoHaveSaveRights = True Then
                Me.btnSave.BringToFront()
                Me.btnSave.Visible = True
                Me.btnSave.Location = New Point(701, 19)
                Me.btnPost.SendToBack()
                Me.btnPrint.SendToBack()
                Me.btnPost.Visible = False
                Me.btnPrint.Visible = False
            Else
                Me.btnSave.SendToBack()
                Me.btnSave.Visible = False
                Me.btnPost.SendToBack()
                Me.btnPrint.SendToBack()
                Me.btnPost.Visible = False
                Me.btnPrint.Visible = False

            End If
            Me.CtrlGrdBar1.mGridPrint.Enabled = DoHavePrintRight
            Me.CtrlGrdBar1.mGridExport.Enabled = DoHaveExportRights
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = DoHaveFieldChooserRights
            'Me.btnSave.Enabled = DoHaveSaveRights
            'Me.btnPost.Enabled = False
            'Me.btnPrint.Enabled = False
            ' Add any initialization after the InitializeComponent() call.
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub New(ByVal Obj As ReturnFromFactoryMasterBE, ByVal UpdateRights As Boolean, ByVal PostRights As Boolean, ByVal PrintRights As Boolean, ByVal DoHaveExportRights As Boolean, ByVal DoHaveFieldChooserRights As Boolean)
        Try
            ' This call is required by the designer.
            InitializeComponent()
            IsFormOpened = True
            ObjDetailDAL = New ReturnFromFactoryDetailDAL()
            ObjDAL = New ReturnFromFactoryMasterDAL()
            FillCombos("Party")
            FillCombos("Location")
            FillCombos("Article")
            FillCombos("Unit")
            ReSetControls()
            IsEditMode = True
            Me.btnSave.Text = "Update"
            'Me.btnSave.Enabled = UpdateRights
            'Me.btnPost.Enabled = PostRights
            'Me.btnPrint.Enabled = PrintRights
            SetButtonsLocation(UpdateRights, PostRights, PrintRights)
            EditRecord(Obj)
            If IsPosted = True Then
                Me.btnPost.Text = "Unpost"
                Me.btnSave.Enabled = False
                Me.btnPrint.Enabled = True
            Else
                Me.btnPost.Text = "Post"
                Me.btnPrint.Enabled = False
            End If
            Me.CtrlGrdBar1.mGridPrint.Enabled = PrintRights
            Me.CtrlGrdBar1.mGridExport.Enabled = DoHaveExportRights
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = DoHaveFieldChooserRights
            ' Add any initialization after the InitializeComponent() call.
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub

    Private Sub txtPackQuantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackQuantity.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQuantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQuantity.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalQuantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalQuantity.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim Str As String = String.Empty
        If Condition = "Article" Then
            ''Below line is commented against TASK TFS4798
            'Str = "SELECT ArticleDefView.ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleDefView.ArticleBrandName As Grade, LastPurchasePrice.Rate AS Price, Isnull(SalePrice,0) as SalePrice, ArticleDefView.SizeRangeID as [Size ID], Isnull(ArticleDefView.ArticleTaxId,0) as [Tax ID], Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SubSubId,0) as AccountId, Isnull(ServiceItem,0) as ServiceItem, ArticleDefView.SortOrder, ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand],Isnull(LogicalItem,0) as LogicalItem, SalesAccountId, CGSAccountId, MasterID , IsNull(Cost_Price,0) as Cost_Price, IsNull(TradePrice,0) as [Trade Price], IsNull(PrintedRetailPrice,0) as [Retail Price], Isnull(StockDetail.Stock,0) as Stock, IsNull(dbo.ArticleDefView.ApplyAdjustmentFuelExp,1) as  ApplyAdjustmentFuelExp FROM ArticleDefView Left Outer Join (SELECT MAX(StockTransDetailId) AS StockTransDetailId, ArticleDefId AS ArticleId, IsNull(Rate, 0) AS Rate FROM StockDetailTable WHERE InQty > 0 GROUP BY ArticleDefId, Rate) AS LastPurchasePrice ON ArticleDefView.ArticleId= LastPurchasePrice.ArticleId LEFT OUTER JOIN  (Select ArticleDefId, Sum(IsNull(InQty, 0)-IsNull(OutQty, 0)) As Stock From StockDetailTable " & IIf(Me.cmbLocation.ActiveRow IsNot Nothing, " WHERE LocationId=" & Me.cmbLocation.Value & "", "") & "  Group By ArticleDefId) As StockDetail ON ArticleDefView.ArticleId = StockDetail.ArticleDefId Where ArticleDefView.Active=1  "
            Str = "SELECT ArticleDefView.ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleDefView.ArticleBrandName As Grade, ISNULL(PurchasePrice, 0) AS Price, Isnull(SalePrice,0) as SalePrice, ArticleDefView.SizeRangeID as [Size ID], Isnull(ArticleDefView.ArticleTaxId,0) as [Tax ID], Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SubSubId,0) as AccountId, Isnull(ServiceItem,0) as ServiceItem, ArticleDefView.SortOrder, ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand],Isnull(LogicalItem,0) as LogicalItem, SalesAccountId, CGSAccountId, MasterID , IsNull(Cost_Price,0) as Cost_Price, IsNull(TradePrice,0) as [Trade Price], IsNull(PrintedRetailPrice,0) as [Retail Price], Isnull(StockDetail.Stock,0) as Stock, IsNull(dbo.ArticleDefView.ApplyAdjustmentFuelExp,1) as  ApplyAdjustmentFuelExp FROM ArticleDefView  LEFT OUTER JOIN  (Select ArticleDefId, Sum(IsNull(InQty, 0)-IsNull(OutQty, 0)) As Stock From StockDetailTable " & IIf(Me.cmbLocation.ActiveRow IsNot Nothing, " WHERE LocationId=" & Me.cmbLocation.Value & "", "") & "  Group By ArticleDefId) As StockDetail ON ArticleDefView.ArticleId = StockDetail.ArticleDefId Where ArticleDefView.Active=1  "

            If ItemSortOrder = True Then
                Str += " ORDER By ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByCode = True Then
                Str += " ORDER By ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByName = True Then
                Str += " ORDER By ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            Else
                Str += " ORDER By ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            End If
            FillUltraDropDown(Me.cmbProduct, Str)

            If cmbProduct.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbProduct.Rows(0).Activate()
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("ApplyAdjustmentFuelExp").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Tax ID").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("SalesAccountId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("MasterID").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Type").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Dept").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Origin").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Brand").Hidden = True

                Me.cmbProduct.DisplayLayout.Bands(0).Columns("SalePrice").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True


                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("LogicalItem").Hidden = True

                Me.cmbProduct.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("LogicalItem").Hidden = True

                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Cost_Price").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Trade Price").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Retail Price").Hidden = True
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Name").Width = 300
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Limit").Width = 80
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Discount").Width = 80
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Width = 200
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head"

                If rbName.Checked = True Then
                    Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(2).Column.Key.ToString
                Else
                    Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(1).Column.Key.ToString
                End If
            End If
        ElseIf Condition = "Party" Then
            If getConfigValueByType("Show Vendor On Sales") = "True" Then
                Str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, vwCOADetail.detail_code as [Code], tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                  "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                                  "FROM  tblCustomer LEFT OUTER JOIN " & _
                                  "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                  "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                  "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                  "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                  "WHERE (vwCOADetail.account_type in('Customer', 'Vendor')) and  vwCOADetail.coa_detail_id is not  null "
            Else
                Str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name,vwCOADetail.detail_code as [Code], tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                  "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile,  vwCOADetail.Sub_Sub_Title " & _
                                                  "FROM         tblCustomer LEFT OUTER JOIN " & _
                                                  "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                  "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                  "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                  "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                  "WHERE (vwCOADetail.account_type='Vendor') and  vwCOADetail.coa_detail_id is not  null "
            End If
            Str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title "
            FillUltraDropDown(cmbParty, Str)
            If cmbParty.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbParty.Rows(0).Activate()
                Me.cmbParty.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                Me.cmbParty.DisplayLayout.Bands(0).Columns("Territory").Hidden = True
                Me.cmbParty.DisplayLayout.Bands(0).Columns("State").Hidden = True
                Me.cmbParty.DisplayLayout.Bands(0).Columns("ExpiryDate").Hidden = True
                Me.cmbParty.DisplayLayout.Bands(0).Columns("Fuel").Hidden = True
                Me.cmbParty.DisplayLayout.Bands(0).Columns("Other Expense").Hidden = True
                Me.cmbParty.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                Me.cmbParty.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbParty.DisplayLayout.Bands(0).Columns("Phone").Hidden = True
                Me.cmbParty.DisplayLayout.Bands(0).Columns("Mobile").Hidden = True
                Me.cmbParty.DisplayLayout.Bands(0).Columns("Limit").Hidden = True
                Me.cmbParty.DisplayLayout.Bands(0).Columns("Discount").Hidden = True
                Me.cmbParty.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Hidden = True


                Me.cmbParty.DisplayLayout.Bands(0).Columns("Name").Width = 200
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Limit").Width = 80
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Discount").Width = 80
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Width = 200
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head"
            End If
        ElseIf Condition = "Location" Then
            Str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, location_name, IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, location_name, IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
            FillUltraDropDown(cmbLocation, Str, False)
            If cmbLocation.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbLocation.Rows(0).Activate()
                Me.cmbLocation.DisplayLayout.Bands(0).Columns("Location_Id").Hidden = True
                Me.cmbLocation.DisplayLayout.Bands(0).Columns("location_name").Width = 300
                Me.cmbLocation.DisplayLayout.Bands(0).Columns("AllowMinusStock").Hidden = True
            End If
        ElseIf Condition = "Unit" Then
            Me.cmbUnit.ValueMember = "ArticlePackId"
            Me.cmbUnit.DisplayMember = "PackName"
            Me.cmbUnit.DataSource = GetPackData(Me.cmbProduct.Value)
            If cmbUnit.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbUnit.Rows(0).Activate()
                Me.cmbUnit.DisplayLayout.Bands(0).Columns("ArticlePackId").Hidden = True
                Me.cmbUnit.DisplayLayout.Bands(0).Columns("PackRate").Hidden = True
                Me.cmbUnit.DisplayLayout.Bands(0).Columns("PackName").Header.Caption = "Pack Name"
                Me.cmbUnit.DisplayLayout.Bands(0).Columns("PackQty").Header.Caption = "Pack Qty"
            End If
        End If
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            ObjModel = New ReturnFromFactoryMasterBE()
            ObjModel.ID = ID
            ObjModel.ReturnNo = Me.txtReturnNo.Text
            ObjModel.ReturnDate = Me.dtpReturnDate.Value
            ObjModel.PartyId = Me.cmbParty.Value
            ObjModel.DriverName = Me.txtDriverName.Text
            ObjModel.VehicleNo = Me.txtVeichleNo.Text
            ObjModel.Remarks = Me.txtRemarks.Text

            ''Master Stock Entry
            'ObjModel.Stock
            'StockMaster = New StockMaster
            ObjModel.Stock.StockTransId = 0I 'transId
            ObjModel.Stock.DocNo = ObjModel.ReturnNo.Replace("'", "''")
            ObjModel.Stock.DocDate = Me.dtpReturnDate.Value.Date
            ObjModel.Stock.DocType = Convert.ToInt32(GetStockDocTypeId("Dispatch"))
            ObjModel.Stock.Remaks = Me.txtRemarks.Text.ToString.Replace("'", "''")
            ObjModel.Stock.Project = 0
            ObjModel.Stock.AccountId = Me.cmbParty.Value
            ObjModel.Stock.StockDetailList = New List(Of StockDetail)
            For Each _Row As Janus.Windows.GridEX.GridEXRow In Me.grdReturnFromFactory.GetRows
                Dim _Detail As New ReturnFromFactoryDetailBE
                _Detail.ID = Val(_Row.Cells(Detail.ID).Value.ToString)
                _Detail.ReturnToFactoryId = Val(_Row.Cells(Detail.ReturnFromFactoryId).Value.ToString)
                _Detail.LocationId = Val(_Row.Cells(Detail.LocationId).Value.ToString)
                _Detail.ArticleId = Val(_Row.Cells(Detail.ArticleId).Value.ToString)
                _Detail.AlternateArticleId = Val(_Row.Cells(Detail.AlternateArticleId).Value.ToString)
                _Detail.Unit = _Row.Cells(Detail.Unit).Value.ToString
                _Detail.Rate = Val(_Row.Cells(Detail.Rate).Value.ToString)
                _Detail.Sz1 = Val(_Row.Cells(Detail.Sz1).Value.ToString)
                _Detail.Sz7 = Val(_Row.Cells(Detail.Sz7).Value.ToString)
                _Detail.Qty = Val(_Row.Cells(Detail.Qty).Value.ToString)
                _Detail.Comments = _Row.Cells(Detail.Comments).Value.ToString
                ObjModel.Detail.Add(_Detail)
                ''Stock detail entry
                Dim StockDetail As New StockDetail
                StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                StockDetail.LocationId = Val(_Row.Cells(Detail.LocationId).Value.ToString)
                StockDetail.ArticleDefId = Val(_Row.Cells(Detail.AlternateArticleId).Value.ToString)
                StockDetail.InQty = Val(_Row.Cells(Detail.Qty).Value.ToString)
                StockDetail.OutQty = 0
                StockDetail.Rate = Val(_Row.Cells(Detail.Rate).Value.ToString)
                StockDetail.InAmount = Val(_Row.Cells(Detail.Qty).Value.ToString) * Val(_Row.Cells(Detail.Rate).Value.ToString)
                StockDetail.OutAmount = 0
                StockDetail.Remarks = _Row.Cells(Detail.Comments).Value.ToString
                StockDetail.Engine_No = String.Empty
                StockDetail.Chassis_No = String.Empty
                StockDetail.PackQty = Val(_Row.Cells(Detail.Sz7).Value.ToString)
                StockDetail.Out_PackQty = Val(_Row.Cells(Detail.Sz1).Value.ToString)
                StockDetail.In_PackQty = 0
                StockDetail.BatchNo = String.Empty
                ObjModel.Stock.StockDetailList.Add(StockDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtReturnNo.Text.Length < 1 Then
                ShowErrorMessage("Return No is required.")
                Me.txtReturnNo.Focus()
                Return False
            End If
            If Me.cmbParty.Value < 1 Then
                ShowErrorMessage("Party is required.")
                Me.cmbParty.Focus()
                Return False
            End If
            If Me.grdReturnFromFactory.RowCount = 0 Then
                ShowErrorMessage("Grid is empty.")
                Me.grdReturnFromFactory.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If IsDetailValidated() Then
                AddToGrid()
                ResetDetailControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub AddToGrid()
        'Public Shared ID As String = "ID"
        'Public Shared ReturnToFactoryId As String = "ReturnToFactoryId"
        'Public Shared LocationId As String = "LocationId"
        'Public Shared Location As String = "Location"
        'Public Shared ArticleId As String = "ArticleId"
        'Public Shared Article As String = "Article"
        'Public Shared Unit As String = "Unit"
        'Public Shared Rate As String = "Rate"
        'Public Shared Sz1 As String = "Sz1"
        'Public Shared Sz7 As String = "Sz7"
        'Public Shared Qty As String = "Qty"
        'Public Shared TotalQty As String = "TotalQty"
        'Public Shared Comments As String = "Comments"
        Try
            Dim dtGrid As DataTable = CType(Me.grdReturnFromFactory.DataSource, DataTable)
            Dim NewRow As DataRow
            NewRow = dtGrid.NewRow
            NewRow(Detail.ID) = 0
            NewRow(Detail.ReturnFromFactoryId) = ID
            NewRow(Detail.LocationId) = Me.cmbLocation.Value
            'NewRow(Detail.Location) = Me.cmbLocation.Text
            NewRow(Detail.ArticleId) = Me.cmbProduct.Value
            NewRow(Detail.Article) = Me.cmbProduct.ActiveRow.Cells("Item").Value.ToString
            NewRow(Detail.AlternateArticleId) = Me.cmbProduct.Value
            NewRow(Detail.AlternateArticle) = Me.cmbProduct.ActiveRow.Cells("Item").Value.ToString
            NewRow(Detail.Unit) = Me.cmbUnit.Text
            NewRow(Detail.Rate) = Me.cmbProduct.ActiveRow.Cells("Price").Value
            NewRow(Detail.Sz1) = Val(Me.txtQuantity.Text)
            NewRow(Detail.Sz7) = Val(Me.txtPackQuantity.Text)
            NewRow(Detail.Qty) = Val(Me.txtTotalQuantity.Text)
            NewRow(Detail.Comments) = Me.txtComments.Text
            dtGrid.Rows.Add(NewRow)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DisplayDetail(ByVal ID As Integer)
        Try
            Me.grdReturnFromFactory.DataSource = ObjDetailDAL.DisplayDetail(ID)
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Sz1).FormatString = "N" & DecimalPointInQty
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Sz7).FormatString = "N" & DecimalPointInQty
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Qty).FormatString = "N" & DecimalPointInQty
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Rate).FormatString = "N" & DecimalPointInValue
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Sz1).TotalFormatString = "N" & DecimalPointInQty
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Sz7).TotalFormatString = "N" & DecimalPointInQty
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Qty).TotalFormatString = "N" & DecimalPointInQty
            'Me.grdReturnToFactory.RootTable.Columns(Detail.Rate).TotalFormatString = "N" & DecimalPointInValue
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Sz1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Sz7).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Qty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Rate).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Sz1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Sz7).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReturnFromFactory.RootTable.Columns(Detail.Rate).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            '' TASK TFS
            Dim dtLocation As DataTable = GetDataTable("SELECT location_id AS LocationId, location_name AS LocationName FROM tblDefLocation")
            'Me.grdReturnToFactory.RootTable.Columns("LocationId").EditType = Janus.Windows.GridEX.EditType.Combo
            'Me.grdReturnToFactory.RootTable.Columns("LocationId").HasValueList = True
            'Me.grdReturnToFactory.RootTable.Columns("LocationId").LimitToList = True
            Me.grdReturnFromFactory.RootTable.Columns("LocationId").ValueList.PopulateValueList(dtLocation.DefaultView, "LocationId", "LocationName")
            '' 
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("RFF" + "-" + Microsoft.VisualBasic.Right(Me.dtpReturnDate.Value.Year, 2) + "-", "ReturnFromFactoryMaster", "ReturnNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("RFF" & "-" & Format(Me.dtpReturnDate.Value, "yy") & Me.dtpReturnDate.Value.Month.ToString("00"), 4, "ReturnFromFactoryMaster", "ReturnNo")
            Else
                Return GetNextDocNo("RFF", 6, "ReturnFromFactoryMaster", "ReturnNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub EditRecord(ByVal Obj As ReturnFromFactoryMasterBE)
        Try
            ID = Obj.ID
            Me.txtReturnNo.Text = Obj.ReturnNo
            Me.dtpReturnDate.Value = Obj.ReturnDate
            Me.cmbParty.Value = Obj.PartyId
            Me.txtDriverName.Text = Obj.DriverName
            Me.txtVeichleNo.Text = Obj.VehicleNo
            Me.txtRemarks.Text = Obj.Remarks
            IsPosted = Obj.IsPosted
            DisplayDetail(ID)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ResetControls()
        Try
            IsEditMode = False
            Me.txtReturnNo.Text = GetDocumentNo()
            Me.txtReturnNo.Focus()
            Me.dtpReturnDate.Value = Now
            Me.cmbParty.Rows(0).Activate()
            Me.txtDriverName.Text = String.Empty
            Me.txtVeichleNo.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
            DisplayDetail(-1)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() Then
                If IsEditMode = False Then
                    ObjModel.ActivityLog = New ActivityLog()
                    ObjModel.ActivityLog.ActivityName = "Save"
                    ObjModel.ActivityLog.ApplicationName = "RFF"
                    ObjModel.ActivityLog.FormCaption = "Return From Factory"
                    ObjModel.ActivityLog.FormName = "frmReturnFromFactory"
                    ObjModel.ActivityLog.LogDateTime = dtpReturnDate.Value
                    ObjModel.ActivityLog.RecordType = String.Empty
                    ObjModel.ActivityLog.RefNo = ObjModel.ReturnNo
                    ObjModel.ActivityLog.Source = "frmReturnFromFactory"
                    ObjModel.ActivityLog.User_Name = LoginUserName
                    ObjModel.ActivityLog.UserID = LoginUserId
                    If ObjDAL.Add(ObjModel) Then
                        msg_Information("Record has been saved successfully.")
                        Me.Close()
                    Else
                        msg_Information("Record could not save!")
                    End If
                Else
                    ObjModel.ActivityLog = New ActivityLog()
                    ObjModel.ActivityLog.ActivityName = "Update"
                    ObjModel.ActivityLog.ApplicationName = "RFF"
                    ObjModel.ActivityLog.FormCaption = "Return From Factory"
                    ObjModel.ActivityLog.FormName = "frmReturnFromFactory"
                    ObjModel.ActivityLog.LogDateTime = Now
                    ObjModel.ActivityLog.RecordType = String.Empty
                    ObjModel.ActivityLog.RefNo = ObjModel.ReturnNo
                    ObjModel.ActivityLog.Source = "frmReturnFromFactory"
                    ObjModel.ActivityLog.User_Name = LoginUserName
                    ObjModel.ActivityLog.UserID = LoginUserId
                    If ObjDAL.Update(ObjModel) Then
                        msg_Information("Record has been updated successfully.")
                        Me.Close()
                    Else
                        msg_Information("Record could not update!")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If IsValidate() Then
                If IsPosted = False Then
                    ObjModel.ActivityLog = New ActivityLog()
                    ObjModel.ActivityLog.ActivityName = "Update"
                    ObjModel.ActivityLog.ApplicationName = "RFF"
                    ObjModel.ActivityLog.FormCaption = "Return From Factory"
                    ObjModel.ActivityLog.FormName = "frmReturnFromFactory"
                    ObjModel.ActivityLog.LogDateTime = Now
                    ObjModel.ActivityLog.RecordType = String.Empty
                    ObjModel.ActivityLog.RefNo = ObjModel.ReturnNo
                    ObjModel.ActivityLog.Source = "frmReturnFromFactory"
                    ObjModel.ActivityLog.User_Name = LoginUserName
                    ObjModel.ActivityLog.UserID = LoginUserId
                    If ObjDAL.Post(ObjModel) Then
                        msg_Information("Record has been posted successfully.")
                        Me.Close()
                    Else
                        msg_Information("Record could not post!")
                    End If
                Else
                    ObjModel.Stock.StockTransId = CInt(StockTransId(ObjModel.ReturnNo))
                    ObjModel.ActivityLog = New ActivityLog()
                    ObjModel.ActivityLog.ActivityName = "Unpost"
                    ObjModel.ActivityLog.ApplicationName = "RFF"
                    ObjModel.ActivityLog.FormCaption = "Return From Factory"
                    ObjModel.ActivityLog.FormName = "frmReturnFromFactory"
                    ObjModel.ActivityLog.LogDateTime = Now
                    ObjModel.ActivityLog.RecordType = String.Empty
                    ObjModel.ActivityLog.RefNo = ObjModel.ReturnNo
                    ObjModel.ActivityLog.Source = "frmReturnFromFactory"
                    ObjModel.ActivityLog.User_Name = LoginUserName
                    ObjModel.ActivityLog.UserID = LoginUserId
                    If ObjDAL.UnPost(ObjModel) Then
                        msg_Information("Record has been unposted successfully.")
                        Me.Close()
                    Else
                        msg_Information("Record could not unpost!")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProduct_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbProduct.RowSelected
        If cmbProduct.IsItemInList = False Then Exit Sub
        Try
            If Me.cmbProduct.Value > 0 Then
                FillCombos("Unit")
            End If
            'If IsOpenedForm = True Then FillCombos("Unit")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbUnit_ValueChanged(sender As Object, e As EventArgs) Handles cmbUnit.ValueChanged
        'Try
        '    If cmbUnit.Text = "Loose" Then
        '        Me.txtPackQuantity.Enabled = False
        '        Me.txtPackQuantity.Text = 1
        '    Else
        '        Me.txtPackQuantity.Enabled = True
        '        Me.txtPackQuantity.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try

        Try
            If Me.cmbUnit.Text = "Loose" Then
                Me.txtPackQuantity.Text = 1
                Me.txtQuantity.Text = 1

                Me.txtPackQuantity.Enabled = False
                Me.txtPackQuantity.TabStop = False
                'Me.txtTotalQty.Enabled = False
            Else
                Me.txtPackQuantity.Enabled = True
                Me.txtPackQuantity.TabStop = True
                Me.txtPackQuantity.Enabled = True
                Me.txtPackQuantity.Text = Val(cmbUnit.ActiveRow.Cells("PackQty").Value.ToString)
                Me.txtQuantity.Text = 1
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtPackQuantity.TextChanged
        
        Try
            If Val(Me.txtPackQuantity.Text) > 0 And Val(Me.txtQuantity.Text) > 0 Then
                Me.txtTotalQuantity.Text = Val(Me.txtPackQuantity.Text) * Val(Me.txtQuantity.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtQuantity.TextChanged
        Try
            If Val(Me.txtPackQuantity.Text) > 0 And Val(Me.txtQuantity.Text) > 0 Then
                Me.txtTotalQuantity.Text = Val(Me.txtPackQuantity.Text) * Val(Me.txtQuantity.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdReturnToFactory_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdReturnFromFactory.CellUpdated
        Try
            If grdReturnFromFactory.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = Detail.Sz1 Then
                    If Val(Me.grdReturnFromFactory.GetRow.Cells(Detail.Sz1).Value.ToString) > 0 Then
                        Me.grdReturnFromFactory.GetRow.Cells(Detail.Qty).Value = Me.grdReturnFromFactory.GetRow.Cells(Detail.Sz1).Value * Me.grdReturnFromFactory.GetRow.Cells(Detail.Sz7).Value
                    End If
                ElseIf e.Column.Key = Detail.Sz7 Then
                    If Val(Me.grdReturnFromFactory.GetRow.Cells(Detail.Sz1).Value.ToString) > 0 Then
                        Me.grdReturnFromFactory.GetRow.Cells(Detail.Qty).Value = Me.grdReturnFromFactory.GetRow.Cells(Detail.Sz1).Value * Me.grdReturnFromFactory.GetRow.Cells(Detail.Sz7).Value
                    End If
                ElseIf e.Column.Key = Detail.Qty Then
                    If Val(Me.grdReturnFromFactory.GetRow.Cells(Detail.Sz7).Value.ToString) = 1 Then
                        Me.grdReturnFromFactory.GetRow.Cells(Detail.Sz1).Value = Me.grdReturnFromFactory.GetRow.Cells(Detail.Qty).Value
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function IsDetailValidated() As Boolean
        Try
            If Not Me.cmbLocation.Value > 0 Then
                ShowErrorMessage("Location is required.")
                Me.cmbLocation.Focus()
                Return False
            End If
            If Not Me.cmbProduct.Value > 0 Then
                ShowErrorMessage("Product is required.")
                Me.cmbProduct.Focus()
                Return False
            End If
            If Val(Me.txtTotalQuantity.Text) = 0 Or Val(Me.txtTotalQuantity.Text) < 0 Then
                ShowErrorMessage("Quantity should be greator than zero.")
                Me.txtTotalQuantity.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If IsEditMode = True Then
                GetCrystalReportRights()
                AddRptParam("@ID", ID)
                ShowReport("ReturnFromFactory")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ResetDetailControls()
        Try
            Me.cmbLocation.Rows(0).Activate()
            Me.cmbProduct.Rows(0).Activate()
            Me.cmbUnit.Text = "Loose"
            Me.txtPackQuantity.Text = 1
            Me.txtQuantity.Text = String.Empty
            Me.txtTotalQuantity.Text = String.Empty
            Me.txtComments.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rbCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode.CheckedChanged
        Try
            If IsFormOpened = False Then Exit Sub
            If Me.rbCode.Checked = True Then Me.cmbProduct.DisplayMember = "Code"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbName_CheckedChanged(sender As Object, e As EventArgs) Handles rbName.CheckedChanged
        Try
            If IsFormOpened = False Then Exit Sub
            If Me.rbName.Checked = True Then Me.cmbProduct.DisplayMember = "Item"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdReturnToFactory_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdReturnFromFactory.ColumnButtonClick
        Try
            If Me.grdReturnFromFactory.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "Delete" Then
                    If IsPosted = False Then
                        If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                        ObjDetailDAL.Delete(Val(Me.grdReturnFromFactory.GetRow.Cells("ID").Value.ToString))
                        Me.grdReturnFromFactory.GetRow.Delete()
                    Else
                        ShowErrorMessage("Posted record can not be deleted")
                    End If
                End If
                If e.Column.Key = "Alternative" Then
                    frmFactoryAlternateItems.ShowDialog()
                    If frmFactoryAlternateItems.ItemId > 0 Then
                        Me.grdReturnFromFactory.GetRow.BeginEdit()
                        Me.grdReturnFromFactory.GetRow.Cells(Detail.AlternateArticleId).Value = frmFactoryAlternateItems.ItemId
                        Me.grdReturnFromFactory.GetRow.Cells(Detail.AlternateArticle).Value = frmFactoryAlternateItems.ItemName
                        Me.grdReturnFromFactory.GetRow.EndEdit()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SetButtonsLocation(ByVal UpdateRights As Boolean, ByVal PostRights As Boolean, ByVal PrintRights As Boolean)
        Try
            'Me.btnSave.Enabled = UpdateRights
            'Me.btnPost.Enabled = PostRights
            'Me.btnPrint.Enabled = PrintRights
            If UpdateRights = True AndAlso PostRights = False AndAlso PrintRights = False Then
                Me.btnSave.BringToFront()
                Me.btnSave.Visible = True
                Me.btnSave.Location = New Point(701, 19)

                Me.btnPost.SendToBack()
                Me.btnPost.Visible = False
                Me.btnPrint.SendToBack()
                Me.btnPrint.Visible = False
            End If
            If UpdateRights = True AndAlso PostRights = True AndAlso PrintRights = False Then
                Me.btnSave.BringToFront()
                Me.btnSave.Visible = True
                Me.btnSave.Location = New Point(615, 19)

                Me.btnPost.BringToFront()
                Me.btnPost.Visible = True
                Me.btnPost.Location = New Point(701, 19)

                Me.btnPrint.SendToBack()
                Me.btnPrint.Visible = False
            End If
            If UpdateRights = True AndAlso PostRights = True AndAlso PrintRights = True Then
                Me.btnSave.BringToFront()
                Me.btnSave.Visible = True
                Me.btnSave.Location = New Point(527, 19)

                Me.btnPost.BringToFront()
                Me.btnPost.Visible = True
                Me.btnPost.Location = New Point(615, 19)

                Me.btnPrint.BringToFront()
                Me.btnPrint.Visible = True
                Me.btnPrint.Location = New Point(701, 19)

            End If

            If UpdateRights = False AndAlso PostRights = True AndAlso PrintRights = True Then
                Me.btnSave.SendToBack()
                Me.btnSave.Visible = False
                'Me.btnSave.Location = New Point(527, 19)

                Me.btnPost.BringToFront()
                Me.btnPost.Visible = True
                Me.btnPost.Location = New Point(615, 19)

                Me.btnPrint.BringToFront()
                Me.btnPrint.Visible = True
                Me.btnPrint.Location = New Point(701, 19)

            End If
            If UpdateRights = False AndAlso PostRights = False AndAlso PrintRights = True Then
                Me.btnSave.SendToBack()
                Me.btnSave.Visible = False
                'Me.btnSave.Location = New Point(527, 19)

                Me.btnPost.SendToBack()
                Me.btnPost.Visible = False
                'Me.btnPost.Location = New Point(615, 19)

                Me.btnPrint.BringToFront()
                Me.btnPrint.Visible = True
                Me.btnPrint.Location = New Point(701, 19)

            End If

            If UpdateRights = False AndAlso PostRights = False AndAlso PrintRights = False Then
                Me.btnSave.SendToBack()
                Me.btnSave.Visible = False
                'Me.btnSave.Location = New Point(527, 19)

                Me.btnPost.SendToBack()
                Me.btnPost.Visible = False
                'Me.btnPost.Location = New Point(615, 19)

                Me.btnPrint.SendToBack()
                Me.btnPrint.Visible = False
                'Me.btnPost.Location = New Point(701, 19)

            End If
            If UpdateRights = False AndAlso PostRights = True AndAlso PrintRights = False Then
                Me.btnSave.SendToBack()
                Me.btnSave.Visible = False
                'Me.btnSave.Location = New Point(527, 19)

                Me.btnPost.SendToBack()
                Me.btnPost.Visible = True
                Me.btnPost.Location = New Point(701, 19)

                Me.btnPrint.SendToBack()
                Me.btnPrint.Visible = False
                'Me.btnPost.Location = New Point(701, 19)

            End If
            If UpdateRights = True AndAlso PostRights = False AndAlso PrintRights = True Then
                Me.btnSave.SendToBack()
                Me.btnSave.Visible = True
                Me.btnSave.Location = New Point(615, 19)

                Me.btnPost.BringToFront()
                Me.btnPost.Visible = False
                'Me.btnPost.Location = New Point(615, 19)

                Me.btnPrint.BringToFront()
                Me.btnPrint.Visible = True
                Me.btnPrint.Location = New Point(701, 19)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbParty_Enter(sender As Object, e As EventArgs)
        Me.cmbParty.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub

    Private Sub cmbProduct_Enter(sender As Object, e As EventArgs) Handles cmbProduct.Enter
        Me.cmbProduct.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub

    Private Sub cmbProduct_InitializeLayout(sender As Object, e As Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbProduct.InitializeLayout

    End Sub

    Private Sub frmReturnFromFactory_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub grdReturnFromFactory_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdReturnFromFactory.CellValueChanged
        ''TASK TFS4852 
        If grdReturnFromFactory.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            If e.Column.Key = "Sz7" Then
                If grdReturnFromFactory.GetRow.Cells("Unit").Value.ToString = "Loose" Then
                    grdReturnFromFactory.CancelCurrentEdit()
                End If
            End If
        End If
        ''END TASK TFS4852
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdReturnFromFactory.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdReturnFromFactory.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdReturnFromFactory.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Return From Factory"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class