'2015-05-22 task# 20150513  Ali Ansari Removing Query Syntax Error
'2015-06-12 task# 2015060010  Delete button not working in costgrid 
'' 17-6-2015 TASKM176151 Imran Ali Add Field Of Remarks In Cost Sheet Define.
''27-6-2015 TASKM276151 Imran Ali Add Feild Of Category In Item List Andalso Set Editable Field Price 
'' TASK TFS3774 Muhammad Amin : Creation of link column of Standard No in Material Grid on 28-05-2018
'' TASK TFS3581 Muhammad Amin: Addition of PackQty field to form and made changes in other files required to implement it. Dated 20-06-2018
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data.OleDb
Imports Infragistics.Win.UltraWinTabControl
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win

Public Class frmFinishGoodStandard
    Enum EnumGridDetail
        StandardNo
        Id
        FinishGoodId
        SubDepartmentId
        SubDepartment
        MaterialArticleId
        ArticleCode
        ArticleDescription
        Color
        Size
        ArticleUnitName
        'Category    '' 25-Dec-2013 ReqId-968 M Ijaz Javed   Category Wise Cost Sheet
        ArticleSize
        DetailArticleId
        PackingId
        PurchasePrice
        SalePrice
        Tax
        PackQty
        Qty
        Percentage
        TotalQty
        TotalPurchaseValue
        TotalSaleValue
        PurchaseTax
        SaleTax
        NetPurchaseValue
        NetSalesValue
        Category
        Remarks
        StandardId
        Delete
    End Enum
    Dim flgCompanyRights As Boolean = False
    Dim PriceUpdateButton As ToolStripButton
    Dim IsOpenedForm As Boolean = False
    Dim AssociateItems As String = "True"
    Dim RowIndex As Integer = 0
    Dim ChildSerialNo As String = ""
    Dim SerialNo As String = ""
    Dim FinishGoodId As Integer = 0
    Dim FinishGood As BEFinishGood
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            If Condition = "Department" Then
                'FillDropDown(Me.cmbDepartment, "Select ArticleGroupId, ArticleGroupName from ArticleGroupDefTable ORDER BY 2 ASC")
            ElseIf Condition = "Item" Then
                'TASKM276151 Add Field Category 
                Dim str As String = String.Empty
                If getConfigValueByType("AvgRate").ToString = "False" Then
                    str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleDefView.ArticleUnitName As Unit, ArticleColorName as Combination, ArticleCompanyName as Category, ArticleLPOName as [Sub Category], Isnull(Cost_Price,0) as PurchasePrice, Isnull(SalePrice,0) as Price, ArticleDefView.SizeRangeID as [Size ID], ArticleDefView.MasterId, ISNULL(FinishGood.StandardNo, '') AS StandardNo, ISNULL(FinishGood.StandardId, '') AS StandardId FROM ArticleDefView LEFT OUTER JOIN (SELECT Price, ArticleDefId FROM ReceivingDetailTable WHERE ReceivingDetailId IN (SELECT Max(ReceivingDetailId) FROM ReceivingDetailTable GROUP BY ArticleDefId)) AS LastPurchasePrice ON ArticleDefView.ArticleId = LastPurchasePrice.ArticleDefId LEFT OUTER JOIN (SELECT StandardNo, MasterArticleId, Id AS StandardId FROM FinishGoodMaster WHERE ISNULL(Default1, 0) = 1) AS FinishGood ON ArticleDefView.MasterID = FinishGood.MasterArticleId where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & ""
                Else
                    str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleDefView.ArticleUnitName As Unit, ArticleColorName as Combination, ArticleCompanyName as Category, ArticleLPOName as [Sub Category], IsNull(LastPurchasePrice.Price, 0) as PurchasePrice, Isnull(SalePrice,0) as Price, ArticleDefView.SizeRangeID as [Size ID], ArticleDefView.MasterId, ISNULL(FinishGood.StandardNo, '') AS StandardNo, ISNULL(FinishGood.StandardId, '') AS StandardId FROM ArticleDefView LEFT OUTER JOIN (SELECT Price, ArticleDefId FROM ReceivingDetailTable WHERE ReceivingDetailId IN (SELECT Max(ReceivingDetailId) FROM ReceivingDetailTable GROUP BY ArticleDefId)) AS LastPurchasePrice ON ArticleDefView.ArticleId = LastPurchasePrice.ArticleDefId LEFT OUTER JOIN (SELECT StandardNo, MasterArticleId, Id AS StandardId FROM FinishGoodMaster WHERE ISNULL(Default1, 0) = 1) AS FinishGood ON ArticleDefView.MasterID = FinishGood.MasterArticleId where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & ""
                End If
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


                Me.cmbItem.DisplayLayout.Bands(0).Columns("StandardNo").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("StandardId").Hidden = True


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
                    'If Me.grdCostSheet.RowCount > 0 Then
                    '    Dim dt As DataTable = CType(Me.grdCostSheet.DataSource, DataTable)
                    '    Dim dtMerged1 As New DataTable
                    '    Dim dtMerged As New DataTable
                    '    dtMerged.Columns.Add("ArticleId")
                    '    dtMerged.Columns.Add("Article Code")
                    '    dtMerged.Columns.Add("Article Description")
                    '    dtMerged.Columns.Add("SerialNo")
                    '    For Each row As DataRow In dt.Rows
                    '        Dim dw As New DataView
                    '        Dim drArray() As DataRow = dtMerged.Select("ArticleId = " & row("ArticleId") & "")
                    '        If drArray.Length = 0 Then
                    '            Dim drMerged As DataRow
                    '            drMerged = dtMerged.NewRow
                    '            drMerged(0) = row.Item("ArticleId")
                    '            drMerged(1) = row.Item("Article code")
                    '            drMerged(2) = row.Item("Article Description")
                    '            drMerged(3) = row.Item("SerialNo")
                    '            dtMerged.Rows.Add(drMerged)
                    '        End If
                    '    Next
                    '    Dim dr As DataRow
                    '    dr = dtMerged.NewRow
                    '    dr(0) = Convert.ToInt32(0)
                    '    dr(1) = strZeroIndexItem
                    '    dr(2) = strZeroIndexItem
                    '    dtMerged.Rows.InsertAt(dr, 0)
                    '    dtMerged.AcceptChanges()
                    '    If Not Me.cmbParentItem.DataSource Is Nothing Then
                    '        Me.cmbParentItem.DataSource = Nothing
                    '    End If
                    '    Me.cmbParentItem.DisplayMember = "Article Description"
                    '    Me.cmbParentItem.ValueMember = "ArticleId"
                    '    Me.cmbParentItem.DataSource = dtMerged
                    '    dtMerged = Nothing
                    'Else
                    '    If Not Me.cmbParentItem.DataSource Is Nothing Then
                    '        Me.cmbParentItem.DataSource = Nothing
                    '    End If
                    'End If
            ElseIf Condition = "Unit" Then
                Me.cmbCostSheetUnit.ValueMember = "ArticlePackId"
                Me.cmbCostSheetUnit.DisplayMember = "PackName"
                Me.cmbCostSheetUnit.DataSource = GetPackData(Me.cmbItem.Value)
                'FillDropDown(Me.cmbCostSheetUnit, "Select PackName, PackName, PackQty From ArticleDefPackTable WHERE ArticleMasterId=" & IIf(Me.cmbItem.IsItemInList = False, 0, Val(Me.cmbItem.SelectedRow.Cells("MasterId").Value.ToString)) & " ORDER BY 1 ASC", False)
                ElseIf Condition = "Category" Then
                    Me.cmbCategorys.DataSource = New ArticleDAL().GetAllCategorys()
                    Me.cmbCategorys.DisplayMember = "Category"
                    Me.cmbCategorys.ValueMember = "Category"
                    If Not Me.cmbCategorys.SelectedIndex = -1 Then Me.cmbCategorys.SelectedIndex = 0
                ElseIf Condition = "Remarks" Then
                    FillDropDown(Me.cmbRemarks, "Select DISTINCT Remarks,Remarks From tblCostSheet WHERE Remarks <> '' Order By Remarks", False)
                ElseIf Condition = "MasterArticle" Then
                    'Dim strItem As String = "Select ArticleId as Id, ArticleDescription as Item, ArticleCode as Code, ArticleUnitDefTable.ArticleUnitName As Unit, Isnull(PackQty,0) as [Pack Qty], IsNull(PurchasePrice,0) as [Pur Price], IsNull(SalePrice,0) as [Sale Price], IsNull(ArticleDefTableMaster.ProductionProcessId, 0) AS ProductionProcessId From ArticleDefTableMaster LEFT JOIN ArticleUnitDefTable ON ArticleDefTableMaster.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId " & IIf(Me.cmbDepartment.SelectedIndex > 0, " WHERE ArticleGroupId=" & Me.cmbDepartment.SelectedValue & "", "") & ""
                    Dim strItem As String = "Select ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleUnitDefTable.ArticleUnitName As Unit, Isnull(PackQty,0) as [Pack Qty], IsNull(PurchasePrice,0) as [Pur Price], IsNull(SalePrice,0) as [Sale Price], IsNull(ArticleDefTableMaster.ProductionProcessId, 0) AS ProductionProcessId From ArticleDefTableMaster LEFT JOIN ArticleUnitDefTable ON ArticleDefTableMaster.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId "

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
                    FillDropDown(Me.cmbSubDepartment, "Select ProdStep_Id AS ProductionStepId, prod_step AS [Production Step], prod_Less from tblProSteps AS ProductionStep INNER JOIN ProductionProcessDetail AS ProcessDetail ON ProcessDetail.ProductionStepId = ProductionStep.ProdStep_Id WHERE ProcessDetail.ProductionProcessId =" & Me.cmbMasterItem.ActiveRow.Cells("ProductionProcessId").Value & "  ORDER BY ProcessDetail.SortOrder ASC")
                    'FillDropDown(Me.cmbSubDepartment, "Select ProdStep_Id, prod_step, prod_Less from tblProSteps ORDER BY 2 ASC")
                ElseIf Condition = "ProductionStep" Then
                    FillDropDown(Me.cmbProductionStep, "Select ProdStep_Id AS ProductionStepId, prod_step AS [Production Step], prod_Less from tblProSteps AS ProductionStep INNER JOIN ProductionProcessDetail AS ProcessDetail ON ProcessDetail.ProductionStepId = ProductionStep.ProdStep_Id WHERE ProcessDetail.ProductionProcessId =" & Me.cmbMasterItem.ActiveRow.Cells("ProductionProcessId").Value & "  ORDER BY ProcessDetail.SortOrder ASC")


                ElseIf Condition = "ProductionStepAccount" Then
                    'FillDropDown(Me.cmbAccount, "SELECT coa_detail_id, detail_title AS Account FROM vwCOADetail WHERE Active = 1 ")
                    FillDropDown(Me.cmbAccount, "SELECT coa_detail_id, detail_title AS Account FROM vwCOADetail WHERE Active = 1 and account_type = 'Expense'")



                ElseIf Condition = "ByProduct" Then
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

                    FillUltraDropDown(Me.cmbByProduct, str)
                    Me.cmbByProduct.Rows(0).Activate()
                    Me.cmbByProduct.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                    Me.cmbByProduct.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                    Me.cmbByProduct.DisplayLayout.Bands(0).Columns("Price").Hidden = True
                    Me.cmbByProduct.DisplayLayout.Bands(0).Columns("PurchasePrice").Header.Caption = "Price"
                    'If rboByProductCode.Checked = True Then
                    '    Me.cmbByProduct.DisplayMember = Me.cmbByProduct.Rows(0).Cells(2).Column.Key.ToString
                    'Else
                    '    Me.cmbByProduct.DisplayMember = Me.cmbByProduct.Rows(0).Cells(1).Column.Key.ToString
                    'End If
                ElseIf Condition = "Stages" Then
                    FillDropDown(Me.cmbStage, "Select ProdStep_Id AS ProductionStepId, prod_step AS [Production Step], prod_Less from tblProSteps AS ProductionStep INNER JOIN ProductionProcessDetail AS ProcessDetail ON ProcessDetail.ProductionStepId = ProductionStep.ProdStep_Id WHERE ProcessDetail.ProductionProcessId =" & Me.cmbMasterItem.ActiveRow.Cells("ProductionProcessId").Value & "  ORDER BY ProcessDetail.SortOrder ASC")
                ElseIf Condition = "LabourType" Then
                    FillUltraDropDown(Me.cmbLabourType, "Select tblLabourType.Id AS LabourTypeId, LabourType, ChargeType.Charge As ChargeType From tblLabourType LEFT OUTER JOIN ChargeType ON tblLabourType.Id = ChargeType.Id")
                    Me.cmbLabourType.Rows(0).Activate()
                    Me.cmbLabourType.DisplayLayout.Bands(0).Columns("LabourTypeId").Hidden = True

                ElseIf Condition = "grdDetailArticle" Then
                Dim dtDetailArticle As DataTable = GetDataTable("Select ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription +' ~ '+ ArticleSizeDefTable.ArticleSizeName +' ~ '+ ArticleColorDefTable.ArticleColorName AS [Article Description] From ArticleDefTable LEFT OUTER JOIN ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
                                                        & " LEFT OUTER JOIN ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId WHERE MasterId = " & Me.cmbMasterItem.Value & " Union Select 0 AS ArticleId, 'N/A' AS ArticleDescription ")
                ''Below line is commented against TASK TFS3998 to report to show Article' s Color and Size ON 26-07-2018
                'Dim dtDetailArticle As DataTable = GetDataTable("Select ArticleId, ArticleDescription From ArticleDefTable WHERE MasterId = " & Me.cmbMasterItem.Value & " Union Select 0 AS ArticleId, 'N/A' AS ArticleDescription ")
                dtDetailArticle.AcceptChanges()
                Me.grdDetail.RootTable.Columns("DetailArticleId").HasValueList = True
                Me.grdDetail.RootTable.Columns("DetailArticleId").EditType = Janus.Windows.GridEX.EditType.Combo
                Me.grdDetail.RootTable.Columns("DetailArticleId").ValueList.PopulateValueList(dtDetailArticle.DefaultView, "ArticleId", "Article Description")
            ElseIf Condition = "grdPacking" Then
                'ArticleDefPackTable (ArticleMasterId,PackName,PackQty,PackRate
                Dim dtPacking As DataTable = GetDataTable("Select ArticlePackId, PackName From ArticleDefPackTable WHERE ArticleMasterId = " & Me.cmbMasterItem.Value & " Union Select 0 AS ArticlePackId, 'N/A' AS PackName ")
                dtPacking.AcceptChanges()
                Me.grdDetail.RootTable.Columns("PackingId").HasValueList = True
                Me.grdDetail.RootTable.Columns("PackingId").EditType = Janus.Windows.GridEX.EditType.Combo
                Me.grdDetail.RootTable.Columns("PackingId").ValueList.PopulateValueList(dtPacking.DefaultView, "ArticlePackId", "PackName")
                End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetDetail(ByVal FinishGoodId As Integer)
        Try

            Me.grdDetail.DataSource = FinishGoodDAL.GetDetail(FinishGoodId)

            Me.grdDetail.RetrieveStructure()

            'Me.grdDetail.RootTable.Columns(0).Visible = False
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Id).Visible = False
            Me.grdDetail.RootTable.Columns(EnumGridDetail.FinishGoodId).Visible = False
            Me.grdDetail.RootTable.Columns(EnumGridDetail.MaterialArticleId).Visible = False
            Me.grdDetail.RootTable.Columns(EnumGridDetail.SubDepartment).Visible = False
            Me.grdDetail.RootTable.Columns(EnumGridDetail.StandardId).Visible = False

            Me.grdDetail.RootTable.Columns(EnumGridDetail.TotalPurchaseValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetail.RootTable.Columns(EnumGridDetail.TotalSaleValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Qty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetail.RootTable.Columns(EnumGridDetail.TotalQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PackQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdDetail.RootTable.Columns(EnumGridDetail.NetPurchaseValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetail.RootTable.Columns(EnumGridDetail.NetSalesValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PurchaseTax).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetail.RootTable.Columns(EnumGridDetail.SaleTax).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdDetail.RootTable.Columns(EnumGridDetail.NetPurchaseValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.NetSalesValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PurchaseTax).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.SaleTax).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Tax).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdDetail.RootTable.Columns(EnumGridDetail.Tax).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.NetPurchaseValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.NetSalesValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PurchaseTax).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.SaleTax).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdDetail.RootTable.Columns(EnumGridDetail.TotalPurchaseValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PurchasePrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.TotalSaleValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.SalePrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PackQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns(EnumGridDetail.TotalQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdDetail.RootTable.Columns(EnumGridDetail.Qty).FormatString = String.Empty


            Me.grdDetail.RootTable.Columns(EnumGridDetail.TotalPurchaseValue).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.TotalSaleValue).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Qty).FormatString = "N" & DecimalPointInQty
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PackQty).FormatString = "N" & DecimalPointInQty

            Me.grdDetail.RootTable.Columns(EnumGridDetail.Percentage).FormatString = "N" & DecimalPointInQty
            Me.grdDetail.RootTable.Columns(EnumGridDetail.TotalQty).FormatString = "N" & DecimalPointInQty
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PackQty).TotalFormatString = "N" & DecimalPointInQty

            Me.grdDetail.RootTable.Columns(EnumGridDetail.TotalPurchaseValue).TotalFormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.TotalSaleValue).TotalFormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Qty).TotalFormatString = "N" & DecimalPointInQty
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Qty).TotalFormatString = "N" & DecimalPointInQty
            Me.grdDetail.RootTable.Columns(EnumGridDetail.TotalQty).TotalFormatString = "N" & DecimalPointInQty


            '----------------'

            Me.grdDetail.RootTable.Columns(EnumGridDetail.NetPurchaseValue).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.NetSalesValue).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PurchaseTax).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.SaleTax).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Tax).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.NetPurchaseValue).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.NetSalesValue).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PurchaseTax).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.SaleTax).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PurchasePrice).FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns(EnumGridDetail.SalePrice).FormatString = "N" & DecimalPointInValue


            '--------------'


            Me.grdDetail.RootTable.Columns(EnumGridDetail.Delete).Caption = "Action"
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Delete).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Delete).ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Delete).ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Delete).ButtonText = "Delete"
            Me.grdDetail.RootTable.Columns(EnumGridDetail.SubDepartmentId).Caption = "Department"
            Me.grdDetail.RootTable.Columns(EnumGridDetail.DetailArticleId).Caption = "Detail Article"
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PackingId).Caption = "Packing"

            Dim SDepartment As New DataTable
            SDepartment = GetDataTable("Select ProdStep_Id, prod_step FROM tblproSteps")
            SDepartment.AcceptChanges()
            Me.grdDetail.RootTable.Columns("SubDepartmentId").HasValueList = True
            Me.grdDetail.RootTable.Columns("SubDepartmentId").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdDetail.RootTable.Columns("SubDepartmentId").ValueList.PopulateValueList(SDepartment.DefaultView, "ProdStep_Id", "prod_step")

            Dim dtDetailArticle As DataTable = GetDataTable("Select ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription +'~'+ ArticleSizeDefTable.ArticleSizeName +'~'+ ArticleColorDefTable.ArticleColorName AS [Article Description] From ArticleDefTable LEFT OUTER JOIN ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId " _
                                                            & " LEFT OUTER JOIN ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId WHERE MasterId = " & Me.cmbMasterItem.Value & " Union Select 0 AS ArticleId, 'N/A' AS ArticleDescription ")
            dtDetailArticle.AcceptChanges()
            Me.grdDetail.RootTable.Columns("DetailArticleId").HasValueList = True
            Me.grdDetail.RootTable.Columns("DetailArticleId").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdDetail.RootTable.Columns("DetailArticleId").ValueList.PopulateValueList(dtDetailArticle.DefaultView, "ArticleId", "Article Description")
            'ArticleDefPackTable (ArticleMasterId,PackName,PackQty,PackRate
            Dim dtPacking As DataTable = GetDataTable("Select ArticlePackId, PackName From ArticleDefPackTable WHERE ArticleMasterId = " & Me.cmbMasterItem.Value & " Union Select 0 AS ArticlePackId, 'N/A' AS PackName ")
            dtPacking.AcceptChanges()
            Me.grdDetail.RootTable.Columns("PackingId").HasValueList = True
            Me.grdDetail.RootTable.Columns("PackingId").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdDetail.RootTable.Columns("PackingId").ValueList.PopulateValueList(dtPacking.DefaultView, "ArticlePackId", "PackName")


            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdDetail.RootTable.Columns
                If col.Index <> EnumGridDetail.Remarks AndAlso col.Index <> EnumGridDetail.SubDepartmentId AndAlso col.Index <> EnumGridDetail.Qty AndAlso col.Index <> EnumGridDetail.Percentage AndAlso col.Index <> EnumGridDetail.DetailArticleId AndAlso col.Index <> EnumGridDetail.PackingId Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Qty).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Tax).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Category).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdDetail.RootTable.Columns(EnumGridDetail.Remarks).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdDetail.RootTable.Columns(EnumGridDetail.PurchasePrice).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdDetail.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True

            '' TASK TFS3447: Standard No Link
            Me.grdDetail.RootTable.Columns(EnumGridDetail.StandardNo).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdDetail.RootTable.Columns(EnumGridDetail.StandardNo).ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdDetail.RootTable.Columns(EnumGridDetail.StandardNo).Caption = "Standard No"
            '' END TASK TFS3447: Standard No Link

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdDetail.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmFinishGoodStandard_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                Me.btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.F2 Then
                Me.btnEdit_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                If Me.grdDetail.RecordCount > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                    ResetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Enter
    '    Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)

    'End Sub
    'Private Sub cmbMasterItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMasterItem.Enter
    '    Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)

    'End Sub
    'Private Sub cmbItem_InitializeLayout(sender As Object, e As Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbItem.InitializeLayout, cmbMasterItem.InitializeLayout
    '    Try
    '        Dim Layout As UltraGridLayout = e.Layout
    '        Dim ov As UltraGridOverride = Layout.Override
    '        ov.HeaderClickAction = HeaderClickAction.SortMulti
    '        ov.AllowRowFiltering = DefaultableBoolean.True
    '        'ov.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Equals
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub cmbItem_TextChanged(sender As Object, e As EventArgs) Handles cmbItem.TextChanged
        Try
            ' Ayesha Rehman : TFS1154 : Adding Trade and Retail price to their respective text boxes
            If cmbItem.ActiveRow IsNot Nothing Then
                Me.cmbItem.DisplayLayout.Bands(0).ColumnFilters.ClearAllFilters()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbMasterItem_TextChanged(sender As Object, e As EventArgs) Handles cmbMasterItem.TextChanged
        Try
            ' Ayesha Rehman : TFS1154 : Adding Trade and Retail price to their respective text boxes
            If cmbMasterItem.ActiveRow IsNot Nothing Then
                Me.cmbMasterItem.DisplayLayout.Bands(0).ColumnFilters.ClearAllFilters()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmFinishGoodStandard_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            FillCombos("Department")
            FillCombos("MasterArticle")
            FillCombos("ByProduct")
            FillCombos("Item")
            FillCombos("LabourType")
            FillCombos("Category")
            FillCombos("Remarks") 'TASKM176151 Call Remarks's Combobox
            FillCombos("SubDepartment")
            FillCombos("ProductionStepAccount")
            FillCombos("ProductionStep")
            'GetDetail(FinishGoodId)
            'GetFinishGoodOverHeads(FinishGoodId)
            'GetFinishGoodByProducts(Me.cmbMasterItem.Value)
            'GetFinishGoodLabourAllocation(Me.cmbMasterItem.Value)
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

            id = Me.cmbProductionStep.SelectedValue
            FillCombos("ProductionStep")
            Me.cmbProductionStep.SelectedValue = id

            id = Me.cmbAccount.SelectedValue
            FillCombos("ProductionStepAccount")
            Me.cmbAccount.SelectedValue = id

            ' ''TASK TFS2104
            id = Me.cmbByProduct.ActiveRow.Cells(0).Value
            FillCombos("ByProduct")
            Me.cmbByProduct.Value = id
            ' ''END TASK TFS2104
            ' ''TASK TFS2113
            id = Me.cmbStage.SelectedValue
            FillCombos("Stages")
            Me.cmbStage.SelectedValue = id
            ' ''END TASK TFS2113

            ' ''TASK TFS2113
            id = Me.cmbLabourType.Value
            FillCombos("LabourType")
            Me.cmbLabourType.Value = id
            ' ''END TASK TFS2113
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbMasterItem_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMasterItem.Leave
        Try
            If Me.cmbMasterItem.ActiveRow Is Nothing Then Exit Sub
            'GetDetail(FinishGoodId)
            'GetByProducts(Me.cmbMasterItem.Value)
            'GetProductionOverHeads(Me.cmbMasterItem.Value)
            'GetLabourAllocation(Me.cmbMasterItem.Value)
            'Me.txtStandardNo.Text = Val(Me.cmbMasterItem.ActiveRow.Cells("Pack Qty").Value.ToString)
            'Me.txtPurchasePrice.Text = Val(Me.cmbMasterItem.ActiveRow.Cells("Pur Price").Value.ToString)
            'Me.txtStandardName.Text = Val(Me.cmbMasterItem.ActiveRow.Cells("Sale Price").Value.ToString)
            If cmbMasterItem.Value > 0 Then
                If btnSave.Text = "&Save" Or btnSave.Text = "Save" Then
                    Me.txtVersion.Text = FinishGoodDAL.GetVersion(Me.cmbMasterItem.Value)
                    'Task 3420 set standard name of product master item exact name
                    Me.txtStandardName.Text = Me.cmbMasterItem.ActiveRow.Cells(2).Value.ToString
                    FillCombos("SubDepartment")
                    FillCombos("ProductionStep")
                    FillCombos("Stages")
                    FillCombos("grdDetailArticle")
                    FillCombos("grdPacking")
                End If
            Else
                Me.txtVersion.Text = String.Empty
                Me.txtStandardName.Text = String.Empty
            End If

            If Me.grdDetail.RowCount > 0 Then
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
            Dim dtGrid As DataTable = CType(Me.grdDetail.DataSource, DataTable)
            Dim Serial As String = Me.grdDetail.GetRows.GetLength(0)
            Dim drFound() As DataRow = dtGrid.Select("MaterialArticleId = " & cmbItem.ActiveRow.Cells(0).Value & " and Category='" & Me.cmbCategorys.Text.Replace("'", "''") & "'")
            Dim drMatched() As DataRow = dtGrid.Select("SubDepartmentId = " & IIf(Me.cmbSubDepartment.SelectedIndex <= 0, -1, Me.cmbSubDepartment.SelectedValue) & " And MaterialArticleId = " & cmbItem.ActiveRow.Cells(0).Value & "")
            If drMatched.Length > 0 Then
                drMatched(0)(EnumGridDetail.PackQty) += Val(Me.txtCostSheet.Text)
                drMatched(0)(EnumGridDetail.Qty) += Val(Me.txtCostSheet.Text)
                drMatched(0)(EnumGridDetail.TotalQty) += Val(Me.txtTotalQty1.Text)

                'drMatched(0)(EnumGridDetail.TotalQty) += Val(qty + (qty * Me.txtPercentageCon.Text / 100))
            ElseIf drFound.Length > 0 AndAlso drMatched.Length = 0 AndAlso Not Me.cmbCategorys.Text = "" Then
                drMatched(0)(EnumGridDetail.PackQty) += Val(Me.txtCostSheet.Text)
                drFound(0)(EnumGridDetail.Qty) += Val(Me.txtCostSheet.Text)
                drFound(0)(EnumGridDetail.TotalQty) += Val(Me.txtTotalQty1.Text)
                ''Below line is commented against TASK TFS3581 on 20-06-2018
                'drFound(0)(EnumGridDetail.TotalQty) += Val(qty + (qty * Me.txtPercentageCon.Text / 100))
            Else
                Dim dr As DataRow = dtGrid.NewRow
                txtPercentageCon_TextChanged(Nothing, Nothing)
                dr(EnumGridDetail.MaterialArticleId) = cmbItem.ActiveRow.Cells(0).Value
                dr(EnumGridDetail.ArticleCode) = cmbItem.ActiveRow.Cells("Code").Value.ToString
                dr(EnumGridDetail.ArticleDescription) = cmbItem.ActiveRow.Cells("Item").Value.ToString
                dr(EnumGridDetail.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Value.ToString
                dr(EnumGridDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value.ToString
                dr(EnumGridDetail.ArticleUnitName) = Me.cmbItem.ActiveRow.Cells("Unit").Value.ToString
                dr(EnumGridDetail.Category) = Me.cmbCategorys.Text
                dr(EnumGridDetail.PurchasePrice) = Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value
                dr(EnumGridDetail.SalePrice) = Me.cmbItem.ActiveRow.Cells("Price").Value

                dr(EnumGridDetail.PackQty) = Val(Me.txtPackQty.Text)

                dr(EnumGridDetail.Qty) = Val(qty)
                dr(EnumGridDetail.Percentage) = Val(Me.txtPercentageCon.Text)
                'dr(EnumGridDetail.TotalQty) = Val(qty + (qty * Me.txtPercentageCon.Text / 100))
                dr(EnumGridDetail.TotalQty) = Val(Me.txtTotalQty1.Text)

                dr(EnumGridDetail.DetailArticleId) = 0
                dr(EnumGridDetail.PackingId) = 0
                dr(EnumGridDetail.ArticleSize) = Me.cmbCostSheetUnit.Text
                dr(EnumGridDetail.Tax) = Val(Me.txtTaxPercent.Text)
                dr(EnumGridDetail.Remarks) = Me.cmbRemarks.Text
                dr(EnumGridDetail.SubDepartmentId) = Me.cmbSubDepartment.SelectedValue
                dr(EnumGridDetail.SubDepartment) = Me.cmbSubDepartment.Text
                dr(EnumGridDetail.Id) = 0
                dr(EnumGridDetail.FinishGoodId) = 0
                dr(EnumGridDetail.StandardNo) = Me.cmbItem.ActiveRow.Cells("StandardNo").Value.ToString
                dr(EnumGridDetail.StandardId) = Val(Me.cmbItem.ActiveRow.Cells("StandardId").Value.ToString)
                dtGrid.Rows.Add(dr)
                dtGrid.AcceptChanges()
                ApplyGridSettings()

                Me.CtrlGrdBar1_Load(Nothing, Nothing)
            End If
            Call ClearDetailControls()
            Me.cmbItem.Focus()
            FillCombos("ParentItem")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Me.grdDetail.RowCount > 0 Then ShowErrorMessage("Record not in grid.") : Exit Sub
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            If IsValidate() Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    Save()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    Edit()
                End If
            End If
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
            If Me.grdDetail.RowCount <= 0 Then Exit Sub
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
            Me.txtStandardNo.Text = GetStandardNo()
            Me.cmbMasterItem.Rows(0).Activate()
            Me.txtVersion.Text = String.Empty
            Me.txtBatchSize.Text = ""
            Me.txtStandardName.Text = String.Empty
            FillCombos("Category")
            FillCombos("Remarks")
            FillCombos("SubDepartment")
            'Me.cmbParentItem.DataSource = Nothing
            'FillCombos("ParentItem")
            Me.cmbItem.Rows(0).Activate()

            'Me.cmbByProduct.Rows(0).Activate()

            'If Not Me.cmbProductionStep.SelectedIndex = -1 Then
            '    Me.cmbProductionStep.SelectedIndex = 0
            'End If

            'If Not Me.cmbAccount.SelectedIndex = -1 Then
            '    Me.cmbAccount.SelectedIndex = 0
            'End If

            Me.txtCostSheet.Text = String.Empty
            Me.cmbMasterItem.Focus()
            Me.cmbMasterItem.Enabled = True
            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab

            GetAllRecords()

            GetDetail(-1)

            ResetDetail()

            ''TASK TFS2103
            GetFinishGoodOverHeads(-1)
            ''TASK TFS2103

            ''TASK TFS2104
            GetFinishGoodByProducts(-1)
            ''TASK TFS2104
            '' TFS2113
            GetFinishGoodLabourAllocation(-1)
            ''END TFS2113

            If Me.grdDetail.RowCount > 0 Then
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
            Me.txtPercentageCon.Text = 0
            Me.txtTotalQty1.Text = String.Empty
            Me.txtStandardName.Enabled = True
            GetSecurityRights()
            'If Not Me.cmbParentItem.ActiveRow Is Nothing Then
            '    Me.cmbParentItem.Rows(0).Activate()
            'End If
            FillCombos("ParentItem")
            RowIndex = 0
            Me.cmbCostSheetUnit.Text = "Loose"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
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
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each Rightsdt As GroupRights In Rights
                    If Rightsdt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rightsdt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
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
            'objDt = GetDataTable("Select ArticleId as Id, ArticleDescription as Item, ArticleCode as Code, Isnull(PackQty,0) as [Pack Qty], IsNull(PurchasePrice,0) as [Pur Price], IsNull(SalePrice,0) as [Sale Price] From ArticleDefTableMaster WHERE ArticleId IN(Select DISTINCT MasterArticleId From tblCostSheet)  ORDER BY ArticleDescription ASC")
            objDt = FinishGoodDAL.GetAll()

            Me.grdSaved.DataSource = objDt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns(0).Visible = False
            Me.grdSaved.RootTable.Columns("MasterArticleId").Visible = False
            Me.grdSaved.RootTable.Columns("Default1").Caption = "Default"

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
            FinishGoodId = Val(Me.grdSaved.GetRow.Cells("Id").Value.ToString)
            Me.cmbMasterItem.Value = Val(Me.grdSaved.GetRow.Cells("MasterArticleId").Value.ToString)
            Me.cmbMasterItem.Enabled = False
            Me.txtStandardNo.Text = Me.grdSaved.GetRow.Cells("StandardNo").Value.ToString
            'Me.txtStandardNo.Enabled = False
            'Me.txtPurchasePrice.Text = Val(Me.grdSaved.GetRow.Cells("Pur Price").Value.ToString)
            Me.txtVersion.Text = Val(Me.grdSaved.GetRow.Cells("Version").Value.ToString)
            'Me.txtVersion.Enabled = False
            Me.txtStandardName.Text = Me.grdSaved.GetRow.Cells("StandardName").Value.ToString

            Me.txtBatchSize.Text = Val(Me.grdSaved.GetRow.Cells("BatchSize").Value.ToString)
            Me.cbDefault.Checked = CBool(Me.grdSaved.GetRow.Cells("Default1").Value)

            'Me.txtStandardName.Enabled = False
            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(0).TabPage.Tab
            Me.GetDetail(FinishGoodId)




            'FillCombos("ProductionStep")
            'FillCombos("Stages")
            'FillCombos("grdDetatilArticle")
            'FillCombos("grdPacking")
            Me.GetFinishGoodOverHeads(FinishGoodId)
            Me.GetFinishGoodByProducts(FinishGoodId)
            Me.GetFinishGoodLabourAllocation(FinishGoodId)
            If Me.grdDetail.RowCount > 0 Then
                Me.btnPriceUpdate.Enabled = True
                Me.btnCostPriceUpdate.Enabled = True
            Else
                Me.btnPriceUpdate.Enabled = False
                Me.btnCostPriceUpdate.Enabled = False
            End If
            FillCombos("SubDepartment")
            FillCombos("ProductionStep")
            FillCombos("Stages")
            FillCombos("grdDetailArticle")
            FillCombos("grdPacking")
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
            'If Not getConfigValueByType("AssociateItems").ToString = "Error" Then
            '    AssociateItems = getConfigValueByType("AssociateItems")
            'End If
            'If AssociateItems = "False" Then
            '    CopyChildItems()
            '    Exit Sub
            'End If
            Dim dtGrid As DataTable = CType(Me.grdDetail.DataSource, DataTable)
            'check if record is already exists in the grid
            Dim drFound() As DataRow = dtGrid.Select("ArticleId = " & cmbItem.ActiveRow.Cells(0).Value)


            Dim strQuery As String = String.Empty
            'strQuery = "Select tblCostSheet.MasterArticleId From tblCostSheet WHERE MasterArticleId=" & Me.cmbItem.Value & ""
            strQuery = "Select FinishGoodMaster.FinishGoodId From FinishGoodMaster WHERE MasterArticleId=" & Me.cmbItem.ActiveRow.Cells("MasterId").Value.ToString & " Where Default1 = 1"
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
                    strQuery = "Select a.MaterialArticleId AS ArticleId, b.ArticleCode as Code, b.ArticleDescription as Item, b.PurchasePrice, b.SalePrice as Price, a.Qty, Isnull(a.ArticleSize,'Loose') as ArticleSize,ArticleColorName as Color, ArticleSizeName as Size, IsNull(a.Tax_Percent,0) as Tax, Category, a.Remarks, IsNull(a.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, b.ArticleUnitName As Unit, IsNull(a.PackingId, 0) AS PackingId, IsNull(a.DetailArticleId, 0) AS DetailArticleId From FinishGoodDetail a INNER JOIN ArticleDefView b on a.MaterialArticleId = b.ArticleId LEFT JOIN tblProSteps ON a.SubDepartmentID = tblProSteps.ProdStep_Id WHERE a.FinishGoodId=" & dtMasterArticle.Rows(0).Item(0)
                    'Altered  against task# 20150513  Ali Ansari Removing Query Syntax Error
                    dt = GetDataTable(strQuery)
                End If
            End If

            For Each row As DataRow In dt.Rows
                If drFound.Length > 0 Then
                    drFound(0)(EnumGridDetail.Qty) += 1
                Else
                    Dim dr As DataRow = dtGrid.NewRow
                    dr(EnumGridDetail.MaterialArticleId) = row("ArticleId") ' ArticleId
                    dr(EnumGridDetail.ArticleCode) = row("Code") 'Article Code
                    dr(EnumGridDetail.ArticleDescription) = row("Item") 'Article Name 
                    dr(EnumGridDetail.Color) = row("Color").ToString
                    dr(EnumGridDetail.Size) = row("Size").ToString
                    dr(EnumGridDetail.ArticleUnitName) = row("Unit").ToString
                    dr(EnumGridDetail.PurchasePrice) = row("PurchasePrice") 'Purchase Price
                    dr(EnumGridDetail.SalePrice) = row("Price") 'Article Sale Price 
                    dr(EnumGridDetail.Qty) = Val(row("Qty").ToString) 'Qty
                    dr(EnumGridDetail.DetailArticleId) = Me.cmbMasterItem.Value
                    dr(EnumGridDetail.ArticleSize) = row("ArticleSize")
                    dr(EnumGridDetail.Tax) = Val(row("Tax").ToString)
                    'TASKM176151 Add Feild Category And Remarks In Grid
                    dr(EnumGridDetail.Category) = row("Category").ToString
                    dr(EnumGridDetail.Remarks) = row("Remarks").ToString
                    dr(EnumGridDetail.SubDepartmentID) = row("SubDepartmentID")
                    dr(EnumGridDetail.SubDepartment) = row("SubDepartment").ToString
                    dr(EnumGridDetail.DetailArticleId) = row("DetailArticleId")
                    dr(EnumGridDetail.PackingId) = row("PackingId")

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
    'Private Sub CopyChildItems()
    '    Try
    '        'If Not getConfigValueByType("AssociateItems").ToString = "Error" Then
    '        '    AssociateItems = getConfigValueByType("AssociateItems")
    '        'End If
    '        Dim qty As Double = 0D
    '        If cmbItem.ActiveRow.Cells(0).Value = 0 Then
    '            ShowValidationMessage("Please select item")
    '            Me.cmbItem.Focus()
    '            Exit Sub
    '            'ElseIf Me.txtCostSheet.Text.Trim <> String.Empty Then
    '            '    Double.TryParse(Me.txtCostSheet.Text, qty)
    '            '    If qty = 0 Then
    '            '        ShowValidationMessage("Invalid Qty")
    '            '        Me.txtCostSheet.Focus()
    '            '        Exit Sub
    '            '    End If
    '        End If
    '        ''Serial numbers
    '        'Dim sNo As String = Me.grdCostSheet.GetRow.Children.ToString
    '        'SerialNo = Me.grdCostSheet.GetRow.Cells("SerialNo").Value.ToString
    '        'ChildSerialNo = "" & SerialNo & "." & sNo + 1 & ""
    '        'If Me.cmbParentItem.Items.Count > 0 Then
    '        '    Me.cmbParentItem.SelectedValue = Me.grdCostSheet.GetRow.Cells("ArticleId").Value
    '        'End If
    '        Dim Serial As String = Me.grdDetail.GetRows.GetLength(0)
    '        '' End Serial numbers
    '        Dim dtGrid As DataTable = CType(Me.grdDetail.DataSource, DataTable)
    '        'check if record is already exists in the grid
    '        Dim drFound() As DataRow = dtGrid.Select("ArticleId = " & cmbItem.ActiveRow.Cells(0).Value)


    '        Dim strQuery As String = String.Empty
    '        'strQuery = "Select tblCostSheet.MasterArticleId From tblCostSheet WHERE MasterArticleId=" & Me.cmbItem.Value & ""
    '        strQuery = "Select tblCostSheet.MasterArticleId From tblCostSheet WHERE MasterArticleId=" & Me.cmbItem.ActiveRow.Cells("MasterId").Value.ToString & ""
    '        Dim dtMasterArticle As New DataTable
    '        dtMasterArticle = GetDataTable(strQuery)
    '        Dim dt As New DataTable
    '        If dtMasterArticle IsNot Nothing Then
    '            If dtMasterArticle.Rows.Count > 0 Then
    '                'Before against task:2411
    '                'strQuery = "Select a.ArticleId, b.ArticleCode as Code, b.ArticleDescription as Item, b.PurchasePrice, b.SalePrice as Price, a.Qty,Isnull(a.ArticleSize,'Loose') as ArticleSize,ArticleColorName as Color, ArticleSizeName as Size From, Category tblCostSheet a INNER JOIN ArticleDefView b on a.ArticleId = b.ArticleId WHERE a.MasterArticleId=" & dtMasterArticle.Rows(0).Item(0)
    '                'Task:2411 Change Query
    '                'Marked against task# 20150513  
    '                'strQuery = "Select a.ArticleId, b.ArticleCode as Code, b.ArticleDescription as Item, b.PurchasePrice, b.SalePrice as Price, a.Qty,Isnull(a.ArticleSize,'Loose') as ArticleSize,ArticleColorName as Color, ArticleSizeName as Size, Category, IsNull(tblCostSheet.Tax_Percent,0) as Tax From tblCostSheet a INNER JOIN ArticleDefView b on a.ArticleId = b.ArticleId WHERE a.MasterArticleId=" & dtMasterArticle.Rows(0).Item(0)
    '                'Marked against task# 20150513  
    '                'ProdStep_Id, prod_step, prod_Less from tblProSteps 
    '                'Altered  against task# 20150513  Ali Ansari Removing Query Syntax Error
    '                strQuery = "Select a.ArticleId, b.ArticleCode as Code, b.ArticleDescription as Item, b.PurchasePrice, b.SalePrice as Price, a.Qty, Isnull(a.ArticleSize,'Loose') as ArticleSize,  ArticleColorName as Color, ArticleSizeName as Size, IsNull(a.Tax_Percent,0) as Tax, Category, a.Remarks, IsNull(a.SubDepartmentID, 0) As SubDepartmentID, tblProSteps.prod_step As SubDepartment, b.ArticleUnitName As Unit, IsNull(a.ParentId, 0) As ParentId, a.SerialNo, a.ParentSerialNo  From tblCostSheet a INNER JOIN ArticleDefView b on a.ArticleId = b.ArticleId LEFT JOIN tblProSteps ON a.SubDepartmentID = tblProSteps.ProdStep_Id WHERE a.MasterArticleId=" & dtMasterArticle.Rows(0).Item(0)
    '                'Altered  against task# 20150513  Ali Ansari Removing Query Syntax Error
    '                dt = GetDataTable(strQuery)
    '            End If
    '        End If
    '        Dim ChildSerial As Integer = 0
    '        For Each row As DataRow In dt.Rows
    '            'If drFound.Length > 0 Then
    '            '    drFound(0)(EnumGridDetail.Qty) += 1
    '            'Else
    '            Dim dr As DataRow = dtGrid.NewRow
    '            dr(EnumGridDetail.MaterialArticleId) = row("ArticleId") ' ArticleId
    '            dr(EnumGridDetail.ArticleCode) = row("Code") 'Article Code
    '            dr(EnumGridDetail.ArticleDescription) = row("Item") 'Article Name 
    '            dr(EnumGridDetail.Color) = row("Color").ToString
    '            dr(EnumGridDetail.Size) = row("Size").ToString
    '            dr(EnumGridDetail.ArticleUnitName) = row("Unit").ToString
    '            dr(EnumGridDetail.PurchasePrice) = row("PurchasePrice") 'Purchase Price
    '            dr(EnumGridDetail.SalePrice) = row("Price") 'Article Sale Price 
    '            dr(EnumGridDetail.Qty) = Val(row("Qty").ToString) 'Qty
    '            dr(EnumGridDetail.DetailArticleId) = Me.cmbMasterItem.Value
    '            dr(EnumGridDetail.ArticleSize) = row("ArticleSize")
    '            dr(EnumGridDetail.Tax) = Val(row("Tax").ToString)
    '            'TASKM176151 Add Feild Category And Remarks In Grid
    '            dr(EnumGridDetail.Category) = row("Category").ToString
    '            dr(EnumGridDetail.Remarks) = row("Remarks").ToString
    '            dr(EnumGridDetail.SubDepartmentID) = row("SubDepartmentID")
    '            dr(EnumGridDetail.SubDepartment) = row("SubDepartment").ToString
    '            'End TASKM176151
    '            'Dim ChildSerialNo1 As String = "" & Serial + 1 & "." & ChildSerial + 1 & ""

    '            'If Not Me.cmbParentItem.SelectedIndex = -1 Then
    '            '    If Me.cmbParentItem.SelectedValue > 0 Then
    '            '        If AssociateItems = "False" Then
    '            '            dr(EnumGridDetail.SerialNo) = ChildSerialNo
    '            '            dr(EnumGridDetail.ParentSerialNo) = SerialNo
    '            '        Else
    '            '            dr(EnumGridDetail.ParentId) = Me.cmbParentItem.SelectedValue
    '            '        End If
    '            '        dr(EnumGridDetail.SerialNo) = ChildSerialNo1
    '            '        dr(EnumGridDetail.ParentSerialNo) = Serial + 1
    '            '    End If
    '            '    dr(EnumGridDetail.SerialNo) = ChildSerialNo1
    '            '    dr(EnumGridDetail.ParentSerialNo) = Serial + 1
    '            'End If
    '            'If Me.cmbParentItem.SelectedIndex = -1 Or Me.cmbParentItem.SelectedValue = 0 Then
    '            '    dr(EnumGridDetail.ParentId) = 0
    '            '    dr(EnumGridDetail.SerialNo) = Serial + 1
    '            'End If
    '            dtGrid.Rows.Add(dr)
    '            'End If
    '        Next
    '        'Me.grdCostSheet.Refetch()
    '        Me.cmbItem.Focus()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdCostSheet_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.ColumnButtonClick
        '2015060010 delete button
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm("Do you want to delete this row ?") = True Then
                    If FinishGoodDAL.DeleteFinishGoodDetail(Me.grdDetail.CurrentRow.Cells("Id").Value) = True Then
                        Me.grdDetail.CurrentRow.Delete()
                        Me.grdDetail.UpdateData()
                        ShowInformationMessage("Deleted Successfully")
                    End If
                Else
                    ShowErrorMessage("Some error occur while deleting this row")
                End If
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
            objCmd.CommandText = "Delete From FinishGoodDetail WHERE FinishGoodId=" & Val(Me.grdSaved.GetRow.Cells("Id").Value.ToString) & ""
            objCmd.ExecuteNonQuery()


            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "Delete From FinishGoodMaster WHERE Id=" & Val(Me.grdSaved.GetRow.Cells("Id").Value.ToString) & ""
            objCmd.ExecuteNonQuery()


            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "Delete FROM FinishGoodOverHeads Where FinishGoodId=" & Val(Me.grdSaved.GetRow.Cells("Id").Value.ToString) & ""
            objCmd.ExecuteNonQuery()

            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "Delete FROM FinishGoodByProducts Where FinishGoodId=" & Val(Me.grdSaved.GetRow.Cells("Id").Value.ToString) & ""
            objCmd.ExecuteNonQuery()

            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "Delete FROM FinishGoodLabourAllocation Where FinishGoodId=" & Val(Me.grdSaved.GetRow.Cells("Id").Value.ToString) & ""
            objCmd.ExecuteNonQuery()

            objTrans.Commit()
            SaveActivityLog("Production", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.txtStandardNo.Text, True)
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.UltraTabControl2.SelectedTab.Index = 0 Then
                If Me.grdDetail.RowCount = 0 Then Me.btnDelete.Visible = False
                Me.btnSave.Visible = True
                Me.btnNew.Visible = True
                Me.btnRefresh.Visible = True
                Me.CtrlGrdBar1.MyGrid = grdDetail
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    'Me.grd.SaveLayoutFile(fs)
                    Me.grdDetail.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
                If Me.cmbMasterItem.ActiveRow IsNot Nothing Then Me.CtrlGrdBar1.txtGridTitle.Text = "Finish Good Standard" & Chr(10) & "" & Me.cmbMasterItem.ActiveRow.Cells("Item").Value.ToString & ""
            Else
                Me.btnDelete.Visible = True
                Me.btnSave.Visible = False
                Me.btnNew.Visible = False
                Me.btnRefresh.Visible = False
                Me.CtrlGrdBar1.MyGrid = grdSaved

                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    'Me.grd.SaveLayoutFile(fs)
                    Me.grdSaved.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
                Me.CtrlGrdBar1.txtGridTitle.Text = "Cost Sheet Detail" & Chr(10) & "" & Me.cmbMasterItem.ActiveRow.Cells("Item").Value.ToString & ""
            End If
            'Me.CtrlGrdBar1.txtGridTitle.Text = "Cost Sheet Detail" & Chr(10) & "" & Me.cmbMasterItem.ActiveRow.Cells("Item").Value.ToString & ""
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl2_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl2.SelectedTabChanged
        Try
            If Me.grdSaved Is Nothing Then Exit Sub
            If Me.grdDetail Is Nothing Then Exit Sub
            If Me.cmbMasterItem.ActiveRow Is Nothing Then Exit Sub
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'If Me.cmbDepartment.SelectedIndex = -1 Then Exit Sub
            'If Me.cmbDepartment.SelectedIndex > 0 Then
            '    FillCombos("MasterArticle")
            'End If
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
        txtPercentageCon.Text = 0
        txtTotalQty1.Text = String.Empty
    End Sub
    'Reseting Detail COntrols Task# 2015060010

    Private Sub grdCostSheet_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDetail.DoubleClick
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
        'Try
        '    'If Not frmMain.Panel2.Controls.Contains(frmGrdRptCostSheetMarginCalculationDetail) Then
        '    frmMain.LoadControl("frmGrdRptCostSheetMarginCalculationDetail")
        '    'End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtPercentageCon_TextChanged(sender As Object, e As EventArgs) Handles txtPercentageCon.TextChanged
        Try
            If Me.txtPercentageCon.Text > 0 Then
                Me.txtTotalQty1.Text = Val(Me.txtCostSheet.Text) + (Val(Me.txtCostSheet.Text) * Val(Me.txtPercentageCon.Text) / 100)
            Else
                Me.txtTotalQty1.Text = Val(Me.txtCostSheet.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCostSheet_TextChanged(sender As Object, e As EventArgs) Handles txtCostSheet.TextChanged
        Try
            If Val(Me.txtPackQty.Text) = 0 Then
                txtPackQty.Text = 1
                txtTotalQty.Text = Val(txtCostSheet.Text)
            Else
                txtTotalQty.Text = Val(txtCostSheet.Text) * Val(txtPackQty.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        'Try
        '    AddRptParam("@MasterId", Me.grdSaved.CurrentRow.Cells("Id").Value)
        '    ShowReport("rptCostSheet")
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub


    Private Sub grdDetail_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.CellEdited
        Try
            ''Below code is commented on 20-06-2018 against TASK TFS3581
            'If Me.grdDetail.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            '    If Me.grdDetail.GetRow.Cells("Percentage").DataChanged = True Or Me.grdDetail.GetRow.Cells("Qty").DataChanged = True Then
            '        If Val(Me.grdDetail.GetRow.Cells("Percentage").Value.ToString) > 0 Then
            '            Me.grdDetail.GetRow.Cells("TotalQty").Value = (Me.grdDetail.GetRow.Cells("Qty").Value + (Me.grdDetail.GetRow.Cells("Qty").Value * Me.grdDetail.GetRow.Cells("Percentage").Value / 100))
            '        Else
            '            Me.grdDetail.GetRow.Cells("TotalQty").Value = Me.grdDetail.GetRow.Cells("Qty").Value
            '        End If
            '    End If
            'End If
            'If e.Column.
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TFS2103
    ''' </summary>
    ''' <param name="ArticleId"></param>
    ''' <remarks></remarks>
    Private Sub GetFinishGoodOverHeads(ByVal FinishGoodId As Integer)
        Try
            Me.grdProductionOverHeads.DataSource = FinishGoodDAL.GetFinishGoodOverHeads(FinishGoodId)
            Me.grdProductionOverHeads.RootTable.Columns("Id").Visible = False
            Me.grdProductionOverHeads.RootTable.Columns("ProductionStepId").Visible = False
            Me.grdProductionOverHeads.RootTable.Columns("AccountId").Visible = False
            Me.grdProductionOverHeads.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' TFS2104
    ''' </summary>
    ''' <param name="ArticleId"></param>
    ''' <remarks></remarks>
    Private Sub GetFinishGoodByProducts(ByVal FinishGoodId As Integer)
        Try
            Me.grdByProduct.DataSource = FinishGoodDAL.GetFinishGoodByProducts(FinishGoodId)
            Me.grdByProduct.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            Me.grdByProduct.RootTable.Columns("Rate").TotalFormatString = "N" & DecimalPointInValue
            Me.grdByProduct.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdByProduct.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInQty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAddProductionOverHeads.Click
        Try
            If ValidateAddToPOHGrid() Then
                AddToPOHGrid()
                ApplyGridSettings()
                ResetDetail()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub AddToPOHGrid()
        Try
            Dim dtPOHs As DataTable = CType(Me.grdProductionOverHeads.DataSource, DataTable)
            Dim dr() As DataRow = dtPOHs.Select("AccountId=" & Me.cmbAccount.SelectedValue & " AND  ProductionStepId = " & Me.cmbProductionStep.SelectedValue & "")
            If dr.Length > 0 Then
                ShowErrorMessage("Same account and stage can not be added again.")
                Me.cmbProductionStep.Focus()
                Exit Sub
            End If
            Dim drPOHs As DataRow
            drPOHs = dtPOHs.NewRow
            drPOHs("Id") = 0
            drPOHs("FinishGoodId") = FinishGoodId
            drPOHs("ProductionStepId") = Me.cmbProductionStep.SelectedValue
            drPOHs("ProductionStep") = CType(Me.cmbProductionStep.SelectedItem, DataRowView).Item("Production Step").ToString
            drPOHs("AccountId") = Me.cmbAccount.SelectedValue
            drPOHs("Account") = CType(cmbAccount.SelectedItem, DataRowView).Item("Account").ToString
            drPOHs("Amount") = Val(Me.txtAmount.Text)
            drPOHs("Remarks") = Me.txtRemarks.Text
            dtPOHs.Rows.Add(drPOHs)

            '
            ''
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ValidateAddToPOHGrid() As Boolean
        Try
            If Me.cmbMasterItem.Value < 1 Then
                ShowErrorMessage("Please select Master Item.")
                Me.cmbMasterItem.Focus()
                Return False
            End If
            If Me.cmbProductionStep.SelectedIndex < 1 Then
                ShowErrorMessage("Please select Production Step.")
                Me.cmbProductionStep.Focus()

                Return False
            End If
            If Me.cmbAccount.SelectedIndex < 1 Then
                ShowErrorMessage("Please select Account.")
                Me.cmbAccount.Focus()
                Return False
            End If
            If Val(Me.txtAmount.Text) < 0 Then
                ShowErrorMessage("Please enter amount.")
                Me.txtAmount.Text = String.Empty
                Me.txtAmount.Focus()
                Return False
            End If
            'If Val(Me.txtAmount.Text) < 1 Then
            '    ShowErrorMessage("Please enter amount.")
            '    Me.txtAmount.Text = String.Empty
            '    Me.txtAmount.Focus()
            '    Return False
            'End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub FillModel()
        Try
            ' Production Over Headss
            FinishGood = New BEFinishGood()
            FinishGood.Id = FinishGoodId
            FinishGood.MasterArticleId = Me.cmbMasterItem.Value
            FinishGood.StandardName = Me.txtStandardName.Text
            FinishGood.StandardNo = Me.txtStandardNo.Text
            FinishGood.BatchSize = Val(Me.txtBatchSize.Text)
            FinishGood.Version = Val(Me.txtVersion.Text)
            FinishGood.Default1 = Me.cbDefault.Checked

            Me.grdDetail.UpdateData()
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                'FinishGoodId , MaterialArticleId, DetailArticleId, PackingId, Qty,ArticleSize,Category,Tax_Percent, Tax_Amount, Remarks, CostPrice, SubDepartmentId, Percentage_Con, TotalQty
                Dim Detail As New BEFinishGoodDetail
                Detail.Id = row.Cells("Id").Value
                Detail.FinishGoodId = row.Cells("FinishGoodId").Value
                Detail.MaterialArticleId = row.Cells("MaterialArticleId").Value
                Detail.DetailArticleId = row.Cells("DetailArticleId").Value
                Detail.PackingId = row.Cells("PackingId").Value
                Detail.PackQty = row.Cells("PackQty").Value
                Detail.Qty = row.Cells("Qty").Value
                Detail.ArticleSize = row.Cells("ArticleSize").Value
                Detail.Category = row.Cells("Category").Value
                Detail.Tax_Percent = row.Cells("Tax").Value
                'Detail.Tax_Amount = row.Cells("Tax_Amount").Value
                Detail.Remarks = row.Cells("Remarks").Value.ToString
                Detail.CostPrice = row.Cells(EnumGridDetail.PurchasePrice).Value
                Detail.SubDepartmentId = row.Cells("SubDepartmentId").Value
                Detail.Percentage_Con = row.Cells(EnumGridDetail.Percentage).Value
                Detail.TotalQty = row.Cells("TotalQty").Value


                FinishGood.Detail.Add(Detail)
            Next


            grdProductionOverHeads.UpdateData()
            If Me.grdProductionOverHeads.RowCount > 0 Then
                'ProductionOverHeadsList = New List(Of BEProductionOverHeads)
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProductionOverHeads.GetRows
                    Dim OverHeads As New BEFinishGoodOverHeads
                    OverHeads.Id = row.Cells("Id").Value
                    OverHeads.ProductionStepId = row.Cells("ProductionStepId").Value
                    OverHeads.AccountId = row.Cells("AccountId").Value
                    OverHeads.FinishGoodId = row.Cells("FinishGoodId").Value
                    OverHeads.Amount = row.Cells("Amount").Value
                    OverHeads.Remarks = row.Cells("Remarks").Value.ToString
                    FinishGood.OverHeadsList.Add(OverHeads)
                Next
            End If
            '' By Product
            grdByProduct.UpdateData()
            If Me.grdByProduct.RowCount > 0 Then
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdByProduct.GetRows
                    Dim ByProduct As New BEFinishGoodByProducts
                    ByProduct.Id = row.Cells("Id").Value
                    ByProduct.ArticleId = row.Cells("ArticleId").Value
                    ByProduct.FinishGoodId = row.Cells("FinishGoodId").Value
                    ByProduct.Rate = row.Cells("Rate").Value
                    ByProduct.Qty = row.Cells("Qty").Value
                    ByProduct.Remarks = row.Cells("Remarks").Value.ToString
                    FinishGood.ByProductList.Add(ByProduct)
                Next
            End If
            '' Labour Allocation
            grdLabourAllocation.UpdateData()
            If Me.grdLabourAllocation.RowCount > 0 Then
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdLabourAllocation.GetRows
                    Dim LabourAllocation As New BEFinishGoodLabourAllocation
                    LabourAllocation.Id = row.Cells("Id").Value
                    LabourAllocation.LabourTypeId = row.Cells("LabourTypeId").Value
                    LabourAllocation.ProductionStepId = row.Cells("ProductionStepId").Value
                    LabourAllocation.RatePerUnit = row.Cells("RatePerUnit").Value
                    LabourAllocation.FinishGoodId = row.Cells("FinishGoodId").Value
                    FinishGood.LabourAllocationList.Add(LabourAllocation)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdProductionOverHeads_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdProductionOverHeads.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Dim ProductionOverHeadsId = Me.grdProductionOverHeads.GetRow.Cells("Id").Value
                If ProductionOverHeadsId > 0 Then
                    If FinishGoodDAL.DeleteFinishGoodOverHeads(ProductionOverHeadsId) Then

                    End If
                End If
                msg_Information("Row has been deleted.")
                Me.grdProductionOverHeads.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ResetDetail()
        Try
            If Not cmbAccount.SelectedIndex = -1 Then
                Me.cmbAccount.SelectedIndex = 0
            End If
            If Not cmbProductionStep.SelectedIndex = -1 Then
                Me.cmbProductionStep.SelectedIndex = 0
            End If
            Me.txtAmount.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''
    Public Function ValidateAddToByProductGrid() As Boolean
        Try
            If Me.cmbMasterItem.Value < 1 Then
                ShowErrorMessage("Please select Master Item.")
                Me.cmbMasterItem.Focus()
                Return False
            End If
            If Me.cmbByProduct.Value < 1 Then
                ShowErrorMessage("Please select Product.")
                Me.cmbByProduct.Focus()
                Return False
            End If
            If Val(Me.txtByProductQty.Text) < 1 Then
                ShowErrorMessage("Please enter valid Qty.")
                Me.txtByProductQty.Text = String.Empty
                Me.txtByProductQty.Focus()
                Return False
            End If
            'If Val(Me.txtAmount.Text) < 1 Then
            '    ShowErrorMessage("Please enter amount.")
            '    Me.txtAmount.Text = String.Empty
            '    Me.txtAmount.Focus()
            '    Return False
            'End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub ResetByProductControls()
        Try
            If Not cmbByProduct.ActiveRow Is Nothing Then
                Me.cmbByProduct.Rows(0).Activate()
            End If
            Me.txtByProductQty.Text = String.Empty
            Me.txtByProductRemarks.Text = String.Empty
            Me.txtByProductRate.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub AddToByProductGrid()
        Try
            Dim dtByProduct As DataTable = CType(Me.grdByProduct.DataSource, DataTable)
            Dim dr() As DataRow = dtByProduct.Select("ArticleId=" & Me.cmbByProduct.Value & "")
            If dr.Length > 0 Then
                ShowErrorMessage("Selected Product already exists.")
                Me.cmbProductionStep.Focus()
                Exit Sub
            End If
            Dim drByProduct As DataRow
            drByProduct = dtByProduct.NewRow
            drByProduct("Id") = 0
            drByProduct("ArticleId") = Me.cmbByProduct.Value
            drByProduct("Product") = Me.cmbByProduct.ActiveRow.Cells("Item").Value.ToString
            drByProduct("FinishGoodId") = FinishGoodId
            drByProduct("Rate") = Val(Me.txtByProductRate.Text)
            drByProduct("Qty") = Val(Me.txtByProductQty.Text)
            drByProduct("Remarks") = Me.txtByProductRemarks.Text
            dtByProduct.Rows.Add(drByProduct)
            ''
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnByProductAdd_Click(sender As Object, e As EventArgs) Handles btnByProductAdd.Click
        Try
            If ValidateAddToByProductGrid() Then
                AddToByProductGrid()
                ApplyGridSettings()
                ResetByProductControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdByProduct_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdByProduct.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Dim ByProductsId = Me.grdByProduct.GetRow.Cells("ByProductsId").Value
                If ByProductsId > 0 Then
                    If FinishGoodDAL.DeleteFinishGoodByProduct(ByProductsId) Then

                    End If
                End If
                msg_Information("Row has been deleted.")
                Me.grdByProduct.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbByProduct_Leave(sender As Object, e As EventArgs) Handles cmbByProduct.Leave
        Try
            Me.txtByProductRate.Text = Val(Me.cmbByProduct.ActiveRow.Cells("PurchasePrice").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoByProCode_CheckedChanged(sender As Object, e As EventArgs) Handles rboByProductCode.CheckedChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.rboByProductCode.Checked = True Then Me.cmbByProduct.DisplayMember = "Code"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoByProName_CheckedChanged(sender As Object, e As EventArgs) Handles rboByProductName.CheckedChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.rboByProductName.Checked = True Then Me.cmbByProduct.DisplayMember = "Item"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddLabourAllocation_Click(sender As Object, e As EventArgs) Handles btnAddLabourAllocation.Click
        Try
            If ValidateAddToLabourAllocation() Then
                AddToLabourAllocationGrid()
                ApplyGridSettings()
                ResetLabourAllocationControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ResetLabourAllocationControls()
        Try
            If Not cmbLabourType.ActiveRow Is Nothing Then
                Me.cmbLabourType.Rows(0).Activate()
            End If
            If Not cmbStage.SelectedIndex = -1 Then
                Me.cmbStage.SelectedIndex = 0
            End If
            Me.txtPerUnitRate.Text = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ValidateAddToLabourAllocation() As Boolean
        Try
            If Me.cmbLabourType.Value < 1 Then
                ShowErrorMessage("Please select Labour Type.")
                Me.cmbLabourType.Focus()
                Return False
            End If
            If Me.cmbStage.SelectedValue < 1 Then
                ShowErrorMessage("Please select State.")
                Me.cmbByProduct.Focus()
                Return False
            End If
            If Val(Me.txtPerUnitRate.Text) < 0 Then
                ShowErrorMessage("Please enter unit rate.")
                Me.txtPerUnitRate.Text = String.Empty
                Me.txtPerUnitRate.Focus()
                Return False
            End If
            'If Val(Me.txtAmount.Text) < 1 Then
            '    ShowErrorMessage("Please enter amount.")
            '    Me.txtAmount.Text = String.Empty
            '    Me.txtAmount.Focus()
            '    Return False
            'End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub AddToLabourAllocationGrid()
        Try
            Dim dtLabourAllocation As DataTable = CType(Me.grdLabourAllocation.DataSource, DataTable)
            Dim dr() As DataRow = dtLabourAllocation.Select("ProductionStepId=" & Me.cmbStage.SelectedValue & " AND LabourTypeId = " & Me.cmbLabourType.Value & "")
            If dr.Length > 0 Then
                ShowErrorMessage("Same stage and type can not be added again.")
                Me.cmbStage.Focus()
                Exit Sub
            End If
            Dim drLabourAllocation As DataRow
            drLabourAllocation = dtLabourAllocation.NewRow
            drLabourAllocation("Id") = 0
            drLabourAllocation("ProductionStepId") = Me.cmbStage.SelectedValue
            drLabourAllocation("ProductionStep") = CType(Me.cmbStage.SelectedItem, DataRowView).Item("Production Step").ToString
            drLabourAllocation("LabourTypeId") = Me.cmbLabourType.Value
            drLabourAllocation("LabourType") = Me.cmbLabourType.ActiveRow.Cells("LabourType").Value.ToString
            drLabourAllocation("ChargeType") = Me.cmbLabourType.ActiveRow.Cells("ChargeType").Value.ToString
            drLabourAllocation("RatePerUnit") = Val(Me.txtPerUnitRate.Text)
            drLabourAllocation("FinishGoodId") = FinishGoodId
            dtLabourAllocation.Rows.Add(drLabourAllocation)
            ''
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' TFS2113
    ''' </summary>
    ''' <param name="ArticleId"></param>
    ''' <remarks></remarks>
    Private Sub GetFinishGoodLabourAllocation(ByVal FinishGooodId As Integer)
        Try
            Me.grdLabourAllocation.DataSource = FinishGoodDAL.GetFinishGoodLabourAllocations(FinishGooodId)
            'Me.grdProductionOverHeads.RootTable.Columns("ProductionOverHeadsId").Visible = False
            'Me.grdProductionOverHeads.RootTable.Columns("ProductionStepId").Visible = False
            'Me.grdProductionOverHeads.RootTable.Columns("AccountId").Visible = False
            'Me.grdProductionOverHeads.RootTable.Columns("ArticleId").Visible = False
            Me.grdLabourAllocation.RootTable.Columns("RatePerUnit").FormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdLabourAllocation_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdLabourAllocation.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                'TASK 3420 Laybour type id name is inccorect which is fixed by Saad Afzaal'
                Dim LabourAllocationId = Me.grdLabourAllocation.GetRow.Cells("Id").Value
                If LabourAllocationId > 0 Then
                    'TASK 3420 Saad Afzaal Call Delete Function to Delete Particular FinishGoodLabourAllocation'
                    If ArticleDAL.DeleteFinishGoodLabourAllocation(LabourAllocationId) Then
                        msg_Information("Row has been deleted.")
                        Me.grdLabourAllocation.GetRow.Delete()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Save()
        Try
            If New FinishGoodDAL().Add(FinishGood) Then
                SaveActivityLog("Production", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtStandardNo.Text, True)
                msg_Information("Record has been updated successfull.")
                ResetControls()
            Else
                msg_Information("Record could not save.")
                'ResetControls()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Edit()
        Try
            If New FinishGoodDAL().Update(FinishGood) Then
                SaveActivityLog("Production", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtStandardNo.Text, True)
                msg_Information("Record has been updated successfull.")
                ResetControls()
            Else
                msg_Information("Record could not update.")
                'ResetControls()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function GetStandardNo() As String
        Dim StandardNo As String = String.Empty
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                StandardNo = GetSerialNo("STD" + "-" + Microsoft.VisualBasic.Right(Now.Year, 2) + "-", "FinishGoodMaster", "StandardNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                StandardNo = GetNextDocNo("STD" & "-" & Format(Now, "yy") & Now.Month.ToString("00"), 4, "FinishGoodMaster", "StandardNo")
            Else
                StandardNo = GetNextDocNo("STD", 6, "FinishGoodMaster", "StandardNo")
            End If
            Return StandardNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidate() As Boolean
        Try
            Dim DataExists As Boolean = False
            If txtStandardNo.Text = "" Then
                msg_Error("Please enter Standard No.")
                txtStandardNo.Focus() : IsValidate = False : Exit Function
            End If

            If Val(txtBatchSize.Text) < 1 Then
                msg_Error("Batch Size should be greator than zero.")
                txtBatchSize.Focus() : IsValidate = False : Exit Function
            End If


            Me.grdDetail.UpdateData()
            If Not Me.grdDetail.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                cmbMasterItem.Focus() : IsValidate = False : Exit Function
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbMasterItem_ValueChanged(sender As Object, e As EventArgs)
        Try
            If Me.cmbMasterItem.ActiveRow Is Nothing Then Exit Sub
            'GetDetail(FinishGoodId)
            'GetByProducts(Me.cmbMasterItem.Value)
            'GetProductionOverHeads(Me.cmbMasterItem.Value)
            'GetLabourAllocation(Me.cmbMasterItem.Value)
            'Me.txtStandardNo.Text = Val(Me.cmbMasterItem.ActiveRow.Cells("Pack Qty").Value.ToString)
            'Me.txtPurchasePrice.Text = Val(Me.cmbMasterItem.ActiveRow.Cells("Pur Price").Value.ToString)
            'Me.txtStandardName.Text = Val(Me.cmbMasterItem.ActiveRow.Cells("Sale Price").Value.ToString)
            If cmbMasterItem.Value > 0 Then
                If btnSave.Text = "&Save" Or btnSave.Text = "Save" Then
                    Me.txtVersion.Text = FinishGoodDAL.GetVersion(Me.cmbMasterItem.Value)
                    Me.txtStandardName.Text = Me.cmbMasterItem.Text
                    'FillCombos("grdDetailArticle")
                    'FillCombos("grdPacking")
                End If
                FillCombos("ProductionStep")
                FillCombos("Stages")
            Else
                Me.txtVersion.Text = String.Empty
                Me.txtStandardName.Text = String.Empty
            End If

            If Me.grdDetail.RowCount > 0 Then
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

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPerUnitRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPerUnitRate.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtByProductRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtByProductRate.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtByProductQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtByProductQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If Me.cmbItem.Value > 0 Then
                FillCombos("Unit")
            End If
            If IsOpenedForm = True Then FillCombos("Unit")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "")

        Me.grdProductionOverHeads.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue

        Me.grdByProduct.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
        Me.grdByProduct.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty

        Me.grdLabourAllocation.RootTable.Columns("RatePerUnit").FormatString = "N" & DecimalPointInValue

        Me.grdDetail.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
        Me.grdDetail.RootTable.Columns("TotalQty").FormatString = "N" & DecimalPointInQty

    End Sub

    Private Sub frmFinishGoodStandard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' TASK TFS3446
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks> Calling of finish good popup here to display the detail of finish good item.</remarks>
    Private Sub grdDetail_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.LinkClicked
        Try
            If Me.grdDetail.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "StandardNo" Then
                    Dim StandardId As Integer = Val(Me.grdDetail.GetRow.Cells("StandardId").Value.ToString)
                    Dim FilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdSaved.RootTable.Columns("Id"), Janus.Windows.GridEX.ConditionOperator.Equal, StandardId)
                    Dim FindAll As Integer = Me.grdSaved.FindAll(FilterCondition)
                    Dim Obj As New BEFinishGood
                    Obj.Id = Val(Me.grdSaved.GetRow.Cells("Id").Value.ToString)
                    Obj.MasterArticleId = Val(Me.grdSaved.GetRow.Cells("MasterArticleId").Value.ToString)
                    Obj.StandardNo = Me.grdSaved.GetRow.Cells("StandardNo").Value.ToString
                    Obj.Version = Val(Me.grdSaved.GetRow.Cells("Version").Value.ToString)
                    Obj.StandardName = Me.grdSaved.GetRow.Cells("StandardName").Value.ToString
                    Obj.BatchSize = Val(Me.grdSaved.GetRow.Cells("BatchSize").Value.ToString)
                    Obj.Default1 = CBool(Me.grdSaved.GetRow.Cells("Default1").Value)
                    Dim Dialog As New frmFinishGoodStandardDialog(Obj)
                    Dialog.ShowDialog()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCostSheetUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCostSheetUnit.SelectedIndexChanged
        Try
            If Me.cmbCostSheetUnit.Text = "Loose" Then
                txtPackQty.Text = 1
                Me.txtPackQty.Enabled = False
                Me.txtPackQty.TabStop = False
                Me.txtTotalQty.Enabled = False
            Else
                Me.txtPackQty.Enabled = True
                Me.txtPackQty.TabStop = True
                Me.txtTotalQty.Enabled = True
                If TypeOf Me.cmbCostSheetUnit.SelectedItem Is DataRowView Then
                    Me.txtPackQty.Text = Val(CType(Me.cmbCostSheetUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_TextChanged(sender As Object, e As EventArgs) Handles txtPackQty.TextChanged
        Try
            If Val(Me.txtPackQty.Text) = 0 Then
                txtPackQty.Text = 1
                txtTotalQty.Text = Val(txtCostSheet.Text)
            Else
                txtTotalQty.Text = Val(txtCostSheet.Text) * Val(txtPackQty.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCostSheet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCostSheet.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbMasterItem_ValueChanged_1(sender As Object, e As EventArgs) Handles cmbMasterItem.ValueChanged
        Try
            FillCombos("SubDepartment")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class