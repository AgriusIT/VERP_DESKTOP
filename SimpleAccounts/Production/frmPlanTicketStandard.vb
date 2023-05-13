''TASK TFS4782 and TFS4784 Batch Size should not be multiplied with Cost Sheet Qty and Batch No combo leaving issue. 

Imports SBDal
Imports SBModel
Imports System.Data.OleDb

Public Class frmPlanTicketStandard
    Implements IGeneral
    Dim PlanTicketsMaster As PlanTicketsMaster
    Dim PlanTicketsDetail As PlanTicketsDetail
    Dim PlanTicketsDAL As PlanTicketsStandardDAL
    Dim PlanTicketsMasterID As Integer = 0
    Public IsEditMode As Boolean = False
    Dim PlanId As Integer = 0
    Dim PlanDetailId As Integer = 0
    Dim Qty As Double = 0
    Dim IssuedQty As Double = 0
    Dim RemainingQty As Double = 0
    Private Const ADDITION_TYPE As String = "Plus"
    Private Const SUBTRACTION_TYPE As String = "Minus"
    Public ParentTicketNo As String = String.Empty
    'Task 3440 Add isOpen Attribute to check whether forn is open or not
    Public Shared isFormOpen As Boolean = False
    'Task 3440 Add Ticket_Id Attribute to get TicketId from class constructor
    Public Shared Ticket_Id As Integer = 0
    Public Shared MasterArticleId As Integer
    Dim IsBulkChildItem As Boolean = False
    'Dim TicketId As Integer = 0

    'Private Sub New()

    '    ' This call is required by the designer.
    '    InitializeComponent()
    '    PlanTicketsDAL = New PlanTicketsDAL()
    '    ' Add any initialization after the InitializeComponent() call.
    'End Sub


    Structure HistoryGrid
        Public Shared MasterArticleId As String = "MasterArticleId"
        Public Shared BatchNo As String = "BatchNo"
        Public Shared BatchSize As String = "BatchSize"
        Public Shared ExpiryDate As String = "ExpiryDate"
        Public Shared TicketId As String = "PlanTicketsMasterID"
        Public Shared CustomerId As String = "CustomerID"
        Public Shared PlanId As String = "PlanID"
        Public Shared TicketDate As String = "TicketDate"
        Public Shared SalesOrderId As String = "SalesOrderID"
        Public Shared Remarks As String = "Remarks"
        Public Shared TicketNo As String = "TicketNo"
        Public Shared FinishGoodId As String = "FinishGoodId"
        Public Shared NoOfBatches As String = "NoOfBatches"
        Public Shared ParentTicketNo As String = "ParentTicketNo"
    End Structure
    Structure DetailGrid
        Public Shared PlanTicketsDetailID As String = "PlanTicketsDetailID"
        Public Shared PlanTicketsMasterID As String = "PlanTicketsMasterID"
        Public Shared ArticleId As String = "ArticleId"
        Public Shared ArticleCode As String = "ArticleCode"
        Public Shared ArticleDescription As String = "ArticleDescription"
        Public Shared UnitName As String = "UnitName"
        Public Shared PlanDetailId As String = "PlanDetailId"
        Public Shared Quantity As String = "Quantity"
        Public Shared PackingId As String = "PackingId"
    End Structure
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            PlanTicketsStandardDAL.Delete(Me.grdMaster.GetRow.Cells("PlanTicketsMasterID").Value)
            SaveActivityLog("Production", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.txtTicketNo.Text, True)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim Str As String = String.Empty
        Try

            If Condition = "Customer" Then
                'str = "SELECT     tblVendor.AccountId AS ID, tblVendor.VendorName AS Name, tblListTerritory.TerritoryName AS Territory, tblListCity.CityName AS City,  " & _
                '        "tblListState.StateName AS State, tblVendor.AccountId AS AcId " & _
                '        "FROM         tblListTerritory INNER JOIN " & _
                '        "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
                '        "tblListState ON tblListCity.StateId = tblListState.StateId INNER JOIN " & _
                '        "tblVendor ON tblListTerritory.TerritoryId = tblVendor.Territory"

                Str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,  vwCOAdetail.sub_sub_title as [Ac Head], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                    "dbo.tblListTerritory.TerritoryName as Territory " & _
                                    "FROM         dbo.tblCustomer INNER JOIN " & _
                                    "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                    "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                    "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                    "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                    "WHERE     (dbo.vwCOADetail.account_type = 'Customer') order by tblCustomer.Sortorder, vwCOADetail.detail_title "

                FillUltraDropDown(cmbCustomer, Str)
                cmbCustomer.Rows(0).Activate()
                If Me.cmbCustomer.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            ElseIf Condition = "SalesOrder" Then
                'Str = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo from SalesOrderMasterTable where SalesOrderMasterTable.VendorId=" & Me.cmbCustomer.Value & " ORDER BY SalesOrderMasterTable.SalesOrderDate DESC "
                Str = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo from SalesOrderMasterTable " & IIf(Me.cmbCustomer.Value > 0, "Where SalesOrderMasterTable.VendorId = " & Me.cmbCustomer.Value & "", "") & " ORDER BY SalesOrderMasterTable.SalesOrderDate DESC "
                FillDropDown(cmbSalesOrder, Str)
            ElseIf Condition = "SalesOrders" Then
                Str = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo from SalesOrderMasterTable where SalesOrderId In(Select POId From PlanMasterTable) AND SalesOrderId Not In(Select SalesOrderId From PlanTicketsMaster)  ORDER BY SalesOrderMasterTable.SalesOrderDate DESC "
                FillDropDown(cmbSalesOrder, Str)
            ElseIf Condition = "Plan" Then
                Str = "Select PlanId, PlanNo + ' ~ ' + Convert(varchar(12), PlanDate,113) as PlanNo, PlanNo As UsedForTicket from PlanMasterTable where POId=" & Me.cmbSalesOrder.SelectedValue & " Order by PlanDate DESC "
                FillDropDown(cmbPlan, Str)
            ElseIf Condition = "Plans" Then
                'Str = "Select PlanId, PlanNo + ' ~ ' + Convert(varchar(12), PlanDate,113) as PlanNo, PlanNo As UsedForTicket from PlanMasterTable where PlanId Not In (Select PlanID from PlanTicketsMaster) Order by PlanDate DESC"
                'Str = "Select PlanId, PlanNo + ' ~ ' + Convert(varchar(12), PlanDate,113) as PlanNo, PlanNo As UsedForTicket from PlanMasterTable Where PlanId In (Select PlanId from PlanDetailTable Where IsNull(Qty, 0) > IsNull(TicketIssuedQty, 0) ) Order by PlanDate DESC"
                Str = "Select PlanId, PlanNo + ' ~ ' + Convert(varchar(12), PlanDate,113) as PlanNo, PlanNo As UsedForTicket from PlanMasterTable Order by PlanDate DESC"

                FillDropDown(cmbPlan, Str)
            ElseIf Condition = "Item" Then
                'Str = "SELECT ArticleDefTable.ArticleId as Id, ArticleDefTable.ArticleDescription Item, ArticleDefTable.ArticleCode Code, ArticleUnitDefTable.ArticleUnitName As UnitName, IsNull(PlanDetailTable.PlanDetailId, 0) As PlanDetailId, Sum(PlanDetailTable.Qty) As Qty, ArticleDefTable.MasterID FROM ArticleDefTable INNER JOIN PlanDetailTable ON ArticleDefTable.ArticleId = PlanDetailTable.ArticleDefId LEFT JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId where PlanDetailTable.PlanId = " & Me.cmbPlan.SelectedValue & " Group By ArticleDefTable.ArticleId, ArticleDefTable.ArticleDescription, ArticleDefTable.ArticleCode, PlanDetailTable.PlanDetailId, ArticleUnitDefTable.ArticleUnitName, ArticleDefTable.MasterID "
                'Str = "SELECT ArticleDefTable.ArticleId as Id, ArticleDefTable.ArticleCode Code, ArticleDefTable.ArticleDescription Item, ArticleUnitDefTable.ArticleUnitName As UnitName, ArticleDefTable.MasterID FROM ArticleDefTable LEFT JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId WHERE ArticleDefTable.MasterID= " & Me.cmbTicketProduct.Value & "   "

                Str = "SELECT ArticleDefTable.ArticleId as Id, ArticleDefTable.ArticleCode Code, ArticleDefTable.ArticleDescription Item, ArticleUnitDefTable.ArticleUnitName As UnitName, ArticleColorName As Color , ArticleSizeName As Size, ArticleDefTable.MasterID FROM ArticleDefTable LEFT JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId INNER JOIN ArticleDefView ON ArticleDefTable.ArticleId = ArticleDefView.ArticleId WHERE ArticleDefTable.MasterID = " & Me.cmbTicketProduct.Value & " "

                FillUltraDropDown(Me.cmbItem, Str)
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterID").Hidden = True
                End If
                'Me.rbCode.Checked = True

            ElseIf Condition = "TicketProduct" Then
                Str = "Select ArticleId as Id, ArticleDescription as Item, ArticleCode as Code, ArticleUnitDefTable.ArticleUnitName As Unit, Isnull(PackQty,0) as [Pack Qty], IsNull(PurchasePrice,0) as [Pur Price], IsNull(SalePrice,0) as [Sale Price], IsNull(ArticleDefTableMaster.ProductionProcessId, 0) AS ProductionProcessId, IsNull(PlanDetailTable.PlanDetailId, 0) As PlanDetailId, Sum((PlanDetailTable.Qty)-IsNull(PlanDetailTable.TicketIssuedQty, 0)) As Qty, ArticleDefTableMaster.SortOrder, IsNull(PlanDetailTable.LocationId, 0) As LocationId From ArticleDefTableMaster INNER JOIN PlanDetailTable ON ArticleDefTableMaster.ArticleId = PlanDetailTable.ArticleDefId LEFT JOIN ArticleUnitDefTable ON ArticleDefTableMaster.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId where PlanDetailTable.PlanId = " & Me.cmbPlan.SelectedValue & "  Group By ArticleDefTableMaster.ArticleId, ArticleDefTableMaster.ArticleDescription, ArticleDefTableMaster.ArticleCode, PlanDetailTable.PlanDetailId, ArticleUnitDefTable.ArticleUnitName, ArticleDefTableMaster.ProductionProcessId, ArticleDefTableMaster.PackQty, ArticleDefTableMaster.PurchasePrice, ArticleDefTableMaster.SalePrice, ArticleDefTableMaster.SortOrder, PlanDetailTable.LocationId "

                If getConfigValueByType("ItemSortOrder").ToString = "True" Then
                    If getConfigValueByType("ItemAscending").ToString = "True" Then
                        Str += " ORDER BY ArticleDefTableMaster.SortOrder ASC"
                    ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                        Str += " ORDER BY  ArticleDefTableMaster.SortOrder DESC"
                    End If
                ElseIf getConfigValueByType("ItemSortOrderByCode").ToString = "True" Then
                    If getConfigValueByType("ItemAscending").ToString = "True" Then
                        Str += " ORDER BY  ArticleDefTableMaster.ArticleCode ASC"
                    ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                        Str += " ORDER BY  ArticleDefTableMaster.ArticleCode DESC"
                    End If
                ElseIf getConfigValueByType("ItemSortOrderByName").ToString = "True" Then
                    If getConfigValueByType("ItemAscending").ToString = "True" Then
                        Str += " ORDER BY  ArticleDefTableMaster.ArticleDescription ASC"
                    ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                        Str += " ORDER BY  ArticleDefTableMaster.ArticleDescription DESC"
                    End If

                End If
                FillUltraDropDown(Me.cmbTicketProduct, Str)
                Me.cmbTicketProduct.Rows(0).Activate()
                If Me.cmbTicketProduct.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbTicketProduct.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbTicketProduct.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                    Me.cmbTicketProduct.DisplayLayout.Bands(0).Columns("ProductionProcessId").Hidden = True
                    Me.cmbTicketProduct.DisplayLayout.Bands(0).Columns("PlanDetailID").Hidden = True

                End If
                'Me.rbCode.Checked = True


            ElseIf Condition = "BulkChildProduct" Then
                Str = "Select ArticleId as Id, ArticleDescription as Item, ArticleCode as Code, ArticleUnitDefTable.ArticleUnitName As Unit, Isnull(PackQty,0) as [Pack Qty], IsNull(PurchasePrice,0) as [Pur Price], IsNull(SalePrice,0) as [Sale Price], IsNull(ArticleDefTableMaster.ProductionProcessId, 0) AS ProductionProcessId, 0 As PlanDetailId, 0 As Qty, ArticleDefTableMaster.SortOrder, 0 As LocationId From ArticleDefTableMaster INNER JOIN PlanTicketsMaster AS Ticket ON ArticleDefTableMaster.ArticleId = Ticket.MasterArticleId LEFT JOIN ArticleUnitDefTable ON ArticleDefTableMaster.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId where Ticket.PlanID = " & Me.cmbPlan.SelectedValue & " "

                If getConfigValueByType("ItemSortOrder").ToString = "True" Then
                    If getConfigValueByType("ItemAscending").ToString = "True" Then
                        Str += " ORDER BY ArticleDefTableMaster.SortOrder ASC"
                    ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                        Str += " ORDER BY  ArticleDefTableMaster.SortOrder DESC"
                    End If
                ElseIf getConfigValueByType("ItemSortOrderByCode").ToString = "True" Then
                    If getConfigValueByType("ItemAscending").ToString = "True" Then
                        Str += " ORDER BY  ArticleDefTableMaster.ArticleCode ASC"
                    ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                        Str += " ORDER BY  ArticleDefTableMaster.ArticleCode DESC"
                    End If
                ElseIf getConfigValueByType("ItemSortOrderByName").ToString = "True" Then
                    If getConfigValueByType("ItemAscending").ToString = "True" Then
                        Str += " ORDER BY  ArticleDefTableMaster.ArticleDescription ASC"
                    ElseIf getConfigValueByType("ItemDescending").ToString = "True" Then
                        Str += " ORDER BY  ArticleDefTableMaster.ArticleDescription DESC"
                    End If

                End If
                FillUltraDropDown(Me.cmbTicketProduct, Str)
                Me.cmbTicketProduct.Rows(0).Activate()
                If Me.cmbTicketProduct.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbTicketProduct.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbTicketProduct.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                    Me.cmbTicketProduct.DisplayLayout.Bands(0).Columns("ProductionProcessId").Hidden = True
                    Me.cmbTicketProduct.DisplayLayout.Bands(0).Columns("PlanDetailID").Hidden = True

                End If

            ElseIf Condition = "Stages" Then
                If Not Me.cmbTicketProduct.ActiveRow Is Nothing Then
                    FillDropDown(Me.cmbProductionStep, "Select ProdStep_Id AS ProductionStepId, prod_step AS [Production Step], prod_Less from tblProSteps AS ProductionStep INNER JOIN ProductionProcessDetail AS ProcessDetail ON ProcessDetail.ProductionStepId = ProductionStep.ProdStep_Id WHERE ProcessDetail.ProductionProcessId =" & Me.cmbTicketProduct.ActiveRow.Cells("ProductionProcessId").Value & "  ORDER BY ProcessDetail.SortOrder ASC")
                End If
                'ElseIf Condition = "grdDetailArticle" Then
                '    Dim dtDetailArticle As DataTable = GetDataTable("Select ArticleId, ArticleDescription From ArticleDefTable WHERE MasterId = " & Me.cmbMasterItem.Value & "")
                '    dtDetailArticle.AcceptChanges()
                '    Me.grdDetail.RootTable.Columns("DetailArticleId").HasValueList = True
                '    Me.grdDetail.RootTable.Columns("DetailArticleId").EditType = Janus.Windows.GridEX.EditType.Combo
                '    Me.grdDetail.RootTable.Columns("DetailArticleId").ValueList.PopulateValueList(dtDetailArticle.DefaultView, "ArticleId", "ArticleDescription")
            ElseIf Condition = "grdPacking" Then
                'ArticleDefPackTable (ArticleMasterId,PackName,PackQty,PackRate
                Dim dtPacking As DataTable = GetDataTable("Select ArticlePackId, PackName From ArticleDefPackTable WHERE ArticleMasterId = " & Me.cmbTicketProduct.Value & " Union Select 0 AS ArticlePackId, 'N/A' AS PackName ")
                dtPacking.AcceptChanges()
                'Me.grdDetail.RootTable.Columns("PackingId").HasValueList = True
                'Me.grdDetail.RootTable.Columns("PackingId").EditType = Janus.Windows.GridEX.EditType.Combo
                Me.grdDetail.RootTable.Columns("PackingId").ValueList.PopulateValueList(dtPacking.DefaultView, "ArticlePackId", "PackName")
            ElseIf Condition = "QCIncharge" Then
                Str = "SELECT dbo.tblDefEmployee.Employee_ID, dbo.tblDefEmployee.Employee_Name, dbo.tblDefEmployee.Father_Name, dbo.EmployeeDesignationDefTable.EmployeeDesignationName, " _
           & "                EmployeeDeptDefTable.EmployeeDeptName, tblDefEmployee.Salary " _
           & "              FROM tblDefEmployee INNER JOIN " _
           & "         EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId INNER JOIN " _
           & "         EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId where tblDefEmployee.active = 1 "
                FillDropDown(cmbQCIncharge, Str)


            ElseIf Condition = "QAIncharge" Then
                Str = "SELECT dbo.tblDefEmployee.Employee_ID, dbo.tblDefEmployee.Employee_Name, dbo.tblDefEmployee.Father_Name, dbo.EmployeeDesignationDefTable.EmployeeDesignationName, " _
           & "                EmployeeDeptDefTable.EmployeeDeptName, tblDefEmployee.Salary " _
           & "              FROM tblDefEmployee INNER JOIN " _
           & "         EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId INNER JOIN " _
           & "         EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId where tblDefEmployee.active = 1 "
                FillDropDown(cmbQAIncharge, Str)


            ElseIf Condition = "Section" Then
                Str = "select distinct section , section from ProductionTicketStages where section <> '' "
                FillDropDown(cmbSection, Str)

            ElseIf Condition = "Packing" Then
                Str = "Select ArticlePackId, PackName From ArticleDefPackTable WHERE ArticleMasterId = " & Me.cmbTicketProduct.Value & ""
                FillDropDown(cmbPacking, Str)


            ElseIf Condition = "ProductionIncharge" Then
                Str = "SELECT dbo.tblDefEmployee.Employee_ID, dbo.tblDefEmployee.Employee_Name, dbo.tblDefEmployee.Father_Name, dbo.EmployeeDesignationDefTable.EmployeeDesignationName, " _
           & "    EmployeeDeptDefTable.EmployeeDeptName, tblDefEmployee.Salary " _
           & "    FROM tblDefEmployee INNER JOIN " _
           & "         EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId INNER JOIN " _
           & "         EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId where tblDefEmployee.active = 1 "
                FillDropDown(cmbProductionIncharge, Str)
            ElseIf Condition = "Batches" Then
                If Not Me.cmbTicketProduct.ActiveRow Is Nothing Then
                    FillUltraDropDown(Me.cmbBatches, "SELECT Id, StandardNo, StandardName, Version, BatchSize FROM FinishGoodMaster WHERE MasterArticleId = " & Me.cmbTicketProduct.ActiveRow.Cells("Id").Value & "")
                    Me.cmbBatches.Rows(0).Activate()
                Else
                    FillUltraDropDown(Me.cmbBatches, "SELECT Id, StandardNo, StandardName, Version, BatchSize FROM FinishGoodMaster WHERE MasterArticleId = " & 0 & "")
                    Me.cmbBatches.Rows(0).Activate()
                End If
                'ElseIf Condition = "grdDetailArticle" Then
                '    Dim dtDetailArticle As DataTable = GetDataTable("Select ArticleId, ArticleDescription From ArticleDefTable WHERE MasterId = " & Me.cmbMasterItem.Value & "")
                '    dtDetailArticle.AcceptChanges()
                '    Me.grdDetail.RootTable.Columns("DetailArticleId").HasValueList = True
                '    Me.grdDetail.RootTable.Columns("DetailArticleId").EditType = Janus.Windows.GridEX.EditType.Combo
                '    Me.grdDetail.RootTable.Columns("DetailArticleId").ValueList.PopulateValueList(dtDetailArticle.DefaultView, "ArticleId", "ArticleDescription")
            ElseIf Condition = "Departments" Then
                FillDropDown(Me.cmbSubDepartment, "Select ProdStep_Id AS ProductionStepId, prod_step AS [Production Step], prod_Less from tblProSteps WHERE Active = 1  ORDER BY tblProSteps.Sort_Order ASC")
            ElseIf Condition = "Location" Then
                Str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                         & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Location_Id = " & Me.cmbTicketProduct.ActiveRow.Cells("LocationId").Value & " order by sort_order " _
                         & " Else " _
                         & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation WHERE Location_Id = " & Me.cmbTicketProduct.ActiveRow.Cells("LocationId").Value & " order by sort_order"
                FillDropDown(cmbLocation, Str, False)
            ElseIf Condition = "grdLocation" Then
                Str = " If exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                     & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                     & " Else " _
                     & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                Dim dt As DataTable = GetDataTable(Str)
                Me.grdDetail.RootTable.Columns("LocationId").ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Dim dt As New DataTable
        Try
            PlanTicketsMaster = New PlanTicketsMaster()
            PlanTicketsMaster.PlanTicketsMasterID = PlanTicketsMasterID
            PlanTicketsMaster.TicketNo = Me.txtTicketNo.Text
            PlanTicketsMaster.ParentTicketNo = ParentTicketNo
            PlanTicketsMaster.TicketDate = Me.dtpDate.Value
            PlanTicketsMaster.CustomerID = Me.cmbCustomer.Value
            PlanTicketsMaster.SalesOrderID = Me.cmbSalesOrder.SelectedValue
            PlanTicketsMaster.PlanID = Me.cmbPlan.SelectedValue
            PlanTicketsMaster.SpecialInstructions = Me.txtSpecialInstructions.Text
            PlanTicketsMaster.ExpiryDate = Me.dtpExpiryDate.Value
            PlanTicketsMaster.BatchNo = Me.txtBatchNo.Text
            'PlanTicketsMaster.BatchSize = Val(Me.txtBatchSize.Text)
            PlanTicketsMaster.BatchSize = Val(Me.txtBatchSize.Text)

            'Task 3551 Saad Afzaal set MasterArticleId Value when PlanTicketStandard from open as popup

            If Me.MasterArticleId > 0 Then

                PlanTicketsMaster.MasterArticleId = Me.MasterArticleId

            Else

                PlanTicketsMaster.MasterArticleId = Me.cmbTicketProduct.Value

            End If
            If Me.cmbBatches.ActiveRow IsNot Nothing Then
                PlanTicketsMaster.FinishGoodId = Me.cmbBatches.ActiveRow.Cells("Id").Value
            Else
                PlanTicketsMaster.FinishGoodId = 0
            End If
            PlanTicketsMaster.NoOfBatches = Val(Me.txtNoOfBatches.Text)
            'PlanTicketsMaster. = Me.txtSpecialInstructions.Text
            For i As Integer = 0 To Me.grdDetail.RowCount - 1
                PlanTicketsDetail = New PlanTicketsDetail()
                PlanTicketsDetail.LocationId = Me.grdDetail.GetRows(i).Cells("LocationId").Value
                PlanTicketsDetail.PlanTicketsDetailID = Me.grdDetail.GetRows(i).Cells("PlanTicketsDetailID").Value
                PlanTicketsDetail.PlanTicketsMasterID = Me.grdDetail.GetRows(i).Cells("PlanTicketsMasterID").Value
                PlanTicketsDetail.ArticleId = Me.grdDetail.GetRows(i).Cells("ArticleId").Value
                PlanTicketsDetail.PlanDetailId = Me.grdDetail.GetRows(i).Cells("PlanDetailId").Value
                PlanTicketsDetail.Quantity = Me.grdDetail.GetRows(i).Cells("Quantity").Value
                PlanTicketsDetail.PackingId = Me.grdDetail.GetRows(i).Cells("PackingId").Value
                PlanTicketsMaster.Detail.Add(PlanTicketsDetail)
            Next
            For i As Integer = 0 To Me.grdStages.RowCount - 1
                Dim ProductionStage As New BEProductionTicketStages()
                ProductionStage.Id = Me.grdStages.GetRows(i).Cells("Id").Value
                ProductionStage.TicketId = Me.grdStages.GetRows(i).Cells("TicketId").Value
                ProductionStage.ProductionStageId = Me.grdStages.GetRows(i).Cells("ProductionStageId").Value
                ProductionStage.StageDate = Me.grdStages.GetRows(i).Cells("StageDate").Value
                ProductionStage.Section = Me.grdStages.GetRows(i).Cells("Section").Value
                ProductionStage.ProductionInchargeId = Me.grdStages.GetRows(i).Cells("ProductionInchargeId").Value
                ProductionStage.QCInchargeId = Me.grdStages.GetRows(i).Cells("QCInchargeId").Value
                ProductionStage.QAInchargeId = Me.grdStages.GetRows(i).Cells("QAInchargeId").Value
                PlanTicketsMaster.StagesList.Add(ProductionStage)
            Next
            For i As Integer = 0 To Me.grdMaterialEstimation.RowCount - 1
                Dim MaterialDetail As New BEPlanTicketMaterialDetail()
                MaterialDetail.Id = Me.grdMaterialEstimation.GetRows(i).Cells("Id").Value
                MaterialDetail.TicketId = PlanTicketsMasterID
                MaterialDetail.CostPrice = Me.grdMaterialEstimation.GetRows(i).Cells("CostPrice").Value
                MaterialDetail.DepartmentId = Me.grdMaterialEstimation.GetRows(i).Cells("DepartmentId").Value
                'MaterialDetail.FinishGoodId = Me.cmbBatches.ActiveRow.Cells("Id").Value
                MaterialDetail.MaterialArticleId = Me.grdMaterialEstimation.GetRows(i).Cells("MaterialArticleId").Value
                MaterialDetail.Qty = Me.grdMaterialEstimation.GetRows(i).Cells("Qty").Value
                MaterialDetail.Type = Me.grdMaterialEstimation.GetRows(i).Cells("Type").Value.ToString
                MaterialDetail.FinishGoodDetailId = Val(Me.grdMaterialEstimation.GetRows(i).Cells("FinishGoodDetailId").Value.ToString)
                PlanTicketsMaster.MaterialDetail.Add(MaterialDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdMaster.DataSource = PlanTicketsStandardDAL.GetAll()
            Me.grdMaster.RetrieveStructure()
            Me.grdMaster.RootTable.Columns(HistoryGrid.TicketId).Visible = False
            Me.grdMaster.RootTable.Columns(HistoryGrid.CustomerId).Visible = False
            Me.grdMaster.RootTable.Columns(HistoryGrid.SalesOrderId).Visible = False
            Me.grdMaster.RootTable.Columns(HistoryGrid.PlanId).Visible = False
            Me.grdMaster.RootTable.Columns(HistoryGrid.MasterArticleId).Visible = False
            Me.grdMaster.RootTable.Columns(HistoryGrid.FinishGoodId).Visible = False
            Me.grdMaster.RootTable.Columns(HistoryGrid.ParentTicketNo).Visible = False
            Me.grdMaster.RootTable.Columns(HistoryGrid.TicketDate).FormatString = str_DisplayDateFormat
            Me.grdMaster.RootTable.Columns(HistoryGrid.ExpiryDate).FormatString = str_DisplayDateFormat
            'Me.grdMaster.RootTable.Columns("TicketNo").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grdMaster.RootTable.Columns("TicketDate").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grdMaster.RootTable.Columns("SpecialInstructions").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtTicketNo.Text = "" Then
                msg_Error("Ticket number is required")
                Me.txtTicketNo.Focus() : IsValidate = False : Exit Function
            ElseIf Me.cmbPlan.SelectedIndex <= 0 Then
                msg_Error("Plan is required")
                Me.cmbPlan.Focus() : IsValidate = False : Exit Function
            ElseIf Not Me.grdDetail.RowCount > 0 Then
                msg_Error("Grid is empty. One or more rows are required")
                Me.grdDetail.Focus() : IsValidate = False : Exit Function
            ElseIf Val(Me.txtNQP.Text) < 1 Then
                msg_Error("NQP Quantity is required")
                Me.txtNQP.Focus() : IsValidate = False : Exit Function
            ElseIf Me.txtBatchNo.Text = String.Empty Then
                msg_Error("Batch No is required")
                Me.txtBatchNo.Focus() : IsValidate = False : Exit Function
            Else
                IsValidate = True
            End If
            FillModel()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtTicketNo.Text = String.Empty
            Me.dtpDate.Value = Date.Now
            Me.txtSpecialInstructions.Text = String.Empty
            Me.txtBatchSize.Text = String.Empty
            Me.txtNoOfBatches.Text = String.Empty
            Me.txtNQP.Text = String.Empty
            Me.txtAvailQty.Text = String.Empty
            Me.txtBatchNo.Text = String.Empty
            ParentTicketNo = String.Empty
            PlanDetailId = 0
            IsEditMode = False
            If cmbCustomer.DisplayLayout.Bands.Count > 0 Then
                Me.cmbCustomer.Rows(0).Activate()
            End If
            FillCombos("SalesOrders")
            FillCombos("Plans")
            FillCombos("Departments")
            'Batches
            ''TASK TFS4782
            FillCombos("Batches")
            '' END TASK TFS4782
            'FillCombos("TicketProduct")
            'Me.cmbSalesOrder.Items.
            If Not Me.cmbSalesOrder.SelectedIndex = -1 Then
                Me.cmbSalesOrder.SelectedIndex = 0
            End If
            If Not Me.cmbPlan.SelectedIndex = -1 Then
                Me.cmbPlan.SelectedIndex = 0
            End If
            If Not cmbItem.SelectedRow Is Nothing Then
                Me.cmbItem.Rows(0).Activate()
            End If
            If Not cmbTicketProduct.SelectedRow Is Nothing Then
                Me.cmbTicketProduct.Rows(0).Activate()
            End If
            If Not cmbBatches.SelectedRow Is Nothing Then
                Me.cmbBatches.Rows(0).Activate()
            End If
            Me.BtnSave.Text = "&Save"
            Me.BtnDelete.Visible = False
            Me.txtTicketQty.Text = ""
            Me.txtAvailQty.Text = ""
            ResetDetailControls()
            GetAllRecords()
            GetDetail(-1)
            GetStages(-1)
            DisplayMaterialDetail(-1)
            GetSecurityRights()
            IsBulkChildItem = False
            ' CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            PlanTicketsStandardDAL.Save(PlanTicketsMaster)
            SaveActivityLog("Production", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtTicketNo.Text, True)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            PlanTicketsStandardDAL.Update(PlanTicketsMaster)
            SaveActivityLog("Production", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtTicketNo.Text, True)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub GetDetail(ByVal MasterID As Integer)
        Try
            Me.grdDetail.DataSource = PlanTicketsStandardDAL.GetDetail(MasterID)
            Me.grdDetail.RootTable.Columns("Quantity").FormatString = "N" & DecimalPointInQty
            FillCombos("grdLocation")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ResetDetailControls()
        Try
            Me.txtTicketQty.Text = ""

        Catch ex As Exception
        End Try
    End Sub


    Private Sub cmbCustomer_ValueChanged(sender As Object, e As EventArgs) Handles cmbCustomer.ValueChanged
        Try
            If Me.cmbCustomer.Value > 0 Then
                FillCombos("SalesOrder")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSalesOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSalesOrder.SelectedIndexChanged
        Try
            If Me.cmbSalesOrder.SelectedValue > 0 Then
                FillCombos("Plan")
            Else
                FillCombos("Plans")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            'Task 3551 Saad Afzaal set Ticket_Id and MasterArticleId value 0 when click on New Button 
            Ticket_Id = 0
            MasterArticleId = 0
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() Then
                If Me.BtnSave.Text = "&Save" Then
                    If Save() Then
                        msg_Information("Record has been saved successfully.")
                        ReSetControls()
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() Then
                        msg_Information("Record has been updated successfully.")
                        ReSetControls()
                        'Task 3551 Saad Afzaal set Ticket_Id and MasterArticleId value 0 at end of update function execute successfully 
                        Ticket_Id = 0
                        MasterArticleId = 0
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() Then
                msg_Information("Record has been deleted successfully.")
            End If
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub EditRecord()
        Try
            IsEditMode = True
            If Not Me.grdMaster.RowCount > 0 Then Exit Sub
            If Me.grdDetail.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.txtTicketNo.Text = Me.grdMaster.CurrentRow.Cells(HistoryGrid.TicketNo).Value.ToString
            ParentTicketNo = Me.grdMaster.CurrentRow.Cells(HistoryGrid.ParentTicketNo).Value.ToString
            If ParentTicketNo.Length > 0 Then
                IsBulkChildItem = True
            Else
                IsBulkChildItem = False
            End If
            Me.dtpDate.Value = Me.grdMaster.CurrentRow.Cells(HistoryGrid.TicketDate).Value
            PlanTicketsMasterID = Me.grdMaster.CurrentRow.Cells(HistoryGrid.TicketId).Value
            Me.cmbCustomer.Value = Me.grdMaster.CurrentRow.Cells(HistoryGrid.CustomerId).Value

            Me.cmbSalesOrder.SelectedValue = Me.grdMaster.CurrentRow.Cells(HistoryGrid.SalesOrderId).Value


            Me.cmbPlan.SelectedValue = Me.grdMaster.CurrentRow.Cells(HistoryGrid.PlanId).Value

            'Task 3551 Saad Afzaal check that if ParentTicketNo is empty then set the cmbTicketProduct value else set save MasterArticleId in local class variable 
            ''Below four lines are commented on 28-06-2018
            'If ParentTicketNo = String.Empty Then
            '    Me.cmbTicketProduct.Value = Me.grdMaster.CurrentRow.Cells(HistoryGrid.MasterArticleId).Value
            'Else
            '    MasterArticleId = Me.grdMaster.CurrentRow.Cells(HistoryGrid.MasterArticleId).Value
            'End If
            Me.cmbTicketProduct.Value = Me.grdMaster.CurrentRow.Cells(HistoryGrid.MasterArticleId).Value

            Me.txtSpecialInstructions.Text = Me.grdMaster.CurrentRow.Cells(HistoryGrid.Remarks).Value.ToString()
            Me.txtBatchNo.Text = Me.grdMaster.CurrentRow.Cells(HistoryGrid.BatchNo).Value.ToString()
            Me.txtBatchSize.Text = Val(Me.grdMaster.CurrentRow.Cells(HistoryGrid.BatchSize).Value.ToString)
            Me.txtNoOfBatches.Text = Val(Me.grdMaster.CurrentRow.Cells(HistoryGrid.NoOfBatches).Value.ToString)

            If IsDBNull(Me.grdMaster.CurrentRow.Cells(HistoryGrid.ExpiryDate).Value) Then
                Me.dtpExpiryDate.Value = Now
            Else
                Me.dtpExpiryDate.Value = Me.grdMaster.CurrentRow.Cells(HistoryGrid.ExpiryDate).Value
            End If

            FillCombos("Item")
            FillCombos("Stages")
            FillCombos("grdPacking")
            FillCombos("Batches")
            Me.cmbBatches.Value = Me.grdMaster.CurrentRow.Cells(HistoryGrid.FinishGoodId).Value
            Me.GetDetail(PlanTicketsMasterID)
            Me.GetStages(PlanTicketsMasterID)
            DisplayMaterialDetail(PlanTicketsMasterID)

            'Me.txtNQP.Text = Val(Me.grdMaster.CurrentRow.Cells(HistoryGrid.BatchSize).Value.ToString)

            Me.BtnSave.Text = "&Update"
            Me.BtnDelete.Visible = True
            'FillCombos("TicketProduct")
            Me.txtTicketQty.Text = ""
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab

            If PlanTicketsStandardDAL.CheckTicket(PlanTicketsMasterID) = True Then
                txtBatchNo.ReadOnly = True
            Else
                txtBatchNo.ReadOnly = False

            End If
            ' CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
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
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
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
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub cmbPlan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlan.SelectedIndexChanged
        'If Me.IsEditMode = True Then Exit Sub
        Try

            If Me.cmbPlan.SelectedValue > 0 Then
                Dim PlanNo As String = CType(Me.cmbPlan.SelectedItem, DataRowView).Item("UsedForTicket").ToString
                If Ticket_Id < 1 AndAlso Me.IsEditMode = False Then
                    Me.txtTicketNo.Text = GetNextTicket(PlanNo)
                End If
                'Me.txtBatchNo.Text = cmbPlan.Text
                Me.txtBatchNo.Text = Me.txtTicketNo.Text
            End If
            If IsBulkChildItem = False Then
                FillCombos("TicketProduct")
            Else
                FillCombos("BulkChildProduct")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicketProduct_ValueChanged(sender As Object, e As EventArgs) Handles cmbTicketProduct.ValueChanged
        Try
            If Me.cmbTicketProduct.Value > 0 Then
                PlanDetailId = Me.cmbTicketProduct.SelectedRow.Cells("PlanDetailId").Value
                IssuedQty = Me.cmbTicketProduct.SelectedRow.Cells("Qty").Value
                Me.txtAvailQty.Text = IssuedQty
                FillCombos("Item")
                FillCombos("Stages")
                FillCombos("grdPacking")
                FillCombos("Location")
                'GetMaterialDetail(Me.cmbTicketProduct.Value)
                FillCombos("Packing")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTicketProduct_Leave(sender As Object, e As EventArgs) Handles cmbTicketProduct.Leave
        Try
            If Me.cmbTicketProduct.Value > 0 Then
                'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbItem.SelectedRow.Cells("Id").Value)
                'Dim GridQty As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Quantity"), Janus.Windows.GridEX.AggregateFunction.Sum)
                PlanDetailId = Me.cmbTicketProduct.SelectedRow.Cells("PlanDetailId").Value
                'Qty = Val(Me.cmbItem.SelectedRow.Cells("Qty").Value.ToString)
                'IssuedQty = GetTicketIssuedQty(PlanDetailId)
                IssuedQty = Me.cmbTicketProduct.SelectedRow.Cells("Qty").Value
                'Dim DifInIssuedAndNewGridQty As Double = (GridQty - IssuedQty)
                'If DifInIssuedAndNewGridQty > 0 Then
                '    RemainingQty = Qty - (IssuedQty + DifInIssuedAndNewGridQty)
                'Else
                '    RemainingQty = Qty - IssuedQty
                'End If
                Me.txtAvailQty.Text = IssuedQty
                FillCombos("Item")
                FillCombos("Stages")
                FillCombos("grdPacking")
                FillCombos("Batches")
                FillCombos("Departments")
                FillCombos("Location")
                'GetMaterialDetail(Me.cmbTicketProduct.Value)
            End If
            'Me.txtAvailableQty.Text = Val(Me.cmbTicketProduct.SelectedRow.Cells("RemainingQty").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            AddToGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub AddToGrid()
        Dim dt As New DataTable
        Try

            If Me.cmbItem.Value <= 0 Then
                msg_Error("Please select a product")
                Me.cmbItem.Focus()
                Exit Sub
            ElseIf Val(Me.txtTicketQty.Text) <= 0 Then
                msg_Error("Quantity is required larger than zero")
                Me.txtTicketQty.Focus()
                Exit Sub
                'ElseIf Val(Me.txtTicketQty.Text) > Val(Me.txtAvailableQty.Text) Then
                '    msg_Error("Quantity should be less than available quantity")
                '    Me.txtAvailableQty.Focus()
                '    Exit Sub
            End If

            ''TASK TFS3998
            'If Me.grdMaterialEstimation.RowCount > 0 Then
            'Dim dtDetailArticle As DataTable = PlanTicketsStandardDAL.IsThisItemPartOfEstimation(Me.cmbTicketProduct.Value, Me.cmbBatches.ActiveRow.Cells("Version").Value, Me.cmbItem.Value, Me.cmbPacking.SelectedValue, Val(Me.txtTicketQty.Text))
            Dim dtDetailArticle As DataTable = PlanTicketsStandardDAL.IsThisItemPartOfEstimation(Me.cmbTicketProduct.Value, Me.cmbBatches.ActiveRow.Cells("Version").Value, Me.cmbItem.Value, Me.cmbPacking.SelectedValue, Val(Me.txtNoOfBatches.Text))

            If dtDetailArticle.Rows.Count > 0 Then
                If msg_Confirm("This item is associated with finish good. Do you want to add it to estimation?") = False Then Exit Sub
                Dim dtEstimation As DataTable = CType(Me.grdMaterialEstimation.DataSource, DataTable)
                dtEstimation.ImportRow(dtDetailArticle.Rows(0))
            End If
            'End If
            ''End TASK TFS3998
            Dim Qty As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Quantity"), Janus.Windows.GridEX.AggregateFunction.Sum)
            Qty += Val(Me.txtTicketQty.Text)

            If Qty > Val(txtNQP.Text) Then
                msg_Error("Quantity is exceeded than Net Quantity")
                Me.txtTicketQty.Focus()
                Exit Sub
            End If
            dt = CType(Me.grdDetail.DataSource, DataTable)
            Dim dr1() As DataRow = dt.Select("ArticleId=" & Me.cmbItem.Value & " AND PackingId =" & Me.cmbPacking.SelectedValue & "")
            If dr1.Length > 0 Then
                ShowErrorMessage("Selected item already exists in the GRID.")
                Me.cmbProductionStep.Focus()
                Exit Sub
            End If
            Dim dr As DataRow
            dr = dt.NewRow
            dr("LocationId") = Me.cmbLocation.SelectedValue
            dr("PlanTicketsDetailID") = 0
            dr("PlanTicketsMasterID") = 0
            dr("ArticleId") = Me.cmbItem.Value
            'dr("ArticleId") = Val(Me.cmbTicketProduct.SelectedRow.Cells("MasterId").Value.ToString)
            dr("ArticleCode") = Me.cmbItem.SelectedRow.Cells("Code").Value.ToString
            dr("ArticleDescription") = Me.cmbItem.SelectedRow.Cells("Item").Value.ToString
            dr("UnitName") = Me.cmbItem.SelectedRow.Cells("UnitName").Value.ToString
            'dr("PlanDetailId") = Me.cmbItem.SelectedRow.Cells("PlanDetailId").Value.ToString
            dr("PlanDetailId") = PlanDetailId
            dr("Quantity") = Val(Me.txtTicketQty.Text)
            dr("PackingId") = cmbPacking.SelectedValue
            dt.Rows.Add(dr)
            'Me.txtAvailableQty.Text -= Val(Me.txtTicketQty.Text)
            ResetDetailControls()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function CreateTicketNo() As String
        Dim ticketNo As String = String.Empty
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                ticketNo = GetSerialNo("TK" + "-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "PlanTicketsMaster", "TicketNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                ticketNo = GetNextDocNo("TK" & "-" & Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "PlanTicketsMaster", "TicketNo")
            Else
                ticketNo = GetNextDocNo("TK", 6, "PlanTicketsMaster", "TicketNo")
            End If
            Return ticketNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetNextTicket(ByVal PlanNo As String) As String
        Try
            Dim str As String = 0
            Dim strSql As String
            strSql = "select IsNull(Max(Convert(Integer, Right(TicketNo, CHARINDEX('-', REVERSE('-' + TicketNo)) - 1))), 0) from PlanTicketsMaster Where PlanId = " & Me.cmbPlan.SelectedValue & "" ' "
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


    Private Sub grdMaster_DoubleClick(sender As Object, e As EventArgs) Handles grdMaster.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Try
            Dim PlanId As Integer = 0
            Dim PlansId As Integer = 0

            PlanId = cmbPlan.SelectedValue
            PlansId = cmbPlan.SelectedValue

            If cmbSalesOrder.SelectedValue > 0 Then

                FillCombos("Plan")
                cmbPlan.SelectedValue = PlanId

            Else

                FillCombos("Plans")
                cmbPlan.SelectedValue = PlansId

                If cmbPlan.SelectedValue = Nothing Then

                    cmbPlan.SelectedValue = 0

                End If

            End If

            FillCombos("Section")

            'frmPlanTickets_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Function GetTicketIssuedQty(ByVal PlanDetailId As Integer) As Double
        Dim dt As New DataTable
        Dim Str As String = String.Empty
        Try
            Str = "Select PlanDetailId, Sum(IsNull(TicketIssuedQty, 0)) As TicketIssuedQty FROM PlanDetailTable Where PlanDetailId = " & PlanDetailId & " Group By PlanDetailId"
            dt = GetDataTable(Str)
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows(0).Item(1).ToString)
            Else
                Return Val(0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub QuantityCalculation()
        Try
            'Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.AllocationGrid.RootTable.Columns("ProductID"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbProduct.Value)
            'Dim GridQty As Double = Me.AllocationGrid.GetTotal(Me.AllocationGrid.RootTable.Columns("Quantity"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
            'RemainingQty = Val(Me.cmbProduct.SelectedRow.Cells("RemainingQty").Value.ToString)
            'Dim Quantity As Double = Val(Me.cmbProduct.SelectedRow.Cells("Quantity").Value.ToString)
            'Dim AllocatedQty As Double = Quantity - RemainingQty
            'Dim TotalAllocatedQty As Double = AllocatedQty + GridQty
            'If TotalAllocatedQty > 0 Then
            '    Me.txtRemainingQty.Text = Quantity - TotalAllocatedQty
            'ElseIf Not GridQty > 0 Then
            '    Me.txtRemainingQty.Text = RemainingQty
            'Else
            '    Me.txtRemainingQty.Text = Quantity
            '    'Me.txtRemainingQty.Text = RemainingQty.ToString
            'End If



            Me.grdDetail.UpdateData()
            Dim GridexFilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("ArticleId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbItem.SelectedRow.Cells("ArticleId").Value)
            Dim GridQty As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum, GridexFilterCondition)
            RemainingQty = Val(Me.cmbItem.SelectedRow.Cells("RemainingQty").Value.ToString)
            Dim Quantity As Double = Val(Me.cmbItem.SelectedRow.Cells("Quantity").Value.ToString)
            Dim AllocatedQty As Double = Quantity - RemainingQty
            Dim TotalAllocatedQty = AllocatedQty + GridQty
            'If TotalAllocatedQty > 0 Then
            '    Me.txtAvailableQty.Text = Quantity - TotalAllocatedQty
            'ElseIf Not GridQty > 0 Then
            '    Me.txtAvailableQty.Text = RemainingQty
            'Else
            '    Me.txtAvailableQty.Text = Quantity
            '    'Me.txtReqQty.Text = RemainingQty
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTicketQty_KeyPress(sender As Object, e As KeyPressEventArgs)
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs)
        Try
            AddRptParam("@PlanTicketsMasterID", PlanTicketsMasterID)
            ShowReport("rptPlanTicket")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbCode_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If Not Me.cmbItem.ActiveRow Is Nothing AndAlso Me.cmbItem.Rows.Count > 1 Then
                If Me.rbCode.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbName_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If Not Me.cmbItem.ActiveRow Is Nothing AndAlso Me.cmbItem.Rows.Count > 1 Then
                If Me.rbName.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnAddTicketStages_Click(sender As Object, e As EventArgs) Handles btnAddTicketStages.Click
        Try
            If ValidateAddToPOHGrid() Then
                AddToStagesGrid()
                ResetStageControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function ValidateAddToPOHGrid() As Boolean
        Try
            If Me.cmbTicketProduct.Value < 1 Then
                ShowErrorMessage("Please select Master Item.")
                Me.cmbTicketProduct.Focus()
                Return False
            End If
            If Me.cmbProductionStep.SelectedIndex < 1 Then
                ShowErrorMessage("Please select Production Step.")
                Me.cmbProductionStep.Focus()

                Return False
            End If
            'If Me.cmbAccount.SelectedIndex < 1 Then
            '    ShowErrorMessage("Please select Account.")
            '    Me.cmbAccount.Focus()
            '    Return False
            'End If

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
    Private Sub AddToStagesGrid()
        Try
            Dim dtStages As DataTable = CType(Me.grdStages.DataSource, DataTable)
            Dim dr() As DataRow = dtStages.Select("ProductionStageId=" & Me.cmbProductionStep.SelectedValue & "")
            If dr.Length > 0 Then
                ShowErrorMessage("Selected production step already exists.")
                Me.cmbProductionStep.Focus()
                Exit Sub
            End If
            Dim drStages As DataRow
            drStages = dtStages.NewRow
            drStages("Id") = 0
            drStages("TicketId") = PlanTicketsMasterID
            drStages("ProductionStageId") = Me.cmbProductionStep.SelectedValue
            drStages("ProductionStage") = CType(Me.cmbProductionStep.SelectedItem, DataRowView).Item("Production Step").ToString

            drStages("ProductionInchargeId") = Me.cmbProductionIncharge.SelectedValue


            If Me.cmbProductionIncharge.SelectedValue = 0 Then

                drStages("ProductionIncharge") = String.Empty

            Else

                drStages("ProductionIncharge") = Me.cmbProductionIncharge.Text

            End If


            drStages("QCInchargeId") = Me.cmbQCIncharge.SelectedValue

            If Me.cmbQCIncharge.SelectedValue = 0 Then

                drStages("QCIncharge") = String.Empty

            Else

                drStages("QCIncharge") = Me.cmbQCIncharge.Text

            End If

            drStages("QAInchargeId") = Me.cmbQAIncharge.SelectedValue

            If Me.cmbQAIncharge.SelectedValue = 0 Then

                drStages("QAIncharge") = String.Empty

            Else

                drStages("QAIncharge") = Me.cmbQAIncharge.Text

            End If

            If Me.cmbSection.SelectedIndex = 0 Then

                drStages("Section") = String.Empty

            Else

                drStages("Section") = Me.cmbSection.Text

            End If

            'drStages("Section") = Me.txtSection.Text
            drStages("StageDate") = Me.dtpDate.Value
            dtStages.Rows.Add(drStages)
            '
            ''
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ResetStageControls()
        Try
            If Not cmbProductionIncharge.SelectedIndex = -1 Then
                Me.cmbProductionIncharge.SelectedIndex = 0
            End If
            If Not cmbProductionStep.SelectedIndex = -1 Then
                Me.cmbProductionStep.SelectedIndex = 0
            End If
            If Not cmbQCIncharge.SelectedIndex = -1 Then
                Me.cmbQCIncharge.SelectedIndex = 0
            End If
            If Not cmbQAIncharge.SelectedIndex = -1 Then
                Me.cmbQAIncharge.SelectedIndex = 0
            End If

            Me.cmbSection.SelectedIndex = 0

            'Me.txtSection.Text = String.Empty
            dtpDate.Value = Now

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmPlanTicketStandard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            PlanTicketsDAL = New PlanTicketsStandardDAL()
            FillCombos("Customer")
            'FillCombos("SalesOrders")
            'FillCombos("Plans")
            'FillCombos("TicketProduct")
            FillCombos("QCIncharge")
            FillCombos("QAIncharge")
            FillCombos("ProductionIncharge")
            FillCombos("Section")
            ReSetControls()

            Me.isFormOpen = True

            If Me.Ticket_Id > 0 Then

                'Task 3440 Saad Afzaal call UpdateRecord function to edit record from production control screen to this page as pop-up'

                UpdateRecord()

            End If
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetStages(ByVal TicketId As Integer)
        'Dim dtPacking As DataTable
        Try
            Me.grdStages.DataSource = PlanTicketsStandardDAL.GetStages(TicketId)

            'If Not Me.cmbTicketProduct.ActiveRow Is Nothing Then
            '    dtPacking = GetDataTable("Select ArticlePackId, PackName From ArticleDefPackTable WHERE ArticleMasterId = " & Me.cmbTicketProduct.Value & " Union Select 0 AS ArticlePackId, 'N/A' AS PackName ")
            'Else
            '    dtPacking = GetDataTable("Select ArticlePackId, PackName From ArticleDefPackTable WHERE ArticleMasterId = " & 0 & " Union Select 0 AS ArticlePackId, 'N/A' AS PackName ")
            'End If
            'dtPacking.AcceptChanges()
            'Me.grdDetail.RootTable.Columns("PackingId").HasValueList = True
            'Me.grdDetail.RootTable.Columns("PackingId").EditType = Janus.Windows.GridEX.EditType.Combo
            'Me.grdStages.RootTable.Columns("Id").Visible = False
            Me.grdStages.RootTable.Columns("StageDate").FormatString = str_DisplayDateFormat
            'Me.grdStages.RootTable.Columns("AccountId").Visible = False
            'Me.grdStages.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayMaterialDetail(ByVal TicketId As Integer)
        'Dim dtPacking As DataTable
        Try
            Me.grdMaterialEstimation.DataSource = PlanTicketsStandardDAL.DisplayMaterialDetail(TicketId)
            Me.grdMaterialEstimation.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdMaterialEstimation.RootTable.Columns("CostPrice").FormatString = "N" & DecimalPointInValue
            'CType(Me.grdMaterialEstimation.DataSource, DataTable).Columns("RequiredQty").Expression = "" & Val(Me.txtNoOfBatches.Text) & " * Qty "
            'CType(Me.grdMaterialEstimation.DataSource, DataTable).Columns("Amount").Expression = "RequiredQty*CostPrice"
            Me.grdMaterialEstimation.RootTable.Columns("RequiredQty").FormatString = "N" & DecimalPointInQty
            Me.grdMaterialEstimation.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetMaterialDetail(ByVal ProductId As Integer)
        'Dim dtPacking As DataTable
        Try
            Me.grdMaterialEstimation.DataSource = PlanTicketsStandardDAL.GetMaterialDetail(ProductId)
            Me.grdMaterialEstimation.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdMaterialEstimation.RootTable.Columns("CostPrice").FormatString = "N" & DecimalPointInValue
            'col.Expression = "CONVERT( DoubleCol * 100, System.Int64 ) / 100"
            'CType(Me.grdMaterialEstimation.DataSource, DataTable).Columns("RequiredQty").Expression = "" & Val(Me.txtNoOfBatches.Text) & " * Qty "
            'CType(Me.grdMaterialEstimation.DataSource, DataTable).Columns("Amount").Expression = "RequiredQty*CostPrice"
            Me.grdMaterialEstimation.RootTable.Columns("RequiredQty").FormatString = "N" & DecimalPointInQty
            Me.grdMaterialEstimation.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetMaterialDetail(ByVal ProductId As Integer, ByVal VersionId As Integer, ByVal Batches As Double)
        'Dim dtPacking As DataTable
        Try
            Me.grdMaterialEstimation.DataSource = PlanTicketsStandardDAL.GetMaterialDetail(ProductId, VersionId, Batches)
            Me.grdMaterialEstimation.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdMaterialEstimation.RootTable.Columns("CostPrice").FormatString = "N" & DecimalPointInValue
            'col.Expression = "CONVERT( DoubleCol * 100, System.Int64 ) / 100"
            'CType(Me.grdMaterialEstimation.DataSource, DataTable).Columns("RequiredQty").Expression = "" & Val(Me.txtNoOfBatches.Text) & " * Qty "
            'CType(Me.grdMaterialEstimation.DataSource, DataTable).Columns("Amount").Expression = "RequiredQty*CostPrice"
            Me.grdMaterialEstimation.RootTable.Columns("RequiredQty").FormatString = "N" & DecimalPointInQty
            Me.grdMaterialEstimation.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdStages_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Dim Id = Me.grdStages.GetRow.Cells("Id").Value
                If Id > 0 Then
                    If PlanTicketsStandardDAL.DeleteStage(Id) Then

                    End If
                End If
                msg_Information("Row has been deleted.")
                Me.grdStages.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs)
        Try
            If e.Tab.Index = 0 Then
                Me.BtnNew.Visible = True
                Me.BtnSave.Visible = True
                Me.BtnRefresh.Visible = True
                'Me.CtrlGrdBar1.MyGrid = grdDetail
                Me.CtrlGrdBar1.MyGrid = grdDetail
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    'Me.grd.SaveLayoutFile(fs)
                    Me.grdDetail.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            ElseIf e.Tab.Index = 1 Then
                Me.BtnNew.Visible = False
                Me.BtnSave.Visible = False
                Me.BtnRefresh.Visible = False
                Me.CtrlGrdBar1.MyGrid = grdMaster
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdMaster.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdMaster.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    'Me.grd.SaveLayoutFile(fs)
                    Me.grdMaster.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
                'Me.CtrlGrdBar1.txtGridTitle.Text = "Cost Sheet Detail" & Chr(10) & "" & Me.cmbMasterItem.ActiveRow.Cells("Item").Value.ToString & ""
            End If
            'Me.CtrlGrdBar1.txtGridTitle.Text = "Cost Sheet Detail" & Chr(10) & "" & Me.cmbMasterItem.ActiveRow.Cells("Item").Value.ToString & ""
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtBatchSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBatchSize.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtBatchSize_TextChanged(sender As Object, e As EventArgs) Handles txtBatchSize.TextChanged
        Try
            Me.txtNQP.Text = Val(Me.txtBatchSize.Text) * Val(Me.txtNoOfBatches.Text)
            'Dim dt As DataTable = CType(Me.grdMaterialEstimation.DataSource, DataTable)
            ''CType(Me.grdMaterialEstimation.DataSource, DataTable).Columns("RequiredQty").Expression = "Convert(" & Me.txtNoOfBatches.Text & " * Qty, System.Decimal)"
            'dt.Columns("RequiredQty").Expression = Val(Me.txtNoOfBatches.Text) & "*Qty"
            ''dt.Columns("RequiredQty").Expression = " Convert(Qty * " & CDec(Me.txtNoOfBatches.Text) & ", System.Decimal)"

            'dt.AcceptChanges()
            'Me.grdMaterialEstimation.DataSource = dt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        'Try
        '    If Val(txtBatchSize.Text) > Val(Me.txtAvailQty.Text) Then
        '        ShowErrorMessage("Batch Size should be less than available quantity.")
        '        Me.txtBatchSize.Text = Val(Me.txtAvailQty.Text)
        '        Me.txtBatchSize.Focus()
        '    End If

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtBatchSize_Leave(sender As Object, e As EventArgs) Handles txtBatchSize.Leave
        Try
            '    If Val(txtBatchSize.Text) > Val(Me.txtAvailQty.Text) Then
            '        ShowErrorMessage("Batch Size should be less than available quantity.")
            '        Me.txtBatchSize.Text = Val(Me.txtAvailQty.Text)
            '        Me.txtBatchSize.Focus()
            '    End If
            ''TASK TFS3540  
            If Me.txtNoOfBatches.Text.Length > 0 AndAlso Me.txtBatchSize.Text.Length > 0 AndAlso Me.cmbBatches.Value > 0 Then
                ''Commented below line against TASK TFS4784 ON 11-10-2018
                'Me.GetMaterialDetail(Me.cmbTicketProduct.Value, Me.cmbBatches.ActiveRow.Cells("Version").Value, (Val(Me.txtBatchSize.Text)))
                Me.GetMaterialDetail(Me.cmbTicketProduct.Value, Me.cmbBatches.ActiveRow.Cells("Version").Value, 1)

            End If
            If Me.grdMaterialEstimation.RowCount > 0 AndAlso Val(txtNoOfBatches.Text) > 0 Then
                For Each _Row As Janus.Windows.GridEX.GridEXRow In Me.grdMaterialEstimation.GetRows
                    Me.grdMaterialEstimation.UpdateData()
                    _Row.BeginEdit()
                    _Row.Cells("NoOfBatches").Value = Val(txtNoOfBatches.Text)
                    _Row.EndEdit()
                    Me.grdMaterialEstimation.UpdateData()
                Next
            End If
            ''END TASK TFS3540
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetail_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            If Me.grdDetail.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "Delete" Then
                    If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                    Me.grdDetail.GetRow.Delete()
                    msg_Information("Row has been deleted successfully.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        'Try
        '    If Me.UltraTabControl1.SelectedTab.Index = 0 Then
        '        Me.CtrlGrdBar1.MyGrid = grdDetail
        '        If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name) Then
        '            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
        '            'Me.grd.SaveLayoutFile(fs)
        '            Me.grdDetail.LoadLayoutFile(fs)
        '            fs.Close()
        '            fs.Dispose()
        '        End If
        '    Else
        '        Me.CtrlGrdBar1.MyGrid = grdMaster
        '        If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdMaster.Name) Then
        '            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdMaster.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
        '            'Me.grd.SaveLayoutFile(fs)
        '            Me.grdMaster.LoadLayoutFile(fs)
        '            fs.Close()
        '            fs.Dispose()
        '        End If
        '        'Me.CtrlGrdBar1.txtGridTitle.Text = "Cost Sheet Detail" & Chr(10) & "" & Me.cmbMasterItem.ActiveRow.Cells("Item").Value.ToString & ""
        '    End If
        '    'Me.CtrlGrdBar1.txtGridTitle.Text = "Cost Sheet Detail" & Chr(10) & "" & Me.cmbMasterItem.ActiveRow.Cells("Item").Value.ToString & ""
        '    Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub cmbQCIncharge_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbQCIncharge.SelectedIndexChanged

    End Sub

    Private Sub txtNoOfBatches_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoOfBatches.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbBatches_Leave(sender As Object, e As EventArgs) Handles cmbBatches.Leave
        Try
            If cmbBatches.ActiveRow Is Nothing Then
                'cmbBatches.
                Exit Sub
            End If
            If Me.cmbBatches.Value > 0 Then
                Me.txtBatchSize.Text = cmbBatches.ActiveRow.Cells("BatchSize").Value
                ''TASK TFS3540  
                If Me.txtNoOfBatches.Text.Length > 0 AndAlso Me.txtBatchSize.Text.Length > 0 AndAlso Me.cmbBatches.Value > 0 Then
                    ''Set default value zero against TASK TFS4784 ON 11-10-2018
                    Me.GetMaterialDetail(Me.cmbTicketProduct.Value, Me.cmbBatches.ActiveRow.Cells("Version").Value, 1)
                End If
                If Me.grdMaterialEstimation.RowCount > 0 AndAlso Val(txtNoOfBatches.Text) > 0 Then
                    For Each _Row As Janus.Windows.GridEX.GridEXRow In Me.grdMaterialEstimation.GetRows
                        Me.grdMaterialEstimation.UpdateData()
                        _Row.BeginEdit()
                        _Row.Cells("NoOfBatches").Value = Val(txtNoOfBatches.Text)
                        _Row.EndEdit()
                        Me.grdMaterialEstimation.UpdateData()
                    Next
                End If
                ''END TASK TFS3540
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtNoOfBatches_TextChanged(sender As Object, e As EventArgs) Handles txtNoOfBatches.TextChanged, cmbBatches.TextChanged
        Try
            ''Below lines are commented on 14-06-2018 against TASK TFS3540
            Me.txtNQP.Text = Val(Me.txtBatchSize.Text) * Val(Me.txtNoOfBatches.Text)
            If Me.grdMaterialEstimation.RowCount > 0 Then
                For Each _Row As Janus.Windows.GridEX.GridEXRow In Me.grdMaterialEstimation.GetRows
                    Me.grdMaterialEstimation.UpdateData()
                    _Row.BeginEdit()
                    _Row.Cells("NoOfBatches").Value = Val(txtNoOfBatches.Text)
                    _Row.EndEdit()
                    Me.grdMaterialEstimation.UpdateData()
                Next
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtNoOfBatches_Leave(sender As Object, e As EventArgs) Handles txtNoOfBatches.Leave
        Try
            If (Val(txtBatchSize.Text) * Val(txtNoOfBatches.Text)) > Val(Me.txtAvailQty.Text) Then
                ShowErrorMessage("Net Quantity should be less than available quantity.")
                'Me.txtNQP.Text = String.Empty
                If IsEditMode = False Then
                    txtNoOfBatches.Text = String.Empty
                Else
                    Me.txtNoOfBatches.Text = Val(Me.grdMaster.CurrentRow.Cells(HistoryGrid.NoOfBatches).Value.ToString)
                End If
                Me.txtNoOfBatches.Focus()
                Exit Sub
                'Else

                '    Dim dt As DataTable = CType(Me.grdMaterialEstimation.DataSource, DataTable)
                '    dt.Columns("RequiredQty").Expression = Val(Me.txtNoOfBatches.Text) & "*Qty"
                '    dt.AcceptChanges()
                '    Me.grdMaterialEstimation.DataSource = dt
            End If

            If Val(Me.txtNoOfBatches.Text) > 0 Then
                If Me.txtNoOfBatches.Text.Length > 0 AndAlso Me.txtBatchSize.Text.Length > 0 AndAlso Me.cmbBatches.Value > 0 Then
                    ''Set default value zero against TASK TFS4784 ON 11-10-2018
                    Me.GetMaterialDetail(Me.cmbTicketProduct.Value, Me.cmbBatches.ActiveRow.Cells("Version").Value, 1)
                End If
            End If
            If Me.grdMaterialEstimation.RowCount > 0 Then
                For Each _Row As Janus.Windows.GridEX.GridEXRow In Me.grdMaterialEstimation.GetRows
                    Me.grdMaterialEstimation.UpdateData()
                    _Row.BeginEdit()
                    _Row.Cells("NoOfBatches").Value = Val(txtNoOfBatches.Text)
                    _Row.EndEdit()
                    Me.grdMaterialEstimation.UpdateData()
                Next
            End If
            ''TASK TFS3540  
            'If Me.txtNoOfBatches.Text.Length > 0 AndAlso Me.txtBatchSize.Text.Length > 0 AndAlso Me.cmbBatches.Value > 0 Then
            '    Me.GetMaterialDetail(Me.cmbTicketProduct.Value, Me.cmbBatches.ActiveRow.Cells("Version").Value, (Val(Me.txtBatchSize.Text)))
            'End If
            ''END TASK TFS3540
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdMaterialEstimation_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdMaterialEstimation.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                Dim Id = Me.grdMaterialEstimation.GetRow.Cells("Id").Value
                If Id > 0 Then
                    If PlanTicketsStandardDAL.DeleteMaterialDetail(Id) Then

                    End If
                End If
                msg_Information("Row has been deleted.")
                Me.grdMaterialEstimation.GetRow.Delete()

            ElseIf e.Column.Key = "Add" Then
                Dim frm As New frmAddSub
                frm.Grid.DataSource = Me.grdMaterialEstimation.DataSource
                frm.PlanItemId = Me.cmbTicketProduct.Value.ToString
                frm.RawMaterialId = Me.grdMaterialEstimation.GetRow.Cells("MaterialArticleId").Value
                frm.Type = "Plus"
                frm.ShowDialog()
            ElseIf e.Column.Key = "Sub" Then
                Dim frm As New frmAddSub
                frm.Grid.DataSource = Me.grdMaterialEstimation.DataSource
                frm.PlanItemId = Me.cmbTicketProduct.Value.ToString
                frm.RawMaterialId = Me.grdMaterialEstimation.GetRow.Cells("MaterialArticleId").Value

                frm.Type = "Minus"
                frm.ShowDialog()
            ElseIf e.Column.Key = "Addnew" Then
                Dim frm As New frmAddSub
                frm.Grid.DataSource = Me.grdMaterialEstimation.DataSource
                frm.PlanItemId = Me.cmbTicketProduct.Value.ToString
                frm.RawMaterialId = Me.grdMaterialEstimation.GetRow.Cells("MaterialArticleId").Value
                frm.Type = "New"
                frm.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub AddToGrid(ByVal Qty As Double, ByVal Type As String, Optional ByVal ArticleId As Integer = 0, Optional ByVal ArticleDescription As String = "", Optional ByVal CostPrice As Double = 0, Optional ByVal DepartmentId As Integer = 0, Optional ByVal Department As String = "")
        Try

            If Type = "New" Then
                Dim dt As DataTable = CType(Me.grdMaterialEstimation.DataSource, DataTable)
                Dim dr As DataRow
                dr = dt.NewRow
                dr("Id") = 0
                dr("TicketId") = PlanTicketsMasterID
                dr("CostPrice") = CostPrice
                dr("DepartmentId") = DepartmentId
                dr("Department") = Department
                dr("MaterialArticleId") = ArticleId
                dr("Material") = ArticleDescription
                dr("Qty") = Qty
                dr("Type") = "Plus"
                dr("NoOfBatches") = Val(Me.grdMaterialEstimation.GetRow.Cells("NoOfBatches").Value.ToString)
                dt.Rows.InsertAt(dr, Me.grdMaterialEstimation.GetRow.RowIndex + 1)
            Else
                Dim dt As DataTable = CType(Me.grdMaterialEstimation.DataSource, DataTable)
                Dim dr As DataRow
                dr = dt.NewRow
                dr("Id") = 0
                dr("TicketId") = PlanTicketsMasterID
                dr("CostPrice") = Me.grdMaterialEstimation.GetRow.Cells("CostPrice").Value
                dr("DepartmentId") = Me.grdMaterialEstimation.GetRow.Cells("DepartmentId").Value
                dr("Department") = Me.grdMaterialEstimation.GetRow.Cells("Department").Value.ToString
                dr("MaterialArticleId") = Me.grdMaterialEstimation.GetRow.Cells("MaterialArticleId").Value
                dr("Material") = Me.grdMaterialEstimation.GetRow.Cells("Material").Value.ToString
                dr("Qty") = Qty
                dr("Type") = Type
                dr("NoOfBatches") = Val(Me.grdMaterialEstimation.GetRow.Cells("NoOfBatches").Value.ToString)
                dt.Rows.InsertAt(dr, Me.grdMaterialEstimation.GetRow.RowIndex + 1)
            End If
            'If Type = ADDITION_TYPE Then
            '    dr("Type") = ADDITION_TYPE
            'ElseIf Type = SUBTRACTION_TYPE Then
            '    dr("Type") = SUBTRACTION_TYPE
            'End If
            'MaterialDetail.Id = Me.grdMaterialEstimation.GetRows(i).Cells("Id").Value
            'MaterialDetail.TicketId = PlanTicketsMasterID
            'MaterialDetail.CostPrice = Me.grdMaterialEstimation.GetRows(i).Cells("CostPrice").Value
            'MaterialDetail.DepartmentId = Me.grdMaterialEstimation.GetRows(i).Cells("DepartmentId").Value
            ''MaterialDetail.FinishGoodId = Me.cmbBatches.ActiveRow.Cells("Id").Value
            'MaterialDetail.MaterialArticleId = Me.grdMaterialEstimation.GetRows(i).Cells("MaterialArticleId").Value
            'MaterialDetail.Qty = Me.grdMaterialEstimation.GetRows(i).Cells("Qty").Value
            'MaterialDetail.Type = Me.grdMaterialEstimation.GetRows(i).Cells("Type").Value.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnNew_Click_1(sender As Object, e As EventArgs)
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Public Sub DisplayTicket(ByVal dt As DataTable)
        Try
            IsEditMode = True
            If Not Me.grdMaster.RowCount > 0 Then Exit Sub
            'If Me.grdDetail.RowCount > 0 Then
            '    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            'End If
            Me.txtTicketNo.Text = dt.Rows(0).Item(HistoryGrid.TicketNo).ToString
            ParentTicketNo = dt.Rows(0).Item(HistoryGrid.ParentTicketNo).ToString
            Me.dtpDate.Value = dt.Rows(0).Item(HistoryGrid.TicketDate)
            PlanTicketsMasterID = dt.Rows(0).Item(HistoryGrid.TicketId)
            Me.cmbCustomer.Value = Val(dt.Rows(0).Item(HistoryGrid.CustomerId).ToString)
            Me.cmbSalesOrder.SelectedValue = Val(dt.Rows(0).Item(HistoryGrid.SalesOrderId).ToString)
            Me.cmbPlan.SelectedValue = Val(dt.Rows(0).Item(HistoryGrid.PlanId).ToString)
            Me.cmbTicketProduct.Value = Val(dt.Rows(0).Item(HistoryGrid.MasterArticleId).ToString)
            Me.txtSpecialInstructions.Text = dt.Rows(0).Item("SpecialInstructions").ToString()
            Me.txtBatchNo.Text = dt.Rows(0).Item(HistoryGrid.BatchNo).ToString()
            Me.txtBatchSize.Text = Val(dt.Rows(0).Item(HistoryGrid.BatchSize).ToString)
            Me.txtNoOfBatches.Text = Val(dt.Rows(0).Item(HistoryGrid.NoOfBatches).ToString)

            If IsDBNull(dt.Rows(0).Item(HistoryGrid.ExpiryDate)) Then
                Me.dtpExpiryDate.Value = Now
            Else
                Me.dtpExpiryDate.Value = dt.Rows(0).Item(HistoryGrid.ExpiryDate)
            End If
            FillCombos("Item")
            FillCombos("Stages")
            FillCombos("grdPacking")
            FillCombos("Batches")
            Me.cmbBatches.Value = Val(dt.Rows(0).Item(HistoryGrid.FinishGoodId).ToString)
            Me.GetDetail(PlanTicketsMasterID)
            Me.GetStages(PlanTicketsMasterID)
            DisplayMaterialDetail(PlanTicketsMasterID)


            'Me.txtNQP.Text = Val(Me.grdMaster.CurrentRow.Cells(HistoryGrid.BatchSize).Value.ToString)

            Me.BtnSave.Text = "&Update"
            Me.BtnDelete.Visible = True
            'FillCombos("TicketProduct")
            Me.txtTicketQty.Text = ""
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rbCode_CheckedChanged_1(sender As Object, e As EventArgs) Handles rbCode.CheckedChanged
        Try
            If Me.rbCode.Checked = True Then
                If Not cmbItem.ActiveRow Is Nothing Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub rbName_CheckedChanged_1(sender As Object, e As EventArgs) Handles rbName.CheckedChanged

        Try
            If Me.rbName.Checked = True Then
                If Not cmbItem.ActiveRow Is Nothing Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub grdStages_ColumnButtonClick_1(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdStages.ColumnButtonClick

        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                'Dim Id = Me.grdMaterialEstimation.GetRow.Cells("Id").Value
                'If Id > 0 Then
                '    If PlanTicketsStandardDAL.DeleteMaterialDetail(Id) Then

                '    End If
                'End If
                msg_Information("Row has been deleted.")
                Me.grdStages.GetRow.Delete()

                'ElseIf e.Column.Key = "Add" Then
                '    Dim frm As New frmAddSub
                '    frm.Grid.DataSource = Me.grdMaterialEstimation.DataSource
                '    frm.PlanItemId = Me.cmbTicketProduct.Value.ToString
                '    frm.RawMaterialId = Me.grdMaterialEstimation.GetRow.Cells("MaterialArticleId").Value
                '    frm.Type = "Plus"
                '    frm.ShowDialog()
                'ElseIf e.Column.Key = "Sub" Then
                '    Dim frm As New frmAddSub
                '    frm.Grid.DataSource = Me.grdMaterialEstimation.DataSource
                '    frm.PlanItemId = Me.cmbTicketProduct.Value.ToString
                '    frm.RawMaterialId = Me.grdMaterialEstimation.GetRow.Cells("MaterialArticleId").Value

                '    frm.Type = "Minus"
                '    frm.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub grdDetail_ColumnButtonClick_1(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.ColumnButtonClick

        Try
            If e.Column.Key = "Delete" Then
                If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                'Dim Id = Me.grdMaterialEstimation.GetRow.Cells("Id").Value
                'If Id > 0 Then
                '    If PlanTicketsStandardDAL.DeleteMaterialDetail(Id) Then

                '    End If
                'End If
                'Dim filterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("ArticleDetailId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdDetail.GetRow.Cells("ArticleId").Value)
                'Dim filterCondition1 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("PackingId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdDetail.GetRow.Cells("PackingId").Value)
                'Dim filterCondition2 As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdMaterialEstimation.RootTable.Columns("PackingI"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.grdDetail.GetRow.Cells("PackingId").Value)
                'filterCondition.AddCondition(filterCondition1)
                ''filterCondition.a
                'Dim IsFound As Integer = Me.grdMaterialEstimation.FindAll(filterCondition)
                'If IsFound > 0 Then
                '    Me.grdMaterialEstimation.GetRow.Delete()
                'End If
                For Each _Row As Janus.Windows.GridEX.GridEXRow In Me.grdMaterialEstimation.GetRows
                    _Row.BeginEdit()
                    If _Row.Cells("DetailArticleId").Value = Me.grdDetail.GetRow.Cells("ArticleId").Value AndAlso _Row.Cells("PackingId").Value = Me.grdDetail.GetRow.Cells("PackingId").Value Then
                        _Row.Delete()
                    End If
                    _Row.EndEdit()
                Next
                msg_Information("Row has been deleted.")
                Me.grdDetail.GetRow.Delete()

                'ElseIf e.Column.Key = "Add" Then
                '    Dim frm As New frmAddSub
                '    frm.Grid.DataSource = Me.grdMaterialEstimation.DataSource
                '    frm.PlanItemId = Me.cmbTicketProduct.Value.ToString
                '    frm.RawMaterialId = Me.grdMaterialEstimation.GetRow.Cells("MaterialArticleId").Value
                '    frm.Type = "Plus"
                '    frm.ShowDialog()
                'ElseIf e.Column.Key = "Sub" Then
                '    Dim frm As New frmAddSub
                '    frm.Grid.DataSource = Me.grdMaterialEstimation.DataSource
                '    frm.PlanItemId = Me.cmbTicketProduct.Value.ToString
                '    frm.RawMaterialId = Me.grdMaterialEstimation.GetRow.Cells("MaterialArticleId").Value

                '    frm.Type = "Minus"
                '    frm.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnPrintMaterialEstimation_Click(sender As Object, e As EventArgs) Handles btnPrintMaterialEstimation.Click

        Try

            GetCrystalReportRights()
            AddRptParam("@PlanTicketsMasterID", Me.grdMaster.CurrentRow.Cells(HistoryGrid.TicketId).Value)
            ShowReport("MaterialEstimationReport")

        Catch ex As Exception

            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    'Private Sub costcenterSave()

    '    Try

    '        Dim cm As New OleDb.OleDbCommand
    '        '   Dim con As New OleDb.OleDbConnection(con.ConnectionString)
    '        cm.CommandText = "insert into tblDefCostCenter(Name,Code,sortorder, CostCenterGroup, Active, OutwardGatepass, DayShift, LCDocId) values(N'" & txtTicketNo.Text.Replace("'", "''") & "','" & "Roger" & "','" & 1 & "', N'" & " " & "', " & 1 & ", " & 0 & ", " & 0 & " , " & 0 & ")"
    '        cm.Connection = Con
    '        If Not Con.State = ConnectionState.Open Then Con.Open()
    '        cm.ExecuteNonQuery()

    '        'msg_Information(str_informSave)
    '        Con.Close()
    '        'End If '''01-Mar-2014 Task:2449 Cost Center New Record Not Create s
    '    Catch ex As Exception

    '        ShowErrorMessage(ex.Message)
    '    Finally
    '        If Con.State = ConnectionState.Open Then Con.Close()

    '    End Try
    'End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Try
            If Not Me.cmbSubDepartment.SelectedIndex = -0 Then
                'If Me.cmbSubDepartment.SelectedValue < 1 Then
                'ShowErrorMessage("Please select department")
                'Exit Sub
                'End If
                Dim frm As New frmAddSub
                frm.Grid.DataSource = Me.grdMaterialEstimation.DataSource
                frm.PlanItemId = Me.cmbTicketProduct.Value.ToString
                frm.RawMaterialId = Me.grdMaterialEstimation.GetRow.Cells("MaterialArticleId").Value
                frm.Type = "New"
                frm.DepartmentId = Me.cmbSubDepartment.SelectedValue
                frm.Department = Me.cmbSubDepartment.Text
                frm.ShowDialog()

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task 3440 Saad Afzaal Add function UpdateRecord call when PlanTicket is open as pop-up on Production Control Form

    Private Sub UpdateRecord()

        Me.IsEditMode = True

        'Task 3440 Saad Afzaal Select the current row of grdmaster Grid according to provided TicketId 
        Dim flag As Boolean = False
        flag = Me.grdMaster.FindAll(Me.grdMaster.RootTable.Columns(HistoryGrid.TicketId), Janus.Windows.GridEX.ConditionOperator.Equal, Me.Ticket_Id)

        Me.txtTicketNo.Text = Me.grdMaster.CurrentRow.Cells(HistoryGrid.TicketNo).Value.ToString
        ParentTicketNo = Me.grdMaster.CurrentRow.Cells(HistoryGrid.ParentTicketNo).Value.ToString
        Me.dtpDate.Value = Me.grdMaster.CurrentRow.Cells(HistoryGrid.TicketDate).Value
        PlanTicketsMasterID = Me.grdMaster.CurrentRow.Cells(HistoryGrid.TicketId).Value
        Me.cmbCustomer.Value = Me.grdMaster.CurrentRow.Cells(HistoryGrid.CustomerId).Value
        Me.cmbSalesOrder.SelectedValue = Me.grdMaster.CurrentRow.Cells(HistoryGrid.SalesOrderId).Value
        Me.cmbPlan.SelectedValue = Me.grdMaster.CurrentRow.Cells(HistoryGrid.PlanId).Value
        'Me.cmbTicketProduct.Value = Me.grdMaster.CurrentRow.Cells(HistoryGrid.MasterArticleId).Value

        Me.MasterArticleId = Me.grdMaster.CurrentRow.Cells(HistoryGrid.MasterArticleId).Value

        Me.MasterArticleId = Me.grdMaster.CurrentRow.Cells(HistoryGrid.MasterArticleId).Value
        Me.txtSpecialInstructions.Text = Me.grdMaster.CurrentRow.Cells(HistoryGrid.Remarks).Value.ToString()
        Me.txtBatchNo.Text = Me.grdMaster.CurrentRow.Cells(HistoryGrid.BatchNo).Value.ToString()
        Me.txtBatchSize.Text = Val(Me.grdMaster.CurrentRow.Cells(HistoryGrid.BatchSize).Value.ToString)
        Me.txtNoOfBatches.Text = Val(Me.grdMaster.CurrentRow.Cells(HistoryGrid.NoOfBatches).Value.ToString)

        If IsDBNull(Me.grdMaster.CurrentRow.Cells(HistoryGrid.ExpiryDate).Value) Then
            Me.dtpExpiryDate.Value = Now
        Else
            Me.dtpExpiryDate.Value = Me.grdMaster.CurrentRow.Cells(HistoryGrid.ExpiryDate).Value
        End If
        FillCombos("Item")
        FillCombos("Stages")
        FillCombos("grdPacking")
        FillCombos("Batches")
        'Me.cmbBatches.Value = Me.grdMaster.CurrentRow.Cells(HistoryGrid.FinishGoodId).Value
        Me.GetDetail(PlanTicketsMasterID)
        Me.GetStages(PlanTicketsMasterID)
        DisplayMaterialDetail(PlanTicketsMasterID)

        'Me.txtNQP.Text = Val(Me.grdMaster.CurrentRow.Cells(HistoryGrid.BatchSize).Value.ToString)

        Me.BtnSave.Text = "&Update"
        Me.BtnDelete.Visible = True
        'FillCombos("TicketProduct")
        Me.txtTicketQty.Text = ""
        Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab

        If PlanTicketsStandardDAL.CheckTicket(PlanTicketsMasterID) = True Then
            txtBatchNo.ReadOnly = True
        Else
            txtBatchNo.ReadOnly = False

        End If

    End Sub

    Private Sub UltraTabControl1_SelectedTabChanging(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged_1(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabPageControl2_TabIndexChanged(sender As Object, e As EventArgs) Handles UltraTabPageControl2.TabIndexChanged
        Try
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPlanTicketStandard_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
