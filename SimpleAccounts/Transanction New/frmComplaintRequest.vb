'' TASK TFS4845 Location should be dropdown in grid and should be displayed location name instead of location code. Done on 29-10-2018 by Muhammad Amin 
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmComplaintRequest

    Implements IGeneral
    Dim objModel As ComplaintRequestBE
    Dim Detail As List(Of ComplaintRequestDetailBE)
    Dim objDal As New ComplaintRequestDAL
    Public Shared ComplainRequestId As Integer = 0
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHavePostRights As Boolean = False
    Public DoHavePrintRights As Boolean = False
    Public DoHaveFieldChooserRights As Boolean = False
    Public DoHaveExportRights As Boolean = False
    Dim IsFormOpened As Boolean = False
    Public Shared EditMode As Boolean = False
    Public Shared Post As Boolean = False

    Dim txtPONo As String
    Dim setVoucherNo As String = String.Empty
    Dim IsWIPAccount As Boolean = False
    Dim flgStoreIssuenceVoucher As Boolean = False
    Dim StockList As List(Of StockDetail)
    Dim StockDetail As StockDetail
    Dim flgCompanyRights As Boolean = False
    Dim setVoucherdate As DateTime
    Dim DispatchId As Integer = 0

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub frmComplaintRequest_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.dtpComplaintDate.Focus()
            frmComplaintRequestList.GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmComplaintRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombos("Vendor")
            FillCombos("Location")
            FillCombos("ArticlePack")
            FillCombos("Item")
            UltraDropDownSearching(cmbVendor, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            'UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            Dim dt As DataTable
            dt = New ComplaintRequestDAL().GetById(ComplainRequestId)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Me.txtDocNo.Text = dt.Rows(0).Item("ComplaintNo")
                    Me.dtpComplaintDate.Value = dt.Rows(0).Item("ComplaintDate")
                    Me.dtpReturnDate.Value = dt.Rows(0).Item("ComplaintReturnDate")
                    Me.cmbVendor.Value = dt.Rows(0).Item("CustomerId")
                    Me.txtPersonName.Text = dt.Rows(0).Item("PersonName")
                    Me.txtContactNo.Text = dt.Rows(0).Item("ContactNo")
                    Me.txtRemarks.Text = dt.Rows(0).Item("Remarks")
                    btnSave.Enabled = DoHaveUpdateRights
                    btnPost.Enabled = DoHavePostRights
                    Me.CtrlGrdBar1.mGridPrint.Enabled = DoHavePrintRights
                    Me.CtrlGrdBar1.mGridExport.Enabled = DoHaveExportRights
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = DoHaveFieldChooserRights
                    Post = dt.Rows(0).Item("Posted")
                    If Post = True Then
                        btnSave.Visible = False
                        btnPrint.Visible = True
                        btnPrint.Enabled = DoHavePrintRights
                        btnPost.Text = "UnPost"
                    Else
                        btnSave.Visible = True
                        btnSave.Text = "Update"
                        btnPrint.Visible = False
                        btnPost.Text = "Post"
                    End If
                Next
            Else
                ReSetControls()
            End If
            IsFormOpened = True
            'Me.dtpComplaintDate.Focus()
            DisplayRecords(ComplainRequestId)
            Me.dtpComplaintDate.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns("ComplaintId").Visible = False
            Me.grd.RootTable.Columns("ComplaintDetailId").Visible = False
            Me.grd.RootTable.Columns("LocationId").Visible = True
            Me.grd.RootTable.Columns("ItemId").Visible = False
            Me.grd.RootTable.Columns("ArticleDescription").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grd.RootTable.Columns("LocationName").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("unit").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("TotalQty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            '' TASK TFS4798
            Me.grd.RootTable.Columns("Price").EditType = Janus.Windows.GridEX.EditType.NoEdit
            '' END TASK TFS4798
            Me.grd.RootTable.Columns("ArticleDescription").Width = 250
            Me.grd.RootTable.Columns("LocationId").Caption = "Location"

            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If
            Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("PackQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PackQty").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("TotalQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("TotalQty").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Price").TotalFormatString = "N" & DecimalPointInValue

            '' TASK TFS
            Dim dtLocation As DataTable = GetDataTable("SELECT location_id AS LocationId, location_name AS LocationName FROM tblDefLocation")
            Me.grd.RootTable.Columns("LocationId").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grd.RootTable.Columns("LocationId").HasValueList = True
            Me.grd.RootTable.Columns("LocationId").LimitToList = True
            Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(dtLocation.DefaultView, "LocationId", "LocationName")
            '' 
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "Vendor" Then
                Dim ShowVendorOnSales As Boolean = False
                Dim ShowMiscAccountsOnSales As Boolean = False
                If Not getConfigValueByType("Show Vendor On Sales") = "Error" Then
                    ShowVendorOnSales = CBool(getConfigValueByType("Show Vendor On Sales"))
                End If
                If Not getConfigValueByType("ShowMiscAccountsOnSales") = "Error" Then
                    ShowMiscAccountsOnSales = CBool(getConfigValueByType("ShowMiscAccountsOnSales"))
                End If
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                 "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, tblCustomer.SaleMan, tblCustomer.Email, tblCustomer.Phone, vwCOADetail.Sub_Sub_Title " & _
                                 "FROM dbo.tblCustomer LEFT OUTER JOIN " & _
                                 "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                 "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId LEFT OUTER JOIN " & _
                                 "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                 "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                 "WHERE dbo.vwCOADetail.detail_title Is Not NULL " & IIf(ShowVendorOnSales = True, " AND (dbo.vwCOADetail.account_type in ('Customer','Vendor'))", " AND (dbo.vwCOADetail.account_type in ('Customer'))") & "" _
                                 & "" & IIf(ShowMiscAccountsOnSales = True, " OR vwCOADetail.coa_detail_id IN (SELECT  DISTINCT tblCOAMainSubSubDetail.coa_detail_id " & _
                                       "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) ", "") & ""
                If flgCompanyRights = True Then
                    str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                End If
                If LoginGroup = "Administrator" Then
                ElseIf GetMappedUserId() > 0 And getGroupAccountsConfigforSales(Me.Name) Then
                    str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, tblCustomer.SaleMan, tblCustomer.Email, tblCustomer.Phone, vwCOADetail.Sub_Sub_Title " & _
                                "FROM dbo.tblCustomer LEFT OUTER JOIN " & _
                                "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId LEFT OUTER JOIN " & _
                                "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                "WHERE (dbo.vwCOADetail.detail_title Is Not NULL) "
                    str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                           & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ") ) "
                End If
                str += " AND vwCOADetail.Active=1"

                str += "order by tblCustomer.Sortorder, vwCOADetail.detail_title"
                FillUltraDropDown(cmbVendor, str, True)
                Me.cmbVendor.Rows(0).Activate()
                If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("TypeId").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("SaleMan").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("State").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("City").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Territory").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" 'Task:2373 Change Caption
                End If
            ElseIf Condition = "Item" Then
                ''Below line is commented against TASK TFS4798 ON 25-10-2018
                'str = "SELECT Distinct ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, isNull((SELECT isNull(StockDetailTable.Rate,0) FROM StockDetailTable WHERE StockDetailTable.StockTransDetailId = (SELECT MAX(StockDetailTable.StockTransDetailId) FROM StockDetailTable WHERE StockDetailTable.ArticleDefId = ArticleDefView.ArticleId AND StockDetailTable.InQty > 0)),0) as Price  FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId LEFT OUTER JOIN receivingdetailtable ON receivingdetailtable.ArticleDefId = ArticleDefView.ArticleId " _
                str = " SELECT Distinct ArticleDefView.ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleDefView.PurchasePrice AS Price  FROM  ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId LEFT OUTER JOIN receivingdetailtable ON receivingdetailtable.ArticleDefId = ArticleDefView.ArticleId " _
                      & " LEFT OUTER JOIN ( SELECT  MAX(StockDetailTable.StockTransDetailId) as StockTransferDetailId , Max(StockDetailTable.Rate) AS Price, ArticleDefID FROM StockDetailTable Group By ArticleDefID) AS Stock ON Stock.ArticleDefID = ArticleDefView.ArticleId "
                str += " where Active=1 "
                If ItemSortOrderByCode = True Then
                    str += " ORDER BY ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByName = True Then
                    str += " ORDER BY ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                End If
                FillUltraDropDown(Me.cmbItem, str, True)
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                If Me.rdoCode.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            ElseIf Condition = "Location" Then
                str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                       & " Select Location_Id, location_name, IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                       & " Else " _
                       & " Select Location_Id, location_name, IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                FillDropDown(cmbLocation, str, False)
            ElseIf Condition = "ArticlePack" Then
                Me.cmbUnit.ValueMember = "ArticlePackId"
                Me.cmbUnit.DisplayMember = "PackName"
                Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New ComplaintRequestBE
            objModel.ComplaintId = ComplainRequestId
            objModel.ComplaintNo = txtDocNo.Text
            objModel.ComplaintDate = dtpComplaintDate.Value
            objModel.ComplaintReturnDate = dtpReturnDate.Value
            objModel.CustomerId = cmbVendor.Value
            objModel.PersonName = txtPersonName.Text
            objModel.ContactNo = txtContactNo.Text
            objModel.Remarks = txtRemarks.Text
            objModel.post = Post
            Detail = New List(Of ComplaintRequestDetailBE)
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                Dim WDetail As New ComplaintRequestDetailBE
                WDetail.ComplaintDetailId = Val(Row.Cells("ComplaintDetailId").Value.ToString)
                WDetail.LocationId = Row.Cells("LocationId").Value.ToString
                WDetail.ItemId = Row.Cells("ItemId").Value.ToString
                WDetail.Unit = Row.Cells("Unit").Value.ToString
                WDetail.Price = Row.Cells("Price").Value.ToString
                WDetail.Sz1 = Row.Cells("Qty").Value.ToString
                WDetail.Sz7 = Row.Cells("PackQty").Value.ToString
                WDetail.Qty = Row.Cells("TotalQty").Value.ToString
                WDetail.Comments = Row.Cells("Comments").Value.ToString
                Detail.Add(WDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub
    Public Sub DisplayRecords(ByVal ComplainRequestId As Integer)
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT ComplaintRequestDetail.ComplaintDetailId, ComplaintRequestDetail.ComplaintId, ComplaintRequestDetail.LocationId, ComplaintRequestDetail.ItemId, ArticleDefView.ArticleDescription, ComplaintRequestDetail.Unit, ComplaintRequestDetail.Sz1 as Qty, ComplaintRequestDetail.Sz7 as PackQty, ComplaintRequestDetail.Qty as TotalQty, ComplaintRequestDetail.Price, ComplaintRequestDetail.Comments FROM ComplaintRequestDetail LEFT OUTER JOIN ArticleDefView ON ComplaintRequestDetail.ItemId = ArticleDefView.ArticleId  Where ComplaintRequestDetail.ComplaintId = " & ComplainRequestId & ""
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            dt.Columns("TotalQty").Expression = "Qty*PackQty"
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.grd.RowCount = 0 Then
                msg_Error("No record found in grid")
                Return False
            End If
            If cmbVendor.Value = 0 Then
                msg_Error("Please select a Customer")
                Return False
            End If
            If txtContactNo.Text = "" Then
                msg_Error("Please Enter a ContactNo")
                Return False
            End If
            If txtPersonName.Text = "" Then
                msg_Error("Please Enter a Person Name")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            If Condition = "Detail" Then
                If cmbLocation.SelectedIndex > 0 Then cmbLocation.SelectedIndex = 0
                If Me.cmbItem.Rows.Count > 0 Then cmbItem.Rows(0).Activate()
                Me.txtDRemarks.Text = ""
                txtPackQuantity.Text = "1"
                txtQuantity.Text = "1"
                txtTotalQuantity.Text = "1"
            Else
                Me.dtpComplaintDate.Focus()
                txtDocNo.Text = GetDocumentNo()
                dtpComplaintDate.Value = Date.Now
                If Me.cmbVendor.Rows.Count > 0 Then cmbItem.Rows(0).Activate()
                Me.dtpReturnDate.Value = Date.Now
                Me.txtPersonName.Text = ""
                txtRemarks.Text = ""
                Me.txtContactNo.Text = ""
                Me.btnSave.Visible = True
                Me.btnSave.Text = "Save"
            End If
            btnSave.Enabled = DoHaveSaveRights
            '' TASK TFS4880
            Me.CtrlGrdBar1.mGridPrint.Enabled = DoHavePrintRights
            Me.CtrlGrdBar1.mGridExport.Enabled = DoHaveExportRights
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = DoHaveFieldChooserRights
            '' END TASK TFS4880
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetDocumentNo() As String
        Dim DocNo As String = String.Empty
        Try
            DocNo = GetNextDocNo("CR-" & Format(Me.dtpComplaintDate.Value, "yy") & Me.dtpComplaintDate.Value.Month.ToString("00"), 4, "ComplaintRequestMaster", "ComplaintNo")
            Return DocNo
        Catch ex As Exception
            msg_Error(ex.Message)
            Return String.Empty
        End Try
    End Function

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
            If Not Me.cmbItem.Value = 0 AndAlso Not txtQuantity.Text = "" AndAlso Not txtTotalQuantity.Text = "" AndAlso Not txtPackQuantity.Text = "" Then
                AddToGrid()
                ReSetControls("Detail")
                Me.cmbItem.Focus()
            Else
                msg_Error("Please Enter Complete Data to add in Grid")
                Me.cmbItem.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Public Sub AddToGrid()
        Try
            Dim dt As DataTable
            dt = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr("ComplaintDetailId") = 0
            dr("ComplaintId") = 0
            dr("ItemId") = Me.cmbItem.Value
            dr("ArticleDescription") = Me.cmbItem.ActiveRow.Cells("Item").Value.ToString
            dr("LocationId") = Me.cmbLocation.SelectedValue
            'dr("LocationName") = Me.cmbLocation.Text
            dr("Unit") = Me.cmbUnit.Text
            dr("Price") = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            dr("Qty") = Me.txtQuantity.Text
            dr("PackQty") = Me.txtPackQuantity.Text
            dr("Comments") = Me.txtDRemarks.Text
            dt.Rows.Add(dr)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If ComplainRequestId = 0 Then
                    If objDal.Add(objModel) Then
                        If objDal.AddComplaintDetail(Detail) Then
                            msg_Information("Record has been saved successfully.")
                            SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, txtDocNo.Text, True)
                            ReSetControls()
                            ReSetControls("Detail")
                            Me.Close()
                        End If
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If objDal.Update(objModel) Then
                        If objDal.AddComplaintDetail(Detail) Then
                            msg_Information("Record has been Updated successfully.")
                            SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, txtDocNo.Text, True)
                            ReSetControls()
                            ReSetControls("Detail")
                            Me.Close()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
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
    Private Sub rdoCode_CheckedChanged(sender As Object, e As EventArgs) Handles rdoCode.CheckedChanged
        Try
            If IsFormOpened = False Then Exit Sub
            Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoName_CheckedChanged(sender As Object, e As EventArgs) Handles rdoName.CheckedChanged
        If IsFormOpened = False Then Exit Sub
        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If btnPost.Text = "Post" Then
                Post = True
            Else
                Post = False
            End If
            If IsValidate() = True Then
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                objModel.StockTransId = StockTransId(Me.txtDocNo.Text)
                If objDal.Update(objModel) Then
                    If objDal.AddComplaintDetail(Detail) Then
                        msg_Information("Record has been Updated successfully.")
                        SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, txtDocNo.Text, True)
                        ReSetControls()
                        Me.Close()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_Leave(sender As Object, e As EventArgs) Handles cmbItem.Leave
        Try
            FillCombos("ArticlePack")
            If Val(Me.txtQuantity.Text) <= 0 Then
                Me.txtQuantity.Text = 1
                Me.txtTotalQuantity.Text = 1
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try
            If cmbUnit.Text = "Loose" Then
                Me.txtPackQuantity.Enabled = False
                Me.txtPackQuantity.Text = 1
            Else
                Me.txtPackQuantity.Enabled = True
                Me.txtPackQuantity.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtQuantity.TextChanged, txtPackQuantity.TextChanged
        Try
            If Val(Me.txtPackQuantity.Text) > 0 AndAlso Val(Me.txtQuantity.Text) > 0 Then
                Me.txtTotalQuantity.Text = Math.Round(Val(Me.txtPackQuantity.Text) * Val(Me.txtQuantity.Text), TotalAmountRounding)
            Else
                Me.txtTotalQuantity.Text = Math.Round(Val(Me.txtQuantity.Text), TotalAmountRounding)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
    Private Sub txtContactNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtContactNo.KeyPress, txtQuantity.KeyPress, txtPackQuantity.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            AddRptParam("@ID", ComplainRequestId)
            ShowReport("ComplaintRequest")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellValueChanged
        ''TASK TFS4852 
        If grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            If e.Column.Key = "PackQty" Then
                If grd.GetRow.Cells("Unit").Value.ToString = "Loose" Then
                    grd.CancelCurrentEdit()
                End If
            End If
        End If
        ''END TASK TFS4852
    End Sub
    ''' <summary>
    '''TASK TFS4880
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Complaint Request"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class