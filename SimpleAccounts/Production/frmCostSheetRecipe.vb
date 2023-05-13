'2015-05-22 task# 20150513  Ali Ansari Removing Query Syntax Error
'2015-06-12 task# 2015060010  Delete button not working in costgrid 
'' 17-6-2015 TASKM176151 Imran Ali Add Field Of Remarks In Cost Sheet Define.
''27-6-2015 TASKM276151 Imran Ali Add Feild Of Category In Item List Andalso Set Editable Field Price 
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data.OleDb

Public Class frmCostSheetRecipe

    Enum EnumGridCostSheet

        AritcleID
        Code
        Description
        Color
        Size
        ArticleUnitName
        'Category    '' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
        ArticleSize
        PurchasePrice
        SalePrice
        Tax
        Qty
        MasterArticleID
        TotalPurchaseValue
        TotalSaleValue
        TotalPurchaseTax
        TotalSaleTax
        NetPurchase
        NetSale
        SubDepartmentID
        SubDepartment
        Category    '' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
        Remarks 'TASKM176151 Add Column Remarks
        Delete
    End Enum
    Dim flgCompanyRights As Boolean = False
    Dim PriceUpdateButton As ToolStripButton
    Dim IsOpenedForm As Boolean = False
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            If Condition = "Department" Then
                FillDropDown(Me.cmbDepartment, "Select ArticleGroupId, ArticleGroupName from ArticleGroupDefTable ORDER BY 2 ASC")
            ElseIf Condition = "Item" Then
                'TASKM276151 Add Field Category 
                Dim str As String = String.Empty
                str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleDefView.ArticleUnitName As Unit, ArticleColorName as Combination, ArticleCompanyName as Category, ArticleLPOName as [Sub Category], Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SalePrice,0) as Price, ArticleDefView.SizeRangeID as [Size ID], ArticleDefView.MasterId FROM ArticleDefView where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & ""
                'End Task
                If getConfigValueByType("ItemSortOrder").ToString = "True" Then
                    If getConfigValueByType("ItemAscending").ToString = "True" Then
                        str += " ORDER BY  ArticleDefView.SortOrder ASC"
                    ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                        str += " ORDER BY  ArticleDefView.SortOrder DESC"
                    End If
                ElseIf getConfigValueByType("ItemSortOrderByCode").ToString = "True" Then
                    If getConfigValueByType("ItemAscending").ToString = "True" Then
                        str += " ORDER BY  ArticleDefView.ArticleCode ASC"
                    ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                        str += " ORDER BY ArticleDefView.ArticleCode DESC"
                    End If
                ElseIf getConfigValueByType("ItemSortOrderByName").ToString = "True" Then
                    If getConfigValueByType("ItemAscending").ToString = "True" Then
                        str += " ORDER BY ArticleDefView.ArticleDescription ASC"
                    ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                        str += " ORDER BY ArticleDefView.ArticleDescription DESC"
                    End If
                End If

                FillUltraDropDown(Me.cmbItem, str)
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
            ElseIf Condition = "ParentItem" Then
                'TASKM276151 Add Field Category 
                Dim str As String = String.Empty
                str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleDefView.ArticleUnitName As Unit, ArticleColorName as Combination, ArticleCompanyName as Category, ArticleLPOName as [Sub Category], Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SalePrice,0) as Price, ArticleDefView.SizeRangeID as [Size ID], ArticleDefView.MasterId FROM ArticleDefView where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & ""
                If Me.grdCostSheet.RowCount > 0 Then
                    Me.cmbParentItem.DisplayMember = "Item"
                    Me.cmbParentItem.ValueMember = "Id"
                    Me.cmbParentItem.DataSource = Me.grdCostSheet.DataSource
                End If
                'For Each dr As DataRow In Me.grdCostSheet.
                'End Task
                'If getConfigValueByType("ItemSortOrder").ToString = "True" Then
                '    If getConfigValueByType("ItemAscending").ToString = "True" Then
                '        str += " ORDER BY  ArticleDefView.SortOrder ASC"
                '    ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                '        str += " ORDER BY  ArticleDefView.SortOrder DESC"
                '    End If
                'ElseIf getConfigValueByType("ItemSortOrderByCode").ToString = "True" Then
                '    If getConfigValueByType("ItemAscending").ToString = "True" Then
                '        str += " ORDER BY  ArticleDefView.ArticleCode ASC"
                '    ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                '        str += " ORDER BY ArticleDefView.ArticleCode DESC"
                '    End If
                'ElseIf getConfigValueByType("ItemSortOrderByName").ToString = "True" Then
                '    If getConfigValueByType("ItemAscending").ToString = "True" Then
                '        str += " ORDER BY ArticleDefView.ArticleDescription ASC"
                '    ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                '        str += " ORDER BY ArticleDefView.ArticleDescription DESC"
                '    End If
                'End If
                'FillUltraDropDown(Me.cmbParentItem, str)
                Me.cmbParentItem.Rows(0).Activate()
                Me.cmbParentItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbParentItem.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                Me.cmbParentItem.DisplayLayout.Bands(0).Columns("Price").Hidden = True
                Me.cmbParentItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Header.Caption = "Price"
                If rdoName.Checked = True Then
                    Me.cmbParentItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                Else
                    Me.cmbParentItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                End If
                ElseIf Condition = "Unit" Then
                    FillDropDown(Me.cmbCostSheetUnit, "Select PackName, PackName, PackQty From ArticleDefPackTable WHERE ArticleMasterId=" & IIf(Me.cmbItem.IsItemInList = False, 0, Val(Me.cmbItem.SelectedRow.Cells("MasterId").Value.ToString)) & " ORDER BY 1 ASC", False)
                ElseIf Condition = "Category" Then
                    Me.cmbCategorys.DataSource = New ArticleDAL().GetAllCategorys()
                    Me.cmbCategorys.DisplayMember = "Category"
                    Me.cmbCategorys.ValueMember = "Category"
                    If Not Me.cmbCategorys.SelectedIndex = -1 Then Me.cmbCategorys.SelectedIndex = 0
                ElseIf Condition = "Remarks" Then
                    FillDropDown(Me.cmbRemarks, "Select DISTINCT Remarks,Remarks From tblCostSheet WHERE Remarks <> '' Order By Remarks", False)
                ElseIf Condition = "MasterArticle" Then
                    'Dim strItem As String = "Select ArticleId as Id, ArticleDescription as Item, ArticleCode as Code, ArticleUnitDefTable.ArticleUnitName As Unit, Isnull(PackQty,0) as [Pack Qty], IsNull(PurchasePrice,0) as [Pur Price], IsNull(SalePrice,0) as [Sale Price] From ArticleDefTableMaster LEFT JOIN ArticleUnitDefTable ON ArticleDefTableMaster.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId " & IIf(Me.cmbDepartment.SelectedIndex > 0, " WHERE ArticleGroupId=" & Me.cmbDepartment.SelectedValue & "", "") & ""
                    Dim strItem As String = "Select ArticleId as Id, ArticleDescription as Item, ArticleCode as Code, ArticleUnitDefTable.ArticleUnitName As Unit, Isnull(PackQty,0) as [Pack Qty], IsNull(PurchasePrice,0) as [Pur Price], IsNull(SalePrice,0) as [Sale Price] From ArticleDefTable LEFT JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId " & IIf(Me.cmbDepartment.SelectedIndex > 0, " WHERE ArticleGroupId=" & Me.cmbDepartment.SelectedValue & "", "") & ""
                    If getConfigValueByType("ItemSortOrder").ToString = "True" Then
                        If getConfigValueByType("ItemAscending").ToString = "True" Then
                            strItem += " ORDER BY ArticleDefTableMaster.SortOrder ASC"
                        ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                            strItem += " ORDER BY  ArticleDefTableMaster.SortOrder DESC"
                        End If
                    ElseIf getConfigValueByType("ItemSortOrderByCode").ToString = "True" Then
                        If getConfigValueByType("ItemAscending").ToString = "True" Then
                            strItem += " ORDER BY  ArticleDefTableMaster.ArticleCode ASC"
                        ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                            strItem += " ORDER BY  ArticleDefTableMaster.ArticleCode DESC"
                        End If
                    ElseIf getConfigValueByType("ItemSortOrderByName").ToString = "True" Then
                        If getConfigValueByType("ItemAscending").ToString = "True" Then
                            strItem += " ORDER BY  ArticleDefTableMaster.ArticleDescription ASC"
                        ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                            strItem += " ORDER BY  ArticleDefTableMaster.ArticleDescription DESC"
                        End If
                    End If

                    FillUltraDropDown(Me.cmbMasterItem, strItem)
                    If Me.cmbMasterItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                        Me.cmbMasterItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                        Me.cmbMasterItem.Rows(0).Activate()
                    End If
                ElseIf Condition = "SubDepartment" Then
                    FillDropDown(Me.cmbSubDepartment, "Select ProdStep_Id, prod_step, prod_Less from tblProSteps ORDER BY 2 ASC")
                End If
        Catch ex As Exception
            Throw ex
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
            'TASKM176151 Before 
            'Dim strsql As String = " SELECT  ArticleDefTable.ArticleId, ArticleDefTable.ArticleCode AS [Article code], ArticleDefTable.ArticleDescription AS [Article Description], " _
            '                  & " ArticleColorDefTable.ArticleColorName as Color , ArticleSizeDefTable.ArticleSizeName as Size ,ISNULL(tblCostSheet.Category,'')as Category, ISNULL(tblCostSheet.ArticleSize,'Loose') as ArticleSize,  IsNull(ArticleDefTable.PurchasePrice,0.0) AS [Purchase Price], IsNull(ArticleDefTable.SalePrice,0.0) AS [Sale Price], IsNull(tblCostSheet.Tax_Percent,0) as Tax, ISNULL(tblCostSheet.Qty,0) as Qty, tblCostSheet.MasterArticleID " _
            '                  & " FROM tblCostSheet INNER JOIN  " _
            '                  & " ArticleDefTable ON tblCostSheet.ArticleID = ArticleDefTable.ArticleId INNER JOIN " _
            '                  & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId INNER JOIN " _
            '                  & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
            '                  & " where MasterArticleID  = " & MasterArticleID

            'TASKM176151 Added Column Remarks, Category
            Dim strsql As String = " SELECT  ArticleDefTable.ArticleId, ArticleDefTable.ArticleCode AS [Article code], ArticleDefTable.ArticleDescription AS [Article Description], " _
                       & " ArticleColorDefTable.ArticleColorName as Color , ArticleSizeDefTable.ArticleSizeName as Size , ArticleUnitDefTable.ArticleUnitName As Unit, ISNULL(tblCostSheet.ArticleSize,'Loose') as ArticleSize,  Case When IsNull(tblCostSheet.CostPrice,0)=0 Then IsNull(ArticleDefTable.PurchasePrice,0.0) Else IsNull(tblCostSheet.CostPrice,0.0) End AS [Purchase Price], IsNull(ArticleDefTable.SalePrice,0.0) AS [Sale Price], IsNull(tblCostSheet.Tax_Percent,0) as Tax, ISNULL(tblCostSheet.Qty,0) as Qty, tblCostSheet.MasterArticleID,Convert(float,0) as [Total Purchase Value], Convert(float,0) as [Total Sale Value], Convert(float,0) as [Purchase Tax], Convert(float,0) as [Sale Tax], Convert(float,0) as [Net Purchase Value], Convert(float,0) as [Net Sales Value], IsNull(tblCostSheet.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, ISNULL(tblCostSheet.Category,'') as Category, ISNULL(tblCostSheet.Remarks,'') as Remarks " _
                       & " FROM tblCostSheet INNER JOIN  " _
                       & " ArticleDefTable ON tblCostSheet.ArticleID = ArticleDefTable.ArticleId INNER JOIN " _
                       & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId INNER JOIN " _
                       & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId LEFT JOIN tblProSteps ON tblCostSheet.SubDepartmentID = tblProSteps.ProdStep_Id LEFT JOIN " _
                       & " ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId" _
                       & " where MasterArticleID  = " & MasterArticleID
            'End TASKM176151
            Dim dtCostSheet As DataTable = GetDataTable(strsql)
            'TASKM176151 Before
            'dtCostSheet.Columns.Add("Total Purchase Value", GetType(System.Double), "isnull([Purchase Price],0) * Qty")
            'dtCostSheet.Columns.Add("Total Sale Value", GetType(System.Double), "isnull([Sale Price],0) * Qty")
            'dtCostSheet.Columns.Add("Purchase Tax", GetType(System.Double), "(([Tax]/100)*(isnull([Purchase Price],0) * IsNull(Qty,0)))")
            'dtCostSheet.Columns.Add("Sale Tax", GetType(System.Double), "(([Tax]/100)*(isnull([Sale Price],0) * IsNull(Qty,0)))")
            'dtCostSheet.Columns.Add("Net Purchase Value", GetType(System.Double)).Expression = "[Total Purchase Value]+[Purchase Tax]"
            'dtCostSheet.Columns.Add("Net Sales Value", GetType(System.Double)).Expression = "[Total Sale Value]+[Sale Tax]"
            'TASKM176151 Set Expression
            dtCostSheet.Columns("Total Purchase Value").Expression = "(isnull([Purchase Price],0) * IsNull(Qty,0))"
            dtCostSheet.Columns("Total Sale Value").Expression = "(isnull([Sale Price],0) * IsNull(Qty,0))"
            dtCostSheet.Columns("Purchase Tax").Expression = "(([Tax]/100)*(isnull([Purchase Price],0) * IsNull(Qty,0)))"
            dtCostSheet.Columns("Sale Tax").Expression = "(([Tax]/100)*(isnull([Sale Price],0) * IsNull(Qty,0)))"
            dtCostSheet.Columns("Net Purchase Value").Expression = "[Total Purchase Value]+[Purchase Tax]"
            dtCostSheet.Columns("Net Sales Value").Expression = "[Total Sale Value]+[Sale Tax]"
            dtCostSheet.Columns.Add("Delete", GetType(System.String))
            'End TASKM176151

            Me.grdCostSheet.DataSource = dtCostSheet

            'Me.grdCostSheet.RetrieveStructure()

            Me.grdCostSheet.RootTable.Columns(0).Visible = False
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.MasterArticleID).Visible = False
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.SubDepartment).Visible = False
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalPurchaseValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalSaleValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Qty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.NetPurchase).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.NetSale).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalPurchaseTax).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalSaleTax).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.NetPurchase).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.NetSale).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalPurchaseTax).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalSaleTax).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Tax).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Tax).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.NetPurchase).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.NetSale).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalPurchaseTax).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalSaleTax).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalPurchaseValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.PurchasePrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalSaleValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.SalePrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Qty).FormatString = String.Empty


            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalPurchaseValue).FormatString = "N" & DecimalPointInValue
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalSaleValue).FormatString = "N" & DecimalPointInValue
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Qty).FormatString = "N" & DecimalPointInQty
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Qty).FormatString = "N" & DecimalPointInQty
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalPurchaseValue).TotalFormatString = "N" & DecimalPointInValue
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.TotalSaleValue).TotalFormatString = "N" & DecimalPointInValue
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Qty).TotalFormatString = "N" & DecimalPointInQty
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Qty).TotalFormatString = "N" & DecimalPointInQty

            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Delete).Caption = "Action"
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Delete).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Delete).ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Delete).ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Delete).ButtonText = "Delete"
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.SubDepartmentID).Caption = "Department"

            Dim SDepartment As New DataTable
            SDepartment = GetDataTable("Select ProdStep_Id, prod_step FROM tblproSteps")
            SDepartment.AcceptChanges()
            Me.grdCostSheet.RootTable.Columns("SubDepartmentID").HasValueList = True
            Me.grdCostSheet.RootTable.Columns("SubDepartmentID").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdCostSheet.RootTable.Columns("SubDepartmentID").ValueList.PopulateValueList(SDepartment.DefaultView, "ProdStep_Id", "prod_step")

            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdCostSheet.RootTable.Columns
                If col.Index <> 9 AndAlso col.Index <> 17 Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
                'col.FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Next
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Qty).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Tax).EditType = Janus.Windows.GridEX.EditType.TextBox
            'TASKM176151 Set Editable Category and Remarks Fields In Grid
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Category).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Remarks).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.PurchasePrice).EditType = Janus.Windows.GridEX.EditType.TextBox

            'Me.grdCostSheet.RootTable.HierarchicalMode = Janus.Windows.GridEX.HierarchicalMode.SelfReferencing
            'Me.grdCostSheet.RootTable.SelfReferencingSettings.AggregationMode = Janus.Windows.GridEX.SelfReferencingAggregationMode.AllRows
            'Me.grdCostSheet.RootTable.SelfReferencingSettings.AutoSizeColumnOnExpand = False
            'Me.grdCostSheet.RootTable.SelfReferencingSettings.ChildDataMember = "MasterArticleID"
            'Me.grdCostSheet.RootTable.SelfReferencingSettings.ChildListMember = "MasterArticleID"
            'Me.grdCostSheet.RootTable.SelfReferencingSettings.ExpandColumn = Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.Description)
            'Me.grdCostSheet.RootTable.SelfReferencingSettings.HierarchicalGroupMode = Janus.Windows.GridEX.HierarchicalGroupMode.AllRows
            'Me.grdCostSheet.RootTable.SelfReferencingSettings.ParentDataMember = "ArticleId"
            'Me.grdCostSheet.RootTable.SelfReferencingSettings.ParentRootMode = Janus.Windows.GridEX.ParentRootMode.UseParentRootValue
            'Me.grdCostSheet.RootTable.SelfReferencingSettings.ParentRootValue = vbNull
            'Me.grdCostSheet.RootTable.SelfReferencingSettings.TreatOrphanRowsAsRoot = True
            'Me.grdCostSheet.RootTable.Columns(EnumGridCostSheet.MasterArticleID)
            'End TASKM176151

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdCostSheet.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmCostSheet_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                Me.btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.F2 Then
                Me.btnEdit_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                If Me.grdCostSheet.RecordCount > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                    ResetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmCostSheet_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub frmCostSheet_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            FillCombos("Department")
            FillCombos("MasterArticle")
            FillCombos("Item")
            'FillCombos("Unit")
            FillCombos("Category")
            FillCombos("Remarks") 'TASKM176151 Call Remarks's Combobox
            FillCombos("SubDepartment")
            MakeCostSheetTable(Me.cmbMasterItem.Value)
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            IsOpenedForm = True
            ResetControls()
        Catch ex As Exception
            ShowReport(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            id = Me.cmbMasterItem.ActiveRow.Cells(0).Value
            FillCombos("MasterArticle")
            Me.cmbMasterItem.Value = id
            id = Me.cmbItem.ActiveRow.Cells(0).Value
            FillCombos("Item")
            Me.cmbItem.Value = id
            'id = Me.cmbCostSheetUnit.SelectedIndex
            FillCombos("Unit")
            'Me.cmbCostSheetUnit.SelectedIndex = id
            FillCombos("Category")


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbMasterItem_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMasterItem.Leave
        Try
            If Me.cmbMasterItem.ActiveRow Is Nothing Then Exit Sub
            MakeCostSheetTable(Me.cmbMasterItem.Value)
            Me.txtPackQty.Text = Val(Me.cmbMasterItem.ActiveRow.Cells("Pack Qty").Value.ToString)
            Me.txtPurchasePrice.Text = Val(Me.cmbMasterItem.ActiveRow.Cells("Pur Price").Value.ToString)
            Me.txtSalePrice.Text = Val(Me.cmbMasterItem.ActiveRow.Cells("Sale Price").Value.ToString)
            If Me.grdCostSheet.RowCount > 0 Then
                Me.btnPriceUpdate.Enabled = True
                Me.btnCostPriceUpdate.Enabled = True
            Else
                Me.btnPriceUpdate.Enabled = False
                Me.btnCostPriceUpdate.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddCostSheet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCostSheet.Click
        Try
            '' Request No 871
            '' 11-18-2013 by Imran Ali
            '' Cost Sheet Batch Wise for City Bread
            If Me.cmbMasterItem.ActiveRow Is Nothing Then Exit Sub
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

            Dim drFound() As DataRow = dtGrid.Select("ArticleId = " & cmbItem.ActiveRow.Cells(0).Value & " and Category='" & Me.cmbCategorys.Text.Replace("'", "''") & "'")
            Dim drMatched() As DataRow = dtGrid.Select("SubDepartmentID = " & IIf(Me.cmbSubDepartment.SelectedIndex <= 0, -1, Me.cmbSubDepartment.SelectedValue) & " And ArticleId = " & cmbItem.ActiveRow.Cells(0).Value & "")
            If drMatched.Length > 0 Then
                drMatched(0)(EnumGridCostSheet.Qty) += Val(Me.txtCostSheet.Text)
            ElseIf drFound.Length > 0 AndAlso drMatched.Length = 0 AndAlso Not Me.cmbCategorys.Text = "" Then
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
                dr(EnumGridCostSheet.ArticleUnitName) = Me.cmbItem.ActiveRow.Cells("Unit").Value.ToString
                dr(EnumGridCostSheet.Category) = Me.cmbCategorys.Text   '' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
                dr(EnumGridCostSheet.PurchasePrice) = Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value
                dr(EnumGridCostSheet.SalePrice) = Me.cmbItem.ActiveRow.Cells("Price").Value
                dr(EnumGridCostSheet.Qty) = Val(qty)
                dr(EnumGridCostSheet.MasterArticleID) = Me.cmbMasterItem.Value
                dr(EnumGridCostSheet.ArticleSize) = Me.cmbCostSheetUnit.Text '' Set Value Loose/Pack
                dr(EnumGridCostSheet.Tax) = Val(Me.txtTaxPercent.Text)
                dr(EnumGridCostSheet.Remarks) = Me.cmbRemarks.Text
                dr(EnumGridCostSheet.SubDepartmentID) = Me.cmbSubDepartment.SelectedValue
                dr(EnumGridCostSheet.SubDepartment) = Me.cmbSubDepartment.Text
                dtGrid.Rows.Add(dr)
                dtGrid.AcceptChanges()
                Me.CtrlGrdBar1_Load(Nothing, Nothing)
                '

            End If
            'Me.grdCostSheet.Refetch()
            'Task# 2015060010  to reset detail records 
            Call ClearDetailControls()
            'Task# 2015060010  to reset detail records 
            'If Not Me.cmbCategorys.SelectedIndex = -1 Then Me.cmbCategorys.SelectedIndex = 0 '' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
            Me.cmbItem.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Me.grdCostSheet.RowCount > 0 Then ShowErrorMessage("Record not in grid.") : Exit Sub
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            'If Me.grdCostSheet.RecordCount = 0 Then Exit Function
            Me.grdCostSheet.UpdateData()
            Dim dtGrid As DataTable = CType(Me.grdCostSheet.DataSource, DataTable)
            dtGrid.AcceptChanges()
            If dtGrid.Rows.Count > 0 Then
                Call New ArticleDAL().AddCostSheet(dtGrid)
                System.Threading.Thread.Sleep(1000)
                ResetControls()
                'ShowInformationMessage(gstrMsgAfterUpdate)
            Else
                Call New ArticleDAL().AddCostSheet(dtGrid, Me.cmbMasterItem.Value)
                'ShowInformationMessage(gstrMsgAfterUpdate)
                System.Threading.Thread.Sleep(1000)
                ResetControls()
            End If
            'Me.MakeCostSheetTable(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
    '    Try
    '        If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
    '        FillCombos("Unit")
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
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
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            If Price_Update() = False Then
                Throw New Exception("Some data is not provided")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnPriceUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPriceUpdate.Click, btnCostPriceUpdate.Click
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            If Me.grdCostSheet.RowCount <= 0 Then Exit Sub
            PriceUpdateButton = CType(sender, ToolStripButton) 'Task:2440 Direct Cast Button
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            'Dim i As Integer = 0
            Do While BackgroundWorker1.IsBusy
                ''BackgroundWorker1.ReportProgress(i)
                'i += 1
                Application.DoEvents()
            Loop
            Dim id As Integer = 0I
            id = Me.cmbMasterItem.ActiveRow.Cells(0).Value
            FillCombos("MasterArticle")
            Me.cmbMasterItem.Value = id
            System.Threading.Thread.Sleep(1000)
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ResetControls(Optional ByVal Condition As String = "")
        Try
            Me.btnSave.Text = "&Save"
            Me.cmbMasterItem.Rows(0).Activate()
            Me.txtPackQty.Text = String.Empty
            Me.txtPurchasePrice.Text = String.Empty
            Me.txtSalePrice.Text = String.Empty
            FillCombos("Category")
            FillCombos("Remarks")
            FillCombos("SubDepartment")
            Me.cmbItem.Rows(0).Activate()
            Me.txtCostSheet.Text = String.Empty
            Me.cmbMasterItem.Focus()
            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
            MakeCostSheetTable(-1)
            GetAllRecords()
            If Me.grdCostSheet.RowCount > 0 Then
                Me.btnPriceUpdate.Enabled = True
                Me.btnCostPriceUpdate.Enabled = True
            Else
                Me.btnPriceUpdate.Enabled = False
                Me.btnCostPriceUpdate.Enabled = False
            End If
            '2015050010'
            cmbCategorys.Text = String.Empty 'Reseting Category,Unit and Tax%  Task# 2015050010
            txtTaxPercent.Text = String.Empty 'Reseting Category,Unit and Tax%  Task# 2015050010
            'cmbCostSheetUnit.SelectedIndex = 0 'Reseting Category,Unit and Tax%  Task# 2015050010
            Me.cmbRemarks.Text = String.Empty
            GetSecurityRights()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmSalesReturn)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False

                For Each Rightsdt As GroupRights In Rights
                    If Rightsdt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rightsdt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "")
        Try
            Dim objDt As New DataTable
            objDt = GetDataTable("Select ArticleId as Id, ArticleDescription as Item, ArticleCode as Code, Isnull(PackQty,0) as [Pack Qty], IsNull(PurchasePrice,0) as [Pur Price], IsNull(SalePrice,0) as [Sale Price] From ArticleDefTableMaster WHERE ArticleId IN(Select DISTINCT MasterArticleId From tblCostSheet)  ORDER BY ArticleDescription ASC")
            Me.grdSaved.DataSource = objDt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns(0).Visible = False
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdSaved.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub EditRecord(Optional ByVal Condition As String = "")
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            Me.cmbMasterItem.Value = Val(Me.grdSaved.GetRow.Cells("Id").Value.ToString)
            Me.txtPackQty.Text = Val(Me.grdSaved.GetRow.Cells("Pack Qty").Value.ToString)
            Me.txtPurchasePrice.Text = Val(Me.grdSaved.GetRow.Cells("Pur Price").Value.ToString)
            Me.txtSalePrice.Text = Val(Me.grdSaved.GetRow.Cells("Sale Price").Value.ToString)
            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
            Me.MakeCostSheetTable(Me.cmbMasterItem.Value)
            If Me.grdCostSheet.RowCount > 0 Then
                Me.btnPriceUpdate.Enabled = True
                Me.btnCostPriceUpdate.Enabled = True
            Else
                Me.btnPriceUpdate.Enabled = False
                Me.btnCostPriceUpdate.Enabled = False
            End If

            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click
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
                    'Marked against task# 20150513  
                    'strQuery = "Select a.ArticleId, b.ArticleCode as Code, b.ArticleDescription as Item, b.PurchasePrice, b.SalePrice as Price, a.Qty,Isnull(a.ArticleSize,'Loose') as ArticleSize,ArticleColorName as Color, ArticleSizeName as Size, Category, IsNull(tblCostSheet.Tax_Percent,0) as Tax From tblCostSheet a INNER JOIN ArticleDefView b on a.ArticleId = b.ArticleId WHERE a.MasterArticleId=" & dtMasterArticle.Rows(0).Item(0)
                    'Marked against task# 20150513  
                    'ProdStep_Id, prod_step, prod_Less from tblProSteps 
                    'Altered  against task# 20150513  Ali Ansari Removing Query Syntax Error
                    strQuery = "Select a.ArticleId, b.ArticleCode as Code, b.ArticleDescription as Item, b.PurchasePrice, b.SalePrice as Price, a.Qty,Isnull(a.ArticleSize,'Loose') as ArticleSize,ArticleColorName as Color, ArticleSizeName as Size, IsNull(a.Tax_Percent,0) as Tax, Category,a.Remarks, IsNull(a.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, b.ArticleUnitName As Unit From tblCostSheet a INNER JOIN ArticleDefView b on a.ArticleId = b.ArticleId LEFT JOIN tblProSteps ON a.SubDepartmentID = tblProSteps.ProdStep_Id WHERE a.MasterArticleId=" & dtMasterArticle.Rows(0).Item(0)
                    'Altered  against task# 20150513  Ali Ansari Removing Query Syntax Error
                    dt = GetDataTable(strQuery)
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
                    dr(EnumGridCostSheet.ArticleUnitName) = row("Unit").ToString
                    dr(EnumGridCostSheet.PurchasePrice) = row("PurchasePrice") 'Purchase Price
                    dr(EnumGridCostSheet.SalePrice) = row("Price") 'Article Sale Price 
                    dr(EnumGridCostSheet.Qty) = Val(row("Qty").ToString) 'Qty
                    dr(EnumGridCostSheet.MasterArticleID) = Me.cmbMasterItem.Value
                    dr(EnumGridCostSheet.ArticleSize) = row("ArticleSize")
                    dr(EnumGridCostSheet.Tax) = Val(row("Tax").ToString)
                    'TASKM176151 Add Feild Category And Remarks In Grid
                    dr(EnumGridCostSheet.Category) = row("Category").ToString
                    dr(EnumGridCostSheet.Remarks) = row("Remarks").ToString
                    dr(EnumGridCostSheet.SubDepartmentID) = row("SubDepartmentID")
                    dr(EnumGridCostSheet.SubDepartment) = row("SubDepartment").ToString
                    'End TASKM176151
                    dtGrid.Rows.Add(dr)
                End If
            Next
            'Me.grdCostSheet.Refetch()
            Me.cmbItem.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdCostSheet_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdCostSheet.ColumnButtonClick
        '2015060010 delete button
        Try
            If e.Column.Key = "Delete" Then
                Me.grdCostSheet.CurrentRow.Delete()
                Me.grdCostSheet.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        '2015060010 delete button
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Not msg_Confirm(str_ConfirmDelete) Then Exit Sub
        If Not Me.grdSaved.RowCount > 0 Then ShowErrorMessage("Record not found.") : Exit Sub
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim objCmd As New OleDbCommand
        Try
            objCmd.Connection = Con
            objCmd.Transaction = objTrans
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "Delete From tblCostSheet WHERE MasterArticleId=" & Val(Me.grdSaved.GetRow.Cells("Id").Value.ToString) & ""
            objCmd.ExecuteNonQuery()
            objTrans.Commit()
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If Me.UltraTabControl2.SelectedTab.Index = 0 Then
                If Me.grdCostSheet.RowCount = 0 Then Me.btnDelete.Visible = False
                Me.btnSave.Visible = True
                Me.btnRefresh.Visible = True
                Me.CtrlGrdBar1.MyGrid = grdCostSheet
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdCostSheet.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdCostSheet.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    'Me.grd.SaveLayoutFile(fs)
                    Me.grdCostSheet.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
                If Me.cmbMasterItem.ActiveRow IsNot Nothing Then Me.CtrlGrdBar1.txtGridTitle.Text = "Cost Sheet Detail" & Chr(10) & "" & Me.cmbMasterItem.ActiveRow.Cells("Item").Value.ToString & ""
            Else
                Me.btnDelete.Visible = True
                Me.btnSave.Visible = False
                Me.btnRefresh.Visible = False
                Me.CtrlGrdBar1.MyGrid = grdSaved

                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    'Me.grd.SaveLayoutFile(fs)
                    Me.grdSaved.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
                'Me.CtrlGrdBar1.txtGridTitle.Text = "Cost Sheet Detail" & Chr(10) & "" & Me.cmbMasterItem.ActiveRow.Cells("Item").Value.ToString & ""
            End If
            'Me.CtrlGrdBar1.txtGridTitle.Text = "Cost Sheet Detail" & Chr(10) & "" & Me.cmbMasterItem.ActiveRow.Cells("Item").Value.ToString & ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl2_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl2.SelectedTabChanged
        Try
            If Me.grdSaved Is Nothing Then Exit Sub
            If Me.grdCostSheet Is Nothing Then Exit Sub
            If Me.cmbMasterItem.ActiveRow Is Nothing Then Exit Sub
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepartment.SelectedIndexChanged
        Try
            If Me.cmbDepartment.SelectedIndex = -1 Then Exit Sub
            If Me.cmbDepartment.SelectedIndex > 0 Then
                FillCombos("MasterArticle")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdoCode.CheckedChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.RdoCode.Checked = True Then Me.cmbItem.DisplayMember = "Code"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoName_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoName.CheckedChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.rdoName.Checked = True Then Me.cmbItem.DisplayMember = "Item"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RdoCode1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdoCode1.CheckedChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.RdoCode1.Checked = True Then Me.cmbMasterItem.DisplayMember = "Code"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RdoName1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdoName1.CheckedChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.RdoName1.Checked = True Then Me.cmbMasterItem.DisplayMember = "Item"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Reseting Detail COntrols Task# 2015060010
    Private Sub ClearDetailControls()
        txtCostSheet.Text = String.Empty
        txtTaxPercent.Text = String.Empty
        'cmbCategorys.Text = String.Empty
        cmbCostSheetUnit.Text = String.Empty
    End Sub
    'Reseting Detail COntrols Task# 2015060010

    Private Sub grdCostSheet_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCostSheet.DoubleClick
        'Try
        '    If Me.grdCostSheet.RowCount = 0 Then Exit Sub
        '    Row_Index = Me.grdCostSheet.CurrentRow.RowIndex 'Me.grd.GetRow.Cells("AccountId").Row.RowIndex
        '    Me.cmbCategorys.Text = Me.grdCostSheet.GetRow.Cells("Head").Text.ToString
        '    Me.cmbCostSheetUnit.Value = Me.grdCostSheet.GetRow.Cells("AccountId").Value
        '    Me.txtPackQty.Text = Me.grdCostSheet.GetRow.Cells("Description").Text.ToString
        '    Me.cmbCostSheetUnit.SelectedValue = Me.grdCostSheet.GetRow.Cells("CostCenterId").Value

        '    Dim rowCol As New Janus.Windows.GridEX.GridEXFormatStyle
        '    rowCol.BackColor = Color.AntiqueWhite
        '    Me.grdCostSheet.CurrentRow.RowStyle = rowCol

        '    Me.grdCostSheet.Enabled = False

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub btnCostSheetDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCostSheetDetail.Click
        Try
            'If Not frmMain.Panel2.Controls.Contains(frmGrdRptCostSheetMarginCalculationDetail) Then
            frmMain.LoadControl("frmGrdRptCostSheetMarginCalculationDetail")
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class