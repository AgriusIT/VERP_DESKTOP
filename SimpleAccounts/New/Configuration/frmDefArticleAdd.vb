''07-June-2015 Task# 7062015, Ahmad Sharif: set picture box SizeMode property to StretchImage
'' 13-Dec-2013 ReqId-925 Imran Ali Item code not update and increase length
'' 19-Dec-2013 ReqId-898 Imrna Ali       Unlimited Space Required in New Inventory Item Tab 
'' 21-Dec-2013 ReqId-962 Imran Ali       Cost Sheet is not working properly 
'' 21-Dec-2013 ReqId-958 M Ijaz Javed   Add security rights of price update against cost sheet
'' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
'' 03-Feb-2014 TSK:2411      Imran Ali     Cost Sheet Problem
'' 18-Feb-2014 TASK2431 Imran Ali  Add cost price update button 
'' 26-Feb-2014 Task:2440 Imran Ali      Cost Price Update Button On Inventory Item Define
'' 18-Mar-2014 TASK:2503 Imran Ali ResetControl After Update On Inventory Item 
'' 16-May-2014 TASK:2617 Imran Ali Price Update Problem New Inventory Item
'  15-Jun-2015 Task# 20150600011 Name Consistent in Entry and History Ali Ansari
'' 17-6-2015 TASKM176151 Imran Ali Add Field Of Remarks In Cost Sheet Define.
'05-Aug-2015 Task#05082015 Ahmad Sharif Resolve the Image Upload issue on Update
'28-10-2015 Task#102815 Muhammad Ameen: Added new DropDownList of Article Status.

Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Collections
Imports System.Data.OleDb
Imports System.IO
Imports System.Runtime.InteropServices

Public Class frmDefArticleAdd
    Implements IGeneral
    Private CurrentId As Integer = 0I
    Private mobjModel As Article
    Private COADetailID As Integer = 0I
    Private _str_Path As String = String.Empty
    Dim OpenFileDialog As New OpenFileDialog
    Dim flgCompanyRights As Boolean = False
    Dim PriceUpdateButton As ToolStripButton
    Dim blnAutoArticleCode As Boolean = False
    Dim IsOpenForm As Boolean = False

#Region "Enumrations"
    Enum EnumGrid
        'ArticleID
        'ArticleCode
        'Description
        'Group
        'GroupID
        'PurchasePrice
        'SalePrice
        'PackQty
        'StockLevel
        'StockOpt
        'StockMax
        'Active
        'StatusID
        'SortOrder
        'TypeID
        'Type
        'Brand
        'Origin
        'Unit
        'CategoryId
        'ArticleLPOId
        'LPO
        'AccountID
        'Remarks
        'ServiceItem
        'ArticlePicture
        'TradePrice
        'Freight
        'MarketReturns
        'GST_Applicable
        'FlatRate_Applicable
        'FlatRate
        'ItemWeight
        'HSCode
        'LargestPackQty
        'Cost_Price
        'ArticleBrandId
        ArticleId
        ArticleCode
        Description
        Group
        GroupId
        PurchasePrice
        SalePrice
        PackQty
        StockLevel
        StockOpt
        StockMax
        Active
        StatusID
        SortOrder
        TypeId
        Type
        Brand
        ArticleGenderId
        Origin
        Unit
        CategoryId
        ArticleLpoId
        LPO
        AccountID
        Remarks
        ServiceItem
        ArticlePicture
        TradePrice
        Freight
        MarketReturns
        GST_Applicable
        FlatRate_Applicable
        FlatRate
        ItemWeight
        HSCode
        LargestPackQty
        Cost_Price
        ArticleBrandId
        ApplyAdjustmentFuelExp
    End Enum

    Enum EnumArticalGrid
        ArticleID
        ArticleCode
        Size
        Color
        Cost
        Retail
        Active
        Delete
        Update
    End Enum

    Enum EnumArticalLocation
        LocationID
        Location
        Rank
        Delete
        ArticleId
    End Enum
    Enum EnumGridCostSheet
        AritcleID
        Code
        Description
        Color
        Size
        'Category    '|' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
        ArticleSize
        PurchasePrice
        SalePrice
        Tax
        Qty
        MasterArticleID
        TotalPurchaseValue
        TotalSaleValue
        TotalPurchaseTaxAmount
        TotalSaleTaxAmount
        NetPurchase
        NetSales
        Category    '|' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
        Remarks 'TaskM176151 Add new column Remarks
        Delete
    End Enum

#End Region

    'TODO : Y subsubid is null in category combo

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "Artical Grid" Then
                With Me.grdAriticals.RootTable
                    .Columns(EnumArticalGrid.ArticleID).Visible = False
                    .Columns(EnumArticalGrid.ArticleCode).Caption = "Artical Code"
                    .Columns(EnumArticalGrid.Cost).Caption = "Purchase Price"
                    .Columns(EnumArticalGrid.Retail).Caption = "Sale Price"

                    .Columns(EnumArticalGrid.Cost).FormatString = ""
                    .Columns(EnumArticalGrid.Retail).FormatString = ""

                    .Columns(EnumArticalGrid.Delete).ColumnType = Janus.Windows.GridEX.ColumnType.Link
                    .Columns(EnumArticalGrid.Update).ColumnType = Janus.Windows.GridEX.ColumnType.Link

                End With
            ElseIf Condition = "ArticalLocation" Then
                With Me.grdItemLocation.RootTable
                    .Columns(EnumArticalLocation.LocationID).Visible = False
                    .Columns(EnumArticalLocation.Location).Width = 200
                    .Columns(EnumArticalLocation.Rank).Width = 400
                    .Columns(EnumArticalLocation.Rank).MaxLength = 50
                    .Columns(EnumArticalLocation.Rank).Caption = "Rack"
                    .Columns(EnumArticalLocation.Rank).Selectable = True
                    '.Columns(EnumArticalLocation.Delete).ColumnType = Janus.Windows.GridEX.ColumnType.Link
                End With
                For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grdItemLocation.RootTable.Columns
                    If c.Index <> EnumArticalLocation.Rank Then
                        c.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
            Else
                Me.grdAllRecords.AutoSizeColumns()
                Me.grdAllRecords.RootTable.Columns(EnumGrid.AccountID).Visible = False
                Me.grdAllRecords.RootTable.Columns(EnumGrid.ArticleID).Visible = False
                Me.grdAllRecords.RootTable.Columns(EnumGrid.GroupID).Visible = False
                Me.grdAllRecords.RootTable.Columns(EnumGrid.TypeID).Visible = False
                Me.grdAllRecords.RootTable.Columns(EnumGrid.ServiceItem).Visible = False
                Me.grdAllRecords.RootTable.Columns(EnumGrid.ArticlePicture).Visible = False
                Me.grdAllRecords.RootTable.Columns(EnumGrid.CategoryId).Visible = False
                Me.grdAllRecords.RootTable.Columns(EnumGrid.ArticleLPOId).Visible = False
                Me.grdAllRecords.RootTable.Columns(EnumGrid.ArticleBrandId).Visible = False
                Me.grdAllRecords.RootTable.Columns(EnumGrid.ArticleCode).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try

            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.BtnCancel.Enabled = True
                Me.btnHistoryCancel.Enabled = True
                Me.btnPriceUpdate.Enabled = True
                Exit Sub
            End If


            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                'Me.BtnNew.Enabled = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnCancel.Enabled = False
                Me.btnHistoryCancel.Enabled = False
                Me.btnPriceUpdate.Enabled = False       '' 21-Dec-2013 ReqId-958 M Ijaz Javed   Add security rights of price update against cost sheet

                SetNavigationButtons(EnumDataMode.Edit)
                Me.grdAllRecords.Enabled = True

            ElseIf Mode.ToString = EnumDataMode.[New].ToString Then

                'BtnNew.Enabled = False

                'If mobjControlList.Item("btnSave") Is Nothing Then
                '    btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'Else
                '    btnSave.Enabled = True ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                'End If

                Me.BtnSave.Enabled = True

                BtnDelete.Enabled = False
                BtnCancel.Enabled = True
                Me.btnHistoryCancel.Enabled = True
                Me.btnPriceUpdate.Enabled = True    '' 21-Dec-2013 ReqId-958 M Ijaz Javed   Add security rights of price update against cost sheet
                SetNavigationButtons(Mode)

                Me.grdAllRecords.Enabled = False

            ElseIf Mode.ToString = EnumDataMode.Edit.ToString Then

                BtnNew.Enabled = True
                BtnSave.Enabled = True

                'If mobjControlList.Item("btnUpdate") Is Nothing Then
                '    btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'Else
                '    btnUpdate.Enabled = True ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                'End If

                'If mobjControlList.Item("btnDelete") Is Nothing Then
                '    btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                'Else
                '    btnDelete.Enabled = True ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                'End If

                BtnDelete.Enabled = True

                Me.btnPriceUpdate.Enabled = True    '' 21-Dec-2013 ReqId-958 M Ijaz Javed   Add security rights of price update against cost sheet

                BtnCancel.Enabled = False
                Me.btnHistoryCancel.Enabled = False

                SetNavigationButtons(Mode)

                Me.grdAllRecords.Enabled = True

                Me.grdAllRecords.Focus()

            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                BtnNew.Enabled = True
                BtnSave.Enabled = False


                BtnDelete.Enabled = False
                BtnCancel.Enabled = False
                Me.btnHistoryCancel.Enabled = False

                Me.btnPriceUpdate.Enabled = False    '' 21-Dec-2013 ReqId-958 M Ijaz Javed   Add security rights of price update against cost sheet
                SetNavigationButtons(Mode)

                Me.grdAllRecords.Enabled = True

                Me.grdAllRecords.Focus()

            End If


            ' '' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
            'If mobjControlList.Item("btnExport") Is Nothing Then
            '    Me.UiCtrlGridBar1.btnExport.Enabled = False
            'End If


            ' '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            'If mobjControlList.Item("btnPrint") Is Nothing Then
            '    Me.UiCtrlGridBar1.btnPrint.Enabled = False
            'End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If Me.grdAllRecords.RecordCount = 0 Then
                ShowErrorMessage(gstrMsgRecordNotFound)
                Exit Function
            End If
            If IsValidToDeleteItem("PurchaseOrderDetailTable", "MasterID", Me.CurrentId) = True And IsValidToDeleteItem("ReceivingDetailTable", "MasterID", Me.CurrentId) = True And IsValidToDeleteItem("SalesDetailTable", "MasterID", Me.CurrentId) = True And IsValidToDeleteItem("SalesOrderDetailTable", "MasterID", Me.CurrentId) = True And IsValidToDeleteItem("DispatchDetailTable", "MasterID", Me.CurrentId) = True And IsValidToDeleteItem("ReceivingNoteDetailTable", "MasterID", Me.CurrentId) = True And IsValidToDeleteItem("ProductionDetailTable", "MasterID", Me.CurrentId) = True And IsValidToDeleteItem("DeliveryChalanDetailTable", "MasterID", Me.CurrentId) = True And IsValidToDeleteItem("PurchaseReturnDetailTable", "MasterID", Me.CurrentId) = True And IsValidToDeleteItem("SalesReturnDetailTable", "MasterID", Me.CurrentId) = True Then
                'If IsValidToDelete("PurchaseOrderDetailTable", "ArticleDefId", Me.CurrentId) = True And IsValidToDelete("ReceivingDetailTable", "ArticleDefId", Me.CurrentId) = True And IsValidToDelete("SalesDetailTable", "ArticleDefId", Me.CurrentId) = True And IsValidToDelete("SalesOrderDetailTable", "ArticleDefId", Me.CurrentId) = True Then
                Me.FillModel(Me.BtnDelete.Text)
                If ShowConfirmationMessage(gstrMsgDelete, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then

                    'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If New ArticleDAL().Delete(Me.mobjModel) Then
                        'ShowInformationMessage(gstrMsgAfterDelete)

                        Dim intGridRowIndex As Integer
                        intGridRowIndex = Me.grdAllRecords.Row

                        Me.GetAllRecords()

                        Me.grdAllRecords_SelectionChanged(Nothing, Nothing)


                        ''Reset the row index to the grid
                        If intGridRowIndex > (Me.grdAllRecords.RowCount - 1) Then intGridRowIndex = (Me.grdAllRecords.RowCount - 1)
                        If Not intGridRowIndex < 0 Then Me.grdAllRecords.Row = intGridRowIndex
                    End If
                End If
            Else
                ShowErrorMessage(gstrMsgDependentRecordFound)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty

            If Condition = Me.cmbCategory.Name Then
                ''filling Category combo
                'strSQL = "SELECT  ArticleGroupDefTable.ArticleGroupId AS Id, ArticleGroupDefTable.ArticleGroupName AS Name, ArticleGroupDefTable.SubSubID" & _
                '        " FROM ArticleGroupDefTable LEFT OUTER JOIN " & _
                '          "    tblCOAMainSubSubDetail ON ArticleGroupDefTable.SubSubID = tblCOAMainSubSubDetail.coa_detail_id" & _
                '        " WHERE     (ArticleGroupDefTable.Active = 1) AND (tblCOAMainSubSubDetail.main_sub_sub_id In(Select main_sub_sub_id from tblCOAMainSubSub WHERE Account_Type='Inventory'))" & _
                '        " ORDER BY ArticleGroupDefTable.SortOrder"
                'Added Field Group Code 


                strSQL = "SELECT  ArticleGroupDefTable.ArticleGroupId AS Id, ArticleGroupDefTable.ArticleGroupName AS Name, ArticleGroupDefTable.SubSubID, ArticleGroupDefTable.GroupCode " & _
                       " FROM ArticleGroupDefTable LEFT OUTER JOIN " & _
                         "    tblCOAMainSubSubDetail ON ArticleGroupDefTable.SubSubID = tblCOAMainSubSubDetail.coa_detail_id" & _
                       " WHERE     (ArticleGroupDefTable.Active = 1) AND (tblCOAMainSubSubDetail.main_sub_sub_id In(Select main_sub_sub_id from tblCOAMainSubSub WHERE Account_Type='Inventory'))" & _
                       " ORDER BY ArticleGroupDefTable.SortOrder"

                Me.cmbCategory.DisplayMember = "Name"
                Me.cmbCategory.ValueMember = "Id"
                Me.cmbCategory.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))

            ElseIf Condition = Me.cmbType.Name Then
                ''filling type combo
                'strSQL = "select ArticleTypeId as Id, ArticleTypeName as Name from ArticleTypeDefTable where active=1 order by sortOrder"
                'Added Field TypeCode
                strSQL = "select ArticleTypeId as Id, ArticleTypeName as Name, TypeCode from ArticleTypeDefTable where active=1 order by sortOrder"
                Me.cmbType.DisplayMember = "Name"
                Me.cmbType.ValueMember = "ID"
                Me.cmbType.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
            ElseIf Condition = Me.cmbBrand.Name Then
                strSQL = String.Empty
                strSQL = "Select ArticleBrandId, ArticleBrandName, [Description], Active, SortOrder From ArticleBrandDefTable Order By ArticleBrandName ASC"
                FillDropDown(Me.cmbBrand, strSQL)
            ElseIf Condition = Me.cmbStatus.Name Then 'Task#102815
                strSQL = "select ArticleStatusID as ID, ArticleStatusName as Name from ArticleStatus where active=1"
                Me.cmbStatus.DisplayMember = "Name"
                Me.cmbStatus.ValueMember = "ID"
                Me.cmbStatus.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
            ElseIf Condition = Me.cmbGender.Name Then
                ''filling Gender combo
                strSQL = "select ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable where active=1 order by sortOrder"
                Me.cmbGender.DisplayMember = "Name"
                Me.cmbGender.ValueMember = "ID"
                Me.cmbGender.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
            ElseIf Condition = Me.cmbUnit.Name Then
                ''filling Unit combo
                strSQL = "select ArticleUnitId as Id, ArticleUnitName as Name from ArticleUnitDefTable where active=1 order by sortOrder"
                Me.cmbUnit.DisplayMember = "Name"
                Me.cmbUnit.ValueMember = "ID"
                Me.cmbUnit.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
            ElseIf Condition = Me.cmbLPO.Name Then
                ''filling Distributor/LPO combo
                'strSQL = "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID,dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId FROM         dbo.ArticleLpoDefTable INNER JOIN               dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId"
                'Added Field LPOCode
                strSQL = "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID,dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId , dbo.ArticleLpoDefTable.SubCategoryCode  FROM         dbo.ArticleLpoDefTable INNER JOIN               dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId"
                Me.cmbLPO.DisplayMember = "Name"
                Me.cmbLPO.ValueMember = "ID"
                Me.cmbLPO.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
                Me.cmbLPO.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                Me.cmbLPO.DisplayLayout.Bands(0).Columns("ArticleCompanyId").Hidden = True
                Me.cmbLPO.DisplayLayout.Bands(0).Columns("SubCategoryCode").Hidden = True
                Me.cmbLPO.DisplayLayout.Bands(0).Columns("Name").Width = 500

            ElseIf Condition = Me.lstCombinitions.Name Then
                ''filling combinitions list 
                strSQL = "select ArticleColorId as Id,ArticleColorName as Name from ArticleColorDefTable where active=1 order by sortOrder"
                Me.lstCombinitions.ListItem.DisplayMember = "Name"
                Me.lstCombinitions.ListItem.ValueMember = "ID"
                Me.lstCombinitions.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)
            ElseIf Condition = Me.lstSizes.Name Then
                ''filling sizes list
                strSQL = "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 order by ArticleSizeName, SortOrder "
                Me.lstSizes.ListItem.DisplayMember = "Name"
                Me.lstSizes.ListItem.ValueMember = "ID"
                Me.lstSizes.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)
            ElseIf Condition = Me.lstModelList.Name Then

                strSQL = "select ModelId as Id, Name from tblDefModelList where Active=1 order by Name"
                Me.lstModelList.ListItem.DisplayMember = "Name"
                Me.lstModelList.ListItem.ValueMember = "Id"
                Me.lstModelList.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)

            ElseIf Condition = Me.cmbCompany.Name Then

                ''filling company list
                'strSQL = "SELECT  ArticleCompanyId AS ID, ArticleCompanyName AS Name" & _
                '        " FROM ArticleCompanyDefTable" & _
                '        " WHERE Active = 1"
                'Added Field CategoryCode
                strSQL = "SELECT  ArticleCompanyId AS ID, ArticleCompanyName AS Name, CategoryCode" & _
                    " FROM ArticleCompanyDefTable" & _
                    " WHERE Active = 1"
                Me.cmbCompany.DisplayMember = "Name"
                Me.cmbCompany.ValueMember = "ID"
                Me.cmbCompany.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))

            ElseIf Condition = Me.cmbItem.Name Then

                FillUltraDropDown(Me.cmbItem, "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination,Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SalePrice,0) as Price, ArticleDefView.SizeRangeID as [Size ID], ArticleDefView.MasterId FROM ArticleDefView where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & "")
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Header.Caption = "Price"
                If rdoName.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                End If
                'ElseIf Condition = Me.cmbSupplier.Name Then
                '    FillDropDown(Me.cmbSupplier, "Select coa_detail_id, detail_title From vwCOADetail WHERE Account_Type='Vendor'")
            ElseIf Condition = Me.cmbCostSheetUnit.Name Then
                FillDropDown(Me.cmbCostSheetUnit, "Select PackName, PackName, PackQty From ArticleDefPackTable WHERE ArticleMasterId=" & IIf(Me.cmbItem.IsItemInList = False, 0, Val(Me.cmbItem.SelectedRow.Cells("MasterId").Value.ToString)) & "")
            ElseIf Condition = String.Empty Then
                ''Filling all combos
                'fill items combo for cost sheet
                FillUltraDropDown(Me.cmbItem, "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination,ArticleCompanyName as Category, ArticleLPOName as [Sub Category], Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SalePrice,0) as Price, ArticleDefView.SizeRangeID as [Size ID], ArticleDefView.MasterId FROM ArticleDefView where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & "")
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Header.Caption = "Price"
                If rdoName.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                End If



                ''filling Category combo
                'strSQL = "SELECT     ArticleGroupDefTable.ArticleGroupId AS Id, ArticleGroupDefTable.ArticleGroupName AS Name, ArticleGroupDefTable.SubSubID " & _
                '        " FROM         ArticleGroupDefTable INNER JOIN " & _
                '          "                    tblCOAMainSubSub ON ArticleGroupDefTable.SubSubID = tblCOAMainSubSub.main_sub_sub_id" & _
                '        " WHERE     (ArticleGroupDefTable.Active = 1) AND (tblCOAMainSubSub.account_type = 'Inventory')" & _
                '        " ORDER BY ArticleGroupDefTable.SortOrder"
                strSQL = "SELECT  ArticleGroupDefTable.ArticleGroupId AS Id, ArticleGroupDefTable.ArticleGroupName AS Name, ArticleGroupDefTable.SubSubID, ArticleGroupDefTable.GroupCode " & _
                     " FROM   ArticleGroupDefTable " & _
                     " WHERE (ArticleGroupDefTable.Active = 1) " & _
                     " ORDER BY ArticleGroupDefTable.SortOrder"
                Me.cmbCategory.DisplayMember = "Name"
                Me.cmbCategory.ValueMember = "Id"
                Me.cmbCategory.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
                ''filling type combo
                'strSQL = "select ArticleTypeId as Id, ArticleTypeName as Name from ArticleTypeDefTable where active=1 order by sortOrder"
                'Added Field TypeCode
                strSQL = "select ArticleTypeId as Id, ArticleTypeName as Name, TypeCode from ArticleTypeDefTable where active=1 order by sortOrder"
                Me.cmbType.DisplayMember = "Name"
                Me.cmbType.ValueMember = "ID"
                Me.cmbType.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
                ''filling Gender combo
                strSQL = "select ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable where active=1 order by sortOrder"
                Me.cmbGender.DisplayMember = "Name"
                Me.cmbGender.ValueMember = "ID"
                Me.cmbGender.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
                ''filling Unit combo
                strSQL = "select ArticleUnitId as Id, ArticleUnitName as Name from ArticleUnitDefTable where active=1 order by sortOrder"
                Me.cmbUnit.DisplayMember = "Name"
                Me.cmbUnit.ValueMember = "ID"
                Me.cmbUnit.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
                ''filling Distributor/LPO combo
                'strSQL = "SELECT     dbo.ArticleLpoDefTable.ArticleLpoId AS ID,dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId FROM         dbo.ArticleLpoDefTable INNER JOIN               dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId"
                'Added Field Sub Category Code
                strSQL = "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID,dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId , dbo.ArticleLpoDefTable.SubCategoryCode  FROM         dbo.ArticleLpoDefTable INNER JOIN               dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId"
                Me.cmbLPO.DisplayMember = "Name"
                Me.cmbLPO.ValueMember = "ID"
                Me.cmbLPO.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
                Me.cmbLPO.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                Me.cmbLPO.DisplayLayout.Bands(0).Columns("ArticleCompanyId").Hidden = True
                Me.cmbLPO.DisplayLayout.Bands(0).Columns("SubCategoryCode").Hidden = True
                Me.cmbLPO.DisplayLayout.Bands(0).Columns("Name").Width = 500
                ''filling combinitions list 
                strSQL = "select ArticleColorId as Id,ArticleColorName as Name from ArticleColorDefTable where active=1 order by sortOrder"
                Me.lstCombinitions.ListItem.DisplayMember = "Name"
                Me.lstCombinitions.ListItem.ValueMember = "ID"
                Me.lstCombinitions.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)

                ''filling sizes list
                strSQL = "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 order by sortOrder"
                Me.lstSizes.ListItem.DisplayMember = "Name"
                Me.lstSizes.ListItem.ValueMember = "ID"
                Me.lstSizes.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)
                '' 29-04-2017
                strSQL = "select ModelId as Id, Name from tblDefModelList where Active=1 order by Name"
                Me.lstModelList.ListItem.DisplayMember = "Name"
                Me.lstModelList.ListItem.ValueMember = "Id"
                Me.lstModelList.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)
                '' End 29-04-2017
                ''filling company list
                'strSQL = "SELECT   ArticleCompanyId AS ID, ArticleCompanyName AS Name" & _
                '        " FROM ArticleCompanyDefTable" & _
                '        " WHERE Active = 1"
                'Added Field CategoryCode
                strSQL = "SELECT   ArticleCompanyId AS ID, ArticleCompanyName AS Name, CategoryCode " & _
                       " FROM ArticleCompanyDefTable" & _
                       " WHERE Active = 1"
                Me.cmbCompany.DisplayMember = "Name"
                Me.cmbCompany.ValueMember = "ID"
                Me.cmbCompany.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
                ''filling Item Location
                strSQL = "SELECT location_id [Location ID],location_name [Location], '' [Rack], 'Delete' [Delete] FROM tblDefLocation"
                Me.grdItemLocation.DataSource = UtilityDAL.GetDataTable(strSQL)
                Me.grdItemLocation.RetrieveStructure()
                Me.ApplyGridSettings("ArticalLocation")
                ''filling Vendors 
                strSQL = "Select coa_detail_id, detail_title From vwCOADetail WHERE account_type='Vendor'"
                Me.cmbvendorslist.DisplayMember = "detail_title"
                Me.cmbvendorslist.ValueMember = "coa_detail_id"
                Me.cmbvendorslist.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
                strSQL = "Select coa_detail_id, detail_title From vwCOADetail WHERE account_type='Customer'"
                Me.ComboBox1.DisplayMember = "detail_title"
                Me.ComboBox1.ValueMember = "coa_detail_id"
                Me.ComboBox1.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
                strSQL = "select ArticleStatusID as ID, ArticleStatusName as Name from ArticleStatus where active=1"  'Task#102815
                Me.cmbStatus.DisplayMember = "Name"
                Me.cmbStatus.ValueMember = "ID"
                Me.cmbStatus.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
                strSQL = String.Empty
                strSQL = "Select ArticleBrandId, ArticleBrandName, [Description], Active, SortOrder From ArticleBrandDefTable Order By ArticleBrandName ASC"
                FillDropDown(Me.cmbBrand, strSQL)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Me.grdCostSheet.UpdateData()
            Me.grdItemLocation.UpdateData()
            Me.grdPackQty.UpdateData()
            Me.grdVendorItems.UpdateData()
            Me.grdCustomerList.UpdateData()
            Dim strArticleSizeCode As String = String.Empty
            Dim strArticleColorCode As String = String.Empty
            Dim str As String = String.Empty
            Me.mobjModel = New Article
            With mobjModel
                .ArticleID = Me.CurrentId
                If Condition <> Me.BtnDelete.Text Then
                    .ArticleCode = Me.uitxtItemCode.Text
                    .ArticleDescription = Me.uitxtItemName.Text
                    .ArticleRemarks = Me.txtRemarks.Text
                    .ArticleGroupID = Me.cmbCategory.SelectedValue
                    .ArticleTypeID = Me.cmbType.SelectedValue
                    .ArticleGenderID = Me.cmbGender.SelectedValue
                    .ArticleUnitID = Me.cmbUnit.SelectedValue
                    .ArticleStatusID = Me.cmbStatus.SelectedValue
                    .ArticleLPOID = Me.cmbLPO.Value
                    .PurchasePrice = Val(Me.uitxtPrice.Text)
                    .SalePrice = Val(Me.uitxtSalePrice.Text)
                    .PackQty = Val(Me.uitxtPackQty.Text)
                    .StockLevel = Val(Me.uitxtStockLevel.Text)
                    .StockLevelOpt = Val(Me.uitxtStockLevelOptimal.Text)
                    .StockLevelMax = Val(Me.uitxtStockLevelMaximum.Text)
                    .Active = Me.uichkActive.Checked
                    .SortOrder = Val(Me.uitxtSortOrder.Text)
                    .IsDate = Date.Today
                    .ArticlePicture = _str_Path
                    .AccountID = CType(Me.cmbCategory.SelectedItem, DataRowView).Item("SubSubID").ToString
                    .COADetail = New COADeail
                    .COADetail.COADetailID = Me.COADetailID
                    .COADetail.DetailTitle = .ArticleDescription
                    .COADetail.Active = Me.uichkActive.Checked
                    .ServiceItem = Me.chkServerItem.Checked
                    .TradePrice = Val(Me.txtTradePrice.Text)
                    .Freight = Val(Me.txtFreight.Text)
                    .MarketReturns = Val(Me.txtMarketReturns.Text)
                    .GST_Applicable = IIf(Me.rbtTax.Checked = True, True, False)
                    .FlatRate_Applicable = IIf(Me.rbtFlatRate.Checked = True, True, False)
                    .FlatRate = IIf(Me.rbtFlatRate.Checked = True, Val(Me.txtFlatRate.Text), 0)
                    .ItemWeight = Val(Me.txtItemWeight.Text)
                    .HS_Code = Me.txtHSCode.Text
                    .LargestPackQty = Val(Me.txtLargestPackQty.Text)
                    .AutoCode = blnAutoArticleCode
                    Dim strDeptCode As String = String.Empty
                    Dim strTypeCode As String = String.Empty
                    Dim strCateCode As String = String.Empty
                    Dim strSubCateCode As String = String.Empty
                    If Me.cmbCategory.SelectedIndex > 0 Then
                        strDeptCode = CType(Me.cmbCategory.SelectedItem, DataRowView).Row.Item("GroupCode").ToString
                    End If
                    If Me.cmbType.SelectedIndex > 0 Then
                        strTypeCode = CType(Me.cmbType.SelectedItem, DataRowView).Row.Item("TypeCode").ToString
                    End If
                    If Me.cmbCompany.SelectedIndex > 0 Then
                        strCateCode = CType(Me.cmbCompany.SelectedItem, DataRowView).Row.Item("CategoryCode").ToString
                    End If
                    .Prefix = CStr(strDeptCode & "-" & strTypeCode & "-" & strCateCode).ToString.Trim.ToUpper & "-"
                    Dim intLength As Integer = Val(getConfigValueByType("ArticleCodePrefixLength").ToString)
                    If intLength = 0 Then
                        .Length = 3
                    Else
                        .Length = Val(getConfigValueByType("ArticleCodePrefixLength").ToString)
                    End If
                    .ArticleCategoryId = IIf(Me.cmbCompany.SelectedIndex > 0, Me.cmbCompany.SelectedValue, 0)
                    'fill Detail
                    .ArticleBrandId = IIf(Me.cmbBrand.SelectedIndex > 0, Me.cmbBrand.SelectedValue, 0)
                    .ApplyAdjustmentFuelExp = IIf(Me.chkApplyAdjustmentFuelExpense.Checked = True, 1, 0)
                    .ArticleDetails = New List(Of ArticleDetail)
                    Dim dtl As ArticleDetail
                    Dim strSizeIDs As String() = Me.lstSizes.SelectedIDs.Split(",")
                    Dim strColorIDs As String() = Me.lstCombinitions.SelectedIDs.Split(",")

                    For Each SizeID As String In strSizeIDs
                        'ArticleSizeCode Here...
                        str = "Select IsNull(SizeCode,ArticleSizeId) as SizeCode From ArticleSizeDefTable WHERE ArticleSizeId =" & SizeID & ""
                        Dim dtArticleSizeCode As DataTable = GetDataTable(str)
                        If dtArticleSizeCode.Rows.Count > 0 Then
                            strArticleSizeCode = dtArticleSizeCode.Rows(0).Item(0).ToString
                        Else
                            strArticleSizeCode = SizeID
                        End If
                        For Each ColorID As String In strColorIDs
                            'ArticleColorCode Here...
                            str = "Select IsNull(ColorCode,ArticleColorId) as ColorCode From ArticleColorDefTable WHERE ArticleColorId=" & ColorID & ""
                            Dim dtArticleColorCode As DataTable = GetDataTable(str)
                            If dtArticleColorCode.Rows.Count > 0 Then
                                strArticleColorCode = dtArticleColorCode.Rows(0).Item(0).ToString
                            Else
                                strArticleColorCode = ColorID
                            End If

                            str = " Select ArticleId From ArticleDefTable where MasterId=" & Me.CurrentId & " and SizeRangeId=" & SizeID & " and ArticleColorId=" & ColorID
                            Dim dtArticleId As DataTable = GetDataTable(str)

                            dtl = New ArticleDetail
                            With dtl
                                If getConfigValueByType("chkAutoArticleCode").ToString = "False" Then
                                    If getConfigValueByType("EnabledSizeCombinationCodeOnArticleCode").ToString = "True" Then
                                        .ArticleCode = Me.uitxtItemCode.Text.Trim & "-" & strArticleSizeCode & "-" & strArticleColorCode
                                    Else
                                        .ArticleCode = Me.uitxtItemCode.Text
                                    End If
                                Else
                                    .ArticleCode = Me.uitxtItemCode.Text
                                End If
                                If dtArticleId.Rows.Count > 0 Then
                                    .ArticleID = dtArticleId.Rows(0).Item(0)
                                End If

                                .ArticleDescription = Me.uitxtItemName.Text
                                .ArticleGroupID = Me.cmbCategory.SelectedValue
                                .ArticleTypeID = Me.cmbType.SelectedValue
                                .ArticleGenderID = Me.cmbGender.SelectedValue
                                .ArticleStatusID = cmbStatus.SelectedValue
                                .ArticleUnitID = Me.cmbUnit.SelectedValue
                                .ArticleLPOID = Me.cmbLPO.Value
                                .PurchasePrice = Val(Me.uitxtPrice.Text)
                                .SalePrice = Val(Me.uitxtSalePrice.Text)
                                .PackQty = Val(Me.uitxtPackQty.Text)
                                .StockLevel = Val(Me.uitxtStockLevel.Text)
                                .StockLevelOpt = Val(Me.uitxtStockLevelOptimal.Text)
                                .StockLevelMax = Val(Me.uitxtStockLevelMaximum.Text)
                                .Active = Me.uichkActive.Checked
                                .SortOrder = Val(Me.uitxtSortOrder.Text)
                                .IsDate = Date.Today
                                .SizeRangeID = SizeID
                                .ArticleColorID = ColorID
                                .ServiceItem = Me.chkServerItem.Checked
                                .TradePrice = Val(Me.txtTradePrice.Text)
                                .Freight = Val(Me.txtFreight.Text)
                                .MarketReturns = Val(Me.txtMarketReturns.Text)
                                .GST_Applicable = IIf(Me.rbtTax.Checked = True, True, False)
                                .FlatRate_Applicable = IIf(Me.rbtFlatRate.Checked = True, True, False)
                                .FlatRate = IIf(Me.rbtFlatRate.Checked = True, Val(Me.txtFlatRate.Text), 0)
                                .ItemWeight = Val(Me.txtItemWeight.Text)
                                .HS_Code = Me.txtHSCode.Text
                                .LargestPackQty = Val(Me.txtLargestPackQty.Text)
                                .ArticleCategoryId = IIf(Me.cmbCompany.SelectedIndex > 0, Me.cmbCompany.SelectedValue, 0)
                                .ArticleBrandId = IIf(Me.cmbBrand.SelectedIndex > 0, Me.cmbBrand.SelectedValue, 0)
                                .ApplyAdjustmentFuelExp = IIf(Me.chkApplyAdjustmentFuelExpense.Checked = True, 1, 0)
                                '' Start 29-04-2017
                                .ArticleModelList = New List(Of ArticleModels)
                                Dim ArticleModel As ArticleModels
                                Dim strModelsIds As String() = Me.lstModelList.SelectedIDs.Split(",")
                                For Each model As String In strModelsIds
                                    ArticleModel = New ArticleModels
                                    ArticleModel.ModelId = Val(model)
                                    .ArticleModelList.Add(ArticleModel)
                                Next
                                '' End 29-04-2017
                            End With
                            .ArticleDetails.Add(dtl)
                        Next
                    Next

                    ''filling Aritcal Location Rank Model
                    .ArticalLocationRank = New List(Of ArticalLocationRank)
                    Dim objALR As ArticalLocationRank
                    Dim dtGrid As DataTable = CType(Me.grdItemLocation.DataSource, DataTable)
                    For Each dr As DataRow In dtGrid.Rows
                        objALR = New ArticalLocationRank
                        With objALR
                            .LocationID = Val(dr("Location ID").ToString)
                            .Rank = dr("Rack").ToString
                        End With
                        .ArticalLocationRank.Add(objALR)
                    Next

                    ''filling Increment Reduction
                    .IncrementReduction = New IncrementReduction

                    .IncrementReduction.IncrementDate = Date.Today

                    If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then

                        .IncrementReduction.Stock = 0
                        .IncrementReduction.PurchasePriceOld = 0
                        .IncrementReduction.SalePriceOld = 0

                    Else
                        'Dim strSQL As String = "SELECT     Stock  FROM vw_ArticleStock  WHERE(ArticleId = " & Me.CurrentId & ")"
                        'Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
                        'If dt.Rows.Count > 0 Then
                        '    .IncrementReduction.Stock = dt.Rows(0)(0)
                        'Else
                        '    .IncrementReduction.Stock = 0
                        'End If
                        .IncrementReduction.PurchasePriceOld = Val(Me.txtOldPurchasePrice.Text)
                        .IncrementReduction.SalePriceOld = Val(Me.txtOldSalePrice.Text)
                    End If

                    .IncrementReduction.PurchasePriceNew = Val(Me.uitxtPrice.Text)
                    .IncrementReduction.SalePriceNew = Val(Me.uitxtSalePrice.Text)
                Else

                End If

                ''filling activity log
                .ActivityLog = New ActivityLog
                .ActivityLog.ApplicationName = "Config"
                .ActivityLog.FormCaption = Me.Text
                .ActivityLog.FormName = ""

                'TODO : Define Loging User ID
                '.ActivityLog.UserID = 1
                .ActivityLog.UserID = LoginUserId
                .ActivityLog.RefNo = Me.uitxtItemCode.Text
                .ActivityLog.LogDateTime = Date.Now

                .ArticleDefVendorsitem = New List(Of ArticleDefVendorsItem)
                Dim ArticleVendors As ArticleDefVendorsItem

                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdVendorItems.GetRows
                    ArticleVendors = New ArticleDefVendorsItem
                    ArticleVendors.ArticleDefId = CurrentId
                    ArticleVendors.VendorId = r.Cells(1).Value
                    ArticleVendors.UserName = LoginUserName
                    ArticleVendors.DateLog = Date.Today
                    .ArticleDefVendorsitem.Add(ArticleVendors)
                Next

                .ArticleCustomerItem = New List(Of ArticleDefCustomersItem)
                Dim ActiculeDefCustomer As ArticleDefCustomersItem
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdCustomerList.GetRows
                    ActiculeDefCustomer = New ArticleDefCustomersItem
                    ActiculeDefCustomer.ArticleDefId = CurrentId
                    ActiculeDefCustomer.CustomerId = r.Cells(1).Value
                    ActiculeDefCustomer.UserName = LoginUserName
                    ActiculeDefCustomer.DateLog = Date.Today
                    .ArticleCustomerItem.Add(ActiculeDefCustomer)
                Next


            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        'Try

        '    'If Me.cmbCategory.Items.Count > 0 Then
        '    '    If Not Me.cmbCategory.SelectedValue > 0 Then
        '    '        If Me.grdAllRecords.RecordCount > 0 Then grdAllRecords.DataSource.clear()
        '    '        Exit Sub
        '    '    End If
        '    'Else
        '    '    Exit Sub
        '    'End If
        '    If Condition = Nothing Then
        '        Dim dt As DataTable = New ArticleDAL().GetAll()

        '        If dt.Rows.Count > 0 Then
        '            Dim uk As New UniqueConstraint("uk", dt.Columns(EnumGrid.ArticleID), True)
        '            dt.Constraints.Add(uk)
        '        End If

        '        'Me.BindingSource1.Clear()
        '        Me.grdAllRecords.DataSource = Nothing
        '        Me.BindingSource1.DataSource = dt
        '        Me.grdAllRecords.DataSource = Me.BindingSource1
        '        Me.grdAllRecords.RetrieveStructure()
        '        Me.ApplyGridSettings()
        '        CtrlGrdBar1_Load(Nothing, Nothing)
        '    End If

        '    If Condition = "packQty" Then
        '        Dim id As Integer = Me.grdAllRecords.CurrentRow.Cells("ArticleId").Value
        '        Dim dt1 As DataTable = New ArticleDAL().GetPackQty(id)
        '        Me.grdPackQty.DataSource = dt1
        '        'Me.grdPackQty.AutoSizeColumns()
        '    End If

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        Try
            If Not Me.cmbCategory.SelectedValue > 0 Then
                'MsgBox("Please select Category", MsgBoxStyle.Information, str_MessageHeader)
                Dim frm As New frmMessage
                frm.MsgBox("Please select Category", str_MessageHeader)
                'msg_Confirm("Please select Category")
                Me.cmbCategory.Focus()
                Return False
            ElseIf Not Me.cmbType.SelectedValue > 0 Then
                Dim frm As New frmMessage
                frm.MsgBox("Please select type", str_MessageHeader)
                Me.cmbType.Focus()
                Return False
            ElseIf Not Me.cmbUnit.SelectedValue > 0 Then

                Dim frm As New frmMessage
                frm.MsgBox("Please select unit", str_MessageHeader)
                Me.cmbUnit.Focus()
                Return False
            ElseIf Me.uitxtItemCode.Text = String.Empty Then
                Dim frm As New frmMessage
                frm.MsgBox("Please enter valid item code", str_MessageHeader)
                Me.uitxtItemCode.Focus()
                Return False

            ElseIf Me.uitxtItemName.Text = String.Empty Then

                Dim frm As New frmMessage
                frm.MsgBox("Please enter valid item name", str_MessageHeader)
                Me.uitxtItemName.Focus()
                Return False


            ElseIf Not Me.lstSizes.SelectedIDs.Length > 0 Then

                Dim frm As New frmMessage
                frm.MsgBox("Please select size", str_MessageHeader)
                Me.lstSizes.ListItem.Focus()
                Return False


            ElseIf Not Me.lstCombinitions.SelectedIDs.Length > 0 Then
                Dim frm As New frmMessage
                frm.MsgBox("Please select color", str_MessageHeader)
                Me.lstCombinitions.ListItem.Focus()
                Return False

                'If Not Me.uitxtPrice.Text > 0 Then
                '    msg_Error("Please enter valid purchase price")
                '    Me.uitxtPrice.Focus()
                '    Exit Sub
                'End If

                'If Not Me.uitxtSalePrice.Text > 0 Then
                '    msg_Error("Please enter valid sale price")
                '    Me.uitxtSalePrice.Focus()
                '    Exit Sub
                'End If

            ElseIf Not Me.uitxtPackQty.Text > 0 Then

                Dim frm As New frmMessage
                frm.MsgBox("Please enter minimum pack quantity of 1", str_MessageHeader)
                Me.uitxtPackQty.Focus()
                Return False
            End If

            'Article Code Validate On Aritlce Master Table .... 
            If Me.BtnSave.Text = "&Save" Then
                Dim str As String = "Select * From ArticleDefTableMaster WHERE ArticleCode='" & Me.uitxtItemCode.Text.Replace("'", "''") & "'"
                Dim dtItemCode As DataTable = GetDataTable(str)
                If dtItemCode.Rows.Count > 0 Then
                    Dim frm As New frmMessage
                    frm.MsgBox("Item Code Already Exist " & vbCrLf & dtItemCode.Rows(0).Item(1).ToString + "-" + dtItemCode.Rows(0).Item(2).ToString, str_MessageHeader)
                    'ShowErrorMessage("Item Code Already Exist " & vbCrLf & dtItemCode.Rows(0).Item(1).ToString + "-" + dtItemCode.Rows(0).Item(2).ToString)
                    Me.uitxtItemCode.Focus()
                    Return False
                End If
            End If

            If Me.BtnSave.Text = "Update" Or Me.BtnSave.Text = "&Update" Then
                If Me.uitxtPrice.Text.Trim <> Me.txtOldPurchasePrice.Text.Trim Or Me.uitxtSalePrice.Text.Trim <> Me.txtOldSalePrice.Text.Trim Then
                    If Not ShowConfirmationMessage("Price has been change.Do you still want to save changes?", MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        Return False
                    End If
                End If
            End If


            Me.FillModel()

            Return True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            _str_Path = String.Empty
            Me.BtnSave.Text = "&Save"
            'If Me.uicmbCategory.ActiveRow.Cells(0).Value > 0 Then Me.uitxtItemCode.Text = GetNextDocNo("", 6, "ArticleDefTable", "ArticleCode") Else Me.uitxtItemCode.Text = ""

            Me.uitxtItemCode.Text = ""
            Me.cmbCategory.Focus()
            Me.uitxtItemName.Text = ""
            Me.txtRemarks.Text = String.Empty
            Me.uitxtPackQty.Text = 1
            Me.uitxtStockLevel.Text = 0
            Me.uitxtStockLevelMaximum.Text = 0
            Me.uitxtStockLevelOptimal.Text = 0
            Me.uitxtPrice.Text = 0
            Me.uitxtSalePrice.Text = 0
            Me.uitxtSortOrder.Text = 1
            Me.txtItemWeight.Text = 1
            Me.txtHSCode.Text = String.Empty
            Me.txtLargestPackQty.Text = 1
            'Me.txtAccountID.Text = 0
            Me.uichkActive.Checked = True
            If getConfigValueByType("ServiceItem").ToString = "True" Then
                Me.chkServerItem.Visible = True
                Me.chkServerItem.Checked = False
            Else
                Me.chkServerItem.Visible = False
                Me.chkServerItem.Checked = False
            End If
            Me.txtTradePrice.Text = 0
            Me.txtFreight.Text = 0
            Me.txtMarketReturns.Text = 0
            Me.txtCostPrice.Text = 0
            Me.rbtTax.Checked = True
            Me.txtFlatRate.Text = 0
            Me.chkApplyAdjustmentFuelExpense.Checked = True
            Me.cmbCategory.SelectedIndex = 0
            If Me.cmbCompany.Items.Count > 1 Then Me.cmbCompany.SelectedIndex = 1 Else Me.cmbCompany.SelectedIndex = 0
            If Me.cmbType.Items.Count > 1 Then Me.cmbType.SelectedIndex = 1 Else Me.cmbType.SelectedIndex = 0
            If Me.cmbBrand.Items.Count > 1 Then Me.cmbBrand.SelectedIndex = 1 Else Me.cmbBrand.SelectedIndex = 0
            Me.lstCombinitions.DeSelect()
            Me.lstSizes.DeSelect()
            Me.lstModelList.DeSelect()
            If Me.lstCombinitions.ListItem.Items.Count > 0 Then Me.lstCombinitions.ListItem.SelectedIndex = 0
            If Me.lstSizes.ListItem.Items.Count > 0 Then Me.lstSizes.ListItem.SelectedIndex = 0

            If Me.cmbType.Items.Count > 1 Then
                Me.cmbType.SelectedIndex = 1
            ElseIf Me.cmbType.Items.Count > 0 Then
                Me.cmbType.SelectedIndex = 0
            End If

            If Me.cmbGender.Items.Count > 1 Then
                Me.cmbGender.SelectedIndex = 1
            ElseIf Me.cmbGender.Items.Count > 0 Then
                Me.cmbGender.SelectedIndex = 0
            End If

            If Me.cmbUnit.Items.Count > 1 Then
                Me.cmbUnit.SelectedIndex = 1
            ElseIf Me.cmbUnit.Items.Count > 0 Then
                Me.cmbUnit.SelectedIndex = 0
            End If
            Me.cmbLPO.Text = ""
            'If Me.cmbLPO.Items.Count > 1 Then
            '    Me.cmbLPO.SelectedIndex = 1
            'ElseIf Me.cmbLPO.Items.Count > 0 Then
            '    Me.cmbLPO.SelectedIndex = 0
            'End If
            ' If Me.uicmbCategory.Rows.Count > 0 Then Me.uicmbCategory.Rows(0).Activate()

            Me.CurrentId = 0I
            Me.COADetailID = 0I
            Me.mobjModel = Nothing

            Dim strSQL As String

            strSQL = "SELECT ArticleDefTable.ArticleId,ArticleDefTable.ArticleCode,ArticleSizeDefTable.ArticleSizeId AS Size," & _
               "ArticleColorDefTable.ArticleColorId AS Color,ArticleDefTable.PurchasePrice AS Cost,ArticleDefTable.SalePrice AS Retail, ISNULL(ArticleDefTable.Active,0) as Active, 'Delete' [Delete], 'Update' as [Update] " & _
               "FROM ArticleDefTable INNER JOIN ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId INNER JOIN " & _
               "ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId Where MasterID = 0"

            Dim dtGrid As DataTable = UtilityDAL.GetDataTable(strSQL)
            Me.grdAriticals.DataSource = Nothing
            Me.grdAriticals.DataSource = dtGrid
            Me.grdAriticals.RetrieveStructure()

            strSQL = String.Empty
            strSQL = "Select ArticleColorId, ArticleColorName From ArticleColorDefTable ORDER BY 1 ASC"
            Dim dtColor As New DataTable
            dtColor = GetDataTable(strSQL)
            grdAriticals.RootTable.Columns("Color").HasValueList = True
            grdAriticals.RootTable.Columns("Color").LimitToList = True
            grdAriticals.RootTable.Columns("Color").EditType = Janus.Windows.GridEX.EditType.Combo
            grdAriticals.RootTable.Columns("Color").ValueList.PopulateValueList(dtColor.DefaultView, "ArticleColorId", "ArticleColorName")

            strSQL = String.Empty
            strSQL = "Select ArticleSizeId, ArticleSizeName From ArticleSizeDefTable ORDER BY 1 ASC"
            Dim dtSize As New DataTable
            dtSize = GetDataTable(strSQL)

            grdAriticals.RootTable.Columns("Size").HasValueList = True
            grdAriticals.RootTable.Columns("Size").LimitToList = True
            grdAriticals.RootTable.Columns("Size").EditType = Janus.Windows.GridEX.EditType.Combo
            grdAriticals.RootTable.Columns("Size").ValueList.PopulateValueList(dtSize.DefaultView, "ArticleSizeId", "ArticleSizeName")

            For c As Integer = 0 To Me.grdAriticals.RootTable.Columns.Count - 1
                If Not Me.grdAriticals.RootTable.Columns(c).Index = 2 AndAlso Not Me.grdAriticals.RootTable.Columns(c).Index = 3 AndAlso Not Me.grdAriticals.RootTable.Columns(c).Index = 6 Then
                    Me.grdAriticals.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next


            Me.ApplyGridSettings("Artical Grid")


            'strSQL = "SELECT location_id [Location ID],location_name [Location],'' [Ranks], 'Delete' [Delete] FROM tblDefLocation"
            strSQL = "SELECT location_id [Location ID],location_name [Location],'' [Rack] FROM tblDefLocation"
            Me.grdItemLocation.DataSource = Nothing
            Me.grdItemLocation.DataSource = UtilityDAL.GetDataTable(strSQL)
            Me.grdItemLocation.RetrieveStructure()
            Me.ApplyGridSettings("ArticalLocation")
            Me.MakeCostSheetTable(-1)
            MakeVendorsItem(-1)
            MakeCustomersItem(-1)
            'Before  ---> 21-Dec-2013 ReqId-958 M Ijaz Javed   Add security rights of price update against cost sheet
            'Me.ApplySecurity(EnumDataMode.[New])
            '' 21-Dec-2013 ReqId-958 M Ijaz Javed   Add security rights of price update against cost sheet
            'GetAllRecords()
            GetSecurityRights()
            'Me.BtnNew.Enabled = False
            Me.BtnDelete.Enabled = False
            '''''''''''''''''''''


            Me.PictureBox1.ImageLocation = String.Empty
            Me.txtPackName.Text = String.Empty
            Me.txtQuantity.Text = String.Empty
            Me.cmbCostSheetUnit.SelectedIndex = 0

            Me.tabAriticalDetail.SelectedTab = Me.tabAriticalDetail.TabPages(0)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If Not Me.IsValidate Then Exit Function

            'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Function
            If New ArticleDAL().Add(Me.mobjModel) Then
                If _str_Path <> String.Empty Then
                    If PictureBox1.Image IsNot Nothing Then
                        Try
                            'Dim DirInfo As IO.DirectoryInfo = New IO.DirectoryInfo(_ArticlePicPath)
                            'Dim FolderSecurity As New Security.AccessControl.DirectorySecurity
                            'FolderSecurity.AddAccessRule(New Security.AccessControl.FileSystemAccessRule(Environment.UserDomainName & "\\" & Environment.UserName, Security.AccessControl.FileSystemRights.FullControl, Security.AccessControl.AccessControlType.Allow))
                            'DirInfo.SetAccessControl(FolderSecurity)
                            If _str_Path.Length > 1 Then
                                If IO.File.Exists(_str_Path) Then
                                    File.Delete(_str_Path)
                                    PictureBox1.Image.Save(_str_Path, System.Drawing.Imaging.ImageFormat.Jpeg)
                                    'PictureBox1.Image.Dispose()
                                    Me.PictureBox1.ImageLocation = String.Empty
                                Else
                                    'Ahmad Sharif : Check path of saving image, if not exist create directory and save image, MOdification on 04-06-2015
                                    Dim dirPath As String = _str_Path.Substring(0, _str_Path.LastIndexOf("\")) 'Returns the directory e.g. C:\ARTICLEIMAGES

                                    If Not IO.Directory.Exists(dirPath) Then
                                        System.IO.Directory.CreateDirectory(dirPath)
                                    End If
                                    PictureBox1.Image.Save(_str_Path, System.Drawing.Imaging.ImageFormat.Jpeg)
                                    'PictureBox1.Image.Dispose()
                                    Me.PictureBox1.ImageLocation = String.Empty
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                End If
                ' msg_Information(str_informSave)
                Dim frm As New frmMessage
                'frm.MsgBox("Item Saved successfully", str_MessageHeader)
                MsgBox("Item Saved successfully", MsgBoxStyle.Information, str_MessageHeader)

                '                Me.GetAllRecords()
                Me.ReSetControls()
            End If
            _str_Path = String.Empty  'Task#05082015
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal mode As EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        'Try

        '    If mode = EnumDataMode.[New] Then
        '        Me.BindingNavigator1.Enabled = False

        '    ElseIf mode = EnumDataMode.Edit Then
        '        Me.BindingNavigator1.Enabled = True
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If Me.tabAriticalDetail.SelectedTab Is Me.TabPgCostSheet Then
                'If Me.grdCostSheet.RecordCount = 0 Then Exit Function
                Dim dtGrid As DataTable = CType(Me.grdCostSheet.DataSource, DataTable)
                dtGrid.AcceptChanges()
                If dtGrid.Rows.Count > 0 Then
                    Call New ArticleDAL().AddCostSheet(dtGrid)
                    'ShowInformationMessage(gstrMsgAfterUpdate)
                Else
                    Call New ArticleDAL().AddCostSheet(dtGrid, CurrentId)
                    'ShowInformationMessage(gstrMsgAfterUpdate)
                End If
                'Me.MakeCostSheetTable(-1)
            Else
                If Not Me.IsValidate Then Exit Function
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Function
                If New ArticleDAL().Update(Me.mobjModel) Then
                    If PictureBox1.Image IsNot Nothing Then
                        Try
                            If IO.Directory.Exists(_ArticlePicPath) Then
                                'Dim DirInfo As IO.DirectoryInfo = New IO.DirectoryInfo(_ArticlePicPath)
                                'Dim FolderSecurity As New Security.AccessControl.DirectorySecurity
                                'FolderSecurity.AddAccessRule(New Security.AccessControl.FileSystemAccessRule(Environment.UserDomainName & "\\" & Environment.UserName, Security.AccessControl.FileSystemRights.FullControl, Security.AccessControl.AccessControlType.Allow))
                                'DirInfo.SetAccessControl(FolderSecurity)
                                If _str_Path.Length > 1 Then
                                    If IO.File.Exists(_str_Path) Then
                                        File.Delete(_str_Path)
                                        PictureBox1.Image.Save(_str_Path, System.Drawing.Imaging.ImageFormat.Jpeg)
                                        'PictureBox1.Image.Dispose()
                                        Me.PictureBox1.ImageLocation = String.Empty
                                    Else
                                        PictureBox1.Image.Save(_str_Path, System.Drawing.Imaging.ImageFormat.Jpeg)
                                        'PictureBox1.Image.Dispose()
                                        Me.PictureBox1.ImageLocation = String.Empty
                                    End If
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                    'msg_Information(str_informUpdate)
                    Me.GetAllRecords()
                    Me.ReSetControls() '' 18-Mar-2014 TASK:2503 Imran Ali ResetControl After Update On Inventory Item 

                    Dim dt As DataTable = CType(CType(Me.grdAllRecords.DataSource, BindingSource).DataSource, DataTable)
                    Dim drFind As DataRow = dt.Rows.Find(Me.CurrentId)
                    If Not drFind Is Nothing Then
                        Me.grdAllRecords.Row = dt.Rows.IndexOf(drFind)
                    End If

                End If
            End If

            _str_Path = String.Empty 'Task#05082015
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Function

    Private Sub frmDefArticle_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 16 -1-14
            If e.KeyCode = Keys.F4 Then
                If BtnSave.Enabled = True Then
                    SaveToolStripButton_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.D And e.Alt Then
                If Me.BtnDelete.Enabled = False Then
                    RemoveHandler Me.BtnDelete.Click, AddressOf Me.DeleteToolStripButton_Click
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub frmDefArticle_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 
        Me.BtnCancel.Visible = False
        Me.BtnDelete.Visible = False
        Me.btnPriceUpdate.Visible = False
        Me.BtnPrint.Visible = False
        Me.btnCostPriceUpdate.Visible = False
        Me.BtnNew.Enabled = True
        ''07-June-2015 Task# 7062015 Ahmad Sharif: set picture box SizeMode property to StretchImage
        Me.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        ''end Task# 7062015

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try

            ''filling the combos
            Me.FillCombos()

            ''getting the grid
            'Me.GetAllRecords()

            ''filling grid
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            If Not getConfigValueByType("chkAutoArticleCode").ToString = "Error" Then
                blnAutoArticleCode = getConfigValueByType("chkAutoArticleCode")
            End If
            IsOpenForm = True
            Me.ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub frmDefArticle_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Private Sub lblCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblCategory.Click
        Try
            Me.FillCombos(Me.cmbCategory.Name)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblType.Click
        Try
            Me.FillCombos(Me.cmbType.Name)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblGender_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblGender.Click
        Try
            Me.FillCombos(Me.cmbGender.Name)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblCombinition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblCombinition.Click
        Try
            Dim strIDs As String = Me.lstCombinitions.SelectedIDs
            Me.FillCombos(Me.lstCombinitions.Name)
            Me.lstCombinitions.SelectItemsByIDs(strIDs)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblSize.Click
        Try
            Dim strIDs As String = Me.lstSizes.SelectedIDs
            Me.FillCombos(Me.lstSizes.Name)
            Me.lstSizes.SelectItemsByIDs(strIDs)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub grdAllRecords_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.grdAllRecords.RecordCount = 0 Then
                'Me.ReSetControls()
                Exit Sub
            End If
            RemoveHandler cmbCategory.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged
            RemoveHandler cmbType.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged
            'RemoveHandler cmbCompany.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged
            If Me.grdAllRecords.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                Me.grdAllRecords.Row -= 1
            End If
            If Not grdAllRecords.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then Exit Sub
            Me.uitxtItemCode.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.ArticleCode).Value.ToString
            Me.uitxtItemName.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.Description).Value.ToString
            'Me.uicmbSize.Text = Me.grdAllRecords.GetRow().Cells("ArticleSizeName").Value.ToString
            'Me.uicmbColor.Text = Me.grdAllRecords.GetRow().Cells("ArticleColorName").Value.ToString
            Me.cmbCategory.SelectedValue = Val(Me.grdAllRecords.GetRow.Cells(EnumGrid.GroupId).Value.ToString)
            Me.cmbType.SelectedValue = Val(Me.grdAllRecords.GetRow().Cells(EnumGrid.TypeId).Value.ToString)
            Me.cmbBrand.SelectedValue = Val(Me.grdAllRecords.GetRow.Cells(EnumGrid.ArticleBrandId).Value.ToString)
            'If Not Me.cmbCompany.SelectedIndex = -1 Then Me.cmbCompany.SelectedIndex = 0
            Me.cmbGender.SelectedValue = Val(Me.grdAllRecords.GetRow().Cells(EnumGrid.ArticleGenderId).Value.ToString)
            'If Me.grdAllRecords.GetRow().Cells(EnumGrid.LPO).Text <> String.Empty Then Me.cmbLPO.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.LPO).Value.ToString Else Me.cmbLPO.SelectedIndex = 0
            'If Me.cmbLPO.SelectedIndex > 0 Then
            'RemoveHandler cmbCompany.SelectedIndexChanged, AddressOf cmbCompany_SelectedIndexChanged
            'Me.cmbCompany.SelectedValue = CType(Me.cmbLPO.SelectedItem, DataRowView).Item(2)
            '    AddHandler cmbCompany.SelectedIndexChanged, AddressOf cmbCompany_SelectedIndexChanged
            'Else
            '    Me.cmbCompany.SelectedIndex = 0
            'End If
            'Me.cmbCategory.SelectedValue = Val(Me.grdAllRecords.GetRow().Cells(EnumGrid.CategoryId).Value.ToString)
            'AddHandler cmbCompany.SelectedIndexChanged, AddressOf cmbCompany_SelectedIndexChanged
            If Me.grdAllRecords.GetRow().Cells(EnumGrid.Unit).Text <> String.Empty Then Me.cmbUnit.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.Unit).Value Else Me.cmbUnit.SelectedIndex = 0
            Me.cmbCompany.SelectedValue = Val(Me.grdAllRecords.GetRow().Cells(EnumGrid.CategoryId).Value.ToString)
            Me.cmbLPO.Value = Val(Me.grdAllRecords.GetRow().Cells(EnumGrid.ArticleLpoId).Value.ToString)
            Me.uitxtPackQty.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.PackQty).Value.ToString
            Me.uitxtStockLevelOptimal.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.StockOpt).Value.ToString
            Me.uitxtStockLevelMaximum.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.StockMax).Value.ToString
            Me.uitxtStockLevel.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.StockLevel).Value.ToString
            Me.uitxtPrice.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.PurchasePrice).Value.ToString
            Me.txtOldPurchasePrice.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.PurchasePrice).Value.ToString
            Me.uitxtSalePrice.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.SalePrice).Value.ToString
            Me.txtOldSalePrice.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.SalePrice).Value.ToString
            Me.uitxtSortOrder.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.SortOrder).Value.ToString
            Me.txtRemarks.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.Remarks).Value.ToString
            Me.uichkActive.Checked = Me.grdAllRecords.GetRow().Cells(EnumGrid.Active).Value.ToString
            Me.cmbStatus.SelectedValue = Val(Me.grdAllRecords.GetRow().Cells(EnumGrid.StatusID).Value.ToString)
            Me.chkServerItem.Checked = Me.grdAllRecords.GetRow().Cells(EnumGrid.ServiceItem).Value
            Me.txtTradePrice.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.TradePrice).Value
            Me.txtFreight.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.Freight).Value
            Me.txtMarketReturns.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.MarketReturns).Value
            Me.txtItemWeight.Text = Me.grdAllRecords.GetRow.Cells(EnumGrid.ItemWeight).Value & ""
            Me.txtHSCode.Text = Me.grdAllRecords.GetRow.Cells(EnumGrid.HSCode).Value & ""
            Me.txtCostPrice.Text = Val(Me.grdAllRecords.GetRow.Cells(EnumGrid.Cost_Price).Value.ToString)
            Me.txtLargestPackQty.Text = Val(Me.grdAllRecords.GetRow.Cells(EnumGrid.LargestPackQty).Value.ToString)
            If IsDBNull(Me.grdAllRecords.GetRow().Cells(EnumGrid.ApplyAdjustmentFuelExp).Value) Then
                Me.chkApplyAdjustmentFuelExpense.Checked = False
            Else
                Me.chkApplyAdjustmentFuelExpense.Checked = Me.grdAllRecords.GetRow().Cells(EnumGrid.ApplyAdjustmentFuelExp).Value
            End If
            If Me.grdAllRecords.GetRow().Cells(EnumGrid.GST_Applicable).Value = True Then
                Me.rbtTax.Checked = True
            ElseIf Me.grdAllRecords.GetRow().Cells(EnumGrid.FlatRate_Applicable).Value = True Then
                Me.rbtFlatRate.Checked = True
            Else
                Me.rbtTax.Checked = True
            End If
            Me.txtFlatRate.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.FlatRate).Value
            If Not IsDBNull(Me.grdAllRecords.GetRow.Cells(EnumGrid.ArticlePicture).Text) Then
                If IO.File.Exists(Me.grdAllRecords.GetRow.Cells(EnumGrid.ArticlePicture).Text) Then
                    Try
                        _str_Path = Me.grdAllRecords.GetRow.Cells(EnumGrid.ArticlePicture).Text
                        Me.PictureBox1.ImageLocation = Me.grdAllRecords.GetRow.Cells(EnumGrid.ArticlePicture).Text 'New Bitmap(Me.grdAllRecords.GetRow.Cells(EnumGrid.ArticlePicture).Text)
                        PictureBox1.Update()
                    Catch ex As Exception

                    End Try
                Else
                    Me.PictureBox1.Image = Nothing
                End If
            Else
                Me.PictureBox1.Image = Nothing
            End If


            ' Me.txtAccountID.Text = Me.grdAllRecords.GetRow().Cells(EnumGrid.AccountID).Value.ToString
            Me.CurrentId = Me.grdAllRecords.GetRow().Cells(EnumGrid.ArticleId).Value.ToString
            Me.COADetailID = Me.grdAllRecords.GetRow().Cells(EnumGrid.AccountID).Value.ToString
            ''get sizes\
            Dim strSQL As String = "Select Distinct SizeRangeID from ArticleDefTable where MasterID = " & Me.CurrentId
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Dim strIDs As String = String.Empty
            Me.lstSizes.DeSelect()
            If dt.Rows.Count > 0 Then

                For Each r As DataRow In dt.Rows
                    If strIDs.Length = 0 Then
                        strIDs = r.Item(0)
                    Else
                        strIDs = strIDs & "," & r.Item(0)
                    End If
                Next
            End If

            Me.lstSizes.SelectItemsByIDs(strIDs)

            strIDs = String.Empty

            ''get color
            strSQL = "Select Distinct ArticleColorID from ArticleDefTable where MasterID = " & Me.CurrentId
            dt = UtilityDAL.GetDataTable(strSQL)
            Me.lstCombinitions.DeSelect()
            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    If strIDs.Length = 0 Then
                        strIDs = r.Item(0)
                    Else
                        strIDs = strIDs & "," & r.Item(0)
                    End If
                Next
            End If
            Me.lstCombinitions.SelectItemsByIDs(strIDs)
            ''Get models dated 29-04-2017
            strIDs = String.Empty
            strSQL = "Select Distinct ModelId from ArticleModelList Where ArticleMasterId = " & Me.CurrentId
            dt = UtilityDAL.GetDataTable(strSQL)
            Me.lstModelList.DeSelect()
            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    If strIDs.Length = 0 Then
                        strIDs = r.Item(0)
                    Else
                        strIDs = strIDs & "," & r.Item(0)
                    End If
                Next
            End If
            Me.lstModelList.SelectItemsByIDs(strIDs)



            '' 

            Me.BtnSave.Text = "&Update"
            strSQL = "SELECT ArticleDefTable.ArticleId,ArticleDefTable.ArticleCode,ArticleSizeDefTable.ArticleSizeId AS Size," & _
                "ArticleColorDefTable.ArticleColorId AS Color,ArticleDefTable.PurchasePrice AS Cost,ArticleDefTable.SalePrice AS Retail, ISNULL(ArticleDefTable.Active,0) as Active, 'Delete' [Delete], 'Update' as [Update] " & _
                "FROM ArticleDefTable INNER JOIN ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId INNER JOIN " & _
                "ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId Where MasterID=" & Me.CurrentId
            Dim dtGrid As DataTable = UtilityDAL.GetDataTable(strSQL)
            Me.grdAriticals.DataSource = Nothing
            Me.grdAriticals.DataSource = dtGrid
            Me.grdAriticals.RetrieveStructure()
            strSQL = String.Empty
            strSQL = "Select ArticleColorId, ArticleColorName From ArticleColorDefTable ORDER BY 1 ASC"
            Dim dtColor As New DataTable
            dtColor = GetDataTable(strSQL)
            grdAriticals.RootTable.Columns("Color").HasValueList = True
            grdAriticals.RootTable.Columns("Color").LimitToList = True
            grdAriticals.RootTable.Columns("Color").EditType = Janus.Windows.GridEX.EditType.Combo
            grdAriticals.RootTable.Columns("Color").ValueList.PopulateValueList(dtColor.DefaultView, "ArticleColorId", "ArticleColorName")

            strSQL = String.Empty
            strSQL = "Select ArticleSizeId, ArticleSizeName From ArticleSizeDefTable ORDER BY 1 ASC"
            Dim dtSize As New DataTable
            dtSize = GetDataTable(strSQL)
            dtSize.AcceptChanges()
            grdAriticals.RootTable.Columns("Size").HasValueList = True
            grdAriticals.RootTable.Columns("Size").LimitToList = True
            grdAriticals.RootTable.Columns("Size").EditType = Janus.Windows.GridEX.EditType.Combo
            grdAriticals.RootTable.Columns("Size").ValueList.PopulateValueList(dtSize.DefaultView, "ArticleSizeId", "ArticleSizeName")
            For c As Integer = 0 To Me.grdAriticals.RootTable.Columns.Count - 1
                If Not Me.grdAriticals.RootTable.Columns(c).Index = 2 AndAlso Not Me.grdAriticals.RootTable.Columns(c).Index = 3 AndAlso Not Me.grdAriticals.RootTable.Columns(c).Index = 6 Then
                    Me.grdAriticals.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            'strSQL = "SELECT tblDefLocation.location_id [Location ID],tblDefLocation.location_name [Location],ArticalDefLocation.Ranks [Ranks], ArticalDefLocation.ArticalID [Delete],  " & _
            '    "FROM tblDefLocation LEFT OUTER JOIN ArticalDefLocation ON tblDefLocation.location_id = ArticalDefLocation.LocationID Where  ArticalDefLocation.ArticalID=" & Me.CurrentId
            strSQL = "SELECT tblDefLocation.location_id [Location ID],tblDefLocation.location_name [Location],ArticalDefLocation.Ranks [Rack]  " & _
                "FROM tblDefLocation LEFT OUTER JOIN ArticalDefLocation ON tblDefLocation.location_id = ArticalDefLocation.LocationID Where  ArticalDefLocation.ArticalID=" & Me.CurrentId

            Me.grdItemLocation.DataSource = Nothing
            Me.grdItemLocation.DataSource = UtilityDAL.GetDataTable(strSQL)
            Me.grdItemLocation.RetrieveStructure()
            If Me.grdItemLocation.RecordCount = 0 Then
                'strSQL = "SELECT location_id [Location ID],location_name [Location],'' [Ranks], 'Delete' [Delete] FROM tblDefLocation"
                strSQL = "SELECT location_id [Location ID],location_name [Location], '' [Rack] FROM tblDefLocation"
                Me.grdItemLocation.DataSource = Nothing
                Me.grdItemLocation.DataSource = UtilityDAL.GetDataTable(strSQL)
                Me.grdItemLocation.RetrieveStructure()
            End If

            Me.ApplyGridSettings("ArticalLocation")

            'Before ----> '' 21-Dec-2013 ReqId-958 M Ijaz Javed   Add security rights of price update against cost sheet
            'Me.ApplySecurity(EnumDataMode.Edit)

            '' 21-Dec-2013 ReqId-958 M Ijaz Javed   Add security rights of price update against cost sheet
            Me.BtnNew.Enabled = True
            Me.BtnDelete.Enabled = True
            '''''''''''''''''''''''''''''''
            GetSecurityRights()
            Me.ApplyGridSettings("Artical Grid")

            Me.MakeCostSheetTable(Me.CurrentId)
            Me.MakeVendorsItem(Me.CurrentId)
            Me.MakeCustomersItem(Me.CurrentId)
            'Me.GetItemDetailByMasterId(Me.CurrentId)

            AddHandler cmbCategory.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged
            AddHandler cmbType.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged
            AddHandler cmbCompany.SelectedIndexChanged, AddressOf cmbCategory_SelectedIndexChanged

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.BtnSave.Enabled = False Then Exit Sub
        Try
            If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                Me.Save()
            Else
                Me.Update1()

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.ReSetControls()
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.grdAllRecords_SelectionChanged(sender, e)
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            Me.Delete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdAllRecords_RowDoubleClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdAllRecords.RowDoubleClick
        Try
            _str_Path = String.Empty 'Task#05082015
            Me.grdAllRecords_SelectionChanged(sender, Nothing)
            ' Me.TabControl1.SelectedIndex = 0
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0)
            GetAllRecords("packQty")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    'Private Sub grdAllRecords_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdAllRecords.KeyUp
    '    If e.KeyCode = Keys.Enter Then
    '        Me.grdAllRecords_SelectionChanged(sender, Nothing)
    '        'Me.TabControl1.SelectedIndex = 0
    '        Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0)
    '    End If

    'End Sub

    Private Sub btnAddCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLPO.Click, btnAddType.Click, btnAddGender.Click, btnAddCompany.Click, btnAddUnit.Click, btnAddBrand.Click

        Try
            Dim btn As Button = CType(sender, Button)

            Dim frm As New Add


            Select Case btn.Name
                'Case Me.btnAddCategory.Name
                '    frm.Combo = AddItems.WhichCombo.Category
                '    If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                '        Me.lblCategory_Click(Nothing, Nothing)
                '    End If
                Case Me.btnAddType.Name
                    frm.Combo = Add.WhichCombo.Type
                    If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                        Me.lblType_Click(Nothing, Nothing)
                    End If
                Case Me.btnAddGender.Name
                    frm.Combo = Add.WhichCombo.Gender
                    If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                        Me.lblGender_Click(Nothing, Nothing)
                    End If
                Case Me.btnAddUnit.Name
                    frm.Combo = Add.WhichCombo.Unit
                    If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                        Me.lblUnit_Click(Nothing, Nothing)
                    End If
                Case Me.btnAddLPO.Name
                    If Me.cmbCompany.SelectedIndex = 0 Then
                        ShowValidationMessage("Select any company to add new Distributor/LPO")
                        Me.cmbCompany.Focus()
                        Exit Sub
                    End If
                    frm.Combo = Add.WhichCombo.LPO
                    frm.CompanyID = Me.cmbCompany.SelectedValue
                    If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                        If Me.cmbCompany.SelectedIndex > 0 Then
                            Me.cmbCompany_SelectedIndexChanged(Nothing, Nothing)
                        Else
                            Me.FillCombos(Me.cmbLPO.Name)
                        End If

                    End If
                Case Me.btnAddCompany.Name
                    frm.Combo = Add.WhichCombo.Company
                    If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                        Me.FillCombos(Me.cmbCompany.Name)
                    End If
                Case Me.btnAddBrand.Name
                    frm.Combo = Add.WhichCombo.Brand
                    If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                        Me.FillCombos(Me.cmbBrand.Name)
                    End If
            End Select

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub lblCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblCompany.Click
        Me.FillCombos(Me.cmbCompany.Name)
    End Sub

    Private Sub lblUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblUnit.Click
        Me.FillCombos(Me.cmbUnit.Name)
    End Sub

    Private Sub cmbCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompany.SelectedIndexChanged
        Try
            If Me.cmbCompany.SelectedIndex > 0 Then
                Dim strSQL As String = "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID,dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId,ArticleLpoDefTable.SubCategoryCode FROM         dbo.ArticleLpoDefTable INNER JOIN               dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId where ArticleCompanyDefTable.ArticleCompanyId = " & Me.cmbCompany.SelectedValue
                Me.cmbLPO.DisplayMember = "Name"
                Me.cmbLPO.ValueMember = "ID"
                Me.cmbLPO.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
            Else
                Me.FillCombos(Me.cmbLPO.Name)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub tabAriticalDetail_Selecting(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles tabAriticalDetail.Selecting
        If BtnSave.Text = "&Save" And e.TabPageIndex = 1 Then
            e.Cancel = True
        ElseIf BtnSave.Text = "&Save" And e.TabPage Is Me.TabPgCostSheet Then
            e.Cancel = True
        ElseIf BtnSave.Text = "&Save" And e.TabPage Is Me.TabPgVendorItems Then
            e.Cancel = True
            'ElseIf BtnSave.Text = "&Save" And e.TabPage Is Me.TabPgOpeningStock Then
            '    e.Cancel = True
        ElseIf BtnSave.Text = "&Save" And e.TabPage Is Me.TabPackQuantity Then
            e.Cancel = True
            'ElseIf BtnSave.Text <> "&Save" And e.TabPage Is Me.TabPgOpeningStock Then
            '    Try
            '        FillCombos(Me.cmbSupplier.Name)
            '        GetItemDetailByMasterId(Me.CurrentId)
            '    Catch ex As Exception
            '        ShowErrorMessage(ex.Message)
            '    End Try
        ElseIf BtnSave.Text <> "&Save" And e.TabPage Is Me.TabPgCostSheet Then
            Try
                Me.MakeCostSheetTable(Me.grdAllRecords.GetRow.Cells(0).Value)
                '' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
                Me.cmbCategorys.DataSource = New ArticleDAL().GetAllCategorys()
                Me.cmbCategorys.DisplayMember = "Category"
                Me.cmbCategorys.ValueMember = "Category"
                If Not Me.cmbCategorys.SelectedIndex = -1 Then Me.cmbCategorys.SelectedIndex = 0
                '''''''''''''''''''''''''''''''''''''
                'TASKM176151 Filled Remarks Combobox Here ...
                FillDropDown(Me.cmbRemarks, "Select Distinct Remarks From tblCostSheet ORder By Remarks", False)



            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
        End If
    End Sub

    Private Sub grdAriticals_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdAriticals.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

    End Sub
    ''ReqId-925 grd Article Update Data
    Private Sub grdAriticals_LinkClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdAriticals.LinkClicked
        Try
            Dim dr As DataRow

            If Me.grdAriticals.RecordCount = 0 Then
                Exit Sub
            End If

            If Me.grdAriticals.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                Exit Sub
            End If

            If Not Me.grdAriticals.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then Exit Sub

            ''ReqId-925 Update Data
            grdAriticals.UpdateData()
            'End RI 925

            If e.Column.Key = "Delete" Then
                Dim strSQL As String

                strSQL = "SELECT * FROM SalesDetailTable Where ArticleDefID = " & Val(Me.grdAriticals.GetRow.Cells(EnumArticalGrid.ArticleID).Value)

                dr = UtilityDAL.ReturnDataRow(strSQL)
                If Not dr Is Nothing Then
                    msg_Error(str_ErrorDependentRecordFound)
                    Exit Sub
                End If

                strSQL = "SELECT * FROM ReceivingDetailTable Where ArticleDefID = " & Val(Me.grdAriticals.GetRow.Cells(EnumArticalGrid.ArticleID).Value)

                dr = UtilityDAL.ReturnDataRow(strSQL)
                If Not dr Is Nothing Then
                    msg_Error(str_ErrorDependentRecordFound)
                    Exit Sub
                End If

                If ShowConfirmationMessage(gstrMsgDelete, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                    strSQL = "DELETE FROM ArticleDefTable Where ArticleID = " & Val(Me.grdAriticals.GetRow.Cells(EnumArticalGrid.ArticleID).Value)
                    UtilityDAL.ExecuteQuery(strSQL)
                    Me.grdAllRecords_SelectionChanged(sender, EventArgs.Empty)
                End If
            ElseIf e.Column.Key = "Update" Then
                Dim strQuery As String = String.Empty
                Dim ArticleCode As String = GetArticleCodeMaster(Val(Me.grdAriticals.GetRow.Cells(EnumArticalGrid.ArticleID).Value))
                Dim dtArticleColorCode As DataTable = GetArticleColorCode(Val(Me.grdAriticals.GetRow.Cells(EnumArticalGrid.Color).Value))
                Dim dtArticleSizeCode As DataTable = GetArticleSizeCode(Val(Me.grdAriticals.GetRow.Cells(EnumArticalGrid.Size).Value))
                ArticleCode = ArticleCode & "-" & IIf(dtArticleSizeCode.Rows(0).Item(1).ToString = "", dtArticleSizeCode.Rows(0).Item(0), dtArticleSizeCode.Rows(0).Item(1)) & "-" & IIf(dtArticleColorCode.Rows(0).Item(1).ToString = "", dtArticleColorCode.Rows(0).Item(0), dtArticleColorCode.Rows(0).Item(1))

                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdAriticals.GetRows
                    If r.Cells(EnumArticalGrid.ArticleCode).Value = ArticleCode Then
                        If Me.grdAriticals.RowCount > 1 Then
                            msg_Error("Sorry record can't be updated because record already exist")
                            Me.grdAriticals.Focus()
                            Exit Sub
                        End If
                    End If
                Next
                If ShowConfirmationMessage(gstrMsgUpdate, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                    strQuery = "Update ArticleDefTable SET ArticleColorId=" & Me.grdAriticals.GetRow.Cells(EnumArticalGrid.Color).Value & ", SizeRangeId=" & Me.grdAriticals.GetRow.Cells(EnumArticalGrid.Size).Value & ", ArticleCode='" & ArticleCode.Replace("'", "''") & "', Active=" & IIf(Me.grdAriticals.GetRow.Cells(EnumArticalGrid.Active).Value = True, 1, 0) & "  Where ArticleID = " & Val(Me.grdAriticals.GetRow.Cells(EnumArticalGrid.ArticleID).Value)
                    UtilityDAL.ExecuteQuery(strQuery)
                    Me.grdAllRecords_SelectionChanged(sender, EventArgs.Empty)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdoCode.CheckedChanged
        Try
            Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString

        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdoName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoName.CheckedChanged
        Try

            Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label12.Click
        Try
            Me.FillCombos(Me.cmbItem.Name)

            Me.cmbCategorys.DataSource = New ArticleDAL().GetAllCategorys()
            Me.cmbCategorys.DisplayMember = "Category"
            Me.cmbCategorys.ValueMember = "Category"
            If Not Me.cmbCategorys.SelectedIndex = -1 Then Me.cmbCategorys.SelectedIndex = 0
            '''''''''''''''''''''''''''''''''''''
            'TASKM176151 Filled Remarks Combobox Here ...
            FillDropDown(Me.cmbRemarks, "Select Distinct Remarks From tblCostSheet ORder By Remarks", False)


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddCostSheet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCostSheet.Click
        Try
            '' Request No 871
            '' 11-18-2013 by Imran Ali
            '' Cost Sheet Batch Wise for City Bread
            Dim qty As Double = 0D
            If cmbItem.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select item")
                Me.cmbItem.Focus()
                Exit Sub
            ElseIf Me.txtCostSheet.Text.Trim <> String.Empty Then
                Double.TryParse(Me.txtCostSheet.Text, qty)
                If qty = 0 Then
                    ShowValidationMessage("Invalid Qty")
                    Me.txtCostSheet.Focus()
                    Exit Sub
                End If
            End If
            Dim dtGrid As DataTable = CType(Me.grdCostSheet.DataSource, DataTable)
            'check if record is already exists in the grid
            Dim drFound() As DataRow = dtGrid.Select("ArticleId = " & cmbItem.ActiveRow.Cells(0).Value & " AND Category='" & Me.cmbCategorys.Text.Replace("'", "''") & "'")
            If drFound.Length > 0 Then
                drFound(0)(EnumGridCostSheet.Qty) += Val(Me.txtCostSheet.Text)
            Else
                Dim dr As DataRow = dtGrid.NewRow
                dr(EnumGridCostSheet.AritcleID) = cmbItem.ActiveRow.Cells(0).Value
                dr(EnumGridCostSheet.Code) = cmbItem.ActiveRow.Cells("Code").Value.ToString
                dr(EnumGridCostSheet.Description) = cmbItem.ActiveRow.Cells("Item").Value.ToString
                'Before against request no. 962
                'dr(EnumGridCostSheet.Color) = Me.cmbItem.ActiveRow.Cells("Color").Value.ToString
                dr(EnumGridCostSheet.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value.ToString 'ReqID-962 Change Field Name
                dr(EnumGridCostSheet.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value.ToString
                dr(EnumGridCostSheet.Category) = Me.cmbCategorys.Text   '' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
                dr(EnumGridCostSheet.PurchasePrice) = Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value
                dr(EnumGridCostSheet.SalePrice) = Me.cmbItem.ActiveRow.Cells("Price").Value
                dr(EnumGridCostSheet.Qty) = Val(qty)
                dr(EnumGridCostSheet.MasterArticleID) = CurrentId
                dr(EnumGridCostSheet.ArticleSize) = Me.cmbCostSheetUnit.Text '' Set Value Loose/Pack
                dr(EnumGridCostSheet.Tax) = Val(Me.txtTaxPercent.Text)
                dr(EnumGridCostSheet.Remarks) = Me.cmbRemarks.Text
                dtGrid.Rows.Add(dr)
                dtGrid.AcceptChanges()
            End If
            'Me.grdCostSheet.Refetch()
            'If Not Me.cmbCategorys.SelectedIndex = -1 Then Me.cmbCategorys.SelectedIndex = 0 '' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
            'TaskM176151 Clear Controls
            Me.txtCostSheet.Text = String.Empty
            Me.txtTaxPercent.Text = String.Empty
            'End TaskM176151

            Me.cmbItem.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub


    Private Sub MakeCostSheetTable(ByVal MasterArticleID As Integer)
        Try
            '' Request No 871
            '' 11-18-2013 by Imran Ali
            '' Cost Sheet Batch Wise for City Bread

            'Before ReqId-968
            '' Add New column of ArticleSize in this query
            'Dim strsql As String = " SELECT  ArticleDefTable.ArticleId, ArticleDefTable.ArticleCode AS [Article code], ArticleDefTable.ArticleDescription AS [Article Description], " _
            '              & " ArticleColorDefTable.ArticleColorName as Color , ArticleSizeDefTable.ArticleSizeName as Size , ISNULL(tblCostSheet.ArticleSize,'Loose') as ArticleSize,  ArticleDefTable.PurchasePrice AS [Purchase Price], ArticleDefTable.SalePrice AS [Sale Price], ISNULL(tblCostSheet.Qty,0) as Qty, tblCostSheet.MasterArticleID " _
            '              & " FROM tblCostSheet INNER JOIN  " _
            '              & " ArticleDefTable ON tblCostSheet.ArticleID = ArticleDefTable.ArticleId INNER JOIN " _
            '              & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId INNER JOIN " _
            '              & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
            '              & " where MasterArticleID  = " & MasterArticleID

            ' ' After 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
            ' Add Column of Category
            'Dim strsql As String = " SELECT  ArticleDefTable.ArticleId, ArticleDefTable.ArticleCode AS [Article code], ArticleDefTable.ArticleDescription AS [Article Description], " _
            '              & " ArticleColorDefTable.ArticleColorName as Color , ArticleSizeDefTable.ArticleSizeName as Size ,ISNULL(tblCostSheet.Category,'')as Category, ISNULL(tblCostSheet.ArticleSize,'Loose') as ArticleSize,  ArticleDefTable.PurchasePrice AS [Purchase Price], ArticleDefTable.SalePrice AS [Sale Price], ISNULL(tblCostSheet.Qty,0) as Qty, tblCostSheet.MasterArticleID " _
            '              & " FROM tblCostSheet INNER JOIN  " _
            '              & " ArticleDefTable ON tblCostSheet.ArticleID = ArticleDefTable.ArticleId INNER JOIN " _
            '              & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId INNER JOIN " _
            '              & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
            '              & " where MasterArticleID  = " & MasterArticleID
            Dim strsql As String = " SELECT  ArticleDefTable.ArticleId, ArticleDefTable.ArticleCode AS [Article code], ArticleDefTable.ArticleDescription AS [Article Description], " _
                       & " ArticleColorDefTable.ArticleColorName as Color , ArticleSizeDefTable.ArticleSizeName as Size , ISNULL(tblCostSheet.ArticleSize,'Loose') as ArticleSize,  IsNull(ArticleDefTable.PurchasePrice,0.0) AS [Purchase Price], IsNull(ArticleDefTable.SalePrice,0.0) AS [Sale Price], IsNull(tblCostSheet.Tax_Percent,0) as Tax, ISNULL(tblCostSheet.Qty,0) as Qty, tblCostSheet.MasterArticleID,Convert(float,0) as [Total Purchase Value], Convert(float,0) as [Total Sale Value], Convert(float,0) as [Purchase Tax], Convert(float,0) as [Sale Tax], Convert(float,0) as [Net Purchase Value], Convert(float,0) as [Net Sales Value], ISNULL(tblCostSheet.Category,'') as Category,ISNULL(tblCostSheet.Remarks,'') as Remarks " _
                       & " FROM tblCostSheet INNER JOIN  " _
                       & " ArticleDefTable ON tblCostSheet.ArticleID = ArticleDefTable.ArticleId INNER JOIN " _
                       & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId INNER JOIN " _
                       & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
                       & " where MasterArticleID  = " & MasterArticleID

            Dim dtCostSheet As DataTable = GetDataTable(strsql)
            'TaskM176151 Before 
            'dtCostSheet.Columns.Add("Total Purchase Value", GetType(System.Double), "(isnull([Purchase Price],0) * IsNull(Qty,0))")
            'dtCostSheet.Columns.Add("Total Sale Value", GetType(System.Double), "(isnull([Sale Price],0) * IsNull(Qty,0))")
            'dtCostSheet.Columns.Add("Purchase Tax", GetType(System.Double), "(([Tax]/100)*(isnull([Purchase Price],0) * IsNull(Qty,0)))")
            'dtCostSheet.Columns.Add("Sale Tax", GetType(System.Double), "(([Tax]/100)*(isnull([Sale Price],0) * IsNull(Qty,0)))")
            'dtCostSheet.Columns.Add("Net Purchase Value", GetType(System.Double)).Expression = "[Total Purchase Value]+[Purchase Tax]"
            'dtCostSheet.Columns.Add("Net Sales Value", GetType(System.Double)).Expression = "[Total Sale Value]+[Sale Tax]"
            'TASKM176151
            dtCostSheet.Columns("Total Purchase Value").Expression = "(isnull([Purchase Price],0) * IsNull(Qty,0))"
            dtCostSheet.Columns("Total Sale Value").Expression = "(isnull([Sale Price],0) * IsNull(Qty,0))"
            dtCostSheet.Columns("Purchase Tax").Expression = "(([Tax]/100)*(isnull([Purchase Price],0) * IsNull(Qty,0)))"
            dtCostSheet.Columns("Sale Tax").Expression = "(([Tax]/100)*(isnull([Sale Price],0) * IsNull(Qty,0)))"
            dtCostSheet.Columns("Net Purchase Value").Expression = "[Total Purchase Value]+[Purchase Tax]"
            dtCostSheet.Columns("Net Sales Value").Expression = "[Total Sale Value]+[Sale Tax]"
            dtCostSheet.Columns.Add("Delete", GetType(System.String))
            'End Task


            Me.grdCostSheet.DataSource = dtCostSheet
            Me.grdCostSheet.RetrieveStructure()

            Me.grdCostSheet.RootTable.Columns(0).Visible = False
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.MasterArticleID).Visible = False
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalPurchaseValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalSaleValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalPurchaseTaxAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalSaleTaxAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.NetPurchase).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.NetSales).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Qty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalPurchaseValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.PurchasePrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalSaleValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.SalePrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.NetPurchase).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.NetSales).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalPurchaseTaxAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalSaleTaxAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Tax).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Tax).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.NetPurchase).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.NetSales).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalPurchaseTaxAmount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalSaleTaxAmount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Qty).FormatString = String.Empty

            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Delete).Caption = "Action"
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Delete).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Delete).ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Delete).ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Delete).ButtonText = "Delete"

            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdCostSheet.RootTable.Columns
                col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            Next
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Tax).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Qty).EditType = Janus.Windows.GridEX.EditType.TextBox
            'TASKM176151 Set Editable Category And Remarks Columns
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Category).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Remarks).EditType = Janus.Windows.GridEX.EditType.TextBox
            'End TaskM176151
            Me.grdCostSheet.AutoSizeColumns()
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub BindingNavigatorMoveFirstItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BindingNavigatorMoveFirstItem.Click, BindingNavigatorMoveLastItem.Click, BindingNavigatorMoveNextItem.Click, BindingNavigatorMovePreviousItem.Click

    '    Try
    '        Me.MakeCostSheetTable(Me.CurrentId)

    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub grdCostSheet_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdCostSheet.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                Me.grdCostSheet.CurrentRow.Delete()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdItemLocation_LinkClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItemLocation.LinkClicked
        Try

            Dim dr As DataRow
            Dim str As String

            str = ""

            If Me.grdItemLocation.RecordCount = 0 Then
                Exit Sub
            End If
            str = "SELECT * FROM SalesDetailTable Where ArticleDefID = " & Val(Me.grdItemLocation.GetRow.Cells(EnumArticalLocation.ArticleId).Value)

            dr = UtilityDAL.ReturnDataRow(str)
            If Not dr Is Nothing Then
                msg_Error(str_ErrorDependentRecordFound)
                Exit Sub
            End If

            str = "SELECT * FROM ReceivingDetailTable Where ArticleDefID = " & Val(Me.grdItemLocation.GetRow.Cells(EnumArticalLocation.ArticleId).Value)
            dr = UtilityDAL.ReturnDataRow(str)
            If Not dr Is Nothing Then
                msg_Error(str_ErrorDependentRecordFound)
                Exit Sub
            End If

            If ShowConfirmationMessage(gstrMsgDelete, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                str = "DELETE FROM ArticalDefLocation Where ArticalID = " & Val(Me.grdItemLocation.GetRow.Cells(EnumArticalLocation.ArticleId).Value) & "  AND LocationId=" & Val(Me.grdItemLocation.GetRow.Cells(EnumArticalLocation.LocationID).Value) & ""

                UtilityDAL.ExecuteQuery(str)

                'Me.grdAllRecords_SelectionChanged(sender, EventArgs.Empty)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblvendorslist.Click

    End Sub
    Private Sub grdVendorItems_LinkClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdVendorItems.LinkClicked
        Try
            ''''''''''''''''' 

            Me.grdVendorItems.GetRow.Delete()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdVendorItems_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdVendorItems.ColumnButtonClick
        '''''''''''''''''
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub MakeVendorsItem(ByVal MasterItemId As Integer)
        Try
            Dim str As String = String.Empty

            'str = "SELECT ArticleDefVendors.VendorId, vwCOADetail.detail_title, ArticleDefVendors.ArticleDefId, ArticleDefTable.ArticleDescription FROM ArticleDefTable INNER JOIN ArticleDefVendors ON ArticleDefTable.ArticleId = ArticleDefVendors.ArticleDefId INNER JOIN vwCOADetail ON ArticleDefVendors.VendorId = vwCOADetail.coa_detail_id WHERE ArticleDefVendors.ArticleDefId= " & MasterItemId & ""

            str = "select DISTINCT ArticleDefVendors.ArticleDefVendorId,ArticleDefVendors.VendorId, vwcoadetail.detail_title as Vendor,'Delete' as [Delete] from vwcoadetail, ArticleDefVendors where vwcoadetail.coa_detail_id=ArticleDefVendors.VendorId and vwcoadetail.account_type='Vendor' And ArticleDefId=" & CurrentId

            Dim dtVendorsItem As DataTable = GetDataTable(str)
            Me.grdVendorItems.DataSource = dtVendorsItem
            'Me.grdVendorItems.RetrieveStructure()
            'grdVendorItems.RootTable.Columns(0).Visible = False
            'grdVendorItems.RootTable.Columns(2).Visible = False
            'Me.grdVendorItems.RootTable.Columns.Add("Delete", Janus.Windows.GridEX.ColumnType.Link)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub MakeCustomersItem(ByVal MasterItemId As Integer)
        Try
            Dim str As String = String.Empty
            'str = "SELECT ArticleDefVendors.VendorId, vwCOADetail.detail_title, ArticleDefVendors.ArticleDefId, ArticleDefTable.ArticleDescription FROM ArticleDefTable INNER JOIN ArticleDefVendors ON ArticleDefTable.ArticleId = ArticleDefVendors.ArticleDefId INNER JOIN vwCOADetail ON ArticleDefVendors.VendorId = vwCOADetail.coa_detail_id WHERE ArticleDefVendors.ArticleDefId= " & MasterItemId & ""
            str = "select DISTINCT ArticleDefCustomers.ArticleDefCustomerId,ArticleDefCustomers.CustomerId, vwcoadetail.detail_title as Customer,'Delete' as [Delete] from vwcoadetail, ArticleDefCustomers where vwcoadetail.coa_detail_id=ArticleDefCustomers.CustomerId and vwcoadetail.account_type='Customer' And ArticleDefId=" & CurrentId
            Dim dtVendorsItem As DataTable = GetDataTable(str)
            Me.grdCustomerList.DataSource = dtVendorsItem
            'Me.grdVendorItems.RetrieveStructure()
            'grdVendorItems.RootTable.Columns(0).Visible = False
            'grdVendorItems.RootTable.Columns(2).Visible = False
            'Me.grdVendorItems.RootTable.Columns.Add("Delete", Janus.Windows.GridEX.ColumnType.Link)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If Me.cmbvendorslist.SelectedIndex = 0 Then
                ShowErrorMessage("Please Select Vendor")
                Exit Sub
            End If
            If Me.grdVendorItems.RowCount < 0 Then Exit Sub
            Dim dtGridVendorItem As DataTable = CType(Me.grdVendorItems.DataSource, DataTable)
            dtGridVendorItem.TableName = "Vendors"
            dtGridVendorItem.AcceptChanges()

            If GetFilterDataFromDataTable(dtGridVendorItem, "VendorId=" & Me.cmbvendorslist.SelectedValue).Count > 0 Then
                ShowErrorMessage("Vendor already exist in the list")
                Exit Sub
            End If

            Dim dr As DataRow
            dr = dtGridVendorItem.NewRow
            dr.Item(0) = 0 'Me.cmbvendorslist.SelectedValue
            dr.Item(1) = Me.cmbvendorslist.SelectedValue
            dr.Item(2) = Me.cmbvendorslist.Text
            dr.Item(3) = "Delete"
            dtGridVendorItem.Rows.InsertAt(dr, 0)
            Me.cmbvendorslist.SelectedIndex = 0
            Me.cmbvendorslist.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally

            Me.cmbvendorslist.Focus()

        End Try
    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Dim strSQL As String = "Select coa_detail_id, detail_title From vwCOADetail WHERE account_type='Vendor'"
            Me.cmbvendorslist.DisplayMember = "detail_title"
            Me.cmbvendorslist.ValueMember = "coa_detail_id"
            Me.cmbvendorslist.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
            Me.cmbvendorslist.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If AddInventoryDept.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                Dim id As Integer = 0
                id = Me.cmbCategory.SelectedValue
                Me.lblCategory_Click(lblCategory, Nothing)
                Me.cmbCategory.SelectedValue = id
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub GetItemDetailByMasterId(ByVal MasterId As Integer)
    '    Try

    '        Dim str As String = String.Empty
    '        str = "SELECT ISNULL(Detail.Location_Id, 0) AS Location_Id, dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, " _
    '                 & " ISNULL(Detail.ArticleSize, 'Loose') AS ArticleSize, ISNULL(Detail.Qty, 0) AS Qty, ISNULL(dbo.ArticleDefView.PurchasePrice, 0) AS Price,  " _
    '                 & " ISNULL(dbo.ArticleDefView.PackQty, 0) AS PackQty " _
    '                 & " FROM dbo.ArticleDefView LEFT OUTER JOIN " _
    '                 & " (SELECT dbo.ReceivingMasterTable.LocationId AS Location_Id, dbo.ReceivingDetailTable.ArticleDefId AS ArticleId, 'Loose' AS ArticleSize,  " _
    '                 & " dbo.ReceivingDetailTable.Qty " _
    '                 & " FROM dbo.ReceivingMasterTable INNER JOIN " _
    '                 & " dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId " _
    '                 & " WHERE (dbo.ReceivingMasterTable.ReceivingNo LIKE '%Opening%')) Detail ON Detail.ArticleId = dbo.ArticleDefView.ArticleId WHERE ArticleDefView.MasterID=" & MasterId
    '        Dim dtOpeningStock As DataTable = GetDataTable(str)
    '        dtOpeningStock.Columns.Add("Amount", GetType(System.Double))
    '        dtOpeningStock.Columns("Amount").Expression = "IIF(ArticleSize='Pack', ((PackQty*Qty)*Price), (Qty*Price))"
    '        Me.grdOpeningStock.DataSource = Nothing
    '        Me.grdOpeningStock.DataSource = dtOpeningStock

    '        str = "Select Location_Id, Location_Code From tblDefLocation UNION SELECT 0, 'Select any value'"
    '        Dim dtLocation As DataTable = GetDataTable(str)
    '        Me.grdOpeningStock.RootTable.Columns("Location_Id").ValueList.PopulateValueList(dtLocation.DefaultView, "Location_Id", "Location_Code")

    '        Dim dtPacking As New DataTable
    '        dtPacking.Columns.Add("ArticleSize", GetType(System.String))
    '        Dim drPacking As DataRow
    '        drPacking = dtPacking.NewRow
    '        drPacking.Item(0) = "Loose"
    '        dtPacking.Rows.Add(drPacking)
    '        drPacking = dtPacking.NewRow
    '        drPacking.Item(0) = "Pack"
    '        dtPacking.Rows.Add(drPacking)
    '        Me.grdOpeningStock.RootTable.Columns("ArticleSize").ValueList.PopulateValueList(dtPacking.DefaultView, "ArticleSize", "ArticleSize")

    '        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdOpeningStock.RootTable.Columns
    '            If Not col.Caption = "Location" AndAlso Not col.Caption = "Qty" AndAlso Not col.Caption = "Price" AndAlso Not col.Caption = "Packing" Then
    '                col.EditType = Janus.Windows.GridEX.EditType.NoEdit
    '            End If
    '        Next
    '        'Me.grdOpeningStock.AutoSizeColumns()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Private Sub btnSaveOpeningStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        'If Me.cmbSupplier.SelectedIndex = 0 Then
    '        '    ShowErrorMessage("Please select supplier")
    '        '    Me.cmbSupplier.Focus()
    '        '    Exit Sub
    '        'End If

    '        Dim OpeningStock As OpeningStockMaster
    '        Dim OpeningStockDetail As OpeningStockDetail
    '        OpeningStock = New OpeningStockMaster

    '        OpeningStock.Document = "OpeningStock"
    '        OpeningStock.DocTypeId = 1
    '        OpeningStock.DcDate = Me.dtpDcDate.Value.Date
    '        OpeningStock.Supplier = Me.cmbSupplier.SelectedValue
    '        OpeningStock.NetAmount = Me.grdOpeningStock.GetTotal(Me.grdOpeningStock.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
    '        OpeningStock.TotalQty = Me.grdOpeningStock.GetTotal(Me.grdOpeningStock.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)
    '        OpeningStock.UserName = LoginUserName
    '        OpeningStock.StockAccountId = Convert.ToInt32(getConfigValueByType("PurchaseDebitAccount").ToString)
    '        OpeningStock.OpeningStockDetail = New List(Of OpeningStockDetail)

    '        For Each grdrow As Janus.Windows.GridEX.GridEXRow In Me.grdOpeningStock.GetRows
    '            OpeningStockDetail = New OpeningStockDetail
    '            OpeningStockDetail.LocationId = grdrow.Cells("Location_Id").Value
    '            OpeningStockDetail.ArticleId = grdrow.Cells("ArticleId").Value
    '            OpeningStockDetail.ArticleSize = grdrow.Cells("ArticleSize").Value.ToString
    '            OpeningStockDetail.Qty = IIf(grdrow.Cells("ArticleSize").Value = "Loose", Val(grdrow.Cells("Qty").Value), (Val(grdrow.Cells("Qty").Value) * Val(grdrow.Cells("PackQty").Value)))
    '            OpeningStockDetail.Price = grdrow.Cells("Price").Value
    '            OpeningStock.OpeningStockDetail.Add(OpeningStockDetail)
    '        Next

    '        If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
    '        Call New SBDal.ArticleDAL().AddOpeningStock(OpeningStock)
    '        msg_Information(str_informSave)
    '        GetItemDetailByMasterId(Me.CurrentId)
    '        Me.cmbSupplier.SelectedIndex = 0

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub AddNewVendorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewVendorToolStripMenuItem.Click
        Try
            FrmAddCustomers.FormType = "Vendor"
            ApplyStyleSheet(FrmAddCustomers)
            FrmAddCustomers.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 
        _str_Path = String.Empty  'Task#05082015

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Try
            Dim id As Integer = 0

            id = Me.cmbCategory.SelectedValue
            FillCombos(Me.cmbCategory.Name)
            Me.cmbCategory.SelectedValue = id

            id = Me.cmbType.SelectedValue
            FillCombos(Me.cmbType.Name)
            Me.cmbType.SelectedValue = id

            id = Me.cmbGender.SelectedValue
            FillCombos(Me.cmbGender.Name)
            Me.cmbGender.SelectedValue = id

            id = Me.cmbCompany.SelectedValue
            FillCombos(Me.cmbCompany.Name)
            Me.cmbCompany.SelectedValue = id

            id = Me.cmbLPO.Value
            FillCombos(Me.cmbLPO.Name)
            Me.cmbLPO.Value = id

            id = Me.cmbUnit.SelectedValue
            FillCombos(Me.cmbUnit.Name)
            Me.cmbUnit.SelectedValue = id

            'id = Me.cmbSupplier.SelectedValue
            'FillCombos(Me.cmbSupplier.Name)
            'Me.cmbSupplier.SelectedValue = id

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            If Not getConfigValueByType("chkAutoArticleCode").ToString = "Error" Then
                blnAutoArticleCode = getConfigValueByType("chkAutoArticleCode")
            End If


            ' FillCombos()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                '' 21-Dec-2013 ReqId-958 M Ijaz Javed   Add security rights of price update against cost sheet
                Me.btnPriceUpdate.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False
                Me.btnPriceUpdate.Enabled = False   '' 21-Dec-2013 ReqId-958 M Ijaz Javed   Add security rights of price update against cost sheet

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        '' 21-Dec-2013 ReqId-958 M Ijaz Javed   Add security rights of price update against cost sheet
                    ElseIf Rights.Item(i).FormControlName = "Price Update" Then
                        Me.btnPriceUpdate.Enabled = True
                        ''''''''''''''''''''''''''''''''
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lnkUploadPic_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkUploadPic.LinkClicked
        Try
            _str_Path = String.Empty    'Task#05082015 Ahmad Sharif
            If Not IO.Directory.Exists(_ArticlePicPath) Then
                IO.Directory.CreateDirectory(_ArticlePicPath)
                'ShowErrorMessage("Folder not exist")
                'Me.lnkUploadPic.Focus()
                'Exit Sub
            End If
            OpenFileDialog.Filter = "Image File |*.*"
            If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.PictureBox1.ImageLocation = OpenFileDialog.FileName ' Image.FromStream(fs, False, False)
                _str_Path = _ArticlePicPath & "\" & OpenFileDialog.FileName.Replace(OpenFileDialog.FileName, Me.uitxtItemCode.Text & ".jpg")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub uitxtItemCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles uitxtItemCode.KeyDown, uitxtItemName.KeyDown
        Try
            If e.KeyCode = Keys.OemOpenBrackets Or e.KeyCode = Keys.OemCloseBrackets Then
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub uitxtItemCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles uitxtItemCode.KeyPress
        Try
            If Me.PictureBox1.Image Is Nothing Then Exit Sub
            _str_Path = str_ApplicationStartUpPath & "\ArticlePictures\" & OpenFileDialog.FileName.Replace(OpenFileDialog.FileName, Me.uitxtItemCode.Text & ".Jpg")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnHistoryCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistoryCancel.Click
        Try
            Me.grdAllRecords_SelectionChanged(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdAllRecords.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdAllRecords.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grdAllRecords.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtTax.CheckedChanged, rbtFlatRate.CheckedChanged
        Try
            If rbtTax.Checked = True Then
                Me.txtFlatRate.ReadOnly = True
            ElseIf rbtFlatRate.Checked = True Then
                Me.txtFlatRate.ReadOnly = False
            Else
                Me.txtFlatRate.ReadOnly = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If Me.ComboBox1.SelectedIndex = 0 Then
                ShowErrorMessage("Please Select Customer")
                Exit Sub
            End If
            If Me.grdCustomerList.RowCount < 0 Then Exit Sub
            Dim dtGridVendorItem As DataTable = CType(Me.grdCustomerList.DataSource, DataTable)
            dtGridVendorItem.TableName = "Vendors"
            dtGridVendorItem.AcceptChanges()
            If GetFilterDataFromDataTable(dtGridVendorItem, "CustomerId=" & Me.ComboBox1.SelectedValue).Count > 0 Then
                ShowErrorMessage("Vendor already exist in the list")
                Exit Sub
            End If
            Dim dr As DataRow
            dr = dtGridVendorItem.NewRow
            dr.Item(0) = 0 'Me.cmbvendorslist.SelectedValue
            dr.Item(1) = Me.ComboBox1.SelectedValue
            dr.Item(2) = Me.ComboBox1.Text
            dr.Item(3) = "Delete"
            dtGridVendorItem.Rows.InsertAt(dr, 0)
            Me.ComboBox1.SelectedIndex = 0
            Me.ComboBox1.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.ComboBox1.Focus()
        End Try
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try

            Dim qty As Double = 0D
            If cmbItem.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select item")
                Me.cmbItem.Focus()
                Exit Sub
                'ElseIf Me.txtCostSheet.Text.Trim <> String.Empty Then
                '    Double.TryParse(Me.txtCostSheet.Text, qty)
                '    If qty = 0 Then
                '        ShowValidationMessage("Invalid Qty")
                '        Me.txtCostSheet.Focus()
                '        Exit Sub
                '    End If
            End If
            Dim dtGrid As DataTable = CType(Me.grdCostSheet.DataSource, DataTable)
            'check if record is already exists in the grid
            Dim drFound() As DataRow = dtGrid.Select("ArticleId = " & cmbItem.ActiveRow.Cells(0).Value)


            Dim strQuery As String = String.Empty
            'strQuery = "Select tblCostSheet.MasterArticleId From tblCostSheet WHERE MasterArticleId=" & Me.cmbItem.Value & ""
            strQuery = "Select tblCostSheet.MasterArticleId From tblCostSheet WHERE MasterArticleId=" & Me.cmbItem.ActiveRow.Cells("MasterId").Value.ToString & ""
            Dim dtMasterArticle As New DataTable
            dtMasterArticle = GetDataTable(strQuery)
            Dim dt As New DataTable
            If dtMasterArticle IsNot Nothing Then
                If dtMasterArticle.Rows.Count > 0 Then
                    'Before against task:2411
                    'strQuery = "Select a.ArticleId, b.ArticleCode as Code, b.ArticleDescription as Item, b.PurchasePrice, b.SalePrice as Price, a.Qty,Isnull(a.ArticleSize,'Loose') as ArticleSize,ArticleColorName as Color, ArticleSizeName as Size From, Category tblCostSheet a INNER JOIN ArticleDefView b on a.ArticleId = b.ArticleId WHERE a.MasterArticleId=" & dtMasterArticle.Rows(0).Item(0)
                    'Task:2411 Change Query
                    'strQuery = "Select a.ArticleId, b.ArticleCode as Code, b.ArticleDescription as Item, b.PurchasePrice, b.SalePrice as Price, a.Qty,Isnull(a.ArticleSize,'Loose') as ArticleSize,ArticleColorName as Color, ArticleSizeName as Size, Category, IsNull(Tax_Percent,0) as Tax From tblCostSheet a INNER JOIN ArticleDefView b on a.ArticleId = b.ArticleId WHERE a.MasterArticleId=" & dtMasterArticle.Rows(0).Item(0)
                    strQuery = "Select a.ArticleId, b.ArticleCode as Code, b.ArticleDescription as Item, b.PurchasePrice, b.SalePrice as Price, a.Qty,Isnull(a.ArticleSize,'Loose') as ArticleSize,ArticleColorName as Color, ArticleSizeName as Size, IsNull(Tax_Percent,0) as Tax, Category, a.Remarks From tblCostSheet a INNER JOIN ArticleDefView b on a.ArticleId = b.ArticleId WHERE a.MasterArticleId=" & dtMasterArticle.Rows(0).Item(0)
                    dt = GetDataTable(strQuery)
                    dt.AcceptChanges()
                End If
            End If

            For Each row As DataRow In dt.Rows
                If drFound.Length > 0 Then
                    drFound(0)(EnumGridCostSheet.Qty) += 1
                Else
                    Dim dr As DataRow = dtGrid.NewRow
                    dr(EnumGridCostSheet.AritcleID) = row("ArticleId") ' ArticleId
                    dr(EnumGridCostSheet.Code) = row("Code") 'Article Code
                    dr(EnumGridCostSheet.Description) = row("Item") 'Article Name 
                    dr(EnumGridCostSheet.Color) = row("Color").ToString
                    dr(EnumGridCostSheet.Size) = row("Size").ToString
                    dr(EnumGridCostSheet.PurchasePrice) = row("PurchasePrice") 'Purchase Price
                    dr(EnumGridCostSheet.SalePrice) = row("Price") 'Article Sale Price 
                    dr(EnumGridCostSheet.Qty) = Val(row("Qty").ToString) 'Qty
                    dr(EnumGridCostSheet.MasterArticleID) = CurrentId
                    dr(EnumGridCostSheet.ArticleSize) = row("ArticleSize")
                    dr(EnumGridCostSheet.Tax) = Val(row("Tax").ToString)
                    'TaskM176151 Add Column Category And Remarks in Grid
                    dr(EnumGridCostSheet.Category) = row("Category").ToString
                    dr(EnumGridCostSheet.Remarks) = row("Remarks").ToString
                    'End TaskM176151
                    dtGrid.Rows.Add(dr)
                End If
            Next
            'Me.grdCostSheet.Refetch()
            Me.cmbItem.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetArticleSizeCode(ByVal ArticleSizeID As Integer) As DataTable
        Try
            Dim strQuery As String = "Select ArticleSizeId, SizeCode From ArticleSizeDefTable WHERE ArticleSizeId=" & ArticleSizeID
            Return GetDataTable(strQuery)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetArticleColorCode(ByVal ArticleColorID As Integer) As DataTable
        Try
            Dim strQuery As String = "Select ArticleColorId, ColorCode From ArticleColorDefTable WHERE ArticleColorId=" & ArticleColorID
            Return GetDataTable(strQuery)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetArticleCodeMaster(ByVal ArticleId As Integer) As String
        Try
            Dim str As String = "Select MasterId From ArticleDefTable WHERE ArticleId=" & ArticleId & ""
            Dim dt As New DataTable
            dt = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Dim str1 As String = "Select ArticleCode From ArticleDefTableMaster WHERE ArticleId=" & dt.Rows(0).Item(0) & ""
                    Dim dtCode As New DataTable
                    dtCode = GetDataTable(str1)
                    If dtCode IsNot Nothing Then
                        If dtCode.Rows.Count > 0 Then
                            Return dtCode.Rows(0).Item(0)
                        Else
                            Return String.Empty
                        End If
                    Else
                        Return String.Empty
                    End If
                Else
                    Return String.Empty
                End If
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.ButtonClick

    End Sub
    Private Sub lnkAddSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkAddSize.Click
        Try
            Dim frm As New Add
            frm.Combo = Add.WhichCombo.Size
            If frm.ShowDialog = Windows.Forms.DialogResult.Yes Then
                Me.lblSize_Click(Nothing, Nothing)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkAddCombination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkAddCombination.Click
        Try
            Dim frm As New Add
            frm.Combo = Add.WhichCombo.Color
            If frm.ShowDialog = Windows.Forms.DialogResult.Yes Then
                Me.lblCombinition_Click(Nothing, Nothing)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddPackQty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPackQty.Click
        Try
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con

            If objCon.State = ConnectionState.Open Then objCon.Close()

            objCon.Open()
            objCommand.Connection = objCon

            Dim trans As OleDbTransaction = objCon.BeginTransaction
            Try
                If Not Vallidate() = True Then Exit Sub
                Dim id As Integer
                id = Me.grdAllRecords.GetRow.Cells("ArticleId").Value
                If Me.btnAddPackQty.Text = "&Save" Or Me.btnAddPackQty.Text = "Save" Then
                    objCommand.CommandType = CommandType.Text
                    objCommand.CommandText = "Insert into ArticleDefPackTable (ArticleMasterId,PackName,PackQty) values ( " & id & ", '" & Me.txtPackName.Text.Replace("'", "''") & "', " & Val(Me.txtQuantity.Text) & ")"
                Else
                    objCommand.CommandType = CommandType.Text
                    objCommand.CommandText = "UPDATE  ArticleDefPackTable SET ArticleMasterId=" & id & ",PackName='" & Me.txtPackName.Text.Replace("'", "''") & "',PackQty=" & Val(Me.txtQuantity.Text) & " WHERE ArticleMasterId=" & id & " AND ArticlePackId=" & Me.grdPackQty.GetRow.Cells("ArticlePackId").Value & ""
                End If
                trans.Commit()
                objCommand.ExecuteNonQuery()
                'str_informSave
                GetAllRecords("packQty")
                ResetValues()
            Catch ex As Exception
                trans.Rollback()
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdPackQty_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdPackQty.ColumnButtonClick
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        objCon = Con

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon
        Dim trans As OleDbTransaction = objCon.BeginTransaction

        Try
            If e.Column.Key = "Delete" Then
                If Me.grdPackQty.RowCount = 0 Then Exit Sub
                Dim id As Integer
                id = Me.grdPackQty.CurrentRow.Cells("ArticleMasterId").Value
                objCommand.CommandType = CommandType.Text
                objCommand.CommandText = "Delete from ArticleDefPackTable where ArticleMasterId = " & id & " AND ArticlePackId=" & Me.grdPackQty.GetRow.Cells("ArticlePackId").Value & ""
                trans.Commit()
                objCommand.ExecuteNonQuery()
                Me.grdPackQty.CurrentRow.Delete()
                GetAllRecords("packQty")
            End If
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Sub

    Private Function Vallidate() As Boolean
        Try
            If Me.txtPackName.Text = String.Empty Then
                ShowErrorMessage("Please enter pack name")
                Me.txtPackName.Focus()
                Return False
                Exit Function
            End If

            If Me.txtQuantity.Text = String.Empty Then
                ShowErrorMessage("Please enter quantity")
                Me.txtQuantity.Focus()
                Return False
                Exit Function
            End If

            Return True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Private Sub ResetValues()
        Try
            Me.txtPackName.Text = String.Empty
            Me.txtQuantity.Text = String.Empty
            Me.btnAddPackQty.Text = "&Save"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdPackQty_RowDoubleClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdPackQty.RowDoubleClick
        Try
            If Me.grdPackQty.RowCount = 0 Then Exit Sub
            Dim id As Integer
            id = Me.grdPackQty.GetRow.Cells("ArticleMasterId").Value
            'Dim dt1 As DataTable
            'dt1 = New ArticleDAL().GetPackQty(id)
            Me.txtPackName.Text = grdPackQty.GetRow.Cells("PackName").Value.ToString 'dt1.Rows(0).Item("PackName").ToString
            Me.txtQuantity.Text = Val(grdPackQty.GetRow.Cells("PackQty").Value.ToString) 'dt1.Rows(0).Item("PackQty").ToString
            btnAddPackQty.Text = "&Update"
            Me.txtPackName.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Me.txtPackName.Text = String.Empty
            Me.txtQuantity.Text = String.Empty
            Me.btnAddPackQty.Text = "&Save"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Price_Update() As Boolean
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDbTransaction = Con.BeginTransaction
        Try
            'Before against task:2431
            'Dim strSQL As String = "SELECT   dbo.ArticleDefView.ArticleId, Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SalePrice,0) as SalePrice, dbo.ArticleDefView.MasterID, ROUND(ISNULL(CostSheet.Price, 0), 3) AS Price " _
            '               & " FROM  dbo.ArticleDefView LEFT OUTER JOIN (SELECT     dbo.tblCostSheet.MasterArticleID, ISNULL(SUM(ISNULL(dbo.tblCostSheet.Qty, 0) * ISNULL(Vw.SalePrice, 0)), 0) AS Price " _
            '               & " FROM  dbo.tblCostSheet INNER JOIN dbo.ArticleDefView AS Vw ON Vw.ArticleId = dbo.tblCostSheet.ArticleID " _
            '               & " GROUP BY dbo.tblCostSheet.MasterArticleID) AS CostSheet ON dbo.ArticleDefView.MasterID = CostSheet.MasterArticleID " _
            '               & " WHERE (dbo.ArticleDefView.MasterID IN (SELECT  DISTINCT   MasterArticleID FROM dbo.tblCostSheet AS CS)) "
            'Task:2431 Added Cost Price Column
            Dim strSQL As String = "SELECT   dbo.ArticleDefView.ArticleId, Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SalePrice,0) as SalePrice, dbo.ArticleDefView.MasterID, ROUND(ISNULL(CostSheet.Price, 0), 3) AS Price, ROUND(ISNULL(CostSheet.CostPrice, 0), 3) as CostPrice " _
                          & " FROM  dbo.ArticleDefView LEFT OUTER JOIN (SELECT     dbo.tblCostSheet.MasterArticleID, ISNULL(SUM(ISNULL(dbo.tblCostSheet.Qty, 0) * ISNULL(Vw.SalePrice, 0)), 0) AS Price, ISNULL(SUM(ISNULL(dbo.tblCostSheet.Qty, 0) * ISNULL(Vw.PurchasePrice, 0)), 0) as CostPrice " _
                          & " FROM  dbo.tblCostSheet INNER JOIN dbo.ArticleDefView AS Vw ON Vw.ArticleId = dbo.tblCostSheet.ArticleID " _
                          & " GROUP BY dbo.tblCostSheet.MasterArticleID) AS CostSheet ON dbo.ArticleDefView.MasterID = CostSheet.MasterArticleID " _
                          & " WHERE (dbo.ArticleDefView.MasterID IN (SELECT  DISTINCT   MasterArticleID FROM dbo.tblCostSheet AS CS)) "
            'End Task:2431
            Dim dtData As New DataTable
            dtData = GetDataTable(strSQL, trans)
            Dim Cmd As New OleDbCommand
            Cmd.Connection = Con
            Cmd.Transaction = trans
            Cmd.CommandType = CommandType.Text
            If dtData IsNot Nothing Then

                For Each r As DataRow In dtData.Rows
                    strSQL = String.Empty
                    'Before against task:2431
                    'strSQL = "UPDATE ArticleDefTableMaster SET SalePrice=" & Val(r.Item("Price").ToString) & " WHERE ArticleId=" & Val(r.Item("MasterId").ToString) & ""
                    If Me.PriceUpdateButton.Name = btnCostPriceUpdate.Name Then
                        'Task:2431 Update Cost Price 
                        strSQL = "UPDATE ArticleDefTableMaster SET PurchasePrice=" & Val(r.Item("CostPrice").ToString) & "  WHERE ArticleId=" & Val(r.Item("MasterId").ToString) & ""
                        'End Task:2431
                        Cmd.CommandText = strSQL
                        Cmd.ExecuteNonQuery()

                        strSQL = String.Empty
                        'Before against task:2431
                        'strSQL = "UPDATE ArticleDefTable SET SalePrice=" & Val(r.Item("Price").ToString) & " WHERE ArticleId=" & Val(r.Item("ArticleId").ToString) & ""
                        'Task:2431 Update Cost Price 
                        strSQL = "UPDATE ArticleDefTable SET PurchasePrice=" & Val(r.Item("CostPrice").ToString) & "  WHERE ArticleId=" & Val(r.Item("ArticleId").ToString) & ""
                        'End Task:2431
                        Cmd.CommandText = strSQL
                        Cmd.ExecuteNonQuery()

                    ElseIf Me.PriceUpdateButton.Name = Me.btnPriceUpdate.Name Then

                        'Task:2431 Update Cost Price 
                        strSQL = "UPDATE ArticleDefTableMaster SET SalePrice=" & Val(r.Item("Price").ToString) & " WHERE ArticleId=" & Val(r.Item("MasterId").ToString) & ""
                        'End Task:2431
                        Cmd.CommandText = strSQL
                        Cmd.ExecuteNonQuery()

                        strSQL = String.Empty
                        'Before against task:2431
                        'strSQL = "UPDATE ArticleDefTable SET SalePrice=" & Val(r.Item("Price").ToString) & " WHERE ArticleId=" & Val(r.Item("ArticleId").ToString) & ""
                        'Task:2431 Update Cost Price 
                        strSQL = "UPDATE ArticleDefTable SET SalePrice=" & Val(r.Item("Price").ToString) & "  WHERE ArticleId=" & Val(r.Item("ArticleId").ToString) & ""
                        'End Task:2431
                        Cmd.CommandText = strSQL
                        Cmd.ExecuteNonQuery()


                    End If
                    strSQL = String.Empty
                    'Before against task:2431
                    'strSQL = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice, SaleNewPrice)" _
                    '& " VALUES('" & Now.ToString("yyyy-M-d hh:mm:ss tt") & "', " & r.Item("ArticleId") & ", " & 0 & ", " & r.Item("PurchasePrice") & ", " & 0 & ", " & r.Item("SalePrice") & ", " & r.Item("Price") & ")"
                    'Task:2431 Insert Cost Price
                    strSQL = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice, SaleNewPrice)" _
                    & " VALUES('" & Now.ToString("yyyy-M-d hh:mm:ss tt") & "', " & r.Item("ArticleId") & ", " & 0 & ", " & r.Item("PurchasePrice") & ", " & IIf(PriceUpdateButton.Name = "btnCostPriceUpdate", Val(r.Item("CostPrice").ToString), 0) & ", " & r.Item("SalePrice") & ", " & r.Item("Price") & ")"
                    'End Task:2431
                    Cmd.CommandText = strSQL
                    Cmd.ExecuteNonQuery()
                Next
                trans.Commit()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Private Sub btnPriceUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPriceUpdate.Click, btnCostPriceUpdate.Click
        Try
            PriceUpdateButton = CType(sender, ToolStripButton) 'Task:2440 Direct Cast Button
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Dim i As Integer = 0
            Do While BackgroundWorker1.IsBusy
                BackgroundWorker1.ReportProgress(i)
                i += 1
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            If Price_Update() = False Then
                Throw New Exception("Some data is not provided")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
    '    Try
    '        If e.ProgressPercentage < 101 Then ToolStripProgressBar1.Value = e.ProgressPercentage
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'Private Sub cmbItem_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
    '    Try
    '        'If Me.cmbItem.IsItemInList = False Then Exit Sub
    '        'FillCombos(Me.cmbCostSheetUnit.Name)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    'Task:2431 Update Price Completed Task
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            Me.GetAllRecords()
            msg_Information("Update Price Successfully.")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2431
    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged, cmbType.SelectedIndexChanged, cmbCompany.SelectedIndexChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If blnAutoArticleCode = False Then Exit Sub
            Dim cmb As ComboBox = CType(sender, ComboBox)

            If cmb.SelectedIndex > 0 Then
                Dim strPrefix As String = IIf(cmbCategory.SelectedIndex > 0, CType(Me.cmbCategory.SelectedItem, DataRowView).Row.Item("GroupCode").ToString, "") & "-" & IIf(cmbType.SelectedIndex > 0, CType(Me.cmbType.SelectedItem, DataRowView).Row.Item("TypeCode").ToString, "") & "-" & IIf(cmbCompany.SelectedIndex > 0, CType(Me.cmbCompany.SelectedItem, DataRowView).Row.Item("CategoryCode").ToString, "")
                Me.uitxtItemCode.Text = ArticleDAL.GetArticleCode(CStr(strPrefix) & "-")
            Else
                Me.uitxtItemCode.Text = String.Empty
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintCostSheetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintCostSheetToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grdAllRecords.RowCount = 0 Then Exit Sub
            ShowReport("rptCostSheetPrint", "{SP_CostSheetPrint;1.MasterArticleID}=" & Val(Me.grdAllRecords.GetRow.Cells(EnumGrid.ArticleId).Value.ToString) & "", "Nothing", "Nothing")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintCostSheetToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintCostSheetToolStripMenuItem1.Click
        Try
            PrintCostSheetToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintAllCostSheetToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PrintAllCostSheetToolStripMenuItem1.Click
        Try
            GetCrystalReportRights()
            ShowReport("rptCostSheetPrint")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintAllCostSheetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintAllCostSheetToolStripMenuItem.Click
        Try
            PrintAllCostSheetToolStripMenuItem1_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblModelList_Click(sender As Object, e As EventArgs) Handles lblModelList.Click
        Try
            Dim strIDs As String = Me.lstModelList.SelectedIDs
            Me.FillCombos(Me.lstModelList.Name)
            Me.lstModelList.SelectItemsByIDs(strIDs)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkAddModel_Click(sender As Object, e As EventArgs) Handles lnkAddModel.Click
        Try
            frmModelList.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub ComboBox1_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.DropDown
    '    Dim myCombo As ComboBox = CType(sender, ComboBox)
    '    Dim cbWidth As Integer = myCombo.DropDownWidth
    '    Dim drawGraphics As Graphics = myCombo.CreateGraphics
    '    Dim myFont As Font = myCombo.Font
    '    Dim longestItem As Integer
    '    For Each cbItems As String In CType(sender, ComboBox).Items
    '        longestItem = (CType(drawGraphics.MeasureString(cbItems, myFont).Width, Integer))
    '        If cbWidth < longestItem Then
    '            cbWidth = longestItem
    '        End If
    '    Next
    '    myCombo.DropDownWidth = cbWidth
    'End Sub

    'Private Sub cmbGender_Click(sender As Object, e As EventArgs) Handles cmbGender.Click
    '    ComboBox1_DropDown(Nothing, Nothing)
    'End Sub

    
End Class