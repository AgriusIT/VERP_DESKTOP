''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
''03-Feb-2014        Task:2406   Imran Ali    FIELD CHOOSER restriction (Senior Rozgar)
''12-Feb-2014    Task:2422 Imran Ali Production entry save without any item
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
'' Task No 2555 Mughees   Amendments to get Unit of Measurement  to get the vaue in cmbUOM
'' 8-May-2014 TASK:2613 Imran Ali Production Entry Problem
''16-June-2014 TASK:2690 Imran Ali Add Department and Employee Fields On Production Entry
''21-Jul-2014 TASK2753 Imran Ali Employee Field On Production Store
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''13-05-2015 Task 20150506 Ali Ansari Add Engine Number & Chasis Number and validation of record
'14-05-2015 Ali Ansari Task#20150503 Filter  CGAccount to expense and asset
''10-June-2015 Task# A2-10-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
''10-June-2015 Task# A1-10-06-2015 Added Key Pres event for some textboxes to take just numeric and dot value
''16-Nov-2015 TASK-TFS-46 Cost Price For Production And Store Issuence.
''TASK-470 Muhammad Ameen on 01-07-2016: Stock Statement Report By Pack
'' TASK : TFS738 Saving record problem whenever parallel users try to save record and sometime wrong document number inserted to voucher . Muhammad Ameen on 17-08-2017
''TASK TFS1427 UserName should be saved in voucher. Ameen on 13-09-2017
''TASK TFS1626 Muhammad Ameen done on 24-10-2017. Record is not displaying against selected ticket and plan.
'' TASK TFS1592 Ayesha Rehman on 19-10-2017 Future date entry should be rights based
'' TASK TFS1616 Ayesha Rehman on 23-10-2017 Add Batch No, Retail Price, Expiry Date on Production Screen
''TFS1596 Lot/Batch wise Stock Management on 30-11-2017
'' TASK TFS1772 Ayesha Rehman on 5-12-2017 Addition of item weight on Production Screen
''TFS4161 Ayesha Rehman : 09-08-2018 : P QTY: (Should Be Static/ Un-Changeable / Un-Editable on All Screens)
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.OleDb
Imports SBDal.StockDAL
Imports SBDal.StockDocTypeDAL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports System.Runtime.InteropServices
Public Class frmProductionStore
    Implements IGeneral
    Dim ProductionMaster As ProductionMaster
    Dim ProductionDetail As ProductionDetail
    Dim StockMaster As StockMaster
    Dim StockDetail As StockDetail
    Dim dtProductions As New DataTable
    Dim MasterId As Integer
    Dim AddItemToGridLoading As Boolean = False
    Dim IsFormLoaded As Boolean = False
    Dim Pack_Qty As Integer
    Dim StockMasterId As Integer
    Dim dtStockTable As DataTable
    Dim blnEditMode As Boolean = False
    Dim StoreIssuence As StoreIssuenceMaster
    Dim StoreIssuenceDetail As StoreIssuenceDetail
    Dim StockDispatch As StockDispatchBE
    Dim StockDispatchDetail As StockDispatchDetailBE
    Dim TotalDispatchQty As Decimal = 0
    Dim TotalDispatchAmount As Decimal = 0
    Dim Email As Email
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim setVoucherNo As String = String.Empty
    Dim setVoucherDate As DateTime
    Dim getVoucher_Id As Integer = 0
    Dim Total_Amount As Double = 0D
    Dim setEditMode As Boolean = False
    Dim crpt As New ReportDocument
    Dim Previouse_Amount As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim flgSelectedProject As Boolean = False
    Dim flgCompanyRights As Boolean = False
    Dim flgLoadAllItems As Boolean = False
    Dim flgLocationWiseItems As Boolean = False
    Dim dtDataEmail As New DataTable
    <VBFixedString(300)> Private strItemDescription As String
    Dim blnUpdateAll As Boolean = False
    Private blnDisplayAll As Boolean = False

    Dim Engine_NoBefore As String = ""
    Dim Chassis_NoBefore As String = ""
    Dim EngineNoFirst As String = ""
    Dim ChassisNoFirst As String = ""
    'Code Edit for task 1592 future date rights Ayesha Rehman
    Dim IsDateChangeAllowed As Boolean = False
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Dim ItemFilterByName As Boolean = False
    Enum EnumGrd
        Location_Id
        ArticleDefId
        Location_Name
        ArticleCode
        ArticleDescription
        ArticleColorName
        ArticleSizeName
        UnitName
        ArticleSize
        PackQty
        Qty
        Dim1
        Dim2
        CurrentRate
        PackPrice
        Total
        Comments
        BatchNo
        'Task No 2555 Append One More Field In Enum
        Uom
        Pack_Desc
        'Task No 1616 Append One More Field In Enum
        RetailPrice
        'Ayesha : TFS1616 : End
        AccountId
        CGSAccountId
        EmployeeId
        ExpiryDate
        EngineNo ' Task 20150506 
        ChasisNo ' Task 20150506 
        PlanDetailId
        MasterId
        TotalQty ''TASK-408 
    End Enum
    Enum EnumGridMaster
        'Production_Id
        'Production_Store
        'Project
        'Production_Date
        'Production_No
        'Location_Name
        'Customer
        'Order_No
        'CostCenter
        'IGPNo
        'Remarks
        'Post
        'IssuedStore
        'RefDocument
        'RefDispatchNo
        'PlanId
        'PlanNo
        'PrintStatus
        'DepartmentId  'Task:2690 Added Index
        'EmployeeID 'Task:2753 Added Index
        'CGSAccountId
        Production_ID
        Production_store
        Project
        Production_date
        Production_no
        location_name
        Customer
        Order_No
        CostCenter
        IGPNo
        Remarks
        Post
        IssuedSTore
        RefDocument
        RefDispatchNo
        PlanId
        PlanNo
        PrintStatus
        DepartmentId
        EmployeeID
        CGSAccountId
        UpdateUserName

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
        Credit_Limit
        Type
    End Enum
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdDetail.RootTable.Columns
                'Marked Agaisnt Task 20150506 Ali Ansari
                'If col.Index <> EnumGrd.Location_Id AndAlso col.Index <> EnumGrd.Qty AndAlso col.Index <> EnumGrd.CurrentRate AndAlso col.Index <> EnumGrd.Comments AndAlso col.Index <> EnumGrd.BatchNo AndAlso col.Index <> EnumGrd.ExpiryDate Then
                'Marked Agaisnt Task 20150506 Ali Ansari
                'Altered Agaisnt Task 20150506 Ali Ansari
                If col.Index <> EnumGrd.Location_Id AndAlso col.Index <> EnumGrd.Qty AndAlso col.Index <> EnumGrd.TotalQty AndAlso col.Index <> EnumGrd.CurrentRate AndAlso col.Index <> EnumGrd.Comments AndAlso col.Index <> EnumGrd.BatchNo AndAlso col.Index <> EnumGrd.ExpiryDate AndAlso col.Index <> EnumGrd.EngineNo AndAlso col.Index <> EnumGrd.ChasisNo Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
                'Altered Agaisnt Task 20150506 Ali Ansari
            Next
            ''Start TFS4161
            If IsPackQtyDisabled = True Then
                Me.grdDetail.RootTable.Columns(EnumGrd.TotalQty).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Else
                Me.grdDetail.RootTable.Columns(EnumGrd.TotalQty).EditType = Janus.Windows.GridEX.EditType.TextBox
            End If
            ''End TFS4161
            Me.grdDetail.RootTable.Columns("Pack_Desc").Position = Me.grdDetail.RootTable.Columns("ArticleSize").Index
            Me.grdDetail.RootTable.Columns("ArticleSize").Position = Me.grdDetail.RootTable.Columns("Pack_Desc").Index

            'Task:2759 Set Rounded Format
            Me.grdDetail.RootTable.Columns(EnumGrd.Total).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGrd.Total).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            'End Task:2759
            'Task:1616 Set Date Format
            Me.grdDetail.RootTable.Columns(EnumGrd.ExpiryDate).FormatString = str_DisplayDateFormat
            Me.grdDetail.RootTable.Columns(EnumGrd.RetailPrice).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGrd.RetailPrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGrd.TotalQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGrd.TotalQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGrd.ExpiryDate).FormatString = str_DisplayDateFormat
            'End Task:1616
            'Task:1772
            Me.grdDetail.RootTable.Columns(EnumGrd.Dim1).Visible = False
            Me.grdDetail.RootTable.Columns(EnumGrd.Dim2).Visible = False
            'End Task:1772
            'Me.grdDetail.AutoSizeColumns()
            Dim StockInConfigration As String = "" ''1596
            ''Start Task 1596
            If Not getConfigValueByType("StockInConfigration").ToString = "Error" Then ''1596
                StockInConfigration = getConfigValueByType("StockInConfigration").ToString
            End If
            'ShowInformationMessage(StockInConfigration)
            If StockInConfigration.Equals("Disabled") Then
                Me.grdDetail.RootTable.Columns(EnumGrd.BatchNo).Visible = False
                Me.grdDetail.RootTable.Columns(EnumGrd.BatchNo).EditType = Janus.Windows.GridEX.EditType.NoEdit
            ElseIf StockInConfigration.Equals("Enabled") Then
                Me.grdDetail.RootTable.Columns(EnumGrd.BatchNo).Visible = True
                Me.grdDetail.RootTable.Columns(EnumGrd.BatchNo).EditType = Janus.Windows.GridEX.EditType.TextBox
            Else
                Me.grdDetail.RootTable.Columns(EnumGrd.BatchNo).Visible = True
                Me.grdDetail.RootTable.Columns(EnumGrd.BatchNo).EditType = Janus.Windows.GridEX.EditType.TextBox
            End If
            ''End Task 1596
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            Call New ProductionDAL().Delete(ProductionMaster)
            'If getConfigValueByType("StoreIssuenceWithProduction").ToString = "True" Then
            '    FillModel("StoreIssuence")
            '    Call New StoreIssuenceDAL().Delete(StoreIssuence)
            'End If
            'If GetConfigValue("StockDispatchOnProduction").ToString = "True" Then
            '    FillModel("Dispatch")
            '    Call New StockDispatchDAL().Delete(StockDispatch)
            'End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty
            If Condition = "ProductionStore" Then
                str = "Select Location_Id, Location_Name From tblDefLocation WHERE (Location_Type = 'Production' OR Location_Type='WIP') "
                FillDropDown(Me.cmbProductionStore, str)
            ElseIf Condition = "CostCenter" Then
                str = "Select CostCenterId, Name as [Cost Center] From tblDefCostCenter"
                If blnEditMode = False Then
                    str += " WHERE Active=1"
                Else
                    str += " WHERE Active IN(1,0,NULL)"
                End If
                str += "ORDER BY Name"
                FillDropDown(Me.cmbCostCenter, str)
            ElseIf Condition = "Location" Then
                'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
                '    str = "Select Location_Id, Location_Name from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") "
                'Else
                '    str = "Select Location_Id, Location_Name from tblDefLocation"
                'End If
                str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                & " Else " _
                & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                FillDropDown(Me.cmbLocation, str, False)
            ElseIf Condition = "Customer" Then
                str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                  "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email " & _
                                                  "FROM         tblCustomer LEFT OUTER JOIN " & _
                                                  "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                  "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                  "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                  "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                  "WHERE (vwCOADetail.account_type='Customer') and vwCOADetail.Active=1 And vwCOADetail.coa_detail_id is not  null "
                If flgCompanyRights = True Then
                    str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                End If

                FillUltraDropDown(cmbVendor, str)
                If cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                    'Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                    'Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
                End If
            ElseIf Condition = "SO" Then
                str = "Select SalesOrderID, SalesOrderNo from SalesOrderMasterTable where salesorderId not in(select PoId from salesMasterTable)"
                If IsFormLoaded = True Then
                    str += " And VendorId=" & Me.cmbVendor.Value & ""
                End If
                FillDropDown(Me.cmbOrderNo, str)

            ElseIf Condition = "Item" Then
                'str = "SP_ProductList"
                'str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], Isnull(SubSubId,0) as AccountId FROM ArticleDefView where Active=1 "
                str = String.Empty
                ''05-Dec-2017  Task:1772    Ayesha Rehman Added Column MultiDimentionalItem
                str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleDefView.ArticleUnitName As UOM,  ArticleColorName as Combination,ArticleDefView.ArticleBrandName As Grade, Isnull(PurchasePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], Isnull(SubSubId,0) as AccountId,  Isnull(MultiDimentionalItem,0) as MultiDimentionalItem, CGSAccountId,ArticleDefView.MasterId FROM ArticleDefView where Active=1 "
                If flgCompanyRights = True Then
                    str += " AND ArticleDefView.CompanyId=" & MyCompanyId
                End If

                If getConfigValueByType("ArticleFilterByLocation").ToString = "True" Then
                    If GetRestrictedItemFlg(Me.cmbLocation.SelectedValue) = True Then
                        str += " AND  ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbLocation.SelectedValue & " AND Restricted=1)"
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

                FillUltraDropDown(Me.cmbItem, str)
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItem.Rows(0).Activate()
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                    'Me.cmbItem.DisplayLayout.Bands(0).Columns("MultiDimentionalItem").Hidden = True
                    If ItemFilterByName = True Then
                        Me.rdoName.Checked = True
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                    Else
                        If rdoName.Checked = True Then
                            Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                        Else
                            Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                        End If
                    End If
                End If
                ''TFS1596
            ElseIf Condition = "BatchNo" Then
                str = "Select  DISTINCT StockTransDetailId, BatchNo From  StockDetailTable  where BatchNo  <> '' "
                If cmbItem.SelectedRow.Index > 0 Then
                    str += " And ArticledefId=" & Me.cmbItem.Value & " And isnull(InQty, 0) - isnull(OutQty, 0) > 0 ORDER BY StockTransDetailId ASC"
                End If
                FillDropDown(Me.cmbBatch, str, False)
                cmbBatch.SelectedIndex = -1
            ElseIf Condition = "TicketWiseItem" Then
                'str = "SP_ProductList"
                'str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], Isnull(SubSubId,0) as AccountId FROM ArticleDefView where Active=1 "
                str = String.Empty
                str = "SELECT ArticleDefView.ArticleId as Id, ArticleDefView.ArticleCode as Code, ArticleDefView.ArticleDescription as Item, ArticleDefView.ArticleSizeName as Size, ArticleDefView.ArticleUnitName As UOM,  ArticleDefView.ArticleColorName as Combination, ArticleDefView.ArticleBrandName As Grade ,Isnull(ArticleDefView.PurchasePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], Isnull(ArticleDefView.SubSubId,0) as AccountId, ArticleDefView.CGSAccountId,ArticleDefView.MasterId FROM ArticleDefView INNER JOIN PlanTicketsDetail ON ArticleDefView.ArticleId = PlanTicketsDetail.ArticleId  where PlanTicketsDetail.PlanTicketsMasterID = " & Me.cmbTicket.SelectedValue & " And ArticleDefView.Active=1 "
                If flgCompanyRights = True Then
                    str += " AND ArticleDefView.CompanyId=" & MyCompanyId
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

                If getConfigValueByType("ArticleFilterByLocation").ToString = "True" Then
                    If GetRestrictedItemFlg(Me.cmbLocation.SelectedValue) = True Then
                        str += " AND  ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbLocation.SelectedValue & " AND Restricted=1)"
                    End If
                End If

                FillUltraDropDown(Me.cmbItem, str)
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                If rdoName.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                End If
            ElseIf Condition = "grdLocation" Then
                'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
                '    str = "Select Location_Id, Location_Name From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")"
                'Else
                '    str = "Select Location_Id, Location_Name From tblDefLocation"
                'End If
                str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                & " Else " _
                & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                'If Me.grdDetail.RowCount = 0 Then Exit Sub
                Dim dt As DataTable = GetDataTable(str)
                Me.grdDetail.RootTable.Columns(EnumGrd.Location_Id).ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
            ElseIf Condition = "IssuedStore" Then
                str = "Select Location_Id, Location_Name From tblDefLocation"
                FillDropDown(Me.cmbIssuedStore, str)
                FillDropDown(Me.cmbDepartment, str)
            ElseIf Condition = "SearchLocation" Then
                str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
                FillDropDown(Me.cmbSearchLocation, str, True)

            ElseIf Condition = "SearchCostCenter" Then
                str = "Select CostCenterID, Name from tblDefCostCenter"
                FillDropDown(Me.cmbSearchCostCenter, str)
                'ElseIf Condition = "Shift" Then
                '    str = "Select ShiftGroupId, ShiftGroupName From ShiftGroupTable "
                '    FillDropDown(Me.cmbShift, str)
            ElseIf Condition = "Plan" Then
                str = "Select PlanId, PlanNo From PlanMasterTable " & IIf(setEditMode = True, "", " WHERE IsNull(ProductionStatus,'Open')='Open'") & " AND PlanId In(Select PlanId From PlanDetailTable Group By PlanId Having SUM(IsNull(Sz1,0)-IsNull(ProducedQty,0)) >0) ORDER BY PlanId DESC"
                FillDropDown(Me.cmbPlan, str)
                '' Task No 2555 Append One Line Of Code To Select The UOM For cmbuom
            ElseIf Condition = "UM" Then
                str = "Select DISTINCT UOM, UOM From ProductionDetailTable WHERE UOM <> '' ORDER BY 1 ASC"
                FillDropDown(Me.CmbUm, str, False)
                '' Task No 1616 Append One Line Of Code To Select The RetailPrice For cmbretailPrice
            ElseIf Condition = "Retail Price" Then
                str = "Select DISTINCT RetailPrice, RetailPrice From ProductionDetailTable WHERE RetailPrice <> '' "
                If cmbItem.SelectedRow.Index > 0 Then
                    str += " AND  ArticledefID=" & Me.cmbItem.Value & " ORDER BY 1 ASC"
                End If
                FillDropDown(Me.cmbRetailPrice, str, False)
            ElseIf Condition = "ArticlePack" Then
                Me.cmbUnit.ValueMember = "ArticlePackId"
                Me.cmbUnit.DisplayMember = "PackName"
                Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
            ElseIf Condition = "Employee" Then
                str = "Select Employee_Id, Employee_Name, Employee_Code From tblDefEmployee ORDER BY Employee_Name ASC"
                ''21-Jul-2014 TASK2753 Imran Ali Employee Field On Production Store
                FillDropDown(Me.cmbEmployee, str)
                'End Task:2753
                'Dim dt As New DataTable
                'dt = GetDataTable(str)
                'Me.grdDetail.RootTable.Columns("EmployeeId").ValueList.PopulateValueList(dt.DefaultView, "Employee_Id", "Employee_Name")
            ElseIf Condition = "CGSAccount" Then
                'Marked Against Task#20150503 Ali Ansari
                'FillUltraDropDown(Me.cmbCGSAccount, "Select coa_detail_id, detail_title as Account, detail_code as Code From vwCOADetail WHERE detail_title <> '' and Account_Type='Expense'")
                'Marked Against Task#20150503 Ali Ansari
                'Altered agaisnt task 20150503 add asset in combo filter
                FillUltraDropDown(Me.cmbCGSAccount, "Select coa_detail_id, detail_title as Account, detail_code as Code From vwCOADetail WHERE main_type in ('Assets','Expense')  and detail_title <> ''")
                'Altered agaisnt task 20150503 add asset in combo filter
                If Me.cmbCGSAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbCGSAccount.Rows(0).Activate()
                    Me.cmbCGSAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            ElseIf Condition = "Ticket" Then
                'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
                'str = "Select Distinct Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join AllocationMaster ON Ticket.PlanTicketsId = AllocationMaster.TicketID Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & ""
                str = "Select PlanTicketsMasterID, BatchNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo FROM PlanTicketsMaster  Where BatchNo <> '' And PlanId = " & Me.cmbPlan.SelectedValue & " " & IIf(setEditMode = False, "And PlanTicketsMasterID Not In (Select IsNull(PlanTicketId, 0) As PlanTicketId From ProductionMasterTable)", "") & " Order By PlanTicketsMasterID DESC"
                FillDropDown(cmbTicket, str)

            ElseIf Condition = "Tickets" Then
                'Str = "Select Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId Where Ticket.PlanId = " & Me.cmbPlan.SelectedValue & " And Ticket.PlanTicketsId Not in (Select PlanTicketId From MaterialEstimation)"
                'str = "Select Distinct Ticket.PlanTicketsId, Ticket.TicketNo + ' ~ ' + Convert(Varchar(12), Ticket.ProductionStartDate, 113) As TicketNo, Ticket.ArticleId, Article.ArticleDescription, Ticket.ProductionStartDate, Ticket.TicketQuantity, Ticket.PlanId, Ticket.PlanDetailId  FROM PlanTickets Ticket Join AllocationMaster ON Ticket.PlanTicketsId = AllocationMaster.TicketID Join ArticleDefTable Article On Ticket.ArticleId = Article.ArticleId "
                str = "Select PlanTicketsMasterID, BatchNo + ' ~ ' + Convert(Varchar(12), TicketDate, 113) As TicketNo FROM PlanTicketsMaster Where PlanTicketsMasterID Not In (Select IsNull(PlanTicketId, 0) As PlanTicketId From ProductionMasterTable) And BatchNo <> '' Order By PlanTicketsMasterID DESC"
                FillDropDown(cmbTicket, str)
            ElseIf Condition = "PlanItems" Then
                'str = "SP_ProductList"
                'str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], Isnull(SubSubId,0) as AccountId FROM ArticleDefView where Active=1 "
                str = String.Empty
                ''05-Dec-2017  Task:1772    Ayesha Rehman Added Column MultiDimentionalItem
                str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleDefView.ArticleUnitName As UOM,  ArticleColorName as Combination, Isnull(PurchasePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], Isnull(SubSubId,0) as AccountId,  Isnull(MultiDimentionalItem,0) as MultiDimentionalItem, CGSAccountId,ArticleDefView.MasterId FROM ArticleDefView where Active=1 "
                If flgCompanyRights = True Then
                    str += " AND ArticleDefView.CompanyId=" & MyCompanyId
                End If

                If Me.cmbPlan.SelectedIndex > 0 Then
                    str += " AND  ArticleId In (SELECT ArticleDefId FROM PlanDetailTable INNER JOIN PlanMasterTable ON PlanDetailTable.PlanId = PlanMasterTable.PlanId WHERE PlanMasterTable.PlanId = " & Me.cmbPlan.SelectedValue & ") "
                End If

                If getConfigValueByType("ArticleFilterByLocation").ToString = "True" Then
                    If GetRestrictedItemFlg(Me.cmbLocation.SelectedValue) = True Then
                        str += " AND  ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbLocation.SelectedValue & " AND Restricted=1)"
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

                FillUltraDropDown(Me.cmbItem, str)
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItem.Rows(0).Activate()
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                    'Me.cmbItem.DisplayLayout.Bands(0).Columns("MultiDimentionalItem").Hidden = True
                    If rdoName.Checked = True Then
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                    Else
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                    End If
                End If



            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            If Me.BtnSave.Text = "&Save" Then
                If Me.chkPost.Visible = False Then
                    Me.chkPost.Checked = False
                End If
            End If

            Me.grdDetail.UpdateData() 'Task:2612 Set Detail Grid Update
            Dim strSQL As String = String.Empty
            If Condition = String.Empty Then
                ProductionMaster = New ProductionMaster
                ProductionMaster.ProductionId = MasterId
                ProductionMaster.Production_No = Me.txtProductionNo.Text
                ''TASK: TFS738
                'ProductionMaster.Production_No = GetDocumentNo()
                ''END TASK : TFS738
                ProductionMaster.Production_Date = Me.dtpProductionDate.Value.Date
                ProductionMaster.Production_Store = Me.cmbProductionStore.SelectedValue
                ProductionMaster.Project = Me.cmbCostCenter.SelectedValue
                ProductionMaster.CustomerCode = Me.cmbVendor.ActiveRow.Cells(Customer.Id).Value
                ProductionMaster.Order_No = IIf(Me.cmbOrderNo.SelectedIndex - 1, 0, Me.cmbOrderNo.SelectedValue)
                ProductionMaster.TotalQty = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)
                ProductionMaster.TotalAmount = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
                ProductionMaster.IGPNo = Me.txtIGPNo.Text.ToString
                ProductionMaster.Remarks = Me.txtRemarks.Text
                ProductionMaster.UserName = LoginUserName
                ProductionMaster.FDate = Date.Now
                ProductionMaster.Post = IIf(Me.chkPost.Checked = True, 1, 0)
                'If BtnSave.Text = "&Save" Then
                '    ProductionMaster.RefDocument = IIf(Me.txtStoreIssuenceNo.Text = String.Empty, GetDocumentNo1, Me.txtStoreIssuenceNo.Text)
                'Else
                '    ProductionMaster.RefDocument = Me.txtStoreIssuenceNo.Text
                'End If
                'ProductionMaster.RefDispatchNo = IIf(Me.txtDispatchNo.Text = String.Empty, GetDocumentNo2, Me.txtDispatchNo.Text)
                ProductionMaster.RefDocument = String.Empty
                ProductionMaster.RefDispatchNo = String.Empty
                ProductionMaster.IssuedStore = IIf(Me.cmbIssuedStore.Visible = True, Me.cmbIssuedStore.SelectedValue, 0)
                ProductionMaster.PlanTicketId = IIf(Me.cmbTicket.SelectedIndex = -1, 0, Me.cmbTicket.SelectedValue)
                ProductionMaster.EmployeeID = IIf(Me.cmbEmployee.SelectedIndex = -1, 0, Me.cmbEmployee.SelectedValue)
                ProductionMaster.ProductionDetail = New List(Of ProductionDetail)
                ProductionMaster.StockMaster = New StockMaster
                ProductionMaster.StockMaster.StockTransId = Convert.ToInt32(GetStockTransId(Me.txtProductionNo.Text).ToString)
                ProductionMaster.StockMaster.DocNo = Me.txtProductionNo.Text
                ProductionMaster.StockMaster.DocDate = Me.dtpProductionDate.Value.Date
                ProductionMaster.StockMaster.DocType = Convert.ToInt32(GetStockDocTypeId("Production"))
                ProductionMaster.StockMaster.Remaks = Me.txtRemarks.Text
                ProductionMaster.StockMaster.Project = Me.cmbCostCenter.SelectedValue
                ProductionMaster.StockMaster.AccountId = Me.cmbVendor.Value
                ProductionMaster.StockMaster.StockDetailList = New List(Of StockDetail) 'Stock Detail Object
                ProductionMaster.PlanId = IIf(Me.cmbPlan.SelectedIndex = -1, 0, Me.cmbPlan.SelectedValue)
                ProductionMaster.DepartmentId = Me.cmbDepartment.SelectedValue 'Task:2690 Set DepertmentId Value.
                ''21-Jul-2014 TASK2753 Imran Ali Employee Field On Production Store
                ProductionMaster.EmployeeID = IIf(Me.cmbEmployee.SelectedIndex > 0, Me.cmbEmployee.SelectedValue, 0)
                'End Task:2753
                ProductionMaster.CGSAccountId = Me.cmbCGSAccount.Value

                'Item Wise Production Voucher Code Writter by Imran Ali on 5/13/2015
                ProductionMaster.VoucherHead = New VouchersMaster
                ProductionMaster.VoucherHead.ActivityLog = New ActivityLog
                ProductionMaster.VoucherHead.ActivityLog.Source = Me.Name.ToString
                ProductionMaster.VoucherHead.ActivityLog.User_Name = LoginUserName
                ProductionMaster.VoucherHead.ActivityLog.UserID = LoginUserId
                ProductionMaster.VoucherHead.ActivityLog.ApplicationName = "Production"
                ProductionMaster.VoucherHead.ActivityLog.FormCaption = Me.Text.ToString
                ProductionMaster.VoucherHead.ActivityLog.FormName = Me.Name.ToString
                ProductionMaster.ActivityLog = ProductionMaster.VoucherHead.ActivityLog

                With ProductionMaster.VoucherHead
                    .VoucherCode = Me.txtProductionNo.Text
                    .VoucherDate = Me.dtpProductionDate.Value
                    .VoucherNo = Me.txtProductionNo.Text
                    .Post = IIf(Me.chkPost.Checked = True, 1, 0)
                    ''TASK TFS1427 
                    If .Post = True Then
                        .Posted_UserName = LoginUserName
                    Else
                        .Posted_UserName = LoginUserName = ""

                    End If
                    ''END TASK TFS1427
                    .References = Me.txtRemarks.Text
                    .Source = Me.Name.ToString
                    .UserName = LoginUserName
                    .VNo = Me.txtProductionNo.Text
                    .VoucherTypeId = 7
                End With

                Dim objV As VouchersDetail

                ProductionMaster.VoucherHead.VoucherDatail = New List(Of VouchersDetail)
                For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows

                    ProductionDetail = New ProductionDetail
                    ProductionDetail.Location_ID = grdRow.Cells(EnumGrd.Location_Id).Value
                    ProductionDetail.ArticledefID = grdRow.Cells(EnumGrd.ArticleDefId).Value
                    ProductionDetail.ArticleSize = grdRow.Cells(EnumGrd.ArticleSize).Text
                    ProductionDetail.Sz1 = Val(grdRow.Cells(EnumGrd.Qty).Value)
                    ProductionDetail.Sz2 = 0
                    ProductionDetail.Sz3 = 0
                    ProductionDetail.Sz4 = 0
                    ProductionDetail.Sz5 = 0
                    ProductionDetail.Sz6 = 0
                    ProductionDetail.Sz7 = Val(grdRow.Cells(EnumGrd.PackQty).Value)
                    ''Commented below row against TASK-408 on 14-06-2016
                    ''ProductionDetail.Qty = IIf(grdRow.Cells(EnumGrd.ArticleSize).Text = "Loose", Val(grdRow.Cells(EnumGrd.Qty).Value), Val(grdRow.Cells(EnumGrd.Qty).Value) * Val(grdRow.Cells(EnumGrd.PackQty).Value))
                    ProductionDetail.Qty = Val(grdRow.Cells(EnumGrd.TotalQty).Value) ''TASK-408
                    ProductionDetail.CurrentRate = Val(grdRow.Cells(EnumGrd.CurrentRate).Value.ToString)
                    ''TASK TFS1496 addition of PackPrice
                    ProductionDetail.PackPrice = Val(grdRow.Cells(EnumGrd.PackPrice).Value.ToString)
                    ProductionDetail.Comments = grdRow.Cells(EnumGrd.Comments).Text.ToString
                    ''TASK TFS1772 addition of Dim1, Dim2
                    ProductionDetail.Dim1 = Val(grdRow.Cells(EnumGrd.Dim1).Value)
                    ProductionDetail.Dim2 = Val(grdRow.Cells(EnumGrd.Dim2).Value)
                    'Altered Against Task 20150506 Ali Ansari
                    ProductionDetail.EngineNo = grdRow.Cells(EnumGrd.EngineNo).Text.ToString
                    ProductionDetail.ChasisNo = grdRow.Cells(EnumGrd.ChasisNo).Text.ToString
                    'Altered Against Task 20150506 Ali Ansari
                    ''Task No 2555 Added The One Line Code To fill The Property of UOM 
                    ProductionDetail.UOM = grdRow.Cells(EnumGrd.Uom).Text
                    ProductionDetail.Pack_Desc = grdRow.Cells("Pack_Desc").Text.ToString

                    ''Task No 1616 Added The Lines of  Code To fill The Property of BatchNo,retailPrice,ExpiryDate, 
                    ProductionDetail.BatchNo = grdRow.Cells(EnumGrd.BatchNo).Text.ToString
                    ProductionDetail.RetailPrice = Val(grdRow.Cells(EnumGrd.RetailPrice).Value)
                    If grdRow.Cells(EnumGrd.ExpiryDate).Value.ToString = "" Then
                        ProductionDetail.ExpiryDate = Nothing
                    Else
                        ProductionDetail.ExpiryDate = grdRow.Cells(EnumGrd.ExpiryDate).Value
                    End If


                    ProductionDetail.PurchaseAccountId = Convert.ToInt32(grdRow.Cells("PurchaseAccountId").Value.ToString)
                    'ProductionDetail.CGSAccountId = Val(grdRow.Cells("CGSAccountId").Value.ToString)
                    ProductionDetail.PlanDetailId = Val(grdRow.Cells("PlanDetailID").Value.ToString)
                    ProductionMaster.ProductionDetail.Add(ProductionDetail)
                    StockDetail = New StockDetail 'Create New Stock Detail Object 
                    StockDetail.StockTransId = ProductionMaster.StockMaster.StockTransId 'Convert.ToInt32(GetStockTransId(Me.txtProductionNo.Text).ToString)
                    StockDetail.LocationId = grdRow.Cells(EnumGrd.Location_Id).Value
                    StockDetail.ArticleDefId = grdRow.Cells(EnumGrd.ArticleDefId).Value
                    ''Commented below row agaisnt TASK-408 to add TotalQty instead of Qty
                    'StockDetail.InQty = IIf(grdRow.Cells(EnumGrd.ArticleSize).Text = "Loose", Val(grdRow.Cells(EnumGrd.Qty).Value), Val(grdRow.Cells(EnumGrd.Qty).Value) * Val(grdRow.Cells(EnumGrd.PackQty).Value))
                    StockDetail.InQty = Val(grdRow.Cells(EnumGrd.TotalQty).Value.ToString) ''TASK-408
                    StockDetail.OutQty = 0
                    StockDetail.Rate = Val(grdRow.Cells(EnumGrd.CurrentRate).Value) 'GetAvgRateByItem(Val(grdRow.Cells("ArticleDefId").Value.ToString)) 'Val(grdRow.Cells("CurrentRate").Value)
                    ''Commented below row against TASK-408 on 14-06-2016 to add TotalQty instead of Qty
                    ''StockDetail.InAmount = ((IIf(grdRow.Cells(EnumGrd.ArticleSize).Text = "Loose", Val(grdRow.Cells(EnumGrd.Qty).Value), Val(grdRow.Cells(EnumGrd.Qty).Value) * Val(grdRow.Cells(EnumGrd.PackQty).Value))) * Val(StockDetail.Rate))
                    StockDetail.InAmount = (Val(grdRow.Cells(EnumGrd.TotalQty).Value.ToString) * Val(StockDetail.Rate)) ''TASK-408
                    StockDetail.OutAmount = 0
                    StockDetail.Remarks = grdRow.Cells(EnumGrd.Comments).Text.ToString
                    ''TASK-470 on 01-07-2016
                    StockDetail.PackQty = Val(grdRow.Cells(EnumGrd.PackQty).Value.ToString)
                    StockDetail.In_PackQty = Val(grdRow.Cells(EnumGrd.Qty).Value.ToString)
                    StockDetail.Out_PackQty = 0
                    ''End TASK-470
                    'Ali Faisal : TFS1362 : 22-Aug-2017 : Fill model for Engine / Chassis number
                    StockDetail.Engine_No = grdRow.Cells(EnumGrd.EngineNo).Value.ToString
                    StockDetail.Chassis_No = grdRow.Cells(EnumGrd.ChasisNo).Value.ToString
                    'Ali Faisal : TFS1362 : 22-Aug-2017 : End
                    'Ayesha Rehman : TFS1596 : 30-11-2017 
                    StockDetail.BatchNo = grdRow.Cells(EnumGrd.BatchNo).Value.ToString
                    StockDetail.ExpiryDate = CType(grdRow.Cells(EnumGrd.ExpiryDate).Value, Date).ToString("yyyy-M-d h:mm:ss tt")
                    'Ayesha Rehman : TFS1596: 30-11-2017 : End
                    ProductionMaster.StockMaster.StockDetailList.Add(StockDetail) 'Collection Values of Stock Detail object 

                    If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "True" Then
                        ProductionDetail.PurchaseAccountId = Val(getConfigValueByType("PurchaseDebitAccount").ToString)
                        If Me.cmbCGSAccount.ActiveRow.Cells(0).Value <= 0 Then ProductionDetail.CGSAccountId = Val(getConfigValueByType("StoreCreditAccount").ToString) Else ProductionDetail.CGSAccountId = Val(Me.cmbCGSAccount.Value)
                    Else
                        ProductionDetail.PurchaseAccountId = Val(grdRow.Cells(EnumGrd.AccountId).Value.ToString)
                        If Me.cmbCGSAccount.ActiveRow.Cells(0).Value > 0 Then ProductionDetail.CGSAccountId = Val(Me.cmbCGSAccount.Value) Else ProductionDetail.CGSAccountId = Val(getConfigValueByType("StoreCreditAccount").ToString)
                    End If

                    'Purchase Account Voucher 
                    objV = New VouchersDetail
                    objV.CoaDetailId = ProductionDetail.PurchaseAccountId
                    objV.Comments = grdRow.Cells(EnumGrd.ArticleDescription).Text & "(" & ProductionDetail.Qty & " x " & ProductionDetail.CurrentRate & ")"
                    objV.contra_coa_detail_id = ProductionDetail.PurchaseAccountId
                    objV.CostCenter = ProductionMaster.Project
                    objV.DebitAmount = (Val(ProductionDetail.Qty) * Val(ProductionDetail.CurrentRate))
                    objV.CreditAmount = 0
                    objV.LocationId = 1
                    objV.VoucherMaster = ProductionMaster.VoucherHead
                    ProductionMaster.VoucherHead.VoucherDatail.Add(objV)

                    'CGS Account Voucher 
                    objV = New VouchersDetail
                    objV.CoaDetailId = ProductionDetail.CGSAccountId
                    objV.Comments = grdRow.Cells(EnumGrd.ArticleDescription).Text & "(" & ProductionDetail.Qty & " x " & ProductionDetail.CurrentRate & ")"
                    objV.contra_coa_detail_id = ProductionDetail.CGSAccountId
                    objV.CostCenter = ProductionMaster.Project
                    objV.DebitAmount = 0
                    objV.CreditAmount = (Val(ProductionDetail.Qty) * Val(ProductionDetail.CurrentRate))
                    objV.LocationId = 1
                    objV.VoucherMaster = ProductionMaster.VoucherHead
                    ProductionMaster.VoucherHead.VoucherDatail.Add(objV)


                Next

                '    TotalDispatchQty = 0
                '    TotalDispatchAmount = 0

                'ElseIf Condition = "StoreIssuence" Then
                '    StoreIssuence = New StoreIssuenceMaster
                '    StoreIssuence.DispatchId = 0
                '    StoreIssuence.DispatchNo = Me.txtStoreIssuenceNo.Text
                '    StoreIssuence.CashPaid = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
                '    StoreIssuence.DispatchDate = Me.dtpProductionDate.Value
                '    StoreIssuence.LocationId = Me.cmbProductionStore.SelectedValue
                '    StoreIssuence.PartyInvoiceNo = String.Empty
                '    StoreIssuence.PartySlipNo = String.Empty
                '    StoreIssuence.Post = Me.chkPost.Checked
                '    StoreIssuence.PurchaseOrderID = Me.cmbCostCenter.SelectedValue
                '    StoreIssuence.RefDocument = Me.txtProductionNo.Text
                '    StoreIssuence.Remarks = Me.txtRemarks.Text
                '    StoreIssuence.UserName = LoginUserName
                '    StoreIssuence.VendorId = 0
                '    StoreIssuence.StockMaster = New StockMaster
                '    StoreIssuence.StockMaster.StockTransId = Convert.ToInt32(GetStockTransId(Me.txtStoreIssuenceNo.Text).ToString)
                '    StoreIssuence.StockMaster.DocNo = IIf(Me.txtStoreIssuenceNo.Text = String.Empty, GetDocumentNo1, Me.txtStoreIssuenceNo.Text)
                '    StoreIssuence.StockMaster.DocDate = Me.dtpProductionDate.Value.Date
                '    StoreIssuence.StockMaster.DocType = Convert.ToInt32(GetStockDocTypeId("StoreIssuence"))
                '    StoreIssuence.StockMaster.Remaks = Me.txtRemarks.Text
                '    StoreIssuence.StockMaster.Project = Me.cmbCostCenter.SelectedValue
                '    StoreIssuence.StockMaster.AccountId = Me.cmbVendor.Value
                '    StoreIssuence.CGSAccountId = Me.cmbCGSAccount.Value
                '    StoreIssuence.PlanId = ProductionMaster.PlanId
                '    StoreIssuence.StoreIssuenceDetail = New List(Of StoreIssuenceDetail)
                '    StoreIssuence.StockMaster.StockDetailList = New List(Of StockDetail)
                '    For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows

                '        Dim str_sql As String = "select * from articledeftable where articleid = '" & grdRow.Cells(EnumGrd.ArticleDefId).Value & "'"
                '        Dim dt_art As DataTable = GetDataTable(str_sql)
                '        dt_art.AcceptChanges()
                '        If Not dt_art Is Nothing Then
                '            strSQL = "SELECT dbo.tblCostSheet.ArticleID, dbo.tblCostSheet.Qty, dbo.ArticleDefView.PurchasePrice FROM  dbo.tblCostSheet INNER JOIN dbo.ArticleDefView ON dbo.tblCostSheet.ArticleID = dbo.ArticleDefView.ArticleId WHERE tblCostSheet.MasterArticleId=" & dt_art.Rows(0).Item("Masterid")
                '        End If
                '        Dim dt As DataTable = GetDataTable(strSQL)
                '        dt.AcceptChanges()

                '        If Not dt Is Nothing Then
                '            For Each dr As DataRow In dt.Rows
                '                StoreIssuenceDetail = New StoreIssuenceDetail
                '                StoreIssuenceDetail.ArticleDefId = Val(dr("ArticleId"))
                '                StoreIssuenceDetail.ArticleSize = "Loose"
                '                StoreIssuenceDetail.BatchID = 0
                '                StoreIssuenceDetail.BatchNo = "xxxx"
                '                StoreIssuenceDetail.CurrentPrice = Val(dr("PurchasePrice"))
                '                StoreIssuenceDetail.DispatchDetailId = 0
                '                StoreIssuenceDetail.DispatchId = 0
                '                StoreIssuenceDetail.LocationId = Me.cmbIssuedStore.SelectedValue
                '                Dim dblCostPrice As Decimal = 0D
                '                Dim dblPurchasePrice As Decimal = 0D
                '                Dim dblPrice As Decimal = 0D
                '                If getConfigValueByType("ApplyCostSheetRateOnProduction").ToString = "True" Then
                '                    'TASK-TFS-46 Get Raw Material Price
                '                    Dim dtprice As New DataTable
                '                    dtprice = GetCostPriceForRawMaterial(StoreIssuenceDetail.ArticleDefId)
                '                    dtprice.AcceptChanges()
                '                    If dtprice.Rows.Count > 0 Then
                '                        dblCostPrice = Val(dtprice.Rows(0).Item("CostPrice").ToString)
                '                        dblPurchasePrice = Val(dtprice.Rows(0).Item("PurchasePrice").ToString)
                '                    End If
                '                    If getConfigValueByType("AvgRate").ToString = "True" Then
                '                        dblPrice = dblCostPrice
                '                    Else
                '                        dblPrice = dblPurchasePrice
                '                    End If
                '                    If dblPrice = 0 Then
                '                        dblPrice = Val(dr("PurchasePrice").ToString)
                '                    End If

                '                    'END TASK-TFS-46
                '                    StoreIssuenceDetail.Price = dblPrice 'Val(dr("PurchasePrice"))
                '                Else
                '                    StoreIssuenceDetail.Price = Val(dr("PurchasePrice"))
                '                End If
                '                StoreIssuenceDetail.Qty = Val(dr("Qty") * (Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value.ToString)))
                '                StoreIssuenceDetail.Sz1 = Val(dr("Qty") * (Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value.ToString)))
                '                StoreIssuenceDetail.Sz2 = 0
                '                StoreIssuenceDetail.Sz3 = 0
                '                StoreIssuenceDetail.Sz4 = 0
                '                StoreIssuenceDetail.Sz5 = 0
                '                StoreIssuenceDetail.Sz6 = 0
                '                StoreIssuenceDetail.Sz7 = 1 'Val(dr("Qty") * (Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value.ToString)))
                '                StoreIssuenceDetail.Pack_Desc = grdRow.Cells("Pack_Desc").Text.ToString
                '                StoreIssuenceDetail.PurchaseAccountId = Val(grdRow.Cells("PurchaseAccountId").Value.ToString)
                '                StoreIssuenceDetail.CGSAccountId = Val(grdRow.Cells("CGSAccountId").Value.ToString)
                '                StoreIssuenceDetail.ArticleDefMasterId = Val(grdRow.Cells(EnumGrd.MasterId).Value.ToString)
                '                StoreIssuence.StoreIssuenceDetail.Add(StoreIssuenceDetail)
                '                StockDetail = New StockDetail 'Create New Stock Detail Object 
                '                StockDetail.StockTransId = StoreIssuence.StockMaster.StockTransId 'Convert.ToInt32(GetStockTransId(Me.txtStoreIssuenceNo.Text).ToString)
                '                StockDetail.LocationId = Me.cmbIssuedStore.SelectedValue
                '                StockDetail.ArticleDefId = Val(dr("ArticleId"))
                '                StockDetail.InQty = 0
                '                StockDetail.OutQty = Val(dr("Qty") * (Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value.ToString)))
                '                If getConfigValueByType("ApplyCostSheetRateOnProduction").ToString = "True" Then
                '                    StockDetail.Rate = dblPrice 'GetAvgRateByItem(Val(dr("ArticleId").ToString)) 'Val(dr("PurchasePrice"))
                '                Else
                '                    StockDetail.Rate = GetAvgRateByItem(Val(dr("ArticleId").ToString)) 'Val(dr("PurchasePrice"))
                '                End If
                '                StockDetail.InAmount = 0
                '                StockDetail.OutAmount = Val(dr("Qty") * ((Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value)) * Val(StockDetail.Rate)))
                '                StockDetail.Remarks = grdRow.Cells(EnumGrd.Comments).Text.ToString
                '                StoreIssuence.StockMaster.StockDetailList.Add(StockDetail)
                '                TotalDispatchQty = TotalDispatchQty + Val(dr("Qty") * (Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value.ToString)))
                '                TotalDispatchAmount = TotalDispatchAmount + Val(dr("Qty") * ((Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value.ToString)) * Val(dr("PurchasePrice"))))
                '            Next
                '        End If
                '    Next
                '    StoreIssuence.DispatchAmount = TotalDispatchAmount 'Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
                '    StoreIssuence.DispatchQty = TotalDispatchQty  'Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)
                'ElseIf Condition = "Dispatch" Then
                '    StockDispatch = New StockDispatchBE
                '    StockDispatch.DispatchId = 0
                '    StockDispatch.DispatchNo = IIf(Me.txtDispatchNo.Text = String.Empty, GetDocumentNo2, Me.txtDispatchNo.Text)
                '    StockDispatch.CashPaid = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
                '    StockDispatch.DispatchDate = Me.dtpProductionDate.Value
                '    StockDispatch.LocationId = Me.cmbProductionStore.SelectedValue
                '    StockDispatch.PartyInvoiceNo = String.Empty
                '    StockDispatch.PartySlipNo = String.Empty
                '    'StockDispatch.Post = Me.chkPost.Checked
                '    StockDispatch.PurchaseOrderID = Me.cmbCostCenter.SelectedValue
                '    StockDispatch.RefDocument = Me.txtProductionNo.Text
                '    StockDispatch.Remarks = Me.txtRemarks.Text
                '    StockDispatch.UserName = LoginUserName
                '    StockDispatch.VendorId = Me.cmbProductionStore.SelectedValue
                '    StockDispatch.StockMaster = New StockMaster
                '    StockDispatch.StockMaster.StockTransId = Convert.ToInt32(GetStockTransId(Me.txtDispatchNo.Text).ToString)
                '    StockDispatch.StockMaster.DocNo = IIf(Me.txtDispatchNo.Text = String.Empty, GetDocumentNo2, Me.txtDispatchNo.Text)
                '    StockDispatch.StockMaster.DocDate = Me.dtpProductionDate.Value.Date
                '    StockDispatch.StockMaster.DocType = Convert.ToInt32(GetStockDocTypeId("Dispatch"))
                '    StockDispatch.StockMaster.Remaks = Me.txtRemarks.Text
                '    StockDispatch.StockDispatchDetail = New List(Of StockDispatchDetailBE)
                '    StockDispatch.StockMaster.StockDetailList = New List(Of StockDetail)
                '    For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                '        'Dim str_sql As String = "select * from articledeftable where articleid = '" & grdRow.Cells(EnumGrd.ArticleDefId).Value & "'"
                '        'Dim dt_art As DataTable = GetDataTable(str_sql)
                '        'If Not dt_art Is Nothing Then
                '        '    strSQL = "SELECT dbo.tblCostSheet.ArticleID, dbo.tblCostSheet.Qty, dbo.ArticleDefView.PurchasePrice FROM  dbo.tblCostSheet INNER JOIN                   dbo.ArticleDefView ON dbo.tblCostSheet.ArticleID = dbo.ArticleDefView.ArticleId WHERE tblCostSheet.MasterArticleId=" & dt_art.Rows(0).Item("Masterid")
                '        'End If
                '        'Dim dt As DataTable = GetDataTable(strSQL)
                '        'If Not dt Is Nothing Then
                '        '    For Each dr As DataRow In dt.Rows
                '        StockDispatchDetail = New StockDispatchDetailBE
                '        StockDispatchDetail.ArticleDefId = grdRow.Cells("ArticleDefId").Value
                '        StockDispatchDetail.ArticleSize = grdRow.Cells("ArticleSize").Text
                '        StockDispatchDetail.BatchID = 0
                '        StockDispatchDetail.BatchNo = "xxxx"
                '        StockDispatchDetail.CurrentPrice = grdRow.Cells("CurrentRate").Value
                '        StockDispatchDetail.DispatchDetailId = 0
                '        StockDispatchDetail.DispatchId = 0
                '        StockDispatchDetail.LocationId = Me.cmbProductionStore.SelectedValue
                '        StockDispatchDetail.Price = grdRow.Cells("CurrentRate").Value
                '        StockDispatchDetail.Qty = Val(grdRow.Cells("Qty").Value)
                '        StockDispatchDetail.Sz1 = Val(grdRow.Cells("Qty").Value)
                '        'StoreIssuenceDetail.Sz2 = 0
                '        'StockDispatchDetail.Sz3 = 0
                '        'StockDispatchDetail.Sz4 = 0
                '        'StockDispatchDetail.Sz5 = 0
                '        'StockDispatchDetail.Sz6 = 0
                '        StockDispatchDetail.Sz7 = Val(grdRow.Cells("PackQty").Value)
                '        StockDispatch.StockDispatchDetail.Add(StockDispatchDetail)


                '        StockDetail = New StockDetail 'Create New Stock Detail Object 
                '        StockDetail.StockTransId = Convert.ToInt32(GetStockTransId(Me.txtStoreIssuenceNo.Text).ToString)
                '        StockDetail.LocationId = Me.cmbProductionStore.SelectedValue
                '        StockDetail.ArticleDefId = Val(grdRow.Cells("ArticleDefId").Value)
                '        StockDetail.InQty = 0
                '        StockDetail.OutQty = (Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value))
                '        StockDetail.Rate = Val(grdRow.Cells("CurrentRate").Value)
                '        StockDetail.InAmount = 0
                '        StockDetail.OutAmount = ((Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value)) * Val(grdRow.Cells("CurrentRate").Value))
                '        StockDetail.Remarks = String.Empty
                '        StockDispatch.StockMaster.StockDetailList.Add(StockDetail)
                '        TotalDispatchQty = TotalDispatchQty + (Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value))
                '        TotalDispatchAmount = TotalDispatchAmount + ((Val(grdRow.Cells("Qty").Value) * Val(grdRow.Cells("PackQty").Value)) * Val(grdRow.Cells("CurrentRate").Value))
                '        'Next
                '        'End If
                '    Next
                '    StockDispatch.DispatchAmount = TotalDispatchAmount 'Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
                '    StockDispatch.DispatchQty = TotalDispatchQty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
            Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
            If Condition = "All" Then
                Me.grdSave.DataSource = New ProductionDAL().GetAllRecord("All", IIf(Me.dtpFrom.Checked = False, Nothing, dtpFrom.Value), IIf(Me.dtpTo.Checked = False, Nothing, Me.dtpTo.Value), Me.txtSearchDocNo.Text, Me.txtPurchaseNo.Text, Val(Me.txtFromAmount.Text), Val(Me.txtToAmount.Text), Me.cmbSearchCostCenter.SelectedValue, Me.txtSearchRemarks.Text, Me.cmbSearchLocation.SelectedValue, Me.txtChassisNo.Text, Me.txtSearchEnginNo.Text)
            Else
                Me.grdSave.DataSource = New ProductionDAL().GetAllRecord()
            End If
            Me.grdSave.RetrieveStructure()
            Me.grdSave.RootTable.Columns.Add("Selected")
            Me.grdSave.RootTable.Columns("Selected").ActAsSelector = True
            Me.grdSave.RootTable.Columns("Selected").UseHeaderSelector = True
            Me.grdSave.RootTable.Columns("Production_Id").Visible = False
            Me.grdSave.RootTable.Columns("Production_Store").Visible = False
            Me.grdSave.RootTable.Columns("Project").Visible = False
            Me.grdSave.RootTable.Columns("CustomerCode").Visible = False
            Me.grdSave.RootTable.Columns("DepartmentId").Visible = False 'TAsk:2690 Column Hidden
            Me.grdSave.RootTable.Columns(EnumGridMaster.Production_date).Caption = "Document Date"
            Me.grdSave.RootTable.Columns(EnumGridMaster.Production_no).Caption = "Document No"
            Me.grdSave.RootTable.Columns(EnumGridMaster.location_name).Caption = "Production Store"
            Me.grdSave.RootTable.Columns(EnumGridMaster.Order_No).Caption = "Order No"
            Me.grdSave.RootTable.Columns(EnumGridMaster.CostCenter).Caption = "Cost Center"
            Me.grdSave.RootTable.Columns(EnumGridMaster.IGPNo).Caption = "IGP No"
            Me.grdSave.RootTable.Columns(EnumGridMaster.Remarks).Caption = "Remarks"
            Me.grdSave.RootTable.Columns(EnumGridMaster.IssuedSTore).Visible = False
            Me.grdSave.RootTable.Columns(EnumGridMaster.RefDocument).Visible = False
            Me.grdSave.RootTable.Columns(EnumGridMaster.CGSAccountId).Visible = False
            Me.grdSave.RootTable.Columns(EnumGridMaster.EmployeeID).Visible = False
            Me.grdSave.RootTable.Columns(EnumGridMaster.PlanId).Visible = False
            Me.grdSave.RootTable.Columns("PlanTicketId").Visible = False
            Me.grdSave.RootTable.Columns(EnumGridMaster.Production_date).FormatString = str_DisplayDateFormat
            Me.grdSave.RootTable.Columns("Selected").Width = 50
            Me.grdSave.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtProductionNo.Text = String.Empty Then
                ShowErrorMessage("Please define document No")
                Me.txtProductionNo.Focus()
                Return False
            End If
            'If Me.cmbProductionStore.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select production store")
            '    Me.cmbProductionStore.Focus()
            '    Return False
            'End If
            'If Me.cmbCostCenter.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select cost center")
            '    Me.cmbCostCenter.Focus()
            '    Return False
            'End If
            If getConfigValueByType("StoreIssuenceWithProduction").ToString = "True" Then
                If Me.cmbIssuedStore.SelectedIndex = 0 Then
                    ShowErrorMessage("Please select Issued Store")
                    Me.cmbIssuedStore.Focus()
                    Me.cmbIssuedStore.DroppedDown = True
                    Return False
                End If
            End If
            '' Task:2422 Validation on without any item in grid


            If Me.cmbCGSAccount.Visible = True And Me.cmbCGSAccount.Enabled = True Then
                If Me.cmbCGSAccount.Value = 0 Then
                    If Convert.ToInt32(getConfigValueByType("StoreCreditAccount").ToString.Replace("Error", "0")) = 0 Then
                        ShowErrorMessage("Please Select CGS Account.")
                        Me.cmbCGSAccount.Focus()
                        Return False
                    End If
                End If
            Else
                If Convert.ToInt32(getConfigValueByType("StoreIssuenceAccount").ToString.Replace("Error", "0")) = 0 Then
                    ShowErrorMessage("Please Mapp CGS Account From Inventory Configuration.")
                    Return False
                End If
            End If

            If Me.grdDetail.RowCount = 0 Then
                ShowErrorMessage("No record in the detail.")
                Me.grdDetail.Focus()
                Return False
            End If

            'End Task:2422
            'Altered by Ali Ansari against task #20150506
            'Rectifying duplicate records in grid
            Dim i As Integer
            Dim j As Integer

            For i = 0 To grdDetail.RowCount - 1

                For j = i + 1 To grdDetail.RowCount - 1
                    If grdDetail.GetRow(j).Cells("Engineno").Value.ToString.Length > 0 Then
                        If grdDetail.GetRow(j).Cells("Engineno").Value.ToString = grdDetail.GetRow(i).Cells("Engineno").Value.ToString Then
                            Throw New Exception("Engine no [" & Me.grdDetail.GetRows(i).Cells(EnumGrd.EngineNo).Value.ToString & "] already exists")
                            Exit Function

                        End If
                    End If

                    If grdDetail.GetRow(j).Cells("Chasisno").Value.ToString.Length > 0 Then
                        If grdDetail.GetRow(j).Cells("ChasisNo").Value.ToString = grdDetail.GetRow(i).Cells("Chasisno").Value.ToString Then
                            Throw New Exception("Chasis no [" & Me.grdDetail.GetRows(i).Cells(EnumGrd.ChasisNo).Value.ToString & "] already exists")
                            Exit Function
                        End If
                    End If

                Next
            Next
            'Altered by Ali Ansari against task #20150506

            Dim StockInConfigration As String = "" ''1596
            ''Start Task 1596
            If Not getConfigValueByType("StockInConfigration").ToString = "Error" Then ''1596
                StockInConfigration = getConfigValueByType("StockInConfigration").ToString
            End If
            'ShowInformationMessage(StockInConfigration)
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                If StockInConfigration.Equals("Required") AndAlso r.Cells(EnumGrd.BatchNo).Value.ToString = String.Empty Then
                    msg_Error("Please Enter Value in Batch No")
                    Return False
                    Exit For
                End If
            Next
            'End Task:1596

            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try

            Dim StockInConfigration As String = "" ''1596

            If Not getConfigValueByType("StockInConfigration").ToString = "Error" Then ''1596
                StockInConfigration = getConfigValueByType("StockInConfigration").ToString
            End If
            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''END TFS4544

            'ShowInformationMessage(StockInConfigration)
            If StockInConfigration.Equals("Disabled") Then
                pnlBatchNo.Visible = False
                pnlMoving.Location = New Point(568, 246)
            ElseIf StockInConfigration.Equals("Enabled") Then
                pnlBatchNo.Visible = True
                pnlMoving.Location = New Point(639, 243)
            Else
                pnlBatchNo.Visible = True
                pnlMoving.Location = New Point(639, 243)
            End If
            'Task 1592 Ayesha Rehman Removing the ErrorProvider on btnNew
            ErrorProvider1.Clear()
            blnEditMode = False
            setEditMode = False
            MasterId = 0
            'DocNo()
            Me.BtnSave.Text = "&Save"
            Me.dtpProductionDate.Value = Date.Now
            Me.dtpProductionDate.Enabled = True
            Me.dtpExpiryDate.Value = Date.Now
            Me.dtpExpiryDate.Enabled = True
            DocNo()
            Me.cmbProductionStore.SelectedIndex = 0
            If flgSelectedProject = True Then
                If Not Me.cmbCostCenter.SelectedIndex = -1 Then Me.cmbCostCenter.SelectedIndex = Me.cmbCostCenter.SelectedIndex
            Else
                If Not Me.cmbCostCenter.SelectedIndex = -1 Then Me.cmbCostCenter.SelectedIndex = 0
            End If
            If Not Me.cmbEmployee.SelectedIndex = -1 Then Me.cmbEmployee.SelectedIndex = 0
            Me.cmbVendor.Rows(0).Activate()
            'Me.cmbOrderNo.SelectedIndex = 0
            Me.txtIGPNo.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
            Me.cmbUnit.SelectedIndex = 0
            Me.cmbLocation.SelectedIndex = 0
            FillCombos("Item")
            If Me.cmbItem.ActiveRow IsNot Nothing Then
                Me.cmbItem.Rows(0).Activate()
            End If
            'Me.GetAllRecords()
            DisplayRecord(-1)
            ApplySecurityAndRights()
            FillCombos("IssuedStore")
            'FillCombos("Plan")
            If Not Me.cmbPlan.SelectedIndex = -1 Then
                Me.cmbPlan.SelectedIndex = 0
            End If
            Me.cmbPlan.Enabled = True
            Me.cmbTicket.Enabled = True
            'FillCombos("Tickets")
            'Task No 2555 Appending Fill Combo Function To Filll Combo Box 
            FillCombos("UM")
            'Task No 1616 Appending Fill Combo Function To Filll Combo Box 
            FillCombos("Retail Price")
            If getConfigValueByType("StoreIssuenceWithProduction").ToString = "True" Then
                Me.cmbIssuedStore.Visible = True
                Me.lblIssuedStore.Visible = True
            Else
                Me.cmbIssuedStore.Visible = False
                Me.lblIssuedStore.Visible = False
            End If
            If Me.cmbCGSAccount.ActiveRow IsNot Nothing Then
                Me.cmbCGSAccount.Rows(0).Activate()
            End If
            GetSecurityRights()
            Me.dtpFrom.Value = Date.Now.AddMonths(-1)
            Me.dtpTo.Value = Date.Now
            Me.dtpFrom.Checked = False
            Me.dtpTo.Checked = False
            Me.txtSearchDocNo.Text = String.Empty
            'Me.cmbSearchLocation.SelectedIndex = 0
            Me.txtFromAmount.Text = String.Empty
            Me.txtToAmount.Text = String.Empty
            Me.txtPurchaseNo.Text = String.Empty
            'Me.cmbSearchCostCenter.SelectedIndex = 0
            Me.txtSearchRemarks.Text = String.Empty
            Me.SplitContainer1.Panel1Collapsed = True
            'GetAllRecords()
            Me.lblPrintStatus.Text = String.Empty
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.btnEdit.Visible = False
            Me.btnDelete.Visible = False
            Me.btnPrint.Visible = False
            blnUpdateAll = False
            Me.btnUpdateAll.Enabled = True
            Engine_NoBefore = String.Empty
            Chassis_NoBefore = String.Empty
            EngineNoFirst = String.Empty
            ChassisNoFirst = String.Empty
            '''''''''''''''''''''''''''
            Me.txtDim1.Text = String.Empty 'TFS1772
            Me.txtDim2.Text = String.Empty 'TFS1772
            Me.cmbRetailPrice.Text = String.Empty
            Me.txtBatchNo.Text = String.Empty
            Me.txtComments.Text = String.Empty
            Me.cmbProductionStore.Focus()
            If Not Me.cmbDepartment.SelectedIndex = -1 Then Me.cmbDepartment.SelectedIndex = 0 'Task:2690 Reseting Department Dropdown
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            ''''''''''''''''''''''
            'Altered By Ali Ansari Against Task#20150506
            'check duplicate record of engine and chasis
            Dim i As Integer
            Dim j As Integer
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            objCommand.Connection = objCon
            Dim trans As OleDbTransaction = objCon.BeginTransaction
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            objCommand.CommandText = ""

            For i = 0 To grdDetail.GetRows.Length - 1

                Dim Engine_No As String = ""
                Dim Chassis_No As String = ""
                If Me.grdDetail.GetRows(i).Cells(EnumGrd.EngineNo).Value.ToString.Length > 0 Then
                    Engine_No = Me.grdDetail.GetRows(i).Cells(EnumGrd.EngineNo).Value.ToString.Substring(4)
                End If
                If Me.grdDetail.GetRows(i).Cells(EnumGrd.ChasisNo).Value.ToString.Length > 0 Then
                    Chassis_No = Me.grdDetail.GetRows(i).Cells(EnumGrd.ChasisNo).Value.ToString.Substring(4)
                End If
                objCommand.CommandText = ""
                objCommand.CommandText = "SELECT dbo.ProductionDetailTable.ArticleDefId, Stuff(ProductionDetailTable.EngineNo, 1, 4, '') As EngineNo " _
                                                & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                & " dbo.ProductionDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.ProductionDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId = " & Me.grdDetail.GetRows(i).Cells(EnumGrd.ArticleDefId).Value & ""
                If Engine_No.Length > 0 Then
                    objCommand.CommandText += " AND Stuff(ProductionDetailTable.EngineNo, 1, 4, '') =N'" & Engine_No & "'"
                End If

                Dim dtVehicleEngineNoIdentificationInfo As New DataTable
                Dim daVehicleEngineNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                daVehicleEngineNoIdentificationInfo.Fill(dtVehicleEngineNoIdentificationInfo)

                ''Retrieving Chasis No
                objCommand.CommandText = ""
                objCommand.CommandText = "SELECT dbo.ProductionDetailTable.ArticleDefId, Stuff(ProductionDetailTable.ChasisNo, 1, 4, '') As ChasisNo " _
                                                & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                & " dbo.ProductionDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.ProductionDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId = " & Me.grdDetail.GetRows(i).Cells(EnumGrd.ArticleDefId).Value & ""

                If Chassis_No.Length > 0 Then
                    objCommand.CommandText += " AND Stuff(ProductionDetailTable.ChasisNo, 1, 4, '') =N'" & Chassis_No & "'"
                End If
                Dim dtVehicleChasisNoIdentificationInfo As New DataTable
                Dim daVehicleChasisNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                daVehicleChasisNoIdentificationInfo.Fill(dtVehicleChasisNoIdentificationInfo)

                If dtVehicleEngineNoIdentificationInfo IsNot Nothing Then
                    If dtVehicleEngineNoIdentificationInfo.Rows.Count > 0 Then

                        If dtVehicleEngineNoIdentificationInfo.Rows(0).Item("EngineNo").ToString.Length > 0 Or Engine_No.Length > 0 Then
                            If Engine_No = dtVehicleEngineNoIdentificationInfo.Rows(0).Item("EngineNo").ToString Then
                                Throw New Exception("Engine no [" & Me.grdDetail.GetRows(i).Cells(EnumGrd.EngineNo).Value.ToString & "] already exists against item [" & Me.grdDetail.GetRows(i).Cells(EnumGrd.ArticleDescription).Value.ToString & "]")
                            End If
                        End If
                    End If
                End If

                If dtVehicleChasisNoIdentificationInfo IsNot Nothing Then
                    If dtVehicleChasisNoIdentificationInfo.Rows.Count > 0 Then
                        If dtVehicleChasisNoIdentificationInfo.Rows(0).Item("ChasisNo").ToString.Length > 0 Or Chassis_No.Length > 0 Then
                            If Chassis_No = dtVehicleChasisNoIdentificationInfo.Rows(0).Item("ChasisNo").ToString Then
                                Throw New Exception("Chassis no [" & Me.grdDetail.GetRows(i).Cells(EnumGrd.ChasisNo).Value.ToString & "] already exists against item [" & Me.grdDetail.GetRows(i).Cells(EnumGrd.ArticleDescription).Value.ToString & "]")
                            End If
                        End If
                    End If
                End If

            Next
            trans.Commit()
            'Altered By Ali Ansari Against Task#20150506
            If ProductionMaster.Production_No <> GetDocumentNo() Then
                Dim DocumentNo As String = ProductionMaster.Production_No
                ProductionMaster.Production_No = GetDocumentNo()
                Me.txtProductionNo.Text = ProductionMaster.Production_No
                Throw New Exception(DocumentNo & " has already been saved and new document is" & ProductionMaster.Production_No)
            End If
            Call New ProductionDAL().Add(ProductionMaster)

            'If getConfigValueByType("StoreIssuenceWithProduction").ToString = "True" Then

            '    FillModel("StoreIssuence")
            '    Call New StoreIssuenceDAL().Add(StoreIssuence)
            '    Dim str_SQL As String = String.Empty
            '    str_SQL = "Select DispatchId From DispatchMasterTable WHERE DispatchNo='" & StoreIssuence.DispatchNo & "'"
            '    Dim dt_Disp As DataTable = UtilityDAL.GetDataTable(str_SQL)
            '    dt_Disp.AcceptChanges()
            '    If Not dt_Disp Is Nothing Then
            '        If dt_Disp.Rows.Count > 0 Then
            '            str_SQL = String.Empty
            '            str_SQL = "Select * From DispatchDetailTable WHERE DispatchId=" & dt_Disp.Rows(0).Item(0).ToString
            '            Dim dt_DispDt As DataTable = UtilityDAL.GetDataTable(str_SQL)
            '            dt_DispDt.AcceptChanges()
            '            If Not dt_DispDt Is Nothing Then
            '                If dt_DispDt.Rows.Count = 0 Then
            '                    Call New StoreIssuenceDAL().Delete(StoreIssuence)
            '                End If
            '            End If
            '        End If
            '    End If


            'End If
            'If GetConfigValue("StockDispatchOnProduction").ToString = "True" Then
            '    FillModel("Dispatch")
            '    Call New StockDispatchDAL().Add(StockDispatch)
            '    Dim str_SQL As String = String.Empty
            '    str_SQL = "Select DispatchId From DispatchMasterTable WHERE DispatchNo='" & StockDispatch.DispatchNo & "'"
            '    Dim dt_Disp As DataTable = UtilityDAL.GetDataTable(str_SQL)
            '    If Not dt_Disp Is Nothing Then
            '        If dt_Disp.Rows.Count > 0 Then
            '            str_SQL = String.Empty
            '            str_SQL = "Select * From DispatchDetailTable WHERE DispatchId=" & dt_Disp.Rows(0).Item(0).ToString
            '            Dim dt_DispDt As DataTable = UtilityDAL.GetDataTable(str_SQL)
            '            If Not dt_DispDt Is Nothing Then
            '                If dt_DispDt.Rows.Count = 0 Then
            '                    Call New StockDispatchDAL().Delete(StockDispatch)
            '                End If
            '            End If
            '        End If
            '    End If
            'End If
            setVoucherNo = ProductionDAL.GetRecordNo
            getVoucher_Id = ProductionDAL.GetRecordId
            setVoucherDate = Me.dtpProductionDate.Value.Date
            setEditMode = False
            Total_Amount = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(EnumGrd.Total), Janus.Windows.GridEX.AggregateFunction.Sum)
            Return True
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
            Dim i As Integer
            Dim j As Integer
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            objCommand.Connection = objCon
            Dim trans As OleDbTransaction = objCon.BeginTransaction
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            objCommand.CommandText = ""
            objCommand.CommandText = String.Empty
            objCommand.CommandText = "Update PlanDetailTable Set ProducedQty=IsNull(ProducedQty,0)-IsNull(ProductionDetailTable.Sz1,0),  ProducedTotalQty=IsNull(ProducedTotalQty,0)-IsNull(ProductionDetailTable.Qty,0) From PlanDetailTable, ProductionDetailTable WHERE ProductionDetailTable.PlanDetailId = PlanDetailTable.PlanDetailId AND ProductionDetailTable.ArticleDefId = PlanDetailTable.ArticleDefId And PlanDetailTable.PlanId=" & ProductionMaster.PlanId & "" ''TASK-408 added new column ProducedTotalQty to be updated too. Dated 14-06-2016
            objCommand.ExecuteNonQuery()
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Str)
            objCommand.CommandText = String.Empty
            objCommand.CommandText = "Delete From ProductionDetailTable WHERE Production_Id=" & ProductionMaster.ProductionId & ""
            objCommand.ExecuteNonQuery()
            trans.Commit()
            For i = 0 To grdDetail.GetRows.Length - 1
                Dim Engine_No As String = ""
                Dim Chassis_No As String = ""
                If Me.grdDetail.GetRows(i).Cells(EnumGrd.EngineNo).Value.ToString.Length > 0 Then
                    Engine_No = Me.grdDetail.GetRows(i).Cells(EnumGrd.EngineNo).Value.ToString.Substring(4)
                End If
                If Me.grdDetail.GetRows(i).Cells(EnumGrd.ChasisNo).Value.ToString.Length > 0 Then
                    Chassis_No = Me.grdDetail.GetRows(i).Cells(EnumGrd.ChasisNo).Value.ToString.Substring(4)
                End If
                objCommand.CommandText = ""
                objCommand.CommandText = "SELECT dbo.ProductionDetailTable.ArticleDefId, Stuff(ProductionDetailTable.EngineNo, 1, 4, '') As EngineNo " _
                                                & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                & " dbo.ProductionDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.ProductionDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId = " & Me.grdDetail.GetRows(i).Cells(EnumGrd.ArticleDefId).Value & ""
                If Engine_No.Length > 0 Then
                    objCommand.CommandText += " AND Stuff(ProductionDetailTable.EngineNo, 1, 4, '') =N'" & Engine_No & "'"
                End If

                Dim dtVehicleEngineNoIdentificationInfo As New DataTable
                Dim daVehicleEngineNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                daVehicleEngineNoIdentificationInfo.Fill(dtVehicleEngineNoIdentificationInfo)

                ''Retrieving Chasis No
                objCommand.CommandText = ""
                objCommand.CommandText = "SELECT dbo.ProductionDetailTable.ArticleDefId, Stuff(ProductionDetailTable.ChasisNo, 1, 4, '') As ChasisNo " _
                                                & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                & " dbo.ProductionDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.ProductionDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId = " & Me.grdDetail.GetRows(i).Cells(EnumGrd.ArticleDefId).Value & ""

                If Chassis_No.Length > 0 Then
                    objCommand.CommandText += " AND Stuff(ProductionDetailTable.ChasisNo, 1, 4, '') =N'" & Chassis_No & "'"
                End If
                Dim dtVehicleChasisNoIdentificationInfo As New DataTable
                Dim daVehicleChasisNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                daVehicleChasisNoIdentificationInfo.Fill(dtVehicleChasisNoIdentificationInfo)

                If dtVehicleEngineNoIdentificationInfo IsNot Nothing Then
                    If dtVehicleEngineNoIdentificationInfo.Rows.Count > 0 Then

                        If dtVehicleEngineNoIdentificationInfo.Rows(0).Item("EngineNo").ToString.Length > 0 Or Engine_No.Length > 0 Then
                            If Engine_No = dtVehicleEngineNoIdentificationInfo.Rows(0).Item("EngineNo").ToString Then
                                Throw New Exception("Engine no [" & Me.grdDetail.GetRows(i).Cells(EnumGrd.EngineNo).Value.ToString & "] already exists against item [" & Me.grdDetail.GetRows(i).Cells(EnumGrd.ArticleDescription).Value.ToString & "]")
                            End If
                        End If
                    End If
                End If

                If dtVehicleChasisNoIdentificationInfo IsNot Nothing Then
                    If dtVehicleChasisNoIdentificationInfo.Rows.Count > 0 Then
                        If dtVehicleChasisNoIdentificationInfo.Rows(0).Item("ChasisNo").ToString.Length > 0 Or Chassis_No.Length > 0 Then
                            If Chassis_No = dtVehicleChasisNoIdentificationInfo.Rows(0).Item("ChasisNo").ToString Then
                                Throw New Exception("Chassis no [" & Me.grdDetail.GetRows(i).Cells(EnumGrd.ChasisNo).Value.ToString & "] already exists against item [" & Me.grdDetail.GetRows(i).Cells(EnumGrd.ArticleDescription).Value.ToString & "]")
                            End If
                        End If
                    End If
                End If

            Next


            Call New ProductionDAL().Update(ProductionMaster)
            'If getConfigValueByType("StoreIssuenceWithProduction").ToString = "True" Then
            '    FillModel("StoreIssuence")
            '    Call New StoreIssuenceDAL().Update(StoreIssuence)
            'End If
            'If GetConfigValue("StockDispatchOnProduction").ToString = "True" Then
            '    FillModel("Dispatch")
            '    Call New StockDispatchDAL().Update(StockDispatch)
            'End If
            setVoucherNo = ProductionDAL.GetRecordNo
            getVoucher_Id = ProductionDAL.GetRecordId
            setVoucherDate = Me.dtpProductionDate.Value.Date
            setEditMode = False
            Total_Amount = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(EnumGrd.Total), Janus.Windows.GridEX.AggregateFunction.Sum)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub DisplayRecord(ByVal MasterId As Integer)
        Try
            Dim dt As New DataTable
            If Not flgLoadAllItems = True Then
                dt = New ProductionDAL().GetRecordById(MasterId, IIf(flgCompanyRights = True, "" & MyCompanyId & "", Nothing))
            Else
                dt = New ProductionDAL().GetRecordById(MasterId, IIf(flgCompanyRights = True, "" & MyCompanyId & "", Nothing), "All")
            End If
            ''Commented below row against TASK-408 to multiply currentrate with TotalQty instead of ArticleSize condition
            'dt.Columns(EnumGrd.Total).Expression = " IIF(ArticleSize='Loose', (Qty*CurrentRate), ((PackQty*Qty)*CurrentRate)) "
            dt.Columns(EnumGrd.Total).Expression = "(TotalQty*CurrentRate)" ''TASK-408
            Me.grdDetail.DataSource = dt
            ApplyGridSettings()
            FillCombos("grdLocation")
            If flgLoadAllItems = True Then
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                    r.BeginEdit()
                    r.Cells(EnumGrd.Location_Id).Value = Me.cmbLocation.SelectedValue
                    r.EndEdit()
                Next
            End If
            FillCombos("Employee") 'Task:2690 Get employee Data
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DisplayRecordByPlan(ByVal MasterId As Integer)
        Try
            Dim dt As New DataTable
            dt = New ProductionDAL().GetRecordById(MasterId, IIf(flgCompanyRights = True, "" & MyCompanyId & "", 0), "Plan")
            dt.AcceptChanges()
            ''Commented below row against TASK-408 to add TotalQty instead of Pack Qty * Qty or Qty
            ''dt.Columns(EnumGrd.Total).Expression = " IIF(ArticleSize='Loose', (IsNull(Qty,0)*IsNull(CurrentRate,0)), ((IsNull(PackQty,0)*Qty)*IsNull(CurrentRate,0))) "
            dt.Columns(EnumGrd.Total).Expression = " (IsNull(TotalQty,0)*IsNull(CurrentRate,0)) "
            Me.grdDetail.DataSource = dt
            ApplyGridSettings()
            FillCombos("grdLocation")
            If flgLoadAllItems = True Then
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                    r.BeginEdit()
                    r.Cells(EnumGrd.Location_Id).Value = Me.cmbLocation.SelectedValue
                    r.EndEdit()
                Next
            End If
            FillCombos("Employee") 'Task:2690 Get employee Data
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub frmProductionStore_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8 -1-14
            If e.KeyCode = Keys.F4 Then
                If Me.BtnSave.Enabled = True Then
                    BtnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then

                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Insert Then
                btnadd_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.U AndAlso e.Alt Then
                If Me.BtnSave.Text = "&Update" Then
                    If Me.BtnSave.Enabled = False Then
                        RemoveHandler Me.BtnSave.Click, AddressOf Me.BtnSave_Click
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmProductionStore_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Dim StockInConfigration As String = "" ''1596
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
            End If
            If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
                flgLoadAllItems = getConfigValueByType("LoadAllItemsInSales")
            End If
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            ''start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161

            If Not getConfigValueByType("StockInConfigration").ToString = "Error" Then ''1596
                StockInConfigration = getConfigValueByType("StockInConfigration").ToString
            End If
            ' ''TASK TFS4544
            'If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            '    ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            'End If
            ' ''END TFS4544

            'ShowInformationMessage(StockInConfigration)
            If StockInConfigration.Equals("Disabled") Then
                pnlBatchNo.Visible = False
                pnlMoving.Location = New Point(566, 44)
                'pnlBatchNo.Location = New Point(641, 34)
            ElseIf StockInConfigration.Equals("Enabled") Then
                pnlBatchNo.Visible = True
                pnlMoving.Location = New Point(641, 34)
                'pnlBatchNo.Location = New Point(568, 45)
            Else
                pnlBatchNo.Visible = True
                pnlMoving.Location = New Point(641, 34)
                'pnlBatchNo.Location = New Point(568, 45)
            End If
            FillCombos("ProductionStore")
            FillCombos("CostCenter")
            FillCombos("Customer")
            'FillCombos("SO")
            FillCombos("Location")
            'Task No 2555 Append One Line Code To Fill UM Combo Box
            FillCombos("UM")
            'FillCombos("Item")
            'FillCombos("Plan")
            FillCombos("ArticlePack")
            FillCombos("IssuedStore") 'TAsk:2690 Get Store Issuance
            FillCombos("Employee")
            FillCombos("CGSAccount")
            FillCombos("Plan")
            IsFormLoaded = True

            ReSetControls()
            Me.GetAllRecords()

            Get_All(frmModProperty.Tags)

            'TFS3360
            UltraDropDownSearching(cmbVendor, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbCGSAccount, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            cmbProductionStore.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub frmProductionStore_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub
    Private Sub EditRecord()
        Try
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpProductionDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dtpProductionDate.Enabled = False
                Else
                    Me.dtpProductionDate.Enabled = True
                End If
            Else
                Me.dtpProductionDate.Enabled = True
            End If

            If Me.grdSave.RowCount = 0 Then Exit Sub
            Me.BtnSave.Text = "&Update"
            setEditMode = True
            'FillCombos("Plan")
            'RemoveHandler Me.cmbPlan.SelectedIndexChanged, AddressOf cmbPlan_SelectedIndexChanged
            MasterId = Me.grdSave.GetRow.Cells(EnumGridMaster.Production_ID).Value
            Me.txtProductionNo.Text = Me.grdSave.GetRow.Cells(EnumGridMaster.Production_no).Text
            'Me.dtpProductionDate.Value = Me.grdSave.GetRow.Cells(EnumGridMaster.Production_date).Value

            'Task 1592
            If Me.grdSave.GetRow.Cells(EnumGridMaster.Production_date).Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
                dtpProductionDate.MaxDate = dtpProductionDate.Value.AddMonths(3)
                dtpProductionDate.Value = CType(Me.grdSave.GetRow.Cells(EnumGridMaster.Production_date).Value, Date)
            Else
                dtpProductionDate.Value = CType(Me.grdSave.GetRow.Cells(EnumGridMaster.Production_date).Value, Date)
            End If
            Me.cmbProductionStore.SelectedValue = Me.grdSave.GetRow.Cells(EnumGridMaster.Production_store).Value
            Me.cmbVendor.Value = Me.grdSave.GetRow.Cells(EnumGridMaster.Customer).Value
            Me.cmbOrderNo.SelectedValue = Me.grdSave.GetRow.Cells(EnumGridMaster.Order_No).Value
            Me.cmbCostCenter.SelectedValue = Me.grdSave.GetRow.Cells(EnumGridMaster.Project).Value
            Me.txtIGPNo.Text = Me.grdSave.GetRow.Cells(EnumGridMaster.IGPNo).Text.ToString
            Me.txtRemarks.Text = Me.grdSave.GetRow.Cells(EnumGridMaster.Remarks).Text
            If (Me.grdSave.GetRow.Cells(EnumGridMaster.Post).Value = "UnPosted") Then
                Me.chkPost.Checked = False
            Else
                Me.chkPost.Checked = True
            End If
            Me.cmbCGSAccount.Value = Val(Me.grdSave.GetRow.Cells("CGSAccountId").Value.ToString)
            Me.cmbPlan.SelectedValue = Val(Me.grdSave.GetRow.Cells("PlanId").Value.ToString)
            FillCombos("Ticket")
            Me.cmbTicket.SelectedValue = Val(Me.grdSave.GetRow.Cells("PlanTicketId").Value.ToString)
            Me.cmbPlan.Enabled = False
            Me.cmbTicket.Enabled = False
            If cmbPlan.SelectedValue Is Nothing Then
                Dim dt As DataTable = CType(Me.cmbPlan.DataSource, DataTable)
                dt.AcceptChanges()
                Dim dr As DataRow = dt.NewRow
                dr(0) = Val(Me.grdSave.GetRow.Cells("PlanId").Value.ToString)
                dr(1) = Me.grdSave.GetRow.Cells("PlanNo").Value.ToString
                dt.Rows.Add(dr)
                dt.AcceptChanges()
            End If
            'Me.cmbPlan.SelectedValue = Val(Me.grdSave.GetRow.Cells("PlanId").Value.ToString)
            Me.cmbIssuedStore.SelectedValue = Me.grdSave.GetRow.Cells(EnumGridMaster.IssuedSTore).Value
            Me.txtStoreIssuenceNo.Text = Me.grdSave.GetRow.Cells(EnumGridMaster.RefDocument).Text.ToString
            Me.cmbEmployee.SelectedValue = Val(Me.grdSave.GetRow.Cells(EnumGridMaster.EmployeeID).Value.ToString)
            ClearItem()
            If blnUpdateAll = False Then Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.DisplayRecord(MasterId)
            '//Start TASK # 1592
            '24-OCT-2017: Ayesha Rehman: If user dont have update rights then btnsave should not be enable true
            If BtnSave.Enabled = True Then
                If grdSave.GetRow.Cells(EnumGridMaster.Production_date).Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
                    Me.BtnSave.Enabled = False
                    ErrorProvider1.SetError(Me.lbldate, "Future Date can not be edit")
                    ErrorProvider1.BlinkRate = 1000
                    ErrorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
                Else
                    Me.BtnSave.Enabled = True
                    ErrorProvider1.Clear()
                End If
            End If
            'End Task # 1592
            Me.cmbEmployee.SelectedValue = Val(Me.grdSave.GetRow.Cells(EnumGridMaster.EmployeeID).Value.ToString)
            Previouse_Amount = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(EnumGrd.Total), Janus.Windows.GridEX.AggregateFunction.Sum)
            'AddHandler Me.cmbPlan.SelectedIndexChanged, AddressOf cmbPlan_SelectedIndexChanged
            If getConfigValueByType("StoreIssuenceWithProduction").ToString = "True" Then
                Me.cmbIssuedStore.Visible = True
                Me.lblIssuedStore.Visible = True
            Else
                Me.lblIssuedStore.Visible = False
                Me.cmbIssuedStore.Visible = False
            End If
            Me.lblPrintStatus.Text = "Print Status: " & Me.grdSave.GetRow.Cells("Print Status").Text.ToString
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.btnDelete.Visible = True
            Me.btnPrint.Visible = True
            ''''''''''''''''''''''''''

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ClearItem()
        Try

            'Me.cmbLocation.SelectedIndex = 0
            'Task No 2555 Made A Coment Of This Qouted Line "Me.cmbItem.Rows(0).Activate() to select the same value as we added to grid "
            'Me.cmbItem.Rows(0).Activate()
            'Task End
            'Me.cmbUnit.SelectedIndex = 0 ''TFS1772
            Me.cmbRetailPrice.SelectedIndex = -1
            Me.txtPackQty.Text = String.Empty
            Me.txtQty.Text = String.Empty
            Me.txtDim1.Text = String.Empty 'TFS1772
            Me.txtDim2.Text = String.Empty  'TFS1772
            Me.txtRate.Text = String.Empty
            Me.txtAmount.Text = String.Empty
            Me.txtTotalQty.Text = String.Empty ''TASK-408
            Me.txtPackRate.Text = String.Empty
            'Me.txtComments.Text = String.Empty
            Me.cmbItem.Focus()
            If Me.pnlWeight.Visible = True Then
                Me.pnlWeight.Visible = True
            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdSave_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSave.DoubleClick
        ''Task# A2-10-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
        Try
            If grdSave.Row < 0 Then
                Exit Sub
            Else
                If Me.grdDetail.RowCount > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                    EditRecord()
                Else
                    EditRecord()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        ''End Task# A2-10-06-2015
    End Sub
    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try
            '1
            'Pack_Qty = 0
            'If Me.cmbUnit.SelectedIndex > 0 Then
            '    Pack_Qty = GetPackQty(cmbItem.Value)
            '    Me.txtPackQty.Text = Pack_Qty
            '    Me.txtPackQty.Enabled = True
            'Else
            '    Me.txtPackQty.Text = 1
            '    Me.txtPackQty.Enabled = False
            'End If
            'Me.txtAmount.Text = ((Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)) * Val(txtRate.Text))


            '2
            'Pack_Qty = 0
            'If Me.cmbUnit.SelectedIndex = 0 Then
            '    Pack_Qty = GetPackQty(cmbItem.Value)
            '    Me.txtPackQty.Text = Pack_Qty
            '    Me.txtPackQty.Enabled = True
            '    'Me.txtPackQty.Text = 1
            '    'Me.txtPackQty.Enabled = False
            'Else
            '    Pack_Qty = GetLoosePackQty(cmbItem.Value)
            '    Me.txtPackQty.Text = Pack_Qty
            '    Me.txtPackQty.Enabled = True
            '    'Me.txtPackQty.Text = 1
            '    'Me.txtPackQty.Enabled = False
            'End If
            ''Me.txtAmount.Text = ((Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)) * Val(txtRate.Text))
            'Me.txtAmount.Text = (Val(Me.txtQty.Text) * Val(txtRate.Text))





            If Me.cmbUnit.Text = "Loose" Then
                txtAmount.Text = Val(txtQty.Text) * Val(txtRate.Text)
                txtPackQty.Text = 1
                Me.txtPackQty.Enabled = False
                Me.txtPackQty.TabStop = False
                ''TASK TFS1496 addition of PackPrice
                Me.txtPackRate.Enabled = False
                If Me.cmbItem.ActiveRow IsNot Nothing Then
                    Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbLocation.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))
                End If
            Else
                ''Start TFS4161
                If IsPackQtyDisabled = True Then
                    Me.txtPackQty.Enabled = False
                    Me.txtPackQty.TabStop = False
                    ''TASK TFS1496 addition of PackPrice
                    Me.txtPackRate.Enabled = False
                    Me.txtTotalQty.Enabled = False
                Else
                    Me.txtPackQty.Enabled = True
                    Me.txtPackQty.TabStop = True
                    ''TASK TFS1496 addition of PackPrice
                    Me.txtPackRate.Enabled = True
                    Me.txtTotalQty.Enabled = True
                End If
                ''End TFS4161
                'Me.txtPackQty.Enabled = True
                'Dim objCommand As New OleDbCommand
                'Dim objCon As OleDbConnection
                'Dim objDataAdapter As New OleDbDataAdapter
                'Dim objDataSet As New DataSet

                'objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

                'If objCon.State = ConnectionState.Open Then objCon.Close()

                'objCon.Open()
                'objCommand.Connection = objCon
                'objCommand.CommandType = CommandType.Text


                'objCommand.CommandText = "Select isnull(PackQty,0) as PackQty from ArticleDefTable where ArticleID = " & cmbItem.ActiveRow.Cells(0).Value
                'txtPackQty.Text = objCommand.ExecuteScalar()
                If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
                    Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
                End If
                txtAmount.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text))
                ''TASK TFS1490
                If Me.cmbItem.ActiveRow IsNot Nothing Then
                    Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbLocation.SelectedValue, IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")))
                End If
                ''END TASK TFS1490
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            flgSelectedProject = False

            ReSetControls()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub DocNo()
        Try
            Me.txtProductionNo.Text = GetDocumentNo() 'GetNextDocNo("PRD", 6, "ProductionMasterTable", "Production_No").ToString
            Me.txtStoreIssuenceNo.Text = GetDocumentNo1() 'GetNextDocNo("I1", 6, "DisPatchMasterTable", "DispatchNo").ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdDetail_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                Me.grdDetail.CurrentRow.Delete()
                Me.grdDetail.UpdateData() 'Task:2613 Set Grid Detail Update
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub AddItemToGrid()
        Try
            Me.grdDetail.UpdateData()
            dtProductions = CType(Me.grdDetail.DataSource, DataTable)
            Dim dr As DataRow
            dr = dtProductions.NewRow
            dr.Item(EnumGrd.Location_Id) = Me.cmbLocation.SelectedValue
            dr.Item(EnumGrd.ArticleDefId) = Me.cmbItem.Value
            dr.Item(EnumGrd.Location_Name) = Me.cmbLocation.Text
            dr.Item(EnumGrd.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text
            dr.Item(EnumGrd.ArticleDescription) = Me.cmbItem.ActiveRow.Cells("Item").Text
            dr.Item(EnumGrd.ArticleSizeName) = Me.cmbItem.ActiveRow.Cells("Size").Text
            dr.Item(EnumGrd.UnitName) = Me.cmbItem.ActiveRow.Cells("UOM").Value.ToString
            dr.Item(EnumGrd.ArticleColorName) = Me.cmbItem.ActiveRow.Cells("Combination").Text
            dr.Item(EnumGrd.ArticleSize) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            dr.Item(EnumGrd.PackQty) = IIf(Val(Me.txtPackQty.Text) = 0, 1, Val(Me.txtPackQty.Text))
            ''TFS1772
            If Me.pnlWeight.Visible = True Then
                dr.Item(EnumGrd.Dim1) = Val(Me.txtDim1.Text)
                dr.Item(EnumGrd.Dim2) = Val(Me.txtDim2.Text)
            Else
                dr.Item(EnumGrd.Dim1) = 0
                dr.Item(EnumGrd.Dim2) = 0
            End If
            ''End TFS1772
            'dr.Item(EnumGrd.Qty) = Val(Me.txtQty.Text)
            'IIf(Val(Me.txtStock.Text) < Val(txtQty.Text), Me.txtStock.Text, txtQty.Text)
            dr.Item(EnumGrd.Qty) = Val(Me.txtQty.Text)
            dr.Item(EnumGrd.CurrentRate) = Val(Me.txtRate.Text)
            ''TASK TFS1496 addition of PackPrice
            dr.Item(EnumGrd.PackPrice) = Val(Me.txtPackRate.Text)
            ''TASK-408 Multiply rate with total quantity
            'dr.Item(EnumGrd.Total) = IIf(Me.cmbUnit.SelectedIndex = 0, (Val(txtQty.Text) * Val(txtRate.Text)), ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)))
            dr.Item(EnumGrd.Total) = (Val(txtTotalQty.Text) * Val(txtRate.Text)) ''TASK-408
            dr.Item(EnumGrd.Comments) = Me.txtComments.Text
            ''TASk NO 1616 Add Batch No Ayesha Rehman
            dr.Item(EnumGrd.BatchNo) = Me.cmbBatch.Text ''TFS1596
            ''Task No 2555 Append One Line Code fro UOM
            dr.Item(EnumGrd.Uom) = Me.CmbUm.Text
            dr.Item(EnumGrd.Pack_Desc) = Me.cmbUnit.Text.ToString
            ''TASk NO 1616 Add Retail Price Ayesha Rehman
            dr.Item(EnumGrd.RetailPrice) = Val(Me.cmbRetailPrice.Text)
            ''TASk NO 1616 Add Expiry Date Ayesha Rehman
            dr.Item(EnumGrd.ExpiryDate) = Me.dtpExpiryDate.Value


            'Dim PurchaseAccount As Integer = Convert.ToInt32(UtilityDAL.GetConfigValue("PurchaseDebitAccount").ToString)
            'Dim StoreAccount As Integer = 0I 'Convert.ToInt32(UtilityDAL.GetConfigValue("StoreCreditAccount").ToString)

            'If ProductionStore.CGSAccountId > 0 Then
            '    StoreAccount = ProductionStore.CGSAccountId
            'Else
            '    StoreAccount = Convert.ToInt32(UtilityDAL.GetConfigValue("StoreCreditAccount").ToString)
            'End If


            dr.Item(EnumGrd.AccountId) = Val(Me.cmbItem.ActiveRow.Cells("AccountId").Value.ToString)
            dr.Item(EnumGrd.CGSAccountId) = Val(Me.cmbItem.ActiveRow.Cells("CGSAccountId").Value.ToString)
            dr.Item(EnumGrd.TotalQty) = Val(Me.txtTotalQty.Text) ''TASK-408




            dtProductions.Rows.InsertAt(dr, 0)
            dtProductions.AcceptChanges() 'Task:2612 Set Status
            Me.grdDetail.UpdateData() 'Task:2612 Set Status
            ClearItem()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If Not IsValidateAddItemToGrd() = True Then Exit Sub
            AddItemToGrid()
            Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.Dropdown)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function IsValidateAddItemToGrd() As Boolean
        Try
            '' Task:1772 Validation of New fields Dim1 ,Dim2
            If Me.pnlWeight.Visible = True Then
                If Val(Me.txtDim1.Text) <= 0 AndAlso Val(Me.txtDim1.Text) <= 0 Then
                    ShowErrorMessage("Dimention 1 and Dimention 2 must be greater than 0")
                    Me.txtDim1.Focus()
                    Return False
                End If
            End If
            ''End TASK-1772
            If Me.cmbItem.ActiveRow.Cells(0).Value < 1 Then
                ShowErrorMessage("Please select item")
                Me.cmbItem.Focus()
                Return False
            End If
            If Val(Me.txtQty.Text) = 0 Or Val(Me.txtQty.Text) < 0 Then
                ShowErrorMessage("Quantity is not grater than 0")
                Me.txtQty.Focus()
                Return False
            End If
            If Val(Me.txtRate.Text) < 0 Then
                ShowErrorMessage("Rate is not grater than 0")
                Me.txtRate.Focus()
                Return False
            End If
            ''TASK-408
            If Val(Me.txtTotalQty.Text) < 0 Then
                ShowErrorMessage("Total quantity is not grater than 0")
                Me.txtRate.Focus()
                Return False
            End If
            ''End TASK-408

            ''TASK-1616
            If Val(Me.cmbRetailPrice.Text) < 0 Then
                ShowErrorMessage("Retail Price is not grater than 0")
                Me.cmbRetailPrice.Focus()
                Return False
            End If
            ''End TASK-1616
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Altered By Ali Ansari Task#20150505
    Function IsValidateEngineChasis() As Boolean
        Try

            For intI As Integer = 0 To grdDetail.RowCount
                For intJ As Integer = intI + 1 To grdDetail.RowCount
                    If grdDetail.GetRows(intI).Cells(EnumGrd.EngineNo).Value.ToString = grdDetail.GetRows(intJ).Cells(EnumGrd.EngineNo).Value.ToString Then
                        MsgBox("Duplicate: " & grdDetail.GetRows(intJ).Cells(EnumGrd.EngineNo).Value)
                        Return False
                        Exit Function
                    End If
                Next
            Next

            For intI As Integer = 0 To grdDetail.RowCount
                For intJ As Integer = intI + 1 To grdDetail.RowCount
                    If grdDetail.GetRows(intI).Cells(EnumGrd.ChasisNo).Value.ToString = grdDetail.GetRows(intJ).Cells(EnumGrd.ChasisNo).Value.ToString Then
                        MsgBox("Duplicate: " & grdDetail.GetRows(intJ).Cells(EnumGrd.ChasisNo).Value)
                        Return False
                        Exit Function
                    End If
                Next
            Next

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbVendor_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbVendor.KeyDown
        ''TFS1781 : Ayesha Rehman :Added for Selection of Customer or Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.AccountType = "'Customer'"
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
    Private Sub cmbVendor_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbVendor.RowSelected
        Try
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            FillCombos("SO")
            If Not Me.cmbOrderNo.SelectedIndex = -1 Then Me.cmbOrderNo.SelectedIndex = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.BtnSave.Enabled = False Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            'If flgDateLock = True Then
            '    If CDate(MyDateLock.ToString("yyyy-M-d 00:00:00")) >= CDate(Me.dtpProductionDate.Value.ToString("yyyy-M-d 00:00:00")) Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            If IsDateLock(Me.dtpProductionDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If
            If Me.dtpProductionDate.Value <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("Your can not change this becuase financial year is closed")
                Me.dtpProductionDate.Focus()
                Exit Sub
            End If

            If Not IsValidate() = True Then Exit Sub
            Me.grdDetail.Update()
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
                'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Save() = True Then
                    'EmailSave()

                    '' Made changes to  against TASK TFS1462 on 13-09-2017
                    Dim Printing As Boolean = False
                    'Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                    Dim DirectVoucherPrinting As Boolean
                    DirectVoucherPrinting = Convert.ToBoolean(getConfigValueByType("DirectVoucherPrinting").ToString)
                    If Printing = True Or DirectVoucherPrinting = True Then
                        If msg_Confirm("Do you want to print", Printing, DirectVoucherPrinting) = True Then
                            Dim Print1 As Boolean = frmMessages.Print
                            Dim PrintVoucher As Boolean = frmMessages.DirectVoucherPrinting
                            'If Print1 = True Then
                            '    Me.PurchaseReturnToolStripMenuItem_Click(Nothing, Nothing)
                            'End If
                            If PrintVoucher = True Then
                                GetVoucherPrint(Me.txtProductionNo.Text, Me.Name, "PKR", 1, True)
                            End If
                        End If
                    End If
                    ''END TASK TFS1462


                    dtDataEmail = CType(Me.grdDetail.DataSource, DataTable)
                    dtDataEmail.AcceptChanges()
                    flgSelectedProject = True
                    'msg_Information(str_informSave)

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
                    ReSetControls()
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Update1() = True Then


                    '' Made changes to  against TASK TFS1462 on 13-09-2017
                    Dim Printing As Boolean = False
                    'Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                    Dim DirectVoucherPrinting As Boolean
                    DirectVoucherPrinting = Convert.ToBoolean(getConfigValueByType("DirectVoucherPrinting").ToString)
                    If Printing = True Or DirectVoucherPrinting = True Then
                        If msg_Confirm("Do you want to print", Printing, DirectVoucherPrinting) = True Then
                            Dim Print1 As Boolean = frmMessages.Print
                            Dim PrintVoucher As Boolean = frmMessages.DirectVoucherPrinting
                            'If Print1 = True Then
                            '    Me.PurchaseReturnToolStripMenuItem_Click(Nothing, Nothing)
                            'End If
                            If PrintVoucher = True Then
                                GetVoucherPrint(Me.txtProductionNo.Text, Me.Name, "PKR", 1, True)
                            End If
                        End If
                    End If
                    ''END TASK TFS1462
                    'EmailSave()
                    Me.grdDetail.UpdateData()
                    dtDataEmail = CType(Me.grdDetail.DataSource, DataTable)
                    dtDataEmail.AcceptChanges()
                    flgSelectedProject = True
                    ' msg_Information(str_informUpdate)


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
                    ReSetControls()
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            blnEditMode = True
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Function GetPackQty(ByVal ItemId As Integer) As Integer
        Try
            Dim str As String = String.Empty
            Dim dt As New DataTable
            str = "Select PackQty from ArticleDefTable WHERE ArticleId=" & ItemId & ""
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetLoosePackQty(ByVal ItemId As Integer) As Integer
        Try
            Dim str As String = String.Empty
            Dim dt As New DataTable
            str = "Select isnull(LargestPackQty,0) as LargestPackQty from ArticleDefTable WHERE ArticleId=" & ItemId & ""
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbItem_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbItem.KeyDown
        Try
            ''TFS1858 : Ayesha Rehman :Item dropdown shall be searchable
            If e.KeyCode = Keys.F1 Then
                If flgCompanyRights = True Then
                    frmItemSearch.CompanyId = MyCompanyId
                End If
                If getConfigValueByType("ArticleFilterByLocation").ToString = "True" Then
                    If GetRestrictedItemFlg(Me.cmbLocation.SelectedValue) = True Then
                        frmItemSearch.LocationId = Me.cmbLocation.SelectedValue
                    Else
                        frmItemSearch.LocationId = 0
                    End If
                End If

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
    Private Sub cmbItem_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            Else
                Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbLocation.SelectedValue))
            End If

            If IsFormLoaded = True Then
                Pack_Qty = 0
                If Me.cmbUnit.SelectedIndex > 0 Then
                    Pack_Qty = GetPackQty(cmbItem.Value)
                    Me.txtPackQty.Text = Pack_Qty
                    Me.txtPackQty.Enabled = True
                    ''TFS1772
                ElseIf Me.cmbUnit.SelectedIndex > 0 AndAlso CType(Me.cmbItem.ActiveRow.Cells("MultiDimentionalItem").Value, Boolean) = True Then
                    Me.pnlWeight.Visible = True
                    Me.cmbUnit.Enabled = False
                    Me.txtPackQty.ReadOnly = True
                    Me.txtPackQty.Text = 1
                Else
                    Me.txtPackQty.Text = 1
                    'Me.txtPackQty.Enabled = False
                End If

                If Me.cmbItem.ActiveRow.Cells(0).Value > 0 Then
                    'Me.txtRate.Text = Convert.ToDouble(Me.cmbItem.ActiveRow.Cells("Price").Text)
                    If getConfigValueByType("ApplyCostSheetRateOnProduction").ToString = "True" Then
                        GetProductionPrice(Me.cmbItem.SelectedRow.Cells("MasterId").Value.ToString) 'TASK-TFS-46 Get Production Price 
                    Else
                        Me.txtRate.Text = Convert.ToDouble(Me.cmbItem.ActiveRow.Cells("Price").Text)
                    End If
                    If Val(Me.txtQty.Text) > 0 Or Val(Me.txtQty.Text) < 0 Then
                        Me.txtQty.Text = Val(Me.txtQty.Text)
                    Else
                        Me.txtQty.Text = 1
                    End If
                Else
                    Me.txtRate.Text = 0
                    If Val(Me.txtQty.Text) > 0 Or Val(Me.txtQty.Text) < 0 Then
                        Me.txtQty.Text = Val(Me.txtQty.Text)
                    Else
                        Me.txtQty.Text = 1
                    End If
                End If
            End If
            Me.txtAmount.Text = Math.Round(((Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPackQty_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackQty.Leave
        'Try
        '    Me.txtAmount.Text = Math.Round(((Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Private Sub txtQty_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.Leave
        'Try
        '    Me.txtAmount.Text = Math.Round(((Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Private Sub txtRate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate.Leave
        Try
            Me.txtAmount.Text = Math.Round((Val(Me.txtTotalQty.Text) * Val(txtRate.Text)), DecimalPointInValue)

            'Me.txtAmount.Text = Math.Round(((Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            'If flgDateLock = True Then
            '    If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpProductionDate.Value.ToString("yyyy-M-d 00:00:00") Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            If IsDateLock(Me.dtpProductionDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
            End If

            If Not IsValidate() = True Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) Then Exit Sub
            If Delete() Then DialogResult = Windows.Forms.DialogResult.Yes
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
            Me.grdSave.CurrentRow.Delete()

            ReSetControls()
            'MsgBox("Record deleted successfully", MsgBoxStyle.Information, str_MessageHeader)
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
            'msg_Information(str_informDelete)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    'Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
    '    Try
    '        AddRptParam("@ProductionId", Me.grdSave.GetRow.Cells(EnumGridMaster.Production_Id).Value)
    '        ShowReport("rptProductionStore")
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub rdoName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoName.CheckedChanged, rdoCode.CheckedChanged
        Try
            If Not IsFormLoaded = True Then Exit Sub
            If rdoName.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
    '    Try
    '        If Me.UltraTabControl1.SelectedTab.Index = 0 Then
    '            Me.btnLoadAll.Visible = False
    '            Me.ToolStripButton1.Visible = False
    '            Me.ToolStripSeparator2.Visible = False
    '        Else
    '            Me.btnLoadAll.Visible = True
    '            Me.ToolStripButton1.Visible = True
    '            Me.ToolStripSeparator2.Visible = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''END TFS4544

            Dim id As Integer = 0
            id = Me.cmbCostCenter.SelectedValue
            FillCombos("CostCenter")
            Me.cmbCostCenter.SelectedValue = id
            id = Me.cmbProductionStore.SelectedValue
            FillCombos("ProductionStore")
            Me.cmbProductionStore.SelectedValue = id
            id = Me.cmbVendor.Value
            FillCombos("Customer")
            Me.cmbVendor.Value = id
            id = Me.cmbOrderNo.SelectedValue
            FillCombos("SO")
            Me.cmbOrderNo.SelectedValue = id

            id = Me.cmbItem.Value
            FillCombos("Item")
            Me.cmbItem.Value = id

            id = Me.cmbEmployee.SelectedIndex
            FillCombos("Employee")
            Me.cmbEmployee.SelectedIndex = id

            id = Me.cmbCGSAccount.ActiveRow.Cells(0).Value
            FillCombos("CGSAccount")
            Me.cmbCGSAccount.Value = id


            id = Me.cmbPlan.SelectedValue
            FillCombos("Plan")
            Me.cmbPlan.SelectedValue = id

            FillCombos("grdLocation")
            'Task No 2555 Filling The ComboBox Of UM
            FillCombos("UM")

            If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
                flgLoadAllItems = getConfigValueByType("LoadAllItemsInSales")
            End If

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItems = getConfigValueByType("ArticleFilterByLocation")
            End If
            ''start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    'Private Sub btnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    Try
    '        Me.GetAllRecords("All")
    '        Me.DisplayRecord(-1)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub
    Private Sub ProductionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductionToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSave.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSave.GetRow.Cells("Production_No").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            AddRptParam("@ProductionId", Me.grdSave.GetRow.Cells(EnumGridMaster.Production_ID).Value)
            ShowReport("rptProductionStore")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub InwardGatepassToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InwardGatepassToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSave.RowCount = 0 Then Exit Sub
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = grdSave.GetRow.Cells("Production_No").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            AddRptParam("@ProductionId", Me.grdSave.GetRow.Cells(EnumGridMaster.Production_ID).Value)
            ShowReport("rptProductionStoreInwardGatepass")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AddNewProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewProjectToolStripMenuItem.Click
        Try
            Dim id As Integer = 0
            frmAddCostCenter.ShowDialog()
            id = Me.cmbCostCenter.SelectedValue
            FillCombos("CostCenter")
            Me.cmbCostCenter.SelectedValue = id
            frmAddCostCenter.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub cmbItem_RowSelected(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If Me.cmbItem.IsItemInList = False Then
                Me.txtStock.Text = 0
                Exit Sub
            Else
                If Me.cmbItem.Value Is Nothing Then Exit Sub
                Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbLocation.SelectedValue))
            End If
            FillCombos("ArticlePack")
            '' Task No 1616 30-10-2017 Select The RetailPrice According to Item Selected in cmbItem
            FillCombos("Retail Price")
            '' Task No 1596 29-11-2017 Select The BatchNo According to Item Selected in cmbItem
            FillCombos("BatchNo")
            ''Start TFS1772 :Ayesha Rehman
            If Me.cmbItem.ActiveRow.Index > 0 AndAlso CType(Me.cmbItem.ActiveRow.Cells("MultiDimentionalItem").Value, Boolean) = True Then
                Me.pnlWeight.Visible = True
                Me.cmbUnit.SelectedIndex = 1
                Me.cmbUnit.Enabled = False
                Me.txtPackQty.ReadOnly = True
            Else
                Me.pnlWeight.Visible = False
                Me.cmbUnit.Enabled = True
                Me.txtPackQty.ReadOnly = False
            End If
            ''End TFS1772
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbLocation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLocation.SelectedIndexChanged
        Try
            If IsFormLoaded = True Then
                If flgLocationWiseItems = True Then FillCombos("Item")
                If flgLoadAllItems = True Then
                    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                        r.BeginEdit()
                        r.Cells(EnumGrd.Location_Id).Value = Me.cmbLocation.SelectedValue
                        r.EndEdit()
                    Next
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ApplySecurityAndRights(Optional ByVal condition As String = "")
        Try
            'Dim dt As DataTable = GetFormRights(EnumForms.frmProductionStore)
            'If Not dt Is Nothing Then
            '    If Not dt.Rows.Count = 0 Then
            '        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
            '            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
            '        Else
            '            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
            '        End If
            '        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
            '        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
            '        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
            '    End If
            'End If
            'UserPostingRights = GetUserPostingRights(LoginUserId)
            'If UserPostingRights = True Then
            '    Me.chkPost.Visible = True
            'Else
            '    Me.chkPost.Visible = False
            '    Me.chkPost.Checked = False
            'End If
            'GetSecurityByPostingUser(UserPostingRights, BtnSave, btnDelete)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdDetail.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Production Store"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("PRD" + "-" + Microsoft.VisualBasic.Right(Me.dtpProductionDate.Value.Year, 2) + "-", "ProductionMasterTable", "Production_No")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("PRD" & "-" & Format(Me.dtpProductionDate.Value, "yy") & Me.dtpProductionDate.Value.Month.ToString("00"), 4, "ProductionMasterTable", "Production_No")
            Else
                Return GetNextDocNo("PRD", 6, "ProductionMasterTable", "Production_no")
            End If
            'Else
            'Return ""
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetDocumentNo1() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("I" + "-" + Microsoft.VisualBasic.Right(Me.dtpProductionDate.Value.Year, 2) + "-", "DispatchMasterTable", "DispatchNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("I" & "-" & Format(Me.dtpProductionDate.Value, "yy") & Me.dtpProductionDate.Value.Month.ToString("00"), 4, "DispatchMasterTable", "DispatchNo")
            Else
                Return GetNextDocNo("I", 6, "DispatchMasterTable", "DispatchNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetDocumentNo2() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("DN" + "-" + Microsoft.VisualBasic.Right(Me.dtpProductionDate.Value.Year, 2) + "-", "DispatchMasterTable", "DispatchNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("DN" & "-" & Format(Me.dtpProductionDate.Value, "yy") & Me.dtpProductionDate.Value.Month.ToString("00"), 4, "DispatchMasterTable", "DispatchNo")
            Else
                Return GetNextDocNo("DN", 6, "DispatchMasterTable", "DispatchNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnPrintVoucher.Enabled = True
                Me.btnPrintVoucher1.Enabled = True
                'Task 1592 Apply future date rights
                IsDateChangeAllowed = True
                dtpProductionDate.MaxDate = Date.Today.AddMonths(3)
                Exit Sub
            End If
            If RegisterStatus = EnumRegisterStatus.Expired Then
                Me.BtnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                Me.btnPrintVoucher.Enabled = False
                Me.btnPrintVoucher1.Enabled = False
                'Me.PrintListToolStripMenuItem.Enabled = False
                'PrintToolStripMenuItem.Enabled = False
                'Task 1592 Apply future date rights
                IsDateChangeAllowed = False
                DateChange(False)

                Exit Sub
            End If

            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                Me.chkPost.Visible = False
                Me.btnPrintVoucher.Enabled = False
                Me.btnPrintVoucher1.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False  ''TFS1823
                CtrlGrdBar2.mGridExport.Enabled = False  ''TFS1823
                CtrlGrdBar1.mGridChooseFielder.Enabled = False 'Task:2406 Added Field Chooser Security
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                'Task 1592 Apply future date rights
                IsDateChangeAllowed = False
                DateChange(False)


                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        CtrlGrdBar2.mGridExport.Enabled = True   ''TFS1823
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True
                        'Task:1592 Added Future Date Rights
                    ElseIf RightsDt.FormControlName = "Future Transaction" Then
                        IsDateChangeAllowed = True
                        DateChange(True)
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    ElseIf RightsDt.FormControlName = "Print Voucher" Then
                        Me.btnPrintVoucher.Enabled = True
                        Me.btnPrintVoucher1.Enabled = True
                    End If
                Next
            Else
                UserPostingRights = GetUserPostingRights(LoginUserId)
                If UserPostingRights = True Then
                    Me.chkPost.Visible = True
                Else
                    Me.chkPost.Visible = False
                    Me.chkPost.Checked = False
                End If
                GetSecurityByPostingUser(UserPostingRights, BtnSave, btnDelete)
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    'Task:1592 Added Future Date Rights
    Private Sub DateChange(ByRef IsDateChangeAllowed As Boolean)
        Try
            If IsDateChangeAllowed = False Then
                dtpProductionDate.MaxDate = DateTime.Now.ToString("yyyy/M/d 23:59:59")
            Else
                dtpProductionDate.MaxDate = Date.Today.AddMonths(3)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'END TASk:1592
    Private Sub btnAddNewitem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewitem.Click
        Try
            Call frmAddItem.ShowDialog()
            Call FillCombos("Item")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function EmailSave()
        EmailSave = Nothing
        Dim flg As Boolean = False
        'If Me.cmbVendor.ActiveRow Is Nothing Then Exit Function
        If IsEmailAlert = True Then
            Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name='frmProductionStore' AND EmailAlert=1")
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
                Email.Subject = "Production " & setVoucherNo & ""
                'Email.Body = "Production " _
                '& " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Previouse_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
                Dim strBody As String = String.Empty
                Dim str As String = String.Empty
                strBody = "Document No: " & setVoucherNo & "" & Chr(10) & "Document Date:" & setVoucherDate.Date & "" & Chr(10) & "" & Chr(10) & "" & Chr(10) & ""
                strBody += StringFixedLength("Article Description", 50) & " " & StringFixedLength("Qty", 10) & " " & StringFixedLength("Price", 10) & " " & StringFixedLength("Amount", 10) & Chr(10)
                For Each r As DataRow In dtDataEmail.Rows
                    If Val(r("Qty").ToString) > 0 Then
                        strBody += StringFixedLength(r.Item("ArticleDescription").ToString, 50) & " " & StringFixedLength(Math.Round(Val(r.Item("Qty").ToString), 2), 10) & " " & StringFixedLength(Math.Round(Val(r.Item("CurrentRate").ToString), 2), 10) & " " & StringFixedLength(Math.Round(Val(r.Item("Total").ToString), 2), 10) & Chr(10)
                    End If
                Next
                Email.Body = strBody
                Email.Status = "Pending"
                Call New MailSentDAL().Add(Email)
                strBody = String.Empty
            End If
        End If

        Return EmailSave

    End Function
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
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetAllRecords("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearchEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchEdit.Click
        Try
            btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ProductionInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductionInvoiceToolStripMenuItem.Click
        Try
            ProductionToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub InwardGatepassToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InwardGatepassToolStripMenuItem1.Click
        Try
            InwardGatepassToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearchDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDelete.Click
        Try
            btnDelete_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.GetAllRecords("All")
            Me.DisplayRecord(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            If Not Me.cmbSearchLocation.Items.Count > 0 Then
                FillCombos("SearchLocation")
                If Not Me.cmbSearchLocation.SelectedIndex = -1 Then Me.cmbSearchLocation.SelectedIndex = 0
            Else
                If Not Me.cmbSearchLocation.SelectedIndex = -1 Then Me.cmbSearchLocation.SelectedIndex = 0
            End If
            If Not Me.cmbSearchCostCenter.Items.Count > 0 Then
                FillCombos("SearchCostCenter")
                If Not Me.cmbSearchCostCenter.SelectedIndex = -1 Then Me.cmbSearchLocation.SelectedIndex = 0
            Else
                If Not Me.cmbSearchCostCenter.SelectedIndex = -1 Then Me.cmbSearchLocation.SelectedIndex = 0
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
    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                GetAllRecords()
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                Me.BtnSave.Visible = False
                Me.btnEdit.Visible = False
                ''TFS1823
                Me.CtrlGrdBar2.Visible = True
                Me.CtrlGrdBar1.Visible = False
            Else
                Me.btnPrint.Visible = False
                Me.btnDelete.Visible = False
                Me.btnEdit.Visible = False
                Me.BtnSave.Visible = True
                ''TFS1823
                Me.CtrlGrdBar2.Visible = False
                Me.CtrlGrdBar1.Visible = True

                ''''''''''''''''''''''''''
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ExportFile(ByVal VoucherId As Integer)
        Try
            'If IsEmailAlert = True Then
            '    If IsAttachmentFile = True Then
            '        crpt.Load(str_ApplicationStartUpPath & "\Reports\rptProductionStore.rpt", DBServerName)
            '        If DBUserName <> "" Then
            '            crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, DBUserName, DBPassword)
            '            crpt.DataSourceConnections.Item(0).SetLogon(DBName, DBPassword)
            '        Else
            '            crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, True)
            '        End If

            '        Dim ConnectionInfo As New ConnectionInfo
            '        With ConnectionInfo
            '            .ServerName = DBServerName
            '            .DatabaseName = DBName
            '            If DBUserName <> "" Then
            '                .UserID = DBUserName
            '                .Password = DBPassword
            '                .IntegratedSecurity = False
            '            Else
            '                .IntegratedSecurity = True
            '            End If
            '        End With
            '        Dim tbLogOnInfo As New TableLogOnInfo
            '        For Each dt As Table In crpt.Database.Tables
            '            tbLogOnInfo = dt.LogOnInfo
            '            tbLogOnInfo.ConnectionInfo = ConnectionInfo
            '            dt.ApplyLogOnInfo(tbLogOnInfo)
            '        Next
            '        'crpt.RecordSelectionFormula = "{DispatchMasterTable.DispatchId}=" & VoucherId



            '        Dim crExportOps As New ExportOptions
            '        Dim crDiskOps As New DiskFileDestinationOptions
            '        Dim crExportType As New PdfRtfWordFormatOptions


            '        If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments\") Then
            '            IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments\")
            '        Else
            '        End If
            '        FileName = String.Empty
            '        FileName = "Production" & "-" & setVoucherNo & ""
            '        SourceFile = String.Empty
            '        SourceFile = _FileExportPath & "\" & FileName & ".pdf"
            '        crDiskOps.DiskFileName = SourceFile
            '        crExportOps = crpt.ExportOptions
            '        With crExportOps
            '            .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            '            .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            '            .ExportDestinationOptions = crDiskOps
            '            .ExportFormatOptions = crExportType
            '        End With
            '        crpt.Refresh()
            '        Try
            '            crpt.SetParameterValue("@CompanyName", CompanyTitle)
            '            crpt.SetParameterValue("@CompanyAddress", CompanyAddHeader)
            '            crpt.SetParameterValue("@ShowHeader", IsCompanyInfo)
            '        Catch ex As Exception
            '            'IsCompanyInfo = False
            '            'CompanyTitle = String.Empty
            '            'CompanyAddHeader = String.Empty
            '        End Try
            '        crpt.SetParameterValue("@ProductionId", VoucherId)
            '        crpt.Export(crExportOps)

            '    End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            ExportFile(getVoucher_Id)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            EmailSave()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Try
            Try
                If Val(Me.txtPackQty.Text) > 0 Then
                    Me.txtTotalQty.Text = (Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text))
                    Me.txtAmount.Text = Math.Round((Val(Me.txtTotalQty.Text) * Val(txtRate.Text)), DecimalPointInValue)
                    'Me.txtAmount.Text = Math.Round(((Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
                End If
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            'Me.txtAmount.Text = Val(Me.txtQty.Text) * Val(Me.txtRate.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtpProductionDate.Value.ToString("yyyy-M-d 00:00:00") Then
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
    Private Sub grdDetail_Invalidated(ByVal sender As Object, ByVal e As System.Windows.Forms.InvalidateEventArgs) Handles grdDetail.Invalidated
    End Sub
    Private Sub grdDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdDetail.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If
        ''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
        'If e.KeyCode = Keys.Delete Then
        '    btnDelete_Click(Nothing, Nothing)
        '    Exit Sub
        'End If

    End Sub
    Private Sub grdSave_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSave.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdSave.RowCount <= 0 Then Exit Sub
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
    Private Sub btnUpdateAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateAll.Click
        Try
            blnUpdateAll = True
            Me.btnUpdateAll.Enabled = False
            Dim blnStatus As Boolean = False
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSave.GetCheckedRows
                Me.grdSave.Row = r.Position
                EditRecord()
                FillModel()
                If Update1() = True Then
                    blnStatus = True
                Else
                    blnStatus = False
                End If
            Next
            If blnStatus = True Then msg_Information("Records update successful.") : Me.btnUpdateAll.Enabled = True : ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''Task# A1-10-06-2015 Added Key Pres event for some textboxes to take just numeric and dot value


    Private Sub txtNUM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStock.KeyPress, txtPackQty.KeyPress, txtQty.KeyPress, txtRate.KeyPress, txtAmount.KeyPress, txtDim2.KeyPress, txtDim1.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''Task-1616 Added Key Pres event for combobox RetailPrice to take just numeric and dot value

    Private Sub cmbNUM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbRetailPrice.KeyPress
        Try
            cmbNumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End Task-1616
    ''End Task# A1-10-06-2015
    Public Function Get_All(ByVal ProductionNo As String)
        Try
            Get_All = Nothing
            If Not ProductionNo.Length > 0 Then Exit Try
            If IsFormLoaded = True Then
                ''Task# H08062015  Ahmad Sharif:
                IsDrillDown = True
                If Me.grdSave.RowCount <= 50 Then
                    blnDisplayAll = True
                    Me.ToolStripButton3_Click(Nothing, Nothing)
                    blnDisplayAll = False
                End If
                Dim flag As Boolean = False
                flag = Me.grdSave.Find(Me.grdSave.RootTable.Columns("Production_No"), Janus.Windows.GridEX.ConditionOperator.Equal, (ProductionNo).Trim, 0, 1)
                'If flag = True Then
                Me.grdSave_DoubleClick(Nothing, Nothing)
                'Else
                'Exit Function
                'End If
                '' End Task# H08062015
                'End If
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'TASK-TFS-46 Added function Production Price 
    Public Sub GetProductionPrice(ArticleId As Integer)
        Try
            Dim dtprice As New DataTable
            dtprice = GetCostPriceForProduction(Val(ArticleId))
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
                dblPrice = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            End If
            Me.txtRate.Text = dblPrice
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'END TASK-TFS-46
    Private Sub cmbPlan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlan.SelectedIndexChanged
        Try
            If IsFormLoaded = False Then Exit Sub
            If Not Me.cmbPlan.SelectedIndex = -1 Then
                DisplayRecordByPlan(Me.cmbPlan.SelectedValue)
                FillCombos("Ticket")
                FillCombos("PlanItems")

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub dtpProductionDate_Leave(sender As Object, e As EventArgs) Handles dtpProductionDate.Leave
        'Try
        '    If Me.BtnSave.Text = "&Save" Then
        '        Me.txtProductionNo.Text = GetDocumentNo()
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtPackQty_TextChanged(sender As Object, e As EventArgs) Handles txtPackQty.TextChanged
        Try
            If Val(Me.txtQty.Text) > 0 Then
                Me.txtTotalQty.Text = (Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text))
                Me.txtAmount.Text = Math.Round((Val(Me.txtTotalQty.Text) * Val(txtRate.Text)), DecimalPointInValue)
                'Me.txtAmount.Text = Math.Round(((Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalQty_TextChanged(sender As Object, e As EventArgs) Handles txtTotalQty.TextChanged
        Try
            Me.txtAmount.Text = Math.Round((Val(Me.txtTotalQty.Text) * Val(txtRate.Text)), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetGridDetailQtyCalculate(ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try

            Me.grdDetail.UpdateData()
            If e.Column.Index = EnumGrd.Qty Or e.Column.Index = EnumGrd.PackQty Then
                If Val(Me.grdDetail.GetRow.Cells(EnumGrd.PackQty).Value.ToString) > 1 Then
                    Me.grdDetail.GetRow.Cells(EnumGrd.TotalQty).Value = (Val(Me.grdDetail.GetRow.Cells(EnumGrd.PackQty).Value.ToString) * Val(Me.grdDetail.GetRow.Cells(EnumGrd.Qty).Value.ToString))
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                Else
                    Me.grdDetail.GetRow.Cells(EnumGrd.TotalQty).Value = Val(Me.grdDetail.GetRow.Cells(EnumGrd.Qty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                End If
            ElseIf e.Column.Index = EnumGrd.TotalQty Then
                If Not Val(Me.grdDetail.GetRow.Cells(EnumGrd.PackQty).Value.ToString) > 1 Then
                    Me.grdDetail.GetRow.Cells(EnumGrd.Qty).Value = Val(Me.grdDetail.GetRow.Cells(EnumGrd.TotalQty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.Qty).Value
                End If
            End If
            'Me.grd.Refetch()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdDetail_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.CellUpdated
        Try
            'If setEditMode = True Then
            'ValidateEngineAndChasisNo(e)
            'End If
            GetGridDetailQtyCalculate(e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    'TODO: Review with Ali Faisal commented due to tsbTask and other controls were missing

    'Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click, tsbTask1.Click
    '    Try
    '        If Not grdSave.GetRow Is Nothing AndAlso grdSave.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
    '            Dim Lcontrol As String = String.Empty
    '            Dim control As String = String.Empty
    '            'Dim VNo = v
    '            Lcontrol = frmModProperty.fname.Name
    '            control = "frmProductionStore"
    '            'frmMain.LoadControl("Tasks")
    '            Dim frmtask As New frmTasks
    '            frmtask.Ref_No = grdSave.CurrentRow.Cells(4).Value.ToString
    '            frmtask.ReferenceForm = control
    '            'frmtask.GetReferenceTasks(frmtask.Ref_No)
    '            'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
    '            frmtask.StartPosition = FormStartPosition.CenterScreen
    '            frmtask.Text = "Production Entry (" & frmtask.Ref_No & ") "
    '            frmtask.Width = 950
    '            frmtask.ShowDialog()
    '            frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
    '            'frmtask.ReSetControls()
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click, tsbConfig1.Click
    '    Try
    '        If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
    '            frmMain.LoadControl("frmSystemConfiguration")
    '        End If
    '        frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Inventory
    '        frmMain.LoadControl("frmSystemConfiguration")
    '        frmSystemConfigurationNew.SelectTab()

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub cmbTicket_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTicket.SelectedIndexChanged
        Try
            If Not Me.cmbTicket.SelectedIndex = -1 Then
                DisplayTicket(Me.cmbTicket.SelectedValue)
                'Me.grdDetail.DataSource = New ProductionDAL().GetRecordById(Val(Me.cmbTicket.SelectedValue), , "Ticket")
                'If Me.cmbTicket.SelectedValue > 0 Then
                '    FillCombos("TicketWiseItem")
                'Else
                FillCombos("Item")
            End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ValidateEngineAndChasisNo(ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim dtVehicleIdentificationInfo As New DataTable
        Dim daVehicleIdentificationInfo As New OleDbDataAdapter(objCommand)
        objCon = Con
        'FillModel()
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        objCommand.Connection = objCon
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        objCommand.CommandType = CommandType.Text
        objCommand.Transaction = trans
        objCommand.CommandText = ""
        Dim Engine_No As String = ""
        Dim Chassis_No As String = ""
        Try

            If e.Column.Index = EnumGrd.EngineNo Then

                Me.grdDetail.UpdateData()
                If Me.grdDetail.GetRow.Cells(EnumGrd.EngineNo).Value.ToString.Length > 0 Then
                    Engine_No = Me.grdDetail.GetRow.Cells(EnumGrd.EngineNo).Value.ToString.Substring(4)
                End If

                objCommand.CommandText = ""
                objCommand.CommandText = "SELECT dbo.ProductionDetailTable.ArticleDefId, Stuff(ProductionDetailTable.EngineNo, 1, 4, '') As EngineNo " _
                                                & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                & " dbo.ProductionDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.ProductionDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId = " & Me.grdDetail.GetRow.Cells(EnumGrd.ArticleDefId).Value & ""
                If Engine_No.Length > 0 Then
                    objCommand.CommandText += " AND Stuff(ProductionDetailTable.EngineNo, 1, 4, '') =N'" & Engine_No & "'"
                End If

                Dim dtVehicleEngineNoIdentificationInfo As New DataTable
                Dim daVehicleEngineNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                daVehicleEngineNoIdentificationInfo.Fill(dtVehicleEngineNoIdentificationInfo)

                If dtVehicleEngineNoIdentificationInfo IsNot Nothing Then
                    If dtVehicleEngineNoIdentificationInfo.Rows.Count > 0 Then

                        If dtVehicleEngineNoIdentificationInfo.Rows(0).Item("EngineNo").ToString.Length > 0 Or Engine_No.Length > 0 Then
                            If Engine_No = dtVehicleEngineNoIdentificationInfo.Rows(0).Item("EngineNo").ToString Then
                                'Throw New Exception("Engine no [" & Me.grdDetail.GetRow.Cells(EnumGrd.EngineNo).Value.ToString & "] already exists")
                                ShowErrorMessage("Engine no [" & Me.grdDetail.GetRow.Cells(EnumGrd.EngineNo).Value.ToString & "] already exists against item [" & Me.grdDetail.GetRow.Cells(EnumGrd.ArticleDescription).Value.ToString & "]")
                                Me.grdDetail.GetRow.Cells(EnumGrd.EngineNo).Value = Engine_NoBefore
                                'Engine_NoBefore = String.Empty
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            ElseIf e.Column.Index = EnumGrd.ChasisNo Then

                Me.grdDetail.UpdateData()
                If Me.grdDetail.GetRow.Cells(EnumGrd.ChasisNo).Value.ToString.Length > 0 Then
                    Chassis_No = Me.grdDetail.GetRow.Cells(EnumGrd.ChasisNo).Value.ToString.Substring(4)
                End If
                ''Retrieving Chasis No
                objCommand.CommandText = ""
                objCommand.CommandText = "SELECT dbo.ProductionDetailTable.ArticleDefId, Stuff(ProductionDetailTable.ChasisNo, 1, 4, '') As ChasisNo " _
                                                & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                & " dbo.ProductionDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.ProductionDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId = " & Me.grdDetail.GetRow.Cells(EnumGrd.ArticleDefId).Value & ""

                If Chassis_No.Length > 0 Then
                    objCommand.CommandText += " AND Stuff(ProductionDetailTable.ChasisNo, 1, 4, '') =N'" & Chassis_No & "'"
                End If
                Dim dtVehicleChasisNoIdentificationInfo As New DataTable
                Dim daVehicleChasisNoIdentificationInfo As New OleDbDataAdapter(objCommand)
                daVehicleChasisNoIdentificationInfo.Fill(dtVehicleChasisNoIdentificationInfo)
                If dtVehicleChasisNoIdentificationInfo IsNot Nothing Then
                    If dtVehicleChasisNoIdentificationInfo.Rows.Count > 0 Then
                        If dtVehicleChasisNoIdentificationInfo.Rows(0).Item("ChasisNo").ToString.Length > 0 Or Chassis_No.Length > 0 Then
                            If Chassis_No = dtVehicleChasisNoIdentificationInfo.Rows(0).Item("ChasisNo").ToString Then
                                'Throw New Exception("Chassis no [" & Me.grdDetail.GetRow.Cells(EnumGrd.ChasisNo).Value.ToString & "] already exists")
                                ShowErrorMessage("Chassis no [" & Me.grdDetail.GetRow.Cells(EnumGrd.ChasisNo).Value.ToString & "] already exists against item [" & Me.grdDetail.GetRow.Cells(EnumGrd.ArticleDescription).Value.ToString & "]")
                                Me.grdDetail.GetRow.Cells(EnumGrd.ChasisNo).Value = Chassis_NoBefore
                                'Chassis_NoBefore = String.Empty
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If
            'trans.Commit()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub


    'Private Sub grdDetail_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.CellValueChanged
    '    Try
    '        If Me.grdDetail.GetRow.Cells(EnumGrd.EngineNo).Value.ToString.Length > 0 Then
    '            If Engine_NoBefore = String.Empty Then
    '                Engine_NoBefore = Me.grdDetail.GetRow.Cells(EnumGrd.EngineNo).Value.ToString
    '            End If
    '        End If
    '        If Me.grdDetail.GetRow.Cells(EnumGrd.ChasisNo).Value.ToString.Length > 0 Then
    '            If Chassis_NoBefore = String.Empty Then
    '                Chassis_NoBefore = Me.grdDetail.GetRow.Cells(EnumGrd.ChasisNo).Value.ToString
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub DisplayTicket(ByVal ReceivingID As Integer)
        Dim str As String = String.Empty
        Try
            ''Below lines are commented on 24-10-2017 against TASK TFS1626
            'Dim flgCostSheetRateonProduction As Boolean = getConfigValueByType("ApplyCostSheetRateOnProduction")
            'If flgCostSheetRateonProduction = False Then
            '    str = "   SELECT Location_Id = " & Me.cmbLocation.SelectedValue & ", ArticleDefView.ArticleId AS ArticleDefID, '' AS Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, " _
            '          & " ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName,'' As ArticleSize, ArticleDefView.PackQty, PlanTicketsDetail.Quantity AS Qty, ArticleDefView.PurchasePrice " _
            '          & " AS CurrentRate, 0 AS PackPrice, PlanTicketsDetail.Quantity * ArticleDefView.PurchasePrice AS Total, '' AS Comments, '' AS BatchNo, '' AS UOM, '' AS Pack_Desc, 0 AS PurchaseAccountId, " _
            '          & " ArticleDefView.CGSAccountId, 0 AS EmployeeId, CONVERT(DateTime, NULL) AS ExpiryDate, '' AS EngineNo, '' AS ChasisNo, PlanTicketsDetail.PlanTicketsDetailID AS PlanDetailID, " _
            '          & " ArticleDefView.MasterID, PlanTicketsDetail.Quantity AS TotalQty " _
            '          & " FROM         PlanTicketsDetail INNER JOIN " _
            '          & " PlanTicketsMaster ON PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID INNER JOIN " _
            '          & " ArticleDefView ON PlanTicketsDetail.ArticleId = ArticleDefView.MasterID INNER JOIN " _
            '          & " tblCostSheet ON ArticleDefView.MasterID = tblCostSheet.MasterArticleID INNER JOIN " _
            '          & " ArticleDefView AS ArticleDefView_1 ON tblCostSheet.ArticleID = ArticleDefView_1.ArticleId " _
            '          & " WHERE PlanTicketsMaster.PlanTicketsMasterID = " & ReceivingID & " " _
            '          & " GROUP BY ArticleDefView.ArticleId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, " _
            '          & " ArticleDefView.PackQty, PlanTicketsDetail.Quantity, ArticleDefView.CGSAccountId, PlanTicketsDetail.PlanTicketsDetailID, ArticleDefView.MasterID ,ArticleDefView.PurchasePrice"
            'ElseIf flgCostSheetRateonProduction = True And getConfigValueByType("AvgRate").ToString = "True" Then
            '    str = "   SELECT     Location_Id = " & Me.cmbLocation.SelectedValue & ", ArticleDefView.ArticleId AS ArticleDefID, '' AS Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, " _
            '          & " ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, '' AS ArticleSize, ArticleDefView.PackQty, PlanTicketsDetail.Quantity AS Qty, SUM(ArticleDefView_1.Cost_Price * tblCostSheet.Qty) " _
            '          & " AS CurrentRate, 0 AS PackPrice, PlanTicketsDetail.Quantity * SUM(ArticleDefView_1.Cost_Price * tblCostSheet.Qty) AS Total, '' AS Comments, '' AS BatchNo, '' AS UOM, '' AS Pack_Desc, 0 AS PurchaseAccountId, " _
            '          & " ArticleDefView.CGSAccountId, 0 AS EmployeeId, CONVERT(DateTime, NULL) AS ExpiryDate, '' AS EngineNo, '' AS ChasisNo, PlanTicketsDetail.PlanTicketsDetailID AS PlanDetailID, " _
            '          & " ArticleDefView.MasterID, PlanTicketsDetail.Quantity AS TotalQty " _
            '          & " FROM         PlanTicketsDetail INNER JOIN " _
            '          & " PlanTicketsMaster ON PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID INNER JOIN " _
            '          & " ArticleDefView ON PlanTicketsDetail.ArticleId = ArticleDefView.MasterID INNER JOIN " _
            '          & " tblCostSheet ON ArticleDefView.MasterID = tblCostSheet.MasterArticleID INNER JOIN " _
            '          & " ArticleDefView AS ArticleDefView_1 ON tblCostSheet.ArticleID = ArticleDefView_1.ArticleId " _
            '          & " WHERE PlanTicketsMaster.PlanTicketsMasterID = " & ReceivingID & " " _
            '          & " GROUP BY ArticleDefView.ArticleId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, " _
            '          & " ArticleDefView.PackQty, PlanTicketsDetail.Quantity, ArticleDefView.CGSAccountId, PlanTicketsDetail.PlanTicketsDetailID, ArticleDefView.MasterID "

            'ElseIf flgCostSheetRateonProduction = True Then
            '    str = "   SELECT     Location_Id = " & Me.cmbLocation.SelectedValue & ", ArticleDefView.ArticleId AS ArticleDefID, '' AS Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, " _
            '          & " ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, '' AS ArticleSize, ArticleDefView.PackQty, PlanTicketsDetail.Quantity AS Qty, SUM(ArticleDefView_1.PurchasePrice * tblCostSheet.Qty) " _
            '          & " AS CurrentRate, 0 AS PackPrice, PlanTicketsDetail.Quantity * SUM(ArticleDefView_1.PurchasePrice * tblCostSheet.Qty) AS Total, '' AS Comments, '' AS BatchNo, '' AS UOM, '' AS Pack_Desc, 0 AS PurchaseAccountId, " _
            '          & " ArticleDefView.CGSAccountId, 0 AS EmployeeId, CONVERT(DateTime, NULL) AS ExpiryDate, '' AS EngineNo, '' AS ChasisNo, PlanTicketsDetail.PlanTicketsDetailID AS PlanDetailID, " _
            '          & " ArticleDefView.MasterID, PlanTicketsDetail.Quantity AS TotalQty " _
            '          & " FROM         PlanTicketsDetail INNER JOIN " _
            '          & " PlanTicketsMaster ON PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID INNER JOIN " _
            '          & " ArticleDefView ON PlanTicketsDetail.ArticleId = ArticleDefView.MasterID INNER JOIN " _
            '          & " tblCostSheet ON ArticleDefView.MasterID = tblCostSheet.MasterArticleID INNER JOIN " _
            '          & " ArticleDefView AS ArticleDefView_1 ON tblCostSheet.ArticleID = ArticleDefView_1.ArticleId " _
            '          & " WHERE PlanTicketsMaster.PlanTicketsMasterID = " & ReceivingID & " " _
            '          & " GROUP BY ArticleDefView.ArticleId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, " _
            '          & " ArticleDefView.PackQty, PlanTicketsDetail.Quantity, ArticleDefView.CGSAccountId, PlanTicketsDetail.PlanTicketsDetailID, ArticleDefView.MasterID "

            'End If

            '' TASK TFS1626 Configured cost sheet with parent id insted master id to display record against selelected Ticket ant Plan.
            Dim flgCostSheetRateonProduction As Boolean = getConfigValueByType("ApplyCostSheetRateOnProduction")
            ''TASK TFS1616 addition column of BatchNo,RetailPrice,ExpiryDate Ayesha Rehman
            ''TASK TFS1772 addition column of Dim1,Dim2 Ayesha Rehman
            If flgCostSheetRateonProduction = False Then
                str = "   SELECT Location_Id = " & Me.cmbLocation.SelectedValue & ", ArticleDefView.ArticleId AS ArticleDefID, '' AS Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, " _
                      & " ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName,'' As ArticleSize, ArticleDefView.PackQty, PlanTicketsDetail.Quantity AS Qty, 0 AS Dim1, 0 AS Dim2 , ArticleDefView.PurchasePrice " _
                      & " AS CurrentRate, 0 AS PackPrice, PlanTicketsDetail.Quantity * ArticleDefView.PurchasePrice AS Total, '' AS Comments, '' AS BatchNo, '' AS UOM, '' AS Pack_Desc, 0 AS RetailPrice, 0 AS PurchaseAccountId, " _
                      & " ArticleDefView.CGSAccountId, 0 AS EmployeeId, '' AS ExpiryDate, '' AS EngineNo, '' AS ChasisNo, PlanTicketsDetail.PlanTicketsDetailID AS PlanDetailID, " _
                      & " ArticleDefView.MasterID, PlanTicketsDetail.Quantity AS TotalQty " _
                      & " FROM PlanTicketsDetail INNER JOIN " _
                      & " PlanTicketsMaster ON PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID INNER JOIN " _
                      & " ArticleDefView ON PlanTicketsDetail.ArticleId = ArticleDefView.ArticleId INNER JOIN " _
                      & " tblCostSheet ON ArticleDefView.ArticleId = tblCostSheet.ParentId " _
                      & " WHERE PlanTicketsMaster.PlanTicketsMasterID = " & ReceivingID & " " _
                      & " GROUP BY ArticleDefView.ArticleId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, " _
                      & " ArticleDefView.PackQty, PlanTicketsDetail.Quantity, ArticleDefView.CGSAccountId, PlanTicketsDetail.PlanTicketsDetailID, ArticleDefView.MasterID ,ArticleDefView.PurchasePrice"
                ''TASK TFS1616 addition column of BatchNo,RetailPrice,ExpiryDate Ayesha Rehman
                ''TASK TFS1772 addition column of Dim1,Dim2 Ayesha Rehman
            ElseIf flgCostSheetRateonProduction = True And getConfigValueByType("AvgRate").ToString = "True" Then
                str = "   SELECT Location_Id = " & Me.cmbLocation.SelectedValue & ", ArticleDefView.ArticleId AS ArticleDefID, '' AS Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, " _
                      & " ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, '' AS ArticleSize, ArticleDefView.PackQty, PlanTicketsDetail.Quantity AS Qty,  0 AS Dim1 , 0 AS Dim2 , SUM(ArticleDefView_1.Cost_Price * tblCostSheet.Qty) " _
                      & " AS CurrentRate, 0 AS PackPrice, PlanTicketsDetail.Quantity * SUM(ArticleDefView_1.Cost_Price * tblCostSheet.Qty) AS Total, '' AS Comments, '' AS BatchNo, '' AS UOM, '' AS Pack_Desc,0 AS RetailPrice, 0 AS PurchaseAccountId, " _
                      & " ArticleDefView.CGSAccountId, 0 AS EmployeeId, '' AS ExpiryDate, '' AS EngineNo, '' AS ChasisNo, PlanTicketsDetail.PlanTicketsDetailID AS PlanDetailID, " _
                      & " ArticleDefView.MasterID, PlanTicketsDetail.Quantity AS TotalQty " _
                      & " FROM PlanTicketsDetail INNER JOIN " _
                      & " PlanTicketsMaster ON PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID INNER JOIN " _
                      & " ArticleDefView ON PlanTicketsDetail.ArticleId = ArticleDefView.ArticleId INNER JOIN " _
                      & " tblCostSheet ON ArticleDefView.ArticleId = tblCostSheet.ParentId INNER JOIN " _
                      & " ArticleDefView AS ArticleDefView_1 ON tblCostSheet.ArticleID = ArticleDefView_1.ArticleId " _
                      & " WHERE PlanTicketsMaster.PlanTicketsMasterID = " & ReceivingID & " " _
                      & " GROUP BY ArticleDefView.ArticleId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, " _
                      & " ArticleDefView.PackQty, PlanTicketsDetail.Quantity, ArticleDefView.CGSAccountId, PlanTicketsDetail.PlanTicketsDetailID, ArticleDefView.MasterID "
                ''TASK TFS1616 addition column of BatchNo,RetailPrice,ExpiryDate Ayesha Rehman
                ''TASK TFS1772 addition column of Dim1,Dim2 Ayesha Rehman
            ElseIf flgCostSheetRateonProduction = True Then
                str = "   SELECT     Location_Id = " & Me.cmbLocation.SelectedValue & ", ArticleDefView.ArticleId AS ArticleDefID, '' AS Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, " _
                      & " ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, '' AS ArticleSize, ArticleDefView.PackQty, PlanTicketsDetail.Quantity AS Qty, 0 AS Dim1, 0 AS Dim2 , SUM(ArticleDefView_1.PurchasePrice * tblCostSheet.Qty) " _
                      & " AS CurrentRate, 0 AS PackPrice, PlanTicketsDetail.Quantity * SUM(ArticleDefView_1.PurchasePrice * tblCostSheet.Qty) AS Total, '' AS Comments, '' AS BatchNo, '' AS UOM, '' AS Pack_Desc,0 AS RetailPrice, 0 AS PurchaseAccountId, " _
                      & " ArticleDefView.CGSAccountId, 0 AS EmployeeId, '' AS ExpiryDate, '' AS EngineNo, '' AS ChasisNo, PlanTicketsDetail.PlanTicketsDetailID AS PlanDetailID, " _
                      & " ArticleDefView.MasterID, PlanTicketsDetail.Quantity AS TotalQty " _
                      & " FROM PlanTicketsDetail INNER JOIN " _
                      & " PlanTicketsMaster ON PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID INNER JOIN " _
                      & " ArticleDefView ON PlanTicketsDetail.ArticleId = ArticleDefView.ArticleId INNER JOIN " _
                      & " tblCostSheet ON ArticleDefView.ArticleId = tblCostSheet.ParentId INNER JOIN " _
                      & " ArticleDefView AS ArticleDefView_1 ON tblCostSheet.ArticleID = ArticleDefView_1.ArticleId " _
                      & " WHERE PlanTicketsMaster.PlanTicketsMasterID = " & ReceivingID & " " _
                      & " GROUP BY ArticleDefView.ArticleId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, " _
                      & " ArticleDefView.PackQty, PlanTicketsDetail.Quantity, ArticleDefView.CGSAccountId, PlanTicketsDetail.PlanTicketsDetailID, ArticleDefView.MasterID "

            End If
            Dim DtDisplayDetail As DataTable = GetDataTable(str)
            DtDisplayDetail.AcceptChanges()
            Me.grdDetail.DataSource = Nothing
            grdDetail.DataSource = DtDisplayDetail
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''TASK : TFS738 
    Private Sub dtpProductionDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpProductionDate.ValueChanged
        Try
            If Me.BtnSave.Text = "&Save" Then
                Me.txtProductionNo.Text = GetDocumentNo()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''END TASK: TFS738
    Private Sub btnPrintVoucher_Click(sender As Object, e As EventArgs) Handles btnPrintVoucher.Click
        Try
            GetVoucherPrint(Me.grdSave.CurrentRow.Cells("Production_no").Value.ToString, Me.Name)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrintVoucher1_Click(sender As Object, e As EventArgs) Handles btnPrintVoucher1.Click
        Try
            GetVoucherPrint(Me.grdSave.CurrentRow.Cells("Production_no").Value.ToString, Me.Name)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS1495
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPackRate_TextChanged(sender As Object, e As EventArgs) Handles txtPackRate.TextChanged
        Try
            If Val(Me.txtPackRate.Text) > 0 Then
                If getConfigValueByType("Apply40KgRate").ToString = "True" AndAlso Me.cmbUnit.Text <> "Loose" Then
                    Me.txtRate.Text = (Val(Me.txtPackRate.Text) / 40)
                ElseIf Me.cmbUnit.Text <> "Loose" Then
                    Me.txtRate.Text = (Val(Me.txtPackRate.Text) / Val(Me.txtPackQty.Text))
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Start TFS1772 :Ayesha Rehman
    Private Sub txtDim1_Leave(sender As Object, e As EventArgs) Handles txtDim1.Leave
        Try
            'Dim value As Double
            'Double.TryParse(txtDim1.Text, value)
            'If value < 0 Then
            '    txtDim1.Text = Math.Abs(value)
            'End If
            If Me.txtDim1.Text = String.Empty Then

                ShowErrorMessage("Enter a value in Dimention 1")
                Me.txtDim1.Focus()
            ElseIf Val(Me.txtDim1.Text) <= 0 Then
                ShowErrorMessage("Dimention 1 must be greater than 0")
                Me.txtDim1.Focus()
                Me.txtDim1.SelectAll()
                'ElseIf Val(Me.txtDim1.Text) <= 0 Then
                '    ShowErrorMessage("Dimention 2 must be greater than 0")
                '    'Me.txtDim2.Focus()
                '    Me.txtDim2.SelectAll()
            End If
            Me.txtPackQty.Text = Math.Round((Val(Me.txtDim1.Text) * Val(txtDim2.Text)), DecimalPointInValue)

            'Me.txtAmount.Text = Math.Round(((Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub txtDim2_Leave(sender As Object, e As EventArgs) Handles txtDim2.Leave
        Try
            'Dim value As Double
            'Double.TryParse(txtDim2.Text, value)
            'If value < 0 Then
            '    txtDim2.Text = Math.Abs(value)
            'End If
            If Me.txtDim2.Text = String.Empty Then

                ShowErrorMessage("Enter a value in Dimention 2")
                Me.txtDim2.Focus()
            ElseIf Val(Me.txtDim2.Text) <= 0 Then
                ShowErrorMessage("Dimention 2 must be greater than 0")
                Me.txtDim2.Focus()
                Me.txtDim2.SelectAll()
                'ElseIf Val(Me.txtDim1.Text) <= 0 Then
                '    ShowErrorMessage("Dimention 2 must be greater than 0")
                '    Me.txtDim2.Focus()
                '    Me.txtDim2.SelectAll()
            End If
            Me.txtPackQty.Text = Math.Round((Val(Me.txtDim1.Text) * Val(txtDim2.Text)), DecimalPointInValue)

            'Me.txtAmount.Text = Math.Round(((Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)) * Val(txtRate.Text)), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End TFS1772 




    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSave.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSave.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSave.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Production Store"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSave.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSave.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSave.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Production Store"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
