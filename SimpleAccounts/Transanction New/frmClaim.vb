'Task No 2638 Rikshaw Claim Record for FRMCLAIM
''27-May-2014 TASK:2660 Imran Ali Claim Account Configuration
''12-June-2014 TASK:2684 Imran Ali Outward Gatepass On Warranty Claim
'26-06-2015 Task# 201506027 Allow Equal Qty in Grid Ali Ansari 
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Net

Public Class frmClaim

    Implements IGeneral
    Dim WarrantyClaim As ClaimBE
    Dim WarrantyClaimID As Integer = 0I
    Dim IsFormOpened As Boolean = False
    Public MyCompanyId As Integer = 0I
    Dim flgCompanyRights As Boolean = False
    Dim strGetAllRecordStatus As String = String.Empty
    Dim IsEditMode As Boolean = False
    Dim GLAccountArticleDepartment As Boolean
    Dim OtherLossAccountId As Integer = 0
    Dim CGSAccountId As Integer = 0I
    Dim PurchaseAccountId As Integer = 0I
    Dim TaxAccountId As Integer = 0I
    Dim Mode As String = "Normal" 'Task#201506027 Added to enforce qty check Ali Ansari
    Enum enmGrdDetail
        LocationId
        ArticleId
        Item
        Code
        Size
        Color
        Unit
        PackQty
        Qty
        Price
        Total
        Current_Price
        Tax_Percent
        Comments
        Engine_No
        Chassis_No
        WarrantyAble
        AccountId
        PackDesc
        Delete
    End Enum
    Enum Customer
        Id
        Name
        State
        City
        Territory
        ExpiryDate
        Discount
        Other_Exp
        Fuel
        CNG
        TransitInsurance
        Credit_Limit
        Type
        Email
        PhoneNo
        SubSubTitle
    End Enum
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                If Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Price AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Qty AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Tax_Percent AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Engine_No AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Chassis_No AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Comments AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.LocationId AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.WarrantyAble Then
                    Me.grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            'Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnLoadAll.Enabled = True
                Me.tsbTask.Enabled = True
                Me.tsbConfig.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDetailAccount)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                Me.btnLoadAll.Enabled = False
                Me.tsbTask.Enabled = False
                Me.tsbConfig.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Load All" Then
                        Me.btnLoadAll.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Task" Then
                        Me.tsbTask.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Config" Then
                        Me.tsbConfig.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Post" Then
                        Me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            WarrantyClaim = New ClaimBE
            WarrantyClaim.DocId = Me.grdSaved.GetRow.Cells("DocId").Value
            WarrantyClaim.DocNo = Me.grdSaved.GetRow.Cells("DocNo").Value
            WarrantyClaim.DocDate = Me.grdSaved.GetRow.Cells("DocDate").Value
            If New ClaimDAL().DeleteRecord(WarrantyClaim) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty
            If Condition = "Company" Then
                'If getConfigValueByType("UserwiseCompany").ToString = "True" Then
                '    strSQL = "Select CompanyId, CompanyName From CompanyDefTable where  CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")"
                'Else
                '    strSQL = "Select CompanyId, CompanyName From CompanyDefTable"
                'End If
                strSQL = "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ") " _
                & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable WHERE CompanyName <> '' " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")" _
                & "Else " _
                & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
                FillDropDown(Me.cmbCompany, strSQL, False)
            ElseIf Condition = "Project" Then
                FillDropDown(Me.cmbProject, "Select CostCenterId, Name as CostCenter From tblDefCostCenter")
            ElseIf Condition = "Category" Then
                'Marked Against Task#201506027 Add ALlow Minus Stock in Category Combo for checking avalable qty Ali Ansari
                'FillDropDown(Me.cmbCategory, "Select Location_Id, Location_Code From tblDefLocation ORDER BY 2 ASC", False)
                'Marked Against Task#201506027 Add ALlow Minus Stock in Category Combo for checking avalable qty Ali Ansari
                'Altered Against Task#201506027 Add ALlow Minus Stock in Category Combo for checking avalable qty Ali Ansari
                'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
                '    strSQL = "Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")  order by sort_order"
                'Else
                '    strSQL = "Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                'End If
                strSQL = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                & " Else " _
                & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                FillDropDown(Me.cmbCategory, strSQL, False)
                'Altered Against Task#201506027 Add ALlow Minus Stock in Category Combo for checking avalable qty Ali Ansari
            ElseIf Condition = "Item" Then
                'Marked Against Task#201506027 Add ALlow Service Item Field in Item Combo for checking avalable qty Ali Ansari
                'Dim Str As String = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ISNULL(PurchasePrice,0) as Price, ArticleDefView.SortOrder, ISNull(ArticleDefView.SubSubId,0) as AccountId FROM  ArticleDefView where Active=1 "
                'Marked Against Task#201506027 Add ALlow Service Item Field in Item Combo for checking avalable qty Ali Ansari
                'Altered Against Task#201506027 Add ALlow Service Item Field in Item Combo for checking avalable qty Ali Ansari
                Dim Str As String = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item,   ArticleSizeName as Size, ArticleColorName as Combination, ISNULL(PurchasePrice,0) as Price,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model,ArticleDefView.SortOrder, ISNull(ArticleDefView.SubSubId,0) as AccountId,Isnull(ServiceItem,0) as ServiceItem FROM  ArticleDefView where Active=1 "
                'Altered Against Task#201506027 Add ALlow Service Item Field in Item Combo for checking avalable qty Ali Ansari
                If flgCompanyRights = True Then
                    If Me.cmbCompany.SelectedIndex > 0 Then
                        Str += " AND ArticleDefView.CompanyId=" & Me.cmbCompany.SelectedValue
                    End If
                End If
                If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                    If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                        Str += " AND ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                    End If
                End If
                If ItemSortOrder = True Then
                    Str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByCode = True Then
                    Str += " ORDER BY ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByName = True Then
                    Str += " ORDER BY ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                Else
                    Str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                End If
                FillUltraDropDown(Me.cmbItem, Str)
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "ArticlePack" Then
                If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
                Me.cmbUnit.ValueMember = "ArticlePackId"
                Me.cmbUnit.DisplayMember = "PackName"
                Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
            ElseIf Condition = "Vendor" Then
                Dim str As String = String.Empty
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                        "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel, tblCustomer.CNG as CNG , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone, vwCOADetail.Sub_Sub_Title " & _
                                        "FROM  tblCustomer LEFT OUTER JOIN " & _
                                        "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                        "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                        "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                        "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                        "WHERE (vwCOADetail.account_type in( 'Customer','Vendor' )) and  vwCOADetail.detail_title is not  null "
                    'End Task:2373
                    If flgCompanyRights = True Then
                        str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                    End If
                Else

                    str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                       "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.CNG as CNG , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone, vwCOADetail.Sub_Sub_Title " & _
                                       "FROM         tblCustomer LEFT OUTER JOIN " & _
                                       "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                       "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                       "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                       "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                       "WHERE (vwCOADetail.account_type='Customer') and  vwCOADetail.detail_title is not  null "
                    'End Task:2373
                    If flgCompanyRights = True Then
                        str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                    End If
                End If
                If IsEditMode = False Then
                    str += " AND vwCOADetail.Active=1"
                Else
                    str += " AND vwCOADetail.Active in(0,1,NULL)"
                End If
                str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title "
                FillUltraDropDown(cmbVendor, str)
                Me.cmbVendor.Rows(0).Activate()
                If cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = False
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                    ' CNG Changes
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.CNG).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Type).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
                    'Task:2373 Column Formating
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.SubSubTitle).Header.Caption = "Ac Head"
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.SubSubTitle).Width = 120
                    'End Task:2373
                End If
            ElseIf Condition = "grdLocation" Then
                Dim strQuery As String = String.Empty
                'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
                '    strQuery = "Select Location_Id, Location_Code From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") ORDER BY 2 ASC"
                'Else
                '    strQuery = "Select Location_Id, Location_Code From tblDefLocation ORDER BY 2 ASC"
                'End If
                strSQL = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
              & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
              & " Else " _
              & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

                Dim objdtLocation As New DataTable
                objdtLocation = GetDataTable(strSQL)
                Me.grd.RootTable.Columns("LocationId").HasValueList = True
                Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(objdtLocation.DefaultView, "Location_Id", "Location_Code")
            ElseIf Condition = "DeliveryChalan" Then
                FillUltraDropDown(Me.cmbDeliveryChalan, "Select DeliveryID, DeliveryNo,DeliveryDate From DeliveryChalanMasterTable WHERE CustomerCode=" & Me.cmbVendor.Value & " Order By DeliveryNo DESC")
                Me.cmbDeliveryChalan.Rows(0).Activate()
                Me.cmbDeliveryChalan.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            WarrantyClaim = New ClaimBE
            With WarrantyClaim
                .DocId = WarrantyClaimID
                .DocNo = Me.txtDocumentNo.Text
                .DocDate = Me.dtpDocumentDate.Value
                .Post = Me.chkPost.Checked
                .CustomerCode = Me.cmbVendor.Value
                .LocationId = IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue)
                .ProjectId = IIf(Me.cmbProject.SelectedIndex > 0, Me.cmbProject.SelectedValue, 0)
                .Remarks = Me.txtRemarks.Text
                .Adjustment = 0
                .EntryDate = Now
                .UserId = LoginUserId
                .OtherLossAccountId = OtherLossAccountId
                .CGSAccountId = CGSAccountId
                .TaxAccountId = TaxAccountId
                .DeliveryID = Me.cmbDeliveryChalan.Value
                .WarrantyClaimDetail = New List(Of WarrantyClaimDetailBE)
                Dim WarrantyClaimDt As WarrantyClaimDetailBE
                Me.grd.UpdateData()
                For Each objRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    WarrantyClaimDt = New WarrantyClaimDetailBE
                    With WarrantyClaimDt
                        .ArticleDefId = Val(objRow.Cells(enmGrdDetail.ArticleId).Value.ToString)
                        .ArticleDescription = objRow.Cells(enmGrdDetail.Item).Value.ToString
                        .ArticleSize = objRow.Cells(enmGrdDetail.Unit).Value.ToString
                        .Chassis_No = objRow.Cells(enmGrdDetail.Chassis_No).Value.ToString
                        .Engine_No = objRow.Cells(enmGrdDetail.Engine_No).Value.ToString
                        .Comments = objRow.Cells(enmGrdDetail.Comments).Value.ToString
                        .Current_Price = Val(objRow.Cells(enmGrdDetail.Current_Price).Value.ToString)
                        .DocDetailId = 0
                        .DocId = WarrantyClaimID
                        .LocationId = Val(objRow.Cells(enmGrdDetail.LocationId).Value.ToString)
                        .Price = Val(objRow.Cells(enmGrdDetail.Price).Value.ToString)
                        .PurchaseAccountId = Val(objRow.Cells(enmGrdDetail.AccountId).Value.ToString)
                        .Sz1 = Val(objRow.Cells(enmGrdDetail.Qty).Value.ToString)
                        .Sz7 = Val(objRow.Cells(enmGrdDetail.PackQty).Value.ToString)
                        .Qty = IIf(objRow.Cells(enmGrdDetail.Unit).Value.ToString <> "Loose", Val(objRow.Cells(enmGrdDetail.PackQty).Value.ToString) * Val(objRow.Cells(enmGrdDetail.Qty).Value.ToString), Val(objRow.Cells(enmGrdDetail.Qty).Value.ToString))
                        .Price = Val(objRow.Cells(enmGrdDetail.Price).Value.ToString)
                        .Current_Price = Val(objRow.Cells(enmGrdDetail.Current_Price).Value.ToString)
                        .Tax_Percent = Val(objRow.Cells(enmGrdDetail.Tax_Percent).Value.ToString)
                        .WarrantyAble = objRow.Cells(enmGrdDetail.WarrantyAble).Value
                        .PackDesc = objRow.Cells(enmGrdDetail.PackDesc).Value.ToString
                    End With
                    .WarrantyClaimDetail.Add(WarrantyClaimDt)
                Next
            End With


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim objDt As New DataTable
            If Condition = "Master" Then
                objDt = New ClaimDAL().GetAllRecord(IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue), strGetAllRecordStatus)
                Me.grdSaved.DataSource = objDt
                Me.grdSaved.RetrieveStructure()
                Me.grdSaved.RootTable.Columns("DocId").Visible = False
                Me.grdSaved.RootTable.Columns("ProjectId").Visible = False
                Me.grdSaved.RootTable.Columns("LocationId").Visible = False
                Me.grdSaved.RootTable.Columns("CustomerCode").Visible = False
                Me.grdSaved.RootTable.Columns("DeliveryID").Visible = False
                Me.grdSaved.RootTable.Columns("DocDate").FormatString = "dd/MMM/yyyy"
                Me.grdSaved.RootTable.Columns("DeliveryDate").FormatString = "dd/MMM/yyyy"
                grdSaved.AutoSizeColumns()
            ElseIf Condition = "Detail" Then
                objDt.Clear()
                objDt.Columns.Clear()
                objDt = New ClaimDAL().GetDetailRecord(WarrantyClaimID)
                objDt.Columns(enmGrdDetail.Total).Expression = "IIF(Unit='Loose',(Qty*Price),((PackQty*Qty)*Price))"
                Me.grd.DataSource = objDt
                FillCombos("grdLocation")
                ApplyGridSettings()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.dtpDocumentDate.Value = Date.MinValue Then
                ShowErrorMessage("Invliad date.")
                Me.dtpDocumentDate.Focus()
                Return False
            End If
            If Me.txtDocumentNo.Text = String.Empty Then
                ShowErrorMessage("Define document no.")
                Me.txtDocumentNo.Focus()
                Return False
            End If
            If Me.grd.RowCount = 0 Then
                ShowErrorMessage("Record not in grid.")
                Me.grd.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            Me.btnDelete.Visible = False
            Me.btnLoadAll.Visible = False
            Me.btnRefresh.Visible = True
            IsEditMode = False
            Me.WarrantyClaimID = 0I
            Me.dtpDocumentDate.Value = Date.Now
            Dim Prefix As String = String.Empty
            Prefix = "WC-" & Microsoft.VisualBasic.Right(Me.dtpDocumentDate.Value.Year, 2) & "-"
            Me.txtDocumentNo.Text = New ClaimDAL().GetSerialNo(Prefix)
            Me.txtRemarks.Text = String.Empty
            Me.chkPost.Checked = True
            If Not Me.cmbCompany.SelectedIndex = -1 Then Me.cmbCompany.SelectedIndex = 0
            If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedIndex = 0
            If Not Me.cmbCategory.SelectedIndex = -1 Then Me.cmbCategory.SelectedIndex = 0
            If Not Me.cmbItem.ActiveRow Is Nothing Then Me.cmbItem.Rows(0).Activate()
            If Not Me.cmbVendor.ActiveRow Is Nothing Then Me.cmbVendor.Rows(0).Activate()
            FillCombos("DeliveryChalan")
            Me.txtPQty.Text = String.Empty
            Me.txtQty.Text = String.Empty
            Me.txtPrice.Text = String.Empty
            Me.txtAmount.Text = String.Empty
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.dtpDocumentDate.Focus()
            GetAllRecords("Master")
            GetAllRecords("Detail")
            ApplySecurity(EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New ClaimDAL().SaveRecord(WarrantyClaim) = True Then
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
            If New ClaimDAL().UpdateRecord(WarrantyClaim) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmClaim_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                If Me.btnSave.Enabled = True Then
                    Me.btnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.F2 Then
                Me.btnEdit_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Delete Then
                If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                    Me.btnDelete_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                If Not Me.grdSaved.RecordCount > 0 Then Exit Sub
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmClaim_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try

            If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
                GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
            Else
                GLAccountArticleDepartment = False
            End If
            If Not getConfigValueByType("PurchaseDebitAccount").ToString = "Error" Then
                PurchaseAccountId = Val(getConfigValueByType("PurchaseDebitAccount"))
            Else
                PurchaseAccountId = 0
            End If
            If Not getConfigValueByType("CGSAccountId").ToString = "Error" Then
                CGSAccountId = Val(getConfigValueByType("CGSAccountId"))
            Else
                CGSAccountId = 0
            End If
            'Task:2660 Get Claim Account Id
            If Not getConfigValueByType("ClaimAccountId").ToString = "Error" Then
                OtherLossAccountId = Val(getConfigValueByType("ClaimAccountId"))
            Else
                OtherLossAccountId = 0
            End If
            'End Task:2660
            If Not getConfigValueByType("SalesTaxCreditAccount").ToString = "Error" Then
                TaxAccountId = Val(getConfigValueByType("SalesTaxCreditAccount"))
            Else
                TaxAccountId = 0
            End If
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            FillCombos("Company")
            FillCombos("Project")
            FillCombos("Vendor")
            'FillCombos("DeliveryChalan")
            FillCombos("Category")
            FillCombos("Item")

            IsFormOpened = True
            ReSetControls()
            Get_All(frmModProperty.Tags)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
            If frmModProperty.Tags.Length > 0 Then frmModProperty.Tags = String.Empty ''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
        End Try

    End Sub

    Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Enter
        Try
            Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        Try
            If IsFormOpened = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            FillCombos("ArticlePack")

            Me.txtPrice.Text = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            'Altered agaisnt task#201506027 Add Stock value in form to check available qty Ali Ansari
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            'Altered agaisnt task#201506027 Add Stock value in form to check available qty Ali Ansari
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsDateLock(Me.dtpDocumentDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    Me.lblProgress.Text = "Loading Please Wait ..."
                    Me.lblProgress.BackColor = Color.LightYellow
                    Me.lblProgress.Visible = True
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()

                    If Save() = True Then
                        ReSetControls()
                        Me.lblProgress.Visible = False
                        Me.Cursor = Cursors.Default
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    Me.lblProgress.Text = "Loading Please Wait ..."
                    Me.lblProgress.BackColor = Color.LightYellow
                    Me.lblProgress.Visible = True
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    If Update1() = True Then
                        ReSetControls()
                        Me.lblProgress.Visible = False
                        Me.Cursor = Cursors.Default
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If IsDateLock(Me.dtpDocumentDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
            End If
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If Delete() = True Then
                ReSetControls()
                Me.lblProgress.Visible = False
                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecords(Optional ByVal Condition As String = "")
        Try

            If Not Me.grdSaved.RecordCount > 0 Then Exit Sub
            If Me.grd.RecordCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.btnSave.Text = "&Update"
            Me.btnDelete.Enabled = True
            IsEditMode = True
            WarrantyClaimID = Me.grdSaved.GetRow.Cells("DocId").Value
            Me.txtDocumentNo.Text = Me.grdSaved.GetRow.Cells("DocNo").Value.ToString
            Me.dtpDocumentDate.Value = Me.grdSaved.GetRow.Cells("DocDate").Value
            Me.chkPost.Checked = Me.grdSaved.GetRow.Cells("Post").Value
            Me.cmbProject.SelectedValue = Me.grdSaved.GetRow.Cells("ProjectId").Value
            Me.cmbVendor.Value = Me.grdSaved.GetRow.Cells("CustomerCode").Value
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString
            Me.cmbDeliveryChalan.Value = Val(Me.grdSaved.GetRow.Cells("DeliveryID").Value.ToString)
            GetAllRecords("Detail")
            ApplySecurity(EnumDataMode.Edit)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
                GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
            Else
                GLAccountArticleDepartment = False
            End If
            If Not getConfigValueByType("PurchaseDebitAccount").ToString = "Error" Then
                PurchaseAccountId = Val(getConfigValueByType("PurchaseDebitAccount"))
            Else
                PurchaseAccountId = 0
            End If
            If Not getConfigValueByType("CGSAccountId").ToString = "Error" Then
                CGSAccountId = Val(getConfigValueByType("CGSAccountId"))
            Else
                CGSAccountId = 0
            End If
            'Task:2660 Get Claim Account Id
            If Not getConfigValueByType("ClaimAccountId").ToString = "Error" Then
                OtherLossAccountId = Val(getConfigValueByType("ClaimAccountId"))
            Else
                OtherLossAccountId = 0
            End If
            'End Task:2660
            If Not getConfigValueByType("SalesTaxCreditAccount").ToString = "Error" Then
                TaxAccountId = Val(getConfigValueByType("SalesTaxCreditAccount"))
            Else
                TaxAccountId = 0
            End If
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            Dim id As Integer = 0I
            id = Me.cmbCompany.SelectedIndex
            FillCombos("Company")
            Me.cmbCompany.SelectedIndex = id
            id = Me.cmbProject.SelectedIndex
            FillCombos("Project")
            Me.cmbProject.SelectedIndex = id
            id = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombos("Vendor")
            Me.cmbVendor.Value = id
            id = Me.cmbCategory.SelectedIndex
            FillCombos("Category")
            Me.cmbCategory.SelectedIndex = id
            id = Me.cmbItem.ActiveRow.Cells(0).Value
            FillCombos("Item")
            Me.cmbItem.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbItem.ActiveRow.Cells(0).Value = 0 Then
                ShowErrorMessage("Please select item.")
                Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
                Me.cmbItem.Focus()
                Exit Sub
            End If
            If Val(Me.txtQty.Text) = 0 Then
                ShowErrorMessage("Please enter qty.")
                Me.txtQty.Focus()
                Exit Sub
            End If


            ''''''''''''''
            'Altered agaisnt task#201506027 to allow equal qty to grid Ali Ansari
            Dim IsMinus As Boolean = True
            If Not IsDBNull(CType(Me.cmbItem.SelectedRow, Infragistics.Win.UltraWinGrid.UltraGridRow).Cells("ServiceItem").Value) Then
                If CType(Me.cmbItem.SelectedRow, Infragistics.Win.UltraWinGrid.UltraGridRow).Cells("ServiceItem").Value = False Then
                    IsMinus = getConfigValueByType("AllowMinusStock")
                End If
            End If
            If Mode = "Normal" Then
                If Not IsMinus = True Then
                    If Val(Me.txtStock.Text) - IIf(Val(txtPQty.Text) = 0, 1, Val(txtPQty.Text)) * Val(txtQty.Text) < 0 Then
                        msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
                        cmbItem.Focus()
                        Exit Sub
                    End If
                End If
                If CType(Me.cmbCategory.SelectedItem, DataRowView).Row.Item("AllowMinusStock").ToString = "False" AndAlso IsMinus = True Then
                    If Val(Me.txtStock.Text) - IIf(Val(txtPQty.Text) = 0, 1, Val(txtPQty.Text)) * Val(txtQty.Text) < 0 Then
                        msg_Error(Me.cmbItem.ActiveRow.Cells("Item").Value.ToString & str_ErrorStockNotEnough)
                        cmbItem.Focus()
                        Exit Sub
                    End If
                End If
            End If
            'Altered agaisnt task#201506027 to allow equal qty to grid Ali Ansari

            '''''''''''''''''''


            Me.grd.UpdateData()
            Dim objdt As New DataTable
            objdt = CType(Me.grd.DataSource, DataTable)
            Dim objDr As DataRow
            objDr = objdt.NewRow
            objDr(enmGrdDetail.LocationId) = Me.cmbCategory.SelectedValue
            objDr(enmGrdDetail.ArticleId) = Me.cmbItem.Value
            objDr(enmGrdDetail.Item) = Me.cmbItem.ActiveRow.Cells("Item").Value.ToString
            objDr(enmGrdDetail.Code) = Me.cmbItem.ActiveRow.Cells("Code").Value.ToString
            objDr(enmGrdDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value.ToString
            objDr(enmGrdDetail.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value.ToString
            If Not Me.cmbUnit.Text = "Loose" Then
                objDr(enmGrdDetail.Unit) = "Pack"
            Else
                objDr(enmGrdDetail.Unit) = "Loose"
            End If
            objDr(enmGrdDetail.PackQty) = Val(Me.txtPQty.Text)
            objDr(enmGrdDetail.Qty) = Val(Me.txtQty.Text)
            objDr(enmGrdDetail.Price) = Val(Me.txtPrice.Text)
            objDr(enmGrdDetail.Current_Price) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            objDr(enmGrdDetail.Comments) = String.Empty
            objDr(enmGrdDetail.Engine_No) = String.Empty
            objDr(enmGrdDetail.Chassis_No) = String.Empty
            objDr(enmGrdDetail.WarrantyAble) = False
            objDr(enmGrdDetail.Tax_Percent) = 0
            objDr(enmGrdDetail.PackDesc) = Me.cmbUnit.Text
            If GLAccountArticleDepartment = True Then
                objDr(enmGrdDetail.AccountId) = Val(Me.cmbItem.ActiveRow.Cells("AccountId").Value.ToString)
            Else
                objDr(enmGrdDetail.AccountId) = PurchaseAccountId
            End If
            objdt.Rows.Add(objDr)
            objdt.AcceptChanges()
            ClearAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompany.SelectedIndexChanged
        Try
            If IsFormOpened = True Then GetAllRecords("Master")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoadAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
        Try
            If IsFormOpened = True Then
                strGetAllRecordStatus = "All"
                GetAllRecords("Master")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            strGetAllRecordStatus = String.Empty
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IsFormOpened = False Then Exit Sub
            If Me.UltraTabControl1.SelectedTab.Index = 0 Then
                Me.CtrlGrdBar1.MyGrid = Me.grd
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
                CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers

            Else
                Me.CtrlGrdBar1.MyGrid = Me.grdSaved
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                    Me.grdSaved.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
                CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers

            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If IsFormOpened = True Then
                If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                    Me.btnRefresh.Visible = False
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = True
                    Me.btnLoadAll.Visible = True
                Else
                    If Me.btnSave.Text = "&Update" Then
                        Me.btnDelete.Visible = True
                    Else
                        Me.btnDelete.Visible = False
                    End If
                    Me.btnLoadAll.Visible = False
                    Me.btnRefresh.Visible = True
                    Me.btnSave.Visible = True
                End If
                CtrlGrdBar1_Load(Nothing, Nothing)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try
            If IsFormOpened = True Then
                If Me.cmbUnit.Text <> "Loose" Then
                    Me.txtPQty.Enabled = True
                    Dim dr As DataRowView = CType(Me.cmbUnit.SelectedItem, DataRowView)
                    If dr IsNot Nothing Then
                        Me.txtPQty.Text = Val(dr(2).ToString)
                    End If
                Else
                    Me.txtPQty.Enabled = False
                    Me.txtPQty.Text = 1
                End If
            End If
            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrice_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice.TextChanged
        Try
            If IsFormOpened = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbUnit.Text = "Loose" Then
                Me.txtAmount.Text = (Val(Me.txtQty.Text) * Val(Me.txtPrice.Text))
            Else
                Me.txtAmount.Text = ((Val(Me.txtQty.Text) * Val(Me.txtPQty.Text)) * Val(Me.txtPrice.Text))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Try
            If IsFormOpened = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbUnit.Text = "Loose" Then
                Me.txtAmount.Text = (Val(Me.txtQty.Text) * Val(Me.txtPrice.Text))
            Else
                Me.txtAmount.Text = ((Val(Me.txtQty.Text) * Val(Me.txtPQty.Text)) * Val(Me.txtPrice.Text))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPQty.TextChanged
        Try
            If IsFormOpened = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbUnit.Text = "Loose" Then
                Me.txtAmount.Text = (Val(Me.txtQty.Text) * Val(Me.txtPrice.Text))
            Else
                Me.txtAmount.Text = ((Val(Me.txtQty.Text) * Val(Me.txtPQty.Text)) * Val(Me.txtPrice.Text))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ClearAll(Optional ByVal Condition As String = "")
        Try

            Me.txtPQty.Text = String.Empty
            Me.txtQty.Text = String.Empty
            Me.txtPrice.Text = String.Empty
            Me.txtAmount.Text = String.Empty
            Me.cmbItem.Focus()
            Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbCode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbCode.CheckedChanged
        Try
            If IsFormOpened = True Then
                If Me.rbCode.Checked = True Then Me.cmbItem.DisplayMember = "Code"
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbName_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbName.CheckedChanged
        Try
            If IsFormOpened = True Then
                If Me.rbCode.Checked = True Then Me.cmbItem.DisplayMember = "Code" Else Me.cmbItem.DisplayMember = "Item"
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            'Ahmad Sharif: Remove Not from condition, 06-06-2015
            'If Not msg_Confirm("Do you want to delete this row.") = True Then
            If msg_Confirm("Do you want to delete this row.") = True Then
                Me.grd.GetRow.Delete()
                Me.grd.UpdateData()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.ButtonClick
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@DocId", Me.grdSaved.GetRow.Cells("DocId").Value)
            ShowReport("RptWarentyClaim")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''12-June-2014 TASK:2684 Imran Ali Outward Gatepass On Warranty Claim
    Private Sub OutwardGatepassToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutwardGatepassToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@DocId", Me.grdSaved.GetRow.Cells("DocId").Value)
            ShowReport("rptOutWardWarrentyClaim")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2684
    Public Function Get_All(ByVal DocNo As String)
        Try
            Get_All = Nothing
            If Not DocNo.Length > 0 Then Exit Try
            If IsFormOpened = True Then
                If DocNo.Length > 0 Then
                    '' Task# H08062015  Ahmad Sharif:
                    IsDrillDown = True
                    If Me.grdSaved.RowCount <= 50 Then
                        Me.btnLoadAll_Click(Nothing, Nothing)
                    End If
                    Dim flag As Boolean = False

                    flag = Me.grdSaved.Find(Me.grdSaved.RootTable.Columns("DocNo"), Janus.Windows.GridEX.ConditionOperator.Equal, DocNo, -1, 1)

                    If flag = True Then
                        Me.grdSaved_DoubleClick(Nothing, Nothing)
                    Else
                        Exit Function
                    End If
                    ''End Task# H08062015
                End If
            End If
            'IsDrillDown = False
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub cmbVendor_ValueChanged(sender As Object, e As EventArgs) Handles cmbVendor.ValueChanged
        Try
            If IsFormOpened = True Then FillCombos("DeliveryChalan")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdSaved.GetRow Is Nothing AndAlso grdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmClaim"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdSaved.CurrentRow.Cells(1).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Warranty Claim (" & frmtask.Ref_No & ") "
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
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Accounts
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
